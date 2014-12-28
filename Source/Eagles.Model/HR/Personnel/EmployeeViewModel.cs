using Eagle.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Eagle.Model.HR
{
    public class EmployeeEntity
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }

    public enum GenderEntity
    {
        [Display(Name = "Female")]
        Female = 0,
        [Display(Name = "Male")]
        Male = 1,
        [Display(Name = "Others")]
        Unknown = -1
    }    

    public class EmployeeViewModel : IDataErrorInfo
    {
        private string _firstName;
        private string _lastName;
        private Dictionary<String, String> _errors = new Dictionary<string, string>();

        [Key]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpID")]
        public int EmpID { get; set; }

        public int UserID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpCode")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        //[Remote("ValidateEmpCode", "Employee", AdditionalFields = "EmpCode")]
        public string EmpCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FirstName")]
        //[StringLength(15, ErrorMessage = "First Name length Should be less than 50")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    _errors.Add("FirstName", "FirstName cannot be blank!");

                _firstName = value;
            }
        }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastName")]
        // [StringLength(50, ErrorMessage = "Last Name length Should be less than 50")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    _errors.Add("LastName", "LastName cannot be blank!");

                _lastName = value;
            }
        }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BloodType")]
        public string BloodType { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Gender")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int Gender { get; set; }
        public string LSGenderName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Gender")]
        public string GenderName
        {
            get
            {
                switch (Gender)
                {
                    case 0:
                        return Eagle.Resource.LanguageResource.Male;
                    case 1:
                        return Eagle.Resource.LanguageResource.Female;
                    case -1:
                        return Eagle.Resource.LanguageResource.NonSpecified;
                    default:
                        return "";
                }
            }           
        }

        public int CareerStatus { get; set; }
        private string _CareerStatus;
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CareerStatus")]
        public string CurrentCareerStatus
        {
            get
            {
                switch (CareerStatus)
                {
                    case 0:
                        _CareerStatus = Eagle.Resource.LanguageResource.PartTime;
                        break;
                    case 1:
                        _CareerStatus = Eagle.Resource.LanguageResource.FullTime;
                        break;
                    case -1:
                        _CareerStatus = string.Empty;
                        break;
                    default:
                        _CareerStatus = "";
                        break;
                }
                return _CareerStatus;
            }
            set { _CareerStatus = value; }
        }

        private string _Saturation;
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Saturation")]
        public string Saturation
        {
            get
            {
                switch (Gender)
                {
                    case 0:
                        return Eagle.Resource.LanguageResource.Mr;
                    case 1:
                        return Eagle.Resource.LanguageResource.Ms;
                    case -1:
                        return string.Empty;
                    default:
                        return "";
                }

            }
            set { _Saturation = value; }
        }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "EmailValid")]
        //[RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage = "Email is not in proper format")]
        //[StringLength(35, ErrorMessage = "eMail Length Should be less than 35")]    
        //[RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "EmailValid")]
        //[Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string Email { get; set; }

        // [RegularExpression(@"^\d{8,15}$", ErrorMessage = "Vui lòng nhập chính xác \"{0}\"!")]
        //[Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        //[StringLength(15, ErrorMessage = "Phone Number length Should be less than 15")]    
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Telephone")]
        public string Telephone { get; set; }

        //[StringLength(10, MinimumLength=3)]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Mobile")]
        public string Mobile { get; set; }
        // [DataType(DataType.ImageUrl)]

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FileId")]
        public int? FileId { get; set; }

        public string FileUrl { get; set; }

        public HttpPostedFileBase FileUpload { get; set; }

        /*Other Properties*/

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FullName")]
        public string FullName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpName")]
        public string EmpName { get { return LastName + " " + FirstName; } }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastCompanyDefine")]
        public string CompanyName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Department")]
        public string Department { get; set; }//level 1

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Section")]
        public string Section { get; set; }//level2

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Position")]
        public string Position { get; set; }

        /*end*/
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IDNo")]
        public string IDNo { get; set; }


        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IssuedDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> IDIssuedDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IssuedPlace")]
        public Nullable<int> IDIssuedPlace { get; set; }
        public string IDIssuedPlaceName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TaxNo")]
        // [DisplayFormat(DataFormatString = string.Empty, ApplyFormatInEditMode = true)]
        public string TaxNo { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PAddress")]
        //[StringLength(100, ErrorMessage = "Address length Should be less than 100")]
        // [DisplayFormat(DataFormatString = string.Empty, ApplyFormatInEditMode = true)]
        public string PAddress { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PLSCountryID")]
        public Nullable<int> PLSCountryID { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PLSProvinceID")]
        public Nullable<int> PLSProvinceID { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PLSDistrictID")]
        public Nullable<int> PLSDistrictID { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TLSCountryID")]
        public Nullable<int> TLSCountryID { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TLSProvinceID")]
        public Nullable<int> TLSProvinceID { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TLSDistrictID")]
        public Nullable<int> TLSDistrictID { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TAddress"), StringLength(200)]
        public string TAddress { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Company")]
        public int LSCompanyID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Company")]
        public string LSCompanyName { get; set; }

        //[Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Department")]
        //public Nullable<int> LSLevel1ID { get; set; }

        //[Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Department")]
        //public string LSLevel1Name { get; set; }

        //[Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Section")]
        //public Nullable<int> LSLevel2ID { get; set; }

        //[Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Section")]
        //public string LSLevel2Name { get; set; }

        //[AllowedYear(18)]
        //[MinDate(1900, 1, 1)]
        [Remote("ValidateBirthDate", "Validation")]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DOB")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> DOB { get; set; }      

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "YOB")]
        public Nullable<int> YOB { get; set; }


        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "StartDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> StartDate { get; set; }


        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "JoinDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> JoinDate { get; set; }


        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CreatedOnDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> CreatedOnDate { get; set; }

        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastModifiedOnDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> LastModifiedOnDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BornLSCountryID")]
        public Nullable<int> BornLSCountryID { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BornLSProvinceID")]
        public Nullable<int> BornLSProvinceID { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BornLSDistrictID")]
        public Nullable<int> BornLSDistrictID { get; set; }



        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "NativeCountryID")]
        public Nullable<int> NativeCountryID { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "NativeProvinceID")]
        public Nullable<int> NativeProvinceID { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "NativeDistrictID")]
        public Nullable<int> NativeDistrictID { get; set; }
        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSMaritalID")]
        public Nullable<int> LSMaritalID { get; set; }
        public string LSMaritalName { get; set; }
        public string Marital
        {
            get
            {
                switch (LSMaritalID)
                {
                    case 1:
                        return Eagle.Resource.LanguageResource.Single;
                    case 2:
                        return Eagle.Resource.LanguageResource.Marriaged;
                    case 3:
                        return Eagle.Resource.LanguageResource.Divorced;
                    default:
                        return Eagle.Resource.LanguageResource.Single;
                }
            }          
        }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSNationalityID")]
        public Nullable<int> LSNationalityID { get; set; }
        public string LSNationalityName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSEthnicID")]
        public Nullable<int> LSEthnicID { get; set; }

        public string LSEthnicName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSReligionID")]
        public Nullable<int> LSReligionID { get; set; }
        public string LSReligionName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSEducationID")]
        public Nullable<int> LSEducationID { get; set; }

        public string LSEducationName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSQualificationID")]
        public Nullable<int> LSQualificationID { get; set; }

        public string LSQualificationName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSMajorID")]
        public Nullable<int> LSMajorID { get; set; }
        public string LSMajorName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LineManagerID")]
        public Nullable<int> LineManagerID { get; set; }
        public string LineManagerName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSLocationID")]
        public Nullable<int> LSLocationID { get; set; }

        public string LSLocationName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Position")]
        public Nullable<int> LSPositionID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Position")]
        public string LSPositionName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "AccountNumber"), StringLength(50)]
        public string AccountNumber { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "AccountName"), StringLength(100)]
        public string AccountName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSBankID")]
        public Nullable<int> LSBankID { get; set; }

        public string LSBankName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSBankBranchID")]
        public Nullable<int> LSBankBranchID { get; set; }
        public string LSBankBranchName { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SelfDeduction")]
        public Nullable<bool> SelfDeduction { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DependDeduction")]
        public Nullable<bool> DependDeduction { get; set; }

        //[RegularExpression(@"^[0-9]{0,15}$", ErrorMessage = "NoOfDependent should contain only numbers")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "NoOfDependent")]
        [Range(0, 100, ErrorMessage = "Number must be a positive number {0} from {1} to {2}")]
        public Nullable<int> NoOfDependent { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SIBook")]
        public string SIBook { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "HIBook")]
        public string HIBook { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PayByBank")]
        public Nullable<bool> PayByBank { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmergencyContact"), StringLength(100)]
        public string EmergencyContact { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmergencyAddess"), StringLength(100)]
        public string EmergencyAddess { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmergencyPhone"), StringLength(50)]
        public string EmergencyPhone { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmergencyMobile"), StringLength(50)]
        public string EmergencyMobile { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Active")]
        public bool Active { get; set; }

        public string this[string columnName]
        {
            get
            {
                if (_errors.ContainsKey(columnName))
                    return _errors[columnName];

                return String.Empty;
            }
        }

        public string Error
        {
            get { return String.Empty; }
        }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SI")]
        public bool SI { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "HI")]
        public bool HI { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "UI")]
        public bool UI { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "UserMachineID")]
        public string UserMachineID { get; set; }
    }
}
