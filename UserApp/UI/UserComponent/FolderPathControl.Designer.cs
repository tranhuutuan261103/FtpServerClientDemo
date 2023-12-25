namespace UserApp.UI.UserComponent
{
    partial class FolderPathControl
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
            flowLayoutPanel = new FlowLayoutPanel();
            lbl_MyDrive = new Guna.UI2.WinForms.Guna2HtmlLabel();
            SuspendLayout();
            // 
            // flowLayoutPanel
            // 
            flowLayoutPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel.Location = new Point(91, 5);
            flowLayoutPanel.Name = "flowLayoutPanel";
            flowLayoutPanel.Size = new Size(539, 40);
            flowLayoutPanel.TabIndex = 0;
            // 
            // lbl_MyDrive
            // 
            lbl_MyDrive.BackColor = Color.Transparent;
            lbl_MyDrive.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_MyDrive.Location = new Point(3, 9);
            lbl_MyDrive.Name = "lbl_MyDrive";
            lbl_MyDrive.Size = new Size(82, 30);
            lbl_MyDrive.TabIndex = 1;
            lbl_MyDrive.Text = "<span style=\"cursor: pointer;\">My drive</span>";
            lbl_MyDrive.Click += lbl_MyDrive_Click;
            lbl_MyDrive.MouseEnter += lbl_MyDrive_MouseEnter;
            lbl_MyDrive.MouseLeave += lbl_MyDrive_MouseLeave;
            // 
            // FolderPathControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lbl_MyDrive);
            Controls.Add(flowLayoutPanel);
            Name = "FolderPathControl";
            Size = new Size(650, 50);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_MyDrive;
    }
}
