using MyClassLibrary.Common;
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

namespace UserApp.UserComponent
{
    public partial class FileControl : UserControl
    {
        public FileControl(FileControlClickHandler fileControlClickHandler)
        {
            FileControlClick = fileControlClickHandler;
            InitializeComponent();
        }

        public delegate void FileControlClickHandler(object sender, EventArgs e);
        public event FileControlClickHandler FileControlClick;

        private FileInfor infor = new FileInfor();

        private void FileControl_DoubleClick(object sender, EventArgs e)
        {
            FileControlRequest fileControlRequest = new FileControlRequest()
            {
                fileInfor = Infor,
                type = FileControlRequestType.ChangeFolder,
            };
            FileControlClick(fileControlRequest, e);
        }

        private void ToolStripMenuItem_Download_Click(object sender, EventArgs e)
        {
            FileControlRequest fileControlRequest = new FileControlRequest()
            {
                fileInfor = Infor,
                type = FileControlRequestType.Download,
            };
            FileControlClick(fileControlRequest, e);
        }

        private void ToolStripMenuItem_Rename_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Rename");
        }

        private void ToolStripMenuItem_Information_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Information");
        }

        private void ToolStripMenuItem_Remove_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Remove");
        }

        public FileInfor Infor
        {
            get { return infor; }
            set
            {
                infor = value;
                if (infor != null)
                {
                    // Safely update label_Name.
                    if (label_Name.IsHandleCreated && !label_Name.IsDisposed)
                    {
                        label_Name.Invoke((MethodInvoker)delegate { label_Name.Text = infor.Name; });
                    }
                    else
                    {
                        label_Name.Text = infor.Name;
                    }
                }
            }
        }
    }
}
