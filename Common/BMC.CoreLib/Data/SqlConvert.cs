/* ================================================================================= 
 * Purpose		:	Sql Server Db Type Conversion Class
 * File Name	:   SqlConvert.cs
 * Author		:	A.Vinod Kumar
 * Created  	:	21/12/2010
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 21/12/2010		A.Vinod Kumar    Initial Version
 * ===============================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BMC.CoreLib.Data {
    /// <summary>
    /// Sql Server Db Type Conversion Class
    /// </summary>
    public static class SqlConvert {
        #region Methods

        /// <summary>
        /// Dbs the type of the type to SQL db.
        /// </summary>
        /// <param name="dbType">Type of the db.</param>
        /// <returns>Sql Db Type.</returns>
        public static SqlDbType DbTypeToSqlDbType(DbType dbType) {
            switch (dbType) {
                case DbType.AnsiString: return SqlDbType.VarChar;
                case DbType.Binary: return SqlDbType.VarBinary;
                case DbType.Byte: return SqlDbType.TinyInt;
                case DbType.Boolean: return SqlDbType.Bit;
                case DbType.Currency: return SqlDbType.Money;
                case DbType.Date: return SqlDbType.DateTime;
                case DbType.DateTime: return SqlDbType.DateTime;
                case DbType.Decimal: return SqlDbType.Decimal;
                case DbType.Double: return SqlDbType.Float;
                case DbType.Guid: return SqlDbType.UniqueIdentifier;
                case DbType.Int16: return SqlDbType.SmallInt;
                case DbType.Int32: return SqlDbType.Int;
                case DbType.Int64: return SqlDbType.BigInt;
                case DbType.Object: return SqlDbType.Variant;
                case DbType.SByte: throw new NotSupportedException(dbType.ToString());
                case DbType.Single: return SqlDbType.Real;
                case DbType.String: return SqlDbType.NVarChar;
                case DbType.UInt16: throw new NotSupportedException(dbType.ToString());
                case DbType.UInt32: throw new NotSupportedException(dbType.ToString());
                case DbType.UInt64: throw new NotSupportedException(dbType.ToString());
                case DbType.VarNumeric: throw new NotSupportedException(dbType.ToString());
                case DbType.AnsiStringFixedLength: return SqlDbType.Char;
                case DbType.StringFixedLength: return SqlDbType.NChar;
                default: throw new NotSupportedException(dbType.ToString());
            }
        }

        /// <summary>
        /// SQLs the type of the db type to db.
        /// </summary>
        /// <param name="sqlDbType">Type of the SQL db.</param>
        /// <returns>Db Type.</returns>
        public static DbType SqlDbTypeToDbType(SqlDbType sqlDbType) {
            switch (sqlDbType) {
                case SqlDbType.BigInt: return DbType.Int64;
                case SqlDbType.Binary: return DbType.Binary;
                case SqlDbType.Bit: return DbType.Boolean;
                case SqlDbType.Char: return DbType.AnsiStringFixedLength;
                case SqlDbType.DateTime: return DbType.DateTime;
                case SqlDbType.Decimal: return DbType.Decimal;
                case SqlDbType.Float: return DbType.Double;
                case SqlDbType.Image: return DbType.Binary;
                case SqlDbType.Int: return DbType.Int32;
                case SqlDbType.Money: return DbType.Currency;
                case SqlDbType.NChar: return DbType.StringFixedLength;
                case SqlDbType.NText: return DbType.String;
                case SqlDbType.NVarChar: return DbType.String;
                case SqlDbType.Real: return DbType.Single;
                case SqlDbType.UniqueIdentifier: return DbType.Guid;
                case SqlDbType.SmallDateTime: return DbType.DateTime;
                case SqlDbType.SmallInt: return DbType.Int16;
                case SqlDbType.SmallMoney: return DbType.Currency;
                case SqlDbType.Text: return DbType.AnsiString;
                case SqlDbType.Timestamp: return DbType.Binary;
                case SqlDbType.TinyInt: return DbType.Byte;
                case SqlDbType.VarBinary: return DbType.Binary;
                case SqlDbType.VarChar: return DbType.AnsiString;
                case SqlDbType.Variant: return DbType.Object;
                default: throw new NotSupportedException(sqlDbType.ToString());
            }
        }

        #endregion
    }
}
