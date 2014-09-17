using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Server.ExecutionSteps;

namespace BMC.ExComms.Server.Handlers.Security
{
    [Flags]
    internal enum FreeformSecurityFlags
    {
        None = 0,
        LocalPartialKey = 2,
        OtherPartialKey = 4,
        CommonKey = 8,
    }

    internal class FreeformSecurityTable
        : DisposableObject
    {
        private readonly Random _rnd = new Random(1);

        internal const int KEY_SIZE = 8;
        internal const int KEY_SIZE_2 = 9;

        public FreeformSecurityFlags Flags = FreeformSecurityFlags.None;
        public int Method = (int)FreeformSecurityMethodTypes.Alpha1;

        public readonly byte[] CommonKey = new byte[KEY_SIZE];
        public readonly byte[] LocalRandomNumber = new byte[KEY_SIZE_2];

        public readonly byte[] OtherPartialKey = new byte[KEY_SIZE_2];
        public readonly byte[] LocalPartialKey = new byte[KEY_SIZE_2];

        public byte PartialKeyIndex = 8;

        internal FreeformSecurityTable()
        {
            this.GenerateAndStoreRandomNumber();
        }

        protected override void ToString(StringBuilder sb)
        {
            base.ToString(sb);

            sb.AppendLine("Flags : " + this.Flags.ToString());
            sb.AppendLine("Method : " + this.Method.ToString());
            sb.AppendLine("Local Random Number : " + this.LocalRandomNumber.GetConvertBytesToHexString(string.Empty));
            sb.AppendLine("Local Partial Key : " + this.LocalPartialKey.GetConvertBytesToHexString(string.Empty));
            sb.AppendLine("Other Partial Key : " + this.OtherPartialKey.GetConvertBytesToHexString(string.Empty));
            sb.AppendLine("Common Key : " + this.CommonKey.GetConvertBytesToHexString(string.Empty));
        }

        public byte[] GenerateRandomNumberLowHigh()
        {
            byte[] randomBytes = new byte[2];
            _rnd.NextBytes(randomBytes);
            return randomBytes;
        }

        public byte GenerateRandomNumber()
        {
            byte[] bytes = Guid.NewGuid().ToByteArray();
            return bytes[_rnd.Next(0, 15)];
        }

        public void GenerateAndStoreRandomNumber()
        {
            for (int i = 0; i < KEY_SIZE_2; i++)
            {
                this.LocalRandomNumber[i] = this.GenerateRandomNumber();
            }
        }
    }

    internal enum FreeformSecurityMethodTypes
    {
        None = 0,
        Alpha1 = 1,
        Alpha2 = 2,
    }

    internal class FreeformSecurityTableCollection
        : SortedDictionary<SECURITY_KEY_INDEX, FreeformSecurityTable>
    {
        private const string DYN_MODULE_NAME = "FreeformSecurityTableCollection";

        internal FreeformSecurityTableCollection()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "Initialize"))
            {
                try
                {
                    Array values = Enum.GetValues(typeof(SECURITY_KEY_INDEX));
                    foreach (SECURITY_KEY_INDEX value in values)
                    {
                        if (!this.ContainsKey(value))
                        {
                            this.Add(value, new FreeformSecurityTable());
                        }
                    }

                    if (this.ContainsKey(SECURITY_KEY_INDEX.NO_KEY))
                    {
                        this.Remove(SECURITY_KEY_INDEX.NO_KEY);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }
    }

    internal static class FreeformEncryptionHelper
    {
        private const string DYN_MODULE_NAME = "FreeformEncryptionHelper";

        private const int TOTAL_KEYS = 32;
        private const int FK_1ST = 3;       /* First floating key */
        private const int FK_LST = 12;      /* Last floating key  */

        private const int LOCAL_KEY = 1;
        private const int OTHER_KEY = 2;
        private const int COMMON_KEY = 4;

        private static readonly IDictionary<int, SECURITY_KEY_INDEX> _securityKeys = null;

        private static readonly byte[] fudge1 = new byte[] {  127, 23, 17, 29, 
                                                              37, 53, 71, 43, 
                                                              37, 241, 13, 97, 
                                                              107, 101, 179, 137, 59
                                                            };

        private static readonly byte[] alpha1 = new byte[] { 233, 13, 197, 19, 107, 223, 31, 11, 251 };
        private static readonly byte[] alpha2 = new byte[] { 41, 71, 211, 79, 229, 73, 83, 101, 17 };
        private static readonly byte[] alphan = new byte[] { 107, 177, 229, 56, 139, 213, 159, 37, 191,
                                                             61, 19, 206, 239, 109, 71, 199, 167, 242,
                                                             76, 43, 176, 29, 163, 130, 11, 97, 54,
                                                             127, 145, 33};

        private static readonly byte[] LOCAL_RANDOM_NUM = { 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7, 0x8 };

        static FreeformEncryptionHelper()
        {
            _securityKeys = new SortedDictionary<int, SECURITY_KEY_INDEX>()
            {
                { (int)FF_AppId_SessionIds.ECash, SECURITY_KEY_INDEX.EFT_KEY },
                { (int)FF_AppId_SessionIds.Security, SECURITY_KEY_INDEX.TICKET_KEY },
                { (int)FF_AppId_SessionIds.Tickets, SECURITY_KEY_INDEX.TICKET_KEY },
            };
        }

        internal static byte MakeAuthenticationByte(byte[] src)
        {
            return FreeformHelper.CalculateCheckSum(src);
        }

        internal static byte MakeAuthenticationByte(List<byte> src)
        {
            return FreeformHelper.CalculateCheckSum(src, 0, src.Count);
        }

        internal static bool Encrypt(this FreeformSecurityTableCollection secTables, FF_FlowInitiation flowInitiation, SECURITY_KEY_INDEX key, ref byte[] data)
        {
            switch (flowInitiation)
            {
                case FF_FlowInitiation.Gmu:
                    return EncryptGmu(secTables, key, ref data);

                default:
                    return EncryptHost(secTables, key, ref data);
            }
        }

        internal static bool EncryptGmu(this FreeformSecurityTableCollection secTables, SECURITY_KEY_INDEX key, ref byte[] data)
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "Encrypt"))
            {
                if (data == null || data.Length == 0) return false;

                try
                {
                    if (!secTables.ContainsKey(key)) return false;

                    FreeformSecurityTable secTable = secTables[key];
                    if ((secTable.Flags & FreeformSecurityFlags.CommonKey) != FreeformSecurityFlags.CommonKey) return false;

                    int j = 0;
                    if (secTable.Method == (int)FreeformSecurityMethodTypes.Alpha2)
                    {
                        j = 5;
                    }

                    for (int i = 0; i < data.Length; i++)
                    {
                        data[i] ^= (byte)(secTable.CommonKey[(i + j) & 0x7] ^ fudge1[i & 0xF]);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                    return false;
                }

                return true;
            }
        }

        internal static bool EncryptHost(this FreeformSecurityTableCollection secTables, SECURITY_KEY_INDEX key, ref byte[] data)
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "Encrypt"))
            {
                if (data == null || data.Length == 0) return false;

                try
                {
                    if (!secTables.ContainsKey(key)) return false;

                    FreeformSecurityTable secTable = secTables[key];
                    if ((secTable.Flags & FreeformSecurityFlags.CommonKey) != FreeformSecurityFlags.CommonKey) return false;

                    for (int i = 0; i < data.Length; ++i)
                    {
                        data[i] ^= (byte)(secTable.CommonKey[i & 0x7] ^ i);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                    return false;
                }

                return true;
            }
        }

        internal static bool Decrypt(this FreeformSecurityTableCollection secTables, FF_FlowInitiation flowInitiation, SECURITY_KEY_INDEX key, ref byte[] data)
        {
            switch (flowInitiation)
            {
                case FF_FlowInitiation.Gmu:
                    return DecryptGmu(secTables, key, ref data);

                default:
                    return DecryptHost(secTables, key, ref data);
            }
        }

        internal static bool DecryptGmu(this FreeformSecurityTableCollection secTables, SECURITY_KEY_INDEX key, ref byte[] data)
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "Decrypt"))
            {
                if (data == null || data.Length == 0) return false;

                try
                {
                    if (!secTables.ContainsKey(key)) return false;

                    FreeformSecurityTable secTable = secTables[key];
                    if ((secTable.Flags & FreeformSecurityFlags.CommonKey) != FreeformSecurityFlags.CommonKey) return false;

                    for (int i = 0; i < data.Length; i++)
                    {
                        data[i] ^= (byte)(secTable.CommonKey[i & 0x7] ^ i);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                    return false;
                }

                return true;
            }
        }

        internal static bool DecryptHost(this FreeformSecurityTableCollection secTables, SECURITY_KEY_INDEX key, ref byte[] data)
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "Decrypt"))
            {
                if (data == null || data.Length == 0) return false;

                try
                {
                    if (!secTables.ContainsKey(key)) return false;

                    FreeformSecurityTable secTable = secTables[key];
                    if ((secTable.Flags & FreeformSecurityFlags.CommonKey) != FreeformSecurityFlags.CommonKey) return false;

                    int j = 0;
                    if (secTable.Method == (int)FreeformSecurityMethodTypes.Alpha2)
                    {
                        j = 5;
                    }

                    for (int i = 0; i < data.Length; ++i)
                    {
                        data[i] ^= (byte)(secTable.CommonKey[(i + j) & 0x7] ^ fudge1[i & 0xF]);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                    return false;
                }

                return true;
            }
        }

        internal static bool ClearKeyData(this FreeformSecurityTableCollection secTables, SECURITY_KEY_INDEX key)
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "Decrypt"))
            {
                try
                {
                    if (!secTables.ContainsKey(key)) return false;
                    FreeformSecurityTable secTable = secTables[key];

                    // clear the flags
                    secTable.Flags = 0;

                    // clear common key
                    for (int i = 0; i < secTable.CommonKey.Length; i++)
                    {
                        secTable.CommonKey[i] = 0;
                    }

                    // clear local key
                    for (int i = 0; i < secTable.LocalRandomNumber.Length; i++)
                    {
                        secTable.LocalRandomNumber[i] = 0;
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                    return false;
                }

                return true;
            }
        }

        internal static byte[] CreatePartialKeyGmu(this FreeformSecurityTableCollection secTables, SECURITY_KEY_INDEX key)
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "CreatePartialKey"))
            {
                byte[] result = null;

                try
                {
                    if (!secTables.ContainsKey(key)) return result;

                    FreeformSecurityTable secTable = secTables[key];
                    byte[] keys = alphan;
                    int j = 0;

                    // initial method
                    if (secTable.Method == (int)FreeformSecurityMethodTypes.None)
                        secTable.Method = (int)FreeformSecurityMethodTypes.Alpha1;

                    if (secTable.Method == (int)FreeformSecurityMethodTypes.Alpha1)
                        keys = alpha1;
                    else if (secTable.Method == (int)FreeformSecurityMethodTypes.Alpha2)
                        keys = alpha2;
                    else
                        j = secTable.Method;

                    // 0 - 7
                    for (int i = 0; i < FreeformSecurityTable.KEY_SIZE; i++)
                    {
                        secTable.LocalPartialKey[i] = Convert.ToByte((exp_mod(keys[i + j], secTable.LocalRandomNumber[i]))
                                                        ^ Convert.ToByte(0x50 + i));
                    }

                    // 8
                    j = FreeformSecurityTable.KEY_SIZE;
                    secTable.LocalPartialKey[j] = Convert.ToByte(((secTable.LocalRandomNumber[j] & 0xC3) |
                                                    (secTable.Method << 2)) ^ 0xA5);

                    // indicates that local partial key is generated and common key is unusable
                    secTable.Flags = (secTable.Flags | FreeformSecurityFlags.LocalPartialKey) & ~FreeformSecurityFlags.CommonKey;
                    result = secTable.LocalPartialKey;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                    return null;
                }

                return result;
            }
        }

        internal static byte[] CreatePartialKeyHost(this FreeformSecurityTableCollection secTables, SECURITY_KEY_INDEX key)
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "CreatePartialKey"))
            {
                byte[] result = null;

                try
                {
                    if (!secTables.ContainsKey(key)) return result;

                    FreeformSecurityTable secTable = secTables[key];
                    byte[] keys = alphan;
                    int j = 0;

                    if (secTable.Method > (FK_1ST - 1))
                        secTable.Method = ((0xC3 * 100) % (FK_LST - FK_1ST + 1)) + FK_1ST;

                    if (secTable.Method == (int)FreeformSecurityMethodTypes.Alpha1)
                        keys = alpha1;
                    else if (secTable.Method == (int)FreeformSecurityMethodTypes.Alpha2)
                        keys = alpha2;
                    else
                        j = secTable.Method;

                    int i = 0;
                    for (int k = 3; i < 8; ++i, k = (k + 3) & 7)
                    {
                        secTable.LocalPartialKey[k] = exp_mod(keys[i + j], secTable.LocalRandomNumber[i]);
                    }
                    secTable.LocalPartialKey[8] = (byte)((0xC3 | (secTable.Method << 2)) ^ 0xA5);

                    // indicates that local partial key is generated and common key is unusable
                    secTable.Flags = (secTable.Flags | FreeformSecurityFlags.LocalPartialKey) & ~FreeformSecurityFlags.CommonKey;
                    result = secTable.LocalPartialKey;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                    return null;
                }

                return result;
            }
        }

        internal static bool StoreOtherPartialKeyGmu(this FreeformSecurityTableCollection secTables, SECURITY_KEY_INDEX key, byte[] partialKey)
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "StoreOtherPartialKey"))
            {
                try
                {
                    if (!secTables.ContainsKey(key)) return false;

                    FreeformSecurityTable secTable = secTables[key];
                    int length = FreeformSecurityTable.KEY_SIZE;
                    for (int i = 0, j = length - 1; i < length; i++, j = ((j + 3) & 0x7))
                    {
                        secTable.OtherPartialKey[j] = partialKey[i];
                    }

                    // indicates that other key is generated and common key is unusable
                    secTable.Flags = (secTable.Flags | FreeformSecurityFlags.OtherPartialKey) & ~FreeformSecurityFlags.CommonKey;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                    return false;
                }

                return true;
            }
        }

        internal static bool StoreOtherPartialKeyHost(this FreeformSecurityTableCollection secTables, SECURITY_KEY_INDEX key, byte[] partialKey)
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "StoreOtherPartialKey"))
            {
                try
                {
                    if (!secTables.ContainsKey(key)) return false;

                    FreeformSecurityTable secTable = secTables[key];
                    int length = FreeformSecurityTable.KEY_SIZE_2;
                    for (int i = 0; i < length; ++i)
                    {
                        secTable.OtherPartialKey[i] = (byte)(partialKey[i] ^ ((byte)(0x50 + i)));
                    }

                    // method
                    secTable.Method = (((partialKey[length - 1] ^ 0xA5) >> 2) & 0x0F);

                    // indicates that other key is generated and common key is unusable
                    secTable.Flags = (secTable.Flags | FreeformSecurityFlags.OtherPartialKey) & ~FreeformSecurityFlags.CommonKey;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                    return false;
                }

                return true;
            }
        }

        internal static bool CreateCommonKey(this FreeformSecurityTableCollection secTables, SECURITY_KEY_INDEX key)
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "CreateCommonKey"))
            {
                try
                {
                    if (!secTables.ContainsKey(key)) return false;

                    FreeformSecurityTable secTable = secTables[key];
                    if (secTable.Flags != (FreeformSecurityFlags.LocalPartialKey |
                                            FreeformSecurityFlags.OtherPartialKey)) return false;

                    byte[] localNum = secTable.LocalRandomNumber;
                    for (int i = 0; i < FreeformSecurityTable.KEY_SIZE; i++)
                    {
                        secTable.CommonKey[i] = exp_mod(secTable.OtherPartialKey[i], localNum[i]);
                    }

                    // all the keys are now available
                    secTable.Flags |= (FreeformSecurityFlags.LocalPartialKey |
                                       FreeformSecurityFlags.OtherPartialKey |
                                       FreeformSecurityFlags.CommonKey);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                    return false;
                }

                return true;
            }
        }

        internal static byte GenerateRandomNumber(this FreeformSecurityTableCollection secTables, SECURITY_KEY_INDEX key)
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "GenerateRandomNumber"))
            {
                byte result = 0;

                try
                {
                    if (!secTables.ContainsKey(key)) return result;
                    result = secTables[key].GenerateRandomNumber();
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                    return 0;
                }

                return result;
            }
        }

        internal static byte GenerateRandomNumber(this FreeformSecurityTable secTable)
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "GenerateRandomNumber"))
            {
                byte result = 0;

                try
                {
                    int count = 10;
                    while (count-- > 0)
                    {
                        byte[] lohi = secTable.GenerateRandomNumberLowHigh();
                        secTable.PartialKeyIndex = (byte)(secTable.PartialKeyIndex > 0 ? secTable.PartialKeyIndex - 1 : 8);
                        result = (byte)(lohi[0] ^ lohi[1]);// ^ secTable.OtherPartialKey[secTable.PartialKeyIndex]);
                        if (result > 0) break;
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                    return 0;
                }

                return result;
            }
        }

        private static byte exp_mod(byte arg, byte Exp) // to be modified
        {
            //arg = 0xE9;
            //Exp = 0x1;
            Int32 ie_ctr = 8;
            UInt32 uiresult = Convert.ToUInt32(arg);
            UInt32 uiexp = Convert.ToUInt32(Exp);

            if (uiexp == 0) return 1;

            for (; (ie_ctr-- != 0) && (Convert.ToInt32(uiexp & 0x80) == 0); uiexp <<= 1) ;


            while (ie_ctr-- > 0)
            {
                uiexp <<= 1;
                uiresult *= uiresult;
                uiresult %= 251;
                uiresult = uiresult & 0xFF; /*To avoid Arithmetic overflow and we are interested on LSB*/
                if (Convert.ToInt32(uiexp & 0x80) == 0x80)
                {
                    uiresult *= Convert.ToUInt32(arg);
                    uiresult %= 251;
                }
                uiresult = uiresult & 0xFF;
            }
            return Convert.ToByte(uiresult & 0xFF); /*get the Least byte*/
        }

        internal static SECURITY_KEY_INDEX GetSecurityKeyIndex(this IFreeformEntity_Msg message)
        {
            int sessionId = (int)message.SessionID;            
            SECURITY_KEY_INDEX keyIndex = SECURITY_KEY_INDEX.NO_KEY;
            if (_securityKeys.ContainsKey(sessionId)) keyIndex = _securityKeys[sessionId];
            return keyIndex;
        }

        internal static void InitSecurityData(IFreeformEntity_Msg message, FFTgt_B2B_Security_Data securityData)
        {
            FFTgt_B2B_Security tgt = new FFTgt_B2B_Security_ResponseRequired()
            {
                SecurityData = securityData,
            };
            message.AddTarget(tgt);
        }

        internal static IFreeformEntity_Msg InitKeyExchangeStartG2H_SIM(this FreeformSecurityTableCollection secTables, string ipAddress, FF_AppId_SessionIds sessionId, int transactionId)
        {
            FreeformEntity_Msg message = FreeformEntityFactory.CreateEntity<FFMsg_G2H>(FF_FlowDirection.G2H,
                             new FFCreateEntityRequest_G2H_ResponseRequired()
                             {
                                 Command = FF_AppId_G2H_Commands.ResponseRequest,
                                 MessageType = FF_AppId_G2H_MessageTypes.FreeForm,
                                 SessionID = sessionId,
                                 TransactionID = transactionId,
                                 IPAddress = ipAddress,
                             });
            InitSecurityData(message, new FFTgt_B2B_Security_KeyExchange_Request());
            return message;
        }

        internal static IFreeformEntity_Msg InitKeyExchangePartialKeyH2G_GMU(this FreeformSecurityTableCollection secTables,
                                                                        IFreeformEntity_Msg request)
        {
            SECURITY_KEY_INDEX keyIndex = FreeformEncryptionHelper.GetSecurityKeyIndex(request);
            byte[] hostPartialKey = secTables.CreatePartialKeyHost(keyIndex);
            IFreeformEntity_Msg message = request.CopyTo(FF_FlowDirection.H2G, new FFCreateEntityRequest_H2G_ResponseRequired()
            {
                PollCode = FF_AppId_H2G_PollCodes.FreeformResponse,
            });
            InitSecurityData(message,
                new FFTgt_B2B_Security_KeyExchange_PartialKey()
                {
                    PartialKey = hostPartialKey,
                });
            return message;
        }

        internal static IFreeformEntity_Msg InitKeyExchangeEndG2H_SIM(this FreeformSecurityTableCollection secTables,
                                                                      IFreeformEntity_Msg request)
        {
            // store the partial key of the host
            FFTgt_B2B_Security_KeyExchange_PartialKey tgt = request.EntityPrimaryTarget as FFTgt_B2B_Security_KeyExchange_PartialKey;
            SECURITY_KEY_INDEX keyIndex = FreeformEncryptionHelper.GetSecurityKeyIndex(request);
            byte[] gmuPartialKey = secTables.CreatePartialKeyGmu(keyIndex);
            secTables.StoreOtherPartialKeyGmu(keyIndex, tgt.PartialKey);
            secTables.CreateCommonKey(keyIndex);

            // send the partial key of the gmu
            IFreeformEntity_Msg response = request.CopyTo(FF_FlowDirection.G2H, new FFCreateEntityRequest_G2H_ResponseRequired()
            {
                MessageType = FF_AppId_G2H_MessageTypes.FreeForm,
                Command = FF_AppId_G2H_Commands.ResponseRequest,
                SkipTransactionId = true,
            });
            FFTgt_B2B_Security tgt2 = new FFTgt_B2B_Security()
            {
                SecurityData = new FFTgt_B2B_Security_KeyExchange_End()
                {
                    PartialKey = gmuPartialKey,
                }
            };
            response.AddTarget(tgt2);
            return response;
        }

        internal static IFreeformEntity_Msg InitKeyExchangeStatusH2G_GMU(this FreeformSecurityTableCollection secTables,
                                                                  IFreeformEntity_Msg request)
        {
            // store the partial key of the gmu
            FFTgt_B2B_Security_KeyExchange_End tgt = request.EntityPrimaryTarget as FFTgt_B2B_Security_KeyExchange_End;
            SECURITY_KEY_INDEX keyIndex = FreeformEncryptionHelper.GetSecurityKeyIndex(request);
            secTables.StoreOtherPartialKeyHost(keyIndex, tgt.PartialKey);
            secTables.CreateCommonKey(keyIndex);

            IFreeformEntity_Msg message = request.CopyTo(FF_FlowDirection.H2G, new FFCreateEntityRequest_H2G_ResponseRequired()
            {
                PollCode = FF_AppId_H2G_PollCodes.FreeformNoResponse,
            });
            InitSecurityData(message,
                new FFTgt_B2B_Security_KeyExchange_Status()
                {
                    Status = (request.EntityPrimaryTarget is FFTgt_B2B_Security_PartialKey ?
                                FF_AppId_ResponseStatus_Types.Success :
                                FF_AppId_ResponseStatus_Types.Fail),
                });
            return message;
        }

        //internal static void InitKeyExchangeStart(FreeformEntity_Msg message)
        //{
        //    InitSecurityData(message, new FFTgt_B2B_Security_KeyExchange_Request());
        //}

        //internal static void InitKeyExchangePartialKey(FreeformEntity_Msg message, byte[] partialKey)
        //{
        //    InitSecurityData(message, new FFTgt_B2B_Security_KeyExchange_PartialKey()
        //    {
        //        PartialKey = partialKey,
        //    });
        //}

        //internal static void InitKeyExchangeEnd(FreeformEntity_Msg message, byte[] partialKey)
        //{
        //    InitSecurityData(message, new FFTgt_B2B_Security_KeyExchange_End()
        //    {
        //        PartialKey = partialKey,
        //    });
        //}

        //internal static void InitKeyExchangeStatus(FreeformEntity_Msg message, FF_AppId_ResponseStatus_Types status)
        //{
        //    InitSecurityData(message, new FFTgt_B2B_Security_KeyExchange_Status()
        //    {
        //        Status = status,
        //    });
        //}
    }

    [Export("FreeformEncryptionFactory", typeof(IFreeformEncryptionFactory))]
    internal sealed class FreeformEncryptionFactory
        : DisposableObject,
        IFreeformEncryptionFactory
    {
        private delegate void EncryptDecryptHandler(FreeformSecurityTableCollection securityTables, FF_FlowInitiation flowInitiation, byte[] source, List<byte> destination);

        internal FreeformEncryptionFactory() { }

        public List<byte> Encrypt(IFreeformEntity_Msg message, IFreeformEntity_MsgTgt target, List<byte> buffer)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Encrypt"))
            {
                List<byte> result = new List<byte>();

                try
                {
                    if (message == null ||
                        buffer == null ||
                        buffer.Count == 0) return result;

                    byte[] source = buffer.ToArray();
                    FreeformSecurityTableCollection securityTables = FFMsgHandlerFactory.Current.GetSecurityTables(message.IpAddress);
                    SECURITY_KEY_INDEX securityKey = message.GetSecurityKeyIndex();
                    FF_AppId_Encryption_Types encryptionType = target.EncryptionType;
                    if (target.PrimaryTarget != null &&
                        target.PrimaryTarget.EncryptionType != FF_AppId_Encryption_Types.None)
                    {
                        encryptionType = target.PrimaryTarget.EncryptionType;
                    }

                    FF_FlowInitiation flowInitiation = message.FlowInitiation;
                    switch (encryptionType)
                    {
                        case FF_AppId_Encryption_Types.Standard:
                            {
                                FreeformEncryptionHelper.Encrypt(securityTables, flowInitiation, securityKey, ref source);
                                result.Add((byte)ENCRYP_TARGETS.ET_SdsEncryption);
                                result.AddRange(source);
                            }
                            break;

                        case FF_AppId_Encryption_Types.AuthByteClearData:
                        case FF_AppId_Encryption_Types.AuthByteEncryptedData:
                            {
                                byte[] authenticationByte = new byte[] { FreeformEncryptionHelper.MakeAuthenticationByte(source) };
                                if (encryptionType == FF_AppId_Encryption_Types.AuthByteEncryptedData)
                                {
                                    result.Add((byte)ENCRYP_TARGETS.ET_EFTStyleEncryption);
                                    FreeformEncryptionHelper.Encrypt(securityTables, flowInitiation, securityKey, ref authenticationByte);
                                }
                                else
                                {
                                    result.Add((byte)ENCRYP_TARGETS.ET_SdsAuthentication);
                                }
                                FreeformEncryptionHelper.Encrypt(securityTables, flowInitiation, securityKey, ref source);                                
                                result.AddRange(authenticationByte);
                                result.AddRange(source);
                            }
                            break;
                        default:
                            result = buffer;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public byte[] Decrypt(IFreeformEntity_Msg message, byte[] buffer)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Decrypt"))
            {
                List<byte> result = new List<byte>();

                try
                {
                    if (message == null ||
                        buffer == null ||
                        buffer.Length == 0) return new byte[0];

                    FreeformSecurityTableCollection securityTables = FFMsgHandlerFactory.Current.GetSecurityTables(message.IpAddress);
                    SECURITY_KEY_INDEX securityKey = message.GetSecurityKeyIndex();
                    ENCRYP_TARGETS encryptionType = (ENCRYP_TARGETS)buffer[0];
                    FF_FlowInitiation flowInitiation = message.FlowInitiation;
                    switch (encryptionType)
                    {
                        case ENCRYP_TARGETS.ET_SdsEncryption:
                            {
                                byte[] bufferCopy = buffer.CopyToBuffer(1, buffer.Length - 1);
                                FreeformEncryptionHelper.Decrypt(securityTables, flowInitiation, securityKey, ref bufferCopy);
                                result.AddRange(bufferCopy);
                            }
                            break;

                        case ENCRYP_TARGETS.ET_SdsAuthentication:
                        case ENCRYP_TARGETS.ET_EFTStyleEncryption:
                            {
                                byte authenticationByteReceived = buffer[1];                                 
                                byte[] bufferCopy = buffer.CopyToBuffer(2, buffer.Length - 2);
                                FreeformEncryptionHelper.Decrypt(securityTables, flowInitiation, securityKey, ref bufferCopy);

                                byte authenticationByteGenerated = FreeformEncryptionHelper.MakeAuthenticationByte(bufferCopy);
                                if (encryptionType == ENCRYP_TARGETS.ET_EFTStyleEncryption)
                                {
                                    byte[] temp = new byte[] { authenticationByteGenerated };
                                    FreeformEncryptionHelper.Decrypt(securityTables, flowInitiation, securityKey, ref temp);
                                    authenticationByteGenerated = temp[0];
                                }
                                if (authenticationByteReceived == authenticationByteGenerated)
                                {
                                    result.AddRange(bufferCopy);
                                }
                            }
                            break;

                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result.ToArray();
            }
        }
    }
}
