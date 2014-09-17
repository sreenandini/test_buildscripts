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
using BMC.EnterpriseClient.Helpers;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;
using BMC.Common.LogManagement;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.Utilities;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmDropScheduleUtility : Form
    {
        //private bool Sunday = false, Monday = false, Tuesday = false, Wednesday = false, Thursday = false, Friday = false, Saturday = false;
        //private int Dateofmonth = 0, Weekofmonth = 0, Dayofweek = 0, Monthduration = 0;
        //private DateTime StartDate, EndDate, FirstDayofMonth, NextDate, tmpEndDate;
        //private string tmpWeekdays=null;
        //private int Occurence=0, OccurrenceType=1;

        private DropScheduleBiz objDropScheduleBiz = DropScheduleBiz.CreateInstance();
        //public DropScheduleEntity ScheduleEntity = new DropScheduleEntity();
        public int EditScheduleId = -1;
        public RegionNameModel RegionEntity = new RegionNameModel();
        public SiteDetailsModel SiteEntity = new SiteDetailsModel();
        public SiteRegion SiteRegionEntity = new SiteRegion();
        BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;


        public bool IsUpdate { get; set; }
        public bool ShowAutoSchedule { get; set; }

        public frmDropScheduleUtility()
        {
            InitializeComponent();
            DropScheduleTab_SelectedIndexChanged(null, null);
            SetPropertyTag();
        }

        private void SetPropertyTag()
        {
            try
            {
                this.btnPrintschedule.Tag = "Key_PrintSchedule";
                this.btnSave.Tag = "Key_SaveCaption";
                this.btnSendDropAlert.Tag = "Key_SendDropAlert";
                this.tbAutomaticschedule.Tag = "Key_AutomaticSchedule";
                this.label1.Tag = "Key_AutomaticScheduler";
                this.btnClose.Tag = "Key_CloseCaption";
                this.rdbDaily.Tag = "Key_Daily";                
                this.lblSchedule.Tag = "Key_GenerateScheduleColon";
                this.lblHourMinute.Tag = "Key_HHMM";
                this.chkIsActive.Tag = "Key_IsActive";
                this.tbManualschedule.Tag = "Key_ManualSchedule";
                this.rdbMonthly.Tag = "Key_Monthly";
                this.lblRegion.Tag = "Key_RegionColon";
                this.lblSite.Tag = "Key_SiteColon";
                this.lblStackerlevelauto.Tag = "Key_StackerLevelColon";
                this.lblStackerlevelmanual.Tag = "Key_StackerLevelColon";
                this.rdbWeekly.Tag = "Key_Weekly";
                this.Tag = "Key_DropScheduleUtility";
                this.lblPercAutomactic.Tag = "Key_PercSymbol";
                this.lblPercManual.Tag = "Key_PercSymbol";

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in Setting Property" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        private void rdWeekly_CheckedChanged(object sender, EventArgs e)
        {
            ucWS.Visible = true;
            ucMS.Visible = false;
        }


        private void rdMonthly_CheckedChanged(object sender, EventArgs e)
        {
            ucWS.Visible = false;
            ucMS.Visible = true;
        }

        private void rdDaily_CheckedChanged(object sender, EventArgs e)
        {
            ucWS.Visible = false;
            ucMS.Visible = false;
        }

        private void DropScheduleTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSendDropAlert.Visible = ((tabDropSchedule.SelectedIndex != 0) || (tabDropSchedule.SelectedTab.Text == "Automatic Schedule" && !tabDropSchedule.SelectedTab.Visible));
            // btnPrintschedule.Visible = (tabDropSchedule.SelectedIndex != 0) || (tabDropSchedule.SelectedTab.Text == "Automatic Schedule" && !tabDropSchedule.SelectedTab.Visible);
            btnPrintschedule.Visible = false;
            btnSave.Visible = (tabDropSchedule.SelectedIndex == 0);

        }

        private void frmDropScheduleUtility_Load(object sender, EventArgs e)
        {
            if (ShowAutoSchedule)
                tabDropSchedule.TabPages.Remove(tbManualschedule);
            else
                tabDropSchedule.TabPages.Remove(tbAutomaticschedule);

            if (tabDropSchedule.SelectedTab == tbManualschedule)
            {
                LoadManualDrop();
            }
            else if (tabDropSchedule.SelectedTab == tbAutomaticschedule)
            {
                LoadAutomaticDrop();
            }
            objDatawatcher = new Helpers.Datawatcher(this);

            this.ResolveResources();
        }

        private void LoadAutomaticDrop()
        {
            try
            {
                btnSendDropAlert.Visible = false;
                btnSave.Visible = true;
                rdDaily_CheckedChanged(null, null);
                ucMS.MonthDuration = 1;

                List<DropScheduleEntity> lstobjDropSchedule;

                if (IsUpdate)  //To Update from Grid in StackerDetails Form through Schedule ID 
                {
                    lstobjDropSchedule = objDropScheduleBiz.GetDropSchedule(EditScheduleId, null);
                }
                else
                {
                    lstobjDropSchedule = objDropScheduleBiz.GetDropSchedule(null);
                }

                if (lstobjDropSchedule.Count == 0)
                {
                    ucRR.StartDate = DateTime.Now;
                    return;
                }

                DropScheduleEntity objDropSchedule = lstobjDropSchedule[0]; // Reads the First record.                

                if (objDropSchedule.ScheduleTime != null)
                    dtpScheduletime.Value = Convert.ToDateTime(objDropSchedule.ScheduleTime);
                nudASStackerLevel.Value = Convert.ToInt32(objDropSchedule.StackerLevelPercentage);
                chkIsActive.Checked = objDropSchedule.IsActive;
                if (objDropSchedule.ScheduleOccurance == ScheduleOccurances.Daily)
                {
                    rdbDaily.Checked = true;
                }
                else if (objDropSchedule.ScheduleOccurance == ScheduleOccurances.Weekly)
                {
                    rdbWeekly.Checked = true;
                    ucWS.SelectedWeekDays = objDropSchedule.WeekDays;
                }
                else if (objDropSchedule.ScheduleOccurance == ScheduleOccurances.Monthly)
                {
                    rdbMonthly.Checked = true;
                    ucMS.DateOfMonth = objDropSchedule.DayOfMonth;
                    ucMS.MonthDuration = objDropSchedule.MonthDuration;
                }
                ucRR.StartDate = objDropSchedule.StartDate;
                ucRR.EndOption = objDropSchedule.EndOption;
                if (ucRR.EndOption == EndOptions.EndByDate)
                {
                    ucRR.EndDate = objDropSchedule.EndDate;
                }
                else if (ucRR.EndOption == EndOptions.EndAfterOccurance)
                {
                    ucRR.Occurrence = objDropSchedule.TotalOccurances;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in LoadAutomaticDrop Method" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        private void LoadManualDrop()
        {
            try
            {
                btnSendDropAlert.Visible = true;
                btnSave.Visible = false;

                List<RegionNameModel> listReg = objDropScheduleBiz.GetRegion();
                listReg.Insert(0, new RegionNameModel { Sub_Company_Region_Name = this.GetResourceTextByKey("Key_All") });
                cmbRegion.DataSource = listReg;
                cmbRegion.DisplayMember = "Sub_Company_Region_Name";
                cmbRegion.ValueMember = "Sub_Company_Region_ID";
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in LoadManualDrop Method" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        private void btnSenddropalert_Click(object sender, EventArgs e)
        {

            if (this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_DROPSCHEDULE_DROPALERTY"), this.Text) == DialogResult.Yes)
            {
                string SuccessBarPositionList = string.Empty;
                string FailedBarPositionList = string.Empty;

                try
                {


                    if (cmbSite.SelectedIndex == 0)
                    {
                        SiteEntity.Site_ID = 0;
                    }
                    else
                    {
                        SiteEntity.Site_ID = SiteRegionEntity.SiteId;
                    }

                    if (cmbRegion.SelectedIndex == 0)
                    {
                        RegionEntity.Sub_Company_Region_ID = 0;
                    }
                    else
                    {
                        RegionEntity.Sub_Company_Region_ID = SiteRegionEntity.RegionID;
                    }

                    SiteRegionEntity.ManualStackLevelPercent = (byte)nudMSStackerLevel.Value;

                    DropScheduleEntity Sch = null;
                    Sch = new DropScheduleEntity();
                    Sch.ScheduleName = "Manual_" + DateTime.Now.ToString("yyyyMMdd hhmmss");
                    Sch.ScheduleTime = DateTime.Now;
                    Sch.StackerLevelPercentage = SiteRegionEntity.ManualStackLevelPercent;
                    Sch.DropAlertType = DropAlertTypes.Manual;
                    Sch.StartDate = DateTime.Now;
                    Sch.EndDate = DateTime.Now;
                    Sch.ScheduleOccurance = ScheduleOccurances.Daily;
                    Sch.TotalOccurances = 1;
                    Sch.WeekDays = 0;
                    Sch.EndOption = EndOptions.NoEndDate;
                    Sch.MonthDuration = 0;
                    Sch.DayOfMonth = 0;
                    Sch.NextOcc = DateTime.Now;
                    Sch.IsActive = true;
                    Sch.DropAlertType = DropAlertTypes.Manual;
                    Sch.SiteId = SiteEntity.Site_ID;
                    Sch.RegionId = RegionEntity.Sub_Company_Region_ID;
                    objDropScheduleBiz.InsertUpdateDropSchedule(Sch);

                    //Insert one record to DropSchedule Table as Mannual Drop
                    //Get the Schedule Id
                    objDropScheduleBiz.InsertUpdateDropSchedule(Sch);

                    List<ManualDropScheduleEntity> lstManualDropAlert;

                    lstManualDropAlert = objDropScheduleBiz.GetManualDropAlertDetails(RegionEntity.Sub_Company_Region_ID, SiteEntity.Site_ID, SiteRegionEntity.ManualStackLevelPercent, AppEntryPoint.Current.StaffId);

                    foreach (var dropAlert in lstManualDropAlert)
                    {
                        try
                        {
                            SuccessBarPositionList = "," + dropAlert.BAR_POSITION_NAME.ToString() + SuccessBarPositionList;
                            XElement item = dropAlert.ToString(Sch.ScheduleId, "Manual", DateTime.Now);
                            LogManager.WriteLog("Manual Drop Alert Data " + item, LogManager.enumLogLevel.Debug);
                            String siteCode = item.Element("SiteId").Value;
                            objDropScheduleBiz.STM_Export_History("drop", 1, siteCode, item);
                        }
                        catch (Exception ex)
                        {
                            FailedBarPositionList = "," + dropAlert.BAR_POSITION_NAME.ToString() + FailedBarPositionList;
                            LogManager.WriteLog("Exception Message " + ex.Message, LogManager.enumLogLevel.Error);
                            LogManager.WriteLog("Exception StackTrace " + ex.StackTrace, LogManager.enumLogLevel.Error);
                            objDropScheduleBiz.InsertUpdateDropScheduleHistory(null, Sch, DateTime.Now, -1);
                        }
                    }

                    if (!String.IsNullOrEmpty(SuccessBarPositionList))
                    {
                        SuccessBarPositionList = SuccessBarPositionList.Substring(1, SuccessBarPositionList.Length - 1);
                    }

                    if (!String.IsNullOrEmpty(FailedBarPositionList))
                    {
                        FailedBarPositionList = FailedBarPositionList.Substring(1, FailedBarPositionList.Length - 1);
                    }

                    string Msg = string.Empty;

                    if (!String.IsNullOrEmpty(SuccessBarPositionList))
                    {
                        Msg = string.Format(this.GetResourceTextByKey(1,"MSG_DROPSCHEDULE_SUCCESS"), SuccessBarPositionList);
                    }

                    if (!String.IsNullOrEmpty(FailedBarPositionList))
                    {
                        Msg = Msg + string.Format(this.GetResourceTextByKey(1, "MSG_DROPSCHEDULE_FAILED"), FailedBarPositionList);
                    }

                    if (String.IsNullOrEmpty(Msg))
                    {
                        Msg = this.GetResourceTextByKey(1, "MSG_DROPSCHEDULE_NO_POS");
                    }

                    this.ShowInfoMessageBox(Msg, this.Text);

                }
                catch (Exception ex)
                {

                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_DROPSCHEDULE_ERROR"), this.Text);
                    LogManager.WriteLog("Error in Senddropalert Method" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);
                }

                try
                {
                    AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                    {
                        business.InsertAuditData(new Audit.Transport.Audit_History
                        {
                            EnterpriseModuleName = ModuleNameEnterprise.DropAlert,
                            Audit_Screen_Name = "DropAlert|ManualDropAlert",
                            Audit_Desc = "Manual Drop Alert Performed.",
                            AuditOperationType = OperationType.ADD,
                            Audit_User_ID = AppEntryPoint.Current.UserId,
                            Audit_User_Name = AppEntryPoint.Current.UserName
                        }, false);

                        business.InsertAuditData(new Audit.Transport.Audit_History
                        {
                            EnterpriseModuleName = ModuleNameEnterprise.DropAlert,
                            Audit_Screen_Name = "DropAlert|ManualDropAlert",
                            Audit_Desc = "SuccessBarPositionList:" + SuccessBarPositionList,
                            AuditOperationType = OperationType.ADD,
                            Audit_User_ID = AppEntryPoint.Current.UserId,
                            Audit_User_Name = AppEntryPoint.Current.UserName
                        }, false);

                        business.InsertAuditData(new Audit.Transport.Audit_History
                        {
                            EnterpriseModuleName = ModuleNameEnterprise.DropAlert,
                            Audit_Screen_Name = "DropAlert|ManualDropAlert",
                            Audit_Desc = "FailedBarPositionList:" + FailedBarPositionList,
                            AuditOperationType = OperationType.ADD,
                            Audit_User_ID = AppEntryPoint.Current.UserId,
                            Audit_User_Name = AppEntryPoint.Current.UserName
                        }, false);
                    }
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("Error While Adding Audit Log for Manual Drop Alert: Error Message:" + ex.Message, LogManager.enumLogLevel.Error);
                }
            }
        }




        //private bool CheckValidDayofMonth(DateTime StartDate, int DayofMonth, int MonthDuration)
        //{
        //    int LastDayofMonth;
        //    DateTime FirstDayofMonth = new DateTime(StartDate.Year, StartDate.Month, 1);         
        //    LastDayofMonth = LastDayOfScheduleMonth(FirstDayofMonth).Day;

        //    if (DayofMonth > LastDayofMonth)
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        //private DateTime LastDayOfScheduleMonth(DateTime ScheduleMonth)
        //{
        //    DateTime firstDayOfTheMonth = new DateTime(ScheduleMonth.Year, ScheduleMonth.Month, 1);
        //    return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
        //}

        //private DateTime GetEndDateForDaily(DateTime StartDate, int NoOfOccurances)
        //{
        //    return StartDate.AddDays(NoOfOccurances-1);
        //}

        //private DateTime GetEndDateForWeekly(DateTime StartDate, int SelectedWeekDays, int NoOfOccurances)
        //{
        //    DateTime WeeklyEndDate = StartDate;

        //    if (WeeklyEndDate < DateTime.Now)
        //        NoOfOccurances = NoOfOccurances + 1;

        //    if (SelectedWeekDays <= 0)
        //        return WeeklyEndDate;
        //    if (NoOfOccurances <= 0)
        //        return WeeklyEndDate;

        //    int iDayOfWeek = 0; ;
        //    int iCurrentWeekDay = 0;
        //    int ExistsResult = 0;


        //    while (NoOfOccurances > 0)
        //    {
        //        iDayOfWeek = Convert.ToInt32(WeeklyEndDate.DayOfWeek);
        //        iCurrentWeekDay = Convert.ToInt32(Math.Pow(2, iDayOfWeek));
        //        ExistsResult = (SelectedWeekDays & iCurrentWeekDay);

        //        if (ExistsResult == iCurrentWeekDay)
        //        {
        //            NoOfOccurances = NoOfOccurances - 1;
        //        }

        //        if (NoOfOccurances != 0)
        //        {
        //            WeeklyEndDate = WeeklyEndDate.AddDays(1);
        //        }
        //    }
        //    return WeeklyEndDate;
        //}


        //private DateTime GetEndDateForMonthly(DateTime StartDate, int DayOfMonth, int MonthDuration, int NoOfOccurances)
        //{

        //    int MaxDaysInCurrentMonth = 0;
        //    int OrgDayOfMonth = DayOfMonth;
        //    int DaysDiff = 0;
        //    int intCurrentDay = 0;
        //    DateTime MonthlyEndDate = StartDate;

        //    MaxDaysInCurrentMonth = DateTime.DaysInMonth(MonthlyEndDate.Year, MonthlyEndDate.Month);

        //    DateTime dt = new DateTime(MonthlyEndDate.Year, MonthlyEndDate.Month, Math.Min(DayOfMonth,MaxDaysInCurrentMonth), MonthlyEndDate.Hour, MonthlyEndDate.Minute, 0);
        //    MonthlyEndDate = dt;

        //    if (MonthlyEndDate < DateTime.Now)
        //        NoOfOccurances = NoOfOccurances + 1;



        //    if ((NoOfOccurances < 1) || (DayOfMonth < 1) || (MonthDuration < 1))
        //        return MonthlyEndDate;

        //    while (NoOfOccurances > 1)
        //    {
        //        MaxDaysInCurrentMonth = DateTime.DaysInMonth(MonthlyEndDate.Year, MonthlyEndDate.Month);

        //        intCurrentDay = MonthlyEndDate.Day;
        //        if ((intCurrentDay == DayOfMonth) || (intCurrentDay==MaxDaysInCurrentMonth))
        //        {
        //            NoOfOccurances = NoOfOccurances - 1;
        //            MonthlyEndDate = MonthlyEndDate.AddMonths(MonthDuration);
        //        }
        //        else
        //        {
        //            MonthlyEndDate = MonthlyEndDate.AddDays(1);
        //        }
        //        if (NoOfOccurances == 1)
        //        {
        //            if (OrgDayOfMonth > MonthlyEndDate.Day)
        //            {
        //                MaxDaysInCurrentMonth = DateTime.DaysInMonth(MonthlyEndDate.Year, MonthlyEndDate.Month);
        //                DaysDiff = MaxDaysInCurrentMonth - MonthlyEndDate.Day;
        //                MonthlyEndDate = MonthlyEndDate.AddDays(DaysDiff);
        //            }
        //        }
        //    }
        //    return MonthlyEndDate;
        //}


        private DateTime GetEndDateForDaily(DateTime StartDate, int NoOfOccurances)
        {
            return StartDate.AddDays(NoOfOccurances - 1);
        }

        private DateTime GetEndDateForWeekly(DateTime StartDate, int SelectedWeekDays, int NoOfOccurances)
        {
            DateTime WeeklyEndDate = StartDate;

            if (SelectedWeekDays <= 0)
                return WeeklyEndDate;

            if (NoOfOccurances <= 0)
                return WeeklyEndDate;

            int iDayOfWeek = 0; ;
            int iCurrentWeekDay = 0;
            int ExistsResult = 0;


            while (NoOfOccurances > 0)
            {
                iDayOfWeek = Convert.ToInt32(WeeklyEndDate.DayOfWeek);
                iCurrentWeekDay = Convert.ToInt32(Math.Pow(2, iDayOfWeek));
                ExistsResult = (SelectedWeekDays & iCurrentWeekDay);

                if (ExistsResult == iCurrentWeekDay)
                {
                    NoOfOccurances = NoOfOccurances - 1;
                }

                if (NoOfOccurances != 0)
                {
                    WeeklyEndDate = WeeklyEndDate.AddDays(1);
                }
            }
            return WeeklyEndDate;
        }


        private DateTime GetEndDateForMonthly(DateTime StartDate, int DayOfMonth, int MonthDuration, int NoOfOccurances)
        {

            int MaxDaysInCurrentMonth = 0;
            int OrgDayOfMonth = DayOfMonth;
            int DaysDiff = 0;
            int intCurrentDay = 0;
            DateTime MonthlyEndDate = StartDate;
            bool MaxDaySelected = false;

            if ((NoOfOccurances < 1) || (DayOfMonth < 1) || (MonthDuration < 1))
                return MonthlyEndDate;

            while (NoOfOccurances > 0)
            {
                MaxDaysInCurrentMonth = DateTime.DaysInMonth(MonthlyEndDate.Year, MonthlyEndDate.Month);
                MaxDaySelected = (OrgDayOfMonth >= MaxDaysInCurrentMonth);

                intCurrentDay = MonthlyEndDate.Day;
                if ((intCurrentDay == DayOfMonth) || (MaxDaySelected && (intCurrentDay == MaxDaysInCurrentMonth)))
                {
                    NoOfOccurances = NoOfOccurances - 1;
                    if (NoOfOccurances <= 0)
                        break;
                    MonthlyEndDate = MonthlyEndDate.AddMonths(MonthDuration);

                }
                else
                {
                    MonthlyEndDate = MonthlyEndDate.AddDays(1);
                }
            }

            if (OrgDayOfMonth > MonthlyEndDate.Day)
            {
                MaxDaysInCurrentMonth = DateTime.DaysInMonth(MonthlyEndDate.Year, MonthlyEndDate.Month);
                DaysDiff = MaxDaysInCurrentMonth - MonthlyEndDate.Day;
                MonthlyEndDate = MonthlyEndDate.AddDays(DaysDiff);
            }
            else if (OrgDayOfMonth < MonthlyEndDate.Day)
            {
                DaysDiff = OrgDayOfMonth - MonthlyEndDate.Day;
                MonthlyEndDate = MonthlyEndDate.AddDays(DaysDiff);
            }

            return MonthlyEndDate;
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            string AutoSchedule = string.Empty;
            //Common Validation
            if ((ucRR.EndAfterOccurrence) && (ucRR.Occurrence <= 0))
            {
                this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_MIN_OCCURRENCE_SHUDBE_ONE"), this.Text);
                return;
            }

            //Common Validation
            //if (!IsUpdate)
            //{           
            //    DateTime dtScheduleDateTime = new DateTime(ucRR.StartDate.Year, ucRR.StartDate.Month, ucRR.StartDate.Day, dtpScheduletime.Value.Hour, dtpScheduletime.Value.Minute, 0);
            //    DateTime dtCurrentDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
            //    TimeSpan Span1 = dtScheduleDateTime.Subtract(dtCurrentDateTime);
            //    double Minutes = Convert.ToDouble(Span1.TotalMinutes);
            //    if (Minutes <= 5)
            //    {
            //        MessageBox.Show("Start Date with Schedule Time must be greator than 5 minutes of Current Date and Time", "START SCHEDULE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return; 
            //    }
            //}
            if (((ucRR.EndAfterOccurrence || ucRR.NoEndDate) && ucRR.StartDate.Date < DateTime.Now.Date)
			 || (!ucRR.NoEndDate && !ucRR.EndAfterOccurrence &&
			(ucRR.StartDate.Date < DateTime.Now.Date || ucRR.EndDate.Date < DateTime.Now.Date)))
            {

                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_DROPSCHEDULE_FAILURE"), this.Text);
                return;
            }
            if (!ucRR.NoEndDate && !ucRR.EndAfterOccurrence && ucRR.StartDate > ucRR.EndDate)
            {
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_DROPSCHEDULE_VALILDDATE"), this.Text);                
                return;
            }
            //Weekly Validation
            if ((rdbWeekly.Checked) && (ucWS.SelectedWeekDays < 1))
            {
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_DROPSCHEDULE_SELECTDAY"), this.Text);                
                return;
            }

            if ((rdbMonthly.Checked) && (ucMS.DateOfMonth < 1))
            {
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_DROPSCHEDULE_SELECTDOM"), this.Text);                
                return;
            }

            if ((rdbMonthly.Checked) && ((ucMS.MonthDuration < 1) || (ucMS.MonthDuration > 12)))
            {
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_DROPSCHEDULE_VALID_MONTH"), this.Text);                
                return;
            }

            if (this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_DROPSCHEDULE_SAVE"), this.Text) == DialogResult.No)
            {
                return;
            }

            try
            {
                DropScheduleEntity Sch = new DropScheduleEntity();
                Sch.ScheduleId = 0;
                if (IsUpdate)
                {
                    Sch.ScheduleId = EditScheduleId;
                }

                Sch.DropAlertType = DropAlertTypes.Automatic;
                Sch.ScheduleName = "CommonSchedule";
                Sch.NextOcc = null;
                Sch.StackerLevelPercentage = Convert.ToByte(nudASStackerLevel.Value);
                Sch.ScheduleTime = dtpScheduletime.Value;
                if (rdbDaily.Checked)
                {
                    Sch.ScheduleOccurance = ScheduleOccurances.Daily;
                }
                else if (rdbWeekly.Checked)
                {
                    Sch.ScheduleOccurance = ScheduleOccurances.Weekly;
                    Sch.WeekDays = ucWS.SelectedWeekDays;
                }
                else if (rdbMonthly.Checked)
                {
                    Sch.ScheduleOccurance = ScheduleOccurances.Monthly;
                    Sch.DayOfMonth = Convert.ToInt32(ucMS.DateOfMonth);
                    Sch.MonthDuration = Convert.ToInt32(ucMS.MonthDuration);
                }
                Sch.StartDate = ucRR.StartDate;
                Sch.EndOption = ucRR.EndOption;
                Sch.TotalOccurances = ucRR.Occurrence;
                if (ucRR.NoEndDate)
                    Sch.EndDate = DateTime.MaxValue;
                else
                    Sch.EndDate = ucRR.EndDate;


                DateTime dtStartDate = Sch.StartDate.AddHours(((DateTime)Sch.ScheduleTime).Hour);
                dtStartDate = dtStartDate.AddMinutes(((DateTime)Sch.ScheduleTime).Minute);

                if (Sch.EndOption == EndOptions.EndAfterOccurance)
                {
                    if (Sch.ScheduleOccurance == ScheduleOccurances.Daily)
                        Sch.EndDate = GetEndDateForDaily(Sch.StartDate, Sch.TotalOccurances);
                    else if (Sch.ScheduleOccurance == ScheduleOccurances.Weekly)
                        Sch.EndDate = GetEndDateForWeekly(dtStartDate, Sch.WeekDays, Sch.TotalOccurances);
                    else if (Sch.ScheduleOccurance == ScheduleOccurances.Monthly)
                        Sch.EndDate = GetEndDateForMonthly(dtStartDate, Sch.DayOfMonth, Sch.MonthDuration, Sch.TotalOccurances);
                }
                else if (Sch.EndOption == EndOptions.EndByDate)
                {
                    Sch.EndDate = ucRR.EndDate;
                }
                else if (Sch.EndOption == EndOptions.NoEndDate)
                {
                    Sch.EndDate = DateTime.MaxValue;
                }
                Sch.IsActive = chkIsActive.Checked;
                objDropScheduleBiz.InsertUpdateDropSchedule(Sch);

                StringBuilder build = new StringBuilder(string.Empty);
                build.Append(String.Format("{0} : {1};\r\n", "ScheduleID", Sch.ScheduleId));
                build.Append(String.Format("{0} : {1};\r\n", "ScheduleName", Sch.ScheduleName));
                build.Append(String.Format("{0} : {1};\r\n", "ScheduleTime", Sch.ScheduleTime));
                build.Append(String.Format("{0} : {1};\r\n", "StackerLevel", Sch.StackerLevelPercentage));
                build.Append(String.Format("{0} : {1};\r\n", "ScheduleType", Sch.ScheduleOccurance));
                build.Append(String.Format("{0} : {1};\r\n", "StartDate", Sch.StartDate));
                build.Append(String.Format("{0} : {1};\r\n", "EndDate", Sch.EndDate));
                build.Append(String.Format("{0} : {1};\r\n", "OccurrenceType", Sch.EndOption));
                build.Append(String.Format("{0} : {1};\r\n", "TotalOcurrence", Sch.TotalOccurances));
                build.Append(String.Format("{0} : {1};\r\n", "WeekDays", Sch.WeekDays));
                build.Append(String.Format("{0} : {1};\r\n", "MonthDuration", Sch.MonthDuration));
                build.Append(String.Format("{0} : {1};\r\n", "DateofMonth", Sch.DayOfMonth));
                build.Append(String.Format("{0} : {1};\r\n", "IsActive", Sch.IsActive));
                AutoSchedule = build.ToString();
                this.Close();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in Save Method" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);
            }

            try
            {
                if (IsUpdate)
                {
                    AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                    {
                        business.InsertAuditData(new Audit.Transport.Audit_History
                        {
                            EnterpriseModuleName = ModuleNameEnterprise.DropAlert,
                            Audit_Screen_Name = "DropAlert|AutoDropAlert",
                            Audit_Desc = "Auto Drop Schedule Updated:" + AutoSchedule,
                            AuditOperationType = OperationType.MODIFY,
                            Audit_User_ID = AppEntryPoint.Current.UserId,
                            Audit_User_Name = AppEntryPoint.Current.UserName
                        }, false);
                    }
                }
                else
                {
                    AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                    {
                        business.InsertAuditData(new Audit.Transport.Audit_History
                        {
                            EnterpriseModuleName = ModuleNameEnterprise.DropAlert,
                            Audit_Screen_Name = "DropAlert|AutoDropAlert",
                            Audit_Desc = "Auto Drop Schedule Added:" + AutoSchedule,
                            AuditOperationType = OperationType.ADD,
                            Audit_User_ID = AppEntryPoint.Current.UserId,
                            Audit_User_Name = AppEntryPoint.Current.UserName
                        }, false);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error While Adding Audit Log for Auto Drop Schedule Alert Insert or Update: Error Message:" + ex.Message, LogManager.enumLogLevel.Error);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            String tmpRegion = String.Empty;
            tmpRegion = cmbRegion.SelectedItem.ToString();
            SiteRegionEntity.RegionID = ((RegionNameModel)(cmbRegion.SelectedItem)).Sub_Company_Region_ID;

            List<SiteDetailsModel> listSite = objDropScheduleBiz.GetSite(SiteRegionEntity.RegionID);
            if (SiteRegionEntity.RegionID != 0 && listSite.Count == 0)
            {
                listSite.Insert(0, new SiteDetailsModel { Site_Name = this.GetResourceTextByKey("Key_NoneText") });
            }
            else
            {
                listSite.Insert(0, new SiteDetailsModel { Site_Name = this.GetResourceTextByKey("Key_All") });
            }
            cmbSite.DataSource = listSite;
            cmbSite.DisplayMember = "Site_Name";
            cmbSite.ValueMember = "Site_ID";
        }
        private void cmbSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            SiteRegionEntity.SiteId = ((SiteDetailsModel)cmbSite.SelectedItem).Site_ID;
        }

        private void nudMSStackerLevel_ValueChanged(object sender, EventArgs e)
        {
            SiteRegionEntity.ManualStackLevelPercent = (byte)nudMSStackerLevel.Value;
        }

    }
}
