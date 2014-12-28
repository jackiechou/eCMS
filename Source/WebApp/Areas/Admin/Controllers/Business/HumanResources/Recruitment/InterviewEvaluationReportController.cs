using Eagle.Common.Extensions;
using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.WebApp.Areas.Admin.Reports.REC.InterviewEvaluationReport;
using Eagle.Core;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class InterviewEvaluationReportController : BaseController
    {
        //
        // GET: /Admin/InterviewEvaluationReport/

        public ActionResult _Create()
        {
            CreateViewBag();
            return PartialView("../Business/HumanResources/REC/InterviewEvaluationReport/_Create");
        }

        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            return View("../Business/HumanResources/REC/InterviewEvaluationReport/Index");
        }
        //public ActionResult Report()
        //{
        //    return this.PartialView("../Business/HumanResources/REC/InterviewEvaluationReport/_Report");
        //}

        public ActionResult ReportViewerPartial(int? ProjectID, int? ProjectTournamentID, string strProject, string strProjectTournament, bool Mode, bool isCallBack)
        {
            ViewBag.ProjectID = ProjectID;
            ViewBag.strProject = strProject;
            ViewBag.ProjectTournamentID = ProjectTournamentID;
            ViewBag.strProjectTournament = strProjectTournament;
            ViewBag.Mode = Mode;
            if (ProjectID == 0 || ProjectID == null)
            {
                ViewData["Report"] = new rptInterviewEvaluation();
            }
            else
            {
                ViewData["Report"] = CreateTrainingPlanReport(ProjectID, strProject, ProjectTournamentID, strProjectTournament, Mode, isCallBack);
            }

            return PartialView("../Business/HumanResources/REC/InterviewEvaluationReport/ReportViewerPartial");
        }
        public ActionResult ExportReportViewerPartial(int? ProjectID, int? ProjectTournamentID, string strProject, string strProjectTournament, bool Mode, bool isCallBack)
        {
            ViewBag.ProjectID = ProjectID;
            ViewBag.strProject = strProject;
            ViewBag.ProjectTournamentID = ProjectTournamentID;
            ViewBag.strProjectTournament = strProjectTournament;
            ViewBag.Mode = Mode;

            var report = CreateTrainingPlanReport(ProjectID, strProject, ProjectTournamentID, strProjectTournament, Mode, isCallBack);
            return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(report);
        }
        private rptInterviewEvaluation CreateTrainingPlanReport(int? ProjectID, string strProject, int? ProjectTournamentID,string strProjectTournament, bool Mode, bool isCallBack = true)
        {
            rptInterviewEvaluation report = new rptInterviewEvaluation();
            if (isCallBack)
            {
                #region Setting header
                //Title
                report.Bands["reportHeaderBand1"].Controls["lblTitle"].Text = Eagle.Resource.LanguageResource.InterviewEvaluationReport;
                //Date
                report.Bands["reportHeaderBand1"].Controls["lblDate"].Text = Eagle.Resource.LanguageResource.Date + ":";
                report.Bands["reportHeaderBand1"].Controls["lblDateText"].Text = DateTime.Now.ToString("dd/MM/yyyy");
                //Project
                report.Bands["reportHeaderBand1"].Controls["lblProjectLabel"].Text = Eagle.Resource.LanguageResource.RecruitmentProject2 + ":";
                report.Bands["reportHeaderBand1"].Controls["lblProjectText"].Text = strProject;
                //Tournament
                if (ProjectTournamentID != null && ProjectTournamentID != 0)
                {
                    report.Bands["reportHeaderBand1"].Controls["lblTournamentLabel"].Text = Eagle.Resource.LanguageResource.Tournament + ":";
                    report.Bands["reportHeaderBand1"].Controls["lblTournamentText"].Text = strProjectTournament;
                }
                else
                {
                    report.Bands["reportHeaderBand1"].Controls["lblTournamentLabel"].Text = "";
                    report.Bands["reportHeaderBand1"].Controls["lblTournamentText"].Text = "";
                }
                //Page Header Band
                report.Bands["pageHeaderBand1"].Controls["xrTable1"].Controls["xrTableRow3"].Controls["xrTableSEQ"].Text = Eagle.Resource.LanguageResource.Seq2;
                report.Bands["pageHeaderBand1"].Controls["xrTable1"].Controls["xrTableRow3"].Controls["xrTableCandidate"].Text = Eagle.Resource.LanguageResource.Candidate;
                report.Bands["pageHeaderBand1"].Controls["xrTable1"].Controls["xrTableRow3"].Controls["xrTableResults"].Text = Eagle.Resource.LanguageResource.Result;
                report.Bands["pageHeaderBand1"].Controls["xrTable1"].Controls["xrTableRow3"].Controls["xrTableStatus"].Text = Eagle.Resource.LanguageResource.Status;
                
                
                #endregion
                #region bind datasource
                InterviewEvaluationRepository _repository = new InterviewEvaluationRepository(db);
                List<REC_sprptInterviewEvaluation_Result> list = _repository.GetResults(ProjectID,ProjectTournamentID,LanguageId);
                DataSet ds = GenericsToDataSet.ConvertGenericList(list);
                report.DataSource = ds;
                report.DataMember = "REC_sprptInterviewEvaluation";
                #endregion
            }

            return report;

        }
       
 

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

    }
}
