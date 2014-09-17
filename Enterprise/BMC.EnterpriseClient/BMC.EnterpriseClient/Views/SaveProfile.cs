using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BMC.Common;
using BMC.CoreLib.Win32;
using BMC.EnterpriseBusiness.Business;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;

namespace BMC.EnterpriseClient.Views
{
    public partial class SaveProfile : Form
    {
        CentralizedSiteSettingBiz _CentralizedSiteSettingBiz = null;

        public delegate void OkClicked(object sender, EventArgs e, string strTextValue);
        public event OkClicked OkClickEvent;
        int ProfileNameExists = 0;

        public SaveProfile()
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
            this.btnSave.Tag = "Key_OKCaption";
            this.lblProfile.Tag = "Key_ProfileNameColon";
            this.Tag = "Key_NewProfile";
            this.ResolveResources();
        }

        private void SaveProfile_Load(object sender, EventArgs e)
        {
            _CentralizedSiteSettingBiz = CentralizedSiteSettingBiz.CreateInstance();
            this.ResolveResources();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtProfileName.Text.Trim() == string.Empty)
            {
                txtProfileName.Text = string.Empty;
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_PROFILE_NAME_CANNOT_BLANK"), this.Text);
            }
            else
            {
                if (txtProfileName.Text.Trim().ToUpper() == "SELECTPROFILE" || txtProfileName.Text.Trim().ToUpper() == "DEFAULTPROFILE" || txtProfileName.Text.Trim().ToUpper() == "UNASSIGNED")
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1,"MSG_ENTERVALIDPROFILENAME"));//Enter a valid profile name.
                    return;
                }
                try
                {
                    ProfileNameExists = _CentralizedSiteSettingBiz.InsertProfile(txtProfileName.Text.Trim());

                    if (ProfileNameExists == -1)
                    {
                        Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_PROFILE_NAME_EXISTS"), this.Text);
                        txtProfileName.Text = string.Empty;
                        txtProfileName.Focus();
                        return;
                    }
                    OkClickEvent.Invoke(sender, e, txtProfileName.Text.Trim());
                    this.Close();
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
            }
        }
    }
}