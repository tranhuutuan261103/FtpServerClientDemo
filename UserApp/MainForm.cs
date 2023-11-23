using MyClassLibrary.Common;
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
            MainForm_BLL = new MainForm_BLL(TransferProgress);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateGridFileAndFolder();
        }

        private void UpdateGridFileAndFolder()
        {
            grid_FileAndFolder.Controls.Clear();
            var fileInfos = MainForm_BLL.GetFileInfos();
            foreach (var item in fileInfos)
            {
                var fileControl = new FileControl(FileControlHandle);
                fileControl.Infor = item;
                grid_FileAndFolder.Controls.Add(fileControl);
            }
        }

        public void FileControlHandle(object sender, EventArgs e)
        {
            MouseEventArgs mouseEventArgs = (MouseEventArgs)e;
            if (mouseEventArgs.Clicks == 2)
            {
                MessageBox.Show("Double Click");
            }
            /*
            FileInfor fileInfo = (FileInfor)sender;
            if (fileInfo.IsDirectory == false)
            {
                MainForm_BLL.Download(fileInfo.Name);
            }
            else if (fileInfo.IsDirectory == true)
            {
                MainForm_BLL.ChangeFolder(fileInfo.Name);
                UpdateGridFileAndFolder();
            }*/
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
                UpdateGridFileAndFolder();
            }
        }

        public void TransferProgress(FileTransferProcessing sender)
        {
            if (sender.Status == FileTransferProcessingStatus.Waiting)
            {
                flowLayoutPanel_ListProcessing.Controls.Add(new FileTransferProcessingControl(sender));
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

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Dispose event processTransfer when form is closed.
            MainForm_BLL.Dispose();
            flowLayoutPanel_ListProcessing.Controls.Clear();
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            MainForm_BLL.Back();
            UpdateGridFileAndFolder();
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