using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseBusiness.Business;
using BMC.Common.ExceptionManagement;
using BMC.CoreLib.Win32;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class ucSiteMaintenance : UserControl, IAdminSite
    {
        #region Local Declartion

        #region Objects
        private SiteMaintenanceBiz _objSiteMaintenanceBiz = null;
        List<TransactionKeyStatus> _lstTransactionKeyStatus;
        ListViewItem[] lvItem;
        AdminSiteEntity _AdminSite = null;
        #endregion Objects

        #endregion Local Declartion

        #region Drived Methods
        //Methods of IAdminSite Interface
        public void LoadDetails(AdminSiteEntity entity)
        {
            _AdminSite = entity;
            if (_AdminSite.Site_ID == 0)
            {
                tblContainer.Enabled = false;
            }
            else
            {
                tblContainer.Enabled = AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Site_Edit");
            }
        }

        public bool SaveDetails(AdminSiteEntity entity)
        {
            return true;//No Save Required
        }
        #endregion Drived Methods

        #region Events
        public ucSiteMaintenance()
        {
            InitializeComponent();
            _objSiteMaintenanceBiz = new SiteMaintenanceBiz();
            _lstTransactionKeyStatus = null;
            lvItem = null;

            // Set Tags for controls
            SetTagProperty();
        }

        private void SetTagProperty()
        {
            this.clmCreatedDate.Tag = "Key_CreatedDate";
            this.clmExpiryDate.Tag = "Key_ExpiryDate";
            this.rdbtnFactoryReset.Tag = "Key_FactoryReset";
            this.clmStaff_First_Name.Tag = "Key_FirstName";
            this.btnCreateAuthorizationKey.Tag = "Key_GenerateAuthorizationKey";
            this.clmStaff_Last_Name.Tag = "Key_LastName";
            this.rdbtnNewSite.Tag = "Key_NewSite";
            this.btnRefresh.Tag = "Key_Refresh";
            this.rdbtnSiteRecovery.Tag = "Key_SiteRecovery";
            this.clmStatus.Tag = "Key_Status";
            this.clmTransactionKey.Tag = "Key_TransactionKey";
            this.clmTransactionFlagName.Tag = "Key_Type";
            this.ctxmenuItemVoid.Text = this.GetResourceTextByKey("Key_Void");
        }

        private void ucSiteMaintenance_Load(object sender, EventArgs e)
        {
            // For externalization
            this.ResolveResources();

            rdbtnSiteRecovery.Visible = false;
            RefreshTransactionKeyStatus();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.RefreshTransactionKeyStatus();
        }

        private void btnCreateAuthorizationKey_Click(object sender, EventArgs e)
        {
            try
            {
                string sAuthenticationKey = string.Empty;

                int iKeyType = 0;

                Random oRandom = new Random();

                if (rdbtnFactoryReset.Checked)
                    iKeyType = (int)AuthenticationKeyType.FactoryReset;
                else if (rdbtnSiteRecovery.Checked)
                    iKeyType = (int)AuthenticationKeyType.SiteRecovery;
                else if (rdbtnNewSite.Checked)
                    iKeyType = (int)AuthenticationKeyType.NewSite;
                else
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_SELECT_ACTION"), this.ParentForm.Text);     // "Please select any of the actions."
                    return;
                }

                sAuthenticationKey = (DateTime.Now.Second) + oRandom.Next(99, 9999).ToString();

                int ret_val = _objSiteMaintenanceBiz.UpdateAuthorizationKey(_AdminSite.Site_ID, AppEntryPoint.Current.StaffId, iKeyType, sAuthenticationKey);
                if (ret_val == -100)
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_CREATE_PROFILE"), this.ParentForm.Text);           // "No profile found, Please create a profile in site settings screen to continue.");
                    txtAuthorizationKey.Text = "";
                }
                else
                {
                    txtAuthorizationKey.Text = sAuthenticationKey;
                }

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void ctxmenuItemVoid_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvTransactionKey.SelectedItems.Count > 0)
                {
                    if (lvTransactionKey.SelectedItems[0].SubItems[6].Text.ToUpper().CompareTo("OPEN") == 0)
                        _objSiteMaintenanceBiz.UpdateTransactionKeyStatus((int)lvTransactionKey.SelectedItems[0].Tag, AppEntryPoint.Current.StaffId);
                    else
                        Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_CANNOT_VOID"), this.ParentForm.Text);   // "Sorry cannot void this record as it is already voided/closed");
                }
                else
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_SELECT_TRANSACTIONKEY"), this.ParentForm.Text);    // "Please select a transaction key to void");
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion Events

        #region User Methods
        private void RefreshTransactionKeyStatus()
        {
            try
            {
                lvTransactionKey.Items.Clear();
                _lstTransactionKeyStatus = _objSiteMaintenanceBiz.GetTransactionKeyStatus(_AdminSite.Site_ID);
                lvItem = new ListViewItem[_lstTransactionKeyStatus.Count];
                int iLVIndex = 0;
                foreach (var item in _lstTransactionKeyStatus)
                {
                    lvItem[iLVIndex] = new ListViewItem(new string[] { item.TransactionKey,
                        item.TransactionFlagName,
                        item.CreatedDate.ToString(),
                        item.ExpiryDate.ToString(),
                        item.Staff_First_Name,
                        item.Staff_Last_Name,
                        item.Status 
                    });

                    lvItem[iLVIndex++].Tag = item.TransactionKeyId;
                }

                lvTransactionKey.Items.AddRange(lvItem);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion User Methods


        #region IAdminSite Members



        #endregion
    }
}
