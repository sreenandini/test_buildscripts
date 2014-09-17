using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Guardian.DBHelper;
using BMC.Common.LogManagement;
using BMC.Guardian.Transport;
using BMC.CoreLib;
using System.IO;
using System.Security.Cryptography;
using BMC.Common.ExceptionManagement;

namespace BMC.Guardian.Business
{
    public class Guardian_LINQBL
    {

        public Guardian_LINQBL()
        {
        }

        public Boolean GetGuardianRights(int nUserID)
        {
            GuardianLINQDataContext objGuardianLINQDataContext = new GuardianLINQDataContext(BMC.Common.Utilities.DatabaseHelper.GetConnectionString());

            foreach (GuardianRightsResult objGuardianRightsResult in objGuardianLINQDataContext.GetGuardianRights(nUserID))
                return objGuardianRightsResult.HQ_GUARDIAN;

            return false;
        }

        public void GetViewSiteStatusInfo(string site_Code,string user_Name, ref string region, ref string notRunhours)
        {
            try
            {
                GuardianLINQDataContext objGuardianLINQDataContext = new GuardianLINQDataContext(BMC.Common.Utilities.DatabaseHelper.GetConnectionString());
                foreach (var SiteStatusInfo in objGuardianLINQDataContext.GetViewSiteStatusInfo(site_Code, user_Name))
                {
                    region = SiteStatusInfo.Region;
                    notRunhours = SiteStatusInfo.HourlyNotRun;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Guardian_LINQBL.GetViewSiteStatusInfo" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

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

            using (GuardianLINQDataContext context = new GuardianLINQDataContext(BMC.Common.Utilities.DatabaseHelper.GetConnectionString()))
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
                                    RoleName = dbReturnValue.RoleName,
                                    SecurityRoleID = (dbReturnValue.SecurityRoleID ?? 0)
                                    //FirstName = 

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

        public int usp_LockByUserName(string userName)
        {
            GuardianLINQDataContext objGuardianLINQDataContext = new GuardianLINQDataContext(BMC.Common.Utilities.DatabaseHelper.GetConnectionString());
            return objGuardianLINQDataContext.usp_LockByUserName(userName);
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
