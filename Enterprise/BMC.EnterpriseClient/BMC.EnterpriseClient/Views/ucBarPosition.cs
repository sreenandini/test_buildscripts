
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness;
using Microsoft.VisualBasic;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseDataAccess;
using System.Text.RegularExpressions;
using BMC.CoreLib.Win32;
using System.IO;
using System.Data.SqlClient;
using System.Data.Linq;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.Utilities;
using BMC.Common;
using System.Linq;

namespace BMC.EnterpriseClient.Views
{
    public partial class ucBarPosition : UserControl
    {
        #region Members
        private int _SiteId = 0;
        private int _BarPositionID = 0;
        private string _BarPositionName = string.Empty;
        BZonePosition objBZonePos = null;
        CommonUtility objCU = null;
        public const bool gbUHEnable = false;
        public string title = string.Empty;
        private string imagePath = string.Empty;
        private string dateFormat = "dd MMM yyyy";
        BarPositionInfoEntity entity = null;
        string sZoneName = string.Empty;
        string sOperatorName = string.Empty;
        string sCategory = string.Empty;
        string sCollectionDay = string.Empty;
        string sMachineType = string.Empty;
        string sDepotName = string.Empty;
        int iUserId = 0;
        string sUserName = string.Empty;
        string _msgBoxTitle;
        private AdminSubCompany adminBusiness;
        private BarpossitionDefaultBUsiness barPosBiz = BarpossitionDefaultBUsiness.CreateInstance();
        #endregion Members

        #region Properties
        public int SiteId
        {
            get
            {
                return _SiteId;
            }
            set
            {
                _SiteId = value;
            }
        }

        public int BarPositionID
        {
            get
            {
                return _BarPositionID;
            }
            set
            {
                _BarPositionID = value;
            }
        }

        public string BarPositionName
        {
            get
            {
                return _BarPositionName;
            }
            set
            {
                _BarPositionName = value;
            }
        }
        #endregion Properties

        #region Constructors
        public ucBarPosition(int BarPositionID, int SiteID)
        {
            InitializeComponent();
            this._BarPositionID = BarPositionID;
            this._SiteId = SiteID;
            //tabBarPosition.TabPages.Remove(tabPage2);
            tabBarPosition.TabPages.Remove(tabPage3);
            tabBarPosition.TabPages.Remove(tabPage5);
            tabBarPosition.TabPages.Remove(tabPage6);
            LoadOpeartorCombo();
            LoadMachineTypeCombo();
            LoadZoneCombo(SiteID);
            //LoadTermsGroup();
            chkTerminate.Visible = DTTerminated.Visible = false;
            // Set Tags for controls
            SetTagProperty();
        }

        public ucBarPosition()
        {
            InitializeComponent();
            //tabBarPosition.TabPages.Remove(tabPage2);
            tabBarPosition.TabPages.Remove(tabPage3);
            tabBarPosition.TabPages.Remove(tabPage4);
            tabBarPosition.TabPages.Remove(tabPage5);
            tabBarPosition.TabPages.Remove(tabPage6);
            //LoadTermsGroup();
            chkTerminate.Visible = DTTerminated.Visible = false;

            // Set Tags for controls
            SetTagProperty();
        }
        #endregion

        #region Events


        private void ucBarPosition_Load(object sender, EventArgs e)
        {
            adminBusiness = new AdminSubCompany();
            objBZonePos = new BZonePosition();
            objCU = new CommonUtility();

            // For externalization
            this.ResolveResources();
            _msgBoxTitle = this.GetResourceTextByKey(1, "MSG_APP_TITLE");

            HideUnusedControls();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Parent.Hide();
        }

        private void cboPositionoperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboPositionoperator.SelectedIndex > 0 && cboPositionoperator.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(cboPositionoperator.SelectedValue)))
                    {
                        int Operator_ID = Convert.ToInt32(cboPositionoperator.SelectedValue.ToString());
                        LoadDepotCombo(Operator_ID);
                    }
                }
                else
                    LoadDepotCombo(-1);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtOSiteCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void btnBarPosPicChange_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JPEG Files (*.jpeg;*.jpg)|*.jpg;*.jpeg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif";
            dialog.InitialDirectory = @"C:\";
            // dialog.Title = "Please select an image file";
            dialog.Title = this.GetResourceTextByKey(1, "MSG_BARPOS_IMAGENAME"); //"Please select an image file";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imagePath = dialog.FileName;
                txtBarPosPic.Text = Path.GetFileName(dialog.FileName);
                if (txtBarPosPic.Text.Length > 50)
                {
                    //  Win32Extensions.ShowInfoMessageBox(this, "File Name cannot be more than 50 Characters");
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_BARPOS_FILENAME"), this.ParentForm.Text);//"File Name cannot be more than 50 Characters"
                    return;
                }
                ImgBarPosPic.Load(dialog.FileName);
            }
        }

        private void cboPrice_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<BPSitePricePerPlayResult> cboPriceloadt = BarpossitionDefaultBUsiness.CreateInstance().BPSitePricePerPlay(1);
                if ((cboPrice.Text == this.GetResourceTextByKey("Key_NoneText")))                // "None")
                {
                    //txtTP.Text = "None";
                    txtTP.Text = this.GetResourceTextByKey(1, "MSG_NONE");
                }
                else
                    if (cboPriceloadt.Count >= 1)
                    {
                        if (cboPrice.Text == this.GetResourceTextByKey("Key_UseSubCompanyDefault"))          // "Use Sub Company Default")
                        {
                            // txtTP.Text = "None";
                            txtTP.Text = this.GetResourceTextByKey(1, "MSG_NONE");
                        }
                        else
                        {
                            txtTP.Text = cboPrice.Text;
                        }
                    }
                if (cboPrice.Text == this.GetResourceTextByKey("Key_UseSubCompanyDefault"))                //"Use Sub Company Default")
                {
                    // txtTP.Text = "None";
                    txtTP.Text = this.GetResourceTextByKey(1, "MSG_NONE");
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion Events

        #region PublicMethods

        public void LoadDefault()
        {
            chkEnable.Enabled = AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Bar_EnableDisable");

            LoadInitialData();
            InitializeData();
            LoadTermsGroup();
        }

        public void LoadTermsGroup()
        {
            BZonePosition objZoneBiz = new BZonePosition();
            try
            {
                DataTable dtTerms = objZoneBiz.GetTermsGroup();
                DataRow drNone = dtTerms.NewRow();
                drNone["Terms_Group_Name"] = this.GetResourceTextByKey("Key_NoneHyphen");                  // "-None-";
                drNone["Terms_Group_ID"] = -1;
                dtTerms.Rows.InsertAt(drNone, 0);
                DataRow drUseSiteDefault = dtTerms.NewRow();
                drUseSiteDefault["Terms_Group_Name"] = "Use Site Default";
                drUseSiteDefault["Terms_Group_ID"] = -2;
                dtTerms.Rows.InsertAt(drUseSiteDefault, 1);
                if (dtTerms.Rows.Count > 0)
                {
                    cboTermGroupPrevious.DataSource = dtTerms.Copy();
                    cboTermGroupPrevious.DisplayMember = "Terms_Group_Name";
                    cboTermGroupPrevious.ValueMember = "Terms_Group_ID";
                    cboTermGroupPrevious.SelectedItem = entity.Terms_Group_Past_ID.HasValue ? cboTermGroupPrevious.Items.OfType<DataRowView>().FirstOrDefault(x => Convert.ToInt32(x.Row["Terms_Group_ID"]) == entity.Terms_Group_Past_ID.Value) : cboTermGroupPrevious.Items.OfType<DataRowView>().FirstOrDefault();

                    cboTermsGroup.DataSource = dtTerms.Copy();
                    cboTermsGroup.DisplayMember = "Terms_Group_Name";
                    cboTermsGroup.ValueMember = "Terms_Group_ID";
                    cboTermsGroup.SelectedItem = entity.Terms_Group_ID.HasValue ? cboTermsGroup.Items.OfType<DataRowView>().FirstOrDefault(x => Convert.ToInt32(x["Terms_Group_ID"]) == entity.Terms_Group_ID.Value) : cboTermsGroup.Items.OfType<DataRowView>().FirstOrDefault();


                    cboGroupFuture.DataSource = dtTerms.Copy();
                    cboGroupFuture.DisplayMember = "Terms_Group_Name";
                    cboGroupFuture.ValueMember = "Terms_Group_ID";
                    cboGroupFuture.SelectedItem = entity.Terms_Group_Future_ID.HasValue ? cboGroupFuture.Items.OfType<DataRowView>().FirstOrDefault(x => Convert.ToInt32(x["Terms_Group_ID"]) == entity.Terms_Group_Future_ID.Value) : cboGroupFuture.Items.OfType<DataRowView>().FirstOrDefault();
                }

                dtpFutureChangeOverDate.Value = Convert.ToDateTime(entity.Terms_Group_Changeover_Date);
                dtpPreviousChangeOverDate.Value = Convert.ToDateTime(entity.Terms_Group_Past_Changeover_Date);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void LoadZoneCombo(int SiteId)
        {
            this.SiteId = SiteId;
            BZonePosition objZoneBiz = new BZonePosition();
            try
            {
                // _SiteId = entity.Site_ID;
                DataTable dtZone = objZoneBiz.GetZoneDetailsBySiteID(SiteId);
                DataRow dr = dtZone.NewRow();
                dr["Zone_Name"] = this.GetResourceTextByKey("Key_NoneHyphen");                  // "-None-";
                dr["Zone_ID"] = -1;
                dtZone.Rows.InsertAt(dr, 0);
                if (dtZone.Rows.Count > 0)
                {
                    cboZone.DataSource = dtZone;
                }
                cboZone.DisplayMember = "Zone_Name";
                cboZone.ValueMember = "Zone_ID";
                cboZone.SelectedValue = -1;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void LoadOpeartorCombo()
        {
            BZonePosition objZoneBiz = new BZonePosition();
            try
            {
                DataTable dtOperator = objZoneBiz.GetOperatorForBarPosition();
                DataRow dr = dtOperator.NewRow();
                dr["Operator_Name"] = this.GetResourceTextByKey("Key_NoneHyphen");                  // "-None-";
                dr["Operator_ID"] = -1;
                dtOperator.Rows.InsertAt(dr, 0);
                if (dtOperator.Rows.Count > 0)
                {
                    cboPositionoperator.DataSource = dtOperator;
                }

                cboPositionoperator.DisplayMember = "Operator_Name";
                cboPositionoperator.ValueMember = "Operator_ID";
                cboPositionoperator.SelectedValue = -1;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void LoadMachineTypeCombo()
        {
            BZonePosition objZoneBiz = new BZonePosition();
            try
            {
                DataTable dtMachine = objZoneBiz.GetMachineType();
                DataRow dr = dtMachine.NewRow();
                dr["Machine_Type_Code"] = this.GetResourceTextByKey("Key_NoneHyphen");                  // "-None-";
                dr["Machine_Type_ID"] = -1;
                dtMachine.Rows.InsertAt(dr, 0);
                if (dtMachine.Rows.Count > 0)
                {
                    cboMachineType.DataSource = dtMachine;
                }
                cboMachineType.DisplayMember = "Machine_Type_Code";
                cboMachineType.ValueMember = "Machine_Type_ID";
                cboMachineType.SelectedValue = -1;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void LoadDepotCombo(int _supplierid)
        {
            BZonePosition objZoneBiz = new BZonePosition();
            try
            {
                DataTable dtDepot = objZoneBiz.GetDepotListForPosition(_supplierid);
                DataRow dr = dtDepot.NewRow();
                dr["Depot_Name"] = this.GetResourceTextByKey("Key_NoneHyphen");                  // "-None-";
                dr["Depot_ID"] = -1;
                dtDepot.Rows.InsertAt(dr, 0);
                if (dtDepot.Rows.Count > 0)
                {
                    cboPositionDepot.DataSource = dtDepot;
                }
                cboPositionDepot.DisplayMember = "Depot_Name";
                cboPositionDepot.ValueMember = "Depot_ID";
                cboPositionDepot.SelectedValue = -1;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }
        }

        public void AuditUpdateBarPositonDetails(BarPositionInfoEntity OldEntity, BarPositionEntity NewEntity, string SiteName)
        {
            string sUserName = AppGlobals.Current.UserName;
            int iUserId = AppGlobals.Current.UserId;
            BZonePosition objZoneBiz = new BZonePosition();
            if ((OldEntity == null) || (NewEntity == null)) return;
            if (OldEntity.Bar_Position_Name.NullToString() != NewEntity.Bar_Position_Name)
                objBZonePos.Audit(ModuleNameEnterprise.AUDIT_POSITION, "BarPosition ", "Site.Position", txtPositionName.Text.Trim(), "Bar_Position_Name", OldEntity.Bar_Position_Name, NewEntity.Bar_Position_Name, iUserId, sUserName, SiteName);
            if (OldEntity.Bar_Position_Company_Position_Code.NullToString() != NewEntity.Bar_Position_Company_Position_Code)
                objBZonePos.Audit(ModuleNameEnterprise.AUDIT_POSITION, "BarPosition ", "Site.Position", txtPositionName.Text.Trim(), "Bar_Position_Company_Position_Code", OldEntity.Bar_Position_Company_Position_Code, NewEntity.Bar_Position_Company_Position_Code, iUserId, sUserName, SiteName);
            if (OldEntity.Bar_Position_Supplier_Position_Code.NullToString() != NewEntity.Bar_Position_Supplier_Position_Code)
                objBZonePos.Audit(ModuleNameEnterprise.AUDIT_POSITION, "BarPosition ", "Site.Position", txtPositionName.Text.Trim(), "Bar_Position_Supplier_Code", OldEntity.Bar_Position_Supplier_Position_Code, NewEntity.Bar_Position_Supplier_Position_Code, iUserId, sUserName, SiteName);
            if (OldEntity.Bar_Position_Supplier_Site_Code.NullToString() != NewEntity.Bar_Position_Supplier_Site_Code)
                objBZonePos.Audit(ModuleNameEnterprise.AUDIT_POSITION, "BarPosition ", "Site.Position", txtPositionName.Text.Trim(), "Bar_Position_Supplier_Site_Code", OldEntity.Bar_Position_Supplier_Site_Code, NewEntity.Bar_Position_Supplier_Site_Code, iUserId, sUserName, SiteName);
            if (OldEntity.Bar_Position_Location.NullToString() != NewEntity.Bar_Position_Location)
                objBZonePos.Audit(ModuleNameEnterprise.AUDIT_POSITION, "BarPosition ", "Site.Position", txtPositionName.Text.Trim(), "Bar_Position_Location", OldEntity.Bar_Position_Location, NewEntity.Bar_Position_Location, iUserId, sUserName, SiteName);
            if (Convert.ToInt32(OldEntity.Depot_ID) != NewEntity.Depot_ID)
                objBZonePos.Audit(ModuleNameEnterprise.AUDIT_POSITION, "BarPosition ", "Site.Position", txtPositionName.Text.Trim(), "Depot_Name", sDepotName, cboPositionDepot.Text, iUserId, sUserName, SiteName);
            if (OldEntity.Bar_Position_Collection_Day.NullToString() != NewEntity.Bar_Position_Collection_Day)
                objBZonePos.Audit(ModuleNameEnterprise.AUDIT_POSITION, "BarPosition ", "Site.Position", txtPositionName.Text.Trim(), "Bar_Position_Collection_Day", sCollectionDay, cboCollectionDay.Text, iUserId, sUserName, SiteName);
            if (OldEntity.Machine_Type_ID != NewEntity.Machine_Type_ID)
                objBZonePos.Audit(ModuleNameEnterprise.AUDIT_POSITION, "BarPosition ", "Site.Position", txtPositionName.Text.Trim(), "Machine_Type_Code", sMachineType, cboMachineType.Text, iUserId, sUserName, SiteName);
            if (OldEntity.Bar_Position_Category != NewEntity.Bar_Position_Category)
                objBZonePos.Audit(ModuleNameEnterprise.AUDIT_POSITION, "BarPosition ", "Site.Position", txtPositionName.Text.Trim(), "Bar_Position_Category", sCategory, cboCategory.Text, iUserId, sUserName, SiteName);
            if (OldEntity.Zone_ID != NewEntity.Zone_ID)
                objBZonePos.Audit(ModuleNameEnterprise.AUDIT_POSITION, "BarPosition ", "Site.Position", txtPositionName.Text.Trim(), "Zone_Name", sZoneName, cboZone.Text, iUserId, sUserName, SiteName);
        }

        public void SaveData()
        {
            try
            {
                BarPositionEntity BarPosData = objBZonePos.BGetBarPositionDetails(this.BarPositionID);
                BarPositionEntity entity1 = objBZonePos.BGetBarPositionDetails(this.BarPositionID);

                //Update BarPositionDetails

                BarPosData.Bar_Position_Name = txtPositionName.Text.PadLeft(3, '0');
                BarPosData.Bar_Position_Company_Position_Code = txtBarBreweryPositionCode.Text;
                BarPosData.Bar_Position_Location = TxtBarLocation.Text;
                if (Convert.ToInt32(cboZone.SelectedValue) >= 0)
                    BarPosData.Zone_ID = Convert.ToInt32(cboZone.SelectedValue);
                else
                    BarPosData.Zone_ID = 0;
                BarPosData.Machine_Type_ID = Convert.ToInt32(string.IsNullOrEmpty(Convert.ToString(cboMachineType.SelectedValue)) ? "0" : Convert.ToString(cboMachineType.SelectedValue));
                BarPosData.Bar_Position_Collection_Period = Convert.ToInt32(objCU.VerifyValidNumberLong(txtCollectionPeriod.Text));
                BarPosData.Bar_Position_Collection_Day = string.Empty;
                BarPosData.Depot_ID = 0;
                BarPosData.Bar_Position_Image_Reference = string.Empty;
                if (cboCollectionDay.SelectedIndex == -1)
                    BarPosData.Bar_Position_Collection_Day = string.Empty;
                else if (cboCollectionDay.Text.ToUpper() == this.GetResourceTextByKey("Key_CollectionDaysAny").ToUpper())            //"-- ANY --")
                    BarPosData.Bar_Position_Collection_Day = string.Empty;
                else
                    BarPosData.Bar_Position_Collection_Day = cboCollectionDay.Text;

                if (cboPositionDepot.Items.Count > 0)
                {
                    if (Convert.ToInt32(cboPositionDepot.SelectedValue) >= 0)
                        BarPosData.Depot_ID = Convert.ToInt32(cboPositionDepot.SelectedValue);
                    else
                        BarPosData.Depot_ID = 0;

                }
                else
                {
                    BarPosData.Depot_ID = 0;
                }
                if (chkTerminate.Checked)
                    BarPosData.Bar_Position_End_Date = DTTerminated.Value.ToShortDateString();
                else
                    BarPosData.Bar_Position_End_Date = null;

                BarPosData.Bar_Position_Image_Reference = txtBarPosPic.Text;

                BarPosData.Bar_Position_Supplier_Site_Code = txtBarSupplierSiteCode.Text;

                BarPosData.Bar_Position_Supplier_Position_Code = txtBarSupplierPositionCode.Text;


                if (BarPosData.Bar_Position_Use_Terms != (chkBarValidateDataThroughTerms.Checked = true))
                {

                    BarPosData.Bar_Position_Use_Terms = chkBarValidateDataThroughTerms.Checked;
                }
                if (objCU.VerifyValidNumberLong(Convert.ToString(BarPosData.Bar_Position_Override_Rent_From_Schedule_To_Rent)) != objCU.VerifyValidNumberLong(Convert.ToString(chkChangeFromScheduleToRent.Checked)))
                {

                    BarPosData.Bar_Position_Override_Rent_From_Schedule_To_Rent = chkChangeFromScheduleToRent.Checked;
                }

                if (BarPosData.Bar_Position_Override_Rent_From_Schedule_To_Rent_Date != DTBarPosChangeFromRentToSchedule.Value.ToString(dateFormat))
                {

                    BarPosData.Bar_Position_Override_Rent_From_Schedule_To_Rent_Date = DTBarPosChangeFromRentToSchedule.Value.ToString(dateFormat);
                }
                if (objCU.VerifyValidNumberLong(BarPosData.Bar_Position_Override_Rent_From_Rent_To_Schedule.ToString()) != objCU.VerifyValidNumberLong(chkChangeFromRentToSchedule.Checked.ToString()))
                {

                    BarPosData.Bar_Position_Override_Rent_From_Rent_To_Schedule = chkChangeFromRentToSchedule.Checked;
                }
                if (BarPosData.Bar_Position_Override_Rent_From_Rent_To_Schedule_Date != DTBarPosChangeFromRentToSchedule.Value.ToString(dateFormat))
                {

                    BarPosData.Bar_Position_Override_Rent_From_Rent_To_Schedule_Date = DTBarPosChangeFromRentToSchedule.Value.ToString(dateFormat);
                }
                if (objCU.VerifyValidNumberLong(BarPosData.Bar_Position_Rent_Schedule_ID_From.ToString()) != Convert.ToInt32(string.IsNullOrEmpty(Convert.ToString(cboFromRentSchedule.SelectedValue)) ? "0" : Convert.ToString(cboFromRentSchedule.SelectedValue)))
                {

                    BarPosData.Bar_Position_Rent_Schedule_ID_From = Convert.ToInt32(cboFromRentSchedule.SelectedValue.ToString());
                }
                if (BarPosData.Bar_Position_Override_Rent != chkUseBarPosRent.Checked)
                {

                    BarPosData.Bar_Position_Override_Rent = chkUseBarPosRent.Checked;
                }

                if (BarPosData.Bar_Position_Override_Rent_Schedule != chkUseBarPosRentSchedule.Checked)
                {

                    BarPosData.Bar_Position_Override_Rent_Schedule = chkUseBarPosRentSchedule.Checked;
                }

                if (BarPosData.Bar_Position_Rent_Schedule_ID != Convert.ToInt32(string.IsNullOrEmpty(Convert.ToString(cboRentSchedule.SelectedValue)) ? "0" : Convert.ToString(cboRentSchedule.SelectedValue)))
                {
                    BarPosData.Bar_Position_Rent_Schedule_ID = Convert.ToInt32(cboRentSchedule.SelectedValue.ToString());
                }
                if (BarPosData.Bar_Position_Override_Shares != chkUseBarPosShareSchedule.Checked)
                {
                    BarPosData.Bar_Position_Override_Shares = chkUseBarPosShareSchedule.Checked;
                }
                if (BarPosData.Bar_Position_Override_Licence != chkUseBarPosLicence.Checked)
                {
                    BarPosData.Bar_Position_Override_Licence = chkUseBarPosLicence.Checked;
                }
                if (BarPosData.Bar_Position_Rent_Previous != objCU.VerifyValidNumberLong(txtBarPosTermsRentPrev.Text))
                {
                    BarPosData.Bar_Position_Rent_Previous = Convert.ToSingle(txtBarPosTermsRentPrev.Text);
                }

                if (BarPosData.Bar_Position_Rent != Convert.ToSingle(txtBarPosTermsRent.Text))
                {

                    BarPosData.Bar_Position_Rent = Convert.ToSingle(txtBarPosTermsRent.Text);
                }

                if (BarPosData.Bar_Position_Rent_Future != Convert.ToSingle(txtBarPosTermsRentFuture.Text))
                {

                    BarPosData.Bar_Position_Rent_Future = Convert.ToSingle(txtBarPosTermsRentFuture.Text);
                }

                if (BarPosData.Bar_Position_Supplier_Share_Previous != Convert.ToSingle(txtBarPosTermsSupplierPrev.Text))
                {

                    BarPosData.Bar_Position_Supplier_Share_Previous = Convert.ToSingle(txtBarPosTermsSupplierPrev.Text);
                }

                if (BarPosData.Bar_Position_Supplier_Share != Convert.ToSingle(txtBarPosTermsSupplier.Text))
                {

                    BarPosData.Bar_Position_Supplier_Share = Convert.ToSingle(txtBarPosTermsSupplier.Text);
                }

                if (BarPosData.Bar_Position_Supplier_Share_Future != Convert.ToSingle(txtBarPosTermsSupplierFuture.Text))
                {

                    BarPosData.Bar_Position_Supplier_Share_Future = Convert.ToSingle(txtBarPosTermsSupplierFuture.Text);
                }

                if (BarPosData.Bar_Position_Site_Share_Previous != Convert.ToSingle(txtBarPosTermsSitePrev.Text))
                {

                    BarPosData.Bar_Position_Site_Share_Previous = Convert.ToSingle(txtBarPosTermsSitePrev.Text);
                }

                if (BarPosData.Bar_Position_Site_Share != Convert.ToSingle(txtBarPosTermsSite.Text))
                {

                    BarPosData.Bar_Position_Site_Share = Convert.ToSingle(txtBarPosTermsSite.Text);
                }

                if (BarPosData.Bar_Position_Site_Share_Future != Convert.ToSingle(txtBarPosTermsSiteFuture.Text))
                {

                    BarPosData.Bar_Position_Site_Share_Future = Convert.ToSingle(txtBarPosTermsSiteFuture.Text);
                }

                if (BarPosData.Bar_Position_Owners_Share_Previous != Convert.ToSingle(txtBarPosTermsCompanyPrev.Text))
                {

                    BarPosData.Bar_Position_Owners_Share_Previous = Convert.ToSingle(txtBarPosTermsCompanyPrev.Text);
                }

                if (BarPosData.Bar_Position_Owners_Share != Convert.ToSingle(txtBarPosTermsCompany.Text))
                {

                    BarPosData.Bar_Position_Owners_Share = Convert.ToSingle(txtBarPosTermsCompany.Text);
                }


                if (BarPosData.Bar_Position_Owners_Share_Future != Convert.ToSingle(txtBarPosTermsCompanyFuture.Text))
                {

                    BarPosData.Bar_Position_Owners_Share_Future = Convert.ToSingle(txtBarPosTermsCompanyFuture.Text);
                }

                if (BarPosData.Bar_Position_Secondary_Owners_Share_Previous != Convert.ToSingle(txtBarPosTermsSecBrewPrev.Text))
                {

                    BarPosData.Bar_Position_Secondary_Owners_Share_Previous = Convert.ToSingle(txtBarPosTermsSecBrewPrev.Text);
                }
                if (BarPosData.Bar_Position_Secondary_Owners_Share != Convert.ToSingle(txtBarPosTermsSecBrew.Text))
                {

                    BarPosData.Bar_Position_Secondary_Owners_Share = Convert.ToSingle(txtBarPosTermsSecBrew.Text);
                }

                if (BarPosData.Bar_Position_Secondary_Owners_Share_Future != Convert.ToSingle(txtBarPosTermsSecBrewFuture.Text))
                {

                    BarPosData.Bar_Position_Secondary_Owners_Share_Future = Convert.ToSingle(txtBarPosTermsSecBrewFuture.Text);
                }

                if (BarPosData.Bar_Position_Licence_Previous != Convert.ToSingle(txtBarPosTermsLicencePrev.Text))
                {

                    BarPosData.Bar_Position_Licence_Previous = Convert.ToSingle(txtBarPosTermsLicencePrev.Text);
                }

                if (BarPosData.Bar_Position_Licence_Charge != Convert.ToSingle(txtBarPosTermsLicence.Text))
                {

                    BarPosData.Bar_Position_Licence_Charge = Convert.ToSingle(txtBarPosTermsLicence.Text);
                }

                if (BarPosData.Bar_Position_Licence_Future != Convert.ToSingle(txtBarPosTermsLicenceFuture.Text))
                {

                    BarPosData.Bar_Position_Licence_Future = Convert.ToSingle(txtBarPosTermsLicenceFuture.Text);
                }

                if (BarPosData.Bar_Position_Rent_Past_Date != DTBarPosRentChangePrev.Value.ToString(dateFormat))
                {

                    BarPosData.Bar_Position_Rent_Past_Date = DTBarPosRentChangePrev.Value.ToString(dateFormat);
                }

                if (BarPosData.Bar_Position_Rent_Future_Date != DTBarPosRentChangeFuture.Value.ToString(dateFormat))
                {

                    BarPosData.Bar_Position_Rent_Future_Date = DTBarPosRentChangeFuture.Value.ToString(dateFormat);
                }

                if (BarPosData.Bar_Position_Share_Past_Date != DTBarPosSharesChangePrev.Value.ToString(dateFormat))
                {

                    BarPosData.Bar_Position_Share_Past_Date = DTBarPosSharesChangePrev.Value.ToString(dateFormat);
                }
                if (BarPosData.Bar_Position_Share_Future_Date != DTBarPosSharesChangeFuture.Value.ToString(dateFormat))
                {

                    BarPosData.Bar_Position_Share_Future_Date = DTBarPosSharesChangeFuture.Value.ToString(dateFormat);
                }

                if (BarPosData.Bar_Position_Licence_Past_Date != DTBarPosLicenceChangePrev.Value.ToString(dateFormat))
                {

                    BarPosData.Bar_Position_Licence_Past_Date = DTBarPosLicenceChangePrev.Value.ToString(dateFormat);
                }

                if (BarPosData.Bar_Position_Licence_Future_Date != DTBarPosLicenceChangeFuture.Value.ToString(dateFormat))
                {

                    BarPosData.Bar_Position_Licence_Future_Date = DTBarPosLicenceChangeFuture.Value.ToString(dateFormat);
                }

                if (BarPosData.Bar_Position_Use_Site_Share_For_Secondary_Brewery != chkUseSiteShareForSecondaryBrewery.Checked)
                {

                    BarPosData.Bar_Position_Use_Site_Share_For_Secondary_Brewery = chkUseSiteShareForSecondaryBrewery.Checked;
                }

                if (BarPosData.Bar_Position_Prize_LOS != chkPrizeLOS.Checked)
                {

                    BarPosData.Bar_Position_Prize_LOS = chkPrizeLOS.Checked;
                }

                if (BarPosData.Bar_Position_Override_Share_Schedule != chkUseBarPosShareSchedule.Checked)
                {

                    BarPosData.Bar_Position_Override_Share_Schedule = chkUseBarPosShareSchedule.Checked;
                }
                if (BarPosData.Bar_Position_Share_Schedule_ID != Convert.ToInt32(string.IsNullOrEmpty(Convert.ToString(cboShareSchedule.SelectedValue)) ? "0" : Convert.ToString(cboShareSchedule.SelectedValue)))
                {

                    BarPosData.Bar_Position_Share_Schedule_ID = Convert.ToInt32(cboShareSchedule.SelectedValue.ToString());
                }

                if (BarPosData.Bar_Position_IsEnable != Convert.ToBoolean(chkEnable.Checked))
                {
                    BarPosData.Bar_Position_IsEnable = Convert.ToBoolean(chkEnable.Checked);
                }

                BarPosData.Bar_Position_Disable_EDI_Export = chkDisableEDIExport.Checked;
                BarPosData.Bar_Position_Image_Reference = txtBarPosPic.Text.Trim();
                if (!string.IsNullOrEmpty(txtBarPosPic.Text.Trim()) && File.Exists(imagePath.Trim()))
                {
                    byte[] image = File.ReadAllBytes(imagePath.Trim());
                    objBZonePos.InsertOrUpdateBarPositionExtension(BarPositionID, image, false);
                }
                if (string.IsNullOrEmpty(txtBarPosPic.Text.Trim()))
                {
                    objBZonePos.InsertOrUpdateBarPositionExtension(BarPositionID, new byte[0], true);
                }
                int updateBarPosition = objBZonePos.BInsertOrUpdateBarPosition(BarPosData, entity1);

                AuditUpdateBarPositonDetails(entity, BarPosData, entity.Site_Name);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public bool ValidateAndSaveData()
        {
            try
            {
                if (!Regex.Match(txtPositionName.Text, "^[0-9]+$").Success)
                {

                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_BARPOS_INVALIDCHAR_FOUND"), this.ParentForm.Text);//"Found invalid characters, Please enter valid Bar Position name"

                    return false;
                }
                if (CommonBiz.EnsureValidString(txtPositionName.Text) != txtPositionName.Text)
                {

                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_BARPOS_INVALID_NAME"), this.ParentForm.Text);//"Invalid name entered"
                    return false;

                }
                if (txtPositionName.Text.Length > 3)
                {

                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_BARPOS_NAME_EXCEED"), this.ParentForm.Text);//"Bar Position Name exceeded 3 Chars. Please enter valid Bar Position"
                    return false;
                }
                int? iNameExists = 0;
                int IsBarPositionName = objBZonePos.BCheckNameExist(SiteId, txtPositionName.Text.Trim(), ref iNameExists, BarPositionID);
                if (iNameExists > 0)
                {
                    txtPositionName.Focus();
                    // Win32Extensions.ShowInfoMessageBox(this, String.Format("A bar position [{0}] already exists.", txtPositionName.Text));--Doubt
                    Win32Extensions.ShowInfoMessageBox(this, String.Format(this.GetResourceTextByKey(1, "MSG_BARPOS_NAME_EXISTS"), txtPositionName.Text), this.ParentForm.Text);
                    return false;
                }
                if (cboPositionoperator.SelectedIndex > 0 && cboPositionDepot.SelectedIndex <= 0)
                {
                    //  Win32Extensions.ShowInfoMessageBox(this, "Depot needs to be selected for the operator.");
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_BARPOS_SELECT_DEPOT"), this.ParentForm.Text);
                    return false;
                }

                if (this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_BARPOS_APPLYSETTING"), this.ParentForm.Text) == DialogResult.No)
                {
                    return false;
                }
                this.SaveData();
                int ExportHistory = objBZonePos.BExportHistory(this.SiteId.ToString(), "SITESETUP", this.SiteId);
                String EnableorDisableLog = chkEnable.Checked ? "Enabled" : "Disabled";
                objBZonePos.Audit(ModuleNameEnterprise.EnableDisableMachine, txtPositionName.Text.Trim() + "---" + EnableorDisableLog, "Site.Position", "", "Enable/Disable", "", "", AppGlobals.Current.UserId, AppGlobals.Current.UserName, entity.Site_Name);
                // objBZonePos.AuditModifyDataForBar(ModuleNameEnterprise.AUDIT_POSITION, "Site.Position",, AppGlobals.Current.UserId, AppGlobals.Current.UserName, entity.Site_Name);

                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_BARPOS_NAME_SAVED"), this.ParentForm.Text);//"Bar Position Information saved successfully"

                //ucBarPosition_Load(sender, e);
                InitializeData();
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        #endregion

        #region PrivateMethods

        void HideUnusedControls()
        {

            lblPosName.Visible = false;
            txtBarBreweryPositionCode.Visible = false;
            lblPosOperator.Visible = false;
            cboPositionoperator.Visible = false;
            lblOpCode.Visible = false;
            lblDepot.Visible = false;
            txtBarSupplierPositionCode.Visible = false;
            lblDepot.Visible = false;
            cboPositionDepot.Visible = false;
            lblSiteCode.Visible = false;
            txtBarSupplierSiteCode.Visible = false;
            lblCollection.Visible = false;
            cboCollectionDay.Visible = false;
            lblDescribtion.Visible = false;
            TxtBarLocation.Visible = false;
            lblType.Visible = false;
            cboMachineType.Visible = false;
            lblDays.Visible = false;
            txtCollectionPeriod.Visible = false;
            lblCategory.Visible = false;
            cboCategory.Visible = false;
        }


        private void LoadInitialData()
        {
            try
            {
                //cboPrice.Items.Add(this.GetResourceTextByKey("Key_UseCompanyDefault"));              //"Use Company Default");
                cboPrice.Items.Add("Use Site Default");
                cboPrice.Items.Add("0");
                cboPrice.Items.Add("1");
                cboPrice.Items.Add("2");
                cboPrice.Items.Add("5");
                cboPrice.Items.Add("10");
                cboPrice.Items.Add("15");
                cboPrice.Items.Add("20");
                cboPrice.Items.Add("25");
                cboPrice.Items.Add("30");
                cboPrice.Items.Add("35");
                cboPrice.Items.Add("40");
                cboPrice.Items.Add("45");
                cboPrice.Items.Add("50");
                cboPrice.Items.Add("60");
                cboPrice.Items.Add("70");
                cboPrice.Items.Add("80");
                cboPrice.Items.Add("90");
                cboPrice.Items.Add("100");
                cboPrice.Items.Add("150");
                cboPrice.Items.Add("200");
                cboPrice.Items.Add("250");
                cboPrice.Items.Add("300");
                cboPrice.Items.Add("500");
                cboPrice.Items.Add("1000");

                //cboPayout.Items.Add(this.GetResourceTextByKey("Key_UseCompanyDefault"));              // "Use Company Default");
                cboPayout.Items.Add("Use Site Default");
                cboPayout.SelectedIndex = 0;

                cboJackpot.Items.Clear();
                //cboJackpot.Items.Add(this.GetResourceTextByKey("Key_UseCompanyDefault"));              //"Use Company Default");
                cboJackpot.Items.Add("Use Site Default");
                cboJackpot.Items.Add("1");
                cboJackpot.Items.Add("2");
                cboJackpot.Items.Add("5");
                cboJackpot.Items.Add("10");
                cboJackpot.Items.Add("15");
                cboJackpot.Items.Add("20");
                cboJackpot.Items.Add("25");
                cboJackpot.Items.Add("50");
                cboJackpot.Items.Add("75");
                cboJackpot.Items.Add("100");
                cboJackpot.Items.Add("150");
                cboJackpot.Items.Add("200");
                cboJackpot.Items.Add("250");
                cboJackpot.Items.Add("500");
                cboJackpot.Items.Add("1000");

                cboAccessKey.Items.Add("None");
                cboAccessKey.Items.Add("Use Site Default");

                for (int i = 1; i <= 100; i++)
                {
                    cboPayout.Items.Add(i.ToString());
                }

                List<ComboBoxItem> lstComboBoxItem = new List<ComboBoxItem>();
                lstComboBoxItem.Add(new ComboBoxItem() { Text = this.GetResourceTextByKey("Key_NoneText"), Value = "0" });                        // "None"
                lstComboBoxItem.Add(new ComboBoxItem() { Text = this.GetResourceTextByKey("Key_ItemsCascade"), Value = "1" });                  //  "Items using cascade"
                lstComboBoxItem.Add(new ComboBoxItem() { Text = this.GetResourceTextByKey("Key_AllItems"), Value = "2" });                   // "All items"

                BarpossitionDefaultBUsiness.CreateInstance().BPAccessKeyResult(1);

                ///cboPrice
                //if (cboPrice.SelectedIndex >= 0)
                //{
                //    if (cboPrice.SelectedIndex == -1)
                //    {

                //        List<BPSitePricePerPlayResult> cboPriceload = BarpossitionDefaultBUsiness.CreateInstance().BPSitePricePerPlay(1);
                //        if (cboPriceload.Count > 0)
                //        {
                //            foreach (var vtapp in cboPriceload)
                //            {

                //                txtPPP.Text = vtapp.Site_Price_Per_Play;

                //            }
                //        }
                //        else
                //        {
                //            txtPPP.Text = "0";
                //        }
                //    }
                //    else
                //    {
                //        txtPPP.Text = Convert.ToString((cboPrice.SelectedItem as ComboBoxItem).Value);
                //    }
                //}
                //else
                //{

                //    txtPPP.Text = Convert.ToString((cboPrice.SelectedItem as ComboBoxItem).Value);

                //}


                /////cboJackpot

                //if (cboJackpot.SelectedIndex >= 0)
                //{
                //    if (cboJackpot.SelectedIndex == -1)
                //    {

                //        List<BPSiteJackpotResult> cboJackpotload = BarpossitionDefaultBUsiness.CreateInstance().BPSiteJackpotdetails(1);
                //        if (cboJackpotload.Count > 0)
                //        {
                //            foreach (var vtapp in cboJackpotload)
                //            {

                //                txtJP.Text = vtapp.Site_Jackpot;
                //            }
                //        }
                //        else
                //        {
                //            txtJP.Text = "0";
                //        }
                //    }
                //    else
                //    {

                //        txtJP.Text = Convert.ToString((cboPrice.SelectedItem as ComboBoxItem).Value);
                //    }
                //}
                //else
                //{

                //    txtJP.Text = Convert.ToString((cboJackpot.SelectedItem as ComboBoxItem).Value);
                //}


                //////cboPayout


                //if (cboPayout.SelectedIndex >= 0)
                //{
                //    if (cboPayout.SelectedIndex == -1)
                //    {

                //        List<BPSitePercentagePayoutResult> cboPayoutload = BarpossitionDefaultBUsiness.CreateInstance().BPSitePercentagePayoutDetail(1);
                //        if (cboPayoutload.Count > 0)
                //        {
                //            foreach (var vtapp in cboPayoutload)
                //            {

                //                txtPerc.Text = vtapp.Site_Percentage_Payout;
                //            }
                //        }
                //        else
                //        {


                //            txtPerc.Text = "0";
                //        }
                //    }
                //    else
                //    {

                //        txtPerc.Text = Convert.ToString((cboPrice.SelectedItem as ComboBoxItem).Value);
                //    }
                //}
                //else
                //{

                //    txtPerc.Text = Convert.ToString((cboPayout.SelectedItem as ComboBoxItem).Value);
                //}

                //if (cboTermsGroup.SelectedIndex >= 0)
                //{
                //    if (cboTermsGroup.SelectedIndex == -1)
                //    {

                //        List<GetBPTermsgroupResult> cboTermsGroupload = BarpossitionDefaultBUsiness.CreateInstance().TermsgroupResult(1);
                //        if (cboTermsGroupload.Count > 0)
                //        {
                //            foreach (var vtapp in cboTermsGroupload)
                //            {
                //                if (vtapp.Terms_Group_ID > 0)
                //                {
                //                    txtTP.Text = vtapp.Terms_Group_Name;
                //                }
                //                else
                //                {
                //                    txtTP.Text = this.GetResourceTextByKey(1, "MSG_BARPOS_NONE");
                //                }
                //            }
                //        }
                //        else
                //        {
                //            txtTP.Text = this.GetResourceTextByKey(1, "MSG_BARPOS_NONE");
                //        }
                //    }
                //    else
                //    {
                //        txtTP.Text = Convert.ToString((cboTermsGroup.SelectedItem as ComboBoxItem).Text);
                //    }
                //}
                //if (cboGroupFuture.SelectedIndex >= 0)
                //{
                //    if (cboGroupFuture.SelectedIndex == -1)
                //    {

                //        List<GetBPTermsgroupResult> cboGroupFutureload = BarpossitionDefaultBUsiness.CreateInstance().TermsgroupResult(1);
                //        if (cboGroupFutureload.Count > 0)
                //        {
                //            foreach (var vtapp in cboGroupFutureload)
                //            {
                //                if (vtapp.Terms_Group_ID > 0)
                //                {
                //                    txtTPFut.Text = vtapp.Terms_Group_Name;
                //                }
                //                else
                //                {
                //                    txtTPFut.Text = this.GetResourceTextByKey(1, "MSG_BARPOS_NONE");
                //                }
                //            }
                //        }
                //        else
                //        {
                //            txtTPFut.Text = this.GetResourceTextByKey(1, "MSG_BARPOS_NONE");

                //        }
                //    }
                //    else
                //    {
                //        txtTP.Text = Convert.ToString((cboGroupFuture.SelectedItem as ComboBoxItem).Text);
                //    }
                //}


                //////
                //if (cboTermGroupPrevious.SelectedIndex >= 0)
                //{
                //    if (cboTermGroupPrevious.SelectedIndex == -1)
                //    {

                //        List<GetBPTermsgroupResult> cboTermGroupPreviousload = BarpossitionDefaultBUsiness.CreateInstance().TermsgroupResult(1);
                //        if (cboTermGroupPreviousload.Count > 0)
                //        {
                //            foreach (var vtapp in cboTermGroupPreviousload)
                //            {
                //                if (vtapp.Terms_Group_ID > 0)
                //                {
                //                    txtTPPrev.Text = vtapp.Terms_Group_Name;
                //                }
                //                else
                //                {
                //                    txtTPPrev.Text = this.GetResourceTextByKey(1, "MSG_BARPOS_NONE");
                //                }
                //            }
                //        }
                //        else
                //        {
                //            txtTPPrev.Text = this.GetResourceTextByKey(1, "MSG_BARPOS_NONE");
                //        }
                //    }
                //    else
                //    {
                //        txtTPPrev.Text = Convert.ToString((cboTermGroupPrevious.SelectedItem as ComboBoxItem).Text);
                //    }
                //}
                //if (cboAccessKey.SelectedIndex >= 0)
                //{
                //    if (cboAccessKey.SelectedIndex == -1)
                //    {

                //        List<getBPAccessKeyResult> cboAccessKeyload = BarpossitionDefaultBUsiness.CreateInstance().BPAccessKeyResult(1);
                //        if (cboAccessKeyload.Count > 0)
                //        {
                //            foreach (var vtapp in cboAccessKeyload)
                //            {
                //                if (vtapp.Access_Key_ID > 0)
                //                {
                //                    txtAKey.Text = vtapp.Access_Key_Name;
                //                }
                //                else
                //                {

                //                    txtAKey.Text = this.GetResourceTextByKey(1, "MSG_BARPOS_NONE");
                //                }
                //            }
                //        }
                //        else
                //        {
                //            txtAKey.Text = this.GetResourceTextByKey(1, "MSG_BARPOS_NONE");
                //        }
                //    }
                //    else
                //    {
                //        txtAKey.Text = Convert.ToString((cboAccessKey.SelectedItem as ComboBoxItem).Text);
                //    }
                //}
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }
        }

        private void InitializeData()
        {
            try
            {
                BZonePosition objZoneBiz = new BZonePosition();
                entity = objZoneBiz.GetBarPositionInfo(BarPositionID);

                if (entity != null)
                {
                    LoadOpeartorCombo();
                    LoadMachineTypeCombo();
                    // title = "Position Admin  " + (entity.Site_Name).Trim() + ", " + (entity.Site_Code).Trim() + " - " + (entity.Bar_Position_Name).Trim();
                    title = string.Format(this.GetResourceTextByKey("Key_PositionAdmin"), (entity.Site_Name).Trim(), (entity.Site_Code).Trim(), (entity.Bar_Position_Name).Trim());
                    txtPositionName.Text = entity.Bar_Position_Name;

                    if (!string.IsNullOrEmpty(entity.Installation_End_Date))
                    {
                        //txtBarCurrentMachine.Text = entity.Machine_Name + (string.IsNullOrEmpty(entity.Machine_BACTA_Code) ? "" : " [" + entity.Machine_BACTA_Code + "]");
                        //title += " [" + Convert.ToString(entity.Machine_Name + "").Trim() + "]";        // Key_MachineParam

                        txtBarCurrentMachine.Text = entity.Machine_Name + (string.IsNullOrEmpty(entity.Machine_BACTA_Code) ? "" : " " + string.Format(this.GetResourceTextByKey("Key_MachineParam"), Convert.ToString(entity.Machine_BACTA_Code)));
                        title += " " + string.Format(this.GetResourceTextByKey("Key_MachineParam"), Convert.ToString(entity.Machine_Name));
                    }
                    if (string.IsNullOrEmpty(txtBarCurrentMachine.Text.Trim()))
                    {
                        txtBarCurrentMachine.Text = this.GetResourceTextByKey("Key_Empty");             // "[EMPTY]";
                        title += " " + this.GetResourceTextByKey("Key_Vacant");                        // " [Vacant]";
                    }

                    txtBarBreweryPositionCode.Text = entity.Bar_Position_Company_Position_Code + "";
                    TxtBarLocation.Text = entity.Bar_Position_Location + "";
                    txtBarPosPic.Text = entity.Bar_Position_Image_Reference + "";
                    txtCollectionPeriod.Text = entity.Bar_Position_Collection_Period + "";
                    txtBarSupplierSiteCode.Text = entity.Bar_Position_Supplier_Site_Code + "";
                    txtBarSupplierPositionCode.Text = entity.Bar_Position_Supplier_Position_Code + "";
                    txtBarPosTermsRentPrev.Text = String.Format("{0:###,##0.00}", entity.Bar_Position_Rent_Previous);
                    txtBarPosTermsRent.Text = String.Format("{0:###,##0.00}", entity.Bar_Position_Rent);
                    txtBarPosTermsRentFuture.Text = String.Format("{0:###,##0.00}", entity.Bar_Position_Rent_Future);
                    txtBarPosTermsSupplierPrev.Text = String.Format("{0:###,##0.00}", entity.Bar_Position_Supplier_Share_Previous);
                    txtBarPosTermsSupplier.Text = String.Format("{0:###,##0.00}", entity.Bar_Position_Supplier_Share);
                    txtBarPosTermsSupplierFuture.Text = String.Format("{0:###,##0.00}", entity.Bar_Position_Supplier_Share_Future);
                    txtBarPosTermsSitePrev.Text = String.Format("{0:###,##0.00}", entity.Bar_Position_Site_Share_Previous);
                    txtBarPosTermsSite.Text = String.Format("{0:###,##0.00}", entity.Bar_Position_Site_Share);
                    txtBarPosTermsSiteFuture.Text = String.Format("{0:###,##0.00}", entity.Bar_Position_Site_Share_Future);
                    txtBarPosTermsCompanyPrev.Text = String.Format("{0:###,##0.00}", entity.Bar_Position_Owners_Share_Previous);
                    txtBarPosTermsCompany.Text = String.Format("{0:###,##0.00}", entity.Bar_Position_Owners_Share);
                    txtBarPosTermsCompanyFuture.Text = String.Format("{0:###,##0.00}", entity.Bar_Position_Owners_Share_Future);
                    txtBarPosTermsSecBrewPrev.Text = String.Format("{0:###,##0.00}", entity.Bar_Position_Secondary_Owners_Share_Previous);
                    txtBarPosTermsSecBrew.Text = String.Format("{0:###,##0.00}", entity.Bar_Position_Secondary_Owners_Share);
                    txtBarPosTermsSecBrewFuture.Text = String.Format("{0:###,##0.00}", entity.Bar_Position_Secondary_Owners_Share_Future);
                    txtBarPosTermsLicencePrev.Text = String.Format("{0:###,##0.00}", entity.Bar_Position_Licence_Previous);
                    txtBarPosTermsLicence.Text = String.Format("{0:###,##0.00}", entity.Bar_Position_Licence_Charge);
                    txtBarPosTermsLicenceFuture.Text = String.Format("{0:###,##0.00}", entity.Bar_Position_Licence_Future);
                    BMC.EnterpriseClient.Helpers.frmAdminUtilities frmUtobj = new BMC.EnterpriseClient.Helpers.frmAdminUtilities();
                    CommonUtility cuobj = new CommonUtility();
                    frmUtobj.setListBox(cboZone, "", Convert.ToInt64(cuobj.VerifyValidNumberLong(Convert.ToString((entity.Zone_ID == null) || (entity.Zone_ID == 0) ? -1 : entity.Zone_ID))));
                    frmUtobj.setListBox(cboPositionoperator, "", Convert.ToInt64(cuobj.VerifyValidNumberLong(Convert.ToString((entity.Operator_ID == null) || (entity.Operator_ID == 0) ? -1 : entity.Operator_ID))));
                    frmUtobj.setListBox(cboPositionDepot, "", Convert.ToInt64(cuobj.VerifyValidNumberLong(Convert.ToString((entity.Depot_ID == null) || (entity.Depot_ID == 0) ? -1 : entity.Depot_ID))));
                    frmUtobj.setListBox(cboInvPeriod, "", Convert.ToInt64(cuobj.VerifyValidNumberLong(Convert.ToString(entity.Bar_Position_Invoice_Period == null ? 0 : entity.Bar_Position_Invoice_Period))));
                    frmUtobj.setListBox(cboMachineType, "", Convert.ToInt64(cuobj.VerifyValidNumberLong(Convert.ToString((entity.Machine_Type_ID == null) || (entity.Machine_Type_ID == 0) ? -1 : entity.Machine_Type_ID))));
                    //frmUtobj.setListBox(cboCollectionDay, "", Convert.ToInt64(cuobj.VerifyValidNumberLong(Convert.ToString(entity.Bar_Position_Collection_Day == null ? "0" : entity.Bar_Position_Collection_Day))));
                    cboCollectionDay.Text = entity.Bar_Position_Collection_Day;
                    chkTerminate.Checked = !string.IsNullOrEmpty(entity.Bar_Position_End_Date);
                    if (chkTerminate.Checked)
                    {
                        chkTerminate.Visible = true;
                        chkTerminate.Enabled = false;
                        //btnApply.Enabled = false; // Need to handle in parent
                        if (this.ParentForm is frmAdminBarPos)
                        {
                            (this.ParentForm as frmAdminBarPos).btnEnable = false;
                        }
                        chkEnable.Enabled = false;
                    }
                    sZoneName = cboZone.Text;
                    sCollectionDay = cboCollectionDay.Text;
                    sOperatorName = cboPositionoperator.Text;
                    sDepotName = cboPositionDepot.Text;
                    sMachineType = cboMachineType.Text;
                    if (!string.IsNullOrEmpty(entity.Bar_Position_Image_Reference) && !string.IsNullOrEmpty(entity.Bar_Position_Image_Reference) && File.Exists(entity.Bar_Position_Image_Reference))
                        ImgBarPosPic.Load(entity.Bar_Position_Image_Reference);
                    //else
                    //    ImgBarPosPic.Load();

                    //Commented out since all data(primitive types) are added at runtime not from database.                    
                    //if (!Convert.ToBoolean(entity.Bar_Position_Price_Per_Play_Default))
                    //    frmUtobj.setListBox(cboPrice, "", Convert.ToInt64(-1));
                    //else
                    //    frmUtobj.setListBox(cboPrice, "", Convert.ToInt64(cuobj.VerifyValidNumberLong(Convert.ToString(entity.Bar_Position_Price_Per_Play == null ? "0" : entity.Bar_Position_Price_Per_Play))));

                    //if (!Convert.ToBoolean(entity.Bar_Position_Jackpot_Default))
                    //    frmUtobj.setListBox(cboJackpot, "", Convert.ToInt64(-1));
                    //else
                    //    frmUtobj.setListBox(cboJackpot, "", Convert.ToInt64(entity.Bar_Position_Jackpot));


                    //if (!Convert.ToBoolean(entity.Bar_Position_Percentage_Payout_Default))
                    //    frmUtobj.setListBox(cboPayout, "", Convert.ToInt64(-1));
                    //else
                    //    frmUtobj.setListBox(cboPayout, "", Convert.ToInt64(cuobj.VerifyValidNumberLong(Convert.ToString(entity.Bar_Position_Percentage_Payout == null ? "0" : entity.Bar_Position_Percentage_Payout))));


                    //if (!Convert.ToBoolean(entity.Terms_Group_ID_Default))
                    //{
                    //    frmUtobj.setListBox(cboTermGroupPrevious, "", Convert.ToInt64(-1));
                    //    frmUtobj.setListBox(cboTermsGroup, "", Convert.ToInt64(-1));
                    //    frmUtobj.setListBox(cboGroupFuture, "", Convert.ToInt64(-1));
                    //}
                    //else
                    //{
                    //    frmUtobj.setListBox(cboTermGroupPrevious, "", Convert.ToInt64(cuobj.VerifyValidNumberLong(Convert.ToString(entity.Terms_Group_Past_ID == null ? 0 : entity.Terms_Group_Past_ID))));
                    //    frmUtobj.setListBox(cboTermsGroup, "", Convert.ToInt64(cuobj.VerifyValidNumberLong(Convert.ToString(entity.Terms_Group_ID == null ? 0 : entity.Terms_Group_ID))));
                    //    frmUtobj.setListBox(cboGroupFuture, "", Convert.ToInt64(cuobj.VerifyValidNumberLong(Convert.ToString(entity.Terms_Group_Future_ID == null ? 0 : entity.Terms_Group_Future_ID))));

                    //}

                    //if (!Convert.ToBoolean(entity.Access_Key_ID_Default))
                    //    frmUtobj.setListBox(cboAccessKey, "", Convert.ToInt64(-1));
                    //else
                    //    frmUtobj.setListBox(cboAccessKey, "", Convert.ToInt64(cuobj.VerifyValidNumberLong(Convert.ToString(entity.Access_Key_ID == null ? 0 : entity.Access_Key_ID))));

                    chkBarValidateDataThroughTerms.Checked = Convert.ToBoolean(entity.Bar_Position_Use_Terms);
                    chkChangeFromScheduleToRent.Checked = Convert.ToBoolean(entity.Bar_Position_Override_Rent_From_Schedule_To_Rent);

                    chkChangeFromRentToSchedule.Checked = Convert.ToBoolean(entity.Bar_Position_Override_Rent_From_Rent_To_Schedule);
                    chkUseBarPosRentSchedule.Checked = Convert.ToBoolean(entity.Bar_Position_Override_Rent_Schedule);

                    chkUseBarPosRent.Checked = Convert.ToBoolean(entity.Bar_Position_Override_Rent);


                    DTBarPosChangeFromScheduleToRent.Text = entity.Bar_Position_Override_Rent_From_Schedule_To_Rent_Date;

                    DTBarPosChangeFromRentToSchedule.Text = entity.Bar_Position_Override_Rent_From_Rent_To_Schedule_Date;

                    chkUseBarPosShares.Checked = Convert.ToBoolean(entity.Bar_Position_Override_Shares);
                    chkUseBarPosLicence.Checked = Convert.ToBoolean(entity.Bar_Position_Override_Licence);

                    DTBarPosRentChangePrev.Text = entity.Bar_Position_Rent_Past_Date;
                    DTBarPosRentChangeFuture.Text = entity.Bar_Position_Rent_Future_Date;

                    DTBarPosSharesChangePrev.Text = entity.Bar_Position_Share_Past_Date;

                    DTBarPosSharesChangeFuture.Text = entity.Bar_Position_Share_Future_Date;

                    DTBarPosLicenceChangePrev.Text = entity.Bar_Position_Licence_Past_Date;

                    DTBarPosLicenceChangeFuture.Text = entity.Bar_Position_Licence_Future_Date;


                    chkUseSiteShareForSecondaryBrewery.Checked = Convert.ToBoolean(entity.Bar_Position_Use_Site_Share_For_Secondary_Brewery);
                    chkPrizeLOS.Checked = Convert.ToBoolean(entity.Bar_Position_Prize_LOS);
                    frmUtobj.setListBox(cboRentSchedule, "", Convert.ToInt64(cuobj.VerifyValidNumberLong(Convert.ToString(entity.Bar_Position_Rent_Schedule_ID == null ? 0 : entity.Bar_Position_Rent_Schedule_ID))));
                    frmUtobj.setListBox(cboFromRentSchedule, "", Convert.ToInt64(cuobj.VerifyValidNumberLong(Convert.ToString(entity.Bar_Position_Rent_Schedule_ID_From == null ? 0 : entity.Bar_Position_Rent_Schedule_ID_From))));
                    //frmUtobj.setListBox(cboShareSchedule, "", Convert.ToInt64(cuobj.VerifyValidNumberLong(Convert.ToString(entity.Bar_Position_Override_Share_Schedule == null ? 0:entity.Bar_Position_Override_Share_Schedule))));


                    chkUseBarPosShareSchedule.Checked = Convert.ToBoolean(entity.Bar_Position_Override_Share_Schedule);

                    chkDisableEDIExport.Checked = Convert.ToBoolean(entity.Bar_Position_Disable_EDI_Export);
                    txtBarPosPic.Text = entity.Bar_Position_Image_Reference;

                    if (SettingsEntity.AllowEnableDisableBarPosition)
                        chkEnable.Checked = Convert.ToBoolean(entity.Bar_Position_IsEnable);
                    else
                    {
                        chkEnable.Checked = Convert.ToBoolean(entity.Bar_Position_IsEnable);
                        chkEnable.Visible = false;
                    }

                    if (!string.IsNullOrEmpty(txtBarPosPic.Text))
                    {
                        BarPositionExtensionEntity extension = objZoneBiz.GetBarPositionExtension(BarPositionID);
                        if (extension != null)
                        {
                            Binary array = extension.Bar_Position_Image;
                            MemoryStream stream = new MemoryStream(array.ToArray());
                            ImgBarPosPic.Image = Image.FromStream(stream);
                            ImgBarPosPic.Load();
                        }
                    }
                    else
                    {
                        ImgBarPosPic.Image = null;
                    }
                    cboPayout.SelectedItem = !string.IsNullOrEmpty(entity.Bar_Position_Percentage_Payout) ? cboPayout.Items.OfType<string>().FirstOrDefault(x => x == entity.Bar_Position_Percentage_Payout) : cboPayout.Items.OfType<string>().FirstOrDefault();
                    cboPrice.SelectedItem = !string.IsNullOrEmpty(entity.Bar_Position_Price_Per_Play) ? cboPrice.Items.OfType<string>().FirstOrDefault(x => x == entity.Bar_Position_Price_Per_Play) : cboPrice.Items.OfType<string>().FirstOrDefault();
                    cboJackpot.SelectedItem = !string.IsNullOrEmpty(entity.Bar_Position_Jackpot) ? cboJackpot.Items.OfType<string>().FirstOrDefault(x => x == entity.Bar_Position_Jackpot) : cboJackpot.Items.OfType<string>().FirstOrDefault();
                    cboAccessKey.SelectedIndex = entity.Access_Key_ID.HasValue ? entity.Access_Key_ID.Value : 1;

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SetTagProperty()
        {
            try
            {
                this.lblname.Tag = "Key_PositionNameMandatory";
                this.chkTerminate.Tag = "Key_QMarkTerminated";
                this.lblPosName.Tag = "Key_3rdPartyPositionCode";
                this.lblAccessKey.Tag = "Key_AccessKeyColon";
                this.btnPriceApply.Tag = "Key_Apply";
                this.btnJackpotApply.Tag = "Key_Apply";
                this.btnPayoutApply.Tag = "Key_Apply";
                this.btnAccessKey.Tag = "Key_Apply";
                this.btnTermsApply.Tag = "Key_Apply";
                this.btnImportMovementsApply.Tag = "Key_Apply";
                this.btnImportCollectionsApply.Tag = "Key_Apply";
                this.btnImportEDCApply.Tag = "Key_Apply";
                this.btnExportMovementsApply.Tag = "Key_Apply";
                this.btnExportCollectionsApply.Tag = "Key_Apply";
                this.btnExportEDCApply.Tag = "Key_Apply";
                this.lblCategory.Tag = "Key_CategoryColon";
                this.btnBarPosPicChange.Tag = "Key_Change";
                this.lblChangeDate.Tag = "Key_ChangeDateColon";
                this.lblChangeDate1.Tag = "Key_ChangeDateColon";
                this.chkChangeFromScheduleToRent.Tag = "Key_ChangeFrom";
                this.lblDays.Tag = "Key_Collectedeverydays";
                this.lblCollection.Tag = "Key_CollectionDayColon";
                this.label6.Tag = "Key_CompanyColon";
                this.lblCurrent.Tag = "Key_Current";
                this.lblMachine.Tag = "Key_CurrentMachineColon";
                this.tabPage5.Tag = "Key_Data";
                this.tabPage2.Tag = "Key_Default";
                this.tbDetails.Tag = "Key_Details";
                this.chkDisableEDIExport.Tag = "Key_DisableEDIExport";
                this.chkEnable.Tag = "Key_Enable";
                this.lblExportCollections.Tag = "Key_ExportCollectionsColon";
                this.lblExportEDC.Tag = "Key_ExportEDCColon";
                this.lblExportMovements.Tag = "Key_ExportMovementsColon";
                this.lblFuture.Tag = "Key_Future";
                this.tabPage4.Tag = "Key_Images";
                this.label2.Tag = "Key_ImportCollectionsColon";
                this.lblImportEDC.Tag = "Key_ImportEDCColon";
                this.lblImportMove.Tag = "Key_ImportMovementsColon";
                this.lblInvoice.Tag = "Key_InvoicePeriodColon";
                this.lblItems.Tag = "Key_Item";
                this.lblItem.Tag = "Key_Items";
                this.lblJackpot.Tag = "Key_JackPotColon";
                this.chkUseBarPosLicence.Tag = "Key_Licence";
                this.lblType.Tag = "Key_MachineTypeColon";
                this.lblOpCode.Tag = "Key_OperatorPositionCodeColon";
                this.lblSiteCode.Tag = "Key_OperatorSiteCodeColon";
                this.lblDepot.Tag = "Key_PositionDepotColon";
                this.lblDescribtion.Tag = "Key_PositionDescriptionColon";
                this.lblPosition.Tag = "Key_PositionImageColon";
                this.lblPosOperator.Tag = "Key_PositionOperator";
                this.lblPrevious.Tag = "Key_Previous";
                this.lblPrice.Tag = "Key_PricePerPlayColon";
                this.chkPrizeLOS.Tag = "Key_PrizemoneyLOSoverrideterms";
                this.chkUseBarPosRentScheduleFrom.Tag = "Key_RentSchedule";
                this.chkUseBarPosRentSchedule.Tag = "Key_RentSchedule";
                this.tabPage6.Tag = "Key_Schedules";
                this.label8.Tag = "Key_SecBrewColon";
                this.chkUseBarPosShareSchedule.Tag = "Key_ShareSchedule";
                this.chkUseBarPosShares.Tag = "Key_Shares";
                this.label7.Tag = "Key_SiteColon";
                this.lblSupplier.Tag = "Key_SupplierColon";
                this.tabPage3.Tag = "Key_Terms";
                this.lblTermGroupChangeOver.Tag = "Key_TermsGroupChangeoverDate";
                this.label13.Tag = "Key_TermsGroupChangeoverDate";
                this.label14.Tag = "Key_TermsGroupFuture";
                this.lblTermGroupPrevious.Tag = "Key_TermsGroupPrevious";
                this.label12.Tag = "Key_TermsGroupColon";
                this.lblSchedules.Tag = "Key_RouteApplicateToSiteMsg";
                this.chkUseSiteShareForSecondaryBrewery.Tag = "Key_UseSiteShareForSecondaryBrewery";
                this.chkBarValidateDataThroughTerms.Tag = "Key_Validateincomingdatathroughterms";
                this.label15.Tag = "Key_Value";
                this.lblValues.Tag = "Key_Values";
                this.lblZone.Tag = "Key_ZoneColon";
                cboZone.Items.Add(this.GetResourceTextByKey("Key_CollectionDaysAny"));
                cboCollectionDay.Items.Add(this.GetResourceTextByKey("Key_CollectionDaysAny"));
                cboCollectionDay.Items.Add(this.GetResourceTextByKey("Key_Monday"));
                cboCollectionDay.Items.Add(this.GetResourceTextByKey("Key_Tuesday"));
                cboCollectionDay.Items.Add(this.GetResourceTextByKey("Key_Wednesday"));
                cboCollectionDay.Items.Add(this.GetResourceTextByKey("Key_Thursday"));
                cboCollectionDay.Items.Add(this.GetResourceTextByKey("Key_Friday"));
                cboCollectionDay.Items.Add(this.GetResourceTextByKey("Key_Saturday"));
                cboCollectionDay.Items.Add(this.GetResourceTextByKey("Key_Sunday"));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion

        private void btnApplyClick(object sender, EventArgs e)
        {
            bool isJackpot = false,
                 isAccessKey = false,
                 isPercentPayout = false,
                 isPricePerPlay = false,
                 isTermsGroup = false;
            int? value = null;

            switch ((sender as Button).Name)
            {
                case "btnPriceApply":
                    isPricePerPlay = true;
                    value = Convert.ToInt32(txtPPP.Text);
                    break;
                case "btnJackpotApply":
                    isJackpot = true;
                    value = Convert.ToInt32(txtJP.Text);
                    break;
                case "btnPayoutApply":
                    isPercentPayout = true;
                    value = Convert.ToInt32(txtPerc.Text);
                    break;
                case "btnAccessKey":
                    isAccessKey = true;
                    value = cboAccessKey.SelectedIndex > 1 ? Convert.ToInt32(txtAKey.Text) : 0;
                    break;
                case "btnTermsApply":
                    isTermsGroup = true;
                    value = 0;
                    break;
            }
            if (value.HasValue)
            {
                barPosBiz.UpdateBarPositionTermsGroup(
                    _BarPositionID,
                    _SiteId,
                    isPricePerPlay,
                    value.Value,
                    isJackpot,
                    value.Value,
                    isPercentPayout,
                    value.Value,
                    isAccessKey,
                    value.Value,
                    isTermsGroup,
                    Convert.ToInt32(cboTermsGroup.SelectedValue),
                    Convert.ToInt32(cboTermGroupPrevious.SelectedValue),
                    Convert.ToInt32(cboGroupFuture.SelectedValue),
                    dtpPreviousChangeOverDate.Value,
                    dtpFutureChangeOverDate.Value,
                    AppEntryPoint.Current.UserId,
                    AppEntryPoint.Current.UserName,
                    ModuleNameEnterprise.SGVIFinancial.ToString(),
                    (int)ModuleNameEnterprise.SGVIFinancial);
            }
        }

        private void cboDefaultsSelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmbDefault = sender as ComboBox;
            if (cmbDefault != null)
            {
                switch (cmbDefault.Name)
                {
                    case "cboPrice":
                        txtPPP.Text = cmbDefault.SelectedIndex > 0 ? cmbDefault.Text : "0";
                        break;
                    case "cboJackpot":
                        txtJP.Text = cmbDefault.SelectedIndex > 0 ? cmbDefault.Text : "0";
                        break;
                    case "cboPayout":
                        txtPerc.Text = cmbDefault.SelectedIndex > 0 ? cmbDefault.Text : "0";
                        break;
                    case "cboAccessKey":
                        txtAKey.Text = cmbDefault.SelectedIndex > 0 || cmbDefault.SelectedIndex > 1 ? "None" : cmbDefault.Text;
                        break;
                    case "cboTermGroupPrevious":
                        txtTPPrev.Text = cmbDefault.Text;
                        break;
                    case "cboTermsGroup":
                        txtTP.Text = cmbDefault.Text;
                        break;
                    case "cboGroupFuture":
                        txtTPFut.Text = cmbDefault.Text;
                        break;
                }
            }
        }
    }
}



