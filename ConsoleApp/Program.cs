// See https://aka.ms/new-console-template for more information
using ConsoleApp;
using MyClassLibrary;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

FtpClient ftpClient = new FtpClient("127.0.0.1", 1234);

string localFolderPath = @"D:\FileClient";
string remoteFolderPath = @"";
FileManager fileManager = new FileManager(localFolderPath);

while (true)
{
    Console.WriteLine("----- FTP Client -----");
    Console.WriteLine($"Bạn đang ở đây: {fileManager.GetPath()}");
    Console.WriteLine("1. Hiển thị danh sách thư mục con");
    Console.WriteLine("2. Thay đổi thư mục làm việc hiện tại");
    Console.WriteLine("3. Tạo thư mục");
    Console.WriteLine("4. Gửi file");
    Console.WriteLine("5. Nhận file");
    Console.WriteLine("10. Thoát");
    Console.Write("Chọn chức năng: ");
    int choice = int.Parse(Console.ReadLine());
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
            Console.Write("Nhập tên thư mục: ");
            string folder = Console.ReadLine() ?? "";
            if (folder == "")
            {
                Console.WriteLine("Tên thư mục không được để trống!");
                break;
            }
            fileManager.CreateDirectory(folder);
            break;
        case 4:
            
            break;
        case 5:
            Console.Write("Nhập tên thư mục: ");
            string? fileName = Console.ReadLine();
            if (fileName == null)
            {
                Console.WriteLine("Tên file không được để trống!");
                break;
            }
            else
            {
                ftpClient.ReceiveFile(remoteFolderPath, fileName, fileManager.GetCurrentFolderPath());
            }
            break;
        case 10:
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Chức năng không tồn tại!");
            break;
    }
}