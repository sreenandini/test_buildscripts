using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.IoC;
using BMC.CoreLib.Win32;

namespace BMC.CoreLib.ExcelPackage
{
    public enum ExternalExcelValidationType
    {
        ValidationNone = 0,
        ValidateInputOnly = 1,
        ValidateWholeNumber = 2,
        ValidateDecimal = 3,
        ValidateList = 4,
        ValidateDate = 5,
        ValidateTime = 6,
        ValidateTextLength = 7,
        ValidateCustom = 8,
    }

    internal class KeyValuePairComparer : IComparer<KeyValuePair<int, int>>
    {
        public int Compare(KeyValuePair<int, int> x, KeyValuePair<int, int> y)
        {
            if (x.Key == y.Key &&
                x.Value == y.Value) return 0;
            if (x.Key == y.Key)
            {
                return x.Value.CompareTo(y.Value);
            }
            else
            {
                return x.Key.CompareTo(y.Key);
            }
        }
    }

    public static class ExternalExcelColorMappings
    {
        private static IDictionary<int, int> _colors = new SortedDictionary<int, int>()
        {
            { Color.Black.ToArgb(), 1 },
            { Color.White.ToArgb(), 2 },
            { Color.Red.ToArgb(), 3 },
            { Color.Green.ToArgb(), 4 },
            { Color.Blue.ToArgb(), 5 },
            { Color.Yellow.ToArgb(), 6 },

            { Color.DarkGray.ToArgb(), 16 },
        };
        private static int _black = 0;

        public static int GetColor(int color)
        {
            if (_colors.ContainsKey(color)) return _colors[color];
            else return _black;
        }
    }

    public delegate bool ExternalWriteExcelHandler(IExternalExcelSheet sheet, ExternalExcelWriteArgs args);

    public delegate object GetExcelWriteColumnName<C>(C col, int index);

    public delegate object GetExcelWriteRowItemValue<C, R>(R row, int rowIndex, C col, int index);

    public class ExternalExcelWriteArgs : DisposableObject
    {
        public IAsyncProgress2 Progress { get; set; }
        public object Source { get; set; }
        public bool WriteHeaders { get; set; }
        public bool WriteCheckedItems { get; set; }
        public Action<ExternalExcelRangeFormatInfo> FormatInfo { get; set; }
        public ExternalWriteExcelHandler ExternalWrite { get; set; }
    }

    public class ExternalExcelWriteArgs<T> : ExternalExcelWriteArgs { }

    public interface IExternalExcelApplicationLoader : IDisposableObject
    {
        int LibraryOrder { get; }

        bool IsLoadedProperly { get; }

        string LibraryName { get; }

        IExternalExcelApplication Create();
    }

    public interface IExternalExcelApplication : IDisposableObject
    {
        Exception LastException { get; }

        bool Open(string fileName, bool write);

        bool Save();

        bool Close();

        void RemoveSheetsExcept(string sheetName);

        IExternalExcelSheet AddOrGet(string sheetName);
    }

    public interface IExternalExcelSheet : IDisposableObject
    {
        IExternalExcelCell Cell(int row, int col);

        IExternalExcelColumn Column(int col);

        object[,] Read(IAsyncProgress2 o, ExternalExcelRangeInfo rangeInfo);

        bool Write(IAsyncProgress2 o, object[,] values, ExternalExcelRangeInfo rangeInfo);

        bool Write<T>(ExternalExcelWriteArgs<T> args);

        bool Write<C, R>(IAsyncProgress2 o, object source,
            bool writeHeaders,
            IEnumerable columns, GetExcelWriteColumnName<C> getColumnName,
            IEnumerable items, GetExcelWriteRowItemValue<C, R> getItemValue,
            Action<ExternalExcelRangeFormatInfo> modifyRange);

        void AutoResizeRows(ExternalExcelRangeInfo rangeInfo);

        void AutoResizeColumns(ExternalExcelRangeInfo rangeInfo);

        void FormatRange(ExternalExcelRangeFormatInfo formatInfo);
    }

    public interface IExternalExcelColumn : IDisposableObject
    {
        double Width { get; set; }
    }

    public interface IExternalExcelCell : IDisposableObject
    {
        object Value { get; set; }
    }

    public abstract class ExternalExcelApplicationLoader : DisposableObject, IExternalExcelApplicationLoader
    {
        public ExternalExcelApplicationLoader() { }

        public abstract int LibraryOrder { get; }

        public abstract bool IsLoadedProperly { get; }

        public abstract string LibraryName { get; }

        public IExternalExcelApplication Create()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Create");
            IExternalExcelApplication result = default(IExternalExcelApplication);

            try
            {
                result = this.CreateInternal();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        protected abstract IExternalExcelApplication CreateInternal();
    }

    public abstract class ExternalExcelApplication : DisposableObject, IExternalExcelApplication
    {
        [CLSCompliant(false)]
        protected string _fileName = string.Empty;

        [CLSCompliant(false)]
        protected bool _write = false;

        [CLSCompliant(false)]
        protected Exception _lastException = null;

        private IDictionary<string, IExternalExcelSheet> _worksheets = null;

        public ExternalExcelApplication()
        {
            _worksheets = new SortedDictionary<string, IExternalExcelSheet>(StringComparer.InvariantCultureIgnoreCase);
        }

        public Exception LastException
        {
            get { return _lastException; }
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            this.Close();
        }

        public bool Open(string fileName, bool write)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Open");
            bool result = default(bool);
            _lastException = null;

            try
            {
                _fileName = fileName;
                _write = write;
                result = this.OpenInternal(fileName, write);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
                _lastException = ex;
            }

            return result;
        }

        protected abstract bool OpenInternal(string fileName, bool write);

        public bool Close()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Close");
            bool result = default(bool);

            try
            {
                _worksheets.DisposeObjects();
                result = this.CloseInternal();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            _lastException = null;
            return result;
        }

        protected abstract bool CloseInternal();

        public bool Save()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Save");
            bool result = default(bool);

            try
            {
                result = this.SaveInternal();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
                _lastException = ex;
            }

            return result;
        }

        protected abstract bool SaveInternal();

        public IExternalExcelSheet AddOrGet(string sheetName)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Close");
            IExternalExcelSheet result = default(IExternalExcelSheet);

            try
            {
                result = this.AddOrGetInternal(sheetName);
                _worksheets.AddOrGet(sheetName, () => { return result; });
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        protected abstract IExternalExcelSheet AddOrGetInternal(string sheetName);

        public abstract void RemoveSheetsExcept(string sheetName);
    }

    public static class ExternalExcelApplicationFactory
    {
        private static IExternalExcelApplicationLoader[] _libraryLoaders = null;
        private static IExternalExcelApplicationLoader _activeLibraryLoader = null;
        private static object _librariesLock = new object();

        public static IExternalExcelApplication Create()
        {
            ModuleProc PROC = new ModuleProc("ExternalExcelLibraryFactory", "Create");
            IExternalExcelApplication result = default(IExternalExcelApplication);

            try
            {
                if (_activeLibraryLoader == null)
                {
                    lock (_librariesLock)
                    {
                        if (_activeLibraryLoader == null)
                        {
                            try
                            {
                                IEnumerable<IExternalExcelApplicationLoader> libraryLoaders = null;

                                try
                                {
                                    libraryLoaders = MEFHelper.GetExportedValues<IExternalExcelApplicationLoader>();
                                }
                                catch
                                {
                                    libraryLoaders = GetAssemblyLoaders();
                                }
                                if (libraryLoaders != null)
                                {
                                    _libraryLoaders = (from l in libraryLoaders
                                                        orderby l.LibraryOrder
                                                        select l).ToArray();

                                    if (_libraryLoaders != null)
                                    {
                                        // load the first matched library until find valid one
                                        foreach (IExternalExcelApplicationLoader libraryLoader in _libraryLoaders)
                                        {
                                            if (libraryLoader != null)
                                            {
                                                if (libraryLoader.IsLoadedProperly)
                                                {
                                                    Log.Info(PROC, "Excel Library : " + libraryLoader.LibraryName + " (Loaded)");
                                                    _activeLibraryLoader = libraryLoader;
                                                    break;
                                                }
                                                else
                                                {
                                                    Log.Info(PROC, "Excel Library : " + libraryLoader.LibraryName + " (Unable to load)");
                                                }
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
                    }
                }

                // still no external loaders found (then take ExcelPackage as a rescuer)
                if (_activeLibraryLoader == null)
                {
                    _activeLibraryLoader = new OpenXmlExcelApplicationLoader();
                }

                if (_activeLibraryLoader != null)
                {
                    IExternalExcelApplication library = null;

                    try
                    {
                        library = _activeLibraryLoader.Create();
                    }
                    catch { library = null; }

                    if (library != null)
                    {
                        result = library;
                        Log.Info(PROC, "Excel Library : " + _activeLibraryLoader.LibraryName + " (Created)");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        private static IEnumerable<IExternalExcelApplicationLoader> GetAssemblyLoaders()
        {
            ModuleProc PROC = new ModuleProc("", "GetAssemblyLoaders");
            IList<IExternalExcelApplicationLoader> loaders = new List<IExternalExcelApplicationLoader>();

            try
            {
                string assemblyNames = Extensions.GetAppSettingValue("ExternalExcelApplicationLoaders", "BMC.ExcelLibrary.dll;ClosedXML.dll");
                if (!assemblyNames.IsEmpty())
                {
                    string[] assemblies = assemblyNames.Split(';');
                    if (assemblies != null)
                    {
                        foreach (var assembly in assemblies)
                        {
                            string asmPath = Path.Combine(Extensions.GetStartupDirectory(), assembly);
                            Log.Info(PROC, "Excel Assembly Loader : " + asmPath);
                            if (File.Exists(asmPath))
                            {
                                try
                                {
                                    Assembly asm = Assembly.LoadFrom(asmPath);
                                    if (asm != null)
                                    {
                                        var loaderType = (from t in asm.GetTypes()
                                                          where typeof(IExternalExcelApplicationLoader).IsAssignableFrom(t)
                                                          select t).FirstOrDefault();
                                        if (loaderType != null)
                                        {
                                            IExternalExcelApplicationLoader loader = Activator.CreateInstance(loaderType) as IExternalExcelApplicationLoader;
                                            if (loader != null)
                                            {
                                                loaders.Add(loader);
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Log.Exception(PROC, ex);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return loaders;
        }
    }

    public abstract class ExternalExcelSheet : DisposableObject, IExternalExcelSheet
    {
        protected IDictionary<KeyValuePair<int, int>, IExternalExcelCell> _cells = null;
        protected IDictionary<int, IExternalExcelColumn> _columns = null;

        public ExternalExcelSheet()
        {
            _cells = new SortedDictionary<KeyValuePair<int, int>, IExternalExcelCell>(new KeyValuePairComparer());
            _columns = new SortedDictionary<int, IExternalExcelColumn>();
        }

        public IExternalExcelCell Cell(int row, int col)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Cell");
            IExternalExcelCell result = default(IExternalExcelCell);

            try
            {
                KeyValuePair<int, int> pair = new KeyValuePair<int, int>(row, col);
                if (_cells.ContainsKey(pair))
                {
                    IExternalExcelCell cell = _cells[pair];
                    if (cell != null)
                    {
                        if (cell.IsDisposed)
                        {
                            _cells.Remove(pair);
                        }
                        else
                        {
                            result = cell;
                        }
                    }
                }

                if (result == null)
                {
                    result = this.CellInternal(row, col);
                    _cells.Add(pair, result);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        protected abstract IExternalExcelCell CellInternal(int row, int col);

        public IExternalExcelColumn Column(int col)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Cell");
            IExternalExcelColumn result = default(IExternalExcelColumn);

            try
            {
                if (_columns.ContainsKey(col))
                {
                    IExternalExcelColumn column = _columns[col];
                    if (column != null)
                    {
                        if (column.IsDisposed)
                        {
                            _columns.Remove(col);
                        }
                        else
                        {
                            result = column;
                        }
                    }
                }

                if (result == null)
                {
                    result = this.ColumnInternal(col);
                    _columns.Add(col, result);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        protected abstract IExternalExcelColumn ColumnInternal(int col);

        public object[,] Read(IAsyncProgress2 o, ExternalExcelRangeInfo rangeInfo)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Read");
            object[,] result = default(object[,]);

            try
            {
                result = this.ReadInternal(o, rangeInfo);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        protected abstract object[,] ReadInternal(IAsyncProgress2 o, ExternalExcelRangeInfo rangeInfo);

        [CLSCompliant(false)]
        protected virtual object[,] _ReadInternal(IAsyncProgress2 o, ExternalExcelRangeInfo rangeInfo)
        {
            int rows = rangeInfo.EndRow - rangeInfo.StartRow + 1;
            int cols = rangeInfo.EndColumn - rangeInfo.StartColumn + 1;

            int[] lowerBounds = new int[] { 1, 1 };
            int[] lengths = new int[] { rows, cols };
            object[,] values = (object[,])Array.CreateInstance(typeof(object), lengths, lowerBounds);

            int totalRows = rows;
            int row = rangeInfo.StartRow;
            int count = 1;

            if (o != null) o.InitializeProgress(1, totalRows);
            for (int i = values.GetLowerBound(0); i <= values.GetUpperBound(0); i++)
            {
                if (o != null && o.ExecutorService.IsShutdown) break;

                int col = rangeInfo.StartColumn;
                for (int j = values.GetLowerBound(1); j <= values.GetUpperBound(1); j++)
                {
                    values[i, j] = this.Cell(row, col).Value;
                    if (o != null) Thread.Sleep(1);
                    col++;
                }

                if (o != null)
                {
                    int proPerc = (int)(((float)(i) / (float)totalRows) * 100.0);
                    o.UpdateStatusProgress((count), "Reading value from excel . . . : " +
                                        (count) + " of " + totalRows.ToString() +
                                        " (" + proPerc + "%)");
                }

                row++;
                count++;
                if (o != null) Thread.Sleep(1);
            }

            return values;
        }

        public bool Write(IAsyncProgress2 o, object[,] values, ExternalExcelRangeInfo rangeInfo)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Write");
            bool result = default(bool);

            try
            {
                result = this.WriteInternal(o, values, rangeInfo);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        protected abstract bool WriteInternal(IAsyncProgress2 o, object[,] values, ExternalExcelRangeInfo rangeInfo);

        [CLSCompliant(false)]
        protected virtual bool _WriteInternal(IAsyncProgress2 o, object[,] values, ExternalExcelRangeInfo rangeInfo)
        {
            int totalRows = values.GetUpperBound(0) + 1;
            int row = rangeInfo.StartRow;
            int count = 1;

            if (o != null) o.InitializeProgress(1, totalRows);
            for (int i = values.GetLowerBound(0); i <= values.GetUpperBound(0); i++)
            {
                if (o != null && o.ExecutorService.IsShutdown) break;

                int col = rangeInfo.StartColumn;
                for (int j = values.GetLowerBound(1); j <= values.GetUpperBound(1); j++)
                {
                    this.Cell(row, col).Value = values[i, j].ToString();
                    if (o != null) Thread.Sleep(1);
                    col++;
                }

                if (o != null)
                {
                    int proPerc = (int)(((float)(i) / (float)totalRows) * 100.0);
                    o.UpdateStatusProgress((count), "Writing value to excel  . . . : " +
                                        (count) + " of " + totalRows.ToString() +
                                        " (" + proPerc + "%)");
                }

                row++;
                count++;
                if (o != null) Thread.Sleep(1);
            }

            return true;
        }

        public bool Write<T>(ExternalExcelWriteArgs<T> args)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Write");
            bool result = default(bool);

            try
            {
                IAsyncProgress2 o = args.Progress;
                object source = args.Source;
                bool writeHeaders = args.WriteHeaders;
                bool writeCheckItemsOnly = args.WriteCheckedItems;
                Action<ExternalExcelRangeFormatInfo> modifyRange = args.FormatInfo;
                Type sourceType = source.GetType();
                bool isList = false;

                if (args.ExternalWrite != null) // It is caller's responsibility to do the write operation
                {
                    return args.ExternalWrite(this, args);
                }

                if (source is DataTable) // Data Table
                {
                    DataTable dt = source as DataTable;
                    result = this.Write<DataColumn, DataRow>(o, dt, writeHeaders,
                       dt.Columns, (c, i) => { return c.ColumnName; },
                       dt.Rows, (r, i, c, j) => { return r[c]; },
                       modifyRange);
                }
                else if (source is DataGridView) // Windows Forms Data Grid
                {
                    DataGridView dgv = source as DataGridView;
                    result = this.Write<DataGridViewColumn, DataGridViewRow>(o, dgv, writeHeaders,
                        dgv.Columns, (c, i) => { return c.HeaderText; },
                        dgv.Rows, (r, i, c, j) => { return r.Cells[c.Index].Value; },
                        modifyRange);
                }
                else if (source is ListView) // List View
                {
                    ListView lvw = source as ListView;
                    ICollection items = lvw.Items;
                    if (writeCheckItemsOnly)
                    {
                        o.CrossThreadInvoke(new Action(() =>
                        {
                            if (lvw.MultiSelect &&
                             lvw.SelectedItems != null &&
                             lvw.SelectedItems.Count > 0)
                            {
                                items = lvw.SelectedItems;
                            }
                            else if (lvw.CheckBoxes &&
                                lvw.CheckedItems != null &&
                                lvw.CheckedItems.Count > 0)
                            {
                                items = lvw.CheckedItems;
                            }
                        }));
                    }

                    result = this.Write<ColumnHeader, ListViewItem>(o, lvw, writeHeaders,
                        lvw.Columns, (c, i) => { return c.Text; },
                        items, (r, i, c, j) => { return r.SubItems[c.Index].Text; },
                        modifyRange);
                }
                else if (source is System.Windows.Controls.ItemsControl) // WPF Items Control
                {
                    System.Windows.Controls.ItemsControl ctlItems = source as System.Windows.Controls.ItemsControl;
                    System.Windows.Controls.DataGrid dgv = source as System.Windows.Controls.DataGrid;

                    if (ctlItems.ItemsSource != null &&
                        typeof(IList).IsAssignableFrom(ctlItems.ItemsSource.GetType()))
                    {
                        source = ctlItems.ItemsSource;
                        sourceType = source.GetType();
                        isList = true;
                    }
                    else if (dgv != null)
                    {
                        result = this.Write<System.Windows.Controls.DataGridColumn, System.Windows.Controls.DataGridRow>(o, dgv, writeHeaders,
                            dgv.Columns, (c, i) => { return c.Header; },
                            dgv.Items, (r, i, c, j) => { return null; },
                            modifyRange);
                    }
                }
                else if (typeof(IList).IsAssignableFrom(sourceType)) // IList (or) IList<T>
                {
                    isList = true;
                }

                // if the item is derived from IList
                if (isList)
                {
                    Type currentType = sourceType;
                    Type typeOfT = null;
                    while (currentType != typeof(object))
                    {
                        Type[] types = currentType.GetGenericArguments();
                        if (types != null && types.Length > 0)
                        {
                            typeOfT = types[0];
                            break;
                        }
                        currentType = currentType.BaseType;
                    }
                    if (typeOfT == null) return false;

                    if (typeof(IList<T>).IsAssignableFrom(sourceType))
                        this.WriteList<T>(source, typeOfT, ref result, o, writeHeaders, modifyRange);
                    else
                        this.WriteList<object>(source, typeOfT, ref result, o, writeHeaders, modifyRange);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        private void WriteList<T>(object source, Type typeOfT, ref bool result,
            IAsyncProgress2 o, bool writeHeaders, Action<ExternalExcelRangeFormatInfo> modifyRange)
        {
            IList items = source as IList;
            IList<PropertyInfo> columns = null;

            // get all the public properties
            columns = (from p in typeOfT.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                       select p).ToList();
            if (columns != null && columns.Count > 0)
            {
                result = this.Write<PropertyInfo, T>(o, items, writeHeaders,
                    columns, (c, i) => { return c.Name; },
                    items, (r, i, c, j) =>
                    {
                        return columns[j].GetValue(r, null);
                    },
                    modifyRange);
            }
        }

        public bool Write<C, R>(IAsyncProgress2 o, object source,
            bool writeHeaders,
            IEnumerable columns, GetExcelWriteColumnName<C> getColumnName,
            IEnumerable items, GetExcelWriteRowItemValue<C, R> getItemValue,
            Action<ExternalExcelRangeFormatInfo> modifyRange)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Write");
            bool result = default(bool);
            if (source == null || items == null || columns == null) return false;

            try
            {
                int rows = 0;
                int cols = 0;
                o.CrossThreadInvoke(new Action(() =>
                {
                    rows = (writeHeaders ? items.GetCollectionCount<R>() + 1 : items.GetCollectionCount<R>());
                    cols = columns.GetCollectionCount<C>();
                }));
                
                int rowIndex = 1, rowIndex0 = 0;
                int colIndex = 1, colIndex0 = 0;
                object[,] values = Extensions.CreateArray(rows, cols, 1, 1);

                if (o != null) o.InitializeProgress(1, rows);
                ExternalExcelRangeFormatInfo rangeInfo = new ExternalExcelRangeFormatInfo(1, 1, rows, cols);
                if (modifyRange != null) modifyRange(rangeInfo);

                if (writeHeaders)
                {
                    if (o != null) o.UpdateStatusProgress(rowIndex, "Preparing row : " + rowIndex.ToString());
                    colIndex0 = 0;

                    foreach (C col in columns.OfType<C>())
                    {
                        o.CrossThreadInvoke(new Action(() =>
                        {
                            values[rowIndex, colIndex] = getColumnName(col, colIndex0);
                        }));
                        using (ExternalExcelRangeFormatInfo rangeInfo2 = new ExternalExcelRangeFormatInfo(rowIndex, colIndex, rowIndex, colIndex))
                        {
                            rangeInfo2.BackColor = Color.Blue;
                            rangeInfo2.ForeColor = Color.White;
                            rangeInfo2.HorizontalAlignment = ExternalExcelCellHAlign.AlignLeft;
                            rangeInfo2.VerticalAlignment = ExternalExcelCellVAlign.AlignCenter;
                            this.FormatRange(rangeInfo2);
                        }
                        colIndex++;
                        colIndex0++;
                    }

                    using (ExternalExcelRangeFormatInfo rangeInfo2 = new ExternalExcelRangeFormatInfo(rowIndex, 1, rowIndex, cols))
                    {
                        rangeInfo2.AutoFilter = true;
                        this.FormatRange(rangeInfo2);
                    }

                    rowIndex++;
                }

                IEnumerable items2 = null;
                o.CrossThreadInvoke(new Action(() =>
                       {
                           items2 = items.OfType<R>();
                       }));
                IEnumerator enumerator = items2.GetEnumerator();
                if (enumerator != null)
                {
                    bool hasNext = true;
                    while (hasNext)
                    {
                        o.CrossThreadInvoke(new Action(() =>
                            {
                                hasNext = enumerator.MoveNext();
                            }));
                        if (hasNext && enumerator.Current != null)
                        {
                            R dr = (R)enumerator.Current;
                            if (rowIndex > rows) break;
                            colIndex = 1;
                            colIndex0 = 0;

                            foreach (C col in columns)
                            {
                                o.CrossThreadInvoke(new Action(() =>
                                {
                                    values[rowIndex, colIndex] = getItemValue(dr, rowIndex0, col, colIndex0);
                                }));
                                colIndex++;
                                colIndex0++;
                            }

                            if (o != null)
                            {
                                if (o.ExecutorService != null &&
                                    o.ExecutorService.IsShutdown) break;
                                o.UpdateStatusProgress(rowIndex, "Preparing row : " + rowIndex.ToString());
                            }

                            rowIndex++;
                            rowIndex0++;
                            Thread.Sleep(1);
                        }
                    }
                }

                // excel writing
                result = this.Write(o, values, rangeInfo);

                // excel formatting
                o.UpdateStatus("Formatting...");
                this.FormatRange(rangeInfo);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            _cells.DisposeObjects();
            _columns.DisposeObjects();
        }

        public abstract void AutoResizeRows(ExternalExcelRangeInfo rangeInfo);

        public abstract void AutoResizeColumns(ExternalExcelRangeInfo rangeInfo);

        public abstract void FormatRange(ExternalExcelRangeFormatInfo formatInfo);
    }

    public enum ExternalExcelCellHAlign
    {
        AlignRight = -4152,
        AlignLeft = -4131,
        AlignJustify = -4130,
        AlignDistributed = -4117,
        AlignCenter = -4108,
        AlignGeneral = 1,
        AlignFill = 5,
        AlignCenterAcrossSelection = 7,
    }

    public enum ExternalExcelCellVAlign
    {
        AlignTop = -4160,
        AlignJustify = -4130,
        AlignDistributed = -4117,
        AlignCenter = -4108,
        AlignBottom = -4107,
    }

    public class ExternalExcelRangeInfo : DisposableObject
    {
        public ExternalExcelRangeInfo()
            : this(-1, -1, -1, -1) { }

        public ExternalExcelRangeInfo(int startRow, int startColumn)
            : this(startRow, startColumn, startRow, startColumn) { }

        public ExternalExcelRangeInfo(int startRow, int startColumn, int endRow, int endColumn)
        {
            this.StartRow = startRow;
            this.EndRow = endRow;
            this.StartColumn = startColumn;
            this.EndColumn = endColumn;
        }

        public int StartRow { get; private set; }
        public int StartColumn { get; private set; }
        public int EndRow { get; private set; }
        public int EndColumn { get; private set; }

        public bool IsValidRowRange
        {
            get
            {
                return (this.StartRow > 0 && (this.EndRow > 0 && this.EndRow >= this.StartRow));
            }
        }

        public bool IsValidColumnRange
        {
            get
            {
                return (this.StartColumn > 0 && (this.EndColumn > 0 && this.EndColumn >= this.StartColumn));
            }
        }

        public bool IsValidRange
        {
            get
            {
                return this.IsValidRowRange && this.IsValidColumnRange;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return this.StartRow == -1 &
                        this.StartColumn == -1 &&
                        this.EndRow == -1 &&
                        this.EndColumn == -1;
            }
        }
    }

    public class ExternalExcelRangeFormatInfo : ExternalExcelRangeInfo
    {
        private ExternalExcelCellHAlign _horizontalAlignment = ExternalExcelCellHAlign.AlignLeft;
        private ExternalExcelCellVAlign _verticalAlignment = ExternalExcelCellVAlign.AlignTop;

        public ExternalExcelRangeFormatInfo(int startRow, int startColumn)
            : base(startRow, startColumn, startRow, startColumn) { }

        public ExternalExcelRangeFormatInfo(int startRow, int startColumn, int endRow, int endColumn)
            : base(startRow, startColumn, endRow, endColumn) { }

        public int ColumnIndex { get; set; }
        public string ColumName { get; set; }

        public Color BackColor { get; set; }
        public Color ForeColor { get; set; }
        public Color BorderColor { get; set; }

        public Font Font { get; set; }
        public bool WrapText { get; set; }
        public bool Merge { get; set; }

        public bool AutoFitRows { get; set; }
        public bool AutoFitColumns { get; set; }

        public double RowHeight { get; set; }
        public double ColumnWidth { get; set; }

        public string Text { get; set; }
        public string ListValues { get; set; }

        public ExternalExcelValidationType ValidationType { get; set; }
        public string[] ValidationFormat { get; set; }
        public string CustomFormat { get; set; }

        public bool AutoFilter { get; set; }

        public ExternalExcelCellHAlign HorizontalAlignment
        {
            get { return _horizontalAlignment; }
            set { _horizontalAlignment = value; }
        }

        public ExternalExcelCellVAlign VerticalAlignment
        {
            get { return _verticalAlignment; }
            set { _verticalAlignment = value; }
        }
    }
}
