using Eagle.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.TER
{
    public class TerminationParameterViewModel
    {
        public int ID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TerminationID")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int CalTerminationLeave { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsPaidForAnnualLeave")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int IsPaidForAnnualLeave { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsPaidForMandatoryTrainingFee")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int IsPaidForMandatoryTrainingFee { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsManipulatedLeaveDayRemains")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int IsManipulatedLeaveDayRemains { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsManipulatedForPaidLeave")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int IsManipulatedForPaidLeave { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsDeletedAfterTerminationSettlement")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int IsDeletedAfterTerminationSettlement { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsAutomatedDecisionNo")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int IsAutomatedDecisionNo { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "AutoNumberLenght")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int AutoNumberLenght { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Prefix")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string Prefix { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Suffix")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string Suffix { get; set; }
    }
}
