using MyClassLibrary.Bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class TcpSession
    {
        private TcpClient _clientSession;
        private TcpSessionStatus _status = TcpSessionStatus.Closed;
        private string _host;
        private int _port;
        private string _username;
        private string _password;

        public TcpSession(string host, int port, string username, string password)
        {
            _clientSession = new TcpClient();
            _host = host;
            _port = port;
            _status = TcpSessionStatus.Closed;
            _username = username;
            _password = password;
        }

        public TcpClient GetTcpClient()
        {
            return _clientSession;
        }

        public bool Connect()
        {
            if (_clientSession.Connected == false)
            {
                _clientSession.Connect(_host, _port);
            }
            _status = TcpSessionStatus.Connected;
            return Login(_username, _password);
        }

        private bool Login(string username, string password)
        {
            try
            {
                string response = "";
                NetworkStream ns = _clientSession.GetStream();
                StreamWriter sw = new StreamWriter(ns);
                StreamReader sr = new StreamReader(ns);
                sw.WriteLine("USER " + username);
                sw.Flush();
                if ((sr.ReadLine() ?? "").StartsWith("331"))
                {
                    sw.WriteLine("PASS " + password);
                    sw.Flush();
                    response = sr.ReadLine() ?? "";
                    if (response.StartsWith("230"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Register(RegisterRequest request)
        {
            try
            {
                if (_clientSession.Connected == false)
                {
                    _clientSession.Connect(_host, _port);
                }
                string response = "";
                NetworkStream ns = _clientSession.GetStream();
                StreamWriter sw = new StreamWriter(ns);
                StreamReader sr = new StreamReader(ns);

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                sw.WriteLine("REGISTER " + json);
                sw.Flush();
                
                response = sr.ReadLine() ?? "";
                if (response.StartsWith("230"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void Close()
        {
            _clientSession.Close();
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

        internal void Dispose()
        {
            _clientSession.Dispose();
            _status = TcpSessionStatus.Closed;
        }

        internal void SetUsername(string username)
        {
            _username = username;
        }

        internal void SetPassword(string password)
        {
            _password = password;
        }
    }

    public enum TcpSessionStatus
    {
        Connected,
        Closed,
        Busy
    }
}
