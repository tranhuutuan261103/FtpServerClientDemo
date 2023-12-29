using MyClassLibrary;
using MyClassLibrary.Bean.Account;
using MyClassLibrary.Bean.File;
using MyClassLibrary.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace MyFtpClient
{
    public class FtpClient
    {
        private string _host;
        private int _port;
        private TcpSession _mainTcpSession;
        private List<TaskSession> _mainTaskSessions = new List<TaskSession>();
        private TcpSession[] subTcpSession = new TcpSession[2];
        private List<FileTransferProcessing> _subTaskSessions = new List<FileTransferProcessing>();
        public FtpClient(string host, int port)
        {
            _host = host;
            _port = port;
            _mainTcpSession = new TcpSession(_host, _port, "", "");
            for (int i = 0; i < subTcpSession.Length; i++)
            {
                subTcpSession[i] = new TcpSession(_host, _port, "", "");
            }
        }

        public void SetIPAdressAndPort(string ipAddress, int port)
        {
            _host = ipAddress; 
            _port = port;
        }

        public bool Register(RegisterRequest request)
        {
            return _mainTcpSession.Register(request);
        }

        public bool ResetPassword(ResetPasswordRequest request)
        {
            return _mainTcpSession.ResetPassword(request);
        }


        public bool Login(string username, string password)
        {
            _mainTcpSession.SetUsername(username);
            _mainTcpSession.SetPassword(password);
            _mainTcpSession.SetHost(_host);
            _mainTcpSession.SetPort(_port);
            if (_mainTcpSession.Connect())
            {
                for (int i = 0; i < subTcpSession.Length; i++)
                {
                    subTcpSession[i].SetUsername(username);
                    subTcpSession[i].SetPassword(password);
                    subTcpSession[i].SetHost(_host);
                    subTcpSession[i].SetPort(_port);
                }
                return true;
            }
            return false;
        }

        public void Start(TransferProgress process, OnChangeFolderAndFile changeFolderAndFile, OnGetAccountInfor onGetAccountInfor, OnGetDetailFile onGetDetailFile, OnGetListFileAccess onGetListFileAccess, LogoutDelegate logout)
        {
            this.logout += logout;
            Thread threadMain = new Thread(MainProcess);
            threadMain.Start();

            Task.Run(async () => await SubProcessAsync());

            progress += process;
            this.changeFolderAndFile += changeFolderAndFile;
            this.onGetAccountInfor += onGetAccountInfor;
            this.onGetDetailFile += onGetDetailFile;
            this.onGetListFileAccess += onGetListFileAccess;
        }

        private void PushMainTaskSession(TaskSession taskSession)
        {
            lock (_mainTaskSessionLock)
            {
                _mainTaskSessions.Add(taskSession);
            }
        }

        private object _sub = new object();
        private void PushSubTaskSession(FileTransferProcessing taskSession)
        {
            lock (_sub)
            {
                _subTaskSessions.Add(taskSession);
            }
            TransferProgressHandler(taskSession);
        }

        private void MainProcess()
        {
            while (true)
            {
                if (_mainTcpSession.GetTcpClient().Client == null 
                    || _mainTcpSession.GetTcpClient().Connected == false)
                {
                    //logout();
                    break;
                }
                lock (_mainTaskSessionLock)
                {
                    if (_mainTaskSessions.Count > 0)
                    {
                        TaskSession taskSession = _mainTaskSessions[0];
                        _mainTaskSessions.RemoveAt(0);
                        ExecuteMainTaskSession(taskSession);
                    }
                }
            }
        }
        private bool PopSubTaskSession()
        {
            lock (_lockObject)
            {
                if (_subTaskSessions.Count > 0)
                {
                    _subTaskSessions.RemoveAt(0);
                    return true;
                }
                return false;
            }
        }

        private FileTransferProcessing? GetFirstSubTaskSession()
        {
            lock (_subTaskSessionLock)
            {
                if (_subTaskSessions.Count > 0)
                {
                    return _subTaskSessions[0];
                }
                return null;
            }
        }

        // Disconnect if connected and command null during 30s in SubProcess
        private readonly object _lockObject = new object(); // For thread safety
        private async Task SubProcessAsync()
        {
            while (true)
            {
                FileTransferProcessing? command = GetFirstSubTaskSession();
                if (command != null)
                {
                    TcpSession? availableSession = subTcpSession.FirstOrDefault(session => session.GetStatus() == TcpSessionStatus.Connected);
                    if (availableSession == null)
                    {
                        availableSession = subTcpSession.FirstOrDefault(session => session.GetStatus() == TcpSessionStatus.Closed);
                        if (availableSession != null)
                        {
                            availableSession.Connect();
                        }
                    }

                    if (availableSession != null && availableSession.GetStatus() == TcpSessionStatus.Connected)
                    {
                        availableSession.LastCommandTime = DateTime.Now;
                        availableSession.SetStatus(TcpSessionStatus.Busy);
                        PopSubTaskSession();

                        // Process the command asynchronously in a separate task
                        _ = Task.Run(async () =>
                        {
                            await HandleCommandAsync(command, availableSession);
                            availableSession.SetStatus(TcpSessionStatus.Connected);
                            availableSession.LastCommandTime = DateTime.Now;
                        });
                    }
                }
                else
                {
                    foreach (TcpSession session in subTcpSession)
                    {
                        if (session.GetStatus() == TcpSessionStatus.Connected &&
                            DateTime.Now - session.LastCommandTime > TimeSpan.FromSeconds(30))
                        {
                            // Gracefully disconnect the session
                            session.Disconnect();
                        }
                    }
                }

                // If no command, wait 500ms to avoid constant looping
                await Task.Delay(500);
            }
        }


        private async Task HandleCommandAsync(FileTransferProcessing command, TcpSession tcpSession)
        {
            await ExecuteSubTaskSession(command, tcpSession.GetTcpClient());
        }

        private void ExecuteMainTaskSession(TaskSession taskSession)
        {
            try
            {
                FtpClientProcessing fcp = new FtpClientProcessing(_mainTcpSession.GetTcpClient(), TransferProgressHandler, TransferRequestHandler);
                AccountClientProcessing ap = new AccountClientProcessing(_mainTcpSession.GetTcpClient());
                switch (taskSession.Type)
                {
                    case "CREATEFOLDER":
                        {
                            CreateFolderRequest request = (CreateFolderRequest)taskSession.Data;
                            fcp.CreateFolder(request);
                        }
                        break;
                    case "GETDETAILFILE":
                        {
                            string id = (string)taskSession.Data;
                            FileDetailVM? fileDetailVM = fcp.GetDetailFile(id);
                            if (fileDetailVM != null)
                            {
                                List<FileAccessVM> fileAccessVMs = fcp.GetListFileAccess(id);
                                GetDetailFileHandler(fileDetailVM, fileAccessVMs);
                            }
                        }
                        break;
                    case "RENAMEFILE":
                        {
                            RenameFileRequest request = (RenameFileRequest)taskSession.Data;
                            fcp.RenameFile(request);
                        }
                        break;
                    case "DELETEFILE":
                        {
                            DeleteFileRequest request = (DeleteFileRequest)taskSession.Data;
                            fcp.DeleteFile(request);
                        }
                        break;
                    case "DELETEFOLDER":
                        {
                            DeleteFileRequest request = (DeleteFileRequest)taskSession.Data;
                            fcp.DeleteFolder(request);
                        }
                        break;
                    case "RESTOREFILE":
                        {
                            RestoreFileRequest request = (RestoreFileRequest)taskSession.Data;
                            fcp.RestoreFile(request.FileId);
                        }
                        break;
                    case "GETLISTFILEACCESS":
                        {
                            string id = (string)taskSession.Data;
                            List<FileAccessVM> fileAccessVMs = fcp.GetListFileAccess(id);
                            GetListFileAccessHandler(fileAccessVMs);
                        }
                        break;
                    case "UPDATEFILEACCESS":
                        {
                            List<FileAccessVM> request = (List<FileAccessVM>)taskSession.Data;
                            fcp.UpdateFileAccess(request);
                        }
                        break;
                    case "GETLISTSHAREDFILE":
                        {
                            GetListFileRequest request = new GetListFileRequest()
                            {
                                IdParent = (string)taskSession.Data ?? "",
                                IdAccess = IdAccess.Shared
                            };
                            FileInforPackage fileInfors = fcp.GetFileInforPackage(request);
                            ChangeFolderAndFileHandler(fileInfors);
                        }
                        break;
                    case "GETLISTDELETEDFILE":
                        {
                            GetListFileRequest request = new GetListFileRequest()
                            {
                                IdParent = (string)taskSession.Data ?? "",
                                IdAccess = IdAccess.Deleted
                            };
                            FileInforPackage fileInfors = fcp.GetFileInforPackage(request);
                            ChangeFolderAndFileHandler(fileInfors);
                        }
                        break;
                    case "LIST":
                        {
                            GetListFileRequest request = new GetListFileRequest()
                            {
                                IdParent = (string)taskSession.Data ?? "",
                                IdAccess = IdAccess.Owner
                            };
                            FileInforPackage fileInfors = fcp.GetFileInforPackage(request);
                            ChangeFolderAndFileHandler(fileInfors);
                        }
                        break;
                    case "DOWNLOADFOLDER":
                        {
                            DownloadFolderRequest request = (DownloadFolderRequest)taskSession.Data;
                            fcp.DownloadFolder(request.RemoteFolderId, request.FullLocalPath);
                        }
                        break;
                    case "UPLOADFOLDER":
                        {
                            UploadFolderRequest uploadFolderRequest = (UploadFolderRequest)taskSession.Data;
                            if (fcp.CheckCanUpload(uploadFolderRequest.ParentFolderId) == true)
                            {
                                fcp.UploadFolder(uploadFolderRequest.FolderName, uploadFolderRequest.FullLocalPath);
                            }
                        }
                        break;
                    case "GETACCOUNTINFOR":
                        {
                            AccountInfoVM accountInfoVM = ap.GetAccountInfor();
                            GetAccountInforHandler(accountInfoVM);
                        }
                        break;
                    case "UPDATEACCOUNTINFOR":
                        {
                            AccountInfoVM account = (AccountInfoVM)taskSession.Data;
                            ap.UpdateAccountInfor(account);
                        }
                        break;
                    default:
                        break;
                }
            } catch (Exception ex)
            {
                if (ex.Message == "Disconnected")
                {
                    _mainTcpSession.Disconnect();
                    logout();
                }
            }
        }

        private async Task ExecuteSubTaskSession(FileTransferProcessing request, TcpClient tcpSessionClient)
        {
            try
            {
                FtpClientProcessing fcp = new FtpClientProcessing(tcpSessionClient, TransferProgressHandler, TransferRequestHandler);
                string command = request.Type;
                switch (command)
                {
                    case "STOR":
                        if (await fcp.CheckCanUploadAsync(request.RemotePath) == true)
                        {
                            await fcp.SendFileAsync(request, tcpSessionClient);
                        }
                        else
                        {
                            request.Status = FileTransferProcessingStatus.Failed;
                            TransferProgressHandler(request);
                        }
                        break;
                    case "EXPRESSUPLOAD":
                        //await fcp.ExpressSendFile(request, tcpSessionClient);
                        break;
                    case "RETR":
                        await fcp.ReceiveFileAsync(request, tcpSessionClient);
                        break;
                    case "EXPRESSDOWNLOAD":
                        //await fcp.ExpressReceiveFile(request, tcpSessionClient);
                        break;
                    default:
                        break;
                }
            } catch (Exception ex)
            {
                if (ex.Message == "Disconnected")
                {
                    _mainTcpSession.Disconnect();
                    logout();
                }
            }
        }

        public delegate void TransferProgress(FileTransferProcessing sender);
        public event TransferProgress progress;

        public void TransferProgressHandler(FileTransferProcessing sender)
        {
            progress?.Invoke(sender);
        }

        public delegate void OnChangeFolderAndFile(FileInforPackage sender);
        public event OnChangeFolderAndFile changeFolderAndFile;

        public void ChangeFolderAndFileHandler(FileInforPackage sender)
        {
            changeFolderAndFile?.Invoke(sender);
        }

        public void TransferRequestHandler(FileTransferProcessing request)
        {
            PushSubTaskSession(request);
        }

        public void Dispose()
        {
            _mainTcpSession.Dispose();
            foreach (TcpSession tcpSession in subTcpSession)
            {
                tcpSession.Dispose();
            }
        }

        public void CreateFolder(CreateFolderRequest request)
        {
            TaskSession taskSession = new TaskSession("CREATEFOLDER", request);
            PushMainTaskSession(taskSession);
        }

        public delegate void OnGetDetailFile(FileDetailVM sender, List<FileAccessVM> fileAccessVMs);
        public event OnGetDetailFile onGetDetailFile;

        public void GetDetailFileHandler(FileDetailVM sender, List<FileAccessVM> fileAccessVMs)
        {
            onGetDetailFile?.Invoke(sender, fileAccessVMs);
        }

        public void GetDetailFile(string id)
        {
            TaskSession taskSession = new TaskSession("GETDETAILFILE", id);
            PushMainTaskSession(taskSession);
        }

        public void RenameFile(RenameFileRequest request)
        {
            TaskSession taskSession = new TaskSession("RENAMEFILE", request);
            PushMainTaskSession(taskSession);
        }

        public void DeleteFile(DeleteFileRequest request)
        {
            TaskSession taskSession = new TaskSession("DELETEFILE", request);
            PushMainTaskSession(taskSession);
        }

        public void DeleteFolder(DeleteFileRequest request)
        {
            TaskSession taskSession = new TaskSession("DELETEFOLDER", request);
            PushMainTaskSession(taskSession);
        }

        public void Download(string remoteFileId, string localPath, string localFileName)
        {
            FileTransferProcessing taskSession = new FileTransferProcessing("RETR", remoteFileId, localFileName, localPath);
            PushSubTaskSession(taskSession);
        }

        public void ExpressDownload(string remotePath, string localPath)
        {
            FileTransferProcessing taskSession = new FileTransferProcessing("EXPRESSDOWNLOAD", Path.GetDirectoryName(remotePath) ?? "\\undefine", Path.GetFileName(remotePath) ?? "", localPath);
            PushSubTaskSession(taskSession);
        }

        public void DownloadFolder(DownloadFolderRequest request)
        {
            TaskSession taskSession = new TaskSession("DOWNLOADFOLDER", request);
            PushMainTaskSession(taskSession);
        }

        public void Upload(string remotePath, string localPath)
        {
            FileTransferProcessing taskSession = new FileTransferProcessing("STOR", remotePath, Path.GetFileName(localPath) ?? "", Path.GetDirectoryName(localPath) ?? "\\undefine");
            PushSubTaskSession(taskSession);
        }

        public void UploadFolder(UploadFolderRequest request)
        {
            TaskSession taskSession = new TaskSession("UPLOADFOLDER", request);
            PushMainTaskSession(taskSession);
        }

        public void ListRemoteFolderAndFiles(string remoteFolderPath)
        {
            TaskSession taskSession = new TaskSession("LIST", remoteFolderPath);
            PushMainTaskSession(taskSession);
        }

        // Manage account
        public void GetAccountInfor()
        {
            TaskSession taskSession = new TaskSession("GETACCOUNTINFOR", "");
            PushMainTaskSession(taskSession);
        }

        public delegate void OnGetAccountInfor(AccountInfoVM sender);
        public event OnGetAccountInfor onGetAccountInfor;

        public void GetAccountInforHandler(AccountInfoVM sender)
        {
            onGetAccountInfor?.Invoke(sender);
        }

        public void UpdateAccountInfor(AccountInfoVM account)
        {
            TaskSession taskSession = new TaskSession("UPDATEACCOUNTINFOR", account);
            PushMainTaskSession(taskSession);
        }

        // Manage file access
        public delegate void OnGetListFileAccess(List<FileAccessVM> sender);
        public event OnGetListFileAccess onGetListFileAccess;

        public void GetListFileAccessHandler(List<FileAccessVM> sender)
        {
            onGetListFileAccess?.Invoke(sender);
        }
        public void GetListFileAccess(string idFile)
        {
            TaskSession taskSession = new TaskSession("GETLISTFILEACCESS", idFile);
            PushMainTaskSession(taskSession);
        }

        public void UpdateFileAccess(List<FileAccessVM> fileAccessVMs)
        {
            TaskSession taskSession = new TaskSession("UPDATEFILEACCESS", fileAccessVMs);
            PushMainTaskSession(taskSession);
        }

        public void ChangeSharedFolder(string idFolder)
        {
            TaskSession taskSession = new TaskSession("GETLISTSHAREDFILE", idFolder);
            PushMainTaskSession(taskSession);
        }

        public void ChangeDeletedFolder(string idFolder)
        {
            TaskSession taskSession = new TaskSession("GETLISTDELETEDFILE", idFolder);
            PushMainTaskSession(taskSession);
        }

        public void RestoreFile(RestoreFileRequest request)
        {
            TaskSession taskSession = new TaskSession("RESTOREFILE", request);
            PushMainTaskSession(taskSession);
        }

        public delegate void LogoutDelegate();
        public event LogoutDelegate logout;

        private object _mainTaskSessionLock = new object();
        private object _subTaskSessionLock = new object();
    }
}
