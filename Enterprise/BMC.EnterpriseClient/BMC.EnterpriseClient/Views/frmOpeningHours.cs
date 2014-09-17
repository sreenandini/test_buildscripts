using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.CoreLib.Win32;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseBusiness.Business;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common;


namespace BMC.EnterpriseClient.Views
{
    public partial class frmOpeningHours : DialogFormBase
    {
        int _SiteID = 0;
        int _ZoneID = 0;

        public frmOpeningHours(int SiteID, int ZoneID)
        {
            InitializeComponent();
            _SiteID = SiteID;
            _ZoneID = ZoneID;
        }

        private void frmOpeningHours_Load(object sender, EventArgs e)
        {
            try
            {
                ucOpeningHour.EnableDisableContols("Edit", true);
                ucOpeningHour.LoadData(_SiteID, _ZoneID, 0, false, false);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        protected override void SaveChanges()
        {
            try
            {
                ucOpeningHour.SaveChanges();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

    }
}
