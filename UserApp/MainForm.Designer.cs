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
            grid_FileAndFolder = new FlowLayoutPanel();
            flowLayoutPanel_ListProcessing = new FlowLayoutPanel();
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
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1094, 450);
            Controls.Add(flowLayoutPanel_ListProcessing);
            Controls.Add(grid_FileAndFolder);
            Name = "MainForm";
            Text = "Transfer data";
            Load += MainForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel grid_FileAndFolder;
        private FlowLayoutPanel flowLayoutPanel_ListProcessing;
    }
}