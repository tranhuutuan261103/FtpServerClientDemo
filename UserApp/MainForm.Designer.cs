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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            grid_FileAndFolder = new FlowLayoutPanel();
            btn_Back = new Guna.UI2.WinForms.Guna2Button();
            btn_New = new Guna.UI2.WinForms.Guna2Button();
            guna2ContextMenuStrip_btnNew = new Guna.UI2.WinForms.Guna2ContextMenuStrip();
            ToolStripMenuItem_NewFolder = new ToolStripMenuItem();
            ToolStripMenuItem_UploadFile = new ToolStripMenuItem();
            ToolStripMenuItem_UploadFolder = new ToolStripMenuItem();
            guna2Elipse_ForMenuStrip = new Guna.UI2.WinForms.Guna2Elipse(components);
            guna2Elipse_Panel = new Guna.UI2.WinForms.Guna2Elipse(components);
            btn_TransferInfor = new Guna.UI2.WinForms.Guna2CircleButton();
            guna2TabControl1 = new Guna.UI2.WinForms.Guna2TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            tabPage4 = new TabPage();
            guna2ContainerControl_TransferInfor = new Guna.UI2.WinForms.Guna2ContainerControl();
            flowLayoutPanel_ListProcessing = new FlowLayoutPanel();
            guna2ContextMenuStrip_btnNew.SuspendLayout();
            guna2TabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            guna2ContainerControl_TransferInfor.SuspendLayout();
            SuspendLayout();
            // 
            // grid_FileAndFolder
            // 
            grid_FileAndFolder.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grid_FileAndFolder.AutoScroll = true;
            grid_FileAndFolder.BackColor = Color.White;
            grid_FileAndFolder.Location = new Point(37, 61);
            grid_FileAndFolder.Name = "grid_FileAndFolder";
            grid_FileAndFolder.Padding = new Padding(30);
            grid_FileAndFolder.Size = new Size(834, 351);
            grid_FileAndFolder.TabIndex = 0;
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
            btn_Back.Location = new Point(37, 6);
            btn_Back.Name = "btn_Back";
            btn_Back.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btn_Back.Size = new Size(91, 34);
            btn_Back.TabIndex = 2;
            btn_Back.Text = "Back";
            btn_Back.Click += btn_Back_Click;
            // 
            // btn_New
            // 
            btn_New.BackColor = Color.Transparent;
            btn_New.BorderRadius = 10;
            btn_New.ContextMenuStrip = guna2ContextMenuStrip_btnNew;
            btn_New.Cursor = Cursors.Hand;
            btn_New.CustomizableEdges = customizableEdges3;
            btn_New.DisabledState.BorderColor = Color.DarkGray;
            btn_New.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_New.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_New.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_New.FillColor = Color.White;
            btn_New.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btn_New.ForeColor = Color.Black;
            btn_New.HoverState.FillColor = Color.FromArgb(51, 255, 51);
            btn_New.Location = new Point(12, 12);
            btn_New.Name = "btn_New";
            btn_New.ShadowDecoration.BorderRadius = 10;
            btn_New.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btn_New.ShadowDecoration.Enabled = true;
            btn_New.ShadowDecoration.Shadow = new Padding(0, 0, 5, 5);
            btn_New.Size = new Size(93, 63);
            btn_New.TabIndex = 3;
            btn_New.Text = "Mới";
            btn_New.Click += btn_Upload_Click;
            // 
            // guna2ContextMenuStrip_btnNew
            // 
            guna2ContextMenuStrip_btnNew.ImageScalingSize = new Size(20, 20);
            guna2ContextMenuStrip_btnNew.Items.AddRange(new ToolStripItem[] { ToolStripMenuItem_NewFolder, ToolStripMenuItem_UploadFile, ToolStripMenuItem_UploadFolder });
            guna2ContextMenuStrip_btnNew.Name = "guna2ContextMenuStrip_btnNew";
            guna2ContextMenuStrip_btnNew.RenderStyle.ArrowColor = Color.FromArgb(151, 143, 255);
            guna2ContextMenuStrip_btnNew.RenderStyle.BorderColor = Color.Gainsboro;
            guna2ContextMenuStrip_btnNew.RenderStyle.ColorTable = null;
            guna2ContextMenuStrip_btnNew.RenderStyle.RoundedEdges = true;
            guna2ContextMenuStrip_btnNew.RenderStyle.SelectionArrowColor = Color.White;
            guna2ContextMenuStrip_btnNew.RenderStyle.SelectionBackColor = Color.FromArgb(100, 88, 255);
            guna2ContextMenuStrip_btnNew.RenderStyle.SelectionForeColor = Color.White;
            guna2ContextMenuStrip_btnNew.RenderStyle.SeparatorColor = Color.Gainsboro;
            guna2ContextMenuStrip_btnNew.RenderStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            guna2ContextMenuStrip_btnNew.Size = new Size(181, 118);
            // 
            // ToolStripMenuItem_NewFolder
            // 
            ToolStripMenuItem_NewFolder.Name = "ToolStripMenuItem_NewFolder";
            ToolStripMenuItem_NewFolder.Padding = new Padding(0, 8, 0, 8);
            ToolStripMenuItem_NewFolder.Size = new Size(180, 38);
            ToolStripMenuItem_NewFolder.Text = "Thư mục mới";
            // 
            // ToolStripMenuItem_UploadFile
            // 
            ToolStripMenuItem_UploadFile.Name = "ToolStripMenuItem_UploadFile";
            ToolStripMenuItem_UploadFile.Padding = new Padding(0, 8, 0, 8);
            ToolStripMenuItem_UploadFile.Size = new Size(180, 38);
            ToolStripMenuItem_UploadFile.Text = "Tải tệp lên";
            ToolStripMenuItem_UploadFile.Click += ToolStripMenuItem_UploadFile_Click;
            // 
            // ToolStripMenuItem_UploadFolder
            // 
            ToolStripMenuItem_UploadFolder.Name = "ToolStripMenuItem_UploadFolder";
            ToolStripMenuItem_UploadFolder.Padding = new Padding(0, 8, 0, 8);
            ToolStripMenuItem_UploadFolder.Size = new Size(180, 38);
            ToolStripMenuItem_UploadFolder.Text = "Tải thư mục lên";
            ToolStripMenuItem_UploadFolder.Click += ToolStripMenuItem_UploadFolder_Click;
            // 
            // guna2Elipse_ForMenuStrip
            // 
            guna2Elipse_ForMenuStrip.BorderRadius = 12;
            guna2Elipse_ForMenuStrip.TargetControl = guna2ContextMenuStrip_btnNew;
            // 
            // guna2Elipse_Panel
            // 
            guna2Elipse_Panel.BorderRadius = 48;
            guna2Elipse_Panel.TargetControl = grid_FileAndFolder;
            // 
            // btn_TransferInfor
            // 
            btn_TransferInfor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_TransferInfor.DisabledState.BorderColor = Color.DarkGray;
            btn_TransferInfor.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_TransferInfor.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_TransferInfor.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_TransferInfor.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            btn_TransferInfor.ForeColor = Color.White;
            btn_TransferInfor.Location = new Point(1005, 12);
            btn_TransferInfor.Name = "btn_TransferInfor";
            btn_TransferInfor.ShadowDecoration.CustomizableEdges = customizableEdges5;
            btn_TransferInfor.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btn_TransferInfor.Size = new Size(50, 50);
            btn_TransferInfor.TabIndex = 4;
            btn_TransferInfor.Text = "!";
            btn_TransferInfor.MouseClick += btn_TransferInfor_MouseClick;
            // 
            // guna2TabControl1
            // 
            guna2TabControl1.Alignment = TabAlignment.Left;
            guna2TabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            guna2TabControl1.Controls.Add(tabPage1);
            guna2TabControl1.Controls.Add(tabPage2);
            guna2TabControl1.Controls.Add(tabPage3);
            guna2TabControl1.Controls.Add(tabPage4);
            guna2TabControl1.ItemSize = new Size(180, 40);
            guna2TabControl1.Location = new Point(0, 81);
            guna2TabControl1.Name = "guna2TabControl1";
            guna2TabControl1.SelectedIndex = 0;
            guna2TabControl1.Size = new Size(1094, 447);
            guna2TabControl1.TabButtonHoverState.BorderColor = Color.Empty;
            guna2TabControl1.TabButtonHoverState.FillColor = Color.FromArgb(40, 52, 70);
            guna2TabControl1.TabButtonHoverState.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Regular, GraphicsUnit.Point);
            guna2TabControl1.TabButtonHoverState.ForeColor = Color.White;
            guna2TabControl1.TabButtonHoverState.InnerColor = Color.FromArgb(40, 52, 70);
            guna2TabControl1.TabButtonIdleState.BorderColor = Color.Empty;
            guna2TabControl1.TabButtonIdleState.FillColor = Color.FromArgb(33, 42, 57);
            guna2TabControl1.TabButtonIdleState.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Regular, GraphicsUnit.Point);
            guna2TabControl1.TabButtonIdleState.ForeColor = Color.FromArgb(156, 160, 167);
            guna2TabControl1.TabButtonIdleState.InnerColor = Color.FromArgb(33, 42, 57);
            guna2TabControl1.TabButtonSelectedState.BorderColor = Color.Empty;
            guna2TabControl1.TabButtonSelectedState.FillColor = Color.FromArgb(29, 37, 49);
            guna2TabControl1.TabButtonSelectedState.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Regular, GraphicsUnit.Point);
            guna2TabControl1.TabButtonSelectedState.ForeColor = Color.White;
            guna2TabControl1.TabButtonSelectedState.InnerColor = Color.FromArgb(76, 132, 255);
            guna2TabControl1.TabButtonSize = new Size(180, 40);
            guna2TabControl1.TabIndex = 5;
            guna2TabControl1.TabMenuBackColor = Color.FromArgb(233, 244, 255);
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.FromArgb(233, 244, 255);
            tabPage1.Controls.Add(btn_Back);
            tabPage1.Controls.Add(grid_FileAndFolder);
            tabPage1.ForeColor = SystemColors.ControlDarkDark;
            tabPage1.Location = new Point(184, 4);
            tabPage1.Margin = new Padding(3, 30, 3, 3);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(906, 439);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Drive của tôi";
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(184, 4);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(906, 439);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Được chia sẻ với tôi";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(184, 4);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(906, 439);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Có gắn dấu sao";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            tabPage4.Location = new Point(184, 4);
            tabPage4.Name = "tabPage4";
            tabPage4.Size = new Size(906, 439);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Thông tin tài khoản";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // guna2ContainerControl_TransferInfor
            // 
            guna2ContainerControl_TransferInfor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            guna2ContainerControl_TransferInfor.BackColor = Color.Transparent;
            guna2ContainerControl_TransferInfor.BorderRadius = 20;
            guna2ContainerControl_TransferInfor.Controls.Add(flowLayoutPanel_ListProcessing);
            guna2ContainerControl_TransferInfor.CustomizableEdges = customizableEdges6;
            guna2ContainerControl_TransferInfor.FillColor = Color.FromArgb(255, 255, 128);
            guna2ContainerControl_TransferInfor.Location = new Point(805, 68);
            guna2ContainerControl_TransferInfor.Name = "guna2ContainerControl_TransferInfor";
            guna2ContainerControl_TransferInfor.ShadowDecoration.BorderRadius = 20;
            guna2ContainerControl_TransferInfor.ShadowDecoration.CustomizableEdges = customizableEdges7;
            guna2ContainerControl_TransferInfor.ShadowDecoration.Enabled = true;
            guna2ContainerControl_TransferInfor.ShadowDecoration.Shadow = new Padding(0, 0, 5, 5);
            guna2ContainerControl_TransferInfor.Size = new Size(250, 250);
            guna2ContainerControl_TransferInfor.TabIndex = 6;
            guna2ContainerControl_TransferInfor.Text = "guna2ContainerControl1";
            guna2ContainerControl_TransferInfor.Visible = false;
            // 
            // flowLayoutPanel_ListProcessing
            // 
            flowLayoutPanel_ListProcessing.AutoScroll = true;
            flowLayoutPanel_ListProcessing.Dock = DockStyle.Fill;
            flowLayoutPanel_ListProcessing.Location = new Point(0, 0);
            flowLayoutPanel_ListProcessing.Name = "flowLayoutPanel_ListProcessing";
            flowLayoutPanel_ListProcessing.Size = new Size(250, 250);
            flowLayoutPanel_ListProcessing.TabIndex = 1;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(233, 244, 255);
            ClientSize = new Size(1094, 528);
            Controls.Add(guna2ContainerControl_TransferInfor);
            Controls.Add(guna2TabControl1);
            Controls.Add(btn_TransferInfor);
            Controls.Add(btn_New);
            Name = "MainForm";
            Text = "Transfer data";
            FormClosed += MainForm_FormClosed;
            Load += MainForm_Load;
            guna2ContextMenuStrip_btnNew.ResumeLayout(false);
            guna2TabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            guna2ContainerControl_TransferInfor.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel grid_FileAndFolder;
        private Guna.UI2.WinForms.Guna2Button btn_Back;
        private Guna.UI2.WinForms.Guna2Button btn_New;
        private Guna.UI2.WinForms.Guna2ContextMenuStrip guna2ContextMenuStrip_btnNew;
        private ToolStripMenuItem ToolStripMenuItem_NewFolder;
        private ToolStripMenuItem ToolStripMenuItem_UploadFile;
        private ToolStripMenuItem ToolStripMenuItem_UploadFolder;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse_ForMenuStrip;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse_Panel;
        private Guna.UI2.WinForms.Guna2CircleButton btn_TransferInfor;
        private Guna.UI2.WinForms.Guna2TabControl guna2TabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Guna.UI2.WinForms.Guna2ContainerControl guna2ContainerControl_TransferInfor;
        private FlowLayoutPanel flowLayoutPanel_ListProcessing;
        private TabPage tabPage3;
        private TabPage tabPage4;
    }
}