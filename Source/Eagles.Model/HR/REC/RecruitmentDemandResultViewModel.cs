using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model
{
    public class RecruitmentDemandResultViewModel
    {
        public int DemandID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DemandCode")]
        public string DemandCode { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmployeeCreate")]
        public string EmpCreateName { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastCompanyDefine")]
        public string LSCompanyName { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSPositionID")]
        public string LSPositionName { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DemandQuantity")]
        public int DemandQuantity { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Year")]
        public int Year { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastCompanyDefine")]
        public int? LSCompanyID { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSPositionID")]
        public int? LSPositionID { get; set; }
        
    }
}
