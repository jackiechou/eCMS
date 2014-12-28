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
using Eagle.Common.Settings;
using Eagle.Repository.SYS.Roles;
namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class UserGroupController : BaseController
    {
        //
        // GET: /Admin/UserGroup/
        public ActionResult Index()
        {
            if (CurrentAcc == null)
            {
                Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                return null;
            }
            return View("../Sys/UserGroup/Index");
        }
        public ActionResult _Create()
        {
            UserRoleRepository _repository = new UserRoleRepository(db);
            IList<AccountViewModel> sources = _repository.ListBox().ToList();
            SelectList UserList = new SelectList(sources, "UserID", "UserName");
            ViewData["lstBox"] = UserList;
            // form load 
            IList<AccountViewModel> sources1 = new List<AccountViewModel>();
            SelectList UserListAssigned = new SelectList(sources1, "UserID", "UserName");
            ViewData["lstBoxAssigned"] = UserListAssigned;

            return PartialView("../Sys/UserGroup/_Create");
        }
        public JsonResult _ListUser(int? groupID)
        {
            try
            {
                UserRoleRepository _repository = new UserRoleRepository(db);
                var userList = _repository.ListUserNotIn(groupID).ToList();

                var retData = userList.Select(m => new SelectListItem()
                {
                    Text = m.UserName.ToString(),
                    Value = m.UserID.ToString(),
                });
                return Json(retData, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// tra ve danh sach account thuoc group 
        /// </summary>
        /// <param name="groupID">groupID</param>
        /// <returns>tra ve danh sach account</returns>
        public JsonResult _ListUserInGroup(int? groupID)
        {
            try
            {
                UserRoleRepository _repository = new UserRoleRepository(db);
                var userList = _repository.ListUserIn(groupID).ToList();

                var retData = userList.Select(m => new SelectListItem()
                {
                    Text = m.UserName.ToString(),
                    Value = m.UserID.ToString(),
                });
                return Json(retData, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Delete(int groupID, List<int> lstBoxAssigned)
        {
            SYS_tblUserGroup model = new SYS_tblUserGroup();
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                UserRoleRepository _repository = new UserRoleRepository(db);
                if (ModelState.IsValid)
                {
                    //SYS_tblUserGroup model = new SYS_tblUserGroup();
                    //{
                    //    GroupID = model.GroupID
                    //};
                    bool bResult = _repository.Delete(groupID, lstBoxAssigned);
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
                errorMessage += modelError.ErrorMessage + "-";
            }
            return RedirectToAction("_CreateError", "UserGroup", new { id = model, ErrorMessage = errorMessage });
        }
        public ActionResult Save(UserGroupCreateViewModel model, IEnumerable<string> lstBox)
        {
                
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            string errorMessage = "";
            if (acc != null)
            {
                UserRoleRepository _repository = new UserRoleRepository(db);
                if (ModelState.IsValid)
                {

                    SYS_tblUserGroup add = new SYS_tblUserGroup()
                    {
                        GroupID = model.GroupID
                    };
                    bool bResult = _repository.Add(add, lstBox);
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
                errorMessage += modelError.ErrorMessage + "-";
            }
            return RedirectToAction("_CreateError", "UserGroup", new { model = model, ErrorMessage = errorMessage });
        }
        public ActionResult _CreateError(SYS_tblUserGroup model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new SYS_tblUserGroup();
            }

            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = Eagle.Resource.LanguageResource.Warning;
            ViewBag.Message = ErrorMessage;

            return PartialView("../Sys/UserGroup/_Create", model);
        }

    }
}
