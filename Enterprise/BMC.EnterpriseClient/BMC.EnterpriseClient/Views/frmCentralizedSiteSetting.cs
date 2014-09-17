using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.Security;
using BMC.Common;
using BMC.CoreLib.Win32;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmCentralizedSiteSetting : Form
    {
        #region Local Decleration

        #region Objects
        CentralizedSiteSettingBiz _CentralizedSiteSettingBiz = null;
        DataTable dtSettings = new DataTable();
        List<PropertyClass> ChangedProperty = new List<PropertyClass>();
        private TimeZoneNames TZN = TimeZoneNames.UTC;
        private LiquidationType liqtype = LiquidationType.Collection;
        private PreCommitmentRatingBasis pcRateBasis = PreCommitmentRatingBasis.Time;
        #endregion Objects
        
        #endregion Local Decleration

        #region Events
        public frmCentralizedSiteSetting()
        {
            InitializeComponent();
            this.SetTagProperty();
        }

        private void frmCentralizedSiteSetting_Load(object sender, EventArgs e)
        {
            this.ResolveResources();

            _CentralizedSiteSettingBiz = CentralizedSiteSettingBiz.CreateInstance();

            BuildTreeViewList();

            RefreshProfileList(string.Empty);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApplyProfile_Click(object sender, EventArgs e)
        {
            ApplyProfile();           
        }

        private void btnSaveProfile_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (PropertyClass propClass in ChangedProperty)
                {
                    _CentralizedSiteSettingBiz.UpdateSetting(cmbProfilesList.SelectedItem.ToString(), propClass.Name, propClass.Value);

                    // call auditing function
                    AuditProfileUpdate(propClass, cmbProfilesList.SelectedItem.ToString());
                }
                DataTable dtSites = _CentralizedSiteSettingBiz.GetSitesForProfile(cmbProfilesList.SelectedItem.ToString());
                foreach (DataRow dr in dtSites.Rows)
                {
                    _CentralizedSiteSettingBiz.InsertExportHistory(dr["Site_ID"].ToString(), dr["Site_Code"].ToString());
                }
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_VALUES_UPDATED"), this.Text);
                tvwSiteList.Select();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnSaveProfileAs_Click(object sender, EventArgs e)
        {
            SaveProfile frmSaveProfile = new SaveProfile();
            frmSaveProfile.OkClickEvent += new SaveProfile.OkClicked(frmSaveProfile_OkClickEvent);
            frmSaveProfile.ShowDialog();
            tvwSiteList.Select();
            frmSaveProfile.OkClickEvent -= frmSaveProfile_OkClickEvent;
        }

        void frmSaveProfile_OkClickEvent(object sender, EventArgs e, string strTextValue)
        {
                SaveAsNewProfile(strTextValue);
            }

        private void cmbProfilesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbProfilesList.SelectedIndex == 0)
                {
                    DisableAllControls();
                }
                else
                {
                    EnableAllControls();
                    LoadPropertyGrid(cmbProfilesList.SelectedItem.ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void tvwSiteList_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node.Name == "All Sites")
                {
                    foreach (TreeNode tNode in e.Node.Nodes)
                    {
                        tNode.Checked = e.Node.Checked;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void tvwSiteList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string strProfile = string.Empty;
            try
            {
                if (tvwSiteList.SelectedNode.Name == "All Sites")
                {
                    cmbProfilesList.SelectedItem = "Select Profile";
                }
                else
                {
                    strProfile = _CentralizedSiteSettingBiz.GetProfileNameForSite(tvwSiteList.SelectedNode.Name);
                    cmbProfilesList.SelectedItem = string.IsNullOrEmpty(strProfile) ? "Select Profile" : strProfile.Trim();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion Events

        #region User Methods

        public void SetTagProperty()
        {
            this.btnApplyProfile.Tag = "Key_Apply";
            this.btnClose.Tag = "Key_CloseCaption";
            this.btnSaveProfileAs.Tag = "Key_SaveAsNewProfile";
            this.lblProfileName.Tag = "Key_ProfileNameColon";
            this.btnSaveProfile.Tag = "Key_SaveCaption";
            this.uxhcSiteTreeView.Tag = "Key_Sites";
            this.Tag = "Key_SiteSetting";
            this.lbl_Status.Tag = "Key_CSS_Status";
        }

        public void BuildTreeViewList()
        {
            try
            {
                //Clear Existing Data.
                tvwSiteList.Nodes.Clear();
                DataTable dtSiteList = _CentralizedSiteSettingBiz.GetSiteList();

                TreeNode tNode = tvwSiteList.Nodes.Add("All Sites", this.GetResourceTextByKey("Key_AllSites"));
                tNode.ExpandAll();
                
                foreach (DataRow dr in dtSiteList.Rows)
                {
                    TreeNode myNode;
                    myNode = tNode.Nodes.Add(dr["Site_Code"].ToString(), dr["Site_Name"].ToString());
                    
                    if (dr["SettingsProfile_Description"].ToString().ToUpper() == "UNASSIGNED")
                    {
                        myNode.ForeColor = Color.Red;
                    }
                    else
                    {
                        myNode.ForeColor = Color.Black;
                    }
                    myNode.ExpandAll();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void RefreshProfileList(string strTextValue)
        {
            try
            {
                DataTable dtProfileList = _CentralizedSiteSettingBiz.GetProfileList();
                //Clear Existing Data.
                cmbProfilesList.Items.Clear();
                
                //Add a default value.
                cmbProfilesList.Items.Add(this.GetResourceTextByKey("Key_SelectProfile"));
                
                foreach (DataRow dr in dtProfileList.Rows)
                {
                    //UnAssigned status is not a profile, hence filtering it
                    if (dr["SettingsProfile_Description"].ToString().ToUpper() != "UNASSIGNED")
                    {
                        cmbProfilesList.Items.Add(dr["SettingsProfile_Description"].ToString());
                    }
                }

                //Set the default value to the first value.
                cmbProfilesList.SelectedItem = string.IsNullOrEmpty(strTextValue) ? cmbProfilesList.Items[0] : strTextValue;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        
        public void DisableAllControls()
        {
            btnApplyProfile.Enabled = false;
            btnSaveProfile.Enabled = false;
            btnSaveProfileAs.Enabled = false;
            pgbSiteSettings.PropertyGridContorl.Enabled = false;
            pgbSiteSettings.PropertyGridContorl.Visible = false;
        }

        
        public void EnableAllControls()
        {
            btnApplyProfile.Enabled = true;
            btnSaveProfile.Enabled = true;
            btnSaveProfileAs.Enabled = true;
            pgbSiteSettings.PropertyGridContorl.Enabled = true;
            pgbSiteSettings.PropertyGridContorl.Visible = true;
        }

        public void LoadPropertyGrid(string strProfileName)
        {
            dtSettings = _CentralizedSiteSettingBiz.GetSettingsList(strProfileName, false);
            PropertyBag pbSetting = new PropertyBag();
            try
            {
                pbSetting.GetValue += new PropertySpecEventHandler(pbSetting_GetValue);
                pbSetting.SetValue += new PropertySpecEventHandler(pbSetting_SetValue);

                foreach (DataRow dr in dtSettings.Rows)
                {
                    if (BMC.Common.TypeHandler.GetRowValue<string>(dr, "Value").ToUpper() == "TRUE" || BMC.Common.TypeHandler.GetRowValue<string>(dr, "Value").ToUpper() == "FALSE")
                        if (BMC.Common.TypeHandler.GetRowValue<string>(dr, "Value").ToUpper() == "TRUE")
                            pbSetting.Properties.Add(new PropertySpec(BMC.Common.TypeHandler.GetRowValue<string>(dr, "Name"), typeof(bool), "BMC Category", BMC.Common.TypeHandler.GetRowValue<string>(dr, "Description"), true));
                        else
                            pbSetting.Properties.Add(new PropertySpec(BMC.Common.TypeHandler.GetRowValue<string>(dr, "Name"), typeof(bool), "BMC Category", BMC.Common.TypeHandler.GetRowValue<string>(dr, "Description"), false));
                    else if (BMC.Common.TypeHandler.GetRowValue<string>(dr, "Name") == "TIMEZONENAME")
                        pbSetting.Properties.Add(new PropertySpec("TIMEZONENAME", typeof(TimeZoneNames), null, null, (TimeZoneNames)Enum.Parse(typeof(TimeZoneNames), BMC.Common.TypeHandler.GetRowValue<string>(dr, "Value"))));

                    else if (BMC.Common.TypeHandler.GetRowValue<string>(dr, "Name").ToUpper() == "LIQUIDATIONTYPE")
                        pbSetting.Properties.Add(new PropertySpec(BMC.Common.TypeHandler.GetRowValue<string>(dr, "Name"), typeof(LiquidationType), null, "Speifies the type of Liquidation (Collection or Read)", (LiquidationType)Enum.Parse(typeof(LiquidationType), BMC.Common.TypeHandler.GetRowValue<string>(dr, "Value"))));

                    else if (BMC.Common.TypeHandler.GetRowValue<string>(dr, "Name").ToUpper() == "PRECOMMITMENTRATINGBASIS")
                        pbSetting.Properties.Add(new PropertySpec(BMC.Common.TypeHandler.GetRowValue<string>(dr, "Name"), typeof(PreCommitmentRatingBasis), null, "Specifies whether Pre Commitment rating interval based on Time or HandlePulls", (PreCommitmentRatingBasis)Enum.Parse(typeof(PreCommitmentRatingBasis), BMC.Common.TypeHandler.GetRowValue<string>(dr, "Value"))));

                    else if (BMC.Common.TypeHandler.GetRowValue<string>(dr, "Name").ToUpper() == "TICKET_EXPIRE")
                        pbSetting.Properties.Add(new PropertySpec(BMC.Common.TypeHandler.GetRowValue<string>(dr, "Name"), typeof(uint), "Ticket", BMC.Common.TypeHandler.GetRowValue<string>(dr, "Description"), false));

                    else if (BMC.Common.TypeHandler.GetRowValue<string>(dr, "Name").ToUpper() == "MAXIMUMPROMOTIONALTICKETSCOUNT")
                        pbSetting.Properties.Add(new PropertySpec(BMC.Common.TypeHandler.GetRowValue<string>(dr, "Name"), typeof(uint), "PromoTicket", BMC.Common.TypeHandler.GetRowValue<string>(dr, "Description"), false));
                    else
                        pbSetting.Properties.Add(new PropertySpec(BMC.Common.TypeHandler.GetRowValue<string>(dr, "Name"), typeof(string), "BMC Category", BMC.Common.TypeHandler.GetRowValue<string>(dr, "Description"), BMC.Common.TypeHandler.GetRowValue<string>(dr, "Value")));
                }
                pgbSiteSettings.PropertyGridContorl.SelectedObject = pbSetting;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        
        private void pbSetting_GetValue(object sender, PropertySpecEventArgs e)
        {
            try
            {
                if (e.Property.Name == "TIMEZONENAME")
                {
                    e.Value = (TimeZoneNames)Enum.Parse(typeof(TimeZoneNames), BMC.Common.TypeHandler.GetRowValue<string>(dtSettings.Select("Name = '" + e.Property.Name + "'")[0], "Value"));
                    return;
                }
                if (e.Property.Name.ToUpper() == "LIQUIDATIONTYPE")
                {
                    e.Value = (LiquidationType)Enum.Parse(typeof(LiquidationType), BMC.Common.TypeHandler.GetRowValue<string>(dtSettings.Select("Name = '" + e.Property.Name + "'")[0], "Value"));
                    return;
                }
                if (e.Property.Name.ToUpper() == "PRECOMMITMENTRATINGBASIS")
                {
                    e.Value = (PreCommitmentRatingBasis)Enum.Parse(typeof(PreCommitmentRatingBasis), BMC.Common.TypeHandler.GetRowValue<string>(dtSettings.Select("Name = '" + e.Property.Name + "'")[0], "Value"));
                    return;
                }
                e.Value = BMC.Common.TypeHandler.GetRowValue<string>(dtSettings.Select("Name = '" + e.Property.Name + "'")[0], "Value");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        
        private void pbSetting_SetValue(object sender, PropertySpecEventArgs e)
        {
            PropertyClass Pb = new PropertyClass();
            bool isContains = false;
            try
            {
                Pb.Name = e.Property.Name;


                Pb.PrevValue = Convert.ToString(e.Property.PreviousValue);
                Pb.Value = e.Value.ToString();

                foreach (PropertyClass item in ChangedProperty)
                {
                    if (item.Name == e.Property.Name)
                    {
                        item.Value = e.Value.ToString();
                        isContains = true;
                        break;
                    }

                }
                if (isContains == false)
                {
                    ChangedProperty.Add(Pb);
                }

                dtSettings.Select("Name = '" + e.Property.Name + "'")[0]["Value"] = e.Value;
                if (Pb.Name == "TIMEZONENAME")
                {
                    TZN = (TimeZoneNames)e.Value;
                    dtSettings.Select("Name = '" + e.Property.Name + "'")[0]["Value"] = e.Value;
                    ChangedProperty.Add(Pb);
                }
                else if (Pb.Name.ToUpper() == "LIQUIDATIONTYPE")
                {
                    liqtype = (LiquidationType)e.Value;
                    dtSettings.Select("Name = '" + e.Property.Name + "'")[0]["Value"] = e.Value;
                    ChangedProperty.Add(Pb);
                }
                else if (Pb.Name.ToUpper() == "PRECOMMITMENTRATINGBASIS")
                {
                    pcRateBasis = (PreCommitmentRatingBasis)e.Value;
                    dtSettings.Select("Name = '" + e.Property.Name + "'")[0]["Value"] = e.Value;
                    ChangedProperty.Add(Pb);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void SaveAsNewProfile(string strTextValue)
        {
            //Calling Audit Method
            Audit_History AH = new Audit_History();
            //Populate required Values            
            AH.EnterpriseModuleName = ModuleNameEnterprise.CentralisedSettings;
            AH.Audit_Screen_Name = "CentralisedSiteSettings|Profile";
            AH.Audit_Desc = "Created new profile " + strTextValue;
            AH.AuditOperationType = OperationType.ADD;
            AH.Audit_Field = "Profile";
            AH.Audit_New_Vl = strTextValue;

            AH.Audit_User_ID = AppGlobals.Current.UserId;
            AH.Audit_User_Name = AppGlobals.Current.UserName;

            AuditViewerBusiness AVB = new AuditViewerBusiness(Common.Utilities.DatabaseHelper.GetConnectionString());
            AVB.InsertAuditData(AH, true);

            //Insert All the default settings.
            _CentralizedSiteSettingBiz.InsertAllSetting(strTextValue, true);
            try
            {
                //Update any values which have been modified.
                foreach (PropertyClass propClass in ChangedProperty)
                {
                    _CentralizedSiteSettingBiz.UpdateSetting(propClass.Name, propClass.Value, strTextValue);

                    // call auditing function
                    AuditProfileUpdate(propClass, strTextValue);
                }
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_PROFILE_SAVED"), this.Text);
                RefreshProfileList(strTextValue);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void ApplyProfile()
        {
            bool bChecked = false;
            DataTable dtSites;

            try
            {
                foreach (TreeNode tNode in tvwSiteList.Nodes["All Sites"].Nodes)
                {
                    if (tNode.Checked)
                    {
                        bChecked = true;
                        break;
                    }
                }
                if (bChecked)
                {
                    foreach (TreeNode tNode in tvwSiteList.Nodes["All Sites"].Nodes)
                    {
                        if (tNode.Checked)
                        {
                            _CentralizedSiteSettingBiz.UpdateProfileForSite(tNode.Name, cmbProfilesList.SelectedItem.ToString());

                            dtSites = _CentralizedSiteSettingBiz.GetSiteList(tNode.Text.Substring(0, tNode.Text.IndexOf('[')).Trim(), 0);

                            _CentralizedSiteSettingBiz.InsertExportHistory(dtSites.Rows[0]["Site_ID"].ToString(), dtSites.Rows[0]["Site_Code"].ToString());
                            
                            tNode.Checked = false;

                            AuditViewerBusiness.CreateInstance(Common.Utilities.DatabaseHelper.GetConnectionString());

                            Audit_History AH = new Audit_History();
                            //Populate required Values
                            AH.EnterpriseModuleName = ModuleNameEnterprise.CentralisedSettings;
                            AH.Audit_Screen_Name = "CentralisedSiteSettings|Profile";
                            AH.Audit_Desc = "The profile " + cmbProfilesList.SelectedItem.ToString() + " applied for site" + tNode.Name;
                            AH.AuditOperationType = OperationType.ADD;
                            AH.Audit_User_ID = AppGlobals.Current.UserId;
                            AH.Audit_User_Name = AppGlobals.Current.UserName;
                            AuditViewerBusiness.InsertAuditData(AH);
                        }
                    }

                    tvwSiteList.Nodes["All Sites"].Checked = false;
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_PROFILE_APPLIED_SUCCESS"), this.Text);
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_RESTART_BMC_SERVICES"), this.Text);
                    tvwSiteList.Select();

                    _CentralizedSiteSettingBiz = CentralizedSiteSettingBiz.CreateInstance();
                    BuildTreeViewList();
                    RefreshProfileList(string.Empty);
                    tvwSiteList.ExpandAll();
                }
                else
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_SELECT_CHECKBOX"), this.Text);
                    tvwSiteList.Select();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void AuditProfileUpdate(PropertyClass propClass, string ProfileName)
        {
            //Calling Audit Method
            Audit_History AH = new Audit_History();
            //Populate required Values
            AH.EnterpriseModuleName = ModuleNameEnterprise.CentralisedSettings;
            AH.Audit_Screen_Name = "CentralisedSiteSettings|Site";

            string Desc;
            Desc = "'" + ProfileName + "' profile modified ..[" + propClass.Name + "]: " + propClass.PrevValue + " --> " + propClass.Value;

            if (Desc.Length > 500)
            {
                AH.Audit_Desc = Desc.Substring(1, 500);
            }
            else
            {
                AH.Audit_Desc = Desc;
            }
            AH.AuditOperationType = OperationType.MODIFY;
            AH.Audit_Field = "PropertyValue";
            AH.Audit_New_Vl = propClass.Value; //object available from page
            AH.Audit_Old_Vl = propClass.PrevValue;  // previous value

            AH.Audit_User_ID = AppGlobals.Current.UserId;
            AH.Audit_User_Name = AppGlobals.Current.UserName;

            AuditViewerBusiness AVB = new AuditViewerBusiness(Common.Utilities.DatabaseHelper.GetConnectionString());
            AVB.InsertAuditData(AH, true);
        }

        #endregion User Methods
    }

    #region MetaData
    public class PropertyClass
    {
        public string ID;
        public string Name;
        public string Value;
        public string PrevValue;
    }

    public enum TimeZoneNames
    {
        Afghanistan_Standard_Time,
        Alaskan_Standard_Time,
        Arab_Standard_Time,
        Arabian_Standard_Time,
        Arabic_Standard_Time,
        Argentina_Standard_Time,
        Armenian_Standard_Time,
        Atlantic_Standard_Time,
        AUS_Central_Standard_Time,
        AUS_Eastern_Standard_Time,
        Azerbaijan_Standard_Time,
        Azores_Standard_Time,
        Bangladesh_Standard_Time,
        Canada_Central_Standard_Time,
        Cape_Verde_Standard_Time,
        Caucasus_Standard_Time,
        Central_America_Standard_Time,
        Central_Asia_Standard_Time,
        Central_Brazilian_Standard_Time,
        Central_Europe_Standard_Time,
        Central_European_Standard_Time,
        Central_Pacific_Standard_Time,
        Central_Standard_Time,
        China_Standard_Time,
        Dateline_Standard_Time,
        Eastern_Standard_Time,
        Egypt_Standard_Time,
        Ekaterinburg_Standard_Time,
        Fiji_Standard_Time,
        FLE_Standard_Time,
        Georgian_Standard_Time,
        GMT_Standard_Time,
        Greenland_Standard_Time,
        Greenwich_Standard_Time,
        GTB_Standard_Time,
        Hawaiian_Standard_Time,
        India_Standard_Time,
        Iran_Standard_Time,
        Israel_Standard_Time,
        Jordan_Standard_Time,
        Kamchatka_Standard_Time,
        Korea_Standard_Time,
        Mauritius_Standard_Time,
        Mexico_Standard_Time,
        Mexico_Standard_Time_2,
        Middle_East_Standard_Time,
        Montevideo_Standard_Time,
        Morocco_Standard_Time,
        Mountain_Standard_Time,
        Myanmar_Standard_Time,
        Namibia_Standard_Time,
        Nepal_Standard_Time,
        New_Zealand_Standard_Time,
        Newfoundland_Standard_Time,
        North_Asia_East_Standard_Time,
        North_Asia_Standard_Time,
        Pacific_SA_Standard_Time,
        Pacific_Standard_Time,
        Pakistan_Standard_Time,
        Paraguay_Standard_Time,
        Romance_Standard_Time,
        Russian_Standard_Time,
        SA_Eastern_Standard_Time,
        SA_Pacific_Standard_Time,
        SA_Western_Standard_Time,
        Samoa_Standard_Time,
        SE_Asia_Standard_Time,
        Singapore_Standard_Time,
        South_Africa_Standard_Time,
        Sri_Lanka_Standard_Time,
        Taipei_Standard_Time,
        Tasmania_Standard_Time,
        Tokyo_Standard_Time,
        Tonga_Standard_Time,
        Ulaanbaatar_Standard_Time,
        US_Eastern_Standard_Time,
        US_Mountain_Standard_Time,
        UTC,
        Venezuela_Standard_Time,
        Vladivostok_Standard_Time,
        West_Asia_Standard_Time,
        West_Pacific_Standard_Time,
        Yakutsk_Standard_Time
    }

    public enum LiquidationType
    {
        Collection,
        Read
    }

    public enum PreCommitmentRatingBasis
    {
        Time,
        HandlePulls
    }

    #endregion MetaData
}
