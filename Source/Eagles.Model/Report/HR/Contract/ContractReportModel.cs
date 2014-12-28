using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.Report.HR.Contract
{
    public class ContractReportModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSContractTypeID")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int LSContractTypeID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSContractTypeName")]
        public string ContractTypeName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TotalMaleContractsByLSContractTypeIDAndMaleGender")]
        public int? TotalMaleContractsByLSContractTypeIDAndMaleGender { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TotalFeMaleContractsByLSContractTypeIDAndMaleGender")]
        public int? TotalFeMaleContractsByLSContractTypeIDAndMaleGender { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TotalContractsByLSContractTypeID")]
        public int? TotalContractsByLSContractTypeID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TotalPercentMaleContractsByLSContractTypeIDAndMaleGender")]
        [DisplayFormat(DataFormatString = "{0:0.0}", ApplyFormatInEditMode = true)]
        public int? TotalPercentMaleContractsByLSContractTypeIDAndMaleGender { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender")]
        [DisplayFormat(DataFormatString = "{0:0.0}", ApplyFormatInEditMode = true)]
        public int? TotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TotalPercentByLSContractTypeID")]
        [DisplayFormat(DataFormatString = "{0:0.0}", ApplyFormatInEditMode = true)]
        public int? TotalPercentByLSContractTypeID { get; set; }
    }
}
