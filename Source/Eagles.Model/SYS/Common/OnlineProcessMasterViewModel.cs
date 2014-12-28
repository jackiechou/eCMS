using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagle.Core;
using System.ComponentModel.DataAnnotations;

namespace Eagle.Model
{
    public class OnlineProcessMasterViewModel : SYS_tblProcessOnlineMaster
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "NameProcessOnline")]
        public string NameProcessOnline { get; set; }
    }
}
