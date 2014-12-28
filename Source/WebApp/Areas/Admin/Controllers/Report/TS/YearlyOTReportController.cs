using Eagle.Common.Extensions;
using Eagle.Repository.SYS.Session;
using DevExpress.XtraReports.UI;
using Eagle.Repository;
using Eagle.Repository.Report.TS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Eagle.WebApp.Areas.Admin.Controllers.Report.TS
{
    public class YearlyOTReportController : BaseController
    {
        //
        // GET: /Admin/YearlyOTReport/

        private int FunctionID = 490;
        private int ModuleID = 9;      
       
        [SessionExpiration]
        public ActionResult Index()
        {
            PopulateYearsToDropDownList(10, string.Empty, true);
            return View("../Report/TS/YearlyOT/Index");
        }
        
        #region Year --------------------------------------------------------------------------------------------------------------------------------------------------

        public void PopulateYearsToDropDownList(int NumberOfYears, string SelectedValue, bool IsShowSelectText)
        {
            ViewBag.Year = CommonRepository.LoadYearList(NumberOfYears, SelectedValue, IsShowSelectText);
        }
        #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------

         #region LOAD REPORT - DETAIL REPORT VIEWER ==========================================================================

        [SessionExpiration]
        [HttpGet]
        public ActionResult LoadReportViewer(int? Year, int? LSCompanyID, string EmpCode, string FullName)
        {            
            ViewBag.Year = Year;
            ViewBag.LSCompanyID = LSCompanyID;
            ViewBag.EmpCode = EmpCode;
            ViewBag.FullName = FullName;
            return PopulateReportViewer_YearlyOT(Year, LSCompanyID, EmpCode, FullName); ;
        }
        #endregion LOAD REPORT - DETAIL REPORT VIEWER =========================================================================

        #region OT By Year REPORT ==========================================================================================
        private OTByYearXtraReport CreateReport_YearlyOT(int? Year, int? LSCompanyID, string EmpCode, string FullName)
        {
            if (Request.QueryString["ModuleID"] != null && Request.QueryString["ModuleID"] != string.Empty)
                ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
            OTByYearXtraReport _report = new OTByYearXtraReport();
            _report.Name = "OTByYearXtraReport";
            _report.Landscape = true;
            _report.PaperKind = System.Drawing.Printing.PaperKind.Custom;

            YearlyOTReportRepository respository = new YearlyOTReportRepository(db);
            DataTable dtParent = respository.GetYearlyMasterList(Year, LSCompanyID, EmpCode, FullName, UserName, ModuleID, FunctionID, LanguageId);
            dtParent.TableName = "Timesheet_sprptOTByYear_Master";
            if (dtParent.Rows.Count == 0)
            {
                var row = dtParent.NewRow();
                row["LSCompanyID"] = -1;
                row["Year"] = Year;
                dtParent.Rows.Add(row);
            }
            DataTable dtChild = respository.GetYearlyDetailList(Year, LSCompanyID, EmpCode, FullName, UserName, ModuleID, FunctionID, LanguageId);
            dtChild.TableName = "Timesheet_sprptOTByYear_Details";
            if (dtChild.Rows.Count == 0)
            {
                var row = dtChild.NewRow();
                row["LSCompanyID"] = -1;
                row["Year"] = Year;
                dtChild.Rows.Add(row);
            }

            string result = string.Empty;
            foreach (DataRow dr in dtChild.Rows)
            {
                result = dr["EmpCode"].ToString()
                    + " -" + dr["FullName"].ToString()
                     + " -" + dr["PositionName"].ToString();
            }

            DataSet DS = DataSetHelper.CreateDataSetJoinedFrom2Tables("ParentChildrenDS", "LSCompanyID", dtParent, dtChild);
            _report.DataSource = DS;
            _report.DataMember = DS.Tables[0].TableName;
            _report.DetailReport.DataSource = DS;
            _report.DetailReport.DataMember = String.Format("{0}.{1}", DS.Tables[0].TableName, DS.Relations[0].RelationName);


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
            xrLabelHeaderTitle.Text = string.Format("{0} {1}", Eagle.Resource.LanguageResource.OTMonthlyReport, Year);
                
            //Header                    
            XRLabel xrLabelHeaderNo = (XRLabel)_report.FindControl("xrLabelHeaderNo", true);
            xrLabelHeaderNo.Text = Eagle.Resource.LanguageResource.No;

            XRLabel xrLabelHeaderEmpCode = (XRLabel)_report.FindControl("xrLabelHeaderEmpCode", true);
            xrLabelHeaderEmpCode.Text = Eagle.Resource.LanguageResource.Code;

            XRLabel xrLabelHeaderFullName = (XRLabel)_report.FindControl("xrLabelHeaderFullName", true);
            xrLabelHeaderFullName.Text = Eagle.Resource.LanguageResource.FullName;

            XRLabel xrLabelHeaderPosition = (XRLabel)_report.FindControl("xrLabelHeaderPosition", true);
            xrLabelHeaderPosition.Text = Eagle.Resource.LanguageResource.Position;


            //Thang 1-12
            XRLabel xrLabelOTJanuary = (XRLabel)_report.FindControl("xrLabelOTJanuary", true);
            xrLabelOTJanuary.Text = Eagle.Resource.LanguageResource.January;

            XRLabel xrLabelOTFebuary = (XRLabel)_report.FindControl("xrLabelOTFebuary", true);
            xrLabelOTFebuary.Text = Eagle.Resource.LanguageResource.Febuary;

            XRLabel xrLabelOTMarch = (XRLabel)_report.FindControl("xrLabelOTMarch", true);
            xrLabelOTMarch.Text = Eagle.Resource.LanguageResource.March;

            XRLabel xrLabelOTApril = (XRLabel)_report.FindControl("xrLabelOTApril", true);
            xrLabelOTApril.Text = Eagle.Resource.LanguageResource.April;

            XRLabel xrLabelOTMay = (XRLabel)_report.FindControl("xrLabelOTMay", true);
            xrLabelOTMay.Text = Eagle.Resource.LanguageResource.May;

            XRLabel xrLabelOTJune = (XRLabel)_report.FindControl("xrLabelOTJune", true);
            xrLabelOTJune.Text = Eagle.Resource.LanguageResource.June;

            XRLabel xrLabelOTJuly = (XRLabel)_report.FindControl("xrLabelOTJuly", true);
            xrLabelOTJuly.Text = Eagle.Resource.LanguageResource.July;

            XRLabel xrLabelOTAugust = (XRLabel)_report.FindControl("xrLabelOTAugust", true);
            xrLabelOTAugust.Text = Eagle.Resource.LanguageResource.August;


            XRLabel xrLabelOTSeptember = (XRLabel)_report.FindControl("xrLabelOTSeptember", true);
            xrLabelOTSeptember.Text = Eagle.Resource.LanguageResource.September;


            XRLabel xrLabelOTOctober = (XRLabel)_report.FindControl("xrLabelOTOctober", true);
            xrLabelOTOctober.Text = Eagle.Resource.LanguageResource.October;


            XRLabel xrLabelOTNovember = (XRLabel)_report.FindControl("xrLabelOTNovember", true);
            xrLabelOTNovember.Text = Eagle.Resource.LanguageResource.November;


            XRLabel xrLabelOTDecember = (XRLabel)_report.FindControl("xrLabelOTDecember", true);
            xrLabelOTDecember.Text = Eagle.Resource.LanguageResource.December;


            //Day - Normal Day
            XRLabel xrLabelHeaderND1 = (XRLabel)_report.FindControl("xrLabelHeaderND1", true);
            XRLabel xrLabelHeaderND2 = (XRLabel)_report.FindControl("xrLabelHeaderND2", true);
            XRLabel xrLabelHeaderND3 = (XRLabel)_report.FindControl("xrLabelHeaderND3", true);
            XRLabel xrLabelHeaderND4 = (XRLabel)_report.FindControl("xrLabelHeaderND4", true);
            XRLabel xrLabelHeaderND5 = (XRLabel)_report.FindControl("xrLabelHeaderND5", true);
            XRLabel xrLabelHeaderND6 = (XRLabel)_report.FindControl("xrLabelHeaderND6", true);
            XRLabel xrLabelHeaderND7 = (XRLabel)_report.FindControl("xrLabelHeaderND7", true);
            XRLabel xrLabelHeaderND8 = (XRLabel)_report.FindControl("xrLabelHeaderND8", true);
            XRLabel xrLabelHeaderND9 = (XRLabel)_report.FindControl("xrLabelHeaderND9", true);
            XRLabel xrLabelHeaderND10 = (XRLabel)_report.FindControl("xrLabelHeaderND10", true);
            XRLabel xrLabelHeaderND11 = (XRLabel)_report.FindControl("xrLabelHeaderND11", true);
            XRLabel xrLabelHeaderND12 = (XRLabel)_report.FindControl("xrLabelHeaderND12", true);

            xrLabelHeaderND1.Text = Eagle.Resource.LanguageResource.DayNormal;
            xrLabelHeaderND2.Text = Eagle.Resource.LanguageResource.DayNormal;
            xrLabelHeaderND3.Text = Eagle.Resource.LanguageResource.DayNormal;
            xrLabelHeaderND4.Text = Eagle.Resource.LanguageResource.DayNormal;
            xrLabelHeaderND5.Text = Eagle.Resource.LanguageResource.DayNormal;
            xrLabelHeaderND6.Text = Eagle.Resource.LanguageResource.DayNormal;
            xrLabelHeaderND7.Text = Eagle.Resource.LanguageResource.DayNormal;
            xrLabelHeaderND8.Text = Eagle.Resource.LanguageResource.DayNormal;
            xrLabelHeaderND9.Text = Eagle.Resource.LanguageResource.DayNormal;
            xrLabelHeaderND10.Text = Eagle.Resource.LanguageResource.DayNormal;
            xrLabelHeaderND11.Text = Eagle.Resource.LanguageResource.DayNormal;
            xrLabelHeaderND12.Text = Eagle.Resource.LanguageResource.DayNormal;


            //Night - Normal Day
            XRLabel xrLabelHeaderNND1 = (XRLabel)_report.FindControl("xrLabelHeaderNND1", true);
            XRLabel xrLabelHeaderNND2 = (XRLabel)_report.FindControl("xrLabelHeaderNND2", true);
            XRLabel xrLabelHeaderNND3 = (XRLabel)_report.FindControl("xrLabelHeaderNND3", true);
            XRLabel xrLabelHeaderNND4 = (XRLabel)_report.FindControl("xrLabelHeaderNND4", true);
            XRLabel xrLabelHeaderNND5 = (XRLabel)_report.FindControl("xrLabelHeaderNND5", true);
            XRLabel xrLabelHeaderNND6 = (XRLabel)_report.FindControl("xrLabelHeaderNND6", true);
            XRLabel xrLabelHeaderNND7 = (XRLabel)_report.FindControl("xrLabelHeaderNND7", true);
            XRLabel xrLabelHeaderNND8 = (XRLabel)_report.FindControl("xrLabelHeaderNND8", true);
            XRLabel xrLabelHeaderNND9 = (XRLabel)_report.FindControl("xrLabelHeaderNND9", true);
            XRLabel xrLabelHeaderNND10 = (XRLabel)_report.FindControl("xrLabelHeaderNND10", true);
            XRLabel xrLabelHeaderNND11 = (XRLabel)_report.FindControl("xrLabelHeaderNND11", true);
            XRLabel xrLabelHeaderNND12 = (XRLabel)_report.FindControl("xrLabelHeaderNND12", true);

            xrLabelHeaderNND1.Text = Eagle.Resource.LanguageResource.NightNormal;
            xrLabelHeaderNND2.Text = Eagle.Resource.LanguageResource.NightNormal;
            xrLabelHeaderNND3.Text = Eagle.Resource.LanguageResource.NightNormal;
            xrLabelHeaderNND4.Text = Eagle.Resource.LanguageResource.NightNormal;
            xrLabelHeaderNND5.Text = Eagle.Resource.LanguageResource.NightNormal;
            xrLabelHeaderNND6.Text = Eagle.Resource.LanguageResource.NightNormal;
            xrLabelHeaderNND7.Text = Eagle.Resource.LanguageResource.NightNormal;
            xrLabelHeaderNND8.Text = Eagle.Resource.LanguageResource.NightNormal;
            xrLabelHeaderNND9.Text = Eagle.Resource.LanguageResource.NightNormal;
            xrLabelHeaderNND10.Text = Eagle.Resource.LanguageResource.NightNormal;
            xrLabelHeaderNND11.Text = Eagle.Resource.LanguageResource.NightNormal;
            xrLabelHeaderNND12.Text = Eagle.Resource.LanguageResource.NightNormal;


            //Day - Weekend 
            XRLabel xrLabelHeaderW1 = (XRLabel)_report.FindControl("xrLabelHeaderW1", true);
            XRLabel xrLabelHeaderW2 = (XRLabel)_report.FindControl("xrLabelHeaderW2", true);
            XRLabel xrLabelHeaderW3 = (XRLabel)_report.FindControl("xrLabelHeaderW3", true);
            XRLabel xrLabelHeaderW4 = (XRLabel)_report.FindControl("xrLabelHeaderW4", true);
            XRLabel xrLabelHeaderW5 = (XRLabel)_report.FindControl("xrLabelHeaderW5", true);
            XRLabel xrLabelHeaderW6 = (XRLabel)_report.FindControl("xrLabelHeaderW6", true);
            XRLabel xrLabelHeaderW7 = (XRLabel)_report.FindControl("xrLabelHeaderW7", true);
            XRLabel xrLabelHeaderW8 = (XRLabel)_report.FindControl("xrLabelHeaderW8", true);
            XRLabel xrLabelHeaderW9 = (XRLabel)_report.FindControl("xrLabelHeaderW9", true);
            XRLabel xrLabelHeaderW10 = (XRLabel)_report.FindControl("xrLabelHeaderW10", true);
            XRLabel xrLabelHeaderW11 = (XRLabel)_report.FindControl("xrLabelHeaderW11", true);
            XRLabel xrLabelHeaderW12 = (XRLabel)_report.FindControl("xrLabelHeaderW12", true);

            xrLabelHeaderW1.Text = Eagle.Resource.LanguageResource.DayWeekend;
            xrLabelHeaderW2.Text = Eagle.Resource.LanguageResource.DayWeekend;
            xrLabelHeaderW3.Text = Eagle.Resource.LanguageResource.DayWeekend;
            xrLabelHeaderW4.Text = Eagle.Resource.LanguageResource.DayWeekend;
            xrLabelHeaderW5.Text = Eagle.Resource.LanguageResource.DayWeekend;
            xrLabelHeaderW6.Text = Eagle.Resource.LanguageResource.DayWeekend;
            xrLabelHeaderW7.Text = Eagle.Resource.LanguageResource.DayWeekend;
            xrLabelHeaderW8.Text = Eagle.Resource.LanguageResource.DayWeekend;
            xrLabelHeaderW9.Text = Eagle.Resource.LanguageResource.DayWeekend;
            xrLabelHeaderW10.Text = Eagle.Resource.LanguageResource.DayWeekend;
            xrLabelHeaderW11.Text = Eagle.Resource.LanguageResource.DayWeekend;
            xrLabelHeaderW12.Text = Eagle.Resource.LanguageResource.DayWeekend;



            //Night - Weekend 
            XRLabel xrLabelHeaderNW1 = (XRLabel)_report.FindControl("xrLabelHeaderNW1", true);
            XRLabel xrLabelHeaderNW2 = (XRLabel)_report.FindControl("xrLabelHeaderNW2", true);
            XRLabel xrLabelHeaderNW3 = (XRLabel)_report.FindControl("xrLabelHeaderNW3", true);
            XRLabel xrLabelHeaderNW4 = (XRLabel)_report.FindControl("xrLabelHeaderNW4", true);
            XRLabel xrLabelHeaderNW5 = (XRLabel)_report.FindControl("xrLabelHeaderNW5", true);
            XRLabel xrLabelHeaderNW6 = (XRLabel)_report.FindControl("xrLabelHeaderNW6", true);
            XRLabel xrLabelHeaderNW7 = (XRLabel)_report.FindControl("xrLabelHeaderNW7", true);
            XRLabel xrLabelHeaderNW8 = (XRLabel)_report.FindControl("xrLabelHeaderNW8", true);
            XRLabel xrLabelHeaderNW9 = (XRLabel)_report.FindControl("xrLabelHeaderNW9", true);
            XRLabel xrLabelHeaderNW10 = (XRLabel)_report.FindControl("xrLabelHeaderNW10", true);
            XRLabel xrLabelHeaderNW11 = (XRLabel)_report.FindControl("xrLabelHeaderNW11", true);
            XRLabel xrLabelHeaderNW12 = (XRLabel)_report.FindControl("xrLabelHeaderNW12", true);

            xrLabelHeaderNW1.Text = Eagle.Resource.LanguageResource.NightWeekend;
            xrLabelHeaderNW2.Text = Eagle.Resource.LanguageResource.NightWeekend;
            xrLabelHeaderNW3.Text = Eagle.Resource.LanguageResource.NightWeekend;
            xrLabelHeaderNW4.Text = Eagle.Resource.LanguageResource.NightWeekend;
            xrLabelHeaderNW5.Text = Eagle.Resource.LanguageResource.NightWeekend;
            xrLabelHeaderNW6.Text = Eagle.Resource.LanguageResource.NightWeekend;
            xrLabelHeaderNW7.Text = Eagle.Resource.LanguageResource.NightWeekend;
            xrLabelHeaderNW8.Text = Eagle.Resource.LanguageResource.NightWeekend;
            xrLabelHeaderNW9.Text = Eagle.Resource.LanguageResource.NightWeekend;
            xrLabelHeaderNW10.Text = Eagle.Resource.LanguageResource.NightWeekend;
            xrLabelHeaderNW11.Text = Eagle.Resource.LanguageResource.NightWeekend;
            xrLabelHeaderNW12.Text = Eagle.Resource.LanguageResource.NightWeekend;


            //Day - Holiday 
            XRLabel xrLabelHeaderH1 = (XRLabel)_report.FindControl("xrLabelHeaderH1", true);
            XRLabel xrLabelHeaderH2 = (XRLabel)_report.FindControl("xrLabelHeaderH2", true);
            XRLabel xrLabelHeaderH3 = (XRLabel)_report.FindControl("xrLabelHeaderH3", true);
            XRLabel xrLabelHeaderH4 = (XRLabel)_report.FindControl("xrLabelHeaderH4", true);
            XRLabel xrLabelHeaderH5 = (XRLabel)_report.FindControl("xrLabelHeaderH5", true);
            XRLabel xrLabelHeaderH6 = (XRLabel)_report.FindControl("xrLabelHeaderH6", true);
            XRLabel xrLabelHeaderH7 = (XRLabel)_report.FindControl("xrLabelHeaderH7", true);
            XRLabel xrLabelHeaderH8 = (XRLabel)_report.FindControl("xrLabelHeaderH8", true);
            XRLabel xrLabelHeaderH9 = (XRLabel)_report.FindControl("xrLabelHeaderH9", true);
            XRLabel xrLabelHeaderH10 = (XRLabel)_report.FindControl("xrLabelHeaderH10", true);
            XRLabel xrLabelHeaderH11 = (XRLabel)_report.FindControl("xrLabelHeaderH11", true);
            XRLabel xrLabelHeaderH12 = (XRLabel)_report.FindControl("xrLabelHeaderH12", true);

            xrLabelHeaderH1.Text = Eagle.Resource.LanguageResource.DayHoliday;
            xrLabelHeaderH2.Text = Eagle.Resource.LanguageResource.DayHoliday;
            xrLabelHeaderH3.Text = Eagle.Resource.LanguageResource.DayHoliday;
            xrLabelHeaderH4.Text = Eagle.Resource.LanguageResource.DayHoliday;
            xrLabelHeaderH5.Text = Eagle.Resource.LanguageResource.DayHoliday;
            xrLabelHeaderH6.Text = Eagle.Resource.LanguageResource.DayHoliday;
            xrLabelHeaderH7.Text = Eagle.Resource.LanguageResource.DayHoliday;
            xrLabelHeaderH8.Text = Eagle.Resource.LanguageResource.DayHoliday;
            xrLabelHeaderH9.Text = Eagle.Resource.LanguageResource.DayHoliday;
            xrLabelHeaderH10.Text = Eagle.Resource.LanguageResource.DayHoliday;
            xrLabelHeaderH11.Text = Eagle.Resource.LanguageResource.DayHoliday;
            xrLabelHeaderH12.Text = Eagle.Resource.LanguageResource.DayHoliday;

            //Night - Holiday 
            XRLabel xrLabelHeaderNH1 = (XRLabel)_report.FindControl("xrLabelHeaderNH1", true);
            XRLabel xrLabelHeaderNH2 = (XRLabel)_report.FindControl("xrLabelHeaderNH2", true);
            XRLabel xrLabelHeaderNH3 = (XRLabel)_report.FindControl("xrLabelHeaderNH3", true);
            XRLabel xrLabelHeaderNH4 = (XRLabel)_report.FindControl("xrLabelHeaderNH4", true);
            XRLabel xrLabelHeaderNH5 = (XRLabel)_report.FindControl("xrLabelHeaderNH5", true);
            XRLabel xrLabelHeaderNH6 = (XRLabel)_report.FindControl("xrLabelHeaderNH6", true);
            XRLabel xrLabelHeaderNH7 = (XRLabel)_report.FindControl("xrLabelHeaderNH7", true);
            XRLabel xrLabelHeaderNH8 = (XRLabel)_report.FindControl("xrLabelHeaderNH8", true);
            XRLabel xrLabelHeaderNH9 = (XRLabel)_report.FindControl("xrLabelHeaderNH9", true);
            XRLabel xrLabelHeaderNH10 = (XRLabel)_report.FindControl("xrLabelHeaderNH10", true);
            XRLabel xrLabelHeaderNH11 = (XRLabel)_report.FindControl("xrLabelHeaderNH11", true);
            XRLabel xrLabelHeaderNH12 = (XRLabel)_report.FindControl("xrLabelHeaderNH12", true);

            xrLabelHeaderNH1.Text = Eagle.Resource.LanguageResource.NightHoliday;
            xrLabelHeaderNH2.Text = Eagle.Resource.LanguageResource.NightHoliday;
            xrLabelHeaderNH3.Text = Eagle.Resource.LanguageResource.NightHoliday;
            xrLabelHeaderNH4.Text = Eagle.Resource.LanguageResource.NightHoliday;
            xrLabelHeaderNH5.Text = Eagle.Resource.LanguageResource.NightHoliday;
            xrLabelHeaderNH6.Text = Eagle.Resource.LanguageResource.NightHoliday;
            xrLabelHeaderNH7.Text = Eagle.Resource.LanguageResource.NightHoliday;
            xrLabelHeaderNH8.Text = Eagle.Resource.LanguageResource.NightHoliday;
            xrLabelHeaderNH9.Text = Eagle.Resource.LanguageResource.NightHoliday;
            xrLabelHeaderNH10.Text = Eagle.Resource.LanguageResource.NightHoliday;
            xrLabelHeaderNH11.Text = Eagle.Resource.LanguageResource.NightHoliday;
            xrLabelHeaderNH12.Text = Eagle.Resource.LanguageResource.NightHoliday;



            XRLabel xrLabelHeaderOTTotalDay = (XRLabel)_report.FindControl("xrLabelHeaderOTTotalDay", true);
            xrLabelHeaderOTTotalDay.Text = Eagle.Resource.LanguageResource.Day;

            XRLabel xrLabelHeaderOTTotalNight = (XRLabel)_report.FindControl("xrLabelHeaderOTTotalNight", true);
            xrLabelHeaderOTTotalNight.Text = Eagle.Resource.LanguageResource.Night;

            XRLabel xrLabelHeaderOTTotalDayAndNight = (XRLabel)_report.FindControl("xrLabelHeaderOTTotalDayAndNight", true);
            xrLabelHeaderOTTotalDayAndNight.Text = Eagle.Resource.LanguageResource.DayAndNight;

            XRLabel xrLabelHeaderTotals = (XRLabel)_report.FindControl("xrLabelHeaderTotals", true);
            xrLabelHeaderTotals.Text = Eagle.Resource.LanguageResource.Totals;

            XRLabel xrLabelHeaderHourTotalsPerYear = (XRLabel)_report.FindControl("xrLabelHeaderHourTotalsPerYear", true);
            xrLabelHeaderHourTotalsPerYear.Text = Eagle.Resource.LanguageResource.HourTotalsPerYear;

            XRLabel xrLabelHeaderOTRestTotal = (XRLabel)_report.FindControl("xrLabelHeaderOTRestTotal", true);
            xrLabelHeaderOTRestTotal.Text = Eagle.Resource.LanguageResource.RemainHourTotal;
          

            //PageFooter --------------------------------------------------------------------------------------
            XRPageInfo xrPageFooterLeftText = (XRPageInfo)_report.FindControl("xrPageFooterLeftText", true);
            xrPageFooterLeftText.Text = DateTime.Now.ToString("dd-MM-yyyy");

            XRPageInfo xrPageFooterRightText = (XRPageInfo)_report.FindControl("xrPageFooterRightText", true);
            xrPageFooterRightText.Text = Eagle.Resource.LanguageResource.Page;
            //--------------------------------------------------------------------------------------------------
            
            return _report;
        }

        public ActionResult PopulateReportViewer_YearlyOT(int? Year, int? LSCompanyID, string EmpCode, string FullName)
        {

            OTByYearXtraReport tableReport = CreateReport_YearlyOT(Year, LSCompanyID, EmpCode, FullName);
            ViewData["Report"] = tableReport;
            return PartialView("../Report/TS/YearlyOT/_YearlyOTReportViewer");
        }

        public ActionResult ExportData_YearlyOT(int? Year, int? LSCompanyID, string EmpCode, string FullName)
        {
            return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(CreateReport_YearlyOT(Year, LSCompanyID, EmpCode, FullName));
        }
        #endregion OT By Month REPORT ==================================================================


    }
}
