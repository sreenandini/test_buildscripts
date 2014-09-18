using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_EPI : FFTgtParser
    {
        internal FFParser_Tgt_Generic_EPI()
            : base()
        {
        }
      
    }

    internal class FFParser_Tgt_MC300_EPI : FFParser_Tgt_Generic_EPI
    {
        internal FFParser_Tgt_MC300_EPI()
            : base()
        {

        }
    }

    internal class FFParser_Tgt_MC300_EPI_G2H : FFParser_Tgt_MC300_EPI
    {

        internal FFParser_Tgt_MC300_EPI_G2H()
            : base()
        {
           
        }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_EPI_Info tgt = new FFTgt_H2G_EPI_Info();
            tgt.DeviceAddress = buffer[0];
            tgt.EPICommand=buffer[1];
            tgt.EPIMessages = FreeformHelper.CopyToBuffer(buffer, 2, buffer.Length);
            return tgt;
        }
      

    }

    internal class FFParser_Tgt_MC300_EPI_H2G : FFParser_Tgt_MC300_EPI
    {

        internal FFParser_Tgt_MC300_EPI_H2G()
            : base()
        {
            
        }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_EPI_Info tgt = new FFTgt_H2G_EPI_Info();
            tgt.DeviceAddress = buffer[0];
            tgt.EPICommand = buffer[1];
            tgt.EPIMessages = FreeformHelper.CopyToBuffer(buffer, 2, buffer.Length);
            return tgt;
        }

    }
}
