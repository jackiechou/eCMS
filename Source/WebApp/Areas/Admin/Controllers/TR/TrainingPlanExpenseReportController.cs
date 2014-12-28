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
    public class TrainingPlanExpenseReportController : BaseController
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
                return this.PartialView("../TR/TrainingPlanExpenseReport/Reset");
            }
            else
            {
                return this.View("../TR/TrainingPlanExpenseReport/Index");
            }
          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var model = new TR_sprptTrainingPlanExpense_Result();            
            return this.PartialView("../TR/TrainingPlanExpenseReport/Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Report(TR_sprptTrainingPlanExpense_Result model)
        {
            this.ViewData["Report"] = new TrainingPlanExpenseXtraReport();
            this.TempData["TrainingRequest"] = model;
        
            return this.PartialView("../TR/TrainingPlanExpenseReport/TrainingPlanExpenseReportViewer");
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateViewBag(TR_sprptTrainingPlanExpense_Result model)
        {            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private TrainingPlanExpenseXtraReport CreateTrainingPlanExpenseReport(TR_sprptTrainingPlanExpense_Result model)
        {
            var errorMessage = String.Empty;                        
            var repository = new TrainingPlanRepository(this.db);
            if (!model.LSTrainingCodeID.HasValue)
            {
                model.LSTrainingCodeID = 0;
            }
            if (!model.LSTrainingCourseID.HasValue)
            {
                model.LSTrainingCourseID = 0;
            }
            var listOfTrainingPlanExpenseReport = repository.GetListOfTrainingPlanExpenseForReport(model, out errorMessage);
            if (listOfTrainingPlanExpenseReport == null)
            {
                return null;
            }           
            var report = new TrainingPlanExpenseXtraReport();
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
            report.Bands["ReportHeader"].Controls["xrLblTrainingRequestEvaluation"].Text = String.Format("{0}", Eagle.Resource.LanguageResource.TrainingRequestExpenseReport);
            report.Bands["ReportHeader"].Controls["xrLblTrainingCode"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.TrainingCodeName, TrainingCodeName);
            report.Bands["ReportHeader"].Controls["xrLblTrainingCourse"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.TrainingCourseName, TrainingCourseName);

            // Setting Header of List
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellSEQ"].Text = Eagle.Resource.LanguageResource.SEQ;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellTrainingCode"].Text = Eagle.Resource.LanguageResource.TrainingCode;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellTrainingCourse"].Text = Eagle.Resource.LanguageResource.TrainingCourse;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellNumStaff"].Text = Eagle.Resource.LanguageResource.TrainingPlanNumberOfEmployees;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellDurationInfo"].Text = Eagle.Resource.LanguageResource.TrainingRequestDuration;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellExpenseOfPlan"].Text = Eagle.Resource.LanguageResource.TrainingExpenseOfPlan;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellExpenseOfRequest"].Text = Eagle.Resource.LanguageResource.TrainingExpenseOfRequest;

            var ds = GenericsToDataSet.ConvertGenericList(listOfTrainingPlanExpenseReport);            
            report.DataSource = ds;
            report.DataMember = "TR_sprptTrainingPlanExpense";

            return report;            
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadTrainingPlanExpenseReportViewer()
        {                        
            var model = this.TempData["TrainingRequest"] as TR_sprptTrainingPlanExpense_Result;
            if (model == null)
            {
                model = new TR_sprptTrainingPlanExpense_Result();
            }
            ViewData["Report"] = this.CreateTrainingPlanExpenseReport(model);
            this.TempData["TrainingRequest"] = model;
            
            return PartialView("../TR/TrainingPlanExpenseReport/TrainingPlanExpenseReportViewer");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult ExportTrainingPlanExpenseData()
        {
            var model = this.TempData["TrainingRequest"] as TR_sprptTrainingPlanExpense_Result;
            if (model == null)
            {
                model = new TR_sprptTrainingPlanExpense_Result();
            }
            this.TempData["TrainingRequest"] = model;
            return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(this.CreateTrainingPlanExpenseReport(model));
        }
         
    }


}
