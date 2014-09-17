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
using System.Xml.Serialization;
using System.Linq;


namespace BMC.EnterpriseClient.Views
{
    public partial class frmMailAlertConfiguration : Form, ICommand
    {
        #region Declarations
        List<AlertTypes> _AlertTypes = null;
        AlertSystemBiz _alertbiz = null;
        public OnLoadInitialDetails loadinitial;
        string ErrorMsg = string.Empty;
        string DisplayMsg = string.Empty;
        private string _userId = string.Empty;
        [XmlElement("MailSubscribers")]
        List<EmailAlertEntity> lEntities = null;
     
        #endregion
        public frmMailAlertConfiguration()
        {
            InitializeComponent();
            setTagProperty();
            _alertbiz = AlertSystemBiz.CreateInstance();

            loadinitial += LoadMailServerInfo;

            loadinitial += LoadInitialDetails;

            loadinitial -= LoadInitialDetails;

            this.dgvSubscribers.CellEndEdit += AfterRowLeave;
            this.dgvCCSubscribers.CellEndEdit += AfterRowLeave;
            this.dgvBccSubscribers.CellEndEdit += AfterRowLeave;
        }



        void AfterRowLeave(object sender, DataGridViewCellEventArgs e)
        {
            ValidateEmail(sender, e.RowIndex);
        }

        /// <summary>
        /// validate email for duplicate entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="RowIndex"></param>
        private void ValidateEmail(object sender, int RowIndex)
        {
            bool bExists = false;
            try
            {
                DataGridView view = sender as DataGridView;
                var Rows = view.Rows.Cast<DataGridViewRow>().
                    Where(row => row.Cells[1].EditedFormattedValue != null && row.Cells[1].EditedFormattedValue.ToString() != string.Empty);

                view.Rows.Cast<DataGridViewRow>().ForEach((item) =>
                {
                    if (item.Cells[1] != null && item.Index != RowIndex && !(string.IsNullOrEmpty(item.Cells[1].EditedFormattedValue.ToString()))
                     && item.Cells[1].EditedFormattedValue.ToString().Equals(view.Rows[RowIndex].Cells[1].EditedFormattedValue.ToString()))

                    { bExists = true; view.Rows[RowIndex].Cells[1].Value = string.Empty; }
                }, () => bExists);


                if (bExists)
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_EMAILID_EXISTS");
                    Win32Extensions.ShowErrorMessageBox(this, ErrorMsg, this.Text);
                    view.Rows[RowIndex].Selected = true;
                }

            }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); }
        }

        private void setTagProperty()
        {
            this.tvAlertTypes.Tag = "Key_AlertType";
            this.lblSubject.Tag = "Key_Subject";
            this.lblSender.Tag = "Key_Sender";
            this.lblSubscribers.Tag = "Key_SubScribers";
            this.Tag = "Key_EmailSubscribers";
            this.lblCCSubscribers.Tag = "Key_CCSubscribers";
            this.lblBccSubscribers.Tag = "Key_BCCSubscribers";
            this.btnSave.Tag = "Key_SaveCaption";
            this.lblServer.Tag = "Key_ServerNameColon";
            this.lblport.Tag = "KEY_Port";
            this.lblUID.Tag = "Key_RF_UserName";
            this.lblPWd.Tag = "Key_PasswordColon";
            this.lblEnableSSL.Tag = "Key_EnableSSL";
            this.btnSaveMailInfo.Tag = "Key_SaveCaption";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<AlertLink> dList = new List<AlertLink>();
            
            StringBuilder items = new StringBuilder();
            try
            {
                if (ValidateControls(ref ErrorMsg))
                {
                    var tCollection = tvAlertTypes.Nodes[0].Nodes.OfType<TreeNode>().Where(item => (item.Checked == true)).ToList();

                    lEntities = new List<EmailAlertEntity>();
                    foreach (TreeNode node in tCollection)
                    {
                        EmailAlertEntity entity = new EmailAlertEntity();
                        entity.AlertTypeId = Convert.ToInt32(node.Tag);

                        entity.Subject = txtSubject.Text;
                        entity.ToMail = SubscribersList(dgvSubscribers);
                        entity.CCMail = SubscribersList(dgvCCSubscribers);
                        entity.BCCMail = SubscribersList(dgvBccSubscribers);
                        entity.FromMail = txtSender.Text;

                        entity.ToMail.Split(';').ToList().ForEach(item => {if (!(string.IsNullOrEmpty(item))) 
                            dList.Add(new AlertLink() { AlertTypeID = entity.AlertTypeId, SubscriberID = item });});

                        lEntities.Add(entity);
                    }
                    tvAlertTypes.Nodes[0].Nodes.OfType<TreeNode>().Where(item =>
                          (item.Checked == true)).ToList().ForEach(item => { items.Append(item.Text); items.Append(","); });

                    _alertbiz.SaveEmailAlerts(lEntities, dList, AppGlobals.Current.UserName, AppGlobals.Current.UserId, string.Empty, "Subject: " + txtSubject.Text
                        + "ToMail: " + SubscribersList(dgvSubscribers)
                      + "Alert Type: " + items.ToString());
                    DisplayMsg = this.GetResourceTextByKey(1, "MSG_EMAILSUBSCRIBER_SAVEDETAILS");
                    Win32Extensions.ShowInfoMessageBox(this, DisplayMsg, this.Text);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    
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
                if (row.Cells[0].Value == null ||
                    row.Cells[1].Value == null || row.Cells[1].EditedFormattedValue.ToString() == string.Empty) continue;
                if (Convert.ToBoolean(row.Cells[0].Value))
                {
                    sSubscribers.Append(row.Cells[1].Value.ToString());
                    sSubscribers.Append(";");
                }
            }
            return sSubscribers.ToString();
        }

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
                    serverinfo.Password = SiteLicensingCryptoHelper.Encrypt(txtPassword.Text, "B411y51T");
                    serverinfo.EnableSSL = chkEnableSSL.Checked ? true : false;

                    _alertbiz.SaveMailServerInfo(serverinfo, AppGlobals.Current.UserName, AppGlobals.Current.UserId, string.Empty, "Server: " + txtServerName.Text 
                        + "Port: " + txtPort.Text
                      + "UserID : " + txtUserID.Text);
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ALERT_MAILSERVERINFO"), this.Text);
                }

                txtSender.Text = txtUserID.Text;
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

        private void ClearControls()
        {
            txtSubject.Text = string.Empty;
            txtSender.Text = string.Empty;
            dgvSubscribers.Rows.Clear();
            dgvCCSubscribers.Rows.Clear();
            dgvBccSubscribers.Rows.Clear();
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
                //Create Root Node.
                TreeNode node = new TreeNode("AlertTypes");
                tvAlertTypes.Nodes.Add(node);
                TreeNode node1;
                _AlertTypes.ForEach(alert =>
                {
                    node1 = new TreeNode(alert.AlertTypeName);
                    node1.Tag = alert.AlertTypeID;
                    tvAlertTypes.Nodes[0].Nodes.Add(node1);
                });
                this.tvAlertTypes.ExpandAll();

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
            try
            {

                List<EmailAlertEntity> lstSubscribers = null;
                lstSubscribers = _alertbiz.GetEmailSubscribers(tvAlertTypes.SelectedNode != null ?
                    tvAlertTypes.SelectedNode.Text : string.Empty);
                ClearControls();
                txtSender.Text = _userId;
                foreach (EmailAlertEntity emailEntity in lstSubscribers)
                {
                    tvAlertTypes.Nodes[0].Nodes.OfType<TreeNode>().ForEachItem(item =>
                        {
                            if (item.Text.ToUpper().Trim() == emailEntity.AlertTypeName.ToUpper().Trim())
                                item.Checked = true;
                        });

                    //if (string.IsNullOrEmpty(txtSubject.Text))
                    //{
                        txtSubject.Text = emailEntity.Subject;
                        MapSubscribers(emailEntity.ToMail, dgvSubscribers);
                        MapSubscribers(emailEntity.CCMail, dgvCCSubscribers);
                        MapSubscribers(emailEntity.BCCMail, dgvBccSubscribers);
                   // }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
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
            view.DataSource = null;
            DataGridViewRow row = null;
            DataGridViewTextBoxCell cell1 = null;
            DataGridViewCheckBoxCell chkCell = null;
            string[] sSub = strSubscribers.Split(';');

         
            var sDistictRows = sSub.Distinct();

            foreach (string str in sDistictRows)
            {
                bool IsExists = false;
                if (string.IsNullOrEmpty(str)) continue;
                row = new DataGridViewRow();
                cell1 = new DataGridViewTextBoxCell();
                chkCell = new DataGridViewCheckBoxCell();
                cell1.Value = str;

                row.Cells.Add(chkCell);
                row.Cells.Add(cell1);

                view.Rows.Cast<DataGridViewRow>().TakeWhile(child => child.Cells[1].EditedFormattedValue.ToString().
                    SequenceEqual(cell1.Value.ToString()))
                    .ToList().ForEach(
                    child => IsExists = true);

                if (!IsExists)
                    view.Rows.Add(row);
                chkCell.Value = true;
            }
        }


        /// <summary>
        /// load the details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMailAlertConfiguration_Load(object sender, EventArgs e)
        {
            try
            {
                this.ResolveResources();
                ClearControls();

                LoadMailServerInfo();
                LoadInitialDetails();
               
                LoadEmailSubscribersDetails();

              
                this.txtUserID.Enabled = false;
                this.txtPassword.Enabled = false;

                CheckCredentials();
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
                var query =
                tvAlertTypes.Nodes[0].Nodes.OfType<TreeNode>().Where(node => node.Checked == true);

                if (query == null)
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_ENT_ALERTTYPE_SELECT");
                    Win32Extensions.ShowErrorMessageBox(this, ErrorMsg, this.Text);
                    tvAlertTypes.Focus();
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
                else if (dgvSubscribers.Rows.Count == 1 &&
                    (dgvSubscribers.Rows[0].Cells[1].Value == null || string.IsNullOrEmpty(dgvSubscribers.Rows[0].Cells[1].Value.ToString())))
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_ENT_TOLIST");
                    Win32Extensions.ShowErrorMessageBox(this, ErrorMsg, this.Text);
                    dgvSubscribers.Focus();
                    return false;
                }
                else if (dgvSubscribers.Rows.Count > 0)
                {
                    retVal = setValidation(dgvSubscribers);
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

                if (row.Cells[1].Value != null)
                {
                    if (!string.IsNullOrEmpty(row.Cells[1].Value.ToString()))
                    {
                        if (!ValidateEmail(row.Cells[1].Value.ToString()))
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
                else if (chkEnableSSL.Checked && (string.IsNullOrEmpty(txtUserID.Text)))
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



        public void Execute()
        {
            throw new NotImplementedException();
        }

        public List<AlertAuditEntity> ExecuteAlerts()
        {
            throw new NotImplementedException();
        }

        public void UnExecute()
        {
            throw new NotImplementedException();
        }

    }
}
