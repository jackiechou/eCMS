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
    
    public partial class HR_tblWorking
    {
        public int WorkingID { get; set; }
        public int EmpID { get; set; }
        public int LSCompanyID { get; set; }
        public int LSChangeStatusID { get; set; }
        public System.DateTime EffectiveDate { get; set; }
        public Nullable<int> LineManagerID { get; set; }
        public Nullable<int> LSLocationID { get; set; }
        public Nullable<int> LSPositionID { get; set; }
        public Nullable<int> LSGradeID { get; set; }
        public Nullable<bool> Decision { get; set; }
        public string DecisionNo { get; set; }
        public Nullable<System.DateTime> SignDate { get; set; }
        public Nullable<int> SignerEmpID { get; set; }
        public Nullable<int> FileId { get; set; }
        public string Note { get; set; }
        public Nullable<int> Creater { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    }
}
