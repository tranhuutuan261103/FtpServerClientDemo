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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            textBox_Username = new Guna.UI2.WinForms.Guna2TextBox();
            textBox_Password = new Guna.UI2.WinForms.Guna2TextBox();
            btn_Submit = new Guna.UI2.WinForms.Guna2Button();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            label1 = new Label();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            label_GoToRegister = new Guna.UI2.WinForms.Guna2HtmlLabel();
            SuspendLayout();
            // 
            // textBox_Username
            // 
            textBox_Username.CustomizableEdges = customizableEdges7;
            textBox_Username.DefaultText = "";
            textBox_Username.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            textBox_Username.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            textBox_Username.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            textBox_Username.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            textBox_Username.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            textBox_Username.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            textBox_Username.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            textBox_Username.Location = new Point(70, 55);
            textBox_Username.Margin = new Padding(3, 4, 3, 4);
            textBox_Username.Name = "textBox_Username";
            textBox_Username.PasswordChar = '\0';
            textBox_Username.PlaceholderText = "";
            textBox_Username.SelectedText = "";
            textBox_Username.ShadowDecoration.CustomizableEdges = customizableEdges8;
            textBox_Username.Size = new Size(280, 40);
            textBox_Username.TabIndex = 0;
            // 
            // textBox_Password
            // 
            textBox_Password.CustomizableEdges = customizableEdges9;
            textBox_Password.DefaultText = "";
            textBox_Password.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            textBox_Password.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            textBox_Password.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            textBox_Password.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            textBox_Password.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            textBox_Password.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            textBox_Password.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            textBox_Password.Location = new Point(70, 142);
            textBox_Password.Margin = new Padding(3, 4, 3, 4);
            textBox_Password.Name = "textBox_Password";
            textBox_Password.PasswordChar = '*';
            textBox_Password.PlaceholderText = "";
            textBox_Password.SelectedText = "";
            textBox_Password.ShadowDecoration.CustomizableEdges = customizableEdges10;
            textBox_Password.Size = new Size(280, 40);
            textBox_Password.TabIndex = 1;
            // 
            // btn_Submit
            // 
            btn_Submit.BorderRadius = 25;
            btn_Submit.CustomizableEdges = customizableEdges11;
            btn_Submit.DisabledState.BorderColor = Color.DarkGray;
            btn_Submit.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_Submit.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_Submit.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_Submit.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            btn_Submit.ForeColor = Color.White;
            btn_Submit.Location = new Point(70, 230);
            btn_Submit.Name = "btn_Submit";
            btn_Submit.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btn_Submit.Size = new Size(280, 50);
            btn_Submit.TabIndex = 2;
            btn_Submit.Text = "Login";
            btn_Submit.Click += btn_Submit_Click;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Location = new Point(70, 26);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(69, 22);
            guna2HtmlLabel1.TabIndex = 3;
            guna2HtmlLabel1.Text = "Username";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(69, 118);
            label1.Name = "label1";
            label1.Size = new Size(70, 20);
            label1.TabIndex = 4;
            label1.Text = "Password";
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.BackColor = Color.Transparent;
            guna2HtmlLabel2.Location = new Point(83, 202);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(157, 22);
            guna2HtmlLabel2.TabIndex = 5;
            guna2HtmlLabel2.Text = "Don't have an account?";
            // 
            // label_GoToRegister
            // 
            label_GoToRegister.BackColor = Color.Transparent;
            label_GoToRegister.Location = new Point(246, 202);
            label_GoToRegister.Name = "label_GoToRegister";
            label_GoToRegister.Size = new Size(77, 22);
            label_GoToRegister.TabIndex = 6;
            label_GoToRegister.Text = "<a>Create new</a>";
            label_GoToRegister.Click += label_GoToRegister_Click;
            // 
            // LoginControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label_GoToRegister);
            Controls.Add(guna2HtmlLabel2);
            Controls.Add(label1);
            Controls.Add(guna2HtmlLabel1);
            Controls.Add(btn_Submit);
            Controls.Add(textBox_Password);
            Controls.Add(textBox_Username);
            Name = "LoginControl";
            Size = new Size(420, 320);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2TextBox textBox_Username;
        private Guna.UI2.WinForms.Guna2TextBox textBox_Password;
        private Guna.UI2.WinForms.Guna2Button btn_Submit;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Label label1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel label_GoToRegister;
    }
}
