using System.Globalization;
using BMC.CoreLib;
using BMC.CoreLib.Comparers;
using BMC.CoreLib.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    /// <summary>
    /// Freeform Helper Class
    /// </summary>
    public static class FreeformHelper
    {
        #region Variables
        private const string DYN_MODULE_NAME = "FreeformHelper";

        private const char HEADER_SEPARATOR = '-';
        private const int HEADER_SEPARATOR_LEN = 80;

        private static Encoding ASCII_ENCODING = Encoding.ASCII;

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes the <see cref="FreeformHelper"/> class.
        /// </summary>
        static FreeformHelper() { }
        #endregion

        #region Helper Methods
        //public static byte GetRequestResponseId(this int id, out bool isResponseRequired)
        //{
        //    byte id2 = (byte)id;
        //    return id2.GetRequestResponseId(out isResponseRequired)
        //}

        public static byte GetRequestResponseId(this int id, out bool isResponseRequired)
        {
            isResponseRequired = false;
            int id2 = (id & 0xFF);
            if ((id2 & FreeformConstants.FF_ISRESPONSEREQUIRED) != 0)
            {
                id2 &= ~FreeformConstants.FF_ISRESPONSEREQUIRED;
                isResponseRequired = true;
            }
            return (byte)id2;
        }

        public static byte CreateRequestResponseId(this byte id, bool isResponseRequired)
        {
            return (byte)(id | (isResponseRequired ? FreeformConstants.FF_ISRESPONSEREQUIRED : 0));
        }

        public static byte CreateRequestResponseId(this int id, bool isResponseRequired)
        {
            return CreateRequestResponseId((byte)id, isResponseRequired);
        }

        public static int CreateCombinedId(int sessionId, int targetId)
        {
            return (((sessionId << 8) | targetId) & 0xFFFF);
        }

        public static int CreateCombinedId(this IFreeformEntity parent, int childId, bool responseRequired2)
        {
            if (parent == null) return CreateRequestResponseId(childId, responseRequired2);
            return CreateCombinedId(parent.UniqueEntityId, parent.IsResponseRequired, childId, responseRequired2);
        }

        public static int CreateCombinedId(this IFreeformEntity child, IFreeformEntity parent)
        {
            if (parent == null) return CreateRequestResponseId(child.UniqueEntityId, child.IsResponseRequired);
            return CreateCombinedId(parent.UniqueEntityId, parent.IsResponseRequired, child.UniqueEntityId, child.IsResponseRequired);
        }

        public static int ExtractCombinedId(this int combinedId)
        {
            return (combinedId & 0xFF);
        }

        public static int CreateCombinedId(int parentId, bool responseRequired1, int childId, bool responseRequired2)
        {
            return CreateCombinedId(CreateRequestResponseId(parentId, responseRequired1),
                                    CreateRequestResponseId(childId, responseRequired2));
        }
        #endregion

        #region Checksum Methods
        /// <summary>
        /// Calculates the check sum from the raw udp data.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <returns>Calculated checksum.</returns>
        public static byte CalculateCheckSumUdp(byte[] buffer)
        {
            return CalculateCheckSum(buffer, FreeformConstants.LEN_GMU_DEVICETYPE_START, (buffer.Length - FreeformConstants.LEN_GMU_IPADDRESS));
        }

        /// <summary>
        /// Calculates the check sum.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <returns>Calculated checksum.</returns>
        public static byte CalculateCheckSum(byte[] buffer)
        {
            return CalculateCheckSum(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Calculates the check sum.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <returns>Calculated checksum.</returns>
        public static byte CalculateCheckSum(IList buffer, int offset, int length)
        {
            byte result = 0;
            int actualLength = (offset + length);

            for (int i = offset; i < actualLength; i++)
            {
                result += (byte)buffer[i];
            }
            result = (byte)(~result + 1);
            return result;
        }

        /// <summary>
        /// Calculates the and store checksum into last byte of the given buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <returns>Input Buffer.</returns>
        public static byte[] CalculateAndStoreChecksum(this byte[] buffer)
        {
            CalculateAndStoreChecksum(buffer, 0, buffer.Length, buffer.Length - 1);
            return buffer;
        }

        /// <summary>
        /// Calculates the and store checksum into last byte of the given buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <returns>Input Buffer.</returns>
        public static List<byte> CalculateAndStoreChecksum(this List<byte> buffer)
        {
            CalculateAndStoreChecksum(buffer, 0, buffer.Count);
            return buffer;
        }

        /// <summary>
        /// <returns>Input Buffer.</returns>
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <param name="checksumOffset">The checksum offset.</param>
        /// <returns>Input Buffer.</returns>
        public static byte[] CalculateAndStoreChecksum(byte[] buffer, int offset, int length, int checksumOffset)
        {
            buffer[checksumOffset] = CalculateCheckSum(buffer, offset, length);
            return buffer;
        }

        /// <summary>
        /// Calculates the and store checksum.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <returns>Input Buffer.</returns>
        public static List<byte> CalculateAndStoreChecksum(this List<byte> buffer, int offset, int length)
        {
            buffer.Add(CalculateCheckSum(buffer, offset, length));
            return buffer;
        }

        /// <summary>
        /// Determines whether [has valid check sum] [the specified buffer].
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <returns>True if succeeded; otherwise false.</returns>
        public static bool HasValidCheckSum(byte[] buffer, int offset, int length)
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "HasValidCheckSum"))
            {
                bool result = default(bool);

                try
                {
                    if (buffer == null ||
                    buffer.Length < FreeformConstants.LEN_GMU_DEVICETYPE_START) return false;

                    int actualLength = (offset + length);
                    int offsetChecksum = (actualLength - 1);
                    if (offsetChecksum > actualLength)
                    {
                        method.Info("Buffer was not in preferred length.");
                        return false;
                    }

                    int calculatedCheckSum = CalculateCheckSum(buffer, offset, length);
                    int actualCheckSum = buffer[offsetChecksum];
                    if (calculatedCheckSum != actualCheckSum)
                    {
                        method.InfoV("Checksum mismatch. Received : {0:D}, Calculated : {1:D}", actualCheckSum, calculatedCheckSum);
                        return false;
                    }

                    result = true;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }
        #endregion

        #region Byte Methods
        public static byte[] CopyToBuffer(this byte[] buffer, int offset, int length)
        {
            byte[] buffer2 = new byte[length];
            Buffer.BlockCopy(buffer, offset, buffer2, 0, buffer2.Length);
            return buffer2;
        }

        public static byte[] CopyToBuffer(this byte[] destination, byte[] source, int offset, int length)
        {
            if (length > source.Length) length = source.Length;
            Buffer.BlockCopy(source, 0, destination, offset, length);
            return destination;
        }

        public static byte[] CopyToBufferASCII(this byte[] destination, byte[] source, int offset, int length)
        {
            destination.CopyToBuffer(source, offset, length);
            ConvertToASCII(destination, offset, length);
            return destination;
        }

        public static byte ConvertToByte(this bool src)
        {
            return src ? (byte)1 : (byte)0;
        }

        public static bool ConvertToBoolean(this byte src)
        {
            return (src == (byte)1);
        }

        public static byte[] GetRange(this byte[] buffer, int offset, int length)
        {
            return CopyToBuffer(buffer, offset, length);
        }
        #endregion

        #region Numeric Parsing

        #region Get Bytes to Number
        #region Number To Bytes
        public static sbyte GetBytesToNumberInt8(this byte[] value, int offset, int length)
        {
            if (offset < 0) offset = value.Length + offset;
            return (sbyte)value[offset];
        }

        public static byte GetBytesToNumberUInt8(this byte[] value, int offset, int length)
        {
            if (offset < 0) offset = value.Length + offset;
            return value[offset];
        }

        public static short GetBytesToNumberInt16(this byte[] value, int offset, int length)
        {
            return FFDataTypeHelper.GetInt16(value, FFEndianType.BigEndian, offset, length);
        }

        public static ushort GetBytesToNumberUInt16(this byte[] value, int offset, int length)
        {
            return FFDataTypeHelper.GetUInt16(value, FFEndianType.BigEndian, offset, length);
        }

        public static int GetBytesToNumberInt32(this byte[] value, int offset, int length)
        {
            return FFDataTypeHelper.GetInt32(value, FFEndianType.BigEndian, offset, length);
        }

        public static uint GetBytesToNumberUInt32(this byte[] value, int offset, int length)
        {
            return FFDataTypeHelper.GetUInt32(value, FFEndianType.BigEndian, offset, length);
        }

        public static long GetBytesToNumberInt64(this byte[] value, int offset, int length)
        {
            return FFDataTypeHelper.GetInt64(value, FFEndianType.BigEndian, offset, length);
        }

        public static ulong GetBytesToNumberUInt64(this byte[] value, int offset, int length)
        {
            return FFDataTypeHelper.GetUInt64(value, FFEndianType.BigEndian, offset, length);
        }

        public static double GetBytesToNumberDouble(this byte[] value, int offset, int length)
        {
            return (double)FFDataTypeHelper.GetUInt64(value, FFEndianType.BigEndian, offset, length);
        }

        public static TimeSpan GetBytesToNumberTimeSpan(this byte[] value, int offset, int length)
        {
            return TimeSpan.Parse("00:00");
        }
        #endregion
        #endregion

        #region Get Value


        #endregion
        /// <summary>
        /// Gets the numeric value from the given byte array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <returns>Value of the given type.</returns>
        public static T GetValue<T>(this byte[] buffer, int offset, int length)
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "GetValue<T>"))
            {
                object result = default(object);

                try
                {
                    Type typeOfT = typeof(T);
                    if (offset < 0)
                    {
                        offset = buffer.Length + offset;
                    }
                    if (length < 0)
                    {
                        length = (buffer.Length - (offset - length));
                    }

                    // byte
                    if (typeOfT == typeof(byte))
                    {
                        result = buffer[offset];
                    }
                    // sbyte
                    else if (typeOfT == typeof(sbyte))
                    {
                        result = buffer[offset];
                    }
                    // ushort
                    else if (typeOfT == typeof(ushort))
                    {
                        ushort value = 0;
                        if (length > 1)
                        {
                            value = (ushort)((((ushort)buffer[offset]) << 8) +
                                                ((ushort)buffer[offset + 1]));
                        }
                        else
                        {
                            value = (ushort)buffer[offset];
                        }
                        result = value;
                    }
                    // short
                    else if (typeOfT == typeof(short))
                    {
                        short value = 0;
                        if (length > 1)
                        {
                            value = (short)((((short)buffer[offset]) << 8) +
                                                  ((short)buffer[offset + 1]));
                        }
                        else
                        {
                            value = (short)buffer[offset];
                        }
                        result = value;
                    }
                    // uint
                    else if (typeOfT == typeof(uint))
                    {
                        uint value = 0;
                        if (length >= 4)
                        {
                            value = (uint)((((uint)buffer[offset]) << 24) +
                                            (((uint)buffer[offset + 1]) << 16) +
                                            (((uint)buffer[offset + 2]) << 8) +
                                            ((uint)buffer[offset + 3]));
                        }
                        else if (length == 3)
                        {
                            value = (uint)((((uint)buffer[offset]) << 16) +
                                               (((uint)buffer[offset + 1]) << 8) +
                                               ((uint)buffer[offset + 2]));
                        }
                        else if (length == 2)
                        {
                            value = (uint)((((uint)buffer[offset]) << 8) +
                                               ((uint)buffer[offset + 1]));
                        }
                        else if (length == 1)
                        {
                            value = (uint)buffer[offset];
                        }
                        result = value;
                    }
                    // int
                    else if (typeOfT == typeof(int))
                    {
                        int value = 0;
                        if (length >= 4)
                        {
                            value = (int)((((int)buffer[offset]) << 24) +
                                            (((int)buffer[offset + 1]) << 16) +
                                            (((int)buffer[offset + 2]) << 8) +
                                            ((int)buffer[offset + 3]));
                        }
                        else if (length == 3)
                        {
                            value = (int)((((int)buffer[offset]) << 16) +
                                               (((int)buffer[offset + 1]) << 8) +
                                               ((int)buffer[offset + 2]));
                        }
                        else if (length == 2)
                        {
                            value = (int)((((int)buffer[offset]) << 8) +
                                               ((int)buffer[offset + 1]));
                        }
                        else if (length == 1)
                        {
                            value = (int)buffer[offset];
                        }
                        result = value;
                    }
                    // ulong
                    else if (typeOfT == typeof(uint))
                    {
                        ulong value = 0;
                        if (length >= 8)
                        {
                            value = (ulong)((((ulong)buffer[offset]) << 54) +
                                            (((ulong)buffer[offset + 1]) << 48) +
                                            (((ulong)buffer[offset + 2]) << 40) +
                                            (((ulong)buffer[offset + 3]) << 32) +
                                            (((ulong)buffer[offset + 4]) << 24) +
                                            (((ulong)buffer[offset + 5]) << 16) +
                                            (((ulong)buffer[offset + 6]) << 8) +
                                            ((ulong)buffer[offset + 7]));
                        }
                        else if (length == 7)
                        {
                            value = (ulong)((((ulong)buffer[offset]) << 48) +
                                            (((ulong)buffer[offset + 1]) << 40) +
                                            (((ulong)buffer[offset + 2]) << 32) +
                                            (((ulong)buffer[offset + 3]) << 24) +
                                            (((ulong)buffer[offset + 4]) << 16) +
                                            (((ulong)buffer[offset + 5]) << 8) +
                                            ((ulong)buffer[offset + 6]));
                        }
                        else if (length == 6)
                        {
                            value = (ulong)((((ulong)buffer[offset]) << 40) +
                                            (((ulong)buffer[offset + 1]) << 32) +
                                            (((ulong)buffer[offset + 2]) << 24) +
                                            (((ulong)buffer[offset + 3]) << 16) +
                                            (((ulong)buffer[offset + 4]) << 8) +
                                            ((ulong)buffer[offset + 5]));
                        }
                        else if (length == 5)
                        {
                            value = (ulong)((((ulong)buffer[offset]) << 32) +
                                            (((ulong)buffer[offset + 1]) << 24) +
                                            (((ulong)buffer[offset + 2]) << 16) +
                                            (((ulong)buffer[offset + 3]) << 8) +
                                            ((ulong)buffer[offset + 4]));
                        }
                        else if (length >= 4)
                        {
                            value = (ulong)GetValue<uint>(buffer, offset, length);
                        }
                        result = value;
                    }
                    // long
                    else if (typeOfT == typeof(uint))
                    {
                        long value = 0;
                        if (length >= 8)
                        {
                            value = (long)((((long)buffer[offset]) << 54) +
                                            (((long)buffer[offset + 1]) << 48) +
                                            (((long)buffer[offset + 2]) << 40) +
                                            (((long)buffer[offset + 3]) << 32) +
                                            (((long)buffer[offset + 4]) << 24) +
                                            (((long)buffer[offset + 5]) << 16) +
                                            (((long)buffer[offset + 6]) << 8) +
                                            ((long)buffer[offset + 7]));
                        }
                        else if (length == 7)
                        {
                            value = (long)((((long)buffer[offset]) << 48) +
                                            (((long)buffer[offset + 1]) << 40) +
                                            (((long)buffer[offset + 2]) << 32) +
                                            (((long)buffer[offset + 3]) << 24) +
                                            (((long)buffer[offset + 4]) << 16) +
                                            (((long)buffer[offset + 5]) << 8) +
                                            ((long)buffer[offset + 6]));
                        }
                        else if (length == 6)
                        {
                            value = (long)((((long)buffer[offset]) << 40) +
                                            (((long)buffer[offset + 1]) << 32) +
                                            (((long)buffer[offset + 2]) << 24) +
                                            (((long)buffer[offset + 3]) << 16) +
                                            (((long)buffer[offset + 4]) << 8) +
                                            ((long)buffer[offset + 5]));
                        }
                        else if (length == 5)
                        {
                            value = (long)((((long)buffer[offset]) << 32) +
                                            (((long)buffer[offset + 1]) << 24) +
                                            (((long)buffer[offset + 2]) << 16) +
                                            (((long)buffer[offset + 3]) << 8) +
                                            ((long)buffer[offset + 4]));
                        }
                        else if (length >= 4)
                        {
                            value = (long)GetValue<uint>(buffer, offset, length);
                        }
                        result = value;
                    }
                    // byte[]
                    else if (typeOfT == typeof(byte[]))
                    {
                        byte[] buffer2 = new byte[length];
                        Buffer.BlockCopy(buffer, offset, buffer2, 0, length);
                        result = buffer2;
                    }
                    // Enumeration
                    else if (typeOfT.IsEnum)
                    {
                        result = TypeSystem.GetValueEnumGeneric<T>(buffer[offset], default(T));
                    }
                    // String
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int i = offset; i < length; i++)
                        {
                            sb.Append("[" + buffer[i].ToString("X2") + "]");
                        }
                        result = sb.ToString();
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return (T)result;
            }
        }

        #region Set Value / Number To Bytes

        #region Number To Bytes
        public static byte[] GetNumberToBytes(this sbyte value, int length)
        {
            return new byte[] { (byte)value };
        }

        public static byte[] GetNumberToBytes(this byte value, int length)
        {
            return new byte[] { value };
        }

        public static byte[] GetNumberToBytes(this short value, int length)
        {
            return FFDataTypeHelper.GetInt16Bytes(value, FFEndianType.BigEndian);
        }

        public static byte[] GetNumberToBytes(this ushort value, int length)
        {
            return FFDataTypeHelper.GetUInt16Bytes(value, FFEndianType.BigEndian);
        }

        public static byte[] GetNumberToBytes(this int value, int length)
        {
            return FFDataTypeHelper.GetInt32Bytes(value, FFEndianType.BigEndian);
        }

        public static byte[] GetNumberToBytes(this uint value, int length)
        {
            return FFDataTypeHelper.GetUInt32Bytes(value, FFEndianType.BigEndian);
        }

        public static byte[] GetNumberToBytes(this long value, int length)
        {
            return FFDataTypeHelper.GetInt64Bytes(value, FFEndianType.BigEndian);
        }

        public static byte[] GetNumberToBytes(this ulong value, int length)
        {
            return FFDataTypeHelper.GetUInt64Bytes(value, FFEndianType.BigEndian);
        }

        public static byte[] GetNumberToBytes(this double value, int length)
        {
            return FFDataTypeHelper.GetUInt64Bytes((ulong)value, FFEndianType.BigEndian);
        }
        #endregion

        #region Set Value
        public static void SetValue(this List<byte> buffer, bool value)
        {
            buffer.Add((byte)(value ? 1 : 0));
        }

        public static void SetValue(this List<byte> buffer, byte value)
        {
            buffer.Add(value);
        }

        public static void SetValue(this List<byte> buffer, sbyte value)
        {
            buffer.Add((byte)value);
        }

        public static void SetValue(this List<byte> buffer, short value, int length)
        {
            byte[] bytes = value.GetNumberToBytes(length);
            if (length == 1) buffer.Add(bytes[1]);
            else buffer.AddRange(bytes);
        }

        public static void SetValue(this List<byte> buffer, ushort value, int length)
        {
            byte[] bytes = value.GetNumberToBytes(length);
            if (length == 1) buffer.Add(bytes[1]);
            else buffer.AddRange(bytes);
        }

        public static void SetValue(this List<byte> buffer, int value, int length)
        {
            buffer.AddRange(value.GetNumberToBytes(length));
        }

        public static void SetValue(this List<byte> buffer, uint value, int length)
        {
            buffer.AddRange(value.GetNumberToBytes(length));
        }

        public static void SetValue(this List<byte> buffer, long value, int length)
        {
            buffer.AddRange(value.GetNumberToBytes(length));
        }

        public static void SetValue(this List<byte> buffer, ulong value, int length)
        {
            buffer.AddRange(value.GetNumberToBytes(length));
        }

        public static void SetValue(this List<byte> buffer, double value, int length)
        {
            buffer.AddRange(value.GetNumberToBytes(length));
        }

        public static void SetValue(this List<byte> buffer, TimeSpan value, int length)
        {
            if (length <= 2) buffer.AddRange(value.ToString("HH").GetBCDToBytes(2));
            else if (length <= 4) buffer.AddRange(value.ToString("HHmm").GetBCDToBytes(4));
            else if (length <= 6) buffer.AddRange(value.ToString("HHmmss").GetBCDToBytes(6));
        }

        //public static void SetValue(this List<byte> buffer, string value, int length)
        //{
        //    buffer.AddRange(value.GetNumberToBytes(length));
        //}
        #endregion

        #endregion

        //public static byte[] SetValue<T>(T value)
        //{
        //    return SetValue<T>(value, 0);
        //}

        //public static byte[] SetValue<T>(T value, int length)
        //{
        //    using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "SetValue<T>"))
        //    {
        //        try
        //        {
        //            Type typeOfT = typeof(T);

        //            // byte
        //            if (typeOfT == typeof(byte))
        //            {
        //                byte value2 = (byte)Convert.ChangeType(value, typeOfT);
        //                return new byte[] { value2 };
        //            }
        //            // sbyte
        //            if (typeOfT == typeof(sbyte))
        //            {
        //                sbyte value2 = (sbyte)Convert.ChangeType(value, typeOfT);
        //                return new byte[] { (byte)value2 };
        //            }
        //            // ushort
        //            else if (typeOfT == typeof(ushort))
        //            {
        //                ushort value2 = (ushort)Convert.ChangeType(value, typeOfT);
        //                return FFDataTypeHelper.GetUInt16Bytes(value2, FFEndianType.BigEndian);
        //            }
        //            // short
        //            else if (typeOfT == typeof(short))
        //            {
        //                short value2 = (short)Convert.ChangeType(value, typeOfT);
        //                return FFDataTypeHelper.GetInt16Bytes(value2, FFEndianType.BigEndian);
        //            }
        //            // uint
        //            else if (typeOfT == typeof(uint))
        //            {
        //                uint value2 = (uint)Convert.ChangeType(value, typeOfT);
        //                return FFDataTypeHelper.GetUInt32Bytes(value2, FFEndianType.BigEndian);
        //            }
        //            // int
        //            else if (typeOfT == typeof(int))
        //            {
        //                int value2 = (int)Convert.ChangeType(value, typeOfT);
        //                return FFDataTypeHelper.GetInt32Bytes(value2, FFEndianType.BigEndian);
        //            }
        //            // ulong
        //            else if (typeOfT == typeof(ulong))
        //            {
        //                ulong value2 = (ulong)Convert.ChangeType(value, typeOfT);
        //                return FFDataTypeHelper.GetUInt64Bytes(value2, FFEndianType.BigEndian);
        //            }
        //            // long
        //            else if (typeOfT == typeof(long))
        //            {
        //                long value2 = (long)Convert.ChangeType(value, typeOfT);
        //                return FFDataTypeHelper.GetInt64Bytes(value2, FFEndianType.BigEndian);
        //            }
        //            // String
        //            else
        //            {
        //                return Encoding.ASCII.GetBytes(value.ToString());
        //            }
        //            //Type typeOfT = typeof(T);

        //            //// byte
        //            //if (typeOfT == typeof(byte))
        //            //{
        //            //    byte value2 = (byte)Convert.ChangeType(value, typeOfT);
        //            //    return new byte[] { value2 };
        //            //}
        //            //// sbyte
        //            //if (typeOfT == typeof(sbyte))
        //            //{
        //            //    sbyte value2 = (sbyte)Convert.ChangeType(value, typeOfT);
        //            //    return new byte[] { (byte)value2 };
        //            //}
        //            //// ushort
        //            //else if (typeOfT == typeof(ushort))
        //            //{
        //            //    ushort value2 = (ushort)Convert.ChangeType(value, typeOfT);
        //            //    return new byte[] 
        //            //    { 
        //            //        (byte)((value2 >> 8) & 0xFF),
        //            //        (byte)((value2) & 0xFF)
        //            //    };
        //            //}
        //            //// short
        //            //else if (typeOfT == typeof(short))
        //            //{
        //            //    short value2 = (short)Convert.ChangeType(value, typeOfT);
        //            //    return new byte[] 
        //            //    { 
        //            //        (byte)((value2 >> 8) & 0xFF),
        //            //        (byte)((value2) & 0xFF)
        //            //    };
        //            //}
        //            //// uint
        //            //else if (typeOfT == typeof(uint))
        //            //{
        //            //    uint value2 = (uint)Convert.ChangeType(value, typeOfT);
        //            //    return new byte[] 
        //            //    { 
        //            //        (byte)((value2 >> 24) & 0xFF),
        //            //        (byte)((value2 >> 16) & 0xFF),
        //            //        (byte)((value2 >> 8) & 0xFF),
        //            //        (byte)((value2) & 0xFF)
        //            //    };
        //            //}
        //            //// int
        //            //else if (typeOfT == typeof(int))
        //            //{
        //            //    int value2 = (int)Convert.ChangeType(value, typeOfT);
        //            //    return new byte[] 
        //            //    { 
        //            //        (byte)((value2 >> 24) & 0xFF),
        //            //        (byte)((value2 >> 16) & 0xFF),
        //            //        (byte)((value2 >> 8) & 0xFF),
        //            //        (byte)((value2) & 0xFF)
        //            //    };
        //            //}
        //            //// ulong
        //            //else if (typeOfT == typeof(ulong))
        //            //{
        //            //    ulong value2 = (ulong)Convert.ChangeType(value, typeOfT);
        //            //    return new byte[] 
        //            //    { 
        //            //        (byte)((value2 >> 56) & 0xFF),
        //            //        (byte)((value2 >> 48) & 0xFF),
        //            //        (byte)((value2 >> 40) & 0xFF),
        //            //        (byte)((value2 >> 32) & 0xFF),
        //            //        (byte)((value2 >> 24) & 0xFF),
        //            //        (byte)((value2 >> 16) & 0xFF),
        //            //        (byte)((value2 >> 8) & 0xFF),
        //            //        (byte)((value2) & 0xFF)
        //            //    };
        //            //}
        //            //// long
        //            //else if (typeOfT == typeof(long))
        //            //{
        //            //    long value2 = (long)Convert.ChangeType(value, typeOfT);
        //            //    return new byte[] 
        //            //    { 
        //            //        (byte)((value2 >> 56) & 0xFF),
        //            //        (byte)((value2 >> 48) & 0xFF),
        //            //        (byte)((value2 >> 40) & 0xFF),
        //            //        (byte)((value2 >> 32) & 0xFF),
        //            //        (byte)((value2 >> 24) & 0xFF),
        //            //        (byte)((value2 >> 16) & 0xFF),
        //            //        (byte)((value2 >> 8) & 0xFF),
        //            //        (byte)((value2) & 0xFF)
        //            //    };
        //            //}
        //            //// String
        //            //else
        //            //{
        //            //    return Encoding.ASCII.GetBytes(value.ToString());
        //            //}
        //        }
        //        catch (Exception ex)
        //        {
        //            method.Exception(ex);
        //        }

        //        return null;
        //    }
        //}
        #endregion

        #region Numeric BCD Parsing

        #region Set BCD Value / BCD To Bytes

        #region BCD To Bytes
        public static byte[] GetBCDToBytes(this sbyte value, int length)
        {
            return GetBCDToBytes((ulong)value, length);
        }

        public static byte[] GetBCDToBytes(this byte value, int length)
        {
            return GetBCDToBytes((ulong)value, length);
        }

        public static byte[] GetBCDToBytes(this short value, int length)
        {
            return GetBCDToBytes((ulong)value, length);
        }

        public static byte[] GetBCDToBytes(this ushort value, int length)
        {
            return GetBCDToBytes((ulong)value, length);
        }

        public static byte[] GetBCDToBytes(this int value, int length)
        {
            return GetBCDToBytes((ulong)value, length);
        }

        public static byte[] GetBCDToBytes(this uint value, int length)
        {
            return GetBCDToBytes((ulong)value, length);
        }

        public static byte[] GetBCDToBytes(this long value, int length)
        {
            return GetBCDToBytes((ulong)value, length);
        }

        public static byte[] GetBCDToBytes(this ulong value, int length)
        {
            ulong tmp = value;
            byte[] output = new byte[length];

            for (int i = 0, j = length - 1; i < length; i++, j--)
            {
                output[j] = (byte)((tmp % 10) + (16 * ((tmp / 10) % 10)));
                tmp /= 100;
            }

            return output;
        }

        public static byte[] GetBCDToBytes(this double value, int length)
        {
            return GetBCDToBytes((ulong)value, length);
        }

        public static byte[] GetBCDToBytes(this string value, int length)
        {
            return GetBCDToBytes(value, length, '0', '0');
        }

        public static byte[] GetBCDToBytes(this string value, int length, char prefix, char emptyChar)
        {
            return GetStringToBytes(value, length, prefix, emptyChar, System.Globalization.NumberStyles.Integer,
                (b) =>
                {
                    return (byte)((b % 10) + (16 * ((b / 10) % 10)));
                });
        }

        public static byte[] GetBCDToBytes(this DateTime value, int length)
        {
            if (length == 3)
                return value.ToString("ddMMyy").GetBCDToBytes(length);
            else if (length == 4)
                return value.ToString("ddMMyyyy").GetBCDToBytes(length);
            else
                return new byte[0];
        }

        public static byte[] GetBCDToBytesUSTimeZone(this DateTime value, int length)
        {
            if (length == 3)
                return value.ToString("MMddyy").GetBCDToBytes(length);
            else if (length == 4)
                return value.ToString("MMddyyyy").GetBCDToBytes(length);
            else
                return new byte[0];
        }

        public static byte[] GetBCDToBytes(this TimeSpan value, int length)
        {
            if (length == 2)
                return value.ToString("hhmm").GetBCDToBytes(length);
            else if (length == 3)
                return value.ToString("hhmmss").GetBCDToBytes(length);
            else
                return new byte[0];
        }
        #endregion

        #region Set BCD Value
        public static void SetBCDValue(this List<byte> buffer, short value, int length)
        {
            buffer.AddRange(value.GetBCDToBytes(length));
        }

        public static void SetBCDValue(this List<byte> buffer, ushort value, int length)
        {
            buffer.AddRange(value.GetBCDToBytes(length));
        }

        public static void SetBCDValue(this List<byte> buffer, int value, int length)
        {
            buffer.AddRange(value.GetBCDToBytes(length));
        }

        public static void SetBCDValue(this List<byte> buffer, uint value, int length)
        {
            buffer.AddRange(value.GetBCDToBytes(length));
        }

        public static void SetBCDValue(this List<byte> buffer, long value, int length)
        {
            buffer.AddRange(value.GetBCDToBytes(length));
        }

        public static void SetBCDValue(this List<byte> buffer, ulong value, int length)
        {
            buffer.AddRange(value.GetBCDToBytes(length));
        }

        public static void SetBCDValue(this List<byte> buffer, double value, int length)
        {
            buffer.AddRange(value.GetBCDToBytes(length));
        }

        public static void SetBCDValue(this List<byte> buffer, string value, int length)
        {
            buffer.AddRange(value.GetBCDToBytes(length));
        }

        public static void SetBCDValue(this List<byte> buffer, DateTime value, int length)
        {
            buffer.AddRange(value.GetBCDToBytes(length));
        }

        public static void SetBCDValueUSTimeZone(this List<byte> buffer, DateTime value, int length)
        {
            buffer.AddRange(value.GetBCDToBytesUSTimeZone(length));
        }

        public static void SetBCDValueTimeSpan(this List<byte> buffer, DateTime value, int length)
        {
            buffer.AddRange(value.GetBCDToBytes(length));
        }

        public static void SetBCDValueTimeSpan(this List<byte> buffer, TimeSpan value, int length)
        {
            buffer.AddRange(value.GetBCDToBytes(length));
        }
        #endregion

        #endregion

        #region Get BCD Value
        ///// <summary>
        ///// Gets the BCD double value.
        ///// </summary>
        ///// <param name="buffer">The buffer.</param>
        ///// <param name="specific">The specific.</param>
        ///// <param name="offset">The offset.</param>
        ///// <param name="length">The length.</param>
        ///// <returns>
        ///// Converted double value.
        ///// </returns>
        //public static double GetBCDDoubleValue(byte[] buffer, int specific, int offset, int length)
        //{
        //    double result = 0;

        //    for (int i = (length - 1), j = 0; i >= offset; i--, j += 2)
        //    {
        //        result += (double)((buffer[i] - specific) * Math.Pow(10, j));
        //    }

        //    return result;
        //}

        public static sbyte GetBytesToBCDInt8(this byte[] buffer, int offset, int length)
        {
            return (sbyte)GetBytesToBCDUInt64(buffer, offset, length);
        }

        public static byte GetBytesToBCDUInt8(this byte[] buffer, int offset, int length)
        {
            return (byte)GetBytesToBCDUInt64(buffer, offset, length);
        }

        public static short GetBytesToBCDInt16(this byte[] buffer, int offset, int length)
        {
            return (short)GetBytesToBCDUInt64(buffer, offset, length);
        }

        public static ushort GetBytesToBCDUInt16(this byte[] buffer, int offset, int length)
        {
            return (ushort)GetBytesToBCDUInt64(buffer, offset, length);
        }

        public static int GetBytesToBCDInt32(this byte[] buffer, int offset, int length)
        {
            return (int)GetBytesToBCDUInt64(buffer, offset, length);
        }

        public static uint GetBytesToBCDUInt32(this byte[] buffer, int offset, int length)
        {
            return (uint)GetBytesToBCDUInt64(buffer, offset, length);
        }

        public static long GetBytesToBCDInt64(this byte[] buffer, int offset, int length)
        {
            return (long)GetBytesToBCDUInt64(buffer, offset, length);
        }

        public static ulong GetBytesToBCDUInt64(this byte[] buffer, int offset, int length)
        {
            ulong output = 0;

            for (int i = offset; i < (offset + length); i++)
            {
                output *= 100;
                output += (ulong)(((buffer[i] >> 4) & 0x0F) * 10) + (ulong)(buffer[i] & 0x0F);
            }

            return output;
        }

        public static double GetBytesToBCDDouble(this byte[] buffer, int offset, int length)
        {
            return (double)GetBytesToBCDUInt64(buffer, offset, length);
        }

        public static DateTime GetBytesToBCDDateTime(this byte[] buffer, int offset, int length)
        {
            int year = 0;
            int monthOffset = 1;

            if (length > 3)
            {
                year = (int)(GetBytesToBCDUInt8(buffer, 0, 1) << 8) | (int)GetBytesToBCDUInt8(buffer, 1, 1);
                monthOffset = 2;
            }
            else
            {
                year = 2000 + GetBytesToBCDUInt8(buffer, 0, 1);
            }
            byte month = GetBytesToBCDUInt8(buffer, monthOffset++, 1);
            byte day = GetBytesToBCDUInt8(buffer, monthOffset, 1);

            return new DateTime(year, month, day);
        }

        public static DateTime GetBytesToBCDDateTimeISO(this byte[] buffer, int offset, int length)
        {
            int year = 0;
            int monthOffset = 1;

            if (length > 3)
            {
                year = (int)(GetBytesToBCDUInt8(buffer, 0, 1) << 8) | (int)GetBytesToBCDUInt8(buffer, 1, 1);
                monthOffset = 2;
            }
            else
            {
                year = 2000 + GetBytesToBCDUInt8(buffer, 0, 1);
            }
            byte month = GetBytesToBCDUInt8(buffer, monthOffset++, 1);
            byte day = GetBytesToBCDUInt8(buffer, monthOffset, 1);
            
            return new DateTime(year, month, day);
        }

        public static DateTime GetBytesToBCDDateTimeUSTimeZone(this byte[] buffer, int offset, int length)
        {
            byte month = GetBytesToBCDUInt8(buffer, 0, 1);
            byte day = GetBytesToBCDUInt8(buffer, 1, 1);
            int year = 0;

            if (length > 3)
            {
                year = (int)(GetBytesToBCDUInt8(buffer, 2, 1) << 8) | (int)GetBytesToBCDUInt8(buffer, 3, 1);
            }
            else
            {
                year = 2000 + GetBytesToBCDUInt8(buffer, 2, 1);
            }

            return new DateTime(year, month, day);
        }

        public static TimeSpan GetBytesToBCDTimeSpan(this byte[] buffer, int offset, int length)
        {
            byte hours = GetBytesToBCDUInt8(buffer, 0, 1);
            byte minutes = GetBytesToBCDUInt8(buffer, 1, 1);
            int seconds = 0;

            if (length > 2)
            {
                seconds = GetBytesToBCDUInt8(buffer, 2, 1);
            }

            return new TimeSpan(hours, minutes, seconds);
        }

        /// <summary>
        /// Gets the BCD value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="length">The length.</param>
        /// <returns>Converted value.</returns>
        public static T GetBCDValue<T>(byte[] target, int length)
          where T : struct, IFormattable
        {
            return GetBCDValue<T>(target, 0, 0, length);
        }

        /// <summary>
        /// Gets the BCD value.
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <returns>
        /// Converted value.
        /// </returns>
        public static T GetBCDValue<T>(byte[] target, int offset, int length)
            where T : struct, IFormattable
        {
            return GetBCDValue<T>(target, 0, offset, length);
        }

        /// <summary>
        /// Gets the BCD value.
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="specific">The specific.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <returns>
        /// Converted value.
        /// </returns>
        public static T GetBCDValue<T>(byte[] target, int specific, int offset, int length)
            where T : struct, IFormattable
        {
            ulong result = 0;
            if (specific == 0) result = GetBytesToBCDUInt64(target, offset, length);
            return (T)Convert.ChangeType(result, typeof(T));
        }

        /// <summary>
        /// Gets the BCD value string.
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="specific">The specific.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <returns>Converted string value.</returns>
        public static string GetBCDValueString(this byte[] target, int specific, int offset, int length)
        {
            ulong value = GetBCDValue<ulong>(target, specific, offset, length);
            return string.Format("{0:D" + (2 * length) + "}", value);
        }
        #endregion

        #region Hexadecimal
        /// <summary>
        /// Gets the BCD double hexadecimal value.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <returns>Converted double value.</returns>
        public static double GetBCDDoubleHexValue(this byte[] buffer, int offset, int length)
        {
            double result = 0;

            for (int i = (length - 1), j = 0; i >= offset; i--, j += 2)
            {
                result += (double)((buffer[i] & 0x0F) * Math.Pow(10, j));
                result += (double)(((buffer[i] >> 4) & 0x0F) * Math.Pow(10, (double)(j + 1)));
            }

            return result;
        }

        /// <summary>
        /// Gets the BCD hexadecimal value.
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="length">The length.</param>
        /// <returns>Converted double value.</returns>
        public static T GetBCDHexValue<T>(byte[] target, int length)
            where T : struct, IConvertible
        {
            return GetBCDHexValue<T>(target, 0, length);
        }

        /// <summary>
        /// Gets the BCD hexadecimal value.
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <returns>Converted double value.</returns>
        public static T GetBCDHexValue<T>(byte[] target, int offset, int length)
            where T : struct, IConvertible
        {
            double doubleResult = GetBCDDoubleHexValue(target, offset, offset + length);
            return (T)Convert.ChangeType(doubleResult, typeof(T));
        }
        #endregion

        #endregion

        #region String Parsing
        public static bool[] GetBooleanArrayValues(this byte[] buffer, int offset, int length)
        {
            bool[] result = new bool[length];
            for (int i = offset, j = 0; i < (offset + length); i++, j++)
            {
                result[j] = TypeSystem.GetValueBoolSimple(buffer[i]);
            }
            return result;
        }

        public static byte[] GetBooleanArrayToBytes(this bool[] buffer, int length)
        {
            byte[] result = new byte[length];
            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer == null) result[i] = 0;
                else result[i] = (byte)(buffer[i] ? 1 : 0);
            }
            return result;
        }

        public static void SetBooleanArrayValues(this List<byte> buffer, bool[] values, int length)
        {
            buffer.AddRange(values.GetBooleanArrayToBytes(length));
        }

        public static byte[] GetStringToBytes(this byte value, int length)
        {
            return new byte[] { value };
        }

        public static byte[] GetStringToBytes(this short value, int length)
        {
            return GetStringToBytes((ulong)value, length);
        }

        public static byte[] GetStringToBytes(this ushort value, int length)
        {
            return GetStringToBytes((ulong)value, length);
        }

        public static byte[] GetStringToBytes(this int value, int length)
        {
            return GetStringToBytes((ulong)value, length);
        }

        public static byte[] GetStringToBytes(this uint value, int length)
        {
            return GetStringToBytes((ulong)value, length);
        }

        public static byte[] GetStringToBytes(this long value, int length)
        {
            return GetStringToBytes((ulong)value, length);
        }

        public static byte[] GetStringToBytes(this ulong value, int length)
        {
            byte[] result = new byte[length];
            for (int i = 0, j = length - 1; i < length; i++, j--)
            {
                result[j] = ((byte)(value % 10));
                value /= 10;
            }
            return result;
        }

        public static byte[] GetStringToBytes(this double value, int length)
        {
            return GetStringToBytes((ulong)value, length);
        }

        public static byte[] GetStringToBytes(this string value, int length, char prefix, char emptyChar, System.Globalization.NumberStyles numberStyle)
        {
            return GetStringToBytes(value, length, prefix, emptyChar, numberStyle, (b) => { return b; });
        }

        public static byte[] GetStringToBytes(this string value, int length, char prefix, char emptyChar, System.Globalization.NumberStyles numberStyle, Func<byte, byte> action)
        {
            int newLength = (length * 2);
            if (value.IsEmpty()) value = new string(emptyChar, newLength);

            int actualLength = value.Length;
            if (actualLength > newLength) actualLength = newLength;
            byte[] result = new byte[length];
            StringBuilder sb = new StringBuilder();

            if (actualLength % 2 != 0)
            {
                sb.Append(prefix);
            }
            for (int i = 0, j = 0; i < actualLength; i++)
            {
                sb.Append(value[i]);
                if (sb.Length == 2)
                {
                    byte b = 0;
                    byte.TryParse(sb.ToString(), numberStyle, null, out b);
                    result[j++] = action(b);
                    if (j > length) break;
                    sb.Clear();
                }
            }
            return result;
        }

        public static byte[] GetMACAddressBytesValue(this string macAddress)
        {
            string[] values = macAddress.Split(':');
            if (values.Length == 6)
            {
                byte[] result = new byte[6];
                for (int i = 0; i < 6; i++)
                {
                    byte b = 0;
                    byte.TryParse(values[i], NumberStyles.HexNumber, null, out b);
                    result[i] = b;
                }
                return result;
            }
            return new byte[0] { };
        }

        public static byte[] ConvertToASCII(this string buffer, int offset, int length)
        {
            return ConvertToASCII(ASCII_ENCODING.GetBytes(buffer), offset, length);
        }

        public static byte[] ConvertToASCII(this byte[] buffer, int offset, int length)
        {
            for (int i = offset; i < (offset + length); i++)
            {
                buffer[i] |= 0x30;
            }
            return buffer;
        }

        public static string GetHexStringValue(this byte[] buffer)
        {
            return GetHexStringValue(buffer, 0, buffer.Length, '\0');
        }

        public static string GetHexStringValue(this byte[] buffer, char separator)
        {
            return GetHexStringValue(buffer, 0, buffer.Length, separator);
        }

        public static string GetHexStringValue(this byte[] buffer, int offset, int length)
        {
            return GetHexStringValue(buffer, offset, length, '\0');
        }

        public static string GetHexStringValue(this byte[] buffer, int offset, int length, char separator)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = offset; i < length; i++)
            {
                if (separator != '\0')
                {
                    if (sb.Length > 0) sb.Append(separator);
                }
                sb.Append(buffer[i].ToString("X2"));

            }
            return sb.ToString();
        }

        public static byte[] GetHexBytesValue(this string value, int length)
        {
            return GetHexBytesValue(value, length, '0', '0');
        }

        public static byte[] GetHexBytesValue(this string value, int length, char prefix, char emptyChar)
        {
            return GetStringToBytes(value, length, prefix, emptyChar, System.Globalization.NumberStyles.HexNumber);
        }

        public static string GetASCIIStringValue(this byte[] buffer)
        {
            return GetASCIIStringValue(buffer, 0, buffer.Length);
        }

        public static string GetASCIIStringValue(this byte[] buffer, int offset, int length)
        {
            StringBuilder sb = new StringBuilder();
            if (length <= 0) length = (buffer.Length - offset);
            for (int i = offset; i < (offset + length); i++)
            {
                sb.Append(Convert.ToChar(buffer[i]));
            }
            return sb.ToString();
        }

        public static string GetASCIIStringValueTrim(this byte[] buffer, int offset, int length)
        {
            return GetASCIIStringValue(buffer, offset, length).Trim();
        }

        public static byte[] GetASCIIBytesValue(this string value)
        {
            if (value.IsEmpty()) return new byte[0];
            return ASCII_ENCODING.GetBytes(value);
        }

        public static byte[] GetASCIIBytesValue(this string value, int maxLength)
        {
            return GetASCIIBytesValue(value, maxLength, '0');
        }

        public static byte[] GetASCIIBytesValueSpace(this string value, int maxLength)
        {
            return GetASCIIBytesValue(value, maxLength, ' ');
        }

        public static byte[] GetASCIIBytesValue(this string value, int maxLength, char paddingChar)
        {
            byte[] result = value.GetASCIIBytesValue();
            if (result.Length > maxLength) return result.CopyToBuffer(0, maxLength);
            else if (result.Length < maxLength)
            {
                int padLength = maxLength - result.Length;
                int remainingLength = maxLength - padLength;

                byte[] result1 = new byte[maxLength];
                byte[] padding = ASCII_ENCODING.GetBytes(new string(paddingChar, padLength));
                Buffer.BlockCopy(padding, 0, result1, 0, padLength);

                if (remainingLength > 0)
                {
                    Buffer.BlockCopy(result, 0, result1, padLength, remainingLength);
                }
                result = result1;
            }
            return result;
        }

        public static void SetASCIIBytesValue(this List<byte> buffer, string value, int maxLength)
        {
            buffer.AddRange(GetASCIIBytesValue(value, maxLength));
        }

        public static void SetASCIIBytesValueWithLength(this List<byte> buffer, string value, int maxLength)
        {
            byte[] result = GetASCIIBytesValue(value, maxLength);
            buffer.Add((byte)result.Length);
            buffer.AddRange(result);
        }

        public static void ConvertBytesToHexString(this byte[] buffer, StringBuilder sb, string prefix)
        {
            if (buffer != null)
            {
                if (!prefix.IsEmpty()) sb.Append(prefix);
                for (int i = 0; i < buffer.Length; i++)
                {
                    sb.AppendFormat("[{0}]", buffer[i].ToString("X2"));
                }
            }
        }

        public static string GetConvertBytesToHexString(this byte[] buffer, string prefix)
        {
            if (buffer == null) return string.Empty;
            StringBuilder sb = new StringBuilder();
            ConvertBytesToHexString(buffer, sb, prefix);
            return sb.ToString();
        }

        public static void WriteLogStringLine(StringBuilder sb, string prefix)
        {
            sb.AppendLine(prefix + new string(HEADER_SEPARATOR, HEADER_SEPARATOR_LEN));
        }

        public static byte[] GetBufferFromHexString(string input)
        {
            int idx = input.IndexOf("[");
            if (idx > 0)
            {
                input = input.Substring(idx, input.Length - idx);
            }
            string data = input.Replace("[", "").Replace("]", ",");
            string[] splitted = data.Split(new char[] { ',' });

            byte[] buffer = new byte[splitted.Length - 1];
            for (int i = 0; i < splitted.Length - 1; i++)
            {
                int val = 0;
                try
                {
                    val = Convert.ToInt32(splitted[i], 16);
                }
                catch
                {
                    val = Convert.ToInt32(splitted[i], 10);
                }
                buffer[i] = (byte)val;
            }
            return buffer;
        }

        public static string GetMaxCharacters(this string source, int maxLength)
        {
            if (source.IsEmpty() || source.Length <= maxLength) return source;
            return source.Substring(0, maxLength);
        }

        #endregion
    }
}
