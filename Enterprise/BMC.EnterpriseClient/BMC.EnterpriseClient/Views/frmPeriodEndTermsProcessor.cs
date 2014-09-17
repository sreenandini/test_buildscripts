namespace BMC.EnterpriseClient.Views
{
    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using BMC.Common.ExceptionManagement;
    using BMC.EnterpriseBusiness.Business;
    using BMC.EnterpriseClient.Helpers;
    using BMC.EnterpriseDataAccess;
    using BMC.Common.LogManagement;
    using BMC.Reports;
    using BMC.CoreLib.Concurrent;
    using System.Threading;
    using BMC.Common;
    using BMC.CoreLib.Win32;
    #endregion Namespaces

    #region Class
    public partial class frmPeriodEndTermsProcessor : Form
    {
        #region Private Members
        private PeriodEndTermsProcessorBusiness periodEndTermsProcessor = null;
        private const string sharePercentFormat = "#0.00";
        private readonly IExecutorService _exec = ExecutorServiceFactory.CreateExecutorService();
        private ListViewCustomSorter _lvCustomSorter = null;
        private string _currencySymbol = string.Empty;
        #endregion Private Members

        #region Constructor
        public frmPeriodEndTermsProcessor()
        {
            InitializeComponent();
            SetTagProperty();
            periodEndTermsProcessor = PeriodEndTermsProcessorBusiness.CreateInstance();

            this.lvSubCompanyCollectionSummary.ClipboardCopyMode = ListViewClipboardCopyMode.EnableWithHeaderText;
            this.lvSubCompanyCollectionSummary.ClipboardCopyFormat = ListViewClipboardCopyFormat.Semicolon;
            _lvCustomSorter = new ListViewCustomSorter(this.lvSubCompanyCollectionSummary, this);

            this.lvAvailableSchedules.ClipboardCopyMode = ListViewClipboardCopyMode.EnableWithHeaderText;
            this.lvAvailableSchedules.ClipboardCopyFormat = ListViewClipboardCopyFormat.Semicolon;
            _lvCustomSorter = new ListViewCustomSorter(this.lvAvailableSchedules, this);

            this.lvCollectionExceptions.ClipboardCopyMode = ListViewClipboardCopyMode.EnableWithHeaderText;
            this.lvCollectionExceptions.ClipboardCopyFormat = ListViewClipboardCopyFormat.Semicolon;
            _lvCustomSorter = new ListViewCustomSorter(this.lvCollectionExceptions, this);
        }
        #endregion Constructor

        #region Events
        private void frmPeriodEndTermsProcessor_Load(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside frmPeriodEndTermsProcessor_Load...", LogManager.enumLogLevel.Info);

                _currencySymbol = new System.Globalization.RegionInfo(BMC.Common.ConfigurationManagement.ConfigManager.Read("GetDefaultCultureForCurrency")).CurrencySymbol;

                this.ResolveResources();

                this.lvSubCompanyCollectionSummary.Columns[4].Text = string.Format("{0} {1}", _currencySymbol, this.lvSubCompanyCollectionSummary.Columns[4].Text);

                this.lvCollectionExceptions.Columns[3].Text = string.Format("{0} {1}", _currencySymbol, this.lvCollectionExceptions.Columns[3].Text);
                this.lvCollectionExceptions.Columns[4].Text = string.Format("{0} {1}", _currencySymbol, this.lvCollectionExceptions.Columns[4].Text);
                this.lvCollectionExceptions.Columns[5].Text = string.Format("{0} {1}", _currencySymbol, this.lvCollectionExceptions.Columns[5].Text);
                this.lvCollectionExceptions.Columns[8].Text = string.Format("{0} {1}", _currencySymbol, this.lvCollectionExceptions.Columns[8].Text);
                this.lvCollectionExceptions.Columns[9].Text = string.Format("{0} {1}", _currencySymbol, this.lvCollectionExceptions.Columns[9].Text);
                this.lvCollectionExceptions.Columns[10].Text = string.Format("{0} {1}", _currencySymbol, this.lvCollectionExceptions.Columns[10].Text);
                this.lvCollectionExceptions.Columns[11].Text = string.Format("{0} {1}", _currencySymbol, this.lvCollectionExceptions.Columns[11].Text);

                this.FillAvailableSchedulesList();

                this.FillSubCompanyDetails();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnApply_Click...", LogManager.enumLogLevel.Info);

                if (this.lvSubCompanyCollectionSummary.SelectedItems.Count > 0 && this.lvAvailableSchedules.SelectedItems.Count > 0)
                {
                    ListViewItem subCompanySelectedItem = this.lvSubCompanyCollectionSummary.SelectedItems[0];
                    ListViewItem availableScheduleSelectedItem = this.lvAvailableSchedules.SelectedItems[0];

                    subCompanySelectedItem.SubItems[5].Text = availableScheduleSelectedItem.SubItems[0].Text;
                    subCompanySelectedItem.SubItems[6].Text = availableScheduleSelectedItem.SubItems[1].Text;
                    subCompanySelectedItem.SubItems[7].Text = availableScheduleSelectedItem.SubItems[2].Text;
                    subCompanySelectedItem.SubItems[8].Text = availableScheduleSelectedItem.SubItems[3].Text;
                    subCompanySelectedItem.SubItems[9].Text = availableScheduleSelectedItem.SubItems[4].Text;

                    this.lvSubCompanyCollectionSummary.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                    this.lvSubCompanyCollectionSummary.Columns[0].Width = 0;
                    this.lvSubCompanyCollectionSummary.Columns[5].Width = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnClear_Click...", LogManager.enumLogLevel.Info);

                if (this.lvSubCompanyCollectionSummary.SelectedItems.Count > 0)
                {
                    ListViewItem subCompanySelectedItem = this.lvSubCompanyCollectionSummary.SelectedItems[0];

                    subCompanySelectedItem.SubItems[5].Text = string.Empty;
                    subCompanySelectedItem.SubItems[6].Text = string.Empty;
                    subCompanySelectedItem.SubItems[7].Text = string.Empty;
                    subCompanySelectedItem.SubItems[8].Text = string.Empty;
                    subCompanySelectedItem.SubItems[9].Text = string.Empty;

                    this.lvSubCompanyCollectionSummary.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                    this.lvSubCompanyCollectionSummary.Columns[0].Width = 0;
                    this.lvSubCompanyCollectionSummary.Columns[5].Width = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnValidateCollections_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnValidateCollections_Click...", LogManager.enumLogLevel.Info);

                this.ShowCompanyExceptionCollections();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnProcess_Click...", LogManager.enumLogLevel.Info);

                BMC.CoreLib.Win32.Win32Extensions.ShowAsyncDialog(this, "Processing...", _exec,
                     (o) =>
                     {
                         BMC.CoreLib.Win32.IAsyncProgress2 o2 = (BMC.CoreLib.Win32.IAsyncProgress2)o;

                         int subCompanyCounter = 0;
                         int subCompanyId = 0, periodEndId = 0, companyId = 0;
                         int count = 0;

                         o2.CrossThreadInvoke(() =>
                         {
                             count = this.lvSubCompanyCollectionSummary.Items.Cast<ListViewItem>().Where(x => string.IsNullOrEmpty(x.SubItems[5].Text)).Count();
                         });

                         //check if all sub-companies had terms scheduled
                         if (count <= 0)
                         {
                             count = lvSubCompanyCollectionSummary.Items.Count;
                             o2.InitializeProgress(1, count);

                             for (int i = 0; i < count; i++)
                             {
                                 o2.InitializeProgress(1, count);
                                 o2.UpdateStatusProgress(i + 1, "Loading ...");
                                 ListViewItem lvItem = null;
                                 int counter = 0;

                                 o2.CrossThreadInvoke(() =>
                                 {
                                     lvItem = lvSubCompanyCollectionSummary.Items[i];

                                     subCompanyCounter++;
                                     subCompanyId = Convert.ToInt32(lvItem.SubItems[0].Text.Split(',')[0]);
                                     periodEndId = Convert.ToInt32(lvItem.SubItems[0].Text.Split(',')[1]);
                                     companyId = Convert.ToInt32(lvItem.SubItems[0].Text.Split(',')[2]);

                                 });
                                 periodEndTermsProcessor.ConfirmPeriodEnd(periodEndId, subCompanyId, -1); // remove dummy statement number

                                 new AdminSubCompany().UpdateSubCompanyAdminDefaults(
                                     companyId,
                                     false,
                                     false,
                                     false,
                                     false,
                                     false,
                                     true,
                                     Convert.ToInt64(lvItem.SubItems[5].Text),
                                     2, //cascadeType 0-none, 1-defaults, 2-all
                                     2, //level 1-company cascade, 2-subcompany, site, barposition cascade
                                     false,
                                     AppEntryPoint.Current.UserId,
                                     AppEntryPoint.Current.UserName,
                                     "MODIFY",
                                     "",
                                     subCompanyId);

                                 List<CollectionIds> periodEndCollections = periodEndTermsProcessor.GetPeriodEndCollections(periodEndId);
                                 if (periodEndCollections.Count > 0)
                                 {
                                     o2.InitializeProgress(1, periodEndCollections.Count);
                                     while (counter < periodEndCollections.Count)
                                     {
                                         if (!new TermsCalcBusiness(periodEndCollections[counter++].collection_id).CalculateTermsForCollectionID())
                                         {
                                             //break;
                                         }

                                         o2.CrossThreadInvoke(() =>
                                         {
                                             string text = string.Format("Processed Collection for {0}: {1}/{2}", lvItem.SubItems[1].Text, counter, periodEndCollections.Count);
                                             o2.UpdateStatusProgress(counter + 1, text);
                                         });
                                     }
                                 }
                             }
                             BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_FINISHED_APPLYINGTERMS"));
                         }
                         else
                         {
                             o2.CrossThreadInvoke(() =>
                             {
                                 BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ITEMS_REQUIRE_TERMS_TO_SELECTED"));
                             });
                         }

                     });
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnInterimWeeklyLiquidation_Click(object sender, EventArgs e)
        {
            int subCompanyId, periodEndId, periodEndDocNo;

            try
            {
                LogManager.WriteLog("Inside btnInterimWeeklyLiquidation_Click...", LogManager.enumLogLevel.Info);

                periodEndDocNo = -1;

                foreach (ListViewItem lvItem in this.lvSubCompanyCollectionSummary.Items)
                {
                    subCompanyId = Convert.ToInt32(lvItem.SubItems[0].Text.Split(',')[0]);
                    periodEndId = Convert.ToInt32(lvItem.SubItems[0].Text.Split(',')[1]);

                    periodEndTermsProcessor.ConfirmPeriodEnd(periodEndId, subCompanyId, periodEndDocNo);
                }

                clsSPParams spParams = new clsSPParams();
                spParams.SubCompany = 0;
                spParams.iStatement_No = periodEndDocNo;
                BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_SSRS_REPORT_SGVI_WRL", "Weekly Revenue Liquidation Report", "WRL", spParams, false);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnConfirmWeeklyLiquidation_Click(object sender, EventArgs e)
        {
            try
            {
                int subCompanyId, periodEndId, periodEndDocNo;
                string period_End_Doc_no = string.Empty;

                try
                {
                    LogManager.WriteLog("Inside btnConfirmWeeklyLiquidation_Click...", LogManager.enumLogLevel.Info);

                    periodEndTermsProcessor.CreatePeriodEndDocNo(ref  period_End_Doc_no);
                    periodEndDocNo = Convert.ToInt32(period_End_Doc_no);

                    foreach (ListViewItem lvItem in this.lvSubCompanyCollectionSummary.Items)
                    {
                        subCompanyId = Convert.ToInt32(lvItem.SubItems[0].Text.Split(',')[0]);
                        periodEndId = Convert.ToInt32(lvItem.SubItems[0].Text.Split(',')[1]);

                        periodEndTermsProcessor.ConfirmPeriodEnd(periodEndId, subCompanyId, periodEndDocNo);
                    }

                    clsSPParams spParams = new clsSPParams();
                    spParams.SubCompany = 0;
                    spParams.iStatement_No = periodEndDocNo;
                    BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_SSRS_REPORT_SGVI_WRL", "Weekly Revenue Liquidation Report", "WRL", spParams, false);
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
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

        private void lvCollectionExceptions_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside lvCollectionExceptions_DrawItem...", LogManager.enumLogLevel.Info);

                if ((Convert.ToInt32(e.Item.SubItems["colMachineNumbers"]) > Convert.ToInt32(e.Item.SubItems["colCollectionNumbers"])) ||
                    Convert.ToInt32(e.Item.SubItems["colCollectionNumbers"]) == 0)
                {
                    e.Item.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void lvSubCompanyCollectionSummary_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside lvSubCompanyCollectionSummary_SelectedIndexChanged...", LogManager.enumLogLevel.Info);

                this.ShowCompanyExceptionCollections();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void lvCollectionExceptions_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside lvCollectionExceptions_MouseClick...", LogManager.enumLogLevel.Info);

                if (e.Button == MouseButtons.Right)
                {
                    if (this.lvCollectionExceptions.FocusedItem.Bounds.Contains(e.Location) == true)
                    {
                        this.ctxMenuListViewCollections.Show(Cursor.Position);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void lvCollectionExceptions_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside lvCollectionExceptions_DoubleClick...", LogManager.enumLogLevel.Info);

                if (this.lvCollectionExceptions.SelectedItems.Count > 0)
                {
                    foreach (ListViewItem lvItem in this.lvCollectionExceptions.SelectedItems)
                    {
                        Form dialog=new frmCollectionHistory(
                            0,
                            Convert.ToInt32(lvItem.SubItems[0].Text), //SiteId
                            Convert.ToInt32(lvItem.SubItems[13].Text), //WeekId
                            FormCollectionHistory_Mode.Week, //Mode
                            lvItem.SubItems[1].Text, //SiteName
                            lvItem.SubItems[15].Text, //Week no
                            (lvItem.SubItems[16].Text + " - " + lvItem.SubItems[17].Text)); //Week Start Date and Week End Date
                        Helpers.Win32Extensions.ShowDialogExAndDestroy(dialog,MainForm.ActiveForm);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void requestBatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside requestBatchToolStripMenuItem_Click...", LogManager.enumLogLevel.Info);

                string reference = string.Format("{0} {1} [{2}]",
                    this.lvSubCompanyCollectionSummary.SelectedItems[0].SubItems[2].Text.ToFormattedDateTime(),
                    this.lvCollectionExceptions.SelectedItems[0].SubItems[1].Text,
                    this.lvCollectionExceptions.SelectedItems[0].SubItems[2].Text);

                if (periodEndTermsProcessor.BatchExportHistory(reference, "GETCOLLBYDATE", Convert.ToInt32(this.lvCollectionExceptions.SelectedItems[0].SubItems[0].Text)) != -1)
                {
                    BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_COLLECTION_BAT_INFO"), this.Text);
                }
                else
                {
                    BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_PROBLEM_REQUESTING_COLLECTIONINFO"), this.Text);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void frmPeriodEndTermsProcessor_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside frmPeriodEndTermsProcessor_FormClosing...", LogManager.enumLogLevel.Info);

                // remove dummy statement number
                foreach (ListViewItem lvItem in this.lvSubCompanyCollectionSummary.Items)
                {
                    string subCompanyId = lvItem.SubItems[5].Text.Split(',')[0],
                            periodEndId = lvItem.SubItems[5].Text.Split(',')[1];

                    periodEndTermsProcessor.ConfirmPeriodEnd(Convert.ToInt32(periodEndId), Convert.ToInt32(subCompanyId), 0);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion Events

        #region Private Methods
        private void SetTagProperty()
        {
            this.btnValidateCollections.Tag = "Key_ValidateCollections";
            this.btnProcess.Tag = "Key_ProcessCaption";
            this.btnInterimWeeklyLiquidation.Tag = "Key_InterimWeeklyLiquidation";
            this.btnConfirmWeeklyLiquidation.Tag = "Key_ConfirmWeeklyLiquidation";
            this.btnClose.Tag = "Key_Close";
            this.lblSubCompanyCollectionSummary.Tag = "Key_SubCompanyCollectionSummary";
            this.colSubCompany.Tag = "Key_SubComp";
            this.colPeriodStart.Tag = "Key_PerStart";
            this.colPeriodEnd.Tag = "Key_PerEnd";
            this.colTotalNet.Tag = "Key_TotalNet";
            this.colSubCompanyScheduleName.Tag = "Key_Schedule";
            this.colSubCompanySitePercent.Tag = "Key_SitePercent";
            this.colSubCompanyOperatorPercent.Tag = "Key_OperatorPercent";
            this.colSubCompanyPercent.Tag = "Key_CompanyPercent";
            this.lblAvailableSchedules.Tag = "Key_AvailableSchedules";
            this.colScheduleId.Tag = "Key_ScheduleId";
            this.colScheduleName.Tag = "Key_Schedule";
            this.colSitePercent.Tag = "Key_SitePercent";
            this.colOperatorPercent.Tag = "Key_OperatorPercent";
            this.colCompanyPercent.Tag = "Key_CompanyPercent";
            this.btnApply.Tag = "Key_ApplyWithGreater";
            this.btnClear.Tag = "Key_ClearWithGreater";
            this.lblCollectionExceptions.Tag = "Key_CollectionExceptions";
            this.colSiteName.Tag = "Key_Site";
            this.colCashIn.Tag = "Key_CashIn";
            this.colCashOut.Tag = "Key_CashOut";
            this.colNet.Tag = "Key_Net";
            this.colCollectionNumbers.Tag = "Key_Colls";
            this.colMachineNumbers.Tag = "Key_MCs";
            this.colBills.Tag = "Key_Bills";
            this.colVoucherIn.Tag = "Key_VoucherIn";
            this.colVoucherOut.Tag = "Key_VoucherOut";
            this.colAttendantPay.Tag = "Key_AttendantPay";
            this.colBatchesNumbers.Tag = "Key_Batches";
            this.requestBatchToolStripMenuItem.Tag = "Key_RequestBatchProcessforthisItem";
            this.Tag = "Key_PeriodEndTermsProcessor";
       
        }
        private void FillSubCompanyDetails()
        {
            try
            {
                LogManager.WriteLog("Inside FillSubCompanyDetails...", LogManager.enumLogLevel.Info);

                DateTime? periodEndDate = periodEndTermsProcessor.GetFirstOpenPeriodEnd(DateTime.Now.ToString().ToFormattedDateTime());

                if (periodEndDate.HasValue)
                {
                    var subCompanyDetails = (from subcompany in periodEndTermsProcessor.GetSubCompanyResult(periodEndDate.ToString().ToFormattedDateTime())
                                             select new ListViewItem(
                                                 new string[] {
                                                 subcompany.sub_company_id + "," + subcompany.Period_ID  + "," + subcompany.company_id,
                                                 subcompany.Sub_Company_Name,
                                                 subcompany.Period_Start.ToFormattedDateTime(),
                                                 subcompany.Period_End.ToFormattedDateTime(),
                                                 subcompany.Total_Net.FormatNumber<decimal>("#,##0.00"),
                                                 string.Empty,
                                                 string.Empty,
                                                 string.Empty,
                                                 string.Empty,
                                                 string.Empty
                                          })).ToArray();

                    this.lvSubCompanyCollectionSummary.Items.AddRange(subCompanyDetails);

                    if (this.lvSubCompanyCollectionSummary.Items.Count > 0)
                    {
                        this.lvSubCompanyCollectionSummary.Items[0].Selected = true;
                    }

                    this.lvSubCompanyCollectionSummary.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                    this.lvSubCompanyCollectionSummary.Columns[0].Width = 0;
                    this.lvSubCompanyCollectionSummary.Columns[5].Width = 0;
                }
                else
                {
                    BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_NO_OPENPERIODS"));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void FillAvailableSchedulesList()
        {
            try
            {
                LogManager.WriteLog("Inside FillAvailableSchedulesList...", LogManager.enumLogLevel.Info);

                var availableSchedules = (from availableSchedule in periodEndTermsProcessor.GetAvailableSchedules()
                                          select new ListViewItem(
                                              new string[] {
                                                 availableSchedule.Terms_Group_ID.ToString(),
                                                 availableSchedule.Terms_Group_Name,
                                                 availableSchedule.Site_Share.FormatNumber<float>(),
                                                 availableSchedule.Operator_share.FormatNumber<float>(),
                                                 availableSchedule.Company_Share.FormatNumber<float>() 
                                             })).ToArray();

                this.lvAvailableSchedules.Items.AddRange(availableSchedules);

                if (this.lvAvailableSchedules.Items.Count > 0)
                {
                    this.lvAvailableSchedules.Items[0].Selected = true;
                }

                this.lvAvailableSchedules.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                this.lvAvailableSchedules.Columns[0].Width = 0;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void ShowCompanyExceptionCollections()
        {
            try
            {
                LogManager.WriteLog("Inside ShowCompanyExceptionCollections...", LogManager.enumLogLevel.Info);

                if (this.lvSubCompanyCollectionSummary.SelectedItems.Count > 0)
                {
                    ListViewItem subCompanySelectedItem = this.lvSubCompanyCollectionSummary.SelectedItems[0];

                    List<CompanyExceptionCollection> companyExceptionCollections = periodEndTermsProcessor.GetCompanyExceptionCollection(Convert.ToInt32(subCompanySelectedItem.SubItems[0].Text.Split(',')[1]));
                    var companyExceptionList = (from item in companyExceptionCollections
                                                select new ListViewItem(
                                                new string[] 
                                       {
                                            item.Site_ID.ToString(),
                                            item.SiteName,
                                            item.Site_Code,
                                            item.CashIn.FormatNumber<double>("###,##0.00"),
                                            item.CashOut.FormatNumber<double>("###,##0.00"),
                                            item.Net.FormatNumber<double>("###,##0.00"),
                                            item.BatchCount == 0 ? "0" : item.CollCount.FormatNumber<int>("####0"),
                                            item.Machines.FormatNumber<int>("####0"),
                                            item.Bills.FormatNumber<double>("###,##0"),
                                            item.TicketIn.FormatNumber<double>("###,##0.00"),
                                            item.TicketOut.FormatNumber<double>("###,##0.00"),
                                            item.Handpays.FormatNumber<double>("###,##0.00"),
                                            item.BatchCount.FormatNumber<int>("####0"),
                                            item.Week_Id.FormatNumber<int>("####"),
                                            item.Batch_Id.FormatNumber<int>("####"),
                                            item.WeekNumber.FormatNumber<int>("####"),
                                            item.WeekStartDate,
                                            item.WeekEndDate
                                       })).ToArray();

                    this.lvCollectionExceptions.Items.Clear();

                    this.lvCollectionExceptions.Items.AddRange(companyExceptionList);

                    this.lvCollectionExceptions.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                    this.lvCollectionExceptions.Columns[0].Width = 0;
                    this.lvCollectionExceptions.Columns[2].Width = 0;
                    this.lvCollectionExceptions.Columns[13].Width = 0;
                    this.lvCollectionExceptions.Columns[14].Width = 0;
                    this.lvCollectionExceptions.Columns[15].Width = 0;
                    this.lvCollectionExceptions.Columns[16].Width = 0;
                    this.lvCollectionExceptions.Columns[17].Width = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.FillAvailableSchedulesList();
            this.FillSubCompanyDetails();
        }
    }
        #endregion Private Methods

    #endregion Class

    #region Static Class
    public static class ThisFormExtensionMethods
    {
        #region Static Methods
        public static string ToFormattedDateTime(this string requestedDate, string format = "dd MMM yy")
        {
            return Convert.ToDateTime(requestedDate).ToString(format);
        }

        public static string FormatNumber<T>(this T? number, string format = "#0.00", string defaultValue = "") where T : struct
        {
            return number.HasValue ? Convert.ToDouble(number.Value.ToString()).ToString(format) : defaultValue;
        }
        #endregion Static Methods
    }
    #endregion Static Class
}

