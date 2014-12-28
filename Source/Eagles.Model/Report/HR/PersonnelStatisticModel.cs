using Eagle.Model.Report.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Eagle.Model.Report.HR
{
    public class PersonnelStatisticModel
    {
        public string LSCompanyID { get; set; }
        public StatisticTypeEntity StatisticType { get; set; }
        public int StatisticFocusId { get; set; }
        public List<SelectListItem> StatisticFocus { get; set; }
    }
}
