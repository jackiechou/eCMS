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
    
    public partial class HR_tblProject
    {
        public int ProjectID { get; set; }
        public int EmpID { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public Nullable<System.DateTime> FromMonth { get; set; }
        public Nullable<System.DateTime> ToMonth { get; set; }
        public string Position { get; set; }
        public string MainWork { get; set; }
        public string Note { get; set; }
        public Nullable<int> Creater { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
    
        public virtual HR_tblEmp HR_tblEmp { get; set; }
    }
}
