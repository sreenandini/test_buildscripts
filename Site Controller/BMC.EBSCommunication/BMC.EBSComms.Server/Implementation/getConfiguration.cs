using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using BMC.EBSComms.Contracts.Dto;
using BMC.EBSComms.DataLayer.Dto;

namespace BMC.EBSComms.Server
{
    public partial class EBSCommServer
    {
        internal void AddConfigurationInfo(s2sMessage target)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "AddConfiguration");

            try
            {
                // set the target configuration
                configuration conf = target.p_body.p_configuration;
                if (conf == null)
                {
                    conf = new configuration();
                    conf.propertyId = target.p_propertyId;
                    target.p_body.p_configuration = conf;
                    target.p_body.Items = new object[] { conf };
                }

                // set the target configuration info
                configurationInfo confInfo = conf.p_configurationInfo;
                if (confInfo == null)
                {
                    confInfo = new configurationInfo();                    
                    conf.p_configurationInfo = confInfo;                    
                    conf.Item = confInfo;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        internal infoUpdateData AddConfigurationInfoUpdate(s2sMessage target)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "AddConfiguration");
            infoUpdateData infoUpdateData = null;

            try
            {
                // set the target configuration info
                infoUpdate infoUpdate = target.p_body.p_infoUpdate;
                if (infoUpdate == null)
                {
                    infoUpdate = new infoUpdate();                    
                    target.p_body.p_infoUpdate = infoUpdate;
                    target.p_body.Items = new object[] { infoUpdate };
                }

                // set infoupdate data
                infoUpdateData = infoUpdate.p_infoUpdateData;
                if (infoUpdateData == null)
                {
                    infoUpdateData = new infoUpdateData();
                    infoUpdateData.className = "configurationInfo";
                    infoUpdate.p_infoUpdateData = infoUpdateData;
                    infoUpdate.Item = infoUpdateData;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return infoUpdateData;
        }

        internal bool Parse_getConfiguration(s2sMessage target, object source)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Parse_getConfiguration");
            bool result = default(bool);
            getConfiguration s2s = source as getConfiguration;

            try
            {
                s2s.ParseAnyElements();

                this.AddConfigurationInfo(target);
                this.InvokeWorkInternal_FromEBS(target, s2s, s2s.getCasino);
                this.InvokeWorkInternal_FromEBS(target, s2s, s2s.getDenomination);
                this.InvokeWorkInternal_FromEBS(target, s2s, s2s.getManufacturer);
                this.InvokeWorkInternal_FromEBS(target, s2s, s2s.getGame);
                this.InvokeWorkInternal_FromEBS(target, s2s, s2s.getMachine);
                this.InvokeWorkInternal_FromEBS(target, s2s, s2s.getZone);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }
        
        private bool AddObjectToInfoUpdateData<T>(s2sMessage target, T obj)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "AddObjectToInfoUpdateData");

            try
            {
                if (obj != null)
                {
                    infoUpdateData infoUpdateData = this.AddConfigurationInfoUpdate(target);
                    infoUpdateData.Value = this.ConvertObjectToXml(obj, false, false);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return true;
        }
    }
}
