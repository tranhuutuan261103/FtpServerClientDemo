using MyClassLibrary;
using MyClassLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyFtpServer
{
    public class FileServerProcessing
    {
        private TcpClient _socket;

        public FileServerProcessing(TcpClient socket, string rootPath)
        {
            _socket = socket;
        }

        public void SendFile(string filePath)
        {
            if (IsExistFilePath(filePath) == false)
            {
                throw new Exception("Đường dẫn tệp tin không tồn tại");
            }

            NetworkStream ns = _socket.GetStream();
            int blocksize = 1024;
            byte[] buffer = new byte[blocksize];
            int byteread = 0;
            lock (this)
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                while (true)
                {
                    byteread = fs.Read(buffer, 0, blocksize);
                    ns.Write(buffer, 0, byteread);
                    if (byteread == 0)
                    {
                        break;
                    }
                }
                ns.Flush();
                ns.Close();
            }
        }

        public void ReceiveFile(string filePath)
        {
            if (IsExistFilePath(filePath) == true)
            {
                throw new Exception("Đường dẫn tệp tin đã tồn tại");
            }
            NetworkStream ns = _socket.GetStream();
            int blocksize = 1024;
            byte[] buffer = new byte[blocksize];
            int byteread = 0;
            lock (this)
            {
                FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                while (true)
                {
                    byteread = ns.Read(buffer, 0, blocksize);
                    fs.Write(buffer, 0, byteread);
                    if (byteread == 0)
                    {
                        break;
                    }
                }
                fs.Flush();
                fs.Close();
            }
        }

        public void SendList(List<FileInfor> fileInfors)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(fileInfors);
            var bytes = Encoding.UTF8.GetBytes(json);
            NetworkStream ns = _socket.GetStream();
            ns.Write(bytes, 0, bytes.Length);
            ns.Flush();
            ns.Close();
        }

        private bool IsExistFilePath(string filePath)
        {
            return System.IO.File.Exists(filePath);
        }
    }
}
