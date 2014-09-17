using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using BMC.CoreLib;
using BMC.CoreLib.Xml;

namespace BMC.EBSComms.Server
{
    public partial class EBSCommServer
    {
        private string ConvertObjectToXml(Object objToXml, bool includeNamespace, bool includeStartDocument)
        {
            return XmlSerializerHelper.ConvertObjectToXml(objToXml, _ns, includeNamespace, includeStartDocument);
        }
    }
}
