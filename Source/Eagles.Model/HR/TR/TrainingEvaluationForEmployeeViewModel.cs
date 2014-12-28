using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Eagle.Core;
using System.ComponentModel.DataAnnotations;

namespace Eagle.Model
{
    public class TrainingEvaluationForEmployeeViewModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Employee")]            
        public int EmpID { get; set; }
        public string FullName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastCompanyDefine")]            
        public int LSCompanyID { get; set; }
        public string CompanyName { get; set; }

        public List<TrainingRequestViewModel> ListOfTrainingRequest = new List<TrainingRequestViewModel>();
    }
}
