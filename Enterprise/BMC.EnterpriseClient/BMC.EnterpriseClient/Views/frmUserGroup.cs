using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.LogManagement;
using System.Text;
using Audit.Transport;
using Audit.BusinessClasses;
using BMC.Common.Utilities;
using System.IO;
using BMC.SecurityVB;
using BMC.Common.ExceptionManagement;
using BMC.CoreLib.Win32;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmUserGroup : Form
    {
        #region Local Declartion
         BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;

        #region Objects
        private UserGroupBiz objUserGroupBiz = null;
        List<UserGroup> lstUserGroup = null;
        List<User_Access> lstUser_Access = null;
        IDictionary<int, TreeNodeCollection> dicUsrGrpRoles = null;
        IList<User_Role_Item<TreeNode>> lstTreeNodes = new List<User_Role_Item<TreeNode>>();        
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
                cmbUserGroupList.DisplayMember = "User_Group_Name";
                cmbUserGroupList.ValueMember = "User_Group_ID";
                cmbUserGroupList.DataSource = lstUserGroup;
                bRetValue = true;
                objDatawatcher = new Helpers.Datawatcher(this);
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
        private bool PopulateUser_Access(int iUserGroupId)
        {
            try
            {
                lstUser_Access = objUserGroupBiz.GetUser_Access(iUserGroupId);
                FrameUserGroupRoles();
                bRetValue = true;
                objDatawatcher = new Helpers.Datawatcher(this);
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
            User_Access x = (from acess in lstUser_Access
                             where acess.Access_Name == "Reports"
                             select acess).Single<User_Access>();
            return x.Access_Value;
        }

        /// <summary>
        /// frameUserGroupRoles
        /// Frames the user group roles in treeview
        /// </summary>
        private void FrameUserGroupRoles()
        {
            tvUserGroupRoles.Nodes.Clear(); // Clear any existing items
            tvUserGroupRoles.BeginUpdate(); // prevent overhead and flicker
            LoadGroupRoles();               // load all the lowest tree nodes
            tvUserGroupRoles.EndUpdate();   // re-enable the tree
            tvUserGroupRoles.Refresh();     // refresh the treeview display
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
            if (!bEditMode)
            {
                cmbUserGroupList.Enabled = false;
                btnNewGroup.Enabled = false;
                tvUserGroupRoles.Enabled = true;
                btnExchgAdmin.Enabled = true;
                btnClose.Visible = true;
                btnRptAdmin.Enabled = IsEnabled();
                btnEdit.Text = "&Update";
                bEditMode = true;
                btnAddEmpCard.Enabled = true;
                btnCardTypes.Enabled = true;
            }
            else
            {
                cmbUserGroupList.Enabled = true;
                btnNewGroup.Enabled = true;
                tvUserGroupRoles.Enabled = false;
                btnRptAdmin.Enabled = false;
                btnExchgAdmin.Enabled = false;
                btnClose.Visible = false;
                bEditMode = false;
                btnAddEmpCard.Enabled = false;
                btnCardTypes.Enabled = false;
                btnEdit.Text = "$Edit";
            }

            PopulateUser_Access(int.Parse(cmbUserGroupList.SelectedValue.ToString()));
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

                foreach (User_Access objUser_Access in lstUser_Access)
                {
                    TreeNodeCollection nodeParent = null;

                    TreeNode nodeChild = new TreeNode(objUser_Access.Access_Name);
                    nodeChild.Tag = objUser_Access;
                    nodeChild.Name = objUser_Access.Access_Key;
                    nodeChild.Checked = objUser_Access.Access_Value;

                    if (!dicUsrGrpRoles.ContainsKey(objUser_Access.Access_Parent_Id))
                    {
                        nodeParent = tvUserGroupRoles.Nodes;
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
                LogManager.WriteLog("Error while populating roles -Error Message- " + ex.Message, LogManager.enumLogLevel.Error);
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

        /// <summary>
        /// CheckMyParent
        /// Verify the current node whether it has parent node or not.
        /// If yes moves to parent node and check/uncheck the node based on bCheck param
        /// </summary>
        /// <param name="treeNode">TreeNode</param>
        /// <param name="bCheck">Bool</param>
        private void CheckMyParent(TreeNode treeNode, Boolean bCheck)
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

        #endregion User Function End

        #region Events

        /// <summary>
        /// Initializes a new instance of the <see cref="frmUserGroup"/> class.
        /// </summary>
        public frmUserGroup()
        {
            InitializeComponent();

            bRetValue = true;
            bChildNodeTrigger = true;
            bParentNodeTrigger = true;
            bEditMode = false;

            objUserGroupBiz = UserGroupBiz.CreateInstance();
            dicUsrGrpRoles = new SortedDictionary<int, TreeNodeCollection>();
            SetTagProperty();
        }

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
                    string strArguments = BMCSecurityMethod.Encrypt(cmbUserGroupList.Text) + " "
                                          + BMCSecurityMethod.Encrypt(AppEntryPoint.Current.UserName.Replace(" ", "")) + " "
                                          + BMCSecurityMethod.Encrypt(AppEntryPoint.Current.UserId.ToString());

                    AppEntryPoint.Current.StartProcess(sender, null, strFileName, strArguments, true);
                }
                else
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_UCUSERGROUP_APPNOTINSTALLED"), this.Text);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in running Report Admin application -Error Message- " + ex.Message, LogManager.enumLogLevel.Error);
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
                    string strArguments = BMCSecurityMethod.Encrypt(cmbUserGroupList.Text) + " "
                                          + BMCSecurityMethod.Encrypt(AppEntryPoint.Current.UserName.Replace(" ", "")) + " "
                                          + BMCSecurityMethod.Encrypt(AppEntryPoint.Current.UserId.ToString());

                    AppEntryPoint.Current.StartProcess(sender, null, strFileName, strArguments, true);
                }
                else
                    LogManager.WriteLog("**********Application.StartupPath************ - " + Application.StartupPath, LogManager.enumLogLevel.Info);
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_UCUSERGROUP_APPNOTINSTALLED"), this.Text);
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
            using (frmNewGroup objFrmNewGroup = new frmNewGroup(this.Text))
            {
                if (objFrmNewGroup.ShowDialog(this) == DialogResult.OK)
                {
                    PopulateUserGroup();
                }
            }
        }

        /// <summary>
        /// Handles the Load event of the frmUserGroup control.
        /// Populates the UserGroup Combobox while loading the screen
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void frmUserGroup_Load(object sender, EventArgs e)
        {
            try
            {
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
                if (PopulateUser_Access(int.Parse(cmbUserGroupList.SelectedValue.ToString())))
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

                    int iResult = objUserGroupBiz.UpdateUser_Access(int.Parse(cmbUserGroupList.SelectedValue.ToString()), strXMLDoc);

                    if (iResult != 0)
                    {
                        LogManager.WriteLog("Error while updating User Role: " + iResult, LogManager.enumLogLevel.Error);
                    }
                    else
                    {
                        var alteredNodes = (from n in lstTreeNodes
                                            where n.UserAccess.IsModified == true
                                            select n).ToList();
                        
                        foreach(User_Role_Item<TreeNode> roleItem in alteredNodes)
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
                                    Audit_Desc = "User group '" + cmbUserGroupList.Text + "' modified   ..[" + strRolesAltered.Replace("_", " ") + "]: '" + ua.Access_Value.ToString() + "' --> '" + ua.Access_Value_New.ToString() + "'",
                                    AuditOperationType = OperationType.MODIFY,
                                    Audit_User_ID = AppEntryPoint.Current.UserId,
                                    Audit_User_Name = AppEntryPoint.Current.UserName
                                }, false);

                            }                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("Error while updating User group roles -Error Message- " + ex.Message, LogManager.enumLogLevel.Error);
                }                
            }

            EditUserRole();
        }

        /// <summary>
        /// Handles the Click event of the btnClose control.
        /// During edit mode it is visible. It will not update DB even any changes made
        /// in treeview control
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            EditUserRole();            
        }

        #endregion Events

        private void btnAddEmpCard_Click(object sender, EventArgs e)
        {
            frmAddEmployeeCard addEmp = new frmAddEmployeeCard();
            addEmp.ShowDialog();
        }

        private void btnCardTypes_Click(object sender, EventArgs e)
        {
            frmEmployeeCardTypes cardTypes = new frmEmployeeCardTypes();
            cardTypes.ShowDialog();
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {

        }

        public void SetTagProperty()
        {
            this.btnClose.Tag = "Key_Close";
            this.btnAddEmpCard.Tag = "Key_NewCaption";
            this.btnRptAdmin.Tag = "Key_ReportAdminCaption";
            this.btnCardTypes.Tag = "Key_CardTypesCaption";
            this.btnEdit.Tag = "Key_Edit";
            this.btnExchgAdmin.Tag = "Key_ExchangeAdminCaption";
            this.btnNewGroup.Tag = "Key_NewGroup";
            this.Tag = "Key_UserGroupAdministration";
        }

    }
}
