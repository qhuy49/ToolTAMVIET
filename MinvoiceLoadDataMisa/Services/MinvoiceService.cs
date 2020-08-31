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
                var sqlConnectionMisa = GetSqlConnectionMisaTest();
                sqlConnectionMisa.Open();
                var where = $"WHERE InvSeries = '{BaseConfig.KyHieu}' AND (CONVERT(DATE, InvDate) = '{DateTime.Now:yyyy-MM-dd}') ORDER BY InvDate, InvNo DESC";
                var dataTableInvoice = DataContext.GetDataTableTest(sqlConnectionMisa, BaseConfig.TableInvocie, where);
                if (dataTableInvoice.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTableInvoice.Rows)
                    {
                        var refId = row["RefID"].ToString();
                        if (!CheckInvoiceInMinvoice(refId, sqlConnectionMisa))
                        {
                            var jObjectData = JsonConvert.ConvertData(sqlConnectionMisa, row);
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
                                        scope.Complete();
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
        public static bool CheckInvoiceInMinvoice(string invoiceAuthId, SqlConnection sqlConnection)
        {
            var webClient = SetupWebClient();
            var url = BaseConfig.UrlGetInvoiceByInvoiceAuthId + invoiceAuthId;
            var result = webClient.DownloadString(url);
            JObject jObject = JObject.Parse(result);
            if (jObject.ContainsKey("inv_InvoiceAuth_id"))
            {
                DataContext.UpdateMisa(sqlConnection, invoiceAuthId, BaseConfig.TableInvocie, jObject);
                return true;
            }
            return false;
        }

        public static void UpdateInvoice(string invoiceNumber, string Inv_InvoiceCode_id)
        {
            try
            {
                var sqlConnectionMisa = GetSqlConnectionMisaTest();
                sqlConnectionMisa.Open();
                var where = $"WHERE InvNo IN ({invoiceNumber}) AND inv_invoiceCode_id = {Inv_InvoiceCode_id}";
                var dataTableInvoice = DataContext.GetDataTableTest(sqlConnectionMisa, BaseConfig.TableInvocie, where);
                if (dataTableInvoice.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTableInvoice.Rows)
                    {
                        var refId = row["RefID"].ToString();
                        var invNo = row["InvNo"].ToString();

                        var jObjectData = JsonConvert.ConvertData(sqlConnectionMisa, row);
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
                                    if (BaseConfig.Version == "2017")
                                    {
                                        DataContext.UpdateMisaVoucher(sqlConnectionMisa, refId, BaseConfig.TableVoucher, BaseConfig.TableVoucherDetail, jToken);
                                    }
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
                string invInvoiceAuthId = "";
                foreach (var invoiceObject in invoiceObjects)
                {
                    invInvoiceAuthId += $"'{invoiceObject.InvInvoiceAuthId}' ,";
                }

             

                invInvoiceAuthId = invInvoiceAuthId.Substring(0, invInvoiceAuthId.Length - 1);
                var sqlConnectionMisa = GetSqlConnectionMisaTest();
                sqlConnectionMisa.Open();
                Log.Debug($"UpdateInvoice - invInvoiceAuthId: {invInvoiceAuthId}");
                var where = $"WHERE RefID IN ({invInvoiceAuthId})";
                var dataTableInvoice = DataContext.GetDataTableTest(sqlConnectionMisa, BaseConfig.TableInvocie, where);
                var _id = BaseConfig.IDUpdate;
                if (dataTableInvoice.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTableInvoice.Rows)
                    {
                        var refId = row["RefID"].ToString();

                        var invoiceObject = invoiceObjects.Single(x => x.InvInvoiceAuthId == refId);
                        row["InvNo"] = invoiceObject.InvoiceNumber;
                        
                        var invNo = invoiceObject.InvoiceNumber;
                        
                        var jObjectData = JsonConvert.ConvertData(sqlConnectionMisa, row);
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

        #region

        public static void GetDataFromMisaToMinvoiceByDay(string day, string day1)
        {
            try
            {
                var sqlConnectionMisa = GetSqlConnectionMisaTest();
                sqlConnectionMisa.Open();
                var where = $"WHERE Isinvoice is null AND InvSeries = '{BaseConfig.KyHieu}' AND CONVERT(DATE, InvDate) between '{day}' and '{day1}' ORDER BY InvNo, InvDate ASC";
                var dataTableInvoice = DataContext.GetDataTableTest(sqlConnectionMisa, BaseConfig.TableInvocie, where);
                if (dataTableInvoice.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTableInvoice.Rows)
                    {
                        var refId = row["RefID"].ToString();
                        if (!CheckInvoiceInMinvoice(refId, sqlConnectionMisa))
                        {
                            var jObjectData = JsonConvert.ConvertData(sqlConnectionMisa, row);
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
                                        if(sqlConnectionMisa.State == ConnectionState.Closed)
                                        {

                                            sqlConnectionMisa = GetSqlConnectionMisaTest();
                                            sqlConnectionMisa.Open();

                                        }
                                        string commandText = $"Update {BaseConfig.TableInvocie} SET IsInvoice = 1 where RefID='{refId}'";
                                        SqlCommand command = new SqlCommand(commandText, sqlConnectionMisa);
                                        command.ExecuteNonQuery();
                                        scope.Complete();
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
        #endregion
    }
}