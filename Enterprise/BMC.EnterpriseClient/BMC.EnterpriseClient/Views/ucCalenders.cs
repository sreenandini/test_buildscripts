/*
Author:Aishwarrya V S
To create a new Calendar, Calendar Period and Calendar Week details.
  */
#region Using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseDataAccess;
using BMC.Common.ExceptionManagement;
using BMC.CoreLib.Win32;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.Utilities;
using System.Globalization;
using BMC.Common;
#endregion Using
namespace BMC.EnterpriseClient.Views
{
    public partial class ucCalenders : UserControl
    {
        #region User defined Variables
        private CalendarBusiness objCalendarBiz = null;
        private const string DBFormat = "dd-MM-yyyy HH:mm:ss";
        public int Calendar_ID = 0;
        int Calendar_Period_Number;
        int _iNewCalendarID = 0;
        string Calendar_Description = string.Empty;
        string Calendar_Year_Start_Date = string.Empty;
        string Calendar_Year_End_Date = string.Empty;
        string Calendar_Period_start_date = string.Empty;
        string Calendar_Period_End_Date = string.Empty;

        string Old_yeardate = string.Empty;
        string Old_yearEndDate = string.Empty;
        List<CalendarPeriod> lstperiod = null;
        List<CalendarWeek> lstWeek = null;
        DateTime dtWeekEnd;
        DateTime dtPeriodEnd;
        string _sActionType = "New";
        bool _bIsCalendarListLoadComplete = false;
        string addCaption, cancelCaption;
        bool _bIsNewCalendar = false;
        int _PerviousCalendarID = 0;
        string _sCalendarBasedOn = "Date";
        int PerviousCalendar_ID = 0;
        #endregion User defined Variables

        #region Constructor
        public ucCalenders()
        {
            try
            {
            InitializeComponent();
            objCalendarBiz = CalendarBusiness.CreateInstance();

            // Set Tags for controls
            SetTagProperty();

            // For Button Captions (Add New/Cancel)
            addCaption = this.GetResourceTextByKey("Key_AddCaption");
            cancelCaption = this.GetResourceTextByKey("Key_CancelCaption");

            // Change Request #205362  fix.
            DTCalendarYearStart.CustomFormat = Common.Utilities.Common.GetDateFormatByUserSetting();
            DTCalendarYearEnd.CustomFormat = Common.Utilities.Common.GetDateFormatByUserSetting();
            DTCalendarPeriodStartDate.CustomFormat = Common.Utilities.Common.GetDateFormatByUserSetting();
            DTCalendarPeriodEndDate.CustomFormat = Common.Utilities.Common.GetDateFormatByUserSetting();
            DTCalendarWeekStartDate.CustomFormat = Common.Utilities.Common.GetDateFormatByUserSetting();
            DTCalendarWeekEndDate.CustomFormat = Common.Utilities.Common.GetDateFormatByUserSetting();
        }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SetTagProperty()
        {
            try
            {
            this.btnCalendarPeriodAddNew.Tag = "Key_AddCaption";
            this.btn_UpdatePeriod.Tag = "Key_UpdateCaption";
            this.btnCalendarPeriodApply.Tag = "Key_Apply";
            this.btnCalendarWeekAddNew.Tag = "Key_AddCaption";
            this.btnCalendarWeekApply.Tag = "Key_Apply";
            this.lbl_PeriodEndDate.Tag = "Key_EndColon";
            this.lbl_WeekEndDate.Tag = "Key_EndColon";
            this.EndDate.Tag = "Key_EndDate";
            this.columnHeader3.Tag = "Key_EndDate";
            this.Number.Tag = "Key_Number";
            this.columnHeader1.Tag = "Key_Number";
            this.lbl_CalendarPeriodNo.Tag = "Key_NumberColon";
            this.lbl_WeekNumber.Tag = "Key_NumberColon";
            this.grpPeriods.Tag = "Key_Periods";
            this.lbl_PeriodStartdate.Tag = "Key_StartColon";
            this.lbl_WeekStartDate.Tag = "Key_StartColon";
            this.StartDate.Tag = "Key_StartDate";
            this.columnHeader2.Tag = "Key_StartDate";
            this.btn_UpdateWeek.Tag = "Key_UpdateCaption";
            this.grpWeek.Tag = "Key_Weeks";
            this.lblYear.Tag = "Key_YearBeginsColon";
            this.lb.Tag = "Key_YearEndColon";
            this.btnCalendarNew.Tag = "Key_NewCaption";
            this.btnCalendarApply.Tag = "Key_Apply";
        }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion Constructor

        #region Events
        /// <summary>
        /// To Create new Calendar Name and evaluates whether Calendar Name already exsists.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalendarNew_Click(object sender, EventArgs e)
        {
            try
            {

                if (_sActionType == "New")
                {
                    txt_CalendarName.Focus();
                    PerviousCalendar_ID = 0;
                    _bIsNewCalendar = true;
                    EnableDisableControls("NewCalendar");
                    DTCalendarYearStart.Value = DateTime.Now;
                    DTCalendarYearEnd.Value = DateAndTime.DateAdd(DateInterval.Day, -1, DateAndTime.DateAdd(DateInterval.Year, 1, DateTime.Now));
                }
                else
                {
                    EnableDisableControls("UpdateCalendar");
                    _bIsNewCalendar = false;
                    LoadCalendars();                    
                    if (lb_Calendars.Items.Count > 0)
                    {
                        grpPeriods.Enabled = true;
                        grpWeek.Enabled = true;
                }
                    else
                    {
                        grpPeriods.Enabled = false;
                        grpWeek.Enabled = false;
                        btnCalendarApply.Enabled = false;
                        DTCalendarYearEnd.Enabled = false;
                        DTCalendarYearStart.Enabled = false;
                        txt_CalendarName.Enabled = false;
            }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Code to Set Calendar Year Date and Checks whether the year duration already exsists and if not, Creates a Calendar entry in Calendar table. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnCalendarApply_Click(object sender, EventArgs e)
        {
            try
            {

                if (txt_CalendarName.Text.Trim().Length > 0)
                {
                    int? iNameExists = 0;
                    int CalendarId = _bIsNewCalendar ? 0 : Convert.ToInt32(lb_Calendars.SelectedValue);

                    objCalendarBiz.IsNameExists(txt_CalendarName.Text, CalendarId, ref iNameExists);
                    if (iNameExists > 0)
                    {
                        //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_CALENDARNAME_EXISTS"), MessageBoxButtons.OK, MessageBoxIcon.Warning);    // "Calendar name already exists"
                        this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_CALENDARNAME_EXISTS"), this.ParentForm.Text);
                        txt_CalendarName.Focus();
                        return;
                    }
                }
                else
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CALENDARNAME_EMPTY"), this.ParentForm.Text);
                    txt_CalendarName.Focus();
                    return;
                }
                bool? bRetValue = false;
                TimeSpan ts = (DTCalendarYearEnd.Value) - (DTCalendarYearStart.Value);
                if (!(ts.Days > 0))
                {
                    //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_DATE_INVALID"), MessageBoxButtons.OK, MessageBoxIcon.Error);   // "Invalid date specified"
                    this.ShowErrorMessageBox(this.GetResourceTextByKey(1, "MSG_DATE_INVALID"), this.ParentForm.Text);
                    return;
                }
                if (_bIsNewCalendar)
                {
                    Calendar_Description = txt_CalendarName.Text;
                    Calendar_Year_Start_Date = SaveDateTimeToDB(DTCalendarYearStart.Value.Date);
                    Calendar_Year_End_Date = SaveDateTimeToDB(DTCalendarYearEnd.Value.Date.AddDays(1).AddMilliseconds(-3));

                    objCalendarBiz.CheckCalendarDurationExists("CALENDAR", Calendar_Year_Start_Date, Calendar_Year_End_Date, ref bRetValue);

                    // "The date duration already exists. Do you want to continue?"
                    if ((bRetValue.Value && (this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_DATEDURATION_EXISTS"), this.ParentForm.Text) == DialogResult.Yes)) || bRetValue.Value == false)
                    {
                        _iNewCalendarID = objCalendarBiz.InsertCalendar(Calendar_Description, Calendar_Year_Start_Date, Calendar_Year_End_Date);
                        //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_CALENDAR_SAVED"), MessageBoxButtons.OK, MessageBoxIcon.Information);  // "Calendar details saved successfully"
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CALENDAR_SAVED"), this.ParentForm.Text);
                        InsertAuditCalendarName();//Audit
                        LoadCalendars();
                        //lb_Calendars.SelectedIndex = ((List<CalendarEntity>)lb_Calendars.DataSource).FindIndex((x) => { return (x.Calendar_Description == Calendar_Description) ? true : false; });
                    }
                    else
                    {
                        if (lvCalendarPeriods.Items.Count != 0 || lvCalendarWeeks.Items.Count != 0)
                        {
                            DTCalendarYearStart.Enabled = false;
                            DTCalendarYearEnd.Enabled = false;
                        }
                    }
                }
                else
                {
                    PerviousCalendar_ID = Convert.ToInt32(lb_Calendars.SelectedValue);
                    List<CalendarEntity> objCalendarEntity = objCalendarBiz.GetCalendar(Calendar_ID).ToList();
                    if (objCalendarEntity.Count > 0)
                    {
                        Calendar_Year_Start_Date = SaveDateTimeToDB(DTCalendarYearStart.Value.Date);
                        Calendar_Year_End_Date = SaveDateTimeToDB(DTCalendarYearEnd.Value.Date.AddDays(1).AddMilliseconds(-3));
                        objCalendarBiz.CheckCalendarDurationExists("CALENDAR", Calendar_Year_Start_Date, Calendar_Year_End_Date, ref bRetValue);

                        // "The date duration already exists. Do you want to continue?"
                        if ((bRetValue.Value && (this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_DATEDURATION_EXISTS"), this.ParentForm.Text) == DialogResult.Yes)) || bRetValue.Value == false)
                        {
                            objCalendarBiz.UpdateCalendar(Calendar_ID, txt_CalendarName.Text, Calendar_Year_Start_Date, Calendar_Year_End_Date);

                            //"Calendar details updated successfully"
                            //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_CALENDAR_UPDATED"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CALENDAR_UPDATED"), this.ParentForm.Text);
                            //Audit if year date is updated.
                            if (Old_yeardate != Calendar_Year_Start_Date)
                                UpdateStartDateAudit(Calendar_Description, Calendar_Year_Start_Date);
                            if (Old_yearEndDate != Calendar_Year_End_Date)
                                UpdateEndDateAudit(Calendar_Description, Calendar_Year_End_Date);

                            DTCalendarWeekStartDate.Value = DTCalendarYearStart.Value;
                            DTCalendarWeekEndDate.Value = DateAndTime.DateAdd(DateInterval.Day, 7, DTCalendarYearStart.Value);
                            DTCalendarPeriodStartDate.Value = DTCalendarYearStart.Value;
                            DTCalendarPeriodEndDate.Value = DateAndTime.DateAdd(DateInterval.Day, 29, DTCalendarYearStart.Value);
                        }
                    }
                    else
                    {
                        //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_CALENDAR_ERROR"), MessageBoxButtons.OK, MessageBoxIcon.Error);   // "Error retrieving calendar details"
                        this.ShowErrorMessageBox(this.GetResourceTextByKey(1, "MSG_CALENDAR_ERROR"), this.ParentForm.Text);
                        return;
                    }
                }
                _bIsNewCalendar = false;
                EnableDisableControls("UpdateCalendar");
                LoadCalendars();
                if (PerviousCalendar_ID > 0)
                {
                    lb_Calendars.SelectedValue = PerviousCalendar_ID;
                }
                else if (lb_Calendars.Items.Count > 0)
                {
                    lb_Calendars.SelectedValue = _iNewCalendarID;                    
                }
            }

            catch (Exception ex)
            {
                _bIsNewCalendar = false;
                ExceptionManager.Publish(ex);
                EnableDisableControls("UpdateCalendar");
                LoadCalendars();
            }
        }

        string SaveDateTimeToDB(DateTime dt_Value)
        {
            try
            {
            // Change Request #205362  fix.           
                return dt_Value.ToString(DBFormat);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return DateTime.Now.ToString(DBFormat);
        }
        DateTime ConvertToCurrentDateFormat(string str_Value)
        {
            try
            {
            	return DateTime.ParseExact(str_Value, DBFormat, new CultureInfo("en-US"), DateTimeStyles.None);
        	}
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return DateTime.Now;
        }

        /// <summary>
        /// Add New Button:
        /// Period number= increments by 1 for each add new click
        /// Period Start Date= Calendar Year Start date will be set
        /// Period End Date=increments by a month and decrements a day of period start date.
        /// Cancel Button:
        /// Reverts back the old value to the textboxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalendarPeriodAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                EnableDisableControls("NewCalendarPeriod");
                if (lb_Calendars.SelectedIndex >= 0)
                {
                    if (btnCalendarPeriodAddNew.Text == addCaption)      //"&Add New":  
                    {
                        lb_Calendars.Enabled = false;
                        if (lvCalendarPeriods.Items.Count == 0)
                        {
                            txtCalendarPeriodNumber.Text = "1";
                            DTCalendarPeriodStartDate.Value = DTCalendarYearStart.Value;
                            DTCalendarPeriodEndDate.Value = DTCalendarPeriodStartDate.Value.AddMonths(1).AddDays(-1);
                        }
                        else
                        {
                            int iDays = DTCalendarYearEnd.Value.Subtract(dtPeriodEnd).Days;
                            if (iDays <= 0)
                            {
                                //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_PERIODEXCEEDS_CURRENTYEAR"), MessageBoxButtons.OK, MessageBoxIcon.Information);   // "Period duration exceeds current calendar year"
                                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_PERIODEXCEEDS_CURRENTYEAR"), this.ParentForm.Text);
                                btnCalendarPeriodAddNew.Text = addCaption;    //"Add New";
                                EnableDisableControls("CalendarPeriodUpdate");
                                return;
                            }
                            if (iDays < 30)
                            {
                                // "Do you want to add " + iDays + " days as new period?"
                                if (this.ShowQuestionMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_ADD_PERIOD"), iDays), this.ParentForm.Text) == DialogResult.Yes)
                                {
                                    txtCalendarPeriodNumber.Text = (Convert.ToInt32(lvCalendarPeriods.Items[lvCalendarPeriods.Items.Count - 1].SubItems[0].Text) + 1).ToString();
                                    DTCalendarPeriodStartDate.Value = Convert.ToDateTime(lstperiod[lstperiod.Count - 1].Calendar_Period_End_Date).AddDays(1);
                                    DTCalendarPeriodEndDate.Value = DateAndTime.DateAdd(DateInterval.Day, -1, DateAndTime.DateAdd(DateInterval.Day, iDays, DTCalendarPeriodStartDate.Value));
                                }
                            }
                            else
                            {
                                txtCalendarPeriodNumber.Text = (Convert.ToInt32(lvCalendarPeriods.Items[lvCalendarPeriods.Items.Count - 1].SubItems[0].Text) + 1).ToString();
                                DTCalendarPeriodStartDate.Value = Convert.ToDateTime(lstperiod[lstperiod.Count - 1].Calendar_Period_End_Date).AddDays(1);
                                DTCalendarPeriodEndDate.Value = DTCalendarPeriodStartDate.Value.AddMonths(1).AddDays(-1);
                            }
                        }
                        btnCalendarPeriodAddNew.Text = cancelCaption;   //"&Cancel"
                        btnCalendarPeriodApply.Focus();
                    }
                    else if (btnCalendarPeriodAddNew.Text == cancelCaption)   //"&Cancel"
                    {
                        btnCalendarPeriodAddNew.Text = addCaption;      // "&Add New";               
                        if (lvCalendarPeriods.SelectedItems != null)
                        {
                            if (lvCalendarPeriods.Items.Count > 0)
                            {
                                lvCalendarPeriods.Items[0].Selected = true;
                            }
                            GetLastPeriodCount();
                            EnableDisableControls("CalendarPeriodUpdate");
                        }
                    }
                    this.lvCalendarPeriods.HideSelection = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Saves the details to the Calendar_period table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalendarPeriodApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (lb_Calendars.SelectedIndex >= 0)
                {
                    Calendar_ID = Convert.ToInt32(lb_Calendars.SelectedValue.ToString());
                    Calendar_Period_Number = Convert.ToInt32(txtCalendarPeriodNumber.Text);
                    if (!txtCalendarPeriodNumber.Text.IsNumeric())
                    {
                        //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_PERIOD_INVALID"), MessageBoxButtons.OK, MessageBoxIcon.Information);   // "Invalid period number"
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_PERIOD_INVALID"), this.ParentForm.Text);
                        EnableDisableControls("CalendarPeriodUpdate");
                        return;
                    }

                    TimeSpan ts = (DTCalendarPeriodEndDate.Value) - (DTCalendarPeriodStartDate.Value);
                    if (ts.Days < 0)
                    {
                        //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_DATE_INVALID"), MessageBoxButtons.OK, MessageBoxIcon.Information);   // "Invalid dates specified"
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_DATE_INVALID"), this.ParentForm.Text);
                        return;
                    }
                    if (!(DTCalendarPeriodEndDate.Value.Date <= DTCalendarYearEnd.Value && DTCalendarPeriodStartDate.Value.Date >= DTCalendarYearStart.Value.Date))
                    {
                        //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_WEEKEXCEEDS_CURRENTYEAR"), MessageBoxButtons.OK, MessageBoxIcon.Information);   // "Week duration exceeds current calendar year"
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_WEEKEXCEEDS_CURRENTYEAR"), this.ParentForm.Text);
                        return;
                    }

                    else if (btnCalendarPeriodAddNew.Text == cancelCaption)   //"&Cancel"
                    {
                        try
                        {
                            int Cancel = objCalendarBiz.GetCancelPeriod(Calendar_ID, Calendar_Period_Number).Count();
                            // bool? bRetValue = false;
                            if (Cancel == 0)
                            {
                                DateTime sCheckEndDate = DateTime.Now;
                                DateTime sCheckStartDate = DateTime.Now;
                                Calendar_ID = Convert.ToInt32(lb_Calendars.SelectedValue.ToString());
                                Calendar_Period_Number = Convert.ToInt32(txtCalendarPeriodNumber.Text);
                                Calendar_Period_start_date = SaveDateTimeToDB(DTCalendarPeriodStartDate.Value.Date);
                                Calendar_Period_End_Date = SaveDateTimeToDB(DTCalendarPeriodEndDate.Value.Date.AddDays(1).AddMilliseconds(-3));
                                lstperiod = objCalendarBiz.GetCalendarPeriod(Calendar_ID).ToList();
                                if (txtCalendarPeriodNumber.Text != "1")
                                {
                                    sCheckEndDate = lstperiod[lstperiod.Count - 1].Calendar_Period_End_Date;
                                    sCheckStartDate = lstperiod[lstperiod.Count - 1].Calendar_Period_Start_Date;
                                }
                                if (DTCalendarPeriodStartDate.Value.Date > sCheckEndDate.Date && DTCalendarPeriodStartDate.Value.Date <= DTCalendarPeriodEndDate.Value.Date || txtCalendarPeriodNumber.Text == "1")
                                {
                                    objCalendarBiz.AddNewCalendarPeriod(Calendar_Period_Number, Calendar_Period_start_date, Calendar_Period_End_Date, Calendar_ID);
                                    Calendar_Description = lb_Calendars.Text;
                                    InsertAuditCalendarPeriod(Calendar_Period_Number);//Audit
                                    btnCalendarPeriodAddNew.Text = addCaption;
                                    LoadListCalendars();
                                    btnCalendarPeriodAddNew.Focus();
                                    GetLastPeriodCount();
                                }
                                else
                                {
                                    // this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_PERIODEXISTS_CANNOTINSERT"), MessageBoxButtons.OK, MessageBoxIcon.Information);  // Period duration already exists, values cannot be inserted
                                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_PERIODEXISTS_CANNOTINSERT"), this.ParentForm.Text);
                                }
                            }
                            else
                            {
                                // this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_PERIODNUMBER_EXISTS"), MessageBoxButtons.OK, MessageBoxIcon.Information);  // That period number already exists
                                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_PERIODNUMBER_EXISTS"), this.ParentForm.Text);
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.Publish(ex);
                        }
                    }

                }
                EnableDisableControls("CalendarPeriodUpdate");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Add New Button:
        /// Week number= increments by 1 for each add new click
        /// Week Start Date= Calendar Year Start date will be set
        /// Week End Date=increments by a month and decrements a day of Week start date.
        /// Cancel Button:
        /// Reverts back the old value to the text boxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnCalendarWeekAddNew_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (lb_Calendars.SelectedIndex >= 0)
                {
                    if (btnCalendarWeekAddNew.Text == addCaption)   // "Add &New"
                    {
                        EnableDisableControls("NewCalendarWeek");
                        try
                        {
                            if (lvCalendarWeeks.Items.Count == 0)
                            {
                                txtCalendarWeekNumber.Text = "1";
                                DTCalendarWeekStartDate.Value = DTCalendarYearStart.Value;
                                DTCalendarWeekEndDate.Value = DateAndTime.DateAdd(DateInterval.Day, -1, DateAndTime.DateAdd(DateInterval.WeekOfYear, 1, DTCalendarWeekStartDate.Value));
                            }
                            else
                            {
                                int iDays = DTCalendarYearEnd.Value.Subtract(dtWeekEnd).Days;
                                if (iDays <= 0)
                                {
                                    //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_WEEK_EXCEEDS_ENDDATE"), MessageBoxButtons.OK, MessageBoxIcon.Information);   // "Week duration exceeds calendar year end date"
                                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_WEEK_EXCEEDS_ENDDATE"), this.ParentForm.Text);
                                    EnableDisableControls("CalendarWeekUpdate");
                                    return;
                                }
                                if (iDays < 7)
                                {
                                    // "Do you want to add remaining " + iDays + " days as new week?"
                                    if (this.ShowQuestionMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_ADD_WEEK"), iDays), this.ParentForm.Text) == DialogResult.Yes)
                                    {
                                        txtCalendarWeekNumber.Text = (Convert.ToInt32(lvCalendarWeeks.Items[lvCalendarWeeks.Items.Count - 1].SubItems[0].Text) + 1).ToString(); ;
                                        DTCalendarWeekStartDate.Value = Convert.ToDateTime(lstWeek[lstWeek.Count - 1].Calendar_Week_End_Date).AddDays(1); //DateTime dtNew = DateTime.ParseExact("02-14-2013 23:00", "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None);
                                        DTCalendarWeekEndDate.Value = DateAndTime.DateAdd(DateInterval.Day, -1, DateAndTime.DateAdd(DateInterval.Day, iDays, DTCalendarWeekStartDate.Value));
                                    }
                                }
                                else
                                {
                                    txtCalendarWeekNumber.Text = (Convert.ToInt32(lvCalendarWeeks.Items[lvCalendarWeeks.Items.Count - 1].SubItems[0].Text) + 1).ToString(); ;
                                    DTCalendarWeekStartDate.Value = Convert.ToDateTime(lstWeek[lstWeek.Count - 1].Calendar_Week_End_Date).AddDays(1);
                                    DTCalendarWeekEndDate.Value = DateAndTime.DateAdd(DateInterval.Day, -1, DateAndTime.DateAdd(DateInterval.WeekOfYear, 1, DTCalendarWeekStartDate.Value));
                                }
                            }
                            lvCalendarWeeks.Enabled = false;
                            btnCalendarWeekAddNew.Text = cancelCaption;    // "Ca&ncel";
                            btnCalendarWeekApply.Focus();
                        }

                        catch (Exception ex)
                        {
                            ExceptionManager.Publish(ex);
                        }
                    }
                    else if (btnCalendarWeekAddNew.Text == cancelCaption)   // "Ca&ncel"
                    {
                        try
                        {
                            lb_Calendars.Enabled = true;
                            lvCalendarWeeks.Enabled = true;
                            btnCalendarWeekAddNew.Text = addCaption;    // "Add &New";
                            if (lvCalendarWeeks.SelectedItems != null)
                            {
                                if (lvCalendarWeeks.Items.Count > 0)
                                {
                                    lvCalendarWeeks.Items[0].Selected = true;
                                }
                                GetLastWeekCount();
                            }
                            EnableDisableControls("CalendarWeekUpdate");
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.Publish(ex);
                        }
                    }
                    //}
                    this.lvCalendarWeeks.HideSelection = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Save the Week Details to Calendar_Week table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalendarWeekApply_Click(object sender, EventArgs e)
        {
            try
            {
                int Calendar_Week_Number = 0;
                string Calendar_Week_start_date = null;
                string Calendar_Week_End_Date = null;
                if (lb_Calendars.SelectedIndex >= 0)
                {
                    Calendar_Week_Number = Convert.ToInt32(txtCalendarWeekNumber.Text);
                    Calendar_ID = Convert.ToInt32(lb_Calendars.SelectedValue.ToString());
                    if (!txtCalendarWeekNumber.Text.IsNumeric())
                    {
                        //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_WEEK_INVALID"), MessageBoxButtons.OK, MessageBoxIcon.Information);  // "Invalid week number"
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_WEEK_INVALID"), this.ParentForm.Text);
                        return;
                    }
                    if (!(DTCalendarWeekEndDate.Value.Date <= DTCalendarYearEnd.Value && DTCalendarWeekStartDate.Value.Date >= DTCalendarYearStart.Value.Date))
                    {
                        //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_WEEKEXCEEDS_CURRENTYEAR"), MessageBoxButtons.OK, MessageBoxIcon.Information);   // "Week duration exceeds current calendar year"
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_WEEKEXCEEDS_CURRENTYEAR"), this.ParentForm.Text);
                        //btnCalendarWeekAddNew.Text = "Add New";
                        //lvCalendarWeeks.Enabled = false;
                        return;
                    }
                    TimeSpan ts = (DTCalendarWeekEndDate.Value) - (DTCalendarWeekStartDate.Value);
                    if (ts.Days < 0)
                    {
                        //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_DATE_INVALID"), MessageBoxButtons.OK, MessageBoxIcon.Information);   // "Invalid dates specified"
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_DATE_INVALID"), this.ParentForm.Text);
                        return;
                    }

                    if (btnCalendarWeekAddNew.Text == addCaption)
                    {
                        try
                        {
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.Publish(ex);
                        }
                    }
                    else if (btnCalendarWeekAddNew.Text == cancelCaption)
                    {
                        try
                        {
                            int objcancel = objCalendarBiz.GetCancelWeek(Calendar_ID, Calendar_Week_Number).Count();
                            if (objcancel == 0)
                            {
                                Calendar_ID = Convert.ToInt32(lb_Calendars.SelectedValue.ToString());
                                Calendar_Week_Number = Convert.ToInt32(txtCalendarWeekNumber.Text);
                                Calendar_Week_start_date = SaveDateTimeToDB(DTCalendarWeekStartDate.Value.Date);
                                Calendar_Week_End_Date = SaveDateTimeToDB(DTCalendarWeekEndDate.Value.Date.AddDays(1).AddMilliseconds(-3));
                                Calendar_Description = lb_Calendars.Text;
                                DateTime sCheckEndDate = DateTime.Now;
                                DateTime sCheckStartDate = DateTime.Now;

                                // objCalendarBiz.CheckCalendarDurationExists("CALENDAR_WEEK", Calendar_Week_start_date, Calendar_Week_End_Date, ref bRetValue);
                                lstWeek = objCalendarBiz.GetCalendarWeek(Calendar_ID).ToList();
                                if (txtCalendarWeekNumber.Text != "1")
                                {
                                    sCheckEndDate = lstWeek[lstWeek.Count - 1].Calendar_Week_End_Date;
                                    sCheckStartDate = lstWeek[lstWeek.Count - 1].Calendar_Week_Start_Date;
                                }
                                if (DTCalendarWeekStartDate.Value.Date > sCheckEndDate.Date && DTCalendarWeekStartDate.Value.Date <= DTCalendarWeekEndDate.Value.Date || txtCalendarWeekNumber.Text == "1")
                                {
                                    objCalendarBiz.AddNewCalendarWeek(Calendar_Week_Number, Calendar_Week_start_date, Calendar_Week_End_Date, Calendar_ID);
                                    InsertAuditCalendarWeek(Calendar_Week_Number);//Audit
                                    btnCalendarWeekAddNew.Text = this.GetResourceTextByKey("Key_AddCaption");     // "Add &New";
                                    LoadListCalendars();
                                    btnCalendarWeekAddNew.Focus();
                                    GetLastWeekCount();
                                }
                                else
                                {
                                    //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_WEEKEXISTS_CANNOTINSERT"), MessageBoxButtons.OK, MessageBoxIcon.Information);  // "Week duration already exists, values cannot be inserted"
                                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_WEEKEXISTS_CANNOTINSERT"), this.ParentForm.Text);
                                    return;
                                }
                            }

                            else
                            {
                                //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_PERIODNUMBER_EXISTS"), MessageBoxButtons.OK, MessageBoxIcon.Information);   // "That period number already exists"
                                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_PERIODNUMBER_EXISTS"), this.ParentForm.Text);
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.Publish(ex);
                        }
                    }
                }
                EnableDisableControls("CalendarWeekUpdate");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        /// <summary>
        /// To Load Calendar Period and Calendar Week Details in listview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        /// <summary>
        /// Selecting row in Calendar Period Listview assigns value to the corresponding textbox and combo box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvCalendarPeriods_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem item in lvCalendarPeriods.SelectedItems)
                {
                    txtCalendarPeriodNumber.Text = item.SubItems[0].Text;
                    DTCalendarPeriodStartDate.Value = Convert.ToDateTime(item.SubItems[1].Text);
                    DTCalendarPeriodEndDate.Value = Convert.ToDateTime(item.SubItems[2].Text);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Selecting row in Calendar Week Listview assigns value to the corresponding textbox and combo box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvCalendarWeeks_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem item in lvCalendarWeeks.SelectedItems)
                {
                    txtCalendarWeekNumber.Text = item.SubItems[0].Text;
                    DTCalendarWeekStartDate.Value = Convert.ToDateTime(item.SubItems[1].Text);
                    DTCalendarWeekEndDate.Value = Convert.ToDateTime(item.SubItems[2].Text);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Allows to Update Calendar Period Date's
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (lb_Calendars.SelectedIndex >= 0)
                {
                    if (DTCalendarPeriodStartDate.Value.Date > DTCalendarPeriodEndDate.Value.Date)
                    {
                        //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_DATE_INVALID"), MessageBoxButtons.OK, MessageBoxIcon.Warning);   // "Invalid date specified"
                        this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_DATE_INVALID"), this.ParentForm.Text);
                        return;
                    }

                    if (DTCalendarPeriodStartDate.Value.Date >= DTCalendarYearStart.Value.Date
                        && DTCalendarPeriodStartDate.Value.Date <= DTCalendarYearEnd.Value.Date
                        && DTCalendarPeriodEndDate.Value.Date >= DTCalendarYearStart.Value.Date
                        && DTCalendarPeriodEndDate.Value.Date <= DTCalendarYearEnd.Value.Date)
                    {
                        Calendar_ID = Convert.ToInt32(lb_Calendars.SelectedValue.ToString());
                        Calendar_Period_Number = Convert.ToInt32(txtCalendarPeriodNumber.Text);
                        string Calendar_Period_start_date = SaveDateTimeToDB(DTCalendarPeriodStartDate.Value.Date);
                        string Calendar_Period_End_Date = SaveDateTimeToDB(DTCalendarPeriodEndDate.Value.Date.AddDays(1).AddMilliseconds(-3));
                        DateTime sCheckNextStartDate = DateTime.Now;
                        DateTime sCheckNextEndDate = DateTime.Now;
                        DateTime sCheckPreStartDate = DateTime.MinValue;
                        DateTime sCheckPreEndDate = DateTime.MinValue;
                        List<ListViewItem> oListItem = new List<ListViewItem>();
                        ListViewItem olvItem = lvCalendarPeriods.SelectedItems[0];
                        if (olvItem != null)
                        {
                            oListItem = lvCalendarPeriods.Items.OfType<ListViewItem>().ToList();
                            int ind = oListItem.IndexOf(olvItem);
                            if (ind != -1)
                            {
                                ListViewItem prev = null;
                                ListViewItem next = null;
                                if (ind + 1 <= oListItem.Count - 1)
                                {
                                    next = oListItem[ind + 1];
                                }

                                if (ind > 0 && ind - 1 < oListItem.Count - 1)
                                {
                                    prev = oListItem[ind - 1];
                                }

                                if (prev != null)
                                {
                                    sCheckPreStartDate = Convert.ToDateTime(prev.SubItems[1].Text);
                                    sCheckPreEndDate = Convert.ToDateTime(prev.SubItems[2].Text);
                                }
                                if (next != null)
                                {
                                    sCheckNextStartDate = Convert.ToDateTime(next.SubItems[1].Text);
                                    sCheckNextEndDate = Convert.ToDateTime(next.SubItems[2].Text);
                                }
                                if ((next == null && DTCalendarPeriodStartDate.Value.Date > sCheckPreEndDate.Date
                                    && DTCalendarPeriodEndDate.Value.Date >= DTCalendarPeriodStartDate.Value.Date)
                                    || (prev != null && next != null && DTCalendarPeriodStartDate.Value.Date > sCheckPreEndDate.Date
                                   && DTCalendarPeriodEndDate.Value.Date < sCheckNextStartDate.Date) || (next != null &&
                                   DTCalendarPeriodStartDate.Value.Date <= DTCalendarPeriodEndDate.Value.Date
                                   && DTCalendarPeriodEndDate.Value.Date < sCheckNextStartDate.Date && DTCalendarPeriodStartDate.Value.Date >= DTCalendarYearStart.Value.Date && DTCalendarPeriodStartDate.Value.Date > sCheckPreEndDate.Date) || (prev == null && next == null && DTCalendarPeriodStartDate.Value.Date >= DTCalendarYearStart.Value.Date
                                   && DTCalendarPeriodEndDate.Value.Date <= DTCalendarYearEnd.Value.Date))
                                {
                                    objCalendarBiz.UpdateCalendarPeriod(Calendar_Period_Number, Calendar_Period_start_date, Calendar_Period_End_Date, Calendar_ID);
                                    lvCalendarPeriods.SelectedItems[0].Text = txtCalendarPeriodNumber.Text;
                                    foreach (ListViewItem item in lvCalendarPeriods.SelectedItems)
                                    {
                                        item.SubItems[1].Text = DTCalendarPeriodStartDate.Value.ToShortDateString();
                                        item.SubItems[2].Text = DTCalendarPeriodEndDate.Value.ToShortDateString();
                                        LoadListCalendars();
                                    }
                                    // this.ShowMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_PERIOD_UPDATED"), Calendar_Period_Number), MessageBoxButtons.OK, MessageBoxIcon.Information);   // "Calendar period number " + Calendar_Period_Number + " is updated successfully"
                                    this.ShowInfoMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_PERIOD_UPDATED"), Calendar_Period_Number), this.ParentForm.Text);
                                }
                                else
                                {
                                    //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_PERIODEXISTS_CANNOTUPDATE"), MessageBoxButtons.OK, MessageBoxIcon.Information);   // "Period duration already exists,values cannot be updated"
                                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_PERIODEXISTS_CANNOTUPDATE"), this.ParentForm.Text);
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_PERIODEXCEEDS_CURRENTYEAR"), MessageBoxButtons.OK, MessageBoxIcon.Information);   // "Period duration exceeds current calendar year"
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_PERIODEXCEEDS_CURRENTYEAR"), this.ParentForm.Text);
                        return;
                    }
                    EnableDisableControls("CalendarPeriodUpdate");
                    btnCalendarPeriodAddNew.Text = addCaption;
                }
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Allows to Update Calendar Week Date's.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_UpdateWeek_Click(object sender, EventArgs e)
        {
            try
            {
                if (DTCalendarWeekStartDate.Value.Date > DTCalendarWeekEndDate.Value.Date)
                {
                    // this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_DATE_INVALID"), MessageBoxButtons.OK, MessageBoxIcon.Warning);   // "Invalid date specified"
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_DATE_INVALID"), this.ParentForm.Text);
                    return;
                }
                int Calendar_Week_Number = Convert.ToInt32(txtCalendarWeekNumber.Text);
                TimeSpan ts = (DTCalendarYearEnd.Value) - (DTCalendarYearStart.Value);
                if (ts.Days > 7 && DTCalendarWeekStartDate.Value.Date >= DTCalendarYearStart.Value.Date
                    && DTCalendarWeekStartDate.Value.Date <= DTCalendarYearEnd.Value.Date
                    && DTCalendarWeekEndDate.Value.Date >= DTCalendarYearStart.Value.Date
                    && DTCalendarWeekEndDate.Value.Date <= DTCalendarYearEnd.Value.Date)
                {
                    Calendar_ID = Convert.ToInt32(lb_Calendars.SelectedValue.ToString());
                    string Calendar_Week_start_date = SaveDateTimeToDB(DTCalendarWeekStartDate.Value.Date);
                    string Calendar_Week_End_Date = SaveDateTimeToDB(DTCalendarWeekEndDate.Value.Date.AddDays(1).AddMilliseconds(-3));
                    DateTime sCheckNextStartDate = DateTime.Now;
                    DateTime sCheckNextEndDate = DateTime.Now;
                    DateTime sCheckPreStartDate = DateTime.Now;
                    DateTime sCheckPreEndDate = DateTime.MinValue;
                    List<ListViewItem> oListItem = new List<ListViewItem>();
                    ListViewItem olvItem = lvCalendarWeeks.SelectedItems[0];
                    if (olvItem != null)
                    {
                        oListItem = lvCalendarWeeks.Items.OfType<ListViewItem>().ToList();
                        int ind = oListItem.IndexOf(olvItem);
                        if (ind != -1)
                        {
                            ListViewItem prev = null;
                            ListViewItem next = null;
                            if (ind + 1 <= oListItem.Count - 1)
                            {
                                next = oListItem[ind + 1];
                            }

                            if (ind > 0 && ind - 1 < oListItem.Count - 1)
                            {
                                prev = oListItem[ind - 1];
                            }

                            if (prev != null)
                            {
                                sCheckPreStartDate = Convert.ToDateTime(prev.SubItems[1].Text);
                                sCheckPreEndDate = Convert.ToDateTime(prev.SubItems[2].Text);
                            }
                            if (next != null)
                            {
                                sCheckNextStartDate = Convert.ToDateTime(next.SubItems[1].Text);
                                sCheckNextEndDate = Convert.ToDateTime(next.SubItems[2].Text);
                            }

                            if ((next == null && DTCalendarWeekStartDate.Value.Date > sCheckPreEndDate.Date
                                    && DTCalendarWeekEndDate.Value.Date >= DTCalendarWeekStartDate.Value.Date)
                                    || (prev != null && next != null && DTCalendarWeekStartDate.Value.Date > sCheckPreEndDate.Date
                                   && DTCalendarWeekEndDate.Value.Date < sCheckNextStartDate.Date) || (next != null &&
                                   DTCalendarWeekStartDate.Value.Date <= DTCalendarWeekEndDate.Value.Date
                                   && DTCalendarWeekEndDate.Value.Date < sCheckNextStartDate.Date
                                   && DTCalendarWeekStartDate.Value.Date >= DTCalendarYearStart.Value.Date && DTCalendarWeekStartDate.Value.Date > sCheckPreEndDate.Date) || (prev == null && next == null && DTCalendarWeekStartDate.Value.Date >= DTCalendarYearStart.Value.Date && DTCalendarWeekEndDate.Value.Date <= DTCalendarYearEnd.Value.Date))
                            {
                                objCalendarBiz.UpdateWeekCalendar(Calendar_Week_Number, Calendar_Week_start_date, Calendar_Week_End_Date, Calendar_ID);
                                lvCalendarWeeks.SelectedItems[0].Text = txtCalendarWeekNumber.Text;
                                foreach (ListViewItem item in lvCalendarWeeks.SelectedItems)
                                {
                                    item.SubItems[1].Text = DTCalendarWeekStartDate.Value.ToShortDateString();
                                    item.SubItems[2].Text = DTCalendarWeekEndDate.Value.ToShortDateString();
                                    LoadListCalendars();
                                }
                                //this.ShowMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_CALENDARWEEK_UPDATED"), Calendar_Week_Number), MessageBoxButtons.OK, MessageBoxIcon.Information);   // "Calendar week " + Calendar_Week_Number + " updated successfully"
                                this.ShowInfoMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_CALENDARWEEK_UPDATED"), Calendar_Week_Number), this.ParentForm.Text);
                            }
                            else
                            {
                                // this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_WEEKEXISTS_CANNOTUPDATE"), MessageBoxButtons.OK, MessageBoxIcon.Information);  // Week duration already exists, values cannot be updated
                                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_WEEKEXISTS_CANNOTUPDATE"), this.ParentForm.Text);
                            }
                        }

                    }
                }
                else
                {
                    //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_WEEKEXCEEDS_CURRENTYEAR"), MessageBoxButtons.OK, MessageBoxIcon.Information);   // Week duration exceeds current calendar year
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_WEEKEXCEEDS_CURRENTYEAR"), this.ParentForm.Text);
                    return;
                }
                btnCalendarWeekAddNew.Text = this.GetResourceTextByKey("Key_AddCaption");
            }

            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }

        }

        private void lvCalendarPeriods_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right && Control.ModifierKeys == Keys.Shift)
                {
                    if (lvCalendarPeriods.SelectedItems != null && lvCalendarPeriods.SelectedItems.Count > 0)
                    {
                        //this.ShowMessageBox(lvCalendarPeriods.SelectedItems[0].Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ShowInfoMessageBox(lvCalendarPeriods.SelectedItems[0].Name, this.ParentForm.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void lvCalendarWeeks_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && Control.ModifierKeys == Keys.ShiftKey)
            {
                if (lvCalendarWeeks.SelectedItems != null && lvCalendarWeeks.SelectedItems.Count > 0)
                {
                    //this.ShowMessageBox(lvCalendarWeeks.SelectedItems[0].Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowInfoMessageBox(lvCalendarWeeks.SelectedItems[0].Name, this.ParentForm.Text);
                }
            }
        }
        #endregion Events

        #region User defined Functions
        /// <summary>
        /// Load Calendar Combo box
        /// </summary>
        public void LoadCalendars()
        {
            try
            {
                List<CalendarEntity> objcalendar = objCalendarBiz.GetLstCalendarDetails(); //handle null exception
                lb_Calendars.DataSource = objcalendar;
                lb_Calendars.DisplayMember = "Calendar_Description";
                lb_Calendars.ValueMember = "Calendar_ID";
                txtCalendarPeriodNumber.Text = "";
                txtCalendarWeekNumber.Text = "";
                DTCalendarYearStart.Value = DateTime.Now;
                DTCalendarYearEnd.Value = DateTime.Now;
                DTCalendarPeriodStartDate.Value = DateTime.Now;
                DTCalendarPeriodEndDate.Value = DateTime.Now;
                DTCalendarWeekStartDate.Value = DateTime.Now;
                DTCalendarWeekEndDate.Value = DateTime.Now;
                _bIsCalendarListLoadComplete = true;
                btnCalendarApply.Enabled = (lb_Calendars.SelectedIndex == -1) ? false : true;
                if (lb_Calendars.Items.Count > 0)
                {
                    lb_Calendars_SelectedIndexChanged(objcalendar, null);
                }
                }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// To Load Calendar Period and Calendar Week Details in listview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LoadListCalendars()
        {
            ListViewItem lstItem = null;

            try
            {
                if (lb_Calendars.Items.Count > 0)
                {
                    //btnCalendarApply.Visible = true;
                    bool EnableYearsDates = false;
                    lvCalendarPeriods.Items.Clear();
                    lvCalendarWeeks.Items.Clear();
                    txt_CalendarPeriodBasedOn.Text = string.Empty;
                    txt_CalendarWeekBasedOn.Text = string.Empty;
                    if (lb_Calendars.SelectedValue == null) return;
                    Calendar_ID = Convert.ToInt32(lb_Calendars.SelectedValue);
                    List<CalendarEntity> objCalendarEntity = objCalendarBiz.GetCalendar(Calendar_ID).ToList();
                    txt_CalendarName.Text = lb_Calendars.Text;
                    _sCalendarBasedOn = objCalendarEntity[0].CalendarBasedOn;

                    //if (Convert.ToBoolean(objCalendarEntity[0].IsCalendarCreatedUsingAutoCalendar))
                    //{
                    //    txt_CalendarName.Enabled = true;
                    //    btnCalendarApply.Enabled = true;
                    //}
                    //else
                    //{
                    //    txt_CalendarName.Enabled = false;
                    //    btnCalendarApply.Enabled = false;
                    //}

                    if (lb_Calendars.SelectedIndex >= 0)
                    {
                        if (Calendar_ID > 0)
                        {
                            foreach (CalendarEntity objCal in objCalendarEntity)
                            {
                                Old_yeardate = objCal.Calendar_Year_Start_Date.ToString();
                                DTCalendarYearStart.Value = Convert.ToDateTime(Old_yeardate);
                                Old_yearEndDate = objCal.Calendar_Year_End_Date.ToString();
                                DTCalendarYearEnd.Value = Convert.ToDateTime(Old_yearEndDate);
                            }
                        }
                        lstperiod = objCalendarBiz.GetCalendarPeriod(Calendar_ID).ToList();
                        if (lstperiod.Count == 0)
                        {
                            EnableYearsDates = true;
                            txtCalendarPeriodNumber.Text = string.Empty;
                            DTCalendarPeriodStartDate.Value = DTCalendarYearStart.Value;
                            DTCalendarPeriodEndDate.Value = Convert.ToDateTime(DateAndTime.DateAdd(DateInterval.Day, 29, DTCalendarYearStart.Value).ToShortDateString());
                        }
                        else
                        {
                            EnableYearsDates = false;
                            foreach (CalendarPeriod obj in lstperiod)
                            {
                                txt_CalendarPeriodBasedOn.Text = _sCalendarBasedOn;
                                ListViewItem lv_item = new ListViewItem();
                                lv_item.Name = "PE," + obj.Calendar_Period_ID;
                                lv_item.Text = obj.Calendar_Period_Number.ToString();
                                lstItem = lvCalendarPeriods.Items.Add(lv_item);
                                lstItem.SubItems.Add(obj.Calendar_Period_Start_Date.ToShortDateString());
                                lstItem.SubItems.Add(obj.Calendar_Period_End_Date.ToShortDateString());
                            }
                            GetLastPeriodCount();
                        }
                    }

                    //Loads lvCalendarWeek
                    lstWeek = objCalendarBiz.GetCalendarWeek(Calendar_ID).ToList();
                    if (lstWeek.Count == 0)
                    {
                        EnableYearsDates = EnableYearsDates && true;
                        txtCalendarWeekNumber.Text = string.Empty;
                        DTCalendarWeekStartDate.Value = DTCalendarYearStart.Value;
                        DTCalendarWeekEndDate.Value = Convert.ToDateTime(DateAndTime.DateAdd(DateInterval.Day, 7, DTCalendarYearStart.Value).ToShortDateString());

                    }
                    else
                    {
                        EnableYearsDates = false;
                        foreach (CalendarWeek obj in lstWeek)
                        {
                            txt_CalendarWeekBasedOn.Text = _sCalendarBasedOn;
                            ListViewItem lv_item = new ListViewItem();
                            lv_item.Name = "WE," + obj.Calendar_Week_ID;
                            lv_item.Text = obj.Calendar_Week_Number.ToString();
                            lstItem = lvCalendarWeeks.Items.Add(lv_item);
                            lstItem.SubItems.Add(obj.Calendar_Week_Start_Date.ToShortDateString());
                            lstItem.SubItems.Add(obj.Calendar_Week_End_Date.ToShortDateString());
                        }
                        GetLastWeekCount();
                    }

                    if (EnableYearsDates)
                    {
                        DTCalendarYearStart.Enabled = true;
                        DTCalendarYearEnd.Enabled = true;
                        //btnCalendarApply.Visible = true;
                    }
                    else
                    {

                        DTCalendarYearStart.Enabled = false;
                        DTCalendarYearEnd.Enabled = false;
                        //btnCalendarApply.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Gets the total number of counts and selects the last row of the listview
        /// </summary>
        public void GetLastWeekCount()
        {
            try
            {
            var r = Enumerable.Empty<ListViewItem>();
            if (this.lvCalendarWeeks.Items.Count == 0)
            {
                txtCalendarWeekNumber.Text = "";
                return;
            }
            else
            {
                r = this.lvCalendarWeeks.Items.OfType<ListViewItem>();
                var last = r.LastOrDefault();
                last.Selected = true;
                dtWeekEnd = Convert.ToDateTime(last.SubItems[2].Text);
            }
        }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Gets the total number of counts and selects the last row of the listview
        /// </summary>
        public void GetLastPeriodCount()
        {
            try
            {
                var r = Enumerable.Empty<ListViewItem>();
                if (this.lvCalendarPeriods.Items.Count == 0)
                {
                    txtCalendarPeriodNumber.Text = "";
                    return;
                }
                else
                {
                    r = this.lvCalendarPeriods.Items.OfType<ListViewItem>();
                    var last = r.LastOrDefault();
                    last.Selected = true;
                    dtPeriodEnd = Convert.ToDateTime(last.SubItems[2].Text);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }
        #endregion User defined Functions

        #region Audit
        private void UpdateStartDateAudit(string Calendar_Description, string Calendar_Year_Start_Date)
        {
            try
            {
                AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                {
                    business.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        EnterpriseModuleName = ModuleNameEnterprise.Calendar,
                        Audit_Screen_Name = "CompanyCalendars",
                        Audit_Desc = "Calendar" + " " + "[" + Calendar_Description + "]" + "Modified" + "[Calendar Start Date]" + Old_yeardate + "-->" + Calendar_Year_Start_Date,
                        Audit_Field = "Calendar_Year_Start_Date",
                        AuditOperationType = OperationType.MODIFY,
                        Audit_New_Vl = Calendar_Year_Start_Date,
                        Audit_Old_Vl = Old_yeardate,
                        Audit_User_ID = AppEntryPoint.Current.UserId,
                        Audit_User_Name = AppEntryPoint.Current.UserName
                    }, false);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void UpdateEndDateAudit(string Calendar_Description, string Calendar_Year_End_Date)
        {
            try
            {
                AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                {
                    business.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        EnterpriseModuleName = ModuleNameEnterprise.Calendar,
                        Audit_Screen_Name = "CompanyCalendars",
                        Audit_Desc = "Calendar" + " " + "[" + Calendar_Description + "]" + "Modified" + "[Calendar End Date]" + Old_yearEndDate + "-->" + Calendar_Year_End_Date,
                        Audit_Field = "Calendar_Year_End_Date",
                        AuditOperationType = OperationType.MODIFY,
                        Audit_New_Vl = Calendar_Year_End_Date,
                        Audit_Old_Vl = Old_yearEndDate,
                        Audit_User_ID = AppEntryPoint.Current.UserId,
                        Audit_User_Name = AppEntryPoint.Current.UserName
                    }, false);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void InsertAuditCalendarName()
        {
            try
            {
                AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                {

                    business.InsertAuditData(new Audit.Transport.Audit_History
                    {
                        EnterpriseModuleName = ModuleNameEnterprise.Calendar,
                        Audit_Screen_Name = "Company Calendars",
                        Audit_Desc = "Record" + " " + "[" + Calendar_Description + "]" + " added to CompanyCalendars",
                        Audit_Field = "Calendar_Description",
                        AuditOperationType = OperationType.ADD,
                        Audit_New_Vl = Calendar_Description,
                        Audit_User_ID = AppEntryPoint.Current.UserId,
                        Audit_User_Name = AppEntryPoint.Current.UserName
                    }, false);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void InsertAuditCalendarPeriod(int Calendar_Period_Number)
        {
            try
            {
                AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                {
                    business.InsertAuditData(new Audit.Transport.Audit_History
                    {
                        EnterpriseModuleName = ModuleNameEnterprise.Calendar,
                        Audit_Screen_Name = "Company Calendars.Period",
                        Audit_Desc = "Record CompanyCalendars.Periods" + "[" + Calendar_Period_Number + "]" + " added to" + "[" + Calendar_Description + "]",
                        Audit_Field = "Calendar_Period_Number",
                        AuditOperationType = OperationType.ADD,
                        Audit_New_Vl = Calendar_Period_Number.ToString(),
                        Audit_User_ID = AppEntryPoint.Current.UserId,
                        Audit_User_Name = AppEntryPoint.Current.UserName
                    }, false);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void InsertAuditCalendarWeek(int Calendar_Week_Number)
        {
            try
            {
                AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                {
                    business.InsertAuditData(new Audit.Transport.Audit_History
                    {
                        EnterpriseModuleName = ModuleNameEnterprise.Calendar,
                        Audit_Screen_Name = "Company Calendars.Week",
                        Audit_Desc = "Record CompanyCalendars.Weeks" + "[" + Calendar_Week_Number + "]" + " added to" + "[" + Calendar_Description + "]",
                        Audit_Field = "Calendar_Week_Number",
                        AuditOperationType = OperationType.ADD,
                        Audit_New_Vl = Calendar_Week_Number.ToString(),
                        Audit_User_ID = AppEntryPoint.Current.UserId,
                        Audit_User_Name = AppEntryPoint.Current.UserName
                    }, false);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion Audit

        private void ucCalenders_Load(object sender, EventArgs e)
        {
            // For externalization
            try
            {
                this.ResolveResources();

                if (!AppGlobals.Current.HasUserAccess("HQ_Calendar_Edit"))
                {
                    btn_UpdateWeek.Visible = false;
                    btnCalendarApply.Enabled = false;
                    btn_UpdatePeriod.Visible = false;
                    btnCalendarWeekApply.Enabled = false;
                    btnCalendarWeekAddNew.Enabled = false;
                    btnCalendarPeriodAddNew.Enabled = false;
                    btnCalendarNew.Enabled = false;
                    DTCalendarPeriodStartDate.Enabled = false;
                    DTCalendarPeriodEndDate.Enabled = false;
                    DTCalendarWeekStartDate.Enabled = false;
                    DTCalendarWeekEndDate.Enabled = false;
                }
                lvCalendarPeriods.Items.Clear();
                lvCalendarWeeks.Items.Clear();
                LoadCalendars();
                if (lb_Calendars.Items.Count > 0)
                {
                    grpPeriods.Enabled = true;
                    grpWeek.Enabled = true;
                }
                else
                {
                    grpPeriods.Enabled = false;
                    grpWeek.Enabled = false;
                    btnCalendarApply.Enabled = false;
                    txt_CalendarName.Enabled = false;
                    DTCalendarYearEnd.Enabled = false;
                    DTCalendarYearStart.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void lb_Calendars_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (_bIsCalendarListLoadComplete && lb_Calendars.SelectedIndex >= 0)
                {
                    _PerviousCalendarID = Convert.ToInt32(lb_Calendars.SelectedValue);
                    LoadListCalendars();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void EnableDisableControls(string Type)
        {
            try
            {
            switch (Type)
            {
                case "NewCalendar":
                    lb_Calendars.Enabled = false;
                    DTCalendarYearStart.Enabled = true;
                    DTCalendarYearEnd.Enabled = true;
                    grpPeriods.Enabled = false;
                    grpWeek.Enabled = false;
                    btnCalendarApply.Enabled = true;
                    txt_CalendarName.Text = string.Empty;
                    txt_CalendarName.Enabled = true;
                    _sActionType = "Cancel";
                    btnCalendarNew.Text = this.GetResourceTextByKey("Key_CancelCaption");//Cancel
                    ClearContents();
                    break;

                case "UpdateCalendar":
                    lb_Calendars.Enabled = true;
                    DTCalendarYearStart.Enabled = true;
                    DTCalendarYearEnd.Enabled = true;
                    grpPeriods.Enabled = true;
                    grpWeek.Enabled = true;
                    txt_CalendarName.Text = string.Empty;
                    _sActionType = "New";
                    btnCalendarNew.Text = this.GetResourceTextByKey("Key_NewCaption");//"New";
                    break;

                case "NewCalendarPeriod":
                    lb_Calendars.Enabled = false;
                    gb_CalendarDetails.Enabled = false;
                    grpWeek.Enabled = false;
                    btn_UpdatePeriod.Enabled = false;
                    btnCalendarPeriodApply.Visible = true;
                    lvCalendarPeriods.Enabled = false;
                    btn_UpdatePeriod.Visible = false;
                    break;

                case "CalendarPeriodUpdate":
                    lb_Calendars.Enabled = true;
                    gb_CalendarDetails.Enabled = true;
                    grpWeek.Enabled = true;
                    btn_UpdatePeriod.Enabled = true;
                    btnCalendarPeriodApply.Visible = false;
                    lvCalendarPeriods.Enabled = true;
                    btn_UpdatePeriod.Visible = true;
                    break;

                case "NewCalendarWeek":
                    lb_Calendars.Enabled = false;
                    gb_CalendarDetails.Enabled = false;
                    grpPeriods.Enabled = false;
                    btnCalendarWeekApply.Visible = true;
                    btn_UpdateWeek.Enabled = false;
                    lvCalendarWeeks.Enabled = true;
                    btn_UpdateWeek.Visible = false;
                    break;

                case "CalendarWeekUpdate":
                    lb_Calendars.Enabled = true;
                    gb_CalendarDetails.Enabled = true;
                    grpPeriods.Enabled = true;
                    btn_UpdateWeek.Enabled = true;
                    btnCalendarWeekApply.Visible = false;
                    lvCalendarWeeks.Enabled = true;
                    btn_UpdateWeek.Visible = true;
                    break;
            }
        }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void ClearContents()
        {
            try
            {
            lvCalendarPeriods.Items.Clear();
            lvCalendarWeeks.Items.Clear();
            txt_CalendarPeriodBasedOn.Text = string.Empty;
            txt_CalendarWeekBasedOn.Text = string.Empty;
            txtCalendarPeriodNumber.Text = string.Empty;
            txtCalendarWeekNumber.Text = string.Empty;
        }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
    }
}
    }
}