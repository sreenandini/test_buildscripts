using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using BMC.Common.Utilities;
//using Microsoft.SqlServer.Management;
//using Microsoft.SqlServer.Management.Smo;
using BMC.DataAccess;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
//using Microsoft.SqlServer.Management.Common;
//using Microsoft.SqlServer.Management.Smo;
using System.IO;
using System.Text.RegularExpressions;
namespace BMC.DBInterface.CashDeskOperator
{
    public class SiteSetupDataAccess
    {
        #region "Private Variables"
        CommonDataAccess commonDataAccess = new CommonDataAccess();
        #endregion

        #region "Private Functions"

        private static SqlParameter AddParameter<T>(string ParamName, SqlDbType DataType, T Value, int Size)
        {
            SqlParameter Param = new SqlParameter();
            Param.SqlDbType = DataType;
            Param.ParameterName = ParamName;
            Param.Value = Value;
            Param.Size = Size;
            return Param;
        }

        private static SqlParameter AddParameter<T>(string ParamName, SqlDbType DataType, T Value)
        {
            SqlParameter Param = new SqlParameter();
            Param.SqlDbType = DataType;
            Param.ParameterName = ParamName;
            Param.Value = Value;
            return Param;
        }

        private static SqlParameter AddOutputParameter<T>(string ParamName, SqlDbType DataType, T Value)
        {
            SqlParameter Param = new SqlParameter();
            Param.SqlDbType = DataType;
            Param.ParameterName = ParamName;
            Param.Value = Value;
            Param.Direction = ParameterDirection.Output;
            return Param;
        }

        private static string ConvertDataTableToXML(DataTable dtBuildSQL, string sRoot, string sTableName)
        {
            DataSet dsBuildSQL = new DataSet(sRoot);
            StringBuilder sbSQL;
            StringWriter swSQL;
            string XMLformat = string.Empty;
            DataColumn column = new DataColumn();
            //string rp = @"(?<DATE>\d{4}-\d{2}-\d{2})(?<TIME>T\d{2}:\d{2}:\d{2}."+
            //            @"\d{7}-)(?<HOUR>\d{2})(?<LAST>:\d{2})";

            try
            {


                sbSQL = new StringBuilder();
                swSQL = new StringWriter(sbSQL);
                dsBuildSQL.Merge(dtBuildSQL, true, MissingSchemaAction.AddWithKey);
                dsBuildSQL.Tables[0].TableName = sTableName;
                foreach (DataRow row in dsBuildSQL.Tables[0].Rows)
                {
                    foreach (DataColumn col in dsBuildSQL.Tables[0].Columns)
                        col.ColumnMapping = MappingType.Attribute;

                    //DataRow updateRow = dsBuildSQL.Tables[0].Rows[dsBuildSQL.Tables[0].Rows.IndexOf(row)];
                    //updateRow[column] = DateTime.Parse(row[column].ToString()).ToString("dd MMM yyyy HH:mm:ss");
                    //dsBuildSQL.Tables[0].AcceptChanges();

                }
                //dsBuildSQL.AcceptChanges();
                dsBuildSQL.WriteXml(swSQL, XmlWriteMode.WriteSchema);
                XMLformat = sbSQL.ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return XMLformat;
        }

        private static string ConvertDataTableToXMLDate(DataTable dtBuildSQL, string sRoot, string sTableName, string strDtColumnName)
        {
            DataSet dsBuildSQL = new DataSet(sRoot);
            StringBuilder sbSQL;
            StringWriter swSQL;
            string XMLformat = string.Empty;
            DataColumn column = new DataColumn();
            string rp = @"(?<DATE>\d{4}-\d{2}-\d{2})(?<TIME>T\d{2}:\d{2}:\d{2}." +
                        @"\d{7}-)(?<HOUR>\d{2})(?<LAST>:\d{2})";

            try
            {


                sbSQL = new StringBuilder();
                swSQL = new StringWriter(sbSQL);
                dsBuildSQL.Merge(dtBuildSQL, true, MissingSchemaAction.AddWithKey);
                dsBuildSQL.Tables[0].TableName = sTableName;
                foreach (DataRow row in dsBuildSQL.Tables[0].Rows)
                {
                    foreach (DataColumn col in dsBuildSQL.Tables[0].Columns)
                        col.ColumnMapping = MappingType.Attribute;

                    //DataRow updateRow = dsBuildSQL.Tables[0].Rows[dsBuildSQL.Tables[0].Rows.IndexOf(row)];
                    //updateRow[column] = DateTime.Parse(row[column].ToString()).ToString("dd MMM yyyy HH:mm:ss");
                    //dsBuildSQL.Tables[0].AcceptChanges();

                }
                //dsBuildSQL.AcceptChanges();
                dsBuildSQL.WriteXml(swSQL, XmlWriteMode.WriteSchema);
                //sBuildSQL.WriteXml(new DateFormatXmlTextWriter( swSQL, strDtColumnName, "yyyy-MM-ddTHH:mm:ss"), XmlWriteMode.IgnoreSchema);
                //XMLformat = sbSQL.ToString();
                string fixedString = Regex.Replace(sbSQL.ToString(), rp, new MatchEvaluator(getHourOffset));
                XMLformat = fixedString;


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return XMLformat;
        }

        private static string getHourOffset(Match m)
        {
            // Need to also account for Daylights Savings 

            // Time when calculating UTC offset value

            DateTime dtLocal = DateTime.Parse(m.Result("${date}"));
            DateTime dtUTC = dtLocal.ToUniversalTime();
            int hourLocalOffset = dtUTC.Hour - dtLocal.Hour;
            int hourServer = int.Parse(m.Result("${hour}"));
            string newHour = (hourServer + (hourLocalOffset -
                hourServer)).ToString("0#");
            string retString = m.Result("${date}" + "${time}" +
               newHour + "${last}");

            return retString;
        }

        public string GetTicketingConnectionString()
        {
            string strConnectionString = "";

            try
            {
                strConnectionString = DatabaseHelper.GetTicketingConnectionString();

                return strConnectionString;
            }
            catch (Exception ex)
            {
                strConnectionString = "";
                return strConnectionString;
            }
        }


        #endregion

        #region "Public Functions"

        public bool IsValidSiteCode(int SiteCode)
        {
            bool isValid = false;

            try
            {
                
                object result = SqlHelper.ExecuteScalar(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "rsp_GetSiteLocCode",new SqlParameter("@SiteCode",@SiteCode));
                return Convert.ToInt32(result) == 1;
            }
            catch (Exception ex)
            {
                isValid = false;
                ExceptionManager.Publish(ex);
            }

            return isValid;

        }

        public bool DatabaseIsEmpty()
        {

            bool isEmpty = false;
            SqlDataReader oReader = null;
            try
            {
                oReader = SqlHelper.ExecuteReader(CommonDataAccess.ExchangeConnectionString, CommandType.Text, "Select top 1 * from Installation");
                if (oReader.HasRows)
                {
                    isEmpty = false;
                    oReader = null;
                    LogManager.WriteLog("Installation table contains records.", LogManager.enumLogLevel.Info);
                    return isEmpty;
                }
                oReader = SqlHelper.ExecuteReader(CommonDataAccess.ExchangeConnectionString, CommandType.Text, "Select top 1 * from Bar_Position");
                if (oReader.HasRows)
                {
                    isEmpty = false;
                    oReader = null;
                    LogManager.WriteLog("Bar_Position table contains records.", LogManager.enumLogLevel.Info);
                    return isEmpty;
                }
                oReader = SqlHelper.ExecuteReader(CommonDataAccess.ExchangeConnectionString, CommandType.Text, "Select top 1 * from Zone");
                if (oReader.HasRows)
                {
                    isEmpty = false;
                    oReader = null;
                    LogManager.WriteLog("Zone table contains records.", LogManager.enumLogLevel.Info);
                    return isEmpty;
                }
                isEmpty = true;
            }
            catch (Exception ex)
            {
                isEmpty = false;
                ExceptionManager.Publish(ex);
            }

            return isEmpty;
        }

        public bool ImportSiteDetails(string strResult)
        {
            bool bSuccess = false;

            try
            {
                SqlParameter[] objSiteSqlParams = new SqlParameter[2];

                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "doc";
                objSQLParam.Value = strResult;
                objSQLParam.Direction = ParameterDirection.Input;
                objSiteSqlParams[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "IsSuccess";
                objSQLParam.Direction = ParameterDirection.Output;
                objSQLParam.SqlDbType = SqlDbType.Int;
                objSiteSqlParams[1] = objSQLParam;

                SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SPIMPORTSITEDETAILS, objSiteSqlParams);

                if (int.Parse(objSiteSqlParams[1].Value.ToString()) == 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("ImportSite WS " + "  Success value " + bSuccess.ToString(), LogManager.enumLogLevel.Info);
                }
                else
                {
                    bSuccess = false;
                    LogManager.WriteLog("ImportSite WS " + "  failed due to " + objSiteSqlParams[1].Value.ToString(), LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                bSuccess = false;
                ExceptionManager.Publish(ex);
            }
            return bSuccess;
        }

        public bool ImportSiteAlliance(string strResult)
        {
            XmlDocument oXmlDoc;
            XmlNodeList oXmlNodelist;
            int iRecordInserted = 0;
            int i = 0;
            bool bSuccess = false;
            try
            {
                //oXmlDoc = new XmlDocument();
                //oXmlDoc.LoadXml(strResult);
                //oXmlNodelist = oXmlDoc.DocumentElement.GetElementsByTagName("SiteAlliances");
                LogManager.WriteLog("Storing records in SiteAlliance table", LogManager.enumLogLevel.Info);
                //while (i < oXmlNodelist.Count)
                //{
                SqlParameter[] oParam = new SqlParameter[1];
                oParam[0] = new SqlParameter("@doc", strResult);
                iRecordInserted = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SPIMPORTSITEALLIANCE, oParam);
                if (iRecordInserted > 0)
                {
                    LogManager.WriteLog("RecordInserted in SiteAlliance table: " + iRecordInserted.ToString(), LogManager.enumLogLevel.Debug);
                }
                //i++;
                //}
                //LogManager.WriteLog("xmllist count in Site Alliance table:  " + oXmlNodelist.Count.ToString(), LogManager.enumLogLevel.Debug);
                LogManager.WriteLog("Importing SiteAlliance completed.", LogManager.enumLogLevel.Info);
                bSuccess = true;
            }


            catch (Exception ex)
            {
                bSuccess = false;
                ExceptionManager.Publish(ex);
            }
            return bSuccess;
        }

        public bool ImportZones(string strResult)
        {
            XmlDocument oXmlDoc;
            XmlNodeList oXmlNodelist;
            int iRecordInserted = 0;
            int i = 0;
            bool bSuccess = false;
            try
            {
                oXmlDoc = new XmlDocument();
                oXmlDoc.LoadXml(strResult);
                oXmlNodelist = oXmlDoc.DocumentElement.GetElementsByTagName("Zone");
                LogManager.WriteLog("Storing records in Zone table", LogManager.enumLogLevel.Info);
                while (i < oXmlNodelist.Count)
                {
                    iRecordInserted = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SPIMPORTZONES,
                        AddParameter<string>(DBConstants.SP_PARAM_IMPORTZONES, SqlDbType.VarChar, oXmlNodelist[i].OuterXml.ToString(), 8000));
                    if (iRecordInserted > 0)
                    {
                        LogManager.WriteLog("RecordInserted in Zone table: " + iRecordInserted.ToString(), LogManager.enumLogLevel.Debug);
                    }
                    i++;
                }
                LogManager.WriteLog("xmllist count in Zone table:  " + oXmlNodelist.Count.ToString(), LogManager.enumLogLevel.Debug);
                LogManager.WriteLog("Importing Zone completed.", LogManager.enumLogLevel.Info);
                bSuccess = true;
            }


            catch (Exception ex)
            {
                bSuccess = false;
                ExceptionManager.Publish(ex);
            }
            return bSuccess;
        }

        public bool ImportInstallations(string strResult)
        {
            XmlDocument oXmlDoc;
            XmlNodeList oXmlNodelist;
            int iRecordInserted = 0;
            int i = 0;
            bool bSuccess;
            try
            {
                oXmlDoc = new XmlDocument();
                oXmlDoc.LoadXml(strResult);
                oXmlNodelist = oXmlDoc.DocumentElement.GetElementsByTagName("Installation");
                LogManager.WriteLog("Storing records in Installation table", LogManager.enumLogLevel.Info);
                while (i < oXmlNodelist.Count)
                {
                    iRecordInserted = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SPIMPORTINSTALLATIONS,
                            AddParameter<string>(DBConstants.SP_PARAM_IMPORTINSTALLATION, SqlDbType.VarChar, oXmlNodelist[i].OuterXml.ToString(), 8000));
                    if (iRecordInserted > 0)
                    {
                        LogManager.WriteLog("RecordInserted in Installation table: " + iRecordInserted.ToString(), LogManager.enumLogLevel.Debug);
                    }
                    i++;
                }
                LogManager.WriteLog("xmllist count in Installation table:  " + oXmlNodelist.Count.ToString(), LogManager.enumLogLevel.Debug);
                LogManager.WriteLog("Importing Installations completed.", LogManager.enumLogLevel.Info);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }

        public bool ImportMachines(string strResult)
        {
            XmlDocument oXmlDoc;
            XmlNodeList oXmlNodelist;
            int iRecordInserted = 0;
            int i = 0;
            bool bSuccess;
            try
            {
                oXmlDoc = new XmlDocument();
                oXmlDoc.LoadXml(strResult);
                oXmlNodelist = oXmlDoc.DocumentElement.GetElementsByTagName("Machine");
                // Store in DB
                LogManager.WriteLog("Storing records in Machine table", LogManager.enumLogLevel.Info);
                while (i < oXmlNodelist.Count)
                {
                    iRecordInserted = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SPIMPORTMACHINES,
                        AddParameter<string>(DBConstants.SP_PARAM_IMPORTMACHINES, SqlDbType.VarChar, oXmlNodelist[i].OuterXml.ToString(), 8000));
                    if (iRecordInserted > 0)
                    {
                        // bSuccess = true;
                        LogManager.WriteLog("RecordInserted in Machine table: " + iRecordInserted.ToString(), LogManager.enumLogLevel.Debug);
                    }
                    i++;
                }
                LogManager.WriteLog("xmllist count in Machine table:  " + oXmlNodelist.Count.ToString(), LogManager.enumLogLevel.Debug);
                LogManager.WriteLog("Importing Machine completed.", LogManager.enumLogLevel.Info);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                bSuccess = false;
                ExceptionManager.Publish(ex);
            }
            return bSuccess;
        }

        public bool ImportBarPositions(string strResult)
        {
            XmlDocument oXmlDoc;
            XmlNodeList oXmlNodelist;
            int iRecordInserted = 0;
            int i = 0;
            bool bSuccess = false;
            try
            {
                oXmlDoc = new XmlDocument();
                oXmlDoc.LoadXml(strResult);
                oXmlNodelist = oXmlDoc.DocumentElement.GetElementsByTagName("Bar_Position");
                LogManager.WriteLog("Storing records in Bar_Position table", LogManager.enumLogLevel.Info);
                while (i < oXmlNodelist.Count)
                {
                    iRecordInserted = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SPIMPORTBARPOSITIONS,
                        AddParameter<string>(DBConstants.SP_PARAM_IMPORTBARPOSITIONS, SqlDbType.VarChar, oXmlNodelist[i].OuterXml.ToString(), 8000));
                    if (iRecordInserted > 0)
                    {
                        LogManager.WriteLog("RecordInserted in Bar_Position table: " + iRecordInserted.ToString(), LogManager.enumLogLevel.Debug);
                    }
                    i++;
                }
                LogManager.WriteLog("xmllist count in Bar_Position table:  " + oXmlNodelist.Count.ToString(), LogManager.enumLogLevel.Debug);
                LogManager.WriteLog("Importing Bar_Position completed.", LogManager.enumLogLevel.Info);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                bSuccess = false;
                ExceptionManager.Publish(ex);
            }
            return bSuccess;
        }

        public string CurrentInstallationDetails()
        {
            return SqlHelper.ExecuteScalar(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONSTANT_RSP_GETINSTALLATIONDETAILSXML).ToString();
        }

        public bool ImportLatestMeterHistory(DataTable dtLatesMeterHistory)
        {
            SqlDataAdapter daMeterHistory = null;
            SqlCommandBuilder commandbuilder = null;
            DataSet dsMeterHistory;
            DataRow newDataRow = null;
            bool bSuccess = false;
            int iRecordUpdated = -1;
            try
            {
                daMeterHistory = new SqlDataAdapter("Select top 1 * from Meter_History", CommonDataAccess.ExchangeConnectionString);
                commandbuilder = new SqlCommandBuilder(daMeterHistory);
                dsMeterHistory = new DataSet();
                daMeterHistory.Fill(dsMeterHistory);
                if (dtLatesMeterHistory != null)
                {
                    if (dtLatesMeterHistory.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtLatesMeterHistory.Rows)
                        {
                            newDataRow = dsMeterHistory.Tables[0].NewRow();

                            for (int i = 0; i < dsMeterHistory.Tables[0].Columns.Count; i++)
                            {
                                for (int j = 0; j < dr.Table.Columns.Count; j++)
                                {
                                    if (newDataRow.Table.Columns[i].ColumnName == dr.Table.Columns[j].ColumnName)
                                    {
                                        newDataRow[i] = dr[j];
                                        break;
                                    }
                                }
                            }

                            dsMeterHistory.Tables[0].Rows.Add(newDataRow);
                        }
                        daMeterHistory.UpdateCommand = commandbuilder.GetUpdateCommand();
                        daMeterHistory.Update(dsMeterHistory);
                        commandbuilder = null;
                        LogManager.WriteLog("MeterHistory record count: " + dtLatesMeterHistory.Rows.Count.ToString(), LogManager.enumLogLevel.Info);
                        iRecordUpdated = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SP_UPDATEMETERHISTORYAFTERRECOVERY);
                        if (iRecordUpdated > 0)
                            LogManager.WriteLog("Installation nos updated in MeterHistory table: " + iRecordUpdated.ToString(), LogManager.enumLogLevel.Debug);
                        else
                            LogManager.WriteLog("Sorry!!Installation nos could not updated in MeterHistory table: " + iRecordUpdated.ToString(), LogManager.enumLogLevel.Debug);

                    }
                }
                LogManager.WriteLog("Importing MeterHistory completed.", LogManager.enumLogLevel.Info);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                bSuccess = false;
                ExceptionManager.Publish(ex);
            }
            return bSuccess;
        }

        //public bool ReseedCollectionBatch(int iBatchId)
        //{
        //    int iReturnValue = 0;
        //    SqlConnection SqlConn = null;
        //    ServerConnection svrConnServerConnection = null;
        //    Server svrServer = null;
        //    object objResult = null;
        //    bool bReturn = false;
        //    string strScriptToRun = string.Empty;
        //    try
        //    {                  
        //        iBatchId = iBatchId + 1;
        //        strScriptToRun = "USE Exchange  DBCC CHECKIDENT ('collection_batch', RESEED," + iBatchId.ToString() + ")";                  
        //        SqlConn = new SqlConnection(CommonDataAccess.ExchangeConnectionString);
        //        svrConnServerConnection = new ServerConnection(SqlConn);
        //        svrServer = new Server(svrConnServerConnection);
        //        svrConnServerConnection = svrServer.ConnectionContext;
        //        objResult = svrConnServerConnection.ExecuteNonQuery(strScriptToRun);
        //        if (objResult != null)
        //        {
        //            iReturnValue = int.Parse(objResult.ToString());
        //        }                 
        //        bReturn = (iReturnValue != 0) ? true : false;
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        bReturn = false;                  
        //        ExceptionManager.Publish(sqlEx);
        //    }
        //    catch (Exception Ex)
        //    {
        //        bReturn = false;                  
        //        ExceptionManager.Publish(Ex);
        //    }
        //    finally
        //    {
        //        SqlConn.Close();
        //    }
        //    return bReturn;
        //}

        public bool UpdateSiteStatus(int iSiteCode, string sUpdate)
        {
            int iRecordUpdated = 0;
            bool bSuccess = false;
            try
            {
                //iRecordUpdated = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.Text, 
                //    "UPDATE Site SET SiteStatus='"+sUpdate+"'  WHERE Code= " +iSiteCode);    
                iRecordUpdated = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SP_UPDATESITESTATUS,
                  AddParameter<string>(DBConstants.SP_PARAM_UPDATESITESTATUS_UPDATE, SqlDbType.VarChar, sUpdate),
                  AddParameter<string>(DBConstants.SP_PARAM_UPDATESITESTATUS_SITECODE, SqlDbType.VarChar, iSiteCode.ToString()));

                bSuccess = (iRecordUpdated > 0) ? true : false;
            }
            catch (Exception Ex)
            {
                bSuccess = false;
                ExceptionManager.Publish(Ex);
            }
            return bSuccess;
        }

        public bool UpdateCheckPoints(int iSiteCode, int Value, string sTableName)
        {
            int iRecordUpdated = 0;
            bool bSuccess = false;
            try
            {
                //iRecordUpdated = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.Text,
                //    "UPDATE [ImportCheckPoints] SET [ImportStatus]= " + Value + " , [SiteCode]=' " + iSiteCode + "' , [ModifiedDate]=' " + DateTime.Now.GetUniversalDateTimeFormat() + "' WHERE TableNames='" + sTableName + "'");
                iRecordUpdated = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SP_UPDATECHECKPOINTS,
               AddParameter<int>(DBConstants.SP_PARAM_UPDATECHECKPOINTS_VALUE, SqlDbType.Int, Value),
               AddParameter<string>(DBConstants.SP_PARAM_UPDATECHECKPOINTS_SITECODE, SqlDbType.VarChar, iSiteCode.ToString()),
               AddParameter<string>(DBConstants.SP_PARAM_UPDATECHECKPOINTS_TABLENAME, SqlDbType.VarChar, sTableName));

                bSuccess = (iRecordUpdated > 0) ? true : false;
            }
            catch (Exception Ex)
            {
                bSuccess = false;
                ExceptionManager.Publish(Ex);
            }
            return bSuccess;
        }

        public bool UpdateAllCheckPoints(int Value)
        {
            int iRecordUpdated = 0;
            bool bSuccess = false;
            try
            {
                //iRecordUpdated = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.Text,
                //    "UPDATE [ImportCheckPoints] SET [ImportStatus]= " + Value + " , [ModifiedDate]=' " + DateTime.Now.GetUniversalDateTimeFormat()+"'" );
                iRecordUpdated = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SP_UPDATECHECKPOINTS,
                AddParameter<int>(DBConstants.SP_PARAM_UPDATECHECKPOINTS_VALUE, SqlDbType.Int, Value),
                AddParameter<string>(DBConstants.SP_PARAM_UPDATECHECKPOINTS_SITECODE, SqlDbType.VarChar, string.Empty),
                AddParameter<string>(DBConstants.SP_PARAM_UPDATECHECKPOINTS_TABLENAME, SqlDbType.VarChar, string.Empty));

                bSuccess = (iRecordUpdated > 0) ? true : false;
            }
            catch (Exception Ex)
            {
                bSuccess = false;
                ExceptionManager.Publish(Ex);
            }
            return bSuccess;
        }

        public bool FlattenSystem()
        {
            bool bSuccess = false;
            try
            {
                SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SP_USP_CLEAREXCHANGESERVERDATA);
                bSuccess = true; ;
            }
            catch (Exception Ex)
            {
                bSuccess = false;
                ExceptionManager.Publish(Ex);
            }
            return bSuccess;
        }

        public Dictionary<int, string> GetCheckPointsStatus(byte iStatus)
        {
            DataTable dtReturnCheckPoints = null;
            Dictionary<int, string> dictReturnCheckPoints = new Dictionary<int, string>();
            try
            {
                dtReturnCheckPoints = SqlHelper.ExecuteDataset(CommonDataAccess.ExchangeConnectionString, CommandType.Text,
                    "select ImportCheckPointsid,tablenames  from ImportCheckPoints where importstatus <>" + iStatus.ToString() + " Order by ImportCheckPointsid").Tables[0];
                if (dtReturnCheckPoints != null)
                {
                    if (dtReturnCheckPoints.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtReturnCheckPoints.Rows)
                        {
                            dictReturnCheckPoints.Add(Convert.ToByte(dr[0]), dr[1].ToString());
                        }
                    }
                }

            }
            catch (Exception Ex)
            {
                dictReturnCheckPoints = null;
                ExceptionManager.Publish(Ex);
            }
            return dictReturnCheckPoints;
        }

        public DataTable GetTableDetails()
        {
            try
            {
                return SqlHelper.ExecuteDataset(CommonDataAccess.ExchangeConnectionString, CommandType.Text,
                       "SELECT TableNames,ImportStatus FROM [ImportCheckPoints]").Tables[0];
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return new DataTable();
            }
        }

        public bool ImportTicketExceptionDetails(DataTable dtTicketException)
        {
            bool bSuccess = false;
            string strXml = string.Empty;
            int iRecordUpdated = -1;
            try
            {

                strXml = ConvertDataTableToXML(dtTicketException, "TICKETEXCEPTIONROOT", "TICKETEXCEPTION");
                strXml.Replace("\r\n", String.Empty);
                LogManager.WriteLog("Ticket_Exception record count: " + dtTicketException.Rows.Count.ToString(), LogManager.enumLogLevel.Info);
                if (strXml.Length > 0)
                {
                    iRecordUpdated = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SP_USP_INSERTTICKETEXCEPTIONFROMXML,
                        AddParameter<string>(DBConstants.SP_PARAM1_INSERTTICKETEXCEPTIONFROMXML, SqlDbType.Xml, strXml),
                        AddOutputParameter<string>(DBConstants.SP_PARAM2_INSERTTICKETEXCEPTIONFROMXML, SqlDbType.VarChar, string.Empty));
                    if (iRecordUpdated > 0)
                    {
                        LogManager.WriteLog("inserted to TICKETEXCEPTION table: " + iRecordUpdated.ToString(), LogManager.enumLogLevel.Debug);
                        iRecordUpdated = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SP_UPDATETICKETEXCEPTIONAFTERRECOVERY);
                        if (iRecordUpdated > 0)
                        {
                            LogManager.WriteLog("Installation nos updated in ticket_exception table: " + iRecordUpdated.ToString(), LogManager.enumLogLevel.Debug);
                            LogManager.WriteLog("Importing Ticket_Exception completed.", LogManager.enumLogLevel.Info);
                            bSuccess = true;
                        }
                        else
                        {
                            LogManager.WriteLog("Sorry!!Installation nos could not updated in ticket_exception table: " + iRecordUpdated.ToString(), LogManager.enumLogLevel.Debug);
                            bSuccess = false;
                        }
                    }

                    else
                    {
                        LogManager.WriteLog("Sorry!!insert failed in TICKETEXCEPTION table: " + iRecordUpdated.ToString(), LogManager.enumLogLevel.Debug);
                        bSuccess = false;
                    }
                }

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    bSuccess = true;
                    ExceptionManager.Publish(ex);
                }
                else
                {
                    bSuccess = false;
                    ExceptionManager.Publish(ex);
                }
            }
            catch (Exception ex)
            {
                bSuccess = false;
                ExceptionManager.Publish(ex);
            }
            return bSuccess;
        }

        //public bool ImportVoucherDetails(DataTable dtVoucher)
        //{
        //   string iTicketSiteId = string.Empty;

        //    bool bSuccess = false;
        //    string strXml = string.Empty;
        //    int iRecordUpdated = -1;
        //    try
        //    {
        //        strXml = ConvertDataTableToXMLDate(dtVoucher, "VOUCHERROOT", "VOUCHER", "dtPrinted");
        //        strXml.Replace("\r\n", String.Empty);
        //        LogManager.WriteLog("Workstation record count: " + dtVoucher.Rows.Count.ToString(), LogManager.enumLogLevel.Info);
        //        if (strXml.Length > 0)
        //        {
        //            iRecordUpdated = SqlHelper.ExecuteNonQuery(CommonDataAccess.TicketingConnectionString, CommandType.StoredProcedure, DBConstants.SP_USP_INSERTVOUCHERFROMXML,
        //                AddParameter<string>(DBConstants.SP_PARAM1_INSERTVOUCHERFROMXML, SqlDbType.Xml, strXml),
        //                AddOutputParameter<string>(DBConstants.SP_PARAM2_INSERTVOUCHERFROMXML, SqlDbType.VarChar, string.Empty));
        //            if (iRecordUpdated > 0)
        //            {
        //                LogManager.WriteLog("inserted to VOUCHER table: " + iRecordUpdated.ToString(), LogManager.enumLogLevel.Debug);
        //                iTicketSiteId = (SqlHelper.ExecuteScalar(CommonDataAccess.TicketingConnectionString, CommandType.Text,
        //                 "Select iSiteID from Site") != null) ? SqlHelper.ExecuteScalar(CommonDataAccess.TicketingConnectionString, CommandType.Text,
        //                 "Select iSiteID from Site").ToString() : string.Empty;
        //                if (!string.IsNullOrEmpty(iTicketSiteId))
        //                {
        //                    //iRecordUpdated = SqlHelper.ExecuteNonQuery(CommonDataAccess.TicketingConnectionString, CommandType.Text,
        //                    //  "UPDATE Voucher SET iSiteID='" + iTicketSiteId + "'");

        //                    iRecordUpdated = SqlHelper.ExecuteNonQuery(CommonDataAccess.TicketingConnectionString, CommandType.StoredProcedure, DBConstants.SP_UPDATEVOUCHER,
        //                    AddParameter<int>(DBConstants.SP_PARAM_UPDATEVOUCHER_TICKETSITEID, SqlDbType.Int, Convert.ToInt32(iTicketSiteId)));

        //                    bSuccess = (iRecordUpdated > 0) ? true : false;
        //                }

        //                LogManager.WriteLog("Voucher Details record count: " + dtVoucher.Rows.Count.ToString(), LogManager.enumLogLevel.Info);
        //                LogManager.WriteLog("Importing Voucher Details completed.", LogManager.enumLogLevel.Info);
        //                bSuccess = true;
        //            }
        //            else
        //            {
        //                LogManager.WriteLog("Sorry!!insert failed in VOUCHER table: " + iRecordUpdated.ToString(), LogManager.enumLogLevel.Debug);
        //                bSuccess = false;
        //            }
        //        }

        //    }
        //    catch (System.Data.SqlClient.SqlException ex)
        //    {
        //        if (ex.Number == 2627)
        //        {
        //            bSuccess = true;
        //            ExceptionManager.Publish(ex);
        //        }
        //        else
        //        {
        //            bSuccess = false;
        //            ExceptionManager.Publish(ex);
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        bSuccess = false;
        //        ExceptionManager.Publish(ex);
        //    }
        //    return bSuccess;
        //}
        public bool ImportVoucherDetails(string sVoucher)
        {

            //int iRecordInserted = 0;
            int iRecordUpdated = -1;
            bool bSuccess = false;
            string iTicketSiteId = string.Empty;

            try
            {
                // Store in DB
                LogManager.WriteLog("Voucher table started: " + sVoucher.Length.ToString(), LogManager.enumLogLevel.Info);

                iRecordUpdated = SqlHelper.ExecuteNonQuery(CommonDataAccess.TicketingConnectionString, CommandType.StoredProcedure, DBConstants.SP_USP_INSERTVOUCHERFROMXML,
                    AddParameter<string>(DBConstants.SP_PARAM1_INSERTVOUCHERFROMXML, SqlDbType.Xml, sVoucher),
                    AddOutputParameter<string>(DBConstants.SP_PARAM2_INSERTVOUCHERFROMXML, SqlDbType.VarChar, string.Empty));

                if (iRecordUpdated > 0)
                {
                    LogManager.WriteLog("inserted to VOUCHER table: " + iRecordUpdated.ToString(), LogManager.enumLogLevel.Debug);
                    iTicketSiteId = (SqlHelper.ExecuteScalar(CommonDataAccess.TicketingConnectionString, CommandType.Text,
                     "Select iSiteID from Site") != null) ? SqlHelper.ExecuteScalar(CommonDataAccess.TicketingConnectionString, CommandType.Text,
                     "Select iSiteID from Site").ToString() : string.Empty;
                    if (!string.IsNullOrEmpty(iTicketSiteId))
                    {
                        iRecordUpdated = SqlHelper.ExecuteNonQuery(CommonDataAccess.TicketingConnectionString, CommandType.StoredProcedure, DBConstants.SP_UPDATEVOUCHER,
                        AddParameter<int>(DBConstants.SP_PARAM_UPDATEVOUCHER_TICKETSITEID, SqlDbType.Int, Convert.ToInt32(iTicketSiteId)));

                        bSuccess = (iRecordUpdated > 0) ? true : false;
                    }
                    LogManager.WriteLog("Updated with isiteid done.", LogManager.enumLogLevel.Info);
                    bSuccess = true;
                }
                else
                {
                    LogManager.WriteLog("Sorry!!insert failed in VOUCHER table: " + iRecordUpdated.ToString(), LogManager.enumLogLevel.Debug);
                    bSuccess = false;
                }
                LogManager.WriteLog("Importing Voucher Details completed.", LogManager.enumLogLevel.Info);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    bSuccess = true;
                    ExceptionManager.Publish(ex);
                }
                else
                {
                    bSuccess = false;
                    ExceptionManager.Publish(ex);
                }
            }

            catch (Exception ex)
            {
                bSuccess = false;
                ExceptionManager.Publish(ex);
            }
            return bSuccess;

        }

        public bool ImportDeviceDetails(DataTable dtDevice)
        {
            bool bSuccess = false;
            int iRecordUpdated = -1;
            string strXml = string.Empty;
            try
            {
                strXml = ConvertDataTableToXML(dtDevice, "DEVICEROOT", "DEVICE");
                strXml.Replace("\r\n", String.Empty);
                LogManager.WriteLog("Device record count: " + dtDevice.Rows.Count.ToString(), LogManager.enumLogLevel.Info);
                if (strXml.Length > 0)
                {
                    iRecordUpdated = SqlHelper.ExecuteNonQuery(CommonDataAccess.TicketingConnectionString, CommandType.StoredProcedure, DBConstants.SP_USP_INSERTDEVICEDETAILSFROMXML,
                        AddParameter<string>(DBConstants.SP_PARAM_INSERTDEVICEDETAILSFROMXML, SqlDbType.Xml, strXml),
                        AddOutputParameter<string>(DBConstants.SP_PARAM_ISSUCCESS_INSERTDEVICEDETAILSFROMXML, SqlDbType.VarChar, string.Empty));
                    if (iRecordUpdated > 0)
                        LogManager.WriteLog("Device nos updated in Device table: " + iRecordUpdated.ToString(), LogManager.enumLogLevel.Debug);
                    else
                        LogManager.WriteLog("Sorry!!Device nos could not updated in Device table: " + iRecordUpdated.ToString(), LogManager.enumLogLevel.Debug);

                    LogManager.WriteLog("Importing Device completed.", LogManager.enumLogLevel.Info);
                    bSuccess = true;
                }

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    bSuccess = true;
                    ExceptionManager.Publish(ex);
                }
                else
                {
                    bSuccess = false;
                    ExceptionManager.Publish(ex);
                }
            }
            catch (Exception ex)
            {
                bSuccess = false;
                ExceptionManager.Publish(ex);
            }
            return bSuccess;
        }

        public bool ImportWorkStationDetails(DataTable dtWorkstation)
        {
            bool bSuccess = false;
            string strXml = string.Empty;
            int iRecordUpdated = -1;
            try
            {
                strXml = ConvertDataTableToXML(dtWorkstation, "WORKSTATIONROOT", "WORKSTATION");
                strXml.Replace("\r\n", String.Empty);
                LogManager.WriteLog("Workstation record count: " + dtWorkstation.Rows.Count.ToString(), LogManager.enumLogLevel.Info);
                if (strXml.Length > 0)
                {
                    iRecordUpdated = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SP_USP_INSERTWORKSTATIONFROMXML,
                        AddParameter<string>(DBConstants.SP_PARAM1_INSERTWORKSTATIONFROMXML, SqlDbType.Xml, strXml),
                        AddOutputParameter<string>(DBConstants.SP_PARAM2_INSERTWORKSTATIONFROMXML, SqlDbType.VarChar, string.Empty));
                    if (iRecordUpdated > 0)
                        LogManager.WriteLog("inserted to WORKSTATION table: " + iRecordUpdated.ToString(), LogManager.enumLogLevel.Debug);
                    else
                        LogManager.WriteLog("Sorry!!insert failed in WORKSTATION table: " + iRecordUpdated.ToString(), LogManager.enumLogLevel.Debug);


                    LogManager.WriteLog("Importing WORKSTATION completed.", LogManager.enumLogLevel.Info);
                    bSuccess = true;
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    bSuccess = true;
                    ExceptionManager.Publish(ex);
                }
                else
                {
                    bSuccess = false;
                    ExceptionManager.Publish(ex);
                }
            }
            catch (Exception ex)
            {
                bSuccess = false;
                ExceptionManager.Publish(ex);
            }
            return bSuccess;
        }

        //public bool ImportHourly(DataTable dtHourly)
        //{             
        //    bool bSuccess = false;
        //    int iRecordUpdated = -1;
        //    string strXml = string.Empty;
        //    try
        //    {
        //        strXml = ConvertDataTableToXMLDate(dtHourly, "HOURLYROOT", "HOURLY", "HS_Date");
        //        strXml.Replace("\r\n", String.Empty);                  
        //        LogManager.WriteLog("Hourly record count: " + dtHourly.Rows.Count.ToString(), LogManager.enumLogLevel.Info);
        //        if (strXml.Length>0)
        //        {
        //            iRecordUpdated = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SP_USP_UPDATEHOURLYSTATISTICSAFTERRECOVERY,
        //                AddParameter<string>(DBConstants.SP_PARAM_UPDATEHOURLYSTATISTICS_XMLDOC, SqlDbType.Xml, strXml));
        //            if (iRecordUpdated > 0)
        //                LogManager.WriteLog("Installation nos updated in hourly table: " + iRecordUpdated.ToString(), LogManager.enumLogLevel.Debug);
        //            else
        //                LogManager.WriteLog("Sorry!!Installation nos could not updated in hourly table: " + iRecordUpdated.ToString(), LogManager.enumLogLevel.Debug);


        //            LogManager.WriteLog("Importing Hourly completed.", LogManager.enumLogLevel.Info);
        //            bSuccess = true;
        //            try
        //            {
        //                SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure,
        //                        DBConstants.SP_ESP_COLLATE_HS_GAMINGDAY);
        //            }
        //            catch (System.Data.SqlClient.SqlException ex)
        //            {
        //                if (ex.Number == 2627)
        //                {
        //                    bSuccess = true;
        //                    ExceptionManager.Publish(ex);
        //                }
        //                else
        //                {
        //                    bSuccess = false;
        //                    ExceptionManager.Publish(ex);
        //                }
        //            }
        //            catch (Exception Ex)
        //            {
        //                ExceptionManager.Publish(Ex);
        //            } 
        //        }
        //    }
        //    catch (System.Data.SqlClient.SqlException ex)
        //    {
        //        if (ex.Number == 2627)
        //        {
        //            bSuccess = true;
        //            ExceptionManager.Publish(ex);
        //        }
        //        else
        //        {
        //            bSuccess = false;
        //            ExceptionManager.Publish(ex);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        bSuccess = false;
        //        ExceptionManager.Publish(ex);
        //    }
        //    return bSuccess;
        //}

        public bool ImportHourly(string sHourly)
        {
            //int iRecordInserted = 0;
            int iRecordUpdated = -1;
            bool bSuccess = false;
            string iTicketSiteId = string.Empty;

            try
            {
                // Store in DB
                LogManager.WriteLog("Hourly record count: " + sHourly.ToString(), LogManager.enumLogLevel.Info);

                iRecordUpdated = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SP_USP_UPDATEHOURLYSTATISTICSAFTERRECOVERY,
                    AddParameter<string>(DBConstants.SP_PARAM_UPDATEHOURLYSTATISTICS_XMLDOC, SqlDbType.Xml, sHourly));

                if (iRecordUpdated > 0)
                    LogManager.WriteLog("Installation nos updated in hourly table: " + iRecordUpdated.ToString(), LogManager.enumLogLevel.Debug);
                else
                    LogManager.WriteLog("Sorry!!Installation nos could not updated in hourly table: " + iRecordUpdated.ToString(), LogManager.enumLogLevel.Debug);


                LogManager.WriteLog("Importing Hourly completed.", LogManager.enumLogLevel.Info);
                bSuccess = true;
                try
                {
                    SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure,
                            DBConstants.SP_ESP_COLLATE_HS_GAMINGDAY);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        bSuccess = true;
                        ExceptionManager.Publish(ex);
                    }
                    else
                    {
                        bSuccess = false;
                        ExceptionManager.Publish(ex);
                    }
                }
                catch (Exception Ex)
                {
                    ExceptionManager.Publish(Ex);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    bSuccess = true;
                    ExceptionManager.Publish(ex);
                }
                else
                {
                    bSuccess = false;
                    ExceptionManager.Publish(ex);
                }
            }
            catch (Exception ex)
            {
                bSuccess = false;
                ExceptionManager.Publish(ex);
            }
            return bSuccess;
        }

        public bool ImportDaily(DataTable dtDaily)
        {
            SqlDataAdapter daDaily = null;
            SqlCommandBuilder commandbuilder = null;
            DataSet dsDaily;
            DataRow newDataRow = null;
            bool bSuccess = false;
            int iRecordUpdated = -1;
            try
            {
                daDaily = new SqlDataAdapter("Select top 1 * from [Read]", CommonDataAccess.ExchangeConnectionString);
                commandbuilder = new SqlCommandBuilder(daDaily);
                dsDaily = new DataSet();
                daDaily.Fill(dsDaily);
                if (dtDaily != null)
                {
                    if (dtDaily.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtDaily.Rows)
                        {
                            newDataRow = dsDaily.Tables[0].NewRow();

                            for (int i = 0; i < dsDaily.Tables[0].Columns.Count; i++)
                            {
                                for (int j = 0; j < dr.Table.Columns.Count; j++)
                                {
                                    if (newDataRow.Table.Columns[i].ColumnName == dr.Table.Columns[j].ColumnName)
                                    {
                                        newDataRow[i] = dr[j];
                                        break;
                                    }
                                }
                            }

                            dsDaily.Tables[0].Rows.Add(newDataRow);
                        }
                        daDaily.UpdateCommand = commandbuilder.GetUpdateCommand();
                        daDaily.Update(dsDaily);
                        commandbuilder = null;
                        LogManager.WriteLog("Daily record count: " + dtDaily.Rows.Count.ToString(), LogManager.enumLogLevel.Info);
                        iRecordUpdated = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SP_USP_UPDATEREADAFTERRECOVERY);
                        if (iRecordUpdated > 0)
                            LogManager.WriteLog("Installation nos updated in read table: " + iRecordUpdated.ToString(), LogManager.enumLogLevel.Debug);
                        else
                            LogManager.WriteLog("Sorry!!Installation nos could not updated in read table: " + iRecordUpdated.ToString(), LogManager.enumLogLevel.Debug);

                    }
                }

                LogManager.WriteLog("Importing Daily completed.", LogManager.enumLogLevel.Info);
                bSuccess = true;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    bSuccess = true;
                    ExceptionManager.Publish(ex);
                }
                else
                {
                    bSuccess = false;
                    ExceptionManager.Publish(ex);
                }
            }
            catch (Exception ex)
            {
                bSuccess = false;
                ExceptionManager.Publish(ex);
            }
            return bSuccess;
        }

        public bool ImportAllEvents(string strResult)
        {
            string strSPName = string.Empty;
            bool bSuccess = false;
            int iRecordInserted = 0;
            try
            {

                iRecordInserted = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SP_IMPORTSITEALLEVENTS,
                     AddParameter<string>(DBConstants.SP_PARAM_IMPORTSITEALLEVENTS_SITECODE, SqlDbType.Xml, strResult),
                      AddOutputParameter<int>(DBConstants.SP_PARAM_IMPORTSITEALLEVENTS_XDays, SqlDbType.Int, 0));

                if (iRecordInserted > 0)
                {
                    if (int.Parse(AddOutputParameter<int>(DBConstants.SP_PARAM_IMPORTSITEALLEVENTS_XDays, SqlDbType.Int, 0).Value.ToString()) == 0)
                    {
                        bSuccess = true;
                        LogManager.WriteLog("Record Inserted in Events table: " + AddOutputParameter<int>(DBConstants.SP_PARAM_IMPORTSITEALLEVENTS_XDays, SqlDbType.Int, 0).Value.ToString(), LogManager.enumLogLevel.Debug);
                    }
                }
                else { bSuccess = false; }

                LogManager.WriteLog("Importing Events completed.", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                bSuccess = false;
                ExceptionManager.Publish(ex);
            }
            return bSuccess;
        }

        public bool ImportSystemSettings(string strResult)
        {
            string strSPName = string.Empty;
            bool bSuccess = true;
            int iRecordInserted = 0;
            string tktConnectionString = string.Empty;
            try
            {
                tktConnectionString = GetTicketingConnectionString();

                iRecordInserted = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SPIMPORTSITESETTINGS,
                      AddParameter<string>(DBConstants.SP_PARAM_IMPORTSITESETTINGS, SqlDbType.Xml, strResult));
                if (iRecordInserted > 0)
                {
                    try
                    {
                        tktConnectionString = CommonDataAccess.ExchangeConnectionString.Replace("Exchange", "Ticketing");
                        BMC.DBInterface.CashDeskOperator.LinqDataAccessDataContext oLinqDataAccessDataContext = new LinqDataAccessDataContext(CommonDataAccess.ExchangeConnectionString);
                        var olist = oLinqDataAccessDataContext.EditSetting(0, DBConstants.TicketConnectSettingName, tktConnectionString, "Voucher database connection setting").ToList();
                        if (olist.FirstOrDefault().Setting_Value.ToLower() == tktConnectionString.ToLower())
                            LogManager.WriteLog("Record Inserted in Site Settings table: " + iRecordInserted.ToString(), LogManager.enumLogLevel.Debug);
                        else
                            LogManager.WriteLog("Record Inserted in Site Settings table but please check the Voucher connection from enterprise: " + olist.FirstOrDefault().Setting_Value.ToLower(), LogManager.enumLogLevel.Debug);
                        bSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        LogManager.WriteLog("local tkt connection string: " + tktConnectionString, LogManager.enumLogLevel.Debug);
                        ExceptionManager.Publish(ex);
                        bSuccess = true;
                    }
                }
                else { bSuccess = false; }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }

        public bool ImportLookupMaster(string strResult)
        {
            bool bSuccess;
            int iRecordInserted;
            try
            {

                iRecordInserted = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "usp_ImportNewLookupMaster",
                      AddParameter<string>(DBConstants.SP_PARAM_IMPORTSITESETTINGS, SqlDbType.Xml, strResult));
                if (iRecordInserted > 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("Record Inserted in Category Reason table: " + iRecordInserted.ToString(), LogManager.enumLogLevel.Debug);
                }
                else { bSuccess = false; }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }

        public bool ImportCashDeskTransactions(string strCashDeskTransactions)
        {

            bool bSuccess = false;
            try
            {
                SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "usp_UpdateTreasuryFromXML",
                   AddParameter<string>(DBConstants.SP_PARAM_IMPORTSITESETTINGS, SqlDbType.Xml, strCashDeskTransactions));
                bSuccess = true;
            }
            catch (Exception ex)
            {
                bSuccess = false;
                ExceptionManager.Publish(ex);
            }
            return bSuccess;
        }

        public bool UpdateTreasuryRelDataForSiteConfig()
        {
            // update the installation and treasury reason code
            int iRecordInserted = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString,
                CommandType.StoredProcedure, "usp_UpdateTreasuryRelDataForSiteConfig");
            return (iRecordInserted > 0);
        }

        public bool ImportCollectionBatch(string CollectionXML)
        {
            bool bSuccess = false;
            int iRecordInserted = 0;

            bSuccess = true;
            try
            {
                iRecordInserted = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONSTANT_USP_INSERTCOLLECTIONFROMXML,
                      AddParameter<string>(DBConstants.SP_PARAM_USP_INSERTCOLLECTIONFROMXML_DOC, SqlDbType.VarChar, CollectionXML),
                      AddOutputParameter<string>(DBConstants.SP_PARAM_USP_INSERTCOLLECTIONFROMXML_SUCCESS, SqlDbType.VarChar, string.Empty));
                if (iRecordInserted > 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("Record Inserted in Collection table: " + iRecordInserted.ToString(), LogManager.enumLogLevel.Debug);
                }
                else { bSuccess = false; }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }

            return bSuccess;
        }


        public bool ImportAFTTransactions(string AFTTransactionsXML)
        {
            bool bSuccess = false;
            int iRecordInserted = 0;

            bSuccess = true;
            try
            {
                iRecordInserted = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONSTANT_USP_INSERTAFTTRANSACTIONSFROMXML,
                      AddParameter<string>(DBConstants.SP_PARAM_USP_INSERTAFTTRANSACTIONSFROMXML_DOC, SqlDbType.VarChar, AFTTransactionsXML));
                if (iRecordInserted > 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("Record Inserted in AFT_Transactions table: " + iRecordInserted.ToString(), LogManager.enumLogLevel.Debug);
                }
                else { bSuccess = false; }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }

            return bSuccess;
        }



        public bool ImportAuditHistory(string AuditHistoryXML)
        {
            bool bSuccess = false;
            int iRecordInserted = 0;

            bSuccess = true;
            try
            {
                iRecordInserted = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONSTANT_USP_INSERTAUDITHISTORYFROMXML,
                      AddParameter<string>(DBConstants.SP_PARAM_USP_INSERTAUDITHISTORYFROMXML_DOC, SqlDbType.VarChar, AuditHistoryXML));
                if (iRecordInserted > 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("Record Inserted in Audit_History table: " + iRecordInserted.ToString(), LogManager.enumLogLevel.Debug);
                }
                else { bSuccess = false; }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }

            return bSuccess;
        }

        public bool ImportTreasuryDetails(string CollectionXML)
        {
            bool bSuccess = false;
            int iRecordInserted = 0;

            bSuccess = true;
            try
            {
                iRecordInserted = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONSTANT_USP_INSERTTREASURYFROMXML,
                      AddParameter<string>(DBConstants.SP_PARAM_USP_INSERTCOLLECTIONFROMXML_DOC, SqlDbType.VarChar, CollectionXML));
                if (iRecordInserted > 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("Record Inserted in Treasury table: " + iRecordInserted.ToString(), LogManager.enumLogLevel.Debug);
                }
                else { bSuccess = false; }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }

            return bSuccess;
        }

        public bool ImportEventsDetails(string EventXML)
        {
            bool bSuccess = false;
            int iRecordInserted = 0;

            bSuccess = true;
            try
            {
                iRecordInserted = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONSTANT_USP_INSERTEVENTSFROMXML,
                      AddParameter<string>(DBConstants.SP_PARAM_USP_INSERTCOLLECTIONFROMXML_DOC, SqlDbType.VarChar, EventXML));
                if (iRecordInserted > 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("Record Inserted in Events table: " + iRecordInserted.ToString(), LogManager.enumLogLevel.Debug);
                }
                else { bSuccess = false; }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }

            return bSuccess;
        }

        public bool ImportOtherMachineDetails(string strResult)
        {
            bool bSuccess;
            try
            {
                var sqlParameters = new SqlParameter[2];
                var sqlParameter = new SqlParameter
                {
                    ParameterName = "doc",
                    Value = strResult,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;
                sqlParameter = new SqlParameter
                {
                    ParameterName = "IsSuccess",
                    Direction = ParameterDirection.Output,
                    SqlDbType = SqlDbType.Int
                };
                sqlParameters[1] = sqlParameter;

                SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "usp_ImportModelDetails", sqlParameters);

                if (int.Parse(sqlParameters[1].Value.ToString()) == 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("ImportOtherMachineDetails" + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
                }
                else
                {
                    bSuccess = false;
                    LogManager.WriteLog("ImportOtherMachineDetails" + "  failed due to " + sqlParameters[1].Value, LogManager.enumLogLevel.Info);
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }

        public bool ImportUserDetails(string strResult)
        {
            string strSPName = string.Empty;
            bool bSuccess = false;
            int iRecordInserted = 0;
            try
            {

                iRecordInserted = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SP_IMPORTSITEALLUSERDETAILS,
                     AddParameter<string>(DBConstants.SP_PARAM_IMPORTSITEALLUSERDETAILS_DOC, SqlDbType.Xml, strResult),
                      AddParameter<bool>(DBConstants.SP_PARAM_IMPORTSITEALLUSERDETAILS_ADDUSER, SqlDbType.Bit, true));
                if (iRecordInserted > 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("Record Inserted in Users table: " + iRecordInserted.ToString(), LogManager.enumLogLevel.Debug);

                }
                else { bSuccess = false; }

                LogManager.WriteLog("Importing Users completed.", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                bSuccess = false;
                ExceptionManager.Publish(ex);
            }
            return bSuccess;
        }

        public bool ImportUserRoles(string strResult)
        {
            string strSPName = string.Empty;
            bool bSuccess = false;
            int iRecordInserted = 0;
            try
            {

                iRecordInserted = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SP_IMPORTSITEALLROLESDETAILS,
                     AddParameter<string>(DBConstants.SP_PARAM_IMPORTSITEALLROLESDETAILS_DOC, SqlDbType.Xml, strResult));
                if (iRecordInserted > 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("Record Inserted in roles table: " + iRecordInserted.ToString(), LogManager.enumLogLevel.Debug);

                }
                else { bSuccess = false; }

                LogManager.WriteLog("Importing roles completed.", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                bSuccess = false;
                ExceptionManager.Publish(ex);
            }
            return bSuccess;
        }

        public bool ImportUserRolesLinks(string strResult)
        {
            string strSPName = string.Empty;
            bool bSuccess = false;
            int iRecordInserted = 0;
            try
            {

                iRecordInserted = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SP_IMPORTSITEALLUSERROLELINK,
                     AddParameter<string>(DBConstants.SP_PARAM_IMPORTSITEALLUSERROLELINK_DOC, SqlDbType.Xml, strResult));
                if (iRecordInserted > 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("Record updated in user roles table: " + iRecordInserted.ToString(), LogManager.enumLogLevel.Debug);

                }
                else { bSuccess = false; }

                LogManager.WriteLog("Importing user roles completed.", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                bSuccess = false;
                ExceptionManager.Publish(ex);
            }
            return bSuccess;
        }

        public bool ImportCalendars(string strResult)
        {
            string strSPName = string.Empty;
            bool bSuccess = false;
            int iRecordInserted = 0;
            try
            {

                iRecordInserted = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONSTANT_USP_IMPORTCALENDARS,
                     AddParameter<string>(DBConstants.SP_PARAM_IMPORTCALENDARS_DOC, SqlDbType.VarChar, strResult),
                     AddOutputParameter<int>(DBConstants.SP_PARAM_IMPORTCALENDARS_SUCCESS, SqlDbType.Int, 0));
                if (iRecordInserted > 0)
                {
                    if (int.Parse(AddOutputParameter<int>(DBConstants.SP_PARAM_IMPORTCALENDARS_SUCCESS, SqlDbType.Int, 0).Value.ToString()) == 0)
                    {
                        bSuccess = true;
                        LogManager.WriteLog("Record updated in calendar table: " + iRecordInserted.ToString(), LogManager.enumLogLevel.Debug);
                    }

                }
                else { bSuccess = false; }

                LogManager.WriteLog("Importing calendar completed.", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                bSuccess = false;
                ExceptionManager.Publish(ex);
            }
            return bSuccess;
        }

        public bool ImportAAMSDetails(string strResult)
        {
            XmlDocument oXmlDoc;
            XmlNodeList oXmlNodelist;
            int iRecordInserted = 0;
            int i = 0;
            bool bSuccess;
            try
            {
                oXmlDoc = new XmlDocument();
                oXmlDoc.LoadXml(strResult);
                oXmlNodelist = oXmlDoc.DocumentElement.GetElementsByTagName("AAMSDetail");
                LogManager.WriteLog("Storing records in AAMSDetails table", LogManager.enumLogLevel.Info);
                while (i < oXmlNodelist.Count)
                {
                    iRecordInserted = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SPIMPORTAAMSDETAILS,
                                      AddParameter<string>(DBConstants.SP_PARAM_IMPORTINSTALLATION, SqlDbType.VarChar, oXmlNodelist[i].OuterXml.ToString(), 8000));
                    if (iRecordInserted > 0)
                    {
                        LogManager.WriteLog("RecordInserted in AAMSDetails table: " + iRecordInserted.ToString(), LogManager.enumLogLevel.Debug);
                    }
                    i++;
                }
                LogManager.WriteLog("xmllist count in AAMSDetails table:  " + oXmlNodelist.Count.ToString(), LogManager.enumLogLevel.Debug);
                LogManager.WriteLog("Importing AAMSDetails completed.", LogManager.enumLogLevel.Info);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }

        public bool ImportInstallationGameInfo(string strResult)
        {
            XmlDocument oXmlDoc;
            XmlNodeList oXmlNodelist;
            int iRecordInserted = 0;
            int i = 0;
            bool bSuccess;
            try
            {
                oXmlDoc = new XmlDocument();
                oXmlDoc.LoadXml(strResult);
                oXmlNodelist = oXmlDoc.DocumentElement.GetElementsByTagName("InstallationGameInfo");
                LogManager.WriteLog("Storing records in InstallationGameInfo table", LogManager.enumLogLevel.Info);
                while (i < oXmlNodelist.Count)
                {
                    iRecordInserted = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SPIMPORTINSTALLATIONGAMEINFO,
                                      AddParameter<string>(DBConstants.SP_PARAM_IMPORTINSTALLATION, SqlDbType.VarChar, oXmlNodelist[i].OuterXml.ToString(), 8000));
                    if (iRecordInserted > 0)
                    {
                        LogManager.WriteLog("RecordInserted in InstallationGameInfo table: " + iRecordInserted.ToString(), LogManager.enumLogLevel.Debug);
                    }
                    i++;
                }
                LogManager.WriteLog("xmllist count in InstallationGameInfo table:  " + oXmlNodelist.Count.ToString(), LogManager.enumLogLevel.Debug);
                LogManager.WriteLog("Importing InstallationGameInfo completed.", LogManager.enumLogLevel.Info);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }

        public bool ImportObjects(string strResult)
        {
            string strSPName = string.Empty;
            bool bSuccess = false;
            int iRecordInserted = 0;
            try
            {

                iRecordInserted = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SP_EXCHANGEADMINOBJECTFROMXML,
                           AddParameter<string>(DBConstants.SP_PARAM1_EXCHANGEADMINOBJECTFROMXML, SqlDbType.Xml, strResult),
                      AddOutputParameter<string>(DBConstants.SP_PARAM2_EXCHANGEADMINOBJECTFROMXML, SqlDbType.VarChar, string.Empty));

                if (iRecordInserted > 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("Record Inserted in Objects table: " + iRecordInserted.ToString(), LogManager.enumLogLevel.Debug);

                }
                else { bSuccess = false; }

                LogManager.WriteLog("Importing Objects completed.", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                bSuccess = false;
                ExceptionManager.Publish(ex);
            }
            return bSuccess;
        }

        public bool ImportRoleAccessObjectRightLnk(string strResult)
        {
            string strSPName = string.Empty;
            bool bSuccess = false;
            int iRecordInserted = 0;
            try
            {

                iRecordInserted = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SP_ROLEACCESSOBJECTRIGHTLNKFROMXML,
                      AddParameter<string>(DBConstants.SP_PARAM1_ROLEACCESSOBJECTRIGHTLNKFROMXML, SqlDbType.Xml, strResult),
                     AddOutputParameter<string>(DBConstants.SP_PARAM2_ROLEACCESSOBJECTRIGHTLNKFROMXML, SqlDbType.VarChar, string.Empty));

                if (iRecordInserted > 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("Record Inserted in RoleAccessObjectRightLnk table: " + iRecordInserted.ToString(), LogManager.enumLogLevel.Debug);

                }
                else { bSuccess = false; }

                LogManager.WriteLog("Importing RoleAccessObjectRightLnk completed.", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                bSuccess = false;
                ExceptionManager.Publish(ex);
            }
            return bSuccess;
        }

        public bool ImportComponentDetails(string strResult)
        {
            int iRecordInserted = 0;
            bool bSuccess;

            try
            {
                LogManager.WriteLog("Storing records in Component Details table", LogManager.enumLogLevel.Info);

                iRecordInserted = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "usp_InsertCompDetailsfromXMLForSiteConfig",
                                      AddParameter<string>(DBConstants.SP_PARAM_IMPORTINSTALLATION, SqlDbType.VarChar, strResult, 8000));

                LogManager.WriteLog("Importing Component Details completed.", LogManager.enumLogLevel.Info);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }

        public bool ImportSeedValues(string strResult)
        {
            bool bSuccess;

            LogManager.WriteLog("ImportSeedValues - Started", LogManager.enumLogLevel.Info);

            try
            {
                foreach (string tab in strResult.Split(','))
                {
                    if (tab != null && tab.Length > 0)
                    {
                        string[] sKV = tab.Split(':');
                        if (sKV != null && sKV[0] != null && sKV[0].Length > 0 && sKV[1] != null && sKV[1].Length > 0)
                        {
                            SqlParameter[] param = new SqlParameter[2];

                            param[0] = new SqlParameter("@Table", SqlDbType.VarChar, 100);
                            param[0].Value = sKV[0];

                            param[1] = new SqlParameter("@SeedValue", SqlDbType.Int);
                            param[1].Value = Int32.Parse(sKV[1]);

                            SqlConnection objcon = new SqlConnection(CommonDataAccess.ExchangeConnectionString);
                            using (objcon)
                            {
                                objcon.Open();
                                SqlCommand cmd = new SqlCommand("usp_UpdateSeedValue", objcon);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddRange(param);
                                cmd.ExecuteNonQuery();
                                objcon.Close();
                            }
                            LogManager.WriteLog("SeedUpdated : " + tab, LogManager.enumLogLevel.Info);
                        }
                    }
                }

                LogManager.WriteLog("Importing SeedValues Completed.", LogManager.enumLogLevel.Info);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }

        public bool ImportGameCategoryDetails(string strResult)
        {
            int iRecordInserted = 0;
            bool bSuccess;

            try
            {
                LogManager.WriteLog("Storing records in GameCategory table", LogManager.enumLogLevel.Info);

                iRecordInserted = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "usp_InsertGameCategoryfromXMLForSiteConfig",
                                      AddParameter<string>(DBConstants.SP_PARAM_IMPORTINSTALLATION, SqlDbType.VarChar, strResult, 8000));

                LogManager.WriteLog("Importing GameCategory completed.", LogManager.enumLogLevel.Info);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }

        public bool ImportGameTitleDetails(string strResult)
        {
            int iRecordInserted = 0;
            bool bSuccess;

            try
            {
                LogManager.WriteLog("Storing records in GameTitle table", LogManager.enumLogLevel.Info);

                iRecordInserted = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "usp_InsertGameTitlefromXMLForSiteConfig",
                                      AddParameter<string>(DBConstants.SP_PARAM_IMPORTINSTALLATION, SqlDbType.VarChar, strResult, 8000));

                LogManager.WriteLog("Importing GameTitle completed.", LogManager.enumLogLevel.Info);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }

        public bool ImportGameLibraryDetails(string strResult)
        {
            int iRecordInserted = 0;
            bool bSuccess;

            try
            {
                LogManager.WriteLog("Storing records in GameLibrary table", LogManager.enumLogLevel.Info);

                iRecordInserted = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "usp_InsertGameLibraryfromXMLForSiteConfig",
                                      AddParameter<string>(DBConstants.SP_PARAM_IMPORTINSTALLATION, SqlDbType.Xml, strResult));

                LogManager.WriteLog("Importing GameLibrary completed.", LogManager.enumLogLevel.Info);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }


        public bool ImportPayTableDetails(string strResult)
        {
            int iRecordInserted = 0;
            bool bSuccess;

            try
            {
                LogManager.WriteLog("Storing records in PayTable table", LogManager.enumLogLevel.Info);

                iRecordInserted = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "usp_InsertPayTablefromXMLForSiteConfig",
                                      AddParameter<string>(DBConstants.SP_PARAM_IMPORTINSTALLATION, SqlDbType.Xml, strResult));

                LogManager.WriteLog("Importing PayTable completed.", LogManager.enumLogLevel.Info);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }

        public bool ImportInstallationGamePayTableInfoDetails(string strResult)
        {
            int iRecordInserted = 0;
            bool bSuccess;

            try
            {
                LogManager.WriteLog("Storing records in InstallationGamePayTableInfo table", LogManager.enumLogLevel.Info);

                iRecordInserted = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "usp_InsertIGPIfromXMLForSiteConfig",
                                      AddParameter<string>(DBConstants.SP_PARAM_IMPORTINSTALLATION, SqlDbType.Xml, strResult));

                LogManager.WriteLog("Importing InstallationGamePayTableInfo completed.", LogManager.enumLogLevel.Info);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }

        #endregion
    }
}
