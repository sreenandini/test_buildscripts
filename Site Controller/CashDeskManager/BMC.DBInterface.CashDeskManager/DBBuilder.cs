using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Data.SqlClient;
using BMC.DataAccess;
using System.Data;
using BMC.Transport;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;

namespace BMC.DBInterface.CashDeskManager
{
    /// <summary>
    /// Author:     Rakesh Marwaha
    /// Purpose:    It helps to build DB transactions like getting connection string
    /// Created:    21 Oct 2008
    /// </summary>
    public static class DBBuilder
    {
        #region "Declarations"
        static Dictionary<string, string> CollectionMachines = null;
        #endregion

        #region Common Functionalities

        /// <summary>
        /// Method to get the connection string.
        /// </summary>
        /// <param name="strTicketString"></param>
        /// <returns></returns>
        /// Method Revision History
        ///
        /// Author:     Rakesh Marwaha
        /// Purpose:    It helps to get connection string
        /// Created:    21 Oct 2008

        public static string GetExchangeConnectionString()
        {
            string strConnectionString = "";

            try
            {
                bool bUseHex = true;
                RegistryKey regKeyConnectionString = Registry.LocalMachine.OpenSubKey("Software\\Honeyframe\\Cashmaster");
                strConnectionString = regKeyConnectionString.GetValue("SQLConnect").ToString();
                regKeyConnectionString.Close();

                if (!strConnectionString.ToUpper().Contains("SERVER"))
                {
                    //BGSGeneral.cConstants objBGSConstants = new BGSGeneral.cConstants();
                    //BGSEncryptDecrypt.clsBlowFish objDecrypt = new BGSEncryptDecrypt.clsBlowFish();
                    //string strKey = objBGSConstants.ENCRYPTIONKEY;
                    //strConnectionString = objDecrypt.DecryptString(ref strConnectionString, ref strKey, ref bUseHex);
                    strConnectionString = BMC.Common.Utilities.DatabaseHelper.GetConnectionString();
                }

                return strConnectionString;
            }
            catch (Exception ex)
            {
                strConnectionString = "";
                return strConnectionString;
            }
        }

        public static string GetTicketingConnectionString()
        {
            string strConnectionString = "";

            try
            {
                strConnectionString = GetSettingFromDB();

                return strConnectionString;
            }
            catch (Exception ex)
            {
                strConnectionString = "";
                return strConnectionString;
            }
        }

        /// <summary>
        /// Get the settings for CMP Kiosk
        /// </summary>
        /// <param name="sqlparams"></param>
        /// <param name="strConnect"></param>
        /// <returns >string</returns>
        private static string GetSettingFromDB()
        {
            string strReturnValue = string.Empty;
            try
            {
                SqlParameter[] sqlparams = GetSettingParameterDB("Ticketing.Connection");
                SqlHelper.ExecuteNonQuery(GetExchangeConnectionString(), System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_GETSETTING, sqlparams);
                if (sqlparams[3].Value != null || sqlparams[3].Value.ToString() != string.Empty)
                {
                    strReturnValue = Convert.ToString(sqlparams[3].Value);
                }
                else
                {
                    strReturnValue = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return strReturnValue;
        }


        /// <summary>
        /// To set parameters for Get Setting SP
        /// </summary>
        /// <param name="strSettingName">string</param>
        /// <returns type=SqlParameter[] >sp_parames</returns>
        private static SqlParameter[] GetSettingParameterDB(string SettingName)
        {
            SqlParameter[] sp_parames = null;
            try
            {

                if (SettingName != null)
                {
                    sp_parames = new SqlParameter[4];

                    sp_parames[0] = new SqlParameter(DBConstants.CONST_SP_PARAM_SETTINGID, 0);
                    sp_parames[1] = new SqlParameter(DBConstants.CONST_SP_PARAM_SETTINGNAME, SettingName.Trim());
                    sp_parames[2] = new SqlParameter(DBConstants.CONST_SP_PARAM_SETTINGDEFAULT, string.Empty);

                    sp_parames[3] = new SqlParameter();
                    sp_parames[3].ParameterName = DBConstants.CONST_SP_PARAM_SETTINGVALUE;
                    sp_parames[3].Direction = ParameterDirection.Output;
                    sp_parames[3].Value = string.Empty;
                    sp_parames[3].SqlDbType = SqlDbType.VarChar;
                    sp_parames[3].Size = 8000;

                    //SqlParameter ReturnValue = new SqlParameter();
                    //ReturnValue.ParameterName = DBConstants.CONST_SP_PARAM_RETURNVALUE;
                    //ReturnValue.Direction = ParameterDirection.ReturnValue;
                    //sp_parames[4] = ReturnValue;

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return sp_parames;
        }

        /// <summary>
        /// Method to get the SQL Connection.
        /// </summary>
        /// <param name=""></param>
        /// <returns>SQLConnection</returns>
        /// Method Revision History
        ///
        /// Author:    Anuradha
        /// Created:   28 April 2009
        private static SqlConnection SQLConnection()
        {
            return (new SqlConnection(GetExchangeConnectionString()));
        }

        private static SqlConnection TicketSQLConnection()
        {
            return (new SqlConnection(GetTicketingConnectionString()));
        }

        #endregion Common Functionalities

        #region "Get Tickets Claimed"
        /// <summary>
        /// Method to get the tickets claimed.
        /// </summary>
        /// <param name=""></param>
        /// <returns>TicketsClaimed</returns>
        /// Method Revision History
        /// Author:    Anuradha
        /// Created:   28 April 2009
        /// 
        private static DataTable GetTicketsClaimed(TicketsClaimed oTickets)
        {

            return ExecuteTable(DBConstants.CONST_SP_GET_TICKETS_CLAIMED, oTickets);
        }


        private static DataTable ExecuteTable(string ProcedureName, TicketsClaimed oTickets)
        {
            return SqlHelper.ExecuteDataset(GetExchangeConnectionString(), ProcedureName, GetSpParameters(oTickets)).Tables[0];
        }

        private static DataTable ExecuteTable( TicketsClaimed oTickets)
        {
            return SqlHelper.ExecuteDataset(GetTicketingConnectionString(), DBConstants.CONST_SP_RSP_GETPROMOTICKETFORPERIODDETAILS, GetSpParameters(oTickets)).Tables[0];
        }

        private static SqlDataReader ExecuteReader(string StoredProcedure, TicketsClaimed oTickets)
        {
            return SqlHelper.ExecuteReader(SQLConnection(), StoredProcedure, CommandType.StoredProcedure, GetSpParameters(oTickets));
        }


        private static DataTable ExecuteTable(string StoredProcedure, Tickets oTickets)
        {
            return SqlHelper.ExecuteDataset(TicketSQLConnection(), StoredProcedure, GetVoucherParameters(oTickets)).Tables[0];
            // return SqlHelper.ExecuteDataset(SQLConnection(), StoredProcedure,CommandType.StoredProcedure, GetVoucherParameters(oTickets)).Tables[0];
        }

        private static DataTable ExecuteTable(string ExchangeConnectionString, Tickets oTickets,bool flag)
        {
            return SqlHelper.ExecuteDataset(ExchangeConnectionString, DBConstants.CONST_SP_RSP_GETTREASURYITEMS,
                GetTreasuryParameters(oTickets)).Tables[0];
            // return SqlHelper.ExecuteDataset(SQLConnection(), StoredProcedure,CommandType.StoredProcedure, GetVoucherParameters(oTickets)).Tables[0];
        }


        private static object[] GetSpParameters(TicketsClaimed oTicketsClaimed)
        {
            SqlParameter[] ObjParams = new SqlParameter[2];
            ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_STARTDATE, oTicketsClaimed.TicketsClaimedFrom);
            ObjParams[1] = new SqlParameter(DBConstants.CONST_PARAM_ENDDATE, oTicketsClaimed.TicketsClaimedTo);

            return ObjParams;
        }
        private static object[] GetTicketsParameters(Tickets oTickets)
        {
            SqlParameter[] ObjParams = new SqlParameter[4];
            ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_STARTDATE, oTickets.StartDate);
            ObjParams[1] = new SqlParameter(DBConstants.CONST_PARAM_ENDDATE, oTickets.EndDate);
            ObjParams[2] = new SqlParameter(DBConstants.CONST_PARAM_TYPE, oTickets.Type);
            ObjParams[3] = new SqlParameter(DBConstants.CONST_PARAM_LIABILITY, oTickets.IsLiability);

            return ObjParams;
        }

        private static object[] GetVoucherParameters(Tickets oTickets)
        {
            SqlParameter[] ObjParams = new SqlParameter[5];
            ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_STARTDATE, oTickets.StartDate);
            ObjParams[1] = new SqlParameter(DBConstants.CONST_PARAM_ENDDATE, oTickets.EndDate);
            ObjParams[2] = new SqlParameter(DBConstants.CONST_PARAM_TYPE, oTickets.Type);
            ObjParams[3] = new SqlParameter(DBConstants.CONST_PARAM_BARCODE, oTickets.BarCode);
            ObjParams[4] = new SqlParameter(DBConstants.CONST_PARAM_LIABILITY, Convert.ToInt16(oTickets.IsLiability));

            return ObjParams;
        }

        private static object[] GetTreasuryParameters(Tickets oTickets)
        {
            SqlParameter[] ObjParams = new SqlParameter[3];
            ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_STARTDATE, oTickets.StartDate);
            ObjParams[1] = new SqlParameter(DBConstants.CONST_PARAM_ENDDATE, oTickets.EndDate);
            ObjParams[2] = new SqlParameter(DBConstants.CONST_PARAM_TYPE, oTickets.Type);
         
            return ObjParams;
        }

      
        #endregion

        #region FillRouteFilter
        public static Dictionary<string, string> GetRoutes()
        {

            Dictionary<string, string> dRoutes = new Dictionary<string, string>();

            string strRoutes = "Select Route_No, Route_Name FROM Route ORDER BY Route_Name ASC";
            DataTable dtRoutes = SqlHelper.ExecuteDataset(SQLConnection(), CommandType.Text, strRoutes).Tables[0];

            dRoutes.Add("0", "--Any--");
            foreach (DataRow row in dtRoutes.Rows)
            {
                dRoutes.Add(row["Route_No"].ToString(), row["Route_Name"].ToString());
            }

            return dRoutes;
        }
        #endregion

        #region FillListofFilteredPositions
        public static List<string> GetFilteredPositions(string RouteNumber)
        {

            List<string> lstPositions = new List<string>();

            string strRoutes = "Select Bar_Pos_Name FROM Route_Member INNER JOIN Bar_Position ON Route_Member.Bar_Pos_No = Bar_Position.Bar_Pos_No  WHERE Route_Member.Route_No = " + RouteNumber;
            DataTable dtRoutes = SqlHelper.ExecuteDataset(SQLConnection(), CommandType.Text, strRoutes).Tables[0];

            lstPositions.Add("--Any--");
            foreach (DataRow row in dtRoutes.Rows)
            {
                lstPositions.Add(row["Bar_Pos_Name"].ToString());
            }

            return lstPositions;
        }
        #endregion

        #region "Get Tickets"

        public static DataTable GetTickets(Tickets oTickets)
        {
            DataTable dtTickets = null;
            try
            {
                string strVoucherInformation = "rsp_BGS_VoucherInformation";

                dtTickets = ExecuteTable(strVoucherInformation, oTickets);
                return dtTickets;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return dtTickets;
            }

        }
        #endregion

        #region GetBarPositionfromAsset
        public static string GetBarPositionFromAsset(string sMachine)
        {
            string strPosName = string.Empty;
            try
            {
                if (CollectionMachines == null)
                {
                    CollectionMachines = new Dictionary<string, string>();
                }
                if (!String.IsNullOrEmpty(FindMachine(sMachine)))
                {
                    strPosName = FindMachine(sMachine);
                    return strPosName;
                }

                SqlParameter[] param = SqlHelperParameterCache.GetSpParameterSet(GetExchangeConnectionString(), "rsp_GetMachineDetailsFromAsset");
                foreach (SqlParameter sqlparam in param)
                {
                    sqlparam.Value = sMachine;
                }


                object oPosName = SqlHelper.ExecuteScalar(GetExchangeConnectionString(), CommandType.StoredProcedure, "rsp_GetMachineDetailsFromAsset", param);

                if (oPosName != null)
                {
                    strPosName = oPosName.ToString();
                }

                if (string.IsNullOrEmpty(strPosName))
                {
                    strPosName = "(" + sMachine + ")";
                }

                CollectionMachines.Add(sMachine, strPosName);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetBarPositionFromAsset" + ex.Message, LogManager.enumLogLevel.Info);
            }
            return strPosName;
        }

        private static string FindMachine(string sMachine)
        {
            string strBarPosition = string.Empty;
            try
            {
                strBarPosition = CollectionMachines[sMachine];
            }
            catch (Exception ex)
            {
                //LogManager.WriteLog("FindMachine " + ex.Message, LogManager.enumLogLevel.Info);
            }
            return strBarPosition;
        }

        public static Dictionary<string, string> LoadTicketWorkstations()
        {
            Dictionary<string, string> dTicketWorkStations = new Dictionary<string, string>();
            SqlDataReader reader = SqlHelper.ExecuteReader(GetExchangeConnectionString(), CommandType.Text, "SELECT * FROM Ticket_Issue_Workstation");
            while (reader.Read())
            {
                if (!string.IsNullOrEmpty(reader["TIW_Name"].ToString()))
                {
                    dTicketWorkStations.Add(reader["TIW_Name"].ToString(), reader["TIW_Name"].ToString());
                }
            }
            return dTicketWorkStations;

        }




        #endregion

        public static List<TicketExceptions> TitoTicketOutExceptions(Tickets oTickets, List<string> lstPositionstoDisplay)
        {
            List<TicketExceptions> lstTickets = null;
            try
            {
                SqlParameter[] parameters = SqlHelperParameterCache.GetSpParameterSet(GetExchangeConnectionString(), "rsp_GetNonCompletedTicketPrints");
                foreach (SqlParameter param in parameters)
                {
                    if (param.ParameterName == "@StartDate")
                    {
                        param.Value = oTickets.StartDate;
                    }
                    if (param.ParameterName == "@EndDate")
                    {
                        param.Value = oTickets.EndDate;
                    }
                }
                SqlDataReader reader = SqlHelper.ExecuteReader(GetExchangeConnectionString(), "rsp_GetNonCompletedTicketPrints", parameters);

                lstTickets = new List<TicketExceptions>();
                TicketExceptions excep = null;

                while (reader.Read())
                {
                    excep = new TicketExceptions();
                    excep.SEGM = reader["bar_pos_name"].ToString();
                    excep.Machine = DBBuilder.GetBarPositionFromAsset(reader["bar_pos_name"].ToString());
                    excep.currValue = (float)Convert.ToDouble(reader["TE_Value"]) / 100;
                    if (!string.IsNullOrEmpty(excep.SEGM) && DBCommon.CheckPositionToDisplay(excep.Machine, lstPositionstoDisplay))
                    {
                        excep.bExceptionRecordFound = true;
                        excep.Type = "OUT";
                        excep.Position = DBBuilder.GetBarPositionFromAsset(reader["bar_pos_name"].ToString());
                        excep.PrintDate = Convert.ToDateTime(reader["TE_Date"]).ToString("dd MMM yyyy") + " " +
                            Convert.ToDateTime(reader["TE_Date"]).ToString("HH:mm");
                        if (!string.IsNullOrEmpty(reader["TE_TicketNumber"].ToString()))
                        {
                            excep.Ticket = reader["ActualBarcode"].ToString();
                        }
                        else
                        {
                            excep.Ticket = reader["TE_TicketNumber"].ToString();
                        }
                        excep.Value = Convert.ToDouble(reader["TE_Value"]) / 100;
                        excep.Asset = reader["stock_no"].ToString();
                        excep.COLINSTALLID = Convert.ToInt16(reader["TE_Installation_No"].ToString());
                        excep.CreateCompleted = reader["CreateExpected"].ToString();

                        excep.cTicketTotal += excep.currValue;
                        excep.cExceptionsTotal += excep.currValue;
                    }
                    else if (DBCommon.IsMachineATicketWorkstation(excep.Machine))
                    {
                        excep.bExceptionRecordFound = true;
                        excep.Type = "OUT";
                        excep.Position = DBBuilder.GetBarPositionFromAsset(reader["bar_pos_name"].ToString());
                        excep.PrintDate = Convert.ToDateTime(reader["TE_Date"]).ToString("dd MMM yyyy") + " " +
                            Convert.ToDateTime(reader["TE_Date"]).ToString("HH:mm");
                        if (!string.IsNullOrEmpty(reader["TE_TicketNumber"].ToString()))
                        {
                            excep.Ticket = reader["ActualBarcode"].ToString();
                        }
                        else
                        {
                            excep.Ticket = reader["TE_TicketNumber"].ToString();
                        }
                        excep.Value = Convert.ToDouble(reader["TE_Value"]) / 100;
                        excep.Asset = reader["stock_no"].ToString();
                        excep.COLINSTALLID = Convert.ToInt16(reader["TE_Installation_No"].ToString());
                        excep.CreateCompleted = reader["CreateExpected"].ToString();

                        excep.cTicketTotal += excep.currValue;
                        excep.cExceptionsTotal += excep.currValue;
                    }
                    lstTickets.Add(excep);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lstTickets;
        }

        public static List<TicketExceptions> TitoTicketsClaimed(Tickets oTickets, List<string> lstPositionstoDisplay)
        {
            string strTicketInException = string.Empty;
            string sPosTer = string.Empty;
            List<TicketExceptions> lstTickets = null;
            TicketExceptions excep = null;

            try
            {
                DataTable dtTickets = DBBuilder.GetTickets(oTickets);
                if (dtTickets == null && dtTickets.Rows.Count < 0)
                {
                }
                else
                {
                    lstTickets = new List<TicketExceptions>();

                    foreach (DataRow row in dtTickets.Rows)
                    {
                        excep = new TicketExceptions();
                        excep.bExceptionRecordFound = false;
                        excep.SEGM = row["PayDevice"].ToString();
                        excep.currValue = (float)Convert.ToDouble(row["iAmount"]) / 100;
                        if (!string.IsNullOrEmpty(excep.SEGM) && row["strDeviceType"].ToString().Equals("EXTITO"))
                        {
                            if (DBCommon.IsMachineATicketWorkstation(excep.SEGM))
                            {
                                if (DBCommon.CheckPositionToDisplay(GetBarPositionFromAsset(row["PrintDevice"].ToString()), lstPositionstoDisplay))
                                {
                                    sPosTer = excep.SEGM;
                                    excep.Machine = GetBarPositionFromAsset(row["PrintDevice"].ToString());
                                    excep.CurrentCashDesk += excep.currValue;
                                    excep.CashDeskClaimedQty += 1;
                                    if (oTickets.IsClaimedInCashDesk == true)
                                    {
                                        excep.bExceptionRecordFound = true;
                                    }
                                }
                            }
                            else
                            {
                                sPosTer = string.Empty;
                                excep.Machine = GetBarPositionFromAsset(excep.SEGM);

                                if (DBCommon.CheckPositionToDisplay(excep.Machine, lstPositionstoDisplay))
                                {
                                    excep.currEGM += excep.currValue;
                                    excep.MachineClaimedQty += 1;
                                    if (oTickets.IsClaimedInMachine)
                                    {
                                        excep.bExceptionRecordFound = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            sPosTer = row["PrintDevice"].ToString();
                            excep.Machine = row["PayDevice"].ToString();
                            excep.CurrentCashDesk += excep.currValue;
                            excep.CashDeskClaimedQty += 1;
                            if (oTickets.IsClaimedInCashDesk)
                            {
                                excep.bExceptionRecordFound = true;
                            }
                        }
                        if (excep.bExceptionRecordFound)
                        {

                            string dPrintDate = row["dtPrinted"].ToString();
                            string dClaimDate = row["dtPaid"].ToString();


                            excep.Position = sPosTer;
                            excep.Machine = DBBuilder.GetBarPositionFromAsset(excep.Machine);
                           // excep.Machine = sPosTer;
                            excep.TransactionType="TITO Claimed";
                            excep.Zone = "n/a";

                            excep.PrintDate = Convert.ToDateTime(row["dtPrinted"]).ToString("dd MMM yyyy") + " " +
                                Convert.ToDateTime(row["dtPrinted"]).ToString("HH:mm");

                            if (!(row["dtPaid"] == null))
                            {
                                excep.PayDate = Convert.ToDateTime(row["dtPaid"] != null ? row["dtPaid"] : string.Empty).ToString("dd MMM yyyy") + " " +
                                                Convert.ToDateTime(row["dtPaid"] != null ? row["dtPaid"] : string.Empty).ToString("hh:mm");
                            }


                            excep.PayDevice = (row["PayDevice"] != null ? GetBarPositionFromAsset(row["PayDevice"].ToString()) : string.Empty);

                            if (!string.IsNullOrEmpty(row["strBarCode"].ToString()))
                            {
                                excep.Ticket = row["ActualBarcode"].ToString();
                            }
                            else
                            {
                                excep.Ticket = row["strBarcode"].ToString();
                            }
                            excep.Value = -(Convert.ToDouble(row["iAmount"]) / 100);
                            excep.Amount = (-(excep.Value)).ToString();


                            if ((Convert.ToDateTime(dPrintDate) >= Convert.ToDateTime(oTickets.StartDate)) &&
                                (Convert.ToDateTime(dClaimDate) <= Convert.ToDateTime(oTickets.EndDate)))
                            {
                                excep.Status = "Printed in different category, Claimed within";
                            }
                            else
                            {
                                excep.Status = "Printed prior to period, Claimed within";
                            }
                            excep.Details = "";
                            excep.cExceptionsTotal += (float)excep.Value;
                            
                        }

                        lstTickets.Add(excep);
                    }
                }

            }

            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
            return lstTickets;
        }


        public static List<TicketExceptions> TitoTicketsClaimedLiability(Tickets oTickets, List<string> lstPositionstoDisplay)
        {
            string strTicketInException = string.Empty;
            string sPosTer = string.Empty;
            List<TicketExceptions> lstTickets = null;
            TicketExceptions excep = null;

            try
            {
                DataTable dtTickets = DBBuilder.GetTickets(oTickets);
                if (dtTickets == null && dtTickets.Rows.Count < 0)
                {
                }
                else
                {
                    lstTickets = new List<TicketExceptions>();

                    foreach (DataRow row in dtTickets.Rows)
                    {
                        excep = new TicketExceptions();
                        excep.bExceptionRecordFound = false;
                        excep.SEGM = row["PayDevice"].ToString();
                        excep.currValue = (float)Convert.ToDouble(row["iAmount"]) / 100;
                        if (!string.IsNullOrEmpty(excep.SEGM) && row["strDeviceType"].ToString().Equals("EXTITO"))
                        {
                            if (DBCommon.IsMachineATicketWorkstation(excep.SEGM))
                            {
                                if (DBCommon.CheckPositionToDisplay(GetBarPositionFromAsset(row["PrintDevice"].ToString()), lstPositionstoDisplay))
                                {
                                    sPosTer = excep.SEGM;
                                    excep.Machine = GetBarPositionFromAsset(row["PrintDevice"].ToString());
                                    excep.bExceptionRecordFound = true;
                                }
                            }
                            else
                            {
                                sPosTer = string.Empty;
                                excep.Machine = GetBarPositionFromAsset(excep.SEGM);

                                if (DBCommon.CheckPositionToDisplay(excep.Machine, lstPositionstoDisplay))
                                {
                                    excep.bExceptionRecordFound = true;
                                }
                            }
                        }
                        else
                        {

                            excep.bExceptionRecordFound = true;
                        }
                        if (excep.bExceptionRecordFound)
                        {

                            string dPrintDate = row["dtPrinted"].ToString();
                            string dClaimDate = row["dtPaid"].ToString();


                            excep.Position = DBBuilder.GetBarPositionFromAsset(row["PrintDevice"].ToString());
                            excep.TransactionType = "TITO Claimed";
                            excep.Zone = "n/a";

                            excep.PrintDate = Convert.ToDateTime(row["dtPrinted"]).ToString("dd MMM yyyy") + " " +
                                Convert.ToDateTime(row["dtPrinted"]).ToString("HH:mm");

                            if (!(row["dtPaid"] == null))
                            {
                                excep.PayDate = Convert.ToDateTime(row["dtPaid"] != null ? row["dtPaid"] : string.Empty).ToString("dd MMM yyyy") + " " +
                                                Convert.ToDateTime(row["dtPaid"] != null ? row["dtPaid"] : string.Empty).ToString("hh:mm");
                            }


                            excep.PayDevice = (row["PayDevice"] != null ? GetBarPositionFromAsset(row["PayDevice"].ToString()) : string.Empty);

                            if (!string.IsNullOrEmpty(row["strBarCode"].ToString()))
                            {
                                excep.Ticket = row["ActualBarcode"].ToString();
                            }
                            else
                            {
                                excep.Ticket = row["strBarcode"].ToString();
                            }
                            excep.Value = -(Convert.ToDouble(row["iAmount"]) / 100);
                            excep.Amount = "(" + (-(excep.Value)) + ")";


                            if ((Convert.ToDateTime(dPrintDate) >= Convert.ToDateTime(oTickets.StartDate)) &&
                                (Convert.ToDateTime(dClaimDate) <= Convert.ToDateTime(oTickets.EndDate)))
                            {
                                excep.Status = "Printed in different category, Claimed within";
                            }
                            else
                            {
                                excep.Status = "Printed prior to period, Claimed within";
                            }
                            excep.cExceptionsTotal += (float)excep.Value;
                        }

                        lstTickets.Add(excep);
                    }
                }

            }

            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
            return lstTickets;
        }

        public static List<TicketExceptions> TitoTicketsPrinted(Tickets oTickets, List<string> lstPositionstoDisplay)
        {
            string strTicketInException = string.Empty;
            string sPosTer = string.Empty;
            List<TicketExceptions> lstTickets = null;
            TicketExceptions excep = null;
            double CurrentCashDesk = 0, currEGM = 0;

            try
            {
               DataTable dtTickets = DBBuilder.GetTickets(oTickets);
                if (dtTickets == null && dtTickets.Rows.Count < 0)
                {
                }
                else
                {
                    lstTickets = new List<TicketExceptions>();

                    foreach (DataRow row in dtTickets.Rows)
                    {
                        excep = new TicketExceptions();
                        excep.bExceptionRecordFound = false;
                        excep.SEGM = row["PrintDevice"].ToString();
                        excep.VoucherStatus = row["strVoucherStatus"].ToString();
                        excep.TicketAddedtoList = false;
                        excep.currValue = (float)Convert.ToDouble(row["iAmount"]) / 100;
                        if (!string.IsNullOrEmpty(excep.SEGM))
                        {
                            if (row["strDeviceType"].ToString().Equals("EXTITO") || string.IsNullOrEmpty(row["strDeviceType"].ToString()))
                            {
                                if (DBCommon.IsMachineATicketWorkstation(excep.SEGM))
                                {
                                    sPosTer = excep.SEGM;
                                    excep.Position = excep.SEGM;
                                    excep.Machine = "";
                                    CurrentCashDesk += excep.currValue;

                                    excep.CashDeskPrintedQty += 1;
                                    if (oTickets.IsPrintedInCashDesk)
                                    {
                                        excep.TicketAddedtoList = true;
                                    }
                                }
                                else
                                {
                                    sPosTer = string.Empty;
                                    excep.Machine = GetBarPositionFromAsset(excep.SEGM);
                                    if (DBCommon.CheckPositionToDisplay(excep.Machine, lstPositionstoDisplay))
                                    {
                                        currEGM += excep.currValue;
                                        excep.MachinePrintedQty += 1;
                                        if (oTickets.IsPrintedInMachine)
                                        {
                                            excep.TicketAddedtoList = true;
                                        }
                                    }
                                    else
                                    {
                                        excep.TicketAddedtoList = false;
                                    }
                                }
                            }
                            else
                            {
                                excep.Position = GetBarPositionFromAsset(excep.SEGM);
                                excep.Machine = "";
                                excep.CurrentCashDesk += excep.currValue;

                                excep.CashDeskPrintedQty += 1;
                                excep.TicketAddedtoList = true;
                            }
                        }

                        if (excep.TicketAddedtoList)
                        {

                            string dPrintDate = row["dtPrinted"].ToString();
                            string dClaimDate = string.IsNullOrEmpty(row["dtPaid"].ToString()) ? "01/01/3100" : row["dtPaid"].ToString();

                            excep.TransactionType = "TITO Printed";
                            excep.Zone = "n/a";


                            if ((Convert.ToDateTime(dPrintDate) >= Convert.ToDateTime(oTickets.StartDate)) &&
                                (Convert.ToDateTime(dClaimDate) <= Convert.ToDateTime(oTickets.EndDate)))
                            {
                                excep.Status = "Printed within, Claimed in different category";
                            }
                            else if (string.IsNullOrEmpty(row["dtPaid"].ToString()))
                            {
                                excep.Status = "Active Ticket";
                            }
                            else
                            {
                                excep.Status = "Printed within period, claimed later";
                            }

                            excep.Position = sPosTer;

                            excep.PrintDate = Convert.ToDateTime(row["dtPrinted"]).ToString("dd MMM yyyy") + " " +
                                Convert.ToDateTime(row["dtPrinted"]).ToString("HH:mm");

                            if (!(row["dtPaid"] == null))
                            {
                                excep.PayDate = Convert.ToDateTime(row["dtPaid"] != null ? row["dtPaid"] : string.Empty).ToString("dd MMM yyyy") + " " +
                                                Convert.ToDateTime(row["dtPaid"] != null ? row["dtPaid"] : string.Empty).ToString("HH:mm");
                            }
                            excep.PayDate = Convert.ToDateTime(row["dtPrinted"] != null ? row["dtPrinted"] : string.Empty).ToString("dd MMM yyyy") + " " +
                                                Convert.ToDateTime(row["dtPrinted"] != null ? row["dtPrinted"] : string.Empty).ToString("HH:mm");

                            excep.PayDevice = (row["PayDevice"] != null ? row["PayDevice"].ToString() : string.Empty);

                            if (!string.IsNullOrEmpty(row["strBarCode"].ToString()))
                            {
                                excep.Ticket = row["ActualBarcode"].ToString();
                            }
                            else
                            {
                                excep.Ticket = row["strBarcode"].ToString();
                            }
                            excep.Details = excep.Ticket;
                            excep.Value = Convert.ToDouble(row["iAmount"]) / 100;
                            excep.Amount = excep.Value.ToString("###0.00");
                        }
                        lstTickets.Add(excep);
                    }
                }

            }

            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
            if (lstTickets != null && lstTickets.Count > 0)
            {
                lstTickets[0].currEGM = currEGM;
                lstTickets[0].CurrentCashDesk = CurrentCashDesk;
            }
            
            return lstTickets;
        }


        public static List<TicketExceptions> TitoTicketsPrintedLiability(Tickets oTickets, List<string> lstPositionstoDisplay)
        {
            string strTicketInException = string.Empty;
            string sPosTer = string.Empty;
            List<TicketExceptions> lstTickets = null;
            TicketExceptions excep = null;

            try
            {
                DataTable dtTickets = DBBuilder.GetTickets(oTickets);
                if (dtTickets == null && dtTickets.Rows.Count < 0)
                {
                }
                else
                {
                    lstTickets = new List<TicketExceptions>();

                    foreach (DataRow row in dtTickets.Rows)
                    {
                        excep = new TicketExceptions();
                        excep.bExceptionRecordFound = false;
                        excep.SEGM = row["PrintDevice"].ToString();
                        excep.VoucherStatus = row["strVoucherStatus"].ToString();
                        excep.TicketAddedtoList = false;
                        excep.currValue = (float)Convert.ToDouble(row["iAmount"]) / 100;
                        if (!string.IsNullOrEmpty(excep.SEGM))
                        {
                            if (row["strDeviceType"].ToString().Equals("EXTITO") || string.IsNullOrEmpty(row["strDeviceType"].ToString()))
                            {
                                if (DBCommon.IsMachineATicketWorkstation(excep.SEGM))
                                {
                                    excep.TicketAddedtoList = true;
                                }
                                else
                                {
                                    sPosTer = string.Empty;
                                    excep.Machine = GetBarPositionFromAsset(excep.SEGM);
                                    if (DBCommon.CheckPositionToDisplay(excep.Machine, lstPositionstoDisplay))
                                    {

                                        excep.TicketAddedtoList = true;
                                    }
                                }
                            }
                            else
                            {  
                                excep.TicketAddedtoList = true;
                            }
                        }

                        if (excep.TicketAddedtoList)
                        {

                            string dPrintDate = row["dtPrinted"].ToString();
                            string dClaimDate = string.IsNullOrEmpty(row["dtPaid"].ToString()) ? "01/01/3100" : row["dtPaid"].ToString();

                            excep.TransactionType = "TITO Printed";
                            excep.Zone = "n/a";


                            if ((Convert.ToDateTime(dPrintDate) >= Convert.ToDateTime(oTickets.StartDate)) &&
                                (Convert.ToDateTime(dClaimDate) <= Convert.ToDateTime(oTickets.EndDate)))
                            {
                                excep.Status = "Printed within, Claimed in different category";
                            }
                            else if (string.IsNullOrEmpty(row["dtPaid"].ToString()))
                            {
                                excep.Status = "Active Ticket";
                            }
                            else
                            {
                                excep.Status = "Printed within period, claimed later";
                            }

                            excep.Position = DBBuilder.GetBarPositionFromAsset(row["PrintDevice"].ToString());

                            excep.PrintDate = Convert.ToDateTime(row["dtPrinted"]).ToString("dd MMM yyyy") + " " +
                                Convert.ToDateTime(row["dtPrinted"]).ToString("HH:mm");

                            if (!(row["dtPaid"] == null))
                            {
                                excep.PayDate = Convert.ToDateTime(row["dtPaid"] != null ? row["dtPaid"] : string.Empty).ToString("dd MMM yyyy") + " " +
                                                Convert.ToDateTime(row["dtPaid"] != null ? row["dtPaid"] : string.Empty).ToString("HH:mm");
                            }


                            excep.PayDevice = (row["PayDevice"] != null ? GetBarPositionFromAsset( row["PayDevice"].ToString() ): string.Empty);

                            if (!string.IsNullOrEmpty(row["strBarCode"].ToString()))
                            {
                                excep.Ticket = row["ActualBarcode"].ToString();
                            }
                            else
                            {
                                excep.Ticket = row["strBarcode"].ToString();
                            }
                            excep.Value = Convert.ToDouble(row["iAmount"]) / 100;
                            excep.Amount = "(" + excep.Value + ")";


                        }

                        lstTickets.Add(excep);
                    }
                }

            }

            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
            return lstTickets;
        }
        public static List<TicketExceptions> TicketsClaimed(TicketsClaimed oTickets, List<string> lstPositionstoDisplay)
        {
            string strTicketInException = string.Empty;
            string sPosTer = string.Empty;
            List<TicketExceptions> lstTickets = null;
            TicketExceptions excep = null;

            try
            {
                DataTable dtTicketsClaimed = GetTicketsClaimed(oTickets);
                if (dtTicketsClaimed != null && dtTicketsClaimed.Rows.Count > 0)
                {
                    lstTickets = new List<TicketExceptions>();

                    foreach (DataRow row in dtTicketsClaimed.Rows)
                    {
                        excep = new TicketExceptions();
                        excep.currValue = (float)Convert.ToDouble(row["tbr_payout_value"]) / 100;
                        if (DBCommon.CheckPositionToDisplay(row["bar_pos_name"].ToString(), lstPositionstoDisplay))
                        {
                            if (Convert.ToDateTime(row["tbr_payout_time"].ToString()) <= Convert.ToDateTime(oTickets.TicketsClaimedFrom))
                            {
                                excep.Position = DBBuilder.GetBarPositionFromAsset(row["TBR_Payout_Machine_Serial"].ToString());

                                excep.PrintDate = Convert.ToDateTime(row["TBR_Payout_Print_Time"]).ToString("dd MMM yyyy") + " " +
                                    Convert.ToDateTime(row["TBR_Payout_Print_Time"]).ToString("HH:mm");

                                if (!(row["TBR_Payout_Time"] == null))
                                {
                                    excep.PayDate = Convert.ToDateTime(row["TBR_Payout_Time"] != null ? row["TBR_Payout_Time"] : string.Empty).ToString("dd MMM yyyy") + " " +
                                                    Convert.ToDateTime(row["TBR_Payout_Time"] != null ? row["TBR_Payout_Time"] : string.Empty).ToString("HH:mm");
                                }

                                excep.ClaimedTerminal = DBBuilder.GetBarPositionFromAsset(row["TBR_Payout_Claimed_Terminal"].ToString());
                                excep.Ticket = row["TBR_Payout_ExternalIndex"].ToString();
                                excep.Status = "Printed prior to period, Claimed within";

                                excep.Value = -(Convert.ToDouble(row["tbr_payout_value"]) / 100);
                                excep.Amount = "(" + excep.Value + ")";
                                excep.cTicketTotal += excep.currValue;

                            }
                        }

                        lstTickets.Add(excep);
                    }
                }

            }

            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
            return lstTickets;
        }

        public static List<TicketExceptions> TicketsPrinted(TicketsClaimed oTickets, List<string> lstPositionstoDisplay)
        {
            string strTicketInException = string.Empty;
            string sPosTer = string.Empty;
            List<TicketExceptions> lstTickets = null;
            TicketExceptions excep = null;

            try
            {
                DataTable dtTicketsClaimed = GetTicketsPrinted(oTickets);
                if (dtTicketsClaimed != null && dtTicketsClaimed.Rows.Count > 0)
                {
                    lstTickets = new List<TicketExceptions>();

                    foreach (DataRow row in dtTicketsClaimed.Rows)
                    {
                        excep = new TicketExceptions();
                        excep.currValue = (float)Convert.ToDouble(row["tbr_payout_value"]) / 100;
                        if (DBCommon.CheckPositionToDisplay(row["bar_pos_name"].ToString(), lstPositionstoDisplay))
                        {
                            if (row["tbr_payout_claimed"].ToString().Equals("1"))
                            {
                                excep.Position = DBBuilder.GetBarPositionFromAsset(row["TBR_Payout_Machine_Serial"].ToString());

                                excep.PrintDate = Convert.ToDateTime(row["TBR_Payout_Print_Time"]).ToString("dd MMM yyyy") + " " +
                                    Convert.ToDateTime(row["TBR_Payout_Print_Time"]).ToString("HH:mm");

                                if (!(row["TBR_Payout_Time"] == null))
                                {
                                    excep.PayDate = Convert.ToDateTime(row["TBR_Payout_Time"] != null ? row["TBR_Payout_Time"] : string.Empty).ToString("dd MMM yyyy") + " " +
                                                    Convert.ToDateTime(row["TBR_Payout_Time"] != null ? row["TBR_Payout_Time"] : string.Empty).ToString("HH:mm");
                                }

                                excep.ClaimedTerminal = DBBuilder.GetBarPositionFromAsset(row["TBR_Payout_Claimed_Terminal"].ToString());
                                excep.Ticket = row["TBR_Payout_ExternalIndex"].ToString();
                                excep.Status = "Printed prior to period, Claimed within";

                                excep.Value = Convert.ToDouble(row["tbr_payout_value"]) / 100;
                                excep.Amount = "(" + excep.Value + ")";
                                excep.cTicketTotal += excep.currValue;

                            }
                        }

                        lstTickets.Add(excep);
                    }
                }

            }

            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
            return lstTickets;
        }

        private static DataTable GetTicketsPrinted(TicketsClaimed oTickets)
        {
            return ExecuteTable(DBConstants.CONST_SP_GET_TICKETS_PRINTED, oTickets);
        }


        public static List<TicketExceptions> TicketsUnClaimed(TicketsClaimed oTickets, List<string> lstPositions)
        {
            string strTicketInException = string.Empty;
            List<TicketExceptions> lstTickets = null;
            TicketExceptions excep = null;


            try
            {
                DataTable dtTickets = ExecuteTable(DBConstants.CONST_SP_GET_TICKETS_UNCLAIMED, oTickets);
                if (dtTickets == null && dtTickets.Rows.Count < 0)
                {
                }
                else
                {
                    lstTickets = new List<TicketExceptions>();

                    foreach (DataRow row in dtTickets.Rows)
                    {
                        excep = new TicketExceptions();
                        excep.SEGM = row["bar_pos_name"].ToString();
                        excep.Machine = DBBuilder.GetBarPositionFromAsset(row["bar_pos_name"].ToString());

                        if (DBCommon.CheckPositionToDisplay(excep.Machine, lstPositions))
                        {
                            excep.Position = DBBuilder.GetBarPositionFromAsset(row["PrintDevice"].ToString());
                            excep.Machine = row["bar_pos_name"].ToString() + " - " + row["tbr_node_serial"].ToString();
                            excep.TransactionType = "Ticket";
                            excep.Zone = row["Zone_Name"] != null ? row["Zone_Name"].ToString() : string.Empty;

                            excep.PrintDate = Convert.ToDateTime(row["tbr_payout_time"]).ToString("dd MMM yyyy") + " " +
                                Convert.ToDateTime(row["tbr_payout_time"]).ToString("HH:mm");

                            excep.PayDate = Convert.ToDateTime(row["tbr_payout_time"]).ToString("dd MMM yyyy") + " " +
                             Convert.ToDateTime(row["tbr_payout_time"]).ToString("HH:mm");

                            excep.SEGM = row["machine_name"].ToString();

                            excep.Value = Convert.ToDouble(row["tbr_payout_value"]) / 100;
                            excep.Amount = "(" + Convert.ToDouble(row["tbr_payout_value"]) / 100 + ")";

                           excep.cExceptionsTotal += (float) excep.Value;
                        }
                        else if (DBCommon.IsMachineATicketWorkstation(row["PrintDevice"].ToString()))
                        {
                            excep.Position = DBBuilder.GetBarPositionFromAsset(row["PrintDevice"].ToString());
                            excep.Machine = row["bar_pos_name"].ToString() + " - " + row["tbr_node_serial"].ToString();
                            excep.TransactionType = "Ticket";
                            excep.Zone = row["Zone_Name"] != null ? row["Zone_Name"].ToString() : string.Empty;

                            excep.PrintDate = Convert.ToDateTime(row["tbr_payout_time"]).ToString("dd MMM yyyy") + " " +
                                Convert.ToDateTime(row["tbr_payout_time"]).ToString("HH:mm");

                            excep.PayDate = Convert.ToDateTime(row["tbr_payout_time"]).ToString("dd MMM yyyy") + " " +
                             Convert.ToDateTime(row["tbr_payout_time"]).ToString("HH:mm");

                            excep.SEGM = row["machine_name"].ToString();

                            excep.Value = Convert.ToDouble(row["tbr_payout_value"]) / 100;
                            excep.Amount = "(" + Convert.ToDouble(row["tbr_payout_value"]) / 100 + ")";

                            excep.cExceptionsTotal += (float)excep.Value;
                        }

                        lstTickets.Add(excep);
                    }
                }

            }

            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
            return lstTickets;
        }

        #region "Get Promo Tickets"
        public static DataTable GetPromoTickets(TicketsClaimed oTickets)
        {

            DataTable dtTickets = null;
            try
            {
                
                dtTickets = ExecuteTable(oTickets);
                return dtTickets;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return dtTickets;
            }
        }
        #endregion "Get Promo Tickets"

        public static List<TicketExceptions> RetrieveTicketAnomalies(TicketsClaimed oTickets, List<string> lstPositions)
        {
           string strTicketInException = string.Empty;
            List<TicketExceptions> lstTickets = null;
            TicketExceptions excep = null;


            try
            {
                DataTable dtTickets = ExecuteTable(DBConstants.CONST_SP_RSP_TICKET_ANOMALIES, oTickets);
                if (dtTickets == null && dtTickets.Rows.Count < 0)
                {
                    return null;
                }
                else
                {
                    lstTickets = new List<TicketExceptions>();

                    foreach (DataRow row in dtTickets.Rows)
                    {
                        excep = new TicketExceptions();
                        excep.SEGM = row["Machine"].ToString();
                        excep.Machine = DBBuilder.GetBarPositionFromAsset(row["Machine"].ToString());

                        if (!string.IsNullOrEmpty(excep.SEGM) &&
                            DBCommon.CheckPositionToDisplay(excep.Machine, lstPositions))
                        {
                            excep.Reference = row["Reference"].ToString();
                            excep.Value = Convert.ToDouble(row["Amount"]);
                            excep.PrintDate = Convert.ToDateTime(row["dtDate"].ToString()).ToString("dd MMM yyyy");
                            excep.Details = row["Details"].ToString();
                        }
                        lstTickets.Add(excep);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lstTickets;
        }

        public static List<TicketExceptions> GetTreasuryItems(Tickets oTickets, List<string> lstPositions)
        {
            List<TicketExceptions> items = null;
            TicketExceptions treasury = null;

            try
            {
                DataTable dtTreasury = ExecuteTable(GetExchangeConnectionString(), oTickets, false);

                items = new List<TicketExceptions>();
                foreach (DataRow row in dtTreasury.Rows)
                {

                    if (DBCommon.CheckPositionToDisplay(row["bar_pos_name"].ToString(), lstPositions))
                    {
                        

                            treasury = new TicketExceptions();
                            treasury.PayDate = row["treasury_date"].ToString() + " " + row["treasury_time"].ToString();
                            treasury.PrintDate = row["treasury_date"].ToString() + " " + row["treasury_time"].ToString();
                            treasury.TransactionType = oTickets.Type;
                            treasury.Zone = row["Zone_name"].ToString();
                            treasury.Position = row["bar_pos_name"].ToString();
                            treasury.Machine = row["machine_name"].ToString();
                            treasury.Amount = row["treasury_amount"].ToString();
                            treasury.Details = "";
                       
                        switch (oTickets.Type)
                        {
                            case DBConstants.CONST_HANDPAYCREDIT:
                                {
                                    treasury.HandpayQty += 1;
                                    break;
                                }
                            case DBConstants.CONST_JACKPOT:
                                {
                                    treasury.JackPotQty += 1;
                                    break;
                                }
                            case DBConstants.CONST_PROG:
                                {
                                    treasury.ProgQty += 1;
                                    break;
                                }
                            case DBConstants.CONST_REFILL:
                                {
                                    treasury.RefillQty += 1;
                                    break;
                                }
                            case DBConstants.CONST_REFUND:
                                {
                                    treasury.RefundQty += 1;
                                    break;
                                }
                            case DBConstants.CONST_SHORTPAY:
                                {
                                    treasury.ShortQty += 1;
                                    break;
                                }
                            case DBConstants.CONST_FLOAT:
                                {
                                    treasury.FloatQty += 1;
                                    break;
                                }
                            default:
                                break;
                        }
                        items.Add(treasury);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return items;
        }

        public static string GetRegionFromSite()
        {
            string Query = "Select Region = Case Upper(Region) When 'US' Then 'US'	WHEN 'UK' THEN  'UK' ELSE '' End  From Site";

            object objRegion=SqlHelper.ExecuteScalar(GetExchangeConnectionString(), CommandType.Text, Query);
            if (objRegion != null)
            {
                return (objRegion.ToString());
            }
            else
            {
               return  string.Empty;
            }
        }
    }
}
