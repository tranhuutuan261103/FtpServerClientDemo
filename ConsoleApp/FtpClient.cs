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
        private TcpSession _mainTcpSession;
        private List<TaskSession> _mainTaskSessions = new List<TaskSession>();
        private TcpSession[] subTcpSession = new TcpSession[2];
        private List<FileTransferProcessing> _subTaskSessions = new List<FileTransferProcessing>();
        public FtpClient(string host, int port, TransferProgress process, OnChangeFolderAndFile changeFolderAndFile)
        {
            _host = host;
            _port = port;

            _mainTcpSession = new TcpSession(_host, _port);
            _mainTcpSession.Connect();
            for (int i = 0; i < subTcpSession.Length; i++)
            {
                subTcpSession[i] = new TcpSession(_host, _port);
            }
            Thread threadMain = new Thread(MainProcess);
            threadMain.Start();

            Thread threadSub = new Thread(SubProcess);
            threadSub.Start();

            progress += process;
            this.changeFolderAndFile += changeFolderAndFile;
        }

        private void PushMainTaskSession(TaskSession taskSession)
        {
            lock(_mainTaskSessionLock)
            {
                _mainTaskSessions.Add(taskSession);
            }
        }

        private void PushSubTaskSession(FileTransferProcessing taskSession)
        {
            lock (_subTaskSessionLock)
            {
                _subTaskSessions.Add(taskSession);
            }
            TransferProgressHandler(taskSession);
        }

        private void MainProcess()
        {
            while (true)
            {
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
            if (_subTaskSessions.Count > 0)
            {
                _subTaskSessions.RemoveAt(0);
                return true;
            }
            return false;
        }

        private FileTransferProcessing? GetFirstSubTaskSession()
        {
            if (_subTaskSessions.Count > 0)
            {
                return _subTaskSessions[0];
            }
            return null;
        }

        private void SubProcess()
        {
            while (true)
            {
                FileTransferProcessing? command = GetFirstSubTaskSession();
                if (command != null)
                {
                    foreach (TcpSession tcpSession in subTcpSession)
                    {
                        if (tcpSession.GetStatus() == TcpSessionStatus.Connected)
                        {
                            PopSubTaskSession();
                            tcpSession.SetStatus(TcpSessionStatus.Busy);
                            Thread thread = new Thread(() => HandleCommand(command, tcpSession));
                            thread.Start();
                            break;
                        }
                        else if (tcpSession.GetStatus() == TcpSessionStatus.Closed)
                        {
                            tcpSession.Connect();
                            PopSubTaskSession();
                            tcpSession.SetStatus(TcpSessionStatus.Busy);
                            Thread thread = new Thread(() => HandleCommand(command, tcpSession));
                            thread.Start();
                            break;
                        }
                    }
                }
            }
        }

        private void HandleCommand(FileTransferProcessing command, TcpSession tcpSession)
        {
            ExecuteSubTaskSession(command, tcpSession.GetTcpClient());
            tcpSession.SetStatus(TcpSessionStatus.Connected);
        }

        private void ExecuteMainTaskSession(TaskSession taskSession)
        {
            FtpClientProcessing fcp = new FtpClientProcessing(_mainTcpSession.GetTcpClient(), null, TransferRequestHandler);
            switch (taskSession.Type)
            {
                case "LIST":
                    {
                        List<FileInfor> fileInfors = fcp.ListRemoteFoldersAndFiles(taskSession.RemotePath);
                        ChangeFolderAndFileHandler(fileInfors);
                    }
                    break;
                case "DOWNLOADFOLDER":
                    {
                        fcp.DownloadFolder(taskSession.RemotePath, taskSession.LocalPath);
                    }
                    break;
                case "UPLOADFOLDER":
                    {
                        fcp.UploadFolder(taskSession.RemotePath, taskSession.LocalPath);
                    }
                    break;
                default:
                    break;
            }
        }

        private void ExecuteSubTaskSession(FileTransferProcessing request, TcpClient tcpSessionClient)
        {
            FtpClientProcessing fcp = new FtpClientProcessing(tcpSessionClient, TransferProgressHandler, null);
            string command = request.Type;
            switch (command)
            {
                case "STOR":
                    fcp.SendFile(request, tcpSessionClient);
                    break;
                case "EXPRESSUPLOAD":
                    fcp.ExpressSendFile(request, tcpSessionClient);
                    break;
                case "RETR":
                    fcp.ReceiveFile(request, tcpSessionClient);
                    break;
                case "EXPRESSDOWNLOAD":
                    fcp.ExpressReceiveFile(request, tcpSessionClient);
                    break;
                default:
                    break;
            }
        }

        public delegate void TransferProgress(FileTransferProcessing sender);
        public event TransferProgress progress;

        public void TransferProgressHandler(FileTransferProcessing sender)
        {
            progress?.Invoke(sender);
        }

        public delegate void OnChangeFolderAndFile(List<FileInfor> sender);
        public event OnChangeFolderAndFile changeFolderAndFile;

        public void ChangeFolderAndFileHandler(List<FileInfor> sender)
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

        public void Download(string remotePath, string localPath)
        {
            FileTransferProcessing taskSession = new FileTransferProcessing("RETR", Path.GetDirectoryName(remotePath) ?? "\\undefine", Path.GetFileName(remotePath) ?? "", localPath);
            PushSubTaskSession(taskSession);
        }

        public void DownloadFolder(string remoteFolderPath, string localFolderPath)
        {
            TaskSession taskSession = new TaskSession("DOWNLOADFOLDER", remoteFolderPath, "", localFolderPath);
            PushMainTaskSession(taskSession);
        }

        public void Upload(string remotePath, string localPath)
        {
            FileTransferProcessing taskSession = new FileTransferProcessing("STOR", remotePath, Path.GetFileName(localPath) ?? "", Path.GetDirectoryName(localPath) ?? "\\undefine");
            PushSubTaskSession(taskSession);
        }

        public void UploadFolder(string remoteFolderPath, string localFolderPath)
        {
            TaskSession taskSession = new TaskSession("UPLOADFOLDER", remoteFolderPath, "", localFolderPath);
            PushMainTaskSession(taskSession);
        }

        public void ListRemoteFolderAndFiles(string remoteFolderPath)
        {
            TaskSession taskSession = new TaskSession("LIST", remoteFolderPath, "", "");
            PushMainTaskSession(taskSession);
        }

        private object _mainTaskSessionLock = new object();
        private object _subTaskSessionLock = new object();
    }
}
