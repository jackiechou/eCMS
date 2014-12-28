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
    public class SalaryDeductionViewModels : LS_tblSalaryDeduction
    {
        public string strUsed
        {
            get
            {
                return (Used == true ? "x" : "");
            }
        }
        public string strBeforeTax
        {
            get
            {
                return (BeforeTax == true ? "x" : "");
            }
        }
    }
}
