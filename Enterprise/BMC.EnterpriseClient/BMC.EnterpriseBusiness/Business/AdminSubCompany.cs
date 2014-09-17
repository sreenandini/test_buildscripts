using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseDataAccess;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.ExceptionManagement;

namespace BMC.EnterpriseBusiness.Business
{
    public class AdminSubCompany
    {
        EnterpriseDataContext dbContext = null;
        public AdminSubCompany()
        {
            dbContext = EnterpriseDataContextHelper.GetDataContext(); 
        }
        public AdminSubCompanyResult GetSubCompanyDetails(int SubCompanyID)
        {
            AdminSubCompanyResult objAdminSubCompanyResult = null;
            try
            {
                objAdminSubCompanyResult = new AdminSubCompanyResult();
                dbContext = EnterpriseDataContextHelper.GetDataContext();
                var result = dbContext.GetSubCompanyAdminDetails(SubCompanyID);
                var lstSubCompanyResult = result.GetResult<SubCompanyResult>();
                var lstRegionResult = result.GetResult<SubCompanyRegionResult>();
                var lstCompanyDefaults = result.GetResult<CompanyDefaultsResult>();
                var lstTermsResult = result.GetResult<TermsEntity>();
                var lstAccessResult = result.GetResult<SubCompanyAccessInfoResult>();
                var  lstCompanyResult  = result.GetResult<SubCompanyCompanyInfoResult>();
                var lstModelResult  = result.GetResult<SubCompanyModelInfoResult>();
                var  lstHoursResult  = result.GetResult<SubCompanyHourInfoResult>();
                var  lstStaffResult  = result.GetResult<SubCompanyStaffInfoResult>();
                var lstJackpotResult = result.GetResult<SubCompanyJackpotInfoResult>();
                
                

                List<SubCompanyEntity> objSubCompanyEntity = (from records in lstSubCompanyResult
                                                              select new SubCompanyEntity
                                                              {
                                                                  Sub_Company_Name = records.Sub_Company_Name,
                                                                  Company_ID = records.Company_ID,
                                                                  Sub_Company_Switchboard_Phone_No = records.Sub_Company_Switchboard_Phone_No,
                                                                  Sub_Company_Address_1 = records.Sub_Company_Address_1,
                                                                  Sub_Company_Address_2 = records.Sub_Company_Address_2,
                                                                  Sub_Company_Address_3 = records.Sub_Company_Address_3,
                                                                  Sub_Company_Address_4 = records.Sub_Company_Address_4,
                                                                  Sub_Company_Address_5 = records.Sub_Company_Address_5,
                                                                  Sub_Company_Postcode = records.Sub_Company_Postcode,
                                                                  Sub_Company_ANA_Number = records.Sub_Company_ANA_Number,
                                                                  Sub_Company_Income_Ledger_Code = records.Sub_Company_Income_Ledger_Code,
                                                                  Sage_Account_Ref = records.Sage_Account_Ref,
                                                                  Company_Model_Set_ID = records.Company_Model_Set_ID,
                                                                  Sub_Company_Trade_Type = records.Sub_Company_Trade_Type,
                                                                  Sub_Company_Trade_Type1 = records.Sub_Company_Trade_Type1,
                                                                  Sub_Company_Contact_Name = records.Sub_Company_Contact_Name,
                                                                  Sub_Company_Contact_Phone_No = records.Sub_Company_Contact_Phone_No,
                                                                  Sub_Company_Contact_Email_Address = records.Sub_Company_Contact_Email_Address,
                                                                  Sub_Company_Use_Split_Rents = records.Sub_Company_Use_Split_Rents,
                                                                  Sub_Company_Price_Per_Play_Default = records.Sub_Company_Price_Per_Play_Default,
                                                                  Sub_Company_Price_Per_Play = records.Sub_Company_Price_Per_Play,
                                                                  Sub_Company_Jackpot_Default = records.Sub_Company_Jackpot_Default,
                                                                  Sub_Company_Jackpot = records.Sub_Company_Jackpot,
                                                                  Sub_Company_Percentage_Payout_Default = records.Sub_Company_Percentage_Payout_Default,
                                                                  Sub_Company_Percentage_Payout = records.Sub_Company_Percentage_Payout,
                                                                  Terms_Group_ID_Default = records.Terms_Group_ID_Default,
                                                                  Terms_Group_ID = records.Terms_Group_ID,
                                                                  Access_Key_ID_Default = records.Access_Key_ID_Default,
                                                                  Access_Key_ID = records.Access_Key_ID,
                                                                  Staff_ID_Default = records.Staff_ID_Default,
                                                                  Staff_ID = records.Staff_ID,
                                                                  Sub_Company_Default_Opening_Hours_ID = records.Sub_Company_Default_Opening_Hours_ID,
                                                                  Sub_Company_Invoice_Name = records.Sub_Company_Invoice_Name,
                                                                  Sub_Company_Invoice_Address = records.Sub_Company_Invoice_Address,
                                                                  Sub_Company_Invoice_Postcode = records.Sub_Company_Invoice_Postcode,
                                                                  Sub_Company_Account_Name = records.Sub_Company_Account_Name,
                                                                  Sub_Company_Account_No = records.Sub_Company_Account_No,
                                                                  Sub_Company_Sort_Code = records.Sub_Company_Sort_Code

                                                              }).ToList();

                List<SubCompanyRegionEntity> lstRegionEntity = GetSubCompanyRegionList(lstRegionResult);

                List<CompanyDefaultsEntity> CompanyDefaultsEntity = (from records in lstCompanyDefaults
                                                                     select new CompanyDefaultsEntity
                                                    {
                                                        Access_Key_ID = records.Access_Key_ID,
                                                        Company_Jackpot = records.Company_Jackpot,
                                                        Company_Percentage_Payout = records.Company_Percentage_Payout,
                                                        Company_Price_Per_Play = records.Company_Price_Per_Play,
                                                        Staff_ID = records.Staff_ID,
                                                        Terms_Group_ID = records.Terms_Group_ID
                                                    }).ToList<CompanyDefaultsEntity>();

                List<TermsEntity> lstTermsEntity = (from records in lstTermsResult
                                                    select new TermsEntity
                                                    {
                                                        Terms_Group_ID = records.Terms_Group_ID,
                                                        Terms_Group_Name = records.Terms_Group_Name,
                                                    }).ToList<TermsEntity>();

                List<SubCompanyAccessEntity> lstAccessEntity = (from records in lstAccessResult
                                                                select new SubCompanyAccessEntity
                                                    {
                                                        Access_Key_ID = records.Access_Key_ID,
                                                        Access_Key_Name = records.Access_Key_Name
                                                    }).ToList<SubCompanyAccessEntity>();
                
                List<SubCompanyCompanyEntity> lstCompanyEntity = (from records in lstCompanyResult
                                                                  select new SubCompanyCompanyEntity
                                                                {
                                                                    Company_ID = records.Company_ID,
                                                                    Company_Name = records.Company_Name
                                                                }).ToList<SubCompanyCompanyEntity>();
                List<SubCompanyModelEntity> lstModelEntity = (from records in lstModelResult
                                                                select new SubCompanyModelEntity
                                                                  {
                                                                      Company_Model_Set_ID = records.Company_Model_Set_ID,
                                                                      Company_Model_Set_Description =records.Company_Model_Set_Description
                                                                  }).ToList<SubCompanyModelEntity>();

                List<SubCompanyHourEntity> lstHoursEntity = (from records in lstHoursResult
                                                                  select new SubCompanyHourEntity
                                                                  {
                                                                      Standard_Opening_Hours_ID = records.Standard_Opening_Hours_ID,
                                                                      Standard_Opening_Hours_Description = records.Standard_Opening_Hours_Description
                                                                  }).ToList<SubCompanyHourEntity>();

                List<SubCompanyStaffEntity> lstStaffEntity = (from records in lstStaffResult
                                                              select new SubCompanyStaffEntity
                                                             {
                                                                 Staff_ID = records.Staff_ID,
                                                                 StaffName = records.StaffName
                                                             }).ToList<SubCompanyStaffEntity>();


                List<SubCompanyJackpotEntity> lstJackpotEntity = (from records in lstJackpotResult
                                                                select new SubCompanyJackpotEntity
                                                             {
                                                                 JackpotValue = records.Column1,
                                                             }).ToList<SubCompanyJackpotEntity>();

                objAdminSubCompanyResult.RegionEntities = lstRegionEntity;
                objAdminSubCompanyResult.SubCompanyEntity = (objSubCompanyEntity.Count > 0)? objSubCompanyEntity[0]:null;
                objAdminSubCompanyResult.CompanyDefaultsEntity = (CompanyDefaultsEntity.Count > 0) ? CompanyDefaultsEntity[0] : null;
                objAdminSubCompanyResult.TermsEntities = lstTermsEntity;
                objAdminSubCompanyResult.AccessEntities = lstAccessEntity;
                objAdminSubCompanyResult.CompanyEntities = lstCompanyEntity;
                objAdminSubCompanyResult.ModelEntities = lstModelEntity;
                objAdminSubCompanyResult.HoursEntities = lstHoursEntity;
                objAdminSubCompanyResult.StaffEntities = lstStaffEntity;
                objAdminSubCompanyResult.JackpotEntities = lstJackpotEntity;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);

            }
            finally
            {
                dbContext.Dispose();
            }
            return objAdminSubCompanyResult;
        }

        public List<SubCompanyRegionEntity> GetSubCompanyRegionDetails(int SubCompanyID)
        {
            dbContext = EnterpriseDataContextHelper.GetDataContext();
            return GetSubCompanyRegionList(dbContext.GetSubCompanyRegionDetails(SubCompanyID));
        }

        public List<SubCompanyAreaEntity> GetSubCompanyAreaDetails(int SubCompanyRegionID)
        {
            dbContext = EnterpriseDataContextHelper.GetDataContext();
            var result = dbContext.GetSubCompanyAreaDetails(SubCompanyRegionID);
            List<SubCompanyAreaEntity> lstAreaEntity = (from record in result
                                                        select new SubCompanyAreaEntity
                                                        {
                                                            Staff_First_Name = record.Staff_First_Name,
                                                            Staff_Last_Name = record.Staff_Last_Name,
                                                            Staff_ID = record.Staff_ID,
                                                            Sub_Company_Area_Description = record.Sub_Company_Area_Description,
                                                            Sub_Company_Area_ID = record.Sub_Company_Area_ID,
                                                            Sub_Company_Area_Name = record.Sub_Company_Area_Name
                                                        }).ToList<SubCompanyAreaEntity>();
            return lstAreaEntity;

        }

        public List<SubCompanyDistrictEntity> GetSubCompanyDistrictDetails(int SubCompanyAreaID)
        {
            dbContext = EnterpriseDataContextHelper.GetDataContext();
            var result = dbContext.GetSubCompanyDistrictDetails(SubCompanyAreaID);
            List<SubCompanyDistrictEntity> lstDistrictEntity = (from record in result
                                                            select new SubCompanyDistrictEntity
                                                        {
                                                            Staff_First_Name = record.Staff_First_Name,
                                                            Staff_Last_Name = record.Staff_Last_Name,
                                                            Staff_ID = record.Staff_ID,
                                                            Sub_Company_District_Description = record.Sub_Company_District_Description,
                                                            Sub_Company_District_ID = record.Sub_Company_District_ID,
                                                            Sub_Company_District_Name = record.Sub_Company_District_Name
                                                        }).ToList<SubCompanyDistrictEntity>();
            return lstDistrictEntity;

        }

        private List<SubCompanyRegionEntity> GetSubCompanyRegionList(IEnumerable<SubCompanyRegionResult> lstRegionResult)
        {
            List<SubCompanyRegionEntity> lstRegionEntity = (from records in lstRegionResult
                                                            select new SubCompanyRegionEntity
                                                            {
                                                                Sub_Company_Region_ID = records.Sub_Company_Region_ID,
                                                                Sub_Company_Region_Name = records.Sub_Company_Region_Name,
                                                                Sub_Company_Region_Description = records.Sub_Company_Region_Description,
                                                                Staff_ID = records.Staff_ID,
                                                                Staff_Last_Name = records.Staff_Last_Name,
                                                                Staff_First_Name = records.Staff_First_Name
                                                            }).ToList<SubCompanyRegionEntity>();
            return lstRegionEntity;
        }

        public string UpdateSubCompanyRegion(SubCompanyRegionEntity _SubCompanyRegionEntity)
        {

            string errMessage = string.Empty;
            int? errCode = 0;
            dbContext = EnterpriseDataContextHelper.GetDataContext();
            dbContext.UpdateSubCompanyRegion(_SubCompanyRegionEntity.Sub_Company_Region_ID, _SubCompanyRegionEntity.Sub_Company_Region_Name, _SubCompanyRegionEntity.Sub_Company_Region_Description, _SubCompanyRegionEntity.Staff_ID, _SubCompanyRegionEntity.Sub_Company_ID, 0, ref errCode, ref errMessage);
            return errMessage;
        }

        public string UpdateSubCompanyArea(SubCompanyAreaEntity _SubCompanyAreaEntity)
        {
            string errMessage = string.Empty;
            int? errCode = 0;
            dbContext = EnterpriseDataContextHelper.GetDataContext();
            dbContext.UpdateSubCompanyArea(_SubCompanyAreaEntity.Sub_Company_Area_ID, _SubCompanyAreaEntity.Sub_Company_Area_Name, _SubCompanyAreaEntity.Sub_Company_Area_Description, _SubCompanyAreaEntity.Staff_ID, _SubCompanyAreaEntity.Sub_Company_Region_ID, ref errCode, ref errMessage);
            return errMessage;
        }

        public string UpdateSubCompanyDistrict(SubCompanyDistrictEntity _SubCompanyDistrictEntity)
        {
            string errMessage = string.Empty;
            int? errCode = 0;
            dbContext = EnterpriseDataContextHelper.GetDataContext();
            dbContext.UpdateSubCompanyDistrict(_SubCompanyDistrictEntity.Sub_Company_District_ID, _SubCompanyDistrictEntity.Sub_Company_District_Name, _SubCompanyDistrictEntity.Sub_Company_District_Description, _SubCompanyDistrictEntity.Staff_ID, _SubCompanyDistrictEntity.Sub_Company_Area_ID, ref errCode, ref errMessage);
            return errMessage;
        }

        public bool IsSubCompanyExist(string subCompanyName, int? companyID, int? subCompanyID)
        {
            dbContext = EnterpriseDataContextHelper.GetDataContext();
            return Convert.ToBoolean(dbContext.IsSubCompanyExist(subCompanyName,companyID,subCompanyID));
        }

        public bool UpdateSubCompany(ref int? subCompanyID, SubCompanyEntity _SubCompanyEntity, bool updateAllSites)
        {
            dbContext = EnterpriseDataContextHelper.GetDataContext();
            return dbContext.UpdateSubCompany(ref subCompanyID,
                                            _SubCompanyEntity.Sub_Company_Name,
                                            _SubCompanyEntity.Company_ID,
                                            _SubCompanyEntity.Sub_Company_Switchboard_Phone_No,
                                            _SubCompanyEntity.Sub_Company_Address_1,
                                            _SubCompanyEntity.Sub_Company_Address_2,
                                            _SubCompanyEntity.Sub_Company_Address_3,
                                            _SubCompanyEntity.Sub_Company_Address_4,
                                            _SubCompanyEntity.Sub_Company_Address_5,
                                            _SubCompanyEntity.Sub_Company_Postcode,
                                            _SubCompanyEntity.Sub_Company_ANA_Number,
                                            _SubCompanyEntity.Sub_Company_Income_Ledger_Code,
                                            _SubCompanyEntity.Sage_Account_Ref,
                                            _SubCompanyEntity.Company_Model_Set_ID,
                                            _SubCompanyEntity.Sub_Company_Trade_Type,
                                            _SubCompanyEntity.Sub_Company_Contact_Name,
                                            _SubCompanyEntity.Sub_Company_Contact_Phone_No,
                                            _SubCompanyEntity.Sub_Company_Contact_Email_Address,
                                            _SubCompanyEntity.Sub_Company_Use_Split_Rents,
                                            _SubCompanyEntity.Sub_Company_Price_Per_Play_Default,
                                            _SubCompanyEntity.Sub_Company_Price_Per_Play,
                                            _SubCompanyEntity.Sub_Company_Jackpot_Default,
                                            _SubCompanyEntity.Sub_Company_Jackpot,
                                            _SubCompanyEntity.Sub_Company_Percentage_Payout_Default,
                                            _SubCompanyEntity.Sub_Company_Percentage_Payout,
                                            _SubCompanyEntity.Terms_Group_ID_Default,
                                            _SubCompanyEntity.Terms_Group_ID,
                                            _SubCompanyEntity.Access_Key_ID_Default,
                                            _SubCompanyEntity.Access_Key_ID,
                                            _SubCompanyEntity.Staff_ID_Default,
                                            _SubCompanyEntity.Staff_ID,
                                            _SubCompanyEntity.Sub_Company_Default_Opening_Hours_ID,
                                            _SubCompanyEntity.Sub_Company_Invoice_Name,
                                            _SubCompanyEntity.Sub_Company_Invoice_Address,
                                            _SubCompanyEntity.Sub_Company_Invoice_Postcode,
                                            _SubCompanyEntity.Sub_Company_Account_Name,
                                            _SubCompanyEntity.Sub_Company_Account_No,
                                            _SubCompanyEntity.Sub_Company_Sort_Code,
                                            updateAllSites) == 0;


        }

        public void UpdateSubCompanyAdminDefaults(
                    System.Nullable<int> company_ID,
                    System.Nullable<bool> bAccess_Key_ID,
                    System.Nullable<bool> bCompany_Jackpot,
                    System.Nullable<bool> bCompany_Percentage_Payout,
                    System.Nullable<bool> bCompany_Price_Per_Play,
                    System.Nullable<bool> bStaff_ID,
                    System.Nullable<bool> bTerms_Group_ID,
                    System.Nullable<long> value,
                    System.Nullable<int> cascadeType,
                    System.Nullable<int> level,
                    System.Nullable<bool> isDefault,
                    System.Nullable<int> audit_User_ID,
                    string audit_User_Name,
                    string auditOperationType,
                    string audit_ModuleName,
                    System.Nullable<int> subCompanyID)
        {
            dbContext = EnterpriseDataContextHelper.GetDataContext();
            dbContext.UpdateSubCompanyAdminDefaults(company_ID,bAccess_Key_ID,bCompany_Jackpot,bCompany_Percentage_Payout,bCompany_Price_Per_Play,bStaff_ID,bTerms_Group_ID,value,cascadeType,level,isDefault,audit_User_ID,audit_User_Name,auditOperationType,audit_ModuleName,subCompanyID);
        }
    }
}
