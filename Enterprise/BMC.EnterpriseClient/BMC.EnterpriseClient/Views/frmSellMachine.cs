using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.CoreLib.Win32;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.ExceptionManagement;
using BMC.Common.Utilities;
using BMC.Common.LogManagement;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common;
namespace BMC.EnterpriseClient.Views
{
    public partial class SellMachineForm : BMC.EnterpriseClient.Helpers.BMCExtendedDialogForm
    {
        private int _MachineID = 0;
        private const string ScreenName = "Sell Machine => ";
        BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;


        Action<int> act_removeMC = null;

        #region Constructor
        public SellMachineForm(int MachineID, Action<int> act_removeSellMC)
        {
            InitializeComponent();
            SetTagProperty();
            try
            {
                _MachineID = MachineID;
                act_removeMC = act_removeSellMC;
                if (_MachineID == 0)
                {
                    this.Close();
                }
                else
                {
                    List<GetMachineAssetDetailsResult> lstMCAsset = AssetManagementBiz.CreateInstance().GetMachineAssetDetails(_MachineID);
                    if (lstMCAsset != null && lstMCAsset.Count > 0)
                    {
                        GetMachineAssetDetailsResult MCAsset = lstMCAsset[0];
                        txtModelName.Text = MCAsset.ModelName;
                        txtAssetNo.Text = MCAsset.Machine_Stock_No;
                        txtSerialNo.Text = MCAsset.Machine_Manufacturers_Serial_No;
                        txtAltSerialNo.Text = MCAsset.Machine_Alternative_Serial_Numbers;

                    }
                    else
                    {
                        Win32Extensions.ShowInfoMessageBox(this,this.GetResourceTextByKey(1, "MSG_ERROR_LOAD_MC_DETAILS"),this.Text);
                        this.Close();
                    }
                }
                objDatawatcher = new Helpers.Datawatcher(this);                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                Win32Extensions.ShowInfoMessageBox(this,this.GetResourceTextByKey(1, "MSG_ERROR_LOAD_MC_DETAILS"),this.Text);
                this.Close();
            }
        }
        #endregion

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {

            this.btnCancel.Tag = "Key_CancelCaption";
            this.btnOK.Tag = "Key_OKCaption";
            this.label4.Tag = "Key_AltSerialColon";
            this.lbl_AssetNo.Tag = "Key_AssetNoColon";
            this.lbl_Date.Tag = "Key_DateColon";
            this.lbl_InvoiceNo.Tag = "Key_InvoiceNoColon";
            this.grp_MachineDetails.Tag = "Key_MachineDetails";
            this.lbl_Model.Tag = "Key_ModelColon";
            this.grp_SaleDetails.Tag = "Key_SaleDetails";
            this.Tag = "Key_SellMachine";
            this.label3.Tag = "Key_SerialNoColon";
            this.lbl_SaleType.Tag = "Key_SoldTypeColon";
            this.lbl_SoldTo.Tag = "Key_SoldToColon";
            this.lbl_Value.Tag = "Key_ValueColon";

        }

        private void SellMachineForm_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
            dtSaleDate.Value = DateTime.Now;
        }

        #region ValidationMethods
        /// <summary>
        /// Validate all controls and returns true if condition is Statsified
        /// </summary>
        /// <param name="ErrorMsg">returns Error Message</param>
        /// <returns></returns>
        bool Validate(ref string ErrorMsg)
        {
            bool retVal = true;
            try
            {

                if (!txtValue.Text.IsNumeric())
                {
                    ErrorMsg = this.GetResourceTextByKey(1,"MSG_SELLMAC_INVALIDSALEVALUE");//"Invalid Sale Value";
                    retVal = false;
                    txtValue.Focus();
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                ErrorMsg = this.GetResourceTextByKey(1,"MSG_SELLMAC_UPDATEFAILED");//ex.Message;
                retVal = false;
            }
            return retVal;

        }
        #endregion

        protected override bool ValidateChanges()
        {
            string ErrorMsg = "";
            if (!Validate(ref ErrorMsg))
            {
                Win32Extensions.ShowInfoMessageBox(this, ErrorMsg,this.Text);
                return false;
            }
            return base.ValidateChanges();
        }

        protected override void SaveChanges()
        {

            try
            {

                if (AssetManagementBiz.CreateInstance().UpdateMachineAssetDetails(_MachineID, dtSaleDate.Value, (int)StockStatus.STOCK_SOLD, txtSoldTo.Text,
                     txtSaleType.Text, Convert.ToDecimal(txtValue.Text), AppEntryPoint.Current.UserId, DateTime.Now.Date,
                     txtSaleInvoiceNumber.Text))
                {
                    LogManager.WriteLog(ScreenName + @"machine selled successfully M\C ID:" + _MachineID, LogManager.enumLogLevel.Info);
                    //Audit
                    AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                    business.InsertAuditData(new Audit.Transport.Audit_History
                    {
                        EnterpriseModuleName = ModuleNameEnterprise.AUDIT_GENERAL,
                        Audit_Slot = "",
                        Audit_Screen_Name = "Machine",
                        Audit_Field = "MachineID",
                        Audit_Old_Vl = _MachineID.ToString(),
                        Audit_New_Vl = _MachineID.ToString(),
                        Audit_Desc = "STOCK_SOLD",
                        AuditOperationType = OperationType.MODIFY,
                        Audit_User_ID = AppEntryPoint.Current.UserId,
                        Audit_User_Name = AppEntryPoint.Current.UserName
                    }, false);
                    if (act_removeMC != null)
                    {
                        act_removeMC(_MachineID);
                    }
                    objDatawatcher.DataModify = false;
                    base.SaveChanges();
                }
                else
                {
                    LogManager.WriteLog(ScreenName + @"Unable to sell machine  M\C ID:" + _MachineID, LogManager.enumLogLevel.Error);
                    Win32Extensions.ShowInfoMessageBox(this,this.GetResourceTextByKey(1, "MSG_UNABLE_TO_SELL_MC"),this.Text);
                }



            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }
        }

    }
}
