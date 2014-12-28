using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Core;
using System.ComponentModel.DataAnnotations;

namespace Eagle.Model
{
    public class InsuranceInformationViewModel : SI_tblInsuranceInformation
    {
        public string LSProvinceIDIssuedSIName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "InsuranceYearsAgo")]
        public string InsuranceYearsAgo { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "InsuranceYears")]
        public string InsuranceYears { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "JoinDateAtCompany")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public DateTime? JoinDateAtCompanyAlowNull { get; set; }

        public string LSHospitalIDOriginalName { get; set; }
        public string LSProvinceIDIssuedHIName { get; set; }
        public string LSHospitalIDNewName { get; set; }

    }
}
