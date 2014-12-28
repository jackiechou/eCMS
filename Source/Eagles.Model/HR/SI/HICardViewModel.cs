using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Core;
using System.ComponentModel.DataAnnotations;

namespace Eagle.Model
{
    public class HICardViewModel : SI_tblHICard
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Hospital")]
        public string LSHospitalName { get; set; }

        public string oldHospitalName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromDate")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public DateTime? FromDateAllowNull { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToDate")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public DateTime? ToDateAllowNull { get; set; }
    }
}
