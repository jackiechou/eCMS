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
using Eagle.WebApp.Areas.Admin.Reports.TS.OTRecord;


namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class OTRecordController : BaseController
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
                return this.PartialView("../Timesheet/OTRecord/Reset");
            }
            else
            {
                return this.View("../Timesheet/OTRecord/Index");
            }
          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var model = new Timesheet_sprptOTRecord_Result();
            model.Month = DateTime.Now.Month;
            model.Year = DateTime.Now.Year;
            this.CreateViewBag(model);
            return this.PartialView("../Timesheet/OTRecord/Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Report(Timesheet_sprptOTRecord_Result model, int? Month, int? Year)
        {
            if (!Month.HasValue)
            {
                model.Month = DateTime.Now.Month;
            }
            if (!Year.HasValue)
            {
                model.Year = DateTime.Now.Year;
            }            
            this.TempData["OTRecord"] = model;
            this.ViewData["Report"] = new OTRecord31XtraReport();

            return this.PartialView("../Timesheet/OTRecord/OTRecord31ReportViewer");
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateViewBag(Timesheet_sprptOTRecord_Result model)
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

        #region CreateOTRecordReport

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OTRecord31XtraReport CreateOTRecord31Report(Timesheet_sprptOTRecord_Result model)
        {            
            var report = new OTRecord31XtraReport();
            // Setting header             
            report.Bands["ReportHeader"].Controls["xrLblMonth"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.Month, model.Month);
            report.Bands["ReportHeader"].Controls["xrLblYear"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.Year, model.Year);
            var CompanyName = String.Empty;
            if (model.LSCompanyID.HasValue)
            {
                var company = this.db.LS_tblCompany.Where(p => p.LSCompanyID == model.LSCompanyID.Value).FirstOrDefault();
                if (company != null)
                {
                    CompanyName = company.Name;
                }
            }
            report.Bands["ReportHeader"].Controls["xrLblCompany"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.LastCompanyDefine, CompanyName);
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
            var repository = new OTRecordReportRepository(this.db);
            var listOfDetail = repository.GetListOfOTRecord(model, out errorMessage);
            var listOfMaster = new List<Timesheet_sprptOTRecord_Master_Result>();

            if (listOfDetail != null)
            {
                // Get List of Master from Detail                
                if (listOfDetail.Count == 0)
                {                    
                    listOfMaster.Add(new Timesheet_sprptOTRecord_Master_Result { LSCompanyID = 0 });
                    listOfDetail.Add(new Timesheet_sprptOTRecord_Result { LSCompanyID = 0 });                    
                }
                else
                {
                    listOfMaster = (from detail in listOfDetail
                                    group detail by new { detail.LSCompanyID } into Master
                                    select new Timesheet_sprptOTRecord_Master_Result
                                    {
                                        LSCompanyID = Master.Key.LSCompanyID.HasValue ? Master.Key.LSCompanyID.Value : 0
                                    }).ToList();
                    if (listOfMaster != null)
                    {
                        foreach (var master in listOfMaster)
                        {
                            var company = this.db.LS_tblCompany.Where(p => p.LSCompanyID == master.LSCompanyID).FirstOrDefault();
                            if (company != null)
                            {
                                master.CompanyName = company.VNName;
                            }
                        }
                    }
                }                
                // Setting data source of report                
                var dsMasterDetail = GenericsToDataSet.ConvertGenericList("OTRecordDS", listOfMaster, listOfDetail, "Timesheet_sprptOTRecord_Master", "Timesheet_sprptOTRecord");
                report.DataSource = dsMasterDetail;
                report.DataMember = dsMasterDetail.Tables[0].TableName;
                report.DetailReport.DataSource = dsMasterDetail;
                report.DetailReport.DataMember = String.Format("{0}.{1}", dsMasterDetail.Tables[0].TableName, dsMasterDetail.Relations[0].RelationName);                                       
            }                                   
            return report;            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OTRecord30XtraReport CreateOTRecord30Report(Timesheet_sprptOTRecord_Result model)
        {
            var report = new OTRecord30XtraReport();
            // Setting header             
            report.Bands["ReportHeader"].Controls["xrLblMonth"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.Month, model.Month);
            report.Bands["ReportHeader"].Controls["xrLblYear"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.Year, model.Year);
            var CompanyName = String.Empty;
            if (model.LSCompanyID.HasValue)
            {
                var company = this.db.LS_tblCompany.Where(p => p.LSCompanyID == model.LSCompanyID.Value).FirstOrDefault();
                if (company != null)
                {
                    CompanyName = company.Name;
                }
            }
            report.Bands["ReportHeader"].Controls["xrLblCompany"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.LastCompanyDefine, CompanyName);
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
            var repository = new OTRecordReportRepository(this.db);
            var listOfDetail = repository.GetListOfOTRecord(model, out errorMessage);
            var listOfMaster = new List<Timesheet_sprptOTRecord_Master_Result>();

            if (listOfDetail != null)
            {
                // Get List of Master from Detail                
                if (listOfDetail.Count == 0)
                {
                    listOfMaster.Add(new Timesheet_sprptOTRecord_Master_Result { LSCompanyID = 0 });
                    listOfDetail.Add(new Timesheet_sprptOTRecord_Result { LSCompanyID = 0 });
                }
                else
                {
                    listOfMaster = (from detail in listOfDetail
                                    group detail by new { detail.LSCompanyID } into Master
                                    select new Timesheet_sprptOTRecord_Master_Result
                                    {
                                        LSCompanyID = Master.Key.LSCompanyID.HasValue ? Master.Key.LSCompanyID.Value : 0
                                    }).ToList();
                    if (listOfMaster != null)
                    {
                        foreach (var master in listOfMaster)
                        {
                            var company = this.db.LS_tblCompany.Where(p => p.LSCompanyID == master.LSCompanyID).FirstOrDefault();
                            if (company != null)
                            {
                                master.CompanyName = company.VNName;
                            }
                        }
                    }
                }
                // Setting data source of report                
                var dsMasterDetail = GenericsToDataSet.ConvertGenericList("OTRecordDS", listOfMaster, listOfDetail, "Timesheet_sprptOTRecord_Master", "Timesheet_sprptOTRecord");
                report.DataSource = dsMasterDetail;
                report.DataMember = dsMasterDetail.Tables[0].TableName;
                report.DetailReport.DataSource = dsMasterDetail;
                report.DetailReport.DataMember = String.Format("{0}.{1}", dsMasterDetail.Tables[0].TableName, dsMasterDetail.Relations[0].RelationName);
            }
            return report;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OTRecord29XtraReport CreateOTRecord29Report(Timesheet_sprptOTRecord_Result model)
        {
            var report = new OTRecord29XtraReport();
            // Setting header             
            report.Bands["ReportHeader"].Controls["xrLblMonth"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.Month, model.Month);
            report.Bands["ReportHeader"].Controls["xrLblYear"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.Year, model.Year);
            var CompanyName = String.Empty;
            if (model.LSCompanyID.HasValue)
            {
                var company = this.db.LS_tblCompany.Where(p => p.LSCompanyID == model.LSCompanyID.Value).FirstOrDefault();
                if (company != null)
                {
                    CompanyName = company.Name;
                }
            }
            report.Bands["ReportHeader"].Controls["xrLblCompany"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.LastCompanyDefine, CompanyName);
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
            var repository = new OTRecordReportRepository(this.db);
            var listOfDetail = repository.GetListOfOTRecord(model, out errorMessage);
            var listOfMaster = new List<Timesheet_sprptOTRecord_Master_Result>();

            if (listOfDetail != null)
            {
                // Get List of Master from Detail                
                if (listOfDetail.Count == 0)
                {
                    listOfMaster.Add(new Timesheet_sprptOTRecord_Master_Result { LSCompanyID = 0 });
                    listOfDetail.Add(new Timesheet_sprptOTRecord_Result { LSCompanyID = 0 });
                }
                else
                {
                    listOfMaster = (from detail in listOfDetail
                                    group detail by new { detail.LSCompanyID } into Master
                                    select new Timesheet_sprptOTRecord_Master_Result
                                    {
                                        LSCompanyID = Master.Key.LSCompanyID.HasValue ? Master.Key.LSCompanyID.Value : 0
                                    }).ToList();
                    if (listOfMaster != null)
                    {
                        foreach (var master in listOfMaster)
                        {
                            var company = this.db.LS_tblCompany.Where(p => p.LSCompanyID == master.LSCompanyID).FirstOrDefault();
                            if (company != null)
                            {
                                master.CompanyName = company.VNName;
                            }
                        }
                    }
                }
                // Setting data source of report                
                var dsMasterDetail = GenericsToDataSet.ConvertGenericList("OTRecordDS", listOfMaster, listOfDetail, "Timesheet_sprptOTRecord_Master", "Timesheet_sprptOTRecord");
                report.DataSource = dsMasterDetail;
                report.DataMember = dsMasterDetail.Tables[0].TableName;
                report.DetailReport.DataSource = dsMasterDetail;
                report.DetailReport.DataMember = String.Format("{0}.{1}", dsMasterDetail.Tables[0].TableName, dsMasterDetail.Relations[0].RelationName);
            }
            return report;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OTRecord28XtraReport CreateOTRecord28Report(Timesheet_sprptOTRecord_Result model)
        {
            var report = new OTRecord28XtraReport();
            // Setting header             
            report.Bands["ReportHeader"].Controls["xrLblMonth"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.Month, model.Month);
            report.Bands["ReportHeader"].Controls["xrLblYear"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.Year, model.Year);
            var CompanyName = String.Empty;
            if (model.LSCompanyID.HasValue)
            {
                var company = this.db.LS_tblCompany.Where(p => p.LSCompanyID == model.LSCompanyID.Value).FirstOrDefault();
                if (company != null)
                {
                    CompanyName = company.Name;
                }
            }
            report.Bands["ReportHeader"].Controls["xrLblCompany"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.LastCompanyDefine, CompanyName);
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
            var repository = new OTRecordReportRepository(this.db);
            var listOfDetail = repository.GetListOfOTRecord(model, out errorMessage);
            var listOfMaster = new List<Timesheet_sprptOTRecord_Master_Result>();

            if (listOfDetail != null)
            {
                // Get List of Master from Detail                
                if (listOfDetail.Count == 0)
                {
                    listOfMaster.Add(new Timesheet_sprptOTRecord_Master_Result { LSCompanyID = 0 });
                    listOfDetail.Add(new Timesheet_sprptOTRecord_Result { LSCompanyID = 0 });
                }
                else
                {
                    listOfMaster = (from detail in listOfDetail
                                    group detail by new { detail.LSCompanyID } into Master
                                    select new Timesheet_sprptOTRecord_Master_Result
                                    {
                                        LSCompanyID = Master.Key.LSCompanyID.HasValue ? Master.Key.LSCompanyID.Value : 0
                                    }).ToList();
                    if (listOfMaster != null)
                    {
                        foreach (var master in listOfMaster)
                        {
                            var company = this.db.LS_tblCompany.Where(p => p.LSCompanyID == master.LSCompanyID).FirstOrDefault();
                            if (company != null)
                            {
                                master.CompanyName = company.VNName;
                            }
                        }
                    }
                }
                // Setting data source of report                
                var dsMasterDetail = GenericsToDataSet.ConvertGenericList("OTRecordDS", listOfMaster, listOfDetail, "Timesheet_sprptOTRecord_Master", "Timesheet_sprptOTRecord");
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
        public ActionResult LoadOTRecordReportViewer()
        {                        
            var model = this.TempData["OTRecord"] as Timesheet_sprptOTRecord_Result;
            if (model == null)
            {
                model = new Timesheet_sprptOTRecord_Result();
            }            
            this.TempData["OTRecord"] = model;
            var numberOfDays = DateTime.DaysInMonth(model.Year, model.Month);
            if (numberOfDays == 31)
            {
                this.ViewData["Report"] = this.CreateOTRecord31Report(model); 
                return this.PartialView("../Timesheet/OTRecord/OTRecord31ReportViewer");
            }
            if (numberOfDays == 30)
            {
                this.ViewData["Report"] = this.CreateOTRecord30Report(model);
                return this.PartialView("../Timesheet/OTRecord/OTRecord30ReportViewer");
            }
            if (numberOfDays == 29)
            {
                this.ViewData["Report"] = this.CreateOTRecord29Report(model);
                return this.PartialView("../Timesheet/OTRecord/OTRecord29ReportViewer");
            }
            if (numberOfDays == 28)
            {
                this.ViewData["Report"] = this.CreateOTRecord28Report(model);
                return this.PartialView("../Timesheet/OTRecord/OTRecord28ReportViewer");
            }
            this.ViewData["Report"] = new OTRecord31XtraReport();
            return this.PartialView("../Timesheet/OTRecord/OTRecord31ReportViewer");
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult ExportOTRecordData()
        {
            var model = this.TempData["OTRecord"] as Timesheet_sprptOTRecord_Result;
            if (model == null)
            {
                model = new Timesheet_sprptOTRecord_Result();
            }
            this.TempData["OTRecord"] = model;
            var numberOfDays = DateTime.DaysInMonth(model.Year, model.Month);
            if (numberOfDays == 31)
            {
                return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(this.CreateOTRecord31Report(model));
            }
            if (numberOfDays == 30)
            {
                return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(this.CreateOTRecord30Report(model));
            }
            if (numberOfDays == 29)
            {
                return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(this.CreateOTRecord29Report(model));
            }
            if (numberOfDays == 28)
            {
                return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(this.CreateOTRecord28Report(model));
            }
            return this.Content("Error");
        }
         
    }


}
