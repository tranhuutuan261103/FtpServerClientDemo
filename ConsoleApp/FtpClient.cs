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
        //private string remoteFolderPath = @"D:\FileServer\";
        public FtpClient(string host, int port)
        {
            _host = host;
            _port = port;
            _client = new TcpClient();
            
            _client.Connect(_host, _port);
            _writer = new StreamWriter(_client.GetStream()) { AutoFlush = true };
            _reader = new StreamReader(_client.GetStream());

            ftpClientSession = new FtpClientSession(this);
            Thread thread = new Thread(() => ftpClientSession.Process());
            thread.Start();
        }

        public string GetHost()
        {
            return _host;
        }

        public int GetPort()
        {
            return _port;
        }

        public object? ExecuteClientCommand(string clientCommand)
        {
            string command = clientCommand.Split(' ')[0].ToUpper();
            string[] args = clientCommand.Split(' ').Skip(1).ToArray();
            switch (command)
            {
                case "LIST":
                    Thread thread = new Thread(() => ListRemoteFolderAndFiles());
                    thread.Start();
                    break;
                case "MLSD":
                    return ListRemoteFolderAndFiles();
                case "CWD":
                    SetRemoteFolderPath(args[0]);
                    break;
                case "PWD":
                    return GetRemoteFolderPath();
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
            return null;
        }

        public void ExecuteSessionCommand(string sessionCommand, TcpClient tcpSessionClient)
        {
            string command = sessionCommand.Split(" ")[0].ToUpper();
            string[] args = sessionCommand.Split(" ").Skip(1).ToArray();
            switch (command)
            {
                case "STOR":
                    SendFile(args[0], args[1], args[2], tcpSessionClient);
                    break;
                case "RETR":
                    ReceiveFile(args[0], args[1], args[2], tcpSessionClient);
                    break;
                default:
                    break;
            }
        }

        public string GetRemoteFolderPath()
        {
            string command, response ;

            command = "PWD";
            _writer.WriteLine(command);
            response = _reader.ReadLine() ?? "";
            if (response.StartsWith("257 ") == true)
            {
                string[] tokens = response.Split('"');
                return tokens[1];
            }
            return "";
        }

        public bool CreateNewRemoteFolder(string remoteFolderName)
        {
            string Command = "", Response = "";
            Command = string.Format("MKD {0}", remoteFolderName);
            _writer.WriteLine(Command);
            Response = _reader.ReadLine() ?? "";
            if (Response.StartsWith("257 ") == true)
            {
                return true;
            }
            return false;
        }
        public bool SetRemoteFolderPath(string remoteFolderPath)
        {
            string Command = "", Response = "";

            Command = string.Format("CWD {0}", remoteFolderPath.Replace("\\", "/"));
            _writer.WriteLine(Command);
            Response = _reader.ReadLine() ?? "";
            if (Response.StartsWith("250 ") == true)
            {
                return true;
            }
            return false;
        }

        public List<FileInfor> ListRemoteFolderAndFiles()
        {
            List<FileInfor> list = new List<FileInfor>();
            string command, response;

            command = "PWD";
            _writer.WriteLine(command);
            response = _reader.ReadLine() ?? "";
            //Console.WriteLine(Response); // Console command line
            if (response.StartsWith("257 ") == true)
            {
                string[] tokens = response.Split('"');
                //Console.WriteLine(tokens[1]); // Console command line
            }

            command = "PASV";
            _writer.WriteLine(command);
            response = _reader.ReadLine() ?? "";
            //Console.WriteLine(Response); // Console command line
            if (response.StartsWith("227 ") == true)
            {
                IPEndPoint server_data_endpoint = GetServerEndpoint(response);
                command = "LIST";
                _writer.WriteLine(command);
                TcpClient data_channel = new TcpClient();
                data_channel.Connect(server_data_endpoint);
                response = _reader.ReadLine() ?? "";
                //Console.WriteLine(Response); // Console command line
                if (response.StartsWith("150 "))
                {
                    FileClientProcessing fileClientProcessing = new FileClientProcessing(data_channel);
                    list = fileClientProcessing.ReceiveListRemoteFiles();

                    response = _reader.ReadLine() ?? "";
                    if (response.StartsWith("226 "))
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
            string command, response;

            command = string.Format("CWD {0}", remoteFolderPath.Replace("\\", "/"));
            streamWriter.WriteLine(command);
            response = streamReader.ReadLine() ?? "";
            //Console.WriteLine(Response); // Console command line
            if (response.StartsWith("250 "))
            {
                command = "PASV";
                streamWriter.WriteLine(command);
                response = streamReader.ReadLine() ?? "";
                //Console.WriteLine(Response); // Console command line
                if (response.StartsWith("227 ") == true)
                {
                    IPEndPoint server_data_endpoint = GetServerEndpoint(response);
                    TcpClient data_channel = new TcpClient();
                    data_channel.Connect(server_data_endpoint);

                    command = string.Format("RETR {0}", remoteFileName);
                    streamWriter.WriteLine(command);
                    response = streamReader.ReadLine() ?? "";
                    //Console.WriteLine(Response); // Console command line
                    if (response.StartsWith("150 "))
                    {
                        FileClientProcessing fileClientProcessing = new FileClientProcessing(data_channel);
                        fileClientProcessing.ReceiveFile(localFolderPath + @"\" + remoteFileName);

                        response = streamReader.ReadLine() ?? "";
                        if (response.StartsWith("226 "))
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
            string command , response;

            command = string.Format("CWD {0}", remoteFolderPath.Replace("\\", "/"));
            streamWriter.WriteLine(command);
            response = streamReader.ReadLine() ?? "";
            if (response.StartsWith("250 "))
            {
                //Console.WriteLine(Response); // Console command line
                command = "PASV";
                streamWriter.WriteLine(command);
                response = streamReader.ReadLine() ?? "";
                //Console.WriteLine(Response); // Console command line
                if (response.StartsWith("227 ") == true)
                {
                    IPEndPoint server_data_endpoint = GetServerEndpoint(response);
                    TcpClient data_channel = new TcpClient();
                    data_channel.Connect(server_data_endpoint);
                
                    command = string.Format("STOR {0}", remoteFileName);
                    streamWriter.WriteLine(command);
                    response = streamReader.ReadLine() ?? "";
                    //Console.WriteLine(Response); // Console command line
                    if (response.StartsWith("150 "))
                    {
                        FileClientProcessing fileClientProcessing = new FileClientProcessing(data_channel);
                        fileClientProcessing.SendFile(localFolderPath + @"\" + remoteFileName);

                        response = streamReader.ReadLine() ?? "";
                        if (response.StartsWith("226 "))
                        {
                            //Console.WriteLine(Response); // Console command line
                            data_channel.Close();
                        }
                    }
                }
            }
        }
        
        public void ReceiveFolder(string remoteFolderPath, string localFolderPath)
        {
            bool currentRemoteFolderPath = SetRemoteFolderPath(remoteFolderPath);
            if (currentRemoteFolderPath == true)
            {
                List<FileInfor> fileInfors = ListRemoteFolderAndFiles();
                foreach (FileInfor fileInfor in fileInfors)
                {
                    if (fileInfor.IsDirectory == true)
                    {
                        string newRemoteFolderPath = remoteFolderPath + @"\" + fileInfor.Name;
                        string newLocalFolderPath = localFolderPath + @"\" + fileInfor.Name;
                        if (Directory.Exists(newLocalFolderPath) == false)
                        {
                            Directory.CreateDirectory(newLocalFolderPath);
                        }
                        ReceiveFolder(newRemoteFolderPath, newLocalFolderPath);
                    }
                    else
                    {
                        string command = $"RETR {remoteFolderPath} {fileInfor.Name} {localFolderPath}";
                        ftpClientSession.PushQueueCommand(command);
                    }
                }
            }
        }

        public void SendFolder(string remoteFolderPath, string localFolderPath)
        {
            CreateNewRemoteFolder(remoteFolderPath);
            bool currentRemoteFolderPath = SetRemoteFolderPath(remoteFolderPath);
            if (currentRemoteFolderPath == true)
            {
                string[] localFiles = Directory.GetFiles(localFolderPath);
                foreach (string localFile in localFiles)
                {
                    string command = $"STOR {remoteFolderPath} {Path.GetFileName(localFile)} {localFolderPath}";
                    ftpClientSession.PushQueueCommand(command);
                }
                string[] localFolders = Directory.GetDirectories(localFolderPath);
                foreach (string localFolder in localFolders)
                {
                    string newRemoteFolderPath = remoteFolderPath + @"\" + Path.GetFileName(localFolder);
                    string newLocalFolderPath = localFolderPath + @"\" + Path.GetFileName(localFolder);
                    SendFolder(newRemoteFolderPath, newLocalFolderPath);
                }
            }
        }   
        
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
    }
}
