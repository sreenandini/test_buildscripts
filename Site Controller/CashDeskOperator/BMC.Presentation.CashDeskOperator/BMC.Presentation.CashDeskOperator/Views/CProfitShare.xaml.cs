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
using System.Data;
using BMC.Transport;
using System.Data.Linq;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Business.CashDeskOperator;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.CommonLiquidation.Utilities;
using BMC.Common.ExceptionManagement;
using BMC.Common.Utilities;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CProfitShare.xaml
    /// </summary>
    public partial class CProfitShare : Window
    {
        #region DataMembers and Properties

        private ProfitShareConfiguration oProfitShareConfiguration = ProfitShareConfiguration.ProfitShareConfigurationInstance;
        private List<ProfitShareGroup> lstProfitShareGroup = null;
        private List<ExpenseShareGroup> lstExpenseShareGroup = null;
        private List<PayPeriods> lstPayPeriods = null;
        private PayPeriods objPayPeriods = new PayPeriods();
        private CommonLiquidationEntity _objCommonLiquidation = null;
        private int SiteId;

        public CommonLiquidationEntity objCommonLiquidation
        {
            get
            {
                return _objCommonLiquidation;
            }
            set
            {
                _objCommonLiquidation = value;
            }
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
            Key.Decimal,
            Key.Enter,
            Key.Back,
            Key.Delete,
            Key.Tab
        };

        #endregion //DataMembers and Properties

        #region Constructor

        public CProfitShare(CommonLiquidationEntity objCommonReadLiquidation)
        {
            this.objCommonLiquidation = objCommonReadLiquidation;
            InitializeComponent();
            LoadProfitShareGroup();
            LoadExpenseShareGroup();
            LoadPayPeriods();
            LoadAmounts();
            EnableDisableControls();
        }

        public CProfitShare(CommonLiquidationEntity entity, int iSiteID, int iBatchNo)
            : this(entity)
        {

            _objCommonLiquidation = entity;
            SiteId = iSiteID;

            string sSettingValue = string.Empty;
            CommonLiquidationDataContext objCommonLiquidation = new CommonLiquidationDataContext(BMC.Business.CashDeskOperator.CommonUtilities.ExchangeConnectionString);

            if (!Settings.ExpenseShare)
            {
                txtblkExpenseShareAmount.Visibility = Visibility.Collapsed;
                txtExpenseShareAmount.Visibility = Visibility.Collapsed;
                txtblkExpenseShareGroup.Visibility = Visibility.Collapsed;
                cboExpenseShareGroup.Visibility = Visibility.Collapsed;
            }

            if (!Settings.WriteOffShare)
            {
                txtblkWriteOffExpense.Visibility = Visibility.Collapsed;
                txtWriteOffExpense.Visibility = Visibility.Collapsed;
            }

            LoadProfitShareGroup();
            LoadExpenseShareGroup();
            LoadPayPeriods();
            LoadAmounts();
            EnableDisableControls();
        }

        #endregion //Constructor

        #region Events


        private void txtWriteoffAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !AreAllValidNumericChars(e.Text);
        }

        private bool AreAllValidNumericChars(string str)
        {
            foreach (char c in str)
            {
                if (!Char.IsNumber(c)) return false;
            }

            return true;
        }


        private void txtWriteOffExpense_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (!BMC.Transport.Settings.OnScreenKeyboard)
                    return;

                string sValue;
                sValue = DisplayNumberPad(((TextBox)sender).Text);
                ((TextBox)sender).Text = sValue;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtExpenseShareAmount_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (!BMC.Transport.Settings.OnScreenKeyboard)
                    return;

                string sValue;
                sValue = DisplayNumberPad(((TextBox)sender).Text);
                ((TextBox)sender).Text = sValue;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtExpenseShareAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                txtExpenseShareAmount.Text = string.IsNullOrEmpty(txtExpenseShareAmount.Text) ? "0.00" : txtExpenseShareAmount.Text;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtWriteOffExpense_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                txtWriteOffExpense.Text = string.IsNullOrEmpty(txtWriteOffExpense.Text) ? "0.00" : txtWriteOffExpense.Text;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cboProfitShareGroup.SelectedIndex <= 0)
                {
                    MessageBox.ShowBox("MessageID490", BMC_Icon.Information);
                    return;
                }

                decimal FixedExpenseAmount = (Convert.ToDecimal(txtExpenseShareAmount.Text) * (Convert.ToDecimal(((ExpenseShareGroup)cboExpenseShareGroup.SelectedItem).ExpenseSharePercentage) / 100));
                decimal dTotal = Convert.ToDecimal(FixedExpenseAmount) + Convert.ToDecimal(txtCarriedForwardAmount.Text);
                if (Math.Round(Convert.ToDecimal(txtWriteOffExpense.Text), 2, MidpointRounding.AwayFromZero) > (Math.Round(FixedExpenseAmount, 2) + Math.Round(Convert.ToDecimal(txtCarriedForwardAmount.Text), 2, MidpointRounding.AwayFromZero)))
                {
                    CAuthorize oCAuthorize = new CAuthorize("BMC.Presentation.CProfitShare.ProfitShareApprover");
                    oCAuthorize.ShowDialog();
                    if (!oCAuthorize.IsAuthorized)
                        return;
                    //string strMsg = Application.Current.FindResource("MessageID890").ToString().Replace("@@@@", Convert.ToDecimal(txtWriteOffExpense.Text).ToString("#,##0.00")).Replace("****", dTotal.ToString("#,##0.00"));
                    //MessageBox.ShowBox(strMsg, BMC_Icon.Error, true);
                }
                objCommonLiquidation.ProfitShareGroupId = Convert.ToInt32(cboProfitShareGroup.SelectedValue);
                objCommonLiquidation.ExpenseShareGroupID = Convert.ToInt32(cboExpenseShareGroup.SelectedValue);
                objCommonLiquidation.ExpenseShareAmount = Convert.ToDecimal(txtExpenseShareAmount.Text);
                objCommonLiquidation.WriteOffAmount = Convert.ToDecimal(txtWriteOffExpense.Text);
                objCommonLiquidation.PayPeriodId = objPayPeriods == null ? 0 : objPayPeriods.Calendar_Period_ID;
                objCommonLiquidation.Percentage_Setting = Convert.ToDecimal(((ProfitShareGroup)cboProfitShareGroup.SelectedItem).ProfitSharePercentage);
                objCommonLiquidation.ExpenseSharePercentage = Convert.ToDecimal(((ExpenseShareGroup)cboExpenseShareGroup.SelectedItem).ExpenseSharePercentage);
                this.Close();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtWriteOffExpense_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                e.Handled = !CustomerDetailsConstants.AllowedNumerics.Contains(e.Key);

                //if (e.Key == Key.OemComma)
                //    e.Handled = false;

                //if (e.Key == Key.OemPeriod || e.Key == Key.Decimal)
                //    e.Handled = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtExpenseShareAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                e.Handled = !CustomerDetailsConstants.AllowedNumerics.Contains(e.Key);

                //if (e.Key == Key.OemComma)
                //    e.Handled = false;

                //if (e.Key == Key.OemPeriod || e.Key == Key.Decimal)
                //    e.Handled = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion //Events

        #region Methods

        private void LoadAmounts()
        {
            try
            {
                txtExpenseShareAmount.Text = objCommonLiquidation.ExpenseShareAmount.ToString("#,##0.00");
                txtWriteOffExpense.Text = objCommonLiquidation.WriteOffAmount.ToString("#,##0.00");
                txtCarriedForwardAmount.Text = objCommonLiquidation.PrevCarriedForwardExpense.GetValueOrDefault().ToString("#,##0.00");
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadProfitShareGroup()
        {
            try
            {
                lstProfitShareGroup = oProfitShareConfiguration.GetProfitShareGroupList();
                cboProfitShareGroup.ItemsSource = lstProfitShareGroup;
                cboProfitShareGroup.DisplayMemberPath = "ProfitShareGroupName";
                cboProfitShareGroup.SelectedValuePath = "ProfitShareGroupID";
                cboProfitShareGroup.SelectedValue = objCommonLiquidation.ProfitShareGroupId;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadExpenseShareGroup()
        {
            try
            {
                lstExpenseShareGroup = oProfitShareConfiguration.GetExpenseShareGroupList();
                cboExpenseShareGroup.ItemsSource = lstExpenseShareGroup;
                cboExpenseShareGroup.DisplayMemberPath = "ExpenseShareGroupName";
                cboExpenseShareGroup.SelectedValuePath = "ExpenseShareGroupID";
                cboExpenseShareGroup.SelectedValue = objCommonLiquidation.ExpenseShareGroupID;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadPayPeriods()
        {
            try
            {
                lstPayPeriods = oProfitShareConfiguration.GetPayPeriods();
                objPayPeriods = lstPayPeriods.Where(item => item.Calendar_Period_Start_Date <= DateTime.Today && item.Calendar_Period_End_Date >= DateTime.Today).FirstOrDefault() as PayPeriods;

                if (objPayPeriods == null)
                {
                    objPayPeriods = new PayPeriods();
                    return;
                };
                txtblkPayPeriod.Text = objPayPeriods.Calendar_Period;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private string DisplayNumberPad(string keytext)
        {
            string strNumberPadText = string.Empty;
            NumberPadWind ObjNumberpadWind = new NumberPadWind(true);
            ObjNumberpadWind.isPlayerClub = true;

            try
            {

                ObjNumberpadWind.ValueText = keytext;

                if (ObjNumberpadWind.ShowDialog() == true)
                {
                    if (ObjNumberpadWind.ValueText == "")
                    {
                        strNumberPadText = "0.00";
                    }
                    else
                    {
                        strNumberPadText = ObjNumberpadWind.ValueText;
                    }
                }
                else
                {
                    strNumberPadText = "0.00";
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

        private string DisplayNumPadWin(string keytext)
        {
            string strNumPadText = string.Empty;
            NumPadWin ObjNumpadWin = new NumPadWin();

            try
            {
                ObjNumpadWin.ValueText = keytext;
                if (ObjNumpadWin.ShowDialog() == true)
                {
                    if (ObjNumpadWin.ValueText == "")
                    {
                        strNumPadText = "0";
                    }
                    else
                    {
                        strNumPadText = ObjNumpadWin.ValueText;
                    }
                }
            }

            catch (Exception ex)
            {
                strNumPadText = ObjNumpadWin.ValueText;
                ObjNumpadWin.Close();
                ExceptionManager.Publish(ex);
            }
            return strNumPadText;
        }

        private void EnableDisableControls()
        {
            try
            {
                if (!Settings.ExpenseShare)
                {
                    txtblkExpenseShareGroup.Visibility = Visibility.Collapsed;
                    cboExpenseShareGroup.Visibility = Visibility.Collapsed;
                    txtblkExpenseShareAmount.Visibility = Visibility.Collapsed;
                    txtExpenseShareAmount.Visibility = Visibility.Collapsed;

                    LayoutRoot.RowDefinitions[2].Height = new GridLength(0);
                    LayoutRoot.RowDefinitions[3].Height = new GridLength(0);
                }

                if (!Settings.WriteOffShare)
                {
                    txtblkWriteOffExpense.Visibility = Visibility.Collapsed;
                    txtWriteOffExpense.Visibility = Visibility.Collapsed;

                    LayoutRoot.RowDefinitions[4].Height = new GridLength(0);
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion //Methods

        private void txtExpenseShareAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !AreAllValidNumericChars(e.Text);
        }

        //Enable disable Expense share and writeoff amount based on Expense share group selection
        private void cboExpenseShareGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboExpenseShareGroup.SelectedIndex != 0)
                {
                    txtExpenseShareAmount.IsEnabled = true;
                    txtWriteOffExpense.IsEnabled = true;
                }
                else
                {
                    txtExpenseShareAmount.IsEnabled = false;
                    txtWriteOffExpense.IsEnabled = false;
                    txtExpenseShareAmount.Text = "0.00";
                    txtWriteOffExpense.Text = "0.00";
                }
            }
            catch (Exception ex)
            {                
                ExceptionManager.Publish(ex);
            }
            
        }

    }

}