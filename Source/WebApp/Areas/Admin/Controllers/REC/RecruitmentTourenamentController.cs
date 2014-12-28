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
    public class RecruitmentTournamentController : BaseController
    {
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../REC/RecruitmentTournament/_Reset");
            }
            else
            {
                if (CurrentAcc == null)
                {
                    Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                    return null;
                }
                return View("../REC/RecruitmentTournament/Index");
            }
        }
        // load danh sach grid nhom
        public ActionResult _List()
        {
            RecruitmentTournamentRepository _repository = new RecruitmentTournamentRepository(db);
            List<LS_tblRecruitmentTournament> list = _repository.GetAll();
            return PartialView("../REC/RecruitmentTournament/_List", list);
        }
        public ActionResult _Create()
        {
            LS_tblRecruitmentTournament model = new LS_tblRecruitmentTournament();
            model.Used = true;
            return PartialView("../REC/RecruitmentTournament/_Create", model);
        }

        public ActionResult _Edit(int id)
        {
            RecruitmentTournamentRepository _repository = new RecruitmentTournamentRepository(db);
            LS_tblRecruitmentTournament modelEntity = _repository.Find(id);
            return PartialView("../REC/RecruitmentTournament/_Create", modelEntity);
        }
        [HttpPost]
        public ActionResult _Delete(int id)
        {
            RecruitmentTournamentRepository _repository = new RecruitmentTournamentRepository(db);
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
        public ActionResult Update(LS_tblRecruitmentTournament model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                if (ModelState.IsValid)
                {
                    RecruitmentTournamentRepository _repository = new RecruitmentTournamentRepository(db);
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
        public ActionResult Save(LS_tblRecruitmentTournament model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
          
            if (acc != null)
            {
                RecruitmentTournamentRepository _repository = new RecruitmentTournamentRepository(db);
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

        public ActionResult _Error(LS_tblRecruitmentTournament model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new LS_tblRecruitmentTournament();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;

            return PartialView("../REC/RecruitmentTournament/_Create", model);
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
            RecruitmentTournamentRepository _repository = new RecruitmentTournamentRepository(db);
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
