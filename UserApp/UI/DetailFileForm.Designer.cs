namespace UserApp.UI
{
    partial class DetailFileForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            flowLayoutPanel1 = new FlowLayoutPanel();
            lbl_Name = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lbl_Owner = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lbl_OwnerData = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lbl_Type = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lbl_TypeData = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lbl_Size = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lbl_SizeData = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lbl_CreatedTime = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lbl_CreatedTimeData = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lbl_AccessAbility = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cbb_AccessAbility = new Guna.UI2.WinForms.Guna2ComboBox();
            btn_Oke = new Guna.UI2.WinForms.Guna2Button();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Location = new Point(12, 66);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(300, 325);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // lbl_Name
            // 
            lbl_Name.BackColor = Color.Transparent;
            lbl_Name.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_Name.Location = new Point(12, 3);
            lbl_Name.Name = "lbl_Name";
            lbl_Name.Size = new Size(80, 30);
            lbl_Name.TabIndex = 1;
            lbl_Name.Text = "Name ...";
            // 
            // lbl_Owner
            // 
            lbl_Owner.BackColor = Color.Transparent;
            lbl_Owner.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_Owner.Location = new Point(345, 60);
            lbl_Owner.Name = "lbl_Owner";
            lbl_Owner.Size = new Size(70, 30);
            lbl_Owner.TabIndex = 2;
            lbl_Owner.Text = "Owner:";
            // 
            // lbl_OwnerData
            // 
            lbl_OwnerData.BackColor = Color.Transparent;
            lbl_OwnerData.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbl_OwnerData.Location = new Point(345, 90);
            lbl_OwnerData.Name = "lbl_OwnerData";
            lbl_OwnerData.Size = new Size(47, 30);
            lbl_OwnerData.TabIndex = 3;
            lbl_OwnerData.Text = "Mine";
            // 
            // lbl_Type
            // 
            lbl_Type.BackColor = Color.Transparent;
            lbl_Type.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_Type.Location = new Point(345, 130);
            lbl_Type.Name = "lbl_Type";
            lbl_Type.Size = new Size(54, 30);
            lbl_Type.TabIndex = 4;
            lbl_Type.Text = "Type:";
            // 
            // lbl_TypeData
            // 
            lbl_TypeData.BackColor = Color.Transparent;
            lbl_TypeData.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbl_TypeData.Location = new Point(405, 130);
            lbl_TypeData.Name = "lbl_TypeData";
            lbl_TypeData.Size = new Size(33, 30);
            lbl_TypeData.TabIndex = 5;
            lbl_TypeData.Text = "File";
            // 
            // lbl_Size
            // 
            lbl_Size.BackColor = Color.Transparent;
            lbl_Size.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_Size.Location = new Point(501, 130);
            lbl_Size.Name = "lbl_Size";
            lbl_Size.Size = new Size(46, 30);
            lbl_Size.TabIndex = 6;
            lbl_Size.Text = "Size:";
            // 
            // lbl_SizeData
            // 
            lbl_SizeData.BackColor = Color.Transparent;
            lbl_SizeData.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbl_SizeData.Location = new Point(553, 130);
            lbl_SizeData.Name = "lbl_SizeData";
            lbl_SizeData.Size = new Size(30, 30);
            lbl_SizeData.TabIndex = 7;
            lbl_SizeData.Text = "0 B";
            // 
            // lbl_CreatedTime
            // 
            lbl_CreatedTime.BackColor = Color.Transparent;
            lbl_CreatedTime.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_CreatedTime.Location = new Point(345, 175);
            lbl_CreatedTime.Name = "lbl_CreatedTime";
            lbl_CreatedTime.Size = new Size(130, 30);
            lbl_CreatedTime.TabIndex = 8;
            lbl_CreatedTime.Text = "Created time:";
            // 
            // lbl_CreatedTimeData
            // 
            lbl_CreatedTimeData.BackColor = Color.Transparent;
            lbl_CreatedTimeData.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbl_CreatedTimeData.Location = new Point(345, 210);
            lbl_CreatedTimeData.Name = "lbl_CreatedTimeData";
            lbl_CreatedTimeData.Size = new Size(186, 30);
            lbl_CreatedTimeData.TabIndex = 9;
            lbl_CreatedTimeData.Text = "2023/12/12 12:00:00";
            // 
            // lbl_AccessAbility
            // 
            lbl_AccessAbility.BackColor = Color.Transparent;
            lbl_AccessAbility.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_AccessAbility.Location = new Point(345, 255);
            lbl_AccessAbility.Name = "lbl_AccessAbility";
            lbl_AccessAbility.Size = new Size(137, 30);
            lbl_AccessAbility.TabIndex = 10;
            lbl_AccessAbility.Text = "Access ability:";
            // 
            // cbb_AccessAbility
            // 
            cbb_AccessAbility.BackColor = Color.Transparent;
            cbb_AccessAbility.CustomizableEdges = customizableEdges1;
            cbb_AccessAbility.DrawMode = DrawMode.OwnerDrawFixed;
            cbb_AccessAbility.DropDownStyle = ComboBoxStyle.DropDownList;
            cbb_AccessAbility.FocusedColor = Color.FromArgb(94, 148, 255);
            cbb_AccessAbility.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cbb_AccessAbility.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            cbb_AccessAbility.ForeColor = Color.FromArgb(68, 88, 112);
            cbb_AccessAbility.ItemHeight = 30;
            cbb_AccessAbility.Location = new Point(345, 291);
            cbb_AccessAbility.Name = "cbb_AccessAbility";
            cbb_AccessAbility.ShadowDecoration.CustomizableEdges = customizableEdges2;
            cbb_AccessAbility.Size = new Size(300, 36);
            cbb_AccessAbility.TabIndex = 11;
            // 
            // btn_Oke
            // 
            btn_Oke.CustomizableEdges = customizableEdges3;
            btn_Oke.DisabledState.BorderColor = Color.DarkGray;
            btn_Oke.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_Oke.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_Oke.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_Oke.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btn_Oke.ForeColor = Color.White;
            btn_Oke.Location = new Point(427, 348);
            btn_Oke.Name = "btn_Oke";
            btn_Oke.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btn_Oke.Size = new Size(142, 43);
            btn_Oke.TabIndex = 12;
            btn_Oke.Text = "Oke";
            // 
            // DetailFileForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(682, 403);
            Controls.Add(btn_Oke);
            Controls.Add(cbb_AccessAbility);
            Controls.Add(lbl_AccessAbility);
            Controls.Add(lbl_CreatedTimeData);
            Controls.Add(lbl_CreatedTime);
            Controls.Add(lbl_SizeData);
            Controls.Add(lbl_Size);
            Controls.Add(lbl_TypeData);
            Controls.Add(lbl_Type);
            Controls.Add(lbl_OwnerData);
            Controls.Add(lbl_Owner);
            Controls.Add(lbl_Name);
            Controls.Add(flowLayoutPanel1);
            Name = "DetailFileForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "DetailForm";
            TopMost = true;
            Load += DetailFileForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_Name;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_Owner;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_OwnerData;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_Type;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_TypeData;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_Size;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_SizeData;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_CreatedTime;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_CreatedTimeData;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_AccessAbility;
        private Guna.UI2.WinForms.Guna2ComboBox cbb_AccessAbility;
        private Guna.UI2.WinForms.Guna2Button btn_Oke;
    }
}