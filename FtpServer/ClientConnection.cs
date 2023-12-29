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
        private TcpClient _dataClient;

        public ClientConnection(int idAccount, TcpClient tcpClient)
        {
            _idAccount = idAccount;
            _tcpClient = tcpClient;
            _dataClient = tcpClient;
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

        public TcpClient DataClient
        {
            get { return _dataClient; }
            set { _dataClient = value; }
        }

        internal void Close()
        {
            try {
                _tcpClient.Close();
                _dataClient.Close();
            } catch (Exception)
            {

            }
        }
    }
}
