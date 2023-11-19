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

namespace UserApp.UserComponent
{
    public partial class FileControl : UserControl
    {
        public FileControl(FileControlClickHandler fileControlClickHandler)
        {
            FileControlClick = fileControlClickHandler;
            InitializeComponent();
        }

        public delegate void FileControlClickHandler(object sender, EventArgs e);
        public event FileControlClickHandler FileControlClick;

        private FileInfor infor = new FileInfor();

        private void FileControl_DoubleClick(object sender, EventArgs e)
        {
            FileControlClick(Infor, e);
        }

        public FileInfor Infor
        {
            get { return infor; }
            set
            {
                infor = value;
                if (infor != null)
                {
                    label_Name.Text = infor.Name;
                }
            }
        }
    }
}
