namespace ServerApp.UserComponent
{
    partial class AccountControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountControl));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pic_Avatar = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            lbl_FullName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lbl_Email = new Guna.UI2.WinForms.Guna2HtmlLabel();
            checkBox = new Guna.UI2.WinForms.Guna2CustomCheckBox();
            pic_Status = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            ((System.ComponentModel.ISupportInitialize)pic_Avatar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic_Status).BeginInit();
            SuspendLayout();
            // 
            // pic_Avatar
            // 
            pic_Avatar.Image = (Image)resources.GetObject("pic_Avatar.Image");
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
            // lbl_FullName
            // 
            lbl_FullName.AutoSize = false;
            lbl_FullName.BackColor = Color.Transparent;
            lbl_FullName.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_FullName.Location = new Point(61, 3);
            lbl_FullName.Name = "lbl_FullName";
            lbl_FullName.Size = new Size(165, 27);
            lbl_FullName.TabIndex = 1;
            lbl_FullName.Text = "Full name";
            // 
            // lbl_Email
            // 
            lbl_Email.BackColor = Color.Transparent;
            lbl_Email.Location = new Point(61, 36);
            lbl_Email.Name = "lbl_Email";
            lbl_Email.Size = new Size(40, 22);
            lbl_Email.TabIndex = 2;
            lbl_Email.Text = "Email";
            // 
            // checkBox
            // 
            checkBox.CheckedState.BorderColor = Color.FromArgb(94, 148, 255);
            checkBox.CheckedState.BorderRadius = 2;
            checkBox.CheckedState.BorderThickness = 0;
            checkBox.CheckedState.FillColor = Color.FromArgb(94, 148, 255);
            checkBox.CustomizableEdges = customizableEdges2;
            checkBox.Location = new Point(232, 20);
            checkBox.Name = "checkBox";
            checkBox.ShadowDecoration.CustomizableEdges = customizableEdges3;
            checkBox.Size = new Size(26, 29);
            checkBox.TabIndex = 3;
            checkBox.Text = "guna2CustomCheckBox1";
            checkBox.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            checkBox.UncheckedState.BorderRadius = 2;
            checkBox.UncheckedState.BorderThickness = 0;
            checkBox.UncheckedState.FillColor = Color.FromArgb(125, 137, 149);
            // 
            // pic_Status
            // 
            pic_Status.BackColor = Color.Transparent;
            pic_Status.FillColor = Color.Red;
            pic_Status.ImageRotate = 0F;
            pic_Status.Location = new Point(37, 43);
            pic_Status.Name = "pic_Status";
            pic_Status.ShadowDecoration.CustomizableEdges = customizableEdges4;
            pic_Status.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            pic_Status.Size = new Size(16, 16);
            pic_Status.TabIndex = 4;
            pic_Status.TabStop = false;
            // 
            // AccountControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pic_Status);
            Controls.Add(checkBox);
            Controls.Add(lbl_Email);
            Controls.Add(lbl_FullName);
            Controls.Add(pic_Avatar);
            Name = "AccountControl";
            Size = new Size(265, 70);
            ((System.ComponentModel.ISupportInitialize)pic_Avatar).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic_Status).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2CirclePictureBox pic_Avatar;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_FullName;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_Email;
        private Guna.UI2.WinForms.Guna2CustomCheckBox checkBox;
        private Guna.UI2.WinForms.Guna2CirclePictureBox pic_Status;
    }
}
