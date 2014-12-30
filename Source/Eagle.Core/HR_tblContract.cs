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
    
    public partial class HR_tblContract
    {
        public int ContractID { get; set; }
        public string ContractNo { get; set; }
        public int LSContractTypeID { get; set; }
        public int ContractStatus { get; set; }
        public int EmpID { get; set; }
        public Nullable<int> SignedEmpID { get; set; }
        public Nullable<System.DateTime> SignedDate { get; set; }
        public System.DateTime EffectiveDate { get; set; }
        public Nullable<System.DateTime> ExpiredDate { get; set; }
        public Nullable<System.DateTime> ProbationFrom { get; set; }
        public Nullable<System.DateTime> ProbationTo { get; set; }
        public Nullable<decimal> ProbationSalary { get; set; }
        public Nullable<decimal> InsuranceSalary { get; set; }
        public int LSPositionID { get; set; }
        public Nullable<int> LSLocationID { get; set; }
        public int MethodPIT { get; set; }
        public Nullable<decimal> PercentPIT { get; set; }
        public string FileIds { get; set; }
        public string Note { get; set; }
        public Nullable<int> Creater { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}