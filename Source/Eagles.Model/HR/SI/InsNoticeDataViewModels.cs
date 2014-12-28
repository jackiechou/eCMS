using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Core;
using System.ComponentModel.DataAnnotations;
namespace Eagle.Model
{
    public class InsNoticeDataCreateViewModels
    {
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmployeeCode")]
        public string EmpCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FullName")]
        public string FullName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Company")]
        public Nullable<int> SearchLSCompanyID { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Status")]
        public Nullable<bool> SearchActive1 { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "AriseDate")]
        public string strAriseFromDate { get; set; }        
        public string strAriseToDate { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "InsNoticeDate")]
        public string strInsNoticeFromDate { get; set; }
        public string strInsNoticeToDate { get; set; }

        public string SourceID { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "SourceName")]
        public string SourceName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Stage")]
        public Nullable<int> Stage { get; set; }
    }
    public class InsNoticeDataListViewModels : SI_spfrmInsNoticeData_Result
    {
        public bool chkCheck { get; set; }
        public DateTime? AriseDate2 { get; set; }
        //public string strNewSalary
        //{
        //    get
        //    {
        //        return Convert.ToDecimal(NewSalary).ToString("#,##0");
        //    }
        //}

    }
}
