using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BMC.Common.ExceptionManagement;
using BMC.CashDeskOperator.BusinessObjects;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for CPortBlocking.xaml
    /// </summary>
    public partial class CPortBlocking : Window, IDisposable
    {
        #region Variables
        int installationNo;
        #endregion

        #region Constructor
        public CPortBlocking(int iInstallationNo)
        {
            InitializeComponent();
            this.installationNo = iInstallationNo;
        }
        #endregion

        #region Events
        private void btnClearPort_Click(object sender, RoutedEventArgs e)
        {
            try
            {   
                DialogResult = false;
                Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnBlockPort_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var port1 = (bool)chkPort1.IsChecked;
                var port2 = (bool)chkPort2.IsChecked;
                var port3 = (bool) chkPort3.IsChecked;
                if (port1 || port2 || port3)
                {
                    CMachineMaintenance objMachineMaintenance = new CMachineMaintenance();
                    var AuxPort = Convert.ToByte(port1);
                    var SlotLinePort = Convert.ToByte(port2);
                    var GATPort = Convert.ToByte(port3);
                    //objMachineMaintenance.BlockPort(installationNo, AuxPort, SlotLinePort, GATPort);
                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        AuditModuleName = ModuleName.MachineMaintenance,
                        Audit_Screen_Name = "Machine Maintenance View",
                        Audit_Desc = "Port Blocking - Installation No: " + installationNo.ToString(),
                        AuditOperationType = OperationType.MODIFY,
                    });
                    MessageBox.ShowBox("MessageID323", BMC_Icon.Information, BMC_Button.OK);
                    Close();
                }
                else
                {
                    MessageBox.ShowBox("MessageID324", BMC_Icon.Information, BMC_Button.OK);

                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }
        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }
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
                        this.UserControl.Loaded -= (this.UserControl_Loaded);
                        this.btnBlockPort.Click -= (this.btnBlockPort_Click);
                        this.btnClearPort.Click -= (this.btnClearPort_Click);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("CPortBlocking objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CPortBlocking"/> is reclaimed by garbage collection.
        /// </summary>
        ~CPortBlocking()
        {
            Dispose(false);
        }

        #endregion  
       
    }
}
