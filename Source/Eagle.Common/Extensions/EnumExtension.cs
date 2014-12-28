using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Eagle.Common.Extensions
{
    public static class EnumExtension
    {       
        public static SelectList ToSelectList<TEnum>(this TEnum enumObj)

            where TEnum : struct, IComparable, IFormattable, IConvertible
        {

            var values = from TEnum e in Enum.GetValues(typeof(TEnum))

                            select new { Id = e, Name = e.ToString() };

            return new SelectList(values, "Id", "Name", enumObj);
            //ViewData["taskStatus"] = task.Status.ToSelectList();
        }        

        public static Dictionary<int, string> BindEnum(Type enumType)
        {
            // get the names from the enumeration
            string[] keys = Enum.GetNames(enumType);
            // get the values from the enumeration
            Array values = Enum.GetValues(enumType);

            ////var sortedPairs = values.Select((x, i) => new { Value = x, Key = enumNames[i] })
            ////            .OrderBy(x => x.Value)
            ////            .ThenBy(x => x.Key)
            ////            .ToArray();

            ////string[] sortedEnumKeys = sortedPairs.Select(x => x.Key).ToArray();
            ////int[] sortedEnumValues = sortedPairs.Select(x => x.Value).ToArray();  

            Dictionary<int, string> dict = new Dictionary<int, string>();
            for (int i = 0; i < keys.Length; i++)
            {
                dict.Add((int)values.GetValue(i), keys[i]);
            }
            return dict;
        }

        public static Dictionary<int, string> BindEnumOrderByName(Type enumType)
        {
            string[] keys = Enum.GetNames(enumType);
            Array values = Enum.GetValues(enumType);
            Array.Sort(keys, values);           
            Dictionary<int, string> dict = new Dictionary<int, string>();
            //string key = string.Empty;
            //int val = 0;
            //Hashtable hashtable = new Hashtable();          
            for (int i = 0; i < values.Length; i++)
            {
                //key = keys[i];
                //val = Convert.ToInt32(keys.Where(e => e.StartsWith(key)).Select(e => (int)Enum.Parse(enumType, e)).FirstOrDefault());
                //hashtable[val] = key;
                //hashtable.Add((int)values.GetValue(i), keys[i]);
                dict.Add((int)values.GetValue(i), keys[i]);
            }
            return dict;
        }

        public static Dictionary<int, string> BindEnumOrderByKey(Type enumType)
        {
            // get the names from the enumeration
            string[] keys = Enum.GetNames(enumType);
            // get the values from the enumeration
            Array values = Enum.GetValues(enumType);
            Array.Sort(values, keys);
            //Array.Reverse(names, 0, names.Length);
            //Array.Reverse(values, 0, values.Length);

            Dictionary<int, string> dict = new Dictionary<int, string>();
            for (int i = 0; i < keys.Length; i++)
            {
                dict.Add((int)values.GetValue(i), keys[i]);
            }
            return dict;
        }      
    }
}
