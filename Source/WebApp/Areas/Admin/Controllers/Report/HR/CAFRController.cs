using Eagle.Repository.SYS.Session;
using DevExpress.XtraReports.UI;
using Eagle.Repository.Report.HR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.WebApp.Areas.Admin.Controllers.Report.HR
{
    public class CAFRController : BaseController
    {

        private int ModuleID = 9;
        ////
        //// GET: /Admin/CAFR/

        [SessionExpiration]
        public ActionResult Index()
        {
            return View("../Report/HR/CAFR/Index");
        }

        #region CAFR by Quarter ===============================================================================
        [SessionExpiration]
        [HttpGet]
        public ActionResult LoadReportViewer_CAFRByQuarter(int? Year, int? LSCompanyID)
        {
            ViewBag.Year = Year;
            ViewBag.LSCompanyID = LSCompanyID;
            return PopulateReportViewer_CAFRByQuarter(Year, LSCompanyID); ;
        }

        public ActionResult PopulateReportViewer_CAFRByQuarter(int? Year, int? LSCompanyID)
        {

            QuarterlyCAFXtraReport tableReport = CreateReport_CAFRByQuarter(Year, LSCompanyID);
            ViewData["QuarterReport"] = tableReport;
            return PartialView("../Report/HR/CAFR/_CAFRByQuarterViewer");
        }

        private QuarterlyCAFXtraReport CreateReport_CAFRByQuarter(int? Year, int? LSCompanyID)
        {
            if (Request.QueryString["ModuleID"] != null && Request.QueryString["ModuleID"] != string.Empty)
                ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
            QuarterlyCAFXtraReport _report = new QuarterlyCAFXtraReport();
            _report.Name = "QuarterlyCAFXtraReport";
            _report.Landscape = true;
            _report.PaperKind = System.Drawing.Printing.PaperKind.Custom;

            CAFRRepository respository = new CAFRRepository(db);
            DataSet DS = respository.GetData_ByQuarter(LSCompanyID, LanguageId);
            _report.DataSource = DS;
            _report.DataMember = DS.Tables[0].TableName;

            //Report Header -------------------------------------------------------------------------------------------------------------------------------    
            string virtualReportSettingFilePath = Eagle.Common.Settings.ConfigSettings.ReadSetting("ReportSettingFilePath");
            DataTable dtReportSetting = Eagle.Common.Utilities.XmlUtils.GetDataFromXmlFile(virtualReportSettingFilePath);
            string PageHeaderText = dtReportSetting.Rows[0]["PageHeaderText"].ToString();
            string PageFooterText = dtReportSetting.Rows[0]["PageFooterText"].ToString();
            string HeaderText = dtReportSetting.Rows[0]["HeaderText"].ToString();
            string CompanyName = dtReportSetting.Rows[0]["CompanyName"].ToString();
            string LogoPath = (!string.IsNullOrEmpty(dtReportSetting.Rows[0]["Logo"].ToString())) ? dtReportSetting.Rows[0]["Logo"].ToString() : "~/Content/Admin/images/logo_report.png";
            string Address = dtReportSetting.Rows[0]["Address"].ToString();
            string Tel = dtReportSetting.Rows[0]["Tel"].ToString();
            string Fax = dtReportSetting.Rows[0]["Fax"].ToString();
            string Email = dtReportSetting.Rows[0]["Email"].ToString();

            XRTable xrTableCompany = (XRTable)_report.FindControl("xrTableCompany", true);
            XRTableCell xrTableCellLabelCompanyName = (XRTableCell)_report.FindControl("xrTableCellLabelCompanyName", true);
            XRTableCell xrTableCellCompanyName = (XRTableCell)_report.FindControl("xrTableCellCompanyName", true);
            xrTableCellLabelCompanyName.Text = Eagle.Resource.LanguageResource.Headquarter;
            xrTableCellCompanyName.Text = CompanyName;

            XRTableCell xrTableCellLabelAddress = (XRTableCell)_report.FindControl("xrTableCellLabelAddress", true);
            XRTableCell xrTableCellAddress = (XRTableCell)_report.FindControl("xrTableCellAddress", true);
            xrTableCellLabelAddress.Text = Eagle.Resource.LanguageResource.Address;
            xrTableCellAddress.Text = Address;

            XRTableCell xrTableCellLabelPhone = (XRTableCell)_report.FindControl("xrTableCellLabelPhone", true);
            XRTableCell xrTableCellPhone = (XRTableCell)_report.FindControl("xrTableCellPhone", true);
            xrTableCellLabelPhone.Text = Eagle.Resource.LanguageResource.Phone;
            xrTableCellPhone.Text = Tel;

            XRTableCell xrTableCellLabelFax = (XRTableCell)_report.FindControl("xrTableCellLabelFax", true);
            XRTableCell xrTableCellFax = (XRTableCell)_report.FindControl("xrTableCellFax", true);
            xrTableCellLabelFax.Text = Eagle.Resource.LanguageResource.Fax;
            xrTableCellFax.Text = Fax;

            XRTableCell xrTableCellLabelEmail = (XRTableCell)_report.FindControl("xrTableCellLabelEmail", true);
            XRTableCell xrTableCellEmail = (XRTableCell)_report.FindControl("xrTableCellEmail", true);
            xrTableCellLabelEmail.Text = Eagle.Resource.LanguageResource.Email;
            xrTableCellEmail.Text = Email;

            XRPictureBox xrPictureBoxLogo = (XRPictureBox)_report.FindControl("xrPictureBoxLogo", true);
            xrPictureBoxLogo.ImageUrl = LogoPath;

            XRLabel xrLabelHeaderTitle = (XRLabel)_report.FindControl("xrLabelHeaderTitle", true);
            xrLabelHeaderTitle.Text = Eagle.Resource.LanguageResource.CAFRByQuarter;

            //Header ========================================================================================================================================       
            XRLabel xrLabelHeaderSeq = (XRLabel)_report.FindControl("xrLabelHeaderSeq", true);
            xrLabelHeaderSeq.Text = Eagle.Resource.LanguageResource.No;

            //XRLabel xrLabelHeaderLSCompanyCode = (XRLabel)_report.FindControl("xrLabelHeaderLSCompanyCode", true);
            //xrLabelHeaderLSCompanyCode.Text = Eagle.Resource.LanguageResource.CompanyCode;

            XRLabel xrLabelHeaderDepartment = (XRLabel)_report.FindControl("xrLabelHeaderDepartment", true);
            xrLabelHeaderDepartment.Text = Eagle.Resource.LanguageResource.Department;

            XRLabel xrLabelHeaderTotalGROSSBasicSalary = (XRLabel)_report.FindControl("xrLabelHeaderTotalGROSSBasicSalary", true);
            xrLabelHeaderTotalGROSSBasicSalary.Text = Eagle.Resource.LanguageResource.TotalGROSSBasicSalaryByDept;

            XRLabel xrLabelHeaderTotalInsuranceFeePaidByIndividual = (XRLabel)_report.FindControl("xrLabelHeaderTotalInsuranceFeePaidByIndividual", true);
            xrLabelHeaderTotalInsuranceFeePaidByIndividual.Text = Eagle.Resource.LanguageResource.TotalInsuranceFeeByDeptPaidByIndividual;

            XRLabel xrLabelHeaderTotalInsuranceFeePaidByCompany = (XRLabel)_report.FindControl("xrLabelHeaderTotalInsuranceFeePaidByCompany", true);
            xrLabelHeaderTotalInsuranceFeePaidByCompany.Text = Eagle.Resource.LanguageResource.TotalInsuranceFeeByDeptPaidByCompany;

            XRLabel xrLabelHeaderYear = (XRLabel)_report.FindControl("xrLabelHeaderYear", true);
            xrLabelHeaderYear.Text = Year.ToString();
            

            //-----------------------------------------------------------------------------------------------------------------------------
            XRLabel xrLabelHeaderFirstQuarterTGBS = (XRLabel)_report.FindControl("xrLabelHeaderFirstQuarterTGBS", true);
            xrLabelHeaderFirstQuarterTGBS.Text = Eagle.Resource.LanguageResource.FirstQuarter;

            XRLabel xrLabelHeaderSecondQuarterTGBS = (XRLabel)_report.FindControl("xrLabelHeaderSecondQuarterTGBS", true);
            xrLabelHeaderSecondQuarterTGBS.Text = Eagle.Resource.LanguageResource.SecondQuarter;

            XRLabel xrLabelHeaderThirdQuarterTGBS = (XRLabel)_report.FindControl("xrLabelHeaderThirdQuarterTGBS", true);
            xrLabelHeaderThirdQuarterTGBS.Text = Eagle.Resource.LanguageResource.ThirdQuarter;

            XRLabel xrLabelHeaderFourthQuarterTGBS = (XRLabel)_report.FindControl("xrLabelHeaderFourthQuarterTGBS", true);
            xrLabelHeaderFourthQuarterTGBS.Text = Eagle.Resource.LanguageResource.FourthQuarter;
            
            //-----------------------------------------------------------------------------------------------------------------------------
            XRLabel xrLabelHeaderFirstQuarterTIFPBI = (XRLabel)_report.FindControl("xrLabelHeaderFirstQuarterTIFPBI", true);
            xrLabelHeaderFirstQuarterTIFPBI.Text = Eagle.Resource.LanguageResource.FirstQuarter;

            XRLabel xrLabelHeaderSecondQuarterTIFPBI = (XRLabel)_report.FindControl("xrLabelHeaderSecondQuarterTIFPBI", true);
            xrLabelHeaderSecondQuarterTIFPBI.Text = Eagle.Resource.LanguageResource.SecondQuarter;

            XRLabel xrLabelHeaderThirdQuarterTIFPBI = (XRLabel)_report.FindControl("xrLabelHeaderThirdQuarterTIFPBI", true);
            xrLabelHeaderThirdQuarterTIFPBI.Text = Eagle.Resource.LanguageResource.ThirdQuarter;

            XRLabel xrLabelHeaderFourthQuarterTIFPBI = (XRLabel)_report.FindControl("xrLabelHeaderFourthQuarterTIFPBI", true);
            xrLabelHeaderFourthQuarterTIFPBI.Text = Eagle.Resource.LanguageResource.FourthQuarter;

            //-----------------------------------------------------------------------------------------------------------------------------
            XRLabel xrLabelHeaderFirstQuarterTIFPBC = (XRLabel)_report.FindControl("xrLabelHeaderFirstQuarterTIFPBC", true);
            xrLabelHeaderFirstQuarterTIFPBC.Text = Eagle.Resource.LanguageResource.FirstQuarter;

            XRLabel xrLabelHeaderSecondQuarterTIFPBC = (XRLabel)_report.FindControl("xrLabelHeaderSecondQuarterTIFPBC", true);
            xrLabelHeaderSecondQuarterTIFPBC.Text = Eagle.Resource.LanguageResource.SecondQuarter;

            XRLabel xrLabelHeaderThirdQuarterTIFPBC = (XRLabel)_report.FindControl("xrLabelHeaderThirdQuarterTIFPBC", true);
            xrLabelHeaderThirdQuarterTIFPBC.Text = Eagle.Resource.LanguageResource.ThirdQuarter;

            XRLabel xrLabelHeaderFourthQuarterTIFPBC = (XRLabel)_report.FindControl("xrLabelHeaderFourthQuarterTIFPBC", true);
            xrLabelHeaderFourthQuarterTIFPBC.Text = Eagle.Resource.LanguageResource.FourthQuarter;

            //-----------------------------------------------------------------------------------------------------------------------------


            XRTableCell xrTableCellTotalsByQuarter = (XRTableCell)_report.FindControl("xrTableCellTotalsByQuarter", true);
            xrTableCellTotalsByQuarter.Text = Eagle.Resource.LanguageResource.TotalsByQuarter;

            XRTableCell xrTableCellInfoTotalsByYear = (XRTableCell)_report.FindControl("xrTableCellInfoTotalsByYear", true);
            xrTableCellInfoTotalsByYear.Text = Eagle.Resource.LanguageResource.TotalsByYear;

            
            //Details =======================================================================================================================================
            XRTableCell xrTableCellSeq = (XRTableCell)_report.FindControl("xrTableCellSeq", true);
            xrTableCellSeq.DataBindings.Add("Text", DS, "Seq");

            //XRTableCell xrTableCellLSCompanyCode = (XRTableCell)_report.FindControl("xrTableCellLSCompanyCode", true);
            //xrTableCellLSCompanyCode.DataBindings.Add("Text", DS, "LSCompanyCode");

            XRTableCell xrTableCellDepartment = (XRTableCell)_report.FindControl("xrTableCellDepartment", true);
            xrTableCellDepartment.DataBindings.Add("Text", DS, "Department");

            XRTableCell xrTableCellTGBSFirstQuarter = (XRTableCell)_report.FindControl("xrTableCellTGBSFirstQuarter", true);
            xrTableCellTGBSFirstQuarter.DataBindings.Add("Text", DS, "TotalGROSSBasicSalaryByDeptQ1", "{0:#,##0.0}");

            XRTableCell xrTableCellTGBSSecondQuarter = (XRTableCell)_report.FindControl("xrTableCellTGBSSecondQuarter", true);
            xrTableCellTGBSSecondQuarter.DataBindings.Add("Text", DS, "TotalGROSSBasicSalaryByDeptQ2", "{0:#,##0.0}");

            XRTableCell xrTableCellTGBSThirdQuarter = (XRTableCell)_report.FindControl("xrTableCellTGBSThirdQuarter", true);
            xrTableCellTGBSThirdQuarter.DataBindings.Add("Text", DS, "TotalGROSSBasicSalaryByDeptQ3", "{0:#,##0.0}");

            XRTableCell xrTableCellTGBSFourthQuarter = (XRTableCell)_report.FindControl("xrTableCellTGBSFourthQuarter", true);
            xrTableCellTGBSFourthQuarter.DataBindings.Add("Text", DS, "TotalGROSSBasicSalaryByDeptQ4", "{0:#,##0.0}");



            XRTableCell xrTableCellTIFPBIFirstQuarter = (XRTableCell)_report.FindControl("xrTableCellTIFPBIFirstQuarter", true);
            xrTableCellTIFPBIFirstQuarter.DataBindings.Add("Text", DS, "TotalInsuranceFeeByDeptPaidByCompanyQ1", "{0:#,##0.0}");

            XRTableCell xrTableCellTIFPBISecondQuarter = (XRTableCell)_report.FindControl("xrTableCellTIFPBISecondQuarter", true);
            xrTableCellTIFPBISecondQuarter.DataBindings.Add("Text", DS, "TotalInsuranceFeeByDeptPaidByCompanyQ2", "{0:#,##0.0}");

            XRTableCell xrTableCellTIFPBIThirdQuarter = (XRTableCell)_report.FindControl("xrTableCellTIFPBIThirdQuarter", true);
            xrTableCellTIFPBIThirdQuarter.DataBindings.Add("Text", DS, "TotalInsuranceFeeByDeptPaidByCompanyQ3", "{0:#,##0.0}");

            XRTableCell xrTableCellTIFPBIFourthQuarter = (XRTableCell)_report.FindControl("xrTableCellTIFPBIFourthQuarter", true);
            xrTableCellTIFPBIFourthQuarter.DataBindings.Add("Text", DS, "TotalInsuranceFeeByDeptPaidByCompanyQ4", "{0:#,##0.0}");


            XRTableCell xrTableCellTIFPBCFirstQuarter = (XRTableCell)_report.FindControl("xrTableCellTIFPBCFirstQuarter", true);
            xrTableCellTIFPBCFirstQuarter.DataBindings.Add("Text", DS, "TotalInsuranceFeeByDeptPaidByIndividualQ1", "{0:#,##0.0}");

            XRTableCell xrTableCellTIFPBCSecondQuarter = (XRTableCell)_report.FindControl("xrTableCellTIFPBCSecondQuarter", true);
            xrTableCellTIFPBCSecondQuarter.DataBindings.Add("Text", DS, "TotalInsuranceFeeByDeptPaidByIndividualQ2", "{0:#,##0.0}");

            XRTableCell xrTableCellTIFPBCThirdQuarter = (XRTableCell)_report.FindControl("xrTableCellTIFPBCThirdQuarter", true);
            xrTableCellTIFPBCThirdQuarter.DataBindings.Add("Text", DS, "TotalInsuranceFeeByDeptPaidByIndividualQ3", "{0:#,##0.0}");

            XRTableCell xrTableCellTIFPBCFourthQuarter = (XRTableCell)_report.FindControl("xrTableCellTIFPBCFourthQuarter", true);
            xrTableCellTIFPBCFourthQuarter.DataBindings.Add("Text", DS, "TotalInsuranceFeeByDeptPaidByIndividualQ4", "{0:#,##0.0}");

            //=================================================================================================================================================


            //Summary 1 ========================================================================================================================================================================
            //===================================================================================================================================================================================
            //Start Total GROSS Basic Salary==========================================================================================================================================================
            //Quarter 1
            //DevExpress.XtraReports.UI.XRSummary xrSummaryTotalBasicSalaryGROSSFirstQuarter = new DevExpress.XtraReports.UI.XRSummary();
            //xrSummaryTotalBasicSalaryGROSSFirstQuarter.FormatString = "{0:#,##0.0}";
            //xrSummaryTotalBasicSalaryGROSSFirstQuarter.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;

            //XRTableCell xrTableCellTotalBasicSalaryGROSSFirstQuarter = (XRTableCell)_report.FindControl("xrTableCellTotalBasicSalaryGROSSFirstQuarter", true);
            //xrTableCellTotalBasicSalaryGROSSFirstQuarter.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}", DS.Tables[0].TableName, "TotalGROSSBasicSalaryByDeptQ1"))});
            //xrTableCellTotalBasicSalaryGROSSFirstQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;          
            //xrTableCellTotalBasicSalaryGROSSFirstQuarter.Summary = xrSummaryTotalBasicSalaryGROSSFirstQuarter;

            //Quarter 1
            XRTableCell xrTableCellTotalBasicSalaryGROSSFirstQuarter = (XRTableCell)_report.FindControl("xrTableCellTotalBasicSalaryGROSSFirstQuarter", true);
            xrTableCellTotalBasicSalaryGROSSFirstQuarter.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}", DS.Tables[1].TableName, "TotalGROSSBasicSalaryByQuarterQ1"), "{0:#,##0.0}")});
            xrTableCellTotalBasicSalaryGROSSFirstQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
           

             //Quarter 2
            XRTableCell xrTableCellTotalBasicSalaryGROSSSeconQuarter = (XRTableCell)_report.FindControl("xrTableCellTotalBasicSalaryGROSSSeconQuarter", true);
            xrTableCellTotalBasicSalaryGROSSSeconQuarter.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}", DS.Tables[1].TableName, "TotalGROSSBasicSalaryByQuarterQ2"), "{0:#,##0.0}")});  
            xrTableCellTotalBasicSalaryGROSSSeconQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;


             //Quarter 3
            XRTableCell xrTableCellTotalBasicSalaryGROSSThirdQuarter = (XRTableCell)_report.FindControl("xrTableCellTotalBasicSalaryGROSSThirdQuarter", true);
            xrTableCellTotalBasicSalaryGROSSThirdQuarter.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}", DS.Tables[1].TableName, "TotalGROSSBasicSalaryByQuarterQ3"), "{0:#,##0.0}")});
            xrTableCellTotalBasicSalaryGROSSThirdQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            

             //Quarter 4
            XRTableCell xrTableCellTotalBasicSalaryGROSSFourthQuarter = (XRTableCell)_report.FindControl("xrTableCellTotalBasicSalaryGROSSFourthQuarter", true);
            xrTableCellTotalBasicSalaryGROSSFourthQuarter.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}", DS.Tables[1].TableName, "TotalGROSSBasicSalaryByQuarterQ4"), "{0:#,##0.0}")});
            xrTableCellTotalBasicSalaryGROSSFourthQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            
            //End Total GROSS Basic Salary==========================================================================================================================================================
            //==================================================================================================================================================================================

            //==================================================================================================================================================================================
            //Total Insurance Fee Paid By Individual============================================================================================================================================
            //Quarter 1
            XRTableCell xrTableCellTotalInsuranceFeePaidByIndividualFirstQuarter = (XRTableCell)_report.FindControl("xrTableCellTotalInsuranceFeePaidByIndividualFirstQuarter", true);
            xrTableCellTotalInsuranceFeePaidByIndividualFirstQuarter.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}", DS.Tables[1].TableName, "TotalInsuranceFeeByQuarterPaidByIndividualQ1"), "{0:#,##0.0}")});
            xrTableCellTotalInsuranceFeePaidByIndividualFirstQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            

             //Quarter 2
            XRTableCell xrTableCellTotalInsuranceFeePaidByIndividualSecondQuarter = (XRTableCell)_report.FindControl("xrTableCellTotalInsuranceFeePaidByIndividualSecondQuarter", true);
            xrTableCellTotalInsuranceFeePaidByIndividualSecondQuarter.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}", DS.Tables[1].TableName, "TotalInsuranceFeeByQuarterPaidByIndividualQ2"), "{0:#,##0.0}")});
            xrTableCellTotalInsuranceFeePaidByIndividualSecondQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            

             //Quarter 3
            XRTableCell xrTableCellTotalInsuranceFeePaidByIndividualThirdQuarter = (XRTableCell)_report.FindControl("xrTableCellTotalInsuranceFeePaidByIndividualThirdQuarter", true);
            xrTableCellTotalInsuranceFeePaidByIndividualThirdQuarter.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}", DS.Tables[1].TableName, "TotalInsuranceFeeByQuarterPaidByIndividualQ3"), "{0:#,##0.0}")});
            xrTableCellTotalInsuranceFeePaidByIndividualThirdQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;            

             //Quarter 4
            XRTableCell xrTableCellTotalInsuranceFeePaidByIndividualFourQuarter = (XRTableCell)_report.FindControl("xrTableCellTotalInsuranceFeePaidByIndividualFourQuarter", true);
            xrTableCellTotalInsuranceFeePaidByIndividualFourQuarter.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}", DS.Tables[1].TableName, "TotalInsuranceFeeByQuarterPaidByIndividualQ4"), "{0:#,##0.0}")});
            xrTableCellTotalInsuranceFeePaidByIndividualFourQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            
            //Total Insurance Fee Paid By Individual============================================================================================================================================
            //===================================================================================================================================================================================

            //==================================================================================================================================================================================
            //Start Total Insurance Fee Paid By Company============================================================================================================================================
            //Quarter 1
            XRTableCell xrTableCellTotalInsuranceFeePaidByCompanyFirstQuarter = (XRTableCell)_report.FindControl("xrTableCellTotalInsuranceFeePaidByCompanyFirstQuarter", true);
            xrTableCellTotalInsuranceFeePaidByCompanyFirstQuarter.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}", DS.Tables[1].TableName, "TotalInsuranceFeeByQuarterPaidByCompanyQ1"), "{0:#,##0.0}")});
            xrTableCellTotalInsuranceFeePaidByCompanyFirstQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;           

            //Quarter 2
            XRTableCell xrTableCellTotalInsuranceFeePaidByCompanySecondQuarter = (XRTableCell)_report.FindControl("xrTableCellTotalInsuranceFeePaidByCompanySecondQuarter", true);
            xrTableCellTotalInsuranceFeePaidByCompanySecondQuarter.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}", DS.Tables[1].TableName, "TotalInsuranceFeeByQuarterPaidByCompanyQ2"), "{0:#,##0.0}")});
            xrTableCellTotalInsuranceFeePaidByCompanySecondQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            

            //Quarter 3
            XRTableCell xrTableCellTotalInsuranceFeePaidByCompanyThirdWarter = (XRTableCell)_report.FindControl("xrTableCellTotalInsuranceFeePaidByCompanyThirdWarter", true);
            xrTableCellTotalInsuranceFeePaidByCompanyThirdWarter.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}", DS.Tables[1].TableName, "TotalInsuranceFeeByQuarterPaidByCompanyQ3"), "{0:#,##0.0}")});
            xrTableCellTotalInsuranceFeePaidByCompanyThirdWarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;            

            //Quarter 4
            XRTableCell xrTableCellTotalInsuranceFeePaidByCompanyFourthQuarter = (XRTableCell)_report.FindControl("xrTableCellTotalInsuranceFeePaidByCompanyFourthQuarter", true);
            xrTableCellTotalInsuranceFeePaidByCompanyFourthQuarter.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}", DS.Tables[1].TableName, "TotalInsuranceFeeByQuarterPaidByCompanyQ4"), "{0:#,##0.0}")});
            xrTableCellTotalInsuranceFeePaidByCompanyFourthQuarter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;            
            //End Total Insurance Fee Paid By Company=================================================================================================================================================
            //===================================================================================================================================================================================
                        

            //=====================================================================================================================================================================================
            //Summary 2 ===========================================================================================================================================================================
            XRTableCell xrTableCellTotalGROSSBasicSalary = (XRTableCell)_report.FindControl("xrTableCellTotalGROSSBasicSalary", true);
            xrTableCellTotalGROSSBasicSalary.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}", DS.Tables[2].TableName,"TotalBasicSalaryGROSS"), "{0:#,##0.0}")});

            XRTableCell xrTableCellTotalInsuranceFeePaidByIndividual = (XRTableCell)_report.FindControl("xrTableCellTotalInsuranceFeePaidByIndividual", true);
            xrTableCellTotalInsuranceFeePaidByIndividual.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}", DS.Tables[2].TableName,"TotalInsuranceFeePaidByIndividual"), "{0:#,##0.0}")});

            XRTableCell xrTableCellTotalInsuranceFeePaidByCompany = (XRTableCell)_report.FindControl("xrTableCellTotalInsuranceFeePaidByCompany", true);
            xrTableCellTotalInsuranceFeePaidByCompany.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}", DS.Tables[2].TableName,"TotalInsuranceFeePaidByCompany"), "{0:#,##0.0}")});
            ///Summary 2 =================================================================================================================================================
            //===================================================================================================================================================================================

            //=====================================================================================================================================================================================
            //Summary 3 ===========================================================================================================================================================================
            DevExpress.XtraReports.UI.XRSummary xrSummaryTotalsByYear = new DevExpress.XtraReports.UI.XRSummary();
            xrSummaryTotalsByYear.FormatString = "{0:#,##0.0}";
            xrSummaryTotalsByYear.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;

            XRTableCell xrTableCellTotalsByYear = (XRTableCell)_report.FindControl("xrTableCellTotalsByYear", true);
            xrTableCellTotalsByYear.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}", DS.Tables[3].TableName,"TotalsByYear"), "{0:#,##0.0}")});
            //===================================================================================================================================================================================


            //PageFooter ---------------------------------------------------------------------------------------------------------------------------------------------------
            XRPageInfo xrPageFooterLeftText = (XRPageInfo)_report.FindControl("xrPageFooterLeftText", true);
            xrPageFooterLeftText.Text = DateTime.Now.ToString("dd-MM-yyyy");

            XRPageInfo xrPageFooterRightText = (XRPageInfo)_report.FindControl("xrPageFooterRightText", true);
            xrPageFooterRightText.Text = Eagle.Resource.LanguageResource.Page;
            //--------------------------------------------------------------------------------------------------------------------------------------------------------------

            return _report;
        }

        //private void Detail_BeforePrint(object sender, PrintEventArgs e)
        //{
        //    // Get the value of the current row in the master report.
        //    xrLabel1.Text = ((DataRowView)GetCurrentRow()).Row["CategoryName"].ToString();

        //    // Get the value of the current cell in the CategoryName column in the master report.
        //    xrLabel2.Text = GetCurrentColumnValue("CategoryName").ToString();
        //}

        public ActionResult ExportData_CAFRByQuarter(int? Year, int? LSCompanyID)
        {
            return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(CreateReport_CAFRByQuarter(Year, LSCompanyID));
        }
        #endregion ============================================================================================

        #region CAFR ==========================================================================================
        [SessionExpiration]
        [HttpGet]
        public ActionResult LoadReportViewer(int? Year, int? LSCompanyID)
        {
            ViewBag.Year = Year;
            ViewBag.LSCompanyID = LSCompanyID;
            return PopulateReportViewer(LSCompanyID); ;
        }
        private CAFXtraReport CreateReport(int? LSCompanyID)
        {
            if (Request.QueryString["ModuleID"] != null && Request.QueryString["ModuleID"] != string.Empty)
                ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
            CAFXtraReport _report = new CAFXtraReport();
            _report.Name = "CAFXtraReport";
            _report.Landscape = true;
            _report.PaperKind = System.Drawing.Printing.PaperKind.Custom;

            CAFRRepository respository = new CAFRRepository(db);
            DataSet DS = respository.GetData_Master(LSCompanyID, LanguageId);
            _report.DataSource = DS;
            _report.DataMember = DS.Tables[0].TableName;
            //_report.DetailReport.DataSource = DS;
            //_report.DetailReport.DataMember = String.Format("{0}.{1}", DS.Tables[0].TableName, DS.Relations[0].RelationName);

            //Report Header -------------------------------------------------------------------------------------------------------------------------------    
            string virtualReportSettingFilePath = Eagle.Common.Settings.ConfigSettings.ReadSetting("ReportSettingFilePath");
            DataTable dtReportSetting = Eagle.Common.Utilities.XmlUtils.GetDataFromXmlFile(virtualReportSettingFilePath);
            string PageHeaderText = dtReportSetting.Rows[0]["PageHeaderText"].ToString();
            string PageFooterText = dtReportSetting.Rows[0]["PageFooterText"].ToString();
            string HeaderText = dtReportSetting.Rows[0]["HeaderText"].ToString();
            string CompanyName = dtReportSetting.Rows[0]["CompanyName"].ToString();
            string LogoPath = (!string.IsNullOrEmpty(dtReportSetting.Rows[0]["Logo"].ToString())) ? dtReportSetting.Rows[0]["Logo"].ToString() : "~/Content/Admin/images/logo_report.png";
            string Address = dtReportSetting.Rows[0]["Address"].ToString();
            string Tel = dtReportSetting.Rows[0]["Tel"].ToString();
            string Fax = dtReportSetting.Rows[0]["Fax"].ToString();
            string Email = dtReportSetting.Rows[0]["Email"].ToString();

            //Format String : {0:#,##0.0}
            XRTable xrTableCompany = (XRTable)_report.FindControl("xrTableCompany", true);
            XRTableCell xrTableCellLabelCompanyName = (XRTableCell)_report.FindControl("xrTableCellLabelCompanyName", true);
            XRTableCell xrTableCellCompanyName = (XRTableCell)_report.FindControl("xrTableCellCompanyName", true);
            xrTableCellLabelCompanyName.Text = Eagle.Resource.LanguageResource.Headquarter;
            xrTableCellCompanyName.Text = CompanyName;

            XRTableCell xrTableCellLabelAddress = (XRTableCell)_report.FindControl("xrTableCellLabelAddress", true);
            XRTableCell xrTableCellAddress = (XRTableCell)_report.FindControl("xrTableCellAddress", true);
            xrTableCellLabelAddress.Text = Eagle.Resource.LanguageResource.Address;
            xrTableCellAddress.Text = Address;

            XRTableCell xrTableCellLabelPhone = (XRTableCell)_report.FindControl("xrTableCellLabelPhone", true);
            XRTableCell xrTableCellPhone = (XRTableCell)_report.FindControl("xrTableCellPhone", true);
            xrTableCellLabelPhone.Text = Eagle.Resource.LanguageResource.Phone;
            xrTableCellPhone.Text = Tel;

            XRTableCell xrTableCellLabelFax = (XRTableCell)_report.FindControl("xrTableCellLabelFax", true);
            XRTableCell xrTableCellFax = (XRTableCell)_report.FindControl("xrTableCellFax", true);
            xrTableCellLabelFax.Text = Eagle.Resource.LanguageResource.Fax;
            xrTableCellFax.Text = Fax;

            XRTableCell xrTableCellLabelEmail = (XRTableCell)_report.FindControl("xrTableCellLabelEmail", true);
            XRTableCell xrTableCellEmail = (XRTableCell)_report.FindControl("xrTableCellEmail", true);
            xrTableCellLabelEmail.Text = Eagle.Resource.LanguageResource.Email;
            xrTableCellEmail.Text = Email;

            XRPictureBox xrPictureBoxLogo = (XRPictureBox)_report.FindControl("xrPictureBoxLogo", true);
            xrPictureBoxLogo.ImageUrl = LogoPath;

            XRLabel xrLabelHeaderTitle = (XRLabel)_report.FindControl("xrLabelHeaderTitle", true);
            xrLabelHeaderTitle.Text = Eagle.Resource.LanguageResource.CAFR;
            //string.Format("{0} {1}", Eagle.Resource.LanguageResource.CAFR, Year);

            //Header                    
            XRTableCell xrTableCellHeaderSeq = (XRTableCell)_report.FindControl("xrTableCellHeaderSeq", true);
            xrTableCellHeaderSeq.Text = Eagle.Resource.LanguageResource.No;

            XRTableCell xrTableCellHeaderLSCompanyCode = (XRTableCell)_report.FindControl("xrTableCellHeaderLSCompanyCode", true);
            xrTableCellHeaderLSCompanyCode.Text = Eagle.Resource.LanguageResource.CompanyCode;

            XRTableCell xrTableCellHeaderDepartment = (XRTableCell)_report.FindControl("xrTableCellHeaderDepartment", true);
            xrTableCellHeaderDepartment.Text = Eagle.Resource.LanguageResource.Department;

            XRTableCell xrTableCellHeaderTotalGROSSBasicSalaryByDept = (XRTableCell)_report.FindControl("xrTableCellHeaderTotalGROSSBasicSalaryByDept", true);
            xrTableCellHeaderTotalGROSSBasicSalaryByDept.Text = Eagle.Resource.LanguageResource.TotalGROSSBasicSalaryByDept;

            XRTableCell xrTableCellHeaderTotalInsuranceFeeByDeptPaidByIndividual = (XRTableCell)_report.FindControl("xrTableCellHeaderTotalInsuranceFeeByDeptPaidByIndividual", true);
            xrTableCellHeaderTotalInsuranceFeeByDeptPaidByIndividual.Text = Eagle.Resource.LanguageResource.TotalInsuranceFeeByDeptPaidByIndividual;

            XRTableCell xrTableCellHeaderTotalInsuranceFeeByDeptPaidByCompany = (XRTableCell)_report.FindControl("xrTableCellHeaderTotalInsuranceFeeByDeptPaidByCompany", true);
            xrTableCellHeaderTotalInsuranceFeeByDeptPaidByCompany.Text = Eagle.Resource.LanguageResource.TotalInsuranceFeeByDeptPaidByCompany;

            XRTableCell xrTableCellTotalSummary = (XRTableCell)_report.FindControl("xrTableCellTotalSummary", true);
            xrTableCellTotalSummary.Text = Eagle.Resource.LanguageResource.Totals;
            


            //Details ===============================================================================================
            XRTableCell xrTableCellSeq = (XRTableCell)_report.FindControl("xrTableCellSeq", true);
            xrTableCellSeq.DataBindings.Add("Text", DS, "Seq");

            XRTableCell xrTableCellLSCompanyCode = (XRTableCell)_report.FindControl("xrTableCellLSCompanyCode", true);
            xrTableCellLSCompanyCode.DataBindings.Add("Text", DS, "LSCompanyCode");

            XRTableCell xrTableCellDepartment = (XRTableCell)_report.FindControl("xrTableCellDepartment", true);
            xrTableCellDepartment.DataBindings.Add("Text", DS, "Department");

            XRTableCell xrTableCellTotalGROSSBasicSalaryByDept = (XRTableCell)_report.FindControl("xrTableCellTotalGROSSBasicSalaryByDept", true);
            xrTableCellTotalGROSSBasicSalaryByDept.DataBindings.Add("Text", DS, "TotalGROSSBasicSalaryByDept", "{0:#,##0.0}");

            XRTableCell xrTableCellTotalInsuranceFeeByDeptPaidByIndividual = (XRTableCell)_report.FindControl("xrTableCellTotalInsuranceFeeByDeptPaidByIndividual", true);
            xrTableCellTotalInsuranceFeeByDeptPaidByIndividual.DataBindings.Add("Text", DS, "TotalInsuranceFeeByDeptPaidByIndividual", "{0:#,##0.0}");

            XRTableCell xrTableCellTotalInsuranceFeeByDeptPaidByCompany = (XRTableCell)_report.FindControl("xrTableCellTotalInsuranceFeeByDeptPaidByCompany", true);
            xrTableCellTotalInsuranceFeeByDeptPaidByCompany.DataBindings.Add("Text", DS, "TotalInsuranceFeeByDeptPaidByCompany", "{0:#,##0.0}");


            //Summary
            XRTableCell xrTableCellTotalBasicSalaryGROSS = (XRTableCell)_report.FindControl("xrTableCellTotalBasicSalaryGROSS", true);
            xrTableCellTotalBasicSalaryGROSS.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}", DS.Tables[1].TableName,"TotalBasicSalaryGROSS"), "{0:#,##0.0}")});
            
            XRTableCell xrTableCellTotalInsuranceFeePaidByIndividual = (XRTableCell)_report.FindControl("xrTableCellTotalInsuranceFeePaidByIndividual", true);
            xrTableCellTotalInsuranceFeePaidByIndividual.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}", DS.Tables[1].TableName,"TotalInsuranceFeePaidByIndividual"), "{0:#,##0.0}")});

            XRTableCell xrTableCellTotalInsuranceFeePaidByCompany = (XRTableCell)_report.FindControl("xrTableCellTotalInsuranceFeePaidByCompany", true);
            xrTableCellTotalInsuranceFeePaidByCompany.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}", DS.Tables[1].TableName,"TotalInsuranceFeePaidByCompany"), "{0:#,##0.0}")});

            //---------------------------------------------------------------------------------------------------------------------------------
            //XRTableCell xrTableCellDepartmentName = (XRTableCell)_report.FindControl("xrTableCellDepartmentName", true);
            //xrTableCellDepartmentName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}", DS.Tables[0].TableName, "DepartmentName"))});

            //XRTableCell xrTableCellSeq = (XRTableCell)_report.FindControl("xrTableCellSeq", true);
            //xrTableCellSeq.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "Seq"))});

            //XRTableCell xrTableCellLSCompanyCode = (XRTableCell)_report.FindControl("xrTableCellLSCompanyCode", true);
            //xrTableCellLSCompanyCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "LSCompanyCode"))});

            //XRTableCell xrTableCellDepartment = (XRTableCell)_report.FindControl("xrTableCellDepartment", true);
            //xrTableCellDepartment.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "Department"))});

            //XRTableCell xrTableCellTotalGROSSBasicSalaryByDept = (XRTableCell)_report.FindControl("xrTableCellTotalGROSSBasicSalaryByDept", true);
            //xrTableCellTotalGROSSBasicSalaryByDept.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "TotalGROSSBasicSalaryByDept"))});

            //XRTableCell xrTableCellTotalInsuranceFeeByDeptPaidByIndividual = (XRTableCell)_report.FindControl("xrTableCellTotalInsuranceFeeByDeptPaidByIndividual", true);
            //xrTableCellTotalInsuranceFeeByDeptPaidByIndividual.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "TotalInsuranceFeeByDeptPaidByIndividual"))});

            //XRTableCell xrTableCellTotalInsuranceFeeByDeptPaidByCompany = (XRTableCell)_report.FindControl("xrTableCellTotalInsuranceFeeByDeptPaidByCompany", true);
            //xrTableCellTotalInsuranceFeeByDeptPaidByCompany.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "TotalInsuranceFeeByDeptPaidByCompany"))});

            ////Report Footer
            //XRTableCell xrTableCellTotalBasicSalaryGROSS = (XRTableCell)_report.FindControl("xrTableCellTotalBasicSalaryGROSS", true);
            //xrTableCellTotalBasicSalaryGROSS.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "TotalBasicSalaryGROSS"))});

            //XRTableCell xrTableCellTotalInsuranceFeePaidByIndividual = (XRTableCell)_report.FindControl("xrTableCellTotalInsuranceFeePaidByIndividual", true);
            //xrTableCellTotalInsuranceFeePaidByIndividual.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "TotalInsuranceFeePaidByIndividual"))});

            //XRTableCell xrTableCellTotalInsuranceFeePaidByCompany = (XRTableCell)_report.FindControl("xrTableCellTotalInsuranceFeePaidByCompany", true);
            //xrTableCellTotalInsuranceFeePaidByCompany.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "TotalInsuranceFeePaidByCompany"))});
            
            //PageFooter --------------------------------------------------------------------------------------
            XRPageInfo xrPageFooterLeftText = (XRPageInfo)_report.FindControl("xrPageFooterLeftText", true);
            xrPageFooterLeftText.Text = DateTime.Now.ToString("dd-MM-yyyy");

            XRPageInfo xrPageFooterRightText = (XRPageInfo)_report.FindControl("xrPageFooterRightText", true);
            xrPageFooterRightText.Text = Eagle.Resource.LanguageResource.Page;
            //--------------------------------------------------------------------------------------------------

            return _report;
        }

        public ActionResult PopulateReportViewer(int? LSCompanyID)
        {

            CAFXtraReport tableReport = CreateReport(LSCompanyID);
            ViewData["Report"] = tableReport;
            return PartialView("../Report/HR/CAFR/_CAFRViewer");
        }

        public ActionResult ExportData(int? LSCompanyID)
        {
            return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(CreateReport(LSCompanyID));
        }
        #endregion CAFR ==================================================================
    }
}
