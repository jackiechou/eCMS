using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Eagle.Core;

namespace Eagle.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class TrainingPlanEvaluationResultViewModel : TR_sprptTrainingPlanEvaluation_Result
    {        
        public bool IsEmployeeEvaluation { get; set; }
        public int EvaluationRequiredValue { get; set; }
        public int EmployeeEvaluationValue { get; set; }
    }
}
