using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BMC.CentralisedSiteSettings.DataAccess;
using BMC.Common;
using BMC.CoreLib.Win32;

namespace BMC.CentralisedSiteSettings.Presentation
{
    public partial class SaveProfile : Form
    {
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
                txtProfileName.Text = "";
                //MessageBox.Show(this, this.GetResourceTextByKey(1, "MSG_PROFILE_NAME_CANNOT_BLANK"), this.GetResourceTextByKey(1, "MSG_APP_TITLE"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_PROFILE_NAME_CANNOT_BLANK"), this.Text);
            }
            else
            {                
                ProfileNameExists = DBHelper.InsertProfile(txtProfileName.Text.Trim());
                if (ProfileNameExists == -1)
                {
                    //MessageBox.Show(this, this.GetResourceTextByKey(1, "MSG_PROFILE_NAME_EXISTS"), this.GetResourceTextByKey(1, "MSG_APP_TITLE"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_PROFILE_NAME_EXISTS"), this.Text);
                    txtProfileName.Text = string.Empty;
                    txtProfileName.Focus();
                    return;
                }
                OkClickEvent.Invoke(sender, e, txtProfileName.Text.Trim());
                this.Close();
            }
        }
        }
    }