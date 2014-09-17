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

namespace BMC.EnterpriseClient.Views
{
    public partial class frmAGSCombination : Form
    {
        string EnrolmentFlagIndex = string.Empty;
        int EnrolmentFlagValueResult = 0;
         BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;

        int i;
        public frmAGSCombination()
        {
           
            InitializeComponent();
            
            // Set Tags for controls
            SetTagProperty(); 

            LoadControl();
            objDatawatcher = new Helpers.Datawatcher(this);
        }

        private void SetTagProperty()
        {
            this.Tag = "Key_AGSCombination";
            btnCancel.Tag = "Key_Cancel";
            btnOK.Tag = "Key_OK";
        }

        public void LoadControl()
        {
            // For externalization
            this.ResolveResources();

            string AGSValue=string.Empty;

            lvEnrolmentFlag.Items.Add("Serial");
            lvEnrolmentFlag.Items[0].Tag = 4;            
            lvEnrolmentFlag.Items.Add("Asset");
            lvEnrolmentFlag.Items[1].Tag = 8;
            lvEnrolmentFlag.Items.Add("GMU");
            lvEnrolmentFlag.Items[2].Tag = 16;
           

            for (i = 0; i < lvEnrolmentFlag.Items.Count; i++)
            {
                if (lvEnrolmentFlag.Items[i].Checked)
                {
                    lvEnrolmentFlag.Items[i].Checked = false;
                }
            }
            string strIsEnrolmentComplete = AdminBusiness.GetSetting("IsEnrolmentComplete", string.Empty);
            if (strIsEnrolmentComplete.Trim().ToUpper() == "TRUE")
            {
                lvEnrolmentFlag.Enabled = false;
                btnOK.Enabled = false;
            }
            else
            {
                lvEnrolmentFlag.Enabled = true;
                btnOK.Enabled = true;
            }

            // Load Enrolment Flags

            AGSValue = AdminBusiness.GetSetting("AGSValue", "0");
            switch (AGSValue)
            {
                case "4":
                    lvEnrolmentFlag.Items[0].Checked = true;
                    lvEnrolmentFlag.Items[1].Checked = false;
                    lvEnrolmentFlag.Items[2].Checked = false;
                    break;
                case "8":
                    lvEnrolmentFlag.Items[0].Checked = false;
                    lvEnrolmentFlag.Items[1].Checked = true;
                    lvEnrolmentFlag.Items[2].Checked = false;
                    break;
                case "16":
                    lvEnrolmentFlag.Items[0].Checked = false;
                    lvEnrolmentFlag.Items[1].Checked = false;
                    lvEnrolmentFlag.Items[2].Checked = true;
                    break;
                case "12":
                    lvEnrolmentFlag.Items[0].Checked = true;
                    lvEnrolmentFlag.Items[1].Checked = true;
                    lvEnrolmentFlag.Items[2].Checked = false;
                    break;
                case "20":
                    lvEnrolmentFlag.Items[0].Checked = true;
                    lvEnrolmentFlag.Items[1].Checked = false;
                    lvEnrolmentFlag.Items[2].Checked = true;
                    break;
                case "24":
                    lvEnrolmentFlag.Items[0].Checked = false;
                    lvEnrolmentFlag.Items[1].Checked = true;
                    lvEnrolmentFlag.Items[2].Checked = true;
                    break;
                case "28":
                    lvEnrolmentFlag.Items[0].Checked = true;
                    lvEnrolmentFlag.Items[1].Checked = true;
                    lvEnrolmentFlag.Items[2].Checked = true;
                    break;
                default:
                    break;
            }
        }

        private void frmAGSCombination_Load(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
           
             for (i = 0; i < lvEnrolmentFlag.Items.Count; i++)
            {
                if (lvEnrolmentFlag.Items[i].Checked)
                {
                    EnrolmentFlagValueResult = EnrolmentFlagValueResult | Convert.ToInt32(lvEnrolmentFlag.Items[i].Tag);
                }
            }

             if (EnrolmentFlagValueResult == 0)
             {
                 this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_AGSCOMBINATION_SELECT"),this.Text);            //"Plase select AGS Combination");
             }
             else
             {
                 string SettingValue = "True";
                 AGSBusiness objAGSBusiness = new AGSBusiness();
                int res= objAGSBusiness.InsertOrUpdateAGSSetting(EnrolmentFlagValueResult.ToString());
                int res1 = objAGSBusiness.InsertOrUpdateSetting("IsEnrolmentComplete", SettingValue);
             }

             this.Close();
    
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            EnrolmentFlagValueResult = 0;
            this.Close();
        }
    }
}
