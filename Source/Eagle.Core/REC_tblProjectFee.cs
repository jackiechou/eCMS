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
    
    public partial class REC_tblProjectFee
    {
        public int ProjectID { get; set; }
        public int LSRecruitmentFeeID { get; set; }
        public decimal Amount { get; set; }
        public int CurrencyID { get; set; }
    
        public virtual LS_tblCurrency LS_tblCurrency { get; set; }
        public virtual LS_tblRecruitmentFee LS_tblRecruitmentFee { get; set; }
        public virtual REC_tblProject REC_tblProject { get; set; }
    }
}
