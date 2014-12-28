using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Core;
using System.ComponentModel.DataAnnotations;

namespace Eagle.Model
{
    public class WorkingExpectationViewModel : REC_tblWorkingExpectation
    {
        public string LSPositionName { get; set; }
        public string LSPositionID_Probation2Name { get; set; }
        public string LSPositionID_Probation1Name { get; set; }
        public string LSCurrencyName { get; set; }
        public string LSLocationName { get; set; }

        
    }
}
