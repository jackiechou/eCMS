using Eagle.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model
{
    public class InterviewCalendarViewModel : REC_tblInterviewCalendar
    {

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CandidateName")]
        public string CandidateName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DOB")]
        public DateTime? DOB { get; set; }

        //[Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "InterviewTime")]

        public bool? Result { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Result")]
        public string ResultName
        {
            get
            {
                //Nếu result = null => chưa phỏng vấn
                //Nếu result != null => đã phỏng vấn
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
        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Interviewer")]
        public string Interviewer { get; set; }

        public string Date { get; set; }
        public string Time { get; set; }
    }
}
