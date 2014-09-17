using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.Common.LogManagement;
using BMC.EnterpriseClient.Views;
using BMC.EnterpriseBusiness.Entities;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;
using BMC.SecurityVB;
using System.IO;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseClient.Helpers;
using BMC.Common;
using System.Text.RegularExpressions;

namespace BMC.EnterpriseClient
{
    public partial class ucUserGroup : UserControl
    {

        bool _IsSuperUser = false;
        bool isServiceCallEnabled = false;
        bool isFinancialEnabled = false;
        BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;

        public ucUserGroup()
        {
            InitializeComponent();
            bRetValue = true;
            bChildNodeTrigger = true;
            bParentNodeTrigger = true;
            bEditMode = false;

            objUserGroupBiz = UserGroupBiz.CreateInstance();
            dicUsrGrpRoles = new SortedDictionary<int, TreeNodeCollection>();
            isServiceCallEnabled = BMC.CoreLib.Extensions.GetAppSettingValueBool("isServiceCallEnabled", false);
            isFinancialEnabled = BMC.CoreLib.Extensions.GetAppSettingValueBool("isFinancialEnabled", false);
            SetTagProperty();
           // objDatawatcher = new BMC.EnterpriseClient.Helpers.Datawatcher(this);
        }

        public ucUserGroup(int UserID, string UserName)
        {
            try
            {
                InitializeComponent();
                this._UserID = UserID;
                this._UserName = UserName;
                bRetValue = true;
                bChildNodeTrigger = true;
                bParentNodeTrigger = true;
                bEditMode = false;

                objUserGroupBiz = UserGroupBiz.CreateInstance();
                dicUsrGrpRoles = new SortedDictionary<int, TreeNodeCollection>();
                isServiceCallEnabled = BMC.CoreLib.Extensions.GetAppSettingValueBool("isServiceCallEnabled", false);
                isFinancialEnabled = BMC.CoreLib.Extensions.GetAppSettingValueBool("isFinancialEnabled", false);
                SetTagProperty();
                //objDatawatcher = new BMC.EnterpriseClient.Helpers.Datawatcher(this);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #region Local Declartion

        #region Objects
        private UserGroupBiz objUserGroupBiz = null;
        List<UserGroup> lstUserGroup = null;
        List<User_Access> lstUser_Access = null;
        IDictionary<int, TreeNodeCollection> dicUsrGrpRoles = null;
        IList<User_Role_Item<TreeNode>> lstTreeNodes = new List<User_Role_Item<TreeNode>>();
        string _UserName;
        int _UserID;
        #endregion Objects

        #region Variables
        bool bRetValue;
        bool bChildNodeTrigger;
        bool bParentNodeTrigger;
        bool bEditMode;
        #endregion Variables

        #endregion Local Declartion

        #region User Function Start

        /// <summary>
        /// PopulateUserGroup
        /// Retrives all user groups from the DB
        /// </summary>
        /// <returns>True if no error while fetching User group</returns>
        private bool PopulateUserGroup()
        {
            try
            {
                lstUserGroup = objUserGroupBiz.GetUserGroup();
                cboUserGroupList.DisplayMember = "User_Group_Name";
                cboUserGroupList.ValueMember = "User_Group_ID";
                cboUserGroupList.DataSource = lstUserGroup;
                bRetValue = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bRetValue = false;
            }

            return bRetValue;
        }

        /// <summary>
        /// PopulateUser_Access
        /// Based on the iUserGroupId retrives the User_Access from the DB
        /// </summary>
        /// <param name="iUserGroupId">Int</param>
        /// <returns>True if no errors while populating treeview else false</returns>
        private bool PopulateUser_Access(int iUserGroupId, string sRoleName)
        {
            try
            {
                if (sRoleName.ToUpper() == "SUPER USER")
                    _IsSuperUser = true;
                else
                    _IsSuperUser = false;
                lstUser_Access = objUserGroupBiz.GetUser_Access(iUserGroupId);
                FrameUserGroupRoles();
                bRetValue = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bRetValue = false;
            }

            return bRetValue;
        }

        /// <summary>
        /// isEnabled
        /// Determines whether Report setting button is enabled for user group
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </returns>
        private bool IsEnabled()
        {
            try
            {
                if (lstUser_Access != null)
                {
                    User_Access x = (from acess in lstUser_Access
                                     where acess.Access_Name == "Reports"
                                     select acess).Single<User_Access>();
                    return x.Access_Value;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }

        /// <summary>
        /// frameUserGroupRoles
        /// Frames the user group roles in treeview
        /// </summary>
        private void FrameUserGroupRoles()
        {
            try
            {
                tvwUserGroupRoles.Nodes.Clear(); // Clear any existing items
                tvwUserGroupRoles.BeginUpdate(); // prevent overhead and flicker
                LoadGroupRoles();               // load all the lowest tree nodes
                tvwUserGroupRoles.EndUpdate();   // re-enable the tree
                tvwUserGroupRoles.Refresh();     // refresh the treeview display
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// EditUserRole
        /// Disables the Controls based on bEditMode variable
        /// bEditMode
        /// - False, User role is not in edit mode
        /// - True, User role is in edit mode
        /// </summary>
        private void EditUserRole()
        {
            try
            {
                if (!bEditMode)
                {
                    cboUserGroupList.Enabled = false;
                    btnNewGroup.Enabled = false;
                    tvwUserGroupRoles.Enabled = true;
                    btnExchgAdmin.Enabled = true;
                    btnCancel.Enabled = true;
                    btnRptAdmin.Enabled = IsEnabled();
                    btnAssociateMode.Enabled = true;

                    if (_IsSuperUser)
                        btnEdit.Enabled = false;
                    else
                        //btnEdit.Text = "Update";
                        btnEdit.Text = this.GetResourceTextByKey("Key_UpdateCaption");

                    bEditMode = true;

                  
                }
                else
                {
                    cboUserGroupList.Enabled = true;
                    btnNewGroup.Enabled = true;
                    tvwUserGroupRoles.Enabled = false;
                    btnRptAdmin.Enabled = false;
                    btnExchgAdmin.Enabled = false;
                    btnAssociateMode.Enabled = false;
                    btnCancel.Enabled = false;
                    bEditMode = false;                  
                    btnEdit.Enabled = true;                    
                    btnEdit.Text = this.GetResourceTextByKey("Key_EditCaption");
                }

                PopulateUser_Access(int.Parse(cboUserGroupList.SelectedValue.ToString()), cboUserGroupList.Text);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// LoadGroupRoles
        /// Reads User_Access list and stores them in dictionary.
        /// adds the 
        /// </summary>
        private void LoadGroupRoles()
        {
            try
            {

                lstTreeNodes.Clear();
                dicUsrGrpRoles.Clear();
                bool isSiteLicensingEnabled = AdminBusiness.GetSetting("IsSiteLicensingEnabled", "False").ToUpper().Trim().Equals("TRUE");
                bool isCentralizedDeclaration = AdminBusiness.GetSetting("CentralizedDeclaration", "False").ToUpper().Trim().Equals("TRUE");
                bool isEmployeeCardTracking = AdminBusiness.GetSetting("IsEmployeeCardTrackingEnabled", "False").ToUpper().Trim().Equals("TRUE");

                foreach (User_Access objUser_Access in lstUser_Access)
                {
                    TreeNodeCollection nodeParent = null;

                    if ((objUser_Access.Access_Key == "HQ_Stock_Asset_Import" ||
                    objUser_Access.Access_Key == "HQ_Stock_Asset_Export") &&
                    AdminBusiness.GetSetting("ImportExport_AssetFile", "False").ToUpper().Trim().Equals("FALSE"))
                        continue;

                    if (!isSiteLicensingEnabled && (objUser_Access.Access_Key == "HQ_Admin_SiteLicensing" ||
                       objUser_Access.Access_Key == "HQ_Admin_SiteLicensing_LicenseGen" ||
                       objUser_Access.Access_Key == "HQ_Admin_SiteLicensing_LicenseGen_RuleInfo" ||
                       objUser_Access.Access_Key == "HQ_Admin_SiteLicensing_LicenseGen_RuleInfo_AddorEditRule" ||
                       objUser_Access.Access_Key == "HQ_Admin_SiteLicensing_LicenseGen_KeyGen" ||
                       objUser_Access.Access_Key == "HQ_Admin_SiteLicensing_ViewCancelLicense" ||
                       objUser_Access.Access_Key == "HQ_Admin_SiteLicensing_LicenseHistory"))
                        continue;

                    if (objUser_Access.Access_Key == "HQ_Admin_Declaration" && !isCentralizedDeclaration)
                        continue;

                    if (objUser_Access.Access_Key == "HQ_Admin_EmployeeCard" && !isEmployeeCardTracking)
                    {
                        continue;
                    }
                    if (!isServiceCallEnabled && (objUser_Access.Access_Key == "HQ_Engineers" ||
                        objUser_Access.Access_Key == "HQ_Engineers_Current" ||
                        objUser_Access.Access_Key == "HQ_Engineers_Current_Edit" ||
                        objUser_Access.Access_Key == "HQ_Engineers_Current_Close" ||
                        objUser_Access.Access_Key == "HQ_Engineers_Closed" ||
                        objUser_Access.Access_Key == "HQ_Engineers_Closed_Edit" ||
                        objUser_Access.Access_Key == "HQ_Engineers_Engineers" ||
                        objUser_Access.Access_Key == "HQ_Engineers_Engineers_Edit" ||
                        objUser_Access.Access_Key == "HQ_Engineers_Schedule" ||
                        objUser_Access.Access_Key == "HQ_Engineers_Schedule_Edit" ||
                        objUser_Access.Access_Key == "HQ_Engineers_Notes" ||
                        objUser_Access.Access_Key == "HQ_Engineers_Notes_History"))
                    {

                        continue;
                    }
                    if (!SettingsEntity.IsVaultEnabled && (
                        objUser_Access.Access_Key == "HQ_Admin_VaultInterface" ||
                        objUser_Access.Access_Key == "HQ_Admin_CreateVault" ||
                        objUser_Access.Access_Key == "HQ_Admin_EditCreateVault" ||
                        objUser_Access.Access_Key == "HQ_Admin_VaultDeclaration" ||
                        objUser_Access.Access_Key == "HQ_Admin_EditVaultDeclaration" ||
                        objUser_Access.Access_Key == "HQ_Admin_AuditVaultDeclaration" ||
                        objUser_Access.Access_Key == "HQ_Admin_EditAuditVaultDeclaration" ||
                        objUser_Access.Access_Key == "HQ_Admin_TerminateVault"
                        ))
                    {
                        continue;
                    }

                    if (!isFinancialEnabled && (objUser_Access.Access_Key == "HQ_Financial"))
                        continue;

                    if (((SettingsEntity.SGVI_Enabled) || !isFinancialEnabled) && (
                            objUser_Access.Access_Key == "HQ_Financial_ShareHolder" ||
                            objUser_Access.Access_Key == "HQ_Financial_ProfitShare" ||
                            objUser_Access.Access_Key == "HQ_Financial_ExpenseShare" ||
                            objUser_Access.Access_Key == "HQ_Financial_ReadLiquidation" ||
                            objUser_Access.Access_Key == "HQ_Financial_CollectionLiquidation" ||
                            objUser_Access.Access_Key == "HQ_Financial_ReadLiquidationReport" ||
                            objUser_Access.Access_Key == "HQ_Admin_AuthorizeProfitShare"
                            ))
                    {
                        continue;
                    }

                    //added for excluding New screens from the tree view  if customer is not SGVI
                    if (((!SettingsEntity.SGVI_Enabled) || !isFinancialEnabled) && (
                        objUser_Access.Access_Key == "HQ_Financial_TermsProfiles" ||
                        objUser_Access.Access_Key == "HQ_Financial_ShareSchedules" ||
                        objUser_Access.Access_Key == "HQ_Financial_TermsSummary" ||
                        objUser_Access.Access_Key == "HQ_Financial_PeriodEndTermsProcessor"
                        ))
                    {
                        continue;
                    }
                    //if(!isFinancialEnabled && (
                    //    objUser_Access.Access_Key == "HQ_Financial" ||
                    //    objUser_Access.Access_Key == "HQ_Financial_ShareHolder" ||
                    //    objUser_Access.Access_Key == "HQ_Financial_ProfitShare" ||
                    //    objUser_Access.Access_Key == "HQ_Financial_ExpenseShare" ||
                    //    objUser_Access.Access_Key == "HQ_Financial_ReadLiquidation" ||
                    //    objUser_Access.Access_Key == "HQ_Financial_CollectionLiquidation" ||
                    //    objUser_Access.Access_Key == "HQ_Financial_ReadLiquidationReport" ||
                    //    objUser_Access.Access_Key == "HQ_Admin_AuthorizeProfitShare"
                    //    ))
                    //{
                    //    continue;
                    //}


                    TreeNode nodeChild = new TreeNode(ResourceExtensions.GetResourceTextByKey(null, "Key_DBV" + objUser_Access.Access_Key));
                    nodeChild.Tag = objUser_Access;
                    nodeChild.Name = objUser_Access.Access_Key;
                    nodeChild.Checked = objUser_Access.Access_Value;

                    if (!dicUsrGrpRoles.ContainsKey(objUser_Access.Access_Parent_Id))
                    {
                        nodeParent = tvwUserGroupRoles.Nodes;
                    }
                    else
                    {
                        nodeParent = dicUsrGrpRoles[objUser_Access.Access_Parent_Id];
                    }

                    if (!dicUsrGrpRoles.ContainsKey(objUser_Access.Access_Id))
                    {
                        dicUsrGrpRoles.Add(objUser_Access.Access_Id, nodeChild.Nodes);
                    }

                    lstTreeNodes.Add(new User_Role_Item<TreeNode>(objUser_Access, nodeChild,
                        (t) =>
                        {

                            return (t.Checked ? "1" : "0");
                        }));

                    nodeParent.Add(nodeChild);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                dicUsrGrpRoles.Clear();
            }
        }

        /// <summary>
        /// Checkallchildren
        /// Verify the current node whether it has child node or not.
        /// If yes moves to parent node and check/uncheck the node based on bCheck param
        /// </summary>
        /// <param name="treeNode">TreeNode</param>
        /// <param name="bCheck">Bool</param>
        private void CheckAllChildren(TreeNode treeNode, Boolean bCheck)
        {
            try
            {
                bParentNodeTrigger = false;
                foreach (TreeNode ctn in treeNode.Nodes)
                {
                    bChildNodeTrigger = false;
                    ctn.Checked = bCheck;
                    bChildNodeTrigger = true;

                    CheckAllChildren(ctn, bCheck);
                }
                bParentNodeTrigger = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// CheckMyParent
        /// Verify the current node whether it has parent node or not.
        /// If yes moves to parent node and check/uncheck the node based on bCheck param
        /// </summary>
        /// <param name="treeNode">TreeNode</param>
        /// <param name="bCheck">Bool</param>
        private void CheckMyParent(TreeNode treeNode, Boolean bCheck)
        {
            try
            {
                if (treeNode == null) return;
                if (treeNode.Parent == null) return;

                bChildNodeTrigger = false;
                bParentNodeTrigger = false;
                treeNode.Parent.Checked = bCheck;
                CheckMyParent(treeNode.Parent, bCheck);
                bParentNodeTrigger = true;
                bChildNodeTrigger = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion User Function End

        #region Events



        /// <summary>
        /// Handles the Click event of the btnRptAdmin control.
        /// Invokes Report role admin app
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnRptAdmin_Click(object sender, EventArgs e)
        {
            //string strFileName = @"C:\starteam\projects\Bally MultiConnect\BMC 12.1.1SP8\Coding\Source\Enterprise\ReportRoleAdmin\bin\Release\BMCReportAdmin.exe";
            string strFileName = Application.StartupPath + @"\BMCReportAdmin.exe";
            try
            {
                if (File.Exists(strFileName))
                {
                    BMCSecurityCallMethod BMCSecurityMethod = new BMCSecurityCallMethod();
                    //string strArguments = "\"sB2L6LBHMOE=\"  \"xgOlNMwT+Fs=\"  \"gZyOOm+7j3M=\"";
                    string strArguments = BMCSecurityMethod.Encrypt(cboUserGroupList.Text) + " "
                                          + BMCSecurityMethod.Encrypt(this._UserName.Replace(" ", "")) + " "
                                          + BMCSecurityMethod.Encrypt(this._UserID.ToString());

                    AppEntryPoint.Current.StartProcess(sender, null, strFileName, strArguments, true);
                }
                else
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_UCUSERGROUP_APPNOTINSTALLED"), this.ParentForm.Text);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnExchgAdmin control
        /// Invokes Exchange role admin app
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">The <see cref="System.EventArgs"/>EventArgs</param>
        private void btnExchgAdmin_Click(object sender, EventArgs e)
        {
            //string strFileName = @"C:\starteam\projects\Bally MultiConnect\BMC 12.1.1SP8\Coding\Source\Enterprise\UserRoleAdmin\bin\Debug\BMCUserRoleAdmin.exe";
            string strFileName = Application.StartupPath + @"\BMCUserRoleAdmin.exe";
            try
            {
                if (File.Exists(strFileName))
                {
                    BMCSecurityCallMethod BMCSecurityMethod = new BMCSecurityCallMethod();
                    //string strArguments = "\"sB2L6LBHMOE=\"  \"xgOlNMwT+Fs=\"  \"gZyOOm+7j3M=\"";
                    string strArguments = BMCSecurityMethod.Encrypt(cboUserGroupList.Text) + " "
                                          + BMCSecurityMethod.Encrypt(this._UserName.Replace(" ", "")) + " "
                                          + BMCSecurityMethod.Encrypt(this._UserID.ToString());

                    AppEntryPoint.Current.StartProcess(sender, null, strFileName, strArguments, true);
                }
                else
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_UCUSERGROUP_APPNOTINSTALLED"), this.ParentForm.Text);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnNewGroup control.        
        /// Creates a new user group and populates the user group combobox
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnNewGroup_Click(object sender, EventArgs e)
        {
            try
            {
                IsNewGroup(true);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Handles the Load event of the frmUserGroup control.
        /// Populates the UserGroup Combobox while loading the screen
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void ucUserGroup_Load(object sender, System.EventArgs e)
        {
            try
            {
                txtNewGroup.Visible = false;
                btnAddGroup.Visible = false;
                btnAssociateMode.Visible = SettingsEntity.IsEmployeeCardTrackingEnabled ? true : false;
                btnAssociateMode.Enabled = AppGlobals.Current.HasUserAccess("HQ_Admin_EmployeeCard") && btnEdit.Text == this.GetResourceTextByKey("Key_EditCaption");
                if (PopulateUserGroup())
                {
                    LogManager.WriteLog("User group is loaded successfuly", LogManager.enumLogLevel.Info);
                }
                this.ResolveResources();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbUserGroupList control.
        /// To refresh the User role in Treeview control for the selected User group
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void cmbUserGroupList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (PopulateUser_Access(int.Parse(cboUserGroupList.SelectedValue.ToString()), cboUserGroupList.Text))
                {
                    LogManager.WriteLog("User group roles are loaded successfuly", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bRetValue = false;
            }
        }

        /// <summary>
        /// Handles the AfterCheck event of the tvUserGroupRoles control.
        /// To Check/Un-Check the child nodes and its corresponding parent nodes in treeview
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewEventArgs"/> instance containing the event data.</param>
        private void tvUserGroupRoles_AfterCheck(object sender, TreeViewEventArgs e)
        {
            ((User_Access)e.Node.Tag).Access_Value_New = e.Node.Checked;

            if (bChildNodeTrigger)
            {
                CheckAllChildren(e.Node, e.Node.Checked);
            }
            if (bParentNodeTrigger && e.Node.Checked)
            {
                CheckMyParent(e.Node, e.Node.Checked);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnEdit control.
        /// Updates the User role in DB based on the changes made in TreeView control
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (bEditMode)
            {
                try
                {
                    StringBuilder sbXMLDoc = new StringBuilder();
                    sbXMLDoc.AppendLine("<UserRoles>");
                    foreach (User_Role_Item<TreeNode> node in lstTreeNodes)
                    {
                        sbXMLDoc.AppendLine(node.GetXmlValue());
                    }
                    sbXMLDoc.AppendLine("</UserRoles>");
                    string strXMLDoc = sbXMLDoc.ToString();

                    int iResult = objUserGroupBiz.UpdateUser_Access(int.Parse(cboUserGroupList.SelectedValue.ToString()), strXMLDoc);

                    if (iResult != 0)
                    {
                        LogManager.WriteLog("Error while updating User Role: " + iResult, LogManager.enumLogLevel.Error);
                    }
                    else
                    {
                        var alteredNodes = (from n in lstTreeNodes
                                            where n.UserAccess.IsModified == true
                                            select n).ToList();

                        foreach (User_Role_Item<TreeNode> roleItem in alteredNodes)
                        {
                            User_Access ua = roleItem.UserAccess;
                            TreeNode tnAlteredNode = roleItem.Node;//tvUserGroupRoles.Nodes.Find(strRolesAltered, true)[0];
                            string strRolesAltered = roleItem.UserAccess.Access_Key;

                            if (ua.IsModified)
                            {
                                //Calling Audit Method
                                AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());

                                business.InsertAuditData(new Audit.Transport.Audit_History
                                {
                                    EnterpriseModuleName = ModuleNameEnterprise.AUDIT_USERGROUP,
                                    Audit_Slot = "",
                                    Audit_Screen_Name = "User Groups",
                                    Audit_Field = strRolesAltered,
                                    Audit_Old_Vl = ua.Access_Value.ToString(),
                                    Audit_New_Vl = ua.Access_Value_New.ToString(),
                                    Audit_Desc = "User group '" + cboUserGroupList.Text + "' modified   ..[" + strRolesAltered.Replace("_", " ") + "]: '" + ua.Access_Value.ToString() + "' --> '" + ua.Access_Value_New.ToString() + "'",
                                    AuditOperationType = OperationType.MODIFY,
                                    Audit_User_ID = AppEntryPoint.Current.UserId,
                                    Audit_User_Name = AppEntryPoint.Current.UserName
                                }, false);

                            }
                        }
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_UCUSERGROUP_ROLESUPDATED"), this.ParentForm.Text);
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
            }

            EditUserRole();
        }

        /// <summary>
        /// Handles the Click event of the btnClose control.
        /// During edit mode it is Enabled. It will not update DB even any changes made
        /// in treeview control
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (!cboUserGroupList.Visible)
                {
                    IsNewGroup(false);
                }
                else
                {
                    EditUserRole();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void btnAddEmpCard_Click(object sender, EventArgs e)
        {
            try
            {
                frmAssociateEmployee addEmp = new frmAssociateEmployee(cboUserGroupList.Text);
                addEmp.ShowDialog();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    

        #endregion Events

        private void tvUserGroupRoles_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            try
            {
                if (_IsSuperUser)
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// This method is used for resource externalization.
        /// </summary>
        public void SetTagProperty()
        {

            try
            {
                this.btnNewGroup.Tag = "Key_NewGroup";
                this.btnRptAdmin.Tag = "Key_ReportAdmin";
                this.btnExchgAdmin.Tag = "Key_ExchangeAdminCaption";
                this.btnAssociateMode.Tag = "Key_NewEmployeeCard";
                this.btnEdit.Tag = "Key_EditCaption";
                this.btnCancel.Tag = "Key_Cancel";                
            }
            catch (Exception ex)
            {
                
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// This event handler is used to add a new user group.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddGroup_Click(object sender, EventArgs e)
        {

            try
            {
                string strGroupName = txtNewGroup.Text.Trim();

                if (Regex.IsMatch(strGroupName, @"^$|[^a-zA-Z0-9\s]", RegexOptions.CultureInvariant))
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_VALID_GRP_NAME"));
                    return;
                }

                int iResult = objUserGroupBiz.NewUserGroup(strGroupName);
                if (iResult == 0)
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_USER_GRP_NAME_EXISTS"));
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
                    this.ShowInfoMessageBox("User Group has been saved successfully !");
                    PopulateUserGroup();
                    IsNewGroup(false);
                }
            }
            catch (Exception ex)
            {
                
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// This method is used to make enable/disable functionality in the usergroup user control.
        /// </summary>
        /// <param name="flag"></param>
        private void IsNewGroup(bool flag)
        {
            try
            {
                cboUserGroupList.Visible = !flag;
                txtNewGroup.Visible = flag;

                btnNewGroup.Visible = !flag;
                btnAddGroup.Visible = flag;

                btnEdit.Enabled = !flag;
                btnCancel.Enabled = flag;
            }
            catch (Exception ex)
            {                
                ExceptionManager.Publish(ex);
            }
        }
    }
}
