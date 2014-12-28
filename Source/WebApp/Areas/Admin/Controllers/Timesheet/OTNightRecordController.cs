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
    public class OTNightRecordController : BaseController
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
                return this.PartialView("../Timesheet/OTNightRecord/Reset");
            }
            else
            {
                return this.View("../Timesheet/OTNightRecord/Index");
            }
          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var model = new Timesheet_sprptOTRecord_Detail_Result();
            model.Month = DateTime.Now.Month;
            model.Year = DateTime.Now.Year;
            this.CreateViewBag(model);
            return this.PartialView("../Timesheet/OTNightRecord/Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Report(Timesheet_sprptOTRecord_Detail_Result model, int? Month, int? Year)
        {
            if (!Month.HasValue)
            {
                model.Month = DateTime.Now.Month;
            }
            if (!Year.HasValue)
            {
                model.Year = DateTime.Now.Year;
            }            
            this.TempData["OTNightRecord"] = model;
            this.ViewData["Report"] = new OTNightRecordXtraReport();

            return this.PartialView("../Timesheet/OTNightRecord/OTNightRecordReportViewer");
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateViewBag(Timesheet_sprptOTRecord_Detail_Result model)
        {
            List<int> months = new List<int>();
            for (int i = 1; i <= 12; i++)
            {
                months.Add(i);
            }
            ViewBag.Month = new SelectList(months, model.Month);
            var years = new List<int>();
            for (int i = DateTime.Now.Year - 10; i <= DateTime.Now.Year + 10; i++)
            {
                years.Add(i);
            }
            this.ViewBag.Year = new SelectList(years, model.Year);
        }

        #region CreateOTNightRecordReport

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OTNightRecordXtraReport CreateOTNightRecordReport(Timesheet_sprptOTRecord_Detail_Result model)
        {
            var report = new OTNightRecordXtraReport();
            /*
            if (!model.LSCompanyID.HasValue)
            {
                model.LSCompanyID = 0;
            }
            */
            // Setting header             
            report.Bands["ReportHeader"].Controls["xrLblMonth"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.Month, model.Month);
            report.Bands["ReportHeader"].Controls["xrLblYear"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.Year, model.Year);
            var CompanyName = String.Empty;
            //if (model.LSCompanyID.HasValue)
            //{
                var com = this.db.LS_tblCompany.Where(p => p.LSCompanyID == model.LSCompanyID).FirstOrDefault();
                if (com != null)
                {
                    CompanyName = com.Name;
                }
            //}
            report.Bands["ReportHeader"].Controls["xrLblCompany"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.LastCompanyDefine, CompanyName);
            report.Bands["ReportHeader"].Controls["xrLblOTNightRecordReport"].Text = Eagle.Resource.LanguageResource.OTNightRecordReport;

            var objInfo = this.db.SYS_tblParameter.Where(p => p.ParameterID == 2).FirstOrDefault();
            if (objInfo != null)
            {
                var OTNightFrom = objInfo.NightOTFrom.HasValue ? objInfo.NightOTFrom.Value.ToString("HH:mm") : String.Empty;
                var OTNightTo = objInfo.NightOTTo.HasValue ? objInfo.NightOTTo.Value.ToString("HH:mm") : String.Empty;
                report.Bands["ReportHeader"].Controls["xrLblOTNight"].Text = String.Format("{0} : {1}PM - {2}AM", Eagle.Resource.LanguageResource.OTNightDate, OTNightFrom, OTNightTo);                
            }
            
            // Setting Header of List
            report.Bands["ReportHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellSeq"].Text = Eagle.Resource.LanguageResource.SEQ;                        
            report.Bands["ReportHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellFullName"].Text = Eagle.Resource.LanguageResource.OTNinghtFullName;
            report.Bands["ReportHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellOTDate"].Text = Eagle.Resource.LanguageResource.OTNightDate;
            report.Bands["ReportHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellTimeInAM"].Text = Eagle.Resource.LanguageResource.OTNightTimeInAM;
            report.Bands["ReportHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellTimeOutAM"].Text = Eagle.Resource.LanguageResource.OTNightTimeOutAM;
            report.Bands["ReportHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellTimeInPM"].Text = Eagle.Resource.LanguageResource.OTNightInPM;
            report.Bands["ReportHeader"].Controls["xrTable2"].Controls["xrTableRow2"].Controls["xrTableCellNightOT"].Text = Eagle.Resource.LanguageResource.OTNight;

            // Setting detail of data
            if (model.FullName == null)
            {
                model.FullName = String.Empty;
            }
            if (!model.LSPositionID.HasValue)
            {
                model.LSPositionID = 0;
            }
            var errorMessage = String.Empty;
                
            var repository = new OTNightRecordReportRepository(this.db);
            var listOfDetail = repository.GetListOfOTDetailRecord(model, out errorMessage);
            var listOfMaster = new List<Timesheet_sprptOTRecord_Master_Result>();
            if (listOfDetail != null)
            {
                if (listOfDetail.Count == 0)
                {
                    listOfMaster.Add(new Timesheet_sprptOTRecord_Master_Result { LSCompanyID = 0 });
                    listOfDetail.Add(new Timesheet_sprptOTRecord_Detail_Result { LSCompanyID = 0 });
                }
                else
                {
                    listOfMaster = (from detail in listOfDetail
                                    group detail by new { detail.LSCompanyID } into Master
                                    select new Timesheet_sprptOTRecord_Master_Result
                                    {
                                        LSCompanyID = Master.Key.LSCompanyID
                                    }).ToList();
                    foreach (var master in listOfMaster)
                    {
                        var company = this.db.LS_tblCompany.Where(p => p.LSCompanyID == master.LSCompanyID).FirstOrDefault();
                        if (company != null)
                        {
                            master.CompanyName = company.Name;
                        }
                    }
                }
                var dsMasterDetail = GenericsToDataSet.ConvertGenericList("OTNightRecordDS", listOfMaster, listOfDetail, "Timesheet_sprptOTRecord_Master", "Timesheet_sprptOTRecord_Detail");
                report.DataSource = dsMasterDetail;
                report.DataMember = dsMasterDetail.Tables[0].TableName;
                report.DetailReport.DataSource = dsMasterDetail;
                report.DetailReport.DataMember = String.Format("{0}.{1}", dsMasterDetail.Tables[0].TableName, dsMasterDetail.Relations[0].RelationName);                                       
            }
                                   
            return report;            
        }            

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadOTNightRecordReportViewer()
        {
            var model = this.TempData["OTNightRecord"] as Timesheet_sprptOTRecord_Detail_Result;
            if (model == null)
            {
                model = new Timesheet_sprptOTRecord_Detail_Result();
            }            
            
            this.ViewData["Report"] = this.CreateOTNightRecordReport(model);
            return this.PartialView("../Timesheet/OTNightRecord/OTNightRecordReportViewer");
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult ExportOTNightRecordData()
        {
            var model = this.TempData["OTNightRecord"] as Timesheet_sprptOTRecord_Detail_Result;
            if (model == null)
            {
                model = new Timesheet_sprptOTRecord_Detail_Result();
            }
            this.TempData["OTNightRecord"] = model;
            return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(this.CreateOTNightRecordReport(model));
        }
         
    }


}
