using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Core;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Eagle.Core.MetaData;

namespace Eagle.Model
{
    public class CandidateQualificationViewModel : REC_tblQualification
    {

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSQualificationID")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int? LSQualificationIDAlowNull { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSQualificationID")]
        public string LSQualificationIDAlowNullName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "QualificationDate")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? QualificationDateAlowNull { get; set; }
        public string QualificationDateAlowNulltmp { get; set; }

        public string LSSchoolName { get; set; }
        public string LSFacultyName { get; set; }
        public string LSMajorName { get; set; }
        public string LSTrainingTypeName { get; set; }
        public string LSCountryName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Priority")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Remote("ValidationCandidatePriority", "Validation", AdditionalFields = "CandidateID,InitialPriorityAlowNull")]
        [Min(1, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "MinErrorMessage")]
        public int? PriorityAlowNull { get; set; }

        public int? InitialPriorityAlowNull { get { return Priority; } }

        
    }
}
