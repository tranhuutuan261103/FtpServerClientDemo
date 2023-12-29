using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary.Bean.File
{
    public class DownloadFolderRequest
    {
        public string RemoteFolderId { get; set; } = "";
        public string FullLocalPath { get; set; } = "";
    }
}
