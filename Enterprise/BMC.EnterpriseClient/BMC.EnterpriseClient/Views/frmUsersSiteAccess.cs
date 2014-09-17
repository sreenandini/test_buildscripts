using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business.UserSiteAccess;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseClient.Helpers;
using BMC.Common.ExceptionManagement;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmUsersSiteAccess : Form
    {
        UserSiteAccess instance = null;
        public frmUsersSiteAccess()
        {
            InitializeComponent();
            LoadAccess();
        }

        private void FillTreeViewCompanies(TreeView view)
        {
            try
            {
                //Fill the companies
                IEnumerable<CompaniesResult> lstCompanies = instance.GetAllCompanies();

                TreeNode myNode;
                view.Nodes.Clear();
                foreach (CompaniesResult node in lstCompanies)
                {
                    myNode = new TreeNode(node.Sub_Company_Name);
                    myNode.Tag = node.Sub_Company_ID;

                    view.Nodes.Add(new TreeNode(node.Company_Name, new TreeNode[] { myNode }));
                }

                chkViewAllCompanies.Checked = false;

                SiteCustomerAccessResult siteStat = lstCustomerAccess.SelectedItem as SiteCustomerAccessResult;
                chkViewAllCompanies.Checked = siteStat.Customer_Access_View_All_Companies ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void FillTreeViewDepots(TreeView view)
        {
            try
            {
                //Fill the Operators
                IEnumerable<OperatorsResult> lstOperators = instance.GetAllOperators();
                TreeNode myNode;

                view.Nodes.Clear();
                foreach (OperatorsResult node in lstOperators)
                {
                    myNode = new TreeNode(node.Depot_Name);
                    myNode.Tag = node.Depot_ID;
                    view.Nodes.Add(new TreeNode(node.Operator_Name, new TreeNode[] { myNode }));
                }

                ChkViewDepots.Checked = false;

                SiteCustomerAccessResult siteStat = lstCustomerAccess.SelectedItem as SiteCustomerAccessResult;
                ChkViewDepots.Checked = siteStat.Customer_Access_View_All_Depots ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void FillTreeViewSites(TreeView view)
        {
            try
            {
                //Fill the Sites
                IEnumerable<SitesResult> lstSites = instance.GetAllSites();
                TreeNode myNode;
                view.Nodes.Clear();

                foreach (SitesResult node in lstSites)
                {
                    myNode = new TreeNode(node.Site_Name + "[ " + node.Site_Code + " ]" + node.Site_Address_2);
                    myNode.Tag = node.Site_ID;
                    view.Nodes.Add(myNode);
                }

                chkSelectAllSites.Checked = false;

                SiteCustomerAccessResult siteStat = lstCustomerAccess.SelectedItem as SiteCustomerAccessResult;
                chkSelectAllSites.Checked = siteStat.Customer_Access_View_All_Sites ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        private void FillCustomerAccess(ComboBox view)
        {
            try
            {
                //Fill the Sites
                view.DataSource = null;

                IEnumerable<SiteCustomerAccessResult> lstCustAccess =
                    instance.GetAllSiteCustomerAccess(
                    lstCustomerAccess.SelectedItem != null ? (lstCustomerAccess.SelectedItem as SiteCustomerAccessResult).Customer_Access_ID
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
        /// Exit the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Load initial settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUserSiteAccess_Load(object sender, EventArgs e)
        {
            LoadAccess();
        }

        /// <summary>
        /// function used to load the settings initially
        /// </summary>
        private void LoadAccess()
        {
            instance = UserSiteAccess.CreateInstance();

            //Fill customer access
            FillCustomerAccess(lstCustomerAccess);

            //Fill companies
            FillTreeViewCompanies(tvCompany);
            //Fill operators
            FillTreeViewDepots(tvDepots);
            // Fill sites
            FillTreeViewSites(tvSite);
        }

        /// <summary>
        /// Creates a new group for customer access
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewGroup_Click(object sender, EventArgs e)
        {
            frmNewCustomeraccessGroup frmAccess = new frmNewCustomeraccessGroup();
            frmAccess.ShowDialog();

            FillCustomerAccess(lstCustomerAccess);
        }


        /// <summary>
        /// Function to select all nodes in treeview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectAll(TreeNode eNode)
        {
            if (eNode.Checked)
            {
                foreach (TreeNode node in eNode.Nodes)
                {
                    node.Checked = true;
                }
            }
            else if (!eNode.Checked)
            {
                foreach (TreeNode node in eNode.Nodes)
                {
                    node.Checked = false;
                }
            }
        }


        /// <summary>
        /// On Selecting the root node all child nodes should be selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvCompany_NodeMouseClick(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
        {
            SelectAll(e.Node);
        }

        /// <summary>
        /// On Selecting the root node all child nodes should be selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvDepots_NodeMouseClick(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
        {
            SelectAll(e.Node);
        }

        /// <summary>
        /// On Selecting the root node all child nodes should be selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvSite_NodeMouseClick(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
        {
            SelectAll(e.Node);
        }

        /// <summary>
        /// Enable and Check the Sites treeview only if the Select All Sites checkbox is checked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void chkSelectAllSites_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAllSites.Checked)
            {
                tvSite.Nodes[0].Checked = true;
                foreach (TreeNode node in tvSite.Nodes[0].Nodes)
                    node.Checked = true;
            }
        }

        /// <summary>
        /// Enable the Depots treeview only if the ViewDepots checkbox is checked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkViewDepots_CheckedChanged(object sender, EventArgs e)
        {
            tvDepots.Enabled = ChkViewDepots.Checked ? true : false;
        }

        /// <summary>
        /// Enable the companies treeview only if the ViewCompany checkbox is checked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkViewAllCompanies_CheckedChanged(object sender, EventArgs e)
        {
            tvCompany.Enabled = chkViewAllCompanies.Checked ? true : false;
        }


        /// <summary>
        /// Update the customer access table with the selected access for companies, depots and sites.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (lstCustomerAccess.SelectedIndex < 0) return;

            //Update status for subcompanies
            foreach (TreeNode nodes in tvCompany.Nodes[0].Nodes)
            {
                instance.UpdateCustomerAccessForCompanies((lstCustomerAccess.SelectedItem as SiteCustomerAccessResult).Customer_Access_ID,
                     nodes.Tag.ToString(), chkViewAllCompanies.Checked);
            }

            //Update status for depots
            foreach (TreeNode nodes in tvDepots.Nodes[0].Nodes)
            {
                instance.UpdateCustomerAccessForDepots((lstCustomerAccess.SelectedItem as SiteCustomerAccessResult).Customer_Access_ID,
                     nodes.Tag.ToString(), ChkViewDepots.Checked);
            }

            //Update status for Sites

            if (tvSite.Nodes[0].Nodes.Count > 0)
            {
                foreach (TreeNode nodes in tvSite.Nodes[0].Nodes)
                {
                    instance.UpdateCustomerAccessForSites((lstCustomerAccess.SelectedItem as SiteCustomerAccessResult).Customer_Access_ID, 2,
                        nodes.Tag.ToString(), nodes.Checked, string.Empty);
                }
            }
            else
            {
                instance.UpdateCustomerAccessForSites((lstCustomerAccess.SelectedItem as SiteCustomerAccessResult).Customer_Access_ID, 2,
                                           tvSite.Nodes[0].Tag.ToString(), tvSite.Nodes[0].Checked, string.Empty);
                this.ShowInfoMessageBox("Customer Access updated Successfully");
            }
        }

        private void lstCustomerAccess_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAccess();
        }

    }
}
