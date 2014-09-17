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
using BMC.CoreLib.Win32;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class ucReadBasedLiquidation : UserControl
    {
        #region Data Members

        private ReadBasedLiquidationBiz objReadBasedLiquidationBiz = new ReadBasedLiquidationBiz();
        string cmbDefaultText = string.Empty;
        #endregion //Data Members

        #region Properties

        public DataGridView ReadDataGrid
        {
            get
            {
                return dtgvReadData;
            }
        }

        public ComboBox SiteCombo
        {
            get
            {
                return cboSite;
            }
        }

        public int ReadDataListCount
        {
            get
            {
                if (dtgvReadData.Rows == null) return 0;
                return dtgvReadData.Rows.Count;
            }
        }

        public int SiteId
        {
            get
            {
                return Convert.ToInt32(cboSite.SelectedValue);
            }
        }

        #endregion //Properties

        #region Constructor

        public ucReadBasedLiquidation()
        {
            InitializeComponent();

            // Set Tags for controls
            SetTagProperty();
        }

        private void SetTagProperty()
        {
            lblSite.Tag = "Key_SiteMandatory";
            cmbDefaultText = this.GetResourceTextByKey(1, "MSG_SITE_SELECT");
        }
        #endregion //Constructor

        #region Events

        private void ucReadBasedLiquidation_Load(object sender, EventArgs e)
        {
            // For externalization
            this.ResolveResources();

            LoadSites();
            FillData();
        }

        private void cboSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillData();
        }

        private void dtgvReadData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowLiquidationDetails(e.RowIndex, e.ColumnIndex);
        }

        #endregion //Events

        #region Methods

        private void LoadSites()
        {
            try
            {
                cboSite.DisplayMember = "DisplayName";
                cboSite.ValueMember = "Site_ID";
                cboSite.DataSource = new BindingSource(objReadBasedLiquidationBiz.GetActiveSiteDetails(AppGlobals.Current.LoggedinUser.SecurityUserID , cmbDefaultText), null);
                cboSite.SelectedItem = 0;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void FillData()
        {
            try
            {
                dtgvReadData.DataSource = objReadBasedLiquidationBiz.GetReadLiquidationRecords(SiteId);
                FormatReadDataGrid();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void ShowLiquidationDetails(int iRowIndex, int iColumnIndex)
        {
            try
            {
                if (iRowIndex == -1)
                {
                    if (iColumnIndex != -1)
                        return;
                }

                if (ReadDataListCount <= 0)
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_NO_READRECORDS"), this.ParentForm.Text);  // "There are no Read records found."
                    return;
                }

                if (ReadDataGrid.SelectedRows.Count <= 0)
                {
                    Win32Extensions.ShowWarningMessageBox(this, this.GetResourceTextByKey(1, "MSG_SELECT_ONEROW"), this.ParentForm.Text);           // "You must select atleast one row."
                    return;
                }

                List<DateTime> lstReadDate = ReadDataGrid.SelectedRows.Cast<DataGridViewRow>().Select(item => Convert.ToDateTime(item.Cells["Read_Date"].Value)).ToList();
                frmReadLiquidationDetails objReadLiquidationDetails = new frmReadLiquidationDetails(SiteId, lstReadDate.Min(), lstReadDate.Max());
                objReadLiquidationDetails.ShowDialog(this);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public int GetReadDataListCount()
        {
            return dtgvReadData.Rows.Count;
        }

        public int GetSiteId()
        {
            return Convert.ToInt32(cboSite.SelectedValue);
        }

        private void FormatReadDataGrid()
        {
            try
            {
                dtgvReadData.Columns["Read_No"].Visible = false;

                dtgvReadData.Columns["Read_Date"].Tag = "Key_ReadDate";    // "Read Date";
                dtgvReadData.Columns["CashIn"].Tag = "Key_CashIn";    //"Cash In";
                dtgvReadData.Columns["CashOut"].Tag = "Key_CashOut";    //"Cash Out";
                dtgvReadData.Columns["CashTake"].Tag = "Key_CashTake";    //"Cash Take";
                dtgvReadData.Columns["Total_Coins_In"].Tag = "Key_TotalCoinsIn";    //"Total Coins In";
                dtgvReadData.Columns["Total_Coins_In"].Width = 120;
                dtgvReadData.Columns["Handpay"].Tag = "Key_Handpay";
                dtgvReadData.Columns["Tickets_In"].Tag = "Key_VouchersIn";    //"Vouchers In";
                dtgvReadData.Columns["Tickets_Out"].Tag = "Key_VouchersOut";    //"Vouchers Out";

                // Resolve once the tags are set
                dtgvReadData.ResolveResources();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion //Methods

    }
}
