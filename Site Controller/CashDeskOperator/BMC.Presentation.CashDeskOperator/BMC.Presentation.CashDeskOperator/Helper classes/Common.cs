
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Windows;
using System.Reflection;
using BMC.Common.ConfigurationManagement;
using System.ComponentModel;

namespace BMC.Presentation.Helper_classes
{
    public class Common
    {

        #region Constant

        const int IntegerNull = int.MinValue;
        DateTime DateUnassigned = DateTime.Parse("12:00:00 AM");
        DateTime DateNull = DateTime.Parse("1/1/1800");
        DateTime DateMin = DateTime.Parse("1/1/1801");
        //DateTime DateMax = DateTime.Parse("12/31/9999");
        DateTime DateTimeBase = DateTime.Parse("1/2/1801");
        const short ShortNull = short.MinValue;
        const decimal DecimalNull = decimal.MinValue;
        const double DoubleNull = double.MinValue;
        const long LongNull = long.MinValue;
        const string StringNull = "";
        public enum PagingMode { First = 1, Next = 2, Previous = 3, Last = 4 };
        public int paging_PageIndex = 1;

        public System.Windows.Controls.Button btnFirst { get; set; }
        public System.Windows.Controls.Button btnNext { get; set; }
        public System.Windows.Controls.Button btnPrev { get; set; }
        public System.Windows.Controls.Button btnLast { get; set; }
        public System.Windows.Controls.TextBlock txtPage { get; set; }

        #endregion

        public static object GetNull(TypeCode DataTypeCode)
        {
            switch (DataTypeCode)
            {
                case TypeCode.Boolean:
                    return 0;
                case TypeCode.DateTime:
                    return DateTime.Parse("1/1/1800");
                case TypeCode.Decimal:
                    return DecimalNull;
                case TypeCode.Double:
                    return DoubleNull;
                case TypeCode.Int32:
                    return IntegerNull;
                case TypeCode.Int64:
                    return IntegerNull;
                case TypeCode.String:
                    return StringNull;
                default:
                    return null;
            }
        }

        public static T GetRowValue<T>(DataRow Row, string ColumnName)
        {
            Type TypeOfT;
            TypeOfT = typeof(T);
            TypeCode tpCode = Type.GetTypeCode(TypeOfT);
            switch (tpCode)
            {

                case TypeCode.DateTime:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return (T)(object)DateTime.Parse("1/1/1800");
                    else
                        return (T)Row[ColumnName];
                case TypeCode.Decimal:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return (T)(object)DecimalNull;
                    else
                        return (T)Row[ColumnName];
                case TypeCode.Double:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return (T)(object)DoubleNull;
                    else
                        //return (T)Row[ColumnName];
                        return (T)(object)Convert.ToDouble(Row[ColumnName]);
                case TypeCode.Int32:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return (T)(object)int.Parse("0");
                    else
                        return (T)Row[ColumnName];

                case TypeCode.Int64:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return (T)(object)int.Parse("0");
                    else
                        return (T)Row[ColumnName];

                case TypeCode.String:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return (T)(object)string.Empty;
                    else
                        return (T)Row[ColumnName];

                case TypeCode.Boolean:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return (T)(object)false;
                    else
                        return (T)Row[ColumnName];

                default:
                    return (T)(object)null;
            }

        }

        public static DataTable ConvertToDatatable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    table.Columns.Add(prop.Name, prop.PropertyType.GetGenericArguments()[0]);
                else
                    table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public static string GetRowValueToTextBox<T>(DataRow Row, string ColumnName)
        {
            Type TypeOfT;
            TypeOfT = typeof(T);
            TypeCode tpCode = Type.GetTypeCode(TypeOfT);
            switch (tpCode)
            {

                case TypeCode.DateTime:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return DateTime.Parse("1/1/1800").ToString().Trim();
                    else
                        return Row[ColumnName].ToString().Trim();
                case TypeCode.Decimal:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return "0";
                    else
                        return Row[ColumnName].ToString().Trim();
                case TypeCode.Double:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return "0";
                    else
                        return Row[ColumnName].ToString().Trim();

                case TypeCode.Int32:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return "0";
                    else
                        return Row[ColumnName].ToString().Trim();

                case TypeCode.Int64:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return "0";
                    else
                        return Row[ColumnName].ToString().Trim();

                case TypeCode.String:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return StringNull.ToString().Trim();
                    else
                        return Row[ColumnName].ToString().Trim();

                default:
                    return (string)(object)null;
            }

        }

        public static DataTable GetDataTableFromReader(System.Data.SqlClient.SqlDataReader DataReader)
        {
            DataTable newTable = new DataTable();
            DataColumn Col;
            DataRow Row;
            for (int i = 0; i < DataReader.FieldCount - 1; i++)
            {
                Col = new DataColumn();
                Col.ColumnName = DataReader.GetName(i);
                Col.DataType = DataReader.GetFieldType(i);

                newTable.Columns.Add(Col);
            }

            while (DataReader.Read())
            {
                Row = newTable.NewRow();
                for (int i = 0; i < DataReader.FieldCount - 1; i++)
                {
                    Row[i] = DataReader[i];
                }
                newTable.Rows.Add(Row);
            }

            return newTable;
        }

        public static void BindListView(DataTable table, System.Windows.Controls.ListView listview)
        {
            System.Windows.Data.Binding bind = new System.Windows.Data.Binding();
            bind.Source = table;
            listview.SetBinding(System.Windows.Controls.ListView.ItemsSourceProperty, bind);
        }

        public static void BindDataGrid(DataTable table, Microsoft.Windows.Controls.DataGrid dataGrid)
        {
            System.Windows.Data.Binding bind = new System.Windows.Data.Binding();
            bind.Source = table;
            dataGrid.SetBinding(Microsoft.Windows.Controls.DataGrid.ItemsSourceProperty, bind);
        }

        public static void BindDataGrid<T>(IEnumerable<T> template, Microsoft.Windows.Controls.DataGrid dataGrid)
        {
            System.Windows.Data.Binding bind = new System.Windows.Data.Binding();
            bind.Source = template;
            dataGrid.SetBinding(Microsoft.Windows.Controls.DataGrid.ItemsSourceProperty, bind);
        }


        public static void BindListView<T>(IEnumerable<T> template,  System.Windows.Controls.ListView listview)
        {
            System.Windows.Data.Binding bind = new System.Windows.Data.Binding();
            bind.Source = template;
            listview.SetBinding(System.Windows.Controls.ListView.ItemsSourceProperty, bind);
        }

        public void CustomPaging(PagingMode Mode, DataTable sourceTable, int NoOfRecPerPage, System.Windows.Controls.ListView listview, bool updated)
        {
            int totalRecords = sourceTable.Rows.Count;

            if (totalRecords <= 0)
                return;

            if (updated)
                paging_PageIndex -= 1;

            switch (Mode)
            {
                case PagingMode.First:
                    paging_PageIndex = 2;
                    CustomPaging(PagingMode.Previous, sourceTable, NoOfRecPerPage, listview, updated);
                    break;
                case PagingMode.Next:
                    if (totalRecords == paging_PageIndex * NoOfRecPerPage)
                        paging_PageIndex -= 1;

                    if (totalRecords > (paging_PageIndex * NoOfRecPerPage))
                    {
                        DataTable tmpTable = new DataTable();
                        tmpTable = sourceTable.Clone();

                        if (totalRecords >= ((paging_PageIndex * NoOfRecPerPage) + NoOfRecPerPage))
                        {
                            for (int i = paging_PageIndex * NoOfRecPerPage;
                                 i < ((paging_PageIndex * NoOfRecPerPage) + NoOfRecPerPage); i++)
                            {
                                tmpTable.ImportRow(sourceTable.Rows[i]);
                            }
                        }
                        else
                        {
                            for (int i = paging_PageIndex * NoOfRecPerPage; i < totalRecords; i++)
                            {
                                tmpTable.ImportRow(sourceTable.Rows[i]);
                            }
                        }

                        paging_PageIndex += 1;

                        BindListView(tmpTable, listview);
                        tmpTable.Dispose();
                    }
                    break;
                case PagingMode.Previous:
                    if (paging_PageIndex > 1)
                    {
                        DataTable tmpTable = new DataTable();
                        tmpTable = sourceTable.Clone();

                        paging_PageIndex -= 1;

                        for (int i = ((paging_PageIndex * NoOfRecPerPage) - NoOfRecPerPage);
                            i < (paging_PageIndex * NoOfRecPerPage); i++)
                        {
                            if (i == totalRecords)
                                break;
                            tmpTable.ImportRow(sourceTable.Rows[i]);
                        }

                        BindListView(tmpTable, listview);
                        tmpTable.Dispose();
                    }
                    break;
                case PagingMode.Last:
                    paging_PageIndex = (totalRecords / NoOfRecPerPage);
                    CustomPaging(PagingMode.Next, sourceTable, NoOfRecPerPage, listview, updated);
                    break;
                default:
                    break;
            }
        }

        public void DisplayPagingInfo(DataTable table, Common common, int NoOfRecPerPage)
        {
            int pageSize = table.Rows.Count / NoOfRecPerPage;
            double origPageSize = Convert.ToDouble(table.Rows.Count) / Convert.ToDouble(NoOfRecPerPage);

            if (origPageSize > Convert.ToDouble(pageSize))
                pageSize += 1;

            if ((paging_PageIndex == 1) && (pageSize <= 1))
            {
                common.btnPrev.Visibility = Visibility.Hidden;
                common.btnNext.Visibility = Visibility.Hidden;
                common.btnFirst.Visibility = Visibility.Hidden;
                common.btnLast.Visibility = Visibility.Hidden;
                return;
            }
            else if ((paging_PageIndex == 1) && (pageSize != 1))
            {
                common.btnPrev.Visibility = Visibility.Hidden;
                common.btnNext.Visibility = Visibility.Visible;
                common.btnFirst.Visibility = Visibility.Hidden;
                common.btnLast.Visibility = Visibility.Visible;
            }
            else if ((paging_PageIndex != 1) && (paging_PageIndex == pageSize))
            {
                common.btnPrev.Visibility = Visibility.Visible;
                common.btnNext.Visibility = Visibility.Hidden;
                common.btnLast.Visibility = Visibility.Hidden;
                common.btnFirst.Visibility = Visibility.Visible;
            }
            else
            {
                common.btnPrev.Visibility = Visibility.Visible;
                common.btnNext.Visibility = Visibility.Visible;
                common.btnLast.Visibility = Visibility.Visible;
                common.btnFirst.Visibility = Visibility.Visible;
            }

            string pagingInfo = "Page " + paging_PageIndex +
            " of " + pageSize;

            common.txtPage.Text = pagingInfo;

        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <typeparam name="T">Type of the disposal object.</typeparam>
        /// <param name="obj">The com object.</param>
        public static void DisposeObject<T>(ref T obj)
            where T : class, IDisposable
        {
            DisposeObject(ref obj, true);
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <typeparam name="T">Type of the disposal object.</typeparam>
        /// <param name="obj">The com object.</param>
        /// <param name="reclaimMemory">if set to <c>true</c> [reclaim memory].</param>
        public static void DisposeObject<T>(ref T obj, bool reclaimMemory)
            where T : class, IDisposable
        {
            if (obj != null)
            {
                try
                {
                    obj.Dispose();
                    obj = null;
                    if (reclaimMemory)
                    {
                        GC.Collect();
                    }
                }
                catch
                {
                    BMC.Common.LogManagement.LogManager.WriteLog("Unable to dispose the object.", BMC.Common.LogManagement.LogManager.enumLogLevel.Error);
                }
            }
        }
    }

    public class Handlers
    {
        #region Char Key Handlers
        public static bool IsInteger(char e)
        {
            if (e == (char)46) // This is the character Code for Decimal. 
                return true;
            if (Char.IsNumber(e) || (e == (char)Keys.Back) || e == (char)Keys.Delete || e == (char)Keys.Enter || e == (char)Keys.Tab)
                return false;
            else
                return true;
        }
        public static bool IsAlpha(char e)
        {
            if (Char.IsLetter(e) || e == (char)Keys.Back || e == (char)Keys.Delete || e == (char)Keys.Enter || e == (char)Keys.Tab)
                return false;
            else
                return true;
        }
        public static bool IsAlphaNumeric(char e)
        {
            if (Char.IsLetterOrDigit(e) || e == (char)Keys.Back || e == (char)Keys.Delete || e == (char)Keys.Enter || e == (char)Keys.Tab)
                return false;
            else
                return true;
        }
        public static bool IsInteger(char e, string AllowedCharacters)
        {
            char[] ch = AllowedCharacters.ToCharArray();

            foreach (char C in ch)
            {
                if (e == C)
                {
                    return false;
                }

            }
            if (e == (char)46)
                return true;
            if (Char.IsNumber(e) || (e == (char)Keys.Back) || e == (char)Keys.Delete || e == (char)Keys.Enter || e == (char)Keys.Tab)
                return false;
            else
                return true;
        }
        #endregion

    }
}
