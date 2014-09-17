using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.Common.ExceptionManagement;
using BMC.CoreLib.Win32;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseBusiness.Business;
using System.Reflection;
using System.Xml.Serialization;
using System.Xml;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public delegate void RefreshOnSaveSite(int siteID);

    public partial class ucAdminSite : UserControl, IControlActivator
    {
        
        private int _siteID = 0;
        private int _subCompanyID = 0;
        private int _companyID = 0;
        //private bool _loading = false;
        //private bool _sitestatus = false;       
        private SiteDetails _siteDetails = new SiteDetails();
        private string _strSiteName = string.Empty;

        private AdminSiteEntity _entity = null;
        private AdminSiteEntity _cloneEntity = null;
        private IAdminSite[] _childTasks = null;
        private bool displayDetails2Tab = false;
        private bool bDisplayGameCappingTab = false;
        public delegate void FnCallDetail1(List<AdminSiteEntity> objCol);

        //public delegate void RefreshOnSave(int siteID);
        public RefreshOnSaveSite NotifyChanges;

        private event FnCallDetail1 FormEvent;

        private Dictionary<string, string> _dTabDetails = new Dictionary<string,string>();
        
        public ucAdminSite(int siteID)
        {
            try
            {
                _siteID = siteID;
                InitializeComponent();
                ucDefaults21.Site_ID = _siteID;
                ucDefaults21.LoadDefaults();
                tbDetails1.Controls.Add(details1UC1);
                FormEvent += new FnCallDetail1(ServiceDepotDetail2);
                details1UC1.DlgDetail1UC = FormEvent;
                SiteDetails sdobj = new SiteDetails();
                string displayTab = string.Empty;
                string sGameCappingTab = string.Empty;
                bool IsAFTEnabledForSite;

                sdobj.GetSetting(null, "DisplayDetails2Tab", "TRUE", ref displayTab);
                sdobj.GetSetting(null, "IsGameCappingEnabled", "TRUE", ref sGameCappingTab);
                displayDetails2Tab = displayTab.Trim().ToUpper().Equals("TRUE");
                bDisplayGameCappingTab = sGameCappingTab.Trim().ToUpper().Equals("TRUE");
                IsAFTEnabledForSite = TypeSystem.GetValueBoolSimple(AdminBusiness.GetSetting("IsAFTEnabledForSite", "false"));

                SetTagProperty();

                //SSTab1.TabPages.Remove(tbDefaults);
                SSTab1.TabPages.Remove(tbImages);
                SSTab1.TabPages.Remove(tbNotes);
                SSTab1.TabPages.Remove(tabGameCapping);
                SSTab1.TabPages.Remove(tbMaintenance);

                if (bDisplayGameCappingTab || IsAFTEnabledForSite)
                {
                    if (!bDisplayGameCappingTab)
                    {
                        grpGameCapping.Visible = false;
                        tblAftAndGameCappingSetting.ColumnStyles[0].Width = 100;
                        tblAftContainer.ColumnStyles[0].Width = 40;
                        tblAftContainer.ColumnStyles[1].Width = 60;
                        tblAftAndGameCappingSetting.ColumnStyles[1].Width = 0;
                    }

                    //Start validating for aftsetting enable for site
                    if (!IsAFTEnabledForSite)
                    {
                        grpAftSetting.Visible = false;
                        tblAftAndGameCappingSetting.ColumnStyles[0].Width = 0;
                        tblAftAndGameCappingSetting.ColumnStyles[1].Width = 100;
                    }
                    //End validating for aftsetting enable for site
                }
                else
                {
                    SSTab1.TabPages.Remove(tbAFTSettings);
                }

                if (!displayDetails2Tab)
                {
                    grpDetails2.Visible = false;
                    tblDetailsAndOwnerMainFram.RowStyles[0].Height = 0;
                    //SSTab1.TabPages.Remove(tbDetails2);
                }

                _childTasks = new IAdminSite[] {
                    details1UC1,details2UC1, ucowner1,areas1,zonePosition1,  ucComms1, ucnotes1, ucimages1,ucnotes1, ucSiteMaintenance1,ucAftsettings1,ucGameCapping1};

                //Add to dictionary with tab details for newly added UCControls
                _dTabDetails.Add(details1UC1.ToString(), tbDetails1.Name);
                _dTabDetails.Add(details2UC1.ToString(), tbDetails2.Name);
                _dTabDetails.Add(ucowner1.ToString(), tbDetails2.Name);
                _dTabDetails.Add(ucnotes1.ToString(), tbOwner.Name);
                _dTabDetails.Add(ucimages1.ToString(), tbOwner.Name);
                _dTabDetails.Add(areas1.ToString(), tbAreas.Name);
                _dTabDetails.Add(zonePosition1.ToString(), tbZonePos.Name);
                _dTabDetails.Add(ucComms1.ToString(), tbComms.Name);
                _dTabDetails.Add(ucSiteMaintenance1.ToString(), tbComms.Name);
                _dTabDetails.Add(ucAftsettings1.ToString(), tbAFTSettings.Name);
                _dTabDetails.Add(ucGameCapping1.ToString(), tbAFTSettings.Name);

                this.Visible = false;
                this.Dock = DockStyle.Fill;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public ucAdminSite(OrganisationDetailsEntity _OrganisationDetailsEntity)
        {
            try
            {
                _siteID = 0;
                InitializeComponent();
                tbDetails1.Controls.Add(details1UC1);
                FormEvent += new FnCallDetail1(ServiceDepotDetail2);
                details1UC1.DlgDetail1UC = FormEvent;
                SiteDetails sdobj = new SiteDetails();
                string displayTab = string.Empty;
                string sGameCappingTab = string.Empty;                
                sdobj.GetSetting(null, "IsGameCappingEnabled", "TRUE", ref sGameCappingTab);
                sdobj.GetSetting(null, "DisplayDetails2Tab", "TRUE", ref displayTab);
                displayDetails2Tab = displayTab.Trim().ToUpper().Equals("TRUE");
                bDisplayGameCappingTab = sGameCappingTab.Trim().ToUpper().Equals("TRUE");
                
                //SSTab1.TabPages.Remove(tbDefaults);
                SSTab1.TabPages.Remove(tbImages);
                SSTab1.TabPages.Remove(tbNotes);
                SSTab1.TabPages.Remove(tabGameCapping);
                SSTab1.TabPages.Remove(tbMaintenance);

                SetTagProperty();

                if (!bDisplayGameCappingTab)
                {
                    grpGameCapping.Visible = false;
                    tblAftAndGameCappingSetting.ColumnStyles[0].Width = 100;
                    tblAftContainer.ColumnStyles[0].Width = 40;
                    tblAftContainer.ColumnStyles[1].Width = 60;
                    tblAftAndGameCappingSetting.ColumnStyles[1].Width = 0;
                }

                if (!displayDetails2Tab)
                {
                    tblDetailsAndOwnerMainFram.RowStyles[0].Height = 0;
                }

                _childTasks = new IAdminSite[] {
                    details1UC1,details2UC1, ucowner1,areas1,zonePosition1,  ucComms1, ucnotes1, ucimages1,ucnotes1, ucSiteMaintenance1,ucAftsettings1,ucGameCapping1};

                //Add to dictionary with tab details for newly added UCControls
                _dTabDetails.Add(details1UC1.ToString(), tbDetails1.Name);
                _dTabDetails.Add(details2UC1.ToString(), tbDetails2.Name);
                _dTabDetails.Add(ucowner1.ToString(), tbDetails2.Name);
                _dTabDetails.Add(ucnotes1.ToString(), tbOwner.Name);
                _dTabDetails.Add(ucimages1.ToString(), tbOwner.Name);
                _dTabDetails.Add(areas1.ToString(), tbAreas.Name);
                _dTabDetails.Add(zonePosition1.ToString(), tbZonePos.Name);
                _dTabDetails.Add(ucComms1.ToString(), tbComms.Name);
                _dTabDetails.Add(ucSiteMaintenance1.ToString(), tbComms.Name);
                _dTabDetails.Add(ucAftsettings1.ToString(), tbAFTSettings.Name);
                _dTabDetails.Add(ucGameCapping1.ToString(), tbAFTSettings.Name);

                this.Visible = false;
                this.Dock = DockStyle.Fill;

                _subCompanyID = Convert.ToInt32(_OrganisationDetailsEntity.Sub_Company_ID);
                _companyID = _OrganisationDetailsEntity.Company_ID;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void LoadRegionsTab(int Subcompany)
        {
            SSTab1.SelectedTab = SSTab1.TabPages[4];
        }

        public void ServiceDepotDetail2(List<AdminSiteEntity> objCol)
        {
            try
            {
                details2UC1.ServiceOperatorfromDetail1(objCol);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                _cloneEntity = new AdminSiteEntity();
                CloneEntity(_entity, _cloneEntity);
                //XmlSerializer x = new XmlSerializer(_entity.GetType());;
                //XmlWriter xml = new XmlWriter(
                //x.Serialize(new XmlWriter(), _entity);
                for(int counter = 0; counter < _childTasks.Length; counter++)                
                {
                    if (!_childTasks[counter].SaveDetails(_cloneEntity))
                    {
                        SSTab1.SelectedTab = SSTab1.TabPages[_dTabDetails[_childTasks[counter].ToString()]];
                        return;
                    }
                }

                if (_siteID != 0)
                {
                    if (!details1UC1.InsertEH_CrossTicketing())
                    {
                        return;
                    }
                }

                if (_entity.Site_Closed == 1 && _cloneEntity.Site_Closed == 0)
                {
                    _cloneEntity.Site_Closed = 0;
                    _cloneEntity.Site_Closed_Date = null;
                    _cloneEntity.Site_Status_ID = 0;
                    _cloneEntity.Site_Inactive_Date = null;
                }
                
                _siteDetails.InsertOrUpdatSite(_cloneEntity);                
                _cloneEntity.Depot_ID = (_cloneEntity.Depot_ID == 0) ? null : _cloneEntity.Depot_ID;
                _siteDetails.AuditData(_cloneEntity, _entity, AppEntryPoint.Current.UserId, AppEntryPoint.Current.UserName, _cloneEntity.Site_Name);
                if (_cloneEntity.bSiteDetailsUpdated)
                {
                    if (Win32Extensions.ShowQuestionMessageBox(this,this.GetResourceTextByKey(1, "MSG_UCADMINSITE_CHANGEDEPOTFORSITE") + _entity.Site_Name + this.GetResourceTextByKey(1, "MSG_UCADMINSITE_UPDATEDEPOTFORSITE"), this.ParentForm.Text) == DialogResult.Yes)
                    {
                        _siteDetails.UpdateDepotIDForBarPosition(_cloneEntity.Depot_ID, _cloneEntity.Site_ID);
                    }
                }
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_UCADMINSITE_SITEUPDATESUCCESSFULL"), this.ParentForm.Text);
                this._siteID = _cloneEntity.Site_ID;
                LoadDatasToUserControls();
                //Notify Change to main screen 
                if (NotifyChanges != null)
                {
                    NotifyChanges(_cloneEntity.Site_ID);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        //******************************Code to be updated***************************///////////////////////

        //bool bURL = false; // bURL value will be set at by checking txtDirLocalInbox.Text  
        //bool Detail1ValidStatus = false;
        //if (Detail1ValidStatus == false)
        //{
        //    SSTab1.SelectedTab = tbDetails1;
        //    Detail1ValidStatus = details1UC1.Detail1Apply_Click();
        //}
        //// bURL value will be set at by checking txtDirLocalInbox.Text 
        //if (Win32Extensions.ShowQuestionMessageBox(this, "Are you sure you want to apply these settings?") == System.Windows.Forms.DialogResult.Yes)
        //{
        //    // bURL value will be set at by checking txtDirLocalInbox.Text                 
        //    if (bURL == true)
        //    {
        //        if (details1UC1.SiteClosed == true)
        //        {
        //            AdminSiteEntity objActiveInstallationForSite = null;
        //            objActiveInstallationForSite = _siteDetails.GetActiveInstallationsForSite(1); //SiteID to be Passed
        //            if (objActiveInstallationForSite != null)
        //            {
        //                Win32Extensions.ShowErrorMessageBox("Please close all active installations before closing the site.");
        //                details1UC1.SiteClosed = false;
        //                return;
        //            }
        //            else
        //            {
        //                if (Win32Extensions.ShowQuestionMessageBox(this, "Are you sure you want to close the site?") == System.Windows.Forms.DialogResult.Yes)
        //                {
        //                    //Call ApplyChanges
        //                    Win32Extensions.ShowInfoMessageBox(this, "Site details updated successfully.");
        //                    //btnCancel_Click
        //                    return;
        //                }
        //                else
        //                {
        //                    return;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            //Call ApplyChanges
        //            //Call ApplyAftChanges
        //            Win32Extensions.ShowInfoMessageBox(this, "Site details updated successfully.");
        //            //btnCancel_Click
        //        }
        //    }



        private void LoadDatasToUserControls()
        {
            try
            {
                if (_siteID != 0)
                {
                    _entity = _siteDetails.GetDetailFields(_siteID);
                    if (!AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Site_Edit"))
                    {
                        btnApply.Enabled = false;
                        this.tblMainFram.RowStyles[0].Height = 30;
                        lblErrorMessage.Text = AppGlobals.Current.UserName + " don't have Permission to edit Site";
                    }
                    else
                    {
                        lblErrorMessage.Visible = false;
                        this.tblMainFram.RowStyles[0].Height = 0;
                    }
                }
                else
                {
                    lblErrorMessage.Visible = false;
                    this.tblMainFram.RowStyles[0].Height = 0;
                    _entity = new AdminSiteEntity();
                    _entity.Sub_Company_ID = _subCompanyID;
                    _entity.Company_ID = _companyID;
                }

                foreach (IAdminSite task in _childTasks)
                {
                    task.LoadDetails(_entity);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void ucAdminSite_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadDatasToUserControls();
				this.ResolveResources();
                grpDetails2.Text = "";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void details2UC1_Load(object sender, EventArgs e)
        {

        }

        private void CloneEntity(AdminSiteEntity entity, AdminSiteEntity cloneEntity)
        {
            try
            {
                foreach (PropertyInfo prop in entity.GetType().GetProperties())
                {
                    prop.SetValue(cloneEntity, prop.GetValue(entity, null), null);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        #region IControlActivator Members

        void IControlActivator.ActivateControl(object input)
        {
            ModuleProc PROC = new ModuleProc("ucAdminSite", "ActivateControl");

            try
            {
                OrganisationInput org = input as OrganisationInput;
                if (org != null)
                {
                    SSTab1.SelectedTab = (org.BarPositionId > 0 ? tbZonePos : tbDetails1);
                    BMC.EnterpriseClient.Helpers.GlobalHelper.ActivateControl(zonePosition1, input);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #endregion

        public void SetTagProperty()
        {
            this.tbAFTSettings.Tag = "Key_Others";
            this.btnApply.Tag = "Key_Apply";
            this.tbAreas.Tag = "Key_Areas";
            this.tbComms.Tag = "Key_SiteConfigration";
            this.tbDefaults.Tag = "Key_Defaults";
            this.tbDetails1.Tag = "Key_Details1";
            this.tbDetails2.Tag = "Key_Details2";
            //this.tabGameCapping.Tag = "Key_GameCapping";
            //this.tbImages.Tag = "Key_Images";
            //this.tbMaintenance.Tag = "Key_Maintenance";
            this.tbOwner.Tag = "Key_Details3";
            this.tbZonePos.Tag = "Key_ZonePos";
            //this.tbNotes.Tag = "Key_Notes";

            this.grpOwner.Tag = "Key_Owner";
            this.grpImage.Tag = "Key_Images";
            this.grpNotes.Tag = "Key_Notes";
            this.grpComms.Tag = "Key_Comms";
            this.grpMaintenanace.Tag = "Key_Maintenance";
            this.grpAftSetting.Tag = "Key_AFTSettings";
            this.grpGameCapping.Tag = "Key_GameCapping";
            this.grpDetails2.Tag = "Key_Details2";

            this.btnClose.Tag = "Key_Close";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                ((Panel)this.Parent).Controls.Remove(this);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    }
}
