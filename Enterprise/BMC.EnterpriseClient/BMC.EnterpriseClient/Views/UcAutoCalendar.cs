using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.CoreLib.Win32;
using BMC.Common;
using System.Configuration;

namespace BMC.EnterpriseClient.Views
{
    public partial class UcAutoCalendar : UserControl
    {

        #region Declared Variables

        AutoCalendar objCalendar = new AutoCalendar();
        List<GetAutoCalendarProfiles> lstCalendarProfiles = null;
        AutoCalendarResult objAutoCalendarResult = null;
        string _sEditMode = "Update";
        string _sButtonCaption = "New";

        public enum Days
        {
            Monday = 1,
            Tuesday = 2,
            Wednesday = 3,
            Thursday = 4,
            Friday = 5,
            Saturday = 6,
            Sunday = 7
        }

        #endregion

        #region Constructor

        public UcAutoCalendar()
        {
            try
            {
                InitializeComponent();
                SetTagProperty();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion

        #region Events

        private void UcAutoCalendar_Load(object sender, EventArgs e)
        {
            try
            {
                this.ResolveResources();
                GetAutoCalendarProfiles();
                int maxvalue = Convert.ToInt32(ConfigurationManager.AppSettings["AutoCalendarMaxDays"]);
                nud_AlertBeforeDays.Maximum = maxvalue;
                nud_AlertRecurrenceDays.Maximum = maxvalue;
                nud_CreateBeforeDays.Maximum = maxvalue;
                cbo_Days.DataSource = Enum.GetValues(typeof(Days));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btn_NewAutoCalendarProfile_Click(object sender, EventArgs e)
        {
            try
            {
                if (_sButtonCaption == "New")
                {
                    _sEditMode = "New";
                    EnableDisableControls("New");
                    ResetControls();
                    txt_ProfileName.Focus();
                }
                else
                {
                    _sEditMode = "Update";
                    ResetControls();
                    EnableDisableControls("Update");
                    if (lb_CalendarProfiles.SelectedItems.Count > 0)
                    {
                        int i = Convert.ToInt32(lb_CalendarProfiles.SelectedIndex);
                        GetAutoCalendarProfiles();
                        lb_CalendarProfiles.SelectedIndex = i;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey("MSG_AC_UNABLETOSAVEPROFILE"));//Unable to save AutoCalendar profile details.
            }
        }

        private void lb_CalendarProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lb_CalendarProfiles.SelectedIndex >= 0)
                {
                    GetAutoCalendarProfilesDetails(Convert.ToInt32(lb_CalendarProfiles.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                int ProfileID = 0;
                if (_sEditMode == "New")
                {
                    if (txt_ProfileName.Text.Trim().Length > 20)
                    {
                        this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_AC_CANNOTEXCEEDLENGTH"), this.Text);//AutoCalendar Profile name can not exceed 20 characters.
                        txt_ProfileName.Focus();
                        return;
                    }

                    if (objCalendar.VerifyAutoCalendarProfiles(txt_ProfileName.Text, 0) == 1)
                    {
                        this.ShowWarningMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_AC_NAMEALREADYEXISTS"), txt_ProfileName.Text), this.Parent.Text);//AutoCalendar Profile name already exists.
                        txt_ProfileName.Focus();
                        return;
                    }
                    if (ValidateControls())
                    {
                        ProfileID = objCalendar.UpdateAutoCalendarProfiles(0,
                                                                            txt_ProfileName.Text,
                                                                            cb_IsAutoCalendarEnabled.Checked,
                                                                            Convert.ToInt32(nud_CreateBeforeDays.Value),
                                                                            Convert.ToInt32(nud_AlertBeforeDays.Value),
                                                                            Convert.ToInt32(nud_AlertRecurrenceDays.Value),
                                                                            cb_IsCalendarBasedonDays.Checked,
                                                                            cbo_Days.SelectedIndex,
                                                                            cb_SetNewCalendarActive.Checked,
                                                                            "", "UPDATE");
                        if (ProfileID > 0)
                        {
                            Win32Extensions.ShowInfoMessageBox(this, string.Format(this.GetResourceTextByKey(1, "MSG_AC_PROFILESAVEDSUCCESSFULLY"), txt_ProfileName.Text));//AutoCalendar profile details saved successfully.
                            //insert data into audit
                            objCalendar.InsertNewAuditEntry("Auto Calendar", "AutoCalendar Profile Name", txt_ProfileName.Text, AppGlobals.Current.UserId, AppGlobals.Current.UserName);
                        }
                        else
                        {
                            Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_AC_UNABLETOSAVEPROFILE"));//Unable to save AutoCalendar profile details.
                        }
                    }
                    else
                    {
                        return;
                    }
                    _sEditMode = "Update";
                }
                else
                {
                    ProfileID = 0;
                    GetAutoCalendarProfilesDetails objUpatedData = new GetAutoCalendarProfilesDetails();

                    if (Convert.ToInt32(lb_CalendarProfiles.SelectedValue) > 0)
                    {
                        if (ValidateControls())
                        {
                            string sSubCompanyResult = string.Empty;

                            foreach (GetAutoCalendarSubCompanyDetails v in lb_AssignedSubCompany.Items)
                            {
                                sSubCompanyResult += "," + v.Sub_Company_ID.ToString();
                            }
                            if (!String.IsNullOrEmpty(sSubCompanyResult))
                            {
                                sSubCompanyResult = sSubCompanyResult.Remove(0, 1);
                            }
                            int iSelectedindex = Convert.ToInt32(lb_CalendarProfiles.SelectedValue);
                            ProfileID = objCalendar.UpdateAutoCalendarProfiles(Convert.ToInt32(lb_CalendarProfiles.SelectedValue),
                                                                                    txt_ProfileName.Text.Trim(),
                                                                                    cb_IsAutoCalendarEnabled.Checked,
                                                                                    Convert.ToInt32(nud_CreateBeforeDays.Value),
                                                                                    Convert.ToInt32(nud_AlertBeforeDays.Value),
                                                                                    Convert.ToInt32(nud_AlertRecurrenceDays.Value),
                                                                                    cb_IsCalendarBasedonDays.Checked,
                                                                                    cbo_Days.SelectedIndex,
                                                                                    cb_SetNewCalendarActive.Checked,
                                                                                    sSubCompanyResult, "UPDATE");
                            if (ProfileID > 0)
                            {
                                Win32Extensions.ShowInfoMessageBox(this, string.Format(this.GetResourceTextByKey(1, "MSG_AC_PROFILESAVEDSUCCESSFULLY"), lb_CalendarProfiles.Text));//AutoCalendar profile details saved successfully.
                                objUpatedData.AutoCalendarProfile_Name = txt_ProfileName.Text.Trim();
                                objUpatedData.IsAutoCalendarEnabled = cb_IsAutoCalendarEnabled.Checked;
                                objUpatedData.CalendarCreateBeforeDays = Convert.ToInt32(nud_CreateBeforeDays.Value);
                                objUpatedData.CalendarAlertBefore = Convert.ToInt32(nud_AlertBeforeDays.Value);
                                objUpatedData.CalendarAlertRecurrence = Convert.ToInt32(nud_AlertRecurrenceDays.Value);
                                objUpatedData.IsCalendarBasedOnDays = cb_IsCalendarBasedonDays.Checked;
                                objUpatedData.NewCalendarDayID = cbo_Days.SelectedIndex;
                                objUpatedData.SetNewCalendarActive = cb_SetNewCalendarActive.Checked;
                            }
                            else
                            {
                                Win32Extensions.ShowErrorMessageBox(this, string.Format(this.GetResourceTextByKey(1, "MSG_AC_UNABLETOSAVEPROFILE"), lb_CalendarProfiles.Text));//Unable to save AutoCalendar profile details.
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        Win32Extensions.ShowInfoMessageBox(this, string.Format(this.GetResourceTextByKey(1, "MSG_AC_NOPROFILETOSAVE"), lb_CalendarProfiles.Text));//No AutoCalendar profile selected to update.
                    }
                }
                GetAutoCalendarProfiles();
                lb_CalendarProfiles.SelectedValue = ProfileID;
                EnableDisableControls("Update");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btn_Assign_Click(object sender, EventArgs e)
        {
            try
            {
                if (lb_UnassignedSubCompany.SelectedItems.Count > 0)
                {
                    MoveListBoxItems(lb_UnassignedSubCompany, lb_AssignedSubCompany);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btn_Unassign_Click(object sender, EventArgs e)
        {
            try
            {
                if (lb_AssignedSubCompany.SelectedItems.Count > 0)
                {
                    MoveListBoxItems(lb_AssignedSubCompany, lb_UnassignedSubCompany);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btn_AssignAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (lb_UnassignedSubCompany.Items.Count > 0)
                {
                    for (int i = 0; i < lb_UnassignedSubCompany.Items.Count; i++)
                    {
                        lb_UnassignedSubCompany.SetSelected(i, true);
                    }
                    MoveListBoxItems(lb_UnassignedSubCompany, lb_AssignedSubCompany);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btn_UnassignAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (lb_AssignedSubCompany.Items.Count > 0)
                {
                    for (int i = 0; i < lb_AssignedSubCompany.Items.Count; i++)
                    {
                        lb_AssignedSubCompany.SetSelected(i, true);
                    }
                    MoveListBoxItems(lb_AssignedSubCompany, lb_UnassignedSubCompany);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cb_IsAutoCalendarEnabled_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cb_IsAutoCalendarEnabled.Checked)
                {
                    nud_CreateBeforeDays.Enabled = true;
                    nud_AlertBeforeDays.Enabled = true;
                    nud_AlertRecurrenceDays.Enabled = true;
                }
                else
                {
                    nud_CreateBeforeDays.Enabled = false;
                    nud_AlertBeforeDays.Enabled = false;
                    nud_AlertRecurrenceDays.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (lb_CalendarProfiles.SelectedItems.Count > 0)
                {
                    if (Win32Extensions.ShowQuestionMessageBox(this, string.Format(this.GetResourceTextByKey(1, "MSG_AC_DELETEPROFILE"), lb_CalendarProfiles.Text)) == DialogResult.Yes)//Do you want to delete the AutoCalendar profile?
                    {
                        int result = objCalendar.UpdateAutoCalendarProfiles(Convert.ToInt32(lb_CalendarProfiles.SelectedValue), lb_CalendarProfiles.Text, false, 0, 0, 0, false, 0, false, "", "DELETE");
                        if (result == 0)
                        {
                            Win32Extensions.ShowInfoMessageBox(this, string.Format(this.GetResourceTextByKey(1, "MSG_AC_DELETESUCCESSFUL"), lb_CalendarProfiles.Text));//AutoCalendar profile deleted successfully.
                            objCalendar.InsertDeteleAuditEntry("AutoCalendar", txt_ProfileName.Text, "", AppGlobals.Current.UserId, AppGlobals.Current.UserName);
                        }
                        else
                        {
                            Win32Extensions.ShowErrorMessageBox(this, string.Format(this.GetResourceTextByKey(1, "MSG_AC_DELETEFAILED"), lb_CalendarProfiles.Text));//Unable to delete AutoCalendar profile details.
                        }
                    }
                }
                else
                {
                    if (lb_CalendarProfiles.Items.Count > 0)
                    {
                        Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_AC_SELECTAPROFILETODELETE"));//Select a AutoCalendar profile delete.
                    }
                    else
                    {
                        Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_AC_NOPROFILESTODELETE"));//No AutoCalendar profiles to delete.
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_AC_DELETEFAILED"));//Unable to delete AutoCalendar profile details.
            }
            GetAutoCalendarProfiles();
        }

        private void cb_IsCalendarBasedonDays_CheckedChanged(object sender, EventArgs e)
        {
            cbo_Days.Enabled = cb_IsCalendarBasedonDays.Checked;
        }

        #endregion

        #region Load Methods

        //Initially Load  Calendar Profiles
        public void GetAutoCalendarProfiles()
        {
            ResetControls();

            try
            {
                lb_CalendarProfiles.DataSource = null;
                lstCalendarProfiles = objCalendar.GetAutoCalendarProfiles();
                lb_CalendarProfiles.DisplayMember = "AutoCalendarProfile_Name";
                lb_CalendarProfiles.ValueMember = "AutoCalendarProfile_ID";
                lb_CalendarProfiles.DataSource = lstCalendarProfiles;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        //based on the profile selected get the details
        private void GetAutoCalendarProfilesDetails(int AutoCalendarProfile_ID)
        {
            try
            {
                objAutoCalendarResult = AutoCalendar.CreateInstance().GetAutoCalendarProfilesDetails(AutoCalendarProfile_ID);
                lb_AssignedSubCompany.Items.Clear();
                lb_UnassignedSubCompany.Items.Clear();

                lb_AssignedSubCompany.DisplayMember = "Sub_Company_Name";
                lb_AssignedSubCompany.ValueMember = "Sub_Company_ID";

                lb_UnassignedSubCompany.DisplayMember = "Sub_Company_Name";
                lb_UnassignedSubCompany.ValueMember = "Sub_Company_ID";

                if (objAutoCalendarResult != null)
                {

                    txt_ProfileName.Text = objAutoCalendarResult.ProfilesDetails[0].AutoCalendarProfile_Name;
                    cb_IsAutoCalendarEnabled.Checked = Convert.ToBoolean(objAutoCalendarResult.ProfilesDetails[0].IsAutoCalendarEnabled);
                    nud_CreateBeforeDays.Value = objAutoCalendarResult.ProfilesDetails[0].CalendarCreateBeforeDays;
                    nud_AlertBeforeDays.Value = objAutoCalendarResult.ProfilesDetails[0].CalendarAlertBefore;
                    nud_AlertRecurrenceDays.Value = objAutoCalendarResult.ProfilesDetails[0].CalendarAlertRecurrence;
                    cb_IsCalendarBasedonDays.Checked = Convert.ToBoolean(objAutoCalendarResult.ProfilesDetails[0].IsCalendarBasedOnDays);
                    cbo_Days.SelectedIndex = objAutoCalendarResult.ProfilesDetails[0].NewCalendarDayID;
                    cb_SetNewCalendarActive.Checked = Convert.ToBoolean(objAutoCalendarResult.ProfilesDetails[0].SetNewCalendarActive);
                    ListViewItem items = new ListViewItem();

                    foreach (GetAutoCalendarSubCompanyDetails details in objAutoCalendarResult.SCDetails)
                    {
                        if (details.Profilestatus == 1)
                        {
                            lb_AssignedSubCompany.Items.Add(details);
                        }
                        else if (details.Profilestatus == 2)
                        {
                            lb_UnassignedSubCompany.Items.Add(details);
                        }
                        else
                        {
                            lb_UnassignedSubCompany.Items.Add(details);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion

        #region Miscellaneous Methods

        private void SetTagProperty()
        {
            try
            {
                lbl_UnAssignedSubCompany.Tag = "Key_AC_UnAssignedSubCompany";
                lbl_AssignedSubCompany.Tag = "Key_AC_AssignedSubCompany";
                lbl_AlertBefore.Tag = "Key_AC_AlertBefore";
                lbl_CreateBeforeDays.Tag = "Key_AC_CreateBeforeDays";
                lbl_AlertRecurrence.Tag = "Key_AC_AlertRecurrence";
                lbl_Profile.Tag = "Key_AC_Profile";
                btn_NewAutoCalendarProfile.Tag = "Key_NewCaption";
                btn_Delete.Tag = "Key_DeleteCaption";
                btn_Update.Tag = "Key_UpdateCaption";
                btn_Unassign.Tag = "Key_LessThan";
                btn_Assign.Tag = "Key_GreaterThan";
                btn_UnassignAll.Tag = "Key_LessThanDouble";
                btn_AssignAll.Tag = "Key_GreaterThanDouble";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        //sawp items between the listboxes
        private void MoveListBoxItems(ListBox source, ListBox destination)
        {
            try
            {
                ListBox.SelectedObjectCollection sourceItems = source.SelectedItems;
                foreach (var item in sourceItems)
                {
                    destination.Items.Add(item);
                }
                while (source.SelectedItems.Count > 0)
                {
                    source.Items.Remove(source.SelectedItems[0]);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void ResetControls()
        {
            try
            {
                txt_ProfileName.Text = string.Empty;

                lb_AssignedSubCompany.Items.Clear();
                lb_UnassignedSubCompany.Items.Clear();

                cb_IsAutoCalendarEnabled.Checked = false;
                cb_IsCalendarBasedonDays.Checked = false;
                cb_SetNewCalendarActive.Checked = false;

                cbo_Days.SelectedIndex = 0;

                nud_CreateBeforeDays.Value = 0;
                nud_AlertBeforeDays.Value = 0;
                nud_AlertRecurrenceDays.Value = 0;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private bool ValidateControls()
        {
            try
            {
                if (txt_ProfileName.Text.Trim() != string.Empty)
                {
                    if (objCalendar.VerifyAutoCalendarProfiles(txt_ProfileName.Text.Trim(), Convert.ToInt32(lb_CalendarProfiles.SelectedValue)) == 1)
                    {
                        this.ShowWarningMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_AC_NAMEALREADYEXISTS"), txt_ProfileName.Text.Trim()), this.Parent.Text);//AutoCalendar Profile name already exists.
                        txt_ProfileName.Focus();
                        return false;
                    }
                    else
                    {
                        if (nud_CreateBeforeDays.Value > 0)
                        {
                            if (nud_AlertRecurrenceDays.Value > 0)
                            {
                                if (nud_CreateBeforeDays.Value < nud_AlertBeforeDays.Value)
                                {
                                    if (nud_AlertRecurrenceDays.Value < nud_AlertBeforeDays.Value)
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_AC_VALIDATERECURRENCEDAYS"));//Calendar alert recurrence days should be less than alert before days.
                                        nud_AlertRecurrenceDays.Focus();
                                        return false;
                                    }
                                }
                                else
                                {
                                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_AC_VALIDATECREATIONDAYS"));//Calendar create before days should be less than alert before days and alert reccurence days.
                                    nud_CreateBeforeDays.Focus();
                                    return false;
                                }
                            }
                            else
                            {
                                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_AC_VALIDATEZERORECURRENCEDAYS"));//Calendar alert recurrence days should be greater than zero.
                                nud_AlertRecurrenceDays.Focus();
                                return false;
                            }
                        }
                        else
                        {
                            Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_AC_VALIDATEZEROCREATIONDAYS"));//Calendar create before days should be greater than zero.
                            nud_CreateBeforeDays.Focus();
                            return false;
                        }
                    }
                }
                else
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_AC_EMPTYPROFILENAME"));//Auto calendar profile name should not be empty.
                    txt_ProfileName.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }

        private void AuditUpdatedOperatorDetails(GetAutoCalendarProfilesDetails objOldData, GetAutoCalendarProfilesDetails objNewData)
        {
            try
            {
                if ((objOldData == null) || (objNewData == null)) return;
                AutoCalendar objAutoCalendar = new AutoCalendar();

                if (objOldData.AutoCalendarProfile_Name.NullToString() != objOldData.AutoCalendarProfile_Name)
                    objAutoCalendar.AuditModifiedData(txt_ProfileName.Text.Trim(), "AutoCalendar Profile Name", objOldData.AutoCalendarProfile_Name, objNewData.AutoCalendarProfile_Name, AppGlobals.Current.UserId, AppGlobals.Current.UserName);

                if (objOldData.IsAutoCalendarEnabled != objNewData.IsAutoCalendarEnabled)
                    objAutoCalendar.AuditModifiedData(txt_ProfileName.Text.Trim(), "IsAutoCalendarEnabled", objOldData.IsAutoCalendarEnabled.ToString(), objNewData.IsAutoCalendarEnabled.ToString(), AppGlobals.Current.UserId, AppGlobals.Current.UserName);

                if (objOldData.CalendarCreateBeforeDays != objNewData.CalendarCreateBeforeDays)
                    objAutoCalendar.AuditModifiedData(txt_ProfileName.Text.Trim(), "CalendarCreateBeforeDays", objOldData.CalendarCreateBeforeDays.ToString(), objNewData.CalendarCreateBeforeDays.ToString(), AppGlobals.Current.UserId, AppGlobals.Current.UserName);

                if (objOldData.CalendarAlertBefore != objNewData.CalendarAlertBefore)
                    objAutoCalendar.AuditModifiedData(txt_ProfileName.Text.Trim(), "CalendarCreateBeforeDays", objOldData.CalendarAlertBefore.ToString(), objNewData.CalendarAlertBefore.ToString(), AppGlobals.Current.UserId, AppGlobals.Current.UserName);

                if (objOldData.CalendarAlertRecurrence != objNewData.CalendarAlertRecurrence)
                    objAutoCalendar.AuditModifiedData(txt_ProfileName.Text.Trim(), "CalendarCreateBeforeDays", objOldData.CalendarAlertRecurrence.ToString(), objNewData.CalendarAlertRecurrence.ToString(), AppGlobals.Current.UserId, AppGlobals.Current.UserName);
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
                    case "New":
                        btn_NewAutoCalendarProfile.Text = this.GetResourceTextByKey("Key_CancelCaption");//"Cancel"
                        _sButtonCaption = "Cancel";
                        lb_CalendarProfiles.Enabled = false;
                        lb_AssignedSubCompany.Enabled = false;
                        lb_UnassignedSubCompany.Enabled = false;
                        btn_Delete.Enabled = false;
                        break;
                    case "Update":
                        _sButtonCaption = "New";
                        btn_NewAutoCalendarProfile.Text = this.GetResourceTextByKey("Key_NewCaption");//"New"
                        lb_CalendarProfiles.Enabled = true;
                        lb_AssignedSubCompany.Enabled = true;
                        lb_UnassignedSubCompany.Enabled = true;
                        btn_Delete.Enabled = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion

    }
}
