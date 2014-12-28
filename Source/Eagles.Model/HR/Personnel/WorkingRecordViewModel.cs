using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eagle.Model.HR
{
    public class WorkingRecordViewModel
    {
        [Key]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "WorkingID")]
        public int WorkingID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpID")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int EmpID { get; set; }

        public string EmpName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSChangeStatusID")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Remote("ValidationNotEqualZero", "Validation", HttpMethod = "POST", ErrorMessage = "Choose One")]
        public int LSChangeStatusID { get; set; }

        public string LSChangeStatusName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EffectiveDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]  
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public DateTime EffectiveDate { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LineManagerID")]
        public Nullable<int> LineManagerID { get; set; }

        public string LineManagerName { get; set; }        

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSCompanyID")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int LSCompanyID { get; set; }
        public string LSCompanyName { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSLocationID")]
        public Nullable<int> LSLocationID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSLocationName")]
        public string LSLocationName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSPositionID")]
        public Nullable<int> LSPositionID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSPositionName")]
        public string LSPositionName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSGradeID")]
        public Nullable<int> LSGradeID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSGradeName")]
        public string LSGradeName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Decision")]
        public Nullable<bool> Decision { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DecisionNo"), StringLength(15)]
        //[Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]        
        public string DecisionNo { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SignDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public Nullable<DateTime> SignDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SignerEmpID")]
        public Nullable<int> SignerEmpID { get; set; }
        public string SignerEmpName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FileId")]
        public Nullable<int> FileId { get; set; }
        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
        public string Note { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Creater")]
        public Nullable<int> Creater { get; set; }

        public string CreaterName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CreateDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public Nullable<DateTime> CreateDate { get; set; }
    }
}
