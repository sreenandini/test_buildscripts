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
using BMC.DBInterface.CashDeskOperator;
using System.Data;
using BMC.Common.ExceptionManagement;
using BMC.Business.CashDeskOperator;
using BMC.Common.LogManagement;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for UCSiteIntegorration.xaml
    /// </summary>
    public partial class UCSiteIntegorration : UserControl
    {
        //Action<bool> fun_filterby = null;

        public UCSiteIntegorration(int iType, DateTime dteStartDate, DateTime DteEndDate, int iWeekID)
        {
            try
            {
                InitializeComponent();
                ucDetails.Initialize(BMC.Presentation.POS.Views.UCAnalysisDetails.AnalysisDetailsParent.SiteInterrogration, this.FindResource("UCSiteIntegorration_xaml_Header").ToString(), iType, AnalysisView.Position, -1, dteStartDate, DteEndDate, iWeekID);
            }
            catch (Exception ex)
            {
                MessageBox.ShowBox("Unable to Load Site Interrogation details", BMC_Icon.Information, true);
                ExceptionManager.Publish(ex);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        


    }
}
