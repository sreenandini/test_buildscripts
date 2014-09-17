using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseClient.Helpers;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.ExceptionManagement;
using BMC.Common.Utilities;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmPayTableMaster : GenericFormBase
    {
        #region Data Members

        private GameLibraryBiz objGameLibraryBiz = new GameLibraryBiz();
        private int iPayTableId = 0;
        private double dOldTheoValue = 0.0D;
         BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;

        #endregion //Data Members

        #region Constructor

        public frmPayTableMaster(int iPayTableId)
        {
            try
            {
                InitializeComponent();
                SetTagProperty();
                this.ResolveResources();
                this.iPayTableId = iPayTableId;
                LoadPayTableDetailsToUI(iPayTableId);
                objDatawatcher = new Helpers.Datawatcher(this);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion //Constructor

        #region Events

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                double dTheoPayout = 0.0D;
                if (!double.TryParse(txtTheoPayout.Text.Trim(), out dTheoPayout))
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_INVALID_THEORETICAL_PAYOUT"), this.Text);
                    return;
                }

                if (dTheoPayout == dOldTheoValue)
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_MODIFY_THEORETICAL_PAYOUT"), this.Text);
                    return;
                }

                objGameLibraryBiz.UpdatePayTableTheoreticalPayout(iPayTableId, dTheoPayout);
                objGameLibraryBiz.AuditPayTableModification("Thoeritical Payout %", dOldTheoValue.ToString(), dTheoPayout.ToString());
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_THEORETICAL_PAYOUT_UPDATED"), this.Text);
                this.DialogResult = DialogResult.OK;
                objDatawatcher.DataModify = false;
                this.Close();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
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

        #endregion //Events

        #region Methods

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.btnCancel.Tag = "Key_CancelCaption";
            this.grpPayTable.Tag = "Key_PayTable";
            this.lblPTPercentagePayout.Tag = "Key_PayTablePayoutColon";
            this.Tag = "Key_PayTableAdministration";
            this.lblPTBet.Tag = "Key_PayTableBetCreditsColon";
            this.lblPTId.Tag = "Key_PayTableIDColon";
            this.btnUpdate.Tag = "Key_SaveCaption";
            this.lblTheoPercentagePayout.Tag = "Key_TheoPayoutPercColon";

        }

        private void LoadPayTableDetailsToUI(int iPayTableID)
        {
            try
            {
                List<PayTableDetails> lstPayTableDetails = objGameLibraryBiz.GetPayTableDetails(iPayTableID);
                foreach (PayTableDetails objPayTableDetails in lstPayTableDetails)
                {
                    txtPayTableID.Text = objPayTableDetails.PT_Description;
                    txtPaytablePayout.Text = objPayTableDetails.Payout.GetValueOrDefault().ToString();
                    txtMaxbet.Text = objPayTableDetails.MaxBet.ToString() == "0" ? "N/A" : objPayTableDetails.MaxBet.ToString();
                    txtTheoPayout.Text = objPayTableDetails.TheoreticalPayout.ToString();
                    dOldTheoValue = objPayTableDetails.TheoreticalPayout;
                    txtTheoPayout.Select(txtTheoPayout.Text.Length, 0);
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion //Methods

        private void txtTheoPayout_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ExtensionMethods.IsValidDecimalNumber(e.KeyChar);
        }
    }
}
