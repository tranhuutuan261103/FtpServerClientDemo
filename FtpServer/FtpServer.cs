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

                    FileServerProcessing fileServerProcessing = new FileServerProcessing(clientSocket, _rootPath); 
                    fileServerProcessing.Execute();
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
