using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business.UserSiteAccess;
using BMC.CoreLib.Win32;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmNewCustomeraccessGroup : Form
    {
        public string NewGroup { get; set; }

        UserSiteAccess _Access = null;
         BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;

        public frmNewCustomeraccessGroup()
        {
            InitializeComponent();
            SetTagProperty();
            this.ResolveResources();
            _Access = UserSiteAccess.CreateInstance();
            objDatawatcher = new Helpers.Datawatcher(this);
        }

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.Tag = "Key_CustomerAccessGroup";
            this.btnCancel.Tag = "Key_CancelCaption";
            this.groupBox1.Tag = "Key_CustomerAccessGroup";
            this.label1.Tag = "Key_Enterthenewcustomeraccessgroupname";
            this.btnSave.Tag = "Key_SaveCaption";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CreateNewGroup();            
        }

        #region User Function
        public void CreateNewGroup()
        {
            //Validate input group name
            if (string.IsNullOrEmpty(txtCustomerAccessGroupname.Text.Trim()))
            {
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CUST_ACCESS_GRP_CANNOT_EMPTY"), this.Text);
                return;
            }

            //Save the customer access group
            switch (_Access.UpdateCustomerAccess(txtCustomerAccessGroupname.Text.Trim()))
            {
                case 0:
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CUST_ACCESS_DETAILS_SAVE_SUCCESS"), this.Text);
                        this.DialogResult = DialogResult.OK;
                        NewGroup = txtCustomerAccessGroupname.Text.Trim();
                        break;
                    }
                case 1:
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CUST_ACCESS_DETAILS_EXISTS"), this.Text);
                        this.DialogResult = DialogResult.Cancel;
                        break;
                    }
                default:
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CUST_ACCESS_DETAILS_SAVE_FAILED"), this.Text);
                        this.DialogResult = DialogResult.Cancel;
                        break;
                    }
            }

            objDatawatcher.DataModify = false;

            this.Close();
        }
        #endregion User Function
    }
}
