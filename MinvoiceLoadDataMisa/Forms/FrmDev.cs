using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MinvoiceLoadDataMisa.Services;
using MinvoiceLoadDataMisa.Config;
using MinvoiceLoadDataMisa.Data;
using MinvoiceLoadDataMisa.Properties;
using System.Data.SqlClient;
using DevExpress.XtraEditors;
using Newtonsoft.Json.Linq;

namespace MinvoiceLoadDataMisa.Forms
{
    public partial class FrmDev : Form
    {

        int num = 1;
        SqlConnection _conn = new SqlConnection();
        SqlConnection connection = new SqlConnection();


        public FrmDev()
        {
            InitializeComponent();
            var a = Properties.Settings.Default.connectLog;
            connection = new SqlConnection(Properties.Settings.Default.connectLog);
            _conn = new SqlConnection(Properties.Settings.Default.ConnectionString);
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (_conn.State == ConnectionState.Closed)
            {
                _conn.Open();
            }
            if ( !int.TryParse(num1.Text, out num))
            {
                XtraMessageBox.Show("Dữ liệu đầu vào sai định dạng");
            }
            else
            {
                if (!int.TryParse(num2.Text, out num))
                {
                    XtraMessageBox.Show("Dữ liệu đầu vào sai định dạng");
                }
                else
                {
                    string Querry = "";
                    if (string.IsNullOrEmpty(num2.ToString()))
                    {
                         Querry = $@"Update {Properties.Settings.Default.TableInvocie} SET IsInvoice = null where invNo = {num1.Text} AND InvSeries = '{Properties.Settings.Default.KyHieu}' ";
                    } else
                    {
                         Querry = $@"Update {Properties.Settings.Default.TableInvocie} SET IsInvoice = null where InvSeries = '{Properties.Settings.Default.KyHieu}' AND invNo between {num1.Text} AND {num2.Text}";
                    }
                    
                    SqlCommand cmd = new SqlCommand(Querry, _conn);
                    cmd.ExecuteNonQuery();
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    string insert = $@"Insert into SaveLogs ([ID],[NumberMisa],[NumberMinvoice],[TimeAdd],[JsonConvert],[Editmode],[Inv_invoiceAuth_ID],[result]) " +
                                        $@" VALUES (NEWID(), NULL ,NULL ,GETDATE(), NULL, NULL, NULL, " +
                                        $@"N'Reload dữ liệu từ Misa lên Minvoice vì trc đó đã làm thao tác xóa hóa đơn trên Minvoice invNo between {num1.Text} AND {num2.Text} AND ký hiệu = {Properties.Settings.Default.KyHieu}')";
                    SqlCommand insertcmd = new SqlCommand(insert, connection);
                    insertcmd.ExecuteNonQuery();
                    connection.Close();
                    XtraMessageBox.Show("Cập nhật thành công, vui lòng Load lại dữ liệu lên Minvoice");
                }   
            }
            _conn.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(_conn.State == ConnectionState.Closed)
            {
                _conn.Open();
            }
           
         

            var date1Convert = date1.Value.ToString("yyyy-MM-dd");
            var date2Convert = date2.Value.ToString("yyyy-MM-dd");

            string Querry = $@"Update {Properties.Settings.Default.TableInvocie} SET IsInvoice = null where InvSeries = '{Properties.Settings.Default.KyHieu}' AND invDate between '{date1Convert}' AND '{date2Convert}'";
            SqlCommand cmd = new SqlCommand(Querry, _conn);
            cmd.ExecuteNonQuery();

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            string insert = $@"Insert into SaveLogs ([ID],[NumberMisa],[NumberMinvoice],[TimeAdd],[JsonConvert],[Editmode],[Inv_invoiceAuth_ID],[result]) " +
                                $@" VALUES (NEWID(), NULL ,NULL ,GETDATE(), NULL, NULL, NULL, " +
                                $@"N'Reload dữ liệu từ Misa lên Minvoice vì trc đó đã làm thao tác xóa hóa đơn trên Minvoice invDate between {date1Convert} AND {date2Convert} AND ký hiệu = {Properties.Settings.Default.KyHieu}')";
            SqlCommand insertcmd = new SqlCommand(insert, connection);
            insertcmd.ExecuteNonQuery();
            connection.Close();
            XtraMessageBox.Show("Cập nhật thành công, vui lòng Load lại dữ liệu lên Minvoice");

            _conn.Close();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            if (_conn.State == ConnectionState.Closed)
            {
                _conn.Open();
            }
            if (!int.TryParse(num3.Text, out num))
            {
                XtraMessageBox.Show("Dữ liệu đầu vào sai định dạng");
            }
            else
            {
                string commandText = $@"Select RefID, IsInvoice from SAinvoice where InvNo = {num3.Text} AND InvSeries = '{Properties.Settings.Default.KyHieu}' ";
                SqlDataAdapter adapter = new SqlDataAdapter(commandText, _conn);
                adapter.Fill(table);

                if(table.Rows.Count > 0)
                {
                    var IsInvoice = table.Rows[0]["IsInvoice"].ToString();
                    var RefID = table.Rows[0]["RefID"].ToString();
                    if (IsInvoice == "0")
                    {
                        XtraMessageBox.Show("Hóa đơn không được tạo từ Tool do giá trị Column IsInvoice đang = 0");
                    }
                    else
                    {
                        var webClient = MinvoiceService.SetupWebClient();
                        var url = BaseConfig.UrlCommand;
                        var command = "FixMisa";
                        JObject json = new JObject
                    {
                       {"command", command },
                         {  "parameter", new JObject
                              {
                                   {"RefID", RefID.ToString() },
                              }
                         }
                    };
                        List<InvoiceObject> invoiceObjects = new List<InvoiceObject>();
                        var rs = webClient.UploadString(url, json.ToString());
                        var result = JArray.Parse(rs);
                        if (result.Count > 0)
                        {
                            XtraMessageBox.Show(" Hóa đơn được cập nhật tự động từ LoadDataMisa");
                        }
                        else
                        {
                            XtraMessageBox.Show(" Hóa đơn không được tạo LoadDataMisa vì RefID khác với Inv_InvoiceAuth_id");
                        }
                    }
                }   
            }
            _conn.Close();
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            if (_conn.State == ConnectionState.Closed)
            {
                _conn.Open();
            }
            if (!int.TryParse(num4.Text, out num))
            {
                XtraMessageBox.Show("Dữ liệu đầu vào sai định dạng");
            }
            else
            {
                string commandText = $@"Select RefID, IsInvoice from SAinvoice where InvNo = {num4.Text} AND InvSeries = '{Properties.Settings.Default.KyHieu}' ";
                SqlDataAdapter adapter = new SqlDataAdapter(commandText, _conn);
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    var IsInvoice = table.Rows[0]["IsInvoice"].ToString();
                    var RefID = table.Rows[0]["RefID"].ToString();
                    if (IsInvoice == "0")
                    {
                        XtraMessageBox.Show("Hóa đơn không được tạo từ Tool do giá trị Column IsInvoice đang = 0");
                    }
                    else
                    {
                        var webClient = MinvoiceService.SetupWebClient();
                        var url = BaseConfig.UrlCommand;
                        var command = "FixMisa";
                        JObject json = new JObject
                    {
                       {"command", command },
                         {  "parameter", new JObject
                              {
                                   {"RefID", RefID.ToString() },
                              }
                         }
                    };
                        List<InvoiceObject> invoiceObjects = new List<InvoiceObject>();
                        var rs = webClient.UploadString(url, json.ToString());
                        var result = JArray.Parse(rs);
                        //var _number = result[0]["inv_invoiceNumber"].ToString();
                        if (result.Count > 0)
                        {
                            var _number = result[0]["inv_invoiceNumber"].ToString();
                            XtraMessageBox.Show($@"Hóa đơn được tạo trên M-Invoice với số {_number} ");
                        }
                        else
                        {
                            XtraMessageBox.Show("Không thể kiểm tra vì hóa đơn không được Load tự động từ LoadDataMisa");
                        }
                    }
                }
            }
            connection.Close();
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            if(connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            DataTable ReadLog = new DataTable();
            string commandText = $@"Select [NumberMisa] , [TimeAdd] , [JsonConvert] , [Editmode] , [Type], [Seri]  From [dbo].[SaveLogs] where [NumberMisa] like '%{num5.Text}' AND Seri = '{Properties.Settings.Default.KyHieu}' ";
            SqlDataAdapter adapter = new SqlDataAdapter(commandText, connection);
            adapter.Fill(ReadLog);
            GridView read = new GridView();
            read._readLog2 = ReadLog;
            read.ShowDialog();
            connection.Close();
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            DataTable ReadLog = new DataTable();
            string commandText = $@"Select [NumberMisa] , [TimeAdd] , [JsonConvert] , [Editmode] , [Type], [Seri]  From [dbo].[SaveLogs] "+
            $@"where [NumberMisa] = '{num6.Text}' AND Seri = '{Properties.Settings.Default.KyHieu}' "+
            $@" AND TimeAdd = (SELECT MAX(TimeAdd)FROM [dbo].[SaveLogs] WHERE [NumberMisa] = '{num6.Text}')";

            SqlDataAdapter adapter = new SqlDataAdapter(commandText, connection);
            adapter.Fill(ReadLog);
            GridView read = new GridView();
            read._readLog2 = ReadLog;
            read.ShowDialog();
            connection.Close();
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            DataTable ReadLog = new DataTable();
            string commandText = $@"Select [NumberMisa] , [TimeAdd] , [JsonConvert] , [Editmode] , [Type], [Seri]  From [dbo].[SaveLogs]";
            SqlDataAdapter adapter = new SqlDataAdapter(commandText, connection);
            adapter.Fill(ReadLog);
            GridView read = new GridView();
            read._readLog2 = ReadLog;
            read.ShowDialog();
            connection.Close();
        }
    }
}
