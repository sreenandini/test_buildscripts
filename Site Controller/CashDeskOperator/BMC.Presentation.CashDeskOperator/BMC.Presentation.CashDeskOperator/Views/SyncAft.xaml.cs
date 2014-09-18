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
using BMC.CashDeskOperator;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for SyncAft.xaml
    /// </summary>
    public partial class SyncAft : UserControl
    {
        public SyncAft()
        {
            InitializeComponent();
            this.DataContext = new AftAssetDetails();
        }

    }
}
