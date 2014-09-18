#region File Description
//-------------------------------------------------------------------------------------
// File: LeftMenuButton.Xaml.cs
//
// Namespace: BMC.Presentation
//
// Description: Keyboard user control interaction logic
//
// Copyright: Bally Technologies Inc. 2008
//
// Revision History:
//------------------------------------------------------------------------------------
// Date				    Engineer			        Description
// -----------------------------------------------------------------------------------
// 22/10/2008          Balasubramanyam.G        LeftMenu buttons logic
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
using System.Windows.Media.Animation;

#endregion

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for TopMenuPanel.xaml
    /// </summary>
    public partial class LeftMenuButton
    {

        public LeftMenuButton()
        {
            this.InitializeComponent();
        }


        public ImageSource ImagePath
        {
            get { return (ImageSource)GetValue(ImagePathProperty); }
            set { SetValue(ImagePathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImagePath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImagePathProperty =
            DependencyProperty.Register("ImagePath", typeof(ImageSource), typeof(LeftMenuButton), new UIPropertyMetadata(null));


        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonTextProperty =
            DependencyProperty.Register("ButtonText", typeof(string), typeof(LeftMenuButton), new UIPropertyMetadata(string.Empty));


        public void Unselect()
        {
            (this.FindResource("Deselect") as Storyboard).Begin(this);
        }

        public void Select()
        {
            (this.FindResource("MouseOver") as Storyboard).Begin(this);
        }







    }
}