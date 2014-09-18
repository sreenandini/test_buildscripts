using System;
using System.Collections.Generic;
using BMC.Common.Utilities;
using Microsoft.Win32;
using System.Data.SqlClient;
using BMC.Common.ExceptionManagement;
using System.Data;
using BMC.DataAccess;
using BMC.Common.LogManagement;
using BMC.Transport;
using BMC.Common.Utilities;
using System.Linq;

namespace BMC.DBInterface.CashDeskOperator
{
    public class CashDeskManagerDataAccess
    {
        #region "Declarations"
         Dictionary<string, string> CollectionMachines = null;
         string CurrencySymbol = string.Empty;
        #endregion

        #region Common Functionalities

        /// <summary>
        /// Method to get the connection string.
        /// </summary>
        /// <param name="strTicketString"></param>
        /// <returns></returns>
        /// Method Revision History
        ///
        /// Author:     Anuradha
        /// Purpose:    It helps to get connection string
        /// 

         public CashDeskManagerDataAccess()
        {
            CurrencySymbol = CurrencySymbol.GetCurrencySymbol();
        }
        public string GetExchangeConnectionString()
        {
            return DatabaseHelper.GetExchangeConnectionString();
        }

        public  string GetTicketingConnectionString()
        {
            return DatabaseHelper.GetTicketingConnectionString();
        }

        public string GetHopperSetting()
        {
            string strHopperSetting = "";

            try
            {
                strHopperSetting = GetSettingFromDB("CDM_SHOW_COIN_HOPPER");

                return strHopperSetting;
            }
            catch (Exception ex)
            {
                strHopperSetting = "";
                return strHopperSetting;
            }
        }
        //
        public bool IsHopperSetting()
        {
            bool _IsHopperSetting = false;

            try
            {
                _IsHopperSetting = Convert.ToBoolean(GetSettingFromDB("CDM_SHOW_COIN_HOPPER"));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return _IsHopperSetting;
        }
        //
        public bool IsRegulatoryEnabled()
        {
            bool _IsRegulatoryEnabled = false;

            try
            {
                _IsRegulatoryEnabled = Convert.ToBoolean(GetSettingFromDB("IsRegulatoryEnabled"));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return _IsRegulatoryEnabled;
        }



        /// <summary>
        /// Get the settings for CMP Kiosk
        /// </summary>
        /// <param name="sqlparams"></param>
        /// <param name="strConnect"></param>
        /// <returns >string</returns>
        private  string GetSettingFromDB(string strSetting)
        {
            string strReturnValue = string.Empty;
            try
            {
                SqlParameter[] sqlparams = GetSettingParameterDB(strSetting);
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
        private  SqlParameter[] GetSettingParameterDB(string SettingName)
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
        private  SqlConnection SQLConnection()
        {
            return (new SqlConnection(GetExchangeConnectionString()));
        }

        private  SqlConnection TicketSQLConnection()
        {
            string TicketingConnectionstring = DatabaseHelper.GetTicketingConnectionString();
            return (new SqlConnection(TicketingConnectionstring));
        }

       public  string HopperSetting()
            {
                return (GetHopperSetting());
            }



        #endregion Common Functionalities

        #region "Get Tickets Claimed"
        /// <summary>
        /// Method to get the tickets claimed.
        /// </summary>
        /// <param name=""></param>;
        /// <returns>TicketsClaimed</returns>
        /// Method Revision History
        /// Author:    Anuradha
        /// Created:   28 April 2009
        /// 
        private  DataTable GetTicketsClaimed(TicketsClaimed oTickets)
        {

            return ExecuteTable(DBConstants.CONST_SP_GET_TICKETS_CLAIMED, oTickets);
        }


        private  DataTable ExecuteTable(string ProcedureName, TicketsClaimed oTickets)
        {
            try
            {
                return SqlHelper.ExecuteDataset(SQLConnection(), ProcedureName, GetSpParameters(oTickets)).Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
               
            }
        }

        private DataTable ExecuteTable(TicketsClaimed oTickets)
        {
            try
            {
                return SqlHelper.ExecuteDataset(GetTicketingConnectionString(), DBConstants.CONST_SP_RSP_GETPROMOTICKETFORPERIODDETAILS, GetSpParameters(oTickets)).Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;

            }
        }

        private SqlDataReader ExecuteReader(string StoredProcedure, TicketsClaimed oTickets)
        {
            try
            {
                return SqlHelper.ExecuteReader(SQLConnection(), StoredProcedure, CommandType.StoredProcedure, GetSpParameters(oTickets));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;

            }
        }


        private DataTable ExecuteTable(string StoredProcedure, Tickets oTickets)
        {
            try
            {
                return SqlHelper.ExecuteDataset(TicketSQLConnection(), StoredProcedure, GetVoucherParameters(oTickets)).Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;

            }
            // return SqlHelper.ExecuteDataset(SQLConnection(), StoredProcedure,CommandType.StoredProcedure, GetVoucherParameters(oTickets)).Tables[0];
        }

        private DataTable ExecuteTable(Tickets oTickets, bool flag)
        {
            try
            {
                return SqlHelper.ExecuteDataset(SQLConnection(), DBConstants.CONST_SP_RSP_GETTREASURYITEMS, GetTreasuryParameters(oTickets)).Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;

            }

            // return SqlHelper.ExecuteDataset(SQLConnection(), StoredProcedure,CommandType.StoredProcedure, GetVoucherParameters(oTickets)).Tables[0];
        }


        private object[] GetSpParameters(TicketsClaimed oTicketsClaimed)
        {
            try
            {
                SqlParameter[] ObjParams = new SqlParameter[2];
                ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_STARTDATE, oTicketsClaimed.TicketsClaimedFrom);
                ObjParams[1] = new SqlParameter(DBConstants.CONST_PARAM_ENDDATE, oTicketsClaimed.TicketsClaimedTo);

                return ObjParams;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;

            }
        }
        private object[] GetTicketsParameters(Tickets oTickets)
        {
            try
            {
                SqlParameter[] ObjParams = new SqlParameter[4];
                ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_STARTDATE, oTickets.StartDate);
                ObjParams[1] = new SqlParameter(DBConstants.CONST_PARAM_ENDDATE, oTickets.EndDate);
                ObjParams[2] = new SqlParameter(DBConstants.CONST_PARAM_TYPE, oTickets.Type);
                ObjParams[3] = new SqlParameter(DBConstants.CONST_PARAM_LIABILITY, oTickets.IsLiability);

                return ObjParams;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;

            }
        }

        private object[] GetVoucherParameters(Tickets oTickets)
        {
            try
            {
                SqlParameter[] ObjParams = new SqlParameter[6];
                ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_STARTDATE, oTickets.StartDate);
                ObjParams[1] = new SqlParameter(DBConstants.CONST_PARAM_ENDDATE, oTickets.EndDate);
                ObjParams[2] = new SqlParameter(DBConstants.CONST_PARAM_TYPE, oTickets.Type);
                ObjParams[3] = new SqlParameter(DBConstants.CONST_PARAM_BARCODE, oTickets.BarCode);
                ObjParams[4] = new SqlParameter(DBConstants.CONST_PARAM_LIABILITY, Convert.ToInt16(oTickets.IsLiability));
                ObjParams[5] = new SqlParameter("@User", oTickets.UserNo);

                return ObjParams;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;

            }
        }

        private object[] GetTreasuryParameters(Tickets oTickets)
        {
            try
            {
                SqlParameter[] ObjParams = new SqlParameter[3];
                ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_STARTDATE, oTickets.StartDate);
                ObjParams[1] = new SqlParameter(DBConstants.CONST_PARAM_ENDDATE, oTickets.EndDate);
                ObjParams[2] = new SqlParameter(DBConstants.CONST_PARAM_TYPE, oTickets.Type);

                return ObjParams;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;

            }
        }


        #endregion

        #region FillRouteFilter
        public  Dictionary<string, string> GetRoutes()
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
        public  List<string> GetFilteredPositions(string RouteNumber)
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

        public  DataTable GetTickets(Tickets oTickets)
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
        public  string GetBarPositionFromAsset(string sMachine)
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

                if (string.IsNullOrEmpty(strPosName) && !string.IsNullOrEmpty(sMachine))
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

        private  string FindMachine(string sMachine)
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

        public  Dictionary<string, string> LoadTicketWorkstations()
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

        public  List<TicketExceptions> TitoTicketOutExceptions(Tickets oTickets, List<string> lstPositionstoDisplay)
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
                    excep.Machine = GetBarPositionFromAsset(reader["bar_pos_name"].ToString());
                    excep.currValue = (float)Convert.ToDouble(reader["TE_Value"]) / 100;
                    if (!string.IsNullOrEmpty(excep.SEGM) && DBCommon.CheckPositionToDisplay(excep.Machine, lstPositionstoDisplay))
                    {
                        excep.bExceptionRecordFound = true;
                        excep.Type = "OUT";
                        excep.Position = GetBarPositionFromAsset(reader["bar_pos_name"].ToString());
                        excep.PrintDate = reader["TE_Date"].ToString().ToShortDateTimeString();
                        //    +" " +                            Convert.ToDateTime(reader["TE_Date"]).ToString("HH:mm");
                        if (!string.IsNullOrEmpty(reader["TE_TicketNumber"].ToString()))
                        {
                            excep.Ticket = reader["ActualBarcode"].ToString();
                        }
                        else
                        {
                            excep.Ticket = reader["TE_TicketNumber"].ToString();
                        }
                        excep.Value = Convert.ToDouble(reader["TE_Value"]) / 100;
                     //   excep.Amount = excep.Value.ToString("###0.#0");
                        excep.Amount = CurrencySymbol + " " + Convert.ToDecimal(excep.Value).GetUniversalCurrencyFormat();
                        excep.Asset = reader["Asset"].ToString();
                        excep.COLINSTALLID = Convert.ToInt16(reader["TE_Installation_No"].ToString());
                        excep.CreateCompleted = reader["CreateExpected"].ToString();

                        excep.cTicketTotal += excep.currValue;
                        excep.cExceptionsTotal += excep.currValue;
                    }
                    else if (DBCommon.IsMachineATicketWorkstation(excep.Machine))
                    {
                        excep.bExceptionRecordFound = true;
                        excep.Type = "OUT";
                        excep.Position = GetBarPositionFromAsset(reader["bar_pos_name"].ToString());
                        excep.PrintDate = reader["TE_Date"].ToString().ToShortDateTimeString();
                         //   + " " +                            Convert.ToDateTime(reader["TE_Date"]).ToString("HH:mm");
                        if (!string.IsNullOrEmpty(reader["TE_TicketNumber"].ToString()))
                        {
                            excep.Ticket = reader["ActualBarcode"].ToString();
                        }
                        else
                        {
                            excep.Ticket = reader["TE_TicketNumber"].ToString();
                        }
                        excep.Value = Convert.ToDouble(reader["TE_Value"]) / 100;
                        //excep.Amount = excep.Value.ToString("###0.#0");
                        excep.Amount = CurrencySymbol + " " + Convert.ToDecimal(excep.Value).GetUniversalCurrencyFormat();
                        excep.Asset = reader["Asset"].ToString();
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

        public  List<TicketExceptions> TitoTicketsClaimed(Tickets oTickets, List<string> lstPositionstoDisplay)
        {
            string strTicketInException = string.Empty;
            string sPosTer = string.Empty;
            List<TicketExceptions> lstTickets = null;
            TicketExceptions excep = null;

            try
            {
                DataTable dtTickets = GetTickets(oTickets);
                if (dtTickets == null || dtTickets.Rows.Count < 0)
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
                                if (DBCommon.CheckPositionToDisplay(GetBarPositionFromAsset(row["PrintDevice"].ToString()), 
                                    lstPositionstoDisplay))
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
                            excep.Machine = GetBarPositionFromAsset(excep.Machine);
                            // excep.Machine = sPosTer;
                            if (excep.MachineClaimedQty > 0)
                                excep.TransactionType = "M/c Voucher Claimed";
                            else if (excep.CashDeskClaimedQty > 0)
                                excep.TransactionType = "C/D Voucher Claimed";
                            
                            excep.Zone = "n/a";

                            excep.Asset = row["Asset"].ToString();

                            excep.PrintDate = row["dtPrinted"].ToString().ToShortDateTimeString();


                            if (!(row["dtPaid"] == null))
                            {
                                excep.PayDate = Convert.ToDateTime(row["dtPaid"]) != null ? row["dtPaid"].ToString().ToShortDateTimeString() :
                                    string.Empty;
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
                            //excep.Amount = (-(excep.Value)).ToString("###0.#0");
                            excep.Amount = CurrencySymbol + " " + Convert.ToDecimal(-excep.Value).GetUniversalCurrencyFormat();

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


        public  List<TicketExceptions> TitoTicketsClaimedLiability(Tickets oTickets, List<string> lstPositionstoDisplay)
        {
            string strTicketInException = string.Empty;
            string sPosTer = string.Empty;
            List<TicketExceptions> lstTickets = null;
            TicketExceptions excep = null;

            try
            {
                DataTable dtTickets = GetTickets(oTickets);
                if (dtTickets == null || dtTickets.Rows.Count < 0)
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
                        excep.VoucherStatus = row["strVoucherStatus"].ToString();
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
                            excep.VoucherStatus = row["strVoucherStatus"].ToString();


                            excep.Position = GetBarPositionFromAsset(row["PrintDevice"].ToString());
                            excep.TransactionType = "Voucher Claimed";
                            excep.Zone = "n/a";

                            excep.PrintDate = row["dtPrinted"].ToString().ToShortDateTimeString();
                               // + " " +           Convert.ToDateTime(row["dtPrinted"]).ToString("HH:mm");

                            if (!(row["dtPaid"] == null))
                            {
                                excep.PayDate = row["dtPaid"].ToString().ToShortDateTimeString();
                                    //+" " + Convert.ToDateTime(row["dtPaid"] != null ? row["dtPaid"] : string.Empty).ToString("HH:mm");
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
                          //  excep.Amount = (-(excep.Value)).ToString("###0.#0");
                            excep.Amount = CurrencySymbol + " " + Convert.ToDecimal(-excep.Value).GetUniversalCurrencyFormat();

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
                            excep.Asset = row["Asset"].ToString();
                            lstTickets.Add(excep);
                        }
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

        public  List<TicketExceptions> TitoTicketsPrinted(Tickets oTickets, List<string> lstPositionstoDisplay)
        {
            string strTicketInException = string.Empty;
            string sPosTer = string.Empty;
            List<TicketExceptions> lstTickets = null;
            TicketExceptions excep = null;
            double CurrentCashDesk = 0, currEGM = 0;

            try
            {
                DataTable dtTickets = GetTickets(oTickets);
                if (dtTickets == null || dtTickets.Rows.Count < 0)
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
                            excep.Value = Convert.ToDouble(row["iAmount"]) / 100;
                            //excep.Amount = excep.Value.ToString("###0.#0");
                            excep.Amount = CurrencySymbol + " " + Convert.ToDecimal(excep.Value).GetUniversalCurrencyFormat();
                            string dPrintDate = row["dtPrinted"].ToString().ToShortDateTimeString();
                            string dClaimDate = string.IsNullOrEmpty(row["dtPaid"].ToString()) ? "01/01/2100" : row["dtPaid"].ToString();

                            if(excep.MachinePrintedQty > 0)
                                excep.TransactionType = "M/c Voucher Issued";
                            else if(excep.CashDeskPrintedQty > 0)
                                excep.TransactionType = "C/D Voucher Issued";
                            
                            excep.Zone = "n/a";
                            excep.Asset = row["Asset"].ToString();


                            if ((Convert.ToDateTime(row["dtPrinted"].ToString()) >= Convert.ToDateTime(oTickets.StartDate)) &&
                                (Convert.ToDateTime(string.IsNullOrEmpty(row["dtPaid"].ToString()) ? DateTime.Now.ToString() : row["dtPaid"].ToString())
                                <= Convert.ToDateTime(oTickets.EndDate)))
                            {
                                excep.Status = "Printed within, Claimed in different category";
                            }
                            else if (string.IsNullOrEmpty(row["dtPaid"].ToString()))
                            {
                                excep.Status = "Active Ticket";
                            }
                            else
                            {
                                excep.Value = -excep.Value;
                                //  excep.Amount = excep.Value.ToString("###0.#0");
                                excep.Amount = CurrencySymbol + " " + Convert.ToDecimal(excep.Value).GetUniversalCurrencyFormat();
                                excep.Status = "Printed within period, claimed later";
                            }
                            excep.Position = sPosTer;


                            excep.PrintDate = row["dtPrinted"].ToString().ToShortDateTimeString();
                          //  excep.PrintDate = row["dtPrinted"].ToShortDateTimeString();
                         
                            if (!(row["dtPrinted"] == null))
                            {
                                excep.PayDate = Convert.ToDateTime(row["dtPrinted"]) != null ?  row["dtPrinted"].ToString().ToShortDateTimeString() :
                                    string.Empty;
                             }
                            excep.PayDate = Convert.ToDateTime(row["dtPrinted"]) != null ? row["dtPrinted"].ToString().ToShortDateTimeString() :
                                    string.Empty;
                          
                            excep.PayDevice = (row["PayDevice"] != null ? row["PayDevice"].ToString() : string.Empty);

                            if (!string.IsNullOrEmpty(row["strBarCode"].ToString()))
                            {
                                excep.Ticket = row["ActualBarcode"].ToString();
                            }
                            else
                            {
                                excep.Ticket = row["strBarcode"].ToString();
                            }
                            // excep.Details = excep.Ticket;
                            excep.Details = string.Empty;
                         
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


        public  List<TicketExceptions> TitoTicketsPrintedLiability(Tickets oTickets, List<string> lstPositionstoDisplay)
        {
            string strTicketInException = string.Empty;
            string sPosTer = string.Empty;
            List<TicketExceptions> lstTickets = null;
            TicketExceptions excep = null;

            try
            {
                DataTable dtTickets = GetTickets(oTickets);
                if (dtTickets == null || dtTickets.Rows.Count < 0)
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

                            excep.Value = excep.currValue;
                      // excep.Amount =      excep.Value.ToString("###0.#0");
                            excep.Amount = CurrencySymbol + " " + Convert.ToDecimal(excep.Value).GetUniversalCurrencyFormat();
                            string dPrintDate = row["dtPrinted"].ToString();
                            string dClaimDate = string.IsNullOrEmpty(row["dtPaid"].ToString()) ? "01/01/3100" : row["dtPaid"].ToString();
                            excep.VoucherStatus = row["strVoucherStatus"].ToString();
                            excep.TransactionType = "Voucher Printed";
                            excep.Zone = "n/a";

                            if (!string.IsNullOrEmpty(row["strBarCode"].ToString()))
                            {

                                excep.Ticket = row["ActualBarcode"].ToString();
                            }
                            else
                            {

                                excep.Ticket = row["strBarcode"].ToString();
                            }


                            if ((Convert.ToDateTime(dPrintDate) >= Convert.ToDateTime(oTickets.StartDate)) &&
                                (Convert.ToDateTime(dClaimDate) <= Convert.ToDateTime(oTickets.EndDate)))
                            {
                                excep.Status = "Printed within, Claimed in different category";
                            }
                            else if (string.IsNullOrEmpty(row["dtPaid"].ToString()) && (Convert.ToDateTime(row["dtExpire"]) > oTickets.EndDate))
                            {
                                excep.Status = "Active Vouchers";
                                excep.Ticket = string.Empty;
                            }
                            else if (string.IsNullOrEmpty(row["dtPaid"].ToString()) && (Convert.ToDateTime(row["dtExpire"]) < oTickets.EndDate))
                            {
                                if (string.IsNullOrEmpty(row["strVoucherStatus"].ToString()) && Convert.ToBoolean(GetSettingFromDB("RedeemExpiredTicket")))
                                {
                                    excep.Ticket = string.Empty;
                                }
                                else
                                {
                                    excep.Ticket = row["strBarcode"].ToString();
                                }
                                excep.Status = "Expired Vouchers";
                            }
                            else
                            {
                                //  excep.Status = "Printed within period, claimed later";
                                excep.Value = -excep.Value;
                                //excep.Amount = excep.Value.ToString("###0.#0");
                                excep.Amount = CurrencySymbol + " " + Convert.ToDecimal(excep.Value).GetUniversalCurrencyFormat();
                                excep.Status = "Printed within period, claimed later";
                            }

                            excep.Position = GetBarPositionFromAsset(row["PrintDevice"].ToString());

                            excep.PrintDate = row["dtPrinted"].ToString().ToShortDateTimeString();
                                //+ " " +   Convert.ToDateTime(row["dtPrinted"]).ToString("HH:mm");

                            if (!(row["dtPaid"] == null))
                            {
                                if (row["dtPaid"] == DBNull.Value)
                                {
                                    excep.PayDate = string.Empty;
                                }
                                else
                                {
                                    excep.PayDate = row["dtPaid"].ToString().ToShortDateTimeString();
                                        //+" " +  Convert.ToDateTime(row["dtPaid"]).ToString("HH:mm");
                                }
                            }


                            excep.PayDevice = ((row["PayDevice"] != null )? GetBarPositionFromAsset(row["PayDevice"].ToString()) : string.Empty);
                            excep.Asset = row["Asset"].ToString();
                           
                            lstTickets.Add(excep);
                        }
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
        public  List<TicketExceptions> TicketsClaimed(TicketsClaimed oTickets, List<string> lstPositionstoDisplay)
        {
            string strTicketInException = string.Empty;
            string sPosTer = string.Empty;
            List<TicketExceptions> lstTickets = null;
            TicketExceptions excep = null;

            try
            {
                DataTable dtTicketsClaimed = GetTicketsClaimed(oTickets);
                if (dtTicketsClaimed != null || dtTicketsClaimed.Rows.Count > 0)
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
                                excep.Position = GetBarPositionFromAsset(row["TBR_Payout_Machine_Serial"].ToString());

                                excep.PrintDate = row["TBR_Payout_Print_Time"].ToString().ToShortDateTimeString();
                                //    +" " +                                    Convert.ToDateTime(row["TBR_Payout_Print_Time"]).ToString("HH:mm");

                                if (!(row["TBR_Payout_Time"] == null))
                                {
                                    if (Convert.ToDateTime(row["TBR_Payout_Time"]) != null)
                                    {
                                        excep.PayDate = row["TBR_Payout_Time"].ToString().ToShortDateTimeString();
                                    }
                                    else
                                    {
                                        excep.PayDate =  string.Empty;
                                    }
                                }

                                excep.ClaimedTerminal = GetBarPositionFromAsset(row["TBR_Payout_Claimed_Terminal"].ToString());
                                excep.Ticket = row["TBR_Payout_ExternalIndex"].ToString();
                                excep.Status = "Printed prior to period, Claimed within";

                                excep.Value = -(Convert.ToDouble(row["tbr_payout_value"]) / 100);
                               // excep.Amount = excep.Value.ToString("###0.#0") ;
                                excep.Amount = CurrencySymbol + " " + Convert.ToDecimal(excep.Value).GetUniversalCurrencyFormat();

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

        public  List<TicketExceptions> TicketsPrinted(TicketsClaimed oTickets, List<string> lstPositionstoDisplay)
        {
            string strTicketInException = string.Empty;
            string sPosTer = string.Empty;
            List<TicketExceptions> lstTickets = null;
            TicketExceptions excep = null;

            try
            {
                DataTable dtTicketsClaimed = GetTicketsPrinted(oTickets);
                if (dtTicketsClaimed != null || dtTicketsClaimed.Rows.Count > 0)
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
                                excep.Position = GetBarPositionFromAsset(row["TBR_Payout_Machine_Serial"].ToString());

                                excep.PrintDate = row["TBR_Payout_Print_Time"].ToString().ToShortDateTimeString();
                                
                                if (!(row["TBR_Payout_Time"] == null))
                                {
                                    if (Convert.ToDateTime(row["TBR_Payout_Time"]) != null)
                                    {
                                        excep.PayDate = row["TBR_Payout_Time"].ToString().ToShortDateTimeString();
                                    }
                                    else
                                    {
                                        excep.PayDate = string.Empty;
                                    } Convert.ToDateTime(row["TBR_Payout_Time"] != null ? row["TBR_Payout_Time"] : string.Empty).ToString("HH:mm");
                                }

                                excep.ClaimedTerminal = GetBarPositionFromAsset(row["TBR_Payout_Claimed_Terminal"].ToString());
                                excep.Ticket = row["TBR_Payout_ExternalIndex"].ToString();
                                excep.Status = "Printed prior to period, Claimed within";

                                excep.Value = Convert.ToDouble(row["tbr_payout_value"]) / 100;
                              //  excep.Amount = excep.Value.ToString("###0.#0") ;
                                excep.Amount = CurrencySymbol + " " + Convert.ToDecimal(excep.Value).GetUniversalCurrencyFormat();
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

        private  DataTable GetTicketsPrinted(TicketsClaimed oTickets)
        {
            return ExecuteTable(DBConstants.CONST_SP_GET_TICKETS_PRINTED, oTickets);
        }


        public  List<TicketExceptions> TicketsUnClaimed(TicketsClaimed oTickets, List<string> lstPositions)
        {
            string strTicketInException = string.Empty;
            List<TicketExceptions> lstTickets = null;
            TicketExceptions excep = null;


            try
            {
                DataTable dtTickets = ExecuteTable(DBConstants.CONST_SP_GET_TICKETS_UNCLAIMED, oTickets);
                if (dtTickets == null || dtTickets.Rows.Count < 0)
                {
                }
                else
                {
                    lstTickets = new List<TicketExceptions>();

                    foreach (DataRow row in dtTickets.Rows)
                    {
                        excep = new TicketExceptions();
                        excep.SEGM = row["bar_pos_name"].ToString();
                        excep.Machine = GetBarPositionFromAsset(row["bar_pos_name"].ToString());

                        if (DBCommon.CheckPositionToDisplay(excep.Machine, lstPositions))
                        {
                            excep.Position = GetBarPositionFromAsset(row["PrintDevice"].ToString());
                            excep.Machine = row["bar_pos_name"].ToString() + " - " + row["tbr_node_serial"].ToString();
                            excep.TransactionType = "Ticket";
                            excep.Zone = row["Zone_Name"] != null ? row["Zone_Name"].ToString() : string.Empty;

                            excep.PrintDate = row["TBR_Payout_Time"].ToString().ToShortDateTimeString();

                            excep.PayDate = row["TBR_Payout_Time"].ToString().ToShortDateTimeString();
                        
                            excep.SEGM = row["machine_name"].ToString();

                            excep.Value = Convert.ToDouble(row["tbr_payout_value"]) / 100;
                            excep.Amount = CurrencySymbol + " " + (Convert.ToDecimal(row["tbr_payout_value"]) / 100).GetUniversalCurrencyFormat();

                            excep.cExceptionsTotal += (float)excep.Value;
                        }
                        else if (DBCommon.IsMachineATicketWorkstation(row["PrintDevice"].ToString()))
                        {
                            excep.Position = GetBarPositionFromAsset(row["PrintDevice"].ToString());
                            excep.Machine = row["bar_pos_name"].ToString() + " - " + row["tbr_node_serial"].ToString();
                            excep.TransactionType = "Ticket";
                            excep.Zone = row["Zone_Name"] != null ? row["Zone_Name"].ToString() : string.Empty;

                            excep.PrintDate = row["TBR_Payout_Time"].ToString().ToShortDateTimeString();

                            excep.PayDate = row["TBR_Payout_Time"].ToString().ToShortDateTimeString();

                            excep.SEGM = row["machine_name"].ToString();

                            excep.Value = Convert.ToDouble(row["tbr_payout_value"]) / 100;
                            excep.Amount = CurrencySymbol + " " + (Convert.ToDecimal(row["tbr_payout_value"]) / 100).GetUniversalCurrencyFormat();


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
        public  DataTable GetPromoTickets(TicketsClaimed oTickets)
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

        public  List<TicketExceptions> RetrieveTicketAnomalies(TicketsClaimed oTickets, List<string> lstPositions)
        {
            string strTicketInException = string.Empty;
            List<TicketExceptions> lstTickets = null;
            TicketExceptions excep = null;


            try
            {
                DataTable dtTickets = ExecuteTable(DBConstants.CONST_SP_RSP_TICKET_ANOMALIES, oTickets);
                if (dtTickets == null || dtTickets.Rows.Count < 0)
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
                        excep.Machine = GetBarPositionFromAsset(row["Machine"].ToString());

                        if (!string.IsNullOrEmpty(excep.SEGM) &&
                            DBCommon.CheckPositionToDisplay(excep.Machine, lstPositions))
                        {
                            excep.Reference = row["Reference"].ToString();
                            excep.Value = Convert.ToDouble(row["Amount"]);
                            excep.Amount = CurrencySymbol + " " + Convert.ToDecimal(excep.Value).GetUniversalCurrencyFormat();

                            excep.PrintDate = row["dtDate"].ToString().ToShortDateTimeString();
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

        public  List<TicketExceptions> GetTreasuryItems(Tickets oTickets, List<string> lstPositions)
        {
            List<TicketExceptions> items = null;
            TicketExceptions treasury = null;

            try
            {
                DataTable dtTreasury = ExecuteTable(oTickets, false);

                items = new List<TicketExceptions>();
                foreach (DataRow row in dtTreasury.Rows)
                {

                    if (DBCommon.CheckPositionToDisplay(row["bar_pos_name"].ToString(), lstPositions))
                    {


                        treasury = new TicketExceptions();
                        //treasury.PayDate = row["treasury_date"].ToString() + " " + row["treasury_time"].ToString();
                        //treasury.PrintDate = row["treasury_date"].ToString() + " " + row["treasury_time"].ToString();
                        //treasury.PayDate = Convert.ToDateTime(row["treasury_date"]).ToString("dd MMM yyyy HH:mm:ss").ReadDateTimeWithSeconds().ToString();
                        //treasury.PrintDate = Convert.ToDateTime(row["treasury_date"]).ToString("dd MMM yyyy HH:mm:ss").ReadDateTimeWithSeconds().ToString();

                        treasury.PayDate = row["treasury_date"].ToString().ToShortDateTimeString();
                        treasury.PrintDate = row["treasury_date"].ToString().ToShortDateTimeString();



                        treasury.TransactionType = oTickets.Type;
                        treasury.Zone = row["Zone_name"].ToString();
                        treasury.Position = row["bar_pos_name"].ToString();
                        treasury.Machine = row["machine_name"].ToString();
                        //treasury.Amount = Convert.ToDouble(row["treasury_amount"]).ToString("###0.#0");
                        treasury.Amount = CurrencySymbol + " " + Convert.ToDecimal(row["treasury_amount"]).GetUniversalCurrencyFormat();
                        treasury.currValue = Convert.ToInt64(row["treasury_amount"]);
                        treasury.Value = Convert.ToDouble(row["treasury_amount"]);
                        treasury.Reason = row["treasury_reason"].ToString();
                        treasury.ReasonCode = row["treasury_reason_code"].ToString();
                        treasury.Asset = row["Asset"].ToString();
						treasury.TreasuryTemp = Convert.ToBoolean(row["Treasury_Temp"].ToString());
                        treasury.Details = "";
                        

                        if (string.IsNullOrEmpty(treasury.Reason))
                        {
                            switch (oTickets.Type)
                            {
                                case DBConstants.CONST_HANDPAYCREDIT:
                                    {

                                        treasury.HandpayQty += 1;
                                        treasury.TransactionType = row["TreasuryTypeDisplayText"].ToString();
                                        break;
                                    }
                                case DBConstants.CONST_JACKPOT:
                                    {
                                        treasury.JackPotQty += 1;
                                        treasury.TransactionType = row["TreasuryTypeDisplayText"].ToString();
                                        break;
                                    }
                                case DBConstants.CONST_PROG:
                                    {
                                        treasury.ProgQty += 1;
                                        treasury.TransactionType = row["TreasuryTypeDisplayText"].ToString();
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

        public  string GetRegionFromSite()
        {
            string Query = "Select Region = Case Upper(Region) When 'US' Then 'US'	WHEN 'UK' THEN  'UK' ELSE '' End  From Site";

            object objRegion = SqlHelper.ExecuteScalar(SQLConnection(), CommandType.Text, Query);
            if (objRegion != null)
            {
                return (objRegion.ToString());
            }
            else
            {
                return string.Empty;
            }
        }

        public bool ClearTicketStatus(string Ticket,string DeviceID)
        {
            // SDGOneConnect.TheOneConnection oSDG =new SDGOneConnect.TheOneConnection();
   
            //try
            //{
            //    //if (!oSDG.ConnectedToServer())
            //    //{
            //    //    LogManager.WriteLog("Could not estabilish connection to ticket DB...", LogManager.enumLogLevel.Info);
            //    //    return false;
            //    //}
            //    if (oSDG.ResetTicketPayment(ref  Ticket, ref  DeviceID))
            //    {
            //        InsertException(0, "Ticket Status Cleared", Ticket, "204");
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionManager.Publish(ex);
            //}
            return false;
        }

        public Dictionary<string,bool> ActivateSDGTicket(string Ticket, string DeviceID,bool iStatus)
        {
            Dictionary<string, bool> dResult = new Dictionary<string, bool>();

            try
            {

                string strTicketConnectionString = DatabaseHelper.GetTicketingConnectionString();

                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@strBarcode", Ticket);
                parameters[0].Direction = ParameterDirection.Input;

                parameters[1] = new SqlParameter("@strDeviceID", DeviceID);
                parameters[1].Direction = ParameterDirection.Input;

                parameters[2] = new SqlParameter("@iStatus", iStatus);
                parameters[2].Direction = ParameterDirection.Input;

                parameters[3] = new SqlParameter("@iResult", 0);
                parameters[3].Direction = ParameterDirection.Output;


                SqlHelper.ExecuteNonQuery(strTicketConnectionString, CommandType.StoredProcedure, "pActivateSDGTicket", parameters);
                int Result = Convert.ToInt32(parameters[3].Value);

                switch (Result)
                {
                    case 0: // Success
                        {
                            dResult.Add("Success", true);
                            break;
                        }

                    case -1: //= Voucher already closed
                        {
                            dResult.Add("MessageID329", false);
                            break;
                        }
                    //  LogMessage PROC, ErrorDescription

                    case -2:
                        {//= ticket not found or
                            dResult.Add("MessageID331", false);
                            break;
                        }
                    // LogMessage PROC, ErrorDescription

                    case -3:
                        {//= Invalid Device for Reset
                            dResult.Add("MessageID330", false);
                            break;
                        }
                    // LogMessage PROC, ErrorDescription

                    default:
                        {
                            dResult.Add("MessageID332", false);
                            break;
                        }
                    // LogMessage PROC, "UNKNOWN RETURN (" & nResult & ")"
                }
                return dResult;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null ;
            }
           


        }


        private void InsertException(int InstallationNo,string Type,string Details,string Ticket)
        {
            try
            {
                SqlParameter[] parames = SqlHelperParameterCache.GetSpParameterSet(GetExchangeConnectionString(), "usp_InsertException");
                foreach (SqlParameter par in parames)
                {
                    if (par.ParameterName == "@Installation_ID") { par.Value = InstallationNo; }
                    if (par.ParameterName == "@Exception_Type") { par.Value = Type; }
                    if (par.ParameterName == "@Details") { par.Value = Details; }
                    if (par.ParameterName == "@Reference") { par.Value = Ticket; }
                    if (par.ParameterName == "@User") { par.Value = BMC.Security.SecurityHelper.CurrentUser; }

                }
                SqlHelper.ExecuteNonQuery(GetExchangeConnectionString(), CommandType.StoredProcedure, "usp_InsertException", parames);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
           
        }

        public List<User> GetListOfUsers(int UserNo)
        {

            try
            {
                DataSet ds = SqlHelper.ExecuteDataset(GetExchangeConnectionString(), CommandType.StoredProcedure, "rsp_GetUsers", new SqlParameter ("@User" ,UserNo));

                var returnList = from row in ds.Tables[0].AsEnumerable()
                                 select new User
                                 {
                                     UserName = row.Field<string>("UserName"),
                                     UserNo = row.Field<int>("UserNo"),
                                     SecurityUserID = row.Field<int>("SecurityUserID")
                                     //RoleAccessName = row.Field<string>("RoleAccessName")

                                 };

                return returnList.ToList();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public List<User> GetListOfUsersRoles(int UserNo)
        {

            try
            {
                DataSet ds = SqlHelper.ExecuteDataset(GetExchangeConnectionString(), CommandType.StoredProcedure, "rsp_GetUsersRoleBased", new SqlParameter("@User", UserNo));

                var returnList = from row in ds.Tables[0].AsEnumerable()
                                 select new User
                                 {
                                     RoleaccessID = row.Field<int>("RoleaccessID"),
                                     RoleAccessName = row.Field<string>("RoleAccessName"),
                                     SecurityUserID = row.Field<int>("SecurityUserID"),
                                     RoleName = row.Field<string>("RoleName"),
                                     UserNo = row.Field<int>("UserNo")
                                 };

                return returnList.ToList();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }
    }

    
}
