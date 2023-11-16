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
            NetworkStream ns = _socket.GetStream();
            int blocksize = 1024;
            byte[] buffer = new byte[blocksize];
            int byteread = 0;
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ReceiveFile(string filePath)
        {
            if (IsExistFilePath(filePath) == true)
            {
                filePath = HandleDuplicatedFileName(filePath);
            }

            NetworkStream ns = _socket.GetStream();
            int blocksize = 1024;
            byte[] buffer = new byte[blocksize];
            int byteread = 0;
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
            try
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
                fs.Flush();
                fs.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
            return File.Exists(filePath);
        }

        public string HandleDuplicatedFileName(string filePath)
        {
            string? folderPath = Path.GetDirectoryName(filePath);
            if (folderPath == null)
            {
                throw new Exception("Folder path is null");
            }
            string fileName = Path.GetFileName(filePath);
            string fileNameWithoutExtension = fileName.Split('.').First();
            string fileExtension = fileName.Split('.').Last();
            int i = 0;
            while (File.Exists(filePath))
            {
                fileName = $"{fileNameWithoutExtension} ({i++}).{fileExtension}";
                filePath = @$"{folderPath}{fileName}";
            }
            return filePath;
        }
    }
}
