using System.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace BMC.Common
{
    public class TypeHandler
    {

        #region Constant

        const int IntegerNull = int.MinValue;
        DateTime DateUnassigned = DateTime.Parse("12:00:00 AM");
        DateTime DateNull = DateTime.Parse("1/1/1800");
        DateTime DateMin = DateTime.Parse("1/1/1801");
        DateTime DateMax = DateTime.Parse("12/31/9999");
        DateTime DateTimeBase = DateTime.Parse("1/2/1801");
        const short ShortNull = short.MinValue;
        const decimal DecimalNull = decimal.MinValue;
        const double DoubleNull = double.MinValue;
        const long LongNull = long.MinValue;
        const string StringNull = "";

        #endregion


        public static object GetNull(TypeCode DataTypeCode)
        {
            

            switch (DataTypeCode)
            {
                case TypeCode.Boolean:
                    return 0;
                case TypeCode.DateTime:
                    return DateTime.Parse("1/1/1800");
                case TypeCode.Decimal:
                    return DecimalNull;
                case TypeCode.Double:
                    return DoubleNull;
                case TypeCode.Int32:
                    return IntegerNull;
                case TypeCode.Int64:
                    return IntegerNull;
                case TypeCode.String:
                    return StringNull;
                default:
                    return null;
            }
        }

        public static T GetRowValue<T>(DataRow Row, string ColumnName)
        {
            Type TypeOfT;
            TypeOfT = typeof(T);
            TypeCode tpCode = Type.GetTypeCode(TypeOfT);
            switch (tpCode)
            {

                case TypeCode.DateTime:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return (T)(object)DateTime.Parse("1/1/1800");
                    else
                        return (T)Row[ColumnName];
                case TypeCode.Decimal:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return (T)(object)DecimalNull;
                    else
                        return (T)Row[ColumnName];
                case TypeCode.Double:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return (T)(object)DoubleNull;
                    else
                        return (T)Row[ColumnName];
                case TypeCode.Int32:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return (T)(object)int.Parse("0");
                    else
                        return (T)Row[ColumnName];

                case TypeCode.Int64:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return (T)(object)int.Parse("0");
                    else
                        return (T)Row[ColumnName];

                case TypeCode.String:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return (T)(object)string.Empty;
                    else
                        return (T)Row[ColumnName];

                default:
                    return (T)(object)null;
            }

        }

        public static string GetRowValueToTextBox<T>(DataRow Row, string ColumnName)
        {
            Type TypeOfT;
            TypeOfT = typeof(T);
            TypeCode tpCode = Type.GetTypeCode(TypeOfT);
            switch (tpCode)
            {

                case TypeCode.DateTime:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return DateTime.Parse("1/1/1800").ToString().Trim();
                    else
                        return Row[ColumnName].ToString().Trim();
                case TypeCode.Decimal:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return "0";
                    else
                        return Row[ColumnName].ToString().Trim();
                case TypeCode.Double:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return "0";
                    else
                        return Row[ColumnName].ToString().Trim();

                case TypeCode.Int32:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return "0";
                    else
                        return Row[ColumnName].ToString().Trim();

                case TypeCode.Int64:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return "0";
                    else
                        return Row[ColumnName].ToString().Trim();

                case TypeCode.String:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return StringNull.ToString().Trim();
                    else
                        return Row[ColumnName].ToString().Trim();

                default:
                    return (string)(object)null;
            }

        }




        public static DataTable GetDataTableFromReader(System.Data.SqlClient.SqlDataReader DataReader)
        {
            DataTable newTable = new DataTable();
            DataColumn Col;
            DataRow Row;
            for (int i = 0; i < DataReader.FieldCount - 1; i++)
            {
                Col = new DataColumn();
                Col.ColumnName = DataReader.GetName(i);
                Col.DataType = DataReader.GetFieldType(i);

                newTable.Columns.Add(Col);
            }

            while (DataReader.Read())
            {
                Row = newTable.NewRow();
                for (int i = 0; i < DataReader.FieldCount - 1; i++)
                {
                    Row[i] = DataReader[i];
                }
                newTable.Rows.Add(Row);
            }

            return newTable;
        }

    }

    public class CharKeyHandler
    {
        #region Char Key Handlers
        public static bool IsInteger(char e)
        {
            if (e == (char)46) // This is the character Code for Decimal. 
                return true;
            if (Char.IsNumber(e) || (e == (char)Keys.Back) || e == (char)Keys.Delete || e == (char)Keys.Enter || e == (char)Keys.Tab)
                return false;
            else
                return true;
        }
        public static bool IsAlpha(char e)
        {
            if (Char.IsLetter(e) || e == (char)Keys.Back || e == (char)Keys.Delete || e == (char)Keys.Enter || e == (char)Keys.Tab)
                return false;
            else
                return true;
        }
        public static bool IsAlphaNumeric(char e)
        {
            if (Char.IsLetterOrDigit(e) || e == (char)Keys.Back || e == (char)Keys.Delete || e == (char)Keys.Enter || e == (char)Keys.Tab)
                return false;
            else
                return true;
        }
        public static bool IsInteger(char e, string AllowedCharacters)
        {
            char[] ch = AllowedCharacters.ToCharArray();

            foreach (char C in ch)
            {
                if (e == C)
                {
                    return false;
                }

            }
            if (e == (char)46)
                return true;
            if (Char.IsNumber(e) || (e == (char)Keys.Back) || e == (char)Keys.Delete || e == (char)Keys.Enter || e == (char)Keys.Tab)
                return false;
            else
                return true;
        }
        #endregion

    }
}
