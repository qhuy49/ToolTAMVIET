using System;
using MinvoiceLoadDataMisa.Services;

namespace MinvoiceLoadDataMisa.Forms
{
    public partial class FrmLoading : DevExpress.XtraEditors.XtraForm
    {
        public FrmLoading()
        {
            InitializeComponent();
        }

        private void FrmLoading_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private int _tick = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            _tick++;
            if (_tick == 3)
            {
                MinvoiceService.GetDataFromMisaToMinvoiceTest();
                timer1.Stop();
                timer1.Dispose();
                Close();
            }
        }
    }
}