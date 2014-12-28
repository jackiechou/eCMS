using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model.Report.Common
{
    //public class StatisticModel
    //{
        public enum ChartTypeEntity
        {
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "PieChart")]
            PieChart = 1,
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ColumnChart")]
            ColumnChart = 2,
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LineChart")]
            LineChart = 3
        }


        public enum StatisticTypeEntity
        {
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "OrganizationalStructureTypes")]
            OrganizationalStructureTypes = 1,
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "AgePeriod")]
            AgePeriod = 2,
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SalaryLevelRatio")]
            SalaryLevelRatio = 3
        }

        public enum QuarterLyEntity
        {
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FirstQuarter")]
            FirstQuarter = 1,
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SecondQuarter")]
            SecondQuarter = 2,
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "ThirdQuarter")]
            ThirdQuarter = 3,
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FourthQuarter")]
            FourthQuarter = 4
        }

        public enum MonthLyEntity
        {
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "January")]
            January = 1,
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "February")]
            February = 2,
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "March")]
            March = 3,
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "April")]
            April = 4,
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "May")]
            May = 5,
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "June")]
            June = 6,
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "July")]
            July = 7,
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "August")]
            August = 8,
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "September")]
            September = 9,
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "October")]
            October = 10,
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "November")]
            November = 11,
            [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "December")]
            December = 12
        }
    //}
}
