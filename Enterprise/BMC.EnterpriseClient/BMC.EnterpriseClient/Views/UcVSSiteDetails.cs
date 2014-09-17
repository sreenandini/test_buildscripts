using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseClient.Helpers;
using BMC.Common.ExceptionManagement;
using System;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class UcVSSiteDetails : UserControl, IUserControl2
    {
        private ViewSitesBusiness _business = new ViewSitesBusiness();

        public UcVSSiteDetails(IViewSiteInfo viewSite)
        {
            this.ViewSite = viewSite;
            InitializeComponent();
            // Set Tags for controls
            SetTagProperty();
        }
        private void SetTagProperty()
        {
            try
            {
                this.lblAddress.Tag = "Key_AddressColon";
                this.lblCode.Tag = "Key_CodeColon";
                this.lblCompany.Tag = "Key_CompanyColon";
                this.lblArea.Tag = "Key_AreaColon";
                this.lblManager.Tag = "Key_ManagerColon";
                this.lblName.Tag = "Key_NameColon";
                this.lblPostcode.Tag = "Key_PostcodeColon";
                this.lblRef.Tag = "Key_RefColon";
                this.lblReg.Tag = "Key_RegColon";
                this.lblSiteRep.Tag = "Key_SiteRepColon";
                this.lblStart.Tag = "Key_StartColon";
                this.lblSubCompany.Tag = "Key_SubCompanyColon";
                this.lblTelephone.Tag = "Key_TelColon";
                this.lblDist.Tag = "Key_DistrictColon";

            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public IViewSiteInfo ViewSite { get; set; }

        #region IUserControl Members

        public bool IsControlInitialized { get; set; }

        public void LoadControl()
        {
            ModuleProc PROC = new ModuleProc("", "LoadControl");

            try
            {
                if (this.ViewSite.SelectedSite != null)
                {
                    VSSiteDetailsEntity siteDetails = _business.GetSiteDetails(this.ViewSite.SelectedSite.Site_ID);
                    if (siteDetails.Count > 0)
                    {
                        VSSiteDetailEntity detail = siteDetails[0];
                        txtName.Text = detail.Site_Name;
                        txtTradeType.Text = detail.Site_Trade_Type;
                        txtCode.Text = detail.Site_Code;
                        txtRef.Text = detail.Site_Reference;

                        StringBuilder sbAddress = new StringBuilder();
                        sbAddress.AppendLine(detail.Site_Address_1);
                        sbAddress.AppendLine(detail.Site_Address_2);
                        sbAddress.AppendLine(detail.Site_Address_3);
                        sbAddress.AppendLine(detail.Site_Address_4);
                        sbAddress.AppendLine(detail.Site_Address_5);
                        txtAddress.Text = sbAddress.ToString();

                        txtCompany.Text = detail.Company_Name;
                        txtSubCompany.Text = detail.Sub_Company_Name;
                        txtReg.Text = detail.Sub_Company_Region_Name;
                        txtArea.Text = detail.Sub_Company_Area_Name;
                        txtDist.Text = detail.Sub_Company_District_Name;
                        txtManager.Text = detail.Site_Manager;

                        txtPostcode.Text = detail.Site_Postcode;
                        txtTelephone.Text = detail.Site_Phone_No;
                        txtSiteRep.Text = detail.Staff_First_Name + " " + detail.Staff_Last_Name;
                        if (!detail.Site_Start_Date.IsEmpty())
                        {
                            txtStart.Text = this.ViewSite.AdminUtilities.GetRegionalDate(detail.Site_Start_Date);
                        }
                        this.ViewSite.FormCaption = detail.DisplayName;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public bool SaveControl()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IUserControl2 Members

        public void ClearControl()
        {
            ModuleProc PROC = new ModuleProc("", "ClearControl");

            try
            {
                txtName.Text = string.Empty;
                txtTradeType.Text = string.Empty;
                txtCode.Text = string.Empty;
                txtRef.Text = string.Empty;
                txtAddress.Text = string.Empty;

                txtCompany.Text = string.Empty;
                txtSubCompany.Text = string.Empty;
                txtReg.Text = string.Empty;
                txtArea.Text = string.Empty;
                txtDist.Text = string.Empty;
                txtManager.Text = string.Empty;

                txtPostcode.Text = string.Empty;
                txtTelephone.Text = string.Empty;
                txtSiteRep.Text = string.Empty;
                txtStart.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #endregion

        private void UcVSSiteDetails_Load(object sender, EventArgs e)
        {
            try
            {
                this.ResolveResources();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    }
}
