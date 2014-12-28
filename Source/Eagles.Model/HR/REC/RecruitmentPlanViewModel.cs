using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Core;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Eagle.Model
{
    public class RecruitmentPlanViewModel : REC_tblPlan
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Position")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int? LSPositionIDAlowNull { 
            get
            {
                if (LSPositionID == 0)
                {
                    return null;
                }else
                {
                    return LSPositionID;
                }
            }
            set {
                if (value == null)
                {
                    LSPositionID = 0;
                }
                else 
                {
                    LSPositionID = value.Value;
                }
            }
        }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Position")]
        public string LSPositionName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastCompanyDefine")]
        public string LSCompanyName { get; set; }
    
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "QuotaRecruitment")]
        public int QuotaRecruitment { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Year")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public int? YearAlowNull
        {
            get
            {
                if (Year == 0)
                {
                    return null;
                }
                else
                {
                    return Year;
                }
            }
            set
            {
                if (value == null)
                {
                    Year = 0;
                }
                else
                {
                    Year = value.Value;
                }
            }
        }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PlanQuantity")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Remote("ValidationPlanQuantity", "Validation", AdditionalFields = "QuotaRecruitment,YearAlowNull,LSCompanyID,LSPositionIDAlowNull,PlanID")]
        
        public int? PlanQuantityAlowNull
        {
            get
            {
                if (PlanQuantity == 0)
                {
                    return null;
                }
                else
                {
                    return PlanQuantity;
                }
            }
            set
            {
                if (value == null)
                {
                    PlanQuantity = 0;
                }
                else
                {
                    PlanQuantity = value.Value;
                }
            }
        }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmployeeCreate")]
        public string EmpIDPlanName { get; set; }

        public string StatusProcessName { 
            get
            {
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

        public string StatusPlanName
        {
            get
            {
                switch (StatusPlan)
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

        public string EmpIDFullName { get; set; }
        
    }
}
