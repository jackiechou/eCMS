using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Eagle.Core;

namespace Eagle.Repository
{
    public class OTNightRecordReportRepository
    {
        public EntityDataContext Context { get; set; }

        public OTNightRecordReportRepository(EntityDataContext context)
        {
            this.Context = context;
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<Timesheet_sprptOTRecord_Detail_Result> GetListOfOTDetailRecord(Timesheet_sprptOTRecord_Detail_Result model, out string errorMessage)
        {
            try
            {
                var listOfFound = this.Context.Timesheet_sprptOTRecord_Detail(model.Month, model.Year, model.LSCompanyID, model.FullName, model.LSPositionID).ToList();
                if (listOfFound != null)
                {
                    foreach (var detail in listOfFound)
                    {
                        detail.OTDateInfo = detail.OTDate.HasValue ? detail.OTDate.Value.ToString("dd/MM/yyyy") : String.Empty;
                        detail.TimeInAMInfo = detail.TimeInAM.HasValue ? detail.TimeInAM.Value.ToString("HH:mm") : String.Empty;
                        detail.TimeOutAMInfo = detail.TimeOutAM.HasValue ? detail.TimeOutAM.Value.ToString("HH:mm") : String.Empty;
                        detail.TimeInPMInfo = detail.TimeInPM.HasValue ? detail.TimeInPM.Value.ToString("HH:mm") : String.Empty;
                        detail.TimeOutPMInfo = detail.TimeOutPM.HasValue ? detail.TimeOutPM.Value.ToString("HH:mm") : String.Empty;
                        detail.NightOTInfo = detail.NightOT.HasValue ? detail.NightOT.Value.ToString("#,#.0") : String.Empty;
                    }                    
                } 
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
