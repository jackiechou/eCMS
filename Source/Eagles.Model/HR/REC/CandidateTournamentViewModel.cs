using Eagle.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model
{
    public class CandidateTournamentViewModel : REC_tblCandidateTournament
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CandidateName")]
        public string CandidateName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DOB")]
        public DateTime? DOB { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Result")]
        public string ResultName { 
            get {
                    string ret = "";
                    switch (Result)
                    {
                        case null:
                            ret = Eagle.Resource.LanguageResource.NotReached;
                            break;
                        case true:
                            ret = Eagle.Resource.LanguageResource.Pass;
                            break;
                        case false:
                            ret = Eagle.Resource.LanguageResource.NotPass;
                            break;
                    }
                    return ret;
                } 
        }
    }
}
