using MinvoiceLoadDataMisa.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinvoiceLoadDataMisa.Forms
{
    public partial class FrmGetDate : Form
    {
        public FrmGetDate()
        {
            InitializeComponent();
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            if (dtpTuNgay.Value.Date > dtpDenNgay.Value.Date)
            {
                MessageBox.Show("Lỗi quy luật chọn ngày", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (dtpDenNgay.Value.Date >= dtpTuNgay.Value.Date)
            {
                var day = dtpTuNgay.Value.ToString("yyyy-MM-dd");
                var day1 = dtpDenNgay.Value.ToString("yyyy-MM-dd");
                //var day = !string.IsNullOrEmpty(dtpTuNgay.Text) ? dtpTuNgay.Value.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd");
                //var day1 = !string.IsNullOrEmpty(dtpDenNgay.Text) ? dtpDenNgay.Value.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd");
                MinvoiceService.GetDataFromMisaToMinvoiceByDay(day, day1);
            }
            else
            {

            }
        }
    }
}
