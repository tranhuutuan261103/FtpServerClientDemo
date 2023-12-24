using MyClassLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class DataTransferClient
    {
        private TcpClient _tcpClient;
        private StreamWriter _writer;
        private StreamReader _reader;

        public delegate void FileClientProcessingEventHandler(FileTransferProcessing sender);
        public event FileClientProcessingEventHandler FileClientProcessingEvent;

        public DataTransferClient(TcpClient tcpClient, FileClientProcessingEventHandler FileClientProcessingEvent, FileTransferProcessing processing)
        {
            _tcpClient = tcpClient;
            _writer = new StreamWriter(_tcpClient.GetStream()) { AutoFlush = true };
            _reader = new StreamReader(_tcpClient.GetStream());
            this.FileClientProcessingEvent = FileClientProcessingEvent;
            this.Processing = processing;

            Thread thread = new Thread(UpdateProcessing);
            thread.Start();
        }

        public DataTransferClient(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
            _writer = new StreamWriter(_tcpClient.GetStream()) { AutoFlush = true };
            _reader = new StreamReader(_tcpClient.GetStream());
        }

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

        public FileInforPackage ReceiveFileInforPackage()
        {
            NetworkStream ns = _tcpClient.GetStream();
            byte[] buffer = new byte[1024];
            int byteread = 0;
            string data = "";
            while (true)
            {
                byteread = ns.Read(buffer, 0, 1024);
                data += Encoding.ASCII.GetString(buffer, 0, byteread);
                if (byteread == 0)
                {
                    break;
                }
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<FileInforPackage>(data) ?? new FileInforPackage();
        }

        public List<FileInfor> ReceiveListRemoteFiles()
        {
            NetworkStream ns = _tcpClient.GetStream();
            byte[] buffer = new byte[1024];
            int byteread = 0;
            string data = "";
            while (true)
            {
                byteread = ns.Read(buffer, 0, 1024);
                data += Encoding.ASCII.GetString(buffer, 0, byteread);
                if (byteread == 0)
                {
                    break;
                }
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<FileInfor>>(data) ?? new List<FileInfor>();
        }

        public void ReceiveFile(string fullFilePath)
        {
            NetworkStream ns = _tcpClient.GetStream();
            int blocksize = 1024;
            byte[] buffer = new byte[blocksize];
            int byteread = 0;

            long totalBytesRead = 0;

            Processing.Status = FileTransferProcessingStatus.Downloading;
            FileClientProcessingEvent(Processing);

            using (FileStream fs = new FileStream(fullFilePath, FileMode.Append, FileAccess.Write))
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
                        totalBytesRead += byteread;
                        Processing.SetFileTransferSize(totalBytesRead);
                    }
                }
            }


            Processing.Status = FileTransferProcessingStatus.Completed;
            FileClientProcessingEvent(Processing);
        }

        public void SendFile(string fullFilePath)
        {
            NetworkStream ns = _tcpClient.GetStream();
            int blocksize = 1024;
            byte[] buffer = new byte[blocksize];
            int byteread = 0;

            Processing.Status = FileTransferProcessingStatus.Uploading;
            FileClientProcessingEvent(Processing);

            long totalBytesRead = 0;
            

            FileStream fs = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read);
            Processing.FileSize = fs.Length;
            while (true)
            {
                byteread = fs.Read(buffer, 0, blocksize);
                ns.Write(buffer, 0, byteread);
                totalBytesRead += byteread;
                Processing.SetFileTransferSize(totalBytesRead);
                if (byteread == 0)
                {
                    break;
                }
            }
            ns.Flush();
            ns.Close();
        }
    }
}
