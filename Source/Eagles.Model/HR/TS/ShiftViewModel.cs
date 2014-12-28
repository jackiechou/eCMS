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
    public class ShiftViewModel
    {
        public int LSShiftID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSShiftCode")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string LSShiftCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ShiftName")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string ShiftName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "WorkTimeBegin")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public System.DateTime WorkTimeBegin { get; set; }
        public string strWorkTimeBegin { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "WorkTimeEnd")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public System.DateTime WorkTimeEnd { get; set; }
        public string strWorkTimeEnd { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BreakTimeBegin")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public System.DateTime BreakTimeBegin { get; set; }
        public string strBreakTimeBegin { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BreakTimeEnd")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public System.DateTime BreakTimeEnd { get; set; }
        public string strBreakTimeEnd { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "isNextDate_OTTimeEnd")]
        public Nullable<bool> isNextDate_OTTimeEnd { get; set; }
        public string strisNextDate_OTTimeEnd { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TimeLate")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public System.DateTime TimeLate { get; set; }
        public string strTimeLate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TimeEarly")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public System.DateTime TimeEarly { get; set; }
        public string strTimeEarly { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "OTNightBegin")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public Nullable<System.DateTime> OTNightBegin { get; set; }
        public string strOTNightBegin { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "OTNightEnd")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public System.DateTime OTNightEnd { get; set; }

        public string strOTNightEnd { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsOTNightEnd")]
        public bool IsOTNightEnd { get; set; }

        public string strIsOTNightEnd { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "WorkHour")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public decimal WorkHour { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
        public string Note { get; set; }
    }
}
