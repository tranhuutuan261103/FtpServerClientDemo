using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFtpServer.DAL.Entities
{
    public class File
    {
        public string Id { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public string FilePath { get; set; } = String.Empty;
        public int IdUser { get; set; }
        public Account User { get; set; } = null!;
        public bool Favorite { get; set; }
        public string? IdParent { get; set; }
        public Folder? ParentFolder { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
