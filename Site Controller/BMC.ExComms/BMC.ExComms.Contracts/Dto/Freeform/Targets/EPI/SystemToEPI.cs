using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_H2G_SystemToEPI
        : FFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TargetIds.SystemToEPIDisplayMessage;
            }
        }

        public FFTgt_H2G_SystemToEPI_Data SystemToEPIData { get; set; }
    }

    public class FFTgt_H2G_SystemToEPI_Data
        : FFTgt_H2G
    {
        public byte MessageAction { get; set; }
        public byte TextLength { get; set; }
        public string Text { get; set; }
    }

    public abstract class FFTgt_H2G_SystemToEPI_MessageType1
        : FFTgt_H2G_SystemToEPI_Data
    {
        public FF_GmuId_SystemToEPI_MessageAction1 MessageActionEnum
        {
            get { return (FF_GmuId_SystemToEPI_MessageAction1)this.MessageAction; }
            set { this.MessageAction = (byte)value; }
        }
    }

    public abstract class FFTgt_H2G_SystemToEPI_MessageType2
        : FFTgt_H2G_SystemToEPI_Data
    {
        public FF_GmuId_SystemToEPI_MessageAction2 MessageActionEnum
        {
            get { return (FF_GmuId_SystemToEPI_MessageAction2)this.MessageAction; }
            set { this.MessageAction = (byte)value; }
        }
    }

    public abstract class FFTgt_H2G_SystemToEPI_MessageType3
        : FFTgt_H2G_SystemToEPI_Data
    {
    }

    public class FFTgt_H2G_SystemToEPI_TicketPrint
        : FFTgt_H2G_SystemToEPI_MessageType1
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_SystemToEPI_MessageTypes.TicketPrint;
            }
        }
    }

    public class FFTgt_H2G_SystemToEPI_TicketRedeem
        : FFTgt_H2G_SystemToEPI_MessageType1
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_SystemToEPI_MessageTypes.TicketRedeem;
            }
        }
    }

    public class FFTgt_H2G_SystemToEPI_TicketError
        : FFTgt_H2G_SystemToEPI_MessageType1
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_SystemToEPI_MessageTypes.TicketError;
            }
        }
    }

    public class FFTgt_H2G_SystemToEPI_Promo
        : FFTgt_H2G_SystemToEPI_MessageType2
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_SystemToEPI_MessageTypes.Promo;
            }
        }
    }

    public class FFTgt_H2G_SystemToEPI_Sports
        : FFTgt_H2G_SystemToEPI_MessageType2
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_SystemToEPI_MessageTypes.Sports;
            }
        }
    }

    public class FFTgt_H2G_SystemToEPI_F5
        : FFTgt_H2G_SystemToEPI_MessageType3
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_SystemToEPI_MessageTypes.F5;
            }
        }
    }
}
