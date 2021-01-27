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
        public static Guid inv_InvoiceAuth_id;
        public static JObject ConvertMasterObject(DataRow row, string inv_invoiceauth_id, string invNo)
        {
            Log.Debug($"NgayPH: {row["NgayPH"].ToString()}");
            try
            {
                
                var jObject = new JObject();
                if (Properties.Settings.Default.Editmode == "1")
                {
                    inv_InvoiceAuth_id = Guid.NewGuid();
                }
                else
                {
                    inv_InvoiceAuth_id = Guid.Parse(inv_invoiceauth_id);
                }

                
                jObject.Add("inv_InvoiceAuth_id", inv_InvoiceAuth_id);

                jObject.Add("key_api", row["SoPhieu"].ToString());
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
                if (Properties.Settings.Default.Editmode == "1")
                {
                    jObject.Add("inv_invoiceNumber", row["VAT_So"].ToString());
                }
                else
                {
                    jObject.Add("inv_invoiceNumber", invNo);
                }
               
                jObject.Add("inv_invoiceName", "Hóa đơn giá trị gia tăng");
                jObject.Add("inv_invoiceIssuedDate", DateTime.Parse(row["NgayPH"].ToString()).ToString("yyyy-MM-dd"));
                jObject.Add(
                    "inv_currencyCode",
                    !string.IsNullOrEmpty(row["LoaiTien"].ToString()) ? row["LoaiTien"].ToString() : "VND"
                );
                jObject.Add(
                    "inv_exchangeRate",
                    !string.IsNullOrEmpty(row["TyGia"].ToString())
                        ? double.Parse(row["TyGia"].ToString())
                        : 1
                );
                jObject.Add("inv_adjustmentType", 1);
                jObject.Add("inv_buyerDisplayName", (!string.IsNullOrEmpty(row["NguoiGiaoDich"].ToString()) ? row["NguoiGiaoDich"].ToString() : ""));
                //jObject.Add(
                //    "inv_buyerDisplayName", BaseConfig.Version.Equals("2017")
                //        ? (!string.IsNullOrEmpty(row["Buyer"].ToString())
                //            ? row["Buyer"].ToString()
                //            : row["AccountObjectName"].ToString())
                //        : (!string.IsNullOrEmpty(row["AccountingObjectName"].ToString())
                //            ? row["AccountingObjectName"].ToString()
                //            : null)
                //);
                jObject.Add("ma_dt", row["DoiTuongCongNoID"].ToString());
                jObject.Add("inv_buyerLegalName",  (!string.IsNullOrEmpty(row["TenDoiTuongCongNo"].ToString()) ? row["TenDoiTuongCongNo"].ToString() : ""));

                jObject.Add( "inv_buyerTaxCode", (!string.IsNullOrEmpty(row["MasoThue"].ToString()) ? row["MasoThue"].ToString()  : ""));

                jObject.Add( "inv_buyerAddressLine", (!string.IsNullOrEmpty(row["DiaChi"].ToString()) ? row["DiaChi"].ToString() : "")  );

                jObject.Add("inv_buyerEmail", row.Table.Columns.Contains("Email") ? (!string.IsNullOrEmpty(row["Email"].ToString()) ? row["Email"].ToString()  : "") : "");

                jObject.Add( "inv_buyerBankAccount", (!string.IsNullOrEmpty(row["TenNganHang"].ToString()) ? row["TenNganHang"].ToString() : "") + (!string.IsNullOrEmpty(row["TaiKhoan"].ToString()) ? row["TaiKhoan"].ToString() : ""));

                jObject.Add("inv_buyerBankName", (!string.IsNullOrEmpty(row["TenNganHang"].ToString()) ? row["TenNganHang"].ToString() : ""));

                jObject.Add( "inv_paymentMethodName",   (!string.IsNullOrEmpty(row["HinhThucThanhToan"].ToString()) ? row["HinhThucThanhToan"].ToString()   : "Tiền mặt/Chuyển khoản") );

                jObject.Add("inv_invoiceNote", (!string.IsNullOrEmpty(row["NoiDung"].ToString()) ? row["NoiDung"].ToString() : ""));

                jObject.Add("ma_dh", (!string.IsNullOrEmpty(row["ChungTuID"].ToString()) ? row["ChungTuID"].ToString() : ""));
                jObject.Add("inv_sellerBankAccount", "");

                jObject.Add("so_benh_an", (!string.IsNullOrEmpty(row["NoiDung"].ToString()) ? row["NoiDung"].ToString() : ""));

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

        //public static JObject ConvertDetailJObjectSaVoucherDetail(DataRow row)
        //{
        //    try
        //    {
        //        var vatAmount = !string.IsNullOrEmpty(row["VATAmountOC"].ToString())
        //            ? double.Parse(row["VATAmountOC"].ToString())
        //            : 0;

        //        var discountAmount = !string.IsNullOrEmpty(row["DiscountAmount"].ToString())
        //            ? double.Parse(row["DiscountAmount"].ToString())
        //            : 0;

        //        var totalAmountWithoutVat = !string.IsNullOrEmpty(row["AmountOC"].ToString())
        //            ? double.Parse(row["AmountOC"].ToString())
        //            : 0;

        //        var totalAmount = totalAmountWithoutVat + vatAmount - discountAmount;

        //        var jObject = new JObject();
        //        jObject.Add("inv_InvoiceAuthDetail_id", row["RefDetailID"].ToString());
        //        jObject.Add(
        //            "inv_InvoiceAuth_id",
        //            !string.IsNullOrEmpty(row["SAInvoiceRefID"].ToString())
        //                ? row["SAInvoiceRefID"].ToString()
        //                : null
        //        );
        //        jObject.Add("stt_rec0", !string.IsNullOrEmpty(row["SortOrder"].ToString()) ? row["SortOrder"].ToString() : null);
        //        jObject.Add("inv_itemCode", !string.IsNullOrEmpty(row["InventoryItemCode"].ToString()) ? row["InventoryItemCode"].ToString() : null);
        //        jObject.Add(
        //            "inv_itemName",
        //            !string.IsNullOrEmpty(row["Description"].ToString()) ? row["Description"].ToString() : null
        //        );
        //        jObject.Add("inv_unitCode", !string.IsNullOrEmpty(row["UnitName"].ToString()) ? row["UnitName"].ToString() : null);
        //        jObject.Add("inv_unitName", !string.IsNullOrEmpty(row["UnitName"].ToString()) ? row["UnitName"].ToString() : null);
        //        Log.Debug(row["UnitPrice"].ToString());
        //        Log.Debug(row["Quantity"].ToString());
        //        Log.Debug(row["AmountOC"].ToString());

        //        jObject.Add(
        //            "inv_unitPrice",
        //            !string.IsNullOrEmpty(row["UnitPrice"].ToString()) ? CommonService.ConvertNumber2(row["UnitPrice"].ToString()) : null
        //        );
        //        jObject.Add(
        //            "inv_quantity",
        //            !string.IsNullOrEmpty(row["Quantity"].ToString()) ? CommonService.ConvertNumber2(row["Quantity"].ToString()) : null
        //        );
        //        jObject.Add(
        //            "inv_TotalAmountWithoutVat",
        //            !string.IsNullOrEmpty(row["AmountOC"].ToString()) ? CommonService.ConvertNumber2(row["AmountOC"].ToString()) : null
        //        );
        //        jObject.Add(
        //            "inv_vatPercentage",
        //            !string.IsNullOrEmpty(row["VATRate"].ToString()) ? CommonService.ConvertNumber2(row["VATRate"].ToString()) : null
        //        );
        //        jObject.Add(
        //            "inv_vatAmount",
        //            !string.IsNullOrEmpty(row["VATAmountOC"].ToString()) ? CommonService.ConvertNumber2(row["VATAmountOC"].ToString()) : null
        //        );
        //        jObject.Add("inv_TotalAmount", totalAmount);
        //        jObject.Add("inv_promotion", false);
        //        jObject.Add(
        //            "inv_discountPercentage",
        //            !string.IsNullOrEmpty(row["DiscountRate"].ToString()) ? CommonService.ConvertNumber2(row["DiscountRate"].ToString()) : null
        //        );
        //        jObject.Add(
        //            "inv_discountAmount",
        //            !string.IsNullOrEmpty(row["DiscountAmount"].ToString())
        //                ? CommonService.ConvertNumber2(row["DiscountAmount"].ToString())
        //                : null
        //        );
        //        jObject.Add(
        //            "ma_thue",
        //            !string.IsNullOrEmpty(row["VATRate"].ToString())
        //                ? CommonService.ConvertNumber2(row["VATRate"].ToString())
        //                : null
        //        );

        //        Log.Debug(jObject.ToString());

        //        return jObject;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"Tên hàng: {row["Description"].ToString()} --- Thứ tự: {row["SortOrder"].ToString()}");
        //        Log.Error(ex.Message);
        //        throw new Exception(ex.Message);
        //    }
        //}
        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static JObject ConvertDetailJObjectSaInvoiceDetail(DataRow row, double thue, double inv_vatAmount1, bool suathue)
        {
            try
            {
                double vatAmount = 0;
                if (suathue == true)
                {
                    vatAmount = inv_vatAmount1;
                }
                else
                {
                    vatAmount = !string.IsNullOrEmpty(row["PhatSinh"].ToString()) ? double.Parse(row["PhatSinh"].ToString()) : 0;
                    vatAmount = vatAmount * thue / 100;
                }
               

                var discountAmount = !string.IsNullOrEmpty(row["TienChietKhauTruocThue"].ToString())
                    ? double.Parse(row["TienChietKhauTruocThue"].ToString())
                    : 0;

                var totalAmountWithoutVat = !string.IsNullOrEmpty(row["PhatSinh"].ToString())
                    ? double.Parse(row["PhatSinh"].ToString())
                    : 0;

                var totalAmount = totalAmountWithoutVat + vatAmount - discountAmount;
                var jObject = new JObject();
                //jObject.Add("inv_InvoiceAuthDetail_id", row["RefDetailID"].ToString());
                jObject.Add(
                    "inv_InvoiceAuth_id", inv_InvoiceAuth_id);
                //jObject.Add("stt_rec0", !string.IsNullOrEmpty(row["SortOrder"].ToString()) ? row["SortOrder"].ToString() : null);
                jObject.Add("inv_itemCode", !string.IsNullOrEmpty(row["VatTuID"].ToString()) ? row["VatTuID"].ToString() : null);
                jObject.Add(
                    "inv_itemName",
                    !string.IsNullOrEmpty(row["TenVatTu_ChiTiet"].ToString()) ? row["TenVatTu_ChiTiet"].ToString() : null );

                jObject.Add("inv_unitCode", (!string.IsNullOrEmpty(row["DVT"].ToString()) ? row["DVT"].ToString() : null));
                jObject.Add("inv_unitName", (!string.IsNullOrEmpty(row["DVT"].ToString()) ? row["DVT"].ToString() : null));
                jObject.Add(
                    "inv_unitPrice",
                    !string.IsNullOrEmpty(row["DonGia"].ToString()) ? CommonService.ConvertNumber2(row["DonGia"].ToString()) : null );

                jObject.Add(
                    "inv_quantity",
                    !string.IsNullOrEmpty(row["SoLuong"].ToString()) ? CommonService.ConvertNumber2(row["SoLuong"].ToString()) : null );

                jObject.Add(
                    "inv_TotalAmountWithoutVat",
                    !string.IsNullOrEmpty(row["PhatSinh"].ToString()) ? CommonService.ConvertNumber2(row["PhatSinh"].ToString()) : null
                );
                jObject.Add(
                    "inv_vatPercentage",
                    !string.IsNullOrEmpty(thue.ToString()) ? CommonService.ConvertNumber2(thue.ToString()) : null
                );
                jObject.Add(
                    "inv_vatAmount",
                    !string.IsNullOrEmpty(vatAmount.ToString()) ? CommonService.ConvertNumber2(vatAmount.ToString()) : null);

                jObject.Add("inv_TotalAmount", totalAmount);

                jObject.Add("inv_promotion", false);

                jObject.Add(
                    "inv_discountPercentage",
                    !string.IsNullOrEmpty(row["ChietKhau"].ToString()) ? CommonService.ConvertNumber2(row["ChietKhau"].ToString()) : null
                );
                jObject.Add(
                    "inv_discountAmount",
                    !string.IsNullOrEmpty(row["TienChietKhauTruocThue"].ToString())
                        ? CommonService.ConvertNumber2(row["TienChietKhauTruocThue"].ToString())   : null  );

                jObject.Add(
                    "ma_thue",
                    !string.IsNullOrEmpty(thue.ToString())
                        ? CommonService.ConvertNumber2(thue.ToString()) : null);

                Log.Debug($"Detail: {jObject}");

                return jObject;
            }
            catch (Exception ex)
            {
                Log.Error($"Tên hàng: {row["TenVatTu_ChiTiet"].ToString()} --- Thứ tự: {row["STT"].ToString()}");
                Log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public static JObject ConvertData(SqlConnection sqlConnection, DataRow row, string inv_invoiceauth_id, string invNo)
        {
            try
            {
                var inv_TotalAmount = 0.0;
                var inv_TotalAmountWithoutVat = 0.0;
                var inv_vatAmount = 0.0;
                var inv_discountAmount = 0.0;
                double thue = !string.IsNullOrEmpty(row["VAT"].ToString()) ? Convert.ToDouble(row["VAT"].ToString()) : 0.0;
                var masterJObject = ConvertMasterObject(row, inv_invoiceauth_id, invNo);

                var SoPhieu = row["SoPhieu"].ToString();
                var jArrayDetail = GetJArrayDetail(sqlConnection, SoPhieu, out inv_TotalAmount, out inv_TotalAmountWithoutVat, out inv_vatAmount, out inv_discountAmount, thue, row);

                if (!string.IsNullOrEmpty(row["SuaTienThue1"].ToString()) && bool.Parse(row["SuaTienThue1"].ToString()) == true)
                {
                    
                   
                    masterJObject.Add("inv_TotalAmountWithoutVat", inv_TotalAmountWithoutVat);

                    inv_vatAmount = !string.IsNullOrEmpty(row["TienThue"].ToString()) ? double.Parse(row["TienThue"].ToString()) : 0;

                    masterJObject.Add("inv_vatAmount", inv_vatAmount);

                    masterJObject.Add("inv_discountAmount", inv_discountAmount);
                    inv_TotalAmount = inv_TotalAmountWithoutVat - inv_discountAmount + inv_vatAmount;


                     masterJObject.Add("inv_TotalAmount", inv_TotalAmount);
                }
                else
                {
                    masterJObject.Add("inv_TotalAmount", inv_TotalAmount);
                    masterJObject.Add("inv_TotalAmountWithoutVat", inv_TotalAmountWithoutVat);
                    masterJObject.Add("inv_vatAmount", inv_vatAmount);
                    masterJObject.Add("inv_discountAmount", inv_discountAmount);
                }
           

                var data = new JObject
                {
                    {"tab_id", "TAB00188"},
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

        private static JArray GetJArrayDetail(SqlConnection sqlConnection, string SoPhieu, out double inv_TotalAmount, out double inv_TotalAmountWithoutVat, out double inv_vatAmount, out double inv_discountAmount, double thue, DataRow row)
        {
            inv_TotalAmount = 0.0;
            inv_TotalAmountWithoutVat = 0.0;
            inv_vatAmount = 0.0;
            inv_discountAmount = 0.0;
            double inv_vatAmount1 = 0;
            bool suathue = false;
            var jArrayDetail = new JArray();

           
                
                string whereVoucherDetail = $" WHERE SoPhieu = '{SoPhieu}' AND GianTiep=0 AND ButToanThueGTGT=0 ORDER BY STT";
                string sqlSelectInvoiceDetail = CommonConstants.SqlSelectSaInvoiceDetail2017;
                sqlSelectInvoiceDetail += whereVoucherDetail;

                var dataTableInvoiceDetail = DataContext.GetDataTableTest(sqlConnection, sqlSelectInvoiceDetail);
                if (dataTableInvoiceDetail.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(row["SuaTienThue1"].ToString()) && bool.Parse(row["SuaTienThue1"].ToString()) == true)
                    {
                        if (dataTableInvoiceDetail.Rows.Count == 1)
                        {
                            foreach (DataRow dataRowInvoiceDetail in dataTableInvoiceDetail.Rows)
                            {
                                suathue = true;
                                inv_vatAmount1 = !string.IsNullOrEmpty(row["TienThue"].ToString()) ? double.Parse(row["TienThue"].ToString()) : 0;
                                var detailJObject = ConvertDetailJObjectSaInvoiceDetail(dataRowInvoiceDetail, thue, inv_vatAmount1, suathue);

                                inv_TotalAmount += (double)detailJObject["inv_TotalAmount"];
                                inv_TotalAmountWithoutVat += (double)detailJObject["inv_TotalAmountWithoutVat"];
                                inv_vatAmount += (double)detailJObject["inv_vatAmount"];
                                inv_discountAmount += (double)detailJObject["inv_discountAmount"];
                                jArrayDetail.Add(detailJObject);


                                Log.Debug("OK");
                            }
                        }
                    }
                    else
                    {
                        foreach (DataRow dataRowInvoiceDetail in dataTableInvoiceDetail.Rows)
                        {

                            var detailJObject = ConvertDetailJObjectSaInvoiceDetail(dataRowInvoiceDetail, thue, inv_vatAmount1, suathue);
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

            //string sqlSelectInvoiceDetail = BaseConfig.Version == "2017"
            //    ? CommonConstants.SqlSelectSaInvoiceDetail2017
            //    : CommonConstants.SqlSelectSaInvoiceDetail2012;

            //var whereInvoiceDetail = $" WHERE a.RefID = '{refId}' ORDER BY a.SortOrder";
            //sqlSelectInvoiceDetail += whereInvoiceDetail;

            //var dataTableInvoiceDetail = DataContext.GetDataTableTest(sqlConnection, sqlSelectInvoiceDetail);
            //if (dataTableInvoiceDetail.Rows.Count > 0)
            //{
            //    foreach (DataRow dataRowInvoiceDetail in dataTableInvoiceDetail.Rows)
            //    {
            //        var detailJObject = ConvertDetailJObjectSaInvoiceDetail(dataRowInvoiceDetail);
            //        inv_TotalAmount += (double)detailJObject["inv_TotalAmount"];
            //        inv_TotalAmountWithoutVat += (double)detailJObject["inv_TotalAmountWithoutVat"];
            //        inv_vatAmount += (double)detailJObject["inv_vatAmount"];
            //        inv_discountAmount += (double)detailJObject["inv_discountAmount"];
            //        jArrayDetail.Add(detailJObject);
            //    }
            //}

            return jArrayDetail;
        }
    }
}