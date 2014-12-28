using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Core;
using System.ComponentModel.DataAnnotations;


namespace Eagle.Model
{
    public class QuotaRecruitmentViewModel : REC_tblQuota
    {
        public string LSCompanyName { get; set; }
        public string LSPositionName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmployeeQuantity")]
        public int EmployeeQuantity { get; set; }
        //public int EmployeeQuantity {
        //    get 
        //    {
        //        EntityDataContext context = new EntityDataContext();
        //        var result = context.HR_tblEmp.Where(p => p.LSPositionID == this.LSPositionID &&
        //                                                p.LSCompanyID == this.LSCompanyID)
        //                                      .Select(p => p.EmpID)
        //                                      .Count();
        //        return result;    
        //    }
        //}

    }
}
