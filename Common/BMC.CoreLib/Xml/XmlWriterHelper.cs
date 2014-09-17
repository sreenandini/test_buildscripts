using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using BMC.CoreLib;
using System.IO;
using System.Xml.Serialization;

namespace BMC.CoreLib.Xml
{
    public class XmlWriterHelper : DisposableObject
    {
        private XmlWriter _tw = null;
        private StringBuilder _sb = null;

        internal XmlWriterHelper(XmlWriter tw)
        {
            _tw = tw;
        }

        internal XmlWriterHelper(XmlWriter tw, StringBuilder sb)
            : this(tw)
        {
            _sb = sb;
        }

        public static XmlWriterHelper GetHelper()
        {
            return GetHelper(false);
        }

        private static XmlWriterSettings GetWriterSettings(bool omitXmlDeclaration)
        {
            XmlWriterSettings xws = new XmlWriterSettings();
            if (omitXmlDeclaration)
            {
                xws.OmitXmlDeclaration = omitXmlDeclaration;
            }
            xws.Indent = true;
            return xws;
        }

        public static XmlWriterHelper GetHelper(bool omitXmlDeclaration)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings xws = GetWriterSettings(omitXmlDeclaration);
            XmlWriter tw = XmlWriter.Create(sb, xws);
            return new XmlWriterHelper(tw, sb);
        }

        public static XmlWriterHelper GetHelper(Stream stream, bool omitXmlDeclaration)
        {
            XmlWriterSettings xws = GetWriterSettings(omitXmlDeclaration);
            XmlWriter tw = XmlWriter.Create(stream, xws);
            return new XmlWriterHelper(tw);
        }

        public static XmlWriterHelper GetHelper(string outputFileName, bool omitXmlDeclaration)
        {
            XmlWriterSettings xws = GetWriterSettings(omitXmlDeclaration);
            XmlWriter tw = XmlWriter.Create(outputFileName, xws);
            return new XmlWriterHelper(tw);
        }

        public static XmlWriterHelper GetHelper(XmlWriter tw)
        {
            return new XmlWriterHelper(tw);
        }

        public XmlWriterHelper WriteStartElement(string elementName)
        {
            _tw.WriteStartElement(elementName);
            return this;
        }

        public XmlWriterHelper WriteString(int value)
        {
            _tw.WriteString(value.ToString());
            return this;
        }

        public XmlWriterHelper WriteString(long value)
        {
            _tw.WriteString(value.ToString());
            return this;
        }

        public XmlWriterHelper WriteString(string value)
        {
            _tw.WriteString(value);
            return this;
        }

        public XmlWriterHelper WriteRawString(string value)
        {
            _tw.WriteRaw(value);
            return this;
        }

        public XmlWriterHelper WriteEndElement()
        {
            _tw.WriteEndElement();
            return this;
        }

        public XmlWriterHelper WriteElementString(string elementName, string content)
        {
            _tw.WriteElementString(elementName, content);
            return this;
        }

        public XmlWriterHelper WriteElementString(string elementName, int content)
        {
            _tw.WriteElementString(elementName, content.ToString());
            return this;
        }

        public XmlWriterHelper WriteElementString(string elementName, long content)
        {
            _tw.WriteElementString(elementName, content.ToString());
            return this;
        }

        public XmlWriterHelper WriteElementString(string elementName, DateTime content, string format)
        {
            _tw.WriteElementString(elementName, content.ToString(format));
            return this;
        }

        public XmlWriterHelper WriteAttribute(string attributeName, int value)
        {
            _tw.WriteAttributeString(attributeName, value.ToString());
            return this;
        }

        public XmlWriterHelper WriteAttribute(string attributeName, string value)
        {
            _tw.WriteAttributeString(attributeName, value);
            return this;
        }

        public XmlWriterHelper WriteAction(Action<XmlWriterHelper> write)
        {
            if (write != null) write(this);
            return this;
        }

        public void Flush()
        {
            _tw.Flush();
        }

        public override string ToString()
        {
            if (_sb == null)
            {
                return base.ToString();
            }
            else
            {
                _tw.Flush();
                return _sb.ToString();
            }
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            _tw.Close();
            _tw = null;
        }
    }

    public static class XmlSerializerHelper
    {
        public static string ConvertObjectToXml(Object objToXml, XmlSerializerNamespaces ns,
                                            bool includeNamespace, bool includeStartDocument)
        {
            return ConvertObjectToXml(objToXml, string.Empty, ns, new UTF8Encoding(), includeNamespace, includeStartDocument);
        }

        public static string ConvertObjectToXml(Object objToXml, string defaultNamespace, XmlSerializerNamespaces ns,
                                           Encoding encoding, bool includeNamespace, bool includeStartDocument)
        {
            String xmlizedString = String.Empty;

            try
            {
                if (null == objToXml)
                {
                    throw new ArgumentNullException("ConvertObjectToXml");
                }

                XmlSerializer xmlSerializer = new XmlSerializer(objToXml.GetType(), defaultNamespace);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (SpecialXmlTextWriter stWriter = new SpecialXmlTextWriter(memoryStream, Encoding.UTF8, includeStartDocument))
                    {
                        if (ns == null)
                            xmlSerializer.Serialize(stWriter, objToXml);
                        else
                            xmlSerializer.Serialize(stWriter, objToXml, ns);

                        memoryStream.Position = 0;
                        xmlizedString = encoding.GetString(memoryStream.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }

            return xmlizedString;
        }

        #region Class SpecialXmlTextWriter
        /// <summary>
        /// SpecialXmlTextWriter Class for personalizing XML Writing
        /// </summary>
        public class SpecialXmlTextWriter : XmlTextWriter
        {
            bool m_includeStartDocument = true;
            public SpecialXmlTextWriter(TextWriter tw, bool includeStartDocument)
                : base(tw)
            {
                m_includeStartDocument = includeStartDocument;
            }

            public SpecialXmlTextWriter(Stream sw, Encoding encoding, bool includeStartDocument)
                : base(sw, null)
            {
                m_includeStartDocument = includeStartDocument;
            }

            public SpecialXmlTextWriter(string filePath, Encoding encoding, bool includeStartDocument)
                : base(filePath, null)
            {
                m_includeStartDocument = includeStartDocument;
            }

            public override void WriteStartDocument()
            {
                if (m_includeStartDocument)
                {
                    base.WriteStartDocument();
                }
            }
        }
        #endregion
    }
}
