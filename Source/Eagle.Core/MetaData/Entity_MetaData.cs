using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Eagle.Resource;

namespace Eagle.Core.MetaData
{
    
    #region SYSTEM module

    [MetadataTypeAttribute(typeof(SYS_tblUserAccount.MetaData))]
    public partial class SYS_tblUserAccount
    {
        internal sealed class MetaData
        {
            public int UserID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpID")]
            public Nullable<int> EmpID{ get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Password")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            //[Required(ErrorMessage="vui jgjg {0}")]
            public string Password{ get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "UserName")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string UserName{ get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsLDAP")]
            public int IsLDAP{ get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FAdm")]
            public Nullable<int> FAdm { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FLock")]
            public Nullable<int> FLock { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromDate")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> FromDate{ get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToDate")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> ToDate{ get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LockDate")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> LockDate{ get; set; }

            public string Creater{ get; set; }
            public string Editer{ get; set; }
            
        }
    }

    [MetadataTypeAttribute(typeof(SYS_tblGroup.MetaData))]
    public partial class SYS_tblGroup
    {
        internal sealed class MetaData
        {
            public int GroupID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "GroupCode")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string GroupCode { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "GroupName")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string GroupName { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "GroupName")]
            public string Note { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }

        }
    }

    [MetadataTypeAttribute(typeof(SYS_tblOnlineProcessEmp.MetaData))]
    public partial class SYS_tblOnlineProcessEmp
    {
        internal sealed class MetaData
        {
            [Key]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "OnlineProcessEmpID")]
            public int OnlineProcessEmpID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "OnlineProcessID")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int OnlineProcessID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Company")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int LSCompanyID { get; set; }
    
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Employee")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int EmpID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ApproveLevel1")]
            public Nullable<bool> ApproveLevel1 { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ApproveLevel2")]
            public Nullable<bool> ApproveLevel2 { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ApproveLevel3")]
            public Nullable<bool> ApproveLevel3 { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ApproveLevel4")]
            public Nullable<bool> ApproveLevel4 { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ApproveLevel5")]
            public Nullable<bool> ApproveLevel5 { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(SYS_tblUserGroup.MetaData))]
    public partial class SYS_tblUserGroup
    {
        internal sealed class MetaData
        {
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "UserID")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int UserID { get; set; }

            [Key]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "GroupID")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int GroupID { get; set; }
            
            public string Creater { get; set; }
            public Nullable<System.DateTime> CreateTime { get; set; }
        }
    }
    [MetadataTypeAttribute(typeof(SYS_tblDataPermission.MetaData))]
    public partial class SYS_tblDataPermission
    {
        internal sealed class MetaData
        {

            [Key]
            public int DataPermissionID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Group")]
            public Nullable<int> GroupID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Module")]
            public Nullable<int> ModuleID{ get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Company")]
            public Nullable<int> LSCompanyID{ get; set; }
        }
    }


    [MetadataTypeAttribute(typeof(SYS_tblDelegate.MetaData))]
    public partial class SYS_tblDelegate
    {
        internal sealed class MetaData
        {
            [Key]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DelegateID")]
            public int DelegateID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Account")]
            public string Account { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Account_delegate")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string Account_delegate { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Process")]
            public int DMQuiTrinhID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromDate")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public System.DateTime FromDate { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToDate")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public System.DateTime ToDate { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Creater")]
            public string Creater { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CreateTime")]
            public Nullable<System.DateTime> CreateTime { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Editer")]
            public string Editer { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EditTime")]
            public Nullable<System.DateTime> EditTime { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }

        }
    }


    [MetadataTypeAttribute(typeof(SYS_tblFunctionList.MetaData))]
    public partial class SYS_tblFunctionList
    {
        public string FunctionName { get; set; }

        internal sealed class MetaData
        {
            [Key]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FunctionModel_FunctionID")]
            public int FunctionID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FunctionModel_FunctionName")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string FunctionName { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<int> Rank { get; set; }


            public Nullable<int> Parent { get; set; }
            public string FunctionNameV { get; set; }
            public string FunctionNameE { get; set; }
            public string ModuleID { get; set; }
            public string Url { get; set; }
            public string Ascx { get; set; }
            public Nullable<bool> Display { get; set; }
            public string Left_Horizontal { get; set; }
            public string cssClass { get; set; }
            public string TitleV { get; set; }
            public string TitleE { get; set; }
            public Nullable<bool> HaveSetup { get; set; }
            public Nullable<int> FunctionDefault { get; set; }
            public int MenuLevel { get; set; }
            public Nullable<int> LevelParent { get; set; }
            public string LookId { get; set; }
            public string AscxParent { get; set; }
            public string Tooltips { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(SYS_tblFunctionPermission.MetaData))]
    public partial class SYS_tblFunctionPermission
    {
        internal sealed class MetaData
        {
            public int PermissionID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Group")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int GroupID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Module")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int ModuleID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Function")]
            public Nullable<int> FunctionID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Function")]
            public Nullable<int> MasterDataID { get; set; }

            
            public Nullable<bool> FView { get; set; }
            public Nullable<bool> FEdit { get; set; }
            public Nullable<bool> FDelete { get; set; }
            public Nullable<bool> FExport { get; set; }
        }
    }
    #endregion

    #region System Parameter

    [MetadataTypeAttribute(typeof(SYS_tblParameter.MetaData))]
    public partial class SYS_tblParameter
    {

        internal sealed class MetaData
        {
            [Key]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ParameterID")]
            public int ParameterID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CutOffSI")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int CutOffSI { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TypeOfSalary")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int TypeOfSalary { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "WorkingCalendar")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int LeaveType { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DateExpireLeaveForward")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public System.DateTime DateExpireLeaveForward { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "OTLimit")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int OTLimit { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "StandardWorking")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int StandardWorking { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "NightOTFrom")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public Nullable<System.DateTime> NightOTFrom { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "NightOTTo")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public Nullable<System.DateTime> NightOTTo { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(SYS_tblPayrollParameter.MetaData))]
    public partial class SYS_tblPayrollParameter
    {
        internal sealed class MetaData
        {
            [Key]
            public int ID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EffectiveDate")]
            [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}", ApplyFormatInEditMode=true)]
            public System.DateTime EffectiveDate { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CoefInsS_E")]
            public Nullable<decimal> CoefInsS_E { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CoefInsS_C")]
            public Nullable<decimal> CoefInsS_C { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CoefInsH_E")]
            public Nullable<decimal> CoefInsH_E { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CoefInsH_C")]
            public Nullable<decimal> CoefInsH_C { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CoefInsE_E")]
            public Nullable<decimal> CoefInsE_E { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CoefInsE_C")]
            public Nullable<decimal> CoefInsE_C { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MinSalary")]
            [DisplayFormat(DataFormatString="{0:###.##}")]
            public Nullable<decimal> MinSalary { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CoefOver")]
            public Nullable<int> CoefOver { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SelfDeduction")]
            [DisplayFormat(DataFormatString = "{0:###.##}")]
            public Nullable<decimal> SelfDeduction { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DependDeduction")]
            [DisplayFormat(DataFormatString = "{0:###.##}")]
            public decimal DependDeduction { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Description")]
            public string Description { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(SYS_tblProcessOnlineMaster.MetaData))]
    public partial class SYS_tblProcessOnlineMaster
    {
        internal sealed class MetaData
        {
            [Key]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "OnlineProcessID")]
            public int OnlineProcessID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DMQuiTrinhID")]
            public int DMQuiTrinhID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "StatusLevel1A")]
            public string StatusLevel1A { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "StatusLevel1U")]
            public string StatusLevel1U { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "StatusLevel2A")]
            public string StatusLevel2A { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "StatusLevel2U")]
            public string StatusLevel2U { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "StatusLevel3A")]
            public string StatusLevel3A { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "StatusLevel3U")]
            public string StatusLevel3U { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "StatusLevel4A")]
            public string StatusLevel4A { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "StatusLevel4U")]
            public string StatusLevel4U { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "StatusLevel5A")]
            public string StatusLevel5A { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "StatusLevel5U")]
            public string StatusLevel5U { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Description")]
            public string Description { get; set; }
        }
    }




    #endregion
    #region Master Data
    [MetadataTypeAttribute(typeof(LS_tblRecruitmentSource.MetaData))]
    public partial class LS_tblRecruitmentSource
    {
        public string ValidLSRecruitmentSourceCode { get { return LSRecruitmentSourceCode; } }
        internal sealed class MetaData
        {

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string LSRecruitmentSourceCode { get; set; }
            
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<short> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }
        }
     }
    [MetadataTypeAttribute(typeof(LS_tblRecruitmentFee.MetaData))]
    public partial class LS_tblRecruitmentFee
    {
        public string ValidLSRecruitmentFeeCode { get { return LSRecruitmentFeeCode; } }
        internal sealed class MetaData
        {

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string LSRecruitmentFeeCode { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<short> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }
        }
    }
    [MetadataTypeAttribute(typeof(LS_tblRecruitmentTournament.MetaData))]
    public partial class LS_tblRecruitmentTournament
    {
        public string ValidLSRecruitmentTournamentCode { get { return LSRecruitmentTournamentCode; } }
        internal sealed class MetaData
        {

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string LSRecruitmentTournamentCode { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<short> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }
        }
    }
    [MetadataTypeAttribute(typeof(LS_tblRecruitmentType.MetaData))]
    public partial class LS_tblRecruitmentType
    {
        public string ValidLSRecruitmentTypeCode { get { return LSRecruitmentTypeCode; } }
        internal sealed class MetaData
        {

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string LSRecruitmentTypeCode { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<short> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(LS_tblCompany.MetaData))]
    public partial class LS_tblCompany
    {
        public string InitialLSCompanyCode { get { return LSCompanyCode; } }
        internal sealed class MetaData
        {


            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "belong")]
            public Nullable<int> Parent { get; set; }


            public int LSCompanyID { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Remote("ValidationLSCompanyCode", "Validation", AdditionalFields = "InitialLSCompanyCode")]
            public string LSCompanyCode { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Type")]
            public int LSCompanyDefineID { get; set; }
             

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "VNName")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string VNName { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Address")]
            public string Address { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Phone")]
            public string Phone { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Fax")]
            public string Fax { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<int> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TaxCode")]
            public string TaxCode { get; set; }

            public string Creater { get; set; }
            public Nullable<System.DateTime> CreateTime { get; set; }
            public string Editor { get; set; }
            public Nullable<System.DateTime> EditTime { get; set; }
        }
    }





    [MetadataTypeAttribute(typeof(LS_tblPosition.MetaData))]
    public partial class LS_tblPosition
    {
        internal sealed class MetaData
        {
            public int LSPositionID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSPositionCode")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string LSPositionCode { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "VNName")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string VNName { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<int> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
        }

    }

    [MetadataTypeAttribute(typeof(LS_tblLocation.MetaData))]
    public partial class LS_tblLocation
    {
        internal sealed class MetaData
        {
            public int LSLocationID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSLocationCode")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string LSLocationCode { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "VNName")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string VNName { get; set; }
            
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<int> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
        }
    }

    public partial class LS_tblMasterData
    {
        internal sealed class MetaData
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string VNName { get; set; }
            public string Module { get; set; }
            public string TableName { get; set; }
            public Nullable<int> Rank { get; set; }
            public string Note { get; set; }
            public Nullable<int> Display { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(LS_tblCountry.MetaData))]
    public partial class LS_tblCountry
    {
        internal sealed class MetaData
        {
            
            public int LSCountryID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSCountryCode")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string LSCountryCode { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "VNName")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string VNName { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<short> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(LS_tblProvince.MetaData))]
    public partial class LS_tblProvince
    {
        internal sealed class MetaData
        {

            public int LSProvinceID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string LSProvinceCode { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "VNName")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string VNName { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<short> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Country")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public Nullable<int> LSCountryID { get; set; }

        }
    }

    [MetadataTypeAttribute(typeof(LS_tblDistrict.MetaData))]
    public partial class LS_tblDistrict
    {
        internal sealed class MetaData
        {
            public int LSDistrictID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string LSDistrictCode { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Province")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int LSProvinceID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "VNName")]
            public string VNName { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<short> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }
            
        }
    }

    [MetadataTypeAttribute(typeof(LS_tblSchool.MetaData))]
    public partial class LS_tblSchool
    {
        internal sealed class MetaData
        {
            public int LSSchoolID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSSchoolCode")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string LSSchoolCode { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "VNName")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string VNName { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<short> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }
        }
    }
    
    [MetadataTypeAttribute(typeof(LS_tblMarital.MetaData))]
    public partial class LS_tblMarital
    {
        internal sealed class MetaData
        {
            public int LSMaritalID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string LSMaritalCode { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "VNName")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string VNName { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<short> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(LS_tblFaculty.MetaData))]
    public partial class LS_tblFaculty
    {
        internal sealed class MetaData
        {
            public int LSFacultyID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string LSFacultyCode { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "VNName")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string VNName { get; set; }


            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<short> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(LS_tblMajor.MetaData))]
    public partial class LS_tblMajor
    {
        internal sealed class MetaData
        {
            public int LSMajorID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string LSMajorCode { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "VNName")]
            public string VNName { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<short> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(LS_tblNationality.MetaData))]
    public partial class LS_tblNationality
    {
        internal sealed class MetaData
        {
            public int LSNationalityID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string LSNationalityCode { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "VNName")]
            public string VNName { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<short> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(LS_tblOnlineProcess.MetaData))]
    public partial class LS_tblOnlineProcess
    {
        internal sealed class MetaData
        {
            [Key]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DMQuiTrinhID")]
            public int DMQuiTrinhID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "NameProcessOnline")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string NameProcessOnline { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Function")]
            public int FunctionID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "NoOfLevel")]
            public int NoOfLevel { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsStart")]
            public bool IsStart { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Description")]
            public string Description { get; set; }
        }
    }


    [MetadataTypeAttribute(typeof(LS_tblEthnic.MetaData))]
    public partial class LS_tblEthnic
    {
        internal sealed class MetaData
        {
            public int LSEthnicID { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string LSEthnicCode { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "VNName")]
            public string VNName { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<short> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(LS_tblReligion.MetaData))]
    public partial class LS_tblReligion
    {
        internal sealed class MetaData
        {
            public int LSReligionID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string LSReligionCode { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "VNName")]
            public string VNName { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<short> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(LS_tblEducation.MetaData))]
    public partial class LS_tblEducation
    {
        internal sealed class MetaData
        {
            public int LSEducationID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string LSEducationCode { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "VNName")]
            public string VNName { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<short> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(LS_tblQualification.MetaData))]
    public partial class LS_tblQualification
    {
        internal sealed class MetaData
        {
            public int LSQualificationID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string LSQualificationCode { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "VNName")]
            public string VNName { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<short> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }
        }
    }


    [MetadataTypeAttribute(typeof(LS_tblBank.MetaData))]
    public partial class LS_tblBank
    {
        internal sealed class MetaData
        {
            public int LSBankID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string LSBankCode { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
            public string VNName { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<short> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Province")]
            public Nullable<int> LSProvinceID { get; set; }
        }
    }
#endregion
}
