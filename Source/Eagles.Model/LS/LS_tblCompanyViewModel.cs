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
    public class CompanyEntity
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
    public class LS_tblCompanyViewModel
    {
        public int LSCompanyID { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CompanyCode")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string LSCompanyCode { get; set; }

        public int? Parent { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string Name { get; set; }       

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "VNName")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string VNName { get; set; }

        public string LSCompanyName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Address")]
        public string Address { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Phone")]
        public string Phone { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Fax")]              
        
        public string Fax { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Attach")]
        public int? FileId { get; set; }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
        [Range(0, 100, ErrorMessage = "Number must be a positive number {0} from {1} to {2}")]
        public Nullable<int> Rank { get; set; }

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

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "TaxCode")]
        public string TaxCode { get; set; }

        public string Creater { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string Editor { get; set; }
        public Nullable<System.DateTime> EditTime { get; set; }
    }

}
