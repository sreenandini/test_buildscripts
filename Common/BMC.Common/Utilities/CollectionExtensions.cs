using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data;
using System.Reflection;

namespace BMC.Common.Utilities
{
    public static class CollectionExtensions
    {
        public static DataSet ToDataSet<T>(this IEnumerable<T> collection, string dataTableName)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            if (string.IsNullOrEmpty(dataTableName))
            {
                throw new ArgumentNullException("dataTableName");
            }

            DataSet data = new DataSet("NewDataSet");
            data.Tables.Add(FillDataTable(dataTableName, collection));
            return data;
        }

        private static DataTable FillDataTable<T>(string tableName, IEnumerable<T> collection)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            DataTable dt = CreateDataTable<T>(tableName,
            collection, properties);

            IEnumerator<T> enumerator = collection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                dt.Rows.Add(FillDataRow<T>(dt.NewRow(),
               enumerator.Current, properties));
            }

            return dt;
        }

        private static DataRow FillDataRow<T>(DataRow dataRow, T item, PropertyInfo[] properties)
        {
            foreach (PropertyInfo property in properties)
            {
                dataRow[property.Name.ToString()] = property.GetValue(item, null);
            }

            return dataRow;
        }

        private static DataTable CreateDataTable<T>(string tableName, IEnumerable<T> collection, PropertyInfo[] properties)
        {
            DataTable dt = new DataTable(tableName);

            foreach (PropertyInfo property in properties)
            {
                dt.Columns.Add(property.Name.ToString());
            }

            return dt;
        }
    }
}
