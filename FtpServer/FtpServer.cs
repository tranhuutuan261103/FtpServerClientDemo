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

        private void ProcessCommand(Socket socket, string command)
        {
            FileServerProcessing processing = new FileServerProcessing(socket, _rootPath);
            switch (command)
            {
                case "get":
                    processing.SendFile();
                    break;
                case "post":
                    processing.ReceiveFile();
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
                Stop();
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
        }

        public void Stop()
        {
            _socket.Close();
        }
    }
}
