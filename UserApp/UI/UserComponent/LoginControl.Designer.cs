namespace UserApp.UI.UserComponent
{
    partial class LoginControl
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
            txt_Email = new Guna.UI2.WinForms.Guna2TextBox();
            txt_Password = new Guna.UI2.WinForms.Guna2TextBox();
            btn_Submit = new Guna.UI2.WinForms.Guna2Button();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            label_GoToRegister = new Guna.UI2.WinForms.Guna2HtmlLabel();
            label_GoToResetPassForm = new Guna.UI2.WinForms.Guna2HtmlLabel();
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
            txt_Email.Location = new Point(70, 60);
            txt_Email.Margin = new Padding(4, 5, 4, 5);
            txt_Email.Name = "txt_Email";
            txt_Email.PasswordChar = '\0';
            txt_Email.PlaceholderText = "Email";
            txt_Email.SelectedText = "";
            txt_Email.ShadowDecoration.CustomizableEdges = customizableEdges2;
            txt_Email.Size = new Size(280, 50);
            txt_Email.TabIndex = 0;
            txt_Email.KeyPress += LoginControl_KeyPress;
            // 
            // txt_Password
            // 
            txt_Password.CustomizableEdges = customizableEdges3;
            txt_Password.DefaultText = "";
            txt_Password.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txt_Password.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txt_Password.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txt_Password.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txt_Password.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_Password.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            txt_Password.ForeColor = Color.Black;
            txt_Password.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_Password.Location = new Point(70, 145);
            txt_Password.Margin = new Padding(4, 5, 4, 5);
            txt_Password.Name = "txt_Password";
            txt_Password.PasswordChar = '●';
            txt_Password.PlaceholderText = "Password";
            txt_Password.SelectedText = "";
            txt_Password.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txt_Password.Size = new Size(280, 50);
            txt_Password.TabIndex = 1;
            txt_Password.UseSystemPasswordChar = true;
            txt_Password.KeyPress += LoginControl_KeyPress;
            // 
            // btn_Submit
            // 
            btn_Submit.BorderRadius = 25;
            btn_Submit.CustomizableEdges = customizableEdges5;
            btn_Submit.DisabledState.BorderColor = Color.DarkGray;
            btn_Submit.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_Submit.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_Submit.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_Submit.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            btn_Submit.ForeColor = Color.White;
            btn_Submit.Location = new Point(70, 274);
            btn_Submit.Name = "btn_Submit";
            btn_Submit.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btn_Submit.Size = new Size(280, 50);
            btn_Submit.TabIndex = 2;
            btn_Submit.Text = "Login";
            btn_Submit.Click += btn_Submit_Click;
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.BackColor = Color.Transparent;
            guna2HtmlLabel2.Location = new Point(85, 242);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(157, 22);
            guna2HtmlLabel2.TabIndex = 5;
            guna2HtmlLabel2.Text = "Don't have an account?";
            // 
            // label_GoToRegister
            // 
            label_GoToRegister.BackColor = Color.Transparent;
            label_GoToRegister.Location = new Point(248, 242);
            label_GoToRegister.Name = "label_GoToRegister";
            label_GoToRegister.Size = new Size(77, 22);
            label_GoToRegister.TabIndex = 6;
            label_GoToRegister.Text = "<a>Create new</a>";
            label_GoToRegister.Click += label_GoToRegister_Click;
            // 
            // label_GoToResetPassForm
            // 
            label_GoToResetPassForm.BackColor = Color.Transparent;
            label_GoToResetPassForm.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            label_GoToResetPassForm.Location = new Point(213, 203);
            label_GoToResetPassForm.Name = "label_GoToResetPassForm";
            label_GoToResetPassForm.Size = new Size(137, 25);
            label_GoToResetPassForm.TabIndex = 7;
            label_GoToResetPassForm.Text = "<a>Forgot password?</a>";
            label_GoToResetPassForm.Click += label_GoToResetPassForm_Click;
            // 
            // LoginControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label_GoToResetPassForm);
            Controls.Add(label_GoToRegister);
            Controls.Add(guna2HtmlLabel2);
            Controls.Add(btn_Submit);
            Controls.Add(txt_Password);
            Controls.Add(txt_Email);
            Name = "LoginControl";
            Size = new Size(420, 380);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2TextBox txt_Email;
        private Guna.UI2.WinForms.Guna2TextBox txt_Password;
        private Guna.UI2.WinForms.Guna2Button btn_Submit;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel label_GoToRegister;
        private Guna.UI2.WinForms.Guna2HtmlLabel label_GoToResetPassForm;
    }
}
