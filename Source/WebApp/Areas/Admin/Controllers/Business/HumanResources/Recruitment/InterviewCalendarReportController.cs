using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class InterviewCalendarReportController : BaseController
    {
        
        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            return View("../Business/HumanResources/REC/InterviewCalendarReport/Index");
        }


        public ActionResult _Create()
        {
            CreateViewBag();
            return PartialView("../Business/HumanResources/REC/InterviewCalendarReport/_Create");
        }

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
                  
            InterviewCalendarRepository _interviewCalendarRepository = new InterviewCalendarRepository(db);
            Dictionary<int, string> EmployeeList = _interviewCalendarRepository.GetAllInterviewer();
            ViewBag.Interviewer = new SelectList(EmployeeList, "Key", "Value", CurrentAcc.EmpId);
        }
        #endregion
        //Tìm theo thông tin Phương án và vòng tuyển dụng
        public ActionResult GetCalendarByProject(int? ProjectID, int? ProjectTournamentID)
        {
           //List<CalendarViewModel> list = new List<CalendarViewModel>();
            //list.Add(new CalendarViewModel() { start = "2014-08-19T14:00:00", end = "2014-08-19T14:30:00", title = "Task zz 1" });
            //list.Add(new CalendarViewModel() { start = "2014-08-19T15:00:00", end = "2014-08-19T15:30:00", title = "Task zz 2" });
            //list.Add(new CalendarViewModel() { start = "2014-08-19T16:00:00", end = "2014-08-19T16:30:00", title = "Task zz 3" });
            //list.Add(new CalendarViewModel() { start = "2014-08-20T14:00:00", end = "2014-08-20T14:30:00", title = "Task zz 4" });
            //list.Add(new CalendarViewModel() { start = "2014-08-25T14:00:00", end = "2014-08-25T14:30:00", title = "Task zz 5" });
            //list.Add(new CalendarViewModel() { start = "2014-08-25T15:00:00", end = "2014-08-25T15:30:00", title = "Task zz 6" });
            
            InterviewCalendarRepository _reporitory = new InterviewCalendarRepository(db);
            List<CalendarViewModel> list = new List<CalendarViewModel>();
            list = _reporitory.GetCalendarByProject(ProjectID, ProjectTournamentID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //Tìm theo Nhân viên phỏng vấn
        public ActionResult GetCalendarByEmployeeID(int? EmpID)
        {
            InterviewCalendarRepository _reporitory = new InterviewCalendarRepository(db);
            List<CalendarViewModel> list = new List<CalendarViewModel>();
            list = _reporitory.GetCalendarByEmployeeID(EmpID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}
