/// Source File Name : MeterAnalysisBI.cs
/// Description		 : Business layer for meter analysis - call the DB builder to get data
/// Revision History
/// Author             Date              Description
/// ---------------------------------------------------
/// Madhusudhanan      13/5/08          created
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BMC.Business.Classes.DBBuilder;
using BMC.Common.LogManagement;
using BMC.Common.ConfigurationManagement;
using System.Linq; 

namespace BMC.Business.Classes
{
    /// <summary>
    /// Business layer for MeterAnalysis 
    /// </summary>
    /// <Author>Madhusudhanan</Author>
    /// <DateCreated>13-5-2008</DateCreated>
    ///
    /// Class Revision History
    ///
    /// Author             Date              Description
    /// ---------------------------------------------------
    ///madhusudhanan        13-05-08        Created
    public class MeterAnalysisBI
    {
        /// <summary>
        /// Gets data for combo box
        /// </summary>
        /// <Author>Madhusudhanan</Author>
        /// <DateCreated>13-5-2008</DateCreated>
        /// <Parameters> </Parameters>
        /// <returns>datatable with data</returns>
        public static DataTable GetList(string strListName, Int32 iID)
        {
            DataTable dtList = new DataTable();
            try
            {
                dtList = MeterAnalysisDBBuilder.GetList(strListName, iID);

            }
            catch { }
            return dtList;
        }
        /// <summary>
        /// Gets data for site details
        /// </summary>
        /// <Author>Madhusudhanan</Author>
        /// <DateCreated>13-5-2008</DateCreated>
        /// <Parameters>TranportLayer object </Parameters>
        /// <returns>datatable with data</returns>
        public static DataTable GetSiteDetails(MeterAnalysisTransport objMATransport)
        {
            DataTable dtSiteDetails = new DataTable();
            try
            {
                dtSiteDetails = MeterAnalysisDBBuilder.GetSiteDetails(objMATransport);
            }
            catch { }
            return dtSiteDetails;

        }

        /// <summary>
        /// Gets data for tree view
        /// </summary>
        /// <Author>Madhusudhanan</Author>
        /// <DateCreated>13-5-2008</DateCreated>
        /// <Parameters> </Parameters>
        /// <returns>data reader</returns>
        public static SqlDataReader GetSitesList(MeterAnalysisTransport ObjMATransport)
        {
            SqlDataReader drSites = null;
            try
            {
                drSites = MeterAnalysisDBBuilder.GetSitesList(ObjMATransport);

            }
            catch { }
            return drSites;
        }

        /// <summary>
        /// Gets data for combo box
        /// </summary>
        /// <Author>Madhusudhanan</Author>
        /// <DateCreated>13-5-2008</DateCreated>
        /// <Parameters>Transport layer object </Parameters>
        /// <returns>datatable with data</returns>
        public static DataTable GetMainGridDetails(MeterAnalysisTransport ObjMATransport, Boolean blnDataForGrid)
        {
            DataTable dtMainGrid = new DataTable();
            try
            {
                dtMainGrid = MeterAnalysisDBBuilder.GetMainGridDetails(ObjMATransport, blnDataForGrid);
                if (dtMainGrid.Rows.Count > 0)
                {
                    DataRow TotalRow = dtMainGrid.NewRow();
                    decimal iQty = 0, iHandle = 0, iDOF = 0, iGamesPlayed = 0, iCasinoWin = 0, iDailyWin = 0, iBet = 0;
                    decimal iTheoNetwin = 0, iAvgGames = 0, iOccupancy = 0;
                    decimal iHoldTheopercent = 0, iHoldActPercent = 0, iHoldPercentVar = 0;
                    decimal iPayoutTheopercent = 0, iPayoutActPercent = 0, iwin = 0, iPayoutPercentVar = 0;
                    //calculate the total for records
                    foreach (DataRow dr in dtMainGrid.Rows)
                    {
                        iQty = ToDecimal(dr["QTYTotal"]);
                        iHandle += ToDecimal(dr["Handle"]);
                        iDOF = ToDecimal(dr["DOFTotal"]);
                        iGamesPlayed += ToDecimal(dr["Games Played"]);
                        iCasinoWin += ToDecimal(dr["Casino Win"]);
                        iDailyWin += ToDecimal(dr["Avg Daily Win"]);
                        iBet += ToDecimal(dr["Avg Bet"]);
                        iTheoNetwin += ToDecimal(dr["Theo_Net_Win"]);
                        iAvgGames += ToDecimal(dr["Avg Games"]);
                        iOccupancy += ToDecimal(dr["OccupancySum"]);

                        iHoldTheopercent += (ToDecimal(dr["Handle"]) * ToDecimal(dr["Hold_Perc"]));
                        //iHoldTheopercent += ToDouble(dr["Hold_Perc"]);
                        iHoldActPercent += ToDecimal(dr["Hold_Act_Perc"]);
                        iHoldPercentVar += ToDecimal(dr["Hold_Perc_Var"]);

                        iPayoutTheopercent += ToDecimal(dr["Payout_Perc"]);
                        iPayoutActPercent += ToDecimal(dr["Payout_Act_Perc"]);
                        iPayoutPercentVar += ToDecimal(dr["Payout_Perc_Var"]);
                        iwin += ToDecimal(dr["win"]);

                    }
                    Int32 iRowCount = dtMainGrid.Rows.Count;
                    iDailyWin = Math.Round(iDailyWin / iRowCount);
                    iBet = Math.Round(iBet / iRowCount);
                    iHoldTheopercent = ToDecimal(iHoldTheopercent) / ToDecimal(iHandle > 0 ? iHandle : 1);                        //Math.Round(iHoldTheopercent / iRowCount, 2);
                    iHoldActPercent = ToDecimal(Math.Round(((iHandle - iwin) / ToDecimal(iHandle > 0 ? iHandle : 1)) * 100, 2)); //Math.Round(iHoldActPercent / iRowCount, 2);
                    iHoldPercentVar = Math.Round(iHoldPercentVar / iRowCount, 2);
                    iPayoutTheopercent = Math.Round(iPayoutTheopercent / iQty, 2);
                    //iPayoutTheopercent = Math.Round(iPayoutTheopercent / iRowCount, 2);
                    iPayoutActPercent = Math.Round(iPayoutActPercent / iRowCount, 2);
                    iPayoutPercentVar = Math.Round(iPayoutPercentVar / iRowCount, 2);
                    iAvgGames = Math.Round(iGamesPlayed / iDOF);
                    iOccupancy = Math.Round((iOccupancy / iDOF) * 100, 2);
                    //assign total values to the row
                    TotalRow[0] = "Total";
                    TotalRow["Qty"] = iQty;
                    TotalRow["Handle"] = iHandle;
                    TotalRow["DOF"] = iDOF;
                    TotalRow["Games Played"] = iGamesPlayed;
                    TotalRow["Casino Win"] = iCasinoWin;
                    if (iDOF == 0 || iQty == 0)
                    {
                        TotalRow["Avg Daily Win"] = Math.Round(iCasinoWin, 2);
                    }
                    else
                    {
                        TotalRow["Avg Daily Win"] = Math.Round(iCasinoWin / (iDOF), 2);
                    }
                    
                    if (iGamesPlayed == 0)
                    {
                        iGamesPlayed = 1;
                    }
                    TotalRow["Avg Bet"] = Math.Round( iHandle / iGamesPlayed,2);
                    TotalRow["Theo_Net_Win"] = iTheoNetwin;// Math.Round(iHandle * (iHoldTheopercent / 100), 2);
                    TotalRow["Avg Games"] = iAvgGames;
                    if (ConfigManager.Read("HideOccupancy").ToUpper() != "YES")
                    {
                        TotalRow["Occupancy"] = iOccupancy;
                    }
                    TotalRow["Hold_Perc"] = iHoldTheopercent;
                    TotalRow["Hold_Act_Perc"] = iHoldActPercent;
                    TotalRow["Hold_Perc_Var"] = Math.Round((ToDecimal(iHoldTheopercent) - ToDecimal(iHoldActPercent)), 2); //iHoldPercentVar;

                    TotalRow["Payout_Perc"] = iPayoutTheopercent;
                    TotalRow["Payout_Act_Perc"] = iPayoutActPercent;
                    TotalRow["Payout_Perc_Var"] = iPayoutPercentVar;
                    //insert the total row to the main table
                    dtMainGrid.Rows.InsertAt(TotalRow, 0);
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }
            return dtMainGrid;
        }
        private static double ToDouble(object ObjValue)
        {
            try
            {
                return Convert.ToDouble(ObjValue);
            }
            catch
            { return 0; }
        }


        private static decimal ToDecimal(object ObjValue)
        {
            try
            {
                return Convert.ToDecimal(ObjValue);
            }
            catch
            { return 0; }
        }


        /// <summary>
        /// Gets data for graph
        /// </summary>
        /// <Author>Madhusudhanan</Author>
        /// <DateCreated>30-6-2008</DateCreated>
        /// <Parameters>Transport layer object </Parameters>
        /// <returns>datatable with data</returns>
        public static DataTable GetGraphDetails(MeterAnalysisTransport ObjMATransport)
        {
            DataTable dtMainGrid = new DataTable();
            try
            {
                dtMainGrid = MeterAnalysisDBBuilder.GetGraphDetails(ObjMATransport);
            }
            catch { }
            return dtMainGrid;
        }

    }
}
