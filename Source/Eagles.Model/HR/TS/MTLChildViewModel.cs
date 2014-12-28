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
    public class MTLChildViewModel 
    {

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmployeeCode")]
        public string EmpCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FullName")]
        public string FullName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Company")]
        public Nullable<int> LSCompanyID { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Status")]
        public Nullable<bool> SearchActive1 { get; set; }
    }

    public class MTLChildListViewModel : Timesheet_tblMTLChild
    {
        public int LSCompanyID { get; set; }
        public string EmpCode { get; set; }
        public string FullName { get; set; }
        public string strPositionName { get; set; }
        public System.DateTime? dFromDate { get; set; }
        public string strFromDate
        {
            get
            {
                return String.Format("{0:dd/MM/yyyy}", FromDate);
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    DateTime? dDateID = new DateTime();
                    DateTimeUtils.TryConvertToDate(value, out dDateID);
                    FromDate = dDateID.Value;
                }
            }
        }
        public System.DateTime? dToDate { get; set; }
        public string strToDate
        {
            get
            {
                return String.Format("{0:dd/MM/yyyy}", ToDate);
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    DateTime? dDateID = new DateTime();
                    DateTimeUtils.TryConvertToDate(value, out dDateID);
                    ToDate = dDateID.Value;
                }
            }
        }
        public bool chkCheck { get; set; }
    }
}
