using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.Utilities;
using System.Text.RegularExpressions;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using System.Collections.ObjectModel;
using BMC.CoreLib.Win32;
using System.IO;
using System.Diagnostics;
using BMC.SecurityVB;
using BMC.Security;
using BMC.Security.Manager;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.EnterpriseClient.Properties;
using BMC.EnterpriseDataAccess;
using System.Reflection;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class ucUserAdmin : UserControl
    {
        #region ToPass
        private int _UserID;

        private const string ScreenName = "User Administration => ";


        private BindingList<CustomerAccessDetailsResult> _collCustomerNotIncluded = null;
        private BindingList<CustomerAccessDetailsResult> _collCustomerIncluded = null;
        private UserAdministrationBiz objUserAdminBiz = null;

        #endregion

        #region Private Variables
        private int UserTableUserID;
        private string _UserName;
        private int CmbSupplier_ind = -1;
        private int CmbDepot_ind = -1;
        private int LstSurname_ind = -1;
        private new bool Update = false;
        private bool NewUser = false;
        private bool isUserTermiated = false;
        private string ProjectTitle;
        private byte islockedValue;
        public UserAuditEntity oAudit = new UserAuditEntity();
        UserAuditEntity oNewEntity = new UserAuditEntity();
        bool isEdit = false;
        #endregion

        #region Constructors
        public ucUserAdmin()
        {
            InitializeComponent();
            this._UserName = AppEntryPoint.Current.UserName; ;
            this._UserID = AppEntryPoint.Current.StaffId;
            objUserAdminBiz = UserAdministrationBiz.CreateInstance();
            // Set Tags for controls
            SetTagProperty();
        }
        #endregion

        #region User Defined Functions

        private void SetTagProperty()
        {
            try
            {
                this.gpCustomerAccessSecurityGroups.Tag = "Key_CustomerAccessSecurity";
                this.lblDateFormat.Tag = "Key_DateFormatColon";
                this.lblDepartment.Tag = "Key_DepartmentColon";
                this.lblDepot.Tag = "Key_DepotColon";
                this.BtnEdit.Tag = "Key_EditCaption";
                this.lblEMail.Tag = "Key_EMailColon";
                this.lblExt.Tag = "Key_ExtColon";
                this.lblIncluded.Tag = "Key_IncludedColon";
                this.chkEngineer.Tag = "Key_IsEngineer";
                this.chkRepresentative.Tag = "Key_IsRepresentative";
                this.chkStockController.Tag = "Key_IsStockController";
                this.lblJobTitleDescription.Tag = "Key_JobTitleDescriptionColon";
                this.btnLockUser.Tag = "Key_LockUser";
                this.lblMobile.Tag = "Key_MobileColon";
                this.btnNew.Tag = "Key_NewCaption";
                this.lblNotIncluded.Tag = "Key_NotIncludedColon";
                this.lblNotes.Tag = "Key_NotesColon";
                this.lblPersonnelNo.Tag = "Key_PersonnelNoColon";
                this.lblPostCode.Tag = "Key_PostcodeColon";
                this.btnResetPassword.Tag = "Key_ResetPasswordCaption";
                this.lblServiceArea.Tag = "Key_ServiceAreaColon";
                this.lblSupplier.Tag = "Key_SupplierColon";
                this.lblTelephone.Tag = "Key_TelephoneColon";
                this.chkTerminate.Tag = "Key_TerminateUser";
                this.label1.Tag = "Key_TitleColon";
                this.btnUnLockUser.Tag = "Key_UnLockUser";
                this.btnUpdate.Tag = "Key_UpdateCaption";
                this.lblUserLanguage.Tag = "Key_UserLanguageColon";
                this.lblUserLevel.Tag = "Key_UserLevelColon";
                this.lblSurname.Tag = "Key_SurnameMandatory";
                this.lblFirstName.Tag = "Key_FirstNameCaptionMandatory";
                this.lblCurrentPassword.Tag = "Key_CurrentPasswordMandatory";
                this.lblUserName.Tag = "Key_UserNameMandatory";
                this.lblWusername.Tag = "Key_WindowsUserNameMandatory";
                this.lblPasswordCheck.Tag = "Key_PasswordCheckMandatory";
                this.lblAddress.Tag = "Key_AddressColon";
                //this.btnAssignEmpCard.Tag = "Key_AssignEmpCardsCaption";
                this.btnAssignSite.Tag = "Key_AssignSitesCaption";
                this.btnCancel.Tag = "Key_CancelCaption";
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void Form_Load()
        {
            try
            {

                ProjectTitle = this.Text;
                LoadSupplier();
                LoadCustomerAccessLists();
                LoadUserGroupDetails();

                LoadLangDateCurrencyDetails();
                LoadRepresentativeTree();
                treeOps.Visible = false;
                LoadStaffName(_UserID);
                UnLockControls(false);
                Update = false;
                NewUser = false;
                btnUpdate.Visible = false;
                btnAssignSite.Enabled = false;
                btnCancel.Visible = false;
                //btnResetPassword.Enabled = false;
                if (AppGlobals.Current.UserId == UserTableUserID)
                    BtnEdit.Visible = AppGlobals.Current.HasUserAccess("HQ_Admin_Users_Edit");
                else
                    BtnEdit.Visible = AppGlobals.Current.HasUserAccess("HQ_Admin_Users_Editall");

                btnNew.Visible = AppGlobals.Current.HasUserAccess("HQ_Admin_Users_Editall");
                cmbSurname.Enabled = AppGlobals.Current.HasUserAccess("HQ_Admin_Users_ViewAll");
                cmbSupplier.Enabled = false;
                cmbDepot.Enabled = false;
                cmdServiceArea.Enabled = false;
                cmbLanguage.Enabled = false;
                cmbCurrency.Enabled = false;
                cmbDate.Enabled = false;
                cmbUserLevel.Enabled = false;
                //btnAssignEmpCard.Visible = SettingsEntity.IsEmployeeCardTrackingEnabled ? true : false;
               // btnAssignEmpCard.Enabled = false;
                btnLockUser.Enabled = false;
                //btnUnLockUser.Enabled = false;
                if (cmbSurname.Items.Count > 0)
                {
                    LstSurname_SelectedIndexChanged(cmbSurname, null);
                }
                else
                {
                    //  Win32Extensions.ShowInfoMessageBox("Please Create A New User");
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_UCUSERADMIN_CREATEUSER"), this.ParentForm.Text);
                }
                txtFirstName.Visible = false;
                txtSurname.Visible = false;
                this.ResolveResources();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadSupplier()
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Load operator details", LogManager.enumLogLevel.Info);
                List<OperatorEntity> lstOp = UserAdministrationBiz.CreateInstance().GetOperatorDetails();
                // lstOp.Insert(0, new OperatorEntity { Operator_ID = -1, Operator_Name = this.GetResourceTextByKey("Key_Any"), SelectedDepot = null });
                cmbSupplier.DataSource = lstOp;
                cmbSupplier.DisplayMember = "Operator_Name";
                cmbSupplier.ValueMember = "Operator_ID";
                if (lstOp.Count > 0)
                {
                    cmbSupplier.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void LoadCustomerAccessLists()
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Load CustomerAccess details", LogManager.enumLogLevel.Info);
                List<CustomerAccessDetailsResult> lstCust = UserAdministrationBiz.CreateInstance().GetCustomerAccessDetails();
                _collCustomerNotIncluded.Clear();
                foreach (CustomerAccessDetailsResult customerAccessDetailsResultItem in lstCust)
                {
                    _collCustomerNotIncluded.Add(customerAccessDetailsResultItem);
                }

                lstCustomerAccessNotIncluded.DataSource = _collCustomerNotIncluded;
                lstCustomerAccessNotIncluded.DisplayMember = "Customer_Access_Name";
                lstCustomerAccessNotIncluded.ValueMember = "Customer_Access_ID";

                _collCustomerIncluded.Clear();
                lstCustomerAccessIncluded.DataSource = _collCustomerIncluded;
                lstCustomerAccessNotIncluded.DisplayMember = "Customer_Access_Name";
                lstCustomerAccessNotIncluded.ValueMember = "Customer_Access_ID";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadUserGroupDetails()
        {
            try
            {
                UserGroupDetailsResult defaultlevel = new UserGroupDetailsResult();
                defaultlevel.User_Group_ID = 0;
                defaultlevel.User_Group_Name = this.GetResourceTextByKey("Key_DefaultSelect");

                LogManager.WriteLog(ScreenName + "Load User Group details", LogManager.enumLogLevel.Info);
                List<UserGroupDetailsResult> lstUserGroup = UserAdministrationBiz.CreateInstance().GetUserGroupDetails();
                //Initial item (select user level) for userlevel combo 
                lstUserGroup.Insert(0, defaultlevel);

                cmbUserLevel.DataSource = lstUserGroup;
                cmbUserLevel.DisplayMember = "User_Group_Name";
                cmbUserLevel.ValueMember = "User_Group_ID";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void LoadStaffName(int? Staff_ID)
        {
            try
            {
                //CheckSecurity

                List<StaffNameResult> lstStaff = UserAdministrationBiz.CreateInstance().GetStaffName(null, null, null);
                cmbSurname.DataSource = lstStaff;

                cmbSurname.DisplayMember = "Staff_Name";
                cmbSurname.ValueMember = "Staff_ID";
                if (Staff_ID.HasValue)
                {
                    int ind = lstStaff.FindIndex(se => se.Staff_ID == Staff_ID.Value);
                    cmbSurname.SelectedIndex = (ind >= 0) ? ind : 0;
                    LstSurname_SelectedIndexChanged(cmbSurname, null);
                    LstSurname_ind = cmbSurname.SelectedIndex;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadLangDateCurrencyDetails()
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Load Language,Date & Currency details", LogManager.enumLogLevel.Info);

                //Initial value of Language dropdown
                UserLanguagesDetailsResult defaultitem = new UserLanguagesDetailsResult();
                defaultitem.LanguageID = 0;
                defaultitem.LanguageDisplayName = this.GetResourceTextByKey("Key_DefaultSelect");

                List<UserLanguagesDetailsResult> lstUserLang = UserAdministrationBiz.CreateInstance().GetUserLangDetails();

                lstUserLang.Insert(0, defaultitem);

                cmbLanguage.DataSource = lstUserLang;
                cmbLanguage.DisplayMember = "LanguageDisplayName";
                cmbLanguage.ValueMember = "LanguageID";

                List<UserLanguagesDetailsResult> lstDate = new List<UserLanguagesDetailsResult>(lstUserLang);
                cmbDate.DataSource = lstDate;
                cmbDate.DisplayMember = "LanguageDisplayName";
                cmbDate.ValueMember = "LanguageID";

                List<UserLanguagesDetailsResult> lstCurrency = new List<UserLanguagesDetailsResult>(lstUserLang);
                cmbCurrency.DataSource = lstCurrency;
                cmbCurrency.DisplayMember = "LanguageDisplayName";
                cmbCurrency.ValueMember = "LanguageID";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UnLockControls(bool Unlock)
        {
            try
            {
                cmbSurname.Enabled = !Unlock;
                chkEngineer.Enabled = Unlock;
                chkRepresentative.Enabled = false;
                chkStockController.Enabled = false;

                chkTerminate.Enabled = Unlock && (txtStaffUserName.Text != AppEntryPoint.Current.UserName);
                txtAddress.Enabled = Unlock;
                txtExtensionNumber.Enabled = Unlock;
                txtFirstName.Enabled = Unlock;
                txtHomePhone.Enabled = Unlock;
                txtMobilePhone.Enabled = Unlock;
                txtPassword.Enabled = Unlock;
                txtPasswordCheck.Enabled = Unlock;
                txtPostCode.Enabled = Unlock;
                txtStaffUserName.Enabled = Unlock;
                txtTitle.Enabled = Unlock;
                txtPersonnelNo.Enabled = Unlock;
                txtWindowsUsername.Enabled = Unlock;
                txtDepart.Enabled = Unlock;
                txtJobDescription.Enabled = Unlock;
                txtEmail.Enabled = Unlock;
                rtfNotes.Enabled = Unlock;
                lstCustomerAccessIncluded.Enabled = Unlock;
                lstCustomerAccessNotIncluded.Enabled = Unlock;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void LoadStaffDetails(int Staff_ID)
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Load Staff details", LogManager.enumLogLevel.Info);
                List<StaffDetailsResult> lstStaff = UserAdministrationBiz.CreateInstance().GetStaffDetails(Staff_ID);
                if (lstStaff.Count > 0)
                {
                    StaffDetailsResult staffdet = lstStaff[0];
                    UserTableUserID = staffdet.UserTableID.Value;
                    txtDepart.Text = staffdet.Staff_Department;
                    txtJobDescription.Text = staffdet.Staff_Job_Title;
                    chkEngineer.Checked = Convert.ToBoolean(staffdet.Staff_IsAnEngineer);
                    chkRepresentative.Checked = Convert.ToBoolean(staffdet.Staff_IsARepresentative);
                    chkStockController.Checked = Convert.ToBoolean(staffdet.Staff_IsAStockController);
                    isUserTermiated = Convert.ToBoolean(staffdet.Staff_Terminated);
                    chkTerminate.Checked = isUserTermiated;
                    txtAddress.Text = staffdet.Staff_Address;
                    txtFirstName.Text = staffdet.Staff_First_Name;
                    txtSurname.Text = staffdet.Staff_Last_Name;
                    txtHomePhone.Text = staffdet.Staff_Phone_No;
                    txtMobilePhone.Text = staffdet.Staff_Mobile_No;
                    txtPostCode.Text = staffdet.Staff_Postcode;
                    txtTitle.Text = staffdet.Staff_Title;

                    List<UserGroupDetailsResult> lstUserGroup = (List<UserGroupDetailsResult>)cmbUserLevel.DataSource;
                    if (lstUserGroup != null)
                    {
                        int ind = lstUserGroup.FindIndex(se => se.User_Group_ID == staffdet.User_Group_ID);
                        cmbUserLevel.SelectedIndex = (ind >= 0) ? ind : 0;

                    }
                    txtStaffUserName.Text = staffdet.Staff_Username;
                    txtPersonnelNo.Text = staffdet.Staff_Personel_No;
                    txtExtensionNumber.Text = staffdet.Staff_Extension_No;
                    rtfNotes.Text = staffdet.Staff_Notes;
                    if (staffdet.Staff_Password == string.Empty)
                    {
                        txtPassword.Text = this.GetResourceTextByKey(1, "MSG_NO_CURRENT_PASSWORD");
                    }
                    else
                    {
                        txtPassword.Text = "1234";//(details.Fields("staff_Password"), details.Fields("Staff_Username"))
                        txtPasswordCheck.Text = "1234";//Decode(details.Fields("staff_Password"), details.Fields("Staff_Username"))
                    }

                    txtEmail.Text = staffdet.Email_Address;
                    List<OperatorEntity> lstOp = (List<OperatorEntity>)cmbSupplier.DataSource;
                    if (lstOp != null)
                    {
                        int ind = lstOp.FindIndex(se => se.Operator_ID == staffdet.Supplier_ID);
                        cmbSupplier.SelectedIndex = (ind >= 0) ? ind : 0;
                    }

                    List<DepotEntity> lstDepot = (List<DepotEntity>)cmbDepot.DataSource;
                    if (lstDepot != null)
                    {
                        int ind = lstDepot.FindIndex(se => se.Depot_ID == staffdet.Depot_ID);
                        cmbDepot.SelectedIndex = (ind >= 0) ? ind : 0;
                    }

                    List<ServiceAreasDetailsResult> lstService = (List<ServiceAreasDetailsResult>)cmdServiceArea.DataSource;
                    if (lstService != null)
                    {
                        int ind = lstService.FindIndex(se => se.Service_Area_ID == staffdet.Service_Area_ID);
                        cmdServiceArea.SelectedIndex = (ind >= 0) ? ind : 0;
                    }
                    LogManager.WriteLog(ScreenName + "Get Staff CustomerAccess details", LogManager.enumLogLevel.Info);
                    List<StaffCustomerAccessResult> lstStaffCust = UserAdministrationBiz.CreateInstance().GetStaffCustomerAccess(Staff_ID);
                    List<CustomerAccessDetailsResult> lstCustomerAccess = UserAdministrationBiz.CreateInstance().GetCustomerAccessDetails();
                    _collCustomerNotIncluded = new BindingList<CustomerAccessDetailsResult>();
                    _collCustomerIncluded = new BindingList<CustomerAccessDetailsResult>();

                    var customersIncluded = (from i in lstStaffCust
                                             join j in lstCustomerAccess
                                             on i.Customer_Access_ID equals j.Customer_Access_ID
                                             select j).DefaultIfEmpty();
                    foreach (var customerIncluded in customersIncluded)
                    {
                        if (customerIncluded != null) _collCustomerIncluded.Add(customerIncluded);
                    }

                    var customersNotIncluded = (from i in lstCustomerAccess
                                                join j in lstStaffCust
                                               on i.Customer_Access_ID equals j.Customer_Access_ID
                                               into matched
                                                from m in matched.DefaultIfEmpty()
                                                where m == null
                                                select i);
                    foreach (var customerNotIncluded in customersNotIncluded)
                    {
                        _collCustomerNotIncluded.Add(customerNotIncluded);
                    }

                    lstCustomerAccessNotIncluded.DataSource = _collCustomerNotIncluded;
                    lstCustomerAccessNotIncluded.DisplayMember = "Customer_Access_Name";
                    lstCustomerAccessNotIncluded.ValueMember = "Customer_Access_ID";

                    lstCustomerAccessIncluded.DataSource = _collCustomerIncluded;
                    lstCustomerAccessIncluded.DisplayMember = "Customer_Access_Name";
                    lstCustomerAccessIncluded.ValueMember = "Customer_Access_ID";

                    LogManager.WriteLog(ScreenName + "Get User details", LogManager.enumLogLevel.Info);
                    List<UserDetailsResult> lstUserDet = UserAdministrationBiz.CreateInstance().GetUserDetails(UserTableUserID);
                    if (lstUserDet.Count > 0)
                    {
                        UserDetailsResult userdet = lstUserDet[0];

                        txtWindowsUsername.Text = userdet.WindowsUserName;
                        if (userdet.isLocked)
                        {
                            btnUnLockUser.Visible = true;
                            btnUnLockUser.Enabled = !isUserTermiated && AppGlobals.Current.HasUserAccess("HQ_Admin_Users_Unlock");
                            btnLockUser.Visible = false;
                            islockedValue = 1;
                        }
                        else
                        {
                            btnLockUser.Visible = true;
                            btnUnLockUser.Visible = false;
                            islockedValue = 0;
                        }

                        LogManager.WriteLog(ScreenName + "Load User Languages details", LogManager.enumLogLevel.Info);
                        List<UserLanguagesDetailsResult> lstUserLang = (List<UserLanguagesDetailsResult>)cmbLanguage.DataSource;
                        if (lstUserLang != null)
                        {
                            int ind = lstUserLang.FindIndex(se => se.LanguageID == userdet.LanguageID);
                            cmbLanguage.SelectedIndex = (ind >= 0) ? ind : 0;

                            int indDate = lstUserLang.FindIndex(se => se.LanguageID == userdet.DateCulture);
                            cmbDate.SelectedIndex = (indDate >= 0) ? indDate : 0;

                            int indCurr = lstUserLang.FindIndex(se => se.LanguageID == userdet.CurrencyCulture);
                            cmbCurrency.SelectedIndex = (indCurr >= 0) ? indCurr : 0;

                        }
                    }
                    else
                    {
                        btnUnLockUser.Visible = false;
                        btnLockUser.Visible = false;
                    }
                }
                InitialValues();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        public void InitialValues()
        {
            try
            {
                oAudit.Staff_Department = txtDepart.Text;
                oAudit.Staff_Job_Title = txtJobDescription.Text;
                oAudit.Staff_IsAnEngineer = chkEngineer.Checked;
                oAudit.Staff_IsARepresentative = chkRepresentative.Checked;
                oAudit.Staff_IsAStockController = chkStockController.Checked;
                oAudit.Staff_Terminated = chkTerminate.Checked;
                oAudit.Staff_Address = txtAddress.Text;
                oAudit.Staff_First_Name = txtFirstName.Text;
                oAudit.Staff_Last_Name = txtSurname.Text;
                oAudit.Staff_Phone_No = txtHomePhone.Text;
                oAudit.Staff_Mobile_No = txtMobilePhone.Text;
                oAudit.Staff_Postcode = txtPostCode.Text;
                oAudit.Staff_Title = txtTitle.Text;
                oAudit.UserLevel = cmbUserLevel.Text;
                oAudit.Staff_Username = txtStaffUserName.Text;
                oAudit.Staff_Personnel_No = txtPersonnelNo.Text;
                oAudit.Staff_Extension_No = txtExtensionNumber.Text;
                oAudit.Staff_Notes = rtfNotes.Text;
                oAudit.Email_Address = txtEmail.Text;
                oAudit.Operator_Name = cmbSupplier.Text;
                oAudit.Depot_Name = cmbDepot.Text;
                oAudit.ServiceArea = cmdServiceArea.Text;
                oAudit.Currency_Format = ((cmbCurrency.SelectedItem) as UserLanguagesDetailsResult).LanguageName;
                oAudit.Date_Format = ((cmbDate.SelectedItem) as UserLanguagesDetailsResult).LanguageName;
                oAudit.User_Language = ((cmbLanguage.SelectedItem) as UserLanguagesDetailsResult).LanguageName;
                oAudit.Password = txtPassword.Text;
                oAudit.Confirm_Password = txtPasswordCheck.Text;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        public void ModifiedValues()
        {
            try
            {
                oNewEntity.Staff_Department = txtDepart.Text;
                oNewEntity.Staff_Job_Title = txtJobDescription.Text;
                oNewEntity.Staff_IsAnEngineer = chkEngineer.Checked;
                oNewEntity.Staff_IsARepresentative = chkRepresentative.Checked;
                oNewEntity.Staff_IsAStockController = chkStockController.Checked;
                oNewEntity.Staff_Terminated = chkTerminate.Checked;
                oNewEntity.Staff_Address = txtAddress.Text;
                oNewEntity.Staff_First_Name = txtFirstName.Text;
                oNewEntity.Staff_Last_Name = txtSurname.Text;
                oNewEntity.Staff_Phone_No = txtHomePhone.Text;
                oNewEntity.Staff_Mobile_No = txtMobilePhone.Text;
                oNewEntity.Staff_Postcode = txtPostCode.Text;
                oNewEntity.Staff_Title = txtTitle.Text;
                oNewEntity.UserLevel = cmbUserLevel.Text;
                oNewEntity.Staff_Username = txtStaffUserName.Text;
                oNewEntity.Staff_Personnel_No = txtPersonnelNo.Text;
                oNewEntity.Staff_Extension_No = txtExtensionNumber.Text;
                oNewEntity.Staff_Notes = rtfNotes.Text;
                oNewEntity.Email_Address = txtEmail.Text;
                oNewEntity.Operator_Name = cmbSupplier.Text;
                oNewEntity.Depot_Name = cmbDepot.Text;
                oNewEntity.ServiceArea = cmdServiceArea.Text;
                oNewEntity.Currency_Format = ((cmbCurrency.SelectedItem) as UserLanguagesDetailsResult).LanguageName;
                oNewEntity.Date_Format = ((cmbDate.SelectedItem) as UserLanguagesDetailsResult).LanguageName;
                oNewEntity.User_Language = ((cmbLanguage.SelectedItem) as UserLanguagesDetailsResult).LanguageName;
                oNewEntity.Password = txtPassword.Text;
                oNewEntity.Confirm_Password = txtPasswordCheck.Text;
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }

        }

        void LoadRepresentativeTree()
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Load Representative details", LogManager.enumLogLevel.Info);
                List<OperatorEntity> lstOpDept = UserAdministrationBiz.CreateInstance().GetOperatorandDepotDetails();
                int Op_ID = 0;
                treeOps.Nodes.Clear();
                TreeNode tOp = new TreeNode();
                foreach (OperatorEntity opt in lstOpDept)
                {
                    if (opt.Operator_ID != Op_ID)
                    {
                        tOp = treeOps.Nodes.Add(opt.Operator_ID.ToString(), opt.Operator_Name);
                        Op_ID = opt.Operator_ID;
                        foreach (DepotEntity dept in opt.Depots)
                        {
                            TreeNode tn_new = new TreeNode();
                            tn_new.Name = dept.Depot_ID.ToString();
                            tn_new.Tag = "DE";
                            tn_new.Text = dept.Depot_Name;
                            tOp.Nodes.Add(tn_new);
                        }
                    }
                }
                treeOps.ExpandAll();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        void SetRepresentativeTree(int Staff_ID)
        {
            try
            {

                UncheckAllNodes(treeOps.Nodes);
                List<StaffDepotResult> lstStaffDept = UserAdministrationBiz.CreateInstance().GetStaffDepot(Staff_ID);
                if (lstStaffDept != null && lstStaffDept.Count > 0)
                {

                    foreach (StaffDepotResult st in lstStaffDept)
                    {
                        TreeNode[] myNode;
                        myNode = treeOps.Nodes.Find(st.Depot_ID.ToString(), true);
                        foreach (TreeNode tn in myNode)
                        {
                            if (tn != null && tn.Parent != null)
                            {
                                tn.Checked = true;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        public void UncheckAllNodes(TreeNodeCollection nodes)
        {
            try
            {
                foreach (TreeNode node in nodes)
                {
                    node.Checked = false;
                    CheckChildren(node, false);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CheckChildren(TreeNode rootNode, bool isChecked)
        {
            try
            {
                foreach (TreeNode node in rootNode.Nodes)
                {
                    CheckChildren(node, isChecked);
                    node.Checked = isChecked;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        bool ValidateControls(ref string ErrorMsg)
        {
            string strAllowedChar = "[^_a-zA-Z0-9]";
            bool retVal = true;

            try
            {
                if (String.IsNullOrEmpty(txtSurname.Text))
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_UCUSERADMIN_SURNAME"); //"Please Enter A Surname";
                    txtSurname.Focus();
                    retVal = false;
                }
                else if (txtSurname.Text.Trim().StartsWith("0"))
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_UCUSERADMIN_ZEROUSERNAME"); //"The User Name cannot start with Zero";
                    txtSurname.Focus();
                    retVal = false;
                }
                else if ((new Regex(strAllowedChar).IsMatch(txtStaffUserName.Text)))
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_UCUSERADMIN_INVALIDUSERNAME"); //"The User Name You have Entered Is Not Valid" + System.Environment.NewLine + "Please Enter A Different One";
                    txtStaffUserName.Focus();
                    retVal = false;
                }
                else if (String.IsNullOrEmpty(txtFirstName.Text))
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_UCUSERADMIN_FIRSTNAME"); //"Please Enter A First Name";
                    txtFirstName.Focus();
                    retVal = false;
                }
                else if (String.IsNullOrEmpty(txtStaffUserName.Text))
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_UCUSERADMIN_INVALIDUSERNAME");// "The User Name You have Entered Is Not Valid" + System.Environment.NewLine + "Please Enter A Different One";
                    txtStaffUserName.Focus();
                    retVal = false;
                }
                else if (String.IsNullOrEmpty(txtWindowsUsername.Text))
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_UCUSERADMIN_INVALIDWINDOWUSERNAME");// "The Windows User Name You have Entered Is Not Valid" + System.Environment.NewLine + "Please Enter A Different One";
                    txtWindowsUsername.Focus();
                    retVal = false;
                }
                else if (String.IsNullOrEmpty(txtPassword.Text) && NewUser)
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_UCUSERADMIN_CHECKPASSWORD"); //"Please Enter And Check A Password";
                    txtPassword.Focus();
                    retVal = false;
                }
                else if (txtPassword.Text.Trim() != txtPasswordCheck.Text.Trim() && NewUser)
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_UCUSERADMIN_PASSWORDCHECK");// "The Password And The Password Check You have Entered Do Not Match" + System.Environment.NewLine + "Please Enter Them Both Again";
                    txtPasswordCheck.Text = "";
                    txtPassword.Focus();
                    retVal = false;
                }
                else if (Convert.ToInt32(cmbUserLevel.SelectedValue) <= 0)
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_UCUSERADMIN_INVALIDUSERLEVEL"); //"Please Enter And Check A Password";
                    //ErrorMsg = "Please select user level";
                    cmbUserLevel.Focus();
                    retVal = false;
                }
                else if (Convert.ToInt32(cmbLanguage.SelectedValue) <= 0)
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_UCUSERADMIN_INVALIDUSERLANG"); //"Please Enter And Check A Password";
                    //ErrorMsg = "Please select user level";
                    cmbLanguage.Focus();
                    retVal = false;
                }
                ////Length > 0
                //else if (!(TxtHomePhone.Text.IsNumeric() && TxtHomePhone.Text.IsLengthGreaterThanZero()) && NewUser)
                //{
                //    ErrorMsg = "Please Enter Valid HomePhone Number ";
                //    TxtHomePhone.Focus();
                //    retVal = false;
                //}
                //else if (!(TxtMobilePhone.Text.IsNumeric() && TxtMobilePhone.Text.IsLengthGreaterThanZero()) && NewUser)
                //{
                //    ErrorMsg = "Please Enter Valid MobilePhone Number ";
                //    TxtMobilePhone.Focus();
                //    retVal = false;
                //}
                //else if (!(TxtPersonnelNo.Text.IsNumeric() && TxtPersonnelNo.Text.IsLengthGreaterThanZero()) && NewUser)
                //{
                //    ErrorMsg = "Please Enter Valid Personnel Number ";
                //    TxtPersonnelNo.Focus();
                //    retVal = false;
                //}
                //else if (!(TxtExtensionNumber.Text.IsNumeric() && TxtExtensionNumber.Text.IsLengthGreaterThanZero()) && NewUser)
                //{
                //    ErrorMsg = "Please Enter Valid Extension Number ";
                //    TxtExtensionNumber.Focus();
                //    retVal = false;
                //}
                else if (txtEmail.Text.Trim() != "" && !txtEmail.Text.IsValidEmail())
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_ENT_UC_DETAILS_EMAIL");
                    txtEmail.Focus();
                    retVal = false;

                }
                else if (!BMC.Security.PasswordHelper.CheckPasswordStrength(txtPassword.Text) && NewUser)
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_UCUSERADMIN_COMBINATION");// "Password is weak. Please enter a password which is a combination of the following character groups [a-z],[A-Z],[0-9]";
                    txtPassword.Focus();
                    retVal = false;
                }
                else if (cmbSupplier.SelectedItem != null && (cmbSupplier.SelectedIndex > 0))//Item: CR #201352 ,Sub-Item: 3, Depot selection made compulsary if user selects operator.
                {
                    if (cmbDepot.SelectedItem == null || (cmbDepot.SelectedIndex == -1) || (cmbDepot.SelectedIndex == 0))
                    {
                        ErrorMsg = this.GetResourceTextByKey(1, "MSG_ENT_UC_DETAILS_DEPOT");// "Supplier is selected.Please select a Depot";
                        cmbDepot.Focus();
                        retVal = false;
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                ErrorMsg = ex.Message;
                retVal = false;
            }
            return retVal;

        }

        void UpdateStaffCustomerAccessDetails(int Staff_ID)
        {
            try
            {

                UserAdministrationBiz.CreateInstance().UpdateStaffCustomerAccess(Staff_ID, 0);
                foreach (CustomerAccessDetailsResult cust in lstCustomerAccessIncluded.Items)
                {
                    if (UserAdministrationBiz.CreateInstance().UpdateStaffCustomerAccess(Staff_ID, cust.Customer_Access_ID))
                    {
                        LogManager.WriteLog(ScreenName + "CustomerAccessDetails Updated Successfully Staff_ID:" + Staff_ID, LogManager.enumLogLevel.Info);
                    }
                    else
                    {
                        LogManager.WriteLog(ScreenName + "CustomerAccessDetails Updation Failed Staff_ID:" + Staff_ID, LogManager.enumLogLevel.Info);
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void CreateNewOrUpdateUser(ref int? _UserTableUserID)
        {
            try
            {
                UserAdministrationBiz.CreateInstance().UpdateUserDetails(txtWindowsUsername.Text, BMC.Security.CryptoHelper.CreateHash(txtPassword.Text), txtStaffUserName.Text,
                    (int)cmbLanguage.SelectedValue, (int)cmbCurrency.SelectedValue, (int)cmbDate.SelectedValue, ref _UserTableUserID);
            }
            catch (Exception ex)
            {
                _UserTableUserID = 0;
                LogManager.WriteLog(ScreenName + "Windows User Name Already Exists", LogManager.enumLogLevel.Info);

                ///ExceptionManager.Publish(ex);
            }

        }

        private void UpdateUserLockStatus()
        {
            List<UserLockStatusResult> objLockStatus = objUserAdminBiz.UserLockStatus
                ((int)cmbSurname.SelectedValue, islockedValue);
        }

        public bool AuditChanges(UserAuditEntity oldEntity, UserAuditEntity NewEntity, int userID, string userName, string User_Name)
        {
            try
            {
                string addOrModify = (isEdit == false) ? "Added" : "Modified";
                string AddOrModify = (isEdit == false) ? "ADD" : "MODIFY";

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    string sValue = string.Empty; string[] strValue; string sFieldValue = string.Empty; int iCount = 0;
                    StringBuilder oString = new StringBuilder();
                    foreach (PropertyInfo prop in NewEntity.GetType().GetProperties())
                    {
                        if (!Convert.ToString(prop.GetValue(NewEntity, null)).Equals(Convert.ToString(prop.GetValue(oldEntity, null))))
                        {
                            sValue = Convert.ToString(prop.GetValue(oldEntity, null)) + "~" + Convert.ToString(prop.GetValue(NewEntity, null));
                            oString.Append(prop.Name).Append(',');
                            iCount = oString.ToString().Split(',').Count();
                        }
                    }
                    if (iCount > 2 && addOrModify == "Modified")
                    {
                        DataContext.InsertAuditData(userID, userName, (int)ModuleNameEnterprise.AUDIT_USERS, ModuleNameEnterprise.AUDIT_USERS.ToString(), "User Administration", "",
                           oString.ToString(), "", "", "User [" + User_Name + "] Modified.. " + oString.ToString(), AddOrModify);

                    }
                    else if (iCount > 0 && addOrModify == "Added")
                    {
                        DataContext.InsertAuditData(userID, userName, (int)ModuleNameEnterprise.AUDIT_USERS, ModuleNameEnterprise.AUDIT_USERS.ToString(), "User Administration", "", oString.ToString(),
                          "", "", "User [ " + User_Name + " ] Added ..", AddOrModify);

                    }
                    else if (iCount != 0)
                    {
                        strValue = sValue.Split('~');
                        string sOldValue = strValue[0].ToString();
                        string sNewValue = strValue[1].ToString();

                        DataContext.InsertAuditData(userID, userName, (int)ModuleNameEnterprise.AUDIT_USERS, ModuleNameEnterprise.AUDIT_USERS.ToString(), "User Administration", "", oString.ToString(),
                          sOldValue, sNewValue, string.Format("User [{0}] Modified ..[{1}]:{2}-->{3}", User_Name, oString.ToString(), sOldValue, sNewValue), AddOrModify);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
            return true;
        }

        #endregion

        #region Events

        private void CmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (cmbSupplier.SelectedItem != null && (CmbSupplier_ind != cmbSupplier.SelectedIndex))
                {
                    OperatorEntity opRes = (OperatorEntity)cmbSupplier.SelectedItem;
                    LogManager.WriteLog(ScreenName + "Load Depot details", LogManager.enumLogLevel.Info);
                    List<DepotEntity> lstDepot = UserAdministrationBiz.CreateInstance().GetDepotDetails(opRes.Operator_ID);
                    //lstDepot.Insert(0, new DepotEntity { Depot_ID = -1, Depot_Name = this.GetResourceTextByKey("Key_Any") });
                    cmbDepot.DataSource = lstDepot;
                    cmbDepot.DisplayMember = "Depot_Name";
                    cmbDepot.ValueMember = "Depot_ID";
                    CmbSupplier_ind = cmbSupplier.SelectedIndex;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CmbDepot_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbDepot.SelectedItem != null && (CmbDepot_ind != cmbDepot.SelectedIndex))
                {
                    DepotEntity DepRes = (DepotEntity)cmbDepot.SelectedItem;
                    LogManager.WriteLog(ScreenName + "Load Service Area details", LogManager.enumLogLevel.Info);
                    List<ServiceAreasDetailsResult> lstService = UserAdministrationBiz.CreateInstance().GetServiceAreasDetails((int)DepRes.Depot_ID);
                    // lstService.Insert(0, new ServiceAreasDetailsResult { Service_Area_ID = -1, Service_Area_Name = this.GetResourceTextByKey("Key_Any") });
                    cmdServiceArea.DataSource = lstService;
                    cmdServiceArea.DisplayMember = "Service_Area_Name";
                    cmdServiceArea.ValueMember = "Service_Area_ID";
                    CmbDepot_ind = cmbDepot.SelectedIndex;

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                isEdit = true;
                if (BtnEdit.Text.Equals(this.GetResourceTextByKey("Key_EditCaption")))                                           // "Edit"))
                {
                    UnLockControls(true);
                    //if (AppGlobals.Current.UserId == UserTableUserID)
                    //{
                    //    btnResetPassword.Enabled = false;
                    //    txtPassword.Enabled = false;
                    //    txtPasswordCheck.Enabled = false;
                    //}
                    //else
                    //{
                    //    btnResetPassword.Enabled = !isUserTermiated;
                    //    txtPassword.Enabled = false;
                    //    txtPasswordCheck.Enabled = false;

                    //}
                    txtStaffUserName.Enabled = false;

                    btnUpdate.Visible = true;
                    BtnEdit.Visible = false;
                    btnNew.Visible = false;
                    //BtnForeColour.Visible = true;
                    //BtnBackColour.Visible = true;
                    ///BtnEdit.Text = "Cancel";
                    btnCancel.Visible = true;
                  //  btnAssignEmpCard.Visible = SettingsEntity.IsEmployeeCardTrackingEnabled ? true : false;

                    btnAssignSite.Enabled = !isUserTermiated;
                    cmbSurname.Visible = false;
                    txtSurname.Visible = true;
                    txtFirstName.Visible = true;
                    cmbLanguage.Enabled = true;
                    cmbCurrency.Enabled = true;
                    cmbDate.Enabled = true;
                    cmbSupplier.Enabled = true;
                    cmbDepot.Enabled = true;
                    cmdServiceArea.Enabled = true;
                    chkRepresentative.Enabled = true;
                    chkStockController.Enabled = true;
                    cmbUserLevel.Enabled = true;
                    //btnAssignEmpCard.Enabled = SettingsEntity.IsEmployeeCardTrackingEnabled && !isUserTermiated ? true : false;
                    btnLockUser.Enabled = (txtStaffUserName.Text == AppEntryPoint.Current.UserName || cmbUserLevel.Text.ToUpper() == "SUPER USER" || isUserTermiated) ? false : true;
                    //btnUnLockUser.Enabled = !isUserTermiated;
                }
                else
                {
                    BtnEdit.Text = this.GetResourceTextByKey("Key_EditCaption");                //  "Edit";
                }


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {

                Update = false;
                int Surname_ind = cmbSurname.SelectedIndex;

                if (AppGlobals.Current.UserId == UserTableUserID)
                    BtnEdit.Visible = AppGlobals.Current.HasUserAccess("HQ_Admin_Users_Edit");
                else
                    BtnEdit.Visible = AppGlobals.Current.HasUserAccess("HQ_Admin_Users_Editall");

                btnNew.Visible = AppGlobals.Current.HasUserAccess("HQ_Admin_Users_Editall");
                btnNew.Enabled = AppGlobals.Current.HasUserAccess("HQ_Admin_Users_Editall");

                Form_Load();
                LoadStaffDetails((cmbSurname.SelectedItem as StaffNameResult).Staff_ID);
                cmbSurname.SelectedIndex = Surname_ind;
                //btnResetPassword.Enabled = false;
                btnAssignSite.Enabled = false;

                cmbSurname.Visible = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            string ErrorMsg = "";
            string sUserName = "";
            try
            {
                LogManager.WriteLog(ScreenName + "BtnUpdate_Click", LogManager.enumLogLevel.Debug);
                if (ValidateControls(ref ErrorMsg))
                {
                    if (isUserTermiated != chkTerminate.Checked && chkTerminate.Checked && Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1, "MSG_UCUSERADMIN_TERMINATEUSER"), this.ParentForm.Text) == DialogResult.No)
                    {
                        chkTerminate.Focus();
                        return;
                    }
                    int? TempUserTableUserID = UserTableUserID;
                    int? StaffID = 0;
                    if (NewUser)
                    {
                        TempUserTableUserID = 0;

                        CreateNewOrUpdateUser(ref TempUserTableUserID);
                        if (TempUserTableUserID.Value == 0)
                        {
                            //Win32Extensions.ShowInfoMessageBox("The Windows User Name You have Entered Is Not Valid \n Please Enter A Different One");
                            Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_UCUSERADMIN_INVALIDWINDOWUSERNAME") + Environment.NewLine + this.GetResourceTextByKey(1, "MSG_UCUSERADMIN_SELECTONE"), this.ParentForm.Text);
                            //txtStaffUserName.Text = "";
                            txtWindowsUsername.Text = "";
                            return;
                        }

                        UserTableUserID = TempUserTableUserID.Value;
                        LogManager.WriteLog(ScreenName + "New User Created successfully UserID:" + UserTableUserID, LogManager.enumLogLevel.Info);

                        btnNew.Enabled = true;

                    }
                    else
                    {
                        StaffID = (int)cmbSurname.SelectedValue;
                        if (UserTableUserID != 0)
                        {
                            CreateNewOrUpdateUser(ref TempUserTableUserID);
                            if (TempUserTableUserID.Value == 0)
                            {
                                //  Win32Extensions.ShowInfoMessageBox("The Windows User Name You have Entered Is Not Valid \n Please Enter A Different One");
                                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_UCUSERADMIN_INVALIDWINDOWUSERNAME") + Environment.NewLine + this.GetResourceTextByKey(1, "MSG_UCUSERADMIN_SELECTONE"), this.ParentForm.Text);
                                txtWindowsUsername.Text = "";
                                return;
                            }
                            //Win32Extensions.ShowInfoMessageBox("User Details Updated Successfully");
                            UserTableUserID = TempUserTableUserID.Value;
                            LogManager.WriteLog(ScreenName + "User Updated successfully UserID:" + UserTableUserID, LogManager.enumLogLevel.Info);
                        }

                    }
                    string StrPassword = BMC.Security.CryptoHelper.CreateHash(txtPassword.Text);
                    string dtEndtime = chkTerminate.Checked ? Common.Utilities.Common.GetUniversalDate(DateTime.Now) : null;

                    if (UserAdministrationBiz.CreateInstance().AddModifyStaffDetails((int)cmbUserLevel.SelectedValue, txtFirstName.Text, txtSurname.Text, txtTitle.Text, txtAddress.Text,
                        txtPostCode.Text, txtHomePhone.Text, txtExtensionNumber.Text, txtMobilePhone.Text, txtJobDescription.Text,
                        txtDepart.Text, chkEngineer.Checked, chkRepresentative.Checked, chkStockController.Checked,
                        Common.Utilities.Common.GetUniversalDate(DateTime.Now), dtEndtime, txtStaffUserName.Text, StrPassword, (int)cmbDepot.SelectedValue,
                        (int)cmdServiceArea.SelectedValue, (int)cmbSupplier.SelectedValue, txtPersonnelNo.Text, chkTerminate.Checked, rtfNotes.Text,
                        txtEmail.Text, UserTableUserID, ref StaffID))
                    {
                        sUserName = txtStaffUserName.Text;
                        LogManager.WriteLog(ScreenName + "Staff created successfully Staff:" + StaffID.Value, LogManager.enumLogLevel.Info);
                        UserAdministrationBiz.CreateInstance().UpdateStaffDepot(StaffID.Value, 0);
                        List<string> lst_chNode = GetCheckedNode(treeOps.Nodes);
                        foreach (string strDepotID in lst_chNode)
                        {
                            if (UserAdministrationBiz.CreateInstance().UpdateStaffDepot(StaffID.Value, Convert.ToInt32(strDepotID)))
                            {
                                LogManager.WriteLog(ScreenName + "StaffDepot updated successfully; StaffID:" + StaffID.Value, LogManager.enumLogLevel.Info);
                            }
                            else
                            {
                                LogManager.WriteLog(ScreenName + "Unable to update StaffDepot; StaffID:" + StaffID.Value, LogManager.enumLogLevel.Error);
                            }
                        }

                        btnUpdate.Visible = false;
                        btnNew.Visible = false;

                        if (cmbUserLevel.SelectedItem != null)
                        {
                            UserGroupDetailsResult UserDet = cmbUserLevel.SelectedItem as UserGroupDetailsResult;
                            if (UserAdministrationBiz.CreateInstance().UpdateRoleAccess(UserTableUserID, UserDet.User_Group_Name))
                            {
                                LogManager.WriteLog(ScreenName + "RoleAccess updated successfully; StaffID:" + StaffID.Value, LogManager.enumLogLevel.Info);
                            }
                            else
                            {
                                LogManager.WriteLog(ScreenName + "Unable to update RoleAccess; StaffID:" + StaffID.Value, LogManager.enumLogLevel.Error);
                            }
                        }
                        UpdateStaffCustomerAccessDetails(StaffID.Value);
                        //Win32Extensions.ShowInfoMessageBox("User Details Inserted Successfully");
                        UnLockControls(false);
                        ModifiedValues();
                        AuditChanges(oAudit, oNewEntity, AppGlobals.Current.UserId, AppGlobals.Current.UserName, sUserName);
                        txtSurname.Visible = false;
                        txtFirstName.Visible = false;

                        cmbSurname.Visible = true;

                        cmbDepot.Enabled = false;
                        cmbSupplier.Enabled = false;
                        cmdServiceArea.Enabled = false;
                        cmbLanguage.Enabled = false;
                        cmbCurrency.Enabled = false;
                        cmbDate.Enabled = false;
                        cmbUserLevel.Enabled = false;

                        Update = false;
                        NewUser = false;
                        btnUpdate.Visible = false;
                        if (AppGlobals.Current.UserId == UserTableUserID)
                        {
                            BtnEdit.Visible = AppGlobals.Current.HasUserAccess("HQ_Admin_Users_Edit");
                        }
                        else
                        {
                            BtnEdit.Visible = AppGlobals.Current.HasUserAccess("HQ_Admin_Users_Editall");
                        }
                        btnNew.Visible = AppGlobals.Current.HasUserAccess("HQ_Admin_Users_Editall");
                        cmbSurname.Enabled = AppGlobals.Current.HasUserAccess("HQ_Admin_Users_ViewAll");
                        btnCancel.Visible = false;
                        btnAssignSite.Enabled = false;
                        btnLockUser.Enabled = false;
                        //btnUnLockUser.Enabled = false;
                       // btnAssignEmpCard.Enabled = false;
                        //btnResetPassword.Enabled = false;
                        LoadStaffName(StaffID.Value);
                        LoadStaffDetails(StaffID.Value);
                    }
                    else
                    {
                        LogManager.WriteLog(ScreenName + "Unable to update staff details", LogManager.enumLogLevel.Info);
                    }
                }
                else
                {
                    Win32Extensions.ShowInfoMessageBox(this, ErrorMsg, this.ParentForm.Text);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        List<String> GetCheckedNode(TreeNodeCollection theNodes)
        {
            List<String> aResult = new List<String>();

            if (theNodes != null)
            {
                foreach (TreeNode aNode in theNodes)
                {
                    if (aNode.Parent != null && aNode.Tag.ToString() == "DE" && aNode.Checked)
                    {
                        aResult.Add(aNode.Name);
                    }

                    aResult.AddRange(GetCheckedNode(aNode.Nodes));
                }
            }

            return aResult;
        }

        private void btnAssignSite_Click(object sender, EventArgs e)
        {
            try
            {
                if (UserTableUserID == 0)
                    //  Win32Extensions.ShowInfoMessageBox("Edit the user details, Update and click Edit button again to assign sites to user.");
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_UCUSERADMIN_EDITUSERDETAILS"), this.ParentForm.Text);
                else
                {
                    if (File.Exists(Application.StartupPath + @"\BMCUserSiteAdmin.exe"))
                    {

                        BMC.SecurityVB.BMCSecurityCallMethod BMCSecurityMethod = new BMCSecurityCallMethod();
                        //ProcessStartInfo psi = new ProcessStartInfo(Application.StartupPath + @"\BMCUserSiteAdmin.exe",
                        //     BMCSecurityMethod.Encrypt(UserTableUserID.ToString()) + " " +
                        //    BMCSecurityMethod.Encrypt(_UserName.Replace(" ", "")) + " " + BMCSecurityMethod.Encrypt(_UserID.ToString()));
                        //Process ps = new Process();
                        //ps.StartInfo = psi;
                        //ps.Start();
                        //// ps.WaitForExit(2000);

                        AppEntryPoint.Current.StartProcess(sender, null, Application.StartupPath + @"\BMCUserSiteAdmin.exe", BMCSecurityMethod.Encrypt(UserTableUserID.ToString()) + " " +
                            BMCSecurityMethod.Encrypt(_UserName.Replace(" ", "")) + " " + BMCSecurityMethod.Encrypt(_UserID.ToString()), true);
                    }
                    else
                        //  Win32Extensions.ShowInfoMessageBox("The User Site Admin package is not currently installed.");
                        Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_UCUSERADMIN_USERADMINSITE"), this.ParentForm.Text);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                string sResult = string.Empty;
                string sEncPass = string.Empty;
                BMC.SecurityVB.BMCSecurityCallMethod BMCSecurityMethod = new BMCSecurityCallMethod();

                string UserID = UserTableUserID.ToString();
                //if (BMC.Security.SecurityHelper.CurrentUser.UserName == "admin")
                //{
                //    Win32Extensions.ShowInfoMessageBox("Access Denied");
                //    return;
                //}

                if (UserTableUserID == 0)
                {
                    // Win32Extensions.ShowInfoMessageBox("Edit the user details, Update and click Edit button again to reset password.");
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_UCUSERADMIN_EDITEUSERDETAILS"), this.ParentForm.Text);
                    return;
                }
                else
                {
                    // if (Win32Extensions.ShowQuestionMessageBox("Do you wish to reset the password?") == DialogResult.Yes)
                    if (Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1, "MSG_UCUSERADMIN_RESETPASSWORD"), this.ParentForm.Text) == DialogResult.Yes)
                    {
                        sResult = BMCSecurityMethod.GeneratePassword();
                        if (sResult != "Unable to Reset")
                        {
                            sResult = sResult.Replace("New Password:", "");
                            // Win32Extensions.ShowInfoMessageBox("New Password is:" + "  " + sResult);
                            Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_UCUSERADMIN_NEWPASSWORD") + sResult, this.ParentForm.Text);
                            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                            {
                                DataContext.InsertAuditData(AppGlobals.Current.UserId, AppGlobals.Current.UserName, (int)ModuleNameEnterprise.AUDIT_USERS, ModuleNameEnterprise.AUDIT_USERS.ToString(), "User Administration", "", "Password_Reset",
                                   "", "Password Reset", "User [ " + txtStaffUserName.Text + " ] Password Reset Successful ..", "ADD");
                            }
                            txtPassword.Text = sResult;
                            txtPasswordCheck.Text = sResult;
                            sEncPass = CryptoHelper.CreateHash(txtPassword.Text);
                            if (BMCSecurityMethod.InsertPassword(ref UserID, ref sEncPass))
                                // Win32Extensions.ShowInfoMessageBox("Password updated successfully");
                                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_UCUSERADMIN_PASSWORDSUCCESS"), this.ParentForm.Text);
                            else
                                // Win32Extensions.ShowInfoMessageBox("Password not updated");
                                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_UCUSERADMIN_NOTUPDATED"), this.ParentForm.Text);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {

            LogManager.WriteLog(ScreenName + "btnNew_Click", LogManager.enumLogLevel.Debug);
            try
            {
                NewUser = true;

                BtnEdit.Visible = false;
                btnNew.Enabled = false;
                btnUpdate.Visible = true;
                btnCancel.Visible = true;
                txtSurname.Text = "";
                txtFirstName.Text = "";
                txtEmail.Text = "";
                txtSurname.Visible = true;
                txtFirstName.Visible = true;
                cmbSurname.Visible = false;

                UnLockControls(true);

                ClearText();

                //Reset Combobox/Listview values.
                cmbUserLevel.SelectedIndex = 0;
                cmbSupplier.SelectedIndex = 0;
                cmbDepot.SelectedIndex = 0;
                cmdServiceArea.SelectedIndex = 0;
                cmbLanguage.SelectedIndex = 0;
                LoadCustomerAccessLists();

                //btnResetPassword.Enabled = false;
                btnAssignSite.Enabled = false;
                chkRepresentative.Enabled = true;
                chkStockController.Enabled = true;
                chkStockController.Checked = false;
                chkRepresentative.Checked = false;
                chkTerminate.Checked = false;
                chkTerminate.Enabled = false;
                cmbUserLevel.Enabled = true;
                cmbSupplier.Enabled = true;
                cmbDepot.Enabled = true;
                cmdServiceArea.Enabled = true;
                cmbLanguage.Enabled = true;
                cmbCurrency.Enabled = true;
                cmbDate.Enabled = true;
                SetCultureIndex();
                LoadRepresentativeTree();
                InitialValues();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SetCultureIndex()
        {
            switch (ExtensionMethods.CurrentSiteCulture)
            {
                case "en-US":
                    cmbLanguage.SelectedIndex = 1;
                    cmbCurrency.SelectedIndex = 1;
                    cmbDate.SelectedIndex = 1;
                    break;
                case "en-GB":
                    cmbLanguage.SelectedIndex =2;
                    cmbCurrency.SelectedIndex = 2;
                    cmbDate.SelectedIndex = 2;
                    break;
                case "it-IT":
                    cmbLanguage.SelectedIndex = 3;
                    cmbCurrency.SelectedIndex = 3;
                    cmbDate.SelectedIndex = 3;
                    break;
                default:
                    cmbLanguage.SelectedIndex = 4;
                    cmbCurrency.SelectedIndex = 4;
                    cmbDate.SelectedIndex = 4;
                    break;
            }
        }

        private void ClearText()
        {

            txtAddress.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtExtensionNumber.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtHomePhone.Text = string.Empty;
            txtMobilePhone.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtPasswordCheck.Text = string.Empty;
            txtPostCode.Text = string.Empty;
            txtStaffUserName.Text = string.Empty;
            txtTitle.Text = string.Empty;
            txtDepart.Text = string.Empty;
            txtJobDescription.Text = string.Empty;
            txtWindowsUsername.Text = string.Empty;

            chkEngineer.Checked = false;
            txtPersonnelNo.Text = string.Empty;

            rtfNotes.Text = string.Empty;

            if (cmbSurname.Visible)
                cmbSurname.Focus();
            else
                txtSurname.Focus();

        }

        private void btnAssignEmpCard_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog(ScreenName + "btnAssignEmpCard_Click", LogManager.enumLogLevel.Debug);
                frmAddEmployeeCard oEmp = new frmAddEmployeeCard();
                oEmp.StartPosition = FormStartPosition.CenterScreen;
                oEmp.ShowDialog();               
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void ucUserAdmin_Load(object sender, System.EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            this.Size = this.Parent.Size;
            LogManager.WriteLog(ScreenName + "ucUserAdmin_Load", LogManager.enumLogLevel.Debug);
            Form_Load();
        }

        private void btnUnLockUser_Click(object sender, EventArgs e)
        {
            try
            {
                islockedValue = 0;
                LogManager.WriteLog(ScreenName + "btnUnLockUser_Click", LogManager.enumLogLevel.Debug);
                DialogResult dr = Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1, "MSG_USUSER_UNLOCKUSER"), this.ParentForm.Text);
                if (dr.ToString() == "Yes")
                {
                    UpdateUserLockStatus();
                    btnUnLockUser.Visible = false;
                    btnLockUser.Visible = true;
                    using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                    {
                        DataContext.InsertAuditData(AppGlobals.Current.UserId, AppGlobals.Current.UserName, (int)ModuleNameEnterprise.AUDIT_USERS, ModuleNameEnterprise.AUDIT_USERS.ToString(), "User Administration", "", "User_UnLocked",
                           "", "UnLocked", "User [ " + txtStaffUserName.Text + " ] UnLocked ..", "ADD");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnLockUser_Click(object sender, EventArgs e)
        {
            try
            {
                islockedValue = 1;
                LogManager.WriteLog(ScreenName + "btnLockUser_Click", LogManager.enumLogLevel.Debug);
                DialogResult dr = Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1, "MSG_USUSER_LOCKUSER"), this.ParentForm.Text);
                if (dr.ToString() == "Yes")
                {
                    UpdateUserLockStatus();
                    //Item: CR #201352 ,Sub-Item: 1
                    //Wrong message was displayed while doing Lock user functionality.
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_UCUSERADMIN_LOCKSUCCESS"), this.ParentForm.Text);
                    btnLockUser.Visible = false;
                    btnUnLockUser.Visible = true;
                    btnUnLockUser.Enabled = true && AppGlobals.Current.HasUserAccess("HQ_Admin_Users_Unlock");
                    using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                    {
                        DataContext.InsertAuditData(AppGlobals.Current.UserId, AppGlobals.Current.UserName, (int)ModuleNameEnterprise.AUDIT_USERS, ModuleNameEnterprise.AUDIT_USERS.ToString(), "User Administration", "", "User_Locked",
                           "", "Locked", "User [ " + txtStaffUserName.Text + " ] Locked ..", "ADD");
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void LstSurname_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                if ((!Update) && LstSurname_ind != cmbSurname.SelectedIndex)
                {

                    if (cmbSurname.SelectedIndex != -1)
                    {
                        StaffNameResult staff = (StaffNameResult)cmbSurname.SelectedItem;

                        LstSurname_ind = cmbSurname.SelectedIndex;
                        LoadStaffDetails(staff.Staff_ID);
                        SetRepresentativeTree(staff.Staff_ID);


                        if (AppGlobals.Current.UserId == UserTableUserID)
                            BtnEdit.Visible = AppGlobals.Current.HasUserAccess("HQ_Admin_Users_Edit");
                        else
                            BtnEdit.Visible = AppGlobals.Current.HasUserAccess("HQ_Admin_Users_Editall");

                        //if (AppGlobals.Current.UserName == TxtStaffUserName.Text)
                        //{
                        //    btnLockUser.Visible = false;
                        //    btnUnLockUser.Visible = false;
                        //}

                            if (AppGlobals.Current.UserId == UserTableUserID)
                        {
                            btnResetPassword.Enabled = false;
                            txtPassword.Enabled = false;
                            txtPasswordCheck.Enabled = false;
                        }
                        else
                        {
                            btnResetPassword.Enabled = !isUserTermiated && AppGlobals.Current.HasUserAccess("HQ_Admin_Users_ResetPassword");;
                            txtPassword.Enabled = false;
                            txtPasswordCheck.Enabled = false;

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void ChkRepresentative_CheckedChanged(object sender, EventArgs e)
        {
            treeOps.Visible = (chkRepresentative.Checked) ? true : false;
        }

        private void lstCustomerAccessIncluded_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (lstCustomerAccessIncluded.SelectedItem != null)
                {
                    CustomerAccessDetailsResult selected = lstCustomerAccessIncluded.SelectedItem as CustomerAccessDetailsResult;
                    _collCustomerIncluded.Remove(selected);
                    _collCustomerNotIncluded.Add(selected);

                    lstCustomerAccessNotIncluded.DataSource = _collCustomerNotIncluded;
                    lstCustomerAccessNotIncluded.DisplayMember = "Customer_Access_Name";
                    lstCustomerAccessNotIncluded.ValueMember = "Customer_Access_ID";

                    lstCustomerAccessIncluded.DataSource = _collCustomerIncluded;
                    lstCustomerAccessIncluded.DisplayMember = "Customer_Access_Name";
                    lstCustomerAccessIncluded.ValueMember = "Customer_Access_ID";
                    oNewEntity.CustomerAccessNotIncluded = _collCustomerNotIncluded[0].Customer_Access_Name;

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void lstCustomerAccessNotIncluded_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (lstCustomerAccessNotIncluded.SelectedItem != null)
                {
                    CustomerAccessDetailsResult selected = lstCustomerAccessNotIncluded.SelectedItem as CustomerAccessDetailsResult;
                    _collCustomerNotIncluded.Remove(selected);
                    _collCustomerIncluded.Add(selected);

                    lstCustomerAccessIncluded.DataSource = _collCustomerIncluded;
                    lstCustomerAccessIncluded.DisplayMember = "Customer_Access_Name";
                    lstCustomerAccessIncluded.ValueMember = "Customer_Access_ID";

                    lstCustomerAccessNotIncluded.DataSource = _collCustomerNotIncluded;
                    lstCustomerAccessNotIncluded.DisplayMember = "Customer_Access_Name";
                    lstCustomerAccessNotIncluded.ValueMember = "Customer_Access_ID";
                    oNewEntity.CustomerAccessIncluded = _collCustomerIncluded[0].Customer_Access_Name;

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void TxtPasswordCheck_Leave(object sender, EventArgs e)
        {
            try
            {
                string ErrorMsg = "";
                if (String.IsNullOrEmpty(txtPassword.Text) && NewUser)
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_USUSER_PASSWORD");// "Please Enter And Check A Password";


                }
                else if (txtPassword.Text.Trim().Length < 5 && NewUser)
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_USUSER_PASSWORD5CHAR");// "Please enter a password text with minimum 5 characters.";


                }
                else if (txtPassword.Text.Trim() != txtPasswordCheck.Text.Trim() && NewUser)
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_UCUSERADMIN_PASSWORDCHECK") + System.Environment.NewLine + this.GetResourceTextByKey(1, " MSG_UCUSERADMIN_BOTH"); //"The Password And The Password Check You have Entered Do Not Match" + System.Environment.NewLine + "Please Enter Them Both Again";
                    txtPasswordCheck.Text = "";

                }
                else if (!BMC.Security.PasswordHelper.CheckPasswordStrength(txtPassword.Text) && NewUser)
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_USERADMIN_COMBINATION"); //"Password is weak. Please enter a password which is a combination of the following character groups [a-z],[A-Z],[0-9]";

                }
                if (!String.IsNullOrEmpty(ErrorMsg))
                {
                    Win32Extensions.ShowInfoMessageBox(this, ErrorMsg, this.ParentForm.Text);
                    txtPasswordCheck.Leave -= TxtPasswordCheck_Leave;
                    txtPassword.Focus();
                    txtPasswordCheck.Leave += TxtPasswordCheck_Leave;
                }


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void TxtExtensionNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtStaffUserName_Leave(object sender, EventArgs e)
        {

            try
            {
                int? StaffID = NewUser ? 0 : (int)cmbSurname.SelectedValue;
                if (UserAdministrationBiz.CreateInstance().CheckUserNameAlreadyExists(txtStaffUserName.Text, Convert.ToInt32(StaffID)) > 0)
                {
                    //Item: CR #201352 ,Sub-Item: 4
                    //Incorrect message was displayed while adding/updating duplicate username.
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_USUSER_EXIST"), this.ParentForm.Text);
                    txtStaffUserName.Text = string.Empty;
                    txtStaffUserName.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void chkTerminate_CheckedChanged(object sender, EventArgs e)
        {
            if (btnUpdate.Visible)
                if (!isUserTermiated)
                {
                    btnResetPassword.Enabled = !(chkTerminate.Checked && !isUserTermiated);
                    btnAssignSite.Enabled = !(chkTerminate.Checked && !isUserTermiated);
                  //  btnAssignEmpCard.Enabled = !(chkTerminate.Checked && SettingsEntity.IsEmployeeCardTrackingEnabled && !isUserTermiated ? true : false);
                    btnLockUser.Enabled = (txtStaffUserName.Text == AppEntryPoint.Current.UserName || cmbUserLevel.Text.ToUpper() == "SUPER USER" || (chkTerminate.Checked && !isUserTermiated)) ? false : true;
                    btnUnLockUser.Enabled = !(chkTerminate.Checked && !isUserTermiated) && AppGlobals.Current.HasUserAccess("HQ_Admin_Users_Unlock");
                }
        }

        #endregion

       
    }
}
