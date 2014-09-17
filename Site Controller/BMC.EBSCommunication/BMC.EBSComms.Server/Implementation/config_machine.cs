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
        internal bool Parse_configurationInfo_getMachine(s2sMessage target, object source)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Parse_configurationInfo_getMachine");
            bool result = default(bool);
            getConfigurationGetMachine s2s = source as getConfigurationGetMachine;

            try
            {
                DLMachineCollectionDto collection = _di.GetMachines(target.p_body.p_configuration.propertyId, s2s.machineId.s2sStringId());
                result = this.Parse_configurationInfo_getMachine(target, collection);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        internal bool Parse_configurationInfo_getMachine(s2sMessage target, XElement source)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Parse_configurationInfo_getMachine");
            bool result = default(bool);

            try
            {
                if (source != null)
                {
                    var elements = source.GetElements("Machine");
                    if (elements != null)
                    {
                        foreach (var element in elements)
                        {
                            machine_infoUpdate dto = new machine_infoUpdate();
                            dto.machineId = element.GetElementValue("MachineID");
                            result = this.AddObjectToInfoUpdateData<machine_infoUpdate>(target, dto);
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

        internal bool Parse_configurationInfo_getMachine(s2sMessage target, DLMachineCollectionDto collection)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Parse_configurationInfo_getMachine");
            bool result = default(bool);

            try
            {
                configurationInfo ci = target.p_body.p_configuration.p_configurationInfo;
                if (collection != null)
                {
                    List<machine> Machines = new List<machine>();
                    foreach (var dto in collection)
                    {
                        Machines.Add(new machine()
                        {
                            machineId = dto.MachineID,
                            areaId = dto.Area,
                            gameId = dto.GameName,
                            machineLoc = dto.MachineLoc,
                            machineActive = dto.IsActive,
                            denominationId = dto.DenominationID,
                            manufacturerId = dto.ManufacturerName,
                            casinoId = dto.CasinoID,
                            machineType = dto.MachineType,
                            zoneId = dto.ZoneID,
                            bankId = dto.Bank
                        });
                    }
                    ci.machine = Machines.ToArray();
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
