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

        public MainForm_BLL()
        {
            fileManager = new FileManager(@"D:\FileClient");
            ftpClient = new FtpClient("127.0.0.1", 1234);
            ftpClient.FtpClientEvent += new FtpClient.FtpClientEventHandler(ftpClient_FtpClientEvent);
            //ftpClient.Login("admin", "admin");
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

        public delegate void ProcessTransfer(FileTransferProcessing sender);
        public event ProcessTransfer processTransfer;

        private void ftpClient_FtpClientEvent(FileTransferProcessing sender)
        {
            if (processTransfer != null)
                processTransfer(sender);
        }

        public void Dispose()
        {
            ftpClient.Dispose();
            processTransfer -= ftpClient_FtpClientEvent;
        }
    }
}
