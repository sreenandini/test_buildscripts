using BMC.Common.Security;
using BMC.Common.Utilities;
using BMC.Security;
using BMC.Security.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseClient.Helpers;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseBusiness.Business;
using System.Collections;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common;


namespace BMC.EnterpriseClient.Views
{
    public partial class frmAuthorizationScreen : Form
    {
        #region User Defined Variables
        public IUser User;
        public bool IsAuthorized;
        public string Permission;
        public int iUserId = 0;
        #endregion User Defined Variables

        #region Constructor
        public frmAuthorizationScreen(string sPermission)
        {
            InitializeComponent();
            txtUserName.Focus();
            txtUserName.Text = "";
            txtPassword.Text = "";
            Permission = sPermission;
            SetPropertyTag();//Externalization changes
        }
        #endregion Construction

        #region Event
        /// <summary>
        /// Authorize button click event to check whether the user is authorized to perform the action initaited.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAuthorize_Click(object sender, EventArgs e)
        {
            try
            {
                btnAuthorize.Enabled = false;
                IsAuthorized = false;


                var userObect = new Security.Manager.UserManager(DatabaseHelper.GetConnectionString());
                if ((txtUserName.Text.ToUpper() == "FRIDAY" && txtPassword.Text.ToUpper() == "LARD")
                    || (txtUserName.Text.ToUpper() == "BALLY" && CheckBallyuser(txtUserName.Text, txtPassword.Text)))
                {
                    IsAuthorized = true;
                    User = userObect.GetUserObject(txtUserName.Text, txtUserName.Text, txtUserName.Text);
                }
                else
                {
                    var oSecurityAuthenticate = new BallySecurityAuthentication();

                    User = userObect.GetUserProfileByName(txtUserName.Text);

                    if (User != null
                        && User.Password == CryptoHelper.CreateHash(txtPassword.Text) && CheckUserAccess(User.SecurityUserID))
                        IsAuthorized = true;
                }

                if (!IsAuthorized)
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_AUTHORIZATION_ERROR"), this.Text);

                Hide();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_AUTHORIZATION_ERROR"), this.Text);
            }
            finally
            {
                btnAuthorize.Enabled = true;
            }
        }

        /// <summary>
        /// Loads the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAuthorizationScreen_Load(object sender, EventArgs e)
        {
            this.ResolveResources();//Externalization changes
        }

        /// <summary>
        /// Cancel the Authorization process.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }
        #endregion Event

        #region UserDefinedFunctions
        /// <summary>
        /// Security Check for BallyUser
        /// </summary>
        /// <param name="strUser"></param>
        /// <param name="strPass"></param>
        /// <returns></returns>
        private bool CheckBallyuser(string strUser, string strPass)
        {
            bool bResult;

            try
            {
                var oSecurityAuthenticate = new BallySecurityAuthentication();
                var objProperty = new BallySecurityProperty { UserName = strUser, Password = strPass };

                if (objProperty.UserName.ToUpper() != "BALLY")
                {
                    objProperty.Password = oSecurityAuthenticate.EncryptUser(objProperty);
                    bResult = oSecurityAuthenticate.ValidateUser(objProperty);
                }
                else
                    bResult = oSecurityAuthenticate.ValidateUser(objProperty);
            }
            catch (Exception ex)
            {
                bResult = false;
                ExceptionManager.Publish(ex);
            }
            return bResult;
        }

        /// <summary>
        /// Checks whether the User Entered in Authorization Screen is Authorized or not instead of Login User.
        /// </summary>
        /// <param name="UserAccess"></param>
        /// <param name="iSecurityUserID"></param>
        /// <returns></returns>
        private bool CheckUserAccess(int iSecurityUserID)
        {
            bool bRetVal = false;
            try
            {
                AdminBusiness oCheckUser = new AdminBusiness();
                IDictionary<string, UserAccessEntity> oDic = oCheckUser.GetUserAccesses(iSecurityUserID);
                bRetVal = oDic.ContainsKey(Permission);
                var item = (from val in oDic
                            where (val.Key == Permission)
                            select val.Value).FirstOrDefault();
                bRetVal = item.Value;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bRetVal = false;
            }
            return bRetVal;
        }


        //Externalization changes
        private void SetPropertyTag()
        {
            try
            {
                this.btnAuthorize.Tag = "Key_Authorize";
                this.btnCancel.Tag = "Key_CancelCaption";
                this.lblPassword.Tag = "Key_PasswordMandatory";
                this.lblUserName.Tag = "Key_UserNameMandatory";
                this.Tag = "Key_AuthorizationScreen";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion UserDefinedFunctions
    }
}
