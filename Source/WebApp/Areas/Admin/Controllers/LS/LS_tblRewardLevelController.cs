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

namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class LS_tblRewardLevelController : BaseController
    {
        //
        // GET: /Admin/LS_tblRewardLevelLevel/

        //
        // GET: /Admin/LS_tblRewardLevel/
        public ActionResult Index()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            LS_tblRewardLevelRespository _repository = new LS_tblRewardLevelRespository(db);
            IList<LS_tblRewardLevelViewModel> sources = _repository.List().ToList();
            return View(sources);
        }
        [HttpPost]
        public ActionResult Save(LS_tblRewardLevelViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                LS_tblRewardLevelRespository _repository = new LS_tblRewardLevelRespository(db);
                if (ModelState.IsValid)
                {
                    LS_tblRewardLevel add = new LS_tblRewardLevel()
                    {
                        LSRewardLevelCode = model.LSRewardLevelCode,
                        Name = model.Name,
                        VNName = model.VNName,
                        Rank = model.Rank,
                        Used = model.Used,
                        Note = model.Note
                    };
                    string strResult = _repository.Add(add);
                    if (strResult == "success")
                    {
                        return Content("success");
                    }
                    else
                    {
                        errorMessage = strResult;
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
        public ActionResult Update(LS_tblRewardLevelViewModel model)
        {
            LS_tblRewardLevelRespository _repository = new LS_tblRewardLevelRespository(db);
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
        public ActionResult _Error(LS_tblRewardLevelViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new LS_tblRewardLevelViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;
            return PartialView("../HR_MasterData/_LS_tblRewardLevelCreate", model);
        }

        /// <summary>
        /// Dùng cho viec binding du lieu cho dropdownlist autocomplete
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DropdownList(string searchTerm, int? Type, int pageSize, int pageNum)
        {
            LS_tblRewardLevelRespository _repository = new LS_tblRewardLevelRespository(db);
            List<AutoCompleteModel> sources = _repository.ListDropdown(searchTerm, Type, pageSize, pageNum, LanguageId).ToList();
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


        // Get RewardLevels ============================================================================================
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetRewardLevels(int? Type)
        {
            var list = db.LS_tblRewardLevel.Where(p=>p.Type == Type)
                            .Select(p => new LS_tblRewardLevelViewModel()
                            {
                                LSRewardLevelID = p.LSRewardLevelID,
                                LSRewardLevelCode = p.LSRewardLevelCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note
                            }).ToList();
            //list.Insert(0, new LS_tblRewardLevelViewModel() { LSRewardLevelID = 0, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });
            var retData = list.Select(m => new SelectListItem()
            {
                Text = (LanguageId == 124) ? m.Name : m.VNName,
                Value = m.LSRewardLevelID.ToString(),
            });
            return Json(retData, JsonRequestBehavior.AllowGet);
        }
        //=============================================================================================================
    }
}
