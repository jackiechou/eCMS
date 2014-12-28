using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagle.WebApp.Models
{
    public class ReportsModel
    {
        public string ReportID { get; set; }
        public XtraReport Report { get; set; }
    }
}