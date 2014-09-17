using System;
using System.Web.UI;
using BMC.Security.Interfaces;
using BMC.Guardian.Business;
using BMC.Common.ExceptionManagement;
using BMC.Security;
using BMC.Guardian.DBHelper;

using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Guardian.Transport;


public partial class Login : System.Web.UI.Page
{
    public string Message = string.Empty;
    UserEntity user;
    AuditViewerBusiness objAudit = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Clear();
            Session["UserName"] = string.Empty;
            Session["SiteName"] = string.Empty;
            Session["HistoryTable"] = null;
            user = null;
            UserNameTextBox.Focus();
        }
    }

    protected void LoginButton_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            objAudit = new AuditViewerBusiness(GuardianDBHelper.EnterpriseConnectionString);
            if (string.IsNullOrEmpty(UserNameTextBox.Text) || string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                Message = "Enter User Name, Password and click Login.";
                return;
            }
            
            BMC.Guardian.Business.Guardian_LINQBL.AuthenticateAndGetUserResult result = GuardianBL.AuthenticateAndGetUser(UserNameTextBox.Text, PasswordTextBox.Text, ref user);
            bool invalidLogin = true;
            switch (result.Result)
            {
                case AuthenticationResult.Authenticated:
                    Guardian_LINQBL objGuardian_LINQBL = new Guardian_LINQBL();
                    if (user != null && objGuardian_LINQBL.GetGuardianRights(user.SecurityUserID))
                    {
                        Audit("Login Successful for User-" + user.DisplayName);
                        invalidLogin = false;
                        Message = "";
                        Session["UserName"] = user.UserName;
                        Session["UserID"] = user.SecurityUserID;
                        Session["DisplayName"] = user.DisplayName;
                        Session["SecurityUserID"] = user.SecurityUserID;
                        Response.Redirect("MainPage.aspx");

                    }
                    else
                    {
                        invalidLogin = false;
                        Audit("User has no rights to access Guardian Website." + ((user == null) ? UserNameTextBox.Text : user.DisplayName));
                        Message = "User has no rights to access Guardian Website.";
                    }
                    break;

                case AuthenticationResult.PasswordExpired:
                    invalidLogin = false;
                    Message = "Password expired. Please change password in enterprise.";
                    Audit("Password expired for User-" + ((user == null) ? UserNameTextBox.Text : user.DisplayName));
                    break;

                case AuthenticationResult.FirstLoginSinceReset:
                    invalidLogin = false;
                    Message = "First login since reset. Please change password in enterprise.";
                    Audit("Password Reset for User-" + ((user == null) ? UserNameTextBox.Text : user.DisplayName));
                    break;

                case AuthenticationResult.UserLocked:
                    invalidLogin = false;
                    Message = "Account is locked. Please contact administrator.";
                    Audit("Account Locked for User-" + ((user == null) ? UserNameTextBox.Text : user.DisplayName));
                    break;

                case AuthenticationResult.Unauthenticated:
                    invalidLogin = true;
                    Message = "Invalid Login.";
                    Audit("Invalid Login attempt for User-" + ((user == null) ? UserNameTextBox.Text : user.DisplayName));
                    break;

                case AuthenticationResult.UserNameNotExists:
                    invalidLogin = false;
                    Message = "Specified user does not exist Invalid Login.";
                    AuditForNonUserLogin("Specified User " + ((user == null) ? UserNameTextBox.Text : user.DisplayName) + "Does Not Exits.");
                    break;

                case AuthenticationResult.UserTerminated:
                    invalidLogin = false;
                    Message = "User account is Terminated. Please contact administrator.";
                    AuditForNonUserLogin("Specified User " + ((user == null) ? UserNameTextBox.Text : user.DisplayName) + "Does Not Exits.");
                    break;
                case AuthenticationResult.PasswordNotMatched:
                    invalidLogin = true;
                    Message = "Username/Password not matched";
                    AuditForNonUserLogin("Specified User " + ((user == null) ? UserNameTextBox.Text : user.DisplayName) + "password not matched.");
                    break;
            }
            if (invalidLogin)
            {
                if ((Convert.ToString(Session["LoginUser"]) + "").Equals(UserNameTextBox.Text.Trim()))
                {
                    Session["RetryCount"] = Convert.ToInt32(Session["RetryCount"]) + 1;
                    if (Convert.ToInt32(Session["RetryCount"]) >= 3)
                    {
                        Session["LoginUser"] = "";
                        Session["RetryCount"] = 0;
                        GuardianBL.usp_LockByUserName(UserNameTextBox.Text.Trim());
                        Message = "User account is locked. Please contact administrator.";
                        Audit("Account Locked for User - " + ((user == null) ? UserNameTextBox.Text : user.DisplayName));

                    }
                }
                else
                {
                    Session["LoginUser"] = UserNameTextBox.Text.Trim();
                    Session["RetryCount"] = 1;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionManager.Publish(ex);
            Message = "Unable to Login";
        }
    }
        
    private void AuditForNonUserLogin(string sDesc)
    {
        objAudit.InsertAuditData(new Audit.Transport.Audit_History
        {
            Audit_User_ID=null,
            Audit_User_Name = UserNameTextBox.Text,
            AuditModuleName = ModuleName.Login,
            Audit_Screen_Name = "Guardian Login Screen",
            Audit_Desc = sDesc,
            AuditOperationType = OperationType.ADD
        },true);
    }
    private void Audit(string sDesc)
    {
        if (user == null)
        {
            AuditForNonUserLogin(sDesc);
            return;
        }
        objAudit.InsertAuditData(new Audit.Transport.Audit_History
        {
            Audit_User_ID=user.SecurityUserID,
            Audit_User_Name=user.DisplayName,
            AuditModuleName = ModuleName.Login,
            Audit_Screen_Name = "Guardian Login Screen",
            Audit_Desc = sDesc,
            AuditOperationType = OperationType.ADD
        },true);
    }



}
