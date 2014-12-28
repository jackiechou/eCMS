using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace Eagle.Model
{
    public class UserGroupViewModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "User")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int UserID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Group")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int GroupID { get; set; }

        public string Creater { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }

        public IEnumerable<SelectListItem> UserList { get; set; }
        public IEnumerable<string> SelectedItemId { get; set; }
    }
}
