using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using BMC.EBSComms.Contracts.Dto;
using BMC.EBSComms.DataLayer.Dto;

namespace BMC.EBSComms.Server
{
    public partial class EBSCommServer
    {
        internal bool Parse_configurationInfo_getSite(s2sMessage target, object source)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Parse_configurationInfo_getSite");
            bool result = default(bool);
            getConfigurationGetCasino s2s = source as getConfigurationGetCasino;

            try
            {
                DLSiteCollectionDto collection = _di.GetSites(target.p_body.p_configuration.propertyId);
                result = this.Parse_configurationInfo_getSite(target, collection);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        internal bool Parse_configurationInfo_getSite(s2sMessage target, XElement source)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Parse_configurationInfo_getSite");
            bool result = default(bool);

            try
            {
                if (source != null)
                {
                    var elements = source.GetElements("Site");
                    if (elements != null)
                    {
                        foreach (var element in elements)
                        {
                            casino_infoUpdate dto = new casino_infoUpdate();
                            dto.casinoId = element.GetElementValue("SiteId");
                            result = this.AddObjectToInfoUpdateData<casino_infoUpdate>(target, dto);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        internal bool Parse_configurationInfo_getSite(s2sMessage target, DLSiteCollectionDto collection)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Parse_getConfiguration");
            bool result = default(bool);

            try
            {
                configurationInfo ci = target.p_body.p_configuration.p_configurationInfo;
                if (collection != null)
                {
                    List<casino> Casinos = new List<casino>();
                    foreach (var dto in collection)
                    {
                        Casinos.Add(new casino()
                        {
                            casinoId = dto.SiteId,
                            casinoName = dto.SiteName,
                            casinoActive = dto.IsActive
                        });
                    }
                    ci.casino = Casinos.ToArray();
                    result = true;
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
