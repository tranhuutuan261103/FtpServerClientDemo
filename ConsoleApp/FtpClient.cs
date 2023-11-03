using MyClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class FtpClient
    {
        private string _host;
        private int _port;
        private TcpClient _client;
        private StreamWriter _writer;
        private StreamReader _reader;
        private string Command;
        private string Response;
        private string remoteFolderPath = @"D:\FileServer\";
        public FtpClient(string host, int port)
        {
            _host = host;
            _port = port;
            _client = new TcpClient();
            
            _client.Connect(_host, _port);
            _writer = new StreamWriter(_client.GetStream()) { AutoFlush = true };
            _reader = new StreamReader(_client.GetStream());

            Command = "";
            Response = "";
        }

        public void ReceiveFile(string remoteFolderPath, string remoteFileName, string localFolderPath)
        {
            Command = "PASV";
            _writer.WriteLine(Command);
            Response = _reader.ReadLine() ?? "";
            Console.WriteLine(Response); // Console command line
            if (Response.StartsWith("227 ") == true)
            {
                IPEndPoint server_data_endpoint = GetServerEndpoint(Response);
                Command = string.Format("CWD {0}", remoteFolderPath.Replace("\\", "/"));
                _writer.WriteLine(Command);
                Response = _reader.ReadLine() ?? "";
                if (Response.StartsWith("250 "))
                {
                    Console.WriteLine(Response); // Console command line
                    Command = string.Format("RETR {0}", remoteFileName);
                    _writer.WriteLine(Command);

                    TcpClient data_channel = new TcpClient();
                    data_channel.Connect(server_data_endpoint);

                    Response = _reader.ReadLine() ?? "";
                    Console.WriteLine(Response); // Console command line
                    if (Response.StartsWith("150 "))
                    {
                        NetworkStream ns = data_channel.GetStream();
                        int blocksize = 1024;
                        byte[] buffer = new byte[blocksize];
                        int byteread = 0;
                        lock (this)
                        {
                            FileStream fs = new FileStream(localFolderPath + @"\" + remoteFileName, FileMode.OpenOrCreate, FileAccess.Write);
                            while (true)
                            {
                                byteread = ns.Read(buffer, 0, blocksize);
                                fs.Write(buffer, 0, byteread);
                                if (byteread == 0)
                                {
                                    break;
                                }
                            }
                            fs.Flush();
                            fs.Close();
                        }
                        Response = _reader.ReadLine() ?? "";
                        if (Response.StartsWith("226 "))
                        {
                            Console.WriteLine(Response); // Console command line
                            data_channel.Close();
                        }
                    }
                }
            }
        }

        private IPEndPoint GetServerEndpoint(string response)
        {
            int start = response.IndexOf('(');
            int end = response.IndexOf(')');
            string substr = response.Substring(start + 1, end - start - 1);
            string[] octets = substr.Split(',');
            int port = int.Parse(octets[4]) * 256 + int.Parse(octets[5]);
            IPAddress address = new IPAddress(new byte[] { byte.Parse(octets[0]), byte.Parse(octets[1]), byte.Parse(octets[2]), byte.Parse(octets[3]) });
            return new IPEndPoint(address, port);
        }
    }
}
