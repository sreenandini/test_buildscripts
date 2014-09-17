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
using BMC.Common.LogManagement;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmTerminateMachine : BMC.EnterpriseClient.Helpers.BMCExtendedDialogForm
    {

         BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;

        public frmTerminateMachine()
        {
            InitializeComponent();
            SetTagProperty();
        }

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.btnCancel.Tag = "Key_CancelCaption";
            this.btnOK.Tag = "Key_Terminate";
            this.label4.Tag = "Key_CommentsColon";
            this.Tag = "Key_TerminateMachine";
            this.label2.Tag = "Key_TerminationDateColon";
            this.lbl_TerminationReason.Tag = "Key_TerminationReasonColon";
            this.label3.Tag = "Key_UserName";
        }

        #region Private Variables
        private const string Branding = "Bally MultiConnect - Enterprise";
        private const string ScreenName = "Terminate Machine => "; 
        #endregion

        #region Public Variables
        public bool IsNGA { get; set; }
        public bool IsEdit { get; set; }
        public string StockNo { get; set; }
        #endregion

        #region Events
        private void frmTerminateMachine_Load(object sender, EventArgs e)
        {
            try
            {
                this.ResolveResources();
                StockNo = StockNo.Substring(0, StockNo.IndexOf(" "));
                this.Text = this.GetResourceTextByKey("Key_TerminateMachine") + "[ " + this.GetResourceTextByKey("Key_TerminateMC_StockNo") + StockNo + "]"; //"Terminate Machine [Stock No - " + StockNo + "]";

                LoadTerminationReason();

                bool IsNotEdit = !IsEdit;
                txtComments.Enabled = IsNotEdit;
                cmbTerminateReason.Enabled = IsNotEdit;
                btnOK.Enabled = IsNotEdit;

                if (IsNotEdit)
                {
                    txtTerminateDate.Text = System.DateTime.Now.ToString("dd/MM/yy");
                    txtUserName.Text = AppEntryPoint.Current.UserName;
                    btnCancel.Text = this.GetResourceTextByKey("Key_CancelCaption");
                }
                else
                {
                    LoadMCTerminationDetails(StockNo);
                    btnCancel.Text = this.GetResourceTextByKey("Key_Close");
                    

                }
                objDatawatcher = new Helpers.Datawatcher(this);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        protected override bool ValidateChanges()
        {
            if (cmbTerminateReason.SelectedIndex == -1)
            {
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SELECT_TERMINATE_REASON"), this.Text);
                cmbTerminateReason.Focus();
                return false;
            }
            return base.ValidateChanges();
        }

        protected override void SaveChanges()
        {
            if (this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_WANT_TO_TERMINATE_ASSET"),this.Text) == DialogResult.Yes)
            {
                try
                {
                    if (AssetManagementBiz.CreateInstance().UpdateTerminationMCDetails(StockNo, txtComments.Text.Trim(), txtUserName.Text, (int)cmbTerminateReason.SelectedValue, (int)StockStatus.STOCK_TERMINATED, txtTerminateDate.Text, IsNGA))
                    {
                        LogManager.WriteLog(ScreenName + @"Terminate_Click: M\C Terminated Successfully, StockNo: " + StockNo, LogManager.enumLogLevel.Debug);
                        this.SuppressConfirmMessageBox = true;
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
               
            }

           base.SaveChanges();
        } 
        #endregion

        #region DBMethods
        private void LoadTerminationReason()
        {
            try
            {
                LogManager.WriteLog(ScreenName + @"Load Termination Reason ", LogManager.enumLogLevel.Debug);
                List<GetMachineTerminationReasonResult> lst_MCReason = AssetManagementBiz.CreateInstance().GetMachineTerminationReason();
                cmbTerminateReason.DataSource = lst_MCReason;
                cmbTerminateReason.DisplayMember = "MTRT_Description";
                cmbTerminateReason.ValueMember = "MTRT_ID";
                if (cmbTerminateReason.Items.Count > 0)
                    cmbTerminateReason.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadMCTerminationDetails(string StockNo)
        {
            try
            {
                LogManager.WriteLog(ScreenName + @"Load M\C Termination Details ", LogManager.enumLogLevel.Debug);
                List<GetTerminationMCDetailsResult> lst_MCTerminate = AssetManagementBiz.CreateInstance().GetTerminationMachineDetails(StockNo);
                if (lst_MCTerminate.Count > 0)
                {
                    GetTerminationMCDetailsResult MC_termin = lst_MCTerminate[0];
                    txtComments.Text = MC_termin.Machine_Termination_Comments;
                    txtTerminateDate.Text = MC_termin.Machine_End_Date;
                    txtUserName.Text = MC_termin.Machine_Termination_Username;
                    List<GetMachineTerminationReasonResult> lst_MCReason = (List<GetMachineTerminationReasonResult>)cmbTerminateReason.DataSource;
                    if (lst_MCReason != null && lst_MCReason.Count > 0)
                    {
                        int ind = lst_MCReason.FindIndex(se => se.MTRT_ID == MC_termin.Machine_Termination_Reason);
                        cmbTerminateReason.SelectedIndex = (ind >= 0) ? ind : 0;
                    }

                    cmbTerminateReason.SelectedIndex = 0;
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
