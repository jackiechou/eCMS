using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model
{
    public class ResultsRecordSearchViewModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "RecruitmentProjectCode")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int? ProjectID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Tournament")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int? ProjectTournamentID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CandidateName")]
        public string FullName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Result")]
        public int? Result { get; set; }
    }
}
