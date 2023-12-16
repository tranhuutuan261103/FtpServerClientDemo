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

namespace MyFtpServer.DAL
{
    public class FileStorage_DAL
    {
        public FileStorage_DAL() { }

        public List<FileInfor> GetFileInfors(string idParent)
        {
            using(var db = new FileStorageDBContext())
            {
                // Get all subfiles
                var files = idParent.IsNullOrEmpty() ? db.Files.Where(f => f.IdParent == null).ToList() : db.Files.Where(f => f.IdParent == idParent).ToList();
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
                var folders = idParent.IsNullOrEmpty() ? db.Folders.Where(f => f.IdParent == null).ToList() : db.Folders.Where(f => f.IdParent == idParent).ToList();
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

        public string CreateNewFile(string idParent, string name)
        {
            using(var db = new FileStorageDBContext())
            {
                string id = Guid.NewGuid().ToString();
                string filePath = "/10000/" + id + name.Substring(name.LastIndexOf("."));
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
                if( db.SaveChanges() > 0)
                {
                    return filePath;
                }
                return "";
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

        public string CreateNewFolder(string idParent, string name)
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
                if(db.SaveChanges() > 0)
                {
                    return id;
                }
                return "";
            }
        }
    }
}
