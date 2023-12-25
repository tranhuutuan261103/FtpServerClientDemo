namespace UserApp.UI
{
    partial class AccountForm
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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountForm));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panel_Container = new Panel();
            controlBox_Close = new Guna.UI2.WinForms.Guna2ControlBox();
            guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(components);
            guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            guna2ShadowForm1 = new Guna.UI2.WinForms.Guna2ShadowForm(components);
            guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            ((System.ComponentModel.ISupportInitialize)guna2PictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel_Container
            // 
            panel_Container.Dock = DockStyle.Bottom;
            panel_Container.Location = new Point(0, 140);
            panel_Container.Name = "panel_Container";
            panel_Container.Size = new Size(420, 380);
            panel_Container.TabIndex = 0;
            // 
            // controlBox_Close
            // 
            controlBox_Close.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            controlBox_Close.CustomizableEdges = customizableEdges1;
            controlBox_Close.FillColor = Color.FromArgb(139, 152, 166);
            controlBox_Close.IconColor = Color.White;
            controlBox_Close.Location = new Point(372, 12);
            controlBox_Close.Name = "controlBox_Close";
            controlBox_Close.ShadowDecoration.CustomizableEdges = customizableEdges2;
            controlBox_Close.Size = new Size(36, 36);
            controlBox_Close.TabIndex = 0;
            // 
            // guna2DragControl1
            // 
            guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            guna2DragControl1.DragStartTransparencyValue = 1D;
            guna2DragControl1.TargetControl = this;
            guna2DragControl1.UseTransparentDrag = true;
            // 
            // guna2ControlBox1
            // 
            guna2ControlBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            guna2ControlBox1.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            guna2ControlBox1.CustomizableEdges = customizableEdges5;
            guna2ControlBox1.FillColor = Color.FromArgb(139, 152, 166);
            guna2ControlBox1.IconColor = Color.White;
            guna2ControlBox1.Location = new Point(330, 12);
            guna2ControlBox1.Name = "guna2ControlBox1";
            guna2ControlBox1.ShadowDecoration.CustomizableEdges = customizableEdges6;
            guna2ControlBox1.Size = new Size(36, 36);
            guna2ControlBox1.TabIndex = 1;
            // 
            // guna2ShadowForm1
            // 
            guna2ShadowForm1.TargetForm = this;
            // 
            // guna2PictureBox1
            // 
            guna2PictureBox1.CustomizableEdges = customizableEdges3;
            guna2PictureBox1.Image = (Image)resources.GetObject("guna2PictureBox1.Image");
            guna2PictureBox1.ImageRotate = 0F;
            guna2PictureBox1.Location = new Point(145, 23);
            guna2PictureBox1.Name = "guna2PictureBox1";
            guna2PictureBox1.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2PictureBox1.Size = new Size(115, 115);
            guna2PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            guna2PictureBox1.TabIndex = 2;
            guna2PictureBox1.TabStop = false;
            // 
            // AccountForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 520);
            Controls.Add(guna2PictureBox1);
            Controls.Add(guna2ControlBox1);
            Controls.Add(controlBox_Close);
            Controls.Add(panel_Container);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AccountForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AccountForm";
            Load += AccountForm_Load;
            ((System.ComponentModel.ISupportInitialize)guna2PictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel_Container;
        private Guna.UI2.WinForms.Guna2ControlBox controlBox_Close;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2ShadowForm guna2ShadowForm1;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
    }
}