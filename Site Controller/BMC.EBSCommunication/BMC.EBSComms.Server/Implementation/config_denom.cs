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
        internal bool Parse_configurationInfo_getDenomination(s2sMessage target, object source)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Parse_configurationInfo_getDenomination");
            bool result = default(bool);
            getConfigurationGetDenomination s2s = source as getConfigurationGetDenomination;

            try
            {
                DLDenominationCollectionDto collection = _di.GetDenominations(target.p_body.p_configuration.propertyId, s2s.denomId.s2sStringId());
                result = this.Parse_configurationInfo_getDenomination(target, collection);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        internal bool Parse_configurationInfo_getDenomination(s2sMessage target, XElement source)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Parse_configurationInfo_getDenomination");
            bool result = default(bool);

            try
            {
                if (source != null)
                {
                    var elements = source.GetElements("Denomination");
                    if (elements != null)
                    {
                        foreach (var element in elements)
                        {
                            denomination_infoUpdate dto = new denomination_infoUpdate();
                            dto.denominationId = element.GetElementValue("DenominationId");
                            result = this.AddObjectToInfoUpdateData<denomination_infoUpdate>(target, dto);
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

        internal bool Parse_configurationInfo_getDenomination(s2sMessage target, DLDenominationCollectionDto collection)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Parse_configurationInfo_getDenomination");
            bool result = default(bool);

            try
            {
                configurationInfo ci = target.p_body.p_configuration.p_configurationInfo;
                if (collection != null)
                {
                    List<denomination> denominations = new List<denomination>();
                    foreach (var dto in collection)
                    {
                        denominations.Add(new denomination()
                        {
                            denominationId = dto.DenominationId,
                            denominationName = dto.DenominationName,
                            denominationValue = dto.DenominationValue,
                            denominationActive = dto.IsActive,
                        });
                    }
                    ci.denomination = denominations.ToArray();
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
