using Eagle.Core;
using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class InterviewCalendarController : BaseController
    {
        //
        // GET: /Admin/InterviewCalendar/

        public ActionResult Index(ResultsRecordSearchViewModel ParamSearch = null)
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }

            ViewBag.ParamSearch = ParamSearch;
            return View("../Business/HumanResources/REC/InterviewCalendar/Index");
        }

        public ActionResult _Create(ResultsRecordSearchViewModel model)
        {
            if (model == null)
            {
                model = new ResultsRecordSearchViewModel();
            }
            CreateViewBag(model.ProjectID, model.ProjectTournamentID);
            return PartialView("../Business/HumanResources/REC/InterviewCalendar/_Create", model);
        }


        public ActionResult _List(ResultsRecordSearchViewModel model)
        {
            InterviewCalendarRepository _repository = new InterviewCalendarRepository(db);
            List<InterviewCalendarViewModel> list = _repository.Search(model);
            return PartialView("../Business/HumanResources/REC/InterviewCalendar/_List", list);
        }

        #region Post
        public ActionResult Save(List<InterviewCalendarViewModel> list, List<InterviewCalendarViewModel> update)
        {
            if (update != null && update.Count > 0)
            {
                //Cập nhật từ string (Date + Time) => Datetime (InterviewDate + InterviewTime)
                #region
                foreach (var item in update)
                {
                    if (item.Date != null)
                    {
                        DateTime? InterviewDate;
                        Eagle.Common.Utilities.DateTimeUtils.TryConvertToDate(item.Date, out InterviewDate);
                        item.InterviewDate = InterviewDate;

                        if (item.Time != null && item.InterviewDate != null)
                        {
                            int hour = 0;
                            int minute = 0;
                            if (int.TryParse(item.Time.Substring(0, 2), out hour) && int.TryParse(item.Time.Substring(3, 2), out minute))
                            {
                                item.InterviewTime = item.InterviewDate.Value.AddHours(hour).AddMinutes(minute);
                            }
                            else
                            {
                                item.InterviewTime = null;
                            }
                        }
                        else
                        {
                            item.InterviewTime = null;
                            item.InterviewDate = null;
                        }
                    }
                    else
                    {
                        item.InterviewTime = null;
                        item.InterviewDate = null;
                    }

                    
                }
                #endregion
               
                try
                {
                    // Cho vào list tạm
                    #region
                    List<InterviewCalendarViewModel> updateList = new List<InterviewCalendarViewModel>();
                    foreach (var item in update)
                    {
                        if (item.InterviewTime != null)
                        {
                            updateList.Add(new InterviewCalendarViewModel()
                            {
                                InterviewCalendarID = item.InterviewCalendarID,
                                CandidateID = item.CandidateID,
                                ProjectTournamentID = item.ProjectTournamentID,
                                InterviewDate = item.InterviewDate,
                                InterviewTime = item.InterviewTime
                            });
                        }
                        else
                        {
                            updateList.Add(new InterviewCalendarViewModel()
                            {
                                InterviewCalendarID = item.InterviewCalendarID,
                                CandidateID = item.CandidateID,
                                ProjectTournamentID = item.ProjectTournamentID,
                                InterviewDate = null,
                                InterviewTime = null
                            }); 
                        }
                    }
                    #endregion

                    ProjectTournamentRepository _projectTournamentRepository = new ProjectTournamentRepository(db);
                    InterviewCalendarInfoViewModel validateDateModel = _projectTournamentRepository.GetInterviewer(update.First().ProjectTournamentID);

                    //Cập nhật vào CSDL
                    #region 
                    foreach (var item in updateList)
                    {
                        //Kiểm tra hợp lệ (nằm trong khoảng thời gian của vòng phỏng vấn)
                        #region
                        if (item.InterviewTime != null)
                        {
                            if (!(item.InterviewDate.Value.CompareTo(validateDateModel.FromDate) >= 0 && item.InterviewDate.Value.CompareTo(validateDateModel.ToDate) <= 0))
                            {
                                var errorMsg = string.Format(Eagle.Resource.LanguageResource.InterviewerInvalid,
                                                    validateDateModel.FromDateViewer, validateDateModel.ToDateViewer);
                                return Content(errorMsg);
                            }
                        }
                        #endregion



                        //Kiểm tra trong csdl đã có chưa
                        //chưa có thì thêm mới
                        //có rồi thì cập nhật
                        var checkModel = db.REC_tblInterviewCalendar.Where(p => p.CandidateID == item.CandidateID && p.ProjectTournamentID == item.ProjectTournamentID).FirstOrDefault();
                        if (checkModel == null)
                        {
                            REC_tblInterviewCalendar model = new REC_tblInterviewCalendar()
                            {
                                CandidateID = item.CandidateID,
                                ProjectTournamentID = item.ProjectTournamentID,
                                InterviewDate = item.InterviewDate,
                                InterviewTime = item.InterviewTime
                            };
                            db.Entry(model).State = System.Data.Entity.EntityState.Added;
                        }
                        else
                        {
                            checkModel.InterviewDate = item.InterviewDate;
                            checkModel.InterviewTime = item.InterviewTime;
                            db.Entry(checkModel).State = System.Data.Entity.EntityState.Modified;
                        }
                    }
                    db.SaveChanges();
                    return Content("success");
                    #endregion
                }
                catch (Exception ex)
                {
                    return Content(ex.Message);
                }
            }
            else
            {
                return Content("success");
            }
        }
        #endregion
        #region common
        private void CreateViewBag(int? ProjectID = null, int? ProjectTournamentID = null)
        {
            // Lấy danh sách các Phương án tuyển dụng mà nhân viên đang đăng nhập được phỏng vấn
            RecruitmentProjectRepository _recruitmentProjectRepository = new RecruitmentProjectRepository(db);
            Dictionary<int, string> prList = _recruitmentProjectRepository.GetAllProjectCode();
            ViewBag.ProjectID = new SelectList(prList, "Key", "Value", ProjectID);

            //Lấy tất cả các danh sách vòng tuyển dụng thuộc phương án tuyển dụng được chọn
            ProjectTournamentRepository _projectTournamentRepository = new ProjectTournamentRepository(db);
            Dictionary<int, string> rtList = _projectTournamentRepository.GetTournamentByProjectID(ProjectID);
            ViewBag.ProjectTournamentID = new SelectList(rtList, "Key", "Value", ProjectTournamentID);


        }
        #endregion
    }
}
