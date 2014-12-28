using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using Eagle.Core;
using Eagle.Repository;
using Eagle.Model;
using Eagle.Model.HR;
using System.Web.Routing;
using Eagle.Common.Utilities;
using Eagle.Common.Helpers;
using Eagle.Common.Settings;
namespace Eagle.WebApp.Areas.Admin.Controllers
{
    public class CyclicController : BaseController
    {
        //
        // GET: /Admin/Cyclic/
        #region Master
            public ActionResult Index()
            {
                if (CurrentAcc == null)
                {
                    Response.Redirect("/Admin/Login?desiredUrl=" + Request.Url.AbsoluteUri);
                    return null;
                }
                if (Request.IsAjaxRequest())
                {
                    return PartialView("../Business/HumanResources/Timesheet/Cyclic/_Reset");
                }
                else
                {
                    return View("../Business/HumanResources/Timesheet/Cyclic/Index");
                }
            }
            public ActionResult _Create()
            {
                CyclicCreateViewModel model = new CyclicCreateViewModel();

                return PartialView("../Business/HumanResources/Timesheet/Cyclic/_Create", model);
            }
            public ActionResult _List()
            {
                AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
                if (acc == null)
                {
                    Response.Redirect("/");
                    return null;
                }

                CyclicRepository _repository = new CyclicRepository(db);
                IList<CyclicCreateViewModel> sources = _repository.List().ToList();
                return PartialView("../Business/HumanResources/Timesheet/Cyclic/_List", sources);
            }
            public ActionResult Save(CyclicCreateViewModel model)
            {
                AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
                string errorMessage = "";
                if (acc != null)
                {
                    CyclicRepository _repository = new CyclicRepository(db);
                    if (ModelState.IsValid)
                    {
                        Timesheet_tblCyclic add = new Timesheet_tblCyclic()
                        {
                            FromCyclic = model.FromCyclic,
                            ToCyclic = model.ToCyclic,
                            Note = model.Note
                        };
                        // add du lieu vao database
                        string bResult = _repository.Add(add);
                        if (bResult == "success")
                        {
                            return Content("success");
                        }
                        else
                        {
                            return _CreateError(model, bResult);
                        }

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
            public ActionResult Update(CyclicCreateViewModel model)
            {
                AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
                string errorMessage = "";
                if (acc != null)
                {
                    CyclicRepository _repository = new CyclicRepository(db);
                    if (ModelState.IsValid)
                    {
                        string bResult = _repository.Update(model);
                        if (bResult == "success")
                        {
                            return Content("success");
                        }
                        else
                        {
                            return _CreateError(model, bResult);
                        }

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
                CyclicRepository _repository = new CyclicRepository(db);
                bool bResult = _repository.Delete(id);
                return Content("success");
            }
            public ActionResult _Edit(int id)
            {
                CyclicRepository _repository = new CyclicRepository(db);
                CyclicCreateViewModel model = _repository.FindEdit(id);
                return PartialView("../Business/HumanResources/Timesheet/Cyclic/_Create", model);
            }
            public ActionResult _CreateError(CyclicCreateViewModel model, string ErrorMessage)
        {
            if (model == null)
            {
                model = new CyclicCreateViewModel();
            }
            ViewBag.DisplayErrorMessage = true;
            ViewBag.CssClass = "alert alert-warning";
            ViewBag.SortMessage = "Error";
            ViewBag.Message = ErrorMessage;
            return PartialView("../Business/HumanResources/Timesheet/Cyclic/_Create", model);

        }
        #endregion
        #region Popup
            // tra ve partial view popup
        public ActionResult _PopupDetail(int CyclicID)
            {
                AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
                if (acc == null)
                {
                    Response.Redirect("/");
                    return null;
                }
                int ModuleID = 9;

                CyclicRepository _repository = new CyclicRepository(db);
                // load len nhung nhan vien chua duoc gan vao chu ky cham comg
                IList<EmployeeViewModel> sources = _repository.ListBox(acc.FAdm == 1, acc.UserName, ModuleID).ToList();                
                SelectList EmpList = new SelectList(sources, "EmpID", "FullName");
                ViewData["lstBox"] = EmpList;

                // form load len nhung nhan vien co trong chu ky cham cong duoc chon
                IList<EmployeeViewModel> sources1 = _repository.ListBoxInCyclic(CyclicID,acc.FAdm == 1, acc.UserName, ModuleID).ToList();                
                SelectList EmpListAssigned = new SelectList(sources1, "EmpID", "FullName");
                ViewData["lstBoxAssigned"] = EmpListAssigned;
                return PartialView("../Business/HumanResources/Timesheet/Cyclic/_popupDetail", null);
            }
        public ActionResult SaveStaff(int CyclicID, IEnumerable<string> lstBox)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc != null)
            {
                bool bResult = true;
                CyclicRepository _repository = new CyclicRepository(db);
                if (ModelState.IsValid)
                {
                     bResult = _repository.AddDetail(CyclicID, lstBox);
                }
                if (bResult)
                    return Content("success");
                else
                    return Content("Error");
            }
            return Content("Error");
        }
        public ActionResult DeleteStaff(int CyclicID, List<int> lstBoxAssigned)
        {
            AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
            if (acc != null)
            {
                CyclicRepository _repository = new CyclicRepository(db);
                if (ModelState.IsValid)
                {
                    bool bResult = _repository.DeleteDetail(CyclicID,lstBoxAssigned);
                    if (bResult)
                    {
                        return Content("success");
                    }
                    else
                    {
                        return Content("error");
                    }

                }
            }
            return Content("error");
        }
        public ActionResult _Search(string SearchTerm)
        {
            try
            {
                AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
                if (acc == null)
                {
                    Response.Redirect("/");
                    return null;
                }
                int ModuleID = 9;
                CyclicRepository _repository = new CyclicRepository(db);
                var userList = _repository.SearchNotIn(SearchTerm, acc.FAdm == 1, acc.UserName, ModuleID).ToList();

                var retData = userList.Select(m => new SelectListItem()
                {
                    Text = m.FullName.ToString(),
                    Value = m.EmpID.ToString(),
                });
                return Json(retData, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult _SearchInCyclic(string SearchTerm, int CyclicID)
        {
            try
            {
                AccountViewModel acc = (AccountViewModel)Session[SettingKeys.AccountInfo];
                if (acc == null)
                {
                    Response.Redirect("/");
                    return null;
                }
                int ModuleID = 9;
                CyclicRepository _repository = new CyclicRepository(db);
                var userList = _repository.SearchInCyclic(SearchTerm,CyclicID, acc.FAdm == 1, acc.UserName, ModuleID).ToList();

                var retData = userList.Select(m => new SelectListItem()
                {
                    Text = m.FullName.ToString(),
                    Value = m.EmpID.ToString(),
                });
                return Json(retData, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

    }
}
