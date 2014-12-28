using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.User
{
    public class UserInfo
    {
        public int UserID { get; set; }

        public Nullable<int> EmpID { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        public int IsLDAP { get; set; }

        public Nullable<int> FAdm { get; set; }

        public Nullable<int> FLock { get; set; }

        public Nullable<System.DateTime> FromDate { get; set; }

        public Nullable<System.DateTime> ToDate { get; set; }

        public Nullable<System.DateTime> LockDate { get; set; }

        public string Creater { get; set; }
        public string Editer { get; set; }
    }
}
