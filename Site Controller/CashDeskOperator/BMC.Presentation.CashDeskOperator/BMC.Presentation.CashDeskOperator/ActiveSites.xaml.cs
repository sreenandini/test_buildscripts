using System.Windows;
using BMC.Common.ExceptionManagement;
using System.Data;
using System;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.LogManagement;
using BMC.Transport;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation
{
    public partial class ActiveSites : IDisposable
    {
        #region Private Variables

        private string _transiteSiteCode = string.Empty;

        #endregion Private Variables

        #region Constructor

        public ActiveSites()
        {
            InitializeComponent();          
        }

        #endregion

        #region Public Constructor

        public string TransiteSiteCode
        {
            get { return _transiteSiteCode; }
            set { _transiteSiteCode = value; }
        }

        #endregion Public Constructor

        #region Events

        private void ActiveSites_Loaded(object sender, RoutedEventArgs e)
        {
            Cursor = System.Windows.Input.Cursors.Wait;

            ShowActiveSites();

            Cursor = System.Windows.Input.Cursors.Arrow;
        }

        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            TransiteSiteCode = cmbActiveSites.SelectedValue.ToString();
            this.DialogResult = true;
            Hide();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        #endregion 

        #region Private Methods

        private void ShowActiveSites()
        {
            try
            {
                LogManager.WriteLog("Inside ShowActiveSites method", LogManager.enumLogLevel.Info);

                DataTable SiteTable                 =   GetSiteDetails();

                LogManager.WriteLog("Binding Active Site Details...", LogManager.enumLogLevel.Info);

                cmbActiveSites.DataContext          =   SiteTable;
                cmbActiveSites.ItemsSource          =   ((System.ComponentModel.IListSource)SiteTable).GetList();
                cmbActiveSites.DataContext          =   SiteTable.DefaultView;
                cmbActiveSites.DisplayMemberPath    =   "SiteName";
                cmbActiveSites.SelectedValuePath    =   "SiteCode";

                cmbActiveSites.SelectedIndex        =   0;

                LogManager.WriteLog("Active Site Details bound successfully.", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private DataTable GetSiteDetails()
        {
            DataTable SiteTable = new DataTable();

            try
            {
                LogManager.WriteLog("Inside GetSiteDetails method", LogManager.enumLogLevel.Info);

                DataTable SiteDataTable         =   GetActiveSiteDetailsFromEnterprise();

                LogManager.WriteLog(string.Format("{0} - {1}", "Active Site details fetched successfuuly from Enterprise. Active Site Count", 
                    SiteDataTable.Rows.Count), LogManager.enumLogLevel.Info);

                SiteTable.Columns.Add("SiteCode");
                SiteTable.Columns.Add("SiteName");

                DataRow siteRow = SiteTable.NewRow();
                siteRow["SiteCode"] = "Select";
                siteRow["SiteName"] = "--Select--";

                SiteTable.Rows.Add(siteRow);

                if (SiteDataTable != null)
                {
                    foreach (DataRow siteDataRow in SiteDataTable.Rows)
                    {
                        if (siteDataRow["Site_Code"].ToString() == Settings.SiteCode)
                            continue;

                        DataRow dataRow         =   SiteTable.NewRow();

                        dataRow["SiteCode"]     =   siteDataRow["Site_Code"];
                        dataRow["SiteName"]     =   string.Format("{0},{1}", siteDataRow["Site_Name"], siteDataRow["Site_Code"]);                        

                        SiteTable.Rows.Add(dataRow);
                    }
                }               
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);                
            }

            return SiteTable;
        }

        private DataTable GetActiveSiteDetailsFromEnterprise()
        {
            try
            {
                LogManager.WriteLog("Inside GetActiveSiteDetailsFromEnterprise method", LogManager.enumLogLevel.Info);

                return EnrollmentBusinessObject.CreateInstance().GetActiveSiteDetails(); ;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        #endregion                

        #region IDisposable Members

        /// <summary>
        /// Variable used to identity whether this object is already disposed or not.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.CleanupWPFObjectTopControls((i) =>
                    {
                        // events
                        this.ActiveSite.Loaded -= (this.ActiveSites_Loaded);
                        this.btnOK.Click -= (this.OK_Button_Click);
                        this.btnCancel.Click -= (this.Cancel_Button_Click);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("ActiveSites objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="ActiveSites"/> is reclaimed by garbage collection.
        /// </summary>
        ~ActiveSites()
        {
            Dispose(false);
        }

        #endregion
    }
}