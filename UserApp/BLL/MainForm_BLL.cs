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

        public MainForm_BLL(TransferProgress process)
        {
            fileManager = new FileManager(@"D:\FileClient");
            ftpClient = new FtpClient("127.0.0.1", 1234, TransferProgressHandler);
            progress += process;
        }

        public List<FileInfor> GetFileInfos()
        {
            return ftpClient.ListRemoteFolderAndFiles(); ;
        }

        public void Download(string fileName)
        {
            string remoteFolder = ftpClient.Pwd();
            ftpClient.Download($"{((remoteFolder == "\\") ? $"{remoteFolder}" : $"{remoteFolder}\\")}{fileName}", fileManager.GetCurrentPath());
        }

        public void DownloadFolder(string folderName)
        {
            string remoteFolder = ftpClient.Pwd();
            ftpClient.DownloadFolder($"{((remoteFolder == "\\") ? $"{remoteFolder}" : $"{remoteFolder}\\")}{folderName}", $"{fileManager.GetCurrentPath()}\\{folderName}");
            ftpClient.SetRemoteFolderPath(remoteFolder);
        }
        public void ChangeFolder(string folderName)
        {
            string remoteFolder = ftpClient.Pwd();
            ftpClient.SetRemoteFolderPath((remoteFolder == "\\") ? $"\\{folderName}" : $"{remoteFolder}\\{folderName}");
        }
        public void Back()
        {
            string remoteFolder = ftpClient.Pwd();
            if (remoteFolder != "\\")
            {
                ftpClient.SetRemoteFolderPath(remoteFolder.Substring(0, remoteFolder.LastIndexOf('\\')));
            }
        }

        public delegate void TransferProgress(FileTransferProcessing sender);
        public event TransferProgress progress;

        private void TransferProgressHandler(FileTransferProcessing sender)
        {
            if (progress != null)
                progress(sender);
        }

        public void Dispose()
        {
            ftpClient.Dispose();
        }

        internal void Upload(string filePath)
        {
            ftpClient.Upload(ftpClient.Pwd(), filePath);
        }

        internal void UploadFolder(string selectedPath)
        {
            string remoteFolder = ftpClient.Pwd();
            string folderName = selectedPath.Substring(selectedPath.LastIndexOf('\\') + 1);
            ftpClient.UploadFolder((ftpClient.Pwd() == "\\") ? $"\\{folderName}" : $"{remoteFolder}\\{folderName}", selectedPath);
            ftpClient.SetRemoteFolderPath(remoteFolder);
        }
    }
}
