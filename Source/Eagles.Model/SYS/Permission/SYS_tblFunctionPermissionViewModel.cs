using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagle.Core;

namespace Eagle.Model
{
    public class SYS_tblFunctionPermissionViewModel : SYS_tblFunctionList
    {
        public Nullable<bool> FView { get; set; }
        public Nullable<bool> FEdit { get; set; }
        public Nullable<bool> FDelete { get; set; }
        public Nullable<bool> FExport { get; set; }
    }
}
