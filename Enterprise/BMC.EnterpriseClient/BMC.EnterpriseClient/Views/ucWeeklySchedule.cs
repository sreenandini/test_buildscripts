using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class ucWeeklySchedule : UserControl
    {
        public ucWeeklySchedule()
        {
            InitializeComponent();
            //chkSunday.Tag = WeekDays.Sunday;
            //chkMonday.Tag = WeekDays.Monday;
            //chkTuesday.Tag = WeekDays.Tuesday;
            //chkWednesday.Tag = WeekDays.Wednesday;
            //chkThursday.Tag = WeekDays.Thursday;
            //chkFriday.Tag = WeekDays.Friday;
            //chkSaturday.Tag = WeekDays.Saturday;           
            SetTagProperty();
        }

        public void SetTagProperty()
        {
            this.chkSunday.Tag = "Key_Sunday";
            this.chkMonday.Tag = "Key_Monday";
            this.chkTuesday.Tag = "Key_Tuesday";
            this.chkWednesday.Tag = "Key_Wednesday";
            this.chkThursday.Tag = "Key_Thursday";
            this.chkFriday.Tag = "Key_Friday";
            this.chkSaturday.Tag = "Key_Saturday";
        }

        private int GetSelectedWeekDays()
        {
            int result = 0;
            if (chkSunday.Checked)
                result += 1;
            if (chkMonday.Checked)
                result += 2;
            if (chkTuesday.Checked)
                result += 4;
            if (chkWednesday.Checked)
                result += 8;
            if (chkThursday.Checked)
                result += 16;
            if (chkFriday.Checked)
                result += 32;
            if (chkSaturday.Checked)
                result += 64;
            return result;
        }

        private void SetSelectedWeekDays(int WeekDays)
        {
            int result = 0;
            result = WeekDays & 1;
            chkSunday.Checked = (result == 1);

            result = WeekDays & 2;
            chkMonday.Checked = (result == 2);

            result = WeekDays & 4;
            chkTuesday.Checked = (result == 4);

            result = WeekDays & 8;
            chkWednesday.Checked = (result == 8);

            result = WeekDays & 16;
            chkThursday.Checked = (result == 16);
            
            result = WeekDays & 32;
            chkFriday.Checked = (result == 32);

            result = WeekDays & 64;
            chkSaturday.Checked = (result == 64);
        }

        public int SelectedWeekDays
        {
            get
            {
                return GetSelectedWeekDays();
            }
            set
            {
                SetSelectedWeekDays(value);
            }
        }

        private void ucWeeklySchedule_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
            
        }

    }
}
