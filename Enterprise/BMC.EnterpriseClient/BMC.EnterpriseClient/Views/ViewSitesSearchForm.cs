using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.CoreLib.Win32;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseClient.Helpers;
using BMC.Common.ExceptionManagement;
using BMC.Common;
namespace BMC.EnterpriseClient.Views
{
    public partial class ViewSitesSearchForm : Form
    {
        private ViewSiteSearchEntity _search = null;

        public ViewSitesSearchForm(ViewSiteSearchEntity search)
        {
            _search = search;
            InitializeComponent();
            // Set Tags for controls
            SetTagProperty();
        }
        private void SetTagProperty()
        {
            try
            {
                this.lblArea.Tag = "Key_AreaColon";
                this.lblCompany.Tag = "Key_CompanyColon";
                this.lblDepot.Tag = "Key_DepotColon";
                this.lblDistrict.Tag = "Key_DistrictColon";
                this.label12.Tag = "Key_ExcludeVacantSites";
                this.lblMCType.Tag = "Key_MCTypeColon";
                this.lblMachineModel.Tag = "Key_MachineModelColon";
                this.lblManufactuer.Tag = "Key_ManufacturerColon";
                this.lblOperator.Tag = "Key_OperatorColon";
                this.lblPercPayout.Tag = "Key_PercPayoutColon";
                this.lblRegion.Tag = "Key_RegionColon";
                this.btnReset.Tag = "Key_Reset";
                this.Tag = "Key_SearchText";
                this.btnSearch.Tag = "Key_SearchCaption";
                this.lblSiteRep.Tag = "Key_SiteRepColon";
                this.lblSiteSearch.Tag = "Key_SiteSearchColon";
                this.lblSubCompany.Tag = "Key_SubCompanyColon";
                this.lblPercPayoutTo.Tag = "Key_ToColon";

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void SaveComboBoxes(bool resetValues)
        {
            ModuleProc PROC = new ModuleProc("", "HookComboBoxes");

            try
            {
                foreach (Control ctl in tblContent.Controls)
                {
                    ComboBox cbo = ctl as ComboBox;
                    if (cbo != null && cbo.DropDownStyle == ComboBoxStyle.DropDownList)
                    {
                        string name = cbo.Name;
                        string text = cbo.Text;
                        if (resetValues &&
                            cbo.Items.Count > 0)
                        {
                            cbo.SelectedIndex = 0;
                            text = cbo.Items[0].ToString();
                        }
                        RdcHQ.SaveSetting("FormConfig", "ViewSiteSearchForm_" + name, text);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void ViewSitesSearchForm_Load(object sender, EventArgs e)
        {
            this.LoadForm(true);
            this.ResolveResources();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.LoadForm(false);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("ViewSitesSearchForm", "btnSearch_Click");

            try
            {
                _search.isFilter = true;
                ViewSiteHelper.SaveSearchComboText(cboSiteSearch, "ViewSiteSearchForm_SearchText");
                _search.CompanyID = null;
                _search.Sub_CompanyID = null;
                _search.Sub_Company_Region_ID = null;
                _search.Sub_Company_Area_ID = null;
                _search.Sub_Company_District_ID = null;
                _search.Operator_ID = null;
                _search.Depot_ID = null;
                _search.Machine_Type_ID = null;
                _search.Manufacturer_ID = null;
                _search.SiteRepId = null;
                _search.ExcludeVacant = null;
                _search.ModelSearch = null;
                _search.PayoutPercentageFrom = null;
                _search.PayoutPercentageTo = null;

                // search text
                _search.SearchText = cboSiteSearch.Text;

                // company
                int companyId = ((VSCompanyEntity)cboCompany.SelectedItem).Company_ID;
                if (companyId != -1) _search.CompanyID = companyId;

                // sub company
                int subCompanyId = ((VSSubCompanyEntity)cboSubCompany.SelectedItem).Sub_Company_ID;
                if (subCompanyId != -1) _search.Sub_CompanyID = subCompanyId;

                // region
                int regionId = ((VSSubCompanyRegionEntity)cboRegion.SelectedItem).Sub_Company_Region_ID;
                if (regionId != -1) _search.Sub_Company_Region_ID = regionId;

                // area
                int areaId = ((VSSubCompanyAreaEntity)cboArea.SelectedItem).Sub_Company_Area_ID;
                if (areaId != -1) _search.Sub_Company_Area_ID = areaId;

                // district
                int districtId = ((VSSubCompanyDistrictEntity)cboDistrict.SelectedItem).Sub_Company_District_ID;
                if (districtId != -1) _search.Sub_Company_District_ID = districtId;

                // operator
                int operatorId = ((VSOperatorEntity)cboOperator.SelectedItem).Operator_ID;
                if (operatorId != -1) _search.Operator_ID = operatorId;

                // depot
                int depotId = ((VSDepotEntity)cboDepot.SelectedItem).Depot_ID;
                if (depotId != -1) _search.Depot_ID = depotId;

                // mcType
                int mcTypeId = ((VSMachineTypeEntity)cboMCType.SelectedItem).Machine_Type_ID;
                if (mcTypeId != -1) _search.Machine_Type_ID = mcTypeId;

                // site rep
                int siteRepId = ((VSStaffEntity)cboSiteRep.SelectedItem).Staff_ID;
                if (siteRepId != -1) _search.SiteRepId = siteRepId;

                // manufacturer
                int manufacturerId = ((VSManufacturerEntity)cboManufactuer.SelectedItem).Manufacturer_ID;
                if (manufacturerId != -1) _search.Manufacturer_ID = manufacturerId;

                // model search
                string modelSearch = txtMachineModel.Text.Trim();
                if (!modelSearch.IsEmpty())
                {
                    _search.ModelSearch = modelSearch;
                }

                // percentage payout from
                if (!txtPercPayoutFrom.Text.IsEmpty() &&
                    Microsoft.VisualBasic.Information.IsNumeric(txtPercPayoutFrom.Text))
                {
                    _search.PayoutPercentageFrom = TypeSystem.GetValueSingle(txtPercPayoutFrom.Text);
                }

                // percentage payout to
                if (!txtPercPayoutTo.Text.IsEmpty() &&
                    Microsoft.VisualBasic.Information.IsNumeric(txtPercPayoutTo.Text))
                {
                    _search.PayoutPercentageTo = TypeSystem.GetValueSingle(txtPercPayoutTo.Text);
                }

                // vacant
                _search.ExcludeVacant = chkExcludeVacant.Checked;                
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void LoadForm(bool isFormLoading)
        {
            ModuleProc PROC = new ModuleProc("ViewSitesSearchForm", "LoadForm");

            try
            {
                ViewSiteHelper.FillSearchCombo(cboSiteSearch, -1, "ViewSiteSearchForm_SearchText");
                cboCompany.FillCompanies();
                cboOperator.FillOperators();
                cboSiteRep.FillStaffs();
                cboMCType.FillMachineTypes();
                cboManufactuer.FillManufacturers();

                if (!isFormLoading)
                {
                    this.SaveComboBoxes(true);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void cboSiteSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSiteSearch.Tag != null) return;
        }

        private void cboSiteSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ViewSiteHelper.SaveSearchCombo(cboSiteSearch, "ViewSiteSearchForm_SearchText");
            }
        }

        private void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCompany.SelectedItem != null &&
                cboCompany.SelectedItem is VSCompanyEntity)
            {
                cboSubCompany.FillSubCompanies(((VSCompanyEntity)cboCompany.SelectedItem).Company_ID);
            }
            if (cboCompany.SelectedIndex == 0)
            {
                cboSubCompany.FillSubCompanies(0);
            }
        }

        private void cboSubCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.FillRegions();
        }

        private void FillRegions()
        {
            ModuleProc PROC = new ModuleProc("", "FillRegions");

            try
            {
                cboRegion.FillSubCompanyRegions(CompanyID, SubCompanyID);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void cboRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboRegion.SelectedItem != null &&
                cboRegion.SelectedItem is VSSubCompanyRegionEntity)
            {
                cboArea.FillSubCompanyAreas(((VSSubCompanyRegionEntity)cboRegion.SelectedItem).Sub_Company_Region_ID, CompanyID, SubCompanyID);
            }
            if (cboRegion.SelectedIndex == 0 && cboRegion.Items.Count>1)
            {
                cboArea.FillSubCompanyAreas(0, CompanyID, SubCompanyID);
            }
        }

        private void cboArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboArea.SelectedItem != null &&
                cboArea.SelectedItem is VSSubCompanyAreaEntity)
            {
                cboDistrict.FillSubCompanyDistricts(((VSSubCompanyAreaEntity)cboArea.SelectedItem).Sub_Company_Area_ID, RegionID, SubCompanyID, CompanyID);
            }
            if (cboArea.SelectedIndex == 0 && cboArea.Items.Count > 1)
            {
                cboDistrict.FillSubCompanyDistricts(0, RegionID, SubCompanyID, CompanyID);
            }
        }

        private void cboOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboOperator.SelectedItem != null &&
                cboOperator.SelectedItem is VSOperatorEntity)
            {
                cboDepot.FillDepots(((VSOperatorEntity)cboOperator.SelectedItem).Operator_ID);
            }
            if (cboOperator.SelectedIndex == 0 || cboOperator.SelectedItem == "--All--")
            {
                cboDepot.FillDepots(0);
            }
        }

        private void ViewSitesSearchForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.SaveComboBoxes(false); Commented the code to save search filter in registry
        }

        private void cboDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboDepot_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboMCType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboSiteRep_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboManufactuer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtMachineModel_TextChanged(object sender, EventArgs e)
        {

        }

        #region Properties

        private int CompanyID
        {
            get
            {
                VSCompanyEntity company = cboCompany.SelectedItem as VSCompanyEntity;
                if (company != null && company.Company_ID != -1) 
                    return company.Company_ID;

                return 0;
            }
        }

        private int SubCompanyID
        {
            get
            {
                VSSubCompanyEntity subCompany = cboSubCompany.SelectedItem as VSSubCompanyEntity;
                if (subCompany != null && subCompany.Sub_Company_ID != -1)
                    return subCompany.Sub_Company_ID;

                return 0;
            }
        }

        private int RegionID
        {
            get
            {
                if (cboRegion.SelectedItem != null && cboRegion.SelectedItem is VSSubCompanyRegionEntity)
                    return ((VSSubCompanyRegionEntity)cboRegion.SelectedItem).Sub_Company_Region_ID;

                return 0;
            }
        }

        #endregion //Properties
    }
}
