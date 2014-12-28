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
    public class TrainingAnswerTypeViewModel : LS_tblTrainingAnswerType
    {
        public string InitialTrainingAnswerTypeCode { get; set; }

        public string AnswerTypeName { get; set; }

        public string UsedInfo
        {
            get
            {
                return this.Used.HasValue == true ? "X" : String.Empty;
            }
        }

        public bool IsAnswer { get; set; }

        public bool Checked { get; set; }

        public int TrainingEvaluationDetailID { get; set; }
    }
}
