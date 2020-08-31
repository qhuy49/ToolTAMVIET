using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using log4net;
using MinvoiceLoadDataMisa.Config;
using MinvoiceLoadDataMisa.Services;

namespace MinvoiceLoadDataMisa.Forms
{
    public partial class FrmConnectDatabase : DevExpress.XtraEditors.XtraForm
    {
        private static readonly ILog Log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string _connectionString = "";
        private SqlConnection _sqlConnection;
        public bool CheckConnectionString = false;

        public FrmConnectDatabase()
        {
            InitializeComponent();
        }

        private void FrmConnectDatabase_Load(object sender, EventArgs e)
        {
            txtServerName.Text = Properties.Settings.Default.serverName;
            txtUsername.Text = Properties.Settings.Default.userName;
            txtPassword.Text = Properties.Settings.Default.passWord;
            lokDatabaseName.Text = Properties.Settings.Default.database;
        }

        private void cboeAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            var value = (sender as ComboBoxEdit)?.SelectedIndex;
            lokDatabaseName.Properties.DataSource = null;
            lokDatabaseName.Enabled = false; if (value == 0)
            {
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
            }
            else
            {
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;
                var valueUsername = txtUsername.Text;
                if (string.IsNullOrEmpty(valueUsername))
                {
                    btnLoadDatabaseName.Enabled = false;
                }

            }

        }


        private void lokDatabaseName_EditValueChanged(object sender, EventArgs e)
        {
            var value = sender as LookUpEdit;
            if (value != null)
            {
                btnOk.Enabled = !string.IsNullOrEmpty(value.Text);
            }
        }

        private void LoadDatabaseName()
        {
            try
            {

                string serverName = txtServerName.Text;
                _connectionString = $"Server={serverName};";
                var cboAuthentication = cboeAuthentication.SelectedIndex;
                if (cboAuthentication == 0)
                {
                    _connectionString += "Trusted_Connection=True;";
                }
                else
                {
                    string userName = txtUsername.Text;
                    string passWord = txtPassword.Text;
                    _connectionString += $"User Id={userName}; Password={passWord}; ";
                }

                _sqlConnection = new SqlConnection(_connectionString);
                _sqlConnection.Open();

                Log.Debug(_connectionString);

                string command = "SELECT name from sys.databases";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command, _sqlConnection);
                DataTable table = new DataTable();
                sqlDataAdapter.Fill(table);

                List<ServerName> lstServerName = GetListDatabaseName(table);
                lokDatabaseName.Properties.DataSource = lstServerName;
                lokDatabaseName.Properties.DisplayMember = "Name";
                lokDatabaseName.Properties.ValueMember = "Name";

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                lokDatabaseName.Properties.DataSource = null;
            }
            lokDatabaseName.Enabled = true;
        }

        private List<ServerName> GetListDatabaseName(DataTable table)
        {
            List<ServerName> lstServerName = new List<ServerName>();
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    lstServerName.Add(new ServerName
                    {
                        Name = row["name"].ToString()

                    });
                }
            }
            return lstServerName;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (CheckConnection())
            {
                string serverName = txtServerName.Text;
                string database = lokDatabaseName.Text;
                string userName = txtUsername.Text;
                string passWord = txtPassword.Text;
                _connectionString = cboeAuthentication.SelectedIndex == 0 ? $"Server={serverName}; Database={database}; Trusted_Connection=True;" : $"Server={serverName}; Database={database}; User Id={userName}; Password = {passWord}; ";

                Log.Debug(_connectionString);

                if (!string.IsNullOrEmpty(_connectionString))
                {
                    CommonService.UpdateSettingAppConfig("ConnectionString", _connectionString);
                    BaseConfig.ConnectionString = _connectionString;
                    CheckConnectionString = true;
                   
                }
                Properties.Settings.Default.serverName = serverName;
                Properties.Settings.Default.userName = userName;
                Properties.Settings.Default.passWord = passWord;
                Properties.Settings.Default.database = database;
                Properties.Settings.Default.Save();
                Close();


            }
            else
            {
                XtraMessageBox.Show("Kết nối thất bại. Vui lòng kiểm tra lại", "Thông Báo", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void brnCancel_Click(object sender, EventArgs e)
        {
            Close();
            CheckConnectionString = false;
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lokDatabaseName.Text))
            {
                if (CheckConnection())
                {
                    XtraMessageBox.Show("Kết nối thành công", "Thông Báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Kết nối thất bại. Vui lòng kiểm tra lại", "Thông Báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                XtraMessageBox.Show("Chưa chọn database name", "Cảnh báo", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void txtServerName_EditValueChanged(object sender, EventArgs e)
        {
            var valueTxtServerName = sender as TextEdit;
            var valueComboBoxAuthentication = cboeAuthentication.SelectedIndex;
            if (valueComboBoxAuthentication == 0)
            {
                if (valueTxtServerName != null)
                {
                    btnLoadDatabaseName.Enabled = !string.IsNullOrEmpty(valueTxtServerName.Text);
                }
            }
            else
            {
                var valueTxtUser = txtUsername.Text;
                if (valueTxtServerName != null)
                {
                    btnLoadDatabaseName.Enabled = !string.IsNullOrEmpty(valueTxtUser) && !string.IsNullOrEmpty(valueTxtServerName.Text);
                }
            }
        }

        private void txtUsername_EditValueChanged(object sender, EventArgs e)
        {
            var valueTxtUsert = sender as TextEdit;
            var valueTxtServerName = txtServerName.Text;
            if (valueTxtUsert != null)
            {
                btnLoadDatabaseName.Enabled = !string.IsNullOrEmpty(valueTxtUsert.Text) && !string.IsNullOrEmpty(valueTxtServerName);
            }
        }

        private void btnLoadDatabaseName_Click(object sender, EventArgs e)
        {
            LoadDatabaseName();
        }

        private bool CheckConnection()
        {
            return _sqlConnection.State == ConnectionState.Open;
        }
    }

    public class ServerName
    {
        public string Name { get; set; }
    }
}