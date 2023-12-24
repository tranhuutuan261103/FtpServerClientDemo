using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary.Bean.File
{
    public class UploadFolderRequest
    {
        public string ParentFolderId { get; set; } = "";
        public string FolderName { get; set; } = "";
        public string FullLocalPath { get; set; } = "";
    }
}
