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
    public partial class InputDialog : Form
    {
        private string _title;
        private string _inputText;
        private string _message;
        public InputDialog()
        {
            InitializeComponent();
            _inputText = string.Empty;
            _message = string.Empty;
            _title = string.Empty;
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                this.Text = value;
            }
        }

        public string InputText
        {
            get { return _inputText; }
            set
            {
                _inputText = value;
                txt_InputText.Text = value;
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                lbl_Message.Text = value;
            }
        }

        private void btn_Oke_Click(object sender, EventArgs e)
        {
            _inputText = txt_InputText.Text;
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
