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
        internal bool Parse_configurationInfo_getManufacturer(s2sMessage target, object source)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Parse_configurationInfo_getManufacturer");
            bool result = default(bool);
            getConfigurationGetManufacturer s2s = source as getConfigurationGetManufacturer;

            try
            {
                DLManufacturerCollectionDto collection = _di.GetManufacturers(target.p_body.p_configuration.propertyId, s2s.manufacturerId.s2sId());
                result = this.Parse_configurationInfo_getManufacturer(target, collection);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        internal bool Parse_configurationInfo_getManufacturer(s2sMessage target, XElement source)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Parse_configurationInfo_getManufacturer");
            bool result = default(bool);

            try
            {
                if (source != null)
                {
                    var elements = source.GetElements("Manufacturer");
                    if (elements != null)
                    {
                        foreach (var element in elements)
                        {
                            manufacturer_infoUpdate dto = new manufacturer_infoUpdate();
                            dto.manufacturerId = element.GetElementValue("ManufacturerID");
                            result = this.AddObjectToInfoUpdateData<manufacturer_infoUpdate>(target, dto);
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

        internal bool Parse_configurationInfo_getManufacturer(s2sMessage target, DLManufacturerCollectionDto collection)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Parse_configurationInfo_getManufacturer");
            bool result = default(bool);

            try
            {
                configurationInfo ci = target.p_body.p_configuration.p_configurationInfo;
                if (collection != null)
                {
                    List<manufacturer> manufacturers = new List<manufacturer>();
                    foreach (var dto in collection)
                    {
                        manufacturers.Add(new manufacturer()
                        {
                            manufacturerId = dto.ManufacturerId.ToString(),
                            manufacturerName = dto.ManufacturerName,
                            manufacturerValue = dto.ManufacturerValue,
                            manufacturerActive = dto.IsActive,
                        });
                    }
                    ci.manufacturer = manufacturers.ToArray();
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
