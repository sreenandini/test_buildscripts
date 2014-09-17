using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    internal interface IMonParser : IDisposable
    {
        IMonitorEntity CreateEntity(IMonitorEntity parent, IFreeformEntity request);
        IFreeformEntity CreateEntity(IMonitorEntity parent, IMonitorEntity request);
    }

    internal interface IMonMsgParser : IMonParser
    {
        //IMonTgtParser TargetParser { get; set; }
    }

    internal interface IMonTgtParser : IMonParser
    {
        MonTgtParserMappingG2HAttribute MappingAttributeG2H { get; set; }
        MonTgtParserMappingH2GAttribute MappingAttributeH2G { get; set; }
    }

    internal abstract class MonParser
        : DisposableObject, IMonParser
    {
        public IMonitorEntity CreateEntity(IMonitorEntity parent, IFreeformEntity request)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "CreateEntity"))
            {
                IMonitorEntity result = default(IMonitorEntity);

                try
                {
                    result = this.CreateEntityInternal(parent, request);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        protected abstract IMonitorEntity CreateEntityInternal(IMonitorEntity parent, IFreeformEntity request);

        public IFreeformEntity CreateEntity(IMonitorEntity parent, IMonitorEntity request)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "CreateEntity"))
            {
                IFreeformEntity result = default(IFreeformEntity);

                try
                {
                    result = this.CreateEntityInternal(parent, request);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        protected abstract IFreeformEntity CreateEntityInternal(IMonitorEntity parent, IMonitorEntity request);        
    }
}
