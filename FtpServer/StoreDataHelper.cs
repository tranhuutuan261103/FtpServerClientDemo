using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFtpServer
{
    public class StoreDataHelper
    {
        public StoreDataHelper() { }
        public bool SaveDataToFilePath(List<byte> data, string filePath)
        {
            if (data == null || data.Count == 0)
            {
                return false;
            }
            try
            {
                File.WriteAllBytes(filePath, data.ToArray());
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
