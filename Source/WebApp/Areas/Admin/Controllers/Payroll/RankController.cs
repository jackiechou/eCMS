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
    public class RankController : BaseController
    {
        //
        // GET: /Admin/Rank/

        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("../Payroll/Rank/_Index");
            }
            return View("../Payroll/Rank/Index");
        }
        public ActionResult _Create()
        {
            RankViewModels model = new RankViewModels() { Used = true };
            return PartialView("../Payroll/Rank/_Create", model);
        }
        public ActionResult _List()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }

            RankRepository _repository = new RankRepository(db);
            IList<RankViewModels> sources = _repository.List(LanguageId).ToList();
            return PartialView("../Payroll/Rank/_List", sources);
        }
        public ActionResult _CreateError(RankViewModels model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new RankViewModels();
            }
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;

            return PartialView("../Payroll/Rank/_Create", model);

        }
        [HttpPost]
        public ActionResult Save(RankViewModels model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                RankRepository _repository = new RankRepository(db);
                if (ModelState.IsValid)
                {
                    PR_tblSalaryRank add = new PR_tblSalaryRank()
                    {
                        SalaryGradeID = model.SalaryGradeIDNull.Value,
                        SalaryRankCode = model.SalaryRankCode,
                        VNName = model.VNName,
                        Name = model.Name,
                        Coef = model.Coef,
                        Rank = model.Rank,
                        Used = model.Used,
                        Note = model.Note
                    };
                    // add du lieu vao database
                    string bResult = _repository.Add(add);
                    if (bResult == "success")
                        return Content("success");
                    else
                        return _CreateError(model, bResult);
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
            }
            return _CreateError(model, errorMessage);
        }
        [HttpPost]
        public ActionResult Update(RankViewModels model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                RankRepository _repository = new RankRepository(db);
                if (ModelState.IsValid)
                {
                    string bResult = _repository.Update(model);
                    if (bResult == "success")
                        return Content("success");
                    else
                        return _CreateError(model, bResult);

                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
            }
            return _CreateError(model, errorMessage);
        }
        [HttpPost]
        public ActionResult _Delete(int id)
        {
            RankRepository _repository = new RankRepository(db);
            bool bResult = _repository.Delete(id);
            return Content("success");
        }
        public ActionResult _Edit(int id)
        {
            RankRepository _repository = new RankRepository(db);
            RankViewModels model = _repository.FindEdit(id, LanguageId);
            return PartialView("../Payroll/Rank/_Create", model);
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
            RankRepository _repository = new RankRepository(db);
            List<AutoCompleteViewModel> sources = _repository.ListDropdown(searchTerm, pageSize, pageNum, LanguageId).ToList();
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
        [HttpGet]
        public ActionResult DropdownListByID(string searchTerm, int pageSize, int pageNum, int SalaryGradeID)
        {
            RankRepository _repository = new RankRepository(db);
            List<AutoCompleteViewModel> sources = _repository.ListDropdownByID(searchTerm, pageSize, pageNum, LanguageId, SalaryGradeID).ToList();
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
