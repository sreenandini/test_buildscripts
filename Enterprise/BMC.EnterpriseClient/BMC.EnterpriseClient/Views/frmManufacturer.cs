using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using BMC.EnterpriseBusiness;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using Audit.Transport;
using BMC.CoreLib.Win32;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmManufacturer : Form
    {
        #region Data Members

        private GameLibraryBiz objGameLibraryBiz = new GameLibraryBiz();
        private List<ManufacturerDetails> lstManufacturerDetails = null;
        private int _iManufacturerId = 0;
        private DialogResult _dialogReault = DialogResult.Cancel;
         BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;

        #endregion //Data Members

        #region Constructor

        public frmManufacturer()
        {
            InitializeComponent();
            SetTagProperty();
            LoadManufacturers();


            objDatawatcher = new Helpers.Datawatcher(this,
                  (w, f) =>
                  {
                      w.RemoveControlFromWatcher((f as frmManufacturer).lstManufacturers);
                      w.RemoveControlFromWatcher((f as frmManufacturer).chkSCB);
                  });
            
          

        }

        public frmManufacturer(int ManufacturerId)
        {
            _iManufacturerId = ManufacturerId;
            InitializeComponent();
            SetTagProperty();
            LoadManufacturers();

            objDatawatcher = new Helpers.Datawatcher(this,
                 (w, f) =>
                 {
                     w.RemoveControlFromWatcher((f as frmManufacturer).lstManufacturers);

                 });
        }

        #endregion //Constructor

        #region Events

        /// <summary>
        /// To switch over between Meter and Sales and Service groups
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnViewMeterSet_Click(object sender, EventArgs e)
        {
            try
            {
                if (grpSingleCoin.Visible == false)
                {
                    grpSingleCoin.Visible = true;
                    grpAll.Visible = false;
                    btnViewMeterSet.Text = this.GetResourceTextByKey("Key_CloseMeterSetCaption");
                    return;
                }

                grpAll.Visible = true;
                grpSingleCoin.Visible = false;
                btnViewMeterSet.Text = this.GetResourceTextByKey("Key_OpenMeterSetCaption");
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// To enable/disable and too change the caption of ViewMeterSet button, based on the Single coin check value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkSCB_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                grpSingleCoin.Visible = false;
                grpAll.Visible = true;
                btnViewMeterSet.Text = this.GetResourceTextByKey("Key_OpenMeterSetCaption");

                if (chkSCB.Checked == false)
                {
                    btnViewMeterSet.Enabled = false;
                    return;
                }
                btnViewMeterSet.Enabled = true;
                objDatawatcher.DataModify = false;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// On form load to disable single coin group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManufacturerForm_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
            try
            {
                grpSingleCoin.Visible = false;
                lstManufacturers.SelectedValue = _iManufacturerId;
                objDatawatcher.DataModify = false;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// On manufacturer changed, to reload Meter, Sales and Service values w.r.t the newly selected manufacturer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstManufacturers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstManufacturers.Items.Count <= 0)
                    return;

                btnViewMeterSet.Enabled = false;
                grpSingleCoin.Visible = false;
                grpAll.Visible = true;

                if (lstManufacturers.SelectedIndex < 0)
                {
                    ClearData();
                    return;
                }

                Manufacturer ObjManufacturer = (Manufacturer)lstManufacturers.SelectedItem;
                lstManufacturerDetails = objGameLibraryBiz.GetManufacturerDetails(ObjManufacturer.Manufacturer_ID);

                foreach (var manf in lstManufacturerDetails)
                {
                    txtManufacturerName.Text = manf.Manufacturer_Name;
                    txtManufacturerCode.Text = manf.Manufacturer_Code;
                    txtSalesAddress.Text = manf.Manufacturer_Sales_Address;
                    txtSalesPostcode.Text = manf.Manufacturer_Sales_Postcode;
                    txtSalesContact.Text = manf.Manufacturer_Sales_Contact;
                    txtSalesTel.Text = manf.Manufacturer_Sales_Tel;
                    txtSalesEmail.Text = manf.Manufacturer_Sales_EMail;
                    txtServiceAddress.Text = manf.Manufacturer_Service_Address;
                    txtServicePostcode.Text = manf.Manufacturer_Service_Postcode;
                    txtServiceContact.Text = manf.Manufacturer_Service_Contact;
                    txtServiceTel.Text = manf.Manufacturer_Service_Tel;
                    txtServiceEmail.Text = manf.Manufacturer_Service_EMail;
                    chkSCB.Checked = Convert.ToBoolean(manf.Manufacturer_Single_Coin_Build);
                    chkCoinsIn.Checked = Convert.ToBoolean(manf.Manufacturer_Coins_In_Meter_Used);
                    chkCoinsOut.Checked = Convert.ToBoolean(manf.Manufacturer_Coins_Out_Meter_Used);
                    chkCoinsDrop.Checked = Convert.ToBoolean(manf.Manufacturer_Coin_Drop_Meter_Used);
                    chkHandpay.Checked = Convert.ToBoolean(manf.Manufacturer_Handpay_Meter_Used);
                    chkExternalCredits.Checked = Convert.ToBoolean(manf.Manufacturer_External_Credits_Meter_Used);
                    chkNotes.Checked = Convert.ToBoolean(manf.Manufacturer_Notes_Meter_Used);
                    chkGamesBet.Checked = Convert.ToBoolean(manf.Manufacturer_Games_Bet_Meter_Used);
                    chkGamesWon.Checked = Convert.ToBoolean(manf.Manufacturer_Games_Won_Meter_Used);
                    chkHandpayAdded2CoinOut.Checked = Convert.ToBoolean(manf.Manufacturer_Handpay_Added_To_Coin_Out);
                    btnViewMeterSet.Enabled = chkSCB.Checked;
                }
                objDatawatcher.DataModify = false;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// On click update validate input values and save it into the DB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstManufacturers.SelectedIndex < 0)
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_NO_MANUFACTURER_SELECTED"), this.Text);
                    return;
                }

                if (string.IsNullOrEmpty(txtManufacturerName.Text))
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_ENTER_MANUFACTURER_NAME"), this.Text);
                    return;
                }

                if (txtSalesEmail.Text.Trim() != "" && !txtSalesEmail.Text.IsValidEmail())
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_INCORRECT_SALES_EMAIL_ID"), this.Text);
                    txtSalesEmail.Focus();
                    return;
                }

                if (txtServiceEmail.Text.Trim() != "" && !txtServiceEmail.Text.IsValidEmail())
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_INCORRECT_SERVICE_EMAIL_ID"), this.Text);
                    txtServiceEmail.Focus();
                    return;
                }

                Manufacturer ObjManufacturer = (Manufacturer)lstManufacturers.SelectedItem;
                if (objGameLibraryBiz.VerifyManufacturerName(txtManufacturerName.Text, ObjManufacturer.Manufacturer_ID) > 0)
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_ALREADY_MANUFACTURER"), this.Text);
                    txtManufacturerName.Focus();
                    return;
                }

                ManufacturerDetails objManufDetails = new ManufacturerDetails();
                objManufDetails.Manufacturer_ID = ObjManufacturer.Manufacturer_ID;
                objManufDetails.Manufacturer_Name = txtManufacturerName.Text;
                objManufDetails.Manufacturer_Code = txtManufacturerCode.Text;
                objManufDetails.Manufacturer_Sales_Address = txtSalesAddress.Text;
                objManufDetails.Manufacturer_Sales_Postcode = txtSalesPostcode.Text;
                objManufDetails.Manufacturer_Sales_Contact = txtSalesContact.Text;
                objManufDetails.Manufacturer_Sales_Tel = txtSalesTel.Text;
                objManufDetails.Manufacturer_Sales_EMail = txtSalesEmail.Text;
                objManufDetails.Manufacturer_Service_Address = txtServiceAddress.Text;
                objManufDetails.Manufacturer_Service_Postcode = txtServicePostcode.Text;
                objManufDetails.Manufacturer_Service_Contact = txtServiceContact.Text;
                objManufDetails.Manufacturer_Service_Tel = txtServiceTel.Text;
                objManufDetails.Manufacturer_Service_EMail = txtServiceEmail.Text;
                objManufDetails.Manufacturer_Single_Coin_Build = chkSCB.Checked;
                objManufDetails.Manufacturer_Coins_In_Meter_Used = chkCoinsIn.Checked;
                objManufDetails.Manufacturer_Coins_Out_Meter_Used = chkCoinsOut.Checked;
                objManufDetails.Manufacturer_Coin_Drop_Meter_Used = chkCoinsDrop.Checked;
                objManufDetails.Manufacturer_Handpay_Meter_Used = chkHandpay.Checked;
                objManufDetails.Manufacturer_External_Credits_Meter_Used = chkExternalCredits.Checked;
                objManufDetails.Manufacturer_Notes_Meter_Used = chkNotes.Checked;
                objManufDetails.Manufacturer_Games_Bet_Meter_Used = chkGamesBet.Checked;
                objManufDetails.Manufacturer_Games_Won_Meter_Used = chkGamesWon.Checked;
                objManufDetails.Manufacturer_Handpay_Added_To_Coin_Out = chkHandpayAdded2CoinOut.Checked;
                int updatestatus = objGameLibraryBiz.UpdateManufacturerDetails(objManufDetails);
                _iManufacturerId = objManufDetails.Manufacturer_ID;
                objGameLibraryBiz.InsertExportHistory(objManufDetails.Manufacturer_ID, "MANUFACTURER_DETAILS", "ALL");
                objGameLibraryBiz.AuditUpdatedManufacturerDetails(lstManufacturerDetails[0], objManufDetails);

                if (updatestatus == 0)
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_MANUFACTURER_UPDATED"), this.Text);
                    _dialogReault = DialogResult.OK;
                    LoadManufacturers();
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Get the new manufacturer name from the user and insert it into the DB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                string sManufacturerName = string.Empty;
                frmInputBox frm_input = new frmInputBox(this.GetResourceTextByKey(1, "MSG_ENTER_MANUFACTURER_NAME"), this.GetResourceTextByKey(1, "MSG_ENTER_MANUFACTURER_NAME"));
                frm_input.Text = this.GetResourceTextByKey(1, "MSG_APP_TITLE");
               
                frm_input.ShowDialog();
                sManufacturerName = frm_input.TextValue;              
                if (sManufacturerName.Trim().Length == 0)
                    return;//cancel is pressed


                if (sManufacturerName.Length > 30)
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_MANUFAC_NAME_CANNOT_EXCEED"), this.Text);
                    return;
                }

                if (CommonBiz.EnsureValidString(sManufacturerName) != sManufacturerName)
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_INVALID_NAME"), this.Text);
                    return;
                }

               
                if (objGameLibraryBiz.VerifyManufacturerName(sManufacturerName, 0) > 0)
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_ALREADY_MANUFACTURER"), this.Text);
                    return;
                }

                int iManufacturerId = objGameLibraryBiz.InsertManufacturerName(sManufacturerName);
                _iManufacturerId = iManufacturerId;
                objGameLibraryBiz.InsertExportHistory(iManufacturerId, "MANUFACTURER_DETAILS", "ALL");
                objGameLibraryBiz.InsertNewAuditEntry(ModuleNameEnterprise.AUDIT_MANUFACTURER, "Manufacturer", "Manufacturer_Name", sManufacturerName);
                _dialogReault = DialogResult.OK;
                LoadManufacturers();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// To close the manufacturer screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtChkValidText_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                e.Handled = e.KeyChar.IsValidText();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void frmManufacturer_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.DialogResult = _dialogReault;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion //Events

        #region Methods

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.btnAddNew.Tag = "Key_AddCaption";
            this.btnClose.Tag = "Key_CloseCaption";
            this.btnViewMeterSet.Tag = "Key_OpenMeterSetCaption";
            this.btnUpdate.Tag = "Key_UpdateCaption";
            this.lbltManufacturerName.Tag = "Key_ManufacturerMandatory";
            this.lblSalesAddress.Tag = "Key_AddressColon";
            this.lblServiceAddress.Tag = "Key_AddressColon";
            this.lblManufacturerCode.Tag = "Key_CodeColon";
            this.lblSalesContact.Tag = "Key_ContactColon";
            this.lblServiceContact.Tag = "Key_ContactColon";
            this.lblSalesEmail.Tag = "Key_EMailColon";
            this.lblServiceEmail.Tag = "Key_EMailColon";
            this.Tag = "Key_ManufacturerForm";
            this.lblSalesPostCode.Tag = "Key_PostcodeColon";
            this.lblServicePostcode.Tag = "Key_PostcodeColon";
            this.lblSales.Tag = "Key_Sales";
            this.lblService.Tag = "Key_Service";
            this.chkSCB.Tag = "Key_SingleCoinBuild";
            this.lblSalesTel.Tag = "Key_TelColon";
            this.lblServiceTel.Tag = "Key_TelColon";
            this.chkHandpayAdded2CoinOut.Tag = "Key_HandPayAdded";
            this.chkCoinsIn.Tag = "Key_CoinsIn";
            this.chkGamesWon.Tag = "Key_GamesWon";
            this.chkCoinsOut.Tag = "Key_CoinsOut";
            this.chkGamesBet.Tag = "Key_GamesBet";
            this.chkCoinsDrop.Tag = "Key_CoinsDrop";
            this.chkNotes.Tag = "Key_Notes";
            this.chkHandpay.Tag = "Key_Handpay";
            this.chkExternalCredits.Tag = "Key_ExternalCredits";


        }

        /// <summary>
        /// Load the manufacturer value from the DB and bind/load it into the manufacturer list
        /// </summary>
        private void LoadManufacturers()
        {
            try
            {
                LogManager.WriteLog("Manufacturer: LoadManufacturers Method", LogManager.enumLogLevel.Info);
                lstManufacturers.DisplayMember = "Manufacturer_Name";
                lstManufacturers.ValueMember = "Manufacturer_ID";

                string defaultstring = this.GetResourceTextByKey("Key_All").ToUpper();
                List<Manufacturer> lstManf = objGameLibraryBiz.GetManufacturers(false, defaultstring, defaultstring).OrderBy(s => s.Manufacturer_Name).ToList();
                lstManufacturers.DataSource = lstManf;
                lstManufacturers.SelectedValue = _iManufacturerId;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// To clear Meter, Sales and Service values
        /// </summary>
        private void ClearData()
        {
            try
            {
                txtManufacturerName.Text = string.Empty;
                txtManufacturerCode.Text = string.Empty;
                txtSalesAddress.Text = string.Empty;
                txtSalesPostcode.Text = string.Empty;
                txtSalesContact.Text = string.Empty;
                txtSalesTel.Text = string.Empty;
                txtSalesEmail.Text = string.Empty;
                txtServiceAddress.Text = string.Empty;
                txtServicePostcode.Text = string.Empty;
                txtServiceContact.Text = string.Empty;
                txtServiceTel.Text = string.Empty;
                txtServiceEmail.Text = string.Empty;
                chkSCB.Checked = false;
                chkCoinsIn.Checked = false;
                chkCoinsOut.Checked = false;
                chkCoinsDrop.Checked = false;
                chkHandpay.Checked = false;
                chkExternalCredits.Checked = false;
                chkNotes.Checked = false;
                chkGamesBet.Checked = false;
                chkGamesWon.Checked = false;
                chkHandpayAdded2CoinOut.Checked = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion //Methods

    }
}
