using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

using Eagle.Core;

namespace Eagle.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Salary13CoefViewModel : PR_tblSalary13Coef
    {
        public Salary13CoefViewModel()
        {            
            this.Year = DateTime.Now.Year;
            this.CalMonth = 13;
            this.LSCompanyID = 0;
            this.LSPositionID = 0;
            this.IsNoSetting = 1;
            this.IsSetting = 0;
        }
        public int LSCompanyIDTree { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpCode")]            
        public string EmployeeCode { get; set; }
        
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Employee")]            
        public string EmloyeepName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastCompanyDefine")]            
        public int LSCompanyID { get; set; }

        public string CompanyName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Position")]            
        public int LSPositionID { get; set; }

        public string PositionName { get; set; }

        public int IsSetting { get; set; }

        public int IsNoSetting { get; set; }

        public DateTime? StartDate { get; set; }

        public string StartDateInfo
        {
            get
            {
                if (!this.StartDate.HasValue)
                {
                    return String.Empty;
                }
                return this.StartDate.Value.ToString("dd/MM/yyyy");
            }
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LanguageId"></param>
        /// <returns></returns>
        public List<CalMonthInfo> ListOfCalMonths(int LanguageId)
        {
            var listOfCalMonths = new List<CalMonthInfo>();
            listOfCalMonths.Add(new CalMonthInfo(13, LanguageId == 124 ? "Month 13" : "Tháng 13"));
            listOfCalMonths.Add(new CalMonthInfo(14, LanguageId == 124 ? "Month 14" : "Tháng 14"));
            listOfCalMonths.Add(new CalMonthInfo(15, LanguageId == 124 ? "Month 15" : "Tháng 15"));
            listOfCalMonths.Add(new CalMonthInfo(16, LanguageId == 124 ? "Month 16" : "Tháng 16"));
            listOfCalMonths.Add(new CalMonthInfo(17, LanguageId == 124 ? "Month 17" : "Tháng 17"));
            listOfCalMonths.Add(new CalMonthInfo(18, LanguageId == 124 ? "Month 18" : "Tháng 18"));
            listOfCalMonths.Add(new CalMonthInfo(19, LanguageId == 124 ? "Month 19" : "Tháng 19"));
            listOfCalMonths.Add(new CalMonthInfo(20, LanguageId == 124 ? "Month 20" : "Tháng 20"));
            return listOfCalMonths;
        }

        public bool Checked { get; set; }
        
    }

    /// <summary>
    /// 
    /// </summary>
    public class CalMonthInfo
    {
        public CalMonthInfo(int Key, string Text)
        {
            this.Key = Key;
            this.Text = Text;
        }

        public int Key { get; set; }
        public string Text { get; set; }
    }
}
