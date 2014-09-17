using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Stacker.Business;
using BMC.EnterpriseClient.Views;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseClient.Helpers;
using BMC.Common.LogManagement;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.Utilities;
using BMC.CoreLib.Win32;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmStackerDetails : Form
    {
        #region Events

        public enum GridFormTypes
        {
            gftSakcer = 0,
            gftSchedule = 1
        }

        public GridFormTypes GridFormType = GridFormTypes.gftSakcer;
        // public GridFormTypes GridFormType = GridFormTypes.gftSchedule;

        public frmStackerDetails()
        {
            InitializeComponent();
            SetTagProperty();
        }

        public frmStackerDetails(GridFormTypes formTypes)
        {
            GridFormType = formTypes;
            InitializeComponent();
            SetTagProperty();
        }


        /// <summary>
        /// Add new Stacker Details
        /// </summary>

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (GridFormType == GridFormTypes.gftSakcer)
                {
                    frmStackerAddEdit stkAdd = new frmStackerAddEdit();
                    stkAdd.ShowDialog();
                    LoadStackerGridView();
                }
                else if (GridFormType == GridFormTypes.gftSchedule)
                {
                    frmDropScheduleUtility DropSchedule = new frmDropScheduleUtility();
                    DropSchedule.IsUpdate = false;
                    DropSchedule.ShowAutoSchedule = true;
                    DropSchedule.ShowDialog();
                    LoadScheduleGridView();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error while saving Stacker or Drop schedule :" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        /// <summary>
        /// Set the properties for the selected Stacker Details
        /// Display the Stacker Details in Edit mode
        /// Edit Stacker Details 
        /// </summary>

        private bool EditGridRow(int rowIndex)
        {
            try
            {

                bool result = false;
                if (GridFormType == GridFormTypes.gftSakcer)
                {
                    result = EditSackerGridRow(rowIndex);
                    if (result)
                        LoadStackerGridView();
                }
                else if (GridFormType == GridFormTypes.gftSchedule)
                {
                    result = EditScheduleGridRow(rowIndex);
                    if (result)
                        LoadScheduleGridView();
                }
                return result;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error - Editing grid row : ", ex.Message, LogManager.enumLogLevel.Error);
                return false;
            }
        }

        private bool DeleteGridRow(int rowIndex)
        {
            //
            try
            {
                bool result = false;
                if (GridFormType == GridFormTypes.gftSakcer)
                {
                    result = DeleteStackerGridRow(rowIndex);
                    if (result)
                        LoadStackerGridView();
                }
                else if (GridFormType == GridFormTypes.gftSchedule)
                {
                    result = DeleteScheduleGridRow(rowIndex);
                    if (result)
                        LoadScheduleGridView();
                }
                return result;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error - Deleting grid row : ", ex.Message, LogManager.enumLogLevel.Error);
                return false;
            }

        }

        private bool EditSackerGridRow(int rowIndex)
        {
            try
            {
                frmStackerAddEdit stkAdd = new frmStackerAddEdit();
                stkAdd.IsUpdate = true;

                int StkID = -1;
                int.TryParse(dgvDetails["StackerId", rowIndex].Value.ToString(), out StkID);
                stkAdd.Stacker.StackerID = StkID;

                //Check whether the selected stacker is in use or not
                //Starts

                StackerBusiness objBStackerDetails = new StackerBusiness();
                int StackerInUseStatus = 0;
                // int a;
                StackerInUseStatus = objBStackerDetails.IsStackerInUse(StkID);
                stkAdd.IsStackerInUseForEdit = StackerInUseStatus == 1;

                stkAdd.Stacker.StackerName = dgvDetails["StackerName", rowIndex].Value.ToString();
                int size = 1;
                int.TryParse(dgvDetails["StackerSize", rowIndex].Value.ToString(), out size);
                stkAdd.Stacker.StackerSize = size;

                stkAdd.Stacker.StackerDescription = dgvDetails["StackerDescription", rowIndex].Value.ToString();

                bool status = false;

                bool.TryParse(dgvDetails["StackerStatus", rowIndex].Value.ToString(), out status);
                stkAdd.Stacker.StackerStatus = status;
                stkAdd.ShowDialog();

                return true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in editing Stacker : " + ex.Message, LogManager.enumLogLevel.Error);
                return false;
            }

        }

        private bool DeleteStackerGridRow(int rowIndex)
        {
            try
            {
                bool result = false;
                string strStackerName = dgvDetails["StackerName", rowIndex].Value.ToString();
                if (BMC.CoreLib.Win32.Win32Extensions.ShowQuestionMessageBox(this,string.Format(this.GetResourceTextByKey(1, "MSG_STACKER_DELETESTACKER"),  strStackerName), this.Text) == DialogResult.Yes)
                {
                    StackerBusiness objBStackerDetails = new StackerBusiness();
                    int Stacker_Id = -1;
                    int.TryParse(dgvDetails["StackerId", rowIndex].Value.ToString(), out Stacker_Id);
                    if (objBStackerDetails.DeleteStacker(Stacker_Id))
                    {
                        BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(this, string.Format(this.GetResourceTextByKey(1, "MSG_STACKER_DELETE_SUCCESS") , strStackerName), this.Text);

                        try
                        {
                            AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                            {
                                business.InsertAuditData(new Audit.Transport.Audit_History
                                {
                                    EnterpriseModuleName = ModuleNameEnterprise.Stacker,
                                    Audit_Screen_Name = "Stacker|StackerAddEdit",
                                    Audit_Desc = "Stacker Deleted-" + "Stacker ID: " + Stacker_Id + "; StackerName: " + strStackerName,
                                    AuditOperationType = OperationType.DELETE,
                                    Audit_User_ID = AppEntryPoint.Current.UserId,
                                    Audit_User_Name = AppEntryPoint.Current.UserName,
                                    Audit_Field = "SysDelete",
                                    Audit_Old_Vl = "Active",
                                    Audit_New_Vl = "Delete"
                                }, false);
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.WriteLog("Error While Adding Audit Log for Stacker Delete: Error Message:" + ex.Message, LogManager.enumLogLevel.Error);
                        }

                        //Del-id, StackerName,  User
                    }
                    else
                    {
                        BMC.CoreLib.Win32.Win32Extensions.ShowWarningMessageBox(this, string.Format(this.GetResourceTextByKey(1, "MSG_STACKER_CANNOT_DELETE"), strStackerName), this.Text);
                    }

                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in deleting Stacker : " + ex.Message, LogManager.enumLogLevel.Error);
                return false;
            }
        }

        private bool EditScheduleGridRow(int rowIndex)
        {
            //
            try
            {
                bool result = false;
                int SchID = -1;
                int.TryParse(dgvDetails["ScheduleID", rowIndex].Value.ToString(), out SchID);
                if (BMC.CoreLib.Win32.Win32Extensions.ShowQuestionMessageBox(this, string.Format(this.GetResourceTextByKey(1, "MSG_DROPSCHEDULE_EDIT"), SchID), this.Text) == DialogResult.Yes)
                {
                    frmDropScheduleUtility DropSchedule = new frmDropScheduleUtility();
                    DropSchedule.IsUpdate = true;
                    DropSchedule.EditScheduleId = SchID;
                    DropSchedule.ShowAutoSchedule = true;
                    DropSchedule.ShowDialog();
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in editing Schedule : " + ex.Message, LogManager.enumLogLevel.Error);
                return false;
            }
        }

        private bool DeleteScheduleGridRow(int rowIndex)
        {
            try
            {
                bool result = false;
                DropScheduleBiz objDropScheduleBiz = DropScheduleBiz.CreateInstance();
                int SchID = -1;
                int.TryParse(dgvDetails["ScheduleID", rowIndex].Value.ToString(), out SchID);

                if (MessageBox.Show(this.GetResourceTextByKey(1, "MSG_ADD_DELETE_DROPSCHEDULE") + SchID + this.GetResourceTextByKey("Key_Question"), this.GetResourceTextByKey(1, "MSG_ADD_SCHEDULE"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    objDropScheduleBiz.ScheduleEntity.ScheduleId = SchID;
                    result = objDropScheduleBiz.DeleteDropSchedule(SchID);
                    try
                    {
                        AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                        {
                            business.InsertAuditData(new Audit.Transport.Audit_History
                            {
                                EnterpriseModuleName = ModuleNameEnterprise.DropAlert,
                                Audit_Screen_Name = "DropAlert|AutoDropAlert",
                                Audit_Desc = "Auto Drop Schedule Deleted for Schedule ID:" + SchID,
                                AuditOperationType = OperationType.DELETE,
                                Audit_User_ID = AppEntryPoint.Current.UserId,
                                Audit_User_Name = AppEntryPoint.Current.UserName
                            }, false);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogManager.WriteLog("Error While Adding Audit Log for Auto Drop Alert Deleting: Error Message:" + ex.Message, LogManager.enumLogLevel.Error);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in Deleting Schedule : " + ex.Message, LogManager.enumLogLevel.Error);
                return false;

            }
        }

        private void dgvStackerDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                int RowIndex = e.RowIndex;
                if (RowIndex < 0)
                    return;

                //bool EditRow = (e.ColumnIndex == dgvDetails.Columns.Count - 2);
                //bool DeleteRow = (e.ColumnIndex == dgvDetails.Columns.Count - 1);
                bool EditRow = (e.ColumnIndex == 0);
                bool DeleteRow = (e.ColumnIndex == 1);

                if (!((DeleteRow) || (EditRow)))
                    return;

                if (EditRow)
                    EditGridRow(RowIndex);
                else if (DeleteRow)
                    DeleteGridRow(RowIndex);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error - Clicking Edit/Delete button in grid view : " + ex.Message, LogManager.enumLogLevel.Error);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StackerDetails_Load(object sender, EventArgs e)
        {
            try
            {
                btnMannualDropAlert.Visible = (GridFormType == GridFormTypes.gftSchedule);
                if (GridFormType == GridFormTypes.gftSakcer)
                {
                    LoadStackerGridView();
                }
                else if (GridFormType == GridFormTypes.gftSchedule)
                {
                    LoadScheduleGridView();
                }
               // this.ResolveResources();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error - Loading Stacker details : " + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        private void btnMannualDropAlert_Click(object sender, EventArgs e)
        {
            //
            try
            {
                frmDropScheduleUtility DropSchedule = new frmDropScheduleUtility();
                DropSchedule.ShowAutoSchedule = false;
                DropSchedule.ShowDialog();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error : Manual Drop alert : " + ex.Message, LogManager.enumLogLevel.Error);
            }
        }


        private void LoadStackerGridView()
        {
            try
            {
                StackerBusiness objBStackerDetails = new StackerBusiness();
                dgvDetails.DataSource = objBStackerDetails.GetStackerDetails();
                //dgvDetails.Columns["StackerId"].ReadOnly = true;
                dgvDetails.Columns["StackerId"].Visible = false;
                dgvDetails.Columns["StackerName"].ReadOnly = true;
                dgvDetails.Columns["StackerSize"].ReadOnly = true;
                dgvDetails.Columns["StackerStatus"].ReadOnly = true;
                dgvDetails.Columns["StackerDescription"].ReadOnly = true;

                SetTagPropertyForStackerGrid();
                this.ResolveResources();
                //dgvDetails.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader);
                //dgvDetails.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.Fill);
                this.dgvDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error : Loading Stacker grid view : " + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        private void LoadScheduleGridView()
        {
            try
            {
                DropScheduleBiz objDropScheduleBiz = DropScheduleBiz.CreateInstance();
                List<DropScheduleEntity> objDropScheduleEntity = objDropScheduleBiz.GetDropSchedule(null);
                dgvDetails.DataSource = objDropScheduleEntity;
                dgvDetails.Columns["ScheduleID"].ReadOnly = true;
                dgvDetails.Columns["ScheduleName"].ReadOnly = true;
                dgvDetails.Columns["ScheduleName"].Visible = false;
                dgvDetails.Columns["ScheduleTime"].ReadOnly = true;
                dgvDetails.Columns["ScheduleTime"].DefaultCellStyle.Format = "HH:mm";
                dgvDetails.Columns["StackerLevelPercentage"].ReadOnly = true;
                dgvDetails.Columns["StackerLevelPercentage"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvDetails.Columns["ScheduleOccurance"].ReadOnly = true;
                dgvDetails.Columns["ScheduleOccurance"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvDetails.Columns["StartDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvDetails.Columns["EndDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvDetails.Columns["StartDate"].ReadOnly = true;
                dgvDetails.Columns["EndDate"].ReadOnly = true;
                dgvDetails.Columns["EndOption"].ReadOnly = true;
                dgvDetails.Columns["EndOption"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvDetails.Columns["TotalOccurances"].ReadOnly = true;
                dgvDetails.Columns["TotalOccurances"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvDetails.Columns["WeekDays"].ReadOnly = true;
                dgvDetails.Columns["WeekDays"].Visible = false;
                dgvDetails.Columns["SelectedWeekDays"].ReadOnly = true;
                dgvDetails.Columns["SelectedWeekDays"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvDetails.Columns["DayOfMonth"].ReadOnly = true;
                dgvDetails.Columns["MonthDuration"].ReadOnly = true;
                //dgvDetails.Columns["IsExpiry"].ReadOnly = true;
                //dgvDetails.Columns["IsExpiry"].Visible = false;
                dgvDetails.Columns["NextOcc"].ReadOnly = true;
                dgvDetails.Columns["NextOcc"].Visible = false;
                dgvDetails.Columns["IsActive"].ReadOnly = true;
                dgvDetails.Columns["IsActive"].Visible = false;
                dgvDetails.Columns["DropAlertType"].Visible = false;
                dgvDetails.Columns["RegionId"].Visible = false;
                dgvDetails.Columns["SiteId"].Visible = false;

                btnAddNew.Enabled = (dgvDetails.Rows.Count <= 0);

                SetTagPropertyForScheduleGrid();
                this.ResolveResources();
                dgvDetails.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error : Loading Schedule grid view : " + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        #endregion

        private void frmStackerDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void SetTagProperty()
        {
            this.Tag = "Key_DropSchedule";          // "Drop Schedule";
            this.btnAddNew.Tag = "Key_AddCaption";
            this.btnClose.Tag = "Key_CloseCaption";
            this.btnMannualDropAlert.Tag = "Key_ManualDropAlertCaption";
            this.dgvbtnDelete.Tag = "Key_DeleteCaption";
            this.dgvbtnEdit.Tag = "Key_EditCaption";
            this.dgvDetails.Columns["dgvbtnEdit"].Tag = "Key_StackerEdit";
            this.dgvDetails.Columns["dgvbtnDelete"].Tag = "Key_StackerDelete";
        }

        private void SetTagPropertyForScheduleGrid()
        {
            if (dgvDetails.ColumnCount > 2)
            {
                dgvDetails.Columns["ScheduleID"].Tag = "Key_ScheduleID";
                dgvDetails.Columns["ScheduleName"].Tag = "Key_ScheduleName";
                dgvDetails.Columns["ScheduleTime"].Tag = "Key_ScheduleTime";
                dgvDetails.Columns["StackerLevelPercentage"].Tag = "Key_StackerLevelPercentage";
                dgvDetails.Columns["ScheduleOccurance"].Tag = "Key_ScheduleOccurance";
                dgvDetails.Columns["StartDate"].Tag = "Key_StartDate";
                dgvDetails.Columns["EndDate"].Tag = "Key_EndDate";
                dgvDetails.Columns["EndOption"].Tag = "Key_EndOption";
                dgvDetails.Columns["TotalOccurances"].Tag = "Key_TotalOccurances";
                dgvDetails.Columns["WeekDays"].Tag = "Key_WeekDays";
                dgvDetails.Columns["SelectedWeekDays"].Tag = "Key_SelectedWeekDays";
                dgvDetails.Columns["DayOfMonth"].Tag = "Key_DayOfMonth";
                dgvDetails.Columns["MonthDuration"].Tag = "Key_MonthDuration";
                dgvDetails.Columns["NextOcc"].Tag = "Key_NextOcc";
                dgvDetails.Columns["IsActive"].Tag = "Key_IsActive";
                dgvDetails.Columns["DropAlertType"].Tag = "Key_DropAlertType";
                dgvDetails.Columns["RegionId"].Tag = "Key_RegionId";
                dgvDetails.Columns["SiteId"].Tag = "Key_SiteId";
               
            }
           
        }

        private void SetTagPropertyForStackerGrid()
        {
            if (dgvDetails.ColumnCount > 2)
            {
                dgvDetails.Columns["StackerId"].Tag = "Key_StackerId";
                dgvDetails.Columns["StackerName"].Tag = "Key_StackerName";
                dgvDetails.Columns["StackerSize"].Tag = "Key_StackerSize";
                dgvDetails.Columns["StackerStatus"].Tag = "Key_StackerStatus";
                dgvDetails.Columns["StackerDescription"].Tag = "Key_StackerDescription";
               
            }
            
        }
    }
}
