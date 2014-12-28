using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Eagle.Core;

namespace Eagle.Model
{
    public class EvaluationTemplateDetailViewModel : TR_tblEvaluationTemplateDetail
    {
        public List<TrainingApprisalItemViewModel> ListOfTrainingApprisalItem = new List<TrainingApprisalItemViewModel>();

        public string LSTrainingApprisalPartName {  get; set; }

        public Nullable<bool> Checked { get; set; }
    }
}
