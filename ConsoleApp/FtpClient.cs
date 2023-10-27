using MyClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class FtpClient
    {
        private string _host;
        private int _port;
        private Socket _socket;
        public FtpClient(string host, int port)
        {
            _host = host;
            _port = port;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public void Connect()
        {
            _socket.Connect(_host, _port);
            Console.WriteLine($"Đã kết nối tới {_host}:{_port}");
        }

        public void Disconnect()
        {
            _socket.Close();
            Console.WriteLine($"Đã ngắt kết nối tới {_host}:{_port}");
        }

        public void SendFile(string filePath)
        {
            if (IsExistFilePath(filePath) == false)
            {
                throw new Exception("Đường dẫn tệp tin không tồn tại");
            }
            SendAction("post");
            SendFileName(filePath);
            SendFileData(filePath);
        }

        private void SendAction(string action)
        {
            byte[] actionData = Encoding.UTF8.GetBytes(action);

            _socket.Send(BitConverter.GetBytes(actionData.Length));
            _socket.Send(actionData);
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
                    _socket.Send(data, length, SocketFlags.None);
                }
            }
            Console.WriteLine("Đã gửi tệp tin thành công");
        }

        private bool IsExistFilePath(string filePath)
        {
            return File.Exists(filePath);
        }

        public void ReceiveFile(string fileName)
        {
            SendAction("get");
            SendFileName(fileName);

            // Kiểm tra xem có tệp tin nào trùng tên không
            FileManager fileManager = new FileManager(@$"C:\Users\TUAN\OneDrive\Máy tính\FileClient\");
            string destinationFilePath = fileManager.HandleDuplicatedFileName($"{fileName}");

            ReceiveFileData(destinationFilePath);
        }

        private void ReceiveFileData(string filePath)
        {
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
    }
}
