namespace UserApp.UI.UserComponent
{
    partial class FolderItemControl
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
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lbl_NameFolder = new Guna.UI2.WinForms.Guna2HtmlLabel();
            SuspendLayout();
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            guna2HtmlLabel1.Location = new Point(3, -4);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(22, 39);
            guna2HtmlLabel1.TabIndex = 0;
            guna2HtmlLabel1.Text = ">";
            // 
            // lbl_NameFolder
            // 
            lbl_NameFolder.BackColor = Color.Transparent;
            lbl_NameFolder.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_NameFolder.Location = new Point(31, 3);
            lbl_NameFolder.Name = "lbl_NameFolder";
            lbl_NameFolder.Size = new Size(99, 27);
            lbl_NameFolder.TabIndex = 1;
            lbl_NameFolder.Text = "Some thing";
            lbl_NameFolder.Click += lbl_NameFolder_Click;
            lbl_NameFolder.MouseEnter += lbl_NameFolder_MouseEnter;
            lbl_NameFolder.MouseLeave += lbl_NameFolder_MouseLeave;
            // 
            // FolderItemControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lbl_NameFolder);
            Controls.Add(guna2HtmlLabel1);
            Name = "FolderItemControl";
            Size = new Size(140, 36);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_NameFolder;
    }
}
