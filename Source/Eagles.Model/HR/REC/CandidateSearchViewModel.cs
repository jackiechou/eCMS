using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model
{
    public class CandidateSearchViewModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CandidateCode")]
        public string CandidateCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FirstName")]
        public string FirstName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastName")]
        public string LastName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Result")]
        public Nullable<int> Result { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Gender")]
        public Nullable<int> Gender { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ExpectationPosition1")]
        public Nullable<int> LSPositionID { get; set; }
        public string LSPositionName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ExpectationPosition2")]
        public Nullable<int> LSPositionID_Probation1 { get; set; }
        public string LSPositionID_Probation1Name { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ExpectationPosition3")]
        public Nullable<int> LSPositionID_Probation2 { get; set; }
        public string LSPositionID_Probation2Name { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ExpectationSalaryFrom")]
        public Nullable<decimal> ExpectationSalaryFrom { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ExpectationSalaryTo")]
        public Nullable<decimal> ExpectationSalaryTo { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Workplace")]
        public Nullable<int> LSLocationID { get; set; }
        public string LSLocationName { get; set; }

    }


    public class CandidateSearch2ViewModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CandidateCode")]
        public string CandidateCodeSearch { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FullName")]
        public string FullNameSearch { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Result")]
        public Nullable<int> ResultSearch { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IDNo")]
        public string IDNoSearch { get; set; }
        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Gender")]
        public Nullable<int> GenderSearch { get; set; }
        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Workplace")]
        public Nullable<int> LSLocationSearchID { get; set; }
        public string LSLocationSearchName { get; set; }

        //[Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ExpectationPosition1")]
        //public Nullable<int> LSPositionSearchID { get; set; }
        //public string LSPositionSearchName { get; set; }

        //[Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ExpectationPosition2")]
        //public Nullable<int> LSPositionID_ProbationSearch1 { get; set; }
        //public string LSPositionID_ProbationSearch1Name { get; set; }

        //[Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ExpectationPosition3")]
        //public Nullable<int> LSPositionID_ProbationSearch2 { get; set; }
        //public string LSPositionID_ProbationSearch2Name { get; set; }

        //[Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ExpectationSalaryFrom")]
        //public Nullable<decimal> ExpectationSalarySearchFrom { get; set; }

        //[Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ExpectationSalaryTo")]
        //public Nullable<decimal> ExpectationSalarySearchTo { get; set; }



    }
}
