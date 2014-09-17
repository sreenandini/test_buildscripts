using System;
using System.Windows.Forms;
using BMC.Common.ExceptionManagement;
using BMC.CoreLib.Win32;
using BMC.EnterpriseBusiness.Business.ServiceCalls;



namespace BMC.EnterpriseClient.Views.ServiceCalls
{
    public partial class frmManualServiceCall : Form
    {
        #region Private Data members
        
        private GMUFaultBusiness objGMUFaultBusiness = null;

        private BMC.EnterpriseClient.Helpers.Datawatcher objDataWatcher = null;
        #endregion

        #region Public Data members

        public bool IsRefresh = false;

        #endregion

        #region Constructors

        public frmManualServiceCall()
        {
            InitializeComponent();
            objDataWatcher = new BMC.EnterpriseClient.Helpers.Datawatcher(this);
           }

        #endregion

        #region Events

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string sFaultName = txtFaultName.Text.Trim();
                bool bToMail = cbToMail.Checked;

                if (string.IsNullOrEmpty(sFaultName))
                {

                    this.ShowErrorMessageBox("Please enter the Fault Name.");
                    this.txtFaultName.Focus();
                }
                else
                {
                    objGMUFaultBusiness.SaveNewFault(sFaultName, bToMail == true ? 1 : 0);
                    this.ShowInfoMessageBox("Fault Name saved successfully.");
                    objGMUFaultBusiness.InsertNewAuditEntry(Audit.Transport.ModuleNameEnterprise.GmuFaultEvents, this.Text, "New", "Fault Name:==>" + txtFaultName.Text);
                    IsRefresh = true;
                    objDataWatcher.DataModify = false;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void frmManualServiceCall_Load(object sender, EventArgs e)
        {
            try
            {
                objGMUFaultBusiness = GMUFaultBusiness.CreateInstance();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }   
}
