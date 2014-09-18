using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region Change Baud Rate

    internal class FFParser_Tgt_Generic_CodeDownload_ChangeBaudRate
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_CodeDownload_ChangeBaudRate()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_CodeDownload_ChangeBaudRate
        : FFParser_Tgt_Generic_CodeDownload_ChangeBaudRate
    {
        internal FFParser_Tgt_MC300_CodeDownload_ChangeBaudRate()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_CodeDownload_ChangeBaudRate_H2G
        : FFParser_Tgt_MC300_CodeDownload_ChangeBaudRate
    {
        internal FFParser_Tgt_MC300_CodeDownload_ChangeBaudRate_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_CodeDownload_ChangeBaudRate tgt = new FFTgt_H2G_CodeDownload_ChangeBaudRate();
            tgt.BaudRate = FreeformHelper.GetBytesToNumberInt32(buffer, 0, 4);
            return tgt;
        }
    }
    #endregion


    #region Baud Rate Test
    internal class FFParser_Tgt_Generic_CodeDownload_BaudRateTest
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_CodeDownload_BaudRateTest()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_CodeDownload_BaudRateTest
        : FFParser_Tgt_Generic_CodeDownload_BaudRateTest
    {
        internal FFParser_Tgt_MC300_CodeDownload_BaudRateTest()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_CodeDownload_BaudRateTest_H2G
        : FFParser_Tgt_MC300_CodeDownload_BaudRateTest
    {
        internal FFParser_Tgt_MC300_CodeDownload_BaudRateTest_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_CodeDownload_BaudRateTest tgt = new FFTgt_H2G_CodeDownload_BaudRateTest();
            tgt.DataValue = buffer;
            return tgt;
        }
    }
    #endregion

    internal class FFParser_Tgt_Generic_CodeDownload_BaudRateTestResponse
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_CodeDownload_BaudRateTestResponse()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_CodeDownload_BaudRateTestResponse
        : FFParser_Tgt_Generic_CodeDownload_BaudRateTestResponse
    {
        internal FFParser_Tgt_MC300_CodeDownload_BaudRateTestResponse()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_CodeDownload_BaudRateTestResponse_H2G
        : FFParser_Tgt_MC300_CodeDownload_BaudRateTestResponse
    {
        internal FFParser_Tgt_MC300_CodeDownload_BaudRateTestResponse_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_CodeDownload_BaudRateTestResponse tgt = new FFTgt_H2G_CodeDownload_BaudRateTestResponse();
            tgt.DataValue = buffer;
            return tgt;
        }
    }


}
