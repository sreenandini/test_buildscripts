using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.CoreLib.Win32;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.Common;
using BMC.EnterpriseClient.Helpers;
using System.Collections;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmShareSchedules : Form
    {
        #region Variables
        ShareScheduleBusiness objShareScheduleBusiness = new ShareScheduleBusiness();
        private ListViewCustomSorter _lvCustomSorter = null;
        #endregion

        public frmShareSchedules()
        {
            InitializeComponent();
            lvShareSchedules.ClipboardCopyMode = ListViewClipboardCopyMode.EnableWithHeaderText;
            lvShareSchedules.ClipboardCopyFormat = ListViewClipboardCopyFormat.Semicolon;
            _lvCustomSorter = new ListViewCustomSorter(lvShareSchedules, this);
            SetTagProperty();
        }

        #region Events
        private void frmShareSchedules_Load(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside frmShareSchedules_Load...", LogManager.enumLogLevel.Info);
                LoadListViewShareSchedule();
                this.ResolveResources();
                lvShareSchedules.ContextMenuStrip = ctMenuItems;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void IDM_EDIT_SCHEDULE_Click(object sender, EventArgs e)
        {
            EditSchedule();
        }

        private void IDM_NEW_SCHEDULE_Click(object sender, EventArgs e)
        {
            AddSchedule();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddSchedule();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditSchedule();
        }

        private void lvShareSchedules_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside lvShareSchedules_MouseDoubleClick...", LogManager.enumLogLevel.Info);
                if (lvShareSchedules.SelectedItems != null)
                {
                    ShareScheduleEntity objEntity = (ShareScheduleEntity)lvShareSchedules.SelectedItems[0].Tag;
                    if (!BMC.CoreLib.Win32.Win32Extensions.ShowDialogExAndDestroy
                        (new frmAddShareSchedule(true, objEntity), this, null, null))
                    {
                        LoadListViewShareSchedule();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void lvShareSchedules_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside lvShareSchedules_MouseUp...", LogManager.enumLogLevel.Info);
                if (lvShareSchedules.SelectedItems != null)
                {
                    IDM_EDIT_SCHEDULE.Text = this.GetResourceTextByKey("Key_Edit_WOShortCut");
                    ShareScheduleEntity objEntity = (ShareScheduleEntity)lvShareSchedules.SelectedItems[0].Tag;
                    IDM_EDIT_SCHEDULE.Text = IDM_EDIT_SCHEDULE.Text + " " + objEntity.Share_Schedule_Name;
                }
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
        #endregion
        #region PrivateMethods
        private void SetTagProperty()
        {
            this.Tag = "Key_ShareSchedules";
            this.IDM_EDIT_SCHEDULE.Text = this.GetResourceTextByKey("Key_Edit_WOShortCut");
            this.IDM_NEW_SCHEDULE.Text = this.GetResourceTextByKey("Key_New_WOShortCut");
            this.clmHeaderName.Tag = "Key_Name";
            this.clmHeaderDescription.Tag = "Key_Description";
            this.clmHeaderNoofBands.Tag = "Key_NoofBands";
            this.clmHeaderStartDate.Tag = "Key_StartDate";
            this.btnClose.Tag = "Key_Close";
            this.btnAdd.Tag = "Key_New_WOShortCut";
            this.btnEdit.Tag = "Key_Edit_WOShortCut";
        }

        private void LoadListViewShareSchedule()
        {
            try
            {
                LogManager.WriteLog("Inside LoadListViewShareSchedule...", LogManager.enumLogLevel.Info);
                lvShareSchedules.Items.Clear();
                List<ShareScheduleEntity> lstShareSchedule = objShareScheduleBusiness.GetShareScheduleDetails();
                foreach (ShareScheduleEntity entity in lstShareSchedule)
                {
                    ListViewItem item = new ListViewItem();
                    item.Tag = entity;
                    item.Text = entity.Share_Schedule_Name;
                    item.SubItems.Add(entity.Share_Schedule_Description);
                    item.SubItems.Add(entity.Share_Schedule_No_Bands.ToString());
                    item.SubItems.Add(Convert.ToDateTime(entity.Share_Schedule_Start_Date).ToString("dd/MM/yyyy"));
                    lvShareSchedules.Items.Add(item);
                }
                lvShareSchedules.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void AddSchedule()
        {
            try
            {
                LogManager.WriteLog("Inside IDM_NEW_SCHEDULE_Click...", LogManager.enumLogLevel.Info);
                if (!BMC.CoreLib.Win32.Win32Extensions.ShowDialogExAndDestroy
                        (new frmAddShareSchedule(false, null), this, null, null))
                {
                    LoadListViewShareSchedule();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void EditSchedule()
        {
            try
            {
                LogManager.WriteLog("Inside IDM_EDIT_SCHEDULE_Click...", LogManager.enumLogLevel.Info);
                ShareScheduleEntity objEntity = (ShareScheduleEntity)lvShareSchedules.SelectedItems[0].Tag;
                if (objEntity != null)
                {

                    if (!BMC.CoreLib.Win32.Win32Extensions.ShowDialogEx
                       (new frmAddShareSchedule(true, objEntity), this, null, null))
                    {
                        LoadListViewShareSchedule();
                    }
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
