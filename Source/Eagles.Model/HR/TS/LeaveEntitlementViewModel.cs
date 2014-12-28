using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace Eagle.Model
{
    public class LeaveEntitlementViewModel
    {
        public decimal? LeaveEntitlement { get; set; }
        public int Seniority { get; set; }
        public decimal? LeaveForward { get; set; }
        public decimal? TotalLeave { get; set; }
        public decimal? LeaveTaken { get; set; }
        public decimal? Balance { get; set; }

    }
}
