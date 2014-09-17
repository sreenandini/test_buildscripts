using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Collections;
using BMC.CoreLib.Diagnostics;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Server.Handlers.Targets;

namespace BMC.ExComms.Server.Handlers
{
    public class FreeformHandlerFactory 
        : DisposableObject, IFreeformHandler
    {
        private readonly IDictionary<int, IFreeformHandler> _handlers = null;
        private readonly IFreeformHandler _genericHandler = null;

        public FreeformHandlerFactory()
        {
            _handlers = new ConcurrentDictionary<int, IFreeformHandler>();
            //_genericHandler = new FreeformHandler_GIM();
            RegisterHandlers();
        }

        private void RegisterHandlers()
        {
            ModuleProc PROC = new ModuleProc("FreeformHandlerFactory", "RegisterHandlers");

            try
            {
                Type genericHandlerType = typeof (FreeformHandler_Generic);

                // entities marked with FreeformHandlerAttribute
                var markedEntities = (from i in typeof(FreeformHandlerFactory).Assembly.GetTypes()
                                      where i.IsClass
                                      from c in i.GetCustomAttributes(typeof(FreeformHandlerAttribute), true).OfType<FreeformHandlerAttribute>()
                                      select new
                                      {
                                          Attr = c,
                                          InterfaceType = i
                                      }).ToArray();

                if (markedEntities != null)
                {
                    // update the interface type and keys
                    foreach (var markedEntity in markedEntities)
                    {
                        FreeformHandlerAttribute attr = markedEntity.Attr;
                        Type ifaceType = markedEntity.InterfaceType;
                        this.AddHandler(attr.SessionId, ifaceType);        
                    }
                }

                // remaining ids
                foreach (var enumValue in Enum.GetValues(typeof(FF_AppId_SessionIds)))
                {
                    int sessionId = (int) enumValue;
                    if (!_handlers .ContainsKey(sessionId))
                    {
                        this.AddHandler(sessionId, genericHandlerType);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void AddHandler(int sessionId, Type ifaceType)
        {
            IFreeformHandler handler = Activator.CreateInstance(ifaceType) as IFreeformHandler;
            if (handler != null &&
                !_handlers.ContainsKey(sessionId))
            {
                _handlers.Add(sessionId, handler);
            }
        }

        public bool ProcessMessage(FFMsg_G2H message)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ProcessMessage(G2H)"))
            {
                bool result = default(bool);

                try
                {
                    var sessionId = (int)message.SessionID;
                    result = _handlers.ContainsKey(sessionId) ? 
                        _handlers[sessionId].ProcessMessage(message) : 
                        _genericHandler.ProcessMessage(message);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public bool ProcessMessage(FFMsg_H2G message)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ProcessMessage(H2G)"))
            {
                bool result = default(bool);

                try
                {
                    var sessionId = (int)message.SessionID;
                    result = _handlers.ContainsKey(sessionId) ?
                        _handlers[sessionId].ProcessMessage(message) :
                        _genericHandler.ProcessMessage(message);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }
    }
}
