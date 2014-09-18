using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_H2G_CodeDownload_CRCResults 
        : FFTgt_B2B_CodeDownload_Data
    {
        public byte[] CRCResults { get; set; }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_CodeDownloadOptions.CRCResults;
            }
        }
    }
}
