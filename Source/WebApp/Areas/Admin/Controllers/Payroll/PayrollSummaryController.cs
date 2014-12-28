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

using Eagle.WebApp.Areas.Admin.Reports.PR.PaySlipSummary;
using System.Data;
using Eagle.Common.Settings;


namespace Eagle.WebApp.Areas.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class PayrollSummaryController : BaseController
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
                return PartialView("../Payroll/PayrollSummaryReport/_Reset");
            }
            else
            {
                return View("../Payroll/PayrollSummaryReport/Index");
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult _List(PayrollSummaryViewModel model)
        {
            var account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (account == null)
            {
                return this.Content("Timeout");
            }
            if (!String.IsNullOrEmpty(model.MonthYear))
            {
                var MonthInfo = model.MonthYear.Substring(0, 2);
                var YearInfo = model.MonthYear.Substring(6, 4); 
                model.Month = -1;
                model.Year = -1;
                if (Eagle.Common.Utilities.ValidatorUtils.CheckNumber(MonthInfo))
                {
                    model.Month = Convert.ToInt16(MonthInfo);
                }
                if (Eagle.Common.Utilities.ValidatorUtils.CheckNumber(YearInfo))
                {
                    model.Year = Convert.ToInt16(YearInfo);
                }
            }
            var errorMessage = String.Empty;
            var repository = new PayrollReportRepository(this.db);
            var listOfPayrollSummary = repository.GetPayrollSummary(account.UserName, PAYROLL_MODULE_ID, model, out errorMessage);
            if (listOfPayrollSummary == null)
            {
                return this._Error(model, errorMessage);
            }
            return this.PartialView("../Payroll/PayrollSummaryReport/_List", listOfPayrollSummary);
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
            model.LSPositionID = 0;
            model.PositionName = Eagle.Resource.LanguageResource.PleaseInputPositionName;
            this.CreateViewBag();
            return PartialView("../Payroll/PayrollSummaryReport/_Create", model);
        }        

        /// <summary>
        /// 
        /// </summary>
        private void CreateViewBag()
        {            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LSCompanyID"></param>
        /// <param name="LSPositionID"></param>
        /// <param name="EmployeeCode"></param>
        /// <param name="EmployeeName"></param>
        /// <param name="MonthYear"></param>
        /// <returns></returns>
        public ActionResult ViewReport(int? LSCompanyID, int? LSPositionID, string EmployeeCode, string EmployeeName, string MonthYear)
        {
            var account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (account == null)
            {
                return this.Content("Timeout");
            }
            var report = this.CreatePayrollSummaryReport(account.UserName, PAYROLL_MODULE_ID, LSCompanyID, LSPositionID, EmployeeCode, EmployeeName, MonthYear);
            var stream = new System.IO.MemoryStream();
            report.ExportToPdf(stream, new PdfExportOptions());
            stream.Seek(0, SeekOrigin.Begin);
            return this.File(stream, "application/pdf");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="ModuleID"></param>
        /// <param name="LSCompanyID"></param>
        /// <param name="LSPositionID"></param>
        /// <param name="EmployeeCode"></param>
        /// <param name="EmployeeName"></param>
        /// <param name="MonthYear"></param>
        /// <returns></returns>
        private PayrollSummaryXtraReport CreatePayrollSummaryReport(string UserName, int ModuleID, int? LSCompanyID, int? LSPositionID, string EmployeeCode, string EmployeeName, string MonthYear)
        {
            var errorMessage = String.Empty;
            var PayslipPersonalRelief = String.Empty;
            var PayslipDependantRelief = String.Empty;

            var listOfMaster = new List<PR_sprptPayrollSummaryReport_Master_Result>();
            var listOfPayroll = new List<PR_sprptPayrollSummaryReport_Result>();

            var repository = new PayrollReportRepository(this.db);            
            listOfPayroll = repository.GetPayrollSummaryReport(UserName, ModuleID, LSCompanyID, LSPositionID, EmployeeCode, EmployeeName, MonthYear, out errorMessage); 
            if (listOfPayroll.Count == 0)
            {
                listOfPayroll.Add(new PR_sprptPayrollSummaryReport_Result() { LSCompanyID = 0 });                
                listOfMaster.Add(new PR_sprptPayrollSummaryReport_Master_Result() { LSCompanyID = 0, Name = String.Empty });
            }
            else 
            {
                PayslipDependantRelief = listOfPayroll[0].DependantRelief;
                PayslipPersonalRelief = listOfPayroll[0].SelfDeduction;

                listOfMaster = (from detail in listOfPayroll
                                group detail by new { detail.LSCompanyID } into Master
                                select new PR_sprptPayrollSummaryReport_Master_Result()
                                {
                                    LSCompanyID = Master.Key.LSCompanyID.HasValue ? Master.Key.LSCompanyID.Value : 0
                                }).ToList();

                if (listOfMaster != null)
                {
                    foreach (var item in listOfMaster)
                    {
                        var obj = this.db.LS_tblCompany.Where(p => p.LSCompanyID == item.LSCompanyID).FirstOrDefault();
                        if (obj != null)
                        {
                            item.Name = obj.Name;
                        }
                    }
                }
            }
            var report = new PayrollSummaryXtraReport();
            var dsMasterDetail = GenericsToDataSet.ConvertGenericList("PayrollSummary", listOfMaster, listOfPayroll, "PR_sprptPayrollSummaryReport_Master", "PR_sprptPayrollSummaryReport");                        

            report.DataSource = dsMasterDetail;
            report.DataMember = dsMasterDetail.Tables[0].TableName;
            report.DetailReport.DataSource = dsMasterDetail;
            report.DetailReport.DataMember = String.Format("{0}.{1}", dsMasterDetail.Tables[0].TableName, dsMasterDetail.Relations[0].RelationName);

            //// Setting header             
            report.Bands["ReportHeader"].Controls["xrLblPayrollSummary"].Text = String.Format("{0}", Eagle.Resource.LanguageResource.PayrollSummary);
            if (String.IsNullOrEmpty(MonthYear))
            {
                report.Bands["ReportHeader"].Controls["xrLblMonthYear"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.MonthYear, MonthYear);
            }
            else
            {
                report.Bands["ReportHeader"].Controls["xrLblMonthYear"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.MonthYear, MonthYear.Substring(0, 2) + "/" + MonthYear.Substring(6, 4));
            }
            

            report.Bands["ReportHeader"].Controls["xrLblPersonalRelief"].Text = String.Format("{0}", Eagle.Resource.LanguageResource.PayslipPersonalRelief);
            report.Bands["ReportHeader"].Controls["xrLblDependantRelief"].Text = String.Format("{0}", Eagle.Resource.LanguageResource.PayslipDependantRelief);
            report.Bands["ReportHeader"].Controls["xrLblPersonalReliefValue"].Text = String.Format("{0}", PayslipPersonalRelief);
            report.Bands["ReportHeader"].Controls["xrLblDependantReliefValue"].Text = String.Format("{0}", PayslipDependantRelief);

            //// Setting Header of List
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellSEQ"].Text = Eagle.Resource.LanguageResource.SEQ;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellEmployeeName"].Text = Eagle.Resource.LanguageResource.Employee;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellPositionName"].Text = Eagle.Resource.LanguageResource.Position;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellStandardWorkingDay"].Text = Eagle.Resource.LanguageResource.StandardWorking;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellActualWorkingDay"].Text = Eagle.Resource.LanguageResource.WorkDays;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellSalary"].Text = Eagle.Resource.LanguageResource.Salary;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellSalaryInsurance"].Text = Eagle.Resource.LanguageResource.PayslipSocialInsurance;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellSalaryRate"].Text = Eagle.Resource.LanguageResource.PayslipSalaryRate;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellInsuranceRate"].Text = Eagle.Resource.LanguageResource.PayslipInsuranceRate;

            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellCurrency"].Text = Eagle.Resource.LanguageResource.Currency;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellSocialInsurance"].Text = Eagle.Resource.LanguageResource.PayslipSocialInsurance;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellHealthInsurance"].Text = Eagle.Resource.LanguageResource.PayslipHealthInsurance;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellUnEmployeeInsurance"].Text = Eagle.Resource.LanguageResource.PayslipUnEmployeeInsurance;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellDependent"].Text = Eagle.Resource.LanguageResource.PayslipDependant;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellPIT"].Text = Eagle.Resource.LanguageResource.PayslipPIT;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellOTIncome"].Text = Eagle.Resource.LanguageResource.PayslipOTIncome;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellOTNoneTax"].Text = Eagle.Resource.LanguageResource.PayslipOT_NoneTax;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellTotalIncome"].Text = Eagle.Resource.LanguageResource.PayslipTotalIncome;

            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellTaxbleIncome"].Text = Eagle.Resource.LanguageResource.PayslipTaxableIncome;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellIncomeTax"].Text = Eagle.Resource.LanguageResource.PayslipIncomeTax;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellNetTakeHome"].Text = Eagle.Resource.LanguageResource.PayslipNetTakeHome;
            
            return report;
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
            return PartialView("../Payroll/PayrollSummaryReport/_Create", model);
        }               
    }
}
