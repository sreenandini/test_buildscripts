using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Collections;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Contracts.DTO.Monitor;

namespace BMC.ExComms.Server.Handlers
{
    public interface IFFMsgHandler : IDisposable, ICloneable
    {
        bool Execute(IFreeformEntity_Msg message);
        //bool ProcessMessage(FFMsg_H2G message);
    }

    internal class FFMsgHandler
        : DisposableObject, IFFMsgHandler
    {
        private class _MappingInfo
             : DisposableObject
        {
            public Type Type { get; set; }
            public FFTgtHandlerAttribute[] Attributes { get; set; }

            public override string ToString()
            {
                return this.Type.Name;
            }
        }

        public class _HandlerInfo
        {
            public IFFTgtHandler Handler { get; set; }
            public FFTgtHandlerAttribute HandlerAttribute { get; set; }
        }

        private static _MappingInfo[] _mappings = null;

        private readonly RequestResponseMapItems _requestResponseMappings = null;
        private readonly IDictionary<string, _HandlerInfo> _targetHandlers = null;
        private readonly FFTgtHandlerDeviceTypes _deviceType = FFTgtHandlerDeviceTypes.GMU;
        private readonly IFFMsgTransmitter _msgTransmitter = null;
        private readonly _FFMsgHandlerFactory _msgHandlerFactory = null;

        static FFMsgHandler()
        {
            Initialize();
        }

        public FFMsgHandler(_FFMsgHandlerFactory msgHandlerFactory,
            FFTgtHandlerDeviceTypes deviceType,
            IFFMsgTransmitter msgTransmitter)
        {
            _msgHandlerFactory = msgHandlerFactory;
            _deviceType = deviceType;
            _msgTransmitter = msgTransmitter;
            _requestResponseMappings = new RequestResponseMapItems();
            _targetHandlers = new StringDictionary<_HandlerInfo>();
            this.CreateTargetHandlers();
        }

        internal _FFMsgHandlerFactory MsgHandlerFactory
        {
            get { return _msgHandlerFactory; }
        }

        private static void Initialize()
        {
            using (ILogMethod method = Log.LogMethod("FFMsgHandler", "Initialize"))
            {
                try
                {
                    _mappings = (from t in typeof(FFMsgHandler).Assembly.GetTypes()
                                 let j = t.GetCustomAttributes(typeof(FFTgtHandlerAttribute), true).OfType<FFTgtHandlerAttribute>().ToArray()
                                 where j.Length > 0
                                 select new _MappingInfo()
                                 {
                                     Type = t,
                                     Attributes = j
                                 }).ToArray();
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        internal static string CreateSessionTargetKey(int sessionId, string typeKey)
        {
            return sessionId.ToString() + ", " + typeKey;
        }

        internal void CreateTargetHandlers()
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "CreateExecutionSteps"))
            {
                try
                {
                    var mappings = _mappings.Where((m) =>
                                            {
                                                return m.Attributes
                                                    .Where(j => j.DeviceType == _deviceType)
                                                    .Count() > 0;
                                            })
                                            .Select(m => m)
                                            .ToArray();

                    foreach (var mapping in mappings)//.AsParallel())
                    {
                        Type classType = mapping.Type;

                        foreach (var mappingAttribute in mapping.Attributes)
                        {
                            Type[] requestTypes = mappingAttribute.Request;
                            Type[] responseTypes = mappingAttribute.Response;

                            if (requestTypes != null)
                            {
                                bool hasMappings = (responseTypes != null && responseTypes.Length == requestTypes.Length);
                                for (int i = 0; i < requestTypes.Length; i++)
                                {
                                    Type requestType = requestTypes[i];
                                    _HandlerInfo handlerInfo = this.AddOrUpdate(mappingAttribute, classType, requestType);

                                    if (hasMappings)
                                    {
                                        Type responseType = responseTypes[i];
                                        string key1 = CreateSessionTargetKey(mappingAttribute.SessionId, requestType.Name);
                                        string key2 = CreateSessionTargetKey(mappingAttribute.SessionId, responseType.Name);

                                        if (!_requestResponseMappings.ContainsKey(key1))
                                        {
                                            _requestResponseMappings.Add(key1, key2, new RequestResponseMapItem());
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        private _HandlerInfo AddOrUpdate(FFTgtHandlerAttribute handlerArrtibute, Type handlerType, Type messageType)
        {
            _HandlerInfo result = null;
            string handlerKey = CreateSessionTargetKey(handlerArrtibute.SessionId, messageType.Name);

            if (!_targetHandlers.ContainsKey(handlerKey))
            {
                result = new _HandlerInfo()
                {
                    Handler = Activator.CreateInstance(handlerType) as IFFTgtHandler,
                    HandlerAttribute = handlerArrtibute,
                };
                result.Handler.MessageHandlerFactory = _msgHandlerFactory;
                _targetHandlers.Add(handlerKey, result);
            }
            else
            {
                result = _targetHandlers[handlerKey];
            }
            return result;
        }

        public bool Execute(IFreeformEntity_Msg request)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ProcessMessage(G2H)"))
            {
                bool result = default(bool);
                string ipAddress = string.Empty;

                try
                {
                    using (FFTgtExecutionContext context = new FFTgtExecutionContext(request))
                    {
                        Stack<IFreeformEntity> st = new Stack<IFreeformEntity>();
                        request.CopyTo(st);

                        // push all the grandchildren into stack and process again
                        while (st.Count != 0)
                        {
                            IFreeformEntity_MsgTgt target = st.Pop() as IFreeformEntity_MsgTgt;
                            if (target == null || target.IsLeafNode) continue;

                            _requestResponseMappings.Persist(request, target);
                            string targetKey = CreateSessionTargetKey((int)request.SessionID, target.TypeKey);
                            if (_targetHandlers.ContainsKey(targetKey))
                            {
                                _HandlerInfo info = _targetHandlers[targetKey];
                                context.HandlerAttribute = info.HandlerAttribute;
                                info.Handler.Execute(context, target);
                            }

                            target.CopyTo(st);
                        }

                        if (request.TransactionID <= 0)
                        {
                            request.TransactionID = FFMsgHandlerFactory.NewTransactionId;
                        }

                        if (request.SessionID != FF_AppId_SessionIds.Internal)
                        {
                            // ok everything processed, now do the external or internal processing
                            if (request.FlowDirection == FF_FlowDirection.G2H)
                            {
                                // add the monitor meters
                                if (context.MonitorMeters.IsValueCreated)
                                {
                                    context.MonitorTargets.Value.Add(context.MonitorMeters.Value);
                                }
                                _msgTransmitter.ProcessMessage(request as FFMsg_G2H, context.MonitorTargets.Value);
                            }
                            else if (request.FlowDirection == FF_FlowDirection.H2G)
                            {
                                _msgTransmitter.ProcessMessage(request as FFMsg_H2G);
                            }

                            // process the internal messages
                            if (context.FreeformTargets != null &&
                                context.FreeformTargets.Count > 0)
                            {
                                FFMsg_H2G h2gMessage = null;
                                foreach (var freeformEntity in context.FreeformTargets)
                                {
                                    if (freeformEntity is IFreeformEntity_Msg)
                                    {
                                        FFMsgHandlerFactory.Current.Execute(freeformEntity as IFreeformEntity_Msg);
                                    }
                                    else if (freeformEntity is IFreeformEntity_MsgTgt)
                                    {
                                        if (h2gMessage == null)
                                        {
                                            h2gMessage = FreeformEntityFactory.CreateEntity<FFMsg_H2G>(FF_FlowDirection.H2G,
                                                new FFCreateEntityRequest_H2G()
                                                {
                                                    IPAddress = request.IpAddress,
                                                    PollCode = FF_AppId_H2G_PollCodes.FreeformNoResponse,
                                                    SessionID = request.SessionID,
                                                    TransactionID = request.TransactionID,
                                                });
                                        }
                                        h2gMessage.Targets.Add(freeformEntity as IFreeformEntity_MsgTgt);
                                    }
                                }

                                if (h2gMessage != null)
                                {
                                    FFMsgHandlerFactory.Current.Execute(h2gMessage);
                                }

                            }
                        }
                    }
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
