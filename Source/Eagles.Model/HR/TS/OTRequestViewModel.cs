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
    public class OTRequestViewModel : Timesheet_tblOTRecordMaster
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Creater")]
        //public string Creater { get; set; }

        //public int EmpID { get; set; }
        public int LSCompanyID { get; set; }
        public string EmpCode { get; set; }
        public string FullName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "OTLimit")]
        public int? OTLimit { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "OTAccumulated")]
        public decimal OTAccumulated { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Month")]
        public DateTime MonthYear { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Month")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string strMonthYear
        {
            get
            {
                try
                {
                    return MonthYear.ToString("dd/MM/yyyy");
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    DateTime? tmpDate;
                    DateTimeUtils.TryConvertToDate(value, out tmpDate);
                    if (tmpDate != null)
                    {
                        MonthYear = tmpDate.Value;
                    }
                }
                catch
                {

                }
            }
        }


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
