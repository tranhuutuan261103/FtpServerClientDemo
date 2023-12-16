using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFtpServer.DAL.Entities
{
    [Table("FolderAccesses")]
    public class FolderAccess
    {
        public string IdFolder { get; set; } = String.Empty;
        public int IdAccess { get; set; }
        public int IdAccount { get; set; }
        public Folder Folder { get; set; } = null!;
        public AccessStatus Role { get; set; } = null!;
        public Account Account { get; set; } = null!;
    }
}
