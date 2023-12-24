using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary.Bean.File
{
    public class CreateFolderRequest
    {
        public string FolderName { get; set; } = "";
        public string ParentFolderId { get; set; } = "";
    }
}
