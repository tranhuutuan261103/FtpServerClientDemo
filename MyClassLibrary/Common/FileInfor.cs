using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary.Common
{
    public class FileInfor
    {
        public string Name { get; set; } = "";
        public DateTime LastWriteTime { get; set; }
        /**
         * If the current FileSystemInfo represents a file, this property returns the size of the file in bytes.
         */
        public long Length { get; set; }
        public bool IsDirectory { get; set; }
    }
}
