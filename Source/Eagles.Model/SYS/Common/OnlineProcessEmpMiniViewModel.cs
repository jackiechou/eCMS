using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model
{
    public class OnlineProcessEmpMiniViewModel
    {
        public int OnlineProcessID { get; set; }
        public int EmpID { get; set; }

        public int LSCompanyID { get; set; }
        public string LSCompanyName { get; set; }
        public bool Checked { get; set; }
    }
}
