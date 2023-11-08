using MyClassLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace MyFtpServer
{
    public class FtpServer
    {
        private string _host;
        private int _port;
        private TcpListener _controlSocket;
        private TcpListener[] _passiveSocket;
        private readonly string _rootPath = @"D:\FileServer";
        int _sessionID = 2;

        public FtpServer(string host, int port)
        {
            _host = host;
            _port = port;
            _controlSocket = new TcpListener(IPAddress.Parse(_host), _port);
            _controlSocket.Start();
            _passiveSocket = new TcpListener[1000];
            for(int i = 0; i< 1000; i++)
            {
                try
                {
                    TcpListener listener = new TcpListener(IPAddress.Parse(_host), i + 30000);
                    listener.Start();
                    _passiveSocket[i] = listener;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void Start()
        {
            Console.WriteLine($"FTP Server đang chạy trên {_host}:{_port}");
            try
            {
                while (true)
                {
                    TcpClient client = _controlSocket.AcceptTcpClient();
                    Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient));
                    clientThread.Start(client);
                }
            }
            catch (Exception ex)
            {
                _controlSocket.Stop();
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
        }

        private void HandleClient(object? client)
        {
            if (client == null)
                return;

            int sessionID = GetSessionID();

            TcpClient tcpClient = (TcpClient)client;
            NetworkStream ns = tcpClient.GetStream();
            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns) { AutoFlush = true };

            IPEndPoint iPEndPoint = (IPEndPoint)tcpClient.Client.RemoteEndPoint;

            Console.WriteLine($"Đã kết nối tới {iPEndPoint.Address} có cổng {iPEndPoint.Port}");
            int passivePort = GetPassivePort();
            string remoteFolderPath = @"\";
            try
            {
                while (true)
                {
                    if (!tcpClient.Connected)
                        break;

                    string? request = reader.ReadLine();
                    if (request == null)
                        break;
                    Console.WriteLine($"C> Session{sessionID} {request}");

                    string[] part = request.Split(' ');
                    string command = part[0];
                    if (command == "PASV")
                    {
                        int newPassivePort = GetPassivePort();
                        passivePort = newPassivePort;
                        writer.WriteLine($"227 Entering Passive Mode (127,0,0,1,{passivePort / 256},{passivePort % 256})");
                        Console.WriteLine($"S> Session{sessionID} 227 Entering Passive Mode (127,0,0,1,{passivePort / 256},{passivePort % 256})");
                    }
                    else if (command == "CWD")
                    {
                        string folderPath = part[1];
                        if (folderPath == null)
                        {
                            writer.WriteLine("501 Syntax error in parameters or arguments"); 
                            continue;
                        }
                        if (Directory.Exists(_rootPath + folderPath) == false)
                        {
                            writer.WriteLine("550 Couldn't open the file or directory");
                            continue;
                        }
                        remoteFolderPath = folderPath;
                        writer.WriteLine("250 CWD command successful");
                        Console.WriteLine($"S> Session{sessionID} 250 CWD command successful");
                    }
                    else if (command == "PWD")
                    {
                        writer.WriteLine($"257 \"{remoteFolderPath}\" is current directory.");
                    }
                    else if (command == "LIST")
                    {
                        List<FileInfor> list = new List<FileInfor>();
                        string[] directories = Directory.GetDirectories(_rootPath + remoteFolderPath);
                        foreach (var directory in directories)
                        {
                            DirectoryInfo dirInfo = new DirectoryInfo(directory);
                            list.Add(new FileInfor()
                            {
                                Name = dirInfo.Name,
                                Length = 0,
                                LastWriteTime = dirInfo.LastWriteTime,
                                IsDirectory = true
                            });
                        }
                        string[] files = Directory.GetFiles(_rootPath + remoteFolderPath);

                        foreach (var file in files)
                        {
                            FileInfo fileInfo = new FileInfo(file);
                            list.Add(new FileInfor()
                            {
                                Name = fileInfo.Name,
                                Length = fileInfo.Length,
                                LastWriteTime = fileInfo.LastWriteTime,
                                IsDirectory = false
                            });
                        }

                        TcpClient tcpClient1 = _passiveSocket[passivePort - 30000].AcceptTcpClient();
                        FileServerProcessing processing = new FileServerProcessing(tcpClient1, "");
                        writer.WriteLine("150 Opening data connection");
                        processing.SendList(list);

                        writer.WriteLine("226 Transfer complete");
                    }
                    else if (command == "RETR")
                    {
                        string filePath = part[1];
                        string fullPath = _rootPath + remoteFolderPath + @"\" + filePath;

                        if (!File.Exists(fullPath))
                        {
                            writer.WriteLine("550 File not exist");
                            continue;
                        }

                        TcpClient tcpClient1 = _passiveSocket[passivePort - 30000].AcceptTcpClient();
                        FileServerProcessing processing = new FileServerProcessing(tcpClient1, fullPath);
                        writer.WriteLine("150 Opening data connection");
                        Console.WriteLine($"S> Session{sessionID} 150 Opening data connection");
                        processing.SendFile(fullPath);
                        writer.WriteLine("226 Transfer complete");
                        Console.WriteLine($"S> Session{sessionID} 226 Transfer complete");
                        tcpClient1.Close();
                    }
                    else if (command == "STOR")
                    {
                        string filePath = part[1];
                        string fullPath = _rootPath + remoteFolderPath + @"\" + filePath;
                        TcpClient tcpClient1 = _passiveSocket[passivePort - 30000].AcceptTcpClient();
                        FileServerProcessing processing = new FileServerProcessing(tcpClient1, fullPath);
                        writer.WriteLine("150 Opening data connection");
                        Console.WriteLine($"S> Session{sessionID} 150 Opening data connection");
                        processing.ReceiveFile(fullPath);
                        writer.WriteLine("226 Transfer complete");
                        Console.WriteLine($"S> Session{sessionID} 226 Transfer complete");
                        tcpClient1.Close();
                    }
                    else if (command == "LIST")
                    {
                        /*
                        string fullPath = _rootPath + remoteFolderPath;
                        TcpClient tcpClient1 = _passiveSocket[passivePort - 30000].AcceptTcpClient();
                        FileServerProcessing processing = new FileServerProcessing(tcpClient1, fullPath);
                        writer.WriteLine("150 Opening data connection");
                        processing.SendListFile(fullPath);
                        writer.WriteLine("226 Transfer complete");
                        tcpClient1.Close();
                        */
                    }
                    else if (command == "QUIT")
                    {
                        writer.WriteLine("221 Goodbye");
                        break;
                    }
                    else
                    {
                        writer.WriteLine("500 Syntax error, command unrecognized");
                    }
                }
            }
            catch (IOException)
            {
                //Console.WriteLine("Error: " + ex.ToString());
            }
            finally
            {
                tcpClient.Close();
                Console.WriteLine($"Đã ngắt kết nối với {iPEndPoint.Address} có cổng {iPEndPoint.Port}");
            }
        }

        private int GetPassivePort()
        {
            Random random = new Random();
            int port = random.Next(30000, 31000);
            return port;
        }

        private int GetSessionID()
        {
            return _sessionID++;
        }
    }
}
