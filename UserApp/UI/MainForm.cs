using ConsoleApp;
using MyClassLibrary;
using MyClassLibrary.Bean.Account;
using MyClassLibrary.Bean.File;
using MyClassLibrary.Common;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;
using UserApp.BLL;
using UserApp.DTO;
using UserApp.UI.Fonts;
using UserApp.UI.UserComponent;

namespace UserApp.UI
{
    public partial class MainForm : Form
    {
        private MainForm_BLL MainForm_BLL;
        private InterFont font = new InterFont();
        private string _email;
        public MainForm(FtpClient ftpClient, string email)
        {
            InitializeComponent();
            flowLayoutPanel_ListProcessing.AutoScroll = false;
            flowLayoutPanel_ListProcessing.HorizontalScroll.Enabled = false;
            flowLayoutPanel_ListProcessing.HorizontalScroll.Visible = false;
            flowLayoutPanel_ListProcessing.HorizontalScroll.Maximum = 0;
            flowLayoutPanel_ListProcessing.AutoScroll = true;
            _email = email;
            MainForm_BLL = new MainForm_BLL(ftpClient, TransferProgress, ChangeFolderAndFileHandler, GetAccountInfor, GetDetailFileHandler);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            MainForm_BLL.GetFileInfos();
        }

        private void ChangeFolderAndFileHandler(FileInforPackage sender)
        {
            UpdateGridFileAndFolder(sender);
        }

        private void UpdateGridFileAndFolder(FileInforPackage fileInforPackage)
        {
            if (fileInforPackage.Category == Category.Owner)
            {
                if (grid_FileAndFolder.IsHandleCreated && !grid_FileAndFolder.IsDisposed)
                {
                    grid_FileAndFolder.BeginInvoke((MethodInvoker)delegate
                    {
                        grid_FileAndFolder.Controls.Clear();
                    });
                }

                foreach (var item in fileInforPackage.fileInfors)
                {
                    if (grid_FileAndFolder.IsHandleCreated && !grid_FileAndFolder.IsDisposed)
                    {
                        grid_FileAndFolder.BeginInvoke((MethodInvoker)delegate
                        {
                            grid_FileAndFolder.Controls.Add(new FileControl(item, FileControlHandle, ShowDetailFile, RenameFileHandler, DeleteFileHandler));
                        });
                    }
                }
            }
            else if (fileInforPackage.Category == Category.Shared)
            {
                if (grid_ListFileAndFolderShared.IsHandleCreated && !grid_ListFileAndFolderShared.IsDisposed)
                {
                    grid_ListFileAndFolderShared.BeginInvoke((MethodInvoker)delegate
                    {
                        grid_ListFileAndFolderShared.Controls.Clear();
                    });
                }
                foreach (var item in fileInforPackage.fileInfors)
                {
                    if (grid_ListFileAndFolderShared.IsHandleCreated && !grid_ListFileAndFolderShared.IsDisposed)
                    {
                        grid_ListFileAndFolderShared.BeginInvoke((MethodInvoker)delegate
                        {
                            grid_ListFileAndFolderShared.Controls.Add(new FileControl(item, FileControlHandle, ShowDetailFile, RenameFileHandler, DeleteFileHandler));
                        });
                    }
                }
            }
        }

        public void FileControlHandle(object sender, EventArgs e)
        {
            var request = (FileControlRequest)sender;
            if (request.type == FileControlRequestType.ChangeFolder)
            {
                if (request.fileInfor.IsDirectory == true)
                {
                    MainForm_BLL.ChangeFolder(request.fileInfor.Id);
                }
            }
            else if (request.type == FileControlRequestType.ChangeSharedFolder)
            {
                if (request.fileInfor.IsDirectory == true)
                {
                    MainForm_BLL.ChangeSharedFolder(request.fileInfor.Id);
                }
            }
            else if (request.type == FileControlRequestType.Download)
            {
                if (request.fileInfor.IsDirectory == false)
                {
                    MainForm_BLL.Download(request.fileInfor);
                }
                else if (request.fileInfor.IsDirectory == true)
                {
                    MainForm_BLL.DownloadFolder(request.fileInfor);
                }
            }
        }

        public void TransferProgress(FileTransferProcessing sender)
        {
            if (flowLayoutPanel_ListProcessing.IsHandleCreated && !flowLayoutPanel_ListProcessing.IsDisposed)
            {
                if (sender.Status == FileTransferProcessingStatus.Waiting)
                {
                    flowLayoutPanel_ListProcessing.Invoke((MethodInvoker)delegate
                    {
                        var control = new FileTransferProcessingControl(sender);
                        flowLayoutPanel_ListProcessing.Controls.Add(control);
                        control.UpdateUI();
                    });
                }
                else
                {
                    foreach (FileTransferProcessingControl control in flowLayoutPanel_ListProcessing.Controls)
                    {
                        if (sender == control.GetFileTransferProcessing())
                        {
                            control.UpdateTransferProcessing(sender);
                            break;
                        }
                    }
                }
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Dispose event processTransfer when form is closed.
            MainForm_BLL.Dispose();
            flowLayoutPanel_ListProcessing.Controls.Clear();
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            MainForm_BLL.Back();
        }
        private void ToolStripMenuItem_NewFolder_Click(object sender, EventArgs e)
        {
            using (InputDialog inputDialog = new InputDialog())
            {
                inputDialog.Title = "Create new folder";
                inputDialog.Message = "Are you sure you want to create a new folder?";
                DialogResult result = inputDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    string inputText = inputDialog.InputText;
                    if (inputText != "")
                    {
                        MainForm_BLL.CreateFolder(inputText);
                    }
                }
            }
        }

        private void ShowDetailFile(FileInfor fileInfor)
        {
            if (fileInfor == null)
            {
                return;
            }
            MainForm_BLL.GetDetailFile(fileInfor.Id);
        }

        private void GetDetailFileHandler(FileDetailVM fileInfor, List<FileAccessVM> fileAccessVMs)
        {
            if (fileInfor == null)
            {
                return;
            }
            DetailFileForm detailFileForm = new DetailFileForm(fileInfor, fileAccessVMs, _email);
            if (detailFileForm.ShowDialog() == DialogResult.OK)
            {
                MainForm_BLL.UpdateFileAccess(detailFileForm.GetFileAccessVMs());
            }
        }

        private void RenameFileHandler(RenameFileRequest sender)
        {
            MainForm_BLL.RenameFile(sender);
        }

        private void DeleteFileHandler(DeleteFileRequest sender)
        {
            if (sender.RequestType == DeleteFileRequestType.File)
            {
                MainForm_BLL.DeleteFile(sender);
            }
            else if (sender.RequestType == DeleteFileRequestType.Folder)
            {
                MainForm_BLL.DeleteFolder(sender);
            }
        }

        private void btn_Upload_Click(object sender, EventArgs e)
        {
            guna2ContextMenuStrip_btnNew.Show(this, this.PointToClient(MousePosition));
        }

        private void ToolStripMenuItem_UploadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFD = new OpenFileDialog();
            if (oFD.ShowDialog() == DialogResult.OK)
            {
                MainForm_BLL.Upload(oFD.FileName);
            }
        }

        private void ToolStripMenuItem_UploadFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fBD = new FolderBrowserDialog();
            if (fBD.ShowDialog() == DialogResult.OK)
            {
                MainForm_BLL.UploadFolder(fBD.SelectedPath);
            }
        }

        private void btn_TransferInfor_MouseClick(object sender, MouseEventArgs e)
        {
            if (flowLayoutPanel_ListProcessing.Visible == true)
            {
                flowLayoutPanel_ListProcessing.Visible = false;
            }
            else
            {
                flowLayoutPanel_ListProcessing.Visible = true;
                foreach (FileTransferProcessingControl control in flowLayoutPanel_ListProcessing.Controls)
                {
                    control.UpdateUI();
                }
            }
        }

        private void tabControl_Profile_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex == 1)
            {
                MainForm_BLL.ChangeSharedFolder("");
            }
            else if (e.TabPageIndex == 3)
            {
                btn_UpdateProfile.Enabled = false;
                MainForm_BLL.GetAccountInfor();
            }
        }

        private void GetAccountInfor(AccountInfoVM accountInfor)
        {
            FileManager fileManager = new FileManager();
            if (tabControl_Profile.IsHandleCreated && !tabControl_Profile.IsDisposed)
            {
                tabControl_Profile.BeginInvoke((MethodInvoker)delegate
                {
                    txt_Email.Text = accountInfor.Email;
                    txt_FirstName.Text = accountInfor.FirstName;
                    txt_FirstName.TextChanged += txt_DataProfile_TextChanged;
                    txt_LastName.Text = accountInfor.LastName;
                    txt_LastName.TextChanged += txt_DataProfile_TextChanged;
                    lbl_StoragedData.Text = fileManager.FileSizeToString(accountInfor.UsedStorage);
                    lbl_FullName.Text = accountInfor.FirstName + " " + accountInfor.LastName;
                    lbl_CreationDate.Text = accountInfor.CreationDate.ToString("yyyy/MM/dd HH:mm");
                    if (accountInfor.Avatar != null && accountInfor.Avatar.Count > 0)
                    {
                        try
                        {
                            using (MemoryStream memoryStream = new MemoryStream(accountInfor.Avatar.ToArray()))
                            {
                                pic_Avatar.Image = Image.FromStream(memoryStream);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                });
            }
            else
            {
                txt_Email.Text = accountInfor.Email;
                txt_FirstName.Text = accountInfor.FirstName;
                txt_FirstName.TextChanged += txt_DataProfile_TextChanged;
                txt_LastName.Text = accountInfor.LastName;
                txt_LastName.TextChanged += txt_DataProfile_TextChanged;
                lbl_StoragedData.Text = fileManager.FileSizeToString(accountInfor.UsedStorage);
                lbl_FullName.Text = accountInfor.FirstName + " " + accountInfor.LastName;
                lbl_CreationDate.Text = accountInfor.CreationDate.ToString("yyyy/MM/dd HH:mm");
                if (accountInfor.Avatar != null && accountInfor.Avatar.Count > 0)
                {
                    try
                    {
                        using (MemoryStream memoryStream = new MemoryStream(accountInfor.Avatar.ToArray()))
                        {
                            pic_Avatar.Image = Image.FromStream(memoryStream);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void txt_DataProfile_TextChanged(object sender, EventArgs e)
        {
            btn_UpdateProfile.Enabled = true;
        }

        private void btn_UpdateProfile_Click(object sender, EventArgs e)
        {
            AccountInfoVM accountInfo = new AccountInfoVM()
            {
                FirstName = txt_FirstName.Text,
                LastName = txt_LastName.Text,
            };
            if (pic_Avatar.Image != null)
            {
                // Nén ảnh và giữ dung lượng dưới 500 KB
                byte[] compressedImageData = ImageHelper.CompressImage(pic_Avatar.Image, 50);

                // Gán ảnh nén vào thông tin tài khoản
                accountInfo.Avatar = compressedImageData.ToList();
            }

            MainForm_BLL.UpdateAccountInfor(accountInfo);
        }

        private void pic_Avatar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files (*.jpg;*.png;)|*.jpg;*.png;";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pic_Avatar.Image = Image.FromFile(ofd.FileName);
            }

            btn_UpdateProfile.Enabled = true;
        }
    }
}