using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Eagle.Resource;
using System;

namespace Eagle.Core.MetaData
{
    [MetadataTypeAttribute(typeof(REC_tblCandidate.MetaData))]
    public partial class REC_tblCandidate
    {
        internal sealed class MetaData
        {

            public int CandidateID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CandidateCode")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Remote("ValidationCandidateCode", "Validation", AdditionalFields = "InitialCandidateCode")]
            public string CandidateCode { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FirstName")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string FirstName { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastName")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string LastName { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DOB")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public Nullable<System.DateTime> DOB { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ApplyDate")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public Nullable<System.DateTime> ApplyDate { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Gender")]
            public int Gender { get; set; }

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

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PLSCountryID")]
            public Nullable<int> PLSCountryID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PLSProvinceID")]
            public Nullable<int> PLSProvinceID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PLSDistrictID")]
            public Nullable<int> PLSDistrictID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PAddress")]
            public string PAddress { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TLSCountryID")]
            public Nullable<int> TLSCountryID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TLSProvinceID")]
            public Nullable<int> TLSProvinceID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TLSDistrictID")]
            public Nullable<int> TLSDistrictID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TAddress")]
            public string TAddress { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Email")]
            [DataType(DataType.EmailAddress, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "EmailInValid")]
            [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "EmailInValid")]
            public string Email { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Telephone")]
            public string Telephone { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Mobile")]
            public string Mobile { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FileImage")]
            public int? FileId { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IDNo")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Remote("ValidationNoIDCandidate", "Validation", AdditionalFields = "InitialIDNo")]
            public string IDNo { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IssuedDate")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public Nullable<System.DateTime> IDIssuedDate { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IssuedPlace")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public Nullable<int> IDIssuedPlace { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastCompanyDefine")]
            public Nullable<int> LSCompanyID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSMaritalID")]
            public Nullable<int> LSMaritalID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSNationalityID")]
            public Nullable<int> LSNationalityID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSEthnicID")]
            public Nullable<int> LSEthnicID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSReligionID")]
            public Nullable<int> LSReligionID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSEducationID")]
            public Nullable<int> LSEducationID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSQualificationID")]
            public Nullable<int> LSQualificationID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSMajorID")]
            public Nullable<int> LSMajorID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmergencyContact")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string EmergencyContact { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmergencyAddess")]
            public string EmergencyAddess { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmergencyPhone")]
            public string EmergencyPhone { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmergencyMobile")]
            public string EmergencyMobile { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Result")]
            public Nullable<int> Result { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(REC_tblCandidateTournament.MetaData))]
    public partial class REC_tblCandidateTournament
    {
         internal sealed class MetaData
         {
             public int ProjectTournamentID { get; set; }
             public int CandidateID { get; set; }
             [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Result")]
             [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
             public Nullable<bool> Result { get; set; }
             [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
             public string Note { get; set; }
         }
    }
    [MetadataTypeAttribute(typeof(REC_tblInterviewCalendar.MetaData))]
    public partial class REC_tblInterviewCalendar
    {
        internal sealed class MetaData
        {
            public int InterviewCalendarID { get; set; }
            public int ProjectTournamentID { get; set; }
            public int CandidateID { get; set; }
            public Nullable<System.DateTime> InterviewDate { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "InterviewTime")]
            public Nullable<System.DateTime> InterviewTime { get; set; }
        }

    }
    [MetadataTypeAttribute(typeof(REC_tblInterviewCriteriaType.MetaData))]
    public partial class REC_tblInterviewCriteriaType
    {
        internal sealed class MetaData
        {
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string Code { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string Name { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<int> Rank { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(REC_tblInterviewCriteria.MetaData))]
    public partial class REC_tblInterviewCriteria
    {
        internal sealed class MetaData
        {
            public int InterviewCriteriaID { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "InterviewCriteriaType")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public Nullable<int> InterviewCriteriaTypeID { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string Code { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string Name { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<int> Rank { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(REC_tblPlan.MetaData))]
    public partial class REC_tblPlan
    {
        internal sealed class MetaData
        {
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Year")]
            public int Year { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastCompanyDefine")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int LSCompanyID { get; set; }
            
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Position")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int LSPositionID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PlanQuantity")]
            public int PlanQuantity { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "RecruitedQuantity")]
            public Nullable<int> RecruitedQuantity { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmployeeCreate")]
            public int EmpIDPlan { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CreateDate")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
            public System.DateTime CreateDate { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Comment")]
            public string Comment { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "StatusPlan")]
            public int StatusPlan { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "StatusProcess")]
            public int StatusProcess { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LevelApprove")]
            public int LevelApprove { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DateOfApproval")]
            public Nullable<System.DateTime> DateLevel1 { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpIDLevel1")]
            public Nullable<int> EmpIDLevel1 { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Lv1ApprovalComment")]
            public string ReasonLevel1 { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DateLevel2")]
            public Nullable<System.DateTime> DateLevel2 { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpIDLevel2")]
            public Nullable<int> EmpIDLevel2 { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Lv2ApprovalComment")]
            public string ReasonLevel2 { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DateLevel3")]
            public Nullable<System.DateTime> DateLevel3 { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpIDLevel3")]
            public Nullable<int> EmpIDLevel3 { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Lv3ApprovalComment")]
            public string ReasonLevel3 { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DateLevel4")]
            public Nullable<System.DateTime> DateLevel4 { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpIDLevel4")]
            public Nullable<int> EmpIDLevel4 { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Lv4ApprovalComment")]
            public string ReasonLevel4 { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DateLevel5")]
            public Nullable<System.DateTime> DateLevel5 { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpIDLevel5")]
            public Nullable<int> EmpIDLevel5 { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Lv5ApprovalComment")]
            public string ReasonLevel5 { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(REC_tblProject.MetaData))]
    public partial class REC_tblProject
    {
        public string InitialProjectCode { get { return ProjectCode; } }
        internal sealed class MetaData
        {
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
            [Remote("ValidationProjectCode", "Validation", AdditionalFields = "InitialProjectCode")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string ProjectCode { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ApplyDateFrom")]
            public System.DateTime ApplyDateFrom { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ApplyDateTo")]
            public System.DateTime ApplyDateTo { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "RecruitFrom")]
            public System.DateTime RecruitFrom { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "RecruitTo")]
            public System.DateTime RecruitTo { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Status")]
            public int Status { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PlanCost")]
            [DisplayFormat(DataFormatString = "{0:#,##0}", ApplyFormatInEditMode = false)]
            public Nullable<decimal> PlanCost { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ActualCost")]
            [DisplayFormat(DataFormatString = "{0:#,##0}", ApplyFormatInEditMode = false)]
            public Nullable<decimal> ActualCost { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSCurrencyID")]
            public Nullable<int> LSCurrencyID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Venue")]
            public Nullable<int> LSLocationID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Content")]
            public string Content { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Description")]
            public string Description { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(REC_tblProjectFee.MetaData))]
    public partial class REC_tblProjectFee
    {
        internal sealed class MetaData
        {
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "RecruitmentFee")]
            public int LSRecruitmentFeeID { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Amount")]
            public decimal Amount { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Currency")]
            public int CurrencyID { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(REC_tblProjectTournament.MetaData))]
    public partial class REC_tblProjectTournament
    {
        internal sealed class MetaData
        {
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "RecruitmentTournament")]
            public int LSRecruitmentTournamentID { get; set; }
            //[Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Type")]
            //public int Type { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SEQ")]
            public int SEQ { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromDate")]
            [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}")]
            public System.DateTime FromDate { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToDate")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
            public System.DateTime ToDate { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "InterviewingStaff")]
            public int EmpID { get; set; }
        }
    }
    [MetadataTypeAttribute(typeof(REC_tblQualification.MetaData))]
    public partial class REC_tblQualification
    {
        public string InitialQualificationNo { get { return QualificationNo; } }
        internal sealed class MetaData
        {
            public int QualificationID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Candidate")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int CandidateID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "QualificationNo")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Remote("ValidationCandidateQualificationNo", "Validation", AdditionalFields = "CandidateID,InitialQualificationNo")]
            public string QualificationNo { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSQualificationID")]
            public int LSQualificationID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "QualificationDate")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public System.DateTime QualificationDate { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromMonth")]
            public Nullable<System.DateTime> FromMonth { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToMonth")]
            public Nullable<System.DateTime> ToMonth { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSSchoolID")]
            public Nullable<int> LSSchoolID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "OtherSchool")]
            public string OtherSchool { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSFacultyID")]
            public Nullable<int> LSFacultyID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "OtherFaculty")]
            public string OtherFaculty { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSMajorID")]
            public Nullable<int> LSMajorID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSTrainingTypeID")]
            public Nullable<int> LSTrainingTypeID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Costs")]
            public bool PayByCompany { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingPlace")]
            public Nullable<int> TrainingPlace { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSCountryID")]
            public Nullable<int> LSCountryID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "AttachFile")]
            public string AttachFile { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Priority")]
            public int Priority { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(REC_tblQuota.MetaData))]
    public partial class REC_tblQuota
    {
        internal sealed class MetaData
        {

            public int QuotaID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Year")]
            public int Year { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Company")]
            public int LSCompanyID { get; set; }
                   

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Position")]
            public int LSPositionID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "QuotaQuantity")]
            public int QuotaQuantity { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "RecruitedQuantity")]
            public int RecruitedQuantity { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
            
        }
    }

    [MetadataTypeAttribute(typeof(REC_tblWorkingBackground.MetaData))]
    public partial class REC_tblWorkingBackground
    {
        internal sealed class MetaData
        {

            public int WokingbackgroupID { get; set; }

            public int CandidateID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromMonth")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [DisplayFormat(DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true)]
            public System.DateTime FromMonth { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToMonth")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [DisplayFormat(DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true)]
            public System.DateTime ToMonth { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "WorkFor")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string WorkFor { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Position")]
            public string Position { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Duty")]
            public string Duty { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Address")]
            public string Address { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            public string ContactName { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Position")]
            public string ContactPosition { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Phone")]
            public string Phone { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Email")]
            public string Email { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ChangeReason")]
            public string ChangeReason { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(REC_tblWorkingExpectation.MetaData))]
    public partial class REC_tblWorkingExpectation
    {
        internal sealed class MetaData
        {

            public int ExpectationID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Candidate")]
            public int CandidateID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ExpectationPosition1")]
            public int LSPositionID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ExpectationPosition2")]
            public Nullable<int> LSPositionID_Probation1 { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ExpectationPosition3")]
            public Nullable<int> LSPositionID_Probation2 { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ExpectationSalary")]
            public Nullable<decimal> ExpectationSalary { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MinSalary")]
            public Nullable<decimal> MinSalary { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CurrentSalary")]
            public Nullable<decimal> CurrentSalary { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Currency")]
            public Nullable<int> LSCurrencyID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Workplace")]
            public Nullable<int> LSLocationID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "WorkType")]
            public int WorkType { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "OtherExpectation")]
            public string OtherExpectation { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
        }
    }
    
    [MetadataTypeAttribute(typeof(REC_tblDemand.MetaData))]
    public partial class REC_tblDemand
    {
        public string InitialDemandCode { get { return DemandCode; } }
        internal sealed class MetaData
        {
            [Key]
            public int DemandID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DemandCode")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Remote("ValidationDemandCode", "Validation", AdditionalFields = "InitialDemandCode")]
            public string DemandCode { get; set; }

            public int PlanID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CreateUser")]
            public int EmpID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CreateTime")]
            public System.DateTime CreateTime { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DemandQuantity")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int DemandQuantity { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ActualQuantity")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int ActualQuantity { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Balance")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int Balance { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DemandDate")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public System.DateTime DemandDate { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EffectiveDate")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public System.DateTime EffectiveDate { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "OtherDemand")]
            public string OtherDemand { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Comment")]
            public string Comment { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LevelApprove")]
            public int LevelApprove { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "StatusProcess")]
            public int StatusProcess { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Status")]
            public int StatusDemand { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastCompanyDefine")]
            public Nullable<int> LSCompanyID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSPositionID")]
            public Nullable<int> LSPositionID { get; set; }
 
        }
    }
}
