using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BMC.ExComms.Contracts.DTO.Freeform
{
    /// <summary>
    /// Base class - GMU to Host (or) Host to GMU Freeform for Code Download
    /// Its is used to download code and options to the GMU
    /// </summary>
    public class FFTgt_B2B_CodeDownload
        : FFTgt_B2B, IFFTgt_B2B
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TargetIds.CodeDownload;
            }
        }

        public FFTgt_B2B_CodeDownload_Data CodeDownloadData { get; set; }
    }

    public class FFTgt_B2B_CodeDownload_Data
        : FFTgt_B2B { }
}
