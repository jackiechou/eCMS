using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagle.Model.Common
{
    public class ConditionViewModel
    {
        public int? Condition_ID { get; set; }
        public string Condition_Name { get; set; }
        public int? Condition_Type { get; set; }
        public double? Condition_Money { get; set; }
        public double? Condition_MoneyTL { get; set; }
        public double? Condition_MoneyST { get; set; }
        public Boolean? Actived { get; set; }

        public string sSearch { get; set; }
        public string Status { get; set; }
    }
}