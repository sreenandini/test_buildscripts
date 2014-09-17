using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.Utilities;
using System.Data.Linq;
using BMC.EnterpriseBusiness.Business.CashierTransations;
using BMC.EnterpriseDataAccess;
using BMC.EnterpriseClient.Views;
using BMC.Common;
using BMC.CoreLib.Win32;
using BMC.Reports;
using BMC.ReportViewer;
using BMC.EnterpriseBusiness.Entities;

namespace BMC.EnterpriseClient.CashierTransactions
{
    public partial class frmCashDeskManager : Form
    {
        const string TREASURY_REFILL = "Refill";
        const string TREASURY_REFUND = "Refund";
        const string TREASURY_SHORTPAY = "Shortpay";
        const string TREASURY_HANDPAY_CREDIT = "Attendantpay Credit";
        const string TREASURY_CASH_DESK_FLOAT = "Cash Desk Float";
        //string TREASURY_PROGRESSIVE = "Progressive";
        //string TREASURY_JACKPOT = "Attendantpay Jackpot";
        bool Isrefreshed = false;
        BMCCashierTreasuryTransactions oBusiness;
        //List<TicketExceptions> lstTicketExcep = null;
        int _SITEID = 0;
        string _SITECODE = string.Empty;
        string _SITENAME = string.Empty;
        DataSet dsCashierHistory = new DataSet();

        CashierTransactionsDA objResult = new CashierTransactionsDA();
        int _iNoofRecords = 0;
        int _UserID = 0;

        string UserName = string.Empty;
        string RouteName = string.Empty;
        public frmCashDeskManager(int SITEID,int UserID)
        {
            InitializeComponent();
            SetTagProperty();
            oBusiness = new BMCCashierTreasuryTransactions();
            _SITEID = SITEID;
            _UserID = UserID;

        }

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            try
            {
                this.Tag = "Key_CashierHistory_Name";
                this.label1.Tag = "Key_Value";
                this.label10.Tag = "Key_VoucherExceptionsInOutColon";
                this.label12.Tag = "Key_ExpiredColon";
                this.label13.Tag = "Key_VoidCancelledColon";
                this.label14.Tag = "Key_CoinHopperFloat";
                this.label15.Tag = "Key_CoinFillsColon";
                this.label16.Tag = "Key_CoinRefunds";
                this.label17.Tag = "Key_VoidVouchersColon";
                this.label18.Tag = "Key_OfflineVoucherColon";
                this.label19.Tag = "Key_ActiveVouchersColon";
                this.label2.Tag = "Key_Quantity";
                this.label20.Tag = "Key_CashableVouchersOutColon";
                this.label21.Tag = "Key_CashableVouchersInColon";
                this.label22.Tag = "Key_Quantity";
                this.label23.Tag = "Key_Value";
                this.label24.Tag = "Key_NonCashableVoucherOutColon";
                this.label25.Tag = "Key_NonCashableVoucherInColon";
                this.label26.Tag = "Key_VoucherLiabilityColon";
                this.label27.Tag = "Key_FromColon";
                this.label28.Tag = "Key_ToColon";
                this.label3.Tag = "Key_VouchersInColon";
                this.label4.Tag = "Key_VouchersOutColon";
                this.label5.Tag = "Key_AttendantPaysColon";
                this.label6.Tag = "Key_JackpotsColon";
                this.label7.Tag = "Key_ProgressiveJackpotsColon";
                this.label8.Tag = "Key_VoidTransactionsColon";
                this.label9.Tag = "Key_ShortpayColon";
                this.lblRoute.Tag = "Key_RouteColon";
                this.lblUser.Tag = "Key_UsersColon";
                this.grpCashDeskTransactions.Tag = "Key_CashDeskTransactions";
                this.grpGamingFloorTransactions.Tag = "Key_GamingFloorTransactions";
                this.grpCriteria.Tag = "Key_Criteria";
                this.btnCashDeskMovement.Tag = "Key_CashDeskMovement";
                this.btnDetails.Tag = "Key_Details";
                this.btnProcess.Tag = "Key_Process";
                this.btnRefresh.Tag = "Key_Refresh";
                this.btnReconciliation.Tag = "Key_CashDeskReconciliation";
                this.btnSystemBalancing.Tag = "Key_SystemBalancing";
                this.ResolveResources();
            }
            catch (Exception)
            {
                
              //
            }
        }

        #region Events

        private void CashDeskManager_Load(object sender, EventArgs e)
        {
            try
            {
                this.ResolveResources();
                dtpFrom.Value = DateTime.Now.AddDays(-1);
                GetSettings();
                oBusiness.GetSite_Code_Name(_SITEID, out  _SITECODE, out  _SITENAME);
                this.Text = this.GetResourceTextByKey("Key_CashierHistory_Name") + "[" + _SITECODE + ", " + _SITENAME + "]";
                LogManager.WriteLog("[" + _SITECODE + ", " + _SITENAME + "]", LogManager.enumLogLevel.Info);


                LogManager.WriteLog("SiteCulture: " + ExtensionMethods.CurrentSiteCulture, LogManager.enumLogLevel.Info);

                PopulateUserDetails();
                PopulateRouteDetails();
                if (ExtensionMethods.CurrentSiteCulture == "it-IT")
                {
                    //disable Promo,Cashable,NonCashable
                    //label11.Visible = false;
                    label24.Visible = false;
                    label25.Visible = false;

                    //txtPromoCashable.Visible = false;
                    //txtPromoCashableQty.Visible = false;
                    //btnPromoCashable.Visible = false;

                    txtNonCashableVoucherIn.Visible = false;
                    txtNonCashableVoucherInQty.Visible = false;
                    chkNonCashableVoucherIn.Visible = false;

                    txtNonCashableVoucherOut.Visible = false;
                    txtNonCashableVoucherOutQty.Visible = false;
                    chkNonCashableVoucherout.Visible = false;

                }
                _iNoofRecords = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CDM_RecordsCount"]);

                if (_iNoofRecords < 1)
                    _iNoofRecords = 1000;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                _iNoofRecords = 1000;
            }
        }

        private void PopulateRouteDetails()
        {
            try
            {
                List<CRMGetRoutesBySiteID> Routes = oBusiness.GetRoutes(this._SITEID);

                Routes.Insert(0, new CRMGetRoutesBySiteID
                {
                    Active = true,
                    CanDelete = '0',
                    Modified = '0',
                    Route_Name = this.GetResourceTextByKey("Key_AllCriteria"), //"--ALL--",
                    Route_No = 0,
                    Site_ID = this._SITEID
                });

                cboRoutes.DataSource = Routes;
                cboRoutes.DisplayMember = "Route_Name";
                cboRoutes.ValueMember = "Route_No";
                cboRoutes.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void PopulateUserDetails()
        {
            try
            {
                cboUsers.DataSource = oBusiness.GetUserDetails(this._SITEID);
                cboUsers.DisplayMember = "UserName";
                cboUsers.ValueMember = "SecurityUserID";
                cboUsers.SelectedValue = 0;
            }
            catch(Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
              
            if (dtpFrom.Value > dtpTo.Value)
            {
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CDM_FROMDT_GREATER_TODT"), this.Text);
                return;
            }

            if (dtpFrom.Value > System.DateTime.Now)
            {
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CDM_FROMDT_GREATER_CURRENT"), this.Text);
                return;
            }

            if (dtpTo.Value > System.DateTime.Now)
            {
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CDM_TODT_GREATER_CURRENT"), this.Text);
                return;
            }

             //lstTicketExcep = LoadCashDeskDetails();
                //LoadData();
                LoadCashierTransactionData();
                Isrefreshed = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpFrom.Value > dtpTo.Value)
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CDM_FROMDT_GREATER_TODT"), this.Text);
                    return;
                }

                if (dtpFrom.Value > System.DateTime.Now)
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CDM_FROMDT_GREATER_CURRENT"), this.Text);
                    return;
                }

                if (dtpTo.Value > System.DateTime.Now)
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CDM_TODT_GREATER_CURRENT"), this.Text);
                    return;
                }
                if (!Isrefreshed)
                    LoadCashierTransactionData();


                double TotalCount = 0;

                if (chkVouchersClaimed.Checked)
                    TotalCount += objResult.CDPaidCount.Value;

                if (chkVouchersPrinted.Checked)
                    TotalCount += objResult.CDPrintedCount.Value;

                if (chkHandpays.Checked)
                    TotalCount += objResult.HandPayCount.Value;

                if (chkShortPays.Checked)
                    TotalCount += objResult.ShortPayCount.Value;

                if (chkVoidVouchers.Checked)
                    TotalCount += objResult.VoidVoucherCount.Value;

                if (chkJackpots.Checked)
                    TotalCount += objResult.JackpotCount.Value;

                if (chkProgressiveJackpots.Checked)
                    TotalCount += objResult.ProgCount.Value;

                if (chkVoidTransactions.Checked)
                    TotalCount += objResult.VoidCount.Value;

                if (chkMachineVouchersIn.Checked)
                    TotalCount += objResult.MCPaidCount.Value;

                if (chkMachineVouchersOut.Checked)
                    TotalCount += objResult.MCPrintCount.Value;

                if (chkActive.Checked)
                    TotalCount += objResult.ActiveCashableVoucherCount.Value;

                if (chkVoidCancel.Checked)
                {
                    TotalCount += objResult.VoidTicketsCount.Value;
                    TotalCount += objResult.CancelledCount.Value;
                }

                if (chkExpired.Checked)
                    TotalCount += objResult.ExpiredCount.Value;

                if (chkException.Checked)
                {
                    TotalCount += objResult.TicketInExceptionCount.Value;
                    TotalCount += objResult.TicketOutExceptionCount.Value;
                }

                if (chkLiability.Checked)
                    TotalCount += objResult.CashableVoucherLiabilityCount.Value;

                //if (chkPromo.Checked)
                //    TotalCount += objResult.PromoCashableCount.Value;

                if (chkNonCashableVoucherIn.Checked)
                    TotalCount += objResult.NonCashableINCount.Value;

                if (chkNonCashableVoucherout.Checked)
                    TotalCount += objResult.NonCashableOutCount.Value;

                if (chkOfflineVoucher.Checked)
                    TotalCount += objResult.OfflineVoucherCount.Value;

                if (TotalCount == 0)
                {
                   this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_NO_DATA"), this.Text);
                    return;
                }
                else if (TotalCount > _iNoofRecords)
                {
                    if (this.ShowQuestionMessageBox( this.GetResourceTextByKey(1, "MSG_TOO_MANYDATA"), this.Text) == DialogResult.No)
                        return;
                }

                DataSet Otemp = new DataSet();
                //string strFilter = GetDetailsFilter();


                frmDetailsScreen DScreen = null;
                //if (strFilter == "-1")
                //{
                //    MessageBox.Show("There are no records to display.", "Bally MultiConnect - Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                Otemp = oBusiness.GetCashierHistory_Details(dtpFrom.Value, dtpTo.Value, _SITEID,
                    ((CRMGetRoutesBySiteID)cboRoutes.SelectedItem) != null ? ((CRMGetRoutesBySiteID)cboRoutes.SelectedItem).Route_No : 0, Convert.ToInt32(((UserDetailsBySiteResult)cboUsers.SelectedItem).SecurityUserID),
                        chkVouchersClaimed.Checked,
                        chkVouchersPrinted.Checked,
                        chkHandpays.Checked,
                        chkShortPays.Checked,
                        chkVoidVouchers.Checked,
                        chkJackpots.Checked,
                        chkProgressiveJackpots.Checked,
                        chkVoidTransactions.Checked,
                        chkMachineVouchersIn.Checked,
                        chkMachineVouchersOut.Checked,
                        chkActive.Checked,
                        chkVoidCancel.Checked,
                        chkExpired.Checked,
                        chkException.Checked,
                        chkLiability.Checked,
                        false,
                        chkNonCashableVoucherIn.Checked,
                        chkNonCashableVoucherout.Checked,
                        chkOfflineVoucher.Checked
                        );


                if (Otemp == null || Otemp.Tables.Count == 0 || Otemp.Tables[0].Rows.Count == 0)
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CDM_NO_RECORDS"), this.Text);
                    return;
                }
                DScreen = new frmDetailsScreen(Otemp, dtpFrom.Value, dtpTo.Value, _SITENAME, GetFooterText(),_UserID);
                DScreen.ShowDialog(this);
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }

        }
        private string GetFooterText()
        {

            try
            {
                string sFooterText = string.Empty;

                sFooterText += chkVouchersClaimed.Checked == true ? "[Vouchers Claimed]" : "";
                sFooterText += chkVouchersPrinted.Checked == true ? " [Vouchers Issued]" : "";
                sFooterText += chkHandpays.Checked == true ? " [Attendant pay]" : "";
                sFooterText += chkJackpots.Checked == true ? " [Jackpot pays]" : "";
                sFooterText += chkProgressiveJackpots.Checked == true ? " [Progressive pays]" : "";
                sFooterText += chkVoidTransactions.Checked == true ? " [Void Transactions]" : "";
                sFooterText += chkMachineVouchersIn.Checked == true ? " [Cashable Voucher In]" : "";
                sFooterText += chkMachineVouchersOut.Checked == true ? " [Cashable Voucher Out]" : "";
                sFooterText += chkActive.Checked == true ? " [Active Vouchers]" : "";
                sFooterText += chkVoidCancel.Checked == true ? " [Void/Cancelled Vouchers]" : "";
                sFooterText += chkException.Checked == true ? " [Exception Vouchers]" : "";
                sFooterText += chkExpired.Checked == true ? " [Expired Vouchers]" : "";
                sFooterText += chkLiability.Checked == true ? " [Liability Vouchers]" : "";
                sFooterText += chkNonCashableVoucherIn.Checked == true ? " [Non Cashable Voucher In]" : "";
                sFooterText += chkNonCashableVoucherout.Checked == true ? " [Non Cashable Voucher Out]" : "";
                sFooterText += chkVoidVouchers.Checked == true ? " [Void Vouchers]" : "";
                sFooterText += chkShortPays.Checked == true ? " [Shortpay]" : "";
                sFooterText += chkOfflineVoucher.Checked == true ? "[Offline Voucher]" : "";
                return sFooterText;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return "";
            }
        }

        private string GetDetailsFilter()
        {
            try
            {
                List<string> sFilter = new List<string>();

                if (chkVouchersClaimed.Checked)
                    sFilter.Add("Summary_Type='CDPaidAmount'");

                if (chkVouchersPrinted.Checked)
                    sFilter.Add("Summary_Type='CDPrintedAmount'");

                if (chkHandpays.Checked)
                    sFilter.Add("Summary_Type='HandPayAmount'");

                if (chkShortPays.Checked)
                    sFilter.Add("Summary_Type='ShortPayAmount'");

                if (chkJackpots.Checked)
                    sFilter.Add("Summary_Type='JackpotAmount'");

                if (chkProgressiveJackpots.Checked)
                    sFilter.Add("Summary_Type='ProgAmount'");

                if (chkVoidTransactions.Checked)
                    sFilter.Add("Summary_Type='VoidAmount'");

                if (chkMachineVouchersIn.Checked)
                    sFilter.Add("Summary_Type='MCPaidAmount'");

                if (chkMachineVouchersOut.Checked)
                    sFilter.Add("Summary_Type='MCPrintAmount'");

                if (chkActive.Checked)
                    sFilter.Add("Summary_Type='ActiveCashableVoucherAmount'");

                if (chkVoidCancel.Checked)
                    sFilter.Add("Summary_Type='VoidTicketsAmount'");
                if (chkVoidCancel.Checked)
                    sFilter.Add("Summary_Type='CancelledAmount'");

                if (chkExpired.Checked)
                    sFilter.Add("Summary_Type='ExpiredAmount'");

                if (chkException.Checked)
                    sFilter.Add("Summary_Type='TicketInExceptionAmount'");

                if (chkException.Checked)
                    sFilter.Add("Summary_Type='TicketOutExceptionAmount'");

                if (chkLiability.Checked)
                    sFilter.Add("Summary_Type='CashableVoucherLiabilityAmount'");

                if (chkNonCashableVoucherIn.Checked)
                    sFilter.Add("Summary_Type='NonCashableINAmount'");

                if (chkNonCashableVoucherout.Checked)
                    sFilter.Add("Summary_Type='NonCashableOutAmount'");

                if (chkVoidVouchers.Checked)
                    sFilter.Add("Summary_Type='VoidVoucherAmount'");

                if (sFilter.Count == 19)
                    return "";
                else if (sFilter.Count == 0)
                    return "-1";
                else
                    return string.Join(" OR ", sFilter);
            }
            catch (Exception ex)
            {
               ExceptionManager.Publish(ex);
               return "";
            }
        }

        #endregion

        #region Internal Methods

        void LoadCashierTransactionData()
        {

            try
            {
                dsCashierHistory = oBusiness.GetCashierHistory(dtpFrom.Value, dtpTo.Value, _SITEID,
                        ((CRMGetRoutesBySiteID)cboRoutes.SelectedItem) != null ? ((CRMGetRoutesBySiteID)cboRoutes.SelectedItem).Route_No : 0, Convert.ToInt32(((UserDetailsBySiteResult)cboUsers.SelectedItem).SecurityUserID));

                DataRow dr = dsCashierHistory.Tables[0].Select(string.Format("Summary_Type='{0}'", "CDPaidAmount"))[0];
                txtVouchersClaimed.Text = Convert.ToDecimal(dr["Amount"]).GetUniversalCurrencyFormat();
                txtVouchersClaimedQty.Text = dr["Count_Summary"].ToString();

                dr = dsCashierHistory.Tables[0].Select(string.Format("Summary_Type='{0}'", "CDPrintedAmount"))[0];
                txtVouchersPrinted.Text = Convert.ToDecimal(dr["Amount"]).GetUniversalCurrencyFormat();
                txtVouchersPrintedQty.Text = dr["Count_Summary"].ToString();

                dr = dsCashierHistory.Tables[0].Select(string.Format("Summary_Type='{0}'", "HandPayAmount"))[0];
                txtHandpays.Text = Convert.ToDecimal(dr["Amount"]).GetUniversalCurrencyFormat();
                txtHandpaysQty.Text = dr["Count_Summary"].ToString();

                dr = dsCashierHistory.Tables[0].Select(string.Format("Summary_Type='{0}'", "ShortPayAmount"))[0];
                txtShortPays.Text = Convert.ToDecimal(dr["Amount"]).GetUniversalCurrencyFormat();
                txtShortPaysQty.Text = dr["Count_Summary"].ToString();

                dr = dsCashierHistory.Tables[0].Select(string.Format("Summary_Type='{0}'", "JackpotAmount"))[0];
                txtJackpots.Text = Convert.ToDecimal(dr["Amount"]).GetUniversalCurrencyFormat();
                txtJackpotsQty.Text = dr["Count_Summary"].ToString();

                dr = dsCashierHistory.Tables[0].Select(string.Format("Summary_Type='{0}'", "ProgAmount"))[0];
                txtProgressiveJackpots.Text = Convert.ToDecimal(dr["Amount"]).GetUniversalCurrencyFormat();
                txtProgressiveJackpotsQty.Text = dr["Count_Summary"].ToString();

                dr = dsCashierHistory.Tables[0].Select(string.Format("Summary_Type='{0}'", "VoidAmount"))[0];
                txtVoidTransactions.Text = Convert.ToDecimal(dr["Amount"]).GetUniversalCurrencyFormat();
                txtVoidTransactionsQty.Text = dr["Count_Summary"].ToString();

                dr = dsCashierHistory.Tables[0].Select(string.Format("Summary_Type='{0}'", "MCPaidAmount"))[0];
                txtMachineVouchersIn.Text = Convert.ToDecimal(dr["Amount"]).GetUniversalCurrencyFormat();
                txtMachineVouchersInQty.Text = dr["Count_Summary"].ToString();

                dr = dsCashierHistory.Tables[0].Select(string.Format("Summary_Type='{0}'", "MCPrintAmount"))[0];
                txtMachineVouchersOut.Text = Convert.ToDecimal(dr["Amount"]).GetUniversalCurrencyFormat();
                txtMachineVouchersOutQty.Text = dr["Count_Summary"].ToString();

                dr = dsCashierHistory.Tables[0].Select(string.Format("Summary_Type='{0}'", "ActiveCashableVoucherAmount"))[0];

                txtActiveVouchers.Text = Convert.ToDecimal(dr["Amount"]).GetUniversalCurrencyFormat();
                txtActiveVouchersQty.Text = dr["Count_Summary"].ToString();

                dr = dsCashierHistory.Tables[0].Select(string.Format("Summary_Type='{0}'", "VoidTicketsAmount"))[0];
                DataRow drCancel = dsCashierHistory.Tables[0].Select(string.Format("Summary_Type='{0}'", "CancelledAmount"))[0];
                txtVoid.Text = Convert.ToDecimal(dr["Amount"]).GetUniversalCurrencyFormat() + "/" + Convert.ToDecimal(drCancel["Amount"]).GetUniversalCurrencyFormat();
                txtVoidQty.Text = dr["Count_Summary"].ToString() + "/" + drCancel["Count_Summary"].ToString();

                dr = dsCashierHistory.Tables[0].Select(string.Format("Summary_Type='{0}'", "ExpiredAmount"))[0];
                txtExpired.Text = Convert.ToDecimal(dr["Amount"]).GetUniversalCurrencyFormat();
                txtExpiredQty.Text = dr["Count_Summary"].ToString();

                dr = dsCashierHistory.Tables[0].Select(string.Format("Summary_Type='{0}'", "TicketInExceptionAmount"))[0];
                DataRow drOut = dsCashierHistory.Tables[0].Select(string.Format("Summary_Type='{0}'", "TicketOutExceptionAmount"))[0];

                txtVoucherExceptions.Text = Convert.ToDecimal(dr["Amount"]).GetUniversalCurrencyFormat() + "/" + Convert.ToDecimal(drOut["Amount"]).GetUniversalCurrencyFormat();
                txtVoucherExceptionsQty.Text = dr["Count_Summary"].ToString() + "/" + drOut["Count_Summary"].ToString();

                dr = dsCashierHistory.Tables[0].Select(string.Format("Summary_Type='{0}'", "CashableVoucherLiabilityAmount"))[0];
                txtVoucherLiability.Text = Convert.ToDecimal(dr["Amount"]).GetUniversalCurrencyFormat();
                txtVoucherLiabilityQty.Text = dr["Count_Summary"].ToString();

                //dr = dsCashierHistory.Tables[0].Select(string.Format("Summary_Type='{0}'", "PromoCashableAmount"))[0];
                //txtPromoCashable.Text = Convert.ToDecimal(dr["Amount"]).GetUniversalCurrencyFormat();
                //txtPromoCashableQty.Text = dr["Count_Summary"].ToString();

                dr = dsCashierHistory.Tables[0].Select(string.Format("Summary_Type='{0}'", "NonCashableINAmount"))[0];
                txtNonCashableVoucherIn.Text = Convert.ToDecimal(dr["Amount"]).GetUniversalCurrencyFormat();
                txtNonCashableVoucherInQty.Text = dr["Count_Summary"].ToString();

                dr = dsCashierHistory.Tables[0].Select(string.Format("Summary_Type='{0}'", "NonCashableOutAmount"))[0];
                txtNonCashableVoucherOut.Text = Convert.ToDecimal(dr["Amount"]).GetUniversalCurrencyFormat();
                txtNonCashableVoucherOutQty.Text = dr["Count_Summary"].ToString();

                dr = dsCashierHistory.Tables[0].Select(string.Format("Summary_Type='{0}'", "VoidVoucherAmount"))[0];
                txtVoidVouchers.Text = Convert.ToDecimal(dr["Amount"]).GetUniversalCurrencyFormat();
                txtVoidVouchersQty.Text = dr["Count_Summary"].ToString();

                dr = dsCashierHistory.Tables[0].Select(string.Format("Summary_Type='{0}'", "OfflineVoucherAmount"))[0];
                txtOfflineVoucherAmount.Text = Convert.ToDecimal(dr["Amount"]).GetUniversalCurrencyFormat();
                txtOfflineVoucherCount.Text = dr["Count_Summary"].ToString();
                foreach (DataRow tdr in dsCashierHistory.Tables[0].Rows)
                {
                    switch (tdr["Summary_Type"].ToString().ToUpper())
                    {
                        case "CDPAIDAMOUNT":
                            objResult.CDPaidAmount = double.Parse(tdr["Amount"].ToString());
                            objResult.CDPaidCount = double.Parse(tdr["Count_Summary"].ToString());

                            break;
                        case "CDPRINTEDAMOUNT":
                            objResult.CDPrintedAmount = double.Parse(tdr["Amount"].ToString());
                            objResult.CDPrintedCount = double.Parse(tdr["Count_Summary"].ToString()); ;
                            break;
                        case "HANDPAYAMOUNT":
                            objResult.HandPayAmount = double.Parse(tdr["Amount"].ToString());
                            objResult.HandPayCount = double.Parse(tdr["Count_Summary"].ToString()); ;

                            break;
                        case "SHORTPAYAMOUNT":
                            objResult.ShortPayAmount = double.Parse(tdr["Amount"].ToString());
                            objResult.ShortPayCount = double.Parse(tdr["Count_Summary"].ToString()); ;
                            break;
                        case "JACKPOTAMOUNT":
                            objResult.JackpotAmount = double.Parse(tdr["Amount"].ToString());
                            objResult.JackpotCount = double.Parse(tdr["Count_Summary"].ToString()); ;
                            //chkjackpot
                            break;
                        case "PROGAMOUNT":
                            objResult.ProgAmount = double.Parse(tdr["Amount"].ToString());
                            objResult.ProgCount = double.Parse(tdr["Count_Summary"].ToString()); ;
                            //chkProghandpays
                            break;
                        case "VOIDAMOUNT":
                            objResult.VoidAmount = double.Parse(tdr["Amount"].ToString());
                            objResult.VoidCount = double.Parse(tdr["Count_Summary"].ToString()); ;
                            break;
                        case "MCPAIDAMOUNT":
                            objResult.MCPaidAmount = double.Parse(tdr["Amount"].ToString());
                            objResult.MCPaidCount = double.Parse(tdr["Count_Summary"].ToString()); ;
                            break;
                        case "MCPRINTAMOUNT":
                            objResult.MCPrintAmount = double.Parse(tdr["Amount"].ToString());
                            objResult.MCPrintCount = double.Parse(tdr["Count_Summary"].ToString()); ;
                            break;
                        case "ACTIVECASHABLEVOUCHERAMOUNT":
                            objResult.ActiveCashableVoucherAmount = double.Parse(tdr["Amount"].ToString());
                            objResult.ActiveCashableVoucherCount = double.Parse(tdr["Count_Summary"].ToString()); ;
                            break;
                        case "VOIDTICKETSAMOUNT":
                            objResult.VoidTicketsAmount = double.Parse(tdr["Amount"].ToString());
                            objResult.VoidTicketsCount = double.Parse(tdr["Count_Summary"].ToString()); ;
                            break;
                        case "VOIDVOUCHERAMOUNT":
                            objResult.VoidVoucherAmount = double.Parse(tdr["Amount"].ToString());
                            objResult.VoidVoucherCount = double.Parse(tdr["Count_Summary"].ToString()); ;
                            break;
                        case "CANCELLEDAMOUNT":
                            objResult.CancelledAmount = double.Parse(tdr["Amount"].ToString());
                            objResult.CancelledCount = double.Parse(tdr["Count_Summary"].ToString()); ;
                            break;
                        case "EXPIREDAMOUNT":
                            objResult.ExpiredAmount = double.Parse(tdr["Amount"].ToString());
                            objResult.ExpiredCount = double.Parse(tdr["Count_Summary"].ToString()); ;
                            break;
                        case "TICKETINEXCEPTIONAMOUNT":
                            objResult.TicketInExceptionAmount = double.Parse(tdr["Amount"].ToString());
                            objResult.TicketInExceptionCount = double.Parse(tdr["Count_Summary"].ToString()); ;
                            break;
                        case "TICKETOUTEXCEPTIONAMOUNT":
                            objResult.TicketOutExceptionAmount = double.Parse(tdr["Amount"].ToString());
                            objResult.TicketOutExceptionCount = double.Parse(tdr["Count_Summary"].ToString()); ;
                            break;
                        case "CASHABLEVOUCHERLIABILITYAMOUNT":
                            objResult.CashableVoucherLiabilityAmount = double.Parse(tdr["Amount"].ToString());
                            objResult.CashableVoucherLiabilityCount = double.Parse(tdr["Count_Summary"].ToString()); ;
                            break;
                        case "PROMOCASHABLEAMOUNT":
                            objResult.PromoCashableAmount = double.Parse(tdr["Amount"].ToString());
                            objResult.PromoCashableCount = double.Parse(tdr["Count_Summary"].ToString()); ;
                            break;
                        case "NONCASHABLEINAMOUNT":
                            objResult.NonCashableINAmount = double.Parse(tdr["Amount"].ToString());
                            objResult.NonCashableINCount = double.Parse(tdr["Count_Summary"].ToString()); ;
                            break;
                        case "NONCASHABLEOUTAMOUNT":
                            objResult.NonCashableOutAmount = double.Parse(tdr["Amount"].ToString());
                            objResult.NonCashableOutCount = double.Parse(tdr["Count_Summary"].ToString()); ;
                            break;
                        case "OFFLINEVOUCHERAMOUNT":
                            objResult.OfflineVoucherAmount = double.Parse(tdr["Amount"].ToString());
                            objResult.OfflineVoucherCount = double.Parse(tdr["Count_Summary"].ToString()); ;
                            break;
                        default:
                            break;
                    }
                }
            }
            catch(Exception ex)
            {
                 ExceptionManager.Publish(ex);
            }

        }

        #endregion

        private void btnReconciliation_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;


            try
            {
                DataSet CashdeskDetails = oBusiness.GetCashDeskReconcilationDetails(dtpFrom.Value, dtpTo.Value, _SITEID,
                    ((CRMGetRoutesBySiteID)cboRoutes.SelectedItem).Route_No, Convert.ToInt32(((UserDetailsBySiteResult)cboUsers.SelectedItem).SecurityUserID));

//Commented to open Report even if No Records Found similar to other Reports
                //if (CashdeskDetails.Tables[0].Rows.Count == 0)
                //{
                //    LogManager.WriteLog("No Records found for the selected criteria - Return", LogManager.enumLogLevel.Info);

                //   this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CDM_NO_RECORDS_FOR_SELECT"), this.Text);

                //    return;
                //}
//Added UserName,RouteName to pass values to report
                if (cboUsers.Items.Count > 0)
                    UserName = cboUsers.Text;

                if (cboRoutes.Items.Count > 0)
                    RouteName = cboRoutes.Text;

                clsSPParams spParams = new clsSPParams();
                spParams.StartDate = dtpFrom.Value.ToString();
                spParams.EndDate = dtpTo.Value.ToString();
                spParams.RouteNo = ((CRMGetRoutesBySiteID)cboRoutes.SelectedItem).Route_No;
                spParams.UserNo =  Convert.ToInt32(((UserDetailsBySiteResult)cboUsers.SelectedItem).SecurityUserID);
                spParams.SiteName = _SITENAME;
                spParams.RouteName = RouteName;
                spParams.UserName = UserName;

                spParams.ReportFilterDateFormat = SettingsEntity.ReportDateTimeFormat;
                spParams.ReportDataDateAloneFormat = SettingsEntity.ReportDataDateAloneFormat;
                spParams.ReportDataDateNTimeFormat = SettingsEntity.ReportDataDateNTimeFormat;
                spParams.ReportPrintDateTimeFormat = SettingsEntity.ReportPrintDateTimeFormat;

                if (CashdeskDetails.Tables.Count > 0)
                {
                     BMC.ReportViewer.RDLReportViewer.Instance.LoadLocalReport(Application.StartupPath + "\\BMC.ClientReports\\ENT_CashDeskReconicilationReport.rdl", "dsCashDeskReconicilation", CashdeskDetails, this.GetResourceTextByKey("Key_RT_DailyCashDeskReconciliationReport"), "ENT_CashDeskReconicilationReport", spParams);
                }
                else
                {
                    LogManager.WriteLog("No data found for user - " + UserName, LogManager.enumLogLevel.Info);
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CDM_NO_RECORDS"), this.Text);
                }

                //using (ReportViewer cReportViewer = new ReportViewer())
                //{
                //    LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);

                //    cReportViewer.ShowCashDeskReconcilationReport(CashdeskDetails, dtpFrom.Value, dtpTo.Value, _SITENAME,
                //         ((CRMGetRoutesBySiteID)cmbRoutes.SelectedItem).Route_No, Convert.ToInt32(((UserDetailsBySiteResult)cmbUsers.SelectedItem).SecurityUserID));
                //    cReportViewer.ShowDialog(this);

                //}
                //Cursor.Current = Cursors.Default;
                LogManager.WriteLog("ShowCashDeskReconcilationReport Successfull", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                Cursor.Current = Cursors.Default;
            }

        }

        private void btnSystemBalancing_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                DataSet SystemBalancingDetails = oBusiness.GetSystemBalancingDetails(dtpFrom.Value, dtpTo.Value, _SITEID,
                      ((CRMGetRoutesBySiteID)cboRoutes.SelectedItem).Route_No, Convert.ToInt32(((UserDetailsBySiteResult)cboUsers.SelectedItem).SecurityUserID));

                LogManager.WriteLog("Fetching report data from database...", LogManager.enumLogLevel.Info);

                //If no records then also should open the Report and display No Records Found similar to other reports
                //if (SystemBalancingDetails.Tables[0].Rows.Count == 0)
                //{
                //    LogManager.WriteLog("No Records found for the selected criteria - Return", LogManager.enumLogLevel.Info);
                //    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CDM_NO_RECORDS_FOR_SELECT"), this.Text);
                //    return;
                //}

               

                if (cboUsers.Items.Count > 0)
                    UserName = cboUsers.Text;

                if (cboRoutes.Items.Count > 0)
                    RouteName = cboRoutes.Text;


                clsSPParams spParams = new clsSPParams();

                spParams.ReportFilterDateFormat = SettingsEntity.ReportDateTimeFormat;
                spParams.ReportDataDateAloneFormat = SettingsEntity.ReportDataDateAloneFormat;
                spParams.ReportDataDateNTimeFormat = SettingsEntity.ReportDataDateNTimeFormat;
                spParams.ReportPrintDateTimeFormat = SettingsEntity.ReportPrintDateTimeFormat;

                spParams.dtStartDate = dtpFrom.Value;
                spParams.dtEndDate = dtpTo.Value;
                spParams.RouteNo = ((CRMGetRoutesBySiteID)cboRoutes.SelectedItem).Route_No;
                spParams.UserNo = Convert.ToInt32(((UserDetailsBySiteResult)cboUsers.SelectedItem).SecurityUserID);
                spParams.SiteName = _SITENAME;
                spParams.SITE = _SITEID;
                spParams.RouteName = RouteName;
                spParams.UserName = UserName;
                //BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_Report_BatchWinLoss", "Batch Win Loss Report", "ENT_BatchWinLossReport", spParams, false);

                BMC.ReportViewer.RDLReportViewer.Instance.LoadLocalReport(Application.StartupPath + "\\BMC.ClientReports\\ENT_SystemBalancing.rdl", "SystemBalancingDetails", SystemBalancingDetails, "Consolidated System Balancing Reportt", "ENT_SystemBalancing", spParams);

            //    BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_REPORT_DailyCashDeskCollectionConsolidated", "Consolidated System Balancing Report", "ENT_SystemBalancing", spParams, false);

                //using (ReportViewer cReportViewer = new ReportViewer())
                //{
                //    LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);

                //    cReportViewer.ShowSystemBalancingReport(SystemBalancingDetails, dtpFrom.Value, dtpTo.Value, _SITENAME, ((CRMGetRoutesBySiteID)cmbRoutes.SelectedItem).Route_No, Convert.ToInt32(((UserDetailsBySiteResult)cmbUsers.SelectedItem).SecurityUserID));

                //    cReportViewer.ShowDialog(this);
                //}
                Cursor.Current = Cursors.Default;

                LogManager.WriteLog("ShowSystemBalancingReport Successfull", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnCashDeskMovement_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                LogManager.WriteLog("Fetching report data from database...", LogManager.enumLogLevel.Info);

                string sRegion = oBusiness.GetRegion_Site(_SITEID);

                DataSet CashdeskDetails = oBusiness.GetCashDeskMovementDetails(dtpFrom.Value, dtpTo.Value, _SITEID, sRegion,
                      ((CRMGetRoutesBySiteID)cboRoutes.SelectedItem).Route_No, Convert.ToInt32(((UserDetailsBySiteResult)cboUsers.SelectedItem).SecurityUserID));



                if (CashdeskDetails.Tables[0].Rows.Count == 0)
                {
                    LogManager.WriteLog("No Records found for the selected criteria - Return", LogManager.enumLogLevel.Info);
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CDM_NO_RECORDS_FOR_SELECT"), this.Text);
                    return;
                }


                //if (sRegion == "US" || sRegion == "AR")
                //{
                //    clsSPParams spParams = new clsSPParams();
                //    spParams.StartDate = dtpFrom.Value.ToString();
                //    spParams.EndDate = dtpTo.Value.ToString();
                //    spParams.RouteNo = ((CRMGetRoutesBySiteID)cmbRoutes.SelectedItem).Route_No;
                //    spParams.UserId =  Convert.ToInt32(((UserDetailsBySiteResult)cmbUsers.SelectedItem).SecurityUserID);
                //    spParams.SiteName = _SITENAME;
                //    spParams.RegionName = sRegion;
                //    BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_REPORT_DailyCashDesk", "Cash Desk Movement Report", "ENT_CashDeskMovementUSReport", spParams, false);


                    //using (ReportViewer cReportViewer = new ReportViewer())
                    //{
                    //    LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);

                    //    cReportViewer.ShowCashDeskMovementUSReport(CashdeskDetails, dtpFrom.Value, dtpTo.Value, _SITENAME, _SITEID, sRegion,
                    //        ((CRMGetRoutesBySiteID)cmbRoutes.SelectedItem).Route_No, Convert.ToInt32(((UserDetailsBySiteResult)cmbUsers.SelectedItem).SecurityUserID));
                    //    cReportViewer.ShowDialog(this);
                    //}
               // }
                //else if (sRegion == "UK")
                //{
                //    using (ReportViewer cReportViewer = new ReportViewer())
                //    {
                //        LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);

                //        cReportViewer.ShowCashDeskMovementReport(CashdeskDetails, dtpFrom.Value, dtpTo.Value, _SITENAME, _SITEID,
                //            ((CRMGetRoutesBySiteID)cmbRoutes.SelectedItem).Route_No, Convert.ToInt32(((UserDetailsBySiteResult)cmbUsers.SelectedItem).SecurityUserID));
                //        cReportViewer.ShowDialog(this);
                //    }
                //}
              //  else
//{
                    clsSPParams spParams = new clsSPParams();
                    spParams.StartDate = dtpFrom.Value.ToString();
                    spParams.EndDate = dtpTo.Value.ToString();
                    spParams.RouteNo = ((CRMGetRoutesBySiteID)cboRoutes.SelectedItem).Route_No;
                    spParams.UserNo =  Convert.ToInt32(((UserDetailsBySiteResult)cboUsers.SelectedItem).SecurityUserID);
                    spParams.Site = _SITEID;
                    spParams.RegionName = sRegion;
                    spParams.SiteName = _SITENAME;

                    spParams.ReportFilterDateFormat = SettingsEntity.ReportDateTimeFormat;
                    spParams.ReportDataDateAloneFormat = SettingsEntity.ReportDataDateAloneFormat;
                    spParams.ReportDataDateNTimeFormat = SettingsEntity.ReportDataDateNTimeFormat;
                    spParams.ReportPrintDateTimeFormat = SettingsEntity.ReportPrintDateTimeFormat;

                    BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_REPORT_DailyCashDesk", "Cash Desk Movement Report", "ENT_CashDeskMovement", spParams, false);
                    
                    //using (ReportViewer cReportViewer = new ReportViewer())
                    //{
                    //    LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);

                    //    cReportViewer.ShowCashDeskMovementReport(CashdeskDetails, dtpFrom.Value, dtpTo.Value, _SITENAME, _SITEID,
                    //        ((CRMGetRoutesBySiteID)cmbRoutes.SelectedItem).Route_No, Convert.ToInt32(((UserDetailsBySiteResult)cmbUsers.SelectedItem).SecurityUserID));
                    //    cReportViewer.ShowDialog(this);
                    //}
              //  }
                Cursor.Current = Cursors.Default;
                LogManager.WriteLog("ShowCashDeskMovementReport Successfull", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                Cursor.Current = Cursors.Default;
            }
        }

        //private void btnActiveVouchers_Click(object sender, EventArgs e)
        //{
        //    Cursor.Current = Cursors.WaitCursor;

        //    try
        //    {
        //        if (txtActiveVouchersQty.Text != "0")
        //        {
        //            DetailsScreen objActiveDetails = new DetailsScreen("ACTIVE", dtpFrom.Value, dtpTo.Value, _SITENAME, _SITEID);
        //            objActiveDetails.ShowDialog(this);
        //        }
        //        else
        //        {
        //            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CDM_NO_RECORDS_FOR_SELECT"), this.Text);
        //            return;
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //    }
        //    Cursor.Current = Cursors.Default;
        //}

        //private void btnVoidCancelled_Click(object sender, EventArgs e)
        //{
        //    Cursor.Current = Cursors.WaitCursor;
        //    try
        //    {
        //        if (txtVoidQty.Text != "0")
        //        {
        //            DetailsScreen objVoidCancelDetails = new DetailsScreen("VOIDCANCEL", dtpFrom.Value, dtpTo.Value, _SITENAME, _SITEID);
        //            objVoidCancelDetails.ShowDialog(this);
        //        }
        //        else
        //        {
        //           this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CDM_NO_RECORDS_FOR_SELECT"), this.Text);
        //            return;
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //      ExceptionManager.Publish(ex);
        //    }
        //    Cursor.Current = Cursors.Default;
        //}

        //private void btnExpired_Click(object sender, EventArgs e)
        //{
        //    Cursor.Current = Cursors.WaitCursor;
        //    try
        //    {
        //        if (txtExpiredQty.Text != "0")
        //        {
        //            DetailsScreen objExpiredDetails = new DetailsScreen("EXPIRED", dtpFrom.Value, dtpTo.Value, _SITENAME, _SITEID);
        //            objExpiredDetails.ShowDialog(this);
        //        }
        //        else
        //        {
        //            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CDM_NO_RECORDS_FOR_SELECT"), this.Text);
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //    }
        //    Cursor.Current = Cursors.Default;
        //}

        //private void btnPromoCashable_Click(object sender, EventArgs e)
        //{
        //    Cursor.Current = Cursors.WaitCursor;
        //    try
        //    {
        //        if (txtPromoCashableQty.Text != "0")
        //        {
        //            DetailsScreen objPromoCashable = new DetailsScreen("PROMO", dtpFrom.Value, dtpTo.Value, _SITENAME, _SITEID);
        //            objPromoCashable.ShowDialog(this);
        //        }
        //        else
        //        {
        //            MessageBox.Show("There are no records available for the selected criteria.", "Bally MultiConnect - Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            return;
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //    }
        //    Cursor.Current = Cursors.Default;
        //}

        //private void btnVoucherException_Click(object sender, EventArgs e)
        //{
        //    Cursor.Current = Cursors.WaitCursor;
        //    try
        //    {
        //        if (txtVoucherExceptionsQty.Text != "0")
        //        {
        //            DetailsScreen objVoucherException = new DetailsScreen("EXCEP", dtpFrom.Value, dtpTo.Value, _SITENAME, _SITEID);
        //            objVoucherException.ShowDialog(this);
        //        }
        //        else
        //        {
        //            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CDM_NO_RECORDS_FOR_SELECT"), this.Text);
        //            return;
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //    }
        //    Cursor.Current = Cursors.Default;
        //}

        //private void btnVoucherLiability_Click(object sender, EventArgs e)
        //{
        //    Cursor.Current = Cursors.WaitCursor;
        //    try
        //    {
        //        if (txtVoucherLiabilityQty.Text != "0")
        //        {
        //            DetailsScreen objLiability = new DetailsScreen("LIABILITY", dtpFrom.Value, dtpTo.Value, _SITENAME, _SITEID);
        //            objLiability.ShowDialog(this);
        //        }
        //        else
        //        {
        //            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CDM_NO_RECORDS_FOR_SELECT"), this.Text);
        //            return;
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //    }
        //    Cursor.Current = Cursors.Default;
        //}

        private void chkAllCD_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                bool bChecked = chkAllCD.Checked;
                chkVouchersClaimed.Checked = bChecked;
                chkVouchersPrinted.Checked = bChecked;
                chkHandpays.Checked = bChecked;
                chkShortPays.Checked = bChecked;
                chkJackpots.Checked = bChecked;
                chkVoidTransactions.Checked = bChecked;
                chkProgressiveJackpots.Checked = bChecked;
                chkVoidVouchers.Checked = bChecked;
                chkOfflineVoucher.Checked = bChecked;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void chkAllMC_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                bool bChecked = chkAllMC.Checked;
                chkMachineVouchersIn.Checked = bChecked;
                chkMachineVouchersOut.Checked = bChecked;
                chkNonCashableVoucherIn.Checked = bChecked;
                chkNonCashableVoucherout.Checked = bChecked;
                //chkPromo.Checked = bChecked;
                chkActive.Checked = bChecked;
                chkException.Checked = bChecked;
                chkExpired.Checked = bChecked;
                chkVoidCancel.Checked = bChecked;
                chkLiability.Checked = bChecked;
            }
            catch(Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        
        }      

        private void GetSettings()
        {
            try
            {

                EnterpriseDataContext objCDMDataContext = EnterpriseDataContextHelper.GetDataContext();
                List<rsp_GetCashierTransactionSettingsResult> lstSetting = objCDMDataContext.GetInitialSettings2().ToList();
                btnReconciliation.Visible = lstSetting[0].EnableCashdeskReconciliation.Value;
                btnCashDeskMovement.Visible = lstSetting[0].EnableCashdeskMovement.Value;
                btnSystemBalancing.Visible = lstSetting[0].EnableSystemBalancing.Value;
            }
            catch (Exception ex)
            {
                btnReconciliation.Visible = false;
                btnCashDeskMovement.Visible = false;
                btnSystemBalancing.Visible = false;
                ExceptionManager.Publish(ex);
            }
        }

        private void dtp_ValueChanged(object sender, EventArgs e)
        {
            Isrefreshed = false;
        }

        private void btnRefresh_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    dtpTo.Value = DateTime.Now;
                }
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

       

    }

    #region Extension Methods
    //public static class ExtensionMethods
    //{

    //    public static string GetUniversalCurrencyFormat(this decimal decimalvalue)
    //    {
    //        if (decimalvalue == 0)
    //            return "0";
    //        var cultureInfo = new CultureInfo("en-US");
    //        var returnString = String.Format(cultureInfo, "{0:###,##0.00}", decimalvalue);
    //        return returnString;
    //    }

    //    public static DateTime ReadDateTimeWithSeconds(this string dateValue)
    //    {
    //        return DateTime.Parse(dateValue, new CultureInfo("en-US"));
    //    }

    //    public static string ToDateTimeString(this object item)
    //    {
    //        DateTime result;
    //        if (DateTime.TryParse(item.ToString(), out result))
    //            return result.ToString("dd MMM yyyy HH:mm:ss");
    //        else
    //            return item.ToString();
    //    }
    //}
    #endregion
}
