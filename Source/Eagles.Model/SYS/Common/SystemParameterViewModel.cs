using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eagle.Model
{
    public class SystemParameterViewModel
    {

        [Key]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ParameterID")]
        public int ParameterID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CutOffSI")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int CutOffSI { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TypeOfSalary")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int TypeOfSalary { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "WorkingCalendar")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int LeaveType { get; set; }
        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "OTLimit")]
        [Remote("ValidationOTLimitIsGreaterThan0", "Validation")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int OTLimit { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "StandardWorking")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int StandardWorking { get; set; }

        public int Day { get; set; }
        public int Month { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "StandardAnnualLeave")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Remote("ValidationStandardAnnualLeaveIsGreaterThan0", "Validation")]
        public int StandardAnnualLeave { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "NightOTFrom")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public Nullable<System.DateTime> NightOTFrom { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "NightOTTo")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public Nullable<System.DateTime> NightOTTo { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LongTermDays")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int DayOfLongTerm { get; set; }
    }
}
