using ConsoleApp;
using MyClassLibrary;
using MyClassLibrary.Bean.Account;
using MyClassLibrary.Bean.File;
using MyClassLibrary.Common;
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

        public MainForm_BLL(FtpClient ftpClient, TransferProgress process, OnChangeFolderAndFile changeFolderAndFile, OnGetAccountInfor onGetAccountInfor)
        {
            fileManager = new FileManager();
            this.ftpClient = ftpClient;
            ftpClient.Start(TransferProgressHandler, ChangeFoldersAndFileHandler, GetAccountInfor);
            progress += process;
            this.changeFolderAndFile += changeFolderAndFile;
            this.getAccountInfor += onGetAccountInfor;
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

        public void RenameFile(RenameFileRequest sender)
        {
            ftpClient.RenameFile(sender);
        }

        public void DeleteFile(DeleteFileRequest sender)
        {
            ftpClient.DeleteFile(sender);
        }

        public void DeleteFolder(DeleteFileRequest sender)
        {
            ftpClient.DeleteFolder(sender);
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
            ftpClient.DownloadFolder(fileInfor.Id, $"{fileManager.GetCurrentPath()}\\{fileInfor.Name}");
        }
        public void ChangeFolder(string idFolder)
        {
            _remoteFolderPath = idFolder;
            ftpClient.ListRemoteFolderAndFiles(idFolder);
        }
        public void Back()
        {
            /*if (_remoteFolderPath == "")
                _remoteFolderPath = "\\";
            if (_remoteFolderPath != "")
            {
                _remoteFolderPath = _remoteFolderPath.Substring(0, _remoteFolderPath.LastIndexOf('\\'));
            }*/
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

        public delegate void OnChangeFolderAndFile(List<FileInfor> sender);
        public event OnChangeFolderAndFile changeFolderAndFile;

        private void ChangeFoldersAndFileHandler(List<FileInfor> fileInfors)
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
            string folderName = selectedPath.Substring(selectedPath.LastIndexOf('\\') + 1);
            ftpClient.UploadFolder(folderName, selectedPath);
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
    }
}
