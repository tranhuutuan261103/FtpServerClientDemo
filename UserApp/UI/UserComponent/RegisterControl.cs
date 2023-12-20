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

namespace UserApp.UI.UserComponent
{
    public partial class RegisterControl : UserControl
    {
        public delegate void RegisterDelegate(RegisterRequest request);
        public RegisterDelegate RegisterInvoke;
        public delegate void SetFormLoginDelegate();
        public SetFormLoginDelegate SetFormLoginInvoke;
        public RegisterControl(RegisterDelegate registerInvoke, SetFormLoginDelegate setFormLoginInvoke)
        {
            InitializeComponent();
            RegisterInvoke = registerInvoke;
            SetFormLoginInvoke = setFormLoginInvoke;
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (textBox_Username.Text == "" || textBox_Password.Text == "")
            {
                MessageBox.Show("Username or password is empty!");
                return;
            }

            if (textBox_Password.Text != textBox_ConfirmPassword.Text)
            {
                MessageBox.Show("Password and confirm password are not the same!");
                return;
            }

            RegisterRequest request = new RegisterRequest()
            {
                Username = textBox_Username.Text,
                Password = textBox_Password.Text,
                FirstName = textBox_FirstName.Text,
                LastName = textBox_LastName.Text,
            };

            RegisterInvoke(request);
        }

        private void label_GoToLogin_Click(object sender, EventArgs e)
        {
            SetFormLoginInvoke();
        }
    }
}
