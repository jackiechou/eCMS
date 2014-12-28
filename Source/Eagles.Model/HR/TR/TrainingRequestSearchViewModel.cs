using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

using Eagle.Common.Utilities;

namespace Eagle.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class TrainingRequestSearchViewModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Evaluation")]
        public int StatusOfEvaluation { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingPlanEmpCode")]
        public string EmpCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingPlanEmpName")]
        public string EmployeeName { get; set; }

        // Information of TrainingRequest
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastCompanyDefine")]
        public Nullable<int> LSCompanyID { get; set; }

        public string CompanyName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCode")]
        public Nullable<int> LSTrainingCodeID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCourse")]
        public Nullable<int> LSTrainingCourseID { get; set; }

        // Information of TrainingRequestDuration
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingRequestFromDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FromDate { get; set; }

        public string FromDateInfo
        {
            get 
            {
                if (this.FromDate.HasValue)
                {
                    return DateTimeUtils.ToString(this.FromDate.Value, "dd/MM/yyyy"); 
                }
                return String.Empty;                
            }
        }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingRequestToDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ToDate { get; set; }
        public string ToDateInfo
        {
            get
            {
                if (this.ToDate.HasValue)
                {
                    return DateTimeUtils.ToString(this.ToDate.Value, "dd/MM/yyyy");
                }
                return String.Empty;                
            }            
        }

        // Information of TrainingRequestDurationUser
        public string EmplyeeName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingEvaluationCreater")]
        public Nullable<int> EmpID { get; set; }

        // Information of TrainingRequestCost
        public Nullable<int> LSTrainingExpenseID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingRequestStatus")]
        public Nullable<int> Status { get; set; }
    }
}
