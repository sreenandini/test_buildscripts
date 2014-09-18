using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    /// <summary>
    /// The host sends this message to the GMU to instruct it to being messaging to upgrade its version.  
    /// If the version specified in the “OldVersion” field does not match the version of the GMU, the GMU should ignore all subsequent download messages
    /// </summary>
    public class FFTgt_H2G_CodeDownload_ChangeVersion
        : FFTgt_B2B_CodeDownload_Data
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_CodeDownloadOptions.ChangeVersion;
            }
        }

        public string NewVersion { get; set; }
        public string OldVersion { get; set; }
    }
}
