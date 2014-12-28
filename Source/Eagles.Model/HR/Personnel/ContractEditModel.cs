using Eagle.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Eagle.Model.HR
{
    public class ContractEditModel
    {
        [Key]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ContractID")]
        public int ContractID { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ContractNo")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [StringLength(50, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "MinLength", MinimumLength = 1)]
        //[Remote("ValidateCode", "Contract", AdditionalFields = "ContractNo")]
        public string ContractNo { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSContractTypeID")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int LSContractTypeID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSContractTypeName")]
        public string LSContractTypeName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ContractStatus")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int ContractStatus { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ContractStatusName")]
        public string ContractStatusName
        {
            get
            {
                if (ContractStatus == 1)
                {
                    return Eagle.Resource.LanguageResource.Probationary;
                }
                else
                {
                    return Eagle.Resource.LanguageResource.Official;
                }
            }
        }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpID")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int EmpID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpName")]
        public string EmpName { get; set; }
        public string EmpCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SignedEmpID")]
        public Nullable<int> SignedEmpID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SignedDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public Nullable<System.DateTime> SignedDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ProbationFrom")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public Nullable<System.DateTime> ProbationFrom { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ProbationTo")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public Nullable<System.DateTime> ProbationTo { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EffectiveDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public System.DateTime EffectiveDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ExpiredDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public Nullable<System.DateTime> ExpiredDate { get; set; }



        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ProbationSalary")]
        public Nullable<decimal> ProbationSalary { get; set; }
        public string ProbationSalaryEdit { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "InsuranceSalary")]
        public Nullable<decimal> InsuranceSalary { get; set; }
        public string InsuranceSalaryEdit { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSPositionID")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int PositionID { get; set; }
        public string PositionName { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSLocationID")]
        public Nullable<int> LocationID { get; set; }
        public string LocationName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MethodPIT")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int MethodPIT { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PercentPIT")]
        public Nullable<decimal> PercentPIT { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FileIds")]
        public string FileIds { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }

        public int InitialFileIds { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note"), StringLength(200)]
        public string Note { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Creater")]
        public Nullable<int> Creater { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CreatedDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public Nullable<System.DateTime> CreatedDate { get; set; }

        public HttpPostedFileBase FileUpload { get; set; }
        public virtual HR_tblEmp HR_tblEmp { get; set; }
        public virtual LS_tblContractType LS_tblContractType { get; set; }
    }
}
