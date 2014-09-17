using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common;
using Audit.Transport;
using BMC.EnterpriseClient.Helpers.ExtensionsMethods;
using BMC.CoreLib.Win32;

namespace BMC.EnterpriseClient.Views
{


    #region Class
    public partial class frmTermsProfilesMachines : Form
    {
        #region Private Members
        private List<UnAssignedMachineTypes> _unAssignedMachineTypes = null;
        private TermsProfilesBusiness _termsProfilesBusiness = null;
        private int _termGroupID = 0;
        private int _termProfilesID = 0;
        private string _type = string.Empty;
        private string termsProfileParent;
        private string termsGroupSelected;
        #endregion Private Members

        #region Constructor
        public frmTermsProfilesMachines()
        {
            InitializeComponent();
            SetTagProperty();
            _termsProfilesBusiness = TermsProfilesBusiness.CreateInstance();
        }

        public frmTermsProfilesMachines(int termGroupID, int termProfilesID, string type, string termsProfileSelected, string termsGroupSelected)
        {
            InitializeComponent();
            SetTagProperty();
            _termsProfilesBusiness = TermsProfilesBusiness.CreateInstance();

            _termGroupID = termGroupID;
            _termProfilesID = termProfilesID;
            _type = type;
            termsProfileParent = termsProfileSelected;
            this.termsGroupSelected = termsGroupSelected;
        }
        #endregion Constructor

        #region Events
        private void frmTermsProfilesMachines_Load(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside frmTersmsProfiles_Load...", LogManager.enumLogLevel.Info);
                this.ResolveResources();
                LoadMachineTypes();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int selectedMachineTypeID;

            try
            {
                LogManager.WriteLog("Inside btnOk_Click...", LogManager.enumLogLevel.Info);

                if (lstAvailableMachineTypes.SelectedIndex >= 0)
                {
                    selectedMachineTypeID = Convert.ToInt32(lstAvailableMachineTypes.SelectedValue.ToString());
                    _termsProfilesBusiness.AddOrCopyTermsProfileForMachineTypes(selectedMachineTypeID, _termGroupID, _termProfilesID, _type);

                    //audit changes
                    new Audit_History()
                        .AddEntry()
                        .SetModule(ModuleNameEnterprise.SGVIFinancial)
                        .SetOperationType(OperationType.ADD)
                        .SetScreen("Terms|Machines")
                        .SetField("Machine_Type_ID")
                        .SetNewValue(selectedMachineTypeID.ToString())
                        .SetDescription(
                            _type.Equals("Copy") ? 
                            "Terms Group '" + termsGroupSelected + "' modified. Terms Profile '" + termsProfileParent + "' shares information and other details copied to new Terms Profile '" + lstAvailableMachineTypes.Text + "'..[Machine_Type_ID]: " + selectedMachineTypeID.ToString() :
                            "Terms Group '" + termsGroupSelected + "' modified. Added Terms Profile '" + lstAvailableMachineTypes.Text + "' ..[Machine_Type_ID]: " + selectedMachineTypeID.ToString())
                        .InsertEntry();
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        #endregion Events

        #region Private Methods
        private void SetTagProperty()
        {
            this.btnOk.Tag = "Key_OKCaption";
            this.btnCancel.Tag = "Key_Cancel";
            this.Tag = "Key_AvailableMachineTypes";
        }

        private void LoadMachineTypes()
        {
            try
            {
                LogManager.WriteLog("Inside LoadMachineTypes...", LogManager.enumLogLevel.Info);
                _unAssignedMachineTypes = _termsProfilesBusiness.GetUnAssignedMachineTypes(_termGroupID);

                var machineDefault = (from n in _unAssignedMachineTypes
                                      where n.Machine_Type_ID == 0
                                      select n).FirstOrDefault();

                if (machineDefault != null)
                    _unAssignedMachineTypes.Insert(0, new UnAssignedMachineTypes { Machine_Type_Code = "Default" });

                lstAvailableMachineTypes.DataSource = _unAssignedMachineTypes;
                lstAvailableMachineTypes.DisplayMember = "Machine_Type_Code";
                lstAvailableMachineTypes.ValueMember = "Machine_Type_ID";

                if (_unAssignedMachineTypes.Count <= 0)
                {
                    Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_NEWPROFILES_NOT_AVAILABLE"));
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.Close();
                }
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

