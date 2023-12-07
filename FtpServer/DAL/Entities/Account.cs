using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFtpServer.DAL.Entities
{
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
        public ICollection<Folder> Folders { get; set; } = new List<Folder>();
        public ICollection<File> Files { get; set; } = new List<File>();
    }
}
