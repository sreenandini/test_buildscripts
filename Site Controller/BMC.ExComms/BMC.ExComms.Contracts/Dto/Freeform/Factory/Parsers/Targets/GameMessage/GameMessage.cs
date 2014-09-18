using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib.Collections;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_GameMessage
        : FFTgtParser_NoSubTargets
    {
        internal FFParser_Tgt_Generic_GameMessage()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_GameMessage
        : FFParser_Tgt_Generic_GameMessage
    {
        internal FFParser_Tgt_MC300_GameMessage()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_GameMessage_G2H
        : FFParser_Tgt_MC300_GameMessage
    {
        private static IDictionary<int, Func<FFTgt_G2H_GameMessage_SASCommand>> _createCommands = null;

        static FFParser_Tgt_MC300_GameMessage_G2H()
        {
            _createCommands = new IntDictionary<Func<FFTgt_G2H_GameMessage_SASCommand>>()
            {
                { (int)FF_AppId_LongPollCodes.LPC_LongPoll_51, ()=>{return new FFTgt_G2H_GM_SAS_TotalGames();} },
            };
        }

        internal FFParser_Tgt_MC300_GameMessage_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            if (buffer.Length == 1)
            {
                return new FFTgt_G2H_GameMessage_ResponseAckNack()
                {
                    Status = buffer[0].GetAppId<FF_GmuId_GameMessage_ResponseTypes, FF_AppId_GameMessage_ResponseTypes>(),
                };
            }
            else if (buffer.Length >= 3)
            {
                FF_AppId_GameMessage_ProtocolTypes protocolType = buffer[0].GetAppId<FF_GmuId_GameMessage_ProtocolTypes, FF_AppId_GameMessage_ProtocolTypes>();
                FF_AppId_GameMessage_GameResponses responseType = buffer[1].GetAppId<FF_GmuId_GameMessage_GameResponses, FF_AppId_GameMessage_GameResponses>();

                if (responseType == FF_AppId_GameMessage_GameResponses.Ignore)
                {
                    return new FFTgt_G2H_GameMessage_MessageNotSent()
                    {
                        ProtocolType = protocolType,
                        IsGameResponseExpected = responseType,
                        MessageData = new byte[] { 0 },
                    };
                }
                else
                {
                    if (buffer[2] == 0)
                    {
                        return new FFTgt_G2H_GameMessage_MessageResponseTimeout()
                        {
                            ProtocolType = protocolType,
                            IsGameResponseExpected = responseType,
                            MessageData = new byte[] { 0 },
                        };
                    }
                    else if (protocolType == FF_AppId_GameMessage_ProtocolTypes.SAS)
                    {
                        int longPollCommand = (int)buffer[2];
                        if (_createCommands.ContainsKey(longPollCommand))
                        {
                            byte[] messageData = null;
                            if (buffer.Length > 3)
                                messageData = buffer.CopyToBuffer(3, buffer.Length - 3);
                            var target = _createCommands[longPollCommand]();
                            target.IsGameResponseExpected = responseType;
                            target.FromRawData(messageData);
                            return target;
                        }
                        else
                        {
                            var target = new FFTgt_G2H_GameMessage_SASCommand()
                            {
                                ProtocolType = protocolType,
                                IsGameResponseExpected = responseType,
                                LongPollCommand = buffer[2],
                            };
                            if (buffer.Length > 3)
                            {
                                target.MessageData = buffer.CopyToBuffer(3, buffer.Length - 3);
                            }
                            return target;
                        }
                    }
                }
            }
            return null;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_B2B_GameMessage2 tgtSrc = tgt as FFTgt_B2B_GameMessage2;
            buffer.Add(tgtSrc.ProtocolType.GetGmuIdInt8());
            buffer.Add(tgtSrc.IsGameResponseExpected.GetGmuIdInt8());
            if (tgtSrc is FFTgt_G2H_GameMessage_SASCommand)
            {
                buffer.Add((byte)(tgtSrc as FFTgt_G2H_GameMessage_SASCommand).LongPollCommand);
            }
            byte[] rawData = tgtSrc.ToRawData();
            if (rawData != null &&
                rawData.Length > 0)
            {
                buffer.AddRange(rawData);
            }
            if (tgtSrc.MessageData != null &&
                tgtSrc.MessageData.Length > 0)
            {
                buffer.AddRange(tgtSrc.MessageData);
            }
        }
    }

    internal class FFParser_Tgt_MC300_GameMessage_H2G
        : FFParser_Tgt_MC300_GameMessage
    {
        private static IDictionary<int, Func<FFTgt_H2G_GameMessage_SASCommand>> _createCommands = null;

        static FFParser_Tgt_MC300_GameMessage_H2G()
        {
            _createCommands = new IntDictionary<Func<FFTgt_H2G_GameMessage_SASCommand>>()
            {
                { (int)FF_AppId_LongPollCodes.LPC_LongPoll_51, () => { return new FFTgt_H2G_GM_SAS_TotalGames(); } },
            };
        }

        internal FFParser_Tgt_MC300_GameMessage_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            if (buffer.Length >= 3)
            {
                FF_AppId_GameMessage_ProtocolTypes protocolType = buffer[0].GetAppId<FF_GmuId_GameMessage_ProtocolTypes, FF_AppId_GameMessage_ProtocolTypes>();
                FF_AppId_GameMessage_GameResponses responseType = buffer[1].GetAppId<FF_GmuId_GameMessage_GameResponses, FF_AppId_GameMessage_GameResponses>();
                FFTgt_H2G_GameMessage_MessageRequest target2 = null;

                if (protocolType == FF_AppId_GameMessage_ProtocolTypes.SAS)
                {
                    int longPollCommand = (int)buffer[2];
                    if (_createCommands.ContainsKey(longPollCommand))
                        target2 = _createCommands[longPollCommand]();
                    target2.IsGameResponseExpected = responseType;
                    if (buffer.Length > 3)
                        target2.MessageData = buffer.CopyToBuffer(3, buffer.Length - 3);
                }
                else
                {
                    var target = new FFTgt_H2G_GameMessage_MessageRequest();
                    target2 = target;
                    target.IsGameResponseExpected = responseType;
                    target.MessageData = buffer.CopyToBuffer(2, buffer.Length - 2);
                }
                return target2;
            }
            return null;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_GameMessage_MessageRequest tgtSrc = tgt as FFTgt_H2G_GameMessage_MessageRequest;
            buffer.Add(tgtSrc.ProtocolType.GetGmuIdInt8());
            buffer.Add(tgtSrc.IsGameResponseExpected.GetGmuIdInt8());
            if (tgtSrc is FFTgt_H2G_GameMessage_SASCommand)
            {
                buffer.Add((byte)(tgtSrc as FFTgt_H2G_GameMessage_SASCommand).LongPollCommand);
            }
            byte[] rawData = tgtSrc.ToRawData();
            if (rawData != null &&
                rawData.Length > 0)
            {
                buffer.AddRange(rawData);
            }
            if (tgtSrc.MessageData != null &&
                tgtSrc.MessageData.Length > 0)
            {
                buffer.AddRange(tgtSrc.MessageData);
            }
        }
    }
}
