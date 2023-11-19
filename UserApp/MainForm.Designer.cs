namespace UserApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            grid_FileAndFolder = new FlowLayoutPanel();
            progressDownload = new Guna.UI2.WinForms.Guna2ProgressBar();
            label1 = new Label();
            SuspendLayout();
            // 
            // grid_FileAndFolder
            // 
            grid_FileAndFolder.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grid_FileAndFolder.Location = new Point(122, 66);
            grid_FileAndFolder.Name = "grid_FileAndFolder";
            grid_FileAndFolder.Size = new Size(636, 346);
            grid_FileAndFolder.TabIndex = 0;
            // 
            // progressDownload
            // 
            progressDownload.CustomizableEdges = customizableEdges1;
            progressDownload.Location = new Point(383, 12);
            progressDownload.Name = "progressDownload";
            progressDownload.ShadowDecoration.CustomizableEdges = customizableEdges2;
            progressDownload.Size = new Size(375, 38);
            progressDownload.TabIndex = 1;
            progressDownload.Text = "guna2ProgressBar1";
            progressDownload.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(313, 21);
            label1.Name = "label1";
            label1.Size = new Size(33, 20);
            label1.TabIndex = 2;
            label1.Text = "0 %";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(progressDownload);
            Controls.Add(grid_FileAndFolder);
            Name = "MainForm";
            Text = "Transfer data";
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel grid_FileAndFolder;
        private Guna.UI2.WinForms.Guna2ProgressBar progressDownload;
        private Label label1;
    }
}