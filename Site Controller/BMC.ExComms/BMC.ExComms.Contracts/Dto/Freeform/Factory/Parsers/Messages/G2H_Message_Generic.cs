using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFCreateEntityRequest_G2H : FFCreateEntityRequest
    {
        public FF_AppId_G2H_MessageTypes MessageType { get; set; }
        public FF_AppId_G2H_Commands Command { get; set; }
        public FF_AppId_SessionIds SessionID { get; set; }
        public int TransactionID { get; set; }
    }

    public class FFCreateEntityRequest_G2H_Secured
        : FFCreateEntityRequest_G2H
    {
        public FFCreateEntityRequest_G2H_Secured()
            : base() { this.IsSecured = true; }
    }

    public class FFCreateEntityRequest_G2H_ResponseRequired
        : FFCreateEntityRequest_G2H
    {
        public FFCreateEntityRequest_G2H_ResponseRequired()
            : base() { this.IsResponseRequired = true; }
    }

    internal class FFParser_MsgFactory_Generic_G2H : FFParser_MsgFactory_Generic_B2B
    {
        internal FFParser_MsgFactory_Generic_G2H(FFParserDictionary subParsers, IFFTgtParser targetParser)
            : base(subParsers, targetParser) { }

        protected virtual FF_AppId_G2H_Commands GetCommand(byte[] buffer)
        {
            return (FF_AppId_G2H_Commands)this.GetAppIdFromGmuId((int)buffer[FreeformConstants.IDX_RECV_COMMAND]);
        }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            // get and verify the command
            FF_AppId_G2H_Commands command = GetCommand(buffer);
            if (command == FF_AppId_G2H_Commands.None)
            {
                Log.Warning("Invalid message passed (Invalid command)");
                return null;
            }

            // create the entity and parse
            IFFParser parser = this.GetParserFromAppId((int)command);
            entity = parser.ParseBuffer(rootEntity, buffer);

            // valid message
            return entity;
        }

        internal override void ParseEntityInternal(IFreeformEntity entity, ref List<byte> buffer)
        {
            // parse the entity and add
            int iCommand = (int)(entity as FFMsg_G2H).Command;
            buffer.AddRange(this.GetParserFromAppId(iCommand).ParseEntity(entity));
        }

        internal override IFreeformEntity CreateEntityInternal(FFCreateEntityRequest request)
        {
            if (!(request is FFCreateEntityRequest_G2H)) return null;

            // create the entity and parse
            FFCreateEntityRequest_G2H request2 = request as FFCreateEntityRequest_G2H;
            int iCommand = (int)request2.Command;
            FFMsg_G2H msg = this.GetParserFromAppId(iCommand).CreateEntity() as FFMsg_G2H;
            msg.IsSecured = request2.IsSecured;
            msg.IsResponseRequired = request2.IsResponseRequired;
            msg.IpAddress = request2.IPAddress;
            msg.Command = (FF_AppId_G2H_Commands)iCommand;
            msg.MessageType = request2.MessageType;
            msg.SessionID = request2.SessionID;
            msg.TransactionID = request2.TransactionID;
            return msg;
        }
    }

    internal abstract class FFParser_Msg_NoSegs_Generic_G2H : FFMsgParser
    {
        protected IFFParser_MsgFactory_Generic_B2B _parentParser = null;

        internal FFParser_Msg_NoSegs_Generic_G2H(IFFParser_MsgFactory_Generic_B2B parentParser)
        {
            _parentParser = parentParser;
        }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            // get and verify the session id
            bool isResponseRequired = false;
            int actualSessionId = 0;
            FF_AppId_SessionIds sessionId = this.GetSessionId(buffer, FreeformConstants.IDX_RECV_SESSION_ID, ref actualSessionId, ref isResponseRequired);
            if (sessionId == FF_AppId_SessionIds.None)
            {
                Log.Warning("Invalid message passed (Invalid session)");
                return null;
            }

            // create the message
            entity = this.CreateEntity();
            FFMsg_G2H msg = entity as FFMsg_G2H;
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

        protected virtual void OnParseBufferPart1(FFMsg_G2H msg, byte[] buffer, FF_AppId_SessionIds sessionId, ref int offset)
        {
            msg.DeviceType = (FF_GmuId_DeviceTypes)buffer[offset++];
            msg.MessageType = buffer.GetBytesToNumberUInt8(offset++, 1).GetAppId<FF_GmuId_G2H_MessageTypes, FF_AppId_G2H_MessageTypes>();
            msg.Command = buffer.GetBytesToNumberUInt8(offset++, 1).GetAppId<FF_GmuId_G2H_Commands, FF_AppId_G2H_Commands>();
            msg.SessionID = sessionId;
            offset++;
            msg.TransactionID = buffer.GetBytesToNumberUInt8(offset++, 1);
        }

        protected virtual void OnParseBufferPart2(FFMsg_G2H msg, byte[] buffer, ref int offset) { }

        protected virtual void OnParseBufferPart3(FFMsg_G2H msg, byte[] buffer, ref int offset)
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
            FFMsg_G2H msg = entity as FFMsg_G2H;
            if (msg != null)
            {
                this.OnParseEntityPart1(msg, ref buffer);
                this.OnParseEntityPart2(msg, ref buffer);
                this.OnParseEntityPart3(msg, ref buffer);

                // Prefix the Ip address
                byte[] addressBytes = msg.IpAddress2.GetAddressBytes();
                buffer.InsertRange(0, addressBytes);
            }
        }

        protected virtual void OnParseEntityPart1(FFMsg_G2H msg, ref List<byte> buffer)
        {
            buffer.Add((byte)msg.DeviceType);
            buffer.Add(msg.MessageType.GetGmuIdInt8());
            buffer.Add(msg.Command.GetGmuIdInt8());
            buffer.Add(msg.SessionID.GetGmuIdInt8().CreateRequestResponseId(msg.IsResponseRequired));
            buffer.Add((byte)msg.TransactionID);
        }

        protected virtual void OnParseEntityPart2(FFMsg_G2H msg, ref List<byte> buffer) { }

        protected virtual void OnParseEntityPart3(FFMsg_G2H msg, ref List<byte> buffer)
        {
            msg.EntityData = this.TargetParser.ParseTarget(msg);
            if (msg.EntityData == null) msg.EntityData = new byte[msg.DataLength];
            msg.DataLength = (ushort)msg.EntityData.Length;

#if !DATA_LEN_RECV_1
            buffer.SetValue(msg.DataLength, 2);
#else
            buffer.SetValue(msg.DataLength, 1);
#endif
            buffer.AddRange(msg.EntityData);
            buffer.CalculateAndStoreChecksum();
        }
    }

    internal abstract class FFParser_Msg_Segs_Generic_G2H : FFParser_Msg_NoSegs_Generic_G2H
    {
        internal FFParser_Msg_Segs_Generic_G2H(IFFParser_MsgFactory_Generic_B2B parentParser)
            : base(parentParser) { }

        protected override void OnParseBufferPart2(FFMsg_G2H msg, byte[] buffer, ref int offset)
        {
            FFMsg_G2H_1 msg2 = msg as FFMsg_G2H_1;
            msg2.SegmentNumber = buffer.GetValue<ushort>(offset, 2);
            offset += 2;
            msg2.SegmentCount = buffer.GetValue<ushort>(offset, 2);
            offset += 2;
        }

        protected override void OnParseEntityPart2(FFMsg_G2H msg, ref List<byte> buffer)
        {
            FFMsg_G2H_1 msg2 = msg as FFMsg_G2H_1;
            buffer.SetValue(msg2.SegmentCount, 2);
            buffer.SetValue(msg2.SegmentNumber, 2);
        }
    }

    internal class FFParser_Msg_Generic_G2H_1 : FFParser_Msg_Segs_Generic_G2H
    {
        internal FFParser_Msg_Generic_G2H_1(IFFParser_MsgFactory_Generic_B2B parentParser)
            : base(parentParser) { }

        public override IFreeformEntity CreateEntity()
        {
            return new FFMsg_G2H_1();
        }
    }

    internal class FFParser_Msg_Generic_G2H_2 : FFParser_Msg_Segs_Generic_G2H
    {
        internal FFParser_Msg_Generic_G2H_2(IFFParser_MsgFactory_Generic_B2B parentParser)
            : base(parentParser) { }

        public override IFreeformEntity CreateEntity()
        {
            return new FFMsg_G2H_2();
        }
    }

    internal class FFParser_Msg_Generic_G2H_3 : FFParser_Msg_NoSegs_Generic_G2H
    {
        internal FFParser_Msg_Generic_G2H_3(IFFParser_MsgFactory_Generic_B2B parentParser)
            : base(parentParser) { }

        public override IFreeformEntity CreateEntity()
        {
            return new FFMsg_G2H_3();
        }
    }
}
