using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BMC.CentralisedSiteSettings.DataAccess;
using BMC.CentralisedSiteSettings.Business;
using BMC.Common.ConfigurationManagement;
using BMC.CoreLib.Win32;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common;
#region Revision History
//
// Mar 2010 C.Taylor ( contractor )     Added auditing hooks for the update of settings and creation of profiles
//
#endregion

namespace BMC.CentralisedSiteSettings.Presentation
{
    public partial class ProfileConfiguration : Form
    {
        #region Variables
        List<PropertyClass> ChangedProperty = new List<PropertyClass>();
        DataTable dtSettings = new DataTable();


        #endregion Variables

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        public ProfileConfiguration()
        {
            InitializeComponent();
            SetTagProperty();
            this.cmbProfilesList.SelectedValueChanged += new EventHandler(cmbProfilesList_SelectedValueChanged);
            AuditViewerBusiness.CreateInstance(Common.Utilities.DatabaseHelper.GetConnectionString());
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
            if (btnExit.Text == "Close")
            {
                this.Close();
            }
            else if (btnExit.Text == "Cancel")
            {
                btnExit.Text = this.GetResourceTextByKey("Key_CloseCaption"); //"Close";
                btnSaveProfile.Text = this.GetResourceTextByKey("Key_NewCaption");//"New";
                propertyGridBag1.Enabled = false;
                btnUpdateProfile.Visible = true;
                cmbProfilesList.Enabled = true;

                //Reload Again.
                LoadPropertyGrid(cmbProfilesList.SelectedItem.ToString().Trim());

                //Clear the changed peoperty.
                ChangedProperty.Clear();
            }
        }

        private void ProfileConfiguration_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
        }
        /// <summary>
        /// Load method of the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SiteSetting_Load(object sender, EventArgs e)
        {
            try
            {
                //Refresh the Profile List.
                RefreshProfileList("");
                this.propertyGridBag1.Enabled = false;
            }
            catch (Exception exSiteSetting_Load)
            {
                LogManager.WriteLog("Error Occured in SiteSetting_Load Method.\n" + exSiteSetting_Load.Message);
            }
        }

        /// <summary>
        /// Edit Profile Button click Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditProfile_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSaveProfile.Text == "New")
                {
                    SaveProfile frmSaveProfile = new SaveProfile();
                    frmSaveProfile.OkClickEvent += new SaveProfile.OkClicked(frmSaveProfile_OkClickEvent);
                    frmSaveProfile.ShowDialog();
                }
                else if (btnSaveProfile.Text == "OK")
                {
                    foreach (PropertyClass propClass in ChangedProperty)
                    {

                        if ((propClass.Name == "TICKET_EXPIRE") && (propClass.Value != null))
                        {
                            if (propClass.Value.Contains("."))
                            {
                                //MessageBox.Show(this.GetResourceTextByKey(1, "MSG_VALIDTKT_EXPIRE_DAYS"), this.GetResourceTextByKey(1, "MSG_APP_TITLE"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_VALIDTKT_EXPIRE_DAYS"), this.Text);
                                ChangedProperty.Remove(propClass);
                                return;
                            }
                            if ((Convert.ToInt32(propClass.Value)) < 0)
                            {
                                //MessageBox.Show(this.GetResourceTextByKey(1, "MSG_VALIDTKT_EXPIRE_DAYS"), this.GetResourceTextByKey(1, "MSG_APP_TITLE"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_VALIDTKT_EXPIRE_DAYS"), this.Text);
                                ChangedProperty.Remove(propClass);
                                return;
                            }
                        }

                        DBHelper.UpdateSetting(propClass.Name, propClass.Value, cmbProfilesList.SelectedItem.ToString());

                        // call auditing function
                        AuditProfileUpdate(propClass, cmbProfilesList.SelectedItem.ToString());

                    }

                    //MessageBox.Show(this.GetResourceTextByKey(1, "MSG_PROFILE_UPDATED"), this.GetResourceTextByKey(1, "MSG_APP_TITLE"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_PROFILE_UPDATED"), this.Text);
                    //Revert to old state.
                    ChangedProperty = new List<PropertyClass>();
                    btnExit.Text = this.GetResourceTextByKey("Key_CloseCaption"); //"Close";
                    btnSaveProfile.Text = this.GetResourceTextByKey("Key_NewCaption");//"New";
                    propertyGridBag1.Enabled = false;
                    btnUpdateProfile.Visible = true;
                    cmbProfilesList.Enabled = true;
                }
            }
            catch (Exception exbtnEditProfile_Click)
            {
                LogManager.WriteLog("Error Occured in btnEditProfile_Click Method.\n" + exbtnEditProfile_Click.Message);
            }
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
                btnExit.Text = "Cancel";
                btnSaveProfile.Text = "OK";
                propertyGridBag1.Enabled = true;
                btnUpdateProfile.Visible = false;
                cmbProfilesList.Enabled = false;
            }
            catch (Exception exbtnUpdateProfile_Click)
            {
                LogManager.WriteLog("Error Occured in btnUpdateProfile_Click Method.\n" + exbtnUpdateProfile_Click.Message);
            }
        }

        /// <summary>
        /// Property Grid Get Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbSetting_GetValue(object sender, PropertySpecEventArgs e)
        {
            try
            {
                e.Value = BMC.Common.TypeHandler.GetRowValue<string>(dtSettings.Select("Name = '" + e.Property.Name + "'")[0], "Value");
            }
            catch (Exception expbSetting_GetValue)
            {
                LogManager.WriteLog("Error Occured in pbSetting_GetValue Method.\n" + expbSetting_GetValue.Message);
            }
        }

        /// <summary>
        /// Property Grid Set Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbSetting_SetValue(object sender, PropertySpecEventArgs e)
        {
            try
            {
                PropertyClass Pb = new PropertyClass();
                Pb.Name = e.Property.Name;
                Pb.PrevValue = Pb.Value;
                Pb.Value = e.Value.ToString();

                ChangedProperty.Add(Pb);
                dtSettings.Select("Name = '" + e.Property.Name + "'")[0]["Value"] = e.Value;
            }
            catch (Exception expbSetting_SetValue)
            {
                LogManager.WriteLog("Error Occured in pbSetting_SetValue Method.\n" + expbSetting_SetValue.Message);
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
                DisableAllControls();
            else
                EnableAllControls();
        }
        #endregion Control Events

        #region Public Methods

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.btnExit.Tag = "Key_Close";
            this.btnUpdateProfile.Tag = "Key_Edit";
            this.btnSaveProfile.Tag = "Key_New";
            this.lblProfileName.Tag = "Key_ProfileNameColon";
            this.groupBox1.Tag = "Key_SiteSettings";

        }

        /// <summary>
        /// Loads the Property Grid Data.
        /// </summary>
        /// <param name="strProfileName"></param>
        public void LoadPropertyGrid(string strProfileName)
        {
            //PropertyGridSettings propSettings = new PropertyGridSettings();
            //// Assign values to the properties
            //propSettings.Age = 50;
            //propSettings.Address = " 114 Maple Drive ";
            ////propSettings.DateOfBirth = Convert.ToDateTime(" 9/14/78");
            //propSettings.SSN = "123-345-3566";
            //propSettings.Email = "bill@aol.com";
            //propSettings.Name = "Bill Smith"; 
            // Sets the the grid with the customer instance to be
            // browsed
            //propertyGrid1.SelectedObject = propSettings;

            try
            {
                dtSettings = DBHelper.GetSettingsList(strProfileName, true);
                PropertyBag pbSetting = new PropertyBag();
                pbSetting.GetValue += new PropertySpecEventHandler(pbSetting_GetValue);
                pbSetting.SetValue += new PropertySpecEventHandler(pbSetting_SetValue);
                foreach (DataRow dr in dtSettings.Rows)
                {
                    if (BMC.Common.TypeHandler.GetRowValue<string>(dr, "Value").ToUpper() == "TRUE" || BMC.Common.TypeHandler.GetRowValue<string>(dr, "Value").ToUpper() == "FALSE")
                        if (BMC.Common.TypeHandler.GetRowValue<string>(dr, "Value").ToUpper() == "TRUE")
                            pbSetting.Properties.Add(new PropertySpec(BMC.Common.TypeHandler.GetRowValue<string>(dr, "Name"), typeof(bool), "BMC Category", null, true));
                        else
                            pbSetting.Properties.Add(new PropertySpec(BMC.Common.TypeHandler.GetRowValue<string>(dr, "Name"), typeof(bool), "BMC Category", null, false));
                    else
                        pbSetting.Properties.Add(new PropertySpec(BMC.Common.TypeHandler.GetRowValue<string>(dr, "Name"), typeof(string), "BMC Category", null, BMC.Common.TypeHandler.GetRowValue<string>(dr, "Value")));

                }
                propertyGridBag1.PropertyGridContorl.SelectedObject = pbSetting;
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
            try
            {
                DataTable dtProfileList = DBHelper.GetProfileList();
                //Clear Existing Data.
                cmbProfilesList.Items.Clear();
                //Add a default value.
                //cmbProfilesList.Items.Add("Select Profile");
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
                LogManager.WriteLog("Error Occured in exRefreshProfileList Method.\n" + exRefreshProfileList.Message);
            }
        }

        /// <summary>
        /// Method to Disable controls when no specific profile is specified.
        /// </summary>
        public void DisableAllControls()
        {
            btnSaveProfile.Enabled = false;
            btnUpdateProfile.Enabled = false;
            propertyGridBag1.PropertyGridContorl.Enabled = false;
        }

        /// <summary>
        /// Method to Enable controls when a specific profile is specified.
        /// </summary>
        public void EnableAllControls()
        {
            btnSaveProfile.Enabled = true;
            btnUpdateProfile.Enabled = true;
            propertyGridBag1.PropertyGridContorl.Enabled = true;
        }

        /// <summary>
        /// Method to save a new profile based on the default profile.
        /// </summary>
        public void SaveNewProfile(string strTextValue)
        {
            try
            {
                //Insert All the default settings.
                DBHelper.InsertAllSetting(strTextValue, true);

                ////Calling Audit Method
                Audit_History AH = new Audit_History();
                //Populate required Values
                AH.EnterpriseModuleName = ModuleNameEnterprise.CentralisedSettings;
                AH.Audit_Screen_Name = "CentralisedSiteSettings|Profile";
                AH.Audit_Desc = "Created new profile " + strTextValue;
                AH.AuditOperationType = OperationType.ADD;
                AH.Audit_Field = "PropertyValue";
                AH.Audit_New_Vl = strTextValue; //object available from page

                //AuditViewerBusiness AVB = new AuditViewerBusiness(Common.Utilities.DatabaseHelper.GetConnectionString());
                //int nReturnValue;
                //nReturnValue = AVB.InsertAuditData(AH);

                AuditViewerBusiness.InsertAuditData(AH);


                //Update any values which have been modified.
                foreach (PropertyClass propClass in ChangedProperty)
                {
                    DBHelper.UpdateSetting(propClass.Name, propClass.Value, strTextValue);

                    // call auditing function
                    AuditProfileUpdate(propClass, strTextValue);

                }
                //MessageBox.Show(this.GetResourceTextByKey(1, "MSG_NEW_PROFILE"), this.GetResourceTextByKey(1, "MSG_APP_TITLE"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_NEW_PROFILE"), this.Text);
                RefreshProfileList(strTextValue);
            }
            catch (Exception exSaveNewProfile)
            {
                LogManager.WriteLog("Error Occured in exSaveNewProfile Method.\n" + exSaveNewProfile.Message);
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
            AH.Audit_Screen_Name = "CentralisedSiteSettings";
            AH.Audit_Desc = propClass.Name + " modified for profile " + ProfileName;
            AH.AuditOperationType = OperationType.MODIFY;
            AH.Audit_Field = "PropertyValue";
            AH.Audit_New_Vl = propClass.Value; //object available from page
            AH.Audit_Old_Vl = propClass.PrevValue;  // previous value

            //AuditViewerBusiness AVB = new AuditViewerBusiness(Common.Utilities.DatabaseHelper.GetConnectionString());
            //int nReturnValue;
            //nReturnValue = AVB.InsertAuditData(AH);
            AuditViewerBusiness.InsertAuditData(AH);
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

        private void propertyGridBag1_Load(object sender, EventArgs e)
        {

        }
    }
}