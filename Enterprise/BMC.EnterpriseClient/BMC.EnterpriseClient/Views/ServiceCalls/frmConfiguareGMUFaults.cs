using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseBusiness.Business.ServiceCalls;
using BMC.EnterpriseBusiness.Entities.ServiceCalls;
using BMC.EnterpriseClient.Helpers;
using System.Linq;

namespace BMC.EnterpriseClient.Views.ServiceCalls
{
    public partial class frmConfiguareGMUFaults : Form
    {
        #region Private Data Members
        private Datawatcher objDataWatcher = null;

        private GMUFaultBusiness objGMUFaultBusiness = null;
        private GMUConfigurationEntity sSelectedConfiguration = null;
        private int _count=0;
        private List<string> _OldValues = new List<string>();
        private List<string> _NewValues = new List<string>();

        #endregion Private Variables

        #region Constructors

        public frmConfiguareGMUFaults()
        {
            InitializeComponent();
            //btnNew.Enabled = AppGlobals.Current.HasUserAccess("HQ_GMU_Events_Add");
            //btnEdit.Enabled = AppGlobals.Current.HasUserAccess("HQ_GMU_Events_Edit");
            //btnUpdate.Enabled = AppGlobals.Current.HasUserAccess("HQ_GMU_Events_Edit");

            objDataWatcher = new Datawatcher(this,
                (w, f) =>
                {
                    w.RemoveControlFromWatcher((f as frmConfiguareGMUFaults).cmbFaultGroup);
                    w.RemoveControlFromWatcher((f as frmConfiguareGMUFaults).cmbFault);  
                });
        }

        #endregion

        #region Events

        /// <summary>
        /// This event is responsible to launch New Fault Creation screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                LoadNewManualService();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// This event is responsible to launch Mail configuration screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfiguareMail_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbAutoCreateMail.Checked)
                {
                    if (sSelectedConfiguration != null)
                    {
                        frmConfigureMailRecipients frmMailRecipiants = new frmConfigureMailRecipients(sSelectedConfiguration.Datapak_Fault_ID,
                            sSelectedConfiguration.Mail_CC,
                            sSelectedConfiguration.Mail_TO);
                        frmMailRecipiants.ShowDialog();
                        //GMU Fault Configuration display grid is required to refresh once the the mail has configured.
                        if (frmMailRecipiants.IsRefresh)
                        {
                            LoadGMUFaultConfiguration();
                        }
                    }
                }
                else
                {
                    this.ShowErrorMessageBox("Can not configure the mail recipients as the mail option is not set.");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void frmConfiguareGMUFaults_Load(object sender, EventArgs e)
        {
            try
            {
                objGMUFaultBusiness = GMUFaultBusiness.CreateInstance();

                //Populating fault group data in Fault group combobox.
                PopulateFaultGroup();

                //Populating fault events in Event type combobox.
                PopulateFaultEvents();

                //default settings needs to be done on first time loading.
                IsGMUConfigurationEditable(false);
                objDataWatcher.DataModify = false;
                StoreLoadvalue();


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void StoreLoadvalue()
        {
            try
            {

                _OldValues.Add(txtDescription.Text);
                _OldValues.Add(cbAutoCloseService.Checked.ToString());
                _OldValues.Add(cbAutoCreateMail.Checked.ToString());
                _OldValues.Add(cbAutoCreateServiceCall.Checked.ToString());
                _OldValues.Add(cmbEventType.Text);
                _OldValues.Add(cmbFault.Text);
                _OldValues.Add(cmbFaultGroup.Text);

           
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }

        }
        private void StoreNewvalue()
        {
            try
            {

                _NewValues.Add(txtDescription.Text);
                _NewValues.Add(cbAutoCloseService.Checked.ToString());
                _NewValues.Add(cbAutoCreateMail.Checked.ToString());
                _NewValues.Add(cbAutoCreateServiceCall.Checked.ToString());
                _NewValues.Add(cmbEventType.Text);
                _NewValues.Add(cmbFault.Text);
                _NewValues.Add(cmbFaultGroup.Text);


            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }

        }


        /// <summary>
        /// This event handler is used to populate the fault list available under selected fault group.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbFaultGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = cmbFaultGroup.SelectedIndex;
                cmbFault.DataSource = null;
                if (selectedIndex > 0)
                {
                    int iSelectedFaultGroupValue = Convert.ToInt32(cmbFaultGroup.SelectedValue);
                    PopulateFaultsByFaultGroupID(iSelectedFaultGroupValue);
                }
                objDataWatcher.DataModify = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

      
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                btnNew.Enabled = false;
                PopulateSelectedConfiguration();
                IsGMUConfigurationEditable(true);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// This event handler is used to update the GMU event information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                IsGMUConfigurationEditable(false);
                if (sSelectedConfiguration != null)
                {
                    GMUConfigurationEntity objUpdatedEntity = new GMUConfigurationEntity();

                    ExtractConfigurationSettingsFromUser(objUpdatedEntity);

                    objGMUFaultBusiness.UpdateGMUConfiguration(objUpdatedEntity);

                    this.ShowInfoMessageBox("GMU Fault Configuration settings updated successfully.");

                    StoreNewvalue();
                    if (!GmuFaultlistCompare(_NewValues, _OldValues))
                    {
                        objGMUFaultBusiness.InsertNewAuditEntry(Audit.Transport.ModuleNameEnterprise.GmuFaultEvents, this.Text, "", "Description:-->" + txtDescription.Text + " Auto Create a Service Call:-->" + cbAutoCreateServiceCall.Checked +
                        " Auto  Close a Service Call:-->" + cbAutoCloseService.Text + " Auto Create Mail:-->" + cbAutoCreateMail.Text + " Fault Group:-->" + cmbFaultGroup.Text + " Fault:-->" + cmbFault.Text
                        + " Event Type:-->" + cmbEventType.Text);
                    }

                   
                 

                    this.LoadGMUFaultConfiguration();
                    objDataWatcher.DataModify = false;
                    try
                    {
                        //It is used to auto select the row on GMU Fault configuration display grid refresh.
                        List<GMUConfigurationEntity> entity = (List<GMUConfigurationEntity>)dgvEventTypeDisplay.DataSource;                       
                        dgvEventTypeDisplay.Rows[entity.FindIndex(x => x.Datapak_Fault_ID == objUpdatedEntity.Datapak_Fault_ID)].Selected = true;
                        dgvEventTypeDisplay.FirstDisplayedScrollingRowIndex = dgvEventTypeDisplay.SelectedRows[0].Index;
                        objDataWatcher.DataModify = false;
                    }
                       
                    catch (Exception ex)
                    {
                       ExceptionManager.Publish(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                PopulateSelectedConfiguration();
                IsGMUConfigurationEditable(false);
                btnNew.Enabled = true;
               
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// This event handler is used to take the selected row from the grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvEventTypeDisplay_RowStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvEventTypeDisplay.SelectedRows.Count > 0)
                {
                    sSelectedConfiguration = (GMUConfigurationEntity)dgvEventTypeDisplay.SelectedRows[0].DataBoundItem;
                    PopulateSelectedConfiguration();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// This event handler is used to do enable/disable the Configure mail button based on auto create mail checkbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbAutoCreateMail_CheckedChanged(object sender, EventArgs e)
        {
            btnConfiguareMail.Enabled = cbAutoCreateMail.Checked;
        }

        /// <summary>
        /// This event handler is used to close the GMU fault event screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            //objDataWatcher.DataModify = false;
            this.Close();
        }

        /// <summary>
        /// This event handler is used to take the selected grid row values to hold in local entity(GMUConfigurationEntity) .
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvEventTypeDisplay_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            try
            {
                sSelectedConfiguration = (GMUConfigurationEntity)e.Row.DataBoundItem;
                PopulateSelectedConfiguration();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// This method is used to extract user updated configuration values and load to entity.
        /// </summary>
        /// <param name="objUpdatedEntity"></param>
        private void ExtractConfigurationSettingsFromUser(GMUConfigurationEntity objUpdatedEntity)
        {
            try
            {
                objUpdatedEntity.Description = txtDescription.Text.Trim();
                objUpdatedEntity.CreateServiceCall = cbAutoCreateServiceCall.Checked;
                objUpdatedEntity.CloseServiceCall = cbAutoCloseService.Checked;
                objUpdatedEntity.ToMail = cbAutoCreateMail.Checked;
                objUpdatedEntity.Fault = cmbFault.Text;
                objUpdatedEntity.Type = cmbEventType.SelectedIndex <= 0 ? (int?)null : cmbEventType.SelectedIndex;
                objUpdatedEntity.Datapak_Fault_ID = sSelectedConfiguration.Datapak_Fault_ID;
                objUpdatedEntity.Call_Fault_ID = sSelectedConfiguration.Call_Fault_ID;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// This method is used to load configuration values to appropriate controls based on the selected configuration from the GMU Fault configuration grid.
        /// </summary>
        private void PopulateSelectedConfiguration()
        {
            try
            {
                txtDescription.Text = sSelectedConfiguration.Description;
                cbAutoCloseService.Checked = Convert.ToBoolean(sSelectedConfiguration.CloseServiceCall);
                cbAutoCreateMail.Checked = Convert.ToBoolean(sSelectedConfiguration.ToMail);
                cbAutoCreateServiceCall.Checked = Convert.ToBoolean(sSelectedConfiguration.CreateServiceCall);

                cmbFault.SelectedIndex = cmbFault.FindStringExact(sSelectedConfiguration.Fault);

                cmbEventType.SelectedValue = sSelectedConfiguration.Type == null ? -1 : sSelectedConfiguration.Type;

                cmbFaultGroup.SelectedValue = sSelectedConfiguration.Call_Group_ID;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// This method is used to populate all the fault group.
        /// </summary>
        private void PopulateFaultGroup()
        {
            try
            {
                List<FaultGroupEntity> allFaultsInEntity = objGMUFaultBusiness.GetAllFaultGroupDescription();
                cmbFaultGroup.DataSource = null;
                cmbFaultGroup.DisplayMember = "Fault_Group_Description";
                cmbFaultGroup.ValueMember = "FaultGroup_ID";
                cmbFaultGroup.DataSource = allFaultsInEntity;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// This method is used to populate fault list based on given fault group
        /// </summary>
        /// <param name="iSelectedFaultGroupValue"></param>
        private void PopulateFaultsByFaultGroupID(int iSelectedFaultGroupValue)
        {
            try
            {
                List<FaultGroupChildEntity> allFaultsInEntity = objGMUFaultBusiness.GetAllFaultsByGroupID(iSelectedFaultGroupValue);
                cmbFault.DisplayMember = "Fault_Description";
                cmbFault.ValueMember = "Fault_ID";
                cmbFault.DataSource = allFaultsInEntity;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// This method is used to populate fault events.
        /// </summary>
        private void PopulateFaultEvents()
        {
            try
            {
                List<FaultEventEntity> allFaultEventsInEntity = objGMUFaultBusiness.GetAllFaultEvents();
                cmbEventType.DataSource = null;
                cmbEventType.DisplayMember = "Fault_Event_Description";
                cmbEventType.ValueMember = "Fault_Event_ID";
                cmbEventType.DataSource = allFaultEventsInEntity;
                LoadGMUFaultConfiguration();
                


                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        /// <summary>
        /// This method is used to do Enable/Disable controls according to given flag.
        /// </summary>
        /// <param name="bEditableFlag"></param>
        private void IsGMUConfigurationEditable(bool bEditableFlag)
        {
            try
            {
                IsControlsVisible = !bEditableFlag;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// This method is used to populate the Manual Fault Creation window.
        /// </summary>
        private void LoadNewManualService()
        {
            try
            {
                using (frmManualServiceCall frmNewServiceCall = new frmManualServiceCall())
                {
                    frmNewServiceCall.ShowDialog(this);
                    if (frmNewServiceCall.IsRefresh)
                    {
                        LoadGMUFaultConfiguration();
                        this.dgvEventTypeDisplay.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// This method is used to populate the GMU fault configuration details .
        /// </summary>
        private void LoadGMUFaultConfiguration()
        {
            try
            {
                dgvEventTypeDisplay.DataSource = objGMUFaultBusiness.GetAllGMUConfigurations();
                RenameGMUGridHeadersText();
                MakeInvisibleColumnsInGMUFaultConfigurationDisplay();


                dgvEventTypeDisplay.Columns["Description"].MinimumWidth = 250;
                dgvEventTypeDisplay.Columns["Fault"].MinimumWidth = 250;
                dgvEventTypeDisplay.Columns["CreateServiceCall"].MinimumWidth = 200;
                dgvEventTypeDisplay.Columns["CloseServiceCall"].MinimumWidth = 200;
                dgvEventTypeDisplay.Columns["SourceProtocol"].MinimumWidth = 200;

                if (sSelectedConfiguration == null)
                {
                    dgvEventTypeDisplay.Rows[1].Selected = true;
                    sSelectedConfiguration = (GMUConfigurationEntity)dgvEventTypeDisplay.Rows[1].DataBoundItem;

                    PopulateSelectedConfiguration();
                }                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// This method is used to set appropriate header text to the GMU Fault Configuration display grid.
        /// </summary>
        private void RenameGMUGridHeadersText()
        {
            try
            {
                if (dgvEventTypeDisplay.DataSource != null)
                {
                    dgvEventTypeDisplay.Columns["Code"].HeaderText = "Code";
                    dgvEventTypeDisplay.Columns["subcode"].HeaderText = "Sup Code";
                    dgvEventTypeDisplay.Columns["SourceProtocol"].HeaderText = "Source Protocol";
                    dgvEventTypeDisplay.Columns["Fault"].HeaderText = "Fault";
                    dgvEventTypeDisplay.Columns["CreateServiceCall"].HeaderText = "Create Service Call";
                    dgvEventTypeDisplay.Columns["CloseServiceCall"].HeaderText = "Close Service Call";
                    dgvEventTypeDisplay.Columns["SourceID"].HeaderText = "Source ID";
                    dgvEventTypeDisplay.Columns["Type"].HeaderText = "Type ID";
                    dgvEventTypeDisplay.Columns["Description"].HeaderText = "Description";
                    dgvEventTypeDisplay.Columns["ToMail"].HeaderText = "To Mail";
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        ///  This method is used to make specified grid columns as invisible in the GMU Fault Configuration display grid.        
        /// </summary>
        private void MakeInvisibleColumnsInGMUFaultConfigurationDisplay()
        {
            try
            {
                if (dgvEventTypeDisplay.DataSource != null)
                {
                    dgvEventTypeDisplay.Columns["Datapak_Fault_ID"].Visible = false;
                    dgvEventTypeDisplay.Columns["Call_Fault_ID"].Visible = false;
                    dgvEventTypeDisplay.Columns["Mail_CC"].Visible = false;
                    dgvEventTypeDisplay.Columns["Mail_TO"].Visible = false;
                    dgvEventTypeDisplay.Columns["Call_Group_ID"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        
        /// <summary>
        /// This method is used to make the controls enable/disable controls
        /// </summary>
        private bool IsControlsVisible
        {
            set
            {
                btnEdit.Visible = value;
                btnCancel.Visible = !value;
                tblGMUConfiguration.Enabled = !value;
                this.btnUpdate.Enabled = !value;
                dgvEventTypeDisplay.Enabled = value;
                if (tblGMUConfiguration.Enabled && cbAutoCreateMail.Checked)
                {
                    btnConfiguareMail.Enabled = true;
                }
            }
        }

        #endregion

        private void cmbFault_SelectedIndexChanged(object sender, EventArgs e)
        {
             _count++;
             if (_count > 2)
             {
                 objDataWatcher.DataModify = true;
             }

          

        }
        public static bool GmuFaultlistCompare<T>(IEnumerable<T> NewValue, IEnumerable<T> Oldvalue)
        {
            var cnt = new Dictionary<T, int>();
            foreach (T s in NewValue)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]++;
                }
                else
                {
                    cnt.Add(s, 1);
                }
            }
            foreach (T s in Oldvalue)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]--;
                }
                else
                {
                    return false;
                }
            }
            return cnt.Values.All(c => c == 0);
        }

    }
}

