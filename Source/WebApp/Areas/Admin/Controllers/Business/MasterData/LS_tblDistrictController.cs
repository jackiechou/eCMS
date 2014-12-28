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
    public class LS_tblDistrictController : BaseController
    {
        //
        // GET: /Admin/LS_tblDistrict/

        public ActionResult Index()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            LS_tblDistrictRepository _repository = new LS_tblDistrictRepository(db);
            IList<LS_tblDistrictViewModel> sources = _repository.List().ToList();
            return View(sources);
        }

        [HttpPost]
        public ActionResult Save(LS_tblDistrictViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                LS_tblDistrictRepository _repository = new LS_tblDistrictRepository(db);
                if (ModelState.IsValid && model.LSProvinceID != null)
                {
                    LS_tblDistrict add = new LS_tblDistrict()
                    {
                        LSDistrictCode = model.LSDistrictCode,
                        LSProvinceID = Convert.ToInt32(model.LSProvinceID),
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
        public ActionResult Update(LS_tblDistrictViewModel model)
        {
            LS_tblDistrictRepository _repository = new LS_tblDistrictRepository(db);

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
        public ActionResult _Error(LS_tblDistrictViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new LS_tblDistrictViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;
            return PartialView("../HR_MasterData/_LS_tblDistrictCreate", model);
        }
        /// <summary>
        /// Dùng cho viec binding du lieu cho dropdownlist autocomplete
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DropdownList(string searchTerm, int? ProvinceID, int pageSize, int pageNum)
        {
            LS_tblDistrictRepository _repository = new LS_tblDistrictRepository(db);
            List<AutoCompleteModel> sources = _repository.ListDropdown(searchTerm, ProvinceID, pageSize, pageNum, LanguageId).ToList();
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

        

        #region  District ------------------------------------
        // Get District By First Province of country
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetDistrictsByFirstProvince(int id)
        {
            try
            {
                var province = db.LS_tblProvince.Where(p => p.LSCountryID == id).OrderBy(p => p.Name).FirstOrDefault();
                if (province != null)
                {
                    var districtList = db.LS_tblDistrict
                        .Where(p => (int)p.LSProvinceID == province.LSProvinceID)
                        .Select(p => new LS_tblDistrictViewModel {
                            LSDistrictID = p.LSDistrictID,
                            Name = p.Name + "(" + p.LSDistrictCode + ")",
                            VNName = p.VNName + "(" + p.LSDistrictCode + ")"
                        }).OrderBy(p => p.Name)
                        .ToList();

                    var retData = districtList.Select(m => new SelectListItem()
                    {
                        Text = (LanguageId == 124) ? m.Name : m.VNName,
                        Value = m.LSDistrictID.ToString(),
                    });
                    return Json(retData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }

            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        // Get Districts By Province Id
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetDistrictsByProvinceId(int id)
        {
            try
            {
                //int proid = Convert.ToInt32(provinceid);
                var districtList = db.LS_tblDistrict
                    .Where(p => (int)p.LSProvinceID == id)
                    .Select(p => new { p.LSDistrictID, DistrictName = p.Name + "(" + p.LSDistrictCode + ")" })
                    .OrderBy(p => p.DistrictName).ToList();

                var retData = districtList.Select(m => new SelectListItem()
                {
                    Text = m.DistrictName,
                    Value = m.LSDistrictID.ToString(),
                });
                return Json(retData, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}
