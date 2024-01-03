using MyClassLibrary;
using MyClassLibrary.Bean.Account;
using MyClassLibrary.Bean.File;
using MyClassLibrary.Common;
using MyFtpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.BLL
{
    public class MainForm_BLL
    {
        private readonly FileManager fileManager;
        private readonly FtpClient ftpClient;
        private string _remoteFolderPath = "";

        public MainForm_BLL(FtpClient ftpClient, TransferProgress process, OnChangeFolderAndFile changeFolderAndFile, OnGetAccountInfor onGetAccountInfor, OnGetDetailFile onGetDetailFile, LogoutDelegate logoutDelegate)
        {
            fileManager = new FileManager();
            this.ftpClient = ftpClient;
            ftpClient.Start(TransferProgressHandler, ChangeFoldersAndFileHandler, GetAccountInfor, OnGetDetailFileHandler, OnGetListFileAccessHandler, LogoutHandler);
            progress += process;
            this.changeFolderAndFile += changeFolderAndFile;
            this.getAccountInfor += onGetAccountInfor;
            this.getDetailFile += onGetDetailFile;
            this.logout += logoutDelegate;
        }

        public void GetFileInfos()
        {
            ftpClient.ListRemoteFolderAndFiles("");
        }
        public void CreateFolder(string inputText)
        {
            CreateFolderRequest request = new CreateFolderRequest()
            {
                FolderName = inputText,
                ParentFolderId = _remoteFolderPath
            };
            ftpClient.CreateFolder(request);
        }

        public delegate void OnGetDetailFile(FileDetailVM fileDetailVM, List<FileAccessVM> fileAccessVMs);
        public event OnGetDetailFile getDetailFile;

        private void OnGetDetailFileHandler(FileDetailVM fileDetailVM, List<FileAccessVM> fileAccessVMs)
        {
            getDetailFile(fileDetailVM, fileAccessVMs);
        }

        public void GetDetailFile(string id)
        {
            ftpClient.GetDetailFile(id);
        }

        public void RenameFile(RenameFileRequest sender)
        {
            ftpClient.RenameFile(sender);
        }

        public void DeleteFile(DeleteFileRequest sender)
        {
            ftpClient.DeleteFile(sender);
        }

        public void TruncateFile(TruncateFileRequest sender)
        {
            ftpClient.TruncateFile(sender);
        }

        public void DeleteFolder(DeleteFileRequest sender)
        {
            ftpClient.DeleteFolder(sender);
        }

        public void TruncateFolder(TruncateFileRequest sender)
        {
            ftpClient.TruncateFolder(sender);
        }

        public void Download(FileInfor fileInfor)
        {
            ftpClient.Download(fileInfor.Id, fileManager.GetCurrentPath(), fileInfor.Name);
        }

        public void ExpressDownload(string fileName)
        {
            ftpClient.ExpressDownload($"{((_remoteFolderPath == "\\") ? $"{_remoteFolderPath}" : $"{_remoteFolderPath}\\")}{fileName}", fileManager.GetCurrentPath());
        }

        public void DownloadFolder(FileInfor fileInfor)
        {
            DownloadFolderRequest request = new DownloadFolderRequest()
            {
                RemoteFolderId = fileInfor.Id,
                FullLocalPath = $"{fileManager.GetCurrentPath()}\\{fileInfor.Name}",
            };
            ftpClient.DownloadFolder(request);
        }
        public void ChangeFolder(string idFolder)
        {
            _remoteFolderPath = idFolder;
            ftpClient.ListRemoteFolderAndFiles(idFolder);
        }
        public void Back()
        {
            _remoteFolderPath = "";
            ftpClient.ListRemoteFolderAndFiles("");
        }

        public delegate void TransferProgress(FileTransferProcessing sender);
        public event TransferProgress progress;

        private void TransferProgressHandler(FileTransferProcessing sender)
        {
            if (progress != null)
                progress(sender);
        }

        public delegate void OnChangeFolderAndFile(FileInforPackage sender);
        public event OnChangeFolderAndFile changeFolderAndFile;

        private void ChangeFoldersAndFileHandler(FileInforPackage fileInfors)
        {
            if (fileInfors != null)
                changeFolderAndFile(fileInfors);
        }

        public delegate void OnGetAccountInfor(AccountInfoVM sender);
        public event OnGetAccountInfor getAccountInfor;

        private void GetAccountInfor(AccountInfoVM account)
        {
            if (account != null)
                getAccountInfor(account);
        }

        public void Dispose()
        {
            ftpClient.Dispose();
        }

        public void Upload(string filePath)
        {
            ftpClient.Upload(_remoteFolderPath, filePath);
        }

        public void UploadFolder(string selectedPath)
        {
            UploadFolderRequest request = new UploadFolderRequest()
            {
                FolderName = selectedPath.Substring(selectedPath.LastIndexOf('\\') + 1),
                ParentFolderId = _remoteFolderPath,
                FullLocalPath = selectedPath
            };
            ftpClient.UploadFolder(request);
        }

        // Manage account
        public void GetAccountInfor()
        {
            ftpClient.GetAccountInfor();
        }

        public void UpdateAccountInfor(AccountInfoVM account)
        {
            ftpClient.UpdateAccountInfor(account);
        }

        // File access
        public delegate void OnGetListFileAccess(List<FileAccessVM> sender);
        public event OnGetListFileAccess onGetListFileAccess;

        private void OnGetListFileAccessHandler(List<FileAccessVM> fileAccessVMs)
        {
            onGetListFileAccess(fileAccessVMs);
        }
        public void GetListFileAccess(string idFile)
        {
            ftpClient.GetListFileAccess(idFile);
        }

        public void UpdateFileAccess(List<FileAccessVM> fileAccessVMs)
        {
            ftpClient.UpdateFileAccess(fileAccessVMs);
        }

        public void ChangeSharedFolder(string idFolder)
        {
            _remoteFolderPath = idFolder;
            ftpClient.ChangeSharedFolder(idFolder);
        }

        internal void ChangeDeletedFolder(string idFolder)
        {
            _remoteFolderPath = idFolder;
            ftpClient.ChangeDeletedFolder(idFolder);
        }

        public void RestoreFile(RestoreFileRequest request)
        {
            ftpClient.RestoreFile(request);
        }

        public delegate void LogoutDelegate();
        public event LogoutDelegate logout;

        private void LogoutHandler()
        {
            logout();
        }
    }
}
