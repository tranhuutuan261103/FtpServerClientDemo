using MyClassLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.DTO
{
    public class FileControlRequest
    {
        public FileInfor fileInfor { get; set; } = new FileInfor();
        public FileControlRequestType type { get; set; }
    }

    public enum FileControlRequestType
    {
        ChangeFolder,
        ChangeSharedFolder,
        Download,
        Rename,
        Information,
        Remove
    }
}
