using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business;
using BMC.CoreLib.Win32;
using BMC.Common;
using BMC.Common.ExceptionManagement;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmEmployeeCardTypes : Form
    {
        #region Declarations
        private EmployeeCardBiz objEmployeeCardBiz = null;
        BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;

        #endregion
        public frmEmployeeCardTypes()
        {
            InitializeComponent();
            objEmployeeCardBiz = EmployeeCardBiz.CreateInstance();
            objDatawatcher = new Helpers.Datawatcher(this);
            SetPropertyTag();
        }

        private void SetPropertyTag()
        {
            try
            {
                this.label1.Tag = "Key_CardTypeColon";
                this.grpCardTypes.Tag = "Key_CardTypes";
                this.btnClose.Tag = "Key_Close";
                this.btnCardTypes.Tag = "Key_SaveCaption";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnCardTypes_Click(object sender, EventArgs e)
        {
            if (objEmployeeCardBiz.InsertEmployeeCardTypes(txtCardType.Text))
            {
                this.ShowInfoMessageBox("Employee card Types inserted successfully");
            }
            else
                this.ShowInfoMessageBox("Employee card Type already exists");

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEmployeeCardTypes_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
        }
    }
}
