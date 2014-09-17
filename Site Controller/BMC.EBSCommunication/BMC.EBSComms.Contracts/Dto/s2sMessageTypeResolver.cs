using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;

namespace BMC.EBSComms.Contracts.Dto
{
   public static class s2sMessageTypeResolver
    {
        private static Type[] _knownTypes = null;

       static s2sMessageTypeResolver()
       {
           ResolveTypes();
       }

       private static void ResolveTypes()
       {
           ModuleProc PROC = new ModuleProc("", "ResolveTypes");

           try
           {
               var types = (from t in typeof(s2sMessage).Assembly.GetTypes()
                            from c in t.GetCustomAttributes(typeof(XmlTypeAttribute), false).OfType<XmlTypeAttribute>()
                            where c.Namespace.IgnoreCaseCompare("http://www.gamingstandards.com/s2s/schemas/v1.2.6/") ||
                                    c.Namespace.IgnoreCaseCompare("http://www.ballytech.com/s2s/schemas/v1.0.0/")
                            orderby t.Name
                            select t).ToArray();
               if (types != null && types.Length > 0)
               {
                   _knownTypes = types;
               }
           }
           catch (Exception ex)
           {
               Log.Exception(PROC, ex);
           }
       }

       public static Type[] KnownTypes
       {
           get { return s2sMessageTypeResolver._knownTypes; }
       }
    }
}
