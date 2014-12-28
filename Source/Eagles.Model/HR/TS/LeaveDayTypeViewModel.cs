using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace Eagle.Model
{
    public class LeaveDayTypeViewModel
    {
        public int LSLeaveDayTypeID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string LSLeaveDayTypeCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LeaveTypeName")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int LSLeaveTypeID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LeaveName")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DaysPerYear")]
        
        [Range(0, 100, ErrorMessage = "Number must be a positive number {0} from {1} to {2}")]
        //[RegularExpression("([0-9]+)", ErrorMessage = "Square Feet must be a positive number")]
        public Nullable<int> DaysPerYear { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DaysPerPeriod")]
        public Nullable<int> DaysPerPeriod { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ConvalescenceLeave")]
        public Nullable<decimal> ConvalescenceLeave { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PercentSI")]
        [DisplayFormat(DataFormatString = "{0:#,##0.0}")]
        public Nullable<decimal> PercentSI { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ExceptedHolidays")]
        public Nullable<bool> ExceptedHolidays { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
        public string Note { get; set; }

        public string strExceptedHolidays { get; set;}
        public string strTypeName { get; set;}
    }
}
