using AutoMapper;
using Eagle.Common.Helpers;
using Eagle.Core;
using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class HospitalController : BaseController
    {
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../SI/Hospital/_Reset");
            }
            else
            {
                if (CurrentAcc == null)
                {
                    Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                    return null;
                }
                return View("../SI/Hospital/Index");
            }
        }
        // load danh sach grid nhom
        public ActionResult _List()
        {
            HospitalRepository _repository = new HospitalRepository(db);
            List<HospitalViewModel> list = _repository.GetAll();
            return PartialView("../SI/Hospital/_List", list);
        }
        public ActionResult _Create()
        {
            HospitalViewModel model = new HospitalViewModel();
            model.Used = true;
            return PartialView("../SI/Hospital/_Create", model);
        }

        public ActionResult _Edit(int id)
        {
            HospitalRepository _repository = new HospitalRepository(db);
            HospitalViewModel modelEntity = _repository.FindEdit(id);
            return PartialView("../SI/Hospital/_Create", modelEntity);
        }
        [HttpPost]
        public ActionResult _Delete(int id)
        {
            HospitalRepository _repository = new HospitalRepository(db);
            string errorMessage = "";
            if (_repository.Delete(id, out errorMessage))
            {
                return Content("success");
            }
            else
            {
                return Content(errorMessage);
            }
        }

        [HttpPost]
        public ActionResult Update(HospitalViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                if (ModelState.IsValid)
                {
                    HospitalRepository _repository = new HospitalRepository(db);

                    LS_tblHospital modelUpdate = new LS_tblHospital();
                    Mapper.CreateMap<HospitalViewModel, LS_tblHospital>();
                    Mapper.Map(model, modelUpdate);

                    if (_repository.Update(modelUpdate, out errorMessage))
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
                errorMessage = "Time out error!";
                return _Error(model, errorMessage);
            }

        }
        [HttpPost]
        public ActionResult Save(HospitalViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
          
            if (acc != null)
            {
                HospitalRepository _repository = new HospitalRepository(db);
                if (ModelState.IsValid)
                {
                    LS_tblHospital modelUpdate = new LS_tblHospital();
                    Mapper.CreateMap<HospitalViewModel, LS_tblHospital>();
                    Mapper.Map(model, modelUpdate);

                    if (_repository.Add(modelUpdate, out errorMessage))
                    {
                        return Content("success");
                    }
                    else
                    {
                        return _Error(model, errorMessage);
                    }
                }
            }
            else
            {
                errorMessage = "Time out error!";
                return _Error(model, errorMessage);
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
            }
            return _Error(model, errorMessage);
        }

        public ActionResult _Error(HospitalViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new HospitalViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;

            return PartialView("../SI/Hospital/_Create", model);
        }

        /// <summary>
        /// Dùng cho viec binding du lieu cho dropdownlist autocomplete
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DropdownList(string searchTerm, int pageSize, int pageNum)
        {
            HospitalRepository _repository = new HospitalRepository(db);
            List<AutoCompleteViewModel> sources = _repository.ListDropdown(searchTerm, pageSize, pageNum).ToList();
            int iTotal = sources.Count;

            //Translate the list into a format the select2 dropdown expects
            Select2PagedResultViewModel pagedList = ConvertListToSelect2Format(sources, iTotal);

            //Return the data as a jsonp result
            return new JsonpResult
            {
                Data = pagedList,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

    }
}
