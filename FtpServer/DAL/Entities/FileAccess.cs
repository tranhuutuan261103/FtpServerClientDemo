using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFtpServer.DAL.Entities
{
    [Table("FileAccesses")]
    public class FileAccess
    {
        public string IdFile { get; set; } = String.Empty;
        public int IdAccess { get; set; }
        public int IdAccount { get; set; }
        public File File { get; set; } = null!;
        public AccessStatus Role { get; set; } = null!;
        public Account Account { get; set; } = null!;
    }
}
