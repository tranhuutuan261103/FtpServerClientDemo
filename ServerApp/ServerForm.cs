using MyFtpServer;
using System.Net;
using System.Net.Sockets;
using static MyFtpServer.FtpServer;

namespace ServerApp
{
    public partial class ServerForm : Form
    {
        private FtpServer _ftpServer;
        private string _serverIp;
        private int _serverPort;
        public ServerForm()
        {
            InitializeComponent();
            try
            {
                _serverIp = GetLocalIPAddress();
            }
            catch
            {
                _serverIp = "127.0.0.1";
            }
            _serverPort = 1234;
            txt_IP.Text = _serverIp;
            txt_Port.Text = _serverPort.ToString();
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            _serverIp = txt_IP.Text;
            _serverPort = int.Parse(txt_Port.Text);
            try
            {
                _ftpServer = new FtpServer(_serverIp, _serverPort, CommandReceivedHandler);
                Thread thread = new Thread(new ThreadStart(_ftpServer.Start));
                thread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CommandReceivedHandler(string message)
        {
            if (richTextBox_Log.InvokeRequired)
            {
                richTextBox_Log.Invoke(new Action<string>(CommandReceivedHandler), new object[] { message });
            }
            else
            {
                richTextBox_Log.AppendText(message + "\n");
                richTextBox_Log.ScrollToCaret();
            }
        }

        private string GetLocalIPAddress()
        {
            string localIp = "";
            try
            {
                IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

                foreach (IPAddress ipAddress in localIPs)
                {
                    if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                    {
                        localIp = ipAddress.ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return localIp;
        }

        private void txt_Port_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txt_Port.Text, out int port))
            {
                if (port < 0 || port > 65535)
                {
                    MessageBox.Show("Port must be in range 0 - 65535");
                    txt_Port.Text = "1234";
                }
            }
            else
            {
                MessageBox.Show("Port must be a number");
                txt_Port.Text = "1234";
            }
        }

        private void txt_Port_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
    }
}