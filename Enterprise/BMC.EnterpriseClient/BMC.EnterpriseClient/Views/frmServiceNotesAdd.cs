using BMC.Common.ExceptionManagement;
using BMC.EnterpriseBusiness.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.CoreLib.Win32;
using BMC.EnterpriseBusiness.Entities;

namespace BMC.EnterpriseClient.Views
{
    public enum ServiceNotes
    {
        SERVICE_NOTES_IN_OUT_GENERAL = 0,
        SERVICE_NOTES_IN_OUT_ENGINEER_TO_STAFF,
        SERVICE_NOTES_IN_OUT_STAFF_TO_ENGINEER,
        SERVICE_NOTES_IN_OUT_ENGINEER_TO_STAFF_UNREAD
    }

    public partial class frmServiceNotesAdd : Form
    {
        int _jobId = 0;
        int _engineerId = 0;
        string noneText = "--NONE--";

        public frmServiceNotesAdd()
        {
            InitializeComponent();
        }

        public frmServiceNotesAdd(int jobId ,  int engineerId)
        {
            InitializeComponent();

            _jobId = jobId;
            _engineerId = engineerId;

            LoadEngineers();           
        }

        private void LoadEngineers()
        {
           cmbEngineer.DataSource = ServiceCallBusiness.CreateInstance().LoadEngineerNames(_engineerId, noneText);
           cmbEngineer.DisplayMember = "Description";
           cmbEngineer.ValueMember = "Id";
           cmbEngineer.SelectedIndex = cmbEngineer.Items.Count == 2 ? 1 : 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNotes.Text == string.Empty)
                {
                    this.ShowMessageBox("Notes cannot be empty", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }

                _engineerId = Convert.ToInt32(cmbEngineer.SelectedValue);
                
                ServiceCallBusiness.CreateInstance().InsertServiceNotes(_jobId, AppGlobals.Current.UserId, _engineerId, txtSubject.Text.Trim(),
                    txtNotes.Text.Trim(), DateTime.Now, _engineerId > 0 ? (int)ServiceNotes.SERVICE_NOTES_IN_OUT_STAFF_TO_ENGINEER : (int)ServiceNotes.SERVICE_NOTES_IN_OUT_GENERAL );

                ServiceCallBusiness.CreateInstance().InsertNewAuditEntry(Audit.Transport.ModuleNameEnterprise.ServiceCalls,
                                   string.Format("Service Note created - [To: {0} , Subject : {1} , Note : {2}]", ((EngineerEntityForService)cmbEngineer.SelectedItem).Description, txtSubject.Text.Trim(), txtNotes.Text.Trim()), "Service Notes");

                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void frmServiceNotesAdd_Load(object sender, EventArgs e)
        {
            this.ActiveControl =  txtSubject;            
        }
    }
}
