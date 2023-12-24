namespace UserApp.UI.UserComponent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileTransferProcessingControl));
            progressBar = new Guna.UI2.WinForms.Guna2ProgressBar();
            label_FileName = new Label();
            label_Status = new Label();
            label_TransferRate = new Label();
            label_FileTransferPercent = new Label();
            delete_btn = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)delete_btn).BeginInit();
            SuspendLayout();
            // 
            // progressBar
            // 
            progressBar.CustomizableEdges = customizableEdges1;
            progressBar.Location = new Point(50, 56);
            progressBar.Name = "progressBar";
            progressBar.ShadowDecoration.CustomizableEdges = customizableEdges2;
            progressBar.Size = new Size(280, 10);
            progressBar.TabIndex = 0;
            progressBar.Text = "guna2ProgressBar1";
            progressBar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            progressBar.MouseEnter += FileTransferProcessingControl_MouseEnter;
            progressBar.MouseLeave += FileTransferProcessingControl_MouseLeave;
            // 
            // label_FileName
            // 
            label_FileName.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label_FileName.Location = new Point(50, 0);
            label_FileName.Name = "label_FileName";
            label_FileName.Size = new Size(244, 28);
            label_FileName.TabIndex = 1;
            label_FileName.Text = "File name";
            label_FileName.MouseEnter += FileTransferProcessingControl_MouseEnter;
            label_FileName.MouseLeave += FileTransferProcessingControl_MouseLeave;
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
            label_Status.MouseEnter += FileTransferProcessingControl_MouseEnter;
            label_Status.MouseLeave += FileTransferProcessingControl_MouseLeave;
            // 
            // label_TransferRate
            // 
            label_TransferRate.Location = new Point(161, 30);
            label_TransferRate.Name = "label_TransferRate";
            label_TransferRate.Size = new Size(174, 20);
            label_TransferRate.TabIndex = 3;
            label_TransferRate.Text = "0 / 0";
            label_TransferRate.TextAlign = ContentAlignment.MiddleRight;
            label_TransferRate.MouseEnter += FileTransferProcessingControl_MouseEnter;
            label_TransferRate.MouseLeave += FileTransferProcessingControl_MouseLeave;
            // 
            // label_FileTransferPercent
            // 
            label_FileTransferPercent.AutoSize = true;
            label_FileTransferPercent.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            label_FileTransferPercent.Location = new Point(5, 47);
            label_FileTransferPercent.Name = "label_FileTransferPercent";
            label_FileTransferPercent.Size = new Size(38, 23);
            label_FileTransferPercent.TabIndex = 4;
            label_FileTransferPercent.Text = "0 %";
            label_FileTransferPercent.MouseEnter += FileTransferProcessingControl_MouseEnter;
            label_FileTransferPercent.MouseLeave += FileTransferProcessingControl_MouseLeave;
            // 
            // delete_btn
            // 
            delete_btn.Image = (Image)resources.GetObject("delete_btn.Image");
            delete_btn.Location = new Point(300, 0);
            delete_btn.Name = "delete_btn";
            delete_btn.Size = new Size(30, 30);
            delete_btn.SizeMode = PictureBoxSizeMode.StretchImage;
            delete_btn.TabIndex = 5;
            delete_btn.TabStop = false;
            delete_btn.Click += delete_btn_Click;
            delete_btn.MouseEnter += delete_btn_MouseEnter;
            delete_btn.MouseLeave += delete_btn_MouseLeave;
            // 
            // FileTransferProcessingControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(delete_btn);
            Controls.Add(label_FileTransferPercent);
            Controls.Add(label_TransferRate);
            Controls.Add(label_Status);
            Controls.Add(label_FileName);
            Controls.Add(progressBar);
            Margin = new Padding(0);
            Name = "FileTransferProcessingControl";
            Size = new Size(370, 80);
            MouseEnter += FileTransferProcessingControl_MouseEnter;
            MouseLeave += FileTransferProcessingControl_MouseLeave;
            ((System.ComponentModel.ISupportInitialize)delete_btn).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2ProgressBar progressBar;
        private Label label_FileName;
        private Label label_Status;
        private Label label_TransferRate;
        private Label label_FileTransferPercent;
        private PictureBox delete_btn;
    }
}
