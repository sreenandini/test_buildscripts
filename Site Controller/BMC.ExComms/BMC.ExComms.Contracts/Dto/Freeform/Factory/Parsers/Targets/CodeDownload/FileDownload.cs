using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_CodeDownload_FileDownload 
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_CodeDownload_FileDownload()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_CodeDownload_FileDownload 
        : FFParser_Tgt_Generic_CodeDownload_FileDownload
    {
        internal FFParser_Tgt_MC300_CodeDownload_FileDownload()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_CodeDownload_FileDownload_H2G 
        : FFParser_Tgt_MC300_CodeDownload_FileDownload
    {
        internal FFParser_Tgt_MC300_CodeDownload_FileDownload_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_CodeDownload_FileDownload tgt = new FFTgt_H2G_CodeDownload_FileDownload();
            tgt.FileType = buffer[0].GetAppId<FF_GmuId_FileDownloadFileType, FF_AppId_FileDownloadFileType>();
            tgt.Length = FreeformHelper.GetBytesToNumberInt32(buffer, 1, 4);
            tgt.CompressedFile = buffer.CopyToBuffer(5, tgt.Length);
            tgt.CRC = FreeformHelper.GetValue<short>(buffer, tgt.Length + 5, 2);
            return tgt;
        }
    }
}
