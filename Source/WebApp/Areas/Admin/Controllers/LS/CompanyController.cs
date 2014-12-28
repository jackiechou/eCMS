using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Core;
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class CompanyController : BaseController
    {
        //
        // GET: /Admin/Company/

        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                if (CurrentAcc == null)
                {
                    return Content("timeout");
                }
                return PartialView("../HR_MasterData/Company/_Reset");
            }
            else
            {

                if (CurrentAcc == null)
                {
                    Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                    return null;
                }
                return View("../HR_MasterData/Company/Index");
            }
            
        }

        public ActionResult _List()
        {
            CompanyRepository _repository = new CompanyRepository(db);
            List<CompanyViewModel> list = _repository.GetAll();
            return PartialView("../HR_MasterData/Company/_List", list);
        }
        public ActionResult _Create()
        {
            CompanyViewModel company = new CompanyViewModel();
            company.Used = true;
            CreateViewBag();
            return PartialView("../HR_MasterData/Company/_Create", company);
        }
        public ActionResult _Edit(int id)
        {
            CompanyRepository _repository = new CompanyRepository(db);
            CompanyViewModel model = _repository.FindEdit(id);
            CreateViewBag(model.LSCompanyDefineID);
            return PartialView("../HR_MasterData/Company/_Create", model);
        }

        [HttpPost]
        public ActionResult Update(CompanyViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                CompanyRepository _repository = new CompanyRepository(db);
                if (ModelState.IsValid)
                {
                    model.Editor = acc.UserName;
                    if (_repository.Update(model, out errorMessage))
                    {
                        //cập nhật thành công
                        return Content("success");
                    }
                    else
                    {
                        //cập nhật không thành công
                        return _Error(model, errorMessage);
                    }
                }
                else
                {
                    // Lỗi model valid
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    foreach (var modelError in errors)
                    {
                        errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
                    }
                    return _Error(model, errorMessage);
                }
            }
            else
            {
                //time out
                errorMessage = Eagle.Resource.LanguageResource.TimeOutError;
                return _Error(model, errorMessage);
            }
        }
        [HttpPost]
        public ActionResult Save(CompanyViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                CompanyRepository _repository = new CompanyRepository(db);
                if (ModelState.IsValid)
                {
                    LS_tblCompany modelAdd = new LS_tblCompany();
                    AutoMapper.Mapper.CreateMap<CompanyViewModel, LS_tblCompany>();
                    AutoMapper.Mapper.Map(model, modelAdd);

                    // khởi tạo giá trị mặc định
                    modelAdd.Creater = acc.UserName;
                    modelAdd.CreateTime = DateTime.Now;

                    if (_repository.Add(modelAdd, out errorMessage))
                    {
                        return Content("success");
                    }
                    else
                    {
                        return _Error(model, errorMessage);
                    }
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    foreach (var modelError in errors)
                    {
                        errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
                    }
                    return _Error(model, errorMessage);
                }
            }
            else
            {
                errorMessage = Eagle.Resource.LanguageResource.TimeOutError;
                return _Error(model, errorMessage);
            }
        }

        public ActionResult _Error(CompanyViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new CompanyViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;
            CreateViewBag(model.LSCompanyDefineID);
            return PartialView("../HR_MasterData/Company/_Create", model);
        }

        private void CreateViewBag(int? CompanyDefineChecked = 1)
        {
            CompanyDefineRepository _repository = new CompanyDefineRepository(db);
            List<CompanyDefineViewModel> CompanyDefineList = _repository.GetAll(LanguageId);
            ViewBag.CompanyDefineList = CompanyDefineList;
            ViewBag.CompanyDefineChecked = CompanyDefineChecked;
        }

    }
}
