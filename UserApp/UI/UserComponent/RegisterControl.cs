using MyClassLibrary.Bean;
using MyClassLibrary.Common;
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
            countdownTimer = new System.Windows.Forms.Timer();
            countdownTimer.Interval = 1000;
            countdownTimer.Tick += CountdownTimer_Tick;
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (txt_Email.Text == "" || txt_Password.Text == "")
            {
                MessageBox.Show("Username or password is empty!");
                return;
            }

            if (txt_Password.Text != txt_ConfirmPassword.Text)
            {
                MessageBox.Show("Password and confirm password are not the same!");
                return;
            }

            if (txt_OTP.Text != otp && txt_OTP.Text.Length < 6)
            {
                MessageBox.Show("OTP is not correct!");
                return;
            }

            RegisterRequest request = new RegisterRequest()
            {
                Username = txt_Email.Text,
                Password = txt_Password.Text,
                FirstName = txt_FirstName.Text,
                LastName = txt_LastName.Text,
            };

            RegisterInvoke(request);
        }

        private void label_GoToLogin_Click(object sender, EventArgs e)
        {
            SetFormLoginInvoke();
        }

        private void txt_OTP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsDigit(e.KeyChar) || txt_OTP.Text.Length >= 6) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        // Handle OTP
        private string otp = "0";
        private DateTime lastClickTime;
        private int cooldownDuration = 60;
        private System.Windows.Forms.Timer countdownTimer;
        private void btn_OTP_Click(object sender, EventArgs e)
        {
            MailHelper mailHelper = new MailHelper();
            if (!mailHelper.IsValidEmail(txt_Email.Text))
            {
                MessageBox.Show("Invalid email!");
                return;
            }

            btn_OTP.Enabled = false;
            Random random = new Random();
            otp = random.Next(000000, 999999).ToString();
            if (mailHelper.SendMail(txt_Email.Text, "OTP", otp) == true)
            {
                lastClickTime = DateTime.Now;

                StartCountdownTimer();
            }
            else
            {
                MessageBox.Show("Send OTP failed!");
            }
        }

        private void StartCountdownTimer()
        {
            if (countdownTimer == null)
            {
                countdownTimer = new System.Windows.Forms.Timer();
                countdownTimer.Interval = 1000;
                countdownTimer.Tick += CountdownTimer_Tick;
            }

            int remainingSeconds = cooldownDuration;
            btn_OTP.Text = $"Resend ({remainingSeconds} s)";

            countdownTimer.Start();
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            int elapsedSeconds = (int)(DateTime.Now - lastClickTime).TotalSeconds;
            int remainingSeconds = cooldownDuration - elapsedSeconds;

            if (remainingSeconds <= 0)
            {
                countdownTimer.Stop();
                btn_OTP.Text = "Resend";
                btn_OTP.Enabled = true;
            }
            else
            {
                btn_OTP.Text = $"Resend ({remainingSeconds} s)";
            }
        }
    }
}
