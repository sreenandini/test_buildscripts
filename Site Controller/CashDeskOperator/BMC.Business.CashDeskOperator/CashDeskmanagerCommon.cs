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
using BMC.Common.LogManagement;
using System.IO;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Common.Utilities;


namespace BMC.Business.CashDeskOperator
{
    public  class CashDeskmanagerCommon
    {
        public  bool isValidDateRange(DateTime dStartdate, DateTime dEndDate)
        {
            return dEndDate >= dStartdate;
        }


        public  bool ExportToExcel(ListView lvView, string path)
        {
            try
            {
                LogManager.WriteLog("Process started - 1", LogManager.enumLogLevel.Info);
                Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();

                Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);

                Worksheet ws = (Worksheet)xla.ActiveSheet;
                LogManager.WriteLog("Process started - 2", LogManager.enumLogLevel.Info);
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
                    //    //    NotFound = true;z
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
               foreach (TicketExceptions item in lvView.Items)
                {

                    ListViewItem item1 = lvView.ItemContainerGenerator.ContainerFromIndex(lvView.Items.IndexOf(item)) as ListViewItem;

                    GridViewRowPresenter rowPresenter =
                          GetFrameworkElementByName<GridViewRowPresenter>(item1);
                     for (int col = 0; col < colcount; col++)
                    {
                        if (rowPresenter != null)
                        {
                            ContentPresenter templatedParent = System.Windows.Media.VisualTreeHelper.GetChild(rowPresenter, col) as ContentPresenter;
                            foreach (PropertyInfo info in item.GetType().GetProperties())
                            {
                                TextBlock block = (TextBlock)gridView.Columns[col].CellTemplate.FindName(info.Name, templatedParent);
                                if (block != null)
                                {
                                    
                                    if (gridView.Columns[col].Header.ToString().ToUpper() == block.Tag.ToString().ToUpper())
                                    {
                                        ws.Cells[i, j] = block.Text;
                                        j++;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    j = 1;
                    i++;
                }               

                ws.SaveAs(path, Microsoft.Office.Interop.Excel.XlFileFormat.xlXMLSpreadsheet,
                    null, null, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, null, null, null);

               // ws.Delete();
                return true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Inside catch", LogManager.enumLogLevel.Info);
                int i = 1;

                int j = 1;
                int colcount = 0;
                FileInfo f = new FileInfo(path);
                if (File.Exists(path))
                {
                    try
                    {
                        File.Delete(path);
                    }                   
                    catch (IOException iex)
                    {
                        LogManager.WriteLog("Inside IOException catch:File being used by another  process" + iex, LogManager.enumLogLevel.Info);
                        return false;
                    }
                }
                StreamWriter w = f.CreateText();
                GridView view = (System.Windows.Controls.GridView)(lvView.View);

                foreach (GridViewColumn col in (IEnumerable<GridViewColumn>)view.Columns)
                {
                    w.Write(col.Header + ",");
                    j++;
                }
                i++;
                //j = 1;

                colcount = j - 1;
                j = 1;
                w.WriteLine("");


                GridView gridView = lvView.View as GridView;
               
                foreach (TicketExceptions item in lvView.Items)
                {

                    ListViewItem item1 = lvView.ItemContainerGenerator.ContainerFromIndex(lvView.Items.IndexOf(item)) as ListViewItem;

                    GridViewRowPresenter rowPresenter =
                          GetFrameworkElementByName<GridViewRowPresenter>(item1);
                    for (int col = 0; col < colcount; col++)
                    {
                        if (rowPresenter != null)
                        {
                            ContentPresenter templatedParent = System.Windows.Media.VisualTreeHelper.GetChild(rowPresenter, col) as ContentPresenter;
                            foreach (PropertyInfo info in item.GetType().GetProperties())
                            {
                                TextBlock block = (TextBlock)gridView.Columns[col].CellTemplate.FindName(info.Name, templatedParent);
                                if (block != null)
                                {                                  
                                    if (gridView.Columns[col].Header.ToString().ToUpper() == block.Tag.ToString().ToUpper())
                                    {
                                        w.Write(block.Text, ",");
                                        j++;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    j = 1;
                    i++;
                    w.WriteLine("");
                }
                LogManager.WriteLog("Saved", LogManager.enumLogLevel.Info);

                ExceptionManager.Publish(ex);
                return true;
            }

        }

        private string Format(double number)
        {
            return  string.Format((number<0)?"\"({0})\",":"\"{0}\",", number + "");
        }

         bool IsMouseOver(Visual target)
        {
            // We need to use MouseUtilities to figure out the cursor
            // coordinates because, during a drag-drop operation, the WPF
            // mechanisms for getting the coordinates behave strangely.

            Rect bounds = VisualTreeHelper.GetDescendantBounds(target);
            System.Windows.Point mousePos = MouseUtilities.GetMousePosition(target);
            return bounds.Contains(mousePos);
        }


        public  T GetFrameworkElementByName<T>(FrameworkElement referenceElement) where T : FrameworkElement
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

        public  void getContainers(ListView lvView)
        {

            // gets all nodes from the TreeView  
            List<ListViewItem> allTreeContainers = GetAllItemContainers(lvView);

        }

         List<ListViewItem> GetAllItemContainers(ListView itemsControl)
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

         public bool HourlyExportToExcel(ListView lvView, string path, bool isFromMainScreen)
         {
             try
             {

                 LogManager.WriteLog("Process started - 1", LogManager.enumLogLevel.Info);
                 Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
                 StreamWriter sw = new StreamWriter(path);
                 Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);

                 Worksheet ws = (Worksheet)xla.ActiveSheet;
                 LogManager.WriteLog("Process started - 2", LogManager.enumLogLevel.Info);
                 int i = 1;
                 int j = 1;
                 GridView view = (System.Windows.Controls.GridView)(lvView.View);
                 GridView gridView = lvView.View as GridView;
                 string mystr = string.Empty;
                 int sum = -1;
                 foreach (GridViewColumn dc in gridView.Columns)
                 {
                     ++sum;
                     if (isFromMainScreen && (sum == 1 || sum == 3 || sum == 4)) continue;
                     else if (!isFromMainScreen && (sum == 0 || sum == 2)) continue;
                     string mynewstr = string.Format("\"{0}\"", dc.Header.ToString() + "");

                     mystr += mynewstr + ",";

                 }
                 mystr += "\r\n";
                 //int Count = -1;
                 foreach (HourlyDetailEntity item in lvView.Items)
                 {
                     if (isFromMainScreen)
                     {
                         mystr += string.Format("\"{0}\",", Convert.ToString(item.Date.GetUniversalDateFormat()) + "");
                         mystr += string.Format("\"{0}\",", Convert.ToString(item.Day) + "");
                         //if (item.Total == 0.0)
                         //{
                         //    mystr += string.Format("\"{0}\",", "-" + "");
                         //}
                         // else
                         // {

                         // }
                     }
                     else if (!isFromMainScreen)
                     {
                         mystr += string.Format("\"{0}\",", Convert.ToString(item.Bar_Position_Name) + "");
                         //++Count;
                         //if (Count == 0)
                         //    mystr += string.Format("\"{0}\",", "Total"); 

                         //else
                         //    mystr += string.Format("\"{0}\",", Count);
                         mystr += string.Format("\"{0}\",", Convert.ToString(item.Machine_Name) + "");
                         mystr += string.Format("\"{0}\",", Convert.ToString(item.Stock_No) + "");
                     }


                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.Stock_No) + "");
                     //}
                     mystr += Format(item.Total);//string.Format("\"{0}\",", Convert.ToString(item.Total) + "");
                     mystr += Format(item.HS_Hour1_Value);
                     mystr += Format(item.HS_Hour2_Value);
                     mystr += Format(item.HS_Hour3_Value);
                     mystr += Format(item.HS_Hour4_Value);
                     mystr += Format(item.HS_Hour5_Value);
                     mystr += Format(item.HS_Hour6_Value);
                     mystr += Format(item.HS_Hour7_Value);
                     mystr += Format(item.HS_Hour8_Value);
                     mystr += Format(item.HS_Hour9_Value);
                     mystr += Format(item.HS_Hour10_Value);
                     mystr += Format(item.HS_Hour11_Value);
                     mystr += Format(item.HS_Hour12_Value);
                     mystr += Format(item.HS_Hour13_Value);
                     mystr += Format(item.HS_Hour14_Value);
                     mystr += Format(item.HS_Hour15_Value);
                     mystr += Format(item.HS_Hour16_Value);
                     mystr += Format(item.HS_Hour17_Value);
                     mystr += Format(item.HS_Hour18_Value);
                     mystr += Format(item.HS_Hour19_Value);
                     mystr += Format(item.HS_Hour20_Value);
                     mystr += Format(item.HS_Hour21_Value);
                     mystr += Format(item.HS_Hour22_Value);
                     mystr += Format(item.HS_Hour23_Value);
                     mystr += Format(item.HS_Hour24_Value);
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour1_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour2_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour3_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour4_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour5_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour6_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour7_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour8_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour9_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour10_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour11_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour12_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour13_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour14_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour15_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour16_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour17_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour18_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour19_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour20_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour21_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour22_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour23_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour24_Value) + "");
                     mystr += "\r\n";
                 }
                 mystr += "\r\n";
                 sw.Write(mystr);
                 mystr = string.Empty;
                 sw.Close();
                 return true;
             }
             catch (Exception ex)
             {
                 ExceptionManager.Publish(ex);
                 return false;
             }
         }

         public bool HourlyExportToExcel(Microsoft.Windows.Controls.DataGrid lvView, string path, bool isFromMainScreen)
         {
             try
             {

                 LogManager.WriteLog("Process started - 1", LogManager.enumLogLevel.Info);
                 Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
                 StreamWriter sw = new StreamWriter(path);
                 Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);

                 Worksheet ws = (Worksheet)xla.ActiveSheet;
                 LogManager.WriteLog("Process started - 2", LogManager.enumLogLevel.Info);
                 int i = 1;
                 int j = 1;                 
                 Microsoft.Windows.Controls.DataGrid gridView = lvView;
                 string mystr = string.Empty;
                 int sum = -1;
                 foreach (Microsoft.Windows.Controls.DataGridColumn dc in gridView.Columns)
                 {
                     ++sum;
                     if (isFromMainScreen && (sum == 1 || sum == 3 || sum == 4)) continue;
                     else if (!isFromMainScreen && (sum == 0 || sum == 2)) continue;
                     string mynewstr = string.Format("\"{0}\"", dc.Header.ToString() + "");

                     mystr += mynewstr + ",";

                 }
                 mystr += "\r\n";
                 //int Count = -1;
                 foreach (HourlyDetailEntity item in lvView.Items)
                 {
                     if (isFromMainScreen)
                     {
                         mystr += string.Format("\"{0}\",", Convert.ToString(item.Date.GetUniversalDateFormat()) + "");
                         mystr += string.Format("\"{0}\",", Convert.ToString(item.Day) + "");
                         //if (item.Total == 0.0)
                         //{
                         //    mystr += string.Format("\"{0}\",", "-" + "");
                         //}
                         // else
                         // {

                         // }
                     }
                     else if (!isFromMainScreen)
                     {
                         mystr += string.Format("\"{0}\",", Convert.ToString(item.Bar_Position_Name) + "");
                         //++Count;
                         //if (Count == 0)
                         //    mystr += string.Format("\"{0}\",", "Total"); 

                         //else
                         //    mystr += string.Format("\"{0}\",", Count);
                         mystr += string.Format("\"{0}\",", Convert.ToString(item.Machine_Name) + "");
                         mystr += string.Format("\"{0}\",", Convert.ToString(item.Stock_No) + "");
                     }


                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.Stock_No) + "");
                     //}
                     mystr += Format(item.Total);//string.Format("\"{0}\",", Convert.ToString(item.Total) + "");
                     mystr += Format(item.HS_Hour1_Value);
                     mystr += Format(item.HS_Hour2_Value);
                     mystr += Format(item.HS_Hour3_Value);
                     mystr += Format(item.HS_Hour4_Value);
                     mystr += Format(item.HS_Hour5_Value);
                     mystr += Format(item.HS_Hour6_Value);
                     mystr += Format(item.HS_Hour7_Value);
                     mystr += Format(item.HS_Hour8_Value);
                     mystr += Format(item.HS_Hour9_Value);
                     mystr += Format(item.HS_Hour10_Value);
                     mystr += Format(item.HS_Hour11_Value);
                     mystr += Format(item.HS_Hour12_Value);
                     mystr += Format(item.HS_Hour13_Value);
                     mystr += Format(item.HS_Hour14_Value);
                     mystr += Format(item.HS_Hour15_Value);
                     mystr += Format(item.HS_Hour16_Value);
                     mystr += Format(item.HS_Hour17_Value);
                     mystr += Format(item.HS_Hour18_Value);
                     mystr += Format(item.HS_Hour19_Value);
                     mystr += Format(item.HS_Hour20_Value);
                     mystr += Format(item.HS_Hour21_Value);
                     mystr += Format(item.HS_Hour22_Value);
                     mystr += Format(item.HS_Hour23_Value);
                     mystr += Format(item.HS_Hour24_Value);
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour1_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour2_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour3_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour4_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour5_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour6_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour7_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour8_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour9_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour10_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour11_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour12_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour13_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour14_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour15_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour16_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour17_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour18_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour19_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour20_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour21_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour22_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour23_Value) + "");
                     //mystr += string.Format("\"{0}\",", Convert.ToString(item.HS_Hour24_Value) + "");
                     mystr += "\r\n";
                 }
                 mystr += "\r\n";
                 sw.Write(mystr);
                 mystr = string.Empty;
                 sw.Close();
                 return true;
             }
             catch (Exception ex)
             {
                 ExceptionManager.Publish(ex);
                 return false;
             }
         }

        public  void CloseExcel()
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
