using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model
{
    public class TransferEditViewModel
    {
        public int CandidateID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpCode")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string EmployeeCode { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FirstName")]
        public string FirstName { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastName")]
        public string LastName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FullName")]
        public string FullName {
            get{
                return LastName + " " + FirstName;
            }
        }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ApplyDate")]
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}")]
        public DateTime? ApplyDate { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "JoinDate")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public DateTime? JoinDate { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastCompanyDefine")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int? LSCompanyID { get; set; }
        public string LSCompanyName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Position")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int? LSPositionID { get; set; }
        public string LSPositionName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LineManagerID")]
        public int? LineManagerID { get; set; }
        public string LineManagerName { get; set; }
    }
}
