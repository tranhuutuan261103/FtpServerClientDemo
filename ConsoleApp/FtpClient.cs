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
        public FtpClient(string host, int port, TransferProgress process)
        {
            _host = host;
            _port = port;
            _client = new TcpClient();
            
            _client.Connect(_host, _port);

            ftpClientSession = new FtpClientSession(this);
            Thread thread = new Thread(() => ftpClientSession.Process());
            thread.Start();

            progress += process;
        }

        public string GetHost()
        {
            return _host;
        }

        public int GetPort()
        {
            return _port;
        }

        public delegate void TransferProgress(FileTransferProcessing sender);
        public event TransferProgress progress;

        public void TransferProgressHandler(FileTransferProcessing sender)
        {
            progress?.Invoke(sender);
        }

        public void Dispose()
        {
            _client.Dispose();
            ftpClientSession.Dispose();
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

        public string GetRemoteFolderPath()
        {
            FtpClientProcessing fcp = new FtpClientProcessing(_client, TransferProgressHandler);
            return fcp.GetRemoteFolderPath();
        }

        public bool CreateNewRemoteFolder(string remoteFolderName)
        {
            FtpClientProcessing fcp = new FtpClientProcessing(_client, TransferProgressHandler);
            return fcp.CreateNewRemoteFolder(remoteFolderName);
        }
        public bool SetRemoteFolderPath(string remoteFolderPath)
        {
            FtpClientProcessing fcp = new FtpClientProcessing(_client, TransferProgressHandler);
            return fcp.SetRemoteFolderPath(remoteFolderPath);
        }

        public List<FileInfor> ListRemoteFolderAndFiles()
        {
            FtpClientProcessing fcp = new FtpClientProcessing(_client, TransferProgressHandler);
            List<FileInfor> list = fcp.ListRemoteFolderAndFiles();
            return list;
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
    }
}
