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
using BMC.BusinessClasses.Proxy;
using BMC.Common.LogManagement;
using BMC.Common;

namespace BMC.MeterAdjustmentTool
{
    public partial class SqlConnectDialog : DialogFormBase
    {
        private rsp_GetSiteDetailsResult _selectedSite = null;
        private BmcConnectionStringBuilder _connectionStringBuilder = null;
        private string _connectionString = null;
        private bool isFromProxy = false;
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

        public SqlConnectDialog(int securityUserID)
        {
            InitializeComponent();
            _connectionStringBuilder = new BmcConnectionStringBuilder();
			this._securityUserID = securityUserID;
            // Set Tags for controls
            SetTagProperty();
        }

        protected override void LoadChanges()
        {
            base.LoadChanges();

            // For externalization
            this.ResolveResources();

            this.FillSites();
        }

        private void SetTagProperty()
        {
            this.Tag = "Key_ConnecttoSite";
            this.lblSite.Tag = "Key_SiteCaptionColon";
            this.btnConnect.Tag = "Key_Connect";
            this.btnCancel.Tag = "Key_CancelCaption";
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
                    ISingleResult<rsp_GetSiteDetailsResult> sites = db.FuncGetSiteDetails(null, null, _securityUserID);

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
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {

            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboSites_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
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
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }
        }

        private void LoadSiteDetails(rsp_GetSiteDetailsResult site)
        {
            BmcConnectionStringBuilder connString = site.GetDecryptedConnectionString();
            if (connString.UserID != "")
            {
                _connectionStringBuilder = connString;
                _connectionString = connString.GetConnectionString();
            }
            else
            {
                _connectionStringBuilder = null;
                _connectionString = "";
            }

        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                this.OnAcceptButtonClicked();

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }

        }

        public override bool ValidateUI()
        {
            bool result = default(bool);
            SqlConnection connection = null;

            try
            {

                if (cboSites.SelectedIndex == -1)
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SL_SELECT_SITE"));        //"Please select a site.");
                    cboSites.Focus();
                    return false;
                }
            z:
                if (_connectionString == string.Empty)
                {
                    isFromProxy = true;

                    if (!this.SelectedSite.WEBUrl.Contains(MeterGlobals.ExchangeWebUrl114))
                    {
                        ConnectExchangeProxy();
                    }
                    else
                    {
                        result = true;
                        return result;
                    }
                }
                connection = new SqlConnection();
                connection.ConnectionString = _connectionString;

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
                                siteCodeMatched = (string.Compare(_selectedSite.Site_code, availSite, true) == 0);
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
                        _connectionString = "";
                        _connectionStringBuilder = null;
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                   
                    LogManager.WriteLog("Unable to connect : " + ex.Message, LogManager.enumLogLevel.Debug);
                    _connectionString = "";
                    _connectionStringBuilder = null;
                    if (!isFromProxy)
                    {                      
                        goto z;
                    }
                    else
                    {
                        this.ShowErrorMessageBox(this.GetResourceTextByKey(1, "MSG_MAT_CANNOTCONNECT"));    //"Unable to connect");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                this.ShowErrorMessageBox(this.GetResourceTextByKey(1, "MSG_MAT_CANNOTCONNECT"));           //"Unable to connect");
                LogManager.WriteLog("Unable to connect : " + ex.Message, LogManager.enumLogLevel.Debug);
                _connectionString = "";
                _connectionStringBuilder = null;
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

        void ConnectExchangeProxy()
        {
            using (Proxy p_exc = new Proxy(SelectedSite.Site_code))
            {
                _connectionString = BMC.Common.Security.CryptEncode.Decrypt(p_exc.GetSiteConnectionString());
                SqlConnectionStringBuilder sb_con = new SqlConnectionStringBuilder(_connectionString);
                _connectionStringBuilder = new BmcConnectionStringBuilder
                {
                    // InstanceName=    sb_con;
                    Password = sb_con.Password,
                    UserID = sb_con.UserID,
                    Server = sb_con.DataSource,
                    Timeout = sb_con.ConnectTimeout

                };
            }

        }
        protected override bool SaveChanges()
        {
            if (!this.SelectedSite.WEBUrl.Contains(MeterGlobals.ExchangeWebUrl114))
            {
                using (EnterpriseDataContextHelper dbContext = new EnterpriseDataContextHelper())
                {
                    dbContext.FuncUpdateSiteDBConnectionString(this.SelectedSite.Site_ID, _connectionStringBuilder.GetEncryptedConnectionString());
                }
                return base.SaveChanges();
            }
            return true;
        }

        protected override void OnBeforeFormClosing(CloseReason reason)
        {

        }

        private void cboSites_KeyDown(object sender, KeyEventArgs e)
        {
            if ((sender as ComboBox).DroppedDown)
                (sender as ComboBox).DroppedDown = false;
        }
    }
}
