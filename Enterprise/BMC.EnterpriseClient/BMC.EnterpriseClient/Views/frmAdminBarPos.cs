using BMC.Common.ExceptionManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmAdminBarPos : Form
    {
        #region Members
        BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;
        #endregion

        #region Properties
        public bool btnEnable { set { btnApply.Enabled = value; } }
         #endregion 
       
        #region Constructors
         public frmAdminBarPos()
         {
             InitializeComponent();
             objDatawatcher = new Helpers.Datawatcher(this);

             // Set Tags for controls
             SetTagProperty();
         }

         private void SetTagProperty()
         {
             btnApply.Tag = "Key_Apply";
             btnCancel.Tag = "Key_CancelCaption";
         }

         public frmAdminBarPos(int BarPositionID, int SiteID, string BarPosName)
         {
             InitializeComponent();

             // Set Tags for controls
             SetTagProperty();
             try
             {
                 objDatawatcher = new Helpers.Datawatcher(this);
                 ucBarPosition1.BarPositionID = BarPositionID;
                 ucBarPosition1.SiteId = SiteID;
                 ucBarPosition1.BarPositionName = BarPosName;
                 ucBarPosition1.LoadDefault();
                 ucBarPosition1.LoadZoneCombo(SiteID);
                 this.tblMainFrame.Controls.Add(ucBarPosition1);
                 objDatawatcher.DataModify = false;
             }
             catch (Exception ex)
             {
                 ExceptionManager.Publish(ex);
             }
         }
         #endregion
         
        #region Events
        private void ucBarPosition1_Load(object sender, EventArgs e)
        {
            try
            {
                // For externalization
                this.ResolveResources();

                this.Text = ucBarPosition1.title;
                objDatawatcher.DataModify = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (ucBarPosition1.ValidateAndSaveData())
                {
                    objDatawatcher.DataModify = false;
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            { 
                this.Close(); 
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion
       
       



    }
}
