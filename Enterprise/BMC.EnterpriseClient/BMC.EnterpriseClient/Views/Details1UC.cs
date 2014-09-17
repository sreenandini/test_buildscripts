using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseClient.Helpers;
using BMC.EnterpriseClient.Views;
using BMC.Common.LogManagement;
using System.IO;
using BMC.SecurityVB;
using BMC.EnterpriseClient;
using BMC.Common.ExceptionManagement;
using BMC.Common;

namespace Details1
{
    public partial class Details1UC : UserControl, IAdminSite
    {        
        int _SiteID = 0;
        string _SiteCode = String.Empty; 
        long DepotChanged = 0;        
        //bool CustomerAccessDepotAll = false;
        SiteDetails sdobj = new SiteDetails();
        CommonUtility cuobj = new CommonUtility();
        frmAdminUtilities frmUtobj = new frmAdminUtilities();
        int indexRegion = 0;
        bool _bIsCrossTicketingEnabled = false;
        
        string strSiteName;
      
        public Details1UC(AdminSiteEntity objDetailFields)
        {
            InitializeComponent();
            setTagProperty();
        }

        private void setTagProperty()
        {
            this.label5.Tag = "Key_AddressColonMandatoryColon";
            this.label2.Tag = "Key_CodeMandatory";
            this.label1.Tag = "Key_NameMandatory";
            this.label9.Tag = "Key_PostZipCodeMandatory";
            this.label17.Tag = "Key_SiteLanguageMandatory";
            this.lblStackerPercentageLimit.Tag = "Key_StackerPerclimitMandatory";
            this.FrmSiteClassAdmin.Tag = "Key_Admin";
            this.label11.Tag = "Key_AreaInSqM";
            this.label10.Tag = "Key_CadastreCodeColon";
            this.label18.Tag = "Key_ClassificationColon";
            this.btnCrossTicketingConfig.Tag = "Key_CrossTicketingConfig";
            this.ChkCrossTicketing.Tag = "Key_CrossTicketingEnabled";
            this.label21.Tag = "Key_DateClosedColon";
            this.label15.Tag = "Key_DepotColon";
            this.label20.Tag = "Key_EMailColon";
            this.label13.Tag = "Key_FaxNumberColon";
            this.label19.Tag = "Key_ManagerColon";
            this.label8.Tag = "Key_MunicipalityColon";
            this.chkNonCashable.Tag = "Key_NonCashableVoucher";
            this.label4.Tag = "Key_OperatorCodeColon";
            this.label14.Tag = "Key_OperatorColon";
            this.label12.Tag = "Key_PhoneNumberColon";
            this.label7.Tag = "Key_ProvinceColon";
            this.chkSiteClosed.Tag = "Key_SiteClosed";
            this.label3.Tag = "Key_SiteOwnersCode";
            this.label6.Tag = "Key_StreetNumberColon";
            this.chkTITO.Tag = "Key_TITOEnabled";
            this.label16.Tag = "Key_TypeofTradeColon";

        }
        public Details1UC()
        {
            InitializeComponent();
            setTagProperty();
        }

        //public string SiteName
        //{
        //    get { return txtName.Text.Trim(); }
        //    set { txtName.Text = value; }
        //}
        //public string SiteCode
        //{
        //    get { return txtCode.Text.Trim(); }
        //    set { txtCode.Text = value; }
        //}
        //public string SiteOwnersCode
        //{
        //    get { return txtSiteOwnerSiteCode.Text.Trim(); }
        //    set { txtSiteOwnerSiteCode.Text = value; }
        //}
        //public string SiteSupplierCode
        //{
        //    get { return txtSupplierSiteCode.Text.Trim(); }
        //    set { txtSupplierSiteCode.Text = value; }
        //}
        //public string SiteAddress1
        //{
        //    get { return txtAddress1.Text.Trim(); }
        //    set { txtAddress1.Text = value; }
        //}
        //public string SiteAddress2
        //{
        //    get { return txtAddress2.Text.Trim(); }
        //    set { txtAddress2.Text = value; }
        //}
        //public string SiteAddress3
        //{
        //    get { return txtAddress3.Text.Trim(); }
        //    set { txtAddress3.Text = value; }
        //}
        //public string SiteAddress4
        //{
        //    get { return txtAddress4.Text.Trim(); }
        //    set { txtAddress4.Text = value; }
        //}
        //public string SiteStreetNumber
        //{
        //    get { return txtStreetNo.Text.Trim(); }
        //    set { txtStreetNo.Text = value; }
        //}
        //public string SiteProvince
        //{
        //    get { return txtProvince.Text.Trim(); }
        //    set { txtProvince.Text = value; }
        //}
        //public string SiteMunicipality
        //{
        //    get { return txtMuncipality.Text.Trim(); }
        //    set { txtMuncipality.Text = value; }
        //}
        //public string SitePostcode
        //{
        //    get { return txtPostcode.Text.Trim(); }
        //    set { txtPostcode.Text = value; }
        //}
        //public string SiteCadastralCode
        //{
        //    get { return txtCadastralCode.Text.Trim(); }
        //    set { txtCadastralCode.Text = value; }
        //}
        //public string SiteArea
        //{
        //    get { return txtArea.Text.Trim(); }
        //    set { txtArea.Text = value; }
        //}
        //public string SitePhoneNo
        //{
        //    get { return txtPhoneNumber.Text.Trim(); }
        //    set { txtPhoneNumber.Text = value; }
        //}
        //public string SiteFaxNo
        //{
        //    get { return txtFaxNumber.Text.Trim(); }
        //    set { txtFaxNumber.Text = value; }
        //}
        //public bool IsTITO
        //{
        //    get { return chkTITO.Checked; }
        //    set { chkTITO.Checked = value; }
        //}
        //public bool IsNonCashVoucher
        //{
        //    get { return chkNonCashable.Checked; }
        //    set { chkNonCashable.Checked = value; }
        //}
        //public bool IsCrossTicketingChecked
        //{
        //    get { return ChkCrossTicketing.Checked; }
        //    set { ChkCrossTicketing.Checked = value; }
        //}        

        //public string Depot
        //{
        //    get { return lstDepot.SelectedItem.ToString(); }
        //    set { lstDepot.SelectedItem = value; }
        //}

        ////To Send the Depot Items to Details2UC
        ////public ComboBox.ObjectCollection MyItems
        ////{
        ////    get { return lstDepot.Items; }             
        ////}

        //public string TradeType
        //{
        //    get { return lstTypeOfTrade.SelectedItem.ToString(); }
        //    set { lstTypeOfTrade.SelectedItem = value; }
        //}        
        //public int TradeTypeCount
        //{
        //    get { return lstTypeOfTrade.Items.Count; }            
        //}
        //public int TradeIndex
        //{
        //    get { return lstTypeOfTrade.SelectedIndex; }
        //    set { lstTypeOfTrade.SelectedIndex = value; }
        //}   
        //public string SiteLanguage
        //{
        //    get { return lstRegion.SelectedItem.ToString(); } 
        //    set { lstRegion.SelectedItem = value; }
        //}
        //public int SiteLanguageIndex
        //{
        //    get { return lstRegion.SelectedIndex; }
        //    set { lstRegion.SelectedIndex = value;}
        //}
        //public string Classification
        //{
        //    get { return lstClassification.SelectedItem.ToString(); }
        //    set { lstClassification.SelectedItem = value; }
        //}
        //public string Manager
        //{
        //    get { return txtManager.Text.Trim(); }
        //    set { txtManager.Text = value; }
        //}
        //public string Email
        //{
        //    get { return txtEmail.Text.Trim(); }
        //    set { txtEmail.Text = value; }
        //}        
        //public bool SiteClosed
        //{
        //    get { return chkSiteClosed.Checked; }
        //    set { chkSiteClosed.Checked = value; }
        //}
        //public DateTime DateClosed
        //{
        //    get { return dtPickerClosureDate.Value; }
        //    set { dtPickerClosureDate.Value = value; }
        //}
        ////////****************Structure Change Ends********************************************//////////////

        private void Details1UC_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
            _bIsCrossTicketingEnabled = SettingsEntity.IsCrossTicketingEnabled;
        }

        private Delegate _Detail1UC;

        public Delegate DlgDetail1UC { get { return _Detail1UC; } set { _Detail1UC = value; } }

        private void lstsupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Win32Extensions.IsInDesignMode()) return;
                lstDepot.Refresh();
                if (lstsupplier.SelectedIndex != -1)
                {
                    if (AppEntryPoint.Current.CustomerAccessViewAllDepot == true || AppGlobals.Current.HasUserAccess("HQ_Customer_Access_View_Entire_Database") == true)
                    {
                        List<AdminSiteEntity> objDepot = sdobj.GetDepot(((AdminSiteEntity)lstsupplier.SelectedItem).Operator_ID);
                        if (objDepot != null)
                        {
                            lstDepot.DataSource = objDepot;
                            lstDepot.DisplayMember = "Depot_Name";
                            lstDepot.ValueMember = "Depot_ID";
                            lstDepot.SelectedIndex = -1;
                            if (DlgDetail1UC != null)
                                DlgDetail1UC.DynamicInvoke(objDepot);
                        }
                    }
                    else if (AppEntryPoint.Current.StaffId != 0 && (((AdminSiteEntity)lstsupplier.SelectedItem).Operator_ID != 0))
                    {
                        List<AdminSiteEntity> objDepot = sdobj.GetCustomerAccessDepot(((AdminSiteEntity)lstsupplier.SelectedItem).Operator_ID, AppEntryPoint.Current.StaffId);
                        if (objDepot != null)
                        {
                            lstDepot.DataSource = objDepot;
                            lstDepot.DisplayMember = "Depot_Name";
                            lstDepot.ValueMember = "Depot_ID";
                            lstDepot.SelectedIndex = -1;
                            if (DlgDetail1UC != null)
                                DlgDetail1UC.DynamicInvoke(objDepot);
                        }
                    }

                }
                else
                {
                    lstDepot.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void chkTITO_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTITO.Checked)
            {
                chkNonCashable.Enabled = true;
                chkNonCashable.Visible = true;
                chkNonCashable.Checked = true;

                ChkCrossTicketing.Enabled = true && _bIsCrossTicketingEnabled;
                ChkCrossTicketing.Visible = true && _bIsCrossTicketingEnabled;
            }
            else
            {
                chkNonCashable.Enabled = false;
                chkNonCashable.Visible = false;
                chkNonCashable.Checked = false;

                ChkCrossTicketing.Enabled = false;
                ChkCrossTicketing.Visible = false;
                ChkCrossTicketing.Checked = false;

                btnCrossTicketingConfig.Enabled = false;
                btnCrossTicketingConfig.Visible = false;
            }
        }

        private void ChkCrossTicketing_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkCrossTicketing.Checked)
            {
                btnCrossTicketingConfig.Visible = true;
                btnCrossTicketingConfig.Enabled = true;
            }
            else
            {
                btnCrossTicketingConfig.Enabled = false;
            }
        }

        private void btnCrossTicketingConfig_Click(object sender, EventArgs e)
        {
            if (_SiteID == 0)
            {
                Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_UC_DETAILS_CONF"), this.ParentForm.Text);
                return;
            }
            
            try
            {
                if (lstClassification.SelectedIndex >= 0)
                {
                    TicketingConfig fTicketingConfig = new TicketingConfig(txtCode.Text);
                    fTicketingConfig.ShowDialog();
                }
                else
                {
                    frmAdminSiteClass AdminSiteClass = new frmAdminSiteClass();
                    AdminSiteClass.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void FrmSiteClassAdmin_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstClassification.SelectedIndex >= 0)
                {
                    frmAdminSiteClass AdminSiteClass = new frmAdminSiteClass(Convert.ToInt32(lstClassification.SelectedValue));
                    AdminSiteClass.ShowDialog();
                }
                else
                {
                    frmAdminSiteClass AdminSiteClass = new frmAdminSiteClass();
                    AdminSiteClass.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public bool Validate_Input()
        {
            try
            {
                if ((txtName.Text).Trim() == "")
                {
                    Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_UC_DETAILS_SITE_NAME"), this.ParentForm.Text);
                    txtName.Select();
                    return false;
                }

                if ((txtCode.Text).Trim() == "")
                {
                    Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_UC_DETAILS_SITE_CODE"), this.ParentForm.Text);
                    txtCode.Select();
                    return false;
                }
                else
                {
                    int value = 0;
                    if (!Int32.TryParse((txtCode.Text).Trim(), out value))
                    {
                        Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_UC_DETAILS_INVALID"), this.ParentForm.Text);
                        txtCode.Select();
                        return false;
                    }                   
                    if (Convert.ToInt32(txtCode.Text.Trim()) < 1000)
                    {
                        Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_UC_DETAILS_INVLAID1"), this.ParentForm.Text);
                        txtCode.Select();
                        return false;
                    }
                    else
                    {
                        AdminSiteEntity objSiteAvail = null;
                        objSiteAvail = sdobj.CheckSite(txtCode.Text, _SiteID); //SiteID to be Passed
                        if (objSiteAvail != null)
                        {
                            Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_UC_DETAILS_INVLAID2"), this.ParentForm.Text);
                            txtCode.Focus();
                                return false;
                            }
                        }
                    }

                if (txtAddress1.Text.Trim() == "" && txtAddress2.Text == "" && txtAddress3.Text == "" && txtAddress4.Text == "")
                {
                    Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_UC_DETAILS_ADDRESS_VALIDATION"), this.ParentForm.Text);
                    txtAddress1.Select();
                    return false;
                }
                if (txtAddress1.Text.Trim() == "")
                {
                    Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_UC_DETAILS_ADDRESS_VALIDATION1"), this.ParentForm.Text);
                    txtAddress1.Select();
                    return false;
                }
                if (txtAddress2.Text.Trim() == "")
                {
                    Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_UC_DETAILS_ADDRESS_VALIDATION2"), this.ParentForm.Text);
                    txtAddress2.Select();
                    return false;
                }
                if (txtPostcode.Text.Trim() == "")
                {
                    Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_UC_DETAILS_ZIP"), this.ParentForm.Text);
                    txtPostcode.Select();
                    return false;
                }
                if (!txtEmail.Text.ValidateEmail() && txtEmail.Text != "")
                {
                    this.ShowErrorMessageBox(this.GetResourceTextByKey(1, "MSG_ENT_UC_DETAILS_EMAIL"), this.ParentForm.Text);
                    txtEmail.Select();
                    return false;
                }

                if (lstsupplier.SelectedIndex > -1 && lstDepot.SelectedIndex == -1)
                {
                    Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_UC_DETAILS_DEPOT"), this.ParentForm.Text);
                    lstDepot.Select();
                    return false;
                }
                if (lstRegion.SelectedIndex == -1)
                {
                    Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_UC_DETAILS_LANG"), this.ParentForm.Text);
                    lstRegion.Select();
                    lstRegion.Focus();
                    return false;
                }
                string StackerFeature = string.Empty;

                try
                {
                    sdobj.GetSetting(null, "StackerFeature", "FALSE", ref StackerFeature);
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("Error in Setting check for Stacker in Valid Input method in Detail1" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);
                }

                if (StackerFeature.Trim().ToUpper().Equals("TRUE"))
                {
                    if (txtStackerLimit.Text.Trim() == "")
                    {
                        Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_UC_DETAILS_STACKER_VALID"), this.ParentForm.Text);
                        txtStackerLimit.Select();
                        return false;
                    }
                    int StackerValid = 0;
                    StackerValid = Convert.ToInt32(txtStackerLimit.Value);
                    if (StackerValid <= 0 || StackerValid > 100)
                    {
                        Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_UC_DETAILS_STACKER_VALID1"), this.ParentForm.Text);
                        txtStackerLimit.Select();
                        return false;
                    }
                    if (!txtStackerLimit.Text.IsNumeric())
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ENT_UC_DETAILS_NUMERIC_VALUE"), this.ParentForm.Text);
                        txtStackerLimit.Select();
                        return false;
                    }
                }
                if (chkSiteClosed.Checked == true)
                {
                    AdminSiteEntity ent = sdobj.GetActiveInstallationsForSite(_SiteID);
                    if (_SiteID != 0 && ent != null && ent.Bar_Position_ID != null && ent.Bar_Position_ID != 0)
                    {
                        Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_UC_DETAILS_INSTALLATION"), this.ParentForm.Text);
                        chkSiteClosed.Checked = false;
                        chkSiteClosed.Focus();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return true;
        }

        public bool InsertEH_CrossTicketing()
        {
            try
            {
                AdminSiteEntity objSiteTicketingURL = null;
                if (ChkCrossTicketing.Checked == true)
                {
                    sdobj.InsertExportHistoryCrossTicketing(_SiteCode);
                    objSiteTicketingURL = sdobj.CheckSiteTicketingURL(_SiteCode);

                    if (objSiteTicketingURL == null)
                    {
                        Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_UC_DETAILS_CROSS_CONFIG"), this.ParentForm.Text);
                        ChkCrossTicketing.Checked = false;
                        return false;  // Must include the return type if anything else than void
                    }
                }
                else
                {
                    sdobj.InsertExportHistoryCrossTicketing(_SiteCode);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return true;
        }

        private void chkSiteClosed_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSiteClosed.Checked)
            {
                dtPickerClosureDate.Enabled = false;
                dtPickerClosureDate.Value = DateTime.Now;
            }
            else
            {
                dtPickerClosureDate.Enabled = false;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #region IAdminSite Members

        public void LoadDetails(AdminSiteEntity entity)
        {
            try
            {
                _SiteID = entity.Site_ID;
                _SiteCode = entity.Site_Code;
                OnLoadSiteDetail1();

                txtName.Text = entity.Site_Name + "";

                strSiteName = entity.Site_Name + ""; // STRING

                txtCode.Text = entity.Site_Code + "";
                txtSiteOwnerSiteCode.Text = entity.Site_Company_Code + "";
                txtSupplierSiteCode.Text = entity.Site_Supplier_Code + "";
                txtPhoneNumber.Text = entity.Site_Phone_No + "";
                txtFaxNumber.Text = entity.Site_Fax_No + "";
                txtAddress1.Text = entity.Site_Address_1 + "";
                txtAddress2.Text = entity.Site_Address_2 + "";
                txtAddress3.Text = entity.Site_Address_3 + "";
                txtAddress4.Text = entity.Site_Address_4 + "";
                txtPostcode.Text = (entity.Site_Postcode + "").ToUpper();
                txtEmail.Text = entity.Site_Email_Address + "";

                txtStreetNo.Text = entity.Site_Street_Number + "";
                txtProvince.Text = entity.Site_Province + "";
                txtMuncipality.Text = entity.Site_Municipality + "";
                txtCadastralCode.Text = entity.Site_Cadastral_Code + "";
                txtArea.Text = entity.Site_Area + "";
                txtManager.Text = entity.Site_Manager + "";
                txtStackerLimit.Text = entity.StackerLimitPercentage + "0";
                entity.Site_Area = (entity.Site_Area == null) ? 0 : entity.Site_Area;
                frmUtobj.setListBox(lstsupplier, "", Convert.ToInt64(cuobj.VerifyValidNumberLong(entity.Supplier_ID.ToString())));
                frmUtobj.setListBox(lstDepot, "", Convert.ToInt64(cuobj.VerifyValidNumberLong(entity.Depot_ID.ToString())));
                frmUtobj.setListBox(lstClassification, "", Convert.ToInt64(cuobj.VerifyValidNumberLong(entity.Site_Classification_ID.ToString())));

                if (entity.Site_Closed == null || entity.Site_Closed == 0)
                {
                    chkSiteClosed.Checked = false;
                }
                else
                {
                    chkSiteClosed.Checked = true;
                    entity.Site_Closed_Date = Convert.ToString(dtPickerClosureDate.Value);
                }

                lstTypeOfTrade.SelectedIndex = -1; // lstTypeOfTrade.ListIndex = -1

                if (entity.Site_Trade_Type != null || entity.Site_Trade_Type != "")
                {
                    string Site_Trade_Type;
                    string TempTradeType;
                    Site_Trade_Type = entity.Site_Trade_Type;

                    lstTypeOfTrade.SelectedValue = Site_Trade_Type;

                }
                else
                {
                    lstTypeOfTrade.SelectedIndex = 0;
                }

                lstRegion.SelectedIndex = -1;

                if (_SiteID != 0)
                {
                    if (entity.Region.IndexOf("US") >= 0)
                    {
                        lstRegion.SelectedIndex = 3;
                    }
                    //else if (entity.Region.IndexOf("GB") >= 0)
                    //{
                    //    lstRegion.SelectedIndex = 0;
                    //}
                    else if (entity.Region.IndexOf("UK") >= 0)
                    {
                        lstRegion.SelectedIndex = 2;
                    }
                    else if (entity.Region.IndexOf("AR") >= 0)
                    {
                        lstRegion.SelectedIndex = 0;
                    }
                    else if(entity.Region.IndexOf("IT")>=0)
                    {
                        lstRegion.SelectedIndex = 1;
                    }
					else
                    {
                        lstRegion.SelectedIndex = 2;
                    }
                }

                chkNonCashable.Enabled = false;
                chkNonCashable.Visible = false;
                ChkCrossTicketing.Enabled = false;
                ChkCrossTicketing.Visible = false;
                btnCrossTicketingConfig.Enabled = false;
                btnCrossTicketingConfig.Visible = false;

                if (_SiteID != 0) // For New Site
                {
                    txtCode.Enabled = false;

                    AdminSiteEntity Status = sdobj.GetSiteStatus(entity.Site_ID);
                    if (Status != null)
                    {
                        bool oldState = chkTITO.Checked;

                        if (Status.IsTITOEnabled == 1)
                            chkTITO.Checked = true;
                        else
                            chkTITO.Checked = false;
                        entity.IsTITOEnabled = Convert.ToInt32(chkTITO.Checked);
                        if (oldState == chkTITO.Checked)
                        {
                            chkTITO_CheckedChanged(chkTITO, EventArgs.Empty);
                        }

                        if (Status.IsCrossTicketingEnabled == 1)
                            ChkCrossTicketing.Checked = true;
                        else
                            ChkCrossTicketing.Checked = false;

                        if (Status.IsNonCashVoucherEnabled == 1)
                            chkNonCashable.Checked = true;
                        else
                            chkNonCashable.Checked = false;
                        entity.IsNonCashVoucherEnabled = Convert.ToInt32(chkNonCashable.Checked);
                    }
                }
                else
                {
                    txtCode.Enabled = true;
                }
                string StackerFeature = string.Empty;
                sdobj.GetSetting(null, "StackerFeature", "FALSE", ref StackerFeature);
                if (StackerFeature.Trim().ToUpper().Equals("TRUE"))
                {
                    lblStackerPercentageLimit.Visible = true;
                    txtStackerLimit.Visible = true;
                }
                else
                {
                    lblStackerPercentageLimit.Visible = false;
                    txtStackerLimit.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            finally
            {
                if (_SiteID != 0 && !AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Site_Edit"))
                {
                    tableLayoutPanel1.Enabled = false;
                }
            }
        }

        public void OnLoadSiteDetail1()
        {
            try
            {
                dtPickerClosureDate.Value = DateTime.Now;

                if (AppEntryPoint.Current.CustomerAccessViewAllDepot == true || AppGlobals.Current.HasUserAccess("HQ_Customer_Access_View_Entire_Database") == true)
                {
                    List<AdminSiteEntity> OpResult = sdobj.GetOperatorInfo();
                    if (OpResult != null)
                    {
                        lstsupplier.DataSource = OpResult;
                        lstsupplier.DisplayMember = "Operator_Name";
                        lstsupplier.ValueMember = "Operator_ID";
                    }
                    lstsupplier.SelectedIndex = -1;
                }
                else if (AppEntryPoint.Current.StaffId != 0)
                {
                    List<AdminSiteEntity> OpResult = sdobj.GetStaffCustomerAccessOperator(AppEntryPoint.Current.StaffId); //////////////////////////TO BE REVIEWED
                    if (OpResult != null)
                    {
                        lstsupplier.DataSource = OpResult;
                        lstsupplier.DisplayMember = "Operator_Name";
                        lstsupplier.ValueMember = "Operator_ID";
                    }
                    lstsupplier.SelectedIndex = -1;
                }

                List<AdminSiteEntity> ClassResult = sdobj.GetClassificationinfo();
                AdminSiteEntity ClassifList = new AdminSiteEntity();
                ClassifList.Site_Classification_Name = this.GetResourceTextByKey("Key_NoneHyphen");
                ClassResult.Insert(0, ClassifList);
                lstClassification.DataSource = ClassResult;
                lstClassification.DisplayMember = "Site_Classification_Name";
                lstClassification.ValueMember = "Site_Classification_ID";
                lstClassification.SelectedIndex = -1;

                List<ComboBoxItem> lstTypeofTradeItems = new List<ComboBoxItem>();
                lstTypeofTradeItems.Add(
                   new ComboBoxItem()
                   {
                       Text = this.GetResourceTextByKey("Key_MixedHypen"),
                       Value = "--Mixed--"

                   });

                lstTypeofTradeItems.Add(
                    new ComboBoxItem()
                    {
                        Text = this.GetResourceTextByKey("Key_Arcade"),
                        Value = "Arcade"

                    });
               lstTypeofTradeItems.Add(new ComboBoxItem()
                    {
                        Text = this.GetResourceTextByKey("Key_Bar"),
                        Value = "Bar"                    
                    });
               lstTypeofTradeItems.Add(new ComboBoxItem()
               {
                   Text = this.GetResourceTextByKey("Key_BingoClub"),
                   Value = "Bingo Club"
               });
               lstTypeofTradeItems.Add(new ComboBoxItem()
               {
                   Text = this.GetResourceTextByKey("Key_Casino"),
                   Value = "Casino"
               });
               lstTypeofTradeItems.Add(new ComboBoxItem()
               {
                   Text = this.GetResourceTextByKey("Key_Club"),
                   Value = "Club"
               });
               lstTypeofTradeItems.Add(new ComboBoxItem()
               {
                   Text = this.GetResourceTextByKey("Key_Franchise"),
                   Value = "Franchise"
               });
               lstTypeofTradeItems.Add(new ComboBoxItem()
               {
                   Text = this.GetResourceTextByKey("Key_IndependantFreeHouse"),
                   Value = "Independant Free House"
               });
               lstTypeofTradeItems.Add(new ComboBoxItem()
               {
                   Text = this.GetResourceTextByKey("Key_ManagedPublicHouse"),
                   Value = "Managed Public House"
               });
               lstTypeofTradeItems.Add(new ComboBoxItem()
               {
                   Text = this.GetResourceTextByKey("Key_Other"),
                   Value = "Other"
               });
               lstTypeofTradeItems.Add(new ComboBoxItem()
               {
                   Text = this.GetResourceTextByKey("Key_TenantedPublicHouse"),
                   Value = "Tenanted Public House"
               });

               lstTypeofTradeItems.Add(new ComboBoxItem()
               {
                   Text = this.GetResourceTextByKey("Key_BarIndependent"),
                   Value = "Bar-Independent"
               });
               lstTypeofTradeItems.Add(new ComboBoxItem()
               {
                   Text = this.GetResourceTextByKey("Key_BarManaged"),
                   Value = "Bar-Managed"
               });
               lstTypeofTradeItems.Add(new ComboBoxItem()
               {
                   Text = this.GetResourceTextByKey("Key_TenantedBarTenanted"),
                   Value = "Tenanted Bar-Tenanted"
               });

                lstTypeOfTrade.DataSource=null;
                lstTypeOfTrade.DataSource = lstTypeofTradeItems;
                lstTypeOfTrade.DisplayMember = "Text";
                lstTypeOfTrade.ValueMember = "Value";
               

                lstRegion.Items.Clear();
                lstRegion.Items.Add(this.GetResourceTextByKey("Key_UK"));
                lstRegion.Items.Add(this.GetResourceTextByKey("Key_US"));
                lstRegion.Items.Add(this.GetResourceTextByKey("Key_Italy"));
                lstRegion.Items.Add(this.GetResourceTextByKey("Key_AR"));
                lstRegion.Sorted=true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion

        #region IAdminSite Members


        public bool SaveDetails(AdminSiteEntity entity)
        {
            try
            {
                DepotChanged = 0;
                if (!Validate_Input())
                    return false;
                entity.Site_Code = txtCode.Text.Trim();
                entity.Site_Name = txtName.Text.Trim();
                entity.Site_Company_Code = txtSiteOwnerSiteCode.Text.Trim();
                entity.Site_Supplier_Code = txtSupplierSiteCode.Text.Trim();
                entity.Site_Phone_No = txtPhoneNumber.Text.Trim();
                entity.Site_Fax_No = txtFaxNumber.Text.Trim();
                entity.Site_Address_1 = txtAddress1.Text.Trim();
                entity.Site_Address_2 = txtAddress2.Text.Trim();
                entity.Site_Address_3 = txtAddress3.Text.Trim();
                entity.Site_Address_4 = txtAddress4.Text.Trim();
                entity.Site_Postcode = txtPostcode.Text.Trim().ToUpper();
                entity.Site_Email_Address = txtEmail.Text.Trim();
                entity.Site_Street_Number = txtStreetNo.Text.Trim();
                entity.Site_Province = txtProvince.Text.Trim();
                entity.Site_Municipality = txtMuncipality.Text.Trim();
                entity.Site_Cadastral_Code = txtCadastralCode.Text.Trim();
                entity.bSiteDetailsUpdated = false;
                if (cuobj.VerifyValidNumberLong(txtArea.Text.Trim()) != 0)
                {
                    entity.Site_Area = Convert.ToInt32(txtArea.Text.Trim());
                }
                else
                {
                    entity.Site_Area = 0;
                }
                string StackerFeature = string.Empty;
                sdobj.GetSetting(null, "StackerFeature", "FALSE", ref StackerFeature);
                if (StackerFeature.Trim().ToUpper().Equals("TRUE"))
                {
                    entity.StackerLimitPercentage = Convert.ToInt32(txtStackerLimit.Value);
                }
                else
                {
                    entity.StackerLimitPercentage = 0;
                }
                entity.Site_Manager = txtManager.Text.Trim();

                if (cuobj.VerifyValidNumberLong(Convert.ToString(entity.Depot_ID)) != frmUtobj.GetItemValue(lstDepot))
                {
                    DepotChanged = frmUtobj.GetItemValue(lstDepot);
                    if (entity.Site_ID != 0)
                        entity.bSiteDetailsUpdated = true;
                }
                if (lstDepot.SelectedIndex >= 0)
                {
                    entity.Depot_ID = Convert.ToInt32(lstDepot.SelectedValue);
                }
                else
                {
                    entity.Depot_ID = 0;
                }
                entity.Site_Classification_ID = Convert.ToInt32(cuobj.VerifyValidNumberLong(Convert.ToString(frmUtobj.GetItemValue(lstClassification))));

                if (lstTypeOfTrade.SelectedIndex >= 0)
                {
                    entity.Site_Trade_Type = lstTypeOfTrade.SelectedValue.ToString();
                }
                else
                {
                    entity.Site_Trade_Type = "";
                }
                string SelectedText=string.Empty;
                if(lstRegion.SelectedIndex>=0)
                indexRegion = lstRegion.SelectedIndex;
                 if (indexRegion==2)
                {
                    entity.Region = "UK";
                }
                else
                {
                   if (lstRegion.SelectedIndex == 3)
                    {
                        entity.Region = "US";
                    }
                    else
                    {
                      
                        if (lstRegion.SelectedIndex == 0)
                        {
                            entity.Region = "AR";
                        }
                        if (lstRegion.SelectedIndex == 1)
                        {
                            entity.Region = "IT";
                        }
                    }
                }

                entity.Site_Closed = chkSiteClosed.Checked ? 1 : 0;

                if (chkSiteClosed.Checked)
                {
                    entity.Site_Closed = 1;
                    entity.Site_Closed_Date = Convert.ToString(dtPickerClosureDate.Value);
                    entity.Site_Status_ID = 1;
                }
                else
                {
                    entity.Site_Closed = 0;
                    entity.Site_Closed_Date = null;
                    entity.Site_Status_ID = 0;
                    entity.Site_Inactive_Date = null;
                }

                if (chkTITO.Checked)
                {
                    entity.IsTITOEnabled = 1;
                }
                else
                {
                    entity.IsTITOEnabled = 0;
                }

                if (chkNonCashable.Checked)
                {
                    entity.IsNonCashVoucherEnabled = 1;
                }
                else
                {
                    entity.IsNonCashVoucherEnabled = 0;
                }

                if (ChkCrossTicketing.Checked)
                {
                    entity.IsCrossTicketingEnabled = 1;
                }
                else
                {
                    entity.IsCrossTicketingEnabled = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
            return true;
        }

        #endregion

        #region Numeric Validation


        private void txtPostcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar == (char)Keys.Space && char.IsSymbol(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
        private void txtStackerLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            else
            {
                if (txtStackerLimit.Value > 100)
                {
                    e.Handled = true;
                    txtStackerLimit.Text = "100";
                }
            }
        }
        #endregion Numeric Validation


    }
}

