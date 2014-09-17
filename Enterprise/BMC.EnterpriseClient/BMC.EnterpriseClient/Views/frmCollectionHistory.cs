using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;
using BMC.CoreLib.Win32;
using BMC.EnterpriseClient.Helpers;
using BMC.CoreLib.Concurrent;
using BMC.CommonLiquidation.Utilities;
using BMC.EnterpriseDataAccess;
using BMC.Common;
using BMC.Reports;
using BMC.ReportViewer;

namespace BMC.EnterpriseClient.Views
{
    public enum FormCollectionHistory_Mode
    {
        Batch,
        Week
    }

    public partial class frmCollectionHistory : Form
    {
        frmAdminUtilities _Utils = new frmAdminUtilities();
        ViewSites_DropBatchBreakDown objBiz = null;
        int COLLECTION_EDC_FORCED = 1;
        private ListViewCustomSorter _lvCustomSorter = null;
        public IExecutorService Executor { get; private set; }
        //private ListViewColumnSorter _lvwColumnSorter = null;
        private ListViewCustomSorter _customSorter = null;
        private double _payoutWeek;
        int _iBatch_ID;
        int _iSite_ID;
        int _Week_ID;
        string _WeekNo;
        string _Week;
        FormCollectionHistory_Mode _Mode;
        String _SiteName = string.Empty;
        private ToolTip UserNameToolTip;
        public string strCurrencyForviewsites { get; private set; }
        public String UserToolTipText;
        public frmCollectionHistory()
        {
            InitializeComponent();
            setTagProperty();
            //_lvwColumnSorter = new ListViewColumnSorter();
            //lst_Collectiondetails.ListViewItemSorter = _lvwColumnSorter;
            //this.SetColumnHeaderDataTypes();
            _lvCustomSorter = new ListViewCustomSorter(lst_Collectiondetails, this.ViewSiteChistory as Form);  
            strCurrencyForviewsites = new System.Globalization.RegionInfo(BMC.Common.ConfigurationManagement.ConfigManager.Read("GetDefaultCultureForCurrency")).CurrencySymbol;
        }

        private void setTagProperty()
        {
            this.clm_Hold.Tag = "Key_PercentHold";
            this.lbl_Hold.Tag = "Key_PercentHoldColon";
            this.clm_Payout.Tag = "Key_PercentPayout";
            this.lbl_Payout.Tag = "Key_PercentPayoutColon";
            this.clm_AssetNo.Tag = "Key_AssetNo";
            this.clm_Attendantpay.Tag = "Key_Attendantpay";
            this.lbl_AttnPayVar.Tag = "Key_AttnPayVarColon";
            this.lbl_AttnPay.Tag = "Key_AttnPayColon";
            this.clm_AttnPayMeterProgressive.Tag = "Key_AttnPayMeterProgressive";
            this.clm_AttnPayProgVar.Tag = "Key_AttnPayProgVar";
            this.clm_AttnPayProgressive.Tag = "Key_AttnPayProgressive";
            this.clm_AttnPayVar.Tag = "Key_AttnPayVar";
            this.clm_Bills.Tag = "Key_Bills";
            this.clm_BillsVar.Tag = "Key_BillsVar";
            this.lbl_BillsVar.Tag = "Key_BillsVarColon";
            this.btn_CashCollectionReport.Tag = "Key_CashCollectionReport";
            this.clm_CoinVar.Tag = "Key_CoinVar";
            this.lbl_CoinVar.Tag = "Key_CoinVarColon";
            this.lbl_CollectionDate.Tag = "Key_CollectionDateColon";
            this.clm_Date.Tag = "Key_Date";
            this.clm_Days.Tag = "Key_Days";
            this.lbl_DecWinLoss.Tag = "Key_DecWinLossColon";
            this.grp_DeclaredBatchDetails.Tag = "Key_DeclaredbatchDetails";
            this.clm_Description.Tag = "Key_Description";
            this.clm_Door.Tag = "Key_Door";
            this.grp_Main.Tag = "Key_DropBatch";
            this.Tag = "Key_DropBatchBreakdown";
            this.clm_Duration.Tag = "Key_Duration";
            this.btn_ExceptionSummaryReport.Tag = "Key_ExceptionSummaryReport";
            this.clm_Faults.Tag = "Key_Faults";
            this.clm_FloatRec.Tag = "Key_FloatRec";
            this.lbl_FloatRec.Tag = "Key_FloatRecColon";
            this.clmGame.Tag = "Key_Game";
            this.lbl_GamingDay.Tag = "Key_GamingDayColon";
            this.clm_GrossCoin.Tag = "Key_GrossCoin";
            this.lbl_GrossCoin.Tag = "Key_GrossCoinColon";
            this.clnHandle.Tag = "Key_Handle";
            this.lbl_Handle.Tag = "Key_HandleColon";
            this.clm_HrsOn.Tag = "Key_HrsOn";
            this.clm_MeterAttnPay.Tag = "Key_MeterAttnPay";
            this.clm_MeterBills.Tag = "Key_MeterBills";
            this.clm_MeterCoin.Tag = "Key_MeterCoin";
            this.clm_MeterVoucher.Tag = "Key_MeterVoucher";
            this.clm_MeterWinLoss.Tag = "Key_MeterWinLoss";
            this.lbl_NetCoin.Tag = "Key_NetCoinColon";
            this.clm_NetCoin.Tag = "Key_NetCoinColon";
            this.lbl_Notes.Tag = "Key_BillsColon";
            this.clm_Pos.Tag = "Key_Pos";
            this.grp_PositionEvents.Tag = "Key_PositionEvents";
            this.clm_Power.Tag = "Key_Power";
            this.lbl_ProgressiveVar.Tag = "Key_ProgressiveVarColon";
            this.lbl_Progressive.Tag = "Key_ProgressiveColon";
            this.clm_PromoCashableIn.Tag = "Key_PromoCashableIn";
            this.clm_PromoNonCashableIn.Tag = "Key_PromoNonCashableIn";
            this.lbl_Qty.Tag = "key_NoOFDrops";
            this.clm_Refills.Tag = "Key_RefillsHeader";
            this.lbl_Refills.Tag = "Key_RefillsColon";
            this.clm_Refunds.Tag = "Key_RefundsHeader";
            this.lbl_Refunds.Tag = "Key_RefundsColon";
            this.btn_SGVILiqReport.Tag = "Key_SGVILiquidationReport";
            this.clm_Shortpay.Tag = "Key_Shortpay";
            this.lbl_Shortpay.Tag = "Key_ShortpayColon";
            this.clm_Time.Tag = "Key_Time";
            this.clm_Type.Tag = "Key_Type";
            this.lbl_User.Tag = "Key_UserColon";
            this.grp_VarianceSummary.Tag = "Key_VarianceSummary";
            this.clm_VoidVoucher.Tag = "Key_VoidVoucher";
            this.clm_VoucherVar.Tag = "Key_VoucherVar";
            this.clm_Vouchers.Tag = "Key_Vouchers";
            this.lbl_VouchersVar.Tag = "Key_VouchersVarColon";
            this.lbl_Vouchers.Tag = "Key_VouchersColon";
            this.clm_WinLoss.Tag = "Key_WinLoss";
            this.clm_WinLossVar.Tag = "Key_WinLossVar";
            this.clm_Zone.Tag = "Key_Zone";
            this.lbl_route.Tag = "Key_RC_RouteName";
            
        }

        protected string CombineCurrencySymbol(string text)
        {
            return strCurrencyForviewsites+ " "+ text;
        }

        public frmCollectionHistory(int Batch_id, int Site_ID, int Week_id, FormCollectionHistory_Mode Mode, String Site_Name,string WeekNo,string Week)
            : this()
        {
            _iBatch_ID = Batch_id;
            _iSite_ID = Site_ID;
            _Week_ID = Week_id;
            _Mode = Mode;
            _SiteName = Site_Name;
            _WeekNo = WeekNo;
            _Week = Week;
            this.MinimizeBox = false;
        }
        public IViewSiteInfo ViewSiteChistory { get; set; }
        private void SetColumnHeaderDataTypes()
        {
            _customSorter = new ListViewCustomSorter(lst_Collectiondetails, this);
            Type numericComparer = typeof(NumericWrapperComparer);
            Type stringType = typeof(string);

            this.clm_Zone.Tag = stringType;
            this.clm_Pos.Tag = numericComparer;
            this.clmGame.Tag = stringType;
            this.clm_AssetNo.Tag = stringType;
            this.clm_Days.Tag = numericComparer;
            this.clm_WinLoss.Tag = numericComparer;
            this.clm_MeterWinLoss.Tag = numericComparer;
            this.clm_WinLossVar.Tag = numericComparer;
            this.clnHandle.Tag = numericComparer;
            this.clm_Payout.Tag = numericComparer;
            this.clm_Hold.Tag = numericComparer;
            this.clm_GrossCoin.Tag = numericComparer;
            this.clm_FloatRec.Tag = numericComparer;
            this.clm_Refills.Tag = numericComparer;
            this.clm_Refunds.Tag = numericComparer;
            this.clm_NetCoin.Tag = numericComparer;
            this.clm_MeterCoin.Tag = numericComparer;
            this.clm_CoinVar.Tag = numericComparer;
            this.clm_Bills.Tag = numericComparer;
            this.clm_MeterBills.Tag = numericComparer;
            this.clm_BillsVar.Tag = numericComparer;
            this.clm_Vouchers.Tag = numericComparer;
            this.clm_Shortpay.Tag = numericComparer;
            this.clm_VoidVoucher.Tag = numericComparer;
            this.clm_MeterVoucher.Tag = numericComparer;
            this.clm_VoucherVar.Tag = numericComparer;
            this.clm_Attendantpay.Tag = numericComparer;
            this.clm_MeterAttnPay.Tag = numericComparer;
            this.clm_AttnPayVar.Tag = numericComparer;
            this.clm_AttnPayProgressive.Tag = numericComparer;
            this.clm_AttnPayMeterProgressive.Tag = numericComparer;
            this.clm_AttnPayProgVar.Tag = numericComparer;
            this.clm_HrsOn.Tag = stringType;
            this.clm_Faults.Tag = numericComparer;
            this.clm_Door.Tag = numericComparer;
            this.clm_Power.Tag = numericComparer;
            this.clm_PromoCashableIn.Tag = numericComparer;
            this.clm_PromoNonCashableIn.Tag = numericComparer;
        }

        private void frmCollectionHistory_Load(object sender, EventArgs e)
        {

            try
            {
                this.ResolveResources();

                btnCollectionReport.Visible = SettingsEntity.ShowCollectionReport;

                if (SettingsEntity.SGVI_Enabled == false)
                    btn_SGVILiqReport.Visible = false;

                if (SettingsEntity.Client.ToUpper() == "SGVI" && SettingsEntity.SGVI_Enabled == true)
                {
                    btn_SGVILiqReport.Text = this.GetResourceTextByKey("Key_SGVILiquidationReport");
                    btn_CashCollectionReport.Visible = false;
                }
                else
                {
                    if (SettingsEntity.LiquidationProfitShare && _Mode == FormCollectionHistory_Mode.Batch)
                    {
                        btn_SGVILiqReport.Visible = true;
                        btn_SGVILiqReport.Text = this.GetResourceTextByKey("Key_LiquidationSummaryReport");  //Liquidation Summary Report -- cmdpsliq

                    }
                    else
                        btn_SGVILiqReport.Visible = false;
                    
                    if (FormCollectionHistory_Mode.Week == _Mode)
                    {
                        btn_CashCollectionReport.Text = this.GetResourceTextByKey("Key_WeeklyWinLossReport");
                        btn_ExceptionSummaryReport.Text = this.GetResourceTextByKey("Key_WeeklyExceptionSummaryReport");

                    }
                    else
                    {
                        btn_CashCollectionReport.Text = this.GetResourceTextByKey("Key_BatchWinLossReport");

                        btn_ExceptionSummaryReport.Text = this.GetResourceTextByKey("Key_ExceptionSummaryReport");

                    }
                }



                if (_Mode == FormCollectionHistory_Mode.Batch)
                {
                    
                    FillBatch();
                    FillBatchSummary();
                    Setsymbol();
                }
                else
                {
                   // lbl_GamingDay.Text = "Week";
                    lbl_GamingDay.Text =this.GetResourceTextByKey("Key_Week");
                   // lbl_CollectionDate.Text = "Dates";
                    lbl_CollectionDate.Text =this.GetResourceTextByKey("Key_Dates") ;
                    //lbl_User.Text = "Qty";
                    lbl_User.Text = this.GetResourceTextByKey("Key_Qty");
                    lbl_Qty.Visible = false;
                    txt_Qty.Visible = false;
                    FillWeekBreakDown();
                    FillWeekSummary();
                    Setsymbol();
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                
            }
        }
        private void lst_Collectiondetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RefreshEvents();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }
        #region "BatchBreakDown"
        void FillBatch()
        {
            objBiz = new ViewSites_DropBatchBreakDown();
             List<DropBatchBreakDown> lstCollection =null;
             BMC.CoreLib.Win32.Win32Extensions.ShowAsyncDialog(this, this.GetResourceTextByKey(1, "MSG_FETCHING_DETAILS"), this.Executor,
             (o) =>
             {
                 o.CloseOnComplete = true;
                 lstCollection = objBiz.GetDropBatchBreakDown(_iSite_ID, _iBatch_ID);
             });
             foreach (var item in lstCollection)
            {
                //lst_Collectiondetails.Items.Add(string.Format("CO,{0}#{1}#SC", item.Collection_ID, item.Installation_ID), item.Zone_Name, 0);
                ListViewItem LstItem = lst_Collectiondetails.Items.Add(string.Format("CO,{0},{1}", item.Collection_ID, item.Installation_ID), item.Zone_Name, 0);
                LstItem.Tag = item;
                LstItem.UseItemStyleForSubItems = false;

                int _payotvalue = Convert.ToInt32((item.Handle - item.MeterWinOrLoss) / (Math.Max(item.Handle, 1)) * 100);
                for (int iSeq = 0; iSeq < lst_Collectiondetails.Columns.Count - 1; iSeq++)
                {
                    LstItem.SubItems.Add("");
                }
                LstItem.Tag = item;

                int iColumnIndex = 1;
                LstItem.SubItems[iColumnIndex + 0].Text = item.PosName;
                LstItem.SubItems[iColumnIndex + 1].Text = item.MachineName;
                LstItem.SubItems[iColumnIndex + 2].Text = item.StockNo;
                LstItem.SubItems[iColumnIndex + 3].Text = item.Collection_Days.ToString();

                _Utils.AddNumericValue(item.DecWinOrLoss, LstItem.SubItems[iColumnIndex + 4]);
                _Utils.AddNumericValue(item.MeterWinOrLoss, LstItem.SubItems[iColumnIndex + 5]);
                _Utils.AddNumericValue(item.TakeVariance, LstItem.SubItems[iColumnIndex + 6]);
                _Utils.AddNumericValue(item.Handle, LstItem.SubItems[iColumnIndex + 7]);
                _Utils.AddNumericValue(item.nCasino, LstItem.SubItems[iColumnIndex + 8]);
                _Utils.AddNumericValue(item.nHold, LstItem.SubItems[iColumnIndex + 9]);
                _Utils.AddNumericValue(item.Declared_Coins, LstItem.SubItems[iColumnIndex + 10]);
                _Utils.AddNumericValue(item.DeFloat, LstItem.SubItems[iColumnIndex + 11]);
                _Utils.AddNumericValue(item.Refills, LstItem.SubItems[iColumnIndex + 12]);
                _Utils.AddNumericValue(item.Refunds, LstItem.SubItems[iColumnIndex + 13]);
                _Utils.AddNumericValue(item.Net_Coin, LstItem.SubItems[iColumnIndex + 14]);

                if (item.Datapak_ID == 0 && item.RDC_Coins == 0 && item.RDC_Notes == 0 && item.VTP == 0)
                {
                    LstItem.SubItems[iColumnIndex + 15].Text = this.GetResourceTextByKey("Key_NoDevice");
                    LstItem.SubItems[iColumnIndex + 15].ForeColor = Color.Blue;
                    LstItem.SubItems[iColumnIndex + 16].Text = this.GetResourceTextByKey("Key_NA"); //Coin Var
                    LstItem.SubItems[iColumnIndex + 16].ForeColor = Color.Blue;
                }
                else
                {

                    if (item.Collection_EDC_Status == COLLECTION_EDC_FORCED)
                    {
                        LstItem.SubItems[iColumnIndex + 15].Text = this.GetResourceTextByKey("Key_Forced");
                        LstItem.SubItems[iColumnIndex + 15].ForeColor = Color.Blue;
                    }
                    else
                    {
                        _Utils.AddNumericValue(item.RDC_Coins, LstItem.SubItems[iColumnIndex + 15]);   //RDC coin
                    }
                    _Utils.AddNumericValue(item.Coin_Var, LstItem.SubItems[iColumnIndex + 16]);  //RDC Var
                    if (item.Collection_EDC_Status == COLLECTION_EDC_FORCED)
                        LstItem.SubItems[iColumnIndex + 16].ForeColor = Color.Blue;
                }

                _Utils.AddNumericValue(item.Declared_Notes, LstItem.SubItems[iColumnIndex + 17]);  //Notes
                _Utils.AddNumericValue(item.RDC_Notes, LstItem.SubItems[iColumnIndex + 18]); //rdc Notes
                _Utils.AddNumericValue(item.Note_Var, LstItem.SubItems[iColumnIndex + 19]); //Notes variance
                _Utils.AddNumericValue(item.DecTicketBalance, LstItem.SubItems[iColumnIndex + 20]); //Declared Tickets
                _Utils.AddNumericValue(item.Shortpay, LstItem.SubItems[iColumnIndex + 21]);
                _Utils.AddNumericValue(item.Void, LstItem.SubItems[iColumnIndex + 22]);
                _Utils.AddNumericValue(item.DecTicketBalance - item.Ticket_Var, LstItem.SubItems[iColumnIndex + 23]); //RDC Tickets
                _Utils.AddNumericValue(item.Ticket_Var, LstItem.SubItems[iColumnIndex + 24]);  //Ticket Variance
                _Utils.AddNumericValue(item.DecHandpay, LstItem.SubItems[iColumnIndex + 25]);  //Declared Handpay


                if (item.Datapak_ID == 0 && item.RDC_Coins == 0 && item.RDC_Notes == 0 && item.VTP == 0)
                {
                    LstItem.SubItems[iColumnIndex + 26].Text = this.GetResourceTextByKey("Key_NA");
                    LstItem.SubItems[iColumnIndex + 26].ForeColor = Color.Blue;
                    LstItem.SubItems[iColumnIndex + 27].Text = this.GetResourceTextByKey("Key_NA");
                    LstItem.SubItems[iColumnIndex + 27].ForeColor = Color.Blue;
                }
                else
                {
                    _Utils.AddNumericValue(item.RDCHandpay, LstItem.SubItems[iColumnIndex + 26]);
                    if (item.Collection_EDC_Status == COLLECTION_EDC_FORCED) //RDC handpay
                    {
                        LstItem.SubItems[iColumnIndex + 26].ForeColor = Color.Blue; ;
                    }
                    _Utils.AddNumericValue(item.Handpay_Var, LstItem.SubItems[iColumnIndex + 27]);

                    if (item.Collection_EDC_Status == COLLECTION_EDC_FORCED) //RDC Handpay Var
                    {
                        LstItem.SubItems[iColumnIndex + 27].ForeColor = Color.Blue; ;
                    }
                }

                //Progressive
                _Utils.AddNumericValue(item.Progressive_Value_Declared, LstItem.SubItems[iColumnIndex + 28]);
                _Utils.AddNumericValue(item.Progressive_Value_Meter, LstItem.SubItems[iColumnIndex + 29]);
                _Utils.AddNumericValue(item.Progressive_Value_Variance, LstItem.SubItems[iColumnIndex + 30]);

                string TmpDurationStr = string.Empty;
                TimeSpan ts = TimeSpan.FromMilliseconds(item.Collection_Total_Power_Duration);
                TmpDurationStr = ts.Hours.ToString("00") + ":" + ts.Minutes.ToString("00");

                LstItem.SubItems[iColumnIndex + 31].Text = TmpDurationStr;
                LstItem.SubItems[iColumnIndex + 32].Text = item.Total_Fault_Events.ToString();
                LstItem.SubItems[iColumnIndex + 33].Text = item.Total_Door_Events.ToString();
                LstItem.SubItems[iColumnIndex + 34].Text = item.Total_Power_Events.ToString();

                LstItem.SubItems[iColumnIndex + 35].Text = item.PromoCashableIn.ToString();
                LstItem.SubItems[iColumnIndex + 36].Text = item.PromoNonCashableIn.ToString();


            }
        }
        void FillBatchSummary()
        {
            FillBatchSummary objSummary = null;
            BMC.CoreLib.Win32.Win32Extensions.ShowAsyncDialog(this, this.GetResourceTextByKey(1, "MSG_FETCHING_DETAILS"), this.Executor,
            (o) =>
            {
                o.CloseOnComplete = true;
                objSummary = objBiz.FillBatchSummary(_iBatch_ID);

            });
      
          //  double _Handle = Math.Round(objSummary.Handle);
            //double _handlevalue = Math.Round(objSummary.Handle);
            double _handlevalue = objSummary.Handle;
            double _payoutvalue = ((((_handlevalue) - objSummary.MeterWinOrLoss) / ((_handlevalue))) * 100);
            double nhold = (100 - _payoutvalue);
           // _payoutvalue = objSummary.nCasino;
            //_payoutvalue = objSummary.PercentageOut;

            this.Text = this.Text + string.Format(" [{0}] [" + this.GetResourceTextByKey("Key_SiteBatchRef")+ "{1}]", _SiteName, objSummary.BatchRef.Split(',')[1]);
            txt_GamingDay.Text = _Utils.GetRegionalDate(objSummary.BatchDate) + " " + TimeSpan.Parse(objSummary.Batch_Time).Hours + ":" + TimeSpan.Parse(objSummary.Batch_Time).Minutes;
            txt_CollectionDate.Text = _Utils.GetRegionalDate(objSummary.Batch_Date_Performed) + " " + +TimeSpan.Parse(objSummary.Batch_Time).Hours + ":" + TimeSpan.Parse(objSummary.Batch_Time).Minutes;
            //     Batch_Date_Performed 
            string[] User = objSummary.Batch_User_Name.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);
            string sUserName = string.Empty;
            if (User.Count() > 1)
            {
                sUserName = User[1] + "," + User[0];
            }
            else
            {
                sUserName = User[0];
            }

            txt_User.Text = sUserName.Split(',').First();
            UserToolTipText = sUserName;

            //txtCollectionTime.Text = Format(Rs.Fields("Batch_Time"), "hh:nn") ' not displayed..1
            string strGrossCoin = CombineCurrencySymbol(_Utils.GetNumericValue(objSummary.Declared_Coins, "#,##0.00", true));
            txt_GrossCoin.Text = strGrossCoin;
                //_Utils.GetNumericValue(objSummary.Declared_Coins, "#,##0.00", true);
            string strFloatRec = CombineCurrencySymbol( _Utils.GetNumericValue(objSummary.Defloat, "#,##0.00", true));
            txt_FloatRec.Text = strFloatRec;
                //_Utils.GetNumericValue(objSummary.Defloat, "#,##0.00", true);
            string strRefills = CombineCurrencySymbol(_Utils.GetNumericValue(objSummary.Refills, "#,##0.00", true));
            txt_Refills.Text = strRefills;
                //_Utils.GetNumericValue(objSummary.Refills, "#,##0.00", true);
            string strRefunds = CombineCurrencySymbol( _Utils.GetNumericValue(objSummary.Refunds, "#,##0.00", true));
            txt_Refunds.Text = strRefunds;
                //_Utils.GetNumericValue(objSummary.Refunds, "#,##0.00", true);
            string strProgressiveValue = CombineCurrencySymbol( _Utils.GetNumericValue(objSummary.Progressive_Value_Declared, "#,##0.00", true));
            txt_Progressive.Text = strProgressiveValue;
                //_Utils.GetNumericValue(objSummary.Progressive_Value_Declared, "#,##0.00", true);
            string strShortpay = CombineCurrencySymbol(_Utils.GetNumericValue(objSummary.Shortpay, "#,##0.00", true));
            txt_Shortpay.Text = strShortpay;
            //_Utils.GetNumericValue(objSummary.Shortpay, "#,##0.00", true);
            string strNetCoin = CombineCurrencySymbol( _Utils.GetNumericValue(objSummary.Net_Coin, "#,##0.00", true));
            txt_NetCoin.Text = strNetCoin;
                //_Utils.GetNumericValue(objSummary.Net_Coin, "#,##0.00", true);
            string strDeclaredNotes = CombineCurrencySymbol(_Utils.GetNumericValue(objSummary.Declared_Notes, "#,##0.00", true));
            txt_Notes.Text = strDeclaredNotes;
            //_Utils.GetNumericValue(objSummary.Declared_Notes, "#,##0.00", true);
            string strDecHandpay = CombineCurrencySymbol( _Utils.GetNumericValue(objSummary.DecHandpay, "#,##0.00", true));
            txt_AttnPay.Text = strDecHandpay;
                //_Utils.GetNumericValue(objSummary.DecHandpay, "#,##0.00", true);
            string strTicketBalance = CombineCurrencySymbol( _Utils.GetNumericValue(objSummary.DecTicketBalance, "#,##0.00", true));
            txt_Vouchers.Text =  strTicketBalance;
                //_Utils.GetNumericValue(objSummary.DecTicketBalance, "#,##0.00", true);
            string strWinLoss = CombineCurrencySymbol( _Utils.GetNumericValue(objSummary.DecWinOrLoss, "#,##0.00", true));
            txt_DecWinLoss.Text = strWinLoss;
                //_Utils.GetNumericValue(objSummary.DecWinOrLoss, "#,##0.00", true);
            string strCoinVar = CombineCurrencySymbol( _Utils.GetNumericValue(objSummary.Coin_Var, "#,##0.00", true));
            txt_CoinVar.Text = strCoinVar;
                //_Utils.GetNumericValue(objSummary.Coin_Var, "#,##0.00", true);
            string strBillsVar = CombineCurrencySymbol(_Utils.GetNumericValue(objSummary.Note_Var, "#,##0.00", true));  
            txt_BillsVar.Text = strBillsVar;
                //_Utils.GetNumericValue(objSummary.Note_Var, "#,##0.00", true);
            string strVouchersVar = CombineCurrencySymbol( _Utils.GetNumericValue(objSummary.Ticket_Var, "#,##0.00", true));
            txt_VouchersVar.Text = strVouchersVar;
                //_Utils.GetNumericValue(objSummary.Ticket_Var, "#,##0.00", true);
            string strAttendantPayVar = CombineCurrencySymbol( _Utils.GetNumericValue(objSummary.Handpay_Var, "#,##0.00", true));
            txt_AttnPayVar.Text = strAttendantPayVar;
                //_Utils.GetNumericValue(objSummary.Handpay_Var, "#,##0.00", true);
             string strProgValueVar = CombineCurrencySymbol( _Utils.GetNumericValue(objSummary.Progressive_Value_Variance, "#,##0.00", true));
             txt_ProgressiveVar.Text = strProgValueVar;
                //_Utils.GetNumericValue(objSummary.Progressive_Value_Variance, "#,##0.00", true);
            if (objSummary.Handle > 0)
            {

                txt_Payout.Text = _Utils.GetNumericValue(_payoutvalue, "#,##0.00", true);
                txt_Hold.Text = _Utils.GetNumericValue(nhold, "#,##0.00", true);
            }
            else
            {
                txt_Payout.Text = "0.00";
                txt_Hold.Text = "100.00";
            }
            string strHandle = CombineCurrencySymbol(_Utils.GetNumericValue(objSummary.Handle, "#,##0.00", true));
            txt_Handle.Text =  strHandle;
                //_Utils.GetNumericValue(objSummary.Handle, "#,##0.00", true);
            txt_Qty.Text = _Utils.GetNumericValue(objSummary.BatchCount, "0", true);
            txtRoute.Text = objSummary.RouteName;
        }
        #endregion

        #region "WeekBreakDown"
        void FillWeekBreakDown()
        {
            objBiz = new ViewSites_DropBatchBreakDown();
            List<DropBatchBreakDown> lstCollection = null;
            BMC.CoreLib.Win32.Win32Extensions.ShowAsyncDialog(this, this.GetResourceTextByKey(1, "MSG_FETCHING_DETAILS"), this.Executor,
          (o) =>
          {
              o.CloseOnComplete = true;
              lstCollection = objBiz.GetDropWeekBreakdown(_iSite_ID, _Week_ID);
          });
           
            foreach (var item in lstCollection)
            {
                //lst_Collectiondetails.Items.Add(string.Format("CO,{0}#{1}#SC", item.Collection_ID, item.Installation_ID), item.Zone_Name, 0);
                ListViewItem LstItem = lst_Collectiondetails.Items.Add(string.Format("CO,{0},{1}", item.Collection_ID, item.Installation_ID), item.Zone_Name, 0);
                LstItem.Tag = item;
                LstItem.UseItemStyleForSubItems = false;
                for (int iSeq = 0; iSeq < lst_Collectiondetails.Columns.Count - 1; iSeq++)
                {
                    LstItem.SubItems.Add("");
                }


                if (item.Handle > 0)
                {
                     _payoutWeek = Math.Round(((item.Handle - item.MeterWinOrLoss) / Math.Max(1, item.Handle)) * (100.00),2);
                }
                else
                {
                    _payoutWeek = 0;
                }
             
               double _payouthold = (100 - _payoutWeek);

                int iColumnIndex = 1;
                LstItem.SubItems[iColumnIndex + 0].Text = item.PosName;
                LstItem.SubItems[iColumnIndex + 1].Text = item.MachineName;
                LstItem.SubItems[iColumnIndex + 2].Text = item.StockNo;
                LstItem.SubItems[iColumnIndex + 3].Text = item.Collection_Days.ToString();

                _Utils.AddNumericValue(item.DecWinOrLoss, LstItem.SubItems[iColumnIndex + 4]);
                _Utils.AddNumericValue(item.MeterWinOrLoss, LstItem.SubItems[iColumnIndex + 5]);
                _Utils.AddNumericValue(item.TakeVariance, LstItem.SubItems[iColumnIndex + 6]);
                _Utils.AddNumericValue(item.Handle, LstItem.SubItems[iColumnIndex + 7]);
                _Utils.AddNumericValue(_payoutWeek, LstItem.SubItems[iColumnIndex + 8]);
                _Utils.AddNumericValue(_payouthold, LstItem.SubItems[iColumnIndex + 9]);
                _Utils.AddNumericValue(item.Declared_Coins, LstItem.SubItems[iColumnIndex + 10]);
                _Utils.AddNumericValue(item.DeFloat, LstItem.SubItems[iColumnIndex + 11]);
                _Utils.AddNumericValue(item.Refills, LstItem.SubItems[iColumnIndex + 12]);
                _Utils.AddNumericValue(item.Refunds, LstItem.SubItems[iColumnIndex + 13]);
                _Utils.AddNumericValue(item.Net_Coin, LstItem.SubItems[iColumnIndex + 14]);

                if (item.Datapak_ID == 0 && item.RDC_Coins == 0 && item.RDC_Notes == 0 && item.VTP == 0)
                {
                    LstItem.SubItems[iColumnIndex + 15].Text = this.GetResourceTextByKey("Key_NoDevice"); ;
                    LstItem.SubItems[iColumnIndex + 15].ForeColor = Color.Blue;
                    LstItem.SubItems[iColumnIndex + 16].Text = this.GetResourceTextByKey("Key_NA"); //Coin Var
                    LstItem.SubItems[iColumnIndex + 16].ForeColor = Color.Blue;
                }
                else
                {

                    if (item.Collection_EDC_Status == COLLECTION_EDC_FORCED)
                    {
                        LstItem.SubItems[iColumnIndex + 15].Text = this.GetResourceTextByKey("Key_Forced");
                        LstItem.SubItems[iColumnIndex + 15].ForeColor = Color.Blue;
                    }
                    else
                    {
                        _Utils.AddNumericValue(item.RDC_Coins, LstItem.SubItems[iColumnIndex + 15]);   //RDC coin
                    }
                    _Utils.AddNumericValue(item.Coin_Var, LstItem.SubItems[iColumnIndex + 16]);  //RDC Var
                    if (item.Collection_EDC_Status == COLLECTION_EDC_FORCED)
                        LstItem.SubItems[iColumnIndex + 16].ForeColor = Color.Blue;
                }

                _Utils.AddNumericValue(item.Declared_Notes, LstItem.SubItems[iColumnIndex + 17]);  //Notes
                _Utils.AddNumericValue(item.RDC_Notes, LstItem.SubItems[iColumnIndex + 18]); //rdc Notes
                _Utils.AddNumericValue(item.Note_Var, LstItem.SubItems[iColumnIndex + 19]); //Notes variance
                _Utils.AddNumericValue(item.DecTicketBalance, LstItem.SubItems[iColumnIndex + 20]); //Declared Tickets
                _Utils.AddNumericValue(item.Shortpay, LstItem.SubItems[iColumnIndex + 21]);
                _Utils.AddNumericValue(item.Void, LstItem.SubItems[iColumnIndex + 22]);
                _Utils.AddNumericValue(item.DecTicketBalance - item.Ticket_Var, LstItem.SubItems[iColumnIndex + 23]); //RDC Tickets
                _Utils.AddNumericValue(item.Ticket_Var, LstItem.SubItems[iColumnIndex + 24]);  //Ticket Variance
                _Utils.AddNumericValue(item.DecHandpay, LstItem.SubItems[iColumnIndex + 25]);  //Declared Handpay


                if (item.Datapak_ID == 0 && item.RDC_Coins == 0 && item.RDC_Notes == 0 && item.VTP == 0)
                {
                    LstItem.SubItems[iColumnIndex + 26].Text = this.GetResourceTextByKey("Key_NA");
                    LstItem.SubItems[iColumnIndex + 26].ForeColor = Color.Blue;
                    LstItem.SubItems[iColumnIndex + 27].Text = this.GetResourceTextByKey("Key_NA");
                    LstItem.SubItems[iColumnIndex + 27].ForeColor = Color.Blue;
                }
                else
                {
                    _Utils.AddNumericValue(item.RDCHandpay, LstItem.SubItems[iColumnIndex + 26]);
                    if (item.Collection_EDC_Status == COLLECTION_EDC_FORCED) //RDC handpay
                    {
                        LstItem.SubItems[iColumnIndex + 26].ForeColor = Color.Blue; ;
                    }
                    _Utils.AddNumericValue(item.Handpay_Var, LstItem.SubItems[iColumnIndex + 27]);

                    if (item.Collection_EDC_Status == COLLECTION_EDC_FORCED) //RDC Handpay Var
                    {
                        LstItem.SubItems[iColumnIndex + 27].ForeColor = Color.Blue; ;
                    }
                }

                //Progressive
                _Utils.AddNumericValue(item.Progressive_Value_Declared, LstItem.SubItems[iColumnIndex + 28]);
                _Utils.AddNumericValue(item.Progressive_Value_Meter, LstItem.SubItems[iColumnIndex + 29]);
                _Utils.AddNumericValue(item.Progressive_Value_Variance, LstItem.SubItems[iColumnIndex + 30]);

                string TmpDurationStr = string.Empty;
                TimeSpan ts = TimeSpan.FromMilliseconds(item.Collection_Total_Power_Duration);
                TmpDurationStr = ts.Hours.ToString("00") + ":" + ts.Minutes.ToString("00");

                LstItem.SubItems[iColumnIndex + 31].Text = TmpDurationStr;
                LstItem.SubItems[iColumnIndex + 32].Text = item.Total_Fault_Events.ToString();
                LstItem.SubItems[iColumnIndex + 33].Text = item.Total_Door_Events.ToString();
                LstItem.SubItems[iColumnIndex + 34].Text = item.Total_Power_Events.ToString();
                LstItem.SubItems[iColumnIndex + 35].Text = item.PromoCashableIn.ToString();
                LstItem.SubItems[iColumnIndex + 36].Text = item.PromoNonCashableIn.ToString();



            }
        }
        void FillWeekSummary()
        {
            CollsWeekDetailSummary objSummary = null;
            BMC.CoreLib.Win32.Win32Extensions.ShowAsyncDialog(this, this.GetResourceTextByKey(1, "MSG_FETCHING_DETAILS"), this.Executor,
       (o) =>
       {
           o.CloseOnComplete = true;
           objSummary = objBiz.FillWeekSummary(_iSite_ID, _Week_ID);
       });

            double _payoutWeek = ((objSummary.Handle - objSummary.MeterWinOrLoss) / objSummary.Handle) * 100;
            double _payouthold = (100 - _payoutWeek);

               //Update Header
            this.Text = string.Format(this.GetResourceTextByKey("Key_DropWeekBreakdown_Title"), this._SiteName, objSummary.WeekNumber);
            txt_GamingDay.Text = objSummary.WeekNumber.ToString(); //Week Number
            txt_CollectionDate.Text =_Utils.GetRegionalDate(objSummary.StartDate) + " - " + _Utils.GetRegionalDate(objSummary.EndDate); // Week Date 
            txt_User.Text = objSummary.Batch_User_Name.Split(',').First();
            UserToolTipText = objSummary.Batch_User_Name;
            string strGrossCoin = CombineCurrencySymbol(_Utils.GetNumericValue(objSummary.Declared_Coins, "#,##0.00", true));
            txt_GrossCoin.Text = strGrossCoin;
                //_Utils.GetNumericValue(objSummary.Declared_Coins, "#,##0.00", true);
            string strFloatRec = CombineCurrencySymbol(_Utils.GetNumericValue(objSummary.Defloat, "#,##0.00", true));
            txt_FloatRec.Text = strFloatRec;
                //_Utils.GetNumericValue(objSummary.Defloat, "#,##0.00", true);
            string strRefills = CombineCurrencySymbol(_Utils.GetNumericValue(objSummary.Refills, "#,##0.00", true));
            txt_Refills.Text = strRefills;
                //_Utils.GetNumericValue(objSummary.Refills, "#,##0.00", true);
            string strRefunds = CombineCurrencySymbol(_Utils.GetNumericValue(objSummary.Refunds, "#,##0.00", true));
            txt_Refunds.Text = strRefunds;
                //_Utils.GetNumericValue(objSummary.Refunds, "#,##0.00", true);
            string strProgressiveValue = CombineCurrencySymbol(_Utils.GetNumericValue(objSummary.Progressive_Value_Declared, "#,##0.00", true));
            txt_Progressive.Text = strProgressiveValue;
                //_Utils.GetNumericValue(objSummary.Progressive_Value_Declared, "#,##0.00", true);
            string strShortpay = CombineCurrencySymbol(_Utils.GetNumericValue(objSummary.Shortpay, "#,##0.00", true));
            txt_Shortpay.Text = strShortpay;
                //_Utils.GetNumericValue(objSummary.Shortpay, "#,##0.00", true);
            string strNetCoin = CombineCurrencySymbol(_Utils.GetNumericValue(objSummary.Net_Coin, "#,##0.00", true));
            txt_NetCoin.Text = strNetCoin;
                //_Utils.GetNumericValue(objSummary.Net_Coin, "#,##0.00", true);
            string strDeclaredNotes = CombineCurrencySymbol(_Utils.GetNumericValue(objSummary.Declared_Notes, "#,##0.00", true));
            txt_Notes.Text = strDeclaredNotes;
                //_Utils.GetNumericValue(objSummary.Declared_Notes, "#,##0.00", true);
            string strDecHandpay = CombineCurrencySymbol(_Utils.GetNumericValue(objSummary.DecHandpay, "#,##0.00", true));
            txt_AttnPay.Text = strDecHandpay;
                //_Utils.GetNumericValue(objSummary.DecHandpay, "#,##0.00", true);
            string strTicketBalance = CombineCurrencySymbol(_Utils.GetNumericValue(objSummary.DecTicketBalance, "#,##0.00", true));
            txt_Vouchers.Text = strTicketBalance;
                //_Utils.GetNumericValue(objSummary.DecTicketBalance, "#,##0.00", true);
            string strWinLoss = CombineCurrencySymbol(_Utils.GetNumericValue(objSummary.DecWinOrLoss, "#,##0.00", true));
            txt_DecWinLoss.Text = strWinLoss;
                //_Utils.GetNumericValue(objSummary.DecWinOrLoss, "#,##0.00", true);
            string strCoinVar = CombineCurrencySymbol(_Utils.GetNumericValue(objSummary.Coin_Var, "#,##0.00", true));
            txt_CoinVar.Text = strCoinVar;
                //_Utils.GetNumericValue(objSummary.Coin_Var, "#,##0.00", true);
            string strNoteVar = CombineCurrencySymbol(_Utils.GetNumericValue(objSummary.Note_Var, "#,##0.00", true));
            txt_BillsVar.Text = strNoteVar;
                //_Utils.GetNumericValue(objSummary.Note_Var, "#,##0.00", true);
            string strTicketVar = CombineCurrencySymbol(_Utils.GetNumericValue(objSummary.Ticket_Var, "#,##0.00", true));
            txt_VouchersVar.Text = strTicketVar;
                ///_Utils.GetNumericValue(objSummary.Ticket_Var, "#,##0.00", true);
            string strHandpayVar = CombineCurrencySymbol(_Utils.GetNumericValue(objSummary.Handpay_Var, "#,##0.00", true));
            txt_AttnPayVar.Text = strHandpayVar;
                //_Utils.GetNumericValue(objSummary.Handpay_Var, "#,##0.00", true);
            string strProgValueVar = CombineCurrencySymbol(_Utils.GetNumericValue(objSummary.Progressive_Value_Variance, "#,##0.00", true));
            txt_ProgressiveVar.Text = strProgValueVar;
                //_Utils.GetNumericValue(objSummary.Progressive_Value_Variance, "#,##0.00", true);
            if (objSummary.Handle > 0)
            {
                txt_Payout.Text = _Utils.GetNumericValue(_payoutWeek, "#,##0.00", true);
                txt_Hold.Text = _Utils.GetNumericValue(_payouthold, "#,##0.00", true);
            }
            else
            {
                txt_Payout.Text = "0.00";
                txt_Hold.Text = "100.00";
            }
            string strHandle = CombineCurrencySymbol(_Utils.GetNumericValue(objSummary.Handle, "#,##0.00", true));
            txt_Handle.Text = strHandle;
                //_Utils.GetNumericValue(objSummary.Handle, "#,##0.00", true);
            //txt_Qty.Text = objSummary.BatchCount.ToString("0");
            txt_User.Text = objSummary.BatchCount.ToString(); // QTY


        }
        #endregion

        void RefreshEvents()
        {
            lst_PositionEvents.Items.Clear();
            List<CollectionEvents> lstEvent = null;
            if (lst_Collectiondetails.SelectedItems.Count > 0)
            {
               int Collection_id = ((DropBatchBreakDown)lst_Collectiondetails.SelectedItems[0].Tag).Collection_ID;
               BMC.CoreLib.Win32.Win32Extensions.ShowAsyncDialog(this, this.GetResourceTextByKey(1, "MSG_FETCHING_EVENTS"), this.Executor,
                (o) =>
                {
                    o.CloseOnComplete = true;
                    lstEvent = objBiz.GetCollectionEvents(Collection_id);
                });
            }
            string TmpDurationStr;
            if (lstEvent == null) return;
            foreach (var tempevent in lstEvent)
            {
                ListViewItem LstItem = null;
                switch (tempevent.Type.ToUpper())
                {
                    case "DOOR":
                        //LstItem = lst_PositionEvents.Items.Add(string.Format("DE{0}", tempevent.Event_ID), tempevent.Event_Date + " " + tempevent.EVENT_Time, 0);
                        LstItem = lst_PositionEvents.Items.Add("Door", 7);
                        //LstItem.SubItems.Add("Door",);
                        LstItem.ForeColor = Color.Blue;
                        LstItem.SubItems[LstItem.SubItems.Count - 1].ForeColor = Color.Blue;
                        LstItem.SubItems.Add(tempevent.Event_Date);
                        LstItem.SubItems[LstItem.SubItems.Count - 1].ForeColor = Color.Blue;
                        LstItem.SubItems.Add(tempevent.EVENT_Time);
                        LstItem.SubItems[LstItem.SubItems.Count - 1].ForeColor = Color.Blue;
                        TimeSpan ts = TimeSpan.FromSeconds(tempevent.Duration);
                        TmpDurationStr = ts.Hours.ToString("00") + ts.Minutes.ToString(":00") + ts.Seconds.ToString(":00");
                        LstItem.SubItems.Add(TmpDurationStr);
                        LstItem.SubItems[LstItem.SubItems.Count - 1].ForeColor = Color.Blue;
                        LstItem.SubItems.Add(tempevent.Description);
                        LstItem.SubItems[LstItem.SubItems.Count - 1].ForeColor = Color.Blue;
                        break;
                    case "FAULT":
                        //LstItem = lst_PositionEvents.Items.Add(string.Format("FE{0}", tempevent.Event_ID), tempevent.Event_Date + " " + tempevent.EVENT_Time, 0);
                        LstItem = lst_PositionEvents.Items.Add("Fault", 6);
                        LstItem.ForeColor = Color.Red;
                        //LstItem.SubItems.Add("Fault");
                        //LstItem.SubItems[LstItem.SubItems.Count - 1].ForeColor = Color.Red;
                        LstItem.SubItems.Add(tempevent.Event_Date);
                        LstItem.SubItems[LstItem.SubItems.Count - 1].ForeColor = Color.Red;
                        LstItem.SubItems.Add(tempevent.EVENT_Time);
                        LstItem.SubItems[LstItem.SubItems.Count - 1].ForeColor = Color.Red;
                        LstItem.SubItems.Add(this.GetResourceTextByKey("Key_NA"));
                        LstItem.SubItems[LstItem.SubItems.Count - 1].ForeColor = Color.Red;
                        LstItem.SubItems.Add(tempevent.Description);
                        LstItem.SubItems[LstItem.SubItems.Count - 1].ForeColor = Color.Red;
                        break;
                    case "POWER":
                        //LstItem = lst_PositionEvents.Items.Add(string.Format("PE{0}", tempevent.Event_ID), tempevent.Event_Date + " " + tempevent.EVENT_Time, 0);
                        LstItem = lst_PositionEvents.Items.Add("Power On", 8);
                        LstItem.ForeColor = Color.Black;
                        //LstItem.SubItems.Add("Power On");
                        //LstItem.SubItems[LstItem.SubItems.Count - 1].ForeColor = Color.Black;
                        LstItem.SubItems.Add(tempevent.Event_Date);
                        LstItem.SubItems[LstItem.SubItems.Count - 1].ForeColor = Color.Black;
                        LstItem.SubItems.Add(tempevent.EVENT_Time);
                        LstItem.SubItems[LstItem.SubItems.Count - 1].ForeColor = Color.Black;
                        TimeSpan ts1 = TimeSpan.FromSeconds(tempevent.Duration);
                        TmpDurationStr = ts1.Hours.ToString("00") + ts1.Minutes.ToString(":00") + ts1.Seconds.ToString(":00");
                        LstItem.SubItems.Add(TmpDurationStr);
                        LstItem.SubItems[LstItem.SubItems.Count - 1].ForeColor = Color.Black;
                        LstItem.SubItems.Add(tempevent.Description);
                        LstItem.SubItems[LstItem.SubItems.Count - 1].ForeColor = Color.Black;
                        break;
                }

            }
        }

        private void lst_Collectiondetails_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                BMC.CoreLib.Win32.Win32Extensions.ShowDialogExAndDestroy(new CollHistoryBreakDownForm(((DropBatchBreakDown)lst_Collectiondetails.SelectedItems[0].Tag).Collection_ID, _iSite_ID, this._SiteName), this, null, null);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

  

        private void lst_Collectiondetails_KeyUp(object sender, KeyEventArgs e)
        {

            try
            {
                if (lst_Collectiondetails.SelectedItems.Count > 0 && e.KeyCode== Keys.Return)
                {
                    BMC.CoreLib.Win32.Win32Extensions.ShowDialogExAndDestroy(new CollHistoryBreakDownForm(((DropBatchBreakDown)lst_Collectiondetails.SelectedItems[0].Tag).Collection_ID, _iSite_ID, this._SiteName), this, null, null);
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void btn_CashCollectionReport_Click(object sender, EventArgs e)
        {
            try
            {
                // TODO
                // Below code is 12.4.3 merge
                // Create reports in rdl file and modify appropriately
                //if (_iBatch_ID == 0)
                //{

                //    if (AdminBusiness.GetSetting("Region", "UK").ToUpper() == "UK")
                //        cReportViewer.ShowBatchWinLossReportForUK(_Week_ID, _iSite_ID, true);
                //    else
                //        cReportViewer.ShowBatchWinLossReport(_Week_ID, _iSite_ID, true);
                //}
                //else
                //{
                //    if (AdminBusiness.GetSetting("Region", "UK").ToUpper() == "UK")
                //        cReportViewer.ShowBatchWinLossReportForUK(_iBatch_ID, _iSite_ID, false);
                //    else
                //        cReportViewer.ShowBatchWinLossReport(_iBatch_ID, _iSite_ID, false);
                //}

                Cursor.Current = Cursors.WaitCursor;
                clsSPParams spParams = new clsSPParams();
                spParams.BatchNo = (_iBatch_ID == 0) ? _Week_ID : _iBatch_ID;
                spParams.IsWeek = (_iBatch_ID == 0);
                spParams.SiteID = _iSite_ID;
                spParams.SiteName = _SiteName;

                spParams.ReportFilterDateFormat = SettingsEntity.ReportDateTimeFormat;
                spParams.ReportDataDateAloneFormat = SettingsEntity.ReportDataDateAloneFormat;
                spParams.ReportDataDateNTimeFormat = SettingsEntity.ReportDataDateNTimeFormat;
                spParams.ReportPrintDateTimeFormat = SettingsEntity.ReportPrintDateTimeFormat;

                spParams.CurrencyCulture = Common.Utilities.ExtensionMethods.CurrentSiteCulture;
                if (!spParams.IsWeek)
                {
                    BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_Report_BatchWinLoss", this.GetResourceTextByKey("Key_RT_BatchWinLossReport"), "ENT_BatchWinLossReport", spParams, false);
                }
                else
                {
                    BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_Report_BatchWinLoss", this.GetResourceTextByKey("Key_RT_WeeklyWinLoss"), "ENT_BatchWinLossReport", spParams, false);
                }

                Cursor.Current = Cursors.Default;   
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btn_ExceptionSummaryReport_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                // TODO
                // Below code is 12.4.3 merge
                // Create reports in rdl file and modify appropriately
                //if (_Mode == FormCollectionHistory_Mode.Batch)
                //{

                //    if (AdminBusiness.GetSetting("Region", "UK").ToUpper() == "UK")
                //        cReportViewer.ShowBatchDropExceptionReportUK(_iBatch_ID, _iSite_ID);
                //    else
                //        cReportViewer.ShowBatchDropExceptionReport(_iBatch_ID, _iSite_ID);
                //}
                //else
                //{
                //    cReportViewer.ShowWeekDropExceptionReport(_iSite_ID, _Week_ID);
                //}

                    if (_Mode == FormCollectionHistory_Mode.Batch)
                   {                      
                        clsSPParams spParams = new clsSPParams();
                        spParams.BatchId = _iBatch_ID;
                        spParams.SiteID = _iSite_ID;
                        spParams.SiteName = this._SiteName;
                        spParams.ReportFilterDateFormat = SettingsEntity.ReportDateTimeFormat;
                        spParams.ReportDataDateAloneFormat = SettingsEntity.ReportDataDateAloneFormat;
                        spParams.ReportDataDateNTimeFormat = SettingsEntity.ReportDataDateNTimeFormat;
                        spParams.ReportPrintDateTimeFormat = SettingsEntity.ReportPrintDateTimeFormat;
                        BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_REPORT_BatchDropException", this.GetResourceTextByKey("Key_RT_BATCHDROPEXCEPTIONREPORT"), "ENT_BatchDropExceptionReport", spParams, false);
                    }
                    else
                    {                       
                        clsSPParams spParams = new clsSPParams();
                        spParams.WeekId = _Week_ID;
                        spParams.SiteID = _iSite_ID;
                        spParams.SiteName = this._SiteName;
                        spParams.WeekNo = this._WeekNo;
                        spParams.Week = this._Week;
                        spParams.ReportFilterDateFormat = SettingsEntity.ReportDateTimeFormat;
                        spParams.ReportDataDateAloneFormat = SettingsEntity.ReportDataDateAloneFormat;
                        spParams.ReportDataDateNTimeFormat = SettingsEntity.ReportDataDateNTimeFormat;
                        spParams.ReportPrintDateTimeFormat = SettingsEntity.ReportPrintDateTimeFormat;
                        BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_Report_WeeklyDropException", this.GetResourceTextByKey("Key_RT_WeeklyExceptionSummaryReport"), "ENT_VSWeeklyDropExceptionReport", spParams, false);
                    }                    
                
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                Cursor.Current = Cursors.Default;
            }
        }

        private void lst_Collectiondetails_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                //if (e.Column == _lvwColumnSorter.SortColumn)
                //{
                //    // Reverse the current sort direction for this column.
                //    if (_lvwColumnSorter.Order == System.Windows.Forms.SortOrder.Ascending)
                //    {
                //        _lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Descending;
                //    }
                //    else
                //    {
                //        _lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                //    }
                //}
                //else
                //{
                //    // Set the column number that is to be sorted; default to ascending.
                //    _lvwColumnSorter.SortColumn = e.Column;
                //    _lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                //}
                //ListView lst_view = sender as ListView;
                //lst_view.Sort();
        }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }
        }

        private void btn_SGVILiqReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (SettingsEntity.LiquidationProfitShare)
                {
                    LiquidationUtility oLiquidation = new LiquidationUtility();
                    int iCount = oLiquidation.GetLiquidationDetailReportRecords(EnterpriseDataContextHelper.ConnectionString, _iBatch_ID, null).Count();
                    if (iCount == 0)
                    {
                        BMC.EnterpriseClient.Helpers.Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_COLL_REC"), this.Text);
                        return;
                    }
                    clsSPParams spParams = new clsSPParams();
                    spParams.BatchId = (_iBatch_ID == null) ? 0 : _iBatch_ID;
                    spParams.ReadId = 0;
                    spParams.SiteName = this._SiteName;
                    spParams.ReportFilterDateFormat = SettingsEntity.ReportDateTimeFormat;
                    spParams.ReportDataDateAloneFormat = SettingsEntity.ReportDataDateAloneFormat;
                    spParams.ReportDataDateNTimeFormat = SettingsEntity.ReportDataDateNTimeFormat;
                    spParams.ReportPrintDateTimeFormat = SettingsEntity.ReportPrintDateTimeFormat;

                    BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_Report_GetLiquidationDetail", this.GetResourceTextByKey("Key_RT_ReportLiquidationSummary_CollectionBased"), "ENT_LiquidationSummary_PS", spParams, false);
                }
                else
                {
                    clsSPParams spParams = new clsSPParams();
                    spParams.Batch_No = (_iBatch_ID == null) ? "0" : _iBatch_ID.ToString();
                    BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_REPORT_SGVI_LiquidationSummary", "SGVI Liquidation Report", "Liquidation", spParams, false);
                }
            }   
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        
        private void Setsymbol()
        {
           
            for (int i = 5; i <= 37; i++)
            {
                if (i != 9 && i != 10 &&  i != 32 && i != 33 && i != 34 && i != 35)
                {                
                    SetCurrencySymbol(i); 
                }
            }
        }
        private void SetCurrencySymbol(int index)
        {
            this.lst_Collectiondetails.Columns[index].Text = CombineCurrencySymbol(this.lst_Collectiondetails.Columns[index].Text);
        }

        private void txt_User_MouseHover(object sender, EventArgs e)
        {
            try
            {
                UserNameToolTip = new ToolTip();
                UserNameToolTip.InitialDelay = 0;
                UserNameToolTip.Show(UserToolTipText, txt_User, 0);
            }
            catch (Exception ex)
            {                
                ExceptionManager.Publish(ex);
            }
           
        }

        private void txt_User_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (UserNameToolTip == null) return;
                UserNameToolTip.Dispose();
            }
            catch (Exception ex)
            {
                
                ExceptionManager.Publish(ex);
            }
           
        }

       private void btnCollectionReport_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {

                using (frmCrystalReportViewer cReportViewer = new frmCrystalReportViewer())
                {
                    if (_iBatch_ID == 0)
                    {
                        cReportViewer.ShowCollectionReport(_Week_ID, _iSite_ID, true);
                    }
                    else
                    {
                    
                          cReportViewer.ShowCollectionReport(_iBatch_ID, _iSite_ID, false);
                    }
                    cReportViewer.ShowDialog();
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                Cursor.Current = Cursors.Default;
            }
        }
        
    }
}
