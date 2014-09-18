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

namespace BMC.Presentation
{
    public partial class CReadLiquidationDetails : IDisposable
    {
        #region DataMembers

        private DateTime _minDateTime = DateTime.MinValue;
        private DateTime _maxDateTime = DateTime.MinValue;

        private ReadLiquidationDetailsConfiguration oReadBasedLiquidationConfiguration = ReadLiquidationDetailsConfiguration.ReadLiquidationDetailsConfigurationInstance;

        #endregion //DataMembers

        #region Constructor

        public CReadLiquidationDetails(DateTime minDateTime, DateTime maxDateTime)
        {
            InitializeComponent();
            _minDateTime = minDateTime;
            _maxDateTime = maxDateTime;
            LoadReadLiquidation();
        }

        #endregion //Constructor

        #region Events

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion Events

        #region Common Methods

        private void LoadReadLiquidation()
        {
            try
            {
                lstReadDetails.DataContext = oReadBasedLiquidationConfiguration.GetReadLiquidationDetails(_minDateTime, _maxDateTime);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
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
                        this.btnClose.Click -= (this.btnClose_Click);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> CReadLiquidationDetails objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CVoid"/> is reclaimed by garbage collection.
        /// </summary>
        ~CReadLiquidationDetails()
        {
            Dispose(false);
        }

        #endregion //IDisposable Members
    }
}