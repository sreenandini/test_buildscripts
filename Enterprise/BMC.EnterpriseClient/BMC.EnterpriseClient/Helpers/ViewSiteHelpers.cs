using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using System.Data;
using System.Threading;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseBusiness.Business;
using System.Drawing;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using System.Data.Linq;
using System.ComponentModel;
using BMC.EnterpriseClient.Views;
using BMC.CoreLib.Win32;
using System.Collections;
using BMC.Common;
using BMC.Reports;


namespace BMC.EnterpriseClient.Helpers
{
    public interface IViewSiteReportViewer
    {
        ListView View { get; set; }
        bool SkipRefresh { get; }
        bool HasFetchTimeoutError { get; }

        void InitReport();
        object FetchData(out int count);
        object FetchData(IAsyncProgress ap, out int count);
        ListViewItem FillReport(object source, int rowIndex);

        void ExportReport();
        DialogResult ShowDialog(object tag);
    }

    public interface IViewSiteDropViewer : IViewSiteReportViewer
    {
        int SiteId { get; set; }
        int BarPositionId { get; set; }
        int Records { get; set; }
        int Weeks { get; set; }
        int Periods { get; set; }
        string BarPosition { get; set; }
        string SiteName { get; set; }
        string Company { get; set; }
        string SubCompany { get; set; }

    }

    public interface IViewSiteAssetViewer : IViewSiteReportViewer
    {
        int SiteId { get; set; }
        int BarPositionId { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        int CategoryId { get; set; }
        string BarPosition { get; set; }
        string SiteName { get; set; }
        string Company { get; set; }
        string SubCompany { get; set; }
        String Category { get; set; }
    }

    public abstract class ViewSiteReportViewerBase : DisposableObject, IViewSiteReportViewer
    {
        protected ViewSitesBusiness _business = new ViewSitesBusiness();
        protected AssetReportBiz _Asset = AssetReportBiz.CreateInstance();
        protected frmAdminUtilities _adminUtilities = new frmAdminUtilities();
        protected bool _isAFTIncludedInCalculation = false;
        protected bool _hasFetchTimeoutError = false;

        public string CurrencyForViewSites { get; private set; }

        protected ViewSiteReportViewerBase()
        {
            CurrencyForViewSites = new System.Globalization.RegionInfo(BMC.Common.ConfigurationManagement.ConfigManager.Read("GetDefaultCultureForCurrency")).CurrencySymbol;
        }

        protected string CombineTextWithCurrencySymbol(string text)
        {
            return CurrencyForViewSites + " " + text;
        }

        #region IViewSiteReportViewer Members

        public ListView View { get; set; }

        public virtual bool SkipRefresh { get { return false; } }

        public bool HasFetchTimeoutError { get { return _hasFetchTimeoutError; } }

        public void InitReport()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "InitReport");

            try
            {
                this.View.Items.Clear();
                this.View.Columns.Clear();
                this.InitReportInternal();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        protected abstract void InitReportInternal();

        public object FetchData(out int count)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "FetchData");
            object result = null;
            count = 0;
            _hasFetchTimeoutError = false;

            try
            {
                result = this.FetchDataInternal(null, out count);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public object FetchData(IAsyncProgress ap, out int count)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "FetchData");
            object result = null;
            count = 0;

            try
            {
                result = this.FetchDataInternal(ap, out count);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        protected abstract object FetchDataInternal(IAsyncProgress ap, out int count);

        protected virtual bool CanAdd(object source, int rowIndex) { return true; }

        public ListViewItem FillReport(object source, int rowIndex)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "InitReport");
            ListViewItem result = null;

            try
            {
                _isAFTIncludedInCalculation = TypeSystem.GetValueBoolSimple(AdminBusiness.GetSetting("IsAFTIncludedInCalculation", "false"));
                if (this.CanAdd(source, rowIndex))
                {
                    result = new ListViewItem();
                    result.UseItemStyleForSubItems = false;

                    if (this.View.Columns.Count > 1)
                    {
                        for (int i = 1; i < this.View.Columns.Count; i++)
                        {
                            result.SubItems.Add("x");
                        }
                    }
                    this.FillReportInternal(result, source, rowIndex);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        protected abstract void FillReportInternal(ListViewItem item, object source, int rowIndex);

        public virtual void ExportReport()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "InitReport");

            try
            {
                this.ExportReportInternal();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        protected virtual void ExportReportInternal() { }

        public DialogResult ShowDialog(object tag)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "ShowDialog");

            try
            {
                Form dialog = this.GetDialogInternal(tag);
                if (dialog != null)
                {
                    return dialog.ShowDialogExResultAndDestroy(MainForm.ActiveForm);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return DialogResult.None;
        }

        protected virtual Form GetDialogInternal(object tag) { return null; }

        #endregion

    }
    public abstract class ViewSiteDropBase : ViewSiteReportViewerBase, IViewSiteDropViewer
    {
        #region IViewSiteDropViewer Members

        public int SiteId { get; set; }

        public int BarPositionId { get; set; }

        public int Records { get; set; }

        public int Weeks { get; set; }

        public int Periods { get; set; }

        public string BarPosition { get; set; }

        public string SiteName { get; set; }

        public string Company { get; set; }

        public string SubCompany { get; set; }

        #endregion

        public override void ExportReport()
        {
            try
            {
                GlobalHelper.ExportTocsv(this.View);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }

    public abstract class ViewSiteAssetBase : ViewSiteReportViewerBase, IViewSiteAssetViewer
    {
        #region IViewSiteDropViewer Members

        public int SiteId { get; set; }
        public int BarPositionId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int CategoryId { get; set; }
        public string BarPosition { get; set; }
        public string SiteName { get; set; }
        public string Company { get; set; }
        public string SubCompany { get; set; }
        public String Category { get; set; }
        #endregion
    }

    public class ViewSiteCollectionDetails : ViewSiteDropBase
    {
        private const int COLLECTION_SOURCE_COL_INPUT_PER = 1;
        private const int COLLECTION_SOURCE_HHT_PER = 2;
        private const int COLLECTION_SOURCE_EDI_PER = 3;
        private const int COLLECTION_SOURCE_IMPORT_PER = 4;
        private const int COLLECTION_SOURCE_HFCMS2_RDC_PER = 5;
        private const int COLLECTION_SOURCE_HFCMS2_WEB_BASIC_PER = 6;
        private const int COLLECTION_SOURCE_HFCMS2_WEB_BASIC_FINALISED_PER = 7;
        private const int COLLECTION_SOURCE_HFCMS2_WEB_ADVANCED_PER = 8;
        private const int COLLECTION_SOURCE_ARCADE_PER = 9;

        protected override void InitReportInternal()
        {
            this.View.Columns.AddRange(
                new ColumnHeader[] 
                { 
                    new ColumnHeader() { TextAlign = HorizontalAlignment.Left, Tag = "Key_Date" },
                    new ColumnHeader() { Tag= "Key_Game", TextAlign = HorizontalAlignment.Left  },
                    new ColumnHeader() { Tag= "Key_Operator", TextAlign = HorizontalAlignment.Left },
                    new ColumnHeader() { Tag= "Key_Depot", TextAlign = HorizontalAlignment.Left },
                    new ColumnHeader() { Tag= "Key_Days", TextAlign = HorizontalAlignment.Right },
                    
                    new ColumnHeader() {  Tag= "Key_DailyAvg", TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Tag= "Key_PercentHold", TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_WinLoss")), TextAlign = HorizontalAlignment.Right },

                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_Bills")), TextAlign = HorizontalAlignment.Right},
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_Coins")), TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() {Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_Vouchers")), TextAlign = HorizontalAlignment.Right },
                    //new ColumnHeader() { Tag= "Promo Cashable In", TextAlign = HorizontalAlignment.Right, Tag = typeof(NumericWrapperComparer) },
                    //new ColumnHeader() { Tag= "Promo NonCashable In", TextAlign = HorizontalAlignment.Right, Tag = typeof(NumericWrapperComparer) },

                    new ColumnHeader() {Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_Attendantpay")), TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_Shortpay")), TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_BillsVar")), TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_CoinVar")), TextAlign = HorizontalAlignment.Right},
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_VoucherVar")), TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_AttendantpayVar")), TextAlign = HorizontalAlignment.Right},
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_WinLossVar")), TextAlign = HorizontalAlignment.Right},

                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_Fills")), TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_RefundsHeader")), TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Tag= "Key_OperatorShare", TextAlign = HorizontalAlignment.Right},
                    new ColumnHeader() { Tag= "Key_CompanyShare", TextAlign = HorizontalAlignment.Right},
                    new ColumnHeader() { Tag= "Key_SiteShare", TextAlign = HorizontalAlignment.Right},
                    new ColumnHeader() { Tag= "Key_Others", TextAlign = HorizontalAlignment.Right},
                    new ColumnHeader() { Tag= "Key_AMLD", TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Tag= "Key_Remarks", TextAlign = HorizontalAlignment.Right},
                    new ColumnHeader() { Tag= "Key_PeriodEnd", TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Tag= "Key_TimeLag", TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Tag= "Key_TargetVar", TextAlign = HorizontalAlignment.Right },

                    new ColumnHeader() { Tag= "Key_OrderDate", TextAlign = HorizontalAlignment.Right},
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_MetBet")), TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_MetWin")), TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_MetResult")), TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Tag= "Key_WeekEnd", TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Tag= "Key_SubCompany", TextAlign = HorizontalAlignment.Left },
                    new ColumnHeader() { Tag= "Key_Company", TextAlign = HorizontalAlignment.Left },
                    new ColumnHeader() { Tag= "Key_PayoutPercent", TextAlign = HorizontalAlignment.Right },

                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_Handle")), TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Tag= "Key_Index", TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Tag= "Key_Pace", TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Tag= "Key_Performance", TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Tag= "Key_GPT", TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Tag= "Key_Stakes", TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Tag= "Key_Payout", TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Tag= "Key_Transactions", TextAlign = HorizontalAlignment.Right },

                    new ColumnHeader() { Tag= "Key_StakesPerTrans", TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Tag= "Key_Margin", TextAlign = HorizontalAlignment.Right },
                });
        }

        protected override object FetchDataInternal(IAsyncProgress ap, out int count)
        {
            VSDropPositionsEntity entity = _business.GetDropPositions(this.SiteId, this.BarPositionId, this.Records, this.Weeks, this.Periods);
            count = entity.Count;
            return entity;
        }

        protected override bool CanAdd(object source, int rowIndex)
        {
            VSDropPositionEntity entity = ((VSDropPositionsEntity)source)[rowIndex];
            return _adminUtilities.CustomerAccessIsDepotVisible(entity.Depot_ID.SafeValue());
        }

        protected override void FillReportInternal(ListViewItem item, object source, int rowIndex)
        {
            VSDropPositionEntity entity = ((VSDropPositionsEntity)source)[rowIndex];
            item.Tag = entity;

            // Icon
            int iconIndex = 0;
            int collSrc = entity.Collection_Source.SafeValue();
            if (collSrc == COLLECTION_SOURCE_HFCMS2_RDC_PER ||
                collSrc == COLLECTION_SOURCE_HFCMS2_WEB_BASIC_PER)
            {
                //Unfinalised remote
                iconIndex = 13;  //white/red diag
            }
            else if (!entity.Batch_Received_All.SafeValue() && entity.Batch_ID > 0)
            {
                //Batch not closed
                iconIndex = 13;  //white/red diag
            }
            else if (entity.Period_End_ID != 0)
            {
                if (entity.Collection_Terms_Invalid.SafeValue())
                    iconIndex = 10;  //orange
                else
                    iconIndex = 3; //dark green
            }
            else if (entity.COLLECTION_REPLACEMENT.SafeValue())
            {
                iconIndex = 7; //yellow
            }
            else if (entity.Collection_Terms_Invalid.SafeValue())
            {
                if (!entity.Collection_Terms_Invalid_Ignore.SafeValue())
                    iconIndex = 5;  //red
                else
                    iconIndex = 12; //light red
            }
            else if (entity.Collection_Processed_Through_Terms.SafeValue())
            {
                if (entity.EDI_Import_Log_ID == 0)//If manual input
                    iconIndex = 9;  //purple
                else
                    iconIndex = 8; //pink
            }
            else
            {
                iconIndex = 6;
            }

            // set the icon
            if (iconIndex > 0)
            {
                item.ImageIndex = iconIndex - 1;
            }

            item.Text = _adminUtilities.GetRegionalDate(entity.Collection_Date);
            int subIndex = 1;
            item.SubItems[subIndex++].Text = (entity.GameName);
            item.SubItems[subIndex++].Text = (entity.Operator_Name);
            item.SubItems[subIndex++].Text = (entity.Depot_Name);
            int collDays = entity.Collection_Days;
            string collDaysString = new String(' ', (4 - Math.Max(4, collSrc.ToString().Length)));
            item.SubItems[subIndex++].Text = collDaysString + collDays.ToString(); // days 

            // DecWinOrLoss
            double DecWinOrLoss = entity.Declared_Notes +
                                    entity.DecTicketBalance.SafeValue() -
                                    entity.DecHandpay.SafeValue() +
                                    entity.Net_Coin.SafeValue() -
                                    (SettingsEntity.AddShortpayInVoucherOut ? 0.0 : entity.Shortpay.SafeValue()) +
                                    (_isAFTIncludedInCalculation ? (entity.DecEftIn.SafeValue() / 100.00) : 0) -
                                    (_isAFTIncludedInCalculation ? (entity.DecEftOut.SafeValue() / 100.00) : 0);
            // MeterWinOrLoss
            double MeterWinOrLoss = entity.RDC_Notes +
                                    entity.DecTicketBalance.SafeValue() -
                                    entity.Ticket_Var.SafeValue() -
                                    entity.RDCHandpay.SafeValue() +
                                    entity.RDC_Coins +
                                    (_isAFTIncludedInCalculation ? (entity.EftIn.SafeValue() / 100.00) : 0) -
                                    (_isAFTIncludedInCalculation ? (entity.EftOut.SafeValue() / 100.00) : 0);

            // Daily Av
            double winloss = entity.Cash_Take.SafeValue();
            winloss += (_isAFTIncludedInCalculation ? (entity.DecEftIn.SafeValue() / 100.00) : 0) -
                       (_isAFTIncludedInCalculation ? (entity.DecEftOut.SafeValue() / 100.00) : 0);
            if (collDays > 0)
            {
                _adminUtilities.AddNumericValue((winloss / ((double)collDays)), item.SubItems[subIndex++], "###,##0.00");
            }
            else
            {
                _adminUtilities.AddNumericValue(0, item.SubItems[subIndex++]);
            }

            // Hold
            double handle = entity.Handle.SafeValue();
            double casino = 0;
            if (handle > 0)
            {
                casino = ((handle - MeterWinOrLoss) / handle) * 100.00;
                double hold = (100.00 - casino);
                _adminUtilities.AddNumericValue(hold, item.SubItems[subIndex++], "###,##0.00");
            }
            else
            {
                _adminUtilities.AddNumericValue(0, item.SubItems[subIndex++]);
            }

            _adminUtilities.AddNumericValue(winloss, item.SubItems[subIndex++], "#,##0.00");
            _adminUtilities.AddNumericValue(entity.Collection_declared_Notes, item.SubItems[subIndex++], "###,##0");

            _adminUtilities.AddNumericValue(entity.Net_Coin, item.SubItems[subIndex++]);
            _adminUtilities.AddNumericValue(entity.DecTicketBalance, item.SubItems[subIndex++]);
            //_adminUtilities.AddNumericValue(entity.PromoCashableIn, item.SubItems[subIndex++]);
            //_adminUtilities.AddNumericValue(entity.PromoNonCashableIn, item.SubItems[subIndex++]);
            _adminUtilities.AddNumericValue(entity.DecHandpay, item.SubItems[subIndex++]);
            _adminUtilities.AddNumericValue(entity.Shortpay, item.SubItems[subIndex++]);
            _adminUtilities.AddNumericValue(entity.Note_Var, item.SubItems[subIndex++], "####0");
            _adminUtilities.AddNumericValue(entity.Coin_Var, item.SubItems[subIndex++]);
            _adminUtilities.AddNumericValue(entity.Ticket_Var, item.SubItems[subIndex++]);
            _adminUtilities.AddNumericValue(entity.Handpay_Var, item.SubItems[subIndex++]);

            _adminUtilities.AddNumericValue((DecWinOrLoss - MeterWinOrLoss), item.SubItems[subIndex++], "#,###,##0.00");
            _adminUtilities.AddNumericValue(entity.Refills, item.SubItems[subIndex++], "###,##0.00");
            _adminUtilities.AddNumericValue(entity.Refunds, item.SubItems[subIndex++], "###,##0.00");
            _adminUtilities.AddNumericValue(entity.Collection_Supplier_Share, item.SubItems[subIndex++], "###,##0.00");
            _adminUtilities.AddNumericValue(entity.Collection_Company_Share, item.SubItems[subIndex++], "###,##0.00");
            _adminUtilities.AddNumericValue(entity.Collection_Location_Share, item.SubItems[subIndex++], "###,##0.00");
            _adminUtilities.AddNumericValue(entity.Collection_Other_Share, item.SubItems[subIndex++], "###,##0.00");
            _adminUtilities.AddNumericValue(entity.Collection_AMLD, item.SubItems[subIndex++], "###,##0.00");

            item.SubItems[subIndex++].Text = entity.Remarks;
            _adminUtilities.AddNumericValue(entity.Period_End_ID, item.SubItems[subIndex++], "####0");
            if (entity.COLLECTION_REPLACEMENT.SafeValue())
            {
                item.SubItems[subIndex++].ForeColor = Color.FromArgb(255, 255, 0);
            }
            item.SubItems[subIndex++].Text = entity.Lag.ToString();
            _adminUtilities.AddNumericValue(entity.TargetVariance, item.SubItems[subIndex++], "###0.00");

            DateTime collDate = TypeSystem.GetValueDateTime(entity.Collection_Date, "dd MMM yyyy");
            if (collDate != DateTime.MinValue)
            {
                DateTime startDate = TypeSystem.GetValueDateTime(entity.Machine_Start_Date, "dd MMM yyyy");
                _adminUtilities.AddNumericValue((collDate - startDate).Days <= 0 ? 1 : (collDate - startDate).Days, item.SubItems[subIndex++], "", false);
            }
            else
            {
                item.SubItems[subIndex++].Text = "999999999";
            }

            _adminUtilities.AddNumericValue(entity.RDCIn, item.SubItems[subIndex++], "###,##0.00");
            _adminUtilities.AddNumericValue(entity.RDCOut, item.SubItems[subIndex++], "###,##0.00");
            _adminUtilities.AddNumericValue(entity.RDCCash, item.SubItems[subIndex++], "###,##0.00");

            _adminUtilities.AddNumericValue(entity.Week_End_ID, item.SubItems[subIndex++], "####0");
            item.SubItems[subIndex++].Text = entity.Sub_Company_Name;
            item.SubItems[subIndex++].Text = entity.Company_Name;

            if (handle > 0)
            {
                _adminUtilities.AddNumericValue(casino, item.SubItems[subIndex++], "###,##0.00");
            }
            else if (entity.MeterCashIn > 0)
            {
                _adminUtilities.AddNumericValue((entity.MeterCashOut / entity.MeterCashIn), item.SubItems[subIndex++], "##0.00%");
            }
            else
            {
                _adminUtilities.AddNumericValue(0, item.SubItems[subIndex++], "-");
            }

            _adminUtilities.ZeroDash(entity.VTP, item.SubItems[subIndex++], 0);

            float pIndex = entity.PIndex.SafeValue();
            float pace = 0;
            if (entity.PacePrev9Days != 0)
            {
                int calc = ((entity.PacePrev9Cash / entity.PacePrev9Days) * entity.PaceDays);
                if (calc != 0)
                {
                    pace = (entity.PaceCash / calc);
                }
            }
            _adminUtilities.ZeroDash((pIndex * 100.00), item.SubItems[subIndex++], 2);
            _adminUtilities.ZeroDash((pace * 100.00), item.SubItems[subIndex++], 2);
            _adminUtilities.ZeroDash(((pIndex - pace) * 100.00), item.SubItems[subIndex++], 2);

            _adminUtilities.ZeroDash(entity.Collection_GPT, item.SubItems[subIndex++], 2);
            _adminUtilities.ZeroDash(entity.Collection_FOBT_Stakes, item.SubItems[subIndex++], 2);
            _adminUtilities.ZeroDash(entity.Collection_FOBT_Payout, item.SubItems[subIndex++], 2);
            _adminUtilities.ZeroDash(entity.Collection_Transactions, item.SubItems[subIndex++], 0);
            _adminUtilities.ZeroDashHFDiv(entity.Collection_FOBT_Stakes, entity.Collection_Transactions, item.SubItems[subIndex++], 2);
            _adminUtilities.ZeroDashHFDiv(entity.CashCollected.SafeValue(), entity.Collection_FOBT_Stakes, item.SubItems[subIndex++], 2);
        }

        protected override Form GetDialogInternal(object tag)
        {
            VSDropPositionEntity entity = tag as VSDropPositionEntity;
            return new CollHistoryBreakDownForm(entity.Collection_ID, this.SiteId, this.SiteName);
        }
    }

    public class ViewSiteCollectionBatch : ViewSiteDropBase
    {
        public override bool SkipRefresh
        {
            get
            {
                return true;
            }
        }

        protected override void InitReportInternal()
        {
            this.View.Columns.AddRange(
                   new ColumnHeader[] 
                { 
                    new ColumnHeader() { Tag= "Key_HQBatchNo", TextAlign = HorizontalAlignment.Right},
                    new ColumnHeader() { Tag= "Key_SiteBatchNo", TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Tag= "Key_GamingDa", TextAlign = HorizontalAlignment.Left },
                    new ColumnHeader() { Tag= "Key_collectionDT", TextAlign = HorizontalAlignment.Left },
                    
                    new ColumnHeader() { Tag= "Key_Qty", TextAlign = HorizontalAlignment.Right},      //Collections was renamed to Qty by Sudarsan S on 01-04-2008 for normalisation
                    new ColumnHeader() { Tag= "Key_Name", TextAlign = HorizontalAlignment.Left },
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_WinLoss")), TextAlign = HorizontalAlignment.Right },                    
                    
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_Bills")), TextAlign = HorizontalAlignment.Right },    //Notes was renamed to Bills by Sudarsan S on 01-04-2008 for normalisation
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_Coins")), TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_Voucherin")), TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_voucherOut")), TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_PromoCashableIn")), TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_PromoNonCashableIn")), TextAlign = HorizontalAlignment.Right },

                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_Fills")), TextAlign = HorizontalAlignment.Right },      //Refills was renamed to Fills by Sudarsan S on 01-04-2008 for normalisation
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_RefundsHeader")), TextAlign = HorizontalAlignment.Right},
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_Attendantpay")), TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_Shortpay")), TextAlign = HorizontalAlignment.Right},
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_VoidVoucher")), TextAlign = HorizontalAlignment.Right },
                    
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_BillsVar")), TextAlign = HorizontalAlignment.Right },       //Notes Var was renamed to Bill Var by Sudarsan S on 01-04-2008 for normalisation
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_CoinVar")), TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_VoucherVar")), TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_AttendantpayVar")), TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_WinLossVar")), TextAlign = HorizontalAlignment.Right },      //Overall Var was renamed to Win/Loss Var by Sudarsan S on 01-04-2008 for normalisation
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_AttnPayProgressive")), TextAlign = HorizontalAlignment.Right },      //Progressive was renamed to H//pay Progressive by Sudarsan S on 01-04-2008 for normalisation
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_AttnPayProgVar")), TextAlign = HorizontalAlignment.Right },
                });
        }

        protected override object FetchDataInternal(IAsyncProgress ap, out int count)
        {
            count = 0;
            VSDropBatches2Entity result = new VSDropBatches2Entity();
            VSLastBatchIdsEntity batchIds = _business.GetLastBatchIds(this.SiteId, this.Weeks, this.Periods);
            int count2 = batchIds.Count;

            try
            {
                if (count2 > 0)
                {
                    IAsyncProgress2 o = ap as IAsyncProgress2;
                    o.InitializeProgress(1, count2);
                    int proIdx = 0;

                    foreach (VSLastBatchIdEntity batchId in batchIds)
                    {
                        ++proIdx;
                        int proPerc = (int)(((float)proIdx / (float)count2) * 100.0);
                        o.UpdateStatusProgress(proIdx - 1, string.Format(BMC.Common.ResourceExtensions.GetResourceTextByKey(null, "Key_FetchDropDetails"), batchId.Batch_ID.ToString(), proIdx, count2.ToString(), proPerc));

                        //o.UpdateStatusProgress(proIdx - 1, "Fetching Drop Batch Details for Batch [" +
                        //                    batchId.Batch_ID.ToString() + "] . . . : " +
                        //                    proIdx + " of " + count2.ToString() +
                        //                    " (" + proPerc + "%)");

                        VSDropBatches2Entity batches = _business.GetDropBatches(this.SiteId, batchId.Batch_ID);
                        if (ap.ExecutorService.WaitForShutdown(1)) break;

                        foreach (VSDropBatch2Entity batch in batches)
                        {
                            result.Add(batch);

                            if (ap.ExecutorService.WaitForShutdown(1)) break;
                            Thread.Sleep(1);
                        }

                        if (ap.ExecutorService.IsShutdown) break;
                        Thread.Sleep(1);
                    }
                }
            }
            finally
            {
                count = result.Count;
            }
            return result;
        }

        protected override void FillReportInternal(ListViewItem item, object source, int rowIndex)
        {
            VSDropBatch2Entity entity = ((VSDropBatches2Entity)source)[rowIndex];
            item.Tag = entity;
            int subIndex = 1;

            if (entity.Batch_ID.IsValid())
                item.Text = entity.Batch_ID.SafeValue().ToString();
            else
                item.Text = "No Batch";

            string batchRef = string.Empty;
            if (!entity.BatchRef.IsEmpty())
            {
                string[] values = entity.BatchRef.SplitString(',', 2);
                if (values != null)
                {
                    batchRef = values[1];
                }
            }
            item.SubItems[subIndex++].Text = batchRef;
            item.SubItems[subIndex++].Text = _adminUtilities.GetRegionalDate(entity.BatchDate);
            item.SubItems[subIndex++].Text = _adminUtilities.GetRegionalDate(entity.Batch_Date_performed) + " " +
                                                TypeSystem.GetValueTimeSpan(entity.Batch_Time_performed).ToString();
            item.SubItems[subIndex++].Text = entity.BatchCount.ToString();
            item.SubItems[subIndex++].Text = (!entity.Batch_Name.IsEmpty() ? entity.Batch_Name : "Not Available");

            // DecWinOrLoss
            double DecWinOrLoss = entity.Notes.SafeValue() +
                                    entity.DecTicketBalance.SafeValue() -
                                    entity.DecHandpay.SafeValue() +
                                    entity.Net_Coin.SafeValue() -
                                    entity.Shortpay.SafeValue() +
                                    (_isAFTIncludedInCalculation ? (entity.DecEftIn.SafeValue() / 100.00) : 0) -
                                    (_isAFTIncludedInCalculation ? (entity.DecEftOut.SafeValue() / 100.00) : 0);
            // MeterWinOrLoss
            double MeterWinOrLoss = entity.RDC_Notes.SafeValue() +
                                    entity.DecTicketBalance.SafeValue() -
                                    entity.TicketVar.SafeValue() -
                                    (SettingsEntity.AddShortpayInVoucherOut ? entity.Shortpay.SafeValue() : 0.00) -
                                    entity.RDCHandpay.SafeValue() +
                                    entity.RDC_Coins.SafeValue() +
                                    (_isAFTIncludedInCalculation ? (entity.EftIn.SafeValue() / 100.00) : 0) -
                                    (_isAFTIncludedInCalculation ? (entity.EftOut.SafeValue() / 100.00) : 0);
            _adminUtilities.AddNumericValue(DecWinOrLoss, item.SubItems[subIndex++], "#,##0.00");

            _adminUtilities.AddNumericValue(entity.Notes.SafeValue(), item.SubItems[subIndex++], "###,##0");
            _adminUtilities.AddNumericValue(entity.Coins.SafeValue(), item.SubItems[subIndex++]);
            _adminUtilities.AddNumericValue(entity.TicktesIn.SafeValue(), item.SubItems[subIndex++]);
            _adminUtilities.AddNumericValue(entity.TicktesOut.SafeValue(), item.SubItems[subIndex++]);
            _adminUtilities.AddNumericValue(entity.PromoCashableIn.SafeValue(), item.SubItems[subIndex++]);
            _adminUtilities.AddNumericValue(entity.PromoNonCashableIn.SafeValue(), item.SubItems[subIndex++]);
            _adminUtilities.AddNumericValue(entity.Refills.SafeValue(), item.SubItems[subIndex++]);
            _adminUtilities.AddNumericValue(entity.Refunds.SafeValue(), item.SubItems[subIndex++]);
            _adminUtilities.AddNumericValue(entity.Handpay.SafeValue(), item.SubItems[subIndex++]);
            _adminUtilities.AddNumericValue(entity.Shortpay.SafeValue(), item.SubItems[subIndex++]);

            _adminUtilities.AddNumericValue(entity.Void.SafeValue(), item.SubItems[subIndex++]);
            _adminUtilities.AddNumericValue(entity.NotesVar.SafeValue(), item.SubItems[subIndex++], "####0");
            _adminUtilities.AddNumericValue(entity.CoinVar.SafeValue(), item.SubItems[subIndex++]);
            _adminUtilities.AddNumericValue(entity.TicketVar.SafeValue(), item.SubItems[subIndex++]);
            _adminUtilities.AddNumericValue(entity.HandpayVar.SafeValue(), item.SubItems[subIndex++]);

            _adminUtilities.AddNumericValue((DecWinOrLoss - MeterWinOrLoss), item.SubItems[subIndex++], "#,##0.00");
            _adminUtilities.AddNumericValue(entity.Progressive_Value_Declared.SafeValue(), item.SubItems[subIndex++]);
            _adminUtilities.AddNumericValue(entity.Progressive_Value_Variance.SafeValue(), item.SubItems[subIndex++]);

        }

        protected override Form GetDialogInternal(object tag)
        {
            VSDropBatch2Entity entity = tag as VSDropBatch2Entity;
            return new frmCollectionHistory(entity.Batch_ID.SafeValue(), this.SiteId, 0, FormCollectionHistory_Mode.Batch, this.SiteName, string.Empty, string.Empty);
        }
    }

    public class ViewSiteCollectionWeek : ViewSiteDropBase
    {
        public override bool SkipRefresh
        {
            get
            {
                return true;
            }
        }

        protected override void InitReportInternal()
        {
            this.View.Columns.AddRange(
                      new ColumnHeader[] 
                { 
                    new ColumnHeader() { Tag= "Key_Week", TextAlign = HorizontalAlignment.Left },
                    new ColumnHeader() { Tag= "Key_Date", TextAlign = HorizontalAlignment.Left },
                    new ColumnHeader() { Tag= "Key_Qty", TextAlign = HorizontalAlignment.Right },              //No of m/c//s renamed to Qty by Sudarsan S on 02-04-2008 for Normalisation
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_WinLoss")), TextAlign = HorizontalAlignment.Right },
                    
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_Bills")), TextAlign = HorizontalAlignment.Right },    //Notes  renamed to Bills by Sudarsan S on 02-04-2008 for Normalisation
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_Coins")), TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_Voucherin")), TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() {Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_voucherOut")), TextAlign = HorizontalAlignment.Right},

                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_PromoCashableIn")), TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_PromoNonCashableIn")), TextAlign = HorizontalAlignment.Right },


                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_Fills")), TextAlign = HorizontalAlignment.Right },   //Refills  renamed to Fills by Sudarsan S on 02-04-2008 for Normalisation
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_RefundsHeader")), TextAlign = HorizontalAlignment.Right},
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_Attendantpay")), TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_Shortpay")), TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() {Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_VoidVoucher")), TextAlign = HorizontalAlignment.Right},
                    
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_BillsVar")), TextAlign = HorizontalAlignment.Right },    //Notes Var renamed to Bills Var by Sudarsan S on 02-04-2008 for Normalisation
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_CoinVar")), TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_VoucherVar")), TextAlign = HorizontalAlignment.Right},
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_AttendantpayVar")), TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_OverAllVar")), TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_AttnPayProgressive")), TextAlign = HorizontalAlignment.Right },    //Progressive Value renamed to Handpay Progressive Value by Sudarsan S on 02-04-2008 for Normalisation / then removed "value"
                    new ColumnHeader() { Text= CombineTextWithCurrencySymbol(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_AttendantpayProgressiveVar")), TextAlign = HorizontalAlignment.Right },  //Progressive Var renamed to H//pay Progressive Var by Sudarsan S on 02-04-2008 for Normalisation
                });
        }

        protected override object FetchDataInternal(IAsyncProgress ap, out int count)
        {
            count = 0;
            int weekId = -1;
            if (this.Weeks != -1) { weekId = this.Weeks; }
            if (this.Periods != -1) { weekId = this.Periods; }
            VSDropWeeksEntity result = new VSDropWeeksEntity();
            VSLastWeekIdsEntity WeekIds = _business.GetLastWeekIds(this.SiteId, weekId);
            int count2 = WeekIds.Count;

            try
            {
                if (count2 > 0)
                {
                    IAsyncProgress2 o = ap as IAsyncProgress2;
                    o.InitializeProgress(1, count2);
                    int proIdx = 0;

                    foreach (VSLastWeekIdEntity WeekId in WeekIds)
                    {
                        ++proIdx;
                        int proPerc = (int)(((float)proIdx / (float)count2) * 100.0);

                        o.UpdateStatusProgress(proIdx - 1, string.Format(BMC.Common.ResourceExtensions.GetResourceTextByKey(null, "Key_FetchWeekDetails"), WeekId.Calendar_Week_ID.ToString(), proIdx, count2.ToString(), proPerc));
                        //o.UpdateStatusProgress(proIdx - 1, "Fetching Drop Week Details for Week [" +
                        //                    WeekId.Calendar_Week_ID.ToString() + "] . . . : " +
                        //                    proIdx + " of " + count2.ToString() +
                        //                    " (" + proPerc + "%)");

                        VSDropWeeksEntity weeks = _business.GetDropWeeks(this.SiteId, WeekId.Calendar_Week_ID);
                        if (ap.ExecutorService.WaitForShutdown(1)) break;

                        foreach (VSDropWeekEntity week in weeks)
                        {
                            if (week.Week_ID != null)
                            {
                                result.Add(week);
                                if (ap.ExecutorService.WaitForShutdown(1)) break;
                            }

                            Thread.Sleep(1);
                        }

                        if (ap.ExecutorService.IsShutdown) break;
                        Thread.Sleep(1);
                    }
                }
            }
            finally
            {
                count = result.Count;
            }
            return result;
        }

        protected override void FillReportInternal(ListViewItem item, object source, int rowIndex)
        {
            VSDropWeekEntity entity = ((VSDropWeeksEntity)source)[rowIndex];
            item.Tag = entity;
            int subIndex = 1;


            item.Text = entity.WeekNumber.SafeValue().ToString();

            item.SubItems[subIndex++].Text = _adminUtilities.GetRegionalDate(entity.StartDate) + " - " +
                                                _adminUtilities.GetRegionalDate(entity.EndDate);
            item.SubItems[subIndex++].Text = entity.WeekCount.ToString();

            //    string WeekNo = entity.WeekNumber.SafeValue().ToString();
            //   string Week = _adminUtilities.GetRegionalDate(entity.StartDate) + " - " +
            //                               _adminUtilities.GetRegionalDate(entity.EndDate);

            // DecWinOrLoss
            double DecWinOrLoss = entity.Cash_Take.SafeValue() +
                                    (_isAFTIncludedInCalculation ? (entity.DecEftIn.SafeValue() / 100.00) : 0) -
                                    (_isAFTIncludedInCalculation ? (entity.DecEftOut.SafeValue() / 100.00) : 0);
            _adminUtilities.AddNumericValue(DecWinOrLoss, item.SubItems[subIndex++], "#,##0.00");

            _adminUtilities.AddNumericValue(entity.Notes.SafeValue(), item.SubItems[subIndex++], "###,##0");
            _adminUtilities.AddNumericValue(entity.Coins.SafeValue(), item.SubItems[subIndex++]);
            _adminUtilities.AddNumericValue(entity.TicktesIn.SafeValue(), item.SubItems[subIndex++]);
            _adminUtilities.AddNumericValue(entity.TicktesOut.SafeValue(), item.SubItems[subIndex++]);

            _adminUtilities.AddNumericValue(entity.PromoCashableIn.SafeValue(), item.SubItems[subIndex++]);
            _adminUtilities.AddNumericValue(entity.PromoNonCashableIn.SafeValue(), item.SubItems[subIndex++]);

            _adminUtilities.AddNumericValue(entity.Refills.SafeValue(), item.SubItems[subIndex++]);
            _adminUtilities.AddNumericValue(entity.Refunds.SafeValue(), item.SubItems[subIndex++]);
            _adminUtilities.AddNumericValue(entity.AttendantPay.SafeValue(), item.SubItems[subIndex++]);
            _adminUtilities.AddNumericValue(entity.Shortpay.SafeValue(), item.SubItems[subIndex++]);

            _adminUtilities.AddNumericValue(entity.Void.SafeValue(), item.SubItems[subIndex++]);
            _adminUtilities.AddNumericValue(entity.NotesVar.SafeValue(), item.SubItems[subIndex++], "####0");
            _adminUtilities.AddNumericValue(entity.CoinVar.SafeValue(), item.SubItems[subIndex++]);
            _adminUtilities.AddNumericValue(entity.VoucherVar.SafeValue(), item.SubItems[subIndex++]);
            _adminUtilities.AddNumericValue(entity.HandpayVar.SafeValue(), item.SubItems[subIndex++]);
            _adminUtilities.AddNumericValue(entity.TakeVar.SafeValue(), item.SubItems[subIndex++]);

            _adminUtilities.AddNumericValue(entity.Progressive_Value_Declared.SafeValue(), item.SubItems[subIndex++]);
            _adminUtilities.AddNumericValue(entity.Progressive_Value_Variance.SafeValue(), item.SubItems[subIndex++]);
        }

        protected override Form GetDialogInternal(object tag)
        {
            VSDropWeekEntity entity = tag as VSDropWeekEntity;
            return new frmCollectionHistory(0, this.SiteId, entity.Week_ID.SafeValue(), FormCollectionHistory_Mode.Week, this.SiteName, entity.WeekNumber.SafeValue().ToString(),_adminUtilities.GetRegionalDate(entity.StartDate) + " - " + _adminUtilities.GetRegionalDate(entity.EndDate));
        }
    }

    public class ViewSiteAssetReport : ViewSiteAssetBase
    {
        protected override void InitReportInternal()
        {
            this.View.Columns.AddRange(
                new ColumnHeader[]
               {
                    new ColumnHeader() { Tag ="Key_Category", TextAlign=HorizontalAlignment.Left }, 
                    new ColumnHeader() { Tag ="Key_Type", TextAlign=HorizontalAlignment.Left }, 
                    new ColumnHeader() { Tag ="Key_Manufacturer", TextAlign=HorizontalAlignment.Left }, 
                    new ColumnHeader() { Tag ="Key_Asset", TextAlign=HorizontalAlignment.Left}, 
                    new ColumnHeader() { Tag ="Key_Model", TextAlign=HorizontalAlignment.Left }, 
                    new ColumnHeader() { Tag ="Key_Position", TextAlign=HorizontalAlignment.Left }, 
                    new ColumnHeader() { Tag ="Key_Handle", TextAlign=HorizontalAlignment.Right }, 
                    new ColumnHeader() { Tag ="Key_NetWinLoss", TextAlign=HorizontalAlignment.Right }, 
                    new ColumnHeader() { Tag ="Key_DailyWin", TextAlign=HorizontalAlignment.Right }, 
                    new ColumnHeader() { Tag ="Key_TheoPerc", TextAlign=HorizontalAlignment.Right}, 
                    new ColumnHeader() { Tag ="Key_ActPerc", TextAlign=HorizontalAlignment.Right}, 
                    new ColumnHeader() { Tag ="Key_VarPerc", TextAlign=HorizontalAlignment.Right }
               });

        }

        protected override object FetchDataInternal(IAsyncProgress ap, out int count)
        {
            //frame the parameters into an entity
            AssetParams _Assetparameters = new AssetParams
            {
                SiteID = this.SiteId,
                StartDate = this.FromDate,
                EndDate = this.ToDate,
                Category = (this.CategoryId == -1) ? 0 : this.CategoryId
            };
            List<AssetReportResult> entity = _Asset.LoadAssetReportDetails(_Assetparameters);
            if (entity == null)
            {
                entity = new List<AssetReportResult>();
            }

            if (entity.Count > 0)
            {
                // Add the total row
                AssetReportResult total = new AssetReportResult()
                {
                    Category = ResourceExtensions.GetResourceTextByKey(null,"Key_Total"),

                    TheoPerc = entity.Average(e => e.TheoPerc),
                    Handle = entity.Sum(e => e.Handle),
                    CasinoWin = entity.Sum(e => e.CasinoWin),
                    DailyWin = entity.Average(e => e.DailyWin),
                    ActPerc = entity.Average(e => e.ActPerc),
                    PercVar = entity.Average(e => e.PercVar),
                };
                entity.Insert(0, total);
            }

            //Get the count of records.
            count = entity.Count;
            return entity;
        }

        protected override void FillReportInternal(ListViewItem item, object source, int rowIndex)
        {
            try
            {
                //Bind the values in the listview.
                AssetReportResult entity = ((List<AssetReportResult>)source)[rowIndex];

                item.SubItems[0].Text = entity.Category;
                item.SubItems[1].Text = entity.Type;
                item.SubItems[2].Text = entity.Manu;
                item.SubItems[3].Text = entity.asset;
                item.SubItems[4].Text = entity.Model;
                item.SubItems[5].Text = entity.Position;

                _adminUtilities.AddNumericValue(entity.Handle.SafeValue(), item.SubItems[6], "#,##0.00", true);
                _adminUtilities.AddNumericValue(entity.CasinoWin.SafeValue(), item.SubItems[7], "#,##0.00", true);
                _adminUtilities.AddNumericValue(entity.DailyWin.SafeValue(), item.SubItems[8], "#,##0.00", true);
                _adminUtilities.AddNumericValue(entity.TheoPerc.SafeValue(), item.SubItems[9], "0.00", false);
                _adminUtilities.AddNumericValue(entity.ActPerc.SafeValue(), item.SubItems[10], "0.00", false);
                _adminUtilities.AddNumericValue(entity.PercVar.SafeValue(), item.SubItems[11], "0.00", false);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Could not load the Asset Report Details....", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
        }

        protected override void ExportReportInternal()
        {
            //Load the report.
            clsSPParams spParams = new clsSPParams();

            

            if (this.CategoryId == -1)
            {
                this.CategoryId = 0;
            }

            spParams.StartDate = Common.Utilities.Common.GetUniversalDate(this.FromDate);
            spParams.EndDate = Common.Utilities.Common.GetUniversalDate(this.ToDate);
            spParams.SiteID = this.SiteId;
            spParams.Category = this.CategoryId;
            spParams.CategoryName = this.Category;
            spParams.SiteName = this.SiteName;
            spParams.ReportFilterDateFormat = SettingsEntity.ReportDateTimeFormat;
            spParams.ReportDataDateAloneFormat = SettingsEntity.ReportDataDateAloneFormat;
            spParams.ReportDataDateNTimeFormat = SettingsEntity.ReportDataDateNTimeFormat;
            spParams.ReportPrintDateTimeFormat = SettingsEntity.ReportPrintDateTimeFormat;
            
            BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_AssetReport", "Asset Report", "ENT_AssetReport", spParams, false);
        }
    }

    public class ViewSiteAssetHistory : ViewSiteAssetBase
    {
        protected override void InitReportInternal()
        {
            //Set the columns
            this.View.Columns.AddRange(
               new ColumnHeader[]
               {
                    new ColumnHeader() {Tag ="Key_StartDate",TextAlign=HorizontalAlignment.Left },
                    new ColumnHeader() {Tag ="Key_EndDate",TextAlign=HorizontalAlignment.Left },
                    new ColumnHeader() {Tag ="Key_Game",TextAlign=HorizontalAlignment.Left },
                    new ColumnHeader() {Tag ="Key_Type",TextAlign=HorizontalAlignment.Left },
                    new ColumnHeader() {Tag ="Key_Operator",TextAlign=HorizontalAlignment.Left },
                    new ColumnHeader() {Tag ="Key_InstallDays",TextAlign=HorizontalAlignment.Right },
                    new ColumnHeader() {Tag ="Key_DropPeriod",TextAlign=HorizontalAlignment.Right },
                    new ColumnHeader() {Tag ="Key_QtyDrops",TextAlign=HorizontalAlignment.Right},
                    new ColumnHeader() {Tag ="Key_WinLoss",TextAlign=HorizontalAlignment.Right },
                    new ColumnHeader() {Tag ="Key_AvgDailyWin" ,TextAlign=HorizontalAlignment.Right },
                    new ColumnHeader() {Tag ="Key_TotOpShare",TextAlign=HorizontalAlignment.Right },
                    new ColumnHeader() {Tag ="Key_totCompanyShare",TextAlign=HorizontalAlignment.Right},
                    new ColumnHeader() {Tag ="Key_totSiteShare",TextAlign=HorizontalAlignment.Right },
                    new ColumnHeader() {Tag ="Key_AssetNo",TextAlign=HorizontalAlignment.Right},
                    new ColumnHeader() {Tag ="Key_SerialNoHeader",TextAlign=HorizontalAlignment.Right },
                    new ColumnHeader() {Tag ="Key_AltSerialNoSortForm",TextAlign=HorizontalAlignment.Right },
                    new ColumnHeader() {Tag ="Key_Depot",TextAlign=HorizontalAlignment.Left},
                    new ColumnHeader() {Tag ="Key_GameCode",TextAlign=HorizontalAlignment.Left },
                    new ColumnHeader() {Tag ="Key_ToOtherShare",TextAlign=HorizontalAlignment.Right }
               });
        }



        protected override object FetchDataInternal(IAsyncProgress ap, out int count)
        {
            //frame the parameters into an entity

            List<DetailedHistoryResult> entity = null;
            try
            {
                AssetHistoryParams _Assetparameters = new AssetHistoryParams
                {
                    BarPositionID = this.BarPositionId,
                    IsDetailed = true,
                    SiteID = this.SiteId,
                };
                entity = _Asset.LoadAssetHistoryDetails(_Assetparameters);
                count = entity.Count;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Timeout"))
                {
                    _hasFetchTimeoutError = true;
                }
                else
                {
                    BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(BMC.Common.ResourceExtensions.GetResourceTextByKey(null, "Key_TryAgain"));
                }

                ExceptionManager.Publish(ex);
            }

            count = entity.Count;
            return entity;
        }

        protected override void FillReportInternal(ListViewItem item, object source, int rowIndex)
        {
            try
            {
                int InstallDays = 0;
                //Bind the values in the listview.
                DetailedHistoryResult entity = ((List<DetailedHistoryResult>)source)[rowIndex];

                if (string.IsNullOrEmpty(entity.Installation_End_Date))
                    InstallDays = (DateTime.Now.Subtract(Convert.ToDateTime(entity.Installation_Start_Date))).Days + 1;
                else
                    InstallDays = (Convert.ToDateTime(entity.Installation_End_Date).Subtract(Convert.ToDateTime(entity.Installation_Start_Date))).Days + 1;

                item.SubItems[0].Text = _adminUtilities.GetRegionalDate(entity.Installation_Start_Date);
                item.SubItems[1].Text = string.IsNullOrEmpty(entity.Installation_End_Date) ? "Active" : _adminUtilities.GetRegionalDate(entity.Installation_End_Date);
                item.SubItems[2].Text = entity.GameName;
                item.SubItems[3].Text = entity.Machine_Type_Code;
                item.SubItems[4].Text = entity.Operator_Name;
                item.SubItems[5].Text = Math.Abs(InstallDays).ToString();
                item.SubItems[6].Text = entity.Collection_Days.SafeValue().ToStringSafe();
                item.SubItems[7].Text = entity.Collection_ID.SafeValue().ToStringSafe();

                _adminUtilities.AddNumericValue(entity.Collection_CashTake.SafeValue(), item.SubItems[8], "###,##0.00", true);
                _adminUtilities.AddNumericValue(entity.AvgDailyWin.SafeValue(), item.SubItems[9], "###,##0.00", true);
                _adminUtilities.AddNumericValue(entity.Collection_Supplier_Share.SafeValue(), item.SubItems[10], "###,##0.00", true);
                _adminUtilities.AddNumericValue(entity.Collection_Company_Share.SafeValue(), item.SubItems[11], "###,##0.00", true);
                _adminUtilities.AddNumericValue(entity.Collection_Location_Share.SafeValue(), item.SubItems[12], "###,##0.00", true);
                item.SubItems[13].Text = entity.Machine_Stock_No;
                item.SubItems[14].Text = entity.Machine_Manufacturers_Serial_No;
                item.SubItems[15].Text = entity.Machine_Alternative_Serial_Numbers;
                item.SubItems[16].Text = entity.Depot_Name;
                item.SubItems[17].Text = entity.Machine_Class_Model_Code;
                _adminUtilities.AddNumericValue(entity.Collection_Other_Share.SafeValue(), item.SubItems[18], "###,##0.00", true);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Could not load the Asset History Details....", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
        }

        protected override void ExportReportInternal()
        {
            //Load the report.            
            clsSPParams spParams = new clsSPParams();
            spParams.Bar_Position_ID = BarPosition;
            spParams.IsDetailed = true;
            spParams.SiteID = this.SiteId;
            spParams.SiteName = this.SiteName;
            spParams.ReportFilterDateFormat = SettingsEntity.ReportDateTimeFormat;
            spParams.ReportDataDateAloneFormat = SettingsEntity.ReportDataDateAloneFormat;
            spParams.ReportDataDateNTimeFormat = SettingsEntity.ReportDataDateNTimeFormat;
            spParams.ReportPrintDateTimeFormat = SettingsEntity.ReportPrintDateTimeFormat;
            BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_GetDetailedHistory", "Asset Detailed Report", "ENT_AssetDetailedReport", spParams, false);
        }
    }

    public class ViewSiteAssetGameReports : ViewSiteAssetBase
    {
        /// <summary>
        /// Inits the report internal.
        /// Adds the columns to list view with corresponding format
        /// </summary>
        protected override void InitReportInternal()
        {
            this.View.Columns.AddRange(
                   new ColumnHeader[] 
                { 
                    new ColumnHeader() { Tag= "Key_Asset", TextAlign = HorizontalAlignment.Left },
                    new ColumnHeader() { Tag= "Key_Manufacturer", TextAlign = HorizontalAlignment.Left },
                    new ColumnHeader() { Tag= "Key_GameTitle", TextAlign = HorizontalAlignment.Left},

                    new ColumnHeader() { Tag= "Key_Handle", TextAlign = HorizontalAlignment.Right},
                    new ColumnHeader() { Tag= "Key_NetWin", TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Tag= "Key_DailyWin", TextAlign = HorizontalAlignment.Right },                    
                    new ColumnHeader() { Tag= "Key_AvgBet", TextAlign = HorizontalAlignment.Right},
                    new ColumnHeader() { Tag= "Key_GamesPlayed", TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Tag= "Key_TotalBet", TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Tag= "Key_TotalWon", TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Tag= "Key_PayoutPercTheo", TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Tag= "Key_PayoutPercAct", TextAlign = HorizontalAlignment.Right },
                    new ColumnHeader() { Tag= "Key_PayoutPercVar", TextAlign = HorizontalAlignment.Right },                    
                });
        }

        /// <summary>
        /// Fetches the data internal.
        /// Retrives all game detail records for selected bar position
        /// returns the record count
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        protected override object FetchDataInternal(IAsyncProgress ap, out int count)
        {
            VSAssetGameReportsEntity entity = _business.GetAssetGameReport(this.BarPositionId, this.SiteId, this.FromDate, this.ToDate);
            count = entity.Count;
            return entity;
        }

        /// <summary>
        /// Fills the report internal.
        /// Binds the records in list view
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="source">The source.</param>
        /// <param name="rowIndex">Index of the row.</param>
        protected override void FillReportInternal(ListViewItem item, object source, int rowIndex)
        {
            VSAssetGameReportEntity entity = ((VSAssetGameReportsEntity)source)[rowIndex];
            item.Text = entity.Machine_Stock_No;
            item.SubItems[1].Text = entity.MG_Game_Manufacturer_Name;
            item.SubItems[2].Text = string.IsNullOrEmpty(entity.MG_Alias_Game_Name.Trim()) ? "[TBD]" : entity.MG_Alias_Game_Name;
            _adminUtilities.AddNumericValue(entity.Handle.SafeValue(), item.SubItems[3], "###,##0.00");
            _adminUtilities.AddNumericValue(entity.NetWin.SafeValue(), item.SubItems[4], "###,##0.00");
            _adminUtilities.AddNumericValue(entity.DailyWin.SafeValue(), item.SubItems[5], "###,##0.00");
            _adminUtilities.AddNumericValue(entity.AvgBet.SafeValue(), item.SubItems[6], "###,##0.00");
            _adminUtilities.AddNumericValue(entity.Played.SafeValue(), item.SubItems[7], "###,##0.00");
            _adminUtilities.AddNumericValue(entity.TotalBet.SafeValue(), item.SubItems[8], "###,##0.00");
            _adminUtilities.AddNumericValue(entity.TotalWon.SafeValue(), item.SubItems[9], "###,##0.00");
            _adminUtilities.AddNumericValue(entity.Theo.SafeValue(), item.SubItems[10], "###,##0.00");
            _adminUtilities.AddNumericValue(entity.Actual.SafeValue(), item.SubItems[11], "###,##0.00");
            _adminUtilities.AddNumericValue(entity.Variance.SafeValue(), item.SubItems[12], "###,##0.00");

        }


        /// <summary>
        /// Exports the report internal.
        /// Report viewr is showed for the datas populated in listview
        /// </summary>
        /// <param name="viewer">The viewer.</param>
        protected override void ExportReportInternal()
        {
            clsSPParams spParams = new clsSPParams();
            spParams.Bar_Position_Id = this.BarPositionId;
            spParams.StartDate = this.FromDate.ToString();
            spParams.EndDate = this.ToDate.ToString();
            spParams.BarPositionName = this.BarPosition;
            spParams.Site_ID = this.SiteId;
            spParams.CompanyName = this.Company;
            spParams.SubCompanyName = this.SubCompany;
            spParams.SiteName = this.SiteName;
            spParams.BarPositionName = this.BarPosition;
            spParams.ReportFilterDateFormat = SettingsEntity.ReportDateTimeFormat;
            spParams.ReportDataDateAloneFormat = SettingsEntity.ReportDataDateAloneFormat;
            spParams.ReportDataDateNTimeFormat = SettingsEntity.ReportDataDateNTimeFormat;
            spParams.ReportPrintDateTimeFormat = SettingsEntity.ReportPrintDateTimeFormat;

            BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_AssetGameReport", "Asset Game Report", "ENT_VSAssetGameReport", spParams, false);
        }
    }

    public static class ViewSiteHelper
    {
        private const string SITESEARCHHISTORY = "SiteSearchHistory";
        private const string FORMCONFIG = "FormConfig";

        public const int TAB_DROP = 0;
        public const int SUB_TAB_DROP_COLL = 0;
        public const int SUB_TAB_DROP_BATCH = 1;
        public const int SUB_TAB_DROP_WEEK = 2;

        public const int TAB_HOURLY = 1;
        public const int SUB_TAB_HOURLY_DETAILS = 0;

        public const int TAB_ASSET = 2;
        public const int SUB_TAB_ASSET_ASSET = 0;
        public const int SUB_TAB_ASSET_HISTORY = 1;
        public const int SUB_TAB_ASSET_CONTROL = 2;
        public const int SUB_TAB_ASSET_GAME = 3;
        public static int iSecurityUserID = 0;


        static ViewSiteHelper()
        {
            RdcHQ.CreateSubKeyIfNotExists(SITESEARCHHISTORY);
            RdcHQ.CreateSubKeyIfNotExists(FORMCONFIG);
            iSecurityUserID = AppGlobals.Current.UserId;
        }

        private static ViewSitesBusiness _business = new ViewSitesBusiness();

        public static void FillSearchCombo(ComboBox searchCombo)
        {
            FillSearchCombo(searchCombo, -1, string.Empty);
        }

        public static void FillSearchCombo(ComboBox searchCombo, int selectedIndex, string value)
        {
            ModuleProc PROC = new ModuleProc("ViewSiteHelper", "FillSearchCombo");
            int index = -1;

            try
            {
                searchCombo.Tag = "1";
                var items = RdcHQ.GetAllSettings(SITESEARCHHISTORY);
                var regText = string.Empty;
                searchCombo.Items.Clear();
                if (selectedIndex == -1 && !value.IsEmpty())
                {
                    regText = GetSearchComboText(value);
                }

                foreach (var item in items)
                {
                    searchCombo.Items.Add(item);
                    index++;
                    if (selectedIndex == -1 &&
                        !regText.IsEmpty() &&
                        item.IgnoreCaseCompare(regText))
                    {
                        selectedIndex = index;
                    }
                }
                if (!(selectedIndex >= 0 && selectedIndex < searchCombo.Items.Count))
                {
                    selectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                searchCombo.Tag = null;
                searchCombo.SelectedIndex = selectedIndex;
            }
        }

        public static bool SaveSearchCombo(ComboBox searchCombo, string regKey)
        {
            ModuleProc PROC = new ModuleProc("ViewSiteHelper", "FillSearchCombo");
            int index = -1;
            bool result = false;

            try
            {
                searchCombo.Tag = "1";
                string text = searchCombo.Text.Trim();
                RdcHQ.SaveSetting(FORMCONFIG, regKey, text);

                if (!text.IsEmpty())
                {
                    var items = RdcHQ.GetAllSettings(SITESEARCHHISTORY).ToList<string>();
                    var found = (from i in items
                                 let k = ++index
                                 where i.IgnoreCaseCompare(text)
                                 select i).FirstOrDefault();
                    if (found.IsEmpty())
                    {
                        index = items.Count;
                        string key = index.ToString();
                        RdcHQ.SaveSetting(SITESEARCHHISTORY, key, text);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                searchCombo.Tag = null;
                if (index != -1)
                {
                    FillSearchCombo(searchCombo, index, string.Empty);
                    result = true;
                }
            }

            return result;
        }

        public static string GetSearchComboText(string key)
        {
            ModuleProc PROC = new ModuleProc("ViewSiteHelper", "GetSearchComboText");

            try
            {
                return RdcHQ.GetSetting(FORMCONFIG, key, string.Empty);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
                return string.Empty;
            }
        }

        public static void SaveSearchComboText(ComboBox searchCombo, string key)
        {
            ModuleProc PROC = new ModuleProc("ViewSiteHelper", "SaveSearchComboText");

            try
            {
                RdcHQ.SaveSetting(FORMCONFIG, key, searchCombo.Text);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        //Commentd the code to fetch search filter in registry and set the selected index by default
        //public static void SelectComboByDisplayMember(this ComboBox cbo)
        //{
        //    ModuleProc PROC = new ModuleProc("", "SelectComboByDisplayMember");

        //    try
        //    {
        //        string text = RdcHQ.GetSetting("FormConfig", "ViewSiteSearchForm_" + cbo.Name, string.Empty);
        //        if (!text.IsEmpty())
        //        {
        //            IListFinder finder = cbo.DataSource as IListFinder;
        //            if (finder != null)
        //            {
        //                int selectedIndex = finder.GetListIndex(text);
        //                if (selectedIndex != -1)
        //                {
        //                    cbo.SelectedIndex = selectedIndex;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Exception(PROC, ex);
        //    }
        //}

        public static void FillCompanies(this ComboBox combo)
        {

            VSCompaniesEntity entity = _business.GetCompanies(iSecurityUserID);
            GlobalHelper.FillCombo(combo, entity, "Company_ID", "Company_Name",
               ComboAdditionalItemType.Any,
                (o, i, s) =>
                {
                    entity.Insert(0, new VSCompanyEntity()
                    {
                        Company_ID = -1,
                        Company_Name = s
                    });
                });
            //SelectComboByDisplayMember(combo);
        }

        public static void FillSubCompanies(this ComboBox combo, int companyId)
        {
            VSSubCompaniesEntity entity = _business.GetSubCompanies(companyId, iSecurityUserID);
            GlobalHelper.FillCombo(combo, entity, "Sub_Company_ID", "Sub_Company_Name",
                ComboAdditionalItemType.Any,
                (o, i, s) =>
                {
                    entity.Insert(0, new VSSubCompanyEntity()
                    {
                        Sub_Company_ID = -1,
                        Sub_Company_Name = s
                    });
                });
            //SelectComboByDisplayMember(combo);
        }

        public static void FillSubCompanyRegions(this ComboBox combo, int? companyId, int? subCompanyId)
        {
            companyId = companyId.GetValueOrDefault() <= 0 ? null : companyId;
            subCompanyId = subCompanyId.GetValueOrDefault() <= 0 ? null : subCompanyId;

            VSSubCompanyRegionsEntity entity = _business.GetSubCompanyRegions(subCompanyId, companyId);
            GlobalHelper.FillCombo(combo, entity, "Sub_Company_Region_ID", "Sub_Company_Region_Name",
                ComboAdditionalItemType.Any,
                (o, i, s) =>
                {
                    entity.Insert(0, new VSSubCompanyRegionEntity()
                    {
                        Sub_Company_Region_ID = -1,
                        Sub_Company_Region_Name = s
                    });
                });
            //SelectComboByDisplayMember(combo);
        }

        public static void FillSubCompanyAreas(this ComboBox combo, int? regionId, int companyId, int subCompanyId)
        {
            regionId = regionId.GetValueOrDefault() < 0 ? 0 : regionId;
            companyId = companyId < 0 ? 0 : companyId;

            VSSubCompanyAreasEntity entity = _business.GetSubCompanyAreas(regionId.GetValueOrDefault(), companyId, subCompanyId);
            GlobalHelper.FillCombo(combo, entity, "Sub_Company_Area_ID", "Sub_Company_Area_Name",
                ComboAdditionalItemType.Any,
                (o, i, s) =>
                {
                    entity.Insert(0, new VSSubCompanyAreaEntity()
                    {
                        Sub_Company_Area_ID = -1,
                        Sub_Company_Area_Name = s
                    });
                });
            //SelectComboByDisplayMember(combo);
        }

        public static void FillSubCompanyDistricts(this ComboBox combo, int? areaId, int regionId, int subCompanyId, int companyId)
        {
            areaId = areaId.GetValueOrDefault() < 0 ? 0 : areaId;
            regionId = regionId < 0 ? 0 : regionId;

            VSSubCompanyDistrictsEntity entity = _business.GetSubCompanyDistricts(regionId, areaId, subCompanyId, companyId);
            GlobalHelper.FillCombo(combo, entity, "Sub_Company_District_ID", "Sub_Company_District_Name",
                ComboAdditionalItemType.Any,
                (o, i, s) =>
                {
                    entity.Insert(0, new VSSubCompanyDistrictEntity()
                    {
                        Sub_Company_District_ID = -1,
                        Sub_Company_District_Name = s
                    });
                });
            //SelectComboByDisplayMember(combo);
        }

        public static void FillOperators(this ComboBox combo)
        {
            VSOperatorsEntity entity = _business.GetOperators();
            GlobalHelper.FillCombo(combo, entity, "Operator_ID", "Operator_Name",
                ComboAdditionalItemType.Any,
                (o, i, s) =>
                {
                    entity.Insert(0, new VSOperatorEntity()
                    {
                        Operator_ID = -1,
                        Operator_Name = s
                    });
                });
            //SelectComboByDisplayMember(combo);
        }

        public static void FillStaffs(this ComboBox combo) // grammer mistake
        {
            VSStaffsEntity entity = _business.GetStaffs();
            GlobalHelper.FillCombo(combo, entity, "Staff_ID", "Staff_Name",
                ComboAdditionalItemType.Any,
                (o, i, s) =>
                {
                    entity.Insert(0, new VSStaffEntity()
                    {
                        Staff_ID = -1,
                        Staff_Name = s
                    });
                });
            //SelectComboByDisplayMember(combo);
        }

        public static void FillMachineTypes(this ComboBox combo)
        {
            VSMachineTypesEntity entity = _business.GetMachineTypes();
            GlobalHelper.FillCombo(combo, entity, "Machine_Type_ID", "Machine_Type_Code",
                ComboAdditionalItemType.Any,
                (o, i, s) =>
                {
                    entity.Insert(0, new VSMachineTypeEntity()
                    {
                        Machine_Type_ID = -1,
                        Machine_Type_Code = s
                    });
                });
            //SelectComboByDisplayMember(combo);
        }

        public static void FillDepots(this ComboBox combo, int? operatorId)
        {
            VSDepotsEntity entity = _business.GetDepots(operatorId);
            GlobalHelper.FillCombo(combo, entity, "Depot_ID", "Depot_Name",
                ComboAdditionalItemType.Any,
                (o, i, s) =>
                {
                    entity.Insert(0, new VSDepotEntity()
                    {
                        Depot_ID = -1,
                        Depot_Name = s
                    });
                });
            //SelectComboByDisplayMember(combo);
        }

        public static void FillManufacturers(this ComboBox combo)
        {
            VSManufacturersEntity entity = _business.GetManufacturers();
            GlobalHelper.FillCombo(combo, entity, "Manufacturer_ID", "Manufacturer_Name",
                ComboAdditionalItemType.Any,
                (o, i, s) =>
                {
                    entity.Insert(0, new VSManufacturerEntity()
                    {
                        Manufacturer_ID = -1,
                        Manufacturer_Name = s
                    });
                });
            //SelectComboByDisplayMember(combo);
        }
    }

    public class NumericWrapperComparer : IComparer<string>
    {
        private IComparer _comparer = null;

        public NumericWrapperComparer()
        {
            _comparer = new CaseInsensitiveComparer();
        }

        public int Compare(string x, string y)
        {
            if (Microsoft.VisualBasic.Information.IsNumeric(x) &&
                Microsoft.VisualBasic.Information.IsNumeric(y))
            {
                return (Convert.ToDouble(x)).CompareTo(Convert.ToDouble(y));
            }
            else
            {
                if (y == "-" && Microsoft.VisualBasic.Information.IsNumeric(x)) return 1;
                if (x == "-" && Microsoft.VisualBasic.Information.IsNumeric(y)) return -1;
                if (x == "-" && y == "-") return 0;

                if (x.StartsWith("(")) x = x.Replace("(", "").Replace(")", "");
                if (y.StartsWith("(")) y = y.Replace("(", "").Replace(")", "");

                if (x.Contains(",")) x = x.Replace(",", "");
                if (y.Contains(",")) y = y.Replace(",", "");

                if (y == "-" && Microsoft.VisualBasic.Information.IsNumeric(x)) return 1;
                if (x == "-" && Microsoft.VisualBasic.Information.IsNumeric(y)) return -1;
                if (x == "-" && y == "-") return 0;

                if (Microsoft.VisualBasic.Information.IsNumeric(x) ||
                    Microsoft.VisualBasic.Information.IsNumeric(y))
                {
                    return (Convert.ToDouble(x)).CompareTo(Convert.ToDouble(y));
                }
            }

            return _comparer.Compare(x, y);
        }
    }

    public class NumericWithTotalComparer : IComparer<string>
    {
        private IComparer _comparer = null;

        public NumericWithTotalComparer()
        {
            _comparer = new CaseInsensitiveComparer();
        }

        public int Compare(string x, string y)
        {
            if (x.IgnoreCaseCompare("Total") &&
                Microsoft.VisualBasic.Information.IsNumeric(y)) return -1;

            if (Microsoft.VisualBasic.Information.IsNumeric(x) &&
                Microsoft.VisualBasic.Information.IsNumeric(y))
            {
                return (Convert.ToDouble(x)).CompareTo(Convert.ToDouble(y));
            }
            else
            {
                if (y == "-" && Microsoft.VisualBasic.Information.IsNumeric(x)) return 1;
                if (x == "-" && Microsoft.VisualBasic.Information.IsNumeric(y)) return -1;
                if (x == "-" && y == "-") return 0;

                if (x.StartsWith("(")) x = x.Replace("(", "").Replace(")", "");
                if (y.StartsWith("(")) y = y.Replace("(", "").Replace(")", "");

                if (x.Contains(",")) x = x.Replace(",", "");
                if (y.Contains(",")) y = y.Replace(",", "");

                if (y == "-" && Microsoft.VisualBasic.Information.IsNumeric(x)) return 1;
                if (x == "-" && Microsoft.VisualBasic.Information.IsNumeric(y)) return -1;
                if (x == "-" && y == "-") return 0;

                if (Microsoft.VisualBasic.Information.IsNumeric(x) ||
                    Microsoft.VisualBasic.Information.IsNumeric(y))
                {
                    return (Convert.ToDouble(x)).CompareTo(Convert.ToDouble(y));
                }
            }

            return _comparer.Compare(x, y);
        }
    }
}
