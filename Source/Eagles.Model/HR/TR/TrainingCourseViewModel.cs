using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using Eagle.Core;

namespace Eagle.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class TrainingCourseViewModel : LS_tblTrainingCourse
    {
        /// <summary>
        /// 
        /// </summary>
        public string InitialTrainingCourse { get { return this.LSTrainingCourseCode; } }

        /// <summary>
        /// 
        /// </summary>
        public string UsedInfo
        {
            get
            {
                return this.Used == true ? "X" : String.Empty;
            }
        }

        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCodeName")]
        public string TrainingCodeName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingCategoryName")]
        public string TrainingCategoryName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingProviderName")]
        public string TrainingProviderName { get; set; }

        public Nullable<int> LSTrainingCodeIDAllowNull { get; set; }
        public Nullable<int> LSTrainingCategoryIDAllowNull { get; set; }
    }
}
