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
    public class InsuranceLeaveViewModel : SI_tblInsuranceLeave
    {
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LeaveType")]
        public int? LSLeaveDayTypeIDAlowNull { get; set; }

        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromDate")]
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

        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToDate")]
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

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BabyDOB")]
        public System.DateTime? dBabyDOB { get; set; }
        public string strBabyDOB
        {
            get
            {
                return String.Format("{0:dd/MM/yyyy}", BabyDOB);
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    DateTime? dDateID = new DateTime();
                    DateTimeUtils.TryConvertToDate(value, out dDateID);
                    BabyDOB = dDateID.Value;
                }
            }
        }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BabyDOD")]
        public System.DateTime? dBabyDOD { get; set; }
        public string strBabyDOD
        {
            get
            {
                return String.Format("{0:dd/MM/yyyy}", BabyDOD);
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    DateTime? dDateID = new DateTime();
                    DateTimeUtils.TryConvertToDate(value, out dDateID);
                    BabyDOD = dDateID.Value;
                }
            }
        }

        public string LSLeaveDayTypeName { get; set; }
    }
}
