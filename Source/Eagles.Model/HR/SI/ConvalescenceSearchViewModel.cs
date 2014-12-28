using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model
{
    public class ConvalescenceSearchViewModel
    {
        public string EmpCode { get; set; }
        public string FullName { get; set; }
        public int? LSCompanyID { get; set; }
        public string LSCompanyName { get; set; }
        public int? LSWorkPlaceID { get; set; }
        public string LSWorkPlaceName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
    public class ConvalescenceSearch2ViewModel
    {
        public string EmpCode { get; set; }
        public string FullName { get; set; }
        public int? LSCompany2ID { get; set; }
        public string LSCompany2Name { get; set; }
        public int? LSWorkPlace2ID { get; set; }
        public string LSWorkPlace2Name { get; set; }
        public DateTime? FromDate2 { get; set; }
        public DateTime? ToDate2 { get; set; }
    }
}
