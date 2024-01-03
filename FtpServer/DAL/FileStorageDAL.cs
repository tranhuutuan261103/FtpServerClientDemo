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

        // Đệ quy truy vấn để tìm quyền truy cập của thư mục cha
        private int GetParentAccess(int idAccount, string idParent)
        {
            using(var db = new FileStorageDBContext())
            {
                var folderAccess = db.FolderAccesses
                    .Where(fa => fa.IdAccount == idAccount && fa.IdFolder == idParent).FirstOrDefault();
                if(folderAccess != null)
                {
                    return folderAccess.IdAccess;
                }
                var folder = db.Folders.FirstOrDefault(f => f.Id == idParent);
                if(folder != null)
                {
                    if (folder.IdParent == null)
                    {
                        return 0;
                    }
                    return GetParentAccess(idAccount, folder.IdParent);
                }
                return 0;
            }
        }

        public FileInforPackage GetFileInfors(int idAccount, string idParent)
        {
            using(var db = new FileStorageDBContext())
            {
                var files = db.Files
                .Where(f => f.IdParent == (idParent.IsNullOrEmpty() ? null : idParent) && f.IsDeleted == false
                && db.FileAccesses
                .Any(fa => fa.IdAccount == idAccount && fa.IdFile == f.Id && fa.IdAccess == 1)
                ).ToList();
                var fileInfors = new List<FileInfor>();
                foreach (var file in files)
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
                    .Any(fa => fa.IdAccount == idAccount && fa.IdFolder == f.Id && fa.IdAccess == 1)).ToList();
                foreach (var folder in folders)
                {
                    var fileInfor = new FileInfor()
                    {
                        Id = folder.Id,
                        Name = folder.Name,
                        IsDirectory = true,
                    };
                    fileInfors.Add(fileInfor);
                }
                var folderParent = db.Folders.FirstOrDefault(f => f.Id == idParent);
                return new FileInforPackage()
                {
                    Category = Category.Owner,
                    IdFolder = idParent,
                    NameFolder = folderParent == null ? "" : folderParent.Name,
                    fileInfors = fileInfors
                };
            }
        }

        public FileInforPackage GetSharedFilePackage(int idAccount, string idParent)
        {
            if (idParent == null)
            {
                idParent = "";
            }
            using(var db = new FileStorageDBContext())
            {
                if (idParent == "")
                {
                    var files = db.Files
                    .Where(f => f.IsDeleted == false
                    && db.FileAccesses
                    .Any(fa => fa.IdAccount == idAccount && fa.IdFile == f.Id && (fa.IdAccess == 2 || fa.IdAccess == 3 ))).ToList();
                    var fileInfors = new List<FileInfor>();
                    foreach (var file in files)
                    {
                        var fileInfor = new FileInfor()
                        {
                            Id = file.Id,
                            Name = file.Name,
                            IsDirectory = false,
                        };
                        fileInfors.Add(fileInfor);
                    }
                    var folders = db.Folders
                        .Where(f => f.IsDeleted == false
                        && db.FolderAccesses
                        .Any(fa => fa.IdAccount == idAccount && fa.IdFolder == f.Id && (fa.IdAccess == 2 || fa.IdAccess == 3))).ToList();
                    foreach (var folder in folders)
                    {
                        var fileInfor = new FileInfor()
                        {
                            Id = folder.Id,
                            Name = folder.Name,
                            IsDirectory = true,
                        };
                        fileInfors.Add(fileInfor);
                    }

                    return new FileInforPackage()
                    {
                        Category = Category.Shared,
                        fileInfors = fileInfors
                    };
                } else
                {
                    var files = db.Files
                    .Where(f => f.IdParent == idParent && f.IsDeleted == false).ToList();
                    var fileInfors = new List<FileInfor>();
                    foreach (var file in files)
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
                        .Where(f => f.IdParent == idParent && f.IsDeleted == false).ToList();
                    foreach (var folder in folders)
                    {
                        var fileInfor = new FileInfor()
                        {
                            Id = folder.Id,
                            Name = folder.Name,
                            IsDirectory = true,
                        };
                        fileInfors.Add(fileInfor);
                    }
                    var folderParent = db.Folders.FirstOrDefault(f => f.Id == idParent);
                    return new FileInforPackage()
                    {
                        Category = Category.Shared,
                        fileInfors = fileInfors,
                        IdFolder = idParent,
                        NameFolder = folderParent == null ? "" : folderParent.Name
                    };
                }
            }
        }

        public FileInforPackage GetCanDownloadFilePackage(int idAccount, string idParent)
        {
            int accessAbility = GetParentAccess(idAccount, idParent);
            if (accessAbility == 0)
            {
                return new FileInforPackage();
            }
            using (var db = new FileStorageDBContext())
            {
                var files = db.Files
                    .Where(f => f.IdParent == idParent && f.IsDeleted == false
                    && db.FileAccesses
                    .Any(fa => fa.IdAccount == idAccount && fa.IdFile == f.Id && fa.IdAccess == 2)).ToList();
                var fileInfors = new List<FileInfor>();
                foreach (var file in files)
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
                    .Where(f => f.IdParent == idParent && f.IsDeleted == false
                    && db.FolderAccesses
                    .Any(fa => fa.IdAccount == idAccount && fa.IdFolder == f.Id && fa.IdAccess == 2)).ToList();
                foreach (var folder in folders)
                {
                    var fileInfor = new FileInfor()
                    {
                        Id = folder.Id,
                        Name = folder.Name,
                        IsDirectory = true,
                    };
                    fileInfors.Add(fileInfor);
                }
                return new FileInforPackage()
                {
                    Category = Category.CanDownload,
                    fileInfors = fileInfors
                };
            }
        }

        public FileInforPackage GetDeletedFilePackage(int idAccount)
        {
            using(var db = new FileStorageDBContext())
            {
                var files = db.Files
                    .Where(f => f.IsDeleted == true && 
                    db.FileAccesses
                    .Any(fa => fa.IdAccount == idAccount && 
                    f.Id == fa.IdFile && 
                    fa.IdAccess == 1)).ToList();
                var fileInfors = new List<FileInfor>();
                foreach (var file in files)
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
                    .Where(f => f.IsDeleted == true &&
                    db.FolderAccesses
                    .Any(fa => fa.IdAccount == idAccount &&
                    f.Id == fa.IdFolder &&
                                                                                                fa.IdAccess == 1)).ToList();
                foreach (var folder in folders)
                {
                    var fileInfor = new FileInfor()
                    {
                        Id = folder.Id,
                        Name = folder.Name,
                        IsDirectory = true,
                    };
                    fileInfors.Add(fileInfor);
                }
                return new FileInforPackage()
                {
                    Category = Category.Deleted,
                    fileInfors = fileInfors
                };
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

        public void TruncateFile(int idAccount, string idFile, string _rootPath)
        {
            using(var db = new FileStorageDBContext())
            {
                var fileAccess = db.FileAccesses.FirstOrDefault(fa => fa.IdAccount == idAccount 
                && fa.IdFile == idFile 
                && (fa.IdAccess == 1 || fa.IdAccess == 2) );

                int effect = 0;
                if (fileAccess != null)
                {
                    db.RemoveRange(db.FileAccesses.Where(fa => fa.IdFile == idFile));
                    effect = db.SaveChanges();
                }

                if (effect > 0)
                {
                    var file = db.Files.FirstOrDefault(f => f.Id == idFile);
                    if (file != null)
                    {
                        db.Remove(file);
                        if (db.SaveChanges() != 0)
                        {
                            try
                            {
                                System.IO.File.Delete(_rootPath + file.FilePath);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                    }
                }
            }
        }

        public File? CreateNewFile(int idAccount, string idParent, string name)
        {
            int realIdAccount = idAccount;
            if (idParent != null && idParent != "")
            {
                realIdAccount = GetRealIdAccount(idParent);
            }
            using(var db = new FileStorageDBContext())
            {
                string id = Guid.NewGuid().ToString();
                string filePath = $"/{realIdAccount}/" + id + name.Substring(name.LastIndexOf("."));
                var file = new File()
                {
                    Id = id,
                    IdParent = idParent == "" ? null : idParent,
                    Name = name,
                    FilePath = filePath,
                    CreationDate = DateTime.Now,
                };
                db.Files.Add(file);
                if( db.SaveChanges() == 0)
                {
                    return null;
                }
                var fileAccess = new FileAccess()
                {
                    IdFile = id,
                    IdAccount = realIdAccount,
                    IdAccess = 1,
                };
                db.FileAccesses.Add(fileAccess);
                if(db.SaveChanges() == 0)
                {
                    return null;
                }
                return new File()
                {
                    Id = id,
                    IdParent = idParent,
                    Name = name,
                    FilePath = filePath,
                    CreationDate = DateTime.Now,
                };
            }
        }

        private int GetRealIdAccount(string idParent)
        {
            using(var db = new FileStorageDBContext())
            {
                var folderAccess = db.FolderAccesses.FirstOrDefault(fa => fa.IdFolder == idParent && fa.IdAccess == 1);
                if(folderAccess != null)
                {
                    return folderAccess.IdAccount;
                }
                return -1;
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
            int realIdAccount = idAccount;
            if(idParent != null && idParent != "")
            {
                realIdAccount = GetRealIdAccount(idParent);
            }
            if (realIdAccount == -1)
            {
                return "";
            }
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
                    IdAccount = realIdAccount,
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
                        FileOwner = "none",
                    };
                    try
                    {
                        fileDetail.Length = new FileInfo(rootPath + file.FilePath).Length;
                    } catch(Exception)
                    {
                        fileDetail.Length = 0;
                    }
                    var fileAccess = db.FileAccesses.FirstOrDefault(fa => fa.IdFile == idFile && fa.IdAccess == 1);
                    
                    if(fileAccess != null)
                    {
                        var owner = db.Accounts.FirstOrDefault(a => a.Id == fileAccess.IdAccount);
                        if (owner != null)
                        {
                            fileDetail.FileOwner = owner.Email;
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
                                fileDetail.FileOwner = owner.Email;
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
                    try
                    {
                        return new FileInfo(filePath).Length;
                    }
                    catch (Exception e)
                    {
                        return 0;
                    }
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

        public bool RestoreFile(int idAccount, string idFile)
        {
            using(var db = new FileStorageDBContext())
            {
                var file = db.Files.FirstOrDefault(f => f.Id == idFile);
                if(file != null)
                {
                    file.IsDeleted = false;
                    if(db.SaveChanges() != 0)
                    {
                        return true;
                    }
                } else
                {
                    var folder = db.Folders.FirstOrDefault(f => f.Id == idFile);
                    if(folder != null)
                    {
                        folder.IsDeleted = false;
                        if(db.SaveChanges() != 0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public List<FileAccessVM>? GetListFileAccess(int idAccount, string idFile, string rootPath)
        {
            using(var db = new FileStorageDBContext())
            {
                var file = db.Files.FirstOrDefault(f => f.Id == idFile);
                if(file != null)
                {
                    var fileAccesses = db.FileAccesses.Where(fa => fa.IdFile == idFile).ToList();
                    var fileAccessVMs = new List<FileAccessVM>();
                    foreach(var fileAccess in fileAccesses)
                    {
                        var account = db.Accounts.FirstOrDefault(a => a.Id == fileAccess.IdAccount);
                        if(account != null)
                        {
                            FileAccessVM fileAccessVM = new FileAccessVM()
                            {
                                IdAccount = account.Id,
                                Email = account.Email,
                                IdAccess = fileAccess.IdAccess,
                                FirstName = account.FirstName,
                                LastName = account.LastName,
                                IdFile = fileAccess.IdFile,
                            };
                            if (account.Avatar != null)
                            {
                                try
                                {
                                    fileAccessVM.Avatar = System.IO.File.ReadAllBytes(rootPath + account.Avatar).ToList();
                                } catch(Exception)
                                {
                                }
                            }
                            fileAccessVMs.Add(fileAccessVM);
                        }
                    }
                    return fileAccessVMs;
                } else
                {
                    var folder = db.Folders.FirstOrDefault(f => f.Id == idFile);
                    if(folder != null)
                    {
                        var folderAccesses = db.FolderAccesses.Where(fa => fa.IdFolder == idFile).ToList();
                        var fileAccessVMs = new List<FileAccessVM>();
                        foreach (var folderAccess in folderAccesses)
                        {
                            var account = db.Accounts.FirstOrDefault(a => a.Id == folderAccess.IdAccount);
                            if (account != null)
                            {
                                FileAccessVM fileAccessVM = new FileAccessVM()
                                {
                                    IdAccount = account.Id,
                                    Email = account.Email,
                                    IdAccess = folderAccess.IdAccess,
                                    FirstName = account.FirstName,
                                    LastName = account.LastName,
                                    IdFile = folderAccess.IdFolder,
                                };
                                if (account.Avatar != null)
                                {
                                    try
                                    {
                                        fileAccessVM.Avatar = System.IO.File.ReadAllBytes(rootPath + account.Avatar).ToList();
                                    }
                                    catch (Exception)
                                    {
                                    }
                                }
                                fileAccessVMs.Add(fileAccessVM);
                            }
                        }
                        return fileAccessVMs;
                    }
                }
                return null;
            }
        }

        public bool UpdateFileAccess(int idAccount, List<FileAccessVM>? list)
        {
            using(var db = new FileStorageDBContext())
            {
                try
                {
                    if (list != null)
                    {
                        foreach (var fileAccessVM in list)
                        {
                            var idAcc = db.Accounts.FirstOrDefault(a => a.Email == fileAccessVM.Email);
                            if (idAcc == null)
                            {
                                continue;
                            }
                            var file = db.Files.FirstOrDefault(f => f.Id == fileAccessVM.IdFile);
                            if (file != null)
                            {
                                var fileAccess = db.FileAccesses.FirstOrDefault(fa => fa.IdAccount == idAcc.Id && fa.IdFile == fileAccessVM.IdFile);
                                if (fileAccess != null)
                                {
                                    db.FileAccesses.Remove(fileAccess);
                                    if (fileAccessVM.IdAccess != 4)
                                    {
                                        fileAccess = new FileAccess()
                                        {
                                            IdAccount = idAcc.Id,
                                            IdFile = fileAccessVM.IdFile,
                                            IdAccess = fileAccessVM.IdAccess,
                                        };
                                        db.FileAccesses.Add(fileAccess);
                                    }
                                }
                                else
                                {
                                    if (fileAccessVM.IdAccess != 4)
                                    {
                                        fileAccess = new FileAccess()
                                        {
                                            IdAccount = idAcc.Id,
                                            IdFile = fileAccessVM.IdFile,
                                            IdAccess = fileAccessVM.IdAccess,
                                        };
                                        db.FileAccesses.Add(fileAccess);
                                    }
                                }
                            } else
                            {
                                var folder = db.Folders.FirstOrDefault(f => f.Id == fileAccessVM.IdFile);
                                if(folder != null)
                                {
                                    var folderAccess = db.FolderAccesses.FirstOrDefault(fa => fa.IdAccount == idAcc.Id && fa.IdFolder == fileAccessVM.IdFile);
                                    if (folderAccess != null)
                                    {
                                        db.Remove(folderAccess);
                                        if (fileAccessVM.IdAccess != 4)
                                        {
                                            folderAccess = new FolderAccess()
                                            {
                                                IdAccount = idAcc.Id,
                                                IdFolder = fileAccessVM.IdFile,
                                                IdAccess = fileAccessVM.IdAccess,
                                            };
                                            db.FolderAccesses.Add(folderAccess);
                                        }
                                    }
                                    else
                                    {
                                        if (fileAccessVM.IdAccess != 4)
                                        {
                                            folderAccess = new FolderAccess()
                                            {
                                                IdAccount = idAcc.Id,
                                                IdFolder = fileAccessVM.IdFile,
                                                IdAccess = fileAccessVM.IdAccess,
                                            };
                                            db.FolderAccesses.Add(folderAccess);
                                        }
                                    }
                                }
                            }
                        }
                        if (db.SaveChanges() == 0)
                        {
                            return false;
                        }
                        return true;
                    }
                    return false;
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }

        public bool CheckCanUpload(int idAccount, string remoteFolderPath)
        {
            if (remoteFolderPath == null || remoteFolderPath == "")
            {
                return true;
            }
            int idAccess = GetParentAccess(idAccount, remoteFolderPath);
            if (idAccess == 1 || idAccess == 2)
            {
                return true;
            }
            return false;
        }
    }
}
