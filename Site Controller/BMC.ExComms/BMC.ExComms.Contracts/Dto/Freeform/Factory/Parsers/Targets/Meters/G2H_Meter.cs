using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_AccountingMeters 
        : FFTgtParser
    {
        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_AccountingMeters tgt = new FFTgt_G2H_AccountingMeters();
            this.ParseBuffer(tgt, rootEntity, buffer, 1, buffer.Length);
            return tgt;
        }
    }

    internal class FFParser_Tgt_MC300_AccountingMeters 
        : FFParser_Tgt_Generic_AccountingMeters
    {
        internal FFParser_Tgt_MC300_AccountingMeters()
            : base()
        {
            this.AddSubParsers();
        }

        protected override void AddSubParsersInternal()
        {
            this.AddBufferEntityParser(typeof(FF_GmuId_AccountingMeterIds), typeof(FF_AppId_AccountingMeterIds),
              (tgt, idx, len, buf) =>
              {
                  FFTgt_G2H_AccountingMeter meter = new FFTgt_G2H_AccountingMeter((FF_AppId_AccountingMeterIds)idx, FreeformHelper.GetBytesToBCDDouble(buf, 0, len));
                  ((FFTgt_G2H_AccountingMeters)tgt).Meters.Add(meter);
                  return meter;
              });
        }
    }

    internal class FFParser_Tgt_MC300_AccountingMeters_G2H
        : FFParser_Tgt_MC300_AccountingMeters
    {
        internal FFParser_Tgt_MC300_AccountingMeters_G2H()
            : base()
        {
        }
    }
}
