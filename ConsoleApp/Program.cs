// See https://aka.ms/new-console-template for more information
using ConsoleApp;
using MyClassLibrary;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;
FileManager fileManager = new FileManager();
//FtpClient ftpClient2 = new FtpClient("127.0.0.1", 1234);
//ftpClient2.Connect();
//ftpClient2.SendFile("C:\\Users\\TUAN\\OneDrive\\Máy tính\\F12.txt");
//ftpClient2.Disconnect();
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
            string folderPath = Console.ReadLine();
            fileManager.CDCommand(folderPath);
            break;
        case 3:
            Console.Write("Nhập tên thư mục: ");
            string? folder = Console.ReadLine();
            if (folder == null)
            {
                Console.WriteLine("Tên thư mục không được để trống!");
                break;
            }
            else
            {
                fileManager.CreateDirectory(folder);
            }
            break;
        case 4:
            Console.Write("Nhập tên file: ");
            string? file = Console.ReadLine();
            if (file == null)
            {
                Console.WriteLine("Tên file không được để trống!");
                break;
            }
            string filePath = @$"{fileManager.GetPath()}\{file}";
            FtpClient ftpClient = new FtpClient("127.0.0.1", 1234);
            ftpClient.Connect();
            try
            {
                ftpClient.SendFile(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
            ftpClient.Disconnect();
            break;
        case 5:
            Console.Write("Nhập tên file: ");
            string? file2 = Console.ReadLine();
            if (file2 == null)
            {
                Console.WriteLine("Tên file không được để trống!");
                break;
            }
            string filePath2 = file2;
            FtpClient ftpClient2 = new FtpClient("127.0.0.1", 1234);
            ftpClient2.Connect();
            try
            {
                ftpClient2.ReceiveFile(filePath2);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
            ftpClient2.Disconnect();
            break;
        case 10:
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Chức năng không tồn tại!");
            break;
    }
}