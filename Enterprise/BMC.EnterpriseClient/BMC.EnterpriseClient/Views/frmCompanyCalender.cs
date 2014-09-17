using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseClient.Views;
using Microsoft.VisualBasic;
using BMC.EnterpriseBusiness.Business;
using BMC.Common.ExceptionManagement;
using BMC.Common;
using BMC.EnterpriseBusiness.Entities;
namespace BMC.EnterpriseClient.Views
{
    public partial class frmCompanyCalender : Form
    {     
        private CalendarBusiness objCalendarBiz = new CalendarBusiness();
         BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;


        public frmCompanyCalender()
        {

            try
            {
                InitializeComponent();
                SetTagproperty();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            
        }

        private void SetTagproperty()
        {
            try
            {
                this.btnClose.Tag = "Key_CloseCaption";
                this.tabPage1.Tag = "Key_Calendars";
                this.tabPage2.Tag = "Key_Companies";
                this.Tag = "Key_FinancialCalender";
                this.tabPage3.Tag = "Key_Operators";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                objCalendarBiz.CalendarClose();
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void frmCompanyCalender_Load(object sender, EventArgs e)
        {
            try
            {
                this.ResolveResources();
                if (!SettingsEntity.IsAutoCalendarEnabled)
                {
                    SSTab1.TabPages.Remove(tbAutoCalendar);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SSTab1_Click(object sender, EventArgs e)
        {
            try
            {
                int ActiveTabPage = SSTab1.SelectedIndex;
                switch (ActiveTabPage)
                {
                    case 0:
                        ucCalender.LoadCalendars();
                        break;
                    case 1:
                        ucCompanyCalender1.LoaducCompanyCalendar();
                        break;

                    case 2:
                        ucOperatorCalendar1.LoadSupplierDetails();
                        break;
                    case 3:
                        ucAutoCalendar1.GetAutoCalendarProfiles();
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    }
}