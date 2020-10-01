using System;
using System.Data;
using System.Data.SqlClient;
using log4net;
using MinvoiceLoadDataMisa.Config;
using Newtonsoft.Json.Linq;


namespace MinvoiceLoadDataMisa.Services
{
    public class JsonConvert
    {
        private static readonly ILog Log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static JObject ConvertMasterObject(DataRow row)
        {
            Log.Debug($"InvDate: {row["InvDate"].ToString()}");
            try
            {
                
                var jObject = new JObject();

                jObject.Add("inv_InvoiceAuth_id", row["RefID"].ToString());
                jObject.Add("inv_invoiceType", "01GTKT");
                if(Properties.Settings.Default.Editmode == "1")
                {
                    jObject.Add("inv_InvoiceCode_id", Properties.Settings.Default.InvoiceCodeId);
                }else
                {
                    jObject.Add("inv_InvoiceCode_id", Properties.Settings.Default.InvoiceCodeId2);
                }
                if (Properties.Settings.Default.Editmode == "1")
                {
                    jObject.Add("inv_invoiceSeries", Properties.Settings.Default.KyHieu);
                }else
                {
                    jObject.Add("inv_invoiceSeries", Properties.Settings.Default._kyHieu);
                }
                    
                jObject.Add("inv_invoiceNumber", row["InvNo"].ToString());
                jObject.Add("inv_invoiceName", "Hóa đơn giá trị gia tăng");
                jObject.Add("inv_invoiceIssuedDate", DateTime.Parse(row["InvDate"].ToString()).ToString("yyyy-MM-dd"));
                jObject.Add(
                    "inv_currencyCode",
                    !string.IsNullOrEmpty(row["CurrencyID"].ToString()) ? row["CurrencyID"].ToString() : "VND"
                );
                jObject.Add(
                    "inv_exchangeRate",
                    !string.IsNullOrEmpty(row["ExchangeRate"].ToString())
                        ? double.Parse(row["ExchangeRate"].ToString())
                        : 1
                );
                jObject.Add("inv_adjustmentType", 1);
                jObject.Add("inv_buyerDisplayName", null);
                //jObject.Add(
                //    "inv_buyerDisplayName", BaseConfig.Version.Equals("2017")
                //        ? (!string.IsNullOrEmpty(row["Buyer"].ToString())
                //            ? row["Buyer"].ToString()
                //            : row["AccountObjectName"].ToString())
                //        : (!string.IsNullOrEmpty(row["AccountingObjectName"].ToString())
                //            ? row["AccountingObjectName"].ToString()
                //            : null)
                //);
                jObject.Add("ma_dt", "");
                jObject.Add("inv_buyerLegalName", BaseConfig.Version.Equals("2017")
                    ? (!string.IsNullOrEmpty(row["AccountObjectName"].ToString())
                        ? row["AccountObjectName"].ToString()
                        : null)
                    : (!string.IsNullOrEmpty(row["AccountingObjectName"].ToString())
                        ? row["AccountingObjectName"].ToString()
                        : null));
                jObject.Add(
                    "inv_buyerTaxCode", BaseConfig.Version.Equals("2017")
                        ? (!string.IsNullOrEmpty(row["AccountObjectTaxCode"].ToString())
                            ? row["AccountObjectTaxCode"].ToString()
                            : null)
                        : (!string.IsNullOrEmpty(row["CompanyTaxCode"].ToString())
                            ? row["CompanyTaxCode"].ToString()
                            : null)
                );
                jObject.Add(
                    "inv_buyerAddressLine", BaseConfig.Version.Equals("2017")
                        ? (!string.IsNullOrEmpty(row["AccountObjectAddress"].ToString())
                            ? row["AccountObjectAddress"].ToString()
                            : "Hà Nội")
                        : (!string.IsNullOrEmpty(row["AccountingObjectAddress"].ToString())
                            ? row["AccountingObjectAddress"].ToString()
                            : "Hà Nội")
                );
                jObject.Add("inv_buyerEmail", row.Table.Columns.Contains("EmailAddress") ? (!string.IsNullOrEmpty(row["EmailAddress"].ToString())
                    ? row["EmailAddress"].ToString()
                    : "") : "");
                jObject.Add(
                    "inv_buyerBankAccount", BaseConfig.Version.Equals("2017")
                        ? (!string.IsNullOrEmpty(row["AccountObjectBankAccount"].ToString())
                            ? row["AccountObjectBankAccount"].ToString()
                            : null)
                        : (!string.IsNullOrEmpty(row["BankAccount"].ToString())
                            ? row["BankAccount"].ToString()
                            : null)
                );
                jObject.Add("inv_buyerBankName", "");
                jObject.Add(
                    "inv_paymentMethodName", BaseConfig.Version.Equals("2017")
                        ? (!string.IsNullOrEmpty(row["PaymentMethod"].ToString())
                            ? row["PaymentMethod"].ToString()
                            : "Tiền mặt/Chuyển khoản")
                        : "Tiền mặt/Chuyển khoản"
                );
                jObject.Add("inv_sellerBankAccount", "");
                jObject.Add("inv_sellerBankName", "");
                jObject.Add("trang_thai", "Chờ ký");
                jObject.Add("nguoi_ky", "");
                jObject.Add("sobaomat", "");
                jObject.Add("trang_thai_hd", 1);
                jObject.Add("in_chuyen_doi", false);
                jObject.Add("ngay_ky", null);
                jObject.Add("nguoi_in_cdoi", "");
                jObject.Add("ngay_in_cdoi", null);
                jObject.Add("mau_hd", BaseConfig.MauSo);
                jObject.Add("ma_ct", "HDDT");
                Log.Debug($"Master: {jObject}");

                return jObject;

            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw new Exception(e.Message);
            }
        }

        public static JObject ConvertDetailJObjectSaVoucherDetail(DataRow row)
        {
            try
            {
                var vatAmount = !string.IsNullOrEmpty(row["VATAmountOC"].ToString())
                    ? double.Parse(row["VATAmountOC"].ToString())
                    : 0;

                var discountAmount = !string.IsNullOrEmpty(row["DiscountAmount"].ToString())
                    ? double.Parse(row["DiscountAmount"].ToString())
                    : 0;

                var totalAmountWithoutVat = !string.IsNullOrEmpty(row["AmountOC"].ToString())
                    ? double.Parse(row["AmountOC"].ToString())
                    : 0;

                var totalAmount = totalAmountWithoutVat + vatAmount - discountAmount;

                var jObject = new JObject();
                jObject.Add("inv_InvoiceAuthDetail_id", row["RefDetailID"].ToString());
                jObject.Add(
                    "inv_InvoiceAuth_id",
                    !string.IsNullOrEmpty(row["SAInvoiceRefID"].ToString())
                        ? row["SAInvoiceRefID"].ToString()
                        : null
                );
                jObject.Add("stt_rec0", !string.IsNullOrEmpty(row["SortOrder"].ToString()) ? row["SortOrder"].ToString() : null);
                jObject.Add("inv_itemCode", !string.IsNullOrEmpty(row["InventoryItemCode"].ToString()) ? row["InventoryItemCode"].ToString() : null);
                jObject.Add(
                    "inv_itemName",
                    !string.IsNullOrEmpty(row["Description"].ToString()) ? row["Description"].ToString() : null
                );
                jObject.Add("inv_unitCode", !string.IsNullOrEmpty(row["UnitName"].ToString()) ? row["UnitName"].ToString() : null);
                jObject.Add("inv_unitName", !string.IsNullOrEmpty(row["UnitName"].ToString()) ? row["UnitName"].ToString() : null);
                Log.Debug(row["UnitPrice"].ToString());
                Log.Debug(row["Quantity"].ToString());
                Log.Debug(row["AmountOC"].ToString());

                jObject.Add(
                    "inv_unitPrice",
                    !string.IsNullOrEmpty(row["UnitPrice"].ToString()) ? CommonService.ConvertNumber2(row["UnitPrice"].ToString()) : null
                );
                jObject.Add(
                    "inv_quantity",
                    !string.IsNullOrEmpty(row["Quantity"].ToString()) ? CommonService.ConvertNumber2(row["Quantity"].ToString()) : null
                );
                jObject.Add(
                    "inv_TotalAmountWithoutVat",
                    !string.IsNullOrEmpty(row["AmountOC"].ToString()) ? CommonService.ConvertNumber2(row["AmountOC"].ToString()) : null
                );
                jObject.Add(
                    "inv_vatPercentage",
                    !string.IsNullOrEmpty(row["VATRate"].ToString()) ? CommonService.ConvertNumber2(row["VATRate"].ToString()) : null
                );
                jObject.Add(
                    "inv_vatAmount",
                    !string.IsNullOrEmpty(row["VATAmountOC"].ToString()) ? CommonService.ConvertNumber2(row["VATAmountOC"].ToString()) : null
                );
                jObject.Add("inv_TotalAmount", totalAmount);
                jObject.Add("inv_promotion", false);
                jObject.Add(
                    "inv_discountPercentage",
                    !string.IsNullOrEmpty(row["DiscountRate"].ToString()) ? CommonService.ConvertNumber2(row["DiscountRate"].ToString()) : null
                );
                jObject.Add(
                    "inv_discountAmount",
                    !string.IsNullOrEmpty(row["DiscountAmount"].ToString())
                        ? CommonService.ConvertNumber2(row["DiscountAmount"].ToString())
                        : null
                );
                jObject.Add(
                    "ma_thue",
                    !string.IsNullOrEmpty(row["VATRate"].ToString())
                        ? CommonService.ConvertNumber2(row["VATRate"].ToString())
                        : null
                );

                Log.Debug(jObject.ToString());

                return jObject;
            }
            catch (Exception ex)
            {
                Log.Error($"Tên hàng: {row["Description"].ToString()} --- Thứ tự: {row["SortOrder"].ToString()}");
                Log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public static JObject ConvertDetailJObjectSaInvoiceDetail(DataRow row)
        {
            try
            {
                var vatAmount = !string.IsNullOrEmpty(row["VATAmountOC"].ToString())
                    ? double.Parse(row["VATAmountOC"].ToString())
                    : 0;

                var discountAmount = !string.IsNullOrEmpty(row["DiscountAmount"].ToString())
                    ? double.Parse(row["DiscountAmount"].ToString())
                    : 0;

                var totalAmountWithoutVat = !string.IsNullOrEmpty(row["AmountOC"].ToString())
                    ? double.Parse(row["AmountOC"].ToString())
                    : 0;

                var totalAmount = totalAmountWithoutVat + vatAmount - discountAmount;
                var jObject = new JObject();
                jObject.Add("inv_InvoiceAuthDetail_id", row["RefDetailID"].ToString());
                jObject.Add(
                    "inv_InvoiceAuth_id",
                    !string.IsNullOrEmpty(row["RefID"].ToString()) ? row["RefID"].ToString() : null
                );
                jObject.Add("stt_rec0", !string.IsNullOrEmpty(row["SortOrder"].ToString()) ? row["SortOrder"].ToString() : null);
                jObject.Add("inv_itemCode", !string.IsNullOrEmpty(row["InventoryItemCode"].ToString()) ? row["InventoryItemCode"].ToString() : null);
                jObject.Add(
                    "inv_itemName",
                    !string.IsNullOrEmpty(row["Description"].ToString()) ? row["Description"].ToString() : null
                );
                jObject.Add("inv_unitCode", BaseConfig.Version == "2017" ? (!string.IsNullOrEmpty(row["UnitName"].ToString()) ? row["UnitName"].ToString() : null) : (!string.IsNullOrEmpty(row["UnitConvert"].ToString()) ? row["UnitConvert"].ToString() : null));
                jObject.Add("inv_unitName", BaseConfig.Version == "2017" ? (!string.IsNullOrEmpty(row["UnitName"].ToString()) ? row["UnitName"].ToString() : null) : (!string.IsNullOrEmpty(row["UnitConvert"].ToString()) ? row["UnitConvert"].ToString() : null));
                jObject.Add(
                    "inv_unitPrice",
                    !string.IsNullOrEmpty(row["UnitPrice"].ToString()) ? CommonService.ConvertNumber2(row["UnitPrice"].ToString()) : null
                );
                jObject.Add(
                    "inv_quantity",
                    !string.IsNullOrEmpty(row["Quantity"].ToString()) ? CommonService.ConvertNumber2(row["Quantity"].ToString()) : null
                );
                jObject.Add(
                    "inv_TotalAmountWithoutVat",
                    !string.IsNullOrEmpty(row["AmountOC"].ToString()) ? CommonService.ConvertNumber2(row["AmountOC"].ToString()) : null
                );
                jObject.Add(
                    "inv_vatPercentage",
                    !string.IsNullOrEmpty(row["VATRate"].ToString()) ? CommonService.ConvertNumber2(row["VATRate"].ToString()) : null
                );
                jObject.Add(
                    "inv_vatAmount",
                    !string.IsNullOrEmpty(row["VATAmountOC"].ToString()) ? CommonService.ConvertNumber2(row["VATAmountOC"].ToString()) : null
                );
                jObject.Add("inv_TotalAmount", totalAmount);
                jObject.Add("inv_promotion", false);
                jObject.Add(
                    "inv_discountPercentage",
                    !string.IsNullOrEmpty(row["DiscountRate"].ToString()) ? CommonService.ConvertNumber2(row["DiscountRate"].ToString()) : null
                );
                jObject.Add(
                    "inv_discountAmount",
                    !string.IsNullOrEmpty(row["DiscountAmount"].ToString())
                        ? CommonService.ConvertNumber2(row["DiscountAmount"].ToString())
                        : null
                );
                jObject.Add(
                    "ma_thue",
                    !string.IsNullOrEmpty(row["VATRate"].ToString())
                        ? CommonService.ConvertNumber2(row["VATRate"].ToString())
                        : null
                );

                Log.Debug($"Detail: {jObject}");

                return jObject;
            }
            catch (Exception ex)
            {
                Log.Error($"Tên hàng: {row["Description"].ToString()} --- Thứ tự: {row["SortOrder"].ToString()}");
                Log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public static JObject ConvertData(SqlConnection sqlConnection, DataRow row)
        {
            try
            {
                var inv_TotalAmount = 0.0;
                var inv_TotalAmountWithoutVat = 0.0;
                var inv_vatAmount = 0.0;
                var inv_discountAmount = 0.0;

                var masterJObject = ConvertMasterObject(row);

                var refId = row["RefID"].ToString();
                var jArrayDetail = GetJArrayDetail(sqlConnection, refId, out inv_TotalAmount, out inv_TotalAmountWithoutVat, out inv_vatAmount, out inv_discountAmount);
                masterJObject.Add("inv_TotalAmount", inv_TotalAmount);
                masterJObject.Add("inv_TotalAmountWithoutVat", inv_TotalAmountWithoutVat);
                masterJObject.Add("inv_vatAmount", inv_vatAmount);
                masterJObject.Add("inv_discountAmount", inv_discountAmount);

                var data = new JObject
                {
                    {"tab_id", "TAB00188 "},
                    {"tab_table", "inv_InvoiceAuthDetail"},
                    {"data", jArrayDetail}
                };
                var details = new JArray(data);
                masterJObject.Add("details", details);
                Log.Debug($"Invocie: {masterJObject}");
                return masterJObject;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw new Exception(e.Message);
            }
        }

        private static JArray GetJArrayDetail(SqlConnection sqlConnection, string refId, out double inv_TotalAmount, out double inv_TotalAmountWithoutVat, out double inv_vatAmount, out double inv_discountAmount)
        {
            inv_TotalAmount = 0.0;
            inv_TotalAmountWithoutVat = 0.0;
            inv_vatAmount = 0.0;
            inv_discountAmount = 0.0;

            var jArrayDetail = new JArray();

            if (BaseConfig.Version.Equals("2017"))
            {
                
                string whereVoucherDetail = $" WHERE a.SAInvoiceRefID = '{refId}' ORDER BY a.SortOrder";
                string sqlSelectVoucherDetail = CommonConstants.SqlSelectSaVoucherDetail2017;
                sqlSelectVoucherDetail += whereVoucherDetail;

                var dataTableVoucherDetail = DataContext.GetDataTableTest(sqlConnection, sqlSelectVoucherDetail);
                if (dataTableVoucherDetail.Rows.Count > 0)
                {
                    foreach (DataRow dataRowVoucherDetail in dataTableVoucherDetail.Rows)
                    {
                        var detailJObject = ConvertDetailJObjectSaVoucherDetail(dataRowVoucherDetail);
                        inv_TotalAmount += (double)detailJObject["inv_TotalAmount"];
                        inv_TotalAmountWithoutVat += (double)detailJObject["inv_TotalAmountWithoutVat"];
                        inv_vatAmount += (double)detailJObject["inv_vatAmount"];
                        inv_discountAmount += (double)detailJObject["inv_discountAmount"];
                        jArrayDetail.Add(detailJObject);


                        Log.Debug("OK");
                    }
                }

            }

            //var whereInvoiceDetail = $"WHERE RefID = '{refId}' ORDER BY SortOrder";
            //var dataTableInvoiceDetail =
            //    DataContext.GetDataTableTest(sqlConnection, BaseConfig.TableInvoiceDetail, whereInvoiceDetail);

            string sqlSelectInvoiceDetail = BaseConfig.Version == "2017"
                ? CommonConstants.SqlSelectSaInvoiceDetail2017
                : CommonConstants.SqlSelectSaInvoiceDetail2012;

            var whereInvoiceDetail = $" WHERE a.RefID = '{refId}' ORDER BY a.SortOrder";
            sqlSelectInvoiceDetail += whereInvoiceDetail;

            var dataTableInvoiceDetail = DataContext.GetDataTableTest(sqlConnection, sqlSelectInvoiceDetail);
            if (dataTableInvoiceDetail.Rows.Count > 0)
            {
                foreach (DataRow dataRowInvoiceDetail in dataTableInvoiceDetail.Rows)
                {
                    var detailJObject = ConvertDetailJObjectSaInvoiceDetail(dataRowInvoiceDetail);
                    inv_TotalAmount += (double)detailJObject["inv_TotalAmount"];
                    inv_TotalAmountWithoutVat += (double)detailJObject["inv_TotalAmountWithoutVat"];
                    inv_vatAmount += (double)detailJObject["inv_vatAmount"];
                    inv_discountAmount += (double)detailJObject["inv_discountAmount"];
                    jArrayDetail.Add(detailJObject);
                }
            }

            return jArrayDetail;
        }
    }
}