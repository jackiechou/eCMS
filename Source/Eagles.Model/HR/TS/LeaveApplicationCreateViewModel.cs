using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace Eagle.Model
{
    public class LeaveApplicationCreateViewModel
    {
        public int LeaveApplicationMasterID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Year")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int Year { get; set; }

        public int EmpID { get; set; }
        public int LSCompanyID { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpCode")]
        public string EmpCode { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FullName")]
        public string FullName { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Status")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int Status { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromDate")]
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> FromDate { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<decimal> BalanceCompare { get; set; }
        public Nullable<decimal> Days { get; set; }
        public Nullable<decimal> LeaveTaken { get; set; }
        public string LeaveTakenView {
            get
            {
                return LeaveTaken.ToString();
            }
        }
        
        public string Creater { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LeaveType")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int LSLeaveDayTypeID { get; set; }
        public string LSLeaveDayTypeName { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm:ss}")]
        public System.DateTime CreateTime { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Comment")]
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
        public string CurrentComment { get; set; }
        public Nullable<bool> isFirstComment { get; set; }
        // trang thai cua process online
        public string s2 { get; set; }
        public string s3 { get; set; }
        public string s4 { get; set; }
        public string s5 { get; set; }
        public string s6 { get; set; }
        public string s7 { get; set; }
        public string s8 { get; set; }
        public string s9 { get; set; }
        public string s10 { get; set; }
        public string s11 { get; set; }

        public string StatusLeaveApplication
        {
            get
            {
                switch (Status)
                {
                    case 0:
                        return Eagle.Resource.LanguageResource.Save;
                    case 1:
                        return Eagle.Resource.LanguageResource.WaittingForApproval;
                    case 2:
                        return s2;
                    case 3:
                        return s3;
                    case 4:
                        return s4;
                    case 5:
                        return s5;
                    case 6:
                        return s6;
                    case 7:
                        return s7;
                    case 8:
                        return s8;
                    case 9:
                        return s9;
                    case 10:
                        return s10;
                    case 11:
                        return s11;
                    default:
                        return "";
                }
            }
        }
    }
}
