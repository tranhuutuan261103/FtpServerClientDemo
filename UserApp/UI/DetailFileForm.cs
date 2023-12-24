using MyClassLibrary;
using MyClassLibrary.Bean.File;
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
using UserApp.BLL;
using UserApp.DTO;
using UserApp.UI.UserComponent;

namespace UserApp.UI
{
    public partial class DetailFileForm : Form
    {
        private FileDetailVM _fileInforVM;
        public List<FileAccessVM> _fileAccessVMs = new List<FileAccessVM>();
        public DetailFileForm(FileDetailVM fileInforVM, List<FileAccessVM> fileAccessVMs)
        {
            InitializeComponent();
            flowLayoutPanel_ListProfile.AutoScroll = false;
            flowLayoutPanel_ListProfile.HorizontalScroll.Enabled = false;
            flowLayoutPanel_ListProfile.HorizontalScroll.Visible = false;
            flowLayoutPanel_ListProfile.HorizontalScroll.Maximum = 0;
            flowLayoutPanel_ListProfile.AutoScroll = true;
            _fileInforVM = fileInforVM;
            _fileAccessVMs = fileAccessVMs;
            SetAccessAbility();
            UpdateUI();
        }

        private void SetAccessAbility()
        {
            List<AccessAbility> accessAbilities = new List<AccessAbility>()
            {
                new AccessAbility()
                {
                    Id = 2,
                    Name = "S",
                    Description = "Can view or edit"
                },
                new AccessAbility()
                {
                    Id = 3,
                    Name = "V",
                    Description = "Can view"
                },
                new AccessAbility()
                {
                    Id = 4,
                    Name = "N",
                    Description = "None"
                }
            };

            cbb_AccessAbility.DataSource = accessAbilities;
        }

        private void UpdateUI()
        {
            if (_fileInforVM != null)
            {
                lbl_Name.Text = _fileInforVM.Name;
                lbl_OwnerData.Text = _fileInforVM.FileOwner;
                lbl_TypeData.Text = _fileInforVM.Type.ToString();
                lbl_CreatedTimeData.Text = _fileInforVM.CreatedTime.ToString();
                FileManager fileManager = new FileManager();
                if (_fileInforVM.Type == FileInforType.Folder)
                {
                    lbl_SizeData.Text = "Unknown";
                }
                else
                {
                    lbl_SizeData.Text = fileManager.FileSizeToString(_fileInforVM.Length);
                }
                foreach (FileAccessVM fileAccessVM in _fileAccessVMs)
                {
                    if (fileAccessVM.IdAccess == 1)
                    {
                        continue;
                    }
                    ProfileControl profileControl = new ProfileControl(fileAccessVM);
                    flowLayoutPanel_ListProfile.Controls.Add(profileControl);
                }
            }
        }
        private void DetailFileForm_Load(object sender, EventArgs e)
        {
            //_mainForm_BLL.GetListFileAccess(_fileInforVM.Id);
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            string email = txt_Email.Text;
            MailHelper mailHelper = new MailHelper();
            if (mailHelper.IsValidEmail(email))
            {
                foreach (FileAccessVM fileAccessVM in _fileAccessVMs)
                {
                    if (fileAccessVM.Email.Equals(email))
                    {
                        MessageBox.Show("Email is already exist");
                        return;
                    }
                }
                if (_fileInforVM != null)
                {
                    FileAccessVM fileAccessVM = new FileAccessVM()
                    {
                        Email = email,
                        IdAccess = 4,
                        FirstName = "Unknown",
                        LastName = "",
                        Avatar = new List<byte>(),
                        IdFile = _fileInforVM.Id,
                        IdAccount = 0
                    };
                    _fileAccessVMs.Add(fileAccessVM);
                    ProfileControl profileControl = new ProfileControl(fileAccessVM);
                    flowLayoutPanel_ListProfile.Controls.Add(profileControl);
                    txt_Email.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Email is not valid");
            }
        }

        private void btn_Oke_Click(object sender, EventArgs e)
        {
            int idAccess = 0;
            if (cbb_AccessAbility.SelectedItem != null)
            {
                AccessAbility access = (AccessAbility)cbb_AccessAbility.SelectedItem;
                idAccess = access.Id;
            }
            if (flowLayoutPanel_ListProfile.Controls != null)
            {
                foreach (ProfileControl profileControl in flowLayoutPanel_ListProfile.Controls)
                {
                    if (profileControl.IsSelected() == true)
                    {
                        FileAccessVM fileAccessVM = profileControl.GetFileAccessVM();
                        fileAccessVM.IdAccess = idAccess;
                        _outputListFileAccessVMs.Add(fileAccessVM);
                    }
                }
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private List<FileAccessVM> _outputListFileAccessVMs = new List<FileAccessVM>();

        public List<FileAccessVM> GetFileAccessVMs()
        {
            return _outputListFileAccessVMs;
        }
    }
}
