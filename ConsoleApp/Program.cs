// See https://aka.ms/new-console-template for more information
using ConsoleApp;
using MyClassLibrary;
using MyClassLibrary.Common;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

FtpClient ftpClient = new FtpClient("127.0.0.1", 1234);

string localFolderPath = @"D:\FileClient";
string remoteFolderPath = @"\";
FileManager fileManager = new FileManager(localFolderPath);

while (true)
{
    Console.WriteLine("----- FTP Client -----");
    Console.WriteLine($"Bạn đang ở đây: {fileManager.GetPath()}");
    Console.WriteLine("----- Menu -----");
    Console.WriteLine("1. Hiển thị danh sách thư mục con");
    Console.WriteLine("2. Thay đổi thư mục làm việc hiện tại");
    Console.WriteLine("3. Vị trí thư mục hiện tại ở server");
    Console.WriteLine("4. Hiển thị danh sách thư mục con của server");
    Console.WriteLine("5. Thay đổi thư mục làm việc hiện tại của server");
    Console.WriteLine("6. Tạo thư mục");
    Console.WriteLine("7. Gửi file");
    Console.WriteLine("8. Nhận file");
    Console.WriteLine("8. Test gửi và nhận file đồng thời");
    Console.WriteLine("10. Nhận folder");
    Console.WriteLine("0. Thoát");
    Console.Write("Chọn chức năng: ");
    int choice = int.Parse(Console.ReadLine() ?? "0");
    switch (choice)
    {
        case 1:
            fileManager.ListDirectoryContents();
            break;
        case 2:
            Console.Write("Nhập đường dẫn thư mục: ");
            string folderPath = Console.ReadLine() ?? "";
            if (folderPath == "")
            {
                Console.WriteLine("Đường dẫn thư mục không được để trống!");
                break;
            }
            fileManager.CDCommand(folderPath);
            break;
        case 3:
            Console.WriteLine($"Bạn đang ở đây: {ftpClient.ExecuteClientCommand("PWD")}");
            break;
        case 4:
            object? obj = ftpClient.ExecuteClientCommand("MLSD");
            if (obj == null)
            {
                Console.WriteLine("Lỗi!");
                break;
            }
            
            List<FileInfor> fileInfors = (List<FileInfor>)obj;
            foreach (var fileInfor in fileInfors)
            {
                Console.WriteLine(fileInfor.ToString());
            }
            break;
        case 5:
            Console.Write("Nhập đường dẫn thư mục: ");
            string folderPath4 = Console.ReadLine() ?? "";
            if (folderPath4 == "")
            {
                Console.WriteLine("Đường dẫn thư mục không được để trống!");
                break;
            }
            //ftpClient.SetRemoteFolderPath(folderPath4);
            ftpClient.ExecuteClientCommand($"CWD {folderPath4}");
            break;
        case 6:
            Console.Write("Enter folder name: ");
            string folder = Console.ReadLine() ?? "";
            if (folder == "")
            {
                Console.WriteLine("Tên thư mục không được để trống!");
                break;
            }
            fileManager.CreateDirectory(folder);
            break;
        case 7:
            Console.Write("Enter file name: ");
            string? fileName4 = Console.ReadLine();
            if (fileName4 == null)
            {
                Console.WriteLine("Tên file không được để trống!");
                break;
            }
            else
            {
                ftpClient.ExecuteClientCommand($"stor {remoteFolderPath} {fileName4} {fileManager.GetCurrentFolderPath()}");
                //ftpClient.SendFile(remoteFolderPath, fileName4, fileManager.GetCurrentFolderPath());
            }
            break;
        case 8:
            Console.Write("Enter file name: ");
            string? fileName = Console.ReadLine();
            if (fileName == null)
            {
                Console.WriteLine("Tên file không được để trống!");
                break;
            }
            else
            {
                ftpClient.ExecuteClientCommand($"retr {remoteFolderPath} {fileName} {fileManager.GetCurrentFolderPath()}");
                //ftpClient.ReceiveFile(remoteFolderPath, fileName, fileManager.GetCurrentFolderPath());
            }
            break;
        case 9:
            ftpClient.ExecuteClientCommand($"stor {remoteFolderPath} xampp.rar {fileManager.GetCurrentFolderPath()}");
            ftpClient.ExecuteClientCommand($"retr {remoteFolderPath} xamppCopy.rar {fileManager.GetCurrentFolderPath()}");
            break;
        case 10:
            Console.Write("Enter folder name: ");
            string? folderName = Console.ReadLine();
            if(folderName == null)
            {
                Console.WriteLine("Tên thư mục không được để trống!");
                break;
            }
            Directory.CreateDirectory(fileManager.GetCurrentFolderPath() + folderName);
            ftpClient.ReceiveFolder(folderName, fileManager.GetCurrentFolderPath() + folderName);
            break;
        case 0:
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Chức năng không tồn tại!");
            break;
    }
}