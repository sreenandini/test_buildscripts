using BMC.Common.ExceptionManagement;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.CoreLib.Win32;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmServiceCallCreate : Form
    {
        private int iService_ID=0;
        private int iSite_ID=0;
		string noneText = "--NONE--";
        string anyText = "--ANY--";

        private bool IsEdit = false;
        private string sJobID = string.Empty;
        private string sServiceVisitNo=string.Empty;
        private bool IsCallClosed = false;
        private CallStatus sOrigCallStatus;
        ServiceEntity sOrigServiceEntity;
        string dateformat = "dd/MM/yyyy HH:mm";

        bool isCallStatusMinimal = true;

        public frmServiceCallCreate()
        {
            InitializeComponent();

            isCallStatusMinimal = !SettingsEntity.IsServiceCallFeatureFull;   // Get from Settings

            LoadControls();
            cmbSites.TabIndex = 0;        
        }

        public frmServiceCallCreate(int _service_ID, int _site_ID, bool _isCallClosed)
        {
            InitializeComponent();

            isCallStatusMinimal = !SettingsEntity.IsServiceCallFeatureFull;    // Get from Settings

            this.iService_ID = _service_ID;
            this.iSite_ID = _site_ID;
            this.IsCallClosed = _isCallClosed;
            this.IsEdit = true;
            EditCall();          
        }

        private void frmServiceCallCreate_Load(object sender, EventArgs e)
        {
            this.ActiveControl = IsEdit ? cmbCallSource : cmbSites;
        }

        private void LoadControls()
        {
            btnAddCall.Text = "&Add Call";
            btnAddCall.Visible = AppGlobals.Current.HasUserAccess("HQ_Engineers_CreateCall_Edit");
            btnAddNote.Visible = false;
            grpNotes.Visible = false;
            chkShowAllEng.Checked = false;
            chkShowMachineHistory.Checked = false;
            chkReceivedTime.Checked = false;

            ClearTextFields();

            LoadComboBox();

            LoadDateTimeControls();
        }


        private void ClearTextFields()
        {
            txtSubCompany.Text = string.Empty;
            txtContact.Text = string.Empty;
            txtContactPhone.Text = string.Empty;
            txtPostcode.Text = string.Empty;
            txtOpenHours.Text = string.Empty;
            txtDepot.Text = string.Empty;
            txtSiteDetails.Text = string.Empty;
            txtFaultNotes.Text = string.Empty;
            txtRemedyNotes.Text = string.Empty;
            cmbEngineer.SelectedIndex = -1;
        }

        private void LoadDateTimeControls()
        {
            dtpReceivedTime.Format = DateTimePickerFormat.Custom;
            dtpReceivedTime.CustomFormat = dateformat;
            dtpPassedToEngrTime.Format = DateTimePickerFormat.Custom;
            dtpPassedToEngrTime.CustomFormat = dateformat;
            dtpEngrAckTime.Format = DateTimePickerFormat.Custom;
            dtpEngrAckTime.CustomFormat = dateformat;
            dtpArrivalTime.Format = DateTimePickerFormat.Custom;
            dtpArrivalTime.CustomFormat = dateformat;
            dtpCompletedTime.Format = DateTimePickerFormat.Custom;
            dtpCompletedTime.CustomFormat = dateformat;

            dtpReceivedTime.Value = DateTime.Now;
            dtpPassedToEngrTime.Value = DateTime.Now;
            dtpEngrAckTime.Value = DateTime.Now;
            dtpArrivalTime.Value = DateTime.Now;
            dtpCompletedTime.Value = DateTime.Now;

            if (IsEdit)
            {
                dtpReceivedTime.Enabled = false;
            }

            if (isCallStatusMinimal)
            {
                tblPassedToEngTime.Visible = false;
                tblEngrAckTime.Visible = false;
                tblArrivedTime.Visible = false;
                tblCompletedTime.Visible = true;
                lblDespatched.Visible = false;
                lblAcknowledged.Visible = false;
                lblArrived.Visible = false;

                // Move the time completed column up
                tblOpenCall.SetRow(lblCompleted, 9);
                tblOpenCall.SetRow(tblCompletedTime, 9);
            }
        }

        private void LoadComboBox()
        {
            try
            {
                List<MachineTypeEntity> lst_MCType=new List<MachineTypeEntity>();

                //Populating Call Status Combobox
                ServiceCallBusiness.LoadCallStatus(cmbCallStatus, false, false, isCallStatusMinimal);
                cmbCallStatus.SelectedIndex = 0;
			    cmbCallStatus.Enabled = IsEdit;	

                cmbFaultGroup.DataSource = ServiceCallBusiness.CreateInstance().LoadCallGroup(noneText);
                cmbFaultGroup.DisplayMember = "Description";
                cmbFaultGroup.ValueMember = "Id";
                cmbFaultGroup.SelectedIndex = 0;

                cmbFault.DataSource = ServiceCallBusiness.CreateInstance().LoadFaultDescription(0, noneText);
                cmbFault.DisplayMember = "Description";
                cmbFault.ValueMember = "Id";
                cmbFault.SelectedIndex = 0;

                cmbCallSource.DataSource = ServiceCallBusiness.CreateInstance().LoadCallSource(noneText);
                cmbCallSource.DisplayMember = "Description";
                cmbCallSource.ValueMember = "Id";
                cmbCallSource.SelectedIndex = 0;

                cmbRemedy.DataSource = ServiceCallBusiness.CreateInstance().LoadCallRemedy(noneText);
                cmbRemedy.DisplayMember = "Description";
                cmbRemedy.ValueMember = "Id";
                cmbRemedy.SelectedIndex = 0;

                cmbSites.DataSource = ServiceCallBusiness.CreateInstance().LoadSiteNames(iSite_ID , noneText);
                cmbSites.DisplayMember = "Description";
                cmbSites.ValueMember = "Id";
                cmbSites.SelectedIndex = 0;
                if (IsEdit)
                {
                    cmbSites.SelectedIndex = 1;
                    cmbSites.Enabled = false;
                }

                if (IsEdit)
                {
                    lst_MCType = ServiceCallBusiness.CreateInstance().LoadMachineTypes(false);   // Passs true for New Call creation
                }
                else
                {
                    lst_MCType = ServiceCallBusiness.CreateInstance().LoadMachineTypes(true);   // Passs true for New Call creation
                }

                if (lst_MCType != null)
                {
                    lst_MCType.Insert(0, new MachineTypeEntity { Machine_Type_ID = -1, Machine_Type_Code = anyText });
                }
                cmbMachineType.SelectedIndex = -1;
                cmbMachineType.DisplayMember = "Machine_Type_Code";
                cmbMachineType.ValueMember = "Machine_Type_ID";
                cmbMachineType.DataSource = lst_MCType;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbFaultGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int grpId = (cmbFaultGroup.SelectedItem as CallGroup).Id;
                cmbFault.DataSource = ServiceCallBusiness.CreateInstance().LoadFaultDescription(grpId, noneText);
                cmbFault.DisplayMember = "Description";
                cmbFault.ValueMember = "Id";
                cmbFault.SelectedIndex = 0;               
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbSites.SelectedIndex <= 0)               
                    ClearTextFields();
                else
                {  
                    if (cmbSites.SelectedValue != null && cmbSites.ValueMember != string.Empty)
                    {
                        // Load Site details
                        SiteDetailEntity siteDetails =  ServiceCallBusiness.CreateInstance().LoadSiteDetails(Convert.ToInt32(cmbSites.SelectedValue));
                        txtSubCompany.Text = siteDetails.Sub_Company_Name;
                        txtContact.Text  = siteDetails.Site_Manager;
                        txtContactPhone.Text = siteDetails.Site_Phone_No;
                        txtPostcode.Text = siteDetails.Site_Postcode;
                        txtOpenHours.Text = siteDetails.Standard_Opening_Hours_Description;
                        if (!string.IsNullOrEmpty(siteDetails.Depot_Name))
                        {
                            txtDepot.Text = siteDetails.Depot_Name + " - " + siteDetails.Service_Area_Name;
                        }
                        txtSiteDetails.Text = siteDetails.Site_Address_1 + "\n" + siteDetails.Site_Address_2 + "\n" +
                                                siteDetails.Site_Address_3 + "\n" + siteDetails.Site_Address_4 + "\n" + siteDetails.Site_Address_5;

                    }
                }

                if (cmbSites.ValueMember != string.Empty)
                {
                    // Load Engineer details
                    chkShowAllEng.Checked = false;

                    // Load Machine Names
                    LoadMachineNames();
                }
                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void chkShowAllEng_CheckedChanged(object sender, EventArgs e)
        {
            LoadEngineers();
        }

        private void LoadEngineers()
        {
            try
            {
                // Load Engineer details
                if (chkShowAllEng.Checked)
                    cmbEngineer.DataSource = ServiceCallBusiness.CreateInstance().LoadEngineerNamesForSite(0, noneText);      // Passing SiteId as 0 to get all Engineers
                else
                    cmbEngineer.DataSource = ServiceCallBusiness.CreateInstance().LoadEngineerNamesForSite(Convert.ToInt32(cmbSites.SelectedValue), noneText);

                cmbEngineer.DisplayMember = "Description";
                cmbEngineer.ValueMember = "Id";
                cmbEngineer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void EditCall()
        {
            try
            {
                cmbCallStatus.Enabled = false;
                cmbSites.Enabled = true;
                btnAddCall.Text = "&Update Call";
                btnAddCall.Visible = AppGlobals.Current.HasUserAccess("HQ_Engineers_Current_Edit");
                LoadComboBox();
                LoadDateTimeControls();
                SetEditCallDetailsToControls();
            
                if(IsCallClosed)
                {
                    foreach (Control cntl in tblOpenCall.Controls)
                    {
                        cntl.Enabled = false;
                    }
                    grpNotes.Enabled = true;
                    btnAddCall.Enabled = false;
                    btnAddNote.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbMachineType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMachineNames();
        }

        private void chkShowMachineHistory_CheckedChanged(object sender, EventArgs e)
        {
            LoadMachineNames();
        }

        private void LoadMachineNames()
        {
            try
            {
                cmbMachineName.DataSource = null;

                // Load Machine Names
                if (cmbSites.SelectedValue != null)
                {
                    int machineTypeId = cmbMachineType.SelectedIndex > 0 ? Convert.ToInt32(cmbMachineType.SelectedValue) : 0;
                    cmbMachineName.DataSource = ServiceCallBusiness.CreateInstance().LoadMachineNames(chkShowMachineHistory.Checked, Convert.ToInt32(cmbSites.SelectedValue), machineTypeId);

                    if (cmbMachineName.Items.Count > 0)
                    {
                        cmbMachineName.DisplayMember = "Custom_Machine_Name";
                        cmbMachineName.ValueMember = "Installation_ID";
                        cmbMachineName.SelectedIndex = -1;
                    }
                    else
                    {
                        cmbMachineName.DataSource = null;
                        cmbMachineName.Items.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private bool CheckValidStatus(int sOrigStatus, int sCurrStatus, ref string errorMsg)
        {
            List<int> validStatuses = new List<int>();
            switch (sOrigStatus)
            {
                case (int)CallStatus.CALL_STATUS_LOGGED:
                    validStatuses.Add((int)CallStatus.CALL_STATUS_VIEWED);
                    validStatuses.Add((int)CallStatus.CALL_STATUS_AWAITING_RECIEPT);
                    break;
                case (int)CallStatus.CALL_STATUS_VIEWED:
                    validStatuses.Add((int)CallStatus.CALL_STATUS_AWAITING_RECIEPT);
                    validStatuses.Add((int)CallStatus.CALL_STATUS_REFUSED);
                    break;
                case (int)CallStatus.CALL_STATUS_AWAITING_RECIEPT:
                    validStatuses.Add((int)CallStatus.CALL_STATUS_ENGINEER_CONFIRMED);                  
                    break;
                case (int)CallStatus.CALL_STATUS_ENGINEER_CONFIRMED:
                    validStatuses.Add((int)CallStatus.CALL_STATUS_ENROUTE);                  
                    break;
                case (int)CallStatus.CALL_STATUS_ENROUTE:
                    validStatuses.Add((int)CallStatus.CALL_STATUS_AT_SITE);  
                    break;
                case (int)CallStatus.CALL_STATUS_AT_SITE:
                    validStatuses.Add((int)CallStatus.CALL_STATUS_RECEIVED);
                    validStatuses.Add((int)CallStatus.CALL_STATUS_COMPLETED);
                    break;
                case (int)CallStatus.CALL_STATUS_RECEIVED:
                    validStatuses.Add((int)CallStatus.CALL_STATUS_REFUSED);
                    validStatuses.Add((int)CallStatus.CALL_STATUS_COMPLETED);  
                    break;
                case (int)CallStatus.CALL_STATUS_REFUSED:
                    break;
                //case (int)CallStatus.CALL_STATUS_COMPLETED:
                //    break;
            }

            foreach (int i in validStatuses)
            {
                errorMsg += ServiceCallBusiness.GetEnumDescription((CallStatus)i) + " / ";
            }

            if (validStatuses.Count > 0 && validStatuses.Contains(sCurrStatus))
                return true;
            else
                return false;
        }

		 private void SetEditCallDetailsToControls()
        {
            try
            {
                sOrigServiceEntity = ServiceCallBusiness.CreateInstance().GetEditServiceDetail(iService_ID, IsCallClosed);

                sJobID = sOrigServiceEntity.Service_Allocated_Job_No.ToString();
                sServiceVisitNo = sOrigServiceEntity.Service_Visit_No.ToString();

                sOrigCallStatus = (CallStatus)sOrigServiceEntity.Call_Status_ID;

                if (IsCallClosed == false)
                {  
                    if (sOrigServiceEntity.Call_Status_ID == (int)CallStatus.CALL_STATUS_LOGGED && sOrigServiceEntity.Service_Issued_To_Staff_ID <= 0 && !isCallStatusMinimal)
                        sOrigServiceEntity.Call_Status_ID = (int)CallStatus.CALL_STATUS_VIEWED;
                }
                 
                cmbCallStatus.SelectedValue = (sOrigServiceEntity.Call_Status_ID == null) ? 0 : sOrigServiceEntity.Call_Status_ID;                               

                cmbCallSource.SelectedValue = (sOrigServiceEntity.Call_Source_ID == null) ? 0 : sOrigServiceEntity.Call_Source_ID;
                cmbFaultGroup.SelectedValue = (sOrigServiceEntity.Call_Group_ID == null) ? 0 : sOrigServiceEntity.Call_Group_ID;
                cmbFault.SelectedValue = (sOrigServiceEntity.Call_Fault_ID == null) ? 0 : sOrigServiceEntity.Call_Fault_ID;
                txtFaultNotes.Text = sOrigServiceEntity.Call_Fault_Additional_Notes;
                if (cmbEngineer.Items.OfType<EngineerEntityForService>().Any(i => i.Id.Equals(sOrigServiceEntity.Service_Issued_To_Staff_ID)))
                {
                    cmbEngineer.SelectedValue = sOrigServiceEntity.Service_Issued_To_Staff_ID;
                }
                else
                {
                    if (sOrigServiceEntity.Service_Issued_To_Staff_ID != null && sOrigServiceEntity.Service_Issued_To_Staff_ID > 0)
                    {
                        // Load Engineer details
                        List<EngineerEntityForService> lstEng = ServiceCallBusiness.CreateInstance().LoadEngineerNamesForSite(Convert.ToInt32(cmbSites.SelectedValue), noneText);
                        
                        EngineerEntityForService objEE=ServiceCallBusiness.CreateInstance().LoadEngineerNames(sOrigServiceEntity.Service_Issued_To_Staff_ID).SingleOrDefault();

                        cmbEngineer.DataSource = null;
                        lstEng.Add(objEE);
                        cmbEngineer.DataSource = lstEng;
                        cmbEngineer.DisplayMember = "Description";
                        cmbEngineer.ValueMember = "Id";
                        cmbEngineer.SelectedValue = objEE.Id;
                    }
                }
                cmbRemedy.SelectedValue = (sOrigServiceEntity.Call_Remedy_ID == null) ? 0 : sOrigServiceEntity.Call_Remedy_ID;
                txtRemedyNotes.Text = sOrigServiceEntity.Call_Remedy_Additional_Description;
                cmbMachineType.SelectedIndex = cmbMachineType.FindStringExact(sOrigServiceEntity.Machine_Type_ID);
                cmbMachineName.SelectedValue = (sOrigServiceEntity.Installation_ID == null) ? 0 : sOrigServiceEntity.Installation_ID;

                if (!string.IsNullOrEmpty(sOrigServiceEntity.Service_Received))
                {
                    dtpReceivedTime.Value = Convert.ToDateTime(sOrigServiceEntity.Service_Received.ToString());
                }
                else
                {
                    dtpReceivedTime.Value = DateTime.Now;
                }

                if (!string.IsNullOrEmpty(sOrigServiceEntity.Service_Acknowledged))
                {
                    dtpEngrAckTime.Value = Convert.ToDateTime(sOrigServiceEntity.Service_Acknowledged.ToString());
                }
                else
                {
                    dtpEngrAckTime.Value = DateTime.Now;
                }

                if (!string.IsNullOrEmpty(sOrigServiceEntity.Service_Arrived_At_Site))
                {
                    dtpArrivalTime.Value = Convert.ToDateTime(sOrigServiceEntity.Service_Arrived_At_Site.ToString());
                }
                else
                {
                    dtpArrivalTime.Value = DateTime.Now;
                }

                if (!string.IsNullOrEmpty(sOrigServiceEntity.Service_Issued))
                {
                    dtpPassedToEngrTime.Value = Convert.ToDateTime(sOrigServiceEntity.Service_Issued.ToString());
                }
                else
                {
                    dtpPassedToEngrTime.Value = DateTime.Now;
                }

                if (!string.IsNullOrEmpty(sOrigServiceEntity.Service_Cleared))
                {
                    dtpCompletedTime.Value = Convert.ToDateTime(sOrigServiceEntity.Service_Cleared.ToString());
                }
                else
                {
                    dtpCompletedTime.Value = DateTime.Now;
                }

                this.Text = (sOrigServiceEntity.Service_Visit_No!=null && sOrigServiceEntity.Service_Visit_No > 0) ? "Service Job - " + sOrigServiceEntity.Service_Allocated_Job_No + "\\" + sOrigServiceEntity.Service_Visit_No : "Service Job - " + sOrigServiceEntity.Service_Allocated_Job_No;

                if (sOrigServiceEntity.Service_Additional_Work_Req!=null && sOrigServiceEntity.Service_Additional_Work_Req==true)
                {
                    this.grpBoxCall.Text = "Additional Work Call";
                }
                else if (IsCallClosed)
                {
                    this.grpBoxCall.Text = "Closed Call";
                }
                else
                {
                    this.grpBoxCall.Text = "Update Call / Close Call";
                }

                if (sOrigServiceEntity.Site_ID == 0 || sOrigServiceEntity.Site_ID==null)
                {
                    cmbSites.Focus();
                }

                FillListViewNotesWithNotes();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void FillListViewNotesWithNotes()
        {
            try
            {
                lvNotes.Items.Clear();

                List<ServiceNotesDisplayEntity> lstSN = ServiceCallBusiness.CreateInstance().GetServiceNotesDisplay(Convert.ToInt32(sJobID.ToString()));
                ListViewItem item;

                foreach (ServiceNotesDisplayEntity SN in lstSN)
                {
                    item = new ListViewItem(SN.Staff_Name);
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, SN.Service_Notes_Notes));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(SN.Service_Notes_Date))));
                    lvNotes.Items.Add(item);
                }                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbEngineer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEngineer.SelectedIndex > 0  && IsEdit==true)
                cmbCallStatus.Enabled = true;            
            else
                cmbCallStatus.Enabled = false;
        }

        private void btnAddCall_Click(object sender, EventArgs e)
        {
            try
            {               
                if (!IsEdit)
                {
                    if (IsValidCallDetails())
                    {
                        SaveCallDetails("New");
                        this.ShowMessageBox("Service Call created successfully", MessageBoxButtons.OK, MessageBoxIcon.None);
                        LoadControls();
                    }
                }
                else
                {
                    if (IsValidCallDetails())
                    {
                        SaveCallDetails("Edit");

                        if(cmbCallStatus.Text == "Completed")
                            this.ShowMessageBox("Service Call closed successfully", MessageBoxButtons.OK, MessageBoxIcon.None);
                        else
                            this.ShowMessageBox("Service Call updated successfully", MessageBoxButtons.OK, MessageBoxIcon.None);

                       this.Close();
                    }
                        
                }
                
            }
            catch (Exception ex)
            {
                this.ShowMessageBox("Error occured while saving Service Call.",
                          MessageBoxButtons.OK, MessageBoxIcon.None);
                ExceptionManager.Publish(ex);
            }
        }

        private bool IsValidCallDetails()
        {         
            if (cmbSites.SelectedIndex <= 0) 
            {
                this.ShowMessageBox("Please select valid Site", MessageBoxButtons.OK, MessageBoxIcon.None);
                this.ActiveControl = cmbSites;
                return false;
            }

            if (cmbCallSource.SelectedIndex <= 0) 
            {
                this.ShowMessageBox("Please select valid Source", MessageBoxButtons.OK, MessageBoxIcon.None);
                this.ActiveControl = cmbCallSource;
                return false;
            }
            if (cmbFaultGroup.SelectedIndex <= 0)  
            {
                this.ShowMessageBox("Please select valid Fault Group", MessageBoxButtons.OK, MessageBoxIcon.None);
                this.ActiveControl = cmbFaultGroup;
                return false;
            }
            if (cmbFault.SelectedIndex <= 0)
            {
                this.ShowMessageBox("Please select valid Fault", MessageBoxButtons.OK, MessageBoxIcon.None);
                this.ActiveControl = cmbFault;
                return false;
            }

            if (IsEdit)
            {
                string sCallStatus = string.Empty;
                sCallStatus = ServiceCallBusiness.GetEnumDescription((Enum)((CallStatus)cmbCallStatus.SelectedValue));


                if (!isCallStatusMinimal)
                {
                    string errorMsg = string.Empty;
                    bool isValid = CheckValidStatus((int)sOrigCallStatus, (int)cmbCallStatus.SelectedValue, ref errorMsg);
                    if ((int)sOrigCallStatus != (int)cmbCallStatus.SelectedValue && isValid == false)
                    {
                        this.ShowMessageBox(string.Format("Call Status - {0} can only be changed to one of the following status \n  - {1}",
                            ServiceCallBusiness.GetEnumDescription((Enum)sOrigCallStatus),
                            errorMsg.Remove(errorMsg.LastIndexOf('/'), 2)), "Service Calls", MessageBoxButtons.OK, MessageBoxIcon.None);

                        this.ActiveControl = cmbCallStatus;
                        return false;
                    }

                    // Only for Fully functional Call Status
                    if (sCallStatus != "Logged" && sCallStatus != "Viewed" && sCallStatus != "Rejected")
                    {
                        if (cmbEngineer.SelectedIndex <= 0)
                        {
                            this.ShowMessageBox("Please select an Engineer", "Service Calls", MessageBoxButtons.OK, MessageBoxIcon.None);
                            this.ActiveControl = cmbEngineer;
                            return false;
                        }
                    }
                }

                if (this.grpBoxCall.Text != "Closed Call")
                {
                    if(!PerformDateValidation())
                        return false;
                }

                if (sCallStatus == "Completed")
                {
                    if (cmbRemedy.SelectedIndex <= 0)
                    {
                        this.ShowMessageBox("Please select valid Remedy", "Service Calls", MessageBoxButtons.OK, MessageBoxIcon.None);
                        this.ActiveControl = cmbRemedy;
                        return false;
                    }
                    if ( txtRemedyNotes.Text == string.Empty)
                    {
                        this.ShowMessageBox("Please enter Remedy Notes", "Service Calls", MessageBoxButtons.OK, MessageBoxIcon.None);
                        this.ActiveControl = txtRemedyNotes;
                        return false;
                    }

                    if (!AppGlobals.Current.HasUserAccess("HQ_Engineers_Current_close"))
                    {
                        this.ShowMessageBox("User does not have rights to Close the Call", "Service Calls", MessageBoxButtons.OK, MessageBoxIcon.None);
                        return false;
                    }
                }
            } 

            // For received date
            if (chkReceivedTime.Checked && dtpReceivedTime.Value.Date  < DateTime.Now.Date)
            {
                this.ShowMessageBox("The date and time the call logged cannot be less than current day", MessageBoxButtons.OK, MessageBoxIcon.None);
                this.ActiveControl = dtpReceivedTime;
                return false;
            }
            
            return true;
        }

        private bool PerformDateValidation()
        {
            if (!isCallStatusMinimal)
            {
                if (chkPassedToEngrTime.Checked)
                {
                    //  The date and time the call was sent to the engineer has be be after the call was logged
                    if (dtpPassedToEngrTime.Value < dtpReceivedTime.Value)
                    {
                        this.ShowMessageBox("The date and time the call was sent to the engineer must be after the call was logged", MessageBoxButtons.OK, MessageBoxIcon.None);
                        this.ActiveControl = dtpPassedToEngrTime;
                        return false;
                    }
                }
                if (chkEngrAckTime.Checked)
                {
                    //  The date and time the call was sent to the engineer must be before they arrived on site
                    if (dtpEngrAckTime.Value < dtpPassedToEngrTime.Value)
                    {
                        this.ShowMessageBox("The date and time the call was acknowledged by the engineer must be after the call was passed to the engineer", MessageBoxButtons.OK, MessageBoxIcon.None);
                        this.ActiveControl = dtpEngrAckTime;
                        return false;
                    }
                }
                if (chkArrivalTime.Checked)
                {
                    // The date and time the call recieved must be before the engineer arrived on site.
                    if (dtpArrivalTime.Value < dtpEngrAckTime.Value)
                    {
                        this.ShowMessageBox("The date and time the engineer has arrived at site must be after the the engineer has acknowledged", MessageBoxButtons.OK, MessageBoxIcon.None);
                        this.ActiveControl = dtpArrivalTime;
                        return false;
                    }
                }
            }
            if (chkCompletedTime.Checked)   // Completed
            {
                if (dtpCompletedTime.Value < dtpReceivedTime.Value)
                {
                    this.ShowMessageBox("The date and time the call is completed must be after the call was logged", MessageBoxButtons.OK, MessageBoxIcon.None);
                    this.ActiveControl = dtpCompletedTime;
                    return false;
                }
            }

            if (!isCallStatusMinimal)
            {
                // If greater than current day
                if (chkPassedToEngrTime.Checked && dtpPassedToEngrTime.Value > DateTime.Now)
                {
                    this.ShowMessageBox("The date and time the call was sent to the engineer cannot be greater than current date", MessageBoxButtons.OK, MessageBoxIcon.None);
                    this.ActiveControl = dtpPassedToEngrTime;
                    return false;
                }
                if (chkEngrAckTime.Checked && dtpEngrAckTime.Value > DateTime.Now)
                {
                    this.ShowMessageBox("The date and time the call was acknowledged by the engineer cannot be greater than current date", MessageBoxButtons.OK, MessageBoxIcon.None);
                    this.ActiveControl = dtpEngrAckTime;
                    return false;
                }
                if (chkArrivalTime.Checked && dtpArrivalTime.Value > DateTime.Now)
                {
                    this.ShowMessageBox("The date and time the engineer has arrived at site cannot be greater than current date", MessageBoxButtons.OK, MessageBoxIcon.None);
                    this.ActiveControl = dtpArrivalTime;
                    return false;
                }
            }
            if (chkCompletedTime.Checked && dtpCompletedTime.Value > DateTime.Now)
            {
                this.ShowMessageBox("The date and time the call is completed cannot be greater than current date", MessageBoxButtons.OK, MessageBoxIcon.None);
                this.ActiveControl = dtpCompletedTime;
                return false;
            }
            return true;
        }

        private void SaveCallDetails(string callMode)
        {
            int serviceAllocatedJobNo = 0;
			try
            {
                System.DateTime? callLoggedTime = null;
                int engrId = -1;
                if (cmbEngineer.SelectedValue != null)
                    engrId = Convert.ToInt32(cmbEngineer.SelectedValue);

                int statusId = -1;
                if (!isCallStatusMinimal && ((int)cmbCallStatus.SelectedValue == (int)CallStatus.CALL_STATUS_LOGGED) && engrId > 0)
                    statusId = (int)CallStatus.CALL_STATUS_AWAITING_RECIEPT;
                else
                    statusId = Convert.ToInt32(cmbCallStatus.SelectedValue.ToString());

            switch (callMode)
            {
                case "New":
                    
                  callLoggedTime = chkReceivedTime.Checked ? dtpReceivedTime.Value : DateTime.Now;

                  serviceAllocatedJobNo =  ServiceCallBusiness.CreateInstance().InsertOrUpdateServiceCall(
                                            0,
                                            Convert.ToInt32(cmbSites.SelectedValue),
                                            Convert.ToInt32(cmbCallSource.SelectedValue),
                                            Convert.ToInt32(cmbFault.SelectedValue),
                                            Convert.ToInt32(cmbFaultGroup.SelectedValue),
                                            Convert.ToInt32(cmbRemedy.SelectedValue),
                                            cmbMachineType.SelectedIndex <= 0 ? "" : cmbMachineType.Text,
                                            Convert.ToInt32(cmbMachineName.SelectedValue),
                                            1, // ServiceVisitNo
                                            AppGlobals.Current.UserId,  // ServiceReceivedStaffID        -> 99
                                            engrId,  // ServiceIssuedToStaffID   -> -1
                                            AppGlobals.Current.UserId,  // ServiceIssuedByStaffID  -> 0
                                            statusId,                                                           
                                            txtFaultNotes.Text.Trim(),
                                            txtRemedyNotes.Text.Trim(),
                                            callLoggedTime.Value);
                  
                  // Add Audit entry for new service call 
                  ServiceCallBusiness.CreateInstance().InsertNewAuditEntry(Audit.Transport.ModuleNameEnterprise.ServiceCalls,
                                 string.Format("New service call is created : " + serviceAllocatedJobNo  + " [Site: {0} with Call Source : {1} and Call Group : {2}]", 
                                 cmbSites.Text, cmbCallSource.Text, cmbFaultGroup.Text), "Create Call");
                  
                  // Do Notes entry
                  DoNoteEntries(callLoggedTime, jobNoNew: serviceAllocatedJobNo);
                        break;
                case "Edit":
                        System.DateTime? dateTimeNull = null; 
                        bool IsCallClosed = false;                       

                        // If status is Completed or Rejected, set IsCallClosed as true.
                        if( (statusId == (int)CallStatus.CALL_STATUS_COMPLETED || statusId == (int)CallStatus.CALL_STATUS_REFUSED))
                            IsCallClosed = true;

                       // Get Dates
                        System.DateTime? passedToEngrTime = null, engrAckTime = null, engAtSiteTime = null, callClearedTime = null;

                        callLoggedTime = Convert.ToDateTime(sOrigServiceEntity.Service_Received);

                        if (chkReceivedTime.Checked && statusId == (int)CallStatus.CALL_STATUS_LOGGED)
                            callLoggedTime = dtpReceivedTime.Value;

                        if (statusId == (int)CallStatus.CALL_STATUS_AWAITING_RECIEPT)
                            passedToEngrTime = chkPassedToEngrTime.Checked ? dtpPassedToEngrTime.Value : DateTime.Now;
                        else if (statusId == (int)CallStatus.CALL_STATUS_ENGINEER_CONFIRMED)
                        {
                            passedToEngrTime = chkPassedToEngrTime.Checked ? dtpPassedToEngrTime.Value : dateTimeNull;
                            engrAckTime = chkEngrAckTime.Checked ? dtpEngrAckTime.Value : DateTime.Now;
                        }
                        else if (statusId == (int)CallStatus.CALL_STATUS_AT_SITE)
                        {
                            passedToEngrTime = chkPassedToEngrTime.Checked ? dtpPassedToEngrTime.Value : dateTimeNull;
                            engrAckTime = chkEngrAckTime.Checked ? dtpEngrAckTime.Value : dateTimeNull;
                            engAtSiteTime = chkArrivalTime.Checked ? dtpArrivalTime.Value : DateTime.Now;
                        }
                        else if (statusId == (int)CallStatus.CALL_STATUS_COMPLETED)
                        {
                            passedToEngrTime = chkPassedToEngrTime.Checked ? dtpPassedToEngrTime.Value : dateTimeNull;
                            engrAckTime = chkEngrAckTime.Checked ? dtpEngrAckTime.Value : dateTimeNull;
                            engAtSiteTime = chkArrivalTime.Checked ? dtpArrivalTime.Value : dateTimeNull;
                            callClearedTime = chkCompletedTime.Checked ? dtpCompletedTime.Value : DateTime.Now;
                        }
                        else if (statusId == (int)CallStatus.CALL_STATUS_REFUSED)
                            callClearedTime = DateTime.Now;                      

                        serviceAllocatedJobNo = ServiceCallBusiness.CreateInstance().InsertOrUpdateServiceCall(
                                      iService_ID, 
                                      iSite_ID,
                                      Convert.ToInt32(cmbCallSource.SelectedValue.ToString()),
                                      Convert.ToInt32(cmbFault.SelectedValue),
                                      Convert.ToInt32(cmbFaultGroup.SelectedValue.ToString()),
                                      Convert.ToInt32(cmbRemedy.SelectedValue.ToString()),
                                      cmbMachineType.SelectedIndex <= 0 ? "" : cmbMachineType.Text,
                                      Convert.ToInt32(cmbMachineName.SelectedValue),
                                      Convert.ToInt32(sServiceVisitNo.ToString()), 
                                      sOrigServiceEntity.Service_Received_Staff_ID.Value, 
                                      engrId,
                                      AppGlobals.Current.UserId,
                                      statusId,
                                      txtFaultNotes.Text, 
                                      txtRemedyNotes.Text,
                                      callLoggedTime.Value, 
                                      Convert.ToInt32(sJobID.ToString()), 
                                      IsCallClosed,
                                      passedToEngrTime,
                                      engrAckTime,
                                      engAtSiteTime,
                                      callClearedTime,
                                      IsCallClosed ? AppGlobals.Current.UserId : 0);


                        ServiceEntity serviceToSave = new ServiceEntity()
                            {
                                Service_ID = iService_ID,
                                Site_ID = iSite_ID,
                                Call_Source_ID = Convert.ToInt32(cmbCallSource.SelectedValue.ToString()),                    
                                Call_Fault_ID = Convert.ToInt32(cmbFault.SelectedValue),
                                Call_Group_ID = Convert.ToInt32(cmbFaultGroup.SelectedValue.ToString()),
                                Call_Remedy_ID = Convert.ToInt32(cmbRemedy.SelectedValue.ToString()),
                                Machine_Type_ID = cmbMachineType.SelectedIndex <= 0 ? "" : cmbMachineType.Text,                                
                                Installation_ID = Convert.ToInt32(cmbMachineName.SelectedValue),
                                Service_Visit_No = Convert.ToInt32(sServiceVisitNo.ToString()),
                                Service_Received_Staff_ID = sOrigServiceEntity.Service_Received_Staff_ID,
                                Service_Issued_To_Staff_ID = engrId,
                                Service_Issued_By_Staff_ID = AppGlobals.Current.UserId,
                                Call_Status_ID = statusId,
                                Call_Fault_Additional_Notes = txtFaultNotes.Text,
                                Call_Remedy_Additional_Description = txtRemedyNotes.Text,
                                Service_Received = callLoggedTime.ToString(),
                                Service_Allocated_Job_No = Convert.ToInt32(sJobID.ToString()),
                                Service_Issued = passedToEngrTime.ToString(),
                                Service_Acknowledged = engrAckTime.ToString(),
                                Service_Arrived_At_Site = engAtSiteTime.ToString(),
                                Service_Cleared = callClearedTime.ToString(),
                                Service_Cleared_Staff_ID = IsCallClosed ? AppGlobals.Current.UserId : 0,
                                SLA_Contract_ID = sOrigServiceEntity.SLA_Contract_ID 
                            };

                    // Do Audit entry for service call update
                        ServiceCallBusiness.CreateInstance().AuditUpdateServiceCall(sOrigServiceEntity, serviceToSave, "Update Call");

                    // Put notes entry
                    DoNoteEntries(null, callClearedTime);
                 
                    break;                
            }
			}
            catch (Exception ex)
            {
                throw;
            }

        }

        private void DoNoteEntries( DateTime? callLoggedTime = null, DateTime? callClearedTime = null, int? jobNoNew = null)
        {
            try
            {
                DateTime notesDt = DateTime.Now;

                if (callLoggedTime != null)
                {
                    // For Call Creation
                    CreateServiceNote(string.Format("Call created at {0}",
                                      callLoggedTime.Value.ToString(dateformat)),
                                      notesDate: notesDt, jobNo:jobNoNew);
                }
                else
                {
                    // For call updates
                    // If status is changed, create a log entry
                    if (sOrigCallStatus != (CallStatus)cmbCallStatus.SelectedValue)
                    {
                        if ((CallStatus)cmbCallStatus.SelectedValue != CallStatus.CALL_STATUS_COMPLETED)
                        {
                            CreateServiceNote(string.Format("Call Status modified from {0} to {1}",
                                                ServiceCallBusiness.GetEnumDescription(sOrigCallStatus),
                                                ServiceCallBusiness.GetEnumDescription((CallStatus)cmbCallStatus.SelectedValue)),
                                                notesDate: notesDt);
                        }
                        else
                        {
                            CreateServiceNote(string.Format("Call closed at {0}",
                                              callClearedTime.Value.ToString(dateformat)),
                                              notesDate: notesDt);
                        }
                    }
                    if (chkReceivedTime.Checked)
                    {
                        // string.Format(sOrigServiceEntity.Service_Received, "dd/MM/yyyy HH:mm"),

                        CreateServiceNote(string.Format("Time call was recieved manually altered From: {0} To: {1}",
                                           Convert.ToDateTime(sOrigServiceEntity.Service_Received).ToString(dateformat),
                                           dtpReceivedTime.Value.ToString(dateformat)),
                                           notesDate: notesDt);
                    }

                    if (chkPassedToEngrTime.Checked)
                    {
                        CreateServiceNote(string.Format("Time passed to engineer manually altered From: {0} To: {1}",
                                            Convert.ToDateTime(sOrigServiceEntity.Service_Issued).ToString(dateformat),
                                           dtpPassedToEngrTime.Value.ToString(dateformat)),
                                           notesDate: notesDt);
                    }
                    if (chkEngrAckTime.Checked)
                    {
                        CreateServiceNote(string.Format("Time call was acknowledged by engineer manually altered From {0} To: {1}",
                                             Convert.ToDateTime(sOrigServiceEntity.Service_Acknowledged).ToString(dateformat),
                                            dtpArrivalTime.Value.ToString(dateformat)),
                                            notesDate: notesDt);
                    }
                    if (chkArrivalTime.Checked)
                    {
                        CreateServiceNote(string.Format("Time Engineer Arrived at site manually altered From {0} To: {1}",
                                               Convert.ToDateTime(sOrigServiceEntity.Service_Arrived_At_Site).ToString(dateformat),
                                              dtpArrivalTime.Value.ToString(dateformat)),
                                              notesDate: notesDt);
                    }
                }

                // For engineer assigned
                if (cmbEngineer.SelectedValue != null && (int)cmbEngineer.SelectedValue > 0)
                {
                    if (!IsEdit  || (IsEdit && sOrigServiceEntity.Service_Issued_To_Staff_ID < 0))
                            CreateServiceNote(string.Format("Job assigned to engineer - {0}", cmbEngineer.Text), notesDate: notesDt);
                }        
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void cmbCallStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsEdit && cmbCallStatus.ValueMember != string.Empty && cmbCallStatus.SelectedIndex >= 0  &&  sOrigCallStatus != CallStatus.CALL_STATUS_COMPLETED)
            {
                switch ((CallStatus)cmbCallStatus.SelectedValue)
                {
                    case CallStatus.CALL_STATUS_LOGGED:   // Logged
                        chkReceivedTime.Enabled = true;
                        chkPassedToEngrTime.Enabled = false; 
                        chkEngrAckTime.Enabled = false;
                        chkArrivalTime.Enabled = false;
                        chkCompletedTime.Enabled = false;
                        lblRemedy.Text = lblRemedy.Text.Replace("* ","").ToString();
                        lblRemedyNotes.Text = lblRemedyNotes.Text.Replace("* ", "").ToString();
                        break;

                    case CallStatus.CALL_STATUS_VIEWED:   // Viewed
                        chkReceivedTime.Enabled = false;
                        chkPassedToEngrTime.Enabled = false; 
                        chkEngrAckTime.Enabled = false;
                        chkArrivalTime.Enabled = false;
                        chkCompletedTime.Enabled = false;
                        lblRemedy.Text = lblRemedy.Text.Replace("* ","").ToString();
                        lblRemedyNotes.Text = lblRemedyNotes.Text.Replace("* ", "").ToString();
                        break;

                    case CallStatus.CALL_STATUS_AWAITING_RECIEPT: // Despatched
                        chkReceivedTime.Enabled = false;
                        chkPassedToEngrTime.Enabled = true; 
                        chkEngrAckTime.Enabled = false;
                        chkArrivalTime.Enabled = false;
                        chkCompletedTime.Enabled = false; 
                        lblRemedy.Text = lblRemedy.Text.Replace("* ","").ToString();
                        lblRemedyNotes.Text = lblRemedyNotes.Text.Replace("* ", "").ToString();
                        break;

                    case CallStatus.CALL_STATUS_ENGINEER_CONFIRMED:   //  Accepted
                        chkReceivedTime.Enabled = false;
                        chkPassedToEngrTime.Enabled = true; 
                        chkEngrAckTime.Enabled = true; 
                        chkArrivalTime.Enabled = false;
                        chkCompletedTime.Enabled = false;
                        lblRemedy.Text = lblRemedy.Text.Replace("* ","").ToString();
                        lblRemedyNotes.Text = lblRemedyNotes.Text.Replace("* ", "").ToString(); 
                        break;

                    case CallStatus.CALL_STATUS_AT_SITE:   // At Site
                        chkReceivedTime.Enabled = false;
                        chkPassedToEngrTime.Enabled = true; 
                        chkEngrAckTime.Enabled = true; 
                        chkArrivalTime.Enabled = true;
                        chkCompletedTime.Enabled = false; 
                        lblRemedy.Text = lblRemedy.Text.Replace("* ","").ToString();
                        lblRemedyNotes.Text = lblRemedyNotes.Text.Replace("* ", "").ToString();
                        break;

                    case CallStatus.CALL_STATUS_COMPLETED:   // Completed
                        chkReceivedTime.Enabled = false;
                        chkReceivedTime.Checked = false;
                        chkPassedToEngrTime.Enabled = true; 
                        chkEngrAckTime.Enabled = true; 
                        chkArrivalTime.Enabled = true; 
                        chkCompletedTime.Enabled = true;
                        lblRemedy.Text = "* " + lblRemedy.Text;
                        lblRemedyNotes.Text = "* " + lblRemedyNotes.Text;
                        break;

                    default: break;
                }
                
                //if (sOrigCallStatus != CallStatus.CALL_STATUS_COMPLETED && ((CallStatus)cmbCallStatus.SelectedValue) == CallStatus.CALL_STATUS_COMPLETED)
                //{
                //    chkCompletedTime.Enabled = true;
                //}
            }
        }

        private void chkPassedToEngrTime_CheckedChanged(object sender, EventArgs e)
        {
            SetDateForControl(dtpPassedToEngrTime);
        }

        private void chkEngrAckTime_CheckedChanged(object sender, EventArgs e)
        {
            SetDateForControl(dtpEngrAckTime);
        }

        private void chkArrivalTime_CheckedChanged(object sender, EventArgs e)
        {
            SetDateForControl(dtpArrivalTime);
        }

        private void chkCompletedTime_CheckedChanged(object sender, EventArgs e)
        {
            SetDateForControl(dtpCompletedTime);
        }

        private void chkReceivedTime_CheckedChanged(object sender, EventArgs e)
        {
            SetDateForControl(dtpReceivedTime);
        }

        private void SetDateForControl(DateTimePicker dtpControl)
        {
            dtpControl.Enabled = true;
            dtpControl.Value = DateTime.Now;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNote_Click(object sender, EventArgs e)
        {
            try
            {
                frmServiceNotesAdd frmAddNote = new frmServiceNotesAdd(Convert.ToInt32(sJobID), Convert.ToInt32(cmbEngineer.SelectedValue));
                frmAddNote.FormClosed += frmAddNote_FormClosed;
                frmAddNote.BringToFront();                
                frmAddNote.ShowDialog();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CreateServiceNote(string message, string subject = "", DateTime? notesDate = null, int? jobNo = null )
        {
            try
            {
                int engineerId = Convert.ToInt32(cmbEngineer.SelectedValue);
             
                // General note - if no engineer is selected
                ServiceCallBusiness.CreateInstance().InsertServiceNotes(
                                    string.IsNullOrEmpty(sJobID) ? jobNo.Value : Convert.ToInt32(sJobID), 
                                    AppGlobals.Current.UserId, 
                                    engineerId, 
                                    subject,
                                    message,
                                    notesDate.HasValue ? notesDate.Value : DateTime.Now, 
                                    engineerId > 0 ? (int)ServiceNotes.SERVICE_NOTES_IN_OUT_STAFF_TO_ENGINEER : (int)ServiceNotes.SERVICE_NOTES_IN_OUT_GENERAL);

                ServiceCallBusiness.CreateInstance().InsertNewAuditEntry(Audit.Transport.ModuleNameEnterprise.ServiceCalls, 
                                   string.Format("Service Note created - [To: {0} , Subject : {1} , Note : {2}]",
                                   cmbEngineer.Text, 
                                   subject, 
                                   message) , "Service Notes");

                FillListViewNotesWithNotes();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void frmAddNote_FormClosed(object sender, FormClosedEventArgs e)
        {
            FillListViewNotesWithNotes();
        }
    }
}
