using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BMC.EnterpriseClient.Helpers;
using BMC.CommonLiquidation.Utilities;
using BMC.EnterpriseBusiness.Business;
using BMC.Common.ExceptionManagement;
using BMC.Common.Utilities;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmProfitShare : GenericFormBase
    {
        #region Data Members

        private List<ProfitShareGroup> lstProfitShareGroup = null;
        private List<ExpenseShareGroup> lstExpenseShareGroup = null;
        private List<PayPeriods> lstPayPeriods = null;
        private CommonLiquidationEntity _objCommonLiquidation = null;
        private PayPeriods objPayPeriods = new PayPeriods();
        private ReadBasedLiquidationBiz objReadBasedLiquidationBiz = new ReadBasedLiquidationBiz();
        private int iSiteId = 0;
        private ToolTip ToolTip1 = null;
        string cmbDefaultText = string.Empty;
        BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;

        #endregion //Data Members

        #region Properties

        public CommonLiquidationEntity objCommonLiquidation
        {
            get
            {
                return _objCommonLiquidation;
            }
            set
            {
                _objCommonLiquidation = value;
            }
        }

        #endregion //Properties

        #region Constructor

        public frmProfitShare(int iSiteId, CommonLiquidationEntity objCommonLiquidation)
        {
            try
            {
                cmbDefaultText = this.GetResourceTextByKey("Key_PleaSelect");

                this.iSiteId = iSiteId;
                this.objCommonLiquidation = objCommonLiquidation;
                InitializeComponent();
                SetTagProperty();
                this.ResolveResources();
                LoadProfitShareGroup();
                LoadExpenseShareGroup();
                LoadPayPeriods();
                LoadAmounts();
                EnableDisableControls();
                objDatawatcher = new Helpers.Datawatcher(this);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion //Constructor

        #region Events

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboProfitShareGroup.SelectedIndex <= 0)
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SELECT_PROFIT_SHARE_GRP"),this.Text);
                    return;
                }
                decimal FixedExpenseAmount = (Convert.ToDecimal(txtExpenseShareAmount.Text) * (Convert.ToDecimal(((ExpenseShareGroup)cboExpenseShareGroup.SelectedItem).ExpenseSharePercentage) / 100));
                decimal dTotal = Convert.ToDecimal(FixedExpenseAmount) + Convert.ToDecimal(txtCarriedForwardAmount.Text);
                if (Math.Round(Convert.ToDecimal(txtWriteOffExpense.Text), 2, MidpointRounding.AwayFromZero) > (Math.Round(FixedExpenseAmount, 2) + Math.Round(Convert.ToDecimal(txtCarriedForwardAmount.Text), 2, MidpointRounding.AwayFromZero)))
                {
                    frmAuthorizationScreen oAuthorize = new frmAuthorizationScreen("HQ_Admin_AuthorizeProfitShare");
                    oAuthorize.ShowDialog();
                    if (!oAuthorize.IsAuthorized)
                        return;
                }
                objCommonLiquidation.ProfitShareGroupId = Convert.ToInt32(cboProfitShareGroup.SelectedValue);
                objCommonLiquidation.ExpenseShareGroupID = Convert.ToInt32(cboExpenseShareGroup.SelectedValue);
                objCommonLiquidation.ExpenseShareAmount = Convert.ToDecimal(txtExpenseShareAmount.Text);
                objCommonLiquidation.WriteOffAmount = Convert.ToDecimal(txtWriteOffExpense.Text);
                objCommonLiquidation.PayPeriodId = objPayPeriods.Calendar_Period_ID;
                objCommonLiquidation.Percentage_Setting = Convert.ToDecimal(((ProfitShareGroup)cboProfitShareGroup.SelectedItem).ProfitSharePercentage);
                objCommonLiquidation.ExpenseSharePercentage = Convert.ToDecimal(((ExpenseShareGroup)cboExpenseShareGroup.SelectedItem).ExpenseSharePercentage);
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

        private void cboExpenseShareGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboExpenseShareGroup.SelectedIndex != 0)
                {
                    txtExpenseShareAmount.ReadOnly = false;
                    txtWriteOffExpense.ReadOnly = false;
                }
                else
                {
                    txtExpenseShareAmount.ReadOnly = true;
                    txtWriteOffExpense.ReadOnly = true;
                    txtExpenseShareAmount.Text = "0.00";
                    txtWriteOffExpense.Text = "0.00";
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion //Events

        #region Method

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.btnCancel.Tag = "Key_CancelCaption";
            this.btnOk.Tag = "Key_OKCaption";
            this.lblCarriedForwardAmount.Tag = "Key_CarryForwardAmountColon";
            this.lblExpenseShareAmount.Tag = "Key_ExpenseAmountColon";
            this.lblExpenseShareGroup.Tag = "Key_ExpenseShareGroupColon";
            this.lblPayPeriod.Tag = "Key_PayPeriodColon";
            this.Tag = "Key_ProfitShare";
            this.lblProfitShareGroup.Tag = "Key_ProfitShareGroupColon";
            this.lblWriteOffExpense.Tag = "Key_WriteOffExpenseColon";

        }

        private void LoadProfitShareGroup()
        {
            try
            {
                lstProfitShareGroup = objReadBasedLiquidationBiz.GetProfitShareGroupList(cmbDefaultText);
                cboProfitShareGroup.DataSource = lstProfitShareGroup;
                cboProfitShareGroup.DisplayMember = "ProfitShareGroupName";
                cboProfitShareGroup.ValueMember = "ProfitShareGroupID";
                cboProfitShareGroup.SelectedValue = objCommonLiquidation.ProfitShareGroupId;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadExpenseShareGroup()
        {
            try
            {
                lstExpenseShareGroup = objReadBasedLiquidationBiz.GetExpenseShareGroupList(cmbDefaultText);
                cboExpenseShareGroup.DataSource = lstExpenseShareGroup;
                cboExpenseShareGroup.DisplayMember = "ExpenseShareGroupName";
                cboExpenseShareGroup.ValueMember = "ExpenseShareGroupID";
                cboExpenseShareGroup.SelectedValue = objCommonLiquidation.ExpenseShareGroupID;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadAmounts()
        {
            try
            {
                txtExpenseShareAmount.Text = objCommonLiquidation.ExpenseShareAmount.ToString("#,##0.00");
                txtWriteOffExpense.Text = objCommonLiquidation.WriteOffAmount.ToString("#,##0.00");
                txtCarriedForwardAmount.Text = objCommonLiquidation.PrevCarriedForwardExpense.GetValueOrDefault().ToString("#,##0.00");
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadPayPeriods()
        {
            try
            {
                lstPayPeriods = objReadBasedLiquidationBiz.GetPayPeriods(iSiteId, cmbDefaultText);
                objPayPeriods = lstPayPeriods.Where(item => item.Calendar_Period_Start_Date <= DateTime.Today && item.Calendar_Period_End_Date >= DateTime.Today).FirstOrDefault() as PayPeriods;

                if (objPayPeriods == null)
                {
                    objPayPeriods = new PayPeriods();
                    return;
                };
                lblPayPeriodValue.Text = objPayPeriods.Calendar_Period;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void EnableDisableControls()
        {
            try
            {
                string strOutput = string.Empty;
                CommonBiz.GetSiteSetting(iSiteId, "ExpenseShare", ref strOutput);
                if (!Convert.ToBoolean(strOutput))
                {
                    lblExpenseShareGroup.Visible = false;
                    cboExpenseShareGroup.Visible = false;
                    lblExpenseShareAmount.Visible = false;
                    txtExpenseShareAmount.Visible = false;

                    tblpnlProfitShare.RowStyles[1].Height = 0;
                    tblpnlProfitShare.RowStyles[1].SizeType = SizeType.AutoSize;
                    tblpnlProfitShare.RowStyles[2].Height = 0;
                    tblpnlProfitShare.RowStyles[2].SizeType = SizeType.AutoSize;
                }

                strOutput = string.Empty;
                CommonBiz.GetSiteSetting(iSiteId, "WriteOffShare", ref strOutput);
                if (!Convert.ToBoolean(strOutput))
                {
                    lblWriteOffExpense.Visible = false;
                    txtWriteOffExpense.Visible = false;

                    tblpnlProfitShare.RowStyles[3].Height = 0;
                    tblpnlProfitShare.RowStyles[3].SizeType = SizeType.AutoSize;
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion //Method

    }
}
