using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Eagle.Core;
using System.ComponentModel.DataAnnotations;

namespace Eagle.Model
{
    public class PayrollSummaryViewModel : PR_sprptPayrollSummaryReport_Result
    {

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TypeOfReport")]
        public int TypeOfReport { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "MonthYear")]
        public string MonthYear { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Quarter")]
        public string Quarter { get; set; }
        
        public string SalaryRateInfo
        {
            get
            {
                if (this.ExchangeRateSalary.HasValue)
                {
                    return this.ExchangeRateSalary.Value.ToString("#,###");
                }
                return String.Empty;
            }
        }

        public string SalaryInsuranceRateInfo
        {
            get
            {
                if (this.ExchangeRateInsurance.HasValue)
                {
                    return this.ExchangeRateInsurance.Value.ToString("#,###");
                }
                return String.Empty;
            }
        }
    }
}
