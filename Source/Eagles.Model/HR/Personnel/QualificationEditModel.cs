using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace Eagle.Model.HR
{
    public class QualificationEditModel    
    {
        [Key]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "QualificationID")]
        public int QualificationID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpID")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int EmpID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpName")]
        public string EmpName { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "QualificationNo")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [StringLength(50, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "MinLength", MinimumLength = 1)]       
        public string QualificationNo { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSQualificationID")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int LSQualificationID { get; set; }
        public string LSQualificationName { get; set; }
               

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "QualificationDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public Nullable<DateTime> QualificationDate { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromMonth")]
        [DisplayFormat(DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
      //  [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public Nullable<DateTime> FromMonth { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToMonth")]
        [DisplayFormat(DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
       // [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public Nullable<DateTime> ToMonth { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSSchoolID")]
        public Nullable<int> LSSchoolID { get; set; }

        public string LSSchoolName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "OtherSchool"), StringLength(200)]
        public string OtherSchool { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSFacultyID")]
        public Nullable<int> LSFacultyID { get; set; }

        public string LSFacultyName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "OtherFaculty"), StringLength(200)]
        public string OtherFaculty { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSMajorID")]
        public Nullable<int> MajorID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSMajorName")]
        public string LSMajorName { get; set; }   

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSTrainingTypeID")]
        public Nullable<int> LSTrainingTypeID { get; set; }

        public string LSTrainingTypeName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PayByCompany")]
        public Nullable<bool> PayByCompany { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingPlace")]
        public Nullable<int> TrainingPlace { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSCountryID")]
        public Nullable<int> LSCountryID { get; set; }

        public string LSCountryName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note"), StringLength(200)]
        public string Note { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FileIds")]
        public string FileIds { get; set; }

        public string InitialFileIds { get; set; }

        //[Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Priority")]
        //[Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        //[Remote("ValidationPriority", "Qualification", AdditionalFields = "EmpID,InitialPriority")]
        //public int Priority { get; set; }
        //public int InitialPriority { get { return Priority; } }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Priority")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Range(0, 100, ErrorMessage = "Number must be a positive number {0} from {1} to {2}")]
        public int Priority { get; set; }
        public int InitialPriority { get; set; }
        
    }
}
