using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.Win32;
using MinvoiceLoadDataMisa.Config;
using MinvoiceLoadDataMisa.Data;
using MinvoiceLoadDataMisa.Services;

namespace MinvoiceLoadDataMisa.Forms
{
    public partial class FrmSettingSystem : DevExpress.XtraEditors.XtraForm
    {
        public FrmSettingSystem()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void LoadData()
        {
            cboVersion.DataSource = GetComboBoxVersion();
            cboVersion.DisplayMember = "Name";
            cboVersion.ValueMember = "Value";

            lblConnectionString.Text = BaseConfig.ConnectionString;
            txtTableInvoice.Text = BaseConfig.TableInvocie;
            txtTableInvoiceDetail.Text = BaseConfig.TableInvoiceDetail;
            txtTableVoucherDetail.Text = BaseConfig.TableVoucherDetail;
            txtUserLoginWeb.Text = BaseConfig.UsernameLoginWeb;
            txtPassLoginWeb.Text = BaseConfig.PasswordLoginWeb;
            txtUrlLogin.Text = BaseConfig.UrlLogin;
            txtUrlSave.Text = BaseConfig.UrlSave;
            txtUrlGetInvoice.Text = BaseConfig.UrlGetInvoiceByInvoiceAuthId;
            numTimeGetData.Value = BaseConfig.TimeGetData;
            txtMauSo.Text = BaseConfig.MauSo;
            txtKyHieu.Text = BaseConfig.KyHieu;
            txtUrlRef.Text = BaseConfig.UrlRef;
            txtInvoiceCodeId.Text = BaseConfig.InvoiceCodeId;
            txtTableVoucher.Text = BaseConfig.TableVoucher;
            txtCommand.Text = BaseConfig.Command;
            txtUrlCommand.Text = BaseConfig.UrlCommand;
            cboVersion.SelectedValue = BaseConfig.Version;

            txtTableInventoryItem.Text = BaseConfig.TableInventoryItem;
            txtTableUnit.Text = BaseConfig.TableUnit;
            txtRefInventoryItem.Text = BaseConfig.RefInventoryItem;
            txtRefUnit.Text = BaseConfig.RefUnit;
        }
        private void Save()
        {
            CommonService.AddColumnIsInvoice();
            Check();

            string tableInvoice = txtTableInvoice.Text;
            string tableInvoiceDetail = txtTableInvoiceDetail.Text;
            string tableVoucherDetail = txtTableVoucherDetail.Text;
            string tableVoucher = txtTableVoucher.Text;

            string userNameWeb = txtUserLoginWeb.Text;
            string passWordWeb = txtPassLoginWeb.Text;

            string urlLogin = txtUrlLogin.Text;
            string urlSave = txtUrlSave.Text;
            string urlGetInvoice = txtUrlGetInvoice.Text;

            string timeGetData = !string.IsNullOrEmpty(numTimeGetData.Text) ? numTimeGetData.Value.ToString("n0") : "600";

            string mauSo = txtMauSo.Text;
            string kyHieu = txtKyHieu.Text;
            string invoiceCodeId = txtInvoiceCodeId.Text;

            string urlRef = txtUrlRef.Text;
            string command = txtCommand.Text;
            string urlCommand = txtUrlCommand.Text;

            string version = (cboVersion.SelectedItem as VersionObject)?.Value;

            string tableInventoryItem = txtTableInventoryItem.Text;
            string tableUnit = txtTableUnit.Text;
            string refInventoryItem = txtRefInventoryItem.Text;
            string refUnit = txtRefUnit.Text;


            CommonService.UpdateSettingAppConfig(CommonConstants.TableInvocie, tableInvoice);
            CommonService.UpdateSettingAppConfig(CommonConstants.TableInvoiceDetail, tableInvoiceDetail);
            CommonService.UpdateSettingAppConfig(CommonConstants.TableVoucherDetail, tableVoucherDetail);
            CommonService.UpdateSettingAppConfig(CommonConstants.TableVoucher, tableVoucher);
            CommonService.UpdateSettingAppConfig(CommonConstants.UsernameLoginWeb, userNameWeb);
            CommonService.UpdateSettingAppConfig(CommonConstants.PasswordLoginWeb, passWordWeb);
            CommonService.UpdateSettingAppConfig(CommonConstants.UrlLogin, urlLogin);
            CommonService.UpdateSettingAppConfig(CommonConstants.UrlSave, urlSave);
            CommonService.UpdateSettingAppConfig(CommonConstants.UrlGetInvoiceByInvoiceAuthId, urlGetInvoice);
            CommonService.UpdateSettingAppConfig(CommonConstants.TimeLoadData, timeGetData);

            CommonService.UpdateSettingAppConfig(CommonConstants.MauSo, mauSo);
            CommonService.UpdateSettingAppConfig(CommonConstants.KyHieu, kyHieu);
            CommonService.UpdateSettingAppConfig(CommonConstants.InvoiceCodeId, invoiceCodeId);
            CommonService.UpdateSettingAppConfig(CommonConstants.UrlRef, urlRef);
            CommonService.UpdateSettingAppConfig(CommonConstants.Command, command);
            CommonService.UpdateSettingAppConfig(CommonConstants.UrlCommand, urlCommand);
            CommonService.UpdateSettingAppConfig(CommonConstants.Version, version);

            CommonService.UpdateSettingAppConfig(CommonConstants.TableInventoryItem, tableInventoryItem);
            CommonService.UpdateSettingAppConfig(CommonConstants.TableUnit, tableUnit);
            CommonService.UpdateSettingAppConfig(CommonConstants.RefInventoryItem, refInventoryItem);
            CommonService.UpdateSettingAppConfig(CommonConstants.RefUnit, refUnit);


            BaseConfig.TableInvocie = tableInvoice;
            BaseConfig.TableInvoiceDetail = tableInvoiceDetail;
            BaseConfig.TableVoucherDetail = tableVoucherDetail;
            BaseConfig.TableVoucher = tableVoucher;
            BaseConfig.UsernameLoginWeb = userNameWeb;
            BaseConfig.PasswordLoginWeb = passWordWeb;
            BaseConfig.UrlLogin = urlLogin;
            BaseConfig.UrlSave = urlSave;
            BaseConfig.UrlGetInvoiceByInvoiceAuthId = urlGetInvoice;
            BaseConfig.TimeGetData = decimal.Parse(timeGetData);
            BaseConfig.MauSo = mauSo;
            BaseConfig.KyHieu = kyHieu;
            BaseConfig.InvoiceCodeId = invoiceCodeId;
            BaseConfig.UrlRef = urlRef;
            BaseConfig.Command = command;
            BaseConfig.UrlCommand = urlCommand;
            BaseConfig.Version = version;

            BaseConfig.TableInventoryItem = tableInventoryItem;
            BaseConfig.TableUnit = tableUnit;
            BaseConfig.RefInventoryItem = refInventoryItem;
            BaseConfig.RefUnit = refUnit;


            SetupRunwithWindow();

            XtraMessageBox.Show("Lưu thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void Check()
        {
            if (string.IsNullOrEmpty(txtTableInvoice.Text) || string.IsNullOrEmpty(txtTableInvoiceDetail.Text) || string.IsNullOrEmpty(txtTableVoucherDetail.Text) || string.IsNullOrEmpty(txtUserLoginWeb.Text) || string.IsNullOrEmpty(txtPassLoginWeb.Text) || string.IsNullOrEmpty(txtUrlLogin.Text) || string.IsNullOrEmpty(txtUrlSave.Text) || string.IsNullOrEmpty(txtUrlGetInvoice.Text) || string.IsNullOrEmpty(txtCommand.Text) || string.IsNullOrEmpty(txtUrlCommand.Text))
            {
                XtraMessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
        }

        private void FrmSettingSystem_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnLayDuLieu_Click(object sender, EventArgs e)
        {
            LoadMauHoaDon();
        }

        private void LoadMauHoaDon()
        {
            try
            {
                List<MauHoaDon> mauHoaDons = CommonService.GetInfoInvoice();
                slokMauHoaDon.Properties.DataSource = mauHoaDons;
                slokMauHoaDon.Properties.DisplayMember = "MauSo";
                slokMauHoaDon.Properties.ValueMember = "Id";
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
                txtKyHieu.Text = mauHoaDon.KyHieu;
                txtMauSo.Text = mauHoaDon.MauSo;
                txtInvoiceCodeId.Text = mauHoaDon.Id;
            }
        }

        private void SetupRunwithWindow()
        {
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (rkApp != null && rkApp.GetValue("MinvoiceLoadDataMisa") == null)
            {
                rkApp.SetValue("MinvoiceLoadDataMisa", Application.ExecutablePath);
            }
            else
            {
                rkApp?.DeleteValue("MinvoiceLoadDataMisa");
            }
        }

        private void txtMst_EditValueChanged(object sender, EventArgs e)
        {
            var mst = txtMst.Text;
            if (!string.IsNullOrEmpty(mst))
            {
                txtUrlLogin.Text = $@"http://{mst}.minvoice.com.vn/api/Account/Login";
                txtUrlGetInvoice.Text = $@"http://{mst}.minvoice.com.vn/api/Invoice/GetById?id=";
                txtUrlSave.Text = $@"http://{mst}.minvoice.com.vn/api/System/Save";
                txtUrlRef.Text = $@"http://{mst}.minvoice.com.vn/api/System/GetDataReferencesByRefId?refId=RF00187";
                txtUrlCommand.Text = $@"http://{mst}.minvoice.com.vn/api/System/ExecuteCommand";
            }

        }

        private void cboVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private List<VersionObject> GetComboBoxVersion()
        {
            List<VersionObject> versions = new List<VersionObject>
            {
                new VersionObject
                {
                    Value = "2012",
                    Name = "MISA SME.NET 2012"
                },
                new VersionObject
                {
                    Value = "2017",
                    Name = "MISA SME.NET 2017"
                }
            };


            return versions;
        }

        private void btnUpdateIsInvoice_Click(object sender, EventArgs e)
        {
            CommonService.UpdateIsInvoice();
            XtraMessageBox.Show("Cập nhật thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}