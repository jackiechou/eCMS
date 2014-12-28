using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Eagle.Core;

namespace Eagle.Model
{
    public class TrainingEvaluationPartViewModel : TR_tblTrainingEvaluationPart
    {
        public List<TrainingEvaluationDetailViewModel> ListOfTrainingEvaluationDetail = new List<TrainingEvaluationDetailViewModel>();

        public string TrainingApprisalPartName { get; set; }

        public int Part_TrainingEvaluationID 
        {
            get
            {
                return this.TrainingEvaluationID;
            }
            set
            {
                this.TrainingEvaluationID = value;
            }
        }

        public int Part_LSTrainingApprisalPartID
        {
            get
            {
                return this.LSTrainingApprisalPartID;
            }
            set
            {
                this.LSTrainingApprisalPartID = value;
            }
        }
    }

    public class TrainingEvaluationPartOnGrid
    {
        public int? Part_TrainingEvaluationID { get; set; }
        public int? Part_LSTrainingApprisalPartID { get; set; }        
    }
}
