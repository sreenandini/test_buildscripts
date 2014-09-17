using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business;
using BMC.Common.ExceptionManagement;
using System.Linq.Expressions;
using BMC.Common.Utilities;
using BMC.Common;
using BMC.EnterpriseClient.Helpers;

namespace BMC.EnterpriseClient.Views
{

    public partial class ucAlerts : UserControl
    {

        AlertSystemBiz _alertBiz = null;
        OnLoadInitialDetails loadDetails;
        string DisplayMsg = string.Empty;
        List<AlertAuditEntity> lstItems = null;
        private int PageSize = 10;
        private int CurrentPageIndex = 0;
        private int TotalPage = 0; 

        System.Timers.Timer timer1 = null;
        public ucAlerts()
        {
            try
            {
                InitializeComponent();
                this.Load += ucAlerts_Load;
                this.dgvAlerts.RowHeaderMouseClick += dgvAlerts_RowHeaderMouseClick;
                this.dgvAlerts.RowsAdded += dgvAlerts_RowsAdded;
                this.chkAutoRefresh.CheckedChanged += chkAutoRefresh_CheckedChanged;
                this.chkCancelPendingEmails.CheckedChanged += chkCancelPendingEmails_CheckedChanged;
                setTagProperty();
                timer1 = new System.Timers.Timer(6000);
                timer1.Elapsed += timer1_Elapsed;
                _alertBiz = AlertSystemBiz.CreateInstance();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

      
        /// <summary>
        /// calculate teh total number of pages.
        /// </summary>
        private void CalculateTotalPages()
        {

            try
            {
                int rowCount = lstItems.Count;
                if (rowCount > 0) CurrentPageIndex = 1;
                TotalPage = rowCount / PageSize;
                // if any row left after calculated pages, add one more page 
                if (rowCount % PageSize > 0)
                    TotalPage += 1;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// on load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucAlerts_Load(object sender, EventArgs e)
        {
            try
            {
                this.ResolveResources();
                LoadDetails();
                this.chkCancelPendingEmails.Enabled = Convert.ToBoolean(_alertBiz.GetSetting("CancelPendingMails"));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void setTagProperty()
        {
            try
            {
                this.lblSite.Tag = "Key_ChooseaSiteColon";
                this.lblAlert.Tag = "Key_AlertType";
                this.btnLoad.Tag = "Key_Load";
                this.Tag = "KEY_ALERT";
                this.chkAutoRefresh.Tag = "Key_AutoUpdate";
                this.grpSubscribers.Tag = "Key_EmailSubscribers";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadDetails()
        {
            //set properties for the listivew
            try
            {
                lstESubscribers.ShowGroups = true;
                lstESubscribers.Columns.Add("", 750);
                lstESubscribers.View = View.Details;


                //invoke the delegate for loading initial details.
                loadDetails += LoadInitialDetails;
                loadDetails();
                //end the invoke.
                loadDetails -= LoadInitialDetails;

                //invoke the delegate for loading subscribers
                loadDetails += LoadSubscribers;
                loadDetails();
                //end the invoke.
                loadDetails -= LoadSubscribers;



                //invoke the delegate for loading sites.
                loadDetails += LoadSites;
                loadDetails();
                //end the invoke.etails();
                loadDetails -= LoadSites;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// load teh site details
        /// </summary>
        private void LoadSites()
        {
            try
            {
                List<SiteEntity> sites = _alertBiz.GetSites(AppGlobals.Current.UserId);
                cboSites.DataSource = sites;
                cboSites.DisplayMember = "SiteName";
                cboSites.ValueMember = "SiteID";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }


        /// <summary>
        /// load the subscribers.
        /// </summary>
        private void LoadSubscribers()
        {
            if (Convert.ToBoolean(_alertBiz.GetSetting("IsEmailAlertEnabled")))
            {
                List<EmailAlertEntity> sEntity = _alertBiz.GetEmailSubscribers((cboAlertTypes.SelectedItem as AlertTypes).AlertTypeName);


                ListViewGroupCollection lstCollection = lstESubscribers.Groups;

                //get the mail ID's
                foreach (EmailAlertEntity entity in sEntity)
                {
                    //TO Mail ID
                    foreach (string str in GetEmailIDs(entity.ToMail))
                    {
                        ListViewItem item = new ListViewItem(str);
                        item.Group = lstESubscribers.Groups["lstSubscribers"];
                        lstESubscribers.Items.Add(item);
                    }

                    //CC 
                    foreach (string str in GetEmailIDs(entity.CCMail))
                    {
                        ListViewItem item = new ListViewItem(str);
                        item.Group = lstESubscribers.Groups["lstCCSubscribers"];
                        lstESubscribers.Items.Add(item);
                    }

                    //BCC
                    foreach (string str in GetEmailIDs(entity.BCCMail))
                    {
                        ListViewItem item = new ListViewItem(str);
                        item.Group = lstESubscribers.Groups["lstBCCSubscribers"];
                        lstESubscribers.Items.Add(item);
                    }
                }
            }
        }


        /// <summary>
        /// separate the email ID's
        /// </summary>
        /// <param name="sEmailIDs"></param>
        /// <returns></returns>
        private List<string> GetEmailIDs(string sEmailIDs)
        {
            List<string> lstEmail = null;
            try
            {
                lstEmail = sEmailIDs.Split(';').AsEnumerable().ToList();
                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lstEmail;
        }

        /// <summary>
        /// Load teh alert types.
        /// </summary>
        private void LoadInitialDetails()
        {
            try
            {
                //get the list of supported alerts.
                List<AlertTypes> _AlertTypes = _alertBiz.GetAlertTypes();
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
        /// Load the Alert Details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnLoad_Click(object sender, EventArgs e)
        {

            try
            {
                LoadAlertData();
                this.dgvAlerts.ScrollBars = ScrollBars.Both;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        /// <summary>
        /// Load the alert data.
        /// </summary>
        private void LoadAlertData()
        {
            //MessageBox.Show(Math.Round(1.8, 0).ToString());
            try
            {
                SimpleRemoteControl remote = new SimpleRemoteControl();
                Alert alert = new Alert();

                int ID = cboAlertTypes.SelectedItem != null ? (cboAlertTypes.SelectedItem as AlertTypes).AlertTypeID : 0;
                string SiteCode = cboSites.SelectedItem != null ? (cboSites.SelectedItem as SiteEntity).SiteCode : string.Empty;

                var cAlert = new Command<Alert>(alert, null, l => l.GetAlertDetails(ID, SiteCode, (bool)chkShowProcessed.Checked));
                remote.SetCommand(cAlert);
                lstItems = remote.ButtonWasPressed();
                CalculateTotalPages();

                PopulateAlertDataOnPage(CurrentPageIndex);



                if (lstItems.Count == 0)
                {
                    DisplayMsg = this.GetResourceTextByKey(1, "MSG_NO_DATA");
                    Win32Extensions.ShowInfoMessageBox(this, DisplayMsg);
                    rtbEmailContent.Text = string.Empty;
                }
                dgvAlerts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                ControlState();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        
        /// <summary>
        /// hide the columns which are unnecessary
        /// </summary>
        private void ControlState()
        {
            try
            {
                dgvAlerts.Columns["Content"].Width = 0;
                dgvAlerts.Columns["Content"].DividerWidth = 0;
                dgvAlerts.Columns["Content"].Resizable = DataGridViewTriState.False;

                dgvAlerts.Columns["RowColor"].Width = 0;
                dgvAlerts.Columns["RowColor"].DividerWidth = 0;
                dgvAlerts.Columns["RowColor"].Resizable = DataGridViewTriState.False;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// start or stop the timer based on auto refresh.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAutoRefresh.Checked)
                {
                    timer1.Start();
                }
                else
                {
                    timer1.Stop();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// timer to load data on auto refresh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                LoadAlertData();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        /// <summary>
        /// populate the content on row select.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dgvAlerts_RowHeaderMouseClick(object sender, System.Windows.Forms.DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow row = dgvAlerts.Rows[e.RowIndex];

                if (row != null)
                {
                    rtbEmailContent.Text = row.Cells["Content"].FormattedValue.ToString();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// set background color for rows to highlight
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dgvAlerts_RowsAdded(object sender, System.Windows.Forms.DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                DataGridViewRow row = dgvAlerts.Rows[e.RowIndex];
                row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml(row.Cells["RowColor"].FormattedValue.ToString());
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// cancel sending of pending mails.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkCancelPendingEmails_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkCancelPendingEmails.Checked)
                {
                    if (dgvAlerts.Rows.Count > 0)
                    {
                        DisplayMsg = this.GetResourceTextByKey(1, "MSG_MAIL_CANCEL");
                        if (Win32Extensions.ShowQuestionMessageBox(this, DisplayMsg) == DialogResult.Yes)
                            _alertBiz.UpdateStatusForPending();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// move to the next page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_NextPage_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentPageIndex == TotalPage) { return; }
                else { CurrentPageIndex = CurrentPageIndex += 1; PopulateAlertDataOnPage(CurrentPageIndex); }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// move to the previous page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PreviousPage_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentPageIndex == 1) { return; }
                CurrentPageIndex = CurrentPageIndex - 1;
                PopulateAlertDataOnPage(CurrentPageIndex);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// move to the first page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_FirstPage_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentPageIndex == 1)
                    return;
                else
                {
                    CurrentPageIndex = 1;
                    PopulateAlertDataOnPage(CurrentPageIndex);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// move to the last page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_LastPage_Click(object sender, EventArgs e)
        {
            try
            {
                CurrentPageIndex = (int)TotalPage;
                PopulateAlertDataOnPage(CurrentPageIndex);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Populate datagrid based on teh page data.
        /// </summary>
        private void PopulateAlertDataOnPage(int _Currentpage)
        {
            try
            {
                dgvAlerts.DataSource = null;

                int startIndex = PageSize * (_Currentpage - 1);
                var result = lstItems.Where((s, k) => (k >= startIndex && k < (startIndex + PageSize))).ToList();
                dgvAlerts.DataSource = result;
                dgvAlerts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                ControlState();

                string title = this.GetResourceTextByKey(1, "MSG_MAIL_PAGING");
                txtCurrentPage.Text = string.Format(title, (dgvAlerts.Rows.Count > 0) ? _Currentpage : 0, TotalPage);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

    }
}
