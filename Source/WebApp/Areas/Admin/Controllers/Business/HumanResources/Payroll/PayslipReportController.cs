using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Eagle.Model;
using Eagle.Core;
using Eagle.Repository;
using Eagle.WebApp.Areas.Admin.Reports;
using Eagle.Common.Extensions;

using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using System.IO;
using Eagle.Common.Settings;


namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class PayslipReportController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../Payroll/PayslipReport/_Reset");
            }
            else
            {
                return View("../Payroll/PayslipReport/Index");
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult _List(PayrollReportViewModel model, int? Years)
        {
            var account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (account == null)
            {
                return this.Content("Timeout");
            }
            var errorMessage = String.Empty;
            var repository = new PayrollReportRepository(this.db);
            if (!Years.HasValue)
            {
                Years = DateTime.Now.Year;
            }
            var listOfPayslip = repository.GetListOfEmployeePayroll(Convert.ToInt32(account.EmpId), Years.Value, out errorMessage);
            if (listOfPayslip == null)
            {
                return this._Error(model, errorMessage);
            }
            return this.PartialView("../Payroll/PayslipReport/_List", listOfPayslip);
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
            var model = new PayrollReportViewModel();
            model.EmpID = Convert.ToInt32(account.EmpId);
            var found = this.db.HR_tblEmp.Where(p => p.EmpID == model.EmpID).FirstOrDefault();
            if (found != null)
            {
                model.EmployeeName = String.Format("{0}{1}{2}", found.LastName, " ", found.FirstName);
                var company = this.db.LS_tblCompany.Single(p => p.LSCompanyID == found.LSCompanyID);
                if (company != null)
                {
                    model.CompanyName = company.Name;
                }                
                var position = this.db.LS_tblPosition.Single(p => p.LSPositionID == found.LSPositionID);
                if (position != null)
                {
                    model.PositionName = position.Name;
                }
            }            
            this.CreateViewBag();
            return PartialView("../Payroll/PayslipReport/_Create", model);
        }        

        /// <summary>
        /// 
        /// </summary>
        private void CreateViewBag()
        {
            var listOfYear = new List<int>();
            var year = DateTime.Now.Year;
            for (int i = year - 10; i <= year + 10; i++)
            {
                listOfYear.Add(i);
            }
            this.ViewBag.Years = new SelectList(listOfYear, year);                 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        public ActionResult _Error(PayrollReportViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new PayrollReportViewModel();
            }
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;            
            this.CreateViewBag();
            return PartialView("../Payroll/PayslipReport/_Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Report(PayrollReportViewModel model, int? EmpID, int? Month, int? Year)
        {
            if (EmpID.HasValue)
            {
                model.EmpID = EmpID.Value;
            }
            if (Month.HasValue)
            {
                model.Month = Month.Value;
            }
            if (Year.HasValue)
            {
                model.Year = Year.Value;
            }
            this.ViewData["Report"] = new PaySlipXtraReport();
            this.TempData["Payslip"] = model;

            return this.PartialView("../Payroll/PayslipReport/PayslipReportViewer");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private PaySlipXtraReport CreatePayslipReport(PayrollReportViewModel model)
        {
            var errorMessage = String.Empty;
            var repository = new PayrollReportRepository(this.db);                        
            var listOfPayslip = repository.GetOfEmployeePayroll(model.EmpID, model.Month, model.Year, out errorMessage);
            if (listOfPayslip == null)
            {
                return null;
            }
            if (listOfPayslip.Count == 0)
            {
                listOfPayslip.Add(new PR_sprptPaySlipReport_Result() { Month = DateTime.Now.Month, Year = DateTime.Now.Year });
            }
            var report = new PaySlipXtraReport();                                              
            
            // Setting HeaderPage
            report.Bands["PageHeader"].Controls["xrLbllPayslip"].Text = String.Format("{0}", Eagle.Resource.LanguageResource.Payslip);
            report.Bands["PageHeader"].Controls["xrLblMonthYear"].Text = String.Format("{0} : {1} - {2} : {3}", Eagle.Resource.LanguageResource.Month, model.Month, Eagle.Resource.LanguageResource.Year, model.Year);
            
            // Setting Detail
            report.Bands["Detail"].Controls["xrLblEmployeeCode"].Text = Eagle.Resource.LanguageResource.EmpCode;
            report.Bands["Detail"].Controls["xrLblEmployeeName"].Text = Eagle.Resource.LanguageResource.EmpName;
            report.Bands["Detail"].Controls["xrLabelDependant"].Text = Eagle.Resource.LanguageResource.PayslipDependant;
            report.Bands["Detail"].Controls["xrLblCompanyName"].Text = Eagle.Resource.LanguageResource.LastCompanyDefine;
            report.Bands["Detail"].Controls["xrLblPositionName"].Text = Eagle.Resource.LanguageResource.Position;
            report.Bands["Detail"].Controls["xrLblSalaryRate"].Text = Eagle.Resource.LanguageResource.SalaryRate;
            report.Bands["Detail"].Controls["xrLblPayDays"].Text = Eagle.Resource.LanguageResource.WorkDays;
            report.Bands["Detail"].Controls["xrLblCurrencyInfo"].Text = Eagle.Resource.LanguageResource.Currency;
            report.Bands["Detail"].Controls["xrLblInsuranceRate"].Text = Eagle.Resource.LanguageResource.InsuranceRate;
            report.Bands["Detail"].Controls["xrLblBasicSalary"].Text = Eagle.Resource.LanguageResource.Salary;
            report.Bands["Detail"].Controls["xrLblSalaryBeforeTax"].Text = Eagle.Resource.LanguageResource.PayslipSalaryBeforeTax;
            report.Bands["Detail"].Controls["xrLblOTIncome"].Text = Eagle.Resource.LanguageResource.PayslipOTIncome;
            report.Bands["Detail"].Controls["xrLblOtherTax"].Text = Eagle.Resource.LanguageResource.PayslipOther;
            report.Bands["Detail"].Controls["xrLblOTNoneTax"].Text = Eagle.Resource.LanguageResource.PayslipOT_NoneTax;
            report.Bands["Detail"].Controls["xrLblSalaryAfterTax"].Text = Eagle.Resource.LanguageResource.PayslipSalaryAfterTax;
            report.Bands["Detail"].Controls["xrLblOtherNoneTax"].Text = Eagle.Resource.LanguageResource.PayslipOther;
            report.Bands["Detail"].Controls["xrLblSI"].Text = Eagle.Resource.LanguageResource.PayslipSocialInsurance;
            report.Bands["Detail"].Controls["xrLblHI"].Text = Eagle.Resource.LanguageResource.PayslipHealthInsurance;
            report.Bands["Detail"].Controls["xrLblEI"].Text = Eagle.Resource.LanguageResource.PayslipUnEmployeeInsurance;
            report.Bands["Detail"].Controls["xrLblEmployeeIncome"].Text = Eagle.Resource.LanguageResource.PayslipEmployeeIncome;
            report.Bands["Detail"].Controls["xrLblPersonalRelief"].Text = Eagle.Resource.LanguageResource.PayslipPersonalRelief;
            report.Bands["Detail"].Controls["xrLblDependantRelief"].Text = Eagle.Resource.LanguageResource.PayslipDependantRelief;
            report.Bands["Detail"].Controls["xrLblPIT"].Text = Eagle.Resource.LanguageResource.PayslipPIT;
            report.Bands["Detail"].Controls["xrLblNetTakeHome"].Text = Eagle.Resource.LanguageResource.PayslipNetTakeHome;

            report.Bands["Detail"].Controls["xrLblTaxableIncome"].Text = String.Format("{0}.{1}", "A1", Eagle.Resource.LanguageResource.PayslipTaxableIncome);
            report.Bands["Detail"].Controls["xrLblNoneTaxableIncome"].Text = String.Format("{0}.{1}", "A2", Eagle.Resource.LanguageResource.PayslipNoneTaxableIncome);
            report.Bands["Detail"].Controls["xrLblNote"].Text = Eagle.Resource.LanguageResource.PayslipNote;

            report.Bands["Detail"].Controls["xrTable1"].Controls["xrTableRow1"].Controls["xrTableEmployeeIncome"].Text = String.Format("{0}.{1}", " A", Eagle.Resource.LanguageResource.PayslipEmployeeIncome);
            report.Bands["Detail"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableDeduction"].Text = String.Format("{0}.{1}", " B", Eagle.Resource.LanguageResource.PayslipDeduction);
            report.Bands["Detail"].Controls["xrTable5"].Controls["xrTableRow5"].Controls["xrTableTaxableIncome"].Text = String.Format("{0}.{1}", " C", Eagle.Resource.LanguageResource.PayslipTaxableIncome);
            report.Bands["Detail"].Controls["xrTable3"].Controls["xrTableRow3"].Controls["xrTablePIT"].Text = String.Format("{0}.{1}", " D", Eagle.Resource.LanguageResource.PayslipPIT);
            report.Bands["Detail"].Controls["xrTable4"].Controls["xrTableRow4"].Controls["xrTableNetTakeHome"].Text = String.Format("{0}.{1}", " E", Eagle.Resource.LanguageResource.PayslipNetTakeHome);

             
            var ds = GenericsToDataSet.ConvertGenericList(listOfPayslip);
            report.DataSource = ds;
            report.DataMember = "PR_sprptPaySlipReport_Result";

            return report;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadPayslipReportViewer()
        {
            var account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (account == null)
            {
                return this.Content("Timeout");
            }
            var model = this.TempData["Payslip"] as PayrollReportViewModel;
            if (model == null)
            {
                model = new PayrollReportViewModel();
            }
            model.EmpID = account.EmpId.Value;
            ViewData["Report"] = this.CreatePayslipReport(model);
            this.TempData["Payslip"] = model;

            return PartialView("../Payroll/PayslipReport/PayslipReportViewer");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult ExportPayslipData()
        {
            var model = this.TempData["Payslip"] as PayrollReportViewModel;
            if (model == null)
            {
                model = new PayrollReportViewModel();
            }
            this.TempData["Payslip"] = model;
            return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(this.CreatePayslipReport(model));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="EmpID"></param>
        /// <param name="Month"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public ActionResult ViewReport(int? EmpID, int? Month, int? Year)
        {                        
            this.ViewData["Report"] = new PaySlipXtraReport();                             
            var report = this.CreatePayslipReportFromQueryString(EmpID, Month, Year);
            var stream = new System.IO.MemoryStream();
            report.ExportToPdf(stream, new PdfExportOptions());
            stream.Seek(0, SeekOrigin.Begin);
            return this.File(stream, "application/pdf");                        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="EmpID"></param>
        /// <param name="Month"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public ActionResult ViewReportForSaveAs(int? EmpID, int? Month, int? Year)
        {
            this.ViewData["Report"] = new PaySlipXtraReport();
            var report = this.CreatePayslipReportFromQueryString(EmpID, Month, Year);
            var stream = new System.IO.MemoryStream();
            report.ExportToPdf(stream);
            stream.Seek(0, SeekOrigin.Begin);                                 
            return this.File(stream, "application/pdf", "Payslip.pdf");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmpID"></param>
        /// <param name="Month"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        private PaySlipXtraReport CreatePayslipReportFromQueryString(int? EmpID, int? Month, int? Year)
        {
            var errorMessage = String.Empty;
            var repository = new PayrollReportRepository(this.db);
            var listOfPayslip = repository.GetOfEmployeePayroll(EmpID.HasValue ? EmpID.Value : 0, Month.HasValue ? Month.Value : -1, Year.HasValue ? Year.Value : -1, out errorMessage);
            if (listOfPayslip == null)
            {
                return null;
            }
            if (listOfPayslip.Count == 0)
            {
                listOfPayslip.Add(new PR_sprptPaySlipReport_Result() { Month = DateTime.Now.Month, Year = DateTime.Now.Year });
            }
            var report = new PaySlipXtraReport();

            // Setting HeaderPage
            report.Bands["PageHeader"].Controls["xrLbllPayslip"].Text = String.Format("{0}", Eagle.Resource.LanguageResource.Payslip);
            report.Bands["PageHeader"].Controls["xrLblMonthYear"].Text = String.Format("{0} : {1} - {2} : {3}", Eagle.Resource.LanguageResource.Month, Month.HasValue ? Month.Value.ToString() : String.Empty, Eagle.Resource.LanguageResource.Year, Year.HasValue ? Year.Value.ToString() : String.Empty);

            // Setting Detail
            report.Bands["Detail"].Controls["xrLblEmployeeCode"].Text = Eagle.Resource.LanguageResource.EmpCode;
            report.Bands["Detail"].Controls["xrLblEmployeeName"].Text = Eagle.Resource.LanguageResource.EmpName;
            report.Bands["Detail"].Controls["xrLabelDependant"].Text = Eagle.Resource.LanguageResource.PayslipDependant;
            report.Bands["Detail"].Controls["xrLblCompanyName"].Text = Eagle.Resource.LanguageResource.LastCompanyDefine;
            report.Bands["Detail"].Controls["xrLblPositionName"].Text = Eagle.Resource.LanguageResource.Position;
            report.Bands["Detail"].Controls["xrLblSalaryRate"].Text = Eagle.Resource.LanguageResource.SalaryRate;
            report.Bands["Detail"].Controls["xrLblPayDays"].Text = Eagle.Resource.LanguageResource.WorkDays;
            report.Bands["Detail"].Controls["xrLblCurrencyInfo"].Text = Eagle.Resource.LanguageResource.Currency;
            report.Bands["Detail"].Controls["xrLblInsuranceRate"].Text = Eagle.Resource.LanguageResource.InsuranceRate;
            report.Bands["Detail"].Controls["xrLblBasicSalary"].Text = Eagle.Resource.LanguageResource.Salary;
            report.Bands["Detail"].Controls["xrLblSalaryBeforeTax"].Text = Eagle.Resource.LanguageResource.PayslipSalaryBeforeTax;
            report.Bands["Detail"].Controls["xrLblOTIncome"].Text = Eagle.Resource.LanguageResource.PayslipOTIncome;
            report.Bands["Detail"].Controls["xrLblOtherTax"].Text = Eagle.Resource.LanguageResource.PayslipOther;
            report.Bands["Detail"].Controls["xrLblOTNoneTax"].Text = Eagle.Resource.LanguageResource.PayslipOT_NoneTax;
            report.Bands["Detail"].Controls["xrLblSalaryAfterTax"].Text = Eagle.Resource.LanguageResource.PayslipSalaryAfterTax;
            report.Bands["Detail"].Controls["xrLblOtherNoneTax"].Text = Eagle.Resource.LanguageResource.PayslipOther;
            report.Bands["Detail"].Controls["xrLblSI"].Text = Eagle.Resource.LanguageResource.PayslipSocialInsurance;
            report.Bands["Detail"].Controls["xrLblHI"].Text = Eagle.Resource.LanguageResource.PayslipHealthInsurance;
            report.Bands["Detail"].Controls["xrLblEI"].Text = Eagle.Resource.LanguageResource.PayslipUnEmployeeInsurance;
            report.Bands["Detail"].Controls["xrLblEmployeeIncome"].Text = Eagle.Resource.LanguageResource.PayslipEmployeeIncome;
            report.Bands["Detail"].Controls["xrLblPersonalRelief"].Text = Eagle.Resource.LanguageResource.PayslipPersonalRelief;
            report.Bands["Detail"].Controls["xrLblDependantRelief"].Text = Eagle.Resource.LanguageResource.PayslipDependantRelief;
            report.Bands["Detail"].Controls["xrLblPIT"].Text = Eagle.Resource.LanguageResource.PayslipPIT;
            report.Bands["Detail"].Controls["xrLblNetTakeHome"].Text = Eagle.Resource.LanguageResource.PayslipNetTakeHome;

            report.Bands["Detail"].Controls["xrLblTaxableIncome"].Text = String.Format("{0}.{1}", "A1", Eagle.Resource.LanguageResource.PayslipTaxableIncome);
            report.Bands["Detail"].Controls["xrLblNoneTaxableIncome"].Text = String.Format("{0}.{1}", "A2", Eagle.Resource.LanguageResource.PayslipNoneTaxableIncome);
            report.Bands["Detail"].Controls["xrLblNote"].Text = Eagle.Resource.LanguageResource.PayslipNote;

            report.Bands["Detail"].Controls["xrTable1"].Controls["xrTableRow1"].Controls["xrTableEmployeeIncome"].Text = String.Format("{0}.{1}", " A", Eagle.Resource.LanguageResource.PayslipEmployeeIncome);
            report.Bands["Detail"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableDeduction"].Text = String.Format("{0}.{1}", " B", Eagle.Resource.LanguageResource.PayslipDeduction);
            report.Bands["Detail"].Controls["xrTable5"].Controls["xrTableRow5"].Controls["xrTableTaxableIncome"].Text = String.Format("{0}.{1}", " C", Eagle.Resource.LanguageResource.PayslipTaxableIncome);
            report.Bands["Detail"].Controls["xrTable3"].Controls["xrTableRow3"].Controls["xrTablePIT"].Text = String.Format("{0}.{1}", " D", Eagle.Resource.LanguageResource.PayslipPIT);
            report.Bands["Detail"].Controls["xrTable4"].Controls["xrTableRow4"].Controls["xrTableNetTakeHome"].Text = String.Format("{0}.{1}", " E", Eagle.Resource.LanguageResource.PayslipNetTakeHome);


            var ds = GenericsToDataSet.ConvertGenericList(listOfPayslip);
            report.DataSource = ds;
            report.DataMember = "PR_sprptPaySlipReport_Result";

            return report;
        }
    }
}
