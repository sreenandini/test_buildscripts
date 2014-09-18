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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using BMC.Common.ExceptionManagement;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Presentation.POS.Helper_classes;
using Audit.Transport;
using Audit.BusinessClasses;
using BMC.Security;
using System.Globalization;
using BMC.Common.Utilities;
using BMC.Business.CashDeskOperator;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for CRedeemOfflineTicket.xaml
    /// </summary>
    public partial class CRedeemOfflineTicket : Window, IDisposable
    {
        private string strTicketNumber = string.Empty;
        DataTable dtInstallations = null;
        Object   AssetName =null;
        Object OfflineAssetPosition = null;
        public bool IsSuccessfull = false; 
        public CRedeemOfflineTicket()
        {
            InitializeComponent();

            dtInstallations = (oCommonUtilities.CreateInstance()).GetInstallationList();
            lstInstallation.DataContext = dtInstallations.DefaultView;
            MessageBox.childOwner = this;
        }
         public string  TicketNumber 
        {
            get
            {
                return strTicketNumber;
            }

            set
            {
                strTicketNumber = value;
            }
            }
         private void lstInstallation_SelectionChanged(object sender, SelectionChangedEventArgs e)
         {
             try
             {
               
                
                 int index = lstInstallation.SelectedIndex;
                 if (index >= 0)
                 {
                     AssetName = dtInstallations.Rows[index][1];
                     OfflineAssetPosition = dtInstallations.Rows[index][0];
                   

                 }
                 //CommonUtilities.AssetName =(string) AssetName;
                 //CommonUtilities.OfflineAssetPosition = (string)OfflineAssetPosition;
              
             }
             catch (Exception ex)
             {
                 ExceptionManager.Publish(ex);
             }
         }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            bool bValidTicket   =   false;
            double amount       =   0;
            int treasuryNo      =   0;

            try
            {
                btnConfirm.IsEnabled = false;
                if (objValueCalc.txtDisplay.Text == "")
                {
                    MessageBox.ShowBox("MessageID183");
                    return;

                }
                else if (objValueCalc.txtDisplay.Text == "0.00")
                {
                    MessageBox.ShowBox("MessageID184");
                    return;
                }
                if (lstInstallation.SelectedIndex < 0)
                {
                    MessageBox.ShowBox("MessageID185");
                    return;

                }
                Int32 iSelectedInstallation = 0;

                DataRowView drv = (DataRowView)lstInstallation.SelectedItem;
                iSelectedInstallation = Convert.ToInt32(drv["Installation_No"]);

                if (!Convert.ToInt32(this.TicketNumber.Substring(4, 5)).Equals(iSelectedInstallation))
                {
                    MessageBox.ShowBox("MessageID882");                    
                    //this.Close();
                    return;
                }
                if (iSelectedInstallation > 0)
                {
                    IRedeemOfflineTicket objCashDesk = RedeemOfflineTicketBusinessObject.CreateInstance();

                    double.TryParse(objValueCalc.txtDisplay.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture), out amount);

                    if (objCashDesk.IsTicketValid(iSelectedInstallation, this.TicketNumber, Convert.ToInt32(amount * 100)))
                    {
                        Transport.CashDeskOperatorEntity.OfflineTicket objOfflineTicket = new BMC.Transport.CashDeskOperatorEntity.OfflineTicket();
                        objOfflineTicket.InstallationNumber = iSelectedInstallation;
                        objOfflineTicket.TicketBarCode = this.TicketNumber;
                        objOfflineTicket.PayableValue = (float)amount * 100;
                        objOfflineTicket.CustomerDetails = "";                   
                        objOfflineTicket.UserID =  Security.SecurityHelper.CurrentUser.User_No;
                        bValidTicket = objCashDesk.SaveOfflineTicketDetails(objOfflineTicket, out treasuryNo);

                        if (bValidTicket && BMC.Transport.Settings.EnableVoucher && treasuryNo > 0 )
                        {
                            IsSuccessfull = true;
                            Audit(objValueCalc.txtDisplay.Text, this.TicketNumber, "Voucher Redeemed Value-" + objValueCalc.txtDisplay.Text);
                            try
                            {
                                (oCommonUtilities.CreateInstance()).PrintCommonReceipt(objOfflineTicket, treasuryNo);
                            }
                            catch (Exception ex1)
                            {
                                ExceptionManager.Publish(ex1);
                                MessageBox.ShowBox("MessageID205");
                                Audit(objValueCalc.txtDisplay.Text, this.TicketNumber, "Unable to print Recipt.");
                            }
                        }
                        else if (bValidTicket)
                        {
                            IsSuccessfull = true;
                            Audit(objValueCalc.txtDisplay.Text, this.TicketNumber, "Voucher Redeemed Value-" + objValueCalc.txtDisplay.Text);
                        }
                        else
                        {
                            MessageBox.ShowBox("MessageID187");
                            Audit("", this.TicketNumber, "Unable to save Offline Voucher in the DB.");
                            IsSuccessfull = false;
                        }
                        this.Close();
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID188");
                        Audit("", this.TicketNumber, "Invalid Voucher Redemption Attempt.");
                        IsSuccessfull = false;
                        //this.Close();                        
                    }

                }
                else
                {
                    MessageBox.ShowBox("MessageID189");
                    IsSuccessfull = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowBox("MessageID190");
                ExceptionManager.Publish(ex);
                Audit("", this.TicketNumber, "An error occured while saving the Voucher.");
                IsSuccessfull = false;
                this.Close();
            }
            finally
            {
                btnConfirm.IsEnabled = true;
            }
        }

        private void Audit(string sRedeemedAmount,string sTicketNumber,string sDesc)
        {
            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
            {
                AuditModuleName = ModuleName.OfflineVoucher_Shortpay,
                Audit_Screen_Name = "Offline Voucher-Shortpay",
                //Audit_Screen_Name = "Vouchers|RedeemVoucher",
                Audit_Desc = sDesc,
                AuditOperationType = OperationType.MODIFY,
                Audit_Field = "Offline Voucher-Shortpay",
                //Audit_Field = "Voucher Number",
                Audit_New_Vl = sTicketNumber
            });
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
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
                        this.btnExit.Click -= (this.btnExit_Click_1);
                        this.btnConfirm.Click -= (this.btnConfirm_Click);
                        this.btnCancel.Click -= (this.btnCancel_Click);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("CRedeemOfflineTicket objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CRedeemOfflineTicket"/> is reclaimed by garbage collection.
        /// </summary>
        ~CRedeemOfflineTicket()
        {
            Dispose(false);
        }

        #endregion
    }
}
