using MyClassLibrary.Bean;
using MyFtpServer.DAL.EF;
using MyFtpServer.DAL.Entities;
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

        public bool Register(RegisterRequest request)
        {
            using (var db = new FileStorageDBContext())
            {
                var account = db.Accounts.FirstOrDefault(a => a.Username == request.Username);
                if (account != null)
                {
                    return false;
                }
                account = new Account()
                {
                    Username = request.Username,
                    Password = request.Password,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    CreateDate = DateTime.Now,
                };
                db.Accounts.Add(account);
                db.SaveChanges();
                return true;
            }
        }

        public bool ResetPassword(ResetPasswordRequest request)
        {
            using (var db = new FileStorageDBContext())
            {
                var account = db.Accounts.FirstOrDefault(a => a.Username == request.Email);
                if (account == null)
                {
                    return false;
                }
                account.Password = request.NewPassword;
                db.SaveChanges();
                return true;
            }
        }
    }
}
