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
    
    public partial class SYS_tblParameter
    {
        public int ParameterID { get; set; }
        public int CutOffSI { get; set; }
        public int TypeOfSalary { get; set; }
        public int LeaveType { get; set; }
        public System.DateTime DateExpireLeaveForward { get; set; }
        public int OTLimit { get; set; }
        public int StandardWorking { get; set; }
        public int StandardAnnualLeave { get; set; }
        public Nullable<System.DateTime> NightOTFrom { get; set; }
        public Nullable<System.DateTime> NightOTTo { get; set; }
        public string CutOffSalary { get; set; }
    }
}
