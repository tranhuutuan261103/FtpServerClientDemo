namespace UserApp.UI.UserComponent
{
    partial class ResetPasswordControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            txt_Email = new Guna.UI2.WinForms.Guna2TextBox();
            txt_OTP = new Guna.UI2.WinForms.Guna2TextBox();
            btn_OTP = new Guna.UI2.WinForms.Guna2Button();
            txt_NewPassword = new Guna.UI2.WinForms.Guna2TextBox();
            txt_ConfirmNewPassword = new Guna.UI2.WinForms.Guna2TextBox();
            btb_Submit = new Guna.UI2.WinForms.Guna2Button();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            label_GoToLogin = new Guna.UI2.WinForms.Guna2HtmlLabel();
            SuspendLayout();
            // 
            // txt_Email
            // 
            txt_Email.CustomizableEdges = customizableEdges1;
            txt_Email.DefaultText = "";
            txt_Email.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txt_Email.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txt_Email.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txt_Email.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txt_Email.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_Email.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            txt_Email.ForeColor = Color.Black;
            txt_Email.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_Email.Location = new Point(70, 50);
            txt_Email.Margin = new Padding(4, 5, 4, 5);
            txt_Email.Name = "txt_Email";
            txt_Email.PasswordChar = '\0';
            txt_Email.PlaceholderText = "Email";
            txt_Email.SelectedText = "";
            txt_Email.ShadowDecoration.CustomizableEdges = customizableEdges2;
            txt_Email.Size = new Size(280, 40);
            txt_Email.TabIndex = 0;
            // 
            // txt_OTP
            // 
            txt_OTP.CustomizableEdges = customizableEdges3;
            txt_OTP.DefaultText = "";
            txt_OTP.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txt_OTP.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txt_OTP.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txt_OTP.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txt_OTP.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_OTP.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txt_OTP.ForeColor = Color.Black;
            txt_OTP.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_OTP.Location = new Point(70, 100);
            txt_OTP.Margin = new Padding(3, 4, 3, 4);
            txt_OTP.Name = "txt_OTP";
            txt_OTP.PasswordChar = '\0';
            txt_OTP.PlaceholderText = "Mã OTP";
            txt_OTP.SelectedText = "";
            txt_OTP.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txt_OTP.Size = new Size(140, 40);
            txt_OTP.TabIndex = 1;
            txt_OTP.KeyPress += txt_OTP_KeyPress;
            // 
            // btn_OTP
            // 
            btn_OTP.CustomizableEdges = customizableEdges5;
            btn_OTP.DisabledState.BorderColor = Color.DarkGray;
            btn_OTP.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_OTP.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_OTP.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_OTP.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btn_OTP.ForeColor = Color.White;
            btn_OTP.Location = new Point(225, 100);
            btn_OTP.Name = "btn_OTP";
            btn_OTP.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btn_OTP.Size = new Size(125, 40);
            btn_OTP.TabIndex = 2;
            btn_OTP.Text = "Gửi OTP";
            btn_OTP.Click += btn_OTP_Click;
            // 
            // txt_NewPassword
            // 
            txt_NewPassword.CustomizableEdges = customizableEdges7;
            txt_NewPassword.DefaultText = "";
            txt_NewPassword.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txt_NewPassword.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txt_NewPassword.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txt_NewPassword.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txt_NewPassword.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_NewPassword.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            txt_NewPassword.ForeColor = Color.Black;
            txt_NewPassword.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_NewPassword.Location = new Point(70, 160);
            txt_NewPassword.Margin = new Padding(4, 5, 4, 5);
            txt_NewPassword.Name = "txt_NewPassword";
            txt_NewPassword.PasswordChar = '●';
            txt_NewPassword.PlaceholderText = "New password";
            txt_NewPassword.SelectedText = "";
            txt_NewPassword.ShadowDecoration.CustomizableEdges = customizableEdges8;
            txt_NewPassword.Size = new Size(280, 40);
            txt_NewPassword.TabIndex = 3;
            txt_NewPassword.UseSystemPasswordChar = true;
            // 
            // txt_ConfirmNewPassword
            // 
            txt_ConfirmNewPassword.CustomizableEdges = customizableEdges9;
            txt_ConfirmNewPassword.DefaultText = "";
            txt_ConfirmNewPassword.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txt_ConfirmNewPassword.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txt_ConfirmNewPassword.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txt_ConfirmNewPassword.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txt_ConfirmNewPassword.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_ConfirmNewPassword.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            txt_ConfirmNewPassword.ForeColor = Color.Black;
            txt_ConfirmNewPassword.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_ConfirmNewPassword.Location = new Point(70, 210);
            txt_ConfirmNewPassword.Margin = new Padding(4, 5, 4, 5);
            txt_ConfirmNewPassword.Name = "txt_ConfirmNewPassword";
            txt_ConfirmNewPassword.PasswordChar = '\0';
            txt_ConfirmNewPassword.PlaceholderText = "Confirm new password";
            txt_ConfirmNewPassword.SelectedText = "";
            txt_ConfirmNewPassword.ShadowDecoration.CustomizableEdges = customizableEdges10;
            txt_ConfirmNewPassword.Size = new Size(280, 40);
            txt_ConfirmNewPassword.TabIndex = 4;
            // 
            // btb_Submit
            // 
            btb_Submit.BorderRadius = 25;
            btb_Submit.CustomizableEdges = customizableEdges11;
            btb_Submit.DisabledState.BorderColor = Color.DarkGray;
            btb_Submit.DisabledState.CustomBorderColor = Color.DarkGray;
            btb_Submit.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btb_Submit.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btb_Submit.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            btb_Submit.ForeColor = Color.White;
            btb_Submit.Location = new Point(70, 300);
            btb_Submit.Name = "btb_Submit";
            btb_Submit.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btb_Submit.Size = new Size(280, 50);
            btb_Submit.TabIndex = 5;
            btb_Submit.Text = "Reset password";
            btb_Submit.Click += btb_Submit_Click;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Location = new Point(102, 272);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(150, 22);
            guna2HtmlLabel1.TabIndex = 6;
            guna2HtmlLabel1.Text = "Remember password?";
            // 
            // label_GoToLogin
            // 
            label_GoToLogin.BackColor = Color.Transparent;
            label_GoToLogin.Location = new Point(258, 272);
            label_GoToLogin.Name = "label_GoToLogin";
            label_GoToLogin.Size = new Size(40, 22);
            label_GoToLogin.TabIndex = 7;
            label_GoToLogin.Text = "<a>Login</a>";
            label_GoToLogin.Click += label_GoToLogin_Click;
            // 
            // ResetPasswordControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label_GoToLogin);
            Controls.Add(guna2HtmlLabel1);
            Controls.Add(btb_Submit);
            Controls.Add(txt_ConfirmNewPassword);
            Controls.Add(txt_NewPassword);
            Controls.Add(btn_OTP);
            Controls.Add(txt_OTP);
            Controls.Add(txt_Email);
            Name = "ResetPasswordControl";
            Size = new Size(420, 380);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2TextBox txt_Email;
        private Guna.UI2.WinForms.Guna2TextBox txt_OTP;
        private Guna.UI2.WinForms.Guna2Button btn_OTP;
        private Guna.UI2.WinForms.Guna2TextBox txt_NewPassword;
        private Guna.UI2.WinForms.Guna2TextBox txt_ConfirmNewPassword;
        private Guna.UI2.WinForms.Guna2Button btb_Submit;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel label_GoToLogin;
    }
}
