using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model
{
    public class CandidateResultViewModel
    {
        public int CandidateID { get; set; }
        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CandidateCode")]
        public string CandidateCode { get; set; }
        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FirstName")]
        public string FirstName { get; set; }
        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastName")]
        public string LastName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FullName")]
        public string FullName { get { return LastName + " " + FirstName; } }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Gender")]
        public int? Gender { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Gender")]
        public string GenderName
        {
            get
            {
                switch (Gender)
                {
                    case 0 :
                        return Eagle.Resource.LanguageResource.Male;
                    case 1 :
                        return Eagle.Resource.LanguageResource.Female;
                    case -1 :
                        return Eagle.Resource.LanguageResource.NonSpecified;
                    default:
                        return "";
                }
            
            }
        }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IDNo")]
        public string IDNo { get; set; }
        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DOB")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DOB { get; set; }
        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ApplyDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ApplyDate { get; set; }
        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Result")]
        public int? Result { get; set; }
        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Result")]
        public string ResultName { get {
            string ret = "";
            switch (Result)
            {
                case 0:
                    ret = Eagle.Resource.LanguageResource.Result_0;
                    break;
                case 1:
                    ret = Eagle.Resource.LanguageResource.Result_1;
                    break;
                case 2:
                    ret = Eagle.Resource.LanguageResource.Result_2;
                    break;
                case 3:
                    ret = Eagle.Resource.LanguageResource.Result_3;
                    break;
                case 4:
                    ret = Eagle.Resource.LanguageResource.Result_4;
                    break;
                case 5:
                    ret = Eagle.Resource.LanguageResource.Result_5;
                    break;
                case 6:
                    ret = Eagle.Resource.LanguageResource.Result_6;
                    break;
                default:
                    ret = "";
                    break;
            }
            return ret;
        } }
        
    }
}
