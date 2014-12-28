//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Eagle.Core
{
    using System;
    using System.Collections.Generic;
    
    public partial class TR_tblPlan
    {
        public TR_tblPlan()
        {
            this.TR_tblPlanComment = new HashSet<TR_tblPlanComment>();
            this.TR_tblTrainingPlanExpense = new HashSet<TR_tblTrainingPlanExpense>();
        }
    
        public int TrainingPlanID { get; set; }
        public int EmpIDCreate { get; set; }
        public Nullable<int> LSCompanyID { get; set; }
        public int LSTrainingCodeID { get; set; }
        public int LSTrainingCategoryID { get; set; }
        public int LSTrainingCourseID { get; set; }
        public Nullable<int> LSProviderID { get; set; }
        public string LearningObjective { get; set; }
        public int LSTrainingTypeID { get; set; }
        public Nullable<int> LSTrainingFormID { get; set; }
        public Nullable<int> LSTrainingLocationID { get; set; }
        public int PlanMonth { get; set; }
        public int PlanYear { get; set; }
        public Nullable<decimal> PlanDays { get; set; }
        public Nullable<int> PlanHours { get; set; }
        public int NumOfStaff { get; set; }
        public int Status { get; set; }
        public string Creater { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
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
        public virtual ICollection<TR_tblPlanComment> TR_tblPlanComment { get; set; }
        public virtual ICollection<TR_tblTrainingPlanExpense> TR_tblTrainingPlanExpense { get; set; }
    }
}
