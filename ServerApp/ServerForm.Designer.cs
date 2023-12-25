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
            label_IPAddress = new Label();
            label1 = new Label();
            txt_IP = new TextBox();
            txt_Port = new TextBox();
            btn_Start = new Button();
            richTextBox_Log = new RichTextBox();
            flowLayoutPanel_Account = new FlowLayoutPanel();
            lbl_User = new Label();
            panel1 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label_IPAddress
            // 
            label_IPAddress.AutoSize = true;
            label_IPAddress.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label_IPAddress.Location = new Point(12, 30);
            label_IPAddress.Name = "label_IPAddress";
            label_IPAddress.Size = new Size(114, 28);
            label_IPAddress.TabIndex = 0;
            label_IPAddress.Text = "IP Address:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(262, 30);
            label1.Name = "label1";
            label1.Size = new Size(17, 28);
            label1.TabIndex = 1;
            label1.Text = ":";
            // 
            // txt_IP
            // 
            txt_IP.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txt_IP.Location = new Point(132, 24);
            txt_IP.Name = "txt_IP";
            txt_IP.Size = new Size(125, 34);
            txt_IP.TabIndex = 2;
            txt_IP.Text = "127.0.0.1";
            // 
            // txt_Port
            // 
            txt_Port.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txt_Port.Location = new Point(285, 24);
            txt_Port.Name = "txt_Port";
            txt_Port.Size = new Size(75, 34);
            txt_Port.TabIndex = 3;
            txt_Port.Text = "1234";
            txt_Port.TextChanged += txt_Port_TextChanged;
            txt_Port.KeyPress += txt_Port_KeyPress;
            // 
            // btn_Start
            // 
            btn_Start.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            btn_Start.Location = new Point(388, 24);
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
            // lbl_User
            // 
            lbl_User.AutoSize = true;
            lbl_User.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_User.Location = new Point(13, 11);
            lbl_User.Name = "lbl_User";
            lbl_User.Size = new Size(67, 28);
            lbl_User.TabIndex = 7;
            lbl_User.Text = "Users:";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panel1.Controls.Add(lbl_User);
            panel1.Controls.Add(flowLayoutPanel_Account);
            panel1.Location = new Point(796, 30);
            panel1.Name = "panel1";
            panel1.Size = new Size(290, 486);
            panel1.TabIndex = 8;
            // 
            // ServerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1098, 528);
            Controls.Add(panel1);
            Controls.Add(richTextBox_Log);
            Controls.Add(btn_Start);
            Controls.Add(txt_Port);
            Controls.Add(txt_IP);
            Controls.Add(label1);
            Controls.Add(label_IPAddress);
            Name = "ServerForm";
            Text = "Server";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label_IPAddress;
        private Label label1;
        private TextBox txt_IP;
        private TextBox txt_Port;
        private Button btn_Start;
        private RichTextBox richTextBox_Log;
        private FlowLayoutPanel flowLayoutPanel_Account;
        private Label lbl_User;
        private Panel panel1;
    }
}