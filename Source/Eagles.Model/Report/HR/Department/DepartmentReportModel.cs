﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.Report.HR.Department
{
    public class DepartmentReportModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSCompanyID")]
        public int LSCompanyID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DepartmentName")]
        public string DepartmentName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Qty")]
        public int? Qty { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Percentage")]
        [DisplayFormat(DataFormatString = "{0:0.0}", ApplyFormatInEditMode = true)]
        public decimal? Percentage { get; set; }
    }
}
