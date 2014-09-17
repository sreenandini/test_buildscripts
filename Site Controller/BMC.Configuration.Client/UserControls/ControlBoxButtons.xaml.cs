#region File Description
//-------------------------------------------------------------------------------------
// File: ControlBoxButtons.Xaml.cs
//
// Namespace: BMC.CashDeskOperator
//
// Description: Keyboard user control interaction logic
//
// Copyright: Bally Technologies Inc. 2008
//
// Revision History:
//------------------------------------------------------------------------------------
// Date				    Engineer			        Description
// -----------------------------------------------------------------------------------
// 22/10/2008          Balasubramanyam.G        Control box buttons logic
//------------------------------------------------------------------------------------
#endregion

#region Namespaces
using System;
using System.Collections.Generic;
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
#endregion

namespace BMC.ExchangeConfig
{
    /// <summary>
    /// Interaction logic for ControlBoxButtons.xaml
    /// </summary>
    public partial class ControlBoxButtons
    {
        // Using a DependencyProperty as the backing store for ImagePath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImagePathProperty =
            DependencyProperty.Register("ImagePath", typeof(ImageSource), typeof(ControlBoxButtons), new UIPropertyMetadata(null));

        // Using a DependencyProperty as the backing store for ButtonText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonTextProperty =
            DependencyProperty.Register("ButtonText", typeof(string), typeof(ControlBoxButtons), new UIPropertyMetadata(string.Empty));


        public ImageSource ImagePath
        {
            get { return (ImageSource)GetValue(ImagePathProperty); }
            set { SetValue(ImagePathProperty, value); }
        }


        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

        
        public ControlBoxButtons()
        {
            this.InitializeComponent();

        }

    }
}