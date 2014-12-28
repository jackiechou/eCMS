using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Repository.SYS.Session;
using DevExpress.Web.Mvc.UI;
using Eagle.Model.HR;
using Eagle.Repository.HR;
using System.Data;
using Eagle.Common.Entities;
using System.Web.Optimization;
using Eagle.Model.Report.HR.Contract;
using Eagle.Model.Report.HR.Position;
using Eagle.Model.Report.HR.Department;
using Eagle.Model.Report.HR.Qualification;
using Eagle.Model.Report.HR;
using Eagle.Model.Report.Common;
using DevExpress.XtraPivotGrid;
using System.Web.SessionState;
using DevExpress.Web.Mvc;
using Eagle.Core;
using Eagle.Common.Extensions;
using Eagle.Repository.Report;
using DevExpress.XtraReports.UI;
using Eagle.Repository.Report.HR;
using System.Text;
using Eagle.Common.Settings;


namespace Eagle.WebApp.Areas.Admin.Controllers.Report.HR
{

    public class PersonnelReportController : BaseController
    {
        //
        // GET: /Admin/HRReport/
        [SessionExpiration]
        public ActionResult Index()
        {
            //PopulateCareerStatusToDropDownList();
            //PopulateStatisticTypeToDropDownList();
            PopulateStatisticFocusToDropDownList();
            PersonnelStatisticModel model = new PersonnelStatisticModel();
            return View("../Report/HR/PersonnelReport", model);
        }

        #region LOAD REPORT - DETAIL REPORT VIEWER =======================
        [HttpGet]
        public ActionResult LoadMasterReportToolbar()
        {
            return PartialView("../Report/HR/_ReportToolbar_Master");
        }

        [HttpGet]
        public ActionResult LoadDetailReportToolbar()
        {
            return PartialView("../Report/HR/_ReportToolbar_Details");
        }
        

        [SessionExpiration]
        [HttpGet]
        public ActionResult LoadReportViewer(int Id, int? LSCompanyID)
        {
            ActionResult result;
            string reportPartialPage = string.Empty;
            ViewData["LSCompanyID"] = LSCompanyID; 
            switch (Id)
            {
                case 1:
                    result = PopulateReportViewer_Department(LSCompanyID);
                    break;
                case 2:
                    result = PopulateReportViewer_Qualification(LSCompanyID);
                    break;
                case 3:
                    result = PopulateReportViewer_Position(LSCompanyID);
                    break;
                case 4:
                    result = PopulateReportViewer_Contract(LSCompanyID);
                    break;
                default:
                    result = PopulateReportViewer_Department(LSCompanyID);
                    break;
            }
            return result;
        }

        [SessionExpiration]
        [HttpGet]
        public ActionResult LoadDetailReportViewer(int Id, int? LSCompanyID)
        {
            ActionResult result;
            string reportPartialPage = string.Empty;
            ViewData["LSCompanyID"] = LSCompanyID; 
            switch (Id)
            {
                case 1:
                    result = PopulateReportViewer_DepartmentDetail(LSCompanyID);
                    break;
                case 2:
                    result = PopulateReportViewer_QualificationDetail(LSCompanyID);
                    break;
                case 3:
                    result = PopulateReportViewer_PositionDetail(LSCompanyID);
                    break;
                case 4:
                    result = PopulateReportViewer_ContractDetail(LSCompanyID);
                    break;
                default:
                    result = PopulateReportViewer_DepartmentDetail(LSCompanyID);
                    break;
            }
            return result;
        }

        #endregion LOAD REPORT - DETAIL REPORT VIEWER ===================



        #region MAIN REPORT - DETAIL REPORT VIEWER =======================

            #region Department REPORT =====================================================================
            private DepartmentXtraReport CreateReport_Department(int? LSCompanyID)
            {
                DataSet DS = HRReportRespository.GetListByDepartment(LSCompanyID, LanguageId);
                int rowCount = DS.Tables[0].Rows.Count;
                DepartmentXtraReport _report = new DepartmentXtraReport();
                _report.Name = "DepartmentXtraReport";
                _report.DataSource = DS;
                _report.DataSourceSchema = DS.GetXmlSchema();
                _report.DataMember = "HR_sprptEmployee_GetListByDepartment";

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
                xrLabelHeaderTitle.Text = Eagle.Resource.LanguageResource.EmployeeStatisticsByDepartment;

                //Header -----------------------------------------------------------------------------------------------------      
                XRTableCell xrTableCellHeaderSeq = (XRTableCell)_report.FindControl("xrTableCellHeaderSeq", true);
                xrTableCellHeaderSeq.Text = Eagle.Resource.LanguageResource.Seq2;

                XRTableCell xrTableCellHeaderLSCompanyCode = (XRTableCell)_report.FindControl("xrTableCellHeaderLSCompanyCode", true);
                xrTableCellHeaderLSCompanyCode.Text = Eagle.Resource.LanguageResource.CompanyCode;                

                XRTableCell xrTableCellHeaderDepartment = (XRTableCell)_report.FindControl("xrTableCellHeaderDepartment", true);
                xrTableCellHeaderDepartment.Text = Eagle.Resource.LanguageResource.Department;

                XRTableCell xrTableCellHeaderQuantity = (XRTableCell)_report.FindControl("xrTableCellHeaderQuantity", true);
                xrTableCellHeaderQuantity.Text = Eagle.Resource.LanguageResource.TotalOfStaffs;

                XRTableCell xrTableCellHeaderPercentage = (XRTableCell)_report.FindControl("xrTableCellHeaderPercentage", true);
                xrTableCellHeaderPercentage.Text = Eagle.Resource.LanguageResource.Percentage;

                //Details
                XRTableCell xrTableCellSeq = (XRTableCell)_report.FindControl("xrTableCellSeq", true);
                xrTableCellSeq.DataBindings.Add("Text", DS, "Seq");

                XRTableCell xrTableCellLSCompanyCode = (XRTableCell)_report.FindControl("xrTableCellLSCompanyCode", true);
                xrTableCellLSCompanyCode.DataBindings.Add("Text", DS, "LSCompanyCode");

                XRTableCell xrTableCellDepartmentName = (XRTableCell)_report.FindControl("xrTableCellDepartmentName", true);
                xrTableCellDepartmentName.DataBindings.Add("Text", DS, "Department");

                XRTableCell xrTableCellQuantity = (XRTableCell)_report.FindControl("xrTableCellQuantity", true);
                xrTableCellQuantity.DataBindings.Add("Text", DS, "Qty");

                XRTableCell xrTableCellPercentage = (XRTableCell)_report.FindControl("xrTableCellPercentage", true);
                xrTableCellPercentage.DataBindings.Add("Text", DS, "Percentage", "{0} %");

                //PageFooter --------------------------------------------------------------------------------------
                XRPageInfo xrPageFooterLeftText = (XRPageInfo)_report.FindControl("xrPageFooterLeftText", true);
                xrPageFooterLeftText.Text = DateTime.Now.ToString("dd-MM-yyyy");

                XRPageInfo xrPageFooterRightText = (XRPageInfo)_report.FindControl("xrPageFooterRightText", true);
                xrPageFooterRightText.Text = Eagle.Resource.LanguageResource.Page;
                //--------------------------------------------------------------------------------------------------

                _report.FillDataSource();
                return _report;
            }

            public ActionResult PopulateReportViewer_Department(int? LSCompanyID)
            {
                DepartmentXtraReport tableReport = CreateReport_Department(LSCompanyID);
                ViewData["Report"] = tableReport;
                return PartialView("../Report/HR/Department/_DepartmentReportViewer");
            }

            public ActionResult ExportData_Department(int? LSCompanyID)
            {
                return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(CreateReport_Department(LSCompanyID));
            }


            //Detail
            private DepartmentDetailXtraReport CreateReport_DepartmentDetail(int? LSCompanyID)
            {
                DataTable dtParent = HRReportRespository.GetListByDepartment_Master(LSCompanyID, LanguageId);
                DataTable dtChild = HRReportRespository.GetListByDepartment_Details(LSCompanyID, LanguageId);

                DataSet DS = DataSetHelper.CreateDataSetJoinedFrom2Tables("ParentChildrenDS", "LSCompanyID", dtParent, dtChild);
                DepartmentDetailXtraReport detail_report = new DepartmentDetailXtraReport();
                detail_report.DataSource = DS;
                detail_report.DataMember = DS.Tables[0].TableName;
                detail_report.DetailReport.DataSource = DS;
                detail_report.DetailReport.DataMember = String.Format("{0}.{1}", DS.Tables[0].TableName, DS.Relations[0].RelationName);

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


                XRTable xrTableCompany = (XRTable)detail_report.FindControl("xrTableCompany", true);
                XRTableCell xrTableCellLabelCompanyName = (XRTableCell)detail_report.FindControl("xrTableCellLabelCompanyName", true);
                XRTableCell xrTableCellCompanyName = (XRTableCell)detail_report.FindControl("xrTableCellCompanyName", true);
                xrTableCellLabelCompanyName.Text = Eagle.Resource.LanguageResource.Headquarter;
                xrTableCellCompanyName.Text = CompanyName;

                XRTableCell xrTableCellLabelAddress = (XRTableCell)detail_report.FindControl("xrTableCellLabelAddress", true);
                XRTableCell xrTableCellAddress = (XRTableCell)detail_report.FindControl("xrTableCellAddress", true);
                xrTableCellLabelAddress.Text = Eagle.Resource.LanguageResource.Address;
                xrTableCellAddress.Text = Address;

                XRTableCell xrTableCellLabelPhone = (XRTableCell)detail_report.FindControl("xrTableCellLabelPhone", true);
                XRTableCell xrTableCellPhone = (XRTableCell)detail_report.FindControl("xrTableCellPhone", true);
                xrTableCellLabelPhone.Text = Eagle.Resource.LanguageResource.Phone;
                xrTableCellPhone.Text = Tel;

                XRTableCell xrTableCellLabelFax = (XRTableCell)detail_report.FindControl("xrTableCellLabelFax", true);
                XRTableCell xrTableCellFax = (XRTableCell)detail_report.FindControl("xrTableCellFax", true);
                xrTableCellLabelFax.Text = Eagle.Resource.LanguageResource.Fax;
                xrTableCellFax.Text = Fax;

                XRTableCell xrTableCellLabelEmail = (XRTableCell)detail_report.FindControl("xrTableCellLabelEmail", true);
                XRTableCell xrTableCellEmail = (XRTableCell)detail_report.FindControl("xrTableCellEmail", true);
                xrTableCellLabelEmail.Text = Eagle.Resource.LanguageResource.Email;
                xrTableCellEmail.Text = Email;


                XRPictureBox xrPictureBoxLogo = (XRPictureBox)detail_report.FindControl("xrPictureBoxLogo", true);
                xrPictureBoxLogo.ImageUrl = LogoPath;

                XRLabel xrLabelHeaderTitle = (XRLabel)detail_report.FindControl("xrLabelHeaderTitle", true);
                xrLabelHeaderTitle.Text = Eagle.Resource.LanguageResource.EmployeeStatisticsByDepartmentDetails;
                

                //Header ----------------------------------------------------------------------------------------------------- 
                XRTableCell xrTableCellHeaderSeq = (XRTableCell)detail_report.FindControl("xrTableCellHeaderSeq", true);
                xrTableCellHeaderSeq.Text = Eagle.Resource.LanguageResource.Seq2;

                XRTableCell xrTableCellHeaderEmpCode = (XRTableCell)detail_report.FindControl("xrTableCellHeaderEmpCode", true);
                xrTableCellHeaderEmpCode.Text = Eagle.Resource.LanguageResource.EmpCode;

                XRTableCell xrTableCellHeaderPosition = (XRTableCell)detail_report.FindControl("xrTableCellHeaderPosition", true);
                xrTableCellHeaderPosition.Text = Eagle.Resource.LanguageResource.Position;

                XRTableCell xrTableCellHeaderGender = (XRTableCell)detail_report.FindControl("xrTableCellHeaderGender", true);
                xrTableCellHeaderGender.Text = Eagle.Resource.LanguageResource.Gender;

                XRTableCell xrTableCellHeaderMarital = (XRTableCell)detail_report.FindControl("xrTableCellHeaderMarital", true);
                xrTableCellHeaderMarital.Text = Eagle.Resource.LanguageResource.Marital;               

                XRTableCell xrTableCellHeaderDOB = (XRTableCell)detail_report.FindControl("xrTableCellHeaderDOB", true);
                xrTableCellHeaderDOB.Text = Eagle.Resource.LanguageResource.DOB;

                XRTableCell xrTableCellHeaderJoinDate = (XRTableCell)detail_report.FindControl("xrTableCellHeaderJoinDate", true);
                xrTableCellHeaderJoinDate.Text = Eagle.Resource.LanguageResource.JoinDate;

                //Details
                XRTableCell xrTableCellDepartment = (XRTableCell)detail_report.FindControl("xrTableCellDepartment", true);
                xrTableCellDepartment.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}", DS.Tables[0].TableName, "Department"))});

                XRTableCell xrTableCellSeq = (XRTableCell)detail_report.FindControl("xrTableCellSeq", true);
                xrTableCellSeq.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "Seq"))});

                XRTableCell xrTableCellEmpCode = (XRTableCell)detail_report.FindControl("xrTableCellEmpCode", true);
                xrTableCellEmpCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "EmpCode"))});

                XRTableCell xrTableCellFullName = (XRTableCell)detail_report.FindControl("xrTableCellFullName", true);
                xrTableCellFullName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "FullName"))});
                
                XRTableCell xrTableCellPosition = (XRTableCell)detail_report.FindControl("xrTableCellPosition", true);
                xrTableCellPosition.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "Position"))});

                XRTableCell xrTableCellGender = (XRTableCell)detail_report.FindControl("xrTableCellGender", true);
                xrTableCellGender.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "Gender"))});
                
                XRTableCell xrTableCellMarital = (XRTableCell)detail_report.FindControl("xrTableCellMarital", true);
                xrTableCellMarital.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "Marital"))});
                
                XRTableCell xrTableCellDOB = (XRTableCell)detail_report.FindControl("xrTableCellDOB", true);
                xrTableCellDOB.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "DOB"))});
                XRTableCell xrTableCellJoinDate = (XRTableCell)detail_report.FindControl("xrTableCellJoinDate", true);
                xrTableCellJoinDate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "JoinDate"))});


                //PageFooter --------------------------------------------------------------------------------------
                XRPageInfo xrPageFooterLeftText = (XRPageInfo)detail_report.FindControl("xrPageFooterLeftText", true);
                xrPageFooterLeftText.Text = DateTime.Now.ToString("dd-MM-yyyy");

                XRPageInfo xrPageFooterRightText = (XRPageInfo)detail_report.FindControl("xrPageFooterRightText", true);
                xrPageFooterRightText.Text = Eagle.Resource.LanguageResource.Page;
                //--------------------------------------------------------------------------------------------------

                detail_report.FillDataSource();                
                return detail_report;
            }

            public ActionResult PopulateReportViewer_DepartmentDetail(int? LSCompanyID)
            {
                DepartmentDetailXtraReport tableDetailReport = CreateReport_DepartmentDetail(LSCompanyID);
                ViewData["ReportDetail"] = tableDetailReport;
                return PartialView("../Report/HR/Department/_DepartmentDetailReportViewer");                
            }

            public ActionResult ExportData_DepartmentDetail(int? LSCompanyID)
            {
                return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(CreateReport_DepartmentDetail(LSCompanyID));
            }
            #endregion Deparment REPORT ==================================================================

            #region Contract REPORT ====================================================================
            private ContractXtraReport CreateReport_Contract(int? LSCompanyID)
            {
                DataSet DS = HRReportRespository.GetData_Contract(LSCompanyID, LanguageId);
                ContractXtraReport _report = new ContractXtraReport();
                int rowCount = DS.Tables[0].Rows.Count; 
                _report.DataSource = DS;
                _report.DataSourceSchema = DS.GetXmlSchema();
                _report.DataMember = "HR_sprptEmployee_GetListByContractType";

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
                xrLabelHeaderTitle.Text = Eagle.Resource.LanguageResource.EmployeeStatisticsByContractTypes;

                //Header ------------------------------------------------------------------------------------------------------------------------------------
                XRTableCell xrTableCellHeaderSeq = (XRTableCell)_report.FindControl("xrTableCellHeaderSeq", true);
                xrTableCellHeaderSeq.Text = Eagle.Resource.LanguageResource.Seq2;


                XRTableCell xrTableCellHeaderLSContractTypeCode= (XRTableCell)_report.FindControl("xrTableCellHeaderLSContractTypeCode", true);
                xrTableCellHeaderLSContractTypeCode.Text = Eagle.Resource.LanguageResource.LSContractTypeCode;

                XRTableCell xrTableCellHeaderContractTypeName = (XRTableCell)_report.FindControl("xrTableCellHeaderContractTypeName", true);
                xrTableCellHeaderContractTypeName.Text = Eagle.Resource.LanguageResource.ContractType;

                XRTableCell xrTableCellHeaderTotalMaleContractsByLSContractTypeIDAndMaleGender = (XRTableCell)_report.FindControl("xrTableCellHeaderTotalMaleContractsByLSContractTypeIDAndMaleGender", true);
                xrTableCellHeaderTotalMaleContractsByLSContractTypeIDAndMaleGender.Text = Eagle.Resource.LanguageResource.TotalMaleContractsByLSContractTypeIDAndMaleGender;

                XRTableCell xrTableCellHeaderTotalFeMaleContractsByLSContractTypeIDAndMaleGender = (XRTableCell)_report.FindControl("xrTableCellHeaderTotalFeMaleContractsByLSContractTypeIDAndMaleGender", true);
                xrTableCellHeaderTotalFeMaleContractsByLSContractTypeIDAndMaleGender.Text = Eagle.Resource.LanguageResource.TotalFeMaleContractsByLSContractTypeIDAndMaleGender;

                XRTableCell xrTableCellHeaderTotalContractsByLSContractTypeID = (XRTableCell)_report.FindControl("xrTableCellHeaderTotalContractsByLSContractTypeID", true);
                xrTableCellHeaderTotalContractsByLSContractTypeID.Text = Eagle.Resource.LanguageResource.TotalContractsByLSContractTypeID;

                XRTableCell xrTableCellHeaderTotalPercentMaleContractsByLSContractTypeIDAndMaleGender = (XRTableCell)_report.FindControl("xrTableCellHeaderTotalPercentMaleContractsByLSContractTypeIDAndMaleGender", true);
                xrTableCellHeaderTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.Text = Eagle.Resource.LanguageResource.TotalPercentMaleContractsByLSContractTypeIDAndMaleGender;

                XRTableCell xrTableCellHeaderTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender = (XRTableCell)_report.FindControl("xrTableCellHeaderTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender", true);
                xrTableCellHeaderTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.Text = Eagle.Resource.LanguageResource.TotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender;

                XRTableCell xrTableCellHeaderTotalPercentByLSContractTypeID = (XRTableCell)_report.FindControl("xrTableCellHeaderTotalPercentByLSContractTypeID", true);
                xrTableCellHeaderTotalPercentByLSContractTypeID.Text = Eagle.Resource.LanguageResource.TotalPercentByLSContractTypeID;

                //Details --------------------------------------------------------------------------------------------------------------------------------
                XRTableCell xrTableCellSeq = (XRTableCell)_report.FindControl("xrTableCellSeq", true);
                xrTableCellSeq.DataBindings.Add("Text", DS, "Seq");

                XRTableCell xrTableCellLSContractTypeCode = (XRTableCell)_report.FindControl("xrTableCellLSContractTypeCode", true);
                xrTableCellLSContractTypeCode.DataBindings.Add("Text", DS, "LSContractTypeCode");

                XRTableCell xrTableCellContractTypeName = (XRTableCell)_report.FindControl("xrTableCellContractTypeName", true);
                xrTableCellContractTypeName.DataBindings.Add("Text", DS, "ContractTypeName");

                XRTableCell xrTableCellTotalMaleContractsByLSContractTypeIDAndMaleGender = (XRTableCell)_report.FindControl("xrTableCellTotalMaleContractsByLSContractTypeIDAndMaleGender", true);
                xrTableCellTotalMaleContractsByLSContractTypeIDAndMaleGender.DataBindings.Add("Text", DS, "TotalMaleContractsByLSContractTypeIDAndMaleGender");

                XRTableCell xrTableCellTotalFeMaleContractsByLSContractTypeIDAndMaleGender = (XRTableCell)_report.FindControl("xrTableCellTotalFeMaleContractsByLSContractTypeIDAndMaleGender", true);
                xrTableCellTotalFeMaleContractsByLSContractTypeIDAndMaleGender.DataBindings.Add("Text", DS, "TotalFeMaleContractsByLSContractTypeIDAndMaleGender");


                XRTableCell xrTableCellTotalContractsByLSContractTypeID = (XRTableCell)_report.FindControl("xrTableCellTotalContractsByLSContractTypeID", true);
                xrTableCellTotalContractsByLSContractTypeID.DataBindings.Add("Text", DS, "TotalContractsByLSContractTypeID");

                XRTableCell xrTableCellTotalPercentMaleContractsByLSContractTypeIDAndMaleGender = (XRTableCell)_report.FindControl("xrTableCellTotalPercentMaleContractsByLSContractTypeIDAndMaleGender", true);
                xrTableCellTotalPercentMaleContractsByLSContractTypeIDAndMaleGender.DataBindings.Add("Text", DS, "TotalPercentMaleContractsByLSContractTypeIDAndMaleGender", "{0} %");

                XRTableCell xrTableCellTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender = (XRTableCell)_report.FindControl("xrTableCellTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender", true);
                xrTableCellTotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender.DataBindings.Add("Text", DS, "TotalPercentFeMaleContractsByLSContractTypeIDAndMaleGender", "{0} %");

                XRTableCell xrTableCellTotalPercentByLSContractTypeID = (XRTableCell)_report.FindControl("xrTableCellTotalPercentByLSContractTypeID", true);
                xrTableCellTotalPercentByLSContractTypeID.DataBindings.Add("Text", DS, "TotalPercentByLSContractTypeID", "{0} %");

                //PageFooter --------------------------------------------------------------------------------------
                XRPageInfo xrPageFooterLeftText = (XRPageInfo)_report.FindControl("xrPageFooterLeftText", true);
                xrPageFooterLeftText.Text = DateTime.Now.ToString("dd-MM-yyyy");

                XRPageInfo xrPageFooterRightText = (XRPageInfo)_report.FindControl("xrPageFooterRightText", true);
                xrPageFooterRightText.Text = Eagle.Resource.LanguageResource.Page;
                //--------------------------------------------------------------------------------------------------

                _report.FillDataSource();               
                return _report;
            }
                   
            private int GetHeight(DevExpress.XtraReports.UI.XtraReport report, int totalsRows)
            {
                int totalHeight = -1;

                foreach (DevExpress.XtraReports.UI.Band band in report.Bands)
                {
                    if (band.GetType() == typeof(DevExpress.XtraReports.UI.DetailBand))
                    {
                        totalHeight += Convert.ToInt32(band.HeightF) * totalsRows;
                    }
                    else
                    {
                        if (band.Visible)
                            totalHeight += Convert.ToInt32(band.HeightF);
                    }
                }
                return totalHeight;
            }

            [SessionExpiration]
            public ActionResult PopulateReportViewer_Contract(int? LSCompanyID)
            {
                if (Session[SettingKeys.UserId] != null)
                {
                    ContractXtraReport tableReport = CreateReport_Contract(LSCompanyID);
                    ViewData["Report"] = tableReport;
                    return PartialView("../Report/HR/Contract/_ContractReportViewer");
                    //List<ContractReportModel> lst = HRReportRespository.GetListByContractTypeID(LanguageId);
                    //return PartialView("../Report/HR/Contract/_ContractReportViewer", lst);
                }
                else
                    return null;
            }

            public ActionResult ExportData_Contract(int? LSCompanyID)
            {
                return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(CreateReport_Contract(LSCompanyID));
            }

            //detail
            private ContractDetailXtraReport CreateReport_ContractDetail(int? LSCompanyID)
            {
                DataTable dtParent = HRReportRespository.GetListByContractType_Master(LSCompanyID, LanguageId);
                DataTable dtChild = HRReportRespository.GetListByContractType_Details(LSCompanyID, LanguageId);
                //int rowCount = dtParent.Rows.Count; 

                DataSet DS = DataSetHelper.CreateDataSetJoinedFrom2Tables("ParentChildrenDS", "LSContractTypeID", dtParent, dtChild);
                ContractDetailXtraReport detail_report = new ContractDetailXtraReport();
                detail_report.DataSource = DS;              
                detail_report.DataMember = DS.Tables[0].TableName;
                detail_report.DetailReport.DataSource = DS;
                detail_report.DetailReport.DataMember = String.Format("{0}.{1}", DS.Tables[0].TableName, DS.Relations[0].RelationName);
                
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


                XRTable xrTableCompany = (XRTable)detail_report.FindControl("xrTableCompany", true);
                XRTableCell xrTableCellLabelCompanyName = (XRTableCell)detail_report.FindControl("xrTableCellLabelCompanyName", true);
                XRTableCell xrTableCellCompanyName = (XRTableCell)detail_report.FindControl("xrTableCellCompanyName", true);
                xrTableCellLabelCompanyName.Text = Eagle.Resource.LanguageResource.Headquarter;
                xrTableCellCompanyName.Text = CompanyName;

                XRTableCell xrTableCellLabelAddress = (XRTableCell)detail_report.FindControl("xrTableCellLabelAddress", true);
                XRTableCell xrTableCellAddress = (XRTableCell)detail_report.FindControl("xrTableCellAddress", true);
                xrTableCellLabelAddress.Text = Eagle.Resource.LanguageResource.Address;
                xrTableCellAddress.Text = Address;

                XRTableCell xrTableCellLabelPhone = (XRTableCell)detail_report.FindControl("xrTableCellLabelPhone", true);
                XRTableCell xrTableCellPhone = (XRTableCell)detail_report.FindControl("xrTableCellPhone", true);
                xrTableCellLabelPhone.Text = Eagle.Resource.LanguageResource.Phone;
                xrTableCellPhone.Text = Tel;

                XRTableCell xrTableCellLabelFax = (XRTableCell)detail_report.FindControl("xrTableCellLabelFax", true);
                XRTableCell xrTableCellFax = (XRTableCell)detail_report.FindControl("xrTableCellFax", true);
                xrTableCellLabelFax.Text = Eagle.Resource.LanguageResource.Fax;
                xrTableCellFax.Text = Fax;

                XRTableCell xrTableCellLabelEmail = (XRTableCell)detail_report.FindControl("xrTableCellLabelEmail", true);
                XRTableCell xrTableCellEmail = (XRTableCell)detail_report.FindControl("xrTableCellEmail", true);
                xrTableCellLabelEmail.Text = Eagle.Resource.LanguageResource.Email;
                xrTableCellEmail.Text = Email;


                XRPictureBox xrPictureBoxLogo = (XRPictureBox)detail_report.FindControl("xrPictureBoxLogo", true);
                xrPictureBoxLogo.ImageUrl = LogoPath;

                XRLabel xrLabelHeaderTitle = (XRLabel)detail_report.FindControl("xrLabelHeaderTitle", true);
                xrLabelHeaderTitle.Text = Eagle.Resource.LanguageResource.EmployeeStatisticsByContractTypeDetails;

                //Header ------------------------------------------------------------------------------------------------------------------------------------
                XRTableCell xrTableCellHeaderSeq = (XRTableCell)detail_report.FindControl("xrTableCellHeaderSeq", true);
                xrTableCellHeaderSeq.Text = Eagle.Resource.LanguageResource.Seq2;

                XRTableCell xrTableCellHeaderFullName = (XRTableCell)detail_report.FindControl("xrTableCellHeaderFullName", true);
                xrTableCellHeaderFullName.Text = Eagle.Resource.LanguageResource.FullName;

                XRTableCell xrTableCellHeaderPosition = (XRTableCell)detail_report.FindControl("xrTableCellHeaderPosition", true);
                xrTableCellHeaderPosition.Text = Eagle.Resource.LanguageResource.Position;

                XRTableCell xrTableCellHeaderDepartment = (XRTableCell)detail_report.FindControl("xrTableCellHeaderDepartment", true);
                xrTableCellHeaderDepartment.Text = Eagle.Resource.LanguageResource.Department;

                XRTableCell xrTableCellHeaderEffectiveDate = (XRTableCell)detail_report.FindControl("xrTableCellHeaderEffectiveDate", true);
                xrTableCellHeaderEffectiveDate.Text = Eagle.Resource.LanguageResource.EffectiveDate;

                XRTableCell xrTableCellHeaderExpiredDate = (XRTableCell)detail_report.FindControl("xrTableCellHeaderExpiredDate", true);
                xrTableCellHeaderExpiredDate.Text = Eagle.Resource.LanguageResource.ExpiredDate;


                //Details -------------------------------------------------------------------------------------------------------------------------------  
                XRTableCell xrTableCellLSContractTypeName = (XRTableCell)detail_report.FindControl("xrTableCellLSContractTypeName", true);
                xrTableCellLSContractTypeName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}", DS.Tables[0].TableName, "LSContractTypeName"))});

                XRTableCell xrTableCellSeq = (XRTableCell)detail_report.FindControl("xrTableCellSeq", true);
                xrTableCellSeq.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "Seq"))});

                XRTableCell xrTableCellFullName = (XRTableCell)detail_report.FindControl("xrTableCellFullName", true);
                xrTableCellFullName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "FullName"))});

                XRTableCell xrTableCellPosition = (XRTableCell)detail_report.FindControl("xrTableCellPosition", true);
                xrTableCellPosition.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "Position"))});

                XRTableCell xrTableCellDepartment = (XRTableCell)detail_report.FindControl("xrTableCellDepartment", true);
                xrTableCellDepartment.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "Department"))});
                    

                XRTableCell xrTableCellEffectiveDate = (XRTableCell)detail_report.FindControl("xrTableCellEffectiveDate", true);
                xrTableCellEffectiveDate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "EffectiveDate"))});

                XRTableCell xrTableCellExpiredDate = (XRTableCell)detail_report.FindControl("xrTableCellExpiredDate", true);
                xrTableCellExpiredDate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "ExpiredDate"))});

                //PageFooter --------------------------------------------------------------------------------------
                XRPageInfo xrPageFooterLeftText = (XRPageInfo)detail_report.FindControl("xrPageFooterLeftText", true);
                xrPageFooterLeftText.Text = DateTime.Now.ToString("dd-MM-yyyy");

                XRPageInfo xrPageFooterRightText = (XRPageInfo)detail_report.FindControl("xrPageFooterRightText", true);
                xrPageFooterRightText.Text = Eagle.Resource.LanguageResource.Page;
                //--------------------------------------------------------------------------------------------------

                detail_report.FillDataSource();               
                return detail_report;
            }

            [SessionExpiration]
            public ActionResult PopulateReportViewer_ContractDetail(int? LSCompanyID)
            {
                ContractDetailXtraReport tableDetailReport = CreateReport_ContractDetail(LSCompanyID);
                ViewData["ReportDetail"] = tableDetailReport;
                return PartialView("../Report/HR/Contract/_ContractDetailReportViewer");                
            }

            public ActionResult ExportData_ContractDetail(int? LSCompanyID)
            {
                return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(CreateReport_ContractDetail(LSCompanyID));
            }

            #endregion Contract REPORT ==================================================================
        
            #region Qualification - Culture Education REPORT ============================================
            private QualificationXtraReport CreateReport_Qualification(int? LSCompanyID)
            {
                QualificationXtraReport _report = new QualificationXtraReport();   
                DataSet DS = HRReportRespository.GetData_Qualification(LSCompanyID, LanguageId);
                int rowCount = DS.Tables[0].Rows.Count;
                _report.DataSource = DS;
                _report.DataSourceSchema = DS.GetXmlSchema();
                _report.DataMember = "HR_sprptQualification_GetList";

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
                xrLabelHeaderTitle.Text = Eagle.Resource.LanguageResource.EmployeeStatisticsByQualification;

                //Header ------------------------------------------------------------------------------------------------------------------------------------
                XRTableCell xrTableCellHeaderSeq = (XRTableCell)_report.FindControl("xrTableCellHeaderSeq", true);
                xrTableCellHeaderSeq.Text = Eagle.Resource.LanguageResource.Seq2;

                XRTableCell xrTableCellHeaderLSQualificationCode = (XRTableCell)_report.FindControl("xrTableCellHeaderLSQualificationCode", true);
                xrTableCellHeaderLSQualificationCode.Text = Eagle.Resource.LanguageResource.QualificationCode;
                
                XRTableCell xrTableCellHeaderQualificationName = (XRTableCell)_report.FindControl("xrTableCellHeaderQualificationName", true);
                xrTableCellHeaderQualificationName.Text = Eagle.Resource.LanguageResource.Qualification;

                XRTableCell xrTableCellHeaderQty = (XRTableCell)_report.FindControl("xrTableCellHeaderQty", true);
                xrTableCellHeaderQty.Text = Eagle.Resource.LanguageResource.TotalOfStaffs;

                XRTableCell xrTableCellHeaderPercentage = (XRTableCell)_report.FindControl("xrTableCellHeaderPercentage", true);
                xrTableCellHeaderPercentage.Text = Eagle.Resource.LanguageResource.Percentage;

                //Details ------------------------------------------------------------------------------------------------------------------------------- 
                XRTableCell xrTableCellSeq = (XRTableCell)_report.FindControl("xrTableCellSeq", true);
                xrTableCellSeq.DataBindings.Add("Text", DS, "Seq");

                XRTableCell xrTableCellLSQualificationCode = (XRTableCell)_report.FindControl("xrTableCellLSQualificationCode", true);
                xrTableCellLSQualificationCode.DataBindings.Add("Text", DS, "LSQualificationCode");

                XRTableCell xrTableCellQualificationName = (XRTableCell)_report.FindControl("xrTableCellQualificationName", true);
                xrTableCellQualificationName.DataBindings.Add("Text", DS, "QualificationName");

                XRTableCell xrTableCellQty = (XRTableCell)_report.FindControl("xrTableCellQty", true);
                xrTableCellQty.DataBindings.Add("Text", DS, "Qty");

                XRTableCell xrTableCellPercentage = (XRTableCell)_report.FindControl("xrTableCellPercentage", true);
                xrTableCellPercentage.DataBindings.Add("Text", DS, "Percentage", "{0} %");

                //PageFooter --------------------------------------------------------------------------------------
                XRPageInfo xrPageFooterLeftText = (XRPageInfo)_report.FindControl("xrPageFooterLeftText", true);
                xrPageFooterLeftText.Text = DateTime.Now.ToString("dd-MM-yyyy");

                XRPageInfo xrPageFooterRightText = (XRPageInfo)_report.FindControl("xrPageFooterRightText", true);
                xrPageFooterRightText.Text = Eagle.Resource.LanguageResource.Page;
                //--------------------------------------------------------------------------------------------------

                _report.FillDataSource();
                return _report;
            }

            public ActionResult PopulateReportViewer_Qualification(int? LSCompanyID)
            {
                QualificationXtraReport tableReport = CreateReport_Qualification(LSCompanyID);
                ViewData["Report"] = tableReport;
                return PartialView("../Report/HR/Qualification/_QualificationReportViewer");
            }

            public ActionResult ExportData_Qualification(int? LSCompanyID)
            {
                return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(CreateReport_Qualification(LSCompanyID));
            }
        
            //detail
            private QualificationDetailXtraReport CreateReport_QualificationDetail(int? LSCompanyID)
            {
                DataTable dtParent = HRReportRespository.GetListByQualification_Master(LSCompanyID, LanguageId);
                DataTable dtChild = HRReportRespository.GetListByQualification_Details(LSCompanyID, LanguageId);

                DataSet DS = DataSetHelper.CreateDataSetJoinedFrom2Tables("ParentChildrenDS", "LSQualificationID", dtParent, dtChild);
                QualificationDetailXtraReport detail_report = new QualificationDetailXtraReport();
                detail_report.DataSource = DS;
                detail_report.DataMember = DS.Tables[0].TableName;
                detail_report.DetailReport.DataSource = DS;
                detail_report.DetailReport.DataMember = String.Format("{0}.{1}", DS.Tables[0].TableName, DS.Relations[0].RelationName);


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

                XRTable xrTableCompany = (XRTable)detail_report.FindControl("xrTableCompany", true);
                XRTableCell xrTableCellLabelCompanyName = (XRTableCell)detail_report.FindControl("xrTableCellLabelCompanyName", true);
                XRTableCell xrTableCellCompanyName = (XRTableCell)detail_report.FindControl("xrTableCellCompanyName", true);
                xrTableCellLabelCompanyName.Text = Eagle.Resource.LanguageResource.Headquarter;
                xrTableCellCompanyName.Text = CompanyName;

                XRTableCell xrTableCellLabelAddress = (XRTableCell)detail_report.FindControl("xrTableCellLabelAddress", true);
                XRTableCell xrTableCellAddress = (XRTableCell)detail_report.FindControl("xrTableCellAddress", true);
                xrTableCellLabelAddress.Text = Eagle.Resource.LanguageResource.Address;
                xrTableCellAddress.Text = Address;

                XRTableCell xrTableCellLabelPhone = (XRTableCell)detail_report.FindControl("xrTableCellLabelPhone", true);
                XRTableCell xrTableCellPhone = (XRTableCell)detail_report.FindControl("xrTableCellPhone", true);
                xrTableCellLabelPhone.Text = Eagle.Resource.LanguageResource.Phone;
                xrTableCellPhone.Text = Tel;

                XRTableCell xrTableCellLabelFax = (XRTableCell)detail_report.FindControl("xrTableCellLabelFax", true);
                XRTableCell xrTableCellFax = (XRTableCell)detail_report.FindControl("xrTableCellFax", true);
                xrTableCellLabelFax.Text = Eagle.Resource.LanguageResource.Fax;
                xrTableCellFax.Text = Fax;

                XRTableCell xrTableCellLabelEmail = (XRTableCell)detail_report.FindControl("xrTableCellLabelEmail", true);
                XRTableCell xrTableCellEmail = (XRTableCell)detail_report.FindControl("xrTableCellEmail", true);
                xrTableCellLabelEmail.Text = Eagle.Resource.LanguageResource.Email;
                xrTableCellEmail.Text = Email;


                XRPictureBox xrPictureBoxLogo = (XRPictureBox)detail_report.FindControl("xrPictureBoxLogo", true);
                xrPictureBoxLogo.ImageUrl = LogoPath;

                XRLabel xrLabelHeaderTitle = (XRLabel)detail_report.FindControl("xrLabelHeaderTitle", true);
                xrLabelHeaderTitle.Text = Eagle.Resource.LanguageResource.EmployeeStatisticsByQualificationDetails;

                //Header ------------------------------------------------------------------------------------------------------------------------------------
                XRTableCell xrTableCellHeaderSeq = (XRTableCell)detail_report.FindControl("xrTableCellHeaderSeq", true);
                xrTableCellHeaderSeq.Text = Eagle.Resource.LanguageResource.Seq2;

                XRTableCell xrTableCellHeaderEmpCode = (XRTableCell)detail_report.FindControl("xrTableCellHeaderEmpCode", true);
                xrTableCellHeaderEmpCode.Text = Eagle.Resource.LanguageResource.EmpCode;

                XRTableCell xrTableCellHeaderFullName = (XRTableCell)detail_report.FindControl("xrTableCellHeaderFullName", true);
                xrTableCellHeaderFullName.Text = Eagle.Resource.LanguageResource.FullName;

                XRTableCell xrTableCellHeaderGender = (XRTableCell)detail_report.FindControl("xrTableCellHeaderGender", true);
                xrTableCellHeaderGender.Text = Eagle.Resource.LanguageResource.Gender;

                XRTableCell xrTableCellHeaderMarital = (XRTableCell)detail_report.FindControl("xrTableCellHeaderMarital", true);
                xrTableCellHeaderMarital.Text = Eagle.Resource.LanguageResource.Marital;

                XRTableCell xrTableCellHeaderDOB = (XRTableCell)detail_report.FindControl("xrTableCellHeaderDOB", true);
                xrTableCellHeaderDOB.Text = Eagle.Resource.LanguageResource.DOB;

                XRTableCell xrTableCellHeaderPosition = (XRTableCell)detail_report.FindControl("xrTableCellHeaderPosition", true);
                xrTableCellHeaderPosition.Text = Eagle.Resource.LanguageResource.Position;

                //Details ------------------------------------------------------------------------------------------------------------------------------- 
                XRTableCell xrTableCellMasterQualificationName = (XRTableCell)detail_report.FindControl("xrTableCellMasterQualificationName", true);
                xrTableCellMasterQualificationName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}", DS.Tables[0].TableName, "QualificationName"))});

                XRTableCell xrTableCellSeq = (XRTableCell)detail_report.FindControl("xrTableCellSeq", true);
                xrTableCellSeq.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "Seq"))});

                XRTableCell xrTableCellEmpCode = (XRTableCell)detail_report.FindControl("xrTableCellEmpCode", true);
                xrTableCellEmpCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "EmpCode"))});
                             
                XRTableCell xrTableCellFullName = (XRTableCell)detail_report.FindControl("xrTableCellFullName", true);
                xrTableCellFullName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "FullName"))});

                XRTableCell xrTableCellGender = (XRTableCell)detail_report.FindControl("xrTableCellGender", true);
                xrTableCellGender.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "Gender"))});
                
                XRTableCell xrTableCellMarital = (XRTableCell)detail_report.FindControl("xrTableCellMarital", true);
                xrTableCellMarital.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "Marital"))});

                XRTableCell xrTableCellPosition = (XRTableCell)detail_report.FindControl("xrTableCellPosition", true);
                xrTableCellPosition.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "Position"))});

                XRTableCell xrTableCellDOB = (XRTableCell)detail_report.FindControl("xrTableCellDOB", true);
                xrTableCellDOB.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "DOB"))});
                
                //PageFooter --------------------------------------------------------------------------------------
                XRPageInfo xrPageFooterLeftText = (XRPageInfo)detail_report.FindControl("xrPageFooterLeftText", true);
                xrPageFooterLeftText.Text = DateTime.Now.ToString("dd-MM-yyyy");

                XRPageInfo xrPageFooterRightText = (XRPageInfo)detail_report.FindControl("xrPageFooterRightText", true);
                xrPageFooterRightText.Text = Eagle.Resource.LanguageResource.Page;
                //--------------------------------------------------------------------------------------------------

                detail_report.FillDataSource();               
                return detail_report;
            }

            public ActionResult PopulateReportViewer_QualificationDetail(int? LSCompanyID)
            {
                QualificationDetailXtraReport tabledDetailReport = CreateReport_QualificationDetail(LSCompanyID);
                ViewData["ReportDetail"] = tabledDetailReport;
                return PartialView("../Report/HR/Qualification/_QualificationDetailReportViewer");
            }

            public ActionResult ExportData_QualificationDetail(int? LSCompanyID)
            {
                return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(CreateReport_QualificationDetail(LSCompanyID));
            }

            #endregion Qualification REPORT ==============================================================

            #region Position REPORT ======================================================================
            private PositionXtraReport CreateReport_Position(int? LSCompanyID)
            {
                DataTable DT = HRReportRespository.GetListByPosition_Master(LSCompanyID, LanguageId);
                PositionXtraReport _report = new PositionXtraReport();
               // int rowCount = DT.Rows.Count;
                _report.DataSource = DT;               
                _report.DataMember = "HR_sprptEmployee_GetListByPosition_Master";
                
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
                xrLabelHeaderTitle.Text = Eagle.Resource.LanguageResource.EmployeeStatisticsByPositions;

                //Header ------------------------------------------------------------------------------------------------------------------------------------
                XRTableCell xrTableCellHeaderSeq = (XRTableCell)_report.FindControl("xrTableCellHeaderSeq", true);
                xrTableCellHeaderSeq.Text = Eagle.Resource.LanguageResource.Seq2;

                XRTableCell xrTableCellHeaderPositionCode = (XRTableCell)_report.FindControl("xrTableCellHeaderPositionCode", true);
                xrTableCellHeaderPositionCode.Text = Eagle.Resource.LanguageResource.Code;

                XRTableCell xrTableCellHeaderPositionName = (XRTableCell)_report.FindControl("xrTableCellHeaderPositionName", true);
                xrTableCellHeaderPositionName.Text = Eagle.Resource.LanguageResource.Position;

                XRTableCell xrTableCellHeaderQty = (XRTableCell)_report.FindControl("xrTableCellHeaderQty", true);
                xrTableCellHeaderQty.Text = Eagle.Resource.LanguageResource.TotalOfStaffs;

                XRTableCell xrTableCellHeaderPercentage = (XRTableCell)_report.FindControl("xrTableCellHeaderPercentage", true);
                xrTableCellHeaderPercentage.Text = Eagle.Resource.LanguageResource.Percentage;

                //Details ------------------------------------------------------------------------------------------------------------------------------- 
                XRTableCell xrTableCellSeq = (XRTableCell)_report.FindControl("xrTableCellSeq", true);
                xrTableCellSeq.DataBindings.Add("Text", DT, "Seq");

                XRTableCell xrTableCellPositionCode = (XRTableCell)_report.FindControl("xrTableCellPositionCode", true);
                xrTableCellPositionCode.DataBindings.Add("Text", DT, "LSPositionCode");

                XRTableCell xrTableCellPositionName = (XRTableCell)_report.FindControl("xrTableCellPositionName", true);
                xrTableCellPositionName.DataBindings.Add("Text", DT, "PositionName");

                XRTableCell xrTableCellQty = (XRTableCell)_report.FindControl("xrTableCellQty", true);
                xrTableCellQty.DataBindings.Add("Text", DT, "Qty");

                XRTableCell xrTableCellPercentage = (XRTableCell)_report.FindControl("xrTableCellPercentage", true);
                xrTableCellPercentage.DataBindings.Add("Text", DT, "Percentage", "{0} %");

                //PageFooter --------------------------------------------------------------------------------------
                XRPageInfo xrPageFooterLeftText = (XRPageInfo)_report.FindControl("xrPageFooterLeftText", true);
                xrPageFooterLeftText.Text = DateTime.Now.ToString("dd-MM-yyyy");

                XRPageInfo xrPageFooterRightText = (XRPageInfo)_report.FindControl("xrPageFooterRightText", true);
                xrPageFooterRightText.Text = Eagle.Resource.LanguageResource.Page;
                //--------------------------------------------------------------------------------------------------

                _report.FillDataSource();
                return _report;
            }

            [SessionExpiration]
            public ActionResult PopulateReportViewer_Position(int? LSCompanyID)
            {
                //List<PositionReportModel> lst = HRReportRespository.GetListByPositionId(LanguageId);
                //return PartialView("../Report/HR/Position/_PositionReportViewer", lst);

                PositionXtraReport tableReport = CreateReport_Position(LSCompanyID);
                ViewData["Report"] = tableReport;
                return PartialView("../Report/HR/Position/_PositionReportViewer");
            }

            public ActionResult ExportData_Position(int? LSCompanyID)
            {
                return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(CreateReport_Position(LSCompanyID));
            }

            
            //detail --------------------------------------------------------------------------------------
            private PositionDetailXtraReport CreateReport_PositionDetail(int? LSCompanyID)
            {
                DataTable dtParent = HRReportRespository.GetListByPosition_Master(LSCompanyID, LanguageId);
                DataTable dtChild = HRReportRespository.GetListByPosition_Detail(LSCompanyID, LanguageId);

                DataSet DS = DataSetHelper.CreateDataSetJoinedFrom2Tables("ParentChildrenDS", "LSPositionID", dtParent, dtChild);
                PositionDetailXtraReport detail_report = new PositionDetailXtraReport();
                detail_report.DataSource = DS;
                detail_report.DataMember = DS.Tables[0].TableName;
                detail_report.DetailReport.DataSource = DS;
                detail_report.DetailReport.DataMember = String.Format("{0}.{1}", DS.Tables[0].TableName, DS.Relations[0].RelationName);

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

                XRTable xrTableCompany = (XRTable)detail_report.FindControl("xrTableCompany", true);
                XRTableCell xrTableCellLabelCompanyName = (XRTableCell)detail_report.FindControl("xrTableCellLabelCompanyName", true);
                XRTableCell xrTableCellCompanyName = (XRTableCell)detail_report.FindControl("xrTableCellCompanyName", true);
                xrTableCellLabelCompanyName.Text = Eagle.Resource.LanguageResource.Headquarter;
                xrTableCellCompanyName.Text = CompanyName;

                XRTableCell xrTableCellLabelAddress = (XRTableCell)detail_report.FindControl("xrTableCellLabelAddress", true);
                XRTableCell xrTableCellAddress = (XRTableCell)detail_report.FindControl("xrTableCellAddress", true);
                xrTableCellLabelAddress.Text = Eagle.Resource.LanguageResource.Address;
                xrTableCellAddress.Text = Address;

                XRTableCell xrTableCellLabelPhone = (XRTableCell)detail_report.FindControl("xrTableCellLabelPhone", true);
                XRTableCell xrTableCellPhone = (XRTableCell)detail_report.FindControl("xrTableCellPhone", true);
                xrTableCellLabelPhone.Text = Eagle.Resource.LanguageResource.Phone;
                xrTableCellPhone.Text = Tel;

                XRTableCell xrTableCellLabelFax = (XRTableCell)detail_report.FindControl("xrTableCellLabelFax", true);
                XRTableCell xrTableCellFax = (XRTableCell)detail_report.FindControl("xrTableCellFax", true);
                xrTableCellLabelFax.Text = Eagle.Resource.LanguageResource.Fax;
                xrTableCellFax.Text = Fax;

                XRTableCell xrTableCellLabelEmail = (XRTableCell)detail_report.FindControl("xrTableCellLabelEmail", true);
                XRTableCell xrTableCellEmail = (XRTableCell)detail_report.FindControl("xrTableCellEmail", true);
                xrTableCellLabelEmail.Text = Eagle.Resource.LanguageResource.Email;
                xrTableCellEmail.Text = Email;


                XRPictureBox xrPictureBoxLogo = (XRPictureBox)detail_report.FindControl("xrPictureBoxLogo", true);
                xrPictureBoxLogo.ImageUrl = LogoPath;
                
                XRLabel xrLabelHeaderTitle = (XRLabel)detail_report.FindControl("xrLabelHeaderTitle", true);
                xrLabelHeaderTitle.Text = Eagle.Resource.LanguageResource.EmployeeStatisticsByPositiondDetails;
              
                //Header ------------------------------------------------------------------------------------------------------------------------------------
                XRTableCell xrTableCellHeaderSeq = (XRTableCell)detail_report.FindControl("xrTableCellHeaderSeq", true);
                xrTableCellHeaderSeq.Text = Eagle.Resource.LanguageResource.Seq2;

                XRTableCell xrTableCellHeaderEmpCode = (XRTableCell)detail_report.FindControl("xrTableCellHeaderEmpCode", true);
                xrTableCellHeaderEmpCode.Text = Eagle.Resource.LanguageResource.EmpCode;

                XRTableCell xrTableCellHeaderFullName = (XRTableCell)detail_report.FindControl("xrTableCellHeaderFullName", true);
                xrTableCellHeaderFullName.Text = Eagle.Resource.LanguageResource.FullName;

                XRTableCell xrTableCellHeaderGender = (XRTableCell)detail_report.FindControl("xrTableCellHeaderGender", true);
                xrTableCellHeaderGender.Text = Eagle.Resource.LanguageResource.Gender;

                XRTableCell xrTableCellHeaderMarital = (XRTableCell)detail_report.FindControl("xrTableCellHeaderMarital", true);
                xrTableCellHeaderMarital.Text = Eagle.Resource.LanguageResource.Marital;

                XRTableCell xrTableCellHeaderDOB = (XRTableCell)detail_report.FindControl("xrTableCellHeaderDOB", true);
                xrTableCellHeaderDOB.Text = Eagle.Resource.LanguageResource.DOB;

                XRTableCell xrTableCellHeaderJoinDate = (XRTableCell)detail_report.FindControl("xrTableCellHeaderJoinDate", true);
                xrTableCellHeaderJoinDate.Text = Eagle.Resource.LanguageResource.JoinDate;

                XRTableCell xrTableCellHeaderQualification = (XRTableCell)detail_report.FindControl("xrTableCellHeaderQualification", true);
                xrTableCellHeaderQualification.Text = Eagle.Resource.LanguageResource.Qualification;

                //Details ------------------------------------------------------------------------------------------------------------------------------- 
                XRTableCell xrTableCellMasterPositionName = (XRTableCell)detail_report.FindControl("xrTableCellMasterPositionName", true);
                xrTableCellMasterPositionName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}", DS.Tables[0].TableName, "PositionName"))});

                XRTableCell xrTableCellSeq = (XRTableCell)detail_report.FindControl("xrTableCellSeq", true);
                xrTableCellSeq.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "Seq"))});

                XRTableCell xrTableCellEmpCode = (XRTableCell)detail_report.FindControl("xrTableCellEmpCode", true);
                xrTableCellEmpCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "EmpCode"))});

                XRTableCell xrTableCellFullName = (XRTableCell)detail_report.FindControl("xrTableCellFullName", true);
                xrTableCellFullName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "FullName"))});

                XRTableCell xrTableCellGender = (XRTableCell)detail_report.FindControl("xrTableCellGender", true);
                xrTableCellGender.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "Gender"))});

                XRTableCell xrTableCellMarital = (XRTableCell)detail_report.FindControl("xrTableCellMarital", true);
                xrTableCellMarital.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "Marital"))});

                XRTableCell xrTableCellQualification = (XRTableCell)detail_report.FindControl("xrTableCellQualification", true);
                xrTableCellQualification.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "Qualification"))});

                XRTableCell xrTableCellDOB = (XRTableCell)detail_report.FindControl("xrTableCellDOB", true);
                xrTableCellDOB.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "DOB"))});

                XRTableCell xrTableCellJoinDate = (XRTableCell)detail_report.FindControl("xrTableCellJoinDate", true);
                xrTableCellJoinDate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, string.Format("{0}.{1}.{2}", DS.Tables[0].TableName, DS.Relations[0].RelationName, "JoinDate"))});
                
                //PageFooter --------------------------------------------------------------------------------------
                XRPageInfo xrPageFooterLeftText = (XRPageInfo)detail_report.FindControl("xrPageFooterLeftText", true);
                xrPageFooterLeftText.Text = DateTime.Now.ToString("dd-MM-yyyy");

                XRPageInfo xrPageFooterRightText = (XRPageInfo)detail_report.FindControl("xrPageFooterRightText", true);
                xrPageFooterRightText.Text = Eagle.Resource.LanguageResource.Page;
                //--------------------------------------------------------------------------------------------------

                detail_report.FillDataSource();
                return detail_report;
            }

            [SessionExpiration]
            public ActionResult PopulateReportViewer_PositionDetail(int? LSCompanyID)
            {
                PositionDetailXtraReport tabledDetailReport = CreateReport_PositionDetail(LSCompanyID);
                ViewData["ReportDetail"] = tabledDetailReport;
                return PartialView("../Report/HR/Position/_PositionDetailReportViewer");
            }

            public ActionResult ExportData_PositionDetail(int? LSCompanyID)
            {
                return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(CreateReport_PositionDetail(LSCompanyID));
            }

            #endregion Position REPORT ===================================================================


         
        #endregion MAIN REPORT - DETAIL REPORT VIEWER ====================     


        #region REPORT DETAILS =====================================================================
        //public ActionResult LoadPivotGrid()
        //{
        //    List<ContractDetailReportModel> lst = GetContractReports();
        //    return PartialView("../Report/HR/Contract/_ContractDetailPivotGrid", lst);
        //}

        //public class Contract_PivotGridHelper
        //{
        //    static HttpSessionState Session
        //    {
        //        get { return System.Web.HttpContext.Current.Session; }
        //    }
        //    public static PivotGridSettings CreateGeneralSettings()
        //    {
        //        var settings = new PivotGridSettings();
        //        settings.Name = "ContractReport";
        //        // settings.CallbackRouteValues = new { Controller = "ContractReport", Action = "LoadPivotGrid" };
        //        //settings.OptionsView.ShowFilterHeaders = false;
        //        //settings.CustomizationFieldsLeft = 600;
        //        //settings.CustomizationFieldsTop = 400;
        //        //settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);


        //        settings.OptionsView.GroupFieldsInCustomizationWindow = false;
        //        settings.OptionsView.ShowHorizontalScrollBar = true;
        //        settings.OptionsPager.Visible = false;
        //        settings.OptionsView.DataHeadersDisplayMode = DevExpress.Web.ASPxPivotGrid.PivotDataHeadersDisplayMode.Popup;
        //        settings.OptionsView.DataHeadersPopupMinCount = 4;
        //        settings.Width = new System.Web.UI.WebControls.Unit(100, System.Web.UI.WebControls.UnitType.Percentage);
        //        settings.Height = new System.Web.UI.WebControls.Unit(100, System.Web.UI.WebControls.UnitType.Percentage);
        //        settings.OptionsView.HideAllTotals();
        //        settings.OptionsView.ShowColumnGrandTotalHeader = true;
        //        settings.OptionsView.ShowColumnGrandTotals = true;

        //        settings.Groups.Add("Contract Types - Contracts");
        //        //settings.Groups.Add("Year - Quarter");
        //        //settings.Groups.Add("Sum, Min, Average");

        //        settings.Fields.Add(field =>
        //        {
        //            field.Area = PivotArea.RowArea;
        //            field.AreaIndex = 0;
        //            field.Caption = "Contract Type";
        //            field.FieldName = "ContractTypeName";
        //            field.GroupIndex = 0;
        //            field.InnerGroupIndex = 0;
        //            //field.CustomTotals.Add(PivotSummaryType.Average);
        //            //field.CustomTotals.Add(PivotSummaryType.Min);
        //            //field.CustomTotals.Add(PivotSummaryType.Max);
        //        });

        //        settings.Fields.Add(field =>
        //        {
        //            field.Area = PivotArea.RowArea;
        //            field.FieldName = "FullName";
        //            field.Caption = "FullName";
        //        });

        //        //settings.Fields.Add(field =>
        //        //{
        //        //    field.Area = PivotArea.FilterArea;
        //        //    field.FieldName = "ContractTypeName";
        //        //    field.Caption = "Contract Type"";
        //        //});

        //        //settings.Fields.Add(field =>
        //        //{
        //        //    field.Area = PivotArea.ColumnArea;
        //        //    field.AreaIndex = 0;
        //        //    field.Caption = "Year";
        //        //    field.FieldName = "ShippedDate";
        //        //    field.GroupInterval = PivotGroupInterval.DateYear;
        //        //    field.GroupIndex = 1;
        //        //    field.InnerGroupIndex = 0;
        //        //    field.UnboundFieldName = "Year";
        //        //});
        //        //settings.Fields.Add(field =>
        //        //{
        //        //    field.Area = PivotArea.ColumnArea;
        //        //    field.AreaIndex = 1;
        //        //    field.Caption = "Quarter";
        //        //    field.FieldName = "ShippedDate";
        //        //    field.GroupInterval = PivotGroupInterval.DateQuarter;
        //        //    field.ExpandedInFieldsGroup = false;
        //        //    field.GroupIndex = 1;
        //        //    field.InnerGroupIndex = 1;
        //        //    field.UnboundFieldName = "Quarter";
        //        //    field.ValueFormat.FormatString = "Qtr {0}";
        //        //    field.ValueFormat.FormatType = FormatType.Numeric;
        //        //});

        //        //settings.Fields.Add(field =>
        //        //{
        //        //    field.Area = PivotArea.RowArea;
        //        //    field.AreaIndex = 1;
        //        //    field.Caption = "LSContractTypeID";
        //        //    field.FieldName = "LSContractTypeID";
        //        //    field.GroupIndex = 0;
        //        //    field.InnerGroupIndex = 1;
        //        //});




        //        //settings.Fields.Add(field =>
        //        //{
        //        //    field.AreaIndex = 0;
        //        //    field.Area = PivotArea.DataArea;
        //        //    field.FieldName = "Qty";
        //        //    field.Caption = "Số lượng";
        //        //    field.CellFormat.FormatType = FormatType.Numeric;
        //        //    field.CellFormat.FormatString = "{0:n0}";
        //        //});


        //        //settings.Fields.Add(field =>
        //        //{
        //        //    field.AreaIndex = 2;
        //        //    field.Area = PivotArea.DataArea;
        //        //    field.FieldName = "Amount";
        //        //    field.Caption = "Thành tiền";
        //        //    field.CellFormat.FormatType = FormatType.Numeric;
        //        //    field.CellFormat.FormatString = "{0:n0}";
        //        //});

        //        // Saves layout after change
        //        settings.GridLayout = (sender, e) =>
        //        {
        //            MVCxPivotGrid PivotGrid = sender as MVCxPivotGrid;
        //            Session["Layout"] = PivotGrid.SaveLayoutToString(PivotGridWebOptionsLayout.DefaultLayout);
        //        };

        //        // Loads layout after change
        //        settings.PreRender = (sender, e) =>
        //        {
        //            MVCxPivotGrid PivotGrid = sender as MVCxPivotGrid;
        //            string layout = Session["Layout"] as string;
        //            if (!string.IsNullOrEmpty(layout))
        //            {
        //                PivotGrid.LoadLayoutFromString(layout, DevExpress.Web.ASPxPivotGrid.PivotGridWebOptionsLayout.DefaultLayout);
        //            }
        //        };

        //        return settings;
        //    }
        //}
        #endregion REPORT DETAILS ==================================================================


        #region REPORT PARAMETERS =================================================================================
        //
        //Career Status ================================================================================
        public void PopulateCareerStatusToDropDownList()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            dict.Add(1, Eagle.Resource.LanguageResource.FullTime);
            dict.Add(0, Eagle.Resource.LanguageResource.PartTime);

            ViewBag.CareerStatus = new SelectList(dict, "Key", "Value", null);
        }

        //
        //Statistic Type =================================================================================
        public void PopulateStatisticTypeToDropDownList()
        {
            //Hashtable hashtable = new Hashtable();
            //hashtable[1] = Eagle.Resource.LanguageResource.OrganizationalStructureTypes;
            //hashtable[2] = Eagle.Resource.LanguageResource.AgePeriod;
            //hashtable[3] = Eagle.Resource.LanguageResource.SalaryLevelRatio;

            //Dictionary<int, string> dict = Eagle.Common.Extensions.EnumExtension.BindEnum(typeof(StatisticTypeEntity));          
            //ViewBag.StatisticType = new SelectList(dict, "Key", "Value", null);

            ViewBag.StatisticType = typeof(StatisticTypeEntity);
        }

        //
        //Statistic Focus =================================================================================
        public void PopulateStatisticFocusToDropDownList()
        {
            List<SelectListItem> lst = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = Eagle.Resource.LanguageResource.DepartmentSection, Value= "1", Selected=true },
                    new SelectListItem() { Text = Eagle.Resource.LanguageResource.CulturalEducation, Value= "2" },
                    new SelectListItem() { Text = Eagle.Resource.LanguageResource.Position, Value= "3" },
                    new SelectListItem() { Text = Eagle.Resource.LanguageResource.ContractType, Value= "4" }
                };

            SelectList selectList = new SelectList(lst, "Value", "Text");
            //ViewData["lstBoxAssigned"] = selectList;
            ViewBag.StatisticFocus = selectList;
        }
        //public void IncludeJsRuntime(string jsFileName)
        //{
        //    var bundle = new Bundle("~/bundles/scripts/chart");
        //    BundleCollection bundles = new BundleCollection();
        //    bundles.Remove(bundle);
        //    bundles.Add(new ScriptBundle("~/bundles/scripts/chart").Include(
        //              "~/Scripts/app/chart/" + jsFileName
        //              ));
        //}
        //
        #endregion =================================================================================
    }
}
