using Eagle.Common.Utilities;
using Eagle.Repository;
using Eagle.Repository.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model.LS;
using Eagle.Model.HR;
using Eagle.Core;
using Eagle.Repository.LS;
using Eagle.Model;
using Eagle.Common.Helpers;
using Eagle.Common.Settings;

namespace Eagle.WebApp.Areas.Admin.Controller
{
    public class LS_tblContractTypeController : BaseController
    {
        //
        // GET: /Admin/LS_tblContractType/

        public ActionResult Index()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            LS_tblContractTypeRespository _repository = new LS_tblContractTypeRespository(db);
            IList<LS_tblContractTypeViewModel> sources = _repository.List().ToList();
            return View(sources);
        }


        [HttpPost]
        public ActionResult Save(LS_tblContractTypeViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                LS_tblContractTypeRespository _repository = new LS_tblContractTypeRespository(db);
                if (ModelState.IsValid)
                {
                    bool bResult = _repository.Add(model, out errorMessage);
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
        public ActionResult Update(LS_tblContractTypeViewModel model)
        {
            LS_tblContractTypeRespository _repository = new LS_tblContractTypeRespository(db);

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
        public ActionResult _Error(LS_tblContractTypeViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new LS_tblContractTypeViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;
            return PartialView("../HR_MasterData/_LS_tblContractTypeCreate", model);
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
            LS_tblContractTypeRespository _repository = new LS_tblContractTypeRespository(db);
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
        public JsonResult GetTrainingTypes()
        {
            var list = db.LS_tblContractType
                            .Select(p => new LS_tblContractTypeViewModel()
                            {
                                LSContractTypeID = p.LSContractTypeID,
                                LSContractTypeCode = p.LSContractTypeCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note
                            }).ToList();
            list.Insert(0, new LS_tblContractTypeViewModel() { LSContractTypeID = 0, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });
            var retData = list.Select(m => new SelectListItem()
            {
                Text = (LanguageId == 124) ? m.Name : m.VNName,
                Value = m.LSContractTypeID.ToString(),
            });
            return Json(retData, JsonRequestBehavior.AllowGet);
        }
    
    }
}
