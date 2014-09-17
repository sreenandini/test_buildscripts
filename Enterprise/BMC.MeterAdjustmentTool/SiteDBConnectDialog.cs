using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.MeterAdjustmentTool.Helpers;
using System.Data.Linq;
using BMC.Common.ExceptionManagement;
using System.Data.SqlClient;
using BMC.Common;

namespace BMC.MeterAdjustmentTool
{
    public partial class SiteDBConnectDialog : DialogFormBase
    {
        private rsp_GetSiteDetailsResult _selectedSite = null;
        private BmcConnectionStringBuilder _connectionStringBuilder = null;
        private string _connectionString = null;
        private int? _siteId = null;                
        private string _siteCode = null;
        private string _siteName = null;
        private int _securityUserID = 0;

        public rsp_GetSiteDetailsResult SelectedSite
        {
            get { return _selectedSite; }
            set { _selectedSite = value; }
        }

        public BmcConnectionStringBuilder ConnectionStringBuilder
        {
            get { return _connectionStringBuilder; }
            set { _connectionStringBuilder = value; }
        }

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public int? SiteId
        {
            get { return _siteId; }
            set { _siteId = value; }
        }

        public string SiteCode
        {
            get { return _siteCode; }
            set { _siteCode = value; }
        }

        public string SiteName
        {
            get { return _siteName; }
            set { _siteName = value; }
        }

        public int SecurityUserID
        {
            get { return _securityUserID; }
            set { _securityUserID = value; }
        }

        public SiteDBConnectDialog()
        {
            InitializeComponent();
            _connectionStringBuilder = new BmcConnectionStringBuilder();

            // Set Tags for controls
            SetTagProperty();
        }

        protected override void LoadChanges()
        {
            base.LoadChanges();

            // For externalization
            this.ResolveResources();

            this.FillSites();
            this.FillServers();            
        }

        private void SetTagProperty()
        {
            this.Tag = "Key_ConnecttoSite";
            this.lblSite.Tag = "Key_SiteCaptionColon";
            this.lblUserName.Tag = "Key_UsrNameColon";
            this.lblDatabaseServer.Tag = "Key_DatabaseServerColon";
            this.lblPassword.Tag = "Key_PasswordCaptionColon";           
            this.lblConnectionTimeout.Tag = "Key_TimeoutColon";
            this.btnConnect.Tag = "Key_Connect";
            this.btnCancel.Tag = "Key_CancelCaption";            
            this.chkSaveDetails.Tag = "Key_SaveDetails";
        }

        private void FillSites()
        {
            try
            {
                cboSites.Items.Clear();
                cboSites.DisplayMember = "Text";
                cboSites.ValueMember = "Value";

                using (EnterpriseDataContextHelper db = new EnterpriseDataContextHelper())
                {
                    ISingleResult<rsp_GetSiteDetailsResult> sites = db.FuncGetSiteDetails(null, null,SecurityUserID);

                    if (sites != null)
                    {
                        foreach (rsp_GetSiteDetailsResult site in sites)
                        {
                            ComboBoxItem<rsp_GetSiteDetailsResult> site2 = new ComboBoxItem<rsp_GetSiteDetailsResult>(
                                site, site.Site_Name);
                            cboSites.Items.Add(site2);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(SiteName))
                    cboSites.SelectedText = SiteName;
                else
                    cboSites.SelectedIndex = -1;            
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {

            }
        }

        private void FillServers()
        {
            try
            {
                cboDatabaseServer.Items.Clear();
                cboDatabaseServer.DisplayMember = "Text";
                cboDatabaseServer.ValueMember = "Value";

                AutoCompleteStringCollection servers = DBServerCollection.GetServers();
                if (servers != null)
                {
                    foreach (string server in servers)
                    {
                        ComboBoxItem<string> site2 = new ComboBoxItem<string>(
                            server, server);
                        cboDatabaseServer.Items.Add(site2);
                    }
                }
                cboDatabaseServer.AutoCompleteCustomSource = servers;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {

            }
        }

        private void LoadSiteDetails(rsp_GetSiteDetailsResult site)
        {
            BmcConnectionStringBuilder connString = site.GetDecryptedConnectionString();            
            cboDatabaseServer.SelectedIndex = -1;

          
            if (chkSaveDetails.Checked)
            {
                if (!connString.Server.IsEmpty())
                {
                    DBServerCollection.AddServer(connString.Server);                    
                }
                this.FillServers();
                cboDatabaseServer.SelectComboBoxItem<string>(connString.Server);             
                txtUserName.Text = connString.UserID;
                txtPassword.Text = connString.Password;
                updConenctionTimeout.Value = connString.Timeout;
            }
            else
            {
                this.FillServers();
                txtUserName.Text = "";
                txtPassword.Text = "";
                updConenctionTimeout.Value = 30;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedSite = null;
            ComboBoxItem<rsp_GetSiteDetailsResult> selItem = cboSites.SelectedItem as ComboBoxItem<rsp_GetSiteDetailsResult>;
            if (selItem != null)
            {
                _selectedSite = selItem.Value;
                if (_selectedSite != null)
                {
                    this.LoadSiteDetails(_selectedSite);
                }
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            this.OnAcceptButtonClicked();
        }

        public override bool ValidateUI()
        {
            bool result = default(bool);
            SqlConnection connection = null;

            try
            {
                _connectionStringBuilder.UserID = txtUserName.Text.Trim();
                _connectionStringBuilder.Password = txtPassword.Text.Trim();

                //if (cboSites.SelectedIndex == -1)
                //{
                //    this.ShowInfoMessageBox("Please select a site.");
                //    cboSites.Focus();
                //    return false;
                //}
                if (cboDatabaseServer.Text.Trim().IsEmpty())
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_MAT_DBSERVERVALUE"));        //"Please enter the database server.");
                    cboDatabaseServer.Focus();
                    return false;
                }
                else if (_connectionStringBuilder.UserID.IsEmpty())
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_MAT_VALIDUSERNAME"));        //"Please enter the valid user name.");
                    txtUserName.Focus();
                    return false;
                }
                else if (_connectionStringBuilder.Password.IsEmpty())
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_MAT_VALIDPASSWORD"));        //"Please enter the valid password.");
                    txtPassword.Focus();
                    return false;
                }

                if (cboDatabaseServer.SelectedItem != null)
                {
                    _connectionStringBuilder.Server = ((ComboBoxItem<string>)cboDatabaseServer.SelectedItem).Text;
                }
                else
                {
                    _connectionStringBuilder.Server = cboDatabaseServer.Text.Trim();
                }

                _connectionStringBuilder.Timeout = Convert.ToInt32(updConenctionTimeout.Value);
                this.ConnectionString = _connectionStringBuilder.GetConnectionString();
                connection = new SqlConnection();
                connection.ConnectionString = this.ConnectionString;

                try
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        bool siteCodeMatched = false;
                        string availSite = string.Empty;

                        try
                        {
                            SqlCommand cmd = new SqlCommand("SELECT TOP 1 Code FROM dbo.SITE", connection);
                            object siteCode = cmd.ExecuteScalar();
                            if (siteCode != null)
                            {
                                availSite = siteCode.ToString();
                                siteCodeMatched = (string.Compare(SiteCode, availSite, true) == 0);
                            }
                        }
                        catch { }

                        if (siteCodeMatched)
                        {
                            result = true;
                        }
                        else
                        {
                            this.ShowWarningMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_MAT_SITECODEMISMATCH"), _selectedSite.Site_code, availSite));
                            // "Selected site's code ({0}) does not match with the actual site's code ({1})."
                        }
                    }
                    else
                    {
                        this.ShowErrorMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_MAT_CANNOTCONNECTSERVER"), _connectionStringBuilder.Server));
                        // "Unable to connect to the server "
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    this.ShowErrorMessageBox(this.GetResourceTextByKey(1, "MSG_MAT_CANNOTCONNECTERROR") + ex.Message);
                    // "Unable to connect : "
                }                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                this.ShowErrorMessageBox(this.GetResourceTextByKey(1, "MSG_MAT_CANNOTCONNECTERROR") + ex.Message);
                // "Unable to connect : "
            }
            finally
            {
                if (connection != null)
                {
                    try
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                }
            }

            return result;
        }

        protected override bool SaveChanges()
        {
            if (chkSaveDetails.Checked)
            {
                using (EnterpriseDataContextHelper dbContext = new EnterpriseDataContextHelper())
                {
                    dbContext.FuncUpdateSiteDBConnectionString(SiteId, _connectionStringBuilder.GetEncryptedConnectionString());
                }
            }
            return base.SaveChanges();
        }

        protected override void OnBeforeFormClosing(CloseReason reason)
        {

        }

        private void cboDatabaseServer_KeyDown(object sender, KeyEventArgs e)
        {
            if ((sender as ComboBox).DroppedDown)
                (sender as ComboBox).DroppedDown = false;
        }

        private void cboSites_KeyDown(object sender, KeyEventArgs e)
        {
            if ((sender as ComboBox).DroppedDown)
                (sender as ComboBox).DroppedDown = false;
        }
    }
}
