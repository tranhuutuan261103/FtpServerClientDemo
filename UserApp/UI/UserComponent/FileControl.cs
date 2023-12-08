using MyClassLibrary.Common;
using Newtonsoft.Json.Linq;
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
    public partial class FileControl : UserControl
    {
        public FileControl(FileInfor item, FileControlClickHandler fileControlClickHandler)
        {
            FileControlClick = fileControlClickHandler;
            infor = item;
            InitializeComponent();
            UpdateInforUI(item);
        }

        public delegate void FileControlClickHandler(object sender, EventArgs e);
        public event FileControlClickHandler FileControlClick;

        private FileInfor infor;

        private void FileControl_DoubleClick(object sender, EventArgs e)
        {
            FileControlRequest fileControlRequest = new FileControlRequest()
            {
                fileInfor = infor,
                type = FileControlRequestType.ChangeFolder,
            };
            FileControlClick(fileControlRequest, e);
        }

        private void ToolStripMenuItem_Download_Click(object sender, EventArgs e)
        {
            FileControlRequest fileControlRequest = new FileControlRequest()
            {
                fileInfor = infor,
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

        public void UpdateInforUI(FileInfor fileInfor)
        {
            infor = fileInfor;
            if (infor != null)
            {
                // Safely update label_Name.
                if (label_Name.IsHandleCreated && !label_Name.IsDisposed)
                {
                    label_Name.Invoke((MethodInvoker)delegate { label_Name.Text = GetSuitString(infor.Name, 15); });
                }
                else
                {
                    label_Name.Text = GetSuitString(infor.Name, 15);
                }

                // Update picturebox.
                if (infor.IsDirectory == true)
                {
                    pictureBox_Folder.Visible = true;
                    pictureBox_File.Visible = false;
                }
                else
                {
                    pictureBox_Folder.Visible = false;
                    pictureBox_File.Visible = true;
                }
            }
        }

        private void btn_MoreOption_Click(object sender, EventArgs e)
        {
            guna2CMStrip.Show(this, this.PointToClient(MousePosition));
        }

        private void pictureBox_MoreOption_MouseEnter(object sender, EventArgs e)
        {
            pictureBox_MoreOption.BackColor = Color.FromArgb(102, 153, 255);
            guna2CirclePictureBox_MoreOption.FillColor = Color.FromArgb(102, 153, 255);
        }

        private void pictureBox_MoreOption_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_MoreOption.BackColor = Color.White;
            guna2CirclePictureBox_MoreOption.FillColor = Color.White;
        }

        private string GetSuitString(string input, int maxLenght)
        {
            if (input.Length > maxLenght)
            {
                return input.Substring(0, maxLenght) + "...";
            }
            return input;
        }
    }
}
