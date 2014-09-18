using System.Windows;
using BMC.Common.ExceptionManagement;
using System.Data;
using System;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.LogManagement;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation
{
    public partial class CTransitAsset
    {
        #region Private Variables

        private string _transitAssetNo = string.Empty;

        #endregion Private Variables

        #region Constructor

        public CTransitAsset()
        {
            InitializeComponent();            
        }

        #endregion

        #region Public Constructor

        public string TransitAssetNo
        {
            get { return _transitAssetNo; }
            set { _transitAssetNo = value; }
        }

        #endregion Public Constructor

        #region Events

        private void TransitAsset_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Cursor = System.Windows.Input.Cursors.Wait;

                ShowTransitAsset();
            }
            finally
            {
                Cursor = System.Windows.Input.Cursors.Arrow;
            }
        }

        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            TransitAssetNo = cmbTransitAsset.SelectedValue.ToString() == "Select" ? string.Empty : cmbTransitAsset.SelectedValue.ToString();
            Hide();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion 

        #region Private Methods

        private void ShowTransitAsset()
        {
            try
            {
                LogManager.WriteLog("Inside ShowTransitAsset method", LogManager.enumLogLevel.Info);

                DataTable TransitAssetTable = GetTransitAssetDetails();

                if (TransitAssetTable.Rows.Count <= 1)
                {
                    MessageBox.ShowBox("MessageID343", BMC_Icon.Information);
                    Hide();
                }

                LogManager.WriteLog("Binding Transit Asset Details...", LogManager.enumLogLevel.Info);

                cmbTransitAsset.DataContext         =   TransitAssetTable;
                cmbTransitAsset.ItemsSource         =   ((System.ComponentModel.IListSource)TransitAssetTable).GetList();
                cmbTransitAsset.DataContext         =   TransitAssetTable.DefaultView;
                cmbTransitAsset.DisplayMemberPath   =   "Asset";
                cmbTransitAsset.SelectedValuePath   =   "AssetNo";

                cmbTransitAsset.SelectedIndex = 0;

                LogManager.WriteLog("Transit Asset Details bound successfully.", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private DataTable GetTransitAssetDetails()
        {
            DataTable TransitAssetTable = new DataTable();

            try
            {
                LogManager.WriteLog("Inside GetTransitAssetDetails method", LogManager.enumLogLevel.Info);

                DataTable TransitAssetDataTable     =   GetTransitAssetDetailsFromEnterprise();

                LogManager.WriteLog(string.Format("{0} - {1}", "Transit Asset details fetched successfuuly from Enterprise. Transit Asset Count",
                    TransitAssetDataTable.Rows.Count), LogManager.enumLogLevel.Info);

                TransitAssetTable.Columns.Add("AssetNo");
                TransitAssetTable.Columns.Add("Asset");

                DataRow assetRow        =   TransitAssetTable.NewRow();
                assetRow["AssetNo"]     =   "Select";
                assetRow["Asset"]     =   "--Select--";

                TransitAssetTable.Rows.Add(assetRow);

                if (TransitAssetDataTable != null)
                {
                    foreach (DataRow assetDataRow in TransitAssetDataTable.Rows)
                    {
                        DataRow dataRow = TransitAssetTable.NewRow();

                        dataRow["AssetNo"] = assetDataRow["AssetNo"];
                        dataRow["Asset"] = assetDataRow["AssetNo"];

                        TransitAssetTable.Rows.Add(dataRow);
                    }
                }               
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);                
            }

            return TransitAssetTable;
        }

        private DataTable GetTransitAssetDetailsFromEnterprise()
        {
            try
            {
                LogManager.WriteLog("Inside GetTransitAssetDetailsFromEnterprise method", LogManager.enumLogLevel.Info);

                return EnrollmentBusinessObject.CreateInstance().GetInTransitAsset();
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
                        this.TransitAsset.Loaded -= (this.TransitAsset_Loaded);
                        this.btnOK.Click -= (this.OK_Button_Click);
                        this.btnCancel.Click -= (this.Cancel_Button_Click);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("CTransitAsset objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CTransitAsset"/> is reclaimed by garbage collection.
        /// </summary>
        ~CTransitAsset()
        {
            Dispose(false);
        }

        #endregion
    }
}