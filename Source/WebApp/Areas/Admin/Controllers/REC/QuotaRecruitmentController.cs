using AutoMapper;
using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Core;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class QuotaRecruitmentController : BaseController
    {
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../REC/QuotaRecruitment/_Reset");
            }
            else
            {
                if (CurrentAcc == null)
                {
                    Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                    return null;
                }
                return View("../REC/QuotaRecruitment/Index");
            }
        }

        public ActionResult _Create()
        {
            QuotaRecruitmentCreateViewModel model = new QuotaRecruitmentCreateViewModel();
            //PopulateCompanyDepartmentSectionCascadingDropdownlist();
            return PartialView("../REC/QuotaRecruitment/_Create", model);

        }
        // load danh sach grid nhom
        public ActionResult _List()
        {
            //CandidatetmpViewModel cadModel = new CandidatetmpViewModel();
            //if (Session["CandidateSelected"] != null)
            //{
            //    cadModel = (CandidatetmpViewModel)Session["CandidateSelected"];
            //}
            //WorkingBackgroundRepository _repository = new WorkingBackgroundRepository(db);
            //List<REC_tblWorkingBackground> list = _repository.GetBy(cadModel.CandidatetmpID);
            //return PartialView("../REC/WorkingBackground/_List", list);
            return null;
        }
        public ActionResult AddToGrid(QuotaRecruitmentCreateViewModel model, List<QuotaRecruitmentViewModel> list)
        {
            //return Content("success");
            if (ModelState.IsValid)
            {
                ViewBag.tmpYear = model.YearSearch;
                //Nếu chưa có khởi tạo
                if (list == null)
                {
                    list = new List<QuotaRecruitmentViewModel>();
                }
                //Nếu đã có kiểm tra 
                //=> đã tồn tại thông báo
                //=> chưa tồn tại add vào grid

                //1 kiểm tra nếu không tồn tại => làm tiếp add vào grid
                if (list.Where(p => p.Year == model.YearSearch.Value &&
                                p.LSCompanyID == model.LSCompanyIDSearch &&
                                p.LSPositionID == model.LSPositionIDSearch).FirstOrDefault() == null)
                {
                    QuotaRecruitmentViewModel modelForAdd = new QuotaRecruitmentViewModel()
                    {
                        RecruitedQuantity = 0,
                        QuotaQuantity = 0,

                        Year = model.YearSearch.Value,
                        LSCompanyID = model.LSCompanyIDSearch.Value,
                        LSCompanyName = model.LSCompanyNameSearch,
                        LSPositionID = model.LSPositionIDSearch.Value,
                        LSPositionName = model.LSPositionNameSearch
                    };
                    list.Add(modelForAdd);
                    return PartialView("../REC/QuotaRecruitment/_List", list);
                }
                else
                {
                    return Content("IsExists");
                }
            }
            return Content("Invalid");
        }
        public ActionResult _Delete(List<QuotaRecruitmentViewModel> list, int pLSCompanyID, int pLSPositionID, int tmpYear)
        {
            ViewBag.tmpYear = tmpYear;
            var itemRemove = list.Where(p => p.LSCompanyID == pLSCompanyID &&
                                            p.LSPositionID == pLSPositionID).FirstOrDefault();
            list.Remove(itemRemove);
            return PartialView("../REC/QuotaRecruitment/_List", list);
        }
        public ActionResult Save(List<QuotaRecruitmentViewModel> list, int tmpYear)
        {
            List<REC_tblQuota> ListForEdit = new List<REC_tblQuota>();
            QuotaRecruitmentRepository _repository = new QuotaRecruitmentRepository(db);
            string errorMessage = string.Empty;
            //Xóa tất cả
            if (list == null || list.Count == 0)
            {
                bool result = _repository.DeleteByYear(tmpYear, out errorMessage);
                if (result)
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
                //Cập nhật lại list
                foreach (var item in list)
                {
                    REC_tblQuota modelForEdit = new REC_tblQuota();
                    Mapper.CreateMap<QuotaRecruitmentViewModel, REC_tblQuota>();
                    modelForEdit = Mapper.Map<REC_tblQuota>(item);
                    ListForEdit.Add(modelForEdit);
                }
                bool result = _repository.Update(ListForEdit, tmpYear, out errorMessage);
                if (result)
                {
                    return Content("success");
                }
                else
                {
                    return Content(errorMessage);
                }
            }
            
        }
        public ActionResult GetListByYear(int year)
        {
            QuotaRecruitmentRepository _repository = new QuotaRecruitmentRepository(db);
            List<QuotaRecruitmentViewModel> list = _repository.GetQuotaRecruitmentListByYear(year,LanguageId);
            ViewBag.tmpYear = year;

            return PartialView("../REC/QuotaRecruitment/_List", list);
        }

    }
}
