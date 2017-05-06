using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace BackgroudWorker
{
    public partial class SomeForm : Form
    {
        private readonly BackgroundWorker _backgroundWorker = new BackgroundWorker();
        public SomeForm()
        {
            InitializeComponent();

            _backgroundWorker.DoWork += _backgroundWorker_DoWork;
            _backgroundWorker.ProgressChanged += _backgroundWorker_ProgressChanged;
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;
            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.WorkerSupportsCancellation = true;

            _backgroundWorker.RunWorkerAsync();
        }

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 101; i++)
            {
                if (i != 0) Thread.Sleep(500);
                _backgroundWorker.ReportProgress(i);

                if (_backgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    _backgroundWorker.ReportProgress(0);
                    return;
                }
            }
            _backgroundWorker.ReportProgress(100);
        }
        private void _backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            label1.Text = $"{e.ProgressPercentage}%";
            progressBar1.Value = e.ProgressPercentage;
        }
        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                label1.Text = "Process was cancelled";
            }
            else if (e.Error != null)
            {
                label1.Text = "Encountered an error";
            }
            else
            {
                label1.Text = "Process was completed";
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            if (_backgroundWorker.IsBusy)
            {
                _backgroundWorker.CancelAsync();
            }
        }
    }
}
