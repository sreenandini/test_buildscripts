using System;
using System.Web.UI;
using BMC.Security.Interfaces;
using BMC.Guardian.Business;
using BMC.Common.ExceptionManagement;
using BMC.Security;
using BMC.Guardian.DBHelper;

using Audit.BusinessClasses;
using Audit.Transport;


public partial class Login : System.Web.UI.Page
{
    public string Message = string.Empty;
    IUser user;
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
         objAudit=new AuditViewerBusiness(GuardianDBHelper.EnterpriseConnectionString);
       
        try
        {
            if (string.IsNullOrEmpty(UserNameTextBox.Text) || string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                Message = "Enter User Name, Password and click Login.";
                return;
            }

            var lResult = GuardianBL.DoLogin(UserNameTextBox.Text, PasswordTextBox.Text, out user);
    
            switch(lResult)
            {
                case SecurityHelper.LoginResults.LoginSuccesful:
                    {

                        Guardian_LINQBL objGuardian_LINQBL = new Guardian_LINQBL();
                        if (objGuardian_LINQBL.GetGuardianRights(user.SecurityUserID))
                        {
                            Audit("Login Successful for User-" + user.DisplayName);
                            Message = "";
                            Session["UserName"] = user.UserName;
                            Response.Redirect("MainPage.aspx");
                        }
                        else
                        {
                            Audit("User has no rights to access Guardian Website." + user.DisplayName);
                            Message = "User has no rights to access Guardian Website.";
                        }
                        break;
                    }
                case SecurityHelper.LoginResults.PasswordExpired:
                    {
                        Message = "Password expired. Please change password in enterprise.";
                        Audit("Password expired for User-" + user.DisplayName);
                        break;
                    }
                case SecurityHelper.LoginResults.LoginReset:
                    {
                        Message = "First login since reset. Please change password in enterprise.";
                        Audit("Password Reset for User-" + user.DisplayName);
                        break;
                    }
                case SecurityHelper.LoginResults.MaxAttemptsExceeded:
                    {
                        Message = "Account is locked. Please contact administrator.";
                        Audit("Account Locked for User-" + user.DisplayName);
                        break;
                    }
                case SecurityHelper.LoginResults.LoginFailed:
                    {
                        Message = "Invalid Login.";
                        Audit("Invalid Login attempt for User-" + user.DisplayName);
                        break;
                    }
            }
            
            
           
        }
        catch 
        {
            Message = "Unable to Login";
        }
        
        

    }
        

    private void Audit(string sDesc)
    {
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
