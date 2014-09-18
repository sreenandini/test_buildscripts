using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_CodeDownload 
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_CodeDownload()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_CodeDownload 
        : FFParser_Tgt_Generic_CodeDownload
    {
        internal FFParser_Tgt_MC300_CodeDownload()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_CodeDownload_G2H 
        : FFParser_Tgt_MC300_CodeDownload
    {
        internal FFParser_Tgt_MC300_CodeDownload_G2H()
            : base()
        {
            this.AddSubParsers();
        }

        protected override void AddSubParsersInternal()
        {
            this.AddBufferEntityParser((int)FF_GmuId_CodeDownloadOptions.GMUDataResponse, (int)FF_AppId_CodeDownloadOptions.GMUDataResponse, new FFParser_Tgt_MC300_CodeDownload_GMUData_G2H());
        }
    }

    internal class FFParser_Tgt_MC300_CodeDownload_H2G 
        : FFParser_Tgt_MC300_CodeDownload
    {
        internal FFParser_Tgt_MC300_CodeDownload_H2G()
            : base()
        {
            this.AddSubParsers();
        }

        protected override void AddSubParsersInternal()
        {
            this.AddBufferEntityParser((int)FF_GmuId_CodeDownloadOptions.ChangeBaudRate, (int)FF_AppId_CodeDownloadOptions.ChangeBaudRate, new FFParser_Tgt_MC300_CodeDownload_ChangeBaudRate_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_CodeDownloadOptions.BaudRateTest, (int)FF_AppId_CodeDownloadOptions.BaudRateTest, new FFParser_Tgt_MC300_CodeDownload_BaudRateTest_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_CodeDownloadOptions.BaudRateTestResponse, (int)FF_AppId_CodeDownloadOptions.BaudRateTestResponse, new FFParser_Tgt_MC300_CodeDownload_BaudRateTestResponse_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_CodeDownloadOptions.GMUDataRequest, (int)FF_AppId_CodeDownloadOptions.GMUDataRequest, new FFParser_Tgt_MC300_CodeDownload_GMUData_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_CodeDownloadOptions.ChangeVersion, (int)FF_AppId_CodeDownloadOptions.ChangeVersion, new FFParser_Tgt_MC300_CodeDownload_ChangeVersion_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_CodeDownloadOptions.FileDownload, (int)FF_AppId_CodeDownloadOptions.FileDownload, new FFParser_Tgt_MC300_CodeDownload_FileDownload_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_CodeDownloadOptions.CRCResults, (int)FF_AppId_CodeDownloadOptions.CRCResults, new FFParser_Tgt_MC300_CRCResults_H2G());
        }
    }
}
