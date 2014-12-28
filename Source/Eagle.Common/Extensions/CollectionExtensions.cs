using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Collections;
using System.Reflection;

namespace Eagle.Common.Extensions
{
    public class CollectionUitls
    {
        public static DataTable FillDataTableFromIEnumerable(IEnumerable enumerable)
        {
            DataTable dataTable = new DataTable();
            DataRow row;
            PropertyInfo[] propertyInfos;

            foreach (object obj in enumerable)
            {
                propertyInfos = obj.GetType().GetProperties();

                if (dataTable.Columns.Count == 0)
                    foreach (PropertyInfo pi in propertyInfos)
                        dataTable.Columns.Add(pi.Name, pi.PropertyType);

                row = dataTable.NewRow();

                foreach (PropertyInfo pi in propertyInfos)
                {
                    object value = pi.GetValue(obj, null);
                    row[pi.Name] = value;
                }

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        /// <summary>
        /// Convert a List{T} to a DataTable.
        /// </summary>
        public static DataTable FillDataTableFromList<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        /// <summary>
        /// Determine of specified type is nullable
        /// </summary>
        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// Return underlying type if type is Nullable otherwise return the type
        /// </summary>
        public static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }

        public static DataSet ToDataSet<T>(IEnumerable<T> collection, string dataTableName)
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
