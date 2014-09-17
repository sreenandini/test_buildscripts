using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using BMC.EnterpriseBusiness.Business.UserSiteAccess;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseClient.Helpers;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.Utilities;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class ucUserSiteAccess : UserControl
    {
        List<CompaniesResult> CheckedNodes = new List<CompaniesResult>();

        #region Audit
        const string AUDITSCREENNAME = "User Site Access";
        AuditViewerBusiness bizAudit = null;

        bool bViewAllCompany = false;
        bool bViewAllDepot = false;
        string sAuditDesc = "";

        #endregion Audit

        #region Object
        UserSiteAccess instance = null;
        #endregion Object

        #region Variables
        bool bChildNodeTrigger;
        bool bParentNodeTrigger;

        #endregion Variables

        #region Constructor
        public ucUserSiteAccess()
        {
            InitializeComponent();
            bChildNodeTrigger = true;
            bParentNodeTrigger = true;
            // Set Tags for controls
            SetTagProperty();
        }
        #endregion Constructor
          private void SetTagProperty()
        {
            try
            {
                this.ChkViewDepots.Tag = "Key_ViewAllDepots";
                this.btnNewGroup.Tag="Key_NewGroup";
                this.chkSelectAllSites.Tag="Key_SelectAllQuestion";
                this.lblOperatorAccess.Tag="Key_SelectDepotThatGroupHasAccess";
                this.lblSiteAccess.Tag="Key_SelectSitesThatGroupHasAccess";
                this.lblSubCmpAccess.Tag="Key_SelectSubCompaniesThatGroupHasAccess";
                this.btnUpdate.Tag="Key_UpdateCaption";
                this.chkViewAllCompanies.Tag="Key_ViewAllCompanies";

            }
                
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #region Form_Events
        /// <summary>
        /// Load initial settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       
        void ucUserSiteAccess_Load(object sender, System.EventArgs e)
        {
            try
            {
                LoadDefaultSettings();
                ExecuteSelectionChange();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }    
        }

        private void LoadDefaultSettings()
        {
            try
            {

                LoadAccess();



                txtNewGroup.Text = string.Empty;

                this.ResolveResources();
                cboCustomerAccess.SelectedIndex = 0;

                tvCompany.Enabled = tvDepots.Enabled = tvSite.Enabled = false;
                chkViewAllCompanies.Enabled = ChkViewDepots.Enabled = chkSelectAllSites.Enabled = false;
                bViewAllDepot = bViewAllCompany = false;

                //Fill companies
                FillTreeViewCompanies(tvCompany);

                //Fill operators
                FillTreeViewDepots(tvDepots);

                // Fill sites
                FillTreeViewSites(tvSite);
                txtNewGroup.Visible = false;
                btnCancel.Enabled = false;
                btnNewGroup.Enabled = true;
                btnUpdate.Visible = false;
                btnEdit.Visible = true;
                cboCustomerAccess.Visible = true;

                FillDepotAccess((cboCustomerAccess.SelectedItem as SiteCustomerAccessResult).Customer_Access_ID);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Creates a new group for customer access
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewGroup_Click(object sender, EventArgs e)
        {

            try
            {
                cboCustomerAccess.Visible = false;
                txtNewGroup.Visible = true;
                btnNewGroup.Visible = false;
                btnSaveGroup.Visible = true;
                btnEdit.Enabled = false;
                btnCancel.Enabled = true;

                tvCompany.Nodes.Clear();
                tvDepots.Nodes.Clear();
                tvSite.Nodes.Clear();
                txtNewGroup.Focus();
             
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
           
        }

        /// <summary>
        /// Handles the AfterCheck event of the tvCompany control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewEventArgs"/> instance containing the event data.</param>
        private void tvCompany_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //SelectAll(e.Node);
            try
            {
                TreeNode node = e.Node;

            
                TreeNodeCollection nodes = this.tvCompany.Nodes;
             
                if (e.Action == TreeViewAction.ByKeyboard || e.Action == TreeViewAction.ByMouse)
                {
                    bool nodeChecked = node.Checked;
                    if(node.Tag is CompaniesResult)
                        (node.Tag as CompaniesResult).IsUpdated = nodeChecked;
                    if (node.Nodes.Count > 0)
                    {
                        foreach (TreeNode child in node.Nodes)
                        {
                            child.Checked = nodeChecked;
                            if(child.Tag is CompaniesResult)
                                (child.Tag as CompaniesResult).IsUpdated = nodeChecked;
                        }
                    }
                    else
                    {
                        if (node.Parent != null)
                        {
                            if (node.Checked && !node.Parent.Checked)
                            {
                                node.Parent.Checked = true;
                            }
                        }
                    }

                    FillTreeViewSites(tvSite);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// On Selecting the root node all child nodes should be selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvDepots_AfterCheck(object sender, TreeViewEventArgs e)
        {
            SelectAll(e.Node);
        }

        /// <summary>
        /// Handles the AfterCheck event of the tvSite control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewEventArgs"/> instance containing the event data.</param>
        private void tvSite_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (!e.Node.Checked)
                chkSelectAllSites.Checked = false;
        }

        /// <summary>
        /// Enable the Depots treeview only if the ViewDepots checkbox is checked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkViewDepots_CheckedChanged(object sender, EventArgs e)
        {
            tvDepots.Enabled = ChkViewDepots.Checked ? false : true;
        }

        /// <summary>
        /// Enable the companies treeview only if the ViewCompany checkbox is checked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkViewAllCompanies_CheckedChanged(object sender, EventArgs e)
        {
        tvCompany.Enabled = chkViewAllCompanies.Checked ? false : true;
            FillTreeViewSites(tvSite);
        }

        /// <summary>
        /// Handles the Click event of the chkSelectAllSites control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void chkSelectAllSites_Click(object sender, EventArgs e)
        {
            if (chkSelectAllSites.Checked)
            {
                foreach (TreeNode node in tvSite.Nodes)
                {
                    node.Checked = true;
                }
            }
            else
            {
                foreach (TreeNode node in tvSite.Nodes)
                {
                    node.Checked = false;
                }
            }
        }

        /// <summary>
        /// Update the customer access table with the selected access for companies, depots and sites.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cboCustomerAccess.SelectedIndex < 0) return;
           
            bool bSuccess = true;

            int iSelectedIndx = cboCustomerAccess.SelectedIndex;
            int iCustomerAccessID = (cboCustomerAccess.SelectedItem as SiteCustomerAccessResult).Customer_Access_ID;

            #region Update status for subcompanies
            try
            {
                if (!chkViewAllCompanies.Checked)
                {
                    foreach (TreeNode childNode in tvCompany.Nodes)
                    {
                        foreach (TreeNode Node in childNode.Nodes)
                        {
                            string sSub_CompanyID = string.Empty;

                            if (Node.Tag != null)
                            {
                                sSub_CompanyID = ((CompaniesResult)Node.Tag).Sub_Company_ID.ToString();
                                bool bIsUpdated = ((CompaniesResult)Node.Tag).IsUpdated;
                                
                                instance.UpdateCustomerAccessForCompanies(iCustomerAccessID, sSub_CompanyID, chkViewAllCompanies.Checked, Node.Checked);

                                if (Node.Checked != bIsUpdated)
                                {
                                    sAuditDesc = sSub_CompanyID + (Node.Checked ? " added to " : " removed from ") + "User Site Access.Sub_Company";

                                    bizAudit.InsertAuditData(new Audit.Transport.Audit_History
                                    {
                                        EnterpriseModuleName = ModuleNameEnterprise.AUDIT_CUSTOMERACCESS,
                                        Audit_Slot = "",
                                        Audit_Screen_Name = AUDITSCREENNAME + ".Sub_Company",
                                        Audit_Field = "Sub_Company_ID",
                                        Audit_Old_Vl = Node.Checked ? "" : sSub_CompanyID,
                                        Audit_New_Vl = Node.Checked ? sSub_CompanyID : "",
                                        Audit_Desc = sAuditDesc,
                                        AuditOperationType = (Node.Checked ? OperationType.ADD : OperationType.DELETE),
                                        Audit_User_ID = AppEntryPoint.Current.UserId,
                                        Audit_User_Name = AppEntryPoint.Current.UserName
                                    }, false);
                                    sAuditDesc = "";
                                }
                            }
                        }
                    }
                }

                instance.UpdateCustomerAccessForCompanies(iCustomerAccessID, "0", chkViewAllCompanies.Checked, false);

                //Audit Process
                if (bViewAllCompany != chkViewAllCompanies.Checked)
                {
                    sAuditDesc = "User Group Access for " + cboCustomerAccess.Text + " modified ..[Customer Access View All Companies]: '" + bViewAllCompany + "' --> '" + chkViewAllCompanies.Checked + "'";

                    bizAudit.InsertAuditData(new Audit.Transport.Audit_History
                    {
                        EnterpriseModuleName = ModuleNameEnterprise.AUDIT_CUSTOMERACCESS,
                        Audit_Slot = "",
                        Audit_Screen_Name = AUDITSCREENNAME,
                        Audit_Field = "Customer_Access_View_All_Companies",
                        Audit_Old_Vl = bViewAllCompany.ToString(),
                        Audit_New_Vl = chkViewAllCompanies.Checked.ToString(),
                        Audit_Desc = sAuditDesc,
                        AuditOperationType = OperationType.MODIFY,
                        Audit_User_ID = AppEntryPoint.Current.UserId,
                        Audit_User_Name = AppEntryPoint.Current.UserName
                    }, false);
                    sAuditDesc = "";
                }
            }
            catch (Exception ex)
            {
                bSuccess = false;
                LogManager.WriteLog("Site Access : Error while updating Company Access -Error Message- " + ex.Message, LogManager.enumLogLevel.Error);
            }
            #endregion Update status for subcompanies

            #region Update status for depots
            try
            {
                if (!ChkViewDepots.Checked)
                {
                    foreach (TreeNode childNode in tvDepots.Nodes)
                    {
                        foreach (TreeNode Node in childNode.Nodes)
                        {
                            string sDepot_ID = string.Empty;

                            if (Node.Tag != null)
                            {
                                sDepot_ID = ((OperatorsResult)Node.Tag).Depot_ID.ToString();
                                bool bIsUpdated = ((OperatorsResult)Node.Tag).IsUpdated;

                                instance.UpdateCustomerAccessForDepots(iCustomerAccessID, sDepot_ID, ChkViewDepots.Checked, Node.Checked);

                                if (Node.Checked != bIsUpdated)
                                {
                                    sAuditDesc = sDepot_ID + (Node.Checked ? " added to " : " removed from ") + "User Site Access.Depot";

                                    bizAudit.InsertAuditData(new Audit.Transport.Audit_History
                                    {
                                        EnterpriseModuleName = ModuleNameEnterprise.AUDIT_CUSTOMERACCESS,
                                        Audit_Slot = "",
                                        Audit_Screen_Name = AUDITSCREENNAME + ".Sub_Company",
                                        Audit_Field = "Depot_ID",
                                        Audit_Old_Vl = Node.Checked ? "" : sDepot_ID,
                                        Audit_New_Vl = Node.Checked ? sDepot_ID : "",
                                        Audit_Desc = sAuditDesc,
                                        AuditOperationType = (Node.Checked ? OperationType.ADD : OperationType.DELETE),
                                        Audit_User_ID = AppEntryPoint.Current.UserId,
                                        Audit_User_Name = AppEntryPoint.Current.UserName
                                    }, false);
                                    sAuditDesc = "";
                                }
                            }
                        }
                    }
                }

                instance.UpdateCustomerAccessForDepots(iCustomerAccessID, "0", ChkViewDepots.Checked, false);

                //Audit Process
                if (bViewAllDepot != chkViewAllCompanies.Checked)
                {
                    sAuditDesc = "User Group Access for " + cboCustomerAccess.Text + " modified ..[Customer Access View All Depot]: '" + bViewAllDepot + "' --> '" + ChkViewDepots.Checked + "'";

                    bizAudit.InsertAuditData(new Audit.Transport.Audit_History
                    {
                        EnterpriseModuleName = ModuleNameEnterprise.AUDIT_CUSTOMERACCESS,
                        Audit_Slot = "",
                        Audit_Screen_Name = AUDITSCREENNAME,
                        Audit_Field = "Customer_Access_View_All_Depots",
                        Audit_Old_Vl = bViewAllDepot.ToString(),
                        Audit_New_Vl = ChkViewDepots.Checked.ToString(),
                        Audit_Desc = sAuditDesc,
                        AuditOperationType = OperationType.MODIFY,
                        Audit_User_ID = AppEntryPoint.Current.UserId,
                        Audit_User_Name = AppEntryPoint.Current.UserName
                    }, false);
                    sAuditDesc = "";
                }
            }
            catch (Exception ex)
            {
                bSuccess = false;
                LogManager.WriteLog("Site Access : Error while updating Depot Access -Error Message- " + ex.Message, LogManager.enumLogLevel.Error);
            }

            #endregion Update status for depots

            #region Update status for Sites
            try
            {
                if (tvSite.Nodes.Count > 0)
                {
                    foreach (TreeNode Node in tvSite.Nodes)
                    {
                        string sSite_ID = string.Empty;

                        if (Node.Tag != null)
                        {
                            sSite_ID = ((SitesResult)Node.Tag).Site_ID.ToString();
                            bool bIsUpdated = ((SitesResult)Node.Tag).IsUpdated;
                            string sSite_Name = Node.Text;
                            instance.UpdateCustomerAccessForSites(iCustomerAccessID, 2, sSite_ID, Node.Checked, string.Empty);
                            if (bIsUpdated != Node.Checked)
                            {
                              

                                sAuditDesc = (Node.Checked ? "Record " + sSite_Name + " added to " : sSite_Name + " deleted from ") + "User Site Access.Site";

                                bizAudit.InsertAuditData(new Audit.Transport.Audit_History
                                {
                                    EnterpriseModuleName = ModuleNameEnterprise.AUDIT_CUSTOMERACCESS,
                                    Audit_Slot = "",
                                    Audit_Screen_Name = AUDITSCREENNAME + ".Site",
                                    Audit_Field = "AllowUser",
                                    Audit_Old_Vl = Node.Checked ? "" : sSite_Name,
                                    Audit_New_Vl = Node.Checked ? sSite_Name : "",
                                    Audit_Desc = sAuditDesc,
                                    AuditOperationType = (Node.Checked ? OperationType.ADD : OperationType.DELETE),
                                    Audit_User_ID = AppEntryPoint.Current.UserId,
                                    Audit_User_Name = AppEntryPoint.Current.UserName
                                }, false);
                                sAuditDesc = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                bSuccess = false;
                LogManager.WriteLog("Site Access : Error while updating Company Access -Error Message- " + ex.Message, LogManager.enumLogLevel.Error);
            }
            #endregion Update status for Sites

            if (!bSuccess)
                //this.ShowInfoMessageBox("Customer Access updated failed");
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_SITEDETAILS_USERPERMISSION"), this.ParentForm.Text);

            else
            {
                //lstCustomerAccess.SelectedIndex = iSelectedIndx;
                //this.ShowInfoMessageBox("Customer Access updated Successfully");
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_SITEDETAILSUPDATE_SUCCESS"), this.ParentForm.Text);

                tvCompany.Enabled = tvDepots.Enabled = tvSite.Enabled = false;
                chkViewAllCompanies.Enabled = ChkViewDepots.Enabled = chkSelectAllSites.Enabled = false;
                bViewAllDepot = bViewAllCompany = false;
                btnEdit.Visible = true;
                btnUpdate.Visible = false;
                btnCancel.Enabled = false;
                btnNewGroup.Enabled = true;
                cboCustomerAccess.Enabled = true;

                //IsGroupEditable(false);
            }

        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the lstCustomerAccess control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void cboCustomerAccess_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCustomerAccess.Focused)
            {
                ExecuteSelectionChange();
            }

        }

        private void ExecuteSelectionChange()
        {
            CheckedNodes.Clear();

            //Fill companies
            FillTreeViewCompanies(tvCompany);

            //Fill operators
            FillTreeViewDepots(tvDepots);



            if (cboCustomerAccess.SelectedIndex >= 0)
            {
                tvCompany.Enabled = tvDepots.Enabled = tvSite.Enabled = true;
                chkViewAllCompanies.Enabled = ChkViewDepots.Enabled = chkSelectAllSites.Enabled = true;
                FillCompanyAccess((cboCustomerAccess.SelectedItem as SiteCustomerAccessResult).Customer_Access_ID);
                // Fill sites
                FillTreeViewSites(tvSite);
                FillDepotAccess((cboCustomerAccess.SelectedItem as SiteCustomerAccessResult).Customer_Access_ID);

                bViewAllCompany = chkViewAllCompanies.Checked;
                bViewAllDepot = ChkViewDepots.Checked;
            }
            tvCompany.Enabled = tvDepots.Enabled = tvSite.Enabled = false;
            chkViewAllCompanies.Enabled = ChkViewDepots.Enabled = chkSelectAllSites.Enabled = false;
            bViewAllDepot = bViewAllCompany = false;
            tvCompany.ExpandAll();
            tvDepots.ExpandAll();
        }

        #endregion Form_Events

        #region User Function
        /// <summary>
        /// Function used to load the settings initially
        /// </summary>
        private void LoadAccess()
        {
            instance = UserSiteAccess.CreateInstance();
            bizAudit = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());

            //Fill customer access
            FillCustomerAccess(cboCustomerAccess);           
        }

        /// <summary>
        /// Fills the customer access.
        /// </summary>
        /// <param name="view">The view.</param>
        private void FillCustomerAccess(ComboBox view)
        {
            try
            {
                //Fill the Sites
                view.DataSource = null;

                IEnumerable<SiteCustomerAccessResult> lstCustAccess =
                    instance.GetAllSiteCustomerAccess(
                    cboCustomerAccess.SelectedItem != null ? (view.SelectedItem as SiteCustomerAccessResult).Customer_Access_ID
                    : 0);
                view.DataSource = lstCustAccess.ToList();
                view.DisplayMember = "Customer_Access_Name";                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Fills the tree view companies.
        /// </summary>
        /// <param name="view">The view.</param>
        private void FillTreeViewCompanies(TreeView view)
        {
            try
            {
                //Fill the companies
                List<CompaniesResult> lstCompanies = null;

                lstCompanies = instance.GetAllCompanies();

                if (lstCompanies != null && lstCompanies.Count > 0)
                {
                    view.Nodes.Clear();

                    int iOldCompany = 0;
                    int iSubCompany = 0;
                    string sCompany_Name = string.Empty;

                    TreeNode[] tnChild = null;

                    foreach (CompaniesResult node in lstCompanies)
                    {
                        if (iOldCompany == 0)
                        {
                            tnChild = new TreeNode[lstCompanies.Count(e => e.Company_ID == node.Company_ID)];
                        }

                        if (iOldCompany != 0 && iOldCompany != node.Company_ID)
                        {
                            iSubCompany = 0;
                            view.Nodes.Add(new TreeNode(sCompany_Name, tnChild));
                            tnChild = new TreeNode[lstCompanies.Count(e => e.Company_ID == node.Company_ID)];
                        }

                        sCompany_Name = node.Company_Name;

                        tnChild[iSubCompany] = new TreeNode(node.Sub_Company_Name);
                        tnChild[iSubCompany++].Tag = node;
                        iOldCompany = node.Company_ID;
                    }

                    view.Nodes.Add(new TreeNode(sCompany_Name, tnChild));

                    chkViewAllCompanies.Checked = false;
                    tvCompany.ExpandAll();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Fills the company access.
        /// </summary>
        /// <param name="CustomerAccessID">The customer access ID.</param>
        private void FillCompanyAccess(int CustomerAccessID)
         {
            try
            {
                //Fill the companies access
                List<CustomerAccessCompanyResult> lstCompanies = instance.GetCompanyAccess(CustomerAccessID).ToList();
                CheckedNodes.Clear();
                List<CustomerAccessCompanyResult> filtered;

                foreach (TreeNode node in tvCompany.Nodes)
                {
                    if (node.GetNodeCount(true) > 0)
                    {
                        foreach (TreeNode subnode in node.Nodes)
                        {
                            CompaniesResult oSubComAccess = (CompaniesResult)subnode.Tag;

                            filtered = lstCompanies.Where(exist => exist.Sub_Company_Name == oSubComAccess.Sub_Company_Name && exist.Sub_Company_ID == oSubComAccess.Sub_Company_ID).ToList();
                            if (filtered.Count > 0)
                            {
                                subnode.Checked = true;
                                ((CompaniesResult)subnode.Tag).IsUpdated = true;
                            }
                            else
                            {
                                subnode.Checked = false;
                                ((CompaniesResult)subnode.Tag).IsUpdated = false;
                            }

                            if (!chkViewAllCompanies.Checked)
                            {
                               
                                CheckedNodes.Add((CompaniesResult)subnode.Tag);
                            }
                        }
                    }
                }

                chkViewAllCompanies.Checked = false;

                SiteCustomerAccessResult siteStat = cboCustomerAccess.SelectedItem as SiteCustomerAccessResult;
                chkViewAllCompanies.Checked = siteStat.Customer_Access_View_All_Companies ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Fills the tree view depots.
        /// </summary>
        /// <param name="view">The view.</param>
        private void FillTreeViewDepots(TreeView view)
        {
            try
            {
                //Fill the Operators
                List<OperatorsResult> lstOperators = null;

                lstOperators = instance.GetAllOperators();

                if (lstOperators != null && lstOperators.Count > 0)
                {
                    view.Nodes.Clear();

                    int iOldOperator = 0;
                    int iDepot = 0;
                    string sOperator_Name = string.Empty;

                    TreeNode[] tnChild = null;

                    foreach (OperatorsResult node in lstOperators)
                    {
                        if (iOldOperator == 0)
                        {
                            tnChild = new TreeNode[lstOperators.Count(e => e.Operator_ID == node.Operator_ID)];
                        }

                        if (iOldOperator != 0 && iOldOperator != node.Operator_ID)
                        {
                            iDepot = 0;
                            view.Nodes.Add(new TreeNode(sOperator_Name, tnChild));
                            tnChild = new TreeNode[lstOperators.Count(e => e.Operator_ID == node.Operator_ID)];
                        }

                        sOperator_Name = node.Operator_Name;

                        tnChild[iDepot] = new TreeNode(node.Depot_Name);
                        tnChild[iDepot++].Tag = node;
                        iOldOperator = node.Operator_ID;
                    }

                    view.Nodes.Add(new TreeNode(sOperator_Name, tnChild));

                    ChkViewDepots.Checked = false;
                    tvDepots.ExpandAll();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Fills the depot access.
        /// </summary>
        /// <param name="CustomerAccessID">The customer access ID.</param>
        private void FillDepotAccess(int CustomerAccessID)
        {
            try
            {
                //Fill the depot access
                List<CustomerAccessDepotResult> lstDepot = instance.GetDepotAccess(CustomerAccessID).ToList();

                List<CustomerAccessDepotResult> filtered;

                foreach (TreeNode node in tvDepots.Nodes)
                {
                    if (node.GetNodeCount(true) > 0)
                    {
                        foreach (TreeNode subnode in node.Nodes)
                        {
                            filtered = lstDepot.Where(exist => exist.Depot_Name == subnode.Text).ToList();
                            if (filtered.Count > 0)
                            {
                                subnode.Checked = true;
                                ((OperatorsResult)subnode.Tag).IsUpdated = true;
                            }
                            else
                            {
                                subnode.Checked = false;
                                ((OperatorsResult)subnode.Tag).IsUpdated = false;
                            }
                        }
                    }
                }

                ChkViewDepots.Checked = false;

                SiteCustomerAccessResult siteStat = cboCustomerAccess.SelectedItem as SiteCustomerAccessResult;
                ChkViewDepots.Checked = siteStat.Customer_Access_View_All_Depots ? true : false;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private List<SitesResult> lstSites = null;
        /// <summary>
        /// Fills the tree view sites.
        /// </summary>
        /// <param name="view">The view.</param>
        private void FillTreeViewSites(TreeView view)
        {
            try
            {
                List<SitesResult> filteredlstSites = null;
                if(lstSites == null || lstSites.Count <= 0)
                    lstSites = instance.GetAllSites();
                if (chkViewAllCompanies.Checked)
                {
                    filteredlstSites = lstSites;
                }

                else
                {
                    if (CheckedNodes.Count > 0)
                    {
                        filteredlstSites = (from t1 in lstSites
                                            join t2 in CheckedNodes on t1.Sub_Company_ID equals t2.Sub_Company_ID
                                            where t2.IsUpdated == true
                                            select t1).ToList();
                        //lstSites.Where(x => x.Sub_Company_ID = CheckedNodes.se
                        //    filteredlstSites = lstSites.Where(exist => exist.Sub_Company_ID == 
                    }


                }
                if (filteredlstSites == null)
                    filteredlstSites = new List<SitesResult>();

                TreeNode myNode;
                view.Nodes.Clear();

                foreach (SitesResult node in filteredlstSites)
                {
                    myNode = new TreeNode(node.Site_Name + " [" + node.Site_Code + "] " + node.Site_Address_3);


                    myNode.Tag = node;
                    view.Nodes.Add(myNode);
                }
                chkSelectAllSites.Checked = false;
                tvSite.ExpandAll();
                if(cboCustomerAccess != null && cboCustomerAccess.SelectedIndex > -1)
                    FillSiteAccess((cboCustomerAccess.SelectedItem as SiteCustomerAccessResult).Customer_Access_ID);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
           
        }

        /// <summary>
        /// Fills the site access.
        /// </summary>
        /// <param name="CustomerAccessID">The customer access ID.</param>
        private void FillSiteAccess(int CustomerAccessID)
        {
            try
            {
                //Fill the Site access
                foreach (TreeNode node in tvSite.Nodes)
                {
                    node.Checked = false;
                }
                List<CustomerAccessSiteResult> lstSite = instance.GetSiteAccess(CustomerAccessID).ToList();
                List<CustomerAccessSiteResult> filtered;

                foreach (TreeNode node in tvSite.Nodes)
                {
                    SitesResult oSiteAccess = (SitesResult)node.Tag;
                    filtered = lstSite.Where(exist => exist.SecurityProfileType_Value == oSiteAccess.Site_ID.ToString()).ToList();
                    if (filtered.Count > 0)
                    {
                        node.Checked = filtered[0].AllowUser.Value;
                        oSiteAccess.IsUpdated = filtered[0].AllowUser.Value;
                    }
                }

                chkSelectAllSites.Checked = false;

                SiteCustomerAccessResult siteStat = cboCustomerAccess.SelectedItem as SiteCustomerAccessResult;
                chkSelectAllSites.Checked = siteStat.Customer_Access_View_All_Sites ? true : false;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #region Tree View
        /// <summary>
        /// Function to select all nodes in treeview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectAll(TreeNode eNode)
        {
            if (bChildNodeTrigger)
            {
                CheckAllChildren(eNode, eNode.Checked);
            }
            if (bParentNodeTrigger && eNode.Checked)
            {
                CheckMyParent(eNode, eNode.Checked);
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
        #endregion Tree View

        private void btnSaveGroup_Click(object sender, EventArgs e)
        {
            string sNewGroupName = txtNewGroup.Text;
            //Validate input group name
            if (string.IsNullOrEmpty(sNewGroupName.Trim()))
            {
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CUST_ACCESS_GRP_CANNOT_EMPTY"), this.Text);
                this.txtNewGroup.Focus();
                return;
            }

            CreateNewGroup(sNewGroupName);
            //Calling Audit Method
            bizAudit.InsertAuditData(new Audit.Transport.Audit_History
            {
                EnterpriseModuleName = ModuleNameEnterprise.AUDIT_CUSTOMERACCESS,
                Audit_Slot = "",
                Audit_Screen_Name = AUDITSCREENNAME,
                Audit_Field = "Customer_Access_Name",
                Audit_Old_Vl = "",
                Audit_New_Vl = sNewGroupName,
                Audit_Desc = "Record  [" + sNewGroupName + "] added to User Site Access",
                AuditOperationType = OperationType.ADD,
                Audit_User_ID = AppEntryPoint.Current.UserId,
                Audit_User_Name = AppEntryPoint.Current.UserName
            }, false);
            FillCustomerAccess(cboCustomerAccess);

            btnSaveGroup.Visible = false;
            btnNewGroup.Visible = true;
            cboCustomerAccess.Visible = true;
            txtNewGroup.Visible = false;
            btnEdit.Enabled = true;
            btnCancel.Enabled = false;

            tvCompany.Enabled = tvDepots.Enabled = tvSite.Enabled = false;
            chkViewAllCompanies.Enabled = ChkViewDepots.Enabled = chkSelectAllSites.Enabled = false;
            bViewAllDepot = bViewAllCompany = false;
            ExecuteSelectionChange();
            //IsNewGroup(false);
        }
    
        #endregion User Function

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnEdit.Visible = false;
            btnUpdate.Visible = true;
            btnNewGroup.Enabled = false;
            btnCancel.Enabled = true;
            cboCustomerAccess.Enabled = false;

            tvCompany.Enabled = tvDepots.Enabled = tvSite.Enabled = true;
            chkViewAllCompanies.Enabled = ChkViewDepots.Enabled = chkSelectAllSites.Enabled = true;
            bViewAllDepot = bViewAllCompany = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
                LoadDefaultSettings();
                ExecuteSelectionChange();
                cboCustomerAccess.Enabled = true;
                btnEdit.Enabled = true;
                btnNewGroup.Visible = true;
                btnSaveGroup.Visible = false;
                cboCustomerAccess.SelectedIndex = 0;
        }
        #region User Function

        /// <summary>
        /// This method is used to create the new user access group.
        /// </summary>
        /// <param name="sNewGroup"></param>
        public void CreateNewGroup(string sNewGroup)
        {
            //Save the customer access group
            switch (instance.UpdateCustomerAccess(sNewGroup))
            {
                case 0:
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CUST_ACCESS_DETAILS_SAVE_SUCCESS"), this.Text);                      
                        break;
                    }
                case 1:
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CUST_ACCESS_DETAILS_EXISTS"), this.Text);
                        break;
                    }
                default:
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CUST_ACCESS_DETAILS_SAVE_FAILED"), this.Text);
                        break;
                    }
            }
        }
        #endregion User Function

    }
}
