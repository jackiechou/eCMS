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
    public class SalaryViewModels
    {
        public int SalaryID { get; set; }
        public int EmpID { get; set; }
        public System.DateTime EffectiveDate { get; set; }
        public string DecisionNo { get; set; }
        public Nullable<System.DateTime> SignDate { get; set; }
        public Nullable<int> EmpID_Signer { get; set; }
        public Nullable<int> FileID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BasicSalary")]
        public Nullable<decimal> BasicSalary { get; set; }
        public string strBasicSalary
        {
            get
            {
                return Convert.ToDecimal(BasicSalary).ToString("#,##0");
            }
        }

        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PercentSalary")]
        [Range(0, 100, ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "RangeInvalid")]
        [DisplayFormat(DataFormatString="{0:###}")]
        public Nullable<decimal> PercentBasicSalary { get; set; }

        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ActualSalary")]
        public decimal ActualSalary { get; set; }

        public string strActualSalary
        {
            get
            {
                return Convert.ToDecimal(ActualSalary).ToString("#,##0");
            }
        }
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Currency")]
        public Nullable<int> LSCurrencyID { get; set; }
        public string LSCurrencyName { get; set; }


        public bool isGross { get; set; }
        public string strGrossNet
        {
            get
            {
                return isGross == true ? "GROSS" : "NET";
            }
        }
        public int TypeSalary { get; set; }
        public string Note { get; set; }

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
        
        public Nullable<bool> GROSSNET { get; set; }


        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "InsuranceSalary")]
        public Nullable<decimal> InsuranceSalary { get; set; }

        public string strInsuranceSalary
        {
            get
            {
                return Convert.ToDecimal(InsuranceSalary).ToString("#,##0");
            }
        }
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Currency")]
        public Nullable<int> InsuranceLSCurrencyID { get; set; }

        public string InsuranceLSCurrencyName { get; set; }
    
    }
    public class SalaryCoefViewModels
    {
        public int SalaryID { get; set; }
        public int EmpID { get; set; }
        public System.DateTime EffectiveDate { get; set; }
        public string DecisionNo { get; set; }
        public Nullable<System.DateTime> SignDate { get; set; }
        public Nullable<int> EmpID_Signer { get; set; }
        public Nullable<int> FileID { get; set; }
        public bool isGross { get; set; }
        public int TypeSalary { get; set; }


        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SalaryScale")]
        public Nullable<int> SalaryScaleID { get; set; }
        public string ScaleName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SalaryGrade")]
        public Nullable<int> SalaryGradeID { get; set; }
        public string GradeName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SalaryRank")]
        public Nullable<int> SalaryRankID { get; set; }
        public string RankName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "BasicCoef")]
        public Nullable<decimal> BasicCoef { get; set; }

        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PercentCoef")]
        public Nullable<decimal> PercentBasicCoef { get; set; }

        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Coef")]
        public Nullable<decimal> ActualCoef { get; set; }


        public string Note { get; set; }

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
        
        public Nullable<bool> GROSSNET { get; set; }


        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "InsuranceCoef")]
        public Nullable<decimal> InsuranceSalary { get; set; }
        
    
    }
}
