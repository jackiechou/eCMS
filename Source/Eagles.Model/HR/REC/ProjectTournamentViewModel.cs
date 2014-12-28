using Eagle.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model
{
    public class ProjectTournamentViewModel : REC_tblProjectTournament
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromDate")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public DateTime? FromDateAlowNull { get; set; }
        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToDate")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public DateTime? ToDateAlowNull { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "RecruitmentTournament")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int? LSRecruitmentTournamentIDAlowNull { get; set; }
        public string LSRecruitmentTournamentName { get; set; }

        //[Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Type")]
        //[Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        //public int? TypeAlowNull { get; set; }
        //public string TypeName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "InterviewingStaff")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string strEmpName { get; set; }

        public string InterviewCiteriaSelected { get; set; }
    }
}
