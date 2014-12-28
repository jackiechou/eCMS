using Eagle.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.HR
{
    public class WorkingHistoryViewModel
    {
        [Key]
        public int WorkingHistoryID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpID")]
        public int EmpID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpName")]
        public string EmpName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "JoinDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> JoinDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> FromDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> ToDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "WorkFor")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [StringLength(200, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "MinLength", MinimumLength = 2)]
        public string WorkFor { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Position")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [StringLength(200, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "MinLength", MinimumLength = 2)]
        public string Position { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Duty")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [StringLength(200, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "MinLength", MinimumLength = 2)]
        public string Duty { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LeaveReason"), StringLength(100)]
        public string LeaveReason { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FormerCompanyAddress"), StringLength(200)]
        public string FormerCompanyAddress { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ContactPersonName"), StringLength(100)]
        public string ContactPersonName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ContactPersonPosition"), StringLength(100)]
        public string ContactPersonPosition { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ContactPersonPhone"), StringLength(50)]
        public string ContactPersonPhone { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ContactPersonEmail"), StringLength(50)]
        [DataType(DataType.EmailAddress, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "EmailValid")]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage = "Email is not in proper format")]
        public string ContactPersonEmail { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note"), StringLength(200)]
        public string Note { get; set; }

        public virtual HR_tblEmp HR_tblEmp { get; set; }
    }
}
