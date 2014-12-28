using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Eagle.Core.MetaData
{
    [MetadataTypeAttribute(typeof(timesheet_sprptTimeKeepingReport_detail_Result.Metadata))]
    public partial class timesheet_sprptTimeKeepingReport_detail_Result
    {
        internal class Metadata
        {            
            public string Seq { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Employee")]
            public string EmployeeName { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastCompanyDefine")]
            public int LSCompanyID { get; set; }

            public string CompanyName { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Position")]
            public Nullable<int> LSPositionID { get; set; }
            public string PositionName { get; set; }
            public System.DateTime DateID { get; set; }
            public string DateInfo { get; set; }
            public int LSShiftID { get; set; }
            public string Shiftname { get; set; }
            public Nullable<System.DateTime> TimeInShift { get; set; }
            public string TimeInInfo { get; set; }
            public Nullable<System.DateTime> TimeOutShift { get; set; }
            public string TimeOutInfo { get; set; }
            public Nullable<System.DateTime> TimeInLate { get; set; }
            public string TimeInLateInfo { get; set; }
            public Nullable<System.DateTime> TimeOutEarly { get; set; }
            public string TimeOutEarlyInfo { get; set; }
        }        
    }
    [MetadataTypeAttribute(typeof(timesheet_sprptTimeKeepingReport_master_Result.Metadata))]
    public partial class timesheet_sprptTimeKeepingReport_master_Result
    {
        internal class Metadata
        {
            public string seq { get; set; }
            public int LSCompanyID { get; set; }
            public string CompanyName { get; set; }
        }        
    }
    [MetadataTypeAttribute(typeof(Timesheet_sprptShiftOfWorkingSchedule_Master_Result.Metadata))]
    public partial class Timesheet_sprptShiftOfWorkingSchedule_Master_Result
    {
        internal class Metadata
        {
            public string Seq { get; set; }
            public Nullable<int> LSCompanyID { get; set; }
            public string CompanyName { get; set; }
        }        
    }
    /// <summary>
    /// 
    /// </summary>
    [MetadataTypeAttribute(typeof(Timesheet_sprptShiftOfWorkingSchedule_Result.Metadata))]
    public partial class Timesheet_sprptShiftOfWorkingSchedule_Result
    {
        internal class Metadata
        {
            public string CompanyName { get; set; }
            public string PostionName { get; set; }
            public string Seq { get; set; }
            public string FullName { get; set; }
            public string StartDateInfo { get; set; }
            public string C01 { get; set; }
            public string C02 { get; set; }
            public string C03 { get; set; }
            public string C04 { get; set; }
            public string C05 { get; set; }
            public string C06 { get; set; }
            public string C07 { get; set; }
            public string C08 { get; set; }
            public string C09 { get; set; }
            public string C10 { get; set; }
            public string C11 { get; set; }
            public string C12 { get; set; }
            public string C13 { get; set; }
            public string C14 { get; set; }
            public string C15 { get; set; }
            public string C16 { get; set; }
            public string C17 { get; set; }
            public string C18 { get; set; }
            public string C19 { get; set; }
            public string C20 { get; set; }
            public string C21 { get; set; }
            public string C22 { get; set; }
            public string C23 { get; set; }
            public string C24 { get; set; }
            public string C25 { get; set; }
            public string C26 { get; set; }
            public string C27 { get; set; }
            public string C28 { get; set; }
            public string C29 { get; set; }
            public string C30 { get; set; }
            public string C31 { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Employee")]
            public int EmpID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastCompanyDefine")]
            public int LSCompanyID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Position")]
            public Nullable<int> LSPositionID { get; set; }
            public Nullable<System.DateTime> StartDate { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Month")]
            public int Month { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Year")]
            public int Year { get; set; }
        }        
    }
    /// <summary>
    /// 
    /// </summary>
    [MetadataTypeAttribute(typeof(Timesheet_sprptOTRecord_Master_Result.Metadata))]
    public partial class Timesheet_sprptOTRecord_Master_Result
    {
        internal class Metadata
        {
            public string Seq { get; set; }
            public Nullable<int> LSCompanyID { get; set; }
            public string CompanyName { get; set; }
            public Nullable<decimal> TotalHours { get; set; }
        }        
    }

    /// <summary>
    /// 
    /// </summary>
    [MetadataTypeAttribute(typeof(Timesheet_sprptOTRecord_Detail_Result.Metadata))]
    public partial class Timesheet_sprptOTRecord_Detail_Result
    {
        internal class Metadata
        {                        
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Employee")]
            public Nullable<int> EmpID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastCompanyDefine")]
            public Nullable<int> LSCompanyID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Position")]
            public Nullable<int> LSPositionID { get; set; }
            public string CompanyName { get; set; }
            public string PositionName { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Month")]
            public int Month { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Year")]
            public int Year { get; set; }
            public string Seq { get; set; }
            public string FullName { get; set; }
            public Nullable<System.DateTime> OTDate { get; set; }
            public string OTDateInfo { get; set; }
            public Nullable<System.DateTime> TimeInAM { get; set; }
            public string TimeInAMInfo { get; set; }
            public Nullable<System.DateTime> TimeOutAM { get; set; }
            public string TimeOutAMInfo { get; set; }
            public Nullable<System.DateTime> TimeInPM { get; set; }
            public string TimeInPMInfo { get; set; }
            public Nullable<System.DateTime> TimeOutPM { get; set; }
            public string TimeOutPMInfo { get; set; }
            public Nullable<decimal> TotalHours { get; set; }
            public Nullable<decimal> NightOT { get; set; }
            public string NightOTInfo { get; set; }
        }        
    }

    /// <summary>
    /// 
    /// </summary>
    [MetadataTypeAttribute(typeof(Timesheet_sprptOTRecord_Result.Metadata))]
    public partial class Timesheet_sprptOTRecord_Result
    {
        internal class Metadata
        {
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Employee")]
            public Nullable<int> EmpID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastCompanyDefine")]
            public Nullable<int> LSCompanyID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Position")]
            public Nullable<int> LSPositionID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Month")]
            public Nullable<int> Month { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Year")]
            public Nullable<int> Year { get; set; }
            public string Seq { get; set; }
            public string FullName { get; set; }
            public Nullable<decimal> TotalHours { get; set; }
            public Nullable<decimal> C01 { get; set; }
            public Nullable<decimal> C02 { get; set; }
            public Nullable<decimal> C03 { get; set; }
            public Nullable<decimal> C04 { get; set; }
            public Nullable<decimal> C05 { get; set; }
            public Nullable<decimal> C06 { get; set; }
            public Nullable<decimal> C07 { get; set; }
            public Nullable<decimal> C08 { get; set; }
            public Nullable<decimal> C09 { get; set; }
            public Nullable<decimal> C10 { get; set; }
            public Nullable<decimal> C11 { get; set; }
            public Nullable<decimal> C12 { get; set; }
            public Nullable<decimal> C13 { get; set; }
            public Nullable<decimal> C14 { get; set; }
            public Nullable<decimal> C15 { get; set; }
            public Nullable<decimal> C16 { get; set; }
            public Nullable<decimal> C17 { get; set; }
            public Nullable<decimal> C18 { get; set; }
            public Nullable<decimal> C19 { get; set; }
            public Nullable<decimal> C20 { get; set; }
            public Nullable<decimal> C21 { get; set; }
            public Nullable<decimal> C22 { get; set; }
            public Nullable<decimal> C23 { get; set; }
            public Nullable<decimal> C24 { get; set; }
            public Nullable<decimal> C25 { get; set; }
            public Nullable<decimal> C26 { get; set; }
            public Nullable<decimal> C27 { get; set; }
            public Nullable<decimal> C28 { get; set; }
            public Nullable<decimal> C29 { get; set; }
            public Nullable<decimal> C30 { get; set; }
            public Nullable<decimal> C31 { get; set; }
        }        
    }

    [MetadataTypeAttribute(typeof(Timesheet_tblCyclicEmp.MetaData))]
    public partial class Timesheet_tblCyclicEmp
    {
        internal sealed class MetaData
        {
            public int ID { get; set; }
            public int CyclicID { get; set; }
            public int EmpID { get; set; }
        }
    }
    [MetadataTypeAttribute(typeof(Timesheet_tblCyclic.MetaData))]
    public partial class Timesheet_tblCyclic
    {
        internal sealed class MetaData
        {
            public int CyclicID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "From")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Range(1, 31, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "From1To31")]
            public int FromCyclic { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "To")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Range(1, 31, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "From1To31")]
            public int ToCyclic { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(Timesheet_tblTimeKeeping_YYYYMM.MetaData))]
    public partial class Timesheet_tblTimeKeeping_YYYYMM
    {
        internal sealed class MetaData
        {
            public int TimekeepingID { get; set; }
            public int EmpID { get; set; }
            public System.DateTime DateID { get; set; }
            public int LSShiftID { get; set; }
            public Nullable<System.DateTime> TimeInShift { get; set; }
            public Nullable<System.DateTime> TimeOutShift { get; set; }
            public Nullable<System.DateTime> TimeIn { get; set; }
            public Nullable<System.DateTime> TimeOut { get; set; }
            public int Type { get; set; }
            public Nullable<System.DateTime> TimeInLate { get; set; }
            public Nullable<System.DateTime> TimeOutEarly { get; set; }
            public Nullable<int> LSLeaveDayTypeID { get; set; }
            public Nullable<decimal> LeaveDays { get; set; }
            public decimal WorkDays { get; set; }
            public string MMYYYY { get; set; }  
        }
    }

    [MetadataTypeAttribute(typeof(Timesheet_tblOTLimitByStaff.MetaData))]
    public partial class Timesheet_tblOTLimitByStaff
    {
        internal sealed class MetaData
        {
            public int OTLimitByStaffID { get; set; }
            public int EmpID { get; set; }
            public int Hours { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(Timesheet_tblMTLChild.MetaData))]
    public partial class Timesheet_tblMTLChild
    {
        internal sealed class MetaData
        {
            public int MLChildID { get; set; }
            public int EmpID { get; set; }
            public System.DateTime FromDate { get; set; }
            public System.DateTime ToDate { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(Timesheet_tblOTRecordDetail.MetaData))]
    public partial class Timesheet_tblOTRecordDetail
    {
        internal sealed class MetaData
        {
            public int OTRecordDetailID { get; set; }
            public int OTRecordID { get; set; }
            public Nullable<System.DateTime> DateID { get; set; }
            public Nullable<System.DateTime> TimeInAM { get; set; }
            public Nullable<System.DateTime> TimeOutAM { get; set; }
            public Nullable<System.DateTime> TimeInPM { get; set; }
            public Nullable<System.DateTime> TimeOutPM { get; set; }
            public Nullable<decimal> TotalHours { get; set; }
            public Nullable<decimal> NightOT { get; set; }
            public Nullable<decimal> HolidayOT { get; set; }
            public Nullable<decimal> Rate100 { get; set; }
            public Nullable<decimal> Rate150 { get; set; }
            public Nullable<decimal> Rate200 { get; set; }
            public Nullable<decimal> Rate300 { get; set; }
            public Nullable<decimal> TOIL { get; set; }
            public Nullable<int> TypeOT { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(Timesheet_tblOTRecordMaster.MetaData))]
    public partial class Timesheet_tblOTRecordMaster
    {
        internal sealed class MetaData
        {
            public int OTRecordID { get; set; }
            public int EmpID { get; set; }
            public int Month { get; set; }
            public int Year { get; set; }
            public decimal TotalHours { get; set; }
            public decimal NightOT { get; set; }
            public decimal HolidaysOT { get; set; }
            public int Status { get; set; }
            public int LevelApprove { get; set; }
            public string Comment { get; set; }
            public string CurrentComment { get; set; }
            public string Creater { get; set; }
            public Nullable<System.DateTime> CreateTime { get; set; }
            public Nullable<bool> isFirstComment { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(LS_tblLeaveType.MetaData))]
    public partial class LS_tblLeaveType
    {
        internal sealed class MetaData
        {
            public int LSLeaveTypeID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSLeaveTypeCode")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string LSLeaveTypeCode { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(Timesheet_tbLeaveDayType.MetaData))]
    public partial class Timesheet_tbLeaveDayType
    {
        internal sealed class MetaData
        {
            public int LSLeaveDayTypeID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string LSLeaveDayTypeCode { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LeaveTypeName")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int LSLeaveTypeID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LeaveName")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string Name { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DaysPerYear")]
            [Range(0, 100, ErrorMessage = "Number must be a positive number {0} from {1} to {2}")]
            public Nullable<int> DaysPerYear { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DaysPerPeriod")]
            public Nullable<int> DaysPerPeriod { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ConvalescenceLeave")]
            public Nullable<decimal> ConvalescenceLeave { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PercentSI")]
            public Nullable<decimal> PercentSI { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ExceptedHolidays")]
            public Nullable<bool> ExceptedHolidays { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }

        }
    }

    [MetadataTypeAttribute(typeof(Timesheet_tblShift.MetaData))]
    public partial class Timesheet_tblShift
    {
        internal sealed class MetaData
        {
            public int LSShiftID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSShiftCode")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string LSShiftCode { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ShiftName")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string ShiftName { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "WorkTimeBegin")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public System.DateTime WorkTimeBegin { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "WorkTimeEnd")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public System.DateTime WorkTimeEnd { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BreakTimeBegin")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public System.DateTime BreakTimeBegin { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BreakTimeEnd")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public System.DateTime BreakTimeEnd { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "isNextDate_OTTimeEnd")]
            public Nullable<bool> isNextDate_OTTimeEnd { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TimeLate")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public System.DateTime TimeLate { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TimeEarly")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public System.DateTime TimeEarly { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "OTNightBegin")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public Nullable<System.DateTime> OTNightBegin { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "OTNightEnd")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public System.DateTime OTNightEnd { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "IsOTNightEnd")]
            public bool IsOTNightEnd { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "WorkHour")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public decimal WorkHour { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
        }
    }

    public partial class Timesheet_tblHolidays
    {
        internal sealed class MetaData
        {
            public System.DateTime DateID { get; set; }
            public int TypeDate { get; set; }
            public string TypeDatestr { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(TimeSheet_tblSchedule.MetaData))]
    public partial class TimeSheet_tblSchedule
    {
        internal sealed class MetaData
        {

            public int ScheduleID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ScheduleCode")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string ScheduleCode { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ScheduleName")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public string ScheduleName { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public bool Used { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(TimeSheet_tblScheduleDetail.MetaData))]
    public partial class TimeSheet_tblScheduleDetail
    {
        internal sealed class MetaData
        {
            public int ScheduleDetailID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ScheduleID")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int ScheduleID { get; set; }


            public System.DateTime DateID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Year")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int Year { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSShiftID")]
            public int? LSShiftID { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(TimeSheet_tblAssignEmpSchedule.MetaData))]
    public partial class TimeSheet_tblAssignEmpSchedule
    {
        internal sealed class MetaData
        {
            public int AssignEmpScheduleID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpName")]
            public int EmpID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Year")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int Year { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Schedule")]
            public int ScheduleID { get; set; }


            public string Creater { get; set; }
            public Nullable<System.DateTime> CreateTime { get; set; }
            public string Editor { get; set; }
            public Nullable<System.DateTime> EditTime { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(Timesheet_tblStandAnualLeaveDays.MetaData))]
    public partial class Timesheet_tblStandAnualLeaveDays
    {
        internal sealed class MetaData
        {
            public int StandardALID { get; set; }
            public int EmpID { get; set; }
            public decimal StandardALDays { get; set; }
        }
    
    }
    [MetadataTypeAttribute(typeof(Timesheet_tblAnnualLeaveAdjust.MetaData))]
    public partial class Timesheet_tblAnnualLeaveAdjust
    {
        internal sealed class MetaData
        {
            public int ALAdjustID { get; set; }


            public int EmpID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Year")]
            public int Year { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Days")]
            public decimal Days { get; set; }


            public System.DateTime AdjustDate { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
        }
    }
    [MetadataTypeAttribute(typeof(Timesheet_tblScheduleChange.MetaData))]
    public partial class Timesheet_tblScheduleChange
    {
        internal sealed class MetaData
        {
            public int ChangeScheduleID { get; set; }
            
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int ScheduleID_To { get; set; }
            
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int ScheduleID_From { get; set; }
            
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public System.DateTime FromDate { get; set; }
            
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public System.DateTime ToDate { get; set; }
            
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int EmpID { get; set; }
            
            public string Note { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(Timesheet_tblLeaveApplicationMaster.MetaData))]
    public partial class Timesheet_tblLeaveApplicationMaster
    {
        internal sealed class MetaData
        {
            public int LeaveApplicationMasterID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Year")]
            public int Year { get; set; }
            
            public int EmpID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "StatusLeaveApplication")]
            public int Status { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Creater")]
            public string Creater { get; set; }

            public Nullable<System.DateTime> FromDate { get; set; }
            public Nullable<System.DateTime> ToDate { get; set; }
            public Nullable<decimal> Days { get; set; }

            public int LSLeaveDayTypeID { get; set; }
            public System.DateTime CreateTime { get; set; }
            public string Comment { get; set; }
            public int LevelApprover { get; set; }
            public Nullable<System.DateTime> DateLevel1 { get; set; }
            public Nullable<int> EmpIDLevel1 { get; set; }
            public string ReasonLevel1 { get; set; }
            public Nullable<System.DateTime> DateLevel2 { get; set; }
            public Nullable<int> EmpIDLevel2 { get; set; }
            public string ReasonLevel2 { get; set; }
            public Nullable<System.DateTime> DateLevel3 { get; set; }
            public Nullable<int> EmpIDLevel3 { get; set; }
            public string ReasonLevel3 { get; set; }
            public Nullable<System.DateTime> DateLevel4 { get; set; }
            public Nullable<int> EmpIDLevel4 { get; set; }
            public string ReasonLevel4 { get; set; }
            public Nullable<System.DateTime> DateLevel5 { get; set; }
            public Nullable<int> EmpIDLevel5 { get; set; }
            public string ReasonLevel5 { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(Timesheet_tblReconciliation.MetaData))]
    public partial class Timesheet_tblReconciliation
    {
        internal sealed class MetaData
        {
            public int ReconciliationID { get; set; }
            public int Year { get; set; }
            public int EmpID { get; set; }
            public Nullable<decimal> ALPrevious { get; set; }
            public Nullable<decimal> ALTakenPrevious { get; set; }
            public Nullable<decimal> ALBalancePrevious { get; set; }
            public Nullable<decimal> LeaveForward { get; set; }
            public Nullable<decimal> LeaveForwardTaken { get; set; }
        }
    }
}
