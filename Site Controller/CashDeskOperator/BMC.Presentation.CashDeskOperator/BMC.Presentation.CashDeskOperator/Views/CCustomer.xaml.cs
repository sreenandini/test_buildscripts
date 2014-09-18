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
using System.ComponentModel;
using BMC.Transport;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using BMC.Presentation.POS.Helper_classes;
using BMC.Common.LogManagement;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for CCustomer.xaml
    /// </summary>
    public partial class CustomerDetails : Window
    {
        public delegate void  CustomerUpdateHandler(object sender, ExecutedRoutedEventArgs e);
        public event CustomerUpdateHandler delCustomerUpdated;

        public delegate  void CustomerCancelHandler(object sender, RoutedEventArgs e);
        public event CustomerCancelHandler delCustomerCancelled;


        public static RoutedCommand ActionCommand = new RoutedCommand();

        private Customer oCustomerDetailsEntity = new Customer();
        private BMC.CashDeskOperator.BusinessObjects.CustomerDetails oCustomerDetails = new BMC.CashDeskOperator.BusinessObjects.CustomerDetails();
        
        public static bool IsShowBankAccNo = false;
        public static bool IsCancelClicked = false;
        private string strKeyText = ""; 

        public CustomerDetails()
        {
        
            InitializeComponent();
            MessageBox.childOwner = this;
        }

        public CustomerDetails(bool ShowBankAccNo)
        {
        
            IsShowBankAccNo=ShowBankAccNo;
            IsCancelClicked = false;
            InitializeComponent();
            MessageBox.childOwner = this;

        }

        public void ActionCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                bool CanExecute = false;
                if (IsShowBankAccNo == true)
                {
                    CanExecute = Validation.GetHasError(txtFirstName) ||
                       Validation.GetHasError(txtMiddleName) ||
                       Validation.GetHasError(txtLastName) ||
                       Validation.GetHasError(txtAdd1) ||
                       Validation.GetHasError(txtAdd2) ||
                       Validation.GetHasError(txtPincode) ||
                       Validation.GetHasError(txtBankAccNo);
                }
                else
                {
                    CanExecute = Validation.GetHasError(txtFirstName) ||
                       Validation.GetHasError(txtMiddleName) ||
                       Validation.GetHasError(txtLastName) ||
                       Validation.GetHasError(txtAdd1) ||
                       Validation.GetHasError(txtAdd2) ||
                       Validation.GetHasError(txtPincode);
                }
                e.CanExecute = !CanExecute;
                if(btnSearch.IsEnabled==true)
                btnSaveCustomer.IsEnabled = !CanExecute;
            }
            catch(Exception ex )
            {
                ExceptionManager.Publish(ex);                
            }
        }

        public void ActionExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if (cmbTitle.SelectedItem == null)
                {
                    MessageBox.ShowBox("MessageID263", BMC_Icon.Information);
                    return; }
                oCustomerDetailsEntity.Title = cmbTitle.SelectedItem.ToString();
                oCustomerDetailsEntity.FirstName = txtFirstName.Text.Trim();
                oCustomerDetailsEntity.MiddleName = txtMiddleName.Text.Trim();
                oCustomerDetailsEntity.LastName = txtLastName.Text.Trim();
                oCustomerDetailsEntity.ADDRESS1 = txtAdd1.Text.Trim();
                oCustomerDetailsEntity.ADDRESS2 = txtAdd2.Text.Trim();
                oCustomerDetailsEntity.ADDRESS3 = txtAdd3.Text.Trim();
                oCustomerDetailsEntity.PinCode = txtPincode.Text.Replace(" ","").Trim();
                oCustomerDetailsEntity.BankAccNo = txtBankAccNo.Text.Replace(" ", "").Trim();
                int Iresult = oCustomerDetails.InsertCustomer(oCustomerDetailsEntity);
                List<SearchCustomerDetailsResult> oSearchCustomerDetailsResult = oCustomerDetails.SearchCustomer(oCustomerDetailsEntity);
                foreach (SearchCustomerDetailsResult cmd in oSearchCustomerDetailsResult)
                    txtCustID.Text = cmd.CustomerID.ToString();

                if (Iresult == 0)
                    MessageBox.ShowBox("MessageID253", BMC_Icon.Information);
                else if (Iresult == 3)
                    MessageBox.ShowBox("MessageID254", BMC_Icon.Warning);
                else
                    MessageBox.ShowBox("MessageID252", BMC_Icon.Warning);
                delCustomerUpdated(this, e);
                CustomerDetails.IsShowBankAccNo = false;
                IsCancelClicked = false;
                Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                delCustomerCancelled(this, e);
                CustomerDetails.IsShowBankAccNo = false;
                Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SomeCharKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                if (!CustomerDetailsConstants.AllowedAlphabets.Contains(e.Key)) { e.Handled = true; }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SomeNumericKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift || (!CustomerDetailsConstants.AllowedNumerics.Contains(e.Key)))
                {
                    //int keyNumber = (int)e.Key;

                    //if (keyNumber > 69 || keyNumber < 44)
                    //{
                    //    if (!CustomerDetailsConstants.AllowedNumerics.Contains(e.Key)) { e.Handled = true; }
                    e.Handled = true;
                    //}
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SomeAlphaNumericKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                if (!CustomerDetailsConstants.AllowedNumerics.Contains(e.Key)&&
                    !CustomerDetailsConstants.AllowedAlphabets.Contains(e.Key)) { e.Handled = true; }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SomeAlphaNumericwithSpecialCharactersKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                if (!CustomerDetailsConstants.AllowedNumerics.Contains(e.Key) &&
                    !CustomerDetailsConstants.AllowedAlphabets.Contains(e.Key) &&
                    !CustomerDetailsConstants.AllowedSpecialCharacters.Contains(e.Key)) 
                { e.Handled = true; }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }      

        private void SomeTextPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Regex AlphaPattern = new Regex("[^a-zA-Z]");
            try
            {
                if (((TextBox)sender).Name == "txtPincode" || ((TextBox)sender).Name == "txtBankAccNo")
                {
                    ((TextBox)sender).Text = DisplayNumberPad(((TextBox)sender).Text);
                }
                else
                {
                    if (!BMC.Transport.Settings.OnScreenKeyboard)
                        return;
                    ((TextBox)sender).Text = DisplayKeyboard(((TextBox)sender).Text, string.Empty);
                  
                    if (AlphaPattern.IsMatch(((TextBox)sender).Text) && (((TextBox)sender).Name == "txtFirstName" ||
                        ((TextBox)sender).Name == "txtLastName" || ((TextBox)sender).Name == "txtMiddleName"))
                    {
                        ((TextBox)sender).Text = AlphaPattern.Replace(((System.Windows.Controls.TextBox)sender).Text, string.Empty);                   
                        return;
                    }
                   
                    ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void ChooseCustomer(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Wait;
                SearchCustomerDetailsResult dr = (SearchCustomerDetailsResult)lstViewCstomers.SelectedItem;

                txtCustID.Text = dr.CustomerID.ToString();
                cmbTitle.SelectedValue = dr.Title;
                txtFirstName.Text = dr.FirstName;
                txtMiddleName.Text = dr.MiddleName;
                txtLastName.Text = dr.LastName;
                txtAdd1.Text = dr.ADDRESS1;
                txtAdd2.Text = dr.ADDRESS2;
                txtAdd3.Text = dr.ADDRESS3;
                txtPincode.Text = dr.PinCode;
                txtBankAccNo.Text = dr.BankAccNo;

                lstViewCstomers.Visibility = Visibility.Hidden;
                pnlHeader.Height = 0;
                pnlHeader.Opacity = 0;           

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }       

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbTitle.Items.Add("Mr.");
            cmbTitle.Items.Add("Ms.");
            cmbTitle.Items.Add("Mrs.");
            cmbTitle.IsEnabled = false;
            btnSaveCustomer.IsEnabled = false;
            btnSearch.IsEnabled = false;
            
            
            txtFirstName.IsEnabled = false;
            txtMiddleName.IsEnabled = false;
            txtLastName.IsEnabled = false;
            txtAdd1.IsEnabled = false;
            txtAdd2.IsEnabled = false;
            txtAdd3.IsEnabled = false;

            txtPincode.IsEnabled = false;
            txtBankAccNo.IsEnabled = false;
            

            this.txtCustID.Text = "";
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FetchCustomerId();
                OnLoad();
                btnAddCustomer.IsEnabled = false;
                btnSearch.IsEnabled = true;
                btnSaveCustomer.IsEnabled = true;
                IsCancelClicked = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void FetchCustomerId()
        {
            try
            {
                long LastCustomer = oCustomerDetails.RecentCustomerID();
                if (LastCustomer == 0)
                {
                    LastCustomer = 1000;
                }
                else
                {
                    LastCustomer = LastCustomer + 1;
                }
                this.txtCustID.Text = LastCustomer.ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void OnLoad()
        {
            cmbTitle.IsEnabled = true;
            txtFirstName.IsEnabled = true;
            txtMiddleName.IsEnabled = true;
            txtLastName.IsEnabled = true;
            txtAdd1.IsEnabled = true;
            txtAdd2.IsEnabled = true;
            txtAdd3.IsEnabled = true;
            txtPincode.IsEnabled = true;
            txtBankAccNo.IsEnabled = (CustomerDetails.IsShowBankAccNo==true)?true:false;
            clearControls();
        }

        private void clearControls()
        {
            txtFirstName.Text = string.Empty;
            txtMiddleName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtAdd1.Text = string.Empty;
            txtAdd2.Text = string.Empty;
            txtAdd3.Text = string.Empty;
            txtPincode.Text = string.Empty;
            txtBankAccNo.Text = string.Empty;
            cmbTitle.SelectedIndex = 0;
            cmbTitle.Focus();
        }

        void BindTextsAgain()//To apply validation rules
        {
                txtFirstName.Text = txtFirstName.Text;
                txtMiddleName.Text = txtMiddleName.Text;
                txtLastName.Text = txtLastName.Text;
                txtAdd1.Text = txtAdd1.Text;
                txtAdd2.Text = txtAdd2.Text;
                txtAdd3.Text = txtAdd3.Text;
                txtPincode.Text = txtPincode.Text;
                txtBankAccNo.Text = txtBankAccNo.Text;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnSearch.IsEnabled = false;
                IsCancelClicked = true;
                BindTextsAgain();
                IsCancelClicked = false;
                oCustomerDetailsEntity.FirstName = txtFirstName.Text;
                oCustomerDetailsEntity.LastName = txtLastName.Text;
                oCustomerDetailsEntity.PinCode = txtPincode.Text;
                oCustomerDetailsEntity.BankAccNo = txtBankAccNo.Text;
                List<SearchCustomerDetailsResult> oSearchCustomerDetailsResult = oCustomerDetails.SearchCustomer(oCustomerDetailsEntity);
                if (oSearchCustomerDetailsResult != null)
                {
                    if (oSearchCustomerDetailsResult.Count > 0)
                    {
                        lstViewCstomers.ItemsSource = oSearchCustomerDetailsResult;
                        lstViewCstomers.Visibility = Visibility.Visible;
                        pnlHeader.Visibility = Visibility.Visible;
                        pnlHeader.Height = 300;
                        pnlHeader.Margin = new Thickness(15.42, -500, 27, 0);
                        pnlHeader.Opacity = 1.0;
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID255", BMC_Icon.Warning);
                        lstViewCstomers.Visibility = Visibility.Hidden;
                        pnlHeader.Visibility = Visibility.Hidden;
                        pnlHeader.Height = 0;
                        pnlHeader.Opacity = 0;
                        BindTextsAgain();

                    }
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                btnSearch.IsEnabled = true;
            }
        }
        private void btnSelectCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnSelectCustomer.IsEnabled = false;
                SelectCustomer();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                btnSelectCustomer.IsEnabled = true;
            }
        }
        private void SelectCustomer()
        {
            try
            {
                this.Cursor = Cursors.Wait;
                SearchCustomerDetailsResult dr = (SearchCustomerDetailsResult)lstViewCstomers.SelectedItem;

                txtCustID.Text = dr.CustomerID.ToString();
                cmbTitle.SelectedValue = dr.Title;
                txtFirstName.Text = dr.FirstName;
                txtMiddleName.Text = dr.MiddleName;
                txtLastName.Text = dr.LastName;
                txtAdd1.Text = dr.ADDRESS1;
                txtAdd2.Text = dr.ADDRESS2;
                txtAdd3.Text = dr.ADDRESS3;
                txtPincode.Text = dr.PinCode;
                txtBankAccNo.Text = dr.BankAccNo;

                lstViewCstomers.Visibility = Visibility.Hidden;
                pnlHeader.Height = 0;
                pnlHeader.Opacity = 0;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }       

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            lstViewCstomers.Visibility = Visibility.Hidden;
            pnlHeader.Height = 0;
            pnlHeader.Opacity = 0;
            IsCancelClicked = false;
            BindTextsAgain();
        }

        #region Keyboard events

        void objKeyboard_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (((KeyboardInterface)sender).DialogResult == true)
            {
                strKeyText = ((KeyboardInterface)sender).KeyString;
            }
        }

        private string DisplayKeyboard(string KeyText, string type)
        {
            strKeyText = "";
            KeyboardInterface objKeyboard = new KeyboardInterface();
            if (type == "Pwd")
            {
                objKeyboard.IsPwd = true;
            }
            objKeyboard.Closing += new System.ComponentModel.CancelEventHandler(objKeyboard_Closing);
            objKeyboard.KeyString = KeyText;
            Point locationFromScreen = this.PointToScreen(new Point(0, 0));
            PresentationSource source = PresentationSource.FromVisual(this);
            System.Windows.Point targetPoints = source.CompositionTarget.TransformFromDevice.Transform(locationFromScreen);            
            objKeyboard.Top = targetPoints.Y + this.Height / 2;
            objKeyboard.Left = targetPoints.X;
            objKeyboard.ShowDialog();
            return strKeyText;
        }

        private string DisplayNumberPad(string keytext)
        {
            string strNumberPadText = string.Empty;
            NumberPadWind ObjNumberpadWind = new NumberPadWind();     

            try
            {

                ObjNumberpadWind.ValueText = keytext;

                if (ObjNumberpadWind.ShowDialog() == true)
                {
                    if (ObjNumberpadWind.ValueText == "")                    
                        strNumberPadText = "0";                    
                    else                    
                        strNumberPadText = ObjNumberpadWind.ValueText;                    
                }
            }
            catch (Exception ex)
            {
                strNumberPadText = ObjNumberpadWind.ValueText;
                ObjNumberpadWind.Close();
                ExceptionManager.Publish(ex);
            }
            return strNumberPadText;

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
                        ((BMC.Presentation.POS.Views.CustomerDetails)(this)).Loaded -= (this.Window_Loaded);
                        this.txtFirstName.KeyDown -= (this.SomeCharKeyDown);
                        this.txtFirstName.PreviewMouseUp -= (this.SomeTextPreviewMouseUp);
                        this.txtLastName.KeyDown -= (this.SomeCharKeyDown);
                        this.txtLastName.PreviewMouseUp -= (this.SomeTextPreviewMouseUp);
                        this.txtAdd1.KeyDown -= (this.SomeAlphaNumericwithSpecialCharactersKeyDown);
                        this.txtAdd1.PreviewMouseUp -= (this.SomeTextPreviewMouseUp);
                        this.txtAdd2.KeyDown -= (this.SomeAlphaNumericwithSpecialCharactersKeyDown);
                        this.txtAdd2.PreviewMouseUp -= (this.SomeTextPreviewMouseUp);
                        this.txtAdd3.KeyDown -= (this.SomeAlphaNumericwithSpecialCharactersKeyDown);
                        this.txtAdd3.PreviewMouseUp -= (this.SomeTextPreviewMouseUp);
                        this.txtPincode.KeyDown -= (this.SomeNumericKeyDown);
                        this.txtPincode.PreviewMouseUp -= (this.SomeTextPreviewMouseUp);
                        this.txtBankAccNo.KeyDown -= (this.SomeNumericKeyDown);
                        this.txtBankAccNo.PreviewMouseUp -= (this.SomeTextPreviewMouseUp);
                        this.btnSearch.Click -= (this.btnSearch_Click);
                        this.btnAddCustomer.Click -= (this.btnAddCustomer_Click);
                        this.btnCancel.Click -= (this.btnCancel_Click);
                        this.btnSelectCustomer.Click -= (this.btnSelectCustomer_Click);
                        this.btnExit.Click -= (this.btnExit_Click);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("CustomerDetails objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CustomerDetails"/> is reclaimed by garbage collection.
        /// </summary>
        ~CustomerDetails()
        {
            Dispose(false);
        }

        #endregion
       
    }
    public class CustomerEntity :ValidationRule, INotifyPropertyChanged
    {
        private int _CustomerID=0;       
        private string _Title=string.Empty;
        private string _FirstName = string.Empty;
        private string _MiddleName = string.Empty;
        private string _LastName = string.Empty;
        private string _ADDRESS1 = string.Empty;
        private string _ADDRESS2 = string.Empty;
        private string _ADDRESS3 = string.Empty;
        private string _PinCode = string.Empty;
        private string _BankAccNo = string.Empty;
        private string _FieldName = string.Empty;

        private int _minimumLength = -1;
        private int _maximumLength = 999;
        private string _errorMessage;


        public int MinimumLength
        { get { return _minimumLength; } set { _minimumLength = value; }     }

        public int MaximumLength
        {  get { return _maximumLength; }  set { _maximumLength = value; }  }

        public string ErrorMessage
        {         get { return _errorMessage; }  set { _errorMessage = value; } }
        public string FieldName
        { get { return _FieldName; } set { _FieldName = value; } }
        public int CustomerID            
        { get { return _CustomerID; }set { _CustomerID = value;OnPropertyChanged("CustomerID"); }}
        public string Title
        { get{return _Title; } set {_Title=value; OnPropertyChanged("Title");}}
        public string FirstName
        { get { return _FirstName; } set { _FirstName = value; OnPropertyChanged("FirstName");}}
        public string MiddleName
        { get { return _MiddleName; } set { _MiddleName = value; OnPropertyChanged("MiddleName"); } }
        public string LastName
        { get { return _LastName; } set { _LastName = value; OnPropertyChanged("LastName"); } }
        public string ADDRESS1
        { get { return _ADDRESS1; } set { _ADDRESS1 = value; OnPropertyChanged("ADDRESS1"); } }
        public string ADDRESS2
        { get { return _ADDRESS2; } set { _ADDRESS2 = value; OnPropertyChanged("ADDRESS2"); } }
        public string ADDRESS3
        { get { return _ADDRESS3; } set { _ADDRESS3 = value; OnPropertyChanged("ADDRESS3"); } }
        public string PinCode
        { get { return _PinCode; } set { _PinCode = value; OnPropertyChanged("PinCode"); } }
        public string BankAccNo
        { get { return _BankAccNo; } set { _BankAccNo = value; OnPropertyChanged("BankAccNo"); } }
       
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);
            try
            {
                if (CustomerDetails.IsCancelClicked)
                    return result;
                string inputString = (value.ToString().Trim() ?? string.Empty).ToString();
                if ((this.FieldName.ToUpper() == "BANKACCNO" && CustomerDetails.IsShowBankAccNo == false))
                    return result;
                else
                {
                    if (inputString.Length < this.MinimumLength)
                    {
                        //result = new ValidationResult(false, String.Format("Please don't leave {0} empty.", FieldName));
                        result = new ValidationResult(false, this.ErrorMessage);
                        return result;
                    }
                    else if ((this.MaximumLength > 0 && inputString.Length > this.MaximumLength))
                    {
                        result = new ValidationResult(false, String.Format("Please enter values between {0} and {1}.", this.MinimumLength, this.MaximumLength));
                        return result;
                    }
                }
                if (this.FieldName.ToUpper() == "PINCODE" && value.ToString().Trim().Contains(' '))
                {
                    result = new ValidationResult(false, "Pincode can not contain special character(s)");
                    return result;
                }
                if (this.FieldName.ToUpper() == "BANKACCNO" && value.ToString().Trim().Contains(' '))
                {
                    result = new ValidationResult(false, "Bank Account No can not contain special character(s)");
                    return result;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return result;

        }
       
    }
}
