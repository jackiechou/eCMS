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
    
    public partial class vTimeKeeping
    {
        public int TimekeepingID { get; set; }
        public int EmpID { get; set; }
        public System.DateTime DateID { get; set; }
        public int LSShiftID { get; set; }
        public Nullable<System.DateTime> TimeInShift { get; set; }
        public Nullable<System.DateTime> TimeOutShift { get; set; }
        public Nullable<System.DateTime> TimeIn { get; set; }
        public Nullable<System.DateTime> TimeOut { get; set; }
        public int Type { get; set; }
        public Nullable<System.DateTime> TimeInLate { get; set; }
        public Nullable<System.DateTime> TimeOutEarly { get; set; }
        public Nullable<int> LSLeaveDayTypeID { get; set; }
        public Nullable<decimal> LeaveDays { get; set; }
        public decimal WorkDays { get; set; }
        public string MMYYYY { get; set; }
    }
}
