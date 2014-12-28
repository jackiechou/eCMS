using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Eagle.Model;
using Eagle.Core;
using Eagle.Repository;

using Eagle.Common.Extensions;

using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using System.IO;

using System.Data;
using Eagle.WebApp.Areas.Admin.Reports.PR;
using Eagle.Common.Settings;


namespace Eagle.WebApp.Areas.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class PIT02KKReportController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        public const int PAYROLL_MODULE_ID = 11;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../Payroll/PIT02KKReport/_Reset");
            }
            else
            {
                return View("../Payroll/PIT02KKReport/Index");
            }
        }               

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult _Create()
        {
            var account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (account == null)
            {
                return this.Content("Timeout");
            }
            var model = new PayrollSummaryViewModel();
            model.LSCompanyID = 0;
            this.CreateViewBag();

            return PartialView("../Payroll/PIT02KKReport/_Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Report(PayrollSummaryViewModel model, int? Years, string Quarters, int? TypeOfReport)
        {
            if (Years.HasValue)
            {
                model.Year = Years.Value;
            }
            if (!String.IsNullOrEmpty(Quarters))
            {
                model.Quarter = Quarters;
            }
            if (TypeOfReport.HasValue)
            {
                model.TypeOfReport = TypeOfReport.Value;
            }
            this.TempData["PIT02KK"] = model;
            this.ViewData["Report"] = new PIT02KKXtraReport();

            return this.PartialView("../Payroll/PIT02KKReport/PIT02KKReportViewer");
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateViewBag()
        {
            this.ViewBag.Quarters = new SelectList(this.GetQuartersOfYear(), "Value", "Text", 0);
            this.ViewBag.Years = new SelectList(this.GetListOfYear(), "Value", "Text", DateTime.Now.Year.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> GetQuartersOfYear()
        {
            var result = new List<SelectListItem>();
            if (this.LanguageId == 124)
            {
                result.Add(new SelectListItem() { Text = Eagle.Resource.LanguageResource.PleaseInputQuarterOfYear, Value = "0" });
                result.Add(new SelectListItem() { Text = "Quý 1", Value = "1, 2, 3" });
                result.Add(new SelectListItem() { Text = "Quý 2", Value = "4, 5, 6" });
                result.Add(new SelectListItem() { Text = "Quý 3", Value = "7, 8, 9" });
                result.Add(new SelectListItem() { Text = "Quý 4", Value = "10, 11, 12" });
            }
            else
            {
                result.Add(new SelectListItem() { Text = Eagle.Resource.LanguageResource.PleaseInputQuarterOfYear, Value = "0" });
                result.Add(new SelectListItem() { Text = "Quarter 1", Value = "1, 2, 3" });
                result.Add(new SelectListItem() { Text = "Quarter 2", Value = "4, 5, 6" });
                result.Add(new SelectListItem() { Text = "Quarter 3", Value = "7, 8, 9" });
                result.Add(new SelectListItem() { Text = "Quarter 4", Value = "10, 11, 12" });
            }
            
                        
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> GetListOfYear()
        {
            var result = new List<SelectListItem>();
            var year = DateTime.Now.Year;            
            for (int i = year - 10; i <= year + 10; i++)
            {
                result.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString() });
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        public ActionResult _Error(PayrollSummaryViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new PayrollSummaryViewModel();
            }
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;            
            this.CreateViewBag();
            return PartialView("../Payroll/PIT02KKReport/_Create", model);
        }

        #region CreatePIT02KKReport

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private PIT02KKXtraReport CreatePIT02KKReport(PayrollSummaryViewModel model)
        {            
            // Creating report                 

            var CompanyName = String.Empty;
            var mst = String.Empty;
            var found = this.db.LS_tblCompany.Where(p => p.LSCompanyID == model.LSCompanyID).FirstOrDefault();
            if (found != null)
            {
                CompanyName = found.Name;
                mst = found.TaxCode;
            }
            if (model.Year == 0)
            {
                model.Year = DateTime.Now.Year;
            }
            if (model.Quarter == null)
            {
                model.Quarter = String.Empty;
            }
            if (model.TypeOfReport == 1 && !String.IsNullOrEmpty(model.MonthYear))
            {
                var monthInfo = model.MonthYear.Substring(0, 2);
                var yearInfo = model.MonthYear.Substring(6, 4);
                if (Eagle.Common.Utilities.ValidatorUtils.CheckNumber(monthInfo) && Eagle.Common.Utilities.ValidatorUtils.CheckNumber(yearInfo))
                {
                    model.Quarter = monthInfo;
                    model.Month = Convert.ToInt16(monthInfo);
                    model.Year = Convert.ToInt16(yearInfo);
                }
            }
            // Setting header 
            var report = new PIT02KKXtraReport();            
            var errorMessage = String.Empty;
            var repository = new PayrollReportRepository(this.db);
            var result = repository.GetPIT02KKReport(model.LSCompanyID, model.Quarter, model.Year, out errorMessage);
            if (result != null && result.Count > 0)
            {
                var ds = GenericsToDataSet.ConvertGenericList(result);
                report.DataSource = ds;
                report.DataMember = "PR_sprpt_02KKTNCN";
                if (model.TypeOfReport == 1)
                {                    
                    
                    report.Bands["PageHeader"].Controls["xrLblMonthYear"].Text = String.Format(@"[01] Kỳ tính thuế : Tháng {0} Năm {1}", model.Month, model.Year);
                }
                else
                {
                    var QuarterName = String.Empty;
                    if (model.Quarter.Contains("3"))
                    {
                        QuarterName = "I";
                    }
                    else if (model.Quarter.Contains("6"))
                    {
                        QuarterName = "II";
                    }
                    else if (model.Quarter.Contains("9"))
                    {
                        QuarterName = "III";
                    }
                    else if (model.Quarter.Contains("12"))
                    {
                        QuarterName = "IV";
                    }
                    report.Bands["PageHeader"].Controls["xrLblMonthYear"].Text = String.Format(@"[01] Kỳ tính thuế : Quý {0} Năm {1}", QuarterName, model.Year);
                }                
                report.Bands["PageHeader"].Controls["xrLblCompanyName"].Text = String.Format("{0}", CompanyName);
                report.Bands["PageHeader"].Controls["xrLblMaSoThue"].Text = String.Format("{0}", mst);
            }

            return report;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadPIT02KKReportViewer()
        {
            var account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (account == null)
            {
                return this.Content("Timeout");
            }
            var model = this.TempData["PIT02KK"] as PayrollSummaryViewModel;
            if (model == null)
            {
                model = new PayrollSummaryViewModel();
            }
            this.TempData["PIT02KK"] = model;
            this.ViewData["Report"] = this.CreatePIT02KKReport(model);

            return this.PartialView("../Payroll/PIT02KKReport/PIT02KKReportViewer");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult ExportPIT02KKData()
        {
            var account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (account == null)
            {
                return this.Content("Timeout");
            }
            var model = this.TempData["PIT02KK"] as PayrollSummaryViewModel;
            if (model == null)
            {
                model = new PayrollSummaryViewModel();
            }
            this.TempData["PIT02KK"] = model;

            return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(this.CreatePIT02KKReport(model));
        }         
    }
}
