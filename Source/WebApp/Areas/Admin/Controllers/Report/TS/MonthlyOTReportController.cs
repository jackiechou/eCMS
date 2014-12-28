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
using Eagle.Model;

namespace Eagle.WebApp.Areas.Admin.Controllers.Report.TS
{
    public class MonthlyOTReportController : BaseController
    {
        private int FunctionID = 478;
        private int ModuleID = 9;
        //
        // GET: /Admin/MonthlyOTReport/
        [SessionExpiration]
        public ActionResult Index()
        {
            return View("../Report/TS/MonthlyOT/Index");
        }

        #region LOAD REPORT - DETAIL REPORT VIEWER ==========================================================================

        [SessionExpiration]
        [HttpGet]
        public ActionResult LoadReportViewer(int? Month, int? Year, int? LSCompanyID, string EmpCode, string FullName)
        {
            ViewBag.Month = Month;
            ViewBag.Year = Year;
            ViewBag.LSCompanyID = LSCompanyID;
            ViewBag.EmpCode = EmpCode;
            ViewBag.FullName = FullName;
            return PopulateReportViewer_MonthlyOT(Month, Year, LSCompanyID, EmpCode, FullName); ;
        }
        #endregion LOAD REPORT - DETAIL REPORT VIEWER =========================================================================

        #region OT By Month REPORT ==========================================================================================
        private OTByMonthXtraReport CreateReport_MonthlyOT(int? Month, int? Year, int? LSCompanyID, string EmpCode, string FullName)
        {
            if (Request.QueryString["ModuleID"] != null && Request.QueryString["ModuleID"] != string.Empty)
                ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
            OTByMonthXtraReport _report = new OTByMonthXtraReport();
            _report.Name = "OTByMonthXtraReport";
            _report.Landscape = true;
            _report.PaperKind = System.Drawing.Printing.PaperKind.A3;

            MonthlyOTReportRespository respository = new MonthlyOTReportRespository(db);
            DataTable dtParent = respository.GetMonthlyMasterList(Month, Year, LSCompanyID, EmpCode, FullName, UserName, ModuleID, FunctionID, LanguageId);
            dtParent.TableName = "Timesheet_sprptOTByMonth_Master";
            if (dtParent.Rows.Count == 0)
            {
                var row = dtParent.NewRow();
                row["LSCompanyID"] = -1;
                row["Month"] = Month;
                row["Year"] = Year;
                dtParent.Rows.Add(row);
            }
            DataTable dtChild = respository.GetMonthlyDetailList(Month, Year, LSCompanyID, EmpCode, FullName, UserName, ModuleID, FunctionID, LanguageId);
            dtChild.TableName = "Timesheet_sprptOTByMonth_Details";
            if (dtChild.Rows.Count == 0)
            {
                var row = dtChild.NewRow();
                row["LSCompanyID"] = -1;
                row["Month"] = Month;
                row["Year"] = Year;
                dtChild.Rows.Add(row);
            }

            string result = string.Empty;
            foreach (DataRow dr in dtChild.Rows)
            {
                result = dr["TotalHoursOfDayOTInNormalDays"].ToString()
                    + " -" + dr["TotalHoursOfNightOTInNormalDays"].ToString()
                     + " -" + dr["TotalHoursOfOTInNormalDays"].ToString();
            }

            DataSet DS = DataSetHelper.CreateDataSetJoinedFrom2Tables("ParentChildrenDS", "LSCompanyID", dtParent, dtChild);
            _report.DataSource = DS;
            _report.DataMember = DS.Tables[0].TableName;
            _report.DetailReport.DataSource = DS;
            _report.DetailReport.DataMember = String.Format("{0}.{1}", DS.Tables[0].TableName, DS.Relations[0].RelationName);


            // DataTable dtResult = new DataTable();
            // dtResult.Columns.Add("ID", typeof(string));
            // dtResult.Columns.Add("name", typeof(string));
            // dtResult.Columns.Add("stock", typeof(int));

            // var query = from drParent in dtParent.AsEnumerable() 
            //             join drChild in dtChild.AsEnumerable()
            //             on drParent.Field<int>("LSCompanyID") equals drChild.Field<int>("LSCompanyID") into _list
            //             from dr in _list.DefaultIfEmpty()
            //             select new
            //             {
            //                 Seq = dr.Field<int>("Seq"),
            //                 EmpCode = dr.Field<string>("EmpCode"),
            //                 FullName = dr.Field<string>("FullName"),
            //                 LSCompanyID = dr.Field<int>("LSCompanyID"),
            //                 DepartmentName = dr.Field<string>("DepartmentName"),
            //                 DepartmentVNName = dr.Field<string>("DepartmentVNName"),
            //                 PositionName = dr.Field<string>("PositionName"),
            //                 PositionVNName = dr.Field<string>("PositionVNName"),
            //                 Month = dr.Field<int>("Month"),
            //                 Year = dr.Field<int>("Year"),
            //                 TotalHoursOfDayOTInNormalDays = dr.Field<decimal>("TotalHoursOfDayOTInNormalDays"),
            //                 TotalHoursOfNightOTInNormalDays = dr.Field<decimal>("TotalHoursOfNightOTInNormalDays"),
            //                 TotalHoursOfOTInNormalDays = dr.Field<decimal>("TotalHoursOfOTInNormalDays"),
            //                 TotalHoursOfDayOTInHolidays = dr.Field<decimal>("TotalHoursOfDayOTInHolidays"),
            //                 TotalHoursOfNightOTInHolidays = dr.Field<decimal>("TotalHoursOfNightOTInHolidays"),
            //                 TotalHoursOfOTInHolidays = dr.Field<decimal>("TotalHoursOfOTInHolidays"),
            //                 TotalHoursOfDayOTInWeekend = dr.Field<decimal>("TotalHoursOfDayOTInWeekend"),
            //                 TotalHoursOfNightOTInWeekend = dr.Field<decimal>("TotalHoursOfNightOTInWeekend"),
            //                 TotalHoursOfOTInWeekend = dr.Field<decimal>("TotalHoursOfOTInWeekend"),
            //                 TotalHoursOfMonthOT = dr.Field<decimal>("TotalHoursOfMonthOT")
            //             };
            //DataTable dtRelation = query.Copy


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
            //xrTableCellEmail.ForeColor = System.Drawing.Color.Black;
            //xrTableCellEmail.Font = new System.Drawing.Font("Times New Roman", 10, System.Drawing.FontStyle.Bold);

            XRPictureBox xrPictureBoxLogo = (XRPictureBox)_report.FindControl("xrPictureBoxLogo", true);
            xrPictureBoxLogo.ImageUrl = LogoPath;

            XRLabel xrLabelHeaderTitle = (XRLabel)_report.FindControl("xrLabelHeaderTitle", true);
            xrLabelHeaderTitle.Text = Eagle.Resource.LanguageResource.OTMonthlyReport;


            //XRLabel xrLabelMonth = (XRLabel)_report.FindControl("xrLabelMonth", true);
            //xrLabelMonth.DataBindings.Add("Text", DS.Tables[0], "Month", Eagle.Resource.LanguageResource.Month + "{0}");

            XRLabel xrLabelMonth = (XRLabel)_report.FindControl("xrLabelMonth", true);
            XRLabel xrLblMonth = (XRLabel)_report.FindControl("xrLblMonth", true);
            if (LanguageId == 124)
            {
                object _selectedMonth = _report.GetCurrentColumnValue("Month");
                if (_selectedMonth != null)
                {
                    if (!string.IsNullOrEmpty(_selectedMonth.ToString()))
                    {
                        string formattedMonth = DateTimeFormatInfo.CurrentInfo.GetMonthName(Convert.ToInt32(_selectedMonth));
                        xrLabelMonth.Text = formattedMonth;
                        xrLblMonth.Text = string.Format("{0}: {1}", Eagle.Resource.LanguageResource.Month, formattedMonth);
                    }
                }
            }
            else
            {
                //XRBinding monthBinding = new XRBinding("Text", DS.Tables[0], "Month");
                //xrLabelMonth.DataBindings.Add(monthBinding);
                xrLabelMonth.DataBindings.Add("Text", DS.Tables[0], "Month", Eagle.Resource.LanguageResource.Month + " {0}");
                xrLblMonth.DataBindings.Add("Text", DS.Tables[0], "Month", Eagle.Resource.LanguageResource.Month + ": {0}");
            }

            XRLabel xrLblYear = (XRLabel)_report.FindControl("xrLblYear", true);
            xrLblYear.DataBindings.Add("Text", DS.Tables[0], "Year", Eagle.Resource.LanguageResource.Year + ": {0}");         

            //Header                    
            XRLabel xrLabelHeaderNo = (XRLabel)_report.FindControl("xrLabelHeaderNo", true);
            xrLabelHeaderNo.Text = Eagle.Resource.LanguageResource.No;

            XRLabel xrLabelHeaderEmpCode = (XRLabel)_report.FindControl("xrLabelHeaderEmpCode", true);
            xrLabelHeaderEmpCode.Text = Eagle.Resource.LanguageResource.Code;

            XRLabel xrLabelHeaderFullName = (XRLabel)_report.FindControl("xrLabelHeaderFullName", true);
            xrLabelHeaderFullName.Text = Eagle.Resource.LanguageResource.FullName;

            XRLabel xrLabelHeaderPosition = (XRLabel)_report.FindControl("xrLabelHeaderPosition", true);
            xrLabelHeaderPosition.Text = Eagle.Resource.LanguageResource.Position;


            //Day
            XRLabel xrLabelHeaderOTWD = (XRLabel)_report.FindControl("xrLabelHeaderOTWD", true);
            xrLabelHeaderOTWD.Text = Eagle.Resource.LanguageResource.OTWD;

            XRLabel xrLabelHeaderOTNDD = (XRLabel)_report.FindControl("xrLabelHeaderOTNDD", true);
            xrLabelHeaderOTNDD.Text = Eagle.Resource.LanguageResource.OTNDD;

            XRLabel xrLabelHeaderOTHDD = (XRLabel)_report.FindControl("xrLabelHeaderOTHDD", true);
            xrLabelHeaderOTHDD.Text = Eagle.Resource.LanguageResource.OTHDD;

            //Night
            XRLabel xrLabelHeaderOTNDN = (XRLabel)_report.FindControl("xrLabelHeaderOTNDN", true);
            xrLabelHeaderOTNDN.Text = Eagle.Resource.LanguageResource.OTNDN;

            XRLabel xrLabelHeaderOTWN = (XRLabel)_report.FindControl("xrLabelHeaderOTWN", true);
            xrLabelHeaderOTWN.Text = Eagle.Resource.LanguageResource.OTWN;

            XRLabel xrLabelHeaderOTHN = (XRLabel)_report.FindControl("xrLabelHeaderOTHN", true);
            xrLabelHeaderOTHN.Text = Eagle.Resource.LanguageResource.OTHN;

            XRLabel xrLabelHeaderOTTotalByMonth = (XRLabel)_report.FindControl("xrLabelHeaderOTTotalByMonth", true);
            xrLabelHeaderOTTotalByMonth.Text = Eagle.Resource.LanguageResource.OTTotalByMonth;

            XRLabel xrLabelHeaderOTTotalsFromFirstMonthToPresent = (XRLabel)_report.FindControl("xrLabelHeaderOTTotalsFromFirstMonthToPresent", true);
            xrLabelHeaderOTTotalsFromFirstMonthToPresent.Text = Eagle.Resource.LanguageResource.OTTotalsFromFirstMonthToPresent;

            XRLabel xrLabelHeaderOTTotalByYear = (XRLabel)_report.FindControl("xrLabelHeaderOTTotalByYear", true);
            xrLabelHeaderOTTotalByYear.Text = Eagle.Resource.LanguageResource.OTTotalByYear;


            XRLabel xrLabelHeaderOTRestTotal = (XRLabel)_report.FindControl("xrLabelHeaderOTRestTotal", true);
            xrLabelHeaderOTRestTotal.Text = Eagle.Resource.LanguageResource.OTRestTotal;


            //////Details ===========================================================================
            //XRTableCell xrTableCellNo = (XRTableCell)_report.DetailReport.FindControl("xrTableCellNo", true);
            //xrTableCellNo.DataBindings.Add("Text", DS.Tables[1], "Seq");

            //XRTableCell xrTableCellEmpCode = (XRTableCell)_report.DetailReport.FindControl("xrTableCellEmpCode", true);
            //xrTableCellEmpCode.DataBindings.Add("Text", DS.Tables[1], "EmpCode");

            //XRTableCell xrTableCellFullName = (XRTableCell)_report.DetailReport.FindControl("xrTableCellFullName", true);
            //xrTableCellFullName.DataBindings.Add("Text", DS.Tables[1], "FullName");

            //if (LanguageId == 124)
            //{
            //    XRTableCell xrTableCellDepartment = (XRTableCell)_report.DetailReport.FindControl("xrTableCellDepartment", true);
            //    xrTableCellDepartment.DataBindings.Add("Text", DS.Tables[1], "DepartmentName");

            //    XRTableCell xrTableCellPosition = (XRTableCell)_report.DetailReport.FindControl("xrTableCellPosition", true);
            //    xrTableCellPosition.DataBindings.Add("Text", DS.Tables[1], "PositionName");
            //}
            //else
            //{
            //    XRTableCell xrTableCellDepartment = (XRTableCell)_report.DetailReport.FindControl("xrTableCellDepartment", true);
            //    xrTableCellDepartment.DataBindings.Add("Text", DS.Tables[1], "DepartmentVNName");

            //    XRTableCell xrTableCellPosition = (XRTableCell)_report.DetailReport.FindControl("xrTableCellPosition", true);
            //    xrTableCellPosition.DataBindings.Add("Text", DS.Tables[1], "PositionVNName");
            //}


            // XRTableCell xrTableCellOTNDD = (XRTableCell)_report.DetailReport.FindControl("xrTableCellOTNDD", true);
            //xrTableCellOTNDD.DataBindings.Add("Text", DS.Tables[1], "TotalHoursOfDayOTInNormalDays", "{0:0.0}");       


            //XRTableCell xrTableCellOTWD = (XRTableCell)_report.DetailReport.FindControl("xrTableCellOTWD", true);
            //xrTableCellOTWD.DataBindings.Add("Text", DS.Tables[1], "TotalHoursOfDayOTInWeekend", "{0:0.0}");

            //XRTableCell xrTableCellOTHDD = (XRTableCell)_report.DetailReport.FindControl("xrTableCellOTHDD", true);
            //xrTableCellOTHDD.DataBindings.Add("Text", DS.Tables[1], "TotalHoursOfDayOTInHolidays", "{0:0.0}");

            ////Night
            //XRTableCell xrTableCellOTNDN = (XRTableCell)_report.DetailReport.FindControl("xrTableCellOTNDN", true);
            //xrTableCellOTNDN.DataBindings.Add("Text", DS.Tables[1], "TotalHoursOfNightOTInNormalDays", "{0:0.0}");

            //XRTableCell xrTableCellOTWN = (XRTableCell)_report.DetailReport.FindControl("xrTableCellOTWN", true);
            //xrTableCellOTWN.DataBindings.Add("Text", DS.Tables[1], "TotalHoursOfNightOTInWeekend", "{0:0.0}");

            //XRTableCell xrTableCellOTHN = (XRTableCell)_report.DetailReport.FindControl("xrTableCellOTHN", true);
            //xrTableCellOTHN.DataBindings.Add("Text", DS.Tables[1], "TotalHoursOfNightOTInHolidays", "{0:0.0}");

            ////Totals
            //XRTableCell xrTableCellOTTotalByMonth = (XRTableCell)_report.DetailReport.FindControl("xrTableCellOTTotalByMonth", true);
            //xrTableCellOTTotalByMonth.DataBindings.Add("Text", DS.Tables[1], "TotalHoursOfMonthOT", "{0:0.0}");

            //XRTableCell xrTableCellOTTotalsFromFirstMonthToSelectedMonth = (XRTableCell)_report.DetailReport.FindControl("xrTableCellOTTotalsFromFirstMonthToSelectedMonth", true);
            //xrTableCellOTTotalsFromFirstMonthToSelectedMonth.DataBindings.Add("Text", DS, "OTTotalsFromFirstMonthToSelectedMonth", "{0:0.0}");

            //XRTableCell xrTableCellOTTotalByYear = (XRTableCell)_report.DetailReport.FindControl("xrTableCellOTTotalByYear", true);
            //xrTableCellOTTotalByYear.DataBindings.Add("Text", DS.Tables[1], "TotalHoursOfYear", "{0:0.0}");

            //XRTableCell xrTableCellOTRestTotal = (XRTableCell)_report.DetailReport.FindControl("xrTableCellOTRestTotal", true);
            //xrTableCellOTRestTotal.DataBindings.Add("Text", DS.Tables[1], "OTRestTotal", "{0:0.0}");

            //PageFooter --------------------------------------------------------------------------------------
            XRPageInfo xrPageFooterLeftText = (XRPageInfo)_report.FindControl("xrPageFooterLeftText", true);
            xrPageFooterLeftText.Text = DateTime.Now.ToString("dd-MM-yyyy");

            XRPageInfo xrPageFooterRightText = (XRPageInfo)_report.FindControl("xrPageFooterRightText", true);
            xrPageFooterRightText.Text = Eagle.Resource.LanguageResource.Page;
            //--------------------------------------------------------------------------------------------------

            return _report;
        }

        public ActionResult PopulateReportViewer_MonthlyOT(int? Month, int? Year, int? LSCompanyID, string EmpCode, string FullName)
        {

            OTByMonthXtraReport tableReport = CreateReport_MonthlyOT(Month, Year, LSCompanyID, EmpCode, FullName);
            ViewData["Report"] = tableReport;
            return PartialView("../Report/TS/MonthlyOT/_MonthlyOTReportViewer");
        }

        public ActionResult ExportData_MonthlyOT(int? Month, int? Year, int? LSCompanyID, string EmpCode, string FullName)
        {
            return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(CreateReport_MonthlyOT(Month, Year, LSCompanyID, EmpCode, FullName));
        }
        #endregion OT By Month REPORT ==================================================================

    }
}
