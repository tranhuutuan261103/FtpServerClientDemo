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
            MainForm_BLL = new MainForm_BLL();
            MainForm_BLL.processTransfer += new MainForm_BLL.ProcessTransfer(ProcessTransfer);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var fileInfos = MainForm_BLL.GetFileInfos();
            foreach (var item in fileInfos)
            {
                var fileControl = new UserComponent.FileControl(Download);
                fileControl.Infor = item;
                grid_FileAndFolder.Controls.Add(fileControl);
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
                MainForm_BLL.DownloadFolder(fileInfo.Name);
            }
        }

        public void ProcessTransfer(FileTransferProcessing sender)
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
            MainForm_BLL.processTransfer -= ProcessTransfer;
            flowLayoutPanel_ListProcessing.Controls.Clear();
        }
    }
}