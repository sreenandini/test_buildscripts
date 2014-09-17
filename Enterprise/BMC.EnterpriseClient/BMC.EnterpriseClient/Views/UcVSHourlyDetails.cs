using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib;
using BMC.CoreLib.Win32;
using BMC.CoreLib.Concurrent;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseBusiness.Business;
using System.Threading;
using System.IO;
using System.Reflection;
using System.Windows.Documents;
using BMC.EnterpriseClient.Helpers;
using BMC.Common.Utilities;
using System.Globalization;
using BMC.Common;
using System.Linq;
namespace BMC.EnterpriseClient.Views
{
    public partial class UcVSHourlyDetails : UserControl, IUserControl2, IUserControl3
    {
        private List<int> _hourlyIndexes = new List<int>(24);
        private int _startHourGamingDay = 6;
        private int _startHour = 6;
        bool isAssetEnabled = false;
        private HourlyBusiness _hourlyBusiness = new HourlyBusiness();
        private VSSiteTreeEntity _oldSite = null;
        private int _barPositionID = 0;
        private frmAdminUtilities _adminUtilities = new frmAdminUtilities();
        private bool _isFormInitializing = true;
        private ListViewCustomSorter _lvCustomSorter = null;
        private ToolTip ttpFilterBy = null;
        private static IDictionary<string, bool> _quantityTypes = new SortedDictionary<string, bool>()
        { 
            {"GAMES_BET", true},{ "GAMES_LOST", true},
            {"GAMES_WON", true}, {"NON_CASHABLE_VOUCHERS_IN_QTY", true},
            {"NON_CASHABLE_VOUCHERS_OUT_QTY" ,true},
            {"TICKETS_INSERTED_QTY", true}, 
            {"TICKETS_PRINTED_QTY", true}
        };

        private void HourlyCurrencyPriceConverter(double value, ListViewItem.ListViewSubItem subItem, bool ShowCurrencySymbol, bool IsOccupancy)
        {
            if (value == 0 || (value.ToString() == string.Empty))
            {
                subItem.Text = "-";
            }

            else
            {
                subItem.Text = value.ToString();
                if (ShowCurrencySymbol)
                {
                    subItem.Text = Convert.ToDecimal(value).GetUniversalCurrencyFormatWithSymbol();
                }

                if (IsOccupancy)
                {
                    subItem.Text = (Math.Round(value, 2) == 0 ? value.ToString() : (value).ToString("#,##0.00", new CultureInfo(Common.Utilities.ExtensionMethods.CurrentCurrenyCulture)));
                }

            }
        }

        private class FilterByDisplay
        {
            public HourlyFilterByEntity Key { get; set; }
            public string Text { get; set; }
        }

        private IDictionary<HourlyFilterByEntity, FilterByDisplay> _fillFilterByValues = new SortedDictionary<HourlyFilterByEntity, FilterByDisplay>();
        private FilterByDisplay _selectedFilterBy = null;

        public UcVSHourlyDetails(IViewSiteInfo viewSite)
        {
            this.ViewSite = viewSite;
            InitializeComponent();
            this.Initialize();
            SetTagProperty();
           // this.ResolveResources();
        }
        public void SetTagProperty()
        {
            this.chkCalendarDay.Tag = "Key_CalendarDay";
            this.chdrDate.Tag = "Key_Date";
            this.chdrDay.Tag = "Key_Day";
            this.btnDetails.Tag = "Key_DetailsCaption";
            this.chdrTotal.Tag = "Key_Total";
            ChdrAsset.Tag = "Key_Asset";
        }
        protected override void CreateHandle()
        {
            base.CreateHandle();
        }

        private void Initialize()
        {
            if (BMC.CoreLib.Win32.Win32Extensions.IsInDesignMode()) return;
            ModuleProc PROC = new ModuleProc("UcHourlyDetails", "Initialize");

            try
            {
                _lvCustomSorter = new ListViewCustomSorter(lvwHourly, this.ViewSite as Form);
                _isFormInitializing = true;
                lvwHourly.ClipboardCopyMode = ListViewClipboardCopyMode.EnableWithHeaderText;
                lvwHourly.ClipboardCopyFormat = ListViewClipboardCopyFormat.Semicolon;

                // Gaming day start hour
                _startHourGamingDay = AdminBusiness.GetSetting("GAMING_DAY_START_HOUR", "6").ConvertToInt32();
                _startHour = _startHourGamingDay;

                // calendar day check box
                chkCalendarDay.Visible = AppGlobals.Current.ShowHourlyCalendarDay;

                btnDetails.Text = this.GetResourceTextByKey("Key_Summary");//TextResources.BTN_DETAILS;
                this.SelectedDate = null;
               chdrTotal.Text = this.GetResourceTextByKey("Key_Total");//TextResources.Hourly_Chdr_Total;
                cboFilterByValue.DisplayMember = "FilterByName";
                cboFilterByValue.ValueMember = "FilterById";
           
                int startIndex = 4;
                for (int i = 0; i < 24; i++)
                {
                    _hourlyIndexes.Add(startIndex + i);
                    int hour1 = (_startHour + i) % 24;
                    int hour2 = (_startHour + 1 + i) % 24;

                    ColumnHeader header = new ColumnHeader();
                    header.TextAlign = HorizontalAlignment.Right;
                    header.Text = string.Format("{0:D} -> {1:D}", hour1, hour2);
                    lvwHourly.Columns.Add(header);
                }

                this.FillHourlyTypes();
                cPeriodUnits1.Fill(CPeriodUnitsType.Records);
                cPeriodUnits1.SelectedUnit = CPeriodUnitsType.Records;
                this.InitializeFilterBy();
                this.Activated();
                //this.ResolveResources();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                lvwHourly.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                _isFormInitializing = false;
                ChdrAsset.Width = 0;
              
            }
        }

        private void FillHourlyHeaders()
        {
            for (int i = 0; i < _hourlyIndexes.Count; i++)
            {
                int index = _hourlyIndexes[i];
                int hour1 = (_startHour + i) % 24;
                int hour2 = (_startHour + 1 + i) % 24;

                ColumnHeader header = lvwHourly.Columns[index];
                header.Text = string.Format("{0:D} -> {1:D}", hour1, hour2);
            }
        }

        private void InitializeFilterBy()
        {
            ModuleProc PROC = new ModuleProc("UcHourlyDetails", "InitializeFilterBy");

            try
            {
                _fillFilterByValues.Add(HourlyFilterByEntity.Position,
                    new FilterByDisplay()
                    {
                        Text = this.GetResourceTextByKey("Key_Position"), // TextResources.Hourly_FilterBy_Position,
                        Key = HourlyFilterByEntity.Position
                    });
                _fillFilterByValues.Add(HourlyFilterByEntity.Site,
                    new FilterByDisplay()
                    {
                        Text = this.GetResourceTextByKey("Key_Site"), //TextResources.Hourly_FilterBy_Site,
                        Key = HourlyFilterByEntity.Site
                    });
                _fillFilterByValues.Add(HourlyFilterByEntity.Zone,
                    new FilterByDisplay()
                    {
                        Text = this.GetResourceTextByKey("Key_Zone"), //TextResources.Hourly_FilterBy_Zone,
                        Key = HourlyFilterByEntity.Zone
                    });
                _fillFilterByValues.Add(HourlyFilterByEntity.Category,
                    new FilterByDisplay()
                    {
                        Text = this.GetResourceTextByKey("Key_Category"), //TextResources.Hourly_FilterBy_Category,
                        Key = HourlyFilterByEntity.Category
                    });

                cboFilterBy.Items.Clear();
                cboFilterBy.DisplayMember = "Text";
                cboFilterBy.ValueMember = "Key";
                foreach (var filterByValue in _fillFilterByValues)
                {
                    cboFilterBy.Items.Add(filterByValue.Value);
                }
                cboFilterBy.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private DateTime? _selectedDate = null;

        public DateTime? SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                bool visible = (value != null);
                btnDetails.Visible = visible;
                cPeriodCount1.Visible = !visible;
                cPeriodUnits1.Visible = !visible;
                cboFilterBy.Visible = !visible;
                cboFilterByValue.Visible = !visible;

                tblHeader.ColumnStyles[2].Width = (visible ? 80 : 0);
                tblHeader.ColumnStyles[3].Width = (!visible ? 70 : 0);
                tblHeader.ColumnStyles[4].Width = (!visible ? 120 : 0);
                tblHeader.ColumnStyles[5].Width = (!visible ? 120 : 0);
                tblHeader.ColumnStyles[6].Width = (!visible ? 120 : 0);
            }
        }

        public IViewSiteInfo ViewSite { get; set; }

        private void FillHourlyTypes()
        {
            ModuleProc PROC = new ModuleProc("UcHourlyDetails", "FillHourlyTypes");

            try
            {
                BindingSource src = new BindingSource();
                src.BindingComplete += new BindingCompleteEventHandler(OnHourlyTypes_BindingComplete);
                src.DataSource = _hourlyBusiness.GetHourlyStatisticsTypes();

                cboHourlyTypes.DataSource = src;
                cboHourlyTypes.DisplayMember = "HST_DisplayName";
                cboHourlyTypes.ValueMember = "HST_Type";
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        void OnHourlyTypes_BindingComplete(object sender, BindingCompleteEventArgs e)
        {

        }

        #region IUserControl Members

        public bool IsControlInitialized { get; set; }

        public void LoadControl()
        {
            try
            {
                if (this.ViewSite.SelectedInstallation == null)
                    return;
                if (_oldSite == null || _oldSite.Site_ID != this.ViewSite.SelectedSite.Site_ID || _barPositionID != this.ViewSite.SelectedInstallation.Bar_Position_ID)
                {
                    this.FillFilterByValues();
                    this.SelectedDate = null;
                }
                this.FillHourlyItems();
               
            }
            catch (Exception ex)
            {
                BMC.Common.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            finally
            {
                if (_oldSite == null || _oldSite.Site_ID != this.ViewSite.SelectedSite.Site_ID)
                    _oldSite = this.ViewSite.SelectedSite;
                _barPositionID = this.ViewSite.SelectedInstallation.Bar_Position_ID;
            }
        }

        public void FillHourlyItems()
        {
            ModuleProc PROC = new ModuleProc("UcHourlyDetails", "FillHourlyItems");
            if (this.ViewSite.SelectedSite == null)
                return;
            if (!this.IsControlInitialized || BMC.CoreLib.Win32.Win32Extensions.IsInDesignMode()) return;

            try
            {
              
                lvwHourly.Items.Clear();
                Form parentForm = this.ParentForm;

                IExecutorService exec = ExecutorServiceFactory.CreateExecutorService();
                HourlyDetailsEntity entity = null;

                //_lvCustomSorter.SortColumn = 0;
                //_lvCustomSorter.SortOrder = SortOrder.Descending;

                if (this.SelectedDate == null)
                {
                   // chdrDate.Text = this.GetResourceTextByKey("Key_Date"); //TextResources.Hourly_Chdr_Date;
                    //chdrDay.Text = this.GetResourceTextByKey("Key_Day");// TextResources.Hourly_Chdr_Day;
                    chdrDate.Tag = "Key_Date";
                    chdrDay.Tag = "Key_Day";
                   // chdrDate.Tag = typeof(DateTime);
                    ChdrAsset.Width = 0;
                    isAssetEnabled = false;
                }
                else
                {
                    //_lvCustomSorter.SortOrder = SortOrder.Ascending;
                    chdrDate.Text = this.GetResourceTextByKey("Key_Position") + " (" + this.SelectedDate.SafeValue().ToStringDate() + ")";
                    //chdrDay.Text = this.GetResourceTextByKey("Key_Game");//TextResources.Hourly_Chdr_Game;
                    //chdrDate.Tag = "Key_Position";
                    chdrDay.Tag = "Key_Game";
                  //  chdrDate.Tag = typeof(NumericWithTotalComparer);
                    ChdrAsset.Width = 60;
                    isAssetEnabled = true;
                }

                bool isQuantity = false;
                bool isOccupancy = false;
                bool bShowCurrencySymbol = false;
                _startHour = (chkCalendarDay.Checked ? 0 : _startHourGamingDay);
                this.FillHourlyHeaders();

                // fetching details
                BMC.CoreLib.Win32.Win32Extensions.ShowAsyncDialog(parentForm, this.GetResourceTextByKey(1,"MSG_HOURLY_LOAD"), exec,
                    (o) =>
                    {
                        o.CloseOnComplete = true;
                        HourlyStatisticsTypeEntity type = null;
                        int rows = 0;
                        int? position = null;
                        int? site = null;
                        int? zone = null;
                        int? category = null;

                        o.CrossThreadInvoke(() =>
                        {
                            type = (HourlyStatisticsTypeEntity)cboHourlyTypes.SelectedItem;
                            rows = cPeriodCount1.SelectedCount;
                            HourlyFilterByValueEntity filterByValue = null;
                            isQuantity = _quantityTypes.ContainsKey(type.HST_Type);

                            if (isQuantity)
                            {
                                bShowCurrencySymbol = false;
                            }
                            else
                            {
                                if (type.HST_Type == "OCCUPANCY(%)")
                                {
                                    isOccupancy = true;
                                    bShowCurrencySymbol = false;
                                }
                                else
                                {
                                    bShowCurrencySymbol = true;
                                }
                            }
                            if (cboFilterByValue.SelectedItem != null)
                            {
                                filterByValue = cboFilterByValue.SelectedItem as HourlyFilterByValueEntity;
                                if (filterByValue != null)
                                {
                                    if (_selectedFilterBy != null)
                                    {
                                        switch (_selectedFilterBy.Key)
                                        {
                                            case HourlyFilterByEntity.Position:
                                                if (this.ViewSite.SelectedInstallation != null)
                                                {
                                                    position = filterByValue.FilterById;
                                                }
                                                else
                                                {
                                                    position = -1;
                                                }
                                                break;

                                            case HourlyFilterByEntity.Zone:
                                                zone = filterByValue.FilterById;
                                                break;

                                            case HourlyFilterByEntity.Category:
                                                category = filterByValue.FilterById;
                                                break;

                                            default:
                                                site = filterByValue.FilterById;
                                                break;
                                        }
                                    }
                                }
                            }
                        });
                        entity = _hourlyBusiness.GetDetails(o as IAsyncProgress2, _startHour, rows, type.HST_Type, category, zone, position, _selectedDate, this.ViewSite.SelectedSite.Site_ID, chkCalendarDay.Checked);
                    });

                // add the total row
                if (this.SelectedDate != null)
                {
                    int count = entity.Count;
                    if (_selectedDate != null)
                    {
                        entity[0].Bar_Position_Name = this.GetResourceTextByKey("Key_Total");//TextResources.Hourly_Row_Total;
                    }
                }

               
                // loading details
                if (entity != null)
                {
                    //Change Request #203622 fix.
                    string dateFormat = Common.Utilities.Common.GetDateFormatByUserSetting();
                    BMC.CoreLib.Win32.Win32Extensions.ShowAsyncDialog(parentForm, this.GetResourceTextByKey(1,"MSG_HOURLY_LOAD"), exec, 1, entity.Count,
                        //Win32Extensions.ShowAsyncDialogContinuous(parentForm, "Hourly - [Loading details...]", exec,
                        (o) =>
                        {
                            IAsyncProgress2 o2 = o as IAsyncProgress2;
                            o.CloseOnComplete = true;
                           
                            int count = entity.Count;
                            for (int i = 0; i < entity.Count; i++)
                            {
                                HourlyDetailEntity det = entity[i];
                                int proIdx = (i + 1);
                                int proPerc = (int)(((float)proIdx / (float)count) * 100.0);
                                o2.UpdateStatusProgress(i, string.Format(this.GetResourceTextByKey(1,"MSG_HOURLY_LOAD_DETAILS"), proIdx, count.ToString(), proPerc));
                                //o2.UpdateStatusProgress(i, "Loading Hourly Details . . . : " +
                                //    proIdx + " of " + count.ToString() +
                                //    " (" + proPerc + "%)");

                                ListViewItem item = new ListViewItem();
                            
                                if (this.SelectedDate == null)
                                {

                                    item.Text = det.Date.ToString(dateFormat, Thread.CurrentThread.CurrentCulture);
                                    item.SubItems.Add(det.Date.DayOfWeek.ToString());
                                    item.SubItems.Add(" ");
                                }
                                 
                                else
                                {
                                    item.Text = det.Bar_Position_Name;
                                    item.SubItems.Add(det.Machine_Name);
                                }
                          
                             if (isAssetEnabled)
                                {

                                    item.SubItems.Add(det.Stock);
                                }
                                
                                ListViewItem.ListViewSubItem subItem = item.SubItems.Add("");
                                HourlyCurrencyPriceConverter(det.Total, subItem, bShowCurrencySymbol, isOccupancy);

                                double[] hourValues = det.GetHourValues();
                                for (int j = 0; j < hourValues.Length; j++)
                                {
                                    double hourValue = hourValues[j];
                                    subItem = item.SubItems.Add("");
                                    HourlyCurrencyPriceConverter(hourValue, subItem, bShowCurrencySymbol, isOccupancy);
                                }
                                item.Tag = det;

                                o.CrossThreadInvoke(() =>
                                {
                                 
                                    ListViewItem itemAdded = lvwHourly.Items.Add(item);
                                    lvwHourly.EnsureVisible(itemAdded.Index);
                                });
                                Thread.Sleep(1);
                            }

                            o.CrossThreadInvoke(() =>
                            {
                                lvwHourly.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                                if (lvwHourly.Items.Count > 0) lvwHourly.EnsureVisible(0);
                                if (!isAssetEnabled)
                                {
                                    lvwHourly.Columns[2].Width = 0;
                                }
                            });
                        });
                }
                lvwHourly.ResolveResources();

                // Setting header dynamically based on date, so have to set after control tags are resolved
                if (this.SelectedDate != null)
                    chdrDate.Text = this.GetResourceTextByKey("Key_Position") + " (" + this.SelectedDate.SafeValue().ToStringDate() + ")";
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public bool SaveControl()
        {
            return false;
        }

        #endregion

        private void cboHourlyTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isFormInitializing)
            {
                this.OnDelailsClicked();
            }
        }

        private void cPeriodCount1_SelectedIndexChanged(object sender, EventArgs e) { }

        private void SelectItem()
        {
            ModuleProc PROC = new ModuleProc("", "lvwHourly_SelectedIndexChanged");

            try
            {
                if (lvwHourly.SelectedItems != null &&
                    lvwHourly.SelectedItems.Count > 0)
                {
                    HourlyDetailEntity entity = lvwHourly.SelectedItems[0].Tag as HourlyDetailEntity;
                    if (entity != null)
                    {
                        if (this.SelectedDate == null)
                            this.SelectedDate = entity.Date;
                        else
                            this.SelectedDate = null;

                        this.FillHourlyItems();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void lvwHourly_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lvwHourly_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.SelectItem();
        }

        private void lvwHourly_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            this.OnDelailsClicked();
        }

        private void OnDelailsClicked()
        {
            this.SelectedDate = null;
            //this.FillHourlyItems();
            lvwHourly.Items.Clear();
            ChdrAsset.Width=0;

        }

        private void cboFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.FillFilterByValues();
        }

        public void FillFilterByValues()
        {
            ModuleProc PROC = new ModuleProc("", "FillFilterByValues");

            try
            {
                if (cboFilterBy.SelectedItem != null)
                {
                    _selectedFilterBy = ((FilterByDisplay)(cboFilterBy.SelectedItem));
                    cboFilterByValue.Items.Clear();
                    int? filterById = null;

                    if (this.ViewSite != null)
                    {
                        switch (_selectedFilterBy.Key)
                        {
                            case HourlyFilterByEntity.Position:
                                {
                                    if (this.ViewSite.SelectedInstallation != null)
                                    {
                                        filterById = this.ViewSite.SelectedInstallation.Bar_Position_ID;
                                    }
                                    else
                                    {
                                        filterById = -1;
                                    }
                                }
                                break;

                            default:
                                {
                                    if (this.ViewSite.SelectedSite != null)
                                    {
                                        filterById = this.ViewSite.SelectedSite.Site_ID;
                                    }
                                }
                                break;
                        }
                    }

                    HourlyFilterByValuesEntity values = _hourlyBusiness.GetFilterByValues(_selectedFilterBy.Key, filterById);
                    foreach (HourlyFilterByValueEntity value in values)
                    {
                        cboFilterByValue.Items.Add(value);
                    }
                    if (cboFilterByValue.Items.Count > 0)
                    {
                        cboFilterByValue.SelectedIndex = 0;
                    }
                }
                lvwHourly.Items.Clear();
                //set selected item in Hourly type based on site settings.
                this.setDefaultHourlyType();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }


        public void setDefaultHourlyType()
        {
            ModuleProc PROC = new ModuleProc("", "setDefaultHourlyType");

            try
            {
                string DefaultItem = string.Empty;

                if (this.ViewSite.SelectedSite != null)
                {
                    _hourlyBusiness.GetExchangeSitesettings(this.ViewSite.SelectedSite.Site_ID, "Hourly_DefaultItem", ref DefaultItem);

                    HourlyStatisticsTypesEntity t_en = ((System.Windows.Forms.BindingSource)(cboHourlyTypes.DataSource)).DataSource as HourlyStatisticsTypesEntity;
                    if (t_en.Find(o => o.HST_Desc.ToUpper() == DefaultItem.ToUpper()) != null)
                    {
                        cboHourlyTypes.SelectedItem = cboHourlyTypes.Items.OfType<HourlyStatisticsTypeEntity>().FirstOrDefault(x => x.HST_Desc == DefaultItem);
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.Exception(PROC, Ex);
            }

        }

        public void ExportReport()
        {
            GlobalHelper.ExportTocsv(lvwHourly);
        }

        public void ClearItems()
        {
            lvwHourly.Items.Clear();
        }
        #region IUserControl2 Members

        public void ClearControl()
        {
            ModuleProc PROC = new ModuleProc("", "ClearControl");

            try
            {
                lvwHourly.Items.Clear();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #endregion

        public void Activated()
        {
            ModuleProc PROC = new ModuleProc("", "Activated");

            try
            {
                cPeriodCount1.SelectedCount = this.ViewSite.GetSelectedPeriodCount(ViewSiteHelper.TAB_HOURLY, ViewSiteHelper.SUB_TAB_HOURLY_DETAILS, 48);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public void DeActivated()
        {
            ModuleProc PROC = new ModuleProc("", "Deactivated");

            try
            {
                this.ViewSite.SetSelectedPeriodCount(ViewSiteHelper.TAB_HOURLY, ViewSiteHelper.SUB_TAB_HOURLY_DETAILS, cPeriodCount1.SelectedCount);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void cboFilterByValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvwHourly.Items.Clear();
        }

        private void lvwHourly_ColumnWidthChanging_1(object sender, ColumnWidthChangingEventArgs e)
        {
            if (!isAssetEnabled)
            {
                if (e.ColumnIndex == 2)
                {
                    e.Cancel = true;
                    e.NewWidth = 0;
                }
            }
        }

        private void UcVSHourlyDetails_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
        }       

        
        public int GetTextWidthByFontStyle(string targetString, Font targetFont)
        {
            ModuleProc PROC = new ModuleProc("", "GetTextWidthByFontStyle");
            try
            {
                return TextRenderer.MeasureText(targetString, targetFont).Width;
            }
            catch (Exception ex)
            {

                Log.Exception(PROC, ex);
                return 0;
            }
        }

        /// <summary>
        /// This event handler is used to populate the tooltip when the selected text length exceeds the width of the combobox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboFilterByValue_MouseHover_1(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "cboFilterByValue_MouseHover");
            try
            {
                if (GetTextWidthByFontStyle(cboFilterByValue.Text, cboFilterByValue.Font) > cboFilterByValue.Size.Width)
                {
                    if (ttpFilterBy == null)
                    {
                        ttpFilterBy = new ToolTip();
                    }
                    ttpFilterBy.SetToolTip(cboFilterByValue, cboFilterByValue.Text);
                    ttpFilterBy.Show(cboFilterByValue.Text, cboFilterByValue);
                    ttpFilterBy.AutoPopDelay = 5000;
                }
            }
            catch (Exception ex)
            {

                Log.Exception(PROC, ex);
            }
        }
        /// <summary>
        /// This method is used to calculate the width of the given text to render in particular combobox
        /// </summary>
        /// <param name="targetString"></param>
        /// <param name="targetFont"></param>
        /// <returns></returns>
        private void cboFilterByValue_MouseLeave_1(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "cboFilterByValue_MouseLeave");
            try
            {
                if (ttpFilterBy != null)
                {
                    ttpFilterBy.RemoveAll();
                }
            }
            catch (Exception ex)
            {

                Log.Exception(PROC, ex);
            }
        }
    }
}
