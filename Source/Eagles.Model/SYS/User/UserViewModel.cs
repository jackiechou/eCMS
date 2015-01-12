using Eagle.Model.HR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Eagle.Model.SYS.User
{
    public class UserViewModel
    {
        

        [Key]
        public int UserId { get; set; }
        public System.Guid UserCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "UserName")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Remote("ValidationUserName", "Validation", AdditionalFields = "InitialUserName")]
        [RegularExpression(@"(\S)+", ErrorMessage = "White space is not allowed.")]
        [Editable(true)]
        public string UserName { get; set; }
        public string LoweredUserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public int PasswordFormat { get; set; }


        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Password")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [RegularExpression(@"(\S)+", ErrorMessage = "White space is not allowed.")]
        [StringLength(6, MinimumLength = 3)]
        public string PasswordSalt { get; set; }
        public string Email { get; set; }
        public string PasswordQuestion { get; set; }
        public string PasswordAnswer { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public Nullable<bool> IsAnonymous { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<bool> IsSuperUser { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<bool> IsLockedOut { get; set; }
        public Nullable<bool> UpdatePassword { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromDate")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/yyyy}")]
        //[Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]

        public Nullable<System.DateTime> StartDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EndDate")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/yyyy}")]
        public Nullable<System.DateTime> EndDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "JoinedDate")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/yyyy}")]
        public Nullable<DateTime> JoinedDate { get; set; }

        public string LastIPAddress { get; set; }
        public Nullable<System.DateTime> LastActivityDate { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public Nullable<System.DateTime> LastPasswordChangedDate { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastLockoutDate")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/yyyy}")]
        public Nullable<System.DateTime> LastLockoutDate { get; set; }
        public Nullable<int> FailedPasswordAttemptCount { get; set; }
        public Nullable<System.DateTime> FailedPasswordAttemptWindowStart { get; set; }
        public Nullable<int> FailedPasswordAnswerAttemptCount { get; set; }
        public Nullable<System.DateTime> FailedPasswordAnswerAttemptWindowStart { get; set; }
        public string Comment { get; set; }

        //=====================================================================================
        public int ApplicationId { get; set; }
        public int RoleId { get; set; }        
        public int ScopeTypeId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "RetypePassword")]
        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [RegularExpression(@"(\S)+", ErrorMessage = "White space is not allowed.")]
        [StringLength(6, MinimumLength = 3)]
        [System.Web.Mvc.Compare("PasswordSalt", ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "ValidatePassword")]
        public string RetypePassword { get; set; }
        public string InitialUserName { get { return UserName; } }
        public string FullName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpId")]
        public Nullable<int> EmpId { get; set; }
        public EmployeeViewModel EmployeeInfo { get; set; }
    }
}
