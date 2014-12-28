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
    public class LS_tblLocationController : BaseController
    {

        //
        // GET: /Admin/LS_tblLocation/

        public ActionResult Index()
        {            
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                return Content(Eagle.Resource.LanguageResource.TimeOutError);
            }
            LS_tblLocationRepository _repository = new LS_tblLocationRepository(db);
            IList<LS_tblLocationViewModel> sources = _repository.ListLocation().ToList();
            return View(sources);
        }
        [HttpPost]
        public ActionResult Save(LS_tblLocationViewModel locationModel)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                LS_tblLocationRepository _repository = new LS_tblLocationRepository(db);
                if (ModelState.IsValid)
                {
                    LS_tblLocation LocationAdd = new LS_tblLocation()
                    {
                        LSLocationCode = locationModel.LSLocationCode,
                        
                        Name = locationModel.Name,
                        VNName = locationModel.VNName,
                        Rank = locationModel.Rank,
                        Used = locationModel.Used,
                        Note = locationModel.Note
                    };
                    bool bResult = _repository.AddLocation(LocationAdd, out errorMessage);
                    if (bResult)
                    {
                        return Content("success");
                    }
                    else
                    {
                        return _Error(locationModel, errorMessage);
                    }
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
            }
            return _Error(locationModel, errorMessage);
        }
        [HttpPost]
        public ActionResult Update(LS_tblLocationViewModel locationModel)
        {
            LS_tblLocationRepository _repository = new LS_tblLocationRepository(db);

             string errorMessage = "";
             if (ModelState.IsValid)
             {
                 if (_repository.Update(locationModel, out errorMessage))
                 {
                     return Content("success");
                 }
                 else
                 {
                     return _Error(locationModel, errorMessage);
                 }
             }
             var errors = ModelState.Values.SelectMany(v => v.Errors);
             foreach (var modelError in errors)
             {
                 errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
             }

             return _Error(locationModel, errorMessage);
        }
        public ActionResult _Error(LS_tblLocationViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new LS_tblLocationViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;
            return PartialView("../HR_MasterData/_LS_tblLocationCreate", model);
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
            LS_tblLocationRepository _repository = new LS_tblLocationRepository(db);
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


        // Get Countries
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetLocations()
        {
            var list = db.LS_tblLocation
                            .Select(p => new LS_tblLocationViewModel()
                            {
                                LSLocationID = p.LSLocationID,
                                LSLocationCode = p.LSLocationCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note,
                                strUsed = p.Used == true ? "X" : ""
                            }).ToList();
            list.Insert(0, new LS_tblLocationViewModel() { LSLocationID = 0, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });
            var retData = list.Select(m => new SelectListItem()
            {
                Text = (LanguageId == 124) ? m.Name : m.VNName,
                Value = m.LSLocationID.ToString(),
            });
            return Json(retData, JsonRequestBehavior.AllowGet);
        }
    }
}
