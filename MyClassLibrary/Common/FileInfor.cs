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

        public FileInfor()
        {
        }

        public FileInfor(string name, DateTime lastWriteTime, long length, bool isDirectory)
        {
            Name = name;
            LastWriteTime = lastWriteTime;
            Length = length;
            IsDirectory = isDirectory;
        }

        public override string ToString()
        {
            if (IsDirectory == true)
            {
                return $"{LastWriteTime}\t{"<DIR>", 24}\t{Name}";
            }

            return $"{LastWriteTime}\t{Length, 24}\t{Name}";
        }
    }
}
