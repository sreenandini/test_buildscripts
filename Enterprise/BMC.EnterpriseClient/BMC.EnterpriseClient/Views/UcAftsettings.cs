using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.CoreLib.Win32;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseDataAccess;
using BMC.EnterpriseBusiness.Business;
using Audit.Transport;
using System.Collections;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class UcAftsettings : UserControl, IAdminSite
    {
        int siteID = 0;
        List<AftsettingsEntity> entity = null;
        List<AftsettingsEntity> objEntity = null;
        public string _SiteName;
        ArrayList denomitems = new ArrayList();
      
        public UcAftsettings()
        {
            InitializeComponent();
            SetTagProperty();
            this.ResolveResources();
            this.Dock = DockStyle.Fill;
        }
        
        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.lblBaseDenom.Tag = "Key_BaseDenomMandatoryColon";
            this.lblEFTtimeoutvalue.Tag = "Key_EFTTimeOutValueMandatory";
            this.lblMaxwithdrawalamount.Tag = "Key_MaxWithdrawalAmountMandatory";
            this.lblMaxdepositamount.Tag = "Key_MaxDepositAmountMandatory";
            this.lblOption1withdrawlamount.Tag = "Key_Option1WithdrawalAmountMandatory";
            this.lblOption2withdrawlamount.Tag = "Key_Option2WithdrawalAmountMandatory";
            this.lbloption3withdrawalamount.Tag = "Key_Option3WithdrawalAmountMandatory";
            this.lbloption4withdrawalamount.Tag = "Key_Option4WithdrawalAmountMandatory";
            this.lbloption5withdrawalamount.Tag = "Key_Option5WithdrawalAmountMandatory";
            this.chkAllowAFTTransactions.Tag = "Key_AFTTransactionsAllowed";
            this.chkAllowCashWithdrawal.Tag = "Key_AllowCashWithdrawal";
            this.chkCashableDep.Tag = "Key_AllowCashableDeposits";
            this.chkAllowNoncashableDep.Tag = "Key_AllowNonCashableDeposits";
            this.chkAllowOffers.Tag = "Key_AllowOffers";
            this.chkAllowPartialTransfer.Tag = "Key_AllowPartialTransfers";
            this.chkAllowPointsWthdrawal.Tag = "Key_AllowPointsWithdrawal";
            this.chkAllowRedeemOffers.Tag = "Key_AllowRedeemOffers";
            this.chkAutoDepositCashable.Tag = "Key_AutoDepositCashableCredits";
            this.chkAllowAutoDepositnon.Tag = "Key_AutoDepositNonCashableCredits";
            this.btndelete.Tag = "Key_DeleteCaption";
            this.lblLegend.Tag = "Key_NoteMax";
            this.btnupdate.Tag = "Key_UpdateCaption";
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Load values for aftsettings.
        /// </summary>
        /// <param name="entity"></param>

        public void LoadDetails(AdminSiteEntity entity)
        {
            if(entity!=null)
                _SiteName = entity.Site_Name;
            AftsettingsEntity entityload =new AftsettingsEntity();
            int denom = Convert.ToInt32(entityload.Denom);
              AdminSystemSettingsResult objAdminSystemSettingsResult =new AdminSystemSettingsResult();
              AdminSettings _AdminSettings =new AdminSettings();
            
            objAdminSystemSettingsResult = _AdminSettings.GetSystemSettingDetails();
            if (objAdminSystemSettingsResult != null)
            {
                if (objAdminSystemSettingsResult.SystemSettings.RegionCulture.ToUpper() == "EN-GB")
                {
                    lblLegend.Text =  this.GetResourceTextByKey("Key_MaxWithDrawalLegend") ;
                }
                else
                {
                    lblLegend.Text = this.GetResourceTextByKey("Key_NoteMax") ;
                }
            }
            try
            {
                LogManager.WriteLog("inside Aftsettings LoadDetails", LogManager.enumLogLevel.Info);
               
                siteID = entity.Site_ID;


                objEntity = AftsettingsBusiness.CreateInstance().Aftsettingdenome(siteID);

                if (siteID == 0)
                {
                    tblAftMainFram.Enabled = false;

                }
                else
                {
                    tblAftMainFram.Enabled = true;
                    cmbBaseDenom.DataSource = objEntity;
                    cmbBaseDenom.DisplayMember = "Denom";
                    cmbBaseDenom.SelectedIndex = -1;
                    txtEFTTimeout.Text = "";
                    txtMaxDepositAmt.Text = "";
                    txtMaxwithdrawAmt.Text = "";
                    txtOption1WithDrawAmt.Text = "";
                    txtOption2WithDrawAmt.Text = "";
                    txtOption3WithDrawAmt.Text = "";
                    txtOption4WithDrawAmt.Text = "";
                    txtOption5WithDrawAmt.Text = "";
                    chkAllowAFTTransactions.Checked = false;
                    chkAllowAutoDepositnon.Checked = false;
                    chkAllowCashWithdrawal.Checked = false;
                    chkAllowNoncashableDep.Checked = false;
                    chkAllowOffers.Checked = false;
                    chkAllowPartialTransfer.Checked = false;
                    chkAllowPointsWthdrawal.Checked = false;
                    chkAllowRedeemOffers.Checked = false;
                    chkAutoDepositCashable.Checked = false;
                    chkCashableDep.Checked = false;

                    foreach (var li in objEntity.Select(w => w.Denom).ToList())
                    {
                            denomitems.Add(li);
                    }
                                    
                }
                LogManager.WriteLog("End Aftsettings Load", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (siteID != 0 && !AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Site_Edit"))
                {
                    tblAftMainFram.Enabled = false;
                }
            }
        }

        private void FillDenom(bool bLoadDetails)
        {
            try
            {
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_AFT_SETTING_DENOM") + " " + cmbBaseDenom.Text + this.GetResourceTextByKey(1, "MSG_UPDATE_SUCCESSFUL"), this.ParentForm.Text);
                objEntity = AftsettingsBusiness.CreateInstance().Aftsettingdenome(siteID);
                cmbBaseDenom.DataSource = objEntity;
                cmbBaseDenom.DisplayMember = "Denom";
                if (!bLoadDetails) return;
              
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                    
            }
         }

        /// <summary>
        /// Fill value fro aft settings tab
        /// </summary>
        /// <param name="siteID"></param>
        /// <param name="denom"></param>
        public void LoadAftDetails(int siteID, int denom)
        {
            try
            {
                LogManager.WriteLog("inside Load  Aft settings values", LogManager.enumLogLevel.Info);
                entity = AftsettingsBusiness.CreateInstance().AftsettingDetails(siteID, denom);
                if (entity.Count < 1)
                {
                    btndelete.Enabled = false;
                }
                else
                {
                    btndelete.Enabled = true;
                }

                foreach (var Aftentity in entity)
                {
                    chkAllowAFTTransactions.Checked = (Aftentity.AFTTransactionsAllowed == true);
                    chkCashableDep.Checked = (Aftentity.AllowCashableDeposits == true);
                    chkAllowNoncashableDep.Checked = (Aftentity.AllowNonCashableDeposits == true);
                    chkAllowRedeemOffers.Checked = (Aftentity.AllowRedeemOffers == true);
                    chkAllowPointsWthdrawal.Checked = (Aftentity.AllowPointsWithdrawal == true);
                    chkAllowCashWithdrawal.Checked = (Aftentity.AllowCashWithdrawal == true);
                    chkAllowPartialTransfer.Checked = (Aftentity.AllowPartialTransfers == true);
                    chkAllowNoncashableDep.Checked = (Aftentity.AllowNonCashableDeposits == true);
                    chkAutoDepositCashable.Checked = (Aftentity.AutoDepositCashableCreditsonCardOut == true);
                    chkAllowAutoDepositnon.Checked = (Aftentity.AutoDepositNonCashableCreditsonCardOut == true);
                    chkAllowOffers.Checked = (Aftentity.AllowOffers == true);
                    txtEFTTimeout.Text = (Aftentity.EFTTimeoutValue).ToString();
                    txtOption1WithDrawAmt.Text = (Aftentity.Option1WithdrawalAmount).ToString();
                    txtOption2WithDrawAmt.Text = (Aftentity.Option2WithdrawalAmount).ToString();
                    txtOption3WithDrawAmt.Text = (Aftentity.Option3WithdrawalAmount).ToString();
                    txtOption4WithDrawAmt.Text = (Aftentity.Option4WithdrawalAmount).ToString();
                    txtOption5WithDrawAmt.Text = (Aftentity.Option5WithdrawalAmount).ToString();
                    txtMaxDepositAmt.Text = (Aftentity.MaxDepositAmount).ToString();
                    txtMaxwithdrawAmt.Text = (Aftentity.MaxWithDrawAmount).ToString();
                    LogManager.WriteLog("End of  Load Aft settings values", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }
        /// <summary>
        /// Update Aftsettings values,insert new values from aft settings tab in to db form ui
        /// </summary>
        public void UpdateAftDetails()
        {

            try
            {

                LogManager.WriteLog("inside UpdateAftDetails Aftsettings", LogManager.enumLogLevel.Info);

                if (siteID == 0)
                {
                  return;
                }
                int siteIDvalue = siteID;
                int denomevalue = Convert.ToInt32((cmbBaseDenom.Text));
               
                //List<ValueType> CurrValue = new List<ValueType>();
                //CurrValue.Add(chkAllowAFTTransactions.Checked);
                //CurrValue.Add(chkCashableDep.Checked);
                //CurrValue.Add(chkAllowNoncashableDep.Checked);
                //CurrValue.Add(chkAllowRedeemOffers.Checked);
                //CurrValue.Add(chkAllowPointsWthdrawal.Checked);
                //CurrValue.Add(chkAllowCashWithdrawal.Checked);
                //CurrValue.Add(chkAllowPartialTransfer.Checked);
                //CurrValue.Add(chkAllowAutoDepositnon.Checked);
                //CurrValue.Add(chkAutoDepositCashable.Checked);
                //CurrValue.Add(chkAllowOffers.Checked);
                //CurrValue.Add(Convert.ToInt32(txtEFTTimeout.Text));
                //CurrValue.Add(Convert.ToInt32(txtOption1WithDrawAmt.Text));
                //CurrValue.Add(Convert.ToInt32(txtOption2WithDrawAmt.Text));
                //CurrValue.Add(Convert.ToInt32(txtOption3WithDrawAmt.Text));
                //CurrValue.Add(Convert.ToInt32(txtOption4WithDrawAmt.Text));
                //CurrValue.Add(Convert.ToInt32(txtOption5WithDrawAmt.Text));
                //CurrValue.Add(Convert.ToInt32(txtMaxDepositAmt.Text));
                //CurrValue.Add(Convert.ToInt32(txtMaxwithdrawAmt.Text));
                //CurrValue.Add(siteID);
                //CurrValue.Add(denomevalue);
                AftsettingsEntity _originalentity = new AftsettingsEntity();
                entity = AftsettingsBusiness.CreateInstance().AftsettingDetails(siteID, denomevalue);
                foreach (var Aftentity in entity)
                { _originalentity = Aftentity; }

                AftsettingsBusiness.CreateInstance().
                                                        UpdateAftsettingDetails(chkAllowAFTTransactions.Checked,
                                                                                chkCashableDep.Checked,
                                                                                chkAllowNoncashableDep.Checked,
                                                                                chkAllowRedeemOffers.Checked,
                                                                                chkAllowPointsWthdrawal.Checked,
                                                                                chkAllowCashWithdrawal.Checked,
                                                                                chkAllowPartialTransfer.Checked,
                                                                                chkAllowAutoDepositnon.Checked,
                                                                                chkAutoDepositCashable.Checked,
                                                                                chkAllowOffers.Checked,
                                                                                Convert.ToInt32(txtEFTTimeout.Text),
                                                                                Convert.ToInt32(txtOption1WithDrawAmt.Text),
                                                                                Convert.ToInt32(txtOption2WithDrawAmt.Text),
                                                                                Convert.ToInt32(txtOption3WithDrawAmt.Text),
                                                                                Convert.ToInt32(txtOption4WithDrawAmt.Text),
                                                                                Convert.ToInt32(txtOption5WithDrawAmt.Text),
                                                                                Convert.ToInt32(txtMaxDepositAmt.Text),
                                                                                Convert.ToInt32(txtMaxwithdrawAmt.Text),
                                                                                siteIDvalue,
                                                                                denomevalue
                                                                                );

                if (!denomitems.Contains(denomevalue))
                {
                    denomitems.Add(denomevalue);
                    AftsettingsBusiness.CreateInstance().AuditNewEntry(ModuleNameEnterprise.AUDIT_SITE, "AFT Settings", "Base Denom", denomevalue.ToString(), AppGlobals.Current.UserId, AppGlobals.Current.UserName, _SiteName);
                }
                else
                {
                    AftsettingsEntity _modifiedenity = new AftsettingsEntity();
                    _modifiedenity.Denom = denomevalue;
                    _modifiedenity.AFTSetting_NO = _originalentity.AFTSetting_NO;
                    _modifiedenity.AFTTransactionsAllowed = chkAllowAFTTransactions.Checked;
                    _modifiedenity.AllowCashableDeposits = chkCashableDep.Checked;
                    _modifiedenity.AllowNonCashableDeposits = chkAllowNoncashableDep.Checked;
                    _modifiedenity.AllowRedeemOffers = chkAllowRedeemOffers.Checked;
                    _modifiedenity.AllowPointsWithdrawal = chkAllowPointsWthdrawal.Checked;
                    _modifiedenity.AllowCashWithdrawal = chkAllowCashWithdrawal.Checked;
                    _modifiedenity.AllowPartialTransfers=chkAllowPartialTransfer.Checked;
                    _modifiedenity.AutoDepositNonCashableCreditsonCardOut = chkAllowAutoDepositnon.Checked;
                    _modifiedenity.AutoDepositCashableCreditsonCardOut = chkAutoDepositCashable.Checked;
                    _modifiedenity.AllowOffers = chkAllowOffers.Checked;
                    _modifiedenity.Option1WithdrawalAmount = Convert.ToInt32(txtOption1WithDrawAmt.Text);
                    _modifiedenity.Option2WithdrawalAmount = Convert.ToInt32(txtOption2WithDrawAmt.Text);
                    _modifiedenity.Option3WithdrawalAmount = Convert.ToInt32(txtOption3WithDrawAmt.Text);
                    _modifiedenity.Option4WithdrawalAmount = Convert.ToInt32(txtOption4WithDrawAmt.Text);
                    _modifiedenity.Option5WithdrawalAmount = Convert.ToInt32(txtOption5WithDrawAmt.Text);
                    _modifiedenity.MaxDepositAmount = Convert.ToInt32(txtMaxDepositAmt.Text);
                    _modifiedenity.MaxWithDrawAmount = Convert.ToInt32(txtMaxwithdrawAmt.Text);
                    _modifiedenity.EFTTimeoutValue = Convert.ToInt32(txtEFTTimeout.Text);
                    AftsettingsBusiness.CreateInstance().AuditModifiedData(ModuleNameEnterprise.AUDIT_SITE, _modifiedenity, _originalentity, denomevalue, AppGlobals.Current.UserId, AppGlobals.Current.UserName, _SiteName);
                
                }
                LogManager.WriteLog("End UpdateAftDetails Aftsettings", LogManager.enumLogLevel.Info);
                FillDenom(true);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

       
      /// <summary>
      /// save details values only insert new values from ui to db
      /// </summary>
      /// <param name="entity"></param>
        public bool SaveDetails(AdminSiteEntity entity)
        {
           return true;
        }
        public bool SaveValues()
        {
            //try
            //{
            //    if (string.IsNullOrEmpty(txtOption1WithDrawAmt.Text) &&  string.IsNullOrEmpty(txtOption2WithDrawAmt.Text)&& string.IsNullOrEmpty(txtOption3WithDrawAmt.Text)&&
            //       string.IsNullOrEmpty(txtOption4WithDrawAmt.Text) && string.IsNullOrEmpty(txtOption5WithDrawAmt.Text)&& string.IsNullOrEmpty(Convert.ToString(cmbBaseDenom.SelectedValue)))
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        if (ValidateAFTSetting() == true)
            //        {
            //            this.Focus();
            //            return false;

            //        }
            //        AftsettingsEntity entity = new AftsettingsEntity();
            //        entity.Denom = Convert.ToInt32(cmbBaseDenom.SelectedValue);
            //        entity.SiteCode = siteID.ToString();
            //        entity.AFTTransactionsAllowed = chkAllowAFTTransactions.Checked;
            //        entity.AllowCashableDeposits = chkCashableDep.Checked;
            //        entity.AllowNonCashableDeposits = chkAllowNoncashableDep.Checked;
            //        entity.AllowRedeemOffers = chkAllowRedeemOffers.Checked;
            //        entity.AllowPointsWithdrawal = chkAllowPointsWthdrawal.Checked;
            //        entity.AllowCashWithdrawal = chkAllowCashWithdrawal.Checked;
            //        entity.AllowPartialTransfers = chkAllowPartialTransfer.Checked;
            //        entity.AutoDepositNonCashableCreditsonCardOut = chkAllowAutoDepositnon.Checked;
            //        entity.AutoDepositCashableCreditsonCardOut = chkAutoDepositCashable.Checked;
            //        entity.AllowOffers = chkAllowOffers.Checked;
            //        entity.EFTTimeoutValue = Convert.ToInt32(txtEFTTimeout.Text);
            //        entity.Option1WithdrawalAmount = Convert.ToInt32(txtOption1WithDrawAmt.Text);
            //        entity.Option2WithdrawalAmount = Convert.ToInt32(txtOption2WithDrawAmt.Text);
            //        entity.Option3WithdrawalAmount = Convert.ToInt32(txtOption3WithDrawAmt.Text);
            //        entity.Option4WithdrawalAmount = Convert.ToInt32(txtOption4WithDrawAmt.Text);
            //        entity.Option5WithdrawalAmount = Convert.ToInt32(txtOption5WithDrawAmt.Text);
            //        entity.MaxDepositAmount = Convert.ToInt32(txtMaxDepositAmt.Text);
            //        entity.MaxWithDrawAmount = Convert.ToInt32(txtMaxwithdrawAmt.Text);
            //    }
               
            //}
            //catch (Exception ex)
            //{
            //   ExceptionManager.Publish(ex);
            //   return false;
            //}
            return true;
        }

        /// <summary>
        /// cmbBaseDenom_SelectedIndexChanged event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void cmbBaseDenom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbBaseDenom.SelectedIndex < 0 || cmbBaseDenom.SelectedValue == null)
                {
                 return;
                }
                btnupdate.Visible = true;
                btndelete.Visible = true;
                LogManager.WriteLog("inside cmbBaseDenom_SelectedIndexChanged", LogManager.enumLogLevel.Info);
                if (cmbBaseDenom.SelectedValue == null)
                {
                    return;
                }
                int denome = Convert.ToInt32(((AftsettingsEntity)cmbBaseDenom.SelectedValue).Denom);
                LoadAftDetails(siteID, denome);
                LogManager.WriteLog("End cmbBaseDenom_SelectedIndexChanged", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }
        /// <summary>
        /// Delete basedenome value 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("inside btndelete_Click", LogManager.enumLogLevel.Info);
                if (cmbBaseDenom.SelectedIndex <= -1)
                {

                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SELECT_APPROPRIATE_BASE_DENOM"), this.ParentForm.Text);
                    return;
                }
                if (this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_DELETE_SELECTED_BASE_DENOM"), this.ParentForm.Text) == DialogResult.No)
                {
                    return;
                }
                else
                {
                   
                    string var = (((AftsettingsEntity)cmbBaseDenom.SelectedValue).Denom).ToString();
                    AftsettingsBusiness.CreateInstance().DeleteAftsettingDetails(siteID,var);
                    objEntity = AftsettingsBusiness.CreateInstance().Aftsettingdenome(siteID);
                    cmbBaseDenom.DataSource = objEntity;
                    cmbBaseDenom.SelectedIndex = -1;
                    txtEFTTimeout.Text = "";
                    txtMaxDepositAmt.Text = "";
                    txtMaxwithdrawAmt.Text = "";
                    txtOption1WithDrawAmt.Text = "";
                    txtOption2WithDrawAmt.Text = "";
                    txtOption3WithDrawAmt.Text = "";
                    txtOption4WithDrawAmt.Text = "";
                    txtOption5WithDrawAmt.Text = "";
                    chkAllowAFTTransactions.Checked = false;
                    chkAllowAutoDepositnon.Checked = false;
                    chkAllowCashWithdrawal.Checked = false;
                    chkAllowNoncashableDep.Checked = false;
                    chkAllowOffers.Checked = false;
                    chkAllowPartialTransfer.Checked = false;
                    chkAllowPointsWthdrawal.Checked = false;
                    chkAllowRedeemOffers.Checked = false;
                    chkAutoDepositCashable.Checked = false;
                    chkCashableDep.Checked = false;
                   // FillDenom(false);
                    cmbBaseDenom.SelectedText = "";
                    //cmbBaseDenom.DataSource = null;

                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_AFT_SETTING_DENOM") + " " + cmbBaseDenom.Text + this.GetResourceTextByKey(1, "MSG_DELETE_SUCCESSFUL"), this.ParentForm.Text);
                                       
                }
                LogManager.WriteLog("End btndelete_Click", LogManager.enumLogLevel.Info);
           
             }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            //MessageBox.Show("AFT Setting for the denom " + cmbBaseDenom.Text + " deleted successfully.","Bally MultiConnect - Enterprise");

        }
        /// <summary>
        /// validation check function for IsMultiplesOfDenom
        /// </summary>
        /// <param name="sVal"></param>
        /// <returns></returns>
        private bool IsMultiplesOfDenom(string sVal)
        {
            
                bool functionReturnValue = false;

                if (((Convert.ToInt64(sVal) % Convert.ToInt64(cmbBaseDenom.Text)) == 0))
                {
                    functionReturnValue = true;
                }
                           
                return functionReturnValue;
                      
        }

        /// <summary>
        /// Aftsettings update button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       private void btnupdate_Click(object sender, EventArgs e)
        {
            LogManager.WriteLog("inside  btnupdate_Click AFTSetting Start here", LogManager.enumLogLevel.Info);
            try
            {

                if (siteID == 0)
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CONFIGURE_SITE_FIRST"), this.ParentForm.Text);
                    return;
                }
                if (this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_UPDATE_SELECTED_BASE_DENOM"), this.ParentForm.Text) == DialogResult.No)
                {
                  return;
                }

                if (ValidateAFTSetting() == true)
                {
                 UpdateAftDetails();

                 LoadAftDetails(siteID,Convert.ToInt32( cmbBaseDenom.Text.Trim()));
                }
                LogManager.WriteLog("inside btnupdate_Click ValidateAFTSetting End here", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                
            }
         }
        /// <summary>
        /// validation for aftsettings 
        /// </summary>
        /// <returns></returns>
        public bool ValidateAFTSetting()
        {
            bool valueValidateAFTSetting = false;
            try
            {
                if (string.IsNullOrEmpty(cmbBaseDenom.Text.Trim()))
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_BASE_DENOM_CANNOT_EMPTY"), this.ParentForm.Text);
                    cmbBaseDenom.Focus();
                    return false;
                }
                if (txtOption1WithDrawAmt.Text.Trim() == "")
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_OPTION1_WITHDRAW_AMT_CANNOT_BLANK"), this.ParentForm.Text);
                    txtOption1WithDrawAmt.Focus();
                    return false;
                }
                if (txtOption2WithDrawAmt.Text.Trim() == "")
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_OPTION2_WITHDRAW_AMT_CANNOT_BLANK"), this.ParentForm.Text);
                    txtOption2WithDrawAmt.Focus();
                    return false;
                }
                if (txtOption3WithDrawAmt.Text.Trim() == "")
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_OPTION3_WITHDRAW_AMT_CANNOT_BLANK"), this.ParentForm.Text);
                    txtOption3WithDrawAmt.Focus();
                    return false;
                }
                if (txtOption4WithDrawAmt.Text.Trim() == "")
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_OPTION4_WITHDRAW_AMT_CANNOT_BLANK"), this.ParentForm.Text);
                    txtOption4WithDrawAmt.Focus();
                    return false;
                }
                if (txtOption5WithDrawAmt.Text.Trim() == "")
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_OPTION5_WITHDRAW_AMT_CANNOT_BLANK"), this.ParentForm.Text);
                    txtOption5WithDrawAmt.Focus();
                    return false;
                }
                if (txtEFTTimeout.Text.Trim() == "")
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_EFT_TIMEOUT_CANNOT_BLANK"), this.ParentForm.Text);
                    txtEFTTimeout.Focus();
                    return false;
                }
                if (txtMaxDepositAmt.Text.Trim() == "")
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_MAXDEPOSIT_AMT_CANNOT_BLANK"), this.ParentForm.Text);
                    txtMaxDepositAmt.Focus();
                    return false;
                }
                if (txtMaxwithdrawAmt.Text.Trim() == "")
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_MAXWITHDRAW_AMT_CANNOT_BLANK"), this.ParentForm.Text);
                    txtMaxwithdrawAmt.Focus();
                    return false;
                }

                if (txtOption1WithDrawAmt.Text.Trim() == "0")
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_OPTION1_WITHDRAW_AMT__SHOULD_GREATER_ZERO"), this.ParentForm.Text);
                    txtOption1WithDrawAmt.Focus();
                    return false;
                }
                if (txtOption2WithDrawAmt.Text.Trim() == "0")
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_OPTION2_WITHDRAW_AMT__SHOULD_GREATER_ZERO"), this.ParentForm.Text);
                    txtOption2WithDrawAmt.Focus();
                    return false;
                }
                if (txtOption3WithDrawAmt.Text.Trim() == "0")
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_OPTION3_WITHDRAW_AMT__SHOULD_GREATER_ZERO"), this.ParentForm.Text);
                    txtOption3WithDrawAmt.Focus();
                    return false;
                }
                if (txtOption4WithDrawAmt.Text.Trim() == "0")
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_OPTION4_WITHDRAW_AMT__SHOULD_GREATER_ZERO"), this.ParentForm.Text);
                    txtOption4WithDrawAmt.Focus();
                    return false;
                }
                if (txtOption5WithDrawAmt.Text.Trim() == "0")
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_OPTION5_WITHDRAW_AMT__SHOULD_GREATER_ZERO"), this.ParentForm.Text);
                    txtOption5WithDrawAmt.Focus();
                    return false;
                }
                if (txtEFTTimeout.Text.Trim() == "0")
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_EFT_TIMEOUT_SHOULD_GREATER_THAN_ZERO"), this.ParentForm.Text);
                    txtEFTTimeout.Focus();
                    return false;
                }
                if (txtMaxDepositAmt.Text.Trim() == "0")
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_MAX_DEPOSIT_AMT_SHOULD_GREATER_THAN_ZERO"), this.ParentForm.Text);
                    txtMaxDepositAmt.Focus();
                    return false;
                }
                if (txtMaxwithdrawAmt.Text.Trim() == "0")
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_MAX_WITHDRAWAL_AMT_SHOULD_GREATER_THAN_ZERO"), this.ParentForm.Text);
                    txtMaxwithdrawAmt.Focus();
                    return false;
                }

                if (!string.IsNullOrEmpty(txtOption1WithDrawAmt.Text.Trim()) && Convert.ToInt32(txtOption1WithDrawAmt.Text) > Convert.ToInt32(txtMaxwithdrawAmt.Text) * 100)
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_OPTION1_WITHDRAW_AMT_SHOULD_LESSER_THAN_MAX_WITHDRAW_AMT"), this.ParentForm.Text);
                    txtOption1WithDrawAmt.Focus();
                    return false;
                }
                if (!string.IsNullOrEmpty(txtOption2WithDrawAmt.Text.Trim()) && Convert.ToInt32(txtOption2WithDrawAmt.Text) > Convert.ToInt32(txtMaxwithdrawAmt.Text) * 100)
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_OPTION2_WITHDRAW_AMT_SHOULD_LESSER_THAN_MAX_WITHDRAW_AMT"), this.ParentForm.Text);
                    txtOption2WithDrawAmt.Focus();
                    return false;
                }
                if (!string.IsNullOrEmpty(txtOption3WithDrawAmt.Text.Trim()) && Convert.ToInt32(txtOption3WithDrawAmt.Text) > Convert.ToInt32(txtMaxwithdrawAmt.Text) * 100)
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_OPTION3_WITHDRAW_AMT_SHOULD_LESSER_THAN_MAX_WITHDRAW_AMT"), this.ParentForm.Text);
                    txtOption3WithDrawAmt.Focus();
                    return false;
                }
                if (!string.IsNullOrEmpty(txtOption4WithDrawAmt.Text.Trim()) && Convert.ToInt32(txtOption4WithDrawAmt.Text) > Convert.ToInt32(txtMaxwithdrawAmt.Text) * 100)
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_OPTION4_WITHDRAW_AMT_SHOULD_LESSER_THAN_MAX_WITHDRAW_AMT"), this.ParentForm.Text);
                    txtOption4WithDrawAmt.Focus();
                    return false;
                }
                if (!string.IsNullOrEmpty(txtOption5WithDrawAmt.Text.Trim()) && Convert.ToInt32(txtOption5WithDrawAmt.Text) > Convert.ToInt32(txtMaxwithdrawAmt.Text) * 100)
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_OPTION5_WITHDRAW_AMT_SHOULD_LESSER_THAN_MAX_WITHDRAW_AMT"), this.ParentForm.Text);
                    txtOption5WithDrawAmt.Focus();
                    return false;
                }
               
                if (Convert.ToInt32(cmbBaseDenom.Text.Trim()) == 0)
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_BASE_DENOM_SHOULD_NOT_ZERO"), this.ParentForm.Text);
                    cmbBaseDenom.Focus();
                    return false;

                }
                if (!string.IsNullOrEmpty((txtOption1WithDrawAmt.Text.Trim())))
                {
                    if (!IsMultiplesOfDenom((txtOption1WithDrawAmt.Text.Trim())))
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ENTER_OPTION1_WITHDRAW_AMT_IN_MULTIPLES_OF") + cmbBaseDenom.Text + "", this.ParentForm.Text);
                        txtOption1WithDrawAmt.Focus();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty((txtOption2WithDrawAmt.Text.Trim())))
                {
                    if (!IsMultiplesOfDenom((txtOption2WithDrawAmt.Text.Trim())))
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ENTER_OPTION2_WITHDRAW_AMT_IN_MULTIPLES_OF") + cmbBaseDenom.Text + "", this.ParentForm.Text);
                        txtOption2WithDrawAmt.Focus();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty((txtOption3WithDrawAmt.Text.Trim())))
                {
                    if (!IsMultiplesOfDenom((txtOption3WithDrawAmt.Text.Trim())))
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ENTER_OPTION3_WITHDRAW_AMT_IN_MULTIPLES_OF") + cmbBaseDenom.Text + "", this.ParentForm.Text);
                        txtOption3WithDrawAmt.Focus();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty((txtOption4WithDrawAmt.Text.Trim())))
                {
                    if (!IsMultiplesOfDenom((txtOption4WithDrawAmt.Text.Trim())))
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ENTER_OPTION4_WITHDRAW_AMT_IN_MULTIPLES_OF") + cmbBaseDenom.Text + "", this.ParentForm.Text);
                        txtOption4WithDrawAmt.Focus();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty((txtOption5WithDrawAmt.Text.Trim())))
                {
                    if (!IsMultiplesOfDenom((txtOption5WithDrawAmt.Text.Trim())))
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ENTER_OPTION5_WITHDRAW_AMT_IN_MULTIPLES_OF") + cmbBaseDenom.Text + "", this.ParentForm.Text);
                        txtOption5WithDrawAmt.Focus();
                        return false;
                    }
                }
                valueValidateAFTSetting = true;

                return valueValidateAFTSetting;
                                
            }
            catch (Exception ex)
            {  
                ExceptionManager.Publish(ex);
                return valueValidateAFTSetting = false;
            }
     
        }
        /// <summary>
        /// valadition for only numeric.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllowOnlyNumeric(object sender, KeyPressEventArgs e)
        {
            if(!char.IsNumber(e.KeyChar)&& !(e.KeyChar == (char)Keys.Back))
           
                e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                cmbBaseDenom.SelectedIndex = -1;
                btnupdate.Visible = false;
                btndelete.Visible = false;
                chkAllowAFTTransactions.Checked = false;
                chkAllowAutoDepositnon.Checked = false;
                chkAllowCashWithdrawal.Checked = false;
                chkAllowNoncashableDep.Checked = false;
                chkAllowOffers.Checked = false;
                chkAllowPartialTransfer.Checked = false;
                chkAllowPointsWthdrawal.Checked = false;
                chkAllowRedeemOffers.Checked = false;
                chkAutoDepositCashable.Checked = false;
                chkCashableDep.Checked = false;
                txtEFTTimeout.Text = "";
                txtMaxDepositAmt.Text = "";
                txtMaxwithdrawAmt.Text = "";
                txtOption1WithDrawAmt.Text = "";
                txtOption2WithDrawAmt.Text = "";
                txtOption3WithDrawAmt.Text = "";
                txtOption4WithDrawAmt.Text = "";
                txtOption5WithDrawAmt.Text = "";
                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnupdate_Click_1(object sender, EventArgs e)
        {
            LogManager.WriteLog("inside  btnupdate_Click AFTSetting Start here", LogManager.enumLogLevel.Info);
            try
            {

                if (siteID == 0)
                {
                  return;
                }
                if (this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_UPDATE_SELECTED_BASE_DENOM"), this.ParentForm.Text) == DialogResult.No)
                {
                    return;
                }

                if (ValidateAFTSetting() == true)
                {
                    UpdateAftDetails();
                }
                LogManager.WriteLog("inside btnupdate_Click ValidateAFTSetting End here", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }

        }

        private void btndelete_Click_1(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("inside btndelete_Click", LogManager.enumLogLevel.Info);
                if (cmbBaseDenom.SelectedIndex <= -1)
                {

                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SELECT_APPROPRIATE_BASE_DENOM"), this.ParentForm.Text);
                    return;
                }
                if (this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_DELETE_SELECTED_BASE_DENOM"), this.ParentForm.Text) == DialogResult.No)
                {
                    return;
                }
                else
                {

                    string var = (((AftsettingsEntity)cmbBaseDenom.SelectedValue).Denom).ToString();
                    AftsettingsBusiness.CreateInstance().DeleteAftsettingDetails(siteID, var);
                    denomitems.Remove(Convert.ToInt32(cmbBaseDenom.Text));
                    objEntity = AftsettingsBusiness.CreateInstance().Aftsettingdenome(siteID);
                    cmbBaseDenom.DataSource = objEntity;
                    cmbBaseDenom.SelectedIndex = -1;
                    txtEFTTimeout.Text = "";
                    txtMaxDepositAmt.Text = "";
                    txtMaxwithdrawAmt.Text = "";
                    txtOption1WithDrawAmt.Text = "";
                    txtOption2WithDrawAmt.Text = "";
                    txtOption3WithDrawAmt.Text = "";
                    txtOption4WithDrawAmt.Text = "";
                    txtOption5WithDrawAmt.Text = "";
                    chkAllowAFTTransactions.Checked = false;
                    chkAllowAutoDepositnon.Checked = false;
                    chkAllowCashWithdrawal.Checked = false;
                    chkAllowNoncashableDep.Checked = false;
                    chkAllowOffers.Checked = false;
                    chkAllowPartialTransfer.Checked = false;
                    chkAllowPointsWthdrawal.Checked = false;
                    chkAllowRedeemOffers.Checked = false;
                    chkAutoDepositCashable.Checked = false;
                    chkCashableDep.Checked = false;
                    cmbBaseDenom.SelectedText = "";

                    AftsettingsBusiness.CreateInstance().AuditDeleteddata(ModuleNameEnterprise.AUDIT_SITE, "AFT Settings", "Base Denom", var, AppGlobals.Current.UserId, AppGlobals.Current.UserName, _SiteName);
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_AFT_SETTING_DENOM") + " " + cmbBaseDenom.Text + this.GetResourceTextByKey(1, "MSG_DELETE_SUCCESSFUL"), this.ParentForm.Text);

                }
                LogManager.WriteLog("End btndelete_Click", LogManager.enumLogLevel.Info);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }
                       
    }
}

