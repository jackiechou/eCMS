using Eagle.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Eagle.Model
{
    public class ConvalescenceViewModel : SI_tblConvalescence
    {

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "FullName")]
        public string FullName { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "EmpCode")]
        public string EmpCode { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Month")]
        public int Month { get; set; }

        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "LastCompanyDefine")]
        public string LSCompanyName { get; set; }

        public string IsConcentrateName { get{
            string ret = "";
            switch (IsConcentrate)
            {
                case true:
                    ret = Eagle.Resource.LanguageResource.Centralization;
                    break;
                case false:
                    ret = Eagle.Resource.LanguageResource.Inattention;
                    break;
                default:
                    break;
            }
            return ret;
        }}

        public string strLeaveDate { get; set; }

        public DateTime tmpToDate { get; set; }
      
        /// <summary>
        /// Lấy trong bảng LS_tblLSLeaveType
        /// Dùng để phân biệt chế độ
        /// Nghỉ do bệnh:
        /// Nghỉ thai sản:
        /// Nghỉ tai nạn nghề nghiệp:
        /// </summary>
        public string LSLeaveTypeCode { get; set; }
        [Display(ResourceType = typeof(Eagle.Resource.LanguageResource), Name = "Regime")]
        public string Regime { get {
            string ret = "";
            switch (LSLeaveTypeCode)
            {
                case "CHILDSICK":
                case "SLLT":
                case "SLST":
                    ret = Eagle.Resource.LanguageResource.Sickness;
                    break;
                case "ANTENATAL":
                case "MAT":
                case "MTL1":
                case "MTL2":
                case "MTL3":
                case "MTL4":
                case "MTL5":
                case "MIS":
                    ret = Eagle.Resource.LanguageResource.Maternity;
                    break;
                case "LAOD":
                    ret = Eagle.Resource.LanguageResource.LAOD;
                    break;
                default:
                    ret = "";
                    break;
            }
            return ret;
        } }
    }
}
