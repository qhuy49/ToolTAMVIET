using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using MinvoiceLoadDataMisa.Config;
using MinvoiceLoadDataMisa.Data;
using Newtonsoft.Json.Linq;

namespace MinvoiceLoadDataMisa.Services
{
    public class CommonService
    {
        public static void UpdateSettingAppConfig(string key, string value)
        {
            switch (key)
            {
                case "TableInvocie":
                    Properties.Settings.Default.TableInvocie = value; break;
                case "TableInvoiceDetail":
                    Properties.Settings.Default.TableInvoiceDetail = value; break;
                case "DauVao":
                    Properties.Settings.Default.DauVao = value; break;
                case "ConnectionString":
                    Properties.Settings.Default.ConnectionString = value; break;
                case "UsernameLoginWeb":
                    Properties.Settings.Default.UsernameLoginWeb = value; break;
                case "PasswordLoginWeb":
                    Properties.Settings.Default.PasswordLoginWeb = value; break;
                case "UrlLogin":
                    Properties.Settings.Default.UrlLogin = value; break;
                case "UrlSave":
                    Properties.Settings.Default.UrlSave = value; break;
                case "UrlGetInvoiceByInvoiceAuthId":
                    Properties.Settings.Default.UrlGetInvoiceByInvoiceAuthId = value; break;
                case "TimeLoadData":
                    Properties.Settings.Default.TimeLoadData = value; break;
                case "MauSo":
                    Properties.Settings.Default.MauSo = value; break;
                case "KyHieu":
                    Properties.Settings.Default.KyHieu = value; break;
                case "UrlRef":
                    Properties.Settings.Default.UrlRef = value; break;
                case "InvoiceCodeId":
                    Properties.Settings.Default.InvoiceCodeId = value; break;
                case "TableDM_DTCN":
                    Properties.Settings.Default.TableDM_DTCN = value; break;
                case "Command":
                    Properties.Settings.Default.Command = value; break;
                case "UrlCommand":
                    Properties.Settings.Default.UrlCommand = value; break;
                case "Version":
                    Properties.Settings.Default.Version = value; break;

                case "TablePS_BangKeGTGT":
                    Properties.Settings.Default.TablePS_BangKeGTGT = value; break;
                case "CmCheck":
                    Properties.Settings.Default.CmCheck = value; break;
                case "RefInventoryItem":
                    Properties.Settings.Default.RefInventoryItem = value; break;
                case "RefUnit":
                    Properties.Settings.Default.RefUnit = value; break;
                case "InvoiceCodeId2":
                    Properties.Settings.Default.InvoiceCodeId2 = value; break;
                case "_kyHieu":
                    Properties.Settings.Default._kyHieu = value; break;

                case "Editmode":
                    Properties.Settings.Default.Editmode = value; break;

                case "connectLog":
                    Properties.Settings.Default.connectLog = value; break;

            }

            Properties.Settings.Default.Save();
        }

        public static List<MauHoaDon> GetInfoInvoice()
        {
            List<MauHoaDon> mauHoaDons = new List<MauHoaDon>();
            var webClient = MinvoiceService.SetupWebClient();
            var url = BaseConfig.UrlRef;
            try
            {
                var result = webClient.DownloadString(url);
                if (!string.IsNullOrEmpty(result))
                {
                    JArray jArray = JArray.Parse(result);
                    if (jArray.Count > 0)
                    {
                        foreach (var jToken in jArray)
                        {
                            MauHoaDon mauHoaDon = new MauHoaDon
                            {
                                KyHieu = jToken["ky_hieu"].ToString(),
                                MauSo = jToken["mau_so"].ToString(),
                                Id = jToken["id"].ToString()
                            };
                            mauHoaDons.Add(mauHoaDon);
                        }
                    }
                }

                return mauHoaDons;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<InvoiceObject> GetInvoiceObjects(string tuNgay, string denNgay, string id)
        {
            var webClient = MinvoiceService.SetupWebClient();
            var url = BaseConfig.UrlCommand;
            var command = BaseConfig.Command;
           JObject json = new JObject
            {
               {"command", command },
                 {  "parameter", new JObject
                      {
                           {"tu_ngay", tuNgay },
                           {"den_ngay", denNgay },
                           {"id", id }

                      }
                 }
            };
            

            List<InvoiceObject> invoiceObjects = new List<InvoiceObject>();
            try
            {
                var rs = webClient.UploadString(url, json.ToString());
                var result = JArray.Parse(rs);
                if (result.Count > 0)
                {
                   
                    foreach (var jToken in result)
                    {
                        InvoiceObject invoiceObject = new InvoiceObject
                        {
                            InvoiceNumber = jToken["inv_invoiceNumber"].ToString(),
                            KyHieu = jToken["inv_invoiceSeries"].ToString(),
                            MauSo = jToken["mau_hd"].ToString(),
                            Selected = false,
                            InvInvoiceAuthId = jToken["inv_InvoiceAuth_id"].ToString(),
                            key_api = jToken["key_api"].ToString()
                        };
                        invoiceObject.Value =
                            $"{invoiceObject.MauSo} - {invoiceObject.KyHieu} - {invoiceObject.InvoiceNumber}  ";
                        invoiceObjects.Add(invoiceObject);
                    }
                }

                return invoiceObjects;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public static bool CheckLogin(string pass)
        {
            return pass.Equals("!@#minvoice@123!@#");
        }

        private static bool CheckColumnIsInvoice()
        {

            string conn = BaseConfig.ConnectionString;
            DataTable table = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(conn))
            {
                try
                {
                    sqlConnection.Open();
                    string commandText = $"SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME ='{BaseConfig.TableInvocie}' AND COLUMN_NAME = 'IsInvoice'";

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

        public static void AddColumnIsInvoice()
        {
            if (CheckColumnIsInvoice() == false)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string conn = BaseConfig.ConnectionString;
                    try
                    {
                        SqlConnection connection = new SqlConnection(conn);
                        connection.Open();

                        string commandText = $"ALTER TABLE {BaseConfig.TableInvocie} ADD IsInvoice INT";
                        SqlCommand command = new SqlCommand(commandText, connection);
                        command.ExecuteNonQuery();
                        commandText = $"Update {BaseConfig.TableInvocie} SET IsInvoice=1";
                        command.CommandText = commandText;
                        command.ExecuteNonQuery();
                        scope.Complete();
                    }
                    catch (Exception e)
                    {
                        scope.Dispose();
                        throw new Exception(e.Message);
                    }

                }
            }

        }

        public static void UpdateIsInvoice()
        {
            if (CheckColumnIsInvoice())
            {
                string conn = BaseConfig.ConnectionString;
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();
                string commandText = $"Update {BaseConfig.TableInvocie} SET IsInvoice = 1";
                SqlCommand command = new SqlCommand(commandText, connection);
                command.ExecuteNonQuery();
            }
        }
        public static double ConvertNumber(string number)
        {
            string[] a = null;
            if (number.Contains("."))
            {
                a = number.Split('.');
            }

            if (number.Contains(","))
            {
                a = number.Split(',');
            }

            if (a != null && a.Any())
            {
                var number1 = a[0];
                var number2 = a[1];
                if (number2.Length > 2)
                {
                    number2 = number2.Substring(0, 2);
                }

                return double.Parse($"{number1}.{number2}");
            }

            return double.Parse(number);
        }

        public static string ConvertNumber2(string value)
        {
            string[] a = new string[] { };
            if (value.Contains("."))
            {
                a = value.Split('.');
            }

            if (value.Contains(","))
            {
                a = value.Split(',');
            }

            if (a.Length > 1)
            {
                //var phanThapPhan = GetMinOfNumber(int.Parse(a[1]));
                var phanThapPhan = GetMinOfNumber(a[1]);
                var phanNguyen = a[0];

                var result = int.Parse(phanThapPhan) > 0 ? $"{phanNguyen}.{phanThapPhan}" : phanNguyen;
                return result;
            }

            return value;
        }

        private static int GetMinOfNumber(int number)
        {
            if (number <= 0) return 0;
            while (number % 10 == 0)
            {
                number = number / 10;

            }

            return number;
        }

        private static string GetMinOfNumber(string number)
        {
            if (int.Parse(number) <= 0) return "0";
            while (number.EndsWith("0"))
            {
                number = number.Substring(0, number.Length - 1);
            }

            return number;
        }
    }
}
