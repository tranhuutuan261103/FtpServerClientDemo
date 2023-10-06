using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyFtpServer
{
    public class FtpServer
    {
        private string _host;
        private int _port;
        private Socket _socket;
        private readonly string _rootPath = @"D:\FileServer\";

        public FtpServer(string host, int port)
        {
            _host = host;
            _port = port;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Parse(_host), _port));
        }

        private void ReceiveFile(Socket socket)
        {
            byte[] fileNameLengthData = new byte[4];
            socket.Receive(fileNameLengthData); // Nhận độ dài của tên tệp

            int fileNameLength = BitConverter.ToInt32(fileNameLengthData, 0);
            byte[] fileNameData = new byte[fileNameLength];
            socket.Receive(fileNameData); // Nhận dữ liệu tên tệp
            string fileName = Encoding.UTF8.GetString(fileNameData);
            string filePath = HandleDuplicatedFileName($"{_rootPath}{fileName}");

            byte[] data = new byte[1024];
            int length;
            using(FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                while ((length = socket.Receive(data)) > 0)
                {
                    fs.Write(data, 0, length);
                }
            }
            Console.WriteLine("Đã nhận tệp tin thành công");
        }

        private void SendFile(Socket socket)
        {
            byte[] fileNameLengthData = new byte[4];
            socket.Receive(fileNameLengthData); // Nhận độ dài của tên tệp

            int fileNameLength = BitConverter.ToInt32(fileNameLengthData, 0);
            byte[] fileNameData = new byte[fileNameLength];
            socket.Receive(fileNameData); // Nhận dữ liệu tên tệp
            string fileName = Encoding.UTF8.GetString(fileNameData);
            string filePath = $"{_rootPath}{fileName}";
            
            if (IsExistFilePath(filePath) == false)
            {
                throw new Exception("Đường dẫn tệp tin không tồn tại");
            }
            SendFileData(socket, filePath);
        }

        private void SendFileName(Socket socket, string filePath)
        {
            string fileName = filePath.Split('\\').Last();
            byte[] fileNameData = Encoding.UTF8.GetBytes(fileName);
            byte[] fileNameLengthData = BitConverter.GetBytes(fileNameData.Length);
            socket.Send(fileNameLengthData); // Gửi độ dài của tên tệp
            socket.Send(fileNameData); // Gửi dữ liệu tên tệp
        }

        private void SendFileData(Socket socket, string filePath)
        {
            byte[] data = new byte[1024];
            int length;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                while ((length = fs.Read(data, 0, data.Length)) > 0)
                {
                    socket.Send(data, 0, length, SocketFlags.None);
                }
            }
        }

        private bool IsExistFilePath(string filePath)
        {
            return File.Exists(filePath);
        }

        private void ProcessCommand(Socket socket, string command)
        {
            switch (command)
            {
                case "get":
                    SendFile(socket);
                    break;
                case "put":
                    ReceiveFile(socket);
                    break;
                default:
                    throw new Exception("Lệnh không hợp lệ");
            }
        }

        public void Start()
        {
            Console.WriteLine($"FTP Server đang chạy trên {_host}:{_port}");
            try
            {
                _socket.Listen(10);
                while (true)
                {
                    Socket clientSocket = _socket.Accept();
                    Console.WriteLine($"Đã kết nối với {clientSocket.RemoteEndPoint}");

                    // Nhận lệnh
                    byte[] lengthCommand = new byte[4];
                    clientSocket.Receive(lengthCommand);

                    int command = BitConverter.ToInt32(lengthCommand, 0);
                    byte[] commandData = new byte[command];
                    clientSocket.Receive(commandData);

                    ProcessCommand(clientSocket, Encoding.UTF8.GetString(commandData));

                    clientSocket.Close();
                }
            }
            catch (Exception ex)
            {
                _socket.Close();
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
        }

        private string HandleDuplicatedFileName(string filePath)
        {
            string fileName = filePath.Split('\\').Last();
            string fileNameWithoutExtension = fileName.Split('.').First();
            string fileExtension = fileName.Split('.').Last();
            int i = 1;
            while (File.Exists(filePath))
            {
                fileName = $"{fileNameWithoutExtension}({i}).{fileExtension}";
                filePath = $"{_rootPath}{fileName}";
                i++;
            }
            return filePath;
        }

        public void Stop()
        {
            _socket.Close();
        }
    }
}
