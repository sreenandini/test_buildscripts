using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.MeterAdjustmentTool.Helpers;
using System.Data.Linq;
using System.Data;
using System.Data.SqlClient;
using BMC.Common.ExceptionManagement;
using System.Collections;
using System.Resources;
using System.Globalization;

namespace BMC.MeterAdjustmentTool
{
    public partial class rsp_GetSiteDetailsResult
    {
        public BmcConnectionStringBuilder GetDecryptedConnectionString()
        {
            BmcConnectionStringBuilder connString = new BmcConnectionStringBuilder();
            Binary binData = this.Site_DB_ConnectionString;
            if (binData != null)
            {
                byte[] bytData = binData.ToArray();
                if (bytData != null)
                {
                    connString.LoadConnectionString(TripleDESEncryption.DecryptFromBytes(bytData));
                }
            }
            return connString;
        }
    }

    public partial class ExchangeDataContext
    {
        public DataSet ExecuteQueryAndGetDataSet(string spName, SqlParameter[] parameters, string[] tableNames)
        {
            DataTable dt = new DataTable();
            DataSet dsResult = new DataSet();

            try
            {
                SqlConnection conn = this.Connection as SqlConnection;
                SqlCommand cmdGetDataTable = new SqlCommand(spName, conn);
                cmdGetDataTable.CommandType = CommandType.StoredProcedure;
                cmdGetDataTable.CommandTimeout = 0;
                cmdGetDataTable.Parameters.AddRange(parameters);

                if (conn.State != ConnectionState.Open) { conn.Open(); }
                SqlDataReader reader = cmdGetDataTable.ExecuteReader();
                dsResult = this.ConvertDataReaderToDataSet(reader, tableNames);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return dsResult;
        }

        public DataSet ConvertDataReaderToDataSet1(SqlDataReader reader)
        {
            DataSet dataSet = new DataSet();

            try
            {
                DataTable schemaTable = reader.GetSchemaTable();
                DataTable dataTable = new DataTable(MeterGlobals.ORIGINAL_TABLE);
                int intCounter = 0;
                //DataRow dataRw = schemaTable.Rows[0];
                //DataColumn dcColumn = new DataColumn("Int");
                //dataTable.Columns.Add(dcColumn);
                for (intCounter = 0; intCounter <= schemaTable.Rows.Count - 1; intCounter++)
                {
                    DataRow dataRow = schemaTable.Rows[intCounter];
                    string columnName = (string)dataRow["ColumnName"];
                    DataColumn column = new DataColumn(columnName, (Type)dataRow["DataType"]);
                    dataTable.Columns.Add(column);
                }
                dataSet.Tables.Add(dataTable);
                while (reader.Read())
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow[0] = 1;
                    for (intCounter = 0; intCounter <= reader.FieldCount - 1; intCounter++)
                    {
                        dataRow[intCounter + 1] = reader.GetValue(intCounter);
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return dataSet;
        }

        public DataSet ConvertDataReaderToDataSet(SqlDataReader reader, string[] tableNames)
        {
            DataSet dataSet = new DataSet();

            try
            {
                int i = 0;
                do
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
                    dataSet.Tables.Add(dataTable);

                    if (tableNames != null)
                    {
                        if (tableNames.Length >= dataSet.Tables.Count)
                        {
                            dataSet.Tables[i].TableName = tableNames[i];
                        }
                    }

                    while (reader.Read())
                    {
                        DataRow dataRow = dataTable.NewRow();
                        for (intCounter = 0; intCounter <= reader.FieldCount - 1; intCounter++)
                        {
                            dataRow[intCounter] = reader.GetValue(intCounter);
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                    i++;
                } while (reader.NextResult()); 
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return dataSet;
        }

        #region "Commeneted - to remove"
        /*
        public string GetRegionCurrencyCulture()
        {
            string sCurrency_Culture = "UK";
               
            try
            {
                string spName = "rsp_GetSetting";

                SqlConnection conn = new SqlConnection(MainForm.CurrentForm.ConnectionString);
                
                SqlParameter[] sSqlParams = new SqlParameter[4];
                sSqlParams[0] = new SqlParameter("@Setting_ID", 0);
                sSqlParams[1] = new SqlParameter("@Setting_Name", "REGION");
                sSqlParams[2] = new SqlParameter("@Setting_Default", "UK");
                sSqlParams[3] = new SqlParameter("@Setting_Value", SqlDbType.VarChar, 50);
                sSqlParams[3].Direction = ParameterDirection.Output;

                SqlCommand cmdGetData = new SqlCommand(spName, conn);
                cmdGetData.CommandType = CommandType.StoredProcedure;
                cmdGetData.CommandTimeout = 0;
                cmdGetData.Parameters.AddRange(sSqlParams);

                if (conn.State != ConnectionState.Open) { conn.Open(); }
                int retval = cmdGetData.ExecuteNonQuery();

                if (sSqlParams[3].SqlValue != DBNull.Value)
                    sCurrency_Culture = sSqlParams[3].SqlValue.ToStringSafe();
                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                sCurrency_Culture = "UK";
            }

            return sCurrency_Culture;
        }

        public DataSet TranslateDataSet(DataSet ds, ResourceManager resourceManager)
        {
            try
            {
                if (resourceManager == null) return ds;
                DataTable dtTobeBounded = null;
                Hashtable Dict = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                ResourceSet rset = resourceManager.GetResourceSet(CultureInfo.InvariantCulture, true, false);
                
                if (rset == null) return ds;

                string sCurrentRegion = GetRegionCurrencyCulture();
                string currency = string.Empty;

                if(sCurrentRegion.ToUpper().CompareTo("UK") == 0)
                {
                    currency = new RegionInfo("en-GB").CurrencySymbol;
                }
                else if (sCurrentRegion.ToUpper().CompareTo("AR") == 0)
                {
                    currency = new RegionInfo("es-AR").CurrencySymbol;
                }
                else
                {
                    currency = new RegionInfo("en-US").CurrencySymbol;
                }
                
                
                foreach (System.Collections.DictionaryEntry col in rset)
                {
                    string colValue = string.Empty;
                    try
                    {
                        colValue = col.Value.ToString();
                        if (colValue.Contains("#"))
                        {
                            colValue = colValue.Replace("#", currency);
                        }
                    }
                    catch { }
                    Dict.Add(col.Key, colValue);
                }

                IEnumerator enumDict = ds.Tables[0].Columns.GetEnumerator();
                dtTobeBounded = ds.Tables[0].Copy();
                while (enumDict.MoveNext())
                {
                    if (!Dict.ContainsKey(((DataColumn)enumDict.Current).ColumnName.Trim()))
                    {
                        dtTobeBounded.Columns.Remove(((DataColumn)enumDict.Current).ColumnName);
                    }
                    else
                    { 
                        string columnName = Dict[((DataColumn)enumDict.Current).ColumnName.ToString()].ToString();
                        dtTobeBounded.Columns[((DataColumn)enumDict.Current).ColumnName].ColumnName = columnName;
                        for (int i = 0; i < dtTobeBounded.Rows.Count; i++)
                        {
                            dtTobeBounded.Rows[i][columnName] = (string.IsNullOrEmpty(dtTobeBounded.Rows[i][columnName].ToString())) ? "0" : dtTobeBounded.Rows[i][columnName].ToString();
                        }
                    }
                }
                dtTobeBounded.TableName = MeterGlobals.TRANSLATED_TABLE;
                ds.Tables.Add(dtTobeBounded.Copy());                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return ds;
        }

        public DataSet TranslateDataSetHourly(DataSet ds, ResourceManager resourceManager)
        {
            try
            {
                DataSet ds2 = this.TranslateDataSet(ds, resourceManager);

                Hashtable Dict = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                ResourceSet rset = resourceManager.GetResourceSet(CultureInfo.InvariantCulture, true, false);
                if (rset == null) return ds;

                foreach (System.Collections.DictionaryEntry col in rset)
                {
                    Dict.Add(col.Key, col.Value);
                }
                ds.Tables[MeterGlobals.TRANSLATED_TABLE].Columns.Add(new DataColumn("Hour"));
                ds.Tables[MeterGlobals.TRANSLATED_TABLE].Columns["Hour"].SetOrdinal(2);
                IEnumerator enumDict = ds.Tables[1].Rows.GetEnumerator();
                while (enumDict.MoveNext())
                {
                    if (Dict.ContainsKey(((DataRow)enumDict.Current)["Column"].ToString().Trim()))
                    {
                        ((DataRow)enumDict.Current)["Hour"] = Dict[((DataRow)enumDict.Current)["Column"].ToString().Trim()];
                    }
                    else
                    {
                        ((DataRow)enumDict.Current)["Hour"] = ((DataRow)enumDict.Current)["Column"];
                    }
                }
                ds.Tables[MeterGlobals.TRANSLATED_TABLE].Columns.Remove("Column");
                return ds;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return ds;
        }

        public DataSet TranslateDataSetForEdit(DataSet ds, ResourceManager resourceManager)
        {
            try
            {
                if (resourceManager == null) return ds;
                Hashtable Dict = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                ResourceSet rset = resourceManager.GetResourceSet(CultureInfo.InvariantCulture, true, false);
                if (rset == null) return ds;

                string sCurrentRegion = GetRegionCurrencyCulture();
                string currency = string.Empty;

                if (sCurrentRegion.ToUpper().CompareTo("UK") == 0)
                {
                    currency = new RegionInfo("en-GB").CurrencySymbol;
                }
                else if (sCurrentRegion.ToUpper().CompareTo("AR") == 0)
                {
                    currency = new RegionInfo("es-AR").CurrencySymbol;
                }
                else
                {
                    currency = new RegionInfo("en-US").CurrencySymbol;
                }

                foreach (System.Collections.DictionaryEntry col in rset)
                {
                    string colValue = string.Empty;
                    try
                    {
                        colValue = col.Value.ToString();
                        if (colValue.Contains("#"))
                        {
                            colValue = colValue.Replace("#", currency);
                        }
                    }
                    catch { }
                    Dict.Add(col.Key, colValue);
                }

                ds.Tables[0].Columns.Add(new DataColumn("Meter"));
                ds.Tables[0].Columns["Meter"].SetOrdinal(0);
                IEnumerator enumDict = ds.Tables[0].Rows.GetEnumerator();
                while (enumDict.MoveNext())
                {
                    if (Dict.ContainsKey(((DataRow)enumDict.Current)["Column"].ToString().Trim().ToUpper()))
                    {
                        ((DataRow)enumDict.Current)["Meter"] = Dict[((DataRow)enumDict.Current)["Column"].ToString().ToUpper().Trim()];
                    }
                    else
                    {
                        ((DataRow)enumDict.Current)["Meter"] = ((DataRow)enumDict.Current)["Column"].ToString().ToUpper();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return ds;
        }
         */
        #endregion
    }

    public partial class CurrencyHelper
    {
        public static string GetCurrencySymbol()
        {
            string sCurrentRegion = GetRegionCurrencyCulture();
            string currency = string.Empty;

            if (sCurrentRegion.ToUpper().CompareTo("UK") == 0)
            {
                currency = new RegionInfo("en-GB").CurrencySymbol;
            }
            else if (sCurrentRegion.ToUpper().CompareTo("AR") == 0)
            {
                currency = new RegionInfo("es-AR").CurrencySymbol;
            }
            else
            {
                currency = new RegionInfo("en-US").CurrencySymbol;
            }
            return currency;
        }

        private static string GetRegionCurrencyCulture()
        {
            string sCurrency_Culture = "UK";

            try
            {
                string spName = "rsp_GetSetting";

                SqlConnection conn = new SqlConnection(MainForm.CurrentForm.ConnectionString);

                SqlParameter[] sSqlParams = new SqlParameter[4];
                sSqlParams[0] = new SqlParameter("@Setting_ID", 0);
                sSqlParams[1] = new SqlParameter("@Setting_Name", "REGION");
                sSqlParams[2] = new SqlParameter("@Setting_Default", "UK");
                sSqlParams[3] = new SqlParameter("@Setting_Value", SqlDbType.VarChar, 50);
                sSqlParams[3].Direction = ParameterDirection.Output;

                SqlCommand cmdGetData = new SqlCommand(spName, conn);
                cmdGetData.CommandType = CommandType.StoredProcedure;
                cmdGetData.CommandTimeout = 0;
                cmdGetData.Parameters.AddRange(sSqlParams);

                if (conn.State != ConnectionState.Open) { conn.Open(); }
                int retval = cmdGetData.ExecuteNonQuery();

                if (sSqlParams[3].SqlValue != DBNull.Value)
                    sCurrency_Culture = sSqlParams[3].SqlValue.ToStringSafe();

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                sCurrency_Culture = "UK";
            }

            return sCurrency_Culture;
        }
    }
}
