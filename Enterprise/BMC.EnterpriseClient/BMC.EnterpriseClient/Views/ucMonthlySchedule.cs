using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.Common;
namespace BMC.EnterpriseClient
{
    public partial class ucMonthlySchedule : UserControl
    {
        public ucMonthlySchedule()
        {
            InitializeComponent();
            SetTagProperty();
        }
        private void SetTagProperty()
        {
            try
            {

                this.label2.Tag = "Key_1month";
                this.lblDay.Tag = "Key_Day";
                this.label4.Tag = "Key_months";
                this.label1.Tag = "Key_ofevery";
                this.label3.Tag = "Key_ofevery";
                this.rdThefirstdayofmonth.Tag = "Key_The";
            }
            catch (Exception ex)
            {
                throw(ex);
            }
        }

        private void rdDayofmonth_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void rdThefirstdayofmonth_CheckedChanged(object sender, EventArgs e)
        {
        }

        public int DateOfMonth
        {
            get
            {
                return Convert.ToInt32(nudDayOfMonth.Value); ;
            }
            set
            {
                nudDayOfMonth.Value = value;
            }
        }

        private int _monthDuration = 1;
        public int MonthDuration
        {            
            get
            {
                //if (cmbMonth.SelectedItem != null)
                //{
                //    return Convert.ToInt32(cmbMonth.SelectedItem.ToString());
                //}
                //else
                //{
                //    return -1;
                //}                
                return _monthDuration;
            }
            set
            {
                ////cmbMonth.SelectedItem = value;
                //if (value>0)
                //    cmbMonth.SelectedIndex = value-1;
                //else
                //    cmbMonth.SelectedIndex = 0;
                _monthDuration = 1;
            }
        }
        public string Week
        {
            get
            {
                return cmbweekofmonth.ToString();
            }
            set
            {
                cmbweekofmonth.SelectedItem = value;
            }
        }
        public string Day
        {
            get
            {
                return cmbdayofweek.ToString();
            }
            set
            {
                cmbdayofweek.SelectedItem = value;
            }
        }
        //public string MonthDuration2
        //{
        //    get
        //    {
        //        return cmbMonth2.ToString();
        //    }
        //    set
        //    {
        //        cmbMonth2.SelectedItem = value;
        //    }
        //}

        //private void txtdateofMonth_Leave(object sender, EventArgs e)
        //{
            //if (!String.IsNullOrEmpty(txtDateOfMonth.Text.Trim()))
            //{
            //    try
            //    {
            //        int idate = Convert.ToInt32(txtDateOfMonth.Text);
            //        if (idate > 31)
            //        {
            //            MessageBox.Show("Date specified is not in valid range", "Info", MessageBoxButtons.OK);
            //            txtDateOfMonth.Focus();
            //        }
            //    }
            //    catch
            //    {
            //        MessageBox.Show("Date specified is not valid", "Info", MessageBoxButtons.OK);
            //        txtDateOfMonth.Focus();
            //    }
            //}
        //}

        private void ucMonthlySchedule_Load(object sender, EventArgs e)
        {
            //cmbMonth.SelectedIndex = 0;
        }
        
    }
}
