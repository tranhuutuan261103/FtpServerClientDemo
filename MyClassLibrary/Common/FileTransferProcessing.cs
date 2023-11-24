using Newtonsoft.Json.Linq;
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
        public long FileSizeTransfered { get; set; }

        public FileTransferProcessing()
        {
            FileTransferedPercent = 0;
            FileName = "";
            Status = FileTransferProcessingStatus.Downloading;
            FileSize = 0;
            FileSizeTransfered = 0;
        }

        public FileTransferProcessing(string type, string remotePath, string fileName , string localPath)
        {
            Type = type;
            LocalPath = localPath;
            RemotePath = remotePath;
            FileName = fileName;
            FileSize = 0;
            FileSizeTransfered = 0;
            FileTransferedPercent = 0;
            Status = FileTransferProcessingStatus.Waiting;
        }

        public void SetFileTransferSize(long fileSizeTransfered)
        {
            FileSizeTransfered = fileSizeTransfered;
            if (FileSize > 0)
            {
                FileTransferedPercent = (int)(FileSizeTransfered * 100 / FileSize);
            }
        }

        public string StatusToString()
        {
            switch (Status)
            {
                case FileTransferProcessingStatus.Downloading:
                    return "Downloading";
                case FileTransferProcessingStatus.Uploading:
                    return "Uploading";
                case FileTransferProcessingStatus.Completed:
                    return "Completed";
                case FileTransferProcessingStatus.Failed:
                    return "Failed";
                case FileTransferProcessingStatus.Waiting:
                    return "Waiting";
                default:
                    return "Unknown";
            }
        }
    }

    public enum FileTransferProcessingStatus
    {
        Downloading,
        Uploading,
        Completed,
        Failed,
        Waiting
    }
}
