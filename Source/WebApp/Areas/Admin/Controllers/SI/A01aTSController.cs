using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.WebApp.Areas.Admin.Reports.SI.A01aTS;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class A01aTSController : BaseController
    {
        public ActionResult Index()
        {
            return View("../SI/A01aTS/Index");
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
            return PartialView("../SI/A01aTS/_Create", model);
        }

        public ActionResult ReportViewerPartial(C66ASearchViewModel model)
        {
            ViewBag.FullName = model.FullName;
            ViewBag.Stage = model.Stage;
            ViewBag.MonthYear = model.MonthYear;
            ViewBag.LSCompanyID = model.LSCompanyID;
            ViewData["Report"] = new A01aTSXtraReport();
            return PartialView("../SI/A01aTS/ReportViewerPartial");
        }
        public ActionResult CallbackReportViewerPartial(C66ASearchViewModel model)
        {

            ViewBag.FullName = model.FullName;
            ViewBag.Stage = model.Stage;
            ViewBag.MonthYear = model.MonthYear;
            ViewBag.LSCompanyID = model.LSCompanyID;

            ViewData["Report"] = CreateReport(model);
            return PartialView("../SI/A01aTS/ReportViewerPartial");
        }
        public ActionResult ExportReportViewerPartial(C66ASearchViewModel model)
        {
            A01aTSXtraReport report = CreateReport(model);
            return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(report);
        }

        private A01aTSXtraReport CreateReport(C66ASearchViewModel model)
        {
            A01aTSXtraReport report = new A01aTSXtraReport();
            DataSet ds = new DataSet();

            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
                {
                    cmd.CommandText = "SI_sprptA01AST";
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

            ds.Tables[0].TableName = "DetailsTemplate";
            ds.Tables[1].TableName = "HeaderInfomation";


            report.DataSource = ds;
            report.DataMember = "DetailsTemplate";

            report.Name = "A01a-TS - Danh sach NLD de nghi cap so BHXH - the BHYT " + "K" + model.Stage.ToString() + "-T" + model.SIMonth.Replace("/", "-");
            return report;
        }
    }
}
