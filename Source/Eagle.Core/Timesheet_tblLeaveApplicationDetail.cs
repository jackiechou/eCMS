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
    
    public partial class Timesheet_tblLeaveApplicationDetail
    {
        public int LeaveApplicationDetailID { get; set; }
        public int LeaveApplicationMasterID { get; set; }
        public System.DateTime LeaveDate { get; set; }
        public decimal Days { get; set; }
        public string TypeLeave { get; set; }
    
        public virtual Timesheet_tblLeaveApplicationMaster Timesheet_tblLeaveApplicationMaster { get; set; }
    }
}
