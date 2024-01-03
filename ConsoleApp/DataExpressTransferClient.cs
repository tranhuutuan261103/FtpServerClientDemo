using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MyClassLibrary.Common;
using MyClassLibrary;

namespace MyFtpClient
{
    public class DataExpressTransferClient
    {
        private IPEndPoint server_data_endpoint;
        private string _fullPath;
        private long _maxBufferSize = (int)Math.Pow(2, 20) * 1024;
        private long _fileSize;
        private long totalBytesRead = 0;

        private object _totalBytesReadLock = new object();

        public DataExpressTransferClient(IPEndPoint server_data_endpoint, string fullPath, long fileSize, FileClientProcessingEventHandler FileClientProcessingEvent, FileTransferProcessing processing)
        {
            this.server_data_endpoint = server_data_endpoint;
            _fullPath = fullPath;
            _fileSize = fileSize;
            this.FileClientProcessingEvent = FileClientProcessingEvent;
            Processing = processing;
            Processing.FileSize = fileSize;

            Thread thread = new Thread(UpdateProcessing);
            thread.Start();
        }

        public delegate void FileClientProcessingEventHandler(FileTransferProcessing sender);
        public event FileClientProcessingEventHandler FileClientProcessingEvent;

        private FileTransferProcessing Processing;

        public void UpdateProcessing()
        {
            while (true)
            {
                if (Processing != null)
                {
                    if (Processing.Status == FileTransferProcessingStatus.Completed ||
                    Processing.Status == FileTransferProcessingStatus.Failed)
                    {
                        break;
                    }
                    FileClientProcessingEvent(Processing);
                }
                Thread.Sleep(500);
            }
        }

        public void ExpressSendFile()
        {
            Processing.Status = FileTransferProcessingStatus.Uploading;
            FileClientProcessingEvent(Processing);
            for (int i = 0; i < (int)Math.Ceiling((double)_fileSize / _maxBufferSize); i++)
            {
                long currentBlock = i;
                Thread thread = new Thread(() => HandleSendTransfer(currentBlock * _maxBufferSize, _maxBufferSize));
                thread.Start();
                Thread.Sleep(100);
            }

            while (true)
            {
                if (totalBytesRead == _fileSize)
                {
                    Processing.Status = FileTransferProcessingStatus.Merging;
                    FileClientProcessingEvent(Processing);
                    break;
                }
                Thread.Sleep(100);
            }
        }

        private void HandleSendTransfer(long offset, long length)
        {
            TcpClient client = new TcpClient();
            client.Connect(server_data_endpoint);
            NetworkStream ns = client.GetStream();

            FileStream fs = new FileStream(_fullPath, FileMode.Open, FileAccess.Read);
            fs.Seek(offset, SeekOrigin.Begin);
            byte[] bytes = new byte[1024];
            long i = 0;
            while (i < length)
            {
                int bytesRead = fs.Read(bytes, 0, (int)Math.Min(bytes.Length, length - i));

                ns.Write(bytes, 0, bytesRead);

                if (bytesRead == 0)
                    break;

                i += bytesRead;

                lock (_totalBytesReadLock)
                {
                    totalBytesRead += bytesRead;
                    Processing.SetFileTransferSize(totalBytesRead);
                }
            }
            fs.Close();
            ns.Close();
        }


        private List<string> partFilePaths = new List<string>();
        private long bytesReceived = 0;
        private int _totalThread;
        private int _currentSuccessThreadCount = 0;

        public void ExpressReceiveFile()
        {
            Processing.Status = FileTransferProcessingStatus.Downloading;
            FileClientProcessingEvent(Processing);

            _totalThread = (int)Math.Ceiling((double)_fileSize / _maxBufferSize);
            for (int i = 0; i < _totalThread; i++)
            {
                Thread thread = new Thread(HandleReceiveTransfer);
                thread.Start();
                Thread.Sleep(100);
            }
            while (true)
            {
                if (_currentSuccessThreadCount == _totalThread)
                {
                    Merger();
                    break;
                }
                Thread.Sleep(100);
            }

            Processing.Status = FileTransferProcessingStatus.Completed;
            FileClientProcessingEvent(Processing);
        }

        private void HandleReceiveTransfer()
        {
            TcpClient client = new TcpClient();
            client.Connect(server_data_endpoint);
            NetworkStream ns = client.GetStream();

            byte[] bytes = new byte[1024];

            string fullPathPart = RandomFileName(_fullPath);
            lock (_lock2)
            {
                partFilePaths.Add(fullPathPart);
            }

            using (FileStream fs = new FileStream(fullPathPart, FileMode.Append, FileAccess.Write))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    while (true)
                    {
                        int bytesRead = ns.Read(bytes, 0, bytes.Length);
                        if (bytesRead == 0)
                        {
                            break;
                        }

                        bw.Write(bytes, 0, bytesRead);

                        lock (_totalBytesReadLock)
                        {
                            totalBytesRead += bytesRead;
                            Processing.SetFileTransferSize(totalBytesRead);
                        }
                    }
                }
            }

            lock (_lock)
            {
                _currentSuccessThreadCount++;
            }
            ns.Close();
        }

        private string RandomFileName(string filePath)
        {
            string? folderPath = Path.GetDirectoryName(filePath);
            if (folderPath == null)
            {
                throw new Exception("Folder path is null");
            }
            string fileName = Path.GetFileName(filePath);
            string fileNameWithoutExtension = fileName.Split('.').First();
            string fileExtension = fileName.Split('.').Last();
            fileName = $"{fileNameWithoutExtension}_{Guid.NewGuid()}.{fileExtension}";
            filePath = @$"{folderPath}\{fileName}";

            return filePath;
        }

        private object _lock = new object();
        private object _lock2 = new object();

        private void Merger()
        {
            Console.WriteLine($"Merging... at {DateTime.Now}");
            Processing.Status = FileTransferProcessingStatus.Merging;
            totalBytesRead = 0;
            Processing.SetFileTransferSize(totalBytesRead);
            FileClientProcessingEvent(Processing);

            const int bufferSize = 1024 * 1024; // 1 MB buffer size

            // Handle duplicate file path
            FileManager fileManager1 = new FileManager();
            _fullPath = fileManager1.HandleDuplicateFilePath(_fullPath);
            Processing.FileName = FileManager.GetFileName(_fullPath);

            using (FileStream finalFileStream = new FileStream(_fullPath, FileMode.OpenOrCreate, FileAccess.Write))
            using (BinaryWriter bw = new BinaryWriter(finalFileStream))
            {
                foreach (var partFilePath in partFilePaths)
                {
                    using (FileStream tempFileStream = new FileStream(partFilePath, FileMode.Open, FileAccess.Read))
                    using (BinaryReader br = new BinaryReader(tempFileStream))
                    {
                        byte[] buffer = new byte[bufferSize];
                        int bytesRead;
                        while ((bytesRead = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, bytesRead);
                            totalBytesRead += bytesRead;
                            Processing.SetFileTransferSize(totalBytesRead);
                        }
                    }

                    File.Delete(partFilePath);
                }
            }

            partFilePaths.Clear();
            Console.WriteLine($"Created at {DateTime.Now} in {_fullPath}");
        }

    }
}
