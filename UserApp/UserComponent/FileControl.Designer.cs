namespace UserApp.UserComponent
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
            guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
            label_Name = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2CMStrip = new Guna.UI2.WinForms.Guna2ContextMenuStrip();
            ToolStripMenuItem_Download = new ToolStripMenuItem();
            ToolStripMenuItem_Rename = new ToolStripMenuItem();
            ToolStripMenuItem_Information = new ToolStripMenuItem();
            ToolStripMenuItem_Remove = new ToolStripMenuItem();
            guna2Elipse2 = new Guna.UI2.WinForms.Guna2Elipse(components);
            guna2CMStrip.SuspendLayout();
            SuspendLayout();
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.BorderRadius = 12;
            guna2Elipse1.TargetControl = this;
            // 
            // label_Name
            // 
            label_Name.BackColor = Color.Transparent;
            label_Name.ContextMenuStrip = guna2CMStrip;
            label_Name.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            label_Name.Location = new Point(40, 3);
            label_Name.Name = "label_Name";
            label_Name.Size = new Size(60, 33);
            label_Name.TabIndex = 0;
            label_Name.Text = "name";
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
            guna2CMStrip.Size = new Size(221, 200);
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
            // FileControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ScrollBar;
            ContextMenuStrip = guna2CMStrip;
            Controls.Add(label_Name);
            Name = "FileControl";
            Size = new Size(250, 60);
            DoubleClick += FileControl_DoubleClick;
            guna2CMStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
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
    }
}
