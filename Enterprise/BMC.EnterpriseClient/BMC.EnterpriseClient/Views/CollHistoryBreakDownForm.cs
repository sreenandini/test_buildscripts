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
using BMC.CoreLib.Concurrent;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class CollHistoryBreakDownForm : Form
    {
        #region Local Declartion

        #region Variables

        int _Collection_ID;
        int _Site_id ;
        string _CurrencySymbol;
        string _Site_Name=string.Empty;
        private ListViewColumnSorter _lvwColumnSorter = null;
        private ToolTip UserNameToolTip;
        public string strCurrencyForviewsite { get; private set; }
        public String UserToolTipText;
        #endregion Variables

        #region Objects

        frmAdminUtilities _Utils = new frmAdminUtilities();
        ViewSites_DropBreakdown objBiz = new ViewSites_DropBreakdown();
        ViewSites_DropBatchBreakDown objEventsBiz = new ViewSites_DropBatchBreakDown();

        List<AssetVarianceHistory> _lstAssetVarianceHistory;
        List<TreasuryDetails> _lstCashDeskTransaction;
        CollectionBreakDown oCollectionBreakDown;
        
        ListViewItem[] lvItem;
        Font oFont;
               
        public IExecutorService Executor { get; private set; }       
        #endregion Objects

        #endregion Local Declartion

        #region Constructor

        private CollHistoryBreakDownForm()
        {
            InitializeComponent();
            _lstAssetVarianceHistory = null;
            _lstCashDeskTransaction = null;
            lvItem = null;
            oFont = null;
            _lvwColumnSorter = new ListViewColumnSorter();
            strCurrencyForviewsite = new System.Globalization.RegionInfo(BMC.Common.ConfigurationManagement.ConfigManager.Read("GetDefaultCultureForCurrency")).CurrencySymbol;
            Setsymbol();
        }
        protected string CombineSymbolWithText(string text)
        {
            return strCurrencyForviewsite + " " + text;
        }

        private void SetTagProperty()
        {
           // this.clmAmount.Tag = "Key_Amount";
            this.clmAttendantpayProgressiveVar.Tag = "Key_AttendantpayProgressiveVar";
            this.clmAttendantpayVar.Tag = "Key_AttendantpayVar";
            this.clmBillsVar.Tag = "Key_BillsVar";
            this.grpCashDeskTrans.Tag = "Key_CashDeskMachhineTransactions";
            this.btnReturn.Tag = "Key_CloseCaption";
            this.clmCoinVar.Tag = "Key_CoinVar";
            this.clmCollectionDate.Tag = "Key_GamingDay";
            this.label1.Tag = "Key_BatchDateColon";
            this.Tag = "Key_CollHistoryBreakDown";
            this.clmComment.Tag = "Key_Comment";
            this.clmDate.Tag = "Key_Date";
            this.clm_Date.Tag = "Key_Date";
            this.clm_Description.Tag = "Key_Description";
            this.grpTitle.Tag = "Key_DropBreakdown";
            this.grpDropDetails.Tag = "Key_DropDetails";
            this.grpDropSummaries.Tag = "Key_DropSummaries";
            this.tbpDropMeterBreakdown.Tag = "Key_DeclaredMeteredBreakdown";
            this.clm_Duration.Tag = "Key_Duration";
            this.clmEFTInVar.Tag = "Key_EFTInVar";
            this.clmEFTOutVar.Tag = "Key_EFTOutVar";
            this.clmEnteredBy.Tag = "Key_EnteredBy";
            this.lblGame.Tag = "Key_GameColon";
            this.lblGamingDay.Tag = "Key_GamingDayColon";
            this.grpInstallationDetails.Tag = "Key_InstallationDetails";
            this.clmIssuedBy.Tag = "Key_IssuedBy";
            this.lblVarLast.Tag = "Key_LastColon";
            this.btn_ModifyDeclaration.Tag = "Key_ModifyDeclaration";
            this.lblPos.Tag = "Key_PosColon";
            this.grpPositionEvents.Tag = "Key_PositionEvents";
            this.lblVarRecords.Tag = "Key_Records";
            this.clmTime.Tag = "Key_Time";
            this.clm_Time.Tag = "Key_Time";
            this.clmType.Tag = "Key_Type";
            this.clm_Type.Tag = "Key_Type";
            this.lblUser.Tag = "Key_UserColon";
            this.tbpVarianceHistory.Tag = "Key_VarianceHistory";
            this.clmVoucherInVar.Tag = "Key_VoucherInVar";
            this.clmVoucherOutVar.Tag = "Key_VoucherOutVar";
            this.lblZone.Tag = "Key_ZoneColon";
            this.lbl_Asset.Tag = "Key_Asset";
            this.lbl_Declaredby.Tag = "Key_DeclaredBy";
        }

        public CollHistoryBreakDownForm(int Collection_id, int Site_id, string Site_Name)
            : this()
        {
            this._Collection_ID = Collection_id;
            this._Site_id = Site_id;
            this._Site_Name = Site_Name;
            this.MinimizeBox = false;
            _lstAssetVarianceHistory = null;
            _lstCashDeskTransaction = null;
            lvItem = null;
            oFont = null;
            clmAmount.Text = CombineSymbolWithText(this.GetResourceTextByKey("Key_Amount")); //added for prefixing currency symbol with Amount Column 
            lvwVarianceHistory.ListViewItemSorter = _lvwColumnSorter;
            lvDropBreakDown.ListViewItemSorter = _lvwColumnSorter;
            lvwPositionEvents.ListViewItemSorter = _lvwColumnSorter;
            SetTagProperty();

        }

        #endregion Constructor

        #region Event Methods

        private void CollHistoryBreakDownForm_Load(object sender, EventArgs e)
        {

            try
            {
                btn_ModifyDeclaration.Visible = AppGlobals.Current.HasUserAccess("HQ_Collections") && SettingsEntity.SGVI_Enabled;
                oFont = new Font(this.Font, FontStyle.Bold);                
                LoadData(true);
                LoadEvents();                
                LoadCashDeskTransaction();
                FillPeriodCount();
                cmbPeriodCount.SelectedIndex = 8;
                this.ResolveResources();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void cmbPeriodCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadVariance();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion Event Methods

        #region User Defined Function

        private void LoadEvents()
        {
            List<CollectionEvents> lstEvent = null;

            BMC.CoreLib.Win32.Win32Extensions.ShowAsyncDialog(this, this.GetResourceTextByKey(1, "MSG_VIEWSITES_BREAKDOWN"), this.Executor,
                (o) =>
                {
                    o.CloseOnComplete = true;
                    lstEvent = objEventsBiz.GetCollectionEvents(this._Collection_ID);
                });


            lvwPositionEvents.Items.Clear();
            string TmpDurationStr;
            if (lstEvent == null) return;
            foreach (var tempevent in lstEvent)
            {
                ListViewItem LstItem = null;
                switch (tempevent.Type.ToUpper())
                {
                    case "DOOR":
                        //LstItem = lst_PositionEvents.Items.Add(string.Format("DE{0}", tempevent.Event_ID), tempevent.Event_Date + " " + tempevent.EVENT_Time, 0);
                        LstItem = lvwPositionEvents.Items.Add("Door", 6);
                        //LstItem.SubItems.Add("Door",);
                        LstItem.ForeColor = Color.Blue;
                        LstItem.SubItems[LstItem.SubItems.Count - 1].ForeColor = Color.Blue;
                        LstItem.SubItems.Add(_Utils.GetRegionalDate(tempevent.Event_Date));
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
                        LstItem = lvwPositionEvents.Items.Add("Fault", 5);
                        LstItem.ForeColor = Color.Red;
                        //LstItem.SubItems.Add("Fault");
                        //LstItem.SubItems[LstItem.SubItems.Count - 1].ForeColor = Color.Red;
                        LstItem.SubItems.Add(_Utils.GetRegionalDate(tempevent.Event_Date));
                        LstItem.SubItems[LstItem.SubItems.Count - 1].ForeColor = Color.Red;
                        LstItem.SubItems.Add(tempevent.EVENT_Time);
                        LstItem.SubItems[LstItem.SubItems.Count - 1].ForeColor = Color.Red;
                        LstItem.SubItems.Add("n/a");
                        LstItem.SubItems[LstItem.SubItems.Count - 1].ForeColor = Color.Red;
                        LstItem.SubItems.Add(tempevent.Description);
                        LstItem.SubItems[LstItem.SubItems.Count - 1].ForeColor = Color.Red;
                        break;
                    case "POWER":
                        //LstItem = lst_PositionEvents.Items.Add(string.Format("PE{0}", tempevent.Event_ID), tempevent.Event_Date + " " + tempevent.EVENT_Time, 0);
                        LstItem = lvwPositionEvents.Items.Add("Power On", 7);
                        LstItem.ForeColor = Color.Black;
                        //LstItem.SubItems.Add("Power On");
                        //LstItem.SubItems[LstItem.SubItems.Count - 1].ForeColor = Color.Black;
                        LstItem.SubItems.Add(_Utils.GetRegionalDate(tempevent.Event_Date));
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

        private void LoadData(bool ConstructList)
        {
            oCollectionBreakDown = null;
            lvDropBreakDown.Items.Clear();
            lvwDropSummaries.Items.Clear();

            //obj= objBiz.CollectionBreakDown(this._Collection_ID, this._Site_id);
            // fetching details
            BMC.CoreLib.Win32.Win32Extensions.ShowAsyncDialog(this, this.GetResourceTextByKey(1, "MSG_FETCHING_DETAILS"), this.Executor,
                (o) =>
                {
                    o.CloseOnComplete = true;
                    oCollectionBreakDown = objBiz.CollectionBreakDown(this._Collection_ID, this._Site_id);
                    lvwVarianceHistory.Columns[9].Width = 0;
                });

            if (oCollectionBreakDown.Setting_Region.ToUpper() == "US")
                _CurrencySymbol = "$";
            else
                _CurrencySymbol = "£";


            this.Text = string.Format(this.GetResourceTextByKey(1, "MSG_VIEWSITES_SITEBATCH"), this._Site_Name, (oCollectionBreakDown.Batch_Ref.Split(',')[1]).ToString(), oCollectionBreakDown.Bar_Position_Name);
            if (ConstructList)
                LoadInitialSettings(oCollectionBreakDown.Setting_Region.ToUpper());
            txtZone.Text = oCollectionBreakDown.Zone_Name;
            txt_Game.Text = oCollectionBreakDown.GameName;
            txt_Asset.Text = oCollectionBreakDown.Asset;
            txt_Pos.Text = oCollectionBreakDown.Bar_Position_Name;
            txt_Declaredby.Text= oCollectionBreakDown.Declaredby;
            txtGamingDay.Text = _Utils.GetRegionalDate(oCollectionBreakDown.Batch_Date) + " " + oCollectionBreakDown.Batch_Time;
            txtCollectionDate.Text = _Utils.GetRegionalDate(oCollectionBreakDown.Collection_Date_Of_Collection) + " " + oCollectionBreakDown.Batch_Time;
            
            string[] User = oCollectionBreakDown.Batch_User_Name.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string sUserName = string.Empty;
            if (User.Count() > 1)
            {
                sUserName = User[1] + "," + User[0];
            }
            else
            {
                sUserName = User[0];
            }

            txtUser.Text = sUserName.Split(',').First();
            UserToolTipText = sUserName;

            //txtCollectionDate.Text = obj.Collection_Date_Of_Collection;
            //lvwDropSummaries.Clear();

            //SUMMARY 
            ListViewItem lstItem = lvwDropSummaries.Items.Add(_Utils.GetRegionalDate(oCollectionBreakDown.Batch_Date), 1);
            lstItem.UseItemStyleForSubItems = false;

            lstItem.SubItems.Add(oCollectionBreakDown.Batch_Time);
            lstItem.SubItems.Add(oCollectionBreakDown.Collection_Defloat_Collection);
            lstItem.SubItems.Add(oCollectionBreakDown.Batch_User_Name);
            //Amount
            string strAmount = _CurrencySymbol + (oCollectionBreakDown.CashCollected).ToString("0.00", System.Globalization.CultureInfo.CurrentUICulture);
            lstItem.SubItems.Add(strAmount);
            //+ oCollectionBreakDown.COL_EFT
            // _Utils.AddNumericValue((oCollectionBreakDown.CashCollected + oCollectionBreakDown.COL_EFT), lstItem.SubItems.Add(""), "0.00", _CurrencySymbol, true);
            //this.ResizeColumnHeaders(lvwDropSummaries);
          var TotalInMeter = oCollectionBreakDown.RDCCashIn + oCollectionBreakDown.RDC_COL_TICKETSIN +
                               oCollectionBreakDown.RDC_COL_EFTIN;
            var TotalInGross = oCollectionBreakDown.Collections + 
                               oCollectionBreakDown.COL_EFT;
            //GROSS ---------------------------------------------------------------------------------------------------
            ListViewItem lstGross = this.CreateCashRDCBreakdownRow(this.GetResourceTextByKey("Key_Gross"), 2);
            lstGross.SubItems[(int) lstBreakDownColumns.Total].Text = TotalInGross.ToString("G");
            lstGross.SubItems[(int)lstBreakDownColumns.D1000].Text = ((int)oCollectionBreakDown.Cash_Collected_100000p).ToString();
            lstGross.SubItems[(int)lstBreakDownColumns.D500].Text = ((int)oCollectionBreakDown.Cash_Collected_50000p).ToString();
            lstGross.SubItems[(int)lstBreakDownColumns.D200].Text = ((int)oCollectionBreakDown.Cash_Collected_20000p).ToString();
            lstGross.SubItems[(int)lstBreakDownColumns.D100].Text = ((int)oCollectionBreakDown.Cash_Collected_10000p).ToString();
            lstGross.SubItems[(int)lstBreakDownColumns.D50].Text = ((int)oCollectionBreakDown.Cash_Collected_5000p).ToString();
            lstGross.SubItems[(int)lstBreakDownColumns.D20].Text = ((int)oCollectionBreakDown.Cash_Collected_2000p).ToString();
            lstGross.SubItems[(int)lstBreakDownColumns.D10].Text = ((int)oCollectionBreakDown.Cash_Collected_1000p).ToString();
            lstGross.SubItems[(int)lstBreakDownColumns.D5].Text = ((int)oCollectionBreakDown.Cash_Collected_500p).ToString();
            lstGross.SubItems[(int)lstBreakDownColumns.D2].Text = oCollectionBreakDown.Cash_Collected_200p.ToString("0.00");
            lstGross.SubItems[(int)lstBreakDownColumns.D1].Text = oCollectionBreakDown.Cash_Collected_100p.ToString("0.00");
            lstGross.SubItems[(int)lstBreakDownColumns.TotalCoins].Text = (oCollectionBreakDown.COL_COINSIN - oCollectionBreakDown.nCoinsOut).ToString("0.00");
            lstGross.SubItems[(int)lstBreakDownColumns.CoinsIn].Text = oCollectionBreakDown.COL_COINSIN.ToString("0.00");
            lstGross.SubItems[(int)lstBreakDownColumns.CoinsOut].Text = oCollectionBreakDown.nCoinsOut.ToString("0.00");
            lstGross.SubItems[(int)lstBreakDownColumns.VoucherIn].Text = oCollectionBreakDown.DeclaredTicketValue.ToString("0.00");
            lstGross.SubItems[(int)lstBreakDownColumns.VoucherOut].Text = oCollectionBreakDown.COL_TICKETSOUT.ToString("0.00");
            lstGross.SubItems[(int)lstBreakDownColumns.Voucher].Text = oCollectionBreakDown.COL_TICKETS.ToString("0.00");
            lstGross.SubItems[(int)lstBreakDownColumns.Attendantpay].Text = oCollectionBreakDown.Collection_Treasury_Handpay_float.ToString("0.00");
            lstGross.SubItems[(int)lstBreakDownColumns.ApayProgressive].Text = oCollectionBreakDown.COL_PROG.ToString("0.00");
            lstGross.SubItems[(int)lstBreakDownColumns.EFTIn].Text = oCollectionBreakDown.COL_EFT.ToString("0.00");
            lstGross.SubItems[(int)lstBreakDownColumns.EFTOut].Text = oCollectionBreakDown.COL_EFTOUT.ToString("0.00");
            lstGross.SubItems[(int)lstBreakDownColumns.EFT].Text = (oCollectionBreakDown.COL_EFT - oCollectionBreakDown.COL_EFTOUT).ToString("0.00");
            lstGross.SubItems[(int)lstBreakDownColumns.PromoCashableIn].Text = oCollectionBreakDown.COL_PromoCashableIn.ToString("0.00");
            lstGross.SubItems[(int)lstBreakDownColumns.PromoNonCashableIn].Text = oCollectionBreakDown.COL_PromoNonCashableIn.ToString("0.00");
            //SHORT PAY 
            //oCollectionBreakDown.RDCCashIn + oCollectionBreakDown.RDC_COL_TICKETSIN + oCollectionBreakDown.RDC_COL_EFTIN --Meter
               // oCollectionBreakDown.Collections + oCollectionBreakDown.DeclaredTicketValue + oCollectionBreakDown.COL_EFT --gross
            ListViewItem lstShortPay = this.CreateCashRDCBreakdownRow(this.GetResourceTextByKey("Key_Shortpay"), 0);
            lstShortPay.SubItems[(int)lstBreakDownColumns.VoucherOut].Text = oCollectionBreakDown.Short_Pay.ToString("0.00");
            lstShortPay.SubItems[(int)lstBreakDownColumns.Voucher].Text = ((oCollectionBreakDown.Short_Pay > 0) ? oCollectionBreakDown.Short_Pay.ToString("-0.00") : oCollectionBreakDown.Short_Pay.ToString("0.00"));
            
            //NET
            ListViewItem lstNet = (ListViewItem)lstGross.Clone();
            lstNet.Text = this.GetResourceTextByKey("Key_Net");
            lvDropBreakDown.Items.Add(lstNet);

            //METER
            ListViewItem lstMeters = this.CreateCashRDCBreakdownRow(this.GetResourceTextByKey("Key_Meters"), 3);
            lstMeters.SubItems[(int) lstBreakDownColumns.Total].Text = TotalInMeter.ToString("0.00");
     
            lstMeters.SubItems[(int)lstBreakDownColumns.D1000].Text = ((int)oCollectionBreakDown.RDC_COL_1000).ToString();
            lstMeters.SubItems[(int)lstBreakDownColumns.D500].Text = ((int)oCollectionBreakDown.COL_500).ToString();
            lstMeters.SubItems[(int)lstBreakDownColumns.D200].Text = ((int)oCollectionBreakDown.COL_200).ToString();
            lstMeters.SubItems[(int)lstBreakDownColumns.D100].Text = ((int)oCollectionBreakDown.COL_100).ToString();
            lstMeters.SubItems[(int)lstBreakDownColumns.D50].Text = ((int)oCollectionBreakDown.COL_50).ToString();
            lstMeters.SubItems[(int)lstBreakDownColumns.D20].Text = ((int)oCollectionBreakDown.COL_20).ToString();
            lstMeters.SubItems[(int)lstBreakDownColumns.D10].Text = ((int)oCollectionBreakDown.COL_10).ToString();
            lstMeters.SubItems[(int)lstBreakDownColumns.D5].Text = ((int)oCollectionBreakDown.COL_5).ToString();
            lstMeters.SubItems[(int)lstBreakDownColumns.D2].Text = oCollectionBreakDown.RDC__COL_2.ToString("0.00");
            lstMeters.SubItems[(int)lstBreakDownColumns.D1].Text = oCollectionBreakDown.RDC_COL_1.ToString("0.00");
            lstMeters.SubItems[(int)lstBreakDownColumns.TotalCoins].Text = (oCollectionBreakDown.RDC_COL_TOTALCOINS).ToString("0.00");
            lstMeters.SubItems[(int)lstBreakDownColumns.CoinsIn].Text = oCollectionBreakDown.RDC_COL_COINSIN.ToString("0.00");
            lstMeters.SubItems[(int)lstBreakDownColumns.CoinsOut].Text = oCollectionBreakDown.nCoinsOut.ToString("0.00");
            lstMeters.SubItems[(int)lstBreakDownColumns.VoucherIn].Text = oCollectionBreakDown.RDC_COL_TICKETSIN.ToString("0.00");
            lstMeters.SubItems[(int)lstBreakDownColumns.VoucherOut].Text = oCollectionBreakDown.RDC_COL_TICKETSOUT.ToString("0.00");
            lstMeters.SubItems[(int)lstBreakDownColumns.Voucher].Text = oCollectionBreakDown.RDC_COL_TICKETS.ToString("0.00");
            lstMeters.SubItems[(int)lstBreakDownColumns.Attendantpay].Text = oCollectionBreakDown.RDC_COL_HANDPAY.ToString("0.00");
            lstMeters.SubItems[(int)lstBreakDownColumns.ApayProgressive].Text = oCollectionBreakDown.RDC_COL_PROG.ToString("0.00");
            lstMeters.SubItems[(int)lstBreakDownColumns.EFTIn].Text = oCollectionBreakDown.RDC_COL_EFTIN.ToString("0.00");
            lstMeters.SubItems[(int)lstBreakDownColumns.EFTOut].Text = oCollectionBreakDown.RDC_COL_EFTOUT.ToString("0.00");
            lstMeters.SubItems[(int)lstBreakDownColumns.EFT].Text = (oCollectionBreakDown.RDC_COL_EFT).ToString("0.00");

            lstMeters.SubItems[(int)lstBreakDownColumns.PromoCashableIn].Text = oCollectionBreakDown.RDC_COL_PromoCashableIn.ToString("0.00");
            lstMeters.SubItems[(int)lstBreakDownColumns.PromoNonCashableIn].Text = oCollectionBreakDown.RDC_COL_PromoNonCashableIn.ToString("0.00");

            //VARIANCE
            ListViewItem lstVariance = this.CreateCashRDCBreakdownRow(this.GetResourceTextByKey("Key_Variance"), 4);
            lstVariance.SubItems[(int) lstBreakDownColumns.Total].Text = (Convert.ToDecimal(TotalInGross)-TotalInMeter ).ToString();
            lstVariance.SubItems[(int)lstBreakDownColumns.D1000].Text =
               (float.Parse(lstNet.SubItems[(int)lstBreakDownColumns.D1000].Text) -
                float.Parse(lstMeters.SubItems[(int)lstBreakDownColumns.D1000].Text)).ToString();
            lstVariance.SubItems[(int)lstBreakDownColumns.D500].Text =
                (float.Parse(lstNet.SubItems[(int)lstBreakDownColumns.D500].Text) -
                float.Parse(lstMeters.SubItems[(int)lstBreakDownColumns.D500].Text)).ToString();
            lstVariance.SubItems[(int)lstBreakDownColumns.D200].Text =
                (float.Parse(lstNet.SubItems[(int)lstBreakDownColumns.D200].Text) -
                float.Parse(lstMeters.SubItems[(int)lstBreakDownColumns.D200].Text)).ToString();
            lstVariance.SubItems[(int)lstBreakDownColumns.D100].Text =
                (float.Parse(lstNet.SubItems[(int)lstBreakDownColumns.D100].Text) -
                float.Parse(lstMeters.SubItems[(int)lstBreakDownColumns.D100].Text)).ToString();
            lstVariance.SubItems[(int)lstBreakDownColumns.D50].Text =
                (float.Parse(lstNet.SubItems[(int)lstBreakDownColumns.D50].Text) -
                float.Parse(lstMeters.SubItems[(int)lstBreakDownColumns.D50].Text)).ToString();
            lstVariance.SubItems[(int)lstBreakDownColumns.D20].Text =
                (float.Parse(lstNet.SubItems[(int)lstBreakDownColumns.D20].Text) -
                float.Parse(lstMeters.SubItems[(int)lstBreakDownColumns.D20].Text)).ToString();
            lstVariance.SubItems[(int)lstBreakDownColumns.D10].Text =
                (float.Parse(lstNet.SubItems[(int)lstBreakDownColumns.D10].Text) -
                float.Parse(lstMeters.SubItems[(int)lstBreakDownColumns.D10].Text)).ToString();
            lstVariance.SubItems[(int)lstBreakDownColumns.D5].Text =
                (float.Parse(lstNet.SubItems[(int)lstBreakDownColumns.D5].Text) -
                float.Parse(lstMeters.SubItems[(int)lstBreakDownColumns.D5].Text)).ToString();
            lstVariance.SubItems[(int)lstBreakDownColumns.D2].Text =
                  (float.Parse(lstNet.SubItems[(int)lstBreakDownColumns.D2].Text) -
                float.Parse(lstMeters.SubItems[(int)lstBreakDownColumns.D2].Text)).ToString();
            lstVariance.SubItems[(int)lstBreakDownColumns.D1].Text =
                  (float.Parse(lstNet.SubItems[(int)lstBreakDownColumns.D1].Text) -
                float.Parse(lstMeters.SubItems[(int)lstBreakDownColumns.D1].Text)).ToString();
            lstVariance.SubItems[(int)lstBreakDownColumns.TotalCoins].Text =
                          (float.Parse(lstNet.SubItems[(int)lstBreakDownColumns.TotalCoins].Text) -
               float.Parse(lstMeters.SubItems[(int)lstBreakDownColumns.TotalCoins].Text)).ToString("0.00");
            lstVariance.SubItems[(int)lstBreakDownColumns.CoinsIn].Text =
            (float.Parse(lstNet.SubItems[(int)lstBreakDownColumns.CoinsIn].Text) -
                    float.Parse(lstMeters.SubItems[(int)lstBreakDownColumns.CoinsIn].Text)).ToString("0.00");

            lstVariance.SubItems[(int)lstBreakDownColumns.CoinsOut].Text =
            (float.Parse(lstNet.SubItems[(int)lstBreakDownColumns.CoinsOut].Text) -
                   float.Parse(lstMeters.SubItems[(int)lstBreakDownColumns.CoinsOut].Text)).ToString("0.00");


            lstVariance.SubItems[(int)lstBreakDownColumns.VoucherIn].Text =
            (float.Parse(lstNet.SubItems[(int)lstBreakDownColumns.VoucherIn].Text) -
                float.Parse(lstMeters.SubItems[(int)lstBreakDownColumns.VoucherIn].Text)).ToString("0.00");

            lstVariance.SubItems[(int)lstBreakDownColumns.VoucherOut].Text =
            (float.Parse(lstNet.SubItems[(int)lstBreakDownColumns.VoucherOut].Text) -
                float.Parse(lstMeters.SubItems[(int)lstBreakDownColumns.VoucherOut].Text)).ToString("0.00");
            lstVariance.SubItems[(int)lstBreakDownColumns.Voucher].Text =
            (float.Parse(lstNet.SubItems[(int)lstBreakDownColumns.Voucher].Text) -
                float.Parse(lstMeters.SubItems[(int)lstBreakDownColumns.Voucher].Text)).ToString("0.00");
            lstVariance.SubItems[(int)lstBreakDownColumns.Attendantpay].Text =
            (decimal.Parse(lstNet.SubItems[(int)lstBreakDownColumns.Attendantpay].Text) -
                decimal.Parse(lstMeters.SubItems[(int)lstBreakDownColumns.Attendantpay].Text)).ToString("0.00");
            lstVariance.SubItems[(int)lstBreakDownColumns.ApayProgressive].Text =
            (decimal.Parse(lstNet.SubItems[(int)lstBreakDownColumns.ApayProgressive].Text) -
             decimal.Parse(lstMeters.SubItems[(int)lstBreakDownColumns.ApayProgressive].Text)).ToString("0.00");
            lstVariance.SubItems[(int)lstBreakDownColumns.EFTIn].Text =
            (float.Parse(lstNet.SubItems[(int)lstBreakDownColumns.EFTIn].Text) -
             float.Parse(lstMeters.SubItems[(int)lstBreakDownColumns.EFTIn].Text)).ToString("0.00");
            lstVariance.SubItems[(int)lstBreakDownColumns.EFTOut].Text =
            (float.Parse(lstNet.SubItems[(int)lstBreakDownColumns.EFTOut].Text) -
             float.Parse(lstMeters.SubItems[(int)lstBreakDownColumns.EFTOut].Text)).ToString("0.00");
            lstVariance.SubItems[(int)lstBreakDownColumns.EFT].Text =
                            (float.Parse(lstNet.SubItems[(int)lstBreakDownColumns.EFT].Text) -
             float.Parse(lstMeters.SubItems[(int)lstBreakDownColumns.EFT].Text)).ToString("0.00");



            lstVariance.SubItems[(int)lstBreakDownColumns.PromoCashableIn].Text =
          (float.Parse(lstNet.SubItems[(int)lstBreakDownColumns.PromoCashableIn].Text) -
              float.Parse(lstMeters.SubItems[(int)lstBreakDownColumns.PromoCashableIn].Text)).ToString("0.00");


            lstVariance.SubItems[(int)lstBreakDownColumns.PromoNonCashableIn].Text =
         (float.Parse(lstNet.SubItems[(int)lstBreakDownColumns.PromoNonCashableIn].Text) -
             float.Parse(lstMeters.SubItems[(int)lstBreakDownColumns.PromoNonCashableIn].Text)).ToString("0.00");



            for (int iIndex = 0; iIndex < lvDropBreakDown.Items.Count; iIndex++)
            {
                lvDropBreakDown.Items[iIndex].UseItemStyleForSubItems = false;
                for (int isubIndex = 1; isubIndex < lvDropBreakDown.Items[iIndex].SubItems.Count; isubIndex++)
                {
                    string str = lvDropBreakDown.Items[iIndex].SubItems[isubIndex].Text;
                    _Utils.AddNumericValue(Double.Parse(str), lvDropBreakDown.Items[iIndex].SubItems[isubIndex]);

                }
            }

        }

        private void ResizeColumnHeaders(ListView lst)
        {
            for (int i = 0; i < lst.Columns.Count - 1; i++)
                lst.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.HeaderSize);
            lst.Columns[lst.Columns.Count - 1].Width = -2;
            lvwVarianceHistory.Columns[9].Width = 0;
        }

        private ListViewItem CreateCashRDCBreakdownRow(string sTitle, int nIconIndex)
        {
            ListViewItem LvItem = lvDropBreakDown.Items.Add(sTitle, nIconIndex);
            for (int i = 1; i <= (int)lstBreakDownColumns.PromoNonCashableIn; i++)
            {
                LvItem.SubItems.Add("");
            }
            //To avoid error during formating and generate default vaules
            LvItem.SubItems[(int)lstBreakDownColumns.Total].Text = "0";
            LvItem.SubItems[(int)lstBreakDownColumns.D1000].Text = "0";
            LvItem.SubItems[(int)lstBreakDownColumns.D500].Text = "0";
            LvItem.SubItems[(int)lstBreakDownColumns.D200].Text = "0";
            LvItem.SubItems[(int)lstBreakDownColumns.D100].Text = "0";
            LvItem.SubItems[(int)lstBreakDownColumns.D50].Text = "0";
            LvItem.SubItems[(int)lstBreakDownColumns.D20].Text = "0";
            LvItem.SubItems[(int)lstBreakDownColumns.D10].Text = "0";
            LvItem.SubItems[(int)lstBreakDownColumns.D5].Text = "0";
            LvItem.SubItems[(int)lstBreakDownColumns.D2].Text = "0.00";
            LvItem.SubItems[(int)lstBreakDownColumns.D1].Text = "0.00";
            LvItem.SubItems[(int)lstBreakDownColumns.CoinsIn].Text = "0.00";
            LvItem.SubItems[(int)lstBreakDownColumns.CoinsOut].Text = "0.00";
            LvItem.SubItems[(int)lstBreakDownColumns.TotalCoins].Text = "0.00";
            LvItem.SubItems[(int)lstBreakDownColumns.VoucherIn].Text = "0.00";
            LvItem.SubItems[(int)lstBreakDownColumns.VoucherOut].Text = "0.00";
            LvItem.SubItems[(int)lstBreakDownColumns.Voucher].Text = "0.00";
            LvItem.SubItems[(int)lstBreakDownColumns.Attendantpay].Text = "0.00";
            LvItem.SubItems[(int)lstBreakDownColumns.ApayProgressive].Text = "0.00";
            LvItem.SubItems[(int)lstBreakDownColumns.EFTIn].Text = "0.00";
            LvItem.SubItems[(int)lstBreakDownColumns.EFTOut].Text = "0.00";
            LvItem.SubItems[(int)lstBreakDownColumns.EFT].Text = "0.00";
            LvItem.SubItems[(int)lstBreakDownColumns.PromoCashableIn].Text = "0.00";
            LvItem.SubItems[(int)lstBreakDownColumns.PromoNonCashableIn].Text = "0.00";


            return LvItem;
        }

        void LoadInitialSettings(string Region)
        {
            try
            {

                lvDropBreakDown.HeaderStyle = ColumnHeaderStyle.Nonclickable;
                lvDropBreakDown.Columns.Add("").Width = 120;
                lvDropBreakDown.Columns.Add("Total In").Width =120;
                lvDropBreakDown.Columns.Add(lstBreakDownColumns.D1000.ToString(), 100, HorizontalAlignment.Right);
                lvDropBreakDown.Columns.Add(lstBreakDownColumns.D500.ToString(), 100, HorizontalAlignment.Right);
                lvDropBreakDown.Columns.Add(lstBreakDownColumns.D200.ToString(), 100, HorizontalAlignment.Right);
                lvDropBreakDown.Columns.Add(lstBreakDownColumns.D100.ToString(), 100, HorizontalAlignment.Right);
                lvDropBreakDown.Columns.Add(lstBreakDownColumns.D50.ToString(), 100, HorizontalAlignment.Right);
                lvDropBreakDown.Columns.Add(lstBreakDownColumns.D20.ToString(), 100, HorizontalAlignment.Right);
                lvDropBreakDown.Columns.Add(lstBreakDownColumns.D10.ToString(), 100, HorizontalAlignment.Right);
                lvDropBreakDown.Columns.Add(lstBreakDownColumns.D5.ToString(), 100, HorizontalAlignment.Right);
                lvDropBreakDown.Columns.Add(lstBreakDownColumns.D2.ToString(), 100, HorizontalAlignment.Right);
                lvDropBreakDown.Columns.Add(lstBreakDownColumns.D1.ToString(), 100, HorizontalAlignment.Right);
                lvDropBreakDown.Columns.Add("Coins In", 100, HorizontalAlignment.Right);
                lvDropBreakDown.Columns.Add("Coins Out", 100, HorizontalAlignment.Right);
                lvDropBreakDown.Columns.Add("Total Coins", 100, HorizontalAlignment.Right);
                lvDropBreakDown.Columns.Add("Voucher In", 100, HorizontalAlignment.Right);
                lvDropBreakDown.Columns.Add("Voucher Out", 100, HorizontalAlignment.Right);
                lvDropBreakDown.Columns.Add("Voucher", 100, HorizontalAlignment.Right);
                lvDropBreakDown.Columns.Add("Attendantpay", 100, HorizontalAlignment.Right);
                lvDropBreakDown.Columns.Add("A'pay Progressive", 100, HorizontalAlignment.Right);
                lvDropBreakDown.Columns.Add("EFT In", 100, HorizontalAlignment.Right);
                lvDropBreakDown.Columns.Add("EFT Out", 100, HorizontalAlignment.Right);
                lvDropBreakDown.Columns.Add("EFT", 100, HorizontalAlignment.Right);
                lvDropBreakDown.Columns.Add("Promo Cashable In", 170, HorizontalAlignment.Right);
                lvDropBreakDown.Columns.Add("Promo NonCashable In", 170, HorizontalAlignment.Right);

                //Hide EFT columns if disabled 
                if (!SettingsEntity.IsAFTEnabledForSite)
                {
                    lvDropBreakDown.Columns[(int)lstBreakDownColumns.EFTIn].Width = 0;
                    lvDropBreakDown.Columns[(int)lstBreakDownColumns.EFTOut].Width = 0;
                    lvDropBreakDown.Columns[(int)lstBreakDownColumns.EFT].Width = 0;
                }


                //Assign Symbol
                switch (Region.ToUpper())
                {
                    case "US":
                        {
                            lvDropBreakDown.Columns[(int)lstBreakDownColumns.D1000].Width = 0;
                            lvDropBreakDown.Columns[(int)lstBreakDownColumns.D500].Width = 0;
                            lvDropBreakDown.Columns[(int)lstBreakDownColumns.D200].Width = 0;
                            lvDropBreakDown.Columns[(int)lstBreakDownColumns.D2].Width = 0;
                            break;
                        }
                    case "UK":
                        {
                            lvDropBreakDown.Columns[(int)lstBreakDownColumns.D1000].Width = 0;
                            lvDropBreakDown.Columns[(int)lstBreakDownColumns.D500].Width = 0;
                            lvDropBreakDown.Columns[(int)lstBreakDownColumns.D200].Width = 0;
                            lvDropBreakDown.Columns[(int)lstBreakDownColumns.D100].Width = 0;
                            lvDropBreakDown.Columns[(int)lstBreakDownColumns.D2].Width = 0;
                            lvDropBreakDown.Columns[(int)lstBreakDownColumns.D1].Width = 0;
                            break;
                        }
                    case "AR":
                        {
                            lvDropBreakDown.Columns[(int)lstBreakDownColumns.D1000].Width = 0;
                            lvDropBreakDown.Columns[(int)lstBreakDownColumns.D500].Width = 0;
                            lvDropBreakDown.Columns[(int)lstBreakDownColumns.D200].Width = 0;
                            lvDropBreakDown.Columns[(int)lstBreakDownColumns.D1].Width = 0;
                            break;
                        }
                    default:
                        lvDropBreakDown.Columns[(int)lstBreakDownColumns.D1000].Width = 0;
                        lvDropBreakDown.Columns[(int)lstBreakDownColumns.D500].Width = 0;
                        lvDropBreakDown.Columns[(int)lstBreakDownColumns.D200].Width = 0;
                        lvDropBreakDown.Columns[(int)lstBreakDownColumns.D100].Width = 0;
                        lvDropBreakDown.Columns[(int)lstBreakDownColumns.D2].Width = 0;
                        lvDropBreakDown.Columns[(int)lstBreakDownColumns.D1].Width = 0;
                        break;
                }


                lvwDropSummaries.Columns.Add("Date", 100).Tag = "Key_Date";
                lvwDropSummaries.Columns.Add("Time", 50).Tag = "Key_Time";
                lvwDropSummaries.Columns.Add("Type", 100).Tag = "Key_Type";
                lvwDropSummaries.Columns.Add("User", 100).Tag = "Key_User";
                lvwDropSummaries.Columns.Add("Declared Amount", 100).Tag = "Key_DeclaredAmt";
                lvwDropSummaries.Columns[4].TextAlign = HorizontalAlignment.Right;
                lvDropBreakDown.Columns[(int)lstBreakDownColumns.Total].Text = CombineSymbolWithText(this.GetResourceTextByKey("Key_RC_TotalIn"));
                lvDropBreakDown.Columns[(int)lstBreakDownColumns.D100].Text = CombineSymbolWithText(this.GetResourceTextByKey("Key_100"));
                lvDropBreakDown.Columns[(int)lstBreakDownColumns.D50].Text = CombineSymbolWithText(this.GetResourceTextByKey("Key_50"));
                lvDropBreakDown.Columns[(int)lstBreakDownColumns.D20].Text = CombineSymbolWithText(this.GetResourceTextByKey("Key_20"));
                lvDropBreakDown.Columns[(int)lstBreakDownColumns.D10].Text = CombineSymbolWithText(this.GetResourceTextByKey("Key_10"));
                lvDropBreakDown.Columns[(int)lstBreakDownColumns.D5].Text = CombineSymbolWithText(this.GetResourceTextByKey("Key_5"));
                lvDropBreakDown.Columns[(int)lstBreakDownColumns.D2].Text = CombineSymbolWithText(this.GetResourceTextByKey("Key_2"));
                lvDropBreakDown.Columns[(int)lstBreakDownColumns.D1].Text = CombineSymbolWithText(this.GetResourceTextByKey("Key_1"));
                lvDropBreakDown.Columns[12].Text = CombineSymbolWithText(this.GetResourceTextByKey("Key_CoinsIn"));
                lvDropBreakDown.Columns[13].Text = CombineSymbolWithText(this.GetResourceTextByKey("Key_CoinsOut"));
                lvDropBreakDown.Columns[14].Text = CombineSymbolWithText(this.GetResourceTextByKey("Key_TotalCoins"));
                lvDropBreakDown.Columns[15].Text = CombineSymbolWithText(this.GetResourceTextByKey("Key_Voucherin"));
                lvDropBreakDown.Columns[16].Text = CombineSymbolWithText(this.GetResourceTextByKey("Key_voucherOut"));
                lvDropBreakDown.Columns[17].Text = CombineSymbolWithText(this.GetResourceTextByKey("Key_Vouchers"));
                lvDropBreakDown.Columns[18].Text = CombineSymbolWithText(this.GetResourceTextByKey("Key_Attendantpay"));
                lvDropBreakDown.Columns[19].Text = CombineSymbolWithText(this.GetResourceTextByKey("Key_AttnPayProgressive"));
                lvDropBreakDown.Columns[20].Text = CombineSymbolWithText(this.GetResourceTextByKey("Key_EftIn"));
                lvDropBreakDown.Columns[21].Text = CombineSymbolWithText(this.GetResourceTextByKey("Key_EftOut"));
                lvDropBreakDown.Columns[22].Text = CombineSymbolWithText(this.GetResourceTextByKey("Key_EFT"));
                lvDropBreakDown.Columns[23].Text = CombineSymbolWithText(this.GetResourceTextByKey("Key_PromoCashableIn"));
                lvDropBreakDown.Columns[24].Text = CombineSymbolWithText(this.GetResourceTextByKey("Key_PromoNonCashableIn"));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void FillPeriodCount()
        {
            cmbPeriodCount.Items.Clear();
            cmbPeriodCount.Items.Add(this.GetResourceTextByKey("Key_AllCriteria"));
            cmbPeriodCount.Items.Add("1");
            cmbPeriodCount.Items.Add("2");
            cmbPeriodCount.Items.Add("3");
            cmbPeriodCount.Items.Add("4");
            cmbPeriodCount.Items.Add("5");
            cmbPeriodCount.Items.Add("6");
            cmbPeriodCount.Items.Add("12");
            cmbPeriodCount.Items.Add("16");
            cmbPeriodCount.Items.Add("24");
            cmbPeriodCount.Items.Add("36");
            cmbPeriodCount.Items.Add("48");
            cmbPeriodCount.Items.Add("60");
            cmbPeriodCount.SelectedIndex = 8;
        }

        private void LoadVariance()
        {
            try
            {
                int iNoOfRecords = 0;
                int iLVIndex = 0;

                iNoOfRecords = cmbPeriodCount.Text == this.GetResourceTextByKey("Key_AllCriteria") ? 0 : Convert.ToInt32(cmbPeriodCount.Text);

                lvwVarianceHistory.Items.Clear();

                _lstAssetVarianceHistory = objBiz.GetAssetVarianceHistory(oCollectionBreakDown.Installation_Id, iNoOfRecords);

                lvItem = new ListViewItem[_lstAssetVarianceHistory.Count + 1];

                float? iSumCoin_Var = _lstAssetVarianceHistory.Sum(e => e.Coin_Var);
                float? fSumNote_Var = _lstAssetVarianceHistory.Sum(e => e.Note_Var);
                double? iSumTicket_In_Var = _lstAssetVarianceHistory.Sum(e => e.Ticket_In_Var);
                double? iSumTicket_Out_Var = _lstAssetVarianceHistory.Sum(e => e.Ticket_Out_Var);
                double? iSumEftIn_Var = _lstAssetVarianceHistory.Sum(e => e.EftIn_Var);
                double? iSumEftOut_Var = _lstAssetVarianceHistory.Sum(e => e.EftOut_Var);
                double? iSumHandpay_Var = _lstAssetVarianceHistory.Sum(e => e.Handpay_Var);
                double? iSumProgressive_Var = _lstAssetVarianceHistory.Sum(e => e.Progressive_Var);

                lvItem[iLVIndex] = new ListViewItem(new string[] { this.GetResourceTextByKey("Key_Total"),
                        "",//iSumCoin_Var
                        "",//fSumNote_Var
                        "",//iSumTicket_In_Var
                        "",//iSumTicket_Out_Var
                        "",//iSumEftIn_Var
                        "",//iSumEftOut_Var
                        "",//iSumHandpay_Var
                        "",//iSumProgressive_Var
                        ""//iSumTotal_Var
                    });

                _Utils.AddNumericValue(iSumCoin_Var, lvItem[iLVIndex].SubItems[1]);
                _Utils.AddNumericValue(fSumNote_Var, lvItem[iLVIndex].SubItems[2]);
                _Utils.AddNumericValue(iSumTicket_In_Var, lvItem[iLVIndex].SubItems[3]);
                _Utils.AddNumericValue(iSumTicket_Out_Var, lvItem[iLVIndex].SubItems[4]);
                _Utils.AddNumericValue(iSumEftIn_Var, lvItem[iLVIndex].SubItems[5]);
                _Utils.AddNumericValue(iSumEftOut_Var, lvItem[iLVIndex].SubItems[6]);
                _Utils.AddNumericValue(iSumHandpay_Var, lvItem[iLVIndex].SubItems[7]);
                _Utils.AddNumericValue(iSumProgressive_Var, lvItem[iLVIndex].SubItems[8]);

                lvItem[iLVIndex].ForeColor = Color.Blue;
                lvItem[iLVIndex].Font = oFont;
                lvItem[iLVIndex++].Tag = "";

                foreach (var item in _lstAssetVarianceHistory)
                {
                    lvItem[iLVIndex] = new ListViewItem(new string[] { _Utils.GetRegionalDate(item.Gaming_Day),
                        "",//Coin_Var
                        "",//Note_Var
                        "",//Ticket_In_Var
                        "",//Ticket_Out_Var
                        "",//EftIn_Var
                        "",//EftOut_Var
                        "",//Handpay_Var
                        "",//Progressive_Var
                        ""//Total_Var
                    });
                    lvItem[iLVIndex].UseItemStyleForSubItems = false;
                    _Utils.AddNumericValue(item.Coin_Var, lvItem[iLVIndex].SubItems[1]);
                    _Utils.AddNumericValue(item.Note_Var, lvItem[iLVIndex].SubItems[2]);
                    _Utils.AddNumericValue(item.Ticket_In_Var, lvItem[iLVIndex].SubItems[3]);
                    _Utils.AddNumericValue(item.Ticket_Out_Var, lvItem[iLVIndex].SubItems[4]);
                    _Utils.AddNumericValue(item.EftIn_Var, lvItem[iLVIndex].SubItems[5]);
                    _Utils.AddNumericValue(item.EftOut_Var, lvItem[iLVIndex].SubItems[6]);
                    _Utils.AddNumericValue(item.Handpay_Var, lvItem[iLVIndex].SubItems[7]);
                    _Utils.AddNumericValue(item.Progressive_Var, lvItem[iLVIndex].SubItems[8]);

                    lvItem[iLVIndex++].Tag = item.Collection_Day;
                }

                lvwVarianceHistory.Items.AddRange(lvItem);

                lvwVarianceHistory.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                lvwVarianceHistory.Columns[9].Width = 0;
                lvwVarianceHistory.Columns[10].Width = 0;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadCashDeskTransaction()
        {
            try
            {
                int iLVIndex = 0;

                lvwCashDeskTrans.Items.Clear();

                _lstCashDeskTransaction = objBiz.GetTreasuryDetails(_Collection_ID);

                lvItem = new ListViewItem[_lstCashDeskTransaction.Count];

                foreach (var item in _lstCashDeskTransaction)
                {
                    lvItem[iLVIndex] = new ListViewItem(new string[] { _Utils.GetRegionalDate(item.Treasury_Date),
                        item.Treasury_Time,
                        item.Treasury_Type,
                        item.Treasury_User,                        
                        item.Treasury_Issued_User,
                        "",
                        item.Treasury_Reason
                    });

                    lvItem[iLVIndex].ImageIndex = 0;
                    _Utils.AddNumericValue(item.Treasury_Amount, lvItem[iLVIndex].SubItems[5]);
                    lvItem[iLVIndex++].Tag = item.Treasury_Date;

                }

                lvwCashDeskTrans.Items.AddRange(lvItem);

                lvwCashDeskTrans.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion User Defined Function

        private void btn_ModifyDeclaration_Click(object sender, EventArgs e)
        {
            try
            {
                CoreLib.Win32.Win32Extensions.ShowDialogExAndDestroy(new frmModifyDeclaration(_Collection_ID, _Site_id), this);
                this.LoadData(false);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void lvDropBreakDown_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {


                if (e.Column == _lvwColumnSorter.SortColumn)
                {
                    // Reverse the current sort direction for this column.
                    if (_lvwColumnSorter.Order == System.Windows.Forms.SortOrder.Ascending)
                    {
                        _lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Descending;
                    }
                    else
                    {
                        _lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                    }
                }
                else
                {
                    // Set the column number that is to be sorted; default to ascending.
                    _lvwColumnSorter.SortColumn = e.Column;
                    _lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                }
                ListView lst_view = sender as ListView;
                lst_view.Sort();
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }

        }

        private void lvwCashDeskTrans_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {


                if (e.Column == _lvwColumnSorter.SortColumn)
                {
                    // Reverse the current sort direction for this column.
                    if (_lvwColumnSorter.Order == System.Windows.Forms.SortOrder.Ascending)
                    {
                        _lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Descending;
                    }
                    else
                    {
                        _lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                    }
                }
                else
                {
                    // Set the column number that is to be sorted; default to ascending.
                    _lvwColumnSorter.SortColumn = e.Column;
                    _lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                }
                ListView lst_view = sender as ListView;
                lst_view.Sort();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }

        }

        private void lvwPositionEvents_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {


                if (e.Column == _lvwColumnSorter.SortColumn)
                {
                    // Reverse the current sort direction for this column.
                    if (_lvwColumnSorter.Order == System.Windows.Forms.SortOrder.Ascending)
                    {
                        _lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Descending;
                    }
                    else
                    {
                        _lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                    }
                }
                else
                {
                    // Set the column number that is to be sorted; default to ascending.
                    _lvwColumnSorter.SortColumn = e.Column;
                    _lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                }
                ListView lst_view = sender as ListView;
                lst_view.Sort();
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }
        }

        private void lvwVarianceHistory_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {

                if (e.Column == _lvwColumnSorter.SortColumn)
                {
                    // Reverse the current sort direction for this column.
                    if (_lvwColumnSorter.Order == System.Windows.Forms.SortOrder.Ascending)
                    {
                        _lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Descending;
                    }
                    else
                    {
                        _lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                    }
                }
                else
                {
                    // Set the column number that is to be sorted; default to ascending.
                    _lvwColumnSorter.SortColumn = e.Column;
                    _lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                }
                ListView lst_view = sender as ListView;
                lst_view.Sort();

            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }
        }
        private void Setsymbol()
        {
            try
            {
                for (int i = 1; i <= 9; i++)
                {
                    SetCurrencySymbol(i);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SetCurrencySymbol(int index)
        {
            try
            {
                this.lvwVarianceHistory.Columns[index].Text = CombineSymbolWithText(this.lvwVarianceHistory.Columns[index].Text);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void lvwVarianceHistory_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if ((e.ColumnIndex == 9)||(e.ColumnIndex==10))
            {
                e.Cancel = true;
                e.NewWidth = lvwVarianceHistory.Columns[e.ColumnIndex].Width;
            }
        }

        

        private void txtUser_MouseHover(object sender, EventArgs e)
        {
            UserNameToolTip = new ToolTip();
            UserNameToolTip.InitialDelay = 0;
            UserNameToolTip.Show(UserToolTipText, txtUser, 0);
        }

        private void txtUser_MouseLeave(object sender, EventArgs e)
        {
            
        }
    }

    public enum lstBreakDownColumns : int
    {
        Total = 1,
        D1000,
        D500,
        D200,
        D100,
        D50,
        D20,
        D10,
        D5,
        D2,
        D1,
        CoinsIn,
        CoinsOut,
        TotalCoins,
        VoucherIn,
        VoucherOut,

        Voucher,
        Attendantpay,
        ApayProgressive,
        EFTIn,
        EFTOut,
        EFT,
        PromoCashableIn,
        PromoNonCashableIn
    }

}
