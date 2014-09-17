using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseBusiness.Business.ServiceCalls;
using BMC.EnterpriseBusiness.Entities.ServiceCalls;
using BMC.EnterpriseClient.Helpers;

namespace BMC.EnterpriseClient.Views.ServiceCalls
{
    public partial class frmConfigureMailRecipients : Form
    {
        #region Private Data Members

        private GMUFaultBusiness objGMUFaultBusiness = null;
        private int iFaultID;
        private BMC.EnterpriseClient.Helpers.Datawatcher ObjDataWatcher=null;

        #endregion

        #region Public Data Members

        public bool IsRefresh = false;

        #endregion

        #region Constructors

        public frmConfigureMailRecipients()
        {
            InitializeComponent();
            ObjDataWatcher = new Datawatcher(this);

        }
        /// <summary>
        /// This Constructor is used to populate configuared mail ids from GMU Fault Configuration.
        /// </summary>
        /// <param name="iFaultId"></param>
        /// <param name="sCcAddress"></param>
        /// <param name="sToAddress"></param>
        public frmConfigureMailRecipients(int iFaultId, string sCcAddress, string sToAddress)
        {
            InitializeComponent();
            iFaultID = iFaultId;
            txtCCAddresse.Text = sCcAddress;
            txtToAddresse.Text = sToAddress;
            ObjDataWatcher = new Datawatcher(this);
        }

        #endregion

        #region events

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string sCCAddress = txtCCAddresse.Text.Trim();
                string sToAddress = txtToAddresse.Text.Trim();

                UpdateMailConfiguration(sCCAddress, sToAddress);
                ObjDataWatcher.DataModify = false;
               
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void frmConfigureMailRecipients_Load(object sender, EventArgs e)
        {
            try
            {
                objGMUFaultBusiness = GMUFaultBusiness.CreateInstance();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// This method is used to validate the given Single/Multiple mailId(s).
        /// </summary>
        /// <param name="sMail"></param>
        /// <returns></returns>
        private bool IsValidMail(string sMail)
        {
            try
            {                
                string[] sAvailableMailIds = sMail.Split(';');
                foreach (string emailid in sAvailableMailIds)
                {
                    var addr = new System.Net.Mail.MailAddress(emailid);
                }
                return true;               
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// This method is used to update mail configuration
        /// </summary>
        /// <param name="sCCAddress"></param>
        /// <param name="sToAddress"></param>
        private void UpdateMailConfiguration(string sCCAddress, string sToAddress)
        {
            try
            {
                if (!string.IsNullOrEmpty(sCCAddress) && !string.IsNullOrEmpty(sToAddress))//Both To & CC address are available
                {
                    if (IsValidMail(sCCAddress) && IsValidMail(sToAddress))//Both To & CC address are valid
                    {
                        UpdateMailConfigurationsDetail(sCCAddress, sToAddress);//Update the values to database.

                    }
                    else
                    {
                        this.ShowErrorMessageBox("The Email Addresses provided are not in the correct format. Please assign the correct addresses.");
                        
                    }
                }
                else if (string.IsNullOrEmpty(sCCAddress) && string.IsNullOrEmpty(sToAddress))//Both To & CC address are not available so display error message to user.
                {
                    this.ShowErrorMessageBox("The To and CC Recipients have not been set.");
                    this.txtToAddresse.Focus();
                }
                else
                {
                    if (string.IsNullOrEmpty(sCCAddress) && !string.IsNullOrEmpty(sToAddress))//To address available and CC address is not available.
                    {
                        if (IsValidMail(sToAddress))//Validate the To address
                        {
                            this.ShowInfoMessageBox("The CC Recipients have not been set.");
                            UpdateMailConfigurationsDetail(sCCAddress, sToAddress);//Save valid To address and empty CC address.
                        }
                        else
                        {
                            this.ShowErrorMessageBox("The Email Addresses provided are not in the correct format. Please assign the correct addresses.");
                            this.txtToAddresse.Focus();
                        }
                    }
                    else
                    {
                        if (IsValidMail(sCCAddress))//validate the CC address 
                        {
                            this.ShowInfoMessageBox("The To Recipients have not been set.");
                            UpdateMailConfigurationsDetail(sCCAddress, sToAddress);
                        }
                        else
                        {
                            this.ShowErrorMessageBox("The Email Addresses provided are not in the correct format. Please assign the correct addresses.");
                            this.txtCCAddresse.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// This method is used to update the CC Address and To Address email Id 
        /// </summary>
        /// <param name="sCCAddress"></param>
        /// <param name="sToAddress"></param>
        private void UpdateMailConfigurationsDetail(string sCCAddress, string sToAddress)
        {
            try
            {
                if (this.ShowQuestionMessageBox("This will save the recipients details. Do you wish to Continue?") == DialogResult.Yes)
                {
                    objGMUFaultBusiness.UpdateMailConfigurations(iFaultID, sToAddress, sCCAddress);
                    this.ShowInfoMessageBox("TO/CC Addresses Information saved successfully.");

                    objGMUFaultBusiness.InsertNewAuditEntry(Audit.Transport.ModuleNameEnterprise.GmuFaultEvents, this.Text, "", "To Addresse(Mail):==>" + txtToAddresse.Text + "CC Addresse(Mail):==>" + txtCCAddresse.Text);

                    IsRefresh = true;
                    
                    ObjDataWatcher.DataModify = false;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion

    }
}
