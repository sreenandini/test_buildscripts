using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMC.EnterpriseClient.Helpers
{
    public class ListViewResizeEventArgs : EventArgs
    {
        private readonly List<float> columnWidthLists;
        public ListViewResizeEventArgs(ref bool resizing, List<float> columnWidthLists)
        {
            this.Resizing = resizing;
            this.columnWidthLists = columnWidthLists;
        }

        public bool Resizing { get; set; }
        public List<float> ColumnWidthLists { get { return columnWidthLists; } }
    }

    public static class ListViewHelper
    {
        public static void ListViewResizeHandler(object sender, ListViewResizeEventArgs e)
        {
            if (!e.Resizing && e.ColumnWidthLists != null)
            {
                e.Resizing = true;
                float totalWidth = e.ColumnWidthLists.Sum();
                ListView listView = sender as ListView;

                if (listView != null)
                {
                    for (int i = 0; i < listView.Columns.Count; i++)
                    {
                        listView.Columns[i].Width = (int)((Convert.ToInt32(e.ColumnWidthLists[i]) / totalWidth) * listView.ClientRectangle.Width);
                    }
                }
            }

            e.Resizing = false;
        }
    }
}

