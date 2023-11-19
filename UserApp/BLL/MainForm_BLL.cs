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
        private FileManager fileManager;
        FtpClient ftpClient;

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

        public delegate void ProcessTransfer(object sender);
        public event ProcessTransfer processTransfer;

        private void ftpClient_FtpClientEvent(object sender)
        {
            processTransfer(sender);
        }
    }
}
