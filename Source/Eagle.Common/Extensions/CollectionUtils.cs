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
    public class CollectionUtils
    {
        public static List<int> ConvertStringToListInt(string strInput, char strSplitCondition)
        {
            List<int> ret = new List<int>();
            if (strInput != null)
            {
                string[] strTmp = strInput.Split(strSplitCondition);
                int tmp;
                foreach (var item in strTmp)
                {
                    try
                    {
                        tmp = Convert.ToInt32(item);
                        ret.Add(tmp);
                    }
                    catch (Exception ex) { ex.ToString(); }
                }
            }

            return ret;
        }

        public static List<T> ConvertDataTableToList<T>(DataTable dt)
        {
            List<T> lst = new System.Collections.Generic.List<T>();
            Type tClass = typeof(T);
            PropertyInfo[] pClass = tClass.GetProperties();
            List<DataColumn> dc = new List<DataColumn>();
            dc = dt.Columns.Cast<DataColumn>().ToList();
            T cn;
            foreach (DataRow item in dt.Rows)
            {
                cn = (T)Activator.CreateInstance(tClass);
                foreach (PropertyInfo pc in pClass)
                {
                    // Can comment try catch block.
                    try
                    {
                        DataColumn d = dc.Find(c => c.ColumnName == pc.Name);
                        if (d != null)
                            pc.SetValue(cn, item[pc.Name], null);
                    }
                    catch
                    {
                    }
                }
                lst.Add(cn);
            }
            return lst;
        }

        public DataTable ConvertIEnumerableToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();
            //Column Names
            PropertyInfo[] oProps = null;
            if (varlist == null) return dtReturn;
            foreach (T rec in varlist)
            {
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }
                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }
                DataRow dr = dtReturn.NewRow();
                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }
                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
        //public static DataTable ConvertToDataTable(IList list)
        //{
        //    try
        //    {
        //        DataTable table = CreateTable((Object[])list[0], list);

        //        foreach (Object[] objs in list)
        //        {
        //            int i = 0;
        //            DataRow row = table.NewRow();

        //            foreach (Object obj in objs)
        //            {
        //                row[i++] = obj;
        //            }

        //            table.Rows.Add(row);
        //        }

        //        return table;
        //    }
        //    catch { }
        //    return null;
        //}

        public static DataTable ConvertTo<T>(IList<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);


            foreach (T item in list)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }
            return table;
        }

        public static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);


            foreach (PropertyDescriptor prop in properties)
            {
                // HERE IS WHERE THE ERROR IS THROWN FOR NULLABLE TYPES
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            return table;
        }

        public static DataTable CreateTable(Object[] objs, IList list)
        {
            DataTable table = new DataTable();
            int i = 0;

            foreach (Object obj in objs)
            {
                Object[] objTemplate = ((Object[])list.Cast<Object>().FirstOrDefault(x => ((Object[])x)[i] != null));
                table.Columns.Add(null, obj == null ?
                    objTemplate == null || objTemplate[i] == null ? typeof(string)
                    : objTemplate.GetType() : obj.GetType());
            }

            return table;
        }

    }
}
