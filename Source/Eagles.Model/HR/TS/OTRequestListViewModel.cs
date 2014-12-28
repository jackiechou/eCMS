using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using Eagle.Core;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Eagle.Common.Utilities;

namespace Eagle.Model
{
    public class OTRequestListViewModel :Timesheet_tblOTRecordDetail
    {
        public bool chkCheck { get; set; }
        public string TypeDate { get; set; }
        public string Creater { get; set;}
        public DateTime CreateTime { get; set; }
        public int Status { get; set; }
        public string strDateID {
            get
            {
                return String.Format("{0:dd/MM/yyyy}", DateID);
            }
            set
            {
                DateTime? dDateID = new DateTime();
                DateTimeUtils.TryConvertToDate(value, out dDateID);
                DateID = dDateID;
            }
        }
    }
}
