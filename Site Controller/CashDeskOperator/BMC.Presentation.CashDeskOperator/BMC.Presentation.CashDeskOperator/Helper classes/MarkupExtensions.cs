using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;
using BMC.Common.Utilities;
using System.Windows.Markup;
using System.Windows;

namespace BMC.Presentation.POS.Helper_classes
{
    [MarkupExtensionReturnType(typeof(string))]
    public sealed class CurrencySymbolProvider : MarkupExtension
    {
        public CurrencySymbolProvider()
        {
        }

        public CurrencySymbolProvider(string contentText)
        {
            ContentText = contentText;
        }

        public string ContentText { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new RegionInfo(ExtensionMethods.CurrentSiteCulture).CurrencySymbol + " " + ContentText;
        }
    }
}


namespace BMC.Presentation.POS.Views.FocusHelper
{
    public static class FocusExtension
    {
        public static bool GetIsFocused(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsFocusedProperty);
        }


        public static void SetIsFocused(DependencyObject obj, bool value)
        {
            obj.SetValue(IsFocusedProperty, value);
        }


        public static readonly DependencyProperty IsFocusedProperty =
            DependencyProperty.RegisterAttached(
             "IsFocused", typeof(bool), typeof(FocusExtension),
             new UIPropertyMetadata(false, OnIsFocusedPropertyChanged));


        private static void OnIsFocusedPropertyChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var uie = (UIElement)d;
            if ((bool)e.NewValue)
            {
                uie.Focus(); // Don't care about false values.
            }
        }
    }
}
