using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using System.Xml.Serialization;
using BMC.CoreLib.Diagnostics;
using System.IO;

namespace BMC.EnterpriseClient.Helpers
{
    [XmlRoot("embedded_exe")]
    public class EmbeddedExeMetadata : DisposableObject
    {
        private string _name = string.Empty;
        private string[] _names = null;

        public EmbeddedExeMetadata() { }

        [XmlAttribute("name", DataType = "string")]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                _names = _name.Split('\\');
            }
        }

        public string[] Names { get { return _names; } }

        [XmlAttribute("type", DataType = "int")]
        public int Type { get; set; }

        [XmlAttribute("prefix_exe_path", DataType = "boolean")]
        public bool PrefixExePath { get; set; }

        [XmlAttribute("model", DataType = "boolean")]
        public bool Model { get; set; }

        [XmlAttribute("nonmdiclient", DataType = "boolean")]
        public bool NonMdiClient { get; set; }

        public ProcessActivatorType ToolType
        {
            get { return (ProcessActivatorType)this.Type; }
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    [XmlRoot("embedded_exes")]
    public class EmbeddedExeMetadatas : DisposableObject
    {
        private static EmbeddedExeMetadatas _current = null;

        public EmbeddedExeMetadatas() { }

        [XmlElement("embedded_exe")]
        public List<EmbeddedExeMetadata> Exes { get; set; }

        public static EmbeddedExeMetadatas Current
        {
            get
            {
                ModuleProc PROC = new ModuleProc("EmbeddedExeMetadatas", "Current");

                try
                {
                    if (_current == null)
                    {
                        // first load the document from exe path
                        string fileName = Path.Combine(Extensions.GetStartupDirectory(), "EmbeddedExeMetadata.xml");
                        if (File.Exists(fileName))
                        {
                            try
                            {
                                using (Stream st = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                                {
                                    _current = Extensions.ReadXmlObject<EmbeddedExeMetadatas>(st);
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.Exception(PROC, ex);
                            }
                        }

                        // not found the external file
                        if (_current == null)
                        {
                            string resrc = "BMC.EnterpriseClient.Helpers.EmbeddedExeMetadata.xml";
                            using (Stream st = typeof(EmbeddedExeMetadatas).Assembly.GetManifestResourceStream(resrc))
                            {
                                _current = Extensions.ReadXmlObject<EmbeddedExeMetadatas>(st);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(PROC, ex);
                }

                return _current;
            }
        }
    }
}
