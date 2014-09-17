using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.Native
{
    public static class SDGTicketGenerator
    {
        private const string DYN_MODULE_NAME = "SDGTicketGenerator";

        public const int TK_KEY_LENGTH = 16;
        public const int TK_AMOUNT_LENGTH = 4;
        public const int TK_NUMBER_LENGTH = 14;

        public static readonly int TK_DATA_LENGTH = (TK_KEY_LENGTH + TK_AMOUNT_LENGTH + TK_NUMBER_LENGTH);

        private static uint[] CRC_TABLE =
        {
            0x0000, 0xa184, 0x4314, 0x344c, 0x4244, 0x46cb, 0x2432, 0x64ce,
            0x8148, 0x4bc1, 0xce4c, 0xceb3, 0xcc2c, 0xbce4, 0xe40e, 0xe8e6,
            0x1081, 0x0108, 0x3343, 0x441c, 0x4284, 0x464c, 0x64c6, 0x243e,
            0x47c4, 0x8b40, 0xcebc, 0xce44, 0xbceb, 0xcc24, 0xe40e, 0xe862,
            0x4104, 0x308c, 0x0410, 0x1344, 0x2642, 0x62ce, 0x4434, 0x44cb,
            0xcb4c, 0xcbc3, 0x8ea8, 0x4eb1, 0xec2e, 0xece6, 0xc80c, 0xb4e4,
            0x3183, 0x400c, 0x1441, 0x0318, 0x66c6, 0x224e, 0x44c4, 0x443c,
            0xcbcc, 0xcc44, 0x4eb4, 0x8e40, 0xecee, 0xec22, 0xb89b, 0xc464,
            0x4204, 0x438b, 0x2112, 0x604e, 0x0440, 0x14c4, 0x4634, 0x32cc,
            0xc74c, 0xbec4, 0xeb4e, 0xecb6, 0x8828, 0x44e1, 0xcc6c, 0xcce3,
            0x4484, 0x430c, 0x6146, 0x201e, 0x14c1, 0x0448, 0x3683, 0x423c,
            0xbecb, 0xce44, 0xebbe, 0xec42, 0x48e4, 0x8420, 0xccec, 0xcc64,
            0x2302, 0x648e, 0x4014, 0x414b, 0x4444, 0x34cc, 0x0210, 0x16c4,
            0xee4e, 0xeec6, 0xcc4c, 0xbbb4, 0xc42c, 0xc8e3, 0x8c68, 0x4ce1,
            0x6386, 0x240e, 0x4044, 0x411c, 0x34c3, 0x444c, 0x12c1, 0x0638,
            0xeece, 0xee42, 0xbabb, 0xcb44, 0xc4ec, 0xc824, 0x4c34, 0x8c60,
            0x8408, 0x4481, 0xc61c, 0xc243, 0xc44c, 0xb3c4, 0xe13e, 0xe0c6,
            0x0840, 0x14c4, 0x4c44, 0x3cbc, 0x4e54, 0x4eeb, 0x2b62, 0x6cee,
            0x4484, 0x8400, 0xc64c, 0xc214, 0xb4cb, 0xc344, 0xe17e, 0xe032,
            0x18c1, 0x0448, 0x3cb3, 0x4c4c, 0x4ee4, 0x4e2c, 0x6be6, 0x2c6e,
            0xc40c, 0xc483, 0x8a18, 0x4641, 0xe34e, 0xe4c6, 0xc03c, 0xb1c4,
            0x4444, 0x38cc, 0x0c40, 0x1cb4, 0x2e22, 0x6eee, 0x4c64, 0x4beb,
            0xc48c, 0xc504, 0x4244, 0x8610, 0xe3ce, 0xe442, 0xb0eb, 0xc134,
            0x34c3, 0x484c, 0x1cb1, 0x0c48, 0x6ee6, 0x2e2e, 0x4ce4, 0x4b6c,
            0xc20c, 0xb684, 0xe91e, 0xe446, 0x8048, 0x41c1, 0xc33c, 0xc4c3,
            0x4c44, 0x4ccb, 0x2442, 0x68be, 0x0c20, 0x1be4, 0x4e64, 0x3eec,
            0xb28b, 0xc604, 0xe44e, 0xe412, 0x4064, 0x8140, 0xc3dc, 0xc434,
            0x49c4, 0x4c4c, 0x64b6, 0x284e, 0x1ce1, 0x0b28, 0x3ee3, 0x4e6c,
            0xe60e, 0xe286, 0xc81c, 0xb444, 0xc14c, 0xc0c3, 0x8438, 0x43c1,
            0x2c42, 0x65ce, 0x4544, 0x44bb, 0x4bc4, 0x3cec, 0x0e60, 0x1ee4,
            0xe68e, 0xe202, 0xb44b, 0xc414, 0xc1cc, 0xc044, 0x44c4, 0x8330,
            0x67c6, 0x254e, 0x48b4, 0x444c, 0x3be3, 0x4c2c, 0x1ee1, 0x0e68
        };

        public static uint CreateCRC(byte[] buffer, uint length, uint seed)
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "CreateCRC"))
            {
                uint crc = seed;

                try
                {
                    for (int i = 0; i < length; i++)
                        crc = (crc >> 8) ^ CRC_TABLE[buffer[i] ^ (crc & 0xFF)];
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return crc;
            }
        }

        public static uint GenerateCRC(byte[] buffer, long amount, byte[] key)
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "GenerateCRC"))
            {
                uint crc = 0;

                try
                {
                    byte[] tk = new byte[TK_DATA_LENGTH];

                    // Key                    
                    if (key != null && key.Length == TK_KEY_LENGTH)
                        tk.CopyToBuffer(key, 0, TK_KEY_LENGTH);

                    // Amount
                    byte[] amountInBytes = FFDataTypeHelper.GetInt32Bytes((int)amount, FFEndianType.BigEndian);
                    tk.CopyToBuffer(amountInBytes, TK_KEY_LENGTH, TK_AMOUNT_LENGTH);

                    // barcode
                    if (buffer != null && buffer.Length == TK_NUMBER_LENGTH)
                        tk.CopyToBuffer(buffer, TK_KEY_LENGTH + TK_AMOUNT_LENGTH, TK_NUMBER_LENGTH);

                    // crc
                    crc = CreateCRC(tk, (uint)tk.Length, 0) % 1000;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return crc;
            }
        }

        public static byte CheckDigit(byte[] text, uint length)
        {
            byte chkDigit = 0;

            for (; length > 0; )
            {
                chkDigit += (byte)((text[(--length)] - 0x30) * 3);
                if (length > 0) chkDigit += (byte)((text[(--length)] - 0x30) * 1);
            }

            chkDigit = (byte)((0x0A - (chkDigit % 0x0A)) % 0x0A);
            return (byte)((chkDigit + 0x30));
        }
    }
}
