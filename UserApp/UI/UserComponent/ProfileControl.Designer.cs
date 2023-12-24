namespace UserApp.UI.UserComponent
{
    partial class ProfileControl
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
            pic_Avatar = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            lbl_Fullname = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lbl_Email = new Guna.UI2.WinForms.Guna2HtmlLabel();
            checkBox = new Guna.UI2.WinForms.Guna2CheckBox();
            lbl_Role = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ((System.ComponentModel.ISupportInitialize)pic_Avatar).BeginInit();
            SuspendLayout();
            // 
            // pic_Avatar
            // 
            pic_Avatar.Image = Properties.Resources.avatar;
            pic_Avatar.ImageRotate = 0F;
            pic_Avatar.Location = new Point(10, 15);
            pic_Avatar.Name = "pic_Avatar";
            pic_Avatar.ShadowDecoration.CustomizableEdges = customizableEdges1;
            pic_Avatar.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            pic_Avatar.Size = new Size(40, 40);
            pic_Avatar.SizeMode = PictureBoxSizeMode.StretchImage;
            pic_Avatar.TabIndex = 0;
            pic_Avatar.TabStop = false;
            // 
            // lbl_Fullname
            // 
            lbl_Fullname.AutoSize = false;
            lbl_Fullname.BackColor = Color.Transparent;
            lbl_Fullname.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_Fullname.Location = new Point(61, 3);
            lbl_Fullname.Name = "lbl_Fullname";
            lbl_Fullname.Size = new Size(165, 27);
            lbl_Fullname.TabIndex = 1;
            lbl_Fullname.Text = "Full name";
            // 
            // lbl_Email
            // 
            lbl_Email.AutoSize = false;
            lbl_Email.BackColor = Color.Transparent;
            lbl_Email.Location = new Point(61, 36);
            lbl_Email.Name = "lbl_Email";
            lbl_Email.Size = new Size(165, 22);
            lbl_Email.TabIndex = 2;
            lbl_Email.Text = "Email";
            // 
            // checkBox
            // 
            checkBox.AutoSize = true;
            checkBox.CheckedState.BorderColor = Color.FromArgb(94, 148, 255);
            checkBox.CheckedState.BorderRadius = 0;
            checkBox.CheckedState.BorderThickness = 0;
            checkBox.CheckedState.FillColor = Color.FromArgb(94, 148, 255);
            checkBox.Location = new Point(240, 18);
            checkBox.Name = "checkBox";
            checkBox.Size = new Size(18, 17);
            checkBox.TabIndex = 3;
            checkBox.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            checkBox.UncheckedState.BorderRadius = 0;
            checkBox.UncheckedState.BorderThickness = 0;
            checkBox.UncheckedState.FillColor = Color.FromArgb(125, 137, 149);
            // 
            // lbl_Role
            // 
            lbl_Role.BackColor = Color.Transparent;
            lbl_Role.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_Role.Location = new Point(240, 37);
            lbl_Role.Name = "lbl_Role";
            lbl_Role.Size = new Size(14, 30);
            lbl_Role.TabIndex = 4;
            lbl_Role.Text = "S";
            // 
            // ProfileControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(194, 211, 255);
            Controls.Add(lbl_Role);
            Controls.Add(checkBox);
            Controls.Add(lbl_Email);
            Controls.Add(lbl_Fullname);
            Controls.Add(pic_Avatar);
            Name = "ProfileControl";
            Size = new Size(270, 70);
            ((System.ComponentModel.ISupportInitialize)pic_Avatar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2CirclePictureBox pic_Avatar;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_Fullname;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_Email;
        private Guna.UI2.WinForms.Guna2CheckBox checkBox;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_Role;
    }
}
