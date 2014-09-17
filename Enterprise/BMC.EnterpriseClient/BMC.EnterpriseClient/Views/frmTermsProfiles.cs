namespace BMC.EnterpriseClient
{
    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using Audit.Transport;
    using BMC.Common;
    using BMC.Common.ExceptionManagement;
    using BMC.Common.LogManagement;
    using BMC.CoreLib.Win32;
    using BMC.EnterpriseBusiness.Business;
    using BMC.EnterpriseBusiness.Entities;
    using BMC.EnterpriseClient.Helpers.ExtensionsMethods;
    using BMC.EnterpriseClient.Views;
    #endregion Namespaces

    #region Class
    public partial class frmTersmsProfiles : Form
    {
        #region Private Members
        private TermsProfilesBusiness _termsProfilesBusiness = null;
        private List<TermsGroupResult> _lstTermsGroupResult = null;
        private List<TermsProfilesEntity> _lstTermsProfilesEntity = null;
        private string oldTermsVariance;
        private string oldValidateTerms;
        private int defaultMachineID = 0;
        private string addType = string.Empty;

        private int SelectedTermsGroup { get { return Convert.ToInt32(lstTermsGroups.SelectedValue.ToString()); } }

        private int SelectedTermsProfile { get { return Convert.ToInt32(lstTermsProfiles.SelectedValue.ToString()); } }

        #endregion Private Members

        #region Constructor
        public frmTersmsProfiles()
        {
            InitializeComponent();
            SetTagProperty();
            _termsProfilesBusiness = TermsProfilesBusiness.CreateInstance();
        }
        #endregion Constructor

        #region Events
        private void frmTersmsProfiles_Load(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside frmTersmsProfiles_Load...", LogManager.enumLogLevel.Info);
                this.ResolveResources();
                LoadTermsGroup();
                oldTermsVariance = txtTermsVariance.Text = AdminBusiness.GetSetting("ImportTermsVariance", string.Empty);
                chkUseTermsValidation.Checked = Convert.ToBoolean(oldValidateTerms = string.IsNullOrEmpty(AdminBusiness.GetSetting("ImportValidateTerms", string.Empty)) ? "false" : AdminBusiness.GetSetting("ImportValidateTerms", string.Empty));

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnTermsGroupsNew_Click(object sender, EventArgs e)
        {
            int? _termsGroupID = 0;
            string _termsGroupName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside btnTermsGroupsNew_Click...", LogManager.enumLogLevel.Info);

                frmInputBox inputBox = new frmInputBox(this.GetResourceTextByKey(1, "MSG_ENTER_NAME_NEW_TERMS"), this.GetResourceTextByKey(1, "MSG_ENTER_NAME_NEW_TERMS"),
                    this.GetResourceTextByKey(1, "MSG_NEWTERMS_GROUP"));
                inputBox.ShowDialog();

                _termsGroupName = string.IsNullOrEmpty(inputBox.TextValue) ? string.Empty : inputBox.TextValue.Trim();

                if (!string.IsNullOrEmpty(_termsGroupName))
                {
                    if (_termsGroupName.Length > 50)
                    {
                        Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_TERMGRP_NAME_MORE_THAN_50CHARS"));
                        return;
                    }

                    var termsGroupAlreadyExists = from termsGroup in _lstTermsGroupResult
                                                  where termsGroup.Terms_Group_Name == _termsGroupName
                                                  select termsGroup;

                    if (termsGroupAlreadyExists.Count() > 0)
                    {
                        Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_TERMGRP_ALREADY_EXISTS"));
                        return;
                    }

                    _termsProfilesBusiness.InsertOrUpdateTermsGroup(_termsGroupName, null, ref _termsGroupID);

                    new Audit_History()
                        .AddEntry()
                        .SetModule(ModuleNameEnterprise.SGVIFinancial)
                        .SetField("Terms_Group_Name")
                        .SetDescription("Terms group '" + _termsGroupName + "' added ..[Terms_Group_Name]: " + _termsGroupName)
                        .SetScreen("Profile")
                        .SetOperationType(OperationType.ADD)
                        .SetNewValue(_termsGroupName)
                        .InsertEntry();

                    _termsProfilesBusiness.InsertTermsProfiles(_termsGroupID.Value, defaultMachineID);

                    new Audit_History()
                        .AddEntry()
                        .SetModule(ModuleNameEnterprise.SGVIFinancial)
                        .SetField("Machine_Type_ID")
                        .SetDescription("Terms group '" + _termsGroupName + "' modified. Added profile 'Default' ..[Machine_Type_ID]: " + defaultMachineID.ToString())
                        .SetScreen("Profile")
                        .SetOperationType(OperationType.ADD)
                        .SetNewValue(defaultMachineID.ToString())
                        .InsertEntry();

                    LoadTermsGroup();

                    lstTermsGroups.SelectedIndex = lstTermsGroups.Items.Cast<TermsGroupResult>().ToList<TermsGroupResult>().FindIndex(x => x.Terms_Group_Name.Equals(_termsGroupName));

                    Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_TERMS_GROUP") + _termsGroupName + this.GetResourceTextByKey(1, "MSG_ADD_SUCCESS"));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnTermsGroupsCopy_Click(object sender, EventArgs e)
        {
            int _termsGroupToCopy = 0;
            string _newTermsGroupName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside btnTermsGroupsCopy_Click...", LogManager.enumLogLevel.Info);

                if (!string.IsNullOrEmpty(lstTermsGroups.SelectedValue.ToString()))
                {
                    _termsGroupToCopy = Convert.ToInt32(lstTermsGroups.SelectedValue.ToString());

                    frmInputBox inputBox = new frmInputBox(this.GetResourceTextByKey(1, "MSG_ENTER_NAME_TERMS_GROUP"), this.GetResourceTextByKey(1, "MSG_ENTER_NAME_TERMS_GROUP"),
                        this.GetResourceTextByKey(1, "MSG_ENTER_NAME_NEW_TERMS"));
                    inputBox.ShowDialog();
                    _newTermsGroupName = string.IsNullOrEmpty(inputBox.TextValue) ? string.Empty : inputBox.TextValue.Trim();

                    if (!string.IsNullOrEmpty(_newTermsGroupName))
                    {
                        if (_newTermsGroupName.Length > 50)
                        {
                            Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_TERMGRP_NAME_MORE_THAN_50CHARS"));
                            return;
                        }

                        var termsGroupAlreadyExists = _lstTermsGroupResult.FirstOrDefault(x => x.Terms_Group_Name.Equals(_newTermsGroupName));

                        if (termsGroupAlreadyExists != null)
                        {
                            Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_TERMGRP_ALREADY_EXISTS"));
                            return;
                        }

                        _termsProfilesBusiness.CopyTermsGroupProfile(_newTermsGroupName, _termsGroupToCopy);

                        new Audit_History()
                            .AddEntry()
                            .SetModule(ModuleNameEnterprise.SGVIFinancial)
                            .SetField("Terms_Group_Name")
                            .SetNewValue(_newTermsGroupName)
                            .SetDescription("Terms group '" + lstTermsGroups.Text + "' shares information and other details copied to new terms group '" + _newTermsGroupName + "' ..[Terms_Group_Name]: "+_newTermsGroupName)
                            .SetScreen("Profile")
                            .SetOperationType(OperationType.ADD)
                            .InsertEntry();

                        LoadTermsGroup();

                        Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_TERMS_GROUP") + _newTermsGroupName + " " + this.GetResourceTextByKey(1, "MSG_COPY_SUCCESS"));
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnTermsGroupsDelete_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnTermsGroupsDelete_Click...", LogManager.enumLogLevel.Info);

                int selectedGrpId = Convert.ToInt32(lstTermsGroups.SelectedValue.ToString());
                BMC.EnterpriseDataAccess.EnterpriseDataContext.InstallationCount termsGroupInstallationCount = null;
                if (lstTermsGroups.SelectedIndex >= 0)
                    termsGroupInstallationCount = _termsProfilesBusiness.GetTermsGroupCountForInstallation(selectedGrpId);
                if (termsGroupInstallationCount != null && termsGroupInstallationCount.active > 0)
                {
                    Win32Extensions.ShowInfoMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_SELECTED_TERMSGROUP_INUSE"), termsGroupInstallationCount.active, termsGroupInstallationCount.active, termsGroupInstallationCount.inactive));
                    return;
                }
                else
                {
                    if (Win32Extensions.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_WANT_TO_REMOVE_GROUP")) == DialogResult.Yes)
                    {
                        if (Win32Extensions.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_ACTION_CANNOTBE_REVERSED")) == DialogResult.Yes)
                        {
                            _termsProfilesBusiness.DeleteTermsGroup(SelectedTermsGroup);
                            new Audit_History()
                                .AddEntry()
                                .SetModule(ModuleNameEnterprise.SGVIFinancial)
                                .SetField("Terms_Group_ID")
                                .SetDescription("Terms group '" + lstTermsGroups.Text + "' deleted ..[Terms_Group_ID]: " + SelectedTermsGroup.ToString())
                                .SetScreen("Profile")
                                .SetOldValue(SelectedTermsGroup.ToString())
                                .SetOperationType(OperationType.DELETE)
                                .InsertEntry();
                        }
                    }
                    LoadTermsGroup();
                    if (lstTermsGroups.Items.Count > 0)
                    {
                        LoadAllProfilesForSelectedGroup(SelectedTermsGroup);
                    }
                    else
                    {
                        lstTermsProfiles.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnTermsGroupsRename_Click(object sender, EventArgs e)
        {
            int? termsGroupID = 0;
            int? newtermsGroupID = 0;
            string _newTermsGroupName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside btnTermsGroupsRename_Click...", LogManager.enumLogLevel.Info);

                if (lstTermsGroups.SelectedIndex >= 0)
                {
                    frmInputBox inputBox = new frmInputBox(this.GetResourceTextByKey(1, "MSG_ENTER_NEW_GROUP"), this.GetResourceTextByKey(1, "MSG_ENTER_NEW_GROUP"), (lstTermsGroups.SelectedItem as TermsGroupResult).Terms_Group_Name);
                    inputBox.ShowDialog();
                    _newTermsGroupName = string.IsNullOrEmpty(inputBox.TextValue) ? string.Empty : inputBox.TextValue.Trim();

                    termsGroupID = Convert.ToInt32(lstTermsGroups.SelectedValue.ToString());

                    if (!string.IsNullOrEmpty(_newTermsGroupName))
                    {
                        if (_newTermsGroupName.Length > 50)
                        {
                            Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_TERMGRP_NAME_MORE_THAN_50CHARS"));
                            return;
                        }

                        var termsGroupAlreadyExists = from termsGroup in _lstTermsGroupResult
                                                      where termsGroup.Terms_Group_Name == _newTermsGroupName
                                                      select termsGroup;

                        if (termsGroupAlreadyExists.Count() > 0)
                        {
                            Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_NAME_IN_USE"));
                            return;
                        }

                        _termsProfilesBusiness.InsertOrUpdateTermsGroup(_newTermsGroupName, SelectedTermsGroup, ref newtermsGroupID);

                        new Audit_History()
                            .AddEntry()
                            .SetModule(ModuleNameEnterprise.SGVIFinancial)
                            .SetOldValue(lstTermsGroups.Text)
                            .SetField("Terms_Group_Name")
                            .SetNewValue(_newTermsGroupName)
                            .SetDescription("Renamed terms group '" + lstTermsGroups.Text + "' ..[Terms_Group_Name]: " + lstTermsGroups.Text + " --> " + _newTermsGroupName)
                            .SetScreen("Profile")
                            .SetOperationType(OperationType.MODIFY)
                            .InsertEntry(!lstTermsGroups.Text.Equals( _newTermsGroupName));

                        Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_TERMS_GROUP") + (lstTermsGroups.SelectedItem as TermsGroupResult).Terms_Group_Name + " " +
    this.GetResourceTextByKey(1, "MSG_RENAMED") + _newTermsGroupName);

                        LoadTermsGroup();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnTermsProfilesNew_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnTermsProfilesCopy_Click...", LogManager.enumLogLevel.Info);

                if (lstTermsProfiles.SelectedIndex >= 0)
                {
                    using (frmTermsProfilesMachines frmTermProfileMachine = new frmTermsProfilesMachines(SelectedTermsGroup, SelectedTermsProfile, "Add", lstTermsProfiles.Text, lstTermsGroups.Text))
                    {
                        if (frmTermProfileMachine.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            //MessageBox.Show("Terms Profile " + SelectedTermsGroup + " added successfully", "Bally MultiConnect - Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadProfilesForSelectedGroup(SelectedTermsGroup);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnTermsProfilesCopy_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnTermsProfilesCopy_Click...", LogManager.enumLogLevel.Info);

                if (lstTermsProfiles.SelectedIndex >= 0)
                {
                    using (frmTermsProfilesMachines frmTermProfileMachine = new frmTermsProfilesMachines(SelectedTermsGroup, SelectedTermsProfile, "Copy", lstTermsProfiles.Text, lstTermsGroups.Text))
                    {
                        if (frmTermProfileMachine.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            //MessageBox.Show("Terms Profile " + SelectedTermsGroup + " copied successfully", "Bally MultiConnect - Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LoadProfilesForSelectedGroup(SelectedTermsGroup);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnTermsProfilesEdit_Click(object sender, EventArgs e)
        {
            int termProfileID = 0;

            try
            {
                LogManager.WriteLog("Inside btnTermsProfilesEdit_Click...", LogManager.enumLogLevel.Info);

                termProfileID = Convert.ToInt32(lstTermsProfiles.SelectedValue.ToString());
                frmTerms frmTerm = new frmTerms(termProfileID, lstTermsGroups.Text, lstTermsProfiles.Text);
                frmTerm.ShowDialog();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnTermsProfilesDelete_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnTermsProfilesDelete_Click...", LogManager.enumLogLevel.Info);

                if (lstTermsProfiles.SelectedIndex >= 0)
                {
                    if (lstTermsProfiles.SelectedIndex == 0)
                    {
                        Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CANNOT_DELETE_DEFAULT_TERMS"));
                    }
                    else
                    {
                        if (Win32Extensions.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_WANT_TO_REMOVE_SELECTED_TERMS")) == DialogResult.Yes)
                        {
                            _termsProfilesBusiness.DeleteTermsProfiles(SelectedTermsProfile);
                            new Audit_History()
                                .AddEntry()
                                .SetModule(ModuleNameEnterprise.SGVIFinancial)
                                .SetField("Terms_Profile_Id")
                                .SetOldValue(SelectedTermsProfile.ToString())
                                .SetDescription("Terms Group '" + lstTermsGroups.Text + "' modified. Deleted Terms Profile '" + lstTermsProfiles.Text + "'")
                                .SetScreen("Profile")
                                .SetOperationType(OperationType.DELETE)
                                .InsertEntry();
                            Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_TERMS_PROFILE") + (lstTermsProfiles.SelectedItem as TermsProfilesEntity).Machine_Type_Code
                                                        + this.GetResourceTextByKey(1, "MSG_DELETE_SUCCESS"));
                            LoadAllProfilesForSelectedGroup(Convert.ToInt32(lstTermsGroups.SelectedValue.ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnSet_Click...", LogManager.enumLogLevel.Info);
                if (string.IsNullOrEmpty(txtTermsVariance.Text.Trim()))
                {
                    Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_TERMS_VARIANCE_NO_VALUE"));
                    return;
                }

                float? value = Convert.ToSingle(txtTermsVariance.Text);

                AGSBusiness agsBusiness = new AGSBusiness();
                agsBusiness.InsertOrUpdateSetting("ImportTermsVariance", value.FormatNumber<float>("###0.00"));

                new Audit_History()
                    .AddEntry()
                    .SetModule(ModuleNameEnterprise.SGVIFinancial)
                    .SetField("ImportTermsVariance")
                    .SetOldValue(oldTermsVariance)
                    .SetNewValue(value.FormatNumber<float>("###0.00"))
                    .SetDescription("All Terms group settings modified ..[ImportTermsVariance]: " + oldTermsVariance + " --> " + value.FormatNumber<float>("###0.00"))
                    .SetScreen("Profile")
                    .SetOperationType(OperationType.MODIFY)
                    .InsertEntry(!oldTermsVariance.Equals(value.FormatNumber<float>("###0.00")));

                agsBusiness.InsertOrUpdateSetting("ImportValidateTerms", chkUseTermsValidation.Checked.ToString());

                new Audit_History()
                    .AddEntry()
                    .SetModule(ModuleNameEnterprise.SGVIFinancial)
                    .SetField("ImportValidateTerms")
                    .SetOldValue(oldValidateTerms)
                    .SetNewValue(chkUseTermsValidation.Checked.ToString())
                    .SetDescription("All Terms group settings modified ..[ImportValidateTerms]: " + oldValidateTerms + " --> " + chkUseTermsValidation.Checked.ToString())
                    .SetScreen("Profile")
                    .SetOperationType(OperationType.MODIFY)
                    .InsertEntry(!oldValidateTerms.Equals(chkUseTermsValidation.Checked.ToString()));

                Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_TERMS_VARIANCE_UPDATED"));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnSetBarPositions_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnSetBarPositions_Click...", LogManager.enumLogLevel.Info);

                frmTermsSetBarPositions frmTermsSetBarPositions = new frmTermsSetBarPositions();
                frmTermsSetBarPositions.ShowDialog();
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

        private void lstTermsGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedGroupId = 0;

            try
            {
                LogManager.WriteLog("Inside lstTermsGroups_SelectedIndexChanged...", LogManager.enumLogLevel.Info);

                if (!string.IsNullOrEmpty(lstTermsGroups.SelectedValue.ToString()))
                {
                    selectedGroupId = Convert.ToInt32(lstTermsGroups.SelectedValue.ToString());
                    LoadProfilesForSelectedGroup(selectedGroupId);
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
            this.lblTermsGroups.Tag = "Key_TermsGroups";
            this.btnTermsGroupsRename.Tag = "Key_Rename";
            this.btnTermsGroupsCopy.Tag = "Key_Copy";
            this.btnTermsGroupsNew.Tag = "Key_NewCaption";
            this.btnTermsGroupsDelete.Tag = "Key_DeleteCaption";
            this.lblTermsProfiles.Tag = "Key_TermsProfiles";
            this.btnTermsProfilesNew.Tag = "Key_NewCaption";
            this.btnTermsProfilesCopy.Tag = "Key_Copy";
            this.btnTermsProfilesEdit.Tag = "Key_EditCaption";
            this.btnTermsProfilesDelete.Tag = "Key_DeleteCaption";
            this.btnClose.Tag = "Key_CloseCaption";
            this.btnSetBarPositions.Tag = "Key_SetBarPositions";
            this.btnSet.Tag = "Key_Set";
            this.lblVarianceAllowed.Tag = "Key_VarianceAllowedonAMEDISImport";
            this.lblUseTermsValidation.Tag = "Key_UseTermsValidion";
            this.Tag = "Key_CurrentTermsProfiles";
        }
        private void LoadTermsGroup()
        {
            try
            {
                LogManager.WriteLog("Inside LoadTermsGroup...", LogManager.enumLogLevel.Info);

                _lstTermsGroupResult = _termsProfilesBusiness.GetTermsGroup();
                lstTermsGroups.DataSource = _lstTermsGroupResult;
                lstTermsGroups.DisplayMember = "Terms_Group_Name";
                lstTermsGroups.ValueMember = "Terms_Group_ID";
                lstTermsGroups.SelectedIndex = 0;
                LoadProfilesForSelectedGroup(Convert.ToInt32(lstTermsGroups.SelectedValue.ToString()));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadProfilesForSelectedGroup(int selectedGroupId)
        {
            try
            {
                LogManager.WriteLog("Inside LoadProfilesForSelectedGroup...", LogManager.enumLogLevel.Info);

                _lstTermsProfilesEntity = _termsProfilesBusiness.GetTermsProfileResultForGroupID(selectedGroupId);
                var termsProfileEntity = (from n in _lstTermsProfilesEntity
                                          where n.Machine_Type_ID == 0
                                          select n).FirstOrDefault();
                if (termsProfileEntity != null)
                {
                    if (termsProfileEntity.Machine_Type_ID == 0)
                        termsProfileEntity.Machine_Type_Code = "Default";
                }

                lstTermsProfiles.DataSource = _lstTermsProfilesEntity;
                lstTermsProfiles.DisplayMember = "Machine_Type_Code";
                lstTermsProfiles.ValueMember = "Terms_Profile_ID";

                if (_lstTermsProfilesEntity.Count == 0)
                    lstTermsProfiles.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadAllProfilesForSelectedGroup(int selectedGrpId)
        {
            try
            {
                LogManager.WriteLog("Inside LoadAllProfilesForSelectedGroup...", LogManager.enumLogLevel.Info);

                var termsEntity = _termsProfilesBusiness.GetTermsProfileResultForGroupID(selectedGrpId);
                var termsProfileEntity = (from n in termsEntity
                                          where n.Machine_Type_ID == 0
                                          select n).FirstOrDefault();
                if (termsProfileEntity != null)
                {
                    if (termsProfileEntity.Machine_Type_ID == 0)
                        termsProfileEntity.Machine_Type_Code = "Default";
                }

                lstTermsProfiles.DataSource = termsEntity;
                lstTermsProfiles.DisplayMember = "Machine_Type_Code";
                lstTermsProfiles.ValueMember = "Terms_Profile_ID";

                if (termsEntity.Count == 0)
                    lstTermsProfiles.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion Private Methods
    }

    #endregion Class
}


