using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Eagle.Core;

namespace Eagle.Repository
{
    public class WorkingScheduleReportRepository
    {
        public EntityDataContext Context { get; set; }

        public WorkingScheduleReportRepository(EntityDataContext context)
        {
            this.Context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<Timesheet_sprptShiftOfWorkingSchedule_Result> GetListOfWorkingSchedule(int? Month, int? Year, Timesheet_sprptShiftOfWorkingSchedule_Result model, out string errorMessage)
        {
            try
            {
                var listOfFound = this.Context.Timesheet_sprptShiftOfWorkingSchedule(Month.Value, Year.Value, model.LSCompanyID, model.FullName, model.LSPositionID).ToList();
                foreach (var detail in listOfFound)
                {
                    detail.StartDateInfo = detail.StartDate.HasValue ? detail.StartDate.Value.ToString("dd/MM/yyyy") : String.Empty;
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
