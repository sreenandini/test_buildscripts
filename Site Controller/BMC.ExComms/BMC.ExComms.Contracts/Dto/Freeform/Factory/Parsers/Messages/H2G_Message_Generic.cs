using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFCreateEntityRequest_H2G 
        : FFCreateEntityRequest
    {
        public FF_AppId_H2G_PollCodes PollCode { get; set; }
        public FF_AppId_SessionIds SessionID { get; set; }
        public int TransactionID { get; set; }
    }

    public class FFCreateEntityRequest_H2G_Secured
        : FFCreateEntityRequest_H2G
    {
        public FFCreateEntityRequest_H2G_Secured()
            : base() { this.IsSecured = true; }
    }

    public class FFCreateEntityRequest_H2G_ResponseRequired
        : FFCreateEntityRequest_H2G
    {
        public FFCreateEntityRequest_H2G_ResponseRequired()
            : base() { this.IsResponseRequired = true; }
    }

    public class FFCreateEntityRequest_H2G_ResponseRequired_Secured
        : FFCreateEntityRequest_H2G
    {
        public FFCreateEntityRequest_H2G_ResponseRequired_Secured()
            : base()
        {
            this.IsResponseRequired = true;
            this.IsSecured = true;
        }
    }

    internal class FFParser_MsgFactory_Generic_H2G 
        : FFParser_MsgFactory_Generic_B2B
    {
        internal FFParser_MsgFactory_Generic_H2G(FFParserDictionary subParsers, IFFTgtParser targetParser)
            : base(subParsers, targetParser) { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            // get and verify the pollCode
            FF_AppId_H2G_PollCodes pollCode = buffer[FreeformConstants.IDX_SEND_POLLCODE].GetAppId<FF_GmuId_H2G_PollCodes, FF_AppId_H2G_PollCodes>();
            if (pollCode == FF_AppId_H2G_PollCodes.None)
            {
                Log.Warning("Invalid message passed (Invalid pollCode)");
                return null;
            }

            // create the entity and parse
            int iPollCode = (int)pollCode;
            entity = this.GetParserFromAppId(iPollCode).ParseBuffer(rootEntity, buffer);

            // valid message
            return entity;
        }

        internal override void ParseEntityInternal(IFreeformEntity entity, ref List<byte> buffer)
        {
            // parse the entity and add
            int iPollCode = (int)(entity as FFMsg_H2G).PollCode;
            buffer.AddRange(this.GetParserFromAppId(iPollCode).ParseEntity(entity));
        }

        internal override IFreeformEntity CreateEntityInternal(FFCreateEntityRequest request)
        {
            if (!(request is FFCreateEntityRequest_H2G)) return null;

            // create the entity and parse
            FFCreateEntityRequest_H2G request2 = request as FFCreateEntityRequest_H2G;
            int iPollCode = (int)request2.PollCode;
            FFMsg_H2G msg = this.GetParserFromAppId(iPollCode).CreateEntity() as FFMsg_H2G;
            msg.IsSecured = request2.IsSecured;
            msg.IsResponseRequired = request2.IsResponseRequired;
            msg.IpAddress = request.IPAddress;
            msg.PollCode = (FF_AppId_H2G_PollCodes)iPollCode;
            msg.SessionID = request2.SessionID;
            msg.TransactionID = request2.TransactionID;
            return msg;
        }
    }

    internal abstract class FFParser_Msg_NoSegs_Generic_H2G 
        : FFMsgParser
    {
        protected IFFParser_MsgFactory_Generic_B2B _parentParser = null;

        internal FFParser_Msg_NoSegs_Generic_H2G(IFFParser_MsgFactory_Generic_B2B parentParser)
        {
            _parentParser = parentParser;
        }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            // get and verify the session id
            bool isResponseRequired = false;
            int actualSessionId = 0;
            FF_AppId_SessionIds sessionId = this.GetSessionId(buffer, FreeformConstants.IDX_SEND_SESSION_ID, ref actualSessionId, ref isResponseRequired);
            if (sessionId == FF_AppId_SessionIds.None)
            {
                Log.Warning("Invalid message passed (Invalid session)");
                return null;
            }

            // create the message
            entity = this.CreateEntity();
            FFMsg_H2G msg = entity as FFMsg_H2G;
            if (msg != null)
            {
                int offset = 0;
                msg.IsResponseRequired = isResponseRequired;
                this.OnParseBufferPart1(msg, buffer, sessionId, ref offset);
                this.OnParseBufferPart2(msg, buffer, ref offset);
                this.OnParseBufferPart3(msg, buffer, ref offset);
            }

            msg.IsResponseRequired = isResponseRequired;
            msg.ActualSessionID = actualSessionId;
            return msg;
        }

        protected virtual void OnParseBufferPart1(FFMsg_H2G msg, byte[] buffer, FF_AppId_SessionIds sessionId, ref int offset)
        {
            msg.DeviceType = (FF_GmuId_DeviceTypes)buffer[offset++];
            msg.PollCode = buffer.GetBytesToNumberUInt8(offset++, 1).GetAppId<FF_GmuId_H2G_PollCodes, FF_AppId_H2G_PollCodes>();
            msg.SessionID = sessionId;
            offset++;
            msg.TransactionID = buffer.GetBytesToNumberUInt8(offset++, 1);
        }

        protected virtual void OnParseBufferPart2(FFMsg_H2G msg, byte[] buffer, ref int offset) { }

        protected virtual void OnParseBufferPart3(FFMsg_H2G msg, byte[] buffer, ref int offset)
        {
            msg.DataLength = buffer.GetValue<ushort>(offset, _parentParser.DataLength);
            offset += _parentParser.DataLength;
            msg.EntityData = buffer.GetValue<byte[]>(offset, msg.DataLength);
            offset += msg.DataLength;

            msg.Checksum = buffer.GetBytesToNumberUInt8(-1, 1);
            msg.ChecksumCalculated = FreeformHelper.CalculateCheckSum(buffer, 0, buffer.Length - 1);
        }

        internal override void ParseEntityInternal(IFreeformEntity entity, ref List<byte> buffer)
        {
            FFMsg_H2G msg = entity as FFMsg_H2G;
            if (msg != null)
            {
                if (msg.SessionID == FF_AppId_SessionIds.Override)
                {
                    buffer.Clear();
                    buffer.AddRange(this.TargetParser.ParseTarget(msg));
                }
                else
                {
                    this.OnParseEntityPart1(msg, ref buffer);
                    this.OnParseEntityPart2(msg, ref buffer);
                    this.OnParseEntityPart3(msg, ref buffer);
                }
            }
        }

        protected virtual void OnParseEntityPart1(FFMsg_H2G msg, ref List<byte> buffer)
        {
            buffer.Add((byte)msg.DeviceType);
            buffer.Add(msg.PollCode.GetGmuIdInt8());
            buffer.Add(msg.SessionID.GetGmuIdInt8().CreateRequestResponseId(msg.IsResponseRequired));
            buffer.Add((byte)msg.TransactionID);
        }

        protected virtual void OnParseEntityPart2(FFMsg_H2G msg, ref List<byte> buffer) { }

        protected virtual void OnParseEntityPart3(FFMsg_H2G msg, ref List<byte> buffer)
        {
            msg.EntityData = this.TargetParser.ParseTarget(msg);
            if (msg.EntityData == null) msg.EntityData = new byte[msg.DataLength];
            msg.DataLength = (ushort)msg.EntityData.Length;
            buffer.SetValue(msg.DataLength, _parentParser.DataLength);
            buffer.AddRange(msg.EntityData);
            buffer.CalculateAndStoreChecksum();
        }
    }

    internal abstract class FFParser_Msg_Segs_Generic_H2G 
        : FFParser_Msg_NoSegs_Generic_H2G
    {
        internal FFParser_Msg_Segs_Generic_H2G(IFFParser_MsgFactory_Generic_B2B parentParser)
            : base(parentParser) { }

        protected override void OnParseBufferPart2(FFMsg_H2G msg, byte[] buffer, ref int offset)
        {
            FFMsg_H2G_1 msg2 = msg as FFMsg_H2G_1;
            msg2.SegmentNumber = buffer.GetValue<ushort>(offset, 2);
            offset += 2;
            msg2.SegmentCount = buffer.GetValue<ushort>(offset, 2);
            offset += 2;
        }

        protected override void OnParseEntityPart2(FFMsg_H2G msg, ref List<byte> buffer)
        {
            FFMsg_H2G_1 msg2 = msg as FFMsg_H2G_1;
            buffer.SetValue(msg2.SegmentCount, 2);
            buffer.SetValue(msg2.SegmentNumber, 2);
        }
    }

    internal class FFParser_Msg_Generic_H2G_1 
        : FFParser_Msg_Segs_Generic_H2G
    {
        internal FFParser_Msg_Generic_H2G_1(IFFParser_MsgFactory_Generic_B2B parentParser)
            : base(parentParser) { }

        public override IFreeformEntity CreateEntity()
        {
            return new FFMsg_H2G_1();
        }
    }

    internal class FFParser_Msg_Generic_H2G_2 
        : FFParser_Msg_Segs_Generic_H2G
    {
        internal FFParser_Msg_Generic_H2G_2(IFFParser_MsgFactory_Generic_B2B parentParser)
            : base(parentParser) { }

        public override IFreeformEntity CreateEntity()
        {
            return new FFMsg_H2G_2();
        }
    }

    internal class FFParser_Msg_Generic_H2G_3 
        : FFParser_Msg_NoSegs_Generic_H2G
    {
        internal FFParser_Msg_Generic_H2G_3(IFFParser_MsgFactory_Generic_B2B parentParser)
            : base(parentParser) { }

        public override IFreeformEntity CreateEntity()
        {
            return new FFMsg_H2G_3();
        }
    }
}
