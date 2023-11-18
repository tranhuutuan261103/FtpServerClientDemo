using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class TaskSession
    {
        private string type;
        private string remotePath;
        private string localPath;

        public TaskSession(string type, string remotePath, string localPath)
        {
            this.type = type;
            this.remotePath = remotePath;
            this.localPath = localPath;
        }

        public string Type { get => type; set => type = value; }
        public string RemotePath { get => remotePath; set => remotePath = value; }
        public string LocalPath { get => localPath; set => localPath = value; }
    }
}
