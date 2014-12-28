using Eagle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagle.Model;

namespace Eagle.Repository
{
    public class CandidateTournamentRepository
    {
        public EntityDataContext context { get; set; }
        public CandidateTournamentRepository(EntityDataContext context)
        {
            this.context = context;
        }


        public List<CandidateTournamentViewModel> Search(ResultsRecordSearchViewModel model, out bool IsFirstRound)
        {
            if (model != null && model.ProjectID != null)
            {
                int first = 0 ;
                List<int> candidateList = new List<int>();
                bool isFirstRound = false;
                IsFirstRound = false;
                first = FindFirstRound(model.ProjectID);

                //model.Result == -1 : Tất cả
                //model.Result ==  0 : Chưa duyệt
                //model.Result ==  1 : Đạt
                //model.Result ==  2 : Không đạt

                //Nếu là vòng đầu tiên thì lấy tất cả các ứng viên
                //Nếu không phải là vòng đầu tiên thì lấy ứng viên đã đạt ở vòng trước
                if (model.ProjectTournamentID == first)
                {
                    isFirstRound = true;
                    IsFirstRound = true;
                }
                else
                {
                    //ProjectTournamentID của vòng trước đó
                    int preRound = FindPrevRound(model.ProjectID, model.ProjectTournamentID);
                    //Danh sách các ứng viên đã đậu vòng trước đó
                    candidateList = (from ct in context.REC_tblCandidateTournament
                                         where ct.ProjectTournamentID == preRound && ct.Result == true
                                         select ct.CandidateID).ToList();
                }
                var result = (from projectTournament in context.REC_tblProjectTournament
                              from candidate in projectTournament.REC_tblProject.REC_tblCandidate
                              join canTourenament in context.REC_tblCandidateTournament on new { projectTournament.ProjectTournamentID, candidate.CandidateID } equals new { canTourenament.ProjectTournamentID, canTourenament.CandidateID } into listTmp
                              from canT in listTmp.DefaultIfEmpty()
                              where projectTournament.ProjectTournamentID == model.ProjectTournamentID
                                    && (model.Result == -1 //Tất cả
                                        || (model.Result == 0 && (canT == null || canT.Result == null))//Chưa xét duyệt
                                        || (model.Result == 1 && (canT != null && canT.Result == true))//Đạt
                                        || (model.Result == 1 && (canT != null && canT.Result == false))//Không đạt
                                        )
                                    && (model.FullName == null || (candidate.LastName + " " + candidate.FirstName).Contains(model.FullName))
                                    && (isFirstRound == true || candidateList.Contains(candidate.CandidateID))
                              select new CandidateTournamentViewModel()
                              {
                                  CandidateTournamentID = canT == null ? 0 : canT.CandidateTournamentID,
                                  ProjectTournamentID = projectTournament.ProjectTournamentID,
                                  CandidateID = candidate.CandidateID,
                                  Result = (canT == null ? null : canT.Result),
                                  CandidateName = candidate.LastName + " " + candidate.FirstName,
                                  Note = canT == null ? null : canT.Note,
                                  DOB = candidate.DOB
                              }).ToList();

                return result;
            }
            else
            {
                IsFirstRound = false;
                return new List<CandidateTournamentViewModel>();
            }
        }



        public int FindFirstRound(int? ProjectID)
        {
            int result = (from pt in context.REC_tblProjectTournament
                          where pt.ProjectID == ProjectID
                          orderby pt.SEQ
                          select pt.ProjectTournamentID).FirstOrDefault();
            return result;
        }
        public int FindSEQLastRound(int? ProjectID)
        {
            int result = (from pt in context.REC_tblProjectTournament
                          where pt.ProjectID == ProjectID
                          orderby pt.SEQ descending
                          select pt.SEQ).FirstOrDefault();
            return result;
        }
        public bool IsLastTournament(int ProjectTournamentID)
        {
            var ProjectTournament = context.REC_tblProjectTournament.Find(ProjectTournamentID);
            if (ProjectTournament != null)
            {
                //ProjectTournamentID của vòng cuối cùng
                int result = (from pt in context.REC_tblProjectTournament
                                where pt.ProjectID == ProjectTournament.ProjectID
                    orderby pt.SEQ descending
                    select pt.ProjectTournamentID).FirstOrDefault();
                return result == ProjectTournamentID;
            }
            else
            {
                return false;
            }
        }

        public int FindPrevRound(int? ProjectID, int? ProjectTournamentID)
        {
            try
            {
                var SEQ = (from pt in context.REC_tblProjectTournament
                           where pt.ProjectID == ProjectID
                           && pt.ProjectTournamentID == ProjectTournamentID
                           select pt.SEQ).First();
                
                var result = (from pt in context.REC_tblProjectTournament
                              where pt.ProjectID == ProjectID && pt.SEQ < SEQ
                               orderby pt.SEQ descending
                               select pt.ProjectTournamentID).First();
                return result;
            }
            catch
            {
                return 0;
            }
        }

        public CandidateTournamentViewModel Find(int? CandidateID, int? ProjectTournamentID)
        {
            int ptID = 0;
            try
            {
                ptID = ProjectTournamentID.Value;
            }
            catch { }
            var result = (from c in context.REC_tblCandidate

                          join ct in context.REC_tblCandidateTournament on new { CandidateID = CandidateID != null ? CandidateID.Value : 0, ProjectTournamentID = ProjectTournamentID != null? ProjectTournamentID.Value : 0 } equals new { ct.CandidateID, ct.ProjectTournamentID } into tmpList
                          from candidateTournament in tmpList.DefaultIfEmpty()

                          where c.CandidateID == CandidateID
                          select new CandidateTournamentViewModel()
                          {
                              CandidateTournamentID = candidateTournament == null ? 0 : candidateTournament.CandidateTournamentID,
                              CandidateName = c.LastName + " " + c.FirstName,
                              CandidateID = c.CandidateID,
                              ProjectTournamentID = ptID,
                              DOB = c.DOB,
                              Result = candidateTournament.Result,
                              Note = candidateTournament.Note,
                              REC_tblCandidateTournament_InterviewCriteria = candidateTournament.REC_tblCandidateTournament_InterviewCriteria
                          });

            return result.FirstOrDefault();
        }

        public bool Add(REC_tblCandidateTournament modelAdd,out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                context.Entry(modelAdd).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public bool Edit(REC_tblCandidateTournament modelAdd, out string ErrorMessage)
        {
            ErrorMessage = "";
            try
            {
                foreach (var item in modelAdd.REC_tblCandidateTournament_InterviewCriteria)
                {
                    context.Entry(item).State = System.Data.Entity.EntityState.Modified;

                }
                context.Entry(modelAdd).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }



        public List<PreviousRoundViewModel> GetPreviousRound(int? CandidateID, int? ProjectTournamentID)
        {
            var thisTournament = context.REC_tblProjectTournament.Where(p => p.ProjectTournamentID == ProjectTournamentID).Select(p => new { p.SEQ, p.ProjectID }).FirstOrDefault();
            if (thisTournament != null)
            {
                //Lấy thông tin Master
                //ProjectTournament
                var result = (from pt in context.REC_tblProjectTournament
                              join ct in context.REC_tblCandidateTournament on pt.ProjectTournamentID equals ct.ProjectTournamentID
                              join emp in context.HR_tblEmp on pt.EmpID equals emp.EmpID
                              orderby pt.SEQ
                              where pt.ProjectID == thisTournament.ProjectID && pt.SEQ < thisTournament.SEQ && ct.CandidateID == CandidateID
                              select new PreviousRoundViewModel() { CandidateTournamentID = ct.CandidateTournamentID, Result = ct.Result, SEQ = pt.SEQ, Interviewer = emp.LastName + " " + emp.FirstName }).ToList();
                
                foreach (var item in result)
                {
                    var InterviewCiteriaList = (from ctic in context.REC_tblCandidateTournament_InterviewCriteria
                                                join ic in context.REC_tblInterviewCriteria on ctic.InterviewCriteriaID equals ic.InterviewCriteriaID
                                                where ctic.CandidateTournamentID == item.CandidateTournamentID
                                                select new InterviewCriteriaViewModel()
                                                {
                                                    Name = ic.Name,
                                                    Note = ctic.Note,
                                                    Rank = ctic.Rank
                                                }).ToList();
                    if (item.InterviewCriter == null)
                    {
                        item.InterviewCriter = new List<InterviewCriteriaViewModel>();
                    }
                    item.InterviewCriter.AddRange(InterviewCiteriaList);
                }

                return result;
            }
            else
            {
                return null;
            }
            

        }
    }
}
