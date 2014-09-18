using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    /// <summary>
    /// This message is sent by the host to the GMU to provide one segment of a file.
    /// </summary>
    public class FFTgt_H2G_CodeDownload_FileDownload 
        : FFTgt_B2B_CodeDownload_Data
    {
        /// <summary>
        /// 1-Image File; 2-Authentication File; 3-Options file.
        /// </summary>
        public FF_AppId_FileDownloadFileType FileType { get; set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the compressed file.
        /// </summary>
        /// <value>
        /// The compressed file.
        /// </value>
        public byte[] CompressedFile { get; set; }

        /// <summary>
        /// CRC of compressed file
        /// </summary>
        public short CRC { get; set; }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_CodeDownloadOptions.FileDownload;
            }
        }
    }
}
