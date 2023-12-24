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

namespace UserApp.UI.UserComponent
{
    public partial class ProfileControl : UserControl
    {
        private FileAccessVM _fileAccessVM;
        private bool _isVisibledCheckBox;
        public ProfileControl(FileAccessVM fileAccessVM, bool isVisibledCheckBox)
        {
            InitializeComponent();
            _fileAccessVM = fileAccessVM;
            _isVisibledCheckBox = isVisibledCheckBox;
            UpdateUI();
        }

        private void UpdateUI()
        {
            checkBox.Visible = _isVisibledCheckBox;
            if (_fileAccessVM != null)
            {
                lbl_Email.Text = _fileAccessVM.Email;
                lbl_Fullname.Text = _fileAccessVM.FirstName + " " + _fileAccessVM.LastName;
                if (_fileAccessVM.IdAccess == 1)
                {
                    lbl_Role.Text = "O";
                }
                else if (_fileAccessVM.IdAccess == 2)
                {
                    lbl_Role.Text = "S";
                }
                else if (_fileAccessVM.IdAccess == 3)
                {
                    lbl_Role.Text = "V";
                }
                else if (_fileAccessVM.IdAccess == 4)
                {
                    lbl_Role.Text = "N";
                }
                if (_fileAccessVM.Avatar.Count > 0)
                {
                    pic_Avatar.Image = Image.FromStream(new System.IO.MemoryStream(_fileAccessVM.Avatar.ToArray()));
                }
            }
        }

        public bool IsSelected()
        {
            return checkBox.Checked;
        }

        public FileAccessVM GetFileAccessVM()
        {
            _fileAccessVM.Avatar = new List<byte>();
            return _fileAccessVM;
        }
    }
}
