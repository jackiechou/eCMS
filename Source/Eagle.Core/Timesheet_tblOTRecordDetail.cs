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
    
    public partial class Timesheet_tblOTRecordDetail
    {
        public int OTRecordDetailID { get; set; }
        public int OTRecordID { get; set; }
        public Nullable<System.DateTime> DateID { get; set; }
        public Nullable<System.DateTime> TimeInAM { get; set; }
        public Nullable<System.DateTime> TimeOutAM { get; set; }
        public Nullable<System.DateTime> TimeInPM { get; set; }
        public Nullable<System.DateTime> TimeOutPM { get; set; }
        public Nullable<decimal> TotalHours { get; set; }
        public Nullable<decimal> NightOT { get; set; }
        public Nullable<decimal> HolidayOT { get; set; }
        public Nullable<decimal> Rate100 { get; set; }
        public Nullable<decimal> Rate150 { get; set; }
        public Nullable<decimal> Rate200 { get; set; }
        public Nullable<decimal> Rate300 { get; set; }
        public Nullable<decimal> TOIL { get; set; }
        public Nullable<int> TypeOT { get; set; }
    
        public virtual Timesheet_tblOTRecordMaster Timesheet_tblOTRecordMaster { get; set; }
    }
}
