using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model
{
    public class QuotaRecruitmentCreateViewModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Year")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int? YearSearch { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastCompanyDefine")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int? LSCompanyIDSearch { get; set; }
        public string LSCompanyNameSearch { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Position")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int? LSPositionIDSearch { get; set; }
        public string LSPositionNameSearch { get; set; }
    }
}
