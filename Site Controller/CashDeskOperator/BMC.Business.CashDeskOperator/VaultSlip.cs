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
using System.Data;
using BMC.Security;

namespace BMC.Business.CashDeskOperator
{
    //public class VaultSlipXml : 
    /// <summary>
    /// VaultSlip Xml Object
    /// </summary>
    /// 

    public class PrintVaultSlip
    {
          string dynTableHeader;
            System.IO.StreamReader fileToPrint;
            System.Drawing.Font printFont;
            PrintDocument printDocument1;
            string filename = "";
            StreamWriter writer = null;
            string filepath;
            FileStream outputfile = null;
            TextSettings _textSettings = null;
        public void PrintSlip(DataRow drFillDetails,string strFillType)
        {
            ValuetoWords objWords = new ValuetoWords();
            VaultSlipXml xml = null;

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

                xml = new VaultSlipXml(writer);
                xml.ParseXmlFile();

                ModuleName module = ModuleName.AttendantPay;
                string Type = string.Empty;
              



                //         Header=0,
                //PrintedDate,
                //SiteName,

                //Vault_Name,
                //SerialNo,
                //Manufacturer,
                //Type,
                //Fill_User,
                //FillDate,
                //Initial_Balance,
                //Fill_Amount,
                //CurrentBalance,
                //Signature



                // required values for xml
                string sVersion = CommonDataAccess.GetVersion();
                Double dAmount =Double.Parse(drFillDetails["FillAmount"].ToString());
                if (dAmount < 0)
                    dAmount = dAmount * -1;
                string AmtInWords = objWords.ConvertValueToWords(dAmount,
                    CommonDataAccess.GetSettingValue("Region") == "US" ? "en-US" :
                    CommonDataAccess.GetSettingValue("Region") == "UK" ? "en-GB" :
                    CommonDataAccess.GetSettingValue("Region") == "IT" ? "it-IT" : "en-US");

                // fill the values
                //xml[VaultSlipXmlFields.Header].Value = "Fill slip";
                xml[VaultSlipXmlFields.PrintedDate].Value = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
                xml[VaultSlipXmlFields.SiteName].Value = Settings.SiteName;

                xml[VaultSlipXmlFields.Vault_Name].Value = drFillDetails["Name"].ToString();
                xml[VaultSlipXmlFields.SerialNo].Value = drFillDetails["Serial_NO"].ToString();
                xml[VaultSlipXmlFields.Manufacturer].Value = drFillDetails["Manufacturer_Name"].ToString();
                xml[VaultSlipXmlFields.Type].Value = drFillDetails["Type_Prefix"].ToString();

                xml[VaultSlipXmlFields.Fill_User].Value =SecurityHelper.CurrentUser.DisplayName;
                xml[VaultSlipXmlFields.FillDate].Value = DateTime.Parse(drFillDetails["CreatedDate"].ToString()).ToString("dd-MMM-yyyy HH:mm:ss");
                xml[VaultSlipXmlFields.Fill_Amount].Value = CommonUtilities.GetCurrency(double.Parse(drFillDetails["FillAmount"].ToString()));

                if(strFillType!=string.Empty)
                    xml[VaultSlipXmlFields.Fill_Type].Value = strFillType;

                xml[VaultSlipXmlFields.Initial_Balance].Value = CommonUtilities.GetCurrency(double.Parse(drFillDetails["TotalAmountOnFill"].ToString()));
                xml[VaultSlipXmlFields.CurrentBalance].Value = CommonUtilities.GetCurrency(double.Parse(drFillDetails["CurrentBalance"].ToString()));
                xml[VaultSlipXmlFields.Signature].Value = string.Empty;


                xml.Write();
                writer.Close();
                _textSettings = xml.TextSettings;
                PrintSlippage(xml.TextSettings);


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
        private void PrintSlippage(TextSettings textSettings)
        {
            string filename = "Print.txt";
            printDocument1 = new PrintDocument();
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            filepath = System.Windows.Forms.Application.StartupPath + "\\" + filename;
            fileToPrint = new System.IO.StreamReader(filepath);
            printFont = textSettings.TextFont;
            printDocument1.DocumentName = filepath;
            printDocument1.Print();
            fileToPrint.Close();
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string line = null;
            float yPos = 0f;
            int count = 0;

            StringFormat sf = new StringFormat();
            float leftMargin = _textSettings.Margins.Left;
            float topMargin = _textSettings.Margins.Top;
            Font HeaderFont = _textSettings.TextFont;
            float linesPerPage = _textSettings.TotalLines;
            while (count < _textSettings.TotalLines)
            {
                line = fileToPrint.ReadLine();
                if (line == null)
                {
                    break;
                }
                else
                {
                   yPos = topMargin + (count * _textSettings.FontSize.Height);
                   e.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, sf);
                }
                count++;
            }
            if (!string.IsNullOrEmpty(line))
            {
                e.HasMorePages = true;
            }

        }

    }

    public class VaultSlipXml : IDisposable
    {
      

        /// <summary>
        /// Xml Field and its values
        /// </summary>
        private readonly IDictionary<VaultSlipXmlFields, JackpotXmlField> _properties = null;

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

        public VaultSlipXml(StreamWriter writer)
            : this(writer, "VaultSlipXml.xml") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="VaultSlipXml"/> class.
        /// </summary>
        public VaultSlipXml(StreamWriter writer, string xmlFileName)
        {
            _writer = writer;
            _properties = new SortedDictionary<VaultSlipXmlFields, JackpotXmlField>();
            _filePath = Path.Combine(Path.GetDirectoryName(
                Assembly.GetEntryAssembly().Location), xmlFileName);
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
        public virtual void ParseXmlFile()
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
                    string.Compare("VaultSlipXml", xRoot.Name, true) != 0)
                {
                    xRoot = xDoc.CreateElement("VaultSlipXml");
                    xDoc.AppendChild(xRoot);
                    saveFile = true;
                }

                // Properties

                

        //         Header=0,
        //PrintedDate,
        //SiteName,
        //Vault_Name,
        //SerialNo,
        //Manufacturer,
        //Type,
        //Fill_User,
        //FillDate,
        //Initial_Balance,
        //Fill_Amount,
        //CurrentBalance,
        //Signature


                saveFile |= this.SaveProperty(xRoot, VaultSlipXmlFields.BlankLine, "", string.Empty, 1f, false,0, 0);
                saveFile |= this.SaveProperty(xRoot, VaultSlipXmlFields.Header, "Vault Transaction Slip", string.Empty, 1f, false,0, 0);
                saveFile |= this.SaveProperty(xRoot, VaultSlipXmlFields.PrintedDate, "Date:", string.Empty, 0.5f, false, 0, 0);
                saveFile |= this.SaveProperty(xRoot, VaultSlipXmlFields.SiteName, "Site Name:", string.Empty, 0.5f,false, 0, 0);
                saveFile |= this.SaveProperty(xRoot, VaultSlipXmlFields.Line1, "==============================", string.Empty, 0.5f, false, 0, 0);
                saveFile |= this.SaveProperty(xRoot, VaultSlipXmlFields.Vault_Name, "Vault Name:", 1);
                saveFile |= this.SaveProperty(xRoot, VaultSlipXmlFields.SerialNo, "Serial No:", 1);
                saveFile |= this.SaveProperty(xRoot, VaultSlipXmlFields.Manufacturer, "Manufacturer:", 1);
                saveFile |= this.SaveProperty(xRoot, VaultSlipXmlFields.Type, "Vault Type:", 1);

                saveFile |= this.SaveProperty(xRoot, VaultSlipXmlFields.Fill_User, "Login User:", 1);
                saveFile |= this.SaveProperty(xRoot, VaultSlipXmlFields.FillDate, "Date:", 1);
                saveFile |= this.SaveProperty(xRoot, VaultSlipXmlFields.Fill_Amount, "Amount:", 1);
                saveFile |= this.SaveProperty(xRoot, VaultSlipXmlFields.Fill_Type, "Transaction Type:", 1);
                saveFile |= this.SaveProperty(xRoot, VaultSlipXmlFields.Initial_Balance, "Initial Balance:", 1);
                saveFile |= this.SaveProperty(xRoot, VaultSlipXmlFields.CurrentBalance, "CurrentBalance:", 1);
                //saveFile |= this.SaveProperty(xRoot, VaultSlipXmlFields.Line2, "------------------------------", 0);
                saveFile |= this.SaveProperty(xRoot, VaultSlipXmlFields.Signature, "Signature:", 1);
                
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
        public JackpotXmlField this[VaultSlipXmlFields fieldName]
        {
            get
            {
                if (_properties.ContainsKey(fieldName))
                    return _properties[fieldName];
                return null;
            }
        }

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
        private bool SaveProperty(XmlElement rootElement, VaultSlipXmlFields xmlField, string defaultValue)
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
        private bool SaveProperty(XmlElement rootElement, VaultSlipXmlFields xmlField, string defaultName, string defaultValue, int repeatableCount)
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
        private bool SaveProperty(XmlElement rootElement, VaultSlipXmlFields xmlField, string defaultName, uint emptyLines)
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
        private bool SaveProperty(XmlElement rootElement, VaultSlipXmlFields xmlField, string defaultName, uint emptyLines, string defaultValue, int repeatableCount)
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
        private bool SaveProperty(XmlElement rootElement, VaultSlipXmlFields xmlField,
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
        public IDictionary<VaultSlipXmlFields, JackpotXmlField> Properties
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
        /// <see cref="VaultSlipXml"/> is reclaimed by garbage collection.
        /// </summary>
        ~VaultSlipXml()
        {
            Dispose(false);
        }

        #endregion
    }

    /// <summary>
    /// VaultSlip Xml Fields
    /// </summary>
    public enum VaultSlipXmlFields
    {
        BlankLine=0,
        Header,
        PrintedDate,
        SiteName,
        Line1,
        Vault_Name,
        SerialNo,
        Manufacturer,
        Type,
        Fill_User,
        FillDate,
        Fill_Amount,
        Fill_Type,
        Initial_Balance,
        CurrentBalance,
        //Line2,
        Signature
    }

   
}
