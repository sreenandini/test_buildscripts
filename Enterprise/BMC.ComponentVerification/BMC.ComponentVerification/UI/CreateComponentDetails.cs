using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.ComponentVerification.BusinessLayer;
using BMC.ComponentVerification.BusinessEntities;
using BMC.Common.ExceptionManagement;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.LogManagement;

namespace BMC.ComponentVerification.UI
{
    public partial class CreateComponentDetails : Form
    {
        #region Variables

        ComponentDetailAccessor cda;
        ComponentTypeAccessor cta;
        int nCdetialId = 0;
        string strCVDCaption = "Component Details";       
        string strOldAlgorithmName = string.Empty;
        string strOldHashValue = string.Empty;
        string strOldSeedValue = string.Empty;
        string strOldSerialNo = string.Empty;

        #endregion Variables

        #region Declaration

        public delegate void RefreshComponentsDelegate();

        public event RefreshComponentsDelegate RefreshComponentsEvent;

        #endregion Declaration

        #region Constructor

        public CreateComponentDetails()
        {
         
            cda = new ComponentDetailAccessor();
            cta  = new ComponentTypeAccessor();
            InitializeComponent();
        }

        public CreateComponentDetails(int cdetailID)
        {
         
            this.nCdetialId = cdetailID;
            cda = new ComponentDetailAccessor();
            cta = new ComponentTypeAccessor();            
            InitializeComponent();
            txtName.Enabled = false;
            cbCType.Enabled = false;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Load the Algorithm Types.
        /// </summary>
        /// <param name="iCompID"></param>
        /// <param name="iNew"></param>
        private void LoadAlgorithmTypes(int iCompID, int iNew)
        {
            try
            {
                List<AlgorithmTypesData> ds = cda.GetAlgorithmTypes(iCompID, iNew);
                cbAlgorithmType.DisplayMember = "CAT_Name";
                cbAlgorithmType.ValueMember = "CAT_Code";
                cbAlgorithmType.DataSource = ds;
            }
            catch (Exception exLoadAlgorithmTypes)
            {
                ExceptionManager.Publish(exLoadAlgorithmTypes);
            }
        }

        /// <summary>
        /// Load Component Types.
        /// </summary>
        private void LoadComponentTypes()
        {
            List<ComponentTypesData> lstCompTypes = new List<ComponentTypesData>();
            try
            {
                //Clear the combo.
                cbCType.DataSource = null;

                //Insert a dummy record.
                ComponentTypesData objCompTypesData = new ComponentTypesData();
                objCompTypesData.CCT_Code  = 0;
                objCompTypesData.CCT_Name  = "---Select---";
                lstCompTypes.Add(objCompTypesData);

                foreach (ComponentTypesData objComponentTypesData in cta.GetComponentTypes())
                {
                    lstCompTypes.Add(objComponentTypesData);
                }

                cbCType.DisplayMember = "CCT_Name";
                cbCType.ValueMember = "CCT_Code";
                cbCType.DataSource = lstCompTypes;
                cbCType.SelectedIndex = 0;

            //List<ComponentTypesData> ds = cta.GetComponentTypes();
            //cbCType.DisplayMember = "CCT_Name";
            //cbCType.ValueMember = "CCT_Code";
            //cbCType.DataSource = ds;
            }
            catch (Exception exLoadComponentTypes)
            {
                ExceptionManager.Publish(exLoadComponentTypes);
            }
        }

        /// <summary>
        /// Check for Hexa decimal.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private bool IsHexaDecimal(string p)
        {
            try
            {
                char[] enteredValues = p.ToCharArray();
                foreach (char ev in enteredValues)
                {
                    if (!char.IsControl(ev)
                       && !char.IsDigit(ev)
                       && !(ev == 'A' || ev == 'a' || ev == 'B' || ev == 'b' || ev == 'C' || ev == 'c' ||
                       ev == 'D' || ev == 'd' || ev == 'E' || ev == 'e' || ev == 'F' || ev == 'f'))
                    {
                        MessageBox.Show("Please enter Hexadecimal values. (Valid Range: 0-9, A-F, a-f)", strCVDCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception exIsHexaDecimal)
            {
                ExceptionManager.Publish(exIsHexaDecimal);
                return false;
            }
        }

        #endregion Methods

        #region Events

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbAlgorithmType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                AlgorithmTypesData data = (AlgorithmTypesData)cbAlgorithmType.SelectedItem;
                txtHash.Text = "";
                txtSeed.Text = "";

                if (data != null)
                    switch (data.CAT_Name)
                    {
                        case "Kobetron I":
                            txtSeed.MaxLength = 4;
                            txtHash.MaxLength = 4;
                            break;
                        case "Kobetron II":
                            txtSeed.MaxLength = 4;
                            txtHash.MaxLength = 8;
                            break;
                        case "CRC 16":
                            txtSeed.MaxLength = 4;
                            txtHash.MaxLength = 4;
                            break;
                        case "CRC 32":
                            txtSeed.MaxLength = 8;
                            txtHash.MaxLength = 8;
                            break;
                        case "MD5":
                            txtSeed.MaxLength = 32;
                            txtHash.MaxLength = 32;
                            break;
                        case "SHA1":
                            txtSeed.MaxLength = 40;
                            txtHash.MaxLength = 40;
                            break;
                        case "SHA512":
                            txtSeed.MaxLength = 128;
                            txtHash.MaxLength = 128;
                            break;
                    }
            }
            catch (Exception excbAlgorithmType_SelectedIndexChanged)
            {
                ExceptionManager.Publish(excbAlgorithmType_SelectedIndexChanged);
            }
        }

        private void cbCType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (nCdetialId > 0)
                {
                    LoadAlgorithmTypes(nCdetialId, 0);
                }
                else
                {
                    ComponentTypesData objComponentTypesData = (ComponentTypesData)cbCType.SelectedItem;
                    LoadAlgorithmTypes(objComponentTypesData.CCT_Code, 1);
                }
                //if (((ComponentTypesData)cbCType.SelectedItem).CCT_Name == "MC300")
                //    txtSeed.Enabled = false;
                //else
                //    txtSeed.Enabled = true;
            }
            catch (Exception excbCType_SelectedIndexChanged)
            {
                ExceptionManager.Publish(excbCType_SelectedIndexChanged);
            }
        }

        private void txtSeed_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            bool value = false;

            try
            {
                AlgorithmTypesData data = (AlgorithmTypesData)cbAlgorithmType.SelectedItem;
                if (!(data.CAT_Name.Contains("Kobetron I") || data.CAT_Name.Contains("Kobetron II")))
                    value = IsHexaDecimal(Convert.ToString(e.KeyChar));
                e.Handled = value;
            }
            catch (Exception extxtSeed_KeyPress)
            {
                ExceptionManager.Publish(extxtSeed_KeyPress);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var bCreate = false;
            int? result = 0;
            string strText = string.Empty;
    
            try
            {
                if (cbCType.SelectedIndex < 1)
                {
                    MessageBox.Show("Please Select the Component Type.", strCVDCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);                    
                    return;
                }
                if (txtName.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter the Component Name.", strCVDCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtName.Focus();
                    return;
                }

                if (txtSerial.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter the Model Name.", strCVDCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSerial.Focus();
                    return;
                }               
                if (txtSeed.Enabled)
                {
                    if (txtSeed.Text.Trim() == "")
                    {
                        MessageBox.Show("Please enter the Seed Value.", strCVDCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSeed.Focus();
                        return;
                    }
                    else if(IsHexaDecimal(txtSeed.Text.Trim()))
                    {
                        txtSeed.Focus();
                        return;
                    }
                    else if(txtSeed.Text.Length != txtSeed.MaxLength)
                    {
                        MessageBox.Show("Please enter a valid " + txtSeed.MaxLength.ToString() + " character length Seed Value.", 
                            strCVDCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);                        
                        txtSeed.Focus();
                        return;
                    }
                }

                if (txtHash.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter the Hash Value.", strCVDCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtHash.Focus();
                    return;
                }             

                if (nCdetialId > 0)
                {
                    if (strOldAlgorithmName.Equals(((AlgorithmTypesData)cbAlgorithmType.SelectedItem).CAT_Name)
                        && strOldHashValue.Equals(txtHash.Text.Trim()) && strOldSeedValue.Equals(txtSeed.Text.Trim()))
                    {
                        if (strOldSerialNo.Equals(txtSerial.Text.Trim()))
                        {
                            this.Close();
                            return;
                        }
                        else
                        {
                            result = cda.ModifyComponentDetails(txtName.Text.Trim(), txtSerial.Text.Trim(),
                               ((ComponentTypesData)cbCType.SelectedItem).CCT_Code,
                               ((AlgorithmTypesData)cbAlgorithmType.SelectedItem).CAT_Code, txtSeed.Text.Trim(), txtHash.Text.Trim());

                            AuditViewerBusiness.CreateInstance(Common.Utilities.DatabaseHelper.GetConnectionString());
                            Audit_History AH = new Audit_History();

                            //Populate required Values
                            AH.EnterpriseModuleName = ModuleNameEnterprise.ComponentVerification;
                            AH.Audit_Screen_Name = "ComponentVerification|Edit Component Details";
                            AH.Audit_Desc = "Component " + ((ComponentTypesData)cbCType.SelectedItem).CCT_Name + " modified";
                            AH.AuditOperationType = OperationType.MODIFY;

                            AH.Audit_User_ID = Program.LoginUserID;
                            AH.Audit_User_Name = Program.LoginUserName;
                            AuditViewerBusiness.InsertAuditData(AH);

                            MessageBox.Show("Component Details updated successfully.", strCVDCaption, MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("Component Details (Algorithm Type/Hash Value/Seed Value) change will trigger \n     'On Demand' " +
                            "verification for the component across ALL SITES and VLT'S.\n\n                  Please click 'Yes' to Confirm or 'No' to Cancel.",
                            strCVDCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            result = cda.ModifyComponentDetails(txtName.Text.Trim(), txtSerial.Text.Trim(),
                                ((ComponentTypesData)cbCType.SelectedItem).CCT_Code,
                                ((AlgorithmTypesData)cbAlgorithmType.SelectedItem).CAT_Code, txtSeed.Text.Trim(), txtHash.Text.Trim());

                            AuditViewerBusiness.CreateInstance(Common.Utilities.DatabaseHelper.GetConnectionString());
                            Audit_History AH = new Audit_History();

                            //Populate required Values
                            AH.EnterpriseModuleName = ModuleNameEnterprise.ComponentVerification;
                            AH.Audit_Screen_Name = "ComponentVerification|Edit Component Details";
                            AH.Audit_Desc = "Component " + ((ComponentTypesData)cbCType.SelectedItem).CCT_Name + " modified";
                            AH.AuditOperationType = OperationType.MODIFY;

                            AH.Audit_User_ID = Program.LoginUserID;
                            AH.Audit_User_Name = Program.LoginUserName;
                            AuditViewerBusiness.InsertAuditData(AH);
                            MessageBox.Show("Component Details updated successfully.",
                                strCVDCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Component Details updation was cancelled by the user.", strCVDCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    result = cda.SaveComponetDetails(txtName.Text.Trim(), txtSerial.Text.Trim(), 
                        ((ComponentTypesData)cbCType.SelectedItem).CCT_Code, 
                        ((AlgorithmTypesData)cbAlgorithmType.SelectedItem).CAT_Code, txtSeed.Text.Trim(), txtHash.Text.Trim());

                    if (result > 0)
                    {                        
                        AuditViewerBusiness.CreateInstance(Common.Utilities.DatabaseHelper.GetConnectionString());
                        Audit_History AH = new Audit_History();

                        //Populate required Values
                        AH.EnterpriseModuleName = ModuleNameEnterprise.ComponentVerification;
                        AH.Audit_Screen_Name = "ComponentVerification|Create Component Details";
                        AH.Audit_Desc = "Component " + ((ComponentTypesData)cbCType.SelectedItem).CCT_Name + " created"; 
                        AH.AuditOperationType = OperationType.ADD;

                        AH.Audit_User_ID = Program.LoginUserID;
                        AH.Audit_User_Name = Program.LoginUserName;
                        AuditViewerBusiness.InsertAuditData(AH);
                        bCreate = true;
                        MessageBox.Show("Component Details created successfully.", strCVDCaption, MessageBoxButtons.OK, 
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Component Name already exists.", strCVDCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtName.Focus();                        
                    }
                }


                //Insert to EH.
                cda.InsertComponentDetailsEHRecord(result);

                if (bCreate)
                {        
                    //Call RefreshComponentsEvent.
                    RefreshComponentsEvent();                    
                }

                
                this.Close();                
            }
            catch (Exception exbtnSave_Click)
            {
                LogManager.WriteLog("CV - Exception occured Save Component Details" + exbtnSave_Click.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(exbtnSave_Click);                
            }
        }

        private void CreateComponentDetails_Load(object sender, EventArgs e)
        {
            try
            {
                LoadComponentTypes();

                if (nCdetialId > 0)
                {
                    var details = cda.GetComponentDetails(nCdetialId);
                    foreach (var data in details)
                    {
                        txtName.Text = data.ccd_name;
                        txtSerial.Text = data.CCD_Model_Name;

                        int i = -1;
                        foreach (var item in cbCType.Items)
                        {
                            i++;
                            if (((ComponentTypesData)item).CCT_Name == data.cct_name)
                                break;
                        }
                        cbCType.SelectedIndex = i;
                        //cbCType.SelectedIndex = i;

                        int j = -1;
                        foreach (var item in cbAlgorithmType.Items)
                        {
                            j++;
                            if (((AlgorithmTypesData)item).CAT_Name == data.cat_name)
                                break;
                        }
                        cbAlgorithmType.SelectedIndex = j;
                        //cbAlgorithmType.SelectedIndex = j;

                        txtSeed.Text = data.ccd_seed_value;
                        txtHash.Text = data.ccd_hash_value;
                        strOldAlgorithmName = data.cat_name;
                        strOldHashValue = txtHash.Text.Trim();
                        strOldSeedValue = txtSeed.Text.Trim();
                        strOldSerialNo = txtSerial.Text.Trim();
                    }
                }
            }
            catch (Exception exCreateComponentDetails_Load)
            {
                LogManager.WriteLog("CV - Exception occured Save Component Details" + exCreateComponentDetails_Load.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(exCreateComponentDetails_Load);               
            }
        }

        #endregion Events
    }
}
