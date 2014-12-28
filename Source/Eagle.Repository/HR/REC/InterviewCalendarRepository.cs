using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model;

namespace Eagle.Repository
{
    public class InterviewCalendarRepository
    {
        public EntityDataContext context { get; set; }
        public InterviewCalendarRepository(EntityDataContext context)
        {
            this.context = context;
        }


        public List<InterviewCalendarViewModel> Search(ResultsRecordSearchViewModel model)
        {
            if (model != null)
            {
                var result = (from projectTournament in context.REC_tblProjectTournament
                              from candidate in projectTournament.REC_tblProject.REC_tblCandidate
                             
                              join canTourenament in context.REC_tblCandidateTournament on new { projectTournament.ProjectTournamentID, candidate.CandidateID } equals new { canTourenament.ProjectTournamentID, canTourenament.CandidateID } into listTmp
                              from canT in listTmp.DefaultIfEmpty()

                              join interviewCalendar in context.REC_tblInterviewCalendar on new { projectTournament.ProjectTournamentID, candidate.CandidateID } equals new { interviewCalendar.ProjectTournamentID, interviewCalendar.CandidateID } into list2Tmp
                              from InterviewCalendar in list2Tmp.DefaultIfEmpty()
                              
                              where projectTournament.ProjectTournamentID == model.ProjectTournamentID                                   
                                    && (model.FullName == null || (candidate.LastName + " " + candidate.FirstName).Contains(model.FullName))

                              select new InterviewCalendarViewModel()
                              {
                                  InterviewCalendarID = InterviewCalendar == null ? 0 : InterviewCalendar.InterviewCalendarID,
                                  ProjectTournamentID = projectTournament.ProjectTournamentID,
                                  CandidateID = candidate.CandidateID,
                                  InterviewTime = InterviewCalendar.InterviewTime,
                                  Result = (canT == null ? null : canT.Result),
                                  CandidateName = candidate.LastName + " " + candidate.FirstName,
                                  DOB = candidate.DOB
                              }).ToList();

                return result;


            }
            else
            {
                return new List<InterviewCalendarViewModel>();
            }
        }

        public Dictionary<int, string> GetAllInterviewer()
        {
            return (from p in context.REC_tblProjectTournament
                          join e in context.HR_tblEmp on p.EmpID equals e.EmpID
                          select new { EmpID = e.EmpID, FullName = e.LastName + " " + e.FirstName })
                              .Distinct().ToDictionary(p => p.EmpID, p => p.FullName);
        }

        public List<CalendarViewModel> GetCalendarByProject(int? ProjectID, int? ProjectTournamentID)
        {
            List<CalendarViewModel> ret = new List<CalendarViewModel>();
            var result = (from p in context.REC_tblInterviewCalendar
                          join pt in context.REC_tblProjectTournament on p.ProjectTournamentID equals pt.ProjectTournamentID
                          join project in context.REC_tblProject on pt.ProjectID equals project.ProjectID
                          join c in context.REC_tblCandidate on p.CandidateID equals c.CandidateID
                          where p.InterviewTime != null &&
                                (ProjectID == 0 || pt.ProjectID == ProjectID) &&
                                (ProjectTournamentID == 0 || pt.ProjectTournamentID == ProjectTournamentID)
                          select new
                          {
                              c.CandidateID,
                              CandidateName = c.LastName + " " + c.FirstName,
                              project.ProjectCode,
                              p.InterviewTime,
                              pt.SEQ,
                              pt.ProjectTournamentID,
                              project.ProjectID
                          }).ToList();

            //2014-08-19T14:00:00
            if (result != null && result.Count > 0)
            {
                foreach (var item in result)
                {
                    ret.Add(new CalendarViewModel()
                    {
                        start = item.InterviewTime.Value.ToString("yyyy-MM-ddTHH:mm:ss"),
                        end = item.InterviewTime.Value.AddMinutes(30).ToString("yyyy-MM-ddTHH:mm:ss"),
                        title = item.CandidateName + " (" + Eagle.Resource.LanguageResource.ShortTournament + item.SEQ.ToString() + ")",
                        url = "/Admin/CandidateTournament/Index?ModuleID=8&ProjectTournamentID=" + item.ProjectTournamentID.ToString() + "&CandidateID=" + item.CandidateID + "&mode=edit&ProjectID=" + item.ProjectID.ToString() + "&ProjectTournamentName=" + Eagle.Resource.LanguageResource.Tournament + " " + item.SEQ.ToString()
                        
                    });
                }
            }

            return ret;
        }

        public List<CalendarViewModel> GetCalendarByEmployeeID(int? EmpID)
        {
            List<CalendarViewModel> ret = new List<CalendarViewModel>();
            var result = (from p in context.REC_tblInterviewCalendar
                          join pt in context.REC_tblProjectTournament on p.ProjectTournamentID equals pt.ProjectTournamentID
                          join project in context.REC_tblProject on pt.ProjectID equals project.ProjectID
                          join c in context.REC_tblCandidate on p.CandidateID equals c.CandidateID
                          where p.InterviewTime != null && pt.EmpID == EmpID
                          select new
                          {
                              c.CandidateID,
                              CandidateName = c.LastName + " " + c.FirstName,
                              project.ProjectCode,
                              p.InterviewTime,
                              pt.SEQ,
                              pt.ProjectTournamentID,
                              project.ProjectID  
                          }).ToList();


            string color = "#000";
            string bgcolor = "#fff";

            //2014-08-19T14:00:00
            if (result != null && result.Count > 0)
            {
                foreach (var item in result)
                {
                    color = "white";
                    bgcolor = "#3a87ad";
                    ret.Add(new CalendarViewModel()
                    {
                        start = item.InterviewTime.Value.ToString("yyyy-MM-ddTHH:mm:ss"),
                        end = item.InterviewTime.Value.AddMinutes(30).ToString("yyyy-MM-ddTHH:mm:ss"),
                        title = item.CandidateName + " (" + Eagle.Resource.LanguageResource.ShortTournament + item.SEQ.ToString() + ")",
                        url = "/Admin/CandidateTournament/Index?ModuleID=8&ProjectTournamentID=" + item.ProjectTournamentID.ToString() + "&CandidateID=" + item.CandidateID + "&mode=edit&ProjectID=" + item.ProjectID.ToString() + "&ProjectTournamentName=" + Eagle.Resource.LanguageResource.Tournament +" "+ item.SEQ.ToString(),
                        bgcolor = bgcolor,
                        color = color
                    });
	            }
            }

            return ret;
        }
    }
}
