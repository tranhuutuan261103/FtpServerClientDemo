using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFtpServer.DAL.Entities
{
    [Table("Files")]
    public class File
    {
        public string Id { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public string FilePath { get; set; } = String.Empty;
        public string? IdParent { get; set; }
        public Folder? ParentFolder { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<FileAccess> FileAccesses { get; set; } = new List<FileAccess>();
    }
}
