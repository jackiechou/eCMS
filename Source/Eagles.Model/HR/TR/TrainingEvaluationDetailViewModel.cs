using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Eagle.Core;
using System.ComponentModel.DataAnnotations;

namespace Eagle.Model
{
    public class TrainingEvaluationDetailViewModel : TR_tblTrainingEvaluationDetail
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingApprisalPartName")]            
        public string TrainingApprisalPartName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingApprisalItemName")]            
        public string TrainingApprisalItemName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingAnswerTypeName")]
        public string TrainingAnswerTypeName { get; set; }

        public bool Checked { get; set; }

        public bool IsMultiAnswers { get; set; }

        public List<TrainingAnswerTypeViewModel> ListOfTrainingAnswer = new List<TrainingAnswerTypeViewModel>();
    }

    public class TrainingEvaluationDetailOnGrid
    {
        public int TrainingEvaluationDetailID { get; set; }
        public int TrainingEvaluationID { get; set; }
        public int? LSTrainingApprisalPartID { get; set; }
        public int? LSTrainingApprisalItemID { get; set; }
        public int LSTrainingAnswerTypeID { get; set; }
        public List<TrainingAnswerTypeOnGrid> ListOfTrainingAnswer { get; set; }
        public bool Checked { get; set; }
    }
    public class TrainingAnswerTypeOnGrid
    {
        public int LSTrainingApprisalItemID { get; set; }
        public int LSTrainingAnswerTypeID { get; set; }
        public bool Checked { get; set; }
    }
}
