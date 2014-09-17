using System;
using System.Collections.Generic;
using BMC.Common.Utilities;
using Microsoft.Win32;
using System.Data.SqlClient;
using BMC.Common.ExceptionManagement;
using System.Data;
using BMC.DataAccess;
using System.Data.Linq;
using BMC.Common.LogManagement;
using System.Linq;
using BMC.EnterpriseDataAccess;


namespace BMC.EnterpriseDataAccess.CashierTransations
{
    public class CashDeskManagerDataAccess
    {
        #region "Declarations"
        Dictionary<string, string> CollectionMachines = null;
        Dictionary<string, string> CollectionZONES = null;
        #endregion

        #region Common Functionalities

        public static string GetConnectionString()
        {
            try
            {
                return DatabaseHelper.GetConnectionString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return "";
            }
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
                ExceptionManager.Publish(ex);
                strHopperSetting = "";
                return strHopperSetting;
            }
        }


        /// <summary>
        /// Get the settings for CMP Kiosk
        /// </summary>
        /// <param name="sqlparams"></param>
        /// <param name="strConnect"></param>
        /// <returns >string</returns>
        public string GetSettingFromDB(string strSetting)
        {
            string strReturnValue = string.Empty;
            try
            {
                SqlParameter[] sqlparams = GetSettingParameterDB(strSetting);
                SqlHelper.ExecuteNonQuery(GetConnectionString(), System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_GETSETTING, sqlparams);
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
        private SqlParameter[] GetSettingParameterDB(string SettingName)
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



                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return sp_parames;
        }


        private SqlConnection SQLConnection()
        {
            return (new SqlConnection(GetConnectionString()));
        }

        private SqlConnection TicketSQLConnection()
        {
            return (new SqlConnection(GetConnectionString()));
        }

        public string HopperSetting()
        {
            return (GetHopperSetting());
        }



        #endregion Common Functionalities

        #region "Get Tickets Claimed"

        private DataTable GetTicketsClaimed(TicketsClaimed oTickets)
        {

            return ExecuteTable(DBConstants.CONST_SP_GET_TICKETS_CLAIMED, oTickets);
        }


        private DataTable ExecuteTable(string ProcedureName, TicketsClaimed oTickets)
        {
            return SqlHelper.ExecuteDataset(GetConnectionString(), ProcedureName, GetSpParameters(oTickets)).Tables[0];
        }

        private DataTable ExecuteTable(TicketsClaimed oTickets)
        {
            return SqlHelper.ExecuteDataset(GetConnectionString(), DBConstants.CONST_SP_RSP_GETPROMOTICKETFORPERIODDETAILS, GetSpParameters(oTickets)).Tables[0];
        }

        private SqlDataReader ExecuteReader(string StoredProcedure, TicketsClaimed oTickets)
        {
            return SqlHelper.ExecuteReader(SQLConnection(), StoredProcedure, CommandType.StoredProcedure, GetSpParameters(oTickets));
        }


        private DataTable ExecuteTable(string StoredProcedure, Tickets oTickets)
        {
            return SqlHelper.ExecuteDataset(TicketSQLConnection(), StoredProcedure, GetVoucherParameters(oTickets)).Tables[0];
            // return SqlHelper.ExecuteDataset(SQLConnection(), StoredProcedure,CommandType.StoredProcedure, GetVoucherParameters(oTickets)).Tables[0];
        }

        private DataTable ExecuteTable(string ExchangeConnectionString, Tickets oTickets, bool flag)
        {
            return SqlHelper.ExecuteDataset(ExchangeConnectionString, DBConstants.CONST_SP_RSP_GETTREASURYITEMS,
                GetTreasuryParameters(oTickets)).Tables[0];
            // return SqlHelper.ExecuteDataset(SQLConnection(), StoredProcedure,CommandType.StoredProcedure, GetVoucherParameters(oTickets)).Tables[0];
        }


        private object[] GetSpParameters(TicketsClaimed oTicketsClaimed)
        {
            SqlParameter[] ObjParams = new SqlParameter[3];
            ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_STARTDATE, oTicketsClaimed.TicketsClaimedFrom);
            ObjParams[1] = new SqlParameter(DBConstants.CONST_PARAM_ENDDATE, oTicketsClaimed.TicketsClaimedTo);
            ObjParams[2] = new SqlParameter(DBConstants.CONST_PARAM_ENDDATE, oTicketsClaimed.SITE);

            return ObjParams;
        }
        private object[] GetTicketsParameters(Tickets oTickets)
        {
            SqlParameter[] ObjParams = new SqlParameter[4];
            ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_STARTDATE, oTickets.StartDate);
            ObjParams[1] = new SqlParameter(DBConstants.CONST_PARAM_ENDDATE, oTickets.EndDate);
            ObjParams[2] = new SqlParameter(DBConstants.CONST_PARAM_TYPE, oTickets.Type);
            ObjParams[3] = new SqlParameter(DBConstants.CONST_PARAM_LIABILITY, oTickets.IsLiability);

            return ObjParams;
        }

        private object[] GetVoucherParameters(Tickets oTickets)
        {
            SqlParameter[] ObjParams = new SqlParameter[6];
            ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_STARTDATE, oTickets.StartDate);
            ObjParams[1] = new SqlParameter(DBConstants.CONST_PARAM_ENDDATE, oTickets.EndDate);
            ObjParams[2] = new SqlParameter(DBConstants.CONST_PARAM_TYPE, oTickets.Type);
            ObjParams[3] = new SqlParameter(DBConstants.CONST_PARAM_BARCODE, oTickets.BarCode);
            ObjParams[4] = new SqlParameter(DBConstants.CONST_PARAM_LIABILITY, Convert.ToInt16(oTickets.IsLiability));
            ObjParams[5] = new SqlParameter(DBConstants.CONST_PARAM_SITE, oTickets.SITE);

            return ObjParams;
        }

        private object[] GetTreasuryParameters(Tickets oTickets)
        {
            SqlParameter[] ObjParams = new SqlParameter[4];
            ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_STARTDATE, oTickets.StartDate);
            ObjParams[1] = new SqlParameter(DBConstants.CONST_PARAM_ENDDATE, oTickets.EndDate);
            ObjParams[2] = new SqlParameter(DBConstants.CONST_PARAM_TYPE, oTickets.Type);
            ObjParams[3] = new SqlParameter(DBConstants.CONST_PARAM_SITE, oTickets.SITE);

            return ObjParams;
        }


        #endregion

        #region FillRouteFilter
        public List<CRMGetRoutesBySiteID> GetRoutes(int? SiteID)
        {
            try
            {
                using (EnterpriseDataContext context = EnterpriseDataContextHelper.GetDataContext())
                    return context.CRMGetActiveRoutesBySiteID(SiteID).ToList();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new List<CRMGetRoutesBySiteID>();
            }
        }
        #endregion

        #region

        public List<UserDetailsBySiteResult> GetUserDetails(int SiteId)
        {
            try
            {
                using (EnterpriseDataContext context = EnterpriseDataContextHelper.GetDataContext())
                    return context.GetUserDetailsBySite(SiteId).ToList();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new List<UserDetailsBySiteResult>();
            }
        }

        #endregion

        #region FillListofFilteredPositions
        public List<string> GetFilteredPositions(string RouteNumber)
        {

            List<string> lstPositions = new List<string>();

            //string strRoutes = "Select Bar_Pos_Name FROM Route_Member INNER JOIN Bar_Position ON Route_Member.Bar_Pos_No = Bar_Position.Bar_Pos_No  WHERE Route_Member.Route_No = " + RouteNumber;
            //DataTable dtRoutes = SqlHelper.ExecuteDataset(SQLConnection(), CommandType.Text, strRoutes).Tables[0];

            lstPositions.Add("--Any--");
            //foreach (DataRow row in dtRoutes.Rows)
            //{
            //    lstPositions.Add(row["Bar_Pos_Name"].ToString());
            //}

            return lstPositions;
        }
        #endregion

        #region "Get Tickets"

        public DataTable GetTickets(Tickets oTickets)
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
        public string GetBarPositionFromAsset(ReqType eReqType, string sMachine, string SITE)
        {
            string strValue = string.Empty;
            try
            {
                if (eReqType == ReqType.Machine)
                {
                    if (CollectionMachines == null)
                    {
                        CollectionMachines = new Dictionary<string, string>();
                    }
                    else if (!String.IsNullOrEmpty(FindMachine(sMachine)))
                    {
                        return FindMachine(sMachine);

                    }
                }
                else if (eReqType == ReqType.Zone)
                {
                    if (CollectionZONES == null)
                    {
                        CollectionZONES = new Dictionary<string, string>();
                    }
                    else if (!String.IsNullOrEmpty(FindZone(SITE)))
                    {
                        return FindZone(SITE);
                    }
                }

                SqlParameter[] param = SqlHelperParameterCache.GetSpParameterSet(GetConnectionString(), "rsp_GetMachine_ZoneDetails");
                param[0].Value = eReqType.ToString();
                param[1].Value = sMachine;
                param[2].Value = SITE;


                object oResValue = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetMachine_ZoneDetails", param);

                if (eReqType == ReqType.Machine)
                {
                    if (oResValue != null)
                    {
                        strValue = oResValue.ToString();
                    }

                    if (string.IsNullOrEmpty(strValue))
                    {
                        strValue = "(" + sMachine + ")";
                    }
                    CollectionMachines.Add(sMachine, strValue);
                }
                else if (eReqType == ReqType.Zone)
                {
                    if (oResValue != null)
                    {
                        strValue = oResValue.ToString();
                    }

                    if (string.IsNullOrEmpty(strValue))
                    {
                        strValue = "(" + SITE + ")";
                    }
                    CollectionZONES.Add(SITE, strValue);
                }


            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetBarPositionFromAsset" + ex.Message, LogManager.enumLogLevel.Info);
            }
            return strValue;
        }

        private string FindMachine(string sMachine)
        {
            string strBarPosition = string.Empty;
            try
            {
                strBarPosition = CollectionMachines[sMachine];
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                //LogManager.WriteLog("FindMachine " + ex.Message, LogManager.enumLogLevel.Info);
            }
            return strBarPosition;
        }

        private string FindZone(string SITE)
        {
            string strZone = string.Empty;
            try
            {
                strZone = CollectionZONES[SITE];
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("FindZone()" + ex.Message, LogManager.enumLogLevel.Info);
            }
            return strZone;
        }


        public enum ReqType
        {
            Machine,
            Zone
        }


        public Dictionary<string, string> LoadTicketWorkstations(string SITE)
        {
            //Dictionary<string, string> dTicketWorkStations = new Dictionary<string, string>();
            //SqlDataReader reader;
            //if (SITE == "0")
            //    reader = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.Text, "SELECT Site_Workstation FROM SITEWORKSTATIONS");
            //else
            //    reader = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.Text, "SELECT Site_Workstation FROM SITEWORKSTATIONS where site_ID=" + SITE);
            //while (reader.Read())
            //{
            //    if (!string.IsNullOrEmpty(reader["Site_Workstation"].ToString()))
            //    {
            //        dTicketWorkStations.Add(reader["Site_Workstation"].ToString(), reader["Site_Workstation"].ToString());
            //    }
            //}
            return CashDeskManagerStaticClass.LoadTicketWorkstations(SITE);

        }




        #endregion

        public List<TicketExceptions> TitoTicketOutExceptions(Tickets oTickets)
        {
            List<TicketExceptions> lstTickets = null;
            try
            {
                SqlParameter[] parameters = SqlHelperParameterCache.GetSpParameterSet(GetConnectionString(), "rsp_GetNonCompletedTicketPrints");
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
                    if (param.ParameterName == "@Site")
                        param.Value = oTickets.SITE;

                }
                SqlDataReader reader = SqlHelper.ExecuteReader(GetConnectionString(), "rsp_GetNonCompletedTicketPrints", parameters);

                lstTickets = new List<TicketExceptions>();
                TicketExceptions excep = null;

                while (reader.Read())
                {
                    excep = new TicketExceptions();
                    excep.SEGM = reader["Bar_position_name"].ToString();
                    excep.Machine = GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, reader["Bar_position_name"].ToString(), oTickets.SITE.ToString());
                    excep.currValue = (float)Convert.ToDouble(reader["TE_Value"]) / 100;
                    if (!string.IsNullOrEmpty(excep.SEGM))
                    {
                        excep.bExceptionRecordFound = true;
                        excep.Type = "OUT";
                        excep.Position = GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, reader["Bar_position_name"].ToString(), oTickets.SITE.ToString());
                        excep.PrintDate = Convert.ToDateTime(reader["TE_Date"]).ToString("dd MMM yyyy") + " " +
                            Convert.ToDateTime(reader["TE_Date"]).ToString("HH:mm:ss");
                        if (!string.IsNullOrEmpty(reader["TE_TicketNumber"].ToString()))
                        {
                            excep.Ticket = reader["ActualBarcode"].ToString();
                        }
                        else
                        {
                            excep.Ticket = reader["TE_TicketNumber"].ToString();
                        }
                        excep.Value = Convert.ToDouble(reader["TE_Value"]) / 100;
                        excep.Amount = excep.Value.ToString("###0.#0");
                        excep.Asset = reader["Machine_Stock_No"].ToString();
                        excep.COLINSTALLID = Convert.ToInt16(reader["TE_Installation_No"].ToString());
                        excep.CreateCompleted = reader["CreateExpected"].ToString();

                        excep.cTicketTotal += excep.currValue;
                        excep.cExceptionsTotal += excep.currValue;
                    }
                    else if (DBCommon.IsMachineATicketWorkstation(excep.Machine, oTickets.SITE.ToString()))
                    {
                        excep.bExceptionRecordFound = true;
                        excep.Type = "OUT";
                        excep.Position = GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, reader["Bar_position_name"].ToString(), oTickets.SITE.ToString());
                        excep.PrintDate = Convert.ToDateTime(reader["TE_Date"]).ToString("dd MMM yyyy") + " " +
                            Convert.ToDateTime(reader["TE_Date"]).ToString("HH:mm:ss");
                        if (!string.IsNullOrEmpty(reader["TE_TicketNumber"].ToString()))
                        {
                            excep.Ticket = reader["ActualBarcode"].ToString();
                        }
                        else
                        {
                            excep.Ticket = reader["TE_TicketNumber"].ToString();
                        }
                        excep.Value = Convert.ToDouble(reader["TE_Value"]) / 100;
                        excep.Amount = excep.Value.ToString("###0.#0");
                        excep.Asset = reader["Machine_Stock_No"].ToString();
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

        public List<TicketExceptions> TitoTicketsClaimed(Tickets oTickets)
        {
            string strTicketInException = string.Empty;
            string sPosTer = string.Empty;
            List<TicketExceptions> lstTickets = null;
            TicketExceptions excep = null;

            try
            {
                DataTable dtTickets = GetTickets(oTickets);
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
                            if (DBCommon.IsMachineATicketWorkstation(excep.SEGM, oTickets.SITE.ToString()))
                            {
                                sPosTer = excep.SEGM;
                                excep.Machine = GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, row["PrintDevice"].ToString(), oTickets.SITE.ToString());
                                excep.CurrentCashDesk += excep.currValue;
                                excep.CashDeskClaimedQty += 1;
                                if (oTickets.IsClaimedInCashDesk == true)
                                {
                                    excep.bExceptionRecordFound = true;
                                }

                            }
                            else
                            {
                                sPosTer = string.Empty;
                                excep.Machine = GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, excep.SEGM, oTickets.SITE.ToString());


                                excep.currEGM += excep.currValue;
                                excep.MachineClaimedQty += 1;
                                if (oTickets.IsClaimedInMachine)
                                {
                                    excep.bExceptionRecordFound = true;
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
                            excep.Machine = GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, excep.Machine, oTickets.SITE.ToString());
                            // excep.Machine = sPosTer;
                            excep.TransactionType = "Voucher Claimed";
                            excep.Zone = "n/a";

                            excep.PrintDate = Convert.ToDateTime(row["dtPrinted"]).ToString("dd MMM yyyy") + " " +
                                Convert.ToDateTime(row["dtPrinted"]).ToString("HH:mm:ss");

                            if (!(row["dtPaid"] == null))
                            {
                                excep.PayDate = Convert.ToDateTime(row["dtPaid"] != null ? row["dtPaid"] : string.Empty).ToString("dd MMM yyyy") + " " +
                                                Convert.ToDateTime(row["dtPaid"] != null ? row["dtPaid"] : string.Empty).ToString("HH:mm:ss");
                            }


                            excep.PayDevice = (row["PayDevice"] != null ? GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, row["PayDevice"].ToString(), oTickets.SITE.ToString()) : string.Empty);

                            if (!string.IsNullOrEmpty(row["strBarCode"].ToString()))
                            {
                                excep.Ticket = row["ActualBarcode"].ToString();
                            }
                            else
                            {
                                excep.Ticket = row["strBarcode"].ToString();
                            }
                            excep.Value = -(Convert.ToDouble(row["iAmount"]) / 100);
                            excep.Amount = (-(excep.Value)).ToString("###0.#0");


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


        public List<TicketExceptions> TitoTicketsClaimedLiability(Tickets oTickets)
        {
            string strTicketInException = string.Empty;
            string sPosTer = string.Empty;
            List<TicketExceptions> lstTickets = null;
            TicketExceptions excep = null;

            try
            {
                DataTable dtTickets = GetTickets(oTickets);
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
                        excep.VoucherStatus = row["strVoucherStatus"].ToString();
                        if (!string.IsNullOrEmpty(excep.SEGM) && row["strDeviceType"].ToString().Equals("EXTITO"))
                        {
                            if (DBCommon.IsMachineATicketWorkstation(excep.SEGM, oTickets.SITE.ToString()))
                            {
                                sPosTer = excep.SEGM;
                                excep.Machine = GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, row["PrintDevice"].ToString(), oTickets.SITE.ToString());
                                excep.bExceptionRecordFound = true;

                            }
                            else
                            {
                                sPosTer = string.Empty;
                                excep.Machine = GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, excep.SEGM, oTickets.SITE.ToString());
                                excep.bExceptionRecordFound = true;

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


                            excep.Position = GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, row["PrintDevice"].ToString(), oTickets.SITE.ToString());
                            excep.TransactionType = "Voucher Claimed";
                            excep.Zone = "n/a";

                            excep.PrintDate = Convert.ToDateTime(row["dtPrinted"]).ToString("dd MMM yyyy") + " " +
                                Convert.ToDateTime(row["dtPrinted"]).ToString("HH:mm:ss");

                            if (!(row["dtPaid"] == null))
                            {
                                excep.PayDate = Convert.ToDateTime(row["dtPaid"] != null ? row["dtPaid"] : string.Empty).ToString("dd MMM yyyy") + " " +
                                                Convert.ToDateTime(row["dtPaid"] != null ? row["dtPaid"] : string.Empty).ToString("HH:mm:ss");
                            }


                            excep.PayDevice = (row["PayDevice"] != null ? GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, row["PayDevice"].ToString(), oTickets.SITE.ToString()) : string.Empty);

                            if (!string.IsNullOrEmpty(row["strBarCode"].ToString()))
                            {
                                excep.Ticket = row["ActualBarcode"].ToString();
                            }
                            else
                            {
                                excep.Ticket = row["strBarcode"].ToString();
                            }
                            excep.Value = -(Convert.ToDouble(row["iAmount"]) / 100);
                            excep.Amount = (-(excep.Value)).ToString("###0.#0");


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

        public List<TicketExceptions> TitoTicketsPrinted(Tickets oTickets)
        {
            string strTicketInException = string.Empty;
            string sPosTer = string.Empty;
            List<TicketExceptions> lstTickets = null;
            TicketExceptions excep = null;
            double CurrentCashDesk = 0, currEGM = 0;

            try
            {
                DataTable dtTickets = GetTickets(oTickets);
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
                                if (DBCommon.IsMachineATicketWorkstation(excep.SEGM, oTickets.SITE.ToString()))
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
                                    excep.Machine = GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, excep.SEGM, oTickets.SITE.ToString());

                                    currEGM += excep.currValue;
                                    excep.MachinePrintedQty += 1;
                                    if (oTickets.IsPrintedInMachine)
                                    {
                                        excep.TicketAddedtoList = true;
                                    }

                                }
                            }
                            else
                            {
                                excep.Position = GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, excep.SEGM, oTickets.SITE.ToString());
                                excep.Machine = "";
                                excep.CurrentCashDesk += excep.currValue;

                                excep.CashDeskPrintedQty += 1;
                                excep.TicketAddedtoList = true;
                            }
                        }

                        if (excep.TicketAddedtoList)
                        {
                            excep.Value = Convert.ToDouble(row["iAmount"]) / 100;
                            excep.Amount = excep.Value.ToString("###0.#0");
                            string dPrintDate = row["dtPrinted"].ToString();
                            string dClaimDate = string.IsNullOrEmpty(row["dtPaid"].ToString()) ? "01/01/3100" : row["dtPaid"].ToString();

                            excep.TransactionType = "Voucher Printed";
                            excep.Zone = "n/a";


                            if ((Convert.ToDateTime(dPrintDate) >= Convert.ToDateTime(oTickets.StartDate)) &&
                                (Convert.ToDateTime(dClaimDate) <= Convert.ToDateTime(oTickets.EndDate)))
                            {
                                excep.Status = "Printed within, Claimed in different category";
                            }
                            else if (string.IsNullOrEmpty(row["dtPaid"].ToString()))
                            {
                                excep.Status = "Active Voucher";
                            }
                            else
                            {
                                excep.Value = -excep.Value;
                                excep.Amount = excep.Value.ToString("###0.#0");
                                excep.Status = "Printed within period, claimed later";
                            }

                            excep.Position = sPosTer;

                            excep.PrintDate = Convert.ToDateTime(row["dtPrinted"]).ToString("dd MMM yyyy") + " " +
                                Convert.ToDateTime(row["dtPrinted"]).ToString("HH:mm:ss");

                            if (!(row["dtPrinted"] == null))
                            {
                                excep.PayDate = Convert.ToDateTime(row["dtPrinted"] != null ? row["dtPrinted"] : string.Empty).ToString("dd MMM yyyy") + " " +
                                                Convert.ToDateTime(row["dtPrinted"] != null ? row["dtPrinted"] : string.Empty).ToString("HH:mm:ss");
                            }
                            excep.PayDate = Convert.ToDateTime(row["dtPrinted"] != null ? row["dtPrinted"] : string.Empty).ToString("dd MMM yyyy") + " " +
                                                Convert.ToDateTime(row["dtPrinted"] != null ? row["dtPrinted"] : string.Empty).ToString("HH:mm:ss");

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


        public List<TicketExceptions> TitoTicketsPrintedLiability(Tickets oTickets)
        {
            string strTicketInException = string.Empty;
            string sPosTer = string.Empty;
            List<TicketExceptions> lstTickets = null;
            TicketExceptions excep = null;

            try
            {
                DataTable dtTickets = GetTickets(oTickets);
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
                                if (DBCommon.IsMachineATicketWorkstation(excep.SEGM, oTickets.SITE.ToString()))
                                {
                                    excep.TicketAddedtoList = true;
                                }
                                else
                                {
                                    sPosTer = string.Empty;
                                    excep.Machine = GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, excep.SEGM, oTickets.SITE.ToString());

                                    excep.TicketAddedtoList = true;

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
                            excep.Amount = excep.Value.ToString("###0.#0");
                            string dPrintDate = row["dtPrinted"].ToString();
                            string dClaimDate = string.IsNullOrEmpty(row["dtPaid"].ToString()) ? "01/01/3100" : row["dtPaid"].ToString();
                            excep.VoucherStatus = row["strVoucherStatus"].ToString();
                            excep.TransactionType = "Voucher Printed";
                            excep.Zone = "n/a";


                            if ((Convert.ToDateTime(dPrintDate) >= Convert.ToDateTime(oTickets.StartDate)) &&
                                (Convert.ToDateTime(dClaimDate) <= Convert.ToDateTime(oTickets.EndDate)))
                            {
                                excep.Status = "Printed within, Claimed in different category";
                            }
                            else if (string.IsNullOrEmpty(row["dtPaid"].ToString()) && (Convert.ToDateTime(row["dtExpire"]) > oTickets.EndDate))
                            {
                                excep.Status = "Active Voucher";
                            }
                            else if (string.IsNullOrEmpty(row["dtPaid"].ToString()) && (Convert.ToDateTime(row["dtExpire"]) < oTickets.EndDate))
                            {
                                excep.Status = "Expired Voucher";
                            }
                            else
                            {
                                //  excep.Status = "Printed within period, claimed later";
                                excep.Value = -excep.Value;
                                excep.Amount = excep.Value.ToString("###0.#0");
                                excep.Status = "Printed within period, claimed later";
                            }

                            excep.Position = GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, row["PrintDevice"].ToString(), oTickets.SITE.ToString());

                            excep.PrintDate = Convert.ToDateTime(row["dtPrinted"]).ToString("dd MMM yyyy") + " " +
                                Convert.ToDateTime(row["dtPrinted"]).ToString("HH:mm:ss");

                            if (!(row["dtPaid"] == null))
                            {
                                if (row["dtPaid"] == DBNull.Value)
                                {
                                    excep.PayDate = string.Empty;
                                }
                                else
                                {
                                    excep.PayDate = Convert.ToDateTime(row["dtPaid"]).ToString("dd MMM yyyy") + " " +
                                                     Convert.ToDateTime(row["dtPaid"]).ToString("HH:mm:ss");
                                }
                            }


                            excep.PayDevice = (row["PayDevice"] != null ? GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, row["PayDevice"].ToString(), oTickets.SITE.ToString()) : string.Empty);

                            // Fix for #107968
                            if (!excep.Status.Contains("Active Voucher") && !excep.Status.Contains("Expired Voucher"))
                            {
                                if (!string.IsNullOrEmpty(row["strBarCode"].ToString()))
                                {
                                    excep.Ticket = row["ActualBarcode"].ToString();
                                }
                                else
                                {
                                    excep.Ticket = row["strBarcode"].ToString();
                                }
                            }
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
        public List<TicketExceptions> TicketsClaimed(TicketsClaimed oTickets)
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

                        if (Convert.ToDateTime(row["tbr_payout_time"].ToString()) <= Convert.ToDateTime(oTickets.TicketsClaimedFrom))
                        {
                            excep.Position = GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, row["TBR_Payout_Machine_Serial"].ToString(), oTickets.SITE.ToString());

                            excep.PrintDate = Convert.ToDateTime(row["TBR_Payout_Print_Time"]).ToString("dd MMM yyyy") + " " +
                                Convert.ToDateTime(row["TBR_Payout_Print_Time"]).ToString("HH:mm:ss");

                            if (!(row["TBR_Payout_Time"] == null))
                            {
                                excep.PayDate = Convert.ToDateTime(row["TBR_Payout_Time"] != null ? row["TBR_Payout_Time"] : string.Empty).ToString("dd MMM yyyy") + " " +
                                                Convert.ToDateTime(row["TBR_Payout_Time"] != null ? row["TBR_Payout_Time"] : string.Empty).ToString("HH:mm:ss");
                            }

                            excep.ClaimedTerminal = GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, row["TBR_Payout_Claimed_Terminal"].ToString(), oTickets.SITE.ToString());
                            excep.Ticket = row["TBR_Payout_ExternalIndex"].ToString();
                            excep.Status = "Printed prior to period, Claimed within";

                            excep.Value = -(Convert.ToDouble(row["tbr_payout_value"]) / 100);
                            excep.Amount = excep.Value.ToString("###0.#0");
                            excep.cTicketTotal += excep.currValue;

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

        public List<TicketExceptions> TicketsPrinted(TicketsClaimed oTickets)
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

                        if (row["tbr_payout_claimed"].ToString().Equals("1"))
                        {
                            excep.Position = GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, row["TBR_Payout_Machine_Serial"].ToString(), oTickets.SITE.ToString());

                            excep.PrintDate = Convert.ToDateTime(row["TBR_Payout_Print_Time"]).ToString("dd MMM yyyy") + " " +
                                Convert.ToDateTime(row["TBR_Payout_Print_Time"]).ToString("HH:mm:ss");

                            if (!(row["TBR_Payout_Time"] == null))
                            {
                                excep.PayDate = Convert.ToDateTime(row["TBR_Payout_Time"] != null ? row["TBR_Payout_Time"] : string.Empty).ToString("dd MMM yyyy") + " " +
                                                Convert.ToDateTime(row["TBR_Payout_Time"] != null ? row["TBR_Payout_Time"] : string.Empty).ToString("HH:mm:ss");
                            }

                            excep.ClaimedTerminal = GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, row["TBR_Payout_Claimed_Terminal"].ToString(), oTickets.SITE.ToString());
                            excep.Ticket = row["TBR_Payout_ExternalIndex"].ToString();
                            excep.Status = "Printed prior to period, Claimed within";

                            excep.Value = Convert.ToDouble(row["tbr_payout_value"]) / 100;
                            excep.Amount = excep.Value.ToString("###0.#0");
                            excep.cTicketTotal += excep.currValue;

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

        private DataTable GetTicketsPrinted(TicketsClaimed oTickets)
        {
            return ExecuteTable(DBConstants.CONST_SP_GET_TICKETS_PRINTED, oTickets);
        }


        public List<TicketExceptions> TicketsUnClaimed(TicketsClaimed oTickets)
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
                        excep.SEGM = row["Bar_position_name"].ToString();
                        excep.Machine = GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, row["Bar_position_name"].ToString(), oTickets.SITE.ToString());

                        if (DBCommon.IsMachineATicketWorkstation(row["PrintDevice"].ToString(), oTickets.SITE.ToString()))
                        {
                            excep.Position = GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, row["PrintDevice"].ToString(), oTickets.SITE.ToString());
                            excep.Machine = row["Bar_position_name"].ToString() + " - " + row["tbr_node_serial"].ToString();
                            excep.TransactionType = "Ticket";
                            excep.Zone = row["Zone_Name"] != null ? row["Zone_Name"].ToString() : string.Empty;

                            excep.PrintDate = Convert.ToDateTime(row["tbr_payout_time"]).ToString("dd MMM yyyy") + " " +
                                Convert.ToDateTime(row["tbr_payout_time"]).ToString("HH:mm:ss");

                            excep.PayDate = Convert.ToDateTime(row["tbr_payout_time"]).ToString("dd MMM yyyy") + " " +
                             Convert.ToDateTime(row["tbr_payout_time"]).ToString("HH:mm:ss");

                            excep.SEGM = row["machine_name"].ToString();

                            excep.Value = Convert.ToDouble(row["tbr_payout_value"]) / 100;
                            excep.Amount = (Convert.ToDouble(row["tbr_payout_value"]) / 100).ToString("###0.#0");

                            excep.cExceptionsTotal += (float)excep.Value;
                        }
                        else
                        {
                            excep.Position = GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, row["PrintDevice"].ToString(), oTickets.SITE.ToString());
                            excep.Machine = row["Bar_position_name"].ToString() + " - " + row["tbr_node_serial"].ToString();
                            excep.TransactionType = "Ticket";
                            excep.Zone = row["Zone_Name"] != null ? row["Zone_Name"].ToString() : string.Empty;

                            excep.PrintDate = Convert.ToDateTime(row["tbr_payout_time"]).ToString("dd MMM yyyy") + " " +
                                Convert.ToDateTime(row["tbr_payout_time"]).ToString("HH:mm:ss");

                            excep.PayDate = Convert.ToDateTime(row["tbr_payout_time"]).ToString("dd MMM yyyy") + " " +
                             Convert.ToDateTime(row["tbr_payout_time"]).ToString("HH:mm:ss");

                            excep.SEGM = row["machine_name"].ToString();

                            excep.Value = Convert.ToDouble(row["tbr_payout_value"]) / 100;
                            excep.Amount = (Convert.ToDouble(row["tbr_payout_value"]) / 100).ToString("###0.#0");

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
        public DataTable GetPromoTickets(TicketsClaimed oTickets)
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

        public List<TicketExceptions> RetrieveTicketAnomalies(TicketsClaimed oTickets, List<string> lstPositions)
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
                        excep.Machine = GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, row["Machine"].ToString(), oTickets.SITE.ToString());

                        if (!string.IsNullOrEmpty(excep.SEGM) &&
                            DBCommon.CheckPositionToDisplay(excep.Machine, lstPositions))
                        {
                            excep.Reference = row["Reference"].ToString();
                            excep.Value = Convert.ToDouble(row["Amount"]);
                            excep.Amount = Convert.ToDouble(row["Amount"]).ToString("###0.#0");
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

        public List<TicketExceptions> GetTreasuryItems(Tickets oTickets)
        {
            List<TicketExceptions> items = null;
            TicketExceptions treasury = null;

            try
            {
                DataTable dtTreasury = ExecuteTable(GetConnectionString(), oTickets, false);

                items = new List<TicketExceptions>();
                foreach (DataRow row in dtTreasury.Rows)
                {
                    treasury = new TicketExceptions();
                    //treasury.PayDate = row["treasury_date"].ToString() + " " + row["treasury_time"].ToString();
                    //treasury.PrintDate = row["treasury_date"].ToString() + " " + row["treasury_time"].ToString();
                    treasury.PayDate = row["treasury_date"].ToString();
                    treasury.PrintDate = row["treasury_date"].ToString();
                    treasury.TransactionType = oTickets.Type;
                    treasury.Zone = row["Zone_name"].ToString();
                    treasury.Position = row["Bar_Position_Name"].ToString();
                    treasury.Machine = row["machine_name"].ToString();
                    treasury.Amount = Convert.ToDouble(row["treasury_amount"]).ToString("###0.#0");
                    treasury.currValue = Convert.ToInt64(row["treasury_amount"]);
                    treasury.Reason = row["treasury_reason"].ToString();
                    treasury.ReasonCode = row["treasury_reason_code"].ToString();
                    treasury.Details = "";

                    if (string.IsNullOrEmpty(treasury.Reason))
                    {
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
                    }
                    items.Add(treasury);

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return items;
        }

        public string GetRegionFromSite()
        {
            string Query = "Select Region = Case Upper(Region) When 'US' Then 'US'	WHEN 'UK' THEN  'UK' ELSE '' End  From Site";

            object objRegion = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.Text, Query);
            if (objRegion != null)
            {
                return (objRegion.ToString());
            }
            else
            {
                return string.Empty;
            }
        }

        public bool ClearTicketStatus(string Ticket, string DeviceID)
        {
            //SDGOneConnect.TheOneConnection oSDG = new SDGOneConnect.TheOneConnection();

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


        //private void InsertException(int InstallationNo, string Type, string Details, string Ticket)
        //{
        //    try
        //    {
        //        SqlParameter[] parames = SqlHelperParameterCache.GetSpParameterSet(GetConnectionString(), "usp_InsertException");
        //        foreach (SqlParameter par in parames)
        //        {
        //            if (par.ParameterName == "@Installation_ID") { par.Value = InstallationNo; }
        //            if (par.ParameterName == "@Exception_Type") { par.Value = Type; }
        //            if (par.ParameterName == "@Details") { par.Value = Details; }
        //            if (par.ParameterName == "@Reference") { par.Value = Ticket; }
        //            if (par.ParameterName == "@User") { par.Value = BMC.Security.SecurityHelper.CurrentUser; }

        //        }
        //        SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_InsertException", parames);
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //    }

        //}

        public DataSet GetCashDeskReconcilationDetails(DateTime StartDate, DateTime EndDate, int SiteID, int RouteNo, int UserNo)
        {
            DataSet dsCashDeskReconDetails = new DataSet();

            try
            {
                LogManager.WriteLog("Inside GetCashDeskReconcilationDetails method", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Stored procedure parameters...", LogManager.enumLogLevel.Info);

                SqlParameter[] objParams = new SqlParameter[5];

                objParams[0] = new SqlParameter(DBConstants.CONST_PARAM_STARTDATE, StartDate);
                objParams[1] = new SqlParameter(DBConstants.CONST_PARAM_ENDDATE, EndDate);
                objParams[2] = new SqlParameter("@Site", SiteID);
                objParams[3] = new SqlParameter("@RouteNo", RouteNo);
                objParams[4] = new SqlParameter("@UserNo", UserNo);

                LogManager.WriteLog("Stored Procedure parameters set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Executing SQLHelper FillDataset method...", LogManager.enumLogLevel.Info);

                SqlHelper.FillDataset(GetConnectionString(), "rsp_REPORT_DailyCashDeskRecon", dsCashDeskReconDetails, new string[] { "CashDeskReconcilation" }, objParams);

                LogManager.WriteLog("SQLHelper FillDataset executed successfully", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return dsCashDeskReconDetails;
        }

        public DataSet GetSystemBalancingDetails(DateTime StartDate, DateTime EndDate, int site, int RouteNo, int UserNo)
        {
            DataSet dsSystemBalancingDetails = new DataSet();

            try
            {
                LogManager.WriteLog("Inside GetSystemBalancingDetails method", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Stored procedure parameters...", LogManager.enumLogLevel.Info);

                SqlParameter[] objParams = new SqlParameter[5];

                objParams[0] = new SqlParameter("@dtStartDate",StartDate);
                objParams[1] = new SqlParameter("@dtEndDate", EndDate);
                objParams[2] = new SqlParameter("@Site", site);
                objParams[3] = new SqlParameter("@RouteNo", RouteNo);
                objParams[4] = new SqlParameter("@UserNo", UserNo);

                LogManager.WriteLog("Stored Procedure parameters set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Executing SQLHelper FillDataset method...", LogManager.enumLogLevel.Info);

                SqlHelper.FillDataset(GetConnectionString(), "rsp_REPORT_DailyCashDeskCollectionConsolidated", dsSystemBalancingDetails, new string[] { "SystemBalancing" }, objParams);

                LogManager.WriteLog("SQLHelper FillDataset executed successfully", LogManager.enumLogLevel.Info);


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw ex;
            }
            return dsSystemBalancingDetails;
        }

        public DataSet GetCashDeskMovementDetails(DateTime StartDate, DateTime EndDate, int site, string sRegion, int RouteNo, int UserNo)
        {
            DataSet dsCashDeskMovementDetails = new DataSet();

            try
            {
                LogManager.WriteLog("Inside GetCashDeskMovementDetails method", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Stored procedure parameters...", LogManager.enumLogLevel.Info);

                SqlParameter[] objParams = new SqlParameter[6];

                objParams[0] = new SqlParameter(DBConstants.CONST_PARAM_STARTDATE, StartDate);
                objParams[1] = new SqlParameter(DBConstants.CONST_PARAM_ENDDATE, EndDate);
                objParams[2] = new SqlParameter("@Site", site);
                objParams[3] = new SqlParameter("@Region", sRegion);
                objParams[4] = new SqlParameter("@RouteNo", RouteNo);
                objParams[5] = new SqlParameter("@UserNo", UserNo);

                LogManager.WriteLog("Stored Procedure parameters set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Executing SQLHelper FillDataset method...", LogManager.enumLogLevel.Info);

                SqlHelper.FillDataset(GetConnectionString(), "rsp_Report_DailyCashDesk", dsCashDeskMovementDetails, new string[] { "CashDeskMovement" }, objParams);

                LogManager.WriteLog("SQLHelper FillDataset executed successfully", LogManager.enumLogLevel.Info);


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return dsCashDeskMovementDetails;
        }

        public bool CheckAccessForViewTicketNumber(int UserID)
        {
            var access = false;
            //  LogManager.WriteLog("Inside CheckAccessForViewTicketNumber method with userID " + UserID.ToString(), LogManager.enumLogLevel.Info);
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter(DBConstants.CONST_USER_ID, UserID);

            var result = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "rsp_CheckViewTicketNumberAccess", objParams);
            while (result.Read())
            {
                access = Convert.ToBoolean(result["HQ_CashierTransactions_ViewNumberTickets"]);
            }

            return access;

        }

        public DataSet GetCashierTransactionsDetails(bool? isCDMPaid, bool? isCDMPrinted, bool? isHandPay, bool? isShortPay, bool? isVoidVoucher, bool? isJackpot,
            bool? isProgressive, bool? isVoid, bool? isMachinePaid, bool? isMachinePrinted,
            bool? isActive, bool? isVoidCancel, bool? isExpired, bool? isException, bool? isLiability,
            bool? isPromo, bool? isNonCashableIN, bool? isNonCashableOut,
            DateTime startDate, DateTime endDate, int nSiteID, int Route_No, bool? isOffline)
        {
            DataSet dsCashierTransactionsDetails = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[23];

                objParams[0] = new SqlParameter("@isCDMPaid", isCDMPaid);
                objParams[1] = new SqlParameter("@isCDMPrinted", isCDMPrinted);
                objParams[2] = new SqlParameter("@isHandPay", isHandPay);
                objParams[3] = new SqlParameter("@isShortPay", isShortPay);
                objParams[4] = new SqlParameter("@isVoidVoucher", isVoidVoucher);
                objParams[5] = new SqlParameter("@isJackpot", isJackpot);
                objParams[6] = new SqlParameter("@isProgressive", isProgressive);
                objParams[7] = new SqlParameter("@isVoid", isVoid);
                objParams[8] = new SqlParameter("@isMachinePaid", isMachinePaid);
                objParams[9] = new SqlParameter("@isMachinePrinted", isMachinePrinted);
                objParams[10] = new SqlParameter("@isActive", isActive);
                objParams[11] = new SqlParameter("@isVoidCancel", isVoidCancel);
                objParams[12] = new SqlParameter("@isExpired", isExpired);
                objParams[13] = new SqlParameter("@isException", isException);
                objParams[14] = new SqlParameter("@isLiability", isLiability);
                objParams[15] = new SqlParameter("@isPromo", isPromo);
                objParams[16] = new SqlParameter("@isNonCashableIN", isNonCashableIN);
                objParams[17] = new SqlParameter("@isNonCashableOut", isNonCashableOut);
                objParams[18] = new SqlParameter("@StartDate", startDate);
                objParams[19] = new SqlParameter("@EndDate", endDate);
                objParams[20] = new SqlParameter("@SITE", nSiteID);
                objParams[21] = new SqlParameter("@Route_No", Route_No);

                objParams[22] = new SqlParameter("@isOffline", isOffline);
                SqlHelper.FillDataset(GetConnectionString(), "rsp_GetCashierTransactionsDetails", dsCashierTransactionsDetails, new string[] { "DetailsView" }, objParams);

                return dsCashierTransactionsDetails;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public DataSet GetCashierHistory(
                   DateTime startDate, DateTime endDate, int nSiteID, int Route_No, int User_No)
        {
            DataSet dsCashierTransactionsDetails = new DataSet();
            try
            {
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@StartDate", startDate);
                objParams[1] = new SqlParameter("@EndDate", endDate);
                objParams[2] = new SqlParameter("@SITE", nSiteID);
                objParams[3] = new SqlParameter("@Route_No", Route_No);
                objParams[4] = new SqlParameter("@User_No", User_No);
                SqlHelper.FillDataset(GetConnectionString(), "rsp_CDM_GetCashierTransactionsDetails_Summary", dsCashierTransactionsDetails, new string[] { "DetailsView", "Summary" }, objParams);
                return dsCashierTransactionsDetails;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public DataSet GetCashierHistory_Details(
                   DateTime startDate, DateTime endDate, int nSiteID, int Route_No, int User_No, bool isCDMPaid, bool isCDMPrinted, bool isHandPay, bool isShortpay
                , bool isVoidVoucher, bool isJackpot, bool isProgressive, bool isVoid, bool isMachinePaid, bool isMachinePrinted, bool isActive, bool isVoidCancel, bool isExpired
                , bool isException, bool isLiability, bool isPromo, bool isNonCashableIN, bool isNonCashableOut, bool isOffline

            )
        {
            DataSet dsCashierTransactionsDetails = new DataSet();
            try
            {
                SqlParameter[] objParams = new SqlParameter[24];
                objParams[0] = new SqlParameter("@StartDate", startDate);
                objParams[1] = new SqlParameter("@EndDate", endDate);
                objParams[2] = new SqlParameter("@SITE", nSiteID);
                objParams[3] = new SqlParameter("@Route_No", Route_No);
                objParams[4] = new SqlParameter("@User_No", User_No);
                objParams[5] = new SqlParameter("@isCDMPaid", isCDMPaid);
                objParams[6] = new SqlParameter("@isCDMPrinted", isCDMPrinted);
                objParams[7] = new SqlParameter("@isHandPay", isHandPay);
                objParams[8] = new SqlParameter("@isShortpay", isShortpay);
                objParams[9] = new SqlParameter("@isVoidVoucher", isVoidVoucher);
                objParams[10] = new SqlParameter("@isJackpot", isJackpot);
                objParams[11] = new SqlParameter("@isProgressive", isProgressive);
                objParams[12] = new SqlParameter("@isVoid", isVoid);
                objParams[13] = new SqlParameter("@isMachinePaid", isMachinePaid);
                objParams[14] = new SqlParameter("@isMachinePrinted", isMachinePrinted);
                objParams[15] = new SqlParameter("@isActive", isActive);
                objParams[16] = new SqlParameter("@isVoidCancel", isVoidCancel);
                objParams[17] = new SqlParameter("@isExpired", isExpired);
                objParams[18] = new SqlParameter("@isException", isException);
                objParams[19] = new SqlParameter("@isLiability", isLiability);
                objParams[20] = new SqlParameter("@isPromo", isPromo);
                objParams[21] = new SqlParameter("@isNonCashableIN", isNonCashableIN);
                objParams[22] = new SqlParameter("@isNonCashableOut", isNonCashableOut);
                objParams[23] = new SqlParameter("@isOffline", isOffline);
                SqlHelper.FillDataset(GetConnectionString(), "rsp_CDM_GetCashierTransactionsDetails", dsCashierTransactionsDetails, new string[] { "DetailsView", "Summary" }, objParams);
                return dsCashierTransactionsDetails;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public static class CashDeskManagerStaticClass
        {
            static Dictionary<string, string> dTicketWorkStations;

            public static Dictionary<string, string> LoadTicketWorkstations(string SITE)
            {
                if (dTicketWorkStations == null)
                    dTicketWorkStations = new Dictionary<string, string>();
                else
                    return dTicketWorkStations;

                SqlDataReader reader;
                if (SITE == "0")
                    reader = SqlHelper.ExecuteReader(DatabaseHelper.GetConnectionString(), CommandType.Text, "SELECT Site_Workstation FROM SITEWORKSTATIONS");
                else
                    reader = SqlHelper.ExecuteReader(DatabaseHelper.GetConnectionString(), CommandType.Text, "SELECT Site_Workstation FROM SITEWORKSTATIONS where site_ID=" + SITE);
                while (reader.Read())
                {
                    if (!string.IsNullOrEmpty(reader["Site_Workstation"].ToString()))
                    {
                        dTicketWorkStations.Add(reader["Site_Workstation"].ToString(), reader["Site_Workstation"].ToString());
                    }
                }
                return dTicketWorkStations;

            }
        }
    }
}
