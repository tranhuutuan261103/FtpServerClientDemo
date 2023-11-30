using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MyClassLibrary.Common;

namespace ConsoleApp
{
    public class DataExpressTransferClient
    {
        private IPEndPoint server_data_endpoint;
        private string _fullPath;
        private int _maxBufferSize = (int)Math.Pow(2, 20) * 512;
        private long _fileSize;
        private long totalBytesRead = 0;

        private object _totalBytesReadLock = new object();

        public DataExpressTransferClient(IPEndPoint server_data_endpoint, string fullPath, long fileSize, FileClientProcessingEventHandler FileClientProcessingEvent, FileTransferProcessing processing)
        {
            this.server_data_endpoint = server_data_endpoint;
            _fullPath = fullPath;
            _fileSize = fileSize;
            this.FileClientProcessingEvent = FileClientProcessingEvent;
            this.Processing = processing;
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
                    FileClientProcessingEvent(Processing);
                }
                Thread.Sleep(500);
            }
        }

        public void ExpressSendFile()
        {
            for (int i = 0; i < (int)Math.Ceiling((double)_fileSize / _maxBufferSize); i++)
            {
                long currentBlock = i;
                Thread thread = new Thread(() => HandleSendTransfer(currentBlock * _maxBufferSize, _maxBufferSize));
                thread.Start();
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
            }
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

            FileStream fs = new FileStream(fullPathPart, FileMode.Append, FileAccess.Write);
            while (true)
            {
                int bytesRead = ns.Read(bytes, 0, bytes.Length);
                fs.Write(bytes, 0, bytesRead);
                lock (_totalBytesReadLock)
                {
                    totalBytesRead += bytesRead;
                    Processing.SetFileTransferSize(totalBytesRead);
                }
                if (bytesRead == 0)
                {
                    break;
                }
            }
            fs.Close();
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
            using (FileStream finalFileStream = new FileStream(_fullPath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                foreach (var partFilePath in partFilePaths)
                {
                    using (FileStream tempFileStream = new FileStream(partFilePath, FileMode.Open, FileAccess.Read))
                    {
                        byte[] buffer = new byte[1024 * 1024];
                        int bytesRead;
                        while ((bytesRead = tempFileStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            finalFileStream.Write(buffer, 0, bytesRead);
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
