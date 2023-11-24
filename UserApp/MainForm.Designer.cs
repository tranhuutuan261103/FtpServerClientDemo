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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            grid_FileAndFolder = new FlowLayoutPanel();
            flowLayoutPanel_ListProcessing = new FlowLayoutPanel();
            btn_Back = new Guna.UI2.WinForms.Guna2Button();
            btn_Upload = new Guna.UI2.WinForms.Guna2Button();
            btn_UploadFolder = new Guna.UI2.WinForms.Guna2Button();
            SuspendLayout();
            // 
            // grid_FileAndFolder
            // 
            grid_FileAndFolder.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grid_FileAndFolder.AutoScroll = true;
            grid_FileAndFolder.Location = new Point(106, 66);
            grid_FileAndFolder.Name = "grid_FileAndFolder";
            grid_FileAndFolder.Size = new Size(602, 346);
            grid_FileAndFolder.TabIndex = 0;
            // 
            // flowLayoutPanel_ListProcessing
            // 
            flowLayoutPanel_ListProcessing.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            flowLayoutPanel_ListProcessing.AutoScroll = true;
            flowLayoutPanel_ListProcessing.Location = new Point(714, 66);
            flowLayoutPanel_ListProcessing.Name = "flowLayoutPanel_ListProcessing";
            flowLayoutPanel_ListProcessing.Size = new Size(356, 346);
            flowLayoutPanel_ListProcessing.TabIndex = 1;
            // 
            // btn_Back
            // 
            btn_Back.CustomizableEdges = customizableEdges1;
            btn_Back.DisabledState.BorderColor = Color.DarkGray;
            btn_Back.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_Back.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_Back.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_Back.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btn_Back.ForeColor = Color.White;
            btn_Back.Location = new Point(106, 12);
            btn_Back.Name = "btn_Back";
            btn_Back.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btn_Back.Size = new Size(91, 34);
            btn_Back.TabIndex = 2;
            btn_Back.Text = "Back";
            btn_Back.Click += btn_Back_Click;
            // 
            // btn_Upload
            // 
            btn_Upload.CustomizableEdges = customizableEdges3;
            btn_Upload.DisabledState.BorderColor = Color.DarkGray;
            btn_Upload.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_Upload.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_Upload.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_Upload.FillColor = Color.FromArgb(0, 192, 0);
            btn_Upload.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btn_Upload.ForeColor = Color.White;
            btn_Upload.Location = new Point(12, 66);
            btn_Upload.Name = "btn_Upload";
            btn_Upload.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btn_Upload.Size = new Size(85, 35);
            btn_Upload.TabIndex = 3;
            btn_Upload.Text = "New";
            btn_Upload.Click += btn_Upload_Click;
            // 
            // btn_UploadFolder
            // 
            btn_UploadFolder.CustomizableEdges = customizableEdges5;
            btn_UploadFolder.DisabledState.BorderColor = Color.DarkGray;
            btn_UploadFolder.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_UploadFolder.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_UploadFolder.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_UploadFolder.FillColor = Color.FromArgb(0, 192, 0);
            btn_UploadFolder.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btn_UploadFolder.ForeColor = Color.White;
            btn_UploadFolder.Location = new Point(12, 119);
            btn_UploadFolder.Name = "btn_UploadFolder";
            btn_UploadFolder.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btn_UploadFolder.Size = new Size(85, 65);
            btn_UploadFolder.TabIndex = 4;
            btn_UploadFolder.Text = "Upload folder";
            btn_UploadFolder.Click += btn_UploadFolder_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1094, 450);
            Controls.Add(btn_UploadFolder);
            Controls.Add(btn_Upload);
            Controls.Add(btn_Back);
            Controls.Add(flowLayoutPanel_ListProcessing);
            Controls.Add(grid_FileAndFolder);
            Name = "MainForm";
            Text = "Transfer data";
            FormClosed += MainForm_FormClosed;
            Load += MainForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel grid_FileAndFolder;
        private FlowLayoutPanel flowLayoutPanel_ListProcessing;
        private Guna.UI2.WinForms.Guna2Button btn_Back;
        private Guna.UI2.WinForms.Guna2Button btn_Upload;
        private Guna.UI2.WinForms.Guna2Button btn_UploadFolder;
    }
}