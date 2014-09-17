using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.LogManagement;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.Utilities;
using BMC.EnterpriseClient;
using BMC.CoreLib.Win32;
using BMC.Common;

namespace BMC
{
    public partial class frmNewGroup : Form
    {
        #region Local Declaration
        
        private UserGroupBiz objUserGroupBiz = null;
         BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;
         private string MessageBoxCaption;

        #endregion Local Declaration

        #region Events
        /// <summary>
        /// Initializes a new instance of the <see cref="frmNewGroup"/> class.
        /// </summary>
         public frmNewGroup(string messageBoxCaption = "")
         {
             this.MessageBoxCaption = string.IsNullOrEmpty(messageBoxCaption) ? this.GetResourceTextByKey(1, "MSG_VALID_GRP_NAME") : messageBoxCaption;
             this.MessageBoxCaption = messageBoxCaption;
             InitializeComponent();
             SetTagProperty();
             this.ResolveResources();
             objUserGroupBiz = UserGroupBiz.CreateInstance();
             objDatawatcher = new BMC.EnterpriseClient.Helpers.Datawatcher(this);
         }

         /// <summary>
         /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
         /// </summary>
         public void SetTagProperty()
         {
             this.lblGrpName.Tag = "Key_NewGroupNameColon";
             this.btnAddGroup.Tag = "Key_AddCaption";
             this.btnClose.Tag = "Key_CloseCaption";
         }

        /// <summary>
        /// Handles the Click event of the btnClose control.
        /// Close the form with confirmation from user
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            
                this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnAddGroup control.
        /// Creates a new user group and returns to User Group screen
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            string strGroupName = txtGrpName.Text.Trim();            
            try
            {
                if (Regex.IsMatch(strGroupName, @"^$|[^a-zA-Z0-9\s]", RegexOptions.CultureInvariant))
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_VALID_GRP_NAME"), this.MessageBoxCaption);
                    return;
                }

                int iResult = objUserGroupBiz.NewUserGroup(strGroupName);
                if (iResult == 0)
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_USER_GRP_NAME_EXISTS"), this.MessageBoxCaption);
                }
                else
                {
                    AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());

                    business.InsertAuditData(new Audit.Transport.Audit_History
                    {
                        EnterpriseModuleName = ModuleNameEnterprise.AUDIT_USERGROUP,
                        Audit_Screen_Name = "Admin Group",
                        Audit_Field = "User_Group_Name",
                        Audit_New_Vl = strGroupName,
                        Audit_Desc = "Record [" + strGroupName + "] added to Admin Group",
                        AuditOperationType = OperationType.ADD,
                        Audit_User_ID = AppEntryPoint.Current.UserId,
                        Audit_User_Name = AppEntryPoint.Current.UserName
                    }, false);

                    this.DialogResult = DialogResult.OK;
                    objDatawatcher.DataModify = false;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in creating new User Group -Error Message- " + ex.Message, LogManager.enumLogLevel.Error);
            }
        }
        #endregion Events
    }
}
