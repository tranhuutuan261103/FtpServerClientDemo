using MyClassLibrary.Common;
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
        private List<FileTransferProcessing> queueCommand = new List<FileTransferProcessing>();

        public FtpClientSession(FtpClient client)
        {
            _client = client;
            for (int i = 0; i < _subClient.Length ; i++)
            {
                _subClient[i] = new TcpSession(_client.GetHost(), _client.GetPort());
            }
        }

        public void PushQueueCommand(FileTransferProcessing command)
        {
            queueCommand.Add(command);
            _client.TransferProgressHandler(command);
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

        private FileTransferProcessing? GetFirstQueueCommand()
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
                FileTransferProcessing? command = GetFirstQueueCommand();
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

        private void HandleCommand(FileTransferProcessing command, TcpSession tcpSession)
        {
            ExecuteSessionCommand(command, tcpSession.GetTcpClient());
            tcpSession.SetStatus(TcpSessionStatus.Connected);
        }

        internal void Dispose()
        {
            foreach (TcpSession tcpSession in _subClient)
            {
                tcpSession.Dispose();
            }
        }

        public void ExecuteSessionCommand(FileTransferProcessing request, TcpClient tcpSessionClient)
        {
            FtpClientProcessing fcp = new FtpClientProcessing(tcpSessionClient, _client.TransferProgressHandler);
            string command = request.Type;
            switch (command)
            {
                case "STOR":
                    fcp.SendFile(request, tcpSessionClient);
                    break;
                case "EXPRESSUPLOAD":
                    fcp.ExpressSendFile(request, tcpSessionClient);
                    break;
                case "RETR":
                    fcp.ReceiveFile(request, tcpSessionClient);
                    break;
                case "EXPRESSDOWNLOAD":
                    fcp.ExpressReceiveFile(request, tcpSessionClient);
                    break;
                default:
                    break;
            }
        }
    }
}
