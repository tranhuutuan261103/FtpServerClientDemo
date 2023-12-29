using MyClassLibrary.Bean.Account;
using MyFtpServer;
using MyFtpServer.DAL;
using ServerApp.UserComponent;
using System.Net;
using System.Net.Sockets;

namespace ServerApp
{
    public partial class ServerForm : Form
    {
        private FtpServer _ftpServer;
        private string _serverIp;
        private int _serverPort;
        private string _rootPath = "C:\\FileServer";
        public ServerForm()
        {
            InitializeComponent();
            flowLayoutPanel_Account.AutoScroll = false;
            flowLayoutPanel_Account.HorizontalScroll.Enabled = false;
            flowLayoutPanel_Account.HorizontalScroll.Visible = false;
            flowLayoutPanel_Account.HorizontalScroll.Maximum = 0;
            flowLayoutPanel_Account.AutoScroll = true;
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
            txt_rootPath.Text = _rootPath;
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            if (btn_Start.Text == "Start")
            {
                StartServer();
            }
            else
            {
                StopServer();
            }
        }

        private void StartServer()
        {
            _serverIp = txt_IP.Text;
            _serverPort = int.Parse(txt_Port.Text);
            _rootPath = txt_rootPath.Text;
            try
            {
                _ftpServer = new FtpServer(_serverIp, _serverPort, _rootPath, CommandReceivedHandler);
                Thread thread = new Thread(new ThreadStart(_ftpServer.Start));
                thread.Start();
                Thread threadConnection = new Thread(HandleListConnections);
                threadConnection.Start();
                SetListUser();
                btn_Start.Text = "Stop";
                txt_IP.Enabled = false;
                txt_Port.Enabled = false;
                txt_rootPath.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StopServer()
        {
            _ftpServer.Stop();
            _ftpServer = null;
            btn_Start.Text = "Start";
            txt_IP.Enabled = true;
            txt_Port.Enabled = true;
            txt_rootPath.Enabled = true;
            lbl_NumConnection.Text = "0";
            flowLayoutPanel_Account.Controls.Clear();
        }

        private void CommandReceivedHandler(string message)
        {
            if (richTextBox_Log.InvokeRequired)
            {
                if (!richTextBox_Log.IsDisposed && !richTextBox_Log.Disposing)
                {
                    richTextBox_Log.Invoke(new Action<string>(CommandReceivedHandler), new object[] { message });
                }
            }
            else
            {
                if (!richTextBox_Log.IsDisposed && !richTextBox_Log.Disposing)
                {
                    richTextBox_Log.AppendText(message + "\n");
                    richTextBox_Log.ScrollToCaret();
                }
            }
        }

        private void HandleListConnections(object? obj)
        {
            try
            {
                while (true)
                {
                    if (_ftpServer != null)
                    {
                        List<ClientConnection> clientConnections = _ftpServer.GetConnectedClients();
                        UpdateNumConnection(clientConnections.Count);
                        UpdateListUser(clientConnections);
                    }
                    Thread.Sleep(2000);
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateListUser(List<ClientConnection> clientConnections)
        {
            try
            {
                if (flowLayoutPanel_Account.InvokeRequired)
                {
                    flowLayoutPanel_Account.Invoke(new Action<List<ClientConnection>>(UpdateListUser), new object[] { clientConnections });
                }
                else
                {
                    foreach (AccountControl accountControl in flowLayoutPanel_Account.Controls)
                    {
                        if (accountControl.GetAccountInfoVM().IsDeleted == true)
                        {
                            accountControl.SetStatus(2);
                            continue;
                        }
                        accountControl.SetStatus(0);
                        foreach (ClientConnection clientConnection in clientConnections)
                        {
                            if (accountControl.GetAccountInfoVM().Id == clientConnection.IdAccount)
                            {
                                accountControl.SetStatus(1);
                                break;
                            }
                        }
                    }
                }
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void UpdateNumConnection(int num)
        {
            if (lbl_NumConnection.IsDisposed)
            {
                return;
            }
            if (lbl_NumConnection.InvokeRequired)
            {
                lbl_NumConnection.Invoke(new Action<int>(UpdateNumConnection), new object[] { num });
            }
            else
            {
                lbl_NumConnection.Text = num.ToString() + " connections";
            }
        }
        private object _lock = new object();
        private void SetListUser()
        {
            AccountDAL accountDAL = new AccountDAL();
            List<AccountInfoVM> accountInfoVMs = accountDAL.GetAccounts(_rootPath);
            lock(_lock)
            {
                flowLayoutPanel_Account.Controls.Clear();
                foreach (AccountInfoVM accountInfoVM in accountInfoVMs)
                {
                    AccountControl ac = new AccountControl(accountInfoVM, 0);
                    flowLayoutPanel_Account.Controls.Add(ac);
                }
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

        private void pictureBox_ClearCommand_Click(object sender, EventArgs e)
        {
            richTextBox_Log.Clear();
        }

        private void btn_UnBan_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to unban these accounts?", "Unban accounts", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            List<AccountInfoVM> list = new List<AccountInfoVM>();
            foreach (AccountControl accountControl in flowLayoutPanel_Account.Controls)
            {
                if (accountControl.IsSelected() == true)
                {
                    list.Add(accountControl.GetAccountInfoVM());
                }
                accountControl.SetSelected(false);
            }
            if (list.Count == 0)
            {
                MessageBox.Show("Please select at least one account");
                return;
            }
            AccountDAL dal = new AccountDAL();
            foreach (var item in list)
            {
                if (dal.RestoreAccount(item.Id) == true)
                {
                    CommandReceivedHandler("Unbanned account " + item.Email);
                }
            }
            SetListUser();
        }

        private void btn_Ban_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to ban these accounts?", "Ban accounts", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            List<AccountInfoVM> list = new List<AccountInfoVM>();
            foreach (AccountControl accountControl in flowLayoutPanel_Account.Controls)
            {
                if (accountControl.IsSelected() == true)
                {
                    list.Add(accountControl.GetAccountInfoVM());
                }
                accountControl.SetSelected(false);
            }
            if (list.Count == 0)
            {
                MessageBox.Show("Please select at least one account");
                return;
            }
            AccountDAL dal = new AccountDAL();
            foreach(var item in list)
            {
                if (dal.DeleteAccount(item.Id) == true)
                {
                    CommandReceivedHandler("Banned account " + item.Email);
                }
            }
            SetListUser();
        }
    }
}