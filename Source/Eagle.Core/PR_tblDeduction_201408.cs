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
    
    public partial class PR_tblDeduction_201408
    {
        public int DeductionSalaryID { get; set; }
        public int EmpID { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int LSDeductSalaryID { get; set; }
        public Nullable<decimal> NET { get; set; }
        public Nullable<decimal> GROSS { get; set; }
        public Nullable<decimal> InComeTax { get; set; }
        public Nullable<decimal> DeductionBeforTax { get; set; }
        public Nullable<decimal> DeductionAfterTax { get; set; }
    }
}
