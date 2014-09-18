using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{

    internal class FFParser_Tgt_Generic_SystemToEPI : FFTgtParser
    {
        internal FFParser_Tgt_Generic_SystemToEPI()
            : base()
        {
        }
    }

    internal class FFParser_Tgt_MC300_SystemToEPI : FFParser_Tgt_Generic_SystemToEPI
    {
        internal FFParser_Tgt_MC300_SystemToEPI()
            : base()
        {

        }
    }

    internal class FFParser_Tgt_MC300_SystemToEPI_G2H : FFParser_Tgt_MC300_SystemToEPI
    {

        internal FFParser_Tgt_MC300_SystemToEPI_G2H()
            : base()
        {

        }
    }

    internal class FFParser_Tgt_MC300_SystemToEPI_H2G : FFParser_Tgt_MC300_SystemToEPI
    {

        internal FFParser_Tgt_MC300_SystemToEPI_H2G()
            : base()
        {

        }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FF_AppId_SystemToEPI_MessageTypes messageType = (FF_AppId_SystemToEPI_MessageTypes)buffer[0];
            FFTgt_H2G_SystemToEPI_Data tgt = null;

            switch (messageType)
            {

                case FF_AppId_SystemToEPI_MessageTypes.TicketPrint:
                    tgt = new FFTgt_H2G_SystemToEPI_TicketPrint();
                    break;
                case FF_AppId_SystemToEPI_MessageTypes.TicketRedeem:
                    tgt = new FFTgt_H2G_SystemToEPI_TicketRedeem();
                    break;
                case FF_AppId_SystemToEPI_MessageTypes.TicketError:
                    tgt = new FFTgt_H2G_SystemToEPI_TicketError();
                    break;
                case FF_AppId_SystemToEPI_MessageTypes.Promo:
                    tgt = new FFTgt_H2G_SystemToEPI_Promo();
                    break;
                case FF_AppId_SystemToEPI_MessageTypes.Sports:
                    tgt = new FFTgt_H2G_SystemToEPI_Sports();
                    break;
                case FF_AppId_SystemToEPI_MessageTypes.F5:
                    tgt = new FFTgt_H2G_SystemToEPI_F5();
                    break;

            }
            tgt.TextLength = buffer[1];
            tgt.Text = FreeformHelper.GetASCIIStringValue(FreeformHelper.CopyToBuffer(buffer, 2, buffer.Length - 2));
            return tgt;

        }



    }



}

