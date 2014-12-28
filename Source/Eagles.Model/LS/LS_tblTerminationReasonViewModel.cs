using Eagle.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.LS
{
    public class TerminationReasonEntity
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
    public class LS_tblTerminationReasonViewModel
    {
        public LS_tblTerminationReasonViewModel()
        {
            this.TER_tblTermination = new HashSet<TER_tblTermination>();
        }
        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSTerminationReasonID")]
        public int LSTerminationReasonID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string Code { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "VNName")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string VNName { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
        [Range(0, 1000, ErrorMessage = "Number must be a positive number {0} from {1} to {2}")]
        public int Rank { get; set; }

         [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
        public bool Used { get; set; }

         private string _strUsed;

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
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

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
        public string Note { get; set; }

        public virtual ICollection<TER_tblTermination> TER_tblTermination { get; set; }
    }
}
