using MyClassLibrary;
using MyClassLibrary.Bean.File;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserApp.UI
{
    public partial class DetailFileForm : Form
    {
        private FileDetailVM _fileInforVM;
        public DetailFileForm(FileDetailVM fileInforVM)
        {
            InitializeComponent();
            _fileInforVM = fileInforVM;
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (_fileInforVM != null)
            {
                lbl_Name.Text = _fileInforVM.Name;
                lbl_OwnerData.Text = _fileInforVM.FileOwner;
                lbl_TypeData.Text = _fileInforVM.Type.ToString();
                //lbl_AccessTypeData.Text = _fileInforVM.AccessType.ToString();
                lbl_CreatedTimeData.Text = _fileInforVM.CreatedTime.ToString();
                FileManager fileManager = new FileManager();
                if (_fileInforVM.Type == FileInforType.Folder)
                {
                    lbl_SizeData.Text = "Unknown";
                } else
                {
                    lbl_SizeData.Text = fileManager.FileSizeToString(_fileInforVM.Length);
                }
            }
        }

        private void DetailFileForm_Load(object sender, EventArgs e)
        {

        }
    }
}
