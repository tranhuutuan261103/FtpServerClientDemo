using MyClassLibrary;
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
        private Socket _socket;
        private readonly string _rootPath = @"D:\FileServer\";

        public FileServerProcessing(Socket socket, string rootPath)
        {
            _socket = socket;
            _rootPath = rootPath;
        }

        public void ReceiveFile()
        {
            byte[] fileNameLengthData = new byte[4];
            _socket.Receive(fileNameLengthData); // Nhận độ dài của tên tệp

            int fileNameLength = BitConverter.ToInt32(fileNameLengthData, 0);
            byte[] fileNameData = new byte[fileNameLength];
            _socket.Receive(fileNameData); // Nhận dữ liệu tên tệp
            string fileName = Encoding.UTF8.GetString(fileNameData);

            // Kiểm tra tên tệp có bị trùng không, nếu có thì thêm số vào tên tệp
            FileManager fileManager = new FileManager(_rootPath);
            string filePath = fileManager.HandleDuplicatedFileName($"{fileName}");

            byte[] data = new byte[1024];
            int length;
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                while ((length = _socket.Receive(data)) > 0)
                {
                    fs.Write(data, 0, length);
                }
            }
            Console.WriteLine("Đã nhận tệp tin thành công");
        }

        public void SendFile()
        {
            byte[] fileNameLengthData = new byte[4];
            _socket.Receive(fileNameLengthData); // Nhận độ dài của tên tệp

            int fileNameLength = BitConverter.ToInt32(fileNameLengthData, 0);
            byte[] fileNameData = new byte[fileNameLength];
            _socket.Receive(fileNameData); // Nhận dữ liệu tên tệp
            string fileName = Encoding.UTF8.GetString(fileNameData);
            string filePath = $"{_rootPath}{fileName}";

            if (IsExistFilePath(filePath) == false)
            {
                throw new Exception("Đường dẫn tệp tin không tồn tại");
            }
            SendFileData(filePath);
        }

        private void SendFileName(string filePath)
        {
            string fileName = filePath.Split('\\').Last();
            byte[] fileNameData = Encoding.UTF8.GetBytes(fileName);
            byte[] fileNameLengthData = BitConverter.GetBytes(fileNameData.Length);
            _socket.Send(fileNameLengthData); // Gửi độ dài của tên tệp
            _socket.Send(fileNameData); // Gửi dữ liệu tên tệp
        }

        private void SendFileData(string filePath)
        {
            byte[] data = new byte[1024];
            int length;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                while ((length = fs.Read(data, 0, data.Length)) > 0)
                {
                    _socket.Send(data, 0, length, SocketFlags.None);
                }
            }
            Console.WriteLine($"Đã gửi tệp tin thành công đến {_socket.LocalEndPoint}");
        }

        private bool IsExistFilePath(string filePath)
        {
            return File.Exists(filePath);
        }

        
    }
}
