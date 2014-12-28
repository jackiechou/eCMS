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
    public class GradeViewModels : PR_tblSalaryGrade
    {
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SalaryScale")]
        public int? SalaryScaleIDNull { get; set; }

        public string strUsed
        {
            get
            {
                return (Used == true ? "x" : "");
            }
        }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SalaryScale")]
        public string SalaryScale
        {
            get;
            set;

        }
        public string SalaryScaleVN { get; set; }
        public string SalaryScaleEN { get; set; }
    }
}
