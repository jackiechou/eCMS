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
    
    public partial class TR_tblTrainingPlanExpense
    {
        public int TrainingPlanExpenseID { get; set; }
        public Nullable<int> TrainingPlanID { get; set; }
        public Nullable<int> LSTrainingExpenseID { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<int> LSCurrencyID { get; set; }
    
        public virtual LS_tblCurrency LS_tblCurrency { get; set; }
        public virtual TR_tblPlan TR_tblPlan { get; set; }
    }
}