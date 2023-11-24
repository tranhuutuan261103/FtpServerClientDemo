using MyClassLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp.DataTransferClient;

namespace ConsoleApp
{
    public class FtpClientProcessing
    {
        private readonly TcpClient _tcpClient;
        private readonly StreamWriter _writer;
        private readonly StreamReader _reader;

        public FtpClientProcessing(TcpClient tcpClient, TransferProgress transferProgress, OnTransferRequest transferRequest)
        {
            _tcpClient = tcpClient;
            _writer = new StreamWriter(_tcpClient.GetStream()) { AutoFlush = true };
            _reader = new StreamReader(_tcpClient.GetStream());
            progress += transferProgress;
            this.transferRequest += transferRequest;
        }

        public delegate void TransferProgress(FileTransferProcessing sender);
        public event TransferProgress progress;
        private void TransferProgressHandler(FileTransferProcessing sender)
        {
            progress?.Invoke(sender);
        }

        public delegate void OnTransferRequest(FileTransferProcessing request);
        public event OnTransferRequest transferRequest;

        public string GetRemoteFolderPath()
        {
            string command, response;

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

                        DataTransferClient fileClientProcessing = new DataTransferClient(data_channel, TransferProgressHandler, processing);
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
                        DataExpressTransferClient fileClientExpressProcessing = new DataExpressTransferClient(server_data_endpoint, request.LocalPath + "\\" + request.FileName, fileSize, TransferProgressHandler, request);
                        fileClientExpressProcessing.ExpressReceiveFile();

                        command = "226 Transfer complete";
                        streamWriter.WriteLine(command);
                        request.Status = FileTransferProcessingStatus.Completed;
                        progress(request);
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

                        DataExpressTransferClient fileClientExpressProcessing = new DataExpressTransferClient(server_data_endpoint, request.LocalPath + @"\" + request.FileName, fileSize, TransferProgressHandler, request);
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

        public void SendFile(FileTransferProcessing processing, TcpClient tcpSessionClient)
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

                    command = string.Format("STOR {0}", processing.FileName);
                    streamWriter.WriteLine(command);
                    response = streamReader.ReadLine() ?? "";
                    if (response.StartsWith("150 "))
                    {
                        DataTransferClient fileClientProcessing = new DataTransferClient(data_channel, TransferProgressHandler, processing);
                        fileClientProcessing.SendFile(processing.LocalPath + @"\" + processing.FileName);

                        response = streamReader.ReadLine() ?? "";
                        if (response.StartsWith("226 "))
                        {
                            processing.Status = FileTransferProcessingStatus.Completed;
                            TransferProgressHandler(processing);
                            data_channel.Close();
                        }
                    }
                }
            }
        }

        public List<FileInfor> ListRemoteFoldersAndFiles(string remoteFolderPath)
        {
            List<FileInfor> list = new List<FileInfor>();
            string command, response;

            command = string.Format("CWD {0}", remoteFolderPath);
            _writer.WriteLine(command);
            response = _reader.ReadLine() ?? "";

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
                    DataTransferClient fileClientProcessing = new DataTransferClient(data_channel);
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

        public void DownloadFolder(string remoteFolderPath, string localFolderPath)
        {
            bool currentRemoteFolderPath = SetRemoteFolderPath(remoteFolderPath);
            if (currentRemoteFolderPath == true)
            {
                List<FileInfor> fileInfors = ListRemoteFoldersAndFiles(remoteFolderPath);
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
                        FileTransferProcessing processing = new FileTransferProcessing("RETR", remoteFolderPath, fileInfor.Name, localFolderPath);
                        transferRequest(processing);
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
                    transferRequest(taskSession);
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
