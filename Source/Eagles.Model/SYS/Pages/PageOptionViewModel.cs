using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model.SYS.Pages
{
    public class PageOptionViewModel
    {     
        public System.Guid PageCode { get; set; }
        public int OptionId { get; set; }
        public System.Guid OptionCode { get; set; }
        public string OptionName { get; set; }
        public string OptionValue { get; set; }
    }
}
