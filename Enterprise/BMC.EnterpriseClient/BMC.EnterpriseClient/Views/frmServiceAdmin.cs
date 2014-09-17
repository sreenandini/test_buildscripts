using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.CoreLib.Win32;
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

namespace BMC.EnterpriseClient.Views
{
    public partial class frmServiceAdmin : Form
    {
        ServiceAdminBusiness _serviceAdminBiz = ServiceAdminBusiness.CreateInstance();
        private const string ScreenName = "Service Admin => ";
        List<CallGroup> callGroupList = null;
        List<CallFault> callFaultList = null;
        List<CallRemedy> callRemedyList = null;
        List<CallSource> callSourceList = null;
        string descText = "[New Desc]";
        string refText = "[New Ref]";
        string faultText = "[New Fault]";
        string fixCodeText = "[New Fix Code]";
        string sourceText = "[New Source]";
        string yesText = "Yes";
        string noText = "No";
        CallGroup origCallGroup = null;
        CallFault origCallFault = null;
        CallRemedy origCallRemedy = null;
        CallSource origCallSource = null;

        public frmServiceAdmin()
        {
            InitializeComponent();

            txtGroupDesc.Width = 400;
            txtFaultDescription.Width = 400;

            chkGroupDowntime.Visible = SettingsEntity.IsServiceCallFeatureFull;
            chkCodeDowntime.Visible = SettingsEntity.IsServiceCallFeatureFull;
            if (!SettingsEntity.IsServiceCallFeatureFull)
            {
                clmGrpDownTime.Width = 0;
                clmCodeDowntime.Width = 0;
            }
        }

        #region "Events"
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void frmServiceAdmin_Load(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Form Load", LogManager.enumLogLevel.Info);

                // Load Fault Groups
                LoadFaultGroups();

                // Load Fix Codes
                LoadFixCodes();

                // Load Source
                LoadSource();

                // Enable buttons only if they have access
                bool _bIsEditPermissionEnabled =  AppGlobals.Current.HasUserAccess("HQ_Engineers_Engineers_Edit");
                EnableControls(_bIsEditPermissionEnabled);               

                // Select first item in Fault group
                if (callGroupList.Count > 0)
                    lvFaultGroups.Items[0].Selected = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }       

        #region "Fault Group"
        private void lvFaultGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Fault Group - SelectedIndexChanged", LogManager.enumLogLevel.Info);

                if (lvFaultGroups.SelectedItems.Count > 0)
                {
                    CallGroup group = lvFaultGroups.SelectedItems[0].Tag as CallGroup;
                    origCallGroup = group;

                    if (group != null)
                    {
                        // Load Fault Details
                        LoadFaultDetailsBasedOnGroup(group.Id);

                        txtGroupRef.Text = group.Reference;
                        txtGroupDesc.Text = group.Description;
                        chkGroupDowntime.Checked = group.Downtime;
                        //chkGroupLogEngr.Checked = group.LogEngineerChange;
                    }
                }
                else
                    origCallGroup = null;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnGroupNew_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Fault Group - New button click", LogManager.enumLogLevel.Info);

                ListViewItem itemMatched = lvFaultGroups.FindItemWithText(refText);
                
                if (itemMatched != null)
                {
                    this.ShowMessageBox("Please modify existing default row", "Service Admin", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }

                txtGroupRef.Text = refText;
                txtGroupDesc.Text = descText;

                // Add new group with default values and select it
                CallGroup grp = new CallGroup() { Reference = refText, Description = descText, Downtime = chkGroupDowntime.Checked };

                ListViewItem item = new ListViewItem(grp.Reference);
                item.Tag = grp;
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, grp.Description));
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, grp.Downtime ? yesText : noText));

                // Add item to list view & select
                lvFaultGroups.Items.Add(item);
                lvFaultGroups.Items[lvFaultGroups.Items.Count - 1].Selected = true;

                txtGroupRef.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnGroupRemove_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Fault Group - Remove button click", LogManager.enumLogLevel.Info);

                if (lvFaultGroups.SelectedItems.Count > 0)
                {
                    int grpId = (lvFaultGroups.SelectedItems[0].Tag as CallGroup).Id;

                    // CanRemoveCallFaultGroup - "Removal Failed : Fault Group is linked to Calls"
                    if (_serviceAdminBiz.CanRemoveCallFaultGroup(grpId) == false)
                    {
                        this.ShowMessageBox("Removal Failed : Fault Group is linked to Calls", "Service Admin", MessageBoxButtons.OK, MessageBoxIcon.None);
                        return;
                    }

                    string msg = lvFaultDetails.Items.Count > 0 ? "Faults are linked to the selected fault group.Are you sure you want to remove this fault group?" : "Are you sure you want to remove this fault group?";

                    DialogResult result = this.ShowMessageBox(msg, "Service Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (grpId != 0)
                            SaveGroupDetails(grpId, DateTime.Now);
                        else
                        {
                            // Load from DB
                            LoadFaultGroups();

                            if (lvFaultGroups.Items.Count > 0)
                                lvFaultGroups.Items[0].Selected = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnGroupApply_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Fault Group - Apply button click", LogManager.enumLogLevel.Info);

                if (lvFaultGroups.SelectedItems.Count > 0)
                {
                    // Should not have empty values or default values
                    if (string.IsNullOrEmpty(txtGroupRef.Text.Trim()) )
                    {
                        this.ShowMessageBox("Reference cannot be empty", "Service Admin", MessageBoxButtons.OK, MessageBoxIcon.None);
                        txtGroupRef.Focus();
                        return;
                    }
                    if (txtGroupRef.Text.Trim() == refText)
                    {
                        this.ShowMessageBox("Please provide valid value for reference", "Service Admin", MessageBoxButtons.OK, MessageBoxIcon.None);
                        txtGroupRef.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtGroupDesc.Text.Trim()))
                    {
                        this.ShowMessageBox("Description cannot be empty", "Service Admin", MessageBoxButtons.OK, MessageBoxIcon.None);
                        txtGroupDesc.Focus();
                        return;
                    }
                    if (txtGroupDesc.Text.Trim() == descText)
                    {
                        this.ShowMessageBox("Please provide valid value for description", "Service Admin", MessageBoxButtons.OK, MessageBoxIcon.None);
                        txtGroupDesc.Focus();
                        return;
                    }

                    CallGroup grp = callGroupList.Find(x => x.Description.ToLower().Equals(txtGroupDesc.Text.Trim().ToLower()));
                    if (grp != null && grp.Id != (lvFaultGroups.SelectedItems[0].Tag as CallGroup).Id)
                    {
                        this.ShowMessageBox("Group already exists with same description. Please provide a different value.", "Service Admin", MessageBoxButtons.OK, MessageBoxIcon.None);
                        txtGroupDesc.Focus();
                        return;
                    }

                    DialogResult result = this.ShowMessageBox("Do you want to apply the changes?", "Service Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        int grpId = (lvFaultGroups.SelectedItems[0].Tag as CallGroup).Id;
                        SaveGroupDetails(grpId);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion

        #region "Fault Details"
        private void lvFaultDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Fault Details - SelectedIndexChanged", LogManager.enumLogLevel.Info);

                if (lvFaultDetails.SelectedItems.Count > 0)
                {
                    CallFault fault = lvFaultDetails.SelectedItems[0].Tag as CallFault;
                    origCallFault = fault;

                    if (fault != null)
                    {
                        txtFaultReference.Text = fault.Reference;
                        txtFaultDescription.Text = fault.Description;
                    }
                }
                else
                {
                    origCallFault = null;
                    txtFaultReference.Text = "";
                    txtFaultDescription.Text = "";
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnFaultNew_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Fault Detail - New button click", LogManager.enumLogLevel.Info);
                int grpId = (lvFaultGroups.SelectedItems[0].Tag as CallGroup).Id;

                if(grpId == 0)
                {
                    this.ShowMessageBox("Please save Group details before adding Fault details", "Service Admin", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }

                ListViewItem itemMatched = lvFaultDetails.FindItemWithText(refText);
                if (itemMatched != null)
                {
                    this.ShowMessageBox("Please modify existing default row", "Service Admin", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }

                txtFaultReference.Text = refText;
                txtFaultDescription.Text = faultText;                

                // Add new fault with default values and select it
                CallFault fault = new CallFault() { GroupId = grpId, Reference = refText, Description = faultText };

                ListViewItem item = new ListViewItem(fault.Reference);
                item.Tag = fault;
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, fault.Description));

                // Add item to list view & select
                lvFaultDetails.Items.Add(item);
                lvFaultDetails.Items[lvFaultDetails.Items.Count - 1].Selected = true;

                txtFaultReference.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnFaultRemove_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Fault Detail - Remove button click", LogManager.enumLogLevel.Info);

                if (lvFaultGroups.SelectedItems.Count > 0)
                {
                    int grpId = (lvFaultGroups.SelectedItems[0].Tag as CallGroup).Id;
                    int faultId = (lvFaultDetails.SelectedItems[0].Tag as CallFault).Id;

                    // CanRemoveCallFault - "Removal Failed: Fault is linked to Calls"
                    if (_serviceAdminBiz.CanRemoveCallFaultDetail(faultId, grpId) == false)
                    {
                        this.ShowMessageBox("Removal Failed: Fault is linked to Calls", "Service Admin", MessageBoxButtons.OK, MessageBoxIcon.None);
                        return;
                    }

                    DialogResult result = this.ShowMessageBox("Are you sure you want to remove this fault code?", "Service Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {                      
                        if (faultId != 0)
                            SaveFaultDetails(faultId, grpId, DateTime.Now);
                        else
                        {
                            // Load from DB
                            LoadFaultDetailsBasedOnGroup(grpId);

                            if (lvFaultDetails.Items.Count > 0)
                                lvFaultDetails.Items[0].Selected = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnFaultApply_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Fault Detail - Apply button click", LogManager.enumLogLevel.Info);

                if (lvFaultDetails.SelectedItems.Count > 0)
                {
                    // Should not have empty values or default values
                    if (string.IsNullOrEmpty(txtFaultReference.Text.Trim()))
                    {
                        this.ShowMessageBox("Reference cannot be empty", "Service Admin", MessageBoxButtons.OK, MessageBoxIcon.None);
                        txtFaultReference.Focus();
                        return;
                    }
                    if (txtFaultReference.Text.Trim() == refText)
                    {
                        this.ShowMessageBox("Please provide valid value for reference", "Service Admin", MessageBoxButtons.OK, MessageBoxIcon.None);
                        txtFaultReference.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtFaultDescription.Text.Trim()))
                    {
                        this.ShowMessageBox("Description cannot be empty", "Service Admin", MessageBoxButtons.OK, MessageBoxIcon.None);
                        txtFaultDescription.Focus();
                        return;
                    }
                    if (txtFaultDescription.Text.Trim() == faultText)
                    {
                        this.ShowMessageBox("Please provide valid value for description", "Service Admin", MessageBoxButtons.OK, MessageBoxIcon.None);
                        txtFaultDescription.Focus();
                        return;
                    }

                    CallFault flt = callFaultList.Find(x => x.Description.ToLower().Equals(txtFaultDescription.Text.Trim().ToLower())); 
                    if (flt != null && flt.Id != (lvFaultDetails.SelectedItems[0].Tag as CallFault).Id)
                    {
                        this.ShowMessageBox("Fault already exists with same description. Please provide a different value.", "Service Admin", MessageBoxButtons.OK, MessageBoxIcon.None);
                        txtFaultDescription.Focus();
                        return;
                    }

                    DialogResult result = this.ShowMessageBox("Do you want to apply the changes?", "Service Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        int grpId = (lvFaultGroups.SelectedItems[0].Tag as CallGroup).Id;
                        int faultId = (lvFaultDetails.SelectedItems[0].Tag as CallFault).Id;
                        SaveFaultDetails(faultId, grpId);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion

        #region "Fix Codes"
        private void txtCodeRef_KeyPress(object sender, KeyPressEventArgs e)
        {
            LogManager.WriteLog(ScreenName + "Fix Code Reference Key Press event", LogManager.enumLogLevel.Info);

            // Check if the pressed character was a backspace or numeric.
            if (e.KeyChar != (char)8 && !char.IsNumber(e.KeyChar))
                e.Handled = true;           
        }

        private void lvFixCodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Fix Code - SelectedIndexChanged", LogManager.enumLogLevel.Info);

                if (lvFixCodes.SelectedItems.Count > 0)
                {
                    CallRemedy fixCode = lvFixCodes.SelectedItems[0].Tag as CallRemedy;
                    origCallRemedy = fixCode;

                    if (fixCode != null)
                    {
                        txtCodeRef.Text = fixCode.Reference.ToString();
                        txtCodeDesc.Text = fixCode.Description;
                    }
                }
                else
                    origCallRemedy = null;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnCodeNew_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Fix Code - New button click", LogManager.enumLogLevel.Info);

                ListViewItem itemMatched = lvFixCodes.FindItemWithText("0");

                if (itemMatched != null)
                {
                    this.ShowMessageBox("Please modify existing default row", "Service Admin", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }

                txtCodeRef.Text = "0";
                txtCodeDesc.Text = fixCodeText;

                // Add new fix code with default values and select it
                CallRemedy code = new CallRemedy() { Reference = 0, Description = fixCodeText, Downtime = chkCodeDowntime.Checked };

                ListViewItem item = new ListViewItem(code.Reference.ToString());
                item.Tag = code;
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, code.Description));
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, code.Downtime ? yesText : noText));

                // Add item to list view & select
                lvFixCodes.Items.Add(item);
                lvFixCodes.Items[lvFixCodes.Items.Count - 1].Selected = true;

                txtCodeRef.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnCodeRemove_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Fix Code - Remove button click", LogManager.enumLogLevel.Info);

                if (lvFixCodes.SelectedItems.Count > 0)
                {
                    int fixCodeId = (lvFixCodes.SelectedItems[0].Tag as CallRemedy).Id;

                    // CanRemoveCallRemedy - "Removal Failed : Fix Code is linked to Calls"
                    if (_serviceAdminBiz.CanRemoveCallRemedy(fixCodeId) == false)
                    {
                        this.ShowMessageBox("Removal Failed : Fix Code is linked to Calls", "Service Admin", MessageBoxButtons.OK, MessageBoxIcon.None);
                        return;
                    }

                    DialogResult result = this.ShowMessageBox("Are you sure you want to remove this fix code?", "Service Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (fixCodeId != 0)
                            SaveFixCode(fixCodeId, DateTime.Now);
                        else
                        {
                            // Load from DB
                            LoadFixCodes();

                            if (lvFixCodes.Items.Count > 0)
                                lvFixCodes.Items[0].Selected = true;
                        }
                    }
                 
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnCodeApply_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Fix Code - Apply button click", LogManager.enumLogLevel.Info);

                if (lvFixCodes.SelectedItems.Count > 0)
                {
                    // Should not have empty values or default values          
                    if (string.IsNullOrEmpty(txtCodeRef.Text.Trim()))
                    {
                        this.ShowMessageBox("Reference cannot be empty", "Service Admin", MessageBoxButtons.OK, MessageBoxIcon.None);
                        txtCodeRef.Focus();
                        return;
                    }
                    if (txtCodeRef.Text.Trim() == "0")
                    {
                        this.ShowMessageBox("Please provide valid value for reference", "Service Admin", MessageBoxButtons.OK, MessageBoxIcon.None);
                        txtCodeRef.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtCodeDesc.Text.Trim()))
                    {
                        this.ShowMessageBox("Description cannot be empty", "Service Admin", MessageBoxButtons.OK, MessageBoxIcon.None);
                        txtCodeDesc.Focus();
                        return;
                    }
                    if (txtCodeDesc.Text.Trim() == fixCodeText)
                    {
                        this.ShowMessageBox("Please provide valid value for description", "Service Admin", MessageBoxButtons.OK, MessageBoxIcon.None);
                        txtCodeDesc.Focus();
                        return;
                    }

                    CallRemedy remedy = callRemedyList.Find(x => x.Description.ToLower().Equals(txtCodeDesc.Text.Trim().ToLower()));
                    if (remedy != null && remedy.Id != (lvFixCodes.SelectedItems[0].Tag as CallRemedy).Id)                  
                    {
                        this.ShowMessageBox("Fix Code already exists with same description. Please provide a different value.", "Service Admin", MessageBoxButtons.OK, MessageBoxIcon.None);
                        txtCodeDesc.Focus();
                        return;
                    }


                    DialogResult result = this.ShowMessageBox("Do you want to apply the changes?", "Service Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        int fixCodeId = (lvFixCodes.SelectedItems[0].Tag as CallRemedy).Id;
                        SaveFixCode(fixCodeId);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
         #endregion

        #region "Source"
        private void lvSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Source - SelectedIndexChanged", LogManager.enumLogLevel.Info);

                if (lvSource.SelectedItems.Count > 0)
                {
                    CallSource source = lvSource.SelectedItems[0].Tag as CallSource;
                    origCallSource = source;

                    if (source != null)
                    {
                        txtSrcRef.Text = source.Reference;
                        txtSrcDesc.Text = source.Description;
                    }
                }
                else
                    origCallSource = null;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnSrcNew_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Source - New button click", LogManager.enumLogLevel.Info);

                ListViewItem itemMatched = lvSource.FindItemWithText(refText);

                if (itemMatched != null)
                {
                    this.ShowMessageBox("Please modify existing default row", "Service Admin", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }

                txtSrcRef.Text = refText;
                txtSrcDesc.Text = sourceText;

                // Add new source with default values and select it
                CallSource source = new CallSource() { Reference = refText, Description = sourceText };

                ListViewItem item = new ListViewItem(source.Reference.ToString());
                item.Tag = source;
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, source.Description));

                // Add item to list view & select
                lvSource.Items.Add(item);
                lvSource.Items[lvSource.Items.Count - 1].Selected = true;

                txtSrcRef.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnSrcApply_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Source - Apply button click", LogManager.enumLogLevel.Info);

                if (lvSource.SelectedItems.Count > 0)
                {                 
                    // Should not have empty values or default values          
                    if (string.IsNullOrEmpty(txtSrcRef.Text.Trim()))
                    {
                        this.ShowMessageBox("Reference cannot be empty", "Service Admin", MessageBoxButtons.OK, MessageBoxIcon.None);
                        txtSrcRef.Focus();
                        return;
                    }
                    if (txtSrcRef.Text.Trim() == refText)
                    {
                        this.ShowMessageBox("Please provide valid value for reference", "Service Admin", MessageBoxButtons.OK, MessageBoxIcon.None);
                        txtSrcRef.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtSrcDesc.Text.Trim()))
                    {
                        this.ShowMessageBox("Description cannot be empty", "Service Admin", MessageBoxButtons.OK, MessageBoxIcon.None);
                        txtSrcDesc.Focus();
                        return;
                    }
                    if (txtSrcDesc.Text.Trim() == sourceText)
                    {
                        this.ShowMessageBox("Please provide valid value for description", "Service Admin", MessageBoxButtons.OK, MessageBoxIcon.None);
                        txtSrcDesc.Focus();
                        return;
                    }

                    CallSource src = callSourceList.Find(x => x.Description.ToLower().Equals(txtSrcDesc.Text.Trim().ToLower()));
                    if (src != null && src.Id != (lvSource.SelectedItems[0].Tag as CallSource).Id)    
                    {
                        this.ShowMessageBox("Source already exists with same description. Please provide a different value.", "Service Admin", MessageBoxButtons.OK, MessageBoxIcon.None);
                        txtSrcDesc.Focus();
                        return;
                    }

                    DialogResult result = this.ShowMessageBox("Do you want to apply the changes?", "Service Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        int sourceId = (lvSource.SelectedItems[0].Tag as CallSource).Id;
                        SaveCallSource(sourceId);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion
        #endregion

        #region "Methods"
        #region "Load Methods"
        private void EnableControls(bool isEnabled)
        {
            txtGroupRef.ReadOnly = !isEnabled;
            txtGroupDesc.ReadOnly = !isEnabled;
            chkGroupDowntime.Enabled = isEnabled;
            //chkGroupLogEngr.Enabled = isEnabled;
            btnGroupNew.Enabled = isEnabled;
            btnGroupApply.Enabled = isEnabled;
            btnGroupRemove.Enabled = isEnabled;

            txtFaultDescription.ReadOnly = !isEnabled;
            txtFaultReference.ReadOnly = !isEnabled;
            btnFaultNew.Enabled = isEnabled;
            btnFaultApply.Enabled = isEnabled;
            btnFaultRemove.Enabled = isEnabled;

            txtCodeDesc.ReadOnly = !isEnabled;
            txtCodeRef.ReadOnly = !isEnabled;
            chkCodeDowntime.Enabled = isEnabled;
            btnCodeNew.Enabled = isEnabled;
            btnCodeApply.Enabled = isEnabled;
            btnCodeRemove.Enabled = isEnabled;

            txtSrcDesc.ReadOnly = !isEnabled;
            txtSrcRef.ReadOnly = !isEnabled;
            btnSrcNew.Enabled = isEnabled;
            btnSrcApply.Enabled = isEnabled;

        }

        private void LoadFaultGroups()
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Load Fault Groups list", LogManager.enumLogLevel.Info);

                // Clear existing list
                lvFaultGroups.Items.Clear();
                callGroupList = _serviceAdminBiz.GetCallGroups();

                ListViewItem item;
                foreach (CallGroup grp in callGroupList)
                {
                    item = new ListViewItem(grp.Reference);
                    item.Tag = grp;
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, grp.Description));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, (grp.Downtime ? yesText : noText)));

                    // Add items to listView
                    lvFaultGroups.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void LoadFaultDetailsBasedOnGroup(int groupId)
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Load Fault details based on selected groupId : " + groupId, LogManager.enumLogLevel.Info);

                // Clear existing list
                lvFaultDetails.Items.Clear();
                callFaultList = _serviceAdminBiz.GetCallFaultsByGroupId(groupId);

                ListViewItem item;
                foreach (CallFault fault in callFaultList)
                {
                    item = new ListViewItem(fault.Reference.ToString());
                    item.Tag = fault;
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, fault.Description));

                    // Add items to listView
                    lvFaultDetails.Items.Add(item);
                }

                if (callFaultList.Count > 0)
                    lvFaultDetails.Items[0].Selected = true;
                else
                {
                    txtFaultReference.Text = "";
                    txtFaultDescription.Text = "";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void LoadFixCodes()
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Load Fix Codes list", LogManager.enumLogLevel.Info);

                // Clear existing list
                lvFixCodes.Items.Clear();
                callRemedyList = _serviceAdminBiz.GetCallFixCodes();

                ListViewItem item;
                foreach (CallRemedy fixCode in callRemedyList)
                {
                    item = new ListViewItem(fixCode.Reference.ToString());
                    item.Tag = fixCode;
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, fixCode.Description));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, (fixCode.Downtime ? yesText : noText)));

                    // Add items to listView
                    lvFixCodes.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void LoadSource()
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Load Source list", LogManager.enumLogLevel.Info);

                // Clear existing list
                lvSource.Items.Clear();
                callSourceList = _serviceAdminBiz.GetCallSources();

                ListViewItem item;
                foreach (CallSource source in callSourceList)
                {
                    item = new ListViewItem(source.Reference);
                    item.Tag = source;
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, source.Description));

                    // Add items to listView
                    lvSource.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region "Save Methods"
        private void SaveGroupDetails(int grpId, DateTime? endDate = null)
        {
            try
            {
                CallGroup currentCallGroup = new CallGroup()
                {
                    Id = grpId,
                    Reference = txtGroupRef.Text.Trim(),
                    Description = txtGroupDesc.Text.Trim(),
                    Downtime = chkGroupDowntime.Checked,
                    //LogEngineerChange = chkGroupLogEngr.Checked,
                    EndDate = endDate
                };

                bool result = _serviceAdminBiz.InsertUpdateCallGroup(currentCallGroup);

                // Audit entry
                if (grpId == 0)
                {
                    _serviceAdminBiz.InsertNewAuditEntry(Audit.Transport.ModuleNameEnterprise.ServiceCalls,  "Call Group",
                                    string.Format("Reference: {0} , Description : {1}", txtGroupRef.Text.Trim(), txtGroupDesc.Text.Trim()));
                }
                else
                {
                    _serviceAdminBiz.AuditUpdateCallGroup(origCallGroup, currentCallGroup);
                }


                if (result)
                    LogManager.WriteLog(ScreenName + "Group details Updated Successfully Call_Group_ID:" + grpId, LogManager.enumLogLevel.Info);
                else
                    LogManager.WriteLog(ScreenName + "Group details Updation Failed Call_Group_ID:" + grpId, LogManager.enumLogLevel.Info);

                // Load Fault Groups
                LoadFaultGroups();

                if (lvFaultGroups.Items.Count > 0)
                    lvFaultGroups.Items[0].Selected = true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void SaveFaultDetails(int faultId, int grpId, DateTime? endDate = null)
        {
            try
            {
                CallFault currentCallFault = new CallFault()
                {
                    Id = faultId,
                    GroupId = grpId,
                    Reference = txtFaultReference.Text.Trim(),
                    Description = txtFaultDescription.Text.Trim(),
                    EndDate = endDate
                };

                bool result = _serviceAdminBiz.InsertUpdateCallFault(currentCallFault);

                if (result)
                    LogManager.WriteLog(ScreenName + "Fault details Updated Successfully Call_Fault_ID:" + faultId, LogManager.enumLogLevel.Info);
                else
                    LogManager.WriteLog(ScreenName + "Fault details Updation Failed Call_Fault_ID:" + faultId, LogManager.enumLogLevel.Info);

                // Audit entry
                if (faultId == 0)
                    _serviceAdminBiz.InsertNewAuditEntry(Audit.Transport.ModuleNameEnterprise.ServiceCalls, "Call Fault",
                                    string.Format("Reference: {0} , Description : {1}", txtFaultReference.Text.Trim(), txtFaultDescription.Text.Trim()));
                else
                    _serviceAdminBiz.AuditUpdateCallFault(origCallFault, currentCallFault);

                // Load Fault Details
                LoadFaultDetailsBasedOnGroup(grpId);

                if (lvFaultDetails.Items.Count > 0)
                    lvFaultDetails.Items[0].Selected = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SaveFixCode(int fixCodeId, DateTime? endDate = null)
        {
            try
            {
                CallRemedy currentCallRemedy = new CallRemedy()
                {
                    Id = fixCodeId,
                    Reference = Convert.ToInt32(txtCodeRef.Text.Trim()),
                    Description = txtCodeDesc.Text.Trim(),
                    Downtime = chkCodeDowntime.Checked,
                    EndDate = endDate
                };

                bool result = _serviceAdminBiz.InsertUpdateFixCode(currentCallRemedy);

                if (result)
                    LogManager.WriteLog(ScreenName + "Fix Code Updated Successfully Call_Remedy_ID:" + fixCodeId, LogManager.enumLogLevel.Info);
                else
                    LogManager.WriteLog(ScreenName + "Fix Code Updation Failed Call_Remedy_ID:" + fixCodeId, LogManager.enumLogLevel.Info);

                // Audit entry
                if (fixCodeId == 0)
                    _serviceAdminBiz.InsertNewAuditEntry(Audit.Transport.ModuleNameEnterprise.ServiceCalls, "Call Remedy",
                                    string.Format("Reference: {0} , Description : {1}", txtCodeRef.Text.Trim(), txtCodeDesc.Text.Trim()));
                else
                    _serviceAdminBiz.AuditUpdateCallRemedy(origCallRemedy, currentCallRemedy);

                // Load Fix Codes
                LoadFixCodes();

                if (lvFixCodes.Items.Count > 0)
                    lvFixCodes.Items[0].Selected = true;

            }
            catch (Exception ex)
            {
                throw;
            }
        }        

        private void SaveCallSource(int sourceId)
        {
            try
            {
                CallSource currentCallSource = new CallSource()
                                {
                                    Id = sourceId,
                                    Reference = txtSrcRef.Text.Trim(),
                                    Description = txtSrcDesc.Text.Trim()
                                };

                bool result = _serviceAdminBiz.InsertUpdateCallSource(currentCallSource);

                if (result)
                    LogManager.WriteLog(ScreenName + "Source Updated Successfully Call_Source_ID:" + sourceId, LogManager.enumLogLevel.Info);
                else
                    LogManager.WriteLog(ScreenName + "Source Updation Failed Call_Source_ID:" + sourceId, LogManager.enumLogLevel.Info);

                // Audit entry
                if (sourceId == 0)
                    _serviceAdminBiz.InsertNewAuditEntry(Audit.Transport.ModuleNameEnterprise.ServiceCalls, "Call Source",
                                    string.Format("Reference: {0} , Description : {1}", txtSrcRef.Text.Trim(), txtSrcDesc.Text.Trim()));
                else
                    _serviceAdminBiz.AuditUpdateCallSource(origCallSource, currentCallSource);

                // Load Fix Codes
                LoadSource();

                if (lvSource.Items.Count > 0)
                    lvSource.Items[0].Selected = true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        private void btnSrcRemove_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
