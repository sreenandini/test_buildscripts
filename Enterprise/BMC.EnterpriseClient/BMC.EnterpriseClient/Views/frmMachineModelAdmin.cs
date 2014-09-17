using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.CoreLib.Win32;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{

    public partial class frmMachineModelAdmin : BMC.EnterpriseClient.Helpers.BMCExtendedForm
    {
        private int _MachineType;
        private const string ScreenName = "Model Administrator [GA]=> ";
        string strNone = BMC.Common.ResourceExtensions.GetResourceTextByKey(null, "Key_NoneHyphen");
        string strAny = BMC.Common.ResourceExtensions.GetResourceTextByKey(null, "Key_Any");
        BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;



        public frmMachineModelAdmin()
        {
            InitializeComponent();
            SetTagProperty();

        }

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {

            this.btnClose.Tag = "Key_CloseCaption";
            this.chkSASAddTrueCoinInToDrop.Tag = "Key_AddTrueCoinIntoCoinDrop";
            this.chkSASJackpotAddedToCC.Tag = "Key_JackpotAddedtoCancelledCredits";
            this.Tag = "Key_MachineModelAdministration";
            this.lblManufacturer.Tag = "Key_ManufacturerColon";
            this.lblModelName.Tag = "Key_ModelNameColon";
            this.chkSASRecreateCancelledCredits.Tag = "Key_RecreateCancelledCredits";
            this.chkSASRecreateTicketsInsertedfromDrop.Tag = "Key_RecreateVouchersInsertedfromDrop";
            this.gpSASMeter.Tag = "Key_SASMeterCalculations";
            this.btnSave.Tag = "Key_SaveCaption";
            this.chkSASUseCancCredAsPrintedTickets.Tag = "Key_UseCancelledCreditsAsPrintedVouchers";

        }



        public void ShowMe(int MachineType)
        {
            this.ResolveResources();
            _MachineType = MachineType;
            LoadMachineNames();
            LoadManfacturer();
            this.ShowDialog();
            objDatawatcher = new Helpers.Datawatcher(this);
        }

        /// <summary>
        /// Load Machine Name from M\C Class
        /// </summary>
        private void LoadMachineNames()
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Load machine names", LogManager.enumLogLevel.Info);
                List<GetMachineNamesOnMachineTypeResult> lst_MCName = AssetManagementBiz.CreateInstance().GetMachineNamesOnMachineType(_MachineType);
                lst_MCName.Insert(0, new GetMachineNamesOnMachineTypeResult { Machine_Name = (lst_MCName == null) ? strNone : strAny });
                cmbModelName.DataSource = lst_MCName;
                cmbModelName.DisplayMember = "Machine_Name";
                cmbModelName.ValueMember = "Machine_Name";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Load Manfacturer by M\C type
        /// </summary>
        private void LoadManfacturer()
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Load manfacturer details", LogManager.enumLogLevel.Info);
                List<GetManufacturerbyMCTypeResult> lst_Manf = AssetManagementBiz.CreateInstance().GetManufacturerbyMCType(_MachineType);
                if (lst_Manf != null)
                {
                    lst_Manf.Insert(0, new GetManufacturerbyMCTypeResult { Manufacturer_ID = -1, Manufacturer_Name = (lst_Manf==null)?strNone:strAny });
                }
                cmbManufacturer.DataSource = lst_Manf;
                cmbManufacturer.DisplayMember = "Manufacturer_Name";
                cmbManufacturer.ValueMember = "Manufacturer_ID";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// get model configurations from db
        /// </summary>
        /// <returns></returns>
        private bool FetchModelConfigurations(bool IsNotUpdate)
        {
            bool retVal = false;
            try
            {
                if (cmbModelName.SelectedIndex <= 0 || cmbManufacturer.SelectedIndex <= 0)
                {
                    ClearModelConfigurations();
                    return retVal;
                }
                LogManager.WriteLog(ScreenName + @"Load Model Configurations details M\C Name: " + cmbModelName.Text, LogManager.enumLogLevel.Info);
                List<GetModelConfigurationsResult> lst_ModelConfig = AssetManagementBiz.CreateInstance().GetModelConfigurations(_MachineType, cmbModelName.Text, (int)cmbManufacturer.SelectedValue);
                if (lst_ModelConfig.Count > 0)
                {
                    if (IsNotUpdate)
                    {
                        GetModelConfigurationsResult model = lst_ModelConfig[0];
                        chkSASRecreateCancelledCredits.Checked = model.Machine_Class_RecreateCancelledCredits ?? false;
                        chkSASJackpotAddedToCC.Checked = model.Machine_Class_JackpotAddedToCancelledCredits ?? false;
                        chkSASAddTrueCoinInToDrop.Checked = model.Machine_Class_AddTrueCoinInToDrop ?? false;
                        chkSASUseCancCredAsPrintedTickets.Checked = model.Machine_Class_UseCancelledCreditsAsTicketsPrinted ?? false;
                        chkSASRecreateTicketsInsertedfromDrop.Checked = model.Machine_Class_RecreateTicketsInsertedfromDrop ?? false;
                    }
                    retVal = true;
                }
                else
                {
                    Win32Extensions.ShowInfoMessageBox(this,this.GetResourceTextByKey(1, "MSG_MODELCONFIGNOTFOUND"), this.Text);
                    ClearModelConfigurations();

                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return retVal;
        }

        /// <summary>
        /// update model configurations to db
        /// </summary>
        /// <returns></returns>
        private bool UpdateModelConfigurations()
        {
            return AssetManagementBiz.CreateInstance().UpdateModelConfigurations(_MachineType, cmbModelName.Text, (int)cmbManufacturer.SelectedValue, chkSASRecreateCancelledCredits.Checked,
                chkSASJackpotAddedToCC.Checked, chkSASAddTrueCoinInToDrop.Checked, chkSASUseCancCredAsPrintedTickets.Checked,
                chkSASRecreateTicketsInsertedfromDrop.Checked);

        }
        /// <summary>
        /// clear model configurations
        /// </summary>
        private void ClearModelConfigurations()
        {

            chkSASRecreateCancelledCredits.Checked = false;
            chkSASJackpotAddedToCC.Checked = false;
            chkSASAddTrueCoinInToDrop.Checked = false;
            chkSASUseCancCredAsPrintedTickets.Checked = false;
            chkSASRecreateTicketsInsertedfromDrop.Checked = false;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            LogManager.WriteLog(ScreenName + "btnClose_Click: Form Closing", LogManager.enumLogLevel.Debug);
            this.Close();
        }

        private void cmbModelName_SelectedIndexChanged(object sender, EventArgs e)
        {
            FetchModelConfigurations(true);
        }

        private void cmbManufacturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            FetchModelConfigurations(true);
        }

        /// <summary>
        /// Save Model Configuration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            string ErrorMsg = "";
            try
            {
                LogManager.WriteLog(ScreenName + "btnSave_Click.", LogManager.enumLogLevel.Debug);
                if (ValidateControls(ref ErrorMsg))
                {
                    if (FetchModelConfigurations(false))
                    {
                        if (UpdateModelConfigurations())
                        {
                            LogManager.WriteLog(ScreenName + "Model configuration saved successfully.", LogManager.enumLogLevel.Debug);
                            Win32Extensions.ShowInfoMessageBox(this,this.GetResourceTextByKey(1, "MSG_MODELCONFIGSAVED"), this.Text);
                        }
                        else
                        {
                            LogManager.WriteLog(ScreenName + "Unable to update model configuration.", LogManager.enumLogLevel.Error);
                            Win32Extensions.ShowInfoMessageBox(this,this.GetResourceTextByKey(1, "MSG_UNABLETOUPDATEMODEL"), this.Text);
                        }
                    }
                }
                else
                {
                    Win32Extensions.ShowInfoMessageBox(this,ErrorMsg, this.Text);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        #region ValidationMethods
        bool ValidateControls(ref string ErrorMsg)
        {
            bool retVal = true;
            try
            {
                if (cmbModelName.SelectedIndex <= 0)
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_SELECT_MODEL");
                    ClearModelConfigurations();
                    retVal = false;
                }
                else if (cmbManufacturer.SelectedIndex <= 0)
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_SELECT_MANUFACTURER");
                    ClearModelConfigurations();
                    retVal = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                ErrorMsg = ex.Message;
                retVal = false;

            }
            return retVal;
        }

        #endregion



    }


}
