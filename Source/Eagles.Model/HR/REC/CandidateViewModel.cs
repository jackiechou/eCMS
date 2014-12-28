using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Core;
using System.ComponentModel.DataAnnotations;
namespace Eagle.Model
{
    public class CandidateViewModel : REC_tblCandidate
    {
        public string BornLSCountryName { get; set; }
        public string BornLSProvinceName { get; set; }
        public string BornLSDistrictName { get; set; }
        public string NativeCountryName { get; set; }
        public string NativeProvinceName { get; set; }
        public string NativeDistrictName { get; set; }
        public string PLSCountryName { get; set; }
        public string PLSProvinceName { get; set; }
        public string PLSDistrictName { get; set; }
        public string TLSCountryName { get; set; }
        public string TLSProvinceName { get; set; }
        public string TLSDistrictName { get; set; }
        public string LSCompanyName { get; set; }
        public string IDIssuedPlaceName { get; set; }
        public string LSNationalityName { get; set; }
        public string LSReligionName { get; set; }
        public string LSEthnicName { get; set; }
        public string LSEducationName { get; set; }
        public string LSQualificationName { get; set; }
        public string LSMajorName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IssuedDate")]
        public string IDIssuedDatetmp { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ApplyDate")]
        public string ApplyDatetmp { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DOB")]
        public string DOBtmp { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FullName")]
        public string FullName
        {
            get
            {
                return LastName + " " + FirstName;
            }
        }
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
                    default:
                        return Eagle.Resource.LanguageResource.NoneSpecified;
                }
            }
        }
        


        public string InitialCandidateCode { get { return CandidateCode; } }
        public string InitialIDNo { get { return IDNo; } }

    }
}
