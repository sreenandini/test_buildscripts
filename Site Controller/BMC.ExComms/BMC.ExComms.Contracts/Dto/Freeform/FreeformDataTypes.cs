using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public enum FFEndianType
    {
        LittleEndian = 0,
        BigEndian = 1
    }

    public static class FFDataTypeHelper
    {
        public static readonly FFEndianType SystemEndianType = FFEndianType.LittleEndian;

        static FFDataTypeHelper()
        {
            SystemEndianType = FindSystemEndianType();
        }

        private static FFEndianType FindSystemEndianType()
        {
            FFInt16 ffValue = new FFInt16() { Value = 0x1234 };
            if (ffValue.Value1 == 0x12 && ffValue.Value2 == 0x34) return FFEndianType.BigEndian;
            else return FFEndianType.LittleEndian;
        }

        public static Int16 GetInt16(byte[] value, FFEndianType endian)
        {
            return GetInt16(value, endian, 0, 2);
        }

        public static Int16 GetInt16(byte[] value, FFEndianType endian, int offset, int length)
        {
            FFInt16 ffValue = new FFInt16();
            if (endian == FFEndianType.BigEndian)
            {
                ffValue.Value1 = value[0];
                ffValue.Value2 = value[1];
            }
            else
            {
                ffValue.Value1 = value[1];
                ffValue.Value2 = value[0];
            }
            return ffValue.Value;
        }

        public static byte[] GetInt16Bytes(Int16 value, FFEndianType endian)
        {
            FFInt16 ffValue = new FFInt16() { Value = value };
            byte[] result = new byte[2];

            if ((endian == FFEndianType.BigEndian && SystemEndianType == FFEndianType.LittleEndian) ||
                (endian == FFEndianType.LittleEndian && SystemEndianType == FFEndianType.BigEndian))
            {
                result[0] = ffValue.Value2;
                result[1] = ffValue.Value1;
            }
            else
            {
                result[0] = ffValue.Value1;
                result[1] = ffValue.Value2;
            }
            return result;
        }

        public static UInt16 GetUInt16(byte[] value, FFEndianType endian)
        {
            return GetUInt16(value, endian, 0, 2);
        }

        public static UInt16 GetUInt16(byte[] value, FFEndianType endian, int offset, int length)
        {
            FFUInt16 ffValue = new FFUInt16();
            if (endian == FFEndianType.BigEndian)
            {
                ffValue.Value1 = value[0];
                ffValue.Value2 = value[1];
            }
            else
            {
                ffValue.Value1 = value[1];
                ffValue.Value2 = value[0];
            }
            return ffValue.Value;
        }

        public static byte[] GetUInt16Bytes(UInt16 value, FFEndianType endian)
        {
            FFUInt16 ffValue = new FFUInt16() { Value = value };
            byte[] result = new byte[2];

            if ((endian == FFEndianType.BigEndian && SystemEndianType == FFEndianType.LittleEndian) ||
                (endian == FFEndianType.LittleEndian && SystemEndianType == FFEndianType.BigEndian))
            {
                result[0] = ffValue.Value2;
                result[1] = ffValue.Value1;
            }
            else
            {
                result[0] = ffValue.Value1;
                result[1] = ffValue.Value2;
            }
            return result;
        }

        public static Int32 GetInt32(byte[] value, FFEndianType endian)
        {
            return GetInt32(value, endian, 0, 4);
        }

        public static Int32 GetInt32(byte[] value, FFEndianType endian, int offset, int length)
        {
            FFInt32 ffValue = new FFInt32();
            if (endian == FFEndianType.BigEndian)
            {
                ffValue.Value1 = value[0];
                ffValue.Value2 = value[1];
                ffValue.Value3 = value[2];
                ffValue.Value4 = value[3];
            }
            else
            {
                ffValue.Value1 = value[3];
                ffValue.Value2 = value[2];
                ffValue.Value3 = value[1];
                ffValue.Value4 = value[0];
            }
            return ffValue.Value;
        }

        public static byte[] GetInt32Bytes(Int32 value, FFEndianType endian)
        {
            FFInt32 ffValue = new FFInt32() { Value = value };
            byte[] result = new byte[4];

            if ((endian == FFEndianType.BigEndian && SystemEndianType == FFEndianType.LittleEndian) ||
                (endian == FFEndianType.LittleEndian && SystemEndianType == FFEndianType.BigEndian))
            {
                result[0] = ffValue.Value4;
                result[1] = ffValue.Value3;
                result[2] = ffValue.Value2;
                result[3] = ffValue.Value1;
            }
            else
            {
                result[0] = ffValue.Value1;
                result[1] = ffValue.Value2;
                result[2] = ffValue.Value3;
                result[3] = ffValue.Value4;
            }
            return result;
        }

        public static UInt32 GetUInt32(byte[] value, FFEndianType endian)
        {
            return GetUInt32(value, endian, 0, 4);
        }

        public static UInt32 GetUInt32(byte[] value, FFEndianType endian, int offset, int length)
        {
            FFUInt32 ffValue = new FFUInt32();
            if (endian == FFEndianType.BigEndian)
            {
                ffValue.Value1 = value[0];
                ffValue.Value2 = value[1];
                ffValue.Value3 = value[2];
                ffValue.Value4 = value[3];
            }
            else
            {
                ffValue.Value1 = value[3];
                ffValue.Value2 = value[2];
                ffValue.Value3 = value[1];
                ffValue.Value4 = value[0];
            }
            return ffValue.Value;
        }

        public static byte[] GetUInt32Bytes(UInt32 value, FFEndianType endian)
        {
            FFUInt32 ffValue = new FFUInt32() { Value = value };
            byte[] result = new byte[4];

            if ((endian == FFEndianType.BigEndian && SystemEndianType == FFEndianType.LittleEndian) ||
                (endian == FFEndianType.LittleEndian && SystemEndianType == FFEndianType.BigEndian))
            {
                result[0] = ffValue.Value4;
                result[1] = ffValue.Value3;
                result[2] = ffValue.Value2;
                result[3] = ffValue.Value1;
            }
            else
            {
                result[0] = ffValue.Value1;
                result[1] = ffValue.Value2;
                result[2] = ffValue.Value3;
                result[3] = ffValue.Value4;
            }
            return result;
        }

        public static Int64 GetInt64(byte[] value, FFEndianType endian)
        {
            return GetInt64(value, endian, 0, 8);
        }

        public static Int64 GetInt64(byte[] value, FFEndianType endian, int offset, int length)
        {
            FFInt64 ffValue = new FFInt64();
            if (endian == FFEndianType.BigEndian)
            {
                ffValue.Value1 = value[0];
                ffValue.Value2 = value[1];
                ffValue.Value3 = value[2];
                ffValue.Value4 = value[3];
                ffValue.Value5 = value[4];
                ffValue.Value6 = value[5];
                ffValue.Value7 = value[6];
                ffValue.Value8 = value[7];
            }
            else
            {
                ffValue.Value1 = value[7];
                ffValue.Value2 = value[6];
                ffValue.Value3 = value[5];
                ffValue.Value4 = value[4];
                ffValue.Value5 = value[3];
                ffValue.Value6 = value[2];
                ffValue.Value7 = value[1];
                ffValue.Value8 = value[0];
            }
            return ffValue.Value;
        }

        public static byte[] GetInt64Bytes(Int64 value, FFEndianType endian)
        {
            FFInt64 ffValue = new FFInt64() { Value = value };
            byte[] result = new byte[8];

            if ((endian == FFEndianType.BigEndian && SystemEndianType == FFEndianType.LittleEndian) ||
                (endian == FFEndianType.LittleEndian && SystemEndianType == FFEndianType.BigEndian))
            {
                result[0] = ffValue.Value8;
                result[1] = ffValue.Value7;
                result[2] = ffValue.Value6;
                result[3] = ffValue.Value5;
                result[4] = ffValue.Value4;
                result[5] = ffValue.Value3;
                result[6] = ffValue.Value2;
                result[7] = ffValue.Value1;
            }
            else
            {
                result[0] = ffValue.Value1;
                result[1] = ffValue.Value2;
                result[2] = ffValue.Value3;
                result[3] = ffValue.Value4;
                result[4] = ffValue.Value5;
                result[5] = ffValue.Value6;
                result[6] = ffValue.Value7;
                result[7] = ffValue.Value8;
            }
            return result;
        }

        public static UInt64 GetUInt64(byte[] value, FFEndianType endian)
        {
            return GetUInt64(value, endian, 0, 8);
        }

        public static UInt64 GetUInt64(byte[] value, FFEndianType endian, int offset, int length)
        {
            FFUInt64 ffValue = new FFUInt64();
            if (endian == FFEndianType.BigEndian)
            {
                ffValue.Value1 = value[0];
                ffValue.Value2 = value[1];
                ffValue.Value3 = value[2];
                ffValue.Value4 = value[3];
                ffValue.Value5 = value[4];
                ffValue.Value6 = value[5];
                ffValue.Value7 = value[6];
                ffValue.Value8 = value[7];
            }
            else
            {
                ffValue.Value1 = value[7];
                ffValue.Value2 = value[6];
                ffValue.Value3 = value[5];
                ffValue.Value4 = value[4];
                ffValue.Value5 = value[3];
                ffValue.Value6 = value[2];
                ffValue.Value7 = value[1];
                ffValue.Value8 = value[0];
            }
            return ffValue.Value;
        }

        public static byte[] GetUInt64Bytes(UInt64 value, FFEndianType endian)
        {
            FFUInt64 ffValue = new FFUInt64() { Value = value };
            byte[] result = new byte[8];

            if ((endian == FFEndianType.BigEndian && SystemEndianType == FFEndianType.LittleEndian) ||
                (endian == FFEndianType.LittleEndian && SystemEndianType == FFEndianType.BigEndian))
            {
                result[0] = ffValue.Value8;
                result[1] = ffValue.Value7;
                result[2] = ffValue.Value6;
                result[3] = ffValue.Value5;
                result[4] = ffValue.Value4;
                result[5] = ffValue.Value3;
                result[6] = ffValue.Value2;
                result[7] = ffValue.Value1;
            }
            else
            {
                result[0] = ffValue.Value1;
                result[1] = ffValue.Value2;
                result[2] = ffValue.Value3;
                result[3] = ffValue.Value4;
                result[4] = ffValue.Value5;
                result[5] = ffValue.Value6;
                result[6] = ffValue.Value7;
                result[7] = ffValue.Value8;
            }
            return result;
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct FFInt16
    {
        [FieldOffset(0)]
        public short Value;
        [FieldOffset(0)]
        public byte Value1;
        [FieldOffset(1)]
        public byte Value2;
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct FFUInt16
    {
        [FieldOffset(0)]
        public ushort Value;
        [FieldOffset(0)]
        public byte Value1;
        [FieldOffset(1)]
        public byte Value2;
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct FFInt32
    {
        [FieldOffset(0)]
        public int Value;
        [FieldOffset(0)]
        public byte Value1;
        [FieldOffset(1)]
        public byte Value2;
        [FieldOffset(2)]
        public byte Value3;
        [FieldOffset(3)]
        public byte Value4;
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct FFUInt32
    {
        [FieldOffset(0)]
        public uint Value;
        [FieldOffset(0)]
        public byte Value1;
        [FieldOffset(1)]
        public byte Value2;
        [FieldOffset(2)]
        public byte Value3;
        [FieldOffset(3)]
        public byte Value4;
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct FFInt64
    {
        [FieldOffset(0)]
        public long Value;
        [FieldOffset(0)]
        public byte Value1;
        [FieldOffset(1)]
        public byte Value2;
        [FieldOffset(2)]
        public byte Value3;
        [FieldOffset(3)]
        public byte Value4;
        [FieldOffset(4)]
        public byte Value5;
        [FieldOffset(5)]
        public byte Value6;
        [FieldOffset(6)]
        public byte Value7;
        [FieldOffset(7)]
        public byte Value8;
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct FFUInt64
    {
        [FieldOffset(0)]
        public ulong Value;
        [FieldOffset(0)]
        public byte Value1;
        [FieldOffset(1)]
        public byte Value2;
        [FieldOffset(2)]
        public byte Value3;
        [FieldOffset(3)]
        public byte Value4;
        [FieldOffset(4)]
        public byte Value5;
        [FieldOffset(5)]
        public byte Value6;
        [FieldOffset(6)]
        public byte Value7;
        [FieldOffset(7)]
        public byte Value8;
    }
}
