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
using BMC.Business.CashDeskOperator;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for CAnalysisDetailsWindow.xaml
    /// </summary>
    public partial class CAnalysisDetailsWindow : Window
    {
        private int _iType;
        private DateTime _dteStartDate;
        private DateTime _DteEndDate;
        private int _iWeekID;
        private string _strCaption;
   
        public CAnalysisDetailsWindow(string strCaption, int iType, DateTime dteStartDate, DateTime DteEndDate, int iWeekID)
        {
            _iType = iType;
            _dteStartDate = dteStartDate;
            _DteEndDate = DteEndDate;
            _iWeekID = iWeekID;
            _strCaption = strCaption;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ucDetails.Initialize((_iType == 2 ? BMC.Presentation.POS.Views.UCAnalysisDetails.AnalysisDetailsParent.ReportDropDetails : BMC.Presentation.POS.Views.UCAnalysisDetails.AnalysisDetailsParent.ReportDetails), _strCaption, _iType, AnalysisView.Position, -1, _dteStartDate, _DteEndDate, _iWeekID);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btn_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
