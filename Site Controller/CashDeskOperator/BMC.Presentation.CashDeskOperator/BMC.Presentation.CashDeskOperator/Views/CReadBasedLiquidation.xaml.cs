using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Presentation.POS.Helper_classes;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.CashDeskOperator;
using BMC.Security;
using BMC.Common.LogManagement;
using BMC.CommonLiquidation.Utilities;
using System.Windows.Data;

namespace BMC.Presentation
{
    public partial class CReadBasedLiquidation : UserControl, IDisposable
    {
        #region DataMembers

        readonly DataTable _dtTabRead = new DataTable("Read");

        private readonly int _pagingNoOfRecPerPage = Int32.Parse(ConfigManager.Read("ReadForLiquidationRecords"));
        GridViewColumnHeader _lastHeaderClicked;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;
        readonly Helper_classes.Common _common = new Helper_classes.Common();
        private ReadBasedLiquidationConfiguration oReadBasedLiquidationConfiguration = ReadBasedLiquidationConfiguration.ReadBasedLiquidationConfigurationInstance;
        private List<CommonLiquidationEntity> lstCommonLiquidation = null;

        #endregion //DataMembers

        #region Constructor

        public CReadBasedLiquidation()
        {
            InitializeComponent();
            _common.btnFirst = btnFirst;
            _common.btnLast = btnLast;
            _common.btnNext = btnNext;
            _common.btnPrev = btnPrev;
            _common.txtPage = txtPage;
            FillData();
            _common.CustomPaging(Helper_classes.Common.PagingMode.First, _dtTabRead, _pagingNoOfRecPerPage, lstRead, false);
            _common.DisplayPagingInfo(_dtTabRead, _common, _pagingNoOfRecPerPage);
        }

        #endregion //Constructor

        #region Events

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //FillData();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            FillData();
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lstRead.SelectedItems.Count <= 0)
                {
                    MessageBox.ShowBox("MessageID497", BMC_Icon.Information);
                    return;
                }

                List<ReadLiquidationEntity> lstReadLiquidation = lstRead.SelectedItems.Cast<ReadLiquidationEntity>().ToList();
                if (lstReadLiquidation == null) return;

                List<DateTime> lstReadDate = lstReadLiquidation.Select(item => Convert.ToDateTime(item.Read_Date)).ToList();

                CReadLiquidationDetails objCReadLiquidationDetails = new CReadLiquidationDetails(lstReadDate.Min(), lstReadDate.Max());
                objCReadLiquidationDetails.Owner = MessageBox.parentOwner;
                objCReadLiquidationDetails.ShowDialog();
            }

            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void btnPerform_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateRead()) return;
                List<ReadLiquidationEntity> lstReadLiquidation  = lstRead.SelectedItems.Cast<ReadLiquidationEntity>().ToList();
                CReadLiquidation objCReadLiquidation = new CReadLiquidation(lstCommonLiquidation, lstReadLiquidation[0].Read_Date.ToString());
                objCReadLiquidation.Owner = MessageBox.parentOwner;
                objCReadLiquidation.ShowDialog();
                FillData();
            }

            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                _common.CustomPaging(Helper_classes.Common.PagingMode.Next, _dtTabRead, _pagingNoOfRecPerPage, lstRead, false);
                _common.DisplayPagingInfo(_dtTabRead, _common, _pagingNoOfRecPerPage);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            try
            {
                _common.CustomPaging(Helper_classes.Common.PagingMode.Previous, _dtTabRead, _pagingNoOfRecPerPage, lstRead, false);
                _common.DisplayPagingInfo(_dtTabRead, _common, _pagingNoOfRecPerPage);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _common.CustomPaging(Helper_classes.Common.PagingMode.First, _dtTabRead, _pagingNoOfRecPerPage, lstRead, false);
                _common.DisplayPagingInfo(_dtTabRead, _common, _pagingNoOfRecPerPage);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _common.CustomPaging(Helper_classes.Common.PagingMode.Last, _dtTabRead, _pagingNoOfRecPerPage, lstRead, false);
                _common.DisplayPagingInfo(_dtTabRead, _common, _pagingNoOfRecPerPage);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion Events

        #region Common Methods

        public void FillData()
        {
            try
            {
                lstRead.DataContext = oReadBasedLiquidationConfiguration.GetReadLiquidationRecords();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private bool ValidateRead()
        {
            try
            {
                if (lstRead.Items.Count <= 0)
                {
                    MessageBox.ShowBox("MessageID487", BMC_Icon.Information);
                    return false;
                }
                if (lstRead.SelectedItems.Count <= 0)
                {
                    MessageBox.ShowBox("MessageID889", BMC_Icon.Information);
                    return false;
                }
                if (lstRead.SelectedItems.Count > 1)
                {
                    MessageBox.ShowBox("MessageID888", BMC_Icon.Information);
                    return false;
                }

                var coll1 = CollectionViewSource.GetDefaultView(lstRead.DataContext).SourceCollection.OfType<ReadLiquidationEntity>();
                var coll2 = lstRead.SelectedItems.OfType<ReadLiquidationEntity>();

                int k = 1;
                var items = (from a in coll1
                             join b in coll2
                             on a.Read_No equals b.Read_No
                             into c
                             from d in c.DefaultIfEmpty()
                             let j = k++
                             select new { Index = j, Data = d }).ToList();
                int count = items.Count;
                int index = 0;
                int selected = 0;
                while (index < count)
                {
                    var item = items[index];
                    if (item.Data == null) break;
                    selected++;
                    index++;
                }
                if (selected != lstRead.SelectedItems.Count)
                {
                    MessageBox.ShowBox("MessageID498", BMC_Icon.Warning);
                    return false;
                }

                List<ReadLiquidationEntity> lstReadLiquidation = lstRead.SelectedItems.Cast<ReadLiquidationEntity>().ToList();
                if (lstReadLiquidation == null) return false;

                lstCommonLiquidation = oReadBasedLiquidationConfiguration.GetReadLiquidation(Convert.ToDateTime(lstReadLiquidation.Select(item => item.Read_Date).Min()), Convert.ToDateTime(lstReadLiquidation.Select(item => item.Read_Date).Max()));
                if (lstCommonLiquidation == null || lstCommonLiquidation.Count <= 0)
                {
                    MessageBox.ShowBox("MessageID487", BMC_Icon.Information);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        #endregion //Common Methods

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
                        this.btnRefresh.Click -= (this.btnRefresh_Click);
                        this.btnPerform.Click -= (this.btnPerform_Click);
                        this.btnPrev.Click -= (this.btnPrev_Click);
                        this.btnFirst.Click -= (this.btnFirst_Click);
                        this.btnNext.Click -= (this.btnNext_Click);
                        this.btnLast.Click -= (this.btnLast_Click);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> CReadBasedLIquidation objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CVoid"/> is reclaimed by garbage collection.
        /// </summary>
        ~CReadBasedLiquidation()
        {
            Dispose(false);
        }

        #endregion //IDisposable Members
    }
}