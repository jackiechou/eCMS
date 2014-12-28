using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using Eagle.Core;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Eagle.Common.Utilities;
namespace Eagle.Model
{
    public class TimekeepingListViewModel : Timesheet_tblTimeKeeping_YYYYMM
    {
        public string FullName { get; set; }
        public string EmpName { get; set; }
        public string EmpCode { get; set; }
        public string CompanyName { get; set; }
        public string PositonName { get; set; }

        public string LeaveType { get;set;}
        public string ShiftCode { get; set; }
        public string strDateID {
            get 
            {
                return String.Format("{0:dd/MM/yyyy}", DateID); 
            }
        }
        public string strTimeInShift
        {
            get 
            {
                return String.Format("{0:HH:mm}", TimeInShift);
            }
        }
        public string strTimeOutShift
        {
            get
            {
                return String.Format("{0:HH:mm}", TimeOutShift);
            }
        }

    }
    public class TimekeepingSearchViewModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmployeeCode")]
        public string EmpCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FullName")]
        public string FullName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Company")]
        public Nullable<int> SearchLSCompanyID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Month")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string MMYYYY { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromDate")]
        public string strFromDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToDate")]
        public string strToDate { get; set; }

        public int TimekeepingType { get; set; }
    }
}
