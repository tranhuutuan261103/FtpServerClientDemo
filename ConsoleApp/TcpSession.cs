using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class TcpSession
    {
        private TcpClient _clientSesstion;
        private TcpSessionStatus _status = TcpSessionStatus.Closed;
        private string _host;
        private int _port;

        public TcpSession(string host, int port)
        {
            _clientSesstion = new TcpClient();
            _host = host;
            _port = port;
        }

        public TcpClient GetTcpClient()
        {
            return _clientSesstion;
        }

        public void Connect()
        {
            _clientSesstion.Connect(_host, _port);
            _status = TcpSessionStatus.Connected;
        }

        public void Close()
        {
            _clientSesstion.Close();
            _status = TcpSessionStatus.Closed;
        }

        public TcpSessionStatus GetStatus()
        {
            return _status;
        }

        public void SetStatus(TcpSessionStatus status)
        {
            _status = status;
        }

    }

    public enum TcpSessionStatus
    {
        Connected,
        Closed,
        Busy
    }
}
