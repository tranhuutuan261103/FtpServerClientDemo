using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary.Bean.File
{
    public class FileAccessVM
    {
        public int IdAccount { get; set; } = 0;
        public string Email { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public List<byte> Avatar { get; set; } = new List<byte>();
        public int IdAccess { get; set; }
        public string IdFile { get; set; } = "";
    }
}
