using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.WebApp.Areas.Admin.Reports.SI.A01TS;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class A01TSController : BaseController
    {
        public ActionResult Index()
        {
            return View("../Business/HumanResources/SecurityInsurance/A01TS/Index");
        }
        public ActionResult _Create()
        {
            C66ASearchViewModel model = new C66ASearchViewModel();
            model.MonthYear = DateTime.Now;
            //Kỳ
            List<int> stageList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            ViewBag.Stage = new SelectList(stageList);
            //Company
            CompanyRepository _repository = new CompanyRepository(db);
            List<CompanyViewModel> companyList = _repository.GetByParent(null, LanguageId);
            ViewBag.LSCompanyID = new SelectList(companyList, "LSCompanyID", "Name");
            //Return
            return PartialView("../Business/HumanResources/SecurityInsurance/A01TS/_Create", model);
        }

        public ActionResult ReportViewerPartial(C66ASearchViewModel model)
        {
            ViewBag.FullName = model.FullName;
            ViewBag.Stage = model.Stage;
            ViewBag.MonthYear = model.MonthYear;
            ViewBag.LSCompanyID = model.LSCompanyID;
            ViewData["Report"] = new A01TSXtraReport();
            return PartialView("../Business/HumanResources/SecurityInsurance/A01TS/ReportViewerPartial");
        }
        public ActionResult CallbackReportViewerPartial(C66ASearchViewModel model)
        {

            ViewBag.FullName = model.FullName;
            ViewBag.Stage = model.Stage;
            ViewBag.MonthYear = model.MonthYear;
            ViewBag.LSCompanyID = model.LSCompanyID;

            ViewData["Report"] = CreateReport(model);
            return PartialView("../Business/HumanResources/SecurityInsurance/A01TS/ReportViewerPartial");
        }
        public ActionResult ExportReportViewerPartial(C66ASearchViewModel model)
        {
            A01TSXtraReport report = CreateReport(model);
            return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(report);
        }

        private A01TSXtraReport CreateReport(C66ASearchViewModel model)
        {
            A01TSXtraReport report = new A01TSXtraReport();
            DataSet ds = new DataSet();

            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
                {
                    cmd.CommandText = "SI_sprptA01ST";
                    cmd.Parameters.AddWithValue("@FullName", model.FullName);
                    cmd.Parameters.AddWithValue("@LSCompanyID", model.LSCompanyID);
                    cmd.Parameters.AddWithValue("@SIMonth", model.SIMonth);
                    cmd.Parameters.AddWithValue("@Stage", model.Stage);
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(cmd);

                    adapter.Fill(ds);

                    conn.Close();
                }
            }

            ds.Tables[0].TableName = "Detail";


            report.DataSource = ds;
            report.DataMember = "Detail";

            report.Name = "A01TS To khai tham gia " + "K" + model.Stage.ToString() + "-T" + model.SIMonth.Replace("/", "-");
            return report;
        }

    }
}
