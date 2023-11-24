using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary.Common
{
    public class TaskSession
    {
        public string Type { get; set; } = "";
        public string FileName { get; set; } = "";
        public string RemotePath { get; set; } = "";
        public string LocalPath { get; set; } = "";

        public TaskSession(string type, string remotePath, string fileName, string localPath)
        {
            Type = type;
            FileName = fileName;
            RemotePath = remotePath;
            LocalPath = localPath;
        }
    }
}
