using System;
using System.Windows;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Transport;
using BMC.Common.Utilities;
using System.Windows.Controls;
//Audit
using Audit.Transport;
using Audit.BusinessClasses;
using BMC.Security;
using System.Globalization;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation
{
    public partial class CPrintTicket : IDisposable
    {
        IssueTicketEntity issueTicketEntity = new IssueTicketEntity();
        IIssueTicket objCashDeskOperator = IssueTicketBusinessObject.CreateInstance();

        //public delegate void ScannerClick(object sender, RoutedEventArgs e);
        
        
        TextBox txt;
        string strValue = string.Empty;
        static string strProc = string.Empty;
        public CPrintTicket()
        {
            this.InitializeComponent();
            this.ucValueCalc.MaxLength = 9;
            if(!Settings.AllowManualKeyboard)
            this.ucValueCalc.txtDisplay.IsReadOnly = true;
            this.ucValueCalc.EnterClicked += (btnIssue_Click);
           

            try
            {
                txtPrinterName.Text = Settings.VoucherPrinterName.ToUpper();

                if (Settings.VoucherPrinterName.ToUpper() == "COUPONEXPRESS")
                    txtSerialNumber.Text = objCashDeskOperator.GetPrinterInformation();
                else
                    lblSerialNumber.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void btnIssue_Click(object sender, RoutedEventArgs e)
        {

            long lValue = 0;
            try
            {
                txt = (TextBox)this.ucValueCalc.txtDisplay;

                strValue = Convert.ToString(txt.Text);
                btnIssue.IsEnabled = false;
                if (strValue != null && strValue != string.Empty)
                    lValue = Convert.ToInt64(strValue.GetSingleFromString() * 100);
                else
                    lValue = 0;

                if (lValue <= 0)
                {
                    MessageBox.ShowBox("MessageID101");
                    return;
                }
              

                if (!string.IsNullOrEmpty(Settings.IssueTicketMaxValue))
                {
                    long lSettingValue = Convert.ToInt64(Settings.IssueTicketMaxValue.GetSingleFromString() * 100);
                    if (lValue > lSettingValue)
                    {
                        string sMessage = Application.Current.FindResource("MessageID247") as string;
                        //MessageBox.ShowBox(sMessage, BMC_Icon.Error, true);
                        MessageBox.ShowBox(sMessage + ": " +
                           ExtensionMethods.GetUniversalCurrencyFormat(Convert.ToInt64(Settings.IssueTicketMaxValue.GetSingleFromString())),
                                                                                                                    BMC_Icon.Error, true);

                        return;
                    }
                }
              
                if ((lValue <= 99999999) && (lValue > 0))
                {
                    issueTicketEntity.lnglValue = lValue;
                    issueTicketEntity.Type = UIIssueTicketConstants.STANDARDTICKET;
                    issueTicketEntity.dblValue = Convert.ToDouble(strValue, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture));
                    issueTicketEntity.Date = DateTime.Now;

                    PrintTicketErrorCodes PrintResult = objCashDeskOperator.IssueTicket(issueTicketEntity);

                    switch (PrintResult)
                    {
                        case PrintTicketErrorCodes.OpenCOMPortFailure:
                            {

                                MessageBox.ShowBox("MessageID295", BMC_Icon.Warning); //Unable to open COM Port. Please check connectivity.
                                ClearValues();
                                Audit("Unable to open COM Port");
                                return;
                            }
                        case PrintTicketErrorCodes.OutofPaper:
                            {
                                MessageBox.ShowBox("MessageID294", BMC_Icon.Warning); //out of Paper
                                ClearValues();
                                Audit("Printer Out of Paper.");
                                return;
                            }
                        case PrintTicketErrorCodes.Success:
                            {

                                //Calling Audit Method
                                Audit.Transport.Audit_History AH = new Audit.Transport.Audit_History();
                                //Populate required Values

                                AH.AuditModuleName = ModuleName.Voucher;
                                AH.Audit_Screen_Name = "Vouchers|PrintVoucher";
                                AH.Audit_Desc = "Voucher Printed Value-" + strValue;
                                AH.AuditOperationType = OperationType.ADD;
                                AH.Audit_Field = "Voucher Number";
                                AH.Audit_New_Vl = issueTicketEntity.BarCode;

                                ClearValues();
                                MessageBox.ShowBox("MessageID103", BMC_Icon.Information);
                                AuditViewerBusiness.InsertAuditData(AH);
                                break;
                            }
                        case PrintTicketErrorCodes.eVoltErr:
                            {
                                MessageBox.ShowBox("Voltage Error", BMC_Icon.Error);
                                ClearValues();
                                Audit("Voltage Error");
                                return;
                            }
                        case PrintTicketErrorCodes.eHeadErr:
                            {
                                MessageBox.ShowBox("Printer Head Error", BMC_Icon.Error);
                                ClearValues();
                                Audit("Printer Head Error");
                                return;
                            }
                        case PrintTicketErrorCodes.ePaperOut:
                            {
                                MessageBox.ShowBox("Out of Paper", BMC_Icon.Error);
                                ClearValues();
                                Audit("Out of Paper");
                                return;
                            }
                        case PrintTicketErrorCodes.ePlatenUP:
                            {
                                MessageBox.ShowBox("Platen Up", BMC_Icon.Error);
                                ClearValues();
                                Audit("Platen Up");
                                return;
                            }
                        case PrintTicketErrorCodes.eSysErr:
                            {
                                MessageBox.ShowBox("System Error", BMC_Icon.Error);
                                ClearValues();
                                Audit("System Error");
                                return;
                            }
                        case PrintTicketErrorCodes.eBusy:
                            {
                                MessageBox.ShowBox("Printer is Busy", BMC_Icon.Error);
                                ClearValues();
                                Audit("Printer is Busy");
                                return;
                            }
                        case PrintTicketErrorCodes.eJobMemOF:
                            {
                                MessageBox.ShowBox("Job Memory off", BMC_Icon.Error);
                                ClearValues();
                                Audit("Job Memory off");
                                return;
                            }
                        case PrintTicketErrorCodes.eBufOF:
                            {
                                MessageBox.ShowBox("Buffer off", BMC_Icon.Error);
                                ClearValues();
                                Audit("Buffer off");
                                return;
                            }
                        case PrintTicketErrorCodes.eLibLoadErr:
                            {
                                MessageBox.ShowBox("Library Load Error", BMC_Icon.Error);
                                ClearValues();
                                Audit("Library Load Error");
                                return;
                            }
                        case PrintTicketErrorCodes.ePRDataErr:
                            {
                                MessageBox.ShowBox("Printer Data Error", BMC_Icon.Error);
                                ClearValues();
                                Audit("Printer Data Error");
                                return;
                            }
                        case PrintTicketErrorCodes.eLibRefErr:
                            {
                                MessageBox.ShowBox("Library Reference Error", BMC_Icon.Error);
                                ClearValues();
                                Audit("Library Reference Error");
                                return;
                            }
                        case PrintTicketErrorCodes.eTempErr:
                            {
                                MessageBox.ShowBox("Temp Error", BMC_Icon.Error);
                                ClearValues();
                                Audit("Temp Error");
                                return;
                            }
                        case PrintTicketErrorCodes.eMissingSupplyIndex:
                            {
                                MessageBox.ShowBox("Supply Index is Missing", BMC_Icon.Error);
                                ClearValues();
                                Audit("Supply Index is Missing");
                                return;
                            }
                        case PrintTicketErrorCodes.eFlashProgErr:
                            {
                                MessageBox.ShowBox("Flash Program Error", BMC_Icon.Error);
                                ClearValues();
                                Audit("Flash Program Error");
                                return;
                            }
                        case PrintTicketErrorCodes.ePaperInChute:
                            {
                                MessageBox.ShowBox("Paper in Chute", BMC_Icon.Error);
                                ClearValues();
                                Audit("Paper in Chute");
                                return;
                            }
                        case PrintTicketErrorCodes.ePrintLibCorr:
                            {
                                MessageBox.ShowBox("Print library is corrupted.", BMC_Icon.Error);
                                ClearValues();
                                Audit("Print library is corrupted.");
                                return;
                            }
                        case PrintTicketErrorCodes.eCmdErr:
                            {
                                MessageBox.ShowBox("Command Error", BMC_Icon.Error);
                                ClearValues();
                                Audit("Command Error");
                                return;
                            }
                        case PrintTicketErrorCodes.ePaperLow:
                            {
                                MessageBox.ShowBox("Paper low.", BMC_Icon.Error);
                                ClearValues();
                                Audit("Paper low.");
                                return;
                            }
                        case PrintTicketErrorCodes.ePaperJam:
                            {
                                MessageBox.ShowBox("Paper jammed.", BMC_Icon.Error);
                                ClearValues();
                                Audit("Paper jammed.");
                                return;
                            }
                        case PrintTicketErrorCodes.eCurrentErr:
                            {
                                MessageBox.ShowBox("Current Error", BMC_Icon.Error);
                                ClearValues();
                                Audit("Current Error");
                                return;
                            }
                        case PrintTicketErrorCodes.eJournalPrint:
                            {
                                MessageBox.ShowBox("Journal Print Error", BMC_Icon.Error);
                                ClearValues();
                                Audit("Journal Print Error");
                                return;
                            }
                        default:
                            {
                                MessageBox.ShowBox("MessageID102", BMC_Icon.Warning);
                                ClearValues();
                                Audit("Unable to Print Voucher.");
                                return;
                            }

                    }
                }
                else
                {
                    MessageBox.ShowBox("MessageID104", BMC_Icon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                this.ucValueCalc.ClearValues();
                this.ucValueCalc.ValueText = ExtensionMethods.GetCurrencyDecimalDelimiter() + "00";
                this.ucValueCalc.s_UnformattedText = "";
                ExceptionManager.Publish(ex);
            }
            finally
            {
                btnIssue.IsEnabled = true;
            }
        }

        private void Audit(string sDesc)
        {
            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
            {
                
                AuditModuleName = ModuleName.Voucher,
                Audit_Screen_Name = "Vouchers|PrintVoucher",
                Audit_Desc = sDesc,
                AuditOperationType = OperationType.ADD
            });
        }

        private void ClearValues()
        {
            this.ucValueCalc.ClearValues();
            this.ucValueCalc.ValueText = ExtensionMethods.GetCurrencyDecimalDelimiter() + "00";
            this.ucValueCalc.s_UnformattedText = "";
        }



        private void ucValueCalc_ValueChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                strValue = Convert.ToString(((ValueCalcComp)sender).txtDisplay.Text);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog(strProc + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
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
                        this.btnIssue.Click -= (this.btnIssue_Click);
                        this.ucValueCalc.ValueChanged -= (this.ucValueCalc_ValueChanged);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("CPrintTicket objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CPrintTicket"/> is reclaimed by garbage collection.
        /// </summary>
        ~CPrintTicket()
        {
            Dispose(false);
        }

        #endregion
    }
}