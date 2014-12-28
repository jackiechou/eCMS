using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace Eagle.Model
{
    public class ScheduleChangeCreateViewModel
    {
        public int ChangeScheduleID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ScheduleID_To")]
        public int? ScheduleID_To { get; set; }
        public string ScheduleName_To { get; set; }

        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ScheduleID_From")]
        public int? ScheduleID_From { get; set; }
        public string ScheduleName_From { get; set; }

        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromDate")]
        public System.DateTime FromDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromDate")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string strFromDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FromDate")]
        [Required(ErrorMessageResourceType = typeof(Eagle.Resource.LanguageResource), ErrorMessageResourceName = "Required")]
        public string strToDate { get; set; }
        
        private string _strFromDateList;
        public string strFromDateList
        {
            get
            {
                _strFromDateList = String.Format("{0:dd/MM/yyyy}", FromDate);
                return _strFromDateList;
            }
            set
            {
                _strFromDateList = value;
            }
        }


        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ToDate")]
        public System.DateTime ToDate { get; set; }


        private string _strToDateList;
        public string strToDateList 
        {
            get
            {
                _strToDateList = String.Format("{0:dd/MM/yyyy}", ToDate);
                return _strToDateList;
            }
            set
            {
                _strToDateList = value;
            }
        }

        public int EmpID { get; set; }
        public string FullName { get; set; }

        public string Note { get; set; }
        
    }
}
