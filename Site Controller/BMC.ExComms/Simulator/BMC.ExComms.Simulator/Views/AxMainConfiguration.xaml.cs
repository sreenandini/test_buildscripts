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
using BMC.CoreLib;
using BMC.ExComms.Simulator.DataLayer;
using BMC.ExComms.Simulator.Models;
using System.Data;

namespace BMC.ExComms.Simulator.Views
{
    /// <summary>
    /// Interaction logic for AxMainConfiguration.xaml
    /// </summary>
    public partial class AxMainConfiguration : UserControl
    {
        private readonly string DYN_MODULE_NAME = "AxMainConfiguration";

        public AxMainConfiguration()
        {
            InitializeComponent();            
        }
    }
}
