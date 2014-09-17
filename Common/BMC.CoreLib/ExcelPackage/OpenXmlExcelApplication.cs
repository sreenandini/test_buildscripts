using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.Win32;
using OfficeOpenXml;

namespace BMC.CoreLib.ExcelPackage
{
    [Export(typeof(IExternalExcelApplicationLoader))]
    internal class OpenXmlExcelApplicationLoader : ExternalExcelApplicationLoader, IExternalExcelApplicationLoader
    {
        public override int LibraryOrder
        {
            get { return 99; }
        }

        public override bool IsLoadedProperly
        {
            get { return true; }
        }

        public override string LibraryName
        {
            get { return "ExcelPackage"; }
        }

        protected override IExternalExcelApplication CreateInternal()
        {
            return new OpenXmlExcelApplication();
        }
    }

    internal class OpenXmlExcelApplication : ExternalExcelApplication
    {
        private OfficeOpenXml.ExcelPackage _obj = null;

        protected override bool OpenInternal(string fileName, bool write)
        {
            if (_obj != null) return false;

            if (write &&
                File.Exists(fileName))
            {
                try { File.Delete(fileName); }
                catch { }
            }
            _obj = new OfficeOpenXml.ExcelPackage(new FileInfo(fileName));
            return true;
        }

        protected override bool SaveInternal()
        {
            if (_obj == null) return false;

            _obj.Save();
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

            ExcelWorksheet ws = _obj.Workbook.Worksheets[sheetName];
            if (ws == null)
            {
                ws = _obj.Workbook.Worksheets.Add(sheetName);
            }
            return new OpenXmlExcelSheet(ws);
        }

        public override void RemoveSheetsExcept(string sheetName) { }
    }

    internal class OpenXmlExcelSheet : ExternalExcelSheet
    {
        private ExcelWorksheet _obj = null;

        internal OpenXmlExcelSheet(ExcelWorksheet obj)
        {
            _obj = obj;
        }

        protected override IExternalExcelCell CellInternal(int row, int col)
        {
            return new OpenXmlExcelCell(_obj.Cell(row, col));
        }

        protected override IExternalExcelColumn ColumnInternal(int col)
        {
            return new OpenXmlExcelColumn(_obj.Column(col));
        }

        protected override object[,] ReadInternal(IAsyncProgress2 o, ExternalExcelRangeInfo rangeInfo)
        {
            return this._ReadInternal(o, rangeInfo);
        }

        protected override bool WriteInternal(IAsyncProgress2 o, object[,] values, ExternalExcelRangeInfo rangeInfo)
        {
            return this._WriteInternal(o, values, rangeInfo);
        }

        public override void AutoResizeRows(ExternalExcelRangeInfo rangeInfo) { }

        public override void AutoResizeColumns(ExternalExcelRangeInfo rangeInfo) { }

        public override void FormatRange(ExternalExcelRangeFormatInfo formatInfo) { }
    }

    internal class OpenXmlExcelColumn : DisposableObject, IExternalExcelColumn
    {
        private ExcelColumn _obj = null;

        internal OpenXmlExcelColumn(ExcelColumn obj)
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
                _obj.Width = value;
            }
        }
    }

    internal class OpenXmlExcelCell : DisposableObject, IExternalExcelCell
    {
        private ExcelCell _obj = null;

        internal OpenXmlExcelCell(ExcelCell obj)
        {
            _obj = obj;
        }

        public object Value
        {
            get
            {
                return _obj.Value;
            }
            set
            {
                _obj.Value = value.ToString();
            }
        }
    }
}
