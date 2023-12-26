using MyClassLibrary.Bean.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerApp.UserComponent
{
    public partial class AccountControl : UserControl
    {
        private AccountInfoVM _accountInfoVM;
        private int status;
        public AccountControl(AccountInfoVM accountInfoVM, int status)
        {
            InitializeComponent();
            _accountInfoVM = accountInfoVM;
            this.status = status;
            UpdateUI();
        }

        public AccountInfoVM GetAccountInfoVM()
        {
            return _accountInfoVM;
        }

        public void SetStatus(int status)
        {
            this.status = status;
            UpdateUI();
        }

        public void UpdateUI()
        {
            if (_accountInfoVM != null)
            {
                lbl_Email.Text = _accountInfoVM.Email;
                lbl_FullName.Text = _accountInfoVM.FirstName + " " + _accountInfoVM.LastName;
                if (_accountInfoVM.Avatar != null && _accountInfoVM.Avatar.Count > 0)
                {
                    try
                    {
                        pic_Avatar.Image = Image.FromStream(new System.IO.MemoryStream(_accountInfoVM.Avatar.ToArray()));
                    }
                    catch
                    {
                        pic_Avatar.Image = null;
                    }
                }
                if (status == 1)
                {
                    pic_Status.FillColor = Color.Green;
                }
                else if (status == 2)
                {
                    pic_Status.FillColor = Color.Red;
                }
                else
                {
                    pic_Status.FillColor = Color.Gray;
                }
            }
        }

        public bool IsSelected()
        {
            return checkBox.Checked;
        }

        public void SetSelected(bool selected)
        {
            checkBox.Checked = selected;
        }
    }
}
