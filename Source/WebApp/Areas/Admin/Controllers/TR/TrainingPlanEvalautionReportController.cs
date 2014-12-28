using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web;
using DevExpress.XtraReports.UI;

using Eagle.Repository;
using Eagle.Model;
using Eagle.Core;
using Eagle.Common.Extensions;


namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class TrainingPlanEvaluationReportController : BaseController
    {        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            this.TempData.Clear();
            if (this.Request.IsAjaxRequest())
            {
                return this.PartialView("../TR/TrainingPlanEvaluationReport/Reset");
            }
            else
            {
                return this.View("../TR/TrainingPlanEvaluationReport/Index");
            }
          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var model = new TrainingPlanEvaluationResultViewModel();
            this.CreateViewBag(model);
            return this.PartialView("../TR/TrainingPlanEvaluationReport/Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Report(TrainingPlanEvaluationResultViewModel model, int? EvaluationRequired, int? EmployeeEvaluation)
        {
            model.EvaluationRequiredValue = EvaluationRequired.HasValue ? EvaluationRequired.Value : 2;
            model.EmployeeEvaluationValue = EmployeeEvaluation.HasValue ? EmployeeEvaluation.Value : 2;
            
            this.ViewData["Report"] = new TrainingPlanEvaluationXtraReport();
            this.TempData["TrainingRequest"] = model;
        
            return this.PartialView("../TR/TrainingPlanEvaluationReport/TrainingPlanEvaluationReportViewer");
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateViewBag(TR_sprptTrainingPlanEvaluation_Result model)
        {
            this.ViewBag.EvaluationRequired = new SelectList(this.ListOfEvaluationRequiredInfo(), "Value", "Key", 2);
            this.ViewBag.EmployeeEvaluation = new SelectList(this.ListOfEmployeeEvaluationInfo(), "Value", "Key", 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<EvaluationInfo> ListOfEvaluationRequiredInfo()
        {
            var list = new List<EvaluationInfo>();
            list.Add(new EvaluationInfo { Key = Eagle.Resource.LanguageResource.TrainingRequestEvaluationRequired_All, Value = 2 });
            list.Add(new EvaluationInfo { Key = Eagle.Resource.LanguageResource.TrainingRequestEvaluationRequired_Required, Value = 1 });
            list.Add(new EvaluationInfo { Key = Eagle.Resource.LanguageResource.TrainingRequestEvaluationRequired_NotRequired, Value = 0 });
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<EvaluationInfo> ListOfEmployeeEvaluationInfo()
        {
            var list = new List<EvaluationInfo>();
            list.Add(new EvaluationInfo { Key = Eagle.Resource.LanguageResource.TrainingRequestEmployeeEvaluation_All, Value = 2 });
            list.Add(new EvaluationInfo { Key = Eagle.Resource.LanguageResource.TrainingRequestEmployeeEvaluation_Evaluate, Value = 1 });
            list.Add(new EvaluationInfo { Key = Eagle.Resource.LanguageResource.TrainingRequestEmployeeEvaluation_NotEvaluate, Value = 0 });
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private TrainingPlanEvaluationXtraReport CreateTrainingPlanEvaluationReport(TrainingPlanEvaluationResultViewModel model)
        {
            var errorMessage = String.Empty;
            var checking = model.IsEmployeeEvaluation;            
            var repository = new TrainingPlanRepository(this.db);
            var listOfTrainingPlanEvaluationReport = repository.GetListOfTrainingPlanEvaluationForReport(model, out errorMessage);
            if (listOfTrainingPlanEvaluationReport == null)
            {
                return null;
            }           
            var report = new TrainingPlanEvaluationXtraReport();
            // Setting header 
            string TrainingCodeName = String.Empty;
            string TrainingCourseName = String.Empty;
            if (model.LSTrainingCodeID.HasValue)
            {
                var obj = this.db.LS_tblTrainingCode.Where(p => p.LSTrainingCodeID == model.LSTrainingCodeID.Value).FirstOrDefault();
                if (obj != null)
                {
                    TrainingCodeName = obj.Name;
                }
            }
            if (model.LSTrainingCourseID.HasValue)
            {
                var obj = this.db.LS_tblTrainingCourse.Where(p => p.LSTrainingCourseID == model.LSTrainingCourseID).FirstOrDefault();
                if (obj != null)
                {
                    TrainingCourseName = obj.Name;
                }
            }
            report.Bands["ReportHeader"].Controls["xrLblTrainingRequestEvaluation"].Text = String.Format("{0}", Eagle.Resource.LanguageResource.TrainingRequestEvaluation);
            report.Bands["ReportHeader"].Controls["xrLblTrainingCode"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.TrainingCodeName, TrainingCodeName);
            report.Bands["ReportHeader"].Controls["xrLblTrainingCourse"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.TrainingCourseName, TrainingCourseName);
            report.Bands["ReportHeader"].Controls["xrLblFullName"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.Employee, model.FullName);
            // Setting Header of List
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellSEQ"].Text = Eagle.Resource.LanguageResource.SEQ;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellTrainingEmployee"].Text = Eagle.Resource.LanguageResource.TrainingEmployee;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellTrainingCode"].Text = Eagle.Resource.LanguageResource.TrainingCode;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellTrainingCourse"].Text = Eagle.Resource.LanguageResource.TrainingCourse;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellDurationFrom"].Text = Eagle.Resource.LanguageResource.TrainingRequestDurationFrom;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellDurationTo"].Text = Eagle.Resource.LanguageResource.TrainingRequestDurationTo;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellEvalRequired"].Text = Eagle.Resource.LanguageResource.TrainingRequestEvaluationRequired;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellEmpEvaluation"].Text = Eagle.Resource.LanguageResource.TrainingEvaluationEmployee;
            var ds = GenericsToDataSet.ConvertGenericList(listOfTrainingPlanEvaluationReport);            
            report.DataSource = ds;
            report.DataMember = "TR_sprptTrainingPlanEvaluation";

            return report;            
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadTrainingPlanEvaluationReportViewer()
        {                        
            var model = this.TempData["TrainingRequest"] as TrainingPlanEvaluationResultViewModel;
            if (model == null)
            {
                model = new TrainingPlanEvaluationResultViewModel();
            }
            ViewData["Report"] = this.CreateTrainingPlanEvaluationReport(model);
            this.TempData["TrainingRequest"] = model;
            
            return PartialView("../TR/TrainingPlanEvaluationReport/TrainingPlanEvaluationReportViewer");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult ExportTrainingPlanEvaluationData()
        {
            var model = this.TempData["TrainingRequest"] as TrainingPlanEvaluationResultViewModel;
            if (model == null)
            {
                model = new TrainingPlanEvaluationResultViewModel();
            }
            this.TempData["TrainingRequest"] = model;
            return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(this.CreateTrainingPlanEvaluationReport(model));
        }
        
        /// <summary>
        /// 
        /// </summary>
        public class EvaluationInfo
        {
            public string Key { get; set; }
            public int Value { get; set; }
        }
    }    
}
