using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Server.Handlers.Tickets
{
    internal sealed class TicketIDInfo
        : DisposableObject
    {
        internal TicketIDInfo()
        {
            UnpackedID = new byte[ExSrvConstants.TicketStringLength * 2];
        }

        internal TicketIDInfo(string barCode)
            : this()
        {
            byte[] barcodeBytes = barCode.ConvertToASCII(0, UnpackedID.Length);
            this.UnpackedID.CopyToBuffer(barcodeBytes, 0, this.UnpackedID.Length);
        }

        public double Amount { get; set; }
        public int TicketNumber { get; set; }
        public int SlotID { get; set; }

        public byte[] UnpackedID { get; private set; }

        public int SequenceNumber
        {
            get
            {
                byte[] seqNo = this.PackedID.CopyToBuffer(4, 3);
                seqNo[0] &= 0x0F;
                return seqNo.GetBytesToBCDInt32(0, 3);
            }
        }

        public byte[] PackedID
        {
            get
            {
                return Barcode.GetBCDToBytes(ExSrvConstants.TicketStringLength);
            }
        }

        public string Barcode
        {
            get
            {
                return Encoding.ASCII.GetString(UnpackedID);
            }
        }
    }
}
