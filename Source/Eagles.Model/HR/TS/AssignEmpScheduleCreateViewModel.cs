using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace Eagle.Model
{
    public class AssignEmpScheduleCreateViewModel
    {
        public int AssignEmpScheduleID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpName")]
        public int EmpID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Year")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int Year { get; set; }

        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Schedule")]
        public int ScheduleID { get; set; }

        public string ScheduleName { get; set; }
        

        public string Creater { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string Editor { get; set; }
        public Nullable<System.DateTime> EditTime { get; set; }



        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmployeeCode")]
        public string EmpCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FullName")]
        public string FullName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Company")]
        public int LSCompanyID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Level1")]
        public Nullable<int> LSLevel1ID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Level2")]
        public Nullable<int> LSLevel2ID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Position")]
        public Nullable<int> LSPositionID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "JoinDate")]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public Nullable<DateTime> JoinDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Status")]
        public Nullable<bool> SearchActive1 { get; set; }
    }
}
