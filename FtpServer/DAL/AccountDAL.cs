using MyFtpServer.DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFtpServer.DAL
{
    public class AccountDAL
    {
        public int Authenticate(string username, string password)
        {
            using (var db = new FileStorageDBContext())
            {
                var account = db.Accounts.FirstOrDefault(a => a.Username == username && a.Password == password);
                if (account != null)
                {
                    return account.Id;
                }
                return 0;
            }
        }
    }
}
