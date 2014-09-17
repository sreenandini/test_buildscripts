using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseDataAccess;
using BMC.Common.ExceptionManagement;
using Audit.Transport;
using BMC.CoreLib.Win32;
using Audit.BusinessClasses;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmSiteMachineControl : Form
    {
        #region "Declarations"
        public string SiteCode;
        private SiteMachineControlBiz objMachineControlBiz = null;
        private GetMachineControlDetails objGetMachinetControl = null;
        private List<GetMachineControlDetails> lst_PosDetails = null;
        public int SiteID = 0;
        public int iIndex = 0;
        public string SiteCodeCap = string.Empty;

        #endregion

        #region Paramaterized Constructor
        public frmSiteMachineControl(IViewSiteInfo viewSite)
        {
            try
            {
                InitializeComponent();


                objMachineControlBiz = SiteMachineControlBiz.CreateInstance(); //Create an business instance. 
                objGetMachinetControl = new GetMachineControlDetails();//Create an Entity instance.
                SiteID = viewSite.SelectedSite.Site_ID; //gets the selected Site
                SiteCodeCap = viewSite.SelectedSite.Site_Code; // Gets Site code to dispaly in form Caption - fixed for 11.4 - 12.4.3 mismatch
                if (!String.IsNullOrEmpty(SiteCodeCap))
                    this.Text = "Site Machine Control (" + SiteCodeCap + ")";
                else
                    this.Text = "Site Machine Control";
                SetTagProperty();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }
        #endregion Paramaterized Constructor

        #region Events
        ///// <summary>
        ///// Loads Position Details in ListView
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        private void frmSiteMachineControl_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(SiteCodeCap))
                    this.Text = "Site Machine Control (" + SiteCodeCap + ")";
                else
                    this.Text = "Site Machine Control";

                FillGridPositonDetails();
                tmrCheckFocus.Enabled = true;
                this.ResolveResources();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        ///// <summary>
        ///// If EnableAll is selected Checks whether the status is Enabled, if not Enables the Position otherwise Process is Set to False.
        ///// If DisableAll is selected Checks whether the status is Disabled, if not Disables the Position otherwise Process is Set to False.
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        private void btnUpdateSite_Click(object sender, EventArgs e)
        {
            try
            {
                if (grv_PosDetails.DataSource == null)
                    return;
                List<GetMachineControlDetails> lstMcControlDetails = this.GetDataSourceFromGrid();

                if (optEnableAll.Checked)
                {
                    List<GetMachineControlDetails> lstMcEnableDetails = lstMcControlDetails.Where(item => item.Current != SiteMachineControlType.ENABLED.ToString() && optEnableAll.Checked ||
                         item.Change != SiteMachineControlType.PENDING.ToString()).ToList();
                    foreach (GetMachineControlDetails oMachineEnable in lstMcEnableDetails)
                    {
                        SendMachineControlToSite(oMachineEnable.bar_position_id.ToString(), oMachineEnable.Current);
                    }
                }
                else
                {
                    List<GetMachineControlDetails> lstMcDisableDetails = lstMcControlDetails.Where(item => item.Current != SiteMachineControlType.DISABLED.ToString() && optDisableAll.Checked ||
                         item.Change != SiteMachineControlType.PENDING.ToString()).ToList();
                    foreach (GetMachineControlDetails oMachineDisable in lstMcDisableDetails)
                    {
                        SendMachineControlToSite(oMachineDisable.bar_position_id.ToString(), oMachineDisable.Current);
                    }
                }
                lst_PosDetails = objMachineControlBiz.GetLstMachineControlDetails(SiteID, this.im_PosDetails);
                if (lst_PosDetails != null)
                    grv_PosDetails.DataSource = lst_PosDetails;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }
        /// <summary>
        /// Disables all the position 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optDisableAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (objGetMachinetControl.Display_Status == SiteMachineControlType.PENDING.ToString())
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SITEMACHINECONTROL_PROCESSPENDING"), this.Text);
                optEnableAll.Checked = false;
                objGetMachinetControl.Display_Status = SiteMachineControlType.PENDING.ToString();

                List<GetMachineControlDetails> lstMcControlDetails = this.GetDataSourceFromGrid();
                List<GetMachineControlDetails> lst_disable = lstMcControlDetails.FindAll(obj => obj.Current != SiteMachineControlType.DISABLED.ToString());
                if (lst_disable != null)
                    lst_disable.ForEach(obj => obj.Current = SiteMachineControlType.DISABLE.ToString());

                lst_disable.ForEach(obj => obj.Change = SiteMachineControlType.PENDING.ToString());

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }
        /// <summary>
        /// Enables all the position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optEnableAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (objGetMachinetControl.Display_Status == SiteMachineControlType.PENDING.ToString())
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SITEMACHINECONTROL_PROCESSPENDING"), this.Text);
                optDisableAll.Checked = false;
                List<GetMachineControlDetails> lstMcControlDetails = this.GetDataSourceFromGrid();
                List<GetMachineControlDetails> lst_Enable = lstMcControlDetails.FindAll(obj => obj.Current != SiteMachineControlType.ENABLED.ToString());
                if (lst_Enable != null)
                    lst_Enable.ForEach(obj => obj.Current = SiteMachineControlType.ENABLE.ToString());

                lst_Enable.ForEach(obj => obj.Change = SiteMachineControlType.PENDING.ToString());
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void TimerMachineControl_Tick(object sender, EventArgs e)
        {
            try
            {
                FillGridPositonDetails();
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex); ;
            }

        }
        /// <summary>
        /// Process the Messages currently in the queue.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrCheckFocus_Tick(object sender, EventArgs e)
        {
            try
            {
                tmrCheckFocus.Enabled = false;
                Application.DoEvents();
                Application.DoEvents();
                Application.DoEvents();

                this.Focus();

                Application.DoEvents();
                Application.DoEvents();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Refresh the Grid For every 1min.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                TimerMachineControl.Interval = 60000;
                TimerMachineControl.Start();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }
        #endregion Events

        #region User Defined Functions
        public List<GetMachineControlDetails> GetDataSourceFromGrid()
        {
            List<GetMachineControlDetails> result = null;

            try
            {
                BMC.EnterpriseClient.Helpers.SortableBindingList<GetMachineControlDetails> list = grv_PosDetails.DataSource as BMC.EnterpriseClient.Helpers.SortableBindingList<GetMachineControlDetails>;
                if (list != null)
                {
                    result = list.ActualList;
                }
                if (result == null)
                {
                    result = grv_PosDetails.DataSource as List<GetMachineControlDetails>;
                }
            }
            catch { result = null; }

            return result;
        }

        /// <summary>
        /// Loads the GridView Postion Details.
        /// </summary>
        public void FillGridPositonDetails()
        {
            try
            {
                List<GetMachineControlDetails> lst_temp = objMachineControlBiz.GetLstMachineControlDetails(SiteID, this.im_PosDetails);
                BMC.EnterpriseClient.Helpers.SortableBindingList<GetMachineControlDetails> lst_PosDetails = new BMC.EnterpriseClient.Helpers.SortableBindingList<GetMachineControlDetails>(lst_temp);
                if (lst_PosDetails != null)
                    grv_PosDetails.DataSource = lst_PosDetails;

                grv_PosDetails.Columns["bar_position_id"].Visible = false;
                grv_PosDetails.Columns["bar_position_machine_enabled"].Visible = false;
                grv_PosDetails.Columns["installation_id"].Visible = false;
                grv_PosDetails.Columns["Image_Index"].HeaderText = "";
                grv_PosDetails.Columns["Image_Index"].Width = 20;
                grv_PosDetails.Columns["Display_Status"].Visible = false;
                grv_PosDetails.Columns["Previous_Status"].Visible = false;
                grv_PosDetails.Columns["Position"].Width = 75;

                grv_PosDetails.Columns["Asset"].Width = 75;
                grv_PosDetails.Columns["GameTitle"].Width = 200;
                grv_PosDetails.Columns["Current"].Width = 75;
                grv_PosDetails.Columns["Change"].Width = 75;

                foreach (DataGridViewColumn column in grv_PosDetails.Columns)
                {
                    if (grv_PosDetails.Columns[column.Name].Name != "Image_Index")
                    {
                        grv_PosDetails.Columns[column.Name].SortMode = DataGridViewColumnSortMode.Automatic;
                    }
                }
                this.grv_PosDetails.ClearSelection();
                SetTagPropertyforPosDetailsGrid();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Puts an Entry in Export History Table     
        /// </summary>
        /// <param name="strEhProcess"></param>
        /// <param name="Reference"></param>
        private void InsertExportHistory(string strEhProcess, string Reference)
        {
            try
            {
                if (grv_PosDetails.SelectedRows != null)
                {
                    objMachineControlBiz.InsertExportHistory(Reference, strEhProcess, SiteID);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strPendingText"></param>
        /// <param name="iPendingval"></param>
        /// <param name="Bar_Pos_ID"></param>
        private void UpdateMachineStatusAsPending(string strPendingText, int iPendingval, string Bar_Pos_ID)
        {
            try
            {
                if (Bar_Pos_ID == null)
                {
                    Bar_Pos_ID = grv_PosDetails.SelectedRows[0].Cells["bar_position_id"].Value.ToString();
                }
                if (grv_PosDetails.SelectedRows != null)
                {
                    objMachineControlBiz.UpdateBarPositionForMachineContol(Bar_Pos_ID, strPendingText, iPendingval);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SendMachineControlToSite(string BarPosID, string Command)
        {
            //bool bIsStatusSame = false;
            try
            {
                if (string.IsNullOrEmpty(BarPosID)) return;
                Command = (Command == SiteMachineControlType.ENABLE.ToString()) ?
                    SiteMachineControlType.MACHINEENABLE.ToString() : SiteMachineControlType.MACHINEDISABLE.ToString();
                //Export only the row which was double clicked and  don't send the same command again
                InsertExportHistory(Command, BarPosID);
                UpdateMachineStatusAsPending(Command, 100, BarPosID);
                InsertNewAuditEntry(ModuleNameEnterprise.EnableDisableMachine, "EnableDisableMachine", "BarPosID-" + BarPosID + "", Command);



            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }
        }
        public void InsertNewAuditEntry(ModuleNameEnterprise moduleName, string strScreenName, string strField, string strNewItem)
        {
            try
            {

                Audit_History AH = new Audit_History();
                AH.EnterpriseModuleName = moduleName;
                AH.Audit_Screen_Name = strScreenName;
                AH.Audit_Desc = "Record [" + strField + "] Change To " + strNewItem + "  State In  " + strScreenName + " Screen";
                AH.AuditOperationType = OperationType.MODIFY;
                AH.Audit_Field = strField;
                AH.Audit_New_Vl = strNewItem;
                AH.Audit_Slot = string.Empty;
                AH.Audit_Old_Vl = string.Empty;
                AH.Audit_User_ID = CommonBiz.iUserId;
                AH.Audit_User_Name = CommonBiz.strUsername;
                AuditViewerBusiness AVB = new AuditViewerBusiness(Common.Utilities.DatabaseHelper.GetConnectionString());
                AVB.InsertAuditData(AH, true);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion User Defined Functions

        private void grv_PosDetails_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                GetMachineControlDetails details = (grv_PosDetails.Rows[e.RowIndex].DataBoundItem) as GetMachineControlDetails;
                if (details.Change == SiteMachineControlType.PENDING.ToString()) return;
                details.Current = (details.Current == SiteMachineControlType.DISABLE.ToString() ||
                    details.Current == SiteMachineControlType.DISABLED.ToString()) ? SiteMachineControlType.ENABLE.ToString() : SiteMachineControlType.DISABLE.ToString();
                details.Change = SiteMachineControlType.PENDING.ToString();

                details.Image_Index = im_PosDetails.Images[(details.Change == SiteMachineControlType.PENDING.ToString()) ? 0 : (details.Change == SiteMachineControlType.ENABLE.ToString()) ? 1 : 2];
                bool bEnableDisable = (details.Current != details.Previous_Status &&
                                        details.Change != SiteMachineControlType.PENDING.ToString());
                if (!bEnableDisable)
                    SendMachineControlToSite(details.bar_position_id.ToString(), details.Current);
                grv_PosDetails.Refresh();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void SetTagProperty()
        {
            this.chkAutoRefresh.Tag = "Key_AutoRefresh";
            this.optDisableAll.Tag = "Key_DisableAll";
            this.optEnableAll.Tag = "Key_EnableAll";
            this.Tag = "Key_SiteMachineControl";
            this.btnUpdateSite.Tag = "Key_UpdateSite";
        }

        public void SetTagPropertyforPosDetailsGrid()
        {
            if (grv_PosDetails.Rows.Count > 0)
            {
                grv_PosDetails.Columns["Position"].Tag = "Key_Position";
                grv_PosDetails.Columns["Asset"].Tag = "Key_Asset";
                grv_PosDetails.Columns["GameTitle"].Tag = "Key_GameTitle";
                grv_PosDetails.Columns["Current"].Tag = "Key_Current";
                grv_PosDetails.Columns["Change"].Tag = "Key_Change";
            }
        }
    }
}
