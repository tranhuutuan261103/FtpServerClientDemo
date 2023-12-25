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
        public AccountControl(AccountInfoVM accountInfoVM)
        {
            InitializeComponent();
            _accountInfoVM = accountInfoVM;
        }

        public void UpdateUI()
        {

        }
    }
}
