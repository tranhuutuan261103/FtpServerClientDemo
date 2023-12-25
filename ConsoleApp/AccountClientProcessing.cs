using MyClassLibrary.Bean.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace MyFtpClient
{
    public class AccountClientProcessing
    {
        private TcpClient _tcpClient;
        private StreamReader _reader;
        private StreamWriter _writer;
        public AccountClientProcessing(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
            _reader = new StreamReader(_tcpClient.GetStream());
            _writer = new StreamWriter(_tcpClient.GetStream()) { AutoFlush = true };
        }
        public AccountInfoVM GetAccountInfor()
        {
            string command, response;

            command = "PASV";
            _writer.WriteLine(command);
            response = _reader.ReadLine() ?? "";
            if (response.StartsWith("227 ") == true)
            {
                IPEndPoint server_data_endpoint = GetServerEndpoint(response);
                command = "GETACCOUNTINFOR";
                _writer.WriteLine(command);
                TcpClient data_channel = new TcpClient();
                data_channel.Connect(server_data_endpoint);
                response = _reader.ReadLine() ?? "";
                if (response.StartsWith("150 "))
                {
                    NetworkStream ns = data_channel.GetStream();
                    byte[] buffer = new byte[1024];
                    int byteread = 0;
                    string data = "";
                    while (true)
                    {
                        byteread = ns.Read(buffer, 0, 1024);
                        data += Encoding.UTF8.GetString(buffer, 0, byteread);
                        if (byteread == 0)
                        {
                            break;
                        }
                    }

                    response = _reader.ReadLine() ?? "";
                    if (response.StartsWith("226 "))
                    {
                        data_channel.Close();
                    }

                    AccountInfoVM accountInfoVM = Newtonsoft.Json.JsonConvert.DeserializeObject<AccountInfoVM>(data);
                    return accountInfoVM ?? new AccountInfoVM();
                }
            }
            return new AccountInfoVM();
        }

        public void UpdateAccountInfor(AccountInfoVM account)
        {
            string command, response;
            command = "PASV";
            _writer.WriteLine(command);
            response = _reader.ReadLine() ?? "";
            if (response.StartsWith("227 ") == true)
            {
                IPEndPoint server_data_endpoint = GetServerEndpoint(response);
                command = "UPDATEACCOUNTINFOR";
                _writer.WriteLine(command);
                TcpClient data_channel = new TcpClient();
                data_channel.Connect(server_data_endpoint);
                response = _reader.ReadLine() ?? "";
                if (response.StartsWith("150 "))
                {
                    NetworkStream ns = data_channel.GetStream();
                    byte[] buffer = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(account));
                    ns.Write(buffer, 0, buffer.Length);
                    ns.Close();
                    response = _reader.ReadLine() ?? "";
                    if (response.StartsWith("226 "))
                    {
                        data_channel.Close();
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
