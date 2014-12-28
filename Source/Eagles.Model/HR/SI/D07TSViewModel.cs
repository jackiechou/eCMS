using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Core;
using System.ComponentModel.DataAnnotations;

namespace Eagles.Model
{
    public class D07TSViewModel : SI_tblChangingInsuranceInformationDetail
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ChangeTypeDetail")]
        public string ChangeTypeName { get; set; }

        public int STT { get; set; }
        public bool Checked { get; set; }
    }
}
