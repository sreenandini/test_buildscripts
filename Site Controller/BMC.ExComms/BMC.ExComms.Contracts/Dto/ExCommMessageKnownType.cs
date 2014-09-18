using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;

namespace BMC.ExComms.Contracts.DTO
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum, AllowMultiple = true, Inherited = true)]
    public class ExCommsMessageKnownType : Attribute { }

    public class ExCommsMessageKnownTypeFactory
    {
        private static Type[] _knownTypes = null;
        private static readonly object _knownTypesLock = new object();

        static ExCommsMessageKnownTypeFactory() { }

        public static Type[] KnownTypes
        {
            get
            {
                if (_knownTypes == null)
                {
                    lock (_knownTypesLock)
                    {
                        if (_knownTypes == null)
                        {
                            ModuleProc PROC = new ModuleProc("", "GetKnownTypes");

                            try
                            {
                                _knownTypes = (from t in typeof(ExCommsMessageKnownType).Assembly.GetTypes()
                                               where t.GetCustomAttributes(typeof(ExCommsMessageKnownType), false).Length > 0
                                               orderby t.Name
                                               select t).ToArray();
                            }
                            catch (Exception ex)
                            {
                                Log.Exception(PROC, ex);
                            }
                        }
                    }
                }

                return _knownTypes;
            }
        }
    }
}
