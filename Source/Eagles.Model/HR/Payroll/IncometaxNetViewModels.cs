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
    public class IncometaxNetViewModels:  PR_tblIncometaxNET
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
            get {
                return Convert.ToDecimal(IncomeTo1).ToString("#,##0");
            }
        }
        public string strSubtract1
        {
            get
            {
                return Convert.ToDecimal(Subtract1).ToString("#,##0");
            }
        }
        public string strDivide1
        {
            get
            {
                return Convert.ToDecimal(Divide1).ToString("#,##0.00");
            }
        }
        public string strIncomeTo2
        {
            get
            {
                return Convert.ToDecimal(IncomeTo2).ToString("#,##0");
            }
        }
        public string strSubtract2
        {
            get
            {
                return Convert.ToDecimal(Subtract2).ToString("#,##0");
            }
        }
        public string strDivide2
        {
            get
            {
                return Convert.ToDecimal(Divide2).ToString("#,##0.00");
            }
        }
        public string strIncomeTo3
        {
            get
            {
                return Convert.ToDecimal(IncomeTo3).ToString("#,##0");
            }
        }
        public string strSubtract3
        {
            get
            {
                return Convert.ToDecimal(Subtract3).ToString("#,##0");
            }
        }
        public string strDivide3
        {
            get
            {
                return Convert.ToDecimal(Divide3).ToString("#,##0.00");
            }
        }
        public string strIncomeTo4
        {
            get
            {
                return Convert.ToDecimal(IncomeTo4).ToString("#,##0");
            }
        }
        public string strSubtract4
        {
            get
            {
                return Convert.ToDecimal(Subtract4).ToString("#,##0");
            }
        }
        public string strDivide4
        {
            get
            {
                return Convert.ToDecimal(Divide4).ToString("#,##0.00");
            }
        }
        public string strIncomeTo5
        {
            get
            {
                return Convert.ToDecimal(IncomeTo5).ToString("#,##0");
            }
        }
        public string strSubtract5
        {
            get
            {
                return Convert.ToDecimal(Subtract5).ToString("#,##0");
            }
        }
        public string strDivide5
        {
            get
            {
                return Convert.ToDecimal(Divide5).ToString("#,##0.00");
            }
        }
        public string strIncomeTo6
        {
            get
            {
                return Convert.ToDecimal(IncomeTo6).ToString("#,##0");
            }
        }
        public string strSubtract6
        {
            get
            {
                return Convert.ToDecimal(Subtract6).ToString("#,##0");
            }
        }
        public string strDivide6
        {
            get
            {
                return Convert.ToDecimal(Divide6).ToString("#,##0.00");
            }
        }
        public string strIncomeTo7
        {
            get
            {
                return Convert.ToDecimal(IncomeTo7).ToString("#,##0");
            }
        }
        public string strSubtract7
        {
            get
            {
                return Convert.ToDecimal(Subtract7).ToString("#,##0");
            }
        }
        public string strDivide7
        {
            get
            {
                return Convert.ToDecimal(Divide7).ToString("#,##0.00");
            }
        }
    }
}