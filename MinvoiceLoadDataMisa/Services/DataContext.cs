using System;
using System.Data;
using System.Data.SqlClient;
using log4net;
using MinvoiceLoadDataMisa.Config;
using Newtonsoft.Json.Linq;

namespace MinvoiceLoadDataMisa.Services
{
    public class DataContext
    {
        private static readonly ILog Log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static SqlConnection GetDbConnection(string dataSource, string initialCatalog, string userId,
            string password)
        {
            string connectionString =
                $@"Data Source={dataSource};Initial Catalog={initialCatalog};User ID={userId};Password={password};Connection Timeout=999; MultipleActiveResultSets=True";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            return sqlConnection;
        }
        public static DataTable GetDataTableTest(SqlConnection sqlConnection, string tableName,string tableBANGKE, string tableDMDTCN,string where = "")        
        {
            DataTable dataTable = new DataTable();
            try
            {
                string commandText = $"SELECT {tableName}.SuaTienThue as SuaTienThue1, {tableName}.*, {tableBANGKE}.*, {tableDMDTCN}.Email, {tableDMDTCN}.DienThoai FROM {tableName} LEFT JOIN {tableBANGKE} ON {tableName}.SoPhieu = {tableBANGKE}.SoPhieu LEFT JOIN {tableDMDTCN} ON {tableDMDTCN}.ID = {tableName}.DoiTuongCongNoID {where}";
                //if (BaseConfig.Version.Equals("2017"))
                //{
                //    commandText =
                //        //$"IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'AccountObject') " +
                //        $"SELECT {tableName}.*, PS_BangkeGTGT.NgayPH,  PS_BangkeGTGT.VAT, PS_BangkeGTGT.DoanhSo,PS_BangkeGTGT.TienThue  FROM {tableName} LEFT JOIN {tableBANGKE} ON {tableName}.SoPhieu = {tableBANGKE}.SoPhieu LEFT JOIN {tableDMDTCN} ON {tableDMDTCN}.ID = {tableName}.DoiTuongCongNoID {where} AND {tableBANGKE}.DauVao={DauVao} ORDER BY InvDate, InvNo ASC " +
                //        $"ELSE SELECT * FROM {tableName} {where}";

                //}
                SqlDataAdapter adapter = new SqlDataAdapter(commandText, sqlConnection);
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception e)
            {
                throw new DataException(e.ToString());
            }
        }

        public static void UpdateMisa(SqlConnection sqlConnection, string SoPhieu, string tableName, JToken jObject)
        {
            try
            {
                string commandText = $"UPDATE {tableName} SET VAT_Seri = '{jObject[0]["inv_invoiceSeries"].ToString().Trim()}', IsInvoice = 1 WHERE SoPhieu = '{SoPhieu}'";

                //string update2017 = $"UPDATE {tableName} SET InvTemplateNo = '{jObject["mau_hd"]}', InvSeries = '{jObject["inv_invoiceSeries"]}', IsInvoice = 1 WHERE RefID = '{refId}'";

                //string commandText = BaseConfig.Version.Equals("2012") ? update2012 : update2017;
                Log.Debug(commandText);
                SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection)
                {
                    CommandType = CommandType.Text
                };
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static void UpdateMisaVoucher(SqlConnection sqlConnection, string refId, string tableVoucherName, string tableVoucherDetailName, JToken jObject)
        {
            try
            {
                if (BaseConfig.Version.Equals("2017"))
                {
                    string commandText = $"UPDATE {tableVoucherName} SET InvSeries = '{jObject["inv_invoiceSeries"]}' WHERE RefID IN (SELECT RefID FROM {tableVoucherDetailName} WHERE SAInvoiceRefID = '{refId}')";

                    Log.Debug(commandText);
                    SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection)
                    {
                        CommandType = CommandType.Text
                    };
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static DataTable GetDataTableTest(SqlConnection sqlConnection, string sql)
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, sqlConnection);
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception e)
            {
                throw new DataException(e.ToString());
            }
        }


    }
}
