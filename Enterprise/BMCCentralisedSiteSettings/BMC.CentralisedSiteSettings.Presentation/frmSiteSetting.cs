using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BMC.CentralisedSiteSettings.DataAccess;
using BMC.CentralisedSiteSettings.Business;

using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.Security;
using BMC.Common;
using BMC.CoreLib.Win32;

#region Revision History
//
// Mar 2010 C.Taylor ( contractor )     Added auditing hooks for the update of settings and creation of profiles
// Mar 2010 C.Taylor ( contractor )     set module id as 502
//
#endregion

namespace BMC.CentralisedSiteSettings.Presentation
{
    public partial class frmSiteSetting : Form
    {
        #region Variables
        List<PropertyClass> ChangedProperty = new List<PropertyClass>();
        DataTable dtSettings = new DataTable();
        const string MessageHeader = "BMC Centralized Site Settings";

        int LoginUserID;
        String LoginUserName;

        #endregion Variables

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        public frmSiteSetting()
        {
            InitializeComponent();
            SetTagProperty();
            this.cmbProfilesList.SelectedValueChanged += new EventHandler(cmbProfilesList_SelectedValueChanged);
        }

        public frmSiteSetting(string loginID, string loginName):this()
        {
            LoginUserID = Convert.ToInt32(CryptographyHelper.Decrypt(loginID));
            LoginUserName = CryptographyHelper.Decrypt(loginName);

            //InitializeComponent();
            //this.cmbProfilesList.SelectedValueChanged += new EventHandler(cmbProfilesList_SelectedValueChanged);
        }
        #endregion Constructor

        #region Control Events
        /// <summary>
        /// The Profiles List Combo Selected Value Changed Value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbProfilesList_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadPropertyGrid(cmbProfilesList.SelectedItem.ToString().Trim());
        }

        /// <summary>
        /// Exit the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Load method of the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SiteSetting_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
            tvwSiteList.CheckBoxes = true;
            //Refresh the Tree View List.
            RefreshTreeViewList();
            //Refresh the Profile List.
            RefreshProfileList("");
        }

        /// <summary>
        /// Edit Profile Button click Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditProfile_Click(object sender, EventArgs e)
        {
            SaveProfile frmSaveProfile = new SaveProfile();
            frmSaveProfile.OkClickEvent += new SaveProfile.OkClicked(frmSaveProfile_OkClickEvent);
            frmSaveProfile.ShowDialog();
            tvwSiteList.Select();
        }

        /// <summary>
        /// Save Profile Click Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="strTextValue"></param>
        void frmSaveProfile_OkClickEvent(object sender, EventArgs e, string strTextValue)
        {
            SaveNewProfile(strTextValue);
        }
        /// <summary>
        /// Update Profile Button click Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (PropertyClass propClass in ChangedProperty)
                {
                    DBHelper.UpdateSetting(propClass.Name, propClass.Value, cmbProfilesList.SelectedItem.ToString());

                    // call auditing function
                    AuditProfileUpdate(propClass, cmbProfilesList.SelectedItem.ToString());
                }
                DataTable dtSites = DBHelper.GetSitesForProfile(cmbProfilesList.SelectedItem.ToString());
                foreach (DataRow dr in dtSites.Rows)
                {
                    DBHelper.InsertExportHistory(Convert.ToInt32(dr["Site_ID"]), "");
                }
                //MessageBox.Show(this.GetResourceTextByKey(1, "MSG_VALUES_UPDATED"), this.GetResourceTextByKey(1, "MSG_APP_TITLE"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_VALUES_UPDATED"), this.Text);
                tvwSiteList.Select();
            }
            catch (Exception exbtnUpdateProfile_Click)
            {
                LogManager.WriteLog("Error Occured in btnUpdateProfile_Click Method.\n" + exbtnUpdateProfile_Click.Message);
            }
        }

        /// <summary>
        /// Treeview node selected change event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeviewSiteList_AfterSelect(object sender, TreeViewEventArgs e)
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
                    strProfile = DBHelper.GetProfileNameForSite(tvwSiteList.SelectedNode.Name);
                    cmbProfilesList.SelectedItem = string.IsNullOrEmpty(strProfile) ? "Select Profile" : strProfile.Trim();
                }
            }
            catch (Exception extreeviewSiteList_AfterSelect)
            {
                LogManager.WriteLog("Error Occured in extreeviewSiteList_AfterSelect Method.\n" + extreeviewSiteList_AfterSelect.Message);
            }
        }

        /// <summary>
        /// Apply Profile Button click Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApplyProfile_Click(object sender, EventArgs e)
        {
            ApplyProfile();
        }

        /// <summary>
        /// Property Grid Get Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private TimeZoneNames TZN = TimeZoneNames.UTC;
        private LiquidationType liqtype = LiquidationType.Collection;
        private PreCommitmentRatingBasis pcRateBasis = PreCommitmentRatingBasis.Time;

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
            catch (Exception expbSetting_GetValue)
            {
                LogManager.WriteLog("Error Occured in Setting_GetValue Method.\n" + expbSetting_GetValue.Message);
            }
        }

        /// <summary>
        /// Property Grid Set Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            catch (Exception expbSetting_SetValue)
            {
                LogManager.WriteLog("Error Occured in pbSetting_SetValue Method.\n" + expbSetting_SetValue.Message);
            }
        }

        /// <summary>
        /// Treeview Node Checked event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeviewSiteList_AfterCheck(object sender, TreeViewEventArgs e)
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
            catch (Exception extreeviewSiteList_AfterCheck)
            {
                LogManager.WriteLog("Error Occured in treeviewSiteList_AfterCheck Method.\n" + extreeviewSiteList_AfterCheck.Message);
            }
        }

        /// <summary>
        /// Profile Combo selected change event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbProfilesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProfilesList.SelectedItem.ToString().Trim() == "Select Profile")
            {
                DisableAllControls();
                SetTagProperty();
                this.ResolveResources();
            }
            else
            {
                EnableAllControls();
                SetTagProperty();
                this.ResolveResources();
            }
        }
        #endregion Control Events

        #region Public Methods

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.btnApplyProfile.Tag = "Key_Apply";
            this.btnExit.Tag = "Key_ExitCaption";
            this.btnSaveProfile.Tag = "Key_SaveAsNewProfile";
            this.lblProfileName.Tag = "Key_ProfileNameColon";
            this.btnUpdateProfile.Tag = "Key_SaveCaption";
            this.groupBox1.Tag = "Key_SiteSettings";
            this.grpBoxSite.Tag = "Key_Sites";
            this.Tag = "Key_CentralisedSiteSettings";
        }

        /// <summary>
        /// Loads the Property Grid Data.
        /// </summary>
        /// <param name="strProfileName"></param>
        public void LoadPropertyGrid(string strProfileName)
        {
            dtSettings = DBHelper.GetSettingsList(strProfileName, false);
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
                PGBSiteSettings.PropertyGridContorl.SelectedObject = pbSetting;
            }
            catch (Exception exLoadPropertyGrid)
            {
                LogManager.WriteLog("Error Occured in LoadPropertyGrid Method.\n" + exLoadPropertyGrid.Message);
            }
        }

        /// <summary>
        /// Refreshes the Profile List.
        /// </summary>
        /// <param name="strTextValue"></param>
        public void RefreshProfileList(string strTextValue)
        {
            DataTable dtProfileList = DBHelper.GetProfileList();
            try
            {
                //Clear Existing Data.
                cmbProfilesList.Items.Clear();
                //Add a default value.
                cmbProfilesList.Items.Add(this.GetResourceTextByKey("Key_SelectProfile"));//"Select Profile");
                foreach (DataRow dr in dtProfileList.Rows)
                {
                    cmbProfilesList.Items.Add(dr["SettingsProfile_Description"].ToString());
                }
                //Set the default value to the first value.
                cmbProfilesList.SelectedItem = cmbProfilesList.Items[0];
                foreach (Object objProfile in cmbProfilesList.Items)
                {
                    if (objProfile.ToString() == strTextValue)
                    {
                        cmbProfilesList.SelectedItem = objProfile;
                        break;
                    }
                }
            }
            catch (Exception exRefreshProfileList)
            {
                LogManager.WriteLog("Error Occured in RefreshProfileList Method.\n" + exRefreshProfileList.Message);
            }
        }

        /// <summary>
        /// Method to Refresh the Tree View List.
        /// </summary>
        public void RefreshTreeViewList()
        {
            DataTable dtSiteList = DBHelper.GetSiteList();
            try
            {
                //Clear Existing Data.
                tvwSiteList.Nodes.Clear();

                TreeNode tNode = tvwSiteList.Nodes.Add("All Sites",this.GetResourceTextByKey("Key_AllSites"));
                foreach (DataRow dr in dtSiteList.Rows)
                {
                    TreeNode myNode;
                    myNode = tNode.Nodes.Add(dr["Site_Code"].ToString(), dr["Site_Name"].ToString());
                }
                tvwSiteList.ExpandAll();
            }
            catch (Exception exRefreshTreeViewList)
            {
                LogManager.WriteLog("Error Occured in RefreshTreeViewList Method.\n" + exRefreshTreeViewList.Message);
            }
        }

        /// <summary>
        /// Method to Disable controls when no specific profile is specified.
        /// </summary>
        public void DisableAllControls()
        {
            btnApplyProfile.Enabled = false;
            btnSaveProfile.Enabled = false;
            btnUpdateProfile.Enabled = false;
            PGBSiteSettings.PropertyGridContorl.Enabled = false;
        }

        /// <summary>
        /// Method to Enable controls when a specific profile is specified.
        /// </summary>
        public void EnableAllControls()
        {
            btnApplyProfile.Enabled = true;
            btnSaveProfile.Enabled = true;
            btnUpdateProfile.Enabled = true;
            PGBSiteSettings.PropertyGridContorl.Enabled = true;
        }

        /// <summary>
        /// Method to apply the profile.
        /// </summary>
        public void ApplyProfile()
        {
            bool bChecked = false;
            DataTable dtSites;
            int SiteID;
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
                            DBHelper.UpdateProfileForSite(tNode.Name, cmbProfilesList.SelectedItem.ToString());

                            // need to show changes.
                            //if ((bool)tNode.Tag == false)
                            //{
                            // Site has been added to profile, create ADD profile

                            //};

                            dtSites = DBHelper.GetSiteList(tNode.Text.Substring(0, tNode.Text.IndexOf('[')).Trim(), 0);
                            SiteID = Convert.ToInt32(dtSites.Rows[0]["Site_ID"]);
                            DBHelper.InsertExportHistory(SiteID, "");
                            tNode.Checked = false;

                            AuditViewerBusiness.CreateInstance(Common.Utilities.DatabaseHelper.GetConnectionString());

                            Audit_History AH = new Audit_History();
                            //Populate required Values
                            AH.EnterpriseModuleName = ModuleNameEnterprise.CentralisedSettings;
                            AH.Audit_Screen_Name = "CentralisedSiteSettings|Profile";
                            AH.Audit_Desc = "The profile " + cmbProfilesList.SelectedItem.ToString() + " applied for site" + tNode.Name;
                            AH.AuditOperationType = OperationType.ADD;
                            AH.Audit_User_ID = LoginUserID;
                            AH.Audit_User_Name = LoginUserName;
                            AuditViewerBusiness.InsertAuditData(AH);
                        }
                        else
                        {
                            // need to show changes.
                            //if ((bool)tNode.Tag == true)
                            //{
                            // site has been removed from profile, create delete profile
                            //}
                        }
                    }
                    tvwSiteList.Nodes["All Sites"].Checked = false;
                    //MessageBox.Show(this.GetResourceTextByKey(1, "MSG_PROFILE_APPLIED_SUCCESS"), this.GetResourceTextByKey(1, "MSG_APP_TITLE"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_PROFILE_APPLIED_SUCCESS"), this.Text);
                    //MessageBox.Show(this.GetResourceTextByKey(1, "MSG_RESTART_BMC_SERVICES"), this.GetResourceTextByKey(1, "MSG_APP_TITLE"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_RESTART_BMC_SERVICES"), this.Text);
                    tvwSiteList.Select();
                }
                else
                {
                    //MessageBox.Show(this.GetResourceTextByKey(1, "MSG_SELECT_CHECKBOX"), this.GetResourceTextByKey(1, "MSG_APP_TITLE"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_SELECT_CHECKBOX"), this.Text);
                    tvwSiteList.Select();
                }
            }
            catch (Exception exApplyProfile)
            {
                LogManager.WriteLog("Error Occured in ApplyProfile Method.\n" + exApplyProfile.Message);
            }
        }

        /// <summary>
        /// Method to save a new profile based on the default profile.
        /// </summary>
        public void SaveNewProfile(string strTextValue)
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

            AH.Audit_User_ID = LoginUserID;
            AH.Audit_User_Name = LoginUserName;

            AuditViewerBusiness AVB = new AuditViewerBusiness(Common.Utilities.DatabaseHelper.GetConnectionString());
            AVB.InsertAuditData(AH, true);


            //Insert All the default settings.
            DBHelper.InsertAllSetting(strTextValue, true);
            try
            {
                //Update any values which have been modified.
                foreach (PropertyClass propClass in ChangedProperty)
                {
                    DBHelper.UpdateSetting(propClass.Name, propClass.Value, strTextValue);

                    // call auditing function
                    AuditProfileUpdate(propClass, strTextValue);
                }
                //MessageBox.Show(this.GetResourceTextByKey(1, "MSG_PROFILE_SAVED"), this.GetResourceTextByKey(1, "MSG_APP_TITLE"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_PROFILE_SAVED"), this.Text);
                RefreshProfileList(strTextValue);
            }
            catch (Exception exSaveNewProfile)
            {
                LogManager.WriteLog("Error Occured in SaveNewProfile Method.\n" + exSaveNewProfile.Message);
            }
        }


        /// <summary>
        /// Method to create and audit record if the old and new values are different
        /// </summary>
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

            AH.Audit_User_ID = LoginUserID;
            AH.Audit_User_Name = LoginUserName;

            AuditViewerBusiness AVB = new AuditViewerBusiness(Common.Utilities.DatabaseHelper.GetConnectionString());
            AVB.InsertAuditData(AH, true);
        }

        #endregion Public Methods

        #region Class
        /// <summary>
        /// Class to store the structure for the property Data.
        /// </summary>
        public class PropertyClass
        {
            public string ID;
            public string Name;
            public string Value;
            public string PrevValue;
        }
        #endregion Class


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

            //W._Europe_Standard_Time, 
            //W._Central_Africa_Standard_Time,
            //E._Europe_Standard_Time,
            //E._Africa_Standard_Time,
            //N._Central_Asia_Standard_Time,
            //W._Australia_Standard_Time,
            //Cen._Australia_Standard_Time,
            //E._Australia_Standard_Time,
            //UTC+12,
            //UTC-02,
            //Mid-Atlantic_Standard_Time,
            //E._South_America_Standard_Time,
            //Central_Standard_Time_(Mexico),
            //Mountain_Standard_Time_(Mexico),
            //Pacific_Standard_Time_(Mexico),
            //UTC-11,

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
    }
}
