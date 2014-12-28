using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace Eagle.Model
{
    public class SchoolEntity
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
    public class LS_tblSchoolViewModel
    {
        public int LSSchoolID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSSchoolCode")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string LSSchoolCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "VNName")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string VNName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
        [Range(0, 100, ErrorMessage = "Number must be a positive number {0} from {1} to {2}")]
        public Nullable<short> Rank { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
        public string Note { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
        public Nullable<bool> Used { get; set; }

        public string strUsed
        {
            get
            {
                if (Used == true)
                {
                    return "X";
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
