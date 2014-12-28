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
    public class RankViewModels : PR_tblSalaryRank
    {
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SalaryGrade")]
        public int? SalaryGradeIDNull { get; set; }
        
        public string strUsed
        {
            get
            {
                return (Used == true ? "x" : "");
            }
        }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SalaryGrade")]
        public string SalaryGrade { get; set; }
        public string SalaryGradeVN { get; set; }
        public string SalaryGradeEN { get; set; }
    }
}
