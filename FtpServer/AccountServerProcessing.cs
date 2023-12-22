using Azure.Core;
using MyClassLibrary.Bean.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyFtpServer
{
    public class AccountServerProcessing
    {
        private TcpClient _tcpClient;

        public AccountServerProcessing(TcpClient tcpClient)
        {
            this._tcpClient = tcpClient;
        }

        public void SendAccountInfor(AccountInfoVM accountInfo)
        {
            NetworkStream ns = _tcpClient.GetStream();
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(accountInfo);
            var bytes = Encoding.UTF8.GetBytes(json);
            ns.Write(bytes, 0, bytes.Length);
            ns.Close();
        }

        public AccountInfoVM ReceiveAccountInfor()
        {
            NetworkStream ns = _tcpClient.GetStream();
            byte[] buffer = new byte[1024];
            int byteRead;
            string data = "";
            while (true)
            {
                byteRead = ns.Read(buffer, 0, 1024);
                data += Encoding.UTF8.GetString(buffer, 0, byteRead);
                if (byteRead == 0)
                {
                    break;
                }
            }
            ns.Close();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<AccountInfoVM>(data);
        }
    }
}
