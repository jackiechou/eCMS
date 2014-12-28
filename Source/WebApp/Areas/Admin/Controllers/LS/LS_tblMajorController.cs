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
    public class LS_tblMajorController : BaseController
    {
        //
        // GET: /Admin/LS_tblMajor/

        public ActionResult Index()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            LS_tblMajorRepository _repository = new LS_tblMajorRepository(db);
            IList<LS_tblMajorViewModel> sources = _repository.List().ToList();
            return View(sources);
        }


        [HttpPost]
        public ActionResult Save(LS_tblMajorViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                LS_tblMajorRepository _repository = new LS_tblMajorRepository(db);
                if (ModelState.IsValid)
                {
                    LS_tblMajor add = new LS_tblMajor()
                    {
                        LSMajorCode = model.LSMajorCode,
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
        public ActionResult Update(LS_tblMajorViewModel model)
        {
            LS_tblMajorRepository _repository = new LS_tblMajorRepository(db);
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
        public ActionResult _Error(LS_tblMajorViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new LS_tblMajorViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;
            return PartialView("../HR_MasterData/_LS_tblMajorCreate", model);
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
            LS_tblMajorRepository _repository = new LS_tblMajorRepository(db);
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


        // Get Majors
        [HttpGet]
        public JsonResult GetMajorSubjects()
        {
            var list = db.LS_tblMajor
                            .Select(p => new LS_tblMajorViewModel()
                            {
                                LSMajorID = p.LSMajorID,
                                LSMajorCode = p.LSMajorCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note
                            }).OrderBy(p => p.Name).ToList();
            //list.Insert(0, new LS_tblMajorViewModel() { LSMajorID = 0, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });
            var retData = list.Select(m => new SelectListItem()
            {
                Text = (LanguageId == 124) ? m.Name : m.VNName,
                Value = m.LSMajorID.ToString(),
            });
            return Json(retData, JsonRequestBehavior.AllowGet);
        }
    }
}
