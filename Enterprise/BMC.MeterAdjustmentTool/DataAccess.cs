using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace BMC.MeterAdjustmentTool
{
    class DataAccess
    {
        //string ExchangeConnectionString = ConfigurationManager.AppSettings["BMC.MeterAdjustmentTool.Properties.Settings.ExchangeConnectionString"];
        //SqlConnection sc = new SqlConnection("ExchangeConnectionString");       
        SqlConnection sc = null;

        public DataAccess(string connectionString)
        {
            sc = new SqlConnection(connectionString);
            sc.Open();
        }

        public DataSet GetAuditReportDetails(DateTime Fromdate, DateTime todate)
        {
            DataSet ds = new DataSet();
            SqlCommand scom = new SqlCommand("GetDeltaAdjustmentAudit", sc);
            scom.CommandType = CommandType.StoredProcedure;
            SqlParameter sparam1 = scom.Parameters.Add("@AuditStartDate", SqlDbType.DateTime);
            SqlParameter sparam2 = scom.Parameters.Add("@AuditEndDate", SqlDbType.DateTime);
            sparam1.Value = Fromdate;
            sparam2.Value = todate;
            if (sc.State == ConnectionState.Closed) { sc.Open(); }
            ds = ConvertDSForAuditReport(scom.ExecuteReader(CommandBehavior.CloseConnection));
            if (sc.State == ConnectionState.Open) { sc.Close(); }

            return ds;
        }
        public DataSet ConvertDSForAuditReport(SqlDataReader reader)
        {
            DataSet dataSet = new DataSet();
            try
            {


                DataTable schemaTable = reader.GetSchemaTable();
                DataTable dataTable = new DataTable();
                int intCounter = 0;
                for (intCounter = 0; intCounter <= schemaTable.Rows.Count - 1; intCounter++)
                {
                    DataRow dataRow = schemaTable.Rows[intCounter];
                    string columnName = (string)dataRow["ColumnName"];
                    DataColumn column = new DataColumn(columnName, (Type)dataRow["DataType"]);
                    dataTable.Columns.Add(column);
                }
                dataTable.TableName = "AuditReport";
                dataSet.Tables.Add(dataTable);
                while (reader.Read())
                {
                    DataRow dataRow = dataTable.NewRow();
                    for (intCounter = 0; intCounter <= reader.FieldCount - 1; intCounter++)
                    {
                        dataRow[intCounter] = reader.GetValue(intCounter);
                    }
                    dataTable.Rows.Add(dataRow);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataSet;

        }

    }
}
