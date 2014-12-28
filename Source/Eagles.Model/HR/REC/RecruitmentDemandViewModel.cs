using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Core;
using System.ComponentModel.DataAnnotations;

namespace Eagle.Model
{
    public class RecruitmentDemandViewModel : REC_tblDemand
    {

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LSPositionID")]
        public string LSPositionName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastCompanyDefine")]
        public string LSCompanyName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "CreatedByUserId")]
        public string EmpCreateName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DemandDate")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public DateTime? DemandDateAlowNull { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EffectiveDate")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public DateTime? EffectiveDateAlowNull { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "DemandQuantity")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int? DemandQuantityAlowNull { get; set; }

       
        public string StatusProcessName { 
            get{
                switch (StatusProcess)
                {
                    case 1:
                        return Eagle.Resource.LanguageResource.Open;
                    case 2:
                        return Eagle.Resource.LanguageResource.Closed;
                    default:
                        return "";
                }
            }
        }

        public string s2 { get; set; }
        public string s3 { get; set; }
        public string s4 { get; set; }
        public string s5 { get; set; }
        public string s6 { get; set; }
        public string s7 { get; set; }
        public string s8 { get; set; }
        public string s9 { get; set; }
        public string s10 { get; set; }
        public string s11 { get; set; }
        
        
        public string StatusDemandName {

            get
            {
                switch (StatusDemand)
                {
                    case 0:
                        return Eagle.Resource.LanguageResource.Save;
                    case 1:
                        return Eagle.Resource.LanguageResource.WaittingForApproval;
                    case 2:
                        return s2;
                    case 3:
                        return s3;
                    case 4:
                        return s4;
                    case 5:
                        return s5;
                    case 6:
                        return s6;
                    case 7:
                        return s7;
                    case 8:
                        return s8;
                    case 9:
                        return s9;
                    case 10:
                        return s10;
                    case 11:
                        return s11;
                    default:
                        return "";
                }
            }
        }
    }
}
