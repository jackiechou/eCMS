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
    public class Salary13ViewModel : PR_tblSalary13
    {
        public Salary13ViewModel()
        {            
            this.Year = DateTime.Now.Year;
            this.CalMonth = 13;
            this.LSCompanyID = 0;
            this.LSCompanyIDTree = 0; 
            this.CompanyName = Eagle.Resource.LanguageResource.PleaseInputCompanyName;
            this.LSPositionID = 0;
            this.PositionName = Eagle.Resource.LanguageResource.PleaseInputPositionName;
            this.LSPositionID = 0;
            this.MonthSalary = new DateTime(DateTime.Now.Year + 1, 1, 1);
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

        public string BasicSalaryInfo
        {
            get
            {
                if (this.BasicSalary.HasValue)
                {
                    return this.BasicSalary.Value.ToString("#,###");
                }
                return String.Empty;
            }
        }

        public string CoefInfo
        {
            get
            {
                if (this.Coef.HasValue)
                {
                    return this.Coef.Value.ToString("#,###");
                }
                return String.Empty;
            }
        }

        public string Salary13Info
        {
            get
            {
                if (this.BasicSalary.HasValue && this.Coef.HasValue)
                {
                    decimal result = (this.BasicSalary.Value * this.Coef.Value) / 100;
                    return result.ToString("#,###");
                }
                return String.Empty;
            }
        }
        
    }
}
