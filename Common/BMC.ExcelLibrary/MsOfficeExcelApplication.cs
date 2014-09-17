using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.ExcelPackage;
using BMC.CoreLib.Win32;
using Microsoft.Office.Interop.Excel;

namespace BMC.ExcelLibrary
{
    [Export(typeof(IExternalExcelApplicationLoader))]
    internal class MsOfficeExcelApplicationLoader : ExternalExcelApplicationLoader, IExternalExcelApplicationLoader
    {
        private bool _isLoaded = false;
        private string _version = string.Empty;

        public MsOfficeExcelApplicationLoader()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Initialize");
            Application app = null;

            try
            {
                app = new Application();
                _version = app.Version;
                _isLoaded = true;
            }
            catch (Exception)
            {
                Log.Info(PROC, "Microsoft Office is not installed.");
            }
            finally
            {
                if (app != null)
                {
                    app.Quit();
                    Extensions.DisposeComObject(app);
                }
            }
        }

        public override int LibraryOrder
        {
            get { return 0; }
        }

        public override bool IsLoadedProperly
        {
            get { return _isLoaded; }
        }

        public override string LibraryName
        {
            get { return "Microsoft Office " + _version; }
        }

        protected override IExternalExcelApplication CreateInternal()
        {
            return new MsOfficeExcelApplication();
        }
    }

    internal class MsOfficeExcelApplication : ExternalExcelApplication
    {
        private Application _app = null;
        private Workbooks _workbooks = null;
        private Sheets _worksheets = null;
        private Workbook _obj = null;

        public MsOfficeExcelApplication() { }

        protected override bool OpenInternal(string fileName, bool write)
        {
            if (_obj != null) return false;

            _app = new Application();
            _app.Visible = false;
            _app.DisplayAlerts = false;
            _app.ScreenUpdating = false;
            _workbooks = _app.Workbooks;

            if (!_write && File.Exists(fileName))
            {
                _obj = _workbooks.Open(fileName);
            }
            else
            {
                _obj = _workbooks.Add();
            }
            _worksheets = _obj.Worksheets;

            return true;
        }

        protected override bool SaveInternal()
        {
            if (_obj == null) return false;

            _obj.SaveAs(_fileName);
            return true;
        }

        protected override bool CloseInternal()
        {
            if (_obj != null)
            {
                _obj.Close();
                _workbooks.Close();
                _app.Quit();
                Extensions.DisposeComObject(_worksheets);
                Extensions.DisposeComObject(_obj);
                Extensions.DisposeComObject(_workbooks);
                Extensions.DisposeComObject(_app);
                _obj = null;
                _app = null;
                return true;
            }
            return false;
        }

        protected override IExternalExcelSheet AddOrGetInternal(string sheetName)
        {
            if (_obj == null) return null;

            Worksheet ws = null;
            bool exists = false;
            if (!_write)
            {
                foreach (var ws1 in _worksheets.OfType<Worksheet>())
                {
                    try
                    {
                        if (ws1.Name.IgnoreCaseCompare(sheetName))
                        {
                            ws = ws1;
                            exists = true;
                            break;
                        }
                    }
                    catch { }
                    finally
                    {
                        if (!exists)
                        {
                            Extensions.DisposeComObject(ws);
                        }
                    }
                }
            }

            if (ws == null)
            {
                ws = _worksheets.Add();
                ws.Name = sheetName;
            }

            Range rows = ws.Rows;
            try
            {
                rows.Locked = false;
            }
            finally { Extensions.DisposeComObject(rows); }
            object missing = Type.Missing;
            ws.Protect(missing, missing, false, missing, missing, missing, missing, missing, missing,
                missing, missing, missing, missing, missing, missing, missing);

            return new MsOfficeExcelSheet(ws);
        }

        public override void RemoveSheetsExcept(string sheetName)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "RemoveSheetsExcept");

            try
            {
                foreach (var ws in _worksheets.OfType<Worksheet>())
                {
                    bool exists = false;
                    try
                    {
                        if (!ws.Name.IgnoreCaseCompare(sheetName))
                        {
                            ws.Delete();
                        }
                        else
                        {
                            exists = true;
                        }
                    }
                    finally
                    {
                        if (!exists)
                        {
                            Extensions.DisposeComObject(ws);
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

    internal class MsOfficeExcelSheet : ExternalExcelSheet
    {
        private Worksheet _obj = null;
        private Range _cells = null;
        private Range _columns = null;

        internal MsOfficeExcelSheet(Worksheet obj)
        {
            _obj = obj;
            _cells = _obj.Cells;
            _columns = _obj.Columns;
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            Extensions.DisposeComObject(_cells);
            Extensions.DisposeComObject(_columns);
            Extensions.DisposeComObject(_obj);
        }

        protected override IExternalExcelCell CellInternal(int row, int col)
        {
            return new MsOfficeExcelCell(_cells[row, col]);
        }

        protected override IExternalExcelColumn ColumnInternal(int col)
        {
            return new MsOfficeExcelColumn(_columns[col]);
        }

        private Range GetRange(ExternalExcelRangeInfo rangeInfo)
        {
            if (!rangeInfo.IsValidRange) return null;
            MsOfficeExcelCell startCell = null;
            MsOfficeExcelCell endCell = null;

            try
            {
                startCell = this.CellInternal(rangeInfo.StartRow, rangeInfo.StartColumn) as MsOfficeExcelCell;
                endCell = this.CellInternal(rangeInfo.EndRow, rangeInfo.EndColumn) as MsOfficeExcelCell;
                return _obj.Range[startCell.CellRange, endCell.CellRange];
            }
            finally
            {
                startCell.Dispose();
                endCell.Dispose();
            }
            
        }

        protected override object[,] ReadInternal(IAsyncProgress2 o, ExternalExcelRangeInfo rangeInfo)
        {
            var readRange = this.GetRange(rangeInfo);
            return readRange.Value2 as object[,];
        }

        protected override bool WriteInternal(CoreLib.Win32.IAsyncProgress2 o, object[,] values, ExternalExcelRangeInfo rangeInfo)
        {
            Range writeRange = null;

            try
            {
                writeRange = this.GetRange(rangeInfo);
                writeRange.Value2 = values;
                return true;
            }
            finally
            {
                Extensions.DisposeComObject(writeRange);
            }
        }

        public override void AutoResizeRows(ExternalExcelRangeInfo rangeInfo)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Method");

            try
            {
                if (rangeInfo.IsEmpty)
                {
                    _obj.Rows.AutoFit();
                }
                else
                {
                    var range = this.GetRange(rangeInfo);
                    range.Rows.AutoFit();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public override void AutoResizeColumns(ExternalExcelRangeInfo rangeInfo)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Method");

            try
            {
                if (rangeInfo.IsEmpty)
                {
                    _obj.Columns.AutoFit();
                }
                else
                {
                    var range = this.GetRange(rangeInfo);
                    range.Columns.AutoFit();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public override void FormatRange(ExternalExcelRangeFormatInfo formatInfo)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Method");
            Range range = null;
            Microsoft.Office.Interop.Excel.Font font = null;
            Borders borders = null;
            Interior interior = null;
            Range rows = null;
            Range columns = null;
            Range entireRow = null;
            Range entireColumn = null;
            Validation validation = null;

            try
            {
                range = this.GetRange(formatInfo);
                if (range == null) return;

                // colors
                font = range.Font;
                borders = range.Borders;
                interior = range.Interior;
                if (formatInfo.ForeColor != Color.Empty) font.ColorIndex = ExternalExcelColorMappings.GetColor(formatInfo.ForeColor.ToArgb());
                if (formatInfo.BackColor != Color.Empty) interior.ColorIndex = ExternalExcelColorMappings.GetColor(formatInfo.BackColor.ToArgb());
                if (formatInfo.BorderColor != Color.Empty) borders.ColorIndex = ExternalExcelColorMappings.GetColor(formatInfo.BorderColor.ToArgb());

                // font
                if (formatInfo.Font != null)
                {
                    font.Name = formatInfo.Font.Name;
                    font.Bold = formatInfo.Font.Bold;
                    font.Size = formatInfo.Font.Size;
                }

                // row height and column width
                if (formatInfo.AutoFitRows)
                {
                    rows = range.Rows;
                    rows.AutoFit();
                }
                if (formatInfo.AutoFitColumns)
                {
                    columns = range.Columns;
                    columns.AutoFit();
                }
                if (formatInfo.RowHeight > 0)
                {
                    entireRow = range.EntireRow;
                    entireRow.RowHeight = formatInfo.RowHeight;
                }
                if (formatInfo.ColumnWidth > 0)
                {
                    entireColumn = range.EntireColumn;
                    entireColumn.ColumnWidth = formatInfo.ColumnWidth;
                }

                // text
                range.WrapText = formatInfo.WrapText;
                if (!formatInfo.Text.IsEmpty())
                {
                    range.Value2 = formatInfo.Text;
                }

                // merge
                if (formatInfo.Merge) range.Merge(0);

                // horizontal and vertical alignment
                range.HorizontalAlignment = (XlHAlign)formatInfo.HorizontalAlignment;
                range.VerticalAlignment = (XlVAlign)formatInfo.VerticalAlignment;

                // auto filter
                if (formatInfo.AutoFilter)
                {
                    range.AutoFilter(formatInfo.StartColumn);
                }

                // custom format
                if (!formatInfo.CustomFormat.IsEmpty())
                {
                    range.NumberFormat = formatInfo.CustomFormat;
                }

                if (formatInfo.ValidationType != ExternalExcelValidationType.ValidationNone)
                {
                    validation = range.Validation;
                    validation.Delete();

                    object formula1 = Type.Missing;
                    object formula2 = Type.Missing;
                    if (formatInfo.ValidationFormat.Length > 0) formula1 = formatInfo.ValidationFormat[0];
                    if (formatInfo.ValidationFormat.Length > 1) formula2 = formatInfo.ValidationFormat[1];

                    switch (formatInfo.ValidationType)
                    {
                        case ExternalExcelValidationType.ValidateInputOnly:
                            break;

                        case ExternalExcelValidationType.ValidateWholeNumber:
                            break;

                        case ExternalExcelValidationType.ValidateDecimal:
                            {
                                validation.Add(XlDVType.xlValidateDecimal, XlDVAlertStyle.xlValidAlertStop,
                                    XlFormatConditionOperator.xlBetween, formula1, formula2);
                                validation.ErrorMessage = string.Format("Please enter the value between {0} and {1}", formula1.ToStringSafe(), formula2.ToStringSafe());
                            }
                            break;

                        case ExternalExcelValidationType.ValidateList:
                            {
                                validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertStop,
                                    XlFormatConditionOperator.xlBetween, formula1);
                                validation.ErrorMessage = string.Format("Please enter the valid range of values.");
                            }
                            break;

                        case ExternalExcelValidationType.ValidateDate:
                            {
                                validation.Add(XlDVType.xlValidateDate, XlDVAlertStyle.xlValidAlertStop,
                                XlFormatConditionOperator.xlBetween, formula1, formula2);
                                validation.ErrorMessage = string.Format("Please enter the date between {0} and {1}", formula1.ToStringSafe(), formula2.ToStringSafe());
                            }
                            break;

                        case ExternalExcelValidationType.ValidateTime:
                            break;

                        case ExternalExcelValidationType.ValidateTextLength:
                            {
                                validation.Add(XlDVType.xlValidateTextLength, XlDVAlertStyle.xlValidAlertStop,
                                    XlFormatConditionOperator.xlLessEqual, formula1);
                                validation.ErrorMessage = string.Format("Please enter the value with max length of {0:D}", formula1.ToStringSafe());
                            }
                            break;

                        case ExternalExcelValidationType.ValidateCustom:
                            break;
                        default:
                            break;
                    }

                    validation.ErrorTitle = formatInfo.ColumName;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                Extensions.DisposeComObject(validation);
                Extensions.DisposeComObject(rows);
                Extensions.DisposeComObject(columns);
                Extensions.DisposeComObject(entireRow);
                Extensions.DisposeComObject(entireColumn);
                Extensions.DisposeComObject(borders);
                Extensions.DisposeComObject(interior);
                Extensions.DisposeComObject(font);
                Extensions.DisposeComObject(range);
            }
        }
    }

    internal class MsOfficeExcelColumn : DisposableObject, IExternalExcelColumn
    {
        private Range _obj = null;

        internal MsOfficeExcelColumn(Range obj)
        {
            _obj = obj;
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            Extensions.DisposeComObject(_obj);
        }

        public double Width
        {
            get
            {
                return _obj.Width;
            }
            set
            {

            }
        }
    }

    internal class MsOfficeExcelCell : DisposableObject, IExternalExcelCell
    {
        private Range _obj = null;

        internal MsOfficeExcelCell(Range obj)
        {
            _obj = obj;
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            Extensions.DisposeComObject(_obj);
        }

        public Range CellRange
        {
            get
            {
                return _obj;
            }
        }

        public object Value
        {
            get
            {
                return _obj.Value;
            }
            set
            {
                _obj.Value = value;
            }
        }
    }
}
