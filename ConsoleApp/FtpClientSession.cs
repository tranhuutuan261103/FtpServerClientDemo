using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class FtpClientSession
    {
        private TcpSession[] _subClient = new TcpSession[2];
        private FtpClient _client;
        private List<string> queueCommand = new List<string>();

        public FtpClientSession(FtpClient client)
        {
            _client = client;
            for (int i = 0; i < _subClient.Length ; i++)
            {
                _subClient[i] = new TcpSession(_client.GetHost(), _client.GetPort());
            }
        }

        public void PushQueueCommand(string command)
        {
            queueCommand.Add(command);
        }

        public bool PopQueueCommand()
        {
            if (queueCommand.Count > 0)
            {
                queueCommand.RemoveAt(0);
                return true;
            }
            return false;
        }

        private string? GetFirstQueueCommand()
        {
            if (queueCommand.Count > 0)
            {
                return queueCommand[0];
            }
            return null;
        }

        public void Process()
        {
            while(true)
            {
                string? command = GetFirstQueueCommand();
                if (command != null)
                {
                    foreach(TcpSession tcpSession in _subClient)
                    {
                        if (tcpSession.GetStatus() == TcpSessionStatus.Connected)
                        {
                            PopQueueCommand();
                            tcpSession.SetStatus(TcpSessionStatus.Busy);
                            Thread thread = new Thread(() => HandleCommand(command, tcpSession));
                            thread.Start();
                            break;
                        }
                        else if (tcpSession.GetStatus() == TcpSessionStatus.Closed)
                        {
                            tcpSession.Connect();
                            PopQueueCommand();
                            tcpSession.SetStatus(TcpSessionStatus.Busy);
                            Thread thread = new Thread(() => HandleCommand(command, tcpSession));
                            thread.Start();
                            break;
                        }
                    }
                }
            }
        }

        private void HandleCommand(string command, TcpSession tcpSession)
        {
            _client.ExecuteSessionCommand(command, tcpSession.GetTcpClient());
            tcpSession.SetStatus(TcpSessionStatus.Connected);
        }
    }
}
