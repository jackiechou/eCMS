using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Eagle.Model.LS
{
    public class BankEntity
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
    public class LS_tblBankViewModel
    {
        public int LSBankID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Code")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string LSBankCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "VNName")]
        public string VNName { get; set; }

        public string BankName { get; set; }
        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Rank")]
        [Range(0, 1000, ErrorMessage = "Number must be a positive number {0} from {1} to {2}")]
        public Nullable<short> Rank { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Note")]
        public string Note { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Used")]
        public bool Used { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Province")]
        public Nullable<int> LSProvinceID { get; set; }
        public string ProvinceName { get; set; }

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
        public string strProvinceName { get; set; }
    }
}
