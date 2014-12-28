using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eagle.Model.HR
{
    public class ProjectViewModel
    {
        [Key]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ProjectID")]
        public int ProjectID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpID")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int EmpID { get; set; }

        public string EmpName { get; set; }     


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ProjectCode")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [StringLength(50, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "MinLength", MinimumLength = 1)]
        [Remote("ValidateCode", "Project", AdditionalFields = "ProjectCode")] 
        public string ProjectCode { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ProjectName")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required"), StringLength(200)]
        public string ProjectName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromMonth")]
        [DisplayFormat(DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public Nullable<DateTime> FromMonth { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToMonth")]
        [DisplayFormat(DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public Nullable<DateTime> ToMonth { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Position"), StringLength(200)]
        public string Position { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MainWork"), StringLength(200)]
        public string MainWork { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note"), StringLength(200)]
        public string Note { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Creater")]
        public Nullable<int> Creater { get; set; }

        public string CreaterName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CreatedTime")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public Nullable<DateTime> CreatedTime { get; set; }
    }
}
