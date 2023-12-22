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
    }
}
