using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_CodeDownload_GMUData 
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_CodeDownload_GMUData()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_CodeDownload_GMUData 
        : FFParser_Tgt_Generic_CodeDownload_GMUData
    {
        internal FFParser_Tgt_MC300_CodeDownload_GMUData()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_CodeDownload_GMUData_H2G 
        : FFParser_Tgt_MC300_CodeDownload_GMUData
    {
        internal FFParser_Tgt_MC300_CodeDownload_GMUData_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_CodeDownload_GMUDataRequest tgt = new FFTgt_H2G_CodeDownload_GMUDataRequest();            
            return tgt;
        }
    }

    internal class FFParser_Tgt_MC300_CodeDownload_GMUData_G2H 
        : FFParser_Tgt_MC300_CodeDownload_GMUData
    {
        internal FFParser_Tgt_MC300_CodeDownload_GMUData_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_CodeDownload_GMUDataResponse tgt = new FFTgt_G2H_CodeDownload_GMUDataResponse();
            
            if (buffer.Length == 19)//ECOxxxx versions
            {
                tgt.GMUVersion = FreeformHelper.GetASCIIStringValue(buffer, 0, 7);
                tgt.EEPROMID = FreeformHelper.GetASCIIStringValue(buffer, 7, 7);
                tgt.OptionVersion = FreeformHelper.GetASCIIStringValue(buffer, 14, 4);
                tgt.Side = buffer[18];
            }
            else
            {
                ///Ver-aaa.bb.ccd versions
                tgt.GMUVersion = FreeformHelper.GetASCIIStringValue(buffer, 0, 14);
                tgt.EEPROMID = FreeformHelper.GetASCIIStringValue(buffer, 14, 7);
                tgt.OptionVersion = FreeformHelper.GetASCIIStringValue(buffer, 21, 4);
                tgt.Side = buffer[25];
            }
            return tgt;
        }
    }
}
