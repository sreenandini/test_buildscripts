using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using BMC.Transport;
using System.Reflection;
using System.Windows.Controls;
using Microsoft.Office.Interop.Excel;
using BMC.Common.ExceptionManagement;
using System.Windows;
using System.Windows.Media;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;


namespace BMC.Business.CashDeskManager
{
    public static class Common
    {
        public static bool isValidDateRange(string dStartdate, string dEndDate)
        {
            bool isValidDateRange = false;
            if (DateAndTime.DateDiff(DateInterval.Day, Convert.ToDateTime(dStartdate), Convert.ToDateTime(dEndDate)
                , FirstDayOfWeek.Sunday, FirstWeekOfYear.System) < 0)
            {
                isValidDateRange = false;
            }
            else
            {

                isValidDateRange = true;
            }

            return isValidDateRange;

        }


        public static bool ExportToExcel(ListView lvView,string path)
        {
            bool NotFound = false;
            try
            {
                Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();

                Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);

                Worksheet ws = (Worksheet)xla.ActiveSheet;

                int i = 1;

                int j = 1;
                int colcount = 0;

                GridView view = (System.Windows.Controls.GridView)(lvView.View);

                foreach (GridViewColumn col in (IEnumerable<GridViewColumn>)view.Columns)
                {
                    ws.Cells[i, j] = col.Header;
                    j++;
                    //foreach (PropertyInfo info in lvView.Items[0].GetType().GetProperties())
                    //{
                    //    //if (col.Header.ToString().Replace(" ", "").ToUpper() == info.Name.Replace(" ", "").ToUpper())
                    //    //{
                    //    //    ws.Cells[i, j] = info.Name;

                    //    //    j++;
                    //    //    break;
                    //    //}
                    //    //else
                    //    //{
                    //    //    NotFound = true;
                    //    //}
                    //}
                    //if (NotFound)
                    //{
                    //    ws.Cells[i, j] = info.Name;
                    //}
                }
                i++;
                //j = 1;

                colcount = j - 1;
                j = 1;


                GridView gridView = lvView.View as GridView;

                int index = 0;
                foreach (TicketExceptions item in lvView.Items)
                 {
                    // StackPanel vsp = (StackPanel)typeof(ItemsControl).InvokeMember
                    //    ("_itemsHost", BindingFlags.Instance | BindingFlags.GetField | BindingFlags.NonPublic, null, lvView, null);

                    //double scrollHeight = vsp.ScrollOwner.ScrollableHeight;

                    ////TicketExceptions item = lvView.Items[row] as TicketExceptions;
                    //double offset = (scrollHeight * lvView.Items.IndexOf(item)) / lvView.Items.Count; // itemIndex_ is index of the item which we want to show in the middle of the view

                    //vsp.SetVerticalOffset(offset);

                    //  lvView.ScrollIntoView(item);


                    ListViewItem item1 = lvView.ItemContainerGenerator.ContainerFromIndex(lvView.Items.IndexOf(item)) as ListViewItem;

                    GridViewRowPresenter rowPresenter = GetFrameworkElementByName<GridViewRowPresenter>(item1);
                    foreach (PropertyInfo info in item.GetType().GetProperties())
                    {
                        for (int col = 0; col < colcount; col++)
                        {
                            if (rowPresenter != null)
                            {
                                ContentPresenter templatedParent = VisualTreeHelper.GetChild(rowPresenter, col) as ContentPresenter;
                                TextBlock block = (TextBlock)gridView.Columns[col].CellTemplate.FindName(info.Name, templatedParent);
                                if (block != null)
                                {
                                    for (int range = 0; range < colcount; range++)
                                    {
                                        if (gridView.Columns[range].Header.ToString().ToUpper() == block.Tag.ToString().ToUpper())
                                        {
                                            j = range + 1;
                                            break;
                                        }
                                    }
                                    ws.Cells[i, j] = block.Text;
                                }
                            }
                        }
                    }

                    j = 1;
                    i++;
                    index++;
                }
              
                ws.SaveAs(path, Microsoft.Office.Interop.Excel.XlFileFormat.xlXMLSpreadsheet,
                    null, null, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, null, null, null);

                ws.Delete();
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }

        }

        static bool IsMouseOver(Visual target)
        {
            // We need to use MouseUtilities to figure out the cursor
            // coordinates because, during a drag-drop operation, the WPF
            // mechanisms for getting the coordinates behave strangely.

            Rect bounds = VisualTreeHelper.GetDescendantBounds(target);
            System.Windows.Point mousePos = MouseUtilities.GetMousePosition(target);
            return bounds.Contains(mousePos);
        }


        private static T GetFrameworkElementByName<T>(FrameworkElement referenceElement) where T : FrameworkElement
        {
            FrameworkElement child = null;
            if (referenceElement != null)
            {
                for (Int32 i = 0; i < VisualTreeHelper.GetChildrenCount(referenceElement); i++)
                {
                    child = VisualTreeHelper.GetChild(referenceElement, i) as FrameworkElement;
                    //MessageBox.Show(child.DataContext.ToString());
                    if (child != null && child.GetType() == typeof(T))
                    {
                        break;
                    }
                    else if (child != null)
                    {
                        child = GetFrameworkElementByName<T>(child);
                        if (child != null && child.GetType() == typeof(T))
                        {
                            break;
                        }
                    }
                }
            }
            return child as T;
        }

        public static void getContainers(ListView lvView)
        {

            // gets all nodes from the TreeView  
            List<ListViewItem> allTreeContainers = GetAllItemContainers(lvView);

        }

        static List<ListViewItem> GetAllItemContainers(ListView itemsControl)
        {

            List<ListViewItem> allItems = new List<ListViewItem>();

            for (int i = 0; i < itemsControl.Items.Count; i++)
            {
                // try to get the item Container  
                ListViewItem childItemContainer = itemsControl.ItemContainerGenerator.ContainerFromIndex(i) as ListViewItem;
                allItems.Add(childItemContainer);

                // the item container maybe null if it is still not generated from the runtime  
               
            }
            return allItems;
        }  
 

        public static void CloseExcel()
        {
            List<Process> excelprocesses = Process.GetProcesses().ToList();

            if (excelprocesses.Count > 0)
            {
                foreach (Process proc in excelprocesses)
                {
                    if (proc.ProcessName.ToLower().Equals("excel"))
                    {
                        proc.Kill();
                    }
                }
            }

        }
    }

    public class MouseUtilities
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(ref Win32Point pt);

        [DllImport("user32.dll")]
        private static extern bool ScreenToClient(IntPtr hwnd, ref Win32Point pt);

        /// <summary>
        /// Returns the mouse cursor location.  This method is necessary during 
        /// a drag-drop operation because the WPF mechanisms for retrieving the
        /// cursor coordinates are unreliable.
        /// </summary>
        /// <param name="relativeTo">The Visual to which the mouse coordinates will be relative.</param>
        public static System.Windows.Point GetMousePosition(Visual relativeTo)
        {
            Win32Point mouse = new Win32Point();
            GetCursorPos(ref mouse);

            // Using PointFromScreen instead of Dan Crevier's code (commented out below)
            // is a bug fix created by William J. Roberts.  Read his comments about the fix
            // here: http://www.codeproject.com/useritems/ListViewDragDropManager.asp?msg=1911611#xx1911611xx
            return relativeTo.PointFromScreen(new System.Windows.Point((double)mouse.X, (double)mouse.Y));

            #region Commented Out
            //System.Windows.Interop.HwndSource presentationSource =
            //    (System.Windows.Interop.HwndSource)PresentationSource.FromVisual( relativeTo );
            //ScreenToClient( presentationSource.Handle, ref mouse );
            //GeneralTransform transform = relativeTo.TransformToAncestor( presentationSource.RootVisual );
            //Point offset = transform.Transform( new Point( 0, 0 ) );
            //return new Point( mouse.X - offset.X, mouse.Y - offset.Y );
            #endregion // Commented Out
        }
    }
}
