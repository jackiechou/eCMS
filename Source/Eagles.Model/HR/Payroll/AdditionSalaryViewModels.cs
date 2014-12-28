using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using Eagle.Core;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Eagle.Common.Utilities;
namespace Eagle.Model
{
    public class AdditionSalaryViewModels : PR_tblAdditionSalary
    {
        public string strAmount
        {
            get
            {
                return Convert.ToDecimal(Amount).ToString("#,##0");
            }
        }
        public string strGrossNet
        {
            get
            {
                return isGross == true ? "GROSS" : "NET";
            }
        }
        public Nullable<bool> GROSSNET { get; set; }
        public string LSCurrencyName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Type")]
        public int? LSSalaryAdjustIDNull { get; set; }
        public string LSSalaryAdjustName { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Type")]
        public int? LSSalaryAdjustIDNullSearch { get; set; }
        public string LSSalaryAdjustNameSearch { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmployeeCode")]
        public string EmpCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FullName")]
        public string FullName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Company")]
        public Nullable<int> LSCompanyID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Position")]
        public int? LSPositionIDNull { get; set; }
        public string LSPositionName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Position")]
        public Nullable<int> LSPositionID { get; set; }

        public string sNote { get; set; }

    }
}
