using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.CoreLib.Win32;
using System.Text.RegularExpressions;
using BMC.CoreLib;
using System.Reflection;
using BMC.EnterpriseDataAccess;
using BMC.Common;



namespace BMC.EnterpriseClient.Views
{
    public partial class frmBuy : Form
    {
        #region Private Variables

        private const int SF_VALUE_SASPROTOCOL = 1;
        private const string ScreenName = "Buy Machine => ";
        private string strAny = "--ANY--";
        private bool DelMachineIDandRefreshFlag = false;

        private int? AutoMachineID;
        private int _machineIDActual = 0;
        private int? ModelDepreciationID;
        private int BuyMCClassAndMachineID;
        private bool IsNGA = false;
        private bool _IsEdit = false;
        private string strMacAddr;
        private bool PreviousMachineAFTState;
        private bool CurrentMachineAFTState;
        private string strAltSerial = "";

        private bool AutoGenerateStockNo = false;

        private bool _Inuse = false;
        private bool? AFTEnabledForSite;
        private int LstOperator_ind = -1;
        private bool IsCustomMultiGameName = false;

        private int iBaseDenom = 0;
        private float dPercentage_Payout = 0;
        Action act_Refresh = null;
        Action<int> act_removeMC = null;
        private bool isLoading = true;

        private AdminSettings _AdminSettings = null;
        private AdminSystemSettingsResult objAdminSystemSettingsResult = null;
        private BuyAuditEntity oAudit = new BuyAuditEntity();
        BuyAuditEntity oNewAudit = null;
        private bool isFromTemplate = false;
        private int _iMachineTypeID = 0;
        bool _isNew = false;
        private bool isEditTemplate = false;
        BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;
        #endregion

        #region Constructor
        /// <summary>
        /// Initailize a new instance of Buyform
        /// </summary>
        /// <param name="MachineClassID">For New Machine- MachineClassID is passed for Edit Machine - Machine ID is Passed</param>
        /// <param name="bIsNGA">Is Non Gaming Asset</param>
        /// <param name="Inuse">Machine In Use</param>
        /// <param name="IsEdit">Is Edit Machine</param>
        /// <param name="act_ref">Pass Refresh method after evey action perfromed</param>
        /// <param name="act_removeMCNode">Pass Remove Machine Class method</param>
        /// <param name="isTemplateSettingEnabled">isTemplateSettingEnabled</param>
        public frmBuy(int MachineClassID, bool bIsNGA, bool Inuse, bool IsEdit, Action act_ref, Action<int> act_removeMCNode, bool isTemplateSettingEnabled, int iMachineTypeID,bool isNew)
        {
            _AdminSettings = new AdminSettings();

            BuyMCClassAndMachineID = MachineClassID;//For New Machine- MachineClassID is passed for Edit Machine - Machine ID is Passed
            IsNGA = bIsNGA;
            _Inuse = Inuse;
            _IsEdit = IsEdit;
            InitializeComponent();
            act_Refresh = act_ref;
            act_removeMC = act_removeMCNode;
            txtMACAddress.ValidatingType = typeof(string);
            isFromTemplate = isTemplateSettingEnabled;
            _iMachineTypeID = iMachineTypeID;
            _isNew = isNew;
            btn_Template.Visible = false;
            btnOK.Visible = true;
            cmbBaseDenom.Enabled = false;
            txtPercentagePayOut.Enabled = false;
            objDatawatcher = new Helpers.Datawatcher(this, 
                (w, f) => 
                {
                    w.RemoveControlFromWatcher((f as frmBuy).cmbAssetTemplate);
                });
            SetPropertyTag();
        }

        private void SetPropertyTag()
        {
            try
            {
                this.btnOK.Tag = "Key_Buy";
                this.btn_Template.Tag = "Key_UpdateCaption";
                this.btnCancel.Tag = "Key_CloseCaption";
                btnSellMachine.Tag = "Key_Sell";
                this.lbl_AliasStockNo.Tag = "Key_AliasColonMandatoryColon";
                this.lblCabinetModelType.Tag = "Key_CabinetModelTypeMandatory";
                this.lblCMpGameType.Tag = "Key_CMPGameTypeMandatory";
                this.lblDeliveredTo.Tag = "Key_DeliveredToMandatory";
                this.lblGamePrefix.Tag = "Key_GamePrefixMandatory";
                this.lblGamelibrary.Tag = "Key_GameTitleMandatory";
                this.lblManufacturer.Tag = "Key_ManufacturerMandatory";
                this.lblOperator.Tag = "Key_OperatorMandatory";
                this.lblStackerName.Tag = "Key_StackerNameMandatory";
                this.lblValidationLength.Tag = "Key_ValidationLengthMandatory";
                this.chkDepreciationUseDefault.Tag = "Key_QMarkUseDefault";
                this.lbl_ActualStockNo.Tag = "Key_ActualColon";
                this.chkAFTEnabled.Tag = "Key_AFTEnabled";
                this.lblAltSerialNo.Tag = "Key_AlternateSerialNumber";
               // this.groupBox1.Tag = "Key_AssetNumber";
                this.lblAsset.Tag = "Key_AssetTitleColon";
                this.lblBaseDenom.Tag = "Key_BaseDenomCents";
                this.lblCurrentNBV.Tag = "Key_CurrentNBVColon";
                this.chkDefaultAssetDetail.Tag = "Key_DefaultAssetDetail";
                this.lblDeliveryDate.Tag = "Key_DeliveryDateColon";
                this.lblDepreciationPolicy.Tag = "Key_DepreciationPolicyColon";
                this.lblDepStDate.Tag = "Key_DepreciationStartDate";
                this.lblDisplayName.Tag = "Key_DisplayNameColon";
                this.lbl_Enrolmentflag.Tag = "Key_EnrolmentTypeColon";
               // this.tabfinancial.Tag = "Key_Financial";
                this.chkGameCapping.Tag = "Key_GameCapping";
               // this.tabGeneral.Tag = "Key_General";
                this.chkGetGameDetails.Tag = "Key_GetGameDetails";
                this.lbl_GMUNo.Tag = "Key_GMUNoColon";
                this.chkKeepWindowActive.Tag = "Key_KeepWindowActive";
                this.lblMACAddress.Tag = "Key_MACAddressColon";
                this.lblMCPurchasedFrom.Tag = "Key_MachinePurchasedFromColon";
                this.lblMachineType.Tag = "Key_MachineTypeColon";
                this.chkMultiGame.Tag = "Key_MultiGame";
                this.chkNonCashable.Tag = "Key_NonCashableVoucher";
                this.lblNotes.Tag = "Key_NotesColon";
                this.lblOccupancyHR.Tag = "Key_OccupancyHr";
                this.lblPercentagePayout.Tag = "Key_PercentagePayoutColon";
                this.lblPurchaseInvoiceNo.Tag = "Key_PurchaseInvoiceNumberColon";                
                this.lblPurchasePrice.Tag = "Key_PurchasePriceColon";
                this.lblRep.Tag = "Key_RepColon";
                this.btnSellMachine.Tag = "Key_SellMachine";
                this.lblSerialNo.Tag = "Key_SerialNumberColon";
                this.lblStatus.Tag = "Key_StatusColon";
                this.lblTemplate.Tag = "Key_TemplateNameColon";
                this.chkTITO.Tag = "Key_TITOEnabled";
                this.lblWeeklyDep.Tag = "Key_WeeklyDepColon";

                strAny = this.GetResourceTextByKey("Key_Any");
            }
            catch (Exception ex)
            {                
                ExceptionManager.Publish(ex);
            }
        }

        // For Reusing Edit Template Option
        public frmBuy()
        {
                _AdminSettings = new AdminSettings();
                InitializeComponent();
                this.Text = this.GetResourceTextByKey(1, "MSG_TEMPLATE_EDIT");            // "Edit Template";
                btn_Template.Visible = true;
                btnOK.Visible = false;
                isFromTemplate = true;
                _isNew = true;
                _Inuse = false;
                IsNGA = false;
                isEditTemplate = true;
                btn_Template.Enabled = false;
                cmbBaseDenom.Enabled = false;
                txtPercentagePayOut.Enabled = false;
                objDatawatcher = new Helpers.Datawatcher(this, 
                (w, f) => 
                {
                    w.RemoveControlFromWatcher((f as frmBuy).cmbAssetTemplate);
                });
                SetPropertyTag();
        }
        #endregion

        class clsStockStatus
        {
            public string DisplayCaption { get; set; }
            public string Caption { get; set; }
            public int StockValue { get; set; }
        }

        #region DBMethods
        /// <summary>
        /// Model Type
        /// </summary>
        /// <param name="IsNGA"></param>
        private void LoadModelType(bool? IsNGA)
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Load Model Type details", LogManager.enumLogLevel.Info);
                List<GetModelTypeResult> lst_ModelType = AssetManagementBiz.CreateInstance().GetModelTypeDetails(IsNGA, null);
                if (lst_ModelType != null & lst_ModelType.Count > 0)
                {
                    lst_ModelType.Insert(0, (new GetModelTypeResult { MT_ID = -1, MT_Model_Name = strAny }));
                }
                cmbModelType.DataSource = lst_ModelType;
                cmbModelType.DisplayMember = "MT_Model_Name";
                cmbModelType.ValueMember = "MT_ID";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Load Operators
        /// </summary>
        private void LoadOperator()
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Load Operator details", LogManager.enumLogLevel.Info);
                List<OperatorEntity> lstOp = UserAdministrationBiz.CreateInstance().GetOperatorDetails();
                OperatorEntity Op_First = lstOp.Find(o => o.Operator_ID == -1);
                if (Op_First != null)
                {
                    Op_First.Operator_Name =  this.GetResourceTextByKey("Key_None");             // "[NONE]"; 
                }
                cmbOperators.DataSource = lstOp;
                cmbOperators.DisplayMember = "Operator_Name";
                cmbOperators.ValueMember = "Operator_ID";
                //if (lstOp.Count >= 2)
                //{
                //    cmbOperators.SelectedIndex = 1;
                //}
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        ///  Load Machine Type Details
        /// </summary>
        private void LoadMachineTypeDetails()
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Load Machine Type details", LogManager.enumLogLevel.Info);
                List<GetMachineTypeDetailsResult> lst_MCType = BuyMachineBiz.CreateInstance().GetMachineTypeDetails(null);
                if (IsNGA)
                {
                    cmbCategory.DataSource = lst_MCType;
                    cmbCategory.DisplayMember = "Machine_Type_Code";
                    cmbCategory.ValueMember = "Machine_Type_ID";
                }
                else
                {
                    cmbMachineType.DataSource = lst_MCType.Where(MacTyp => MacTyp.IsNonGamingAssetType == 0).ToList();
                    cmbMachineType.DisplayMember = "Machine_Type_Code";
                    cmbMachineType.ValueMember = "Machine_Type_ID";
                    int Idx = 0;
                    Idx = ((List<GetMachineTypeDetailsResult>)cmbMachineType.DataSource).FindIndex(se => se.Machine_Type_ID == _iMachineTypeID);
                    cmbMachineType.SelectedIndex = (Idx >= 0) ? Idx : 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        ///  Loads Depreciation Details
        /// </summary>
        private void LoadDepreciationDetails()
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Load Depreciation details", LogManager.enumLogLevel.Info);
                List<DepreciationEntity> lst_depreciation = DepreciationBusiness.CreateInstance().LoadDepreciationPolicy(null).ToList();
                cmbDepreciation.DataSource = lst_depreciation;
                cmbDepreciation.DisplayMember = "Depreciation_Policy_Description";
                cmbDepreciation.ValueMember = "Depreciation_Policy_ID";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        ///  Loads Manufacturer Details
        /// </summary>
        private void LoadManufacturer()
        {
            try
            {
                cmbManufacturer.Items.Clear();
                LogManager.WriteLog(ScreenName + "Load Manufacturer details", LogManager.enumLogLevel.Info);
                List<Manufacturer> lst_manf = BuyMachineBiz.CreateInstance().GetManufacturers();
                Manufacturer m_negative = lst_manf.Find(o => o.Manufacturer_ID == -1);
                if (m_negative != null)
                {
                    m_negative.Manufacturer_Name = strAny;
                    //lst_manf.Remove(m_negative);
                }
                cmbManufacturer.DataSource = lst_manf;
                cmbManufacturer.DisplayMember = "Manufacturer_Name";
                cmbManufacturer.ValueMember = "Manufacturer_ID";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Loads Game Title
        /// </summary>
        private void LoadGameTitle(bool isMultiGame)
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Load GameTitle details", LogManager.enumLogLevel.Info);
                List<GetGameTitleResult> lst_GameTitle = BuyMachineBiz.CreateInstance().GetGameTitleDetails(isMultiGame);
                if (lst_GameTitle != null & lst_GameTitle.Count > 0)
                {
                    lst_GameTitle.Insert(0, (new GetGameTitleResult { Game_Title_ID = -1, Game_Title = strAny }));
                }
                cmbCategory.DataSource = lst_GameTitle;
                cmbCategory.DisplayMember = "Game_Title";
                cmbCategory.ValueMember = "Game_Title_ID";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        ///  Loads Stacker Details
        /// </summary>
        private void LoadStackerDetails()
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Load Stacker details", LogManager.enumLogLevel.Info);
                List<GetActiveStackerDetailsResult> lst_Stacker = BuyMachineBiz.CreateInstance().GetActiveStakersDetails();
                if (lst_Stacker != null & lst_Stacker.Count > 0)
                {
                    lst_Stacker.Insert(0, (new GetActiveStackerDetailsResult { ActiveStacker_Id = -1, ActiveStackerName = strAny }));
                }

                cmbStackerList.DataSource = lst_Stacker;
                cmbStackerList.DisplayMember = "ActiveStackerName";
                cmbStackerList.ValueMember = "ActiveStacker_Id";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// load staff details
        /// </summary>
        /// <param name="MC"></param>
        void GetStaffDetails(GetBuyMachineDetailsResult MC)
        {
            try
            {

                List<StaffDetailsResult> lstStaff = UserAdministrationBiz.CreateInstance().GetStaffDetails(AppEntryPoint.Current.StaffId);
                if (lstStaff.Count > 0)
                {
                    StaffDetailsResult Staff_det = lstStaff[0];
                    bool IsAStockController = Staff_det.Staff_IsAStockController ?? false;
                    bool IsSTOCK_NOT_IN_USE = (MC.Machine_Status_Flag != (int)StockStatus.STOCK_IN_USE);
                    bool bEnabled = (IsAStockController || IsSTOCK_NOT_IN_USE);
                    cmbOperators.Enabled = bEnabled;
                    cmbDepot.Enabled = bEnabled;
                    cmbRep.Enabled = bEnabled;

                    //173,060 - System allows User to change the Status of Slot Machine as 'In Use' without enrolling the machine.
                    //Which leads to make that Asset as not usable in future.
                    //Solution: Disabled status combo as per 11.4 design
                    cmbStatus.Enabled = false;

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        ///  disable controls by using M\C status
        /// </summary>
        /// <param name="MC"></param>
        void CheckMachineStatus(GetBuyMachineDetailsResult MC)
        {
            try
            {

                bool IsSTOCK_NOT_IN_USE = (MC.Machine_Status_Flag != (int)StockStatus.STOCK_IN_USE);
                txtSerialNumber.Enabled = IsNGA ? false : IsSTOCK_NOT_IN_USE;
                cmbCategory.Enabled = IsSTOCK_NOT_IN_USE;
                txt_MultiGame.Enabled = IsCustomMultiGameName && IsSTOCK_NOT_IN_USE;
                cmbModelType.Enabled = IsSTOCK_NOT_IN_USE;
                txt_ActualStockNo.Enabled = IsSTOCK_NOT_IN_USE;
                txt_GMUNo.Enabled = IsSTOCK_NOT_IN_USE;
                chkMultiGame.Enabled = IsSTOCK_NOT_IN_USE;
                txtStockNumber.Enabled = false;
                cmbManufacturer.Enabled = IsNGA ? false : IsSTOCK_NOT_IN_USE;
                cmbStackerList.Enabled = IsSTOCK_NOT_IN_USE;
                chkDefaultAssetDetail.Enabled = IsSTOCK_NOT_IN_USE;
                //grpAssetDetails.Enabled = chkDefaultAssetDetail.Checked && IsSTOCK_NOT_IN_USE;
                cmbMachineType.Enabled = IsSTOCK_NOT_IN_USE;

                if (IsNGA)
                {
                    txtMACAddress.Enabled = true;
                    cmbCategory.Enabled = false;
                }
                else if ((AdminBusiness.GetSetting("IsLGERequired", "FALSE").ToUpper().Trim() == "TRUE") && (AdminBusiness.GetSetting("LGEEnabled", "FALSE").ToUpper().Trim() == "TRUE"))
                {
                    txtMACAddress.Enabled = false;
                }
                else
                {
                    txtMACAddress.Enabled = true;
                }
                int? InstallationID = 0;
                string Site_Code = "";
                BuyMachineBiz.CreateInstance().GetActiveInstallationFromMachineID(AutoMachineID, ref InstallationID, ref Site_Code);
                btnSellMachine.Enabled = !(InstallationID.HasValue && InstallationID > 0);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Check manufacture game combination based on gametitle and manufacture_id
        /// </summary>
        /// <param name="Gametitle"></param>
        /// <param name="Manufacture_ID"></param>
        private void CheckManufactureGameCombination(string Gametitle, int? Manufacture_ID,int? Machine_typ_Id)
        {
            try
            {
                List<GetMachineClassDetailsResult> lst_MachineClass = BuyMachineBiz.CreateInstance().GetMachineClassDetails(null, Gametitle, Manufacture_ID, Machine_typ_Id);
                if (lst_MachineClass.Count > 0)
                {
                    // ManufacturerID = lst_MachineClass[0].Manufacturer_ID;
                    txtValidationLength.Text = lst_MachineClass[0].Validation_Length.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        /// <summary>
        /// Load DepreciationDetails
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="NBV"></param>
        /// <param name="DepreciationPerWeek"></param>
        /// <param name="PurchasePrice"></param>
        /// <param name="DateTo"></param>
        /// <returns></returns>
        public bool GetDepreciationDetailsFromMachineID(int? MachineID, ref decimal NBV, ref decimal DepreciationPerWeek, ref decimal PurchasePrice, string DateTo)
        {

            try
            {
                int DaysLeft = 0;
                Single YearsLeft = 0;
                decimal Depreciation_Perc = 0;
                decimal Purchase_Price = 0;
                decimal Residual_Value;
                string Date_To;
                DepreciationPerWeek = 0;
                List<GetDepreciationDetailsResult> lst_Deprec = BuyMachineBiz.CreateInstance().GetDepreciationDetails(MachineID);
                if (lst_Deprec != null && lst_Deprec.Count > 0 && lst_Deprec[0] != null)
                {
                    GetDepreciationDetailsResult deprec = lst_Deprec[0];

                    DateTime dt_DateTo = new DateTime();
                    if (DateTime.TryParse(DateTo, out dt_DateTo))
                    {
                        Date_To = DateTo;
                    }
                    else if (deprec.Machine_End_Date.Equals(string.Empty))
                    {
                        Date_To = DateTime.Now.ToShortDateString();// Format(Now, "Short Date");
                    }
                    else
                    {
                        Date_To = deprec.Machine_End_Date;
                    }

                    DateTime dt_DepStartDate = new DateTime();

                    if (DateTime.TryParse(deprec.Machine_Depreciation_Start_Date, out dt_DepStartDate)
                       && DateTime.TryParse(Date_To, out dt_DateTo)
                       && deprec.Depreciation_Policy_Details_ID.HasValue)
                    {
                        Purchase_Price = deprec.Machine_Original_Purchase_Price.Value;
                        Residual_Value = Decimal.Parse(deprec.Depreciation_Policy_Residual_Value.Value.ToString());
                        DaysLeft = dt_DateTo.Subtract(dt_DepStartDate).Days;// DateDiff("d", DateTime.Parse(myRs), Machine_Depreciation_Start_Date);  DateTime.Parse(Date_To);
                        if ((DaysLeft > 1) && (Residual_Value < Purchase_Price))
                        {
                            YearsLeft = (Convert.ToSingle(DaysLeft) / 365);
                            foreach (GetDepreciationDetailsResult dep_Det in lst_Deprec)
                            {
                                if (YearsLeft > (Convert.ToSingle(dep_Det.Depreciation_Policy_Details_Duration) / 12))
                                {
                                    Depreciation_Perc = Depreciation_Perc + dep_Det.Depreciation_Policy_Details_Percentage.Value;
                                    YearsLeft = (YearsLeft - dep_Det.Depreciation_Policy_Details_Duration.Value / 12);
                                }
                                else
                                {
                                    Depreciation_Perc = Depreciation_Perc + Convert.ToDecimal(dep_Det.Depreciation_Policy_Details_Percentage.Value
                                                * (YearsLeft / (Convert.ToSingle(dep_Det.Depreciation_Policy_Details_Duration.Value) / 12)));
                                    break;
                                }
                            }
                        }


                        PurchasePrice = Purchase_Price;
                        NBV = Purchase_Price - (Depreciation_Perc / 100) * (Purchase_Price - Residual_Value);

                        DepreciationPerWeek = (((Purchase_Price - Residual_Value)
                                        * Convert.ToDecimal(Convert.ToSingle(deprec.Depreciation_Policy_Details_Percentage.Value) / 100 * 7))
                                        / Decimal.Parse((deprec.Depreciation_Policy_Details_Duration.Value * 30.4).ToString()));

                    }
                    else
                    {
                        PurchasePrice = Purchase_Price;
                        NBV = Purchase_Price;
                        DepreciationPerWeek = 0;
                    }

                }
                else
                {
                    PurchasePrice = 0;
                    DepreciationPerWeek = 0;
                    NBV = 0;
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

        #region ValidationMethods
        bool Validate(ref string ErrorMsg)
        {
            return Validate(ref ErrorMsg, true);
        }
        /// <summary>
        /// Validate all controls and returns true if condition is Statsified
        /// </summary>
        /// <param name="ErrorMsg">returns Error Message</param>
        /// <returns></returns>
        bool Validate(ref string ErrorMsg,bool isValidateAgs)
        {
            bool retVal = true;
            LogManager.WriteLog(ScreenName + "Validating Controls", LogManager.enumLogLevel.Info);
            try
            {
                string strAllowedChar = "[^a-zA-Z0-9]";
                if (Convert.ToInt32(cmbManufacturer.SelectedValue) == -1)
                {
                    ErrorMsg = this.GetResourceTextByKey(1,"MSG_BUY_MANUFACTURERMAND");
                    cmbManufacturer.Focus();
                    retVal = false;

                }
                else if (cmbCategory.Enabled && Convert.ToInt32(cmbCategory.SelectedValue) == -1)
                {
                    ErrorMsg = this.GetResourceTextByKey(1,"MSG_BUY_GAMETITLEMAND");
                    cmbCategory.Focus();
                    retVal = false;

                }
                else if (cmbOperators.SelectedIndex <= 0)
                {
                    ErrorMsg = this.GetResourceTextByKey(1,"MSG_BUY_VALID_OPER");
                    cmbOperators.Focus();
                    retVal = false;

                }
                else if (cmbDepot.SelectedIndex <= 0)
                {
                    ErrorMsg = this.GetResourceTextByKey(1,"MSG_BUY_VALID_DEPOT");
                    cmbDepot.Focus();
                    retVal = false;
                }
                else if (BuyMachineBiz.CreateInstance().CheckForceSiteRepsDetails() && (cmbRep.SelectedIndex == 0))
                {
                    ErrorMsg = this.GetResourceTextByKey(1,"MSG_BUY_VALID_REP");
                    cmbRep.Focus();
                    retVal = false;
                }
                else if (cmbRep.SelectedIndex == -1)
                {
                    ErrorMsg =this.GetResourceTextByKey(1, "MSG_BUY_VALID_REP");
                    cmbRep.Focus();
                    retVal = false;

                }
                // check if the stock numer is valid & unique
                else if (txtStockNumber.Text.Trim() == string.Empty)
                {
                    ErrorMsg = this.GetResourceTextByKey(1,"MSG_BUY_VALID_ASSET");
                    txtStockNumber.Focus();
                    retVal = false;
                }
                else if (chkMultiGame.Checked && cmbCategory.SelectedIndex <= 0)
                {
                    ErrorMsg = "Please enter the Multi Game Name.";
                    cmbCategory.Focus();
                    retVal = false;
                }
                else if (new Regex(strAllowedChar).IsMatch(txtStockNumber.Text))
                {
                    ErrorMsg = this.GetResourceTextByKey(1,"MSG_BUY_VALID_ASSET");
                    txtStockNumber.Text = "";
                    txtStockNumber.Focus();
                    retVal = false;
                }
                else if (IsNGA && BuyMachineBiz.CreateInstance().CheckMACInUse(txtMACAddress.Text, AutoMachineID))
                {
                    ErrorMsg = this.GetResourceTextByKey(1,"MSG_BUY_VALID_MACASSIGNED");
                    txtMACAddress.Focus();
                    retVal = false;
                }
                else if ((!IsNGA) && txtGameType.Text.Trim() == string.Empty && txtGameType.Enabled)// check if the CMp Game Type is valid & unique
                {
                    ErrorMsg = this.GetResourceTextByKey(1,"MSG_BUY_VALID_CMP");
                    txtGameType.Focus();
                    retVal = false;

                }
                // check if the CMP Game prefix is valid & unique
                else if ((!IsNGA) && txtGamePrefix.Text.Trim() == string.Empty && txtGamePrefix.Enabled)
                {
                    ErrorMsg = this.GetResourceTextByKey(1,"MSG_BUY_VALID_CMPPREFIX");
                    txtGamePrefix.Focus();
                    retVal = false;
                }
                //else if ((!IsNGA) && Convert.ToInt64("0" + txtSerialNumber.Text.Trim()) <= 0)
                //{
                //    ErrorMsg = "Please enter the Serial Number greater than zero";
                //    txtSerialNumber.Focus();
                //    retVal = false;
                //}
                else if ((txtMACAddress.Text == "  -  -  -  -  -" || !IsValidMACAddress(txtMACAddress.Text)) && IsNGA)
                {
                    ErrorMsg = this.GetResourceTextByKey(1,"MSG_BUY_VALID_MAC");
                    txtMACAddress.Focus();
                    retVal = false;
                }
                else if (cmbModelType.SelectedIndex <= 0 && cmbModelType.Enabled)
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_BUY_VALID_CABINET");
                    cmbModelType.Focus();
                    retVal = false;
                }
                else if (chkDefaultAssetDetail.Checked && (cmbBaseDenom.Items.Count == 0 || iBaseDenom < 1))
                {
                    ErrorMsg = this.GetResourceTextByKey(1,"MSG_BUY_VALID_DENOM");
                    cmbBaseDenom.Focus();
                    retVal = false;
                }
                else if (chkDefaultAssetDetail.Checked && (dPercentage_Payout <= 0 || Convert.ToDouble("0" + txtPercentagePayOut.Text) > 100))
                {
                    ErrorMsg = this.GetResourceTextByKey(1,"MSG_BUY_VALID_PAYOUT");
                    txtPercentagePayOut.Focus();
                    retVal = false;
                }
                else if (cmbStackerList.Visible && cmbStackerList.SelectedIndex <= 0)
                {
                    ErrorMsg = this.GetResourceTextByKey(1,"MSG_BUY_VALID_STACKER");
                    cmbStackerList.Focus();
                    retVal = false;
                }
                else if (txtValidationLength.Visible && txtValidationLength.Enabled)
                {
                    if (txtValidationLength.Text.Trim() == string.Empty && (!(txtValidationLength.Text.Trim().IsNumeric())))
                    {
                        ErrorMsg = this.GetResourceTextByKey(1,"MSG_BUY_VALID_LENGTH");
                        txtValidationLength.Focus();
                        retVal = false;
                    }
                }
                else if (cmbStackerList.Visible && cmbStackerList.SelectedIndex <= 0)
                {
                    ErrorMsg =this.GetResourceTextByKey(1, "MSG_BUY_VALID_STACKER");
                    cmbStackerList.Focus();
                    retVal = false;
                }
                AGSBusiness objAGSBusiness = new AGSBusiness();
                //AGS Combination Check-starts

                string AGSCombinationSetting = AdminBusiness.GetSetting("IsEnrolmentFlag", "false");
                //bool isSameMachine = CheckSameAgsCombinationExist(txt_ActualStockNo.Text, txtSerialNumber.Text, txt_GMUNo.Text);
                string ValidateAGS = AdminBusiness.GetSetting("ValidateAGSForGMU", "false");
                
                if (isValidateAgs)
                {
                    
                    if (retVal && ValidateAGS.Trim().ToUpper() == "TRUE" && AGSCombinationSetting.Trim().ToUpper() == "TRUE" && !_Inuse && !IsNGA)
                    {

                        int EnrolmentFlagIndex;
                        int? Result = 0;
                        EnrolmentFlagIndex = Convert.ToInt32(AdminBusiness.GetSetting("AGSValue", "0"));
                        if (EnrolmentFlagIndex == 4) //Serial
                        {
                            Result = objAGSBusiness.CheckEnrolmentTypeBiz(txtSerialNumber.Text, null, null, Result, AutoMachineID);
                            if (Result > 0)
                            {
                                ErrorMsg = this.GetResourceTextByKey(1,"MSG_BUY_VALID_SERIAL");
                                //MessageBox.Show("The Serial number is already available for another machine", "Buy Machine", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtSerialNumber.Focus();
                                retVal = false;
                            }

                        }


                        else if (EnrolmentFlagIndex == 8) //Asset
                        {
                            Result = objAGSBusiness.CheckEnrolmentTypeBiz(null, txt_ActualStockNo.Text, null, Result, AutoMachineID);
                            if (Result > 0)
                            {
                                ErrorMsg = this.GetResourceTextByKey(1,"MSG_BUY_VALID_ASSETMACH");
                                txt_ActualStockNo.Focus();
                                retVal = false;
                            }

                        }

                        else if (EnrolmentFlagIndex == 16) //GMU
                        {
                            Result = objAGSBusiness.CheckEnrolmentTypeBiz(null, null, txt_GMUNo.Text, Result, AutoMachineID);
                            if (Result > 0)
                            {
                                ErrorMsg = this.GetResourceTextByKey(1,"MSG_BUY_VALID_GMU");
                                txt_GMUNo.Focus();
                                retVal = false;
                            }

                        }

                        else if (EnrolmentFlagIndex == 12) //Serial+Asset
                        {
                            Result = objAGSBusiness.CheckEnrolmentTypeBiz(txtSerialNumber.Text, txt_ActualStockNo.Text, null, Result, AutoMachineID);
                            if (Result > 0)
                            {
                                ErrorMsg = this.GetResourceTextByKey(1,"MSG_BUY_SERIALASSETCOMBINATION");
                                txt_ActualStockNo.Focus();
                                retVal = false;
                            }

                        }

                        else if (EnrolmentFlagIndex == 20) //Serial+GMU
                        {
                            Result = objAGSBusiness.CheckEnrolmentTypeBiz(txtSerialNumber.Text, null, txt_GMUNo.Text, Result, AutoMachineID);
                            if (Result > 0)
                            {
                                ErrorMsg = this.GetResourceTextByKey(1,"MSG_BUY_SERIALGMUCOMBINATION");
                                txtSerialNumber.Focus();
                                retVal = false;
                            }

                        }

                        else if (EnrolmentFlagIndex == 24) //Asset+GMU
                        {
                            Result = Result = objAGSBusiness.CheckEnrolmentTypeBiz(null, txt_ActualStockNo.Text, txt_GMUNo.Text, Result, AutoMachineID);
                            if (Result > 0)
                            {
                                ErrorMsg = this.GetResourceTextByKey(1,"MSG_BUY_ASSETSERIALCOMBINATION");
                                txt_ActualStockNo.Focus();
                                retVal = false;
                            }

                        }
                        else if (EnrolmentFlagIndex == 28) // Serial + Asset + GMU
                        {

                            Result = objAGSBusiness.CheckEnrolmentTypeBiz(txtSerialNumber.Text, txt_ActualStockNo.Text, txt_GMUNo.Text, Result, AutoMachineID);
                            if (Result > 0)
                            {
                                ErrorMsg = this.GetResourceTextByKey(1,"MSG_BUY_SERIALASSETGMUCOMBINATION");
                                txt_ActualStockNo.Focus();
                                retVal = false;
                            }

                        }
                    }

                    //AGS Combination Check-ends


                    if (retVal && (!IsNGA))
                    {
                        bool? MachineExists = false;
                        bool? MachineClassExists = false;
                        //Checking Machine and MachineClass Exists or not
                        BuyMachineBiz.CreateInstance().CheckMCAndMCClassExists(txtStockNumber.Text, AutoMachineID, txtSerialNumber.Text, txt_ActualStockNo.Text, txt_GMUNo.Text, ref MachineExists, ref MachineClassExists);
                        if (MachineExists ?? false)
                        {
                            ErrorMsg = string.Format(this.GetResourceTextByKey(1, "MSG_BUY_VALID_STOCK"), txtStockNumber.Text);
                            retVal = false;
                        }
                        else if (ValidateAGS.Trim().ToUpper() == "FALSE" && AGSCombinationSetting.Trim().ToUpper() == "FALSE" && (MachineClassExists ?? false))
                        {
                            //switch (0)
                            //{
                            //    case 0:
                            ErrorMsg = this.GetResourceTextByKey(1,"MSG_BUY_VALID_AGS");
                            //    break;
                            //case 1:
                            //    ErrorMsg = "The Asset Number entered is already assigned to an asset.";
                            //    break;
                            //case 2:
                            //    ErrorMsg = "The GMU number entered is already assigned to an asset.";
                            //    break;
                            //}
                            if (txtSerialNumber.Enabled)
                            {
                                txtSerialNumber.Focus();
                            }
                            retVal = false;
                        }
                    }

                    if (txtSerialNumber.Text.Trim().Length < 1)
                    {
                        txtSerialNumber.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                ErrorMsg = ex.Message;
                retVal = false;

            }
            return retVal;
        }
        #endregion

        #region MiscellaneousMethods
        /// <summary>
        /// Load Stock Status
        /// </summary>
        void SetStockStatus()
        {
            List<clsStockStatus> lst_st = new List<clsStockStatus>();
            //lst_st.Add(new clsStockStatus { Caption = "Converted", StockValue = (int)StockStatus.STOCK_CONVERTED });
            //lst_st.Add(new clsStockStatus { Caption = "Due Out", StockValue = (int)StockStatus.STOCK_DUE_OUT });
            lst_st.Add(new clsStockStatus {DisplayCaption=this.GetResourceTextByKey("Key_InStock"), Caption = "In Stock", StockValue = (int)StockStatus.STOCK_IN_STOCK });
            //lst_st.Add(new clsStockStatus { Caption = "In Stock Unusable", StockValue = (int)StockStatus.STOCK_IN_STOCK_UNUSABLE });
            lst_st.Add(new clsStockStatus { DisplayCaption=this.GetResourceTextByKey("Key_InUse"),Caption = "In Use", StockValue = (int)StockStatus.STOCK_IN_USE });
            //lst_st.Add(new clsStockStatus { Caption = "On Order", StockValue = (int)StockStatus.STOCK_ON_ORDER });
            lst_st.Add(new clsStockStatus { DisplayCaption=this.GetResourceTextByKey("Key_Sold"),Caption = "Sold", StockValue = (int)StockStatus.STOCK_SOLD });
            //lst_st.Add(new clsStockStatus { Caption = "Under Repair", StockValue = (int)StockStatus.STOCK_UNDER_REPAIR });
            cmbStatus.Items.Clear();
            cmbStatus.DataSource = lst_st;
            cmbStatus.DisplayMember = "DisplayCaption";
            cmbStatus.ValueMember = "StockValue";
            oAudit.Status = (cmbStatus.SelectedItem as clsStockStatus).Caption;
        }

        /// <summary>
        /// this method is call when new M\C is purchased
        /// </summary>
        /// <param name="MachineClassID"></param>
        /// <param name="bIsNGA">Non-Gaming Asset or Not</param>
        public void ShowMe(int MachineClassID, bool bIsNGA)
        {

            try
            {
                cmbAssetTemplate.Enabled = true && !bIsNGA;
                cmbAssetTemplate.Visible = isFromTemplate && !bIsNGA;
                lblTemplate.Visible = isFromTemplate && !bIsNGA;
                //cmbAssetTemplate.Visible = false;              
                //lblTemplate.Visible = false;

                IsNGA = bIsNGA;
                LogManager.WriteLog(ScreenName + "ShowMe Started", LogManager.enumLogLevel.Debug);

                List<clsStockStatus> lst_st = (List<clsStockStatus>)cmbStatus.DataSource;
                int ind = lst_st.FindIndex(se => se.StockValue == (int)StockStatus.STOCK_IN_STOCK);
                cmbStatus.SelectedIndex = ind;

                BuyMCClassAndMachineID = MachineClassID;
                _IsEdit = false;

                if (IsNGA)
                {

                    EnableDisableControls(false, false);
                    LogManager.WriteLog(ScreenName + "get machine class details", LogManager.enumLogLevel.Debug);
                    List<GetMachineClassDetailsResult> lst_MachineClass = BuyMachineBiz.CreateInstance().GetMachineClassDetails(MachineClassID, null, null,null);
                    if (lst_MachineClass.Count > 0)
                    {
                        GetMachineClassDetailsResult MC = lst_MachineClass[0];

                        this.Text = string.Format(this.GetResourceTextByKey(1, "MSG_MACHINE_BUYNEW"), MC.Machine_Name);            // "Buying a new " + MC.Machine_Name;
                        oAudit.Machine_Name = txtAssetTitle.Text = MC.Machine_Name;
                        txtAssetTitle.Enabled = false;
                        ModelDepreciationID = MC.Depreciation_Policy_ID;
                        //cmbCategory.SelectedIndex = MC.Machine_Class_Category_ID;
                        if (cmbCategory.DataSource != null)
                        {
                            List<GetMachineTypeDetailsResult> lst_MCType = (List<GetMachineTypeDetailsResult>)cmbCategory.DataSource;
                            ind = lst_MCType.FindIndex(se => se.Machine_Type_ID == _iMachineTypeID);
                            cmbCategory.SelectedIndex = (ind >= 0) ? ind : 0;
                        }
                        cmbCategory.Enabled = false;

                        //AutoGenerateStockNo = SystemParameterBiz.CreateInstance().GeSystemParameterSettings();
                        //txtStockNumber.Enabled = (!AutoGenerateStockNo);
                        //txtStockNumber.Text = myStockNo;

                        if (MC.Manufacturer_ID > 0)
                        {
                            cmbManufacturer.Enabled = false;
                        }
                        else
                        {
                            cmbManufacturer.Enabled = true;
                        }
                        oAudit.Manufacturer_Name = cmbManufacturer.Text = MC.Manufacturer_Name;
                    }

                }
                else if ((AdminBusiness.GetSetting("IsLGERequired", "FALSE").ToUpper().Trim() == "TRUE") && (AdminBusiness.GetSetting("LGEEnabled", "FALSE").ToUpper().Trim() == "TRUE"))
                {
                    txtMACAddress.Enabled = false;
                }
                else
                {
                    EnableDisableControls(true, false);
                }

                try
                {
                    chkGameCapping.Visible = Convert.ToBoolean(AdminBusiness.GetSetting("IsGameCappingEnabled", "FALSE")) && !IsNGA;
                }
                catch
                {
                    chkGameCapping.Visible = true;
                }
                
                string myStockNo = "";

                //Create TempMachineID

                AutoMachineID = BuyMachineBiz.CreateInstance().AddOrDeleteMachineDetails(null, bIsNGA ? (int?)MachineClassID : null, 1, ref myStockNo);

                /*START
                 *CR#173,062
                 *[BMC Enterprise]:
                 *(Gaming Asset Screen):-> When the 'Asset Number' creation is in 'Auto Generation ' mode, 'Alias Name' is getting created for more than '10 digits.
                 *While giving the Alias Name manually, system won't allow Users to enter more than 10 digits.
                 *In the same way, system should behave in 'Auto Generation' mode also.
                 *
                */
                if (myStockNo.Length > txtStockNumber.MaxLength)
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_STOCKNO_LENGTH"), this.Text);            // "StockNo length is greater than the expected length.");
                    this.Close();
                }
                //END CR#173,062

                if (AutoMachineID != null)
                {
                    _machineIDActual = AutoMachineID.Value;
                }
                LogManager.WriteLog(ScreenName + "ShowMe:Created Temp MachineID:" + AutoMachineID, LogManager.enumLogLevel.Info);
                if (!myStockNo.Equals(string.Empty))
                {
                    txtStockNumber.Enabled = false;

                }
                else
                {
                    AutoGenerateStockNo = true;
                }
                //AutoGenerateStockNo = SystemParameterBiz.CreateInstance().GeSystemParameterSettings();
                //txtStockNumber.Enabled = (!AutoGenerateStockNo);

                oAudit.Stock_No = txtStockNumber.Text = myStockNo;

                //Calling Audit Method
                

                EnableDisableControls(bIsNGA, true);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                Win32Extensions.ShowInfoMessageBox(this,this.GetResourceTextByKey(1, "MSG_MACHINEDETAILS_RETRIVE"), this.Text);            // "Unable to retrive machine details");
            }
        }

        /// <summary>
        /// this method is call when edit a existing M\C
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="bIsNGA"></param>
        /// <param name="Inuse"></param>
        public void ShowMeEdit(int MachineID, string templateName, bool bIsNGA, bool Inuse)
        {
            try
            {

                cmbAssetTemplate.Visible = isFromTemplate;
                lblTemplate.Visible = isFromTemplate;

                AutoMachineID = MachineID;

                _IsEdit = !isFromTemplate;

                List<GetBuyMachineDetailsResult> lst_buyMC = BuyMachineBiz.CreateInstance().GetBuyMachineDetails(AutoMachineID, templateName);
                if (lst_buyMC != null && lst_buyMC.Count > 0)
                {
                    LogManager.WriteLog(ScreenName + "ShowMeEdit:Get Machine details:" + AutoMachineID, LogManager.enumLogLevel.Info);
                    GetBuyMachineDetailsResult MC = lst_buyMC[0];
                    ModelDepreciationID = (MC.Class_Depreciation != null && MC.Class_Depreciation > 0) ? MC.Class_Depreciation : MC.Type_Depreciation;                    
                    int ind = 0;
                    SetSelectedIndex(MC, isFromTemplate);
                    if (IsNGA)
                    {
                        InVisibleControls(false);
                        List<GetMachineTypeDetailsResult> lst_MCType = (List<GetMachineTypeDetailsResult>)cmbCategory.DataSource;
                        if (lst_MCType != null && lst_MCType.Count > 0)
                        {
                            ind = lst_MCType.FindIndex(se => se.Machine_Type_ID == _iMachineTypeID);
                            cmbCategory.SelectedIndex = (ind >= 0) ? ind : 0;
                        }
                        oAudit.Machine_Name = txtAssetTitle.Text = MC.Machine_Name;

                    }
                    else
                    {
                        InVisibleControls(true);
                        if (isEditTemplate)
                        {
                            oAudit.Stock_No = txtStockNumber.Text = MC.Machine_Stock_No;
                        }
                        //AutoGenerateStockNo = SystemParameterBiz.CreateInstance().GeSystemParameterSettings();
                        //txtStockNumber.Enabled = (!AutoGenerateStockNo) ? true : false;
                        chkTITO.Checked = false;
                        oAudit.IsTITOEnabled = chkTITO.Checked = MC.IsTITOEnabled ?? false;
                        oAudit.IsNonCashVoucherEnabled = chkNonCashable.Checked = MC.IsNonCashVoucherEnabled ?? false;
                        //Game
                        if (MC.IsMultiGame ?? false)
                        {
                            chkMultiGame.Checked = true;
                            cmbCategory.Enabled = false;
                        }
                        else
                        {
                            chkMultiGame.Checked = false;
                            cmbCategory.Enabled = true;
                        }
                       // txt_MultiGame.Text = MC.MultiGameName ?? "";
                        oAudit.GetGameDetails = chkGetGameDetails.Checked = MC.GetGameDetails;
                        chkGameCapping.Checked = MC.IsGameCappingEnabled ?? false;
                        chkGetGameDetails.Enabled = !(MC.Machine_Status_Flag == (int)StockStatus.STOCK_IN_USE);
                        oAudit.AFTEnable = chkAFTEnabled.Checked = MC.IsAFTEnabled ?? false;                        
                        oAudit.IsGameCappingEnabled = chkGameCapping.Checked;
                        //PreviousState = MC.IsMultiGame ?? false; //Added to send change to exchange when editing a machine only if it the mulitgame checkbox value changed
                        PreviousMachineAFTState = MC.IsAFTEnabled ?? false;

                    }

                    try
                    {
                        chkGameCapping.Visible = Convert.ToBoolean(AdminBusiness.GetSetting("IsGameCappingEnabled", "FALSE")) && !IsNGA; 
                    }
                    catch
                    {
                        chkGameCapping.Visible = true;
                    }

                    if (!isFromTemplate)
                    {
                        GetStaffDetails(MC);
                    }
                    if (isFromTemplate)
                    {
                        this.Text = isEditTemplate ? this.GetResourceTextByKey(1, "MSG_TEMPLATE_EDIT") : this.GetResourceTextByKey(1, "MSG_PURCHASE_MACHINE");       // "Edit Template" : "Purchase Machine";
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(MC.Machine_BACTA_Code))
                        {
                            this.Text = string.Format(this.GetResourceTextByKey(1, "MSG_EDIT_STOCK_PARAM"), MC.Machine_Name, MC.Machine_BACTA_Code) + MC.Machine_Stock_No;
                            //this.Text = "Edit " + MC.Machine_Name + " [" + MC.Machine_BACTA_Code + "] - Stock:" + MC.Machine_Stock_No;
                        }
                        else
                        {
                            this.Text = string.Format(this.GetResourceTextByKey(1, "MSG_EDIT_STOCK"), MC.Machine_Name) + (isFromTemplate ? txtStockNumber.Text : MC.Machine_Stock_No);
                            //this.Text = "Edit " + MC.Machine_Name + " - Stock:" + (isFromTemplate ? txtStockNumber.Text : MC.Machine_Stock_No);
                        }
                    }
                    if (isFromTemplate)
                    {
                        chkKeepWindowActive.Visible = true;
                    }
                    else
                    {
                        //frameDepreciationValue.Visible = true;
                        chkKeepWindowActive.Visible = false;
                    }
                    if (MC.Machine_End_Date == string.Empty)
                    {
                      // btnSellMachine.Visible = !isEditTemplate;
                        btnOK.Enabled = true;
                    }
                    else
                    {
                        if (!isFromTemplate)
                        {
                            LockAllControls(true);
                            btnSellMachine.Visible = false;
                            btnOK.Enabled = false;
                        }
                    }

                    AssignMachineDetailToCtrl(MC, isFromTemplate);


                    decimal NBV = 0;
                    decimal DepPerWeek = 0;
                    decimal PurcPrice = 0;

                    GetDepreciationDetailsFromMachineID(AutoMachineID, ref NBV, ref DepPerWeek, ref  PurcPrice, "");

                    txtNBV.Text = NBV.ToString("###,##0.00");
                    txtWeeklyDepreciation.Text = DepPerWeek.ToString("###,##0.00");
                    btnOK.Tag = (isFromTemplate ?  "Key_Buy" : "Key_UpdateCaption");                    
                   // btnOK.Text = (isFromTemplate ? "Buy" : "Update");
                    oAudit.NBV = txtNBV.Text;
                    oAudit.WeeklyDepreciation = txtWeeklyDepreciation.Text;

                    if (!isFromTemplate)
                    {
                        CheckMachineStatus(MC);
                    }
                    if (cmbCategory.DataSource is List<GetGameTitleResult>)
                    {
                        List<GetGameTitleResult> lst_game = cmbCategory.DataSource as List<GetGameTitleResult>;
                        if (lst_game != null && lst_game.Find(obj => obj.Game_Title.ToUpper().Equals("MULTI GAME")) != null)
                        {
                            cmbCategory.Enabled = false;
                        }

                        if (lst_game.Count > 0)
                        {
                            ind = lst_game.FindIndex(se => se.Game_Title_ID == MC.MG_Game_ID);
                            cmbCategory.SelectedIndex = (ind >= 0) ? ind : 0;
                            oAudit.Game_Title = cmbCategory.Text;
                        }

                    }


                }
                else
                {
                    Win32Extensions.ShowInfoMessageBox(this,this.GetResourceTextByKey(1, "MSG_MACHINEDETAILS_RETRIVE"), this.Text);            // "Unable to retrive machine details");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                Win32Extensions.ShowInfoMessageBox(this,this.GetResourceTextByKey(1, "MSG_MACHINEDETAILS_RETRIVE"), this.Text);            // "Unable to retrive machine details");
            }

        }

        ///// <summary>
        ///// this method is call when edit a existing M\C
        ///// </summary>
        ///// <param name="MachineID"></param>
        ///// <param name="bIsNGA"></param>
        ///// <param name="Inuse"></param>
        //public void ShowMeEdit(int MachineID, bool bIsNGA, bool Inuse, bool isFromTemplate)
        //{
        //    try
        //    {
        //        AutoMachineID = MachineID;
        //        OldMach = false;
        //        _IsEdit = false;

        //        List<GetBuyMachineDetailsResult> lst_buyMC = BuyMachineBiz.CreateInstance().GetBuyMachineDetails(AutoMachineID);
        //        if (lst_buyMC != null && lst_buyMC.Count > 0)
        //        {
        //            LogManager.WriteLog(ScreenName + "ShowMeEdit:Get Machine details:" + AutoMachineID, LogManager.enumLogLevel.Info);
        //            GetBuyMachineDetailsResult MC = lst_buyMC[0];
        //            ModelDepreciationID = (MC.Class_Depreciation != null && MC.Class_Depreciation > 0) ? MC.Class_Depreciation : MC.Type_Depreciation;


        //            chkAFTEnabled.Checked = MC.IsAFTEnabled ?? false;
        //            //cmbAssetTemplate.Items.Add(lst_buyMC)

        //            int ind = 0;
        //            SetSelectedIndex(MC, true);
        //            if (IsNGA)
        //            {
        //                InVisibleControls(false);
        //                List<GetMachineTypeDetailsResult> lst_MCType = (List<GetMachineTypeDetailsResult>)cmbCategory.DataSource;
        //                if (lst_MCType != null && lst_MCType.Count > 0)
        //                {
        //                    ind = lst_MCType.FindIndex(se => se.Machine_Type_ID == MC.Machine_Class_Category_ID);
        //                    cmbCategory.SelectedIndex = (ind >= 0) ? ind : 0;
        //                }
        //                txtAssetTitle.Text = MC.Machine_Name;


        //            }
        //            else
        //            {
        //                InVisibleControls(true);
        //                chkTITO.Checked = MC.IsTITOEnabled ?? false;
        //                chkNonCashable.Checked = MC.IsNonCashVoucherEnabled ?? false;
        //                //Game
        //                if (MC.IsMultiGame ?? false)
        //                {
        //                    chkMultiGame.Checked = true;
        //                    cmbCategory.Enabled = false;
        //                }
        //                else
        //                {
        //                    chkMultiGame.Checked = false;
        //                    cmbCategory.Enabled = true;
        //                }
        //                //PreviousState = MC.IsMultiGame ?? false; //Added to send change to exchange when editing a machine only if it the mulitgame checkbox value changed
        //                PreviousMachineAFTState = MC.IsAFTEnabled ?? false;

        //            }

        //            // GetStaffDetails(MC);

        //            if (!String.IsNullOrEmpty(MC.Machine_BACTA_Code))
        //            {
        //                this.Text = "Edit " + MC.Machine_Name + " [" + MC.Machine_BACTA_Code + "] - Stock:" + MC.Machine_Stock_No;
        //            }
        //            else
        //            {
        //                this.Text = "Edit " + MC.Machine_Name + " - Stock:" + txtStockNumber.Text;
        //            }
        //            frameDepreciationValue.Visible = true;
        //            if (MC.Machine_End_Date == string.Empty)
        //            {
        //                //LockAllControls(false);
        //                btnSellMachine.Visible = true;
        //                btnOK.Enabled = true;
        //            }
        //            else
        //            {
        //                LockAllControls(true);
        //                btnSellMachine.Visible = false;
        //                btnOK.Enabled = false;
        //            }

        //            AssignMachineDetailToCtrl(MC, true);


        //            decimal NBV = 0;
        //            decimal DepPerWeek = 0;
        //            decimal PurcPrice = 0;

        //            GetDepreciationDetailsFromMachineID(AutoMachineID, ref NBV, ref DepPerWeek, ref  PurcPrice, "");

        //            txtNBV.Text = NBV.ToString("###,##0.00");
        //            txtWeeklyDepreciation.Text = DepPerWeek.ToString("###,##0.00");


        //            btnOK.Text = "Buy";

        //            // CheckMachineStatus(MC);
        //            if (cmbCategory.DataSource is List<GetGameTitleResult>)
        //            {
        //                List<GetGameTitleResult> lst_game = cmbCategory.DataSource as List<GetGameTitleResult>;
        //                if (lst_game != null && lst_game.Find(obj => obj.Game_Title.ToUpper().Equals("MULTI GAME")) != null)
        //                {
        //                    cmbCategory.Enabled = false;
        //                }

        //                if (lst_game.Count > 0)
        //                {
        //                    ind = lst_game.FindIndex(se => se.Game_Title_ID == MC.MG_Game_ID);
        //                    cmbCategory.SelectedIndex = (ind >= 0) ? ind : 0;
        //                }

        //            }


        //        }
        //        else
        //        {
        //            Win32Extensions.ShowInfoMessageBox("Unable to retrive machine details");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //        Win32Extensions.ShowInfoMessageBox("Unable to retrive machine details");
        //    }

        //}


        /// <summary>
        /// lock each controls
        /// </summary>
        /// <param name="Locked"></param>
        private void LockAllControls(bool Locked)
        {
            foreach (Control myCtl in this.Controls)
            {
                if ((myCtl.Name != "SSTab1"))
                {
                    myCtl.Enabled = Locked;
                }
            }

        }

        /// <summary>
        /// Set Selected index for operator,depot,staff,manufacturer,modeltype,stacker,depreciation
        /// </summary>
        /// <param name="MC"></param>
        void SetSelectedIndex(GetBuyMachineDetailsResult MC, bool isFromTemplate)
        {
            try
            {
                int ind = 0;
                List<OperatorEntity> lst_Operator = (List<OperatorEntity>)cmbOperators.DataSource;
                if (lst_Operator != null && lst_Operator.Count > 0)
                {
                    ind = lst_Operator.FindIndex(se => se.Operator_ID == MC.Operator_ID);
                    cmbOperators.SelectedIndex = (ind >= 0) ? ind : 0;
                    oAudit.Operator_Name = cmbOperators.Text;
                }

                List<DepotEntity> lst_Depot = (List<DepotEntity>)cmbDepot.DataSource;
                if (lst_Depot != null && lst_Depot.Count > 0)
                {
                    ind = lst_Depot.FindIndex(se => se.Depot_ID == MC.Depot_ID);
                    cmbDepot.SelectedIndex = (ind >= 0) ? ind : 0;
                    oAudit.Depot_Name = cmbDepot.Text;
                }


                txtGameType.Text = MC.GameTypeCode ?? "";
                oAudit.GameTypeCode = (string.IsNullOrEmpty(txtGameType.Text))? "" : txtGameType.Text;
                txtGamePrefix.Text = (MC.CMPGameType ?? '\0').ToString();
                oAudit.GamePrefix = (string.IsNullOrEmpty(txtGamePrefix.Text))? "":txtGamePrefix.Text;
                txtDisplayName.Text = MC.AssetDisplayName;
                oAudit.AssetDisplayName = (string.IsNullOrEmpty(txtDisplayName.Text)) ? "" : txtDisplayName.Text; ;                         
                List<GetStaffByDepotResult> lst_Staff = (List<GetStaffByDepotResult>)cmbRep.DataSource;
                if (lst_Staff != null && lst_Staff.Count > 0)
                {
                    ind = lst_Staff.FindIndex(se => se.Staff_ID == MC.Staff_ID);
                    cmbRep.SelectedIndex = (ind >= 0) ? ind : 0;
                    oAudit.Rep_Name = cmbRep.Text;
                }

                List<Manufacturer> lst_Manf = (List<Manufacturer>)cmbManufacturer.DataSource;
                if (lst_Manf != null && lst_Manf.Count > 0)
                {
                    ind = lst_Manf.FindIndex(se => se.Manufacturer_ID == MC.Manufacturer_ID);
                    cmbManufacturer.SelectedIndex = (ind >= 0) ? ind : 0;
                    oAudit.Manufacturer_Name = cmbManufacturer.Text;
                }

                List<GetModelTypeResult> lst_MCModel = (List<GetModelTypeResult>)cmbModelType.DataSource;
                if (lst_MCModel != null && lst_MCModel.Count > 0)
                {
                    ind = lst_MCModel.FindIndex(se => se.MT_ID == MC.Machine_ModelTypeID);
                    cmbModelType.SelectedIndex = (ind >= 0) ? ind : 0;
                    oAudit.Model_Type = cmbModelType.Text;
                }

                List<GetActiveStackerDetailsResult> lst_Stack = (List<GetActiveStackerDetailsResult>)cmbStackerList.DataSource;
                if (lst_Stack != null && lst_Stack.Count > 0)
                {
                    ind = lst_Stack.FindIndex(se => se.ActiveStacker_Id == MC.Stacker_Id);
                    cmbStackerList.SelectedIndex = ind;
                    if(!IsNGA)
                    oAudit.Stacker_Name = cmbStackerList.Text;
                }

                List<DepreciationEntity> lst_Deprec = (List<DepreciationEntity>)cmbDepreciation.DataSource;
                if (lst_Deprec != null && lst_Deprec.Count > 0)
                {
                    ind = lst_Deprec.FindIndex(se => se.Depreciation_Policy_ID == MC.Depreciation_Policy_ID);
                    cmbDepreciation.SelectedIndex = ind;
                    oAudit.Depreciation = cmbDepreciation.Text;
                }
                if (isFromTemplate) return;
                List<clsStockStatus> lst_stock = (List<clsStockStatus>)cmbStatus.DataSource;
                if (lst_stock != null && lst_stock.Count > 0)
                {
                    ind = lst_stock.FindIndex(se => se.StockValue == (MC.Machine_Status_Flag ?? 0));
                    cmbStatus.SelectedIndex = (ind >= 0) ? ind : 0;
                    oAudit.Status = (cmbStatus.SelectedItem as clsStockStatus).Caption;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// MC - Machine Details
        /// </summary>
        /// <param name="MC"></param>
        void AssignMachineDetailToCtrl(GetBuyMachineDetailsResult MC, bool isFromTemplate)
        {
            try
            {
                if (isFromTemplate)
                {
                    oAudit.Machine_Purchased_From = txtMachinePurchasedFrom.Text = MC.Machine_Purchased_From;
                    oAudit.Machine_Alternative_Serial_Numbers = txtAltStockNumbers.Text = MC.Machine_Alternative_Serial_Numbers;
                    txtValidationLength.Text = (MC.Validation_Length ?? 0).ToString();
                    oAudit.ActStockNo = txt_ActualStockNo.Text = MC.ActAssetNo ?? "0";
                    oAudit.GMUNo = txt_GMUNo.Text = MC.GMUNo ?? "0";
                    oAudit.ActSerialNo = txtSerialNumber.Text = MC.ActSerialNo ?? "0";
                    oAudit.Validation_Length = string.IsNullOrEmpty(txtValidationLength.Text)?0:Convert.ToInt32(txtValidationLength.Text);
                    oAudit.Machine_MAC_Address = txtMACAddress.Text = MC.Machine_MAC_Address;
                    oAudit.Machine_Memo = rtfNotes.Text = MC.Machine_Memo;
                    oAudit.Original_Price = txtOriginalPurchasePrice.Text = (MC.Machine_Original_Purchase_Price ?? 0).ToString("0.00");
                    oAudit.Invoice_Number = txtPurchaseInvoiceNumber.Text = MC.Machine_Purchase_Invoice_Number;
                    oAudit.IsDefaultAssetDetail = chkDefaultAssetDetail.Checked = MC.IsDefaultAssetDetail ?? false;
                    txtPercentagePayOut.Text = MC.Percentage_Payout.ToString();
                    oAudit.Percentage_Payout = Convert.ToInt32(txtPercentagePayOut.Text);
                    cmbBaseDenom.Text = MC.Base_Denom.ToString();
                    oAudit.Base_Denom = string.IsNullOrEmpty(cmbBaseDenom.Text)?0:Convert.ToInt32(cmbBaseDenom.Text);
                    oAudit.GetGameDetails = chkGetGameDetails.Checked = MC.GetGameDetails;
                    oAudit.IsNonCashVoucherEnabled = chkNonCashable.Checked = MC.IsNonCashVoucherEnabled ?? false;
                    oAudit.IsGameCappingEnabled = chkGameCapping.Checked = MC.IsGameCappingEnabled ?? false;
                    if (!IsNGA)
                    {
                        txtOccupanyPerhour.Text = (MC.Machine_Class_Occupancy_Games_Per_Hour ?? 0).ToString();
                        oAudit.Occupancy_Games_Per_Hour = Convert.ToInt32(txtOccupanyPerhour.Text);
                    }
                    cmbStackerList.SelectedValue = Convert.ToInt32(MC.Stacker_Id);
                    oAudit.Stacker_Name = cmbStackerList.Text;
                }
                else
                {

                    oAudit.Stock_No = txtStockNumber.Text = MC.Machine_Stock_No;
                    oAudit.ActStockNo = txt_ActualStockNo.Text = MC.ActAssetNo ?? "0";
                    oAudit.GMUNo = txt_GMUNo.Text = MC.GMUNo ?? "0";
                    oAudit.ActSerialNo = txtSerialNumber.Text = MC.ActSerialNo ?? "0";

                    oAudit.Machine_Purchased_From = txtMachinePurchasedFrom.Text = MC.Machine_Purchased_From;
                    oAudit.Machine_Alternative_Serial_Numbers = txtAltStockNumbers.Text = MC.Machine_Alternative_Serial_Numbers;
                    strAltSerial = txtAltStockNumbers.Text;
                    oAudit.Machine_MAC_Address = txtMACAddress.Text = MC.Machine_MAC_Address;
                    strMacAddr = txtMACAddress.Text;
                    txtValidationLength.Text = (MC.Validation_Length ?? 0).ToString();
                    oAudit.Validation_Length = Convert.ToInt32(txtValidationLength.Text);
                    if (!IsNGA)
                    {
                        txtOccupanyPerhour.Text = (MC.Machine_Class_Occupancy_Games_Per_Hour ?? 0).ToString();
                        oAudit.Occupancy_Games_Per_Hour = Convert.ToInt32(txtOccupanyPerhour.Text);
                    }
                    bool OldMachine = (MC.Old_Machine_ID > 0);


                    DateTime dtDepreciationStDate = new DateTime();
                    DateTime dtMachineStDate = new DateTime();
                    if (DateTime.TryParse(MC.Machine_Depreciation_Start_Date, out dtDepreciationStDate))
                    {
                        DTDepreciationStartDate.Value = dtDepreciationStDate;
                    }
                    else if (DateTime.TryParse(OldMachine ? MC.Old_Machine_Start_Date : MC.Machine_Start_Date, out dtMachineStDate))
                    {
                        DTDepreciationStartDate.Value = dtMachineStDate;
                    }
                    else
                    {
                        DTDepreciationStartDate.Value = OldMachine ? DateTime.Parse(MC.Machine_Start_Date) : DateTime.Parse("01/01/1980");
                    }
                    oAudit.DepreciationStartDate = DTDepreciationStartDate.Value.Date;
                    DTDelivery.Enabled = !OldMachine;
                    if (DateTime.TryParse(OldMachine ? MC.Old_Machine_Start_Date : MC.Machine_Start_Date, out dtMachineStDate))
                    {
                        DTDelivery.Value = dtMachineStDate;
                    }
                    else
                    {
                        DTDelivery.Value = DateTime.Now;
                    }
                    oAudit.DeliveryDate = DTDelivery.Value.Date;
                    oAudit.Original_Price = txtOriginalPurchasePrice.Text = (MC.Machine_Original_Purchase_Price ?? 0).ToString("0.00");
                    oAudit.Invoice_Number = txtPurchaseInvoiceNumber.Text = MC.Machine_Purchase_Invoice_Number;
                    oAudit.Machine_Memo = rtfNotes.Text = MC.Machine_Memo;
                    chkDepreciationUseDefault.Checked = MC.Depreciation_Policy_Use_Default ?? false;
                    oAudit.IsDefaultAssetDetail = chkDefaultAssetDetail.Checked = MC.IsDefaultAssetDetail ?? false;
                    txtPercentagePayOut.Text = MC.Percentage_Payout.ToString();
                    oAudit.Percentage_Payout =Convert.ToInt32(txtPercentagePayOut.Text);
                    cmbBaseDenom.Text = MC.Base_Denom.ToString();
                    oAudit.Base_Denom = (string.IsNullOrEmpty(cmbBaseDenom.Text))?0:Convert.ToInt32(cmbBaseDenom.Text);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Check stacker feature is enabled or not
        /// </summary>
        void IsStackerFeatureEnabled()
        {
            try
            {
                bool IsStackerEnabled = (AdminBusiness.GetSetting("StackerFeature", "FALSE").ToUpper().Trim() == "TRUE");
                cmbStackerList.Visible = IsStackerEnabled;
                lblStackerName.Visible = IsStackerEnabled;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// enable\disable controls for GA
        /// </summary>
        void EnableDisableControls(bool NonNGA, bool DefaultSettings)
        {
            try
            {
                if (DefaultSettings)
                {
                    txtMACAddress.Text = "";
                    txtMachinePurchasedFrom.Text = "";
                    txtSerialNumber.Text = "0";
                    txtAltStockNumbers.Text = "";

                    txtMACAddress.Enabled = true;
                    chkDepreciationUseDefault.Checked = true;
                    btnOK.Text = this.GetResourceTextByKey("Key_Buy");
                    chkDepreciationUseDefault.Visible = true;
                    btnOK.Enabled = true;
                    btnSellMachine.Visible = false;
                    //frameDepreciationValue.Visible = false;
                    bool IsAFTEnabled = IsAFTEnabledForSite();
                    txtGameType.Enabled = IsAFTEnabled;
                    txtGamePrefix.Enabled = IsAFTEnabled;
                    chkAFTEnabled.Visible = !NonNGA && IsAFTEnabled;
                }
                else
                {
                    txt_ActualStockNo.Enabled = NonNGA;
                    txt_GMUNo.Enabled = NonNGA;
                    txtOccupanyPerhour.Visible = NonNGA;
                    lblOccupancyHR.Visible = NonNGA;
                    txtAssetTitle.Visible = !NonNGA;
                    lblAsset.Visible = !NonNGA;
                    chkMultiGame.Visible = NonNGA;
                    lblGamelibrary.Text = NonNGA ? "* Game Title :" : "Category";
                    lblValidationLength.Enabled = NonNGA;
                    txtValidationLength.Enabled = NonNGA;
                    chkAFTEnabled.Visible = NonNGA;                    
                    chkNonCashable.Enabled = NonNGA;
                    chkNonCashable.Visible = NonNGA;
                    txtSerialNumber.Enabled = NonNGA;
                    txtGamePrefix.Visible = NonNGA;
                    txtGameType.Visible = NonNGA;
                    lblCMpGameType.Visible = NonNGA;
                    lblGamePrefix.Visible = NonNGA;
                    cmbCategory.Enabled = NonNGA;
                    chkKeepWindowActive.Visible = NonNGA;
                    lblDisplayName.Visible = NonNGA;
                    txtDisplayName.Visible = NonNGA;
                    if (NonNGA)
                    {
                        txt_ActualStockNo.Text = "0";
                        txt_GMUNo.Text = "0";
                        txtSerialNumber.Text = "0";
                        IsStackerFeatureEnabled();
                    }
                    else
                    {
                        cmbStackerList.Visible = false;
                        lblSerialNo.Enabled = false;
                        lblAltSerialNo.Enabled = false;
                        chkTITO.Visible = false;
                        lbl_ActualStockNo.Enabled = false;
                        lbl_GMUNo.Enabled = false;
                        chkAFTEnabled.Visible = false;
                        chkTITO.Enabled = false;
                        lblStackerName.Visible = false;
                        chkGetGameDetails.Visible = false;
                        txtAltStockNumbers.Enabled = false;
                        lblValidationLength.Visible = true;
                        txtValidationLength.Visible = true;
                        cmbManufacturer.Enabled = false;
                        chkDefaultAssetDetail.Visible = false;
                        //grpAssetDetails.Visible = false;
                        chkGameCapping.Visible = false;
                        lblMachineType.Visible = false;
                        cmbMachineType.Visible = false;
                        txt_ActualStockNo.Visible = false;
                        txt_GMUNo.Visible = false;
                        txtValidationLength.Visible = false;
                        lbl_ActualStockNo.Visible = false;
                        lbl_GMUNo.Visible = false;
                        lblValidationLength.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                // ''Check if Stacker Level alert global setting is false -Ends
            }
        }


        /// <summary>
        /// visible\Invisible controls for GA
        /// </summary>
        void InVisibleControls(bool NonNGA)
        {
            chkNonCashable.Visible = false;
            chkNonCashable.Enabled = false;
            txtStockNumber.Enabled = (AutoGenerateStockNo && isFromTemplate);

            txtAssetTitle.Visible = !NonNGA;
            lblAsset.Visible = !NonNGA;
            lblGamelibrary.Text = NonNGA ? "Game Title" : "Category";
            lblValidationLength.Visible = NonNGA;
            txtValidationLength.Visible = NonNGA;
            chkKeepWindowActive.Visible = NonNGA;
            chkTITO.Enabled = NonNGA;
            chkTITO.Visible = NonNGA;
            txtOccupanyPerhour.Visible = NonNGA;
            lblOccupancyHR.Visible = NonNGA;
            chkAFTEnabled.Visible = (NonNGA && IsAFTEnabledForSite());
            chkGetGameDetails.Visible = NonNGA;
            chkGetGameDetails.Enabled = NonNGA;
            chkGetGameDetails.Checked = true;            
            //Check is AFT Enabled
            bool IsAFTEnabled = IsAFTEnabledForSite();
            chkAFTEnabled.Visible = NonNGA && IsAFTEnabled;
            txtGamePrefix.Enabled = IsAFTEnabled;
            txtGameType.Enabled = IsAFTEnabled;
            chkKeepWindowActive.Visible = NonNGA;
            txtDisplayName.Visible = NonNGA;
            lblDisplayName.Visible = NonNGA;
            //Check Stack Feature
            IsStackerFeatureEnabled();
            lblMachineType.Visible = NonNGA;
            cmbMachineType.Visible = NonNGA;
            if (!NonNGA)
            {
                chkGetGameDetails.Visible = false;
                txtGamePrefix.Visible = false;
                txtGameType.Visible = false;
                lblCMpGameType.Visible = false;
                lblGamePrefix.Visible = false;
                cmbStackerList.Visible = false;
                txtAltStockNumbers.Enabled = false;
                txt_ActualStockNo.Visible = false;
                txt_GMUNo.Visible = false;
                lbl_ActualStockNo.Visible = false;
                txtAssetTitle.Enabled = false;
                cmbCategory.Enabled = false;
                lbl_GMUNo.Visible = false;
                cmbManufacturer.Enabled = false;
                chkMultiGame.Visible = false;
                lblStackerName.Visible = false;
                chkDefaultAssetDetail.Visible = false;
                //grpAssetDetails.Visible = false;
                chkGameCapping.Visible = false;

            }

        }

        /// <summary>
        /// To Check AFT Setting is enabled or not.
        /// </summary>
        /// <returns></returns>
        bool IsAFTEnabledForSite()
        {
            if (AFTEnabledForSite == null)
            {
                AFTEnabledForSite = (AdminBusiness.GetSetting("IsAFTEnabledForSite", "FALSE").ToUpper().Trim() == "TRUE");
            }
            return AFTEnabledForSite.Value;
        }

        /// <summary>
        /// this method is called from FormClose & Cancel Click
        /// </summary>
        /// <param name="IsClosing">For FormClose false</param>
        void FormClose(bool IsClosing)
        {
            try
            {
                if (!DelMachineIDandRefreshFlag && !_IsEdit)
                {
                    string myStockNo = "";
                    //dell temp Record
                    BuyMachineBiz.CreateInstance().AddOrDeleteMachineDetails(AutoMachineID, null, null, ref myStockNo);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadCmbTemplate()
        {
            try
            {

                List<GetAssetTemplateDetailsResult> objTemplate = AssetManagementBiz.CreateInstance().DisplayTemplate();
                objTemplate.Insert(0, new GetAssetTemplateDetailsResult { AssetCrTempNumber = -1, TemplateName = this.GetResourceTextByKey("Key_None") });              //"[NONE]"
                cmbAssetTemplate.SelectedIndexChanged -= cmbAssetTemplate_SelectedIndexChanged;
                cmbAssetTemplate.DisplayMember = "TemplateName";
                cmbAssetTemplate.ValueMember = "AssetCrTempNumber";
                cmbAssetTemplate.DataSource = objTemplate;
                cmbAssetTemplate.SelectedIndexChanged += cmbAssetTemplate_SelectedIndexChanged;
                isLoading = false;
                oAudit.Asset_Template = cmbAssetTemplate.Text;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadDefault()
        {
            try
            {
                txtOccupanyPerhour.Text = "";
                cmbManufacturer.SelectedIndex = 0;
                cmbOperators.SelectedIndex = 0;
                cmbRep.SelectedIndex = 0;
                cmbRep.Enabled = true;
                chkKeepWindowActive.Visible = true;
                chkKeepWindowActive.Enabled = true;
                txtMachinePurchasedFrom.Text = "";
                txt_ActualStockNo.Text = "";
                txtSerialNumber.Text = "";
                txt_GMUNo.Text = "";
                txtAltStockNumbers.Text = "";
                txtValidationLength.Text = "";
                txtGameType.Text = "";
                txtGamePrefix.Text = "";
                cmbModelType.SelectedIndex = 0;
                cmbStackerList.SelectedIndex = 0;
                chkAFTEnabled.Checked = false;
                chkGameCapping.Checked = false;
                chkTITO.Checked = false;
                chkNonCashable.Visible = false;
                chkDefaultAssetDetail.Checked = false;
                cmbCategory.SelectedIndex = 0;
                rtfNotes.Text = "";
                txtOriginalPurchasePrice.Text = "0";
                oAudit.Original_Price = "0";
                txtPurchaseInvoiceNumber.Text = "";
                chkDepreciationUseDefault.Checked = true;
                txtPercentagePayOut.Text = "";
                txtMACAddress.Text = "";
                cmbBaseDenom.Text = "1";
                txt_ActualStockNo.Text = "0";
                txt_GMUNo.Text = "0";
                txtSerialNumber.Text = "0";
                chkGetGameDetails.Enabled = true;
                chkGetGameDetails.Checked = true;
                cmbRep.SelectedIndex = -1;
                chkMultiGame.Checked = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void GetMachineId(string TemplateName, ref int MachineId)
        {
            try
            {
                BuyMachineBiz objBuyBiz = new BuyMachineBiz();
                objBuyBiz.GetMachine_IDForTemplate(TemplateName, ref MachineId);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        #endregion

        #region Events

        /// <summary>
        /// Buy M/C Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmBuy_Load(object sender, EventArgs e)
        {
            try
            {
                int Size = Convert.ToInt32("0" + AdminBusiness.GetSetting("AliesStockNumberMaxCharSize", "10"));
                txtStockNumber.MaxLength = (Size > 0) ? Size : 10;
                btnSellMachine.Visible = AppGlobals.Current.HasUserAccess("HQ_Stock_Edit");
               // SSTab1.SelectedIndex = 0;
                ChkSalesItem.Enabled = false;
                chkGetGameDetails.Checked = true;
                // Change Request #205362  fix.
                DTDelivery.CustomFormat = Common.Utilities.Common.GetDateFormatByUserSetting();
                DTDepreciationStartDate.CustomFormat = Common.Utilities.Common.GetDateFormatByUserSetting();

                DTDelivery.Value = DateTime.Now;
                DTDepreciationStartDate.Value = DateTime.Now;
                LoadMultiGameName();
                SetStockStatus();
                LoadManufacturer();//load  Manufacturer
                LoadOperator(); // for operator combo box
                LoadDepreciationDetails();// depreciation combo box
                
                LoadMachineTypeDetails();//load M\C Type details
                
                if (!IsNGA)
                {
                    LoadGameTitle(false);//load Game details

                    objAdminSystemSettingsResult = _AdminSettings.GetSystemSettingDetails();
                    this.Tag = "Key_BuyMachine";

                    if (objAdminSystemSettingsResult != null)
                    {
                        if (objAdminSystemSettingsResult.SystemSettings.RegionCulture.ToUpper() == "EN-GB")
                        {
                            lblBaseDenom.Tag = "Key_Denom_Penny";                           
                        }
                        else if (objAdminSystemSettingsResult.SystemSettings.RegionCulture.ToUpper() == "ES-AR")
                        {
                            lblBaseDenom.Tag = "Key_Denom_Centavo";
                        }
                        else
                        {
                            lblBaseDenom.Tag = "Key_BaseDenomCents";
                        }
                    }

                    cmbBaseDenom.DisplayMember = "Text";
                    cmbBaseDenom.ValueMember = "Value";
                    cmbBaseDenom.Items.Clear();

                    IList<ComboBoxItem<int>> items = new List<ComboBoxItem<int>>();
                    Extensions.GetAppSettingValue("BaseDenom", "0")
                        .Split(',')
                        .OrderBy(s => s.ConvertToInt32())
                        .ForEachItem(s =>
                            cmbBaseDenom.Items.Add(new ComboBoxItem<int>()
                            {
                                Text = s,
                                Value = s.ConvertToInt32()
                            }));

                    if (cmbBaseDenom.Items.Count > 0) cmbBaseDenom.SelectedIndex = 0;

                }

                LoadModelType(IsNGA);//load Model Type details

                LoadStackerDetails();//load Stacker details

                if (_IsEdit)
                {
                    isFromTemplate = false;
                    ShowMeEdit(BuyMCClassAndMachineID, "", IsNGA, _Inuse);
                }
                else
                {

                    ShowMe(BuyMCClassAndMachineID, IsNGA);
                }
                chkNonCashable.Visible = chkTITO.Checked;
                LoadCmbTemplate();
                this.ResolveResources();

            }
            catch (Exception ex)
            {
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_MACHINEDETAILS_RETRIVE"), this.Text);            // "Unable to retrive machine details");
                ExceptionManager.Publish(ex);
            }

        }
        private void LoadMultiGameName()
        {
            try
            {
                string s_MultiGameName = "";
                BuyMachineBiz.CreateInstance().GetSetting(0, "CustomMultiGameName", "True", ref s_MultiGameName);
                IsCustomMultiGameName = (s_MultiGameName != "" && s_MultiGameName.ToLower() == "true");
            }
            catch (Exception ex)
            {
                Win32Extensions.ShowInfoMessageBox("Unable to Load MultiGame Name");
                ExceptionManager.Publish(ex);
            }
        }

        private void chkTITO_CheckedChanged(object sender, EventArgs e)
        {
            bool bTITO = chkTITO.Checked;
            chkNonCashable.Enabled = bTITO;
            chkNonCashable.Visible = bTITO;
            chkNonCashable.Checked = bTITO;

        }

        #region UpdateMethod

        /// <summary>
        /// Add Or Update machine details when buy button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                bool _BuyMachineDataChange = false;
                string ErrorMsg = "";                
            
                if (_isNew)
                {
                    oAudit.DeliveryDate = DateTime.Now.Date;
                    oAudit.DepreciationStartDate = DateTime.Now.Date;
                }
                if (!IsNGA)
                {
                    iBaseDenom = ((ComboBoxItem<int>)cmbBaseDenom.SelectedItem).Value;
                    dPercentage_Payout = Convert.ToSingle("0" + txtPercentagePayOut.Text);
                }
                string sAssetNo = string.Empty;

                if (Validate(ref ErrorMsg))
                {
                    DelMachineIDandRefreshFlag = true;
                    LogManager.WriteLog(ScreenName + " btnOk_Click:", LogManager.enumLogLevel.Info);

                    if (txtSerialNumber.Text.Trim().Equals(string.Empty))
                    {
                        txtSerialNumber.Text = "0";
                    }
                    if (txt_ActualStockNo.Text.Trim().Equals(string.Empty))
                    {
                        txt_ActualStockNo.Text = "0";
                    }
                    if (txt_GMUNo.Text.Trim().Equals(string.Empty))
                    {
                        txt_GMUNo.Text = "0";
                    }
                    sAssetNo = txtStockNumber.Text;
                    if (txtSerialNumber.Text != "")
                    {
                        int ManufacturerID = cmbManufacturer.SelectedIndex >= 0 ? ((int)cmbManufacturer.SelectedValue) : 0;
                        int? ValidationLength = (txtValidationLength.Text == "") ? (int?)null : Convert.ToInt32(txtValidationLength.Text);
                        int? MachineClass_ID = 0;
                        int? MCType_ID = IsNGA? _iMachineTypeID : Convert.ToInt32(cmbMachineType.SelectedValue);

                        if (IsNGA && !_IsEdit)
                        {
                            MachineClass_ID = BuyMCClassAndMachineID;
                        }
                        #region Add/Update Machine_Class

                        // Add/Update Machine_Class Details
                        if (BuyMachineBiz.CreateInstance().AddMachineClass_Details(AutoMachineID, (chkMultiGame.Checked ? "MULTI GAME" : cmbCategory.Text), _IsEdit, IsNGA, ManufacturerID, ref MCType_ID,
                             BuyMCClassAndMachineID, SF_VALUE_SASPROTOCOL, cmbCategory.Text,
                              0, true, 
                              //Convert.ToInt32("0" + txtOccupanyPerhour.Text),
                              0, 0,
                              0, 0,
                              "0", "0",
                              false, false,
                              99999999, false, ValidationLength, ref MachineClass_ID))
                        {

                            CurrentMachineAFTState = chkAFTEnabled.Checked;
                            LogManager.WriteLog(ScreenName + "Machine Class  updated successfully MachineClassID:" + MachineClass_ID, LogManager.enumLogLevel.Info);
                        }
                        else
                        {
                            LogManager.WriteLog(ScreenName + "Unable to update machine class details", LogManager.enumLogLevel.Error);
                            Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_PURCHASEDETAILS_ADD"), this.Text);            // "Unable to Add Purchase Machine details");

                            return;
                        }

                        if (MCType_ID.HasValue && MCType_ID.Value > 0)
                        {
                            BuyMCClassAndMachineID = MCType_ID.Value;
                        }
                        #endregion
                        string MacAddress = null;
                        string MacAddressPrev = null;
                        if (txtMACAddress.Enabled)
                        {
                            MacAddress = txtMACAddress.Text;
                            if (txtMACAddress.Text.Trim() != strMacAddr)
                            {
                                MacAddressPrev = strMacAddr;
                            }
                        }
                        if (MacAddress != null && MacAddress.Replace("-", "").Trim().Equals(string.Empty))
                        {
                            MacAddress = "";
                        }
                        if (MacAddressPrev != null && MacAddressPrev.Replace("-", "").Trim().Equals(string.Empty))
                        {
                            MacAddressPrev = "";
                        }
                        int MC_NewInstall = 1;
                        int Depreciation_Policy_ID = 0;
                        if (cmbDepreciation.SelectedIndex >= 0)
                        {
                            Depreciation_Policy_ID = (int)cmbDepreciation.SelectedValue;
                        }
                        int Staffid = (int)cmbRep.SelectedValue;
                        Staffid = (Staffid == -1) ? 0 : Staffid;

                        char? GamePrefix = (txtGamePrefix.Text.Trim().Length >= 1) ? txtGamePrefix.Text.Trim()[0] : '\0';
                        // Add/Update Machine Details
                        int? DepotID = (int)cmbDepot.SelectedValue;
                        int? MCModelTypeID = (int)cmbModelType.SelectedValue;
                        int? MCStatusFlag = (int)cmbStatus.SelectedValue;
                        int? OperatorID = (int)cmbOperators.SelectedValue;

                        BuyMachineBiz bMachine = BuyMachineBiz.CreateInstance();

                        int? StackerID = cmbStackerList.SelectedValue != null ? (int)cmbStackerList.SelectedValue : 0;
                        #region Add/Update Machine_Class
                        if (BuyMachineBiz.CreateInstance().AddMachineDetails(AutoMachineID, txt_ActualStockNo.Text.Trim(), txtSerialNumber.Text.Trim(), GamePrefix, txtGameType.Text.Trim(), DepotID,
                            Depreciation_Policy_ID, chkDepreciationUseDefault.Checked, 0, txt_GMUNo.Text.Trim(), chkAFTEnabled.Checked,
                            chkMultiGame.Checked, chkNonCashable.Checked ? 1 : 0, chkTITO.Checked ? 1 : 0, txtAltStockNumbers.Text.Trim(),
                            BuyMCClassAndMachineID, MachineClass_ID, Common.Utilities.Common.GetUniversalDate(DateTime.Now), Common.Utilities.Common.GetUniversalDate(DTDepreciationStartDate.Value),
                            string.Empty, MacAddress, MacAddressPrev, txtSerialNumber.Text.Trim(),
                            rtfNotes.Text, MCModelTypeID, MC_NewInstall, Convert.ToDecimal("0" + txtOriginalPurchasePrice.Text.Trim()),
                            txtPurchaseInvoiceNumber.Text.Trim(), txtMachinePurchasedFrom.Text.Trim(),
                            Common.Utilities.Common.GetUniversalDate(DTDepreciationStartDate.Value), "Usable Stock", MCStatusFlag, txtStockNumber.Text, chkDefaultAssetDetail.Checked, iBaseDenom, dPercentage_Payout, OperatorID,
                            StackerID, Staffid, AppEntryPoint.Current.UserId, 0, chkGetGameDetails.Checked,chkGameCapping.Checked, txtDisplayName.Text.Trim(),
                           Convert.ToInt32("0" + txtOccupanyPerhour.Text)))
                        {
                            LogManager.WriteLog(ScreenName + "Machine details updated successfully MachineID:" + AutoMachineID, LogManager.enumLogLevel.Info);
                            if (IsCustomMultiGameName)
                            {
                                try
                                {
                                    BuyMachineBiz.CreateInstance().AddMultiGameNameForAsset(AutoMachineID, cmbCategory.Text, chkMultiGame.Checked);
                                }
                                catch (Exception ex)
                                {
                                    ExceptionManager.Publish(ex);
                                }
                            }
                            if ((txtAltStockNumbers.Text.Trim() != strAltSerial) && strAltSerial.Trim().Length > 0 || chkTITO.Visible)
                            {
                                // Code to export machine specific details to site - Alt Serial No.
                                if (BuyMachineBiz.CreateInstance().InsertMachineUpdateEHRecord(AutoMachineID, "ALL"))
                                {
                                    LogManager.WriteLog(ScreenName + "Machine EHRecord updated successfully", LogManager.enumLogLevel.Info);

                                    List<GetMachineDetailsFromAssetResult> lst_MCDetails = bMachine.GetMachineDetailsFromAsset(sAssetNo);

                                    if (lst_MCDetails != null && lst_MCDetails.Count > 0)
                                    {
                                        if (PreviousMachineAFTState != CurrentMachineAFTState && lst_MCDetails[0].Site_ID > 0)
                                        {
                                            if (IsAFTEnabledForSite())
                                            {
                                                EmployeeCardBiz.CreateInstance().InsertExportHistory(lst_MCDetails[0].Installation_ID.ToString(), AppGlobals.Current.UserId, "AFTENABLEDISABLE", lst_MCDetails[0].Site_Code);
                                                LogManager.WriteLog(ScreenName + "AFTENABLEDISABLE Exported successfully", LogManager.enumLogLevel.Info);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    LogManager.WriteLog(ScreenName + "Unable to update machine EH record", LogManager.enumLogLevel.Error);
                                }
                            }

                            int? InstallationID = 0;
                            string Site_Code = "";
                            BuyMachineBiz.CreateInstance().GetActiveInstallationFromMachineID(AutoMachineID, ref InstallationID, ref Site_Code);
                            if (InstallationID > 0)
                            {
                                EmployeeCardBiz.CreateInstance().InsertExportHistory(AutoMachineID.Value.ToString(), AppGlobals.Current.UserId, "CMPGameType", Site_Code);
                                LogManager.WriteLog(ScreenName + "CMPGameType Exported successfully", LogManager.enumLogLevel.Info);
                            }
                            CloneEntity();
                            AuditChanges(oAudit, oNewAudit, AppGlobals.Current.UserId, AppGlobals.Current.UserName, txtStockNumber.Text);

                            if (chkKeepWindowActive.Checked && (BuyMCClassAndMachineID > 0))
                            {

                                int OldCategory = (int)cmbCategory.SelectedValue;

                                if (IsNGA)
                                {
                                    ShowMe(BuyMCClassAndMachineID, true);
                                }
                                else
                                {
                                    ShowMe(BuyMCClassAndMachineID, false);
                                }
                                if (cmbCategory.DataSource is List<GetMachineTypeDetailsResult>)
                                {
                                    List<GetMachineTypeDetailsResult> lst_MCType = (List<GetMachineTypeDetailsResult>)cmbCategory.DataSource;
                                    int ind = lst_MCType.FindIndex(obj => obj.Machine_Type_ID == OldCategory);
                                    cmbStatus.SelectedIndex = (ind >= 0) ? ind : 0;
                                }

                            }
                            else
                            {
                                objDatawatcher.DataModify = false;
                                this.Close();
                            }

                        }
                        else
                        {
                            objDatawatcher.DataModify = false;
                            Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_MACHINEDETAILS_ADD"), this.Text);            // "Unable to add machine details");
                            LogManager.WriteLog(ScreenName + "Unable to update machine details", LogManager.enumLogLevel.Error);
                        }
                        #endregion
                    }
                }
                else
                {

                    _BuyMachineDataChange = true;
                    Win32Extensions.ShowInfoMessageBox(this, ErrorMsg, this.Text);
                }
                if (!_BuyMachineDataChange)
                {
                    objDatawatcher.DataModify = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_MACHINEDETAILS_ADD"), this.Text);            // "Unable to add machine details");

            }
        }
        #endregion

        private void cmdManufacturer_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbManufacturer.SelectedIndex != -1 && cmbCategory.SelectedIndex != -1)
                {
                    if (cmbCategory.SelectedItem is GetGameTitleResult)
                    {
                        GetGameTitleResult g_res = (GetGameTitleResult)cmbCategory.SelectedItem;
                        Manufacturer m_det = (Manufacturer)cmbManufacturer.SelectedItem;
                        GetMachineTypeDetailsResult MCType = (GetMachineTypeDetailsResult)cmbMachineType.SelectedItem;
                        CheckManufactureGameCombination(g_res.Game_Title, m_det.Manufacturer_ID,MCType.Machine_Type_ID);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbManufacturer.SelectedIndex != -1 && cmbCategory.SelectedIndex != -1)
                {
                    Manufacturer m_det = (Manufacturer)cmbManufacturer.SelectedItem;
                    if (cmbCategory.SelectedItem is GetGameTitleResult)
                    {
                        GetGameTitleResult g_res = (GetGameTitleResult)cmbCategory.SelectedItem;
                        GetMachineTypeDetailsResult MCType = (GetMachineTypeDetailsResult)cmbMachineType.SelectedItem;
                        CheckManufactureGameCombination(g_res.Game_Title, m_det.Manufacturer_ID,MCType.Machine_Type_ID);
                    }
                    else if (cmbCategory.SelectedItem is GetMachineTypeDetailsResult)
                    {
                        GetMachineTypeDetailsResult MCType = (GetMachineTypeDetailsResult)cmbCategory.SelectedItem;
                     //   CheckManufactureGameCombination(MCType.Machine_Type_Code, m_det.Manufacturer_ID);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// This event is trigger to close BuyForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listOperators_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbOperators.SelectedItem != null && (LstOperator_ind != cmbOperators.SelectedIndex))
                {
                    OperatorEntity opRes = (OperatorEntity)cmbOperators.SelectedItem;
                    if (opRes.Operator_ID != -1)
                    {
                        List<DepotEntity> lstDepot = UserAdministrationBiz.CreateInstance().GetDepotDetails(opRes.Operator_ID);
                        //DepotEntity dep = lstDepot.Find(de => de.Depot_ID == -1);
                        //if (dep != null)
                        //{
                        //    dep.Depot_Name = strAny;
                        //    //lstDepot.Remove(dep);
                        //}
                        cmbDepot.DataSource = lstDepot;
                        cmbDepot.DisplayMember = "Depot_Name";
                        cmbDepot.ValueMember = "Depot_ID";
                        LstOperator_ind = cmbOperators.SelectedIndex;
                    }
                    else
                    {

                        cmbDepot.DataSource = null;
                        LstOperator_ind = cmbOperators.SelectedIndex;
                        cmbRep.SelectedIndex = -1;

                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LstDepot_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbDepot.SelectedIndex != -1)
                {
                    DepotEntity de_ent = (DepotEntity)cmbDepot.SelectedItem;
                    List<GetStaffByDepotResult> lst_StaffDepot = BuyMachineBiz.CreateInstance().GetStaffByDepot(de_ent.Depot_ID);
                    lst_StaffDepot.Insert(0, new GetStaffByDepotResult { Staff_ID = -1, FullName = this.GetResourceTextByKey("Key_NoneHyphen") });   // "--NONE--" Key_NoneHyphen
                    if (lst_StaffDepot.Count > 0)
                    {
                        cmbRep.DataSource = lst_StaffDepot;
                        cmbRep.DisplayMember = "FullName";
                        cmbRep.ValueMember = "Staff_ID";
                    }
                    cmbRep.Enabled = true;
                }
                else
                {
                    cmbRep.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// To allow only numeric character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_ActualStockNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void frmBuy_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormClose(true);
            LogManager.WriteLog(ScreenName + "frmBuy_FormClosing: Closed:" + AutoMachineID, LogManager.enumLogLevel.Info);
            if (act_Refresh != null && DelMachineIDandRefreshFlag)
            {
                act_Refresh();
            }

        }

        private void chkDepreciationUseDefault_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                cmbDepreciation.Enabled = !chkDepreciationUseDefault.Checked;
                if (!cmbDepreciation.Enabled)
                {

                    List<DepreciationEntity> lst_Deprec = (List<DepreciationEntity>)cmbDepreciation.DataSource;
                    if (lst_Deprec.Count > 0)
                    {
                        cmbDepreciation.SelectedIndex = lst_Deprec.FindIndex(se => se.Depreciation_Policy_ID == ModelDepreciationID);

                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtMACAddress_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            if (!e.IsValidInput && IsNGA)
            {
                tooltipMAC.ToolTipTitle = this.GetResourceTextByKey(1, "MSG_MACADDRESS_INVALID");        //  "Invalid MAC Address";
                tooltipMAC.Show(this.GetResourceTextByKey(1, "MSG_MACADDRESS_FORMAT"), txtMACAddress, 0, -20, 5000);           // "The MAC Address should be in specified format:  00-13-72-28-96-1F."
                // e.Cancel = true;
            }
        }
        public static bool IsValidMACAddress(string macAddress)
        {
            string sResult = "";
            Regex rgx = new Regex("([0-9a-fA-F][0-9a-fA-F]-){5}([0-9a-fA-F][0-9a-fA-F])", RegexOptions.IgnoreCase);
            Match mtc = rgx.Match(macAddress);
            sResult = mtc.Groups[0].Value;
            bool bResult = (string.IsNullOrEmpty(sResult)) ? false : true;
            if (sResult.Length == 12)
            {
                return bResult;
            }
            return bResult;
        }

        /// <summary>
        /// Sell Machine
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSellMachine_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog(ScreenName + "btnSellMachine_Click", LogManager.enumLogLevel.Info);
                SellMachineForm frm_sell = new SellMachineForm(AutoMachineID.Value, act_removeMC);                
                if (DialogResult.OK == frm_sell.ShowDialog())
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1,"MSG_BUY_SELLMACHINE"));
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void chkMultiGame_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog(ScreenName + "chkMultiGame_CheckedChanged", LogManager.enumLogLevel.Info);
                if (chkMultiGame.Checked)
                    LoadGameTitle(true);
                else
                    LoadGameTitle(false);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// To Load Template Name 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbAssetTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //int machineId = 0;
                if (isLoading || cmbAssetTemplate.Text.ToUpper() == this.GetResourceTextByKey("Key_None").ToUpper())            //"[NONE]")
                {
                    LoadDefault();
                    btn_Template.Enabled = false;
                    return;
                }
                btn_Template.Enabled = true; 
                isFromTemplate = true;
                //string TemplateName = cmbAssetTemplate.Text;
                //GetMachineId(TemplateName, ref machineId);
                ShowMeEdit(0, cmbAssetTemplate.Text, false, false);
                AutoMachineID = _machineIDActual;
                objDatawatcher.DataModify = false;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void chkDefaultAssetDetail_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDefaultAssetDetail.Checked)
            {

                cmbBaseDenom.Enabled = true;
                txtPercentagePayOut.Enabled = true;
                //grpAssetDetails.Enabled = true;
            }
            else
            {
                cmbBaseDenom.Enabled = false;
                txtPercentagePayOut.Enabled = false;
                //grpAssetDetails.Enabled = false;
            }
        }

        private void txtPercentagePayOut_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
                return;
            }



        }
        private void txt_ActualStockNo_Leave(object sender, EventArgs e)
        {
            try
            {
                TextBox txt_ags = sender as TextBox;
                txt_ags.Text = Convert.ToInt64("0" + txt_ags.Text).ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion
        public void CloneEntity()
        {
            try
            {
                oNewAudit = new BuyAuditEntity(); ;
                oNewAudit.Manufacturer_Name = cmbManufacturer.Text;                
                oNewAudit.Operator_Name = cmbOperators.Text;
                oNewAudit.Depot_Name = cmbDepot.Text;
                oNewAudit.Rep_Name = cmbRep.Text;
                oNewAudit.Status = (cmbStatus.SelectedItem as clsStockStatus).Caption;
                oNewAudit.Machine_Purchased_From = txtMachinePurchasedFrom.Text;
                oNewAudit.Stock_No = txtStockNumber.Text;
                oNewAudit.Machine_MAC_Address = txtMACAddress.Text.Replace('-', ' ').Trim() == "" ? "" : txtMACAddress.Text;
                oNewAudit.Model_Type = cmbModelType.Text;
                if (!IsNGA)
                {                   
                    oNewAudit.Stacker_Name = cmbStackerList.Text;                   
                    oNewAudit.Game_Title = cmbCategory.Text;
                    oNewAudit.IsTITOEnabled = chkTITO.Checked;
                    oNewAudit.AFTEnable = chkAFTEnabled.Checked;                    
                    oNewAudit.GetGameDetails = chkGetGameDetails.Checked;
                    oNewAudit.IsNonCashVoucherEnabled = chkNonCashable.Checked;
                    oNewAudit.GamePrefix = (string.IsNullOrEmpty(txtGamePrefix.Text)) ? "" : txtGamePrefix.Text;
                    oNewAudit.GameTypeCode = (string.IsNullOrEmpty(txtGameType.Text)) ? "" : txtGameType.Text;
                    oNewAudit.Occupancy_Games_Per_Hour = (txtOccupanyPerhour.Text == "") ? 0 : Convert.ToInt32(txtOccupanyPerhour.Text);                   
                    oNewAudit.IsGameCappingEnabled = chkGameCapping.Checked;
                    oNewAudit.AssetDisplayName = (string.IsNullOrEmpty(txtDisplayName.Text)) ? "" : txtDisplayName.Text; ;                         
                }
                oNewAudit.Base_Denom = (string.IsNullOrEmpty(cmbBaseDenom.Text)) ? 0 : Convert.ToInt32(cmbBaseDenom.Text);
                oNewAudit.ActStockNo = txt_ActualStockNo.Text;       
                oNewAudit.IsDefaultAssetDetail = chkDefaultAssetDetail.Checked;
                oNewAudit.Validation_Length = (txtValidationLength.Text == "") ? 0 : Convert.ToInt32(txtValidationLength.Text);
                oNewAudit.Percentage_Payout = (txtPercentagePayOut.Text == "") ? 0 : Convert.ToInt32(txtPercentagePayOut.Text);
                oNewAudit.GMUNo = txt_GMUNo.Text;
                oNewAudit.ActSerialNo = txtSerialNumber.Text;  
                oNewAudit.Machine_Alternative_Serial_Numbers = txtAltStockNumbers.Text;                
                oNewAudit.Original_Price = txtOriginalPurchasePrice.Text;                       
                oNewAudit.Asset_Template = cmbAssetTemplate.Text;
                oNewAudit.Machine_Memo = rtfNotes.Text;
                oNewAudit.DeliveryDate = DTDelivery.Value.Date;
                oNewAudit.DepreciationStartDate = DTDepreciationStartDate.Value.Date;
                oNewAudit.NBV = txtNBV.Text;
                oNewAudit.WeeklyDepreciation = txtWeeklyDepreciation.Text;               
                oNewAudit.Machine_Name = txtAssetTitle.Text;
                oNewAudit.Invoice_Number = txtPurchaseInvoiceNumber.Text;               
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);  
            }            
        }


        public bool AuditChanges(BuyAuditEntity oldEntity, BuyAuditEntity NewEntity, int userID, string userName, string Stock_Name)
        {
            return AuditChanges(oldEntity, NewEntity, userID, userName, Stock_Name, false);
        }

        public bool AuditChanges(BuyAuditEntity oldEntity, BuyAuditEntity NewEntity, int userID, string userName, string Stock_Name, bool isTemplate)
        {
            try
            {
                string addOrModify = (_isNew) ? "Added" : "Modified";
                string AddOrModify = (_isNew) ? "ADD" : "MODIFY";
                string type = "Asset";
                if (isTemplate)
                {
                    addOrModify = "Modified";
                    AddOrModify = "MODIFY";
                    type = "Template";
                }

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    string sValue = string.Empty; string[] strValue; string sFieldValue = string.Empty; int iCount = 0;
                    StringBuilder oString = new StringBuilder();
                    foreach (PropertyInfo prop in NewEntity.GetType().GetProperties())
                    {
                        if (!Convert.ToString(prop.GetValue(NewEntity, null)).Equals(Convert.ToString(prop.GetValue(oldEntity, null))))
                        {
                            sValue = Convert.ToString(prop.GetValue(oldEntity, null)) + "~" + Convert.ToString(prop.GetValue(NewEntity, null));
                            oString.Append(prop.Name).Append(',');
                            iCount = oString.ToString().Split(',').Count();
                        }
                    }
                    if (iCount > 2 && addOrModify == "Modified")
                    {
                        DataContext.InsertAuditData(userID, userName, isTemplate ? (int)ModuleNameEnterprise.AssetTemplate : (int)ModuleNameEnterprise.PURCHASEMACHINE, isTemplate ? ModuleNameEnterprise.AssetTemplate.ToString() : ModuleNameEnterprise.PURCHASEMACHINE.ToString(), isTemplate ? "Edit Template" : "Buy Machine", "",
                           oString.ToString(), "", "", string.Format("{0} [ {1}  Modified.. {2}", type, Stock_Name, oString.ToString()), AddOrModify);

                    }
                    else if (iCount > 0 && addOrModify == "Added")
                    {
                        DataContext.InsertAuditData(userID, userName, isTemplate ? (int)ModuleNameEnterprise.AssetTemplate : (int)ModuleNameEnterprise.PURCHASEMACHINE, isTemplate ? ModuleNameEnterprise.AssetTemplate.ToString() : ModuleNameEnterprise.PURCHASEMACHINE.ToString(), isTemplate ? "Edit Template" : "Buy Machine", "", "",
                          "", "", "Asset [ " + Stock_Name + " ] Added ..", AddOrModify);

                    }
                    else if (iCount != 0)
                    {
                        strValue = sValue.Split('~');
                        string sOldValue = strValue[0].ToString();
                        string sNewValue = strValue[1].ToString();

                        DataContext.InsertAuditData(userID, userName, isTemplate ? (int)ModuleNameEnterprise.AssetTemplate : (int)ModuleNameEnterprise.PURCHASEMACHINE, isTemplate ? ModuleNameEnterprise.AssetTemplate.ToString() : ModuleNameEnterprise.PURCHASEMACHINE.ToString(), isTemplate ? "Edit Template" : "Buy Machine", "", oString.ToString(),
                          sOldValue, sNewValue, string.Format("{4} [{0}] Modified ..[{1}]:{2}-->{3}", Stock_Name, oString.ToString(), sOldValue, sNewValue, type), AddOrModify);
                    }
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            return true;
        }

        private void btn_Template_Click(object sender, EventArgs e)
        {
            string ErrorMsg = "";
            DelMachineIDandRefreshFlag = true;
            if (_isNew)
            {
                oAudit.DeliveryDate = DateTime.Now.Date;
                oAudit.DepreciationStartDate = DateTime.Now.Date;
            }
            if (!IsNGA)
            {
                iBaseDenom = ((ComboBoxItem<int>)cmbBaseDenom.SelectedItem).Value;
                dPercentage_Payout = Convert.ToSingle("0" + txtPercentagePayOut.Text);
            }
            string sAssetNo = string.Empty;

            if (Validate(ref ErrorMsg, false))
            {
                LogManager.WriteLog(ScreenName + " btnOk_Click:", LogManager.enumLogLevel.Info);

                if (txtSerialNumber.Text.Trim().Equals(string.Empty))
                {
                    txtSerialNumber.Text = "0";
                }
                if (txt_ActualStockNo.Text.Trim().Equals(string.Empty))
                {
                    txt_ActualStockNo.Text = "0";
                }
                if (txt_GMUNo.Text.Trim().Equals(string.Empty))
                {
                    txt_GMUNo.Text = "0";
                }
                sAssetNo = txtStockNumber.Text;
                if (txtSerialNumber.Text != "")
                {
                    int ManufacturerID = cmbManufacturer.SelectedIndex >= 0 ? ((int)cmbManufacturer.SelectedValue) : 0;
                    int? ValidationLength = (txtValidationLength.Text == "") ? (int?)null : Convert.ToInt32(txtValidationLength.Text);
                    int? MachineClass_ID = 0;
                    int? MCType_ID = BuyMCClassAndMachineID;
                    if (IsNGA && !_IsEdit)
                    {
                        MachineClass_ID = BuyMCClassAndMachineID;
                    }

                    if (BuyMachineBiz.CreateInstance().UpdateTemplateMachineClass(cmbAssetTemplate.Text, cmbCategory.Text, _IsEdit, IsNGA, ManufacturerID, ref MCType_ID,
                                 BuyMCClassAndMachineID, SF_VALUE_SASPROTOCOL, cmbCategory.Text,
                                  0, true, Convert.ToInt32("0" + txtOccupanyPerhour.Text),
                                  0, 0,
                                  0, 0,
                                  "0", "0",
                                  false, false,
                                  99999999, false, ValidationLength, ref MachineClass_ID))
                    {

                        CurrentMachineAFTState = chkAFTEnabled.Checked;
                        LogManager.WriteLog(ScreenName + "Template Machine Class  updated successfully MachineClassID:" + MachineClass_ID, LogManager.enumLogLevel.Info);
                    }
                    else
                    {
                        LogManager.WriteLog(ScreenName + "Unable to update Template machine class details", LogManager.enumLogLevel.Error);
                        Win32Extensions.ShowInfoMessageBox(this,this.GetResourceTextByKey(1, "MSG_TEMPLATEDETAILS_UPDATE"), this.Text);            // "Unable to update Template Machine details");

                        return;
                    }

                    if (MCType_ID.HasValue && MCType_ID.Value > 0)
                    {
                        BuyMCClassAndMachineID = MCType_ID.Value;
                    }
                    string MacAddress = null;
                    string MacAddressPrev = null;
                    if (txtMACAddress.Enabled)
                    {
                        MacAddress = txtMACAddress.Text;
                        if (txtMACAddress.Text.Trim() != strMacAddr)
                        {
                            MacAddressPrev = strMacAddr;
                        }
                    }
                    if (MacAddress != null && MacAddress.Replace("-", "").Trim().Equals(string.Empty))
                    {
                        MacAddress = "";
                    }
                    if (MacAddressPrev != null && MacAddressPrev.Replace("-", "").Trim().Equals(string.Empty))
                    {
                        MacAddressPrev = "";
                    }
                    int MC_NewInstall = 1;
                    int Depreciation_Policy_ID = 0;
                    if (cmbDepreciation.SelectedIndex >= 0)
                    {
                        Depreciation_Policy_ID = (int)cmbDepreciation.SelectedValue;
                    }
                    int Staffid = (int)cmbRep.SelectedValue;
                    Staffid = (Staffid == -1) ? 0 : Staffid;

                    char? GamePrefix = (txtGamePrefix.Text.Trim().Length >= 1) ? txtGamePrefix.Text.Trim()[0] : '\0';
                    // Add/Update Machine Details
                    int? DepotID = (int)cmbDepot.SelectedValue;
                    int? MCModelTypeID = (int)cmbModelType.SelectedValue;
                    int? MCStatusFlag = (int)cmbStatus.SelectedValue;
                    int? OperatorID = (int)cmbOperators.SelectedValue;

                    BuyMachineBiz bMachine = BuyMachineBiz.CreateInstance();

                    int? StackerID = cmbStackerList.SelectedValue != null ? (int)cmbStackerList.SelectedValue : 0;
                    if (BuyMachineBiz.CreateInstance().UpdateTemplateDetails(cmbAssetTemplate.Text, txt_ActualStockNo.Text, txtSerialNumber.Text, GamePrefix, txtGameType.Text, DepotID,
                        Depreciation_Policy_ID, chkDepreciationUseDefault.Checked, 0, txt_GMUNo.Text, chkAFTEnabled.Checked,
                        chkMultiGame.Checked, chkNonCashable.Checked ? 1 : 0, chkTITO.Checked ? 1 : 0, txtAltStockNumbers.Text,
                        BuyMCClassAndMachineID, MachineClass_ID, Common.Utilities.Common.GetUniversalDate(DateTime.Now), Common.Utilities.Common.GetUniversalDate(DTDepreciationStartDate.Value),
                        string.Empty, MacAddress, MacAddressPrev, txtSerialNumber.Text,
                        rtfNotes.Text, MCModelTypeID, MC_NewInstall, Convert.ToDecimal("0" + txtOriginalPurchasePrice.Text),
                        txtPurchaseInvoiceNumber.Text, txtMachinePurchasedFrom.Text,
                        Common.Utilities.Common.GetUniversalDate(DTDepreciationStartDate.Value), "Usable Stock", MCStatusFlag, txtStockNumber.Text, chkDefaultAssetDetail.Checked, iBaseDenom, dPercentage_Payout, OperatorID,
                        StackerID, Staffid, AppEntryPoint.Current.UserId, 0, chkGetGameDetails.Checked, chkGameCapping.Checked, txtDisplayName.Text.Trim()))
                    {
                        LogManager.WriteLog(ScreenName + "Machine details updated successfully MachineID:" + AutoMachineID, LogManager.enumLogLevel.Info);
                        if (IsCustomMultiGameName)
                        {
                            try
                            {
                                BuyMachineBiz.CreateInstance().AddMultiGameNameForAsset(AutoMachineID, txt_MultiGame.Text, chkMultiGame.Checked);
                            }
                            catch (Exception ex)
                            {
                                ExceptionManager.Publish(ex);
                            }
                        }
                        if ((txtAltStockNumbers.Text.Trim() != strAltSerial) && strAltSerial.Trim().Length > 0 || chkTITO.Visible)
                        {
                            // Code to export machine specific details to site - Alt Serial No.
                            if (BuyMachineBiz.CreateInstance().InsertMachineUpdateEHRecord(AutoMachineID, "ALL"))
                            {
                                LogManager.WriteLog(ScreenName + "Machine EHRecord updated successfully", LogManager.enumLogLevel.Info);

                                List<GetMachineDetailsFromAssetResult> lst_MCDetails = bMachine.GetMachineDetailsFromAsset(sAssetNo);

                                if (lst_MCDetails != null && lst_MCDetails.Count > 0)
                                {
                                    if (PreviousMachineAFTState != CurrentMachineAFTState && lst_MCDetails[0].Site_ID > 0)
                                    {
                                        if (IsAFTEnabledForSite())
                                        {
                                            EmployeeCardBiz.CreateInstance().InsertExportHistory(lst_MCDetails[0].Installation_ID.ToString(), AppGlobals.Current.UserId, "AFTENABLEDISABLE", lst_MCDetails[0].Site_Code);
                                            LogManager.WriteLog(ScreenName + "AFTENABLEDISABLE Exported successfully", LogManager.enumLogLevel.Info);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                LogManager.WriteLog(ScreenName + "Unable to update machine EH record", LogManager.enumLogLevel.Error);
                            }
                        }

                        int? InstallationID = 0;
                        string Site_Code = "";
                        BuyMachineBiz.CreateInstance().GetActiveInstallationFromMachineID(AutoMachineID, ref InstallationID, ref Site_Code);
                        if (InstallationID > 0)
                        {
                            EmployeeCardBiz.CreateInstance().InsertExportHistory(AutoMachineID.Value.ToString(), AppGlobals.Current.UserId, "CMPGameType", Site_Code);
                            LogManager.WriteLog(ScreenName + "CMPGameType Exported successfully", LogManager.enumLogLevel.Info);
                        }
                        CloneEntity();
                        oNewAudit.Asset_Template = oAudit.Asset_Template;
                        AuditChanges(oAudit, oNewAudit, AppGlobals.Current.UserId, AppGlobals.Current.UserName, cmbAssetTemplate.Text, true);

                        if (chkKeepWindowActive.Checked && (BuyMCClassAndMachineID > 0))
                        {

                            int OldCategory = (int)cmbCategory.SelectedValue;

                            if (IsNGA)
                            {
                                ShowMe(BuyMCClassAndMachineID, true);
                            }
                            else
                            {
                                ShowMe(BuyMCClassAndMachineID, false);
                            }
                            if (cmbCategory.DataSource is List<GetMachineTypeDetailsResult>)
                            {
                                List<GetMachineTypeDetailsResult> lst_MCType = (List<GetMachineTypeDetailsResult>)cmbCategory.DataSource;
                                int ind = lst_MCType.FindIndex(obj => obj.Machine_Type_ID == OldCategory);
                                cmbStatus.SelectedIndex = (ind >= 0) ? ind : 0;
                            }
                        }
                        else
                        {
                            this.Close();
                        }

                    }
                }
            }
            else
            {
                Win32Extensions.ShowInfoMessageBox(this, ErrorMsg, this.Text);
            }
        }

        private void chkAFTEnabled_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtNBV_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblWeeklyDep_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}


