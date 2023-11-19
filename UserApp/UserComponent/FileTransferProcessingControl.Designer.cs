namespace UserApp.UserComponent
{
    partial class FileTransferProcessingControl
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
            progressBar = new Guna.UI2.WinForms.Guna2ProgressBar();
            label_FileName = new Label();
            label_Status = new Label();
            label_TransferRate = new Label();
            label_FileTransferPercent = new Label();
            SuspendLayout();
            // 
            // progressBar
            // 
            progressBar.CustomizableEdges = customizableEdges1;
            progressBar.Location = new Point(50, 56);
            progressBar.Name = "progressBar";
            progressBar.ShadowDecoration.CustomizableEdges = customizableEdges2;
            progressBar.Size = new Size(280, 20);
            progressBar.TabIndex = 0;
            progressBar.Text = "guna2ProgressBar1";
            progressBar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // label_FileName
            // 
            label_FileName.AutoSize = true;
            label_FileName.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label_FileName.Location = new Point(50, 0);
            label_FileName.Name = "label_FileName";
            label_FileName.Size = new Size(95, 28);
            label_FileName.TabIndex = 1;
            label_FileName.Text = "File name";
            // 
            // label_Status
            // 
            label_Status.AutoSize = true;
            label_Status.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            label_Status.Location = new Point(50, 25);
            label_Status.Name = "label_Status";
            label_Status.Size = new Size(60, 25);
            label_Status.TabIndex = 2;
            label_Status.Text = "Status";
            // 
            // label_TransferRate
            // 
            label_TransferRate.AutoSize = true;
            label_TransferRate.Location = new Point(212, 30);
            label_TransferRate.Name = "label_TransferRate";
            label_TransferRate.Size = new Size(39, 20);
            label_TransferRate.TabIndex = 3;
            label_TransferRate.Text = "0 / 0";
            // 
            // label_FileTransferPercent
            // 
            label_FileTransferPercent.AutoSize = true;
            label_FileTransferPercent.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            label_FileTransferPercent.Location = new Point(5, 53);
            label_FileTransferPercent.Name = "label_FileTransferPercent";
            label_FileTransferPercent.Size = new Size(38, 23);
            label_FileTransferPercent.TabIndex = 4;
            label_FileTransferPercent.Text = "0 %";
            // 
            // FileTransferProcessingControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label_FileTransferPercent);
            Controls.Add(label_TransferRate);
            Controls.Add(label_Status);
            Controls.Add(label_FileName);
            Controls.Add(progressBar);
            Name = "FileTransferProcessingControl";
            Size = new Size(350, 90);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2ProgressBar progressBar;
        private Label label_FileName;
        private Label label_Status;
        private Label label_TransferRate;
        private Label label_FileTransferPercent;
    }
}
