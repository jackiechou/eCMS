using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

using Eagle.Core;

namespace Eagle.Model
{
    /// <summary>
    /// 
    /// </summary>
    public enum TrainingPlanStatus
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
    public enum LevelApproveStatus
    {
        Level0 = 0,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5        
    };

    /// <summary>
    /// 
    /// </summary>
    public class TrainingPlanViewModel : TR_tblPlan
    {        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingPlanMonthYear")]        
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string MonthYear { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingExpenseName")]
        public Nullable<int> LSTrainingExpenseID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Currency")]
        public Nullable<int> LSCurrencyID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Costs")]
        public Nullable<decimal> Cost { get; set; }

        public string TrainingExpenseName { get; set; }
        public string TrainingCurrencyName { get; set; }

        public List<TrainingPlanExpenseModelView> ListOfTrainingExpense = new List<TrainingPlanExpenseModelView>();

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
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingPlanNumberOfEmployees")]
        public Nullable<int> NumOfStaffAllNull { get; set; }               

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Company")]
        public string CompanyName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Department")]
        public string DepartmentName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Section")]
        public string SectionName { get; set; }
        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCourseName")]
        public string TrainingCourseName { get; set; }
        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCodeName")]
        public string TrainingCodeName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCode")]
        public string TrainingCode { get; set; }
        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCategoryName")]
        public string TrainingCategoryName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingProviderName")]
        public string TrainingProviderName { get; set; }
        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingTypeName")]
        public string TrainingTypeName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingFormName")]
        public string TrainingFormName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingLocationName")]
        public string TrainingLocationName { get; set; }
        
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
                    default : break; 
                }
                return result;
            }
        }

        public string PlanUserName { get; set; }

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
    }

    /// <summary>
    /// 
    /// </summary>
    public class TrainingPlanReportViewModel
    {
        public int SEQ { get; set; }
        
        public string TrainingCode { get; set; }
        
        public string TrainingCourse { get; set; }
        
        public string TrainingCategory { get; set; }
        
        public string TrainingLocation { get; set; }        

        public decimal? Cost { get; set; }

        public string CostInfo 
        {
            get
            {
                if (this.Cost.HasValue)
                {
                    return this.Cost.Value.ToString("#,###");
                }
                return String.Empty;
            }
        }
        
        public int NumOffStaff { get; set; }
        
        public int PlanYear { get; set; }

        public int PlanMonth { get; set; }
    }
}
