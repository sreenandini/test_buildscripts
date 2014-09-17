using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.Common.ExceptionManagement;
using BMC.CoreLib.Collections;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    #region IMonEntityFactory

    public interface IMonEntityFactory : IDisposable
    {
        MonMsg_G2H CreateEntity(FFMsg_G2H ffEntityMsg);
        FFMsg_H2G CreateEntity(MonMsg_H2G monEntityMsg);
        MonMsg_G2H CreateEntity(FFMsg_G2H request, IList<MonitorEntity_MsgTgt> targets);
        MonitorEntity_MsgTgt CreateTargetEntity(IFreeformEntity_MsgTgt request);
        MonitorEntity_MsgTgt CreateTargetEntity(IMonitorEntity parent, IFreeformEntity_MsgTgt request);
        void FillTargetInfo(IMonitorEntity_MsgTgt target);
    }

    #endregion //IMonEntityFactory

    #region MonitorEntityFactory

    /// <summary>
    /// Monitor Entity Factory - Converts from freefor to Monitor Entity or viceversa
    /// </summary>
    public class MonEntityFactory :
        DisposableObject, IMonEntityFactory
    {
        #region Data Members

        private delegate MonMsg_G2H MonitorMessage_G2H_Delegate(IFreeformEntity_MsgTgt ffEntity);
        private delegate FFMsg_H2G MonitorMessage_H2G_Delegate(MonitorEntity_MsgTgt monEntity);

        private IDictionary<KeyValuePair<int, int>, Delegate> _monEntitymsg_G2H = null;
        private IDictionary<KeyValuePair<int, int>, Delegate> _monEntitymsg_H2G = null;

        private readonly IMonParser _monMsgParserG2H = null;
        private readonly IMonParser _monMsgParserH2G = null;

        private static IDictionary<string, MappingTypeG2HInfo> _tgtParserMappingTypesG2H = null;
        private IDictionary<string, IMonTgtParser> _tgtParserMappingsG2H = null;
        private IDictionary<string, IMonTgtParser> _tgtParserMappingsByTypeG2H = null;
        private IDictionary<string, MonTgtParserMappingAttribute> _monitorTypeAttributesG2H = null;

        private static IDictionary<string, MappingTypeH2GInfo> _tgtParserMappingTypesH2G = null;
        private IDictionary<string, IMonTgtParser> _tgtParserMappingsH2G = null;
        private IDictionary<string, IMonTgtParser> _tgtParserMappingsByTypeH2G = null;
        private IDictionary<string, MonTgtParserMappingAttribute> _monitorTypeAttributesH2G = null;

        private class MappingTypeInfo<T>
            : DisposableObject
            where T : MonTgtParserMappingAttribute
        {
            public Type MappingType { get; set; }
            public T MappingAttribute { get; set; }
        }

        private class MappingTypeG2HInfo
            : MappingTypeInfo<MonTgtParserMappingG2HAttribute>
        {
        }

        private class MappingTypeH2GInfo
            : MappingTypeInfo<MonTgtParserMappingH2GAttribute>
        {
        }

        #endregion //Data Members

        #region Constructor

        static MonEntityFactory()
        {
            InitializeTgtParserMappingTypesG2H();
            InitializeTgtParserMappingTypesH2G();
        }

        public MonEntityFactory()
        {
            _monMsgParserG2H = new MonMsgParser_G2H();
            _monMsgParserH2G = new MonMsgParser_H2G();
            InitializeTgtParserMappingsG2H();
            InitializeTgtParserMappingsH2G();
            FillMonitorEntityMethodDictionary();
        }

        #endregion //Constructor

        #region Freeform To Monitor Entity

        #region Common Methods

        /// <summary>
        /// To Fill the Delegate method in the monitor entity dictionary based on Target, SubTarget or FaultType, FaultSorce
        /// </summary>
        private void FillMonitorEntityMethodDictionary()
        {
            try
            {
                _monEntitymsg_G2H = new Dictionary<KeyValuePair<int, int>, Delegate>()
                {
                };

                _monEntitymsg_H2G = new Dictionary<KeyValuePair<int, int>, Delegate>() {
                { new KeyValuePair<int, int>(Convert.ToInt32(FaultSource.GIM_Event), Convert.ToInt32(FaultType_GIM.Game_Id_Info_H2G)), new MonitorMessage_H2G_Delegate(this.Create_H2G_GIM_GameInfo_Entity) },
                { new KeyValuePair<int, int>(Convert.ToInt32(FaultSource.GIM_Event), Convert.ToInt32(FaultType_GIM.Aux_Network_Enable_Disable_H2G)), new MonitorMessage_H2G_Delegate(this.Create_H2G_GIM_AuxNetworkEnableDisable) },
                { new KeyValuePair<int, int>(Convert.ToInt32(FaultSource.GIM_Event), Convert.ToInt32(FaultType_GIM.Game_Id_Request_H2G)), new MonitorMessage_H2G_Delegate(this.Create_H2G_GIM_GameIdRequest) },
             };
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private static void InitializeTgtParserMappingTypesG2H()
        {
            using (ILogMethod method = Log.LogMethod("MonEntityFactory", "InitializeMappingTypes"))
            {
                try
                {
                    _tgtParserMappingTypesG2H = new ConcurrentDictionary<string, MappingTypeG2HInfo>(StringComparer.OrdinalIgnoreCase);
                    var tgtParserMappingTypes = (from t in typeof(MonTgtParserMappingG2HAttribute).Assembly.GetTypes()
                                                 let j = t.GetCustomAttributes(typeof(MonTgtParserMappingG2HAttribute), true).OfType<MonTgtParserMappingG2HAttribute>().ToArray()
                                                 where j.Length == 1
                                                 select new
                                                 {
                                                     Type = t,
                                                     Attribute = j.FirstOrDefault()
                                                 }).ToArray();

                    foreach (var parserMappingType in tgtParserMappingTypes.AsParallel())
                    {
                        Type classType = parserMappingType.Type;
                        string key = classType.FullName;

                        if (!_tgtParserMappingTypesG2H.ContainsKey(key))
                        {
                            _tgtParserMappingTypesG2H.Add(key, new MappingTypeG2HInfo()
                            {
                                MappingType = classType,
                                MappingAttribute = parserMappingType.Attribute,
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        private static void InitializeTgtParserMappingTypesH2G()
        {
            using (ILogMethod method = Log.LogMethod("MonEntityFactory", "InitializeMappingTypes"))
            {
                try
                {
                    _tgtParserMappingTypesH2G = new ConcurrentDictionary<string, MappingTypeH2GInfo>(StringComparer.OrdinalIgnoreCase);
                    var tgtParserMappingTypes = (from t in typeof(MonTgtParserMappingH2GAttribute).Assembly.GetTypes()
                                                 let j = t.GetCustomAttributes(typeof(MonTgtParserMappingH2GAttribute), true).OfType<MonTgtParserMappingH2GAttribute>().ToArray()
                                                 where j.Length == 1
                                                 select new
                                                 {
                                                     Type = t,
                                                     Attribute = j.FirstOrDefault()
                                                 }).ToArray();

                    foreach (var parserMappingType in tgtParserMappingTypes.AsParallel())
                    {
                        Type classType = parserMappingType.Type;
                        string key = classType.FullName;

                        if (!_tgtParserMappingTypesH2G.ContainsKey(key))
                        {
                            _tgtParserMappingTypesH2G.Add(key, new MappingTypeH2GInfo()
                            {
                                MappingType = classType,
                                MappingAttribute = parserMappingType.Attribute,
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        private void InitializeTgtParserMappingsG2H()
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "InitializeMappings"))
            {
                IDictionary<string, IMonTgtParser> parserInstances = null;

                try
                {
                    _tgtParserMappingsG2H = new StringDictionary<IMonTgtParser>();
                    _tgtParserMappingsByTypeG2H = new StringDictionary<IMonTgtParser>();
                    _monitorTypeAttributesG2H = new StringDictionary<MonTgtParserMappingAttribute>();
                    parserInstances = new StringDictionary<IMonTgtParser>();

                    foreach (var mappingType in _tgtParserMappingTypesG2H.AsParallel())
                    {
                        IMonTgtParser parser = null;
                        MappingTypeG2HInfo typeInfo = mappingType.Value;
                        MonTgtParserMappingG2HAttribute attr = typeInfo.MappingAttribute;
                        string mappingTypeKey = typeInfo.MappingType.FullName;

                        if (parserInstances.ContainsKey(mappingTypeKey))
                        {
                            parser = parserInstances[mappingTypeKey] as IMonTgtParser;
                        }
                        else
                        {
                            parser = Activator.CreateInstance(typeInfo.MappingType) as IMonTgtParser;
                            if (parser != null)
                            {
                                parser.MappingAttributeG2H = typeInfo.MappingAttribute;
                                parserInstances.Add(mappingTypeKey, parser);
                            }
                        }

                        if (parser == null) continue;
                        if (!_tgtParserMappingsG2H.ContainsKey(attr.FaultSourceTypeKey))
                        {
                            _tgtParserMappingsG2H.Add(attr.FaultSourceTypeKey, parser);
                        }
                        if (attr.FreeformTargetType != null)
                        {
                            string key = attr.FreeformTargetType.FullName;
                            if (!attr.ChildKey.IsEmpty()) key += "_" + attr.ChildKey;
                            if (!_tgtParserMappingsByTypeG2H.ContainsKey(key))
                            {
                                _tgtParserMappingsByTypeG2H.Add(key, parser);
                            }
                            if (attr.MonitorTargetType != null)
                            {
                                string monitorKey = attr.MonitorTargetType.Name;
                                if (!_monitorTypeAttributesG2H.ContainsKey(monitorKey))
                                {
                                    _monitorTypeAttributesG2H.Add(monitorKey, attr);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
                finally
                {
                    if (parserInstances != null)
                    {
                        parserInstances.Clear();
                        parserInstances = null;
                    }
                }
            }
        }

        private void InitializeTgtParserMappingsH2G()
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "InitializeMappings"))
            {
                IDictionary<string, IMonTgtParser> parserInstances = null;

                try
                {
                    _tgtParserMappingsH2G = new StringDictionary<IMonTgtParser>();
                    _tgtParserMappingsByTypeH2G = new StringDictionary<IMonTgtParser>();
                    _monitorTypeAttributesH2G = new StringDictionary<MonTgtParserMappingAttribute>();
                    parserInstances = new StringDictionary<IMonTgtParser>();

                    foreach (var mappingType in _tgtParserMappingTypesH2G.AsParallel())
                    {
                        IMonTgtParser parser = null;
                        MappingTypeH2GInfo typeInfo = mappingType.Value;
                        MonTgtParserMappingH2GAttribute attr = typeInfo.MappingAttribute;
                        string mappingTypeKey = typeInfo.MappingType.FullName;

                        if (parserInstances.ContainsKey(mappingTypeKey))
                        {
                            parser = parserInstances[mappingTypeKey] as IMonTgtParser;
                        }
                        else
                        {
                            parser = Activator.CreateInstance(typeInfo.MappingType) as IMonTgtParser;
                            if (parser != null)
                            {
                                parser.MappingAttributeH2G = typeInfo.MappingAttribute;
                                parserInstances.Add(mappingTypeKey, parser);
                            }
                        }

                        if (parser == null) continue;
                        if (!_tgtParserMappingsH2G.ContainsKey(attr.FaultSourceTypeKey))
                        {
                            _tgtParserMappingsH2G.Add(attr.FaultSourceTypeKey, parser);
                        }
                        if (attr.MonitorTargetType != null)
                        {
                            if (!_tgtParserMappingsByTypeH2G.ContainsKey(attr.MonitorTargetType.FullName))
                            {
                                _tgtParserMappingsByTypeH2G.Add(attr.MonitorTargetType.FullName, parser);
                            }

                            string monitorKey = attr.MonitorTargetType.Name;
                            if (!_monitorTypeAttributesH2G.ContainsKey(monitorKey))
                            {
                                _monitorTypeAttributesH2G.Add(monitorKey, attr);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
                finally
                {
                    if (parserInstances != null)
                    {
                        parserInstances.Clear();
                        parserInstances = null;
                    }
                }
            }
        }

        /// <summary>
        /// Converts Freeform entity message to Moitor entity message
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MonMsg_G2H CreateEntity(FFMsg_G2H request)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "CreateEntity"))
            {
                MonMsg_G2H result = null;
                try
                {
                    result = _monMsgParserG2H.CreateEntity(null, request) as MonMsg_G2H;
                    if (result == null)
                    {
                        Log.Info("Unable to create the monitor message");
                        return null;
                    }

                    Stack<IFreeformEntity> st = new Stack<IFreeformEntity>();
                    request.CopyTo(st);

                    // push all the grandchildren into stack and process again
                    while (st.Count != 0)
                    {
                        IFreeformEntity child = st.Pop() as IFreeformEntity;
                        if (child.IsLeafNode) continue;

                        MonitorEntity_MsgTgt target = this.CreateTargetEntity(child as IFreeformEntity_MsgTgt);
                        if (target != null) result.AddTarget(target);

                        if (target is MonTgt_G2H_Status_CardBase)
                        {
                            result.CardNumber = (target as MonTgt_G2H_Status_CardBase).CardNumber;
                        }

                        child.CopyTo(st);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public MonMsg_G2H CreateEntity(FFMsg_G2H request, IList<MonitorEntity_MsgTgt> targets)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "CreateEntity"))
            {
                MonMsg_G2H result = null;
                try
                {
                    result = _monMsgParserG2H.CreateEntity(null, request) as MonMsg_G2H;
                    if (result == null)
                    {
                        Log.Info("Unable to create the monitor message");
                        return null;
                    }

                    foreach (var target in targets)
                    {
                        if (target != null)
                        {
                            if (target is MonTgt_G2H_Status_CardBase)
                            {
                                result.CardNumber = (target as MonTgt_G2H_Status_CardBase).CardNumber;
                            }
                            else if (target is MonTgt_G2H_Meters)
                            {
                                result.Meters = target as MonTgt_G2H_Meters;
                                continue;
                            }

                            result.Targets.Add(target);
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

        public MonitorEntity_MsgTgt CreateTargetEntity(IFreeformEntity_MsgTgt request)
        {
            return this.CreateTargetEntity(null, request);
        }

        /// <summary>
        /// Converts Freeform entity message to Moitor entity message
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MonitorEntity_MsgTgt CreateTargetEntity(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "CreateTargetEntity"))
            {
                MonitorEntity_MsgTgt result = null;

                try
                {
                    IFreeformEntity child = request;
                    if (child.IsLeafNode) return null;

                    string key = child.GetType().FullName;
                    if (!child.EntityKey.IsEmpty()) key += "_" + child.EntityKey;
                    if (_tgtParserMappingsByTypeG2H.ContainsKey(key))
                    {
                        IMonTgtParser parser = _tgtParserMappingsByTypeG2H[key];
                        MonTgtParserMappingG2HAttribute mappingAttribute = parser.MappingAttributeG2H;
                        result = parser.CreateEntity(parent, child) as MonitorEntity_MsgTgt;
                        if (result != null)
                        {
                            result.FaultSource = mappingAttribute.FaultSource;
                            if (mappingAttribute.FaultType != -1)
                                result.FaultType = mappingAttribute.FaultType;
                            result.ExtraAttribute = mappingAttribute;
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

        /// <summary>
        /// Invoke appropriate method based on Target and Sub Target Id
        /// </summary>
        /// <param name="ffEntity"></param>
        /// <returns></returns>
        private MonMsg_G2H InvokeAndReturnMonitorEntity(IFreeformEntity_MsgTgt ffEntity, int targetId)
        {
            MonMsg_G2H monMsg = null;
            try
            {
                KeyValuePair<int, int> _value = new KeyValuePair<int, int>(targetId, ffEntity.TargetID);
                if (_monEntitymsg_G2H.ContainsKey(_value))
                    monMsg = _monEntitymsg_G2H[_value].DynamicInvoke(ffEntity) as MonMsg_G2H;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return monMsg;
        }

        public void FillTargetInfo(IMonitorEntity_MsgTgt target)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "FillTargetInfo"))
            {
                try
                {
                    MonTgtParserMappingAttribute attribute = null;

                    if (target is IMonTgt_G2H)
                    {
                        attribute = _monitorTypeAttributesG2H.GetIfExists<string, MonTgtParserMappingAttribute>(target.GetType().Name);
                    }
                    else if (target is IMonTgt_H2G)
                    {
                        attribute = _monitorTypeAttributesH2G.GetIfExists<string, MonTgtParserMappingAttribute>(target.GetType().Name);
                    }

                    if (attribute != null)
                    {
                        target.FaultSource = attribute.FaultSource;
                        target.FaultType = attribute.FaultType;
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        #endregion //Common Methods

        #endregion //Freeform To Monitor Entity

        #region Monitor to Freeform Entity

        #region Common Methods

        /// <summary>
        /// Converts Monitor entity message to Freeform entity message
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Freeform entity message.</returns>
        public FFMsg_H2G CreateEntity(MonMsg_H2G request)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "CreateEntity"))
            {
                FFMsg_H2G result = null;
                try
                {
                    if (request == null || request.Targets.Count == 0)
                    {
                        Log.Info("Unable to create the freeform message (Invalid targets found).");
                        return null;
                    }

                    string key = request.FaultSourceTypeKey;

                    foreach (var monTgt in request.Targets)
                    {
                        IMonTgtParser parser = null;
                        MonTgtParserMappingH2GAttribute mappingAttribute = null;

                        //if (_tgtParserMappingsH2G.ContainsKey(key))
                        //{
                        //    parser = _tgtParserMappingsH2G[key];
                        //    mappingAttribute = parser.MappingAttributeH2G;
                        //}

                        //if (parser == null)
                        //{
                        key = monTgt.GetType().FullName;
                        if (_tgtParserMappingsByTypeH2G.ContainsKey(key))
                        {
                            parser = _tgtParserMappingsByTypeH2G[key];
                            mappingAttribute = parser.MappingAttributeH2G;
                        }
                        //}

                        if (parser != null)
                        {
                            IFreeformEntity_MsgTgt ffTgt = parser.CreateEntity(request, monTgt) as IFreeformEntity_MsgTgt;
                            if (ffTgt != null)
                            {
                                if (result == null)
                                {
                                    result = FreeformEntityFactory.CreateEntity<FFMsg_H2G>(FF_FlowDirection.H2G,
                                        new FFCreateEntityRequest_H2G()
                                        {
                                            PollCode = mappingAttribute.PollCode,
                                            SessionID = mappingAttribute.SessionID,
                                            IPAddress = request.IpAddress,
                                        });
                                    result.InstallationNo = request.InstallationNo;
                                }
                                result.Targets.Add(ffTgt);
                            }
                        }
                    }

                    if (result == null)
                    {
                        Log.Info("Unable to create the freeform message");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        private FFMsg_H2G InvokeAndReturnFreeFormEntity(MonMsg_H2G monEntity)
        {
            try
            {
                KeyValuePair<int, int> _value = new KeyValuePair<int, int>(monEntity.FaultSource, monEntity.FaultType);
                if (_monEntitymsg_G2H.ContainsKey(_value))
                {
                    if (monEntity.Targets[0] == null) return null;
                    return _monEntitymsg_G2H[_value].DynamicInvoke(monEntity.Targets[0]) as FFMsg_H2G;
                }
                return null;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        #endregion //Common Methods

        /// <summary>
        /// Create GIM GameInfo Freeform entity from Monitor GIM GameInfo Taget
        /// </summary>
        /// <param name="monTgtMsg"></param>
        /// <returns></returns>
        private FFMsg_H2G Create_H2G_GIM_GameInfo_Entity(MonitorEntity_MsgTgt monEntity)
        {
            try
            {
                MonTgt_H2G_GIM_GameIDInfo monTgtMsg = monEntity as MonTgt_H2G_GIM_GameIDInfo;
                if (monTgtMsg == null) return null;

                FFMsg_H2G ffMsg = new FFMsg_H2G();
                FFTgt_B2B_GIM ffTgtGIMMSg = new FFTgt_B2B_GIM();

                FFTgt_H2G_GIM_GameIDInfo ffTgtMsg = new FFTgt_H2G_GIM_GameIDInfo()
                {
                    SourceAddress = monTgtMsg.SourceAddress,
                    AssetNumberInt = monTgtMsg.AssetNumberInt,
                    PokerGamePrefix = monTgtMsg.PokerGamePrefix
                };

                ffTgtGIMMSg.AddTarget(ffTgtMsg);
                ffMsg.AddTarget(ffTgtGIMMSg);
                return ffMsg;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        /// <summary>
        /// Create GIM AuxNetworkEnableDisable Freeform entity from Monitor GIM AuxNetworkEnableDisable Taget
        /// </summary>
        /// <param name="monTgtMsg"></param>
        /// <returns></returns>
        private FFMsg_H2G Create_H2G_GIM_AuxNetworkEnableDisable(MonitorEntity_MsgTgt monEntity)
        {
            try
            {
                MonTgt_H2G_GIM_AuxNetworkEnableDisable monTgtMsg = monEntity as MonTgt_H2G_GIM_AuxNetworkEnableDisable;
                if (monTgtMsg == null) return null;

                FFMsg_H2G ffMsg = new FFMsg_H2G();
                FFTgt_B2B_GIM ffTgtGIMMSg = new FFTgt_B2B_GIM();

                FFTgt_H2G_GIM_AuxNetworkEnableDisable ffTgtMsg = new FFTgt_H2G_GIM_AuxNetworkEnableDisable()
                {
                    EnableDisable = monTgtMsg.EnableDisable,
                    Display = monTgtMsg.Display
                };

                ffTgtGIMMSg.AddTarget(ffTgtMsg);
                ffMsg.AddTarget(ffTgtGIMMSg);
                return ffMsg;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        /// <summary>
        /// Create GIM GameIdRequest Freeform entity from Monitor GIM GameIdRequest Target
        /// </summary>
        /// <param name="monEntity"></param>
        /// <returns></returns>
        private FFMsg_H2G Create_H2G_GIM_GameIdRequest(MonitorEntity_MsgTgt monEntity)
        {
            try
            {
                MonTgt_H2G_GIM_GameIDRequest monTgtMsg = monEntity as MonTgt_H2G_GIM_GameIDRequest;
                if (monTgtMsg == null) return null;

                FFMsg_H2G ffMsg = new FFMsg_H2G();
                FFTgt_B2B_GIM ffTgtGIMMSg = new FFTgt_B2B_GIM();

                FFTgt_H2G_GIM_GameIDRequest ffTgtMsg = new FFTgt_H2G_GIM_GameIDRequest() { };

                ffTgtGIMMSg.AddTarget(ffTgtMsg);
                ffMsg.AddTarget(ffTgtGIMMSg);
                return ffMsg;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        #endregion //Monitor to Freeform Entity
    }

    #endregion //MonitorEntityFactory

    #region MonitorEntityFactory Wrapper

    /// <summary>
    /// Monitor Entity Factory Wrapper exposed for Freeform Executor to invoke Monitor Entity Factory
    /// </summary>
    public static class MonitorEntityFactory
    {
        #region Data Member

        private static readonly SingletonHelperBase<IMonEntityFactory> _singletonHelper = new SingletonThreadHelper<IMonEntityFactory>(
                new Lazy<IMonEntityFactory>(() => new MonEntityFactory()));

        #endregion //Data Member

        #region Properties

        private static IMonEntityFactory Factory
        {
            get { return _singletonHelper.Current; }
        }

        #endregion //Properties

        #region Static Methods

        public static MonitorEntity_MsgTgt CreateTargetEntity(IFreeformEntity_MsgTgt request)
        {
            return Factory.CreateTargetEntity(request);
        }

        public static MonitorEntity_MsgTgt CreateTargetEntity(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            return Factory.CreateTargetEntity(parent, request);
        }

        public static MonMsg_G2H CreateEntity(FFMsg_G2H request, IList<MonitorEntity_MsgTgt> targets)
        {
            return Factory.CreateEntity(request, targets);
        }

        public static MonMsg_G2H CreateEntity(FFMsg_G2H ffEntityMsg)
        {
            return Factory.CreateEntity(ffEntityMsg);
        }

        public static FFMsg_H2G CreateEntity(MonMsg_H2G monEntityMsg)
        {
            return Factory.CreateEntity(monEntityMsg);
        }

        public static void FillTargetInfo(this IMonitorEntity_MsgTgt target)
        {
            Factory.FillTargetInfo(target);
        }

        #endregion //Static Methods
    }

    #endregion //MonitorEntityFactory Wrapper
}
