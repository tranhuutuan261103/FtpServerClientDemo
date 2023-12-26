using MyClassLibrary.Bean;
using MyFtpClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserApp.DTO;
using UserApp.UI.UserComponent;

namespace UserApp.UI
{
    public partial class AccountForm : Form
    {
        private FtpClient ftpClient;
        private LoginControl loginControl;
        private RegisterControl registerControl;
        private ResetPasswordControl resetPasswordControl;
        public AccountForm()
        {
            InitializeComponent();
            loginControl = new LoginControl(LoginInvoke, SetFormRegisterInvoke, SetFormResetPasswordInvoke);
            registerControl = new RegisterControl(RegisterInvoke, SetFormLoginInvoke);
            resetPasswordControl = new ResetPasswordControl(ResetPasswordInvoke, SetFormLogin);
            txt_IPAddress.Text = GetLocalIPAddress();
            txt_Port.Text = "1234";
            ftpClient = new FtpClient(txt_IPAddress.Text, 1234);
        }

        public delegate void SetFormLoginDelegate();
        public void SetFormLoginInvoke()
        {
            if (InvokeRequired)
            {
                Invoke(new SetFormLoginDelegate(SetFormLogin));
            }
            else
            {
                SetFormLogin();
            }
        }
        private void SetFormLogin()
        {
            panel_Container.Controls.Clear();
            panel_Container.Controls.Add(loginControl);
            loginControl.Show();
        }

        public delegate void SetFormRegisterDelegate();
        public void SetFormRegisterInvoke()
        {
            if (InvokeRequired)
            {
                Invoke(new SetFormRegisterDelegate(SetFormRegister));
            }
            else
            {
                SetFormRegister();
            }
        }

        private void SetFormRegister()
        {
            panel_Container.Controls.Clear();
            panel_Container.Controls.Add(registerControl);
            registerControl.Show();
        }

        public delegate void SetFormResetPasswordDelegate();
        public void SetFormResetPasswordInvoke()
        {
            if (InvokeRequired)
            {
                Invoke(new SetFormResetPasswordDelegate(SetFormResetPassword));
            }
            else
            {
                SetFormResetPassword();
            }
        }

        private void SetFormResetPassword()
        {
            panel_Container.Controls.Clear();
            panel_Container.Controls.Add(resetPasswordControl);
            resetPasswordControl.Show();
        }

        private void AccountForm_Load(object sender, EventArgs e)
        {
            SetFormLogin();
        }

        private void LoginInvoke(string username, string password)
        {
            ftpClient = GetFtpClient();
            if (ftpClient.Login(username, password) == true)
            {
                MainForm mainForm = new MainForm(ftpClient, username);
                mainForm.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Login failed");
            }
        }

        private void RegisterInvoke(RegisterRequest request)
        {
            ftpClient = GetFtpClient();
            if (ftpClient.Register(request) == true)
            {
                MessageBox.Show("Register successfully");
                SetFormLoginInvoke();
                loginControl.SetUsername(request.Username);
                loginControl.SetPassword("");
            }
            else
            {
                MessageBox.Show("Register failed");
            }
        }

        private void ResetPasswordInvoke(ResetPasswordRequest request)
        {
            ftpClient = GetFtpClient();
            if (ftpClient.ResetPassword(request) == true)
            {
                MessageBox.Show("Reset password successfully");
                SetFormLoginInvoke();
                loginControl.SetUsername(request.Email);
                loginControl.SetPassword("");
            }
            else
            {
                MessageBox.Show("Reset password failed");
            }
        }

        private void txt_Port_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
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

        private FtpClient GetFtpClient()
        {
            string ipAddress = txt_IPAddress.Text;
            int port = int.Parse(txt_Port.Text);
            ftpClient.SetIPAdressAndPort(ipAddress, port);
            return ftpClient;
        }

        private string GetLocalIPAddress()
        {
            string localIp = "127.0.0.1";
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
    }
}
