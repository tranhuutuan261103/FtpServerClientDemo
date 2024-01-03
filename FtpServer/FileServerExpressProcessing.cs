using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyFtpServer
{
    public class FileServerExpressProcessing
    {
        private readonly TcpListener _tcpListener;
        private string _fullPath;
        private long _length;
        private long _maxBufferSize = (long)Math.Pow(2, 20) * 1024;
        private int _totalThread;
        private int _currentSuccessThreadCount = 0;
        List<string> partFilePaths = new List<string>();
        public FileServerExpressProcessing(TcpListener tcpListener, string fullPath, long length)
        {
            _tcpListener = tcpListener;
            _fullPath = fullPath;
            _length = length;
        }

        public void ReceiveExpressFile()
        {
            _totalThread = (int)Math.Ceiling((double)_length / _maxBufferSize);

            int currentThreadCount = 0;
            while (true)
            {
                if (currentThreadCount == _totalThread)
                {
                    if (_currentSuccessThreadCount == _totalThread)
                    {
                        Merger();
                        break;
                    }
                }
                else
                {
                    TcpClient client = _tcpListener.AcceptTcpClient();
                    currentThreadCount++;
                    Thread thread = new Thread(() => HandleReceiveExpressFile(client));
                    thread.Start();
                }
            }
        }

        private void HandleReceiveExpressFile(TcpClient client)
        {
            NetworkStream ns = client.GetStream();
            int blocksize = 1024;
            byte[] buffer = new byte[blocksize];
            int byteread;

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
                        byteread = ns.Read(buffer, 0, blocksize);
                        if (byteread == 0)
                        {
                            break;
                        }
                        bw.Write(buffer, 0, byteread);
                    }
                }
            }

            lock (_lock)
            {
                _currentSuccessThreadCount++;
            }
            Console.WriteLine($"Created at {DateTime.Now} in {fullPathPart}");
        }

        public void SendExpressFile()
        {
            int currentThreadCount = 0;
            _totalThread = (int)Math.Ceiling((double)_length / _maxBufferSize);
            while (true)
            {
                if (currentThreadCount == _totalThread)
                {
                    break;
                }
                else
                {
                    TcpClient client = _tcpListener.AcceptTcpClient();
                    long offset = (long)currentThreadCount * _maxBufferSize;
                    Thread thread = new Thread(() => HandleSendExpressFile(client, offset, _maxBufferSize));
                    thread.Start();
                    currentThreadCount++;
                }
            }
        }

        private void HandleSendExpressFile(TcpClient client, long offset, long length)
        {
            NetworkStream ns = client.GetStream();
            int blocksize = 1024;
            byte[] buffer = new byte[blocksize];
            int byteread;

            long i = 0;
            using (FileStream fs = new FileStream(_fullPath, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(offset, SeekOrigin.Begin);
                while (i < length)
                {
                    byteread = fs.Read(buffer, 0, (int)Math.Min(buffer.Length, length - i));
                    ns.Write(buffer, 0, byteread);
                    
                    if (byteread == 0)
                    {
                        break;
                    }

                    i += byteread;
                }
            }
            ns.Close();
        }

        private object _lock = new object();
        private object _lock2 = new object();

        private void Merger()
        {
            Console.WriteLine($"Merging... at {DateTime.Now}");

            using (FileStream finalFileStream = new FileStream(_fullPath, FileMode.OpenOrCreate, FileAccess.Write))
            using (BinaryWriter bw = new BinaryWriter(finalFileStream))
            {
                foreach (var partFilePath in partFilePaths)
                {
                    using (FileStream tempFileStream = new FileStream(partFilePath, FileMode.Open, FileAccess.Read))
                    using (BinaryReader br = new BinaryReader(tempFileStream))
                    {
                        byte[] buffer = new byte[1024];
                        int bytesRead;
                        while ((bytesRead = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, bytesRead);
                        }
                    }

                    File.Delete(partFilePath);
                }
            }

            partFilePaths.Clear();
            Console.WriteLine($"Created at {DateTime.Now} in {_fullPath}");
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
    }
}
