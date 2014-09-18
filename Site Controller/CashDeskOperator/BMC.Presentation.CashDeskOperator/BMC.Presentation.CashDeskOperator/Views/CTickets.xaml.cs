/*******************************************************************************************************
 *  Revision History
 *  Name            TrackCode   Modified Date   Change Description
 *  Selva Kumar S   S001        27th Jul 2012   Commented TITO based validation in Centralized Cashier
 *                                              Transaction
 * ****************************************************************************************************/

using System;
using System.Windows;
using BMC.Common.ExceptionManagement;
using BMC.Transport;
using BMC.Presentation.POS.Helper_classes;
using BMC.Common.LogManagement;
using BMC.Security;
using System.Data.SqlClient;
using BMC.Presentation.POS.Views;

namespace BMC.Presentation
{
    public partial class CTickets
    {
        CPrintTicket PrintTicket;
        CRedeemTicket RedeemTicket;
                
        CAttendantPays Attendantpays;
        CManualAttendantPays ManualAttendantpays;
        RedeemMultipleTickets redeemMultipleTickets;

        CVoidTicket VoidTicket;

        bool IsTicketRedeem = false;

        public CTickets()
        {
            this.InitializeComponent();
            Loaded += new RoutedEventHandler(CTickets_Loaded);
        }

        void CTickets_Loaded(object sender, RoutedEventArgs e)
        {
            //if (Settings.CAGE_ENABLED)                                    //-S001
            if (Settings.CAGE_ENABLED || !(IsTitoEnabled()) )       //+S001
            {
                chkPrint.Visibility = Visibility.Collapsed;
                chkRedeem.Visibility = Visibility.Collapsed;
                chkVoid.Visibility = Visibility.Collapsed;
                chkMultipleVoucherRedeem.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (!SecurityHelper.HasAccess("BMC.Presentation.CPrintTicket"))
                    chkPrint.Visibility = Visibility.Collapsed;

                if (!SecurityHelper.HasAccess("BMC.Presentation.CRedeemTicket"))
                {
                    chkRedeem.Visibility = Visibility.Collapsed;
                    if (chkPrint.Visibility != Visibility.Collapsed)
                        chkPrint.IsChecked = true;
                }
                else
                    chkRedeem.IsChecked = true;

                if ((!SecurityHelper.HasAccess("BMC.Presentation.MultipleVoucher")) || !Settings.IsMultipleVoucherRedemptionEnabled)
                {
                    chkMultipleVoucherRedeem.Visibility = Visibility.Collapsed;
                }
                else
                {
                    redeemMultipleTickets = new RedeemMultipleTickets();
                    chkMultipleVoucherRedeem.IsChecked = true;
                    
                }
            }

            if (!SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.AttendantPay"))
            {
                chkAttendantPays.Visibility = Visibility.Collapsed;
                if (chkPrint.Visibility != Visibility.Collapsed)
                    chkPrint.IsChecked = true;             
            }
            else if ((chkPrint.Visibility == Visibility.Collapsed) && (chkRedeem.Visibility == Visibility.Collapsed))
                chkAttendantPays.IsChecked = true;
            

            if ((!SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.ManualAttendantPay")) || (!Settings.HandpayManual))
            {
                chkManualAttendantPays.Visibility = Visibility.Collapsed;
                if (chkPrint.Visibility != Visibility.Collapsed)
                    chkPrint.IsChecked = true;
            }
            if (!SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.CVoidTicket") || !Settings.VoidVouchers)
            {
                chkVoid.Visibility = Visibility.Collapsed;
                if (chkVoid.Visibility != Visibility.Collapsed)
                    chkVoid.IsChecked = true;
            }

            if (chkRedeem.Visibility != Visibility.Collapsed)
                chkRedeem.IsChecked = true;
            else if (chkPrint.Visibility != Visibility.Collapsed)
                chkPrint.IsChecked = true;             
            else if (chkVoid.Visibility != Visibility.Collapsed)
                chkVoid.IsChecked = true;
            else if (chkAttendantPays.Visibility != Visibility.Collapsed)
                chkAttendantPays.IsChecked = true;
            else if (chkManualAttendantPays.Visibility != Visibility.Collapsed)
                chkManualAttendantPays.IsChecked = true;
           
            //if (Settings.CAGE_ENABLED)
            //{
            //    chkPrint.Visibility = Visibility.Collapsed;
            //    chkRedeem.Visibility = Visibility.Collapsed;

            //}
            
            //if (!SecurityHelper.HasAccess("BMC.Presentation.CPrintTicket"))
            //    chkPrint.Visibility = Visibility.Collapsed;


            //if (!SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.AttendantPay"))
            //{
            //    chkAttendantPays.Visibility = Visibility.Collapsed;
            //    if (chkPrint.Visibility != Visibility.Collapsed)
            //        chkPrint.IsChecked = true;
            //}
            //else
            //    chkRedeem.IsChecked = true;

            //if (!SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.ManualAttendantPay"))
            //{
            //  chkManualAttendantPays.Visibility = Visibility.Collapsed;
            //    if (chkPrint.Visibility != Visibility.Collapsed)
            //        chkPrint.IsChecked = true;
            //}

            //if (!SecurityHelper.HasAccess("BMC.Presentation.CRedeemTicket"))
            //{
            //    chkRedeem.Visibility = Visibility.Collapsed;
            //    if (chkPrint.Visibility != Visibility.Collapsed)
            //        chkPrint.IsChecked = true;
            //}
            //else
            //    chkAttendantPays.IsChecked = true;

            ////if (chkPrint.Visibility == Visibility.Collapsed && chkRedeem.Visibility == Visibility.Collapsed)
            ////    pnlTicketContent.Visibility = Visibility.Hidden;

            //if (Settings.Not_Issue_Ticket)
            //    chkPrint.IsEnabled = false;      
        }

        private void chkRedeem_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                chkRedeem.IsEnabled = false;
                PrintTicket = null;
                RedeemTicket = new CRedeemTicket();
                pnlTicketContent.Child = RedeemTicket;
                RedeemTicket.Margin = new Thickness(0);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                chkRedeem.IsEnabled = true;
            }
        }

        private void chkPrint_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                chkPrint.IsEnabled = false;
                RedeemTicket = null;
                PrintTicket = new CPrintTicket();
                pnlTicketContent.Child = PrintTicket;
                PrintTicket.Margin = new Thickness(0);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                chkPrint.IsEnabled = true;
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
                        this.Loaded -= (CTickets_Loaded);
                        this.chkPrint.Checked -= (this.chkPrint_Checked);
                        this.chkRedeem.Checked -= (this.chkRedeem_Checked);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> CTickets objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CTickets"/> is reclaimed by garbage collection.
        /// </summary>
        ~CTickets()
        {
            Dispose(false);
        }

        #endregion
        private void chkAttendantPays_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                PrintTicket = null;
                RedeemTicket = null;
                Attendantpays = new CAttendantPays();
                pnlTicketContent.Child = Attendantpays;
                Attendantpays.Margin = new Thickness(0);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                chkRedeem.IsEnabled = true;
                
            }
        }
        private void chkManualAttendantPays_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                PrintTicket = null;
                RedeemTicket = null;
                Attendantpays = null;
                ManualAttendantpays = new CManualAttendantPays();
                pnlTicketContent.Child = ManualAttendantpays;
                ManualAttendantpays.Margin = new Thickness(0);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                chkRedeem.IsEnabled = true;
            }
        }

        private void chkMultipleVoucherRedeem_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                //redeemMultipleTickets = new RedeemMultipleTickets();
                pnlTicketContent.Child = redeemMultipleTickets;
                redeemMultipleTickets.Margin = new Thickness(0);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                chkMultipleVoucherRedeem.IsEnabled = true;
            }
        }



        private void chkVoid_Checked(object sender, RoutedEventArgs e)
        {

            try
            {
                chkVoid.IsEnabled = false;
                PrintTicket = null;
                RedeemTicket = null;
                VoidTicket = new CVoidTicket();
                pnlTicketContent.Child = VoidTicket;
                VoidTicket.Margin = new Thickness(0);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                chkVoid.IsEnabled = true;
            }
        }

        #region +S001 START
        private bool IsTitoEnabled()
        {
            int iTitoStatus = 0;
            try
            {

                using (SqlConnection oConn = new SqlConnection(BMC.DBInterface.CashDeskOperator.ConnectionStringHelper.ExchangeConnectionString))
                {

                    using (SqlCommand oCmd = new SqlCommand("dbo.rsp_GetTITOStatus", oConn))
                    {
                        oConn.Open();
                        oCmd.Connection = oConn;
                        iTitoStatus = Convert.ToInt32(oCmd.ExecuteScalar());
                    }
                }

            }
            catch (Exception Ex)
            {
                LogManager.WriteLog("IsTitoEnabled(): [Error] " + Ex.Message, LogManager.enumLogLevel.Error);
                iTitoStatus = 0;
            }
            return (iTitoStatus == 1) ? true : false;
        }
        #endregion +S001 END
    }
}