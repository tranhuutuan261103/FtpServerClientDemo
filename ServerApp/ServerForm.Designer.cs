namespace ServerApp
{
    partial class ServerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerForm));
            label1 = new Label();
            txt_IP = new TextBox();
            txt_Port = new TextBox();
            btn_Start = new Button();
            richTextBox_Log = new RichTextBox();
            flowLayoutPanel_Account = new FlowLayoutPanel();
            panel1 = new Panel();
            btn_UnBan = new Guna.UI2.WinForms.Guna2CircleButton();
            btn_Ban = new Guna.UI2.WinForms.Guna2CircleButton();
            lbl_NumConnection = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txt_rootPath = new TextBox();
            pictureBox_ClearCommand = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_ClearCommand).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(144, 30);
            label1.Name = "label1";
            label1.Size = new Size(17, 28);
            label1.TabIndex = 1;
            label1.Text = ":";
            // 
            // txt_IP
            // 
            txt_IP.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txt_IP.Location = new Point(14, 24);
            txt_IP.Name = "txt_IP";
            txt_IP.PlaceholderText = "IP Address";
            txt_IP.Size = new Size(125, 34);
            txt_IP.TabIndex = 2;
            txt_IP.Text = "127.0.0.1";
            // 
            // txt_Port
            // 
            txt_Port.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txt_Port.Location = new Point(167, 24);
            txt_Port.Name = "txt_Port";
            txt_Port.PlaceholderText = "Port";
            txt_Port.Size = new Size(75, 34);
            txt_Port.TabIndex = 3;
            txt_Port.Text = "1234";
            txt_Port.TextChanged += txt_Port_TextChanged;
            txt_Port.KeyPress += txt_Port_KeyPress;
            // 
            // btn_Start
            // 
            btn_Start.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            btn_Start.Location = new Point(562, 24);
            btn_Start.Name = "btn_Start";
            btn_Start.Size = new Size(94, 34);
            btn_Start.TabIndex = 4;
            btn_Start.Text = "Start";
            btn_Start.UseVisualStyleBackColor = true;
            btn_Start.Click += btn_Start_Click;
            // 
            // richTextBox_Log
            // 
            richTextBox_Log.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBox_Log.BackColor = Color.White;
            richTextBox_Log.Location = new Point(12, 81);
            richTextBox_Log.Name = "richTextBox_Log";
            richTextBox_Log.ReadOnly = true;
            richTextBox_Log.Size = new Size(774, 435);
            richTextBox_Log.TabIndex = 5;
            richTextBox_Log.Text = "";
            // 
            // flowLayoutPanel_Account
            // 
            flowLayoutPanel_Account.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel_Account.BorderStyle = BorderStyle.FixedSingle;
            flowLayoutPanel_Account.Location = new Point(0, 51);
            flowLayoutPanel_Account.Name = "flowLayoutPanel_Account";
            flowLayoutPanel_Account.Size = new Size(290, 435);
            flowLayoutPanel_Account.TabIndex = 6;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panel1.Controls.Add(btn_UnBan);
            panel1.Controls.Add(btn_Ban);
            panel1.Controls.Add(lbl_NumConnection);
            panel1.Controls.Add(flowLayoutPanel_Account);
            panel1.Location = new Point(796, 30);
            panel1.Name = "panel1";
            panel1.Size = new Size(290, 486);
            panel1.TabIndex = 8;
            // 
            // btn_UnBan
            // 
            btn_UnBan.DisabledState.BorderColor = Color.DarkGray;
            btn_UnBan.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_UnBan.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_UnBan.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_UnBan.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btn_UnBan.ForeColor = Color.White;
            btn_UnBan.Location = new Point(191, 3);
            btn_UnBan.Name = "btn_UnBan";
            btn_UnBan.ShadowDecoration.CustomizableEdges = customizableEdges1;
            btn_UnBan.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btn_UnBan.Size = new Size(45, 45);
            btn_UnBan.TabIndex = 11;
            btn_UnBan.Text = "A";
            btn_UnBan.Click += btn_UnBan_Click;
            // 
            // btn_Ban
            // 
            btn_Ban.DisabledState.BorderColor = Color.DarkGray;
            btn_Ban.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_Ban.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_Ban.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_Ban.FillColor = Color.Red;
            btn_Ban.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btn_Ban.ForeColor = Color.White;
            btn_Ban.Location = new Point(242, 3);
            btn_Ban.Name = "btn_Ban";
            btn_Ban.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btn_Ban.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btn_Ban.Size = new Size(45, 45);
            btn_Ban.TabIndex = 10;
            btn_Ban.Text = "X";
            btn_Ban.Click += btn_Ban_Click;
            // 
            // lbl_NumConnection
            // 
            lbl_NumConnection.BackColor = Color.Transparent;
            lbl_NumConnection.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbl_NumConnection.Location = new Point(3, 8);
            lbl_NumConnection.Name = "lbl_NumConnection";
            lbl_NumConnection.Size = new Size(14, 30);
            lbl_NumConnection.TabIndex = 8;
            lbl_NumConnection.Text = "0";
            // 
            // txt_rootPath
            // 
            txt_rootPath.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txt_rootPath.Location = new Point(264, 24);
            txt_rootPath.Name = "txt_rootPath";
            txt_rootPath.PlaceholderText = "Enter root path";
            txt_rootPath.Size = new Size(277, 34);
            txt_rootPath.TabIndex = 9;
            txt_rootPath.Text = "D:\\FileServer";
            // 
            // pictureBox_ClearCommand
            // 
            pictureBox_ClearCommand.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox_ClearCommand.BackColor = SystemColors.ButtonShadow;
            pictureBox_ClearCommand.Image = (Image)resources.GetObject("pictureBox_ClearCommand.Image");
            pictureBox_ClearCommand.Location = new Point(749, 42);
            pictureBox_ClearCommand.Name = "pictureBox_ClearCommand";
            pictureBox_ClearCommand.Size = new Size(37, 37);
            pictureBox_ClearCommand.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_ClearCommand.TabIndex = 10;
            pictureBox_ClearCommand.TabStop = false;
            pictureBox_ClearCommand.Click += pictureBox_ClearCommand_Click;
            // 
            // ServerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1098, 528);
            Controls.Add(pictureBox_ClearCommand);
            Controls.Add(txt_rootPath);
            Controls.Add(panel1);
            Controls.Add(richTextBox_Log);
            Controls.Add(btn_Start);
            Controls.Add(txt_Port);
            Controls.Add(txt_IP);
            Controls.Add(label1);
            Name = "ServerForm";
            Text = "Server";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_ClearCommand).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private TextBox txt_IP;
        private TextBox txt_Port;
        private Button btn_Start;
        private RichTextBox richTextBox_Log;
        private FlowLayoutPanel flowLayoutPanel_Account;
        private Panel panel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_NumConnection;
        private Guna.UI2.WinForms.Guna2CircleButton btn_UnBan;
        private Guna.UI2.WinForms.Guna2CircleButton btn_Ban;
        private TextBox txt_rootPath;
        private PictureBox pictureBox_ClearCommand;
    }
}