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
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.ExceptionManagement;
using BMC.CoreLib.Win32;
using BMC.Common;
namespace BMC.EnterpriseClient.Views
{
    public partial class frmDepreciation : BMC.EnterpriseClient.Helpers.BMCExtendedForm
    {

        private const string ScreenName = "Depreciation Policy => ";
        private const string Branding = "Bally MultiConnect - Enterprise";

        private string Dep_PD_ID = "";
        BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;
        string _msgBoxTitle = string.Empty;

        #region Constructor

        public frmDepreciation()
        {
            InitializeComponent();
            SetPropertyTag();
        }

        private void SetPropertyTag()
        {
            try
            {
                this.label4.Tag = "Key_PercentDropColon";
                this.Drop.Tag = "Key_PercentDrop";
                this.btnApplyPolicy.Tag = "Key_Apply";
                this.btnApplyDetails.Tag = "Key_Apply";
                this.btnClose.Tag = "Key_Close";
                this.btnDeletePolicy.Tag = "Key_DeleteCaption";
                this.btnDeleteDetails.Tag = "Key_DeleteCaption";                
                this.groupBox2.Tag = "Key_DepreciationDetails";
                this.groupBox1.Tag = "Key_DepreciationPolicy";
                this.Duration.Tag = "Key_DurationMonths";
                this.label3.Tag = "Key_DurationColon";
                this.btnNewPolicy.Tag = "Key_NewCaption";
                this.btnNewDetails.Tag = "Key_NewCaption";
                this.Period.Tag = "Key_Period";
                this.label2.Tag = "Key_PeriodColon";
                this.label1.Tag = "Key_ResidualValueColon";
                this.btnUpdateDefaults.Tag = "Key_UpdateDefaults";
                this.Tag = "Key_Depreciation";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion

        #region Load Methods
        /// <summary>
        /// Load depreciation policy
        /// </summary>
        private void LoadDepreciationPolicy()
        {
            try
            {

                cmbDepreciationPolicies.DisplayMember = "Depreciation_Policy_Description";
                cmbDepreciationPolicies.ValueMember = "Depreciation_Policy_ID";
                cmbDepreciationPolicies.DataSource = DepreciationBusiness.CreateInstance().LoadDepreciationPolicy(null);
                cmbDepreciationPolicies.SelectedIndex = -1;


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Load depreciation policy details
        /// </summary>
        /// <param name="Depreciation_Policy_ID"></param>
        private void LoadDepreciationPoliciesDetails(int Depreciation_Policy_ID)
        {
            try
            {

                lv_DepreciationDetails.Items.Clear();
                LogManager.WriteLog(ScreenName + "Load depreciation policies details", LogManager.enumLogLevel.Debug);
                List<DepreciationEntity> lstDep = DepreciationBusiness.CreateInstance().LoadDepreciationPoliciesDetails(Depreciation_Policy_ID);
                if (lstDep.Count > 0)
                {

                    foreach (DepreciationEntity obj in lstDep)
                    {
                        ListViewItem items = new ListViewItem(new string[] {
                            obj.Depreciation_Policy_Details_Period.ToString(),
                            obj.Depreciation_Policy_Details_Duration.ToString(), 
                            obj.Depreciation_Policy_Details_Percentage.ToString() 
                        });
                        items.Name = obj.Depreciation_Policy_Details_ID.ToString();
                        lv_DepreciationDetails.Items.Add(items);
                    }
                    txtResidualValue.Text = (lstDep[0].Depreciation_Policy_Residual_Value ?? 0).ToString();

                }
                else
                {
                    BindingList<DepreciationEntity> dep_entity = DepreciationBusiness.CreateInstance().LoadDepreciationPolicy(Depreciation_Policy_ID);
                    if (dep_entity != null && dep_entity.Count > 0)
                    {
                        txtResidualValue.Text = (dep_entity[0].Depreciation_Policy_Residual_Value ?? 0).ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion

        #region Event Methods

        private void frmDepreciation_Load(object sender, EventArgs e)
        {
            this.cmbDepreciationPolicies.SelectedIndexChanged -= new System.EventHandler(this.cmbDepreciationPolicies_SelectedIndexChanged);
            LogManager.WriteLog(ScreenName + "frmDepreciation_Load:Load depreciation policy", LogManager.enumLogLevel.Info);
            LoadDepreciationPolicy();
            this.cmbDepreciationPolicies.SelectedIndexChanged += new System.EventHandler(this.cmbDepreciationPolicies_SelectedIndexChanged);
            objDatawatcher = new Helpers.Datawatcher(this);
            this.ResolveResources();
        }

        private void btn_DeletePolicy_Click(object sender, EventArgs e)
        {
            if (cmbDepreciationPolicies.SelectedIndex == -1)
                return;
            try
            {
                if (DialogResult.Yes == this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_DEPRECIATION_REMOVEPOLICY"), this.Text))
                {

                    int Depreciation_Policy_ID = Convert.ToInt32(cmbDepreciationPolicies.SelectedValue);
                    if (DepreciationBusiness.CreateInstance().DeleteDepreciationPolicy(Depreciation_Policy_ID))
                    {



                        BindingList<DepreciationEntity> lstDep = cmbDepreciationPolicies.DataSource as BindingList<DepreciationEntity>;
                        lstDep.Remove(cmbDepreciationPolicies.SelectedItem as DepreciationEntity);
                        //cmbDepreciationPolicies.DataSource = new List<DepreciationEntity>();
                        //cmbDepreciationPolicies.DisplayMember = "Depreciation_Policy_Description";
                        //cmbDepreciationPolicies.ValueMember = "Depreciation_Policy_ID";
                        //cmbDepreciationPolicies.DataSource = lstDep;
                        cmbDepreciationPolicies.SelectedIndex = -1;
                        lv_DepreciationDetails.Items.Clear();
                        txtDropPercent.Text = "";
                        txtDuration.Text = "";
                        txtPeriod.Text = "";
                        txtResidualValue.Text = "";
                        Dep_PD_ID = "";
                        LogManager.WriteLog(ScreenName + "Depreciation policy removed successfully", LogManager.enumLogLevel.Info);
                    }
                    else
                    {
                        LogManager.WriteLog(ScreenName + "Unable to remove 3 policy", LogManager.enumLogLevel.Info);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btn_NewPolicy_Click(object sender, EventArgs e)
        {
            try
            {

                txtResidualValue.Text = string.Empty;
                int index = cmbDepreciationPolicies.Items.Count;
                string NewDepreciationPolicyName = string.Empty;
                frmInputBox frmDepPolicy = new frmInputBox(this.GetResourceTextByKey(1, "MSG_DEPRECIATION_ENTER_NAME"), this.GetResourceTextByKey("Key_Depreciation_Policy"));
                frmDepPolicy.ShowDialog();
                if (frmDepPolicy.TextValue != null && frmDepPolicy.TextValue != "")
                {
                    string ErrorMsg = "";
                    NewDepreciationPolicyName = frmDepPolicy.TextValue.Trim();
                    if (ValidateNewPolicy(NewDepreciationPolicyName, ref ErrorMsg))
                    {
                        int Dep_PolicyID = 0;
                        if (DepreciationBusiness.CreateInstance().InsertDepreciationPolicy(NewDepreciationPolicyName, 0, ref Dep_PolicyID))
                        {
                            BindingList<DepreciationEntity> lst_dep = (BindingList<DepreciationEntity>)cmbDepreciationPolicies.DataSource;
                            DepreciationEntity dep_new = new DepreciationEntity { Depreciation_Policy_ID = Dep_PolicyID, Depreciation_Policy_Description = NewDepreciationPolicyName, Depreciation_Policy_Residual_Value = 0 };
                            lst_dep.Add(dep_new);
                            //cmbDepreciationPolicies.DataSource = null;
                            //cmbDepreciationPolicies.DisplayMember = "Depreciation_Policy_Description";
                            //cmbDepreciationPolicies.ValueMember = "Depreciation_Policy_ID";
                            //cmbDepreciationPolicies.DataSource = lst_dep;
                            cmbDepreciationPolicies.SelectedItem = dep_new;
                            LogManager.WriteLog(ScreenName + "Depreciation policy added successfully", LogManager.enumLogLevel.Info);
                        }
                        else
                        {
                            LogManager.WriteLog(ScreenName + "Unable to insert depreciation policy", LogManager.enumLogLevel.Info);
                        }
                    }
                    else
                    {
                        this.ShowInfoMessageBox(ErrorMsg, this.Text);
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btn_ApplyPolicy_Click(object sender, EventArgs e)
        {
            try
            {
                int Depreciation_Policy_ID = Convert.ToInt32(cmbDepreciationPolicies.SelectedValue);
                string DepreciationPolicyName = cmbDepreciationPolicies.Text;
                float Depreciation_Policy_Residual_Value = Convert.ToSingle(Math.Round(Convert.ToDouble("0" + txtResidualValue.Text), 2));
                if (DepreciationBusiness.CreateInstance().UpdateDepreciationPolicy(Depreciation_Policy_ID, DepreciationPolicyName, Depreciation_Policy_Residual_Value))
                {
                    LogManager.WriteLog(ScreenName + "Depreciation policy updated successfully", LogManager.enumLogLevel.Info);
                    txtResidualValue.Text = Depreciation_Policy_Residual_Value.ToString();
                }
                else
                {

                    this.ShowErrorMessageBox(this.GetResourceTextByKey(1, "MSG_DEPRECIATION_ERROR"), this.Text);
                    LogManager.WriteLog(ScreenName + "Unable to updated depreciation policy", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbDepreciationPolicies_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbDepreciationPolicies.SelectedIndex == -1)
                    return;
                txtDropPercent.Text = "";
                txtDuration.Text = "";
                txtPeriod.Text = "";
                Dep_PD_ID = "";
                DepreciationEntity dep_entity = cmbDepreciationPolicies.SelectedItem as DepreciationEntity;
                //txtResidualValue.Text = (dep_entity.Depreciation_Policy_Residual_Value ?? 0).ToString();
                LoadDepreciationPoliciesDetails(dep_entity.Depreciation_Policy_ID);

            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }
        }

        private void btn_NewDetails_Click(object sender, EventArgs e)
        {
            try
            {

                txtPeriod.Text = txtDuration.Text = txtDropPercent.Text = string.Empty;
                if (cmbDepreciationPolicies.SelectedIndex == -1) return;
                int sindex = Convert.ToInt32(cmbDepreciationPolicies.SelectedValue);
                int Depreciation_PD_ID = 0;
                if (DepreciationBusiness.CreateInstance().InsertDepreciationDetails((Convert.ToInt32(cmbDepreciationPolicies.SelectedValue)), (lv_DepreciationDetails.Items.Count + 1), 0, 0, ref Depreciation_PD_ID))
                {
                    //initially "0" will be passsed for "period" and "%"
                    ListViewItem items = new ListViewItem(new string[] { (lv_DepreciationDetails.Items.Count + 1).ToString(), "0", "0" });

                    items.Name = Depreciation_PD_ID.ToString();
                    lv_DepreciationDetails.Items.Add(items);
                    txtDropPercent.Text = "0";
                    txtDuration.Text = "0";
                    Dep_PD_ID = Depreciation_PD_ID.ToString();
                    txtPeriod.Text = (lv_DepreciationDetails.Items.Count).ToString();
                    txtDuration.Focus();
                    LogManager.WriteLog(ScreenName + "Depreciation details added successfully", LogManager.enumLogLevel.Info);
                }
                else
                {
                    LogManager.WriteLog(ScreenName + "Unable to insert depreciation details", LogManager.enumLogLevel.Info);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void btn_ApplyDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (lv_DepreciationDetails.Items.Count < 0)
                    return;
                int Depreciation_PD_ID = Convert.ToInt32("0" + Dep_PD_ID);
                if (Depreciation_PD_ID > 0 && (!IsSamePolicy()))
                {
                    GetDepreciationPolicyPercentResult DP_percent = DepreciationBusiness.CreateInstance().GetDepreciationPolicyPercent((int)cmbDepreciationPolicies.SelectedValue, Depreciation_PD_ID);
                    int DropPercent = txtDropPercent.Text.IndexOf('.') != -1 ? Convert.ToInt32(Math.Round(Convert.ToDouble(txtDropPercent.Text))) : Convert.ToInt32(txtDropPercent.Text);
                    int Duration = txtDuration.Text.IndexOf('.') != -1 ? Convert.ToInt32(Math.Round(Convert.ToDouble(txtDuration.Text))) : Convert.ToInt32(txtDuration.Text);
                    if (DP_percent.TotalDrop.HasValue && ((DP_percent.TotalDrop.Value + DropPercent) > 100))
                    {

                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_DEPRECIATION_INVALIDDATE"), this.Text);
                        txtDropPercent.Focus();
                        return;
                    }

                    if (DepreciationBusiness.CreateInstance().UpdateDepreciationDetails(Depreciation_PD_ID, Duration, DropPercent))
                    {
                        LogManager.WriteLog(ScreenName + "Depreciation details updated successfully", LogManager.enumLogLevel.Info);

                        lv_DepreciationDetails.Items[Dep_PD_ID].SubItems[1].Text = Duration.ToString();
                        lv_DepreciationDetails.Items[Dep_PD_ID].SubItems[2].Text = DropPercent.ToString();
                        txtDropPercent.Text = DropPercent.ToString();
                        txtDuration.Text = Duration.ToString();
                    }
                    else
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_DEPRECIATION_ERROR"), this.Text);                       
                        LogManager.WriteLog(ScreenName + "Unable to update depreciation details", LogManager.enumLogLevel.Info);
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btn_DeleteDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (lv_DepreciationDetails.Items.Count < 0 && lv_DepreciationDetails.SelectedItems[0] == null)
                    return;


                if (lv_DepreciationDetails.Items.Count != 1 && lv_DepreciationDetails.Items.Count != (lv_DepreciationDetails.SelectedItems[0].Index + 1))
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_DEPRECIATION_REMOVELAST"), this.Text);                                           
                    return;
                }
                if (DepreciationBusiness.CreateInstance().DeleteDepreciationDetails(Convert.ToInt32(lv_DepreciationDetails.SelectedItems[0].Name)))
                {
                    LogManager.WriteLog(ScreenName + "Depreciation policy details removed successfully", LogManager.enumLogLevel.Info);
                    lv_DepreciationDetails.Items.Remove(lv_DepreciationDetails.SelectedItems[0]);
                    txtDropPercent.Text = "";
                    txtDuration.Text = "";
                    txtPeriod.Text = "";
                    Dep_PD_ID = "";
                }
                else
                {
                    LogManager.WriteLog(ScreenName + "Unable to remove depreciation policy details", LogManager.enumLogLevel.Info);
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_DEPRECIATION_ERRORPOLICY"), this.Text);                       
                    
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void lv_DepreciationDetails_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                txtPeriod.Text = e.Item.SubItems[0].Text;
                txtDuration.Text = e.Item.SubItems[1].Text;
                txtDropPercent.Text = e.Item.SubItems[2].Text;
                Dep_PD_ID = e.Item.Name;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDuration_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void btnUpdateDefaults_Click(object sender, EventArgs e)
        {
            try
            {
                if (DepreciationBusiness.CreateInstance().UpdateDepreciationUseDefault(true))
                {
                    LogManager.WriteLog(ScreenName + @"DepreciationUseDefault flag update to M\C and M\C Class successfully", LogManager.enumLogLevel.Info);
                }
                else
                {
                    LogManager.WriteLog(ScreenName + "Unable to update depreciationusedefault flag", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion

        #region Validation Methods

        private bool ValidateNewPolicy(string DepreciationPolicyName, ref string ErrorMsg)
        {
            bool retval = true;
            char[] strKeyBoard = { '\'', '"', '%', '_', '`' };
            try
            {
                if (DepreciationPolicyName.IndexOfAny(strKeyBoard) != -1)
                {
                    ErrorMsg = this.GetResourceTextByKey(1,"MSG_BARPOS_INVALID_NAME");
                    retval = false;
                }
                else if (DepreciationPolicyName.Length > 50)
                {
                    ErrorMsg = this.GetResourceTextByKey(1,"MSG_NAME_EXCEEDLENGTH");
                    retval = false;
                }
                else if (DepreciationBusiness.CreateInstance().IsDepreciationPolicyExists(DepreciationPolicyName))
                {
                    ErrorMsg = this.GetResourceTextByKey(1,"MSG_DEPRECIATION_POLICYNAME_EXISTS");
                    retval = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                ErrorMsg = this.GetResourceTextByKey(1,"MSG_UNABLE_VALIDATE_DEPRECIATION");
                retval = false;
            }
            return retval;
        }

        bool IsSamePolicy()
        {
            bool retval = false;
            retval = (txtDuration.Text == lv_DepreciationDetails.Items[Dep_PD_ID].SubItems[1].Text) && (txtDropPercent.Text == lv_DepreciationDetails.Items[Dep_PD_ID].SubItems[2].Text);

            return retval;
        }

        #endregion

    }
}
