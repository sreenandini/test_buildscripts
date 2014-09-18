using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Transport.Enum;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Presentation.POS.Helper_classes;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Transport;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for CShortpays.xaml
    /// </summary>
    public partial class CShortpays : IDisposable
    {
        private string s_KeyText = string.Empty;
        string strValue = string.Empty;
        IShortPay objCashDesk = ShortPayBusinessObject.CreateInstance();
        BMC.Transport.CashDeskOperatorEntity.ReasonCode objReasonCode = new BMC.Transport.CashDeskOperatorEntity.ReasonCode();
        DataTable dtInstallations;
        bool bIsAuthorised = false;
        BMC.Security.Interfaces.IUser AuthorisedUsr = null;

        public void ClearControls()
        {
            txtTicketNumber.Text = "";
            lvReason.SelectedIndex = -1;
            lstInstallation.SelectedIndex = -1;
            lstInstallation.Focus();
            objValueCalc.txtDisplay.Clear();
            txtComments.Text = "";
            objValueCalc.ClearAll();
            //objValueCalc.Clear_Click(null, null);
        }

        public void  LoadControls()
        {

            dtInstallations = (oCommonUtilities.CreateInstance()).GetInstallationList();
            lstInstallation.DataContext = dtInstallations.DefaultView;           
            IShortPay objCashDesk = ShortPayBusinessObject.CreateInstance();
            dtInstallations = objCashDesk.GetFailureReasons();
            lvReason.DataContext = dtInstallations.DefaultView;
            ClearControls();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
          
            lstInstallation.Focus();
            if (dtInstallations.Rows.Count > 0)
            {
                lstInstallation.SelectedIndex = 0;
            }
          
        }

        public CShortpays()
        {
            try
            {
                InitializeComponent();

                this.objValueCalc.MaxLength = 9;
                if (!Settings.AllowManualKeyboard)
                this.objValueCalc.txtDisplay.IsReadOnly = true;
                LoadControls();
                //DataTable dtInstallations = (oCommonUtilities.CreateInstance()).GetInstallationList();
                //lstInstallation.DataContext = dtInstallations.DefaultView;
                //IShortPay objCashDesk = ShortPayBusinessObject.CreateInstance();
                //dtInstallations = objCashDesk.GetFailureReasons();
                //lvReason.DataContext = dtInstallations.DefaultView;
                //ClearControls();

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID191");
            }

        }

        private bool CheckData()
        {
            try
            {

                if (lstInstallation.SelectedIndex < 0)
                {
                    MessageBox.ShowBox("MessageID194");
                    lstInstallation.Focus();
                    return false;

                }

                if (string.IsNullOrEmpty(txtTicketNumber.Text.Trim()))
                {
                    MessageBox.ShowBox("MessageID196");
                    txtTicketNumber.Focus();
                    return false;
                }

                if (txtComments.Text == "")
                {
                    if (lvReason.SelectedIndex < 0)
                    {
                        MessageBox.ShowBox("MessageID195");
                        lvReason.Focus();
                        return false;
                    }

                }
                else
                {

                    if (lvReason.SelectedIndex >= 0)
                    {
                        MessageBox.ShowBox("MessageID426");
                        lvReason.Focus();
                        return false;
                    }

                }

                if (objValueCalc.txtDisplay.Text == "")
                {
                    MessageBox.ShowBox("MessageID192");
                    objValueCalc.Focus();
                    return false;

                }
                else if (objValueCalc.txtDisplay.Text == "0.00")
                {
                    MessageBox.ShowBox("MessageID193");                    
                    this.objValueCalc.txtDisplay.Focus();
                    this.objValueCalc.txtDisplay.Select(0, this.objValueCalc.txtDisplay.Text.Length);
                    return false;
                }
                return true;
            }

            catch
            {
                return false;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int iReasonID = 0;
                btnSave.IsEnabled = false;
                if (!CheckData())
                {
                    return;
                   
                }

                int iSelectedInstallation = 0;
                int iSelectedReasoncode = 0;
                string strExceptionDetails = string.Empty;
                int iExceptionCode = 0;
                string strExpectedStatus = string.Empty;
                string strReason = "";
                string strComment = "";

                IHandpay IUser_No = HandpayBusinessObject.CreateInstance();
                int AuthorizedUserNo = Security.SecurityHelper.CurrentUser.User_No;

                DataRowView drv = (DataRowView)lstInstallation.SelectedItem;
                iSelectedInstallation = Convert.ToInt32(drv["Installation_No"]);

                if (lvReason.SelectedIndex >= 0)
                {
                    iSelectedReasoncode = 0;
                    drv = (DataRowView)lvReason.SelectedItem;
                    iSelectedReasoncode = Convert.ToInt32(drv[0]);
                }
                else
                {
                    if (lvReason.Items.SourceCollection is System.Data.DataView)
                    {
                        DataView dvReason = (DataView)lvReason.Items.SourceCollection;
                        DataTable dtReason = new DataTable();
                        dtReason = dvReason.ToTable().Copy();
                        DataView dvReasonClone = new DataView(dtReason);
                        dvReasonClone.RowFilter = "ReasonDescription='" + txtComments.Text + "'";
                        if (dvReasonClone.ToTable().Rows.Count > 0)
                        {
                            iSelectedReasoncode = Convert.ToInt32(dvReasonClone.ToTable().Rows[0]["ReasonCode"]);
                        }
                        else
                        {
                            iSelectedReasoncode = lvReason.Items.Count + 1;
                            
                        }
                    }
                }

                double dValue = Convert.ToDouble(objValueCalc.txtDisplay.Text);
                bool blnTicketFoundinException = false;
                
                IShortPay objCashDesk = ShortPayBusinessObject.CreateInstance();

                BMC.Transport.CashDeskOperatorEntity.Treasury objTreasuries = new BMC.Transport.CashDeskOperatorEntity.Treasury();
                objTreasuries.InstallationNumber = iSelectedInstallation;
                objTreasuries.UserID = Security.SecurityHelper.CurrentUser.User_No;                
                objTreasuries.TreasuryType = "Shortpay";
                objTreasuries.TreasuryReasonCode = iSelectedReasoncode;

                if (lvReason.SelectedIndex >= 0)
                {
                    DataRowView drvReason = (DataRowView)lvReason.SelectedItem;
                    strReason = drvReason[1].ToString();
                    strComment = strReason + ". Voucher Number is" + txtTicketNumber.Text;
                }
                else
                {
                    objReasonCode.Reason_Code = iSelectedReasoncode;
                    objReasonCode.ReasonDescription = txtComments.Text;
                    if (!String.IsNullOrEmpty(txtComments.Text))
                    {
                        if (Settings.AddShortpayCommentstoDefault)
                        {
                            iReasonID = objCashDesk.SaveReasonDetails(objReasonCode);
                        }
                        strComment = txtComments.Text + " . Voucher Number is " + txtTicketNumber.Text;
                        strReason = txtComments.Text;
                        objTreasuries.TreasuryReason = strComment;
                    }
                
                }

                if (strComment.Length > 199)
                    objTreasuries.TreasuryReason = strComment.Substring(0, 199);
                else
                    objTreasuries.TreasuryReason = strComment;
                
                objTreasuries.TreasuryAmount = dValue;
                objTreasuries.TreasuryIssuerUserNo = AuthorizedUserNo;

				int iShortPayID = 0;
                int UserSecurityId = 0;
                if (BMC.Transport.Settings.ShortPayAuthorizationRequired &&
                    objTreasuries.TreasuryAmount >= BMC.Transport.Settings.ShortPayAuthorizationLimit)
                {
                    if (objCashDesk.CreateShortPayForApproval(objTreasuries,ref iShortPayID))
                    {
                        LogManager.WriteLog("Shortpay saved for authorization. Ticketnumber: " + txtTicketNumber.Text, LogManager.enumLogLevel.Info);
                    }
                    else
                    {
                        LogManager.WriteLog("Unable to save Shortpay authorization. Ticketnumber: " + txtTicketNumber.Text, LogManager.enumLogLevel.Info);
                        MessageBox.ShowBox("MessageID198");
						return;
                    }

                    CAuthorize oCAuthorize = new CAuthorize("BMC.Presentation.CShortPay.ShortPayApprover");
                    oCAuthorize.ShowDialog();
                    if (!oCAuthorize.IsAuthorized)
                    {
                        objCashDesk.CancelShortPayForApproval(iShortPayID);                        
                        return;
                    }
                    bIsAuthorised = true;
                    AuthorisedUsr = oCAuthorize.User;
                    UserSecurityId = oCAuthorize.User.SecurityUserID;
                    AuthorizedUserNo = IUser_No.GetUserID(UserSecurityId);
                }

                //Update user no with authorized user if shortpay is authorised by different user
                objTreasuries.TreasuryIssuerUserNo = AuthorizedUserNo;
                objTreasuries.AuthorizedUser_No = AuthorizedUserNo;
                objTreasuries.Authorized_Date = DateTime.Now;

                int iTreasuryID = objCashDesk.SaveShortpayDetails(objTreasuries);

				if (iShortPayID > 0 )
                    objCashDesk.ApproveShortPay(iShortPayID.ToString(), UserSecurityId, iTreasuryID);

                if (iTreasuryID > 0)
                {
                    LogManager.WriteLog("Shortpay saved in treasury. Ticketnumber: " + txtTicketNumber.Text, LogManager.enumLogLevel.Info);
                    if (iReasonID > 0)
                    {
                        LogManager.WriteLog("ReasonCode saved in Reason_Code. ReasonCode: " + objReasonCode.Reason_Code, LogManager.enumLogLevel.Info);
                    }
                }
                else
                {
                    LogManager.WriteLog("Unable to save Shortpay in treasury. Ticketnumber: " + txtTicketNumber.Text, LogManager.enumLogLevel.Info);
                    return;
                }

                strExceptionDetails = "Normal Treasury Entry";
                iExceptionCode = (int)ShortpayExceptionCodes.NormalTreasuryEntry;
                IHandpay handpay = HandpayBusinessObject.CreateInstance();
                DataTable dtException = handpay.GetTicketingExceptionTable(txtTicketNumber.Text);

                if (dtException.Rows.Count > 0)
                {
                    LogManager.WriteLog("Voucher Number :" + txtTicketNumber.Text + " found in Ticket_Exception table.", LogManager.enumLogLevel.Info);
                    strExpectedStatus = dtException.Rows[0]["TE_Status_Create_Expected"].ToString();
                    blnTicketFoundinException = true;
                    if (objCashDesk.UpdateTicketException(0, txtTicketNumber.Text, "V") == 0)
                    {
                        LogManager.WriteLog("Ticket Exception table updated. Voucher Number:" + txtTicketNumber.Text, LogManager.enumLogLevel.Info);
                    }

                    strExceptionDetails = "House Keeping Void";
                    iExceptionCode = (int)ShortpayExceptionCodes.HouseKeepingVoid;
                }

                if (blnTicketFoundinException && !string.IsNullOrEmpty(strExpectedStatus))
                {
                    if (strExpectedStatus == "VOID_SP" || strExpectedStatus == "ACTIVE")
                    {
                        BMC.Transport.CashDeskOperatorEntity.VoidOrExpiredTreasury objVoidTreasury = new BMC.Transport.CashDeskOperatorEntity.VoidOrExpiredTreasury();
                        objVoidTreasury.TicketNumber = txtTicketNumber.Text;
                        objVoidTreasury.TransactionType = "Shortpay";
                        objVoidTreasury.TreasuryReason = "Voiding Voucher for ShortPay";
                        objCashDesk.UpdateVoidorExpiredTreasury(objVoidTreasury);
                        LogManager.WriteLog("Updated void or expired treasury. Ticketnumber: " + txtTicketNumber.Text, LogManager.enumLogLevel.Info);
                        strExceptionDetails = "Voiding Vocuher for ShortPay";
                        iExceptionCode = (int)ShortpayExceptionCodes.VoidTicketForShortpay;
                    }
                }
                if (!blnTicketFoundinException)
                {
                    strExceptionDetails = "No entry found in ticket_exception table for the Voucher entered";
                    iExceptionCode = (int)ShortpayExceptionCodes.NoEntryInTicket_Exception;
                }

                Transport.CashDeskOperatorEntity.TicketException objException = new BMC.Transport.CashDeskOperatorEntity.TicketException();
                objException.InstallationNumber = iSelectedInstallation;
                objException.ExceptionDetails = strExceptionDetails;
                objException.ExceptionType = iExceptionCode;
                objException.Reference = txtTicketNumber.Text;
                objException.User = AuthorizedUserNo;
                objCashDesk.InsertException(objException);
                LogManager.WriteLog("Voucher inserted into Exception. Voucher Number :" + txtTicketNumber.Text, LogManager.enumLogLevel.Info);

                //MachineDetails.Value = dValue.ToString();
                //MachineDetails.TreasuryNo = iTreasuryID.ToString();
                
                string sDesc = "Position: " + ((System.Data.DataRowView)(lstInstallation.SelectedValue)).Row.ItemArray[0].ToString() + " Details: " + strReason + " TicketNo.: " + txtTicketNumber.Text + " Amount: " + dValue;
                sDesc += bIsAuthorised ? " Approved By : " + AuthorisedUsr.UserName : "";
                
                AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                {
                    AuditModuleName = ModuleName.Shortpay,
                    Audit_Screen_Name = "Shortpay",
                    Audit_Desc = sDesc,
                    AuditOperationType = OperationType.ADD,
                    Audit_Field = "Shortpay ",
                    Audit_New_Vl = "NULL"
                });
                
                MessageBox.ShowBox("MessageID197");

                LoadControls();
                lstInstallation.Focus();
                if (dtInstallations.Rows.Count > 0)
                {
                    lstInstallation.SelectedIndex = 0;
                }
                
                //To Print Receipt.
                if (!bIsAuthorised)
                    (oCommonUtilities.CreateInstance()).PrintCommonReceipt(false, "Shortpay", iTreasuryID.ToString());
                else
                    (oCommonUtilities.CreateInstance()).PrintCommonReceipt(false, "Shortpay", iTreasuryID.ToString(), AuthorisedUsr);                    

				AuthorisedUsr = null;
                bIsAuthorised = false;
                LogManager.WriteLog("Recipt printed for shortpay. Voucher Number :" + txtTicketNumber.Text, LogManager.enumLogLevel.Info);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID198");
                AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                {
                    AuditModuleName = ModuleName.Shortpay,
                    Audit_Screen_Name = "Shortpay",
                    Audit_Desc = "Exception Occured while saving the Data",
                    AuditOperationType = OperationType.ADD,
                    Audit_Field = "Shortpay ",
                    Audit_New_Vl = "NULL"
                });
            }
            finally
            {
                btnSave.IsEnabled = true;
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClearControls();
                AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                {
                    AuditModuleName = ModuleName.Shortpay,
                    Audit_Screen_Name = "Shortpay",
                    Audit_Desc = "Cleared Data",
                    AuditOperationType = OperationType.ADD,
                    Audit_Field = "Shortpay",
                    Audit_New_Vl = "NULL"
                });
                //objValueCalc.Clear_Click(sender, e);
                objValueCalc.ClearAll();
                lstInstallation.Focus();
                if (dtInstallations.Rows.Count > 0)
                {
                    lstInstallation.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

        }

        void objKeyboard_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (((NumberPadWind)sender).DialogResult == true)
            {
                s_KeyText = ((NumberPadWind)sender).ValueText;
            }
        }

        private string DisplayKeyboard(string KeyText)
        {
            s_KeyText = "";
            BMC.Presentation.NumberPadWind objKeyboard = new NumberPadWind();
            objKeyboard.Closing += new System.ComponentModel.CancelEventHandler(objKeyboard_Closing);
            objKeyboard.ValueText = KeyText;
            objKeyboard.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            objKeyboard.ShowDialog();
            return s_KeyText;
        }

        //Collection containing list of allowed keys
        private List<Key> AllowedKeys = new List<Key> 
        {
            //Numbers 0-9
            Key.D0, 
            Key.D1,
            Key.D2,
            Key.D3,
            Key.D4,
            Key.D6,
            Key.D7,
            Key.D8,
            Key.D9,

            //Keypad 0-9
            Key.NumPad0,
            Key.NumPad1,
            Key.NumPad2,
            Key.NumPad3,
            Key.NumPad4,
            Key.NumPad5,
            Key.NumPad6,
            Key.NumPad7,
            Key.NumPad8,
            Key.NumPad9,

            //Backspace,Decimal,enter and delete keys
            Key.Enter,
            Key.Back,
            Key.Delete,
            Key.Tab
        };

        private void txtTicketNumber_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !AllowedKeys.Contains(e.Key);
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                lvReason.Focus();
                if (dtInstallations.Rows.Count > 0)
                    if (lvReason.SelectedIndex < 0)
                        lvReason.SelectedIndex = 0;
            }

        }

        private void txtTicketNumber_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (BMC.Transport.Settings.OnScreenKeyboard)
            {
                txtTicketNumber.Text = DisplayKeyboard(txtTicketNumber.Text);
                txtTicketNumber.SelectionStart = txtTicketNumber.Text.Length;
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
                        this.txtTicketNumber.KeyDown -= (this.txtTicketNumber_KeyDown);
                        this.txtTicketNumber.PreviewMouseUp -= (this.txtTicketNumber_PreviewMouseUp);
                        this.btnSave.Click -= (this.btnSave_Click);
                        this.btnClear.Click -= (this.btnClear_Click);
                        this.btnClose.Click -= (this.btnClose_Click);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> CShortpays objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CShortpays"/> is reclaimed by garbage collection.
        /// </summary>
        ~CShortpays()
        {
            Dispose(false);
        }

        #endregion

        private void objValueCalc_ValueChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                strValue = Convert.ToString(((ValueCalcComp)sender).txtDisplay.Text);

            }
            catch (Exception ex)
            {
                //LogManager.WriteLog(strProc + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvReason.SelectedIndex > -1)
            {
                string strDeleteReason;
                int iReasonDeleteID;
                strDeleteReason = ((System.Data.DataRowView)(lvReason.SelectedItem)).Row.ItemArray[1].ToString();
                objReasonCode.ReasonDescription = strDeleteReason;
                iReasonDeleteID = objCashDesk.DeleteReasonDetails(objReasonCode);
                if (iReasonDeleteID == 1)
                {
                    MessageBox.ShowBox("MessageID429");

                }
                LoadControls();

            }
            else
            {
                MessageBox.ShowBox("MessageID428");
                lvReason.Focus();

            }
        }

        private void ObjKeyboardClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (((KeyboardInterface)sender).DialogResult == true)
            {
                s_KeyText = ((KeyboardInterface)sender).KeyString;
            }
        }


        private string DisplayKeyboard(string keyText, string type)
        {
            s_KeyText = "";

            var objKeyboard = new KeyboardInterface();
            if (type == "Pwd")
            {
                objKeyboard.IsPwd = true;
            }
            objKeyboard.Closing += ObjKeyboardClosing;
            objKeyboard.KeyString = keyText;
            Point locationFromScreen = this.PointToScreen(new Point(0, 0));
            PresentationSource source = PresentationSource.FromVisual(this);
            System.Windows.Point targetPoints = source.CompositionTarget.TransformFromDevice.Transform(locationFromScreen);
            objKeyboard.Top = targetPoints.Y + this.Height / 2;
            objKeyboard.Left = targetPoints.X;
            objKeyboard.ShowDialog();
            return s_KeyText;
        }

        private void txtComments_PreviewMouseUp_1(object sender, MouseButtonEventArgs e)
        {

            if (!BMC.Transport.Settings.OnScreenKeyboard)
                return;
            txtComments.Text = DisplayKeyboard(txtComments.Text, string.Empty);
            txtComments.SelectionStart = txtComments.Text.Length;
        }

        private void lvReason_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (lvReason.SelectedIndex < 0)
                return;
            else
            {                
                //txtComments.Text = ((DataRowView)(lvReason.SelectedItem)).Row.ItemArray[1].ToString();
                txtComments.Text = string.Empty;
            }
        }

        private void txtComments_TextChanged_1(object sender, TextChangedEventArgs e)
        {            
            if(txtComments.Text.Length > 0)
                lvReason.SelectedIndex = -1;
        }       

        private void txtComments_KeyUp_1(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                objValueCalc.txtDisplay.Focus();
                e.Handled = true;
            }           
            
        }

        private void objValueCalc_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                btnSave.Focus();
                e.Handled = true;
            }           
        }

        private void btnClear_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                lstInstallation.Focus();
                e.Handled = true;
            }
        }

        private void lstInstallation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                txtTicketNumber.Focus();
                e.Handled = true;
            }
        }

        private void lvReason_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                txtComments.Focus();
                e.Handled = true;
            }
        }

        private void objValueCalc_EnterClicked(object sender, RoutedEventArgs e)
        {            
            btnSave_Click(sender, e);
        }       
    }
}
