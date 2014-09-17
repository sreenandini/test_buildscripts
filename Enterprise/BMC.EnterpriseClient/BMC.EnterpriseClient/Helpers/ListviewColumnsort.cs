using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;

namespace BMC.EnterpriseClient.Helpers
{
    #region ListViewColumnSorter
    /// <summary>
    /// This class is an implementation of the 'IComparer' interface.
    /// </summary>
    public class ListViewColumnSorter : IComparer
    {
        /// <summary>
        /// Specifies the column to be sorted
        /// </summary>
        private int ColumnToSort;
        /// <summary>
        /// Specifies the order in which to sort (i.e. 'Ascending').
        /// </summary>
        private SortOrder OrderOfSort;
        /// <summary>
        /// Case insensitive comparer object
        /// </summary>
        private CaseInsensitiveComparer ObjectCompare;

        /// <summary>
        /// Class constructor.  Initializes various elements
        /// </summary>
        public ListViewColumnSorter()
        {
            // Initialize the column to '0'
            ColumnToSort = 0;

            // Initialize the sort order to 'none'
            OrderOfSort = SortOrder.None;

            // Initialize the CaseInsensitiveComparer object
            ObjectCompare = new CaseInsensitiveComparer();
        }

        /// <summary>
        /// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
        /// </summary>
        /// <param name="x">First object to be compared</param>
        /// <param name="y">Second object to be compared</param>
        /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX, listviewY;

            // Cast the objects to be compared to ListViewItem objects
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;

            // Compare the two items
            compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);

            // Calculate correct return value based on object comparison
            if (OrderOfSort == SortOrder.Ascending)
            {
                // Ascending sort is selected, return normal result of compare operation
                return compareResult;
            }
            else if (OrderOfSort == SortOrder.Descending)
            {
                // Descending sort is selected, return negative result of compare operation
                return (-compareResult);
            }
            else
            {
                // Return '0' to indicate they are equal
                return 0;
            }
        }

        /// <summary>
        /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
        /// </summary>
        public int SortColumn
        {
            set
            {
                ColumnToSort = value;
            }
            get
            {
                return ColumnToSort;
            }
        }

        /// <summary>
        /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
        /// </summary>
        public SortOrder Order
        {
            set
            {
                OrderOfSort = value;
            }
            get
            {
                return OrderOfSort;
            }
        }

    } 
    #endregion

    #region ListViewCustomSorter
    public class ListViewCustomSorter : DisposableObject, IComparer
    {
        private ListView _listView = null;
        private SortOrder _sortOrder = SortOrder.None;
        private int _sortColumn = 0;
        private IComparer _comparer = null;
        private NumericWrapperComparer _numericWrapperComparer = null;
        private NumericWithTotalComparer _numericWithTotalComparer = null;
        private Form _ownerForm = null;

        public ListViewCustomSorter(ListView listView, Form ownerForm)
        {
            _ownerForm = ownerForm;
            _listView = listView;            
            _listView.ListViewItemSorter = this;
            _comparer = new CaseInsensitiveComparer();
            _numericWrapperComparer = new NumericWrapperComparer();
            _numericWithTotalComparer = new NumericWithTotalComparer();
            if (_ownerForm != null)
            {
                _ownerForm.FormClosing += new FormClosingEventHandler(OnOwnerForm_FormClosing);
            }
            _listView.ColumnClick += new ColumnClickEventHandler(OnListView_ColumnClick);
        }

        void OnOwnerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _listView.ColumnClick -= (OnListView_ColumnClick);
        }
        public SortOrder SortOrder
        {
            get { return _sortOrder; }
            set { _sortOrder = value; }
        }
        public int SortColumn
        {
            get { return _sortColumn; }
            set { _sortColumn = value; }
        }

        void OnListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnListView_ColumnClick");

            try
            {
                if (e.Column == _sortColumn)
                {
                    _sortOrder = (_sortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending);
                }
                else
                {
                    _sortOrder = SortOrder.Ascending;
                    _sortColumn = e.Column;
                }
                _listView.Sort();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            _listView.ColumnClick -= (OnListView_ColumnClick);
        }

        private int GenericCompare(Type type, object src1, object src2)
        {
            if (src1 == null || src2 == null) return -2;
            if (type == typeof(NumericWrapperComparer))
            {
                return _numericWrapperComparer.Compare(src1.ToString(), src2.ToString());
            }
            else if (type == typeof(NumericWithTotalComparer))
            {
                return _numericWithTotalComparer.Compare(src1.ToString(), src2.ToString());
            }

            object dest1 = Convert.ChangeType(src1, type);
            object dest2 = Convert.ChangeType(src2, type);
            IComparable comparable = dest1 as IComparable;

            if (comparable == null) return -2;
            return comparable.CompareTo(dest2);
        }

        public int Compare(object x, object y)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Compare");
            int result = default(int);

            try
            {
                Type type = _listView.Columns[_sortColumn].Tag as Type;
                ListViewItem lx = (ListViewItem)x;
                ListViewItem ly = (ListViewItem)y;
                string textX = lx.SubItems[_sortColumn].Text;
                string textY = ly.SubItems[_sortColumn].Text;
                bool found = false;

                if (type != null)
                {
                    try
                    {
                        result = this.GenericCompare(type, textX, textY);
                        found = true;
                    }
                    catch { }
                }

                if (!found)
                {
                    result = _comparer.Compare(textX, textY);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            // Calculate correct return value based on object comparison
            if (_sortOrder == SortOrder.Ascending)
            {
                // Ascending sort is selected, return normal result of compare operation
                return result;
            }
            else if (_sortOrder == SortOrder.Descending)
            {
                // Descending sort is selected, return negative result of compare operation
                return (-result);
            }
            else
            {
                // Return '0' to indicate they are equal
                return 0;
            }
        }
    }
    #endregion

}
