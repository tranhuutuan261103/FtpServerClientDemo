using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary.Bean.File
{
    public class GetListFileRequest
    {
        public string IdParent { get; set; } = "";
        public IdAccess IdAccess { get; set; } = IdAccess.None;
    }

    public enum IdAccess
    {
        None = 0,
        Owner = 1,
        Shared = 2,
        View = 3,
        CanDownload = 4,
        CanUpload = 5,
        Deleted = 6,
    }
}
