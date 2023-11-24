using ConsoleApp;
using MyClassLibrary;
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
        private string _remoteFolderPath = "\\";

        public MainForm_BLL(TransferProgress process, OnChangeFolderAndFile changeFolderAndFile)
        {
            fileManager = new FileManager(@"D:\FileClient");
            ftpClient = new FtpClient("127.0.0.1", 1234, TransferProgressHandler, ChangeFoldersAndFileHandler);
            progress += process;
            this.changeFolderAndFile += changeFolderAndFile;
        }

        public void GetFileInfos()
        {
            ftpClient.ListRemoteFolderAndFiles("\\");
        }

        public void Download(string fileName)
        {
            ftpClient.Download($"{((_remoteFolderPath == "\\") ? $"{_remoteFolderPath}" : $"{_remoteFolderPath}\\")}{fileName}", fileManager.GetCurrentPath());
        }

        public void DownloadFolder(string folderName)
        {
            ftpClient.DownloadFolder($"{((_remoteFolderPath == "\\") ? $"{_remoteFolderPath}" : $"{_remoteFolderPath}\\")}{folderName}", $"{fileManager.GetCurrentPath()}\\{folderName}");
        }
        public void ChangeFolder(string folderName)
        {
            _remoteFolderPath = (_remoteFolderPath == "\\") ? $"\\{folderName}" : $"{_remoteFolderPath}\\{folderName}";
            ftpClient.ListRemoteFolderAndFiles(_remoteFolderPath);
        }
        public void Back()
        {
            if (_remoteFolderPath == "")
                _remoteFolderPath = "\\";
            if (_remoteFolderPath != "\\")
            {
                _remoteFolderPath = _remoteFolderPath.Substring(0, _remoteFolderPath.LastIndexOf('\\'));
            }
            ftpClient.ListRemoteFolderAndFiles(_remoteFolderPath);
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
            ftpClient.UploadFolder((_remoteFolderPath == "\\") ? $"\\{folderName}" : $"{_remoteFolderPath}\\{folderName}", selectedPath);
        }
    }
}
