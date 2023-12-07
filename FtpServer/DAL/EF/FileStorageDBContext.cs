using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFtpServer.DAL.Entities;
using File = MyFtpServer.DAL.Entities.File;

namespace MyFtpServer.DAL.EF
{
    public class FileStorageDBContext : DbContext
    {
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Folder> Folders { get; set; }
        public virtual DbSet<File> Files { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@$"Server=MSI\SQLEXPRESS;Database=FileStorageDB;TrustServerCertificate=true;User Id=sa;Password=tuan261103");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).UseIdentityColumn(10000, 1);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(32);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(32);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(32);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(32);
                entity.Property(e => e.CreateDate).IsRequired().HasDefaultValue(DateTime.Now);
                entity.Property(e => e.IsDeleted).IsRequired();
                entity.Property(e => e.Avatar).HasMaxLength(256);
            });

            modelBuilder.Entity<Folder>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasMaxLength(36);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(64);
                entity.Property(e => e.IdUser).IsRequired();
                entity.Property(e => e.IdParent).HasMaxLength(36);
                entity.Property(e => e.CreationDate).IsRequired().HasDefaultValue(DateTime.Now);
                entity.Property(e => e.IsDeleted).IsRequired();

                entity.HasOne(e => e.User)
                    .WithMany(e => e.Folders)
                    .HasForeignKey(e => e.IdUser)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.ParentFolder)
                    .WithMany(e => e.SubFolders)
                    .HasForeignKey(e => e.IdParent)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(64);
                entity.Property(e => e.FilePath).IsRequired().HasMaxLength(128);
                entity.Property(e => e.IdUser).IsRequired();
                entity.Property(e => e.IdParent).HasMaxLength(36);
                entity.Property(e => e.CreationDate).IsRequired().HasDefaultValue(DateTime.Now);
                entity.Property(e => e.IsDeleted).IsRequired();

                entity.HasOne(e => e.User)
                    .WithMany(e => e.Files)
                    .HasForeignKey(e => e.IdUser)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.ParentFolder)
                    .WithMany(e => e.Files)
                    .HasForeignKey(e => e.IdParent)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // modelBuilder.Seed();
        }
    }
}
