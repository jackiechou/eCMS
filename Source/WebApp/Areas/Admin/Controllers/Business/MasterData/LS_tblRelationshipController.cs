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
    public class LS_tblRelationshipController : BaseController
    {
        //
        // GET: /Admin/LS_tblRelationship/

        public ActionResult Index()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            LS_tblRelationshipRepository _repository = new LS_tblRelationshipRepository(db);
            IList<LS_tblRelationshipViewModel> sources = _repository.List().ToList();
            return View(sources);
        }
        [HttpPost]
        public ActionResult Save(LS_tblRelationshipViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string Message = "";
            if (acc != null)
            {
                LS_tblRelationshipRepository _repository = new LS_tblRelationshipRepository(db);
                if (ModelState.IsValid)
                {
                    bool bResult = _repository.Add(model, out Message);
                    if (bResult)
                        return Content("success");
                    else
                        return _Error(model, Message);
                }
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                Message += modelError.ErrorMessage;
            }
            return _Error(model, Message);
        }


        [HttpPost]
        public ActionResult Update(LS_tblRelationshipViewModel model)
        {
            LS_tblRelationshipRepository _repository = new LS_tblRelationshipRepository(db);
            string Message = "";
            if (ModelState.IsValid)
            {
                bool bResult = _repository.Update(model, out Message);
                if (bResult)
                    return Content("success");
                else
                    return _Error(model, Message);               
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                Message += "&lt;br /&gt;" + modelError.ErrorMessage;
            }

            return _Error(model, Message);
        }
        public ActionResult _Error(LS_tblRelationshipViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new LS_tblRelationshipViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;
            return PartialView("../HR_MasterData/_LS_tblRelationshipCreate", model);
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
            LS_tblRelationshipRepository _repository = new LS_tblRelationshipRepository(db);
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
        public JsonResult GetRewards()
        {
            var list = db.LS_tblRelationship
                            .Select(p => new LS_tblRelationshipViewModel()
                            {
                                LSRelationshipID = p.LSRelationshipID,
                                LSRelationshipCode = p.LSRelationshipCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note
                            }).ToList();
            list.Insert(0, new LS_tblRelationshipViewModel() { LSRelationshipID = 0, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });
            var retData = list.Select(m => new SelectListItem()
            {
                Text = (LanguageId == 124) ? m.Name : m.VNName,
                Value = m.LSRelationshipID.ToString(),
            });
            return Json(retData, JsonRequestBehavior.AllowGet);
        }
    }
}
