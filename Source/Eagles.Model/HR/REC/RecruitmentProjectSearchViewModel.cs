using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Core;
using System.ComponentModel.DataAnnotations;


namespace Eagle.Model
{
    public class RecruitmentProjectSearchViewModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
        public string ProjectCode { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Status")]
        public int Status { get; set; }
    }
}
