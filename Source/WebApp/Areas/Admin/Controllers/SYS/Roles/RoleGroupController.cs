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
using Eagle.Repository.SYS.Roles;
namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class GroupController : BaseController
    {
        //
        // GET: /Admin/Group/
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("../SYS/Group/_Reset");
            }
            else
            {
                if (CurrentAcc == null)
                {
                    Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                    return null;
                }
                return View("../SYS/Group/Index");
            }
        }
        // load danh sach grid nhom
        public ActionResult _List()
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc != null)
            {
                RoleRepository _repository = new RoleRepository(db);
                IList<GroupViewModel> sources = _repository.List().ToList();
                return PartialView("../SYS/Group/_List", sources);
            }
            else
            {
                return Content("TimeOut");
            }
        }
        public ActionResult _Create()
        {
            GroupCreateViewModel acc = new GroupCreateViewModel();
            return PartialView("../SYS/Group/_Create", acc);
        }
        public ActionResult _Edit(int id)
        {
            RoleRepository _repository = new RoleRepository(db);
            GroupCreateViewModel model = _repository.FindEdit(id);
            return PartialView("../SYS/Group/_Create", model);
        }

        [HttpPost]
        public ActionResult _Delete(int id)
        {
            RoleRepository _repository = new RoleRepository(db);
            bool bResult = _repository.Delete(id);
            return Content("success");
        }
        [HttpPost]
        public ActionResult Update(GroupCreateViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                RoleRepository _repository = new RoleRepository(db);
                if (ModelState.IsValid)
                {
                    if (_repository.Update(model, out errorMessage))
                    {
                        return Content("success");
                    }
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
            }
            return _CreateSYS_tblGroupError(model,errorMessage);
        }
        [HttpPost]
        public ActionResult Save(GroupCreateViewModel model)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                RoleRepository _repository = new RoleRepository(db);
                if (ModelState.IsValid)
                {
                    SYS_tblGroup add = new SYS_tblGroup()
                    {
                        GroupCode = model.GroupCode,
                        GroupName = model.GroupName,
                        Used = model.Used,
                        Note = model.Note
                    };
                    bool bResult = _repository.Add(add);
                    if (bResult)
                    {
                        return Content("success");
                    }
                    else
                    {
                        errorMessage = "Loi";
                    }

                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in errors)
            {
                errorMessage += "&lt;br /&gt;" + modelError.ErrorMessage;
            }
            return _CreateSYS_tblGroupError(model,errorMessage);
        }
        
        public ActionResult _CreateSYS_tblGroupError(GroupCreateViewModel acc, string ErrorMessage)
        {
            if (acc == null)
            {
                acc = new GroupCreateViewModel();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;

            return PartialView("../SYS/Group/_Create", acc);
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
            RoleRepository _repository = new RoleRepository(db);
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
