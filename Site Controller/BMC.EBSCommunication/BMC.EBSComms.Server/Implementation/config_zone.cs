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
        internal bool Parse_configurationInfo_getZone(s2sMessage target, object source)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Parse_configurationInfo_getZone");
            bool result = default(bool);
            getConfigurationGetZone s2s = source as getConfigurationGetZone;

            try
            {
                DLZoneCollectionDto collection = _di.GetZones(target.p_body.p_configuration.propertyId, s2s.zoneId.s2sId());
                result = this.Parse_configurationInfo_getZone(target, collection);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        internal bool Parse_configurationInfo_getZone(s2sMessage target, XElement source)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Parse_configurationInfo_getZone");
            bool result = default(bool);

            try
            {
                if (source != null)
                {
                    var elements = source.GetElements("Zone");
                    if (elements != null)
                    {
                        foreach (var element in elements)
                        {
                            zone_infoUpdate dto = new zone_infoUpdate();
                            dto.zoneId = element.GetElementValue("ZoneID");
                            result = this.AddObjectToInfoUpdateData<zone_infoUpdate>(target, dto);
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

        internal bool Parse_configurationInfo_getZone(s2sMessage target, DLZoneCollectionDto collection)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Parse_configurationInfo_getZone");
            bool result = default(bool);

            try
            {
                configurationInfo ci = target.p_body.p_configuration.p_configurationInfo;
                if (collection != null)
                {
                    List<zone> Zones = new List<zone>();
                    foreach (var dto in collection)
                    {
                        Zones.Add(new zone()
                        {
                            zoneId = dto.ZoneID.ToStringSafe(),
                            zoneName = dto.ZoneName,
                            zoneActive = dto.IsActive
                        });
                    }
                    ci.zone = Zones.ToArray();
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
