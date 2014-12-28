using Eagle.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagle.Model
{
    public class LS_tblOnlineProcessViewModel : LS_tblOnlineProcess
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Function")]
        public string FunctionName { get; set; }

        public string EmpName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Cc")]
        public string Cc { get; set; }

        public string CcText { get; set; }

        public string CcJson { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Bcc")]
        public string Bcc { get; set; }

        public string BccText { get; set; }

        public string BccJson { get; set; }

        public virtual List<LS_tblOnlineProcessMailBcc> BccList { get; set; }
        public virtual List<LS_tblOnlineProcessMailCc> CcList { get; set; }
    }
}
