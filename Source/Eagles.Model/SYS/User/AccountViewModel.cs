using Eagle.Core;
using Eagle.Model.HR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;



namespace Eagle.Model
{
    public class AccountViewModel
    {
       [Key]
        public int UserID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpId")]
       public int? EmpId { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Password")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [RegularExpression(@"(\S)+", ErrorMessage = "White space is not allowed.")]
        [StringLength(6, MinimumLength = 3)]
        public string Password { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "RetypePassword")]
        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [RegularExpression(@"(\S)+", ErrorMessage = "White space is not allowed.")]
        [StringLength(6, MinimumLength = 3)]
        [System.Web.Mvc.Compare("Password", ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "ValidatePassword")]
        public string RetypePassword { get; set; }

        public string InitialUserName { get { return UserName; } } 

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "UserName")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Remote("ValidationUserName", "Validation", AdditionalFields = "InitialUserName")]        
        [RegularExpression(@"(\S)+", ErrorMessage = "White space is not allowed.")]
        [Editable(true)]
        public string UserName { get; set; }
        public string FullName { get; set; }        

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsLDAP")]
        public int IsLDAP { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FAdm")]
        public Nullable<int> FAdm { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FLock")]
        public Nullable<int> FLock { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "JoinDate")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/yyyy}")]
        public Nullable<DateTime> JoinDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromDate")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/yyyy}")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public Nullable<DateTime> FromDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToDate")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/yyyy}")]
        public Nullable<DateTime> ToDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LockDate")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/yyyy}")]
        public Nullable<DateTime> LockDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpName")]
        public string strEmpName { get { return LastName + " " + FirstName; } }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Creater { get; set; }
        public string Editer { get; set; }
        public string strIsLDAP { get; set; }
        public string strFAdm { get; set; }
        public string strFLock { get; set; }
        public string strFromDate { get; set; }
        public string strToDate { get; set; }
        public string strLockDate { get; set; }
        public string strJoinDate { get; set; }

        public EmployeeViewModel EmployeeInfo { get; set; }
    }
}
