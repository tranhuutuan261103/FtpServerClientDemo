using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary.Bean.Account
{
    public class AccountInfoVM
    {
        public int Id { get; set; } = 0;
        public string Email { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public long UsedStorage { get; set; } = 0;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public List<byte> Avatar { get; set; } = new List<byte>();
        public bool IsDeleted { get; set; } = false;
    }
}
