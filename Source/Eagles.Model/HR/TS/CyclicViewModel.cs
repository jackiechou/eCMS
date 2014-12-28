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
    public class CyclicCreateViewModel : Timesheet_tblCyclic
    {
        public int hToCyclic { get; set; }
        public string NameCyclic { get; set; }
    }
    public class CyclicEmpCreateViewModel : Timesheet_tblCyclicEmp
    {
        
    }
}
