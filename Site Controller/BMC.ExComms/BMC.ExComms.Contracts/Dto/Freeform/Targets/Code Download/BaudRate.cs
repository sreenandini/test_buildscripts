using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region ChangeBaudRate
    /// <summary>
    /// The host uses this message to change the baud rate the GMU uses for Slot Line communications.
    /// </summary>
    public class FFTgt_H2G_CodeDownload_ChangeBaudRate 
        : FFTgt_B2B_CodeDownload_Data
    {
        /// <summary>
        /// Baud Rate as 32-bit Big Endian integer.
        /// </summary>
        public int BaudRate { get; set; }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_CodeDownloadOptions.ChangeBaudRate;
            }
        }
    } 
    #endregion

    #region BaudRateTest
    /// <summary>
    /// The host uses this message to send a data value to the GMU. The GMU will send this data value back to the host in the Baud Rate Test Reply message
    /// </summary>
    public class FFTgt_H2G_CodeDownload_BaudRateTest 
        : FFTgt_B2B_CodeDownload_Data
    {
        /// <summary>
        /// Arbitrary data value provided by the host. This value will be echoed back in the Baud Rate Test Reply message
        /// </summary>
        public byte[] DataValue
        {
            get;
            set;
        }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_CodeDownloadOptions.BaudRateTest;
            }
        }
    } 
    #endregion

    #region BaudRateTestResponse
    /// <summary>
    /// The host uses this message to send a data value to the GMU. 
    /// The GMU will send this data value back to the host in the Baud Rate Test Reply message
    /// </summary>
    public class FFTgt_H2G_CodeDownload_BaudRateTestResponse
        : FFTgt_B2B_CodeDownload_Data
    {
        /// <summary>
        /// Arbitrary data value being echoed back to the host.
        /// </summary>
        public byte[] DataValue
        {
            get;
            set;
        }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_CodeDownloadOptions.BaudRateTestResponse;
            }
        }
    } 
    #endregion
}
