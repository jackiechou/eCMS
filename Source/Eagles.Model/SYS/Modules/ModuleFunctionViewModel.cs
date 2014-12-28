using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Modules
{
    public class ModuleFunctionViewModel
    {
        public int ModuleId { get; set; }
        public int FunctionId { get; set; }
        public string FunctionName { get; set; }
        public string DispalyName { get; set; }
        public bool Status { get; set; }
    }
}
