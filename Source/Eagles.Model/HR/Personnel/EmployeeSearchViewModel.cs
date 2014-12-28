using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagle.Model.HR
{
    public class EmployeeSearchViewModel
    {
        [Key]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpID")]
        public int EmpID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmployeeCode")]
        public string EmpCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FullName")]
        public string FullName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastCompanyDefine")]
        public int LSCompanyID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Position")]
        public Nullable<int> LSPositionID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Position")]
        public string LSPositionName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "JoinDate")]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public Nullable<DateTime> JoinDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Status")]
        public Nullable<bool> Active { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastCompanyDefine")]
        public string LSCompanyName { get; set; }

        //public string LSLevel1Name { get; set; }
        //public string LSLevel2Name { get; set; }
    }
}
