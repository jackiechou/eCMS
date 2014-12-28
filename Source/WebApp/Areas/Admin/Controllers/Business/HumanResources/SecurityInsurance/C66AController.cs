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
using Eagle.WebApp.Areas.Admin.Reports.SI.C66A;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class C66AController : BaseController
    {
        //
        // GET: /Admin/C66A/

        public ActionResult Index()
        {
            return View("../Business/HumanResources/SecurityInsurance/C66A/Index");
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
            return PartialView("../Business/HumanResources/SecurityInsurance/C66A/_Create", model);
        }

        public ActionResult ReportViewerPartial(C66ASearchViewModel model)
        {
            ViewBag.FullName = model.FullName;
            ViewBag.Stage = model.Stage;
            ViewBag.MonthYear = model.MonthYear;
            ViewBag.LSCompanyID = model.LSCompanyID;
            ViewData["Report"] = new C66AXtraReport();
            return PartialView("../Business/HumanResources/SecurityInsurance/C66A/ReportViewerPartial");
        }
        public ActionResult CallbackReportViewerPartial(C66ASearchViewModel model)
        {

            ViewBag.FullName = model.FullName;
            ViewBag.Stage = model.Stage;
            ViewBag.MonthYear = model.MonthYear;
            ViewBag.LSCompanyID = model.LSCompanyID;

            ViewData["Report"] = CreateReport(model);
            return PartialView("../Business/HumanResources/SecurityInsurance/C66A/ReportViewerPartial");
        }
        public ActionResult ExportReportViewerPartial(C66ASearchViewModel model)
        {
            C66AXtraReport report = CreateReport(model);
            return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(report);
        }
        
        private C66AXtraReport CreateReport(C66ASearchViewModel model)
        {
            C66AXtraReport report = new C66AXtraReport();
            DataSet ds = new DataSet();

            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
                {
                    cmd.CommandText = "SI_sprptC66A";
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

            ds.Tables[0].TableName = "SI_sprptC66A";
            ds.Tables[1].TableName = "LeaveType";
            ds.Tables[2].TableName = "HeaderInfomation";
            DataRelation relation = new DataRelation("SI_sprptC66A_LeaveType", ds.Tables["LeaveType"].Columns["LSLeaveTypeCode"], ds.Tables["SI_sprptC66A"].Columns["LSLeaveTypeCode"]);
            

            ds.Relations.Add(relation);



            report.DataSource = ds;
            report.DataMember = "LeaveType";
            report.DetailReport.DataSource = ds;
            report.DetailReport.DataMember = "LeaveType.SI_sprptC66A_LeaveType";
            report.Name = "C66a-HD - Danh sach nguoi lao dong de nghi huong che do om dau " + "K" + model.Stage.ToString() + "-T" + model.SIMonth.Replace("/", "-");
            return report;
        }

    }
}
