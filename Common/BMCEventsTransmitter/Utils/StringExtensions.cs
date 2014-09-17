using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace BMC.EventsTransmitter.Utils
{
    public static class StringExtension
    {
        public static string STMFormat(this string str, int Length)
        {
            str = str.NullToString(); 
            if (str.Length > Length)
            {
                return str.Substring(str.Length - Length, Length);
            }
            else
                if (str.Trim() == string.Empty)
                {
                    return str.PadLeft(Length, ' ');
                }
                else
                {
                    return str.PadLeft(Length, '0');
                }
        }
        public static string STMXMLFormat(this string str, string XMLTag)
        {
            str = str.NullToString();
            return  new XElement(XMLTag, str).ToString();
            //return String.Format(string.Format("<{0}>{1}</{2}>", XMLTag, str, XMLTag));
        }


        public static string NullToString(this string StringVal)
        {
            if (StringVal == null)
                return string.Empty;
            else
               return StringVal.ToString();
        }
        public static string NullToString(this object StringVal)
        {
            if (StringVal == null)
                return string.Empty;
            else
                return StringVal.ToString();
        }

        public static int NullToInt(this string StringVal)
        {
            int iVal=0;
            if (StringVal == null)
                return iVal;
            else
            {
                int.TryParse(StringVal.ToString(),out iVal);
                return iVal; 
            }

        }
        public static int NullToInt(this string StringVal,int  DefaultValue)
        {
            int iVal = 0;
            if (StringVal == null)
                return DefaultValue;
            else
            {
                int.TryParse(StringVal.ToString(), out iVal);
                return iVal;
            }

        }
        public static int NullToInt(this object StringVal)
        {
            int iVal = 0;
            if (StringVal == null)
                return iVal;
            else
            {
                int.TryParse(StringVal.NulltoString(), out iVal);
                return iVal;
            }
        }
        public static int NullToInt(this  object StringVal, int DefaultValue)
        {
            int iVal = 0;
            if (StringVal == null)
                return DefaultValue;
            else
            {
                int.TryParse(StringVal.NulltoString(), out iVal);
                return iVal;
            }

        }

        public static string NulltoString(this object stringVal)
        {
            if (stringVal == null)
                return string.Empty;
            else
                return stringVal.ToString(); 
        }

        public static string NulltoString(this object stringVal,string DefaultValue)
        {
            if (stringVal == null)
                return DefaultValue.NullToString();
            else
                return stringVal.ToString();
        }

    }
}
