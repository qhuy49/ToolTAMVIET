using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.Win32;
using MinvoiceLoadDataMisa.Config;
using MinvoiceLoadDataMisa.Data;
using MinvoiceLoadDataMisa.Services;
using System.Data;
using System.IO;

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
            //txtMauSo.Text = BaseConfig.MauSo;
            //txtKyHieu.Text = BaseConfig.KyHieu;
            txtUrlRef.Text = BaseConfig.UrlRef;
            //txtInvoiceCodeId.Text = BaseConfig.InvoiceCodeId;
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

            //CommonService.UpdateSettingAppConfig(CommonConstants.MauSo, mauSo);
            //CommonService.UpdateSettingAppConfig(CommonConstants.KyHieu, kyHieu);
            //CommonService.UpdateSettingAppConfig(CommonConstants.InvoiceCodeId, invoiceCodeId);
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
            //BaseConfig.MauSo = mauSo;
            //BaseConfig.KyHieu = kyHieu;
            //BaseConfig.InvoiceCodeId = invoiceCodeId;
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

        private void btnLog_Click(object sender, EventArgs e)
        {
            
            var _Connect = Properties.Settings.Default.ConnectionString;
            SqlConnection Connect = new SqlConnection(_Connect);
            var connectLog = $"Server={Properties.Settings.Default.serverName}; Database=LogMinvoice; User Id={Properties.Settings.Default.userName}; Password = {Properties.Settings.Default.passWord}; ";
            Connect.Open();
            if (string.IsNullOrEmpty(Properties.Settings.Default.passWord))
            {
                connectLog = $"Server={Properties.Settings.Default.serverName}; Database=LogMinvoice; Trusted_Connection=True;";
            }
           


            if (CheckDBLog() == true)
            {
                XtraMessageBox.Show("Tạo Log thành công");
            }
            else
            {

                DataTable table = new DataTable();
                //Lấy đường dẫn lưu Database Misa trên server
                string _SelectFolder = "SELECT physical_name FROM sys.database_files WHERE physical_name LIKE '%.mdf'";
                SqlDataAdapter adapter = new SqlDataAdapter(_SelectFolder, Connect);
                adapter.Fill(table);

                var Arr = table.Rows[0]["physical_name"].ToString();
                string[] FolderArr = Arr.Split('\\');

                //Ghép đường dẫn
                var Folder1 = FolderArr[0];
                var Folder2 = FolderArr[1];

                // Tạo Database Log trên Folder Server
                SqlCommand command = Connect.CreateCommand();
                command.CommandText = $@" USE [master] " +

                    $@" CREATE DATABASE [LogMinvoice] ON  PRIMARY " +
                    $@" ( NAME = N'LogMinvoice', FILENAME = N'{Folder1}\{Folder2}\LogMinvoice.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB ) " +
                    $@"  LOG ON " +
                    $@" ( NAME = N'LogMinvoice_log', FILENAME = N'{Folder1}\{Folder2}\LogMinvoice.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%) " +

                    $@" ALTER DATABASE [LogMinvoice] SET COMPATIBILITY_LEVEL = 100";

                command.ExecuteNonQuery();

                SqlCommand _AlterDB = Connect.CreateCommand();
                _AlterDB.CommandText = Config.CreateLogs._AlterDB.ToString();
                _AlterDB.ExecuteNonQuery();
                




                XtraMessageBox.Show("Create Database LogMinvoice Successfully", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            CommonService.UpdateSettingAppConfig(CommonConstants.connectLog, connectLog);

        }

        //Check Đã có Database Log hay chưa 
        private static bool CheckDBLog()
        {
            string conn = Properties.Settings.Default.ConnectionString;
            DataTable table = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(conn))
            {
                try
                {
                    sqlConnection.Open();
                    string commandText = $@"SELECT name FROM master.dbo.sysdatabases WHERE name = N'LogMinvoice'";

                    SqlDataAdapter adapter = new SqlDataAdapter(commandText, sqlConnection);
                    adapter.Fill(table);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return table.Rows.Count > 0;
        }
    }
}