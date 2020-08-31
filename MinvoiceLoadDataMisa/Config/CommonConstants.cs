﻿namespace MinvoiceLoadDataMisa.Config
{
    public class CommonConstants
    {

        public static string TableInvocie = "TableInvocie";

        public static string TableInvoiceDetail = "TableInvoiceDetail";

        public static string TableVoucherDetail = "TableVoucherDetail";

        public static string UsernameLoginWeb = "UsernameLoginWeb";

        public static string PasswordLoginWeb = "PasswordLoginWeb";

        public static string UrlLogin = "UrlLogin";

        public static string UrlSave = "UrlSave";

        public static string UrlGetInvoiceByInvoiceAuthId = "UrlGetInvoiceByInvoiceAuthId";

        public static string TimeLoadData = "TimeLoadData";

        public static string MauSo = "MauSo";

        public static string KyHieu = "KyHieu";

        public static string UrlRef = "UrlRef";

        public static string InvoiceCodeId = "InvoiceCodeId";

        public static string InvoiceCodeId2 = "InvoiceCodeId2";

        public static string TableVoucher = "TableVoucher";

        public static string Command = "Command";

        public static string UrlCommand = "UrlCommand";

        public static string Version = "Version";

        public static string TableInventoryItem = "TableInventoryItem";

        public static string TableUnit = "TableUnit";

        public static string RefInventoryItem = "RefInventoryItem";

        public static string RefUnit = "RefUnit";

        public static string SqlSelectSaInvoice = $"SELECT * FROM {BaseConfig.TableInvocie} WHERE InvSeries = '{BaseConfig.KyHieu}' AND IsInvoice IS NULL ORDER BY InvDate";

        public static string SqlSelectSaInvoiceDetail2012 = $"SELECT a.*, b.InventoryItemCode FROM {BaseConfig.TableInvoiceDetail} AS a LEFT JOIN {BaseConfig.TableInventoryItem} AS b ON b.{BaseConfig.RefInventoryItem} = a.{BaseConfig.RefInventoryItem} ";

        public static string SqlSelectSaInvoiceDetail2017 = $"SELECT a.*, b.InventoryItemCode, c.UnitName FROM {BaseConfig.TableInvoiceDetail} AS a LEFT JOIN {BaseConfig.TableInventoryItem} AS b ON b.{BaseConfig.RefInventoryItem} = a.{BaseConfig.RefInventoryItem} LEFT JOIN {BaseConfig.TableUnit} AS c ON c.{BaseConfig.RefUnit} = a.{BaseConfig.RefUnit} ";

        public static string SqlSelectSaVoucherDetail2017 = $"SELECT a.*, b.InventoryItemCode, c.UnitName FROM {BaseConfig.TableVoucherDetail} AS a LEFT JOIN {BaseConfig.TableInventoryItem} AS b ON b.{BaseConfig.RefInventoryItem} = a.{BaseConfig.RefInventoryItem} LEFT JOIN {BaseConfig.TableUnit} AS c ON c.{BaseConfig.RefUnit} = a.{BaseConfig.RefUnit} ";
    }
}
