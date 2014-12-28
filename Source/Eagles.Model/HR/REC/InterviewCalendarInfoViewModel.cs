using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eagle.Model
{
    public class InterviewCalendarInfoViewModel
    {
        public string Interviewer { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string FromDateViewer { get {
            return FromDate.ToString("dd/MM/yyyy");
        } }
        public string ToDateViewer
        {
            get
            {
                return ToDate.ToString("dd/MM/yyyy");
            }
        }
    }
}
