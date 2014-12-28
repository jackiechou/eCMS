using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace Eagle.Model
{
    public class LeaveApplicationDetailListViewModel
    {
        public int LeaveApplicationDetailID { get; set; }
        public int LeaveApplicationMasterID { get; set; }
        public System.DateTime LeaveDate { get; set; }
        public string Time { get; set; }
        public string _TypeLeave;
        // AM, PM, ALLDAY
        public string TypeLeave {
            get
            {
                switch (Time)
                {
                    case "1" :
                        return "AM";
                    case "2":
                        return "PM";
                    case "3":
                        return "ALLDAYS";
                    default :
                        return "ALLDAYS";

                }
            }
            set
            {
                _TypeLeave = value;
            }
        } 
        
        private string _strLeaveDate;
        public string strLeaveDate
        {
            get
            {
                _strLeaveDate = String.Format("{0:dd/MM/yyyy}", LeaveDate);
                return _strLeaveDate;
            }
            set
            {
                _strLeaveDate = value;
            }
        }

        public decimal Days { get; set; }
    }
}
