using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using log4net;
using MinvoiceLoadDataMisa.Config;
using MinvoiceLoadDataMisa.Data;
using Newtonsoft.Json.Linq;

namespace MinvoiceLoadDataMisa.Services
{
    public class MinvoiceService
    {
        private static readonly ILog Log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static WebClient SetupWebClient()
        {
            var userName = BaseConfig.UsernameLoginWeb;
            var passWord = BaseConfig.PasswordLoginWeb;
            var webClient = new WebClient
            {
                Encoding = Encoding.UTF8
            };
            webClient.Headers.Add("Content-Type", "application/json; charset=utf-8");

            LoginService.CreateAuthorization(webClient, userName, passWord);
            return webClient;
        }

        private static SqlConnection GetSqlConnectionMisaTest()
        {
            var connectionString = BaseConfig.ConnectionString;
            Log.Debug(connectionString);
            var sqlConnection = new SqlConnection(connectionString);
            return sqlConnection;
        }

        public static void GetDataFromMisaToMinvoiceTest(int choose = 1)
        {
            try
            {
                CommonService.UpdateSettingAppConfig(CommonConstants.Editmode, "1");
                var ConnectLog = $@"{Properties.Settings.Default.connectLog}";
                SqlConnection _connLog = new SqlConnection(ConnectLog);
                if (_connLog.State == ConnectionState.Closed)
                {
                    _connLog.Open();
                }
                Log.Debug($"CheckConnectLog - ConnectString: {ConnectLog}");
                var sqlConnectionMisa = GetSqlConnectionMisaTest();
                sqlConnectionMisa.Open();
                var where = $"WHERE Isinvoice is null AND VAT_Seri = '{Properties.Settings.Default.KyHieu}' AND {BaseConfig.TablePS_BangKeGTGT}.DauVao={BaseConfig.DauVao} AND {BaseConfig.TableInvocie}.ChungTuID in ('HDBH','HDDV') ORDER BY {BaseConfig.TablePS_BangKeGTGT}.NgayPH, {BaseConfig.TableInvocie}.VAT_So ASC";/*AND (CONVERT(DATE, {BaseConfig.TablePS_BangKeGTGT}.NgayPH) ='{DateTime.Now:yyyy-MM-dd}') */
                //var where = $"WHERE InvSeries = '{BaseConfig.KyHieu}' AND (CONVERT(DATE, InvDate) = '{DateTime.Now:yyyy-MM-dd}') ORDER BY InvDate, InvNo ASC";
                var dataTableInvoice = DataContext.GetDataTableTest(sqlConnectionMisa, BaseConfig.TableInvocie, BaseConfig.TablePS_BangKeGTGT, BaseConfig.TableDM_DTCN, where);
                if (dataTableInvoice.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTableInvoice.Rows)
                    {
                        var SoPhieu = row["SoPhieu"].ToString();
                         
                        if (!CheckInvoiceInMinvoice(SoPhieu, sqlConnectionMisa, Properties.Settings.Default.KyHieu))
                        {
                            var inv_invoiceauth_id = "";
                            var invNo="";
                            var jObjectData = JsonConvert.ConvertData(sqlConnectionMisa, row, inv_invoiceauth_id.ToString(), invNo.ToString());
                            var jArrayData = new JArray { jObjectData };
                            var jObjectMainData = new JObject
                            {
                                {"windowid", "WIN00187"},
                                {"editmode", 1},
                                {"data", jArrayData}
                            };

                            var dataRequest = jObjectMainData.ToString();
                            Log.Debug(dataRequest);
                            var url = BaseConfig.UrlSave;
                            using (var scope = new TransactionScope())
                            {
                                try
                                {
                                    var webClient = SetupWebClient();
                                    var result = webClient.UploadString(url, dataRequest);
                                    var resultResponse = JObject.Parse(result);
                                    if (resultResponse.ContainsKey("ok") && resultResponse.ContainsKey("data"))
                                    {
                                        var _numMisa = row["VAT_So"].ToString();
                                        var _AuthID = resultResponse["data"]["inv_InvoiceAuth_id"].ToString();
                                        var _Type = resultResponse["data"]["inv_invoiceType"].ToString();
                                        var _seri = resultResponse["data"]["inv_invoiceSeries"].ToString();
                                        var _numberinvoice = resultResponse["data"]["inv_invoiceNumber"].ToString();
                                        if (sqlConnectionMisa.State == ConnectionState.Closed)
                                        {

                                            sqlConnectionMisa = GetSqlConnectionMisaTest();
                                            sqlConnectionMisa.Open();

                                        }

                                        // Ghi vào Database Log
                                        if (CheckDBLog() == true)
                                        {
                                            string InserLog = $@"Insert into SaveLogs ([ID],[NumberMisa],[NumberMinvoice],[TimeAdd],[JsonConvert],[Editmode],[Inv_invoiceAuth_ID],[RefID],[Type], [Seri], [result]) " +
                                                $@" VALUES (NEWID(), '{_numMisa}' ,{_numberinvoice} ,GETDATE(), N'{jArrayData}', N'{Properties.Settings.Default.Editmode}',N'{_AuthID}' ,N'{SoPhieu}',N'{_Type}', N'{_seri}',N'Tạo thành công')";
                                            SqlCommand _Insert = new SqlCommand(InserLog, _connLog);
                                            _Insert.ExecuteNonQuery();

                                            // Update IsInvoice vào Database Misa
                                            string commandText = $"Update {BaseConfig.TableInvocie} SET IsInvoice = 1 where SoPhieu='{SoPhieu}'";
                                            SqlCommand command = new SqlCommand(commandText, sqlConnectionMisa);
                                            command.ExecuteNonQuery();
                                            scope.Complete();
                                        }
                                        else
                                        {
                                            // Update IsInvoice vào Database Misa
                                            string commandText = $"Update {BaseConfig.TableInvocie} SET IsInvoice = 1 where SoPhieu='{SoPhieu}'";
                                            SqlCommand command = new SqlCommand(commandText, sqlConnectionMisa);
                                            command.ExecuteNonQuery();
                                            scope.Complete();
                                        }
                                    }
                                    if (resultResponse.ContainsKey("error"))
                                    {
                                        if (CheckDBLog() == true)
                                        {
                                            string InserLog = $@"Insert into SaveLogs ([ID],[NumberMisa],[NumberMinvoice],[TimeAdd],[JsonConvert],[Editmode],[Inv_invoiceAuth_ID],[RefID],[Type], [Seri], [result]) " +
                                        $@" VALUES (NEWID(), N'{row["VAT_So"].ToString()}', N'',GETDATE(), N'{jArrayData}', N'{Properties.Settings.Default.Editmode}',N'' ,N'{SoPhieu}',N'{resultResponse["error"]}', N'{jObjectData["inv_invoiceSeries"]}',N'Tạo hóa đơn thất bại')";
                                            SqlCommand _Insert = new SqlCommand(InserLog, _connLog);
                                            _Insert.ExecuteNonQuery();

                                        }

                                    }
                                }
                                catch (Exception ex)
                                {
                                    XtraMessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
                if (choose == 1)
                {
                    XtraMessageBox.Show("Lấy dữ liệu thành công", "Thông Báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception e)
            {
                XtraMessageBox.Show(e.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static bool CheckInvoiceInMinvoice(string SoPhieu, SqlConnection sqlConnection, string kyhieu)
        {
            var webClient = SetupWebClient();
            var url = BaseConfig.UrlCommand ;

            var CmCheck = BaseConfig.CmCheck;
            var json = "{\"command\":\"" + CmCheck + "\" , parameter:{\"key_api\":\"" + SoPhieu + "\" , \"inv_invoiceSeries\":\"" + kyhieu.Trim() + "\"}}";

            var result = webClient.UploadString(url, json);
            // if (result == "null" || result == "[]")

            //      result = "{\"abc\":\"4y114\"}";
            //var jObject1 = result.Replace("[","{").Replace("]","}");
            var jObject = JArray.Parse(result);
            foreach (var jtoken in jObject)
            {
                if (jtoken["key_api"].ToString() != "")
                {
                    DataContext.UpdateMisa(sqlConnection, SoPhieu, BaseConfig.TableInvocie, jObject);
                    //ck = true;
                   
                    return true;
                }

            }
            return false;

            //var result = webClient.DownloadString(url);
            //JObject jObject = JObject.Parse(result);
            //if (jObject.ContainsKey("inv_InvoiceAuth_id"))
            //{
            //    DataContext.UpdateMisa(sqlConnection, invoiceAuthId, BaseConfig.TableInvocie, jObject);
            //    return true;
            //}
            //return false;
        }

        public static void UpdateInvoice(string invoiceNumber, string Inv_InvoiceCode_id)
        {
            try
            {
                var sqlConnectionMisa = GetSqlConnectionMisaTest();
                sqlConnectionMisa.Open();
                var where = $"WHERE InvNo IN ({invoiceNumber}) AND inv_invoiceCode_id = {Inv_InvoiceCode_id}";
                var dataTableInvoice = DataContext.GetDataTableTest(sqlConnectionMisa, BaseConfig.TableInvocie, BaseConfig.TablePS_BangKeGTGT, BaseConfig.TableDM_DTCN, where);
                if (dataTableInvoice.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTableInvoice.Rows)
                    {
                        var refId = row["RefID"].ToString();
                        var invNo = row["InvNo"].ToString();
                        var inv_invoiceauth_id = "";
                        //var invNo = "";
                        var jObjectData = JsonConvert.ConvertData(sqlConnectionMisa, row,inv_invoiceauth_id.ToString(), invNo.ToString());
                        var jArrayData = new JArray { jObjectData };
                        var jObjectMainData = new JObject
                            {
                                {"windowid", "WIN00187"},
                                {"editmode", 2},
                                {"data", jArrayData}
                            };

                        var dataRequest = jObjectMainData.ToString();
                        var url = BaseConfig.UrlSave;
                        using (var scope = new TransactionScope())
                        {
                            try
                            {
                                var webClient = SetupWebClient();
                                var result = webClient.UploadString(url, dataRequest);
                                var resultResponse = JObject.Parse(result);
                                if (resultResponse.ContainsKey("ok") && resultResponse.ContainsKey("data"))
                                {
                                    var jToken = resultResponse["data"];
                                    DataContext.UpdateMisa(sqlConnectionMisa, refId, BaseConfig.TableInvocie, jToken);
                                    //if (BaseConfig.Version == "2017")
                                    //{
                                    //    DataContext.UpdateMisaVoucher(sqlConnectionMisa, refId, BaseConfig.TableVoucher, BaseConfig.TableVoucherDetail, jToken);
                                    //}
                                    scope.Complete();
                                    XtraMessageBox.Show($"Cập nhật hóa đơn {invNo} thành công", "Thông Báo", MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                                }
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show($"Cập nhật hóa đơn {invNo} không thành công. {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                XtraMessageBox.Show(e.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void UpdateInvoice(List<InvoiceObject> invoiceObjects)
        {
            try
            {
                string SoPhieu = "";
                foreach (var invoiceObject in invoiceObjects)
                {
                    SoPhieu += $"'{invoiceObject.key_api}' ,";
                }



                SoPhieu = SoPhieu.Substring(0, SoPhieu.Length - 1);
                var sqlConnectionMisa = GetSqlConnectionMisaTest();
                sqlConnectionMisa.Open();
                var ConnectLog = $@"{Properties.Settings.Default.connectLog}";
                SqlConnection _connLog = new SqlConnection(ConnectLog);
                if (_connLog.State == ConnectionState.Closed)
                {
                    _connLog.Open();
                }
                Log.Debug($"UpdateInvoice - SoPhieu: {SoPhieu}");
                var where = $"WHERE {BaseConfig.TableInvocie}.SoPhieu IN ({SoPhieu})";
                var dataTableInvoice = DataContext.GetDataTableTest(sqlConnectionMisa, BaseConfig.TableInvocie, BaseConfig.TablePS_BangKeGTGT, BaseConfig.TableDM_DTCN, where);
                var _id = BaseConfig.IDUpdate;
                if (dataTableInvoice.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTableInvoice.Rows)
                    {
                        var key_api = row["SoPhieu"].ToString();

                        var invoiceObject = invoiceObjects.Single(x => x.key_api == key_api);
                        var inv_invoiceauth_id = invoiceObject.InvInvoiceAuthId;
                        
                        var invNo = invoiceObject.InvoiceNumber;
                        
                        var jObjectData = JsonConvert.ConvertData(sqlConnectionMisa, row, inv_invoiceauth_id.ToString(), invNo.ToString());
                        var jArrayData = new JArray { jObjectData };
                        var jObjectMainData = new JObject
                            {
                                {"windowid", "WIN00187"},
                                {"Inv_InvoiceCode_id", _id},
                                {"editmode", 2},
                                {"data", jArrayData}
                            };

                        var dataRequest = jObjectMainData.ToString();

                        Log.Debug(dataRequest);

                        var url = BaseConfig.UrlSave;
                        using (var scope = new TransactionScope())
                        {
                            try
                            {
                                var webClient = SetupWebClient();
                                var result = webClient.UploadString(url, dataRequest);
                                var resultResponse = JObject.Parse(result);
                                if (resultResponse.ContainsKey("ok") && resultResponse.ContainsKey("data"))
                                {
                                    if (CheckDBLog() == true)
                                    {
                                        string InserLog = $@"Insert into SaveLogs ([ID],[NumberMisa],[NumberMinvoice],[TimeAdd],[JsonConvert],[Editmode],[Inv_invoiceAuth_ID],[RefID],[Type], [Seri], [result]) " +
                                    $@" VALUES (NEWID(), N'{row["VAT_So"].ToString()}', N'{invNo.ToString()}',GETDATE(), N'{jArrayData}', N'{Properties.Settings.Default.Editmode}',N'{inv_invoiceauth_id.ToString()}' ,N'{key_api}',N'', N'{jObjectData["inv_invoiceSeries"]}',N'Cập nhật thành công')";
                                        SqlCommand _Insert = new SqlCommand(InserLog, _connLog);
                                        _Insert.ExecuteNonQuery();
                                        scope.Complete();
                                    } else
                                    {
                                        scope.Complete();
                                    }
                                    XtraMessageBox.Show($"Cập nhật hóa đơn {invNo} thành công", "Thông Báo", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                                }
                                if(resultResponse.ContainsKey("error"))
                                {
                                    if (CheckDBLog() == true)
                                    {
                                        string InserLog = $@"Insert into SaveLogs ([ID],[NumberMisa],[NumberMinvoice],[TimeAdd],[JsonConvert],[Editmode],[Inv_invoiceAuth_ID],[RefID],[Type], [Seri], [result]) " +
                                    $@" VALUES (NEWID(), N'{row["VAT_So"].ToString()}', N'{invNo.ToString()}',GETDATE(), N'{jArrayData}', N'{Properties.Settings.Default.Editmode}',N'{inv_invoiceauth_id.ToString()}' ,N'{key_api}',N'{resultResponse["error"]}', N'{jObjectData["inv_invoiceSeries"]}',N'Cập nhật thất bại')";
                                        SqlCommand _Insert = new SqlCommand(InserLog, _connLog);
                                        _Insert.ExecuteNonQuery();
                                      
                                    }
                                    XtraMessageBox.Show($"Cập nhật hóa đơn {invNo} thất bại! \n {resultResponse["error"]}", "Lỗi", MessageBoxButtons.OK,
                                     MessageBoxIcon.Error);
                                }
                            
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show($"Cập nhật hóa đơn {invNo} không thành công. {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                XtraMessageBox.Show(e.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region

        public static void GetDataFromMisaToMinvoiceByDay(string day, string day1, string editmode)
        {
            try
            {

                var ConnectLog =$@"{Properties.Settings.Default.connectLog}";
                SqlConnection _connLog = new SqlConnection(ConnectLog);
                if(_connLog.State == ConnectionState.Closed)
                {
                    _connLog.Open();
                }
                Log.Debug($"CheckConnectLog - ConnectString: {ConnectLog}");
                var sqlConnectionMisa = GetSqlConnectionMisaTest();
                sqlConnectionMisa.Open();
                //var where = $"WHERE Isinvoice is null AND VAT_Seri = '{Properties.Settings.Default.KyHieu}' AND {BaseConfig.TablePS_BangKeGTGT}.DauVao={BaseConfig.DauVao} AND {BaseConfig.TableInvocie}.ChungTuID in ('HDBH','HDDV') ORDER BY {BaseConfig.TablePS_BangKeGTGT}.NgayPH, {BaseConfig.TableInvocie}.VAT_So ASC";/*AND (CONVERT(DATE, {BaseConfig.TablePS_BangKeGTGT}.NgayPH) ='{DateTime.Now:yyyy-MM-dd}') */
                //var where = $"WHERE InvSeries = '{BaseConfig.KyHieu}' AND (CONVERT(DATE, InvDate) = '{DateTime.Now:yyyy-MM-dd}') ORDER BY InvDate, InvNo ASC";
             
                var where = $"WHERE Isinvoice is null AND VAT_Seri = '{Properties.Settings.Default.KyHieu}' AND {BaseConfig.TablePS_BangKeGTGT}.DauVao={BaseConfig.DauVao} AND {BaseConfig.TableInvocie}.ChungTuID in ('HDBH','HDDV') AND (CONVERT(DATE, {BaseConfig.TablePS_BangKeGTGT}.NgayPH) between '{day}' and '{day1}')  ORDER BY {BaseConfig.TablePS_BangKeGTGT}.NgayPH, {BaseConfig.TableInvocie}.VAT_So ASC";
                var dataTableInvoice = DataContext.GetDataTableTest(sqlConnectionMisa, BaseConfig.TableInvocie, BaseConfig.TablePS_BangKeGTGT, BaseConfig.TableDM_DTCN, where);
                if (dataTableInvoice.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTableInvoice.Rows)
                    {
                        var SoPhieu = row["SoPhieu"].ToString();
                        if (!CheckInvoiceInMinvoice(SoPhieu, sqlConnectionMisa, Properties.Settings.Default.KyHieu))
                        {
                            var inv_invoiceauth_id = "";
                            var invNo = "";
                            var jObjectData = JsonConvert.ConvertData(sqlConnectionMisa, row, inv_invoiceauth_id.ToString(), invNo.ToString());
                            var jArrayData = new JArray { jObjectData };
                            var jObjectMainData = new JObject
                            {
                                {"windowid", "WIN00187"},
                                {"editmode", Properties.Settings.Default.Editmode},
                                {"data", jArrayData}
                            };

                            var dataRequest = jObjectMainData.ToString();
                            Log.Debug(dataRequest);
                            var url = BaseConfig.UrlSave;
                            using (var scope = new TransactionScope())
                            {
                                try
                                {
                                    var webClient = SetupWebClient();
                                    var result = webClient.UploadString(url, dataRequest);
                                    var resultResponse = JObject.Parse(result);
                                    if (resultResponse.ContainsKey("ok") && resultResponse.ContainsKey("data"))
                                    {
                                        var _numMisa = row["VAT_So"].ToString();
                                        var _AuthID = resultResponse["data"]["inv_InvoiceAuth_id"].ToString();
                                        var _Type = resultResponse["data"]["inv_invoiceType"].ToString();
                                        var _seri = resultResponse["data"]["inv_invoiceSeries"].ToString();
                                        var _numberinvoice = resultResponse["data"]["inv_invoiceNumber"].ToString();
                                        if (sqlConnectionMisa.State == ConnectionState.Closed)
                                        {

                                            sqlConnectionMisa = GetSqlConnectionMisaTest();
                                            sqlConnectionMisa.Open();

                                        }

                                        // Ghi vào Database Log
                                        if (CheckDBLog() == true)
                                        {
                                            string InserLog = $@"Insert into SaveLogs ([ID],[NumberMisa],[NumberMinvoice],[TimeAdd],[JsonConvert],[Editmode],[Inv_invoiceAuth_ID],[RefID],[Type], [Seri], [result]) " +
                                                $@" VALUES (NEWID(), '{_numMisa}' ,{_numberinvoice} ,GETDATE(), N'{jArrayData}', N'{Properties.Settings.Default.Editmode}',N'{_AuthID}' ,N'{SoPhieu}',N'{_Type}', N'{_seri}',N'Tạo thành công')";
                                            SqlCommand _Insert = new SqlCommand(InserLog, _connLog);
                                            _Insert.ExecuteNonQuery();

                                            // Update IsInvoice vào Database Misa
                                            string commandText = $"Update {BaseConfig.TableInvocie} SET IsInvoice = 1 where SoPhieu='{SoPhieu}'";
                                            SqlCommand command = new SqlCommand(commandText, sqlConnectionMisa);
                                            command.ExecuteNonQuery();
                                            scope.Complete();
                                        }
                                        else
                                        {
                                            // Update IsInvoice vào Database Misa
                                            string commandText = $"Update {BaseConfig.TableInvocie} SET IsInvoice = 1 where SoPhieu='{SoPhieu}'";
                                            SqlCommand command = new SqlCommand(commandText, sqlConnectionMisa);
                                            command.ExecuteNonQuery();
                                            scope.Complete();
                                        }
                                    }
                                    if (resultResponse.ContainsKey("error"))
                                    {
                                        if (CheckDBLog() == true)
                                        {
                                            string InserLog = $@"Insert into SaveLogs ([ID],[NumberMisa],[NumberMinvoice],[TimeAdd],[JsonConvert],[Editmode],[Inv_invoiceAuth_ID],[RefID],[Type], [Seri], [result]) " +
                                        $@" VALUES (NEWID(), N'{row["VAT_So"].ToString()}', N'',GETDATE(), N'{jArrayData}', N'{Properties.Settings.Default.Editmode}',N'' ,N'{SoPhieu}',N'{resultResponse["error"]}', N'{jObjectData["inv_invoiceSeries"]}',N'Tạo hóa đơn thất bại')";
                                            SqlCommand _Insert = new SqlCommand(InserLog, _connLog);
                                            _Insert.ExecuteNonQuery();

                                        }
                                        
                                    }
                                }
                                catch (Exception ex)
                                {
                                    XtraMessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }

                        

                        //XtraMessageBox.Show("Lấy dữ liệu thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    XtraMessageBox.Show("Lấy dữ liệu thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
                else
                {
                    XtraMessageBox.Show("Không tìm thấy dữ liệu", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                sqlConnectionMisa.Close();
            }
            catch (Exception e)
            {
                XtraMessageBox.Show(e.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
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

                    Log.Debug($"CheckDBLog - conn: {conn}");
                    Log.Debug($"CheckDBLog - commandText: {commandText}");


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
        #endregion
    }
}