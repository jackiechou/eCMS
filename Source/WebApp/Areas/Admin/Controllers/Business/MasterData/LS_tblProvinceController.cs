using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using Eagle.Core;
using Eagle.Repository;
using Eagle.Model;

using System.Web.Routing;
using Eagle.Common.Helpers;
using Eagle.Common.Settings;
namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class LS_tblProvinceController : BaseController
    {
        //
        // GET: /Admin/LS_tblProvince/
       
        public ActionResult Index()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            LS_tblProvinceRepository _repository = new LS_tblProvinceRepository(db);
            IList<LS_tblProvinceViewModel> sources = _repository.List().ToList();
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
        public ActionResult DropdownList2(string searchTerm, int pageSize, int pageNum)
        {
            LS_tblProvinceRepository _repository = new LS_tblProvinceRepository(db);

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
        
        [HttpGet]
        public ActionResult DropdownList(string searchTerm, int? CountryID, int pageSize, int pageNum)
        {
            LS_tblProvinceRepository _repository = new LS_tblProvinceRepository(db);

            List<AutoCompleteModel> sources = _repository.ListDropdown(searchTerm, CountryID, pageSize, pageNum, LanguageId).ToList();
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
        public ActionResult Save(LS_tblProvinceViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                LS_tblProvinceRepository _repository = new LS_tblProvinceRepository(db);
                if (ModelState.IsValid)
                {
                    LS_tblProvince add = new LS_tblProvince()
                    {
                        LSProvinceCode = model.LSProvinceCode,
                        LSCountryID = model.LSCountryID,
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
        public ActionResult Update(LS_tblProvinceViewModel model)
        {
            LS_tblProvinceRepository _repository = new LS_tblProvinceRepository(db);

            string errorMessage = "";
            if (ModelState.IsValid)
            {
                if (_repository.Update(model, out errorMessage))
                {
                    return Content("success");
                }
                else
                {
                    return _Error(model, errorMessage);
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
            }
            return _Error(model, errorMessage);
        }

        public ActionResult _Error(LS_tblProvinceViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new LS_tblProvinceViewModel();
            }
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;
            return PartialView("../HR_MasterData/_LS_tblProvinceCreate", model);
        }
        // Get Provinces By Country Id
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetProvincesByCountryId(int? id)
        {
            try
            {                
                var province = db.LS_tblProvince
                    .Where(p => (int)p.LSCountryID == id)
                    .Select(p => new { p.LSProvinceID, p.Name, p.VNName })
                    .OrderBy(p => p.Name).ToList();

                var retData = province.Select(m => new SelectListItem()
                {
                    Text = (LanguageId == 124) ? m.Name : m.VNName,
                    Value = m.LSProvinceID.ToString(),
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
