using DevExpress.XtraEditors;
using MinvoiceLoadDataMisa.Config;
using MinvoiceLoadDataMisa.Data;
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
            var mauHoaDon = searchLookUpEdit1View.GetFocusedRow() as MauHoaDon;
            CommonService.UpdateSettingAppConfig(CommonConstants.Editmode, "1");
            
            var a = Properties.Settings.Default.KyHieu;
            var b = Properties.Settings.Default.MauSo;
            var c = Properties.Settings.Default.InvoiceCodeId;
            var d = Properties.Settings.Default.Editmode;
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
                MinvoiceService.GetDataFromMisaToMinvoiceByDay(day, day1, CommonConstants.Editmode);
            }
            else
            {

            }
        }

        private void btnGetMau_Click(object sender, EventArgs e)
        {
            LoadMauHoaDon();
            
        }
        private void LoadMauHoaDon()
        {
            try
            {
                
                List<MauHoaDon> mauHoaDons = CommonService.GetInfoInvoice();
                txtMauSoHD.Properties.DataSource = mauHoaDons;
                txtMauSoHD.Properties.DisplayMember = "MauSo";
                txtMauSoHD.Properties.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void slokMauHoaDon_EditValueChanged(object sender, EventArgs e)
        {
            var mauHoaDon = searchLookUpEdit1View.GetFocusedRow() as MauHoaDon;
            if (mauHoaDon != null)
            {
                txtKyHieuHD.Text = mauHoaDon.KyHieu;
                txtMauSoHD.Text = mauHoaDon.MauSo;
                txtCodeId.Text = mauHoaDon.Id;
            }
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var mauHoaDon = searchLookUpEdit1View.GetFocusedRow() as MauHoaDon;


            CommonService.UpdateSettingAppConfig(CommonConstants.MauSo, mauHoaDon.MauSo);
            CommonService.UpdateSettingAppConfig(CommonConstants.KyHieu, mauHoaDon.KyHieu);
            CommonService.UpdateSettingAppConfig(CommonConstants.InvoiceCodeId, mauHoaDon.Id);

            var a = Properties.Settings.Default.MauSo;
            var b = Properties.Settings.Default.KyHieu;
            
            XtraMessageBox.Show("Lưu thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FrmGetDate_Load(object sender, EventArgs e)
        {
            txtCodeId.Text = Properties.Settings.Default.InvoiceCodeId.ToString();
            txtKyHieuHD.Text = Properties.Settings.Default.KyHieu.ToString();
            txtMauSoHD.Text = Properties.Settings.Default.MauSo.ToString();

        }
    }
}
