using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Eagle.Core;
using System.ComponentModel.DataAnnotations;

namespace Eagle.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class PayrollReportViewModel : PR_vPayRoll
    {
        //----------------------------------- For Payslip summary-------------------------------------//
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmployeeCode")]
        public string EmployeeCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TrainingPlanMonthYear")]      
        public string MonthYear { get; set; }

        //----------------------------------- Common ------------------------------------------------//
        //[Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Employee")]
        //public Nullable<int> EmpID { get; set; }

        public string EmployeeName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Company")]
        public Nullable<int> LSCompanyID { get; set; }

        public string CompanyName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Position")]
        public Nullable<int> LSPositionID { get; set; }
        public string PositionName { get; set; }
        public string CurrencyName { get; set; }
        public string StandandWorkInfo
        {
            get
            {
                return this.StandardWork.ToString("#,###.0");
            }
        }
        public string ActualWorkInfo
        {
            get
            {
                return this.ActualWork.ToString("#,###.0");
            }
        }
        //-----------------------------------Employee Income------------------------------------------//
        public decimal? TaxIncome
        {
            get
            {
                if (this.Salary.HasValue && this.AdditionSalaryBeforTax.HasValue && this.OT.HasValue && this.OT_NoneTax.HasValue && this.OtherTaxable.HasValue)
                {
                    return this.Salary.Value + this.AdditionSalaryBeforTax.Value + (this.OT.Value - this.OT_NoneTax.Value) + this.OtherTaxable.Value;                    
                }
                return null;
            }

        }
        public string TaxIncomeInfo
        {
            get
            {
                if (this.TaxIncome.HasValue)
                {
                    return this.TaxIncome.Value.ToString("#,###");
                }
                return String.Empty;
            }
        }
        public decimal? NoneTaxableIncome
        {
            get
            {
                if (this.OT_NoneTax.HasValue && this.AdditionSalaryAfterTax.HasValue && this.OtherNoneTaxable.HasValue)
                {
                    return this.OT_NoneTax.Value + this.AdditionSalaryAfterTax.Value + this.OtherNoneTaxable.Value;                    
                }
                return null;
            }
        }
        public string NoneTaxableIncomeInfo
        {
            get
            {
                if (this.NoneTaxableIncome.HasValue)
                {
                    return this.NoneTaxableIncome.Value.ToString("#,###");
                }
                return String.Empty;
            }
        }
        public string SalaryInfo
        {
            get
            {
                if (this.Salary.HasValue)
                {
                    return this.Salary.Value.ToString("#,###");
                }
                return String.Empty;
            }
        }
        public string OTInfo
        {
            get
            {
                if (this.OT.HasValue)
                {
                    return this.OT.Value.ToString("#,###");
                }
                return String.Empty;
            }
        }
        public string OTNoneTaxInfo
        {
            get
            {
                if (this.OT_NoneTax.HasValue)
                {
                    return this.OT_NoneTax.Value.ToString("#,###");
                }
                return String.Empty;
            }
        }
        public string OTInComeInfo
        {
            get
            {
                if (this.OT.HasValue && this.OT_NoneTax.HasValue)
                {
                    var result = OT.Value - OT_NoneTax.Value;
                    return result.ToString("#,###");
                }
                return String.Empty;
            }
        }
        public string AdditionSalaryBeforeTaxInfo
        {
            get
            {
                if (this.AdditionSalaryBeforTax.HasValue)
                {
                    return this.AdditionSalaryBeforTax.Value.ToString("#,###");
                }
                return String.Empty;                
            }
        }
        public string AdditionSalaryAfterTaxInfo
        {
            get
            {
                if (this.AdditionSalaryAfterTax.HasValue)
                {
                    return this.AdditionSalaryAfterTax.Value.ToString("#,###");
                }
                return String.Empty;
            }
        }
        public decimal? OtherTaxable
        {
            get
            {
                return 0;
            }
            set
            {
                this.OtherTaxable = value;
            }
            
        }
        public string OtherTaxableInfo
        {
            get
            {
                if (this.OtherTaxable.HasValue)
                {
                    return this.OtherTaxable.Value.ToString("#,###");
                }
                return String.Empty;
            }
        }
        public decimal? OtherNoneTaxable
        {
            get
            {
                return 0;
            }
            set
            {
                this.OtherNoneTaxable = value;
            }
        }
        public string OtherNoneTaxableInfo
        {
            get
            {
                if (this.OtherNoneTaxable.HasValue)
                {
                    return this.OtherNoneTaxable.Value.ToString("#,###");
                }
                return String.Empty;
            }
        }
        //-----------------------------------Taxable Income------------------------------------------//
        public string PersonalReliefInfo
        {
            get
            {
                if (this.SelfDeduction.HasValue)
                {
                    return this.SelfDeduction.Value.ToString("#,###");
                }
                return String.Empty;
            }
        }
        public string DependantReliefInfo
        {
            get
            {
                if (this.Dependent.HasValue && this.DependentDeduction.HasValue)
                {
                    var result = this.Dependent.Value * this.DependentDeduction.Value;
                    return result.ToString("#,###");
                }
                return String.Empty;
            }
        }
        public string EmploymentIncomeInfo
        {
            get
            {
                return String.Empty;
            }
        }
        //------------------------------------- PIT ------------------------------------------------//
        public string PITInfo
        {
            get
            {
                if (this.IncomeTax.HasValue)
                {
                    return this.IncomeTax.Value.ToString("#,###");
                }
                return String.Empty;
            }
        }
        //----------------------------------Net Take Home ------------------------------------------//
        public string NetTakeHomeInfo
        {
            get
            {
                if (this.NetTakeHome.HasValue)
                {
                    return this.NetTakeHome.Value.ToString("#,###");
                }
                return String.Empty;
            }
        }
        //---------------------------------- Deduction -------------------------------------------//
        public string SocialInsuranceInfo
        {
            get
            {
                if (this.InS.HasValue)
                {
                    return this.InS.Value.ToString("#,###");
                }
                return String.Empty;
            }
        }
        public string HealthInsuranceInfo
        {
            get
            {
                if (this.InH.HasValue)
                {
                    return this.InH.Value.ToString("#,###");
                }
                return String.Empty;
            }
        }
        public string UnEmployeeInsuranceInfo
        {
            get
            {
                if (this.InE.HasValue)
                {
                    return this.InE.Value.ToString("#,###");
                }
                return String.Empty;
            }
        }
    }
}
