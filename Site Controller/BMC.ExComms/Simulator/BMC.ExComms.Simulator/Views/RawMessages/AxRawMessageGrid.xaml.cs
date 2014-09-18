using System;
using System.Collections;
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

namespace BMC.ExComms.Simulator.Views.RawMessages
{
    /// <summary>
    /// Interaction logic for AxRawMessageGrid.xaml
    /// </summary>
    public partial class AxRawMessageGrid : UserControl
    {
        public AxRawMessageGrid()
        {
            InitializeComponent();
        }

        public object ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(AxRawMessageGrid), new UIPropertyMetadata(null, new PropertyChangedCallback(ItemsSourceCallback)));

        private static void ItemsSourceCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            using (ILogMethod method = Log.LogMethod("AxRawMessageGrid", "ItemsSourceCallback"))
            {
                try
                {
                    AxRawMessageGrid grid = d as AxRawMessageGrid;
                    grid.dgvRawMessages.ItemsSource = e.NewValue as IEnumerable;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }
        
    }
}
