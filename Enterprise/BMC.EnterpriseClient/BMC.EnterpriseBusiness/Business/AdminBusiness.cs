using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.EnterpriseDataAccess;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.ExceptionManagement;
using System.IO;
using System.Security.Cryptography;
using System.Data.Linq;
using Audit.Transport;
using Audit.BusinessClasses;

namespace BMC.EnterpriseBusiness.Business
{
    public class AdminBusiness : DisposableObject
    {
        public AdminBusiness() { }

        /// <summary>
        /// Creates the MD5 hash.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns>Computed Hash</returns>
        public static string CreateHash(string original)
        {
            if (original.IsEmpty()) return string.Empty;

            using (MemoryStream ms = new MemoryStream(Encoding.Default.GetBytes(original)))
            {
                MD5 md5Hash = HashAlgorithm.Create("MD5") as MD5;
                return Convert.ToBase64String(md5Hash.ComputeHash(ms));
            }
        }

        public class AuthenticateAndGetUserResult
        {
            public AuthenticationResult Result { get; set; }
            public Exception Exception { get; set; }
        }

        public AuthenticateAndGetUserResult AuthenticateAndGetUser(string userName, string password, ref UserEntity entity)
        {
            AuthenticateAndGetUserResult result = new AuthenticateAndGetUserResult()
            {
                Result = AuthenticationResult.Unauthenticated
            };

            using (EnterpriseDataContext context = EnterpriseDataContextHelper.GetDataContext())
            {
                try
                {
                    int? isAuthenticated = 0;
                    var dbResult = context.rsp_AuthenticateAndGetUser(userName, CreateHash(password), ref isAuthenticated);
                    if (isAuthenticated != null &&
                        isAuthenticated.HasValue &&
                        dbResult != null)
                    {
                        result.Result = (AuthenticationResult)isAuthenticated.Value;
                        //if (result.Result == AuthenticationResult.Authenticated)
                        {
                            foreach (rsp_AuthenticateAndGetUserResult dbReturnValue in dbResult)
                            {
                                entity = new UserEntity()
                                {
                                    ChangePassword = dbReturnValue.ChangePassword.SafeValue(),
                                    CreatedDate = dbReturnValue.CreatedDate.SafeValue(),
                                    CurrencyCulture = dbReturnValue.CurrencyCulture.SafeValue(),
                                    DateCulture = dbReturnValue.DateCulture.SafeValue(),
                                    IsLocked = dbReturnValue.isLocked,
                                    IsReset = dbReturnValue.isReset.SafeValue(),
                                    LanguageID = dbReturnValue.LanguageID.SafeValue(),
                                    Password = string.Empty,
                                    PasswordChangeDate = dbReturnValue.PasswordChangeDate.SafeValue(),
                                    SecurityUserID = dbReturnValue.SecurityUserID,
                                    UserName = dbReturnValue.UserName,
                                    WindowsUserName = dbReturnValue.WindowsUserName, //,
                                    StaffID = (dbReturnValue.Staff_ID ?? 0),
                                    RoleName=dbReturnValue.RoleName,
                                    SecurityRoleID=(dbReturnValue.SecurityRoleID??0)
                                    
                                };
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    result.Exception = ex;
                    result.Result = AuthenticationResult.Unauthenticated;
                }
            }

            return result;
        }

        public void GetUserDetailsByUserID(Int32 iUserId, ref UserEntity entity)
        {
            using (EnterpriseDataContext context = EnterpriseDataContextHelper.GetDataContext())
            {
                try
                {
                    var dbResult = context.GetUserDetailsByUserID(iUserId);
                    foreach (rsp_GetUserDetailsByUserIDResult dbReturnValue in dbResult)
                    {
                        entity = new UserEntity()
                        {
                            ChangePassword = dbReturnValue.ChangePassword.SafeValue(),
                            CreatedDate = dbReturnValue.CreatedDate.SafeValue(),
                            CurrencyCulture = dbReturnValue.CurrencyCulture.SafeValue(),
                            DateCulture = dbReturnValue.DateCulture.SafeValue(),
                            IsLocked = dbReturnValue.isLocked,
                            IsReset = dbReturnValue.isReset.SafeValue(),
                            LanguageID = dbReturnValue.LanguageID.SafeValue(),
                            Password = string.Empty,
                            PasswordChangeDate = dbReturnValue.PasswordChangeDate.SafeValue(),
                            SecurityUserID = dbReturnValue.SecurityUserID,
                            UserName = dbReturnValue.UserName,
                            WindowsUserName = dbReturnValue.WindowsUserName,
                            RoleName = dbReturnValue.RoleName,
                            SecurityRoleID=(dbReturnValue.SecurityRoleID??0)
                        };
                        break;
               	    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
            }
        }

        public bool Signout(UserEntity entity)
        {
            bool result = default(bool);

            try
            {
                InsertNewAuditEntry(ModuleNameEnterprise.Logout, "Enterprise Logout Screen", "", "", entity.SecurityUserID, entity.UserName, " Successful.");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return result;
        }

        public List<string> GetWindowsServiceNames()
        {
            List<string> result = null;

            try
            {
                using (EnterpriseDataContext dbContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    string serviceNamesJoined = string.Empty;
                    dbContext.GetSetting(null, "WindowsServices", string.Empty, ref serviceNamesJoined);

                    if (!serviceNamesJoined.IsEmpty())
                    {
                        string[] serviceNames = serviceNamesJoined.Split(new char[] { ',' });
                        if (serviceNames != null)
                        {
                            result = new List<string>(serviceNames);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (result == null) result = new List<string>();
            }

            return result;
        }

        public static string GetSetting(string settingName, string defaultValue)
        {
            string refString = string.Empty;
            try
            {

                using (EnterpriseDataContext dbContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    dbContext.GetSetting(null, settingName, defaultValue, ref refString);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return refString;
        }

        public IDictionary<string, UserAccessEntity> GetUserAccesses(int securityUserId)
        {
            IDictionary<string, UserAccessEntity> result = new SortedDictionary<string, UserAccessEntity>(StringComparer.InvariantCultureIgnoreCase);
            return GetUserAccesses(securityUserId, ref result);
        }

        public IDictionary<string, UserAccessEntity> GetUserAccesses(int securityUserId, ref IDictionary<string, UserAccessEntity> result)
        {
            result.Clear();

            try
            {
                using (EnterpriseDataContext dbContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    ISingleResult<rsp_GetHQ_User_AccessInfoResult> userAccesses = dbContext.GetHQ_User_AccessInfo(securityUserId);

                    if (userAccesses != null)
                    {
                        foreach (rsp_GetHQ_User_AccessInfoResult userAccess in userAccesses)
                        {
                            if (!result.ContainsKey(userAccess.Col))
                            {
                                result.Add(userAccess.Col, new UserAccessEntity(userAccess.Col, userAccess.VALUE.SafeValue()));
                            }
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

        public ProductVersionEntity GetProductVersion()
        {
            ProductVersionEntity result = new ProductVersionEntity();

            try
            {
                using (EnterpriseDataContext dbContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    ISingleResult<rsp_GetSplashDetailsResult> splashDetails = dbContext.GetSplashDetails();

                    if (splashDetails != null)
                    {
                        foreach (rsp_GetSplashDetailsResult splashDetail in splashDetails)
                        {
                            result.Company = splashDetail.COMPANYNAME;
                            result.Copyright = splashDetail.COPYRIGTINFO;
                            result.Description = splashDetail.PRODUCTDESC;
                            result.Name = splashDetail.PRODUCTNAME;
                            result.Version = splashDetail.PRODUCTVERSION;
                            break;
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

        public GetCustomerAccessViewAllDepotsResult CheckCustomerAccessDepotAll(int StaffID)
        {
            GetCustomerAccessViewAllDepotsResult objcoll = null;
            try
            {
                List<rsp_GetCustomerAccessViewAllDepotsResult> CustomerAccess;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    CustomerAccess = DataContext.GetCustomerAccessViewAllDepots(StaffID).ToList();
    			}
                objcoll = (from obj in CustomerAccess
                           select new GetCustomerAccessViewAllDepotsResult
                           {
                               Customer_Access_View_All_Depots = obj.Customer_Access_View_All_Depots
                           }).Single<GetCustomerAccessViewAllDepotsResult>();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);               
            }
            return objcoll;
        }

        public GetCustomerAccessViewAllCompaniesResult CheckCustomerAccessViewAllCompanies(int StaffID)
        {
            GetCustomerAccessViewAllCompaniesResult objcoll = null;
            try
            {
                List<rsp_GetCustomerAccessViewAllCompaniesResult> CustomerAccess;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    CustomerAccess = DataContext.GetCustomerAccessViewAllCompanies(StaffID).ToList();
                }
                objcoll = (from obj in CustomerAccess
                           select new GetCustomerAccessViewAllCompaniesResult
                           {
                               Customer_Access_View_All_Companies = obj.Customer_Access_View_All_Companies
                           }).Single<GetCustomerAccessViewAllCompaniesResult>();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            return objcoll;
        }
        //
        ///Method to check whether the Logged in user (Role) has rights to view all reports or not       
        //

        public bool RoleHasRightsAllReports(int securityRoleID)
        {
            try
            {
                List<rsp_GetAllReportsToRolesAccessResult> ReportsToRolesAccess;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    ReportsToRolesAccess = DataContext.rsp_GetAllReportsToRolesAccess(securityRoleID).ToList();
                }
                if (ReportsToRolesAccess.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        public void InsertNewAuditEntry(ModuleNameEnterprise moduleName, string strScreenName, string strField, string strNewItem, int iUserId, string strUsername,string status)
        {
            try
            {
                //Calling Audit Method
                Audit_History AH = new Audit_History();
                //Populate required Values            
                AH.EnterpriseModuleName = moduleName;
                AH.Audit_Screen_Name = strScreenName;
                AH.Audit_Desc = moduleName + " User: " + strUsername + ", Status: " + status;
                AH.AuditOperationType = OperationType.ADD;
                AH.Audit_Field = strField;
                AH.Audit_New_Vl = strNewItem;
                AH.Audit_Slot = string.Empty;
                AH.Audit_Old_Vl = string.Empty;

                AH.Audit_User_ID = iUserId;
                AH.Audit_User_Name = strUsername;

                AuditViewerBusiness AVB = new AuditViewerBusiness(Common.Utilities.DatabaseHelper.GetConnectionString());
                AVB.InsertAuditData(AH, true);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void AuditModifiedData( ModuleNameEnterprise moduleName, string screen_name, string sField, string sPrevValue, string sNewValue, int iUserId, string strUsername,string status)
        {
            try
            {
                Audit_History AH = new Audit_History();
                AH.EnterpriseModuleName = moduleName;
                AH.Audit_Screen_Name = screen_name;
                AH.Audit_Desc = "";
                AH.AuditOperationType = OperationType.MODIFY;
                AH.Audit_Field = sField;
                AH.Audit_User_ID = iUserId;
                AH.Audit_User_Name = strUsername;
                AH.Audit_New_Vl = sNewValue; //current value
                AH.Audit_Old_Vl = sPrevValue;  // previous value
                AH.Audit_Slot = string.Empty;

                AuditViewerBusiness AVB = new AuditViewerBusiness(Common.Utilities.DatabaseHelper.GetConnectionString());
                AVB.InsertAuditData(AH, true);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public int ExportLiquidationDetailsToSite(string sSiteCode)
        {
            int iResult = 0;
            using (EnterpriseDataContext context = EnterpriseDataContextHelper.GetDataContext())
            {
                try
                {
                    iResult = context.rsp_ExportAllLiquidationDataToSite(sSiteCode);
                }

                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
            }
            return iResult;
        }
    }

    public enum AuthenticationResult
    {
        Unauthenticated = 0,
        Authenticated = 1,
        UserNameEmpty = -1,
        PasswordEmpty = -2,
        UserNameNotExists = -3,
        PasswordNotMatched = -4,
        UserTerminated = -5,
        UserLocked = -6,
        FirstLoginSinceReset = -7,
        PasswordExpired = -8,
    }
}
