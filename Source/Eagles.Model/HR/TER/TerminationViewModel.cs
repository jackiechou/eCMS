using Eagle.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.TER
{
    public class TerminationViewModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TerminationID")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int TerminationID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpID")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int EmpID { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpCode")]
        public string EmpCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FullName")]

        public string FullName { get; set; }

        public int? Gender { get; set; }
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
        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "EmailValid")]
        public string Email { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Telephone")]
        public string Telephone { get; set; }

        //[StringLength(10, MinimumLength=3)]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Mobile")]
        public string Mobile { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Position")]
        public string Position { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSTerminationReasonID")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int LSTerminationReasonID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSTerminationReasonName")]
        public string LSTerminationReasonName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSTerminationTypeID")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int LSTerminationTypeID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSTerminationTypeName")]
        public string LSTerminationTypeName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Description")]
        public string Description { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "JoinedDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> JoinedDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "InformedDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public System.DateTime InformedDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastWorkingDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public System.DateTime LastWorkingDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EffectiveDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public System.DateTime EffectiveDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsTerminationPaid")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public bool IsTerminationPaid { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ReturnDateForInsuranceCard")]
        public Nullable<System.DateTime> ReturnDateForInsuranceCard { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MonthsOfUnPaidInsuranceCard")]
        public Nullable<int> MonthsOfUnPaidInsuranceCard { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DecisionNo")]
        public string DecisionNo { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SignDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public Nullable<System.DateTime> SignDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpIDSigner")]
        public Nullable<int> EmpIDSigner { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FileIds")]
        public string FileIds { get; set; }

        //show employee avatar or photo
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FileId")]
        public int? FileId { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Company")]
        public int LSCompanyID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Company")]
        public string LSCompanyName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Department")]
        public string Department { get; set; }//level 1

        public virtual HR_tblEmp HR_tblEmp { get; set; }

        public virtual LS_tblTerminationReason LS_tblTerminationReason { get; set; }

        public virtual LS_tblTerminationType LS_tblTerminationType { get; set; }
    }
}
