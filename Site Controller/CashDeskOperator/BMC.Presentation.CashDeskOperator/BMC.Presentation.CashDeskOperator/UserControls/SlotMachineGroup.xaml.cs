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

namespace BMC.Presentation.POS.UserControls
{
    /// <summary>
    /// Interaction logic for SlotMachineGroup.xaml
    /// </summary>
    public partial class SlotMachineGroup : UserControl
    {

        public string DisplayText
        {
            get { return (string)GetValue(DisplayTextProperty); }
            set { SetValue(DisplayTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayTextProperty =
            DependencyProperty.Register("DisplayText", typeof(string), typeof(SlotMachineGroup), new PropertyMetadata(string.Empty));

        public Brush OuterColor
        {
            get { return (Brush)GetValue(OuterColorProperty); }
            set { SetValue(OuterColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OuterColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OuterColorProperty =
            DependencyProperty.Register("OuterColor", typeof(Brush), typeof(SlotMachineGroup), new PropertyMetadata(null));

        public Brush OuterForeColor
        {
            get { return (Brush)GetValue(OuterForeColorProperty); }
            set { SetValue(OuterForeColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OuterForeColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OuterForeColorProperty =
            DependencyProperty.Register("OuterForeColor", typeof(Brush), typeof(SlotMachineGroup), new PropertyMetadata(null));


        public SlotMachineGroup()
        {
            InitializeComponent();
        }
    }
}
