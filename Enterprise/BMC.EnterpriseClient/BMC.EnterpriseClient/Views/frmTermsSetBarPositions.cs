using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseBusiness.Business;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common;
using BMC.CoreLib.Win32;

namespace BMC.EnterpriseClient.Views
{
    #region Class
    public partial class frmTermsSetBarPositions : Form
    {
        #region Private Members
        private TermsSetBarPositionBusiness _termsSetBarPositionBusiness = null;
        private List<CompanyInfo> _lstCompanyInfo = null;
        private List<SubCompanyInfo> _lstSubCompanyInfo = null;
        private List<SiteInfo> _lstSiteInfo = null;
        private bool validationFlag = false;
        private DateTime preDateTime;
        private DateTime postDateTime;
        private int companyID = 0;
        private int subCompanyID = 0;
        private int siteID = 0;
        #endregion Private Members

        #region Constructor
        public frmTermsSetBarPositions()
        {
            InitializeComponent();
            SetTagProperty();
            _termsSetBarPositionBusiness = TermsSetBarPositionBusiness.CreateInstance();
        }
        #endregion Constructor

        #region Events
        private void frmSetBarPositionsTermsValidationPatch_Load(object sender, EventArgs e)
        {
            int _companyID = 0;
            int _subCompanyID = 0;

            try
            {
                this.ResolveResources();
                LoadCompanyName();
                _companyID = cmbCompany.SelectedValue != null ? Convert.ToInt32(cmbCompany.SelectedValue) : 0;

                LoadSubCompanyName(_companyID);
                _subCompanyID = cmbSubCompany.SelectedValue != null ? Convert.ToInt32(cmbSubCompany.SelectedValue) : 0;

                LoadSiteName(_subCompanyID);

                rdoTrue.Checked = true;
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
                LogManager.WriteLog("Inside btnApply_Click...", LogManager.enumLogLevel.Info);

                validationFlag = rdoTrue.Checked == true ? true : false;
                preDateTime = dpPreDates.Value;
                postDateTime = dpPostDates.Value;
                siteID = Convert.ToInt32(cmbSite.SelectedValue.ToString());
                companyID = Convert.ToInt32(cmbCompany.SelectedValue.ToString());
                subCompanyID = Convert.ToInt32(cmbSubCompany.SelectedValue.ToString());

                pbSetBarPositions.Value = 1;
                pbSetBarPositions.Maximum = 10000;
                pbSetBarPositions.Step = 1;
                for (int i = 0; i < 10000; i++)
                {
                    pbSetBarPositions.PerformStep();
                }

                _termsSetBarPositionBusiness.UpdateTermsBarPosition(validationFlag, preDateTime, postDateTime, siteID, companyID, subCompanyID);



                
                Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_BAR_POS_DETAILS_UPDATE_SUCCESS"));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside cmbCompany_SelectedIndexChanged...", LogManager.enumLogLevel.Info);

                LoadSubCompanyName((int)cmbCompany.SelectedValue);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbSubCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside cmbSubCompany_SelectedIndexChanged...", LogManager.enumLogLevel.Info);

                LoadSiteName((int)cmbSubCompany.SelectedValue);
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }
        }
        #endregion Events

        #region Private Methods
        private void SetTagProperty()
        {
            this.btnClose.Tag = "Key_Close";
            this.btnApply.Tag = "Key_Apply";
            this.grpFlagControls.Tag = "Key_TermsValidationFlag";
            this.gpDateControls.Tag = "Key_TermsPreandPostDates";
            this.lblSubCompany.Tag = "Key_SubCompany";
            this.lblSite.Tag = "Key_Site";
            this.lblCompany.Tag = "Key_Company";
            this.Tag = "Key_BarPositionTermsValidationPatch";
            this.rdoTrue.Tag = "Key_True";
            this.RdoFalse.Tag = "Key_False";
        }


        private void LoadCompanyName()
        {
            try
            {
                LogManager.WriteLog("Inside LoadCompanyName...", LogManager.enumLogLevel.Info);

                _lstCompanyInfo = _termsSetBarPositionBusiness.GetAllCompanyNames();
                _lstCompanyInfo.Insert(0, new CompanyInfo { Company_Name = this.GetResourceTextByKey("Key_Any"), Company_ID = 0 });

                cmbCompany.DataSource = _lstCompanyInfo;
                cmbCompany.DisplayMember = "Company_Name";
                cmbCompany.ValueMember = "Company_ID";
                cmbCompany.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadSubCompanyName(int companyID)
        {
            try
            {
                LogManager.WriteLog("Inside LoadSubCompanyName...", LogManager.enumLogLevel.Info);

                _lstSubCompanyInfo = _termsSetBarPositionBusiness.GetAllSubCompanyNamesForCompanyID(companyID);
                _lstSubCompanyInfo.Insert(0, new SubCompanyInfo { Sub_Company_ID = 0, Sub_Company_Name = this.GetResourceTextByKey("Key_Any") });

                cmbSubCompany.DataSource = _lstSubCompanyInfo;
                cmbSubCompany.DisplayMember = "Sub_Company_Name";
                cmbSubCompany.ValueMember = "Sub_Company_ID";
                cmbSubCompany.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadSiteName(int subCompanyID)
        {
            try
            {
                LogManager.WriteLog("Inside LoadSiteName...", LogManager.enumLogLevel.Info);

                _lstSiteInfo = _termsSetBarPositionBusiness.GetAllSiteNamesForSubCompanyID(subCompanyID);
                _lstSiteInfo.Insert(0, new SiteInfo { Site_ID = 0, Site_Name = this.GetResourceTextByKey("Key_Any") });

                cmbSite.DataSource = _lstSiteInfo;
                cmbSite.DisplayMember = "Site_Name";
                cmbSite.ValueMember = "Site_ID";
                cmbSite.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion Private Methods
    }
    #endregion Class
}
