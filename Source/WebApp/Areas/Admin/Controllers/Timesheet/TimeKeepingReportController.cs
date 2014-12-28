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
using Eagle.Common.Utilities;
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class TimeKeepingReportController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        public const int TIMESHEET_MODULE_ID = 9;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            this.TempData.Clear();
            if (this.Request.IsAjaxRequest())
            {
                return this.PartialView("../Timesheet/TimeKeepingReport/Reset");
            }
            else
            {
                return this.View("../Timesheet/TimeKeepingReport/Index");
            }
          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var model = new TimeKeepingReportViewModel();
            model.FromDateInfo = String.Empty;
            model.ToDateInfo = String.Empty;
            this.CreateViewBag(model);
            return this.PartialView("../Timesheet/TimeKeepingReport/Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Report(TimeKeepingReportViewModel model)
        {
            if (DateTimeUtils.IsDateTime(model.FromDateInfo, "dd/MM/yyyy"))
            {
                model.FromDate = DateTime.ParseExact(model.FromDateInfo, "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());
            }
            if (DateTimeUtils.IsDateTime(model.ToDateInfo, "dd/MM/yyyy"))
            {
                model.ToDate = DateTime.ParseExact(model.ToDateInfo, "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());
            }
            this.TempData["TimeKeeping"] = model;
            this.ViewData["Report"] = new TimeKeepingXtraReport();

            return this.PartialView("../Timesheet/TimeKeepingReport/TimeKeepingReportViewer");
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateViewBag(TimeKeepingReportViewModel model)
        {
            return;
        }

        #region CreateTimeKeepingReport

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private TimeKeepingXtraReport CreateTimeKeepingReport(TimeKeepingReportViewModel model, string userName, int ModuleID)
        {
            if (DateTimeUtils.IsDateTime(model.FromDateInfo, "dd/MM/yyyy"))
            {
                model.FromDate = DateTime.ParseExact(model.FromDateInfo, "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());
            }
            if (DateTimeUtils.IsDateTime(model.ToDateInfo, "dd/MM/yyyy"))
            {
                model.ToDate = DateTime.ParseExact(model.ToDateInfo, "dd/MM/yyyy", new System.Globalization.DateTimeFormatInfo());
            }
            // Creating report
            var report = new TimeKeepingXtraReport();            
            
            // Setting header             
            report.Bands["ReportHeader"].Controls["xrLblTimeKeepingReport"].Text = String.Format("{0}", Eagle.Resource.LanguageResource.StatisticOfLateEarlyEmployeeReport);
            report.Bands["ReportHeader"].Controls["xrLblFromDate"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.FromDate, model.FromDateInfo);
            report.Bands["ReportHeader"].Controls["xrLblToDate"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.ToDate, model.ToDateInfo);

            // Setting Header of List
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellSEQ"].Text = Eagle.Resource.LanguageResource.SEQ;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellEmployeeName"].Text = Eagle.Resource.LanguageResource.Employee;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellPositionName"].Text = Eagle.Resource.LanguageResource.Position;            
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellDate"].Text = Eagle.Resource.LanguageResource.Date;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellShiftName"].Text = Eagle.Resource.LanguageResource.ShiftName;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellTimeIn"].Text = Eagle.Resource.LanguageResource.WorkTimeBegin;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellTimeOut"].Text = Eagle.Resource.LanguageResource.WorkTimeEnd;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellRealTimeIn"].Text = Eagle.Resource.LanguageResource.TimeLate;
            report.Bands["PageHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellRealTimeOut"].Text = Eagle.Resource.LanguageResource.TimeEarly;
            
            // Setting data           
            if (!model.LSPositionID.HasValue)
            {
                model.LSPositionID = 0;
            }
            var errorMessage = String.Empty;
            var repository = new TimeKeepingReportRepository(this.db);
            var listOfDetail = repository.GetDetailOfTimeKeepingReport(userName, ModuleID, model.FromDate, model.ToDate, model.LSCompanyID, model.LSPositionID, model.EmployeeName, model.EmployeeCode, out errorMessage);
            var listOfMaster = new List<timesheet_sprptTimeKeepingReport_master_Result>();
            if (listOfDetail != null)
            {
                if (listOfDetail.Count == 0)
                {
                    listOfMaster.Add(new timesheet_sprptTimeKeepingReport_master_Result { LSCompanyID = 0 });
                    listOfDetail.Add(new timesheet_sprptTimeKeepingReport_detail_Result { LSCompanyID = 0 });
                }
                else
                {
                    listOfMaster = (from detail in listOfDetail
                                    group detail by new { detail.LSCompanyID } into Master
                                    select new timesheet_sprptTimeKeepingReport_master_Result
                                    {
                                        LSCompanyID = Master.Key.LSCompanyID
                                    }).ToList();
                    foreach (var master in listOfMaster)
                    {
                        var company = this.db.LS_tblCompany.Where(p => p.LSCompanyID == master.LSCompanyID).FirstOrDefault();
                        if (company != null)
                        {
                            master.CompanyName = company.VNName;
                        }
                    }
                }
                var dsMasterDetail = GenericsToDataSet.ConvertGenericList("TimeKeepingDS", listOfMaster, listOfDetail, "timesheet_sprptTimeKeepingReport_master", "timesheet_sprptTimeKeepingReport_detail");
                report.DataSource = dsMasterDetail;
                report.DataMember = dsMasterDetail.Tables[0].TableName;
                report.DetailReport.DataSource = dsMasterDetail;
                report.DetailReport.DataMember = String.Format("{0}.{1}", dsMasterDetail.Tables[0].TableName, dsMasterDetail.Relations[0].RelationName);
                return report;
            }
                   
            return report;               
        }            

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadTimeKeepingReportViewer()
        {
            var account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (account == null)
            {
                return this.Content("Timeout");
            }
            var model = this.TempData["TimeKeeping"] as TimeKeepingReportViewModel;
            if (model == null)
            {
                model = new TimeKeepingReportViewModel();
            }
            this.TempData["TimeKeeping"] = model;
            this.ViewData["Report"] = this.CreateTimeKeepingReport(model, account.UserName, TIMESHEET_MODULE_ID);

            return this.PartialView("../Timesheet/TimeKeepingReport/TimeKeepingReportViewer");
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult ExportTimeKeepingData()
        {
            var account = this.Session[SettingKeys.AccountInfo] as AccountViewModel;
            if (account == null)
            {
                return this.Content("Timeout");
            }
            var model = this.TempData["TimeKeeping"] as TimeKeepingReportViewModel;
            if (model == null)
            {
                model = new TimeKeepingReportViewModel();
            }
            this.TempData["TimeKeeping"] = model;

            return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(this.CreateTimeKeepingReport(model, account.UserName, TIMESHEET_MODULE_ID));
        }         
    }
}
