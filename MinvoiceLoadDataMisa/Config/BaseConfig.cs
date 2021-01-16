using System.Configuration;

namespace MinvoiceLoadDataMisa.Config
{
    public class BaseConfig
    {

        public static bool ReturnInfoCustomer = false;

        public static string TableInvocie = Properties.Settings.Default.TableInvocie.Trim();

        public static string TableInvoiceDetail = Properties.Settings.Default.TableInvoiceDetail.Trim();

        public static string DauVao = Properties.Settings.Default.DauVao.Trim();

        public static string ConnectionString = Properties.Settings.Default.ConnectionString.Trim();

        public static string UsernameLoginWeb = Properties.Settings.Default.UsernameLoginWeb.Trim();

        public static string PasswordLoginWeb = Properties.Settings.Default.PasswordLoginWeb.Trim();

        public static string UrlLogin = Properties.Settings.Default.UrlLogin.Trim();

        public static string UrlSave = Properties.Settings.Default.UrlSave.Trim();

        public static string UrlRef = Properties.Settings.Default.UrlRef.Trim();

        public static string UrlGetInvoiceByInvoiceAuthId =
            Properties.Settings.Default.UrlGetInvoiceByInvoiceAuthId.Trim();

        public static decimal TimeGetData = !string.IsNullOrEmpty(Properties.Settings.Default.TimeLoadData)
            ? decimal.Parse(Properties.Settings.Default.TimeLoadData)
            : 300;

        public static string MauSo = Properties.Settings.Default.MauSo.Trim();

        public static string KyHieu = Properties.Settings.Default.KyHieu.Trim();

        public static string InvoiceCodeId = Properties.Settings.Default.InvoiceCodeId.Trim();

        public static string TableDM_DTCN = Properties.Settings.Default.TableDM_DTCN.Trim();

        public static string Command = Properties.Settings.Default.Command.Trim();

        public static string UrlCommand = Properties.Settings.Default.UrlCommand.Trim();

        public static string Version = Properties.Settings.Default.Version.Trim();

        public static string TablePS_BangKeGTGT = Properties.Settings.Default.TablePS_BangKeGTGT.Trim();

        public static string CmCheck = Properties.Settings.Default.CmCheck.Trim();

        public static string RefInventoryItem = Properties.Settings.Default.RefInventoryItem.Trim();

        public static string RefUnit = Properties.Settings.Default.RefUnit.Trim();

        public static string IDUpdate = Properties.Settings.Default.InvoiceCodeId2.Trim();

        public static string _kyHieu = Properties.Settings.Default._kyHieu.Trim();

        public static string editmode = Properties.Settings.Default.Editmode.Trim();

    }


    public enum ServerNameOption
    {
        Misa,
        Minvoice
    }
}
