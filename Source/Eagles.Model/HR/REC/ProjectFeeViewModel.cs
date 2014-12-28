using Eagle.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model
{
    public class ProjectFeeViewModel : REC_tblProjectFee
    {

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "RecruitmentFee")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int? LSRecruitmentFeeIDAlowNull { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "RecruitmentFee")]
        public string LSRecruitmentFeeName { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Currency")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int? CurrencyIDAlowNull { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Currency")]
        public string CurrencyName { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Amount")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [DisplayFormat(DataFormatString = "{0:N}")]
        public decimal? AmountAlowNull { get; set; }
    }
}
