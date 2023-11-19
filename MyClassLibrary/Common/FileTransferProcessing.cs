using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary.Common
{
    public class FileTransferProcessing
    {
        public int FileTransferedPercent { get; set; }
        public string FileName { get; set; } = "";
        public string Type { get; set; } = "";
        public string RemotePath { get; set; } = "";
        public string LocalPath { get; set; } = "";
        public FileTransferProcessingStatus Status { get; set; }
        public long FileSize { get; set; }
        public long FileTransfered { get; set; }

        public FileTransferProcessing()
        {
            FileTransferedPercent = 0;
            FileName = "";
            Status = FileTransferProcessingStatus.Downloading;
            FileSize = 0;
            FileTransfered = 0;
        }

        public FileTransferProcessing(string type, string remotePath, string fileName , string localPath)
        {
            Type = type;
            LocalPath = localPath;
            RemotePath = remotePath;
            FileName = fileName;
        }

    }

    public enum FileTransferProcessingStatus
    {
        Downloading,
        Uploading,
        Completed,
        Failed
    }
}
