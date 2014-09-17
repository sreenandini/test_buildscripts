using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BMC.EventsTransmitter.Utils
{
    public static class XmlExtension
    {
        /// <summary>
        /// Handles null values while reading InnerText 
        /// </summary>
        /// <param name="Node"></param>
        /// <returns></returns>
        public static string GetInnerText(this XmlNode Node)
        {
            if (Node == null)
                return string.Empty;
            else
                return StringExtension.NullToString(Node.InnerText);


        }
    }
}
