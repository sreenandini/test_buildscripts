using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{    
    internal class FFParser_Tgt_Generic_CodeDownload_CRCResults 
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_CodeDownload_CRCResults()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_CodeDownload_CRCResults 
        : FFParser_Tgt_Generic_CodeDownload_CRCResults
    {
        internal FFParser_Tgt_MC300_CodeDownload_CRCResults()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_CRCResults_H2G 
        : FFParser_Tgt_MC300_CodeDownload_CRCResults
    {
        internal FFParser_Tgt_MC300_CRCResults_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_CodeDownload_CRCResults tgt = new FFTgt_H2G_CodeDownload_CRCResults();          
            tgt.CRCResults = buffer;
            return tgt;
        }
    }
}
