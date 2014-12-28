using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Eagle.Core;
using System.ComponentModel.DataAnnotations;

namespace Eagle.Model
{
    /// <summary>
    /// 
    /// </summary>
    public enum TrainingRequestType
    {
        FromPlan = 1,
        AdHoc
    }
    /// <summary>
    /// 
    /// </summary>
    public enum TrainingRequestStatus
    {
        Save = 0,
        WaitingForApproval,
        Level1Approval,
        Level1UnApproval,
        Level2Approval,
        Level2UnApproval,
        Level3Approval,
        Level3UnApproval,
        Level4Approval,
        Level4UnApproval,
        Level5Approval,
        Level5UnApproval
    };   

    /// <summary>
    /// 
    /// </summary>
    public class TrainingRequestViewModel : TR_tblTrainingRequest
    {
        // For Evaluation of Employee
        public bool IsComplete { get; set; }
        public int EmployeeEvaluationID { get; set; }
        public string EmployeeEvaluation { get; set; }
        public Nullable<DateTime> FromDate { get; set; }
        public Nullable<DateTime> ToDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingRequestDuration")]
        public string DurationInfo
        {
            get
            {
                return String.Format("({0} - {1})", this.FromDateInfo, this.ToDateInfo);
            }
        }
        public string IsEvaluatedInfo
        {
            get
            {
                return this.IsEvaluated == true ? "X" : String.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string FromDateInfo
        {
            get
            {
                if (this.FromDate.HasValue)
                {
                    return this.FromDate.Value.ToString("dd/MM/yyyy");
                }
                return String.Empty;
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        public string ToDateInfo
        {
            get
            {
                if (this.ToDate.HasValue)
                {
                    return this.ToDate.Value.ToString("dd/MM/yyyy");
                }
                return String.Empty;
            }
        }

        public Nullable<int> TrainingEvaluationID { get; set; }
        // Information nullable value
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCodeName")]
        public Nullable<int> LSTrainingCodeIDAllowNull { get; set; }

        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCourseName")]
        public Nullable<int> LSTrainingCourseIDAllowNull { get; set; }

        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCategoryName")]
        public Nullable<int> LSTrainingCategoryIDAllowNull { get; set; }

        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingTypeName")]
        public Nullable<int> LSTrainingTypeIDAllowNull { get; set; }

        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingRequestLocationInfo")]
        public string LocationInfo 
        { 
            get 
            {  
                return "LocationInfo";   
            } 
            set  
            {
                this.LocationInfo = value;
            }
        } 

        public Nullable<int> NumOfStaffAllowNull { get; set; }
        
        public string Company { get; set; }
        public string TrainingCode { get; set; }
        public string TrainingCodeName { get; set; }
        public string TrainingCourseName { get; set; }
        public string TrainingCategoryName { get; set; }
        public string TrainingProviderName { get; set; }        
        public string TrainingLocationName { get; set; }
        public string TrainingFormName { get; set; }
        public string TrainingTypeName { get; set; }

        // For searching
        public string TrainingTypeInfo
        {
            get
            {
                var result = String.Empty;
                switch (this.TrainingType)
                {
                    case 1: result = TrainingRequestType.FromPlan.ToString(); break;
                    case 2: result = TrainingRequestType.AdHoc.ToString(); break;
                    default: break;
                }
                return result;
            }
        }
        public string StatusInfo
        {
            get
            {
                var result = String.Empty;
                switch (this.Status)
                {
                    case 0: result = TrainingPlanStatus.Save.ToString(); break;
                    case 1: result = TrainingPlanStatus.WaitingForApproval.ToString(); break;
                    case 2: result = TrainingPlanStatus.Level1Approval.ToString(); break;
                    case 3: result = TrainingPlanStatus.Level1UnApproval.ToString(); break;
                    case 4: result = TrainingPlanStatus.Level2Approval.ToString(); break;
                    case 5: result = TrainingPlanStatus.Level2UnApproval.ToString(); break;
                    case 6: result = TrainingPlanStatus.Level3Approval.ToString(); break;
                    case 7: result = TrainingPlanStatus.Level3UnApproval.ToString(); break;
                    case 8: result = TrainingPlanStatus.Level4Approval.ToString(); break;
                    case 9: result = TrainingPlanStatus.Level4UnApproval.ToString(); break;
                    case 10: result = TrainingPlanStatus.Level5Approval.ToString(); break;
                    case 11: result = TrainingPlanStatus.Level5UnApproval.ToString(); break;
                    default: break;
                }
                return result;
            }
        }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingEvaluationTemplateName")]
        public string EvaluationTemplateName { get; set; }        
        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingRequestEvaluationRequest")]
        public string EvaluationRequiredInfo
        {
            get
            {
                return this.EvaluationRequired == true ? "X" : String.Empty;
            }
        }

        public string CourseCompleteStatusInfo
        {
            get
            {
                return this.CourseCompleteStatus == true ? Eagle.Resource.LanguageResource.TrainingRequestCourseCompleteYes : Eagle.Resource.LanguageResource.TrainingRequestCourseCompleteNo;
            }
        }

        public string LevelApproveInfo
        {
            get
            {
                var result = String.Empty;
                switch (this.LevelApprove)
                {
                    case 0: result = LevelApproveStatus.Level0.ToString(); break;
                    case 1: result = LevelApproveStatus.Level1.ToString(); break;
                    case 2: result = LevelApproveStatus.Level2.ToString(); break;
                    case 3: result = LevelApproveStatus.Level3.ToString(); break;
                    case 4: result = LevelApproveStatus.Level4.ToString(); break;
                    case 5: result = LevelApproveStatus.Level5.ToString(); break;
                    default: break;
                }
                return result;
            }
        }

        public string RequestUserName { get; set; }

        /// <summary>
        /// TrainingEvaluationForEmployee for information of duration
        /// </summary>
        public string DurationInfoForTrainingEvaluation { get; set; }

        /// <summary>
        /// TrainingEvaluationForEmployee from evaluation training request
        /// </summary>
        public bool AllowEvaluationByDuraionEnded { get; set; }

        /// <summary>
        /// TrainingEvaluationForEmployee from evaluation training request
        /// </summary>
        public bool IsEvaluated { get; set; }



        /// <summary>
        /// TrainingEvaluationForEmployee from evaluation training request
        /// </summary>
        public bool IsMaxLevelApproval { get; set; }
        
    }    
}
