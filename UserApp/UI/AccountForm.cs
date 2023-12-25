using ConsoleApp;
using MyClassLibrary.Bean;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            ftpClient = new FtpClient("127.0.0.1", 1234);
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
    }
}
