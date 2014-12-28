using AutoMapper;
using EntityModels;
using Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using WebUI.Areas.Administration.Reports.SI.D07TS;

namespace WebUI.Areas.Administration.Controllers
{
    public class D07TSController : BaseController
    {
        public ActionResult Index(string mode, int? MasterID)
        {
            if (string.IsNullOrEmpty(mode))
            {
                return View("../SI/D07TS/Index");
            }
            else if (mode == "viewreport")
            {
                ViewBag.Title = "Danh sách đề nghị thay đổi thông tin của người tham gia BHXH, BHYT, BHTN";
                return View("../SI/D07TS/Report/Index", MasterID);
            }
            else if (mode == "add")
            {
                return View("../SI/D07TS/Add/Index");
            }
            else if (mode == "edit")
            {
                ViewBag.MasterID = MasterID;
                return View("../SI/D07TS/Add/Index");
            }
            else
            {
                return View("../SI/D07TS/Index");
            }
            
        }

        public ActionResult _Create()
        {
            //Company
            CompanyRepository _repository = new CompanyRepository(db);
            List<CompanyViewModel> companyList = _repository.GetByParent(null, LanguageId);
            ViewBag.LSCompanyID = new SelectList(companyList, "LSCompanyID", "Name");
            return PartialView("../SI/D07TS/_Create");
        }

        public ActionResult _MasterList(int? LSCompanyID, DateTime? MonthYear, bool? isNotified)
        {
            ChangingInsuranceInformationMasterRepository _repository = new ChangingInsuranceInformationMasterRepository(db);
            List<ChangingInsuranceInformationMasterViewModel> list = _repository.GetList(LSCompanyID, MonthYear, isNotified);
            return PartialView("../SI/D07TS/_MasterList", list);
        }

        public ActionResult _UpdateIsNotified(int MasterID)
        {
            string errorMessage = "";
            ChangingInsuranceInformationMasterRepository _repository = new ChangingInsuranceInformationMasterRepository(db);
            if (_repository.UpdateIsNotified(MasterID, out errorMessage))
            {
                return Content("success");
            }
            else
            {
                return Content(errorMessage);
            }
        }
        public ActionResult _Delete(int MasterID)
        {
            string errorMessage = "";
            ChangingInsuranceInformationMasterRepository _repository = new ChangingInsuranceInformationMasterRepository(db);
            if (_repository.Delete(MasterID, out errorMessage))
            {
                return Content("success");
            }
            else
            {
                return Content(errorMessage);
            }
        }

        #region ViewReport
        public ActionResult ReportViewerPartial(int? MasterID)
        {
            ViewBag.MasterID = MasterID;
            ViewData["Report"] = new D07TSXtraReport();
            return PartialView("../SI/D07TS/Report/ReportViewerPartial");
        }
        public ActionResult CallbackReportViewerPartial(int? MasterID)
        {
            ViewBag.MasterID = MasterID;
            ViewData["Report"] = CreateReport(MasterID);
            return PartialView("../SI/D07TS/Report/ReportViewerPartial");
        }
        public ActionResult ExportReportViewerPartial(int? MasterID)
        {
            D07TSXtraReport report = CreateReport(MasterID);
            return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(report);
        }
        private D07TSXtraReport CreateReport(int? MasterID)
        {

            D07TSXtraReport report = new D07TSXtraReport();
            DataSet ds = new DataSet();

            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
                {
                    cmd.CommandText = "SI_sprptD07ST";
                    cmd.Parameters.AddWithValue("@MasterID", MasterID);
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
            report.Name = "D07-TS - Danh sach de nghi thay doi thong tin";
            return report;
        }
        #endregion

        #region Add

        public ActionResult _AddCreate()
        {
            D07TSViewModel model = new D07TSViewModel();
            return PartialView("../SI/D07TS/Add/_AddCreate",model);
        }
        public ActionResult _Search()
        {
            C66ASearchViewModel model = new C66ASearchViewModel();
            model.MonthYear = DateTime.Now;
            //Company
            CompanyRepository _repository = new CompanyRepository(db);
            List<CompanyViewModel> companyList = _repository.GetByParent(null, LanguageId);
            ViewBag.LSCompanyID = new SelectList(companyList, "LSCompanyID", "Name");

            return PartialView("../SI/D07TS/Add/_Search", model);
        }

        public ActionResult _List(C66ASearchViewModel model = null, int? MasterID = null, string mode = null)
        {
            List<D07TSViewModel> list = new List<D07TSViewModel>();

            if (string.IsNullOrEmpty(mode))
            {
                //add
                if (model != null && model.LSCompanyID != null)
                {
                    list = db.SI_spfrmGetChangingInsuranceInfomation(model.LSCompanyID, model.MonthYear.Value.Month, model.MonthYear.Value.Year)
                                               .Select(p => new D07TSViewModel()
                                               {
                                                   EmpID = p.EmpID,
                                                   FullName = p.FullName,
                                                   Old = p.Old,
                                                   New = p.New,
                                                   ChangeTypeName = p.ChangeDetail,
                                                   SIBook = p.SIBook,
                                                   ChangeTypeID = p.ChangeTypeID
                                               }).ToList();
                }
            }
            else
            { 
                //edit
                ChangingInsuranceInformationDetailRepository _repository = new ChangingInsuranceInformationDetailRepository(db);
                list = _repository.GetByMasterID(MasterID);

            }
            return PartialView("../SI/D07TS/Add/_List", list);
        }
        public ActionResult _AddToGrid(List<D07TSViewModel> list,D07TSViewModel model)
        {
            if (list == null)
            {
                list = new List<D07TSViewModel>();
            }
            list.Add(model);
            return PartialView("../SI/D07TS/Add/_List", list);
        }
        public ActionResult _SaveGrid(List<D07TSViewModel> list, int LSCompanyID, DateTime MonthYear, string Name, int? MasterID = null, string mode = null)
        {
            Name = "Báo cáo tháng " + MonthYear.ToString("MM/yyyy");
            ChangingInsuranceInformationMasterRepository _repository = new ChangingInsuranceInformationMasterRepository(db);
            SI_tblChangingInsuranceInformationMaster masterModel;
            if (MasterID == null || MasterID == 0)
            {
                masterModel = new SI_tblChangingInsuranceInformationMaster();
	        }
            else
            {
                masterModel = _repository.Find(MasterID);
                masterModel.SI_tblChangingInsuranceInformationDetail.Clear();
            }
            
            masterModel.LSCompanyID = LSCompanyID;
            masterModel.CreateTime = DateTime.Now;
            masterModel.CreateUser = CurrentAcc.UserName;
            masterModel.isNotified = false;
            masterModel.Name = Name;
            masterModel.Month = MonthYear.Month;
            masterModel.Year = MonthYear.Year;

            if (list != null && list.Count > 0)
            {
                Mapper.CreateMap<D07TSViewModel, SI_tblChangingInsuranceInformationDetail>();

                foreach (var item in list)
                {
                    //Kiểm tra dữ liệu
                    if (item.EmpID == null || item.ChangeTypeID == null)
                    {
                        return Content("Vui lòng điền đầy đủ thông tin \"Họ và Tên\" và \"Nội dung đề nghị thay đổi\" trong bảng đề nghị thay đổi.");
                    }


                    SI_tblChangingInsuranceInformationDetail model = new SI_tblChangingInsuranceInformationDetail();
                    Mapper.Map(item, model);
                    if (item.FullName != null && item.FullName.LastIndexOf("-") >= 0)
                    {
                        model.FullName = model.FullName.Substring(0, item.FullName.LastIndexOf("-") - 1);
                    }
                    masterModel.SI_tblChangingInsuranceInformationDetail.Add(model);
                }
                string errorMessage = "";
                
                if (_repository.Update(masterModel, out errorMessage))
                {
                    return Content("success");
                }
                else
                {
                    return Content(errorMessage);
                }
            }
            else
            {
                return Content("Vui lòng nhập dữ liệu cho bảng đề nghị thay đổi trước khi lưu.");
            }
        }
        public ActionResult _DeleteList(List<D07TSViewModel> list)
        {
            if (list == null)
            {
                list = new List<D07TSViewModel>();
            }
            //List<D07TSViewModel> ret = new List<D07TSViewModel>();
            //foreach (var p in list.Where(p => p.Checked == false).ToList())
            //{
            //    D07TSViewModel model = new D07TSViewModel()
            //    {
            //        SIBook = p.SIBook,
            //        BasedChange = p.BasedChange,
            //        Checked = false,
            //        ChangeTypeID = p.ChangeTypeID,
            //        ChangeTypeName = p.ChangeTypeName,
            //        DetailID = p.DetailID,
            //        //EmpID = p.EmpID,
            //        FromMonth = p.FromMonth,
            //        FullName = p.FullName,
            //        MasterID = p.MasterID,
            //        New = p.New,
            //        Old = p.Old,                   
            //        ToMonth = p.ToMonth
            //    };
            //    ret.Add(model);
            //}
            return PartialView("../SI/D07TS/Add/_List", list.Where(p => p.Checked == false).ToList());
        }
        public ActionResult _AddRow(List<D07TSViewModel> list)
        {
            if (list == null)
            {
                list = new List<D07TSViewModel>();
            }
            list.Add(new D07TSViewModel());
            return PartialView("../SI/D07TS/Add/_List", list);
        }

        //Hàm lấy thông số "số sổ BHXH"
        public ActionResult _GetSIBook(int? EmpID)
        {
            var sibook = db.SI_tblInsuranceInformation.Where(p => p.EmpID == EmpID).Select(p => p.SIBook).FirstOrDefault();
            return Content(sibook);
        }
        #endregion

    }
}
