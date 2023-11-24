using MyClassLibrary.Common;
using System.Diagnostics.Eventing.Reader;
using UserApp.BLL;
using UserApp.UserComponent;

namespace UserApp
{
    public partial class MainForm : Form
    {
        private MainForm_BLL MainForm_BLL;
        public MainForm()
        {
            InitializeComponent();
            MainForm_BLL = new MainForm_BLL(TransferProgress, ChangeFolderAndFileHandler);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            MainForm_BLL.GetFileInfos();
        }

        private void ChangeFolderAndFileHandler(List<FileInfor> sender)
        {
            UpdateGridFileAndFolder(sender);
        }

        private void UpdateGridFileAndFolder(List<FileInfor> fileInfors)
        {
            if (grid_FileAndFolder.IsHandleCreated && !grid_FileAndFolder.IsDisposed)
            {
                grid_FileAndFolder.Invoke((MethodInvoker)delegate {
                    grid_FileAndFolder.Controls.Clear();
                });
            }
            
            foreach (var item in fileInfors)
            {
                if (grid_FileAndFolder.IsHandleCreated && !grid_FileAndFolder.IsDisposed)
                {
                    grid_FileAndFolder.Invoke((MethodInvoker)delegate { 
                        grid_FileAndFolder.Controls.Add(new FileControl(FileControlHandle) {
                            Infor = item
                        });
                    });
                }
            }
        }

        public void FileControlHandle(object sender, EventArgs e)
        {
            MouseEventArgs mouseEventArgs = (MouseEventArgs)e;
            FileInfor fileInfo = (FileInfor)sender;
            if (mouseEventArgs.Clicks == 2 && fileInfo.IsDirectory)
            {
                MainForm_BLL.ChangeFolder(fileInfo.Name);
            }
            else if (mouseEventArgs.Button == MouseButtons.Right)
            {
                if (fileInfo.IsDirectory == false)
                {
                    MainForm_BLL.Download(fileInfo.Name);
                }
                else if (fileInfo.IsDirectory == true)
                {
                    MainForm_BLL.DownloadFolder(fileInfo.Name);
                }
            }
        }

        public void Download(object sender, EventArgs e)
        {
            FileInfor fileInfo = (FileInfor)sender;
            if (fileInfo.IsDirectory == false)
            {
                MainForm_BLL.Download(fileInfo.Name);
            }
            else if (fileInfo.IsDirectory == true)
            {
                MainForm_BLL.ChangeFolder(fileInfo.Name);
                //UpdateGridFileAndFolder();
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
                        flowLayoutPanel_ListProcessing.Controls.Add(new FileTransferProcessingControl(sender));
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

        private void btn_Upload_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFD = new OpenFileDialog();
            if (oFD.ShowDialog() == DialogResult.OK)
            {
                MainForm_BLL.Upload(oFD.FileName);
            }
        }

        private void btn_UploadFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fBD = new FolderBrowserDialog();
            if (fBD.ShowDialog() == DialogResult.OK)
            {
                MainForm_BLL.UploadFolder(fBD.SelectedPath);
            }
        }
    }
}