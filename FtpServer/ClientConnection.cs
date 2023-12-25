using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyFtpServer
{
    public class ClientConnection
    {
        private int _idAccount;
        private TcpClient _tcpClient;

        public ClientConnection(int idAccount, TcpClient tcpClient)
        {
            _idAccount = idAccount;
            _tcpClient = tcpClient;
        }

        public int IdAccount
        {
            get { return _idAccount; }
            set { _idAccount = value; }
        }

        public TcpClient TcpClient
        {
            get { return _tcpClient; }
            set { _tcpClient = value; }
        }

        internal void Close()
        {
            _tcpClient.Close();
        }
    }
}
