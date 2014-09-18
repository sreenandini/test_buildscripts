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
using BMC.ExComms.Simulator.Models;

namespace BMC.ExComms.Simulator.Views
{
    /// <summary>
    /// Interaction logic for AxMainConsole.xaml
    /// </summary>
    public partial class AxMainConsole : UserControl
    {
        public AxMainConsole()
        {
            InitializeComponent();
            this.Exceptions = new ExceptionModelCollection();
        }

        public ExceptionModelCollection Exceptions
        {
            get { return (ExceptionModelCollection)GetValue(ExceptionsProperty); }
            set { SetValue(ExceptionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Exceptions.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExceptionsProperty =
            DependencyProperty.Register("Exceptions", typeof(ExceptionModelCollection), typeof(AxMainConsole), new PropertyMetadata(null));

        public string Output
        {
            get { return (string)GetValue(OutputProperty); }
            set { SetValue(OutputProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Output.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OutputProperty =
            DependencyProperty.Register("Output", typeof(string), typeof(AxMainConsole), new PropertyMetadata(string.Empty));

        public void ScrollToEnd()
        {
            txtOutput.ScrollToEnd();
        }
    }
}
