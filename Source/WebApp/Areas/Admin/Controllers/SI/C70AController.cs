using Eagle.Common.Extensions;
using Eagle.Core;
using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.WebApp.Areas.Admin.Reports.SI.C70A;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class C70AController : BaseController
    {
        //
        // GET: /Admin/C70A/

        public ActionResult Index()
        {
            return View("../SI/C70A/Index");
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
            return PartialView("../SI/C70A/_Create", model);
        }

        public ActionResult ReportViewerPartial(C66ASearchViewModel model)
        {
            ViewBag.FullName = model.FullName;
            ViewBag.Stage = model.Stage;
            ViewBag.MonthYear = model.MonthYear;
            ViewBag.LSCompanyID = model.LSCompanyID;
            ViewData["Report"] = new C70AXtraReport();
            return PartialView("../SI/C70A/ReportViewerPartial");
        }
        public ActionResult CallbackReportViewerPartial(C66ASearchViewModel model)
        {

            ViewBag.FullName = model.FullName;
            ViewBag.Stage = model.Stage;
            ViewBag.MonthYear = model.MonthYear;
            ViewBag.LSCompanyID = model.LSCompanyID;

            ViewData["Report"] = CreateReport(model);
            return PartialView("../SI/C70A/ReportViewerPartial");
        }
        public ActionResult ExportReportViewerPartial(C66ASearchViewModel model)
        {
            C70AXtraReport report = CreateReport(model);
            return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(report);
        }
        private C70AXtraReport CreateReport(C66ASearchViewModel model)
        {

            C70AXtraReport report = new C70AXtraReport();
            DataSet ds = new DataSet();

            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
                {
                    cmd.CommandText = "SI_sprptC70A";
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
            report.Name = "C70A-HD - Danh sach nguoi lao dong de nghi huong tro cap nghi DSPHSK sau TNLD - BNN " + "K" + model.Stage.ToString() + "-T" + model.SIMonth.Replace("/", "-");
            return report;
        }

    }
}
