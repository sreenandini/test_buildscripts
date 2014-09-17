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
using BMC.Common;
namespace BMC.EnterpriseClient.Views
{
    public partial class ucUsers : UserControl
    {
        #region ToPass
        private int _UserID = 1;
        public const string Branding = "Bally MultiConnect - Enterprise";
        private int GlobalUserTableId = 1;

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
        private bool Update = false;
        private bool NewUser = false;
        private bool AtLoad;
        private string ProjectTitle;
        private byte islockedValue;
        #endregion

        #region Constructors
        public ucUsers()
        {
            InitializeComponent();
            // Set Tags for controls
            SetTagProperty();
        }
         private void SetTagProperty()
        {
            try
            {

                this.lblAddress.Tag = "Key_AddressColon";
                this.btnAssignEmpCard.Tag = "Key_AssignEmpCards";
                this.btnAssignSite.Tag = "Key_AssignSites";
                this.btnCancel.Tag = "Key_Cancel";
                this.lblCurrencyFormat.Tag = "Key_CurrencyFormatColon";
                this.lblCurrentPassword.Tag = "Key_CurrentPasswordColon";
                this.gpCustomerAccessSecurityGroups.Tag = "Key_CustomerAccessSecurity";
                this.lblDateFormat.Tag = "Key_DateFormatColon";
                this.lblDepartment.Tag = "Key_DepartmentColon";
                this.lblDepot.Tag = "Key_DepotColon";
                this.BtnEdit.Tag = "Key_Edit";
                this.lblEMail.Tag = "Key_EMailColon";
                this.lblExt.Tag = "Key_ExtColon";
                this.lblFirstName.Tag = "Key_FirstNameColon";
                this.lblIncluded.Tag = "Key_IncludedColon";
                this.ChkEngineer.Tag = "Key_IsEngineer";
                this.ChkRepresentative.Tag = "Key_IsRepresentative";
                this.ChkStockController.Tag = "Key_IsStockController";
                this.lblJobTitleDescription.Tag = "Key_JobTitleDescriptionColon";
                this.btnLockUser.Tag = "Key_LockUser";
                this.lblMobile.Tag = "Key_MobileColon";
                this.btnNew.Tag = "Key_NewUser";
                this.lblNotIncluded.Tag = "Key_NotIncludedColon";
                this.lblNotes.Tag = "Key_NotesColon";
                this.lblPasswordCheck.Tag = "Key_PasswordCheckColon";
                this.lblPersonnelNo.Tag = "Key_PersonnelNoDot";
                this.lblPostCode.Tag = "Key_PostcodeColon";
                this.btnResetPassword.Tag = "Key_ResetPassword";
                this.lblServiceArea.Tag = "Key_ServiceAreaColon";
                this.lblSupplier.Tag = "Key_SupplierColon";
                this.lblSurname.Tag = "Key_SurnameColon";
                this.lblTelephone.Tag = "Key_TelephoneColon";
                this.ChkTerminate.Tag = "Key_TerminateUser";
                this.lblTitle.Tag = "Key_TitleColon";
                this.btnUnLockUser.Tag = "Key_UnLockUser";
                this.gp_userdetails.Tag = "Key_UserDetails";
                this.lblUserLanguage.Tag = "Key_UserLanguageColon";
                this.lblUserLevel.Tag = "Key_UserLevelColon";
                this.lblUserName.Tag = "Key_UserNameColon";
                this.lblWindowsUserName.Tag = "Key_WindowsUserNameColon";
            }
                
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        public ucUsers(string UserName, int UserId)
        {
            InitializeComponent();
            this._UserName = UserName;
            this._UserID = UserId;
            objUserAdminBiz = UserAdministrationBiz.CreateInstance();
            // Set Tags for controls
            SetTagProperty();
        }
        #endregion

        #region User Defined Functions

  

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
                LoadStaffName(null);

                LockControls();

                Update = false;
                NewUser = false;


                BtnUpdate.Visible = false;
                btnAssignSite.Enabled = false;
                btnCancel.Visible = false;
                btnResetPassword.Enabled = false;
                if (GlobalUserTableId == UserTableUserID)
                    BtnEdit.Visible = AppGlobals.Current.HasUserAccess("HQ_Admin_Users_Edit");
                else
                    BtnEdit.Visible = AppGlobals.Current.HasUserAccess("HQ_Admin_Users_Editall");

                btnNew.Visible = AppGlobals.Current.HasUserAccess("HQ_Admin_Users_Editall");
                CmbSupplier.Enabled = false;
                CmbDepot.Enabled = false;
                CmdServiceArea.Enabled = false;
                cmbLanguage.Enabled = false;
                cmbCurrency.Enabled = false;
                cmbDate.Enabled = false;
                LstUserLevel.Enabled = false;
                btnAssignEmpCard.Visible = false;
                btnLockUser.Visible = false;
                btnUnLockUser.Visible = false;
                if (LstSurname.Items.Count > 0)
                {
                    LstSurname_SelectedIndexChanged_1(LstSurname, null);
                }
                else
                {
                    // MsgBox("Please Create A New User", , Branding);
                    //BtnExpand.Visible = false;
                }
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
                List<OperatorEntity> lstOp = UserAdministrationBiz.CreateInstance().GetOperatorDetails();
                CmbSupplier.DataSource = lstOp;
                CmbSupplier.DisplayMember = "Operator_Name";
                CmbSupplier.ValueMember = "Operator_ID";
                if (lstOp.Count > 0)
                {
                    CmbSupplier.SelectedIndex = 0;
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
                List<CustomerAccessDetailsResult> lstCust = UserAdministrationBiz.CreateInstance().GetCustomerAccessDetails();
                lstCustomerAccessNotIncluded.DataSource = lstCust;
                lstCustomerAccessNotIncluded.DisplayMember = "Customer_Access_Name";
                lstCustomerAccessNotIncluded.ValueMember = "Customer_Access_ID";
                lstCustomerAccessIncluded.Items.Clear();
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
                List<UserGroupDetailsResult> lstUserGroup = UserAdministrationBiz.CreateInstance().GetUserGroupDetails();
                LstUserLevel.DataSource = lstUserGroup;
                LstUserLevel.DisplayMember = "User_Group_Name";
                LstUserLevel.ValueMember = "User_Group_ID";
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
                AtLoad = true;
                List<StaffNameResult> lstStaff = UserAdministrationBiz.CreateInstance().GetStaffName(Staff_ID, null, null);
                LstSurname.DataSource = lstStaff;
                AtLoad = false;
                LstSurname.DisplayMember = "Staff_Name";
                LstSurname.ValueMember = "Staff_ID";
                int ind = lstStaff.FindIndex(se => se.Staff_ID == _UserID);
                LstSurname.SelectedIndex = (ind >= 0) ? ind : 0;
                LstSurname_SelectedIndexChanged_1(LstSurname, null);
                LstSurname_ind = LstSurname.SelectedIndex;
                ChkTerminate.Checked = false;
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
                List<UserLanguagesDetailsResult> lstUserLang = UserAdministrationBiz.CreateInstance().GetUserLangDetails();
                cmbLanguage.DataSource = lstUserLang;
                cmbLanguage.DisplayMember = "LanguageName";
                cmbLanguage.ValueMember = "LanguageID";

                List<UserLanguagesDetailsResult> lstDate = new List<UserLanguagesDetailsResult>(lstUserLang);
                cmbDate.DataSource = lstDate;
                cmbDate.DisplayMember = "LanguageName";
                cmbDate.ValueMember = "LanguageID";

                List<UserLanguagesDetailsResult> lstCurrency = new List<UserLanguagesDetailsResult>(lstUserLang);
                cmbCurrency.DataSource = lstCurrency;
                cmbCurrency.DisplayMember = "LanguageName";
                cmbCurrency.ValueMember = "LanguageID";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void LockControls()
        {
            try
            {
                LstSurname.Enabled = true;
                ChkEngineer.Enabled = false;
                ChkRepresentative.Enabled = false;
                ChkStockController.Enabled = false;
                ChkTerminate.Enabled = false;

                TxtAddress.Enabled = false;
                TxtExtensionNumber.Enabled = false;
                TxtFirstName.Enabled = false;
                TxtHomePhone.Enabled = false;
                TxtMobilePhone.Enabled = false;
                TxtPassword.Enabled = false;
                TxtPasswordCheck.Enabled = false;
                TxtPostCode.Enabled = false;
                TxtStaffUserName.Enabled = false;
                TxtTitle.Enabled = false;
                txtWindowsUsername.Enabled = false;
                TxtDepart.Enabled = false;
                TxtJobDescription.Enabled = false;
                txtEmail.Enabled = false;
                TxtPersonnelNo.Enabled = false;
                rtfNotes.Enabled = false;

                lstCustomerAccessIncluded.Enabled = false;
                lstCustomerAccessNotIncluded.Enabled = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UnLockControls()
        {
            try
            {
                LstSurname.Enabled = false;
                ChkEngineer.Enabled = true;
                ChkRepresentative.Enabled = false;
                ChkStockController.Enabled = false;

                ChkTerminate.Enabled = true;

                TxtAddress.Enabled = true;
                TxtExtensionNumber.Enabled = true;
                TxtFirstName.Enabled = true;
                TxtHomePhone.Enabled = true;
                TxtMobilePhone.Enabled = true;
                TxtPassword.Enabled = true;
                TxtPasswordCheck.Enabled = true;
                TxtPostCode.Enabled = true;
                TxtStaffUserName.Enabled = true;
                TxtTitle.Enabled = true;
                TxtPersonnelNo.Enabled = true;
                txtWindowsUsername.Enabled = true;
                TxtDepart.Enabled = true;
                TxtJobDescription.Enabled = true;
                txtEmail.Enabled = true;


                rtfNotes.Enabled = true;

                lstCustomerAccessIncluded.Enabled = true;
                lstCustomerAccessNotIncluded.Enabled = true;
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
                List<StaffDetailsResult> lstStaff = UserAdministrationBiz.CreateInstance().GetStaffDetails(Staff_ID);
                if (lstStaff.Count > 0)
                {
                    StaffDetailsResult staffdet = lstStaff[0];
                    TxtDepart.Text = staffdet.Staff_Department;
                    TxtJobDescription.Text = staffdet.Staff_Job_Title;
                    ChkEngineer.Checked = Convert.ToBoolean(staffdet.Staff_IsAnEngineer);
                    ChkRepresentative.Checked = Convert.ToBoolean(staffdet.Staff_IsARepresentative);
                    ChkStockController.Checked = Convert.ToBoolean(staffdet.Staff_IsAStockController);
                    ChkTerminate.Checked = Convert.ToBoolean(staffdet.Staff_Terminated);
                    TxtAddress.Text = staffdet.Staff_Address;
                    TxtFirstName.Text = staffdet.Staff_First_Name;
                    txtSurname.Text = staffdet.Staff_Last_Name;
                    TxtHomePhone.Text = staffdet.Staff_Phone_No;
                    TxtMobilePhone.Text = staffdet.Staff_Mobile_No;
                    TxtPostCode.Text = staffdet.Staff_Postcode;
                    TxtTitle.Text = staffdet.Staff_Title;


                    List<UserGroupDetailsResult> lstUserGroup = (List<UserGroupDetailsResult>)LstUserLevel.DataSource;
                    if (lstUserGroup != null)
                    {
                        int ind = lstUserGroup.FindIndex(se => se.User_Group_ID == staffdet.User_Group_ID);
                        LstUserLevel.SelectedIndex = (ind >= 0) ? ind : 0;
                    }
                    TxtStaffUserName.Text = staffdet.Staff_Username;
                    TxtPersonnelNo.Text = staffdet.Staff_Personel_No;
                    TxtExtensionNumber.Text = staffdet.Staff_Extension_No;

                    rtfNotes.Text = staffdet.Staff_Notes;

                    if (staffdet.Staff_Password == string.Empty)
                    {
                        TxtPassword.Text = "No Current Password!";
                    }
                    else
                    {
                        //txtPassword.Text = Decode(details.Fields("staff_Password"), details.Fields("Staff_Username"))
                        //TxtPasswordCheck.Text = Decode(details.Fields("staff_Password"), details.Fields("Staff_Username"))
                    }

                    txtEmail.Text = staffdet.Email_Address;

                    List<OperatorEntity> lstOp = (List<OperatorEntity>)CmbSupplier.DataSource;
                    if (lstOp != null)
                    {
                        int ind = lstOp.FindIndex(se => se.Operator_ID == staffdet.Supplier_ID);
                        CmbSupplier.SelectedIndex = (ind >= 0) ? ind : 0;
                    }

                    List<DepotEntity> lstDepot = (List<DepotEntity>)CmbDepot.DataSource;
                    if (lstDepot != null)
                    {
                        int ind = lstDepot.FindIndex(se => se.Depot_ID == staffdet.Depot_ID);
                        CmbDepot.SelectedIndex = (ind >= 0) ? ind : 0;
                    }

                    List<ServiceAreasDetailsResult> lstService = (List<ServiceAreasDetailsResult>)CmdServiceArea.DataSource;
                    if (lstService != null)
                    {
                        int ind = lstService.FindIndex(se => se.Service_Area_ID == staffdet.Service_Area_ID);
                        CmdServiceArea.SelectedIndex = (ind >= 0) ? ind : 0;
                    }
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

                    List<UserDetailsResult> lstUserDet = UserAdministrationBiz.CreateInstance().GetUserDetails(_UserID);
                    if (lstUserDet.Count > 0)
                    {
                        UserDetailsResult userdet = lstUserDet[0];

                        txtWindowsUsername.Text = userdet.WindowsUserName;
                        TxtPassword.Text = userdet.Password;
                        TxtPasswordCheck.Text = userdet.Password;

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
                }
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
                List<OperatorEntity> lstOpDept = UserAdministrationBiz.CreateInstance().GetOperatorandDepotDetails();
                int Op_ID = 0;
                TreeOps.Nodes.Clear();
                TreeNode tOp = new TreeNode();
                foreach (OperatorEntity opt in lstOpDept)
                {
                    if (opt.Operator_ID != Op_ID)
                    {
                        tOp = TreeOps.Nodes.Add(opt.Operator_ID.ToString(), opt.Operator_Name);
                        Op_ID = opt.Operator_ID;
                        foreach (DepotEntity dept in opt.Depots)
                        {
                            tOp.Nodes.Add(dept.Depot_ID.ToString(), dept.Depot_Name);
                        }
                    }
                }
                TreeOps.ExpandAll();
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

                UncheckAllNodes(TreeOps.Nodes);
                List<StaffDepotResult> lstStaffDept = UserAdministrationBiz.CreateInstance().GetStaffDepot(Staff_ID);
                if (lstStaffDept != null && lstStaffDept.Count > 0)
                {

                    foreach (StaffDepotResult st in lstStaffDept)
                    {
                        TreeNode[] myNode;
                        myNode = TreeOps.Nodes.Find(st.Depot_ID.ToString(), true);
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
            string strAllowedChar = "[_a-zA-Z0-9]+";
            bool retVal = true;
            try
            {
                if (String.IsNullOrEmpty(txtSurname.Text))
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_USUSER_SURNAME"); //"Please Enter A Surname";
                    txtSurname.Focus();
                    retVal = false;
                }
                else if (txtSurname.Text.Trim().StartsWith("0"))
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_USUSER_ZERO"); //"The User Name cannot start with Zero";
                    txtSurname.Focus();
                    retVal = false;
                }
                else if (!(new Regex(strAllowedChar).IsMatch(TxtStaffUserName.Text)))
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_USUSER_INVALIDUSERNAME"); //"The User Name You have Entered Is Not Valid" + System.Environment.NewLine + "Please Enter A Different One";
                    TxtStaffUserName.Focus();
                    retVal = false;
                }

                else if (String.IsNullOrEmpty(TxtFirstName.Text))
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_USUSER_FIRSTNAME");// "Please Enter A First Name";
                    TxtFirstName.Focus();
                    retVal = false;
                }
                else if (String.IsNullOrEmpty(TxtStaffUserName.Text))
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_USUSER_INVALIDUSERNAME");  //"The User Name You have Entered Is Not Valid" + System.Environment.NewLine + "Please Enter A Different One";
                    TxtStaffUserName.Focus();
                    retVal = false;
                }
                else if (String.IsNullOrEmpty(txtWindowsUsername.Text))
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_USUSER_WINDOWUSERNAME");//"The Windows User Name You have Entered Is Not Valid" + System.Environment.NewLine + "Please Enter A Different One";
                    txtWindowsUsername.Focus();
                    retVal = false;
                }
                else if (String.IsNullOrEmpty(TxtPassword.Text) && NewUser)
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_USUSER_PASSWORD"); //"Please Enter And Check A Password";
                    TxtPassword.Focus();
                    retVal = false;
                }
                //Length > 0
                else if (!(TxtHomePhone.Text.IsNumeric() && TxtHomePhone.Text.IsLengthGreaterThanZero()) && NewUser)
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_USUSER_PHONENUMBER");// "Please Enter Valid HomePhone Number ";
                    TxtHomePhone.Focus();
                    retVal = false;
                }
                else if (!(TxtMobilePhone.Text.IsNumeric() && TxtMobilePhone.Text.IsLengthGreaterThanZero()) && NewUser)
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_USUSER_MOBILENUMBER");// "Please Enter Valid MobilePhone Number ";
                    TxtMobilePhone.Focus();
                    retVal = false;
                }
                else if (!(TxtPersonnelNo.Text.IsNumeric() && TxtPersonnelNo.Text.IsLengthGreaterThanZero()) && NewUser)
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_USUSER_PERSONALNUMBER"); //"Please Enter Valid Personnel Number ";
                    TxtPersonnelNo.Focus();
                    retVal = false;
                }
                else if (!(TxtExtensionNumber.Text.IsNumeric() && TxtExtensionNumber.Text.IsLengthGreaterThanZero()) && NewUser)
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_USUSER_EXTENSIONNUMBER");// "Please Enter Valid Extension Number ";
                    TxtExtensionNumber.Focus();
                    retVal = false;
                }
                else if (!txtEmail.Text.IsValidEmail() && NewUser)
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_USUSER_EMAILID"); //"The E-Mail ID is not in correct format.";
                    txtEmail.Focus();
                    retVal = false;

                }
                else if (TxtPassword.Text.Trim().Length < 5 && NewUser)
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_USUSER_PASSWORD5CHAR");// "Please enter a password text with minimum 5 characters.";
                    TxtPassword.Focus();
                    retVal = false;
                }
                else if (TxtPassword.Text != TxtPasswordCheck.Text && NewUser)
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_USUSER_INVALIDPASSWORDthis");//"The Password And The Password Check You have Entered Do Not Match" + System.Environment.NewLine + "Please Enter Them Both Again";
                    TxtPasswordCheck.Text = "";
                    TxtPassword.Focus();
                    retVal = false;
                }
                else if (!BMC.Security.PasswordHelper.CheckPasswordStrength(TxtPassword.Text) && NewUser)
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_USUSER_PASSWORDWEAK"); //"Password is weak. Please enter a password which is a combination of the following character groups [a-z],[A-Z],[0-9]";
                    retVal = false;
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

        void UpdateStaffCustomerAccessDetails()
        {
            try
            {
                if (!NewUser)
                {
                    if (LstSurname.SelectedItem != null)
                    {


                        StaffNameResult staff_res = LstSurname.SelectedItem as StaffNameResult;
                        UserAdministrationBiz.CreateInstance().UpdateStaffCustomerAccess(staff_res.Staff_ID, 0);
                        foreach (CustomerAccessDetailsResult cust in lstCustomerAccessIncluded.Items)
                        {
                            if (UserAdministrationBiz.CreateInstance().UpdateStaffCustomerAccess(staff_res.Staff_ID, cust.Customer_Access_ID))
                            {
                                LogManager.WriteLog("CustomerAccessDetails Updated Successfully Staff_ID:" + staff_res.Staff_ID, LogManager.enumLogLevel.Info);
                            }
                            else
                            {
                                LogManager.WriteLog("CustomerAccessDetails Updation Failed Staff_ID:" + staff_res.Staff_ID, LogManager.enumLogLevel.Info);
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

        void CreateNewOrUpdateUser(int? _UserTableUserID)
        {
            try
            {
                UserTableUserID = UserAdministrationBiz.CreateInstance().UpdateUserDetails(txtWindowsUsername.Text,BMC.Security.CryptoHelper.Encrypt(TxtPassword.Text), TxtStaffUserName.Text,
                   (int)cmbLanguage.SelectedValue, (int)cmbCurrency.SelectedValue, (int)cmbDate.SelectedValue,ref _UserTableUserID);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        #endregion

        #region Events

        private void CmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (CmbSupplier.SelectedItem != null && (CmbSupplier_ind != CmbSupplier.SelectedIndex))
                {
                    OperatorEntity opRes = (OperatorEntity)CmbSupplier.SelectedItem;
                    List<DepotEntity> lstDepot = UserAdministrationBiz.CreateInstance().GetDepotDetails(opRes.Operator_ID);
                    CmbDepot.DataSource = lstDepot;
                    CmbDepot.DisplayMember = "Depot_Name";
                    CmbDepot.ValueMember = "Depot_ID";
                    CmbSupplier_ind = CmbSupplier.SelectedIndex;
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
                if (CmbDepot.SelectedItem != null && (CmbDepot_ind != CmbDepot.SelectedIndex))
                {
                    DepotEntity DepRes = (DepotEntity)CmbDepot.SelectedItem;
                    List<ServiceAreasDetailsResult> lstService = UserAdministrationBiz.CreateInstance().GetServiceAreasDetails((int)DepRes.Depot_ID);
                    CmdServiceArea.DataSource = lstService;
                    CmdServiceArea.DisplayMember = "Service_Area_Name";
                    CmdServiceArea.ValueMember = "Service_Area_ID";
                    CmbDepot_ind = CmbDepot.SelectedIndex;
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
                if (BtnEdit.Text.Equals("Edit"))
                {
                    UnLockControls();
                    if (GlobalUserTableId == UserTableUserID)
                    {
                        btnResetPassword.Enabled = false;
                        TxtPassword.Enabled = false;
                        TxtPasswordCheck.Enabled = false;
                    }
                    else
                    {
                        btnResetPassword.Enabled = true;
                        TxtPassword.Enabled = false;
                        TxtPasswordCheck.Enabled = false;

                    }
                    TxtStaffUserName.Enabled = false;

                    BtnUpdate.Visible = true;
                    BtnEdit.Visible = false;
                    btnNew.Visible = false;
                    //BtnForeColour.Visible = true;
                    //BtnBackColour.Visible = true;
                    ///BtnEdit.Text = "Cancel";
                    btnCancel.Visible = true;
                    btnAssignEmpCard.Visible = true;

                    btnAssignSite.Enabled = true;
                    LstSurname.Visible = false;
                    txtSurname.Visible = true;
                    TxtFirstName.Visible = true;
                    cmbLanguage.Enabled = true;
                    cmbCurrency.Enabled = true;
                    cmbDate.Enabled = true;
                    CmbSupplier.Enabled = true;
                    CmbDepot.Enabled = true;
                    CmdServiceArea.Enabled = true;
                    ChkRepresentative.Enabled = true;
                    ChkStockController.Enabled = true;
                    LstUserLevel.Enabled = true;
                    btnAssignEmpCard.Enabled = true;
                    btnLockUser.Enabled = true;
                    btnUnLockUser.Enabled = true;
                }
                else
                {
                    BtnEdit.Text = "Edit";
                    btnCancel_Click();
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
                int Surname_ind = LstSurname.SelectedIndex;

                if (GlobalUserTableId == UserTableUserID)
                    BtnEdit.Visible = AppGlobals.Current.HasUserAccess("HQ_Admin_Users_Edit");
                else
                    BtnEdit.Visible = AppGlobals.Current.HasUserAccess("HQ_Admin_Users_Editall");

                btnNew.Visible = AppGlobals.Current.HasUserAccess("HQ_Admin_Users_Editall");

                Form_Load();
                LstSurname.SelectedIndex = Surname_ind;
                btnResetPassword.Enabled = false;
                btnAssignSite.Enabled = false;

                LstSurname.Visible = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            string ErrorMsg = "";

            if (ValidateControls(ref ErrorMsg))
            {
                UpdateStaffCustomerAccessDetails();
                if (NewUser)
                {
                    CreateNewOrUpdateUser(0);
                  //  Win32Extensions.ShowInfoMessageBox("User Details Inserted Successfully");
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_USUSER_INSERT"), this.ParentForm.Text);
                }
                else
                {

                    if (UserTableUserID != 0)
                    {
                        CreateNewOrUpdateUser(UserTableUserID);
                      //  Win32Extensions.ShowInfoMessageBox("User Details Updated Successfully");
                        Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_USUSER_UPDATE"), this.ParentForm.Text);
                    }
                }
            }
            else
            {
                Win32Extensions.ShowInfoMessageBox(this, ErrorMsg, this.ParentForm.Text);
            }
            btnLockUser.Enabled = false;
            btnUnLockUser.Enabled = false;
            btnResetPassword.Enabled = false;
            btnAssignSite.Enabled = false;
        }


        private void lstCustomerAccessNotIncluded_Click(object sender, EventArgs e)
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

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void lstCustomerAccessIncluded_Click(object sender, EventArgs e)
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


                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnAssignSite_Click(object sender, EventArgs e)
        {
            if (UserTableUserID == 0)
               // Win32Extensions.ShowInfoMessageBox("Edit the user details, Update and click Edit button again to assign sites to user.");
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_USUSER_USERDETAILS"), this.ParentForm.Text);
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
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_USUSER_ADMINPACKAGE"), this.ParentForm.Text);
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
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
                //Win32Extensions.ShowInfoMessageBox("Edit the user details, Update and click Edit button again to reset password.");
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_USUSER_EDITPASSWORD"), this.ParentForm.Text);
                return;
            }
            else
            {
                //if (Win32Extensions.ShowQuestionMessageBox("Do you wish to reset the password?") == DialogResult.Yes)
                if (Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1, "MSG_USUSER_RESTPASSWORD"), this.ParentForm.Text) == DialogResult.Yes)
                {
                    sResult = BMCSecurityMethod.GeneratePassword();
                    if (sResult != "Unable to Reset")
                    {
                        sResult = sResult.Replace("New Password: ", "");
                     //   Win32Extensions.ShowInfoMessageBox("New Password is :" + sResult);
                        Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_USUSER_NEWPASSWORD"), this.ParentForm.Text);

                        TxtPassword.Text = sResult;
                        TxtPasswordCheck.Text = sResult;
                        sEncPass = CryptoHelper.CreateHash(TxtPassword.Text);
                        if (BMCSecurityMethod.InsertPassword(ref UserID, ref sEncPass))
                           // Win32Extensions.ShowInfoMessageBox("Password updated successfully");
                            Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_USUSER_UPDATEPASSWORD"), this.ParentForm.Text);
                        else
                          //  Win32Extensions.ShowInfoMessageBox("Password not updated");
                            Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_USUSER_NOTUPDATED"), this.ParentForm.Text);
                    }
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewUser = true;
            BtnEdit.Visible = false;
            btnNew.Enabled = false;
            BtnUpdate.Visible = true; //'CheckSecurity("HQ_Admin_Users_Edit")
            txtSurname.Visible = true;
            TxtFirstName.Visible = true;
            LstSurname.Visible = false;

            btnResetPassword.Enabled = false;
            btnAssignSite.Enabled = false;

            ChkRepresentative.Enabled = true;
            ChkStockController.Enabled = true;
            ChkStockController.Checked = false;
            ChkRepresentative.Checked = false;
            LstUserLevel.Enabled = true;
            CmbSupplier.Enabled = true;
            CmbDepot.Enabled = true;
            CmdServiceArea.Enabled = true;
            cmbLanguage.Enabled = true;
            cmbCurrency.Enabled = true;
            cmbDate.Enabled = true;
            cmbLanguage.SelectedIndex = -1;
            cmbCurrency.SelectedIndex = -1;
            cmbDate.SelectedIndex = -1;
            LoadRepresentativeTree();

            TxtAddress.Enabled = true;
            txtEmail.Enabled = true;
            TxtExtensionNumber.Enabled = true;
            TxtFirstName.Enabled = true;
            TxtHomePhone.Enabled = true;
            TxtMobilePhone.Enabled = true;
            TxtPassword.Enabled = true;
            TxtPasswordCheck.Enabled = true;
            TxtPostCode.Enabled = true;
            TxtStaffUserName.Enabled = true;
            TxtTitle.Enabled = true;
            TxtDepart.Enabled = true;
            TxtJobDescription.Enabled = true;
            txtWindowsUsername.Enabled = true;

            ChkEngineer.Checked = false;
            TxtPersonnelNo.Enabled = true;

            rtfNotes.Enabled = true;

            LstSurname.Enabled = true;
            ClearText();
        }

        private void ClearText()
        {

            TxtAddress.Text = string.Empty;
            txtEmail.Text = string.Empty;
            TxtExtensionNumber.Text = string.Empty;
            TxtFirstName.Text = string.Empty;
            TxtHomePhone.Text = string.Empty;
            TxtMobilePhone.Text = string.Empty;
            TxtPassword.Text = string.Empty;
            TxtPasswordCheck.Text = string.Empty;
            TxtPostCode.Text = string.Empty;
            TxtStaffUserName.Text = string.Empty;
            TxtTitle.Text = string.Empty;
            TxtDepart.Text = string.Empty;
            TxtJobDescription.Text = string.Empty;
            txtWindowsUsername.Text = string.Empty;

            ChkEngineer.Checked = false;
            TxtPersonnelNo.Text = string.Empty;

            rtfNotes.Text = string.Empty;

            if (LstSurname.Visible)
                LstSurname.Focus();
            else
                txtSurname.Focus();

        }

        private void btnAssignEmpCard_Click(object sender, EventArgs e)
        {
            frmAssociateUsertoEmployeeCard assEmpcard = new frmAssociateUsertoEmployeeCard(UserTableUserID);
            assEmpcard.StartPosition = FormStartPosition.CenterScreen;
            assEmpcard.ShowDialog();


        }
        #endregion

        private void ucUsers_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            this.Size = this.Parent.Size;
            Form_Load();
        }

        private void LstSurname_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                if ((!Update) && LstSurname_ind != LstSurname.SelectedIndex)
                {

                    if (LstSurname.SelectedIndex != -1)
                    {
                        StaffNameResult staff = (StaffNameResult)LstSurname.SelectedItem;
                        //LoadStaffDetails(staff.Staff_ID);
                        LstSurname_ind = LstSurname.SelectedIndex;
                        LoadStaffDetails(staff.Staff_ID);
                        SetRepresentativeTree(staff.Staff_ID);

                        UserTableUserID = staff.UserTableID;

                    }
                    else
                    {
                        //Call MsgBox("Please Update the changes you have made!", , Branding)
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnUnLockUser_Click(object sender, EventArgs e)
        {
            islockedValue = 0;
            DialogResult dr = Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1, "MSG_USUSER_UNLOCKUSER"), this.ParentForm.Text);//this.ShowQuestionMessageBox("Do you want to unlock the User?");
            if (dr.ToString() == "Yes")
            {
                UpdateUserLockStatus();
               // this.ShowInfoMessageBox("User UnLocked Successfully");
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_USUSER_UNLOCKSUCCESSFUL"), this.ParentForm.Text);
            }
            btnUnLockUser.Visible = false;
            btnLockUser.Visible = true;
        }

        private void UpdateUserLockStatus()
        {
            List<UserLockStatusResult> objLockStatus = objUserAdminBiz.UserLockStatus
                (UserTableUserID, islockedValue);
        }

        private void btnLockUser_Click(object sender, EventArgs e)
        {
            islockedValue = 1;
            DialogResult dr = Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1, "MSG_USUSER_LOCKUSER"), this.ParentForm.Text);//this.ShowQuestionMessageBox("Do you want to lock the User?");
            if (dr.ToString() == "Yes")
            {
                UpdateUserLockStatus();
               // this.ShowInfoMessageBox("User Locked Successfully");
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_USUSER_LOCKSUCCESSFUL"), this.ParentForm.Text);
            }
            btnLockUser.Visible = false;
            btnUnLockUser.Visible = true;
        }

        private void btnCancel_Click()
        {

        }

    }
}
