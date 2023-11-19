using MyClassLibrary.Common;
using UserApp.BLL;

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
        }

        public void ProcessTransfer(object sender)
        {
            int progress = (int)sender;
            progressDownload.Value = progress;
            label1.BeginInvoke(delegate { label1.Text =  progress + "%"; });
        }
    }
}