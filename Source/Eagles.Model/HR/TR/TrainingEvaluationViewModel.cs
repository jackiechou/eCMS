using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Eagle.Core;
using System.ComponentModel.DataAnnotations;

namespace Eagle.Model
{
    public class TrainingEvaluationViewModel : TR_tblTrainingEvaluation
    {
        public List<TrainingEvaluationPartViewModel> ListOfTrainingEvaluationPart = new List<TrainingEvaluationPartViewModel>();
        
        public string EvaluationTemplateName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingEvaluationEmployee")]            
        public string EvaluationEmpName { get; set; }

        public string TrainingCodeName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCode")]            
        public string TrainingCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCourse")]            
        public string TrainingCourseName { get; set; }
    }
}
