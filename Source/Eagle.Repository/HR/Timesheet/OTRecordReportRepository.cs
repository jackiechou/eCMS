using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Eagle.Core;

namespace Eagle.Repository
{
    public class OTRecordReportRepository
    {
        public EntityDataContext Context { get; set; }

        public OTRecordReportRepository(EntityDataContext context)
        {
            this.Context = context;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<Timesheet_sprptOTRecord_Result> GetListOfOTRecord(Timesheet_sprptOTRecord_Result model, out string errorMessage)
        {
            try
            {
                var listOfFound = this.Context.Timesheet_sprptOTRecord(model.Month, model.Year, model.LSCompanyID, model.FullName, model.LSPositionID).ToList();                               
                errorMessage = String.Empty;

                return listOfFound;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
                return null;
            }
        }       
    }
}
