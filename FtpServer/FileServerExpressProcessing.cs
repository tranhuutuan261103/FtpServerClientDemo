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
        private byte[] _data;
        private long length;
        private int maxBufferSize = (int)Math.Pow(2, 20) * 256;
        private int totalThread;
        private int currentSuccessThreadCount = 0;
        List<string> partFilePaths = new List<string>();
        public FileServerExpressProcessing(TcpListener tcpListener, long length)
        {
            _tcpListener = tcpListener;
            _data = new byte[length];
            this.length = length;
        }

        public void ReceiveExpressFile(string fullPath, long length)
        {
            totalThread = (int)Math.Ceiling((double)length / maxBufferSize);

            int i = 0;
            while (true)
            {
                if (i == totalThread)
                {
                    if (currentSuccessThreadCount == totalThread)
                    {
                        Merger(fullPath);
                        break;
                    }
                }
                else
                {
                    TcpClient client = _tcpListener.AcceptTcpClient();
                    IPEndPoint iPEndPoint = (IPEndPoint)client.Client.RemoteEndPoint;
                    Console.WriteLine($"Client {iPEndPoint.Address}:{iPEndPoint.Port} connected");
                    int index = i;
                    Thread thread = new Thread(() => HandleReceiveExpressFile(index, client, fullPath));
                    i++;
                    thread.Start();
                }
            }
        }

        private void HandleReceiveExpressFile(int index, TcpClient client, string fullPath)
        {
            NetworkStream ns = client.GetStream();
            int blocksize = 1024;
            byte[] buffer = new byte[blocksize];
            int byteread = 0;

            string fullPath2 = HandleFileName(fullPath, index);
            lock (_lock2)
            {
                partFilePaths.Add(fullPath2);
            }
            
            using (FileStream fs = new FileStream(fullPath2, FileMode.Create, FileAccess.Write))
            {
                while (true)
                {
                    byteread = ns.Read(buffer, 0, blocksize);
                    fs.Write(buffer, 0, byteread);
                    if (byteread == 0)
                    {
                        break;
                    }
                }
            }
            lock (_lock)
            {
                currentSuccessThreadCount++;
            }
            Console.WriteLine($"Created at {DateTime.Now} in {fullPath2}");
        }

        private object _lock = new object();
        private object _lock2 = new object();

        private void Merger(string fullPath)
        {
            Console.WriteLine($"Merging... at {DateTime.Now}");
            using (FileStream finalFileStream = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                foreach (var partFilePath in partFilePaths)
                {
                    using (FileStream tempFileStream = new FileStream(partFilePath, FileMode.Open))
                    {
                        byte[] buffer = new byte[1024 * 1024];
                        int bytesRead;
                        while ((bytesRead = tempFileStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            finalFileStream.Write(buffer, 0, bytesRead);
                        }
                    }
                }
            }
            Console.WriteLine($"Created at {DateTime.Now} in {fullPath}");
        }

        private string HandleFileName(string filePath, int index)
        {
            string? folderPath = Path.GetDirectoryName(filePath);
            if (folderPath == null)
            {
                throw new Exception("Folder path is null");
            }
            string fileName = Path.GetFileName(filePath);
            string fileNameWithoutExtension = fileName.Split('.').First();
            string fileExtension = fileName.Split('.').Last();
            fileName = $"{fileNameWithoutExtension}({index++}).{fileExtension}";
            filePath = @$"{folderPath}\{fileName}";

            return filePath;
        }
    }
}
