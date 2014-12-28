using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Core;
using System.ComponentModel.DataAnnotations;


namespace Eagle.Model
{
    public class RecruitmentProjectViewModel : REC_tblProject
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ApplyDateFrom")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public System.DateTime? ApplyDateFromAlowNull { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ApplyDateTo")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]          
        public System.DateTime? ApplyDateToAlowNull { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "RecruitFrom")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]  
        public System.DateTime? RecruitFromAlowNull { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "RecruitTo")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]  
        public System.DateTime? RecruitToAlowNull { get; set; }

        public bool chkRecruitmentTournamentList { get; set; }
        public bool chkCandidateList { get; set; }
        public bool chkRecruitmentRequirementsRelatedList { get; set; }
        public bool chkRecruitmentCosts { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSCurrencyID")]
        public string LSCurrencyName { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Venue")]
        public string LSLocationName { get; set; }

        public string StatusName {
            get {
                switch (Status)
                {
                    case 1:
                        return Eagle.Resource.LanguageResource.Open;
                    default:
                        return Eagle.Resource.LanguageResource.Closed;
                }
            }
        }
        
    }
}
