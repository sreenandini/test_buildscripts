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
using BMC.Presentation.CashDeskManager.HelperClasses;
using System.Windows.Forms;
using BMC.Business.CashDeskManager;

namespace BMC.Presentation.CashDeskManager.UserControls
{
    /// <summary>
    /// Interaction logic for CReport.xaml
    /// </summary>
    public partial class CReport : System.Windows.Controls.UserControl
    {
        #region Declarations
        TreasuryTransactions busTreasury;
        #endregion
        public CReport()
        {
            InitializeComponent();
            OnLoad();
        }


        private void OnLoad()
        {
            wfDatePickerFrom.Width = 93;
            wfDatePickerTo.Width = 93;
            wfTimePickerTo.Width = 93;
            wfTimePickerFrom.Width = 93;
            cmbDatePickerFrom.Items.Clear();
            cmbDatePickerFrom.Items.Add(RegionalSetting.GetRegionalDate(DateTime.Now));
            cmbDatePickerFrom.SelectedIndex = 0;

            cmbDatePickerTo.Items.Clear();
            cmbDatePickerTo.Items.Add(RegionalSetting.GetRegionalDate(this.wfDatePickerFrom.Value));
            cmbDatePickerTo.SelectedIndex = 0;

            this.wfTimePickerTo.Format = DateTimePickerFormat.Time;
            this.wfTimePickerTo.ShowUpDown = true;
            lstTimePickerTo.Items.Clear();
            lstTimePickerTo.Items.Add(RegionalSetting.GetRegionalTime(DateTime.Now));
            lstTimePickerTo.SelectedIndex = 0;

            this.wfTimePickerFrom.Format = DateTimePickerFormat.Time;
            this.wfTimePickerFrom.ShowUpDown = true;
            lstTimePickerFrom.Items.Clear();
            lstTimePickerFrom.Items.Add(RegionalSetting.GetRegionalTime(DateTime.Now));
            lstTimePickerFrom.SelectedIndex = 0;
        }

        private void wfTimePickerFrom_ValueChanged(object sender, EventArgs e)
        {
            lstTimePickerFrom.Items.Clear();
            lstTimePickerFrom.Items.Add(RegionalSetting.GetRegionalTime(this.wfTimePickerFrom.Value));
            lstTimePickerFrom.SelectedIndex = 0;
        }

        private void wfTimePickerTo_ValueChanged(object sender, EventArgs e)
        {

            lstTimePickerTo.Items.Clear();
            lstTimePickerTo.Items.Add(RegionalSetting.GetRegionalTime(this.wfTimePickerTo.Value));
            lstTimePickerTo.SelectedIndex = 0;
        }

        private void wfDatePickerTo_ValueChanged(object sender, EventArgs e)
        {
            cmbDatePickerTo.Items.Clear();
            cmbDatePickerTo.Items.Add(RegionalSetting.GetRegionalDate(this.wfDatePickerTo.Value));
            cmbDatePickerTo.SelectedIndex = 0;
        }

        private void wfDatePickerFrom_ValueChanged(object sender, EventArgs e)
        {
            cmbDatePickerFrom.Items.Clear();
            cmbDatePickerFrom.Items.Add(RegionalSetting.GetRegionalDate(this.wfDatePickerFrom.Value));
            cmbDatePickerFrom.SelectedIndex = 0;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            wfDatePickerTo.Value = DateTime.Now;
            wfDatePickerFrom.Value = DateTime.Now.AddDays(-1);
            busTreasury = new TreasuryTransactions();
            System.Windows.Controls.ListViewItem item = null;
            foreach (KeyValuePair<string, string> KeyValue in busTreasury.FillRouteFilter())
            {
                item = new System.Windows.Controls.ListViewItem();
                item.Content = KeyValue.Value;
                item.Tag = KeyValue.Key;
                cmbRouteFilter.Items.Add(item);
            }
            cmbRouteFilter.SelectedIndex = 0;
        }

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.ListViewItem item = (System.Windows.Controls.ListViewItem)cmbRouteFilter.SelectedItem;
            busTreasury.FillListOfFilteredPositions(item.Tag.ToString());
        }

        private void btnExceptions_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.ListViewItem item = (System.Windows.Controls.ListViewItem)cmbRouteFilter.SelectedItem;

            //CExceptions objExceptions = new CExceptions(item.Tag.ToString(), cmbDatePickerFrom.Text, cmbDatePickerTo.Text,
            //    lstTimePickerFrom.SelectedValue.ToString(),lstTimePickerTo.SelectedValue.ToString());
            string StartTime = "06:00:00";
            string EndTime = "06:00:00";

            //CExceptions objExceptions = new CExceptions("0", dtpStartDate.Text, dtpEndDate.Text,
            //    StartTime, EndTime);
            CExceptions objExceptions = new CExceptions("0", "01 Sep 2008", "02 Sep 2008",
              StartTime, EndTime);
            MyGrid.Children.Add(objExceptions);
            objExceptions.Margin = new Thickness(-90, 50, 0, 0);
        }

        private void btnActiveTickets_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLiability_Click(object sender, RoutedEventArgs e)
        {
         
        }

        private void btnVoidCancelled_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExpired_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
