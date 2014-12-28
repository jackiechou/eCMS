using Eagle.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Eagle.Model.HR
{
    public class FamilyRelationshipEntity
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
    public class RelationshipViewModel
    {
        private bool _IsYOB = false;
        private string _Gender = string.Empty;

        [Key]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "RelationshipID")]
        public int RelationshipID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpID")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int EmpID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpName")]
        public string EmpName { get; set; }
        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSRelationshipID")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int LSRelationshipID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSRelationshipName")]
        public string LSRelationshipName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastName")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "MinLength", MinimumLength = 2)]
        public string LastName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FirstName")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "MinLength", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FullName")]
        public string FullName { get { return LastName + " " + FirstName; } }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Gender")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int Gender { get; set; }
        public string strGender
        {
            get
            {
                if (Gender == 0)
                 _Gender = Eagle.Resource.LanguageResource.Male;
                else if (Gender ==1)              
                    _Gender = Eagle.Resource.LanguageResource.Female;
                else
                    _Gender = Eagle.Resource.LanguageResource.NonSpecified;
                return _Gender;
            }
            set
            {
                _Gender = value;
            }
        }

        [Remote("ValidateBirthDate", "Validation")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DOB")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        public Nullable<System.DateTime> DOB { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "YOB")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<int> YOB { get; set; }
        public bool IsYOB
        {
            get
            {
                if (YOB != null && YOB > 0)
                {
                    _IsYOB = true;
                }
                else
                {
                    _IsYOB = false;
                }
                return _IsYOB;
            }
            set
            {
                _IsYOB = value;
            }
        }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IDNo"), StringLength(50)]
        public string IDNo { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Address"), StringLength(200)]
        public string Address { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Telephone"), StringLength(50)]
        public string Telephone { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsDependant")]
        public bool IsDependant { get; set; }

        private string _strIsDependant;
        public string strIsDependant
        {
            get
            {
                if (IsDependant == true)
                {
                    _strIsDependant = "X";
                }
                else
                {
                    _strIsDependant = string.Empty;
                }
                return _strIsDependant;
            }
            set
            {
                _strIsDependant = value;
            }
        }



        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromDatePIT")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FromDatePIT { get; set; }

        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "DateValid")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToDatePIT")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ToDatePIT { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "WokPlace"), StringLength(200)]
        public string WokPlace { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "StillAlive")]
        public bool StillAlive { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Before75"), StringLength(200)]
        public string Before75 { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "After75"), StringLength(200)]
        public string After75 { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TaxNo"), StringLength(50)]
        public string TaxNo { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note"), StringLength(200)]
        public string Note { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Creater")]
        public int? Creater { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CreatedDate")]
        public Nullable<System.DateTime> CreatedDate { get; set; }

        public virtual HR_tblEmp HR_tblEmp { get; set; }
        public virtual LS_tblRelationship LS_tblRelationship { get; set; }
    }

}
