using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Core.MetaData
{
    [MetadataTypeAttribute(typeof(LS_tblHospital.Metadata))]
    public partial class LS_tblHospital
    {
        internal class Metadata
        {
            public int LSHospitalID { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "HospitalCode")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string LSHospitalCode { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            public string Name { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "VNName")]
            public string VNName { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<short> Rank { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Province")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public Nullable<int> LSProvinceID { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(SI_tblChangingInsuranceInformationMaster.Metadata))]
    public partial class SI_tblChangingInsuranceInformationMaster
    {
        internal class Metadata
        {
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ReportName")]
            public string Name { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Company")]
            public Nullable<int> LSCompanyID { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Month")]
            public Nullable<int> Month { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Year")]
            public Nullable<int> Year { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CreateTime")]
            public Nullable<System.DateTime> CreateTime { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CreateUser")]
            public string CreateUser { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ModifyTime")]
            public Nullable<System.DateTime> ModifyTime { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ModifyUser")]
            public string ModifyUser { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "isNotified")]
            public Nullable<bool> isNotified { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(SI_tblChangingInsuranceInformationDetail.Metadata))]
    public partial class SI_tblChangingInsuranceInformationDetail
    {
        internal class Metadata
        {
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FullName")]
            public string FullName { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SIBook")]
            public string SIBook { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ChangeTypeDetail")]
            public Nullable<int> ChangeTypeID { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Old")]
            public string Old { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "New")]
            public string New { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromMonth")]
            public Nullable<System.DateTime> FromMonth { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToMonth")]
            public Nullable<System.DateTime> ToMonth { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BasedChange")]
            public string BasedChange { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(SI_tblConvalescence.Metadata))]
    public partial class SI_tblConvalescence
    {
        internal class Metadata
        {
            public long ID { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Year")]
            public Nullable<int> YYYY { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Quarter")]
            public Nullable<int> Quarter { get; set; }
            public Nullable<int> EmpID { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Stage")]
            public Nullable<int> Stage { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "NumberOfDaysOff")]
            public Nullable<float> Days { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "StartLeaveDate")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
            public Nullable<System.DateTime> LeaveDate { get; set; }
            
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToDate")]
            [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}")]
            public Nullable<System.DateTime> ToDate { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Concentrate")]
            public Nullable<bool> IsConcentrate { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Percent")]
            public Nullable<int> Percent { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MinSalary")]
            [DisplayFormat(DataFormatString = "{0:#,##0}")]
            public Nullable<decimal> MinSalary { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Amount")]
            [DisplayFormat(DataFormatString = "{0:#,##0}")]
            public Nullable<decimal> Amount { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LeaveType")]
            public Nullable<int> LeaveType { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSLeaveDayTypeID")]
            public Nullable<int> LSLeaveDayTypeID { get; set; }
        }
    }
    [MetadataTypeAttribute(typeof(SI_tblHICard.Metadata))]
    public partial class SI_tblHICard
    {
        internal class Metadata
        {
            public int HICardID { get; set; }
            public int EmpID { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromDate")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
            public DateTime FromDate { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToDate")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
            public DateTime ToDate { get; set; }
            public bool ChangePlace { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Hospital")]
            public Nullable<int> LSHospitalID { get; set; }
            public string Note { get; set; }
        }
    }



    [MetadataTypeAttribute(typeof(SI_tblInsuranceInformation.Metadata))]
    public partial class SI_tblInsuranceInformation
    {
        internal class Metadata
        {
            public int InsuranceID { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Employee")]
            public int EmpID { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SIBook")]
            public string SIBook { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "StartDateSI")]
            public Nullable<DateTime> StartDateSI { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IssuedDateSIBook")]
            public Nullable<DateTime> IssuedDateSIBook { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "JoinDateAtCompany2")]
            public DateTime JoinDateAtCompany { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSProvinceIDIssuedSI")]
            public Nullable<int> LSProvinceIDIssuedSI { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "HIBook")]
            public string HIBook { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSHospitalIDOriginal")]
            public Nullable<int> LSHospitalIDOriginal { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IssuedDateHIBook")]
            public Nullable<DateTime> IssuedDateHIBook { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSProvinceIDIssuedHI")]
            public Nullable<int> LSProvinceIDIssuedHI { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "StartDateHI")]
            public Nullable<DateTime> StartDateHI { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSHospitalIDNew")]
            public Nullable<int> LSHospitalIDNew { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ReturnDateHI")]
            public Nullable<DateTime> ReturnDateHI { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
        }
    }


    [MetadataTypeAttribute(typeof(SI_tblPreviousRecord.Metadata))]
    public partial class SI_tblPreviousRecord
    {
        internal class Metadata
        {
            public int PreviousRecordID { get; set; }
            public int EmpID { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromMonth")]
            [DisplayFormat(DataFormatString = "{0:MM/yyyy}")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public System.DateTime FromMonth { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToMonth")]
            [DisplayFormat(DataFormatString = "{0:MM/yyyy}")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public System.DateTime ToMonth { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "NoOfMonth")]
            [DisplayFormat(DataFormatString = "{0:#,##0}")]
            public Nullable<decimal> NoOfMonth { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Company")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string CompanyName { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Position")]
            public string Position { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ActualSalary")]
            [DisplayFormat(DataFormatString = "{0:#,##0}", ApplyFormatInEditMode = false)]
            public decimal Salary { get; set; }
        }
    }
    [MetadataTypeAttribute(typeof(SI_tblInsuranceLeave.Metadata))]
    public partial class SI_tblInsuranceLeave
    {
        internal class Metadata
        {
            public int ID { get; set; }
            public int EmpID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromDate")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
            public System.DateTime FromDate { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToDate")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
            public System.DateTime ToDate { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LeaveType")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int LSLeaveDayTypeID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "NumOfDay")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [DisplayFormat(DataFormatString = "{0:#,##0.0}")]
            public Nullable<decimal> Total { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsInsNotice")]
            public Nullable<bool> IsInsNotice { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MonthNotice")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string SIMonth { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BabyDOB")]
            public Nullable<System.DateTime> BabyDOB { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsBenefits")]
            public Nullable<bool> IsBenefits { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "AvgSalary")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [DisplayFormat(DataFormatString = "{0:#,##0}")]
            public Nullable<decimal> AvgSalary { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BenifitSalary")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [DisplayFormat(DataFormatString = "{0:#,##0}")]
            public Nullable<decimal> BenifitSalary { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BabyDOD")]
            public Nullable<System.DateTime> BabyDOD { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Stage")]
            public Nullable<int> Stage { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsConvalescence")]
            public Nullable<bool> IsConvalescence { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ConditionEffect")]
            public string ConditionEffect { get; set; }
        }
    }

}
