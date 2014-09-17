/// Source File Name : MeterAnalysisDBBuilder.cs
/// Description		 : Business layer for meter analysis - call SQLHelper to get data
/// Revision History
/// Author             Date              Description
/// ---------------------------------------------------
/// Madhusudhanan      13/5/08          created
using System;
using System.Collections.Generic;
using System.Data.Sql;
using System.Data;

using System.Text;
using BMC.Common.ConfigurationManagement;
using System.Configuration;
using Microsoft.Win32;
using BMC.Common;
using BMC.DataAccess;
using System.Data.SqlClient;
using BMC.Business.Classes;
using BMC.Common.LogManagement;

namespace BMC.Business.Classes.DBBuilder
{
    /// <summary>
    /// DBBuilder - Business layer for MeterAnalysis 
    /// </summary>
    /// <Author>Madhusudhanan</Author>
    /// <DateCreated>13-5-2008</DateCreated>
    ///
    /// Class Revision History
    ///
    /// Author             Date              Description
    /// ---------------------------------------------------
    ///madhusudhanan        13-05-08        Created

    class MeterAnalysisDBBuilder
    {
        /// <summary>
        /// Retrieves connnection string from the registry
        /// </summary>
        /// <Author>Madhusudhanan</Author>
        /// <DateCreated>15-5-2008</DateCreated>
        /// <Parameters></Parameters>
        /// <returns>connection string</returns>
        static string GetConnectionString()
        {
            try
            {
                string strConnectionString = "";
                //bool bUseHex = true;
                //RegistryKey RegKey = Registry.LocalMachine.OpenSubKey("Software\\Honeyframe\\CashmasterHQ\\Settings");
                //strConnectionString = RegKey.GetValue("SQLConnect").ToString();
                //if (!strConnectionString.ToUpper().Contains("SERVER"))
                //{
                //    BGSGeneral.cConstants objBGSConstants = new BGSGeneral.cConstants();
                //    BGSEncryptDecrypt.clsBlowFish objDecrypt = new BGSEncryptDecrypt.clsBlowFish();
                //    string strKey = objBGSConstants.ENCRYPTIONKEY;
                //    strConnectionString = objDecrypt.DecryptString(ref strConnectionString, ref strKey, ref bUseHex);

                //}
                //strConnectionString = strConnectionString.Replace("Enterprise", "MeterAnalysis");
                //RegKey.Close();
                //if (string.IsNullOrEmpty(strConnectionString))
                //{
                //    LogManager.WriteLog("Error in retrieving connection string from registry", LogManager.enumLogLevel.Error);
                //}

                //return strConnectionString;
                strConnectionString= Common.Utilities.DatabaseHelper.GetConnectionString();
                return strConnectionString.Replace("Enterprise", "MeterAnalysis");
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error retrieving connection string from registry. " + "Error :" + ex.Message, LogManager.enumLogLevel.Error);
                return string.Empty;
            }

        }

        /// <summary>
        ///  Gets the data to load the tree view
        /// </summary>
        /// <Author>Madhusudhanan</Author>
        /// <DateCreated>15-5-2008</DateCreated>
        /// <Parameters></Parameters>
        /// <returns>Datatable</returns>
        public static SqlDataReader GetSitesList(MeterAnalysisTransport ObjMATransport)
        {
            SqlDataReader drSites = null;
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[8];
                sqlParams[0] = new SqlParameter("@SiteID", ObjMATransport.SiteID);
                sqlParams[1] = new SqlParameter("@DistrictID", ObjMATransport.DistrictID);
                sqlParams[2] = new SqlParameter("@AreaID", ObjMATransport.AreaID);
                sqlParams[3] = new SqlParameter("@RegionID", ObjMATransport.RegionID);
                sqlParams[4] = new SqlParameter("@SubCompanyID", ObjMATransport.SubCompanyID);
                sqlParams[5] = new SqlParameter("@CompanyID", ObjMATransport.CompanyID);
                sqlParams[6] = new SqlParameter("@SiteStatusID", ObjMATransport.SiteStatusID);
                sqlParams[7] = new SqlParameter("@UserID", ObjMATransport.UserID);
                drSites = SqlHelper.ExecuteReader(GetConnectionString(), SqlProcedures.CONST_SITE_DETAILS_PROC, sqlParams);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in getting site deatils for treeview.  " + "Error :" + ex.Message, LogManager.enumLogLevel.Error);
            }
            return drSites;
        }

        /// <summary>
        ///  Gets the site details for the selected tree node
        /// </summary>
        /// <Author>Madhusudhanan</Author>
        /// <DateCreated>15-5-2008</DateCreated>
        /// <Parameters></Parameters>
        /// <returns>Datatable</returns>
        public static DataTable GetSiteDetails(MeterAnalysisTransport ObjMATransport)
        {
            try
            {
                DataSet dsSiteDetails = new DataSet();
                SqlParameter[] sqlParams = new SqlParameter[8];
                sqlParams[0] = new SqlParameter("@SiteID", ObjMATransport.SiteID);
                sqlParams[1] = new SqlParameter("@DistrictID", ObjMATransport.DistrictID);
                sqlParams[2] = new SqlParameter("@AreaID", ObjMATransport.AreaID);
                sqlParams[3] = new SqlParameter("@RegionID", ObjMATransport.RegionID);
                sqlParams[4] = new SqlParameter("@SubCompanyID", ObjMATransport.SubCompanyID);
                sqlParams[5] = new SqlParameter("@CompanyID", ObjMATransport.CompanyID);
                sqlParams[6] = new SqlParameter("@SiteStatusID", ObjMATransport.SiteStatusID);
                sqlParams[7] = new SqlParameter("@UserID", ObjMATransport.UserID);

                string strConnectionString = GetConnectionString();
                if (!string.IsNullOrEmpty(strConnectionString))
                {
                    SqlHelper.FillDataset(strConnectionString, SqlProcedures.CONST_SITE_DETAILS_PROC, dsSiteDetails, new string[] { "SiteDetails" }, sqlParams);
                    return dsSiteDetails.Tables["SiteDetails"];
                }

            }
            catch
            {
                LogManager.WriteLog("Error in getting site deatils for selected node", LogManager.enumLogLevel.Error);
            }
            return new DataTable();

        }

        /// <summary>
        ///  Gets the data for combobox
        /// </summary>
        /// <Author>Madhusudhanan</Author>
        /// <DateCreated>15-5-2008</DateCreated>
        /// <Parameters></Parameters>
        /// <returns>Datatable</returns>
        public static DataTable GetList(string strListName, Int32 iID)
        {
            DataSet dsList = new DataSet();
            try
            {
                SqlParameter[] sqlParams;
                string strConnectionString = GetConnectionString();
                if (!string.IsNullOrEmpty(strConnectionString))
                {
                    //call the respective SP based on the Listname 
                    switch (strListName)
                    {
                        case "Category":
                            SqlHelper.FillDataset(strConnectionString, CommandType.Text, SqlProcedures.CONST_CATEGORY_QUERY, dsList, new string[] { "Category" });
                            break;
                        case "Operator":
                            SqlHelper.FillDataset(strConnectionString, CommandType.Text, SqlProcedures.CONST_OPERATOR_QUERY, dsList, new string[] { "operator" });
                            break;
                        case "Types":
                            SqlHelper.FillDataset(strConnectionString, CommandType.Text, SqlProcedures.CONST_TYPE_QUERY, dsList, new string[] { "Types" });
                            break;
                        case "Manufacturer":
                            SqlHelper.FillDataset(strConnectionString, CommandType.Text, SqlProcedures.CONST_MANUFACTURER_QUERY, dsList, new string[] { "Manufacturers" });
                            break;
                        case "Depot":
                            sqlParams = new SqlParameter[1];
                            sqlParams[0] = new SqlParameter("@TypeID", iID);
                            SqlHelper.FillDataset(strConnectionString, SqlProcedures.CONST_DEPOT_PROC, dsList, new string[] { "Depot" }, sqlParams);
                            break;
                        case "MachineClass":
                            sqlParams = new SqlParameter[1];
                            sqlParams[0] = new SqlParameter("@SupplierID", iID);
                            SqlHelper.FillDataset(strConnectionString, SqlProcedures.CONST_MACHINE_CLASS_PROC, dsList, new string[] { "MachineClass" }, sqlParams);
                            break;
                    }
                    return dsList.Tables[0];

                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in getting data for combobox :" + strListName + "Error :" + ex.Message, LogManager.enumLogLevel.Error);

            }
            return new DataTable();
        }

        /// <summary>
        ///  Gets the data for main grid 
        /// </summary>
        /// <Author>Madhusudhanan</Author>
        /// <DateCreated>15-5-2008</DateCreated>
        /// <Parameters></Parameters>
        /// <returns>Datatable</returns>
        public static DataTable GetMainGridDetails(MeterAnalysisTransport ObjMATransport, Boolean blnDataForGrid)
        {
            DataSet dsMainGrid = new DataSet();
            try
            {
                string strConnectionString = GetConnectionString();
                if (!string.IsNullOrEmpty(strConnectionString))
                {

                    SqlParameter[] sqlParams = new SqlParameter[24];
                    sqlParams[0] = new SqlParameter("@StartDate", ObjMATransport.StartDate);
                    sqlParams[1] = new SqlParameter("@EndDate", ObjMATransport.EndDate);
                    sqlParams[2] = new SqlParameter("@Company_ID", ObjMATransport.CompanyID);
                    sqlParams[3] = new SqlParameter("@Sub_Company_ID", ObjMATransport.SubCompanyID);
                    sqlParams[4] = new SqlParameter("@Site_ID", ObjMATransport.SiteID);
                    sqlParams[5] = new SqlParameter("@Operator_ID", ObjMATransport.OperatorID);
                    sqlParams[6] = new SqlParameter("@Depot_ID", ObjMATransport.DepotID);
                    sqlParams[7] = new SqlParameter("@Type", ObjMATransport.TypeID);
                    sqlParams[8] = new SqlParameter("@Category", ObjMATransport.CategoryID);
                    sqlParams[9] = new SqlParameter("@Class", ObjMATransport.ClassID);

                    sqlParams[10] = new SqlParameter("@Region_ID", ObjMATransport.RegionID);
                    sqlParams[11] = new SqlParameter("@Area_ID", ObjMATransport.AreaID);
                    sqlParams[12] = new SqlParameter("@District_ID", ObjMATransport.DistrictID);
                    sqlParams[13] = new SqlParameter("@Manufacturer_ID", ObjMATransport.ManufacturerID);
                    sqlParams[15] = new SqlParameter("@Period", ObjMATransport.PeriodID);
                    //for grid
                    if (blnDataForGrid)
                    {
                        sqlParams[14] = new SqlParameter("@GroupBy", ObjMATransport.GroupByClause);
                        sqlParams[16] = new SqlParameter("@Search_MC_ID", ObjMATransport.SearchMCID);
                        sqlParams[17] = new SqlParameter("@Search_Asset", ObjMATransport.SearchAsset);
                        sqlParams[18] = new SqlParameter("@Search_Installation_ID", ObjMATransport.SearchInstallationID);
                        sqlParams[19] = new SqlParameter("@NoOfRecords", ObjMATransport.NoOfRecords);
                        sqlParams[22] = new SqlParameter("@MeterName", ObjMATransport.Criteria);
                    }
                    //graph
                    else
                    {
                        sqlParams[14] = new SqlParameter("@GroupBy", string.Empty);
                        sqlParams[16] = new SqlParameter("@Search_MC_ID", DBNull.Value);
                        sqlParams[17] = new SqlParameter("@Search_Asset", string.Empty);
                        sqlParams[18] = new SqlParameter("@Search_Installation_ID", DBNull.Value);
                        sqlParams[19] = new SqlParameter("@NoOfRecords", string.Empty);
                        sqlParams[22] = new SqlParameter("@MeterName", string.Empty);
                    }
                    sqlParams[20] = new SqlParameter("@Active", ObjMATransport.Active);
                    sqlParams[21] = new SqlParameter("@ActiveSites", ObjMATransport.IsOnlyActiveSites);
                    sqlParams[23] = new SqlParameter("@UserID", ObjMATransport.UserID);
                    SqlHelper.FillDataset(strConnectionString, SqlProcedures.CONST_LOAD_GRID_PROC, dsMainGrid, new string[] { "MainGrid" }, sqlParams);
                    return dsMainGrid.Tables[0];
                }
                else
                {
                    return new DataTable();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in getting data for main grid --" + ex.Message, LogManager.enumLogLevel.Error);
                return new DataTable();
            }

        }
        /// <summary>
        ///  Gets the data for graph
        /// </summary>
        /// <Author>Madhusudhanan</Author>
        /// <DateCreated>15-5-2008</DateCreated>
        /// <Parameters></Parameters>
        /// <returns>Datatable</returns>
        public static DataTable GetGraphDetails(MeterAnalysisTransport ObjMATransport)
        {
            DataSet dsMainGrid = new DataSet();
            try
            {
                string strConnectionString = GetConnectionString();
                if (!string.IsNullOrEmpty(strConnectionString))
                {


                    //SqlParameter[] sqlParams = new SqlParameter[17];
                    //sqlParams[0] = new SqlParameter("@StartDate", ObjMATransport.StartDate);
                    //sqlParams[1] = new SqlParameter("@EndDate", ObjMATransport.EndDate);
                    //sqlParams[2] = new SqlParameter("@Company_ID", ObjMATransport.CompanyID);
                    //sqlParams[3] = new SqlParameter("@Sub_Company_ID", ObjMATransport.SubCompanyID);
                    //sqlParams[4] = new SqlParameter("@Site_ID", ObjMATransport.SiteID);
                    //sqlParams[5] = new SqlParameter("@Operator_ID", ObjMATransport.OperatorID);
                    //sqlParams[6] = new SqlParameter("@Depot_ID", ObjMATransport.DepotID);
                    //sqlParams[7] = new SqlParameter("@Type", ObjMATransport.TypeID);
                    //sqlParams[8] = new SqlParameter("@Category", ObjMATransport.CategoryID);
                    //sqlParams[9] = new SqlParameter("@Class", ObjMATransport.ClassID);

                    //sqlParams[10] = new SqlParameter("@Region_ID", ObjMATransport.RegionID);
                    //sqlParams[11] = new SqlParameter("@Area_ID", ObjMATransport.AreaID);
                    //sqlParams[12] = new SqlParameter("@District_ID", ObjMATransport.DistrictID);
                    //sqlParams[13] = new SqlParameter("@Manufacturer_ID", ObjMATransport.ManufacturerID);
                    //sqlParams[14] = new SqlParameter("@GroupBy", ObjMATransport.GroupByClause);
                    //sqlParams[15] = new SqlParameter("@Period", ObjMATransport.PeriodID);
                    //sqlParams[16] = new SqlParameter("@Active", ObjMATransport.Active);
                    //sqlParams[16] = new SqlParameter("@ActiveSites", ObjMATransport.IsOnlyActiveSites);

                    SqlParameter[] sqlParams = new SqlParameter[23];
                    sqlParams[0] = new SqlParameter("@StartDate", ObjMATransport.StartDate);
                    sqlParams[1] = new SqlParameter("@EndDate", ObjMATransport.EndDate);
                    sqlParams[2] = new SqlParameter("@Company_ID", ObjMATransport.CompanyID);
                    sqlParams[3] = new SqlParameter("@Sub_Company_ID", ObjMATransport.SubCompanyID);
                    sqlParams[4] = new SqlParameter("@Site_ID", ObjMATransport.SiteID);
                    sqlParams[5] = new SqlParameter("@Operator_ID", ObjMATransport.OperatorID);
                    sqlParams[6] = new SqlParameter("@Depot_ID", ObjMATransport.DepotID);
                    sqlParams[7] = new SqlParameter("@Type", ObjMATransport.TypeID);
                    sqlParams[8] = new SqlParameter("@Category", ObjMATransport.CategoryID);
                    sqlParams[9] = new SqlParameter("@Class", ObjMATransport.ClassID);

                    sqlParams[10] = new SqlParameter("@Region_ID", ObjMATransport.RegionID);
                    sqlParams[11] = new SqlParameter("@Area_ID", ObjMATransport.AreaID);
                    sqlParams[12] = new SqlParameter("@District_ID", ObjMATransport.DistrictID);
                    sqlParams[13] = new SqlParameter("@Manufacturer_ID", ObjMATransport.ManufacturerID);
                    sqlParams[15] = new SqlParameter("@Period", ObjMATransport.PeriodID);

                    sqlParams[14] = new SqlParameter("@GroupBy", ObjMATransport.GroupByClause);
                    sqlParams[16] = new SqlParameter("@Search_MC_ID", DBNull.Value);
                    sqlParams[17] = new SqlParameter("@Search_Asset", string.Empty);
                    sqlParams[18] = new SqlParameter("@Search_Installation_ID", DBNull.Value);
                    sqlParams[19] = new SqlParameter("@NoOfRecords", string.Empty);

                    sqlParams[20] = new SqlParameter("@Active", ObjMATransport.Active);
                    sqlParams[21] = new SqlParameter("@ActiveSites", ObjMATransport.IsOnlyActiveSites);
                    sqlParams[22] = new SqlParameter("@UserID", ObjMATransport.UserID);
                    SqlHelper.FillDataset(strConnectionString, SqlProcedures.CONST_LOAD_GRAPH_PROC, dsMainGrid, new string[] { "MainGrid" }, sqlParams);
                    return dsMainGrid.Tables[0];
                }
                else
                {
                    return new DataTable();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in getting data for graph --" + ex.Message, LogManager.enumLogLevel.Error);
                return new DataTable();
            }

        }

    }
}
