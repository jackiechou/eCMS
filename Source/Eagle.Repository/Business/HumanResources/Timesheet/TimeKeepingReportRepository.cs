using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Eagle.Core;

namespace Eagle.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class TimeKeepingReportRepository
    {
        /// <summary>
        /// 
        /// </summary>
        public EntityDataContext Context { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Context"></param>
        public TimeKeepingReportRepository(EntityDataContext Context)
        {
            this.Context = Context;
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserGroupID"></param>
        /// <param name="ModuleID"></param>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="LSCompanyID"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<timesheet_sprptTimeKeepingReport_detail_Result> GetDetailOfTimeKeepingReport(string UserGroupID, int ModuleID, Nullable<DateTime> FromDate, Nullable<DateTime> ToDate, Nullable<int> LSCompanyID, Nullable<int> LSPostionID, string FullName, string EmpCode, out string errorMessage)
        {
            try
            {
                var found = this.Context.timesheet_sprptTimeKeepingReport_detail(UserGroupID, ModuleID, FromDate.HasValue ? FromDate.Value.ToString("dd/MM/yyy") : String.Empty, ToDate.HasValue ? ToDate.Value.ToString("dd/MM/yyy") : String.Empty, LSCompanyID, LSPostionID, FullName, EmpCode).ToList();                
                errorMessage = String.Empty;

                return found;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return null;
            }
        }
    }
}
