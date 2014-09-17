// -----------------------------------------------------------------------
// <copyright file="FrameworkElementBehaviors.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BMC.CoreLib.WPF.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Input;
    using System.Windows;
    using BMC.CoreLib.Diagnostics;
    using System.Windows.Controls.Primitives;
    using System.Windows.Controls;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class FrameworkElementBehaviors
    {
        static FrameworkElementBehaviors() { }

        public static ICommand GetLoadedCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(LoadedCommandProperty);
        }

        public static void SetLoadedCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(LoadedCommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for LoadedCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoadedCommandProperty =
            DependencyProperty.RegisterAttached("LoadedCommand", typeof(ICommand),
            typeof(FrameworkElementBehaviors), new PropertyMetadata(null, OnLoadedCommandChanged));

        private static void OnLoadedCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ModuleProc PROC = new ModuleProc("FrameworkElementBehaviors", "OnLoadedCommandChanged");

            try
            {
                FrameworkElement fe = d as FrameworkElement;
                if (fe != null)
                {
                    fe.Loaded += (s, a) =>
                    {
                        ICommand cmd = e.NewValue as ICommand;
                        if (cmd != null &&
                            cmd.CanExecute(fe))
                        {
                            cmd.Execute(fe);
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }
    }

    public static class MenuExtensions
    {
        public static PlacementMode GetMenuPlacementMode(DependencyObject obj)
        {
            return (PlacementMode)obj.GetValue(MenuPlacementModeProperty);
        }

        public static void SetMenuPlacementMode(DependencyObject obj, PlacementMode value)
        {
            obj.SetValue(MenuPlacementModeProperty, value);
        }

        // Using a DependencyProperty as the backing store for MenuPlacementMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuPlacementModeProperty =
            DependencyProperty.RegisterAttached("MenuPlacementMode", typeof(PlacementMode), typeof(MenuExtensions),
            new FrameworkPropertyMetadata(PlacementMode.Bottom, FrameworkPropertyMetadataOptions.None,
                new PropertyChangedCallback(MenuPlacementModeChanged)));

        public static void MenuPlacementModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ModuleProc PROC = new ModuleProc("MenuExtensions", "MenuPlacementModeChanged");

            try
            {
                MenuItem mi = d as MenuItem;
                if (mi != null)
                {
                    if (mi.IsLoaded)
                    {
                        SetMenuPlacement(mi, (PlacementMode)e.NewValue);
                    }
                    else
                    {
                        mi.Loaded += (o, f) =>
                        {
                            SetMenuPlacement(mi, (PlacementMode)e.NewValue);
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private static void SetMenuPlacement(MenuItem menuItem, PlacementMode mode)
        {
            ModuleProc PROC = new ModuleProc("MenuExtensions", "SetMenuPlacement");

            try
            {
                Popup menuItemPopup = menuItem.Template.FindName("PART_Popup", menuItem) as Popup;
                if (menuItemPopup != null)
                {
                    menuItemPopup.Placement = mode;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }
    }
}
