using MyClassLibrary;
using MyClassLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class FtpClient
    {
        private string _host;
        private int _port;
        private TcpClient _client;
        //       private TcpClient[] _subClient = new TcpClient[2];
        private FtpClientSession ftpClientSession;
        private StreamWriter _writer;
        private StreamReader _reader;
        private string Command;
        private string Response;
        //private string remoteFolderPath = @"D:\FileServer\";
        public FtpClient(string host, int port)
        {
            _host = host;
            _port = port;
            _client = new TcpClient();
            
            _client.Connect(_host, _port);
            _writer = new StreamWriter(_client.GetStream()) { AutoFlush = true };
            _reader = new StreamReader(_client.GetStream());

            Command = "";
            Response = "";

            ftpClientSession = new FtpClientSession(this);
            Thread thread = new Thread(() => ftpClientSession.Process());
            thread.Start();
        }

        public void ExecuteClientCommand(string clientCommand)
        {
            string command = clientCommand.Split(' ')[0].ToUpper();
            string[] args = clientCommand.Split(' ').Skip(1).ToArray();
            switch (command)
            {
                case "LIST":
                    Thread thread = new Thread(() => ListRemoteFolderAndFiles());
                    thread.Start();
                    break;
                case "CWD":
                    Thread threadcwd = new Thread(() => SetRemoteFolderPath(args[0]));
                    threadcwd.Start();
                    break;
                case "PWD":
                    Thread threadpwd = new Thread(() => GetRemoteFolderPath());
                    threadpwd.Start();
                    break;
                case "STOR":
                    ftpClientSession.PushQueueCommand(clientCommand);
                    break;
                case "RETR":
                    ftpClientSession.PushQueueCommand(clientCommand);
                    break;
                case "quit":
                    //Quit();
                    break;
                default:
                    break;
            }
        }

        public void ExecuteSessionCommand(string sessionCommand, TcpClient tcpSessionClient)
        {
            string command = sessionCommand.Split(" ")[0].ToUpper();
            string[] args = sessionCommand.Split(" ").Skip(1).ToArray();
            switch (command)
            {
                case "STOR":
                    Thread thread = new Thread(() => SendFile(args[0], args[1], args[2], tcpSessionClient));
                    thread.Start();
                    break;
                case "RETR":
                    Thread thread1 = new Thread(() => ReceiveFile(args[0], args[1], args[2], tcpSessionClient));
                    thread1.Start();
                    break;
                default:
                    break;
            }
        }

        public string GetRemoteFolderPath()
        {
            Command = "PWD";
            _writer.WriteLine(Command);
            Response = _reader.ReadLine() ?? "";
            if (Response.StartsWith("257 ") == true)
            {
                string[] tokens = Response.Split('"');
                return tokens[1];
            }
            return "";
        }

        public void SetRemoteFolderPath(string remoteFolderPath)
        {
            Command = string.Format("CWD {0}", remoteFolderPath.Replace("\\", "/"));
            _writer.WriteLine(Command);
            Response = _reader.ReadLine() ?? "";
            Console.WriteLine(Response); // Console command line
        }

        public List<FileInfor> ListRemoteFolderAndFiles()
        {
            List<FileInfor> list = new List<FileInfor>();
            Command = "PASV";
            _writer.WriteLine(Command);
            Response = _reader.ReadLine() ?? "";
            //Console.WriteLine(Response); // Console command line
            if (Response.StartsWith("227 ") == true)
            {
                IPEndPoint server_data_endpoint = GetServerEndpoint(Response);
                Command = "LIST";
                _writer.WriteLine(Command);
                TcpClient data_channel = new TcpClient();
                data_channel.Connect(server_data_endpoint);
                Response = _reader.ReadLine() ?? "";
                //Console.WriteLine(Response); // Console command line
                if (Response.StartsWith("150 "))
                {
                    FileClientProcessing fileClientProcessing = new FileClientProcessing(data_channel);
                    list = fileClientProcessing.ReceiveListRemoteFiles();

                    Response = _reader.ReadLine() ?? "";
                    if (Response.StartsWith("226 "))
                    {
                        //Console.WriteLine(Response); // Console command line
                        data_channel.Close();
                    }
                }
            }
            return list;
        }

        public void ReceiveFile(string remoteFolderPath, string remoteFileName, string localFolderPath, TcpClient tcpSessionClient)
        {
            StreamWriter streamWriter = new StreamWriter(tcpSessionClient.GetStream()) { AutoFlush = true };
            StreamReader streamReader = new StreamReader(tcpSessionClient.GetStream());

            Command = "PASV";
            streamWriter.WriteLine(Command);
            Response = streamReader.ReadLine() ?? "";
            //Console.WriteLine(Response); // Console command line
            if (Response.StartsWith("227 ") == true)
            {
                IPEndPoint server_data_endpoint = GetServerEndpoint(Response);
                Command = string.Format("CWD {0}", remoteFolderPath.Replace("\\", "/"));
                streamWriter.WriteLine(Command);
                Response = streamReader.ReadLine() ?? "";
                //Console.WriteLine(Response); // Console command line
                if (Response.StartsWith("250 "))
                {
                    Command = string.Format("RETR {0}", remoteFileName);
                    streamWriter.WriteLine(Command);

                    TcpClient data_channel = new TcpClient();
                    data_channel.Connect(server_data_endpoint);

                    Response = streamReader.ReadLine() ?? "";
                    //Console.WriteLine(Response); // Console command line
                    if (Response.StartsWith("150 "))
                    {
                        FileClientProcessing fileClientProcessing = new FileClientProcessing(data_channel);
                        fileClientProcessing.ReceiveFile(localFolderPath + @"\" + remoteFileName);

                        Response = streamReader.ReadLine() ?? "";
                        if (Response.StartsWith("226 "))
                        {
                            //Console.WriteLine(Response); // Console command line
                            data_channel.Close();
                        }
                    }
                }
            }
        }

        public void SendFile(string remoteFolderPath, string remoteFileName, string localFolderPath, TcpClient tcpSessionClient)
        {
            StreamWriter streamWriter = new StreamWriter(tcpSessionClient.GetStream()) { AutoFlush = true };
            StreamReader streamReader = new StreamReader(tcpSessionClient.GetStream());

            Command = "PASV";
            streamWriter.WriteLine(Command);
            Response = streamReader.ReadLine() ?? "";
            //Console.WriteLine(Response); // Console command line
            if (Response.StartsWith("227 ") == true)
            {
                IPEndPoint server_data_endpoint = GetServerEndpoint(Response);
                Command = string.Format("CWD {0}", remoteFolderPath.Replace("\\", "/"));
                streamWriter.WriteLine(Command);
                Response = streamReader.ReadLine() ?? "";
                if (Response.StartsWith("250 "))
                {
                    //Console.WriteLine(Response); // Console command line
                    Command = string.Format("STOR {0}", remoteFileName);
                    streamWriter.WriteLine(Command);
                    TcpClient data_channel = new TcpClient();
                    data_channel.Connect(server_data_endpoint);
                    Response = streamReader.ReadLine() ?? "";
                    //Console.WriteLine(Response); // Console command line
                    if (Response.StartsWith("150 "))
                    {
                        FileClientProcessing fileClientProcessing = new FileClientProcessing(data_channel);
                        fileClientProcessing.SendFile(localFolderPath + @"\" + remoteFileName);

                        Response = streamReader.ReadLine() ?? "";
                        if (Response.StartsWith("226 "))
                        {
                            //Console.WriteLine(Response); // Console command line
                            data_channel.Close();
                        }
                    }
                }
            }
        }
        /*
        public void ReceiveFolder(string remoteFolderPath, string localFolderPath)
        {
            if (IsDirectory(remoteFolderPath))
            {
                // 1. Tạo thư mục server nếu nó không tồn tại.
                if (!Directory.Exists(localFolderPath))
                {
                    Directory.CreateDirectory(localFolderPath);
                }

                //List<string> fileList = ListFilesAndFolders(remoteFolderPath);
                List<FileInfor> fileList = ListRemoteFolderAndFiles();

                for (int i = 0; i < fileList.Count(); i++)
                {
                    string remoteItemPath = remoteFolderPath + "\\" + fileList[i].Name;
                    string localItemPath = localFolderPath + "\\" + fileList[i].Name;
                    ReceiveFolder(remoteItemPath, localItemPath);
                }
            }
            else
            {
                ReceiveFile(remoteFolderPath, localFolderPath);
            }
        }
        */
        private IPEndPoint GetServerEndpoint(string response)
        {
            int start = response.IndexOf('(');
            int end = response.IndexOf(')');
            string substr = response.Substring(start + 1, end - start - 1);
            string[] octets = substr.Split(',');
            int port = int.Parse(octets[4]) * 256 + int.Parse(octets[5]);
            IPAddress address = new IPAddress(new byte[] { byte.Parse(octets[0]), byte.Parse(octets[1]), byte.Parse(octets[2]), byte.Parse(octets[3]) });
            return new IPEndPoint(address, port);
        }

        private bool IsDirectory(string remoteFolderPath)
        {   Command = string.Format("CWD {0}", remoteFolderPath.Replace("\\", "/"));
            _writer.WriteLine(Command);
            Response = _reader.ReadLine() ?? "";
            if (Response.StartsWith("250 "))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
