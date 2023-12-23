using MyClassLibrary.Bean.File;
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
        public FileControl(FileInfor item, FileControlClickHandler fileControlClickHandler, RenameClickHandler renameClickHandler, DeleteClickHandler deleteClickHandler)
        {
            FileControlClick = fileControlClickHandler;
            RenameClick = renameClickHandler;
            DeleteClick = deleteClickHandler;
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

        public delegate void RenameClickHandler(RenameFileRequest sender);
        public event RenameClickHandler RenameClick;

        private void ToolStripMenuItem_Rename_Click(object sender, EventArgs e)
        {
            using (InputDialog inputDialog = new InputDialog())
            {
                inputDialog.Title = "Rename";
                inputDialog.Message = "Are you sure you want to rename?";
                if (infor.IsDirectory == true)
                {
                    inputDialog.InputText = infor.Name;
                } else
                {
                    inputDialog.InputText = infor.Name.Substring(0, infor.Name.LastIndexOf('.'));
                }
                
                if (inputDialog.ShowDialog() == DialogResult.OK)
                {
                    if (inputDialog.InputText == infor.Name || inputDialog.InputText == "")
                    {
                        return;
                    }
                    RenameFileRequest request = new RenameFileRequest()
                    {
                        Id = infor.Id,
                        NewName = inputDialog.InputText,
                    };
                    if (infor.IsDirectory == false)
                    {
                        request.NewName += infor.Name.Substring(infor.Name.LastIndexOf('.'));
                    }
                    RenameClick(request);
                }
            }
        }

        private void ToolStripMenuItem_Information_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Information");
        }

        public delegate void DeleteClickHandler(DeleteFileRequest sender);
        public event DeleteClickHandler DeleteClick;

        private void ToolStripMenuItem_Remove_Click(object sender, EventArgs e)
        {
            DeleteFileRequest request = new DeleteFileRequest()
            {
                Id = infor.Id,
            };
            if (infor.IsDirectory == true)
            {
                request.RequestType = DeleteFileRequestType.Folder;
            }
            else
            {
                request.RequestType = DeleteFileRequestType.File;
            }
            DeleteClick(request);
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
