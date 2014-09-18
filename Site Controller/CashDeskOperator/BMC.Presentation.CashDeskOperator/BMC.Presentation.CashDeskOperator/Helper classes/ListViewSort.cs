using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Collections;
namespace BMC.Presentation.POS.Helper_classes
{
    public interface IListViewCustomComparer : IComparer
    {                   
        String SortBy { get; set; }
        ListSortDirection SortDirection { get; set; }
    }

    public static class ListViewSorter
    {       
        private static Dictionary<String, ListViewSortItem> _listViewDefinitions = new Dictionary<String, ListViewSortItem>();       

        public static readonly DependencyProperty CustomListViewSorterProperty = DependencyProperty.RegisterAttached(
            "CustomListViewSorter",
            typeof(String),
            typeof(ListViewSorter),
            new FrameworkPropertyMetadata("", new PropertyChangedCallback(OnRegisterSortableGrid)));  
       
        /// Gets the custom list view sorter.        
        public static String GetCustomListViewSorter(DependencyObject obj)
        {
            return (String)obj.GetValue(CustomListViewSorterProperty);
        }
       
        /// Sets the custom list view sorter.       
        public static void SetCustomListViewSorter(DependencyObject obj, String value)
        {
            obj.SetValue(CustomListViewSorterProperty, value);
        }       
           
        /// Grids the view column header clicked handler.       
        public static void GridViewColumnHeaderClickedHandler(Object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.ListView view = sender as System.Windows.Controls.ListView;
            if (view == null) return;

            if (_listViewDefinitions == null) return;

            ListViewSortItem listViewSortItem = (_listViewDefinitions.ContainsKey(view.Name)) ? _listViewDefinitions[view.Name] : null;
            if (listViewSortItem == null) return;

            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
            if (headerClicked == null) return;

            ListCollectionView collectionView = CollectionViewSource.GetDefaultView(view.ItemsSource) as ListCollectionView;
            if (collectionView == null) return;

            ListSortDirection sortDirection = GetSortingDirection(headerClicked, listViewSortItem);

            // get header name
            String header = headerClicked.Column.Header as string;
            if (String.IsNullOrEmpty(header)) return;

            // sort listview
            if (listViewSortItem.Comparer != null)
            {
                listViewSortItem.Comparer.SortBy = header;
                listViewSortItem.Comparer.SortDirection = sortDirection;
                collectionView.CustomSort = listViewSortItem.Comparer;
                view.Items.Refresh();
            }
            else
            {
                view.Items.SortDescriptions.Clear();
                view.Items.SortDescriptions.Add(new SortDescription(headerClicked.Column.Header.ToString(), sortDirection));
                view.Items.Refresh();
            }

            // change datatemplate of previous and current column header
            headerClicked.Column.HeaderTemplate = GetHeaderColumnsDataTemplate(view, listViewSortItem, sortDirection);

            // Set current sort values as last sort values
            listViewSortItem.LastColumnHeaderClicked = headerClicked;
            listViewSortItem.LastSortDirection = sortDirection;
        }
                 
        /// Called when [register sortable grid].
        private static void OnRegisterSortableGrid(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
          
            // Check if we are in design mode, if so don't do anything.
            if ((Boolean)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue)) return;

            ListView view = obj as ListView;
            if (view != null)
            {
                foreach (string s in _listViewDefinitions.Keys)
                {
                    if (s.ToString() == view.Name) { 
                    _listViewDefinitions.Remove(view.Name);
                        break; }
                }
                if (!string.IsNullOrEmpty(view.Name))
                {
                    _listViewDefinitions.Add(view.Name, new ListViewSortItem(System.Activator.CreateInstance(Type.GetType(GetCustomListViewSorter(obj))) as IListViewCustomComparer, null, ListSortDirection.Ascending));
                    view.AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(GridViewColumnHeaderClickedHandler));
                }                
            }
        }
       
        /// Gets the header columns data template.       
        private static DataTemplate GetHeaderColumnsDataTemplate(System.Windows.Controls.ListView view, ListViewSortItem listViewSortItem, ListSortDirection sortDirection)
        {
            // remove mark from previous sort column
            if (listViewSortItem.LastColumnHeaderClicked != null)
                listViewSortItem.LastColumnHeaderClicked.Column.HeaderTemplate = view.TryFindResource("ListViewHeaderTemplateNoSorting") as DataTemplate;

            // set correct mark to current column
            switch (sortDirection)
            {
                case ListSortDirection.Ascending:
                    return view.TryFindResource("ListViewHeaderTemplateAscendingSorting") as DataTemplate;
                case ListSortDirection.Descending:
                    return view.TryFindResource("ListViewHeaderTemplateDescendingSorting") as DataTemplate;
                default:
                    return null;
            }
        }
               
        /// Gets the sorting direction of the header clicked.      
        private static ListSortDirection GetSortingDirection(GridViewColumnHeader headerClicked, ListViewSortItem listViewSortItem)
        {
            if (headerClicked != listViewSortItem.LastColumnHeaderClicked) return ListSortDirection.Ascending;
            else
                return (listViewSortItem.LastSortDirection == ListSortDirection.Ascending) ? ListSortDirection.Descending : ListSortDirection.Ascending;
        }
        
    }

    public class ListViewSortItem
    {    
        public ListViewSortItem(IListViewCustomComparer comparer, GridViewColumnHeader lastColumnHeaderClicked, ListSortDirection lastSortDirection)
        {
            Comparer = comparer;
            LastColumnHeaderClicked = lastColumnHeaderClicked;
            LastSortDirection = lastSortDirection;
        }   
       
        public IListViewCustomComparer Comparer { get; private set; }
       
        public GridViewColumnHeader LastColumnHeaderClicked { get; set; }
       
        public ListSortDirection LastSortDirection { get; set; }
       
    }

    public abstract class ListViewCustomComparer<T> : IComparer, IListViewCustomComparer where T : class
    {
        private String sortBy = String.Empty;
        private ListSortDirection direction = ListSortDirection.Ascending;
       
        ///  sort by data column name.       
        public String SortBy
        {
            get { return sortBy; }
            set { sortBy = value; }
        }
       
        /// the sort direction.       
        public ListSortDirection SortDirection
        {
            get { return direction; }
            set { direction = value; }
        }
      
        /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.     
        
        /// <exception cref="T:System.ArgumentException">Neither <paramref name="x"/> nor <paramref name="y"/> implements the <see cref="T:System.IComparable"/> interface.-or- <paramref name="x"/> and <paramref name="y"/> are of different types and neither one can handle comparisons with the other. </exception>
        public Int32 Compare(Object x, Object y)
        {

            T item1 = x as T;
            T item2 = y as T;

            if (item1 == null || item2 == null)                          
                return 0;           

            return Compare(item1, item2);
        }
      
        /// Compares the specified x to y.       
        public abstract Int32 Compare(T x, T y);

        
    }
}
