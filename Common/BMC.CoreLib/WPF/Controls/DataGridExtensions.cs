using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib.WPF.Controls
{
    public static class DataGridExtensions
    {
        public static bool GetSingleClickEdit(DependencyObject obj)
        {
            return (bool)obj.GetValue(SingleClickEditProperty);
        }

        public static void SetSingleClickEdit(DependencyObject obj, bool value)
        {
            obj.SetValue(SingleClickEditProperty, value);
        }

        // Using a DependencyProperty as the backing store for SingleClickEdit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SingleClickEditProperty =
            DependencyProperty.RegisterAttached("SingleClickEdit", typeof(bool), typeof(DataGridExtensions),
            new UIPropertyMetadata(false, new PropertyChangedCallback(OnSingleClickEditCallback)));

        private static void OnSingleClickEditCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ModuleProc PROC = new ModuleProc("WpfGridViewSort", "OnSingleClickEditCallback");

            try
            {
                DataGridColumn owner = d as DataGridColumn;
                if (owner != null)
                {
                    bool oldValue = (bool)e.OldValue;
                    bool newValue = (bool)e.NewValue;

                    if (oldValue && !newValue)
                        ModifySingleClickEdit(owner, false);
                    if (!oldValue && newValue)
                        ModifySingleClickEdit(owner, true);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private static void ModifySingleClickEdit(DataGridColumn owner, bool subcribe)
        {
            ModuleProc PROC = new ModuleProc("", "ModifySingleClickEdit");

            try
            {
                EventSetter setter = new EventSetter();
                setter.Event = DataGridCell.PreviewMouseLeftButtonDownEvent;
                setter.Handler = new MouseButtonEventHandler(OnGridSingleClickEdit);
                Style cellStyle = new Style();
                cellStyle.Setters.Add(setter);
                owner.CellStyle = cellStyle;
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private static void OnGridSingleClickEdit(object sender, MouseButtonEventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "OnGridViewColumnHeaderClick");

            try
            {
                DataGridCell cell = sender as DataGridCell;
                if (cell != null && !cell.IsEditing && !cell.IsReadOnly)
                {
                    if (!cell.IsFocused)
                    {
                        cell.Focus();
                    }
                    DataGrid dataGrid = FindVisualParent<DataGrid>(cell);
                    if (dataGrid != null)
                    {
                        if (dataGrid.SelectionUnit != DataGridSelectionUnit.FullRow)
                        {
                            if (!cell.IsSelected)
                                cell.IsSelected = true;
                        }
                        else
                        {
                            DataGridRow row = FindVisualParent<DataGridRow>(cell);
                            if (row != null && !row.IsSelected)
                            {
                                row.IsSelected = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        static T FindVisualParent<T>(UIElement element) where T : UIElement
        {
            UIElement parent = element;
            while (parent != null)
            {
                T correctlyTyped = parent as T;
                if (correctlyTyped != null)
                {
                    return correctlyTyped;
                }

                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }
            return null;
        }
    }
}
