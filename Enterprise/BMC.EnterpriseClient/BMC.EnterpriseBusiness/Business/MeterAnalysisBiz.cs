using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseDataAccess;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.ExceptionManagement;
using System.Data.Linq;
using System.Data;
using System.Data.SqlClient;

namespace BMC.EnterpriseBusiness.Business
{
    public class MeterAnalysisBiz
    {
        #region Local Declaration

        private static MeterAnalysisBiz _MeterAnalysisBiz;

        #endregion Local Declaration

        #region Instance Method
        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <returns>MaintenanceBiz Object</returns>
        public static MeterAnalysisBiz CreateInstance()
        {
            if (_MeterAnalysisBiz == null)
                _MeterAnalysisBiz = new MeterAnalysisBiz();

            return _MeterAnalysisBiz;
        }
        #endregion Instance Method

        #region Methods
        public void GetCommonData(out List<Operators> lstOperator, out List<Machine_Types> lstMachine_Type,
                                    out List<Manufacturers> lstManufacturer, out List<GameCategory> lstGameCategory)
        {
            MeterAnalysisDataContext dbContext = null;
            try
            {
                dbContext = MeterAnalysisDataContextHelper.GetDataContext();
                var result1 = dbContext.GetMeterAnalysis_CommonData();

                try
                {
                    var lstOperatorResult = result1.GetResult<Operator>();
                    lstOperator = (from records in lstOperatorResult
                                   select new Operators
                                   {
                                       Operator_ID = records.Operator_ID,
                                       Operator_Name = records.Operator_Name
                                   }).ToList<Operators>();
                }
                catch (Exception Ex)
                {
                    ExceptionManager.Publish(Ex);
                    lstOperator = new List<Operators>();
                }

                try
                {
                    var lstMachine_TypeResult = result1.GetResult<Machine_Type>();

                    lstMachine_Type = (from records in lstMachine_TypeResult
                                       select new Machine_Types
                                   {
                                       Machine_Type_ID = records.Machine_Type_ID,
                                       Machine_Type_Code = records.Machine_Type_Code
                                   }).ToList<Machine_Types>();
                }
                catch (Exception Ex)
                {
                    ExceptionManager.Publish(Ex);
                    lstMachine_Type = new List<Machine_Types>();
                }

                try
                {
                    var lstManufacturerResult = result1.GetResult<rsp_Manufacturer>();

                    lstManufacturer = (from records in lstManufacturerResult
                                       select new Manufacturers
                                   {
                                       Manufacturer_ID = records.Manufacturer_ID,
                                       Manufacturer_Name = records.Manufacturer_Name
                                   }).ToList<Manufacturers>();
                }
                catch (Exception Ex)
                {
                    ExceptionManager.Publish(Ex);
                    lstManufacturer = new List<Manufacturers>();
                }

                try
                {
                    var lstGameCategoryResult = result1.GetResult<rsp_GetGameCategoryResult>();

                    lstGameCategory = (from records in lstGameCategoryResult
                                       select new GameCategory
                                       {
                                           Game_Category_ID = records.Game_Category_ID,
                                           Game_Category_Name = records.Game_Category_Name
                                       }).ToList<GameCategory>();
                }
                catch (Exception Ex)
                {
                    ExceptionManager.Publish(Ex);
                    lstGameCategory = new List<GameCategory>();
                }

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                lstOperator = new List<Operators>();
                lstMachine_Type = new List<Machine_Types>();
                lstManufacturer = new List<Manufacturers>();
                lstGameCategory = new List<GameCategory>();
            }
        }

        public List<GameTitle> GetGameTitleForCategory(int iID,bool bIsGameDetails)
        {
            List<rsp_GetGameTitleForCategory> resultGetGameTitleForCategory = null;
            List<GameTitle> lstGameTitle = null;

            MeterAnalysisDataContext dbContext = null;
            try
            {
                dbContext = MeterAnalysisDataContextHelper.GetDataContext();
                if(bIsGameDetails)
                    resultGetGameTitleForCategory = dbContext.GetGameTitleForCategory(iID).ToList();
                else
                    resultGetGameTitleForCategory = dbContext.GetMachineClassList(iID).ToList();

                lstGameTitle = (from records in resultGetGameTitleForCategory
                               select new GameTitle
                               {
                                   Game_Title_ID = records.Game_Title_ID,
                                   Game_Title = records.Game_Title
                               }).ToList<GameTitle>();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                lstGameTitle = new List<GameTitle>();
            }

            return lstGameTitle;
        }

        public VSDepotsEntity GetDepots(int operatorId)
        {
            VSDepotsEntity result = new VSDepotsEntity();

            try
            {
                using (MeterAnalysisDataContext db = MeterAnalysisDataContextHelper.GetDataContext())
                {
                    ISingleResult<rsp_GetDepoDetailsResult> dbResults = db.GetDepoList(operatorId);
                    if (dbResults != null)
                    {
                        foreach (rsp_GetDepoDetailsResult dbResult in dbResults)
                        {
                            result.Add(new VSDepotEntity()
                            {
                                Depot_ID = dbResult.Depot_ID,
                                Depot_Name = dbResult.Depot_Name,
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return result;
        }

        public List<BaseDenom> GetBaseDenoms(int iGameTitleID)
        {
            List<BaseDenom> lstBaseDenom = null;
            List<MGMDInstallationDenom> lstDenom = null;
            MeterAnalysisDataContext dataContext = null;
            try
            {
                dataContext = MeterAnalysisDataContextHelper.GetDataContext();
                lstDenom = dataContext.GetBaseDenomForMeterAnalysis(iGameTitleID).ToList();
                lstBaseDenom = (from records in lstDenom
                                select new BaseDenom
                                {
                                    MGMD_Denom = records.MGMD_Denom,
                                    MGMD_Denom_Value = records.MGMD_Denom_Value.ToString()
                                }).ToList<BaseDenom>();


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                lstBaseDenom = new List<BaseDenom>();
            }

            return lstBaseDenom;

        }

        public List<Payout> GetPayoutPercent(int iMGMD_Denom, int iGameTitleID)
        {
            List<Payout> lstPayout = null;
            List<PaytablePercent> lstPayTable = null;
            MeterAnalysisDataContext dataContext = null;
            try
            {
                dataContext = MeterAnalysisDataContextHelper.GetDataContext();
                lstPayTable = dataContext.GetPayoutPercent(iMGMD_Denom, iGameTitleID).ToList();
                lstPayout = (from records in lstPayTable
                             select new Payout
                             {
                                 TheoPayout = records.TheoPayout,
                                 TheoreticalPayout = records.TheoreticalPayout.ToString()
                             }).ToList<Payout>();

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lstPayout;
        }

        public List<OrganisationHierarchy> GetOrganisationHierarchy(int iSiteStatus, int iUser)
        {
            List<rsp_GetSiteDetailsForMAResult> resultOrganisationDetails = null;
            List<OrganisationHierarchy> lstOrganisationDetails = null;

            MeterAnalysisDataContext dbContext = null;
            try
            {
                dbContext = MeterAnalysisDataContextHelper.GetDataContext();
                resultOrganisationDetails = dbContext.GetOrganisationHierarchy(0, 0, 0, 0, 0, 0, iSiteStatus, iUser).ToList();
                lstOrganisationDetails = (from records in resultOrganisationDetails
                                          select new OrganisationHierarchy
                                          {
                                              Company_ID = records.Company_ID,
                                              Company_Name = records.Company_Name,
                                              Site_Code = records.Site_Code,
                                              Site_ID = records.Site_ID,
                                              Site_Name = records.Site_Name,
                                              Sub_Company_Area_ID = records.Sub_Company_Area_ID,
                                              Sub_Company_Area_Name = records.Sub_Company_Area_Name,
                                              Sub_Company_District_ID = records.Sub_Company_District_ID,
                                              Sub_Company_District_Name = records.Sub_Company_District_Name,
                                              Sub_Company_ID = records.Sub_Company_ID,
                                              Sub_Company_Name = records.Sub_Company_Name,
                                              Sub_Company_Region_ID = records.Sub_Company_Region_ID,
                                              Sub_Company_Region_Name = records.Sub_Company_Region_Name
                                          }).ToList<OrganisationHierarchy>();

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                lstOrganisationDetails = new List<OrganisationHierarchy>();
            }

            return lstOrganisationDetails;
        }

        public DataTable GetMeterAnalysisData(MeterAnalysisParams oMAParams, string SP_Name, bool isGameData)
        {
            DataTable dtMeterAnalysis = null;
            MeterAnalysisDataContext dbContext = null;

            try
            {
                dbContext = MeterAnalysisDataContextHelper.GetDataContext();
                SqlParameter[] sqlParams = null;
                if (isGameData)
                {
                    sqlParams = new SqlParameter[23];
                }
                else
                {
                    sqlParams = new SqlParameter[21];
                }
                sqlParams[0] = new SqlParameter("@StartDate", oMAParams.StartDate);
                sqlParams[1] = new SqlParameter("@EndDate", oMAParams.EndDate);
                sqlParams[2] = new SqlParameter("@Company_ID", oMAParams.CompanyID);
                sqlParams[3] = new SqlParameter("@Sub_Company_ID", oMAParams.SubCompanyID);
                sqlParams[4] = new SqlParameter("@Region_ID", oMAParams.RegionID);
                sqlParams[5] = new SqlParameter("@Area_ID", oMAParams.AreaID);
                sqlParams[6] = new SqlParameter("@District_ID", oMAParams.DistrictID);
                sqlParams[7] = new SqlParameter("@Site_ID", oMAParams.SiteID);
                sqlParams[8] = new SqlParameter("@ActiveSites", oMAParams.ActiveSites);
                sqlParams[9] = new SqlParameter("@Operator_ID", oMAParams.OperatorID);
                sqlParams[10] = new SqlParameter("@Depot_ID", oMAParams.DepotID);
                sqlParams[11] = new SqlParameter("@Machine_TypeID", oMAParams.Machine_TypeID);
                sqlParams[12] = new SqlParameter("@ActiveAssets", oMAParams.ActiveAsset);
                sqlParams[13] = new SqlParameter("@Manufacturer_ID", oMAParams.ManufacturerID);
                sqlParams[14] = new SqlParameter("@Game_Category_ID", oMAParams.Game_CategoryID);

                sqlParams[15] = isGameData ? new SqlParameter("@Game_Title_ID", oMAParams.Game_Title_ID) : new SqlParameter("@Game_Title", oMAParams.Game_Title);
                
                sqlParams[16] = new SqlParameter("@GroupBy", oMAParams.GroupByClause);
                sqlParams[17] = new SqlParameter("@NoOfRecords", oMAParams.NoOfRecords);
                sqlParams[18] = new SqlParameter("@OrderBy", oMAParams.Order_By);
                sqlParams[19] = new SqlParameter("@UserID", oMAParams.UserID);
                sqlParams[20] = new SqlParameter("@Period", oMAParams.PeriodID);

                if (isGameData)
                {
                    sqlParams[21] = new SqlParameter("@Denom", oMAParams.Denom);
                    sqlParams[22] = new SqlParameter("@Payout", oMAParams.Payout);
                }

                dtMeterAnalysis = dbContext.GetMeterAnalysisData(sqlParams, SP_Name);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                dtMeterAnalysis = new DataTable();
            }

            return dtMeterAnalysis;
        }

        #endregion Methods
    }
}




