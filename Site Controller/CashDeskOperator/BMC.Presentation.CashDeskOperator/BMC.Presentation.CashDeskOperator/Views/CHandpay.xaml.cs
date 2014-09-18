using System;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BMC.Common.ExceptionManagement;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.CashDeskOperator.BusinessObjects;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CHandpay.xaml
    /// </summary>
    public partial class CHandpay : UserControl
    {

        public CHandpay()
        {
            InitializeComponent();

            this.ucValueCalcComp.MaxLength = 11;

        }

        #region Declarations
        int iTreasuryID = 0;
        private string strTextValue = string.Empty;
        TextBox txtBox = null;

        public const string TREASURY_HANDPAY_JACKPOT = "Handpay Jackpot";
        public const string TREASURY_HANDPAY_CREDIT = "Handpay Credit";
        public const string TREASURY_SHORTPAY = "Shortpay";
        public const string TREASURY_CASH_DESK_FLOAT = "Cash Desk Float";
        public const string TREASURY_FLOAT = "Float";
        public const string TREASURY_PROG = "Prog";
        public const string TREASURY_VOID = "VOID";
        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        #endregion

        public void GetUnprocessedHandpays(object sender, RoutedEventArgs e)
        {
            try
            {
                FillHandpays();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void FillHandpays()
        {
            IHandpay objCashDeskOperator = HandpayBusinessObject.CreateInstance();
            DataTable dtHandpay = objCashDeskOperator.GetUnprocessedHandpays();

            foreach (DataRow row in dtHandpay.Rows)
            {
                row["Amount"] = CommonBusinessObjects.GetCurrency(Convert.ToDouble(row["Amount"]));
            }
            Binding bind = new Binding();
            bind.Source = dtHandpay;

            lvUnprocessedHandpays.SetBinding(ListView.ItemsSourceProperty, bind);
     
            if (lvUnprocessedHandpays.Items.Count > 0)
            {
                lvUnprocessedHandpays.SelectedItem = 0;
                DataRowView row = (DataRowView)lvUnprocessedHandpays.SelectedItem;
                txtSelectedValue.Text = row["Amount"].ToString();
            }
        }

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            SaveDetails_EventsFromConnexus(false);
        }


        private void SaveDetails_EventsFromConnexus(bool IsVoid)
        {
            string strTreasuryType = string.Empty;
            float fTreasuryAmount = 0.00F;
            try
            {
                Treasury objHandpayEntity = new Treasury();
                IHandpay objCashDeskOperator = HandpayBusinessObject.CreateInstance();
                DataRowView objHandpays = null;

                if (lvUnprocessedHandpays.SelectedItem != null)
                {
                    objHandpays = (DataRowView)lvUnprocessedHandpays.SelectedItem;
                    strTreasuryType = objHandpays.Row["Type"].ToString();

                    if (strTreasuryType.ToUpper() == "HANDPAY")
                    {
                        strTreasuryType = TREASURY_HANDPAY_CREDIT; ;
                    }
                    else if (strTreasuryType.ToUpper() == "JACKPOT")
                    {
                        strTreasuryType = TREASURY_HANDPAY_JACKPOT;
                    }
                    else if (strTreasuryType.ToUpper() == "PROG")
                    {
                        strTreasuryType = TREASURY_PROG;
                    }

                    objHandpayEntity.TreasuryType = strTreasuryType;

                    objHandpayEntity.CollectionNumber = 0;
                    objHandpayEntity.InstallationNumber = int.Parse(objHandpays.Row["Installation_No"].ToString());
                    objHandpayEntity.UserID = BMC.Common.clsSecurity.UserID;
                    objHandpayEntity.TreasuryReason = "";
                    objHandpayEntity.TreasuryReasonCode = 0;
                    objHandpayEntity.TreasuryAllocated = 0;
                    objHandpayEntity.TreasuryBreakdown100p = 0;
                    objHandpayEntity.TreasuryBreakdown10p = 0;
                    objHandpayEntity.TreasuryBreakdown200p = 0;
                    objHandpayEntity.TreasuryBreakdown20p = 0;
                    objHandpayEntity.TreasuryBreakdown2p = 0;
                    objHandpayEntity.TreasuryBreakdown50p = 0;
                    objHandpayEntity.TreasuryBreakdown5p = 0;
                    objHandpayEntity.TreasuryIssuerUserNo = BMC.Common.clsSecurity.UserID;
                    objHandpayEntity.TreasuryMembershipNo = "0";

                    objHandpayEntity.TreasuryAmount = float.Parse(objHandpays.Row["Amount"].ToString());
                    fTreasuryAmount = objHandpayEntity.TreasuryAmount;

                    iTreasuryID = objCashDeskOperator.ProcessHandPay(objHandpayEntity);

                    string strTEID = string.Empty;
                    strTEID = objHandpays.Row["TE_Id"].ToString();
                    if (objCashDeskOperator.UpdateTicketException(strTEID) == false)
                    {
                        BMC.Common.LogManagement.LogManager.WriteLog("Error while updating ticket exception table", BMC.Common.LogManagement.LogManager.enumLogLevel.Info);
                    }

                    if (IsVoid == true) { fTreasuryAmount = -fTreasuryAmount; }

                    //MachineDetails.TreasuryNo = iTreasuryID.ToString();
                    //MachineDetails.Value = objHandpayEntity.TreasuryAmount.ToString();

                    if (strTreasuryType == TREASURY_HANDPAY_CREDIT)
                    {
                        strTreasuryType = "Handpay";
                    }
                    (oCommonUtilities.CreateInstance()).PrintCommonReceipt(strTreasuryType);

                    objCashDeskOperator.ClearHandpayLock(int.Parse(objHandpays.Row["Installation_No"].ToString()));

                    if (IsVoid == false)
                    {
                        MessageBox.showBox("Data is now saved", BMC_Icon.Information);
                    }
                    txtSelectedValue.Text = string.Empty;

                    FillHandpays();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            optHandpay.IsChecked = true;
            FillHandpays();
        }


        private void lvUnprocessedHandpays_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lvUnprocessedHandpays.Items.Count > 0)
                {
                    if (lvUnprocessedHandpays.SelectedItem != null)
                    {
                        DataRowView objhandpay = (DataRowView)lvUnprocessedHandpays.SelectedItem;                   
                        txtSelectedValue.Text = objhandpay.Row["Amount"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnVoid_Click(object sender, RoutedEventArgs e)
        {
            IVoidTransaction objCashDeskOperator = VoidTransactionBusinessObject.CreateInstance();
            if (lvUnprocessedHandpays.Items.Count == 0)
            {
                MessageBox.showBox("No handpay entry exists to make it Void", BMC_Icon.Information);
                return;
            }
            else
                if (lvUnprocessedHandpays.SelectedIndex != -1)
                {
                    System.Windows.Forms.DialogResult dr = System.Windows.Forms.MessageBox.Show("Are you sure you want to void this handpay?",
                        "Cash Desk Operator", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
                    if (dr.ToString() == "Yes")
                    {
                        SaveDetails_EventsFromConnexus(true);

                        VoidTranCreate objVoidTreasury = new VoidTranCreate();
                        objVoidTreasury.UserNo = BMC.Common.clsSecurity.UserID.ToString();
                        objVoidTreasury.Time = DateTime.Now.ToString("HH:mm:ss");
                        objVoidTreasury.Date = DateTime.Now.Date.ToString("dd MMM yyyy");
                        objVoidTreasury.TreasuryID = iTreasuryID.ToString();

                        int iResult = objCashDeskOperator.VoidTransaction(objVoidTreasury);

                        if (iResult == -1)
                        {
                            MessageBox.showBox("This Handpay entry has already been voided!", BMC_Icon.Information);
                        }
                        else if (iResult == 0)
                        {
                            MessageBox.showBox("The Handpay entry voided successfully ", BMC_Icon.Information);
                        }
                        else
                        {
                            MessageBox.showBox("Error occured while voiding Handpay entry", BMC_Icon.Information);
                            return;
                        }
                    }
                }

            FillHandpays();

        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (ValidInfo())
            {
                SaveManualHandpay();
            }
        }


        private bool ValidInfo()
        {
            if (txtBox != null)
            {
                if (txtBox.Text.Length == 0)
                {
                    MessageBox.showBox("Enter the amount of handpay", BMC_Icon.Error);
                    return false;
                }
                if (txtBox.Text.Length > 8)
                {
                    MessageBox.showBox("Only 6 numbers allowed", BMC_Icon.Information);
                    txtBox.Text = "0.00";
                    return false;
                }
            }
            else
            {
                MessageBox.showBox("Please enter a amount", BMC_Icon.Information);
                return false;
            }
            return true;

        }

        private void SaveManualHandpay()
        {
            string strTreasuryType = string.Empty;
            try
            {
                Treasury objHandpayEntity = new Treasury();
                if (optHandpay.IsChecked == true)
                {
                    strTreasuryType = TREASURY_HANDPAY_CREDIT; ;
                }
                else if (optJackpot.IsChecked == true)
                {
                    strTreasuryType = TREASURY_HANDPAY_JACKPOT;
                }
                else
                {
                    strTreasuryType = TREASURY_PROG;
                }

                objHandpayEntity.TreasuryType = strTreasuryType;
                DataRowView rowview = (DataRowView)lvMachines.SelectedItem;

                objHandpayEntity.InstallationNumber = int.Parse(rowview.Row[3].ToString());                
                objHandpayEntity.UserID = BMC.Common.clsSecurity.UserID;
                objHandpayEntity.TreasuryReason = "";
                objHandpayEntity.TreasuryReasonCode = 0;
                objHandpayEntity.TreasuryAllocated = 0;
                objHandpayEntity.TreasuryBreakdown100p = 0;
                objHandpayEntity.TreasuryBreakdown10p = 0;
                objHandpayEntity.TreasuryBreakdown200p = 0;
                objHandpayEntity.TreasuryBreakdown20p = 0;
                objHandpayEntity.TreasuryBreakdown2p = 0;
                objHandpayEntity.TreasuryBreakdown50p = 0;
                objHandpayEntity.TreasuryBreakdown5p = 0;
                objHandpayEntity.TreasuryIssuerUserNo = BMC.Common.clsSecurity.UserID;
                objHandpayEntity.TreasuryMembershipNo = "0";

                if (txtBox != null)
                {
                    objHandpayEntity.TreasuryAmount = float.Parse(txtBox.Text);
                }

                IHandpay objCashDeskOperator = HandpayBusinessObject.CreateInstance();
                int iTreasuryID = objCashDeskOperator.ProcessHandPay(objHandpayEntity);
                if (iTreasuryID != 0)
                {
                    MessageBox.showBox("This Data is now saved", BMC_Icon.Information);

                    //MachineDetails.TreasuryNo = iTreasuryID.ToString();
                    //MachineDetails.Value = objHandpayEntity.TreasuryAmount.ToString();
                    if (strTreasuryType == TREASURY_HANDPAY_CREDIT)
                    {
                        strTreasuryType = "Handpay";
                    }
                    (oCommonUtilities.CreateInstance()).PrintCommonReceipt(strTreasuryType);
                    
                    txtBox.Text = "0.00";
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void ValueCalcComp_ValueChanged(object sender, RoutedEventArgs e)
        {
            try
            {

                txtBox = (TextBox)((ValueCalcComp)sender).txtDisplay;
                txtBox.IsReadOnly = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            GridManualHandpay.Visibility = Visibility.Hidden;
            GHandpay.Visibility = Visibility.Visible;
        }

        private void btnManualHandpay_Click(object sender, RoutedEventArgs e)
        {            
            GridManualHandpay.Visibility = Visibility.Visible;
            GHandpay.Visibility = Visibility.Hidden;
            LoadmachineList();

        }
       
        private void FillMachines(object sender, RoutedEventArgs e)
        {
            LoadmachineList();
        }

        private void LoadmachineList()
        {           
            IHandpay objCashDeskOperator = HandpayBusinessObject.CreateInstance();

            DataTable dtMachines = null;// objCashDeskOperator.FillMachines();
            Binding bind = new Binding();
            bind.Source = dtMachines;
            lvMachines.SetBinding(ListView.ItemsSourceProperty, bind);
        }


        public void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    string header = headerClicked.Column.Header as string;
                    Sort(header, direction);

                    if (direction == ListSortDirection.Ascending)
                    {
                        headerClicked.Column.HeaderTemplate = Resources["UpArrow"] as DataTemplate;
                    }
                    else
                    {
                        headerClicked.Column.HeaderTemplate = Resources["DownArrow"] as DataTemplate;
                    }

                    if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked)
                    {
                        _lastHeaderClicked.Column.HeaderTemplate = null;
                    }


                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
        }


        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView =
              CollectionViewSource.GetDefaultView(lvUnprocessedHandpays.ItemsSource);

            if (sortBy == "Position")
            {
                sortBy = "Bar_Pos_Name";
            }
            else if (sortBy == "Machine")
            {
                sortBy = "Name";
            }
            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }

        private void optHandpay_Checked(object sender, RoutedEventArgs e)
        {
            if (optHandpay.IsChecked == true)
            {
                if (txtBox != null)
                {
                    txtBox.Text = "0.00";
                    ucValueCalcComp.s_UnformattedText = "";
                }
                lvMachines.SelectedIndex = 0;
            }
        }

        private void optJackpot_Checked(object sender, RoutedEventArgs e)
        {
            if (optJackpot.IsChecked == true)
            {
                if (txtBox != null)
                {
                    txtBox.Text = "0.00";
                    ucValueCalcComp.s_UnformattedText = "";
                }
                lvMachines.SelectedIndex = 0;
            }
        }

        private void optProgressive_Checked(object sender, RoutedEventArgs e)
        {
            if (optProgressive.IsChecked == true)
            {
                if (txtBox != null)
                {
                    txtBox.Text = "0.00";
                    ucValueCalcComp.s_UnformattedText = "";
                }
                lvMachines.SelectedIndex = 0;
            }
        }
    }
}
