using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Eagle.Core;
using System.ComponentModel.DataAnnotations;

namespace Eagle.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class TrainingRequestDurationUserViewModel : TR_tblDurationUser
    {
        public string EmployeeName { get; set; }
        public string CompanyName { get; set; }
        public string PositionName { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingRequestTotalDays")]
        public Nullable<int> TotalDays { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingRequestTotalHours")]
        public Nullable<int> TotalHours { get; set; }

        public string DurationInfo 
        {
            get
            {
                return String.Format("{0}-{1}", this.FromDate.ToString("dd/MM/yyyy"), this.ToDate.ToString("dd/MM/yyyy"));
            }    
        }
    }
}
