using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model.LS
{
    public class LS_tblOnlineProcessMailBccViewModel
    {
        public int DMQuiTrinhID { get; set; }

        public int EmpID { get; set; }

        public virtual HR_tblEmp HR_tblEmp { get; set; }

        public virtual LS_tblOnlineProcess LS_tblOnlineProcess { get; set; }
    }
}
