using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using MinvoiceLoadDataMisa.Config;
using MinvoiceLoadDataMisa.Forms;
using MinvoiceLoadDataMisa.Services;

namespace MinvoiceLoadDataMisa
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        private bool _stop;
        private BackgroundWorker _bw;
        public XtraForm1()
        {
            InitializeComponent();
            _bw = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            _bw.DoWork += _bw_DoWork;
            _bw.RunWorkerCompleted += _bw_RunWorkerCompleted;
        }

        private void _bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //if (e.Cancelled)
            //{
            //    notifyIcon1.ShowBalloonTip(5000, "Thông Báo", "Đã hủy lấy dữ liệu tự động", ToolTipIcon.Info);
            //}
            //else if (e.Error != null)
            //{
            //    notifyIcon1.ShowBalloonTip(5000, "Thông Báo", "Có lỗi xảy ra", ToolTipIcon.Error);
            //}
            //else
            //{
            //    notifyIcon1.ShowBalloonTip(5000, "Thông Báo", "Đã lấy dữ liệu thành công", ToolTipIcon.Info);
            //    btnGetData.Enabled = true;
            //    backgroundWorker1.RunWorkerAsync();
            //}

        }

        private void _bw_DoWork(object sender, DoWorkEventArgs e)
        {
            //backgroundWorker1.CancelAsync();
            //FrmLoading frmLoading = new FrmLoading();
            //frmLoading.ShowDialog();
            backgroundWorker1.CancelAsync();
            FrmGetDate frmGetDate = new FrmGetDate();
            frmGetDate.ShowDialog();
        }

        private void XtraForm1_Load(object sender, EventArgs e)
        {
            CheckConnectionString();
            //notifyIcon1.ShowBalloonTip(2000,"Loading","Đang lấy dữ liệu", ToolTipIcon.Info);
            //timer1.Start();

            notifyIcon1.ShowBalloonTip(5000, "Thông Báo", "Đang lấy dữ liệu", ToolTipIcon.Info);
            backgroundWorker1.RunWorkerAsync();
        }

        private void CheckConnectionString()
        {
            string connectionString = BaseConfig.ConnectionString;
            if (string.IsNullOrEmpty(connectionString))
            {
                //timer1.Start();
                new FrmConnectDatabase().ShowDialog();
                //timer1.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //_tick++;
            //var timeGetData = (int)BaseConfig.TimeGetData;
            //if (_tick == timeGetData)
            //{
            //    MinvoiceService.GetDataFromMisaToMinvoiceTest(0);
            //    _tick = 0;
            //}
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            ////timer1.Stop();
            ////FrmLoading frmLoading = new FrmLoading();
            ////frmLoading.ShowDialog();
            ////timer1.Start();
            //btnGetData.Enabled = false;
            //backgroundWorker1.CancelAsync();
            //_bw.RunWorkerAsync();
            FrmGetDate frmGetDate = new FrmGetDate();
            frmGetDate.ShowDialog();

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            //if (timer1.Enabled)
            //{
            //    timer1.Stop();
            //    btnStop.Text = @"Tiếp tục";//}
            //else
            //{
            //    timer1.Start();
            //    btnStop.Text = @"Dừng";
            //}
            _stop = !_stop;

            if (backgroundWorker1.IsBusy != true)
            {
                btnStop.Text = @"Dừng";
                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                if (backgroundWorker1.WorkerSupportsCancellation)
                {
                    btnStop.Text = @"Tiếp tục";
                    backgroundWorker1.CancelAsync();
                }
            }
        }

        private void btnSetupDatabase_Click(object sender, EventArgs e)
        {
            //timer1.Stop();
            var frmConnectDatabase = new FrmConnectDatabase();
            frmConnectDatabase.ShowDialog();
            //timer1.Start();
        }

        private void btnSettingSystem_Click(object sender, EventArgs e)
        {
            //timer1.Stop();
            FrmLogin frm = new FrmLogin(); frm.ShowDialog(); //timer1.Start();
        }

        private void lấyDữLiệuNgayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //GetData();
            _bw.RunWorkerAsync();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //timer1.Stop();
            FrmSelectInvoice frmSelectInvoice = new FrmSelectInvoice();
            frmSelectInvoice.ShowDialog();
            //timer1.Start();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            while (true)
            {
                if (worker != null && worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                else
                {
                    MinvoiceService.GetDataFromMisaToMinvoiceTest(0);
                    Thread.Sleep(5000);
                }

            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                notifyIcon1.ShowBalloonTip(5000, "Thông Báo", "Đã hủy lấy dữ liệu tự động", ToolTipIcon.Info);
            }
            else if (e.Error != null)
            {
                notifyIcon1.ShowBalloonTip(5000, "Thông Báo", "Có lỗi xảy ra", ToolTipIcon.Error);
            }
            else
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }
        private void btnGetInvoiceByDate_Click_1(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
            _bw.RunWorkerAsync();
        }
    }
}