using MyClassLibrary.Bean.Account;
using MyClassLibrary.Bean.File;
using MyClassLibrary.Common;
using MyFtpServer.DAL;
using Newtonsoft.Json;
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
using File = MyFtpServer.DAL.Entities.File;

namespace MyFtpServer
{
    public class FtpServer
    {
        private string _host;
        private int _port;
        private TcpListener _controlSocket;
        private bool _isRunning = false;
        private string _rootPath = @"D:\FileServer";
        int _sessionID = 0;
        private long largeFileThreshold = (long)Math.Pow(2, 20) * 1024 * 4; // 4GB
        List<ClientConnection> _connections = new List<ClientConnection>();

        public FtpServer(string host, int port, string rootPath, CommandReceivedHandler commandReceived)
        {
            if (!Directory.Exists(rootPath))
                throw new Exception("Root path not exist");
            try
            {
                _host = host;
                _port = port;
                _rootPath = rootPath;
                CommandReceived += commandReceived;
                _controlSocket = new TcpListener(IPAddress.Parse(_host), _port);
                _passivePort = 30000;
            } catch (Exception ex)
            {
                throw new Exception("FTP Server error: " + ex.Message);
            }
        }

        public void SetConfiguration(string host, int port, string rootPath)
        {
            try {
                _host = host;
                _port = port;
                _rootPath = rootPath;
                _controlSocket = new TcpListener(IPAddress.Parse(_host), _port);
            } catch (Exception ex)
            {
                throw new Exception("FTP Server error: " + ex.Message);
            }
        }

        private object _lock = new object();
        public List<ClientConnection> GetConnectedClients()
        {
            lock (_lock)
            {
                return new List<ClientConnection>(_connections);
            }
        }

        public List<ClientConnection> GetConnectedClients(int accountId)
        {
            lock (_lock)
            {
                return new List<ClientConnection>(_connections.Where(c => c.IdAccount == accountId));
            }
        }

        public void ServerStart()
        {
            try
            {
                _controlSocket.Start();
                _isRunning = true;
                ResponseStatus(0, $"FTP Server already start at {_host}:{_port}");
                while (_isRunning)
                {
                    TcpClient client = _controlSocket.AcceptTcpClient();
                    ClientConnection clientConnection = new ClientConnection(0, client);
                    _connections.Add(clientConnection);
                    Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient));
                    clientThread.Start(clientConnection);
                }
            }
            catch (Exception ex)
            {
                _controlSocket.Stop();
                Console.WriteLine($"Server error: {ex.Message}");
            }
        }

        private async void HandleClient(object? clientConnectionPar)
        {
            if (clientConnectionPar == null)
                return;

            int sessionID = GetSessionID();

            ClientConnection clientConnection = (ClientConnection)clientConnectionPar;

            TcpClient tcpClient = clientConnection.TcpClient;
            NetworkStream ns = tcpClient.GetStream();
            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns) { AutoFlush = true };

            IPEndPoint iPEndPoint = (IPEndPoint)tcpClient.Client.RemoteEndPoint;

            ResponseStatus(sessionID, $"220 FTP Server already for {iPEndPoint.Address}:{iPEndPoint.Port}");

            int idAccount;
            try
            {
                do
                {
                    idAccount = Authenticate(sessionID, reader, writer);
                } while (idAccount == 0);
            } catch (Exception ex)
            {
                ResponseStatus(sessionID, $"530 {ex.Message}");
                _connections.Remove(clientConnection);
                return;
            }
            clientConnection.IdAccount = idAccount;
            
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
                    else if (command == "GETDETAILFILE")
                    {
                        string idFile = string.Join(" ", parts, 1, parts.Length - 1);
                        FileStorageDAL dal = new FileStorageDAL();
                        FileDetailVM? fileDetailVM = dal.GetDetailFile(idAccount, idFile, _rootPath);
                        if (fileDetailVM == null)
                        {
                            writer.WriteLine("550 File not found");
                            ResponseStatus(sessionID, $"550 File not found");
                            continue;
                        }
                        string json = JsonConvert.SerializeObject(fileDetailVM);
                        writer.WriteLine($"257 {json}");
                    }
                    else if (command == "RNTO")
                    {
                        string newName = string.Join(" ", parts, 1, parts.Length - 1);
                        if (newName == null)
                        {
                            writer.WriteLine("501 Syntax error in parameters or arguments");
                            ResponseStatus(sessionID, $"501 Syntax error in parameters or arguments");
                            continue;
                        }
                        FileStorageDAL dal = new FileStorageDAL();
                        string id = dal.RenameFolder(idAccount, remoteFolderPath, newName);
                        if (id == "")
                        {
                            writer.WriteLine("550 Directory already exists");
                            ResponseStatus(sessionID, $"550 Directory already exists");
                            continue;
                        }
                        writer.WriteLine($"250 {id}");
                        ResponseStatus(sessionID, $"250 Directory renamed");
                    }
                    else if (command == "DELE")
                    {
                        string idFile = string.Join(" ", parts, 1, parts.Length - 1);

                        FileStorageDAL dal = new FileStorageDAL();
                        string id = dal.DeleteFile(idAccount, idFile);
                        if (id == "")
                        {
                            writer.WriteLine("550 File not found");
                            ResponseStatus(sessionID, $"550 File not found");
                            continue;
                        }
                        writer.WriteLine($"250 {id}");
                        ResponseStatus(sessionID, $"250 File deleted");
                    } else if (command == "TRUNCATEFILE")
                    {
                        string idFile = string.Join(" ", parts, 1, parts.Length - 1);
                        FileStorageDAL dal = new FileStorageDAL();
                        if (await dal.TruncateFile(idAccount, idFile, _rootPath) == true)
                        {
                            writer.WriteLine($"250 {idFile}");
                            ResponseStatus(sessionID, $"250 File truncated");
                        }
                        else
                        {
                            writer.WriteLine("550 File not found");
                            ResponseStatus(sessionID, $"550 File not found");
                        }
                    }
                    else if (command == "RESTOREFILE")
                    {
                        string idFile = string.Join(" ", parts, 1, parts.Length - 1);
                        FileStorageDAL dal = new FileStorageDAL();
                        if (dal.RestoreFile(idAccount, idFile) == true)
                        {
                            writer.WriteLine($"250 {idFile}");
                            ResponseStatus(sessionID, $"250 File restored");
                        }
                        else
                        {
                            writer.WriteLine("550 File not found");
                            ResponseStatus(sessionID, $"550 File not found");
                        }
                    }
                    else if (command == "RMD")
                    {
                        string idFolder = string.Join(" ", parts, 1, parts.Length - 1);

                        FileStorageDAL dal = new FileStorageDAL();
                        string id = dal.DeleteFolder(idAccount, idFolder);
                        if (id == "")
                        {
                            writer.WriteLine("550 Directory not found");
                            ResponseStatus(sessionID, $"550 Directory not found");
                            continue;
                        }
                        writer.WriteLine($"250 {id}");
                    } else if (command == "TRUNCATEFOLDER")
                    {
                        string idFolder = string.Join(" ", parts, 1, parts.Length - 1);

                        FileStorageDAL dal = new FileStorageDAL();
                        await dal.TruncateFolder(idAccount, idFolder, _rootPath);
                        
                        writer.WriteLine("250 Delete successful");
                        ResponseStatus(sessionID, $"250 Delete successful");
                    }
                    else if (command == "GETLISTFILEACCESS")
                    {
                        string idFile = string.Join(" ", parts, 1, parts.Length - 1);
                        FileStorageDAL dal = new FileStorageDAL();
                        List<FileAccessVM>? list = dal.GetListFileAccess(idAccount, idFile, _rootPath);
                        if (list == null)
                        {
                            writer.WriteLine("550 File not found");
                            ResponseStatus(sessionID, $"550 File not found");
                            continue;
                        }
                        string json = JsonConvert.SerializeObject(list);
                        writer.WriteLine($"257 {json}");
                    }
                    else if (command == "UPDATEFILEACCESS")
                    {
                        string json = string.Join(" ", parts, 1, parts.Length - 1);
                        List<FileAccessVM> list = JsonConvert.DeserializeObject<List<FileAccessVM>>(json);
                        FileStorageDAL dal = new FileStorageDAL();
                        if (dal.UpdateFileAccess(idAccount, list) == false)
                        {
                            writer.WriteLine("550 File not found");
                            ResponseStatus(sessionID, $"550 File not found");
                            continue;
                        }
                        writer.WriteLine($"257 Oke");
                    }
                    else if (command == "MLSD")
                    {
                        // Get list file and folder

                        string json = string.Join(" ", parts, 1, parts.Length - 1);
                        GetListFileRequest request1 = JsonConvert.DeserializeObject<GetListFileRequest>(json);

                        FileStorageDAL dal = new FileStorageDAL();
                        FileInforPackage fileInforPackage = new FileInforPackage();
                        if (request1 != null)
                        {
                            if (request1.IdAccess == IdAccess.Owner)
                            {
                                fileInforPackage = dal.GetFileInfors(idAccount, request1.IdParent);
                            }
                            else if (request1.IdAccess == IdAccess.Shared)
                            {
                                fileInforPackage = dal.GetSharedFilePackage(idAccount, request1.IdParent);
                            }
                            else if (request1.IdAccess == IdAccess.CanDownload)
                            {
                                fileInforPackage = dal.GetCanDownloadFilePackage(idAccount, request1.IdParent);
                            }
                            else if (request1.IdAccess == IdAccess.Deleted)
                            {
                                fileInforPackage = dal.GetDeletedFilePackage(idAccount);
                            }
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
                        clientConnection.DataClient = data_channel;
                        FileServerProcessing processing = new FileServerProcessing(data_channel);
                        writer.WriteLine("150 Opening data connection");
                        ResponseStatus(sessionID, "150 Opening data connection");
                        processing.SendFileInforPackage(fileInforPackage);

                        writer.WriteLine("226 Transfer complete");
                        ResponseStatus(sessionID, "226 Transfer complete");
                        if (tcpListener != null)
                            tcpListener.Stop();
                    }
                    else if (command == "RETR")
                    {
                        FileStorageDAL dal = new FileStorageDAL();
                        string fullPath = _rootPath + dal.GetFilePath(remoteFolderPath);

                        if (!System.IO.File.Exists(fullPath))
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
                        clientConnection.DataClient = data_channel;
                        FileServerProcessing processing = new FileServerProcessing(data_channel);

                        if (new FileInfo(fullPath).Length > largeFileThreshold)
                        {
                            writer.WriteLine($"213 File very large");
                            ResponseStatus(sessionID, $"213 File very large");
                            if (tcpListener != null)
                                tcpListener.Stop();
                            continue;
                        }

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
                    else if (command == "CHECKCANUPLOAD")
                    {
                        FileStorageDAL dal = new FileStorageDAL();
                        if (dal.CheckCanUpload(idAccount, remoteFolderPath) == true)
                        {
                            writer.WriteLine("200 Oke");
                            ResponseStatus(sessionID, $"200 Oke");
                        }
                        else
                        {
                            writer.WriteLine("550 Can't upload");
                            ResponseStatus(sessionID, $"550 Can't upload");
                        }
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
                        File? createFile = dal.CreateNewFile(idAccount, remoteFolderPath, fileName);
                        if (createFile == null)
                        {
                            writer.WriteLine("550 Couldn't create the file or directory");
                            ResponseStatus(sessionID, $"550 Couldn't create the file or directory");
                            continue;
                        }
                        string fullPath = _rootPath + createFile.FilePath;

                        if (tcpListener == null)
                            continue;
                        data_channel = tcpListener.AcceptTcpClient();
                        if (data_channel == null)
                        {
                            writer.WriteLine("425 Can't open data connection.");
                            ResponseStatus(sessionID, $"425 Can't open data connection.");
                            continue;
                        }
                        clientConnection.DataClient = data_channel;
                        FileServerProcessing processing = new FileServerProcessing(data_channel);

                        writer.WriteLine("150 Opening data connection");
                        ResponseStatus(sessionID, $"150 Opening data connection");

                        try
                        {
                            processing.ReceiveFile(fullPath);
                        } catch (Exception ex)
                        {
                            await dal.TruncateFile(idAccount, createFile.Id, _rootPath);
                            if (ex.Message == "File is exist")
                            {
                                writer.WriteLine("550 File is exist");
                                ResponseStatus(sessionID, $"550 File is exist");
                            } else if (ex.Message == "Can't create path")
                            {
                                writer.WriteLine("Can't create path");
                                ResponseStatus(sessionID, $"Can't create path");
                            }
                            else if (ex.Message == "Can't transfer data")
                            {
                                writer.WriteLine("Can't transfer data");
                                ResponseStatus(sessionID, $"Can't transfer data");
                            }
                            continue;
                        }

                        writer.WriteLine("226 Transfer complete");
                        ResponseStatus(sessionID, $"226 Transfer complete");

                        if (tcpListener != null)
                            tcpListener.Stop();
                    }
                    else if (command == "EXPRESSUPLOAD")
                    {
                        FileStorageDAL dal = new FileStorageDAL();
                        if (dal.IsDirectory(remoteFolderPath) == false)
                        {
                            writer.WriteLine("550 Couldn't open the file or directory");
                            ResponseStatus(sessionID, $"550 Couldn't open the file or directory");
                            continue;
                        }

                        string fileName = string.Join(" ", parts, 1, parts.Length - 1);
                        File? createFile = dal.CreateNewFile(idAccount, remoteFolderPath, fileName);
                        if (createFile == null)
                        {
                            writer.WriteLine("550 Couldn't create the file or directory");
                            ResponseStatus(sessionID, $"550 Couldn't create the file or directory");
                            continue;
                        }

                        string fullPath = _rootPath + createFile.FilePath;

                        if (tcpListener == null)
                            continue;

                        writer.WriteLine("150 Opening data connection");
                        ResponseStatus(sessionID, $"150 Opening data connection");

                        long length = long.Parse(reader.ReadLine() ?? "0");

                        FileServerExpressProcessing processing = new FileServerExpressProcessing(tcpListener, fullPath, length);
                        try
                        {
                            processing.ReceiveExpressFile();
                        } catch (Exception ex)
                        {
                            await dal.TruncateFile(idAccount, createFile.Id, _rootPath);
                            if (ex.Message == "File is exist")
                            {
                                writer.WriteLine("550 File is exist");
                                ResponseStatus(sessionID, $"550 File is exist");
                            }
                            else if (ex.Message == "Can't create path")
                            {
                                writer.WriteLine("Can't create path");
                                ResponseStatus(sessionID, $"Can't create path");
                            }
                            else if (ex.Message == "Can't transfer data")
                            {
                                writer.WriteLine("Can't transfer data");
                                ResponseStatus(sessionID, $"Can't transfer data");
                            }
                            continue;
                        }

                        writer.WriteLine("226 Transfer complete");
                        ResponseStatus(sessionID, $"226 Transfer complete");
                        if (tcpListener != null)
                            tcpListener.Stop();
                    }
                    else if (command == "EXPRESSDOWNLOAD")
                    {
                        FileStorageDAL dal = new FileStorageDAL();
                        string fullPath = _rootPath + dal.GetFilePath(remoteFolderPath);

                        if (!System.IO.File.Exists(fullPath))
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

                        FileServerExpressProcessing processing = new FileServerExpressProcessing(tcpListener, fullPath, new FileInfo(fullPath).Length);
                        processing.SendExpressFile();

                        command = reader.ReadLine() ?? "550";
                        if (command.StartsWith("226"))
                        {
                            ResponseStatus(sessionID, $"226 Transfer complete");
                        }
                        else
                        {
                            ResponseStatus(sessionID, $"550 Transfer error");
                        }

                        if (tcpListener != null)
                            tcpListener.Stop();
                    }
                    else if (command == "GETACCOUNTINFOR")
                    {
                        AccountDAL accountDAL = new AccountDAL();
                        AccountInfoVM account = accountDAL.GetAccount(idAccount);
                        if (account == null)
                        {
                            writer.WriteLine("550 Account not exist");
                            ResponseStatus(sessionID, "550 Account not exist");
                            continue;
                        }
                        account.Avatar = accountDAL.GetAvatar(idAccount, _rootPath);
                        FileStorageDAL fileStorageDAL = new FileStorageDAL();
                        account.UsedStorage = fileStorageDAL.GetUsedStorage(idAccount, _rootPath);

                        data_channel = tcpListener.AcceptTcpClient();
                        if (data_channel == null)
                        {
                            writer.WriteLine("425 Can't open data connection.");
                            ResponseStatus(sessionID, $"425 Can't open data connection.");
                            continue;
                        }
                        clientConnection.DataClient = data_channel;

                        writer.WriteLine("150 Opening data connection");
                        ResponseStatus(sessionID, $"150 Opening data connection");

                        AccountServerProcessing asp = new AccountServerProcessing(data_channel);
                        asp.SendAccountInfor(account);

                        writer.WriteLine("226 Transfer complete");
                        ResponseStatus(sessionID, $"226 Transfer complete");
                    }
                    else if (command == "UPDATEACCOUNTINFOR")
                    {
                        data_channel = tcpListener.AcceptTcpClient();
                        if (data_channel == null)
                        {
                            writer.WriteLine("425 Can't open data connection.");
                            ResponseStatus(sessionID, $"425 Can't open data connection.");
                            continue;
                        }
                        clientConnection.DataClient = data_channel;
                        writer.WriteLine("150 Opening data connection");
                        ResponseStatus(sessionID, $"150 Opening data connection");
                        AccountServerProcessing asp = new AccountServerProcessing(data_channel);
                        AccountInfoVM account = asp.ReceiveAccountInfor();
                        AccountDAL accountDAL = new AccountDAL();
                        accountDAL.UpdateAccount(idAccount, account);
                        StoreDataHelper storeDataHelper = new StoreDataHelper();
                        if (storeDataHelper.SaveDataToFilePath(account.Avatar, _rootPath + @"\avatars\" + idAccount + ".png") == true)
                        {
                            accountDAL.UpdateAvatar(idAccount, @"\avatars\" + idAccount + ".png");
                        }
                        writer.WriteLine("226 Transfer complete");
                        ResponseStatus(sessionID, $"226 Transfer complete");
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
                _connections.Remove(clientConnection);
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
            else
            {
                writer.WriteLine("530 Not logged in");
                ResponseStatus(sessionID, $"530 Not logged in");
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
                if (_passivePort > 65000)
                    _passivePort = 30000;
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
                return _sessionID+=1;
            }
        }

        public delegate void CommandReceivedHandler(string message);
        public event CommandReceivedHandler CommandReceived;

        private void CommandStatus(int sessionId, string message)
        {
            DateTime now = DateTime.Now;
            CommandReceived($"C> {now}\tSession {sessionId}\t {message}");
        }

        private void ResponseStatus(int sessionId, string message)
        {
            DateTime now = DateTime.Now;
            CommandReceived($"S> {now}\tSession {sessionId}\t {message}");
        }

        public void Stop()
        {
            _isRunning = false;
            _controlSocket.Stop();
            _connections.ForEach(c => c.Close());
            _connections.Clear();
        }

        public void Disconnect(int id)
        {
            var connections = _connections.Where(c => c.IdAccount == id);
            if (connections != null)
            {
                foreach (var item in connections)
                {
                    item.Close();
                    connections.ToList().Remove(item);
                }
            }
        }
    }
}
