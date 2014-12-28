using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Eagle.Model
{
    public class AccountCreateModel
    {

        public int UserID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Employee")]
        public string strEmpName { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpCode")]
        public Nullable<int> EmpID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Password")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Confirm")]
        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [System.Web.Mvc.Compare("Password", ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "ValidatePassword")]
        public string RetypePassword { get; set; }

        public string InitialUserName { get { return UserName; } }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "UserName")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Remote("ValidationUserName", "Validation", AdditionalFields = "InitialUserName")]
        public string UserName { get; set; }




        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsLDAP")]
        public bool IsLDAP { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FAdm")]
        public Nullable<bool> FAdm { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FLock")]
        public Nullable<bool> FLock { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromDate")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FromDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromDate")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FromDatetmp { get { return FromDate; } }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToDate")]
        public Nullable<System.DateTime> ToDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LockDate")]
        public Nullable<System.DateTime> LockDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "JoinDate")]
        public Nullable<System.DateTime> JoinDate { get; set; }
        public string Creater { get; set; }
        public string Editer { get; set; }

       
    }
}
