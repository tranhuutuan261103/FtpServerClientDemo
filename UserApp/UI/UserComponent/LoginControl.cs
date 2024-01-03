using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserApp.UI.UserComponent
{
    public partial class LoginControl : UserControl
    {
        public delegate void LoginDelegate(string username, string password);
        public LoginDelegate LoginInvoke;
        public delegate void SetFormRegisterDelegate();
        public SetFormRegisterDelegate SetFormRegisterInvoke;
        public delegate void SetFormResetPasswordDelegate();
        public SetFormResetPasswordDelegate SetFormResetPasswordInvoke;
        public LoginControl(LoginDelegate loginInvoke, SetFormRegisterDelegate setFormRegisterInvoke, SetFormResetPasswordDelegate setFormResetPasswordInvoke)
        {
            InitializeComponent();
            LoginInvoke = loginInvoke;
            txt_Email.Text = "testdtcl1123@gmail.com";
            txt_Password.Text = "1234";
            SetFormRegisterInvoke = setFormRegisterInvoke;
            SetFormResetPasswordInvoke = setFormResetPasswordInvoke;
        }
        public void SetUsername(string username)
        {
            txt_Email.Text = username;
        }
        public void SetPassword(string password)
        {
            txt_Password.Text = password;
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            LoginInvoke?.Invoke(txt_Email.Text, txt_Password.Text);
        }

        private void label_GoToRegister_Click(object sender, EventArgs e)
        {
            SetFormRegisterInvoke();
        }

        private void label_GoToResetPassForm_Click(object sender, EventArgs e)
        {
            SetFormResetPasswordInvoke();
        }

        private void LoginControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            LoginInvoke?.Invoke(txt_Email.Text, txt_Password.Text);
        }
    }
}
