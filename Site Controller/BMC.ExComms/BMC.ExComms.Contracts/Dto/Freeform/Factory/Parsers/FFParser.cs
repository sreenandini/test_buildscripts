using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Collections;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public sealed class FFParserItemInfo : DisposableObject
    {
        public IFFParser Parser { get; set; }

        public FFTgtParseBufferHandler Action { get; set; }
    }

    public class FFParserItem : DisposableObject
    {
        private Lazy<FFParserItemInfo> _anyParserInfo = null;
        private Lazy<FFParserItemInfo> _gmuParserInfo = null;
        private Lazy<FFParserItemInfo> _hostParserInfo = null;

        private static Func<FFParserItemInfo> CREATE_INFO = () => { return new FFParserItemInfo(); };
        public FFParserItem()
        {
            _anyParserInfo = new Lazy<FFParserItemInfo>(CREATE_INFO);
            _gmuParserInfo = new Lazy<FFParserItemInfo>(CREATE_INFO);
            _hostParserInfo = new Lazy<FFParserItemInfo>(CREATE_INFO);
        }

        public int GmuId { get; set; }

        public int AppId { get; set; }

        public FFParserItemInfo this[FF_FlowInitiation type]
        {
            get
            {
                if (type == FF_FlowInitiation.Any)
                    return _anyParserInfo.Value;
                else if (type == FF_FlowInitiation.Host)
                    return _hostParserInfo.Value;
                else if (type == FF_FlowInitiation.Gmu)
                    return _gmuParserInfo.Value;
                else
                    return null;
            }
        }

        public IFFParser GetParser(FF_FlowInitiation type)
        {
            if (type == FF_FlowInitiation.Any)
                return (_anyParserInfo.Value.Parser ??
                        _hostParserInfo.Value.Parser ??
                        _gmuParserInfo.Value.Parser);
            else if (type == FF_FlowInitiation.Host)
                return _hostParserInfo.Value.Parser;
            else if (type == FF_FlowInitiation.Gmu)
                return _gmuParserInfo.Value.Parser;
            else
                return null;
        }

        public override string ToString()
        {
            return string.Format("{0:D}=>{1:D}", this.GmuId, this.AppId);
        }
    }

    public class FFParserDictionary : IntDictionary<FFParserItem>
    {
        public virtual void AddParserItem(FF_FlowInitiation flowInitiation, int gmuId, int appId, IFFParser parser, FFTgtParseBufferHandler action)
        {
            using (ILogMethod method = Log.LogMethod("FFParserDictionary", "AddTargetParser"))
            {
                try
                {
                    FFParserItem item = null;

                    if (!this.ContainsKey(gmuId))
                    {
                        this.Add(gmuId, (item = new FFParserItem()));
                    }
                    else
                    {
                        item = this[gmuId];
                    }

                    item.GmuId = gmuId;
                    item.AppId = appId;

                    FFParserItemInfo itemInfo = item[flowInitiation];
                    itemInfo.Parser = parser;
                    itemInfo.Action = action;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }
    }

    public interface IFFParser : IDisposable
    {
        FFParserDictionary SubParsers { get; }
        FF_FlowInitiation FlowInitiation { get; }
        bool SkipTargetInfo { get; }

        void AddBufferEntityParser(int gmuId, int appId, IFFParser parser);
        void AddBufferEntityParser(int gmuId, int appId, FFTgtParseBufferHandler action);
        void AddBufferEntityParser(int gmuId, int appId, IFFParser parser, FFTgtParseBufferHandler action);

        void AddBufferEntityParser(Type gmuIdEnum, Type appIdEnum, IFFParser parser);
        void AddBufferEntityParser(Type gmuIdEnum, Type appIdEnum, FFTgtParseBufferHandler action);
        void AddBufferEntityParser(Type gmuIdEnum, Type appIdEnum, IFFParser parser, FFTgtParseBufferHandler action);

        int GetGmuIdFromAppId(int appId);
        int GetAppIdFromGmuId(int gmuId);

        FFParserItem GetParserItemFromGmuId(int gmuId);
        FFParserItem GetParserItemFromAppId(int appId);

        IFFParser GetParserFromAppId(int appId);
        IFFParser GetParserFromAppId(int appId, out int gmuId);

        IFreeformEntity ParseBuffer(IFreeformEntity rootEntity, byte[] buffer);
        IFreeformEntity ParseBuffer(IFreeformEntity rootEntity, int id, byte[] buffer);
        IFreeformEntity ParseBuffer(IFreeformEntity parent, IFreeformEntity rootEntity, byte[] buffer, int offset, int length);

        IFreeformEntity CreateEntity(FFCreateEntityRequest request);
        IFreeformEntity CreateEntity();

        byte[] ParseEntity(IFreeformEntity entity);
        byte[] ParseTarget(IFreeformEntity entity);
        void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer);
    }

    public interface IFFMsgParser : IFFParser
    {
        IFFTgtParser TargetParser { get; set; }
    }

    public interface IFFTgtParser : IFFParser
    {
        bool HasSubTargets { get; }
    }

    public interface IFFEnumParser : IFFParser { }

    internal abstract class FFParser : DisposableObject, IFFParser
    {
        protected IFFFactory _factory = null;
        protected FFParserDictionary _subParsers = null;
        protected IntDictionary<int> _parserMappings = null;

        internal FFParser() { }

        internal FFParser(IFFFactory factory)
        {
            _factory = factory;
        }

        internal FFParser(FFParserDictionary subParsers)
        {
            _subParsers = subParsers;
        }

        public FFParserDictionary SubParsers
        {
            get { return _subParsers; }
        }

        public virtual FF_FlowInitiation FlowInitiation
        {
            get { return FF_FlowInitiation.Any; }
        }

        public virtual bool SkipTargetInfo
        {
            get { return false; }
        }

        public int GetGmuIdFromAppId(int appId)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "GetBufferIdFromEntityId"))
            {
                int result = default(int);

                try
                {
                    if (_parserMappings.ContainsKey(appId))
                    {
                        result = _parserMappings[appId];
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public int GetAppIdFromGmuId(int gmuId)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "GetBufferIdFromEntityId"))
            {
                int result = default(int);

                try
                {
                    if (_subParsers.ContainsKey(gmuId))
                    {
                        result = _subParsers[gmuId].AppId;
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public virtual FFParserItem GetParserItemFromGmuId(int gmuId)
        {
            if (_subParsers != null &&
                _subParsers.ContainsKey(gmuId))
                return _subParsers[gmuId];
            return null;
        }

        public virtual FFParserItem GetParserItemFromAppId(int appId)
        {
            int gmuId = 0;
            return this.GetParserItemFromAppId(appId, out gmuId);
        }

        public virtual FFParserItem GetParserItemFromAppId(int appId, out int gmuId)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "GetParserFromEntityId"))
            {
                gmuId = 0;
                FFParserItem parserItem = default(FFParserItem);

                try
                {
                    if (_parserMappings.ContainsKey(appId))
                    {
                        parserItem = _subParsers[_parserMappings[appId]];
                    }
                    else
                    {
                        parserItem = _subParsers[-1];
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return parserItem;
            }
        }

        public virtual IFFParser GetParserFromAppId(int appId)
        {
            int gmuId = 0;
            return this.GetParserFromAppId(appId, out gmuId);
        }

        public virtual IFFParser GetParserFromAppId(int appId, out int gmuId)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "GetParserFromEntityId"))
            {
                gmuId = 0;
                IFFParser result = default(IFFParser);

                try
                {
                    FFParserItem parserItem = null;
                    if (_parserMappings.ContainsKey(appId))
                    {
                        parserItem = _subParsers[_parserMappings[appId]];
                    }
                    else
                    {
                        parserItem = _subParsers[-1];
                    }
                    gmuId = parserItem.GmuId;
                    result = parserItem[this.FlowInitiation].Parser;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public virtual void AddBufferEntityParser(Type gmuIdEnum, Type appIdEnum, IFFParser parser)
        {
            this.AddBufferEntityParser(gmuIdEnum, appIdEnum, parser, null);
        }

        public virtual void AddBufferEntityParser(Type gmuIdEnum, Type appIdEnum, FFTgtParseBufferHandler action)
        {
            this.AddBufferEntityParser(gmuIdEnum, appIdEnum, null, action);
        }

        public virtual void AddBufferEntityParser(Type gmuIdEnum, Type appIdEnum, IFFParser parser, FFTgtParseBufferHandler action)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "AddBufferEntityParser"))
            {
                try
                {
                    //Array gmuIdValues = Enum.GetValues(gmuIdEnum);
                    //Array appIdValues = Enum.GetValues(appIdEnum);

                    //if (gmuIdValues.Length == appIdValues.Length)
                    //{
                    //    for (int i = 0; i < gmuIdValues.Length; i++)
                    //    {
                    //        this.AddBufferEntityParser((int)gmuIdValues.GetValue(i), (int)appIdValues.GetValue(i), parser, action);
                    //    }
                    //}
                    string[] gmuIdNames = Enum.GetNames(gmuIdEnum);
                    foreach (var gmuIdName in gmuIdNames)
                    {
                        try
                        {
                            object gmuIdValue = Enum.Parse(gmuIdEnum, gmuIdName, true);
                            object appIdValue = Enum.Parse(appIdEnum, gmuIdName, true);
                            this.AddBufferEntityParser((int)gmuIdValue, (int)appIdValue, parser, action);
                        }
                        catch (Exception ex)
                        {
                            method.InfoV("::: ENUM ERROR : Unable to convert from ({0}) to ({1})", gmuIdEnum.FullName, appIdEnum.FullName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        //public void AddBufferEntityParser<G, A>(G gmuIdEnum, A appIdEnum, IFFParser parser)
        //{
        //    this.AddBufferEntityParser(gmuIdEnum, appIdEnum, parser, null);
        //}
        //public void AddBufferEntityParser<G, A>(G gmuIdEnum, A appIdEnum, FFTgtParseBufferHandler action)
        //{
        //    this.AddBufferEntityParser(gmuIdEnum, appIdEnum, null, action);
        //}
        //public void AddBufferEntityParser<G, A>(G gmuIdEnum, A appIdEnum, IFFParser parser, FFTgtParseBufferHandler action)
        //{
        //    using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "AddBufferEntityParser"))
        //    {
        //        try
        //        {
        //            //Array gmuIdValues = Enum.GetValues(gmuIdEnum);
        //            //Array appIdValues = Enum.GetValues(appIdEnum);

        //            //if (gmuIdValues.Length == appIdValues.Length)
        //            //{
        //            //    for (int i = 0; i < gmuIdValues.Length; i++)
        //            //    {
        //            //        this.AddBufferEntityParser((int)gmuIdValues.GetValue(i), (int)appIdValues.GetValue(i), parser, action);
        //            //    }
        //            //}
        //            Array gmuIdNames = Enum.GetNames(gmuIdEnum);
        //            foreach (var gmuIdName in gmuIdNames.OfType<string>())
        //            {
        //                int appIdValue = 0;
        //                Enum.TryParse<int>(gmuIdName, out appIdValue);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            method.Exception(ex);
        //        }
        //    }
        //}

        public virtual void AddBufferEntityParser(int gmuId, int appId, IFFParser parser)
        {
            this.AddBufferEntityParser(gmuId, appId, parser, null);
        }

        public virtual void AddBufferEntityParser(int gmuId, int appId, FFTgtParseBufferHandler action)
        {
            this.AddBufferEntityParser(gmuId, appId, null, action);
        }

        public virtual void AddBufferEntityParser(int gmuId, int appId, IFFParser parser, FFTgtParseBufferHandler action)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "AddBufferEntityParser"))
            {
                try
                {
                    FF_FlowInitiation flowDirection = this.FlowInitiation;
                    if (_subParsers == null)
                    {
                        _subParsers = new FFParserDictionary();
                    }
                    if (_parserMappings == null)
                    {
                        _parserMappings = new IntDictionary<int>();
                    }

                    if (parser != null && parser.FlowInitiation != FF_FlowInitiation.Any)
                        flowDirection = parser.FlowInitiation;

                    _subParsers.AddParserItem(flowDirection, gmuId, appId, parser, action);
                    if (appId != -1 &&
                        !_parserMappings.ContainsKey(appId))
                    {
                        _parserMappings.Add(appId, gmuId);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        protected void AddSubParsers()
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "AddTargetParsers"))
            {
                try
                {
                    this.AddSubParsersInternal();
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        protected virtual void AddSubParsersInternal() { }

        public IFreeformEntity CreateEntity(FFCreateEntityRequest request)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Parse"))
            {
                IFreeformEntity result = default(IFreeformEntity);

                try
                {
                    result = this.CreateEntityInternal(request);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        internal virtual IFreeformEntity CreateEntityInternal(FFCreateEntityRequest request) { return null; }

        public virtual IFreeformEntity CreateEntity() { return null; }

        public IFreeformEntity ParseBuffer(IFreeformEntity rootEntity, byte[] buffer)
        {
            return this.ParseBuffer(rootEntity, 0, buffer);
        }

        public IFreeformEntity ParseBuffer(IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Parse"))
            {
                IFreeformEntity result = default(IFreeformEntity);

                try
                {
                    result = this.ParseBufferInternal(ref result, rootEntity, id, buffer);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        internal abstract IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer);

        public IFreeformEntity ParseBuffer(IFreeformEntity parent, IFreeformEntity rootEntity, byte[] buffer, int offset, int length)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Parse"))
            {
                IFreeformEntity result = default(IFreeformEntity);

                try
                {
                    result = this.ParseBufferInternal(parent, rootEntity, ref result, buffer, offset, length);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        protected virtual IFreeformEntity ParseBufferInternal(IFreeformEntity parent, IFreeformEntity rootEntity, ref IFreeformEntity entity, byte[] buffer, int offset, int length) { return null; }

        public byte[] ParseEntity(IFreeformEntity entity)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Parse"))
            {
                List<byte> result = new List<byte>();

                try
                {
                    this.ParseEntityInternal(entity, ref result);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result.ToArray();
            }
        }

        internal abstract void ParseEntityInternal(IFreeformEntity entity, ref List<byte> buffer);

        public byte[] ParseTarget(IFreeformEntity entity)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ParseTarget"))
            {
                List<byte> result = new List<byte>();

                try
                {
                    if (entity.Targets != null &&
                        entity.Targets.Count > 0)
                    {
                        foreach (var target in entity.Targets)
                        {
                            byte[] data = this.ParseEntity(target);
                            if (data != null)
                            {
                                result.AddRange(data);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result.ToArray();
            }
        }

        public virtual void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer) { }
    }

    internal abstract class FFMsgParser : FFParser, IFFMsgParser
    {
        internal FFMsgParser() { }

        internal FFMsgParser(FFParserDictionary subParsers)
            : base(subParsers) { }

        internal FFMsgParser(FFParserDictionary subParsers, IFFTgtParser targetParser)
            : this(subParsers)
        {
            this.TargetParser = targetParser;
        }

        public IFFTgtParser TargetParser { get; set; }

        protected virtual FF_AppId_SessionIds GetSessionId(byte[] buffer, int offset, ref int actualSessionId, ref bool isResponseRequired)
        {
            // get and verify the session id
            int sessionIdInt = buffer[offset];
            if ((sessionIdInt & FreeformConstants.FF_ISRESPONSEREQUIRED) != 0)
            {
                actualSessionId = sessionIdInt;
                sessionIdInt &= ~FreeformConstants.FF_ISRESPONSEREQUIRED;
                isResponseRequired = true;
            }
            return ((byte)sessionIdInt).GetAppId<FF_GmuId_SessionIds, FF_AppId_SessionIds>();
        }
    }

    internal abstract class FFTgtParser : FFParser, IFFTgtParser
    {
        private static readonly int SECURITY_ID = 0;

        static FFTgtParser()
        {
            SECURITY_ID = ((int)FFEnumParserFactory.GetGmuId<FF_AppId_TargetIds, FF_GmuId_TargetIds>(FF_AppId_TargetIds.Security)).CreateRequestResponseId(true);
        }

        internal FFTgtParser() { }

        internal FFTgtParser(FFParserDictionary subParsers)
            : base(subParsers) { }

        public virtual bool HasSubTargets
        {
            get
            {
                return true;
            }
        }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            throw new NotImplementedException();
        }

        protected override IFreeformEntity ParseBufferInternal(IFreeformEntity parent, IFreeformEntity rootEntity, ref IFreeformEntity entity, byte[] buffer, int offset, int length)
        {
            if (length == 0) return null;
            int sessionId = -1;

            if (buffer != null)
            {
                if (length < 0)
                    length = buffer.Length;
                bool hasSubTargets = this.HasSubTargets;

                while (offset < length)
                {
                    int id = buffer[offset];
                    int combinedId = id;
                    byte length2 = 0;
                    byte[] buffer2 = null;

                    if (rootEntity != null &&
                        rootEntity is IFreeformEntity_Msg)
                    {
                        combinedId = rootEntity.CreateCombinedId(id, false);
                    }

                    if (HasSubTargets)
                    {
                        length2 = buffer[++offset]; // we may get zero length buffer
                        if (length2 > 0)
                        {
                            buffer2 = new byte[length2];
                            Buffer.BlockCopy(buffer, ++offset, buffer2, 0, buffer2.Length);
                        }
                        else
                        {
                            offset += 1;
                            continue;
                        }
                    }
                    else
                    {
                        buffer2 = new byte[buffer.Length - 1];
                        Buffer.BlockCopy(buffer, ++offset, buffer2, 0, buffer2.Length);
                    }

                    if (buffer2 != null)
                    {
                        IFreeformEntity_MsgTgt target = null;
                        bool isSecured = false;
                        bool isResponseRequired = false;
                        bool isResponseRequired2 = false;
                        FFParserItem parserItem = this.GetParserItem(rootEntity, id);
                        int appId = id;

                        // not found, then try it with combined id
                        if (parserItem == null)
                        {
                            parserItem = this.GetParserItem(rootEntity, combinedId);
                        }

                        // found
                        if (parserItem != null)
                        {
                            // check if response required
                            id = id.GetRequestResponseId(out isResponseRequired);

                            // check if the packet is secured
                            appId = parserItem.AppId.GetRequestResponseId(out isResponseRequired2);

                            FFTgtParseBufferHandler action = parserItem[this.FlowInitiation].Action;
                            if (action != null)
                            {
                                target = action(parent as IFreeformEntity_MsgTgt, id, length2, buffer2);
                            }
                            else
                            {
                                IFFParser parser = parserItem.GetParser(this.FlowInitiation);
                                if (parser != null)
                                {
                                    target = parser.ParseBuffer(rootEntity, appId, buffer2) as IFreeformEntity_MsgTgt;
                                }
                            }
                        }

                        // secured target
                        if (target is FFTgt_B2B_Encrypted)
                        {
                            rootEntity.EncryptedTarget = target;
                            isSecured = true;
                        }

                        entity = target;
                        if (target != null)
                        {
                            target.TargetID = appId;
                            target.TargetLength = length2;
                            target.EntityData = buffer2;
                            target.IsSecured = isSecured;
                            target.IsResponseRequired = isResponseRequired;
                            if (parent != null) parent.Targets.Add(target);
                            if (target is IFreeformEntity_MsgTgt_Primary &&
                                rootEntity != null) rootEntity.EntityPrimaryTarget = target;
                        }
                    }

                    if (hasSubTargets)
                        offset += length2;
                    else
                        break;
                }
            }

            return entity;
        }

        protected virtual FFParserItem GetParserItem(IFreeformEntity rootEntity, int id)
        {
            return this.GetParserItemFromGmuId(id);
        }

        internal override void ParseEntityInternal(IFreeformEntity entity, ref List<byte> buffer)
        {
            IFreeformEntity_MsgTgt tgt = entity as IFreeformEntity_MsgTgt;
            IFreeformEntity_MsgTgt tgt2 = tgt;

            if (tgt2 != null)
            {
                // has raw data inside the target
                if (tgt2 is IFFTgt_Override)
                {
                    buffer.AddRange(tgt2.ToRawData());
                    return;
                }

                int gmuParserId = this.GetTargetId(tgt, out tgt2);
                int gmuTargetId = gmuParserId.ExtractCombinedId();
                bool isSecured = tgt2.IsSecured;
                bool hasSubTargets = this.HasSubTargets;
                byte[] buffer2 = null;
                IFFParser parser = this;
                IFFParser parserSecured = null;

                FFParserItem parserItem = this.GetParserItem(null, gmuParserId);
                if (parserItem == null)
                {
                    if (tgt2.Parent != null &&
                        tgt2.Parent is IFreeformEntity_Msg)
                    {
                        int id = FreeformHelper.CreateCombinedId(tgt2.Parent.UniqueEntityId.GetGmuIdInt8(), gmuParserId);
                        parserItem = this.GetParserItem(null, id);
                    }
                }

                // found?
                if (parserItem != null)
                {
                    // parser
                    IFFParser parser2 = parserItem.GetParser(this.FlowInitiation);
                    if (parser2 != null)
                    {
                        if (tgt2.Targets != null &&
                            tgt2.Targets.Count > 0)
                        {
                            buffer2 = parser2.ParseTarget(tgt2);
                        }
                        else
                        {
                            parser = parser2;
                        }
                    }

                    // secured parser
                    if (isSecured)
                    {
                        int securityId = FreeformHelper.CreateCombinedId(entity.Parent as IFreeformEntity_Msg, SECURITY_ID, false);
                        FFParserItem parserItem2 = this.GetParserItem(null, securityId);
                        if (parserItem2 != null)
                        {
                            parserSecured = parserItem2.GetParser(this.FlowInitiation);
                        }
                    }
                }

                bool skipTargetInfo = parser.SkipTargetInfo;
                if (buffer2 == null)
                {
                    List<byte> data = new List<byte>();
                    parser.GetTargetData(tgt2, ref data);
                    buffer2 = data.ToArray();
                }

                // encrypt the target
                if (isSecured &&
                    parserSecured != null)
                {
                    List<byte> bufferToEncrypt = new List<byte>();
                    this.AddTargetToBuffer(ref bufferToEncrypt, (byte)gmuTargetId, buffer2, true, false);

                    parserSecured.GetTargetData(tgt2, ref bufferToEncrypt);
                    buffer2 = bufferToEncrypt.ToArray();
                    gmuTargetId = SECURITY_ID;
                    hasSubTargets = true;
                    skipTargetInfo = false;
                }

                this.AddTargetToBuffer(ref buffer, (byte)gmuTargetId, buffer2, hasSubTargets, skipTargetInfo);
            }
        }

        protected virtual int GetTargetId(IFreeformEntity_MsgTgt tgt, out IFreeformEntity_MsgTgt tgtActual)
        {
            tgtActual = tgt;

            // without parent
            int id = tgt.CreateCombinedId(null);
            if (_parserMappings.ContainsKey(id))
            {
                return _parserMappings[id];
            }

            // with parent
            id = tgt.CreateCombinedId(tgt.Parent as IFreeformEntity_Msg);
            if (_parserMappings.ContainsKey(id))
            {
                return _parserMappings[id];
            }

            // not found the target
            if (tgt.Parent != null &&
                tgt.Parent is IFreeformEntity_MsgTgt)
            {
                Stack<IFreeformEntity_MsgTgt> st = new Stack<IFreeformEntity_MsgTgt>();
                st.Push(tgt.Parent as IFreeformEntity_MsgTgt);

                while (st.Count != 0)
                {
                    IFreeformEntity_MsgTgt tgt2 = st.Pop();
                    if (_parserMappings.ContainsKey(tgt2.EntityId))
                    {
                        tgtActual = tgt2;
                        return _parserMappings[tgt2.EntityId];
                    }
                    if (tgt2.Parent != null &&
                        tgt2.Parent is IFreeformEntity_MsgTgt) st.Push(tgt2.Parent as IFreeformEntity_MsgTgt);
                }
            }
            return 0;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer) { }

        protected virtual void AddTargetToBuffer(ref List<byte> buffer, byte id, byte[] data)
        {
            this.AddTargetToBuffer(ref buffer, id, data, false);
        }

        protected virtual void AddTargetToBuffer(ref List<byte> buffer, byte id, byte[] data, bool skipTargetInfo)
        {
            this.AddTargetToBuffer(ref buffer, id, data, this.HasSubTargets, skipTargetInfo);
        }

        protected virtual void AddTargetToBuffer(ref List<byte> buffer, byte id, byte[] data, bool hasSubTargets, bool skipTargetInfo)
        {
            if (data == null) data = new byte[0];

            if (!skipTargetInfo)
            {
                buffer.Add(id);
                if (hasSubTargets) buffer.Add((byte)data.Length);
            }
            buffer.AddRange(data);
        }

        protected virtual void AddTargetToBuffer(ref List<byte> buffer, byte id, params byte[][] datas)
        {
            int idxLen = 0;
            byte length = 0;
            buffer.Add(id);
            idxLen = buffer.Count;
            buffer.Add(0);
            foreach (var data in datas)
            {
                buffer.AddRange(data);
                length += (byte)data.Length;
            }
            buffer[idxLen] = length;
        }
    }

    internal abstract class FFTgtParser_Gmu : FFTgtParser, IFFTgtParser
    {
        internal FFTgtParser_Gmu() { }

        internal FFTgtParser_Gmu(FFParserDictionary subParsers)
            : base(subParsers) { }

        public override FF_FlowInitiation FlowInitiation
        {
            get
            {
                return FF_FlowInitiation.Gmu;
            }
        }
    }

    internal abstract class FFTgtParser_Host : FFTgtParser, IFFTgtParser
    {
        internal FFTgtParser_Host() { }

        internal FFTgtParser_Host(FFParserDictionary subParsers)
            : base(subParsers) { }

        public override FF_FlowInitiation FlowInitiation
        {
            get
            {
                return FF_FlowInitiation.Host;
            }
        }
    }

    internal abstract class FFTgtParser_NoSubTargets : FFTgtParser
    {
        public override bool HasSubTargets
        {
            get
            {
                return false;
            }
        }
    }

    internal class FFEnumParser : FFParser, IFFEnumParser
    {
        internal FFEnumParser() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer) { return null; }

        internal override void ParseEntityInternal(IFreeformEntity entity, ref List<byte> buffer) { }
    }
}
