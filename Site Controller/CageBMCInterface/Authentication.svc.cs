using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BMC.Transport;
using BMC.Security;
using System.Security.Cryptography;
using BMC.Security.DataContext;
using BMC.Security.Manager;
using BMC.Security.Interfaces;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common;
using BMC.Common.ExceptionManagement;

namespace CageBMCInterface
{
    // NOTE: If you change the class name "Authendication" here, you must also update the reference to "Authendication" in Web.config.
    public class Authentication : FrameworkEndPoint
    {
        public authenticateUserResponse1 authenticateUser(authenticateUserRequest request)
        {
            SettingInitializer.Initialize();  
            authenticateUserResponse1 oResponse =null;
            //oResponse.authenticateUserResponse.@return.errorPresent = true;  
            try
            {
                var loginResult = Checkuser(request.authenticateUser.arg0, request.authenticateUser.arg1);
                if (loginResult == SecurityHelper.LoginResults.LoginSuccesful)
                {
                    SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.Tickets");
                    oResponse = new authenticateUserResponse1() { authenticateUserResponse = new authenticateUserResponse() { @return = new userDTO() { actorLoginFails = 0, confirmPassword = request.authenticateUser.arg1, daysSincePwdChange = 10, employeeCard = "0", employeeLockStatus = true, errorPresent = false, firstName = request.authenticateUser.arg0, lastName = request.authenticateUser.arg0, signOnFailureReason = loginResult.ToString() , userId = 1, userName = request.authenticateUser.arg0 } } };
                }
                else
                {
                    oResponse = new authenticateUserResponse1() { authenticateUserResponse = new authenticateUserResponse() { @return = new userDTO() { actorLoginFails = 0, confirmPassword = request.authenticateUser.arg1, daysSincePwdChange = 10, employeeCard = "0", employeeLockStatus = true, errorPresent = true, firstName = request.authenticateUser.arg0, lastName = request.authenticateUser.arg0, userId = 1, signOnFailureReason = loginResult.ToString(), userName = request.authenticateUser.arg0 } } };
                }

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex); 
            }
            return oResponse;

        }
        public static SecurityHelper.LoginResults Checkuser(string strUser, string strPass)
        {
            try
            {
                IUser user;
                SecurityHelper.CreateInstance(oCommonUtilities.CreateInstance().GetConnectionString(), false);
                var result = SecurityHelper.Login(strUser, strPass, out user);
                if (result == SecurityHelper.LoginResults.LoginSuccesful || result == SecurityHelper.LoginResults.PasswordExpired)
                {
                    clsSecurity.UserID = user.SecurityUserID;
                    clsSecurity.UserName = user.UserName;
                }
                return result;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return SecurityHelper.LoginResults.LoginFailed;
        }

    }
}
