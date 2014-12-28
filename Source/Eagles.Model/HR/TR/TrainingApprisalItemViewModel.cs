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
    public class TrainingApprisalItemViewModel : LS_tblTrainingApprisalItem
    {
        public string InitialTrainingApprisalItem { get; set; }

        public string AnswerTypeName { get; set; }

        public string TrainingApprisalPartName { get; set; }

        public string UsedInfo
        {
            get
            {
                if (this.Used.HasValue)
                {
                    return this.Used.Value == true ? "X" : String.Empty;
                }
                return String.Empty;
            }
        }

        public string IsMultiAnswerInfo
        {
            get
            {
                if (this.IsMultiAnswer.HasValue)
                {
                    return this.IsMultiAnswer.Value == true ? "X" : String.Empty;
                }
                return String.Empty;
            }
        }
        public int? Item_LSTrainingApprisalPartID 
        {
            get
            {
                if (this.LSTrainingApprisalPartID.HasValue)
                {
                    return this.LSTrainingApprisalPartID.Value;
                }
                return null;
            }
            set
            {
                this.LSTrainingApprisalPartID = value;
            }
        }
        public int Item_LSTrainingApprisalItemID 
        {
            get
            {
                return this.LSTrainingApprisalItemID;
            }
            set
            {
                this.LSTrainingApprisalItemID = value;
            }        
        }

        public int Item_TemplateItemID { get; set; }

        public int? Item_EvaluationTemplateID { get; set; }

        public Nullable<bool> Checked { get; set; }

        public bool IsAllowDelete { get; set; }
    }

    public class TrainingApprisalItemOnGrid
    {
        public int Item_TemplateItemID { get; set; }
        public int? Item_EvaluationTemplateID { get; set; }
        public int? Item_LSTrainingApprisalPartID { get; set; }
        public int? Item_LSTrainingApprisalItemID { get; set; }
        public bool Item_Checked { get; set; }
    }

    public class EvaluationTemplateDetailOnGrid
    {
        public int? EvaluationTemplateID { get; set; }
        public int? LSTrainingApprisalPartID { get; set; }
        public bool Checked { get; set; }
    }    
}
