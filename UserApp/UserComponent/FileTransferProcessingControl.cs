using Guna.UI2.WinForms;
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

                // Safely update label_FileName.

                UpdateControlText(label_FileName, processing.FileName);

                // Safely update label_Status.

                UpdateControlText(label_Status, processing.StatusToString());

                // Safely update label_FileTransferPercent.

                UpdateControlText(label_FileTransferPercent, $"{processing.FileTransferedPercent,3}%");

                // Safely update label_TransferRate.

                UpdateControlText(label_TransferRate, TransferRateFormat(processing.FileSizeTransfered, processing.FileSize));

                // Safely update progressBar.

                UpdateProgressBar(progressBar, processing.FileTransferedPercent);

            }

        }

        private void UpdateControlText(System.Windows.Forms.Label label, string text)

        {
            if (label.IsHandleCreated && !label.Disposing && !label.IsDisposed)
            {
                label.Invoke((MethodInvoker)delegate { label.Text = text; });
            }
        }

        private void UpdateProgressBar(Guna2ProgressBar progressBar, int value)

        {

            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke((MethodInvoker)delegate { SafeSetProgressBarValue(progressBar, value); });
            }
            else
            {
                SafeSetProgressBarValue(progressBar, value);
            }

        }

        private void SafeSetProgressBarValue(Guna2ProgressBar progressBar, int value)

        {

            // Ensure the value is within bounds for the ProgressBar.

            value = Math.Clamp(value, progressBar.Minimum, progressBar.Maximum);

            if (!progressBar.Disposing && !progressBar.IsDisposed)

            {

                progressBar.Value = value;

            }

        }

        private string TransferRateFormat(long fileTransfered, long fileSize)
        {
            string[] type = { "B", "KB", "MB", "GB", "TB" };
            int i = 0;
            int j = 0;
            while (fileTransfered > 1024)
            {
                fileTransfered /= 1024;
                i++;
            }
            while (fileSize > 1024)
            {
                fileSize /= 1024;
                j++;
            }
            return $"{fileTransfered} {type[i]}/{fileSize} {type[j]}";
        }

        public void UpdateTransferProcessing(FileTransferProcessing processing)
        {
            this.processing = processing;
            UpdateUI();
        }
    }
}
