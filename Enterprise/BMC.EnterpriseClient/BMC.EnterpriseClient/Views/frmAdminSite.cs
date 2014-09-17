using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using Details1;
//using Details2;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseClient.Helpers;
using BMC.Common.ExceptionManagement;
using BMC.Common;


namespace BMC.EnterpriseClient.Views
{
    public partial class frmAdminSite : Form
    {
        private int _siteID = 0;
        //private bool bLoading;
        //private bool Sitestatus;
        //Details1UC Detail1 = new Details1UC();
        //Details2UC Detail2 = new Details2UC();
        SiteDetails _siteDetails = new SiteDetails();
        //string strSiteName;
        private AdminSiteEntity _entity = null;
        private IAdminSite[] _childTasks = null;

        public frmAdminSite(int siteID)
        {
            _siteID = siteID;
            InitializeComponent();
            tbDetails1.Controls.Add(details1UC1);
            tbDetails2.Controls.Add(details2UC1);
            FormEvent += new FnCallDetail1(ServiceDepotDetail2);
            details1UC1.DlgDetail1UC = FormEvent;                     

           // ShowMe(TheSiteID, bISNewSite);  ////Call to be from frmAdminSubCompany Site ID to be passed 
            _childTasks = new IAdminSite[] {
                details1UC1, details2UC1,ucComms1
           };

            // Set Tags for controls
            SetTagProperty(); 
        }

        private void SetTagProperty()
        {
            this.tbAFTSettings.Tag = "Key_AFTSettings";
            this.btnApply.Tag = "Key_Apply";
            this.tbAreas.Tag = "Key_Areas";
            this.btnCancel.Tag = "Key_Close";
            this.tbComms.Tag = "Key_Comms";
            this.tbDefaults.Tag = "Key_Defaults";
            this.tbDetails1.Tag = "Key_Details1";
            this.tbDetails2.Tag = "Key_Details2";
            this.tbImages.Tag = "Key_Images";
            this.tbMaintenance.Tag = "Key_Maintenance";
            this.tbNotes.Tag = "Key_Notes";
            this.tbOwner.Tag = "Key_Owner";
            this.Tag = "Key_SiteAdministrator";
            this.tbZonePos.Tag = "Key_ZonePos";
        }

        public delegate void FnCallDetail1(List<AdminSiteEntity> objCol);

        private event FnCallDetail1 FormEvent;

        public void ServiceDepotDetail2(List<AdminSiteEntity> objCol)
        {
            details2UC1.ServiceOperatorfromDetail1(objCol);
        }    
             
        

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (IAdminSite task in _childTasks)
                {
                    task.SaveDetails(_entity);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            // actual saving
            
            //bool bURL = false;
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

            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAdminSite_Load(object sender, EventArgs e)
        {
            // For externalization
            this.ResolveResources();

            this.LoadDatasToUserControls();           
        }

        private void LoadDatasToUserControls()
        {
            try
            {
                _entity = _siteDetails.GetDetailFields(_siteID);

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
    }
}
