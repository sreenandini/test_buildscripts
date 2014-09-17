using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.Utilities;
using BMC.EnterpriseDataAccess;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseBusiness.Business;
using BMC.CoreLib.Win32;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class ucCompanyCalender : UserControl
    {
        #region User Defined Variables
        private CalendarBusiness objCalendarBiz = null;
        List<SubCompanyCalendar> lstSubCmpCalendar = null;
        List<GetSubCompanyCalendarActive> lstcalendarActive = null;

        public int calenderid, subcompanyid, iPrvCalendarID;
        ListViewItem lvItem = null;
        public TreeNode tnNode = new TreeNode();
        string noneText, naText;
        #endregion User Defined Variables

        #region enum
        enum TreeLevel
        {
            Company = 0,
            SubCompany,

        }
        #endregion enum

        #region Constructor
        public ucCompanyCalender()
        {
            InitializeComponent();
            objCalendarBiz = CalendarBusiness.CreateInstance();

            // Set Tags for controls
            SetTagProperty();
        }

        private void SetTagProperty()
        {
            this.btnCompanyCalendarApply.Tag = "Key_Apply";
            this.btnExportCalendar.Tag = "Key_ExportCaption";
            this.label1.Tag = "Key_CalendarHistoryColon";
            this.CalendarId.Tag = "Key_CalendarId";
            this.lblCurrentCalendar.Tag = "Key_CurrentCalendarColon";
            this.lblCntPeriod.Tag = "Key_CurrentPeriodColon";
            this.lblCntWeek.Tag = "Key_CurrentWeekColon";
            this.groupBox3.Tag = "Key_Details";
            this.EndDate.Tag = "Key_EndDate";
            this.CompanyName.Tag = "Key_Name";
            this.lbl.Tag = "Key_NextPeriodEndColon";
            this.label6.Tag = "Key_NextYearEndColon";
            this.StartDate.Tag = "Key_StartDate";
            this.lbl_Status.Tag = "Key_Cal_Status";
            noneText = this.GetResourceTextByKey("Key_None");
            naText = this.GetResourceTextByKey("Key_NA");
        }
        #endregion Constructor

        #region Events

        /// <summary>
        /// Selecting a row fetches Calendar ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvCompaniesCalendars_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnCompanyCalendarApply.Enabled = true;
                if (lvCompaniesCalendars.SelectedItems != null)
                {
                    //foreach (ListViewItem lv in lvCompaniesCalendars.SelectedItems)
                    //{
                    ListViewItem lv = lvCompaniesCalendars.SelectedItems[0];
                    calenderid = Convert.ToInt32(lv.SubItems[0].Text);
                    txtCompanyNextYearEnd.Text = Convert.ToDateTime(lv.SubItems[3].Text).ToShortDateString();
                    GetCurrentCalDetails(calenderid);

                    btnCompanyCalendarApply.Enabled = Convert.ToBoolean(lv.Tag);
                    //}
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Selecting a Company displays its Active Calendar Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvCompanies_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (AppGlobals.Current.HasUserAccess("HQ_Calendar_Edit"))
                {
                    btnExportCalendar.Enabled = (trvCompanies.SelectedNode.Parent == null) ? false : true;
                }
                if (e.Node.Level == (Int32)TreeLevel.SubCompany)
                {
                    List<CompanyCalEntity> getSiteDetailsResults = objCalendarBiz.GetCompanyInfo();
                    TreeNode tnNode = e.Node;
                    lstCompanyCalendarHistory.DataSource = null;
                    txtCompanyCurrentWeek.Text = noneText;      // "[None]";
                    txtCompanyCurrentPeriod.Text = noneText;    //"[None]";
                    txtCompanyNextPeriodEnd.Text = naText;      //"n/a";
                    txtCompanyNextYearEnd.Text = naText;        //"n/a";
                    if (tnNode != null)
                    {
                        LoadCalendarHistory(Convert.ToInt32(tnNode.Tag.ToString()));
                        GetCalForCompany();
                        if (lstcalendarActive.Count == 0) return;
                        iPrvCalendarID = lstcalendarActive[0].Calendar_ID;
                    }
                }
                else
                {
                    ClearEntries();
                }
                this.trvCompanies.HideSelection = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Allocates Calendar to the Selected Sub Company
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCompanyCalendarApply_Click(object sender, EventArgs e)
        {

            try
            {
                if (trvCompanies.SelectedNode != null && trvCompanies.SelectedNode.ToString() != "" && trvCompanies.SelectedNode.Parent != null)
                {
                    List<CompanyCalEntity> GetCompanyDetailsResults = objCalendarBiz.GetCompanyInfo().Distinct<CompanyCalEntity>().ToList();
                    subcompanyid = Convert.ToInt32(trvCompanies.SelectedNode.Tag);
                    if (lvCompaniesCalendars.SelectedItems.Count != 0)
                    {
                        ListViewItem lv = lvCompaniesCalendars.SelectedItems[0];
                        calenderid = Convert.ToInt32(lv.SubItems[0].Text);
                        int record = objCalendarBiz.GetSubCompanyDetails(subcompanyid, calenderid).Count();
                        if (record > 0)
                        {
                            //Error: This calendar has already been allocated to this supplier
                            // this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_CALENDAR_ALLOCATED"), this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.ShowErrorMessageBox(this.GetResourceTextByKey(1, "MSG_CALENDAR_ALLOCATED_SUBCOMPANY"), this.ParentForm.Text);
                            return;
                        }
                        if (this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_CHANGE_ALLOCATION"), this.ParentForm.Text) == DialogResult.Yes)   // Are you sure you want to change the calendar allocation?
                        {
                            int rec = objCalendarBiz.GetSubCompanyByID(subcompanyid).Count();
                            if (rec > 0)
                            {
                                lstSubCmpCalendar = objCalendarBiz.GetSubCompanyByActive(subcompanyid);
                            }
                            else
                            {
                                //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_ERROR_SUBCOMPANY"), this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);   // Error retrieving SubCompany details
                                this.ShowErrorMessageBox(this.GetResourceTextByKey(1, "MSG_ERROR_SUBCOMPANY"), this.ParentForm.Text);
                                return;
                            }
                            foreach (SubCompanyCalendar obj in lstSubCmpCalendar)
                            {
                                obj.Sub_Company_Calendar_Active = false;
                                objCalendarBiz.UpdateSubCompany(subcompanyid);
                                UpdateCalendarAudit(subcompanyid);
                            }
                            bool SubCompany_Calendar_Active = true;
                            objCalendarBiz.InsertNewSubCompanyCalendar(subcompanyid, calenderid, SubCompany_Calendar_Active);
                            InsertCalendarAudit(subcompanyid);
                            if (this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_CALENDAR_SAVE_AND_EXPORT"), this.ParentForm.Text) == DialogResult.Yes)
                            {
                                ExportCompanyCalendars();
                            }
                        }
                        else
                        {
                            return;//
                        }
                        LoadCalendarHistory(subcompanyid);
                    }
                    else
                    {
                        //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_CALENDAR_SELECT"), this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);   // Please select a Calendar
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CALENDAR_SELECT"), this.ParentForm.Text);
                    }
                }
                else
                {
                    //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_SL_SELECT_SUBCOMPANY"), this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);     // Please select a SubCompany
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SL_SELECT_SUBCOMPANY"), this.ParentForm.Text);
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Exports Calendar Details based on selected Sub Company
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportCalendar_Click(object sender, EventArgs e)
        {
            try
            {
                ExportCompanyCalendars();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion Events

        #region User Defined Functions
        public void LoaducCompanyCalendar()
        {
            try
            {
                CreateNodesinTreeView();
                lvCompaniesCalendars.Columns[0].Width = 0;
                //GetCalForCompany();
                ClearEntries();
                if (!AppGlobals.Current.HasUserAccess("HQ_Calendar_Edit"))
                {
                    btnCompanyCalendarApply.Enabled = false;
                    btnExportCalendar.Enabled = false;
                }
                btnExportCalendar.Enabled = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        /// <summary>
        /// Creating Company and Sub Company in Tree View
        /// </summary>
        public void CreateNodesinTreeView()
        {
            int iOldSubComp = 0, iOldCompany = 0;
            try
            {

                TreeNode tnAllCompNode = new TreeNode();
                trvCompanies.Nodes.Clear();
                List<CompanyCalEntity> GetCompanyDetailsResults = objCalendarBiz.GetCompanyInfo().Distinct<CompanyCalEntity>().ToList();
                foreach (CompanyCalEntity getCompanyDetailsResult in GetCompanyDetailsResults)
                {
                    if (iOldCompany != getCompanyDetailsResult.Company_ID)
                    {
                        TreeNode tempnode = trvCompanies.Nodes.Add("CO,#" + getCompanyDetailsResult.Company_ID.ToString(), getCompanyDetailsResult.Company_Name.ToString());
                        trvCompanies.Nodes["CO,#" + getCompanyDetailsResult.Company_ID.ToString()].ExpandAll();
                        iOldCompany = getCompanyDetailsResult.Company_ID;
                    }
                    if (getCompanyDetailsResult.Sub_Company_ID != null && iOldSubComp != getCompanyDetailsResult.Sub_Company_ID)
                    {
                        TreeNode tnNode = trvCompanies.Nodes.Find("CO,#" + getCompanyDetailsResult.Company_ID.ToString(), true)[0];
                        if (tnNode != null)
                        {
                            TreeNode tempnode = tnNode.Nodes.Add("SC,#" + getCompanyDetailsResult.Sub_Company_ID.ToString(), getCompanyDetailsResult.Sub_Company_Name.ToString());
                            tempnode.Tag = getCompanyDetailsResult.Sub_Company_ID.ToString();
                            iOldSubComp = Convert.ToInt32(getCompanyDetailsResult.Sub_Company_ID);
                        }
                    }
                }


            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Loads Calendar Details in the ListView
        /// </summary>
        public void GetCalForCompany()
        {
            try
            {
                lvCompaniesCalendars.Items.Clear();
                List<GetCalendarListEntity> lstCalendarEntity;
                bool? bValidCalendar = false;
                lstCalendarEntity = objCalendarBiz.GetCalendarList().ToList();
                foreach (GetCalendarListEntity CompanyCal in lstCalendarEntity)
                {
                    //objCalendarBiz.CheckCompleteCalendar(CompanyCal.Calendar_ID, ref bValidCalendar);
                    //if (Convert.ToBoolean(bValidCalendar))
                    {
                        lvItem = lvCompaniesCalendars.Items.Add(CompanyCal.Calendar_ID.ToString(), CompanyCal.Calendar_ID);
                        lvItem.SubItems.Add(CompanyCal.Calendar_Description);
                        lvItem.SubItems.Add(Convert.ToDateTime(CompanyCal.Calendar_Year_Start_Date).ToShortDateString());
                        lvItem.SubItems.Add(Convert.ToDateTime(CompanyCal.Calendar_Year_End_Date).ToShortDateString());
                    }

                    if (!Convert.ToBoolean(CompanyCal.IsCompleteCalendar))
                    {
                        lvItem.Tag = false;
                        lvItem.ForeColor = Color.Red;
                    }
                    else
                    {
                        lvItem.Tag = true;

                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }
        }

        /// <summary>
        /// Load Allocated Calendar in the Calendar History With Active Calendar Mentioned
        /// </summary>
        /// <param name="SCompanyId"></param>
        private void LoadCalendarHistory(int SCompanyId)
        {
            try
            {
                txtCurrentCalendar.Text = noneText;      //"[None]";
                lstcalendarActive = null;
                lstcalendarActive = objCalendarBiz.GetCalendarHistory(SCompanyId).ToList();
                btnExportCalendar.Enabled = false;
                if (lstcalendarActive.Count == 0) return;
                lstCompanyCalendarHistory.DataSource = lstcalendarActive;
                lstCompanyCalendarHistory.DisplayMember = "Calendar_History";
                lstCompanyCalendarHistory.ValueMember = "Sub_Company_Calendar_ID";
                if (AppGlobals.Current.HasUserAccess("HQ_Calendar_Edit"))
                {
                    btnExportCalendar.Enabled = (lstCompanyCalendarHistory.Items.Count == 0) ? false : true;
                }
                txtCurrentCalendar.Text = lstcalendarActive[0].Calendar_Description;
                txtCompanyNextPeriodEnd.Text = Convert.ToDateTime(lstcalendarActive[0].Calendar_Year_Start_Date).ToShortDateString();
                txtCompanyNextYearEnd.Text = Convert.ToDateTime(lstcalendarActive[0].Calendar_Year_End_Date).ToShortDateString();
                GetCurrentCalDetails(lstcalendarActive[0].Calendar_ID);



            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        public void ClearEntries()
        {
            try
            {
                lstCompanyCalendarHistory.DataSource = null;
                lstCompanyCalendarHistory.Items.Clear();
                lvCompaniesCalendars.Items.Clear();
                txtCurrentCalendar.Text = noneText;         //"[None]";
                txtCompanyNextPeriodEnd.Text = naText;      //"n/a";
                txtCompanyNextYearEnd.Text = naText;        //"n/a";
                txtCompanyCurrentPeriod.Text = noneText;    //"[None]";
                txtCompanyCurrentWeek.Text = noneText;      //"[None]";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Loads Current Calendar Details into the Readonly Textboxes
        /// </summary>
        /// <param name="iCalenderID"></param>
        private void GetCurrentCalDetails(int iCalenderID)
        {
            try
            {
                List<CurrentCalendarDetails> lstCurrentCalDetails = objCalendarBiz.GetCurrentCalendarDetails(iCalenderID);
                if (lstCurrentCalDetails.Count > 0)
                {
                    txtCompanyCurrentWeek.Text = lstCurrentCalDetails[0].Calendar_Week_Number.Value.ToString();
                    txtCompanyCurrentPeriod.Text = lstCurrentCalDetails[0].Calendar_Period_Number.Value.ToString();
                    txtCompanyNextPeriodEnd.Text = Convert.ToDateTime(lstCurrentCalDetails[0].Calendar_Period_End_Date).ToShortDateString();
                }
                else
                {
                    txtCompanyCurrentWeek.Text = noneText;      //"[None]";
                    txtCompanyCurrentPeriod.Text = noneText;    //"[None]";
                    txtCompanyNextPeriodEnd.Text = naText;      //"n/a";
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion User Defined Functions

        #region Audit
        private void UpdateCalendarAudit(int subcompanyid)
        {
            try
            {
                AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                {
                    CalendarEntity _entity = new CalendarEntity();
                    business.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        EnterpriseModuleName = ModuleNameEnterprise.Calendar,
                        Audit_Screen_Name = "CompanyCalendars.SubCompany",
                        Audit_Desc = "Calendar Assigned to SubCompany" + " " + "[" + subcompanyid + "]" + "Calendar" + iPrvCalendarID + "-->" + calenderid + "",
                        Audit_Field = "Calendar_ID",
                        AuditOperationType = OperationType.MODIFY,
                        Audit_New_Vl = Convert.ToString(calenderid),
                        Audit_Old_Vl = Convert.ToString(iPrvCalendarID),
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

        private void InsertCalendarAudit(int SubcompanyId)
        {
            try
            {
                AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                {
                    business.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        EnterpriseModuleName = ModuleNameEnterprise.Calendar,
                        Audit_Screen_Name = "Calendars.SubCompany",
                        Audit_Desc = "Record" + " " + "[" + SubcompanyId + "]" + " added to Sub_Company_Calendar ",
                        Audit_Field = "Sub_Company_ID",
                        AuditOperationType = OperationType.ADD,
                        Audit_New_Vl = Convert.ToString(SubcompanyId),
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
        private void InsertExportCalendarAudit(int iSubCompanyId)
        {
            try
            {
                AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                {
                    business.InsertAuditData(new Audit.Transport.Audit_History
                    {
                        EnterpriseModuleName = ModuleNameEnterprise.Calendar,
                        Audit_Screen_Name = "ExportCalendars.Sub_Company",
                        Audit_Desc = "Record" + " " + "[" + iSubCompanyId + "]" + " added to calendar.SubCompany ",
                        Audit_Field = "Sub_Company_ID",
                        AuditOperationType = OperationType.ADD,
                        Audit_New_Vl = Convert.ToString(iSubCompanyId),
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

        private void ucCompanyCalender_Load(object sender, EventArgs e)
        {
            // For externalization
            try
            {
                this.ResolveResources();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void ExportCompanyCalendars()
        {
            try
            {
                if (trvCompanies.SelectedNode != null && trvCompanies.SelectedNode.Parent != null)
                {
                    List<CompanyCalEntity> GetCompanyDetailsResults = objCalendarBiz.GetCompanyInfo().Distinct<CompanyCalEntity>().ToList();
                    subcompanyid = Convert.ToInt32(trvCompanies.SelectedNode.Tag);

                    List<ExportCalendar> ExpCal = objCalendarBiz.GetExportCalendar(subcompanyid).Distinct<ExportCalendar>().ToList();
                    if (ExpCal.Count == 0)
                    {
                        //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_SITES_NOTAVAILABLE"), this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);   // No Sites available for the selected SubCompany
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SITES_NOTAVAILABLE"), this.ParentForm.Text);
                    }
                    else
                    {
                        foreach (ExportCalendar obj in ExpCal)
                        {
                            objCalendarBiz.InsertExportHistory(obj.Site_ID.ToString(), "S-CALENDAR", obj.Site_ID);
                        }
                        InsertExportCalendarAudit(subcompanyid);//Audit
                        //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_CALENDEREXPORT"), this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);    // Calendar export complete
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CALENDEREXPORT"), this.ParentForm.Text);
                    }
                }
                else
                {
                    //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_CALENDAR_NOTALLOCATED"), this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);   // A calendar has not yet been allocated to this company.
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CALENDAR_NOTALLOCATED"), this.ParentForm.Text);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    }
}



