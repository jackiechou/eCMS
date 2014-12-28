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
    public class LS_tblRewardController : BaseController
    {
        //
        // GET: /Admin/LS_tblReward/
        public ActionResult Index()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc == null)
            {
                Response.Redirect("/");
                return null;
            }
            LS_tblRewardRespository _repository = new LS_tblRewardRespository(db);
            IList<LS_tblRewardViewModel> sources = _repository.List().ToList();
            return View(sources);
        }
        [HttpPost]
        public ActionResult Save(LS_tblRewardViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                LS_tblRewardRespository _repository = new LS_tblRewardRespository(db);
                if (ModelState.IsValid)
                {
                    LS_tblReward add = new LS_tblReward()
                    {
                        LSRewardCode = model.LSRewardCode,
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
        public ActionResult Update(LS_tblRewardViewModel model)
        {
            LS_tblRewardRespository _repository = new LS_tblRewardRespository(db);
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
        public ActionResult _Error(LS_tblRewardViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new LS_tblRewardViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;
            return PartialView("../HR_MasterData/_LS_tblRewardCreate", model);
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
            LS_tblRewardRespository _repository = new LS_tblRewardRespository(db);
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


        // Get Countries
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetRewards()
        {
            var list = db.LS_tblReward
                            .Select(p => new LS_tblRewardViewModel()
                            {
                                LSRewardID = p.LSRewardID,
                                LSRewardCode = p.LSRewardCode,
                                Name = p.Name,
                                VNName = p.VNName,
                                Rank = p.Rank,
                                Used = p.Used,
                                Note = p.Note
                            }).ToList();
            list.Insert(0, new LS_tblRewardViewModel() { LSRewardID = 0, Name = " --- " + @Eagle.Resource.LanguageResource.Select + " --- " });
            var retData = list.Select(m => new SelectListItem()
            {
                Text = (LanguageId == 124) ? m.Name : m.VNName,
                Value = m.LSRewardID.ToString(),
            });
            return Json(retData, JsonRequestBehavior.AllowGet);
        }

    }
}
