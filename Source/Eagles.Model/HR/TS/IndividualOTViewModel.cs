using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace Eagle.Model
{
    public class IndividualOTSearchViewModel
    {
        //
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmployeeCode")]
        public string EmpCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FullName")]
        public string FullName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Company")]
        public int LSCompanyID { get; set; }

        
    }
    public class IndividualOTListViewModel
    {
        //
        public string EmpCode { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public DateTime? DateID { get; set; }
        public string strDateID
        {
            get 
            {
                try
                {
                    return DateID.Value.ToString("dd/MM/yyyy");
                }
                catch
                {
                    return "";
                }
                
            }
        }
        public DateTime? TimeInAM { get; set; }
        public string strTimeInAM {
            get 
            {
                try
                {
                    return TimeInAM.Value.ToString("HH:mm");
                }
                catch
                {
                    return "";   
                }
                
            }
        }
        public DateTime? TimeOutAM { get; set; }
        public string strTimeOutAM
        {
            get {
                return TimeOutAM.Value.ToString("HH:mm");
            }
        }
        public DateTime? TimeInPM { get; set; }
        public string strTimeInPM
        {
            get 
            {
                try
                {
                    return TimeInPM.Value.ToString("HH:mm");
                }
                catch
                {
                    return "";
                }
                
            }
        }
        public DateTime? TimeOutPM { get; set; }
        public string strTimeOutPM
        {
            get 
            {
                try
                {
                    return TimeOutPM.Value.ToString("HH:mm");
                }
                catch
                {
                    return "";
                }
                
            }
        }

        public decimal? TotalHours { get; set; }
        public decimal? NightHours { get; set; }
        public decimal? HolidayHours { get; set; }
    }
}
