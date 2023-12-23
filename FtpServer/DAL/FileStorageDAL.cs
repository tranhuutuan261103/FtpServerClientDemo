using Microsoft.IdentityModel.Tokens;
using MyClassLibrary.Bean.File;
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

        public FileDetailVM? GetDetailFile(int idAccount, string idFile, string rootPath)
        {
            using(var db = new FileStorageDBContext())
            {
                var file = db.Files.FirstOrDefault(f => f.Id == idFile);
                if(file != null)
                {
                    var fileDetail = new FileDetailVM()
                    {
                        Id = file.Id,
                        Name = file.Name,
                        CreatedTime = file.CreationDate,
                        Type = FileInforType.File,
                        Length = new FileInfo(rootPath + file.FilePath).Length,
                        FileOwner = "none",
                    };
                    var fileAccess = db.FileAccesses.FirstOrDefault(fa => fa.IdFile == idFile && fa.IdAccess == 1);
                    
                    if(fileAccess != null)
                    {
                        var owner = db.Accounts.FirstOrDefault(a => a.Id == fileAccess.IdAccount);
                        if (owner != null)
                        {
                            fileDetail.FileOwner = owner.Username;
                        }
                    }
                    return fileDetail;
                } else
                {
                    var folder = db.Folders.FirstOrDefault(f => f.Id == idFile);
                    if(folder != null)
                    {
                        var fileDetail = new FileDetailVM()
                        {
                            Id = folder.Id,
                            Name = folder.Name,
                            CreatedTime = folder.CreationDate,
                            Type = FileInforType.Folder,
                            Length = 0,
                            FileOwner = "none",
                        };
                        var folderAccess = db.FolderAccesses.FirstOrDefault(fa => fa.IdFolder == idFile && fa.IdAccess == 1);
                        if(folderAccess != null)
                        {
                            var owner = db.Accounts.FirstOrDefault(a => a.Id == folderAccess.IdAccount);
                            if(owner != null)
                            {
                                fileDetail.FileOwner = owner.Username;
                            }
                        }
                        return fileDetail;
                    }
                }
                return null;
            }
        }

        public long GetFileSize(string id, string rootPath)
        {
            using(var db = new FileStorageDBContext())
            {
                var file = db.Files.FirstOrDefault(f => f.Id == id);
                if(file != null)
                {
                    string filePath = rootPath + file.FilePath;
                    return new FileInfo(filePath).Length;
                }
                return 0;
            }
        }

        public long GetUsedStorage(int idAccount, string rootPath)
        {
            using(var db = new FileStorageDBContext())
            {
                var files = db.Files.Where(f => f.IsDeleted == false
                               && db.FileAccesses.Any(fa => fa.IdAccount == idAccount && fa.IdFile == f.Id)).ToList();
                long size = 0;
                foreach(var file in files)
                {
                    size += GetFileSize(file.Id, rootPath);
                }
                return size;
            }
        }

        public string RenameFolder(int idAccount, string remoteFolderPath, string newName)
        {
            using(var db = new FileStorageDBContext())
            {
                var folder = db.Folders.FirstOrDefault(f => f.Id == remoteFolderPath);
                if(folder != null)
                {
                    folder.Name = newName;
                    if(db.SaveChanges() == 0)
                    {
                        return "";
                    }
                    return folder.Id;
                } else
                {
                    var file = db.Files.FirstOrDefault(f => f.Id == remoteFolderPath);
                    if(file != null)
                    {
                        file.Name = newName;
                        if(db.SaveChanges() == 0)
                        {
                            return "";
                        }
                        return file.Id;
                    }
                }
                return "";
            }
        }

        public string DeleteFile(int idAccount, string fileId)
        {
            using (var db = new FileStorageDBContext())
            {
                var file = db.Files.FirstOrDefault(f => f.Id == fileId);
                if (file != null)
                {
                    file.IsDeleted = true;
                    if (db.SaveChanges() == 0)
                    {
                        return "";
                    }
                    return file.Id;
                }
                return "";
            }
        }

        public string DeleteFolder(int idAccount, string folderId)
        {
            using (var db = new FileStorageDBContext())
            {
                var folder = db.Folders.FirstOrDefault(f => f.Id == folderId);
                if (folder != null)
                {
                    folder.IsDeleted = true;
                    if (db.SaveChanges() == 0)
                    {
                        return "";
                    }
                    return folder.Id;
                }
                return "";
            }
        }
    }
}
