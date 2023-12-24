using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary.Common
{
    public class FileInforPackage
    {
        public Category Category { get; set; } = Category.None;
        public List<FileInfor> fileInfors { get; set; } = new List<FileInfor>();
    }

    public enum Category
    {
        Owner,
        Shared,
        None,
        CanDownload,
        Deleted
    }
}
