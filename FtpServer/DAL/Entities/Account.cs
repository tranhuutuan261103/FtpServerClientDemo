using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFtpServer.DAL.Entities
{
    [Table("Accounts")]
    public class Account
    {
        public int Id { get; set; }
        public string Username { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public DateTime CreateDate { get; set; }
        public string? Avatar { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<FileAccess> FileAccesses { get; set; } = new List<FileAccess>();
        public ICollection<FolderAccess> FolderAccesses = new List<FolderAccess>();
    }
}
