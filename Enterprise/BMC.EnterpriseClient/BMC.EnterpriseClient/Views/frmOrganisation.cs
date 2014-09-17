using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using System.Text.RegularExpressions;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using BMC.EnterpriseClient.Helpers;
using BMC.Common;
using BMC.CoreLib.Win32;
namespace BMC.EnterpriseClient.Views
{
    public partial class frmOrganisation : Form, IControlActivator
    {
        #region Private Variables
        OrganisationDetails _OrganisationDetails = new OrganisationDetails();
        enum TreeLevel
        {
            Company = 0,
            SubCompany,
            Site
        }
        OrganisationInput _input = null;
        #endregion

        public frmOrganisation()
        {
            InitializeComponent();
            SetTagProperty();
        }

        private void SetTagProperty()
        {
            this.Tag = "Key_FormTitle";

            this.newCompanyToolStripMenuItem.Text = this.GetResourceTextByKey("Key_NewCompany");
            this.newSubCompanyToolStripMenuItem.Text = this.GetResourceTextByKey("Key_NewCompany");
            this.newSiteToolStripMenuItem1.Text = this.GetResourceTextByKey("Key_NewSite");
            this.newSiteToolStripMenuItem.Text = this.GetResourceTextByKey("Key_NewSubCompany");

        }

        public frmOrganisation(OrganisationInput input)
        {
            _input = input;
            InitializeComponent();
            SetTagProperty();
        }

        private void frmOrganisation_Load(object sender, EventArgs e)
        {
            try
            {
                this.ResolveResources();

                trvOrganisation.AfterSelect += new TreeViewEventHandler(OnOrganisation_AfterSelect);
                CreateNodesinTreeView();

                if (!AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Company_New"))
                    cntMenuStrip2.Items[0].Enabled = false;

                if (!AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Company_New"))
                    cntMenuStrip1.Enabled = false;

                if (!AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Sub_New"))
                    cntMenuStrip2.Items[1].Enabled = false;

                if (!AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Site_New"))
                    cntMenuStrip3.Enabled = false;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void CreateNodesinTreeView()
        {
            CreateNodesinTreeView(0, 0, 0);
            if (trvOrganisation.Nodes.Count == 0)
            {
                RemoveContol();
                ucAdminCompany objucAdminCompany = new ucAdminCompany(0);
                objucAdminCompany.NotifyChanges = new RefreshOnSaveCompany(RefreshCompany);
                pnlContent.Controls.Add(objucAdminCompany);
                objucAdminCompany.Show();
            }
        }


        private void CreateNodesinTreeView(int companyID, int subCompanyID, int siteID)
        {
            try
            {
                if (!AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Company"))
                    return;
                Int32 iOldSubComp = 0, iOldCompany = 0, iOldSite = 0;
                TreeNode tnAllCompNode = new TreeNode();
                trvOrganisation.Nodes.Clear();

                List<OrganisationDetailsEntity> getSiteDetailsResults = _OrganisationDetails.GetOrganisationInfo(AppEntryPoint.Current.UserId);

                foreach (OrganisationDetailsEntity getSiteDetailsResult in getSiteDetailsResults)
                {
                    //Add company Node
                    if (iOldCompany != getSiteDetailsResult.Company_ID)
                    {
                        TreeNode tempnode = trvOrganisation.Nodes.Add("CO,#" + getSiteDetailsResult.Company_ID.ToString(), getSiteDetailsResult.Company_Name.ToString());
                        //trvOrganisation.Nodes["ALL"].Nodes.Add("CO,#" + getSiteDetailsResult.Company_ID.ToString(), getSiteDetailsResult.Company_Name.ToString());
                        trvOrganisation.Nodes["CO,#" + getSiteDetailsResult.Company_ID.ToString()].ExpandAll();
                        iOldCompany = getSiteDetailsResult.Company_ID;
                        tempnode.ContextMenuStrip = cntMenuStrip2;
                        tempnode.Tag = getSiteDetailsResult;
                    }
                    //Add subcompany if available in record
                    if (!AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Sub"))
                        continue;
                    if (getSiteDetailsResult.Sub_Company_ID != null && iOldSubComp != getSiteDetailsResult.Sub_Company_ID)
                    {
                        //find the parent where this node needs to be added
                        TreeNode tnNode = trvOrganisation.Nodes.Find("CO,#" + getSiteDetailsResult.Company_ID.ToString(), true)[0];
                        //if found add the node to parent
                        if (tnNode != null)
                        {
                            TreeNode tempnode = tnNode.Nodes.Add("SC,#" + getSiteDetailsResult.Sub_Company_ID.ToString(), getSiteDetailsResult.Sub_Company_Name.ToString());
                            tnNode.ExpandAll();
                            iOldSubComp = Convert.ToInt32(getSiteDetailsResult.Sub_Company_ID);
                            tempnode.ContextMenuStrip = cntMenuStrip3;
                            tempnode.Tag = getSiteDetailsResult;
                        }
                    }
                    if (!AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Site"))
                        continue;
                    //Add site to the respective nodes
                    if (getSiteDetailsResult.Site_ID != null && iOldSite != getSiteDetailsResult.Site_ID)
                    {
                        TreeNode tnNode;
                        if (!string.IsNullOrEmpty(getSiteDetailsResult.Sub_Company_ID.ToString()))
                        {
                            tnNode = trvOrganisation.Nodes.Find("SC,#" + getSiteDetailsResult.Sub_Company_ID.ToString(), true)[0];
                        }
                        else
                        {
                            tnNode = trvOrganisation.Nodes.Find("CO,#" + getSiteDetailsResult.Company_ID.ToString(), true)[0];
                        }
                        if (tnNode != null)
                        {
                            TreeNode tempnode = null;
                            if (string.IsNullOrEmpty(getSiteDetailsResult.SiteStatus))
                                tempnode = tnNode.Nodes.Add("SI,#" + getSiteDetailsResult.Site_ID.ToString(), getSiteDetailsResult.Site_Name + "[" + getSiteDetailsResult.Site_Code + "]");
                            else
                                tempnode = tnNode.Nodes.Add("SI,#" + getSiteDetailsResult.Site_ID.ToString(), getSiteDetailsResult.Site_Name + "[" + getSiteDetailsResult.Site_Code + "][" + getSiteDetailsResult.SiteStatus + "]");

                            tnNode = trvOrganisation.Nodes.Find("SI,#" + getSiteDetailsResult.Site_ID.ToString(), true)[0];
                            tnNode.ExpandAll();
                            tempnode.ContextMenuStrip = cntMenuStrip4;
                            tempnode.Tag = getSiteDetailsResult;//.Site_ID;
                        }
                    }
                    //FormatLicenseDetailsGrid();
                }
                trvOrganisation.ExpandAll();
                if (siteID != 0)
                {
                    TreeNode node = trvOrganisation.Nodes.Find("SI,#" + siteID.ToString(), true)[0];
                    trvOrganisation.SelectedNode = node;
                }
                else if (subCompanyID != 0)
                {
                    TreeNode node = trvOrganisation.Nodes.Find("SC,#" + subCompanyID.ToString(), true)[0];
                    trvOrganisation.SelectedNode = node;
                }
                else if (companyID != 0)
                {
                    TreeNode node = trvOrganisation.Nodes.Find("CO,#" + companyID.ToString(), true)[0];
                    trvOrganisation.SelectedNode = node;
                }


                //dtgvLicenseDetails.DataSource = null;
            }
            catch (Exception exCreateNodesinTreeView)
            {
                LogManager.WriteLog("Error in CreateNodesinTreeView method" + "-Error Message-" + exCreateNodesinTreeView.Message, LogManager.enumLogLevel.Error);
            }
            finally
            {
                if (_input != null)
                {
                    ((IControlActivator)this).ActivateControl(_input);
                }
            }
        }

        private void RemoveContol()
        {
            if (pnlContent.Controls.Count > 0)
            {
                pnlContent.Controls.RemoveAt(0);
            }
        }
        private void trvOrganisation_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ////
            //try
            //{
            //    if (e.Button == MouseButtons.Right)
            //    {
            //        trvOrganisation.SelectedNode = e.Node;
            //        return;
            //    }

            //    RemoveContol();
            //    if (e.Node.Level == (Int32)TreeLevel.Site)
            //    //if (e.Node.Name.Contains("SI,#"))
            //    {
            //        ucAdminSite ucAdminSites1 = null;
            //        ucAdminSites1 = new ucAdminSite(Convert.ToInt32(((OrganisationDetailsEntity)e.Node.Tag).Site_ID));
            //        ucAdminSites1.NotifyChanges = new RefreshOnSave(RefreshLeftNav);
            //        pnlContent.Controls.Add(ucAdminSites1);
            //        ucAdminSites1.Show();
            //    }
            //    if (e.Node.Level == (Int32)TreeLevel.Company)//if (e.Node.Name.Contains("CO,#"))
            //    {
            //        ucAdminCompany objucAdminCompany = new ucAdminCompany(Convert.ToInt32(((OrganisationDetailsEntity)e.Node.Tag).Company_ID));
            //        objucAdminCompany.NotifyChanges = new RefreshOnSave(RefreshLeftNav);
            //        pnlContent.Controls.Add(objucAdminCompany);
            //        objucAdminCompany.Show();
            //    }
            //    if (e.Node.Level == (Int32)TreeLevel.SubCompany)//if (e.Node.Name.Contains("SC,#"))
            //    {
            //        ucAdminSubCompany ucAdminSubCompany = new ucAdminSubCompany(Convert.ToInt32(((OrganisationDetailsEntity)e.Node.Tag).Sub_Company_ID));
            //        ucAdminSubCompany.NotifyChanges = new RefreshOnSave(RefreshLeftNav);
            //        pnlContent.Controls.Add(ucAdminSubCompany);
            //        ucAdminSubCompany.Show();
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    ExceptionManager.Publish(Ex);
            //}
        }

        void OpenAdminCompany(OrganisationDetailsEntity entity)
        {
            int CompanyID = 0;
            if (entity == null) return;
            CompanyID = entity.Company_ID;

            ucAdminCompany objucAdminCompany = new ucAdminCompany(CompanyID);
            objucAdminCompany.Entity = entity;
            objucAdminCompany.NotifyChanges = new RefreshOnSaveCompany(RefreshCompany);
            objucAdminCompany.OpenSubCompanyForm = new OnOpenExisitingSubCompanyHandler(CreateNewSubCompany);
            pnlContent.Controls.Add(objucAdminCompany);
            objucAdminCompany.Show();
        }
        void OpenAdminSite(int SiteID)
        {
            OrganisationInput org = new OrganisationInput();
            ucAdminSubCompany objucSubCompany = new ucAdminSubCompany(org.SubCompanyId);
            ucAdminSite ucAdminSites1 = new ucAdminSite(SiteID);
            ucAdminSites1.NotifyChanges = new RefreshOnSaveSite(RefreshSite);
            pnlContent.Controls.Add(ucAdminSites1);
            ucAdminSites1.Show();
        }
        void OpenAdminSite(OrganisationDetailsEntity entity)
        {
            // OrganisationInput org = new OrganisationInput();
            //ucAdminSubCompany objucSubCompany = new ucAdminSubCompany(org.SubCompanyId);
            ucAdminSite ucAdminSites1 = new ucAdminSite(entity);
            //objucSubCompany.OpenSiteForm = new OnOpenSiteHandler(CreateNewSite);
            ucAdminSites1.NotifyChanges = new RefreshOnSaveSite(RefreshSite);
            pnlContent.Controls.Add(ucAdminSites1);
            ucAdminSites1.Show();
        }
        void OpenAdminSubCompany(OrganisationDetailsEntity entity)
        {
            int SubCompanyID = entity.Sub_Company_ID.SafeValue();
            ucAdminSubCompany objucSubCompany = new ucAdminSubCompany(SubCompanyID);
            objucSubCompany.Entity = entity;
            objucSubCompany.NotifyChanges = new RefreshOnSaveSubCompany(RefreshSubCompany);
            objucSubCompany.OpenSiteForm = new OnOpenSiteHandler(CreateNewSite);
            pnlContent.Controls.Add(objucSubCompany);
            objucSubCompany.Show();
        }
        void OpenAdminSubCompanyNew(OrganisationDetailsEntity organisationDetailsEntity)
        {
            ucAdminSubCompany objucSubCompany = new ucAdminSubCompany(organisationDetailsEntity);
            objucSubCompany.NotifyChanges = new RefreshOnSaveSubCompany(RefreshSubCompany);
            objucSubCompany.OpenSiteForm = new OnOpenSiteHandler(CreateNewSite);
            pnlContent.Controls.Add(objucSubCompany);
            objucSubCompany.Show();
        }

        void OnOrganisation_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //
            try
            {
                if (e.Node == null) return;

                UxHeaderContent oHeaderCont = this.Controls[0] as UxHeaderContent;

                RemoveContol();
                if (e.Node.Level == (Int32)TreeLevel.Site)
                //if (e.Node.Name.Contains("SI,#"))
                {
                    oHeaderCont.Text = this.Text + " - [" + this.GetResourceTextByKey("Key_SiteAdministration") + "]";
                    ucAdminSite ucAdminSites1 = null;
                    ucAdminSites1 = new ucAdminSite(Convert.ToInt32(((OrganisationDetailsEntity)e.Node.Tag).Site_ID));
                    ucAdminSites1.NotifyChanges = new RefreshOnSaveSite(RefreshSite);
                    pnlContent.Controls.Add(ucAdminSites1);
                    ucAdminSites1.Show();
                }
                if (e.Node.Level == (Int32)TreeLevel.Company)//if (e.Node.Name.Contains("CO,#"))
                {
                    oHeaderCont.Text = this.Text + " - [" + this.GetResourceTextByKey("Key_CompanyAdministration") + "]";
                    this.OpenAdminCompany(e.Node.Tag as OrganisationDetailsEntity);
                    //ucAdminCompany objucAdminCompany = new ucAdminCompany(Convert.ToInt32(((OrganisationDetailsEntity)e.Node.Tag).Company_ID));
                    //objucAdminCompany.NotifyChanges = new RefreshOnSave(RefreshLeftNav);
                    //pnlContent.Controls.Add(objucAdminCompany);
                    //objucAdminCompany.Show();
                }
                if (e.Node.Level == (Int32)TreeLevel.SubCompany)//if (e.Node.Name.Contains("SC,#"))
                {
                    oHeaderCont.Text = this.Text + " - [" + this.GetResourceTextByKey("Key_SubCompanyAdministration") + "]";
                    this.OpenAdminSubCompany(e.Node.Tag as OrganisationDetailsEntity);
                    //ucAdminSubCompany ucAdminSubCompany = new ucAdminSubCompany(Convert.ToInt32(((OrganisationDetailsEntity)e.Node.Tag).Sub_Company_ID));
                    //ucAdminSubCompany.NotifyChanges = new RefreshOnSave(RefreshLeftNav);
                    //pnlContent.Controls.Add(ucAdminSubCompany);
                    //ucAdminSubCompany.Show();
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void RefreshCompany(int CompanyID)
        {
            CreateNodesinTreeView(CompanyID, 0, 0);
        }

        private void RefreshSubCompany(int SubCompanyID)
        {
            CreateNodesinTreeView(0, SubCompanyID, 0);
        }

        private void RefreshSite(int SiteID)
        {
            CreateNodesinTreeView(0, 0, SiteID);
        }

        private void newCompanyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                UxHeaderContent oHeaderCont = this.Controls[0] as UxHeaderContent;
                oHeaderCont.Text = this.Text + " - [" + this.GetResourceTextByKey("Key_CompanyAdministration") + "]";
                if (trvOrganisation.Nodes.Count != 0)
                {
                    RemoveContol();
                    ucAdminCompany objucAdminCompany = new ucAdminCompany(0);
                    objucAdminCompany.NotifyChanges = new RefreshOnSaveCompany(RefreshCompany);
                    pnlContent.Controls.Add(objucAdminCompany);
                    objucAdminCompany.Show();
                }

                ((GroupBox)pnlContent.Controls.Find("grpDetails", true)[0]).Visible = true;
                ((GroupBox)pnlContent.Controls.Find("grpInvoice", true)[0]).Visible = true;

                ((Button)pnlContent.Controls.Find("btnNewSubCompany", true)[0]).Enabled = false;
                ((Button)pnlContent.Controls.Find("btn_AddnewCompany", true)[0]).Enabled = false;
                ((Button)pnlContent.Controls.Find("btnDetailsSave", true)[0]).Enabled = true;
                ((Button)pnlContent.Controls.Find("btnDetailsClose", true)[0]).Enabled = true;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);

            }
        }

        private void newSiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                UxHeaderContent oHeaderCont = this.Controls[0] as UxHeaderContent;
                oHeaderCont.Text = this.Text + " - [" + this.GetResourceTextByKey("Key_SubCompanyAdministration") + "]";
                RemoveContol();
                ucAdminSubCompany ucAdminSubCompany = null;
                if (trvOrganisation.SelectedNode == null && trvOrganisation.Nodes.Count > 0)
                    ucAdminSubCompany = new ucAdminSubCompany((OrganisationDetailsEntity)trvOrganisation.Nodes[0].Tag);
                else
                    ucAdminSubCompany = new ucAdminSubCompany((OrganisationDetailsEntity)trvOrganisation.SelectedNode.Tag);
                ucAdminSubCompany.NotifyChanges = new RefreshOnSaveSubCompany(RefreshSubCompany);
                pnlContent.Controls.Add(ucAdminSubCompany);
                ucAdminSubCompany.Show();
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }
        }

        private void newSiteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                UxHeaderContent oHeaderCont = this.Controls[0] as UxHeaderContent;
                oHeaderCont.Text = this.Text + " - [" + this.GetResourceTextByKey("Key_SiteAdministration") + "]";
                RemoveContol();
                ucAdminSite ucAdminSites1 = null;
                ucAdminSites1 = new ucAdminSite((OrganisationDetailsEntity)trvOrganisation.SelectedNode.Tag);
                ucAdminSites1.NotifyChanges = new RefreshOnSaveSite(RefreshSite);
                pnlContent.Controls.Add(ucAdminSites1);
                ucAdminSites1.Show();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }
        void CreateNewSite(OrganisationDetailsEntity organisationDetailsEntit)
        {
            UxHeaderContent oHeaderCont = this.Controls[0] as UxHeaderContent;
            oHeaderCont.Text = this.Text + " - [" + this.GetResourceTextByKey("Key_SiteAdministration") + "]";
            RemoveContol();
            OpenAdminSite(organisationDetailsEntit);
        }

        //void CreateNewSubCompany(OrganisationDetailsEntity organisationDetailsEntity)
        //{
        //    RemoveContol();
        //    OpenAdminSubCompany(organisationDetailsEntity);
        //}

        void CreateNewSubCompany(OrganisationDetailsEntity organisationDetailsEntity)
        {
            UxHeaderContent oHeaderCont = this.Controls[0] as UxHeaderContent;
            oHeaderCont.Text = this.Text + " - [" + this.GetResourceTextByKey("Key_SubCompanyAdministration") + "]";
            RemoveContol();
            OpenAdminSubCompanyNew(organisationDetailsEntity);
        }

        #region IFormActivator Members

        public void ActivateControl(object input)
        {
            ModuleProc PROC = new ModuleProc("frmOrganisation", "ActivateControl");

            try
            {
                string nodeFind = string.Empty;
                OrganisationInput org = input as OrganisationInput;

                if (org != null)
                {
                    switch (org.InputType)
                    {
                        case OrganisationInputType.Position:
                        case OrganisationInputType.Site:
                            nodeFind = "SI,#" + org.SiteId.ToString();
                            break;

                        case OrganisationInputType.SubCompany:
                            nodeFind = "SC,#" + org.SubCompanyId.ToString();
                            break;

                        case OrganisationInputType.Company:
                            nodeFind = "CO,#" + org.CompanyId.ToString();
                            break;

                        default:
                            break;
                    }
                    TreeNode[] selectedNodes = trvOrganisation.Nodes.Find(nodeFind, true);
                    if (selectedNodes != null && selectedNodes.Length > 0)
                    {
                        trvOrganisation.SelectedNode = selectedNodes[0];
                        if (pnlContent.Controls.Count > 0)
                        {
                            GlobalHelper.ActivateControl(pnlContent.Controls[0], input);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #endregion

        private void trvOrganisation_MouseClick(object sender, MouseEventArgs e)
        {
            trvOrganisation.SelectedNode = null;
            trvOrganisation.SelectedNode = trvOrganisation.HitTest(e.X, e.Y).Node;

        }


    }

    public class OrganisationInput
    {
        public int CompanyId { get; set; }
        public int SubCompanyId { get; set; }
        public int SiteId { get; set; }
        public int InstallationId { get; set; }
        public int BarPositionId { get; set; }

        public OrganisationInputType InputType { get; set; }
    }

    public enum OrganisationInputType
    {
        Position = 0,
        Site = 1,
        SubCompany = 2,
        Company = 3
    }
}
