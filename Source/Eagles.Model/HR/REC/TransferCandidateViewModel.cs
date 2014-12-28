using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model
{
    public class TransferCandidateViewModel
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CandidateCode")]
        public string CandidateCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FullName")]
        public string FullName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Gender")]
        public Nullable<int> Gender { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Status")]
        public Nullable<int> Status { get; set; }
    }
}
