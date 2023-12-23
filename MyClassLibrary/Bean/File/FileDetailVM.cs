using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary.Bean.File
{
    public class FileDetailVM
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public DateTime CreatedTime { get; set; }
        /**
         ** If the current FileSystemInfo represents a file, this property returns the size of the file in bytes.
         **/
        public long Length { get; set; }
        public FileInforType Type { get; set; }
        public string FileOwner { get; set; } = "";
        public AccessType AccessType { get; set; }
    }

    public enum FileInforType
    {
        File,
        Folder
    }

    public enum AccessType
    {
        Private,
        CanDownload,
        CanDownloadAndUpload
    }
}
