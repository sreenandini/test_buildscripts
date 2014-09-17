#region Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BMC.Common;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseDataAccess;
using BMC.Common.LogManagement;
using BMC.EnterpriseClient.Helpers;
using Audit.Transport;
using BMC.EnterpriseClient.Helpers.ExtensionsMethods;
#endregion Namespaces


namespace BMC.EnterpriseClient.Views
{
    #region Class
    public partial class frmTerms : Form
    {
        #region Constants
        private const int TERMS_NONE = 0;
        private const int TERMS_LEFT_ON_SITE = 1;
        private const int TERMS_SITE = 2;
        private const int TERMS_SUPPLIER = 3;
        private const int TERMS_SITE_GROUP = 4;
        private const int TERMS_INVOICED = 5;
        private const int TERMS_SECONDARY_GROUP = 6;
        private const int TERMS_CUSTOMS_AND_EXCISE = 7;
        private const int TERMS_COLLECTOR_ACCOUNT = 8;
        private const int TERMS_CHEQUE = 9;
        private const int TERMS_STANDING_ORDER = 10;
        private const int TERMS_DIRECT_DEBIT = 11;
        private const int TERMS_CASH_BOX = 12;
        private const int TERMS_LICENCE_AUTHORITY = 13;
        private const int TERMS_SECONDARY_GROUP1 = 14;

        private const int TERMS_RENT = 20;
        private const int TERMS_RENT_FULL = 21;
        private const int TERMS_RENT_ONLY = 22;
        private const int TERMS_RENT_FIXED = 23;
        private const int TERMS_COURTESY = 24;
        private const int TERMS_FRONT_MONEY = 25;
        private const int TERMS_MIN = 26;
        private const int TERMS_MAX = 27;
        private const int TERMS_RENT_SCHEDULE = 28;
        private const int TERMS_SHARE_SCHEDULE = 29;
        private const int TERMS_PUBMASTER = 30;

        private const int TERMS_PRIMARY_FULL = 41;
        private const int TERMS_PRIMARY_TO_ZERO = 42;
        private const int TERMS_SECONDARY_REMAINDER = 43;
        private const int TERMS_SECONDARY_TO_ZERO = 44;
        private const int TERMS_PERCENTAGE = 45;
        private const int TERMS_CARRIED_FORWARD = 46;
        private const int TERMS_USE_PARTNERS_GUARANTORS = 47;

        private const int TERMS_FREQUENCY_PER_WEEK = 0;
        private const int TERMS_FREQUENCY_PER_COLLECTION = 1;

        private const int SUPPLIER_ROW_INDEX = 0;
        private const int SITE_ROW_INDEX = 1;
        private const int SITE_GROUP_ROW_INDEX = 2;
        private const int SEC_SITE_GROUP_ROW_INDEX = 3;

        private const int OUTPUT_VAT_ROW_INDEX = 0;
        private const int INPUT_VAT_OPERATOR_ROW_INDEX = 1;
        private const int INPUT_VAT_SITE_ROW_INDEX = 2;
        private const int INPUT_VAT_SITE_GROUP_ROW_INDEX = 3;
        private const int INPUT_VAT_SEC_SITE_GROUP_ROW_INDEX = 4;

        private const int GPT_ROW_INDEX = 0;

        private const int OTHER_LICENCE_ROW_INDEX = 0;
        private const int OTHER_PRIZE_ROW_INDEX = 1;
        private const int OTHER_CONSULTANCY_ROW_INDEX = 2;
        private const int OTHER_ROYALTY_ROW_INDEX = 3;
        private const int OTHER_OTHER1_ROW_INDEX = 4;
        private const int OTHER_OTHER2_ROW_INDEX = 5;
        #endregion Constants

        #region Private Members
        private TermsBusiness _termsBusiness = null;

        private Dictionary<int, string> _partnerCollection = new Dictionary<int, string>();
        private Dictionary<int, string> _partnerCashDestinationCollection = new Dictionary<int, string>();
        private Dictionary<int, string> _partnerDeferredRemittanceCollection = new Dictionary<int, string>();
        private Dictionary<int, string> _partnerChargeTypeCollection = new Dictionary<int, string>();
        private Dictionary<int, string> _partnerShortfallGuarantorCollection = new Dictionary<int, string>();

        private Dictionary<int, string> _vatCollection = new Dictionary<int, string>();
        private Dictionary<int, string> _vatCashDestinationCollection = new Dictionary<int, string>();
        private Dictionary<int, string> _vatDeferredRemittanceCollection = new Dictionary<int, string>();
        private Dictionary<int, string> _vatPaidByCollection = new Dictionary<int, string>();
        private Dictionary<int, string> _vatShortfallGuarantorCollection = new Dictionary<int, string>();

        private Dictionary<int, string> _gptCollection = new Dictionary<int, string>();
        private Dictionary<int, string> _gptCashDestinationCollection = new Dictionary<int, string>();
        private Dictionary<int, string> _gptDeferredRemittanceCollection = new Dictionary<int, string>();

        private Dictionary<int, string> _otherChargesCollection = new Dictionary<int, string>();
        private Dictionary<int, string> _otherChargesCashDestinationCollection = new Dictionary<int, string>();
        private Dictionary<int, string> _otherChargesDeferredRemittanceCollection = new Dictionary<int, string>();
        private Dictionary<int, string> _otherChargesPaidByCollection = new Dictionary<int, string>();
        private Dictionary<int, string> _otherChargesGuarantorCollection = new Dictionary<int, string>();
        private Dictionary<int, string> _otherChargesFrequencyCollection = new Dictionary<int, string>();

        private Dictionary<int, string> _rentScheduleCollection = new Dictionary<int, string>();
        private Dictionary<int, string> _shareScheduleCollection = new Dictionary<int, string>();

        private bool _isLoaded = false;
        private int _termsProfileID = 0;
        private int _termsOrder = 0;
        private int? _partnerShareSchedule = 0;
        private int? _partnerRentSchedule = 0;
        private float? _partnerSupplierValue = 0;
        private string _termsProfileName, _termsGroupName;

        private TermsProfileResult _termsProfileResult = null, _newTermsProfileResult = null;

        #endregion Private Members

        #region Constructor
        public frmTerms(int termsProfileID, string termsGroupName, string termsProfileName)
        {
            InitializeComponent();

            SetTagProperty();
            _termsBusiness = TermsBusiness.CreateInstance();
            LoadDefaultValuesForCollection();
            LoadPartnerData();
            LoadVATData();
            LoadGPTData();
            LoadOtherChargesData();
            txtLondonRent.ReadOnly = !chkLondonRentEnabled.Checked;
            _termsProfileID = termsProfileID;
            _isLoaded = true;
            _termsProfileName = termsProfileName;
            _termsGroupName = termsGroupName;
            PopulateTerms();
            this.ResolveResources();
        }
        #endregion Constructor

        #region Events
        private void dgvPartners_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside dgvPartners_CurrentCellDirtyStateChanged...", LogManager.enumLogLevel.Info);

                if (_isLoaded)
                {
                    if (dgvPartners.IsCurrentCellDirty)
                    {
                        dgvPartners.CommitEdit(DataGridViewDataErrorContexts.Commit);

                        if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "clm_PartnerType")
                            SetSupplierValue();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void dgvPartners_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside dgvPartners_CellClick...", LogManager.enumLogLevel.Info);

                if (_isLoaded)
                {
                    if (dgvPartners.Columns[e.ColumnIndex].Name == "clm_PartnerOrder")
                        if (string.IsNullOrEmpty(dgvPartners.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()) || dgvPartners.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "0")
                            dgvPartners.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = ++_termsOrder;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void dgvPartners_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside dgvPartners_DataBindingComplete...", LogManager.enumLogLevel.Info);

                if (_isLoaded)
                {
                    dgvPartners.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void dgvVAT_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside dgvVAT_CurrentCellDirtyStateChanged...", LogManager.enumLogLevel.Info);

                if (_isLoaded)
                {
                    if (dgvVAT.IsCurrentCellDirty)
                    {
                        dgvVAT.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void dgvVAT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside dgvVAT_CellClick...", LogManager.enumLogLevel.Info);

                if (_isLoaded)
                {
                    if (dgvVAT.Columns[e.ColumnIndex].Name == "clm_VATOrder")
                        if (string.IsNullOrEmpty(dgvVAT.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()) || dgvVAT.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "0")
                            dgvVAT.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = ++_termsOrder;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void dgvGPT_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside dgvGPT_CurrentCellDirtyStateChanged...", LogManager.enumLogLevel.Info);

                if (_isLoaded)
                {
                    if (dgvGPT.IsCurrentCellDirty)
                    {
                        dgvGPT.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void dgvGPT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside dgvGPT_CellClick...", LogManager.enumLogLevel.Info);

                if (_isLoaded)
                {
                    if (dgvGPT.Columns[e.ColumnIndex].Name == "clm_GPTOrder")
                        if (string.IsNullOrEmpty(dgvGPT.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()) || dgvGPT.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "0")
                            dgvGPT.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = ++_termsOrder;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void dgvOtherCharges_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside dgvOtherCharges_CurrentCellDirtyStateChanged...", LogManager.enumLogLevel.Info);

                if (_isLoaded)
                {
                    if (dgvOtherCharges.IsCurrentCellDirty)
                    {
                        dgvOtherCharges.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void dgvOtherCharges_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside dgvOtherCharges_CellClick...", LogManager.enumLogLevel.Info);

                if (_isLoaded)
                {
                    if (dgvOtherCharges.Columns[e.ColumnIndex].Name == "clm_OtherChargesOrder")
                        if (string.IsNullOrEmpty(dgvOtherCharges.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()) || dgvOtherCharges.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "0")
                            dgvOtherCharges.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = ++_termsOrder;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void chkLondonRentEnabled_Click(object sender, EventArgs e)
        {
            txtLondonRent.ReadOnly = !chkLondonRentEnabled.Checked;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnSave_Click...", LogManager.enumLogLevel.Info);

                if (CheckValidityOfTerms())
                {
                    if (UpdateTerms(_termsProfileID) != -1)
                        Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_TERMSET_SAVED"));
                    else
                        Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ERROR_SAVING_TERMSSET"));
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnClearOrders_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnClearOrders_Click...", LogManager.enumLogLevel.Info);

                dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerOrder"].Value = "0";
                dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerOrder"].Value = "0";
                dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerOrder"].Value = "0";
                dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerOrder"].Value = "0";

                dgvVAT.Rows[OUTPUT_VAT_ROW_INDEX].Cells["clm_VATOrder"].Value = "0";
                dgvVAT.Rows[INPUT_VAT_OPERATOR_ROW_INDEX].Cells["clm_VATOrder"].Value = "0";
                dgvVAT.Rows[INPUT_VAT_SITE_ROW_INDEX].Cells["clm_VATOrder"].Value = "0";
                dgvVAT.Rows[INPUT_VAT_SITE_GROUP_ROW_INDEX].Cells["clm_VATOrder"].Value = "0";
                dgvVAT.Rows[INPUT_VAT_SEC_SITE_GROUP_ROW_INDEX].Cells["clm_VATOrder"].Value = "0";

                dgvGPT.Rows[GPT_ROW_INDEX].Cells["clm_GPTOrder"].Value = "0";

                dgvOtherCharges.Rows[OTHER_LICENCE_ROW_INDEX].Cells["clm_OtherChargesOrder"].Value = "0";
                dgvOtherCharges.Rows[OTHER_PRIZE_ROW_INDEX].Cells["clm_OtherChargesOrder"].Value = "0";
                dgvOtherCharges.Rows[OTHER_CONSULTANCY_ROW_INDEX].Cells["clm_OtherChargesOrder"].Value = "0";
                dgvOtherCharges.Rows[OTHER_ROYALTY_ROW_INDEX].Cells["clm_OtherChargesOrder"].Value = "0";
                dgvOtherCharges.Rows[OTHER_OTHER1_ROW_INDEX].Cells["clm_OtherChargesOrder"].Value = "0";
                dgvOtherCharges.Rows[OTHER_OTHER2_ROW_INDEX].Cells["clm_OtherChargesOrder"].Value = "0";

                _termsOrder = 0;
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
        #endregion Events

        #region Private Methods
        private void SetTagProperty()
        {
            try
            {
                LogManager.WriteLog("Inside SetTagProperty...", LogManager.enumLogLevel.Info);

                this.groupBox3.Tag = "Key_GPT";
                this.clm_GPTOrder.Tag = "Key_Order";
                this.clm_GPT.Tag = "Key_Use";
                this.clm_GPTCashDestination.Tag = "Key_CashDestination";
                this.clm_GPTDeferredRemittance.Tag = "Key_DeferredRemittance";
                this.btnSave.Tag = "Key_Save";
                this.btnClose.Tag = "Key_Close";
                this.btnClearOrders.Tag = "Key_ClearOrders";
                this.groupBox2.Tag = "Key_Partners";
                this.clm_PartnerOrder.Tag = "Key_Order";
                this.clm_Partner.Tag = "Key_Use";
                this.clm_PartnerCashDestination.Tag = "Key_CashDestination";
                this.clm_PartnerDeferredRemittance.Tag = "Key_DeferredRemittance";
                this.clm_PartnerType.Tag = "Key_TypeWOShortcut";
                this.clm_PartnerValue.Tag = "Key_Value";
                this.clm_PartnerRentGuaranteed.Tag = "Key_G";
                this.clm_PartnerShare.Tag = "Key_Shr";
                this.clm_PartnerShareGuaranteed.Tag = "Key_G";
                this.clm_PartnerShortfallGuarantor.Tag = "Key_ShortfallGuarantor";
                this.groupBox4.Tag = "Key_OtherCharges";
                this.clm_OtherChargesOrder.Tag = "Key_Order";
                this.clm_OtherCharges.Tag = "Key_Use";
                this.clm_OtherChargesVAT.Tag = "Key_VATQ";
                this.clm_OtherChargesCashDestination.Tag = "Key_CashDestination";
                this.clm_OtherChargesDeferredRemittance.Tag = "Key_DeferredRemittance";
                this.clm_OtherChargesCharge.Tag = "Key_Charge";
                this.clm_OtherChargesPaidBy.Tag = "Key_PaidBy";
                this.clm_OtherChargesShortfallGuarantor.Tag = "Key_ShortfallGuarantor";
                this.clm_OtherChargesFrequency.Tag = "Key_Frequency";
                this.groupBox5.Tag = "Key_LondonRent";
                this.label1.Tag = "Key_Use";
                this.label2.Tag = "Key_Value";
                this.groupBox1.Tag = "Key_VAT";
                this.clm_VATOrder.Tag = "Key_Order";
                this.clm_VAT.Tag = "Key_Use";
                this.clm_VATCashDestination.Tag = "Key_CashDestination";
                this.clm_VATDeferredRemittance.Tag = "Key_DeferredRemittance";
                this.clm_VATPaidBy.Tag = "Key_PaidBy";
                this.clm_VATShortfallGuarantor.Tag = "Key_ShortfallGuarantor";
                this.Tag = "Key_TermsSet";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadDefaultValuesForCollection()
        {
            try
            {
                LogManager.WriteLog("Inside LoadDefaultValuesForCollection...", LogManager.enumLogLevel.Info);

                // Load Partner Collection                
                _partnerCollection.Add(SUPPLIER_ROW_INDEX, this.GetResourceTextByKey("Key_Terms_Supplier"));
                _partnerCollection.Add(SITE_ROW_INDEX, this.GetResourceTextByKey("Key_Terms_Site"));
                _partnerCollection.Add(SITE_GROUP_ROW_INDEX, this.GetResourceTextByKey("Key_Terms_SiteGroup"));
                _partnerCollection.Add(SEC_SITE_GROUP_ROW_INDEX, this.GetResourceTextByKey("Key_Terms_SecBrewery"));

                // Load Partner Cash Destination Collection
                _partnerCashDestinationCollection.Add(TERMS_LEFT_ON_SITE,this.GetResourceTextByKey("Key_Terms_LeftOnSite"));
                _partnerCashDestinationCollection.Add(TERMS_SUPPLIER, this.GetResourceTextByKey("Key_Terms_Supplier"));
                _partnerCashDestinationCollection.Add(TERMS_SITE_GROUP, this.GetResourceTextByKey("Key_Terms_SiteGroup"));
                _partnerCashDestinationCollection.Add(TERMS_INVOICED, this.GetResourceTextByKey("Key_Terms_Invoiced"));
                _partnerCashDestinationCollection.Add(TERMS_SECONDARY_GROUP, this.GetResourceTextByKey("Key_Terms_SecGroup"));

                // Load Partner Deferred Remittance Collection Collection
                _partnerDeferredRemittanceCollection.Add(TERMS_NONE, this.GetResourceTextByKey("Key_Terms_None"));
                _partnerDeferredRemittanceCollection.Add(TERMS_SUPPLIER, this.GetResourceTextByKey("Key_Terms_Supplier"));
                _partnerDeferredRemittanceCollection.Add(TERMS_SITE,this.GetResourceTextByKey("Key_Terms_Site"));
                _partnerDeferredRemittanceCollection.Add(TERMS_SITE_GROUP,this.GetResourceTextByKey("Key_Terms_SiteGroup"));
                _partnerDeferredRemittanceCollection.Add(TERMS_SECONDARY_GROUP, this.GetResourceTextByKey("Key_Terms_HoldingCompany"));
                _partnerDeferredRemittanceCollection.Add(TERMS_SECONDARY_GROUP1,this.GetResourceTextByKey("Key_Terms_SecBrewery"));

                // Load Partner Charge Type Collection
                _partnerChargeTypeCollection.Add(TERMS_RENT, this.GetResourceTextByKey("Key_Terms_Rent"));
                _partnerChargeTypeCollection.Add(TERMS_RENT_FULL,this.GetResourceTextByKey("Key_Terms_RentFull"));
                _partnerChargeTypeCollection.Add(TERMS_RENT_ONLY, this.GetResourceTextByKey("Key_Terms_RentOnly"));
                _partnerChargeTypeCollection.Add(TERMS_RENT_FIXED, this.GetResourceTextByKey("Key_Terms_RentFixed"));
                _partnerChargeTypeCollection.Add(TERMS_COURTESY, this.GetResourceTextByKey("Key_Terms_Courtesy"));
                _partnerChargeTypeCollection.Add(TERMS_FRONT_MONEY, this.GetResourceTextByKey("Key_Terms_FM"));
                _partnerChargeTypeCollection.Add(TERMS_MIN, this.GetResourceTextByKey("Key_Terms_Min"));
                _partnerChargeTypeCollection.Add(TERMS_MAX, this.GetResourceTextByKey("Key_Terms_Max"));
                _partnerChargeTypeCollection.Add(TERMS_RENT_SCHEDULE, this.GetResourceTextByKey("Key_Terms_RSch"));
                _partnerChargeTypeCollection.Add(TERMS_SHARE_SCHEDULE, this.GetResourceTextByKey("Key_Terms_SSch"));
                _partnerChargeTypeCollection.Add(TERMS_PUBMASTER,this.GetResourceTextByKey("Key_Terms_PubMaster"));

                // Load Partner ShortfallGuarantor Collection
                _partnerShortfallGuarantorCollection.Add(TERMS_NONE, this.GetResourceTextByKey("Key_Terms_None"));
                _partnerShortfallGuarantorCollection.Add(TERMS_CARRIED_FORWARD, this.GetResourceTextByKey("Key_Terms_CarriedForward"));
                _partnerShortfallGuarantorCollection.Add(TERMS_PRIMARY_FULL, this.GetResourceTextByKey("Key_Terms_PrimaryFull"));
                _partnerShortfallGuarantorCollection.Add(TERMS_PRIMARY_TO_ZERO,this.GetResourceTextByKey("Key_Terms_PrimaryToZero"));
                _partnerShortfallGuarantorCollection.Add(TERMS_SECONDARY_REMAINDER, this.GetResourceTextByKey("Key_Terms_SecondaryRemainder"));
                _partnerShortfallGuarantorCollection.Add(TERMS_SECONDARY_TO_ZERO, this.GetResourceTextByKey("Key_Terms_SecondaryToZero"));
                _partnerShortfallGuarantorCollection.Add(TERMS_PERCENTAGE, this.GetResourceTextByKey("Key_Terms_Percentage"));

                // Load VAT Collection
                _vatCollection.Add(OUTPUT_VAT_ROW_INDEX, this.GetResourceTextByKey("Key_Terms_OutputVAT"));
                _vatCollection.Add(INPUT_VAT_OPERATOR_ROW_INDEX, this.GetResourceTextByKey("Key_Terms_InputVATOperator"));
                _vatCollection.Add(INPUT_VAT_SITE_ROW_INDEX, this.GetResourceTextByKey("Key_Terms_InputVATSite"));
                _vatCollection.Add(INPUT_VAT_SITE_GROUP_ROW_INDEX, this.GetResourceTextByKey("Key_Terms_InputVATSiteGroup"));
                _vatCollection.Add(INPUT_VAT_SEC_SITE_GROUP_ROW_INDEX, this.GetResourceTextByKey("Key_Terms_InputVATSecBrewery"));

                // Load VAT Cash Destination Collection
                _vatCashDestinationCollection.Add(TERMS_LEFT_ON_SITE, this.GetResourceTextByKey("Key_Terms_LeftOnSite"));
                _vatCashDestinationCollection.Add(TERMS_SUPPLIER, this.GetResourceTextByKey("Key_Terms_SupplierAccount"));
                _vatCashDestinationCollection.Add(TERMS_SITE_GROUP, this.GetResourceTextByKey("Key_Terms_SiteGroup"));

                // Load VAT Deferred Remittance Collection
                _vatDeferredRemittanceCollection.Add(TERMS_SITE, this.GetResourceTextByKey("Key_Terms_SiteAccount"));
                _vatDeferredRemittanceCollection.Add(TERMS_SITE_GROUP, this.GetResourceTextByKey("Key_Terms_GroupAccount"));
                _vatDeferredRemittanceCollection.Add(TERMS_SECONDARY_GROUP, this.GetResourceTextByKey("Key_Terms_SecGroup"));
                _vatDeferredRemittanceCollection.Add(TERMS_SUPPLIER, this.GetResourceTextByKey("Key_Terms_SupplierAccount"));
                _vatDeferredRemittanceCollection.Add(TERMS_CUSTOMS_AND_EXCISE, this.GetResourceTextByKey("Key_Terms_CustomsExcise"));
                _vatDeferredRemittanceCollection.Add(TERMS_SECONDARY_GROUP1, this.GetResourceTextByKey("Key_Terms_SecGroupAccount"));

                // Load VAT PaidBy Collection
                _vatPaidByCollection.Add(TERMS_CASH_BOX, this.GetResourceTextByKey("Key_Terms_CashBox"));
                _vatPaidByCollection.Add(TERMS_SUPPLIER, this.GetResourceTextByKey("Key_Terms_Supplier"));
                _vatPaidByCollection.Add(TERMS_SITE, this.GetResourceTextByKey("Key_Terms_Site"));
                _vatPaidByCollection.Add(TERMS_SITE_GROUP, this.GetResourceTextByKey("Key_Terms_SiteGroup"));
                _vatPaidByCollection.Add(TERMS_SECONDARY_GROUP, this.GetResourceTextByKey("Key_Terms_SecBrewery"));

                // Load VAT ShortfallGuarantor Collection
                _vatShortfallGuarantorCollection.Add(TERMS_NONE, this.GetResourceTextByKey("Key_Terms_None"));
                _vatShortfallGuarantorCollection.Add(TERMS_SUPPLIER, this.GetResourceTextByKey("Key_Terms_Supplier"));
                _vatShortfallGuarantorCollection.Add(TERMS_SITE, this.GetResourceTextByKey("Key_Terms_Site"));
                _vatShortfallGuarantorCollection.Add(TERMS_SITE_GROUP, this.GetResourceTextByKey("Key_Terms_SiteGroup"));
                _vatShortfallGuarantorCollection.Add(TERMS_SECONDARY_GROUP, this.GetResourceTextByKey("Key_Terms_SecGroup"));

                // Load GPT Collection
                _gptCollection.Add(GPT_ROW_INDEX, this.GetResourceTextByKey("Key_Terms_GPT"));

                // Load GPT Cash Destination Collection
                _gptCashDestinationCollection.Add(TERMS_LEFT_ON_SITE, this.GetResourceTextByKey("Key_Terms_LeftOnSite"));
                _gptCashDestinationCollection.Add(TERMS_SUPPLIER, this.GetResourceTextByKey("Key_Terms_SupplierAccount"));
                _gptCashDestinationCollection.Add(TERMS_SITE_GROUP, this.GetResourceTextByKey("Key_Terms_SiteGroup"));

                // Load GPT Deferred Remittance Collection
                _gptDeferredRemittanceCollection.Add(TERMS_SITE, this.GetResourceTextByKey("Key_Terms_SiteAccount"));
                _gptDeferredRemittanceCollection.Add(TERMS_SITE_GROUP, this.GetResourceTextByKey("Key_Terms_GroupAccount"));
                _gptDeferredRemittanceCollection.Add(TERMS_SECONDARY_GROUP, this.GetResourceTextByKey("Key_Terms_SecGroup"));
                _gptDeferredRemittanceCollection.Add(TERMS_SUPPLIER, this.GetResourceTextByKey("Key_Terms_SupplierAccount"));
                _gptDeferredRemittanceCollection.Add(TERMS_CUSTOMS_AND_EXCISE, this.GetResourceTextByKey("Key_Terms_CustomsExcise"));

                // Load Other Charges Collection
                _otherChargesCollection.Add(OTHER_LICENCE_ROW_INDEX, this.GetResourceTextByKey("Key_Terms_Licence"));
                _otherChargesCollection.Add(OTHER_PRIZE_ROW_INDEX, this.GetResourceTextByKey("Key_Terms_Prize"));
                _otherChargesCollection.Add(OTHER_CONSULTANCY_ROW_INDEX, this.GetResourceTextByKey("Key_Terms_Consultancy"));
                _otherChargesCollection.Add(OTHER_ROYALTY_ROW_INDEX, this.GetResourceTextByKey("Key_Terms_Royalty"));
                _otherChargesCollection.Add(OTHER_OTHER1_ROW_INDEX, this.GetResourceTextByKey("Key_Terms_Other1"));
                _otherChargesCollection.Add(OTHER_OTHER2_ROW_INDEX, this.GetResourceTextByKey("Key_Terms_Other2"));

                // Load Other Charges Cash Destination Collection
                _otherChargesCashDestinationCollection.Add(TERMS_LEFT_ON_SITE, this.GetResourceTextByKey("Key_Terms_LeftOnSite"));
                _otherChargesCashDestinationCollection.Add(TERMS_SUPPLIER, this.GetResourceTextByKey("Key_Terms_SupplierAccount"));
                _otherChargesCashDestinationCollection.Add(TERMS_SITE_GROUP, this.GetResourceTextByKey("Key_Terms_GroupAccount"));
                _otherChargesCashDestinationCollection.Add(TERMS_INVOICED, this.GetResourceTextByKey("Key_Terms_BulkCharge"));

                // Load Other Charges Deferred Remittance Collection
                _otherChargesDeferredRemittanceCollection.Add(TERMS_SUPPLIER, this.GetResourceTextByKey("Key_Terms_SupplierAccount"));
                _otherChargesDeferredRemittanceCollection.Add(TERMS_SITE, this.GetResourceTextByKey("Key_Terms_SiteAccount"));
                _otherChargesDeferredRemittanceCollection.Add(TERMS_SITE_GROUP, this.GetResourceTextByKey("Key_Terms_SiteGroupAccount"));
                _otherChargesDeferredRemittanceCollection.Add(TERMS_SECONDARY_GROUP, this.GetResourceTextByKey("Key_Terms_SecGroupAccount"));
                _otherChargesDeferredRemittanceCollection.Add(TERMS_LICENCE_AUTHORITY, this.GetResourceTextByKey("Key_Terms_LicAuthority"));
                _otherChargesDeferredRemittanceCollection.Add(TERMS_NONE, this.GetResourceTextByKey("Key_Terms_None"));

                // Load Other Charges PaidBy Collection
                _otherChargesPaidByCollection.Add(TERMS_CASH_BOX, this.GetResourceTextByKey("Key_Terms_CashBox"));
                _otherChargesPaidByCollection.Add(TERMS_SUPPLIER, this.GetResourceTextByKey("Key_Terms_Supplier"));
                _otherChargesPaidByCollection.Add(TERMS_SITE, this.GetResourceTextByKey("Key_Terms_Site"));
                _otherChargesPaidByCollection.Add(TERMS_SITE_GROUP, this.GetResourceTextByKey("Key_Terms_SiteGroup"));
                _otherChargesPaidByCollection.Add(TERMS_SECONDARY_GROUP, this.GetResourceTextByKey("Key_Terms_SecGroup"));

                // Load Other Charges Guarantor Collection
                _otherChargesGuarantorCollection.Add(TERMS_NONE, this.GetResourceTextByKey("Key_Terms_None"));
                _otherChargesGuarantorCollection.Add(TERMS_SUPPLIER, this.GetResourceTextByKey("Key_Terms_Supplier"));
                _otherChargesGuarantorCollection.Add(TERMS_SITE, this.GetResourceTextByKey("Key_Terms_Site"));
                _otherChargesGuarantorCollection.Add(TERMS_SITE_GROUP, this.GetResourceTextByKey("Key_Terms_SiteGroup"));
                _otherChargesGuarantorCollection.Add(TERMS_SECONDARY_GROUP, this.GetResourceTextByKey("Key_Terms_SecGroup"));
                _otherChargesGuarantorCollection.Add(TERMS_PERCENTAGE, this.GetResourceTextByKey("Key_Terms_Percentage"));

                // Load Other Charges Frequency Collection
                _otherChargesFrequencyCollection.Add(TERMS_FREQUENCY_PER_WEEK, this.GetResourceTextByKey("Key_Terms_PerWeek"));
                _otherChargesFrequencyCollection.Add(TERMS_FREQUENCY_PER_COLLECTION, this.GetResourceTextByKey("Key_Terms_PerCollection"));

                // Load Share Schedule Collection
                _shareScheduleCollection = _termsBusiness.GetAllShareSchedules().ToDictionary(x => x.Share_Schedule_ID, y => y.Share_Schedule_Name);

                // Load Rent Schedule Collection
                _rentScheduleCollection = _termsBusiness.GetAllRentSchedules().ToDictionary(x => x.Rent_Schedule_ID, y => y.Rent_Schedule_Name);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadPartnerData()
        {
            try
            {
                LogManager.WriteLog("Inside LoadPartnerData...", LogManager.enumLogLevel.Info);

                foreach (KeyValuePair<int, string> partner in _partnerCollection)
                {
                    var rowIndex = dgvPartners.Rows.Add();
                    dgvPartners.Rows[rowIndex].Cells["clm_Partner"].Value = partner.Value;
                    dgvPartners.Rows[rowIndex].Cells["clm_PartnerEnabled"].Value = true;
                    ((DataGridViewComboBoxCell)dgvPartners.Rows[rowIndex].Cells["clm_PartnerCashDestination"]).Items.AddRange(GetPartnerCashDestination(partner.Value));
                    ((DataGridViewComboBoxCell)dgvPartners.Rows[rowIndex].Cells["clm_PartnerDeferredRemittance"]).Items.AddRange(GetPartnerDeferredRemittance(partner.Value));
                    ((DataGridViewComboBoxCell)dgvPartners.Rows[rowIndex].Cells["clm_PartnerType"]).Items.AddRange(GetPartnerChargeType(partner.Value));
                    ((DataGridViewComboBoxCell)dgvPartners.Rows[rowIndex].Cells["clm_PartnerShortfallGuarantor"]).Items.AddRange(GetPartnerShortfallGuarantor(partner.Value));
                    ((DataGridViewComboBoxCell)dgvPartners.Rows[rowIndex].Cells["clm_PartnerCashDestination"]).Value = string.Empty;
                    ((DataGridViewComboBoxCell)dgvPartners.Rows[rowIndex].Cells["clm_PartnerDeferredRemittance"]).Value = string.Empty;
                    ((DataGridViewComboBoxCell)dgvPartners.Rows[rowIndex].Cells["clm_PartnerType"]).Value = string.Empty;
                    ((DataGridViewComboBoxCell)dgvPartners.Rows[rowIndex].Cells["clm_PartnerShortfallGuarantor"]).Value = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadVATData()
        {
            try
            {
                LogManager.WriteLog("Inside LoadVATData...", LogManager.enumLogLevel.Info);

                foreach (KeyValuePair<int, string> vatType in _vatCollection)
                {
                    var rowIndex = dgvVAT.Rows.Add();
                    dgvVAT.Rows[rowIndex].Cells["clm_VAT"].Value = vatType.Value;
                    dgvVAT.Rows[rowIndex].Cells["clm_VATEnabled"].Value = true;
                    ((DataGridViewComboBoxCell)dgvVAT.Rows[rowIndex].Cells["clm_VATCashDestination"]).Items.AddRange(GetVATCashDestination(vatType.Value));
                    ((DataGridViewComboBoxCell)dgvVAT.Rows[rowIndex].Cells["clm_VATDeferredRemittance"]).Items.AddRange(GetVATDeferredRemittance(vatType.Value));
                    ((DataGridViewComboBoxCell)dgvVAT.Rows[rowIndex].Cells["clm_VATPaidBy"]).Items.AddRange(GetVATPaidBy(vatType.Value));
                    ((DataGridViewComboBoxCell)dgvVAT.Rows[rowIndex].Cells["clm_VATShortfallGuarantor"]).Items.AddRange(GetVATShortfallGuarantor(vatType.Value));

                    ((DataGridViewComboBoxCell)dgvVAT.Rows[rowIndex].Cells["clm_VATCashDestination"]).Value = string.Empty;
                    ((DataGridViewComboBoxCell)dgvVAT.Rows[rowIndex].Cells["clm_VATDeferredRemittance"]).Value = string.Empty;
                    ((DataGridViewComboBoxCell)dgvVAT.Rows[rowIndex].Cells["clm_VATPaidBy"]).Value = string.Empty;
                    ((DataGridViewComboBoxCell)dgvVAT.Rows[rowIndex].Cells["clm_VATShortfallGuarantor"]).Value = string.Empty;
                }

                ((DataGridViewComboBoxCell)dgvVAT.Rows[OUTPUT_VAT_ROW_INDEX].Cells["clm_VATShortfallGuarantor"]).ReadOnly = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadGPTData()
        {
            try
            {
                LogManager.WriteLog("Inside LoadGPTData...", LogManager.enumLogLevel.Info);

                foreach (KeyValuePair<int, string> gptType in _gptCollection)
                {
                    var rowIndex = dgvGPT.Rows.Add();
                    dgvGPT.Rows[rowIndex].Cells["clm_GPT"].Value = this.GetResourceTextByKey("Key_GPT");
                    ((DataGridViewComboBoxCell)dgvGPT.Rows[rowIndex].Cells["clm_GPTCashDestination"]).Items.AddRange(GetGPTCashDestination(gptType.Value));
                    ((DataGridViewComboBoxCell)dgvGPT.Rows[rowIndex].Cells["clm_GPTDeferredRemittance"]).Items.AddRange(GetGPTDeferredRemittance(gptType.Value));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadOtherChargesData()
        {
            try
            {
                LogManager.WriteLog("Inside LoadOtherChargesData...", LogManager.enumLogLevel.Info);

                foreach (KeyValuePair<int, string> otherCharge in _otherChargesCollection)
                {
                    var rowIndex = dgvOtherCharges.Rows.Add();
                    dgvOtherCharges.Rows[rowIndex].Cells["clm_OtherCharges"].Value = otherCharge.Value;
                    ((DataGridViewComboBoxCell)dgvOtherCharges.Rows[rowIndex].Cells["clm_OtherChargesCashDestination"]).Items.AddRange(GetOtherChargesCashDestination(otherCharge.Value));
                    ((DataGridViewComboBoxCell)dgvOtherCharges.Rows[rowIndex].Cells["clm_OtherChargesDeferredRemittance"]).Items.AddRange(GetOtherChargesDeferredRemittance(otherCharge.Value));
                    ((DataGridViewComboBoxCell)dgvOtherCharges.Rows[rowIndex].Cells["clm_OtherChargesPaidBy"]).Items.AddRange(GetOtherChargesPaidBy(otherCharge.Value));
                    ((DataGridViewComboBoxCell)dgvOtherCharges.Rows[rowIndex].Cells["clm_OtherChargesShortfallGuarantor"]).Items.AddRange(GetOtherChargesGuarantor(otherCharge.Value));
                    ((DataGridViewComboBoxCell)dgvOtherCharges.Rows[rowIndex].Cells["clm_OtherChargesFrequency"]).Items.AddRange(GetOtherChargesFrequency(otherCharge.Value));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private string[] GetPartnerCashDestination(string partner)
        {
            try
            {
                LogManager.WriteLog("Inside GetPartnerCashDestination...", LogManager.enumLogLevel.Info);

                switch (partner)
                {
                    case "Supplier":
                        return _partnerCashDestinationCollection.Where(x => x.Value == "Left On Site" || x.Value == "Supplier" || x.Value == "Site Group" || x.Value == "Invoiced").Select(x => x.Value).ToArray();
                    case "Site":
                    case "Site Group":
                        return _partnerCashDestinationCollection.Where(x => x.Value == "Left On Site" || x.Value == "Supplier" || x.Value == "Site Group").Select(x => x.Value).ToArray();
                    case "Sec Brewery":
                        return _partnerCashDestinationCollection.Where(x => x.Value == "Left On Site" || x.Value == "Supplier" || x.Value == "Site Group" || x.Value == "Sec Group").Select(x => x.Value).ToArray();
                    default:
                        return _partnerCashDestinationCollection.Select(x => x.Value).ToArray();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return new string[] { };
        }

        private string[] GetPartnerDeferredRemittance(string partner)
        {
            try
            {
                LogManager.WriteLog("Inside GetPartnerDeferredRemittance...", LogManager.enumLogLevel.Info);

                switch (partner)
                {
                    case "Supplier":
                        return _partnerDeferredRemittanceCollection.Where(x => x.Value == "None" || x.Value == "Supplier").Select(x => x.Value).ToArray();
                    case "Site":
                        return _partnerDeferredRemittanceCollection.Where(x => x.Value == "None" || x.Value == "Site" || x.Value == "Site Group" || x.Value == "Holding Company").Select(x => x.Value).ToArray();
                    case "Site Group":
                        return _partnerDeferredRemittanceCollection.Where(x => x.Value == "None" || x.Value == "Site Group").Select(x => x.Value).ToArray();
                    case "Sec Brewery":
                        return _partnerDeferredRemittanceCollection.Where(x => x.Value == "None" || x.Value == "Sec Brewery").Select(x => x.Value).ToArray();
                    default:
                        return _partnerDeferredRemittanceCollection.Select(x => x.Value).ToArray();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return new string[] { };
        }

        private string[] GetPartnerChargeType(string partner)
        {
            try
            {
                LogManager.WriteLog("Inside GetPartnerChargeType...", LogManager.enumLogLevel.Info);

                switch (partner)
                {
                    case "Supplier":
                        return _partnerChargeTypeCollection.Where(x => x.Value == "Rent Full" || x.Value == "Rent Only" || x.Value == "Rent Fixed" || x.Value == "Courtesy" || x.Value == "F/M"
                            || x.Value == "Min" || x.Value == "Max" || x.Value == "RSch" || x.Value == "SSch" || x.Value == "PubMaster").Select(x => x.Value).ToArray();
                    case "Site":
                    case "Site Group":
                    case "Sec Brewery":
                        return _partnerChargeTypeCollection.Where(x => x.Value == "Rent" || x.Value == "F/M" || x.Value == "Min" || x.Value == "Max").Select(x => x.Value).ToArray();
                    default:
                        return _partnerChargeTypeCollection.Select(x => x.Value).ToArray();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return new string[] { };
        }

        private string[] GetPartnerShortfallGuarantor(string partner)
        {
            try
            {
                LogManager.WriteLog("Inside GetPartnerShortfallGuarantor...", LogManager.enumLogLevel.Info);

                switch (partner)
                {
                    case "Supplier":
                        return _partnerShortfallGuarantorCollection.Where(x => x.Value == "None" || x.Value == "Primary Full" || x.Value == "Primary To Zero"
                            || x.Value == "Secondary Remainder" || x.Value == "Secondary To Zero" || x.Value == "Percentage").Select(x => x.Value).ToArray();
                    case "Site":
                        return _partnerShortfallGuarantorCollection.Where(x => x.Value == "None" || x.Value == "Carried Forward" || x.Value == "Primary Full"
                            || x.Value == "Primary To Zero" || x.Value == "Secondary Remainder" || x.Value == "Secondary To Zero" || x.Value == "Percentage").Select(x => x.Value).ToArray();
                    case "Site Group":
                    case "Sec Brewery":
                        return _partnerShortfallGuarantorCollection.Where(x => x.Value == "None" || x.Value == "Primary Full" || x.Value == "Primary To Zero" || x.Value == "Secondary Remainder"
                            || x.Value == "Secondary To Zero" || x.Value == "Percentage").Select(x => x.Value).ToArray();
                    default:
                        return _partnerShortfallGuarantorCollection.Select(x => x.Value).ToArray();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return new string[] { };
        }

        private string[] GetVATCashDestination(string partner)
        {
            try
            {
                LogManager.WriteLog("Inside GetVATCashDestination...", LogManager.enumLogLevel.Info);

                return _vatCashDestinationCollection.Select(x => x.Value).ToArray();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return new string[] { };
        }

        private string[] GetVATDeferredRemittance(string partner)
        {
            try
            {
                LogManager.WriteLog("Inside GetVATDeferredRemittance...", LogManager.enumLogLevel.Info);

                switch (partner)
                {
                    case "Output VAT":
                        return _vatDeferredRemittanceCollection.Where(x => x.Value == "Site Account" || x.Value == "Group Account" || x.Value == "Sec Group" || x.Value == "Supplier Account" || x.Value == "Customs & Excise").Select(x => x.Value).ToArray();
                    case "Input VAT - Operator":
                    case "Input VAT - Site":
                    case "Input VAT - Site Group":
                        return _vatDeferredRemittanceCollection.Where(x => x.Value == "Site Account" || x.Value == "Group Account" || x.Value == "Supplier Account" || x.Value == "Customs & Excise").Select(x => x.Value).ToArray();
                    case "Input VAT - Sec Brewery":
                        return _vatDeferredRemittanceCollection.Where(x => x.Value == "Site Account" || x.Value == "Group Account" || x.Value == "Sec Group Account" || x.Value == "Supplier Account"
                            || x.Value == "Customs & Excise").Select(x => x.Value).ToArray();
                    default:
                        return _vatDeferredRemittanceCollection.Select(x => x.Value).ToArray();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return new string[] { };
        }

        private string[] GetVATPaidBy(string partner)
        {
            try
            {
                LogManager.WriteLog("Inside GetVATPaidBy...", LogManager.enumLogLevel.Info);

                switch (partner)
                {
                    case "Output VAT":
                        return new string[] { };
                    case "Input VAT - Operator":
                    case "Input VAT - Site":
                    case "Input VAT - Site Group":
                        return _vatPaidByCollection.Where(x => x.Value == "Cash Box" || x.Value == "Supplier" || x.Value == "Site" || x.Value == "Site Group").Select(x => x.Value).ToArray();
                    case "Input VAT - Sec Brewery":
                        return _vatPaidByCollection.Where(x => x.Value == "Cash Box" || x.Value == "Supplier" || x.Value == "Site" || x.Value == "Site Group" || x.Value == "Sec Brewery").Select(x => x.Value).ToArray();
                    default:
                        return _vatPaidByCollection.Select(x => x.Value).ToArray();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return new string[] { };
        }

        private string[] GetVATShortfallGuarantor(string partner)
        {
            try
            {
                LogManager.WriteLog("Inside GetVATShortfallGuarantor...", LogManager.enumLogLevel.Info);

                return _vatShortfallGuarantorCollection.Select(x => x.Value).ToArray();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return new string[] { };
        }

        private string[] GetGPTCashDestination(string partner)
        {
            try
            {
                LogManager.WriteLog("Inside GetGPTCashDestination...", LogManager.enumLogLevel.Info);

                return _gptCashDestinationCollection.Select(x => x.Value).ToArray();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return new string[] { };
        }

        private string[] GetGPTDeferredRemittance(string partner)
        {
            try
            {
                LogManager.WriteLog("Inside GetGPTDeferredRemittance...", LogManager.enumLogLevel.Info);

                return _gptDeferredRemittanceCollection.Select(x => x.Value).ToArray();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return new string[] { };
        }

        private string[] GetOtherChargesCashDestination(string otherCharge)
        {
            try
            {
                LogManager.WriteLog("Inside GetOtherChargesCashDestination...", LogManager.enumLogLevel.Info);

                switch (otherCharge)
                {
                    case "Licence":
                        return _otherChargesCashDestinationCollection.Where(x => x.Value == "Left On Site" || x.Value == "Supplier Account" || x.Value == "Group Account" || x.Value == "Bulk Charge").Select(x => x.Value).ToArray();
                    case "Prize":
                    case "Consultancy":
                    case "Royalty":
                    case "Other1":
                    case "Other2":
                        return _otherChargesCashDestinationCollection.Where(x => x.Value == "Left On Site" || x.Value == "Supplier Account" || x.Value == "Group Account").Select(x => x.Value).ToArray();
                    default:
                        return _otherChargesCashDestinationCollection.Select(x => x.Value).ToArray();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return new string[] { };
        }

        private string[] GetOtherChargesDeferredRemittance(string otherCharge)
        {
            try
            {
                LogManager.WriteLog("Inside GetOtherChargesDeferredRemittance...", LogManager.enumLogLevel.Info);

                switch (otherCharge)
                {
                    case "Licence":
                        return _otherChargesDeferredRemittanceCollection.Where(x => x.Value == "Supplier Account" || x.Value == "Site Account" || x.Value == "Site Group Account"
                            || x.Value == "Sec Group Account" || x.Value == "Lic Authority").Select(x => x.Value).ToArray();
                    case "Prize":
                    case "Consultancy":
                    case "Royalty":
                    case "Other1":
                    case "Other2":
                        return _otherChargesDeferredRemittanceCollection.Where(x => x.Value == "None" || x.Value == "Supplier Account" || x.Value == "Site Account" || x.Value == "Site Group Account"
                            || x.Value == "Sec Group Account").Select(x => x.Value).ToArray();
                    default:
                        return _otherChargesDeferredRemittanceCollection.Select(x => x.Value).ToArray();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return new string[] { };
        }

        private string[] GetOtherChargesPaidBy(string otherCharge)
        {
            try
            {
                LogManager.WriteLog("Inside GetOtherChargesPaidBy...", LogManager.enumLogLevel.Info);

                switch (otherCharge)
                {
                    case "Licence":
                    case "Prize":
                    case "Consultancy":
                    case "Royalty":
                    case "Other1":
                    case "Other2":
                        return _otherChargesPaidByCollection.Where(x => x.Value == "Cash Box" || x.Value == "Supplier" || x.Value == "Site" || x.Value == "Site Group" || x.Value == "Sec Group").Select(x => x.Value).ToArray();
                    default:
                        return _otherChargesPaidByCollection.Select(x => x.Value).ToArray();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return new string[] { };
        }

        private string[] GetOtherChargesGuarantor(string otherCharge)
        {
            try
            {
                LogManager.WriteLog("Inside GetOtherChargesGuarantor...", LogManager.enumLogLevel.Info);

                switch (otherCharge)
                {
                    case "Licence":
                    case "Prize":
                    case "Consultancy":
                    case "Royalty":
                    case "Other1":
                    case "Other2":
                        return _otherChargesGuarantorCollection.Where(x => x.Value == "None" || x.Value == "Supplier" || x.Value == "Site" || x.Value == "Site Group" || x.Value == "Sec Group" || x.Value == "Percentage").Select(x => x.Value).ToArray();
                    default:
                        return _otherChargesGuarantorCollection.Select(x => x.Value).ToArray();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return new string[] { };
        }

        private string[] GetOtherChargesFrequency(string otherCharge)
        {
            try
            {
                LogManager.WriteLog("Inside GetOtherChargesFrequency...", LogManager.enumLogLevel.Info);

                switch (otherCharge)
                {
                    case "Licence":
                    case "Prize":
                    case "Consultancy":
                    case "Royalty":
                    case "Other1":
                    case "Other2":
                        return _otherChargesFrequencyCollection.Where(x => x.Value == "Per Week" || x.Value == "Per Collection").Select(x => x.Value).ToArray();
                    default:
                        return _otherChargesFrequencyCollection.Select(x => x.Value).ToArray();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return new string[] { };
        }

        private string GetValueFromKey(string type, string subType, int key)
        {
            string returnValue = string.Empty;

            try
            {
                LogManager.WriteLog("Inside GetValueFromKey...", LogManager.enumLogLevel.Info);

                if (key == -1) return returnValue;

                switch (type)
                {
                    case "PartnerCashDestination":
                        _partnerCashDestinationCollection.TryGetValue(key, out returnValue);
                        returnValue = (GetPartnerCashDestination(subType).Contains(returnValue)) ? returnValue : string.Empty;
                        break;
                    case "PartnerDeferredRemittance":
                        _partnerDeferredRemittanceCollection.TryGetValue(key, out returnValue);
                        returnValue = (GetPartnerDeferredRemittance(subType).Contains(returnValue)) ? returnValue : string.Empty;
                        break;
                    case "PartnerChargeType":
                        _partnerChargeTypeCollection.TryGetValue(key, out returnValue);
                        returnValue = (GetPartnerChargeType(subType).Contains(returnValue)) ? returnValue : string.Empty;
                        break;
                    case "PartnerShortfallGuarantor":
                        _partnerShortfallGuarantorCollection.TryGetValue(key, out returnValue);
                        returnValue = (GetPartnerShortfallGuarantor(subType).Contains(returnValue)) ? returnValue : string.Empty;
                        break;
                    case "VATCashDestination":
                        _vatCashDestinationCollection.TryGetValue(key, out returnValue);
                        returnValue = (GetVATCashDestination(subType).Contains(returnValue)) ? returnValue : string.Empty;
                        break;
                    case "VATDeferredRemittance":
                        _vatDeferredRemittanceCollection.TryGetValue(key, out returnValue);
                        returnValue = (GetVATDeferredRemittance(subType).Contains(returnValue)) ? returnValue : string.Empty;
                        break;
                    case "VATPaidBy":
                        _vatPaidByCollection.TryGetValue(key, out returnValue);
                        returnValue = (GetVATPaidBy(subType).Contains(returnValue)) ? returnValue : string.Empty;
                        break;
                    case "VATShortfallGuarantor":
                        _vatShortfallGuarantorCollection.TryGetValue(key, out returnValue);
                        returnValue = (GetVATShortfallGuarantor(subType).Contains(returnValue)) ? returnValue : string.Empty;
                        break;
                    case "GPTCashDestination":
                        _gptCashDestinationCollection.TryGetValue(key, out returnValue);
                        returnValue = (GetGPTCashDestination(subType).Contains(returnValue)) ? returnValue : string.Empty;
                        break;
                    case "GPTDeferredRemittance":
                        _gptDeferredRemittanceCollection.TryGetValue(key, out returnValue);
                        returnValue = (GetGPTDeferredRemittance(subType).Contains(returnValue)) ? returnValue : string.Empty;
                        break;
                    case "OtherChargesCashDestination":
                        _otherChargesCashDestinationCollection.TryGetValue(key, out returnValue);
                        returnValue = (GetOtherChargesCashDestination(subType).Contains(returnValue)) ? returnValue : string.Empty;
                        break;
                    case "OtherChargesDeferredRemittance":
                        _otherChargesDeferredRemittanceCollection.TryGetValue(key, out returnValue);
                        returnValue = (GetOtherChargesDeferredRemittance(subType).Contains(returnValue)) ? returnValue : string.Empty;
                        break;
                    case "OtherChargesPaidBy":
                        _otherChargesPaidByCollection.TryGetValue(key, out returnValue);
                        returnValue = (GetOtherChargesPaidBy(subType).Contains(returnValue)) ? returnValue : string.Empty;
                        break;
                    case "OtherChargesGuarantor":
                        _otherChargesGuarantorCollection.TryGetValue(key, out returnValue);
                        returnValue = (GetOtherChargesGuarantor(subType).Contains(returnValue)) ? returnValue : string.Empty;
                        break;
                    case "OtherChargesFrequency":
                        _otherChargesFrequencyCollection.TryGetValue(key, out returnValue);
                        returnValue = (GetOtherChargesFrequency(subType).Contains(returnValue)) ? returnValue : string.Empty;
                        break;
                    case "ShareSchedule":
                        _shareScheduleCollection.TryGetValue(key, out returnValue);
                        break;
                    case "RentSchedule":
                        _rentScheduleCollection.TryGetValue(key, out returnValue);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return returnValue != null ? returnValue : string.Empty;
        }

        private int GetKeyFromValue(string type, string value)
        {
            int returnValue = 0;

            try
            {
                LogManager.WriteLog("Inside GetKeyFromValue...", LogManager.enumLogLevel.Info);

                if (value == string.Empty) return -1;

                switch (type)
                {
                    case "PartnerCashDestination":
                        returnValue = _partnerCashDestinationCollection.Where(x => x.Value == value).Select(x => x.Key).SingleOrDefault();
                        break;
                    case "PartnerDeferredRemittance":
                        returnValue = _partnerDeferredRemittanceCollection.Where(x => x.Value == value).Select(x => x.Key).SingleOrDefault();
                        break;
                    case "PartnerChargeType":
                        returnValue = _partnerChargeTypeCollection.Where(x => x.Value == value).Select(x => x.Key).SingleOrDefault();
                        break;
                    case "PartnerShortfallGuarantor":
                        returnValue = _partnerShortfallGuarantorCollection.Where(x => x.Value == value).Select(x => x.Key).SingleOrDefault();
                        break;
                    case "VATCashDestination":
                        returnValue = _vatCashDestinationCollection.Where(x => x.Value == value).Select(x => x.Key).SingleOrDefault();
                        break;
                    case "VATDeferredRemittance":
                        returnValue = _vatDeferredRemittanceCollection.Where(x => x.Value == value).Select(x => x.Key).SingleOrDefault();
                        break;
                    case "VATPaidBy":
                        returnValue = _vatPaidByCollection.Where(x => x.Value == value).Select(x => x.Key).SingleOrDefault();
                        break;
                    case "VATShortfallGuarantor":
                        returnValue = _vatShortfallGuarantorCollection.Where(x => x.Value == value).Select(x => x.Key).SingleOrDefault();
                        break;
                    case "GPTCashDestination":
                        returnValue = _gptCashDestinationCollection.Where(x => x.Value == value).Select(x => x.Key).SingleOrDefault();
                        break;
                    case "GPTDeferredRemittance":
                        returnValue = _gptDeferredRemittanceCollection.Where(x => x.Value == value).Select(x => x.Key).SingleOrDefault();
                        break;
                    case "OtherChargesCashDestination":
                        returnValue = _otherChargesCashDestinationCollection.Where(x => x.Value == value).Select(x => x.Key).SingleOrDefault();
                        break;
                    case "OtherChargesDeferredRemittance":
                        returnValue = _otherChargesDeferredRemittanceCollection.Where(x => x.Value == value).Select(x => x.Key).SingleOrDefault();
                        break;
                    case "OtherChargesPaidBy":
                        returnValue = _otherChargesPaidByCollection.Where(x => x.Value == value).Select(x => x.Key).SingleOrDefault();
                        break;
                    case "OtherChargesGuarantor":
                        returnValue = _otherChargesGuarantorCollection.Where(x => x.Value == value).Select(x => x.Key).SingleOrDefault();
                        break;
                    case "OtherChargesFrequency":
                        returnValue = _otherChargesFrequencyCollection.Where(x => x.Value == value).Select(x => x.Key).SingleOrDefault();
                        break;
                    case "ShareSchedule":
                        returnValue = _shareScheduleCollection.Where(x => x.Value == value).Select(x => x.Key).SingleOrDefault();
                        break;
                    case "RentSchedule":
                        returnValue = _rentScheduleCollection.Where(x => x.Value == value).Select(x => x.Key).SingleOrDefault();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return returnValue;
        }

        private bool CheckValidityOfTerms()
        {
            return true;
        }

        private void PopulateTerms()
        {
            string machineType = string.Empty;

            try
            {
                LogManager.WriteLog("Inside PopulateTerms...", LogManager.enumLogLevel.Info);

                _termsProfileResult = _termsBusiness.GetTermsProfileInfoForTermsCalculation(_termsProfileID);

                if (_termsProfileResult != null)
                {
                    machineType = _termsProfileResult.Machine_Type_ID > 0 ? _termsProfileResult.Machine_Type_Code : "Default";
                    this.Text = string.Format("Terms Profile - {0} [{1}]", _termsProfileResult.Terms_Group_Name, machineType);

                    //######### Partners #########

                    //Supplier                   
                    dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerOrder"].Value = _termsProfileResult.Terms_Profile_Partners_Supplier_Index != null ? _termsProfileResult.Terms_Profile_Partners_Supplier_Index.Value : 0;
                    dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerEnabled"].Value = _termsProfileResult.Terms_Profile_Partners_Supplier_Use != null ? _termsProfileResult.Terms_Profile_Partners_Supplier_Use.Value : false;
                    ((DataGridViewComboBoxCell)dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerCashDestination"]).Value = GetValueFromKey("PartnerCashDestination", "Supplier", _termsProfileResult.Terms_Profile_Partners_Supplier_Cash_Destination.Value);
                    ((DataGridViewComboBoxCell)dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerDeferredRemittance"]).Value = GetValueFromKey("PartnerDeferredRemittance", "Supplier", _termsProfileResult.Terms_Profile_Partners_Supplier_Deferred_Remittance.Value);
                    ((DataGridViewComboBoxCell)dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerType"]).Value = GetValueFromKey("PartnerChargeType", "Supplier", _termsProfileResult.Terms_Profile_Partners_Supplier_Type.Value);
                    _partnerSupplierValue = _termsProfileResult.Terms_Profile_Partners_Supplier_Value != null ? _termsProfileResult.Terms_Profile_Partners_Supplier_Value : 0;
                    dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerRentGuaranteed"].Value = _termsProfileResult.Terms_Profile_Partners_Supplier_Value_Guaranteed != null ? _termsProfileResult.Terms_Profile_Partners_Supplier_Value_Guaranteed.Value : false;
                    dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerShare"].Value = _termsProfileResult.Terms_Profile_Partners_Supplier_Share != null ? _termsProfileResult.Terms_Profile_Partners_Supplier_Share.Value : 0;
                    dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerShareGuaranteed"].Value = _termsProfileResult.Terms_Profile_Partners_Supplier_Share_Guaranteed != null ? _termsProfileResult.Terms_Profile_Partners_Supplier_Share_Guaranteed.Value : false;
                    _partnerShareSchedule = _termsProfileResult.Terms_Profile_Partners_Supplier_Share_Schedule != null ? _termsProfileResult.Terms_Profile_Partners_Supplier_Share_Schedule : 0;
                    _partnerRentSchedule = _termsProfileResult.Terms_Profile_Partners_Supplier_Rent_Schedule != null ? _termsProfileResult.Terms_Profile_Partners_Supplier_Rent_Schedule : 0;
                    ((DataGridViewComboBoxCell)dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerShortfallGuarantor"]).Value = GetValueFromKey("PartnerShortfallGuarantor", "Supplier", _termsProfileResult.Terms_Profile_Partners_Supplier_Guarantor.Value);
                    dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerPercentage"].Value = _termsProfileResult.Terms_Profile_Partners_Supplier_Guarantor_Percentage != null ? _termsProfileResult.Terms_Profile_Partners_Supplier_Guarantor_Percentage.Value : 0;

                    //Site                   
                    dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerOrder"].Value = _termsProfileResult.Terms_Profile_Partners_Site_Index != null ? _termsProfileResult.Terms_Profile_Partners_Site_Index.Value : 0;
                    dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerEnabled"].Value = _termsProfileResult.Terms_Profile_Partners_Site_Use != null ? _termsProfileResult.Terms_Profile_Partners_Site_Use.Value : false;
                    ((DataGridViewComboBoxCell)dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerCashDestination"]).Value = GetValueFromKey("PartnerCashDestination", "Site", _termsProfileResult.Terms_Profile_Partners_Site_Cash_Destination.Value);
                    ((DataGridViewComboBoxCell)dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerDeferredRemittance"]).Value = GetValueFromKey("PartnerDeferredRemittance", "Site", _termsProfileResult.Terms_Profile_Partners_Site_Deferred_Remittance.Value);
                    ((DataGridViewComboBoxCell)dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerType"]).Value = GetValueFromKey("PartnerChargeType", "Site", _termsProfileResult.Terms_Profile_Partners_Site_Type.Value);
                    dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerValue"].Value = _termsProfileResult.Terms_Profile_Partners_Site_Value != null ? _termsProfileResult.Terms_Profile_Partners_Site_Value.Value : 0;
                    dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerRentGuaranteed"].Value = _termsProfileResult.Terms_Profile_Partners_Site_Value_Guaranteed != null ? _termsProfileResult.Terms_Profile_Partners_Site_Value_Guaranteed.Value : false;
                    dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerShare"].Value = _termsProfileResult.Terms_Profile_Partners_Site_Share != null ? _termsProfileResult.Terms_Profile_Partners_Site_Share.Value : 0;
                    dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerShareGuaranteed"].Value = _termsProfileResult.Terms_Profile_Partners_Site_Share_Guaranteed != null ? _termsProfileResult.Terms_Profile_Partners_Site_Share_Guaranteed.Value : false;
                    ((DataGridViewComboBoxCell)dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerShortfallGuarantor"]).Value = GetValueFromKey("PartnerShortfallGuarantor", "Site", _termsProfileResult.Terms_Profile_Partners_Site_Guarantor.Value);
                    dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerPercentage"].Value = _termsProfileResult.Terms_Profile_Partners_Site_Guarantor_Percentage != null ? _termsProfileResult.Terms_Profile_Partners_Site_Guarantor_Percentage.Value : 0;

                    //Group
                    dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerOrder"].Value = _termsProfileResult.Terms_Profile_Partners_Group_Index != null ? _termsProfileResult.Terms_Profile_Partners_Group_Index.Value : 0;
                    dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerEnabled"].Value = _termsProfileResult.Terms_Profile_Partners_Group_Use != null ? _termsProfileResult.Terms_Profile_Partners_Group_Use.Value : false;
                    ((DataGridViewComboBoxCell)dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerCashDestination"]).Value = GetValueFromKey("PartnerCashDestination", "Site Group", _termsProfileResult.Terms_Profile_Partners_Group_Cash_Destination.Value);
                    ((DataGridViewComboBoxCell)dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerDeferredRemittance"]).Value = GetValueFromKey("PartnerDeferredRemittance", "Site Group", _termsProfileResult.Terms_Profile_Partners_Group_Deferred_Remittance.Value);
                    ((DataGridViewComboBoxCell)dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerType"]).Value = GetValueFromKey("PartnerChargeType", "Site Group", _termsProfileResult.Terms_Profile_Partners_Group_Type.Value);
                    dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerValue"].Value = _termsProfileResult.Terms_Profile_Partners_Group_Value != null ? _termsProfileResult.Terms_Profile_Partners_Group_Value.Value : 0;
                    dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerRentGuaranteed"].Value = _termsProfileResult.Terms_Profile_Partners_Group_Value_Guaranteed != null ? _termsProfileResult.Terms_Profile_Partners_Group_Value_Guaranteed.Value : false;
                    dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerShare"].Value = _termsProfileResult.Terms_Profile_Partners_Group_Share != null ? _termsProfileResult.Terms_Profile_Partners_Group_Share.Value : 0;
                    dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerShareGuaranteed"].Value = _termsProfileResult.Terms_Profile_Partners_Group_Share_Guaranteed != null ? _termsProfileResult.Terms_Profile_Partners_Group_Share_Guaranteed.Value : false;
                    ((DataGridViewComboBoxCell)dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerShortfallGuarantor"]).Value = GetValueFromKey("PartnerShortfallGuarantor", "Site Group", _termsProfileResult.Terms_Profile_Partners_Group_Guarantor.Value);
                    dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerPercentage"].Value = _termsProfileResult.Terms_Profile_Partners_Group_Guarantor_Percentage != null ? _termsProfileResult.Terms_Profile_Partners_Group_Guarantor_Percentage.Value : 0;

                    //Sec Group
                    dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerOrder"].Value = _termsProfileResult.Terms_Profile_Partners_Sec_Group_Index != null ? _termsProfileResult.Terms_Profile_Partners_Sec_Group_Index.Value : 0;
                    dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerEnabled"].Value = _termsProfileResult.Terms_Profile_Partners_Sec_Group_Use != null ? _termsProfileResult.Terms_Profile_Partners_Sec_Group_Use.Value : false;
                    ((DataGridViewComboBoxCell)dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerCashDestination"]).Value = GetValueFromKey("PartnerCashDestination", "Sec Brewery", _termsProfileResult.Terms_Profile_Partners_Sec_Group_Cash_Destination.Value);
                    ((DataGridViewComboBoxCell)dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerDeferredRemittance"]).Value = GetValueFromKey("PartnerDeferredRemittance", "Sec Brewery", _termsProfileResult.Terms_Profile_Partners_Sec_Group_Deferred_Remittance.Value);
                    ((DataGridViewComboBoxCell)dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerType"]).Value = GetValueFromKey("PartnerChargeType", "Sec Brewery", _termsProfileResult.Terms_Profile_Partners_Sec_Group_Type.Value);
                    dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerValue"].Value = _termsProfileResult.Terms_Profile_Partners_Sec_Group_Value != null ? _termsProfileResult.Terms_Profile_Partners_Sec_Group_Value.Value : 0;
                    dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerRentGuaranteed"].Value = _termsProfileResult.Terms_Profile_Partners_Sec_Group_Value_Guaranteed != null ? _termsProfileResult.Terms_Profile_Partners_Sec_Group_Value_Guaranteed.Value : false;
                    dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerShare"].Value = _termsProfileResult.Terms_Profile_Partners_Sec_Group_Share != null ? _termsProfileResult.Terms_Profile_Partners_Sec_Group_Share.Value : 0;
                    dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerShareGuaranteed"].Value = _termsProfileResult.Terms_Profile_Partners_Sec_Group_Share_Guaranteed != null ? _termsProfileResult.Terms_Profile_Partners_Sec_Group_Share_Guaranteed.Value : false;
                    ((DataGridViewComboBoxCell)dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerShortfallGuarantor"]).Value = GetValueFromKey("PartnerShortfallGuarantor", "Sec Brewery", _termsProfileResult.Terms_Profile_Partners_Sec_Group_Guarantor.Value);
                    dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerPercentage"].Value = _termsProfileResult.Terms_Profile_Partners_Sec_Group_Guarantor_Percentage != null ? _termsProfileResult.Terms_Profile_Partners_Sec_Group_Guarantor_Percentage.Value : 0;

                    //######### VAT #########

                    //Output                   
                    dgvVAT.Rows[OUTPUT_VAT_ROW_INDEX].Cells["clm_VATOrder"].Value = _termsProfileResult.Terms_Profile_VAT_Output_Index != null ? _termsProfileResult.Terms_Profile_VAT_Output_Index.Value : 0;
                    dgvVAT.Rows[OUTPUT_VAT_ROW_INDEX].Cells["clm_VATEnabled"].Value = _termsProfileResult.Terms_Profile_VAT_Output_Use != null ? _termsProfileResult.Terms_Profile_VAT_Output_Use.Value : false;
                    dgvVAT.Rows[OUTPUT_VAT_ROW_INDEX].Cells["clm_VATCashDestination"].Value = GetValueFromKey("VATCashDestination", "Output VAT", _termsProfileResult.Terms_Profile_VAT_Output_Cash_Destination.Value);
                    dgvVAT.Rows[OUTPUT_VAT_ROW_INDEX].Cells["clm_VATDeferredRemittance"].Value = GetValueFromKey("VATDeferredRemittance", "Output VAT", _termsProfileResult.Terms_Profile_VAT_Output_Deferred_Remittance.Value);

                    //Supplier
                    dgvVAT.Rows[INPUT_VAT_OPERATOR_ROW_INDEX].Cells["clm_VATOrder"].Value = _termsProfileResult.Terms_Profile_VAT_Supplier_Index != null ? _termsProfileResult.Terms_Profile_VAT_Supplier_Index.Value : 0;
                    dgvVAT.Rows[INPUT_VAT_OPERATOR_ROW_INDEX].Cells["clm_VATEnabled"].Value = _termsProfileResult.Terms_Profile_VAT_Supplier_Use != null ? _termsProfileResult.Terms_Profile_VAT_Supplier_Use.Value : false;
                    dgvVAT.Rows[INPUT_VAT_OPERATOR_ROW_INDEX].Cells["clm_VATCashDestination"].Value = GetValueFromKey("VATCashDestination", "Input VAT - Operator", _termsProfileResult.Terms_Profile_VAT_Supplier_Cash_Destination.Value);
                    dgvVAT.Rows[INPUT_VAT_OPERATOR_ROW_INDEX].Cells["clm_VATDeferredRemittance"].Value = GetValueFromKey("VATDeferredRemittance", "Input VAT - Operator", _termsProfileResult.Terms_Profile_VAT_Supplier_Deferred_Remittance.Value);
                    dgvVAT.Rows[INPUT_VAT_OPERATOR_ROW_INDEX].Cells["clm_VATPaidBy"].Value = GetValueFromKey("VATPaidBy", "Input VAT - Operator", _termsProfileResult.Terms_Profile_VAT_Supplier_Paid_By.Value);
                    dgvVAT.Rows[INPUT_VAT_OPERATOR_ROW_INDEX].Cells["clm_VATShortfallGuarantor"].Value = GetValueFromKey("VATShortfallGuarantor", "Input VAT - Operator", _termsProfileResult.Terms_Profile_VAT_Supplier_Guarantor.Value);

                    //Site
                    dgvVAT.Rows[INPUT_VAT_SITE_ROW_INDEX].Cells["clm_VATOrder"].Value = _termsProfileResult.Terms_Profile_VAT_Site_Index != null ? _termsProfileResult.Terms_Profile_VAT_Site_Index.Value : 0;
                    dgvVAT.Rows[INPUT_VAT_SITE_ROW_INDEX].Cells["clm_VATEnabled"].Value = _termsProfileResult.Terms_Profile_VAT_Site_Use != null ? _termsProfileResult.Terms_Profile_VAT_Site_Use.Value : false;
                    dgvVAT.Rows[INPUT_VAT_SITE_ROW_INDEX].Cells["clm_VATCashDestination"].Value = GetValueFromKey("VATCashDestination", "Input VAT - Site", _termsProfileResult.Terms_Profile_VAT_Site_Cash_Destination.Value);
                    dgvVAT.Rows[INPUT_VAT_SITE_ROW_INDEX].Cells["clm_VATDeferredRemittance"].Value = GetValueFromKey("VATDeferredRemittance", "Input VAT - Site", _termsProfileResult.Terms_Profile_VAT_Site_Deferred_Remittance.Value);
                    dgvVAT.Rows[INPUT_VAT_SITE_ROW_INDEX].Cells["clm_VATPaidBy"].Value = GetValueFromKey("VATPaidBy", "Input VAT - Site", _termsProfileResult.Terms_Profile_VAT_Site_Paid_By.Value);
                    dgvVAT.Rows[INPUT_VAT_SITE_ROW_INDEX].Cells["clm_VATShortfallGuarantor"].Value = GetValueFromKey("VATShortfallGuarantor", "Input VAT - Site", _termsProfileResult.Terms_Profile_VAT_Site_Guarantor.Value);

                    //Site Group
                    dgvVAT.Rows[INPUT_VAT_SITE_GROUP_ROW_INDEX].Cells["clm_VATOrder"].Value = _termsProfileResult.Terms_Profile_VAT_Group_Index != null ? _termsProfileResult.Terms_Profile_VAT_Group_Index.Value : 0;
                    dgvVAT.Rows[INPUT_VAT_SITE_GROUP_ROW_INDEX].Cells["clm_VATEnabled"].Value = _termsProfileResult.Terms_Profile_VAT_Group_Use != null ? _termsProfileResult.Terms_Profile_VAT_Group_Use.Value : false;
                    dgvVAT.Rows[INPUT_VAT_SITE_GROUP_ROW_INDEX].Cells["clm_VATCashDestination"].Value = GetValueFromKey("VATCashDestination", "Input VAT - Site Group", _termsProfileResult.Terms_Profile_VAT_Group_Cash_Destination.Value);
                    dgvVAT.Rows[INPUT_VAT_SITE_GROUP_ROW_INDEX].Cells["clm_VATDeferredRemittance"].Value = GetValueFromKey("VATDeferredRemittance", "Input VAT - Site Group", _termsProfileResult.Terms_Profile_VAT_Group_Deferred_Remittance.Value);
                    dgvVAT.Rows[INPUT_VAT_SITE_GROUP_ROW_INDEX].Cells["clm_VATPaidBy"].Value = GetValueFromKey("VATPaidBy", "Input VAT - Site Group", _termsProfileResult.Terms_Profile_VAT_Group_Paid_By.Value);
                    dgvVAT.Rows[INPUT_VAT_SITE_GROUP_ROW_INDEX].Cells["clm_VATShortfallGuarantor"].Value = GetValueFromKey("VATShortfallGuarantor", "Input VAT - Site Group", _termsProfileResult.Terms_Profile_VAT_Group_Guarantor.Value);

                    //Sec Site Group
                    dgvVAT.Rows[INPUT_VAT_SEC_SITE_GROUP_ROW_INDEX].Cells["clm_VATOrder"].Value = _termsProfileResult.Terms_Profile_VAT_Sec_Group_Index != null ? _termsProfileResult.Terms_Profile_VAT_Sec_Group_Index.Value : 0;
                    dgvVAT.Rows[INPUT_VAT_SEC_SITE_GROUP_ROW_INDEX].Cells["clm_VATEnabled"].Value = _termsProfileResult.Terms_Profile_VAT_Sec_Group_Use != null ? _termsProfileResult.Terms_Profile_VAT_Sec_Group_Use.Value : false;
                    dgvVAT.Rows[INPUT_VAT_SEC_SITE_GROUP_ROW_INDEX].Cells["clm_VATCashDestination"].Value = GetValueFromKey("VATCashDestination", "Input VAT - Sec Brewery", _termsProfileResult.Terms_Profile_VAT_Sec_Group_Cash_Destination.Value);
                    dgvVAT.Rows[INPUT_VAT_SEC_SITE_GROUP_ROW_INDEX].Cells["clm_VATDeferredRemittance"].Value = GetValueFromKey("VATDeferredRemittance", "Input VAT - Sec Brewery", _termsProfileResult.Terms_Profile_VAT_Sec_Group_Deferred_Remittance.Value);
                    dgvVAT.Rows[INPUT_VAT_SEC_SITE_GROUP_ROW_INDEX].Cells["clm_VATPaidBy"].Value = GetValueFromKey("VATPaidBy", "Input VAT - Sec Brewery", _termsProfileResult.Terms_Profile_VAT_Sec_Group_Paid_By.Value);
                    dgvVAT.Rows[INPUT_VAT_SEC_SITE_GROUP_ROW_INDEX].Cells["clm_VATShortfallGuarantor"].Value = GetValueFromKey("VATShortfallGuarantor", "Input VAT - Sec Brewery", _termsProfileResult.Terms_Profile_VAT_Sec_Group_Guarantor.Value);

                    //########## GPT ##########

                    //GPT                   
                    dgvGPT.Rows[GPT_ROW_INDEX].Cells["clm_GPTOrder"].Value = _termsProfileResult.Terms_Profile_GPT_Index;
                    dgvGPT.Rows[GPT_ROW_INDEX].Cells["clm_GPTEnabled"].Value = _termsProfileResult.Terms_Profile_GPT_Use;
                    dgvGPT.Rows[GPT_ROW_INDEX].Cells["clm_GPTCashDestination"].Value = GetValueFromKey("GPTCashDestination", "GPT", _termsProfileResult.Terms_Profile_GPT_Cash_Destination);
                    dgvGPT.Rows[GPT_ROW_INDEX].Cells["clm_GPTDeferredRemittance"].Value = GetValueFromKey("GPTDeferredRemittance", "GPT", _termsProfileResult.Terms_Profile_GPT_Deferred_Remittance);

                    //########## OTHER ##########

                    //Licence                   
                    dgvOtherCharges.Rows[OTHER_LICENCE_ROW_INDEX].Cells["clm_OtherChargesOrder"].Value = _termsProfileResult.Terms_Profile_Other_Licence_Index != null ? _termsProfileResult.Terms_Profile_Other_Licence_Index.Value : 0;
                    dgvOtherCharges.Rows[OTHER_LICENCE_ROW_INDEX].Cells["clm_OtherChargesEnabled"].Value = _termsProfileResult.Terms_Profile_Other_Licence_Index != null ? _termsProfileResult.Terms_Profile_Other_Licence_Index.Value : 0;
                    dgvOtherCharges.Rows[OTHER_LICENCE_ROW_INDEX].Cells["clm_OtherChargesVAT"].Value = _termsProfileResult.Terms_Profile_Other_Licence_Vat != null ? _termsProfileResult.Terms_Profile_Other_Licence_Vat.Value : false;
                    dgvOtherCharges.Rows[OTHER_LICENCE_ROW_INDEX].Cells["clm_OtherChargesCashDestination"].Value = GetValueFromKey("OtherChargesCashDestination", "Licence", _termsProfileResult.Terms_Profile_Other_Licence_Cash_Destination.Value);
                    dgvOtherCharges.Rows[OTHER_LICENCE_ROW_INDEX].Cells["clm_OtherChargesDeferredRemittance"].Value = GetValueFromKey("OtherChargesDeferredRemittance", "Licence", _termsProfileResult.Terms_Profile_Other_Licence_Deferred_Remittance.Value);
                    dgvOtherCharges.Rows[OTHER_LICENCE_ROW_INDEX].Cells["clm_OtherChargesCharge"].Value = _termsProfileResult.Terms_Profile_Other_Licence_Charge != null ? _termsProfileResult.Terms_Profile_Other_Licence_Charge.Value : 0;
                    dgvOtherCharges.Rows[OTHER_LICENCE_ROW_INDEX].Cells["clm_OtherChargesPaidBy"].Value = GetValueFromKey("OtherChargesPaidBy", "Licence", _termsProfileResult.Terms_Profile_Other_Licence_Paid_By.Value);
                    dgvOtherCharges.Rows[OTHER_LICENCE_ROW_INDEX].Cells["clm_OtherChargesShortfallGuarantor"].Value = GetValueFromKey("OtherChargesGuarantor", "Licence", _termsProfileResult.Terms_Profile_Other_Licence_Guarantor.Value);
                    dgvOtherCharges.Rows[OTHER_LICENCE_ROW_INDEX].Cells["clm_OtherChargesFrequency"].Value = GetValueFromKey("OtherChargesFrequency", "Licence", _termsProfileResult.Terms_Profile_Other_Licence_Frequency.Value);

                    //Prize
                    dgvOtherCharges.Rows[OTHER_PRIZE_ROW_INDEX].Cells["clm_OtherChargesOrder"].Value = _termsProfileResult.Terms_Profile_Other_Prize_Index != null ? _termsProfileResult.Terms_Profile_Other_Prize_Index.Value : 0;
                    dgvOtherCharges.Rows[OTHER_PRIZE_ROW_INDEX].Cells["clm_OtherChargesEnabled"].Value = _termsProfileResult.Terms_Profile_Other_Prize_Index != null ? _termsProfileResult.Terms_Profile_Other_Prize_Index.Value : 0;
                    dgvOtherCharges.Rows[OTHER_PRIZE_ROW_INDEX].Cells["clm_OtherChargesVAT"].Value = _termsProfileResult.Terms_Profile_Other_Prize_Vat != null ? _termsProfileResult.Terms_Profile_Other_Prize_Vat.Value : false;
                    dgvOtherCharges.Rows[OTHER_PRIZE_ROW_INDEX].Cells["clm_OtherChargesCashDestination"].Value = GetValueFromKey("OtherChargesCashDestination", "Prize", _termsProfileResult.Terms_Profile_Other_Prize_Cash_Destination.Value);
                    dgvOtherCharges.Rows[OTHER_PRIZE_ROW_INDEX].Cells["clm_OtherChargesDeferredRemittance"].Value = GetValueFromKey("OtherChargesDeferredRemittance", "Prize", _termsProfileResult.Terms_Profile_Other_Prize_Deferred_Remittance.Value);
                    dgvOtherCharges.Rows[OTHER_PRIZE_ROW_INDEX].Cells["clm_OtherChargesCharge"].Value = _termsProfileResult.Terms_Profile_Other_Prize_Charge != null ? _termsProfileResult.Terms_Profile_Other_Prize_Charge.Value : 0;
                    dgvOtherCharges.Rows[OTHER_PRIZE_ROW_INDEX].Cells["clm_OtherChargesPaidBy"].Value = GetValueFromKey("OtherChargesPaidBy", "Prize", _termsProfileResult.Terms_Profile_Other_Prize_Paid_By.Value);
                    dgvOtherCharges.Rows[OTHER_PRIZE_ROW_INDEX].Cells["clm_OtherChargesShortfallGuarantor"].Value = GetValueFromKey("OtherChargesGuarantor", "Prize", _termsProfileResult.Terms_Profile_Other_Prize_Guarantor.Value);
                    dgvOtherCharges.Rows[OTHER_PRIZE_ROW_INDEX].Cells["clm_OtherChargesFrequency"].Value = GetValueFromKey("OtherChargesFrequency", "Prize", _termsProfileResult.Terms_Profile_Other_Prize_Frequency.Value);

                    //Consultancy
                    dgvOtherCharges.Rows[OTHER_CONSULTANCY_ROW_INDEX].Cells["clm_OtherChargesOrder"].Value = _termsProfileResult.Terms_Profile_Other_Consultancy_Index != null ? _termsProfileResult.Terms_Profile_Other_Consultancy_Index.Value : 0;
                    dgvOtherCharges.Rows[OTHER_CONSULTANCY_ROW_INDEX].Cells["clm_OtherChargesEnabled"].Value = _termsProfileResult.Terms_Profile_Other_Consultancy_Index != null ? _termsProfileResult.Terms_Profile_Other_Consultancy_Index.Value : 0;
                    dgvOtherCharges.Rows[OTHER_CONSULTANCY_ROW_INDEX].Cells["clm_OtherChargesVAT"].Value = _termsProfileResult.Terms_Profile_Other_Consultancy_Vat != null ? _termsProfileResult.Terms_Profile_Other_Consultancy_Vat.Value : false;
                    dgvOtherCharges.Rows[OTHER_CONSULTANCY_ROW_INDEX].Cells["clm_OtherChargesCashDestination"].Value = GetValueFromKey("OtherChargesCashDestination", "Consultancy", _termsProfileResult.Terms_Profile_Other_Consultancy_Cash_Destination.Value);
                    dgvOtherCharges.Rows[OTHER_CONSULTANCY_ROW_INDEX].Cells["clm_OtherChargesDeferredRemittance"].Value = GetValueFromKey("OtherChargesDeferredRemittance", "Consultancy", _termsProfileResult.Terms_Profile_Other_Consultancy_Deferred_Remittance.Value);
                    dgvOtherCharges.Rows[OTHER_CONSULTANCY_ROW_INDEX].Cells["clm_OtherChargesCharge"].Value = _termsProfileResult.Terms_Profile_Other_Consultancy_Charge != null ? _termsProfileResult.Terms_Profile_Other_Consultancy_Charge.Value : 0;
                    dgvOtherCharges.Rows[OTHER_CONSULTANCY_ROW_INDEX].Cells["clm_OtherChargesPaidBy"].Value = GetValueFromKey("OtherChargesPaidBy", "Consultancy", _termsProfileResult.Terms_Profile_Other_Consultancy_Paid_By.Value);
                    dgvOtherCharges.Rows[OTHER_CONSULTANCY_ROW_INDEX].Cells["clm_OtherChargesShortfallGuarantor"].Value = GetValueFromKey("OtherChargesGuarantor", "Consultancy", _termsProfileResult.Terms_Profile_Other_Consultancy_Guarantor.Value);
                    dgvOtherCharges.Rows[OTHER_CONSULTANCY_ROW_INDEX].Cells["clm_OtherChargesFrequency"].Value = GetValueFromKey("OtherChargesFrequency", "Consultancy", _termsProfileResult.Terms_Profile_Other_Consultancy_Frequency.Value);

                    //Royalty
                    dgvOtherCharges.Rows[OTHER_ROYALTY_ROW_INDEX].Cells["clm_OtherChargesOrder"].Value = _termsProfileResult.Terms_Profile_Other_Royalty_Index != null ? _termsProfileResult.Terms_Profile_Other_Royalty_Index.Value : 0;
                    dgvOtherCharges.Rows[OTHER_ROYALTY_ROW_INDEX].Cells["clm_OtherChargesEnabled"].Value = _termsProfileResult.Terms_Profile_Other_Royalty_Index != null ? _termsProfileResult.Terms_Profile_Other_Royalty_Index.Value : 0;
                    dgvOtherCharges.Rows[OTHER_ROYALTY_ROW_INDEX].Cells["clm_OtherChargesVAT"].Value = _termsProfileResult.Terms_Profile_Other_Royalty_Vat != null ? _termsProfileResult.Terms_Profile_Other_Royalty_Vat.Value : false;
                    dgvOtherCharges.Rows[OTHER_ROYALTY_ROW_INDEX].Cells["clm_OtherChargesCashDestination"].Value = GetValueFromKey("OtherChargesCashDestination", "Royalty", _termsProfileResult.Terms_Profile_Other_Royalty_Cash_Destination.Value);
                    dgvOtherCharges.Rows[OTHER_ROYALTY_ROW_INDEX].Cells["clm_OtherChargesDeferredRemittance"].Value = GetValueFromKey("OtherChargesDeferredRemittance", "Royalty", _termsProfileResult.Terms_Profile_Other_Royalty_Deferred_Remittance.Value);
                    dgvOtherCharges.Rows[OTHER_ROYALTY_ROW_INDEX].Cells["clm_OtherChargesCharge"].Value = _termsProfileResult.Terms_Profile_Other_Royalty_Charge != null ? _termsProfileResult.Terms_Profile_Other_Royalty_Charge.Value : 0;
                    dgvOtherCharges.Rows[OTHER_ROYALTY_ROW_INDEX].Cells["clm_OtherChargesPaidBy"].Value = GetValueFromKey("OtherChargesPaidBy", "Royalty", _termsProfileResult.Terms_Profile_Other_Royalty_Paid_By.Value);
                    dgvOtherCharges.Rows[OTHER_ROYALTY_ROW_INDEX].Cells["clm_OtherChargesShortfallGuarantor"].Value = GetValueFromKey("OtherChargesGuarantor", "Royalty", _termsProfileResult.Terms_Profile_Other_Royalty_Guarantor.Value);
                    dgvOtherCharges.Rows[OTHER_ROYALTY_ROW_INDEX].Cells["clm_OtherChargesFrequency"].Value = GetValueFromKey("OtherChargesFrequency", "Royalty", _termsProfileResult.Terms_Profile_Other_Royalty_Frequency.Value);

                    //Other1
                    dgvOtherCharges.Rows[OTHER_OTHER1_ROW_INDEX].Cells["clm_OtherChargesOrder"].Value = _termsProfileResult.Terms_Profile_Other_Other1_Index != null ? _termsProfileResult.Terms_Profile_Other_Other1_Index.Value : 0;
                    dgvOtherCharges.Rows[OTHER_OTHER1_ROW_INDEX].Cells["clm_OtherChargesEnabled"].Value = _termsProfileResult.Terms_Profile_Other_Other1_Index != null ? _termsProfileResult.Terms_Profile_Other_Other1_Index.Value : 0;
                    dgvOtherCharges.Rows[OTHER_OTHER1_ROW_INDEX].Cells["clm_OtherChargesVAT"].Value = _termsProfileResult.Terms_Profile_Other_Other1_Vat != null ? _termsProfileResult.Terms_Profile_Other_Other1_Vat.Value : false;
                    dgvOtherCharges.Rows[OTHER_OTHER1_ROW_INDEX].Cells["clm_OtherChargesCashDestination"].Value = GetValueFromKey("OtherChargesCashDestination", "Other1", _termsProfileResult.Terms_Profile_Other_Other1_Cash_Destination.Value);
                    dgvOtherCharges.Rows[OTHER_OTHER1_ROW_INDEX].Cells["clm_OtherChargesDeferredRemittance"].Value = GetValueFromKey("OtherChargesDeferredRemittance", "Other1", _termsProfileResult.Terms_Profile_Other_Other1_Deferred_Remittance.Value);
                    dgvOtherCharges.Rows[OTHER_OTHER1_ROW_INDEX].Cells["clm_OtherChargesCharge"].Value = _termsProfileResult.Terms_Profile_Other_Other1_Charge != null ? _termsProfileResult.Terms_Profile_Other_Other1_Charge.Value : 0;
                    dgvOtherCharges.Rows[OTHER_OTHER1_ROW_INDEX].Cells["clm_OtherChargesPaidBy"].Value = GetValueFromKey("OtherChargesPaidBy", "Other1", _termsProfileResult.Terms_Profile_Other_Other1_Paid_By.Value);
                    dgvOtherCharges.Rows[OTHER_OTHER1_ROW_INDEX].Cells["clm_OtherChargesShortfallGuarantor"].Value = GetValueFromKey("OtherChargesGuarantor", "Other1", _termsProfileResult.Terms_Profile_Other_Other1_Guarantor.Value);
                    dgvOtherCharges.Rows[OTHER_OTHER1_ROW_INDEX].Cells["clm_OtherChargesFrequency"].Value = GetValueFromKey("OtherChargesFrequency", "Other1", _termsProfileResult.Terms_Profile_Other_Other1_Frequency.Value);

                    //Other2
                    dgvOtherCharges.Rows[OTHER_OTHER2_ROW_INDEX].Cells["clm_OtherChargesOrder"].Value = _termsProfileResult.Terms_Profile_Other_Other2_Index != null ? _termsProfileResult.Terms_Profile_Other_Other2_Index.Value : 0;
                    dgvOtherCharges.Rows[OTHER_OTHER2_ROW_INDEX].Cells["clm_OtherChargesEnabled"].Value = _termsProfileResult.Terms_Profile_Other_Other2_Index != null ? _termsProfileResult.Terms_Profile_Other_Other2_Index.Value : 0;
                    dgvOtherCharges.Rows[OTHER_OTHER2_ROW_INDEX].Cells["clm_OtherChargesVAT"].Value = _termsProfileResult.Terms_Profile_Other_Other2_Vat != null ? _termsProfileResult.Terms_Profile_Other_Other2_Vat.Value : false;
                    dgvOtherCharges.Rows[OTHER_OTHER2_ROW_INDEX].Cells["clm_OtherChargesCashDestination"].Value = GetValueFromKey("OtherChargesCashDestination", "Other2", _termsProfileResult.Terms_Profile_Other_Other2_Cash_Destination.Value);
                    dgvOtherCharges.Rows[OTHER_OTHER2_ROW_INDEX].Cells["clm_OtherChargesDeferredRemittance"].Value = GetValueFromKey("OtherChargesDeferredRemittance", "Other2", _termsProfileResult.Terms_Profile_Other_Other2_Deferred_Remittance.Value);
                    dgvOtherCharges.Rows[OTHER_OTHER2_ROW_INDEX].Cells["clm_OtherChargesCharge"].Value = _termsProfileResult.Terms_Profile_Other_Other2_Charge != null ? _termsProfileResult.Terms_Profile_Other_Other2_Charge.Value : 0;
                    dgvOtherCharges.Rows[OTHER_OTHER2_ROW_INDEX].Cells["clm_OtherChargesPaidBy"].Value = GetValueFromKey("OtherChargesPaidBy", "Other2", _termsProfileResult.Terms_Profile_Other_Other2_Paid_By.Value);
                    dgvOtherCharges.Rows[OTHER_OTHER2_ROW_INDEX].Cells["clm_OtherChargesShortfallGuarantor"].Value = GetValueFromKey("OtherChargesGuarantor", "Other2", _termsProfileResult.Terms_Profile_Other_Other2_Guarantor.Value);
                    dgvOtherCharges.Rows[OTHER_OTHER2_ROW_INDEX].Cells["clm_OtherChargesFrequency"].Value = GetValueFromKey("OtherChargesFrequency", "Other2", _termsProfileResult.Terms_Profile_Other_Other2_Frequency.Value);

                    SetSupplierValue();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private int UpdateTerms(int termsProfileID)
        {
            float? _partnerSupValue = null;
            int? _partnerSSchValue = null;
            int? _partnerRSchValue = null;
            int result = 0;

            try
            {
                LogManager.WriteLog("Inside UpdateTerms...", LogManager.enumLogLevel.Info);

                switch (dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerType"].Value.ToString())
                {
                    case "SSch":
                        _partnerSSchValue = Convert.ToInt32(GetKeyFromValue("ShareSchedule", dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerValue"].Value.ToString()));
                        _partnerRSchValue = _partnerRentSchedule;
                        _partnerSupValue = _partnerSupplierValue;
                        break;
                    case "RSch":
                        _partnerRSchValue = Convert.ToInt32(GetKeyFromValue("RentSchedule", dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerValue"].Value.ToString()));
                        _partnerSSchValue = _partnerShareSchedule;
                        _partnerSupValue = _partnerSupplierValue;
                        break;
                    default:
                        _partnerSupValue = float.Parse(dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerValue"].Value.ToString());
                        _partnerSSchValue = _partnerShareSchedule;
                        _partnerRSchValue = _partnerRentSchedule;
                        break;
                }

                //make sure previously updated changes are in sync
                if (_newTermsProfileResult != null)
                {
                    _termsProfileResult = _newTermsProfileResult;
                }

                //audit entity - after changes
                _newTermsProfileResult = new TermsProfileResult
                {
                    //No Change fields
                    Machine_Type_ID = _termsProfileResult.Machine_Type_ID,
                    Terms_Group_ID = _termsProfileResult.Terms_Group_ID,
                    Terms_Group_Name = _termsProfileResult.Terms_Group_Name,
                    Terms_Profile_London_Rent_Charge = _termsProfileResult.Terms_Profile_London_Rent_Charge,
                    Machine_Type_Code = _termsProfileResult.Machine_Type_Code,

                    //Updated fields
                    Terms_Profile_ID = termsProfileID,
                    Terms_Profile_Name = termsProfileID == 0 ? "New Terms Set" : _termsProfileResult.Terms_Profile_Name,
                    Terms_Profile_Partners_Supplier_Index = Convert.ToInt32(dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerOrder"].Value),
                    Terms_Profile_Partners_Supplier_Use = Convert.ToBoolean(dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerEnabled"].Value),
                    Terms_Profile_Partners_Supplier_Cash_Destination = GetKeyFromValue("PartnerCashDestination", dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerCashDestination"].Value.ToString()),
                    Terms_Profile_Partners_Supplier_Deferred_Remittance = GetKeyFromValue("PartnerDeferredRemittance", dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerDeferredRemittance"].Value.ToString()),
                    Terms_Profile_Partners_Supplier_Type = GetKeyFromValue("PartnerChargeType", dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerType"].Value.ToString()),
                    Terms_Profile_Partners_Supplier_Value = _partnerSupValue,
                    Terms_Profile_Partners_Supplier_Value_Guaranteed = Convert.ToBoolean(dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerRentGuaranteed"].Value),
                    Terms_Profile_Partners_Supplier_Share = Convert.ToInt32(dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerShare"].Value),
                    Terms_Profile_Partners_Supplier_Share_Guaranteed = Convert.ToBoolean(dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerShareGuaranteed"].Value),
                    Terms_Profile_Partners_Supplier_Share_Schedule = _partnerSSchValue,
                    Terms_Profile_Partners_Supplier_Rent_Schedule = _partnerRSchValue,
                    Terms_Profile_Partners_Supplier_Guarantor = GetKeyFromValue("PartnerShortfallGuarantor", dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerShortfallGuarantor"].Value.ToString()),
                    Terms_Profile_Partners_Supplier_Guarantor_Percentage = Convert.ToInt32(dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerPercentage"].Value),
                    Terms_Profile_Partners_Site_Index = Convert.ToInt32(dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerOrder"].Value),
                    Terms_Profile_Partners_Site_Use = Convert.ToBoolean(dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerEnabled"].Value),
                    Terms_Profile_Partners_Site_Cash_Destination = GetKeyFromValue("PartnerCashDestination", dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerCashDestination"].Value.ToString()),
                    Terms_Profile_Partners_Site_Deferred_Remittance = GetKeyFromValue("PartnerDeferredRemittance", dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerDeferredRemittance"].Value.ToString()),
                    Terms_Profile_Partners_Site_Type = GetKeyFromValue("PartnerChargeType", dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerType"].Value.ToString()),
                    Terms_Profile_Partners_Site_Value = Convert.ToInt32(dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerValue"].Value),
                    Terms_Profile_Partners_Site_Value_Guaranteed = Convert.ToBoolean(dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerRentGuaranteed"].Value),
                    Terms_Profile_Partners_Site_Share = Convert.ToInt32(dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerShare"].Value),
                    Terms_Profile_Partners_Site_Share_Guaranteed = Convert.ToBoolean(dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerShareGuaranteed"].Value),
                    Terms_Profile_Partners_Site_Guarantor = GetKeyFromValue("PartnerShortfallGuarantor", dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerShortfallGuarantor"].Value.ToString()),
                    Terms_Profile_Partners_Site_Guarantor_Percentage = Convert.ToInt32(dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerPercentage"].Value),
                    Terms_Profile_Partners_Group_Index = Convert.ToInt32(dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerOrder"].Value),
                    Terms_Profile_Partners_Group_Use = Convert.ToBoolean(dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerEnabled"].Value),
                    Terms_Profile_Partners_Group_Cash_Destination = GetKeyFromValue("PartnerCashDestination", dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerCashDestination"].Value.ToString()),
                    Terms_Profile_Partners_Group_Deferred_Remittance = GetKeyFromValue("PartnerDeferredRemittance", dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerDeferredRemittance"].Value.ToString()),
                    Terms_Profile_Partners_Group_Type = GetKeyFromValue("PartnerChargeType", dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerType"].Value.ToString()),
                    Terms_Profile_Partners_Group_Value = Convert.ToInt32(dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerValue"].Value),
                    Terms_Profile_Partners_Group_Value_Guaranteed = Convert.ToBoolean(dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerRentGuaranteed"].Value),
                    Terms_Profile_Partners_Group_Share = Convert.ToInt32(dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerShare"].Value),
                    Terms_Profile_Partners_Group_Share_Guaranteed = Convert.ToBoolean(dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerShareGuaranteed"].Value),
                    Terms_Profile_Partners_Group_Guarantor = GetKeyFromValue("PartnerShortfallGuarantor", dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerShortfallGuarantor"].Value.ToString()),
                    Terms_Profile_Partners_Group_Guarantor_Percentage = Convert.ToInt32(dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerPercentage"].Value),
                    Terms_Profile_Partners_Sec_Group_Index = Convert.ToInt32(dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerOrder"].Value),
                    Terms_Profile_Partners_Sec_Group_Use = Convert.ToBoolean(dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerEnabled"].Value),
                    Terms_Profile_Partners_Sec_Group_Cash_Destination = GetKeyFromValue("PartnerCashDestination", dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerCashDestination"].Value.ToString()),
                    Terms_Profile_Partners_Sec_Group_Deferred_Remittance = GetKeyFromValue("PartnerDeferredRemittance", dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerDeferredRemittance"].Value.ToString()),
                    Terms_Profile_Partners_Sec_Group_Type = GetKeyFromValue("PartnerChargeType", dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerType"].Value.ToString()),
                    Terms_Profile_Partners_Sec_Group_Value = Convert.ToInt32(dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerValue"].Value),
                    Terms_Profile_Partners_Sec_Group_Value_Guaranteed = Convert.ToBoolean(dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerRentGuaranteed"].Value),
                    Terms_Profile_Partners_Sec_Group_Share = Convert.ToInt32(dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerShare"].Value),
                    Terms_Profile_Partners_Sec_Group_Share_Guaranteed = Convert.ToBoolean(dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerShareGuaranteed"].Value),
                    Terms_Profile_Partners_Sec_Group_Guarantor = GetKeyFromValue("PartnerShortfallGuarantor", dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerShortfallGuarantor"].Value.ToString()),
                    Terms_Profile_Partners_Sec_Group_Guarantor_Percentage = Convert.ToInt32(dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerPercentage"].Value),
                    Terms_Profile_VAT_Output_Index = Convert.ToInt32(dgvVAT.Rows[OUTPUT_VAT_ROW_INDEX].Cells["clm_VATOrder"].Value),
                    Terms_Profile_VAT_Output_Use = Convert.ToBoolean(dgvVAT.Rows[OUTPUT_VAT_ROW_INDEX].Cells["clm_VATEnabled"].Value),
                    Terms_Profile_VAT_Output_Cash_Destination = GetKeyFromValue("VATCashDestination", dgvVAT.Rows[OUTPUT_VAT_ROW_INDEX].Cells["clm_VATCashDestination"].Value.ToString()),
                    Terms_Profile_VAT_Output_Deferred_Remittance = GetKeyFromValue("VATDeferredRemittance", dgvVAT.Rows[OUTPUT_VAT_ROW_INDEX].Cells["clm_VATDeferredRemittance"].Value.ToString()),
                    Terms_Profile_VAT_Supplier_Index = Convert.ToInt32(dgvVAT.Rows[INPUT_VAT_OPERATOR_ROW_INDEX].Cells["clm_VATOrder"].Value),
                    Terms_Profile_VAT_Supplier_Use = Convert.ToBoolean(dgvVAT.Rows[INPUT_VAT_OPERATOR_ROW_INDEX].Cells["clm_VATEnabled"].Value),
                    Terms_Profile_VAT_Supplier_Cash_Destination = GetKeyFromValue("VATCashDestination", dgvVAT.Rows[INPUT_VAT_OPERATOR_ROW_INDEX].Cells["clm_VATCashDestination"].Value.ToString()),
                    Terms_Profile_VAT_Supplier_Deferred_Remittance = GetKeyFromValue("VATDeferredRemittance", dgvVAT.Rows[INPUT_VAT_OPERATOR_ROW_INDEX].Cells["clm_VATDeferredRemittance"].Value.ToString()),
                    Terms_Profile_VAT_Supplier_Paid_By = GetKeyFromValue("VATPaidBy", dgvVAT.Rows[INPUT_VAT_OPERATOR_ROW_INDEX].Cells["clm_VATPaidBy"].Value.ToString()),
                    Terms_Profile_VAT_Supplier_Guarantor = GetKeyFromValue("VATShortfallGuarantor", dgvVAT.Rows[INPUT_VAT_OPERATOR_ROW_INDEX].Cells["clm_VATShortfallGuarantor"].Value.ToString()),
                    Terms_Profile_VAT_Site_Index = Convert.ToInt32(dgvVAT.Rows[INPUT_VAT_SITE_ROW_INDEX].Cells["clm_VATOrder"].Value),
                    Terms_Profile_VAT_Site_Use = Convert.ToBoolean(dgvVAT.Rows[INPUT_VAT_SITE_ROW_INDEX].Cells["clm_VATEnabled"].Value),
                    Terms_Profile_VAT_Site_Cash_Destination = GetKeyFromValue("VATCashDestination", dgvVAT.Rows[INPUT_VAT_SITE_ROW_INDEX].Cells["clm_VATCashDestination"].Value.ToString()),
                    Terms_Profile_VAT_Site_Deferred_Remittance = GetKeyFromValue("VATDeferredRemittance", dgvVAT.Rows[INPUT_VAT_SITE_ROW_INDEX].Cells["clm_VATDeferredRemittance"].Value.ToString()),
                    Terms_Profile_VAT_Site_Paid_By = GetKeyFromValue("VATPaidBy", dgvVAT.Rows[INPUT_VAT_SITE_ROW_INDEX].Cells["clm_VATPaidBy"].Value.ToString()),
                    Terms_Profile_VAT_Site_Guarantor = GetKeyFromValue("VATShortfallGuarantor", dgvVAT.Rows[INPUT_VAT_SITE_ROW_INDEX].Cells["clm_VATShortfallGuarantor"].Value.ToString()),
                    Terms_Profile_VAT_Group_Index = Convert.ToInt32(dgvVAT.Rows[INPUT_VAT_SITE_GROUP_ROW_INDEX].Cells["clm_VATOrder"].Value),
                    Terms_Profile_VAT_Group_Use = Convert.ToBoolean(dgvVAT.Rows[INPUT_VAT_SITE_GROUP_ROW_INDEX].Cells["clm_VATEnabled"].Value),
                    Terms_Profile_VAT_Group_Cash_Destination = GetKeyFromValue("VATCashDestination", dgvVAT.Rows[INPUT_VAT_SITE_GROUP_ROW_INDEX].Cells["clm_VATCashDestination"].Value.ToString()),
                    Terms_Profile_VAT_Group_Deferred_Remittance = GetKeyFromValue("VATDeferredRemittance", dgvVAT.Rows[INPUT_VAT_SITE_GROUP_ROW_INDEX].Cells["clm_VATDeferredRemittance"].Value.ToString()),
                    Terms_Profile_VAT_Group_Paid_By = GetKeyFromValue("VATPaidBy", dgvVAT.Rows[INPUT_VAT_SITE_GROUP_ROW_INDEX].Cells["clm_VATPaidBy"].Value.ToString()),
                    Terms_Profile_VAT_Group_Guarantor = GetKeyFromValue("VATShortfallGuarantor", dgvVAT.Rows[INPUT_VAT_SITE_GROUP_ROW_INDEX].Cells["clm_VATShortfallGuarantor"].Value.ToString()),
                    Terms_Profile_VAT_Sec_Group_Index = Convert.ToInt32(dgvVAT.Rows[INPUT_VAT_SEC_SITE_GROUP_ROW_INDEX].Cells["clm_VATOrder"].Value),
                    Terms_Profile_VAT_Sec_Group_Use = Convert.ToBoolean(dgvVAT.Rows[INPUT_VAT_SEC_SITE_GROUP_ROW_INDEX].Cells["clm_VATEnabled"].Value),
                    Terms_Profile_VAT_Sec_Group_Cash_Destination = GetKeyFromValue("VATCashDestination", dgvVAT.Rows[INPUT_VAT_SEC_SITE_GROUP_ROW_INDEX].Cells["clm_VATCashDestination"].Value.ToString()),
                    Terms_Profile_VAT_Sec_Group_Deferred_Remittance = GetKeyFromValue("VATDeferredRemittance", dgvVAT.Rows[INPUT_VAT_SEC_SITE_GROUP_ROW_INDEX].Cells["clm_VATDeferredRemittance"].Value.ToString()),
                    Terms_Profile_VAT_Sec_Group_Paid_By = GetKeyFromValue("VATPaidBy", dgvVAT.Rows[INPUT_VAT_SEC_SITE_GROUP_ROW_INDEX].Cells["clm_VATPaidBy"].Value.ToString()),
                    Terms_Profile_VAT_Sec_Group_Guarantor = GetKeyFromValue("VATShortfallGuarantor", dgvVAT.Rows[INPUT_VAT_SEC_SITE_GROUP_ROW_INDEX].Cells["clm_VATShortfallGuarantor"].Value.ToString()),
                    Terms_Profile_GPT_Index = Convert.ToInt32(dgvGPT.Rows[GPT_ROW_INDEX].Cells["clm_GPTOrder"].Value),
                    Terms_Profile_GPT_Use = Convert.ToInt32(dgvGPT.Rows[GPT_ROW_INDEX].Cells["clm_GPTEnabled"].Value),
                    Terms_Profile_GPT_Cash_Destination = GetKeyFromValue("GPTCashDestination", dgvGPT.Rows[GPT_ROW_INDEX].Cells["clm_GPTCashDestination"].Value.ToString()),
                    Terms_Profile_GPT_Deferred_Remittance = GetKeyFromValue("GPTDeferredRemittance", dgvGPT.Rows[GPT_ROW_INDEX].Cells["clm_GPTDeferredRemittance"].Value.ToString()),
                    Terms_Profile_Other_Licence_Index = Convert.ToInt32(dgvOtherCharges.Rows[OTHER_LICENCE_ROW_INDEX].Cells["clm_OtherChargesOrder"].Value),
                    Terms_Profile_Other_Licence_Use = Convert.ToBoolean(dgvOtherCharges.Rows[OTHER_LICENCE_ROW_INDEX].Cells["clm_OtherChargesEnabled"].Value),
                    Terms_Profile_Other_Licence_Vat = Convert.ToBoolean(dgvOtherCharges.Rows[OTHER_LICENCE_ROW_INDEX].Cells["clm_OtherChargesVAT"].Value),
                    Terms_Profile_Other_Licence_Cash_Destination = GetKeyFromValue("OtherChargesCashDestination", dgvOtherCharges.Rows[OTHER_LICENCE_ROW_INDEX].Cells["clm_OtherChargesCashDestination"].Value.ToString()),
                    Terms_Profile_Other_Licence_Deferred_Remittance = GetKeyFromValue("OtherChargesDeferredRemittance", dgvOtherCharges.Rows[OTHER_LICENCE_ROW_INDEX].Cells["clm_OtherChargesDeferredRemittance"].Value.ToString()),
                    Terms_Profile_Other_Licence_Charge = Convert.ToInt32(dgvOtherCharges.Rows[OTHER_LICENCE_ROW_INDEX].Cells["clm_OtherChargesCharge"].Value),
                    Terms_Profile_Other_Licence_Paid_By = GetKeyFromValue("OtherChargesPaidBy", dgvOtherCharges.Rows[OTHER_LICENCE_ROW_INDEX].Cells["clm_OtherChargesPaidBy"].Value.ToString()),
                    Terms_Profile_Other_Licence_Guarantor = GetKeyFromValue("OtherChargesGuarantor", dgvOtherCharges.Rows[OTHER_LICENCE_ROW_INDEX].Cells["clm_OtherChargesShortfallGuarantor"].Value.ToString()),
                    Terms_Profile_Other_Licence_Frequency = GetKeyFromValue("OtherChargesFrequency", dgvOtherCharges.Rows[OTHER_LICENCE_ROW_INDEX].Cells["clm_OtherChargesFrequency"].Value.ToString()),
                    Terms_Profile_Other_Prize_Index = Convert.ToInt32(dgvOtherCharges.Rows[OTHER_PRIZE_ROW_INDEX].Cells["clm_OtherChargesOrder"].Value),
                    Terms_Profile_Other_Prize_Use = Convert.ToBoolean(dgvOtherCharges.Rows[OTHER_PRIZE_ROW_INDEX].Cells["clm_OtherChargesEnabled"].Value),
                    Terms_Profile_Other_Prize_Vat = Convert.ToBoolean(dgvOtherCharges.Rows[OTHER_PRIZE_ROW_INDEX].Cells["clm_OtherChargesVAT"].Value),
                    Terms_Profile_Other_Prize_Cash_Destination = GetKeyFromValue("OtherChargesCashDestination", dgvOtherCharges.Rows[OTHER_PRIZE_ROW_INDEX].Cells["clm_OtherChargesCashDestination"].Value.ToString()),
                    Terms_Profile_Other_Prize_Deferred_Remittance = GetKeyFromValue("OtherChargesDeferredRemittance", dgvOtherCharges.Rows[OTHER_PRIZE_ROW_INDEX].Cells["clm_OtherChargesDeferredRemittance"].Value.ToString()),
                    Terms_Profile_Other_Prize_Charge = Convert.ToInt32(dgvOtherCharges.Rows[OTHER_PRIZE_ROW_INDEX].Cells["clm_OtherChargesCharge"].Value),
                    Terms_Profile_Other_Prize_Paid_By = GetKeyFromValue("OtherChargesPaidBy", dgvOtherCharges.Rows[OTHER_PRIZE_ROW_INDEX].Cells["clm_OtherChargesPaidBy"].Value.ToString()),
                    Terms_Profile_Other_Prize_Guarantor = GetKeyFromValue("OtherChargesGuarantor", dgvOtherCharges.Rows[OTHER_PRIZE_ROW_INDEX].Cells["clm_OtherChargesShortfallGuarantor"].Value.ToString()),
                    Terms_Profile_Other_Prize_Frequency = GetKeyFromValue("OtherChargesFrequency", dgvOtherCharges.Rows[OTHER_PRIZE_ROW_INDEX].Cells["clm_OtherChargesFrequency"].Value.ToString()),
                    Terms_Profile_Other_Consultancy_Index = Convert.ToInt32(dgvOtherCharges.Rows[OTHER_CONSULTANCY_ROW_INDEX].Cells["clm_OtherChargesOrder"].Value),
                    Terms_Profile_Other_Consultancy_Use = Convert.ToBoolean(dgvOtherCharges.Rows[OTHER_CONSULTANCY_ROW_INDEX].Cells["clm_OtherChargesEnabled"].Value),
                    Terms_Profile_Other_Consultancy_Vat = Convert.ToBoolean(dgvOtherCharges.Rows[OTHER_CONSULTANCY_ROW_INDEX].Cells["clm_OtherChargesVAT"].Value),
                    Terms_Profile_Other_Consultancy_Cash_Destination = GetKeyFromValue("OtherChargesCashDestination", dgvOtherCharges.Rows[OTHER_CONSULTANCY_ROW_INDEX].Cells["clm_OtherChargesCashDestination"].Value.ToString()),
                    Terms_Profile_Other_Consultancy_Deferred_Remittance = GetKeyFromValue("OtherChargesDeferredRemittance", dgvOtherCharges.Rows[OTHER_CONSULTANCY_ROW_INDEX].Cells["clm_OtherChargesDeferredRemittance"].Value.ToString()),
                    Terms_Profile_Other_Consultancy_Charge = Convert.ToInt32(dgvOtherCharges.Rows[OTHER_CONSULTANCY_ROW_INDEX].Cells["clm_OtherChargesCharge"].Value),
                    Terms_Profile_Other_Consultancy_Paid_By = GetKeyFromValue("OtherChargesPaidBy", dgvOtherCharges.Rows[OTHER_CONSULTANCY_ROW_INDEX].Cells["clm_OtherChargesPaidBy"].Value.ToString()),
                    Terms_Profile_Other_Consultancy_Guarantor = GetKeyFromValue("OtherChargesGuarantor", dgvOtherCharges.Rows[OTHER_CONSULTANCY_ROW_INDEX].Cells["clm_OtherChargesShortfallGuarantor"].Value.ToString()),
                    Terms_Profile_Other_Consultancy_Frequency = GetKeyFromValue("OtherChargesFrequency", dgvOtherCharges.Rows[OTHER_CONSULTANCY_ROW_INDEX].Cells["clm_OtherChargesFrequency"].Value.ToString()),
                    Terms_Profile_Other_Royalty_Index = Convert.ToInt32(dgvOtherCharges.Rows[OTHER_ROYALTY_ROW_INDEX].Cells["clm_OtherChargesOrder"].Value),
                    Terms_Profile_Other_Royalty_Use = Convert.ToBoolean(dgvOtherCharges.Rows[OTHER_ROYALTY_ROW_INDEX].Cells["clm_OtherChargesEnabled"].Value),
                    Terms_Profile_Other_Royalty_Vat = Convert.ToBoolean(dgvOtherCharges.Rows[OTHER_ROYALTY_ROW_INDEX].Cells["clm_OtherChargesVAT"].Value),
                    Terms_Profile_Other_Royalty_Cash_Destination = GetKeyFromValue("OtherChargesCashDestination", dgvOtherCharges.Rows[OTHER_ROYALTY_ROW_INDEX].Cells["clm_OtherChargesCashDestination"].Value.ToString()),
                    Terms_Profile_Other_Royalty_Deferred_Remittance = GetKeyFromValue("OtherChargesDeferredRemittance", dgvOtherCharges.Rows[OTHER_ROYALTY_ROW_INDEX].Cells["clm_OtherChargesDeferredRemittance"].Value.ToString()),
                    Terms_Profile_Other_Royalty_Charge = Convert.ToInt32(dgvOtherCharges.Rows[OTHER_ROYALTY_ROW_INDEX].Cells["clm_OtherChargesCharge"].Value),
                    Terms_Profile_Other_Royalty_Paid_By = GetKeyFromValue("OtherChargesPaidBy", dgvOtherCharges.Rows[OTHER_ROYALTY_ROW_INDEX].Cells["clm_OtherChargesPaidBy"].Value.ToString()),
                    Terms_Profile_Other_Royalty_Guarantor = GetKeyFromValue("OtherChargesGuarantor", dgvOtherCharges.Rows[OTHER_ROYALTY_ROW_INDEX].Cells["clm_OtherChargesShortfallGuarantor"].Value.ToString()),
                    Terms_Profile_Other_Royalty_Frequency = GetKeyFromValue("OtherChargesFrequency", dgvOtherCharges.Rows[OTHER_ROYALTY_ROW_INDEX].Cells["clm_OtherChargesFrequency"].Value.ToString()),
                    Terms_Profile_Other_Other1_Index = Convert.ToInt32(dgvOtherCharges.Rows[OTHER_OTHER1_ROW_INDEX].Cells["clm_OtherChargesOrder"].Value),
                    Terms_Profile_Other_Other1_Name = string.Empty,
                    Terms_Profile_Other_Other1_Use = Convert.ToBoolean(dgvOtherCharges.Rows[OTHER_OTHER1_ROW_INDEX].Cells["clm_OtherChargesEnabled"].Value),
                    Terms_Profile_Other_Other1_Vat = Convert.ToBoolean(dgvOtherCharges.Rows[OTHER_OTHER1_ROW_INDEX].Cells["clm_OtherChargesVAT"].Value),
                    Terms_Profile_Other_Other1_Cash_Destination = GetKeyFromValue("OtherChargesCashDestination", dgvOtherCharges.Rows[OTHER_OTHER1_ROW_INDEX].Cells["clm_OtherChargesCashDestination"].Value.ToString()),
                    Terms_Profile_Other_Other1_Deferred_Remittance = GetKeyFromValue("OtherChargesDeferredRemittance", dgvOtherCharges.Rows[OTHER_OTHER1_ROW_INDEX].Cells["clm_OtherChargesDeferredRemittance"].Value.ToString()),
                    Terms_Profile_Other_Other1_Charge = Convert.ToInt32(dgvOtherCharges.Rows[OTHER_OTHER1_ROW_INDEX].Cells["clm_OtherChargesCharge"].Value),
                    Terms_Profile_Other_Other1_Paid_By = GetKeyFromValue("OtherChargesPaidBy", dgvOtherCharges.Rows[OTHER_OTHER1_ROW_INDEX].Cells["clm_OtherChargesPaidBy"].Value.ToString()),
                    Terms_Profile_Other_Other1_Guarantor = GetKeyFromValue("OtherChargesGuarantor", dgvOtherCharges.Rows[OTHER_OTHER1_ROW_INDEX].Cells["clm_OtherChargesShortfallGuarantor"].Value.ToString()),
                    Terms_Profile_Other_Other1_Frequency = GetKeyFromValue("OtherChargesFrequency", dgvOtherCharges.Rows[OTHER_OTHER1_ROW_INDEX].Cells["clm_OtherChargesFrequency"].Value.ToString()),
                    Terms_Profile_Other_Other2_Index = Convert.ToInt32(dgvOtherCharges.Rows[OTHER_OTHER2_ROW_INDEX].Cells["clm_OtherChargesOrder"].Value),
                    Terms_Profile_Other_Other2_Name = string.Empty,
                    Terms_Profile_Other_Other2_Use = Convert.ToBoolean(dgvOtherCharges.Rows[OTHER_OTHER2_ROW_INDEX].Cells["clm_OtherChargesEnabled"].Value),
                    Terms_Profile_Other_Other2_Vat = Convert.ToBoolean(dgvOtherCharges.Rows[OTHER_OTHER2_ROW_INDEX].Cells["clm_OtherChargesVAT"].Value),
                    Terms_Profile_Other_Other2_Cash_Destination = GetKeyFromValue("OtherChargesCashDestination", dgvOtherCharges.Rows[OTHER_OTHER2_ROW_INDEX].Cells["clm_OtherChargesCashDestination"].Value.ToString()),
                    Terms_Profile_Other_Other2_Deferred_Remittance = GetKeyFromValue("OtherChargesDeferredRemittance", dgvOtherCharges.Rows[OTHER_OTHER2_ROW_INDEX].Cells["clm_OtherChargesDeferredRemittance"].Value.ToString()),
                    Terms_Profile_Other_Other2_Charge = Convert.ToInt32(dgvOtherCharges.Rows[OTHER_OTHER2_ROW_INDEX].Cells["clm_OtherChargesCharge"].Value),
                    Terms_Profile_Other_Other2_Paid_By = GetKeyFromValue("OtherChargesPaidBy", dgvOtherCharges.Rows[OTHER_OTHER2_ROW_INDEX].Cells["clm_OtherChargesPaidBy"].Value.ToString()),
                    Terms_Profile_Other_Other2_Guarantor = GetKeyFromValue("OtherChargesGuarantor", dgvOtherCharges.Rows[OTHER_OTHER2_ROW_INDEX].Cells["clm_OtherChargesShortfallGuarantor"].Value.ToString()),
                    Terms_Profile_Other_Other2_Frequency = GetKeyFromValue("OtherChargesFrequency", dgvOtherCharges.Rows[OTHER_OTHER2_ROW_INDEX].Cells["clm_OtherChargesFrequency"].Value.ToString())
                };


                result = _termsBusiness.UpdateCollectionDetailsWithTermsResults(
                        termsProfileID,
                        termsProfileID == 0 ? "New Terms Set" : string.Empty,
                        Convert.ToInt32(dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerOrder"].Value),
                        Convert.ToBoolean(dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerEnabled"].Value),
                        GetKeyFromValue("PartnerCashDestination", dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerCashDestination"].Value.ToString()),
                        GetKeyFromValue("PartnerDeferredRemittance", dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerDeferredRemittance"].Value.ToString()),
                        GetKeyFromValue("PartnerChargeType", dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerType"].Value.ToString()),
                        _partnerSupValue,
                        Convert.ToBoolean(dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerRentGuaranteed"].Value),
                        Convert.ToInt32(dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerShare"].Value),
                        Convert.ToBoolean(dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerShareGuaranteed"].Value),
                        _partnerSSchValue,
                        _partnerRSchValue,
                        GetKeyFromValue("PartnerShortfallGuarantor", dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerShortfallGuarantor"].Value.ToString()),
                        Convert.ToInt32(dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerPercentage"].Value),
                        Convert.ToInt32(dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerOrder"].Value),
                        Convert.ToBoolean(dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerEnabled"].Value),
                        GetKeyFromValue("PartnerCashDestination", dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerCashDestination"].Value.ToString()),
                        GetKeyFromValue("PartnerDeferredRemittance", dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerDeferredRemittance"].Value.ToString()),
                        GetKeyFromValue("PartnerChargeType", dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerType"].Value.ToString()),
                        Convert.ToInt32(dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerValue"].Value),
                        Convert.ToBoolean(dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerRentGuaranteed"].Value),
                        Convert.ToInt32(dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerShare"].Value),
                        Convert.ToBoolean(dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerShareGuaranteed"].Value),
                        GetKeyFromValue("PartnerShortfallGuarantor", dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerShortfallGuarantor"].Value.ToString()),
                        Convert.ToInt32(dgvPartners.Rows[SITE_ROW_INDEX].Cells["clm_PartnerPercentage"].Value),
                        Convert.ToInt32(dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerOrder"].Value),
                        Convert.ToBoolean(dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerEnabled"].Value),
                        GetKeyFromValue("PartnerCashDestination", dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerCashDestination"].Value.ToString()),
                        GetKeyFromValue("PartnerDeferredRemittance", dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerDeferredRemittance"].Value.ToString()),
                        GetKeyFromValue("PartnerChargeType", dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerType"].Value.ToString()),
                        Convert.ToInt32(dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerValue"].Value),
                        Convert.ToBoolean(dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerRentGuaranteed"].Value),
                        Convert.ToInt32(dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerShare"].Value),
                        Convert.ToBoolean(dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerShareGuaranteed"].Value),
                        GetKeyFromValue("PartnerShortfallGuarantor", dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerShortfallGuarantor"].Value.ToString()),
                        Convert.ToInt32(dgvPartners.Rows[SITE_GROUP_ROW_INDEX].Cells["clm_PartnerPercentage"].Value),
                        Convert.ToInt32(dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerOrder"].Value),
                        Convert.ToBoolean(dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerEnabled"].Value),
                        GetKeyFromValue("PartnerCashDestination", dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerCashDestination"].Value.ToString()),
                        GetKeyFromValue("PartnerDeferredRemittance", dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerDeferredRemittance"].Value.ToString()),
                        GetKeyFromValue("PartnerChargeType", dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerType"].Value.ToString()),
                        Convert.ToInt32(dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerValue"].Value),
                        Convert.ToBoolean(dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerRentGuaranteed"].Value),
                        Convert.ToInt32(dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerShare"].Value),
                        Convert.ToBoolean(dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerShareGuaranteed"].Value),
                        GetKeyFromValue("PartnerShortfallGuarantor", dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerShortfallGuarantor"].Value.ToString()),
                        Convert.ToInt32(dgvPartners.Rows[SEC_SITE_GROUP_ROW_INDEX].Cells["clm_PartnerPercentage"].Value),
                        Convert.ToInt32(dgvVAT.Rows[OUTPUT_VAT_ROW_INDEX].Cells["clm_VATOrder"].Value),
                        Convert.ToBoolean(dgvVAT.Rows[OUTPUT_VAT_ROW_INDEX].Cells["clm_VATEnabled"].Value),
                        GetKeyFromValue("VATCashDestination", dgvVAT.Rows[OUTPUT_VAT_ROW_INDEX].Cells["clm_VATCashDestination"].Value.ToString()),
                        GetKeyFromValue("VATDeferredRemittance", dgvVAT.Rows[OUTPUT_VAT_ROW_INDEX].Cells["clm_VATDeferredRemittance"].Value.ToString()),
                        Convert.ToInt32(dgvVAT.Rows[INPUT_VAT_OPERATOR_ROW_INDEX].Cells["clm_VATOrder"].Value),
                        Convert.ToBoolean(dgvVAT.Rows[INPUT_VAT_OPERATOR_ROW_INDEX].Cells["clm_VATEnabled"].Value),
                        GetKeyFromValue("VATCashDestination", dgvVAT.Rows[INPUT_VAT_OPERATOR_ROW_INDEX].Cells["clm_VATCashDestination"].Value.ToString()),
                        GetKeyFromValue("VATDeferredRemittance", dgvVAT.Rows[INPUT_VAT_OPERATOR_ROW_INDEX].Cells["clm_VATDeferredRemittance"].Value.ToString()),
                        GetKeyFromValue("VATPaidBy", dgvVAT.Rows[INPUT_VAT_OPERATOR_ROW_INDEX].Cells["clm_VATPaidBy"].Value.ToString()),
                        GetKeyFromValue("VATShortfallGuarantor", dgvVAT.Rows[INPUT_VAT_OPERATOR_ROW_INDEX].Cells["clm_VATShortfallGuarantor"].Value.ToString()),
                        Convert.ToInt32(dgvVAT.Rows[INPUT_VAT_SITE_ROW_INDEX].Cells["clm_VATOrder"].Value),
                        Convert.ToBoolean(dgvVAT.Rows[INPUT_VAT_SITE_ROW_INDEX].Cells["clm_VATEnabled"].Value),
                        GetKeyFromValue("VATCashDestination", dgvVAT.Rows[INPUT_VAT_SITE_ROW_INDEX].Cells["clm_VATCashDestination"].Value.ToString()),
                        GetKeyFromValue("VATDeferredRemittance", dgvVAT.Rows[INPUT_VAT_SITE_ROW_INDEX].Cells["clm_VATDeferredRemittance"].Value.ToString()),
                        GetKeyFromValue("VATPaidBy", dgvVAT.Rows[INPUT_VAT_SITE_ROW_INDEX].Cells["clm_VATPaidBy"].Value.ToString()),
                        GetKeyFromValue("VATShortfallGuarantor", dgvVAT.Rows[INPUT_VAT_SITE_ROW_INDEX].Cells["clm_VATShortfallGuarantor"].Value.ToString()),
                        Convert.ToInt32(dgvVAT.Rows[INPUT_VAT_SITE_GROUP_ROW_INDEX].Cells["clm_VATOrder"].Value),
                        Convert.ToBoolean(dgvVAT.Rows[INPUT_VAT_SITE_GROUP_ROW_INDEX].Cells["clm_VATEnabled"].Value),
                        GetKeyFromValue("VATCashDestination", dgvVAT.Rows[INPUT_VAT_SITE_GROUP_ROW_INDEX].Cells["clm_VATCashDestination"].Value.ToString()),
                        GetKeyFromValue("VATDeferredRemittance", dgvVAT.Rows[INPUT_VAT_SITE_GROUP_ROW_INDEX].Cells["clm_VATDeferredRemittance"].Value.ToString()),
                        GetKeyFromValue("VATPaidBy", dgvVAT.Rows[INPUT_VAT_SITE_GROUP_ROW_INDEX].Cells["clm_VATPaidBy"].Value.ToString()),
                        GetKeyFromValue("VATShortfallGuarantor", dgvVAT.Rows[INPUT_VAT_SITE_GROUP_ROW_INDEX].Cells["clm_VATShortfallGuarantor"].Value.ToString()),
                        Convert.ToInt32(dgvVAT.Rows[INPUT_VAT_SEC_SITE_GROUP_ROW_INDEX].Cells["clm_VATOrder"].Value),
                        Convert.ToBoolean(dgvVAT.Rows[INPUT_VAT_SEC_SITE_GROUP_ROW_INDEX].Cells["clm_VATEnabled"].Value),
                        GetKeyFromValue("VATCashDestination", dgvVAT.Rows[INPUT_VAT_SEC_SITE_GROUP_ROW_INDEX].Cells["clm_VATCashDestination"].Value.ToString()),
                        GetKeyFromValue("VATDeferredRemittance", dgvVAT.Rows[INPUT_VAT_SEC_SITE_GROUP_ROW_INDEX].Cells["clm_VATDeferredRemittance"].Value.ToString()),
                        GetKeyFromValue("VATPaidBy", dgvVAT.Rows[INPUT_VAT_SEC_SITE_GROUP_ROW_INDEX].Cells["clm_VATPaidBy"].Value.ToString()),
                        GetKeyFromValue("VATShortfallGuarantor", dgvVAT.Rows[INPUT_VAT_SEC_SITE_GROUP_ROW_INDEX].Cells["clm_VATShortfallGuarantor"].Value.ToString()),
                        Convert.ToInt32(dgvGPT.Rows[GPT_ROW_INDEX].Cells["clm_GPTOrder"].Value),
                        Convert.ToInt32(dgvGPT.Rows[GPT_ROW_INDEX].Cells["clm_GPTEnabled"].Value),
                        GetKeyFromValue("GPTCashDestination", dgvGPT.Rows[GPT_ROW_INDEX].Cells["clm_GPTCashDestination"].Value.ToString()),
                        GetKeyFromValue("GPTDeferredRemittance", dgvGPT.Rows[GPT_ROW_INDEX].Cells["clm_GPTDeferredRemittance"].Value.ToString()),
                        Convert.ToInt32(dgvOtherCharges.Rows[OTHER_LICENCE_ROW_INDEX].Cells["clm_OtherChargesOrder"].Value),
                        Convert.ToBoolean(dgvOtherCharges.Rows[OTHER_LICENCE_ROW_INDEX].Cells["clm_OtherChargesEnabled"].Value),
                        Convert.ToBoolean(dgvOtherCharges.Rows[OTHER_LICENCE_ROW_INDEX].Cells["clm_OtherChargesVAT"].Value),
                        GetKeyFromValue("OtherChargesCashDestination", dgvOtherCharges.Rows[OTHER_LICENCE_ROW_INDEX].Cells["clm_OtherChargesCashDestination"].Value.ToString()),
                        GetKeyFromValue("OtherChargesDeferredRemittance", dgvOtherCharges.Rows[OTHER_LICENCE_ROW_INDEX].Cells["clm_OtherChargesDeferredRemittance"].Value.ToString()),
                        Convert.ToInt32(dgvOtherCharges.Rows[OTHER_LICENCE_ROW_INDEX].Cells["clm_OtherChargesCharge"].Value),
                        GetKeyFromValue("OtherChargesPaidBy", dgvOtherCharges.Rows[OTHER_LICENCE_ROW_INDEX].Cells["clm_OtherChargesPaidBy"].Value.ToString()),
                        GetKeyFromValue("OtherChargesGuarantor", dgvOtherCharges.Rows[OTHER_LICENCE_ROW_INDEX].Cells["clm_OtherChargesShortfallGuarantor"].Value.ToString()),
                        GetKeyFromValue("OtherChargesFrequency", dgvOtherCharges.Rows[OTHER_LICENCE_ROW_INDEX].Cells["clm_OtherChargesFrequency"].Value.ToString()),
                        Convert.ToInt32(dgvOtherCharges.Rows[OTHER_PRIZE_ROW_INDEX].Cells["clm_OtherChargesOrder"].Value),
                        Convert.ToBoolean(dgvOtherCharges.Rows[OTHER_PRIZE_ROW_INDEX].Cells["clm_OtherChargesEnabled"].Value),
                        Convert.ToBoolean(dgvOtherCharges.Rows[OTHER_PRIZE_ROW_INDEX].Cells["clm_OtherChargesVAT"].Value),
                        GetKeyFromValue("OtherChargesCashDestination", dgvOtherCharges.Rows[OTHER_PRIZE_ROW_INDEX].Cells["clm_OtherChargesCashDestination"].Value.ToString()),
                        GetKeyFromValue("OtherChargesDeferredRemittance", dgvOtherCharges.Rows[OTHER_PRIZE_ROW_INDEX].Cells["clm_OtherChargesDeferredRemittance"].Value.ToString()),
                        Convert.ToInt32(dgvOtherCharges.Rows[OTHER_PRIZE_ROW_INDEX].Cells["clm_OtherChargesCharge"].Value),
                        GetKeyFromValue("OtherChargesPaidBy", dgvOtherCharges.Rows[OTHER_PRIZE_ROW_INDEX].Cells["clm_OtherChargesPaidBy"].Value.ToString()),
                        GetKeyFromValue("OtherChargesGuarantor", dgvOtherCharges.Rows[OTHER_PRIZE_ROW_INDEX].Cells["clm_OtherChargesShortfallGuarantor"].Value.ToString()),
                        GetKeyFromValue("OtherChargesFrequency", dgvOtherCharges.Rows[OTHER_PRIZE_ROW_INDEX].Cells["clm_OtherChargesFrequency"].Value.ToString()),
                        Convert.ToInt32(dgvOtherCharges.Rows[OTHER_CONSULTANCY_ROW_INDEX].Cells["clm_OtherChargesOrder"].Value),
                        Convert.ToBoolean(dgvOtherCharges.Rows[OTHER_CONSULTANCY_ROW_INDEX].Cells["clm_OtherChargesEnabled"].Value),
                        Convert.ToBoolean(dgvOtherCharges.Rows[OTHER_CONSULTANCY_ROW_INDEX].Cells["clm_OtherChargesVAT"].Value),
                        GetKeyFromValue("OtherChargesCashDestination", dgvOtherCharges.Rows[OTHER_CONSULTANCY_ROW_INDEX].Cells["clm_OtherChargesCashDestination"].Value.ToString()),
                        GetKeyFromValue("OtherChargesDeferredRemittance", dgvOtherCharges.Rows[OTHER_CONSULTANCY_ROW_INDEX].Cells["clm_OtherChargesDeferredRemittance"].Value.ToString()),
                        Convert.ToInt32(dgvOtherCharges.Rows[OTHER_CONSULTANCY_ROW_INDEX].Cells["clm_OtherChargesCharge"].Value),
                        GetKeyFromValue("OtherChargesPaidBy", dgvOtherCharges.Rows[OTHER_CONSULTANCY_ROW_INDEX].Cells["clm_OtherChargesPaidBy"].Value.ToString()),
                        GetKeyFromValue("OtherChargesGuarantor", dgvOtherCharges.Rows[OTHER_CONSULTANCY_ROW_INDEX].Cells["clm_OtherChargesShortfallGuarantor"].Value.ToString()),
                        GetKeyFromValue("OtherChargesFrequency", dgvOtherCharges.Rows[OTHER_CONSULTANCY_ROW_INDEX].Cells["clm_OtherChargesFrequency"].Value.ToString()),
                        Convert.ToInt32(dgvOtherCharges.Rows[OTHER_ROYALTY_ROW_INDEX].Cells["clm_OtherChargesOrder"].Value),
                        Convert.ToBoolean(dgvOtherCharges.Rows[OTHER_ROYALTY_ROW_INDEX].Cells["clm_OtherChargesEnabled"].Value),
                        Convert.ToBoolean(dgvOtherCharges.Rows[OTHER_ROYALTY_ROW_INDEX].Cells["clm_OtherChargesVAT"].Value),
                        GetKeyFromValue("OtherChargesCashDestination", dgvOtherCharges.Rows[OTHER_ROYALTY_ROW_INDEX].Cells["clm_OtherChargesCashDestination"].Value.ToString()),
                        GetKeyFromValue("OtherChargesDeferredRemittance", dgvOtherCharges.Rows[OTHER_ROYALTY_ROW_INDEX].Cells["clm_OtherChargesDeferredRemittance"].Value.ToString()),
                        Convert.ToInt32(dgvOtherCharges.Rows[OTHER_ROYALTY_ROW_INDEX].Cells["clm_OtherChargesCharge"].Value),
                        GetKeyFromValue("OtherChargesPaidBy", dgvOtherCharges.Rows[OTHER_ROYALTY_ROW_INDEX].Cells["clm_OtherChargesPaidBy"].Value.ToString()),
                        GetKeyFromValue("OtherChargesGuarantor", dgvOtherCharges.Rows[OTHER_ROYALTY_ROW_INDEX].Cells["clm_OtherChargesShortfallGuarantor"].Value.ToString()),
                        GetKeyFromValue("OtherChargesFrequency", dgvOtherCharges.Rows[OTHER_ROYALTY_ROW_INDEX].Cells["clm_OtherChargesFrequency"].Value.ToString()),
                        Convert.ToInt32(dgvOtherCharges.Rows[OTHER_OTHER1_ROW_INDEX].Cells["clm_OtherChargesOrder"].Value),
                        string.Empty,
                        Convert.ToBoolean(dgvOtherCharges.Rows[OTHER_OTHER1_ROW_INDEX].Cells["clm_OtherChargesEnabled"].Value),
                        Convert.ToBoolean(dgvOtherCharges.Rows[OTHER_OTHER1_ROW_INDEX].Cells["clm_OtherChargesVAT"].Value),
                        GetKeyFromValue("OtherChargesCashDestination", dgvOtherCharges.Rows[OTHER_OTHER1_ROW_INDEX].Cells["clm_OtherChargesCashDestination"].Value.ToString()),
                        GetKeyFromValue("OtherChargesDeferredRemittance", dgvOtherCharges.Rows[OTHER_OTHER1_ROW_INDEX].Cells["clm_OtherChargesDeferredRemittance"].Value.ToString()),
                        Convert.ToInt32(dgvOtherCharges.Rows[OTHER_OTHER1_ROW_INDEX].Cells["clm_OtherChargesCharge"].Value),
                        GetKeyFromValue("OtherChargesPaidBy", dgvOtherCharges.Rows[OTHER_OTHER1_ROW_INDEX].Cells["clm_OtherChargesPaidBy"].Value.ToString()),
                        GetKeyFromValue("OtherChargesGuarantor", dgvOtherCharges.Rows[OTHER_OTHER1_ROW_INDEX].Cells["clm_OtherChargesShortfallGuarantor"].Value.ToString()),
                        GetKeyFromValue("OtherChargesFrequency", dgvOtherCharges.Rows[OTHER_OTHER1_ROW_INDEX].Cells["clm_OtherChargesFrequency"].Value.ToString()),
                        Convert.ToInt32(dgvOtherCharges.Rows[OTHER_OTHER2_ROW_INDEX].Cells["clm_OtherChargesOrder"].Value),
                        string.Empty,
                        Convert.ToBoolean(dgvOtherCharges.Rows[OTHER_OTHER2_ROW_INDEX].Cells["clm_OtherChargesEnabled"].Value),
                        Convert.ToBoolean(dgvOtherCharges.Rows[OTHER_OTHER2_ROW_INDEX].Cells["clm_OtherChargesVAT"].Value),
                        GetKeyFromValue("OtherChargesCashDestination", dgvOtherCharges.Rows[OTHER_OTHER2_ROW_INDEX].Cells["clm_OtherChargesCashDestination"].Value.ToString()),
                        GetKeyFromValue("OtherChargesDeferredRemittance", dgvOtherCharges.Rows[OTHER_OTHER2_ROW_INDEX].Cells["clm_OtherChargesDeferredRemittance"].Value.ToString()),
                        Convert.ToInt32(dgvOtherCharges.Rows[OTHER_OTHER2_ROW_INDEX].Cells["clm_OtherChargesCharge"].Value),
                        GetKeyFromValue("OtherChargesPaidBy", dgvOtherCharges.Rows[OTHER_OTHER2_ROW_INDEX].Cells["clm_OtherChargesPaidBy"].Value.ToString()),
                        GetKeyFromValue("OtherChargesGuarantor", dgvOtherCharges.Rows[OTHER_OTHER2_ROW_INDEX].Cells["clm_OtherChargesShortfallGuarantor"].Value.ToString()),
                        GetKeyFromValue("OtherChargesFrequency", dgvOtherCharges.Rows[OTHER_OTHER2_ROW_INDEX].Cells["clm_OtherChargesFrequency"].Value.ToString()));


                //audit changes
                new Audit_History()
                    .AddEntry()
                    .SetOperationType(termsProfileID > 0 ? OperationType.MODIFY : OperationType.ADD)
                    .SetScreen("Terms")
                    .SetModule(ModuleNameEnterprise.SGVIFinancial)
                    .SetDescription(termsProfileID > 0 ? "Terms group '" + _termsGroupName + "' modified. Terms Profile '" + _termsProfileName + "' share details modified ..[{0}]: {2} --> {1}" : "Terms group '" + _termsGroupName + "'modified. Terms Profile '" + _termsProfileName + "'  share details added ..[{0}]: {1}")
                    .InsertAuditEntries(_termsProfileResult, _newTermsProfileResult, termsProfileID <= 0, (oldValue, newValue) => (!string.IsNullOrEmpty(oldValue.ToString()) &&  newValue != "-1") || (string.IsNullOrEmpty(oldValue) && newValue.ToLower() == "true") , null);

                return result;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return -1;
        }

        private void SetSupplierValue()
        {
            try
            {
                LogManager.WriteLog("Inside SetSupplierValue...", LogManager.enumLogLevel.Info);

                switch (dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerType"].Value.ToString())
                {
                    case "SSch":
                        DataGridViewComboBoxCell SSchComboBox = new DataGridViewComboBoxCell();
                        SSchComboBox.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                        SSchComboBox.FlatStyle = FlatStyle.Standard;
                        SSchComboBox.Items.AddRange(_shareScheduleCollection.Select(x => x.Value).ToArray());
                        dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerValue"] = SSchComboBox;
                        ((DataGridViewComboBoxCell)dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerValue"]).Value = GetValueFromKey("ShareSchedule", string.Empty, _partnerShareSchedule.Value);
                        break;
                    case "RSch":
                        DataGridViewComboBoxCell RSchComboBox = new DataGridViewComboBoxCell();
                        RSchComboBox.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                        RSchComboBox.FlatStyle = FlatStyle.Standard;
                        RSchComboBox.Items.AddRange(_rentScheduleCollection.Select(x => x.Value).ToArray());
                        dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerValue"] = RSchComboBox;
                        ((DataGridViewComboBoxCell)dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerValue"]).Value = GetValueFromKey("RentSchedule", string.Empty, _partnerRentSchedule.Value);
                        break;
                    default:
                        dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerValue"] = new DataGridViewTextBoxCell();
                        dgvPartners.Rows[SUPPLIER_ROW_INDEX].Cells["clm_PartnerValue"].Value = _partnerSupplierValue;
                        break;
                }
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
