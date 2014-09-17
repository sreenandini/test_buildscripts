using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.EnterpriseDataAccess;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.ExceptionManagement;


namespace BMC.EnterpriseBusiness.Business
{

    public class AdminCompany
    {
        public AdminCompanyResult GetCompanyDetails(int CompanyID)
        {
            EnterpriseDataContext dbContext = null;
            AdminCompanyResult objAdminCompanyResult = null;
            try
            {
                objAdminCompanyResult = new AdminCompanyResult();
                dbContext = EnterpriseDataContextHelper.GetDataContext();
                var result1 = dbContext.GetCompanyAdminDetails(CompanyID);
                var lstTermsResult = result1.GetResult<TermsResult>();
                var lstAccessKeyResult = result1.GetResult<AccessKeyResult>();
                var lstStaffResult = result1.GetResult<StaffResult>();
                var lstCompanyResult = result1.GetResult<CompanyResult>();


                List<TermsEntity> lstTermsEntity = (from records in lstTermsResult
                                                    select new TermsEntity
                                                    {
                                                        Terms_Group_ID = records.Terms_Group_ID,
                                                        Terms_Group_Name = records.Terms_Group_Name,
                                                    }).ToList<TermsEntity>();

                List<AccessKeyEntity> lstAccessKeyEntity = (from records in lstAccessKeyResult
                                                            select new AccessKeyEntity
                                                            {
                                                                Access_Key_ID = records.Access_Key_ID,
                                                                Access_Key_Name = records.Access_Key_Name,
                                                                Access_Key_Ref = records.Access_Key_Ref,
                                                                Access_Key_Manufacturer = records.Access_Key_Manufacturer,
                                                                Access_Key_Type = records.Access_Key_Type
                                                            }).ToList<AccessKeyEntity>();

                List<StaffEntity> lstStaffEntity = (from records in lstStaffResult
                                                    select new StaffEntity
                                                        {
                                                            Staff_ID = records.Staff_ID,
                                                            Staff_Last_Name = records.Staff_Last_Name,
                                                            Staff_First_Name = records.Staff_First_Name
                                                        }).ToList<StaffEntity>();


                List<CompanyEntity> objCompanyEntity = (from records in lstCompanyResult
                                                        select new CompanyEntity
                                                            {
                                                                Company_Name = records.Company_Name,
                                                                Company_Switchboard_Phone_No = records.Company_Switchboard_Phone_No,
                                                                Company_Address_1 = records.Company_Address_1,
                                                                Company_Address_2 = records.Company_Address_2,
                                                                Company_Address_3 = records.Company_Address_3,
                                                                Company_Address_4 = records.Company_Address_4,
                                                                Company_Address_5 = records.Company_Address_5,
                                                                Company_Postcode = records.Company_Postcode,
                                                                Company_Contact_Name = records.Company_Contact_Name,
                                                                Company_Contact_Phone_No = records.Company_Contact_Phone_No,
                                                                Company_Contact_Email_Address = records.Company_Contact_Email_Address,
                                                                Company_Price_Per_Play = records.Company_Price_Per_Play,
                                                                Company_Jackpot = records.Company_Jackpot,
                                                                Company_Percentage_Payout = records.Company_Percentage_Payout,
                                                                Terms_Group_ID = records.Terms_Group_ID.SafeValue<int>(),
                                                                Access_Key_ID = records.Access_Key_ID.SafeValue<int>(),
                                                                Staff_ID = records.Staff_ID.SafeValue<int>(),
                                                                Company_Invoice_Name = records.Company_Invoice_Name,
                                                                Company_Invoice_Address = records.Company_Invoice_Address,
                                                                Company_Invoice_Postcode = records.Company_Invoice_Postcode
                                                            }).ToList();

                objAdminCompanyResult.AccessKeyEntitys = lstAccessKeyEntity;
                objAdminCompanyResult.CompanyEntity = (objCompanyEntity.Count > 0) ? objCompanyEntity.Single() : null;
                objAdminCompanyResult.StaffEntitys = lstStaffEntity;
                objAdminCompanyResult.TermsEntitys = lstTermsEntity;
  
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);

            }
            finally
            {
                dbContext.Dispose();
            }
            return objAdminCompanyResult;
        }
        public int UpdateCompanyDetails(int CompanyID, CompanyEntity objCompany)
        {
            EnterpriseDataContext dbContext = EnterpriseDataContextHelper.GetDataContext();
            return dbContext.UpdateCompanyDetails(CompanyID, objCompany.Company_Name, objCompany.Company_Switchboard_Phone_No, objCompany.Company_Address_1, objCompany.Company_Address_2, objCompany.Company_Address_3, objCompany.Company_Address_4, objCompany.Company_Address_5, objCompany.Company_Postcode, objCompany.Company_Contact_Name, objCompany.Company_Contact_Phone_No, objCompany.Company_Contact_Email_Address, objCompany.Company_Invoice_Name, objCompany.Company_Invoice_Address, objCompany.Company_Invoice_Postcode).Single().CompanyID.SafeValue(); ;

        }

        public List<CompanyDecendants> UpdateCompanyAdminDefaults(int company_ID, bool bAccess_Key_ID, bool bCompany_Jackpot, bool bCompany_Percentage_Payout, bool bCompany_Price_Per_Play, bool bStaff_ID, bool bTerms_Group_ID, long value, int cascadeType, bool isDefault,int audit_User_ID,string audit_User_Name, string auditOperationType)
        {
            EnterpriseDataContext dbContext = EnterpriseDataContextHelper.GetDataContext();
            ISingleResult<usp_ecUpdateCompanyAdminDefaultsResult> Result = dbContext.UpdateCompanyAdminDefaults(company_ID, bAccess_Key_ID, bCompany_Jackpot, bCompany_Percentage_Payout, bCompany_Price_Per_Play, bStaff_ID, bTerms_Group_ID, value, cascadeType, isDefault,audit_User_ID, audit_User_Name, auditOperationType);
            List<CompanyDecendants> objCompanyDecendants = null;
            if (Result != null)
            {
                 objCompanyDecendants = (from o in Result
                                                                select new CompanyDecendants()
                                                                {
                                                                    ID = o.ID.SafeValue<int>(),
                                                                    FieldName=o.FieldName,
                                                                    Type = o.Type,
                                                                }).ToList();
            }                                    
            return objCompanyDecendants;
        }


    }
    
}
