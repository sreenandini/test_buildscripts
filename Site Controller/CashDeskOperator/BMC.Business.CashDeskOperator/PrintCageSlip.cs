//#define SP5_SLIP_FORMAT

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BMC.Common.Utilities;
using BMC.DBInterface.CashDeskOperator;
using BMC.Transport;
using System.Drawing.Printing;
using System.Drawing;
using System.Diagnostics;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Reflection;
using BMC.Common.ExceptionManagement;
using System.Xml;
using BMC.Common.LogManagement;
using System.Windows.Forms;
using System.Drawing.Text;
using Audit.Transport;
using BMC.Transport.CashDeskOperatorEntity;
using Audit.BusinessClasses;

namespace BMC.Business.CashDeskOperator
{
    public class PrintCageSlip
    {
        string dynTableHeader;
        System.IO.StreamReader fileToPrint;
        System.Drawing.Font printFont;
        PrintDocument printDocument1;
        public int InstallationNumber;
        private string filename = "";
        private DateTime StartDate = DateTime.Now;
        private DateTime EndDate = DateTime.Now;
        private StreamWriter writer = null;        
        string filepath;
        FileStream outputfile = null;

        ValuetoWords objWords = new ValuetoWords();

#if SP5_SLIP_FORMAT
        public void PrintSlip(jackpotProcessInfoDTO jackpot)
        {
            filename = "Print.txt";
            filepath = System.Windows.Forms.Application.StartupPath + "\\" + filename;
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            outputfile = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
            writer = new StreamWriter(outputfile);
            writer.BaseStream.Seek(0, SeekOrigin.End);

            string Type = string.Empty;
            switch (jackpot.jackpotTypeId)
            {
                case 1:
                    Type = "AttendantPay Credit";
                    break;
                case 2:
                    Type = "AttendantPay Jackpot";
                    break;
                case 3:
                    Type = "Progressive";
                    break;
                case 4:
                    Type = "MANUAL CREDIT";
                    break;
                case 5:
                    Type = "MANUAL JACKPOT";
                    break;
                case 6:
                    Type = "MANUAL PROGRESSIVE";
                    break;
                default:
                    Type = "Handpay";
                    break;
            }

            dynTableHeader = "";

            string sVersion = CommonDataAccess.GetVersion();
            DoWrite(" Site Name : " + jackpot.siteNo + "                            BMC Version : " + sVersion);

            //   DoWrite(dynTableHeader);
            DoWrite("<LN>");

            //DoWrite(" TYPE OF JACKPOT ");
            //   DoWrite(dynTableHeader);

            DoWrite("");

            DoWrite(" JP TYPE : " + Type);
            //   DoWrite(dynTableHeader);

            DoWrite(" SLOT :" + jackpot.Slot);
            //   DoWrite(dynTableHeader);

            DoWrite(" STAND : " + jackpot.assetConfigNumber);
            //   DoWrite(dynTableHeader);

            DoWrite(" TRANSACTION DATE : " + jackpot.TransactionDate);
            //   DoWrite(dynTableHeader);

            DoWrite(" EMPLOYEE ID : " + jackpot.UserID);
            //   DoWrite(dynTableHeader);

            DoWrite(" EMPLOYEE NAME : " + jackpot.UserName);
            //   DoWrite(dynTableHeader);


            DoWrite(" SITE ID : " + jackpot.siteId);
            //   DoWrite(dynTableHeader);


            DoWrite(" DENOM : " + jackpot.Denom);
            //   DoWrite(dynTableHeader);
            double JAmt = Double.Parse(jackpot.hpjpAmount.ToString()) / 100;
            DoWrite(" ORIGINAL AMT : " + CommonUtilities.GetCurrency(JAmt));
            //   DoWrite(dynTableHeader);

            DoWrite(" ACTUAL AMT : " + CommonUtilities.GetCurrency(JAmt));
            //   DoWrite(dynTableHeader);

            DoWrite(" HP JP AMT : " + CommonUtilities.GetCurrency(JAmt));
            //   DoWrite(dynTableHeader);

            DoWrite(" TOTAL TAX AMT : -------------------------");
            //   DoWrite(dynTableHeader);

            DoWrite(" TOTAL JACKPOT AMT : " + CommonUtilities.GetCurrency(JAmt));
            //   DoWrite(dynTableHeader);

            DoWrite(" AMT IN WRITING : " +
                objWords.ConvertValueToWords(JAmt, CommonDataAccess.GetSettingValue("Region") == "US" ? "en-US" :
                CommonDataAccess.GetSettingValue("Region") == "UK" ? "en-GB" :
                CommonDataAccess.GetSettingValue("Region") == "IT" ? "it-IT" : "en-US"));
            //   DoWrite(dynTableHeader);

            DoWrite(" SHIFT: DAY ");
            //   DoWrite(dynTableHeader);

            DoWrite(" PAYLINE : -------------------------");
            //   DoWrite(dynTableHeader);

            DoWrite(" COINS PLAYED : -------------------------");
            //   DoWrite(dynTableHeader);


            DoWrite(" WINNING COMBO : -------------------------");
            //   DoWrite(dynTableHeader);

            DoWrite(" WINDOW : -------------------------");
            //   DoWrite(dynTableHeader);

            DoWrite(" PLAYER CARD : 0000000000 ");
            //   DoWrite(dynTableHeader);

            DoWrite(" PLAYER NAME : -------------------------");
            //   DoWrite(dynTableHeader);

            DoWrite(" TOP : -------------------------");
            //   DoWrite(dynTableHeader);

            DoWrite(" MIDDLE : -------------------------");
            //   DoWrite(dynTableHeader);

            DoWrite(" BOTTOM : -------------------------");
            //   DoWrite(dynTableHeader);


            DoWrite(" IN-MTR : -------------------------");
            //   DoWrite(dynTableHeader);

            DoWrite(" RESET : -------------------------");
            //   DoWrite(dynTableHeader);

            DoWrite(" REEL SYMBOLS : -------------------------");
            //   DoWrite(dynTableHeader);

            DoWrite(" KIOSK : " + Environment.MachineName);
            //   DoWrite(dynTableHeader);

            DoWrite(" GAMING DAY : -------------------------");
            //   DoWrite(dynTableHeader);

            DoWrite(" AREA : -------------------------");
            //   DoWrite(dynTableHeader);


            DoWrite(" SLOT ATTN ID : -------------------------");
            //   DoWrite(dynTableHeader);


            DoWrite(" SLOT ATTN NAME : -------------------------");
            //   DoWrite(dynTableHeader);


            DoWrite(" FIRST AUTH EMP ID : -------------------------");
            //   DoWrite(dynTableHeader);


            DoWrite(" FIRST AUTH EMP NAME : -------------------------");
            //   DoWrite(dynTableHeader);


            DoWrite(" SECOND AUTH EMP ID : -------------------------");
            //   DoWrite(dynTableHeader);


            DoWrite(" SECOND AUTH EMP NAME : -------------------------");
            //   DoWrite(dynTableHeader);


            DoWrite(" PRINTER : -------------------------");
            //   DoWrite(dynTableHeader);


            DoWrite(" REGULATORY ID :  ------------------------- ");
            //   DoWrite(dynTableHeader);


            DoWrite(" JP DESCRIPTION : -------------------------");
            //   DoWrite(dynTableHeader);


            DoWrite(" FLOOR PERSON : -------------------------");
            //   DoWrite(dynTableHeader);

            DoWrite(" CHANGE PERSON : -------------------------");
            //   DoWrite(dynTableHeader);

            DoWrite(" PREPARER : -------------------------");
            //   DoWrite(dynTableHeader);

            DoWrite(" PRINTED DATE : " + DateTime.Now.GetUniversalDateTimeFormat());
            //   DoWrite(dynTableHeader);

            DoWrite(" SEQUENCE NO : " + jackpot.sequenceNumber.ToString());
            //  //   DoWrite(dynTableHeader);
            DoWrite("<LN>");

            writer.Close();
            PrintSlippage();

        }
#else
        TextSettings _textSettings = null;
        
        public void PrintSlip(jackpotProcessInfoDTO jackpot)
        {            
            JackpotXml xml = null;

            try
            {
                filename = "Print.txt";
                filepath = System.Windows.Forms.Application.StartupPath + "\\" + filename;
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
                outputfile = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
                writer = new StreamWriter(outputfile);
                writer.BaseStream.Seek(0, SeekOrigin.End);

                xml = new JackpotXml(writer);
                xml.ParseXmlFile();

                ModuleName module = ModuleName.AttendantPay;
                string Type = string.Empty;                
                string screenName = Settings.CAGE_ENABLED ? "PrintCageSlip|" : "";
                switch (jackpot.jackpotTypeId)
                {
                    case 1:
                        Type = "AttendantPay Credit";
                        screenName += "AttendantPay Credit";
                        break;
                    case 2:
                        Type = "AttendantPay Jackpot";
                        screenName += "AttendantPay Jackpot";
                        break;
                    case 3:
                        Type = "Progressive";
                        screenName += "Progressive";
                        break;
                    case 4:
                        Type = "MANUAL CREDIT";
                        module = ModuleName.ManualAttendantPay;
                        screenName += "MANUAL CREDIT";
                        break;
                    case 5:
                        Type = "MANUAL JACKPOT";
                        module = ModuleName.ManualAttendantPay;
                        screenName += "MANUAL JACKPOT";
                        break;
                    case 6:
                        Type = "MANUAL PROGRESSIVE";
                        module = ModuleName.ManualAttendantPay;
                        screenName += "MANUAL PROGRESSIVE";
                        break;
                    default:
                        Type = "Handpay";
                        screenName += "Handpay";
                        break;
                }

                // Modified by A.Vinod Kumar at 3:40 PM 17/01/12
                // required values for xml
                string sVersion = CommonDataAccess.GetVersion();
                double JAmt = Double.Parse(jackpot.hpjpAmount.ToString()) / 100;
                double jActualAmt = JAmt;
                if (JAmt < 0) jActualAmt = (-1 * JAmt);
                string JAmtInWords = objWords.ConvertValueToWords(jActualAmt,
                    CommonDataAccess.GetSettingValue("Region") == "US" ? "en-US" :
                    CommonDataAccess.GetSettingValue("Region") == "UK" ? "en-GB" :
                    CommonDataAccess.GetSettingValue("Region") == "IT" ? "it-IT" : "en-US");

                // fill the values
                xml[JackpotXmlFields.SiteName].Value = jackpot.siteNo;                                          // Site Name
                xml[JackpotXmlFields.BMCVersion].Value = sVersion;                                              // BMC Version
                xml[JackpotXmlFields.Type].Value = Type;                                                        // Type
                xml[JackpotXmlFields.Amount].Value = CommonUtilities.GetCurrency(JAmt);                         // Amount
                xml[JackpotXmlFields.AmountInWords].Value = JAmtInWords;                                        // Amount in Words
                xml[JackpotXmlFields.TransactionDateTime].Value = jackpot.TransactionDate;                      // Transaction Date/Time
                xml[JackpotXmlFields.BarPosition].Value = jackpot.assetConfigNumber + "/" + jackpot.Slot;       // Bar Position
                xml[JackpotXmlFields.SlotDenom].Value = jackpot.Denom.ToString();                               // Slot Denomination
                xml[JackpotXmlFields.SiteID].Value = jackpot.siteId.ToString();                                 // Site Id
                xml[JackpotXmlFields.PrintedDate].Value = DateTime.Now.GetUniversalDateTimeFormat();            // Printed Date/Time
                xml[JackpotXmlFields.SequenceNo].Value = jackpot.sequenceNumber.ToString();                     // Sequence No

                xml.Write();
                writer.Close();
                _textSettings = xml.TextSettings;
                PrintSlippage(xml.TextSettings);

                AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                {
                    AuditModuleName = module,
                    Audit_Screen_Name = screenName,
                    Audit_Desc = Type,
                    AuditOperationType = OperationType.ADD,
                    Audit_Field = "Amount",
                    Audit_New_Vl = xml[JackpotXmlFields.Amount].Value.ToString(),
                    Audit_Slot = xml[JackpotXmlFields.BarPosition].Value
                });
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (xml != null)
                {
                    xml.Dispose();
                    xml = null;
                }
            }
        }
#endif

        #region Print Functionality

#if SP5_SLIP_FORMAT
        private void PrintSlippage()
#else
        private void PrintSlippage(TextSettings textSettings)
#endif
        {
            string filename = "Print.txt";
            printDocument1 = new PrintDocument();
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            filepath = System.Windows.Forms.Application.StartupPath + "\\" + filename;
            fileToPrint = new System.IO.StreamReader(filepath);
#if SP5_SLIP_FORMAT
            printFont = new System.Drawing.Font("Courier New", 10, FontStyle.Bold);
#else
            printFont = textSettings.TextFont;
#endif
            printDocument1.DocumentName = filepath;
            printDocument1.Print();
            fileToPrint.Close();
        }

        void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string line = null;
            float yPos = 0f;
            int count = 0;

#if SP5_SLIP_FORMAT
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            
            Font HeaderFont = new System.Drawing.Font("Courier New", 12, FontStyle.Bold);
            float linesPerPage = e.MarginBounds.Height / printFont.GetHeight(e.Graphics);
            while (count < linesPerPage)
            {
                line = fileToPrint.ReadLine();
                if (line == null)
                {
                    break;
                }
                if (line == "<LN>")
                {
                    yPos = topMargin + count * HeaderFont.GetHeight(e.Graphics);

                    e.Graphics.DrawLine(new Pen(Brushes.Black), leftMargin + 10, yPos, e.MarginBounds.Right, yPos);
                }
                else
                {
                    yPos = topMargin + count * printFont.GetHeight(e.Graphics);
                    e.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                }
                count++;
            }
            if (line != null)
            {
                e.HasMorePages = true;
            }
#else
            StringFormat sf = new StringFormat();
            float leftMargin = _textSettings.Margins.Left;
            float topMargin = _textSettings.Margins.Top;

            Font HeaderFont = _textSettings.TextFont;
            float linesPerPage = _textSettings.TotalLines;
            while (count < linesPerPage)
            {
                line = fileToPrint.ReadLine();
                if (line == null)
                {
                    break;
                }
                if (line == "<LN>")
                {
                    yPos = topMargin + (count * _textSettings.FontSize.Height);
                    e.Graphics.DrawLine(new Pen(Brushes.Black), leftMargin + 10, yPos, _textSettings.Margins.Right, yPos);
                }
                else
                {
                    yPos = topMargin + (count * _textSettings.FontSize.Height);
                    if (Settings.CAGE_ENABLED &&
                        (line.Length > _textSettings.CharactersPerLine))
                    {
                        float width = _textSettings.CharactersPerLine;
                        float height = (float)Math.Ceiling((float)line.Length / (float)_textSettings.CharactersPerLine);

                        float actualWidth = (width * _textSettings.FontSize.Width);
                        if (actualWidth > _textSettings.Margins.Width)
                        {
                            actualWidth = _textSettings.Margins.Width;
                            height += 1;
                        }
                        float actualHeight = (height * _textSettings.FontSize.Height);
                        RectangleF rcText = new RectangleF(leftMargin, yPos, actualWidth, actualHeight);
                        e.Graphics.DrawString(line, printFont, Brushes.Black, rcText, sf);
                    }
                    else
                    {
                        e.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, sf);
                    }
                }
                count++;
            }
            if (!string.IsNullOrEmpty(line))
            {
                e.HasMorePages = true;
            }
#endif
        }
        #endregion Print Functionality

        private void DoWrite(String line)
        {            
            dynTableHeader = string.Empty;
            writer.WriteLine(line);
            writer.Flush();
        }
    }

#if !SP5_SLIP_FORMAT
    /// <summary>
    /// Jackpot Xml Field
    /// </summary>
    public class JackpotXmlField
    {
        /// <summary>
        /// Text Settings
        /// </summary>
        private TextSettings _textSettings = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="JackpotXmlField"/> class.
        /// </summary>
        /// <param name="textSettings">The text settings.</param>
        /// <param name="name">The name.</param>
        public JackpotXmlField(TextSettings textSettings, string name)
        {
            _textSettings = textSettings;
            this.XmlName = name;
            this.NameWidthPercentage = 0.36f;
            this.ValueWidthPercentage = 0.64f;
        }

        /// <summary>
        /// Gets or sets the name of the XML.
        /// </summary>
        /// <value>
        /// The name of the XML.
        /// </value>
        public string XmlName { get; private set; }

        /// <summary>
        /// Gets or sets the name of the field.
        /// </summary>
        /// <value>
        /// The name of the field.
        /// </value>
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the width percentage.
        /// </summary>
        /// <value>
        /// The width percentage.
        /// </value>
        public float WidthPercentage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [skip new line].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [skip new line]; otherwise, <c>false</c>.
        /// </value>
        public bool SkipNewLine { get; set; }

        /// <summary>
        /// Gets or sets the empty lines.
        /// </summary>
        /// <value>
        /// The empty lines.
        /// </value>
        public uint EmptyLines { get; set; }

        /// <summary>
        /// Gets or sets the repeatable count.
        /// </summary>
        /// <value>
        /// The repeatable count.
        /// </value>
        public int RepeatableCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has bottom line.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has bottom line; otherwise, <c>false</c>.
        /// </value>
        public bool HasBottomLine { get; set; }

        /// <summary>
        /// Gets or sets the name width percentage.
        /// </summary>
        /// <value>
        /// The name width percentage.
        /// </value>
        public float NameWidthPercentage { get; set; }

        /// <summary>
        /// Gets or sets the value width percentage.
        /// </summary>
        /// <value>
        /// The value width percentage.
        /// </value>
        public float ValueWidthPercentage { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                float availableArea = (float)Math.Ceiling(_textSettings.CharactersPerLine * this.WidthPercentage);//(float)Math.Ceiling(charsPerPixels * this.WidthPercentage);
                int nameWidth = 0;
                int valueWidth = 0;

                string actualName = " " + this.FieldName;
                string actualValue = this.Value;
                if (string.IsNullOrEmpty(actualValue)) actualValue = "";
                string prefixName = "-";
                string prefixValue = "-";
                string formatName = string.Empty;
                string formatValue = string.Empty;
                bool canFormatName = true;

                if (this.NameWidthPercentage == -1 && this.ValueWidthPercentage == -1)
                {
                    nameWidth = actualName.Length;
                    valueWidth = ((int)availableArea) - nameWidth;
                    actualValue = " " + actualValue;
                }
                else if (this.NameWidthPercentage == -2 && this.ValueWidthPercentage == -2)
                {
                    formatName = string.Empty;
                    prefixName = prefixValue = "";
                    actualValue = actualName + "" + actualValue;
                    actualName = "";
                    valueWidth = ((int)availableArea) - 1;
                    canFormatName = false;
                }
                else
                {
                    nameWidth = (int)Math.Ceiling(availableArea * this.NameWidthPercentage);
                    valueWidth = (int)Math.Ceiling(availableArea * this.ValueWidthPercentage);
                }
                valueWidth = Math.Min(valueWidth, (int)(availableArea - nameWidth));

                // Don't stripped the actual text, it should be displayed as it is
                //if (!_textSettings.IsReceiptPrinter &&
                //    (actualValue.Length > valueWidth))
                //{
                //    actualValue = actualValue.Substring(0, valueWidth);
                //}

                if (canFormatName)
                {
                    formatName = "{0," + prefixName + nameWidth.ToString() + "}";
                }
                formatValue = "{0," + prefixValue + valueWidth.ToString() + "}";

                // string repeating
                if (this.RepeatableCount == -1 ||
                    this.RepeatableCount > 0)
                {
                    int length = 0;
                    if (this.RepeatableCount == -1)
                    {
                        length = valueWidth;
                    }
                    else if (this.RepeatableCount > 0)
                    {
                        length = Math.Min(this.RepeatableCount, valueWidth);
                    }

                    // fill the string with the given character
                    if (length > 0)
                    {
                        char repeatChar = this.Value[0];

                        //special case for '_'
                        if (repeatChar == '_')
                        {
                            int count = (length / 2);
                            StringBuilder sb2 = new StringBuilder();
                            for (int i = 0; i < count; i++)
                            {
                                sb2.Append("_ ");
                            }
                            actualValue = sb2.ToString();
                        }
                        else
                        {
                            actualValue = new string(this.Value[0], length);
                        }
                    }
                    else
                    {
                        actualValue = string.Empty;
                    }
                }

                sb.AppendFormat(formatName, actualName);
                sb.AppendFormat(formatValue, actualValue);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return sb.ToString(); ;
        }
    }

    /// <summary>
    /// Text Settings
    /// </summary>
    public class TextSettings
    {
        /// <summary>
        /// Text Font
        /// </summary>
        private Font _textFont = null;

        /// <summary>
        /// Gets the text font.
        /// </summary>
        public Font TextFont
        {
            get { return _textFont; }
        }

        /// <summary>
        /// Characters per line
        /// </summary>
        private int _charactersPerLine = 0;

        /// <summary>
        /// Gets the characters per line.
        /// </summary>
        public int CharactersPerLine
        {
            get { return _charactersPerLine; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextSettings"/> class.
        /// </summary>
        public TextSettings(PageSettings pageSettings)
        {
            this.PageSettings = pageSettings;
            this.CalculateSizes();
        }

        /// <summary>
        /// Calculates the sizes.
        /// </summary>
        private void CalculateSizes()
        {
            Graphics g = null;
            int charactersOnPage = 0;
            int linesPerPage = 0;

            try
            {
                _textFont = new System.Drawing.Font("Courier New", 10, FontStyle.Bold);

                PageSettings pageSettings = this.PageSettings;
                Rectangle pageBounds = pageSettings.Bounds;
                Rectangle marginBounds = new Rectangle(pageSettings.Margins.Left,
                                                       pageSettings.Margins.Top,
                                                       pageBounds.Width - (pageSettings.Margins.Left + pageSettings.Margins.Right),
                                                       pageBounds.Height - (pageSettings.Margins.Top + pageSettings.Margins.Bottom));

                g = this.PageSettings.PrinterSettings.CreateMeasurementGraphics();

                // charecters per line
                string characters = new string('W', 512);
                g.MeasureString(characters, _textFont,
                    marginBounds.Size, StringFormat.GenericTypographic,
                    out charactersOnPage, out linesPerPage);

                // Font Width and Height
                this.FontSize = g.MeasureString("W", _textFont);

                // Calculate charecters/line and margin
                if (Settings.CAGE_ENABLED)
                {
                    _charactersPerLine = (int)Math.Floor((decimal)(charactersOnPage / linesPerPage));
                    this.Margins = marginBounds;
                }
                else
                {
                    _charactersPerLine = 38; // If cage is disabled, then the receipt should be printed in EPSON RECEIPT PRINTER
                    this.Margins = new Rectangle(3, 3, 3, 3);
                    this.IsReceiptPrinter = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (g != null)
                {
                    g.Dispose();
                }
            }
        }

        /// <summary>
        /// Gets the margins.
        /// </summary>
        public Rectangle Margins { get; private set; }

        /// <summary>
        /// Gets or sets the page settings.
        /// </summary>
        /// <value>
        /// The page settings.
        /// </value>
        public PageSettings PageSettings { get; private set; }

        /// <summary>
        /// Gets the size of the font.
        /// </summary>
        /// <value>
        /// The size of the font.
        /// </value>
        public SizeF FontSize { get; private set; }

        /// <summary>
        /// Gets or sets the total lines.
        /// </summary>
        /// <value>
        /// The total lines.
        /// </value>
        public uint TotalLines { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is receipt printer.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is receipt printer; otherwise, <c>false</c>.
        /// </value>
        public bool IsReceiptPrinter { get; private set; }
    }

    /// <summary>
    /// Jackpot Xml Object
    /// </summary>
    public class JackpotXml : IDisposable
    {
        /// <summary>
        /// Xml Field and its values
        /// </summary>
        private readonly IDictionary<JackpotXmlFields, JackpotXmlField> _properties = null;

        /// <summary>
        /// File Path
        /// </summary>
        private readonly string _filePath = string.Empty;

        /// <summary>
        /// Xml File Information
        /// </summary>
        private FileInfo _xmlFile = null;

        /// <summary>
        /// Writer
        /// </summary>
        private StreamWriter _writer = null;

        /// <summary>
        /// Text Settings
        /// </summary>
        private TextSettings _textSettings = null;

        /// <summary>
        /// Gets the text settings.
        /// </summary>
        public TextSettings TextSettings
        {
            get { return _textSettings; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JackpotXml"/> class.
        /// </summary>
        public JackpotXml(StreamWriter writer)
        {
            _writer = writer;
            _properties = new SortedDictionary<JackpotXmlFields, JackpotXmlField>();
            _filePath = Path.Combine(Path.GetDirectoryName(
                Assembly.GetEntryAssembly().Location), "JackpotXml.xml");
            _xmlFile = new FileInfo(_filePath);
            this.PopulatePrinterSettings();
        }

        /// <summary>
        /// Populates the printer settings.
        /// </summary>
        private void PopulatePrinterSettings()
        {
            PageSettings pageSettings = null;

            try
            {
                PrintDocument doc = new PrintDocument();
                PrinterSettings prnSettings = doc.PrinterSettings;

                if (prnSettings != null)
                {
                    pageSettings = prnSettings.DefaultPageSettings;
                }

                if (pageSettings == null)
                {
                    pageSettings = doc.DefaultPageSettings;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (pageSettings == null)
                {
                    LogManager.WriteLog("^^^^=> (PopulatePrinterSettings) : Unable to get the page settings.", LogManager.enumLogLevel.Info);
                }
                _textSettings = new TextSettings(pageSettings);
            }
        }

        /// <summary>
        /// Parses the XML.
        /// </summary>
        public void ParseXmlFile()
        {
            bool saveFile = false;
            XmlDocument xDoc = new XmlDocument();
            FileStream fs = null;

            try
            {
                if (!_xmlFile.Exists)
                {
                    saveFile = true;
                }
                fs = _xmlFile.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);

                try
                {
                    xDoc.Load(fs);
                }
                catch { }
                XmlElement xRoot = xDoc.DocumentElement;
                if (xRoot == null ||
                    string.Compare("JackpotXml", xRoot.Name, true) != 0)
                {
                    xRoot = xDoc.CreateElement("JackpotXml");
                    xDoc.AppendChild(xRoot);
                    saveFile = true;
                }

                // Properties
                saveFile |= this.SaveProperty(xRoot, JackpotXmlFields.SiteName, "SITE NAME:", string.Empty, 0.5f, true, 0, 0);
                this[JackpotXmlFields.SiteName].NameWidthPercentage = -1;
                this[JackpotXmlFields.SiteName].ValueWidthPercentage = -1;

                saveFile |= this.SaveProperty(xRoot, JackpotXmlFields.BMCVersion, "BMC VERSION:", string.Empty, 0.5f, false, 1, 0);
                this[JackpotXmlFields.BMCVersion].HasBottomLine = true;
                this[JackpotXmlFields.BMCVersion].NameWidthPercentage = -2;
                this[JackpotXmlFields.BMCVersion].ValueWidthPercentage = -2;

                saveFile |= this.SaveProperty(xRoot, JackpotXmlFields.Type, "TYPE:", 1);
                saveFile |= this.SaveProperty(xRoot, JackpotXmlFields.Amount, "AMOUNT:", 1);
                saveFile |= this.SaveProperty(xRoot, JackpotXmlFields.AmountInWords, "AMOUNT IN WRITING:", 1);
                saveFile |= this.SaveProperty(xRoot, JackpotXmlFields.TransactionDateTime, "TRANSACTION DATE/TIME:", 1);
                saveFile |= this.SaveProperty(xRoot, JackpotXmlFields.BarPosition, "STAND/POSITION:", 1);
                saveFile |= this.SaveProperty(xRoot, JackpotXmlFields.SlotDenom, "SLOT DENOM:", 1);
                saveFile |= this.SaveProperty(xRoot, JackpotXmlFields.SiteID, "SITE ID:", 1);
                saveFile |= this.SaveProperty(xRoot, JackpotXmlFields.Shift, "SHIFT:", 1, "....", 0);
                if (string.IsNullOrEmpty(this[JackpotXmlFields.Shift].Value))
                {
                    this[JackpotXmlFields.Shift].Value = "....";
                }
                saveFile |= this.SaveProperty(xRoot, JackpotXmlFields.PlayerCard, "PLAYER CARD:", 1, "_", -1);
                saveFile |= this.SaveProperty(xRoot, JackpotXmlFields.PlayerName, "PLAYER NAME:", 1, "_", -1);
                saveFile |= this.SaveProperty(xRoot, JackpotXmlFields.SlotAttendantID, "SLOT ATTENDANT ID:", 1, "_", -1);
                saveFile |= this.SaveProperty(xRoot, JackpotXmlFields.SlotAttendantName, "SLOT ATTENDANT NAME:", 1, "_", -1);
                saveFile |= this.SaveProperty(xRoot, JackpotXmlFields.SlotSupervisorID, "SLOT SUPERVISOR ID:", 1, "_", -1);
                saveFile |= this.SaveProperty(xRoot, JackpotXmlFields.SlotSupervisorName, "SLOT SUPERVISOR NAME:", 1, "_", -1);
                saveFile |= this.SaveProperty(xRoot, JackpotXmlFields.SlotManagerID, "SLOT MANAGER ID:", 1, "_", -1);
                saveFile |= this.SaveProperty(xRoot, JackpotXmlFields.SlotManagerName, "SLOT MANAGER NAME:", 1, "_", -1);
                saveFile |= this.SaveProperty(xRoot, JackpotXmlFields.CashierID, "CASHIER ID:", 1, "_", -1);
                saveFile |= this.SaveProperty(xRoot, JackpotXmlFields.CashierName, "CASHIER NAME:", 2, "_", -1);
                saveFile |= this.SaveProperty(xRoot, JackpotXmlFields.PrintedDate, "PRINTED DATE / TIME:", 1);
                saveFile |= this.SaveProperty(xRoot, JackpotXmlFields.SequenceNo, "SEQUENCE NO:", 1);
                this[JackpotXmlFields.SequenceNo].HasBottomLine = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                try
                {
                    fs.Close();
                    fs.Dispose();
                }
                catch { }

                if (saveFile)
                {
                    xDoc.Save(_xmlFile.FullName);
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="System.String"/> with the specified field name.
        /// </summary>
        public JackpotXmlField this[JackpotXmlFields fieldName]
        {
            get
            {
                if (_properties.ContainsKey(fieldName))
                    return _properties[fieldName];
                return null;
            }
        }

        ///// <summary>
        ///// Gets or sets the <see cref="System.String"/> with the specified field name.
        ///// </summary>
        //public string this[JackpotXmlFields fieldName]
        //{
        //    get
        //    {
        //        if (_properties.ContainsKey(fieldName))
        //            return _properties[fieldName].Value;
        //        return string.Empty;
        //    }
        //    set
        //    {
        //        if (_properties.ContainsKey(fieldName))
        //        {
        //            _properties[fieldName].Value = value;
        //        }
        //    }
        //}

        /// <summary>
        /// Saves the property.
        /// </summary>
        /// <param name="rootElement">The root element.</param>
        /// <param name="xmlField">The XML field.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="width">The width.</param>
        /// <param name="skipNewline">if set to <c>true</c> [skip newline].</param>
        /// <returns>
        /// True if the element is modified; otherwise false.
        /// </returns>
        private bool SaveProperty(XmlElement rootElement, JackpotXmlFields xmlField, string defaultValue)
        {
            return this.SaveProperty(rootElement, xmlField, defaultValue, string.Empty, 1, false, 0, 0);
        }

        /// <summary>
        /// Saves the property.
        /// </summary>
        /// <param name="rootElement">The root element.</param>
        /// <param name="xmlField">The XML field.</param>
        /// <param name="defaultName">The default name.</param>
        /// <param name="emptyLines">The empty lines.</param>
        /// <returns>
        /// True if the element is modified; otherwise false.
        /// </returns>
        private bool SaveProperty(XmlElement rootElement, JackpotXmlFields xmlField, string defaultName, string defaultValue, int repeatableCount)
        {
            return this.SaveProperty(rootElement, xmlField, defaultName, defaultValue, 1, false, 0, repeatableCount);
        }

        /// <summary>
        /// Saves the property.
        /// </summary>
        /// <param name="rootElement">The root element.</param>
        /// <param name="xmlField">The XML field.</param>
        /// <param name="defaultName">The default name.</param>
        /// <param name="emptyLines">The empty lines.</param>
        /// <returns>
        /// True if the element is modified; otherwise false.
        /// </returns>
        private bool SaveProperty(XmlElement rootElement, JackpotXmlFields xmlField, string defaultName, uint emptyLines)
        {
            return this.SaveProperty(rootElement, xmlField, defaultName, string.Empty, 1, false, emptyLines, 0);
        }

        /// <summary>
        /// Saves the property.
        /// </summary>
        /// <param name="rootElement">The root element.</param>
        /// <param name="xmlField">The XML field.</param>
        /// <param name="defaultName">The default name.</param>
        /// <param name="emptyLines">The empty lines.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="repeatableCount">The repeatable count.</param>
        /// <returns>
        /// True if the element is modified; otherwise false.
        /// </returns>
        private bool SaveProperty(XmlElement rootElement, JackpotXmlFields xmlField, string defaultName, uint emptyLines, string defaultValue, int repeatableCount)
        {
            return this.SaveProperty(rootElement, xmlField, defaultName, defaultValue, 1, false, emptyLines, repeatableCount);
        }

        /// <summary>
        /// Saves the property.
        /// </summary>
        /// <param name="rootElement">The root element.</param>
        /// <param name="xmlField">The XML field.</param>
        /// <param name="defaultName">The default name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="widthPercentage">The width percentage.</param>
        /// <param name="skipNewline">if set to <c>true</c> [skip newline].</param>
        /// <param name="emptyLines">The empty lines.</param>
        /// <param name="repeatbleCount">The repeatble count.</param>
        /// <returns>
        /// True if the element is modified; otherwise false.
        /// </returns>
        private bool SaveProperty(XmlElement rootElement, JackpotXmlFields xmlField,
            string defaultName, string defaultValue, float widthPercentage, bool skipNewline,
            uint emptyLines, int repeatbleCount)
        {
            string propName = xmlField.ToString();
            XmlElement xElem = rootElement.SelectSingleNode(string.Format("Property[@Name='{0}']", propName)) as XmlElement;
            bool result = false;

            if (xElem == null)
            {
                xElem = rootElement.OwnerDocument.CreateElement("Property");
                rootElement.AppendChild(xElem);
                result = true;
            }

            JackpotXmlField field = null;
            if (!_properties.ContainsKey(xmlField))
            {
                field = new JackpotXmlField(_textSettings, propName);
                _properties.Add(xmlField, field);
            }
            else
            {
                field = _properties[xmlField];
            }
            field.FieldName = this.GetAttributeString(xElem, "Name", propName, ref result);
            field.FieldName = this.GetAttributeString(xElem, "DisplayName", defaultName, ref result);
            field.Value = this.GetAttributeString(xElem, "Value", defaultValue, ref result);
            field.WidthPercentage = widthPercentage;
            field.SkipNewLine = skipNewline;
            field.EmptyLines = emptyLines;
            field.RepeatableCount = repeatbleCount;
            return result;
        }

        /// <summary>
        /// Gets the attribute string.
        /// </summary>
        /// <param name="xElem">The x elem.</param>
        /// <param name="attrName">Name of the attr.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="isModified">if set to <c>true</c> [is modified].</param>
        /// <returns>Attribute String.</returns>
        private string GetAttributeString(XmlElement xElem, string attrName, string defaultValue, ref bool isModified)
        {
            XmlAttribute xAttr = xElem.Attributes[attrName];
            isModified |= false;
            if (xAttr == null)
            {
                xAttr = xElem.OwnerDocument.CreateAttribute(attrName);
                xElem.Attributes.Append(xAttr);
                xAttr.InnerText = defaultValue;
                isModified = true;
            }
            return xAttr.InnerText;
        }

        /// <summary>
        /// Writes this instance.
        /// </summary>
        public void Write()
        {
            try
            {
                uint totalLines = 0;

                foreach (JackpotXmlField field in _properties.Values)
                {
                    // actual string
                    if (field.SkipNewLine)
                    {
                        _writer.Write(field);
                    }
                    else
                    {
                        _writer.WriteLine(field);
                        totalLines++;
                    }

                    // line
                    if (!field.SkipNewLine &&
                        field.HasBottomLine)
                    {
                        _writer.WriteLine("<LN>");
                        totalLines++;
                    }

                    // empty lines
                    uint lines = field.EmptyLines;
                    while (lines > 0)
                    {
                        _writer.WriteLine("");
                        totalLines++;
                        lines--;
                    }
                }
                _textSettings.TotalLines = totalLines;
                _writer.Flush();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        public IDictionary<JackpotXmlFields, JackpotXmlField> Properties
        {
            get
            {
                return _properties;
            }
        }


        #region IDisposable Members

        /// <summary>
        /// Variable used to identity whether this object is already disposed or not.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="JackpotXml"/> is reclaimed by garbage collection.
        /// </summary>
        ~JackpotXml()
        {
            Dispose(false);
        }

        #endregion
    }

    /// <summary>
    /// Jackpot Xml Fields
    /// </summary>
    public enum JackpotXmlFields
    {
        SiteName = 0,
        BMCVersion,
        Type,
        Amount,
        AmountInWords,
        TransactionDateTime,
        BarPosition,
        SlotDenom,
        SiteID,
        Shift,
        PlayerCard,
        PlayerName,
        SlotAttendantID,
        SlotAttendantName,
        SlotSupervisorID,
        SlotSupervisorName,
        SlotManagerID,
        SlotManagerName,
        CashierID,
        CashierName,
        PrintedDate,
        SequenceNo
    }
#endif
}
