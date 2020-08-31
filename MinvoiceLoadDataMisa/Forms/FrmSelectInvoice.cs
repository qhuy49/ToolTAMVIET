using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MinvoiceLoadDataMisa.Config;
using MinvoiceLoadDataMisa.Data;
using MinvoiceLoadDataMisa.Services;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;

namespace MinvoiceLoadDataMisa.Forms
{
    public partial class FrmSelectInvoice : DevExpress.XtraEditors.XtraForm
    {
        public FrmSelectInvoice()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmSelectInvoice_Load(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Save();
            string invoiceNumber = "";
            List<InvoiceObject> invoiceObjects = new List<InvoiceObject>();

            foreach (var selectedItem in checkedListBoxControl1.CheckedItems)
            {
                invoiceNumber += $"'{(selectedItem as InvoiceObject)?.InvoiceNumber}' ,";

                var invoiceObject = selectedItem as InvoiceObject;
                invoiceObjects.Add(invoiceObject);

            }

            if (!string.IsNullOrEmpty(invoiceNumber))
            {
                invoiceNumber = invoiceNumber.Substring(0, invoiceNumber.Length - 1);
                //MinvoiceService.UpdateInvoice(invoiceNumber);
                MinvoiceService.UpdateInvoice(invoiceObjects);
            }
            else
            {
                XtraMessageBox.Show("Chưa chọn hóa đơn muốn cập nhật", "Thông Báo", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
        private void Save()
        {
            string id = "";
            var mauHoaDon2 = slokMauHoaDon1.GetSelectedDataRow();
            if (mauHoaDon2 != null)
            {
                id = ((MauHoaDon)mauHoaDon2).Id.ToString();
            }
            CommonService.UpdateSettingAppConfig(CommonConstants.InvoiceCodeId2, id);
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            var checkEdit = sender as CheckEdit;
            if (checkEdit != null && checkEdit.Checked)
            {
                checkedListBoxControl1.CheckAll();
            }
            else
            {
                checkedListBoxControl1.UnCheckAll();
            }
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            string id = "";
            var mauHoaDon = slokMauHoaDon1.GetSelectedDataRow();
            if (mauHoaDon != null)
            {
                id = ((MauHoaDon)mauHoaDon).Id.ToString();
            }

            var tuNgay = !string.IsNullOrEmpty(dteTuNgay.Text) ? dteTuNgay.DateTime.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd");
            var denNgay = !string.IsNullOrEmpty(dteDenNgay.Text) ? dteDenNgay.DateTime.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd");
            checkedListBoxControl1.DataSource = CommonService.GetInvoiceObjects(tuNgay, denNgay, id);
            checkedListBoxControl1.DisplayMember = "Value";
            checkedListBoxControl1.ValueMember = "InvoiceNumber";
        }

        private void btnCall_CheckedChanged(object sender, EventArgs e)
        {
            LoadMauHoaDon();
        }

        private void LoadMauHoaDon()
        {


            try
            {
                List<MauHoaDon> mauHoaDons = CommonService.GetInfoInvoice();
                slokMauHoaDon1.Properties.DataSource = mauHoaDons;
                slokMauHoaDon1.Properties.DisplayMember = "MauSo";
                slokMauHoaDon1.Properties.ValueMember = "Id";
                

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void slokMauHoaDon_EditValueChanged(object sender, EventArgs e)
        {

        }
        
    }
}