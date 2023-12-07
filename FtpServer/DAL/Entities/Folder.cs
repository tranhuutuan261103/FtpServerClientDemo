using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFtpServer.DAL.Entities
{
    public class Folder
    {
        public string Id { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public int IdUser { get; set; }
        public Account User { get; set; } = null!;
        public string? IdParent { get; set; }
        public Folder? ParentFolder { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Folder> SubFolders { get; set; } = new List<Folder>();
        public ICollection<File> Files { get; set; } = new List<File>();
    }
}
