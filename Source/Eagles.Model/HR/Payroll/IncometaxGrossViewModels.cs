using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Core;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Eagle.Common.Utilities;
namespace Eagle.Model
{
    public class IncometaxGrossViewModels : PR_tblIncomeTaxGROSS
    {
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
        public string strIncomeTo1
        {
            get
            {
                return Convert.ToDecimal(IncomeTo1).ToString("#,##0");
            }
        }
        public string strIncomeTo2
        {
            get
            {
                return Convert.ToDecimal(IncomeTo2).ToString("#,##0");
            }
        }
        public string strIncomeTo3
        {
            get
            {
                return Convert.ToDecimal(IncomeTo3).ToString("#,##0");
            }
        }
        public string strIncomeTo4
        {
            get
            {
                return Convert.ToDecimal(IncomeTo4).ToString("#,##0");
            }
        }
        public string strIncomeTo5
        {
            get
            {
                return Convert.ToDecimal(IncomeTo5).ToString("#,##0");
            }
        }
        public string strIncomeTo6
        {
            get
            {
                return Convert.ToDecimal(IncomeTo6).ToString("#,##0");
            }
        }
        public string strIncomeTo7
        {
            get
            {
                return Convert.ToDecimal(IncomeTo7).ToString("#,##0");
            }
        }
    }
}
