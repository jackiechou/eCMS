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
    public class PayrollExchangeRateViewModels : PR_tblPayrollExchangeRate
    {

        //[Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string LSCurrencyName { get; set; }

        public string strExchangeRate
        {
            get
            {
                return Convert.ToDecimal(ExchangeRate).ToString("#,##0");
            }
        }

        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EffectiveDate")]
        public System.DateTime? dEffectiveDate { get; set; }

        public string strEffectiveDate
        {
            get
            {
                return String.Format("{0:dd/MM/yyyy}", EffectiveDate);
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    DateTime? dDateID = new DateTime();
                    DateTimeUtils.TryConvertToDate(value, out dDateID);
                    EffectiveDate = dDateID.Value;
                }
            }
        }
    }
}
