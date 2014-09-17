using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class ucRecurrenceRange : UserControl
    {
        public ucRecurrenceRange()
        {
            InitializeComponent();            
            rdNoenddate_CheckedChanged(null, null);

            // Set Tags for controls
            SetTagProperty();
        }

        private EndOptions _EndOption;
        public EndOptions EndOption
        {
            get
            {
                if (rdNoenddate.Checked)
                {
                    _EndOption = EndOptions.NoEndDate;
                    rdNoenddate_CheckedChanged(null, null);
                }
                else if (rdEndby.Checked)
                    _EndOption = EndOptions.EndByDate;
                else
                    _EndOption = EndOptions.EndAfterOccurance;
                return _EndOption;
            }
            set
            {
                if (EndOptions.NoEndDate == value)
                    rdNoenddate.Checked = true;
                else if (EndOptions.EndByDate == value)
                    rdEndby.Checked = true;
                else 
                    rdEndafter.Checked = true; 
            }
        }

        private void SetTagProperty()
        {
            this.rdEndafter.Tag = "Key_EndafterColon";
            this.rdEndby.Tag = "Key_EndbyColon";
            this.rdNoenddate.Tag = "Key_Noenddate";
            this.label2.Tag = "Key_occurrences";
            this.grpRecurrenceRange.Tag = "Key_Rangeofrecurrence";
            this.label1.Tag = "Key_StartColon";
        }

        private void rdNoenddate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdNoenddate.Checked == true)
            {              
                //dtEndDate.Value = dtEndDate.MaxDate;
                dtEndDate.Enabled = false;
                nudOcc.Enabled = false;
                nudOcc.Value = 0;
            }
        }

        private void rdEndafter_CheckedChanged(object sender, EventArgs e)
        {
            if (rdEndafter.Checked == true)
            {
                //dtEndDate.Value = dtEndDate.MaxDate;
                dtEndDate.Enabled = false;
                nudOcc.Enabled = true;
                nudOcc.Value = 1;
                nudOcc.Focus();
            }
        }

        private void rdEndby_CheckedChanged(object sender, EventArgs e)
        {
            if (rdEndby.Checked == true)
            {
                //dtEndDate.Value = DateTime.Now;
                nudOcc.Enabled = false;
                nudOcc.Value = 0;
                dtEndDate.Enabled = true;
            }
        }

        private void txtOccurrences_Leave(object sender, EventArgs e)
        {
            //if (!String.IsNullOrEmpty(txtOccurrences.Text.Trim()))
            //{
            //    try
            //    {
            //        int iOccur = Convert.ToInt32(txtOccurrences.Text.Trim());
            //        if (iOccur == 0)
            //        {
            //        MessageBox.Show("Minimum Occurrece must be 1", "Info", MessageBoxButtons.OK);
            //        txtOccurrences.Focus();
            //    }
            //    }
            //    catch
            //    {
            //        MessageBox.Show("Occurrences specified is not valid number", "Info", MessageBoxButtons.OK);
            //        txtOccurrences.Focus();
            //    }
            //}
        }
        public DateTime StartDate
        {
            get
            {
                return dtStartdate.Value;
            }
            set
            {
                dtStartdate.Value = value;
                dtStartdate.Value = new DateTime(value.Year, value.Month, value.Day, 0, 0, 0);
            }
        }

        public int Occurrence
        {
            get
            {
                return Convert.ToInt32(nudOcc.Value);
            }
            set
            {
                nudOcc.Value = value;
            }
        }

        public bool NoEndDate
        {
            get
            {
                return rdNoenddate.Checked;
            }
            set
            {
                rdNoenddate.Checked = value;
            }
        }

        public bool EndAfterOccurrence
        {
            get
            {
                return rdEndafter.Checked;
            }
            set
            {
                rdEndafter.Checked = value;
            }
        }
        public bool EndByDate
        {
            get
            {
                return rdEndby.Checked;
            }
            set
            {
                rdEndby.Checked = value;
            }
        }

        public DateTime EndDate
        {
            get
            {
                return dtEndDate.Value;
            }
            set
            {
                dtEndDate.Value = new DateTime(value.Year, value.Month, value.Day, 0, 0, 0);
                if (dtEndDate.Value < dtStartdate.Value)
                    dtEndDate.Value = dtEndDate.MaxDate;

            }
        }

        private void ucRecurrenceRange_Load(object sender, EventArgs e)
        {
            // For externalization
            this.ResolveResources();
        }
    }
}
