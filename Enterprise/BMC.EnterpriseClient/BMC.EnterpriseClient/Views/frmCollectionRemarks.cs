using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.CoreLib.Win32;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmCollectionRemarks : Form
    {
        private int collection_id;
        private string Collection_Remarks;
         BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;


        public frmCollectionRemarks(int collection_id,string Collection_Remarks)
        {
            InitializeComponent();
            this.collection_id = collection_id;
            this.Collection_Remarks = Collection_Remarks;
            txtRemarks.Text = Collection_Remarks;
            objDatawatcher = new Helpers.Datawatcher(this);
            
            // Set Tags for controls
            SetTagProperty();

            // For externalization
            this.ResolveResources();
        }

        private void SetTagProperty()
        {
            this.Tag = "Key_CollectionRemarks";
            btnUpdate.Tag = "Key_Update";
            btnClose.Tag = "Key_Close";
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (txtRemarks.Text == "")
                {
                  this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_REMARKS_EMPTY"),this.Text);          // "Remarks text should not be empty.");
                  return;
                }
                ViewSitesBusiness ViewSitesBusinessobj = new ViewSitesBusiness();
                ViewSitesBusinessobj.UpdateRemarksCollection(collection_id, txtRemarks.Text);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    }
}
