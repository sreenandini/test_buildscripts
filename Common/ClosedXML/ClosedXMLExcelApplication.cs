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

namespace ClosedXML.Excel
{
    [Export(typeof(IExternalExcelApplicationLoader))]
    internal class CloseXMLExcelApplicationLoader : ExternalExcelApplicationLoader, IExternalExcelApplicationLoader
    {
        private bool _isLoaded = false;

        public CloseXMLExcelApplicationLoader()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Initialize");

            try
            {
                _isLoaded = true;
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public override int LibraryOrder
        {
            get { return 1; }
        }

        public override bool IsLoadedProperly
        {
            get { return _isLoaded; }
        }

        public override string LibraryName
        {
            get { return "ClosedXML"; }
        }

        protected override IExternalExcelApplication CreateInternal()
        {
            return new CloseXMLExcelApplication();
        }
    }

    internal class CloseXMLExcelApplication : ExternalExcelApplication
    {
        private XLWorkbook _obj = null;

        public CloseXMLExcelApplication() { }

        protected override bool OpenInternal(string fileName, bool write)
        {
            if (_obj != null) return false;

            if (!_write && File.Exists(fileName))
            {
                _obj = new XLWorkbook(fileName);
            }
            else
            {
                _obj = new XLWorkbook();
            }
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
                _obj.Dispose();
                _obj = null;
                return true;
            }
            return false;
        }

        protected override IExternalExcelSheet AddOrGetInternal(string sheetName)
        {
            if (_obj == null) return null;

            XLWorksheet ws = null;
            if (!_write)
            {
                foreach (var ws1 in _obj.Worksheets.OfType<XLWorksheet>())
                {
                    try
                    {
                        if (ws1.Name.IgnoreCaseCompare(sheetName))
                        {
                            ws = ws1;
                            break;
                        }
                    }
                    catch { }
                }
            }

            if (ws == null)
            {
                ws = _obj.Worksheets.Add(sheetName) as XLWorksheet;
            }

            return new CloseXMLExcelSheet(ws);
        }

        public override void RemoveSheetsExcept(string sheetName)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "RemoveSheetsExcept");

            try
            {
                foreach (var ws in _obj.Worksheets.OfType<XLWorksheet>())
                {
                    if (!ws.Name.IgnoreCaseCompare(sheetName))
                    {
                        ws.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }
    }

    internal class CloseXMLExcelSheet : ExternalExcelSheet
    {
        private XLWorksheet _obj = null;

        internal CloseXMLExcelSheet(XLWorksheet obj)
        {
            _obj = obj;
        }

        protected override IExternalExcelCell CellInternal(int row, int col)
        {
            return new CloseXMLExcelCell(_obj.Cell(row, col));
        }

        protected override IExternalExcelColumn ColumnInternal(int col)
        {
            return new CloseXMLExcelColumn(_obj.Column(col));
        }

        private XLRange GetRange(ExternalExcelRangeInfo rangeInfo)
        {
            if (!rangeInfo.IsValidRange) return null;

            CloseXMLExcelCell startCell =  this.Cell(rangeInfo.StartRow, rangeInfo.StartColumn) as CloseXMLExcelCell;
            CloseXMLExcelCell endCell = this.Cell(rangeInfo.EndRow, rangeInfo.EndColumn) as CloseXMLExcelCell;
            return _obj.Range(startCell.Cell, endCell.Cell);
        }

        protected override object[,] ReadInternal(IAsyncProgress2 o, ExternalExcelRangeInfo rangeInfo)
        {
            var readRange = this.GetRange(rangeInfo);
            return readRange.Value2 as object[,];
        }

        protected override bool WriteInternal(IAsyncProgress2 o, object[,] values, ExternalExcelRangeInfo rangeInfo)
        {
            var writeRange = this.GetRange(rangeInfo);
            writeRange.Value2 = values;
            return true;
        }

        public override void AutoResizeRows(ExternalExcelRangeInfo rangeInfo)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Method");

            try
            {
                if (rangeInfo.IsEmpty)
                {
                    _obj.Rows().AdjustToContents();
                }
                else
                {
                    var range = this.GetRange(rangeInfo);
                    //range.Rows.AutoFit();
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
                    _obj.Columns().AdjustToContents();
                }
                else
                {
                    var range = this.GetRange(rangeInfo);
                    //range.AsRange().AddConditionalFormat
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

            try
            {
                XLRange range = this.GetRange(formatInfo);
                if (range == null) return;

                // colors
                if (formatInfo.ForeColor != Color.Empty) range.Style.Font.FontColor = XLColor.FromColor(formatInfo.ForeColor);
                if (formatInfo.BackColor != Color.Empty) range.Style.Fill.BackgroundColor = XLColor.FromColor(formatInfo.BackColor);
                if (formatInfo.BorderColor != Color.Empty) range.Style.Border.OutsideBorderColor = XLColor.FromColor(formatInfo.BorderColor);

                // font
                if (formatInfo.Font != null)
                {
                    range.Style.Font.FontName = formatInfo.Font.Name;
                    range.Style.Font.Bold = formatInfo.Font.Bold;
                    range.Style.Font.FontSize = formatInfo.Font.Size;
                }

                // row height and column width
                //if (formatInfo.AutoFitRows) range.Rows().AutoFit();
                //if (formatInfo.AutoFitColumns) range.Columns.AutoFit();
                if (formatInfo.RowHeight > 0) range.Worksheet.RowHeight = formatInfo.RowHeight;
                if (formatInfo.ColumnWidth > 0 && formatInfo.ColumnIndex>0)
                {
                    range.Column(formatInfo.ColumnIndex).WorksheetColumn().Width = formatInfo.ColumnWidth;
                }

                // text
                range.Style.Alignment.WrapText = formatInfo.WrapText;
                if (!formatInfo.Text.IsEmpty())
                {
                    range.Value = formatInfo.Text;
                }

                // merge
                if (formatInfo.Merge) range.Merge();

                // horizontal and vertical alignment
                switch (formatInfo.HorizontalAlignment)
                {
                    case ExternalExcelCellHAlign.AlignRight:
                        range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                        break;
                    case ExternalExcelCellHAlign.AlignLeft:
                        range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        break;
                    case ExternalExcelCellHAlign.AlignJustify:
                        range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                        break;
                    case ExternalExcelCellHAlign.AlignDistributed:
                        range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Distributed;
                        break;
                    case ExternalExcelCellHAlign.AlignCenter:
                        range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        break;
                    case ExternalExcelCellHAlign.AlignGeneral:
                        range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.General;
                        break;
                    case ExternalExcelCellHAlign.AlignFill:
                        range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Fill;
                        break;
                    case ExternalExcelCellHAlign.AlignCenterAcrossSelection:
                        range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        break;
                    default:
                        break;
                }
                switch (formatInfo.VerticalAlignment)
                {
                    case ExternalExcelCellVAlign.AlignTop:
                        range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                        break;
                    case ExternalExcelCellVAlign.AlignJustify:
                        range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Justify;
                        break;
                    case ExternalExcelCellVAlign.AlignDistributed:
                        range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Distributed;
                        break;
                    case ExternalExcelCellVAlign.AlignCenter:
                        range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        break;
                    case ExternalExcelCellVAlign.AlignBottom:
                        range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Bottom;
                        break;
                    default:
                        break;
                }

                // auto filter
                if (formatInfo.AutoFilter)
                {
                    range.SetAutoFilter();
                }

                // custom format
                if (!formatInfo.CustomFormat.IsEmpty())
                {
                    range.Style.NumberFormat.Format = formatInfo.CustomFormat;
                }

                if (formatInfo.ValidationType != ExternalExcelValidationType.ValidationNone)
                {
                    var validation = range.DataValidation;
                    validation.Clear();

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
                                validation.Decimal.Between(Convert.ToDouble(formula1), Convert.ToDouble(formula2));
                                validation.ErrorMessage = string.Format("Please enter the value between {0} and {1}", formula1.ToStringSafe(), formula2.ToStringSafe());
                            }
                            break;

                        case ExternalExcelValidationType.ValidateList:
                            //validations.List(formatInfo.ValidationFormat);
                            break;

                        case ExternalExcelValidationType.ValidateDate:
                            {
                                validation.Date.Between(Convert.ToDateTime(formula1), Convert.ToDateTime(formula2));
                                validation.ErrorMessage = string.Format("Please enter the date between {0} and {1}", formula1.ToStringSafe(), formula2.ToStringSafe());
                            }
                            break;

                        case ExternalExcelValidationType.ValidateTime:
                            break;

                        case ExternalExcelValidationType.ValidateTextLength:
                            {
                                validation.TextLength.EqualOrLessThan(Convert.ToInt32(formula1));
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
        }
    }

    internal class CloseXMLExcelColumn : DisposableObject, IExternalExcelColumn
    {
        private XLColumn _obj = null;

        internal CloseXMLExcelColumn(XLColumn obj)
        {
            _obj = obj;
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

    internal class CloseXMLExcelCell : DisposableObject, IExternalExcelCell
    {
        private XLRange _obj = null;
        private XLCell _cell = null;

        internal CloseXMLExcelCell(XLRange obj)
        {
            _obj = obj;
        }

        internal CloseXMLExcelCell(XLCell obj)
        {
            _cell = obj;
        }

        public XLRange CellRange
        {
            get
            {
                return _obj;
            }
        }

        public XLCell Cell
        {
            get
            {
                return _cell;
            }
        }

        public object Value
        {
            get
            {
                if (_cell != null) return _cell.Value;
                return null;
            }
            set
            {
                if (_cell != null) _cell.Value = value;
                else if (_obj != null) _obj.Value = value;
            }
        }
    }
}
