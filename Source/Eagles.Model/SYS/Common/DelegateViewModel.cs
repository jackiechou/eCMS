using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagle.Core;
using System.ComponentModel.DataAnnotations;

namespace Eagle.Model
{
    public class DelegateViewModel : SYS_tblDelegate
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromDate")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public Nullable<DateTime> FromDateNullable { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToDate")]
        //[Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public Nullable<DateTime> ToDateNullable { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromDate")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string FromDateNullabletmp { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToDate")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string ToDateNullabletmp { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "NameProcessOnline")]
        public string NameProcessOnline { get; set; }
    }
}
