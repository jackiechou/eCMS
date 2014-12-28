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
    public class C67AController : BaseController
    {
        //
        // GET: /Admin/C67A/

        public ActionResult Index()
        {
            return View("../Business/HumanResources/SecurityInsurance/C67A/Index");
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
            return PartialView("../Business/HumanResources/SecurityInsurance/C67A/_Create", model);
        }

        public ActionResult ReportViewerPartial(C66ASearchViewModel model)
        {
            ViewBag.FullName = model.FullName;
            ViewBag.Stage = model.Stage;
            ViewBag.MonthYear = model.MonthYear;
            ViewBag.LSCompanyID = model.LSCompanyID;
            ViewData["Report"] = new C67AXtraReport();
            return PartialView("../Business/HumanResources/SecurityInsurance/C67A/ReportViewerPartial");
        }
        public ActionResult CallbackReportViewerPartial(C66ASearchViewModel model)
        {

            ViewBag.FullName = model.FullName;
            ViewBag.Stage = model.Stage;
            ViewBag.MonthYear = model.MonthYear;
            ViewBag.LSCompanyID = model.LSCompanyID;

            ViewData["Report"] = CreateReport(model);
            return PartialView("../Business/HumanResources/SecurityInsurance/C67A/ReportViewerPartial");
        }
        public ActionResult ExportReportViewerPartial(C66ASearchViewModel model)
        {
            C67AXtraReport report = CreateReport(model);
            return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(report);
        }
        //public class LeaveType
        //{
        //    public string LSLeaveTypeCode { get; set; }
        //    public string Name { get; set; }
        //}
        //public class HeaderInfomation
        //{
        //    public string MaDonVi { get; set; }
        //    public string TenDonVi { get; set; }
        //    public string DiaChi { get; set; }
        //    public string DienThoai { get; set; }
        //    public string Fax { get; set; }
        //    public string SoHieuTK { get; set; }
        //    public string MoTai { get; set; }
        //    public int TongSoLaoDong { get; set; }
        //    public int TrongDoNu { get; set; }
        //    public decimal TongQuyLuongTrongThang { get; set; }
        //}
        private C67AXtraReport CreateReport(C66ASearchViewModel model)
        {

            C67AXtraReport report = new C67AXtraReport();
            DataSet ds = new DataSet();

            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
                {
                    cmd.CommandText = "SI_sprptC67A";
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
            report.Name = "C67A-HD - Danh sach nguoi lao dong de nghi huong che do thai san " + "K" + model.Stage.ToString() + "-T" + model.SIMonth.Replace("/", "-");
            return report;
        }

    }
}
