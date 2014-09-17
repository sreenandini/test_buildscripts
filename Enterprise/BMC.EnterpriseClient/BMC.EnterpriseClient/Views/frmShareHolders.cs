using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business;
using BMC.Common;
namespace BMC.EnterpriseClient.Views
{
    public partial class ShareHolders : Form
    {
        public ShareHolders()
        {
            InitializeComponent();
            SetTagProperty();
            LoadDefaultShareHolders();
        }

        private void SetTagProperty()
        {
            this.btnCancel.Tag = "Key_Cancel";
            this.Tag = "Key_frmShareHolders";
            this.grpMandatoryShareHolders.Tag = "Key_MandatoryShareHolders";
            this.btnSave.Tag = "Key_SaveCaption";

        }

        private void LoadDefaultShareHolders()
        {
            this.ResolveResources();
            //ShareHoldersBiz ShareHoldersBiz = new ShareHoldersBiz();
       //     grdvwDefaultShareHolders.DataSource = ShareHoldersBiz.getDefaultShareHolders();
           
        }


    }
}
