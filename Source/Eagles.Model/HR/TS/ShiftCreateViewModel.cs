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
    public class ShiftCreateViewModel
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
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan WorkTimeBegin { get; set; }

        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public TimeSpan WorkTimeBeginTimeSpan { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "WorkTimeEnd")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [DisplayFormat(DataFormatString = "{0:HH\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan WorkTimeEnd { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BreakTimeBegin")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan BreakTimeBegin { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BreakTimeEnd")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan BreakTimeEnd { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "isNextDate_OTTimeEnd")]
        public bool isNextDate_OTTimeEnd { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TimeLate")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan TimeLate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TimeEarly")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan TimeEarly { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "OTNightBegin")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan OTNightBegin { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "OTNightEnd")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan OTNightEnd { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsOTNightEnd")]
        public bool IsOTNightEnd { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "WorkHour")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Range(1, 24, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "RequiredWorkHour")]
        public decimal WorkHour { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
        public string Note { get; set; }
    }
}
