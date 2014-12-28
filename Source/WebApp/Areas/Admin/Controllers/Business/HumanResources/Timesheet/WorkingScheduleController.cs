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
using Eagle.WebApp.Areas.Admin.Reports.TS.WorkingSchedule;


namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class WorkingScheduleController : BaseController
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
                return this.PartialView("../Business/HumanResources/Timesheet/WorkingSchedule/Reset");
            }
            else
            {
                return this.View("../Business/HumanResources/Timesheet/WorkingSchedule/Index");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var model = new Timesheet_sprptShiftOfWorkingSchedule_Result();
            this.CreateViewBag(model, DateTime.Now.Month, DateTime.Now.Year);
            return this.PartialView("../Business/HumanResources/Timesheet/WorkingSchedule/Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Report(Timesheet_sprptShiftOfWorkingSchedule_Result model, int? Month, int? Year)
        {
            if (!Month.HasValue)
            {
                model.Month = DateTime.Now.Month;
            }
            if (!Year.HasValue)
            {
                model.Year = DateTime.Now.Year;
            }
            this.TempData["WorkingSchedule"] = model;
            this.ViewData["Report"] = new WorkingSchedule31XtraReport();

            return this.PartialView("../Business/HumanResources/Timesheet/WorkingSchedule/WorkingSchedule31ReportViewer");
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateViewBag(Timesheet_sprptShiftOfWorkingSchedule_Result model, int Month, int Year)
        {
            List<int> months = new List<int>();
            for (int i = 1; i <= 12; i++)
            {
                months.Add(i);
            }
            ViewBag.Month = new SelectList(months, Month);
            var years = new List<int>();
            for (int i = DateTime.Now.Year - 10; i <= DateTime.Now.Year + 10; i++)
            {
                years.Add(i);
            }
            this.ViewBag.Year = new SelectList(years, Year);
        }

        #region CreateWorkingScheduleReport

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private WorkingSchedule31XtraReport CreateWorkingSchedule31Report(Timesheet_sprptShiftOfWorkingSchedule_Result model)
        {
            var report = new WorkingSchedule31XtraReport();
            // Setting header             
            report.Bands["ReportHeader"].Controls["xrLblMonth"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.Month, model.Month);
            report.Bands["ReportHeader"].Controls["xrLblYear"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.Year, model.Year);
            var CompanyName = String.Empty;
            var company = this.db.LS_tblCompany.Where(p => p.LSCompanyID == model.LSCompanyID).FirstOrDefault();
            if (company != null)
            {
                CompanyName = company.Name;
            }
            report.Bands["ReportHeader"].Controls["xrLblCompany"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.LastCompanyDefine, CompanyName);

            var errorMessage = String.Empty;
            var repository = new WorkingScheduleReportRepository(this.db);
            var listOfDetail = repository.GetListOfWorkingSchedule(model.Month, model.Year, model, out errorMessage);
            var listOfMaster = new List<Timesheet_sprptShiftOfWorkingSchedule_Master_Result>();
            if (listOfDetail != null)
            {
                if (listOfDetail.Count == 0)
                {
                    listOfMaster.Add(new Timesheet_sprptShiftOfWorkingSchedule_Master_Result { LSCompanyID = 0 });
                    listOfDetail.Add(new Timesheet_sprptShiftOfWorkingSchedule_Result { LSCompanyID = 0 });
                }
                else
                {
                    listOfMaster = (from detail in listOfDetail
                                    group detail by new { detail.LSCompanyID } into Master
                                    select new Timesheet_sprptShiftOfWorkingSchedule_Master_Result
                                    {
                                        LSCompanyID = Master.Key.LSCompanyID
                                    }).ToList();
                    foreach (var master in listOfMaster)
                    {
                        var found = this.db.LS_tblCompany.Where(p => p.LSCompanyID == master.LSCompanyID).FirstOrDefault();
                        if (company != null)
                        {
                            master.CompanyName = found.Name;
                        }
                    }
                }
                var dsMasterDetail = GenericsToDataSet.ConvertGenericList("WorkingScheduleDS", listOfMaster, listOfDetail, "Timesheet_sprptShiftOfWorkingSchedule_Master", "Timesheet_sprptShiftOfWorkingSchedule");
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
        private WorkingSchedule30XtraReport CreateWorkingSchedule30Report(Timesheet_sprptShiftOfWorkingSchedule_Result model)
        {
            var report = new WorkingSchedule30XtraReport();
            // Setting header             
            report.Bands["ReportHeader"].Controls["xrLblMonth"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.Month, model.Month);
            report.Bands["ReportHeader"].Controls["xrLblYear"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.Year, model.Year);
            var CompanyName = String.Empty;
            var company = this.db.LS_tblCompany.Where(p => p.LSCompanyID == model.LSCompanyID).FirstOrDefault();
            if (company != null)
            {
                CompanyName = company.Name;
            }
            report.Bands["ReportHeader"].Controls["xrLblCompany"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.LastCompanyDefine, CompanyName);

            var errorMessage = String.Empty;
            var repository = new WorkingScheduleReportRepository(this.db);
            var listOfDetail = repository.GetListOfWorkingSchedule(model.Month, model.Year, model, out errorMessage);
            var listOfMaster = new List<Timesheet_sprptShiftOfWorkingSchedule_Master_Result>();
            if (listOfDetail != null)
            {
                if (listOfDetail.Count == 0)
                {
                    listOfMaster.Add(new Timesheet_sprptShiftOfWorkingSchedule_Master_Result { LSCompanyID = 0 });
                    listOfDetail.Add(new Timesheet_sprptShiftOfWorkingSchedule_Result { LSCompanyID = 0 });
                }
                else
                {
                    listOfMaster = (from detail in listOfDetail
                                    group detail by new { detail.LSCompanyID } into Master
                                    select new Timesheet_sprptShiftOfWorkingSchedule_Master_Result
                                    {
                                        LSCompanyID = Master.Key.LSCompanyID
                                    }).ToList();
                    foreach (var master in listOfMaster)
                    {
                        var found = this.db.LS_tblCompany.Where(p => p.LSCompanyID == master.LSCompanyID).FirstOrDefault();
                        if (company != null)
                        {
                            master.CompanyName = found.Name;
                        }
                    }
                }
                var dsMasterDetail = GenericsToDataSet.ConvertGenericList("WorkingScheduleDS", listOfMaster, listOfDetail, "Timesheet_sprptShiftOfWorkingSchedule_Master", "Timesheet_sprptShiftOfWorkingSchedule");
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
        private WorkingSchedule29XtraReport CreateWorkingSchedule29Report(Timesheet_sprptShiftOfWorkingSchedule_Result model)
        {
            var report = new WorkingSchedule29XtraReport();
            // Setting header             
            report.Bands["ReportHeader"].Controls["xrLblMonth"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.Month, model.Month);
            report.Bands["ReportHeader"].Controls["xrLblYear"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.Year, model.Year);
            var CompanyName = String.Empty;
            var company = this.db.LS_tblCompany.Where(p => p.LSCompanyID == model.LSCompanyID).FirstOrDefault();
            if (company != null)
            {
                CompanyName = company.Name;
            }
            report.Bands["ReportHeader"].Controls["xrLblCompany"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.LastCompanyDefine, CompanyName);

            var errorMessage = String.Empty;
            var repository = new WorkingScheduleReportRepository(this.db);
            var listOfDetail = repository.GetListOfWorkingSchedule(model.Month, model.Year, model, out errorMessage);
            var listOfMaster = new List<Timesheet_sprptShiftOfWorkingSchedule_Master_Result>();
            if (listOfDetail != null)
            {
                if (listOfDetail.Count == 0)
                {
                    listOfMaster.Add(new Timesheet_sprptShiftOfWorkingSchedule_Master_Result { LSCompanyID = 0 });
                    listOfDetail.Add(new Timesheet_sprptShiftOfWorkingSchedule_Result { LSCompanyID = 0 });
                }
                else
                {
                    listOfMaster = (from detail in listOfDetail
                                    group detail by new { detail.LSCompanyID } into Master
                                    select new Timesheet_sprptShiftOfWorkingSchedule_Master_Result
                                    {
                                        LSCompanyID = Master.Key.LSCompanyID
                                    }).ToList();
                    foreach (var master in listOfMaster)
                    {
                        var found = this.db.LS_tblCompany.Where(p => p.LSCompanyID == master.LSCompanyID).FirstOrDefault();
                        if (company != null)
                        {
                            master.CompanyName = found.Name;
                        }
                    }
                }
                var dsMasterDetail = GenericsToDataSet.ConvertGenericList("WorkingScheduleDS", listOfMaster, listOfDetail, "Timesheet_sprptShiftOfWorkingSchedule_Master", "Timesheet_sprptShiftOfWorkingSchedule");
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
        private WorkingSchedule28XtraReport CreateWorkingSchedule28Report(Timesheet_sprptShiftOfWorkingSchedule_Result model)
        {
            var report = new WorkingSchedule28XtraReport();
            // Setting header             
            report.Bands["ReportHeader"].Controls["xrLblMonth"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.Month, model.Month);
            report.Bands["ReportHeader"].Controls["xrLblYear"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.Year, model.Year);
            var CompanyName = String.Empty;
            var company = this.db.LS_tblCompany.Where(p => p.LSCompanyID == model.LSCompanyID).FirstOrDefault();
            if (company != null)
            {
                CompanyName = company.Name;
            }
            report.Bands["ReportHeader"].Controls["xrLblCompany"].Text = String.Format("{0} : {1}", Eagle.Resource.LanguageResource.LastCompanyDefine, CompanyName);

            var errorMessage = String.Empty;
            var repository = new WorkingScheduleReportRepository(this.db);
            var listOfDetail = repository.GetListOfWorkingSchedule(model.Month, model.Year, model, out errorMessage);
            var listOfMaster = new List<Timesheet_sprptShiftOfWorkingSchedule_Master_Result>();
            if (listOfDetail != null)
            {
                if (listOfDetail.Count == 0)
                {
                    listOfMaster.Add(new Timesheet_sprptShiftOfWorkingSchedule_Master_Result { LSCompanyID = 0 });
                    listOfDetail.Add(new Timesheet_sprptShiftOfWorkingSchedule_Result { LSCompanyID = 0 });
                }
                else
                {
                    listOfMaster = (from detail in listOfDetail
                                    group detail by new { detail.LSCompanyID } into Master
                                    select new Timesheet_sprptShiftOfWorkingSchedule_Master_Result
                                    {
                                        LSCompanyID = Master.Key.LSCompanyID
                                    }).ToList();
                    foreach (var master in listOfMaster)
                    {
                        var found = this.db.LS_tblCompany.Where(p => p.LSCompanyID == master.LSCompanyID).FirstOrDefault();
                        if (company != null)
                        {
                            master.CompanyName = found.Name;
                        }
                    }
                }
                var dsMasterDetail = GenericsToDataSet.ConvertGenericList("WorkingScheduleDS", listOfMaster, listOfDetail, "Timesheet_sprptShiftOfWorkingSchedule_Master", "Timesheet_sprptShiftOfWorkingSchedule");
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
        public ActionResult LoadWorkingScheduleReportViewer()
        {
            var model = this.TempData["WorkingSchedule"] as Timesheet_sprptShiftOfWorkingSchedule_Result;
            if (model == null)
            {
                model = new Timesheet_sprptShiftOfWorkingSchedule_Result();
            }
            this.TempData["WorkingSchedule"] = model;
            var numberOfDays = DateTime.DaysInMonth(model.Year, model.Month);            

            if (numberOfDays == 31)
            {
                this.ViewData["Report"] = this.CreateWorkingSchedule31Report(model);
                return this.PartialView("../Business/HumanResources/Timesheet/WorkingSchedule/WorkingSchedule31ReportViewer");
            }
            if (numberOfDays == 30)
            {
                this.ViewData["Report"] = this.CreateWorkingSchedule30Report(model);
                return this.PartialView("../Business/HumanResources/Timesheet/WorkingSchedule/WorkingSchedule30ReportViewer");
            }
            if (numberOfDays == 29)
            {
                this.ViewData["Report"] = this.CreateWorkingSchedule29Report(model);
                return this.PartialView("../Business/HumanResources/Timesheet/WorkingSchedule/WorkingSchedule29ReportViewer");
            }
            if (numberOfDays == 28)
            {
                this.ViewData["Report"] = this.CreateWorkingSchedule28Report(model);
                return this.PartialView("../Business/HumanResources/Timesheet/WorkingSchedule/WorkingSchedule28ReportViewer");
            }
            this.ViewData["Report"] = new OTRecord31XtraReport();
            return this.PartialView("../Business/HumanResources/Timesheet/WorkingSchedule/WorkingSchedule31ReportViewer");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult ExportWorkingScheduleData()
        {
            var model = this.TempData["WorkingSchedule"] as Timesheet_sprptShiftOfWorkingSchedule_Result;
            if (model == null)
            {
                model = new Timesheet_sprptShiftOfWorkingSchedule_Result();
            }
            this.TempData["WorkingSchedule"] = model;
            var numberOfDays = DateTime.DaysInMonth(model.Year, model.Month);
            if (numberOfDays == 31)
            {
                return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(this.CreateWorkingSchedule31Report(model));
            }
            if (numberOfDays == 30)
            {
                return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(this.CreateWorkingSchedule30Report(model));
            }
            if (numberOfDays == 29)
            {
                return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(this.CreateWorkingSchedule29Report(model));
            }
            if (numberOfDays == 28)
            {
                return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(this.CreateWorkingSchedule28Report(model));
            }
            return this.Content("Error");
        }

    }
}
