using MyClassLibrary.Bean;
using MyClassLibrary.Common;
using MyFtpServer.DAL;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly string _rootPath = @"D:\FileServer";
        int _sessionID = 2;

        public FtpServer(string host, int port)
        {
            _host = host;
            _port = port;
            _controlSocket = new TcpListener(IPAddress.Parse(_host), _port);
            _controlSocket.Start();
            _passivePort = 30000;
        }

        public void Start()
        {
            Console.WriteLine($"FTP Server already start at {_host}:{_port}");
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
                Console.WriteLine($"Server error: {ex.Message}");
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

            ResponseStatus(sessionID, $"220 FTP Server already for {iPEndPoint.Address}:{iPEndPoint.Port}");

            int idAccount;
            do
            {
                idAccount = Authenticate(sessionID, reader, writer);
            } while (idAccount == 0);
            
            TcpListener tcpListener = new TcpListener(IPAddress.Parse(_host), _passivePort);
            int passivePort;
            TcpClient data_channel;

            // string remoteFolderPath = @"\";
            string remoteFolderPath = @"";
            try
            {
                while (true)
                {
                    if (!tcpClient.Connected)
                        break;

                    string? request = reader.ReadLine();
                    if (request == null)
                        break;
                    CommandStatus(sessionID, request);

                    string[] parts = request.Split(' ');
                    string command = parts[0];
                    if (command == "PASV")
                    {
                        passivePort = GetPortPassiveMode();
                        tcpListener = new TcpListener(IPAddress.Parse(_host), passivePort);
                        tcpListener.Start();
                        writer.WriteLine($"227 Entering Passive Mode ({_host.Replace('.', ',')},{passivePort / 256},{passivePort % 256})");
                        ResponseStatus(sessionID, $"227 Entering Passive Mode ({_host.Replace('.', ',')},{passivePort / 256},{passivePort % 256})");

                    }
                    else if (command == "CWD")
                    {
                        string folderPath = string.Join(" ", parts, 1, parts.Length - 1);
                        if (folderPath == null)
                        {
                            writer.WriteLine("501 Syntax error in parameters or arguments");
                            ResponseStatus(sessionID, $"501 Syntax error in parameters or arguments");
                            continue;
                        }
                        /*
                        if (Directory.Exists(_rootPath + folderPath) == false)
                        {
                            writer.WriteLine("550 Couldn't open the file or directory");
                            ResponseStatus(sessionID, $"550 Couldn't open the file or directory");
                            continue;
                        }*/
                        remoteFolderPath = folderPath;
                        writer.WriteLine("250 CWD command successful");
                        ResponseStatus(sessionID, $"250 CWD command successful");
                    }
                    else if (command == "PWD")
                    {
                        string response = $"257 \"{remoteFolderPath}\" is current directory.";
                        writer.WriteLine(response);
                        ResponseStatus(sessionID, response);
                    }
                    else if (command == "MKD")
                    {
                        string folderName = string.Join(" ", parts, 1, parts.Length - 1); ;
                        if (folderName == null)
                        {
                            writer.WriteLine("501 Syntax error in parameters or arguments");
                            ResponseStatus(sessionID, $"501 Syntax error in parameters or arguments");
                            continue;
                        }
                        /*
                        if (Directory.Exists(_rootPath + folderPath))
                        {
                            writer.WriteLine("550 Directory already exists");
                            ResponseStatus(sessionID, $"550 Directory already exists");
                            continue;
                        }
                        Directory.CreateDirectory(_rootPath + folderPath);*/
                        FileStorageDAL dal = new FileStorageDAL();
                        if (dal.IsDirectory(remoteFolderPath) == false)
                        {
                            Console.WriteLine(remoteFolderPath);
                            writer.WriteLine("550 Directory already exists");
                            ResponseStatus(sessionID, $"550 Directory already exists");
                            continue;
                        }
                        string id = dal.CreateNewFolder(idAccount, remoteFolderPath, folderName);
                        writer.WriteLine($"257 {id}");
                        ResponseStatus(sessionID, $"257 Directory created");
                    }
                    else if (command == "MLSD")
                    {
                        // Get list file and folder

                        string idParent = remoteFolderPath;

                        FileStorageDAL dal = new FileStorageDAL();
                        List<FileInfor> list = dal.GetFileInfors(idAccount, idParent);

                        // Check Tcp Listener
                        if (tcpListener == null)
                            continue;
                        data_channel = tcpListener.AcceptTcpClient();
                        if (data_channel == null)
                        {
                            writer.WriteLine("425 Can't open data connection.");
                            ResponseStatus(sessionID, $"425 Can't open data connection.");
                            continue;
                        }
                        FileServerProcessing processing = new FileServerProcessing(data_channel);
                        writer.WriteLine("150 Opening data connection");
                        ResponseStatus(sessionID, "150 Opening data connection");
                        processing.SendList(list);

                        writer.WriteLine("226 Transfer complete");
                        ResponseStatus(sessionID, "226 Transfer complete");
                        if (tcpListener != null)
                            tcpListener.Stop();
                    }
                    else if (command == "RETR")
                    {
                        //string filePath = string.Join(" ", parts, 1, parts.Length - 1);
                        //string fullPath = _rootPath + remoteFolderPath + @"\" + filePath;
                        Console.WriteLine(remoteFolderPath);
                        FileStorageDAL dal = new FileStorageDAL();
                        string fullPath = _rootPath + dal.GetFilePath(remoteFolderPath);

                        if (!File.Exists(fullPath))
                        {
                            writer.WriteLine("550 File not exist");
                            ResponseStatus(sessionID, "550 File not exist");
                            continue;
                        }

                        // Check Tcp Listener
                        if (tcpListener == null)
                            continue;
                        data_channel = tcpListener.AcceptTcpClient();
                        if (data_channel == null)
                        {
                            writer.WriteLine("425 Can't open data connection.");
                            ResponseStatus(sessionID, $"425 Can't open data connection.");
                            continue;
                        }
                        FileServerProcessing processing = new FileServerProcessing(data_channel);

                        writer.WriteLine("150 Opening data connection");
                        ResponseStatus(sessionID, $"150 Opening data connection");

                        //writer.WriteLine($"213 {new FileInfo(fullPath).Length}");
                        writer.WriteLine($"{new FileInfo(fullPath).Length}");

                        processing.SendFile(fullPath);

                        writer.WriteLine("226 Transfer complete");
                        ResponseStatus(sessionID, $"226 Transfer complete");
                        if (tcpListener != null)
                            tcpListener.Stop();
                    }
                    else if (command == "STOR")
                    {
                        FileStorageDAL dal = new FileStorageDAL();
                        if (dal.IsDirectory(remoteFolderPath) == false)
                        {
                            writer.WriteLine("550 Couldn't open the file or directory");
                            ResponseStatus(sessionID, $"550 Couldn't open the file or directory");
                            continue;
                        }

                        string fileName = string.Join(" ", parts, 1, parts.Length - 1);
                        string fullPath = _rootPath + dal.CreateNewFile(idAccount, remoteFolderPath, fileName);

                        if (tcpListener == null)
                            continue;
                        data_channel = tcpListener.AcceptTcpClient();
                        if (data_channel == null)
                        {
                            writer.WriteLine("425 Can't open data connection.");
                            ResponseStatus(sessionID, $"425 Can't open data connection.");
                            continue;
                        }
                        FileServerProcessing processing = new FileServerProcessing(data_channel);

                        writer.WriteLine("150 Opening data connection");
                        ResponseStatus(sessionID, $"150 Opening data connection");

                        processing.ReceiveFile(fullPath);

                        writer.WriteLine("226 Transfer complete");
                        ResponseStatus(sessionID, $"226 Transfer complete");
                        if (tcpListener != null)
                            tcpListener.Stop();
                    }
                    else if (command == "EXPRESSUPLOAD")
                    {
                        string filePath = string.Join(" ", parts, 1, parts.Length - 1);
                        string fullPath = _rootPath + remoteFolderPath + @"\" + filePath;

                        if (tcpListener == null)
                            continue;

                        writer.WriteLine("150 Opening data connection");
                        ResponseStatus(sessionID, $"150 Opening data connection");

                        long length = long.Parse(reader.ReadLine() ?? "0");

                        FileServerExpressProcessing processing = new FileServerExpressProcessing(tcpListener, fullPath , length);
                        processing.ReceiveExpressFile();

                        writer.WriteLine("226 Transfer complete");
                        ResponseStatus(sessionID, $"226 Transfer complete");
                        if (tcpListener != null)
                            tcpListener.Stop();
                    }
                    else if (command == "EXPRESSDOWNLOAD")
                    {
                        string filePath = string.Join(" ", parts, 1, parts.Length - 1);
                        string fullPath = _rootPath + remoteFolderPath + @"\" + filePath;
                        if (!File.Exists(fullPath))
                        {
                            writer.WriteLine("550 File not exist");
                            ResponseStatus(sessionID, "550 File not exist");
                            continue;
                        }

                        writer.WriteLine(new FileInfo(fullPath).Length.ToString());

                        if (tcpListener == null)
                            continue;
                        writer.WriteLine("150 Opening data connection");
                        ResponseStatus(sessionID, $"150 Opening data connection");

                        FileServerExpressProcessing processing = new FileServerExpressProcessing(tcpListener, fullPath ,new FileInfo(fullPath).Length);
                        processing.SendExpressFile();

                        command = reader.ReadLine() ?? "550";
                        if (command.StartsWith("226"))
                        {
                            writer.WriteLine("226 Transfer complete");
                            ResponseStatus(sessionID, $"226 Transfer complete");
                        }
                        else
                        {
                            writer.WriteLine("550 Transfer error");
                            ResponseStatus(sessionID, $"550 Transfer error");
                        }

                        if (tcpListener != null)
                            tcpListener.Stop();
                    }
                    else if (command == "QUIT")
                    {
                        writer.WriteLine("221 Goodbye");
                        break;
                    }
                    else
                    {
                        writer.WriteLine("500 Syntax error, command unrecognized");
                        ResponseStatus(sessionID, $"500 Syntax error, command unrecognized");
                    }
                }
            }
            catch (Exception)
            {
                //Console.WriteLine("Error: " + ex.ToString());
            }
            finally
            {
                tcpClient.Close();
                ResponseStatus(sessionID, $"??? FTP Server disconnected with {iPEndPoint.Address}:{iPEndPoint.Port}");
            }
        }

        private int Authenticate(int sessionID, StreamReader reader, StreamWriter writer)
        {
            string command = reader.ReadLine() ?? "USER";
            if (command.StartsWith("USER"))
            {
                string username = command.Substring(5);
                writer.WriteLine("331 Password required for " + username);
                ResponseStatus(sessionID, $"331 Password required for {username}");
                command = reader.ReadLine() ?? "PASS";
                if (command.StartsWith("PASS"))
                {
                    string password = command.Substring(5);
                    AccountDAL accountDAL = new AccountDAL();
                    int result = accountDAL.Authenticate(username, password);
                    if (result != 0)
                    {
                        writer.WriteLine("230 User logged in");
                        ResponseStatus(sessionID, $"230 User logged in");
                        return result;
                    }
                    else
                    {
                        writer.WriteLine("530 Not logged in");
                        ResponseStatus(sessionID, $"530 Not logged in");
                    }
                }
            } else if (command.StartsWith("REGISTER"))
            {
                string json = command.Substring(9);

                RegisterRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<RegisterRequest>(json) ?? null;

                if (request == null)
                {
                    writer.WriteLine("530 Register failed");
                    ResponseStatus(sessionID, $"Register failed");
                    return 0;
                }
                
                AccountDAL accountDAL = new AccountDAL();
                bool result = accountDAL.Register(request);
                if (result != false)
                {
                    writer.WriteLine("230 Register successful");
                    ResponseStatus(sessionID, $"230 Register successful");
                    return 0;
                }
                else
                {
                    writer.WriteLine("530 Register failed");
                    ResponseStatus(sessionID, $"Register failed");
                }
            } else if (command.StartsWith("RESETPASSWORD"))
            {
                string json = command.Substring(14);

                ResetPasswordRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<ResetPasswordRequest>(json) ?? null;

                if (request == null)
                {
                    writer.WriteLine("530 Reset password failed");
                    ResponseStatus(sessionID, $"Reset password failed");
                    return 0;
                }

                AccountDAL accountDAL = new AccountDAL();
                bool result = accountDAL.ResetPassword(request);
                if (result != false)
                {
                    writer.WriteLine("230 Reset password successful");
                    ResponseStatus(sessionID, $"230 Reset password successful");
                    return 0;
                }
                else
                {
                    writer.WriteLine("530 Reset password failed");
                    ResponseStatus(sessionID, $"Reset password failed");
                }
            }
            return 0;
        }

        private int _passivePort;
        private object lockObjectPassiveMode = new object();
        private int GetPortPassiveMode()
        {
            lock (lockObjectPassiveMode)
            {
                int port = _passivePort++;
                if (_passivePort > 30200)
                    _passivePort = 30100;
                TcpListener tcpListener = new TcpListener(IPAddress.Parse(_host), port);
                try
                {
                    tcpListener.Start();
                }
                catch (Exception)
                {
                    return GetPortPassiveMode();
                }
                return _passivePort;
            }
        }

        private object lockObject = new object();
        private int GetSessionID()
        {
            lock (lockObject) {
                return _sessionID+=2;
            }
        }

        private void CommandStatus(int sessionId, string message)
        {
            DateTime now = DateTime.Now;
            Console.WriteLine($"C> {now}\tSession {sessionId}\t {message}");
        }

        private void ResponseStatus(int sessionId, string message)
        {
            DateTime now = DateTime.Now;
            Console.WriteLine($"S> {now}\tSession {sessionId}\t {message}");
        }
    }
}
