using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagle.Model.LS
{
    public class GradeEntity
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
    public class LS_tblGradeViewModel
    {
        [Key]
        public int LSGradeID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSGradeCode")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string LSGradeCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "VNName")]
        public string VNName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
        public Nullable<short> Rank { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
        public string Note { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
        public Nullable<bool> Used { get; set; }

        private string _strUsed;
        public string strUsed
        {
            get
            {
                if (Used == true)
                {
                    _strUsed = "X";
                }
                else
                {
                    _strUsed = string.Empty;
                }
                return _strUsed;
            }
            set
            {
                _strUsed = value;
            }
        }
    }
}
