﻿using System;
using System.Drawing;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Reflection;

namespace BMCMeterAnalysis
{
    /// <summary>
    /// ExportStyle specifies Column wise,Row wise or Sheet wise
    /// </summary>
    public enum ExportStyle
    {
        RowWise = 0,
        ColumnWise,
        SheetWise
    }

    /// <summary>
    /// Specifies the formatting details for the excel
    /// </summary>
    sealed public class ExcelStyle
    {
        public Color HeaderBackColor = Color.LightGray;
        public Color HeaderForeColor = Color.Black;
        public Color ItemForeColor = Color.Black;
        public Color ItemBackColor = Color.White;
        public Color ItemAlternateBackColor = Color.White;
        public string FontName = "Verdana";
        public bool ItemFontBold = false;
        public bool HeaderFontBold = true;
        public bool ItemItalic = false;
        public bool HeaderItalic = false;
        public ushort FontSize = 9;
        //Excel column-row settings
        public int ColumnSpace = 1;
        public int RowSpace = 1;
        public int ColumnSpaceBetweenTables = 2;
        public int RowSpaceBetweenTables = 2;
        public bool RepeatColumnHeader = true;
    }
    public class DataExportUtility
    {

        public delegate void ExportProgressDelegete(int iCurrentItem, int iTotalItems);
        /// <summary>
        /// Export progress indicator event.
        /// </summary>
        public event ExportProgressDelegete ExportRowProgress;
        SortedList _listrow = null;
        ExcelStyle excelStyle;

        /// <summary>
        /// Specifies the formatting style for the excel like color,font etc.
        /// </summary>
        public ExcelStyle ExcelFormattingStyle
        {
            set
            {
                excelStyle = value;
            }
            get
            {
                return excelStyle;
            }
        }

        public DataExportUtility()
        {
            excelStyle = new ExcelStyle();
        }

        /// <summary>
        /// Exports the DataSet to the excel and opens the excel.
        /// </summary>
        /// <param name="dsData">Dataset to be exported</param>
        /// <param name="style">Style specifies Column wise,Row wise or Sheet wise</param>
        public void ExportDataToExcel(DataSet dsData, ExportStyle style, string strFileName)
        {
            Excel.ApplicationClass excel = null;
            //Excel.Workbooks workbooks = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet ws = null;
            try
            {
                excel = new Excel.ApplicationClass();
                workbook = (Excel.Workbook)excel.Workbooks.Add(Missing.Value);
                ws = (Excel.Worksheet)workbook.Worksheets["Sheet1"];


                DataTable table = dsData.Tables[0];
                int ColumnIndex = 0;
                foreach (DataColumn col in table.Columns)
                {
                    ColumnIndex++;
                    ws.Cells[1, ColumnIndex] = col.ColumnName;
                }
                int rowIndex = 0;
                foreach (DataRow row in table.Rows)
                {
                    rowIndex++;
                    ColumnIndex = 0;
                    foreach (DataColumn col in table.Columns)
                    {
                        ColumnIndex++;
                        ws.Cells[rowIndex + 1, ColumnIndex] = row[col.ColumnName].ToString();
                    }
                }


                try
                {
                    workbook.SaveAs(strFileName, Excel.XlFileFormat.xlWorkbookNormal, null, null, false, false, Excel.XlSaveAsAccessMode.xlExclusive, false, false, null, null, null);
                }
                catch { }

                //Excel.Worksheet worksheet = (Excel.Worksheet )excel.ActiveSheet;
                //worksheet.SaveAs(strFileName,Excel.XlFileFormat.xlWorkbookNormal, null, null, false, false, Excel.XlSaveAsAccessMode.xlShared, false, false, null);

                //_listrow = new SortedList();
                //excel =  new Excel.ApplicationClass();
                //workbooks = excel.Workbooks;
                //workbook = workbooks.Add(true);

                //ExportCurrentData(excel,dsData,style);
                //_listrow = null;
                //Excel.Worksheet worksheet = (Excel.Worksheet)excel.ActiveSheet; 
                ////worksheet.a();
                //excel.Visible = true;
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                if (workbook != null) Marshal.ReleaseComObject(workbook);
                if (excel != null) Marshal.ReleaseComObject(excel);
                workbook = null;
                excel = null;
            }
        }


        private void ExportCurrentData(Excel.ApplicationClass excel, DataSet dsData, ExportStyle style)
        {
            int rowIndex = 1;
            int colIndex = 0;
            int startCol = 0;
            int i = 0;
            int eventRow = 1;
            foreach (DataTable dtb in dsData.Tables)
            {
                startCol = colIndex;
                AddDataTableToExcel(excel, dtb, style, ref rowIndex, ref colIndex);
                if (ExportRowProgress != null)
                    ExportRowProgress(eventRow++, dsData.Tables.Count);
                switch (style)
                {
                    case ExportStyle.RowWise:
                        {
                            colIndex = 0;
                            rowIndex += excelStyle.RowSpaceBetweenTables + 1;
                            break;
                        }
                    case ExportStyle.ColumnWise:
                        {
                            rowIndex = 1;
                            colIndex += excelStyle.ColumnSpaceBetweenTables;
                            break;
                        }
                    case ExportStyle.SheetWise:
                        {
                            if (i != dsData.Tables.Count - 1)
                            {
                                excel.Worksheets.Add(
                                    Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                            }
                            colIndex = 0;
                            rowIndex = 1;
                            break;
                        }
                }
                i++;
            }//For
        }

        private void AddDataTableToExcel(Excel.ApplicationClass excel, System.Data.DataTable table, ExportStyle style, ref int rowIndex, ref int columnIndex)
        {
            int colstart = columnIndex;
            int colbak = colstart;
            if (style == ExportStyle.SheetWise)
            {
                Excel.Worksheet worksheet = (Excel.Worksheet)excel.ActiveSheet;
                worksheet.Name = table.TableName;
            }

            foreach (DataColumn col in table.Columns)
            {
                columnIndex += excelStyle.ColumnSpace;
                Excel.Range cel = (Excel.Range)excel.Cells[rowIndex, columnIndex];
                cel.Font.Bold = true;
                cel.Interior.Color = System.Drawing.ColorTranslator.ToOle(excelStyle.HeaderBackColor);
                cel.Font.Color = System.Drawing.ColorTranslator.ToOle(excelStyle.HeaderForeColor);
                cel.Font.Name = excelStyle.FontName;
                cel.Font.Size = excelStyle.FontSize;
                cel.Font.Italic = excelStyle.HeaderItalic;
                cel.Font.Bold = excelStyle.HeaderFontBold;
                excel.Cells[rowIndex, columnIndex] = col.ColumnName;
            }

            foreach (DataRow row in table.Rows)
            {
                rowIndex += excelStyle.RowSpace;
                foreach (DataColumn col in table.Columns)
                {
                    colstart += excelStyle.ColumnSpace;

                    Excel.Range cel = (Excel.Range)excel.Cells[rowIndex, colstart];

                    if (rowIndex != 0 && rowIndex % 2 == 0)
                    {
                        if (excelStyle.ItemAlternateBackColor != Color.White)
                            cel.Interior.Color = System.Drawing.ColorTranslator.ToOle(excelStyle.ItemAlternateBackColor);
                    }
                    else
                    {
                        if (excelStyle.ItemBackColor != Color.White)
                            cel.Interior.Color = System.Drawing.ColorTranslator.ToOle(excelStyle.ItemBackColor);
                    }

                    cel.Font.Color = System.Drawing.ColorTranslator.ToOle(excelStyle.ItemForeColor);

                    cel.Font.Name = excelStyle.FontName;
                    cel.Font.Size = excelStyle.FontSize;
                    cel.Font.Italic = excelStyle.ItemItalic;
                    cel.Font.Bold = excelStyle.ItemFontBold;
                    excel.Cells[rowIndex, colstart] = row[col.ColumnName].ToString();
                }
                colstart = colbak;

            }
            if (!_listrow.ContainsKey(table.Rows.Count))
            {
                _listrow.Add(table.Rows.Count, rowIndex);
            }
        }



    }
}
