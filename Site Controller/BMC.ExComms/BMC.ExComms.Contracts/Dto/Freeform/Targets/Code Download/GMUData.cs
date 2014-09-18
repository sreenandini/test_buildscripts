using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region Request
    /// <summary>
    /// The host sends this message to the GMU to request information about the GMU’s configuration
    /// </summary>
    public class FFTgt_H2G_CodeDownload_GMUDataRequest
        : FFTgt_B2B_CodeDownload_Data
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_CodeDownloadOptions.GMUDataRequest;
            }
        }
    } 
    #endregion

    #region Response
    /// <summary>
    /// The GMU sends this message to the host in reply to the GMU Data Request message
    /// </summary>
    public class FFTgt_G2H_CodeDownload_GMUDataResponse
        : FFTgt_B2B_CodeDownload_Data
    {
        /// <summary>
        /// ECOxxxx versions or Ver-aaa.bb.ccd versions
        /// </summary>
        public string GMUVersion { get; set; }
        public string EEPROMID { get; set; }
        public string OptionVersion { get; set; }
        public byte Side { get; set; }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_CodeDownloadOptions.GMUDataResponse;
            }
        }
    } 
    #endregion
}
