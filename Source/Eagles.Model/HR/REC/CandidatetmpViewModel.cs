using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model
{
    //class dùng cho popup search
    public class CandidatetmpViewModel
    {
        public int CandidatetmpID { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FirstName")]
        public string FirstNametmp { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastName")]
        public string LastNametmp { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CandidateCode")]
        public string CandidatetmpCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FullName")]
        public string FullNametmp { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Gender")]
        public int? Gender { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Gender")]
        public string GenderName
        {
            get
            {
                switch (Gender)
                {
                    case 0:
                        return Eagle.Resource.LanguageResource.Male;
                    case 1:
                        return Eagle.Resource.LanguageResource.Female;
                    case -1:
                        return Eagle.Resource.LanguageResource.NonSpecified;
                    default:
                        return "";
                }

            }
        }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IDNo")]
        public string IDNotmp { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DOB")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DOBtmp { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ApplyDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ApplyDatetmp { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Result")]
        public int? Result { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Result")]
        public string ResultName
        {
            get
            {
                switch (Result)
                {
                    case 0:
                        return Eagle.Resource.LanguageResource.Result_0;
                    case 1:
                        return Eagle.Resource.LanguageResource.Result_1;
                    case 2:
                        return Eagle.Resource.LanguageResource.Result_2;
                    case 3:
                        return Eagle.Resource.LanguageResource.Result_3;
                    case 4:
                        return Eagle.Resource.LanguageResource.Result_4;
                    case 5:
                        return Eagle.Resource.LanguageResource.Result_5;
                    case 6:
                        return Eagle.Resource.LanguageResource.Result_6;
                    default:
                        return "";
                }
            }
        }

    }
}
