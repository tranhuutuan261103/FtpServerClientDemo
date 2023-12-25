// See https://aka.ms/new-console-template for more information
using MyClassLibrary;
using MyClassLibrary.Common;
using MyFtpClient;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

FtpClient ftpClient = new FtpClient("127.0.0.1", 1234);

void TransferProgress(FileTransferProcessing sender)
{
    Console.WriteLine($"{sender.FileName}");
}

void ChangeFoldersAndFileHandler(List<FileInfor> foldersAndFiles)
{
    foreach (var item in foldersAndFiles)
    {
        Console.WriteLine($"{item.Name}");
    }
}

string localFolderPath = @"D:\FileClient";
FileManager fileManager = new FileManager(localFolderPath);

Console.WriteLine("Help - Hiển thị danh sách các lệnh\n");
/*
while (true)
{
    Console.Write(fileManager.GetCurrentPath() + ">");
    string request = Console.ReadLine() ?? "";
    string command = request.Split(' ')[0].ToUpper();

    switch (command)
    {
        case "HELP":
            Console.WriteLine("DIR - Get list folder and file in local");
            Console.WriteLine("CD - Change path in local");
            Console.WriteLine("PWD - Path of server");
            Console.WriteLine("DOWNLOAD - Download file form server to local");
            Console.WriteLine("UPLOAD - Upload file to server");
            Console.WriteLine("EXIT - Exit program");
            break;
        case "DIR":
            fileManager.Dir();
            break;
        case "CD":
            fileManager.ChangeDirectory(request);
            break;
        case "PWD":
            Console.WriteLine("S>" + ftpClient.Pwd());
            break;
        case "CWD":
            Console.WriteLine(ftpClient.Cwd(request) ? "S>" + ftpClient.Pwd() : "Couldn't found" );
            break;
        case "MLSD":
            Console.WriteLine("S>" + ftpClient.Pwd());
            ftpClient.Mlsd();
            break;
        case "EXPRESSDOWNLOAD":
            string[] parts5 = request.Split(' ');
            string fileName5 = request.Substring(parts5[0].Length + 1).Trim();
            string remoteFolder5 = ftpClient.Pwd();
            ftpClient.ExpressDownload($"{((remoteFolder5 == "\\") ? $"{remoteFolder5}" : $"{remoteFolder5}\\")}{fileName5}", fileManager.GetCurrentPath());
            break;
        case "DOWNLOAD":
            string[] parts = request.Split(' ');
            string fileName = request.Substring(parts[0].Length + 1).Trim();
            string remoteFolder = ftpClient.Pwd();
            ftpClient.Download($"{((remoteFolder == "\\") ? $"{remoteFolder}" : $"{remoteFolder}\\")}{fileName}", fileManager.GetCurrentPath());
            break;
        case "DOWNLOADFOLDER":
            string[] parts3 = request.Split(' ');
            string folderName3 = request.Substring(parts3[0].Length + 1).Trim();
            string remoteFolder3 = ftpClient.Pwd();
            ftpClient.DownloadFolder($"{((remoteFolder3 == "\\") ? $"{remoteFolder3}" : $"{remoteFolder3}\\")}{folderName3}", $"{fileManager.GetCurrentPath()}\\{folderName3}");
            break;
        case "EXPRESSUPLOAD":
            string[] parts4 = request.Split(' ');
            string fileName4 = request.Substring(parts4[0].Length + 1).Trim();
            ftpClient.ExpressUpload(ftpClient.Pwd(), $"{fileManager.GetCurrentPath()}\\{fileName4}");
            break;
        case "UPLOAD":
            string[] parts1 = request.Split(' ');
            string fileName1 = request.Substring(parts1[0].Length + 1).Trim();
            ftpClient.Upload(ftpClient.Pwd(), $"{fileManager.GetCurrentPath()}\\{fileName1}");
            break;
        case "UPLOADFOLDER":
            string[] parts2 = request.Split(' ');
            string folderName = request.Substring(parts2[0].Length + 1).Trim();
            string remoteFolder2 = ftpClient.Pwd();
            ftpClient.UploadFolder($"{((remoteFolder2 == "\\") ? $"{remoteFolder2}" : $"{remoteFolder2}\\")}{folderName}", $"{fileManager.GetCurrentPath()}\\{folderName}");
            break;
        case "EXIT":
            return;
        default:
            Console.WriteLine("Chức năng không tồn tại!");
            break;
    }
}*/