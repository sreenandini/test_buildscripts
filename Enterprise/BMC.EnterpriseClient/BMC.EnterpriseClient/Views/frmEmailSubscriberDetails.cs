using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseClient.Helpers;
using BMC.Common.Utilities;
using BMC.Common;
using BMC.CoreLib;
using System.Text.RegularExpressions;
using BMC.Security;


namespace BMC.EnterpriseClient.Views
{
    public delegate void OnRowSelect(object sender, EventArgs e);

    public delegate void OnLoadInitialDetails();

    public partial class frmEmailSubscriberDetails : Form
    {
        /// <summary>
        /// Declarations
        /// </summary>
        /// 
        #region Declarations
        List<AlertTypes> _AlertTypes = null;
        AlertSystemBiz _alertbiz = null;
        OnRowSelect rowSelect = null;
        public OnLoadInitialDetails loadinitial;
        Helpers.Datawatcher objDatawatcher = null;
        string ErrorMsg = string.Empty;
        string DisplayMsg = string.Empty;
        private string _userId = string.Empty;

        #endregion
        public frmEmailSubscriberDetails()
        {
            InitializeComponent();

           // objDatawatcher = new Helpers.Datawatcher(this,
           //(w, f) =>
           //{
           //    w.RemoveControlFromWatcher((f as frmEmailSubscriberDetails).cboAlertTypes);
           //});

            setTagProperty();
            _alertbiz = AlertSystemBiz.CreateInstance();

            loadinitial += LoadInitialDetails;

            loadinitial -= LoadInitialDetails;

            loadinitial += LoadMailServerInfo;
        }

        private void setTagProperty()
        {
            this.lblAlertType.Tag = "Key_AlertType";
            this.lblSubject.Tag = "Key_Subject";
            this.lblSender.Tag = "Key_Sender";
            this.lblSubscribers.Tag = "Key_SubScribers";
            this.Tag = "Key_EmailSubscribers";
            this.lblCCSubscribers.Tag = "Key_CCSubscribers";
            this.lblBCCSubscribers.Tag = "Key_BCCSubscribers";
            this.btnSave.Tag = "Key_SaveCaption";
            this.lblServer.Tag = "Key_ServerNameColon";
            this.lblport.Tag = "KEY_Port";
            this.lblUID.Tag = "Key_RF_UserName";
            this.lblPWd.Tag = "Key_PasswordColon";
            this.lblEnableSSL.Tag = "Key_EnableSSL";
            this.btnSaveMailInfo.Tag = "Key_SaveCaption";
        }

        #region "Events"
      
        /// <summary>
        /// save the subscribers details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateControls(ref ErrorMsg))
                {
                    EmailAlertEntity entity = new EmailAlertEntity();
                    entity.AlertTypeId = (cboAlertTypes.SelectedItem as AlertTypes).AlertTypeID;
                    entity.Subject = txtSubject.Text;
                    entity.ToMail = SubscribersList(dgvSubscribers);
                    entity.CCMail = SubscribersList(dgvCCSubscribers);
                    entity.BCCMail = SubscribersList(dgvBccSubscribers);
                    entity.FromMail = txtSender.Text;

                    //_alertbiz.SaveEmailAlerts(entity);
                    DisplayMsg = this.GetResourceTextByKey(1, "MSG_EMAILSUBSCRIBER_SAVEDETAILS");
                    Win32Extensions.ShowInfoMessageBox(this, DisplayMsg, this.Text);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        bool ValidateControls(ref string ErrorMsg)
        {
            bool retVal = true;
            try
            {
                if (cboAlertTypes.SelectedIndex == -1)
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_ENT_ALERTTYPE_SELECT");
                    Win32Extensions.ShowErrorMessageBox(this, ErrorMsg, this.Text);
                    cboAlertTypes.Focus();
                    return false;
                }
                 if (string.IsNullOrEmpty(txtSubject.Text.Trim()))
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_ENT_SUBJECT");
                    Win32Extensions.ShowErrorMessageBox(this, ErrorMsg, this.Text);
                    txtSubject.Focus();
                    return false;
                }
                if (dgvSubscribers.Rows.Count == 0)
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_ENT_TOLIST");
                    Win32Extensions.ShowErrorMessageBox(this, ErrorMsg, this.Text);
                    txtSubject.Focus();
                    return false;
                }
                else if (dgvSubscribers.Rows.Count == 1 && (dgvSubscribers.Rows[0].Cells[0].Value == null || string.IsNullOrEmpty(dgvSubscribers.Rows[0].Cells[0].Value.ToString())))
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_ENT_TOLIST");
                    Win32Extensions.ShowErrorMessageBox(this, ErrorMsg, this.Text);
                    dgvSubscribers.Focus();
                    return false;
                }
                else if (dgvSubscribers.Rows.Count > 0)
                {
                    retVal= setValidation(dgvSubscribers);
                    if (!retVal) return retVal;
                }
                if (dgvCCSubscribers.Rows.Count > 0)
                {
                    retVal = setValidation(dgvCCSubscribers);
                    if (!retVal) return retVal;
                }
                if (dgvBccSubscribers.Rows.Count > 0)
                {
                    retVal = setValidation(dgvBccSubscribers);
                    if (!retVal) return retVal;
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

        private bool setValidation(DataGridView view)
        {
            bool retVal = true;

            foreach (DataGridViewRow row in view.Rows)
            {

                if (row.Cells[0].Value != null)
                {
                    if (!string.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    {
                        if (!ValidateEmail(row.Cells[0].Value.ToString()))
                        {
                            ErrorMsg = this.GetResourceTextByKey(1, "MSG_INVALID_EMAIL_ID");
                            Win32Extensions.ShowErrorMessageBox(this, ErrorMsg, this.Text);
                            row.Selected = true;
                            retVal = false;
                            break;
                        }
                    }
                }
            }

            return retVal && DuplicateCheck(view);
        }

        private bool DuplicateCheck(DataGridView view )
        {
            bool retVal =true;
            if (view.DataSource != null)
            {
                if (view.Rows.Count > 1)
                {
                    var Res = view.Rows.OfType<DataGridViewRow>().Select(
                                        r => r.Cells.OfType<DataGridViewCell>().Select(c => c.Value).ToArray()).ToList();

                    if (Res.Distinct().Count() != Res.Count())
                    {
                        ErrorMsg = this.GetResourceTextByKey(1, "MSG_EMAILID_EXISTS");
                        Win32Extensions.ShowErrorMessageBox(this, ErrorMsg, this.Text);

                        retVal = false;
                    }
                }
            }
            return retVal;
        }
    
        bool ValidateMailServerControls(ref string ErrorMsg)
        {
            bool retVal = true;
            try
            {
                if (string.IsNullOrEmpty(txtServerName.Text.Trim()))
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_EC_ENTER_SERVERNAME");
                    Win32Extensions.ShowErrorMessageBox(this, ErrorMsg, this.Text);
                    txtServerName.Focus();
                    retVal = false;
                }
                else if (string.IsNullOrEmpty(txtPort.Text.Trim()) || (!(txtPort.Text.Trim().IsNumeric())))
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_ALERT_PORT_NO");
                    Win32Extensions.ShowErrorMessageBox(this, ErrorMsg, this.Text);
                    txtPort.Focus();
                    retVal = false;
                }
                else if (chkEnableSSL.Checked && (string.IsNullOrEmpty(txtUserID.Text) ) )
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_ALERT_MAILSERVERUSERNAME");
                    Win32Extensions.ShowErrorMessageBox(this, ErrorMsg, this.Text);
                    retVal = false;
                }
                else if (chkEnableSSL.Checked && !ValidateEmail(txtUserID.Text))
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_INVALID_EMAIL_ID");
                    Win32Extensions.ShowErrorMessageBox(this, ErrorMsg, this.Text);
                    retVal = false;
                }
                else if (chkEnableSSL.Checked && (string.IsNullOrEmpty(txtPassword.Text)))
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_ALERT_MAILSERVERPASSWORD");
                    Win32Extensions.ShowErrorMessageBox(this, ErrorMsg, this.Text);
                    retVal = false;
                }
                else
                    retVal = true;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                ErrorMsg = ex.Message;
                retVal = false;
            }
            return retVal;
        }

        private void frmEmailSubscriberDetails_Load(object sender, System.EventArgs e)
        {
            try
            {
                this.ResolveResources();
                //objDatawatcher.DataModify = false;
                ClearControls();
              
                LoadInitialDetails();
                cboAlertTypes.SelectedIndex = 0;
                LoadMailServerInfo();
                LoadEmailSubscribersDetails();

                //assign the delegate.
                rowSelect = this.RowSelectEvt;
                //add the delete menu item.
                string key=this.GetResourceTextByKey("Key_Delete");
                Strip.Items.Add(key, null, new EventHandler(rowSelect));

                this.txtUserID.Enabled = false;
                this.txtPassword.Enabled = false;

                CheckCredentials();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }


        /// <summary>
        /// get the context menu clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            try
            {
                var Dgv = sender as DataGridView;
                if (Dgv != null)
                {
                    // Change the selection to reflect the right-click
                    Dgv.ClearSelection();
                    Dgv.Rows[e.RowIndex].Selected = true;
                }
            }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); }
        }

        /// <summary>
        /// Delete the selected row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RowSelectEvt(object sender, EventArgs e)
        {
            try
            {
                ToolStripDropDownItem item = sender != null ? sender as ToolStripDropDownItem : null;
                ContextMenuStrip parent = item.Owner != null ? item.Owner as ContextMenuStrip : null;

                DataGridView view = parent.SourceControl != null ? parent.SourceControl as DataGridView : null;
                // Now pick up the selection as we know this is the row we right-clicked on

                if (view != null)
                {
                    DisplayMsg = this.GetResourceTextByKey(1,"MSG_ALERT_DELETEROW");
                    DialogResult result = Win32Extensions.ShowQuestionMessageBox(DisplayMsg);

                    if (result.ToString().ToUpper() == "YES")
                        view.Rows.RemoveAt(view.CurrentCell.RowIndex);
                }
            }
            catch (Exception ex)
            {
                DisplayMsg = this.GetResourceTextByKey(1, "MSG_ALERT_ERRDELETEROW");
                Win32Extensions.ShowInfoMessageBox(DisplayMsg);
                ExceptionManager.Publish(ex);
            }
        }

        #endregion

        #region "Private Methods"


        /// <summary>
        /// get the list of subscribers.
        /// </summary>
        /// <param name="dgvView"></param>
        /// <returns></returns>
        private string SubscribersList(DataGridView dgvView)
        {
            StringBuilder sSubscribers = new StringBuilder();
            foreach (DataGridViewRow row in dgvView.Rows)
            {
                if (row.Cells[0].Value == null) continue;
                sSubscribers.Append(row.Cells[0].Value.ToString());
                sSubscribers.Append(";");
            }
            return sSubscribers.ToString();
        }

        /// <summary>
        /// Load teh alert types.
        /// </summary>
        private void LoadInitialDetails()
        {
            try
            {
                //get the list of supported alerts.
                _AlertTypes = _alertbiz.GetAlertTypes();
                cboAlertTypes.DataSource = _AlertTypes;
                cboAlertTypes.DisplayMember = "AlertTypeName";
                cboAlertTypes.ValueMember = "AlertTypeID";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Load the subscriber details.
        /// </summary>
        private void LoadEmailSubscribersDetails()
        {
            List<EmailAlertEntity> lstSubscribers = null;
            lstSubscribers = _alertbiz.GetEmailSubscribers((cboAlertTypes.SelectedItem as AlertTypes).AlertTypeName);
            ClearControls();
            foreach (EmailAlertEntity emailEntity in lstSubscribers)
            {
                cboAlertTypes.SelectedIndex = cboAlertTypes.FindString(emailEntity.AlertTypeName, 0);
                txtSubject.Text = emailEntity.Subject;
                txtSender.Text = _userId;
                MapSubscribers(emailEntity.ToMail, dgvSubscribers);
                MapSubscribers(emailEntity.CCMail, dgvCCSubscribers);
                MapSubscribers(emailEntity.BCCMail, dgvBccSubscribers);
            }

        }

        /// <summary>
        /// load the mail server details.
        /// </summary>
        private void LoadMailServerInfo()
        {
            try
            {
                MailServer serverinfo = _alertbiz.GetMailServerInfo();

                txtPassword.Text = SiteLicensingCryptoHelper.Decrypt(serverinfo.Password, "B411y51T");
                txtPort.Text = serverinfo.Port;
                txtServerName.Text = serverinfo.ServerName;
                txtUserID.Text = serverinfo.UserID;
                chkEnableSSL.Checked = serverinfo.EnableSSL;
                _userId = serverinfo.UserID;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Map the subscribers to individual grid.
        /// </summary>
        /// <param name="strSubscribers"></param>
        /// <param name="view"></param>
        private void MapSubscribers(string strSubscribers, DataGridView view)
        {
            DataGridViewRow row = null;
            DataGridViewCell cell1 = null;
            string[] sSub = strSubscribers.Split(';');

            foreach (string str in sSub)
            {
                if (string.IsNullOrEmpty(str)) return;
                row = new DataGridViewRow();
                cell1 = new DataGridViewTextBoxCell();
                cell1.Value = str;
                row.Cells.Add(cell1);
                view.Rows.Add(row);
            }
        }

        #endregion

        /// <summary>
        /// save the mail server details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveMailInfo_Click(object sender, EventArgs e)
        {
         
            
            try
            {
                if (ValidateMailServerControls(ref ErrorMsg))
                {
                    MailServer serverinfo = new MailServer();
                    serverinfo.ServerName = txtServerName.Text;
                    serverinfo.Port = txtPort.Text;
                    serverinfo.UserID = txtUserID.Text;
                    serverinfo.Password = SiteLicensingCryptoHelper.Encrypt(txtPassword.Text,"B411y51T");
                    serverinfo.EnableSSL = chkEnableSSL.Checked ? true : false;

                    _alertbiz.SaveMailServerInfo(serverinfo, AppGlobals.Current.UserName,AppGlobals.Current.UserId,string.Empty, "Server: " + txtServerName.Text + "port:" + txtPort.Text
                      + "UserID:" + txtUserID.Text);
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ALERT_MAILSERVERINFO"), this.Text);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public static bool ValidateEmail(string EMailID)
        {
            string MatchEmailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
       + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
       + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
       + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

            return Regex.IsMatch(EMailID, MatchEmailPattern);
        }

        private void chkEnableSSL_CheckedChanged(object sender, EventArgs e)
        {
            CheckCredentials();
        }

        private void CheckCredentials()
        {
            if (chkEnableSSL.Checked)
            {
                txtUserID.Enabled = true;
                txtPassword.Enabled = true;
            }
            else
            {
                txtUserID.Enabled = false;
                txtPassword.Enabled = false;
            }
        
        }


        private void cboAlertTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearControls();
            LoadEmailSubscribersDetails();
        }

        private void ClearControls()
        {
            txtSubject.Text = string.Empty;
            txtSender.Text = string.Empty;
            dgvSubscribers.Rows.Clear();
            dgvCCSubscribers.Rows.Clear();
            dgvBccSubscribers.Rows.Clear();
        }
    }
}