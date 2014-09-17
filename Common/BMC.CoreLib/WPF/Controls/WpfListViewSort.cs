using BMC.CoreLib.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace BMC.CoreLib.WPF.Controls
{
    public static class WpfListViewSort
    {
        static WpfListViewSort() { }

        public static DataTemplate HeaderAscendingTemplate { get; set; }

        public static DataTemplate HeaderDescendingTemplte { get; set; }

        #region Command
        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(WpfListViewSort),
            new UIPropertyMetadata(null));
        #endregion

        #region Sort
        public static bool GetSort(DependencyObject obj)
        {
            return (bool)obj.GetValue(SortProperty);
        }

        public static void SetSort(DependencyObject obj, bool value)
        {
            obj.SetValue(SortProperty, value);
        }

        // Using a DependencyProperty as the backing store for Sort.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SortProperty =
            DependencyProperty.RegisterAttached("Sort", typeof(bool), typeof(WpfListViewSort), new UIPropertyMetadata(false, new PropertyChangedCallback(OnSortChangedCallback)));

        private static void OnSortChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ModuleProc PROC = new ModuleProc("WpfGridViewSort", "OnCommandChangedCallback");

            try
            {
                ListView listView = d as ListView;
                if (listView != null)
                {
                    bool oldValue = (bool)e.OldValue;
                    bool newValue = (bool)e.NewValue;

                    if (oldValue && !newValue)
                        ModifyColumnHeaderClick(listView, false);
                    if (!oldValue && newValue)
                        ModifyColumnHeaderClick(listView, true);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }
        #endregion

        #region DefaultColumnIndex
        public static int GetDefaultColumnIndex(DependencyObject obj)
        {
            return (int)obj.GetValue(DefaultColumnIndexProperty);
        }

        public static void SetDefaultColumnIndex(DependencyObject obj, int value)
        {
            obj.SetValue(DefaultColumnIndexProperty, value);
        }

        // Using a DependencyProperty as the backing store for DefaultColumnIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DefaultColumnIndexProperty =
            DependencyProperty.RegisterAttached("DefaultColumnIndex", typeof(int), typeof(WpfListViewSort),
            new UIPropertyMetadata(0, new PropertyChangedCallback(OnDefaultColumnIndexChangedCallback)));

        private static void OnDefaultColumnIndexChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ModuleProc PROC = new ModuleProc("WpfGridViewSort", "OnCommandChangedCallback");

            try
            {
                ListView listView = d as ListView;
                if (listView != null)
                {
                    int newValue = (int)e.NewValue;
                    GridView gridView = listView.View as GridView;

                    if (gridView == null)
                    {
                        listView.Initialized -= listView_Initialized;
                        listView.Initialized += listView_Initialized;
                    }
                    else
                    {
                        DoDefaultSort(listView, newValue);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private static void DoDefaultSort(ListView listView, int defaultIndex)
        {
            ModuleProc PROC = new ModuleProc("", "DoDefaultSort");

            try
            {
                if (listView == null) return;
                GridView gridView = listView.View as GridView;

                int newValue = defaultIndex;
                if (newValue == -1) newValue = GetDefaultColumnIndex(listView);

                if (gridView != null &&
                    (newValue >= 0) &&
                    (newValue >= 0 && newValue < gridView.Columns.Count))
                {
                    GridViewColumn col = gridView.Columns[newValue];
                    DoSort(listView, col, GetPropertyNameOrHeaderName(col));
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        static void listView_Initialized(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "listView_Initialized");

            try
            {
                DoDefaultSort(sender as ListView, -1);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }
        #endregion


        #region PropertyName
        public static string GetPropertyName(DependencyObject obj)
        {
            return (string)obj.GetValue(PropertyNameProperty);
        }

        public static void SetPropertyName(DependencyObject obj, string value)
        {
            obj.SetValue(PropertyNameProperty, value);
        }

        // Using a DependencyProperty as the backing store for PropertyName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PropertyNameProperty =
            DependencyProperty.RegisterAttached("PropertyName", typeof(string), typeof(WpfListViewSort), new UIPropertyMetadata(string.Empty));
        #endregion

        #region CurrentColumn
        public static GridViewColumn GetCurrentColumn(DependencyObject obj)
        {
            return (GridViewColumn)obj.GetValue(CurrentColumnProperty);
        }

        public static void SetCurrentColumn(DependencyObject obj, GridViewColumn value)
        {
            obj.SetValue(CurrentColumnProperty, value);
        }

        // Using a DependencyProperty as the backing store for CurrentColumn.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentColumnProperty =
            DependencyProperty.RegisterAttached("CurrentColumn", typeof(GridViewColumn), typeof(WpfListViewSort), new UIPropertyMetadata(null));
        #endregion

        #region Sort Methods
        private static void ModifyColumnHeaderClick(ListView listView, bool subcribe)
        {
            ModuleProc PROC = new ModuleProc("", "ModifyColumnHeaderClick");

            try
            {
                if (subcribe)
                {
                    listView.AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(OnGridViewColumnHeaderClick));
                }
                else
                {
                    listView.RemoveHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(OnGridViewColumnHeaderClick));
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private static void OnGridViewColumnHeaderClick(object sender, RoutedEventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "OnGridViewColumnHeaderClick");

            try
            {
                GridViewColumnHeader colhdr = e.OriginalSource as GridViewColumnHeader;
                if (colhdr != null)
                {
                    ListView listView = GetParent<ListView>(colhdr);
                    if (listView != null)
                    {
                        InvokeGridViewColumnClicked(listView, colhdr.Column);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private static void InvokeGridViewColumnClicked(ListView listView, GridViewColumn column)
        {
            ModuleProc PROC = new ModuleProc("", "InvokeGridViewColumnClicked");

            try
            {
                // get the attached property name from gridview column
                string propertyName = GetPropertyNameOrHeaderName(column);
                if (!propertyName.IsEmpty())
                {
                    if (listView != null)
                    {
                        // do the sorting
                        DoSort(listView, column, propertyName);

                        // execute the command if any
                        ICommand command = GetCommand(listView);
                        if (command != null &&
                            command.CanExecute(propertyName))
                        {
                            command.Execute(propertyName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private static string GetPropertyNameOrHeaderName(GridViewColumn col)
        {
            ModuleProc PROC = new ModuleProc("", "GetPropertyNameOrHeaderName");
            string result = default(string);

            try
            {
                result = GetPropertyName(col);
                //if (result.IsEmpty())
                //{
                //    if (col.CellTemplate != null)
                //    {
                //        DataTemplate dt = col.CellTemplate;                        
                //    }
                //}
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public static T GetParent<T>(DependencyObject reference)
            where T : DependencyObject
        {
            ModuleProc PROC = new ModuleProc("", "GetParent");

            try
            {
                DependencyObject parent = VisualTreeHelper.GetParent(reference);
                do
                {
                    if (parent is T) return (T)parent;
                    parent = VisualTreeHelper.GetParent(parent);
                } while (parent != null);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return null;
        }

        private static void DoSort(ListView listView, GridViewColumn column, string propertyName)
        {
            ModuleProc PROC = new ModuleProc("", "DoSort");

            try
            {
                // get the collection view
                ICollectionView cv = null;
                if (listView.DataContext != null)
                {
                    cv = CollectionViewSource.GetDefaultView(listView.DataContext);
                }

                if (cv == null && listView.ItemsSource != null)
                {
                    cv = CollectionViewSource.GetDefaultView(listView.DataContext);
                }

                if (cv == null && listView.Items != null)
                {
                    cv = listView.Items;
                }

                // list direction
                ListSortDirection newDirection = ListSortDirection.Ascending;
                if (cv != null)
                {
                    if (cv.SortDescriptions.Count > 0)
                    {
                        SortDescription sortDescription = cv.SortDescriptions[0];
                        if (sortDescription.PropertyName == propertyName)
                        {
                            if (sortDescription.Direction == ListSortDirection.Ascending)
                            {
                                newDirection = ListSortDirection.Descending;
                            }
                            else
                            {
                                newDirection = ListSortDirection.Ascending;
                            }
                        }
                        cv.SortDescriptions.Clear();
                    }

                    if (!propertyName.IsEmpty())
                    {
                        // clear the header template from existing column
                        GridViewColumn currentColumn = GetCurrentColumn(listView);
                        if (currentColumn != null)
                        {
                            currentColumn.HeaderTemplate = null;
                        }

                        // add the sort description
                        cv.SortDescriptions.Add(new SortDescription(propertyName, newDirection));

                        // set the header template
                        DataTemplate headerTemplate = (newDirection == ListSortDirection.Ascending ? HeaderAscendingTemplate : HeaderDescendingTemplte);
                        if (headerTemplate != null)
                        {
                            column.HeaderTemplate = headerTemplate;
                            SetCurrentColumn(listView, column);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }
        #endregion
    }
}
