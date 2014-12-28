using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Eagle.Model
{
    public class DateTimeFormatAttribute : ValidationAttribute
    {
        public string Format { get; set; }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            DateTime val;
            try
            {
                val = DateTime.ParseExact(value.ToString(), Format, null);
            }
            catch (FormatException)
            {
                return false;
            }

            //Not entirely sure it'd ever reach this, but I need a return statement in all codepaths
            return val != DateTime.MinValue;
        }
    }


    public class AutoCompleteViewModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string text { get; set; }
    }

    public class AutoCompleteModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string text { get; set; }
    }
}
