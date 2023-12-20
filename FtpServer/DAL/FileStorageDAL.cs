using Microsoft.IdentityModel.Tokens;
using MyClassLibrary.Common;
using MyFtpServer.DAL.EF;
using MyFtpServer.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = MyFtpServer.DAL.Entities.File;
using FileAccess = MyFtpServer.DAL.Entities.FileAccess;

namespace MyFtpServer.DAL
{
    public class FileStorageDAL
    {
        public FileStorageDAL() { }

        public List<FileInfor> GetFileInfors(int idAccount, string idParent)
        {
            using(var db = new FileStorageDBContext())
            {
                // Get all subfiles
                var files = db.Files
                    .Where(f => f.IdParent == (idParent.IsNullOrEmpty() ? null : idParent) && f.IsDeleted == false
                    && db.FileAccesses
                    .Any(fa => fa.IdAccount == idAccount && fa.IdFile == f.Id)
                    ).ToList();
                var fileInfors = new List<FileInfor>();
                foreach(var file in files)
                {
                    var fileInfor = new FileInfor()
                    {
                        Id = file.Id,
                        Name = file.Name,
                        IsDirectory = false,
                    };
                    fileInfors.Add(fileInfor);
                }

                // Get all subfolders
                var folders = db.Folders
                    .Where(f => f.IdParent == (idParent.IsNullOrEmpty() ? null : idParent) && f.IsDeleted == false
                    && db.FolderAccesses
                    .Any(fa => fa.IdAccount == idAccount && fa.IdFolder == f.Id) ).ToList();
                foreach(var folder in folders)
                {
                    var fileInfor = new FileInfor()
                    {
                        Id = folder.Id,
                        Name = folder.Name,
                        IsDirectory = true,
                    };
                    fileInfors.Add(fileInfor);
                }
                return fileInfors;
            }
        }

        public string GetIdParent(string id)
        {
            using(var db = new FileStorageDBContext())
            {
                var file = db.Files.FirstOrDefault(f => f.Id == id);
                if(file != null)
                {
                    return file.IdParent;
                }
                var folder = db.Folders.FirstOrDefault(f => f.Id == id);
                if(folder != null)
                {
                    return folder.IdParent;
                }
                return null;
            }
        }

        public string GetFilePath(string id)
        {
            using(var db = new FileStorageDBContext())
            {
                var file = db.Files.FirstOrDefault(f => f.Id == id);
                if(file != null)
                {
                    return file.FilePath;
                }
                return "";
            }
        }

        public string CreateNewFile(int idAccount, string idParent, string name)
        {
            using(var db = new FileStorageDBContext())
            {
                string id = Guid.NewGuid().ToString();
                string filePath = $"/{idAccount}/" + id + name.Substring(name.LastIndexOf("."));
                var file = new File()
                {
                    Id = id,
                    IdParent = idParent == "" ? null : idParent,
                    Name = name,
                    FilePath = filePath,
                    CreationDate = DateTime.Now,
                    Favorite = false,
                };
                db.Files.Add(file);
                if( db.SaveChanges() == 0)
                {
                    return "";
                }
                var fileAccess = new FileAccess()
                {
                    IdFile = id,
                    IdAccount = idAccount,
                    IdAccess = 1,
                };
                db.FileAccesses.Add(fileAccess);
                if(db.SaveChanges() == 0)
                {
                    return "";
                }
                return filePath;
            }
        }

        public bool IsDirectory(string folderPath)
        {
            if (folderPath == null)
            {
                return false;
            }
            if (folderPath == "")
            {
                return true;
            }
            using (var db = new FileStorageDBContext())
            {
                var folder = db.Folders.FirstOrDefault(f => f.Id == folderPath);
                if (folder != null)
                {
                    return true;
                }
                return false;
            }
        }

        public string CreateNewFolder(int idAccount, string idParent, string name)
        {
            using(var db = new FileStorageDBContext())
            {
                string id = Guid.NewGuid().ToString();
                var folder = new Folder()
                {
                    Id = id,
                    IdParent = idParent == "" ? null : idParent,
                    Name = name,
                    CreationDate = DateTime.Now,
                };
                db.Folders.Add(folder);
                if(db.SaveChanges() == 0)
                {
                    return "";
                }
                db.FolderAccesses.Add(new FolderAccess()
                {
                    IdAccount = idAccount,
                    IdFolder = id,
                    IdAccess = 1,
                });
                if(db.SaveChanges() == 0)
                {
                    return "";
                }
                return id;
            }
        }
    }
}
