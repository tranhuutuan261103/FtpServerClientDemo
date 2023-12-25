using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserApp.DTO;

namespace UserApp.UI.UserComponent
{
    public partial class FolderItemControl : UserControl
    {
        private FolderItemVM folder;
        public FolderItemControl(FolderItemVM folder, ClickFolderItemControl clickFolderItemControl)
        {
            InitializeComponent();
            this.folder = folder;
            ClickFolderItemControlEvent += clickFolderItemControl;
            UpdateUI();
        }

        private void UpdateUI()
        {
            lbl_NameFolder.Text = folder.NameFolder;
            int width = lbl_NameFolder.Location.X + lbl_NameFolder.Width;
            this.Width = width;
        }

        public delegate void ClickFolderItemControl(FolderItemVM folderItemVM);
        public event ClickFolderItemControl ClickFolderItemControlEvent;

        private void lbl_NameFolder_Click(object sender, EventArgs e)
        {
            lbl_NameFolder.ForeColor = Color.Blue;
            ClickFolderItemControlEvent(folder);
        }

        public FolderItemVM GetFolderItemVM()
        {
            return folder;
        }

        private void lbl_NameFolder_MouseEnter(object sender, EventArgs e)
        {
            lbl_NameFolder.ForeColor = Color.Blue;
        }

        private void lbl_NameFolder_MouseLeave(object sender, EventArgs e)
        {
            lbl_NameFolder.ForeColor = Color.Black;
        }
    }
}
