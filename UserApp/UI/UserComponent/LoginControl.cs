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
        public LoginControl(LoginDelegate loginInvoke, SetFormRegisterDelegate setFormRegisterInvoke)
        {
            InitializeComponent();
            LoginInvoke = loginInvoke;
            textBox_Username.Text = "tuan";
            textBox_Password.Text = "tuan";
            SetFormRegisterInvoke = setFormRegisterInvoke;
        }
        public void SetUsername(string username)
        {
            textBox_Username.Text = username;
        }
        public void SetPassword(string password)
        {
            textBox_Password.Text = password;
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            LoginInvoke?.Invoke(textBox_Username.Text, textBox_Password.Text);
        }

        private void label_GoToRegister_Click(object sender, EventArgs e)
        {
            SetFormRegisterInvoke();
        }
    }
}
