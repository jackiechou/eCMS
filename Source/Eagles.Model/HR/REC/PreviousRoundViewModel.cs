using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model
{
    public class PreviousRoundViewModel
    {
        public int CandidateTournamentID { get; set; }
        public string Interviewer { get; set; }
        public int SEQ { get; set; }
        public bool? Result { get; set; }
        public string ResultName { get{
            switch (Result)
            {
                case true:
                    return Eagle.Resource.LanguageResource.Pass;
                case false:
                    return Eagle.Resource.LanguageResource.NotPass;
                default:
                    return "";
                    
            }
        }}

        public List<InterviewCriteriaViewModel> InterviewCriter { get; set; }

    }
}
