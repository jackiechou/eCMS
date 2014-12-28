using Eagle.Common.Helpers;
using Eagle.Core;
using Eagle.Repository.LS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model;
using Eagle.Model.LS;
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controllers.LS
{
    public class LS_tblCurrencyController : BaseController
    {
        //
        // GET: /Admin/LS_tblCurrency/

        public ActionResult Index()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            LS_tblCurrencyRespository _repository = new LS_tblCurrencyRespository(db);
            IList<LS_tblCurrencyViewModel> sources = _repository.List().ToList();
            return View(sources);
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
            LS_tblCurrencyRespository _repository = new LS_tblCurrencyRespository(db);
            List<AutoCompleteModel> sources = _repository.ListDropdown(searchTerm, pageSize, pageNum, LanguageId).ToList();
            int iTotal = sources.Count;

            //Translate the list into a format the select2 dropdown expects
            Select2PagedResultViewModel pagedList = ConvertAutoListToSelect2Format(sources, iTotal);

            //Return the data as a jsonp result
            return new JsonpResult
            {
                Data = pagedList,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpPost]
        public ActionResult Save(LS_tblCurrencyViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                LS_tblCurrencyRespository _repository = new LS_tblCurrencyRespository(db);
                if (ModelState.IsValid)
                {
                    LS_tblCurrency add = new LS_tblCurrency()
                    {
                        LSCurrencyCode = model.LSCurrencyCode,
                        Name = model.Name,
                        VNName = model.VNName,
                        Rank = model.Rank,
                        Used = model.Used,
                        Note = model.Note
                    };
                    bool bResult = _repository.Add(add, out errorMessage);
                    if (bResult)
                    {
                        return Content("success");
                    }
                    else
                    {
                        return _Error(model, errorMessage);
                    }
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
            }

            return _Error(model, errorMessage);
        }


        [HttpPost]
        public ActionResult Update(LS_tblCurrencyViewModel model)
        {
            LS_tblCurrencyRespository _repository = new LS_tblCurrencyRespository(db);
            string errorMessage = "";
            if (ModelState.IsValid)
            {
                if (_repository.Update(model, out errorMessage))
                {
                    return Content("success");
                }
                else
                {
                    return RedirectToAction("_CreateLS_tblCurrencyError", "HR_MasterData", new { id = model, ErrorMessage = errorMessage });
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
            }

            return _Error(model, errorMessage);
        }
        public ActionResult _Error(LS_tblCurrencyViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new LS_tblCurrencyViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;
            return PartialView("../HR_MasterData/_LS_tblCurrencyCreate", model);
        }
        // Get Companies
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetBanks()
        {
            try
            {
                var lst = db.LS_tblCurrency
                    .Select(p => new LS_tblCurrencyViewModel()
                    {
                        LSCurrencyID = p.LSCurrencyID,
                        LSCurrencyCode = p.LSCurrencyCode,
                        Name = p.Name,
                        VNName = p.VNName
                    }).OrderBy(p => p.Name).ToList();

                var retData = lst.Select(m => new SelectListItem()
                {
                    Text = (LanguageId == 124) ? m.Name : m.VNName,
                    Value = m.LSCurrencyID.ToString(),
                });
                return Json(retData, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
