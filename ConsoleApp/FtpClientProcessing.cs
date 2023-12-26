using MyClassLibrary.Bean.File;
using MyClassLibrary.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static MyFtpClient.DataTransferClient;

namespace MyFtpClient
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
        public bool CreateFolder(CreateFolderRequest request)
        {
            if (SetRemoteFolderPath(request.ParentFolderId) == false)
            {
                return false;
            }
            string Command = "", Response = "";
            Command = string.Format("MKD {0}", request.FolderName);
            _writer.WriteLine(Command);
            Response = _reader.ReadLine() ?? "";
            if (Response.StartsWith("257 ") == true)
            {
                return true;
            }
            return false;
        }

        public FileDetailVM? GetDetailFile(string id)
        {
            string Command = "", Response = "";
            Command = string.Format("GETDETAILFILE {0}", id);
            _writer.WriteLine(Command);
            Response = _reader.ReadLine() ?? "";
            if (Response.StartsWith("257 ") == true)
            {
                string dataJson = Response.Substring(Response.IndexOf(" ") + 1);
                if (string.IsNullOrEmpty(dataJson) == false)
                {
                    FileDetailVM fileDetailVM = JsonConvert.DeserializeObject<FileDetailVM>(dataJson);
                    return fileDetailVM;
                }
                return null;
            }
            return null;
        }

        public bool RenameFile(RenameFileRequest request)
        {
            if (SetRemoteFolderPath(request.Id) == false)
            {
                return false;
            }
            string Command = "", Response = "";
            Command = string.Format("RNTO {0}", request.NewName);
            _writer.WriteLine(Command);
            Response = _reader.ReadLine() ?? "";
            if (Response.StartsWith("250 ") == true)
            {
                return true;
            }
            return false;
        }

        public void DeleteFile(DeleteFileRequest request)
        {
            string Command, Response;
            Command = string.Format("DELE {0}", request.Id);
            _writer.WriteLine(Command);
            Response = _reader.ReadLine() ?? "";
            if (Response.StartsWith("250 ") == true)
            {
                return;
            }
        }

        public void DeleteFolder(DeleteFileRequest request)
        {
            string Command, Response;
            Command = string.Format("RMD {0}", request.Id);
            _writer.WriteLine(Command);
            Response = _reader.ReadLine() ?? "";
            if (Response.StartsWith("250 ") == true)
            {
                return;
            }
        }

        public bool RestoreFile(string fileId)
        {
            string Command, Response;
            Command = string.Format("RESTOREFILE {0}", fileId);
            _writer.WriteLine(Command);
            Response = _reader.ReadLine() ?? "";
            if (Response.StartsWith("250 ") == true)
            {
                return true;
            }
            return false;
        }

        public List<FileAccessVM> GetListFileAccess(string id)
        {
            string Command = "", Response = "";
            Command = string.Format("GETLISTFILEACCESS {0}", id);
            _writer.WriteLine(Command);
            Response = _reader.ReadLine() ?? "";
            if (Response.StartsWith("257 ") == true)
            {
                string dataJson = Response.Substring(Response.IndexOf(" ") + 1);
                if (string.IsNullOrEmpty(dataJson) == false)
                {
                    List<FileAccessVM> fileAccessVMs = JsonConvert.DeserializeObject<List<FileAccessVM>>(dataJson);
                    return fileAccessVMs;
                }
                return new List<FileAccessVM>();
            }
            return new List<FileAccessVM>();
        }

        public bool UpdateFileAccess(List<FileAccessVM> request)
        {
            string Command, Response;
            Command = string.Format("UPDATEFILEACCESS {0}", JsonConvert.SerializeObject(request));
            _writer.WriteLine(Command);
            Response = _reader.ReadLine() ?? "";
            if (Response.StartsWith("257 ") == true)
            {
                return true;
            }
            return false;
        }

        public string CreateNewRemoteFolder(string remoteFolderName)
        {
            string Command = "", Response = "";
            Command = string.Format("MKD {0}", remoteFolderName);
            _writer.WriteLine(Command);
            Response = _reader.ReadLine() ?? "";
            if (Response.StartsWith("257 ") == true)
            {
                return Response.Substring(Response.IndexOf(" ") + 1);
            }
            return "";
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

        public async Task ReceiveFileAsync(FileTransferProcessing processing, TcpClient tcpSessionClient)
        {
            StreamWriter streamWriter = new StreamWriter(tcpSessionClient.GetStream()) { AutoFlush = true };
            StreamReader streamReader = new StreamReader(tcpSessionClient.GetStream());
            string command, response;
            command = string.Format("CWD {0}", processing.RemotePath);
            await streamWriter.WriteLineAsync(command);
            response = await streamReader.ReadLineAsync() ?? "";

            if (response.StartsWith("250 "))
            {
                command = "PASV";
                await streamWriter.WriteLineAsync(command);
                response = await streamReader.ReadLineAsync() ?? "";

                if (response.StartsWith("227 ") == true)
                {
                    IPEndPoint serverDataEndpoint = GetServerEndpoint(response);
                    TcpClient dataChannel = new TcpClient();
                    await dataChannel.ConnectAsync(serverDataEndpoint.Address, serverDataEndpoint.Port);

                    command = string.Format("RETR {0}", processing.FileName);
                    await streamWriter.WriteLineAsync(command);
                    response = await streamReader.ReadLineAsync() ?? "";

                    if (response.StartsWith("150 "))
                    {
                        long fileSize = long.Parse(await streamReader.ReadLineAsync() ?? "0");
                        processing.FileSize = fileSize;

                        DataTransferClient fileClientProcessing = new DataTransferClient(dataChannel, TransferProgressHandler, processing);
                        await fileClientProcessing.ReceiveFileAsync(processing.LocalPath + @"\" + processing.FileName);

                        response = await streamReader.ReadLineAsync() ?? "";
                        if (response.StartsWith("226 "))
                        {
                            dataChannel.Close();
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

        public async Task SendFileAsync(FileTransferProcessing processing, TcpClient tcpSessionClient)
        {
            StreamWriter streamWriter = new StreamWriter(tcpSessionClient.GetStream()) { AutoFlush = true };
            StreamReader streamReader = new StreamReader(tcpSessionClient.GetStream());
            string command, response;

            command = string.Format("CWD {0}", processing.RemotePath);
            await streamWriter.WriteLineAsync(command);
            response = await streamReader.ReadLineAsync() ?? "";

            if (response.StartsWith("250 "))
            {
                command = "PASV";
                await streamWriter.WriteLineAsync(command);
                response = await streamReader.ReadLineAsync() ?? "";

                if (response.StartsWith("227 ") == true)
                {
                    IPEndPoint server_data_endpoint = GetServerEndpoint(response);
                    TcpClient data_channel = new TcpClient();
                    await data_channel.ConnectAsync(server_data_endpoint.Address, server_data_endpoint.Port);

                    command = string.Format("STOR {0}", processing.FileName);
                    await streamWriter.WriteLineAsync(command);
                    response = await streamReader.ReadLineAsync() ?? "";

                    if (response.StartsWith("150 "))
                    {
                        DataTransferClient fileClientProcessing = new DataTransferClient(data_channel, TransferProgressHandler, processing);
                        await fileClientProcessing.SendFileAsync(processing.LocalPath + @"\" + processing.FileName);

                        response = await streamReader.ReadLineAsync() ?? "";
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

        public FileInforPackage GetFileInforPackage(GetListFileRequest request)
        {
            FileInforPackage fileInforPackage = new FileInforPackage();
            string command, response;

            command = string.Format("CWD {0}", request.IdParent);
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
                string json = JsonConvert.SerializeObject(request);
                command = "MLSD " + json;
                _writer.WriteLine(command);
                TcpClient data_channel = new TcpClient();
                data_channel.Connect(server_data_endpoint);
                response = _reader.ReadLine() ?? "";
                //Console.WriteLine(Response); // Console command line
                if (response.StartsWith("150 "))
                {
                    DataTransferClient fileClientProcessing = new DataTransferClient(data_channel);
                    fileInforPackage = fileClientProcessing.ReceiveFileInforPackage();

                    response = _reader.ReadLine() ?? "";
                    if (response.StartsWith("226 "))
                    {
                        //Console.WriteLine(Response); // Console command line
                        data_channel.Close();
                    }
                }
            }
            return fileInforPackage;
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
            if (Directory.Exists(localFolderPath) == false)
            {
                Directory.CreateDirectory(localFolderPath);
            }
            bool currentRemoteFolderPath = SetRemoteFolderPath(remoteFolderPath);
            if (currentRemoteFolderPath == true)
            {
                GetListFileRequest request = new GetListFileRequest()
                {
                    IdParent = remoteFolderPath,
                    IdAccess = IdAccess.Shared,
                };
                FileInforPackage fileInfors = GetFileInforPackage(request);
                foreach (FileInfor fileInfor in fileInfors.fileInfors)
                {
                    if (fileInfor.IsDirectory == true)
                    {
                        string newRemoteFolderPath = fileInfor.Id;
                        string newLocalFolderPath = localFolderPath + @"\" + fileInfor.Name;
                        DownloadFolder(newRemoteFolderPath, newLocalFolderPath);
                    }
                    else
                    {
                        FileTransferProcessing processing = new FileTransferProcessing("RETR", fileInfor.Id, fileInfor.Name, localFolderPath);
                        transferRequest(processing);
                    }
                }
            }
        }

        public bool CheckCanUpload(string remotePath)
        {
            string command, response;
            command = string.Format("CWD {0}", remotePath);
            _writer.WriteLine(command);
            response = _reader.ReadLine() ?? "";
            if (response.StartsWith("250 ") == true)
            {
                command = "CHECKCANUPLOAD";
                _writer.WriteLine(command);
                response = _reader.ReadLine() ?? "";
                if (response.StartsWith("200 ") == true)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> CheckCanUploadAsync(string remotePath)
        {
            string command, response;
            command = string.Format("CWD {0}", remotePath);
            await _writer.WriteLineAsync(command);
            response = await _reader.ReadLineAsync() ?? "";
            if (response.StartsWith("250 ") == true)
            {
                command = "CHECKCANUPLOAD";
                await _writer.WriteLineAsync(command);
                response = await _reader.ReadLineAsync() ?? "";
                if (response.StartsWith("200 ") == true)
                {
                    return true;
                }
            }
            return false;
        }

        public void UploadFolder(string remoteFolderPath, string localFolderPath)
        {
            string remoteFolderId = CreateNewRemoteFolder(remoteFolderPath);
            bool currentRemoteFolderPath = SetRemoteFolderPath(remoteFolderId);
            if (currentRemoteFolderPath == true)
            {
                string[] localPaths = Directory.GetFiles(localFolderPath);
                foreach (string localPath in localPaths)
                {
                    FileTransferProcessing taskSession = new FileTransferProcessing("STOR", remoteFolderId, Path.GetFileName(localPath) ?? "\\undefine", Path.GetDirectoryName(localPath) ?? "\\undefine");
                    transferRequest(taskSession);
                }
                string[] localFolders = Directory.GetDirectories(localFolderPath);
                foreach (string localFolder in localFolders)
                {
                    string newRemoteFolderName = Path.GetFileName(localFolder);
                    string newLocalFolderPath = localFolderPath + @"\" + Path.GetFileName(localFolder);
                    UploadFolder(newRemoteFolderName, newLocalFolderPath);
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
