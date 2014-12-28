using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

using Eagle.Core;
using Eagle.Common.Utilities;

namespace Eagle.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class TrainingRequestDurationViewModel : TR_tblTrainingRequestDuration
    {                
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingRequestTotalDays")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public Nullable<int> TotalDaysAllowNull { get; set; }

        public string DurationInfo
        {
            get
            {
                if (this.FromDate.HasValue && this.ToDate.HasValue)
                {
                    return String.Format("{0}-{1}", this.FromDate.Value.ToString("dd/MM/yyyy"), this.ToDate.Value.ToString("dd/MM/yyyy"));
                }
                return String.Empty;
            }
        }

        public string FromDateInfo { get; set; }

        public string ToDateInfo { get; set; }

        public int Key { get; set; }
    }
}
