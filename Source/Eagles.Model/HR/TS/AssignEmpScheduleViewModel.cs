using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace Eagle.Model
{
    public class AssignEmpScheduleViewModel
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
    }
}
