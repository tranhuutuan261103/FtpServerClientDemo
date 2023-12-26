using Microsoft.EntityFrameworkCore;
using MyFtpServer.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFtpServer.DAL.InitData
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    Id = 10000,
                    Email = "tuan",
                    Password = "tuan",
                    FirstName = "Admin",
                    LastName = "Admin",
                    CreateDate = DateTime.Now,
                    Avatar = null,
                    IsDeleted = false
                },
                new Account
                {
                    Id = 10001,
                    Email = "user",
                    Password = "user",
                    FirstName = "User",
                    LastName = "User",
                    CreateDate = DateTime.Now,
                    Avatar = null,
                    IsDeleted = false
                }
            );

            modelBuilder.Entity<AccessStatus>().HasData(
                new AccessStatus
                {
                    Id = 1,
                    Name = "Owner",
                    Description = "The person who create folder or file"
                },
                new AccessStatus
                {
                    Id = 2,
                    Name = "Shared",
                    Description = "The person who can view or edit folder or file"
                },
                new AccessStatus
                {
                    Id = 3,
                    Name = "Viewer",
                    Description = "The person who can view folder or file"
                }
            );
        }
    }
}
