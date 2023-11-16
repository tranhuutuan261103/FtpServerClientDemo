using MyClassLibrary.Common;
using System.IO;
using System.Text;

namespace MyClassLibrary
{
    public class FileManager
    {
        private string _folderPath;
        public FileManager(string folderPath)
        {
            _folderPath = folderPath;
        }

        public FileManager()
        {
            _folderPath = @"D:\";
        }

        public string GetCurrentPath()
        {
            return _folderPath;
        }

        public void Dir()
        {
            List<FileInfor> list = new List<FileInfor>();
            string[] directories = Directory.GetDirectories(_folderPath);
            foreach (var directory in directories)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(directory);
                list.Add(new FileInfor()
                {
                    Name = dirInfo.Name,
                    Length = 0,
                    LastWriteTime = dirInfo.LastWriteTime,
                    IsDirectory = true
                });
            }
            string[] files = Directory.GetFiles(_folderPath);

            foreach (var file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                list.Add(new FileInfor()
                {
                    Name = fileInfo.Name,
                    Length = fileInfo.Length,
                    LastWriteTime = fileInfo.LastWriteTime,
                    IsDirectory = false
                });
            }

            foreach (var fileInfor in list)
            {
                Console.WriteLine(fileInfor.ToString());
            }
            return;
        }

        public string ChangeDirectory(string request)
        {
            request = request.Trim();
            string[] parts = request.Split(' ');
            if (parts.Length < 2)
            {
                return _folderPath;
            }
            
            string folderName = request.Substring(parts[0].Length + 1).Trim();
            if (folderName == "..")
            {
                string[] partsOfFolderPath = _folderPath.Split('\\');
                if (partsOfFolderPath.Length > 1)
                {
                    _folderPath = string.Join('\\', partsOfFolderPath.Take(partsOfFolderPath.Length - 1));
                }
                return _folderPath;
            }
            string folderPath = @$"{_folderPath}\{folderName}";
            if (Directory.Exists(folderPath))
            {
                _folderPath = folderPath;
            }
            else
            {
                Console.WriteLine("The system cannot find the path specified.");
            }
            return _folderPath;
        }

        /// <summary>
        /// Rename file if name is duplicated
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>File path name is unduplicated</returns>
        public string HandleDuplicatedFileName(string fileName)
        {
            string filePath = $"{_folderPath}{fileName}";
            string fileNameWithoutExtension = fileName.Split('.').First();
            string fileExtension = fileName.Split('.').Last();
            int i = 1;
            while (File.Exists(filePath))
            {
                fileName = $"{fileNameWithoutExtension} ({i}).{fileExtension}";
                filePath = $"{_folderPath}{fileName}";
                i++;
            }
            return filePath;
        }
    }
}