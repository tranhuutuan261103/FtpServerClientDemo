namespace UserApp.UI.UserComponent
{
    partial class RegisterControl
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            btn_Submit = new Guna.UI2.WinForms.Guna2Button();
            txt_Email = new Guna.UI2.WinForms.Guna2TextBox();
            txt_Password = new Guna.UI2.WinForms.Guna2TextBox();
            txt_ConfirmPassword = new Guna.UI2.WinForms.Guna2TextBox();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            label_GoToLogin = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txt_FirstName = new Guna.UI2.WinForms.Guna2TextBox();
            txt_LastName = new Guna.UI2.WinForms.Guna2TextBox();
            txt_OTP = new Guna.UI2.WinForms.Guna2TextBox();
            btn_OTP = new Guna.UI2.WinForms.Guna2Button();
            SuspendLayout();
            // 
            // btn_Submit
            // 
            btn_Submit.BorderRadius = 25;
            btn_Submit.CustomizableEdges = customizableEdges1;
            btn_Submit.DisabledState.BorderColor = Color.DarkGray;
            btn_Submit.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_Submit.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_Submit.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_Submit.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            btn_Submit.ForeColor = Color.White;
            btn_Submit.Location = new Point(70, 307);
            btn_Submit.Name = "btn_Submit";
            btn_Submit.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btn_Submit.Size = new Size(280, 50);
            btn_Submit.TabIndex = 8;
            btn_Submit.Text = "Sign up";
            btn_Submit.Click += btn_Submit_Click;
            // 
            // txt_Email
            // 
            txt_Email.CustomizableEdges = customizableEdges3;
            txt_Email.DefaultText = "";
            txt_Email.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txt_Email.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txt_Email.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txt_Email.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txt_Email.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_Email.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            txt_Email.ForeColor = Color.Black;
            txt_Email.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_Email.Location = new Point(70, 60);
            txt_Email.Margin = new Padding(5, 6, 5, 6);
            txt_Email.Name = "txt_Email";
            txt_Email.PasswordChar = '\0';
            txt_Email.PlaceholderText = "Email";
            txt_Email.SelectedText = "";
            txt_Email.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txt_Email.Size = new Size(280, 40);
            txt_Email.TabIndex = 3;
            txt_Email.KeyPress += RegisterControl_KeyPress;
            // 
            // txt_Password
            // 
            txt_Password.CustomizableEdges = customizableEdges5;
            txt_Password.DefaultText = "";
            txt_Password.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txt_Password.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txt_Password.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txt_Password.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txt_Password.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_Password.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            txt_Password.ForeColor = Color.Black;
            txt_Password.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_Password.Location = new Point(70, 170);
            txt_Password.Margin = new Padding(5, 6, 5, 6);
            txt_Password.Name = "txt_Password";
            txt_Password.PasswordChar = '●';
            txt_Password.PlaceholderText = "Password";
            txt_Password.SelectedText = "";
            txt_Password.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txt_Password.Size = new Size(280, 40);
            txt_Password.TabIndex = 5;
            txt_Password.UseSystemPasswordChar = true;
            txt_Password.KeyPress += RegisterControl_KeyPress;
            // 
            // txt_ConfirmPassword
            // 
            txt_ConfirmPassword.CustomizableEdges = customizableEdges7;
            txt_ConfirmPassword.DefaultText = "";
            txt_ConfirmPassword.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txt_ConfirmPassword.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txt_ConfirmPassword.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txt_ConfirmPassword.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txt_ConfirmPassword.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_ConfirmPassword.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            txt_ConfirmPassword.ForeColor = Color.Black;
            txt_ConfirmPassword.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_ConfirmPassword.Location = new Point(70, 220);
            txt_ConfirmPassword.Margin = new Padding(5, 6, 5, 6);
            txt_ConfirmPassword.Name = "txt_ConfirmPassword";
            txt_ConfirmPassword.PasswordChar = '●';
            txt_ConfirmPassword.PlaceholderText = "Confirm password";
            txt_ConfirmPassword.SelectedText = "";
            txt_ConfirmPassword.ShadowDecoration.CustomizableEdges = customizableEdges8;
            txt_ConfirmPassword.Size = new Size(280, 40);
            txt_ConfirmPassword.TabIndex = 6;
            txt_ConfirmPassword.UseSystemPasswordChar = true;
            txt_ConfirmPassword.KeyPress += RegisterControl_KeyPress;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Location = new Point(121, 281);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(117, 22);
            guna2HtmlLabel1.TabIndex = 4;
            guna2HtmlLabel1.Text = "Already account?";
            // 
            // label_GoToLogin
            // 
            label_GoToLogin.BackColor = Color.Transparent;
            label_GoToLogin.Location = new Point(244, 281);
            label_GoToLogin.Name = "label_GoToLogin";
            label_GoToLogin.Size = new Size(40, 22);
            label_GoToLogin.TabIndex = 5;
            label_GoToLogin.Text = "<a>Login</a>";
            label_GoToLogin.Click += label_GoToLogin_Click;
            // 
            // txt_FirstName
            // 
            txt_FirstName.CustomizableEdges = customizableEdges9;
            txt_FirstName.DefaultText = "";
            txt_FirstName.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txt_FirstName.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txt_FirstName.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txt_FirstName.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txt_FirstName.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_FirstName.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            txt_FirstName.ForeColor = Color.Black;
            txt_FirstName.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_FirstName.Location = new Point(70, 0);
            txt_FirstName.Margin = new Padding(8);
            txt_FirstName.Name = "txt_FirstName";
            txt_FirstName.PasswordChar = '\0';
            txt_FirstName.PlaceholderText = "First name";
            txt_FirstName.SelectedText = "";
            txt_FirstName.ShadowDecoration.CustomizableEdges = customizableEdges10;
            txt_FirstName.Size = new Size(130, 40);
            txt_FirstName.TabIndex = 1;
            txt_FirstName.KeyPress += RegisterControl_KeyPress;
            // 
            // txt_LastName
            // 
            txt_LastName.CustomizableEdges = customizableEdges11;
            txt_LastName.DefaultText = "";
            txt_LastName.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txt_LastName.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txt_LastName.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txt_LastName.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txt_LastName.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_LastName.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            txt_LastName.ForeColor = Color.Black;
            txt_LastName.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_LastName.Location = new Point(220, 0);
            txt_LastName.Margin = new Padding(8, 7, 8, 7);
            txt_LastName.Name = "txt_LastName";
            txt_LastName.PasswordChar = '\0';
            txt_LastName.PlaceholderText = "Last name";
            txt_LastName.SelectedText = "";
            txt_LastName.ShadowDecoration.CustomizableEdges = customizableEdges12;
            txt_LastName.Size = new Size(130, 40);
            txt_LastName.TabIndex = 2;
            txt_LastName.KeyPress += RegisterControl_KeyPress;
            // 
            // txt_OTP
            // 
            txt_OTP.CustomizableEdges = customizableEdges13;
            txt_OTP.DefaultText = "";
            txt_OTP.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txt_OTP.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txt_OTP.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txt_OTP.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txt_OTP.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_OTP.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            txt_OTP.ForeColor = Color.Black;
            txt_OTP.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_OTP.Location = new Point(70, 110);
            txt_OTP.Margin = new Padding(5, 6, 5, 6);
            txt_OTP.Name = "txt_OTP";
            txt_OTP.PasswordChar = '\0';
            txt_OTP.PlaceholderText = "Mã OTP";
            txt_OTP.SelectedText = "";
            txt_OTP.ShadowDecoration.CustomizableEdges = customizableEdges14;
            txt_OTP.Size = new Size(140, 40);
            txt_OTP.TabIndex = 4;
            txt_OTP.KeyPress += RegisterControl_KeyPress;
            // 
            // btn_OTP
            // 
            btn_OTP.CustomizableEdges = customizableEdges15;
            btn_OTP.DisabledState.BorderColor = Color.DarkGray;
            btn_OTP.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_OTP.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_OTP.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_OTP.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btn_OTP.ForeColor = Color.White;
            btn_OTP.Location = new Point(225, 110);
            btn_OTP.Name = "btn_OTP";
            btn_OTP.ShadowDecoration.CustomizableEdges = customizableEdges16;
            btn_OTP.Size = new Size(125, 40);
            btn_OTP.TabIndex = 7;
            btn_OTP.Text = "Gửi mã OTP";
            btn_OTP.Click += btn_OTP_Click;
            // 
            // RegisterControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btn_OTP);
            Controls.Add(txt_OTP);
            Controls.Add(txt_LastName);
            Controls.Add(txt_FirstName);
            Controls.Add(label_GoToLogin);
            Controls.Add(guna2HtmlLabel1);
            Controls.Add(txt_ConfirmPassword);
            Controls.Add(txt_Password);
            Controls.Add(txt_Email);
            Controls.Add(btn_Submit);
            Name = "RegisterControl";
            Size = new Size(420, 380);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btn_Submit;
        private Guna.UI2.WinForms.Guna2TextBox txt_Email;
        private Guna.UI2.WinForms.Guna2TextBox txt_Password;
        private Guna.UI2.WinForms.Guna2TextBox txt_ConfirmPassword;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel label_GoToLogin;
        private Guna.UI2.WinForms.Guna2TextBox txt_FirstName;
        private Guna.UI2.WinForms.Guna2TextBox txt_LastName;
        private Guna.UI2.WinForms.Guna2TextBox txt_OTP;
        private Guna.UI2.WinForms.Guna2Button btn_OTP;
    }
}
