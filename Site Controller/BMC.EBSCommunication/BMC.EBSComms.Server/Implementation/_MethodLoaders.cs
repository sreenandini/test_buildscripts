using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using BMC.CoreLib;
using BMC.CoreLib.Comparers;
using BMC.CoreLib.Diagnostics;
using BMC.EBSComms.Contracts.Dto;

namespace BMC.EBSComms.Server
{
    public delegate bool RecvFromEBSWorkHandler(s2sMessage target, object source);
    public delegate bool SendToEBSWorkHandler(s2sMessage target, XElement source);

    public class TypeFromEBSWorkDictionary : SortedDictionary<Type, RecvFromEBSWorkHandler>
    {
        public TypeFromEBSWorkDictionary()
            : base(new TypeComparer()) { }
    }

    public class StringToEBSWorkDictionary : SortedDictionary<string, SendToEBSWorkHandler>
    {
        public StringToEBSWorkDictionary()
            : base(StringComparer.InvariantCultureIgnoreCase) { }
    }

    public partial class EBSCommServer
    {
        private IDictionary<Type, TypeFromEBSWorkDictionary> _worksFromEBS = new SortedDictionary<Type, TypeFromEBSWorkDictionary>(new TypeComparer());
        private IDictionary<Type, StringToEBSWorkDictionary> _worksToEBS = new SortedDictionary<Type, StringToEBSWorkDictionary>(new TypeComparer());

        private void InitWorkHandlers()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "InitWorkHandlers");

            try
            {
                // From EBS
                _worksFromEBS = new SortedDictionary<Type, TypeFromEBSWorkDictionary>(new TypeComparer())
                {
                    { typeof(s2sMessage),
                        new TypeFromEBSWorkDictionary()
                        {
                            { typeof(s2sHeader), new RecvFromEBSWorkHandler(this.Parse_s2sHeader) },
                            { typeof(s2sBody), new RecvFromEBSWorkHandler(this.Parse_s2sBody) },
                        }
                    },
                    { typeof(s2sBody),
                        new TypeFromEBSWorkDictionary()
                        {
                            { typeof(configuration), new RecvFromEBSWorkHandler(this.Parse_configuration) },
                        }
                    },
                    { typeof(configuration),
                        new TypeFromEBSWorkDictionary()
                        {
                            { typeof(getConfiguration), new RecvFromEBSWorkHandler(this.Parse_getConfiguration) },                            
                        }
                    },
                    { typeof(getConfiguration),
                        new TypeFromEBSWorkDictionary()
                        {
                            { typeof(getConfigurationGetManufacturer), new RecvFromEBSWorkHandler(this.Parse_configurationInfo_getManufacturer) },
                            { typeof(getConfigurationGetDenomination), new RecvFromEBSWorkHandler(this.Parse_configurationInfo_getDenomination) },
                            { typeof(getConfigurationGetGame), new RecvFromEBSWorkHandler(this.Parse_configurationInfo_getGame) },
                            { typeof(getConfigurationGetMachine), new RecvFromEBSWorkHandler(this.Parse_configurationInfo_getMachine) },
                            { typeof(getConfigurationGetCasino), new RecvFromEBSWorkHandler(this.Parse_configurationInfo_getSite) },
                            { typeof(getConfigurationGetZone), new RecvFromEBSWorkHandler(this.Parse_configurationInfo_getZone) }
                        }
                    },
                };

                // To EBS
                _worksToEBS = new SortedDictionary<Type, StringToEBSWorkDictionary>(new TypeComparer())
                {
                    { typeof(infoUpdateData),
                        new StringToEBSWorkDictionary()
                        {
                            { "MANUFACTURER", new SendToEBSWorkHandler(this.Parse_configurationInfo_getManufacturer) },
                            { "DENOM", new SendToEBSWorkHandler(this.Parse_configurationInfo_getDenomination) },
                            { "GAME", new SendToEBSWorkHandler(this.Parse_configurationInfo_getGame) },
                            { "MACHINE", new SendToEBSWorkHandler(this.Parse_configurationInfo_getMachine) },
                            { "SITE", new SendToEBSWorkHandler(this.Parse_configurationInfo_getSite) },
                            { "ZONE", new SendToEBSWorkHandler(this.Parse_configurationInfo_getZone) },
                        }
                    },
                };
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void InvokeWork_FromEBS(s2sMessage target, object source)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "InvokeWork");

            try
            {
                if (source == null) return;

                if (source is Is2sMessage_Items)
                {
                    this.InvokeWorkInternal_FromEBS(target, source as Is2sMessage_Items);
                }
                else if (source is Is2sMessage_Item)
                {
                    this.InvokeWorkInternal_FromEBS(target, source as Is2sMessage_Item);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void InvokeWorkInternal_FromEBS(s2sMessage target, Is2sMessage_Items source)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "InvokeWork_Items");

            try
            {
                Type sourceType = source.GetType();
                if (_worksFromEBS.ContainsKey(sourceType))
                {
                    TypeFromEBSWorkDictionary workDict = _worksFromEBS[sourceType];

                    foreach (var item in source.Items)
                    {
                        Type childType = item.GetType();
                        if (workDict.ContainsKey(childType))
                        {
                            workDict[childType](target, item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void InvokeWorkInternal_FromEBS(s2sMessage target, Is2sMessage_Item source)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "InvokeWork_Items");

            try
            {
                Type sourceType = source.GetType();
                if (_worksFromEBS.ContainsKey(sourceType))
                {
                    TypeFromEBSWorkDictionary workDict = _worksFromEBS[sourceType];

                    object item = source.Item;
                    Type childType = item.GetType();
                    if (workDict.ContainsKey(childType))
                    {
                        workDict[childType](target, item);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void InvokeWorkInternal_FromEBS(s2sMessage target, object source, object[] sourceChild)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "InvokeWork_Items");

            try
            {

                Type sourceType = source.GetType();
                if (_worksFromEBS.ContainsKey(sourceType))
                {
                    TypeFromEBSWorkDictionary workDict = _worksFromEBS[sourceType];

                    if (sourceChild != null && sourceChild.Length > 0)
                    {
                        object item = sourceChild[0];
                        Type childType = item.GetType();
                        if (workDict.ContainsKey(childType))
                        {
                            workDict[childType](target, item);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private bool InvokeWorkInternal_ToEBS(s2sMessage target, object source, string type, XElement data)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "InvokeWork_Items");
            bool result = false;

            try
            {
                Type sourceType = source.GetType();
                if (_worksToEBS.ContainsKey(sourceType))
                {
                    StringToEBSWorkDictionary workDict = _worksToEBS[sourceType];
                    if (workDict.ContainsKey(type))
                    {
                        result = workDict[type](target, data);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }
    }
}
