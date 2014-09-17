using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.ComponentVerification.BusinessEntities;
using BMC.ComponentVerification.BusinessLayer;

namespace BMC.ComponentVerification.UI
{
    public partial class VerificationOnDemand : Form
    {
        VerificationDemandAccessor vda;
        public VerificationOnDemand()
        {
            InitializeComponent();
            vda = new VerificationDemandAccessor();            
        }

        private void VerificationOnDemand_Load(object sender, EventArgs e)
        {
            LoadSiteDetails();

        }

        private void LoadSiteDetails()
        {
            //cbSite.Items.Add("--Select--");            
            List<SiteDetailsData> ds = vda.GetAllSites();            
            cbSite.DataSource = ds;
            cbSite.DisplayMember = "Site_Name";
            cbSite.ValueMember = "Site_ID";            
            cbSite.SelectedIndex = -1;
        }

        private void LoadMachineDetails(int siteId)
        {
            List<MachineDetailsData> ds = vda.GetAllMachinesForSite(siteId);
            cbMachines.DisplayMember = "Machine_Manufacturers_Serial_No";
            cbMachines.ValueMember = "Machine_Manufacturers_Serial_No";
            cbMachines.DataSource = ds;
            cbMachines.SelectedIndex = -1;
        }

        private void btnCanel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbSite_SelectedIndexChanged(object sender, EventArgs e)
        {
    
            if (cbSite.SelectedIndex > -1 && cbSite.Focused == true)
            {
                SiteDetailsData data = (SiteDetailsData)cbSite.SelectedItem;
                cbMachines.Enabled = true;
                LoadMachineDetails(data.Site_ID);
            }
        }

        private void cbMachines_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMachines.SelectedIndex > -1 && cbMachines.Focused == true)
            {
                MachineDetailsData data = (MachineDetailsData)cbMachines.SelectedItem;
                cbComponents.Enabled = true;
                LoadMachineCompDetails(data.Machine_Manufacturers_Serial_No);
            }
        }

        private void LoadMachineCompDetails(string p)
        {
            List<MachineCompDetails> ds = vda.GetAllComponentsForMachine(p);
            cbComponents.DisplayMember = "Component_Type";
            cbComponents.ValueMember = "Component_ID";
            cbComponents.DataSource = ds;
            cbComponents.SelectedIndex = -1;
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            var iCompId = 0;
            var strSerialNo = string.Empty;
            if (!(cbSite.SelectedIndex > -1 && cbMachines.SelectedIndex > -1))
            {
                MessageBox.Show("Please Select Site and Machine to verify component");                
                return;
            }
            strSerialNo = ((MachineDetailsData)cbMachines.SelectedItem).Machine_Manufacturers_Serial_No;
            if (cbComponents.SelectedIndex == -1)
            {
                MessageBox.Show("All components for the selected machine would be verified");
                ComboBox.ObjectCollection col = cbComponents.Items;
                foreach (MachineCompDetails comp in col)
                    vda.InsertComponentVerificationData(comp.Component_ID, strSerialNo);
            }
            else
            {
                iCompId = ((MachineCompDetails)cbComponents.SelectedItem).Component_ID;
                if (!CheckComponentStatus(iCompId, strSerialNo))
                {
                    MessageBox.Show("Component Verification is in progress for the selected component");
                    Reset();
                    return;
                }
                else
                    vda.InsertComponentVerificationData(iCompId, strSerialNo);
            }
            MessageBox.Show("Component verification has requested for the selected machine");
            Reset();           
        }

        private void Reset()
        {
            cbSite.SelectedIndex = -1;
            cbMachines.SelectedIndex = -1;
            cbComponents.SelectedIndex = -1;
        }

        private bool CheckComponentStatus(int iCompId,string strSerialNo)
        {
            var flag = false;
            var result = vda.CheckComponentStatus(iCompId,strSerialNo);
            if (result > 0)
                flag = true;
            return flag;
        }
    }
}
