using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace OfficeOpenXml
{
    /// <summary>
    /// A class that can be used to represent the dimenension of a <see cref="OfficeOpenXmlExcelWorkSheet"/>
    /// </summary>
    public class ExcelWorkSheetDimension
    {
        const string worksheetSchema = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";

        private string topLeft;
        private string bottomRight;
        private int firstRow;
        private int firstCol;
        private int lastRow;
        private int lastCol;

        private static String handleDimensionFromExcelWorksheet(ExcelWorksheet w)
        {
            if (w == null)
                throw new ArgumentNullException("w");

            XmlNode n = w.WorksheetXml.DocumentElement.SelectSingleNode("//d:dimension", w.NameSpaceManager);
            XmlAttribute dimensionRef = n.Attributes["ref"];
            if ((dimensionRef != null) && (!String.IsNullOrEmpty(dimensionRef.Value)))
                return dimensionRef.Value;

            // no dimension available...?
            throw new XmlException("dimension attribute not found!");
        }

        /// <summary>
        /// Creates a ExcelWorkSheetDimension
        /// </summary>
        /// <param name="w">The <see cref="OfficeOpenXmlExcelWorkSheet"/> to create the dimension object for</param>
        public ExcelWorkSheetDimension(ExcelWorksheet w)
            : this(handleDimensionFromExcelWorksheet(w))
        {
        }

        /// <summary>
        /// Creates a ExcelWorkSheetDimension using a string with Cell Range representation like 'A1:B5'.
        /// </summary>
        /// <param name="dimension">a string with Cell Range representation like 'A1:B5'</param>
        public ExcelWorkSheetDimension(String dimension)
        {
            String[] dimensions = dimension.Split(':');
            this.topLeft = dimensions[0];
            this.bottomRight = dimensions[1];
            if (!ExcelCell.IsValidCellAddress(topLeft) || (!ExcelCell.IsValidCellAddress(BottomRight)))
                throw new ArgumentException("No valid excel sheet dimension!");
            firstRow = ExcelCell.GetRowNumber(topLeft);
            firstCol = ExcelCell.GetColumnNumber(topLeft);
            lastCol = ExcelCell.GetColumnNumber(bottomRight);
            lastRow = ExcelCell.GetRowNumber(bottomRight);
        }

        /// <summary>
        /// Creates a ExcelWorkSheetDimension using a Excel two cell representations.
        /// </summary>
        /// <param name="topLeft">a top left cell, like 'A1'</param>
        /// <param name="rightBottom">a right bottom cell, like 'B5'</param>
        public ExcelWorkSheetDimension(String topLeft, String rightBottom)
            :
            this(String.Format("{0}:{1}", topLeft, rightBottom))
        {
        }

        public string TopLeft { get { return topLeft; } }
        public string BottomRight { get { return bottomRight; } }
        public int FirstCol { get { return firstCol; } }
        public int FirstRow { get { return firstRow; } }
        public int LastCol { get { return lastCol; } }
        public int LastRow { get { return lastRow; } }
    }
}
