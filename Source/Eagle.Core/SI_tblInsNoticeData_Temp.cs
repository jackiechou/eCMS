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
    
    public partial class SI_tblInsNoticeData_Temp
    {
        public long ID { get; set; }
        public int EmpID { get; set; }
        public System.DateTime AriseDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public string SourceID { get; set; }
        public Nullable<bool> IsInsNotice { get; set; }
        public Nullable<System.DateTime> InsNoticeDate { get; set; }
        public Nullable<byte> Stage { get; set; }
        public Nullable<System.DateTime> IncNoticeDate { get; set; }
        public Nullable<System.DateTime> RightNoticeDate { get; set; }
        public Nullable<decimal> OldSalaryCoef { get; set; }
        public Nullable<decimal> OldSalary { get; set; }
        public Nullable<decimal> NewSalaryCoef { get; set; }
        public Nullable<decimal> NewSalary { get; set; }
        public Nullable<System.DateTime> FromMonth { get; set; }
        public Nullable<System.DateTime> ToMonth { get; set; }
        public Nullable<decimal> Percent { get; set; }
        public Nullable<bool> ChangePosition { get; set; }
        public Nullable<bool> ReturnHICard { get; set; }
        public string NoteInc { get; set; }
        public string NoteDesc { get; set; }
        public Nullable<decimal> PercentSI { get; set; }
        public Nullable<decimal> PercentHI { get; set; }
        public Nullable<decimal> PercentUI { get; set; }
        public Nullable<decimal> SIIncrease { get; set; }
        public Nullable<decimal> SIDecrease { get; set; }
        public Nullable<decimal> HIIncrease { get; set; }
        public Nullable<decimal> HIDecrease { get; set; }
        public Nullable<decimal> UIIncrease { get; set; }
        public Nullable<decimal> UIDecrease { get; set; }
        public Nullable<int> AdjustedMonths { get; set; }
        public Nullable<decimal> SIIncreaseAdjusted { get; set; }
        public Nullable<decimal> SIDecreaseAdjusted { get; set; }
        public Nullable<decimal> HIIncreaseAdjusted { get; set; }
        public Nullable<decimal> HIDecreaseAdjusted { get; set; }
        public Nullable<decimal> UIIncreaseAdjusted { get; set; }
        public Nullable<decimal> UIDecreaseAdjusted { get; set; }
        public Nullable<int> PercentSupplement { get; set; }
        public Nullable<byte> SupplementType { get; set; }
        public Nullable<int> TotalMonth { get; set; }
        public Nullable<bool> IsSupplement { get; set; }
        public Nullable<bool> IsNhapTay { get; set; }
        public string Creater { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string Editer { get; set; }
        public Nullable<System.DateTime> EditTime { get; set; }
    }
}
