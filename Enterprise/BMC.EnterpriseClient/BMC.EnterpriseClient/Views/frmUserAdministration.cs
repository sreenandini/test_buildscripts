using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.Common.ExceptionManagement;
using System.IO;
using BMC.Common.LogManagement;
using BMC.SecurityVB;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmUserAdministration : Form
    {
        private string _UserName;
        private int _UserID;
        private bool _InvokeFromAssetMgt = false;

        public frmUserAdministration()
        {
            InitializeComponent();
            SetTagProperty();
        }

        public frmUserAdministration(string UserName, int UserID)
        {
            this._UserName = UserName;
            this._UserID = UserID;
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            SetTagProperty();
        }

        public frmUserAdministration(string UserName, int UserID, bool InvokeFromAssetMgt)
        {
            this._UserName = UserName;
            this._UserID = UserID;
            InitializeComponent();
            SetTagProperty();
            this._InvokeFromAssetMgt = InvokeFromAssetMgt;
            if (this._InvokeFromAssetMgt = InvokeFromAssetMgt)
            {
                this.Size = new Size(1520, 750);
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
            if (InvokeFromAssetMgt && AppGlobals.Current.HasUserAccess("HQ_Admin_Users"))
            {
                tsbtnUsers_Click(this, null);
            }
        }

        #region Events


        private void frmUserAdministration_Load(object sender, EventArgs e)
        {
            if (_InvokeFromAssetMgt)
            {
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
            this.ResolveResources();
        }

        #endregion Events

        #region Methods



        #endregion //Methods



        /// <summary>
        /// This is used for the resource externalization
        /// </summary>
        private void SetTagProperty()
        {
            this.Tag = "Key_UserAdmin";
        }

        /// <summary>
        /// This event handler is used to load the user group user control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnUserRoles_Click(object sender, EventArgs e)
        {
            try
            {
                tsbtnUserRoles.Checked = true;
                tsbtnUserSiteAccess.Checked = false;
                tsbtnUsers.Checked = false;
                tsbtnAssignSites.Checked = false;

                grpControls.Controls.Clear();
                ucUserGroup userGroup = new ucUserGroup(this._UserID, this._UserName);
                userGroup.Dock = DockStyle.Fill;
                this.grpControls.Text = this.GetResourceTextByKey("Key_UserRoleAdministration");// "User Role Administration";
                grpControls.Controls.Add(userGroup);
                grpControls.Controls["ucUserGroup"].Show();
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// This event handler is used to load the user site access.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnUserSiteAccess_Click(object sender, EventArgs e)
        {

            try
            {
                tsbtnUserRoles.Checked = false;
                tsbtnUserSiteAccess.Checked = true;
                tsbtnUsers.Checked = false;
                tsbtnAssignSites.Checked = false;
                grpControls.Controls.Clear();
                ucUserSiteAccess userSiteAccess = new ucUserSiteAccess();
                userSiteAccess.Dock = DockStyle.Fill;
                this.grpControls.Text = this.GetResourceTextByKey("Key_UserSiteAccessAdministration");//"User Site Access Administration";
                grpControls.Controls.Add(userSiteAccess);
                grpControls.Controls["ucUserSiteAccess"].Show();
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// This event handler is used to load the user admin user control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnUsers_Click(object sender, EventArgs e)
        {
            try
            {
                tsbtnUserRoles.Checked = false;
                tsbtnUserSiteAccess.Checked = false;
                tsbtnUsers.Checked = true;
                tsbtnAssignSites.Checked = false;
                grpControls.Controls.Clear();
                ucUserAdmin users = new ucUserAdmin();
                grpControls.Controls.Add(users);
                users.Dock = DockStyle.Fill;
                this.grpControls.Text = this.GetResourceTextByKey("Key_UserAdministration");//"User Administration";
                grpControls.Controls["ucUserAdmin"].Show();
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// This event handler is used to load the site details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnAssignSites_Click(object sender, EventArgs e)
        {
            try
            {
                tsbtnUserRoles.Checked = false;
                tsbtnUserSiteAccess.Checked = false;
                tsbtnUsers.Checked = false;
                tsbtnAssignSites.Checked = true;

                LogManager.WriteLog("Inside btnSiteAccess_Click", LogManager.enumLogLevel.Info);
                if (File.Exists(Application.StartupPath + @"\BMCUserSiteAdmin.exe"))
                {
                    BMC.SecurityVB.BMCSecurityCallMethod BMCSecurityMethod = new BMCSecurityCallMethod();
                    AppEntryPoint.Current.StartProcess(sender, null, Application.StartupPath + @"\BMCUserSiteAdmin.exe", BMCSecurityMethod.Encrypt(_UserID.ToString()) + " " +
                            BMCSecurityMethod.Encrypt(_UserName.Replace(" ", "")) + " " + BMCSecurityMethod.Encrypt(_UserID.ToString()) + " " + BMCSecurityMethod.Encrypt("AssignSites"), true);

                }
                else
                {
                    BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_USUSER_ADMINPACKAGE"), this.Text);
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// This event handler is used to close the User administration screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
