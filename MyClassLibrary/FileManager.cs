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

        public void CreateDirectory(string folderName)
        {
            try
            {
                // Tạo thư mục
                Directory.CreateDirectory(@$"{_folderPath}\{folderName}");
                Console.WriteLine("Tạo thư mục thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
        }

        public void CDCommand(string folderPath)
        {
            _folderPath += @"\" + folderPath;
        }

        public string GetPath()
        {
            return _folderPath;
        }

        public void SetCurrentDirectory()
        {
            Directory.SetCurrentDirectory(_folderPath);
        }

        public void ListDirectoryContents()
        {
            try
            {
                // Hiển thị thông tin về thư mục
                Console.WriteLine("Directory of " + _folderPath + "\n");

                GetSubDirectories();
                GetFileList();
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
        }

        private void GetSubDirectories()
        {
            // Lấy danh sách thư mục con
            string[] directories = Directory.GetDirectories(_folderPath);
            foreach (var directory in directories)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(directory);
                Console.WriteLine($"{dirInfo.LastWriteTime:MM/dd/yyyy  hh:mm tt}    <DIR>                    {dirInfo.Name}");
            }
        }

        private void GetFileList()
        {
            // Lấy danh sách tệp tin
            string[] files = Directory.GetFiles(_folderPath);
            foreach (var file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                Console.WriteLine($"{fileInfo.LastWriteTime:MM/dd/yyyy  hh:mm tt}    {fileInfo.Length,24} {fileInfo.Name}");
            }
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