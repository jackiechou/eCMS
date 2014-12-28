using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eagle.Model
{
    public class MinDateAttribute : ValidationAttribute
    {
        public DateTime MinDate { get; set; }
        public MinDateAttribute(int year, int month, int day)
        {
            this.MinDate = new DateTime((year >= 1900) ? year : 1900, (month > 0 &&  month <=12) ? month : 1, (day > 0 && day <=31) ? day : 1);
        }
   
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            DateTime val;
            try
            {
                val = Convert.ToDateTime(value);

                if (val.CompareTo(MinDate) < 0)
                {
                    return false;
                }else
                {
                    return true;
                }

            }
            catch (FormatException)
            {
                return false;
            }
        }
    }

    public class AllowedYearAttribute : ValidationAttribute
    {
        public DateTime MinDateTime { get; set; }
        public AllowedYearAttribute(int AllowedYear)
        {
            int _AllowedYear = (AllowedYear > 0) ? AllowedYear : 18;
            this.MinDateTime = DateTime.Now.AddYears(-System.Math.Abs(_AllowedYear));

        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            DateTime val;
            try
            {
                val = Convert.ToDateTime(value);

                if (val.CompareTo(MinDateTime) > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
