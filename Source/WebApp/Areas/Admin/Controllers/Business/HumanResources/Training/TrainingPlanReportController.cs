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
    public class TrainingPlanReportController : BaseController
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
                return this.PartialView("../Business/HumanResources/Training/TrainingPlanReport/Reset");
            }
            else
            {
                return this.View("../Business/HumanResources/Training/TrainingPlanReport/Index");
            }
          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var model = new TR_sprptTrainingPlan_Result();
            model.PlanYear = DateTime.Now.Year;
            model.PlanMonth = DateTime.Now.Month;
            this.CreateViewBag(model);
            return this.PartialView("../Business/HumanResources/Training/TrainingPlanReport/Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Report()
        {
            return this.PartialView("../Business/HumanResources/Training/TrainingPlanReport/TrainingPlanReportViewer");
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateViewBag(TR_sprptTrainingPlan_Result model)
        {
            List<int> months = new List<int>();
            for (int i = 1; i <= 12; i++)
            {
                months.Add(i);
            }
            ViewBag.Month = new SelectList(months, model.PlanMonth);
            var years = new List<int>();
            for (int i = DateTime.Now.Year - 10; i <= DateTime.Now.Year + 10; i++)
            {
                years.Add(i);
            }
            this.ViewBag.Year = new SelectList(years, model.PlanYear);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private TrainingPlanXtraReport CreateTrainingPlanReport(int Month, int Year)
        {
            var errorMessage = String.Empty;
            var repository = new TrainingPlanRepository(this.db);
            var listOfTrainingPlanReport = repository.GetListOfTrainingPlanForReport(Year, Month, out errorMessage);
            if (listOfTrainingPlanReport == null)
            {
                return null;
            }           
            var report = new TrainingPlanXtraReport();            
            // Setting header
            report.Bands["ReportHeader"].Controls["xrLbllTrainingPlan"].Text = String.Format("{0}", Eagle.Resource.LanguageResource.TrainingPlan);
            report.Bands["ReportHeader"].Controls["xrLbllMonth"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.TrainingPlanMonth, Month);
            report.Bands["ReportHeader"].Controls["xrLblYear"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.TrainingPlanYear, Year);

            // Setting Header of List
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellSEQ"].Text = Eagle.Resource.LanguageResource.SEQ;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellTrainingCode"].Text = Eagle.Resource.LanguageResource.TrainingCode;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellTrainingCourse"].Text = Eagle.Resource.LanguageResource.TrainingCourse;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellTrainingCategory"].Text = Eagle.Resource.LanguageResource.TrainingCategory;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellTrainingLocation"].Text = Eagle.Resource.LanguageResource.TrainingLocation;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellDays"].Text = Eagle.Resource.LanguageResource.TrainingPlanDay;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellHours"].Text = Eagle.Resource.LanguageResource.TrainingPlanHours;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellNumStaff"].Text = Eagle.Resource.LanguageResource.TrainingPlanNumberOfEmployees;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellCost"].Text = Eagle.Resource.LanguageResource.TrainingRequestCost;

            var ds = GenericsToDataSet.ConvertGenericList(listOfTrainingPlanReport);            
            report.DataSource = ds;
            report.DataMember = "TR_sprptTrainingPlan";

            return report;            
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadTrainingPlanReportViewer(int? Month, int? Year)        
        {
            if (!Month.HasValue)
            {
                Month = DateTime.Now.Month;
            }
            if (!Year.HasValue)
            {
                Year = DateTime.Now.Year;
            }
            ViewBag.Month = Month;
            ViewBag.Year = Year;
            this.TempData["Month"] = Month;
            this.TempData["Year"] = Year;
            ViewData["Report"] = this.CreateTrainingPlanReport(Month.Value, Year.Value);            
            return PartialView("../Business/HumanResources/Training/TrainingPlanReport/TrainingPlanReportViewer");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult ExportTrainingPlanData(int? Month, int? Year)
        {
            if (!Month.HasValue)
            {
                Month = DateTime.Now.Month;
            }
            if (!Year.HasValue)
            {
                Year = DateTime.Now.Year;
            }                     
            Month = (int)this.TempData["Month"];
            Year = (int)this.TempData["Year"];
            this.TempData["Month"] = Month;
            this.TempData["Year"] = Year;
            return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(this.CreateTrainingPlanReport(Month.Value, Year.Value));
        }
    }


}
