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
    public class RecruitmentFeeController : BaseController
    {
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../REC/RecruitmentFee/_Reset");
            }
            else
            {
                if (CurrentAcc == null)
                {
                    Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                    return null;
                }
                return View("../REC/RecruitmentFee/Index");
            }
        }
        // load danh sach grid nhom
        public ActionResult _List()
        {
            RecruitmentFeeRepository _repository = new RecruitmentFeeRepository(db);
            List<LS_tblRecruitmentFee> list = _repository.GetAll();
            return PartialView("../REC/RecruitmentFee/_List", list);
        }
        public ActionResult _Create()
        {
            LS_tblRecruitmentFee model = new LS_tblRecruitmentFee();
            model.Used = true;
            return PartialView("../REC/RecruitmentFee/_Create", model);
        }

        public ActionResult _Edit(int id)
        {
            RecruitmentFeeRepository _repository = new RecruitmentFeeRepository(db);
            LS_tblRecruitmentFee modelEntity = _repository.Find(id);
            return PartialView("../REC/RecruitmentFee/_Create", modelEntity);
        }
        [HttpPost]
        public ActionResult _Delete(int id)
        {
            RecruitmentFeeRepository _repository = new RecruitmentFeeRepository(db);
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
        public ActionResult Update(LS_tblRecruitmentFee model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                if (ModelState.IsValid)
                {
                    RecruitmentFeeRepository _repository = new RecruitmentFeeRepository(db);
                    if (_repository.Update(model, out errorMessage))
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
        public ActionResult Save(LS_tblRecruitmentFee model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
          
            if (acc != null)
            {
                RecruitmentFeeRepository _repository = new RecruitmentFeeRepository(db);
                if (ModelState.IsValid)
                {
                    if (_repository.Add(model, out errorMessage))
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

        public ActionResult _Error(LS_tblRecruitmentFee model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new LS_tblRecruitmentFee();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;

            return PartialView("../REC/RecruitmentFee/_Create", model);
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
            RecruitmentFeeRepository _repository = new RecruitmentFeeRepository(db);
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
