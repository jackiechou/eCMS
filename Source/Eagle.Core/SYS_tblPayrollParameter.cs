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
    
    public partial class SYS_tblPayrollParameter
    {
        public int ID { get; set; }
        public System.DateTime EffectiveDate { get; set; }
        public Nullable<decimal> CoefInsS_E { get; set; }
        public Nullable<decimal> CoefInsS_C { get; set; }
        public Nullable<decimal> CoefInsH_E { get; set; }
        public Nullable<decimal> CoefInsH_C { get; set; }
        public Nullable<decimal> CoefInsE_E { get; set; }
        public Nullable<decimal> CoefInsE_C { get; set; }
        public Nullable<decimal> MinSalary { get; set; }
        public Nullable<int> CoefOver { get; set; }
        public Nullable<decimal> SelfDeduction { get; set; }
        public decimal DependDeduction { get; set; }
        public string Description { get; set; }
    }
}
