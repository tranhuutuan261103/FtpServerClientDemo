namespace UserApp.UI.UserComponent
{
    partial class InputDialog
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

        #region Windows Form Designer generated code

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
            lbl_Message = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btn_Oke = new Guna.UI2.WinForms.Guna2Button();
            btn_Cancel = new Guna.UI2.WinForms.Guna2Button();
            txt_InputText = new Guna.UI2.WinForms.Guna2TextBox();
            SuspendLayout();
            // 
            // lbl_Message
            // 
            lbl_Message.AutoSize = false;
            lbl_Message.BackColor = Color.Transparent;
            lbl_Message.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbl_Message.Location = new Point(40, 12);
            lbl_Message.Name = "lbl_Message";
            lbl_Message.Size = new Size(292, 58);
            lbl_Message.TabIndex = 0;
            lbl_Message.Text = "Are you sure you want to create a new folder?";
            // 
            // btn_Oke
            // 
            btn_Oke.CustomizableEdges = customizableEdges1;
            btn_Oke.DisabledState.BorderColor = Color.DarkGray;
            btn_Oke.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_Oke.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_Oke.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_Oke.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btn_Oke.ForeColor = Color.White;
            btn_Oke.Location = new Point(40, 146);
            btn_Oke.Name = "btn_Oke";
            btn_Oke.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btn_Oke.Size = new Size(130, 50);
            btn_Oke.TabIndex = 1;
            btn_Oke.Text = "Oke";
            btn_Oke.Click += btn_Oke_Click;
            // 
            // btn_Cancel
            // 
            btn_Cancel.CustomizableEdges = customizableEdges3;
            btn_Cancel.DisabledState.BorderColor = Color.DarkGray;
            btn_Cancel.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_Cancel.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_Cancel.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_Cancel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btn_Cancel.ForeColor = Color.White;
            btn_Cancel.Location = new Point(210, 146);
            btn_Cancel.Name = "btn_Cancel";
            btn_Cancel.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btn_Cancel.Size = new Size(130, 50);
            btn_Cancel.TabIndex = 2;
            btn_Cancel.Text = "Cancel";
            btn_Cancel.Click += btn_Cancel_Click;
            // 
            // txt_InputText
            // 
            txt_InputText.CustomizableEdges = customizableEdges5;
            txt_InputText.DefaultText = "";
            txt_InputText.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txt_InputText.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txt_InputText.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txt_InputText.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txt_InputText.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_InputText.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txt_InputText.ForeColor = Color.Black;
            txt_InputText.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txt_InputText.Location = new Point(40, 80);
            txt_InputText.Margin = new Padding(4, 6, 4, 6);
            txt_InputText.Name = "txt_InputText";
            txt_InputText.PasswordChar = '\0';
            txt_InputText.PlaceholderText = "";
            txt_InputText.SelectedText = "";
            txt_InputText.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txt_InputText.Size = new Size(300, 50);
            txt_InputText.TabIndex = 3;
            txt_InputText.KeyPress += InputDialog_KeyPress;
            // 
            // InputDialog
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(382, 226);
            Controls.Add(txt_InputText);
            Controls.Add(btn_Cancel);
            Controls.Add(btn_Oke);
            Controls.Add(lbl_Message);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "InputDialog";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "InputDialog";
            TopMost = true;
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_Message;
        private Guna.UI2.WinForms.Guna2Button btn_Oke;
        private Guna.UI2.WinForms.Guna2Button btn_Cancel;
        private Guna.UI2.WinForms.Guna2TextBox txt_InputText;
    }
}