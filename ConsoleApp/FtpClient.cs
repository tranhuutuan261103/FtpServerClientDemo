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
        private FtpClientSession ftpClientSession;
        private StreamWriter _writer;
        private StreamReader _reader;
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

        public delegate void FtpClientEventHandler(FileTransferProcessing sender);
        public FtpClientEventHandler FtpClientEvent { set; get; }

        private void FileClientProcessingEventHandler(FileTransferProcessing sender)
        {
            FtpClientEvent?.Invoke(sender);
        }

        public void Mlsd()
        {
            var list = ListRemoteFolderAndFiles();
            foreach (var file in list)
            {
                Console.WriteLine(file.ToString());
            }
        }

        public string Pwd()
        {
            return GetRemoteFolderPath();
        }

        public bool Cwd(string request)
        {
            request = request.Trim();
            string[] parts = request.Split(' ');
            if (parts.Length < 2)
            {
                return false;
            }

            string folderName = request.Substring(parts[0].Length + 1).Trim();
            return SetRemoteFolderPath(folderName);
        }

        public bool Mkd(string request)
        {
            request = request.Trim();
            string[] parts = request.Split(' ');
            if (parts.Length < 2)
            {
                return false;
            }
            string folderName = request.Substring(parts[0].Length + 1).Trim();
            return CreateNewRemoteFolder(folderName);
        }

        public void ExpressDownload(string remotePath, string localPath)
        {
            FileTransferProcessing taskSession = new FileTransferProcessing("EXPRESSDOWNLOAD", Path.GetDirectoryName(remotePath) ?? "\\undefine", Path.GetFileName(remotePath) ?? "", localPath);
            ftpClientSession.PushQueueCommand(taskSession);
        }

        public void Download(string remotePath, string localPath)
        {
            FileTransferProcessing taskSession = new FileTransferProcessing("RETR", Path.GetDirectoryName(remotePath) ?? "\\undefine", Path.GetFileName(remotePath) ?? "", localPath)
            {
                Status = FileTransferProcessingStatus.Waiting
            };
            ftpClientSession.PushQueueCommand(taskSession);
        }

        public void ExpressUpload(string remotePath, string localPath)
        {
            FileTransferProcessing taskSession = new FileTransferProcessing("EXPRESSUPLOAD", remotePath , Path.GetFileName(localPath) ?? "\\undefine", Path.GetDirectoryName(localPath) ?? "\\undefine");
            ftpClientSession.PushQueueCommand(taskSession);
        }

        public void Upload(string remotePath, string localPath)
        {
            FileTransferProcessing taskSession = new FileTransferProcessing("STOR", remotePath, Path.GetFileName(localPath) ?? "\\undefine", Path.GetDirectoryName(localPath) ?? "\\undefine");
            ftpClientSession.PushQueueCommand(taskSession);
        }

        public void ExecuteSessionCommand(FileTransferProcessing request, TcpClient tcpSessionClient)
        {
            string command = request.Type;
            switch (command)
            {
                case "STOR":
                    SendFile(request, tcpSessionClient);
                    break;
                case "EXPRESSUPLOAD":
                    ExpressSendFile(request, tcpSessionClient);
                    break;
                case "RETR":
                    ReceiveFile(request, tcpSessionClient);
                break;
                case "EXPRESSDOWNLOAD":
                    ExpressReceiveFile(request, tcpSessionClient);
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
            string command, response;

            command = string.Format("CWD {0}", remoteFolderPath);
            _writer.WriteLine(command);
            response = _reader.ReadLine() ?? "";
            if (response.StartsWith("250 ") == true)
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
                command = "MLSD";
                _writer.WriteLine(command);
                TcpClient data_channel = new TcpClient();
                data_channel.Connect(server_data_endpoint);
                response = _reader.ReadLine() ?? "";
                //Console.WriteLine(Response); // Console command line
                if (response.StartsWith("150 "))
                {
                    FileClientProcessing fileClientProcessing = new FileClientProcessing(data_channel, FileClientProcessingEventHandler);
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

        public void ReceiveFile(FileTransferProcessing processing, TcpClient tcpSessionClient)
        {
            StreamWriter streamWriter = new StreamWriter(tcpSessionClient.GetStream()) { AutoFlush = true };
            StreamReader streamReader = new StreamReader(tcpSessionClient.GetStream());
            string command, response;
            command = string.Format("CWD {0}", processing.RemotePath);
            streamWriter.WriteLine(command);
            response = streamReader.ReadLine() ?? "";
            if (response.StartsWith("250 "))
            {
                command = "PASV";
                streamWriter.WriteLine(command);
                response = streamReader.ReadLine() ?? "";
                if (response.StartsWith("227 ") == true)
                {
                    IPEndPoint server_data_endpoint = GetServerEndpoint(response);
                    TcpClient data_channel = new TcpClient();
                    data_channel.Connect(server_data_endpoint);

                    command = string.Format("RETR {0}", processing.FileName);
                    streamWriter.WriteLine(command);
                    response = streamReader.ReadLine() ?? "";
                    if (response.StartsWith("150 "))
                    {
                        long fileSize = long.Parse(streamReader.ReadLine() ?? "0");
                        processing.FileSize = fileSize;

                        FileClientProcessing fileClientProcessing = new FileClientProcessing(data_channel, FileClientProcessingEventHandler, processing);
                        fileClientProcessing.ReceiveFile(processing.LocalPath + @"\" + processing.FileName);

                        response = streamReader.ReadLine() ?? "";
                        if (response.StartsWith("226 "))
                        {
                            data_channel.Close();
                        }
                    }
                }
            }
        }

        public void ExpressReceiveFile(FileTransferProcessing request, TcpClient tcpSessionClient)
        {
            StreamWriter streamWriter = new StreamWriter(tcpSessionClient.GetStream()) { AutoFlush = true };
            StreamReader streamReader = new StreamReader(tcpSessionClient.GetStream());
            string command, response;
            command = string.Format("CWD {0}", request.RemotePath);
            streamWriter.WriteLine(command);
            response = streamReader.ReadLine() ?? "";
            if (response.StartsWith("250 "))
            {
                command = "PASV";
                streamWriter.WriteLine(command);
                response = streamReader.ReadLine() ?? "";
                if (response.StartsWith("227 ") == true)
                {
                    IPEndPoint server_data_endpoint = GetServerEndpoint(response);

                    command = string.Format("EXPRESSDOWNLOAD {0}", request.FileName);
                    streamWriter.WriteLine(command);

                    long fileSize = long.Parse(streamReader.ReadLine() ?? "0");

                    response = streamReader.ReadLine() ?? "";
                    if (response.StartsWith("150 "))
                    {
                        FileClientExpressProcessing fileClientExpressProcessing = new FileClientExpressProcessing(server_data_endpoint, request.LocalPath + "\\" + request.FileName, fileSize);
                        fileClientExpressProcessing.ExpressReceiveFile();

                        command = "226 Transfer complete";
                        streamWriter.WriteLine(command);
                    }
                }
            }
        }

        public void ExpressSendFile(FileTransferProcessing request, TcpClient tcpSessionClient)
        {
            StreamWriter streamWriter = new StreamWriter(tcpSessionClient.GetStream()) { AutoFlush = true };
            StreamReader streamReader = new StreamReader(tcpSessionClient.GetStream());
            string command, response;
            command = string.Format("CWD {0}", request.RemotePath);
            streamWriter.WriteLine(command);
            response = streamReader.ReadLine() ?? "";
            if (response.StartsWith("250 "))
            {
                command = "PASV";
                streamWriter.WriteLine(command);
                response = streamReader.ReadLine() ?? "";
                if (response.StartsWith("227 ") == true)
                {
                    IPEndPoint server_data_endpoint = GetServerEndpoint(response);
                    
                    command = string.Format("EXPRESSUPLOAD {0}", request.FileName);
                    streamWriter.WriteLine(command);
                    response = streamReader.ReadLine() ?? "";
                    if (response.StartsWith("150 "))
                    {
                        long fileSize = new FileInfo(request.LocalPath + @"\" + request.FileName).Length;
                        streamWriter.WriteLine(fileSize);

                        FileClientExpressProcessing fileClientExpressProcessing = new FileClientExpressProcessing(server_data_endpoint, request.LocalPath + @"\" + request.FileName, fileSize);
                        fileClientExpressProcessing.ExpressSendFile();

                        response = streamReader.ReadLine() ?? "";
                        if (response.StartsWith("226 "))
                        {
                            //Console.WriteLine("Upload file success!");
                            //data_channel.Close();
                        }
                    }
                }
            }
        }

        public void SendFile(FileTransferProcessing processing ,TcpClient tcpSessionClient)
        {
            StreamWriter streamWriter = new StreamWriter(tcpSessionClient.GetStream()) { AutoFlush = true };
            StreamReader streamReader = new StreamReader(tcpSessionClient.GetStream());
            string command , response;

            command = string.Format("CWD {0}", processing.RemotePath);
            streamWriter.WriteLine(command);
            response = streamReader.ReadLine() ?? "";
            if (response.StartsWith("250 "))
            {
                command = "PASV";
                streamWriter.WriteLine(command);
                response = streamReader.ReadLine() ?? "";
                if (response.StartsWith("227 ") == true)
                {
                    IPEndPoint server_data_endpoint = GetServerEndpoint(response);
                    TcpClient data_channel = new TcpClient();
                    data_channel.Connect(server_data_endpoint);
                
                    command = string.Format("STOR {0}", processing.FileName);
                    streamWriter.WriteLine(command);
                    response = streamReader.ReadLine() ?? "";
                    if (response.StartsWith("150 "))
                    {
                        FileClientProcessing fileClientProcessing = new FileClientProcessing(data_channel, FileClientProcessingEventHandler, processing);
                        fileClientProcessing.SendFile(processing.LocalPath + @"\" + processing.FileName);

                        response = streamReader.ReadLine() ?? "";
                        if (response.StartsWith("226 "))
                        {
                            data_channel.Close();
                        }
                    }
                }
            }
        }
        
        public void DownloadFolder(string remoteFolderPath, string localFolderPath)
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
                        DownloadFolder(newRemoteFolderPath, newLocalFolderPath);
                    }
                    else
                    {
                        FileTransferProcessing processing = new FileTransferProcessing("RETR", Path.GetDirectoryName(remoteFolderPath) ?? "\\undefine", fileInfor.Name, localFolderPath)
                        {
                            Status = FileTransferProcessingStatus.Waiting
                        };
                        ftpClientSession.PushQueueCommand(processing);
                    }
                }
            }
        }

        public void UploadFolder(string remoteFolderPath, string localFolderPath)
        {
            CreateNewRemoteFolder(remoteFolderPath);
            bool currentRemoteFolderPath = SetRemoteFolderPath(remoteFolderPath);
            if (currentRemoteFolderPath == true)
            {
                string[] localPaths = Directory.GetFiles(localFolderPath);
                foreach (string localPath in localPaths)
                {
                    FileTransferProcessing taskSession = new FileTransferProcessing("STOR", remoteFolderPath, Path.GetFileName(localPath) ?? "\\undefine", Path.GetDirectoryName(localPath) ?? "\\undefine");
                    ftpClientSession.PushQueueCommand(taskSession);
                }
                string[] localFolders = Directory.GetDirectories(localFolderPath);
                foreach (string localFolder in localFolders)
                {
                    string newRemoteFolderPath = remoteFolderPath + @"\" + Path.GetFileName(localFolder);
                    string newLocalFolderPath = localFolderPath + @"\" + Path.GetFileName(localFolder);
                    UploadFolder(newRemoteFolderPath, newLocalFolderPath);
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
