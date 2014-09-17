using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseBusiness.Business;
using BMC.CoreLib.Win32;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.Utilities;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class ucOperatorCalendar : UserControl
    {
        #region Member Vaiables
        public int Operatorid = 0, Calendar_Id = 0, iPrvCalendarID = 0;
        ListViewItem lvItem = null;
        List<Operator_Calendar> lstoperator = null;
        List<GetOperatorCalendarActive> lstOperatorActive = null;
        private CalendarBusiness objCalendarBiz = null;
        string noneText, naText;
        #endregion Member Vaiables

        #region Events
        /// <summary>
        /// Allocates calendar to selected Operator 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSuppliersCalendarApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstSuppliers.SelectedItems.Count != 0)
                {
                    Operatorid = Convert.ToInt32(lstSuppliers.SelectedValue.ToString());
                    if (lvSuppliersCalendar.SelectedItems.Count != 0)
                    {
                        foreach (ListViewItem lv in lvCompaniesCalendars.SelectedItems)
                        {
                            Calendar_Id = Convert.ToInt32(lv.SubItems[0].Text);
                        }
                        int record = objCalendarBiz.GetOperatorCalendar(Operatorid, Calendar_Id).Count();
                        if (record > 0)
                        {
                            //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_CALENDAR_ALLOCATED"),,this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);   // "This calendar has already been allocated to this supplier"
                            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CALENDAR_ALLOCATED"),this.ParentForm.Text);
                            return;
                        }
                        if (Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1, "MSG_CHANGE_ALLOCATION"),this.ParentForm.Text) == DialogResult.Yes)        // "Are you sure you want to change the calendar allocation?"
                        {
                            int rec = objCalendarBiz.GetOperatorCal(Operatorid).Count();
                            if (rec > 0)
                                lstoperator = objCalendarBiz.GetOperatorByActive(Operatorid);
                            //Audit

                            foreach (var obj in lstoperator)
                            {
                                obj.Operator_Calendar_Active = false;
                                objCalendarBiz.UpdateOpertor(Operatorid);
                                UpdateOperatorCalendarAudit(Operatorid);
                            }
                            bool Operator_Calendar_Active = true;
                            objCalendarBiz.InsertNewOperatorCalendar(Operatorid, Calendar_Id, Operator_Calendar_Active);
                            InsertOperatorCalendarAudit(Operatorid);
                            ExportCalendar.Enabled = true;
                            if (this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_CALENDAR_SAVE_AND_EXPORT"),this.ParentForm.Text) == DialogResult.Yes)
                            {
                                ExportOperatorCalendars();
                            }
                        }
                    }
                    else
                    {
                        //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_CALENDAR_SELECT"),this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);        // "Please select a Calendar"
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CALENDAR_SELECT"), this.ParentForm.Text);
                    }
                    LoadOperatorHistory(Operatorid);
                }
                else
                {
                   // this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_SUPPLIER_SELECT"),this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);           // "Please select a Supplier"
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SUPPLIER_SELECT"), this.ParentForm.Text);   
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Exports the Calendar Details to Exchange
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportCalendar_Click(object sender, EventArgs e)
        {
            try
            {
                ExportOperatorCalendars();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Load the Active Calendar Details in the 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetCalForOperator();
                lstSupplierCalendarHistory.DataSource = null;
                txtSuppliersCurrentWeek.Text = noneText;
                txtSuppliersCurrentPeriod.Text = noneText;
                txtSuppliersNextPeriodEnd.Text = naText;
                txtSuppliersNextYearEnd.Text = naText;
                Operatorid = Convert.ToInt32(lstSuppliers.SelectedValue.ToString());
                LoadOperatorHistory(Operatorid);
                if (lstOperatorActive.Count == 0) return;
                iPrvCalendarID = lstOperatorActive[0].Calendar_ID;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Load details corresponding to the selected operator
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvSuppliersCalendar_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lvSuppliersCalendar.SelectedItems != null)
                {
                    //foreach (ListViewItem lv in lvSuppliersCalendar.SelectedItems)
                    {
                        ListViewItem lv = lvSuppliersCalendar.SelectedItems[0];
                        Calendar_Id = Convert.ToInt32(lv.SubItems[0].Text);
                        txtSuppliersNextYearEnd.Text = (lv.SubItems[3].Text);
                        GetCurrentCalenderDetails(Calendar_Id);

                        btnSuppliersCalendarApply.Enabled = Convert.ToBoolean(lv.Tag);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion Events

        #region User Defined Functions
        public ucOperatorCalendar()
        {
            try
            {
                InitializeComponent();
                objCalendarBiz = CalendarBusiness.CreateInstance();

                // Set Tags for controls
                SetTagProperty();
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
                this.btnSuppliersCalendarApply.Tag = "Key_Apply";
                this.ExportCalendar.Tag = "Key_ExportCaption";
                this.lblCalHistory.Tag = "Key_CalendarHistoryColon";
                this.CalendarId.Tag = "Key_CalendarId";
                this.lblCurrentCal.Tag = "Key_CurrentCalendarColon";
                this.lblCntPeriod.Tag = "Key_CurrentPeriodColon";
                this.lblCntWeek.Tag = "Key_CurrentWeekColon";
                this.grpOperatorDetails.Tag = "Key_Details";
                this.EndDate.Tag = "Key_EndDate";
                this.OperName.Tag = "Key_Name";
                this.lbl.Tag = "Key_NextPeriodEndColon";
                this.label3.Tag = "Key_NextYearEndColon";
                this.StartDate.Tag = "Key_StartDate";
                this.lbl_Status.Tag = "Key_Cal_Status";
                noneText = this.GetResourceTextByKey("Key_None");
                naText = this.GetResourceTextByKey("Key_NA");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Load all available Calendars in the listview
        /// </summary>
        public void GetCalForOperator()
        {
            try
            {
                lvSuppliersCalendar.Items.Clear();
                List<GetCalendarListEntity> lstCalendarEntity;
                lstCalendarEntity = objCalendarBiz.GetCalendarList().ToList();
                bool? bValidCalendar = false;
                foreach (var obj in lstCalendarEntity)
                {
                    //objCalendarBiz.CheckCompleteCalendar(obj.Calendar_ID, ref bValidCalendar);
                    //if (Convert.ToBoolean(bValidCalendar))
                    {
                        lvItem = lvSuppliersCalendar.Items.Add(obj.Calendar_ID.ToString(), obj.Calendar_ID);
                        lvItem.SubItems.Add(obj.Calendar_Description);
                        lvItem.SubItems.Add(Convert.ToDateTime(obj.Calendar_Year_Start_Date).ToShortDateString());
                        lvItem.SubItems.Add(Convert.ToDateTime(obj.Calendar_Year_End_Date).ToShortDateString());
                    }

                    if (!Convert.ToBoolean(obj.IsCompleteCalendar))
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
        public void LoadSupplierDetails()
        {
            try
            {
                lstSupplierCalendarHistory.DataSource = null;
                lstSupplierCalendarHistory.Items.Clear();
                lvSuppliersCalendar.Items.Clear();
                txtSuppliersCurrentCalendar.Text = noneText;       // "[None]";
                txtCurrentPeriod.Text = noneText;                  // "[None]";
                txtSuppliersCurrentPeriod.Text = noneText;         // "[None]";
                txtSuppliersNextPeriodEnd.Text = noneText;         // "[None]";
                txtSuppliersNextYearEnd.Text = noneText;           // "[None]";
                txtSuppliersCurrentWeek.Text = noneText;           // "[None]";
                if (!AppGlobals.Current.HasUserAccess("HQ_Calendar_Edit"))
                {
                    ExportCalendar.Enabled = false;
                    btnSuppliersCalendarApply.Enabled = false;
                }
                ExportCalendar.Enabled = false;
                GetCalForOperator();
                LoadOperator();
                lvSuppliersCalendar.Columns[0].Width = 0;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }



        /// <summary>
        /// Load Operators in treeview
        /// </summary>
        public void LoadOperator()
        {

            try
            {
                lstSuppliers.SelectedIndexChanged -= new System.EventHandler(this.lstSuppliers_SelectedIndexChanged);
                lstoperator = objCalendarBiz.GetOperatorDetails();
                lstSuppliers.DataSource = lstoperator;
                lstSuppliers.DisplayMember = "Operator_Name";
                lstSuppliers.ValueMember = "Operator_ID";
                if (lstSuppliers.Items.Count > 0)
                {
                    lstSuppliers.SelectedIndex = -1;
                    lstSuppliers.SelectedIndexChanged += new System.EventHandler(this.lstSuppliers_SelectedIndexChanged);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Reterives the active Calendar Details
        /// </summary>
        /// <param name="Calendar_Id"></param>
        private void GetCurrentCalenderDetails(int Calendar_Id)
        {
            try
            {
                List<CurrentCalendarDetails> lstCurrentCalDetails = objCalendarBiz.GetCurrentCalendarDetails(Calendar_Id);
                if (lstCurrentCalDetails.Count > 0)
                {
                    txtSuppliersCurrentWeek.Text = lstCurrentCalDetails[0].Calendar_Week_Number.ToString();//CurrentWeek.Calendar_Week_Number.Value.ToString();
                    txtSuppliersCurrentPeriod.Text = lstCurrentCalDetails[0].Calendar_Period_Number.ToString();//CurrentWeek.Calendar_Period_Number.ToString();
                    txtSuppliersNextPeriodEnd.Text = Convert.ToDateTime(lstCurrentCalDetails[0].Calendar_Period_End_Date).ToShortDateString();//Convert.ToDateTime(CurrentWeek.Calendar_Period_End_Date).ToString("dd/MM/yyyy");
                }
                else
                {
                    txtSuppliersCurrentWeek.Text = noneText;      // "[None]";
                    txtSuppliersCurrentPeriod.Text = noneText;    // "[None]";
                    txtSuppliersNextPeriodEnd.Text = naText;      // "n/a";
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Load Active Operator Details 
        /// </summary>
        /// <param name="OperatorID"></param>
        public void LoadOperatorHistory(int OperatorID)
        {
            try
            {
                //lstSupplierCalendarHistory.Items.Clear();
                txtSuppliersCurrentCalendar.Text = noneText;      // "[None]";
                lstOperatorActive = null;
                lstOperatorActive = objCalendarBiz.GetOperatorHistory(OperatorID).ToList();
                ExportCalendar.Enabled = false;
                if (lstOperatorActive.Count == 0) return;
                lstSupplierCalendarHistory.DataSource = lstOperatorActive;
                lstSupplierCalendarHistory.DisplayMember = "Calendar_History";
                lstSupplierCalendarHistory.ValueMember = "Operator_Calendar_ID";
                if (AppGlobals.Current.HasUserAccess("HQ_Calendar_Edit"))
                {
                    ExportCalendar.Enabled = (lstSupplierCalendarHistory.Items.Count == 0) ? false : true;
                }
                txtSuppliersCurrentCalendar.Text = lstOperatorActive[0].Calendar_Description;
                txtSuppliersNextPeriodEnd.Text = Convert.ToDateTime(lstOperatorActive[0].Calendar_Year_Start_Date).ToShortDateString();
                txtSuppliersNextYearEnd.Text = Convert.ToDateTime(lstOperatorActive[0].Calendar_Year_End_Date).ToShortDateString();
                GetCurrentCalenderDetails(lstOperatorActive[0].Calendar_ID);

                txtSuppliersCurrentCalendar.Text = lstOperatorActive[0].Calendar_Description.ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion User Defined Functions

        #region Audit
        private void InsertExportOperatorCalendarAudit(int Operatorid)
        {
            try
            {
                AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                {
                    business.InsertAuditData(new Audit.Transport.Audit_History
                    {
                        EnterpriseModuleName = ModuleNameEnterprise.Calendar,
                        Audit_Screen_Name = "ExportCalendars.Operator",
                        Audit_Desc = "Record" + " " + "[" + Operatorid + "]" + " added to calendar.Operator ",
                        Audit_Field = "Operator_ID",
                        AuditOperationType = OperationType.ADD,
                        Audit_New_Vl = Convert.ToString(Operatorid),
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
        private void InsertOperatorCalendarAudit(int Operatorid)
        {
            try
            {
                AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                {
                    business.InsertAuditData(new Audit.Transport.Audit_History
                    {
                        EnterpriseModuleName = ModuleNameEnterprise.Calendar,
                        Audit_Screen_Name = "Calendars.Operator",
                        Audit_Desc = "Record" + " " + "[" + Operatorid + "]" + " added to calendar.Operator ",
                        Audit_Field = "Operator_ID",
                        AuditOperationType = OperationType.ADD,
                        Audit_New_Vl = Convert.ToString(Operatorid),
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

        private void UpdateOperatorCalendarAudit(int OperatorId)
        {
            try
            {
                AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                {
                    CalendarEntity _entity = new CalendarEntity();
                    business.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        EnterpriseModuleName = ModuleNameEnterprise.Calendar,
                        Audit_Screen_Name = "Calendars.Operator",
                        Audit_Desc = "Calendar Assigned to SubCompany" + " " + "[" + OperatorId + "]" + "Calendar" + iPrvCalendarID + "-->" + Calendar_Id + "",
                        Audit_Field = "Calendar_ID",
                        AuditOperationType = OperationType.MODIFY,
                        Audit_New_Vl = Convert.ToString(Calendar_Id),
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
        #endregion Audit

        private void ucOperatorCalendar_Load(object sender, EventArgs e)
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

        private void ExportOperatorCalendars()
        {
            try
            {
                if (lstSuppliers.SelectedItem != null)
                {
                    Operatorid = Convert.ToInt32(lstSuppliers.SelectedValue.ToString());
                    List<ExportCalendar> objExp = objCalendarBiz.GetExportOperaotorID(Operatorid).ToList();

                    if (objExp.Count() > 0)
                    {
                        foreach (var obj in objExp)
                        {
                            objCalendarBiz.InsertExportHistory(obj.Site_ID.ToString(), "O-CALENDAR", obj.Site_ID);

                        }
                        InsertExportOperatorCalendarAudit(Operatorid);
                       // this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_CALENDEREXPORT"), this.ParentForm.Text,MessageBoxButtons.OK, MessageBoxIcon.Information);            // "Calendar export complete."
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CALENDEREXPORT"), this.ParentForm.Text);
                    }
                    else
                    {
                        //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_NOSITES_OPERATOR"),this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);          // "No sites available for the selected Operator"
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_NOSITES_OPERATOR"), this.ParentForm.Text);
                    }
                }
                else
                {
                   // this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_SUPPLIER_NOCALENDAR"),this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);               // "Calendar has not yet been allocated to this supplier."
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SUPPLIER_NOCALENDAR"), this.ParentForm.Text);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    }
}












