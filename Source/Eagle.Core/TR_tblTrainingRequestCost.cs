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
    
    public partial class TR_tblTrainingRequestCost
    {
        public int TrainingRequestID { get; set; }
        public int LSTrainingExpenseID { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<decimal> ActualCost { get; set; }
        public int LSCurrencyID { get; set; }
    
        public virtual LS_tblTrainingExpense LS_tblTrainingExpense { get; set; }
        public virtual TR_tblTrainingRequest TR_tblTrainingRequest { get; set; }
    }
}