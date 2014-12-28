using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Eagle.Core.MetaData
{
    [MetadataTypeAttribute(typeof(TR_tblTrainingRequestComment.Metadata))]
    public partial class TR_tblTrainingRequestComment
    {
        internal class Metadata
        {
            public int CommentID { get; set; }
            public Nullable<int> TraningRequestID { get; set; }
            public Nullable<int> EmpID { get; set; }
            public Nullable<System.DateTime> Time { get; set; }
            public string Comment { get; set; }
            public string StatusName { get; set; }

            public virtual TR_tblTrainingRequest TR_tblTrainingRequest { get; set; }
        }        
    }
    [MetadataTypeAttribute(typeof(TR_tblPlanComment.Metadata))]
    public partial class TR_tblPlanComment
    {
        internal class Metadata
        {
            public int CommentID { get; set; }
            public Nullable<int> TraningPlanID { get; set; }
            public Nullable<int> EmpID { get; set; }
            public Nullable<System.DateTime> Time { get; set; }
            public string Comment { get; set; }
            public string StatusName { get; set; }

            public virtual TR_tblPlan TR_tblPlan { get; set; }
        }        
    }
    [MetadataTypeAttribute(typeof(TR_sprptTrainingPlanExpense_Result.Metadata))]
    public partial class TR_sprptTrainingPlanExpense_Result
    {
        internal class Metadata
        {
            public int TrainingRequestID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCodeName")]
            public Nullable<int> LSTrainingCodeID { get; set; }

            public string TrainingCode { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCodeName")]
            public Nullable<int> LSTrainingCourseID { get; set; }
            public string TrainingCourse { get; set; }
            public string TrainingCategory { get; set; }
            public int NumOfStaff { get; set; }
            public string DurationInfo { get; set; }
            public Nullable<int> TrainingPlanID { get; set; }
            public Nullable<decimal> ExpenseOfTrainingPlan { get; set; }
            public Nullable<decimal> ExpenseOfTrainingRequest { get; set; }
            public string ExpenseOfTrainingPlanInfo { get; set; }
            public string ExpenseOfTrainingRequestInfo { get; set; }
        }        
    }    

    /// <summary>
    /// 
    /// </summary>
    [MetadataTypeAttribute(typeof(TR_sprptTrainingPlanEvaluation_Result.Metadata))]
    public partial class TR_sprptTrainingPlanEvaluation_Result
    {
        internal class Metadata
        {
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingEmployee")]
            public Nullable<int> EmpID { get; set; }
            public string FullName { get; set; }
            public Nullable<int> TrainingRequestID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCodeName")]
            public int LSTrainingCodeID { get; set; }

            public string TrainingCode { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCourseName")]
            public int LSTrainingCourseID { get; set; }

            public string TrainingCourse { get; set; }

            public string FromDate { get; set; }
            public string ToDate { get; set; }
            
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingRequestEvaluationRequest")]
            public Nullable<bool> EvaluationRequired { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingRequestEvaluationOfEmployee")]
            public Nullable<int> NumOfEvaluation { get; set; }

            public string EvaluationRequiredInfo { get; set; }
            public string EmployeeEvaluation { get; set; }
            public string Seq { get; set; }
            public string DurationInfo { get; set; }
        }        
    }

    /// <summary>
    /// 
    /// </summary>
    [MetadataTypeAttribute(typeof(TR_sprptTrainingPlan_Result.Metadata))]
    public partial class TR_sprptTrainingPlan_Result
    {
        internal class Metadata
        {
            public string Seq { get; set; }
            public string CostInfo { get; set; }            
            public int TrainingPlanID { get; set; }
            public string PlanCreater { get; set; }
            public string TrainingCode { get; set; }
            public string TrainingCourse { get; set; }
            public string LearningObjective { get; set; }
            public int NumOfStaff { get; set; }
            public Nullable<decimal> PlanDays { get; set; }
            public Nullable<int> PlanHours { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingPlanMonth")]
            public int PlanMonth { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingPlanYear")]
            public int PlanYear { get; set; }

            public Nullable<decimal> Cost { get; set; }
            public string TrainingCategory { get; set; }
            public string TrainingProvider { get; set; }
            public string TrainingType { get; set; }
            public string TrainingForm { get; set; }
            public string TrainingLocation { get; set; }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    [MetadataTypeAttribute(typeof(TR_tblTrainingEvaluationPart.Metadata))]
    public partial class TR_tblTrainingEvaluationPart
    {
        internal class Metadata
        {
            public int TrainingEvaluationID { get; set; }
            public int LSTrainingApprisalPartID { get; set; }
            public string Note { get; set; }

            public virtual LS_tblTrainingApprisalPart LS_tblTrainingApprisalPart { get; set; }
            public virtual TR_tblTrainingEvaluation TR_tblTrainingEvaluation { get; set; }
        }        
    }
    /// <summary>
    /// 
    /// </summary>
    [MetadataTypeAttribute(typeof(TR_tblTrainingEvaluationDetail.Metadata))]
    public partial class TR_tblTrainingEvaluationDetail
    {
        internal class Metadata
        {            
            public int TrainingEvaluationDetailID { get; set; }
            public int TrainingEvaluationID { get; set; }
            public Nullable<int> LSTrainingApprisalPartID { get; set; }
            public Nullable<int> LSTrainingApprisalItemID { get; set; }
            public Nullable<int> LSTrainingAnswerTypeID { get; set; }
            public Nullable<bool> IsAnswer { get; set; }
            public string Comment { get; set; }
    
            public virtual LS_tblTrainingApprisalItem LS_tblTrainingApprisalItem { get; set; }
            public virtual LS_tblTrainingApprisalPart LS_tblTrainingApprisalPart { get; set; }            
        }        
    }

    /// <summary>
    /// 
    /// </summary>
    [MetadataTypeAttribute(typeof(TR_tblTrainingEvaluation.Metadata))]
    public partial class TR_tblTrainingEvaluation
    {
        internal class Metadata
        {
            public int TrainingEvaluationID { get; set; }

            public Nullable<int> TrainingRequestID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]            
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingEvaluationTemplateName")]            
            public int EvaluationTemplateID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingEvaluationEmployee")]            
            public Nullable<int> EvaluationEmpID { get; set; }

            public string Comment { get; set; }
            public Nullable<int> Creater { get; set; }
            public Nullable<System.DateTime> CreateDate { get; set; }
            public Nullable<int> Updater { get; set; }
            public Nullable<System.DateTime> UpdateDate { get; set; }
            public Nullable<bool> IsComplete { get; set; }

            public virtual TR_tblTrainingRequest TR_tblTrainingRequest { get; set; }           
            public virtual ICollection<TR_tblTrainingEvaluationPart> TR_tblTrainingEvaluationPart { get; set; }
            public virtual ICollection<TR_tblTrainingEvaluationDetail> TR_tblTrainingEvaluationDetail { get; set; }
        }        
    }
    /// <summary>
    /// 
    /// </summary>
    [MetadataTypeAttribute(typeof(TR_tblTemplateItem.Metadata))]
    public partial class TR_tblTemplateItem
    {
        internal class Metadata
        {
            public int TemplateItemID { get; set; }
            public int EvaluationTemplateID { get; set; }
            public int LSTrainingApprisalPartID { get; set; }
            public int LSTrainingApprisalItemID { get; set; }
            public string Note { get; set; }

            public virtual LS_tblTrainingApprisalItem LS_tblTrainingApprisalItem { get; set; }
            public virtual TR_tblEvaluationTemplate TR_tblEvaluationTemplate { get; set; }


        }        
    }
    /// <summary>
    /// 
    /// </summary>
    [MetadataTypeAttribute(typeof(TR_tblEvaluationTemplateDetail.Metadata))]
    public partial class TR_tblEvaluationTemplateDetail
    {
        internal class Metadata
        {
            public int EvaluationTemplateID { get; set; }
            public int LSTrainingApprisalPartID { get; set; }
            public string Note { get; set; }

            public virtual LS_tblTrainingApprisalPart LS_tblTrainingApprisalPart { get; set; }
            public virtual TR_tblEvaluationTemplate TR_tblEvaluationTemplate { get; set; }                        
        }        
    }

    /// <summary>
    /// 
    /// </summary>
    [MetadataTypeAttribute(typeof(TR_tblEvaluationTemplate.Metadata))]
    public partial class TR_tblEvaluationTemplate
    {
        internal class Metadata
        {
            public int EvaluationTemplateID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [StringLength(50)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingEvaluationTemplateCode")]
            [Remote("ValidationTrainingApprisalItem", "Validation", AdditionalFields = "InitialEvaluationTemplate")]
            public string EvaluationTemplateCode { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [StringLength(100)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingEvaluationTemplateName")]
            public string EvaluationTemplateName { get; set; }
            public Nullable<int> CreateEmpID { get; set; }
            public Nullable<System.DateTime> CreateDate { get; set; }
            public Nullable<int> UpdateEmpID { get; set; }
            public Nullable<System.DateTime> UpdateDate { get; set; }
            
            [StringLength(200)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Comment")]
            public string Note { get; set; }

            public virtual ICollection<TR_tblEvaluationTemplateDetail> TR_tblEvaluationTemplateDetail { get; set; }
            public virtual ICollection<TR_tblTemplateItem> TR_tblTemplateItem { get; set; }                        
        }        
    }

    /// <summary>
    /// 
    /// </summary>
    [MetadataTypeAttribute(typeof(LS_tblTrainingApprisalItem.Metadata))]
    public partial class LS_tblTrainingApprisalItem
    {
        internal class Metadata
        {
            public int LSTrainingApprisalItemID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [StringLength(50)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingApprisalItemCode")]
            [Remote("ValidationTrainingApprisalItem", "Validation", AdditionalFields = "InitialTrainingApprisalItem")]
            public string LSTrainingApprisalItemCode { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [StringLength(100)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingApprisalItemName")]
            public string Name { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingApprisalPart")]
            public Nullable<int> LSTrainingApprisalPartID { get; set; }
            
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "AnswerType")]
            public Nullable<int> LSAnswerTypeID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<int> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingApprisalItemMultiAnswer")]
            public Nullable<bool> IsMultiAnswer { get; set; }

            [StringLength(200)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }

            public virtual LS_tblAnswerType LS_tblAnswerType { get; set; }
            public virtual LS_tblTrainingApprisalPart LS_tblTrainingApprisalPart { get; set; }

            public virtual ICollection<LS_tblTrainingAnswerType> LS_tblTrainingAnswerType { get; set; }
            public virtual ICollection<TR_tblTemplateItem> TR_tblTemplateItem { get; set; }
            public virtual ICollection<TR_tblTrainingEvaluationDetail> TR_tblTrainingEvaluationDetail { get; set; }
        }
        
    }
    /// <summary>
    /// 
    /// </summary>
    [MetadataTypeAttribute(typeof(LS_tblTrainingApprisalPart.Metadata))]
    public partial class LS_tblTrainingApprisalPart
    {
        internal class Metadata
        {            
            public int LSTrainingApprisalPartID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [StringLength(50)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingApprisalPartCode")]
            [Remote("ValidationTrainingApprisalPart", "Validation", AdditionalFields = "InitialTrainingApprisalPart")]
            public string LSTrainingApprisalPartCode { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [StringLength(100)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingApprisalPartName")]
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<int> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            [StringLength(200)]
            public string Note { get; set; }

            public virtual ICollection<LS_tblTrainingApprisalItem> LS_tblTrainingApprisalItem { get; set; }
        }        
    }
    /// <summary>
    /// 
    /// </summary>
    [MetadataTypeAttribute(typeof(LS_tblTrainingAnswerType.Metadata))]
    public partial class LS_tblTrainingAnswerType
    {
        internal class Metadata
        {
            public int LSTrainingAnswerTypeID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [StringLength(50)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingAnswerTypeCode")]
            [Remote("ValidationTrainingAnswerType", "Validation", AdditionalFields = "InitialTrainingAnswerType")]
            public string LSTrainingAnswerTypeCode { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [StringLength(100)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingAnswerTypeName")]
            public string Name { get; set; }            

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<int> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            [StringLength(200)]
            public string Note { get; set; }

            public virtual LS_tblTrainingApprisalItem LS_tblTrainingApprisalItem { get; set; }
        }        
    }
    /// <summary>
    /// 
    /// </summary>
    [MetadataTypeAttribute(typeof(LS_tblAnswerType.Metadata))]
    public partial class LS_tblAnswerType
    {
        internal class Metadata
        {
            public int LSAnswerTypeID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [StringLength(50)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "AnswerTypeCode")]
            [Remote("ValidationAnswerType", "Validation", AdditionalFields = "InitialAnswerType")]
            public string AnswerTypeCode { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [StringLength(100)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "AnswerTypeName")]
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<int> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            [StringLength(200)]
            public string Note { get; set; }
            
            public virtual ICollection<LS_tblTrainingApprisalItem> LS_tblTrainingApprisalItem { get; set; }
        }
        
    }
    /// <summary>
    /// 
    /// </summary>
    [MetadataTypeAttribute(typeof(TR_tblTrainingRequestCost.Metadata))]
    public partial class TR_tblTrainingRequestCost
    {
        internal class Metadata
        {
            public int TrainingRequestID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingExpenseName")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int LSTrainingExpenseID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingRequestCost")]            
            public Nullable<decimal> Cost { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingRequestActualCost")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public Nullable<decimal> ActualCost { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Currency")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int LSCurrencyID { get; set; }

            public virtual LS_tblTrainingExpense LS_tblTrainingExpense { get; set; }
            public virtual TR_tblTrainingRequest TR_tblTrainingRequest { get; set; }
        }        
    }

    /// <summary>
    /// 
    /// </summary>
    public partial class TR_tblDurationUser
    {
        internal class Metadata
        {
            public int TrainingDurationID { get; set; }
            public int EmpID { get; set; }
            public string Note { get; set; }

            public virtual HR_tblEmp HR_tblEmp { get; set; }
            public virtual TR_tblTrainingRequestDuration TR_tblTrainingRequestDuration { get; set; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [MetadataTypeAttribute(typeof(TR_tblTrainingRequestDuration.Metadata))]
    public partial class TR_tblTrainingRequestDuration
    {
        internal class Metadata
        {
            public int TrainingDurationID { get; set; }
            public int TrainingRequestID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingRequestFromDate")]            
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public Nullable<DateTime> FromDate { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingRequestToDate")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public Nullable<DateTime> ToDate { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingRequestTotalDays")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int TotalDays { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingRequestTotalHours")]
            public Nullable<int> TotalHours { get; set; }            

            public virtual ICollection<TR_tblDurationUser> TR_tblDurationUser { get; set; }
            public virtual TR_tblTrainingRequest TR_tblTrainingRequest { get; set; }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    [MetadataTypeAttribute(typeof(TR_tblTrainingRequest.Metadata))]
    public partial class TR_tblTrainingRequest
    {
        internal class Metadata
        {
            public int TrainingRequestID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingRequestCreater")]
            public int EmpIDCreate { get; set; }
            
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastCompanyDefine")]
            public Nullable<int> LSCompanyID { get; set; }
            
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCodeName")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int LSTrainingCodeID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCategoryName")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int LSTrainingCategoryID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCourseName")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int LSTrainingCourseID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingProviderName")]
            public Nullable<int> LSProviderID { get; set; }
            
            public string LearningObjective { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingTypeName")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int LSTrainingTypeID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingFormName")]
            public Nullable<int> LSTrainingFormID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingLocationName")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public Nullable<int> LSTrainingLocationID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingPlanNumberOfEmployees")]
            public int NumOfStaff { get; set; }

            public int TrainingType { get; set; }

            public Nullable<int> TrainingPlanID { get; set; }

            public int Status { get; set; }
            public decimal TotalCost { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingRequestEvaluationRequest")]
            public bool EvaluationRequired { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingEvaluationTemplateName")]            
            public Nullable<int> EvaluationTemplateID { get; set; }

            public bool CourseCompleteStatus { get; set; }
            public string Creater { get; set; }
            public Nullable<System.DateTime> CreateTime { get; set; }
            public int LevelApprove { get; set; }
            public Nullable<System.DateTime> DateLevel1 { get; set; }
            public Nullable<int> EmpIDLevel1 { get; set; }
            public string ReasonLevel1 { get; set; }
            public Nullable<System.DateTime> DateLevel2 { get; set; }
            public Nullable<int> EmpIDLevel2 { get; set; }
            public string ReasonLevel2 { get; set; }
            public Nullable<System.DateTime> DateLevel3 { get; set; }
            public Nullable<int> EmpIDLevel3 { get; set; }
            public string ReasonLevel3 { get; set; }
            public Nullable<System.DateTime> DateLevel4 { get; set; }
            public Nullable<int> EmpIDLevel4 { get; set; }
            public string ReasonLevel4 { get; set; }
            public Nullable<System.DateTime> DateLevel5 { get; set; }
            public Nullable<int> EmpIDLevel5 { get; set; }
            public string ReasonLevel5 { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingRequestComment")]            
            public string Comment { get; set; }
            public string CurrentComment { get; set; }
            public Nullable<bool> isFirstComment { get; set; }

            public virtual HR_tblEmp HR_tblEmp { get; set; }
            public virtual LS_tblCompany LS_tblCompany { get; set; }
            public virtual LS_tblTrainingCategory LS_tblTrainingCategory { get; set; }
            public virtual LS_tblTrainingCode LS_tblTrainingCode { get; set; }
            public virtual LS_tblTrainingCourse LS_tblTrainingCourse { get; set; }
            public virtual LS_tblTrainingForm LS_tblTrainingForm { get; set; }
            public virtual LS_tblTrainingLocation LS_tblTrainingLocation { get; set; }
            public virtual LS_tblTrainingProvider LS_tblTrainingProvider { get; set; }
            public virtual LS_tblTrainingType LS_tblTrainingType { get; set; }
            public virtual ICollection<TR_tblTrainingEvaluation> TR_tblTrainingEvaluation { get; set; }
            public virtual ICollection<TR_tblTrainingRequestCost> TR_tblTrainingRequestCost { get; set; }
            public virtual ICollection<TR_tblTrainingRequestDuration> TR_tblTrainingRequestDuration { get; set; }
            public virtual ICollection<TR_tblTrainingRequestComment> TR_tblTrainingRequestComment { get; set; }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    [MetadataTypeAttribute(typeof(TR_tblTrainingPlanExpense.Metadata))]
    public partial class TR_tblTrainingPlanExpense
    {
        internal class Metadata
        {
            public int TrainingPlanExpenseID { get; set; }
            public Nullable<int> TrainingPlanID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingExpenseName")]
            public Nullable<int> LSTrainingExpenseID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Costs")]
            public Nullable<decimal> Cost { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Currency")]
            public Nullable<int> LSCurrencyID { get; set; }

            public virtual LS_tblCurrency LS_tblCurrency { get; set; }
            public virtual TR_tblPlan TR_tblPlan { get; set; }
        }        
    }

    /// <summary>
    /// 
    /// </summary>
    [MetadataTypeAttribute(typeof(TR_tblPlan.Metadata))]
    public partial class TR_tblPlan
    {
        internal class Metadata
        {
            public int TrainingPlanID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingPlanCreater")]
            public int EmpIDCreate { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastCompanyDefine")]
            public Nullable<int> LSCompanyID { get; set; }           

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCodeName")]
            public int LSTrainingCodeID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCategoryName")]
            public int LSTrainingCategoryID { get; set; }


            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCourseName")]
            public int LSTrainingCourseID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingProviderName")]
            public Nullable<int> LSProviderID { get; set; }

            [StringLength(200)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCourseLearningObjectives")]
            public string LearningObjective { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingTypeName")]
            public int LSTrainingTypeID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingFormName")]
            public Nullable<int> LSTrainingFormID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingLocationName")]
            public Nullable<int> LSTrainingLocationID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingPlanMonth")]
            public int PlanMonth { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingPlanYear")]
            public int PlanYear { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingPlanDay")]
            public Nullable<decimal> PlanDays { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingPlanHours")]
            public Nullable<int> PlanHours { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingPlanNumberOfEmployees")]
            public int NumOfStaff { get; set; }

            public int Status { get; set; }
            public string Creater { get; set; }
            public Nullable<System.DateTime> CreateTime { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
            public int LevelApprove { get; set; }
            public Nullable<System.DateTime> DateLevel1 { get; set; }
            public Nullable<int> EmpIDLevel1 { get; set; }

            
            public string ReasonLevel1 { get; set; }
            public Nullable<System.DateTime> DateLevel2 { get; set; }
            public Nullable<int> EmpIDLevel2 { get; set; }
            public string ReasonLevel2 { get; set; }
            public Nullable<System.DateTime> DateLevel3 { get; set; }
            public Nullable<int> EmpIDLevel3 { get; set; }
            public string ReasonLevel3 { get; set; }
            public Nullable<System.DateTime> DateLevel4 { get; set; }
            public Nullable<int> EmpIDLevel4 { get; set; }
            public string ReasonLevel4 { get; set; }
            public Nullable<System.DateTime> DateLevel5 { get; set; }
            public Nullable<int> EmpIDLevel5 { get; set; }
            public string ReasonLevel5 { get; set; }
            
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Comment")]
            public string Comment { get; set; }
                        
            public string CurrentComment { get; set; }

            public Nullable<bool> isFirstComment { get; set; }

            public virtual ICollection<TR_tblTrainingPlanExpense> TR_tblTrainingPlanExpense { get; set; }
            public virtual ICollection<TR_tblPlanComment> TR_tblPlanComment { get; set; }
            
        }
    }
    /// <summary>
    /// 
    /// </summary>
    [MetadataTypeAttribute(typeof(LS_tblTrainingCourse.Metadata))]
    public partial class LS_tblTrainingCourse
    {                
        internal class Metadata
        {           
            public int LSTrainingCourseID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [StringLength(50)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCourseCode")]
            [Remote("ValidationTrainingCourse", "Validation", AdditionalFields = "InitialTrainingCourse")]
            public string LSTrainingCourseCode { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [StringLength(200)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCourseName")]
            public string Name { get; set; }

            [StringLength(200)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCourseLearningObjectives")]
            public string LearningObjectives { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCodeName")]
            public int LSTrainingCodeID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCategoryName")]
            public int LSTrainingCategoryID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingProviderName")]
            public Nullable<int> LSTrainingProviderID { get; set; }

            public Nullable<int> Rank { get; set; }

            public bool Used { get; set; }

            [StringLength(100)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }

            public virtual LS_tblTrainingCategory LS_tblTrainingCategory { get; set; }
            public virtual LS_tblTrainingCode LS_tblTrainingCode { get; set; }
            public virtual LS_tblTrainingProvider LS_tblTrainingProvider { get; set; }            


        }
    }
    /// <summary>
    /// 
    /// </summary>
    [MetadataTypeAttribute(typeof(LS_tblTrainingCode.MetaData))]
    public partial class LS_tblTrainingCode
    {
        public string InitialTrainingCode { get { return Code; } }

        public string UsedInfo
        {
            get
            {
                return this.Used == true ? "X" : String.Empty;
            }
        }
       
        internal sealed class MetaData
        {
            public int LSTrainingCodeID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]          
            [StringLength(50)]   
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCode")]
            [Remote("ValidationTrainingCode", "Validation", AdditionalFields = "InitialTrainingCode")]
            public string Code { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [StringLength(200)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingName")]                        
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<int> Rank { get; set; }

            [StringLength(100)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }           
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [MetadataTypeAttribute(typeof(LS_tblTrainingCategory.Metadata))]
    public partial class LS_tblTrainingCategory
    {
        public string InitialTrainingCategory { get { return Code; } }

        public string UsedInfo
        {
            get
            {
                return this.Used == true ? "X" : String.Empty;
            }
        }

        internal class Metadata
        {
            public int LSTrainingCategoryID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [StringLength(50)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCategoryCode")]
            [Remote("ValidationTrainingCategory", "Validation", AdditionalFields = "InitialTrainingCategory")]
            public string Code { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [StringLength(200)]  
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCategoryName")]
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<int> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }

            [StringLength(100)]  
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [MetadataTypeAttribute(typeof(LS_tblTrainingProvider.Metadata))]
    public partial class LS_tblTrainingProvider
    {
        public string InitialTrainingProvider { get { return this.Code; } }

        public string UsedInfo
        {
            get
            {
                return this.Used == true ? "X" : String.Empty;
            }
        }

        internal class Metadata
        {
            public int LSTrainingProviderID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [StringLength(50)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingProviderCode")]
            [Remote("ValidationTrainingProvider", "Validation", AdditionalFields = "InitialTrainingProvider")]
            public string Code { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [StringLength(200)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingProviderName")]
            public string Name { get; set; }
            
            [StringLength(200)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingProviderAddress")]
            public string Address { get; set; }
            
            [StringLength(50)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingProviderPhone")]
            public string Phone { get; set; }

            [StringLength(50)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingProviderFax")]
            public string Fax { get; set; }

            [StringLength(100)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingProviderContactPerson")]
            public string ContactPerson { get; set; }

            [StringLength(100)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingProviderWebsite")]
            [DataType(DataType.Url, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "TrainingProviderWebsiteInvalid")]
            [RegularExpression(@"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$", ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "TrainingProviderWebsiteInvaild")]
            public string Website { get; set; }

            [StringLength(100)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingProviderEmail")]
            [DataType(DataType.EmailAddress, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "EmailInValid")]
            [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "EmailInValid")]
            public string Email { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<int> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }

            [StringLength(100)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [MetadataTypeAttribute(typeof(LS_tblTrainingExpense.MetaData))]
    public partial class LS_tblTrainingExpense
    {
        public string InitialTrainingExpense { get { return Code; } }

        public string UsedInfo
        {
            get
            {
                return this.Used == true ? "X" : String.Empty;
            }
        }

        internal sealed class MetaData
        {
            public int LSTrainingExpenseID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [StringLength(50)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingExpenseCode")]
            [Remote("ValidationTrainingExpense", "Validation", AdditionalFields = "InitialTrainingCode")]
            public string Code { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [StringLength(200)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingExpenseName")]
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<int> Rank { get; set; }

            [StringLength(100)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [MetadataTypeAttribute(typeof(LS_tblTrainingForm.Metadata))]
    public partial class LS_tblTrainingForm
    {
        public string InitialTrainingForm { get { return Code; } }

        public string UsedInfo
        {
            get
            {
                return this.Used == true ? "X" : String.Empty;
            }
        }

        internal class Metadata
        {
            public int LSTrainingFormID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [StringLength(50)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingFormCode")]
            [Remote("ValidationTrainingForm", "Validation", AdditionalFields = "InitialTrainingForm")]
            public string Code { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [StringLength(200)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingFormName")]
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<int> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }

            [StringLength(100)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [MetadataTypeAttribute(typeof(LS_tblTrainingType.Metadata))]
    public partial class LS_tblTrainingType
    {
        public string InitialTrainingType { get { return this.LSTrainingTypeCode; } }

        public string UsedInfo
        {
            get
            {
                return this.Used == true ? "X" : String.Empty;
            }
        }
        internal class Metadata
        {
            public int LSTrainingTypeID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [StringLength(50)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingTypeCode")]
            [Remote("ValidationTrainingType", "Validation", AdditionalFields = "InitialTrainingType")]
            public string LSTrainingTypeCode { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [StringLength(200)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingTypeName")]
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<int> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }

            [StringLength(100)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [MetadataTypeAttribute(typeof(LS_tblTrainingLocation.Metadata))]
    public partial class LS_tblTrainingLocation
    {
        public string InitialTrainingLocation { get { return Code; } }

        public string UsedInfo
        {
            get
            {
                return this.Used == true ? "X" : String.Empty;
            }
        }
        
        internal class Metadata
        {
            public int LSTrainingLocationID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [StringLength(50)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingLocationCode")]
            [Remote("ValidationTrainingLocation", "Validation", AdditionalFields = "InitialTrainingLocation")]
            public string Code { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [StringLength(200)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingLocationName")]
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<int> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }

            [StringLength(100)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
        }        
    }    
}
