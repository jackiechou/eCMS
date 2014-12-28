using Eagle.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.LS
{
    public class RelationshipEntity
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
    public class LS_tblRelationshipViewModel
    {  
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSRelationshipID")]
        public int LSRelationshipID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSRelationshipCode")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string LSRelationshipCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "VNName")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string VNName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
        [Range(0, 1000, ErrorMessage = "Number must be a positive number {0} from {1} to {2}")]
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
