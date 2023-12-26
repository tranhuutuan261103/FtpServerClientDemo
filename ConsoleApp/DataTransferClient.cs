using MyClassLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyFtpClient
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
            Processing = processing;

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
                    if (Processing.Status == FileTransferProcessingStatus.Completed ||
                        Processing.Status == FileTransferProcessingStatus.Failed)
                    {
                        break;
                    }
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
                data += Encoding.UTF8.GetString(buffer, 0, byteread);
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

        public async Task ReceiveFileAsync(string fullFilePath)
        {
            NetworkStream ns = _tcpClient.GetStream();
            int blocksize = 1024;
            byte[] buffer = new byte[blocksize];
            int bytesRead = 0;

            long totalBytesRead = 0;

            Processing.Status = FileTransferProcessingStatus.Downloading;
            FileClientProcessingEvent(Processing);

            try
            {
                using (FileStream fs = new FileStream(fullFilePath, FileMode.Append, FileAccess.Write))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        while ((bytesRead = await ns.ReadAsync(buffer, 0, blocksize)) > 0)
                        {
                            bw.Write(buffer, 0, bytesRead);
                            totalBytesRead += bytesRead;
                            Processing.SetFileTransferSize(totalBytesRead);
                        }
                    }
                }

                Processing.Status = FileTransferProcessingStatus.Completed;
                FileClientProcessingEvent(Processing);
            }
            catch (Exception)
            {
                Processing.Status = FileTransferProcessingStatus.Failed;
                FileClientProcessingEvent(Processing);
            }
        }


        public async Task SendFileAsync(string fullFilePath)
        {
            NetworkStream ns = _tcpClient.GetStream();
            int blocksize = 1024;
            byte[] buffer = new byte[blocksize];
            int bytesRead = 0;

            Processing.Status = FileTransferProcessingStatus.Uploading;
            FileClientProcessingEvent(Processing);

            long totalBytesRead = 0;

            try
            {
                using (FileStream fs = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read))
                {
                    Processing.FileSize = fs.Length;

                    while ((bytesRead = await fs.ReadAsync(buffer, 0, blocksize)) > 0)
                    {
                        await ns.WriteAsync(buffer, 0, bytesRead);
                        totalBytesRead += bytesRead;
                        Processing.SetFileTransferSize(totalBytesRead);
                    }
                }
            }
            catch (Exception)
            {
                Processing.Status = FileTransferProcessingStatus.Failed;
                FileClientProcessingEvent(Processing);
                return;
            }
            finally
            {
                ns.Flush();
                ns.Close();
            }
        }

    }
}
