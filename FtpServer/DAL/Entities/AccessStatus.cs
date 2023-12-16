using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFtpServer.DAL.Entities
{
    [Table("AccessStatuses")]
    public class AccessStatus
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;

        public string Description { get; set; } = String.Empty;
        public ICollection<FileAccess> FileAccesses { get; set; } = new List<FileAccess>();
        public ICollection<FolderAccess> FolderAccesses { get; set; } = new List<FolderAccess>();

    }
}
