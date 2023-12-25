using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserApp.DTO;

namespace UserApp.UI.UserComponent
{
    public partial class FolderPathControl : UserControl
    {
        List<FolderItemVM> folderItemVMs = new List<FolderItemVM>();
        public FolderPathControl()
        {
            InitializeComponent();
            lbl_MyDrive.Cursor = Cursors.Hand;
            UpdateUI();
        }

        public void UpdateUI()
        {
            flowLayoutPanel.Controls.Clear();
            foreach (var item in folderItemVMs)
            {
                flowLayoutPanel.Controls.Add(new FolderItemControl(item, ClickFolderItemControlHandler));
            }
        }

        public delegate void ClickFolderItemControl(FolderItemVM folderItemVM);
        public event ClickFolderItemControl ClickFolderItemControlEvent;

        public void ClickFolderItemControlHandler(FolderItemVM folderItemVM)
        {
            ClickFolderItemControlEvent?.Invoke(folderItemVM);
        }

        private void lbl_MyDrive_Click(object sender, EventArgs e)
        {
            lbl_MyDrive.ForeColor = Color.Blue;
            ClickFolderItemControlEvent?.Invoke(new FolderItemVM() { IdFolder = "", NameFolder = "My Drive" });
        }

        public bool HasFolderItemVM(string idFolder)
        {
            foreach (var item in folderItemVMs)
            {
                if (item.IdFolder == idFolder)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddItem(FolderItemVM folderItemVM)
        {
            folderItemVMs.Add(folderItemVM);
            UpdateUI();
        }

        public void RemoveItem(string idFolder)
        {
            foreach (var item in folderItemVMs)
            {
                if (item.IdFolder == idFolder)
                {
                    folderItemVMs.RemoveRange(folderItemVMs.IndexOf(item) + 1, folderItemVMs.Count - folderItemVMs.IndexOf(item) - 1);
                    break;
                }
            }
            UpdateUI();
        }

        public void Reset()
        {
            folderItemVMs.Clear();
            UpdateUI();
        }

        private void lbl_MyDrive_MouseEnter(object sender, EventArgs e)
        {
            lbl_MyDrive.ForeColor = Color.Blue;
        }

        private void lbl_MyDrive_MouseLeave(object sender, EventArgs e)
        {
            lbl_MyDrive.ForeColor = Color.Black;
        }
    }
}
