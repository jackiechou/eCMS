using Eagle.Common.Extensions;
using Eagle.Repository.SYS.Session;
using DevExpress.XtraReports.UI;
using Eagle.Repository.Report.TS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Eagle.WebApp.Areas.Admin.Controllers.Report.TS
{
    public class MonthlyTimeKeepingReportController : BaseController
    {
        private int ModuleID = 9;
        //
        // GET: /Admin/TimeKeepingReport/

        [SessionExpiration]
        public ActionResult Index()
        {
            return View("../Report/TS/MonthlyTimeKeeping/Index");
        }


        //#region LOAD REPORT - DETAIL REPORT VIEWER ==========================================================================

        [SessionExpiration]
        [HttpGet]
        public ActionResult LoadReportViewer(int? Month, int? Year, int? LSCompanyID, string EmpCode, string FullName)
        {
            ViewBag.Month = Month;
            ViewBag.Year = Year;
            ViewBag.LSCompanyID = LSCompanyID;
            ViewBag.EmpCode = EmpCode;
            ViewBag.FullName = FullName;
            return PopulateReportViewer_MonthlyTimeKeeping(Month, Year, LSCompanyID, EmpCode, FullName); ;
        }
        //#endregion LOAD REPORT - DETAIL REPORT VIEWER =========================================================================

        #region OT By Month REPORT ==========================================================================================
        private MonthlyTimeKeepingXtraReport CreateReport_MonthlyTimeKeeping(int? Month, int? Year, int? LSCompanyID, string EmpCode, string FullName)
        {
            if (Request.QueryString["ModuleID"] != null && Request.QueryString["ModuleID"] != string.Empty)
                ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);

            MonthlyTimeKeepingXtraReport _report = new MonthlyTimeKeepingXtraReport();
            _report.Name = "MonthlyTimeKeepingXtraReport";
            _report.Landscape = true;
            _report.PaperKind = System.Drawing.Printing.PaperKind.A3;

            MonthlyTimeKeepingReportRespository respository = new MonthlyTimeKeepingReportRespository(db);
            DataTable dtParent = respository.GetMonthlyMasterList(Month, Year, LSCompanyID, EmpCode, FullName, UserName, ModuleID, LanguageId);
            dtParent.TableName = "Timesheet_sprptTimeKeepingByMonth_Master";            
            if (dtParent.Rows.Count == 0)
            {
                var row = dtParent.NewRow();
                row["LSCompanyID"] = -1;
                row["MONTH"] = Month;
                row["YEAR"] = Year;
                dtParent.Rows.Add(row);
            }
            DataTable dtChild = respository.GetMonthlyDetailList(Month, Year, LSCompanyID, EmpCode, FullName, UserName, ModuleID, LanguageId);
            dtChild.TableName = "Timesheet_sprptTimeKeepingByMonth_Details";
            if (dtChild.Rows.Count == 0)
            {
                var row = dtChild.NewRow();
                row["LSCompanyID"] = -1;
                row["MONTH"] = Month;
                row["YEAR"] = Year;
                dtChild.Rows.Add(row);
            }
          
            //Report Header -------------------------------------------------------------------------------------------------------------------------------    
            DataSet DS = DataSetHelper.CreateDataSetJoinedFrom2Tables("ParentChildrenDS", "LSCompanyID", dtParent, dtChild);
            _report.DataSource = DS;
            _report.DataMember = DS.Tables[0].TableName;

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

            //Header -----------------------------------------------------------------------------------------------------------
            XRTable xrTableHeader = (XRTable)_report.FindControl("xrTableHeader", true);
            XRTableRow xrTableRowHeader = (XRTableRow)xrTableHeader.FindControl("xrTableRowHeader", true);
      
            XRTableCell xrTableCellHeaderNo = (XRTableCell)_report.FindControl("xrTableCellHeaderNo", true);
            xrTableCellHeaderNo.Text = Eagle.Resource.LanguageResource.No;

            XRTableCell xrTableCellHeaderEmpCode = (XRTableCell)_report.FindControl("xrTableCellHeaderEmpCode", true);
            xrTableCellHeaderEmpCode.Text = Eagle.Resource.LanguageResource.Code;

            XRTableCell xrTableCellHeaderFullName = (XRTableCell)_report.FindControl("xrTableCellHeaderFullName", true);
            xrTableCellHeaderFullName.Text = Eagle.Resource.LanguageResource.FullName;

            XRTableCell xrTableCellHeaderPosition = (XRTableCell)_report.FindControl("xrTableCellHeaderPosition", true);
            xrTableCellHeaderPosition.Text = Eagle.Resource.LanguageResource.Position;

            XRLabel xrLabelHeaderTitle = (XRLabel)_report.FindControl("xrLabelHeaderTitle", true);
            xrLabelHeaderTitle.Text = Eagle.Resource.LanguageResource.MonthlyTimeKeepingReport;
            
            XRLabel xrLblYear = (XRLabel)_report.FindControl("xrLblYear", true);
            xrLblYear.Text = string.Format("{0}: {1}", Eagle.Resource.LanguageResource.Year, Year);

            string formattedMonth = string.Empty;          
            if (LanguageId == 124)
                formattedMonth = DateTimeFormatInfo.CurrentInfo.GetMonthName((int)Month);
            else
                formattedMonth = Month.ToString();
            XRLabel xrLblMonth = (XRLabel)_report.FindControl("xrLblMonth", true);
            xrLblMonth.Text = string.Format("{0}: {1}", Eagle.Resource.LanguageResource.Month, formattedMonth);


            //Details ------------------------------------------------------------------------------------------------------
            XRTable xrTableDetails = (XRTable)_report.FindControl("xrTableDetails", true);
            XRTableRow xrTableRowDetails = (XRTableRow)xrTableDetails.FindControl("xrTableRowDetails", true);
            XRTableCell xrTableCellHeader31st = (XRTableCell)xrTableRowHeader.FindControl("xrTableCellHeader31st", true);
            XRTableCell xrTableCellHeader30th = (XRTableCell)xrTableRowHeader.FindControl("xrTableCellHeader30th", true);
            XRTableCell xrTableCellHeader29th = (XRTableCell)xrTableRowHeader.FindControl("xrTableCellHeader29th", true);

            XRTableCell xrTableCell31st = (XRTableCell)xrTableRowDetails.FindControl("xrTableCell31st", true);
            XRTableCell xrTableCell30th = (XRTableCell)xrTableRowDetails.FindControl("xrTableCell30th", true);
            XRTableCell xrTableCell29th = (XRTableCell)xrTableRowDetails.FindControl("xrTableCell29th", true);
                           

            if (Month == 1 || Month == 3 || Month == 5 || Month == 5
            || Month == 7 || Month == 8 || Month == 10 || Month == 12)
            {
                //31 ngay
                //do nothing
            }
            else if (Month == 4 || Month == 6 || Month == 9 || Convert.ToInt32(Month) == 11)
            {
                //tong cong la 30 ngay                      
                xrTableRowHeader.Controls.Remove(xrTableCellHeader31st);
                xrTableRowDetails.Controls.Remove(xrTableCell31st);
            }else if(Year % 4 == 0  && Year % 100 != 0 ||  Year% 400  == 0){
                //tong cong 29 ngay                                              
                xrTableRowHeader.Controls.Remove(xrTableCellHeader30th);
                xrTableRowDetails.Controls.Remove(xrTableCell30th);

                xrTableRowHeader.Controls.Remove(xrTableCellHeader31st);
                xrTableRowDetails.Controls.Remove(xrTableCell31st);
            }else
            {
                //Tong cong la 28 ngay
                xrTableRowHeader.Controls.Remove(xrTableCellHeader29th);
                xrTableRowDetails.Controls.Remove(xrTableCell29th);

                xrTableRowHeader.Controls.Remove(xrTableCellHeader30th);
                xrTableRowDetails.Controls.Remove(xrTableCell30th);

                xrTableRowHeader.Controls.Remove(xrTableCellHeader31st);
                xrTableRowDetails.Controls.Remove(xrTableCell31st);
            }

            //PageFooter -----------------------------------------------------------------------------------------            
            XRPageInfo xrPageFooterLeftText = (XRPageInfo)_report.FindControl("xrPageFooterLeftText", true);
            xrPageFooterLeftText.Text = DateTime.Now.ToString("dd-MM-yyyy");

            XRPageInfo xrPageFooterRightText = (XRPageInfo)_report.FindControl("xrPageFooterRightText", true);
            xrPageFooterRightText.Text = Eagle.Resource.LanguageResource.Page;
            //----------------------------------------------------------------------------------------------------

            return _report;
        }

        public ActionResult PopulateReportViewer_MonthlyTimeKeeping(int? Month, int? Year, int? LSCompanyID, string EmpCode, string FullName)
        {

            MonthlyTimeKeepingXtraReport tableReport = CreateReport_MonthlyTimeKeeping(Month, Year, LSCompanyID, EmpCode, FullName);
            ViewData["Report"] = tableReport;
            return PartialView("../Report/TS/MonthlyTimeKeeping/_MonthlyTimeKeepingReportViewer");
        }

        public ActionResult ExportData_MonthlyTimeKeeping(int? Month, int? Year, int? LSCompanyID, string EmpCode, string FullName)
        {
            return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(CreateReport_MonthlyTimeKeeping(Month, Year, LSCompanyID, EmpCode, FullName));
        }
        #endregion OT By Month REPORT ==================================================================

    }
}
