using MyClassLibrary.Bean;
using MyClassLibrary.Bean.Account;
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

        public AccountInfoVM GetAccount(int idAccount)
        {
            using (var db = new FileStorageDBContext())
            {
                var account = db.Accounts.FirstOrDefault(a => a.Id == idAccount);
                if (account == null)
                {
                    return null;
                }
                return new AccountInfoVM()
                {
                    Email = account.Username,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    UsedStorage = 0,
                    CreationDate = account.CreateDate,
                    Avatar = null
                };
            }
        }

        public List<byte> GetAvatar(int idAccount, string rootPath)
        {
            using(var db = new FileStorageDBContext())
            {
                var account = db.Accounts.FirstOrDefault(a => a.Id == idAccount);
                if(account == null)
                {
                    return new List<byte>();
                }
                string avatarPath = rootPath + account.Avatar;
                try
                {
                    return System.IO.File.ReadAllBytes(avatarPath).ToList();
                }
                catch(Exception)
                {
                    return new List<byte>();
                }
            }
        }

        public void UpdateAccount(int idAccount, AccountInfoVM account)
        {
            using(var db = new FileStorageDBContext())
            {
                var accountEntity = db.Accounts.FirstOrDefault(a => a.Id == idAccount);
                if(accountEntity == null)
                {
                    return;
                }
                accountEntity.FirstName = account.FirstName;
                accountEntity.LastName = account.LastName;
                db.SaveChanges();
            }
        }

        public bool UpdateAvatar(int idAccount, string avatarPath)
        {
            using(var db = new FileStorageDBContext())
            {
                var account = db.Accounts.FirstOrDefault(a => a.Id == idAccount);
                if(account == null)
                {
                    return false;
                }
                account.Avatar = avatarPath;
                db.SaveChanges();
                return true;
            }
        }
    }
}
