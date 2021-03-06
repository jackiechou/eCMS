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
    
    public partial class SI_tblConvalescence
    {
        public long ID { get; set; }
        public Nullable<int> YYYY { get; set; }
        public Nullable<int> Quarter { get; set; }
        public Nullable<int> EmpID { get; set; }
        public Nullable<int> Stage { get; set; }
        public Nullable<float> Days { get; set; }
        public Nullable<System.DateTime> LeaveDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<bool> IsConcentrate { get; set; }
        public Nullable<int> Percent { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Note { get; set; }
        public Nullable<int> LeaveType { get; set; }
        public Nullable<int> LSLeaveDayTypeID { get; set; }
        public Nullable<int> InsuranceLeaveID { get; set; }
        public Nullable<decimal> MinSalary { get; set; }
        public string ConditionEffect { get; set; }
    
        public virtual SI_tblInsuranceLeave SI_tblInsuranceLeave { get; set; }
        public virtual Timesheet_tbLeaveDayType Timesheet_tbLeaveDayType { get; set; }
    }
}
