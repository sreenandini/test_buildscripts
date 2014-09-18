using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using BMC.CoreLib;
using BMC.CoreLib.Collections;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_EnumCollection_Generic_B2B : FFEnumParser
    {
        private static CultureInfo CULTURE = Thread.CurrentThread.CurrentCulture;
        private IDoubleKeyDictionary<string, IFFEnumParser> _parsers = null;

        internal FFParser_EnumCollection_Generic_B2B()
        {
            this.Initialize();
        }

        protected virtual void Initialize()
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "FFParser_EnumCollection_Generic_B2B"))
            {
                try
                {
                    _parsers = new DoubleKeyDictionary<string, IFFEnumParser>(StringComparer.InvariantCultureIgnoreCase);
                    var enumItems = (from i in typeof(FFEnumParserFactory).Assembly.GetTypes()
                                     where i.IsEnum
                                     from c in i.GetCustomAttributes(typeof(FFGmuIdAppIdMappingAttribute), true).OfType<FFGmuIdAppIdMappingAttribute>()
                                     select new
                                     {
                                         Attr = c,
                                         EnumType = i
                                     }).ToArray();
                    foreach (var enumItem in enumItems)
                    {
                        FFGmuIdAppIdMappingAttribute mapAttr = enumItem.Attr;
                        mapAttr.GmuIdType = enumItem.EnumType;

                        IFFEnumParser parser = new FFEnumParser();
                        parser.AddBufferEntityParser(mapAttr.GmuIdType, mapAttr.AppIdType, parser);
                        _parsers.Add(mapAttr.GmuIdType.FullName, mapAttr.AppIdType.FullName, parser);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        public S GetAppId<T, S>(T value)
            where T : IConvertible
            where S : IConvertible
        {
            return this.GetAppId<T, S>(value.ToInt32(CULTURE));
        }

        public S GetAppId<T, S>(int value)
            where T : IConvertible
            where S : IConvertible
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "GetAppId"))
            {
                S result = default(S);

                try
                {
                    IFFEnumParser parser = _parsers.GetValueFromKey1(typeof(T).FullName);
                    if (parser != null)
                    {
                        result = TypeSystem.GetValueEnumGeneric2<S>(parser.GetAppIdFromGmuId(value), default(S));
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public S GetGmuId<T, S>(T value)
            where T : IConvertible
            where S : IConvertible
        {
            return this.GetGmuId<T, S>(value.ToInt32(CULTURE));
        }

        public S GetGmuId<T, S>(int value)
            where T : IConvertible
            where S : IConvertible
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "GetGmuId"))
            {
                S result = default(S);

                try
                {
                    IFFEnumParser parser = _parsers.GetValueFromKey2(typeof(T).FullName);
                    if (parser != null)
                    {
                        result = TypeSystem.GetValueEnumGeneric2<S>(parser.GetGmuIdFromAppId(value), default(S));
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public int GetAppIdInt32<T>(T value)
            where T : IConvertible
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "GetGmuId"))
            {
                int  result = default(int);

                try
                {
                    IFFEnumParser parser = _parsers.GetValueFromKey1(typeof(T).FullName);
                    if (parser != null)
                    {
                        result = parser.GetAppIdFromGmuId(value.ToInt32(CULTURE));
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public int GetGmuIdInt32<T>(T value)
            where T : IConvertible
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "GetGmuId"))
            {
                int result = default(int);

                try
                {
                    IFFEnumParser parser = _parsers.GetValueFromKey2(typeof(T).FullName);
                    if (parser != null)
                    {
                        result = parser.GetGmuIdFromAppId(value.ToInt32(CULTURE));
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }
    }

    public static class FFEnumParserFactory
    {
        private const string DYN_MODULE_NAME = "FFEnumParserFactory";
        private static object _instanceLock = new object();
        [ThreadStatic]
        private static FFParser_EnumCollection_Generic_B2B _instance = null;

        #region Single Thread Helper (Instance)

        private static SingletonHelperBase<FFParser_EnumCollection_Generic_B2B> _instanceHelper = new SingletonHelper<FFParser_EnumCollection_Generic_B2B>(
                                    new Lazy<FFParser_EnumCollection_Generic_B2B>(() => new FFParser_EnumCollection_Generic_B2B()));

        #endregion

        private static FFParser_EnumCollection_Generic_B2B GetParser()
        {
            return _instanceHelper.Current;
        }

        public static S GetAppId<T, S>(T value)
            where T : IConvertible
            where S : IConvertible
        {
            return GetParser().GetAppId<T, S>(value);
        }

        public static S GetAppId<T, S>(this int value)
            where T : IConvertible
            where S : IConvertible
        {
            return GetParser().GetAppId<T, S>(value);
        }

        public static S GetAppId<T, S>(this byte value)
            where T : IConvertible
            where S : IConvertible
        {
            return GetParser().GetAppId<T, S>((int)value);
        }

        public static int GetAppIdInt32<T>(this T value)
            where T : IConvertible
        {
            return GetParser().GetAppIdInt32<T>(value);
        }

        public static byte GetAppIdInt8<T>(this T value)
            where T : IConvertible
        {
            return (byte)GetParser().GetAppIdInt32<T>(value);
        }

        public static S GetGmuId<T, S>(T value)
            where T : IConvertible
            where S : IConvertible
        {
            return GetParser().GetGmuId<T, S>(value);
        }

        public static S GetGmuId<T, S>(this int value)
            where T : IConvertible
            where S : IConvertible
        {
            return GetParser().GetGmuId<T, S>(value);
        }

        public static S GetGmuId<T, S>(this byte value)
            where T : IConvertible
            where S : IConvertible
        {
            return GetParser().GetGmuId<T, S>((int)value);
        }

        public static int GetGmuIdInt32<T>(this T value)
            where T : IConvertible
        {
            return GetParser().GetGmuIdInt32<T>(value);
        }

        public static byte GetGmuIdInt8<T>(this T value)
            where T : IConvertible
        {
            return (byte)GetParser().GetGmuIdInt32<T>(value);
        }
    }
}
