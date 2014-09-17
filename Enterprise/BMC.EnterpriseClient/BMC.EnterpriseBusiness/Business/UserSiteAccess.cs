using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseDataAccess;
using System.Data.Linq;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.ExceptionManagement;

namespace BMC.EnterpriseBusiness.Business.UserSiteAccess
{
    public class UserSiteAccess
    {
        #region "Declarations"
        /// <summary>
        /// Declarations
        /// </summary>

        private static UserSiteAccess _instance = null;
        private static EnterpriseDataContext _context = null;
        #endregion

        #region "Instance Creation"
        /// <summary>
        /// This function is used to create instance of this class object
        /// </summary>
        /// <returns>instance of this class</returns>
        public static UserSiteAccess CreateInstance()
        {
            _context = EnterpriseDataContextHelper.GetDataContext();
            if (_instance == null)
            {
                _instance = new UserSiteAccess();
            }
            return _instance;

        }

        #endregion

        #region ProcedureCalls
        /// <summary>
        /// This function is used to get all the companies for the site access 
        /// </summary>
        /// <returns>all companies</returns>
        /// 

        public List<CompaniesResult> GetAllCompanies()
        {
            try
            {
                List<CompaniesResult> result =
                        (from AH in _context.GetSiteAccessCompanies()
                         orderby AH.Company_ID,AH.Sub_Company_ID ascending
                         select new CompaniesResult
                         {
                             Company_ID = AH.Company_ID,
                             Company_Name = AH.Company_Name,
                             Sub_Company_Name = AH.Sub_Company_Name,
                             Sub_Company_ID = AH.Sub_Company_ID
                         }).Distinct().ToList();


                return result;
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
                return null;
            }
        }
        #endregion

        #region ProcedureCalls
        /// <summary>
        /// This function is used to get all the Operators for the site access 
        /// </summary>
        /// <returns>all Operators</returns>
        /// 

        public List<OperatorsResult> GetAllOperators()
        {
            try
            {
                List<OperatorsResult> result =
                        (from AH in _context.GetSiteAccessOperators()
                         orderby AH.Operator_ID ascending
                         select new OperatorsResult
                            {
                                Operator_ID = AH.Operator_ID,
                                Operator_Name = AH.Operator_Name,
                                Depot_ID = AH.Depot_ID,
                                Depot_Name = AH.Depot_Name
                            }).Distinct().ToList();

                return result;
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
                return null;
            }
        }
        #endregion


        #region ProcedureCalls
        /// <summary>
        /// This function is used to get all the Sites for the customer access 
        /// </summary>
        /// <returns>all Operators</returns>
        /// 

        public List<SitesResult> GetAllSites()
        {
            try
            {
                List<SitesResult> result =
                        (from AH in _context.GetSiteAccessSites()
                         orderby AH.Site_Name ascending
                         select new SitesResult
                         {
                             Site_ID = AH.Site_ID,
                             Site_Name = AH.Site_Name,
                             Site_Code = AH.Site_Code,
                             Site_Address_2 = AH.Site_Address_2,
                             Site_Address_3 = AH.Site_Address_3,
                             Sub_Company_ID = AH.Sub_Company_ID,
                             Sub_Company_Name = AH.Sub_Company_Name,
                             Company_ID = AH.Company_ID,
                             Company_Name = AH.Company_Name
                         }).Distinct().ToList();


                return result;
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
                return null;
            }
        }
        #endregion

        #region ProcedureCalls
        /// <summary>
        /// This function is used to get all the customer access 
        /// </summary>
        /// <returns>all Operators</returns>
        /// 

        public IEnumerable<SiteCustomerAccessResult> GetAllSiteCustomerAccess(int CustomerID)
        {
            try
            {
                IEnumerable<SiteCustomerAccessResult> result =
                        (from AH in _context.GetSiteCustomerAccess(CustomerID)
                         select new SiteCustomerAccessResult
                         {
                             Customer_Access_ID = AH.Customer_Access_ID,
                             Customer_Access_Name = AH.Customer_Access_Name,
                             Customer_Access_View_All_Companies = AH.Customer_Access_View_All_Companies,
                             Customer_Access_View_All_Depots = AH.Customer_Access_View_All_Depots,
                             Customer_Access_View_All_Sites = AH.Customer_Access_View_All_Sites
                         }).Distinct();


                return result;
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
                return null;
            }
        }
        #endregion

        #region CreateNewCustomerAccess
        /// <summary>
        /// This function is used to update all the customer access 
        /// </summary>
        /// <returns>all Operators</returns>
        /// 

        public int UpdateCustomerAccess(string CustomerAccess)
        {
            try
            {
                int ReturnValue = _context.esp_InsertCustomerAccessForSite(CustomerAccess);
                return ReturnValue;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -99;
            }
        }
        #endregion



        /// <summary>
        /// Function to update the status for customer access for companies and subcompanies
        /// </summary>
        /// <remarks></remarks>
        /// <param name="CustomerID"></param>
        /// <param name="SubCompanyID"></param>
        /// <param name="Status"></param>

        public void UpdateCustomerAccessForCompanies(int CustomerID, string SubCompanyID, bool Status, bool isChecked)
        {
            _context.UpdateCompaniesCustomerAccess(CustomerID, Convert.ToInt32(SubCompanyID), Status, isChecked);
        }

        /// <summary>
        /// Function to update the status for customer access for Depots
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="DepotID"></param>
        /// <param name="Status"></param>
        public void UpdateCustomerAccessForDepots(int CustomerID, string DepotID, bool Status, bool isChecked)
        {
            _context.UpdateDepotsCustomerAccess(CustomerID, Convert.ToInt32(DepotID), Status, isChecked);
        }


        /// <summary>
        /// Function to update the status for customer access for sites
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="DepotID"></param>
        /// <param name="Status"></param>
        public void UpdateCustomerAccessForSites(int CustomerID, int SecurityProfileTypeID, string SecurityProfileTypeValue, bool? AllowUser, string Description)
        {
            _context.EditSecurityProfile(CustomerID, SecurityProfileTypeID, SecurityProfileTypeValue, AllowUser, Description);
        }

        /// <summary>
        /// Function to get the company access
        /// </summary>
        /// <param name="customeraccessID"></param>
        public IEnumerable<CustomerAccessCompanyResult> GetCompanyAccess(int CustomerAccessID)
        {
            try
            {
                IEnumerable<CustomerAccessCompanyResult> result =
                        (from AH in _context.GetCustomerAccessCompany(CustomerAccessID)
                         orderby AH.Sub_Company_ID descending
                         select new CustomerAccessCompanyResult
                         {
                             Sub_Company_ID = AH.Sub_Company_ID,
                             Sub_Company_Name=AH.Sub_Company_Name

                         }).Distinct();


                return result;
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
                return null;
            }
        }

        /// <summary>
        /// Function to get the depot access
        /// </summary>
        /// <param name="customeraccessID"></param>
        public IEnumerable<CustomerAccessDepotResult> GetDepotAccess(int CustomerAccessID)
        {
            try
            {
                IEnumerable<CustomerAccessDepotResult> result =
                        (from AH in _context.GetCustomerAccessDepot(CustomerAccessID)
                         orderby AH.Depot_ID descending
                         select new CustomerAccessDepotResult
                         {
                             Depot_ID = AH.Depot_ID,
                             Depot_Name=AH.Depot_Name

                         }).Distinct();


                return result;
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
                return null;
            }
        }
        /// <summary>
        /// Function to get the site access
        /// </summary>
        /// <param name="customeraccessID"></param>
        public IEnumerable<CustomerAccessSiteResult> GetSiteAccess(int CustomerAccessID)
        {
            try
            {
                IEnumerable<CustomerAccessSiteResult> result =
                        (from AH in _context.GetCustomerAccessSite(CustomerAccessID)
                         orderby AH.SecurityProfileType_Value descending
                         select new CustomerAccessSiteResult
                         {
                             AllowUser = AH.AllowUser,
                             SecurityProfileType_Value = AH.SecurityProfileType_Value
                             

                         }).Distinct();


                return result;
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
                return null;
            }
        }
    }
}