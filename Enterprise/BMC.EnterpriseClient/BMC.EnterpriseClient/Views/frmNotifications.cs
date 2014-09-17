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
using BMC.Common.ExceptionManagement;
using BMC.CoreLib.Win32;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmNotifications : Form
    {
        #region Declared Variables
        
        NotificationBusiness objBusiness = new NotificationBusiness();
        List<GetNotificationsEntity> _lstNotifications = null;
        GetNotificationsEntity _objNotification = null;

        #endregion

        #region Constructor
        
        public frmNotifications()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        private void frmNotifications_Load(object sender, EventArgs e)
        {
            try
            {
                GetNotifications();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            try
            {
                lbl_Status.Text = string.Empty;
                string sNotificationId = string.Empty;
                if (lv_Notifications.Items.Count > 0)
                {
                    foreach (ListViewItem lvitems in lv_Notifications.Items)
                    {
                        _objNotification = (GetNotificationsEntity)lvitems.Tag;
                        if (lvitems.Checked)
                        {
                            sNotificationId += "," + _objNotification.NotificationID.ToString();
                        }
                    }

                    if (!String.IsNullOrEmpty(sNotificationId))
                    {
                        sNotificationId = sNotificationId.Remove(0, 1);

                    }
                    if (!String.IsNullOrEmpty(sNotificationId))
                    {
                        objBusiness.UpdateNotifications(sNotificationId,AppGlobals.Current.UserId);
                        cb_SelectAll.Checked = false;
                        GetNotifications();
                    }
                    else
                    {
                        lbl_Status.Text = "Select notifications to clear.";
                        return;
                    }
                }
                else
                {
                    lbl_Status.Text = "No notifications to clear";
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            try
            {
                GetNotifications();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cb_SelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (_lstNotifications.Count > 0)
                {
                    foreach (ListViewItem lvitems in lv_Notifications.Items)
                    {
                        lvitems.Checked = cb_SelectAll.Checked;
                    }
                }
                else
                {
                    lbl_Status.Text = "No notifications to select/unselect";
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region LoadMethods

        private void GetNotifications()
        {
            lv_Notifications.Items.Clear();
            lbl_Status.Text = string.Empty;
            List<GetNotificationsEntity> notificationList = objBusiness.GetNotifications();
            _lstNotifications = notificationList;
            if (notificationList != null)
            {
                foreach (GetNotificationsEntity items in notificationList)
                {
                    ListViewItem lvItem = new ListViewItem();
                    lvItem.Tag = items;
                    lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, items.NotificationItem.ToString()));
                    lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, items.Notifications.ToString()));
                    lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, items.NotifiedDate.ToString()));
                    lv_Notifications.Items.Add(lvItem);
                }

                if (lv_Notifications.Items.Count <= 0)
                {
                    cb_SelectAll.Enabled = false;
                    lbl_Status.Text = "No notifications found.";
                }
                else
                {
                    cb_SelectAll.Enabled = true;
                }
            }
        }

        #endregion

        #region Miscellaneous Methods

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) this.Close();
            bool res = base.ProcessCmdKey(ref msg, keyData);
            return res;
        }

        #endregion

    }
}
