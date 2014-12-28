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
    public class AnswerTypeViewModel : LS_tblAnswerType
    {
        public List<TrainingAnswerTypeViewModel> ListOfTrainingAnswerType = new List<TrainingAnswerTypeViewModel>();
        public List<TrainingApprisalItemViewModel> ListOfApprisalItem = new List<TrainingApprisalItemViewModel>();

        public string InitialAnswerType { get; set; }

        public AnswerTypeViewModel()
        {
        }

        public string UsedInfo
        {
            get { return this.Used.Value == true ? "X" : String.Empty; }
        }
    }
}
