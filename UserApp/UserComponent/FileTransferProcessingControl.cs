using MyClassLibrary.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserApp.UserComponent
{
    public partial class FileTransferProcessingControl : UserControl
    {
        public FileTransferProcessingControl(FileTransferProcessing processing)
        {
            InitializeComponent();
            this.processing = processing;
            UpdateUI();
        }

        private FileTransferProcessing processing;

        public FileTransferProcessing GetFileTransferProcessing()
        {
            return processing;
        }

        public void UpdateUI()
        {
            if (processing != null)
            {
                if (label_FileName.IsHandleCreated)
                {
                    label_FileName.Invoke((MethodInvoker)delegate { label_FileName.Text = processing.FileName; });
                }
                else
                {
                    label_FileName.Text = processing.FileName;
                }
                //label_Status.BeginInvoke(delegate { label_Status.Text = processing.Status.ToString(); });
                if (label_FileTransferPercent.IsHandleCreated)
                {
                    label_FileTransferPercent.Invoke((MethodInvoker)delegate { label_FileTransferPercent.Text = $"{processing.FileTransferedPercent,3}%"; });
                }
                else
                {
                    label_FileTransferPercent.Text = $"{processing.FileTransferedPercent,3}%";
                }
                //label_TransferRate.BeginInvoke(delegate { label_TransferRate.Text = processing.FileSizeTransfered + "/" + processing.FileSize; });
                if (label_TransferRate.IsHandleCreated)
                {
                    label_TransferRate.Invoke((MethodInvoker)delegate { label_TransferRate.Text = processing.FileSizeTransfered + "/" + processing.FileSize; });
                }
                else
                {
                    label_TransferRate.Text = processing.FileSizeTransfered + "/" + processing.FileSize;
                }
                progressBar.Value = processing.FileTransferedPercent;
            }
        }

        public void UpdateTransferProcessing(FileTransferProcessing processing)
        {
            this.processing = processing;
            UpdateUI();
        }
    }
}
