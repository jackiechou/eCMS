using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model;

namespace Eagle.Repository
{
    public class ProjectTournamentRepository
    {
        public EntityDataContext context { get; set; }
        public ProjectTournamentRepository(EntityDataContext context)
        {
            this.context = context;
        }


        public Dictionary<int, string> GetTournamentByProjectID(int? ProjectID)
        {

            Dictionary<int, string> ret = new Dictionary<int, string>();
            ret.Add(0, "--" + Eagle.Resource.LanguageResource.Select + "--");
            
            string Tournament = Eagle.Resource.LanguageResource.Tournament;
            var result = (from pt in context.REC_tblProjectTournament
                            where pt.ProjectID == ProjectID
                            orderby pt.SEQ
                            select new
                            {
                                ID = pt.ProjectTournamentID,
                                SEQ = pt.SEQ
                            }).ToList();


            if (result != null && result.Count > 0)
            {
                foreach (var item in result)
                {
                    ret.Add(item.ID, Tournament + " "+ item.SEQ.ToString());
                }
            }
            
            return ret;
        }

        public int? GetTournamentDefaultValueByProjectID(int? ProjectID, int? EmpID)
        {
            var result = (from pt in context.REC_tblProjectTournament
                          where pt.ProjectID == ProjectID && pt.EmpID == EmpID
                          orderby pt.SEQ
                          select  pt.ProjectTournamentID
                          ).FirstOrDefault();

            return result;

        }
        public REC_tblProjectTournament Find(int? ProjectTournamentID)
        {
            return context.REC_tblProjectTournament.Find(ProjectTournamentID);
        }


        public InterviewCalendarInfoViewModel GetInterviewer(int ProjectTournamentID)
        {
            return (from projecttournament in context.REC_tblProjectTournament
                          join employee in context.HR_tblEmp on projecttournament.EmpID equals employee.EmpID
                          where projecttournament.ProjectTournamentID == ProjectTournamentID
                    select new InterviewCalendarInfoViewModel() { Interviewer = employee.LastName + " " + employee.FirstName, FromDate = projecttournament.FromDate, ToDate = projecttournament.ToDate }).FirstOrDefault();
        }

    }
}
