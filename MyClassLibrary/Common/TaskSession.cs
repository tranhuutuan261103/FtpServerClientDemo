using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary.Common
{
    public class TaskSession
    {
        public string Type { get; set; } = "";
        public object Data { get; set; }

        public TaskSession(string type, object obj)
        {
            Type = type;
            Data = obj;
        }
    }
}
