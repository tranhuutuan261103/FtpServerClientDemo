namespace UserApp.UI.UserComponent
{
    partial class FileControl
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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileControl));
            guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
            label_Name = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2CMStrip = new Guna.UI2.WinForms.Guna2ContextMenuStrip();
            ToolStripMenuItem_Download = new ToolStripMenuItem();
            ToolStripMenuItem_Rename = new ToolStripMenuItem();
            ToolStripMenuItem_Information = new ToolStripMenuItem();
            ToolStripMenuItem_Remove = new ToolStripMenuItem();
            guna2Elipse2 = new Guna.UI2.WinForms.Guna2Elipse(components);
            pictureBox_Folder = new PictureBox();
            pictureBox_File = new PictureBox();
            guna2Elipse3 = new Guna.UI2.WinForms.Guna2Elipse(components);
            guna2CirclePictureBox_MoreOption = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            pictureBox_MoreOption = new PictureBox();
            guna2CMStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_Folder).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_File).BeginInit();
            ((System.ComponentModel.ISupportInitialize)guna2CirclePictureBox_MoreOption).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_MoreOption).BeginInit();
            SuspendLayout();
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.BorderRadius = 12;
            guna2Elipse1.TargetControl = this;
            // 
            // label_Name
            // 
            label_Name.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_Name.AutoSize = false;
            label_Name.BackColor = Color.Transparent;
            label_Name.ContextMenuStrip = guna2CMStrip;
            label_Name.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            label_Name.Location = new Point(66, 0);
            label_Name.Name = "label_Name";
            label_Name.Size = new Size(221, 65);
            label_Name.TabIndex = 0;
            label_Name.Text = "name";
            label_Name.TextAlignment = ContentAlignment.MiddleLeft;
            label_Name.DoubleClick += FileControl_DoubleClick;
            // 
            // guna2CMStrip
            // 
            guna2CMStrip.ImageScalingSize = new Size(20, 20);
            guna2CMStrip.Items.AddRange(new ToolStripItem[] { ToolStripMenuItem_Download, ToolStripMenuItem_Rename, ToolStripMenuItem_Information, ToolStripMenuItem_Remove });
            guna2CMStrip.Name = "guna2CMStrip";
            guna2CMStrip.RenderStyle.ArrowColor = Color.FromArgb(151, 143, 255);
            guna2CMStrip.RenderStyle.BorderColor = Color.Gainsboro;
            guna2CMStrip.RenderStyle.ColorTable = null;
            guna2CMStrip.RenderStyle.RoundedEdges = true;
            guna2CMStrip.RenderStyle.SelectionArrowColor = Color.White;
            guna2CMStrip.RenderStyle.SelectionBackColor = Color.FromArgb(100, 88, 255);
            guna2CMStrip.RenderStyle.SelectionForeColor = Color.White;
            guna2CMStrip.RenderStyle.SeparatorColor = Color.Gainsboro;
            guna2CMStrip.RenderStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            guna2CMStrip.Size = new Size(221, 172);
            // 
            // ToolStripMenuItem_Download
            // 
            ToolStripMenuItem_Download.Name = "ToolStripMenuItem_Download";
            ToolStripMenuItem_Download.Padding = new Padding(0, 10, 0, 10);
            ToolStripMenuItem_Download.Size = new Size(220, 42);
            ToolStripMenuItem_Download.Text = "Tải xuống";
            ToolStripMenuItem_Download.Click += ToolStripMenuItem_Download_Click;
            // 
            // ToolStripMenuItem_Rename
            // 
            ToolStripMenuItem_Rename.Name = "ToolStripMenuItem_Rename";
            ToolStripMenuItem_Rename.Padding = new Padding(0, 10, 0, 10);
            ToolStripMenuItem_Rename.Size = new Size(220, 42);
            ToolStripMenuItem_Rename.Text = "Đổi tên";
            ToolStripMenuItem_Rename.Click += ToolStripMenuItem_Rename_Click;
            // 
            // ToolStripMenuItem_Information
            // 
            ToolStripMenuItem_Information.Name = "ToolStripMenuItem_Information";
            ToolStripMenuItem_Information.Padding = new Padding(0, 10, 0, 10);
            ToolStripMenuItem_Information.Size = new Size(220, 42);
            ToolStripMenuItem_Information.Text = "Thông tin";
            ToolStripMenuItem_Information.Click += ToolStripMenuItem_Information_Click;
            // 
            // ToolStripMenuItem_Remove
            // 
            ToolStripMenuItem_Remove.Name = "ToolStripMenuItem_Remove";
            ToolStripMenuItem_Remove.Padding = new Padding(0, 10, 0, 10);
            ToolStripMenuItem_Remove.Size = new Size(220, 42);
            ToolStripMenuItem_Remove.Text = "Chuyển vào thùng rác";
            ToolStripMenuItem_Remove.Click += ToolStripMenuItem_Remove_Click;
            // 
            // guna2Elipse2
            // 
            guna2Elipse2.BorderRadius = 12;
            guna2Elipse2.TargetControl = guna2CMStrip;
            // 
            // pictureBox_Folder
            // 
            pictureBox_Folder.BackColor = Color.Transparent;
            pictureBox_Folder.Image = (Image)resources.GetObject("pictureBox_Folder.Image");
            pictureBox_Folder.Location = new Point(15, 16);
            pictureBox_Folder.Name = "pictureBox_Folder";
            pictureBox_Folder.Size = new Size(30, 30);
            pictureBox_Folder.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_Folder.TabIndex = 1;
            pictureBox_Folder.TabStop = false;
            // 
            // pictureBox_File
            // 
            pictureBox_File.BackColor = Color.Transparent;
            pictureBox_File.Image = (Image)resources.GetObject("pictureBox_File.Image");
            pictureBox_File.Location = new Point(15, 16);
            pictureBox_File.Name = "pictureBox_File";
            pictureBox_File.Size = new Size(30, 30);
            pictureBox_File.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_File.TabIndex = 2;
            pictureBox_File.TabStop = false;
            pictureBox_File.Visible = false;
            // 
            // guna2Elipse3
            // 
            guna2Elipse3.BorderRadius = 30;
            // 
            // guna2CirclePictureBox_MoreOption
            // 
            guna2CirclePictureBox_MoreOption.Anchor = AnchorStyles.Right;
            guna2CirclePictureBox_MoreOption.BackColor = Color.Transparent;
            guna2CirclePictureBox_MoreOption.Cursor = Cursors.Hand;
            guna2CirclePictureBox_MoreOption.ImageRotate = 0F;
            guna2CirclePictureBox_MoreOption.Location = new Point(294, 9);
            guna2CirclePictureBox_MoreOption.Name = "guna2CirclePictureBox_MoreOption";
            guna2CirclePictureBox_MoreOption.ShadowDecoration.CustomizableEdges = customizableEdges1;
            guna2CirclePictureBox_MoreOption.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            guna2CirclePictureBox_MoreOption.Size = new Size(45, 45);
            guna2CirclePictureBox_MoreOption.TabIndex = 4;
            guna2CirclePictureBox_MoreOption.TabStop = false;
            guna2CirclePictureBox_MoreOption.Click += btn_MoreOption_Click;
            guna2CirclePictureBox_MoreOption.MouseEnter += pictureBox_MoreOption_MouseEnter;
            guna2CirclePictureBox_MoreOption.MouseLeave += pictureBox_MoreOption_MouseLeave;
            // 
            // pictureBox_MoreOption
            // 
            pictureBox_MoreOption.BackColor = Color.White;
            pictureBox_MoreOption.Cursor = Cursors.Hand;
            pictureBox_MoreOption.Image = (Image)resources.GetObject("pictureBox_MoreOption.Image");
            pictureBox_MoreOption.Location = new Point(302, 18);
            pictureBox_MoreOption.Name = "pictureBox_MoreOption";
            pictureBox_MoreOption.Size = new Size(30, 30);
            pictureBox_MoreOption.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_MoreOption.TabIndex = 5;
            pictureBox_MoreOption.TabStop = false;
            pictureBox_MoreOption.Click += btn_MoreOption_Click;
            pictureBox_MoreOption.MouseEnter += pictureBox_MoreOption_MouseEnter;
            pictureBox_MoreOption.MouseLeave += pictureBox_MoreOption_MouseLeave;
            // 
            // FileControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(194, 211, 255);
            ContextMenuStrip = guna2CMStrip;
            Controls.Add(pictureBox_MoreOption);
            Controls.Add(guna2CirclePictureBox_MoreOption);
            Controls.Add(pictureBox_File);
            Controls.Add(pictureBox_Folder);
            Controls.Add(label_Name);
            Name = "FileControl";
            Size = new Size(350, 65);
            DoubleClick += FileControl_DoubleClick;
            guna2CMStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox_Folder).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_File).EndInit();
            ((System.ComponentModel.ISupportInitialize)guna2CirclePictureBox_MoreOption).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_MoreOption).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2HtmlLabel label_Name;
        private Guna.UI2.WinForms.Guna2ContextMenuStrip guna2CMStrip;
        private ToolStripMenuItem ToolStripMenuItem_Download;
        private ToolStripMenuItem ToolStripMenuItem_Rename;
        private ToolStripMenuItem ToolStripMenuItem_Information;
        private ToolStripMenuItem ToolStripMenuItem_Remove;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse2;
        private PictureBox pictureBox_Folder;
        private PictureBox pictureBox_File;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse3;
        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox_MoreOption;
        private PictureBox pictureBox_MoreOption;
    }
}
