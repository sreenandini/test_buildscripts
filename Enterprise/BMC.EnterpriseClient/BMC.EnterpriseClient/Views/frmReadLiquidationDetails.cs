using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseClient.Helpers;
using BMC.EnterpriseBusiness.Business;
using BMC.Common.ExceptionManagement;
using BMC.Common;
namespace BMC.EnterpriseClient.Views
{
    public partial class frmReadLiquidationDetails : GenericFormBase
    {
        #region DataMembers
        
        private int _iSiteId = 0;
        private DateTime _minDateTime = DateTime.MinValue;
        private DateTime _maxDateTime = DateTime.MinValue;
        private ReadBasedLiquidationBiz objReadBasedLiquidationBiz = new ReadBasedLiquidationBiz();

        #endregion //DataMembers

        #region Constructor

        public frmReadLiquidationDetails(int iSiteId, DateTime minDateTime, DateTime maxDateTime)
        {
            try
            {
                InitializeComponent();
                SetTagProperty();
                _iSiteId = iSiteId;
                _minDateTime = minDateTime;
                _maxDateTime = maxDateTime;
                LoadReadLiquidation();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SetTagProperty()
        {
            this.btnClose.Tag = "Key_CloseCaption";
            this.grpReadData.Tag = "Key_ReadDetails";
            this.Tag = "Key_ReadLiquidationDetails";
            this.ResolveResources();
        }

        #endregion //Constructor

        #region Events

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion //Events
        #region Methods

        private void LoadReadLiquidation()
        {
            dtgvReadData.DataSource = objReadBasedLiquidationBiz.GetReadLiquidationDetails(_iSiteId, _minDateTime, _maxDateTime);
            FormatReadDataGrid();
        }

        #endregion //Methods

        private void frmReadLiquidationDetails_Load(object sender, EventArgs e)
        {
            this.ResolveResources();

        }
        private void FormatReadDataGrid()
        {
            try
            {
                dtgvReadData.Columns["Read_No"].Tag = "Key_ReadNo";
                dtgvReadData.Columns["Bar_Pos_Name"].Tag = "Key_BarPosName";
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
    }
}


