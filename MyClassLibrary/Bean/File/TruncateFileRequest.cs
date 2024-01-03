using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary.Bean.File
{
    public class TruncateFileRequest
    {
        public string Id { get; set; } = "";
        public TruncateFileRequestType RequestType { get; set; }
    }

    public enum TruncateFileRequestType
    {
        Folder,
        File
    }
}
