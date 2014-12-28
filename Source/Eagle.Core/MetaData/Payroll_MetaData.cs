using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Eagle.Core.MetaData
{
    [MetadataTypeAttribute(typeof(PR_sprpt_02KKTNCN_Result.Metadata))]
    public partial class PR_sprpt_02KKTNCN_Result
    {
        public class Metadata
        {
            public string NumOfEmployee { get; set; }
            public Nullable<int> NumberOfEmployeeHasContract { get; set; }
            public Nullable<int> C23 { get; set; }
            public Nullable<int> NumOfEmployeeAddress { get; set; }
            public int NumOfEmployeeNoAddress { get; set; }
            public string C26 { get; set; }
            public string SumOfTotalIncomeAddressHasContract { get; set; }
            public string SumOfTotalIncomeAddressNoHasContract { get; set; }
            public int SumOfTotalIncomeNoAddress { get; set; }
            public string C30 { get; set; }
            public string SumOfTotalIncomeTaxAddressHasContract { get; set; }
            public string SumOfTotalIncomeTaxAddressNoHasContract { get; set; }
            public int SumOfTotalIncomeTaxNoAddress { get; set; }
            public string C34 { get; set; }
            public string SumOfIncomeTaxAddressHasContract { get; set; }
            public string SumOfIncomeTaxAddressNoHasContract { get; set; }
            public int SumOfIncomeTaxNoAddress { get; set; }
        }        
    }
    [MetadataTypeAttribute(typeof(PR_sprptPayrollSummaryReport_Master_Result.Metadata))]
    public partial class PR_sprptPayrollSummaryReport_Master_Result
    {
        internal class Metadata
        {
            public int LSCompanyID { get; set; }
            public string Name { get; set; }
        }        
    }
    [MetadataTypeAttribute(typeof(PR_sprptPayrollSummaryReport_Result.Metadata))]
    public partial class PR_sprptPayrollSummaryReport_Result
    {
        internal class Metadata
        {            
            public int PRID { get; set; }
            public Nullable<int> Seq { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastCompanyDefine")]
            public Nullable<int> LSCompanyID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmployeeName")]
            public int EmpID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmployeeName")]
            public string EmployeeName { get; set; }
            public string SelfDeduction { get; set; }
            public string DependantRelief { get; set; }
            public int Month { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Year")]
            public int Year { get; set; }
            public string StandardWorkINfo { get; set; }
            public string ActualWorkINfo { get; set; }
            public string SalaryInfo { get; set; }
            public string SalaryInsuranceInfo { get; set; }
            public Nullable<decimal> ExchangeRateSalary { get; set; }
            public Nullable<decimal> ExchangeRateInsurance { get; set; }
            public string ExchangeRateInsuranceInfo { get; set; }
            public string ExchangeRateSalaryInfo { get; set; }
            public string CurrencyName { get; set; }
            public string SocialInsuranceInfo { get; set; }
            public string HealthInsucreanceInfo { get; set; }
            public string UnEmployeeInsuranceInfo { get; set; }
            public int Dependent { get; set; }
            public string DeductionBeforTax { get; set; }
            public string DeductionAfterTax { get; set; }
            public string OT { get; set; }
            public string OT_IncomeTax { get; set; }
            public string OT_NoneTax { get; set; }
            public string PITInfo { get; set; }
            public string TotalIncome { get; set; }
            public string TaxableIncome { get; set; }
            public string IncomeTax { get; set; }
            public string NetTakeHome { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmployeeCode")]
            public string EmployeeCode { get; set; }
            public string CompanyName { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Position")]
            public Nullable<int> LSPositionID { get; set; }
            public string PositionName { get; set; }
            public Nullable<int> LSCurrencyID { get; set; }
        }        
    }
    [MetadataTypeAttribute(typeof(PR_sprptPaySlipReport_Result.Metadata))]
    public partial class PR_sprptPaySlipReport_Result
    {
        internal class Metadata
        {
            public int EmpID { get; set; }
            public int Month { get; set; }
            public int Year { get; set; }
            public string EmployeeCode { get; set; }
            public string EmployeeName { get; set; }
            public string CompanyName { get; set; }
            public string PositionName { get; set; }
            public string CurrencyName { get; set; }
            public int Dependent { get; set; }
            public string ActualWorkINfo { get; set; }
            public string SalaryInfo { get; set; }
            public string AdditionSalaryBeforeTaxInfo { get; set; }
            public string OTIncomeInfo { get; set; }
            public string OTNoneTaxInfo { get; set; }
            public string AdditionSalaryAfterTaxInfo { get; set; }
            public string SocialInsuranceInfo { get; set; }
            public string HealthInsucreanceInfo { get; set; }
            public string UnEmployeeInsuranceInfo { get; set; }
            public string A1 { get; set; }
            public string B { get; set; }
            public string EmployeeIncome { get; set; }
            public string PersonalRelief { get; set; }
            public string DependantRelief { get; set; }
            public string PITInfo { get; set; }
            public string Nettakehome { get; set; }
            public Nullable<int> OtherTaxable { get; set; }
            public Nullable<int> OhtherNoneTaxable { get; set; }
            public Nullable<decimal> ExchangeRateSalary { get; set; }
            public Nullable<decimal> ExchangeRateInsurance { get; set; }
        }        
    }
    [MetadataTypeAttribute(typeof(PR_vPayRoll.Metadata))]
    public partial class PR_vPayRoll
    {
        internal class Metadata
        {
            public int PRID { get; set; }
            public int EmpID { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Year")]
            public int Year { get; set; }
            public int Month { get; set; }
            public decimal StandardWork { get; set; }
            public decimal ActualWork { get; set; }
            public Nullable<bool> isGross { get; set; }
            public Nullable<decimal> BasicSalaryNET { get; set; }
            public Nullable<decimal> BasicSalaryGROSS { get; set; }
            public Nullable<decimal> CoefSalary { get; set; }
            public Nullable<decimal> Salary { get; set; }
            public Nullable<decimal> BasicSalaryInsurance { get; set; }
            public Nullable<int> LSCurrencyIDSalary { get; set; }
            public Nullable<decimal> ExchangeRateSalary { get; set; }
            public Nullable<int> LSCurrencyIDInsurance { get; set; }
            public Nullable<decimal> ExchangeRateInsurance { get; set; }
            public decimal MinSalary { get; set; }
            public Nullable<decimal> CoefInS { get; set; }
            public decimal CoefInH { get; set; }
            public Nullable<decimal> CoefInE { get; set; }
            public Nullable<decimal> CoefInS_C { get; set; }
            public Nullable<decimal> CoefInH_C { get; set; }
            public Nullable<decimal> CoefInE_C { get; set; }
            public Nullable<decimal> InS { get; set; }
            public Nullable<decimal> InH { get; set; }
            public Nullable<decimal> InE { get; set; }
            public Nullable<decimal> InS_C { get; set; }
            public Nullable<decimal> InH_C { get; set; }
            public Nullable<decimal> InE_C { get; set; }
            public Nullable<int> Dependent { get; set; }
            public Nullable<decimal> DependentDeduction { get; set; }
            public Nullable<decimal> SelfDeduction { get; set; }
            public Nullable<decimal> Deduction { get; set; }
            public Nullable<decimal> DeductionBeforTax { get; set; }
            public Nullable<decimal> DeductionAfterTax { get; set; }
            public Nullable<decimal> AdditionSalaryBeforTax { get; set; }
            public Nullable<decimal> AdditionSalaryAfterTax { get; set; }
            public Nullable<decimal> OT { get; set; }
            public Nullable<decimal> OT_NoneTax { get; set; }
            public Nullable<decimal> OT_IncomeTax { get; set; }
            public Nullable<decimal> TotalIncome { get; set; }
            public Nullable<decimal> TaxableIncome { get; set; }
            public Nullable<decimal> IncomeTax { get; set; }
            public Nullable<decimal> TotalIncomeNET { get; set; }
            public Nullable<decimal> NetTakeHome { get; set; }
        }        
    }    
    [MetadataTypeAttribute(typeof(PR_tblSalary13.Metadata))]
    public partial class PR_tblSalary13
    {
        internal class Metadata
        {
            public int Salary13ID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Salary13CoefYear")]
            public int Year { get; set; }
            public int EmpID { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Salary13CoefCalMonth")]
            public Nullable<int> CalMonth { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Salary13CoefOnGrid")]
            public Nullable<decimal> Coef { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BasicSalary")]
            public Nullable<decimal> BasicSalary { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Salary13")]
            public Nullable<decimal> Salary13 { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Salary13Month")]
            public Nullable<System.DateTime> MonthSalary { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(PR_tblSalary13Coef.Metadata))]
    public partial class PR_tblSalary13Coef
    {
        internal class Metadata
        {
            public int SalaryCoefID { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Salary13CoefYear")]
            public int Year { get; set; }
            public int EmpID { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Salary13CoefCalMonth")]
            public Nullable<int> CalMonth { get; set; }
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Salary13CoefOnGrid")]
            public Nullable<decimal> Coef { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(PR_tblDeductionSalary.MetaData))]
    public partial class PR_tblDeductionSalary
    {
        internal sealed class MetaData
        {
            public int DeductionSalaryID { get; set; }
            public int EmpID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Type")]
            public int LSDeductSalaryID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromMonth")]
            public string FromMonth { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToMonth")]
            public string ToMonth { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PaymentMethod")]
            public string PaymentMethod { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Amount")]
            public decimal Amount { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Currency")]
            public Nullable<int> LSCurrencyID { get; set; }

            public bool isGross { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            [MaxLength(200, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "MaxLength")]
            public string Note { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(PR_tblAdditionSalary.MetaData))]
    public partial class PR_tblAdditionSalary
    {
        internal sealed class MetaData
        {
            public int SalaryAdjustID { get; set; }
            public int EmpID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Type")]
            public int LSSalaryAdjustID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromMonth")]
            public string FromMonth { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToMonth")]
            public string ToMonth { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PaymentMethod")]
            public string PaymentMethod { get; set; }
            

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Amount")]
            public decimal Amount { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Currency")]
            public Nullable<int> LSCurrencyID { get; set; }

            
            public bool isGross { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            [MaxLength(200, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "MaxLength")]
            public string Note { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(PR_tblAddSalaryByPosition.MetaData))]
    public partial class PR_tblAddSalaryByPosition
    {
        internal sealed class MetaData
        {

            public int AddSalaryByPositionID { get; set; }

            
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Position")]
            public int LSPositionID { get; set; }

            
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Type")]
            public int LSSalaryAdjustID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromMonth")]
            public string FromMonth { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToMonth")]
            public string ToMonth { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PaymentMethod")]
            public string PaymentMethod { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Amount")]
            public decimal Amount { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Currency")]
            public Nullable<int> LSCurrencyID { get; set; }
            
            public bool isGross { get; set; }

            
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            
            public string Note { get; set; }
        }

    }
    
    [MetadataTypeAttribute(typeof(PR_tblPayrollExchangeRate.MetaData))]
    public partial class PR_tblPayrollExchangeRate
    {
        internal sealed class MetaData
        {
            public int LSPayrollExchangeRateID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EffectiveDate")]
            public System.DateTime EffectiveDate { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Currency")]
            public int LSCurrencyID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ExchangeRate")]
            public decimal ExchangeRate { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
        }
             
    }

    [MetadataTypeAttribute(typeof(PR_tblInsuranceExchangeRate.MetaData))]
    public partial class PR_tblInsuranceExchangeRate
    {
        internal sealed class MetaData
        {
            public int LSInsuranceExchangeRateID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EffectiveDate")]
            public System.DateTime EffectiveDate { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Currency")]
            public int LSCurrencyID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ExchangeRate")]
            public decimal ExchangeRate { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
        }

    }

    [MetadataTypeAttribute(typeof(PR_tblIncomeTaxGROSS.MetaData))]
    public partial class PR_tblIncomeTaxGROSS
    {
        internal sealed class MetaData
        {
            public int IncomeTaxGrossID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EffectiveDate")]
            public System.DateTime EffectiveDate { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public decimal IncomeTo1 { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Range(1, 100, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "RangeInvalid")]
            public int Rate1 { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public decimal IncomeTo2 { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Range(1, 100, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "RangeInvalid")]
            public int Rate2 { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public decimal IncomeTo3 { get; set; }

            [Range(1, 100, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "RangeInvalid")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int Rate3 { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public decimal IncomeTo4 { get; set; }

            [Range(1, 100, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "RangeInvalid")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int Rate4 { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public decimal IncomeTo5 { get; set; }

            [Range(1, 100, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "RangeInvalid")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int Rate5 { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public decimal IncomeTo6 { get; set; }
            
            [Range(1, 100, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "RangeInvalid")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int Rate6 { get; set; }
            
            public decimal IncomeTo7 { get; set; }

            [Range(1, 100, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "RangeInvalid")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public int Rate7 { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(PR_tblIncometaxNET.MetaData))]
    public partial class PR_tblIncometaxNET
    {
        internal sealed class MetaData
        {
            public int IncometaxNetID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EffectiveDate")]
            public System.DateTime EffectiveDate { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public decimal? IncomeTo1 { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public decimal Subtract1 { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public decimal Divide1 { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public decimal? IncomeTo2 { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public decimal Subtract2 { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public decimal Divide2 { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public decimal? IncomeTo3 { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public decimal Subtract3 { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public decimal Divide3 { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public decimal IncomeTo4 { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public decimal Subtract4 { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public decimal Divide4 { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public decimal IncomeTo5 { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public decimal Subtract5 { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public decimal Divide5 { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public decimal IncomeTo6 { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public decimal Subtract6 { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public decimal Divide6 { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public decimal IncomeTo7 { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public decimal Subtract7 { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public decimal Divide7 { get; set; }
        }
    }


    [MetadataTypeAttribute(typeof(PR_tblSalaryRank.MetaData))]
    public partial class PR_tblSalaryRank
    {
        internal sealed class MetaData
        {
            public int SalaryRankID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
            public string SalaryRankCode { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SalaryGrade")]
            public int SalaryGradeID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            public string Name { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "VNName")]
            public string VNName { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CoefSalary")]
            [Range(1, 100, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "RangeInvalid")]
            public decimal Coef { get; set; }
            
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<int> Rank { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public bool Used { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(PR_tblSalaryGrade.MetaData))]
    public partial class PR_tblSalaryGrade
    {
        internal sealed class MetaData
        {
            public int SalaryGradeID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SalaryScale")]
            public int SalaryScaleID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
            public string SalaryGradeCode { get; set; }
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            public string Name { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "VNName")]
            public string VNName { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<int> Rank { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public bool Used { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(PR_tblSalaryScale.MetaData))]
    public partial class PR_tblSalaryScale
    {
        internal sealed class MetaData
        {
            public int SalaryScaleID { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
            public string SalaryScaleCode { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            public string Name { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "VNName")]
            public string VNName { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            public Nullable<int> Rank { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public bool Used { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
        }
    }

    [MetadataTypeAttribute(typeof(LS_tblSalaryDeduction.MetaData))]
    public partial class LS_tblSalaryDeduction
    {
        internal sealed class MetaData
        {
            public int LSDeductSalaryID { get; set; }

            [StringLength(50)]            
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
            public string LSDeductSalaryCode { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            public string Name { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "VNName")]
            public string VNName { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BeforeTax")]
            public bool BeforeTax { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public Nullable<int> Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public bool Used { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }
        }
    }

    /// <summary>
    /// LS_tblSalaryAdiust
    /// </summary>
    [MetadataTypeAttribute(typeof(LS_tblSalaryAdjust.Metadata))]
    public partial class LS_tblSalaryAdjust
    {        
        internal class Metadata
        {
            public int LSSalaryAdjustID { get; set; }
                       
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [StringLength(15)]
            [Remote("ValidationSalaryAdjust", "Validation", AdditionalFields = "InitialSalaryAdjust")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
            public string LSSalaryAdjustCode { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [StringLength(150)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
            public string Name { get; set; }
            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [StringLength(150)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "VNName")]
            public string VNName { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PR_SalaryType")]
            public int TypeAdd { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BeforeTax")]
            public bool PIT { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PR_AddSalary")]
            public bool AddSalary { get; set; }

            [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PR_TypeAdd")]
            public int Type { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
            public short Rank { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PR_MaxNonePIT")]
            public Nullable<decimal> MaxNonPIT { get; set; }

            [StringLength(255)]
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
            public string Note { get; set; }

            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
            public Nullable<bool> Used { get; set; }
        }
    }
}
