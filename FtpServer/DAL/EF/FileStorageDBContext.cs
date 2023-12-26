using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFtpServer.DAL.Entities;
using File = MyFtpServer.DAL.Entities.File;
using MyFtpServer.DAL.InitData;

namespace MyFtpServer.DAL.EF
{
    public class FileStorageDBContext : DbContext
    {
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Folder> Folders { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<AccessStatus> AccessStatuses { get; set; }
        public virtual DbSet<Entities.FileAccess> FileAccesses { get; set; }
        public virtual DbSet<FolderAccess> FolderAccesses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@$"Server=MSI\SQLEXPRESS;Database=FileStorageDB;TrustServerCertificate=true;User Id=sa;Password=tuan261103");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Id).UseIdentityColumn(10000, 1);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(32);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(32);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(32);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(32);
                entity.Property(e => e.CreateDate).IsRequired().HasDefaultValue(DateTime.Now);
                entity.Property(e => e.Avatar).HasMaxLength(256);
                entity.Property(e => e.IsDeleted).IsRequired();
            });

            modelBuilder.Entity<Folder>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasMaxLength(36);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(64);
                entity.Property(e => e.IdParent).HasMaxLength(36);
                entity.Property(e => e.IsDeleted).IsRequired();

                entity.HasOne(e => e.ParentFolder)
                    .WithMany(e => e.SubFolders)
                    .HasForeignKey(e => e.IdParent)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasMaxLength(36);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(64);
                entity.Property(e => e.FilePath).IsRequired().HasMaxLength(128);
                entity.Property(e => e.IdParent).HasMaxLength(36);
                entity.Property(e => e.IsDeleted).IsRequired();

                entity.HasOne(e => e.ParentFolder)
                    .WithMany(e => e.Files)
                    .HasForeignKey(e => e.IdParent)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<AccessStatus>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(32);
                entity.Property(e => e.Description).HasMaxLength(256);
            });

            modelBuilder.Entity<Entities.FileAccess>(entity =>
            {
                entity.HasKey(e => new { e.IdFile, e.IdAccess, e.IdAccount });
                entity.Property(e => e.IdFile).IsRequired().HasMaxLength(36);
                entity.Property(e => e.IdAccess).IsRequired();
                entity.Property(e => e.IdAccount).IsRequired();
                entity.HasOne(e => e.File)
                    .WithMany(e => e.FileAccesses)
                    .HasForeignKey(e => e.IdFile)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Role)
                    .WithMany(e => e.FileAccesses)
                    .HasForeignKey(e => e.IdAccess)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Account)
                    .WithMany(e => e.FileAccesses)
                    .HasForeignKey(e => e.IdAccount)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<FolderAccess>(entity =>
            {
                entity.HasKey(e => new { e.IdFolder, e.IdAccess, e.IdAccount });
                entity.Property(e => e.IdFolder).IsRequired().HasMaxLength(36);
                entity.Property(e => e.IdAccess).IsRequired();
                entity.Property(e => e.IdAccount).IsRequired();
                entity.HasOne(e => e.Folder)
                    .WithMany(e => e.FolderAccesses)
                    .HasForeignKey(e => e.IdFolder)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Role)
                    .WithMany(e => e.FolderAccesses)
                    .HasForeignKey(e => e.IdAccess)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Account)
                    .WithMany(e => e.FolderAccesses)
                    .HasForeignKey(e => e.IdAccount)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Seed();
        }

        public override int SaveChanges()
        {
            var fileEntities = ChangeTracker.Entries()
                .Where(x => x.Entity is File && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entityEntry in fileEntities)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((File)entityEntry.Entity).CreationDate = DateTime.Now;
                }
            }

            var folderEntities = ChangeTracker.Entries()
                .Where(x => x.Entity is Folder && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entityEntry in folderEntities)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((Folder)entityEntry.Entity).CreationDate = DateTime.Now;
                }
            }

            var accountEntities = ChangeTracker.Entries()
                .Where(x => x.Entity is Account && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entityEntry in accountEntities)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((Account)entityEntry.Entity).CreateDate = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }
    }
}
