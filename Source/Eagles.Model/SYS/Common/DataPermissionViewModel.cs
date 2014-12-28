using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model
{
    public class DataPermissionViewModel
    {
        public int DataPermissionID { get; set; }
        public int ModuleID { get; set; }
        public int GroupID { get; set; }
        public int LSCompanyID { get; set; }
        public string LSCompanyName { get; set; }
        public bool Checked { get; set; }
    }
}
