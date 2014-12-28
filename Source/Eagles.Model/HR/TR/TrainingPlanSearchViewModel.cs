using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class TrainingPlanSearchViewModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingPlanStatus")]        
        public int Status { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingPlanEmpCode")]        
        public string EmpCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingPlanEmpName")]        
        public string EmployeeName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingPlanMonthYear")]        
        public string MonthYear { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastCompanyDefine")]
        public Nullable<int> LSCompanyID { get; set; }

        public string CompanyName { get; set; }        

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCodeName")]
        public Nullable<int> LSTrainingCodeID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCourseName")]
        public Nullable<int> LSTrainingCourseID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingPlanMonth")]
        public Nullable<int> PlanMonth { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingPlanYear")]
        public Nullable<int> PlanYear { get; set; }
     }
}
