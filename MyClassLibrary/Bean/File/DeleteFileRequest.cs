using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary.Bean.File
{
    public class DeleteFileRequest
    {
        public string Id { get; set; } = "";
        public DeleteFileRequestType RequestType { get; set; }
    }

    public enum DeleteFileRequestType
    {
        Folder,
        File
    }
}
